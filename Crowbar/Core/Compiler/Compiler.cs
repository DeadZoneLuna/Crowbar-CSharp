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
	public class Compiler : BackgroundWorker
	{
#region Create and Destroy

		public Compiler() : base()
		{

			this.theCompiledLogFiles = new BindingListEx<string>();
			this.theCompiledMdlFiles = new BindingListEx<string>();

			this.WorkerReportsProgress = true;
			this.WorkerSupportsCancellation = true;
			this.DoWork += this.Compiler_DoWork;
		}

#endregion

#region Properties

#endregion

#region Methods

		public void Run()
		{
			this.RunWorkerAsync();
		}

		public void SkipCurrentModel()
		{
			//NOTE: This might have thread race condition, but it probably doesn't matter.
			this.theSkipCurrentModelIsActive = true;
		}

		public string GetOutputPathFileName(string relativePathFileName)
		{
			string pathFileName = Path.Combine(this.theOutputPath, relativePathFileName);

			pathFileName = Path.GetFullPath(pathFileName);

			return pathFileName;
		}

#endregion

#region Private Methods

#endregion

#region Private Methods in Background Thread

		private void Compiler_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			this.ReportProgress(0, "");

			this.theOutputPath = this.GetOutputPath();

			AppEnums.StatusMessage status = 0;
			if (this.CompilerInputsAreValid())
			{
				status = this.Compile();
			}
			else
			{
				status = AppEnums.StatusMessage.Error;
			}
			e.Result = this.GetCompilerOutputs(status);

			if (this.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private string GetGameCompilerPathFileName()
		{
			string gameCompilerPathFileName = null;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			gameCompilerPathFileName = gameSetup.CompilerPathFileName;

			return gameCompilerPathFileName;
		}

		private string GetGamePath()
		{
			string gamePath = null;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName);

			return gamePath;
		}

		private string GetGameModelsPath()
		{
			string gameModelsPath = Path.Combine(this.GetGamePath(), "models");


			return gameModelsPath;
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
			string outputPath = null;

			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption != AppEnums.CompileOutputPathOptions.GameModelsFolder)
			{
				if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.Subfolder)
				{
					if (File.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
					{
						outputPath = Path.Combine(FileManager.GetPath(MainCROWBAR.TheApp.Settings.CompileQcPathFileName), MainCROWBAR.TheApp.Settings.CompileOutputSubfolderName);
					}
					else if (Directory.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
					{
						outputPath = Path.Combine(MainCROWBAR.TheApp.Settings.CompileQcPathFileName, MainCROWBAR.TheApp.Settings.CompileOutputSubfolderName);
					}
					else
					{
						outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					}
				}
				else
				{
					outputPath = MainCROWBAR.TheApp.Settings.CompileOutputFullPath;
				}
			}
			else
			{
				outputPath = this.GetGameModelsPath();
			}

			//This will change a relative path to an absolute path.
			outputPath = Path.GetFullPath(outputPath);
			return outputPath;
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

			string gameCompilerPathFileName = this.GetGameCompilerPathFileName();
			GameSetup gameSetup = null;
			string gamePathFileName = null;
			gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			gamePathFileName = gameSetup.GamePathFileName;

			if (!File.Exists(gameCompilerPathFileName))
			{
				inputsAreValid = false;
				this.WriteErrorMessage(1, "The model compiler, \"" + gameCompilerPathFileName + "\", does not exist.");
				this.UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
			}
			//TODO: [CompilerInputsAreValid] If GoldSource, then only check for liblist.gam if output is for game's "models" folder.
			//TODO: [CompilerInputsAreValid] Change error message to include "liblist.gam" or "gameinfo.txt" as appropriate.
			if (!File.Exists(gamePathFileName))
			{
				inputsAreValid = false;
				this.WriteErrorMessage(1, "The game's \"" + gamePathFileName + "\" file does not exist.");
				this.UpdateProgress(1, Properties.Resources.ErrorMessageSDKMissingCause);
			}
			if (string.IsNullOrEmpty(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
			{
				inputsAreValid = false;
				this.WriteErrorMessage(1, "QC file or folder has not been selected.");
			}
			else if (MainCROWBAR.TheApp.Settings.CompileMode == AppEnums.InputOptions.File && !File.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
			{
				inputsAreValid = false;
				this.WriteErrorMessage(1, "The QC file, \"" + MainCROWBAR.TheApp.Settings.CompileQcPathFileName + "\", does not exist.");
			}
			if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked)
			{
				if (MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesCreateFileIsChecked)
				{
					string defineBonesPathFileName = this.GetDefineBonesPathFileName();
					if (File.Exists(defineBonesPathFileName) && !MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesOverwriteQciFileIsChecked)
					{
						inputsAreValid = false;
						this.WriteErrorMessage(1, "The DefineBones file, \"" + defineBonesPathFileName + "\", already exists.");
					}
				}
			}
			//If TheApp.Settings.CompileOutputFolderIsChecked Then
			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption != AppEnums.CompileOutputPathOptions.GameModelsFolder)
			{
				if (!FileManager.PathExistsAfterTryToCreate(this.theOutputPath))
				{
					inputsAreValid = false;
					this.WriteErrorMessage(1, "The Output Folder, \"" + this.theOutputPath + "\" could not be created.");
				}
			}

			return inputsAreValid;
		}

		private CompilerOutputInfo GetCompilerOutputs(AppEnums.StatusMessage status)
		{
			CompilerOutputInfo compileResultInfo = new CompilerOutputInfo();

			compileResultInfo.theStatus = status;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			if (this.theCompiledMdlFiles.Count > 0)
			{
				compileResultInfo.theCompiledRelativePathFileNames = this.theCompiledMdlFiles;
			}
			else if (gameSetup.GameEngine == AppEnums.GameEngine.GoldSource && MainCROWBAR.TheApp.Settings.CompileGoldSourceLogFileIsChecked)
			{
				compileResultInfo.theCompiledRelativePathFileNames = this.theCompiledLogFiles;
			}
			else if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileSourceLogFileIsChecked)
			{
				compileResultInfo.theCompiledRelativePathFileNames = this.theCompiledLogFiles;
			}

			return compileResultInfo;
		}

		private AppEnums.StatusMessage Compile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			this.theSkipCurrentModelIsActive = false;

			this.theCompiledLogFiles.Clear();
			this.theCompiledMdlFiles.Clear();

			string qcPathFileName = MainCROWBAR.TheApp.Settings.CompileQcPathFileName;
			if (File.Exists(qcPathFileName))
			{
				this.theInputQcPath = FileManager.GetPath(qcPathFileName);
			}
			else if (Directory.Exists(qcPathFileName))
			{
				this.theInputQcPath = qcPathFileName;
			}

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			//Dim info As New CompilerInputInfo()
			//info.compilerPathFileName = gameSetup.CompilerPathFileName
			//info.compilerOptions = TheApp.Settings.CompileOptionsText
			//info.gamePathFileName = gameSetup.GamePathFileName
			//info.qcPathFileName = TheApp.Settings.CompileQcPathFileName
			//info.customModelFolder = TheApp.Settings.CompileOutputSubfolderName
			//info.theCompileMode = TheApp.Settings.CompileMode

			string defineBonesText = "";
			if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked)
			{
				defineBonesText = "Define Bones ";
			}

			string progressDescriptionText = "Compiling " + defineBonesText + "with " + MainCROWBAR.TheApp.GetProductNameAndVersion() + ": ";

			try
			{
				if (MainCROWBAR.TheApp.Settings.CompileMode == AppEnums.InputOptions.FolderRecursion)
				{
					progressDescriptionText += "\"" + this.theInputQcPath + "\" (folder + subfolders)";
					this.UpdateProgressStart(progressDescriptionText + " ...");

					status = this.CreateLogTextFile("");
					//If status = StatusMessage.Error Then
					//	Return status
					//End If

					this.CompileModelsInFolderRecursively(this.theInputQcPath);
				}
				else if (MainCROWBAR.TheApp.Settings.CompileMode == AppEnums.InputOptions.Folder)
				{
					progressDescriptionText += "\"" + this.theInputQcPath + "\" (folder)";
					this.UpdateProgressStart(progressDescriptionText + " ...");

					status = this.CreateLogTextFile("");
					//If status = StatusMessage.Error Then
					//	Return status
					//End If

					this.CompileModelsInFolder(this.theInputQcPath);
				}
				else
				{
					progressDescriptionText += "\"" + qcPathFileName + "\"";
					this.UpdateProgressStart(progressDescriptionText + " ...");
					status = this.CompileOneModel(qcPathFileName);
				}
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
			}
			finally
			{
				if (this.theLogFileStream != null)
				{
					this.theLogFileStream.Flush();
					this.theLogFileStream.Close();
					this.theLogFileStream = null;
				}
			}

			this.UpdateProgressStop("... " + progressDescriptionText + " finished.");

			return status;
		}

		private void CompileModelsInFolderRecursively(string modelsPathName)
		{
			this.CompileModelsInFolder(modelsPathName);

			foreach (string aPathName in Directory.GetDirectories(modelsPathName))
			{
				this.CompileModelsInFolderRecursively(aPathName);
				if (this.CancellationPending)
				{
					return;
				}
			}
		}

		private void CompileModelsInFolder(string modelsPathName)
		{
			foreach (string aPathFileName in Directory.GetFiles(modelsPathName, "*.qc"))
			{
				this.CompileOneModel(aPathFileName);

				//TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
				this.ReportProgress(5, "");

				if (this.CancellationPending)
				{
					return;
				}
				else if (this.theSkipCurrentModelIsActive)
				{
					this.theSkipCurrentModelIsActive = false;
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
				string qcPath = null;
				string qcFileName = null;
				string qcRelativePath = null;
				string qcRelativePathFileName = null;
				qcPath = FileManager.GetPath(qcPathFileName);
				qcFileName = Path.GetFileName(qcPathFileName);
				qcRelativePath = FileManager.GetRelativePathFileName(this.theInputQcPath, FileManager.GetPath(qcPathFileName));
				qcRelativePathFileName = Path.Combine(qcRelativePath, qcFileName);

				//Dim gameSetup As GameSetup
				//Dim gamePath As String
				//Dim gameModelsPath As String
				//gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
				//gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
				//gameModelsPath = Path.Combine(gamePath, "models")
				string gameModelsPath = this.GetGameModelsPath();

				GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

				SourceQcFile qcFile = null;
				string qcModelName = null;
				//Dim qcModelTopFolderPath As String
				string qcModelLongestExtantPath = null;
				string qcModelTopNonextantPath = "";
				string compiledMdlPathFileName = null;
				string compiledMdlPath = null;
				qcFile = new SourceQcFile();
				qcModelName = qcFile.GetQcModelName(qcPathFileName);
				try
				{
					compiledMdlPathFileName = Path.GetFullPath(qcModelName);
					//qcModelTopFolderPath = FileManager.GetTopFolderPath(qcModelName)
					if (compiledMdlPathFileName != qcModelName)
					{
						if (gameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
						{
							//	- The compiler does not create folders, so Crowbar needs to create the relative or absolute path found in $modelname, 
							//		starting in the "current folder" [SetCurrentDirectory()].
							//		* For example, with $modelname "C:\valve/models/barney.mdl", need to create "C:\valve\models" path.
							//		* For example, with $modelname "valve/models/barney.mdl", need to create "[current folder]\valve\models" path.
							compiledMdlPathFileName = Path.Combine(qcPath, qcModelName);
							//If qcModelTopFolderPath <> "" Then
							//	qcModelTopFolderPath = Path.Combine(qcPath, qcModelTopFolderPath)
							//End If
						}
						else
						{
							compiledMdlPathFileName = Path.Combine(gameModelsPath, qcModelName);
							//If qcModelTopFolderPath <> "" Then
							//	qcModelTopFolderPath = Path.Combine(gameModelsPath, qcModelTopFolderPath)
							//End If
						}
						compiledMdlPathFileName = Path.GetFullPath(compiledMdlPathFileName);
						//If qcModelTopFolderPath <> "" Then
						//	qcModelTopFolderPath = Path.GetFullPath(qcModelTopFolderPath)
						//End If
					}

					if (Path.GetExtension(compiledMdlPathFileName) != ".mdl")
					{
						compiledMdlPathFileName = Path.ChangeExtension(compiledMdlPathFileName, ".mdl");
					}
				}
				catch (Exception ex)
				{
					compiledMdlPathFileName = "";
				}
				compiledMdlPath = FileManager.GetPath(compiledMdlPathFileName);
				qcModelLongestExtantPath = FileManager.GetLongestExtantPath(compiledMdlPath, ref qcModelTopNonextantPath);
				if (!string.IsNullOrEmpty(qcModelTopNonextantPath))
				{
					FileManager.CreatePath(compiledMdlPath);
				}

				//Me.theModelOutputPath = Path.Combine(Me.theOutputPath, qcRelativePathName)
				//Me.theModelOutputPath = Path.GetFullPath(Me.theModelOutputPath)
				//If TheApp.Settings.CompileFolderForEachModelIsChecked Then
				//    Dim modelName As String
				//    modelName = Path.GetFileNameWithoutExtension(modelRelativePathFileName)
				//    Me.theModelOutputPath = Path.Combine(Me.theModelOutputPath, modelName)
				//End If
				//Me.UpdateProgressWithModelOutputPath(Me.theModelOutputPath)

				//FileManager.CreatePath(Me.theModelOutputPath)

				//Me.CreateLogTextFile(qcPathFileName)
				if (MainCROWBAR.TheApp.Settings.CompileMode == AppEnums.InputOptions.File)
				{
					status = this.CreateLogTextFile(qcPathFileName);
					//If status = StatusMessage.Error Then
					//	Return status
					//End If
				}

				string defineBonesText = "";
				if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked)
				{
					defineBonesText = "Define Bones of ";
				}

				this.UpdateProgress();
				this.UpdateProgress(1, "Compiling " + defineBonesText + "\"" + qcRelativePathFileName + "\" ...");

				string result = this.CheckFiles();
				if (result == "success")
				{
					if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesCreateFileIsChecked)
					{
						this.OpenDefineBonesFile();
					}

					this.UpdateProgress(2, "Output from compiler \"" + this.GetGameCompilerPathFileName() + "\": ");
					this.RunStudioMdlApp(qcPath, qcFileName);

					if (!this.theProcessHasOutputData)
					{
						this.UpdateProgress(2, "ERROR: The compiler did not return any status messages.");
						this.UpdateProgress(2, "CAUSE: The compiler is not the correct one for the selected game.");
						this.UpdateProgress(2, "SOLUTION: Verify integrity of game files via Steam so that the correct compiler is installed.");
					}
					else if (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked)
					{
						if (this.theDefineBonesFileStream != null)
						{
							string qciPathFileName = ((FileStream)this.theDefineBonesFileStream.BaseStream).Name;

							this.CloseDefineBonesFile();

							//NOTE: Must do this after closing define bones file.
							if (File.Exists(qciPathFileName))
							{
								FileInfo qciFileInfo = new FileInfo(qciPathFileName);
								if (qciFileInfo.Length == 0)
								{
									this.UpdateProgress(2, "CROWBAR WARNING: No define bones were written to QCI file.");

									try
									{
										File.Delete(qciPathFileName);
									}
									catch (Exception ex)
									{
										this.UpdateProgress(2, "CROWBAR WARNING: Failed to delete empty QCI file: \"" + qciPathFileName + "\"");
									}
								}
								else
								{
									this.UpdateProgress(2, "CROWBAR: Wrote define bones into QCI file: \"" + qciPathFileName + "\"");

									if (MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesModifyQcFileIsChecked)
									{
										string line = this.InsertAnIncludeDefineBonesFileCommandIntoQcFile(qciPathFileName);
										this.UpdateProgress(2, "CROWBAR: Wrote in the QC file this line: " + line);
									}
								}
							}
							else
							{
								this.UpdateProgress(2, "CROWBAR WARNING: Failed to write QCI file: \"" + qciPathFileName + "\"");
							}
						}
					}
					else
					{
						if (File.Exists(compiledMdlPathFileName))
						{
							this.ProcessCompiledModel(compiledMdlPathFileName, qcModelName);
						}
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
						{
							this.UpdateProgress(2, "CROWBAR: Deleted empty temporary compile folder \"" + fullPathDeleted + "\".");
						}
					}
				}

				this.UpdateProgress(1, "... Compiling " + defineBonesText + "\"" + qcRelativePathFileName + "\" finished. Check above for any errors.");
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
			string result = "success";

			//TODO: Implement counting of all materials used in all mesh SMD files, excluding the phy mesh.

			return result;
		}

		private void RunStudioMdlApp(string qcPath, string qcFileName)
		{
			string currentFolder = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(qcPath);

			string gameCompilerPathFileName = this.GetGameCompilerPathFileName();

			string arguments = "";
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			if (gameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				arguments += "-game";
				arguments += " ";
				arguments += "\"";
				arguments += this.GetGamePath();
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
			myProcess.OutputDataReceived += this.myProcess_OutputDataReceived;
			myProcess.ErrorDataReceived += this.myProcess_ErrorDataReceived;
			myProcess.Start();
			//Directory.SetCurrentDirectory(currentFolder)
			myProcess.StandardInput.AutoFlush = true;
			myProcess.BeginOutputReadLine();
			myProcess.BeginErrorReadLine();
			this.theProcessHasOutputData = false;

			//myProcess.StandardOutput.ReadToEnd()
			//'NOTE: Do this to handle "hit a key to continue" at the end of Dota 2's compiler.
			//myProcess.StandardInput.Write(" ")
			//myProcess.StandardInput.Close()

			myProcess.WaitForExit();

			myProcess.Close();
			myProcess.OutputDataReceived -= this.myProcess_OutputDataReceived;
			myProcess.ErrorDataReceived -= this.myProcess_ErrorDataReceived;

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
			string sourcePath = null;
			string sourceFileNameWithoutExtension = null;
			string targetPathFileName = null;
			List<string> createdFolders = new List<string>();
			string outputPathModelsFolder = null;
			string modelsSubpath = null;
			string targetPath = null;

			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			sourcePath = FileManager.GetPath(compiledMdlPathFileName);
			sourceFileNameWithoutExtension = Path.GetFileNameWithoutExtension(compiledMdlPathFileName);

			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder)
			{
				outputPathModelsFolder = this.theOutputPath;
			}
			else
			{
				outputPathModelsFolder = Path.Combine(this.theOutputPath, "models");
			}
			modelsSubpath = this.GetModelsSubpath(FileManager.GetPath(qcModelName), gameSetup.GameEngine);
			targetPath = Path.Combine(outputPathModelsFolder, modelsSubpath);
			FileManager.CreatePath(targetPath);

			string searchPattern = null;
			List<string> listOfCompiledExtensions = null;
			if (gameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				searchPattern = sourceFileNameWithoutExtension + "*.mdl";
				listOfCompiledExtensions = new List<string>(new string[] {".mdl"});
			}
			else
			{
				searchPattern = sourceFileNameWithoutExtension + ".*";
				listOfCompiledExtensions = new List<string>(new string[] {".ani", ".mdl", ".phy", ".vtx", ".vvd"});
			}
			foreach (string sourcePathFileName in Directory.EnumerateFiles(sourcePath, searchPattern))
			{
				if (!listOfCompiledExtensions.Contains(Path.GetExtension(sourcePathFileName).ToLower()))
				{
					continue;
				}

				targetPathFileName = Path.Combine(targetPath, Path.GetFileName(sourcePathFileName));

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
					catch (Exception ex)
					{
						int debug = 4242;
					}
					try
					{
						File.Move(sourcePathFileName, targetPathFileName);
						this.UpdateProgress(2, "CROWBAR: Moved compiled model file \"" + sourcePathFileName + "\" to \"" + targetPath + "\"");
					}
					catch (Exception ex)
					{
						this.UpdateProgress();
						this.UpdateProgress(2, "WARNING: Crowbar tried to move the file, \"" + sourcePathFileName + "\", to the output folder, but Windows complained with this message: " + ex.Message.Trim());
						this.UpdateProgress(2, "SOLUTION: Compile the model again (and hope Windows does not complain again) or move the file yourself.");
						this.UpdateProgress();
					}
					//End If
				}

				//NOTE: Make list of main MDL files compiled.
				if (string.Compare(Path.GetFileName(targetPathFileName), Path.GetFileName(compiledMdlPathFileName), true) == 0)
				{
					this.theCompiledMdlFiles.Add(FileManager.GetRelativePathFileName(this.theOutputPath, targetPathFileName));
				}
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
			string modelsSubpath = "";
			string tempSubpath = null;
			string lastFolderInPath = null;

			if (string.IsNullOrEmpty(iPath))
			{
				return "";
			}

			string fullPath = Path.GetFullPath(iPath);

			if (iGameEngine == AppEnums.GameEngine.GoldSource || iPath == fullPath)
			{
				tempSubpath = iPath;
				while (!string.IsNullOrEmpty(tempSubpath))
				{
					lastFolderInPath = Path.GetFileName(tempSubpath);
					if (lastFolderInPath == "models")
					{
						break;
					}
					else if (string.IsNullOrEmpty(lastFolderInPath))
					{
						modelsSubpath = "";
						break;
					}
					else
					{
						modelsSubpath = Path.Combine(lastFolderInPath, modelsSubpath);
					}
					tempSubpath = FileManager.GetPath(tempSubpath);
				}
			}
			else
			{
				modelsSubpath = iPath;
			}

			return modelsSubpath;
		}

		private void myProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
		{
			Process myProcess = (Process)sender;
			string line = null;

			try
			{
				line = e.Data;
				if (line != null)
				{
					this.theProcessHasOutputData = true;
					this.UpdateProgress(3, line);

					if (this.theDefineBonesFileStream != null)
					{
						line = line.Trim();
						if (line.StartsWith("$definebone"))
						{
							this.theDefineBonesFileStream.WriteLine(line);
						}
					}

					if (line.StartsWith("Hit a key"))
					{
						this.StopCompile(false, myProcess);
					}
					//TEST: 
					//Else
					//	Dim i As Integer = 42

					//NOTE: This works for handling CSGO's studiomdl when an MDL file exists where the new one is being compiled, but the new one has fewer sequences.
					//      Not sure why the line "Please confirm sequence deletion: [y/n]" does not show until after Crowbar writes the "y".
					if (line.StartsWith("WARNING: This model has fewer sequences than its predecessor."))
					{
						myProcess.StandardInput.Write("y");
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (this.CancellationPending)
				{
					this.StopCompile(true, myProcess);
				}
				else if (this.theSkipCurrentModelIsActive)
				{
					this.StopCompile(true, myProcess);
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
					this.UpdateProgress(3, line);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (this.CancellationPending)
				{
					this.StopCompile(true, myProcess);
				}
				else if (this.theSkipCurrentModelIsActive)
				{
					this.StopCompile(true, myProcess);
				}
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
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}

			if (processIsCanceled)
			{
				this.theLastLine = "...Compiling canceled.";
			}
		}

		private AppEnums.StatusMessage CreateLogTextFile(string qcPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			if ((gameSetup.GameEngine == AppEnums.GameEngine.GoldSource && MainCROWBAR.TheApp.Settings.CompileGoldSourceLogFileIsChecked) || (gameSetup.GameEngine == AppEnums.GameEngine.Source && MainCROWBAR.TheApp.Settings.CompileSourceLogFileIsChecked))
			{
				string qcFileName = null;
				string logPath = null;
				string logFileName = null;
				string logPathFileName = null;

				try
				{
					if (!string.IsNullOrEmpty(qcPathFileName))
					{
						logPath = FileManager.GetPath(qcPathFileName);
						qcFileName = Path.GetFileNameWithoutExtension(qcPathFileName);
						logFileName = qcFileName + " compile-log.txt";
					}
					else
					{
						logPath = this.theInputQcPath;
						logFileName = "compile-log.txt";
					}
					FileManager.CreatePath(logPath);
					logPathFileName = Path.Combine(logPath, logFileName);

					this.theLogFileStream = File.CreateText(logPathFileName);
					this.theLogFileStream.AutoFlush = true;

					if (File.Exists(logPathFileName))
					{
						this.theCompiledLogFiles.Add(FileManager.GetRelativePathFileName(this.theOutputPath, logPathFileName));
					}

					this.theLogFileStream.WriteLine("// " + MainCROWBAR.TheApp.GetHeaderComment());
					this.theLogFileStream.Flush();
				}
				catch (Exception ex)
				{
					this.UpdateProgress();
					this.UpdateProgress(2, "ERROR: Crowbar tried to write the compile log file but the system gave this message: " + ex.Message);
					status = AppEnums.StatusMessage.Error;
				}
			}
			else
			{
				this.theLogFileStream = null;
			}

			return status;
		}

		private void UpdateProgressStart(string line)
		{
			this.UpdateProgressInternal(0, line);
		}

		private void UpdateProgressStop(string line)
		{
			this.UpdateProgressInternal(100, "\r" + line);
		}

		private void UpdateProgress()
		{
			this.UpdateProgressInternal(1, "");
		}

		private void WriteErrorMessage(int indentLevel, string line)
		{
			this.UpdateProgress(indentLevel, "Crowbar ERROR: " + line);
		}

		private void UpdateProgress(int indentLevel, string line)
		{
			string indentedLine = "";

			for (int i = 1; i <= indentLevel; i++)
			{
				indentedLine += "  ";
			}
			indentedLine += line;
			this.UpdateProgressInternal(1, indentedLine);
		}

		private string GetDefineBonesPathFileName()
		{
			string fileName = null;
			if (string.IsNullOrEmpty(Path.GetExtension(MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesQciFileName)))
			{
				fileName = MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesQciFileName + ".qci";
			}
			else
			{
				fileName = MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesQciFileName;
			}
			string qcPath = FileManager.GetPath(MainCROWBAR.TheApp.Settings.CompileQcPathFileName);
			string pathFileName = Path.Combine(qcPath, fileName);

			return pathFileName;
		}

		private void OpenDefineBonesFile()
		{
			try
			{
				this.theDefineBonesFileStream = File.CreateText(this.GetDefineBonesPathFileName());
			}
			catch (Exception ex)
			{
				this.theDefineBonesFileStream = null;
			}
		}

		private void CloseDefineBonesFile()
		{
			this.theDefineBonesFileStream.Flush();
			this.theDefineBonesFileStream.Close();
			this.theDefineBonesFileStream = null;
		}

		private string InsertAnIncludeDefineBonesFileCommandIntoQcFile(string qciPathFileName)
		{
			SourceQcFile qcFile = new SourceQcFile();
			return qcFile.InsertAnIncludeFileCommand(MainCROWBAR.TheApp.Settings.CompileQcPathFileName, qciPathFileName);
		}

		private void UpdateProgressInternal(int progressValue, string line)
		{
			if (progressValue == 1 && this.theLogFileStream != null)
			{
				this.theLogFileStream.WriteLine(line);
				this.theLogFileStream.Flush();
			}

			this.ReportProgress(progressValue, line);
		}

#endregion

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

	}

}