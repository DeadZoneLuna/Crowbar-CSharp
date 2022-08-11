using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace Crowbar
{
	public class Compiler : BackgroundWorker
	{
		#region Data
		private bool theSkipCurrentModelIsActive;
		private string theInputQcPath;
		private string theOutputPath;
		//Private theModelOutputPath As String

		private StreamWriter theLogFileStream;
		private string theLastLine;

		private BindingListEx<string> theCompiledLogFiles;
		private BindingListEx<string> theCompiledMdlFiles;

		private StreamWriter theDefineBonesFileStream;

		private bool theProcessHasOutputData;
		#endregion

		#region Create and Destroy
		public Compiler() : base()
		{
			theCompiledLogFiles = new BindingListEx<string>();
			theCompiledMdlFiles = new BindingListEx<string>();

			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
			DoWork += Compiler_DoWork;
		}
		#endregion

		#region Properties

		#endregion

		#region Methods
		public void Run()
		{
			RunWorkerAsync();
		}

		public void SkipCurrentModel()
		{
			//NOTE: This might have thread race condition, but it probably doesn't matter.
			theSkipCurrentModelIsActive = true;
		}

		public string GetOutputPathFileName(string relativePathFileName)
		{
			return Path.GetFullPath(Path.Combine(theOutputPath, relativePathFileName));
		}
		#endregion

		#region Private Methods

		#endregion

		#region Private Methods in Background Thread
		private void Compiler_DoWork(object sender, DoWorkEventArgs e)
		{
			ReportProgress(0, "");

			theOutputPath = GetOutputPath();

			AppEnums.StatusMessage status;
			if (CompilerInputsAreValid())
				status = Compile();
			else
				status = AppEnums.StatusMessage.Error;
			e.Result = GetCompilerOutputs(status);

			if (CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private string GetGameCompilerPathFileName()
		{
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			return gameSetup.CompilerPathFileName;
		}

		private string GetGamePath()
		{
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			return FileManager.GetPath(gameSetup.GamePathFileName);
		}

		private string GetGameModelsPath()
		{
			return Path.Combine(GetGamePath(), "models");
		}

		//Private Function GetOutputPath() As String
		//	Dim outputPath As String

		//	If TheApp.Settings.CompileOutputFolderIsChecked Then
		//		If TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName Then
		//			If File.Exists(TheApp.Settings.CompileQcPathFileName) Then
		//				outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.CompileQcPathFileName), TheApp.Settings.CompileOutputSubfolderName)
		//			ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
		//				outputPath = Path.Combine(TheApp.Settings.CompileQcPathFileName, TheApp.Settings.CompileOutputSubfolderName)
		//			Else
		//				outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		//			End If
		//		Else
		//			outputPath = TheApp.Settings.CompileOutputFullPath
		//		End If
		//	Else
		//		outputPath = Me.GetGameModelsPath()
		//	End If

		//	'This will change a relative path to an absolute path.
		//	outputPath = Path.GetFullPath(outputPath)
		//	Return outputPath
		//End Function

		private string GetOutputPath()
		{
			string outputPath;
			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption != AppEnums.CompileOutputPathOptions.GameModelsFolder)
			{
				if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.Subfolder)
				{
					if (File.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
						outputPath = Path.Combine(FileManager.GetPath(MainCROWBAR.TheApp.Settings.CompileQcPathFileName), MainCROWBAR.TheApp.Settings.CompileOutputSubfolderName);
					else if (Directory.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
						outputPath = Path.Combine(MainCROWBAR.TheApp.Settings.CompileQcPathFileName, MainCROWBAR.TheApp.Settings.CompileOutputSubfolderName);
					else
						outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
				else
					outputPath = MainCROWBAR.TheApp.Settings.CompileOutputFullPath;
			}
			else
				outputPath = GetGameModelsPath();

			//This will change a relative path to an absolute path.
			return Path.GetFullPath(outputPath);
		}

		private bool CompilerInputsAreValid()
		{
			bool inputsAreValid = true;

			//'NOTE: Check for qc path file name first, because status file is written relative to it.
			//If Not File.Exists(info.qcPathFileName) Then
			//	'WriteCriticalErrorMesssage("", Nothing, "ERROR: Missing file.", e, info)
			//	WriteCriticalErrorMesssage("", "Missing file: " + info.qcPathFileName, info)
			//	Return
			//End If
			//If Not File.Exists(info.compilerPathFileName) Then
			//	'WriteCriticalErrorMesssage(info.qcPathFileName, Nothing, "ERROR: Missing file.", e, info)
			//	WriteCriticalErrorMesssage(info.qcPathFileName, "Missing file: " + info.compilerPathFileName, info)
			//	Return
			//End If
			//If Not File.Exists(info.gamePathFileName) Then
			//	'WriteCriticalErrorMesssage(info.qcPathFileName, Nothing, "ERROR: Missing file.", e, info)
			//	WriteCriticalErrorMesssage(info.qcPathFileName, "Missing file: " + info.gamePathFileName, info)
			//	Return
			//End If

			string gameCompilerPathFileName = GetGameCompilerPathFileName();
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			string gamePathFileName = gameSetup.GamePathFileName;

			if (!File.Exists(gameCompilerPathFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "The model compiler, \"" + gameCompilerPathFileName + "\", does not exist.");
				UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
			}

			//TODO: [CompilerInputsAreValid] If GoldSource, then only check for liblist.gam if output is for game's "models" folder.
			//TODO: [CompilerInputsAreValid] Change error message to include "liblist.gam" or "gameinfo.txt" as appropriate.
			if (!File.Exists(gamePathFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "The game's \"" + gamePathFileName + "\" file does not exist.");
				UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
			}

			if (string.IsNullOrEmpty(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "QC file or folder has not been selected.");
			}
			else if (MainCROWBAR.TheApp.Settings.CompileMode == AppEnums.InputOptions.File && !File.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "The QC file, \"" + MainCROWBAR.TheApp.Settings.CompileQcPathFileName + "\", does not exist.");
			}

			if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked)
			{
				if (MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesCreateFileIsChecked)
				{
					string defineBonesPathFileName = GetDefineBonesPathFileName();
					if (File.Exists(defineBonesPathFileName) && !MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesOverwriteQciFileIsChecked)
					{
						inputsAreValid = false;
						WriteErrorMessage(1, "The DefineBones file, \"" + defineBonesPathFileName + "\", already exists.");
					}
				}
			}
			//If TheApp.Settings.CompileOutputFolderIsChecked Then
			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption != AppEnums.CompileOutputPathOptions.GameModelsFolder)
			{
				if (!FileManager.PathExistsAfterTryToCreate(theOutputPath))
				{
					inputsAreValid = false;
					WriteErrorMessage(1, "The Output Folder, \"" + theOutputPath + "\" could not be created.");
				}
			}

			return inputsAreValid;
		}

		private CompilerOutputInfo GetCompilerOutputs(AppEnums.StatusMessage status)
		{
			CompilerOutputInfo compileResultInfo = new CompilerOutputInfo { theStatus = status };
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			if (theCompiledMdlFiles.Count > 0)
				compileResultInfo.theCompiledRelativePathFileNames = theCompiledMdlFiles;
			else if (gameSetup.GameEngine == AppEnums.GameEngine.GoldSource && MainCROWBAR.TheApp.Settings.CompileGoldSourceLogFileIsChecked)
				compileResultInfo.theCompiledRelativePathFileNames = theCompiledLogFiles;
			else if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileSourceLogFileIsChecked)
				compileResultInfo.theCompiledRelativePathFileNames = theCompiledLogFiles;

			return compileResultInfo;
		}

		private AppEnums.StatusMessage Compile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			theSkipCurrentModelIsActive = false;

			theCompiledLogFiles.Clear();
			theCompiledMdlFiles.Clear();

			string qcPathFileName = MainCROWBAR.TheApp.Settings.CompileQcPathFileName;
			if (File.Exists(qcPathFileName))
				theInputQcPath = FileManager.GetPath(qcPathFileName);
			else if (Directory.Exists(qcPathFileName))
				theInputQcPath = qcPathFileName;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			//Dim info As New CompilerInputInfo()
			//info.compilerPathFileName = gameSetup.CompilerPathFileName
			//info.compilerOptions = TheApp.Settings.CompileOptionsText
			//info.gamePathFileName = gameSetup.GamePathFileName
			//info.qcPathFileName = TheApp.Settings.CompileQcPathFileName
			//info.customModelFolder = TheApp.Settings.CompileOutputSubfolderName
			//info.theCompileMode = TheApp.Settings.CompileMode

			string defineBonesText = string.Empty;
			if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked)
				defineBonesText = "Define Bones ";

			string progressDescriptionText = "Compiling " + defineBonesText + "with " + MainCROWBAR.TheApp.GetProductNameAndVersion() + ": ";

			try
			{
				if (MainCROWBAR.TheApp.Settings.CompileMode == AppEnums.InputOptions.FolderRecursion)
				{
					progressDescriptionText += "\"" + theInputQcPath + "\" (folder + subfolders)";
					UpdateProgressStart(progressDescriptionText + " ...");

					status = CreateLogTextFile(string.Empty);
					//If status = StatusMessage.Error Then
					//	Return status
					//End If

					CompileModelsInFolderRecursively(theInputQcPath);
				}
				else if (MainCROWBAR.TheApp.Settings.CompileMode == AppEnums.InputOptions.Folder)
				{
					progressDescriptionText += "\"" + theInputQcPath + "\" (folder)";
					UpdateProgressStart(progressDescriptionText + " ...");

					status = CreateLogTextFile(string.Empty);
					//If status = StatusMessage.Error Then
					//	Return status
					//End If

					CompileModelsInFolder(theInputQcPath);
				}
				else
				{
					progressDescriptionText += "\"" + qcPathFileName + "\"";
					UpdateProgressStart(progressDescriptionText + " ...");
					status = CompileOneModel(qcPathFileName);
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

		private void CompileModelsInFolderRecursively(string modelsPathName)
		{
			CompileModelsInFolder(modelsPathName);
			foreach (string aPathName in Directory.GetDirectories(modelsPathName))
			{
				CompileModelsInFolderRecursively(aPathName);
				if (CancellationPending)
					return;
			}
		}

		private void CompileModelsInFolder(string modelsPathName)
		{
			foreach (string aPathFileName in Directory.GetFiles(modelsPathName, "*.qc"))
			{
				CompileOneModel(aPathFileName);

				//TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
				ReportProgress(5, string.Empty);

				if (CancellationPending)
					return;
				else if (theSkipCurrentModelIsActive)
				{
					theSkipCurrentModelIsActive = false;
					continue;
				}
			}
		}

		//SET Left4Dead2PathRootFolder=C:\Program Files (x86)\Steam\SteamApps\common\left 4 dead 2\
		//SET StudiomdlPathName=%Left4Dead2PathRootFolder%bin\studiomdl.exe
		//SET Left4Dead2PathSubFolder=%Left4Dead2PathRootFolder%left4dead2
		//SET StudiomdlParams=-game "%Left4Dead2PathSubFolder%" -nop4 -verbose -nox360
		//SET FileName=%ModelName%_%TargetApp%
		//"%StudiomdlPathName%" %StudiomdlParams% .\%FileName%.qc > .\%FileName%.log
		private AppEnums.StatusMessage CompileOneModel(string qcPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				string qcPath = FileManager.GetPath(qcPathFileName);
				string qcFileName = Path.GetFileName(qcPathFileName);
				string qcRelativePath = FileManager.GetRelativePathFileName(theInputQcPath, FileManager.GetPath(qcPathFileName));
				string qcRelativePathFileName = Path.Combine(qcRelativePath, qcFileName);
				string gameModelsPath = GetGameModelsPath();
				GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

				string compiledMdlPathFileName;
				SourceQcFile qcFile = new SourceQcFile();
				string qcModelName = qcFile.GetQcModelName(qcPathFileName);
				try
				{
					compiledMdlPathFileName = Path.GetFullPath(qcModelName);
					if (compiledMdlPathFileName != qcModelName)
					{
						if (gameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
						{
							//	- The compiler does not create folders, so Crowbar needs to create the relative or absolute path found in $modelname, 
							//		starting in the "current folder" [SetCurrentDirectory()].
							//		* For example, with $modelname "C:\valve/models/barney.mdl", need to create "C:\valve\models" path.
							//		* For example, with $modelname "valve/models/barney.mdl", need to create "[current folder]\valve\models" path.
							compiledMdlPathFileName = Path.Combine(qcPath, qcModelName);
						}
						else
							compiledMdlPathFileName = Path.Combine(gameModelsPath, qcModelName);

						compiledMdlPathFileName = Path.GetFullPath(compiledMdlPathFileName);
					}

					if (Path.GetExtension(compiledMdlPathFileName) != ".mdl")
						compiledMdlPathFileName = Path.ChangeExtension(compiledMdlPathFileName, ".mdl");
				}
				catch (Exception ex)
				{
					compiledMdlPathFileName = string.Empty;
				}

				string compiledMdlPath = FileManager.GetPath(compiledMdlPathFileName);
				string qcModelTopNonextantPath = string.Empty;
				FileManager.GetLongestExtantPath(compiledMdlPath, ref qcModelTopNonextantPath);
				if (!string.IsNullOrEmpty(qcModelTopNonextantPath))
					FileManager.CreatePath(compiledMdlPath);

				//Me.CreateLogTextFile(qcPathFileName)
				if (MainCROWBAR.TheApp.Settings.CompileMode == AppEnums.InputOptions.File)
					status = CreateLogTextFile(qcPathFileName);

				string defineBonesText = string.Empty;
				if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked)
					defineBonesText = "Define Bones of ";

				UpdateProgress();
				UpdateProgress(1, "Compiling " + defineBonesText + "\"" + qcRelativePathFileName + "\" ...");

				string result = CheckFiles();
				if (result == "success")
				{
					if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesCreateFileIsChecked)
						OpenDefineBonesFile();

					UpdateProgress(2, "Output from compiler \"" + GetGameCompilerPathFileName() + "\": ");
					RunStudioMdlApp(qcPath, qcFileName);

					if (!theProcessHasOutputData)
					{
						UpdateProgress(2, "ERROR: The compiler did not return any status messages.");
						UpdateProgress(2, "CAUSE: The compiler is not the correct one for the selected game.");
						UpdateProgress(2, "SOLUTION: Verify integrity of game files via Steam so that the correct compiler is installed.");
					}
					else if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked)
					{
						if (theDefineBonesFileStream != null)
						{
							string qciPathFileName = ((FileStream)theDefineBonesFileStream.BaseStream).Name;
							CloseDefineBonesFile();

							//NOTE: Must do this after closing define bones file.
							if (File.Exists(qciPathFileName))
							{
								FileInfo qciFileInfo = new FileInfo(qciPathFileName);
								if (qciFileInfo.Length == 0)
								{
									UpdateProgress(2, "CROWBAR WARNING: No define bones were written to QCI file.");

									try
									{
										File.Delete(qciPathFileName);
									}
									catch (Exception ex)
									{
										UpdateProgress(2, "CROWBAR WARNING: Failed to delete empty QCI file: \"" + qciPathFileName + "\"");
									}
								}
								else
								{
									UpdateProgress(2, "CROWBAR: Wrote define bones into QCI file: \"" + qciPathFileName + "\"");

									if (MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesModifyQcFileIsChecked)
									{
										string line = InsertAnIncludeDefineBonesFileCommandIntoQcFile(qciPathFileName);
										UpdateProgress(2, "CROWBAR: Wrote in the QC file this line: " + line);
									}
								}
							}
							else
							{
								UpdateProgress(2, "CROWBAR WARNING: Failed to write QCI file: \"" + qciPathFileName + "\"");
							}
						}
					}
					else
					{
						if (File.Exists(compiledMdlPathFileName))
							ProcessCompiledModel(compiledMdlPathFileName, qcModelName);
					}

					// Clean up any created folders.
					//If qcModelTopFolderPath <> "" Then
					//	Dim fullPathDeleted As String
					//	fullPathDeleted = FileManager.DeleteEmptySubpath(qcModelTopFolderPath)
					//	If fullPathDeleted <> "" Then
					//		Me.UpdateProgress(2, "Crowbar: Deleted empty temporary compile folder """ + fullPathDeleted + """")
					//	End If
					//End If
					//------
					if (!string.IsNullOrEmpty(qcModelTopNonextantPath))
					{
						string fullPathDeleted = FileManager.DeleteEmptySubpath(qcModelTopNonextantPath);
						if (!string.IsNullOrEmpty(fullPathDeleted))
							UpdateProgress(2, "CROWBAR: Deleted empty temporary compile folder \"" + fullPathDeleted + "\".");
					}
				}

				UpdateProgress(1, "... Compiling " + defineBonesText + "\"" + qcRelativePathFileName + "\" finished. Check above for any errors.");
			}
			catch (Exception ex)
			{
				//TODO: [CompileOneModel] Should at least give an error message to let user know something prevented the compile.
				int debug = 4242;
				//Finally
				//	If Me.theLogFileStream IsNot Nothing Then
				//		Me.theLogFileStream.Flush()
				//		Me.theLogFileStream.Close()
				//	End If
			}

			return status;
		}

		private string CheckFiles()
		{
			//TODO: Implement counting of all materials used in all mesh SMD files, excluding the phy mesh.
			return "success";
		}

		private void RunStudioMdlApp(string qcPath, string qcFileName)
		{
			string currentFolder = Directory.GetCurrentDirectory();

			Directory.SetCurrentDirectory(qcPath);
			string gameCompilerPathFileName = GetGameCompilerPathFileName();

			string arguments = string.Empty;
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			if (gameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				arguments += "-game";
				arguments += " ";
				arguments += "\"";
				arguments += GetGamePath();
				arguments += "\"";
				arguments += " ";
			}
			arguments += MainCROWBAR.TheApp.Settings.CompileOptionsText;
			arguments += " ";
			arguments += "\"";
			arguments += qcFileName;
			arguments += "\"";

			Process myProcess = new Process();
			ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(gameCompilerPathFileName, arguments);
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
			//Directory.SetCurrentDirectory(currentFolder)
			myProcess.StandardInput.AutoFlush = true;
			myProcess.BeginOutputReadLine();
			myProcess.BeginErrorReadLine();
			theProcessHasOutputData = false;

			//myProcess.StandardOutput.ReadToEnd()
			//'NOTE: Do this to handle "hit a key to continue" at the end of Dota 2's compiler.
			//myProcess.StandardInput.Write(" ")
			//myProcess.StandardInput.Close()

			myProcess.WaitForExit();

			myProcess.Close();
			myProcess.OutputDataReceived -= myProcess_OutputDataReceived;
			myProcess.ErrorDataReceived -= myProcess_ErrorDataReceived;

			Directory.SetCurrentDirectory(currentFolder);
		}

		// Possible source and target paths:
		// mdlRelativePathFileName = qcFile.GetMdlRelativePathFileName(qcPathFileName)
		// GoldSource:
		//     source (compile) path  : FileManager.GetPath(compiledMdlPathFileName)
		//     Game's "models" folder : Me.theOutputPath + modelsSubpath
		//     Work folder            : Me.theOutputPath + mdlRelativePathStartingAtModels
		//     Subfolder (of QC input): Me.theOutputPath + mdlRelativePathStartingAtModels
		// Source:
		//     source (compile) path  : FileManager.GetPath(compiledMdlPathFileName)
		//     Game's "models" folder : Me.theOutputPath + modelsSubpath OR source (compile) path
		//     Work folder            : Me.theOutputPath + mdlRelativePathStartingAtModels
		//     Subfolder (of QC input): Me.theOutputPath + mdlRelativePathStartingAtModels
		// Examples of $modelname and output target:
		//     C:\model.mdl                         [Every Source model compiler I have tested stops compile with error for absolute path as $modelname.]
		//     C:\test\model.mdl                    [Every Source model compiler I have tested stops compile with error for absolute path as $modelname.]
		//     C:\test\models\model.mdl             [Every Source model compiler I have tested stops compile with error for absolute path as $modelname.]
		//     C:\test\models\subfolder\model.mdl   [Every Source model compiler I have tested stops compile with error for absolute path as $modelname.]
		//     model.mdl                            => <output folder>\models\model.mdl             [no "models" so assume relative to it, like with Source]
		//     test\model.mdl                       => <output folder>\models\test\model.mdl        [no "models" so assume relative to it, like with Source]
		//     test\models\model.mdl                => <output folder>\models\model.mdl             [has "models" so ignore path before it]
		//     test\models\subfolder\model.mdl      => <output folder>\models\subfolder\model.mdl   [has "models" so ignore path before it]
		private void ProcessCompiledModel(string compiledMdlPathFileName, string qcModelName)
		{
			List<string> createdFolders = new List<string>();
			string modelsSubpath = null;
			string targetPath = null;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			string sourcePath = FileManager.GetPath(compiledMdlPathFileName);
			string sourceFileNameWithoutExtension = Path.GetFileNameWithoutExtension(compiledMdlPathFileName);

			string outputPathModelsFolder;
			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder)
				outputPathModelsFolder = theOutputPath;
			else
				outputPathModelsFolder = Path.Combine(theOutputPath, "models");

			modelsSubpath = GetModelsSubpath(FileManager.GetPath(qcModelName), gameSetup.GameEngine);
			targetPath = Path.Combine(outputPathModelsFolder, modelsSubpath);
			FileManager.CreatePath(targetPath);

			string searchPattern = null;
			List<string> listOfCompiledExtensions = null;
			if (gameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				searchPattern = sourceFileNameWithoutExtension + "*.mdl";
				listOfCompiledExtensions = new List<string>(new string[] { ".mdl" });
			}
			else
			{
				searchPattern = sourceFileNameWithoutExtension + ".*";
				listOfCompiledExtensions = new List<string>(new string[] { ".ani", ".mdl", ".phy", ".vtx", ".vvd" });
			}
			foreach (string sourcePathFileName in Directory.EnumerateFiles(sourcePath, searchPattern))
			{
				if (!listOfCompiledExtensions.Contains(Path.GetExtension(sourcePathFileName).ToLower()))
				{
					continue;
				}

				string targetPathFileName = Path.Combine(targetPath, Path.GetFileName(sourcePathFileName));
				if (string.Compare(sourcePathFileName, targetPathFileName, true) != 0)
				{
					//If TheApp.Settings.CompileOutputFolderOption <> CompileOutputPathOptions.GameModelsFolder OrElse gameSetup.GameEngine = GameEngine.GoldSource Then
					try
					{
						if (File.Exists(targetPathFileName))
						{
							File.Delete(targetPathFileName);
						}
					}
					catch (Exception)
					{
						int debug = 4242;
					}
					try
					{
						File.Move(sourcePathFileName, targetPathFileName);
						UpdateProgress(2, "CROWBAR: Moved compiled model file \"" + sourcePathFileName + "\" to \"" + targetPath + "\"");
					}
					catch (Exception ex)
					{
						UpdateProgress();
						UpdateProgress(2, "WARNING: Crowbar tried to move the file, \"" + sourcePathFileName + "\", to the output folder, but Windows complained with this message: " + ex.Message.Trim());
						UpdateProgress(2, "SOLUTION: Compile the model again (and hope Windows does not complain again) or move the file yourself.");
						UpdateProgress();
					}
					//End If
				}

				//NOTE: Make list of main MDL files compiled.
				if (string.Compare(Path.GetFileName(targetPathFileName), Path.GetFileName(compiledMdlPathFileName), true) == 0)
					theCompiledMdlFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, targetPathFileName));
			}
		}

		// GoldSource:
		//     "C:\"                   => ""            [absolute path is same as if the path were relative]
		//     ""                      => ""            [no "models" so assume relative to it, like with Source]
		//     "test"                  => "test"        [no "models" so assume relative to it, like with Source]
		//     "test\models"           => ""            [has "models" so ignore path before it]
		//     "test\models\subfolder" => "subfolder"   [has "models" so ignore path before it]
		// Source:
		//     "C:\"                   => ""            [absolute path is same as GoldSource method]
		//     "test"                  => "test"        [relative path is always "models" subfolder]
		private string GetModelsSubpath(string iPath, AppEnums.GameEngine iGameEngine)
		{
			if (string.IsNullOrEmpty(iPath))
				return string.Empty;

			string modelsSubpath = string.Empty;
			string fullPath = Path.GetFullPath(iPath);
			if (iGameEngine == AppEnums.GameEngine.GoldSource || iPath == fullPath)
			{
				string tempSubpath = iPath;
				while (!string.IsNullOrEmpty(tempSubpath))
				{
					string lastFolderInPath = Path.GetFileName(tempSubpath);
					if (lastFolderInPath == "models")
						break;
					else if (string.IsNullOrEmpty(lastFolderInPath))
					{
						modelsSubpath = string.Empty;
						break;
					}
					else
						modelsSubpath = Path.Combine(lastFolderInPath, modelsSubpath);

					tempSubpath = FileManager.GetPath(tempSubpath);
				}
			}
			else
				modelsSubpath = iPath;

			return modelsSubpath;
		}

		private void myProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
		{
			Process myProcess = (Process)sender;
			try
			{
				string line = e.Data;
				if (line != null)
				{
					theProcessHasOutputData = true;
					UpdateProgress(3, line);

					if (theDefineBonesFileStream != null)
					{
						line = line.Trim();
						if (line.StartsWith("$definebone"))
							theDefineBonesFileStream.WriteLine(line);
					}

					if (line.StartsWith("Hit a key"))
						StopCompile(false, myProcess);
					//TEST: 
					//Else
					//	Dim i As Integer = 42

					//NOTE: This works for handling CSGO's studiomdl when an MDL file exists where the new one is being compiled, but the new one has fewer sequences.
					//      Not sure why the line "Please confirm sequence deletion: [y/n]" does not show until after Crowbar writes the "y".
					if (line.StartsWith("WARNING: This model has fewer sequences than its predecessor."))
						myProcess.StandardInput.Write("y");
				}
			}
			catch (Exception)
			{
				int debug = 4242;
			}
			finally
			{
				if (CancellationPending)
					StopCompile(true, myProcess);
				else if (theSkipCurrentModelIsActive)
					StopCompile(true, myProcess);
			}
		}

		private void myProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			Process myProcess = (Process)sender;
			try
			{
				string line = e.Data;
				if (line != null)
					UpdateProgress(3, line);
			}
			catch (Exception)
			{
				int debug = 4242;
			}
			finally
			{
				if (CancellationPending)
					StopCompile(true, myProcess);
				else if (theSkipCurrentModelIsActive)
					StopCompile(true, myProcess);
			}
		}

		private void StopCompile(bool processIsCanceled, Process myProcess)
		{
			if (myProcess != null && !myProcess.HasExited)
			{
				try
				{
					myProcess.CancelOutputRead();
					myProcess.CancelErrorRead();
					myProcess.Kill();
				}
				catch (Exception)
				{
					int debug = 4242;
				}
			}

			if (processIsCanceled)
				theLastLine = "...Compiling canceled.";
		}

		private AppEnums.StatusMessage CreateLogTextFile(string qcPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			if ((gameSetup.GameEngine == AppEnums.GameEngine.GoldSource && MainCROWBAR.TheApp.Settings.CompileGoldSourceLogFileIsChecked) || (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileSourceLogFileIsChecked))
			{
				try
				{
					string logPath = theInputQcPath;
					string logFileName = "compile-log.txt";
					if (!string.IsNullOrEmpty(qcPathFileName))
					{
						logPath = FileManager.GetPath(qcPathFileName);
						logFileName = Path.GetFileNameWithoutExtension(qcPathFileName) + " " + logFileName;
					}

					FileManager.CreatePath(logPath);
					string logPathFileName = Path.Combine(logPath, logFileName);
					theLogFileStream = File.CreateText(logPathFileName);
					theLogFileStream.AutoFlush = true;

					if (File.Exists(logPathFileName))
						theCompiledLogFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, logPathFileName));

					theLogFileStream.WriteLine("// " + MainCROWBAR.TheApp.GetHeaderComment());
					theLogFileStream.Flush();
				}
				catch (Exception ex)
				{
					UpdateProgress();
					UpdateProgress(2, "ERROR: Crowbar tried to write the compile log file but the system gave this message: " + ex.Message);
					status = AppEnums.StatusMessage.Error;
				}
			}
			else
				theLogFileStream = null;

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
			UpdateProgressInternal(1, string.Empty);
		}

		private void WriteErrorMessage(int indentLevel, string line)
		{
			UpdateProgress(indentLevel, "Crowbar ERROR: " + line);
		}

		private void UpdateProgress(int indentLevel, string line)
		{
			string indentedLine = string.Empty;
			for (int i = 1; i <= indentLevel; i++)
				indentedLine += "  ";

			indentedLine += line;
			UpdateProgressInternal(1, indentedLine);
		}

		private string GetDefineBonesPathFileName()
		{
			string fileName = MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesQciFileName;
			string qcPath = FileManager.GetPath(fileName);
			if (string.IsNullOrEmpty(Path.GetExtension(MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesQciFileName)))
				fileName += ".qci";

			return Path.Combine(qcPath, fileName);
		}

		private void OpenDefineBonesFile()
		{
			try
			{
				theDefineBonesFileStream = File.CreateText(GetDefineBonesPathFileName());
			}
			catch (Exception)
			{
				theDefineBonesFileStream = null;
			}
		}

		private void CloseDefineBonesFile()
		{
			theDefineBonesFileStream.Flush();
			theDefineBonesFileStream.Close();
			theDefineBonesFileStream = null;
		}

		private string InsertAnIncludeDefineBonesFileCommandIntoQcFile(string qciPathFileName)
		{
			SourceQcFile qcFile = new SourceQcFile();
			return qcFile.InsertAnIncludeFileCommand(MainCROWBAR.TheApp.Settings.CompileQcPathFileName, qciPathFileName);
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
	}
}