using System.Diagnostics;
using System.Reactive.Linq;
using BrightIdeasSoftware;
using PowBasics.CollectionsExt;
using PowMaybe;
using PowRxVar;
using PowTrees.Algorithms;
using PowWinForms.TreeEditing;
using PowWinForms.TreeEditing.Structs;
using PowWinForms.Utils;

namespace Demos.Demos.TreeEditorDemo;

record Rec(string Name);


static class TreeEditorLogic
{
	private static TNod<Rec> M(Rec r, params TNod<Rec>[] kids) => Nod.Make(r, kids);

	private static readonly TNod<Rec> loadTree =
		M(new Rec("Vlad"),
			M(new Rec("Erik"),
				M(new Rec("Milou")),
				M(new Rec("Sylvain"))
			),
			M(new Rec("Philippe"),
				M(new Rec("Goncalo")),
				M(new Rec("Jaeton"))
			)
		);

	public static IDisposable Init(TreeEditorWin ui)
	{
		var d = new Disp();

		TreeEditor.Setup<Rec>(
			out var tree,
			out var selNode,
			out var evtSig,
			out var evtObs,
			ui.treeCtrl
		).D(d);

		SetupColumns(ui.treeCtrl);
		SetupContextMenu(ui, evtSig, selNode).D(d);
		SetupButtons(ui, evtSig).D(d);
		SetupNodeEditor(ui, selNode, evtSig).D(d);

		selNode.Subscribe(mayNode =>
		{
			Debug.WriteLine($"SelNode <- {mayNode}");
		}).D(d);

		tree
			.ObserveOnWinFormsUIThread()
			.Subscribe(mayVal =>
			{
				var str = mayVal.IsSome(out var val) switch
				{
					false => new[] { "None" },
					true => val!.LogToStrings()
				};
				ui.treeText.Text = str.JoinText(Environment.NewLine);
			}).D(d);

		return d;
	}


	private static void SetupColumns(TreeListView ctrl)
	{
		ctrl.Columns.Add(new OLVColumn("Node", "Node")
		{
			FillsFreeSpace = true,
			AspectGetter = obj =>
			{
				if (obj is not TNod<Rec> nod) return "_";
				return nod.V.Name;
			}
		});
	}


	private static IDisposable SetupContextMenu(
		TreeEditorWin ui,
		ITreeEvtSig<Rec> evtSig,
		IRoVar<Maybe<TNod<Rec>>> selNode
	)
	{
		var d = new Disp();

		ui.treeContextMenu.Events().Opening.Subscribe(e =>
		{
			var mayNode = selNode.V;
			if (mayNode.IsNone(out var node))
			{
				e.Cancel = true;
				return;
			}
			ui.addNodeMenuItem.Enabled = true;
			ui.removeNodeMenuItem.Enabled = node.Parent != null;
		}).D(d);

		ui.addNodeMenuItem.Events().Click.Subscribe(_ =>
		{
			var mayNode = selNode.V;
			if (mayNode.IsNone(out var node)) return;
			evtSig.SignalNodeAdded(node, new Rec("new node"));
		}).D(d);

		Observable.Merge(
			ui.removeNodeMenuItem.Events().Click.ToUnit(),
			ui.treeCtrl.Events().KeyDown.Where(e => e.KeyCode == Keys.Delete).ToUnit()
		)
			.Where(_ => selNode.V.IsSome(out var node) && node.Parent != null)
			.Subscribe(_ =>
			{
				var node = selNode.V.Ensure();
				evtSig.SignalNodeRemoved(node);
			}).D(d);

		return d;
	}


	private static IDisposable SetupButtons(TreeEditorWin ui, ITreeEvtSig<Rec> evtSig)
	{
		var d = new Disp();

		ui.loadBtn.Events().Click.Subscribe(_ =>
		{
			evtSig.SignalTreeLoaded(loadTree);
		}).D(d);

		ui.clearBtn.Events().Click.Subscribe(_ =>
		{
			evtSig.SignalTreeUnloaded();
		}).D(d);

		return d;
	}


	private static IDisposable SetupNodeEditor(TreeEditorWin ui, IRoVar<Maybe<TNod<Rec>>> selNode, ITreeEvtSig<Rec> evtSig)
	{
		var d = new Disp();

		var disableEvents = false;

		selNode.Subscribe(mayNode =>
		{
			disableEvents = true;
			if (mayNode.IsSome(out var node))
			{
				ui.nameText.Enabled = true;
				ui.nameText.Text = node.V.Name;
			}
			else
			{
				ui.nameText.Enabled = false;
				ui.nameText.Text = string.Empty;
			}
			disableEvents = false;
		}).D(d);

		ui.nameText.Events().TextChanged
			.Where(_ => !disableEvents)
			.Subscribe(_ =>
			{
				if (selNode.V.IsNone(out var node)) return;
				evtSig.SignalNodeChanged(node, new Rec(ui.nameText.Text));
			}).D(d);

		return d;
	}
}