﻿//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;

namespace Crowbar
{
	public class MappingTool : BackgroundWorker
	{
#region Create and Destroy

		public MappingTool() : base()
		{

			isDisposed = false;

			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
			DoWork += MappingTool_DoWork;
		}

#region IDisposable Support

		//Public Sub Dispose() Implements IDisposable.Dispose
		//	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
		//	Dispose(True)
		//	GC.SuppressFinalize(Me)
		//End Sub

		protected void Dispose(bool disposing)
		{
			if (!isDisposed)
			{
				if (disposing)
				{
					Halt(false);
				}
				//NOTE: free shared unmanaged resources
			}
			isDisposed = true;
			base.Dispose(disposing);
		}

		~MappingTool()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(false);
//INSTANT C# NOTE: The base class Finalize method is automatically called from the destructor:
			//base.Finalize();
		}

#endregion

#endregion

#region Init and Free

		//Private Sub Init()
		//End Sub

		//Private Sub Free()
		//End Sub

#endregion

#region Methods

		public void Run(int gameSetupSelectedIndex)
		{
			MappingToolInfo info = new MappingToolInfo();
			info.gameSetupSelectedIndex = gameSetupSelectedIndex;
			RunWorkerAsync(info);
		}

		public void Halt()
		{
			Halt(false);
		}

#endregion

#region Event Handlers

#endregion

#region Private Methods that can be called in either the main thread or the background thread

		private void Halt(bool calledFromBackgroundThread)
		{
			if (theMappingToolProcess != null && !theMappingToolProcess.HasExited)
			{
				try
				{
					if (!theMappingToolProcess.CloseMainWindow())
					{
						theMappingToolProcess.Kill();
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
				finally
				{
					theMappingToolProcess.Close();
					theMappingToolProcess = null;
					//NOTE: This raises an exception when the background thread has already completed its work.
					//If calledFromBackgroundThread Then
					//	Me.UpdateProgressStop("Model viewer closed.")
					//End If
				}
			}
		}

#endregion

#region Private Methods that are called in the background thread

		private void MappingTool_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			ReportProgress(0, "");

			MappingToolInfo info = (MappingToolInfo)e.Argument;

			theGameSetupSelectedIndex = info.gameSetupSelectedIndex;
			if (MappingToolInputsAreOkay())
			{
				UpdateProgress(1, "Mapping tool opened.");
				RunMappingTool();
			}
		}

		//TODO: Check inputs as done in Compiler.CompilerInputsAreValid().
		private bool MappingToolInputsAreOkay()
		{
			bool inputsAreValid = true;


			GameSetup gameSetup = null;
			string mappingToolPathFileName = null;
			gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[theGameSetupSelectedIndex];
			mappingToolPathFileName = gameSetup.MappingToolPathFileName;

			if (!File.Exists(mappingToolPathFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage("The mapping tool, \"" + mappingToolPathFileName + "\", does not exist.");
				UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
			}

			return inputsAreValid;
		}

		private void RunMappingTool()
		{
			string mappingToolPathFileName = null;
			//Dim steamAppPathFileName As String
			//Dim gameAppId As String
			string gamePath = null;
			//Dim gameFileName As String
			//Dim mappingToolOptions As String
			//Dim currentFolder As String

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[theGameSetupSelectedIndex];
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName);
			//gameFileName = Path.GetFileName(gameSetup.GamePathFileName)
			mappingToolPathFileName = gameSetup.MappingToolPathFileName;
			//mappingToolOptions = gameSetup.MappingToolOptions

			//currentFolder = Directory.GetCurrentDirectory()
			//Directory.SetCurrentDirectory(gameModelsPath)

			string arguments = "";
			//NOTE: Needed for CSGO's hammer and seems to work with L4D2, SDK 2013 MP and SP, and HL2.
			arguments += " -nop4";
			arguments += " -game \"";
			arguments += gamePath;
			arguments += "\"";
			//arguments += " "
			//arguments += mappingToolOptions

			theMappingToolProcess = new Process();
			ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(mappingToolPathFileName, arguments);
			myProcessStartInfo.CreateNoWindow = true;
			myProcessStartInfo.RedirectStandardError = true;
			myProcessStartInfo.RedirectStandardOutput = true;
			myProcessStartInfo.UseShellExecute = false;
			// Instead of using asynchronous running, use synchronous and wait for process to exit, so this background thread won't complete until model viewer is closed.
			//      This allows background thread to announce to main thread when model viewer process exits.
			theMappingToolProcess.EnableRaisingEvents = true;
			theMappingToolProcess.StartInfo = myProcessStartInfo;

			theMappingToolProcess.Start();
			theMappingToolProcess.WaitForExit();
			theMappingToolProcess.Close();
			theMappingToolProcess = null;

			//Directory.SetCurrentDirectory(currentFolder)
		}

		private void UpdateProgressStart(string line)
		{
			UpdateProgressInternal(0, line);
		}

		private void UpdateProgressStop(string line)
		{
			UpdateProgressInternal(100, "\r" + line);
		}

		private void UpdateProgress()
		{
			UpdateProgressInternal(1, "");
		}

		private void WriteErrorMessage(string line)
		{
			UpdateProgressInternal(1, "ERROR: " + line);
		}

		private void UpdateProgress(int indentLevel, string line)
		{
			string indentedLine = "";

			for (int i = 1; i <= indentLevel; i++)
			{
				indentedLine += "  ";
			}
			indentedLine += line;
			UpdateProgressInternal(1, indentedLine);
		}

		private void UpdateProgressInternal(int progressValue, string line)
		{
			//'If progressValue = 0 Then
			//'	Do not write to file stream.
			//If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
			//    Me.theLogFileStream.WriteLine(line)
			//    Me.theLogFileStream.Flush()
			//End If

			ReportProgress(progressValue, line);
		}

#endregion

#region Data

		private bool isDisposed;

		private int theGameSetupSelectedIndex;

		private Process theMappingToolProcess;

#endregion

	}

}