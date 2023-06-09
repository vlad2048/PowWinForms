﻿namespace Demos;

partial class MainWin
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
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
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		treeEditorBtn = new Button();
		listBoxSourceListViewerBtn = new Button();
		SuspendLayout();
		// 
		// treeEditorBtn
		// 
		treeEditorBtn.Location = new Point(12, 12);
		treeEditorBtn.Name = "treeEditorBtn";
		treeEditorBtn.Size = new Size(202, 23);
		treeEditorBtn.TabIndex = 0;
		treeEditorBtn.Text = "Tree Editor Demo";
		treeEditorBtn.UseVisualStyleBackColor = true;
		// 
		// listBoxSourceListViewerBtn
		// 
		listBoxSourceListViewerBtn.Location = new Point(12, 41);
		listBoxSourceListViewerBtn.Name = "listBoxSourceListViewerBtn";
		listBoxSourceListViewerBtn.Size = new Size(202, 23);
		listBoxSourceListViewerBtn.TabIndex = 1;
		listBoxSourceListViewerBtn.Text = "ListBox SourceList Viewer Demo";
		listBoxSourceListViewerBtn.UseVisualStyleBackColor = true;
		// 
		// MainWin
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(listBoxSourceListViewerBtn);
		Controls.Add(treeEditorBtn);
		Name = "MainWin";
		Text = "PowWinForms Demos";
		ResumeLayout(false);
	}

	#endregion

	private Button treeEditorBtn;
	private Button listBoxSourceListViewerBtn;
}
