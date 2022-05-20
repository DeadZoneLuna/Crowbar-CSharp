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
			//' Create a job with JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE flag, so that all processes 
			//'	(e.g. HLMV called by Crowbar) associated with the job 
			//'	terminate when the last handle to the job is closed.
			//' From MSDN: By default, processes created using CreateProcess by a process associated with a job 
			//'	are associated with the job.
			//TheJob = New WindowsJob()
			//TheJob.AddProcess(Process.GetCurrentProcess().Handle())

			AppExceptionHandler anExceptionHandler = new AppExceptionHandler();
			Application.ThreadException += anExceptionHandler.Application_ThreadException;
			// Set the unhandled exception mode to call Application.ThreadException event for all Windows Forms exceptions.
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

			//Dim appUniqueIdentifier As String
			//Dim appMutex As System.Threading.Mutex
			//appUniqueIdentifier = Application.ExecutablePath.Replace("\", "_")
			//appMutex = New System.Threading.Mutex(False, appUniqueIdentifier)
			//If appMutex.WaitOne(0, False) = False Then
			//	appMutex.Close()
			//	appMutex = Nothing
			//	'MessageBox.Show("Another instance is already running!")
			//	Win32Api.PostMessage(CType(Win32Api.WindowsMessages.HWND_BROADCAST, IntPtr), appUniqueWindowsMessageIdentifier, IntPtr.Zero, IntPtr.Zero)
			//Else
			//NOTE: Use the Windows Vista and later visual styles (such as rounded buttons).
			Application.EnableVisualStyles();
			//NOTE: Needed for keeping Label and Button text rendering correctly.
			Application.SetCompatibleTextRenderingDefault(false);

			AppDomain.CurrentDomain.AssemblyResolve += ResolveAssemblies;

			TheApp = new App();
			//Try
			TheApp.Init();
			if (TheApp.Settings.AppIsSingleInstance)
			{
				SingleInstanceApplication.Run(new MainForm(), StartupNextInstanceEventHandler);
			}
			else
			{
				System.Windows.Forms.Application.Run(MainForm.DefaultInstance);
			}
			//Catch e As Exception
			//	MsgBox(e.Message)
			//Finally
			//End Try
			TheApp.Dispose();
			//End If

			return 0;
		}

		private static void StartupNextInstanceEventHandler(object sender, SingleInstanceEventArgs e)
		{
			if (e.MainForm.WindowState == FormWindowState.Minimized)
			{
				e.MainForm.WindowState = FormWindowState.Normal;
			}
			e.MainForm.Activate();
			((MainForm)e.MainForm).Startup(e.CommandLine);
		}

		private static System.Reflection.Assembly ResolveAssemblies(object sender, System.ResolveEventArgs e)
		{
			System.Reflection.AssemblyName desiredAssembly = new System.Reflection.AssemblyName(e.Name);
			//If desiredAssembly.Name = "SevenZipSharp" Then
			//	Return Reflection.Assembly.Load(My.Resources.SevenZipSharp)
			//ElseIf desiredAssembly.Name = "Steamworks.NET" Then
			//	Return Reflection.Assembly.Load(My.Resources.Steamworks_NET)
			//Else
			//	Return Nothing
			//End If
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