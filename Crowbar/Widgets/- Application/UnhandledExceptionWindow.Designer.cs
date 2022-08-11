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
			ExitButton = new System.Windows.Forms.Button();
			CopyErrorReportButton = new System.Windows.Forms.Button();
			LinkLabel1 = new System.Windows.Forms.LinkLabel();
			Label1 = new System.Windows.Forms.Label();
			ErrorReportTextBox = new TextBoxEx();
			SuspendLayout();
			//
			//ExitButton
			//
			ExitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			ExitButton.Location = new System.Drawing.Point(614, 379);
			ExitButton.Name = "ExitButton";
			ExitButton.Size = new System.Drawing.Size(90, 23);
			ExitButton.TabIndex = 4;
			ExitButton.Text = "Exit Crowbar";
			//
			//CopyErrorReportButton
			//
			CopyErrorReportButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			CopyErrorReportButton.Location = new System.Drawing.Point(507, 379);
			CopyErrorReportButton.Name = "CopyErrorReportButton";
			CopyErrorReportButton.Size = new System.Drawing.Size(101, 23);
			CopyErrorReportButton.TabIndex = 3;
			CopyErrorReportButton.Text = "Copy Error Report";
			//
			//LinkLabel1
			//
			LinkLabel1.LinkArea = new System.Windows.Forms.LinkArea(204, 26);
			LinkLabel1.Location = new System.Drawing.Point(12, 12);
			LinkLabel1.Name = "LinkLabel1";
			LinkLabel1.Size = new System.Drawing.Size(692, 138);
			LinkLabel1.TabIndex = 0;
			LinkLabel1.TabStop = true;
			LinkLabel1.Text = resources.GetString("LinkLabel1.Text");
			LinkLabel1.UseCompatibleTextRendering = true;
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(12, 153);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(71, 13);
			Label1.TabIndex = 1;
			Label1.Text = "Error Report:";
			//
			//ErrorReportTextBox
			//
			ErrorReportTextBox.Location = new System.Drawing.Point(15, 169);
			ErrorReportTextBox.Multiline = true;
			ErrorReportTextBox.Name = "ErrorReportTextBox";
			ErrorReportTextBox.ReadOnly = true;
			ErrorReportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			ErrorReportTextBox.Size = new System.Drawing.Size(689, 204);
			ErrorReportTextBox.TabIndex = 2;
			//
			//UnhandledExceptionWindow
			//
			AcceptButton = ExitButton;
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = CopyErrorReportButton;
			ClientSize = new System.Drawing.Size(716, 418);
			ControlBox = false;
			Controls.Add(ErrorReportTextBox);
			Controls.Add(Label1);
			Controls.Add(LinkLabel1);
			Controls.Add(CopyErrorReportButton);
			Controls.Add(ExitButton);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "UnhandledExceptionWindow";
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Crowbar Internal Error";
			ResumeLayout(false);
			PerformLayout();

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