//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace Crowbar
{
	public class SingleInstanceApplication : WindowsFormsApplicationBase
	{
		public static void Run(Form form, EventHandler<SingleInstanceEventArgs> handler)
		{
			SingleInstanceEvent += handler;
			applicationBase = new SingleInstanceApplication();
			applicationBase.MainForm = form;
			applicationBase.StartupNextInstance += StartupNextInstanceEventHandler;
			applicationBase.Run(Environment.GetCommandLineArgs());
		}

		private static void StartupNextInstanceEventHandler(object sender, StartupNextInstanceEventArgs e)
		{
			if (SingleInstanceEvent != null)
				SingleInstanceEvent(applicationBase, new SingleInstanceEventArgs(e.CommandLine, applicationBase.MainForm));
		}

		private static event EventHandler<SingleInstanceEventArgs> SingleInstanceEvent;
		private static SingleInstanceApplication applicationBase;

		private SingleInstanceApplication()
		{
			base.IsSingleInstance = true;
		}

	}

}