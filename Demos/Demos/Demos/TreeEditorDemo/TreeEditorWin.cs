using PowRxVar;
using PowWinForms;

namespace Demos.Demos.TreeEditorDemo;

sealed partial class TreeEditorWin : Form
{
	public TreeEditorWin()
	{
		InitializeComponent();

		this.InitRX(d => TreeEditorLogic.Init(this).D(d));
	}
}
