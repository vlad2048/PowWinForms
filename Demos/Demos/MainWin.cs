using Demos.Demos.ListBoxSourceListViewerDemo;
using Demos.Demos.TreeEditorDemo;
using PowRxVar;
using PowWinForms;

namespace Demos;

sealed partial class MainWin : Form
{
	public MainWin()
	{
		InitializeComponent();

		this.InitRX(d =>
		{
			HookBtn<TreeEditorWin>(treeEditorBtn).D(d);
			HookBtn<ListBoxSourceListViewerWin>(listBoxSourceListViewerBtn).D(d);
		});
	}

	private static IDisposable HookBtn<T>(Button btn) where T : Form, new() =>
		btn.Events().Click.Subscribe(_ => {
			var win = new T();
			win.Show();
		});
}
