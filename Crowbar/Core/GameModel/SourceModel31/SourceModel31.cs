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
	public class SourceModel31 : SourceModel2531
	{
#region Creation and Destruction

		public SourceModel31(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
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
				return !string.IsNullOrEmpty(this.theVtxPathFileName) && File.Exists(this.theVtxPathFileName);
				//Return False
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
									status = AppEnums.FilesFoundFlags.ErrorRequiredVtxFileNotFound;
								}
							}
						}
					}
				}
			}

			return status;
		}

		//Public Overrides Function ReadPhyFile() As AppEnums.StatusMessage
		//	Dim status As AppEnums.StatusMessage = StatusMessage.Success

		//	If String.IsNullOrEmpty(Me.thePhyPathFileName) Then
		//		status = Me.CheckForRequiredFiles()
		//	End If

		//	If Not String.IsNullOrEmpty(Me.thePhyPathFileName) Then
		//		If status = StatusMessage.Success Then
		//			Try
		//				Me.ReadFile(Me.thePhyPathFileName, AddressOf Me.ReadPhyFile_Internal)
		//				If Me.thePhyFileData.checksum <> Me.theMdlFileData.checksum Then
		//					'status = StatusMessage.WarningPhyChecksumDoesNotMatchMdl
		//					Me.NotifySourceModelProgress(ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum, "")
		//				End If
		//			Catch ex As Exception
		//				status = StatusMessage.Error
		//			End Try
		//		End If
		//	End If

		//	Return status
		//End Function

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

			SourceMdlAnimationDesc31 anAnimationDesc = null;
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

		public AppEnums.StatusMessage WriteMeshSmdFile(string smdPathFileName, int lodIndex, SourceVtxModel06 aVtxModel, SourceMdlModel31 aModel, int bodyPartVertexIndexStart)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				this.theOutputFileTextWriter = File.CreateText(smdPathFileName);
				SourceSmdFile31 smdFile = new SourceSmdFile31(this.theOutputFileTextWriter, this.theMdlFileData);

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
				if (this.theOutputFileTextWriter != null)
				{
					this.theOutputFileTextWriter.Flush();
					this.theOutputFileTextWriter.Close();
				}
			}

			return status;
		}

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

#endregion

