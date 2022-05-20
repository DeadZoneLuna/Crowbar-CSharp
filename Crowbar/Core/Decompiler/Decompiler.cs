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
	public class Decompiler : BackgroundWorker
	{
#region Create and Destroy

		public Decompiler() : base()
		{

			this.theDecompiledQcFiles = new BindingListEx<string>();
			this.theDecompiledFirstRefSmdFiles = new BindingListEx<string>();
			this.theDecompiledFirstLodSmdFiles = new BindingListEx<string>();
			this.theDecompiledPhysicsFiles = new BindingListEx<string>();
			this.theDecompiledVtaFiles = new BindingListEx<string>();
			this.theDecompiledFirstBoneAnimSmdFiles = new BindingListEx<string>();
			this.theDecompiledVrdFiles = new BindingListEx<string>();
			this.theDecompiledDeclareSequenceQciFiles = new BindingListEx<string>();
			this.theDecompiledFirstTextureBmpFiles = new BindingListEx<string>();
			this.theDecompiledLogFiles = new BindingListEx<string>();
			this.theDecompiledFirstDebugFiles = new BindingListEx<string>();

			this.WorkerReportsProgress = true;
			this.WorkerSupportsCancellation = true;
			this.DoWork += this.Decompiler_DoWork;
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

		public string GetOutputPathFolderOrFileName()
		{
			return this.theOutputPathOrModelOutputFileName;
		}

#endregion

#region Private Methods

#endregion

#region Private Methods in Background Thread

		private void Decompiler_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			this.ReportProgress(0, "");

			this.theOutputPath = this.GetOutputPath();

			AppEnums.StatusMessage status = 0;
			if (this.DecompilerInputsAreValid())
			{
				status = this.Decompile();
			}
			else
			{
				status = AppEnums.StatusMessage.Error;
			}
			e.Result = this.GetDecompilerOutputs(status);

			if (this.CancellationPending)
			{
				e.Cancel = true;
			}
		}

		private string GetOutputPath()
		{
			string outputPath = null;

			if (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.Subfolder)
			{
				if (File.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
				{
					outputPath = Path.Combine(FileManager.GetPath(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName), MainCROWBAR.TheApp.Settings.DecompileOutputSubfolderName);
				}
				else if (Directory.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
				{
					outputPath = Path.GetFullPath(Path.Combine(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName, MainCROWBAR.TheApp.Settings.DecompileOutputSubfolderName));
				}
				else
				{
					outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
			else
			{
				outputPath = MainCROWBAR.TheApp.Settings.DecompileOutputFullPath;
			}

			return outputPath;
		}

		private bool DecompilerInputsAreValid()
		{
			bool inputsAreValid = false;

			if (string.IsNullOrEmpty(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
			{
				inputsAreValid = false;
			}
			else
			{
				inputsAreValid = FileManager.PathExistsAfterTryToCreate(this.theOutputPath);
			}

			return inputsAreValid;
		}

		private DecompilerOutputInfo GetDecompilerOutputs(AppEnums.StatusMessage status)
		{
			DecompilerOutputInfo decompileResultInfo = new DecompilerOutputInfo();

			decompileResultInfo.theStatus = status;

			if (MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledQcFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledFirstRefSmdFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompileLodMeshSmdFilesIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledFirstLodSmdFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledPhysicsFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledVtaFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledFirstBoneAnimSmdFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledVrdFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompileDeclareSequenceQciFileIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledDeclareSequenceQciFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompileTextureBmpFilesIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledFirstTextureBmpFiles;
			}
			else if (MainCROWBAR.TheApp.Settings.DecompileLogFileIsChecked)
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledLogFiles;
			}
			else
			{
				decompileResultInfo.theDecompiledRelativePathFileNames = this.theDecompiledFirstDebugFiles;
			}

			if (decompileResultInfo.theDecompiledRelativePathFileNames.Count <= 0 || this.theDecompiledQcFiles.Count <= 0)
			{
				this.theOutputPathOrModelOutputFileName = "";
				//ElseIf decompileResultInfo.theDecompiledRelativePathFileNames.Count = 1 Then
				//	Me.theOutputPathOrModelOutputFileName = decompileResultInfo.theDecompiledRelativePathFileNames(0)
			}
			else
			{
				this.theOutputPathOrModelOutputFileName = this.theOutputPath;
			}

			return decompileResultInfo;
		}

		private AppEnums.StatusMessage Decompile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			this.theSkipCurrentModelIsActive = false;

			this.theDecompiledQcFiles.Clear();
			this.theDecompiledFirstRefSmdFiles.Clear();
			this.theDecompiledFirstLodSmdFiles.Clear();
			this.theDecompiledPhysicsFiles.Clear();
			this.theDecompiledVtaFiles.Clear();
			this.theDecompiledFirstBoneAnimSmdFiles.Clear();
			this.theDecompiledVrdFiles.Clear();
			this.theDecompiledDeclareSequenceQciFiles.Clear();
			this.theDecompiledFirstTextureBmpFiles.Clear();
			this.theDecompiledLogFiles.Clear();
			this.theDecompiledFirstDebugFiles.Clear();

			string mdlPathFileName = MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName;
			if (File.Exists(mdlPathFileName))
			{
				this.theInputMdlPathName = FileManager.GetPath(mdlPathFileName);
			}
			else if (Directory.Exists(mdlPathFileName))
			{
				this.theInputMdlPathName = mdlPathFileName;
			}

			string progressDescriptionText = "Decompiling with " + MainCROWBAR.TheApp.GetProductNameAndVersion() + ": ";

			try
			{
				if (string.IsNullOrEmpty(this.theInputMdlPathName))
				{
					//Can get here if mdlPathFileName exists, but only with parts of the path using "Length8.3" names.
					//Somehow when drag-dropping such a pathFileName, even though Windows shows full names in the path, Crowbar shows it with "Length8.3" names.
					progressDescriptionText += "\"" + mdlPathFileName + "\"";
					this.UpdateProgressStart(progressDescriptionText + " ...");
					this.UpdateProgress();
					this.UpdateProgress(1, "ERROR: Failed because actual path is too long.");
					status = AppEnums.StatusMessage.Error;
				}
				else if (MainCROWBAR.TheApp.Settings.DecompileMode == AppEnums.InputOptions.FolderRecursion)
				{
					progressDescriptionText += "\"" + this.theInputMdlPathName + "\" (folder + subfolders)";
					this.UpdateProgressStart(progressDescriptionText + " ...");

					status = this.CreateLogTextFile("");
					//If status = StatusMessage.Error Then
					//	Return status
					//End If

					this.DecompileModelsInFolderRecursively(this.theInputMdlPathName);
				}
				else if (MainCROWBAR.TheApp.Settings.DecompileMode == AppEnums.InputOptions.Folder)
				{
					progressDescriptionText += "\"" + this.theInputMdlPathName + "\" (folder)";
					this.UpdateProgressStart(progressDescriptionText + " ...");

					status = this.CreateLogTextFile("");
					//If status = StatusMessage.Error Then
					//	Return status
					//End If

					this.DecompileModelsInFolder(this.theInputMdlPathName);
				}
				else
				{
					progressDescriptionText += "\"" + mdlPathFileName + "\"";
					this.UpdateProgressStart(progressDescriptionText + " ...");
					status = this.DecompileOneModel(mdlPathFileName);
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

		private AppEnums.StatusMessage DecompileModelsInFolderRecursively(string modelsPathName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = this.DecompileModelsInFolder(modelsPathName);
			if (this.CancellationPending)
			{
				status = AppEnums.StatusMessage.Canceled;
				return status;
			}

			foreach (string aPathName in Directory.GetDirectories(modelsPathName))
			{
				status = this.DecompileModelsInFolderRecursively(aPathName);
				if (this.CancellationPending)
				{
					status = AppEnums.StatusMessage.Canceled;
					return status;
				}
			}
//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return 0;
		}

		private AppEnums.StatusMessage DecompileModelsInFolder(string modelsPathName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			foreach (string aPathFileName in Directory.GetFiles(modelsPathName, "*.mdl"))
			{
				status = this.DecompileOneModel(aPathFileName);

				if (this.CancellationPending)
				{
					status = AppEnums.StatusMessage.Canceled;
					return status;
				}
				else if (this.theSkipCurrentModelIsActive)
				{
					this.theSkipCurrentModelIsActive = false;
					continue;
				}
			}
//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return 0;
		}

		private AppEnums.StatusMessage DecompileOneModel(string mdlPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				string mdlFileName = null;
				string mdlRelativePathName = null;
				string mdlRelativePathFileName = null;
				mdlFileName = Path.GetFileName(mdlPathFileName);
				mdlRelativePathName = FileManager.GetRelativePathFileName(this.theInputMdlPathName, FileManager.GetPath(mdlPathFileName));
				mdlRelativePathFileName = Path.Combine(mdlRelativePathName, mdlFileName);

				string modelName = Path.GetFileNameWithoutExtension(mdlPathFileName);

				this.theModelOutputPath = Path.Combine(this.theOutputPath, mdlRelativePathName);
				this.theModelOutputPath = Path.GetFullPath(this.theModelOutputPath);
				if (MainCROWBAR.TheApp.Settings.DecompileFolderForEachModelIsChecked)
				{
					this.theModelOutputPath = Path.Combine(this.theModelOutputPath, modelName);
				}

				FileManager.CreatePath(this.theModelOutputPath);

				//Try
				//	Me.CreateLogTextFile(mdlPathFileName)
				//Catch ex As Exception
				//	Me.UpdateProgress()
				//	Me.UpdateProgress(2, "ERROR: Crowbar tried to write the decompile log file but the system gave this message: " + ex.Message)
				//	status = StatusMessage.Error
				//	Return status
				//End Try
				if (MainCROWBAR.TheApp.Settings.DecompileMode == AppEnums.InputOptions.File)
				{
					status = this.CreateLogTextFile(mdlPathFileName);
					//If status = StatusMessage.Error Then
					//	Return status
					//End If
				}

				this.UpdateProgress();
				this.UpdateProgress(1, "Decompiling \"" + mdlRelativePathFileName + "\" ...");

				SourceModel model = null;
				int version = 0;
				try
				{
					model = SourceModel.Create(mdlPathFileName, MainCROWBAR.TheApp.Settings.DecompileOverrideMdlVersion, ref version);
					if (model != null)
					{
						if (MainCROWBAR.TheApp.Settings.DecompileOverrideMdlVersion == AppEnums.SupportedMdlVersion.DoNotOverride)
						{
							this.UpdateProgress(2, "Model version " + model.Version.ToString() + " detected.");
						}
						else
						{
							this.UpdateProgress(2, "Model version overridden to be " + model.Version.ToString() + ".");
						}
					}
					else
					{
						this.UpdateProgress(2, "ERROR: Model version " + version.ToString() + " not currently supported.");
						this.UpdateProgress(2, "       If the model works in-game or HLMV, try changing 'Override MDL version' option.");
						this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" FAILED.");
						status = AppEnums.StatusMessage.Error;
						return status;
					}
				}
				catch (FormatException ex)
				{
					this.UpdateProgress(2, ex.Message);
					this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" FAILED.");
					status = AppEnums.StatusMessage.Error;
					return status;
				}
				catch (Exception ex)
				{
					//Me.UpdateProgress(2, "ERROR: " + ex.Message)
					this.UpdateProgress(2, "Crowbar tried to read the MDL file but the system gave this message: " + ex.Message);
					this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" FAILED.");
					status = AppEnums.StatusMessage.Error;
					return status;
				}

				this.UpdateProgress(2, "Reading MDL file header ...");
				status = model.ReadMdlFileHeader();
				//If status = StatusMessage.ErrorInvalidMdlFileId Then
				//	Me.UpdateProgress(2, "ERROR: File does not have expected MDL header ID (first 4 bytes of file) of 'IDST' (without quotes). MDL file is not a GoldSource- or Source-engine MDL file.")
				//	Return status
				//ElseIf status = StatusMessage.ErrorInvalidInternalMdlFileSize Then
				//	Me.UpdateProgress(3, "WARNING: The internally recorded file size is different than the actual file size. Some data might not decompile correctly.")
				//ElseIf status = StatusMessage.ErrorRequiredMdlFileNotFound Then
				//	Me.UpdateProgress(2, "ERROR: MDL file not found.")
				//	Return status
				//End If
				if (status == AppEnums.StatusMessage.ErrorInvalidInternalMdlFileSize)
				{
					this.UpdateProgress(3, "WARNING: The internally recorded file size is different than the actual file size. Some data might not decompile correctly.");
				}
				this.UpdateProgress(2, "... Reading MDL file header finished.");

				this.UpdateProgress(2, "Checking for required files ...");
				AppEnums.FilesFoundFlags filesFoundFlags = model.CheckForRequiredFiles();
				//If status = StatusMessage.ErrorRequiredSequenceGroupMdlFileNotFound Then
				//	Me.UpdateProgress(2, "ERROR: Sequence Group MDL file not found.")
				//	Return status
				//ElseIf status = StatusMessage.ErrorRequiredTextureMdlFileNotFound Then
				//	Me.UpdateProgress(2, "ERROR: Texture MDL file not found.")
				//	Return status
				//ElseIf status = StatusMessage.ErrorRequiredAniFileNotFound Then
				//	Me.UpdateProgress(2, "ERROR: ANI file not found.")
				//	Return status
				//ElseIf status = StatusMessage.ErrorRequiredVtxFileNotFound Then
				//	Me.UpdateProgress(2, "ERROR: VTX file not found.")
				//	Return status
				//ElseIf status = StatusMessage.ErrorRequiredVvdFileNotFound Then
				//	Me.UpdateProgress(2, "ERROR: VVD file not found.")
				//	Return status
				//End If
				//Me.UpdateProgress(2, "... All required files found.")
				if (filesFoundFlags == AppEnums.FilesFoundFlags.ErrorRequiredSequenceGroupMdlFileNotFound)
				{
					this.UpdateProgress(2, "ERROR: Sequence Group MDL file not found.");
					return AppEnums.StatusMessage.ErrorRequiredSequenceGroupMdlFileNotFound;
				}
				else if (filesFoundFlags == AppEnums.FilesFoundFlags.ErrorRequiredTextureMdlFileNotFound)
				{
					this.UpdateProgress(2, "ERROR: Texture MDL file not found.");
					return AppEnums.StatusMessage.ErrorRequiredTextureMdlFileNotFound;
				}
				if ((filesFoundFlags & AppEnums.FilesFoundFlags.ErrorRequiredAniFileNotFound) > 0)
				{
					this.UpdateProgress(3, "WARNING: ANI file not found.");
				}
				if ((filesFoundFlags & AppEnums.FilesFoundFlags.ErrorRequiredVtxFileNotFound) > 0)
				{
					this.UpdateProgress(3, "WARNING: VTX file not found.");
				}
				if ((filesFoundFlags & AppEnums.FilesFoundFlags.ErrorRequiredVvdFileNotFound) > 0)
				{
					this.UpdateProgress(3, "WARNING: VVD file not found.");
				}
				if (filesFoundFlags == AppEnums.FilesFoundFlags.AllFilesFound)
				{
					this.UpdateProgress(2, "... All required files found.");
				}
				else
				{
					this.UpdateProgress(2, "... Not all required files found, but decompiling available files.");
				}

				if (this.CancellationPending)
				{
					return status;
				}
				else if (this.theSkipCurrentModelIsActive)
				{
					return status;
				}

				this.UpdateProgress(2, "Reading data ...");
				status = this.ReadCompiledFiles(mdlPathFileName, model);
				if (status == AppEnums.StatusMessage.ErrorRequiredMdlFileNotFound || status == AppEnums.StatusMessage.ErrorRequiredAniFileNotFound || status == AppEnums.StatusMessage.ErrorRequiredVtxFileNotFound || status == AppEnums.StatusMessage.ErrorRequiredVvdFileNotFound)
				{
					this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" stopped due to missing file.");
					return status;
				}
				else if (status == AppEnums.StatusMessage.ErrorInvalidMdlFileId)
				{
					this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" stopped due to invalid file.");
					return status;
				}
				else if (status == AppEnums.StatusMessage.Error)
				{
					this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" stopped due to error.");
					return status;
				}
				else if (this.CancellationPending)
				{
					this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" canceled.");
					status = AppEnums.StatusMessage.Canceled;
					return status;
				}
				else if (this.theSkipCurrentModelIsActive)
				{
					this.UpdateProgress(1, "... Skipping \"" + mdlRelativePathFileName + "\".");
					return status;
				}
				else
				{
					this.UpdateProgress(2, "... Reading data finished.");
				}

				//Me.UpdateProgress(2, "Processinging data ...")
				//status = Me.ProcessData(model)
				//Me.UpdateProgress(2, "... Processinging data finished.")

				//NOTE: Write log files before data files, in case something goes wrong with writing data files.
				if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
				{
					this.UpdateProgress(2, "Writing decompile-info files ...");
					this.WriteDebugFiles(model);
					if (this.CancellationPending)
					{
						this.UpdateProgress(1, "... Decompile of \"" + mdlRelativePathFileName + "\" canceled.");
						status = AppEnums.StatusMessage.Canceled;
						return status;
					}
					else if (this.theSkipCurrentModelIsActive)
					{
						this.UpdateProgress(1, "... Skipping \"" + mdlRelativePathFileName + "\".");
						status = AppEnums.StatusMessage.Skipped;
						return status;
					}
					else
					{
						this.UpdateProgress(2, "... Writing decompile-info files finished.");
					}
				}

				this.UpdateProgress(2, "Writing data ...");
				this.WriteDecompiledFiles(model);
				if (this.CancellationPending)
				{
					this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" canceled.");
					status = AppEnums.StatusMessage.Canceled;
					return status;
				}
				else if (this.theSkipCurrentModelIsActive)
				{
					this.UpdateProgress(1, "... Skipping \"" + mdlRelativePathFileName + "\".");
					status = AppEnums.StatusMessage.Skipped;
					return status;
				}
				else
				{
					this.UpdateProgress(2, "... Writing data finished.");
				}

				this.UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" finished.");
			}
			catch (Exception ex)
			{
				int debug = 4242;
				//Finally
				//	If Me.theLogFileStream IsNot Nothing Then
				//		Me.theLogFileStream.Flush()
				//		Me.theLogFileStream.Close()
				//	End If
			}

			return status;
		}

		private AppEnums.StatusMessage CreateLogTextFile(string mdlPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileLogFileIsChecked)
			{
				string mdlFileName = null;
				string logPath = null;
				string logFileName = null;
				string logPathFileName = null;

				try
				{
					if (!string.IsNullOrEmpty(mdlPathFileName))
					{
						logPath = this.theModelOutputPath;
						mdlFileName = Path.GetFileNameWithoutExtension(mdlPathFileName);
						logFileName = mdlFileName + " " + Properties.Resources.Decompile_LogFileNameSuffix;
					}
					else
					{
						logPath = this.theOutputPath;
						logFileName = Properties.Resources.Decompile_LogFileNameSuffix;
					}
					FileManager.CreatePath(logPath);
					logPathFileName = Path.Combine(logPath, logFileName);

					this.theLogFileStream = File.CreateText(logPathFileName);
					this.theLogFileStream.AutoFlush = true;

					if (File.Exists(logPathFileName))
					{
						this.theDecompiledLogFiles.Add(FileManager.GetRelativePathFileName(this.theOutputPath, logPathFileName));
					}

					this.theLogFileStream.WriteLine("// " + MainCROWBAR.TheApp.GetHeaderComment());
					this.theLogFileStream.Flush();
				}
				catch (Exception ex)
				{
					this.UpdateProgress();
					this.UpdateProgress(2, "ERROR: Crowbar tried to write the decompile log file but the system gave this message: " + ex.Message);
					status = AppEnums.StatusMessage.Error;
				}
			}
			else
			{
				this.theLogFileStream = null;
			}

			return status;
		}

		private AppEnums.StatusMessage ReadCompiledFiles(string mdlPathFileName, SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			this.UpdateProgress(3, "Reading MDL file ...");
			status = model.ReadMdlFile();
			if (status == AppEnums.StatusMessage.Success)
			{
				this.UpdateProgress(3, "... Reading MDL file finished.");
			}
			else if (status == AppEnums.StatusMessage.Error)
			{
				this.UpdateProgress(3, "... Reading MDL file FAILED. (Probably unexpected format.)");
				return status;
			}

			if (model.SequenceGroupMdlFilesAreUsed)
			{
				this.UpdateProgress(3, "Reading sequence group MDL files ...");
				status = model.ReadSequenceGroupMdlFiles();
				if (status == AppEnums.StatusMessage.Success)
				{
					this.UpdateProgress(3, "... Reading sequence group MDL files finished.");
				}
				else if (status == AppEnums.StatusMessage.Error)
				{
					this.UpdateProgress(3, "... Reading sequence group MDL files FAILED. (Probably unexpected format.)");
				}
			}

			if (model.TextureMdlFileIsUsed)
			{
				this.UpdateProgress(3, "Reading texture MDL file ...");
				status = model.ReadTextureMdlFile();
				if (status == AppEnums.StatusMessage.Success)
				{
					this.UpdateProgress(3, "... Reading texture MDL file finished.");
				}
				else if (status == AppEnums.StatusMessage.Error)
				{
					this.UpdateProgress(3, "... Reading texture MDL file FAILED. (Probably unexpected format.)");
				}
			}

			if (model.PhyFileIsUsed)
			{
				this.UpdateProgress(3, "Reading PHY file ...");
				model.SourceModelProgress += this.Model_SourceModelProgress;
				status = model.ReadPhyFile();
				model.SourceModelProgress -= this.Model_SourceModelProgress;
				if (status == AppEnums.StatusMessage.Success)
				{
					this.UpdateProgress(3, "... Reading PHY file finished.");
				}
				else if (status == AppEnums.StatusMessage.Error)
				{
					this.UpdateProgress(3, "... Reading PHY file FAILED. (Probably unexpected format.)");
				}
			}

			if (model.VtxFileIsUsed)
			{
				this.UpdateProgress(3, "Reading VTX file ...");
				status = model.ReadVtxFile();
				if (status == AppEnums.StatusMessage.Success)
				{
					this.UpdateProgress(3, "... Reading VTX file finished.");
				}
				else if (status == AppEnums.StatusMessage.Error)
				{
					this.UpdateProgress(3, "... Reading VTX file FAILED. (Probably unexpected format.)");
				}
			}

			if (model.AniFileIsUsed && MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked)
			{
				this.UpdateProgress(3, "Reading ANI file ...");
				status = model.ReadAniFile();
				if (status == AppEnums.StatusMessage.Success)
				{
					this.UpdateProgress(3, "... Reading ANI file finished.");
				}
				else if (status == AppEnums.StatusMessage.Error)
				{
					this.UpdateProgress(3, "... Reading ANI file FAILED. (Probably unexpected format.)");
				}
			}

			if (model.VvdFileIsUsed)
			{
				this.UpdateProgress(3, "Reading VVD file ...");
				status = model.ReadVvdFile();
				if (status == AppEnums.StatusMessage.Success)
				{
					this.UpdateProgress(3, "... Reading VVD file finished.");
				}
				else if (status == AppEnums.StatusMessage.Error)
				{
					this.UpdateProgress(3, "... Reading VVD file FAILED. (Probably unexpected format.)");
				}
			}

			return status;
		}

		//Private Function ProcessData(ByVal model As SourceModel) As AppEnums.StatusMessage
		//	Dim status As AppEnums.StatusMessage = StatusMessage.Success

		//	'TODO: Create all possible SMD file names before using them, so can handle any name collisions.
		//	'      Store mesh SMD file names in list in SourceMdlModel where the index is lodIndex.
		//	'      Store anim SMD file name in SourceMdlAnimationDesc48.

		//	Return status
		//End Function

		private AppEnums.StatusMessage WriteDecompiledFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			MainCROWBAR.TheApp.SmdFileNames.Clear();

			//TEST:
			//Me.TestWriteDmx()

			status = this.WriteQcFile(model);
			if (status == AppEnums.StatusMessage.Canceled)
			{
				return status;
			}
			else if (status == AppEnums.StatusMessage.Skipped)
			{
				return status;
			}

			status = this.WriteReferenceMeshFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
			{
				return status;
			}
			else if (status == AppEnums.StatusMessage.Skipped)
			{
				return status;
			}

			status = this.WriteLodMeshFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
			{
				return status;
			}
			else if (status == AppEnums.StatusMessage.Skipped)
			{
				return status;
			}

			status = this.WritePhysicsMeshFile(model);
			if (status == AppEnums.StatusMessage.Canceled)
			{
				return status;
			}
			else if (status == AppEnums.StatusMessage.Skipped)
			{
				return status;
			}

			status = this.WriteProceduralBonesFile(model);
			if (status == AppEnums.StatusMessage.Canceled)
			{
				return status;
			}
			else if (status == AppEnums.StatusMessage.Skipped)
			{
				return status;
			}

			status = this.WriteVertexAnimationFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
			{
				return status;
			}
			else if (status == AppEnums.StatusMessage.Skipped)
			{
				return status;
			}

			status = this.WriteBoneAnimationFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
			{
				return status;
			}
			else if (status == AppEnums.StatusMessage.Skipped)
			{
				return status;
			}

			status = this.WriteTextureFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
			{
				return status;
			}
			else if (status == AppEnums.StatusMessage.Skipped)
			{
				return status;
			}

			status = this.WriteDeclareSequenceQciFile(model);

			return status;
		}

		//Private Sub TestWriteDmx()
		//	Dim currentFolder As String
		//	currentFolder = Directory.GetCurrentDirectory()
		//	Directory.SetCurrentDirectory(Me.theModelOutputPath)

		//	Dim HelloWorld As New Datamodel.Datamodel("model", 1)		' must provide a format name (can be anything) and version
		//	HelloWorld.Root = HelloWorld.CreateElement("my_root")
		//	HelloWorld.Root("Hello") = "World"		' any supported attribute type can be assigned
		//	Dim MyString As String = HelloWorld.Root.[Get](Of String)("Hello")

		//	HelloWorld.Save("hello world.dmx", "binary", 2)		' must provide an encoding name and version	

		//	Directory.SetCurrentDirectory(currentFolder)
		//End Sub

		private AppEnums.StatusMessage WriteQcFile(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked)
			{
				if (MainCROWBAR.TheApp.Settings.DecompileGroupIntoQciFilesIsChecked)
				{
					//Me.UpdateProgress(3, "Writing QC and QCI files ...")
					this.UpdateProgress(3, "QC and QCI files: ");
				}
				else
				{
					//Me.UpdateProgress(3, "Writing QC file ...")
					this.UpdateProgress(3, "QC file: ");
				}
				this.theDecompiledFileType = AppEnums.DecompiledFileType.QC;
				this.theFirstDecompiledFileHasBeenAdded = false;
				model.SourceModelProgress += this.Model_SourceModelProgress;

				string qcPathFileName = Path.Combine(this.theModelOutputPath, model.Name + ".qc");

				status = model.WriteQcFile(qcPathFileName);

				if (File.Exists(qcPathFileName))
				{
					this.theDecompiledQcFiles.Add(FileManager.GetRelativePathFileName(this.theOutputPath, qcPathFileName));
				}

				model.SourceModelProgress -= this.Model_SourceModelProgress;
				//If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
				//	Me.UpdateProgress(3, "... Writing QC and QCI files finished.")
				//Else
				//	Me.UpdateProgress(3, "... Writing QC file finished.")
				//End If
			}

			if (this.CancellationPending)
			{
				status = AppEnums.StatusMessage.Canceled;
			}
			else if (this.theSkipCurrentModelIsActive)
			{
				status = AppEnums.StatusMessage.Skipped;
			}

			return status;
		}

		private AppEnums.StatusMessage WriteReferenceMeshFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked)
			{
				if (model.HasMeshData)
				{
					//Me.UpdateProgress(3, "Writing reference mesh files ...")
					this.UpdateProgress(3, "Reference mesh files: ");
					this.theDecompiledFileType = AppEnums.DecompiledFileType.ReferenceMesh;
					this.theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += this.Model_SourceModelProgress;

					status = model.WriteReferenceMeshFiles(this.theModelOutputPath);

					model.SourceModelProgress -= this.Model_SourceModelProgress;
					//Me.UpdateProgress(3, "... Writing reference mesh files finished.")
				}
			}

			return status;
		}

		private AppEnums.StatusMessage WriteLodMeshFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileLodMeshSmdFilesIsChecked)
			{
				if (model.HasLodMeshData)
				{
					//Me.UpdateProgress(3, "Writing LOD mesh files ...")
					this.UpdateProgress(3, "LOD mesh files: ");
					this.theDecompiledFileType = AppEnums.DecompiledFileType.LodMesh;
					this.theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += this.Model_SourceModelProgress;

					status = model.WriteLodMeshFiles(this.theModelOutputPath);

					model.SourceModelProgress -= this.Model_SourceModelProgress;
					//Me.UpdateProgress(3, "... Writing LOD mesh files finished.")
				}
			}

			return status;
		}

		private AppEnums.StatusMessage WritePhysicsMeshFile(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked)
			{
				if (model.HasPhysicsMeshData)
				{
					//Me.UpdateProgress(3, "Writing physics mesh file ...")
					this.UpdateProgress(3, "Physics mesh file: ");
					this.theDecompiledFileType = AppEnums.DecompiledFileType.PhysicsMesh;
					this.theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += this.Model_SourceModelProgress;

					status = model.WritePhysicsMeshSmdFile(this.theModelOutputPath);

					model.SourceModelProgress -= this.Model_SourceModelProgress;
					//Me.UpdateProgress(3, "... Writing physics mesh file finished.")
				}
			}

			if (this.CancellationPending)
			{
				status = AppEnums.StatusMessage.Canceled;
			}
			else if (this.theSkipCurrentModelIsActive)
			{
				status = AppEnums.StatusMessage.Skipped;
			}

			return status;
		}

		private AppEnums.StatusMessage WriteVertexAnimationFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked)
			{
				if (model.HasVertexAnimationData)
				{
					//Me.UpdateProgress(3, "Writing VTA file ...")
					this.UpdateProgress(3, "Vertex animation files: ");
					this.theDecompiledFileType = AppEnums.DecompiledFileType.VertexAnimation;
					this.theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += this.Model_SourceModelProgress;

					//Dim vtaPathFileName As String
					//vtaPathFileName = Path.Combine(Me.theModelOutputPath, SourceFileNamesModule.GetVtaFileName(model.Name))

					//status = model.WriteVertexAnimationVtaFile(vtaPathFileName)
					status = model.WriteVertexAnimationVtaFiles(this.theModelOutputPath);

					//If File.Exists(vtaPathFileName) Then
					//	Me.theDecompiledVtaFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, vtaPathFileName))
					//End If

					model.SourceModelProgress -= this.Model_SourceModelProgress;
					//Me.UpdateProgress(3, "... Writing VTA file finished.")
				}
			}

			if (this.CancellationPending)
			{
				status = AppEnums.StatusMessage.Canceled;
			}
			else if (this.theSkipCurrentModelIsActive)
			{
				status = AppEnums.StatusMessage.Skipped;
			}

			return status;
		}

		private AppEnums.StatusMessage WriteBoneAnimationFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked)
			{
				if (model.HasBoneAnimationData)
				{
					string outputPath = Path.Combine(this.theModelOutputPath, SourceFileNamesModule.GetAnimationSmdRelativePath(model.Name));
					if (FileManager.PathExistsAfterTryToCreate(outputPath))
					{
						//Me.UpdateProgress(3, "Writing bone animation SMD files ...")
						this.UpdateProgress(3, "Bone animation files: ");
						this.theDecompiledFileType = AppEnums.DecompiledFileType.BoneAnimation;
						this.theFirstDecompiledFileHasBeenAdded = false;
						model.SourceModelProgress += this.Model_SourceModelProgress;

						status = model.WriteBoneAnimationSmdFiles(this.theModelOutputPath);

						model.SourceModelProgress -= this.Model_SourceModelProgress;
						//Me.UpdateProgress(3, "... Writing bone animation SMD files finished.")
					}
					else
					{
						this.UpdateProgress(3, "WARNING: Unable to create \"" + outputPath + "\" where bone animation SMD files would be written.");
					}
				}
			}

			return status;
		}

		private AppEnums.StatusMessage WriteProceduralBonesFile(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked)
			{
				if (model.HasProceduralBonesData)
				{
					//Me.UpdateProgress(3, "Writing VRD file ...")
					this.UpdateProgress(3, "Procedural bones file: ");
					this.theDecompiledFileType = AppEnums.DecompiledFileType.ProceduralBones;
					this.theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += this.Model_SourceModelProgress;

					string vrdPathFileName = Path.Combine(this.theModelOutputPath, SourceFileNamesModule.GetVrdFileName(model.Name));

					status = model.WriteVrdFile(vrdPathFileName);

					if (File.Exists(vrdPathFileName))
					{
						this.theDecompiledVrdFiles.Add(FileManager.GetRelativePathFileName(this.theOutputPath, vrdPathFileName));
					}

					model.SourceModelProgress -= this.Model_SourceModelProgress;
					//Me.UpdateProgress(3, "... Writing VRD file finished.")
				}
			}

			if (this.CancellationPending)
			{
				status = AppEnums.StatusMessage.Canceled;
			}
			else if (this.theSkipCurrentModelIsActive)
			{
				status = AppEnums.StatusMessage.Skipped;
			}

			return status;
		}

		private AppEnums.StatusMessage WriteDeclareSequenceQciFile(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileDeclareSequenceQciFileIsChecked)
			{
				if (model.HasBoneAnimationData)
				{
					this.UpdateProgress(3, "DeclareSequence QCI file: ");
					this.theDecompiledFileType = AppEnums.DecompiledFileType.DeclareSequenceQci;
					this.theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += this.Model_SourceModelProgress;

					string declareSequenceQciPathFileName = Path.Combine(this.theModelOutputPath, SourceFileNamesModule.GetDeclareSequenceQciFileName(model.Name));

					status = model.WriteDeclareSequenceQciFile(declareSequenceQciPathFileName);

					if (File.Exists(declareSequenceQciPathFileName))
					{
						this.theDecompiledDeclareSequenceQciFiles.Add(FileManager.GetRelativePathFileName(this.theOutputPath, declareSequenceQciPathFileName));
					}

					model.SourceModelProgress -= this.Model_SourceModelProgress;
				}
			}

			if (this.CancellationPending)
			{
				status = AppEnums.StatusMessage.Canceled;
			}
			else if (this.theSkipCurrentModelIsActive)
			{
				status = AppEnums.StatusMessage.Skipped;
			}

			return status;
		}

		private AppEnums.StatusMessage WriteTextureFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.DecompileTextureBmpFilesIsChecked)
			{
				if (model.HasTextureFileData)
				{
					//Me.UpdateProgress(3, "Writing texture files ...")
					this.UpdateProgress(3, "Texture files: ");
					this.theDecompiledFileType = AppEnums.DecompiledFileType.TextureBmp;
					this.theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += this.Model_SourceModelProgress;

					status = model.WriteTextureFiles(this.theModelOutputPath);

					model.SourceModelProgress -= this.Model_SourceModelProgress;
					//Me.UpdateProgress(3, "... Writing texture files finished.")
				}
			}

			return status;
		}

		private void WriteDebugFiles(SourceModel model)
		{
			string debugPath = MainCROWBAR.TheApp.GetDebugPath(this.theModelOutputPath, model.Name);

			FileManager.CreatePath(debugPath);

			this.theDecompiledFileType = AppEnums.DecompiledFileType.Debug;
			this.theFirstDecompiledFileHasBeenAdded = false;
			model.SourceModelProgress += this.Model_SourceModelProgress;

			model.WriteAccessedBytesDebugFiles(debugPath);
			if (this.CancellationPending)
			{
				return;
			}
			else if (this.theSkipCurrentModelIsActive)
			{
				return;
			}

			model.SourceModelProgress -= this.Model_SourceModelProgress;

			//Dim debug3File As AppDebug3File
			//debug3File = New AppDebug3File()
			//debugPathFileName = Path.Combine(debugPathName, model.Name + " debug - unknown bytes.txt")
			//debug3File.WriteFile(debugPathFileName, model.MdlFileData.theUnknownValues)

			//	Dim debugFile As AppDebug1File
			//	debugFile = New AppDebug1File()
			//	debugPathFileName = Path.Combine(debugPathName, TheSourceEngineModel.ModelName + " debug - Structure info.txt")
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

#region Event Handlers

		private void Model_SourceModelProgress(object sender, SourceModelProgressEventArgs e)
		{
			if (e.Progress == AppEnums.ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum)
			{
				//TODO: Test that this shows when needed.
				this.UpdateProgress(4, "WARNING: The PHY file's checksum value does not match the MDL file's checksum value.");
			//ElseIf e.Progress = ProgressOptions.WritingFileStarted Then
			//	Dim pathFileName As String
			//	Dim fileName As String
			//	pathFileName = e.Message
			//	fileName = Path.GetFileName(pathFileName)
			//	'TODO: Figure out how to rename SMD file if already written in a previous step, which might happen if, for example, an anim is named "<modelname>_reference" or "<modelname>_physics".
			//	'      Could also happen if the loop through SequenceDescs has already created the SMD file before the loop through AnimationDescs.
			//	'      The same name can be used by multiple sequences, as is the case for 3 "frontkick" sequences in Half-Life Opposing Force "massn.mdl".
			//	If TheApp.SmdFileNames.Contains(pathFileName) Then
			//		Dim model As SourceModel
			//		model = CType(sender, SourceModel)
			//		model.WritingSingleFileIsCanceled = True
			//		'Me.UpdateProgress(4, "WARNING: The file, """ + smdFileName + """, was written already in a previous step.")
			//		'Else
			//		'	Me.UpdateProgress(4, "Writing """ + fileName + """ file ...")
			//	End If
			}
			else if (e.Progress == AppEnums.ProgressOptions.WritingFileFailed)
			{
				this.UpdateProgress(4, e.Message);
			}
			else if (e.Progress == AppEnums.ProgressOptions.WritingFileFinished)
			{
				string pathFileName = null;
				string fileName = null;
				pathFileName = e.Message;
				fileName = Path.GetFileName(pathFileName);
				//Me.UpdateProgress(4, "... Writing """ + fileName + """ file finished.")
				this.UpdateProgress(4, fileName);

				if (!this.theFirstDecompiledFileHasBeenAdded && File.Exists(pathFileName))
				{
					string relativePathFileName = FileManager.GetRelativePathFileName(this.theOutputPath, pathFileName);

					if (this.theDecompiledFileType == AppEnums.DecompiledFileType.ReferenceMesh)
					{
						this.theDecompiledFirstRefSmdFiles.Add(relativePathFileName);
					}
					else if (this.theDecompiledFileType == AppEnums.DecompiledFileType.LodMesh)
					{
						this.theDecompiledFirstLodSmdFiles.Add(relativePathFileName);
					}
					else if (this.theDecompiledFileType == AppEnums.DecompiledFileType.BoneAnimation)
					{
						this.theDecompiledFirstBoneAnimSmdFiles.Add(relativePathFileName);
					}
					else if (this.theDecompiledFileType == AppEnums.DecompiledFileType.PhysicsMesh)
					{
						this.theDecompiledPhysicsFiles.Add(relativePathFileName);
					}
					else if (this.theDecompiledFileType == AppEnums.DecompiledFileType.TextureBmp)
					{
						this.theDecompiledFirstTextureBmpFiles.Add(relativePathFileName);
					}
					else if (this.theDecompiledFileType == AppEnums.DecompiledFileType.Debug)
					{
						this.theDecompiledFirstDebugFiles.Add(relativePathFileName);
					}

					this.theFirstDecompiledFileHasBeenAdded = true;
				}
				//TheApp.SmdFileNames.Add(pathFileName)

				SourceModel model = (SourceModel)sender;
				if (this.CancellationPending)
				{
					//status = StatusMessage.Canceled
					model.WritingIsCanceled = true;
					//ElseIf Me.theSkipCurrentModelIsActive Then
					//	'status = StatusMessage.Skipped
					//	model.WritingSingleFileIsCanceled = True
				}
			}
			else
			{
				int progressUnhandled = 4242;
			}
		}

#endregion

#region Data

		private bool theSkipCurrentModelIsActive;
		private string theInputMdlPathName;
		private string theOutputPath;
		private string theModelOutputPath;
		private string theOutputPathOrModelOutputFileName;

		private StreamWriter theLogFileStream;

		private BindingListEx<string> theDecompiledQcFiles;
		private BindingListEx<string> theDecompiledFirstRefSmdFiles;
		private BindingListEx<string> theDecompiledFirstLodSmdFiles;
		private BindingListEx<string> theDecompiledPhysicsFiles;
		private BindingListEx<string> theDecompiledVtaFiles;
		private BindingListEx<string> theDecompiledFirstBoneAnimSmdFiles;
		private BindingListEx<string> theDecompiledVrdFiles;
		private BindingListEx<string> theDecompiledDeclareSequenceQciFiles;
		private BindingListEx<string> theDecompiledFirstTextureBmpFiles;
		private BindingListEx<string> theDecompiledLogFiles;
		private BindingListEx<string> theDecompiledFirstDebugFiles;

		private AppEnums.DecompiledFileType theDecompiledFileType;
		private bool theFirstDecompiledFileHasBeenAdded;

#endregion

	}

}