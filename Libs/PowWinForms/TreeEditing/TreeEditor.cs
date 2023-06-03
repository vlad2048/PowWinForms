using System.Reactive.Linq;
using System.Reactive.Subjects;
using BrightIdeasSoftware;
using PowMaybe;
using PowRxVar;
using PowWinForms.TreeEditing.Structs;
using PowWinForms.TreeEditing.Utils;

namespace PowWinForms.TreeEditing;

public static class TreeEditor
{
	public static IDisposable Setup<T>(
		out IRoVar<Maybe<TNod<T>>> tree,
		out IRoVar<Maybe<TNod<T>>> selNode,
		out ITreeEvtSig<T> evtSig,
		out ITreeEvtObs<T> evtObs,
		TreeListView ctrl
	)
	{
		var d = new Disp();

		TreeCtrlOps.SetNodGeneric<T>(ctrl);
		var treeVar = Var.Make(May.None<TNod<T>>()).D(d);
		tree = treeVar;
		(evtSig, evtObs) = TreeEvt<T>.Make().D(d);
		TreeCtrlOps.GetSelectedNode<T>(out var selNodeRaw, ctrl).D(d);

		UpdateTreeAndCtrlOnEvents(out var whenSelNodeChanged, evtObs, treeVar, ctrl).D(d);

		selNode = Var.Make(
			May.None<TNod<T>>(),
			Observable.Merge(
				selNodeRaw,
				evtObs.WhenTreeLoaded().Select(_ => May.None<TNod<T>>()),
				evtObs.WhenTreeUnloaded().Select(_ => May.None<TNod<T>>()),
				whenSelNodeChanged
			)
		).D(d);

		return d;
	}

	private static IDisposable UpdateTreeAndCtrlOnEvents<T>(
		out IObservable<Maybe<TNod<T>>> whenSelNodeChanged,
		ITreeEvtObs<T> evtObs,
		IRwVar<Maybe<TNod<T>>> tree,
		TreeListView ctrl
	)
	{
		var d = new Disp();

		ISubject<Maybe<TNod<T>>> selNodeSubj = new Subject<Maybe<TNod<T>>>().D(d);
		whenSelNodeChanged = selNodeSubj.AsObservable();

		evtObs.WhenTreeLoaded().Subscribe(e =>
		{
			L("[TreeLoaded]");
			tree.V = May.Some(e.Root);
			TreeCtrlOps.NotifyTreeLoaded(ctrl, e.Root);
			ctrl.SelectedIndices.Clear();
		}).D(d);

		evtObs.WhenTreeUnloaded().Subscribe(_ =>
		{
			L("[TreeUnloaded]");
			TreeCtrlOps.NotifyTreeUnloaded(ctrl);
		}).D(d);

		evtObs.WhenNodeAdded().Subscribe(e =>
		{
			L("[NodeAdded]");
			var node = Nod.Make(e.NodeContent);
			if (tree.V.IsNone(out var root)) throw new ArgumentException("Cannot add a parented node on an empty tree");
			if (!root.Contains(e.Parent)) throw new ArgumentException("Cannot find the parent to add a node under");
			e.Parent.AddChild(node);
			TreeCtrlOps.NotifyNodeAddedAndSelectIt(ctrl, e.Parent, node);
		}).D(d);

		evtObs.WhenNodeRemoved().Subscribe(e =>
		{
			L("[NodeRemoved]");
			if (tree.V.IsNone(out var root)) throw new ArgumentException("Cannot remove a node on an empty tree");
			if (!root.Contains(e.Node)) throw new ArgumentException("Cannot find the node to remove");
			var parent = e.Node.Parent;
			if (parent == null) throw new ArgumentException("Cannot remove the root node");
			parent.RemoveChild(e.Node);
			selNodeSubj.OnNext(May.None<TNod<T>>());
			TreeCtrlOps.NotifyNodeRemoved(ctrl, e.Node);
		}).D(d);


		/*evtObs.WhenNodeChanged()
			.Buffer(TimeSpan.FromMilliseconds(500))
			.Subscribe(list =>
			{
				foreach (var e in list)
				{
					L("[NodeChanged]");
					if (tree.V.IsNone(out var root)) throw new ArgumentException("Cannot change a node on an empty tree");
					if (!root.Contains(e.Node)) throw new ArgumentException("Cannot find the node to change");
					e.Node.ChangeContent(e.NodeContent);
					TreeCtrlOps.NotifyNodeChanged(ctrl, e.Node);
				}
			}).D(d);*/

		/*evtObs.WhenNodeChanged().Subscribe(e =>
		{
			L("[NodeChanged]");
			if (tree.V.IsNone(out var root)) throw new ArgumentException("Cannot change a node on an empty tree");
			if (!root.Contains(e.Node)) throw new ArgumentException("Cannot find the node to change");
			e.Node.ChangeContent(e.NodeContent);
			TreeCtrlOps.NotifyNodeChanged(ctrl, e.Node);
		}).D(d);*/

		return d;
	}

	// ReSharper disable once UnusedParameter.Local
	private static void L(string s)
	{
		//Debug.WriteLine(s);
	}
}