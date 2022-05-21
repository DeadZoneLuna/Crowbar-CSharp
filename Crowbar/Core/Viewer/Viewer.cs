//INSTANT C# NOTE: Formerly VB project-level imports:
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
	public class Viewer : BackgroundWorker
	{
#region Create and Destroy

		public Viewer() : base()
		{

			isDisposed = false;

			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
			DoWork += ModelViewer_DoWork;
		}

#region IDisposable Support

		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (!isDisposed)
			{
				//Me.Halt(False)
				if (disposing)
				{
					Free();
				}
				//NOTE: free shared unmanaged resources
			}
			isDisposed = true;
			base.Dispose(disposing);
		}

		//Protected Overrides Sub Finalize()
		//	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		//	Dispose(False)
		//	MyBase.Finalize()
		//End Sub

#endregion

#endregion

#region Init and Free

		//Private Sub Init()
		//End Sub

		private void Free()
		{
			Halt(false);
			FreeViewModel();
		}

#endregion

#region Methods

		public void Run(int gameSetupSelectedIndex, string inputMdlPathFileName, bool viewAsReplacement, string viewAsReplacementExtraSubfolder)
		{
			ViewerInfo info = new ViewerInfo();
			info.viewerAction = ViewerInfo.ViewerActionType.ViewModel;
			info.gameSetupSelectedIndex = gameSetupSelectedIndex;
			info.mdlPathFileName = inputMdlPathFileName;
			info.viewAsReplacement = viewAsReplacement;
			info.viewAsReplacementExtraSubfolder = viewAsReplacementExtraSubfolder;
			RunWorkerAsync(info);
		}

		public void Run(string inputMdlPathFileName, AppEnums.SupportedMdlVersion mdlVersionOverride)
		{
			ViewerInfo info = new ViewerInfo();
			info.viewerAction = ViewerInfo.ViewerActionType.GetData;
			info.mdlPathFileName = inputMdlPathFileName;
			info.mdlVersionOverride = mdlVersionOverride;
			RunWorkerAsync(info);
		}

		public void Run(int gameSetupSelectedIndex)
		{
			ViewerInfo info = new ViewerInfo();
			info.viewerAction = ViewerInfo.ViewerActionType.OpenViewer;
			info.gameSetupSelectedIndex = gameSetupSelectedIndex;
			RunWorkerAsync(info);
		}

		//Public Sub Halt()
		//	Me.Halt(False)
		//End Sub

#endregion

#region Event Handlers

		//Private Sub HlmvApp_Exited(ByVal sender As Object, ByVal e As System.EventArgs)
		//	Me.Halt(True)
		//End Sub

#endregion

