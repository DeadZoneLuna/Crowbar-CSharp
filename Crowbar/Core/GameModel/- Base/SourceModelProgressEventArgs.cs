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

			theProgress = progress;
			theMessage = message;
		}

		public AppEnums.ProgressOptions Progress
		{
			get
			{
				return theProgress;
			}
		}

		public string Message
		{
			get
			{
				return theMessage;
			}
			set
			{
				theMessage = value;
			}
		}

		private AppEnums.ProgressOptions theProgress;
		private string theMessage;

	}
}