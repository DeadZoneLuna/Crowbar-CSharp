//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using Crowbar;

namespace Crowbar
{
	public class SteamPipeEventArgs : EventArgs
	{
		public SteamPipeEventArgs(string message)
		{
			theMessage = message;
		}

		public string Message
		{
			get
			{
				return theMessage;
			}
		}

		private string theMessage;

		public static implicit operator string(SteamPipeEventArgs v)
		{
			throw new NotImplementedException();
		}
	}

}