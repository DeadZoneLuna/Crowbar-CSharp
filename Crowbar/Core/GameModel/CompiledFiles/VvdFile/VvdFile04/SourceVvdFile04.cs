//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Text;

namespace Crowbar
{
	public class SourceVvdFile04
	{

#region Creation and Destruction

		public SourceVvdFile04(BinaryReader vvdFileReader, SourceVvdFileData04 vvdFileData, long vvdFileOffsetStart = 0)
		{
			theInputFileReader = vvdFileReader;
			theVvdFileOffsetStart = vvdFileOffsetStart;
			theVvdFileData = vvdFileData;

			theVvdFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

#endregion

#region Methods

		public void ReadSourceVvdHeader()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theVvdFileData.id = theInputFileReader.ReadChars(4);
			theVvdFileData.version = theInputFileReader.ReadInt32();
			theVvdFileData.checksum = theInputFileReader.ReadInt32();
			theVvdFileData.lodCount = theInputFileReader.ReadInt32();
			for (int i = 0; i < SourceConstants.MAX_NUM_LODS; i++)
			{
				theVvdFileData.lodVertexCount[i] = theInputFileReader.ReadInt32();
			}
			theVvdFileData.fixupCount = theInputFileReader.ReadInt32();
			theVvdFileData.fixupTableOffset = theInputFileReader.ReadInt32();
			theVvdFileData.vertexDataOffset = theInputFileReader.ReadInt32();
			theVvdFileData.tangentDataOffset = theInputFileReader.ReadInt32();

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VVD File Header");
		}

		public void ReadVertexes(int mdlVersion = 0)
		{
			if (theVvdFileData.lodCount <= 0)
			{
				return;
			}

			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			theInputFileReader.BaseStream.Seek(theVvdFileOffsetStart + theVvdFileData.vertexDataOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			//Dim boneWeightingIsIncorrect As Boolean
			float weight = 0;
			byte boneIndex = 0;

			int vertexCount = theVvdFileData.lodVertexCount[0];
			theVvdFileData.theVertexes = new List<SourceVertex>(vertexCount);
			for (int j = 0; j < vertexCount; j++)
			{
				SourceVertex aStudioVertex = new SourceVertex();

				SourceBoneWeight boneWeight = new SourceBoneWeight();
				//boneWeightingIsIncorrect = False
				for (int x = 0; x < SourceConstants.MAX_NUM_BONES_PER_VERT; x++)
				{
					weight = theInputFileReader.ReadSingle();
					boneWeight.weight[x] = weight;
					//If weight > 1 Then
					//	boneWeightingIsIncorrect = True
					//End If
				}
				for (int x = 0; x < SourceConstants.MAX_NUM_BONES_PER_VERT; x++)
				{
					boneIndex = theInputFileReader.ReadByte();
					boneWeight.bone[x] = boneIndex;
					//If boneIndex > 127 Then
					//	boneWeightingIsIncorrect = True
					//End If
				}
				boneWeight.boneCount = theInputFileReader.ReadByte();
				//'TODO: ReadVertexes() -- boneWeight.boneCount > MAX_NUM_BONES_PER_VERT, which seems like incorrect vvd format 
				//If boneWeight.boneCount > MAX_NUM_BONES_PER_VERT Then
				//	boneWeight.boneCount = CByte(MAX_NUM_BONES_PER_VERT)
				//End If
				//If boneWeightingIsIncorrect Then
				//	boneWeight.boneCount = 0
				//End If
				aStudioVertex.boneWeight = boneWeight;

				aStudioVertex.positionX = theInputFileReader.ReadSingle();
				aStudioVertex.positionY = theInputFileReader.ReadSingle();
				aStudioVertex.positionZ = theInputFileReader.ReadSingle();
				aStudioVertex.normalX = theInputFileReader.ReadSingle();
				aStudioVertex.normalY = theInputFileReader.ReadSingle();
				aStudioVertex.normalZ = theInputFileReader.ReadSingle();
				aStudioVertex.texCoordX = theInputFileReader.ReadSingle();
				aStudioVertex.texCoordY = theInputFileReader.ReadSingle();
				if (mdlVersion >= 54 && mdlVersion <= 59)
				{
					theInputFileReader.ReadSingle();
					theInputFileReader.ReadSingle();
					theInputFileReader.ReadSingle();
					theInputFileReader.ReadSingle();
				}
				theVvdFileData.theVertexes.Add(aStudioVertex);
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVertexes " + theVvdFileData.theVertexes.Count.ToString());
		}

		public void ReadFixups()
		{
			if (theVvdFileData.fixupCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				theInputFileReader.BaseStream.Seek(theVvdFileOffsetStart + theVvdFileData.fixupTableOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theVvdFileData.theFixups = new List<SourceVvdFixup04>(theVvdFileData.fixupCount);
				for (int fixupIndex = 0; fixupIndex < theVvdFileData.fixupCount; fixupIndex++)
				{
					SourceVvdFixup04 aFixup = new SourceVvdFixup04();

					aFixup.lodIndex = theInputFileReader.ReadInt32();
					aFixup.vertexIndex = theInputFileReader.ReadInt32();
					aFixup.vertexCount = theInputFileReader.ReadInt32();
					theVvdFileData.theFixups.Add(aFixup);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theFixups " + theVvdFileData.theFixups.Count.ToString());

				if (theVvdFileData.lodCount > 0)
				{
					theInputFileReader.BaseStream.Seek(theVvdFileOffsetStart + theVvdFileData.vertexDataOffset, SeekOrigin.Begin);

					for (int lodIndex = 0; lodIndex < theVvdFileData.lodCount; lodIndex++)
					{
						SetupFixedVertexes(lodIndex);
					}
				}
			}
		}

		public void ReadUnreadBytes()
		{
			theVvdFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

#endregion

#region Private Methods

		private void SetupFixedVertexes(int lodIndex)
		{
			SourceVvdFixup04 aFixup = null;
			SourceVertex aStudioVertex = null;

			try
			{
				theVvdFileData.theFixedVertexesByLod[lodIndex] = new List<SourceVertex>();
				for (int fixupIndex = 0; fixupIndex < theVvdFileData.theFixups.Count; fixupIndex++)
				{
					aFixup = theVvdFileData.theFixups[fixupIndex];

					if (aFixup.lodIndex >= lodIndex)
					{
						for (int j = 0; j < aFixup.vertexCount; j++)
						{
							aStudioVertex = theVvdFileData.theVertexes[aFixup.vertexIndex + j];
							theVvdFileData.theFixedVertexesByLod[lodIndex].Add(aStudioVertex);
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

#endregion

#region Data

		private BinaryReader theInputFileReader;
		private long theVvdFileOffsetStart;
		private SourceVvdFileData04 theVvdFileData;

#endregion

	}

}