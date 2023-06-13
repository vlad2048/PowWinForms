namespace Demos.Demos.ListBoxSourceListViewerDemo;

partial class ListBoxSourceListViewerWin
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
		listBox = new ListBox();
		addBtn = new Button();
		delBtn = new Button();
		clearBtn = new Button();
		label1 = new Label();
		selectedLabel = new Label();
		SuspendLayout();
		// 
		// listBox
		// 
		listBox.FormattingEnabled = true;
		listBox.ItemHeight = 15;
		listBox.Location = new Point(12, 12);
		listBox.Name = "listBox";
		listBox.Size = new Size(187, 199);
		listBox.TabIndex = 0;
		// 
		// addBtn
		// 
		addBtn.Location = new Point(12, 217);
		addBtn.Name = "addBtn";
		addBtn.Size = new Size(75, 23);
		addBtn.TabIndex = 1;
		addBtn.Text = "Add";
		addBtn.UseVisualStyleBackColor = true;
		// 
		// delBtn
		// 
		delBtn.Location = new Point(124, 217);
		delBtn.Name = "delBtn";
		delBtn.Size = new Size(75, 23);
		delBtn.TabIndex = 2;
		delBtn.Text = "Del";
		delBtn.UseVisualStyleBackColor = true;
		// 
		// clearBtn
		// 
		clearBtn.Location = new Point(124, 246);
		clearBtn.Name = "clearBtn";
		clearBtn.Size = new Size(75, 23);
		clearBtn.TabIndex = 3;
		clearBtn.Text = "Clear";
		clearBtn.UseVisualStyleBackColor = true;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(205, 12);
		label1.Name = "label1";
		label1.Size = new Size(54, 15);
		label1.TabIndex = 4;
		label1.Text = "Selected:";
		// 
		// selectedLabel
		// 
		selectedLabel.AutoSize = true;
		selectedLabel.Location = new Point(265, 12);
		selectedLabel.Name = "selectedLabel";
		selectedLabel.Size = new Size(12, 15);
		selectedLabel.TabIndex = 5;
		selectedLabel.Text = "_";
		// 
		// ListBoxSourceListViewerWin
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(410, 277);
		Controls.Add(selectedLabel);
		Controls.Add(label1);
		Controls.Add(clearBtn);
		Controls.Add(delBtn);
		Controls.Add(addBtn);
		Controls.Add(listBox);
		Name = "ListBoxSourceListViewerWin";
		Text = "ListBox SourceList Viewer Demo";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private ListBox listBox;
	private Button addBtn;
	private Button delBtn;
	private Button clearBtn;
	private Label label1;
	private Label selectedLabel;
}