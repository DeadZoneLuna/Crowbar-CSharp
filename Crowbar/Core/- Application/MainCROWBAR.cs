//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Crowbar
{
	internal static class MainCROWBAR
	{

		[STAThread]
		// Entry point of application.
		public static int Main()
		{
			AppExceptionHandler anExceptionHandler = new AppExceptionHandler();
			Application.ThreadException += anExceptionHandler.Application_ThreadException;
			// Set the unhandled exception mode to call Application.ThreadException event for all Windows Forms exceptions.
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

			//NOTE: Use the Windows Vista and later visual styles (such as rounded buttons).
			Application.EnableVisualStyles();
			//NOTE: Needed for keeping Label and Button text rendering correctly.
			Application.SetCompatibleTextRenderingDefault(false);

			AppDomain.CurrentDomain.AssemblyResolve += ResolveAssemblies;

			TheApp = new App();
			TheApp.Init();
			if (MainForm.DefaultInstance.WindowState == FormWindowState.Minimized)
				MainForm.DefaultInstance.WindowState = FormWindowState.Normal;
			MainForm.DefaultInstance.Activate();
			MainForm.DefaultInstance.Startup(ConversionHelper.CommandLineArgs);
			Application.Run(MainForm.DefaultInstance);
			TheApp.Dispose();

			return 0;
		}

		private static System.Reflection.Assembly ResolveAssemblies(object sender, System.ResolveEventArgs e)
		{
			System.Reflection.AssemblyName desiredAssembly = new System.Reflection.AssemblyName(e.Name);
			if (desiredAssembly.Name == "Steamworks.NET")
			{
				return System.Reflection.Assembly.Load(Properties.Resources.Steamworks_NET);
			}
			else
			{
				return null;
			}
		}

		//Public TheJob As WindowsJob
		public static App TheApp;
	}

}