#region Private Methods that can be called in either the main thread or the background thread

		private void Halt(bool calledFromBackgroundThread)
		{
			if (theHlmvAppProcess != null)
			{
				//RemoveHandler Me.theHlmvAppProcess.Exited, AddressOf HlmvApp_Exited

				try
				{
					if (!theHlmvAppProcess.HasExited && !theHlmvAppProcess.CloseMainWindow())
					{
						theHlmvAppProcess.Kill();
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
				finally
				{
					//NOTE: Due to threading, Me.theHlmvAppProcess might be Nothing at this point.
					if (theHlmvAppProcess != null)
					{
						theHlmvAppProcess.Close();
						//NOTE: This raises an exception when the background thread has already completed its work.
						//If calledFromBackgroundThread Then
						//	Me.UpdateProgressStop("Model viewer closed.")
						//End If
						theHlmvAppProcess = null;
					}
				}
			}
		}

#endregion

#region Private Methods that are called in the background thread

		private void ModelViewer_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			ReportProgress(0, "");

			ViewerInfo info = (ViewerInfo)e.Argument;

			theInputMdlPathFileName = info.mdlPathFileName;
			theViewAsReplacementExtraSubfolder = info.viewAsReplacementExtraSubfolder;
			theInputMdlIsViewedAsReplacement = info.viewAsReplacement;
			theGameSetup = MainCROWBAR.TheApp.Settings.GameSetups[info.gameSetupSelectedIndex];

			if (ViewerInputsAreOkay(info.viewerAction))
			{
				if (info.viewerAction == ViewerInfo.ViewerActionType.GetData)
				{
					ViewData(info.mdlVersionOverride);
				}
				else if (info.viewerAction == ViewerInfo.ViewerActionType.ViewModel)
				{
					//Me.UpdateProgress(1, "Model viewer opening ...")
					UpdateProgress(1, "Model viewer opened.");
					ViewModel();
					//Me.UpdateProgress(1, "Model viewer opened.")
				}
				else if (info.viewerAction == ViewerInfo.ViewerActionType.OpenViewer)
				{
					//Me.UpdateProgress(1, "Model viewer opening ...")
					UpdateProgress(1, "Model viewer opened.");
					OpenViewer();
					//Me.UpdateProgress(1, "Model viewer opened.")
				}
			}
		}

		//TODO: Check inputs as done in Compiler.CompilerInputsAreValid().
		private bool ViewerInputsAreOkay(ViewerInfo.ViewerActionType viewerAction)
		{
			bool inputsAreValid = true;


			if (viewerAction == ViewerInfo.ViewerActionType.GetData || viewerAction == ViewerInfo.ViewerActionType.ViewModel)
			{
				if (string.IsNullOrEmpty(theInputMdlPathFileName))
				{
					UpdateProgressStart("");
					WriteErrorMessage("MDL file is blank.");
					inputsAreValid = false;
				}
				else if (!File.Exists(theInputMdlPathFileName))
				{
					UpdateProgressStart("");
					WriteErrorMessage("MDL file does not exist.");
					inputsAreValid = false;
				}
			}

			if (viewerAction == ViewerInfo.ViewerActionType.ViewModel || viewerAction == ViewerInfo.ViewerActionType.OpenViewer)
			{
				string gamePath = null;
				string modelViewerPathFileName = null;
				gamePath = FileManager.GetPath(theGameSetup.GamePathFileName);
				//modelViewerPathFileName = Path.Combine(FileManager.GetPath(Me.theGameSetup.CompilerPathFileName), "hlmv.exe")
				modelViewerPathFileName = theGameSetup.ViewerPathFileName;

				if (!File.Exists(modelViewerPathFileName))
				{
					inputsAreValid = false;
					WriteErrorMessage("The model viewer, \"" + modelViewerPathFileName + "\", does not exist.");
					UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
				}
			}

			if (viewerAction == ViewerInfo.ViewerActionType.OpenViewer)
			{
			}

			return inputsAreValid;
		}

		private void ViewData(AppEnums.SupportedMdlVersion mdlVersionOverride)
		{
			string progressDescriptionText = "Getting model data for ";
			progressDescriptionText += "\"" + Path.GetFileName(theInputMdlPathFileName) + "\"";

			UpdateProgressStart(progressDescriptionText + " ...");

			ShowDataFromMdlFile(mdlVersionOverride);

			UpdateProgressStop("... " + progressDescriptionText + " finished.");
		}

		private void ShowDataFromMdlFile(AppEnums.SupportedMdlVersion mdlVersionOverride)
		{
			SourceModel model = null;
			int version = 0;
			try
			{
				if (File.Exists(theInputMdlPathFileName))
				{
					model = SourceModel.Create(theInputMdlPathFileName, mdlVersionOverride, ref version);
					if (model != null)
					{
						List<string> textLines = model.GetOverviewTextLines(theInputMdlPathFileName, mdlVersionOverride);
						UpdateProgress();
						foreach (string aTextLine in textLines)
						{
							UpdateProgress(1, aTextLine);
						}
					}
					else
					{
						UpdateProgress(1, "ERROR: Model version not currently supported: " + version.ToString());
						UpdateProgress(1, "       Try changing 'Override MDL version' option.");
					}
				}
				else
				{
					UpdateProgress(1, "ERROR: Model file not found: " + "\"" + theInputMdlPathFileName + "\"");
				}
			}
			catch (Exception ex)
			{
				WriteErrorMessage(ex.Message);
			}
		}

		private void ViewModel()
		{
			InitViewModel();
			RunHlmvApp(true);
			FreeViewModel();
		}

		private void OpenViewer()
		{
			RunHlmvApp(false);
		}

		private void RunHlmvApp(bool viewerIsOpeningModel)
		{
			string modelViewerPathFileName = null;
			string gamePath = null;
			string tempGamePath = null;
			string gameFileName = null;
			string gameModelsPath = null;
			string currentFolder = "";

			if (theInputMdlIsViewedAsReplacement)
			{
				tempGamePath = GetTempGamePath();
			}
			else
			{
				tempGamePath = FileManager.GetPath(theGameSetup.GamePathFileName);
			}
			gameFileName = Path.GetFileName(theGameSetup.GamePathFileName);
			//modelViewerPathFileName = Path.Combine(FileManager.GetPath(Me.theGameSetup.CompilerPathFileName), "hlmv.exe")
			modelViewerPathFileName = theGameSetup.ViewerPathFileName;
			gameModelsPath = Path.Combine(tempGamePath, "models");

			//TODO: Counter-Strike: Source and Portal (and maybe others) do not have a "models" folder.
			//      Can Crowbar avoid SetCurrentDirectory() in these cases?

			if (Directory.Exists(gameModelsPath))
			{
				currentFolder = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(gameModelsPath);
			}

			string arguments = "";
			if (gameFileName.ToLower() == "gameinfo.txt")
			{
				gamePath = FileManager.GetPath(theGameSetup.GamePathFileName);
				//NOTE: The -olddialogs param adds "(Steam) Load Model" menu item, which usually means HLMV can then open a model from anywhere in file system via the "Load Model" menu item.
				//      This also allows some HLMVs to open MDL v49 via the View button.
				arguments += " -olddialogs -game \"";
				arguments += gamePath;
				arguments += "\"";
			}
			if (viewerIsOpeningModel)
			{
				arguments += " \"";
				if (theInputMdlIsViewedAsReplacement)
				{
					arguments += theInputMdlRelativePathName;
				}
				else
				{
					arguments += theInputMdlPathFileName;
				}
				arguments += "\"";
			}

			theHlmvAppProcess = new Process();
			ProcessStartInfo hlmvAppProcessStartInfo = new ProcessStartInfo(modelViewerPathFileName, arguments);
			hlmvAppProcessStartInfo.CreateNoWindow = true;
			hlmvAppProcessStartInfo.RedirectStandardError = true;
			hlmvAppProcessStartInfo.RedirectStandardOutput = true;
			hlmvAppProcessStartInfo.UseShellExecute = false;
			//NOTE: Instead of using asynchronous running, use synchronous and wait for process to exit, 
			//      so this background thread won't complete until model viewer is closed.
			//      This allows background thread to announce to main thread when model viewer process exits.
			theHlmvAppProcess.EnableRaisingEvents = false;
			theHlmvAppProcess.StartInfo = hlmvAppProcessStartInfo;

			theHlmvAppProcess.Start();
			theHlmvAppProcess.WaitForExit();
			Halt(true);

			//TODO: Test if this code works if placed immediately after starting process, to prevent a second view from setting current folder to what the first view was using as a temp current folder.
			if (!string.IsNullOrEmpty(currentFolder))
			{
				Directory.SetCurrentDirectory(currentFolder);
			}
		}

		private void InitViewModel()
		{
			if (theGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				string gamePath = null;
				string gameModelsPath = null;

				gamePath = FileManager.GetPath(theGameSetup.GamePathFileName);
				gameModelsPath = Path.Combine(gamePath, "models");

				if (!theInputMdlPathFileName.StartsWith(gameModelsPath))
				{
					//NOTE: Avoid any changes and copying if user used the "View" button.
					if (theInputMdlIsViewedAsReplacement)
					{
						ModifyGameInfoFile();

						theInputMdlRelativePathName = CreateReplacementModelFiles();
						if (string.IsNullOrEmpty(theInputMdlRelativePathName))
						{
							return;
						}

						//TODO: Uncomment this after it only copies the files used by the model.
						//Me.CopyMaterialAndTextureFiles()
					}
				}
			}
		}

		private void FreeViewModel()
		{
			if (theGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				RevertGameInfoFile();

				if (theInputMdlIsViewedAsReplacement)
				{
					DeleteReplacementModelFiles();
				}

				//TODO: Uncomment this after CopyMaterialAndTextureFiles() has been redone.
				//Me.DeleteMaterialAndTextureFiles()
			}
		}

		private string GetTempGamePath()
		{
			string gamePath = FileManager.GetPath(theGameSetup.GamePathFileName);

			//NOTE: These two lines change gamePath from actual gamePath to the new "crowbar" gamePath for temp use.
			gamePath = FileManager.GetPath(gamePath);
			gamePath = Path.Combine(gamePath, "crowbar");

			return gamePath;
		}

		private void ModifyGameInfoFile()
		{
			theGameInfoFile = GameInfoTxtFile.Create();
			theGameInfoFile.WriteNewGamePath(theGameSetup.GamePathFileName, "crowbar");
		}

		private void RevertGameInfoFile()
		{
			if (theGameInfoFile != null)
			{
				theGameInfoFile.RestoreGameInfoFile(theGameSetup.GamePathFileName);
				theGameInfoFile = null;
			}
		}

		private string CreateReplacementModelFiles()
		{
			string replacementMdlRelativePathFileName = null;
			string replacementMdlPathFileName = null;

			string replacementMdlRelativePath = null;
			string gamePath = null;
			string gameModelsPath = null;
			string gameModelsTempPath = null;
			replacementMdlRelativePath = theViewAsReplacementExtraSubfolder;
			gamePath = GetTempGamePath();
			gameModelsPath = Path.Combine(gamePath, "models");
			gameModelsTempPath = Path.Combine(gameModelsPath, replacementMdlRelativePath);

			if (FileManager.PathExistsAfterTryToCreate(gameModelsTempPath))
			{
				string replacementMdlFileName = Path.GetFileName(theInputMdlPathFileName);
				replacementMdlRelativePathFileName = Path.Combine(replacementMdlRelativePath, replacementMdlFileName);
				thePathForModelFiles = gameModelsPath;
				thePathForModelFilesForViewAsReplacement = gameModelsTempPath;
				replacementMdlPathFileName = Path.Combine(gameModelsTempPath, replacementMdlFileName);

				try
				{
					if (File.Exists(replacementMdlPathFileName))
					{
						File.Delete(replacementMdlPathFileName);
					}
					File.Copy(theInputMdlPathFileName, replacementMdlPathFileName);
				}
				catch (Exception ex)
				{
					WriteErrorMessage("Crowbar tried to copy the file \"" + theInputMdlPathFileName + "\" to \"" + replacementMdlPathFileName + "\" but Windows gave this message: " + ex.Message);
				}

				if (File.Exists(replacementMdlPathFileName))
				{
					SourceModel model = null;
					int version = 0;
					try
					{
						model = SourceModel.Create(theInputMdlPathFileName, AppEnums.SupportedMdlVersion.DoNotOverride, ref version);
						if (model != null)
						{
							model.WriteMdlFileNameToMdlFile(replacementMdlPathFileName, replacementMdlRelativePathFileName);
							model.WriteAniFileNameToMdlFile(replacementMdlPathFileName, replacementMdlRelativePathFileName);
						}
						else
						{
							WriteErrorMessage("Model version not currently supported: " + version.ToString());
							return "";
						}
					}
					catch (FormatException ex)
					{
						WriteErrorMessage(ex.Message);
					}
					catch (Exception ex)
					{
						WriteErrorMessage("Crowbar tried to write to the temporary replacement MDL file but the system gave this message: " + ex.Message);
						return "";
					}

					string inputMdlPath = null;
					string inputMdlFileNameWithoutExtension = null;
					string replacementMdlPath = null;
					string targetFileName = null;
					string targetPathFileName = "";
					inputMdlPath = FileManager.GetPath(theInputMdlPathFileName);
					inputMdlFileNameWithoutExtension = Path.GetFileNameWithoutExtension(theInputMdlPathFileName);
					replacementMdlPath = FileManager.GetPath(replacementMdlPathFileName);
					theModelFilesForViewAsReplacement = new List<string>();
					foreach (string inputPathFileName in Directory.GetFiles(inputMdlPath, inputMdlFileNameWithoutExtension + ".*"))
					{
						try
						{
							targetFileName = Path.GetFileName(inputPathFileName);
							targetPathFileName = Path.Combine(replacementMdlPath, targetFileName);
							if (!File.Exists(targetPathFileName))
							{
								File.Copy(inputPathFileName, targetPathFileName);
							}
							theModelFilesForViewAsReplacement.Add(targetPathFileName);
						}
						catch (Exception ex)
						{
							WriteErrorMessage("Crowbar tried to copy the file \"" + inputPathFileName + "\" to \"" + targetPathFileName + "\" but Windows gave this message: " + ex.Message);
						}
					}
				}
			}
			else
			{
				WriteErrorMessage("Crowbar tried to create \"" + gameModelsTempPath + "\", but it failed.");
				replacementMdlRelativePathFileName = "";
			}

			return replacementMdlRelativePathFileName;
		}

		private void DeleteReplacementModelFiles()
		{
			if (theModelFilesForViewAsReplacement == null)
			{
				return;
			}

			string pathFileName = null;
			for (int modelFileIndex = theModelFilesForViewAsReplacement.Count - 1; modelFileIndex >= 0; modelFileIndex--)
			{
				try
				{
					pathFileName = theModelFilesForViewAsReplacement[modelFileIndex];
					if (File.Exists(pathFileName))
					{
						File.Delete(pathFileName);
						theModelFilesForViewAsReplacement.RemoveAt(modelFileIndex);
					}
				}
				catch (Exception ex)
				{
					//TODO: Write a warning message.
					int debug = 4242;
				}
			}

			try
			{
				//NOTE: Give a little time for other Viewer threads to complete; otherwise the Delete will not happen.
				System.Threading.Thread.Sleep(500);
				if (Directory.Exists(thePathForModelFilesForViewAsReplacement))
				{
					Directory.Delete(thePathForModelFilesForViewAsReplacement);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			try
			{
				//NOTE: Give a little time for other Viewer threads to complete; otherwise the Delete will not happen.
				System.Threading.Thread.Sleep(500);
				if (Directory.Exists(thePathForModelFiles))
				{
					Directory.Delete(thePathForModelFiles);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void CopyMaterialAndTextureFiles()
		{
			string tempPath = null;
			string tempFolder = null;
			string inputMaterialsPath = null;
			tempPath = FileManager.GetPath(theInputMdlPathFileName);
			do
			{
				if (tempPath.Length <= 3)
				{
					//TODO: Tell user that model is not in a models folder.
					return;
				}
				tempFolder = Path.GetFileName(tempPath);
				tempPath = FileManager.GetPath(tempPath);
			} while (!(tempFolder == "models"));
			inputMaterialsPath = Path.Combine(tempPath, "materials");

			string gamePath = null;
			string gameMaterialsPath = null;
			gamePath = GetTempGamePath();
			gameMaterialsPath = Path.Combine(gamePath, "materials");

			if (inputMaterialsPath != gameMaterialsPath && Directory.Exists(inputMaterialsPath))
			{
				//Me.theGameMaterialsFolder = GameMaterialsFolder.Create()
				//Me.theGameMaterialsFolder.CopyFolder(inputMaterialsPath, gameMaterialsPath)
				try
				{
					if (FileManager.PathExistsAfterTryToCreate(gameMaterialsPath))
					{
						ConversionHelper.CopyDirectory(inputMaterialsPath, gameMaterialsPath);
						//Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(inputMaterialsPath, gameMaterialsPath);
						theGameMaterialsFolder = gameMaterialsPath;
					}
					else
					{
						//errorMessage = "Crowbar tried to create """ + gameMaterialsPath + """, but it failed."
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
					//Throw
				}
			}
		}

		private void DeleteMaterialAndTextureFiles()
		{
			if (theGameMaterialsFolder != null)
			{
				try
				{
					string gamePath = null;
					string gameMaterialsPath = null;
					gamePath = GetTempGamePath();
					gameMaterialsPath = Path.Combine(gamePath, "materials");

					if (theGameMaterialsFolder == gameMaterialsPath)
					{
						theGameMaterialsFolder = "";

						if (Directory.Exists(gameMaterialsPath))
						{
							Directory.Delete(gameMaterialsPath, true);
						}
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
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

		private GameSetup theGameSetup;
		private string theInputMdlPathFileName;
		private string theInputMdlRelativePathName;
		private bool theInputMdlIsViewedAsReplacement;
		private string theViewAsReplacementExtraSubfolder;

		private Process theHlmvAppProcess;
		private GameInfoTxtFile theGameInfoFile;
		//Private theGameMaterialsFolder As GameMaterialsFolder
		private string theGameMaterialsFolder;
		private List<string> theModelFilesForViewAsReplacement;
		private string thePathForModelFiles;
		private string thePathForModelFilesForViewAsReplacement;
		//Private theMaterialPathsThatWereCreated As List(Of String)
		//Private theMaterialFilesThatWereCopied As List(Of String)
		//Private theMaterialsFolderThatWasRenamed As String

#endregion

	}

}