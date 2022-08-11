using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public partial class EditUserControl : BaseUserControl
	{
		//UserControl overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			Panel1 = new System.Windows.Forms.Panel();
			GotoQcButton = new System.Windows.Forms.Button();
			Label6 = new System.Windows.Forms.Label();
			QcPathFileNameTextBox = new System.Windows.Forms.TextBox();
			BrowseForQcPathFolderOrFileNameButton = new System.Windows.Forms.Button();
			UseInCompileButton = new System.Windows.Forms.Button();
			Panel1.SuspendLayout();
			SuspendLayout();
			//
			//Panel1
			//
			Panel1.Controls.Add(GotoQcButton);
			Panel1.Controls.Add(Label6);
			Panel1.Controls.Add(QcPathFileNameTextBox);
			Panel1.Controls.Add(BrowseForQcPathFolderOrFileNameButton);
			Panel1.Controls.Add(UseInCompileButton);
			Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel1.Location = new System.Drawing.Point(0, 0);
			Panel1.Name = "Panel1";
			Panel1.Size = new System.Drawing.Size(776, 536);
			Panel1.TabIndex = 16;
			//
			//GotoQcButton
			//
			GotoQcButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoQcButton.Location = new System.Drawing.Point(733, 3);
			GotoQcButton.Name = "GotoQcButton";
			GotoQcButton.Size = new System.Drawing.Size(40, 23);
			GotoQcButton.TabIndex = 29;
			GotoQcButton.Text = "Goto";
			GotoQcButton.UseVisualStyleBackColor = true;
			//
			//Label6
			//
			Label6.AutoSize = true;
			Label6.Location = new System.Drawing.Point(3, 8);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(87, 13);
			Label6.TabIndex = 26;
			Label6.Text = "QC file or folder:";
			//
			//QcPathFileNameTextBox
			//
			QcPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			QcPathFileNameTextBox.Location = new System.Drawing.Point(91, 5);
			QcPathFileNameTextBox.Name = "QcPathFileNameTextBox";
			QcPathFileNameTextBox.Size = new System.Drawing.Size(555, 21);
			QcPathFileNameTextBox.TabIndex = 27;
			//
			//BrowseForQcPathFolderOrFileNameButton
			//
			BrowseForQcPathFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForQcPathFolderOrFileNameButton.Location = new System.Drawing.Point(652, 3);
			BrowseForQcPathFolderOrFileNameButton.Name = "BrowseForQcPathFolderOrFileNameButton";
			BrowseForQcPathFolderOrFileNameButton.Size = new System.Drawing.Size(75, 23);
			BrowseForQcPathFolderOrFileNameButton.TabIndex = 28;
			BrowseForQcPathFolderOrFileNameButton.Text = "Browse...";
			BrowseForQcPathFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//UseInCompileButton
			//
			UseInCompileButton.Enabled = false;
			UseInCompileButton.Location = new System.Drawing.Point(3, 54);
			UseInCompileButton.Name = "UseInCompileButton";
			UseInCompileButton.Size = new System.Drawing.Size(90, 23);
			UseInCompileButton.TabIndex = 25;
			UseInCompileButton.Text = "Use in Compile";
			UseInCompileButton.UseVisualStyleBackColor = true;
			//
			//EditUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Panel1);
			Name = "EditUserControl";
			Size = new System.Drawing.Size(776, 536);
			Panel1.ResumeLayout(false);
			Panel1.PerformLayout();
			ResumeLayout(false);

		}
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.Button UseInCompileButton;
		internal System.Windows.Forms.Button GotoQcButton;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.TextBox QcPathFileNameTextBox;
		internal System.Windows.Forms.Button BrowseForQcPathFolderOrFileNameButton;

	}

}