#region Private Methods

		protected override void ReadMdlFileHeader_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData31();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile31 mdlFile = new SourceMdlFile31(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData31();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile31 mdlFile = new SourceMdlFile31(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");

			//'mdlFile.ReadTexturePaths()
			//mdlFile.ReadTextures()
		}

		protected override void ReadMdlFile_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData31();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile31 mdlFile = new SourceMdlFile31(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader00("MDL File Header 00");
			mdlFile.ReadMdlHeader01("MDL File Header 01");

			// Read what WriteBoneInfo() writes.
			mdlFile.ReadBones();
			//mdlFile.ReadBoneControllers()
			//mdlFile.ReadAttachments()
			if (this.theMdlFileData.version >= 27 && this.theMdlFileData.version <= 30)
			{
				mdlFile.ReadHitboxes_MDL27to30();
			}
			else
			{
				mdlFile.ReadHitboxSets();
			}
			//mdlFile.ReadBoneDescs()

			//' Read what WriteSequenceInfo() writes.
			//'NOTE: Must read sequences before reading animations.
			//mdlFile.ReadAnimGroups()
			mdlFile.ReadSequences();
			mdlFile.ReadSequenceGroups();
			//mdlFile.ReadTransitions()

			//' Read what WriteAnimations() writes.
			mdlFile.ReadLocalAnimationDescs();

			//' Read what WriteModel() writes.
			mdlFile.ReadBodyParts();
			//mdlFile.ReadFlexDescs()
			//mdlFile.ReadFlexControllers()
			//'NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
			//mdlFile.ReadFlexRules()
			//mdlFile.ReadIkChains()
			//mdlFile.ReadIkLocks()
			//mdlFile.ReadMouths()
			//mdlFile.ReadPoseParamDescs()

			//' Read what WriteTextures() writes.
			mdlFile.ReadTexturePaths();
			//'NOTE: ReadTextures must be after ReadTexturePaths(), so it can compare with the texture paths.
			mdlFile.ReadTextures();
			mdlFile.ReadSkinFamilies();

			// '' Read what WriteKeyValues() writes.
			//'mdlFile.ReadKeyValues()

			//mdlFile.ReadFinalBytesAlignment()
			mdlFile.ReadUnreadBytes();

			// '' Post-processing.
			//'mdlFile.BuildBoneTransforms()
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
				this.theVtxFileData = new SourceVtxFileData06();
			}

			SourceVtxFile06 vtxFile = new SourceVtxFile06(this.theInputFileReader, this.theVtxFileData);

			vtxFile.ReadSourceVtxHeader();
			//If Me.theVtxFileData.lodCount > 0 Then
			vtxFile.ReadSourceVtxBodyParts();
			//End If
			vtxFile.ReadSourceVtxMaterialReplacementLists();
			vtxFile.ReadUnreadBytes();
		}

		protected override void WriteQcFile()
		{
			//Dim qcFile As New SourceQcFile31(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.thePhyFileData, Me.theVtxFileData, Me.theName)
			SourceQcFile31 qcFile = new SourceQcFile31(this.theOutputFileTextWriter, this.theQcPathFileName, this.theMdlFileData, this.theVtxFileData, this.thePhyFileDataGeneric, this.theName);

			try
			{
				qcFile.WriteHeaderComment();

				qcFile.WriteModelNameCommand();

				qcFile.WriteStaticPropCommand();

				qcFile.WriteBodyGroupCommand();
				qcFile.WriteGroup("lod", qcFile.WriteGroupLod, false, false);

				qcFile.WriteSurfacePropCommand();
				//qcFile.WriteJointSurfacePropCommand()
				//qcFile.WriteContentsCommand()
				//qcFile.WriteJointContentsCommand()
				qcFile.WriteIllumPositionCommand();

				qcFile.WriteEyePositionCommand();
				//qcFile.WriteNoForcedFadeCommand()
				//qcFile.WriteForcePhonemeCrossfadeCommand()

				//qcFile.WriteAmbientBoostCommand()
				//qcFile.WriteOpaqueCommand()
				//qcFile.WriteObsoleteCommand()
				qcFile.WriteCdMaterialsCommand();
				qcFile.WriteTextureGroupCommand();
				if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
				{
					qcFile.WriteTextureFileNameComments();
				}

				//qcFile.WriteAttachmentCommand()

				qcFile.WriteGroup("box", qcFile.WriteGroupBox, true, false);

				//qcFile.WriteControllerCommand()
				//qcFile.WriteScreenAlignCommand()

				//qcFile.WriteGroup("bone", AddressOf qcFile.WriteGroupBone, False, False)

				qcFile.WriteGroup("animation", qcFile.WriteGroupAnimation, false, false);

				qcFile.WriteGroup("collision", qcFile.WriteGroupCollision, false, false);

				//'qcFile.WriteKeyValues(Me.theMdlFileData.theKeyValuesText, "$KeyValues")
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
			SourceMdlModel31 aBodyModel = null;
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

									try
									{
										string bodyModelName = this.theMdlFileData.theSequenceGroups[0].theFileName;
										if (string.IsNullOrEmpty(bodyModelName) || FileManager.FilePathHasInvalidChars(bodyModelName))
										{
											bodyModelName = new string(this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name);
										}
										aBodyModel.theSmdFileNames[lodIndex] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[lodIndex], bodyPartIndex, modelIndex, lodIndex, this.theName, bodyModelName);
										smdPathFileName = Path.Combine(modelOutputPath, aBodyModel.theSmdFileNames[lodIndex]);

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
			SourceSmdFile31 physicsMeshSmdFile = new SourceSmdFile31(this.theOutputFileTextWriter, this.theMdlFileData, this.thePhyFileDataGeneric);

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
			SourceSmdFile31 smdFile = new SourceSmdFile31(this.theOutputFileTextWriter, this.theMdlFileData);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection(-1);
				//If Me.theMdlFileData.theFirstAnimationDesc IsNot Nothing AndAlso Me.theMdlFileData.theFirstAnimationDescFrameLines.Count = 0 Then
				//	smdFile.CalculateFirstAnimDescFrameLinesForSubtract()
				//End If
				smdFile.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

#endregion

#region Data

		private SourceMdlFileData31 theMdlFileData;
		private SourceVtxFileData06 theVtxFileData;

#endregion

	}

}