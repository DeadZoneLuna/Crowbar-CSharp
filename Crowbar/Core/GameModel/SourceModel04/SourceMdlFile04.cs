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
	public class SourceMdlFile04
	{

#region Creation and Destruction

		public SourceMdlFile04(BinaryReader mdlFileReader, SourceMdlFileData04 mdlFileData)
		{
			theInputFileReader = mdlFileReader;
			theMdlFileData = mdlFileData;

			theMdlFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile04(BinaryWriter mdlFileWriter, SourceMdlFileData04 mdlFileData)
		{
			theOutputFileWriter = mdlFileWriter;
			theMdlFileData = mdlFileData;
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

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theMdlFileData.id = theInputFileReader.ReadChars(4);
			theMdlFileData.theID = new string(theMdlFileData.id);
			theMdlFileData.version = theInputFileReader.ReadInt32();

			theMdlFileData.theActualFileSize = theInputFileReader.BaseStream.Length;

			theMdlFileData.unknown01 = theInputFileReader.ReadInt32();

			theMdlFileData.boneCount = theInputFileReader.ReadInt32();
			theMdlFileData.bodyPartCount = theInputFileReader.ReadInt32();
			theMdlFileData.unknownCount = theInputFileReader.ReadInt32();
			theMdlFileData.sequenceDescCount = theInputFileReader.ReadInt32();
			theMdlFileData.sequenceFrameCount = theInputFileReader.ReadInt32();

			theMdlFileData.unknown02 = theInputFileReader.ReadInt32();

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header");
		}

		public void ReadBones()
		{
			if (theMdlFileData.boneCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theBones = new List<SourceMdlBone04>(theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < theMdlFileData.boneCount; boneIndex++)
					{
						SourceMdlBone04 aBone = new SourceMdlBone04();

						aBone.parentBoneIndex = theInputFileReader.ReadInt32();
						aBone.unknown = theInputFileReader.ReadInt32();
						aBone.position.x = theInputFileReader.ReadSingle();
						aBone.position.y = theInputFileReader.ReadSingle();
						aBone.position.z = theInputFileReader.ReadSingle();
						//If aBone.parentBoneIndex > -1 Then
						//	aBone.position.x += Me.theMdlFileData.theBones(aBone.parentBoneIndex).position.x
						//	aBone.position.y += Me.theMdlFileData.theBones(aBone.parentBoneIndex).position.y
						//	aBone.position.z += Me.theMdlFileData.theBones(aBone.parentBoneIndex).position.z
						//End If
						//If aBone.parentBoneIndex > -1 Then
						//	aBone.position.x -= Me.theMdlFileData.theBones(aBone.parentBoneIndex).position.x
						//	aBone.position.y -= Me.theMdlFileData.theBones(aBone.parentBoneIndex).position.y
						//	aBone.position.z -= Me.theMdlFileData.theBones(aBone.parentBoneIndex).position.z
						//End If
						//aBone.rotationX.the16BitValue = Me.theInputFileReader.ReadUInt16()
						//aBone.rotationY.the16BitValue = Me.theInputFileReader.ReadUInt16()
						//aBone.rotationZ.the16BitValue = Me.theInputFileReader.ReadUInt16()
						//aBone.positionX.the16BitValue = Me.theInputFileReader.ReadUInt16()
						//aBone.positionY.the16BitValue = Me.theInputFileReader.ReadUInt16()
						//aBone.positionZ.the16BitValue = Me.theInputFileReader.ReadUInt16()
						//aBone.rotationX.the16BitValue = Me.theInputFileReader.ReadUInt16()
						//aBone.rotationY.the16BitValue = Me.theInputFileReader.ReadUInt16()
						//aBone.rotationZ.the16BitValue = Me.theInputFileReader.ReadUInt16()

						theMdlFileData.theBones.Add(aBone);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones " + theMdlFileData.theBones.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSequenceDescs()
		{
			if (theMdlFileData.sequenceDescCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theSequenceDescs = new List<SourceMdlSequenceDesc04>(theMdlFileData.sequenceDescCount);
					for (int sequenceDescIndex = 0; sequenceDescIndex < theMdlFileData.sequenceDescCount; sequenceDescIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;

						SourceMdlSequenceDesc04 aSequenceDesc = new SourceMdlSequenceDesc04();

						aSequenceDesc.name = theInputFileReader.ReadChars(aSequenceDesc.name.Length);
						aSequenceDesc.theName = new string(aSequenceDesc.name);
						aSequenceDesc.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aSequenceDesc.theName);
						aSequenceDesc.frameCount = theInputFileReader.ReadInt32();
						aSequenceDesc.flag = theInputFileReader.ReadInt32();

						theMdlFileData.theSequenceDescs.Add(aSequenceDesc);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequenceDesc theName = " + aSequenceDesc.theName);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceHeaders " + Me.theMdlFileData.theSequenceHeaders.Count.ToString())

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")

					fileOffsetStart = theInputFileReader.BaseStream.Position;

					for (int i = 0; i < theMdlFileData.unknownCount; i++)
					{
						int unknown = theInputFileReader.ReadInt32();
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "In-between Me.theMdlFileData.theSequenceDescs and aSequenceDesc.theSequences (4 bytes for each unknown in theMdlFileData.unknownCount)");

					for (int sequenceDescIndex = 0; sequenceDescIndex < theMdlFileData.theSequenceDescs.Count; sequenceDescIndex++)
					{
						SourceMdlSequenceDesc04 aSequenceDesc = theMdlFileData.theSequenceDescs[sequenceDescIndex];

						ReadSequences(aSequenceDesc);
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
			if (theMdlFileData.bodyPartCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 8, "theMdlFileData.theBodyParts pre-alignment")

					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theBodyParts = new List<SourceMdlBodyPart04>(theMdlFileData.bodyPartCount);
					for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.bodyPartCount; bodyPartIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;

						SourceMdlBodyPart04 aBodyPart = new SourceMdlBodyPart04();

						aBodyPart.name = theInputFileReader.ReadChars(aBodyPart.name.Length);
						aBodyPart.theName = new string(aBodyPart.name);
						aBodyPart.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aBodyPart.theName);
						aBodyPart.unknown = theInputFileReader.ReadInt32();
						aBodyPart.modelCount = theInputFileReader.ReadInt32();

						theMdlFileData.theBodyParts.Add(aBodyPart);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart name = " + aBodyPart.theName);

						ReadModels(aBodyPart);
						SetFileNameForMeshes(aBodyPart);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts " + Me.theMdlFileData.theBodyParts.Count.ToString())

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBodyParts alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		//' The bone positions and rotations do not seem correct, so get them from the first sequence's first frame.
		//Public Sub GetBoneDataFromFirstSequenceFirstFrame()
		//	Dim aSequence As SourceMdlSequenceDesc04
		//	Dim anAnimation As SourceMdlAnimation04
		//	Dim aBone As SourceMdlBone04
		//	aSequence = Me.theMdlFileData.theSequences(0)

		//	For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
		//		aBone = Me.theMdlFileData.theBones(boneIndex)
		//		anAnimation = aSequence.theAnimations(boneIndex)

		//		aBone.position.x = anAnimation.theBonePositionsAndRotations(0).position.x
		//		aBone.position.y = anAnimation.theBonePositionsAndRotations(0).position.y
		//		aBone.position.z = anAnimation.theBonePositionsAndRotations(0).position.z
		//		aBone.rotation.x = anAnimation.theBonePositionsAndRotations(0).rotation.x
		//		aBone.rotation.y = anAnimation.theBonePositionsAndRotations(0).rotation.y
		//		aBone.rotation.z = anAnimation.theBonePositionsAndRotations(0).rotation.z
		//	Next
		//End Sub

		public void ReadUnreadBytes()
		{
			theMdlFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

#endregion

#region Private Methods

		private void ReadSequences(SourceMdlSequenceDesc04 aSequenceDesc)
		{
			if (aSequenceDesc.frameCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					aSequenceDesc.theSequences = new List<SourceMdlSequence04>(aSequenceDesc.frameCount);
					for (int sequenceIndex = 0; sequenceIndex < aSequenceDesc.frameCount; sequenceIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;

						SourceMdlSequence04 aSequence = new SourceMdlSequence04();

						aSequence.sequenceFrameIndexAsSingle = theInputFileReader.ReadSingle();
						for (int x = 0; x < aSequence.unknown.Length; x++)
						{
							aSequence.unknown[x] = theInputFileReader.ReadInt32();
						}
						aSequence.unknownSingle01 = theInputFileReader.ReadSingle();
						aSequence.unknownSingle02 = theInputFileReader.ReadSingle();
						aSequence.unknownSingle03 = theInputFileReader.ReadSingle();
						//aSequence.positionScaleX = Me.theInputFileReader.ReadInt16()
						//aSequence.positionScaleY = Me.theInputFileReader.ReadInt16()
						//aSequence.positionScaleZ = Me.theInputFileReader.ReadInt16()
						//aSequence.rotationScaleX = Me.theInputFileReader.ReadInt16()
						//aSequence.rotationScaleY = Me.theInputFileReader.ReadInt16()
						//aSequence.rotationScaleZ = Me.theInputFileReader.ReadInt16()

						aSequenceDesc.theSequences.Add(aSequence);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence");

						ReadSequenceValues(aSequence);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequenceHeader.theSequences " + aSequenceHeader.theSequences.Count.ToString())

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 8, "aSequenceHeader.theSequences and aSequence.theUnknownValues alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSequenceValues(SourceMdlSequence04 aSequence)
		{
			if (theMdlFileData.theBones.Count > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSequence.thePositionsAndRotations = new List<SourceMdlSequenceValue04>(theMdlFileData.theBones.Count);
					for (int sequenceIndex = 0; sequenceIndex < theMdlFileData.theBones.Count; sequenceIndex++)
					{
						SourceMdlSequenceValue04 aSequenceValue = new SourceMdlSequenceValue04();

						aSequenceValue.position = new SourceVector();
						aSequenceValue.rotation = new SourceVector();
						aSequenceValue.position.x = theInputFileReader.ReadByte();
						aSequenceValue.position.y = theInputFileReader.ReadByte();
						aSequenceValue.position.z = theInputFileReader.ReadByte();
						aSequenceValue.rotation.x = theInputFileReader.ReadByte();
						aSequenceValue.rotation.y = theInputFileReader.ReadByte();
						aSequenceValue.rotation.z = theInputFileReader.ReadByte();
						//aSequenceValue.position = New SourceVector()
						//aSequenceValue.position.x = Me.theInputFileReader.ReadSingle()
						//aSequenceValue.position.y = Me.theInputFileReader.ReadSingle()
						//aSequenceValue.position.z = Me.theInputFileReader.ReadSingle()

						aSequence.thePositionsAndRotations.Add(aSequenceValue);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence.thePositionsAndRotations " + aSequence.thePositionsAndRotations.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModels(SourceMdlBodyPart04 aBodyPart)
		{
			if (aBodyPart.modelCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aBodyPart.theModels = new List<SourceMdlModel04>(aBodyPart.modelCount);
					for (int modelIndex = 0; modelIndex < aBodyPart.modelCount; modelIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;

						SourceMdlModel04 aModel = new SourceMdlModel04();

						aModel.unknownSingle = theInputFileReader.ReadSingle();
						aModel.vertexCount = theInputFileReader.ReadInt32();
						aModel.normalCount = theInputFileReader.ReadInt32();
						aModel.meshCount = theInputFileReader.ReadInt32();

						aBodyPart.theModels.Add(aModel);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel");

						ReadVertexes(aModel);
						ReadNormals(aModel);
						ReadMeshes(aModel);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels " + aBodyPart.theModels.Count.ToString())

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadNormals(SourceMdlModel04 aModel)
		{
			if (aModel.normalCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theNormals = new List<SourceMdlNormal04>(aModel.normalCount);
					for (int normalIndex = 0; normalIndex < aModel.normalCount; normalIndex++)
					{
						SourceMdlNormal04 aNormal = new SourceMdlNormal04();

						aNormal.index = theInputFileReader.ReadInt32();
						aNormal.vector.x = theInputFileReader.ReadSingle();
						aNormal.vector.y = theInputFileReader.ReadSingle();
						aNormal.vector.z = theInputFileReader.ReadSingle();

						aModel.theNormals.Add(aNormal);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormals " + aModel.theNormals.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadVertexes(SourceMdlModel04 aModel)
		{
			if (aModel.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theVertexes = new List<SourceMdlVertex04>(aModel.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModel.vertexCount; vertexIndex++)
					{
						SourceMdlVertex04 aVertex = new SourceMdlVertex04();

						aVertex.boneIndex = theInputFileReader.ReadInt32();
						aVertex.vector.x = theInputFileReader.ReadSingle();
						aVertex.vector.y = theInputFileReader.ReadSingle();
						aVertex.vector.z = theInputFileReader.ReadSingle();

						aModel.theVertexes.Add(aVertex);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModel.theVertexes.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMeshes(SourceMdlModel04 aModel)
		{
			if (aModel.meshCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theMeshes = new List<SourceMdlMesh04>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;

						SourceMdlMesh04 aMesh = new SourceMdlMesh04();

						aMesh.name = theInputFileReader.ReadChars(aMesh.name.Length);
						aMesh.theName = new string(aMesh.name);
						aMesh.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aMesh.theName);
						aMesh.faceCount = theInputFileReader.ReadInt32();
						aMesh.unknownCount = theInputFileReader.ReadInt32();
						aMesh.textureWidth = theInputFileReader.ReadUInt32();
						aMesh.textureHeight = theInputFileReader.ReadUInt32();

						aModel.theMeshes.Add(aMesh);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh name = " + aMesh.theName);

						ReadFaces(aMesh);
						ReadTextureBmpData(aMesh);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes " + aModel.theMeshes.Count.ToString())

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadFaces(SourceMdlMesh04 aMesh)
		{
			if (aMesh.faceCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aMesh.theFaces = new List<SourceMdlFace04>(aMesh.faceCount);
					for (int faceIndex = 0; faceIndex < aMesh.faceCount; faceIndex++)
					{
						SourceMdlFace04 aFace = new SourceMdlFace04();

						//For x As Integer = 0 To aFace.vertexIndex.Length - 1
						//	aFace.vertexIndex(x) = Me.theInputFileReader.ReadInt32()
						//Next
						for (int x = 0; x < aFace.vertexInfo.Length; x++)
						{
							aFace.vertexInfo[x].vertexIndex = theInputFileReader.ReadInt32();
							aFace.vertexInfo[x].normalIndex = theInputFileReader.ReadInt32();
							aFace.vertexInfo[x].s = theInputFileReader.ReadInt32();
							aFace.vertexInfo[x].t = theInputFileReader.ReadInt32();
						}

						aMesh.theFaces.Add(aFace);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFaces " + aMesh.theFaces.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadTextureBmpData(SourceMdlMesh04 aMesh)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aMesh.theTextureBmpData = new List<byte>((int)(aMesh.textureWidth * aMesh.textureHeight));
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of aMesh.textureWidth * aMesh.textureHeight + 256 * 3 for every iteration:
				long tempVar = aMesh.textureWidth * aMesh.textureHeight + 256 * 3;
				for (long byteIndex = 0; byteIndex < tempVar; byteIndex++)
				{
					byte data = theInputFileReader.ReadByte();


					aMesh.theTextureBmpData.Add(data);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theTextureBmpData");
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void SetFileNameForMeshes(SourceMdlBodyPart04 aBodyPart)
		{
			SourceMdlModel04 aModel = null;
			SourceMdlMesh04 aMesh = null;
			string textureFileName = null;

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
							textureFileName = "bodypart" + bodyPartIndex.ToString() + "_model" + modelIndex.ToString() + "_mesh" + meshIndex.ToString() + ".bmp";
							aMesh.theTextureFileName = textureFileName;
						}
						catch (Exception ex)
						{
							int debug = 4242;
						}
					}
				}
			}
		}

#endregion

#region Data

		protected BinaryReader theInputFileReader;
		protected BinaryWriter theOutputFileWriter;

		protected SourceMdlFileData04 theMdlFileData;

#endregion

	}

}