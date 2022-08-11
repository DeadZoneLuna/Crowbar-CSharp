using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace Crowbar
{
	public class GameApp : BackgroundWorker
	{
		#region Data
		private bool isDisposed;
		private int theGameSetupSelectedIndex;
		private Process theGameAppProcess;
		#endregion

		#region Create and Destroy
		public GameApp() : base()
		{
			isDisposed = false;
			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
			DoWork += GameApp_DoWork;
		}

		#region IDisposable Support
		protected void Dispose(bool disposing)
		{
			if (!isDisposed && disposing)
				Halt(false);

			//NOTE: free shared unmanaged resources
			isDisposed = true;
			base.Dispose(disposing);
		}

		~GameApp()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(false);
			//base.Finalize();
		}
		#endregion
		#endregion

		#region Init and Free
		#endregion

		#region Methods
		public void Run(int gameSetupSelectedIndex)
		{
			GameAppInfo info = new GameAppInfo();
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
			if (theGameAppProcess != null && !theGameAppProcess.HasExited)
			{
				try
				{
					if (!theGameAppProcess.CloseMainWindow())
						theGameAppProcess.Kill();
				}
				catch (Exception ex)
				{
					#if DEBUG
					Console.WriteLine("GameApp (Halt): " + ex.Message);
					#endif
				}
				finally
				{
					theGameAppProcess.Close();
					theGameAppProcess = null;
					//NOTE: This raises an exception when the background thread has already completed its work.
					//If calledFromBackgroundThread Then
					//	Me.UpdateProgressStop("Model viewer closed.")
					//End If
				}
			}
		}
		#endregion

		#region Private Methods that are called in the background thread
		private void GameApp_DoWork(object sender, DoWorkEventArgs e)
		{
			ReportProgress(0, string.Empty);

			GameAppInfo info = (GameAppInfo)e.Argument;

			theGameSetupSelectedIndex = info.gameSetupSelectedIndex;
			if (GameAppInputsAreOkay())
			{
				UpdateProgress(1, "Game run.");
				RunGameApp();
			}
		}

		//TODO: Check inputs as done in Compiler.CompilerInputsAreValid().
		private bool GameAppInputsAreOkay()
		{
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[theGameSetupSelectedIndex];
			string gameAppPathFileName = gameSetup.GameAppPathFileName;

			if (!File.Exists(gameAppPathFileName))
			{
				WriteErrorMessage("The game's executable, \"" + gameAppPathFileName + "\", does not exist.");
				UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
				return false;
			}

			return true;
		}

		private void RunGameApp()
		{
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[theGameSetupSelectedIndex];
			string gamePath = FileManager.GetPath(gameSetup.GamePathFileName);
			//string gameFileName = Path.GetFileName(gameSetup.GamePathFileName);
			string gameAppPathFileName = gameSetup.GameAppPathFileName;
			string gameAppOptions = gameSetup.GameAppOptions;

			string arguments = "";
			arguments += " -game \"";
			arguments += gamePath;
			arguments += "\" ";
			arguments += gameAppOptions;

			theGameAppProcess = new Process();
			ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(gameAppPathFileName, arguments);
			myProcessStartInfo.CreateNoWindow = true;
			myProcessStartInfo.RedirectStandardError = true;
			myProcessStartInfo.RedirectStandardOutput = true;
			myProcessStartInfo.UseShellExecute = false;
			// Instead of using asynchronous running, use synchronous and wait for process to exit, so this background thread won't complete until model viewer is closed.
			//      This allows background thread to announce to main thread when model viewer process exits.
			theGameAppProcess.EnableRaisingEvents = true;
			theGameAppProcess.StartInfo = myProcessStartInfo;

			theGameAppProcess.Start();
			theGameAppProcess.WaitForExit();
			theGameAppProcess.Close();
			theGameAppProcess = null;
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
			UpdateProgressInternal(1, string.Empty);
		}

		private void WriteErrorMessage(string line)
		{
			UpdateProgressInternal(1, "ERROR: " + line);
		}

		private void UpdateProgress(int indentLevel, string line)
		{
			string indentedLine = string.Empty;
			for (int i = 1; i <= indentLevel; i++)
				indentedLine += "  ";
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
	}
}