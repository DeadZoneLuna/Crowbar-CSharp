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
	public class SourceModelProgressEventArgs : System.EventArgs
	{
		public SourceModelProgressEventArgs(AppEnums.ProgressOptions progress, string message) : base()
		{

			this.theProgress = progress;
			this.theMessage = message;
		}

		public AppEnums.ProgressOptions Progress
		{
			get
			{
				return this.theProgress;
			}
		}

		public string Message
		{
			get
			{
				return this.theMessage;
			}
			set
			{
				this.theMessage = value;
			}
		}

		private AppEnums.ProgressOptions theProgress;
		private string theMessage;

	}
}