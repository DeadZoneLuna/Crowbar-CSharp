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
				return !this.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations && this.theMdlFileData.theTextures != null && this.theMdlFileData.theTextures.Count > 0;
			}
		}

		public override bool HasMeshData
		{
			get
			{
				if (!this.theMdlFileData.theMdlFileOnlyHasAnimations && this.theMdlFileData.theBones != null && this.theMdlFileData.theBones.Count > 0 && this.theVtxFileData != null)
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
				if (!this.theMdlFileData.theMdlFileOnlyHasAnimations && this.theMdlFileData.theBones != null && this.theMdlFileData.theBones.Count > 0 && this.theVtxFileData != null && this.theVtxFileData.lodCount > 0)
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
				if (this.thePhyFileDataGeneric != null && this.thePhyFileDataGeneric.theSourcePhyCollisionDatas != null && !this.theMdlFileData.theMdlFileOnlyHasAnimations && this.theMdlFileData.theBones != null && this.theMdlFileData.theBones.Count > 0)
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
				if (this.theMdlFileData != null && this.theMdlFileData.theProceduralBonesCommandIsUsed && !this.theMdlFileData.theMdlFileOnlyHasAnimations && this.theMdlFileData.theBones != null && this.theMdlFileData.theBones.Count > 0)
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
				if (this.theMdlFileData.theAnimationDescs != null && this.theMdlFileData.theAnimationDescs.Count > 0)
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
				if (!this.theMdlFileData.theMdlFileOnlyHasAnimations && this.theMdlFileData.theFlexDescs != null && this.theMdlFileData.theFlexDescs.Count > 0)
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
					this.ReadFile(this.thePhyPathFileName, this.ReadPhyFile_Internal);
					if (this.thePhyFileDataGeneric.checksum != this.theMdlFileData.checksum)
					{
						//status = StatusMessage.WarningPhyChecksumDoesNotMatchMdl
						this.NotifySourceModelProgress(AppEnums.ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum, "");
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

			status = this.WriteMeshSmdFiles(modelOutputPath, 1, this.theVtxFileData.lodCount - 1);

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
				for (int anAnimDescIndex = 0; anAnimDescIndex < this.theMdlFileData.theAnimationDescs.Count; anAnimDescIndex++)
				{
					anAnimationDesc = this.theMdlFileData.theAnimationDescs[anAnimDescIndex];

					anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, this.Name, anAnimationDesc.theName);
					smdPathFileName = Path.Combine(modelOutputPath, anAnimationDesc.theSmdRelativePathFileName);
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

						writeStatus = this.WriteBoneAnimationSmdFile(smdPathFileName, null, anAnimationDesc);

						if (writeStatus == "Success")
						{
							this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
						}
						else
						{
							this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFailed, writeStatus);
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
				for (int aBodyPartIndex = 0; aBodyPartIndex < this.theMdlFileData.theBodyParts.Count; aBodyPartIndex++)
				{
					aBodyPart = this.theMdlFileData.theBodyParts[aBodyPartIndex];

					if (aBodyPart.theFlexFrames == null || aBodyPart.theFlexFrames.Count == 0)
					{
						continue;
					}

					vtaFileName = SourceFileNamesModule.GetVtaFileName(this.Name, aBodyPartIndex);
					vtaPathFileName = Path.Combine(modelOutputPath, vtaFileName);
					vtaPath = FileManager.GetPath(vtaPathFileName);
					if (FileManager.PathExistsAfterTryToCreate(vtaPath))
					{
						this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, vtaPathFileName);
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

						this.WriteVertexAnimationVtaFile(vtaPathFileName, aBodyPart);

						this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, vtaPathFileName);
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

			for (int i = 0; i < this.theMdlFileData.theTexturePaths.Count; i++)
			{
				string aTextureFolder = this.theMdlFileData.theTexturePaths[i];

				textureFolders.Add(aTextureFolder);
			}

			return textureFolders;
		}

		public override List<string> GetTextureFileNames()
		{
			List<string> textureFileNames = new List<string>();

			for (int i = 0; i < this.theMdlFileData.theTextures.Count; i++)
			{
				SourceMdlTexture aTexture = this.theMdlFileData.theTextures[i];

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
			if (this.theAniFileData49 == null)
			{
				//Me.theAniFileData49 = New SourceAniFileData49()
				this.theAniFileData49 = new SourceMdlFileData49();
				this.theAniFileDataGeneric = this.theAniFileData49;
			}

			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData53();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceAniFile49 aniFile = new SourceAniFile49(this.theInputFileReader, this.theAniFileData49, this.theMdlFileData);

			aniFile.ReadMdlHeader00("ANI File Header 00");
			aniFile.ReadMdlHeader01("ANI File Header 01");

			aniFile.ReadAnimationAniBlocks();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData53();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile53 mdlFile = new SourceMdlFile53(this.theInputFileReader, this.theMdlFileData);

			this.theMdlFileData.theSectionFrameCount = 0;
			this.theMdlFileData.theModelCommandIsUsed = false;
			this.theMdlFileData.theProceduralBonesCommandIsUsed = false;

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

			if (this.theMdlFileData.localAnimationCount > 0)
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
			if (this.theMdlFileData.phyOffset > 0)
			{
				mdlFile.SetReaderToPhyOffset();
				this.ReadPhyFile_Internal();
			}
			if (this.theMdlFileData.vtxOffset > 0)
			{
				mdlFile.SetReaderToVtxOffset();
				this.ReadVtxFile_Internal();
			}
			if (this.theMdlFileData.vvdOffset > 0)
			{
				mdlFile.SetReaderToVvdOffset();
				this.ReadVvdFile_Internal();
			}

			mdlFile.ReadUnreadBytes();

			// Post-processing.
			mdlFile.CreateFlexFrameList();
			Common.ProcessTexturePaths(this.theMdlFileData.theTexturePaths, this.theMdlFileData.theTextures, this.theMdlFileData.theModifiedTexturePaths, this.theMdlFileData.theModifiedTextureFileNames);
		}

		protected override void ReadPhyFile_Internal()
		{
			if (this.thePhyFileDataGeneric == null)
			{
				this.thePhyFileDataGeneric = new SourcePhyFileData();
			}

			SourcePhyFile phyFile = new SourcePhyFile(this.theInputFileReader, this.thePhyFileDataGeneric, this.theMdlFileData.vtxOffset);

			phyFile.ReadSourcePhyHeader();
			if (this.thePhyFileDataGeneric.solidCount > 0)
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
			if (this.theVtxFileData == null)
			{
				this.theVtxFileData = new SourceVtxFileData07();
			}

			SourceVtxFile07 vtxFile = new SourceVtxFile07(this.theInputFileReader, this.theVtxFileData, this.theInputFileReader.BaseStream.Position);

			vtxFile.ReadSourceVtxHeader();
			if (this.theVtxFileData.lodCount > 0)
			{
				vtxFile.ReadSourceVtxBodyParts();
			}
			vtxFile.ReadSourceVtxMaterialReplacementLists();
			vtxFile.ReadUnreadBytes();
		}

		protected override void ReadVvdFile_Internal()
		{
			if (this.theVvdFileData49 == null)
			{
				this.theVvdFileData49 = new SourceVvdFileData04();
			}

			SourceVvdFile04 vvdFile = new SourceVvdFile04(this.theInputFileReader, this.theVvdFileData49, this.theInputFileReader.BaseStream.Position);

			vvdFile.ReadSourceVvdHeader();
			vvdFile.ReadVertexes();
			vvdFile.ReadFixups();
			vvdFile.ReadUnreadBytes();
		}

		protected override void WriteQcFile()
		{
			//Dim qcFile As New SourceQcFile53(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theVtxFileData, Me.thePhyFileDataGeneric, Me.theAniFileData49, Me.theName)
			SourceQcFile53 qcFile = new SourceQcFile53(this.theOutputFileTextWriter, this.theQcPathFileName, this.theMdlFileData, this.theVtxFileData, this.thePhyFileDataGeneric, this.theName);

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

				qcFile.WriteKeyValues(this.theMdlFileData.theKeyValuesText, "$KeyValues");
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
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData53();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile53 mdlFile = new SourceMdlFile53(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");

			//If Me.theMdlFileData.fileSize <> Me.theMdlFileData.theActualFileSize Then
			//	status = StatusMessage.ErrorInvalidInternalMdlFileSize
			//End If
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData53();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile53 mdlFile = new SourceMdlFile53(this.theInputFileReader, this.theMdlFileData);

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

			if (this.theVtxFileData.theVtxBodyParts != null && this.theMdlFileData.theBodyParts != null)
			{
				for (int bodyPartIndex = 0; bodyPartIndex < this.theVtxFileData.theVtxBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = this.theVtxFileData.theVtxBodyParts[bodyPartIndex];

					if (aBodyPart.theVtxModels != null)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theVtxModels.Count; modelIndex++)
						{
							aVtxModel = aBodyPart.theVtxModels[modelIndex];

							if (aVtxModel.theVtxModelLods != null)
							{
								aBodyModel = this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex];
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

									smdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[lodIndex], bodyPartIndex, modelIndex, lodIndex, this.theName, new string(this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name));
									smdPathFileName = Path.Combine(modelOutputPath, smdFileName);

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

									this.WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxModel, aBodyModel, bodyPartVertexIndexStart);

									this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
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
			SourceSmdFile53 smdFile = new SourceSmdFile53(this.theOutputFileTextWriter, this.theMdlFileData, this.theVvdFileData49);

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
			SourceSmdFile53 physicsMeshSmdFile = new SourceSmdFile53(this.theOutputFileTextWriter, this.theMdlFileData, this.thePhyFileDataGeneric);

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
			SourceVrdFile53 vrdFile = new SourceVrdFile53(this.theOutputFileTextWriter, this.theMdlFileData);

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
			SourceQcFile53 qciFile = new SourceQcFile53(this.theOutputFileTextWriter, this.theMdlFileData, this.theName);

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
			SourceSmdFile53 smdFile = new SourceSmdFile53(this.theOutputFileTextWriter, this.theMdlFileData);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection(-1);
				if (this.theMdlFileData.theFirstAnimationDesc != null && this.theMdlFileData.theFirstAnimationDescFrameLines.Count == 0)
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
			SourceVtaFile53 vertexAnimationVtaFile = new SourceVtaFile53(this.theOutputFileTextWriter, this.theMdlFileData, this.theVvdFileData49);

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

			if (this.theMdlFileDataGeneric != null)
			{
				debugPathFileName = Path.Combine(debugPath, this.theName + " " + Properties.Resources.Decompile_DebugMdlFileNameSuffix);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				this.WriteAccessedBytesDebugFile(debugPathFileName, this.theMdlFileDataGeneric.theFileSeekLog);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (this.theAniFileDataGeneric != null)
			{
				debugPathFileName = Path.Combine(debugPath, this.theName + " " + Properties.Resources.Decompile_DebugAniFileNameSuffix);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				this.WriteAccessedBytesDebugFile(debugPathFileName, this.theAniFileDataGeneric.theFileSeekLog);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (this.theVtxFileData != null)
			{
				debugPathFileName = Path.Combine(debugPath, this.theName + " " + Properties.Resources.Decompile_DebugVtxFileNameSuffix);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				this.WriteAccessedBytesDebugFile(debugPathFileName, this.theVtxFileData.theFileSeekLog);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (this.theVvdFileData49 != null)
			{
				debugPathFileName = Path.Combine(debugPath, this.theName + " " + Properties.Resources.Decompile_DebugVvdFileNameSuffix);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				this.WriteAccessedBytesDebugFile(debugPathFileName, this.theVvdFileData49.theFileSeekLog);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			if (this.thePhyFileDataGeneric != null)
			{
				debugPathFileName = Path.Combine(debugPath, this.theName + " " + Properties.Resources.Decompile_DebugPhyFileNameSuffix);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				this.WriteAccessedBytesDebugFile(debugPathFileName, this.thePhyFileDataGeneric.theFileSeekLog);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
			}

			return status;
		}

		protected override void WriteMdlFileNameToMdlFile(string internalMdlFileName)
		{
			SourceMdlFile53 mdlFile = new SourceMdlFile53(this.theOutputFileBinaryWriter, this.theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
		}

		protected override void WriteAniFileNameToMdlFile(string internalAniFileName)
		{
			SourceMdlFile53 mdlFile = new SourceMdlFile53(this.theOutputFileBinaryWriter, this.theMdlFileData);

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