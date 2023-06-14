using System.Reactive.Linq;
using BrightIdeasSoftware;
using PowMaybe;
using PowRxVar;

namespace PowWinForms.TreeEditing.Utils;

public static class TreeCtrlOps
{
	public static Maybe<TNod<T>> GetNodeUnderMouse<T>(TreeListView ctrl)
	{
		if (ctrl.MouseMoveHitTest == null) return May.None<TNod<T>>();
		var obj = ctrl.MouseMoveHitTest.RowObject;
		if (obj == null) return May.None<TNod<T>>();
		if (obj is not TNod<T> node) return May.None<TNod<T>>();
		return May.Some(node);
	}
	

    public static void SetNodGeneric<T>(TreeListView ctrl)
	{
		ctrl.CanExpandGetter = delegate (object o)
		{
			var nod = (TNod<T>)o;
			return nod.Children.Any();
		};
		ctrl.ChildrenGetter = delegate (object o)
		{
			var nod = (TNod<T>)o;
			return nod.Children;
		};
		ctrl.ParentGetter = delegate (object o)
		{
			var nod = (TNod<T>)o;
			return nod.Parent;
		};
	}


    public static IDisposable GetSelectedNode<T>(
		out IRoMayVar<TNod<T>> selectedNode,
		TreeListView ctrl
	)
	{
		var d = new Disp();
		
		selectedNode = VarMay.Make(
			Observable.Merge(
				ctrl.Events().ItemSelectionChanged.Select(e => e.IsSelected switch
				{
					true => May.Some((TNod<T>)ctrl.SelectedObject),
					false => May.None<TNod<T>>()
				}),
				ctrl.Events().Click.Where(_ => GetNodeUnderMouse<T>(ctrl).IsNone())
					.Select(_ => May.None<TNod<T>>())
			)
		).D(d);
		
		return d;
	}

	
    public static void NotifyTreeLoaded<T>(TreeListView ctrl, TNod<T> root)
	{
		ctrl.SetObjects(root.ToArr());
		ctrl.ExpandAll();
	}


    public static void NotifyTreeUnloaded(TreeListView ctrl)
	{
		ctrl.ClearObjects();
		ctrl.SelectedIndices.Clear();
	}


	/// <summary>
	/// ItemSelectionChanged will be fired
	/// so the caller doesn't need to do anything special
	/// </summary>
	public static void NotifyNodeAddedAndSelectIt<T>(TreeListView ctrl, TNod<T> parent, TNod<T> child)
	{
		ctrl.RefreshObject(parent); // needed for non leaf nodes (otherwise it doesn't appear)
		ctrl.Reveal(child, true);
	}


	/// <summary>
	/// ItemSelectionChanged will NOT be fired
	/// So the caller should also set the SelectedNode to None
	/// </summary>
	public static void NotifyNodeRemoved<T>(TreeListView ctrl, TNod<T> node)
	{
		ctrl.RemoveObject(node);
	}


	public static void NotifyNodeChanged<T>(TreeListView ctrl, TNod<T> node)
	{
		ctrl.RefreshObject(node);
	}


	private static T[] ToArr<T>(this T obj) => new[] { obj };
}