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
			this.theInputFileReader = mdlFileReader;
			this.theMdlFileData = mdlFileData;

			this.theMdlFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile04(BinaryWriter mdlFileWriter, SourceMdlFileData04 mdlFileData)
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

			this.theMdlFileData.theActualFileSize = this.theInputFileReader.BaseStream.Length;

			this.theMdlFileData.unknown01 = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.boneCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.bodyPartCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknownCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.sequenceDescCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.sequenceFrameCount = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.unknown02 = this.theInputFileReader.ReadInt32();

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header");
		}

		public void ReadBones()
		{
			if (this.theMdlFileData.boneCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theBones = new List<SourceMdlBone04>(this.theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < this.theMdlFileData.boneCount; boneIndex++)
					{
						SourceMdlBone04 aBone = new SourceMdlBone04();

						aBone.parentBoneIndex = this.theInputFileReader.ReadInt32();
						aBone.unknown = this.theInputFileReader.ReadInt32();
						aBone.position.x = this.theInputFileReader.ReadSingle();
						aBone.position.y = this.theInputFileReader.ReadSingle();
						aBone.position.z = this.theInputFileReader.ReadSingle();
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

						this.theMdlFileData.theBones.Add(aBone);
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

		public void ReadSequenceDescs()
		{
			if (this.theMdlFileData.sequenceDescCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theSequenceDescs = new List<SourceMdlSequenceDesc04>(this.theMdlFileData.sequenceDescCount);
					for (int sequenceDescIndex = 0; sequenceDescIndex < this.theMdlFileData.sequenceDescCount; sequenceDescIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						SourceMdlSequenceDesc04 aSequenceDesc = new SourceMdlSequenceDesc04();

						aSequenceDesc.name = this.theInputFileReader.ReadChars(aSequenceDesc.name.Length);
						aSequenceDesc.theName = new string(aSequenceDesc.name);
						aSequenceDesc.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aSequenceDesc.theName);
						aSequenceDesc.frameCount = this.theInputFileReader.ReadInt32();
						aSequenceDesc.flag = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theSequenceDescs.Add(aSequenceDesc);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequenceDesc theName = " + aSequenceDesc.theName);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceHeaders " + Me.theMdlFileData.theSequenceHeaders.Count.ToString())

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")

					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					for (int i = 0; i < this.theMdlFileData.unknownCount; i++)
					{
						int unknown = this.theInputFileReader.ReadInt32();
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "In-between Me.theMdlFileData.theSequenceDescs and aSequenceDesc.theSequences (4 bytes for each unknown in theMdlFileData.unknownCount)");

					for (int sequenceDescIndex = 0; sequenceDescIndex < this.theMdlFileData.theSequenceDescs.Count; sequenceDescIndex++)
					{
						SourceMdlSequenceDesc04 aSequenceDesc = this.theMdlFileData.theSequenceDescs[sequenceDescIndex];

						this.ReadSequences(aSequenceDesc);
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
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 8, "theMdlFileData.theBodyParts pre-alignment")

					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theBodyParts = new List<SourceMdlBodyPart04>(this.theMdlFileData.bodyPartCount);
					for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.bodyPartCount; bodyPartIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						SourceMdlBodyPart04 aBodyPart = new SourceMdlBodyPart04();

						aBodyPart.name = this.theInputFileReader.ReadChars(aBodyPart.name.Length);
						aBodyPart.theName = new string(aBodyPart.name);
						aBodyPart.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aBodyPart.theName);
						aBodyPart.unknown = this.theInputFileReader.ReadInt32();
						aBodyPart.modelCount = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theBodyParts.Add(aBodyPart);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart name = " + aBodyPart.theName);

						this.ReadModels(aBodyPart);
						this.SetFileNameForMeshes(aBodyPart);
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
			this.theMdlFileData.theFileSeekLog.LogUnreadBytes(this.theInputFileReader);
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
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						SourceMdlSequence04 aSequence = new SourceMdlSequence04();

						aSequence.sequenceFrameIndexAsSingle = this.theInputFileReader.ReadSingle();
						for (int x = 0; x < aSequence.unknown.Length; x++)
						{
							aSequence.unknown[x] = this.theInputFileReader.ReadInt32();
						}
						aSequence.unknownSingle01 = this.theInputFileReader.ReadSingle();
						aSequence.unknownSingle02 = this.theInputFileReader.ReadSingle();
						aSequence.unknownSingle03 = this.theInputFileReader.ReadSingle();
						//aSequence.positionScaleX = Me.theInputFileReader.ReadInt16()
						//aSequence.positionScaleY = Me.theInputFileReader.ReadInt16()
						//aSequence.positionScaleZ = Me.theInputFileReader.ReadInt16()
						//aSequence.rotationScaleX = Me.theInputFileReader.ReadInt16()
						//aSequence.rotationScaleY = Me.theInputFileReader.ReadInt16()
						//aSequence.rotationScaleZ = Me.theInputFileReader.ReadInt16()

						aSequenceDesc.theSequences.Add(aSequence);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence");

						this.ReadSequenceValues(aSequence);
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
			if (this.theMdlFileData.theBones.Count > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aSequence.thePositionsAndRotations = new List<SourceMdlSequenceValue04>(this.theMdlFileData.theBones.Count);
					for (int sequenceIndex = 0; sequenceIndex < this.theMdlFileData.theBones.Count; sequenceIndex++)
					{
						SourceMdlSequenceValue04 aSequenceValue = new SourceMdlSequenceValue04();

						aSequenceValue.position = new SourceVector();
						aSequenceValue.rotation = new SourceVector();
						aSequenceValue.position.x = this.theInputFileReader.ReadByte();
						aSequenceValue.position.y = this.theInputFileReader.ReadByte();
						aSequenceValue.position.z = this.theInputFileReader.ReadByte();
						aSequenceValue.rotation.x = this.theInputFileReader.ReadByte();
						aSequenceValue.rotation.y = this.theInputFileReader.ReadByte();
						aSequenceValue.rotation.z = this.theInputFileReader.ReadByte();
						//aSequenceValue.position = New SourceVector()
						//aSequenceValue.position.x = Me.theInputFileReader.ReadSingle()
						//aSequenceValue.position.y = Me.theInputFileReader.ReadSingle()
						//aSequenceValue.position.z = Me.theInputFileReader.ReadSingle()

						aSequence.thePositionsAndRotations.Add(aSequenceValue);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence.thePositionsAndRotations " + aSequence.thePositionsAndRotations.Count.ToString());

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
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						SourceMdlModel04 aModel = new SourceMdlModel04();

						aModel.unknownSingle = this.theInputFileReader.ReadSingle();
						aModel.vertexCount = this.theInputFileReader.ReadInt32();
						aModel.normalCount = this.theInputFileReader.ReadInt32();
						aModel.meshCount = this.theInputFileReader.ReadInt32();

						aBodyPart.theModels.Add(aModel);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel");

						this.ReadVertexes(aModel);
						this.ReadNormals(aModel);
						this.ReadMeshes(aModel);
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
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theNormals = new List<SourceMdlNormal04>(aModel.normalCount);
					for (int normalIndex = 0; normalIndex < aModel.normalCount; normalIndex++)
					{
						SourceMdlNormal04 aNormal = new SourceMdlNormal04();

						aNormal.index = this.theInputFileReader.ReadInt32();
						aNormal.vector.x = this.theInputFileReader.ReadSingle();
						aNormal.vector.y = this.theInputFileReader.ReadSingle();
						aNormal.vector.z = this.theInputFileReader.ReadSingle();

						aModel.theNormals.Add(aNormal);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormals " + aModel.theNormals.Count.ToString());

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
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theVertexes = new List<SourceMdlVertex04>(aModel.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModel.vertexCount; vertexIndex++)
					{
						SourceMdlVertex04 aVertex = new SourceMdlVertex04();

						aVertex.boneIndex = this.theInputFileReader.ReadInt32();
						aVertex.vector.x = this.theInputFileReader.ReadSingle();
						aVertex.vector.y = this.theInputFileReader.ReadSingle();
						aVertex.vector.z = this.theInputFileReader.ReadSingle();

						aModel.theVertexes.Add(aVertex);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModel.theVertexes.Count.ToString());

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
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theMeshes = new List<SourceMdlMesh04>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						SourceMdlMesh04 aMesh = new SourceMdlMesh04();

						aMesh.name = this.theInputFileReader.ReadChars(aMesh.name.Length);
						aMesh.theName = new string(aMesh.name);
						aMesh.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aMesh.theName);
						aMesh.faceCount = this.theInputFileReader.ReadInt32();
						aMesh.unknownCount = this.theInputFileReader.ReadInt32();
						aMesh.textureWidth = this.theInputFileReader.ReadUInt32();
						aMesh.textureHeight = this.theInputFileReader.ReadUInt32();

						aModel.theMeshes.Add(aMesh);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh name = " + aMesh.theName);

						this.ReadFaces(aMesh);
						this.ReadTextureBmpData(aMesh);
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
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aMesh.theFaces = new List<SourceMdlFace04>(aMesh.faceCount);
					for (int faceIndex = 0; faceIndex < aMesh.faceCount; faceIndex++)
					{
						SourceMdlFace04 aFace = new SourceMdlFace04();

						//For x As Integer = 0 To aFace.vertexIndex.Length - 1
						//	aFace.vertexIndex(x) = Me.theInputFileReader.ReadInt32()
						//Next
						for (int x = 0; x < aFace.vertexInfo.Length; x++)
						{
							aFace.vertexInfo[x].vertexIndex = this.theInputFileReader.ReadInt32();
							aFace.vertexInfo[x].normalIndex = this.theInputFileReader.ReadInt32();
							aFace.vertexInfo[x].s = this.theInputFileReader.ReadInt32();
							aFace.vertexInfo[x].t = this.theInputFileReader.ReadInt32();
						}

						aMesh.theFaces.Add(aFace);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFaces " + aMesh.theFaces.Count.ToString());

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
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aMesh.theTextureBmpData = new List<byte>((int)(aMesh.textureWidth * aMesh.textureHeight));
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of aMesh.textureWidth * aMesh.textureHeight + 256 * 3 for every iteration:
				long tempVar = aMesh.textureWidth * aMesh.textureHeight + 256 * 3;
				for (long byteIndex = 0; byteIndex < tempVar; byteIndex++)
				{
					byte data = this.theInputFileReader.ReadByte();


					aMesh.theTextureBmpData.Add(data);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theTextureBmpData");
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

			for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.theBodyParts.Count; bodyPartIndex++)
			{
				aBodyPart = this.theMdlFileData.theBodyParts[bodyPartIndex];
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