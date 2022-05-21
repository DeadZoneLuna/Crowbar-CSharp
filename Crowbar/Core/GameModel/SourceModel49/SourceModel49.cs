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

		public override bool AniFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(theAniPathFileName) && File.Exists(theAniPathFileName);
			}
		}

		public override bool VvdFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(theVvdPathFileName) && File.Exists(theVvdPathFileName);
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

			if (theMdlFileData.animBlockCount > 0)
			{
				theAniPathFileName = Path.ChangeExtension(theMdlPathFileName, ".ani");
				if (!File.Exists(theAniPathFileName))
				{
					status |= AppEnums.FilesFoundFlags.ErrorRequiredAniFileNotFound;
				}
			}

			//If Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations Then
			thePhyPathFileName = Path.ChangeExtension(theMdlPathFileName, ".phy");

			//TODO: If the checksum of the vtx does not match checksum in MDL, check the next vtx.
			theVtxPathFileName = Path.ChangeExtension(theMdlPathFileName, ".dx11.vtx");
			if (!File.Exists(theVtxPathFileName))
			{
				theVtxPathFileName = Path.ChangeExtension(theMdlPathFileName, ".dx90.vtx");
				if (!File.Exists(theVtxPathFileName))
				{
					theVtxPathFileName = Path.ChangeExtension(theMdlPathFileName, ".dx80.vtx");
					if (!File.Exists(theVtxPathFileName))
					{
						theVtxPathFileName = Path.ChangeExtension(theMdlPathFileName, ".sw.vtx");
						if (!File.Exists(theVtxPathFileName))
						{
							theVtxPathFileName = Path.ChangeExtension(theMdlPathFileName, ".vtx");
							if (!File.Exists(theVtxPathFileName))
							{
								status |= AppEnums.FilesFoundFlags.ErrorRequiredVtxFileNotFound;
							}
						}
					}
				}
			}

			theVvdPathFileName = Path.ChangeExtension(theMdlPathFileName, ".vvd");
			if (!File.Exists(theVvdPathFileName))
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

			if (!string.IsNullOrEmpty(thePhyPathFileName))
			{
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
			}

			return status;
		}

		public override AppEnums.StatusMessage ReadAniFile()
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//If String.IsNullOrEmpty(Me.theAniPathFileName) Then
			//	status = Me.CheckForRequiredFiles()
			//End If

			if (!string.IsNullOrEmpty(theAniPathFileName))
			{
				if (status == AppEnums.StatusMessage.Success)
				{
					try
					{
						ReadFile(theAniPathFileName, ReadAniFile_Internal);
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

			status = WriteMeshSmdFiles(modelOutputPath, 0, 0);

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

			string smdPathFileName = null;
			string smdPath = null;
			string writeStatus = null;

			try
			{
				if (theMdlFileData.theCorrectiveAnimationDescs != null)
				{
					foreach (SourceMdlAnimationDesc49 anAnimationDesc in theMdlFileData.theCorrectiveAnimationDescs)
					{
						smdPathFileName = Path.Combine(modelOutputPath, SourceFileNamesModule.CreateCorrectiveAnimationSmdRelativePathFileName(anAnimationDesc.theName, Name));
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

							writeStatus = "Failed";
							writeStatus = WriteCorrectiveAnimationSmdFile(smdPathFileName, null, anAnimationDesc);
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

				foreach (SourceMdlAnimationDesc49 anAnimationDesc in theMdlFileData.theAnimationDescs)
				{
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

		public override AppEnums.StatusMessage WriteVrdFile(string vrdPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, vrdPathFileName);
			WriteTextFile(vrdPathFileName, WriteVrdFile);
			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, vrdPathFileName);

			return status;
		}

		public override AppEnums.StatusMessage WriteDeclareSequenceQciFile(string declareSequenceQciPathFileName)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, declareSequenceQciPathFileName);
			WriteTextFile(declareSequenceQciPathFileName, WriteDeclareSequenceQciFile);
			NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, declareSequenceQciPathFileName);

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

					if (aBodyPart.theFlexFrames == null || aBodyPart.theFlexFrames.Count <= 1)
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
				theMdlFileData = new SourceMdlFileData49();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceAniFile49 aniFile = new SourceAniFile49(theInputFileReader, theAniFileData49, theMdlFileData);

			aniFile.ReadMdlHeader00("ANI File Header 00");
			aniFile.ReadMdlHeader01("ANI File Header 01");

			aniFile.ReadAnimationAniBlocks();
			aniFile.ReadUnreadBytes();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData49();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile49 mdlFile = new SourceMdlFile49(theInputFileReader, theMdlFileData);

			theMdlFileData.theSectionFrameCount = 0;
			//Me.theMdlFileData.theModelCommandIsUsed = False
			theMdlFileData.theProceduralBonesCommandIsUsed = false;
			theMdlFileData.theAnimBlockSizeNoStallOptionIsUsed = false;

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
			if (theMdlFileData.studioHeader2Offset > 0)
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
			Common.ProcessTexturePaths(theMdlFileData.theTexturePaths, theMdlFileData.theTextures, theMdlFileData.theModifiedTexturePaths, theMdlFileData.theModifiedTextureFileNames);
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
				theVtxFileData = new SourceVtxFileData07();
			}

			//TEST: When a model has a nameCopy, it seems to also use the VTF file strip group topology fields.
			SourceVtxFile07 vtxFile = new SourceVtxFile07(theInputFileReader, theVtxFileData);

			vtxFile.ReadSourceVtxHeader();
			//TODO: Why is this "If" statement needed?
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

			SourceVvdFile04 vvdFile = new SourceVvdFile04(theInputFileReader, theVvdFileData49);

			vvdFile.ReadSourceVvdHeader();
			vvdFile.ReadVertexes(theMdlFileData.version);
			vvdFile.ReadFixups();
			vvdFile.ReadUnreadBytes();
		}

		protected override void WriteQcFile()
		{
			//Dim qcFile As New SourceQcFile49(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theVtxFileData, Me.thePhyFileDataGeneric, Me.theAniFileData49, Me.theName)
			SourceQcFile49 qcFile = new SourceQcFile49(theOutputFileTextWriter, theQcPathFileName, theMdlFileData, theVtxFileData, thePhyFileDataGeneric, theName);

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

				SetUpCorrectiveSubtractAnimationBlocks();
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
				qcFile.WriteKeyValues(theMdlFileData.theKeyValuesText, command);
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
				theMdlFileData = new SourceMdlFileData49();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile49 mdlFile = new SourceMdlFile49(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
			if (theMdlFileData.studioHeader2Offset > 0)
			{
				mdlFile.ReadMdlHeader02("MDL File Header 02");
			}

			//If Me.theMdlFileData.fileSize <> Me.theMdlFileData.theActualFileSize Then
			//	status = StatusMessage.ErrorInvalidInternalMdlFileSize
			//End If
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData49();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile49 mdlFile = new SourceMdlFile49(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
			if (theMdlFileData.studioHeader2Offset > 0)
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

			if (theVtxFileData.theVtxBodyParts != null && theMdlFileData.theBodyParts != null)
			{
				for (int bodyPartIndex = 0; bodyPartIndex < theVtxFileData.theVtxBodyParts.Count; bodyPartIndex++)
				{
					aVtxBodyPart = theVtxFileData.theVtxBodyParts[bodyPartIndex];

					if (aVtxBodyPart.theVtxModels != null)
					{
						for (int modelIndex = 0; modelIndex < aVtxBodyPart.theVtxModels.Count; modelIndex++)
						{
							aVtxBodyModel = aVtxBodyPart.theVtxModels[modelIndex];

							if (aVtxBodyModel.theVtxModelLods != null)
							{
								aBodyModel = theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex];
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

									WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxBodyModel, aBodyModel, bodyPartVertexIndexStart);

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
			SourceSmdFile49 smdFile = new SourceSmdFile49(theOutputFileTextWriter, theMdlFileData, theVvdFileData49);

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
			SourceSmdFile49 physicsMeshSmdFile = new SourceSmdFile49(theOutputFileTextWriter, theMdlFileData, thePhyFileDataGeneric);

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
			SourceVrdFile49 vrdFile = new SourceVrdFile49(theOutputFileTextWriter, theMdlFileData);

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
			SourceQcFile49 qciFile = new SourceQcFile49(theOutputFileTextWriter, theMdlFileData, theName);

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
			SourceSmdFile49 smdFile = new SourceSmdFile49(theOutputFileTextWriter, theMdlFileData);

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
			SourceVtaFile49 vertexAnimationVtaFile = new SourceVtaFile49(theOutputFileTextWriter, theMdlFileData, theVvdFileData49, bodyPart);

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
			SourceMdlFile49 mdlFile = new SourceMdlFile49(theOutputFileBinaryWriter, theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
			mdlFile.WriteInternalMdlFileNameCopy(internalMdlFileName);
		}

		protected override void WriteAniFileNameToMdlFile(string internalAniFileName)
		{
			SourceMdlFile49 mdlFile = new SourceMdlFile49(theOutputFileBinaryWriter, theMdlFileData);

			mdlFile.WriteInternalAniFileName(internalAniFileName);
		}

		private void SetUpCorrectiveSubtractAnimationBlocks()
		{
			if (theMdlFileData.theSequenceDescs != null)
			{
				SourceMdlAnimationDesc49 anAnimationDesc = null;
				string name = null;
				theMdlFileData.theCorrectiveAnimationDescs = new List<SourceMdlAnimationDesc49>();

				foreach (SourceMdlSequenceDesc aSequenceDesc in theMdlFileData.theSequenceDescs)
				{
					if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_DELTA) > 0)
					{
						if (aSequenceDesc.theAnimDescIndexes != null && aSequenceDesc.theAnimDescIndexes.Count > 0)
						{
							for (int j = 0; j < aSequenceDesc.theAnimDescIndexes.Count; j++)
							{
								anAnimationDesc = theMdlFileData.theAnimationDescs[aSequenceDesc.theAnimDescIndexes[j]];
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
								if (!theMdlFileData.theCorrectiveAnimationDescs.Contains(anAnimationDesc))
								{
									theMdlFileData.theCorrectiveAnimationDescs.Add(anAnimationDesc);
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
				theOutputFileTextWriter = File.CreateText(smdPathFileName);

				SourceSmdFile49 smdFile = new SourceSmdFile49(theOutputFileTextWriter, theMdlFileData);

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