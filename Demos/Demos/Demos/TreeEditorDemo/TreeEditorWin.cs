using PowRxVar;
using PowWinForms;

namespace Demos.Demos.TreeEditorDemo;

partial class TreeEditorWin : Form
{
	public TreeEditorWin()
	{
	InitializeComponent();

	var d = this.MakeD();

	this.Events().HandleCreated.Subscribe(_ => {
	TreeEditorLogic.Init(this).D(d);
	});
	}
}
