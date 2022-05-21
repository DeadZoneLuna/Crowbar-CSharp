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
	public class SourceModel32 : SourceModel31
	{
#region Creation and Destruction

		public SourceModel32(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{
		}

#endregion

#region Properties

		public override bool AniFileIsUsed
		{
			get
			{
				return false;
			}
		}

		//TODO: Delete after reading phy file is implemented.
		//Public Overrides ReadOnly Property PhyFileIsUsed As Boolean
		//	Get
		//		Return False
		//	End Get
		//End Property

		public override bool VtxFileIsUsed
		{
			get
			{
				return !string.IsNullOrEmpty(theVtxPathFileName) && File.Exists(theVtxPathFileName);
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
				//TODO: Change back to top line after reading texture info from MDL file is implemented.
				//Return Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations AndAlso Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0
				return false;
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
				//If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
				//		 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
				//		 AndAlso Me.theMdlFileData.theBones.Count > 0 _
				//		 AndAlso Me.theVtxFileData IsNot Nothing _
				//		 AndAlso Me.theVtxFileData.lodCount > 0 Then
				//	Return True
				//Else
				return false;
				//End If
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
				//If Me.theMdlFileData IsNot Nothing _
				// AndAlso Me.theMdlFileData.theProceduralBonesCommandIsUsed _
				// AndAlso Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
				// AndAlso Me.theMdlFileData.theBones IsNot Nothing _
				// AndAlso Me.theMdlFileData.theBones.Count > 0 Then
				//	Return True
				//Else
				return false;
				//End If
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
				//TODO: Change back to commented-out lines once implemented.
				//If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
				// AndAlso Me.theMdlFileData.theFlexDescs IsNot Nothing _
				// AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
				//	Return True
				//Else
				//	Return False
				//End If
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
									status = AppEnums.FilesFoundFlags.ErrorRequiredVtxFileNotFound;
								}
							}
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

		public AppEnums.StatusMessage WriteMeshSmdFile(string smdPathFileName, int lodIndex, SourceVtxModel06 aVtxModel, SourceMdlModel37 aModel, int bodyPartVertexIndexStart)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				theOutputFileTextWriter = File.CreateText(smdPathFileName);
				SourceSmdFile32 smdFile = new SourceSmdFile32(theOutputFileTextWriter, theMdlFileData);

				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection(lodIndex);
				smdFile.WriteSkeletonSection(lodIndex);
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

			SourceMdlAnimationDesc32 anAnimationDesc = null;
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

#endregion

#region Private Methods

		protected override void ReadMdlFileHeader_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData32();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile32 mdlFile = new SourceMdlFile32(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData32();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile32 mdlFile = new SourceMdlFile32(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");

			//'mdlFile.ReadTexturePaths()
			//mdlFile.ReadTextures()
		}

		protected override void ReadMdlFile_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData32();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile32 mdlFile = new SourceMdlFile32(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");

			// Read what WriteBoneInfo() writes.
			mdlFile.ReadBones();
			mdlFile.ReadBoneControllers();
			mdlFile.ReadAttachments();
			mdlFile.ReadHitboxSets();

			// Read what WriteSequenceInfo() writes.
			//NOTE: Must read sequences before reading animations.
			mdlFile.ReadSequences();
			mdlFile.ReadSequenceGroups();
			mdlFile.ReadTransitions();

			// Read what WriteAnimations() writes.
			mdlFile.ReadLocalAnimationDescs();

			// Read what WriteModel() writes.
			mdlFile.ReadBodyParts();
			mdlFile.ReadFlexDescs();
			mdlFile.ReadFlexControllers();
			//NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
			mdlFile.ReadFlexRules();
			mdlFile.ReadIkChains();
			mdlFile.ReadIkLocks();
			mdlFile.ReadMouths();
			mdlFile.ReadPoseParamDescs();

			// Read what WriteTextures() writes.
			mdlFile.ReadTexturePaths();
			//NOTE: ReadTextures must be after ReadTexturePaths(), so it can compare with the texture paths.
			mdlFile.ReadTextures();
			mdlFile.ReadSkinFamilies();

			//' Read what WriteKeyValues() writes.
			//mdlFile.ReadKeyValues()

			//mdlFile.ReadFinalBytesAlignment()
			mdlFile.ReadUnreadBytes();

			//' Post-processing.
			//mdlFile.BuildBoneTransforms()
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
				theVtxFileData = new SourceVtxFileData06();
			}

			SourceVtxFile06 vtxFile = new SourceVtxFile06(theInputFileReader, theVtxFileData);

			vtxFile.ReadSourceVtxHeader();
			//If Me.theVtxFileData.lodCount > 0 Then
			vtxFile.ReadSourceVtxBodyParts();
			//End If
			vtxFile.ReadSourceVtxMaterialReplacementLists();
		}

		protected override void WriteQcFile()
		{
			SourceQcFile32 qcFile = new SourceQcFile32(theOutputFileTextWriter, theQcPathFileName, theMdlFileData, thePhyFileDataGeneric, theVtxFileData, theName);

			try
			{
				qcFile.WriteHeaderComment();

				qcFile.WriteModelNameCommand();

				qcFile.WriteStaticPropCommand();

				if (theMdlFileData.theModelCommandIsUsed)
				{
					qcFile.WriteModelCommand();
					qcFile.WriteBodyGroupCommand(1);
				}
				else
				{
					qcFile.WriteBodyGroupCommand(0);
				}
				qcFile.WriteGroup("lod", qcFile.WriteGroupLod, false, false);

				qcFile.WriteSurfacePropCommand();
				qcFile.WriteJointSurfacePropCommand();
				qcFile.WriteContentsCommand();
				qcFile.WriteJointContentsCommand();
				qcFile.WriteIllumPositionCommand();

				qcFile.WriteEyePositionCommand();
				qcFile.WriteNoForcedFadeCommand();
				qcFile.WriteForcePhonemeCrossfadeCommand();

				qcFile.WriteAmbientBoostCommand();
				qcFile.WriteOpaqueCommand();
				qcFile.WriteObsoleteCommand();
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

				//qcFile.WriteKeyValues(Me.theMdlFileData.theKeyValuesText, "$KeyValues")
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}
		}

		protected override AppEnums.StatusMessage WriteMeshSmdFiles(string modelOutputPath, int lodStartIndex, int lodStopIndex)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			//Dim smdFileName As String
			string smdPathFileName = null;
			SourceVtxBodyPart06 aBodyPart = null;
			SourceVtxModel06 aVtxModel = null;
			SourceMdlModel37 aBodyModel = null;
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
										string bodyModelName = theMdlFileData.theSequenceGroups[0].theFileName;
										if (string.IsNullOrEmpty(bodyModelName) || FileManager.FilePathHasInvalidChars(bodyModelName))
										{
											bodyModelName = new string(theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name);
										}
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

		protected override void WritePhysicsMeshSmdFile()
		{
			SourceSmdFile32 physicsMeshSmdFile = new SourceSmdFile32(theOutputFileTextWriter, theMdlFileData, thePhyFileDataGeneric);

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

		protected override void WriteBoneAnimationSmdFile(SourceMdlSequenceDescBase aSequenceDesc, SourceMdlAnimationDescBase anAnimationDesc)
		{
			SourceSmdFile32 smdFile = new SourceSmdFile32(theOutputFileTextWriter, theMdlFileData);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection(-1);
				smdFile.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

#endregion

#region Data

		private SourceMdlFileData32 theMdlFileData;
		//Private thePhyFileData As SourcePhyFileData37
		private SourceVtxFileData06 theVtxFileData;

#endregion

	}

}