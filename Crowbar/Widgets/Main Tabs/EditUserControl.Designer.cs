//INSTANT C# NOTE: Formerly VB project-level imports:
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
			this.Panel1 = new System.Windows.Forms.Panel();
			this.GotoQcButton = new System.Windows.Forms.Button();
			this.Label6 = new System.Windows.Forms.Label();
			this.QcPathFileNameTextBox = new System.Windows.Forms.TextBox();
			this.BrowseForQcPathFolderOrFileNameButton = new System.Windows.Forms.Button();
			this.UseInCompileButton = new System.Windows.Forms.Button();
			this.Panel1.SuspendLayout();
			this.SuspendLayout();
			//
			//Panel1
			//
			this.Panel1.Controls.Add(this.GotoQcButton);
			this.Panel1.Controls.Add(this.Label6);
			this.Panel1.Controls.Add(this.QcPathFileNameTextBox);
			this.Panel1.Controls.Add(this.BrowseForQcPathFolderOrFileNameButton);
			this.Panel1.Controls.Add(this.UseInCompileButton);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel1.Location = new System.Drawing.Point(0, 0);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(776, 536);
			this.Panel1.TabIndex = 16;
			//
			//GotoQcButton
			//
			this.GotoQcButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoQcButton.Location = new System.Drawing.Point(733, 3);
			this.GotoQcButton.Name = "GotoQcButton";
			this.GotoQcButton.Size = new System.Drawing.Size(40, 23);
			this.GotoQcButton.TabIndex = 29;
			this.GotoQcButton.Text = "Goto";
			this.GotoQcButton.UseVisualStyleBackColor = true;
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(3, 8);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(87, 13);
			this.Label6.TabIndex = 26;
			this.Label6.Text = "QC file or folder:";
			//
			//QcPathFileNameTextBox
			//
			this.QcPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.QcPathFileNameTextBox.Location = new System.Drawing.Point(91, 5);
			this.QcPathFileNameTextBox.Name = "QcPathFileNameTextBox";
			this.QcPathFileNameTextBox.Size = new System.Drawing.Size(555, 21);
			this.QcPathFileNameTextBox.TabIndex = 27;
			//
			//BrowseForQcPathFolderOrFileNameButton
			//
			this.BrowseForQcPathFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForQcPathFolderOrFileNameButton.Location = new System.Drawing.Point(652, 3);
			this.BrowseForQcPathFolderOrFileNameButton.Name = "BrowseForQcPathFolderOrFileNameButton";
			this.BrowseForQcPathFolderOrFileNameButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForQcPathFolderOrFileNameButton.TabIndex = 28;
			this.BrowseForQcPathFolderOrFileNameButton.Text = "Browse...";
			this.BrowseForQcPathFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//UseInCompileButton
			//
			this.UseInCompileButton.Enabled = false;
			this.UseInCompileButton.Location = new System.Drawing.Point(3, 54);
			this.UseInCompileButton.Name = "UseInCompileButton";
			this.UseInCompileButton.Size = new System.Drawing.Size(90, 23);
			this.UseInCompileButton.TabIndex = 25;
			this.UseInCompileButton.Text = "Use in Compile";
			this.UseInCompileButton.UseVisualStyleBackColor = true;
			//
			//EditUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel1);
			this.Name = "EditUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.Button UseInCompileButton;
		internal System.Windows.Forms.Button GotoQcButton;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.TextBox QcPathFileNameTextBox;
		internal System.Windows.Forms.Button BrowseForQcPathFolderOrFileNameButton;

	}

}