//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Runtime.InteropServices;

namespace Crowbar
{
	public class SourceModel10 : SourceModel06
	{
#region Creation and Destruction

		public SourceModel10(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{

			this.theExternalTexturesAreUsed = false;
		}

#endregion

#region Properties

		public override bool SequenceGroupMdlFilesAreUsed
		{
			get
			{
				return this.theMdlFileData != null && this.theMdlFileData.sequenceGroupCount > 1 && this.theMdlFileData.sequenceGroupCount == this.theSequenceGroupMdlPathFileNames.Count;
			}
		}

		public override bool TextureMdlFileIsUsed
		{
			get
			{
				return this.theMdlFileData.textureCount == 0 && !string.IsNullOrEmpty(this.theTextureMdlPathFileName);
			}
		}

		public override bool HasTextureData
		{
			get
			{
				return this.theMdlFileData.textureCount > 0 || this.theTextureMdlFileData10 != null;
			}
		}

		public override bool HasMeshData
		{
			get
			{
				if (this.theMdlFileData.theBones != null && this.theMdlFileData.theBones.Count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public override bool HasBoneAnimationData
		{
			get
			{
				if (this.theMdlFileData.theSequences != null && this.theMdlFileData.theSequences.Count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public override bool HasTextureFileData
		{
			get
			{
				return this.theMdlFileData.textureCount > 0 || this.theTextureMdlFileData10 != null;
			}
		}

#endregion

#region Methods

		public override AppEnums.FilesFoundFlags CheckForRequiredFiles()
		{
			AppEnums.FilesFoundFlags status = AppEnums.FilesFoundFlags.AllFilesFound;

			string mdlPath = null;
			string mdlFileNameWithoutExtension = null;
			string mdlExtension = null;
			string textureMdlFileName = null;

			try
			{
				mdlPath = FileManager.GetPath(this.theMdlPathFileName);
				mdlFileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.theMdlPathFileName);
				mdlExtension = Path.GetExtension(this.theMdlPathFileName);

				this.theSequenceGroupMdlPathFileNames = new List<string>(this.theMdlFileData.sequenceGroupCount);

				this.theSequenceGroupMdlPathFileNames.Add(this.theMdlPathFileName);
				//NOTE: Start index at 1 because 0 is the main MDL file, handled above.
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string aSequenceGroupMdlFileName = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string aSequenceGroupMdlPathFileName = null;
				for (int sequenceGroupIndex = 1; sequenceGroupIndex < this.theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
				{
	//				Dim aSequenceGroupMdlFileName As String
	//				Dim aSequenceGroupMdlPathFileName As String
					//sequenceGroupMdlFileName = Path.GetFileName(aSequenceGroup.theFileName)
					//sequenceGroupMdlPathFileName = Path.Combine(mdlPath, sequenceGroupMdlFileName)
					//NOTE: Ignore internal name for sequence group file names and use file name of MDL file (not the internal name).
					//      This seems to be how it is handled by Half-Life and all the tools for it.
					aSequenceGroupMdlFileName = mdlFileNameWithoutExtension + sequenceGroupIndex.ToString("00") + mdlExtension;
					aSequenceGroupMdlPathFileName = Path.Combine(mdlPath, aSequenceGroupMdlFileName);
					//If Not File.Exists(aSequenceGroupMdlPathFileName) Then
					//	status = StatusMessage.Error
					//End If
					this.theSequenceGroupMdlPathFileNames.Add(aSequenceGroupMdlPathFileName);

					if (!File.Exists(aSequenceGroupMdlPathFileName))
					{
						status = AppEnums.FilesFoundFlags.ErrorRequiredSequenceGroupMdlFileNotFound;
						return status;
					}
				}

				if (this.theMdlFileData.textureCount == 0)
				{
					textureMdlFileName = mdlFileNameWithoutExtension + "T" + mdlExtension;
					this.theTextureMdlPathFileName = Path.Combine(mdlPath, textureMdlFileName);
					if (!File.Exists(this.theTextureMdlPathFileName))
					{
						status = AppEnums.FilesFoundFlags.ErrorRequiredTextureMdlFileNotFound;
						return status;
					}
				}
			}
			catch (Exception ex)
			{
				status = AppEnums.FilesFoundFlags.Error;
			}

			return status;
		}

		public override AppEnums.StatusMessage ReadSequenceGroupMdlFiles()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlSequenceGroup10 aSequenceGroup = null;
			string mdlPath = null;
			string sequenceGroupMdlFileName = null;
			string sequenceGroupMdlPathFileName = null;
			//Dim extension As String

			//NOTE: Start at index 1 because sequence group 0 is in the main MDL file.
			for (int sequenceGroupIndex = 1; sequenceGroupIndex < this.theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
			{
				aSequenceGroup = this.theMdlFileData.theSequenceGroups[sequenceGroupIndex];

				//mdlPath = FileManager.GetPath(Me.theMdlPathFileName)
				//sequenceGroupMdlFileName = Path.GetFileName(aSequenceGroup.theFileName)
				//sequenceGroupMdlPathFileName = Path.Combine(mdlPath, sequenceGroupMdlFileName)
				//'NOTE: PS2 Half-Life models that use sequence groups store "DOL" extension internally instead of "MDL".
				//If Not File.Exists(sequenceGroupMdlPathFileName) Then
				//	extension = Path.GetExtension(aSequenceGroup.theFileName)
				//	If extension.ToLower() = ".dol" Then
				//		sequenceGroupMdlPathFileName = Path.ChangeExtension(sequenceGroupMdlPathFileName, ".mdl")
				//	End If
				//End If
				//======
				//NOTE: Ignore internal name for sequence group file names and use file name of MDL file (not the internal name).
				//      This seems to be how it is handled by Half-Life and all the tools for it.
				mdlPath = FileManager.GetPath(this.theMdlPathFileName);
				sequenceGroupMdlFileName = Path.GetFileNameWithoutExtension(this.theMdlPathFileName) + sequenceGroupIndex.ToString("00") + ".mdl";
				sequenceGroupMdlPathFileName = Path.Combine(mdlPath, sequenceGroupMdlFileName);

				status = this.ReadSequenceGroupMdlFile(sequenceGroupMdlPathFileName, sequenceGroupIndex);
			}

			return status;
		}

		public override AppEnums.StatusMessage ReadTextureMdlFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//If String.IsNullOrEmpty(Me.theTextureMdlPathFileName) Then
			//	status = Me.CheckForRequiredFiles()
			//End If

			if (status == AppEnums.StatusMessage.Success)
			{
				try
				{
					this.ReadFile(this.theTextureMdlPathFileName, this.ReadTextureMdlFile_Internal);
				}
				catch (Exception ex)
				{
					status = AppEnums.StatusMessage.Error;
				}
			}

			return status;
		}

		public override AppEnums.StatusMessage WriteReferenceMeshFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//Dim smdFileName As String
			//Dim smdPathFileName As String
			//Dim aBodyPart As SourceVtxBodyPart
			//Dim aVtxModel As SourceVtxModel
			//Dim aModel As SourceMdlModel
			//Dim bodyPartVertexIndexStart As Integer

			//bodyPartVertexIndexStart = 0
			//If Me.theVtxFileData48.theVtxBodyParts IsNot Nothing AndAlso Me.theMdlFileData48.theBodyParts IsNot Nothing Then
			//	For bodyPartIndex As Integer = 0 To Me.theVtxFileData48.theVtxBodyParts.Count - 1
			//		aBodyPart = Me.theVtxFileData48.theVtxBodyParts(bodyPartIndex)

			//		If aBodyPart.theVtxModels IsNot Nothing Then
			//			For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
			//				aVtxModel = aBodyPart.theVtxModels(modelIndex)

			//				If aVtxModel.theVtxModelLods IsNot Nothing Then
			//					aModel = Me.theMdlFileData48.theBodyParts(bodyPartIndex).theModels(modelIndex)
			//					If aModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
			//						Continue For
			//					End If

			//					For lodIndex As Integer = lodStartIndex To lodStopIndex
			//						smdFileName = SourceFileNamesModule.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex, Me.theMdlFileData48.theModelCommandIsUsed, Me.theName, Me.theMdlFileData48.theBodyParts(bodyPartIndex).theModels(modelIndex).name, Me.theMdlFileData48.theBodyParts.Count, Me.theMdlFileData48.theBodyParts(bodyPartIndex).theModels.Count)
			//						smdPathFileName = Path.Combine(modelOutputPath, smdFileName)

			//						Me.NotifySourceModelProgress(ProgressOptions.WritingSmdFileStarted, smdPathFileName)
			//						'NOTE: Check here in case writing is canceled in the above event.
			//						If Me.theWritingIsCanceled Then
			//							status = StatusMessage.Canceled
			//							Return status
			//						ElseIf Me.theWritingSingleFileIsCanceled Then
			//							status = StatusMessage.Skipped
			//							Exit For
			//						End If

			//						Me.WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart)

			//						Me.NotifySourceModelProgress(ProgressOptions.WritingSmdFileFinished, smdPathFileName)
			//					Next

			//					bodyPartVertexIndexStart += aModel.vertexCount
			//				End If
			//			Next
			//		End If
			//	Next
			//End If
			SourceMdlBodyPart10 aBodyPart = null;
			SourceMdlModel10 aBodyModel = null;
			//Dim smdFileName As String
			string smdPathFileName = null;
			//Dim aVertex As SourceVector
			if (this.theMdlFileData.theBodyParts != null)
			{
				for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = this.theMdlFileData.theBodyParts[bodyPartIndex];

					if (aBodyPart.theModels != null)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theModels.Count; modelIndex++)
						{
							aBodyModel = aBodyPart.theModels[modelIndex];
							if (aBodyModel.theName == "blank")
							{
								continue;
							}

							aBodyModel.theSmdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileName, bodyPartIndex, modelIndex, 0, this.theName, aBodyModel.theName);
							smdPathFileName = Path.Combine(modelOutputPath, aBodyModel.theSmdFileName);

							this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, smdPathFileName);
							//NOTE: Check here in case writing is canceled in the above event.
							if (this.theWritingIsCanceled)
							{
								status = AppEnums.StatusMessage.Canceled;
								return status;
							}
							else if (this.theWritingSingleFileIsCanceled)
							{
								this.theWritingSingleFileIsCanceled = false;
								continue;
							}

							this.WriteMeshSmdFile(smdPathFileName, aBodyModel);

							this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
							//If aBodyModel.theVertexes IsNot Nothing Then
							//	For vertexIndex As Integer = 0 To aBodyModel.theVertexes.Count - 1
							//		aVertex = aBodyModel.theVertexes(vertexIndex)

							//	Next
							//End If
						}
					}
				}
			}

			return status;
		}

