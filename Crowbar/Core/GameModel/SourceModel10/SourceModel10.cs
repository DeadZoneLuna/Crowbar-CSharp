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

			theExternalTexturesAreUsed = false;
		}

#endregion

#region Properties

		public override bool SequenceGroupMdlFilesAreUsed
		{
			get
			{
				return theMdlFileData != null && theMdlFileData.sequenceGroupCount > 1 && theMdlFileData.sequenceGroupCount == theSequenceGroupMdlPathFileNames.Count;
			}
		}

		public override bool TextureMdlFileIsUsed
		{
			get
			{
				return theMdlFileData.textureCount == 0 && !string.IsNullOrEmpty(theTextureMdlPathFileName);
			}
		}

		public override bool HasTextureData
		{
			get
			{
				return theMdlFileData.textureCount > 0 || theTextureMdlFileData10 != null;
			}
		}

		public override bool HasMeshData
		{
			get
			{
				if (theMdlFileData.theBones != null && theMdlFileData.theBones.Count > 0)
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
				if (theMdlFileData.theSequences != null && theMdlFileData.theSequences.Count > 0)
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
				return theMdlFileData.textureCount > 0 || theTextureMdlFileData10 != null;
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
				mdlPath = FileManager.GetPath(theMdlPathFileName);
				mdlFileNameWithoutExtension = Path.GetFileNameWithoutExtension(theMdlPathFileName);
				mdlExtension = Path.GetExtension(theMdlPathFileName);

				theSequenceGroupMdlPathFileNames = new List<string>(theMdlFileData.sequenceGroupCount);

				theSequenceGroupMdlPathFileNames.Add(theMdlPathFileName);
				//NOTE: Start index at 1 because 0 is the main MDL file, handled above.
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string aSequenceGroupMdlFileName = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string aSequenceGroupMdlPathFileName = null;
				for (int sequenceGroupIndex = 1; sequenceGroupIndex < theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
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
					theSequenceGroupMdlPathFileNames.Add(aSequenceGroupMdlPathFileName);

					if (!File.Exists(aSequenceGroupMdlPathFileName))
					{
						status = AppEnums.FilesFoundFlags.ErrorRequiredSequenceGroupMdlFileNotFound;
						return status;
					}
				}

				if (theMdlFileData.textureCount == 0)
				{
					textureMdlFileName = mdlFileNameWithoutExtension + "T" + mdlExtension;
					theTextureMdlPathFileName = Path.Combine(mdlPath, textureMdlFileName);
					if (!File.Exists(theTextureMdlPathFileName))
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
			for (int sequenceGroupIndex = 1; sequenceGroupIndex < theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
			{
				aSequenceGroup = theMdlFileData.theSequenceGroups[sequenceGroupIndex];

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
				mdlPath = FileManager.GetPath(theMdlPathFileName);
				sequenceGroupMdlFileName = Path.GetFileNameWithoutExtension(theMdlPathFileName) + sequenceGroupIndex.ToString("00") + ".mdl";
				sequenceGroupMdlPathFileName = Path.Combine(mdlPath, sequenceGroupMdlFileName);

				status = ReadSequenceGroupMdlFile(sequenceGroupMdlPathFileName, sequenceGroupIndex);
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
					ReadFile(theTextureMdlPathFileName, ReadTextureMdlFile_Internal);
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
			if (theMdlFileData.theBodyParts != null)
			{
				for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];

					if (aBodyPart.theModels != null)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theModels.Count; modelIndex++)
						{
							aBodyModel = aBodyPart.theModels[modelIndex];
							if (aBodyModel.theName == "blank")
							{
								continue;
							}

							aBodyModel.theSmdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileName, bodyPartIndex, modelIndex, 0, theName, aBodyModel.theName);
							smdPathFileName = Path.Combine(modelOutputPath, aBodyModel.theSmdFileName);

							NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, smdPathFileName);
							//NOTE: Check here in case writing is canceled in the above event.
							if (theWritingIsCanceled)
							{
								status = AppEnums.StatusMessage.Canceled;
								return status;
							}
							else if (theWritingSingleFileIsCanceled)
							{
								theWritingSingleFileIsCanceled = false;
								continue;
							}

							WriteMeshSmdFile(smdPathFileName, aBodyModel);

							NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
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
				for (int aSequenceIndex = 0; aSequenceIndex < theMdlFileData.theSequences.Count; aSequenceIndex++)
				{
					aSequenceDesc = theMdlFileData.theSequences[aSequenceIndex];

					for (int blendIndex = 0; blendIndex < aSequenceDesc.blendCount; blendIndex++)
					{
						if (aSequenceDesc.blendCount == 1)
						{
							aSequenceDesc.theSmdRelativePathFileNames[blendIndex] = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames[blendIndex], theName, aSequenceDesc.theName, -1);
						}
						else
						{
							aSequenceDesc.theSmdRelativePathFileNames[blendIndex] = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames[blendIndex], theName, aSequenceDesc.theName, blendIndex);
						}

						smdPathFileName = Path.Combine(modelOutputPath, aSequenceDesc.theSmdRelativePathFileNames[blendIndex]);
						smdPath = FileManager.GetPath(smdPathFileName);
						if (FileManager.PathExistsAfterTryToCreate(smdPath))
						{
							NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, smdPathFileName);
							//NOTE: Check here in case writing is canceled in the above event.
							if (theWritingIsCanceled)
							{
								status = AppEnums.StatusMessage.Canceled;
								return status;
							}
							else if (theWritingSingleFileIsCanceled)
							{
								theWritingSingleFileIsCanceled = false;
								continue;
							}

							WriteBoneAnimationSmdFile(smdPathFileName, aSequenceDesc, blendIndex);

							NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
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
			if (theMdlFileData.theTextures != null && theMdlFileData.theTextures.Count > 0)
			{
				aTextureList = theMdlFileData.theTextures;
			}
			else if (theTextureMdlFileData10 != null)
			{
				aTextureList = theTextureMdlFileData10.theTextures;
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
						NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, texturePathFileName);
						//NOTE: Check here in case writing is canceled in the above event.
						if (theWritingIsCanceled)
						{
							status = AppEnums.StatusMessage.Canceled;
							return status;
						}
						else if (theWritingSingleFileIsCanceled)
						{
							theWritingSingleFileIsCanceled = false;
							continue;
						}

						BitmapFile aBitmap = new BitmapFile(texturePathFileName, aTexture.width, aTexture.height, aTexture.theData);
						aBitmap.Write();

						NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, texturePathFileName);
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
				theOutputFileTextWriter = File.CreateText(smdPathFileName);

				SourceSmdFile10 smdFile = new SourceSmdFile10(theOutputFileTextWriter, theMdlFileData);

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
				if (theOutputFileTextWriter != null)
				{
					theOutputFileTextWriter.Flush();
					theOutputFileTextWriter.Close();
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

			if (theMdlFileData != null)
			{
				debugPathFileName = Path.Combine(debugPath, theName + " " + Properties.Resources.Decompile_DebugMdlFileNameSuffix);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				WriteAccessedBytesDebugFile(debugPathFileName, theMdlFileData.theFileSeekLog);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (theSequenceGroupMdlFileDatas10 != null)
			{
				string fileName = null;
				string fileNameWithoutExtension = null;
				string fileExtension = null;
				for (int i = 0; i < theSequenceGroupMdlFileDatas10.Count; i++)
				{
					fileName = theName + " " + Properties.Resources.Decompile_DebugSequenceGroupMDLFileNameSuffix;
					fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
					fileExtension = Path.GetExtension(fileName);
					debugPathFileName = Path.Combine(debugPath, fileNameWithoutExtension + (i + 1).ToString("00") + fileExtension);
					NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
					WriteAccessedBytesDebugFile(debugPathFileName, theSequenceGroupMdlFileDatas10[i].theFileSeekLog);
					NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
				}
			}

			if (theTextureMdlFileData10 != null)
			{
				debugPathFileName = Path.Combine(debugPath, theName + " " + Properties.Resources.Decompile_DebugTextureMDLFileNameSuffix);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				WriteAccessedBytesDebugFile(debugPathFileName, theTextureMdlFileData10.theFileSeekLog);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			return status;
		}

		public override List<string> GetTextureFileNames()
		{
			List<string> textureFileNames = new List<string>();

			for (int i = 0; i < theMdlFileData.theTextures.Count; i++)
			{
				SourceMdlTexture10 aTexture = theMdlFileData.theTextures[i];

				textureFileNames.Add(aTexture.theFileName);
			}

			return textureFileNames;
		}

#endregion

#region Private Methods

		protected override void ReadMdlFileHeader_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData10();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile10 mdlFile = new SourceMdlFile10(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader();
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData10();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile10 mdlFile = new SourceMdlFile10(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader();

			//mdlFile.ReadTexturePaths()
			mdlFile.ReadTextures();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData10();
				theMdlFileDataGeneric = theMdlFileData;
			}

			theMdlFileData.theFileName = theName;
			SourceMdlFile10 mdlFile = new SourceMdlFile10(theInputFileReader, theMdlFileData);

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
			if (theSequenceGroupMdlFileDatas10 == null)
			{
				theSequenceGroupMdlFileDatas10 = new List<SourceMdlFileData10>();
			}

			SourceMdlFileData10 aSequenceGroupMdlFileData10 = new SourceMdlFileData10();
			//NOTE: Need some data from the main MDL file.
			aSequenceGroupMdlFileData10.theBones = theMdlFileData.theBones;
			aSequenceGroupMdlFileData10.theSequences = theMdlFileData.theSequences;

			SourceMdlFile10 sequenceGroupMdlFile = new SourceMdlFile10(theInputFileReader, aSequenceGroupMdlFileData10);

			sequenceGroupMdlFile.ReadSequenceGroupMdlHeader();
			theMdlFileData.theSequenceGroupFileHeaders[sequenceGroupIndex].theActualFileSize = aSequenceGroupMdlFileData10.theActualFileSize;
			sequenceGroupMdlFile.ReadAnimations(sequenceGroupIndex);

			theSequenceGroupMdlFileDatas10.Add(aSequenceGroupMdlFileData10);
		}

		protected override void ReadTextureMdlFile_Internal()
		{
			if (theTextureMdlFileData10 == null)
			{
				theTextureMdlFileData10 = new SourceMdlFileData10();
			}

			SourceMdlFile10 textureMdlFile = new SourceMdlFile10(theInputFileReader, theTextureMdlFileData10);

			textureMdlFile.ReadMdlHeader();
			textureMdlFile.ReadTextures();
			textureMdlFile.ReadSkins();

			if (theMdlFileData.theTextures == null)
			{
				theExternalTexturesAreUsed = true;
			}
		}

		protected override void WriteQcFile()
		{
			if (theExternalTexturesAreUsed)
			{
				theMdlFileData.skinReferenceCount = theTextureMdlFileData10.skinReferenceCount;
				theMdlFileData.skinFamilyCount = theTextureMdlFileData10.skinFamilyCount;
				theMdlFileData.theSkinFamilies = theTextureMdlFileData10.theSkinFamilies;
				theMdlFileData.theTextures = theTextureMdlFileData10.theTextures;
			}

			SourceQcFile10 qcFile = new SourceQcFile10(theOutputFileTextWriter, theQcPathFileName, theMdlFileData, theName);

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

			if (theExternalTexturesAreUsed)
			{
				theMdlFileData.skinReferenceCount = 0;
				theMdlFileData.skinFamilyCount = 0;
				theMdlFileData.theSkinFamilies = null;
				theMdlFileData.theTextures = null;
			}
		}

		protected AppEnums.StatusMessage WriteMeshSmdFile(string smdPathFileName, SourceMdlModel10 aModel)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				theOutputFileTextWriter = File.CreateText(smdPathFileName);

				WriteMeshSmdFile(aModel);
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

		protected void WriteMeshSmdFile(SourceMdlModel10 aModel)
		{
			if (theExternalTexturesAreUsed)
			{
				theMdlFileData.theTextures = theTextureMdlFileData10.theTextures;
			}

			SourceSmdFile10 smdFile = new SourceSmdFile10(theOutputFileTextWriter, theMdlFileData);

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

			if (theExternalTexturesAreUsed)
			{
				theMdlFileData.theTextures = null;
			}
		}

		protected override void WriteMdlFileNameToMdlFile(string internalMdlFileName)
		{
			SourceMdlFile10 mdlFile = new SourceMdlFile10(theOutputFileBinaryWriter, theMdlFileData);

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