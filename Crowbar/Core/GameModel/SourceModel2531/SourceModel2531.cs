//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Crowbar
{
	public class SourceModel2531 : SourceModel10
	{
#region Creation and Destruction

		public SourceModel2531(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{
		}

#endregion

#region Properties

		public override bool SequenceGroupMdlFilesAreUsed
		{
			get
			{
				return false;
			}
		}

		public override bool TextureMdlFileIsUsed
		{
			get
			{
				return false;
			}
		}

		public override bool PhyFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(thePhyPathFileName) && File.Exists(thePhyPathFileName);
			}
		}

		public override bool VtxFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(theVtxPathFileName) && File.Exists(theVtxPathFileName);
			}
		}

		public override bool HasTextureData
		{
			get
			{
				return !theMdlFileDataGeneric.theMdlFileOnlyHasAnimations && theMdlFileData.theTextures != null && theMdlFileData.theTextures.Count > 0;
			}
		}

		public override bool HasMeshData
		{
			get
			{
				//TODO: [HasMeshData] Should check more than theBones.
				if (!theMdlFileDataGeneric.theMdlFileOnlyHasAnimations && theMdlFileData.theBones != null && theMdlFileData.theBones.Count > 0 && theVtxFileData != null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public override bool HasLodMeshData
		{
			get
			{
				if (!theMdlFileData.theMdlFileOnlyHasAnimations && theMdlFileData.theBones != null && theMdlFileData.theBones.Count > 0 && theVtxFileData != null && theVtxFileData.lodCount > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public override bool HasPhysicsMeshData
		{
			get
			{
				if (thePhyFileDataGeneric != null && thePhyFileDataGeneric.theSourcePhyCollisionDatas != null && !theMdlFileData.theMdlFileOnlyHasAnimations && theMdlFileData.theBones != null && theMdlFileData.theBones.Count > 0)
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
				if (theMdlFileData.theSequences != null && theMdlFileData.theSequences.Count > 0 && theMdlFileData.theAnimationDescs != null && theMdlFileData.theAnimationDescs.Count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public override bool HasVertexAnimationData
		{
			get
			{
				if (!theMdlFileData.theMdlFileOnlyHasAnimations && theMdlFileData.theFlexDescs != null && theMdlFileData.theFlexDescs.Count > 0)
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
				return false;
			}
		}

#endregion

#region Methods

		public override AppEnums.FilesFoundFlags CheckForRequiredFiles()
		{
			AppEnums.FilesFoundFlags status = AppEnums.FilesFoundFlags.AllFilesFound;

			if (!theMdlFileDataGeneric.theMdlFileOnlyHasAnimations)
			{
				thePhyPathFileName = Path.ChangeExtension(theMdlPathFileName, ".phy");

				theVtxPathFileName = Path.ChangeExtension(theMdlPathFileName, ".dx80.vtx");
				if (!File.Exists(theVtxPathFileName))
				{
					theVtxPathFileName = Path.ChangeExtension(theMdlPathFileName, ".dx7_2bone.vtx");
					if (!File.Exists(theVtxPathFileName))
					{
						theVtxPathFileName = Path.ChangeExtension(theMdlPathFileName, ".vtx");
						if (!File.Exists(theVtxPathFileName))
						{
							status = AppEnums.FilesFoundFlags.ErrorRequiredVtxFileNotFound;
						}
					}
				}
			}

			return status;
		}

		public override AppEnums.StatusMessage ReadPhyFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//If String.IsNullOrEmpty(Me.thePhyPathFileName) Then
			//	status = Me.CheckForRequiredFiles()
			//End If

			if (status == AppEnums.StatusMessage.Success)
			{
				try
				{
					ReadFile(thePhyPathFileName, ReadPhyFile_Internal);
					if (thePhyFileDataGeneric.checksum != theMdlFileData.checksum)
					{
						//status = StatusMessage.WarningPhyChecksumDoesNotMatchMdl
						NotifySourceModelProgress(AppEnums.ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum, "");
					}
				}
				catch (Exception ex)
				{
					status = AppEnums.StatusMessage.Error;
				}
			}

			return status;
		}

		public override AppEnums.StatusMessage WritePhysicsMeshSmdFile(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			string physicsMeshPathFileName = null;
			//Me.thePhysicsMeshSmdFileName = SourceFileNamesModule.CreatePhysicsSmdFileName(Me.thePhysicsMeshSmdFileName, Me.theName)
			//physicsMeshPathFileName = Path.Combine(modelOutputPath, Me.thePhysicsMeshSmdFileName)
			thePhyFileDataGeneric.thePhysicsMeshSmdFileName = SourceFileNamesModule.CreatePhysicsSmdFileName(thePhyFileDataGeneric.thePhysicsMeshSmdFileName, theName);
			physicsMeshPathFileName = Path.Combine(modelOutputPath, thePhyFileDataGeneric.thePhysicsMeshSmdFileName);
			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, physicsMeshPathFileName);
			WriteTextFile(physicsMeshPathFileName, WritePhysicsMeshSmdFile);
			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, physicsMeshPathFileName);

			return status;
		}

		public override AppEnums.StatusMessage WriteReferenceMeshFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = WriteMeshSmdFiles(modelOutputPath, 0, 0);

			return status;
		}

		public override AppEnums.StatusMessage WriteLodMeshFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = WriteMeshSmdFiles(modelOutputPath, 1, theVtxFileData.lodCount - 1);

			return status;
		}

		public AppEnums.StatusMessage WriteMeshSmdFile(string smdPathFileName, int lodIndex, SourceVtxModel107 aVtxModel, SourceMdlModel2531 aModel, int bodyPartVertexIndexStart)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				theOutputFileTextWriter = File.CreateText(smdPathFileName);
				SourceSmdFile2531 smdFile = new SourceSmdFile2531(theOutputFileTextWriter, theMdlFileData);

				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection();
				smdFile.WriteSkeletonSection();
				smdFile.WriteTrianglesSection(lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart);
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

		public override AppEnums.StatusMessage WriteBoneAnimationSmdFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlAnimationDesc2531 anAnimationDesc = null;
			string smdPath = null;
			//Dim smdFileName As String
			string smdPathFileName = null;
			string writeStatus = null;

			for (int anAnimDescIndex = 0; anAnimDescIndex < theMdlFileData.theAnimationDescs.Count; anAnimDescIndex++)
			{
				try
				{
					anAnimationDesc = theMdlFileData.theAnimationDescs[anAnimDescIndex];

					anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, Name, anAnimationDesc.theName);
					smdPathFileName = Path.Combine(modelOutputPath, anAnimationDesc.theSmdRelativePathFileName);
					smdPath = FileManager.GetPath(smdPathFileName);
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

					writeStatus = WriteBoneAnimationSmdFile(smdPathFileName, null, anAnimationDesc);

					if (writeStatus == "Success")
					{
						NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
					}
					else
					{
						NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFailed, writeStatus);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}

			return status;
		}

		//Public Overrides Function WriteVertexAnimationVtaFile(ByVal vtaPathFileName As String) As AppEnums.StatusMessage
		//	Dim status As AppEnums.StatusMessage = StatusMessage.Success

		//	Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, vtaPathFileName)
		//	Me.WriteTextFile(vtaPathFileName, AddressOf Me.WriteVertexAnimationVtaFile)
		//	Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, vtaPathFileName)

		//	Return status
		//End Function

		public override AppEnums.StatusMessage WriteVertexAnimationVtaFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;
			string vtaFileName = null;
			string vtaPathFileName = null;

			vtaFileName = SourceFileNamesModule.GetVtaFileName(Name, 0);
			vtaPathFileName = Path.Combine(modelOutputPath, vtaFileName);

			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, vtaPathFileName);
			try
			{
				theOutputFileTextWriter = File.CreateText(vtaPathFileName);

				WriteVertexAnimationVtaFile(null);
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
			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, vtaPathFileName);

			return status;
		}

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

			if (theVtxFileData != null)
			{
				debugPathFileName = Path.Combine(debugPath, theName + " " + Properties.Resources.Decompile_DebugVtxFileNameSuffix);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				WriteAccessedBytesDebugFile(debugPathFileName, theVtxFileData.theFileSeekLog);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (thePhyFileDataGeneric != null)
			{
				debugPathFileName = Path.Combine(debugPath, theName + " " + Properties.Resources.Decompile_DebugPhyFileNameSuffix);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				WriteAccessedBytesDebugFile(debugPathFileName, thePhyFileDataGeneric.theFileSeekLog);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			return status;
		}

		public override List<string> GetTextureFolders()
		{
			List<string> textureFolders = new List<string>();

			for (int i = 0; i < theMdlFileData.theTexturePaths.Count; i++)
			{
				string aTextureFolder = theMdlFileData.theTexturePaths[i];

				textureFolders.Add(aTextureFolder);
			}

			return textureFolders;
		}

		public override List<string> GetTextureFileNames()
		{
			List<string> textureFileNames = new List<string>();

			for (int i = 0; i < theMdlFileData.theTextures.Count; i++)
			{
				SourceMdlTexture2531 aTexture = theMdlFileData.theTextures[i];

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
				theMdlFileData = new SourceMdlFileData2531();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile2531 mdlFile = new SourceMdlFile2531(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader();

			//If Me.theMdlFileData.fileSize <> Me.theMdlFileData.theActualFileSize Then
			//	status = StatusMessage.ErrorInvalidInternalMdlFileSize
			//End If
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData2531();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile2531 mdlFile = new SourceMdlFile2531(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader();

			mdlFile.ReadTexturePaths();
			mdlFile.ReadTextures();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData2531();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile2531 mdlFile = new SourceMdlFile2531(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader();

			mdlFile.ReadBones();
			mdlFile.ReadBoneControllers();
			mdlFile.ReadAttachments();
			mdlFile.ReadHitboxSets();
			mdlFile.ReadSurfaceProp();

			mdlFile.ReadSequenceGroups();
			//NOTE: Must read sequences before reading animations.
			mdlFile.ReadSequences();
			//mdlFile.ReadTransitions()

			mdlFile.ReadLocalAnimationDescs();

			//NOTE: Read flex descs before body parts so that flexes (within body parts) can add info to flex descs.
			mdlFile.ReadFlexDescs();
			mdlFile.ReadBodyParts();
			mdlFile.ReadFlexControllers();
			//NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
			mdlFile.ReadFlexRules();
			mdlFile.ReadPoseParamDescs();

			mdlFile.ReadTextures();
			mdlFile.ReadTexturePaths();
			mdlFile.ReadSkins();

			mdlFile.ReadIncludeModels();

			mdlFile.ReadUnreadBytes();

			// Post-processing.
			mdlFile.CreateFlexFrameList();
		}

		protected override void ReadPhyFile_Internal()
		{
			if (thePhyFileDataGeneric == null)
			{
				thePhyFileDataGeneric = new SourcePhyFileData();
			}

			SourcePhyFile phyFile = new SourcePhyFile(theInputFileReader, thePhyFileDataGeneric);

			phyFile.ReadSourcePhyHeader();
			if (thePhyFileDataGeneric.solidCount > 0)
			{
				phyFile.ReadSourceCollisionData();
				phyFile.CalculateVertexNormals();
				phyFile.ReadSourcePhysCollisionModels();
				phyFile.ReadSourcePhyRagdollConstraintDescs();
				phyFile.ReadSourcePhyCollisionRules();
				phyFile.ReadSourcePhyEditParamsSection();
				phyFile.ReadCollisionTextSection();
			}
			phyFile.ReadUnreadBytes();
		}

		protected override void ReadVtxFile_Internal()
		{
			if (theVtxFileData == null)
			{
				theVtxFileData = new SourceVtxFileData107();
			}

			SourceVtxFile107 vtxFile = new SourceVtxFile107(theInputFileReader, theVtxFileData);

			vtxFile.ReadSourceVtxHeader();
			vtxFile.ReadSourceVtxBodyParts();
			vtxFile.ReadUnreadBytes();
		}

		protected override void WriteQcFile()
		{
			SourceQcFile2531 qcFile = new SourceQcFile2531(theOutputFileTextWriter, theQcPathFileName, theMdlFileData, theVtxFileData, thePhyFileDataGeneric, theName);

			try
			{
				qcFile.WriteHeaderComment();

				qcFile.WriteModelNameCommand();

				qcFile.WriteBodyGroupCommand();
				qcFile.WriteLodCommand();

				qcFile.WriteStaticPropCommand();
				//qcFile.WriteFlagsCommand()
				qcFile.WriteIllumPositionCommand();
				qcFile.WriteEyePositionCommand();
				qcFile.WriteSurfacePropCommand();

				qcFile.WriteCdMaterialsCommand();
				qcFile.WriteTextureGroupCommand();
				//If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				//	qcFile.WriteTextureFileNameComments()
				//End If

				qcFile.WriteAttachmentCommand();

				qcFile.WriteCBoxCommand();
				qcFile.WriteBBoxCommand();
				qcFile.WriteHBoxRelatedCommands();

				qcFile.WriteControllerCommand();

				//qcFile.WriteSequenceGroupCommands()
				qcFile.WriteSequenceCommands();
				qcFile.WriteIncludeModelCommands();

				qcFile.WriteCollisionModelOrCollisionJointsCommand();
				qcFile.WriteCollisionTextCommand();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}
		}

		protected override void WritePhysicsMeshSmdFile()
		{
			SourceSmdFile2531 physicsSmdFile = new SourceSmdFile2531(theOutputFileTextWriter, theMdlFileData, thePhyFileDataGeneric);

			try
			{
				physicsSmdFile.WriteHeaderComment();

				physicsSmdFile.WriteHeaderSection();
				physicsSmdFile.WriteNodesSection();
				physicsSmdFile.WriteSkeletonSection();
				physicsSmdFile.WriteTrianglesSectionForPhysics();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}
		}

		protected virtual AppEnums.StatusMessage WriteMeshSmdFiles(string modelOutputPath, int lodStartIndex, int lodStopIndex)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//Dim smdFileName As String
			string smdPathFileName = null;
			SourceVtxBodyPart107 aBodyPart = null;
			SourceVtxModel107 aVtxModel = null;
			SourceMdlModel2531 aBodyModel = null;
			int bodyPartVertexIndexStart = 0;

			if (theVtxFileData.theVtxBodyParts != null && theMdlFileData.theBodyParts != null)
			{
				for (int bodyPartIndex = 0; bodyPartIndex < theVtxFileData.theVtxBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = theVtxFileData.theVtxBodyParts[bodyPartIndex];

					if (aBodyPart.theVtxModels != null)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theVtxModels.Count; modelIndex++)
						{
							aVtxModel = aBodyPart.theVtxModels[modelIndex];

							if (aVtxModel.theVtxModelLods != null)
							{
								aBodyModel = theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex];
								if (aBodyModel.name[0] == '\0' && aVtxModel.theVtxModelLods[0].theVtxMeshes == null)
								{
									continue;
								}

								for (int lodIndex = lodStartIndex; lodIndex <= lodStopIndex; lodIndex++)
								{
									//TODO: Why would this count be different than the file header count?
									if (lodIndex >= aVtxModel.theVtxModelLods.Count)
									{
										break;
									}

									try
									{
										string bodyModelName;
										//bodyModelName = Me.theMdlFileData.theSequenceGroups(0).theFileName
										//If String.IsNullOrEmpty(bodyModelName) OrElse FileManager.FilePathHasInvalidChars(bodyModelName) Then
										bodyModelName = new string(theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name);
										//End If
										aBodyModel.theSmdFileNames[lodIndex] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[lodIndex], bodyPartIndex, modelIndex, lodIndex, theName, bodyModelName);
										smdPathFileName = Path.Combine(modelOutputPath, aBodyModel.theSmdFileNames[lodIndex]);

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

										WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxModel, aBodyModel, bodyPartVertexIndexStart);

										NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
									}
									catch (Exception ex)
									{
										int debug = 4242;
									}
								}

								bodyPartVertexIndexStart += aBodyModel.vertexCount;
							}
						}
					}
				}
			}

			return status;
		}

		protected override void WriteBoneAnimationSmdFile(SourceMdlSequenceDescBase aSequenceDesc, SourceMdlAnimationDescBase anAnimationDesc)
		{
			SourceSmdFile2531 smdFile = new SourceSmdFile2531(theOutputFileTextWriter, theMdlFileData);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection();
				smdFile.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		protected override void WriteVertexAnimationVtaFile(SourceMdlBodyPart bodyPart)
		{
			SourceVtaFile2531 vertexAnimationVtaFile = new SourceVtaFile2531(theOutputFileTextWriter, theMdlFileData);

			try
			{
				vertexAnimationVtaFile.WriteHeaderComment();

				vertexAnimationVtaFile.WriteHeaderSection();
				vertexAnimationVtaFile.WriteNodesSection();
				vertexAnimationVtaFile.WriteSkeletonSectionForVertexAnimation();
				vertexAnimationVtaFile.WriteVertexAnimationSection();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}
		}

		protected override void WriteMdlFileNameToMdlFile(string internalMdlFileName)
		{
			SourceMdlFile2531 mdlFile = new SourceMdlFile2531(theOutputFileBinaryWriter, theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
		}

#endregion

#region Constants

		//'#define MAX_NUM_BONES_PER_VERT 4
		//'#define MAX_NUM_BONES_PER_TRI ( MAX_NUM_BONES_PER_VERT * 3 )
		//'#define MAX_NUM_BONES_PER_STRIP 16
		//Public Shared MAX_NUM_BONES_PER_VERT As Integer = 4
		//Public Shared MAX_NUM_BONES_PER_TRI As Integer = MAX_NUM_BONES_PER_VERT * 3
		//Public Shared MAX_NUM_BONES_PER_STRIP As Integer = 16
		//------
		//FROM: VAMPTools-master\MDLConverter\inc\external\studio.h
		//#define MAX_NUM_BONES_PER_VERT 3
		public static int MAX_NUM_BONES_PER_VERT = 3;

#endregion

#region Data

		private SourceMdlFileData2531 theMdlFileData;
		//Private thePhyFileData As SourcePhyFileData2531
		private SourceVtxFileData107 theVtxFileData;

#endregion

	}

}