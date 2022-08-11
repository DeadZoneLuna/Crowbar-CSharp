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
	public class SourceModel53 : SourceModel49
	{
#region Creation and Destruction

		public SourceModel53(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{
		}

#endregion

#region Properties

		public override bool VtxFileIsUsed
		{
			get
			{
				return false;
			}
		}

		public override bool AniFileIsUsed
		{
			get
			{
				return false;
			}
		}

		public override bool VvdFileIsUsed
		{
			get
			{
				return false;
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
				if (!theMdlFileData.theMdlFileOnlyHasAnimations && theMdlFileData.theBones != null && theMdlFileData.theBones.Count > 0 && theVtxFileData != null)
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

		public override bool HasProceduralBonesData
		{
			get
			{
				if (theMdlFileData != null && theMdlFileData.theProceduralBonesCommandIsUsed && !theMdlFileData.theMdlFileOnlyHasAnimations && theMdlFileData.theBones != null && theMdlFileData.theBones.Count > 0)
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
				if (theMdlFileData.theAnimationDescs != null && theMdlFileData.theAnimationDescs.Count > 0)
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

#endregion

#region Methods

		public override AppEnums.FilesFoundFlags CheckForRequiredFiles()
		{
			AppEnums.FilesFoundFlags status = AppEnums.FilesFoundFlags.AllFilesFound;

			//If Me.theMdlFileData.animBlockCount > 0 Then
			//	Me.theAniPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".ani")
			//	If Not File.Exists(Me.theAniPathFileName) Then
			//		status = status Or FilesFoundFlags.ErrorRequiredAniFileNotFound
			//	End If
			//End If

			//If Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations Then
			//	Me.thePhyPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".phy")

			//	Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx11.vtx")
			//	If Not File.Exists(Me.theVtxPathFileName) Then
			//		Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx90.vtx")
			//		If Not File.Exists(Me.theVtxPathFileName) Then
			//			Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx80.vtx")
			//			If Not File.Exists(Me.theVtxPathFileName) Then
			//				Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".sw.vtx")
			//				If Not File.Exists(Me.theVtxPathFileName) Then
			//					Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".vtx")
			//					If Not File.Exists(Me.theVtxPathFileName) Then
			//						status = status Or FilesFoundFlags.ErrorRequiredVtxFileNotFound
			//					End If
			//				End If
			//			End If
			//		End If
			//	End If

			//	Me.theVvdPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".vvd")
			//	If Not File.Exists(Me.theVvdPathFileName) Then
			//		status = status Or FilesFoundFlags.ErrorRequiredVvdFileNotFound
			//	End If
			//End If

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

		public override AppEnums.StatusMessage WriteLodMeshFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = WriteMeshSmdFiles(modelOutputPath, 1, theVtxFileData.lodCount - 1);

			return status;
		}

		public override AppEnums.StatusMessage WriteBoneAnimationSmdFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlAnimationDesc52 anAnimationDesc = null;
			string smdPath = null;
			//Dim smdFileName As String
			string smdPathFileName = null;
			string writeStatus = null;

			try
			{
				for (int anAnimDescIndex = 0; anAnimDescIndex < theMdlFileData.theAnimationDescs.Count; anAnimDescIndex++)
				{
					anAnimationDesc = theMdlFileData.theAnimationDescs[anAnimDescIndex];

					anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, Name, anAnimationDesc.theName);
					smdPathFileName = Path.Combine(modelOutputPath, anAnimationDesc.theSmdRelativePathFileName);
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
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			return status;
		}

		public override AppEnums.StatusMessage WriteVertexAnimationVtaFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlBodyPart aBodyPart = null;
			string vtaFileName = null;
			string vtaPath = null;
			string vtaPathFileName = null;

			try
			{
				for (int aBodyPartIndex = 0; aBodyPartIndex < theMdlFileData.theBodyParts.Count; aBodyPartIndex++)
				{
					aBodyPart = theMdlFileData.theBodyParts[aBodyPartIndex];

					if (aBodyPart.theFlexFrames == null || aBodyPart.theFlexFrames.Count == 0)
					{
						continue;
					}

					vtaFileName = SourceFileNamesModule.GetVtaFileName(Name, aBodyPartIndex);
					vtaPathFileName = Path.Combine(modelOutputPath, vtaFileName);
					vtaPath = FileManager.GetPath(vtaPathFileName);
					if (FileManager.PathExistsAfterTryToCreate(vtaPath))
					{
						NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, vtaPathFileName);
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

						WriteVertexAnimationVtaFile(vtaPathFileName, aBodyPart);

						NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, vtaPathFileName);
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
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
				SourceMdlTexture aTexture = theMdlFileData.theTextures[i];

				textureFileNames.Add(aTexture.thePathFileName);
			}

			return textureFileNames;
		}

		//Public Overrides Function GetSequenceInfo() As List(Of String)
		//	Dim sequenceFileNames As New List(Of String)()

		//	For i As Integer = 0 To Me.theMdlFileData.theSequenceDescs.Count - 1
		//		Dim aSequence As SourceMdlSequenceDesc
		//		aSequence = Me.theMdlFileData.theSequenceDescs(i)

		//		sequenceFileNames.Add(aSequence.theName)
		//	Next

		//	Return sequenceFileNames
		//End Function

#endregion

#region Private Methods

		protected override void ReadAniFile_Internal()
		{
			if (theAniFileData49 == null)
			{
				//Me.theAniFileData49 = New SourceAniFileData49()
				theAniFileData49 = new SourceMdlFileData49();
				theAniFileDataGeneric = theAniFileData49;
			}

			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData53();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceAniFile49 aniFile = new SourceAniFile49(theInputFileReader, theAniFileData49, theMdlFileData);

			aniFile.ReadMdlHeader00("ANI File Header 00");
			aniFile.ReadMdlHeader01("ANI File Header 01");

			aniFile.ReadAnimationAniBlocks();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData53();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile53 mdlFile = new SourceMdlFile53(theInputFileReader, theMdlFileData);

			theMdlFileData.theSectionFrameCount = 0;
			theMdlFileData.theModelCommandIsUsed = false;
			theMdlFileData.theProceduralBonesCommandIsUsed = false;

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
			//If Me.theMdlFileData.studioHeader2Offset > 0 Then
			//	mdlFile.ReadMdlHeader02("MDL File Header 02")
			//End If

			mdlFile.ReadBones();
			mdlFile.ReadBoneControllers();
			mdlFile.ReadAttachments();

			mdlFile.ReadHitboxSets();

			//mdlFile.ReadBoneTableByName()

			if (theMdlFileData.localAnimationCount > 0)
			{
				try
				{
					mdlFile.ReadLocalAnimationDescs();
					mdlFile.ReadAnimationSections();
					mdlFile.ReadAnimationMdlBlocks();
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}

			mdlFile.ReadSequenceDescs();
			mdlFile.ReadLocalNodeNames();
			mdlFile.ReadLocalNodes();

			//NOTE: Read flex descs before body parts so that flexes (within body parts) can add info to flex descs.
			mdlFile.ReadFlexDescs();
			mdlFile.ReadBodyParts();
			mdlFile.ReadFlexControllers();
			//NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
			mdlFile.ReadFlexRules();
			//mdlFile.ReadIkChains()
			//mdlFile.ReadIkLocks()
			//mdlFile.ReadMouths()
			//mdlFile.ReadPoseParamDescs()
			mdlFile.ReadModelGroups();
			//'TODO: Me.ReadAnimBlocks()
			//'TODO: Me.ReadAnimBlockName()

			mdlFile.ReadTexturePaths();
			//NOTE: ReadTextures must be after ReadTexturePaths(), so it can compare with the texture paths.
			mdlFile.ReadTextures();
			mdlFile.ReadSkinFamilies();

			mdlFile.ReadKeyValues();

			//mdlFile.ReadBoneTransforms()
			//mdlFile.ReadLinearBoneTable()

			//'TODO: ReadLocalIkAutoPlayLocks()
			//mdlFile.ReadFlexControllerUis()

			// Read VTX info, VVD info, and PHY info.
			if (theMdlFileData.phyOffset > 0)
			{
				mdlFile.SetReaderToPhyOffset();
				ReadPhyFile_Internal();
			}
			if (theMdlFileData.vtxOffset > 0)
			{
				mdlFile.SetReaderToVtxOffset();
				ReadVtxFile_Internal();
			}
			if (theMdlFileData.vvdOffset > 0)
			{
				mdlFile.SetReaderToVvdOffset();
				ReadVvdFile_Internal();
			}

			mdlFile.ReadUnreadBytes();

			// Post-processing.
			mdlFile.CreateFlexFrameList();
			Common.ProcessTexturePaths(theMdlFileData.theTexturePaths, theMdlFileData.theTextures, theMdlFileData.theModifiedTexturePaths, theMdlFileData.theModifiedTextureFileNames);
		}

		protected override void ReadPhyFile_Internal()
		{
			if (thePhyFileDataGeneric == null)
			{
				thePhyFileDataGeneric = new SourcePhyFileData();
			}

			SourcePhyFile phyFile = new SourcePhyFile(theInputFileReader, thePhyFileDataGeneric, theMdlFileData.vtxOffset);

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
				theVtxFileData = new SourceVtxFileData07();
			}

			SourceVtxFile07 vtxFile = new SourceVtxFile07(theInputFileReader, theVtxFileData, theInputFileReader.BaseStream.Position);

			vtxFile.ReadSourceVtxHeader();
			if (theVtxFileData.lodCount > 0)
			{
				vtxFile.ReadSourceVtxBodyParts();
			}
			vtxFile.ReadSourceVtxMaterialReplacementLists();
			vtxFile.ReadUnreadBytes();
		}

		protected override void ReadVvdFile_Internal()
		{
			if (theVvdFileData49 == null)
			{
				theVvdFileData49 = new SourceVvdFileData04();
			}

			SourceVvdFile04 vvdFile = new SourceVvdFile04(theInputFileReader, theVvdFileData49, theInputFileReader.BaseStream.Position);

			vvdFile.ReadSourceVvdHeader();
			vvdFile.ReadVertexes();
			vvdFile.ReadFixups();
			vvdFile.ReadUnreadBytes();
		}

		protected override void WriteQcFile()
		{
			//Dim qcFile As New SourceQcFile53(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theVtxFileData, Me.thePhyFileDataGeneric, Me.theAniFileData49, Me.theName)
			SourceQcFile53 qcFile = new SourceQcFile53(theOutputFileTextWriter, theQcPathFileName, theMdlFileData, theVtxFileData, thePhyFileDataGeneric, theName);

			try
			{
				qcFile.WriteHeaderComment();

				qcFile.WriteModelNameCommand();

				qcFile.WriteStaticPropCommand();
				qcFile.WriteConstDirectionalLightCommand();

				//If Me.theMdlFileData.theModelCommandIsUsed Then
				//	qcFile.WriteModelCommand()
				//	qcFile.WriteBodyGroupCommand(1)
				//Else
				//	qcFile.WriteBodyGroupCommand(0)
				//End If
				qcFile.WriteBodyGroupCommand();
				//TODO: LOD option "replacebone" is wrong because bone.flags is read-in incorrectly.
				//qcFile.WriteGroup("lod", AddressOf qcFile.WriteGroupLod, False, False)

				qcFile.WriteSurfacePropCommand();
				qcFile.WriteJointSurfacePropCommand();
				qcFile.WriteContentsCommand();
				qcFile.WriteJointContentsCommand();
				qcFile.WriteIllumPositionCommand();

				qcFile.WriteEyePositionCommand();
				qcFile.WriteMaxEyeDeflectionCommand();
				qcFile.WriteNoForcedFadeCommand();
				qcFile.WriteForcePhonemeCrossfadeCommand();

				qcFile.WriteAmbientBoostCommand();
				qcFile.WriteOpaqueCommand();
				qcFile.WriteObsoleteCommand();
				//TODO: Probably should be just like MDL v52.
				//qcFile.WriteCastTextureShadowsCommand()
				//qcFile.WriteDoNotCastShadowsCommand()
				qcFile.WriteCdMaterialsCommand();
				qcFile.WriteTextureGroupCommand();
				if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
				{
					qcFile.WriteTextureFileNameComments();
				}

				qcFile.WriteAttachmentCommand();

				qcFile.WriteGroup("box", qcFile.WriteGroupBox, true, false);

				qcFile.WriteControllerCommand();
				qcFile.WriteScreenAlignCommand();

				qcFile.WriteGroup("bone", qcFile.WriteGroupBone, false, false);

				qcFile.WriteGroup("animation", qcFile.WriteGroupAnimation, false, false);

				qcFile.WriteGroup("collision", qcFile.WriteGroupCollision, false, false);

				qcFile.WriteKeyValues(theMdlFileData.theKeyValuesText, "$KeyValues");
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}
		}

		protected override void ReadMdlFileHeader_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData53();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile53 mdlFile = new SourceMdlFile53(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");

			//If Me.theMdlFileData.fileSize <> Me.theMdlFileData.theActualFileSize Then
			//	status = StatusMessage.ErrorInvalidInternalMdlFileSize
			//End If
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData53();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile53 mdlFile = new SourceMdlFile53(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");

			mdlFile.ReadTexturePaths();
			mdlFile.ReadTextures();
			mdlFile.ReadSequenceDescs();
		}

		protected override AppEnums.StatusMessage WriteMeshSmdFiles(string modelOutputPath, int lodStartIndex, int lodStopIndex)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			string smdFileName = null;
			string smdPathFileName = null;
			SourceVtxBodyPart07 aBodyPart = null;
			SourceVtxModel07 aVtxModel = null;
			SourceMdlModel aBodyModel = null;
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

									smdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[lodIndex], bodyPartIndex, modelIndex, lodIndex, theName, new string(theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name));
									smdPathFileName = Path.Combine(modelOutputPath, smdFileName);

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

								bodyPartVertexIndexStart += aBodyModel.vertexCount;
							}
						}
					}
				}
			}

			return status;
		}

		protected override void WriteMeshSmdFile(int lodIndex, SourceVtxModel07 aVtxModel, SourceMdlModel aModel, int bodyPartVertexIndexStart)
		{
			SourceSmdFile53 smdFile = new SourceSmdFile53(theOutputFileTextWriter, theMdlFileData, theVvdFileData49);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection(lodIndex);
				smdFile.WriteSkeletonSection(lodIndex);
				smdFile.WriteTrianglesSection(aVtxModel, lodIndex, aModel, bodyPartVertexIndexStart);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		protected override void WritePhysicsMeshSmdFile()
		{
			SourceSmdFile53 physicsMeshSmdFile = new SourceSmdFile53(theOutputFileTextWriter, theMdlFileData, thePhyFileDataGeneric);

			try
			{
				physicsMeshSmdFile.WriteHeaderComment();

				physicsMeshSmdFile.WriteHeaderSection();
				physicsMeshSmdFile.WriteNodesSection(-1);
				physicsMeshSmdFile.WriteSkeletonSection(-1);
				physicsMeshSmdFile.WriteTrianglesSectionForPhysics();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}
		}

		protected override void WriteVrdFile()
		{
			SourceVrdFile53 vrdFile = new SourceVrdFile53(theOutputFileTextWriter, theMdlFileData);

			try
			{
				vrdFile.WriteHeaderComment();
				vrdFile.WriteCommands();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}
		}

		protected override void WriteDeclareSequenceQciFile()
		{
			SourceQcFile53 qciFile = new SourceQcFile53(theOutputFileTextWriter, theMdlFileData, theName);

			try
			{
				qciFile.WriteHeaderComment();

				qciFile.WriteQciDeclareSequenceLines();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		protected override void WriteBoneAnimationSmdFile(SourceMdlSequenceDescBase aSequenceDesc, SourceMdlAnimationDescBase anAnimationDesc)
		{
			SourceSmdFile53 smdFile = new SourceSmdFile53(theOutputFileTextWriter, theMdlFileData);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection(-1);
				if (theMdlFileData.theFirstAnimationDesc != null && theMdlFileData.theFirstAnimationDescFrameLines.Count == 0)
				{
					smdFile.CalculateFirstAnimDescFrameLinesForSubtract();
				}
				//If anAnimationDesc.animBlock > 0 AndAlso Me.theSourceEngineModel.MdlFileHeader.version >= 49 AndAlso Me.theSourceEngineModel.MdlFileHeader.version <> 2531 Then
				//	smdFile.WriteSkeletonSectionForAnimationAni_VERSION49(aSequenceDesc, anAnimationDesc)
				//Else
				//End If
				smdFile.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		protected override void WriteVertexAnimationVtaFile(SourceMdlBodyPart bodyPart)
		{
			SourceVtaFile53 vertexAnimationVtaFile = new SourceVtaFile53(theOutputFileTextWriter, theMdlFileData, theVvdFileData49);

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

		public override AppEnums.StatusMessage WriteAccessedBytesDebugFiles(string debugPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			string debugPathFileName = null;

			if (theMdlFileDataGeneric != null)
			{
				debugPathFileName = Path.Combine(debugPath, theName + " " + Properties.Resources.Decompile_DebugMdlFileNameSuffix);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				WriteAccessedBytesDebugFile(debugPathFileName, theMdlFileDataGeneric.theFileSeekLog);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (theAniFileDataGeneric != null)
			{
				debugPathFileName = Path.Combine(debugPath, theName + " " + Properties.Resources.Decompile_DebugAniFileNameSuffix);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				WriteAccessedBytesDebugFile(debugPathFileName, theAniFileDataGeneric.theFileSeekLog);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (theVtxFileData != null)
			{
				debugPathFileName = Path.Combine(debugPath, theName + " " + Properties.Resources.Decompile_DebugVtxFileNameSuffix);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				WriteAccessedBytesDebugFile(debugPathFileName, theVtxFileData.theFileSeekLog);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (theVvdFileData49 != null)
			{
				debugPathFileName = Path.Combine(debugPath, theName + " " + Properties.Resources.Decompile_DebugVvdFileNameSuffix);
				NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				WriteAccessedBytesDebugFile(debugPathFileName, theVvdFileData49.theFileSeekLog);
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

		protected override void WriteMdlFileNameToMdlFile(string internalMdlFileName)
		{
			SourceMdlFile53 mdlFile = new SourceMdlFile53(theOutputFileBinaryWriter, theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
		}

		protected override void WriteAniFileNameToMdlFile(string internalAniFileName)
		{
			SourceMdlFile53 mdlFile = new SourceMdlFile53(theOutputFileBinaryWriter, theMdlFileData);

			mdlFile.WriteInternalAniFileName(internalAniFileName);
		}

#endregion

#region Data

		//Private theAniFileData49 As SourceAniFileData49
		private SourceMdlFileData49 theAniFileData49;
		private SourceMdlFileData53 theMdlFileData;
		//Private thePhyFileData49 As SourcePhyFileData49
		private SourceVtxFileData07 theVtxFileData;
		private SourceVvdFileData04 theVvdFileData49;

#endregion

	}

}