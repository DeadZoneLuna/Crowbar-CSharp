//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

// Example: PLAYER from HLAlpha

namespace Crowbar
{
	public class SourceModel04 : SourceModel
	{
#region Creation and Destruction

		public SourceModel04(string mdlPathFileName, int mdlVersion) : base(mdlPathFileName, mdlVersion)
		{
		}

#endregion

#region Properties

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
				if (theMdlFileData.theSequenceDescs != null && theMdlFileData.theSequenceDescs.Count > 0)
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
				return true;
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

			SourceMdlBodyPart04 aBodyPart = null;
			SourceMdlModel04 aBodyModel = null;
			//Dim smdFileName As String
			string smdPathFileName = null;
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

							aBodyModel.theSmdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileName, bodyPartIndex, modelIndex, 0, theName, "");
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
						}
					}
				}
			}

			return status;
		}

		public override AppEnums.StatusMessage WriteBoneAnimationSmdFiles(string modelOutputPath)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			SourceMdlSequenceDesc04 aSequenceDesc = null;
			string smdPath = null;
			string smdPathFileName = null;

			try
			{
				for (int aSequenceIndex = 0; aSequenceIndex < theMdlFileData.theSequenceDescs.Count; aSequenceIndex++)
				{
					aSequenceDesc = theMdlFileData.theSequenceDescs[aSequenceIndex];

					aSequenceDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileName, theName, aSequenceDesc.theName, -1);

					smdPathFileName = Path.Combine(modelOutputPath, aSequenceDesc.theSmdRelativePathFileName);
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

						WriteBoneAnimationSmdFile(smdPathFileName, aSequenceDesc);

						NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, smdPathFileName);
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

			SourceMdlBodyPart04 aBodyPart = null;
			SourceMdlModel04 aModel = null;
			SourceMdlMesh04 aMesh = null;
			string texturePath = null;
			string textureFileName = null;
			string texturePathFileName = null;
			for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
			{
				aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];
				for (int modelIndex = 0; modelIndex < aBodyPart.theModels.Count; modelIndex++)
				{
					aModel = aBodyPart.theModels[modelIndex];
					for (int meshIndex = 0; meshIndex < aModel.theMeshes.Count; meshIndex++)
					{
						aMesh = aModel.theMeshes[meshIndex];
						try
						{
							texturePath = modelOutputPath;
							//textureFileName = "bodypart" + bodyPartIndex.ToString() + "_model" + modelIndex.ToString() + "_mesh" + meshIndex.ToString() + ".bmp"
							textureFileName = aMesh.theTextureFileName;
							texturePathFileName = Path.Combine(texturePath, textureFileName);
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

								BitmapFile aBitmap = new BitmapFile(texturePathFileName, aMesh.textureWidth, aMesh.textureHeight, aMesh.theTextureBmpData);
								aBitmap.Write();

								NotifySourceModelProgress(AppEnums.ProgressOptions.WritingFileFinished, texturePathFileName);
							}
						}
						catch (Exception ex)
						{
							status = AppEnums.StatusMessage.Error;
						}
					}
				}
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

			return status;
		}

#endregion

#region Private Methods

		protected override void ReadMdlFileHeader_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData04();
				theMdlFileDataGeneric = theMdlFileData;
			}

			SourceMdlFile04 mdlFile = new SourceMdlFile04(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader();
		}

		protected override void ReadMdlFileForViewer_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData04();
				theMdlFileDataGeneric = theMdlFileData;
			}


			SourceMdlFile04 mdlFile = new SourceMdlFile04(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader();
		}

		protected override void ReadMdlFile_Internal()
		{
			if (theMdlFileData == null)
			{
				theMdlFileData = new SourceMdlFileData04();
				theMdlFileDataGeneric = theMdlFileData;
			}


			SourceMdlFile04 mdlFile = new SourceMdlFile04(theInputFileReader, theMdlFileData);

			mdlFile.ReadMdlHeader();

			mdlFile.ReadBones();
			mdlFile.ReadSequenceDescs();
			mdlFile.ReadBodyParts();

			mdlFile.ReadUnreadBytes();

			// Post-processing.
			//mdlFile.GetBoneDataFromFirstSequenceFirstFrame()
		}

		protected AppEnums.StatusMessage WriteMeshSmdFile(string smdPathFileName, SourceMdlModel04 aModel)
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

		protected void WriteMeshSmdFile(SourceMdlModel04 aModel)
		{
			bool externalTexturesAreUsed = false;

			SourceSmdFile04 smdFile = new SourceSmdFile04(theOutputFileTextWriter, theMdlFileData);

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
		}

		public AppEnums.StatusMessage WriteBoneAnimationSmdFile(string smdPathFileName, SourceMdlSequenceDesc04 aSequence)
		{
			AppEnums.StatusMessage status = AppEnums.StatusMessage.Success;

			try
			{
				theOutputFileTextWriter = File.CreateText(smdPathFileName);

				SourceSmdFile04 smdFile = new SourceSmdFile04(theOutputFileTextWriter, theMdlFileData);

				smdFile.WriteHeaderComment();

				smdFile.WriteHeaderSection();
				smdFile.WriteNodesSection();
				smdFile.WriteSkeletonSectionForAnimation(aSequence);
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

#endregion

#region Data

		private SourceMdlFileData04 theMdlFileData;

#endregion

	}

}