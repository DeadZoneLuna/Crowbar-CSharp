using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Crowbar
{
	public class Decompiler : BackgroundWorker
	{
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

		#region Create and Destroy
		public Decompiler() : base()
		{
			theDecompiledQcFiles = new BindingListEx<string>();
			theDecompiledFirstRefSmdFiles = new BindingListEx<string>();
			theDecompiledFirstLodSmdFiles = new BindingListEx<string>();
			theDecompiledPhysicsFiles = new BindingListEx<string>();
			theDecompiledVtaFiles = new BindingListEx<string>();
			theDecompiledFirstBoneAnimSmdFiles = new BindingListEx<string>();
			theDecompiledVrdFiles = new BindingListEx<string>();
			theDecompiledDeclareSequenceQciFiles = new BindingListEx<string>();
			theDecompiledFirstTextureBmpFiles = new BindingListEx<string>();
			theDecompiledLogFiles = new BindingListEx<string>();
			theDecompiledFirstDebugFiles = new BindingListEx<string>();

			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
			DoWork += Decompiler_DoWork;
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

		public string GetOutputPathFolderOrFileName()
		{
			return theOutputPathOrModelOutputFileName;
		}
		#endregion

		#region Private Methods
		#endregion

		#region Private Methods in Background Thread
		private void Decompiler_DoWork(object sender, DoWorkEventArgs e)
		{
			ReportProgress(0, string.Empty);

			theOutputPath = GetOutputPath();

			AppEnums.StatusMessage status = AppEnums.StatusMessage.Error;
			if (DecompilerInputsAreValid())
				status = Decompile();

			e.Result = GetDecompilerOutputs(status);
			if (CancellationPending)
				e.Cancel = true;
		}

		private string GetOutputPath()
		{
			if (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.Subfolder)
			{
				if (File.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
					return Path.Combine(FileManager.GetPath(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName), MainCROWBAR.TheApp.Settings.DecompileOutputSubfolderName);
				else if (Directory.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
					return Path.GetFullPath(Path.Combine(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName, MainCROWBAR.TheApp.Settings.DecompileOutputSubfolderName));
				else
					return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}

			return MainCROWBAR.TheApp.Settings.DecompileOutputFullPath;
		}

		private bool DecompilerInputsAreValid()
		{
			return string.IsNullOrEmpty(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName) ? false : FileManager.PathExistsAfterTryToCreate(theOutputPath);
		}

		private DecompilerOutputInfo GetDecompilerOutputs(AppEnums.StatusMessage status)
		{
			DecompilerOutputInfo decompileResultInfo = new DecompilerOutputInfo { theStatus = status };

			if (MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledQcFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledFirstRefSmdFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompileLodMeshSmdFilesIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledFirstLodSmdFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledPhysicsFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledVtaFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledFirstBoneAnimSmdFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledVrdFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompileDeclareSequenceQciFileIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledDeclareSequenceQciFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompileTextureBmpFilesIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledFirstTextureBmpFiles;
			else if (MainCROWBAR.TheApp.Settings.DecompileLogFileIsChecked)
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledLogFiles;
			else
				decompileResultInfo.theDecompiledRelativePathFileNames = theDecompiledFirstDebugFiles;

			theOutputPathOrModelOutputFileName = theOutputPath;
			if (decompileResultInfo.theDecompiledRelativePathFileNames.Count <= 0 || theDecompiledQcFiles.Count <= 0)
				theOutputPathOrModelOutputFileName = string.Empty;

			return decompileResultInfo;
		}

		private AppEnums.StatusMessage Decompile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			theSkipCurrentModelIsActive = false;

			theDecompiledQcFiles.Clear();
			theDecompiledFirstRefSmdFiles.Clear();
			theDecompiledFirstLodSmdFiles.Clear();
			theDecompiledPhysicsFiles.Clear();
			theDecompiledVtaFiles.Clear();
			theDecompiledFirstBoneAnimSmdFiles.Clear();
			theDecompiledVrdFiles.Clear();
			theDecompiledDeclareSequenceQciFiles.Clear();
			theDecompiledFirstTextureBmpFiles.Clear();
			theDecompiledLogFiles.Clear();
			theDecompiledFirstDebugFiles.Clear();

			string mdlPathFileName = MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName;
			if (File.Exists(mdlPathFileName))
				theInputMdlPathName = FileManager.GetPath(mdlPathFileName);
			else if (Directory.Exists(mdlPathFileName))
				theInputMdlPathName = mdlPathFileName;

			string progressDescriptionText = "Decompiling with " + MainCROWBAR.TheApp.GetProductNameAndVersion() + ": ";

			try
			{
				if (string.IsNullOrEmpty(theInputMdlPathName))
				{
					//Can get here if mdlPathFileName exists, but only with parts of the path using "Length8.3" names.
					//Somehow when drag-dropping such a pathFileName, even though Windows shows full names in the path, Crowbar shows it with "Length8.3" names.
					progressDescriptionText += "\"" + mdlPathFileName + "\"";
					UpdateProgressStart(progressDescriptionText + " ...");
					UpdateProgress();
					UpdateProgress(1, "ERROR: Failed because actual path is too long.");
					status = AppEnums.StatusMessage.Error;
				}
				else if (MainCROWBAR.TheApp.Settings.DecompileMode == AppEnums.InputOptions.FolderRecursion)
				{
					progressDescriptionText += "\"" + theInputMdlPathName + "\" (folder + subfolders)";
					UpdateProgressStart(progressDescriptionText + " ...");

					status = CreateLogTextFile("");
					//If status = StatusMessage.Error Then
					//	Return status
					//End If

					DecompileModelsInFolderRecursively(theInputMdlPathName);
				}
				else if (MainCROWBAR.TheApp.Settings.DecompileMode == AppEnums.InputOptions.Folder)
				{
					progressDescriptionText += "\"" + theInputMdlPathName + "\" (folder)";
					UpdateProgressStart(progressDescriptionText + " ...");

					status = CreateLogTextFile("");
					//If status = StatusMessage.Error Then
					//	Return status
					//End If

					DecompileModelsInFolder(theInputMdlPathName);
				}
				else
				{
					progressDescriptionText += "\"" + mdlPathFileName + "\"";
					UpdateProgressStart(progressDescriptionText + " ...");
					status = DecompileOneModel(mdlPathFileName);
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

		private AppEnums.StatusMessage DecompileModelsInFolderRecursively(string modelsPathName)
		{
			AppEnums.StatusMessage status = DecompileModelsInFolder(modelsPathName);

			if (CancellationPending)
				return AppEnums.StatusMessage.Canceled;

			foreach (string aPathName in Directory.GetDirectories(modelsPathName))
			{
				status = DecompileModelsInFolderRecursively(aPathName);
				if (CancellationPending)
					return AppEnums.StatusMessage.Canceled;
			}

			return status;
		}

		private AppEnums.StatusMessage DecompileModelsInFolder(string modelsPathName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			foreach (string aPathFileName in Directory.GetFiles(modelsPathName, "*.mdl"))
			{
				status = DecompileOneModel(aPathFileName);

				if (CancellationPending)
					return AppEnums.StatusMessage.Canceled;
				else if (theSkipCurrentModelIsActive)
				{
					theSkipCurrentModelIsActive = false;
					continue;
				}
			}
			return status;
		}

		private AppEnums.StatusMessage DecompileOneModel(string mdlPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				string mdlFileName = Path.GetFileName(mdlPathFileName);
				string mdlRelativePathName = FileManager.GetRelativePathFileName(theInputMdlPathName, FileManager.GetPath(mdlPathFileName));
				string mdlRelativePathFileName = Path.Combine(mdlRelativePathName, mdlFileName);

				string modelName = Path.GetFileNameWithoutExtension(mdlPathFileName);

				theModelOutputPath = Path.Combine(theOutputPath, mdlRelativePathName);
				theModelOutputPath = Path.GetFullPath(theModelOutputPath);
				if (MainCROWBAR.TheApp.Settings.DecompileFolderForEachModelIsChecked)
					theModelOutputPath = Path.Combine(theModelOutputPath, modelName);

				FileManager.CreatePath(theModelOutputPath);

				if (MainCROWBAR.TheApp.Settings.DecompileMode == AppEnums.InputOptions.File)
					status = CreateLogTextFile(mdlPathFileName);

				UpdateProgress();
				UpdateProgress(1, "Decompiling \"" + mdlRelativePathFileName + "\" ...");

				SourceModel model = null;
				int version = 0;
				try
				{
					model = SourceModel.Create(mdlPathFileName, MainCROWBAR.TheApp.Settings.DecompileOverrideMdlVersion, ref version);
					if (model != null)
					{
						if (MainCROWBAR.TheApp.Settings.DecompileOverrideMdlVersion == AppEnums.SupportedMdlVersion.DoNotOverride)
							UpdateProgress(2, "Model version " + model.Version.ToString() + " detected.");
						else
							UpdateProgress(2, "Model version overridden to be " + model.Version.ToString() + ".");
					}
					else
					{
						UpdateProgress(2, "ERROR: Model version " + version.ToString() + " not currently supported.");
						UpdateProgress(2, "       If the model works in-game or HLMV, try changing 'Override MDL version' option.");
						UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" FAILED.");
						status = AppEnums.StatusMessage.Error;
						return status;
					}
				}
				catch (FormatException ex)
				{
					UpdateProgress(2, ex.Message);
					UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" FAILED.");
					status = AppEnums.StatusMessage.Error;
					return status;
				}
				catch (Exception ex)
				{
					//Me.UpdateProgress(2, "ERROR: " + ex.Message)
					UpdateProgress(2, "Crowbar tried to read the MDL file but the system gave this message: " + ex.Message);
					UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" FAILED.");
					status = AppEnums.StatusMessage.Error;
					return status;
				}

				UpdateProgress(2, "Reading MDL file header ...");
				status = model.ReadMdlFileHeader();

				if (status == AppEnums.StatusMessage.ErrorInvalidInternalMdlFileSize)
					UpdateProgress(3, "WARNING: The internally recorded file size is different than the actual file size. Some data might not decompile correctly.");
				UpdateProgress(2, "... Reading MDL file header finished.");

				UpdateProgress(2, "Checking for required files ...");
				AppEnums.FilesFoundFlags filesFoundFlags = model.CheckForRequiredFiles();
				if (filesFoundFlags == AppEnums.FilesFoundFlags.ErrorRequiredSequenceGroupMdlFileNotFound)
				{
					UpdateProgress(2, "ERROR: Sequence Group MDL file not found.");
					return AppEnums.StatusMessage.ErrorRequiredSequenceGroupMdlFileNotFound;
				}
				else if (filesFoundFlags == AppEnums.FilesFoundFlags.ErrorRequiredTextureMdlFileNotFound)
				{
					UpdateProgress(2, "ERROR: Texture MDL file not found.");
					return AppEnums.StatusMessage.ErrorRequiredTextureMdlFileNotFound;
				}
				if ((filesFoundFlags & AppEnums.FilesFoundFlags.ErrorRequiredAniFileNotFound) > 0)
					UpdateProgress(3, "WARNING: ANI file not found.");
				if ((filesFoundFlags & AppEnums.FilesFoundFlags.ErrorRequiredVtxFileNotFound) > 0)
					UpdateProgress(3, "WARNING: VTX file not found.");
				if ((filesFoundFlags & AppEnums.FilesFoundFlags.ErrorRequiredVvdFileNotFound) > 0)
					UpdateProgress(3, "WARNING: VVD file not found.");
				if (filesFoundFlags == AppEnums.FilesFoundFlags.AllFilesFound)
					UpdateProgress(2, "... All required files found.");
				else
					UpdateProgress(2, "... Not all required files found, but decompiling available files.");

				if (CancellationPending)
					return status;
				else if (theSkipCurrentModelIsActive)
					return status;

				UpdateProgress(2, "Reading data ...");
				status = ReadCompiledFiles(mdlPathFileName, model);
				if (status == AppEnums.StatusMessage.ErrorRequiredMdlFileNotFound || status == AppEnums.StatusMessage.ErrorRequiredAniFileNotFound || status == AppEnums.StatusMessage.ErrorRequiredVtxFileNotFound || status == AppEnums.StatusMessage.ErrorRequiredVvdFileNotFound)
				{
					UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" stopped due to missing file.");
					return status;
				}
				else if (status == AppEnums.StatusMessage.ErrorInvalidMdlFileId)
				{
					UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" stopped due to invalid file.");
					return status;
				}
				else if (status == AppEnums.StatusMessage.Error)
				{
					UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" stopped due to error.");
					return status;
				}
				else if (CancellationPending)
				{
					UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" canceled.");
					status = AppEnums.StatusMessage.Canceled;
					return status;
				}
				else if (theSkipCurrentModelIsActive)
				{
					UpdateProgress(1, "... Skipping \"" + mdlRelativePathFileName + "\".");
					return status;
				}
				else
					UpdateProgress(2, "... Reading data finished.");

				//NOTE: Write log files before data files, in case something goes wrong with writing data files.
				if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
				{
					UpdateProgress(2, "Writing decompile-info files ...");
					WriteDebugFiles(model);
					if (CancellationPending)
					{
						UpdateProgress(1, "... Decompile of \"" + mdlRelativePathFileName + "\" canceled.");
						status = AppEnums.StatusMessage.Canceled;
						return status;
					}
					else if (theSkipCurrentModelIsActive)
					{
						UpdateProgress(1, "... Skipping \"" + mdlRelativePathFileName + "\".");
						status = AppEnums.StatusMessage.Skipped;
						return status;
					}
					else
						UpdateProgress(2, "... Writing decompile-info files finished.");
				}

				UpdateProgress(2, "Writing data ...");
				WriteDecompiledFiles(model);
				if (CancellationPending)
				{
					UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" canceled.");
					status = AppEnums.StatusMessage.Canceled;
					return status;
				}
				else if (theSkipCurrentModelIsActive)
				{
					UpdateProgress(1, "... Skipping \"" + mdlRelativePathFileName + "\".");
					status = AppEnums.StatusMessage.Skipped;
					return status;
				}
				else
					UpdateProgress(2, "... Writing data finished.");

				UpdateProgress(1, "... Decompiling \"" + mdlRelativePathFileName + "\" finished.");
			}
			catch (Exception)
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
				try
				{
					string logPath;
					string logFileName;
					if (!string.IsNullOrEmpty(mdlPathFileName))
					{
						logPath = theModelOutputPath;
						string mdlFileName = Path.GetFileNameWithoutExtension(mdlPathFileName);
						logFileName = mdlFileName + " " + Properties.Resources.Decompile_LogFileNameSuffix;
					}
					else
					{
						logPath = theOutputPath;
						logFileName = Properties.Resources.Decompile_LogFileNameSuffix;
					}

					FileManager.CreatePath(logPath);
					string logPathFileName = Path.Combine(logPath, logFileName);

					theLogFileStream = File.CreateText(logPathFileName);
					theLogFileStream.AutoFlush = true;

					if (File.Exists(logPathFileName))
						theDecompiledLogFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, logPathFileName));

					theLogFileStream.WriteLine("// " + MainCROWBAR.TheApp.GetHeaderComment());
					theLogFileStream.Flush();
				}
				catch (Exception ex)
				{
					UpdateProgress();
					UpdateProgress(2, "ERROR: Crowbar tried to write the decompile log file but the system gave this message: " + ex.Message);
					status = AppEnums.StatusMessage.Error;
				}
			}
			else
				theLogFileStream = null;

			return status;
		}

		private AppEnums.StatusMessage ReadCompiledFiles(string mdlPathFileName, SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			UpdateProgress(3, "Reading MDL file ...");
			status = model.ReadMdlFile();
			if (status == AppEnums.StatusMessage.Success)
				UpdateProgress(3, "... Reading MDL file finished.");
			else if (status == AppEnums.StatusMessage.Error)
			{
				UpdateProgress(3, "... Reading MDL file FAILED. (Probably unexpected format.)");
				return status;
			}

			if (model.SequenceGroupMdlFilesAreUsed)
			{
				UpdateProgress(3, "Reading sequence group MDL files ...");
				status = model.ReadSequenceGroupMdlFiles();
				if (status == AppEnums.StatusMessage.Success)
					UpdateProgress(3, "... Reading sequence group MDL files finished.");
				else if (status == AppEnums.StatusMessage.Error)
					UpdateProgress(3, "... Reading sequence group MDL files FAILED. (Probably unexpected format.)");
			}

			if (model.TextureMdlFileIsUsed)
			{
				UpdateProgress(3, "Reading texture MDL file ...");
				status = model.ReadTextureMdlFile();
				if (status == AppEnums.StatusMessage.Success)
					UpdateProgress(3, "... Reading texture MDL file finished.");
				else if (status == AppEnums.StatusMessage.Error)
					UpdateProgress(3, "... Reading texture MDL file FAILED. (Probably unexpected format.)");
			}

			if (model.PhyFileIsUsed)
			{
				UpdateProgress(3, "Reading PHY file ...");
				model.SourceModelProgress += Model_SourceModelProgress;
				status = model.ReadPhyFile();
				model.SourceModelProgress -= Model_SourceModelProgress;
				if (status == AppEnums.StatusMessage.Success)
					UpdateProgress(3, "... Reading PHY file finished.");
				else if (status == AppEnums.StatusMessage.Error)
					UpdateProgress(3, "... Reading PHY file FAILED. (Probably unexpected format.)");
			}

			if (model.VtxFileIsUsed)
			{
				UpdateProgress(3, "Reading VTX file ...");
				status = model.ReadVtxFile();
				if (status == AppEnums.StatusMessage.Success)
					UpdateProgress(3, "... Reading VTX file finished.");
				else if (status == AppEnums.StatusMessage.Error)
					UpdateProgress(3, "... Reading VTX file FAILED. (Probably unexpected format.)");
			}

			if (model.AniFileIsUsed && MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked)
			{
				UpdateProgress(3, "Reading ANI file ...");
				status = model.ReadAniFile();
				if (status == AppEnums.StatusMessage.Success)
					UpdateProgress(3, "... Reading ANI file finished.");
				else if (status == AppEnums.StatusMessage.Error)
					UpdateProgress(3, "... Reading ANI file FAILED. (Probably unexpected format.)");
			}

			if (model.VvdFileIsUsed)
			{
				UpdateProgress(3, "Reading VVD file ...");
				status = model.ReadVvdFile();
				if (status == AppEnums.StatusMessage.Success)
					UpdateProgress(3, "... Reading VVD file finished.");
				else if (status == AppEnums.StatusMessage.Error)
					UpdateProgress(3, "... Reading VVD file FAILED. (Probably unexpected format.)");
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
			MainCROWBAR.TheApp.SmdFileNames.Clear();

			//TEST:
			//Me.TestWriteDmx()

			AppEnums.StatusMessage status = WriteQcFile(model);
			if (status == AppEnums.StatusMessage.Canceled)
				return status;
			else if (status == AppEnums.StatusMessage.Skipped)
				return status;

			status = WriteReferenceMeshFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
				return status;
			else if (status == AppEnums.StatusMessage.Skipped)
				return status;

			status = WriteLodMeshFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
				return status;
			else if (status == AppEnums.StatusMessage.Skipped)
				return status;

			status = WritePhysicsMeshFile(model);
			if (status == AppEnums.StatusMessage.Canceled)
				return status;
			else if (status == AppEnums.StatusMessage.Skipped)
				return status;

			status = WriteProceduralBonesFile(model);
			if (status == AppEnums.StatusMessage.Canceled)
				return status;
			else if (status == AppEnums.StatusMessage.Skipped)
				return status;

			status = WriteVertexAnimationFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
				return status;
			else if (status == AppEnums.StatusMessage.Skipped)
				return status;

			status = WriteBoneAnimationFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
				return status;
			else if (status == AppEnums.StatusMessage.Skipped)
				return status;

			status = WriteTextureFiles(model);
			if (status == AppEnums.StatusMessage.Canceled)
				return status;
			else if (status == AppEnums.StatusMessage.Skipped)
				return status;

			return WriteDeclareSequenceQciFile(model);
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
				UpdateProgress(3, MainCROWBAR.TheApp.Settings.DecompileGroupIntoQciFilesIsChecked ? "QC and QCI files: " : "QC file: ");
				theDecompiledFileType = AppEnums.DecompiledFileType.QC;
				theFirstDecompiledFileHasBeenAdded = false;
				model.SourceModelProgress += Model_SourceModelProgress;

				string qcPathFileName = Path.Combine(theModelOutputPath, model.Name + ".qc");
				status = model.WriteQcFile(qcPathFileName);

				if (File.Exists(qcPathFileName))
					theDecompiledQcFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, qcPathFileName));

				model.SourceModelProgress -= Model_SourceModelProgress;
			}

			if (CancellationPending)
				status = AppEnums.StatusMessage.Canceled;
			else if (theSkipCurrentModelIsActive)
				status = AppEnums.StatusMessage.Skipped;

			return status;
		}

		private AppEnums.StatusMessage WriteReferenceMeshFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			if (MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked)
			{
				if (model.HasMeshData)
				{
					UpdateProgress(3, "Reference mesh files: ");
					theDecompiledFileType = AppEnums.DecompiledFileType.ReferenceMesh;
					theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += Model_SourceModelProgress;

					status = model.WriteReferenceMeshFiles(theModelOutputPath);

					model.SourceModelProgress -= Model_SourceModelProgress;
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
					UpdateProgress(3, "LOD mesh files: ");
					theDecompiledFileType = AppEnums.DecompiledFileType.LodMesh;
					theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += Model_SourceModelProgress;

					status = model.WriteLodMeshFiles(theModelOutputPath);

					model.SourceModelProgress -= Model_SourceModelProgress;
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
					UpdateProgress(3, "Physics mesh file: ");
					theDecompiledFileType = AppEnums.DecompiledFileType.PhysicsMesh;
					theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += Model_SourceModelProgress;

					status = model.WritePhysicsMeshSmdFile(theModelOutputPath);

					model.SourceModelProgress -= Model_SourceModelProgress;
				}
			}

			if (CancellationPending)
				status = AppEnums.StatusMessage.Canceled;
			else if (theSkipCurrentModelIsActive)
				status = AppEnums.StatusMessage.Skipped;

			return status;
		}

		private AppEnums.StatusMessage WriteVertexAnimationFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			if (MainCROWBAR.TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked)
			{
				if (model.HasVertexAnimationData)
				{
					UpdateProgress(3, "Vertex animation files: ");
					theDecompiledFileType = AppEnums.DecompiledFileType.VertexAnimation;
					theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += Model_SourceModelProgress;

					status = model.WriteVertexAnimationVtaFiles(theModelOutputPath);

					model.SourceModelProgress -= Model_SourceModelProgress;
				}
			}

			if (CancellationPending)
				status = AppEnums.StatusMessage.Canceled;
			else if (theSkipCurrentModelIsActive)
				status = AppEnums.StatusMessage.Skipped;

			return status;
		}

		private AppEnums.StatusMessage WriteBoneAnimationFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			if (MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked)
			{
				if (model.HasBoneAnimationData)
				{
					string outputPath = Path.Combine(theModelOutputPath, SourceFileNamesModule.GetAnimationSmdRelativePath(model.Name));
					if (FileManager.PathExistsAfterTryToCreate(outputPath))
					{
						UpdateProgress(3, "Bone animation files: ");
						theDecompiledFileType = AppEnums.DecompiledFileType.BoneAnimation;
						theFirstDecompiledFileHasBeenAdded = false;
						model.SourceModelProgress += Model_SourceModelProgress;

						status = model.WriteBoneAnimationSmdFiles(theModelOutputPath);

						model.SourceModelProgress -= Model_SourceModelProgress;
					}
					else
						UpdateProgress(3, "WARNING: Unable to create \"" + outputPath + "\" where bone animation SMD files would be written.");
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
					UpdateProgress(3, "Procedural bones file: ");
					theDecompiledFileType = AppEnums.DecompiledFileType.ProceduralBones;
					theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += Model_SourceModelProgress;

					string vrdPathFileName = Path.Combine(theModelOutputPath, SourceFileNamesModule.GetVrdFileName(model.Name));

					status = model.WriteVrdFile(vrdPathFileName);

					if (File.Exists(vrdPathFileName))
						theDecompiledVrdFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, vrdPathFileName));

					model.SourceModelProgress -= Model_SourceModelProgress;
				}
			}

			if (CancellationPending)
				status = AppEnums.StatusMessage.Canceled;
			else if (theSkipCurrentModelIsActive)
				status = AppEnums.StatusMessage.Skipped;

			return status;
		}

		private AppEnums.StatusMessage WriteDeclareSequenceQciFile(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			if (MainCROWBAR.TheApp.Settings.DecompileDeclareSequenceQciFileIsChecked)
			{
				if (model.HasBoneAnimationData)
				{
					UpdateProgress(3, "DeclareSequence QCI file: ");
					theDecompiledFileType = AppEnums.DecompiledFileType.DeclareSequenceQci;
					theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += Model_SourceModelProgress;

					string declareSequenceQciPathFileName = Path.Combine(theModelOutputPath, SourceFileNamesModule.GetDeclareSequenceQciFileName(model.Name));

					status = model.WriteDeclareSequenceQciFile(declareSequenceQciPathFileName);

					if (File.Exists(declareSequenceQciPathFileName))
						theDecompiledDeclareSequenceQciFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, declareSequenceQciPathFileName));

					model.SourceModelProgress -= Model_SourceModelProgress;
				}
			}

			if (CancellationPending)
				status = AppEnums.StatusMessage.Canceled;
			else if (theSkipCurrentModelIsActive)
				status = AppEnums.StatusMessage.Skipped;

			return status;
		}

		private AppEnums.StatusMessage WriteTextureFiles(SourceModel model)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			if (MainCROWBAR.TheApp.Settings.DecompileTextureBmpFilesIsChecked)
			{
				if (model.HasTextureFileData)
				{
					UpdateProgress(3, "Texture files: ");
					theDecompiledFileType = AppEnums.DecompiledFileType.TextureBmp;
					theFirstDecompiledFileHasBeenAdded = false;
					model.SourceModelProgress += Model_SourceModelProgress;

					status = model.WriteTextureFiles(theModelOutputPath);

					model.SourceModelProgress -= Model_SourceModelProgress;
				}
			}

			return status;
		}

		private void WriteDebugFiles(SourceModel model)
		{
			string debugPath = MainCROWBAR.TheApp.GetDebugPath(theModelOutputPath, model.Name);

			FileManager.CreatePath(debugPath);

			theDecompiledFileType = AppEnums.DecompiledFileType.Debug;
			theFirstDecompiledFileHasBeenAdded = false;
			model.SourceModelProgress += Model_SourceModelProgress;

			model.WriteAccessedBytesDebugFiles(debugPath);
			if (CancellationPending)
				return;
			else if (theSkipCurrentModelIsActive)
				return;

			model.SourceModelProgress -= Model_SourceModelProgress;
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

		private void UpdateProgress(int indentLevel, string line)
		{
			string indentedLine = string.Empty;
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

		#region Event Handlers
		private void Model_SourceModelProgress(object sender, SourceModelProgressEventArgs e)
		{
			if (e.Progress == AppEnums.ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum)
			{
				//TODO: Test that this shows when needed.
				UpdateProgress(4, "WARNING: The PHY file's checksum value does not match the MDL file's checksum value.");
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
				UpdateProgress(4, e.Message);
			else if (e.Progress == AppEnums.ProgressOptions.WritingFileFinished)
			{
				string pathFileName = e.Message;
				string fileName = Path.GetFileName(pathFileName);
				//Me.UpdateProgress(4, "... Writing """ + fileName + """ file finished.")
				UpdateProgress(4, fileName);

				if (!theFirstDecompiledFileHasBeenAdded && File.Exists(pathFileName))
				{
					string relativePathFileName = FileManager.GetRelativePathFileName(theOutputPath, pathFileName);
					if (theDecompiledFileType == AppEnums.DecompiledFileType.ReferenceMesh)
						theDecompiledFirstRefSmdFiles.Add(relativePathFileName);
					else if (theDecompiledFileType == AppEnums.DecompiledFileType.LodMesh)
						theDecompiledFirstLodSmdFiles.Add(relativePathFileName);
					else if (theDecompiledFileType == AppEnums.DecompiledFileType.BoneAnimation)
						theDecompiledFirstBoneAnimSmdFiles.Add(relativePathFileName);
					else if (theDecompiledFileType == AppEnums.DecompiledFileType.PhysicsMesh)
						theDecompiledPhysicsFiles.Add(relativePathFileName);
					else if (theDecompiledFileType == AppEnums.DecompiledFileType.TextureBmp)
						theDecompiledFirstTextureBmpFiles.Add(relativePathFileName);
					else if (theDecompiledFileType == AppEnums.DecompiledFileType.Debug)
						theDecompiledFirstDebugFiles.Add(relativePathFileName);

					theFirstDecompiledFileHasBeenAdded = true;
				}

				SourceModel model = (SourceModel)sender;
				if (CancellationPending)
					model.WritingIsCanceled = true;
			}
			#if DEBUG
			else
			{
				if(e.Progress != AppEnums.ProgressOptions.WritingFileStarted)
					Console.WriteLine(string.Format("Decompiler (Model_SourceModelProgress): ({0}) unhandled!", e.Progress));
			}
			#endif
		}
		#endregion
	}
}