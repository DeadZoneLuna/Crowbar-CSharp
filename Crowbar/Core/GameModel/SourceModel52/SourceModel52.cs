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
	public class SourceModel52 : SourceModel49
	{
#region Creation and Destruction

		public SourceModel52(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{
		}

#endregion

#region Properties

		public override bool VtxFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(this.theVtxPathFileName) && File.Exists(this.theVtxPathFileName);
			}
		}

		public override bool AniFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(this.theAniPathFileName) && File.Exists(this.theAniPathFileName);
			}
		}

		public override bool VvdFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(this.theVvdPathFileName) && File.Exists(this.theVvdPathFileName);
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

			if (this.theMdlFileData.animBlockCount > 0)
			{
				this.theAniPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".ani");
				if (!File.Exists(this.theAniPathFileName))
				{
					status |= AppEnums.FilesFoundFlags.ErrorRequiredAniFileNotFound;
				}
			}

			if (!this.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations)
			{
				this.thePhyPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".phy");

				this.theVtxPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".dx11.vtx");
				if (!File.Exists(this.theVtxPathFileName))
				{
					this.theVtxPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".dx90.vtx");
					if (!File.Exists(this.theVtxPathFileName))
					{
						this.theVtxPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".dx80.vtx");
						if (!File.Exists(this.theVtxPathFileName))
						{
							this.theVtxPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".sw.vtx");
							if (!File.Exists(this.theVtxPathFileName))
							{
								this.theVtxPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".vtx");
								if (!File.Exists(this.theVtxPathFileName))
								{
									status |= AppEnums.FilesFoundFlags.ErrorRequiredVtxFileNotFound;
								}
							}
						}
					}
				}

				this.theVvdPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".vvd");
				if (!File.Exists(this.theVvdPathFileName))
				{
					status |= AppEnums.FilesFoundFlags.ErrorRequiredVvdFileNotFound;
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

			if (this.thePhyFileDataGeneric != null)
			{
				debugPathFileName = Path.Combine(debugPath, this.theName + " " + Properties.Resources.Decompile_DebugPhyFileNameSuffix);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, debugPathFileName);
				this.WriteAccessedBytesDebugFile(debugPathFileName, this.thePhyFileDataGeneric.theFileSeekLog);
				this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, debugPathFileName);
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
			if (this.theAniFileData == null)
			{
				//Me.theAniFileData = New SourceAniFileData52()
				this.theAniFileData = new SourceMdlFileData52();
				this.theAniFileDataGeneric = this.theAniFileData;
			}

			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData52();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceAniFile52 aniFile = new SourceAniFile52(this.theInputFileReader, this.theAniFileData, this.theMdlFileData);

			aniFile.ReadMdlHeader00("ANI File Header 00");
			aniFile.ReadMdlHeader01("ANI File Header 01");

			aniFile.ReadAnimationAniBlocks();
			aniFile.ReadUnreadBytes();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData52();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile52 mdlFile = new SourceMdlFile52(this.theInputFileReader, this.theMdlFileData);

			this.theMdlFileData.theSectionFrameCount = 0;
			this.theMdlFileData.theModelCommandIsUsed = false;
			this.theMdlFileData.theProceduralBonesCommandIsUsed = false;

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
			if (this.theMdlFileData.studioHeader2Offset > 0)
			{
				mdlFile.ReadMdlHeader02("MDL File Header 02");
			}

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
			mdlFile.ReadIkChains();
			mdlFile.ReadIkLocks();
			mdlFile.ReadMouths();
			mdlFile.ReadPoseParamDescs();
			mdlFile.ReadModelGroups();
			//TODO: Me.ReadAnimBlocks()
			//TODO: Me.ReadAnimBlockName()

			mdlFile.ReadTexturePaths();
			//NOTE: ReadTextures must be after ReadTexturePaths(), so it can compare with the texture paths.
			mdlFile.ReadTextures();
			mdlFile.ReadSkinFamilies();

			mdlFile.ReadKeyValues();

			mdlFile.ReadBoneTransforms();
			mdlFile.ReadLinearBoneTable();

			//TODO: ReadLocalIkAutoPlayLocks()
			mdlFile.ReadFlexControllerUis();

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

			SourcePhyFile phyFile = new SourcePhyFile(this.theInputFileReader, this.thePhyFileDataGeneric);

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

			//TEST: When a model has a nameCopy, it seems to also use the VTF file strip group topology fields.
			SourceVtxFile07 vtxFile = new SourceVtxFile07(this.theInputFileReader, this.theVtxFileData);

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

			SourceVvdFile04 vvdFile = new SourceVvdFile04(this.theInputFileReader, this.theVvdFileData49);

			vvdFile.ReadSourceVvdHeader();
			vvdFile.ReadVertexes();
			vvdFile.ReadFixups();
			vvdFile.ReadUnreadBytes();
		}

		protected override void WriteQcFile()
		{
			//Dim qcFile As New SourceQcFile52(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theVtxFileData, Me.thePhyFileDataGeneric, Me.theAniFileData, Me.theName)
			SourceQcFile52 qcFile = new SourceQcFile52(this.theOutputFileTextWriter, this.theQcPathFileName, this.theMdlFileData, this.theVtxFileData, this.thePhyFileDataGeneric, this.theName);

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
				//qcFile.WriteModelCommand()
				qcFile.WriteBodyGroupCommand();
				qcFile.WriteGroup("lod", qcFile.WriteGroupLod, false, false);

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
				qcFile.WriteCastTextureShadowsCommand();
				qcFile.WriteDoNotCastShadowsCommand();
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
				this.theMdlFileData = new SourceMdlFileData52();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile52 mdlFile = new SourceMdlFile52(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
			if (this.theMdlFileData.studioHeader2Offset > 0)
			{
				mdlFile.ReadMdlHeader02("MDL File Header 02");
			}

			//If Me.theMdlFileData.fileSize <> Me.theMdlFileData.theActualFileSize Then
			//	status = StatusMessage.ErrorInvalidInternalMdlFileSize
			//End If
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData52();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile52 mdlFile = new SourceMdlFile52(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
			if (this.theMdlFileData.studioHeader2Offset > 0)
			{
				mdlFile.ReadMdlHeader02("MDL File Header 02");
			}

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
			SourceSmdFile52 smdFile = new SourceSmdFile52(this.theOutputFileTextWriter, this.theMdlFileData, this.theVvdFileData49);

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
			SourceSmdFile52 physicsMeshSmdFile = new SourceSmdFile52(this.theOutputFileTextWriter, this.theMdlFileData, this.thePhyFileDataGeneric);

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
			SourceVrdFile52 vrdFile = new SourceVrdFile52(this.theOutputFileTextWriter, this.theMdlFileData);

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
			SourceQcFile52 qciFile = new SourceQcFile52(this.theOutputFileTextWriter, this.theMdlFileData, this.theName);

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
			SourceSmdFile52 smdFile = new SourceSmdFile52(this.theOutputFileTextWriter, this.theMdlFileData);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection(-2);
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
			SourceVtaFile52 vertexAnimationVtaFile = new SourceVtaFile52(this.theOutputFileTextWriter, this.theMdlFileData, this.theVvdFileData49);

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
			SourceMdlFile52 mdlFile = new SourceMdlFile52(this.theOutputFileBinaryWriter, this.theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
			mdlFile.WriteInternalMdlFileNameCopy(internalMdlFileName);
		}

		protected override void WriteAniFileNameToMdlFile(string internalAniFileName)
		{
			SourceMdlFile52 mdlFile = new SourceMdlFile52(this.theOutputFileBinaryWriter, this.theMdlFileData);

			mdlFile.WriteInternalAniFileName(internalAniFileName);
		}

#endregion

#region Data

		//Private theAniFileData As SourceAniFileData52
		private SourceMdlFileData52 theAniFileData;
		private SourceMdlFileData52 theMdlFileData;
		//Private thePhyFileData49 As SourcePhyFileData
		private SourceVtxFileData07 theVtxFileData;
		private SourceVvdFileData04 theVvdFileData49;

#endregion

	}

}