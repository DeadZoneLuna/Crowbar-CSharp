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
	public partial class UnhandledExceptionWindow : BaseForm
	{
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnhandledExceptionWindow));
			this.ExitButton = new System.Windows.Forms.Button();
			this.CopyErrorReportButton = new System.Windows.Forms.Button();
			this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
			this.Label1 = new System.Windows.Forms.Label();
			this.ErrorReportTextBox = new TextBoxEx();
			this.SuspendLayout();
			//
			//ExitButton
			//
			this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ExitButton.Location = new System.Drawing.Point(614, 379);
			this.ExitButton.Name = "ExitButton";
			this.ExitButton.Size = new System.Drawing.Size(90, 23);
			this.ExitButton.TabIndex = 4;
			this.ExitButton.Text = "Exit Crowbar";
			//
			//CopyErrorReportButton
			//
			this.CopyErrorReportButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CopyErrorReportButton.Location = new System.Drawing.Point(507, 379);
			this.CopyErrorReportButton.Name = "CopyErrorReportButton";
			this.CopyErrorReportButton.Size = new System.Drawing.Size(101, 23);
			this.CopyErrorReportButton.TabIndex = 3;
			this.CopyErrorReportButton.Text = "Copy Error Report";
			//
			//LinkLabel1
			//
			this.LinkLabel1.LinkArea = new System.Windows.Forms.LinkArea(204, 26);
			this.LinkLabel1.Location = new System.Drawing.Point(12, 12);
			this.LinkLabel1.Name = "LinkLabel1";
			this.LinkLabel1.Size = new System.Drawing.Size(692, 138);
			this.LinkLabel1.TabIndex = 0;
			this.LinkLabel1.TabStop = true;
			this.LinkLabel1.Text = resources.GetString("LinkLabel1.Text");
			this.LinkLabel1.UseCompatibleTextRendering = true;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(12, 153);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(71, 13);
			this.Label1.TabIndex = 1;
			this.Label1.Text = "Error Report:";
			//
			//ErrorReportTextBox
			//
			this.ErrorReportTextBox.Location = new System.Drawing.Point(15, 169);
			this.ErrorReportTextBox.Multiline = true;
			this.ErrorReportTextBox.Name = "ErrorReportTextBox";
			this.ErrorReportTextBox.ReadOnly = true;
			this.ErrorReportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ErrorReportTextBox.Size = new System.Drawing.Size(689, 204);
			this.ErrorReportTextBox.TabIndex = 2;
			//
			//UnhandledExceptionWindow
			//
			this.AcceptButton = this.ExitButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CopyErrorReportButton;
			this.ClientSize = new System.Drawing.Size(716, 418);
			this.ControlBox = false;
			this.Controls.Add(this.ErrorReportTextBox);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.LinkLabel1);
			this.Controls.Add(this.CopyErrorReportButton);
			this.Controls.Add(this.ExitButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnhandledExceptionWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Crowbar Internal Error";
			this.ResumeLayout(false);
			this.PerformLayout();

//INSTANT C# NOTE: Converted design-time event handler wireups:
			LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel1_LinkClicked);
			ExitButton.Click += new System.EventHandler(CloseButton_Click);
			CopyErrorReportButton.Click += new System.EventHandler(CopyErrorReportButton_Click);
		}
		internal System.Windows.Forms.Button ExitButton;
		internal System.Windows.Forms.Button CopyErrorReportButton;
		internal System.Windows.Forms.LinkLabel LinkLabel1;
		internal System.Windows.Forms.Label Label1;
		internal TextBoxEx ErrorReportTextBox;

	}

}