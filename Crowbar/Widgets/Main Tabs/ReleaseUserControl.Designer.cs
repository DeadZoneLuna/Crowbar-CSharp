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
			this.Panel1 = new System.Windows.Forms.Panel();
			this.TextBoxEx4 = new Crowbar.TextBoxEx();
			this.Panel1.SuspendLayout();
			this.SuspendLayout();
			//
			//Panel1
			//
			this.Panel1.Controls.Add(this.TextBoxEx4);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel1.Location = new System.Drawing.Point(0, 0);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(776, 536);
			this.Panel1.TabIndex = 0;
			//
			//TextBoxEx4
			//
			this.TextBoxEx4.Location = new System.Drawing.Point(3, 3);
			this.TextBoxEx4.Name = "TextBoxEx4";
			this.TextBoxEx4.ReadOnly = true;
			this.TextBoxEx4.Size = new System.Drawing.Size(778, 22);
			this.TextBoxEx4.TabIndex = 2;
			this.TextBoxEx4.Text = "Not implemented yet.";
			//
			//ReleaseUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel1);
			this.Name = "ReleaseUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Panel Panel1;
		internal Crowbar.TextBoxEx TextBoxEx4;

	}

}