		public override AppEnums.StatusMessage WriteBoneAnimationSmdFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlSequenceDesc10 aSequenceDesc = null;
			string smdPath = null;
			//Dim smdFileName As String
			string smdPathFileName = null;

			try
			{
				for (int aSequenceIndex = 0; aSequenceIndex < this.theMdlFileData.theSequences.Count; aSequenceIndex++)
				{
					aSequenceDesc = this.theMdlFileData.theSequences[aSequenceIndex];

					for (int blendIndex = 0; blendIndex < aSequenceDesc.blendCount; blendIndex++)
					{
						if (aSequenceDesc.blendCount == 1)
						{
							aSequenceDesc.theSmdRelativePathFileNames[blendIndex] = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames[blendIndex], this.theName, aSequenceDesc.theName, -1);
						}
						else
						{
							aSequenceDesc.theSmdRelativePathFileNames[blendIndex] = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames[blendIndex], this.theName, aSequenceDesc.theName, blendIndex);
						}

						smdPathFileName = Path.Combine(modelOutputPath, aSequenceDesc.theSmdRelativePathFileNames[blendIndex]);
						smdPath = FileManager.GetPath(smdPathFileName);
						if (FileManager.PathExistsAfterTryToCreate(smdPath))
						{
							this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, smdPathFileName);
							//NOTE: Check here in case writing is canceled in the above event.
							if (this.theWritingIsCanceled)
							{
								status = AppEnums.StatusMessage.Canceled;
								return status;
							}
							else if (this.theWritingSingleFileIsCanceled)
							{
								this.theWritingSingleFileIsCanceled = false;
								continue;
							}

							this.WriteBoneAnimationSmdFile(smdPathFileName, aSequenceDesc, blendIndex);

							this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			return status;
		}

