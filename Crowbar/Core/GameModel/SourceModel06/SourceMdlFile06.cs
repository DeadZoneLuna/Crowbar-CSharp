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
	public class SourceMdlFile06
	{

#region Creation and Destruction

		public SourceMdlFile06(BinaryReader mdlFileReader, SourceMdlFileData06 mdlFileData)
		{
			this.theInputFileReader = mdlFileReader;
			this.theMdlFileData = mdlFileData;

			this.theMdlFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile06(BinaryWriter mdlFileWriter, SourceMdlFileData06 mdlFileData)
		{
			this.theOutputFileWriter = mdlFileWriter;
			this.theMdlFileData = mdlFileData;
		}

#endregion

#region Methods

		public void ReadMdlHeader()
		{
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			this.theMdlFileData.id = this.theInputFileReader.ReadChars(4);
			this.theMdlFileData.theID = new string(this.theMdlFileData.id);
			this.theMdlFileData.version = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.name = this.theInputFileReader.ReadChars(64);
			this.theMdlFileData.theModelName = (new string(this.theMdlFileData.name)).Trim('\0');

			this.theMdlFileData.fileSize = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.theActualFileSize = this.theInputFileReader.BaseStream.Length;

			this.theMdlFileData.boneCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.boneControllerCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneControllerOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.sequenceCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.sequenceOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.textureCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.textureOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.textureDataOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.skinReferenceCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinFamilyCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.bodyPartCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.bodyPartOffset = this.theInputFileReader.ReadInt32();

			for (int x = 0; x < this.theMdlFileData.unused.Length; x++)
			{
				this.theMdlFileData.unused[x] = this.theInputFileReader.ReadInt32();
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header");
		}

		public void ReadBones()
		{
			if (this.theMdlFileData.boneCount > 0)
			{
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theBones = new List<SourceMdlBone06>(this.theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < this.theMdlFileData.boneCount; boneIndex++)
					{
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlBone06 aBone = new SourceMdlBone06();

						aBone.name = this.theInputFileReader.ReadChars(32);
						aBone.theName = new string(aBone.name);
						aBone.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aBone.theName);
						if (string.IsNullOrEmpty(aBone.theName))
						{
							aBone.theName = "unnamed_bone_" + boneIndex.ToString("000");
						}
						aBone.parentBoneIndex = this.theInputFileReader.ReadInt32();
						aBone.position.x = this.theInputFileReader.ReadSingle();
						aBone.position.y = this.theInputFileReader.ReadSingle();
						aBone.position.z = this.theInputFileReader.ReadSingle();
						aBone.rotation.x = this.theInputFileReader.ReadSingle();
						aBone.rotation.y = this.theInputFileReader.ReadSingle();
						aBone.rotation.z = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theBones.Add(aBone);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones " + this.theMdlFileData.theBones.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadBoneControllers()
		{
			if (this.theMdlFileData.boneControllerCount > 0)
			{
				//Dim boneControllerInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theBoneControllers = new List<SourceMdlBoneController06>(this.theMdlFileData.boneControllerCount);
					for (int boneControllerIndex = 0; boneControllerIndex < this.theMdlFileData.boneControllerCount; boneControllerIndex++)
					{
						//boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlBoneController06 aBoneController = new SourceMdlBoneController06();

						aBoneController.boneIndex = this.theInputFileReader.ReadInt32();
						aBoneController.type = this.theInputFileReader.ReadInt32();
						aBoneController.startAngleDegrees = this.theInputFileReader.ReadSingle();
						aBoneController.endAngleDegrees = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theBoneControllers.Add(aBoneController);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + this.theMdlFileData.theBoneControllers.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSequences()
		{
			if (this.theMdlFileData.sequenceCount > 0)
			{
				//Dim sequenceInputFileStreamPosition As Long
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.sequenceOffset, SeekOrigin.Begin);
					this.theMdlFileData.theSequences = new List<SourceMdlSequenceDesc06>(this.theMdlFileData.sequenceCount);
					for (int sequenceIndex = 0; sequenceIndex < this.theMdlFileData.sequenceCount; sequenceIndex++)
					{
						SourceMdlSequenceDesc06 aSequence = new SourceMdlSequenceDesc06();

						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						aSequence.name = this.theInputFileReader.ReadChars(32);
						aSequence.theName = new string(aSequence.name);
						aSequence.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aSequence.theName);

						aSequence.fps = this.theInputFileReader.ReadSingle();

						aSequence.flags = this.theInputFileReader.ReadInt32();
						aSequence.eventCount = this.theInputFileReader.ReadInt32();
						aSequence.eventOffset = this.theInputFileReader.ReadInt32();
						aSequence.frameCount = this.theInputFileReader.ReadInt32();
						aSequence.unused01 = this.theInputFileReader.ReadInt32();
						aSequence.pivotCount = this.theInputFileReader.ReadInt32();
						aSequence.pivotOffset = this.theInputFileReader.ReadInt32();

						aSequence.motiontype = this.theInputFileReader.ReadInt32();
						aSequence.motionbone = this.theInputFileReader.ReadInt32();
						aSequence.unused02 = this.theInputFileReader.ReadInt32();
						aSequence.linearmovement.x = this.theInputFileReader.ReadSingle();
						aSequence.linearmovement.y = this.theInputFileReader.ReadSingle();
						aSequence.linearmovement.z = this.theInputFileReader.ReadSingle();

						aSequence.blendCount = this.theInputFileReader.ReadInt32();
						aSequence.animOffset = this.theInputFileReader.ReadInt32();

						for (int x = 0; x < aSequence.unused03.Length; x++)
						{
							aSequence.unused03[x] = this.theInputFileReader.ReadInt32();
						}

						this.theMdlFileData.theSequences.Add(aSequence);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadEvents(aSequence);
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.theEvents alignment")

						this.ReadPivots(aSequence);
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.thePivots alignment")

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadAnimations()
		{
			if (this.theMdlFileData.theSequences != null)
			{
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				SourceMdlSequenceDesc06 aSequence = null;
				//Dim previousFrameIndex As Integer

				try
				{
					for (int sequenceIndex = 0; sequenceIndex < this.theMdlFileData.theSequences.Count; sequenceIndex++)
					{
						aSequence = this.theMdlFileData.theSequences[sequenceIndex];

						this.theInputFileReader.BaseStream.Seek(aSequence.animOffset, SeekOrigin.Begin);
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						aSequence.theAnimations = new List<SourceMdlAnimation06>(aSequence.frameCount * this.theMdlFileData.theBones.Count);
						//For frameIndex As Integer = 0 To aSequence.frameCount - 1
						//While True
						for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
						{
							fileOffsetStart = this.theInputFileReader.BaseStream.Position;
							SourceMdlAnimation06 anAnimation = new SourceMdlAnimation06();

							anAnimation.bonePositionCount = this.theInputFileReader.ReadInt32();
							anAnimation.bonePositionOffset = this.theInputFileReader.ReadInt32();
							anAnimation.boneRotationCount = this.theInputFileReader.ReadInt32();
							anAnimation.boneRotationOffset = this.theInputFileReader.ReadInt32();

							aSequence.theAnimations.Add(anAnimation);

							fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation");

							inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

							anAnimation.theBonePositionsAndRotations = new List<SourceBonePostionAndRotation06>(aSequence.frameCount);
							for (int frameIndex = 0; frameIndex < aSequence.frameCount; frameIndex++)
							{
								SourceBonePostionAndRotation06 bonePositionAndRotation = new SourceBonePostionAndRotation06();
								anAnimation.theBonePositionsAndRotations.Add(bonePositionAndRotation);
							}
							this.ReadAnimationBonePositions(anAnimation);
							this.ReadAnimationBoneRotations(anAnimation);

							this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

							//If anAnimation.theBonePositions.Count > 0 AndAlso anAnimation.theBonePositions(boneIndex).frameIndex < previousFrameIndex Then

							//End If
						}
						//Next
						//End While
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadBodyParts()
		{
			if (this.theMdlFileData.bodyPartCount > 0)
			{
				long bodyPartInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				long modelsEndInputFileStreamPosition = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.bodyPartOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					modelsEndInputFileStreamPosition = 0;

					this.theMdlFileData.theBodyParts = new List<SourceMdlBodyPart06>(this.theMdlFileData.bodyPartCount);
					for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.bodyPartCount; bodyPartIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						bodyPartInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart06 aBodyPart = new SourceMdlBodyPart06();

						aBodyPart.name = this.theInputFileReader.ReadChars(64);
						aBodyPart.theName = (new string(aBodyPart.name)).Trim('\0');
						aBodyPart.modelCount = this.theInputFileReader.ReadInt32();
						aBodyPart.@base = this.theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theBodyParts.Add(aBodyPart);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadModels(aBodyPart);
						//If bodyPartIndex = Me.theMdlFileData.bodyPartCount - 1 Then
						//	modelsEndInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//End If

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart [" + aBodyPart.theName + "]");
					}

					//If modelsEndInputFileStreamPosition > 0 Then
					//	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//	Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, modelsEndInputFileStreamPosition - 1, 4, "theMdlFileData.theBodyParts - End of Models alignment")

					//	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					//End If

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadTextures()
		{
			if (this.theMdlFileData.textureCount > 0)
			{
				//Dim boneInputFileStreamPosition As Long
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.textureOffset, SeekOrigin.Begin);

					this.theMdlFileData.theTextures = new List<SourceMdlTexture06>(this.theMdlFileData.textureCount);
					for (int textureIndex = 0; textureIndex < this.theMdlFileData.textureCount; textureIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlTexture06 aTexture = new SourceMdlTexture06();

						aTexture.fileName = this.theInputFileReader.ReadChars(64);
						aTexture.theFileName = (new string(aTexture.fileName)).Trim('\0');
						aTexture.flags = this.theInputFileReader.ReadInt32();
						aTexture.width = this.theInputFileReader.ReadUInt32();
						aTexture.height = this.theInputFileReader.ReadUInt32();
						aTexture.dataOffset = this.theInputFileReader.ReadUInt32();

						this.theMdlFileData.theTextures.Add(aTexture);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture [" + aTexture.theFileName + "]");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadTextureData(aTexture);
						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aTexture.theData alignment")

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSkins()
		{
			if (this.theMdlFileData.skinFamilyCount > 0 && this.theMdlFileData.skinReferenceCount > 0)
			{
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				short aSkinRef = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.skinOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theSkins = new List<List<short>>(this.theMdlFileData.skinFamilyCount);
					for (int skinFamilyIndex = 0; skinFamilyIndex < this.theMdlFileData.skinFamilyCount; skinFamilyIndex++)
					{
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						List<short> aSkinFamily = new List<short>();

						for (int skinRefIndex = 0; skinRefIndex < this.theMdlFileData.skinReferenceCount; skinRefIndex++)
						{
							aSkinRef = this.theInputFileReader.ReadInt16();
							aSkinFamily.Add(aSkinRef);
						}

						this.theMdlFileData.theSkins.Add(aSkinFamily);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkins " + this.theMdlFileData.theSkins.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkins alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadUnreadBytes()
		{
			this.theMdlFileData.theFileSeekLog.LogUnreadBytes(this.theInputFileReader);
		}

		// The bone positions and rotations are all zeroes, so get them from the first sequence's first frame.
		public void GetBoneDataFromFirstSequenceFirstFrame()
		{
			SourceMdlSequenceDesc06 aSequence = null;
			SourceMdlAnimation06 anAnimation = null;
			SourceMdlBone06 aBone = null;
			aSequence = this.theMdlFileData.theSequences[0];

			for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
			{
				aBone = this.theMdlFileData.theBones[boneIndex];
				anAnimation = aSequence.theAnimations[boneIndex];

				aBone.position.x = anAnimation.theBonePositionsAndRotations[0].position.x;
				aBone.position.y = anAnimation.theBonePositionsAndRotations[0].position.y;
				aBone.position.z = anAnimation.theBonePositionsAndRotations[0].position.z;
				aBone.rotation.x = anAnimation.theBonePositionsAndRotations[0].rotation.x;
				aBone.rotation.y = anAnimation.theBonePositionsAndRotations[0].rotation.y;
				aBone.rotation.z = anAnimation.theBonePositionsAndRotations[0].rotation.z;
			}
		}

		public void BuildBoneTransforms()
		{
			this.theMdlFileData.theBoneTransforms = new List<SourceBoneTransform06>(this.theMdlFileData.theBones.Count);
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
			SourceMdlBone06 aBone = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
			int parentBoneIndex = 0;
			for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
			{
	//			Dim aBone As SourceMdlBone06
				SourceBoneTransform06 boneTransform = new SourceBoneTransform06();
	//			Dim parentBoneIndex As Integer

				aBone = this.theMdlFileData.theBones[boneIndex];

				SourceVector boneMatrixColumn0 = new SourceVector();
				SourceVector boneMatrixColumn1 = new SourceVector();
				SourceVector boneMatrixColumn2 = new SourceVector();
				SourceVector boneMatrixColumn3 = new SourceVector();
				//MathModule.AngleMatrix(aBone.rotation.x, aBone.rotation.y, aBone.rotation.z, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
				//MathModule.AngleMatrix(aBone.rotation.z, aBone.rotation.x, aBone.rotation.y, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
				MathModule.AngleMatrix(aBone.rotation.y, aBone.rotation.z, aBone.rotation.x, ref boneMatrixColumn0, ref boneMatrixColumn1, ref boneMatrixColumn2, ref boneMatrixColumn3);

				boneMatrixColumn3.x = aBone.position.x;
				boneMatrixColumn3.y = aBone.position.y;
				boneMatrixColumn3.z = aBone.position.z;

				parentBoneIndex = this.theMdlFileData.theBones[boneIndex].parentBoneIndex;
				if (parentBoneIndex == -1)
				{
					boneTransform.matrixColumn0.x = boneMatrixColumn0.x;
					boneTransform.matrixColumn0.y = boneMatrixColumn0.y;
					boneTransform.matrixColumn0.z = boneMatrixColumn0.z;
					boneTransform.matrixColumn1.x = boneMatrixColumn1.x;
					boneTransform.matrixColumn1.y = boneMatrixColumn1.y;
					boneTransform.matrixColumn1.z = boneMatrixColumn1.z;
					boneTransform.matrixColumn2.x = boneMatrixColumn2.x;
					boneTransform.matrixColumn2.y = boneMatrixColumn2.y;
					boneTransform.matrixColumn2.z = boneMatrixColumn2.z;
					boneTransform.matrixColumn3.x = boneMatrixColumn3.x;
					boneTransform.matrixColumn3.y = boneMatrixColumn3.y;
					boneTransform.matrixColumn3.z = boneMatrixColumn3.z;
				}
				else
				{
					SourceBoneTransform06 parentBoneTransform = this.theMdlFileData.theBoneTransforms[parentBoneIndex];

					//			R_ConcatTransforms( g_bonetransform[pbones[i].parent], bonematrix, g_bonetransform[i] );
					MathModule.R_ConcatTransforms(parentBoneTransform.matrixColumn0, parentBoneTransform.matrixColumn1, parentBoneTransform.matrixColumn2, parentBoneTransform.matrixColumn3, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3, ref boneTransform.matrixColumn0, ref boneTransform.matrixColumn1, ref boneTransform.matrixColumn2, ref boneTransform.matrixColumn3);
				}

				this.theMdlFileData.theBoneTransforms.Add(boneTransform);
			}
		}

		public void WriteInternalMdlFileName(string internalMdlFileName)
		{
			this.theOutputFileWriter.BaseStream.Seek(0x8, SeekOrigin.Begin);
			//TODO: Should only write up to 64 characters.
			this.theOutputFileWriter.Write(internalMdlFileName.ToCharArray());
			//NOTE: Write the ending null byte.
			this.theOutputFileWriter.Write(Convert.ToByte(0));
		}

#endregion

#region Private Methods

		private void ReadEvents(SourceMdlSequenceDesc06 aSequence)
		{
			if (aSequence.eventCount > 0)
			{
				//Dim sequenceInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					//If Me.theInputFileReader.BaseStream.Position <> aSequence.eventOffset Then
					//	Dim offsetIsNotRight As Boolean = True
					//End If

					this.theInputFileReader.BaseStream.Seek(aSequence.eventOffset, SeekOrigin.Begin);
					aSequence.theEvents = new List<SourceMdlEvent06>(aSequence.eventCount);
					for (int eventIndex = 0; eventIndex < aSequence.eventCount; eventIndex++)
					{
						//sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlEvent06 anEvent = new SourceMdlEvent06();

						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						anEvent.frameIndex = this.theInputFileReader.ReadInt16();
						anEvent.eventType = this.theInputFileReader.ReadInt16();

						aSequence.theEvents.Add(anEvent);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anEvent");
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadPivots(SourceMdlSequenceDesc06 aSequence)
		{
			if (aSequence.pivotCount > 0)
			{
				//Dim sequenceInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					//If Me.theInputFileReader.BaseStream.Position <> aSequence.pivotOffset Then
					//	Dim offsetIsNotRight As Boolean = True
					//End If

					this.theInputFileReader.BaseStream.Seek(aSequence.pivotOffset, SeekOrigin.Begin);
					aSequence.thePivots = new List<SourceMdlPivot06>(aSequence.pivotCount);
					for (int pivotIndex = 0; pivotIndex < aSequence.pivotCount; pivotIndex++)
					{
						//sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlPivot06 aPivot = new SourceMdlPivot06();

						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						aPivot.point.x = this.theInputFileReader.ReadSingle();
						aPivot.point.y = this.theInputFileReader.ReadSingle();
						aPivot.point.z = this.theInputFileReader.ReadSingle();
						aPivot.pivotStart = this.theInputFileReader.ReadInt32();
						aPivot.pivotEnd = this.theInputFileReader.ReadInt32();

						aSequence.thePivots.Add(aPivot);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aPivot");
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModels(SourceMdlBodyPart06 aBodyPart)
		{
			//Dim modelInputFileStreamPosition As Long
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				this.theInputFileReader.BaseStream.Seek(aBodyPart.modelOffset, SeekOrigin.Begin);

				aBodyPart.theModels = new List<SourceMdlModel06>(aBodyPart.modelCount);
				for (int bodyPartIndex = 0; bodyPartIndex < aBodyPart.modelCount; bodyPartIndex++)
				{
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;
					//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlModel06 aModel = new SourceMdlModel06();

					aModel.name = this.theInputFileReader.ReadChars(64);
					aModel.theName = (new string(aModel.name)).Trim('\0');
					aModel.type = this.theInputFileReader.ReadInt32();

					aModel.unknown01 = this.theInputFileReader.ReadInt32();
					aModel.unused01 = this.theInputFileReader.ReadInt32();

					aModel.meshCount = this.theInputFileReader.ReadInt32();
					aModel.meshOffset = this.theInputFileReader.ReadInt32();

					aModel.vertexCount = this.theInputFileReader.ReadInt32();
					aModel.vertexBoneInfoOffset = this.theInputFileReader.ReadInt32();

					aModel.normalCount = this.theInputFileReader.ReadInt32();
					aModel.normalBoneInfoOffset = this.theInputFileReader.ReadInt32();

					aModel.unused02 = this.theInputFileReader.ReadInt32();
					aModel.modelDataOffset = this.theInputFileReader.ReadInt32();

					aBodyPart.theModels.Add(aModel);

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]");

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					this.ReadModelVertexBoneInfos(aModel);
					this.ReadModelNormalBoneInfos(aModel);
					this.ReadModelData(aModel);
					this.ReadMeshes(aModel);

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadModelVertexBoneInfos(SourceMdlModel06 aModel)
		{
			if (aModel.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					int vertexBoneInfo = 0;
					this.theInputFileReader.BaseStream.Seek(aModel.vertexBoneInfoOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theVertexBoneInfos = new List<int>(aModel.vertexCount);
					for (int vertexBoneInfoIndex = 0; vertexBoneInfoIndex < aModel.vertexCount; vertexBoneInfoIndex++)
					{
						vertexBoneInfo = this.theInputFileReader.ReadByte();
						aModel.theVertexBoneInfos.Add(vertexBoneInfo);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexBoneInfos " + aModel.theVertexBoneInfos.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexBoneInfos alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModelNormalBoneInfos(SourceMdlModel06 aModel)
		{
			if (aModel.normalCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					int normalBoneInfo = 0;
					this.theInputFileReader.BaseStream.Seek(aModel.normalBoneInfoOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theNormalBoneInfos = new List<int>(aModel.normalCount);
					for (int normalBoneInfoIndex = 0; normalBoneInfoIndex < aModel.normalCount; normalBoneInfoIndex++)
					{
						normalBoneInfo = this.theInputFileReader.ReadByte();
						aModel.theNormalBoneInfos.Add(normalBoneInfo);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormalBoneInfos " + aModel.theNormalBoneInfos.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aModel.theNormalBoneInfos alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModelData(SourceMdlModel06 aModel)
		{
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				this.theInputFileReader.BaseStream.Seek(aModel.modelDataOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				//NOTE: No idea why there are sequenceCount of model data. The first one seems to be the only one used.
				for (int sequenceIndex = 0; sequenceIndex < this.theMdlFileData.sequenceCount; sequenceIndex++)
				{
					SourceMdlModelData06 aModelData = new SourceMdlModelData06();

					aModelData.unknown01 = this.theInputFileReader.ReadInt32();
					aModelData.unknown02 = this.theInputFileReader.ReadInt32();
					aModelData.unknown03 = this.theInputFileReader.ReadInt32();
					aModelData.vertexCount = this.theInputFileReader.ReadInt32();
					aModelData.vertexOffset = this.theInputFileReader.ReadInt32();
					aModelData.normalCount = this.theInputFileReader.ReadInt32();
					aModelData.normalOffset = this.theInputFileReader.ReadInt32();

					aModel.theModelDatas.Add(aModelData);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theModelDatas " + aModel.theModelDatas.Count.ToString());

				//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theModelDatas alignment")

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				this.ReadModelVertexes(aModel.theModelDatas[0]);
				this.ReadModelNormals(aModel.theModelDatas[0]);

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadModelVertexes(SourceMdlModelData06 aModelData)
		{
			if (aModelData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(aModelData.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModelData.theVertexes = new List<SourceVector>(aModelData.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModelData.vertexCount; vertexIndex++)
					{
						SourceVector vertex = new SourceVector();
						vertex.x = this.theInputFileReader.ReadSingle();
						vertex.y = this.theInputFileReader.ReadSingle();
						vertex.z = this.theInputFileReader.ReadSingle();
						aModelData.theVertexes.Add(vertex);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModelData.theVertexes.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModelNormals(SourceMdlModelData06 aModelData)
		{
			if (aModelData.normalCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(aModelData.normalOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModelData.theNormals = new List<SourceVector>(aModelData.normalCount);
					for (int normalIndex = 0; normalIndex < aModelData.normalCount; normalIndex++)
					{
						SourceVector normal = new SourceVector();
						normal.x = this.theInputFileReader.ReadSingle();
						normal.y = this.theInputFileReader.ReadSingle();
						normal.z = this.theInputFileReader.ReadSingle();
						aModelData.theNormals.Add(normal);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormals " + aModelData.theNormals.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theNormals alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMeshes(SourceMdlModel06 aModel)
		{
			if (aModel.meshCount > 0)
			{
				//Dim meshInputFileStreamPosition As Long
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(aModel.meshOffset, SeekOrigin.Begin);

					aModel.theMeshes = new List<SourceMdlMesh06>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlMesh06 aMesh = new SourceMdlMesh06();

						aMesh.faceCount = this.theInputFileReader.ReadInt32();
						aMesh.faceOffset = this.theInputFileReader.ReadInt32();
						aMesh.skinref = this.theInputFileReader.ReadInt32();
						aMesh.normalCount = this.theInputFileReader.ReadInt32();
						aMesh.normalOffset = this.theInputFileReader.ReadInt32();

						aModel.theMeshes.Add(aMesh);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadFaces(aMesh);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadFaces(SourceMdlMesh06 aMesh)
		{
			if (aMesh.faceCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(aMesh.faceOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aMesh.theVertexAndNormalIndexes = new List<SourceMdlTriangleVertex06>();
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of aMesh.faceCount * 3 for every iteration:
					int tempVar = aMesh.faceCount * 3;
					for (int faceIndex = 0; faceIndex < tempVar; faceIndex++)
					{
						SourceMdlTriangleVertex06 vertexAndNormalIndexInfo = new SourceMdlTriangleVertex06();

						vertexAndNormalIndexInfo.vertexIndex = this.theInputFileReader.ReadUInt16();
						vertexAndNormalIndexInfo.normalIndex = this.theInputFileReader.ReadUInt16();
						vertexAndNormalIndexInfo.s = this.theInputFileReader.ReadInt16();
						vertexAndNormalIndexInfo.t = this.theInputFileReader.ReadInt16();

						aMesh.theVertexAndNormalIndexes.Add(vertexAndNormalIndexInfo);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theVertexAndNormalIndexes " + aMesh.theVertexAndNormalIndexes.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aMesh.theVertexAndNormalIndexes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadTextureData(SourceMdlTexture06 aTexture)
		{
			//Dim boneInputFileStreamPosition As Long
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				this.theInputFileReader.BaseStream.Seek(aTexture.dataOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aTexture.theData = new List<byte>((int)(aTexture.width * aTexture.height));
				//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
				//      Void ResizeTexture(s_texture_t * ptexture)
				//          ptexture->size = ptexture->skinwidth * ptexture->skinheight + 256 * 3;
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of aTexture.width * aTexture.height + 256 * 3 for every iteration:
				long tempVar = aTexture.width * aTexture.height + 256 * 3;
				for (long byteIndex = 0; byteIndex < tempVar; byteIndex++)
				{
					//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					byte data = this.theInputFileReader.ReadByte();


					aTexture.theData.Add(data);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture.theData");
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadAnimationBonePositions(SourceMdlAnimation06 anAnimation)
		{
			if (anAnimation.bonePositionCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				int startFrameIndex = 0;
				int stopFrameIndex = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(anAnimation.bonePositionOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					anAnimation.theRawBonePositions = new List<SourceMdlBonePosition06>(anAnimation.bonePositionCount);
					for (int bonePositionIndex = 0; bonePositionIndex < anAnimation.bonePositionCount; bonePositionIndex++)
					{
						SourceMdlBonePosition06 bonePosition = new SourceMdlBonePosition06();

						bonePosition.frameIndex = this.theInputFileReader.ReadInt32();
						bonePosition.position.x = this.theInputFileReader.ReadSingle();
						bonePosition.position.y = this.theInputFileReader.ReadSingle();
						bonePosition.position.z = this.theInputFileReader.ReadSingle();

						anAnimation.theRawBonePositions.Add(bonePosition);
					}

					//NOTE: Set up list indexed by frame for easier writing of animation SMD files.
					startFrameIndex = 0;
					for (int bonePositionIndex = 0; bonePositionIndex < anAnimation.theRawBonePositions.Count; bonePositionIndex++)
					{
						SourceMdlBonePosition06 bonePosition = new SourceMdlBonePosition06();
						bonePosition = anAnimation.theRawBonePositions[bonePositionIndex];

						if (bonePositionIndex == anAnimation.theRawBonePositions.Count - 1)
						{
							stopFrameIndex = anAnimation.theBonePositionsAndRotations.Count - 1;
						}
						else
						{
							stopFrameIndex = anAnimation.theRawBonePositions[bonePositionIndex + 1].frameIndex - 1;
						}
						for (int frameIndex = startFrameIndex; frameIndex <= stopFrameIndex; frameIndex++)
						{
							SourceBonePostionAndRotation06 bonePositionAndRotation = new SourceBonePostionAndRotation06();
							bonePositionAndRotation = anAnimation.theBonePositionsAndRotations[frameIndex];

							bonePositionAndRotation.position.x = bonePosition.position.x;
							bonePositionAndRotation.position.y = bonePosition.position.y;
							bonePositionAndRotation.position.z = bonePosition.position.z;
						}
						startFrameIndex = stopFrameIndex + 1;
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation.theBonePositions " + anAnimation.theRawBonePositions.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimation.theBonePositions alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadAnimationBoneRotations(SourceMdlAnimation06 anAnimation)
		{
			if (anAnimation.boneRotationCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				int startFrameIndex = 0;
				int stopFrameIndex = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(anAnimation.boneRotationOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					anAnimation.theRawBoneRotations = new List<SourceMdlBoneRotation06>(anAnimation.boneRotationCount);
					for (int boneRotationIndex = 0; boneRotationIndex < anAnimation.boneRotationCount; boneRotationIndex++)
					{
						SourceMdlBoneRotation06 boneRotation = new SourceMdlBoneRotation06();

						boneRotation.frameIndex = this.theInputFileReader.ReadInt16();
						boneRotation.angle[0] = this.theInputFileReader.ReadInt16();
						boneRotation.angle[1] = this.theInputFileReader.ReadInt16();
						boneRotation.angle[2] = this.theInputFileReader.ReadInt16();

						anAnimation.theRawBoneRotations.Add(boneRotation);
					}

					//NOTE: Set up list indexed by frame for easier writing of animation SMD files.
					startFrameIndex = 0;
					for (int boneRotationIndex = 0; boneRotationIndex < anAnimation.theRawBoneRotations.Count; boneRotationIndex++)
					{
						SourceMdlBoneRotation06 boneRotation = new SourceMdlBoneRotation06();
						boneRotation = anAnimation.theRawBoneRotations[boneRotationIndex];

						if (boneRotationIndex == anAnimation.theRawBoneRotations.Count - 1)
						{
							stopFrameIndex = anAnimation.theBonePositionsAndRotations.Count - 1;
						}
						else
						{
							stopFrameIndex = anAnimation.theRawBoneRotations[boneRotationIndex + 1].frameIndex - 1;
						}
						for (int frameIndex = startFrameIndex; frameIndex <= stopFrameIndex; frameIndex++)
						{
							SourceBonePostionAndRotation06 bonePositionAndRotation = new SourceBonePostionAndRotation06();
							bonePositionAndRotation = anAnimation.theBonePositionsAndRotations[frameIndex];

							//#define STUDIO_TO_RAD		(Q_PI_F/18000.0f)
							bonePositionAndRotation.rotation.x = boneRotation.angle[0] * (Math.PI / 18000);
							bonePositionAndRotation.rotation.y = boneRotation.angle[1] * (Math.PI / 18000);
							bonePositionAndRotation.rotation.z = boneRotation.angle[2] * (Math.PI / 18000);
						}
						startFrameIndex = stopFrameIndex + 1;
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation.theBoneRotations " + anAnimation.theRawBoneRotations.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimation.theBoneRotations alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

#endregion

#region Data

		protected BinaryReader theInputFileReader;
		protected BinaryWriter theOutputFileWriter;

		protected SourceMdlFileData06 theMdlFileData;

#endregion

	}

}