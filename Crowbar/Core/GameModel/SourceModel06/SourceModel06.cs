//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

// Example: barney from HLAlpha

namespace Crowbar
{
	public class SourceModel06 : SourceModel04
	{
#region Creation and Destruction

		public SourceModel06(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{
		}

#endregion

#region Properties

		public override bool HasTextureData
		{
			get
			{
				return this.theMdlFileData.theTextures != null && this.theMdlFileData.theTextures.Count > 0;
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
				return this.theMdlFileData.theTextures != null && this.theMdlFileData.theTextures.Count > 0;
			}
		}

#endregion

#region Methods

		public override AppEnums.FilesFoundFlags CheckForRequiredFiles()
		{
			AppEnums.FilesFoundFlags status = AppEnums.FilesFoundFlags.AllFilesFound;

			return status;
		}

		public override AppEnums.StatusMessage WriteReferenceMeshFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlBodyPart06 aBodyPart = null;
			SourceMdlModel06 aBodyModel = null;
			//Dim smdFileName As String
			string smdPathFileName = null;
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
						}
					}
				}
			}

			return status;
		}

		public override AppEnums.StatusMessage WriteBoneAnimationSmdFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlSequenceDesc06 aSequenceDesc = null;
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
							aSequenceDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileName, this.theName, aSequenceDesc.theName, -1);
						}
						else
						{
							aSequenceDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileName, this.theName, aSequenceDesc.theName, blendIndex);
						}

						smdPathFileName = Path.Combine(modelOutputPath, aSequenceDesc.theSmdRelativePathFileName);
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

		public override AppEnums.StatusMessage WriteTextureFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			List<SourceMdlTexture06> aTextureList = null;
			if (this.theMdlFileData.theTextures != null && this.theMdlFileData.theTextures.Count > 0)
			{
				aTextureList = this.theMdlFileData.theTextures;
			}
			else
			{
				return AppEnums.StatusMessage.Error;
			}

			string texturePath = null;
			string texturePathFileName = null;
			SourceMdlTexture06 aTexture = null;
			for (int textureIndex = 0; textureIndex < aTextureList.Count; textureIndex++)
			{
				try
				{
					aTexture = aTextureList[textureIndex];
					texturePathFileName = Path.Combine(modelOutputPath, aTexture.theFileName);
					texturePath = FileManager.GetPath(texturePathFileName);
					if (FileManager.PathExistsAfterTryToCreate(texturePath))
					{
						this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileStarted, texturePathFileName);
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

						BitmapFile aBitmap = new BitmapFile(texturePathFileName, aTexture.width, aTexture.height, aTexture.theData);
						aBitmap.Write();

						this.NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, texturePathFileName);
					}
				}
				catch (Exception ex)
				{
					status = AppEnums.StatusMessage.Error;
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

			return status;
		}

		public override List<string> GetTextureFileNames()
		{
			List<string> textureFileNames = new List<string>();

			for (int i = 0; i < this.theMdlFileData.theTextures.Count; i++)
			{
				SourceMdlTexture06 aTexture = this.theMdlFileData.theTextures[i];

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
				this.theMdlFileData = new SourceMdlFileData06();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile06 mdlFile = new SourceMdlFile06(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData06();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile06 mdlFile = new SourceMdlFile06(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();

			//mdlFile.ReadTexturePaths()
			mdlFile.ReadTextures();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (this.theMdlFileData == null)
			{
				this.theMdlFileData = new SourceMdlFileData06();
				this.theMdlFileDataGeneric = this.theMdlFileData;
			}

			SourceMdlFile06 mdlFile = new SourceMdlFile06(this.theInputFileReader, this.theMdlFileData);

			mdlFile.ReadMdlHeader();

			mdlFile.ReadBones();
			mdlFile.ReadBoneControllers();

			//NOTE: Must read sequences before reading animations.
			mdlFile.ReadSequences();
			mdlFile.ReadAnimations();

			mdlFile.ReadBodyParts();

			mdlFile.ReadTextures();
			mdlFile.ReadSkins();

			mdlFile.ReadUnreadBytes();

			// Post-processing.
			mdlFile.GetBoneDataFromFirstSequenceFirstFrame();
			mdlFile.BuildBoneTransforms();
		}

		protected override void WriteQcFile()
		{
			SourceQcFile06 qcFile = new SourceQcFile06(this.theOutputFileTextWriter, this.theQcPathFileName, this.theMdlFileData, this.theName);

			try
			{
				qcFile.WriteHeaderComment();

				qcFile.WriteModelNameCommand();

				qcFile.WriteBodyGroupCommand();

				//If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				//	qcFile.WriteTextureFileNameComments()
				//End If

				qcFile.WriteControllerCommand();

				qcFile.WriteSequenceCommands();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}
		}

		protected AppEnums.StatusMessage WriteMeshSmdFile(string smdPathFileName, SourceMdlModel06 aModel)
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

		protected void WriteMeshSmdFile(SourceMdlModel06 aModel)
		{
			bool externalTexturesAreUsed = false;

			SourceSmdFile06 smdFile = new SourceSmdFile06(this.theOutputFileTextWriter, this.theMdlFileData);

			try
			{
				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection();
				smdFile.WriteSkeletonSection();
				smdFile.WriteTrianglesSection(aModel);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			if (externalTexturesAreUsed)
			{
				this.theMdlFileData.theTextures = null;
			}
		}

		public AppEnums.StatusMessage WriteBoneAnimationSmdFile(string smdPathFileName, SourceMdlSequenceDesc06 aSequence, int blendIndex)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				this.theOutputFileTextWriter = File.CreateText(smdPathFileName);

				SourceSmdFile06 smdFile = new SourceSmdFile06(this.theOutputFileTextWriter, this.theMdlFileData);

				smdFile.WriteHeaderComment();

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

		protected override void WriteMdlFileNameToMdlFile(string internalMdlFileName)
		{
			SourceMdlFile06 mdlFile = new SourceMdlFile06(this.theOutputFileBinaryWriter, this.theMdlFileData);

			mdlFile.WriteInternalMdlFileName(internalMdlFileName);
		}

#endregion

#region Data

		private SourceMdlFileData06 theMdlFileData;
		//Private theTextureMdlFileData10 As SourceMdlFileData06

#endregion

	}

}