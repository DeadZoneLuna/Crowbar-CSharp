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
	public class SourceModel14 : SourceModel06
	{
#region Creation and Destruction

		public SourceModel14(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{

			//Me.theExternalTexturesAreUsed = False
		}

#endregion

#region Properties

		public override bool SequenceGroupMdlFilesAreUsed
		{
			get
			{
				//Return Me.theMdlFileData IsNot Nothing AndAlso Me.theMdlFileData.sequenceGroupCount > 1 AndAlso Me.theMdlFileData.sequenceGroupCount = Me.theSequenceGroupMdlPathFileNames.Count
				return false;
			}
		}

		public override bool TextureMdlFileIsUsed
		{
			get
			{
				//Return Me.theMdlFileData.textureCount = 0 AndAlso Not String.IsNullOrEmpty(Me.theTextureMdlPathFileName)
				return false;
			}
		}

		public override bool HasTextureData
		{
			get
			{
				//Return Me.theMdlFileData.textureCount > 0 OrElse Me.theTextureMdlFileData10 IsNot Nothing
				return false;
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
				//Return Me.theMdlFileData.textureCount > 0 OrElse Me.theTextureMdlFileData10 IsNot Nothing
				return false;
			}
		}

#endregion

#region Methods

		public override AppEnums.FilesFoundFlags CheckForRequiredFiles()
		{
			AppEnums.FilesFoundFlags status = AppEnums.FilesFoundFlags.AllFilesFound;

			//Dim mdlPath As String
			//Dim mdlFileNameWithoutExtension As String
			//Dim mdlExtension As String
			//Dim textureMdlFileName As String

			//Try
			//	mdlPath = FileManager.GetPath(Me.theMdlPathFileName)
			//	mdlFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Me.theMdlPathFileName)
			//	mdlExtension = Path.GetExtension(Me.theMdlPathFileName)

			//	'TODO: Fill theSequenceGroupMdlPathFileNames with actual names stored as is done in ReadSequenceGroupMdlFiles().
			//	'      Requires reading in the SequenceGroup data.
			//	Me.theSequenceGroupMdlPathFileNames = New List(Of String)(Me.theMdlFileData.sequenceGroupCount)

			//	Me.theSequenceGroupMdlPathFileNames.Add(Me.theMdlPathFileName)
			//	'NOTE: Start index at 1 because 0 is the main MDL file, handled above.
			//	For sequenceGroupIndex As Integer = 1 To Me.theMdlFileData.sequenceGroupCount - 1
			//		Dim aSequenceGroupMdlFileName As String
			//		Dim aSequenceGroupMdlPathFileName As String
			//		'sequenceGroupMdlFileName = Path.GetFileName(aSequenceGroup.theFileName)
			//		'sequenceGroupMdlPathFileName = Path.Combine(mdlPath, sequenceGroupMdlFileName)
			//		aSequenceGroupMdlFileName = mdlFileNameWithoutExtension + sequenceGroupIndex.ToString("00") + mdlExtension
			//		aSequenceGroupMdlPathFileName = Path.Combine(mdlPath, aSequenceGroupMdlFileName)
			//		'If Not File.Exists(aSequenceGroupMdlPathFileName) Then
			//		'	status = StatusMessage.Error
			//		'End If
			//		Me.theSequenceGroupMdlPathFileNames.Add(aSequenceGroupMdlPathFileName)

			//		If Not File.Exists(aSequenceGroupMdlPathFileName) Then
			//			status = FilesFoundFlags.ErrorRequiredSequenceGroupMdlFileNotFound
			//			Return status
			//		End If
			//	Next

			//	If Me.theMdlFileData.textureCount = 0 Then
			//		textureMdlFileName = mdlFileNameWithoutExtension + "T" + mdlExtension
			//		Me.theTextureMdlPathFileName = Path.Combine(mdlPath, textureMdlFileName)
			//		If Not File.Exists(Me.theTextureMdlPathFileName) Then
			//			status = FilesFoundFlags.ErrorRequiredTextureMdlFileNotFound
			//			Return status
			//		End If
			//	End If
			//Catch ex As Exception
			//	status = FilesFoundFlags.Error
			//End Try

			return status;
		}

		public override AppEnums.StatusMessage ReadSequenceGroupMdlFiles()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlSequenceGroup10 aSequenceGroup = null;
			string mdlPath = null;
			string sequenceGroupMdlFileName = null;
			string sequenceGroupMdlPathFileName = null;

			//NOTE: Start at index 1 because sequence group 0 is in the main MDL file.
			for (int sequenceGroupIndex = 1; sequenceGroupIndex < this.theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
			{
				aSequenceGroup = this.theMdlFileData.theSequenceGroups[sequenceGroupIndex];
				mdlPath = FileManager.GetPath(this.theMdlPathFileName);
				sequenceGroupMdlFileName = Path.GetFileName(aSequenceGroup.theFileName);
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
			SourceMdlBodyPart14 aBodyPart = null;
			SourceMdlModel14 aBodyModel = null;
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

		//Public Overrides Function WriteTextureFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		//	Dim status As AppEnums.StatusMessage = StatusMessage.Success

		//	Dim aTextureList As List(Of SourceMdlTexture14)
		//	If Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 Then
		//		aTextureList = Me.theMdlFileData.theTextures
		//	ElseIf Me.theTextureMdlFileData10 IsNot Nothing Then
		//		aTextureList = Me.theTextureMdlFileData10.theTextures
		//	Else
		//		Return StatusMessage.Error
		//	End If

		//	Dim texturePath As String
		//	Dim texturePathFileName As String
		//	Dim aTexture As SourceMdlTexture14
		//	For textureIndex As Integer = 0 To aTextureList.Count - 1
		//		Try
		//			aTexture = aTextureList(textureIndex)
		//			texturePathFileName = Path.Combine(modelOutputPath, aTexture.theFileName)
		//			texturePath = FileManager.GetPath(texturePathFileName)
		//			If FileManager.PathExistsAfterTryToCreate(texturePath) Then
		//				Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, texturePathFileName)
		//				'NOTE: Check here in case writing is canceled in the above event.
		//				If Me.theWritingIsCanceled Then
		//					status = StatusMessage.Canceled
		//					Return status
		//				ElseIf Me.theWritingSingleFileIsCanceled Then
		//					Me.theWritingSingleFileIsCanceled = False
		//					Continue For
		//				End If

		//				Dim aBitmap As New BitmapFile(texturePathFileName, aTexture.width, aTexture.height, aTexture.theData)
		//				aBitmap.Write()

		//				Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, texturePathFileName)
		//			End If
		//		Catch ex As Exception
		//			status = StatusMessage.Error
		//		End Try
		//	Next

		//	Return status
		//End Function

		public AppEnums.StatusMessage WriteBoneAnimationSmdFile(string smdPathFileName, SourceMdlSequenceDesc10 aSequence, int blendIndex)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				this.theOutputFileTextWriter = File.CreateText(smdPathFileName);

				SourceSmdFile14 smdFile = new SourceSmdFile14(this.theOutputFileTextWriter, this.theMdlFileData);

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

		//	Dim aTextureList As List(Of SourceMdlTexture14)
		//	If Me.theMdlFileData10.textureCount > 0 Then
		//		aTextureList = Me.theMdlFileData10.theTextures
		//	ElseIf Me.theTextureMdlFileData10 IsNot Nothing Then
		//		aTextureList = Me.theTextureMdlFileData10.theTextures
		//	Else
		//		Return StatusMessage.Error
		//	End If

		//	Dim texturePathFileName As String
		//	Dim aTexture As SourceMdlTexture14
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

			//If Me.theSequenceGroupMdlFileDatas10 IsNot Nothing Then
			//	Dim fileName As String
			//	Dim fileNameWithoutExtension As String
			//	Dim fileExtension As String
			//	For i As Integer = 0 To Me.theSequenceGroupMdlFileDatas10.Count - 1
			//		fileName = Me.theName + " " + My.Resources.Decompile_DebugSequenceGroupMDLFileNameSuffix
			//		fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName)
			//		fileExtension = Path.GetExtension(fileName)
			//		debugPathFileName = Path.Combine(debugPath, fileNameWithoutExtension + (i + 1).ToString("00") + fileExtension)
			//		Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			//		Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theSequenceGroupMdlFileDatas10(i).theFileSeekLog)
			//		Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
			//	Next
			//End If

			//If Me.theTextureMdlFileData10 IsNot Nothing Then
			//	debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugTextureMDLFileNameSuffix)
			//	Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, debugPathFileName)
			//	Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theTextureMdlFileData10.theFileSeekLog)
			//	Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, debugPathFileName)
			//End If

			return status;
		}

		public override List<string> GetTextureFileNames()
		{
			List<string> textureFileNames = new List<string>();

			for (int i = 0; i < this.theMdlFileData.theTextures.Count; i++)
			{
				SourceMdlTexture14 aTexture = this.theMdlFileData.theTextures[i];

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
				this.theMdlFileData = new SourceMdlFileData14();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile14 mdlFile = new SourceMdlFile14(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData14();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile14 mdlFile = new SourceMdlFile14(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();

			//'mdlFile.ReadTexturePaths()
			//mdlFile.ReadTextures()
		}

		protected override void ReadMdlFile_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData14();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			this.theMdlFileData.theFileName = this.theName;
			SourceMdlFile14 mdlFile = new SourceMdlFile14(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();

			mdlFile.ReadBones();
			mdlFile.ReadBoneControllers();
			mdlFile.ReadAttachments();
			mdlFile.ReadHitboxes();

			//NOTE: Must read sequences before reading animations.
			mdlFile.ReadSequences();
			mdlFile.ReadSequenceGroups();
			//mdlFile.ReadTransitions()

			mdlFile.ReadAnimations(0);

			mdlFile.ReadBodyParts();
			mdlFile.ReadIndexes();
			mdlFile.ReadVertexes();
			mdlFile.ReadNormals();
			mdlFile.ReadUVs();
			mdlFile.ReadWeightingWeights();
			mdlFile.ReadWeightingBones();

			mdlFile.ReadTextures();
			mdlFile.ReadSkins();

			mdlFile.ReadUnreadBytes();

			// Post-processing.
			//mdlFile.BuildBoneTransforms()
		}

		//Protected Overrides Sub ReadSequenceGroupMdlFile(ByVal sequenceGroupIndex As Integer)
		//	If Me.theSequenceGroupMdlFileDatas10 Is Nothing Then
		//		Me.theSequenceGroupMdlFileDatas10 = New List(Of SourceMdlFileData14)()
		//	End If

		//	Dim aSequenceGroupMdlFileData10 As New SourceMdlFileData14()
		//	'NOTE: Need some data from the main MDL file.
		//	aSequenceGroupMdlFileData10.theBones = Me.theMdlFileData.theBones
		//	aSequenceGroupMdlFileData10.theSequences = Me.theMdlFileData.theSequences

		//	Dim sequenceGroupMdlFile As New SourceMdlFile10(Me.theInputFileReader, aSequenceGroupMdlFileData10)

		//	sequenceGroupMdlFile.ReadSequenceGroupMdlHeader()
		//	Me.theMdlFileData.theSequenceGroupFileHeaders(sequenceGroupIndex).theActualFileSize = aSequenceGroupMdlFileData10.theActualFileSize
		//	sequenceGroupMdlFile.ReadAnimations(sequenceGroupIndex)

		//	Me.theSequenceGroupMdlFileDatas10.Add(aSequenceGroupMdlFileData10)
		//End Sub

		//Protected Overrides Sub ReadTextureMdlFile_Internal()
		//	If Me.theTextureMdlFileData10 Is Nothing Then
		//		Me.theTextureMdlFileData10 = New SourceMdlFileData14()
		//	End If

		//	Dim textureMdlFile As New SourceMdlFile10(Me.theInputFileReader, Me.theTextureMdlFileData10)

		//	textureMdlFile.ReadMdlHeader()
		//	textureMdlFile.ReadTextures()
		//	textureMdlFile.ReadSkins()

		//	If Me.theMdlFileData.theTextures Is Nothing Then
		//		Me.theExternalTexturesAreUsed = True
		//	End If
		//End Sub

		protected override void WriteQcFile()
		{
			//If Me.theExternalTexturesAreUsed Then
			//	Me.theMdlFileData.skinReferenceCount = Me.theTextureMdlFileData10.skinReferenceCount
			//	Me.theMdlFileData.skinFamilyCount = Me.theTextureMdlFileData10.skinFamilyCount
			//	Me.theMdlFileData.theSkinFamilies = Me.theTextureMdlFileData10.theSkinFamilies
			//	Me.theMdlFileData.theTextures = Me.theTextureMdlFileData10.theTextures
			//End If

			SourceQcFile14 qcFile = new SourceQcFile14(this.theOutputFileTextWriter, this.theQcPathFileName, this.theMdlFileData, this.theName);

			try
			{
				qcFile.WriteHeaderComment();

				qcFile.WriteModelNameCommand();
				//qcFile.WriteCDCommand()
				//qcFile.WriteCDTextureCommand()
				//qcFile.WriteClipToTexturesCommand()
				//qcFile.WriteScaleCommand()

				qcFile.WriteBodyGroupCommand();

				qcFile.WriteFlagsCommand();
				qcFile.WriteEyePositionCommand();

				//qcFile.WriteExternalTexturesCommand()
				//qcFile.WriteTextureGroupCommand()
				//If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				//	qcFile.WriteTextureFileNameComments()
				//End If
				//qcFile.WriteTexRenderMode()

				//qcFile.WriteAttachmentCommand()

				qcFile.WriteCBoxCommand();
				qcFile.WriteBBoxCommand();
				//qcFile.WriteHBoxCommands()

				//qcFile.WriteControllerCommand()

				//qcFile.WriteSequenceGroupSizeCommand()
				//qcFile.WriteSequenceGroupCommands()
				//qcFile.WriteSequenceCommands()
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}

			//If Me.theExternalTexturesAreUsed Then
			//	Me.theMdlFileData.skinReferenceCount = 0
			//	Me.theMdlFileData.skinFamilyCount = 0
			//	Me.theMdlFileData.theSkinFamilies = Nothing
			//	Me.theMdlFileData.theTextures = Nothing
			//End If
		}

		protected AppEnums.StatusMessage WriteMeshSmdFile(string smdPathFileName, SourceMdlModel14 aModel)
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

		protected void WriteMeshSmdFile(SourceMdlModel14 aModel)
		{
			//If Me.theExternalTexturesAreUsed Then
			//	Me.theMdlFileData.theTextures = Me.theTextureMdlFileData10.theTextures
			//End If

			SourceSmdFile14 smdFile = new SourceSmdFile14(this.theOutputFileTextWriter, this.theMdlFileData);

			try
			{
				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection();
				smdFile.WriteSkeletonSection();
				smdFile.WriteTrianglesSection(aModel);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			//If Me.theExternalTexturesAreUsed Then
			//	Me.theMdlFileData.theTextures = Nothing
			//End If
		}

		protected override void WriteMdlFileNameToMdlFile(string internalMdlFileName)
		{
			SourceMdlFile14 mdlFile = new SourceMdlFile14(this.theOutputFileBinaryWriter, this.theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
		}

#endregion

#region Data

		private SourceMdlFileData14 theMdlFileData;
		//Private theSequenceGroupMdlFileDatas10 As List(Of SourceMdlFileData14)
		//Private theTextureMdlFileData10 As SourceMdlFileData14
		//Private theExternalTexturesAreUsed As Boolean

#endregion

	}

}