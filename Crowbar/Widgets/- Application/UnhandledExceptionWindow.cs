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
	public partial class UnhandledExceptionWindow
	{

		public UnhandledExceptionWindow()
		{
			InitializeComponent();
		}

		private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(Properties.Resources.BugReportLink);
		}

		private void CloseButton_Click(System.Object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void CopyErrorReportButton_Click(System.Object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.None;
			DataObject data = new DataObject(this.ErrorReportTextBox.Text);
			//NOTE: Set the second parameter to True so that the Clipboard will keep the text on it when the application exits.
			Clipboard.SetDataObject(data, true);
		}

	}

}