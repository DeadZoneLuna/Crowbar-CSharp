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
	public partial class ReleaseUserControl : BaseUserControl
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
			TextBoxEx4 = new Crowbar.TextBoxEx();
			Panel1.SuspendLayout();
			SuspendLayout();
			//
			//Panel1
			//
			Panel1.Controls.Add(TextBoxEx4);
			Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel1.Location = new System.Drawing.Point(0, 0);
			Panel1.Name = "Panel1";
			Panel1.Size = new System.Drawing.Size(776, 536);
			Panel1.TabIndex = 0;
			//
			//TextBoxEx4
			//
			TextBoxEx4.Location = new System.Drawing.Point(3, 3);
			TextBoxEx4.Name = "TextBoxEx4";
			TextBoxEx4.ReadOnly = true;
			TextBoxEx4.Size = new System.Drawing.Size(778, 22);
			TextBoxEx4.TabIndex = 2;
			TextBoxEx4.Text = "Not implemented yet.";
			//
			//ReleaseUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Panel1);
			Name = "ReleaseUserControl";
			Size = new System.Drawing.Size(776, 536);
			Panel1.ResumeLayout(false);
			Panel1.PerformLayout();
			ResumeLayout(false);

		}
		internal System.Windows.Forms.Panel Panel1;
		internal Crowbar.TextBoxEx TextBoxEx4;

	}

}