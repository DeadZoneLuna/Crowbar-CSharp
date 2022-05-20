//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Crowbar
{
	public class SingleInstanceEventArgs : EventArgs
	{
		public SingleInstanceEventArgs(ReadOnlyCollection<string> commandLine, Form mainForm)
		{
			_commandLine = commandLine;
			_mainForm = mainForm;
		}

		public ReadOnlyCollection<string> CommandLine
		{
			get
			{
				return _commandLine;
			}
		}

		public Form MainForm
		{
			get
			{
				return _mainForm;
			}
		}

		private ReadOnlyCollection<string> _commandLine;
		private Form _mainForm;

	}

}