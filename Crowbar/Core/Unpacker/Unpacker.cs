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
	public class Unpacker : BackgroundWorker
	{
#region Create and Destroy

		public Unpacker() : base()
		{

			theUnpackedMdlFiles = new BindingListEx<string>();
			theLogFiles = new BindingListEx<string>();
			theUnpackedPaths = new List<string>();
			theUnpackedRelativePathsAndFileNames = new BindingListEx<string>();
			theUnpackedTempPathsAndPathFileNames = new List<string>();

			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
			DoWork += Unpacker_DoWork;
		}

#endregion

#region Init and Free

		//Private Sub Init()
		//End Sub

		//Private Sub Free()
		//End Sub

#endregion

#region Properties

#endregion

#region Methods

		public void Run(AppEnums.ArchiveAction unpackerAction, SortedList<string, List<int>> archivePathFileNameToEntryIndexesMap, bool outputPathIsExtendedWithPackageName, string selectedRelativeOutputPath)
		{
			theSynchronousWorkerIsActive = false;
			UnpackerInputInfo info = new UnpackerInputInfo();
			info.theArchiveAction = unpackerAction;
			info.theArchivePathFileNameToEntryIndexesMap = archivePathFileNameToEntryIndexesMap;
			info.theOutputPathIsExtendedWithPackageName = outputPathIsExtendedWithPackageName;
			info.theSelectedRelativeOutputPath = selectedRelativeOutputPath;
			RunWorkerAsync(info);
		}

		public string RunSynchronous(AppEnums.ArchiveAction unpackerAction, SortedList<string, List<int>> archivePathFileNameToEntryIndexesMap, bool outputPathIsExtendedWithPackageName, string selectedRelativeOutputPath)
		{
			theSynchronousWorkerIsActive = true;
			UnpackerInputInfo info = new UnpackerInputInfo();
			info.theArchiveAction = unpackerAction;
			info.theArchivePathFileNameToEntryIndexesMap = archivePathFileNameToEntryIndexesMap;
			info.theOutputPathIsExtendedWithPackageName = outputPathIsExtendedWithPackageName;
			info.theSelectedRelativeOutputPath = selectedRelativeOutputPath;

			theRunSynchronousResultMessage = "";
			System.ComponentModel.DoWorkEventArgs e = new System.ComponentModel.DoWorkEventArgs(info);
			OnDoWork(e);
			return theRunSynchronousResultMessage;
		}

		public void UnpackFolderTreeFromVPK(string folderTreeToExtract)
		{
			theSynchronousWorkerIsActive = true;
			UnpackerInputInfo info = new UnpackerInputInfo();
			info.theArchiveAction = AppEnums.ArchiveAction.ExtractFolderTree;
			info.theGamePath = folderTreeToExtract;
			System.ComponentModel.DoWorkEventArgs e = new System.ComponentModel.DoWorkEventArgs(info);
			OnDoWork(e);
		}

		//Public Sub GetTempPathFileNames(ByVal packInternalPathFileNames As List(Of String), ByRef tempPathFileNames As List(Of String))
		//	tempPathFileNames = New List(Of String)()
		//	For Each packInternalPathFileName As String In packInternalPathFileNames
		//		tempPathFileNames.Add(Path.Combine(Me.theTempUnpackPaths(0), packInternalPathFileName))
		//	Next
		//End Sub
		//Public Function GetTempPathsAndPathFileNames(ByVal packInternalPathFileNames As List(Of String)) As List(Of String)
		//	Dim tempPathFileNames As List(Of String)

		//	tempPathFileNames = New List(Of String)()
		//	For Each packInternalPathFileName As String In packInternalPathFileNames
		//		tempPathFileNames.Add(Path.Combine(Me.theOutputPath, packInternalPathFileName))
		//	Next

		//	Return tempPathFileNames
		//End Function
		public List<string> GetTempRelativePathsAndFileNames()
		{
			List<string> tempRelativePathsAndFileNames = new List<string>();

			string topRelativePath = null;
			foreach (string relativePathOrFileName in theUnpackedRelativePathsAndFileNames)
			{
				topRelativePath = FileManager.GetTopFolderPath(relativePathOrFileName);
				if (string.IsNullOrEmpty(topRelativePath))
				{
					tempRelativePathsAndFileNames.Add(Path.Combine(theOutputPath, relativePathOrFileName));
				}
				else
				{
					tempRelativePathsAndFileNames.Add(Path.Combine(theOutputPath, topRelativePath));
				}
			}

			return tempRelativePathsAndFileNames;
		}

		public void SkipCurrentPackage()
		{
			//NOTE: This might have thread race condition, but it probably doesn't matter.
			theSkipCurrentPackIsActive = true;
		}

		public string GetOutputPathFileName(string relativePathFileName)
		{
			string pathFileName = Path.Combine(theOutputPath, relativePathFileName);

			pathFileName = Path.GetFullPath(pathFileName);

			return pathFileName;
		}

		public string GetOutputPathOrOutputFileName()
		{
			return theOutputPathOrModelOutputFileName;
		}

		public void DeleteTempUnpackFolder()
		{
			if (theUnpackedPathsAreInTempPath)
			{
				theUnpackedPathsAreInTempPath = false;
				try
				{
					foreach (string unpackedPath in theUnpackedPaths)
					{
						if (unpackedPath != null && Directory.Exists(unpackedPath))
						{
							Directory.Delete(unpackedPath, true);
						}
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

#endregion

#region Private Methods

#endregion

#region Private Methods in Background Thread

		private void Unpacker_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			if (!theSynchronousWorkerIsActive)
			{
				//TODO: This indication that work has started in backgroundworker seems unimportant and should probably be removed.
				ReportProgress(0, "");
			}

			UnpackerInputInfo info = (UnpackerInputInfo)e.Argument;
			theOutputPathIsExtendedWithPackageName = info.theOutputPathIsExtendedWithPackageName;
			theSelectedRelativeOutputPath = info.theSelectedRelativeOutputPath;

			theUnpackedPathsAreInTempPath = false;

			AppEnums.StatusMessage status = 0;
			if (info.theArchiveAction == AppEnums.ArchiveAction.ExtractFolderTree)
			{
				status = ExtractFolderTree(info.theGamePath);
			}
			else
			{
				if (UnpackerInputsAreValid())
				{
					if (info.theArchiveAction == AppEnums.ArchiveAction.List)
					{
						List();
					}
					else if (info.theArchiveAction == AppEnums.ArchiveAction.Unpack)
					{
						status = Unpack(info.theArchivePathFileNameToEntryIndexesMap);
						//ElseIf info.theArchiveAction = ArchiveAction.Extract Then
						//	status = Me.Extract(info.theArchivePathFileNameToEntryIndexesMap)
					}
					else if (info.theArchiveAction == AppEnums.ArchiveAction.ExtractAndOpen)
					{
						status = ExtractWithoutLogging(info.theArchivePathFileNameToEntryIndexesMap);
						if (status == AppEnums.StatusMessage.Success)
						{
							StartFile(Path.Combine(theOutputPath, theUnpackedRelativePathsAndFileNames[0]));
						}
					}
					else if (info.theArchiveAction == AppEnums.ArchiveAction.ExtractToTemp)
					{
						status = ExtractWithoutLogging(info.theArchivePathFileNameToEntryIndexesMap);
					}
				}
				else
				{
					status = AppEnums.StatusMessage.Error;
				}

				e.Result = GetUnpackerOutputInfo(status);

				if (CancellationPending)
				{
					e.Cancel = true;
				}
			}
		}

		private string GetOutputPath()
		{
			string outputPath = null;

			if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.SameFolder)
			{
				outputPath = MainCROWBAR.TheApp.Settings.UnpackOutputSamePath;
			}
			else if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.Subfolder)
			{
				if (File.Exists(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
				{
					outputPath = Path.Combine(FileManager.GetPath(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName), MainCROWBAR.TheApp.Settings.UnpackOutputSubfolderName);
				}
				else if (Directory.Exists(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
				{
					outputPath = Path.Combine(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName, MainCROWBAR.TheApp.Settings.UnpackOutputSubfolderName);
				}
				else
				{
					outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
			else
			{
				outputPath = MainCROWBAR.TheApp.Settings.UnpackOutputFullPath;
			}

			//This will change a relative path to an absolute path.
			outputPath = Path.GetFullPath(outputPath);
			return outputPath;
		}

		private bool UnpackerInputsAreValid()
		{
			bool inputsAreValid = true;

			if (string.IsNullOrEmpty(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "Package file or folder has not been selected.");
			}
			else if (MainCROWBAR.TheApp.Settings.UnpackMode == AppEnums.InputOptions.File && !File.Exists(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
			{
				inputsAreValid = false;
				WriteErrorMessage(1, "The package file, \"" + MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName + "\", does not exist.");
			}

			return inputsAreValid;
		}

		private UnpackerOutputInfo GetUnpackerOutputInfo(AppEnums.StatusMessage status)
		{
			UnpackerOutputInfo unpackResultInfo = new UnpackerOutputInfo();

			unpackResultInfo.theStatus = status;

			if (theUnpackedMdlFiles.Count > 0)
			{
				unpackResultInfo.theUnpackedRelativePathFileNames = theUnpackedMdlFiles;
			}
			else if (theUnpackedRelativePathsAndFileNames.Count > 0)
			{
				unpackResultInfo.theUnpackedRelativePathFileNames = theUnpackedRelativePathsAndFileNames;
			}
			else if (MainCROWBAR.TheApp.Settings.UnpackLogFileIsChecked)
			{
				unpackResultInfo.theUnpackedRelativePathFileNames = theLogFiles;
			}
			else
			{
				unpackResultInfo.theUnpackedRelativePathFileNames = null;
			}

			if (unpackResultInfo.theUnpackedRelativePathFileNames == null || unpackResultInfo.theUnpackedRelativePathFileNames.Count <= 0 || theUnpackedMdlFiles.Count <= 0)
			{
				theOutputPathOrModelOutputFileName = "";
			}
			else if (unpackResultInfo.theUnpackedRelativePathFileNames.Count == 1)
			{
				theOutputPathOrModelOutputFileName = Path.Combine(theOutputPath, unpackResultInfo.theUnpackedRelativePathFileNames[0]);
			}
			else
			{
				theOutputPathOrModelOutputFileName = theOutputPath;
			}

			return unpackResultInfo;
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

		private AppEnums.StatusMessage CreateLogTextFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (MainCROWBAR.TheApp.Settings.UnpackLogFileIsChecked)
			{
				string logPath = null;
				string logFileName = null;
				string logPathFileName = null;

				try
				{
					logPath = theOutputPath;
					//logFileName = vpkPathFileName + " " + My.Resources.Unpack_LogFileNameSuffix
					logFileName = Properties.Resources.Unpack_LogFileNameSuffix;
					FileManager.CreatePath(logPath);
					logPathFileName = Path.Combine(logPath, logFileName);

					theLogFileStream = File.CreateText(logPathFileName);
					theLogFileStream.AutoFlush = true;

					if (File.Exists(logPathFileName))
					{
						theLogFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, logPathFileName));
					}

					theLogFileStream.WriteLine("// " + MainCROWBAR.TheApp.GetHeaderComment());
					theLogFileStream.Flush();
				}
				catch (Exception ex)
				{
					UpdateProgress();
					UpdateProgress(2, "ERROR: Crowbar tried to write the unpack log file but the system gave this message: " + ex.Message);
					status = AppEnums.StatusMessage.Error;
				}
			}
			else
			{
				theLogFileStream = null;
			}

			return status;
		}

		private void UpdateProgressInternal(int progressValue, string line)
		{
			if (progressValue == 1 && theLogFileStream != null)
			{
				theLogFileStream.WriteLine(line);
				theLogFileStream.Flush();
			}

			if (!theSynchronousWorkerIsActive)
			{
				ReportProgress(progressValue, line);
			}
		}

		private AppEnums.StatusMessage ExtractFolderTree(string gamePath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			// Example: 
			//      Me.theGamePath = gamePath = "E:\Users\ZeqMacaw\Steam\steamapps\common\Half-Life 2\hl2"
			//      gameRootPath              = "E:\Users\ZeqMacaw\Steam\steamapps\common\Half-Life 2"
			theGamePath = gamePath;
			string gameRootPath = FileManager.GetPath(gamePath);

			try
			{
				ExtractFolderTreeFromArchivesInFolderRecursively(gameRootPath);
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
			}

			return status;
		}

		private AppEnums.StatusMessage ExtractFolderTreeFromArchivesInFolderRecursively(string packagePath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			ExtractFolderTreeFromArchivesInFolder(packagePath);

			try
			{
				foreach (string aPathSubFolder in Directory.GetDirectories(packagePath))
				{
					ExtractFolderTreeFromArchivesInFolderRecursively(aPathSubFolder);

					if (CancellationPending)
					{
						return AppEnums.StatusMessage.Canceled;
					}
				}
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
			}

			return status;
		}

		private AppEnums.StatusMessage ExtractFolderTreeFromArchivesInFolder(string packagePath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				//NOTE: Feature only valid for VPK files.
				string packageFileNameFilter = "*.vpk";
				foreach (string aPackagePathFileName in Directory.GetFiles(packagePath, packageFileNameFilter))
				{
					ExtractFolderTreeFromArchive(aPackagePathFileName);

					if (CancellationPending)
					{
						return AppEnums.StatusMessage.Canceled;
					}
				}
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
			}

			return status;
		}

		private AppEnums.StatusMessage ExtractFolderTreeFromArchive(string packagePathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			BasePackageFileData aPackageFileData = null;
			//aVpkFileData = New BasePackageFileData()

			FileStream inputFileStream = null;
			theInputFileReader = null;
			try
			{
				inputFileStream = new FileStream(packagePathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				if (inputFileStream != null)
				{
					try
					{
						theInputFileReader = new BinaryReader(inputFileStream, System.Text.Encoding.ASCII);

						//Dim vpkFile As New VpkFile(Me.theInputFileReader, aVpkFileData)
						BasePackageFile vpkFile = BasePackageFile.Create(packagePathFileName, theArchiveDirectoryInputFileReader, theInputFileReader, ref aPackageFileData);

						vpkFile.ReadHeader();
						vpkFile.ReadEntries(this);
					}
					catch (Exception ex)
					{
						throw;
					}
					finally
					{
						if (theInputFileReader != null)
						{
							theInputFileReader.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
				throw;
			}
			finally
			{
				if (inputFileStream != null)
				{
					inputFileStream.Close();
				}
			}

			if (CancellationPending)
			{
				return AppEnums.StatusMessage.Canceled;
			}

			if (aPackageFileData != null && aPackageFileData.IsSourcePackage)
			{
				BasePackageDirectoryEntry entry = null;
				string line = null;
				string archivePathFileName = null;
				string vpkPath = null;
				string vpkFileNameWithoutExtension = null;
				string vpkFileNamePrefix = null;
				List<string> paths = new List<string>();

				//Me.UpdateProgressInternal(1, "")
				for (int i = 0; i < aPackageFileData.theEntries.Count; i++)
				{
					entry = aPackageFileData.theEntries[i];
					if (entry.archiveIndex != 0x7FFF)
					{
						vpkPath = FileManager.GetPath(packagePathFileName);
						vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(packagePathFileName);
						vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf(aPackageFileData.DirectoryFileNameSuffix));
						archivePathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + aPackageFileData.FileExtension);
					}
					else
					{
						archivePathFileName = packagePathFileName;
					}

					line = aPackageFileData.theEntryDataOutputTexts[i];

					//Example output:
					//addonimage.jpg crc=0x50ea4a15 metadatasz=0 fnumber=32767 ofs=0x0 sz=10749
					//materials/models/weapons/melee/crowbar_normal.vtf crc=0x7ac0e054 metadatasz=0 fnumber=32767 ofs=0x2fed8 sz=1398196

					string[] fields = line.Split(' ');

					string pathFileName = fields[0];
					//NOTE: The last 5 fields should not have any spaces, but the path+filename field might.
					for (int fieldIndex = 1; fieldIndex <= fields.Length - 6; fieldIndex++)
					{
						pathFileName = pathFileName + " " + fields[fieldIndex];
					}

					//NOTE: Only need to create "models" folder-tree to have models accessible in HLMV.
					if (pathFileName.StartsWith("models/"))
					{
						string aRelativePath = FileManager.GetPath(pathFileName);
						string aPath = Path.Combine(theGamePath, aRelativePath);
						if (!FileManager.PathExistsAfterTryToCreate(aPath))
						{
							//TODO: [ExtractFolderTreeFromArchive] Path was not created, so warn user.
							//Me.UpdateProgressInternal(1, "")
							int debug = 4242;
						}
					}
				}
			}

			return status;
		}

		private void List()
		{
			string archivePathFileName = null;
			string archivePath = "";
			archivePathFileName = MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName;
			if (File.Exists(archivePathFileName))
			{
				archivePath = FileManager.GetPath(archivePathFileName);
			}
			else if (Directory.Exists(archivePathFileName))
			{
				archivePath = archivePathFileName;
			}

			if (string.IsNullOrEmpty(archivePath))
			{
				return;
			}

			theArchivePathFileNameToFileDataMap = new SortedList<string, BasePackageFileData>();

			try
			{
				if (MainCROWBAR.TheApp.Settings.UnpackMode == AppEnums.InputOptions.FolderRecursion)
				{
					ListArchivesInFolderRecursively(archivePath);
				}
				else if (MainCROWBAR.TheApp.Settings.UnpackMode == AppEnums.InputOptions.Folder)
				{
					ListArchivesInFolder(archivePath);
				}
				else
				{
					ListArchive(archivePathFileName, true);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ListArchivesInFolderRecursively(string archivePath)
		{
			ListArchivesInFolder(archivePath);

			try
			{
				foreach (string aPathSubFolder in Directory.GetDirectories(archivePath))
				{
					ListArchivesInFolderRecursively(aPathSubFolder);

					if (CancellationPending)
					{
						return;
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ListArchivesInFolder(string archivePath)
		{
			try
			{
				List<string> packageExtensions = BasePackageFile.GetListOfPackageExtensions();
				foreach (string packageExtension in packageExtensions)
				{
					foreach (string anArchivePathFileName in Directory.GetFiles(archivePath, packageExtension))
					{
						ListArchive(anArchivePathFileName, false);

						if (CancellationPending)
						{
							return;
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		//checkForDirFile = true: Check if package file is valid. If not, check for a directory package file in same folder and open that instead.
		private void ListArchive(string packageDirectoryPathFileName, bool checkForDirFile)
		{
			BasePackageFileData aPackageFileData = null;

			FileStream inputFileStream = null;
			theInputFileReader = null;
			bool loopingIsNeeded = true;
			while (loopingIsNeeded)
			{
				try
				{
					inputFileStream = new FileStream(packageDirectoryPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					if (inputFileStream != null)
					{
						try
						{
							theInputFileReader = new BinaryReader(inputFileStream, System.Text.Encoding.ASCII);

							BasePackageFile packageFile = BasePackageFile.Create(packageDirectoryPathFileName, theArchiveDirectoryInputFileReader, theInputFileReader, ref aPackageFileData);

							packageFile.ReadHeader();
							if (aPackageFileData != null && aPackageFileData.IsSourcePackage)
							{
								thePackageDirectoryPathFileName = packageDirectoryPathFileName;
								thePackageFileData = aPackageFileData;
								UpdateProgressInternal(1, "");
								packageFile.PackEntryRead += Package_PackEntryRead;
								packageFile.ReadEntries(this);
								packageFile.PackEntryRead -= Package_PackEntryRead;
								loopingIsNeeded = false;
							}
							else if (checkForDirFile && Path.GetExtension(packageDirectoryPathFileName) == ".vpk")
							{
								//NOTE: Reaches this when user tries to list from a VPK file that is part of a multi-file package, but it is not the "dir" file.
								//NOTE: Set this to false to only check once for a package directory file.
								checkForDirFile = false;

								string tempPath = FileManager.GetPath(packageDirectoryPathFileName);
								string tempFileName = Path.GetFileNameWithoutExtension(packageDirectoryPathFileName);
								int pos = tempFileName.LastIndexOf("_");
								if (pos >= 0)
								{
									string dirFileName = tempFileName.Remove(pos + 1) + "dir.vpk";
									packageDirectoryPathFileName = Path.Combine(tempPath, dirFileName);
								}
								else
								{
									loopingIsNeeded = false;
								}
							}
							else
							{
								loopingIsNeeded = false;
							}
						}
						catch (Exception ex)
						{
							throw;
						}
						finally
						{
							if (theInputFileReader != null)
							{
								theInputFileReader.Close();
							}
						}
					}
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					if (inputFileStream != null)
					{
						inputFileStream.Close();
					}
				}

				if (CancellationPending)
				{
					return;
				}
			}

			//If aPackageFileData IsNot Nothing AndAlso aPackageFileData.IsSourcePackage Then
			//	Dim entry As BasePackageDirectoryEntry
			//	Dim line As String
			//	Dim archivePathFileName As String
			//	Dim vpkPath As String
			//	Dim vpkFileNameWithoutExtension As String
			//	Dim vpkFileNamePrefix As String

			//	Me.UpdateProgressInternal(1, "")
			//	For i As Integer = 0 To aPackageFileData.theEntries.Count - 1
			//		entry = aPackageFileData.theEntries(i)
			//		If entry.archiveIndex <> &H7FFF Then
			//			vpkPath = FileManager.GetPath(packageDirectoryPathFileName)
			//			vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(packageDirectoryPathFileName)
			//			vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf(aPackageFileData.DirectoryFileNameSuffix))
			//			archivePathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + aPackageFileData.FileExtension)
			//		Else
			//			archivePathFileName = packageDirectoryPathFileName
			//		End If
			//		If Not Me.theArchivePathFileNameToFileDataMap.Keys.Contains(archivePathFileName) Then
			//			Me.theArchivePathFileNameToFileDataMap.Add(archivePathFileName, aPackageFileData)
			//		End If
			//		Me.UpdateProgressInternal(2, archivePathFileName)

			//		line = aPackageFileData.theEntryDataOutputTexts(i)
			//		Me.UpdateProgressInternal(3, line)

			//		If Me.CancellationPending Then
			//			Return
			//		End If
			//	Next
			//End If
		}

		private void Package_PackEntryRead(object sender, SourcePackageEventArgs e)
		{
			BasePackageDirectoryEntry entry = null;
			string line = null;
			string archivePathFileName = null;
			string vpkPath = null;
			string vpkFileNameWithoutExtension = null;
			string vpkFileNamePrefix = null;

			entry = e.Entry;
			if (entry.archiveIndex != 0x7FFF)
			{
				vpkPath = FileManager.GetPath(thePackageDirectoryPathFileName);
				vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(thePackageDirectoryPathFileName);
				vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf(thePackageFileData.DirectoryFileNameSuffix));
				archivePathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + thePackageFileData.FileExtension);
			}
			else
			{
				archivePathFileName = thePackageDirectoryPathFileName;
			}
			if (!theArchivePathFileNameToFileDataMap.Keys.Contains(archivePathFileName))
			{
				theArchivePathFileNameToFileDataMap.Add(archivePathFileName, thePackageFileData);
			}
			UpdateProgressInternal(2, archivePathFileName);
			if (File.Exists(archivePathFileName))
			{
				UpdateProgressInternal(3, "True");
			}
			else
			{
				UpdateProgressInternal(3, "False");
			}

			line = e.EntryDataOutputText;
			UpdateProgressInternal(4, line);

			if (CancellationPending)
			{
				return;
			}
		}

		private AppEnums.StatusMessage Unpack(SortedList<string, List<int>> archivePathFileNameToEntryIndexMap)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			theSkipCurrentPackIsActive = false;

			theUnpackedPaths.Clear();
			theUnpackedRelativePathsAndFileNames.Clear();
			theUnpackedMdlFiles.Clear();
			theLogFiles.Clear();

			theOutputPath = GetOutputPath();
			string vpkPathFileName = MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName;
			if (File.Exists(vpkPathFileName))
			{
				theInputVpkPath = FileManager.GetPath(vpkPathFileName);
			}
			else if (Directory.Exists(vpkPathFileName))
			{
				theInputVpkPath = vpkPathFileName;
			}

			string progressDescriptionText = "Unpacking with " + MainCROWBAR.TheApp.GetProductNameAndVersion() + ": ";

			try
			{
				//If TheApp.Settings.UnpackMode = InputOptions.FolderRecursion Then
				//	progressDescriptionText += """" + Me.theInputVpkPath + """ (folder + subfolders)"
				//	Me.UpdateProgressStart(progressDescriptionText + " ...")

				//	status = Me.CreateLogTextFile("")

				//	Me.UnpackArchivesInFolderRecursively(Me.theInputVpkPath)
				//ElseIf TheApp.Settings.UnpackMode = InputOptions.Folder Then
				//	progressDescriptionText += """" + Me.theInputVpkPath + """ (folder)"
				//	Me.UpdateProgressStart(progressDescriptionText + " ...")

				//	status = Me.CreateLogTextFile("")

				//	Me.UnpackArchivesInFolder(Me.theInputVpkPath)
				//Else
				//	'vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
				//	progressDescriptionText += """" + vpkPathFileName + """"
				//	Me.UpdateProgressStart(progressDescriptionText + " ...")
				//	'Me.UnpackArchive(vpkPathFileName)
				//	Me.ExtractFromArchive(vpkPathFileName, Nothing)
				//End If
				//------
				progressDescriptionText += "\"" + vpkPathFileName + "\"";
				UpdateProgressStart(progressDescriptionText + " ...");
				ExtractFromArchive(vpkPathFileName, archivePathFileNameToEntryIndexMap);
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
			}

			if (CancellationPending)
			{
				UpdateProgressStop("... " + progressDescriptionText + " canceled.");
			}
			else
			{
				UpdateProgressStop("... " + progressDescriptionText + " finished.");
			}

			return status;
		}

		//Private Function Extract(ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		//	Dim status As AppEnums.StatusMessage = StatusMessage.Success

		//	Me.theSkipCurrentPackIsActive = False

		//	Me.theUnpackedPaths.Clear()
		//	Me.theUnpackedRelativePathFileNames.Clear()
		//	Me.theUnpackedMdlFiles.Clear()
		//	Me.theLogFiles.Clear()

		//	Me.theOutputPath = Me.GetAdjustedOutputPath()
		//	Dim vpkPathFileName As String
		//	vpkPathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName
		//	If File.Exists(vpkPathFileName) Then
		//		Me.theInputVpkPath = FileManager.GetPath(vpkPathFileName)
		//	ElseIf Directory.Exists(vpkPathFileName) Then
		//		Me.theInputVpkPath = vpkPathFileName
		//	End If

		//	Dim progressDescriptionText As String
		//	progressDescriptionText = "Unpacking with " + TheApp.GetProductNameAndVersion() + ": "

		//	Try
		//		'vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
		//		progressDescriptionText += """" + vpkPathFileName + """"
		//		Me.UpdateProgressStart(progressDescriptionText + " ...")
		//		'Me.ExtractFromArchive(vpkPathFileName, entries)
		//		Me.ExtractFromArchive(vpkPathFileName, archivePathFileNameToEntryIndexMap)
		//	Catch ex As Exception
		//		status = StatusMessage.Error
		//	End Try

		//	If Me.CancellationPending Then
		//		Me.UpdateProgressStop("... " + progressDescriptionText + " canceled.")
		//	Else
		//		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
		//	End If

		//	Return status
		//End Function

		private AppEnums.StatusMessage ExtractWithoutLogging(SortedList<string, List<int>> archivePathFileNameToEntryIndexMap)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			theUnpackedPaths.Clear();
			theUnpackedRelativePathsAndFileNames.Clear();
			theUnpackedTempPathsAndPathFileNames.Clear();

			// Create and add a folder to the Temp path, to prevent potential file collisions and to provide user more obvious folder name.
			Guid guid = new Guid();
			guid = Guid.NewGuid();
			string folder = "Crowbar_" + guid.ToString();
			theOutputPath = Path.Combine(Path.GetTempPath(), folder);
			theUnpackedPathsAreInTempPath = true;
			if (!FileManager.PathExistsAfterTryToCreate(theOutputPath))
			{
				theRunSynchronousResultMessage = "WARNING: Tried to create \"" + theOutputPath + "\" needed for extracting, but Windows did not allow it.";
				status = AppEnums.StatusMessage.ErrorUnableToCreateTempFolder;
				return status;
			}

			//Dim vpkPathFileName As String
			//vpkPathFileName = TheApp.Settings.UnpackPackagePathFolderOrFileName

			try
			{
				string archivePathFileName = null;
				List<int> archiveEntryIndexes = null;

				theArchiveDirectoryFileNamePrefix = "";
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string vpkPath = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string vpkFileName = null;
				for (int i = 0; i < archivePathFileNameToEntryIndexMap.Count; i++)
				{
					archivePathFileName = archivePathFileNameToEntryIndexMap.Keys[i];
					archiveEntryIndexes = archivePathFileNameToEntryIndexMap.Values[i];

	//				Dim vpkPath As String
	//				Dim vpkFileName As String
					vpkPath = FileManager.GetPath(archivePathFileName);
					vpkFileName = Path.GetFileName(archivePathFileName);

					OpenArchiveDirectoryFile(theArchivePathFileNameToFileDataMap[archivePathFileName], archivePathFileName);
					DoUnpackAction(theArchivePathFileNameToFileDataMap[archivePathFileName], vpkPath, vpkFileName, archiveEntryIndexes);
				}
				if (!string.IsNullOrEmpty(theArchiveDirectoryFileNamePrefix))
				{
					CloseArchiveDirectoryFile();
				}
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
			}

			return status;
		}

		//Private Sub UnpackArchivesInFolderRecursively(ByVal archivePath As String)
		//	Me.UnpackArchivesInFolder(archivePath)

		//	For Each aPathSubFolder As String In Directory.GetDirectories(archivePath)
		//		Me.UnpackArchivesInFolderRecursively(aPathSubFolder)
		//		If Me.CancellationPending Then
		//			Return
		//		End If
		//	Next
		//End Sub

		//Private Sub UnpackArchivesInFolder(ByVal archivePath As String)
		//	For Each anArchivePathFileName As String In Directory.GetFiles(archivePath, "*.vpk")
		//		'Me.UnpackArchive(anArchivePathFileName)
		//		Me.ExtractFromArchive(anArchivePathFileName, Nothing)

		//		If Not Me.theSynchronousWorkerIsActive Then
		//			'TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
		//			Me.ReportProgress(5, "")
		//		End If

		//		If Me.CancellationPending Then
		//			Return
		//		ElseIf Me.theSkipCurrentPackIsActive Then
		//			Me.theSkipCurrentPackIsActive = False
		//			Continue For
		//		End If
		//	Next
		//End Sub

		//Private Sub UnpackArchive(ByVal archivePathFileName As String)
		//	Try
		//		Dim vpkPath As String
		//		Dim vpkFileName As String
		//		Dim vpkRelativePath As String
		//		Dim vpkRelativePathFileName As String
		//		vpkPath = FileManager.GetPath(archivePathFileName)
		//		vpkFileName = Path.GetFileName(archivePathFileName)
		//		vpkRelativePath = FileManager.GetRelativePathFileName(Me.theInputVpkPath, FileManager.GetPath(archivePathFileName))
		//		vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName)

		//		Dim vpkName As String
		//		vpkName = Path.GetFileNameWithoutExtension(archivePathFileName)

		//		Me.CreateLogTextFile(vpkName)

		//		Me.UpdateProgress()
		//		Me.UpdateProgress(1, "Unpacking """ + vpkRelativePathFileName + """ ...")

		//		Me.DoUnpackAction(vpkPath, vpkFileName, Nothing)

		//		If Me.CancellationPending Then
		//			Me.UpdateProgress(1, "... Unpacking """ + vpkRelativePathFileName + """ canceled. Check above for any errors.")
		//		Else
		//			Me.UpdateProgress(1, "... Unpacking """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
		//		End If
		//	Catch ex As Exception
		//		Dim debug As Integer = 4242
		//	Finally
		//		If Me.theLogFileStream IsNot Nothing Then
		//			Me.theLogFileStream.Flush()
		//			Me.theLogFileStream.Close()
		//			Me.theLogFileStream = Nothing
		//		End If
		//	End Try
		//End Sub

		private AppEnums.StatusMessage ExtractFromArchive(string archiveDirectoryPathFileName, SortedList<string, List<int>> archivePathFileNameToEntryIndexMap)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				string vpkPath = null;
				string vpkFileName = null;
				string vpkRelativePath = null;
				string vpkRelativePathFileName = null;
				//vpkPath = FileManager.GetPath(archiveDirectoryPathFileName)
				vpkFileName = Path.GetFileName(archiveDirectoryPathFileName);
				vpkRelativePath = FileManager.GetRelativePathFileName(theInputVpkPath, FileManager.GetPath(archiveDirectoryPathFileName));
				vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName);

				string vpkName = Path.GetFileNameWithoutExtension(archiveDirectoryPathFileName);

				string vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName);
				//Dim extractPath As String
				//extractPath = Path.Combine(Me.theOutputPath, vpkFileNameWithoutExtension)

				//Me.CreateLogTextFile(vpkName)
				status = CreateLogTextFile();

				UpdateProgress();
				//Me.UpdateProgress(1, "Unpacking from """ + vpkRelativePathFileName + """ to """ + extractPath + """ ...")
				UpdateProgress(1, "Unpacking from \"" + vpkRelativePathFileName + "\" to \"" + theOutputPath + "\" ...");

				//Me.DoUnpackAction(Me.theVpkFileDatas.Values(0), vpkPath, vpkFileName, entryIndexes)
				//======
				//If archivePathFileNameToEntryIndexMap Is Nothing Then
				//	vpkPath = FileManager.GetPath(archiveDirectoryPathFileName)
				//	Me.DoUnpackAction(Me.theArchivePathFileNameToFileDataMap(archiveDirectoryPathFileName), vpkPath, vpkFileName, Nothing)
				//Else
				string archivePathFileName = null;
				List<int> archiveEntryIndexes = null;

				theArchiveDirectoryFileNamePrefix = "";
				for (int i = 0; i < archivePathFileNameToEntryIndexMap.Count; i++)
				{
					archivePathFileName = archivePathFileNameToEntryIndexMap.Keys[i];
					archiveEntryIndexes = archivePathFileNameToEntryIndexMap.Values[i];

					vpkPath = FileManager.GetPath(archivePathFileName);
					vpkFileName = Path.GetFileName(archivePathFileName);

					OpenArchiveDirectoryFile(theArchivePathFileNameToFileDataMap[archivePathFileName], archivePathFileName);
					DoUnpackAction(theArchivePathFileNameToFileDataMap[archivePathFileName], vpkPath, vpkFileName, archiveEntryIndexes);
				}
				if (!string.IsNullOrEmpty(theArchiveDirectoryFileNamePrefix))
				{
					CloseArchiveDirectoryFile();
				}
				//End If

				if (CancellationPending)
				{
					UpdateProgress(1, "... Unpacking from \"" + vpkRelativePathFileName + "\" canceled. Check above for any errors.");
				}
				else
				{
					UpdateProgress(1, "... Unpacking from \"" + vpkRelativePathFileName + "\" finished. Check above for any errors.");
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

			return status;
		}

		private void OpenArchiveDirectoryFile(BasePackageFileData vpkFileData, string archivePathFileName)
		{
			if (vpkFileData == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(vpkFileData.DirectoryFileNameSuffix))
			{
				return;
			}

			string archiveDirectoryPathFileName = null;
			string vpkFileNameWithoutExtension = null;
			string vpkFileNamePrefix = null;
			int underscoreIndex = 0;

			vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(archivePathFileName);
			underscoreIndex = vpkFileNameWithoutExtension.LastIndexOf("_");
			if (underscoreIndex >= 0)
			{
				vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, underscoreIndex);
			}
			else
			{
				vpkFileNamePrefix = "";
			}

			if (vpkFileNamePrefix != theArchiveDirectoryFileNamePrefix)
			{
				CloseArchiveDirectoryFile();

				try
				{
					theArchiveDirectoryFileNamePrefix = vpkFileNamePrefix;

					string vpkPath = FileManager.GetPath(archivePathFileName);
					archiveDirectoryPathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + vpkFileData.DirectoryFileNameSuffixWithExtension);

					if (File.Exists(archiveDirectoryPathFileName))
					{
						theArchiveDirectoryInputFileStream = new FileStream(archiveDirectoryPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						if (theArchiveDirectoryInputFileStream != null)
						{
							try
							{
								theArchiveDirectoryInputFileReader = new BinaryReader(theArchiveDirectoryInputFileStream, System.Text.Encoding.ASCII);
							}
							catch (Exception ex)
							{
								throw;
							}
						}
					}
				}
				catch (Exception ex)
				{
					CloseArchiveDirectoryFile();
					throw;
				}
			}
		}

		private void CloseArchiveDirectoryFile()
		{
			if (theArchiveDirectoryInputFileReader != null)
			{
				theArchiveDirectoryInputFileReader.Close();
				theArchiveDirectoryInputFileReader = null;
			}
			if (theArchiveDirectoryInputFileStream != null)
			{
				theArchiveDirectoryInputFileStream.Close();
				theArchiveDirectoryInputFileStream = null;
			}
		}

		private void DoUnpackAction(BasePackageFileData vpkFileData, string vpkPath, string vpkFileName, List<int> entryIndexes)
		{
			if (vpkFileData == null)
			{
				return;
			}

			//Dim currentPath As String
			//currentPath = Directory.GetCurrentDirectory()
			//Directory.SetCurrentDirectory(vpkPath)

			List<BasePackageDirectoryEntry> entries = null;
			if (entryIndexes == null)
			{
				entries = new List<BasePackageDirectoryEntry>(vpkFileData.theEntries.Count);
				foreach (BasePackageDirectoryEntry entry in vpkFileData.theEntries)
				{
					entries.Add(entry);
				}
			}
			else
			{
				entries = new List<BasePackageDirectoryEntry>(entryIndexes.Count);
				foreach (int entryIndex in entryIndexes)
				{
					entries.Add(vpkFileData.theEntries[entryIndex]);
				}
			}

			string vpkPathFileName = Path.Combine(vpkPath, vpkFileName);
			string vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName);
			//Dim extractPath As String
			//extractPath = Path.Combine(Me.theOutputPath, vpkFileNameWithoutExtension)
			//'TODO: Make this a unique folder so that its name is extremely unlikely to interfere with existing temp folders; maybe use a GUID at end. 
			//If Not Me.theTempUnpackPaths.Contains(extractPath) Then
			//	Me.theTempUnpackPaths.Add(extractPath)
			//End If

			//If vpkFileNameWithoutExtension.EndsWith("_dir") Then
			//	Dim vpkFileNameWithoutIndex As String
			//	vpkFileNameWithoutIndex = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf("_dir"))

			//	For Each entry As VpkDirectoryEntry In entries
			//		If entry.archiveIndex <> &H7FFF Then
			//			vpkPathFileName = Path.Combine(vpkPath, vpkFileNameWithoutIndex + "_" + entry.archiveIndex.ToString("000") + ".vpk")
			//		End If
			//		Me.UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries, extractPath)
			//	Next
			//Else
			//Me.UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries, extractPath)
			UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries);
			//End If

			//Directory.SetCurrentDirectory(currentPath)
		}

		//Private Sub UnpackEntryDatasToFiles(ByVal vpkFileData As BasePackageFileData, ByVal vpkPathFileName As String, ByVal entries As List(Of VpkDirectoryEntry), ByVal extractPath As String)
		private void UnpackEntryDatasToFiles(BasePackageFileData vpkFileData, string vpkPathFileName, List<BasePackageDirectoryEntry> entries)
		{
			// Example: [03-Nov-2019] Left 4 Dead main multi-file VPK does not have a "pak01_048.vpk" file.
			if (!File.Exists(vpkPathFileName))
			{
				UpdateProgress(2, "WARNING: Package file not found - \"" + vpkPathFileName + "\". The following files are indicated as being in the missing package file: ");
				foreach (BasePackageDirectoryEntry entry in entries)
				{
					UpdateProgress(3, "\"" + entry.thePathFileName + "\"");
				}
				return;
			}

			FileStream inputFileStream = null;
			theInputFileReader = null;

			try
			{
				inputFileStream = new FileStream(vpkPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				if (inputFileStream != null)
				{
					try
					{
						theInputFileReader = new BinaryReader(inputFileStream, System.Text.Encoding.ASCII);

						//Dim packageDirectoryFileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(vpkPathFileName)
						string packageDirectoryFileNameWithoutExtension = GetPackageDirectoryFileNameWithoutExtension(vpkPathFileName, vpkFileData);

						//Dim vpkFile As New VpkFile(Me.theArchiveDirectoryInputFileReader, Me.theInputFileReader, vpkFileData)
						BasePackageFile vpkFile = BasePackageFile.Create(vpkPathFileName, theArchiveDirectoryInputFileReader, theInputFileReader, ref vpkFileData);

						foreach (BasePackageDirectoryEntry entry in entries)
						{
							//Me.UnpackEntryDataToFile(vpkFile, entry, extractPath)
							UnpackEntryDataToFile(packageDirectoryFileNameWithoutExtension, vpkFile, entry);

							if (CancellationPending)
							{
								break;
							}
						}
					}
					catch (Exception ex)
					{
						throw;
					}
					finally
					{
						if (theInputFileReader != null)
						{
							theInputFileReader.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (inputFileStream != null)
				{
					inputFileStream.Close();
				}
			}
		}

		//Private Sub UnpackEntryDataToFile(ByVal vpkFile As VpkFile, ByVal entry As VpkDirectoryEntry, ByVal extractPath As String)
		private void UnpackEntryDataToFile(string packageFileNameWithoutExtension, BasePackageFile vpkFile, BasePackageDirectoryEntry entry)
		{
			string outputPathStart = null;
			if (MainCROWBAR.TheApp.Settings.UnpackFolderForEachPackageIsChecked || theOutputPathIsExtendedWithPackageName)
			{
				outputPathStart = Path.Combine(theOutputPath, packageFileNameWithoutExtension);
			}
			else
			{
				outputPathStart = theOutputPath;
			}

			string entryPathFileName = null;
			if (entry.thePathFileName.StartsWith("<"))
			{
				entryPathFileName = entry.theRealPathFileName;
			}
			else
			{
				entryPathFileName = entry.thePathFileName;
			}

			string outputPathFileName = null;
			if (MainCROWBAR.TheApp.Settings.UnpackKeepFullPathIsChecked)
			{
				outputPathFileName = Path.Combine(outputPathStart, entryPathFileName);
			}
			else
			{
				string entryRelativePathFileName = FileManager.GetRelativePathFileName(theSelectedRelativeOutputPath, entryPathFileName);
				outputPathFileName = Path.Combine(outputPathStart, entryRelativePathFileName);
			}

			string outputPath = FileManager.GetPath(outputPathFileName);

			if (FileManager.PathExistsAfterTryToCreate(outputPath))
			{
				vpkFile.UnpackEntryDataToFile(entry, outputPathFileName);
			}

			if (File.Exists(outputPathFileName))
			{
				if (!theUnpackedPaths.Contains(theOutputPath))
				{
					theUnpackedPaths.Add(theOutputPath);
				}
				theUnpackedRelativePathsAndFileNames.Add(FileManager.GetRelativePathFileName(theOutputPath, outputPathFileName));
				//If Not Me.theUnpackedTempPathsAndPathFileNames.Contains(entry.thePathFileName) Then
				//	Me.theUnpackedTempPathsAndPathFileNames.Add(entry.thePathFileName)
				//End If
				if (Path.GetExtension(outputPathFileName) == ".mdl")
				{
					theUnpackedMdlFiles.Add(FileManager.GetRelativePathFileName(theOutputPath, outputPathFileName));
				}

				if (entry.thePathFileName.StartsWith("<"))
				{
					UpdateProgress(2, "Unpacked: \"" + entry.thePathFileName + "\" as \"" + entry.theRealPathFileName + "\"");
				}
				else
				{
					UpdateProgress(2, "Unpacked: " + entry.thePathFileName);
				}
			}
			else
			{
				UpdateProgress(2, "WARNING: Not unpacked: " + entry.thePathFileName);
			}
		}

		//Private Sub StartFile(ByVal packInternalPathFileName As String)
		//	Dim tempUnpackRelativePathFileName As String
		//	Dim pathFileName As String
		//	tempUnpackRelativePathFileName = Path.Combine(Me.theTempUnpackPaths(0), packInternalPathFileName)
		//	pathFileName = Me.GetOutputPathFileName(tempUnpackRelativePathFileName)

		//	System.Diagnostics.Process.Start(pathFileName)
		//End Sub
		private void StartFile(string pathFileName)
		{
			System.Diagnostics.Process.Start(pathFileName);
		}

		private string GetPackageDirectoryFileNameWithoutExtension(string archivePathFileName, BasePackageFileData vpkFileData)
		{
			string packageDirectoryFileNameWithoutExtension = "";

			string vpkFileNameWithoutExtension = null;
			string vpkFileNamePrefix = null;
			int underscoreIndex = 0;
			vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(archivePathFileName);

			packageDirectoryFileNameWithoutExtension = vpkFileNameWithoutExtension;
			if (!string.IsNullOrEmpty(vpkFileData.DirectoryFileNameSuffix))
			{
				underscoreIndex = vpkFileNameWithoutExtension.LastIndexOf("_");
				if (underscoreIndex >= 0)
				{
					vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, underscoreIndex);
					string packageDirectoryPathFileName = Path.Combine(FileManager.GetPath(archivePathFileName), vpkFileNamePrefix + vpkFileData.DirectoryFileNameSuffix + vpkFileData.FileExtension);
					if (File.Exists(packageDirectoryPathFileName))
					{
						packageDirectoryFileNameWithoutExtension = vpkFileNamePrefix + vpkFileData.DirectoryFileNameSuffix;
					}
				}
			}

			return packageDirectoryFileNameWithoutExtension;
		}

#endregion

#region Data

		private bool theSkipCurrentPackIsActive;
		private bool theSynchronousWorkerIsActive;
		private string theRunSynchronousResultMessage;
		private string theInputVpkPath;
		private string theOutputPath;
		private string theOutputPathOrModelOutputFileName;
		private bool theOutputPathIsExtendedWithPackageName;
		private string theSelectedRelativeOutputPath;

		private StreamWriter theLogFileStream;
		private string theLastLine;

		//TODO: Not currently used for anything but for drag-drop temp path deleteion.
		private List<string> theUnpackedPaths;
		//NOTE: Extra guard against deleting non-temp paths from accidental bad coding.
		private bool theUnpackedPathsAreInTempPath;
		// Used for listing unpacked files in combobox.
		private BindingListEx<string> theUnpackedRelativePathsAndFileNames;
		//TODO: Not currently used for anything.
		private List<string> theUnpackedTempPathsAndPathFileNames;
		private BindingListEx<string> theUnpackedMdlFiles;
		private BindingListEx<string> theLogFiles;

		//Private theTempUnpackPaths As List(Of String)

		//Private theVpkFileData As BasePackageFileData
		private SortedList<string, BasePackageFileData> theArchivePathFileNameToFileDataMap;
		private string theArchiveDirectoryFileNamePrefix;
		private FileStream theArchiveDirectoryInputFileStream;
		private BinaryReader theArchiveDirectoryInputFileReader;
		private BinaryReader theInputFileReader;

		private string thePackageDirectoryPathFileName;
		private BasePackageFileData thePackageFileData;

		private string theGamePath;

#endregion

	}

}