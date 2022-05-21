//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.ComponentModel;

namespace Crowbar
{
	public abstract class SourceModel
	{

#region Shared

		public static SourceModel Create(string mdlPathFileName, AppEnums.SupportedMdlVersion overrideVersion, ref int version)
		{
			SourceModel model = null;
			int storedVersion = 0;

			try
			{
				storedVersion = SourceModel.GetVersion(mdlPathFileName);
				if (overrideVersion == AppEnums.SupportedMdlVersion.DoNotOverride)
				{
					version = storedVersion;
				}
				else
				{
					version = int.Parse(EnumHelper.GetDescription(overrideVersion));
				}

				//If version = 4 Then
				//	'NOT IMPLEMENTED YET.
				//	'model = New SourceModel04(mdlPathFileName, version)
				//Else
				if (version == 6)
				{
					model = new SourceModel06(mdlPathFileName, version);
				}
				else if (version == 10)
				{
					model = new SourceModel10(mdlPathFileName, version);
					//ElseIf version = 11 Then
					//NOT IMPLEMENTED YET.
					//model = New SourceModel10(mdlPathFileName, version)
					//ElseIf version = 14 Then
					//NOT IMPLEMENTED YET.
					//model = New SourceModel14(mdlPathFileName, version)
				}
				else if (version == 2531)
				{
					model = new SourceModel2531(mdlPathFileName, version);
				}
				else if (version == 27)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel31(mdlPathFileName, version);
				}
				else if (version == 28)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel31(mdlPathFileName, version);
				}
				else if (version == 29)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel31(mdlPathFileName, version);
				}
				else if (version == 30)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel31(mdlPathFileName, version);
				}
				else if (version == 31)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel31(mdlPathFileName, version);
				}
				else if (version == 32)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel32(mdlPathFileName, version);
				}
				else if (version == 35)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel36(mdlPathFileName, version);
				}
				else if (version == 36)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel36(mdlPathFileName, version);
				}
				else if (version == 37)
				{
					//NOT FULLY IMPLEMENTED YET.
					model = new SourceModel37(mdlPathFileName, version);
				}
				else if (version == 44)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 45)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 46)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 47)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 48)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 49)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 52)
				{
					//TODO: Finish.
					model = new SourceModel52(mdlPathFileName, version);
				}
				else if (version == 53)
				{
					//TODO: Finish.
					model = new SourceModel53(mdlPathFileName, version);
				}
				else if (version == 54)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 55)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 56)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 58)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else if (version == 59)
				{
					model = new SourceModel49(mdlPathFileName, version);
				}
				else
				{
					// Version not implemented.
					model = null;
				}
			}
			catch (Exception ex)
			{
				throw;
			}

			return model;
		}

		private static int GetVersion(string mdlPathFileName)
		{
			int version = 0;
			FileStream inputFileStream = null;
			BinaryReader inputFileReader = null;

			version = -1;
			inputFileStream = null;
			inputFileReader = null;
			try
			{
				inputFileStream = new FileStream(mdlPathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				if (inputFileStream != null)
				{
					try
					{
						//NOTE: Important to set System.Text.Encoding.ASCII so that ReadChars() only reads in one byte per Char.
						inputFileReader = new BinaryReader(inputFileStream, System.Text.Encoding.ASCII);

						string id = new string(inputFileReader.ReadChars(4));
						version = inputFileReader.ReadInt32();
						if (id == "MDLZ")
						{
							if (version != 14)
							{
								throw new FormatException("File with header ID (first 4 bytes of file) of 'MDLZ' (without quotes) does not have expected MDL version of 14. MDL file is not a GoldSource- or Source-engine MDL file.");
							}
						}
						else if (id != "IDST")
						{
							throw new FormatException("File does not have expected MDL header ID (first 4 bytes of file) of 'IDST' or 'MDLZ' (without quotes). MDL file is not a GoldSource- or Source-engine MDL file.");
						}
					}
					catch (FormatException ex)
					{
						throw;
					}
					catch (Exception ex)
					{
						//Dim debug As Integer = 4242
						throw;
					}
					finally
					{
						if (inputFileReader != null)
						{
							inputFileReader.Close();
						}
					}
				}
			}
			catch (FormatException ex)
			{
				throw;
			}
			catch (Exception ex)
			{
				//Dim debug As Integer = 4242
				throw;
			}
			finally
			{
				if (inputFileStream != null)
				{
					inputFileStream.Close();
				}
			}

			return version;
		}

		//Private Shared version_shared As Integer

#endregion

#region Creation and Destruction

		protected SourceModel(string mdlPathFileName, int mdlVersion)
		{
			theMdlPathFileName = mdlPathFileName;
			theName = Path.GetFileNameWithoutExtension(mdlPathFileName);
			theVersion = mdlVersion;
		}

#endregion

#region Properties - Model Data

		public string ID
		{
			get
			{
				return theMdlFileDataGeneric.theID;
			}
		}

		public int Version
		{
			get
			{
				return theVersion;
			}
		}

		public string Name
		{
			get
			{
				return theName;
			}
			//Set(ByVal value As String)
			//	Me.theName = value
			//End Set
		}

#endregion

#region Properties - File-Related

		// The *Used properties should return whether the files are actually referred to by the MDL file.
		// For the PHY file and others that have no reference in the MDL file, simply return whether each file exists.

		public virtual bool SequenceGroupMdlFilesAreUsed
		{
			get
			{
				return false;
			}
		}

		public virtual bool TextureMdlFileIsUsed
		{
			get
			{
				return false;
			}
		}

		public virtual bool PhyFileIsUsed
		{
			get
			{
				return false;
			}
		}

		public virtual bool VtxFileIsUsed
		{
			get
			{
				return false;
			}
		}

		public virtual bool AniFileIsUsed
		{
			get
			{
				return false;
			}
		}

		public virtual bool VvdFileIsUsed
		{
			get
			{
				return false;
			}
		}

		public bool WritingIsCanceled
		{
			get
			{
				return theWritingIsCanceled;
			}
			set
			{
				theWritingIsCanceled = value;
			}
		}

		//Public Property WritingSingleFileIsCanceled As Boolean
		//	Get
		//		Return Me.theWritingSingleFileIsCanceled
		//	End Get
		//	Set(value As Boolean)
		//		Me.theWritingSingleFileIsCanceled = value
		//	End Set
		//End Property

#endregion

#region Properties - Data Query

		public virtual bool HasTextureData
		{
			get
			{
				return false;
			}
		}

		public virtual bool HasMeshData
		{
			get
			{
				return false;
			}
		}

		public virtual bool HasLodMeshData
		{
			get
			{
				return false;
			}
		}

		public virtual bool HasPhysicsMeshData
		{
			get
			{
				return false;
			}
		}

		public virtual bool HasProceduralBonesData
		{
			get
			{
				return false;
			}
		}

		public virtual bool HasBoneAnimationData
		{
			get
			{
				return false;
			}
		}

		public virtual bool HasVertexAnimationData
		{
			get
			{
				return false;
			}
		}

		public virtual bool HasTextureFileData
		{
			get
			{
				return false;
			}
		}

#endregion

#region Methods

		public virtual AppEnums.StatusMessage ReadMdlFileHeader()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			if (!File.Exists(theMdlPathFileName))
			{
				status = AppEnums.StatusMessage.ErrorRequiredMdlFileNotFound;
			}

			if (status == AppEnums.StatusMessage.Success)
			{
				ReadFile(theMdlPathFileName, ReadMdlFileHeader_Internal);
			}

			return status;
		}

		public virtual AppEnums.FilesFoundFlags CheckForRequiredFiles()
		{
			AppEnums.FilesFoundFlags status = AppEnums.FilesFoundFlags.AllFilesFound;


			return status;
		}

		public virtual AppEnums.StatusMessage ReadAniFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = AppEnums.StatusMessage.Error;

			return status;
		}

		public virtual AppEnums.StatusMessage ReadSequenceGroupMdlFiles()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = AppEnums.StatusMessage.Error;

			return status;
		}

		public virtual AppEnums.StatusMessage ReadTextureMdlFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = AppEnums.StatusMessage.Error;

			return status;
		}

		public virtual AppEnums.StatusMessage ReadPhyFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = AppEnums.StatusMessage.Error;

			return status;
		}

		public virtual AppEnums.StatusMessage ReadMdlFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				ReadFile(theMdlPathFileName, ReadMdlFile_Internal);
			}
			catch (Exception ex)
			{
				status = AppEnums.StatusMessage.Error;
			}

			return status;
		}

		public virtual AppEnums.StatusMessage ReadVtxFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//If String.IsNullOrEmpty(Me.theVtxPathFileName) Then
			//	status = Me.CheckForRequiredFiles()
			//End If

			if (status == AppEnums.StatusMessage.Success)
			{
				ReadFile(theVtxPathFileName, ReadVtxFile_Internal);
			}

			return status;
		}

		public virtual AppEnums.StatusMessage ReadVvdFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//If String.IsNullOrEmpty(Me.theVvdPathFileName) Then
			//	status = Me.CheckForRequiredFiles()
			//End If

			if (status == AppEnums.StatusMessage.Success)
			{
				ReadFile(theVvdPathFileName, ReadVvdFile_Internal);
			}

			return status;
		}

		//Public Overridable Function ReadMdlFileForViewer() As AppEnums.StatusMessage
		//	Dim status As AppEnums.StatusMessage = StatusMessage.Success

		//	If Not File.Exists(Me.theMdlPathFileName) Then
		//		status = StatusMessage.ErrorRequiredMdlFileNotFound
		//	End If

		//	If status = StatusMessage.Success Then
		//		Me.ReadFile(Me.theMdlPathFileName, AddressOf Me.ReadMdlFileForViewer_Internal)
		//	End If

		//	Return status
		//End Function

		public virtual AppEnums.StatusMessage SetAllSmdPathFileNames()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			return status;
		}

		public virtual AppEnums.StatusMessage WriteQcFile(string qcPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			string writeStatus = null;

			theQcPathFileName = qcPathFileName;
			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, qcPathFileName);
			writeStatus = WriteTextFile(qcPathFileName, WriteQcFile);
			if (writeStatus == "Success")
			{
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, qcPathFileName);
			}
			else
			{
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFailed, writeStatus);
			}

			return status;
		}

		public virtual AppEnums.StatusMessage WriteReferenceMeshFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			return status;
		}

		public virtual AppEnums.StatusMessage WriteLodMeshFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			return status;
		}

		public virtual string WriteMeshSmdFile(string smdPathFileName, int lodIndex, SourceVtxModel07 aVtxModel, SourceMdlModel aModel, int bodyPartVertexIndexStart)
		{
			string status = "Success";

			try
			{
				theOutputFileTextWriter = File.CreateText(smdPathFileName);

				WriteMeshSmdFile(lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart);
			}
			catch (PathTooLongException ex)
			{
				status = "ERROR: Crowbar tried to create \"" + smdPathFileName + "\" but the system gave this message: " + ex.Message;
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (theOutputFileTextWriter != null)
				{
					theOutputFileTextWriter.Flush();
					theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

		public virtual AppEnums.StatusMessage WritePhysicsMeshSmdFile(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			return status;
		}

		public virtual AppEnums.StatusMessage WriteBoneAnimationSmdFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			return status;
		}

		public virtual AppEnums.StatusMessage WriteVertexAnimationVtaFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			return status;
		}

		public virtual string WriteBoneAnimationSmdFile(string smdPathFileName, SourceMdlSequenceDescBase aSequenceDesc, SourceMdlAnimationDescBase anAnimationDesc)
		{
			string status = "Success";

			try
			{
				theOutputFileTextWriter = File.CreateText(smdPathFileName);

				WriteBoneAnimationSmdFile(aSequenceDesc, anAnimationDesc);
			}
			catch (PathTooLongException ex)
			{
				status = "ERROR: Crowbar tried to create \"" + smdPathFileName + "\" but the system gave this message: " + ex.Message;
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (theOutputFileTextWriter != null)
				{
					if (theOutputFileTextWriter.BaseStream != null)
					{
						theOutputFileTextWriter.Flush();
					}
					theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

		public virtual string WriteVertexAnimationVtaFile(string vtaPathFileName, SourceMdlBodyPart bodyPart)
		{
			string status = "Success";

			try
			{
				theOutputFileTextWriter = File.CreateText(vtaPathFileName);

				WriteVertexAnimationVtaFile(bodyPart);
			}
			catch (PathTooLongException ex)
			{
				status = "ERROR: Crowbar tried to create \"" + vtaPathFileName + "\" but the system gave this message: " + ex.Message;
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (theOutputFileTextWriter != null)
				{
					theOutputFileTextWriter.Flush();
					theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

		public virtual AppEnums.StatusMessage WriteVrdFile(string vrdPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			return status;
		}

		public virtual AppEnums.StatusMessage WriteTextureFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = AppEnums.StatusMessage.Error;

			return status;
		}

		public virtual AppEnums.StatusMessage WriteDeclareSequenceQciFile(string declareSequenceQciPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			return status;
		}

		public virtual void WriteMdlFileNameToMdlFile(string mdlPathFileName, string internalMdlFileName)
		{
			ReadFile(mdlPathFileName, ReadMdlFileHeader_Internal);
			WriteFile(mdlPathFileName, WriteMdlFileNameToMdlFile, internalMdlFileName, theMdlFileDataGeneric);
		}

		public virtual void WriteAniFileNameToMdlFile(string mdlPathFileName, string internalMdlFileName)
		{
			ReadFile(mdlPathFileName, ReadMdlFileHeader_Internal);
			string internalAniFileName = Path.Combine("models", Path.ChangeExtension(internalMdlFileName, ".ani"));
			WriteFile(mdlPathFileName, WriteAniFileNameToMdlFile, internalAniFileName, theMdlFileDataGeneric);
		}

		public virtual AppEnums.StatusMessage WriteAccessedBytesDebugFiles(string debugPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = AppEnums.StatusMessage.Error;

			return status;
		}

		public virtual List<string> GetOverviewTextLines(string mdlPathFileName, AppEnums.SupportedMdlVersion mdlVersionOverride)
		{
			List<string> textLines = new List<string>();

			try
			{
				ReadFile(mdlPathFileName, ReadMdlFileForViewer_Internal);

				GetHeaderDataFromMdlFile(textLines, mdlVersionOverride);
				textLines.Add("");
				GetModelFileDataFromMdlFile(textLines);
				textLines.Add("");
				GetTextureDataFromMdlFile(textLines);
				//textLines.Add("")
				//Me.GetSequenceDataFromMdlFile(textLines)
			}
			catch (Exception ex)
			{
				//textLines.Add("ERROR: " + ex.Message)
				throw;
			}

			return textLines;
		}

		public virtual List<string> GetTextureFolders(string mdlPathFileName)
		{
			List<string> textureFolders = null;

			try
			{
				ReadFile(mdlPathFileName, ReadMdlFileForViewer_Internal);

				if (HasTextureData)
				{
					if (theMdlFileDataGeneric.version <= 10)
					{
					}
					else
					{
						textureFolders = GetTextureFolders();
					}
				}
				else
				{
				}
			}
			catch (Exception ex)
			{
				throw;
			}

			return textureFolders;
		}

		public virtual List<string> GetTextureFolders()
		{
			return new List<string>();
		}

		public virtual List<string> GetTextureFileNames()
		{
			return new List<string>();
		}

		public virtual List<string> GetSequenceInfo()
		{
			return new List<string>();
		}

#endregion

#region Events

		public event SourceModelProgressEventHandler SourceModelProgress;

#endregion

#region Protected Methods

		protected virtual void ReadAniFile_Internal()
		{

		}

		protected virtual void ReadMdlFile_Internal()
		{

		}

		protected virtual void ReadPhyFile_Internal()
		{

		}

		protected virtual AppEnums.StatusMessage ReadSequenceGroupMdlFile(string pathFileName, int sequenceGroupIndex)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			FileStream inputFileStream = null;
			theInputFileReader = null;
			try
			{
				inputFileStream = new FileStream(pathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				if (inputFileStream != null)
				{
					try
					{
						theInputFileReader = new BinaryReader(inputFileStream, System.Text.Encoding.ASCII);

						ReadSequenceGroupMdlFile(sequenceGroupIndex);
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

			return status;
		}

		protected virtual void ReadSequenceGroupMdlFile(int sequenceGroupIndex)
		{

		}

		protected virtual void ReadTextureMdlFile_Internal()
		{

		}

		protected virtual void ReadVtxFile_Internal()
		{

		}

		protected virtual void ReadVvdFile_Internal()
		{

		}

		protected virtual void WriteQcFile()
		{

		}

		protected virtual void WriteMeshSmdFile(int lodIndex, SourceVtxModel07 aVtxModel, SourceMdlModel aModel, int bodyPartVertexIndexStart)
		{

		}

		protected virtual void WritePhysicsMeshSmdFile()
		{

		}

		protected virtual void WriteBoneAnimationSmdFile(SourceMdlSequenceDescBase aSequenceDesc, SourceMdlAnimationDescBase anAnimationDesc)
		{

		}

		protected virtual void WriteVertexAnimationVtaFile(SourceMdlBodyPart bodyPart)
		{

		}

		protected virtual void WriteVrdFile()
		{

		}

		protected virtual void WriteTextureFile()
		{

		}

		protected virtual void WriteDeclareSequenceQciFile()
		{

		}

		protected virtual void ReadMdlFileHeader_Internal()
		{

		}

		protected virtual void ReadMdlFileForViewer_Internal()
		{

		}

		protected virtual void WriteMdlFileNameToMdlFile(string internalMdlFileName)
		{

		}

		protected virtual void WriteAniFileNameToMdlFile(string internalAniFileName)
		{

		}

		protected void ReadFile(string pathFileName, ReadFileDelegate readFileAction)
		{
			FileStream inputFileStream = null;
			theInputFileReader = null;
			try
			{
				inputFileStream = new FileStream(pathFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				if (inputFileStream != null)
				{
					try
					{
						theInputFileReader = new BinaryReader(inputFileStream, System.Text.Encoding.ASCII);

						readFileAction.Invoke();
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

		protected void WriteFile(string pathFileName, WriteFileDelegate writeFileAction, string value, SourceFileData fileData)
		{
			FileStream outputFileStream = null;
			try
			{
				outputFileStream = new FileStream(pathFileName, FileMode.Open);
				if (outputFileStream != null)
				{
					try
					{
						theOutputFileBinaryWriter = new BinaryWriter(outputFileStream, System.Text.Encoding.ASCII);

						writeFileAction.Invoke(value);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
					finally
					{
						if (theOutputFileBinaryWriter != null)
						{
							theOutputFileBinaryWriter.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (outputFileStream != null)
				{
					outputFileStream.Close();
				}
			}
		}

		protected string WriteTextFile(string outputPathFileName, WriteTextFileDelegate writeTextFileAction)
		{
			string status = "Success";

			try
			{
				theOutputFileTextWriter = File.CreateText(outputPathFileName);

				writeTextFileAction.Invoke();
			}
			catch (PathTooLongException ex)
			{
				status = "ERROR: Crowbar tried to create \"" + outputPathFileName + "\" but the system gave this message: " + ex.Message;
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (theOutputFileTextWriter != null)
				{
					theOutputFileTextWriter.Flush();
					theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

		protected void NotifySourceModelProgress(AppEnums.ProgressOptions progress, string message)
		{
			if (SourceModelProgress != null)
				SourceModelProgress(this, new SourceModelProgressEventArgs(progress, message));
		}

		protected string WriteAccessedBytesDebugFile(string debugPathFileName, FileSeekLog fileSeekLog)
		{
			string status = "Success";

			try
			{
				theOutputFileTextWriter = File.CreateText(debugPathFileName);

				AccessedBytesDebugFile debugFile = new AccessedBytesDebugFile(theOutputFileTextWriter);
				debugFile.WriteHeaderComment();
				debugFile.WriteFileSeekLog(fileSeekLog);
			}
			catch (PathTooLongException ex)
			{
				status = "ERROR: Crowbar tried to create \"" + debugPathFileName + "\" but the system gave this message: " + ex.Message;
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (theOutputFileTextWriter != null)
				{
					theOutputFileTextWriter.Flush();
					theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

#endregion

#region Private Delegates

		public delegate void SourceModelProgressEventHandler(object sender, SourceModelProgressEventArgs e);

		protected delegate void ReadFileDelegate();
		protected delegate void WriteFileDelegate(string value);
		protected delegate void WriteTextFileDelegate();

#endregion

#region Private Methods

		private void GetHeaderDataFromMdlFile(List<string> ioTextLines, AppEnums.SupportedMdlVersion mdlVersionOverride)
		{
			//Dim mdlFileData48 As SourceMdlFileData48 = Nothing
			//Dim mdlFileData49 As SourceMdlFileData49 = Nothing
			//If Me.theMdlFileDataGeneric.version = 48 Then
			//	mdlFileData48 = CType(Me.theMdlFileDataGeneric, SourceMdlFileData48)
			//ElseIf Me.theMdlFileDataGeneric.version = 49 Then
			//	mdlFileData49 = CType(Me.theMdlFileDataGeneric, SourceMdlFileData49)
			//End If

			ioTextLines.Add("=== General Info ===");
			ioTextLines.Add("");

			string fileTypeId = theMdlFileDataGeneric.theID;
			if (fileTypeId == "IDST")
			{
				ioTextLines.Add("MDL file type ID: " + fileTypeId);
			}
			else
			{
				ioTextLines.Add("ERROR: MDL file type ID is not IDST. This is either a corrupted MDL file or not a GoldSource or Source model file.");
			}

			string extraVersionText = "";
			if (mdlVersionOverride != AppEnums.SupportedMdlVersion.DoNotOverride)
			{
				extraVersionText = "   Model version override: " + EnumHelper.GetDescription(mdlVersionOverride);
			}
			ioTextLines.Add("MDL file version: " + theMdlFileDataGeneric.version.ToString("N0") + extraVersionText);

			ioTextLines.Add("MDL stored file name: \"" + theMdlFileDataGeneric.theModelName + "\"");
			//If mdlFileData48 IsNot Nothing AndAlso mdlFileData48.nameCopyOffset > 0 Then
			//	ioTextLines.Add("MDL stored file name copy: """ + mdlFileData48.theNameCopy + """")
			//End If
			//If mdlFileData49 IsNot Nothing AndAlso mdlFileData49.nameCopyOffset > 0 Then
			//	ioTextLines.Add("MDL stored file name copy: """ + mdlFileData49.theNameCopy + """")
			//End If

			long storedFileSize = 0;
			long actualFileSize = 0;
			storedFileSize = theMdlFileDataGeneric.fileSize;
			actualFileSize = theMdlFileDataGeneric.theActualFileSize;
			if (storedFileSize > -1)
			{
				ioTextLines.Add("MDL stored file size: " + storedFileSize.ToString("N0") + " bytes");
				ioTextLines.Add("MDL actual file size: " + actualFileSize.ToString("N0") + " bytes");
				if (theMdlFileDataGeneric.fileSize != theMdlFileDataGeneric.theActualFileSize)
				{
					ioTextLines.Add("WARNING: MDL file size is different than the internally-stored file size. This means the MDL file was changed after compiling -- possibly corrupted from hex-editing.");
				}
			}
			else
			{
				ioTextLines.Add("MDL file size: " + actualFileSize.ToString("N0") + " bytes");
			}

			if (theMdlFileDataGeneric.theChecksumIsValid)
			{
				ioTextLines.Add("MDL checksum: " + theMdlFileDataGeneric.checksum.ToString("X8"));
			}
		}

		private void GetModelFileDataFromMdlFile(List<string> ioTextLines)
		{
			CheckForRequiredFiles();

			ioTextLines.Add("=== Model Files ===");
			ioTextLines.Add("");

			if (AniFileIsUsed)
			{
				if (File.Exists(theAniPathFileName))
				{
					ioTextLines.Add("\"" + Path.GetFileName(theAniPathFileName) + "\"");
				}
				else
				{
					ioTextLines.Add("ERROR: File not found: \"" + Path.GetFileName(theAniPathFileName) + "\"");
				}
			}

			ioTextLines.Add("\"" + Path.GetFileName(theMdlPathFileName) + "\"");

			//TODO: For GoldSource, list all SequenceGroup MDL files.
			//If Me.SequenceGroupMdlFilesAreUsed Then
			//	'If File.Exists(Me.thePhyPathFileName) Then
			//	'	ioTextLines.Add("""" + Path.GetFileName(Me.thePhyPathFileName) + """")
			//	'Else
			//	'	ioTextLines.Add("ERROR: File not found: """ + Path.GetFileName(Me.thePhyPathFileName) + """")
			//	'End If
			//End If

			if (TextureMdlFileIsUsed)
			{
				if (File.Exists(theTextureMdlPathFileName))
				{
					ioTextLines.Add("\"" + Path.GetFileName(theTextureMdlPathFileName) + "\"");
				}
				else
				{
					ioTextLines.Add("ERROR: File not found: \"" + Path.GetFileName(theTextureMdlPathFileName) + "\"");
				}
			}

			if (PhyFileIsUsed)
			{
				if (File.Exists(thePhyPathFileName))
				{
					ioTextLines.Add("\"" + Path.GetFileName(thePhyPathFileName) + "\"");
				}
				else
				{
					ioTextLines.Add("ERROR: File not found: \"" + Path.GetFileName(thePhyPathFileName) + "\"");
				}
			}

			//TODO: List all vtx files, not just the one used for decompiling.
			if (VtxFileIsUsed)
			{
				if (File.Exists(theVtxPathFileName))
				{
					ioTextLines.Add("\"" + Path.GetFileName(theVtxPathFileName) + "\"");
				}
				else
				{
					ioTextLines.Add("ERROR: File not found: \"" + Path.GetFileName(theVtxPathFileName) + "\"");
				}
			}

			if (VvdFileIsUsed)
			{
				if (File.Exists(theVvdPathFileName))
				{
					ioTextLines.Add("\"" + Path.GetFileName(theVvdPathFileName) + "\"");
				}
				else
				{
					ioTextLines.Add("ERROR: File not found: \"" + Path.GetFileName(theVvdPathFileName) + "\"");
				}
			}
		}

		private void GetTextureDataFromMdlFile(List<string> ioTextLines)
		{
			ioTextLines.Add("=== Material and Texture Info ===");
			ioTextLines.Add("");

			if (HasTextureData)
			{
				if (theMdlFileDataGeneric.version <= 10)
				{
					if (TextureMdlFileIsUsed)
					{
						ioTextLines.Add("Texture files are stored within the separate 't' MDL file: " + Path.GetFileName(theTextureMdlPathFileName));
					}
					else
					{
						ioTextLines.Add("Texture files are stored within the MDL file.");
					}
				}
				else
				{
					ioTextLines.Add("Material Folders ($CDMaterials lines in QC file -- folders where VMT files should be, relative to game's \"materials\" folder): ");
					List<string> textureFolders = GetTextureFolders();
					if (textureFolders.Count == 0)
					{
						ioTextLines.Add("No material folders set.");
					}
					else
					{
						foreach (string aTextureFolder in textureFolders)
						{
							ioTextLines.Add("\"" + aTextureFolder + "\"");
						}
					}
				}

				ioTextLines.Add("");

				ioTextLines.Add("Material File Names (file names in mesh SMD files and in QC $texturegroup command): ");
				List<string> textureFileNames = GetTextureFileNames();
				ioTextLines.Add("(Total used: " + textureFileNames.Count.ToString() + ")");
				if (textureFileNames.Count == 0)
				{
					ioTextLines.Add("No material file names found.");
				}
				else
				{
					foreach (string aTextureFileName in textureFileNames)
					{
						ioTextLines.Add("\"" + aTextureFileName + "\"");
					}
				}
			}
			else
			{
				//ioTextLines.Add("No texture data because this model only has animation data.")
				ioTextLines.Add("No texture data.");
			}
		}

		//Private Sub GetSequenceDataFromMdlFile(ByVal ioTextLines As List(Of String))
		//	ioTextLines.Add("=== Sequence Info ===")
		//	ioTextLines.Add("")

		//	If Me.HasBoneAnimationData Then
		//		If Me.theMdlFileDataGeneric.version <= 10 Then
		//		Else
		//			Dim sequenceNames As List(Of String)
		//			sequenceNames = Me.GetSequenceInfo()
		//			For Each aSequenceName As String In sequenceNames
		//				ioTextLines.Add("""" + aSequenceName + """")
		//			Next
		//		End If
		//	Else
		//		ioTextLines.Add("No sequence data.")
		//	End If
		//End Sub

#endregion

#region Data

		protected int theVersion;
		protected string theName;
		//Protected thePhysicsMeshSmdFileName As String

		protected SourceMdlFileDataBase theMdlFileDataGeneric;
		protected SourceFileData theAniFileDataGeneric;
		protected SourcePhyFileData thePhyFileDataGeneric;

		protected BinaryReader theInputFileReader;
		protected BinaryWriter theOutputFileBinaryWriter;
		protected StreamWriter theOutputFileTextWriter;

		protected bool theWritingIsCanceled;
		protected bool theWritingSingleFileIsCanceled;

		protected string theAniPathFileName;
		protected string thePhyPathFileName;
		protected string theMdlPathFileName;
		protected List<string> theSequenceGroupMdlPathFileNames;
		protected string theTextureMdlPathFileName;
		protected string theVtxPathFileName;
		protected string theVvdPathFileName;

		protected string theQcPathFileName;

#endregion

	}

}