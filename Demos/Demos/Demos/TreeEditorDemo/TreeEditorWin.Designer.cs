namespace Demos.Demos.TreeEditorDemo;

partial class TreeEditorWin
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		components = new System.ComponentModel.Container();
		treeCtrl = new BrightIdeasSoftware.TreeListView();
		treeContextMenu = new ContextMenuStrip(components);
		addNodeMenuItem = new ToolStripMenuItem();
		removeNodeMenuItem = new ToolStripMenuItem();
		label1 = new Label();
		nameText = new TextBox();
		loadBtn = new Button();
		clearBtn = new Button();
		treeText = new TextBox();
		label2 = new Label();
		((System.ComponentModel.ISupportInitialize)treeCtrl).BeginInit();
		treeContextMenu.SuspendLayout();
		SuspendLayout();
		// 
		// treeCtrl
		// 
		treeCtrl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
		treeCtrl.CellEditUseWholeCell = false;
		treeCtrl.ContextMenuStrip = treeContextMenu;
		treeCtrl.Location = new Point(12, 12);
		treeCtrl.Name = "treeCtrl";
		treeCtrl.ShowGroups = false;
		treeCtrl.Size = new Size(277, 426);
		treeCtrl.TabIndex = 0;
		treeCtrl.View = View.Details;
		treeCtrl.VirtualMode = true;
		// 
		// treeContextMenu
		// 
		treeContextMenu.Items.AddRange(new ToolStripItem[] { addNodeMenuItem, removeNodeMenuItem });
		treeContextMenu.Name = "treeContextMenu";
		treeContextMenu.Size = new Size(148, 48);
		// 
		// addNodeMenuItem
		// 
		addNodeMenuItem.Name = "addNodeMenuItem";
		addNodeMenuItem.Size = new Size(147, 22);
		addNodeMenuItem.Text = "Add node";
		// 
		// removeNodeMenuItem
		// 
		removeNodeMenuItem.Name = "removeNodeMenuItem";
		removeNodeMenuItem.Size = new Size(147, 22);
		removeNodeMenuItem.Text = "Remove node";
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(295, 84);
		label1.Name = "label1";
		label1.Size = new Size(42, 15);
		label1.TabIndex = 1;
		label1.Text = "Name:";
		// 
		// nameText
		// 
		nameText.Location = new Point(343, 81);
		nameText.Name = "nameText";
		nameText.Size = new Size(100, 23);
		nameText.TabIndex = 2;
		// 
		// loadBtn
		// 
		loadBtn.Location = new Point(295, 12);
		loadBtn.Name = "loadBtn";
		loadBtn.Size = new Size(75, 23);
		loadBtn.TabIndex = 3;
		loadBtn.Text = "Load";
		loadBtn.UseVisualStyleBackColor = true;
		// 
		// clearBtn
		// 
		clearBtn.Location = new Point(376, 12);
		clearBtn.Name = "clearBtn";
		clearBtn.Size = new Size(75, 23);
		clearBtn.TabIndex = 4;
		clearBtn.Text = "Clear";
		clearBtn.UseVisualStyleBackColor = true;
		// 
		// treeText
		// 
		treeText.Location = new Point(343, 123);
		treeText.Multiline = true;
		treeText.Name = "treeText";
		treeText.Size = new Size(206, 228);
		treeText.TabIndex = 5;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(295, 126);
		label2.Name = "label2";
		label2.Size = new Size(31, 15);
		label2.TabIndex = 6;
		label2.Text = "Tree:";
		// 
		// TreeEditorWin
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(label2);
		Controls.Add(treeText);
		Controls.Add(clearBtn);
		Controls.Add(loadBtn);
		Controls.Add(nameText);
		Controls.Add(label1);
		Controls.Add(treeCtrl);
		Name = "TreeEditorWin";
		Text = "Tree Editor Demo";
		((System.ComponentModel.ISupportInitialize)treeCtrl).EndInit();
		treeContextMenu.ResumeLayout(false);
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion
	private Label label1;
	public BrightIdeasSoftware.TreeListView treeCtrl;
	public TextBox nameText;
	public Button loadBtn;
	public Button clearBtn;
	public ContextMenuStrip treeContextMenu;
	public ToolStripMenuItem addNodeMenuItem;
	public ToolStripMenuItem removeNodeMenuItem;
	private Label label2;
	public TextBox treeText;
}