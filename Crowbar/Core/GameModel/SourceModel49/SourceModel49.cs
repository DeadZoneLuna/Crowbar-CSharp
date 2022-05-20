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
	public class SourceModel49 : SourceModel37
	{
#region Creation and Destruction

		public SourceModel49(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{
		}

#endregion

#region Properties

		public override bool PhyFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(this.thePhyPathFileName) && File.Exists(this.thePhyPathFileName);
			}
		}

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

			//If Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations Then
			this.thePhyPathFileName = Path.ChangeExtension(this.theMdlPathFileName, ".phy");

			//TODO: If the checksum of the vtx does not match checksum in MDL, check the next vtx.
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
			//End If

			return status;
		}

		public override AppEnums.StatusMessage ReadPhyFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//If String.IsNullOrEmpty(Me.thePhyPathFileName) Then
			//	status = Me.CheckForRequiredFiles()
			//End If

			if (!string.IsNullOrEmpty(this.thePhyPathFileName))
			{
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
			}

			return status;
		}

		public override AppEnums.StatusMessage ReadAniFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//If String.IsNullOrEmpty(Me.theAniPathFileName) Then
			//	status = Me.CheckForRequiredFiles()
			//End If

			if (!string.IsNullOrEmpty(this.theAniPathFileName))
			{
				if (status == AppEnums.StatusMessage.Success)
				{
					try
					{
						this.ReadFile(this.theAniPathFileName, this.ReadAniFile_Internal);
					}
					catch (Exception ex)
					{
						status = AppEnums.StatusMessage.Error;
					}
				}
			}

			return status;
		}

		public override AppEnums.StatusMessage WriteReferenceMeshFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			status = this.WriteMeshSmdFiles(modelOutputPath, 0, 0);

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

			string smdPathFileName = null;
			string smdPath = null;
			string writeStatus = null;

			try
			{
				if (this.theMdlFileData.theCorrectiveAnimationDescs != null)
				{
					foreach (SourceMdlAnimationDesc49 anAnimationDesc in this.theMdlFileData.theCorrectiveAnimationDescs)
					{
						smdPathFileName = Path.Combine(modelOutputPath, SourceFileNamesModule.CreateCorrectiveAnimationSmdRelativePathFileName(anAnimationDesc.theName, this.Name));
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

							writeStatus = "Failed";
							writeStatus = this.WriteCorrectiveAnimationSmdFile(smdPathFileName, null, anAnimationDesc);
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

				foreach (SourceMdlAnimationDesc49 anAnimationDesc in this.theMdlFileData.theAnimationDescs)
				{
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

		public override AppEnums.StatusMessage WriteVrdFile(string vrdPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, vrdPathFileName);
			this.WriteTextFile(vrdPathFileName, this.WriteVrdFile);
			this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, vrdPathFileName);

			return status;
		}

		public override AppEnums.StatusMessage WriteDeclareSequenceQciFile(string declareSequenceQciPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, declareSequenceQciPathFileName);
			this.WriteTextFile(declareSequenceQciPathFileName, this.WriteDeclareSequenceQciFile);
			this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, declareSequenceQciPathFileName);

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

					if (aBodyPart.theFlexFrames == null || aBodyPart.theFlexFrames.Count <= 1)
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
				this.theMdlFileData = new SourceMdlFileData49();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceAniFile49 aniFile = new SourceAniFile49(this.theInputFileReader, this.theAniFileData49, this.theMdlFileData);

			aniFile.ReadMdlHeader00("ANI File Header 00");
			aniFile.ReadMdlHeader01("ANI File Header 01");

			aniFile.ReadAnimationAniBlocks();
			aniFile.ReadUnreadBytes();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData49();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile49 mdlFile = new SourceMdlFile49(this.theInputFileReader, this.theMdlFileData);

			this.theMdlFileData.theSectionFrameCount = 0;
			//Me.theMdlFileData.theModelCommandIsUsed = False
			this.theMdlFileData.theProceduralBonesCommandIsUsed = false;
			this.theMdlFileData.theAnimBlockSizeNoStallOptionIsUsed = false;

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
			if (this.theMdlFileData.studioHeader2Offset > 0)
			{
				mdlFile.ReadMdlHeader02("MDL File Header 02");
			}

			// Read what WriteBoneInfo() writes.
			mdlFile.ReadBones();
			mdlFile.ReadBoneControllers();
			mdlFile.ReadAttachments();

			mdlFile.ReadHitboxSets();

			mdlFile.ReadBoneTableByName();

			// Read what WriteAnimations() writes.
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

			// Read what WriteSequenceInfo() writes.
			mdlFile.ReadSequenceDescs();
			mdlFile.ReadLocalNodeNames();
			mdlFile.ReadLocalNodes();

			// Read what WriteModel() writes.
			//Me.theCurrentFrameIndex = 0
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

			// Read what WriteTextures() writes.
			mdlFile.ReadTexturePaths();
			//NOTE: ReadTextures must be after ReadTexturePaths(), so it can compare with the texture paths.
			mdlFile.ReadTextures();
			mdlFile.ReadSkinFamilies();

			// Read what WriteKeyValues() writes.
			mdlFile.ReadKeyValues();

			// Read what WriteBoneTransforms() writes.
			mdlFile.ReadBoneTransforms();
			mdlFile.ReadLinearBoneTable();

			//TODO: ReadLocalIkAutoPlayLocks()
			mdlFile.ReadFlexControllerUis();

			//mdlFile.ReadFinalBytesAlignment()
			//mdlFile.ReadUnknownValues(Me.theMdlFileData.theFileSeekLog)
			mdlFile.ReadUnreadBytes();

			// Post-processing.
			mdlFile.PostProcess();
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
			//TODO: Why is this "If" statement needed?
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
			vvdFile.ReadVertexes(this.theMdlFileData.version);
			vvdFile.ReadFixups();
			vvdFile.ReadUnreadBytes();
		}

		protected override void WriteQcFile()
		{
			//Dim qcFile As New SourceQcFile49(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theVtxFileData, Me.thePhyFileDataGeneric, Me.theAniFileData49, Me.theName)
			SourceQcFile49 qcFile = new SourceQcFile49(this.theOutputFileTextWriter, this.theQcPathFileName, this.theMdlFileData, this.theVtxFileData, this.thePhyFileDataGeneric, this.theName);

			try
			{
				qcFile.WriteHeaderComment();

				qcFile.WriteModelNameCommand();

				qcFile.WriteUpAxisCommand();
				qcFile.WriteStaticPropCommand();
				qcFile.WriteConstantDirectionalLightCommand();

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

				this.SetUpCorrectiveSubtractAnimationBlocks();
				qcFile.WriteGroup("animation", qcFile.WriteGroupAnimation, false, false);

				qcFile.WriteGroup("collision", qcFile.WriteGroupCollision, false, false);

				string command = null;
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					command = "$KeyValues";
				}
				else
				{
					command = "$keyvalues";
				}
				qcFile.WriteKeyValues(this.theMdlFileData.theKeyValuesText, command);
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
				this.theMdlFileData = new SourceMdlFileData49();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile49 mdlFile = new SourceMdlFile49(this.theInputFileReader, this.theMdlFileData);

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
				this.theMdlFileData = new SourceMdlFileData49();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile49 mdlFile = new SourceMdlFile49(this.theInputFileReader, this.theMdlFileData);

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
			SourceVtxBodyPart07 aVtxBodyPart = null;
			SourceVtxModel07 aVtxBodyModel = null;
			SourceMdlModel aBodyModel = null;
			int bodyPartVertexIndexStart = 0;

			if (this.theVtxFileData.theVtxBodyParts != null && this.theMdlFileData.theBodyParts != null)
			{
				for (int bodyPartIndex = 0; bodyPartIndex < this.theVtxFileData.theVtxBodyParts.Count; bodyPartIndex++)
				{
					aVtxBodyPart = this.theVtxFileData.theVtxBodyParts[bodyPartIndex];

					if (aVtxBodyPart.theVtxModels != null)
					{
						for (int modelIndex = 0; modelIndex < aVtxBodyPart.theVtxModels.Count; modelIndex++)
						{
							aVtxBodyModel = aVtxBodyPart.theVtxModels[modelIndex];

							if (aVtxBodyModel.theVtxModelLods != null)
							{
								aBodyModel = this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex];
								if (aBodyModel.name[0] == '\0' && aVtxBodyModel.theVtxModelLods[0].theVtxMeshes == null)
								{
									continue;
								}

								for (int lodIndex = lodStartIndex; lodIndex <= lodStopIndex; lodIndex++)
								{
									//TODO: Why would this count be different than the file header count?
									if (lodIndex >= aVtxBodyModel.theVtxModelLods.Count)
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

									this.WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxBodyModel, aBodyModel, bodyPartVertexIndexStart);

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
			SourceSmdFile49 smdFile = new SourceSmdFile49(this.theOutputFileTextWriter, this.theMdlFileData, this.theVvdFileData49);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection();
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
			SourceSmdFile49 physicsMeshSmdFile = new SourceSmdFile49(this.theOutputFileTextWriter, this.theMdlFileData, this.thePhyFileDataGeneric);

			try
			{
				physicsMeshSmdFile.WriteHeaderComment();

				physicsMeshSmdFile.WriteHeaderSection();
				physicsMeshSmdFile.WriteNodesSection();
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
			SourceVrdFile49 vrdFile = new SourceVrdFile49(this.theOutputFileTextWriter, this.theMdlFileData);

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
			SourceQcFile49 qciFile = new SourceQcFile49(this.theOutputFileTextWriter, this.theMdlFileData, this.theName);

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
			SourceSmdFile49 smdFile = new SourceSmdFile49(this.theOutputFileTextWriter, this.theMdlFileData);

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
			SourceVtaFile49 vertexAnimationVtaFile = new SourceVtaFile49(this.theOutputFileTextWriter, this.theMdlFileData, this.theVvdFileData49, bodyPart);

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
			SourceMdlFile49 mdlFile = new SourceMdlFile49(this.theOutputFileBinaryWriter, this.theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
			mdlFile.WriteInternalMdlFileNameCopy(internalMdlFileName);
		}

		protected override void WriteAniFileNameToMdlFile(string internalAniFileName)
		{
			SourceMdlFile49 mdlFile = new SourceMdlFile49(this.theOutputFileBinaryWriter, this.theMdlFileData);

			mdlFile.WriteInternalAniFileName(internalAniFileName);
		}

		private void SetUpCorrectiveSubtractAnimationBlocks()
		{
			if (this.theMdlFileData.theSequenceDescs != null)
			{
				SourceMdlAnimationDesc49 anAnimationDesc = null;
				string name = null;
				this.theMdlFileData.theCorrectiveAnimationDescs = new List<SourceMdlAnimationDesc49>();

				foreach (SourceMdlSequenceDesc aSequenceDesc in this.theMdlFileData.theSequenceDescs)
				{
					if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_DELTA) > 0)
					{
						if (aSequenceDesc.theAnimDescIndexes != null && aSequenceDesc.theAnimDescIndexes.Count > 0)
						{
							for (int j = 0; j < aSequenceDesc.theAnimDescIndexes.Count; j++)
							{
								anAnimationDesc = this.theMdlFileData.theAnimationDescs[aSequenceDesc.theAnimDescIndexes[j]];
								name = anAnimationDesc.theName;

								if (name[0] == '@')
								{
									//NOTE: There should only be one implied anim desc.
									//aSequenceDesc.theCorrectiveSubtractAnimationOptionIsUsed = True
									aSequenceDesc.theCorrectiveAnimationName = SourceFileNamesModule.CreateCorrectiveAnimationName(name);
								}
								else
								{
									//anAnimationDesc.theCorrectiveSubtractAnimationOptionIsUsed = True
									anAnimationDesc.theCorrectiveAnimationName = SourceFileNamesModule.CreateCorrectiveAnimationName(name);
								}
								if (!this.theMdlFileData.theCorrectiveAnimationDescs.Contains(anAnimationDesc))
								{
									this.theMdlFileData.theCorrectiveAnimationDescs.Add(anAnimationDesc);
								}
							}
						}
					}
				}
			}
		}

		private string WriteCorrectiveAnimationSmdFile(string smdPathFileName, SourceMdlSequenceDescBase aSequenceDesc, SourceMdlAnimationDescBase anAnimationDesc)
		{
			string status = "Success";

			try
			{
				this.theOutputFileTextWriter = File.CreateText(smdPathFileName);

				SourceSmdFile49 smdFile = new SourceSmdFile49(this.theOutputFileTextWriter, this.theMdlFileData);

				try
				{
					smdFile.WriteHeaderComment();

					smdFile.WriteHeaderSection();
					smdFile.WriteNodesSection();
					smdFile.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc, true);
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
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
				if (this.theOutputFileTextWriter != null)
				{
					if (this.theOutputFileTextWriter.BaseStream != null)
					{
						this.theOutputFileTextWriter.Flush();
					}
					this.theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

#endregion

#region Data

		//Private theAniFileData49 As SourceAniFileData49
		private SourceMdlFileData49 theAniFileData49;
		private SourceMdlFileData49 theMdlFileData;
		//Private thePhyFileData49 As SourcePhyFileData
		private SourceVtxFileData07 theVtxFileData;
		private SourceVvdFileData04 theVvdFileData49;

		//Private theCorrectiveAnimationDescs As List(Of SourceMdlAnimationDesc49)

#endregion

	}

}