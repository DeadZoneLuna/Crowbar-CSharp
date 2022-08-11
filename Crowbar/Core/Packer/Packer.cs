using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace Crowbar
{
	public class Packer : BackgroundWorker
	{
#region Create and Destroy

		public Packer() : base()
		{

			thePackedLogFiles = new BindingListEx<string>();
			thePackedFiles = new BindingListEx<string>();

			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
			DoWork += Packer_DoWork;
		}

#endregion

#region Properties

#endregion

#region Methods

		public void Run()
		{
			RunWorkerAsync();
		}

		public void SkipCurrentFolder()
		{
			//NOTE: This might have thread race condition, but it probably doesn't matter.
			theSkipCurrentModelIsActive = true;
		}

		public string GetOutputPathFileName(string relativePathFileName)
		{
			string pathFileName = Path.Combine(theOutputPath, relativePathFileName);

			pathFileName = Path.GetFullPath(pathFileName);

			return pathFileName;
		}

#endregion

#region Private Methods

#endregion

#region Private Methods in Background Thread

		private void Packer_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			ReportProgress(0, "");

			theOutputPath = GetOutputPath();

			AppEnums.StatusMessage status = 0;
			if (PackerInputsAreValid())
			{
				status = Pack();
			}
			else
			{
				status = AppEnums.StatusMessage.Error;
			}
			e.Result = GetPackerOutputs(status);

			if (CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private string GetGamePackerPathFileName()
		{
			string gamePackerPathFileName = null;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex];
			gamePackerPathFileName = gameSetup.PackerPathFileName;

			return gamePackerPathFileName;
		}

		private string GetGamePath()
		{
			string gamePath = null;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex];
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName);

			return gamePath;
		}

		private string GetOutputPath()
		{
			string outputPath = null;

			if (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.ParentFolder)
			{
				outputPath = MainCROWBAR.TheApp.Settings.PackOutputParentPath;
			}
			else
			{
				outputPath = MainCROWBAR.TheApp.Settings.PackOutputPath;
			}

			//This will change a relative path to an absolute path.
			outputPath = Path.GetFullPath(outputPath);
			return outputPath;
		}

		private bool PackerInputsAreValid()
		{
			bool inputsAreValid = true;

			string gamePackerPathFileName = GetGamePackerPathFileName();
			GameSetup gameSetup = null;
			string gamePathFileName = null;
			gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex];
			gamePathFileName = gameSetup.GamePathFileName;

			if (!File.Exists(gamePackerPathFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "The model packer, \"" + gamePackerPathFileName + "\", does not exist.");
				UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
			}
			if (!File.Exists(gamePathFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "The game's \"" + gamePathFileName + "\" file does not exist.");
				UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
			}
			if (string.IsNullOrEmpty(MainCROWBAR.TheApp.Settings.PackInputPath))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "Input Folder has not been selected.");
			}
			else if (!Directory.Exists(MainCROWBAR.TheApp.Settings.PackInputPath))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "The Input Folder, \"" + MainCROWBAR.TheApp.Settings.PackInputPath + "\", does not exist.");
			}
			if (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.WorkFolder)
			{
				if (!FileManager.PathExistsAfterTryToCreate(theOutputPath))
				{
					inputsAreValid = false;
					WriteErrorMessage(1, "The Output Folder, \"" + theOutputPath + "\" could not be created.");
				}
			}

			return inputsAreValid;
		}

		private PackerOutputInfo GetPackerOutputs(AppEnums.StatusMessage status)
		{
			PackerOutputInfo packResultInfo = new PackerOutputInfo();

			packResultInfo.theStatus = status;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex];

			if (thePackedFiles.Count > 0)
			{
				packResultInfo.thePackedRelativePathFileNames = thePackedFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.PackLogFileIsChecked)
			{
				packResultInfo.thePackedRelativePathFileNames = thePackedLogFiles;
			}

			return packResultInfo;
		}

		private AppEnums.StatusMessage Pack()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			theSkipCurrentModelIsActive = false;

			thePackedLogFiles.Clear();
			thePackedFiles.Clear();

			string inputPath = MainCROWBAR.TheApp.Settings.PackInputPath;

			string progressDescriptionText = "Packing with " + MainCROWBAR.TheApp.GetProductNameAndVersion() + ": ";

			try
			{
				if (MainCROWBAR.TheApp.Settings.PackMode == AppEnums.PackInputOptions.ParentFolder)
				{
					progressDescriptionText += "\"" + inputPath + "\" (parent folder)";
					UpdateProgressStart(progressDescriptionText + " ...");

					status = CreateLogTextFile(inputPath, null);

					PackFoldersInParentFolder(inputPath);
				}
				else
				{
					progressDescriptionText += "\"" + inputPath + "\"";
					UpdateProgressStart(progressDescriptionText + " ...");
					status = PackOneFolder(inputPath);
				}
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
			}
			finally
			{
				if (theLogFileStream != null)
				{
					theLogFileStream.Flush();
					theLogFileStream.Close();
					theLogFileStream = null;
				}
			}

			UpdateProgressStop("... " + progressDescriptionText + " finished.");

			return status;
		}

		private void PackFoldersInParentFolder(string parentPath)
		{
			foreach (string aChildPath in Directory.GetDirectories(parentPath))
			{
				PackOneFolder(aChildPath);

				//TODO: Double-check if this is wanted. If so, then add equivalent to Unpacker.UnpackModelsInFolder().
				ReportProgress(5, "");

				if (CancellationPending)
				{
					return;
				}
				else if (theSkipCurrentModelIsActive)
				{
					theSkipCurrentModelIsActive = false;
					continue;
				}
			}
		}

		private AppEnums.StatusMessage PackOneFolder(string inputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				if (MainCROWBAR.TheApp.Settings.PackMode == AppEnums.PackInputOptions.Folder)
				{
					status = CreateLogTextFile(null, inputPath);
				}

				UpdateProgress();
				UpdateProgress(1, "Packing \"" + inputPath + "\" ...");

				string result = CheckFiles(inputPath);
				if (result == "success")
				{
					UpdateProgress(2, "Output from packer \"" + GetGamePackerPathFileName() + "\": ");
					RunPackerApp(inputPath);

					if (!theProcessHasOutputData)
					{
						UpdateProgress(2, "ERROR: The packer did not return any status messages.");
						UpdateProgress(2, "CAUSE: The packer is not the correct one for the selected game.");
						UpdateProgress(2, "SOLUTION: Verify integrity of game files via Steam so that the correct packer is installed.");
					}
					else
					{
						ProcessPackage(inputPath);
					}

					//' Clean up any created folders.
					//If qcModelTopNonextantPath <> "" Then
					//	Dim fullPathDeleted As String
					//	fullPathDeleted = FileManager.DeleteEmptySubpath(qcModelTopNonextantPath)
					//	If fullPathDeleted <> "" Then
					//		Me.UpdateProgress(2, "CROWBAR: Deleted empty temporary pack folder """ + fullPathDeleted + """.")
					//	End If
					//End If
				}

				UpdateProgress(1, "... Packing \"" + inputPath + "\" finished. Check above for any errors.");
			}
			catch (Exception ex)
			{
				//TODO: [PackOneFolder] Should at least give an error message to let user know something prevented the pack.
				int debug = 4242;
			}

			return status;
		}

		private string CheckFiles(string inputPath)
		{
			string result = "success";

			//TODO: Determine what to check before Packer runs for a folder.
			string gamePackerPathFileName = GetGamePackerPathFileName();
			string gamePackerFileName = Path.GetFileName(gamePackerPathFileName);
			if (gamePackerFileName == "gmad.exe")
			{
				if (Directory.Exists(inputPath))
				{
					GarrysModSteamAppInfo garrysModAppInfo = new GarrysModSteamAppInfo();
					string addonJsonPathFileName = garrysModAppInfo.CreateAddonJsonFile(inputPath, MainCROWBAR.TheApp.Settings.PackGmaTitle, MainCROWBAR.TheApp.Settings.PackGmaItemTags);
					if (!File.Exists(addonJsonPathFileName))
					{
						result = "error";
					}
				}
			}

			return result;
		}

		private void RunPackerApp(string inputPath)
		{
			string currentFolder = Directory.GetCurrentDirectory();
			string parentPath = FileManager.GetPath(inputPath);
			string inputFolder = Path.GetFileName(inputPath);
			Directory.SetCurrentDirectory(parentPath);

			string gamePackerPathFileName = GetGamePackerPathFileName();
			string gamePackerFileName = Path.GetFileName(gamePackerPathFileName);

			string arguments = "";
			if (gamePackerFileName == "gmad.exe")
			{
				arguments += "create -folder ";
			}
			arguments += "\"";
			arguments += inputFolder;
			arguments += "\"";
			arguments += " ";
			//NOTE: Gmad.exe and vpk.exe expect extra options after the input folder option.
			arguments += MainCROWBAR.TheApp.Settings.PackOptionsText;

			Process myProcess = new Process();
			ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(gamePackerPathFileName, arguments);
			myProcessStartInfo.UseShellExecute = false;
			myProcessStartInfo.RedirectStandardOutput = true;
			myProcessStartInfo.RedirectStandardError = true;
			myProcessStartInfo.RedirectStandardInput = true;
			myProcessStartInfo.CreateNoWindow = true;
			myProcess.StartInfo = myProcessStartInfo;
			//'NOTE: Need this line to make Me.myProcess_Exited be called.
			//myProcess.EnableRaisingEvents = True
			myProcess.OutputDataReceived += myProcess_OutputDataReceived;
			myProcess.ErrorDataReceived += myProcess_ErrorDataReceived;

			myProcess.Start();
			myProcess.StandardInput.AutoFlush = true;
			myProcess.BeginOutputReadLine();
			myProcess.BeginErrorReadLine();
			theProcessHasOutputData = false;
			theGmadResultFileName = "";
			myProcess.WaitForExit();

			myProcess.Close();
			myProcess.OutputDataReceived -= myProcess_OutputDataReceived;
			myProcess.ErrorDataReceived -= myProcess_ErrorDataReceived;

			Directory.SetCurrentDirectory(currentFolder);
		}

		private void ProcessPackage(string inputPath)
		{
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex];
			string gamePackerFileName = Path.GetFileName(gameSetup.PackerPathFileName);
			string sourcePathFileName = inputPath;
			if (gamePackerFileName == "gmad.exe")
			{
				//sourcePathFileName += ".gma"
				// Gmad removes the first dot and text past that from the created file name, 
				//    so use the file name shown in the log from Gmad.
				sourcePathFileName = Path.GetDirectoryName(sourcePathFileName);
				sourcePathFileName = Path.Combine(sourcePathFileName, theGmadResultFileName);
			}
			else
			{
				sourcePathFileName += ".vpk";
			}
			if (File.Exists(sourcePathFileName))
			{
				string targetPath = theOutputPath;
				string targetFileName = Path.GetFileName(sourcePathFileName);
				string targetPathFileName = Path.Combine(targetPath, targetFileName);

				if (string.Compare(sourcePathFileName, targetPathFileName, true) != 0)
				{
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
						UpdateProgress(2, "CROWBAR: Moved package file \"" + sourcePathFileName + "\" to \"" + targetPath + "\"");
					}
					catch (Exception ex)
					{
						UpdateProgress();
						UpdateProgress(2, "WARNING: Crowbar tried to move the file, \"" + sourcePathFileName + "\", to the output folder, but Windows complained with this message: " + ex.Message.Trim());
						UpdateProgress(2, "SOLUTION: Pack again (and hope Windows does not complain again) or move the file yourself.");
						UpdateProgress();
					}
				}

				thePackedFiles.Add(targetFileName);
			}
		}

		//' GoldSource:
		//'     "C:\"                   => ""            [absolute path is same as if the path were relative]
		//'     ""                      => ""            [no "models" so assume relative to it, like with Source]
		//'     "test"                  => "test"        [no "models" so assume relative to it, like with Source]
		//'     "test\models"           => ""            [has "models" so ignore path before it]
		//'     "test\models\subfolder" => "subfolder"   [has "models" so ignore path before it]
		//' Source:
		//'     "C:\"                   => ""            [absolute path is same as GoldSource method]
		//'     "test"                  => "test"        [relative path is always "models" subfolder]
		//Private Function GetModelsSubpath(ByVal iPath As String, ByVal iGameEngine As GameEngine) As String
		//	Dim modelsSubpath As String = ""
		//	Dim tempSubpath As String
		//	Dim lastFolderInPath As String

		//	If iPath = "" Then
		//		Return ""
		//	End If

		//	Dim fullPath As String
		//	fullPath = Path.GetFullPath(iPath)

		//	If iGameEngine = GameEngine.GoldSource OrElse iPath = fullPath Then
		//		tempSubpath = iPath
		//		While tempSubpath <> ""
		//			lastFolderInPath = Path.GetFileName(tempSubpath)
		//			If lastFolderInPath = "models" Then
		//				Exit While
		//			ElseIf lastFolderInPath = "" Then
		//				modelsSubpath = ""
		//				Exit While
		//			Else
		//				modelsSubpath = Path.Combine(lastFolderInPath, modelsSubpath)
		//			End If
		//			tempSubpath = FileManager.GetPath(tempSubpath)
		//		End While
		//	Else
		//		modelsSubpath = iPath
		//	End If

		//	Return modelsSubpath
		//End Function

		private void myProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
		{
			Process myProcess = (Process)sender;
			string line = null;

			try
			{
				line = e.Data;
				if (line != null)
				{
					// Gmad removes the first dot and text past that from the created file name, 
					//    so get the file name shown in the log from Gmad.
					if (line.StartsWith("Successfully saved to "))
					{
						char[] delimiters = {'\"'};
						string[] tokens = {""};
						tokens = line.Split(delimiters);
						theGmadResultFileName = tokens[1];
					}

					theProcessHasOutputData = true;
					UpdateProgress(3, line);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (CancellationPending)
				{
					StopPack(true, myProcess);
				}
				else if (theSkipCurrentModelIsActive)
				{
					StopPack(true, myProcess);
				}
			}
		}

		private void myProcess_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
		{
			Process myProcess = (Process)sender;
			string line = null;

			try
			{
				line = e.Data;
				if (line != null)
				{
					UpdateProgress(3, line);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (CancellationPending)
				{
					StopPack(true, myProcess);
				}
				else if (theSkipCurrentModelIsActive)
				{
					StopPack(true, myProcess);
				}
			}
		}

		private void StopPack(bool processIsCanceled, Process myProcess)
		{
			if (myProcess != null && !myProcess.HasExited)
			{
				try
				{
					myProcess.CancelOutputRead();
					myProcess.CancelErrorRead();
					myProcess.Kill();
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}

			if (processIsCanceled)
			{
				theLastLine = "...Packing canceled.";
			}
		}

		private AppEnums.StatusMessage CreateLogTextFile(string inputParentPath, string inputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex];

			if (MainCROWBAR.TheApp.Settings.PackLogFileIsChecked)
			{
				string inputFolderName = null;
				string logPath = null;
				string logFileName = null;
				string logPathFileName = null;

				try
				{
					if (inputParentPath != null)
					{
						logPath = inputParentPath;
						logFileName = "pack-log.txt";
					}
					else
					{
						logPath = FileManager.GetPath(inputPath);
						inputFolderName = Path.GetFileNameWithoutExtension(inputPath);
						logFileName = inputFolderName + " pack-log.txt";
					}
					FileManager.CreatePath(logPath);
					logPathFileName = Path.Combine(logPath, logFileName);

					theLogFileStream = File.CreateText(logPathFileName);
					theLogFileStream.AutoFlush = true;

					if (File.Exists(logPathFileName))
					{
						thePackedLogFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, logPathFileName));
					}

					theLogFileStream.WriteLine("// " + MainCROWBAR.TheApp.GetHeaderComment());
					theLogFileStream.Flush();
				}
				catch (Exception ex)
				{
					UpdateProgress();
					UpdateProgress(2, "ERROR: Crowbar tried to write the pack log file but the system gave this message: " + ex.Message);
					status = AppEnums.StatusMessage.Error;
				}
			}
			else
			{
				theLogFileStream = null;
			}

			return status;
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

		private void WriteErrorMessage(int indentLevel, string line)
		{
			UpdateProgress(indentLevel, "Crowbar ERROR: " + line);
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
			if (progressValue == 1 && theLogFileStream != null)
			{
				theLogFileStream.WriteLine(line);
				theLogFileStream.Flush();
			}

			ReportProgress(progressValue, line);
		}

#endregion

#region Data

		private bool theSkipCurrentModelIsActive;
		private string theOutputPath;

		private StreamWriter theLogFileStream;
		private string theLastLine;

		private BindingListEx<string> thePackedLogFiles;
		private BindingListEx<string> thePackedFiles;

		private StreamWriter theDefineBonesFileStream;

		private bool theProcessHasOutputData;
		private string theGmadResultFileName;

#endregion

	}

}