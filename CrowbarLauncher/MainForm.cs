//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;

using System.Collections.ObjectModel;
using System.IO;

namespace CrowbarLauncher
{
	public partial class MainForm
	{

		public MainForm()
		{
			InitializeComponent();
		}

		private void Init()
		{
			//TODO: Move new Crowbar.exe to where current Crowbar.exe is and then run the new Crowbar.exe.
			ReadOnlyCollection<string> commandLineValues = new ReadOnlyCollection<string>(System.Environment.GetCommandLineArgs());
			string startupPath = Application.StartupPath;
			string sourcePathFileName = Path.Combine(startupPath, "Crowbar.exe");
			string targetPathFileName = commandLineValues[2];

			if (commandLineValues.Count > 2 && !string.IsNullOrEmpty(commandLineValues[1]))
			{
				Int32 crowbarExeProcessId = int.Parse(commandLineValues[1]);
				// GetProcessById will raise exception if crowbarExeProcessId is invalid, which should mean that Crowbar has already closed.
				try
				{
					Process crowbarExeProcess = Process.GetProcessById(crowbarExeProcessId);
					crowbarExeProcess.WaitForExit();
				}
				catch (Exception ex)
				{
				}

				try
				{
					if (File.Exists(targetPathFileName))
					{
						File.Delete(targetPathFileName);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}

				try
				{
					File.Move(sourcePathFileName, targetPathFileName);
					//Me.UpdateProgress(2, "CROWBAR: Moved package file """ + sourcePathFileName + """ to """ + targetPath + """")
				}
				catch (Exception ex)
				{
					int debug = 4242;
					//Me.UpdateProgress()
					//Me.UpdateProgress(2, "WARNING: Crowbar tried to move the file, """ + sourcePathFileName + """, to the output folder, but Windows complained with this message: " + ex.Message.Trim())
					//Me.UpdateProgress(2, "SOLUTION: Pack again (and hope Windows does not complain again) or move the file yourself.")
					//Me.UpdateProgress()
				}
			}

			if (File.Exists(targetPathFileName))
			{
				// Run Crowbar.exe and exit CrowbarLauncher.
				Process crowbarExeProcess = new Process();
				try
				{
					crowbarExeProcess.StartInfo.UseShellExecute = false;
					//NOTE: From Microsoft website: 
					//      On Windows Vista and earlier versions of the Windows operating system, 
					//      the length of the arguments added to the length of the full path to the process must be less than 2080. 
					//      On Windows 7 and later versions, the length must be less than 32699. 
					crowbarExeProcess.StartInfo.FileName = targetPathFileName;
					if (commandLineValues.Count > 3 && !string.IsNullOrEmpty(commandLineValues[3]))
					{
						//NOTE: The commandLineValues(3) does not keep the double-quotes.
						crowbarExeProcess.StartInfo.Arguments = "\"" + commandLineValues[3] + "\"";
					}
#if DEBUG
					crowbarExeProcess.StartInfo.CreateNoWindow = false;
#else
					crowbarExeProcess.StartInfo.CreateNoWindow = true;
#endif
					crowbarExeProcess.Start();
					Application.Exit();
				}
				catch (Exception ex)
				{
					int debug = 4242;
					//Throw New System.Exception("Crowbar tried to compress the file """ + gmaPathFileName + """ to """ + processedPathFileName + """ but Windows gave this message: " + ex.Message)
				}
				finally
				{
				}
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Init();
		}


		[STAThread]
		static void Main()
		{
			Application.Run(new MainForm());
		}

	}

}