		public override AppEnums.StatusMessage WriteTextureFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			List<SourceMdlTexture10> aTextureList = null;
			if (this.theMdlFileData.theTextures != null && this.theMdlFileData.theTextures.Count > 0)
			{
				aTextureList = this.theMdlFileData.theTextures;
			}
			else if (this.theTextureMdlFileData10 != null)
			{
				aTextureList = this.theTextureMdlFileData10.theTextures;
			}
			else
			{
				return AppEnums.StatusMessage.Error;
			}

			string texturePath = null;
			string texturePathFileName = null;
			SourceMdlTexture10 aTexture = null;
			for (int textureIndex = 0; textureIndex < aTextureList.Count; textureIndex++)
			{
				try
				{
					aTexture = aTextureList[textureIndex];
					texturePathFileName = Path.Combine(modelOutputPath, aTexture.theFileName);
					texturePath = FileManager.GetPath(texturePathFileName);
					if (FileManager.PathExistsAfterTryToCreate(texturePath))
					{
						this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, texturePathFileName);
						//NOTE: Check here in case writing is canceled in the above event.
						if (this.theWritingIsCanceled)
						{
							status = AppEnums.StatusMessage.Canceled;
							return status;
						}
						else if (this.theWritingSingleFileIsCanceled)
						{
							this.theWritingSingleFileIsCanceled = false;
							continue;
						}

						BitmapFile aBitmap = new BitmapFile(texturePathFileName, aTexture.width, aTexture.height, aTexture.theData);
						aBitmap.Write();

						this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, texturePathFileName);
					}
				}
				catch (Exception ex)
				{
					status = AppEnums.StatusMessage.Error;
				}
			}

			return status;
		}

		public AppEnums.StatusMessage WriteBoneAnimationSmdFile(string smdPathFileName, SourceMdlSequenceDesc10 aSequence, int blendIndex)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				this.theOutputFileTextWriter = File.CreateText(smdPathFileName);

				SourceSmdFile10 smdFile = new SourceSmdFile10(this.theOutputFileTextWriter, this.theMdlFileData);

				//smdFile.WriteHeaderComment()

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection();
				smdFile.WriteSkeletonSectionForAnimation(aSequence, blendIndex);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (this.theOutputFileTextWriter != null)
				{
					this.theOutputFileTextWriter.Flush();
					this.theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

		//Public Overrides Function WriteTextureFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		//	Dim status As AppEnums.StatusMessage = StatusMessage.Success

		//	Dim aTextureList As List(Of SourceMdlTexture10)
		//	If Me.theMdlFileData10.textureCount > 0 Then
		//		aTextureList = Me.theMdlFileData10.theTextures
		//	ElseIf Me.theTextureMdlFileData10 IsNot Nothing Then
		//		aTextureList = Me.theTextureMdlFileData10.theTextures
		//	Else
		//		Return StatusMessage.Error
		//	End If

		//	Dim texturePathFileName As String
		//	Dim aTexture As SourceMdlTexture10
		//	Dim managedDataArray As Byte()
		//	Dim size As Integer
		//	Dim unmanagedDataPointer As IntPtr
		//	Dim newBitmap As Bitmap
		//	For textureIndex As Integer = 0 To aTextureList.Count - 1
		//		Try
		//			aTexture = aTextureList(textureIndex)
		//			texturePathFileName = Path.Combine(modelOutputPath, aTexture.theFileName)

		//			managedDataArray = aTexture.theData.ToArray()

		//			size = Marshal.SizeOf(managedDataArray(0)) * managedDataArray.Length
		//			unmanagedDataPointer = Marshal.AllocHGlobal(size)
		//			Marshal.Copy(managedDataArray, 0, unmanagedDataPointer, managedDataArray.Length)
		//			newBitmap = New Bitmap(aTexture.width, aTexture.height, 8 * aTexture.width, System.Drawing.Imaging.PixelFormat.Format32bppRgb, unmanagedDataPointer)
		//			newBitmap.Save(texturePathFileName, System.Drawing.Imaging.ImageFormat.Bmp)
		//		Catch ex As Exception
		//			status = StatusMessage.Error
		//		Finally
		//			Marshal.FreeHGlobal(unmanagedDataPointer)
		//		End Try
		//	Next

		//	Return status
		//End Function

		public override AppEnums.StatusMessage WriteAccessedBytesDebugFiles(string debugPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			string debugPathFileName = null;

			if (this.theMdlFileData != null)
			{
				debugPathFileName = Path.Combine(debugPath, this.theName + " " + Properties.Resources.Decompile_DebugMdlFileNameSuffix);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				this.WriteAccessedBytesDebugFile(debugPathFileName, this.theMdlFileData.theFileSeekLog);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (this.theSequenceGroupMdlFileDatas10 != null)
			{
				string fileName = null;
				string fileNameWithoutExtension = null;
				string fileExtension = null;
				for (int i = 0; i < this.theSequenceGroupMdlFileDatas10.Count; i++)
				{
					fileName = this.theName + " " + Properties.Resources.Decompile_DebugSequenceGroupMDLFileNameSuffix;
					fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
					fileExtension = Path.GetExtension(fileName);
					debugPathFileName = Path.Combine(debugPath, fileNameWithoutExtension + (i + 1).ToString("00") + fileExtension);
					this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
					this.WriteAccessedBytesDebugFile(debugPathFileName, this.theSequenceGroupMdlFileDatas10[i].theFileSeekLog);
					this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
				}
			}

			if (this.theTextureMdlFileData10 != null)
			{
				debugPathFileName = Path.Combine(debugPath, this.theName + " " + Properties.Resources.Decompile_DebugTextureMDLFileNameSuffix);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				this.WriteAccessedBytesDebugFile(debugPathFileName, this.theTextureMdlFileData10.theFileSeekLog);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			return status;
		}

		public override List<string> GetTextureFileNames()
		{
			List<string> textureFileNames = new List<string>();

			for (int i = 0; i < this.theMdlFileData.theTextures.Count; i++)
			{
				SourceMdlTexture10 aTexture = this.theMdlFileData.theTextures[i];

				textureFileNames.Add(aTexture.theFileName);
			}

			return textureFileNames;
		}

#endregion

#region Private Methods

		protected override void ReadMdlFileHeader_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData10();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile10 mdlFile = new SourceMdlFile10(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData10();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile10 mdlFile = new SourceMdlFile10(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();

			//mdlFile.ReadTexturePaths()
			mdlFile.ReadTextures();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData10();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			this.theMdlFileData.theFileName = this.theName;
			SourceMdlFile10 mdlFile = new SourceMdlFile10(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();

			// Read what WriteBoneInfo() writes.
			mdlFile.ReadBones();
			mdlFile.ReadBoneControllers();
			mdlFile.ReadAttachments();
			mdlFile.ReadHitboxes();

			// Read what WriteSequenceInfo() writes.
			//NOTE: Must read sequences before reading animations.
			mdlFile.ReadSequences();
			mdlFile.ReadSequenceGroups();
			mdlFile.ReadTransitions();

			// Read what WriteAnimations() writes.
			mdlFile.ReadAnimations(0);

			// Read what WriteModel() writes.
			mdlFile.ReadBodyParts();

			// Read what WriteTextures() writes.
			mdlFile.ReadTextures();
			mdlFile.ReadSkins();

			mdlFile.ReadUnreadBytes();

			// Post-processing.
			mdlFile.BuildBoneTransforms();
		}

		protected override void ReadSequenceGroupMdlFile(int sequenceGroupIndex)
		{
			if (this.theSequenceGroupMdlFileDatas10 == null)
			{
				this.theSequenceGroupMdlFileDatas10 = new List<SourceMdlFileData10>();
			}

			SourceMdlFileData10 aSequenceGroupMdlFileData10 = new SourceMdlFileData10();
			//NOTE: Need some data from the main MDL file.
			aSequenceGroupMdlFileData10.theBones = this.theMdlFileData.theBones;
			aSequenceGroupMdlFileData10.theSequences = this.theMdlFileData.theSequences;

			SourceMdlFile10 sequenceGroupMdlFile = new SourceMdlFile10(this.theInputFileReader, aSequenceGroupMdlFileData10);

			sequenceGroupMdlFile.ReadSequenceGroupMdlHeader();
			this.theMdlFileData.theSequenceGroupFileHeaders[sequenceGroupIndex].theActualFileSize = aSequenceGroupMdlFileData10.theActualFileSize;
			sequenceGroupMdlFile.ReadAnimations(sequenceGroupIndex);

			this.theSequenceGroupMdlFileDatas10.Add(aSequenceGroupMdlFileData10);
		}

		protected override void ReadTextureMdlFile_Internal()
		{
			if (this.theTextureMdlFileData10 == null)
			{
				this.theTextureMdlFileData10 = new SourceMdlFileData10();
			}

			SourceMdlFile10 textureMdlFile = new SourceMdlFile10(this.theInputFileReader, this.theTextureMdlFileData10);

			textureMdlFile.ReadMdlHeader();
			textureMdlFile.ReadTextures();
			textureMdlFile.ReadSkins();

			if (this.theMdlFileData.theTextures == null)
			{
				this.theExternalTexturesAreUsed = true;
			}
		}

		protected override void WriteQcFile()
		{
			if (this.theExternalTexturesAreUsed)
			{
				this.theMdlFileData.skinReferenceCount = this.theTextureMdlFileData10.skinReferenceCount;
				this.theMdlFileData.skinFamilyCount = this.theTextureMdlFileData10.skinFamilyCount;
				this.theMdlFileData.theSkinFamilies = this.theTextureMdlFileData10.theSkinFamilies;
				this.theMdlFileData.theTextures = this.theTextureMdlFileData10.theTextures;
			}

			SourceQcFile10 qcFile = new SourceQcFile10(this.theOutputFileTextWriter, this.theQcPathFileName, this.theMdlFileData, this.theName);

			try
			{
				qcFile.WriteHeaderComment();

				qcFile.WriteModelNameCommand();
				qcFile.WriteCDCommand();
				qcFile.WriteCDTextureCommand();
				qcFile.WriteClipToTexturesCommand();
				qcFile.WriteScaleCommand();

				qcFile.WriteBodyGroupCommand();

				qcFile.WriteFlagsCommand();
				qcFile.WriteEyePositionCommand();

				qcFile.WriteExternalTexturesCommand();
				qcFile.WriteTextureGroupCommand();
				//If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				//	qcFile.WriteTextureFileNameComments()
				//End If
				qcFile.WriteTexRenderMode();

				qcFile.WriteAttachmentCommand();

				qcFile.WriteCBoxCommand();
				qcFile.WriteBBoxCommand();
				qcFile.WriteHBoxCommands();

				qcFile.WriteControllerCommand();

				qcFile.WriteSequenceGroupSizeCommand();
				//qcFile.WriteSequenceGroupCommands()
				qcFile.WriteSequenceCommands();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}

			if (this.theExternalTexturesAreUsed)
			{
				this.theMdlFileData.skinReferenceCount = 0;
				this.theMdlFileData.skinFamilyCount = 0;
				this.theMdlFileData.theSkinFamilies = null;
				this.theMdlFileData.theTextures = null;
			}
		}

		protected AppEnums.StatusMessage WriteMeshSmdFile(string smdPathFileName, SourceMdlModel10 aModel)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				this.theOutputFileTextWriter = File.CreateText(smdPathFileName);

				this.WriteMeshSmdFile(aModel);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (this.theOutputFileTextWriter != null)
				{
					this.theOutputFileTextWriter.Flush();
					this.theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

		protected void WriteMeshSmdFile(SourceMdlModel10 aModel)
		{
			if (this.theExternalTexturesAreUsed)
			{
				this.theMdlFileData.theTextures = this.theTextureMdlFileData10.theTextures;
			}

			SourceSmdFile10 smdFile = new SourceSmdFile10(this.theOutputFileTextWriter, this.theMdlFileData);

			try
			{
				//smdFile.WriteHeaderComment()

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection();
				smdFile.WriteSkeletonSection();
				smdFile.WriteTrianglesSection(aModel);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			if (this.theExternalTexturesAreUsed)
			{
				this.theMdlFileData.theTextures = null;
			}
		}

		protected override void WriteMdlFileNameToMdlFile(string internalMdlFileName)
		{
			SourceMdlFile10 mdlFile = new SourceMdlFile10(this.theOutputFileBinaryWriter, this.theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
		}

#endregion

#region Data

		private SourceMdlFileData10 theMdlFileData;
		private List<SourceMdlFileData10> theSequenceGroupMdlFileDatas10;
		private SourceMdlFileData10 theTextureMdlFileData10;
		private bool theExternalTexturesAreUsed;

#endregion

	}

}