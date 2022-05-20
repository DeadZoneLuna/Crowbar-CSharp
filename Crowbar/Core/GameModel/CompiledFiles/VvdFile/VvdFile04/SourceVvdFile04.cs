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
			this.theInputFileReader = vvdFileReader;
			this.theVvdFileOffsetStart = vvdFileOffsetStart;
			this.theVvdFileData = vvdFileData;

			this.theVvdFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;
		}

#endregion

#region Methods

		public void ReadSourceVvdHeader()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			this.theVvdFileData.id = this.theInputFileReader.ReadChars(4);
			this.theVvdFileData.version = this.theInputFileReader.ReadInt32();
			this.theVvdFileData.checksum = this.theInputFileReader.ReadInt32();
			this.theVvdFileData.lodCount = this.theInputFileReader.ReadInt32();
			for (int i = 0; i < SourceConstants.MAX_NUM_LODS; i++)
			{
				this.theVvdFileData.lodVertexCount[i] = this.theInputFileReader.ReadInt32();
			}
			this.theVvdFileData.fixupCount = this.theInputFileReader.ReadInt32();
			this.theVvdFileData.fixupTableOffset = this.theInputFileReader.ReadInt32();
			this.theVvdFileData.vertexDataOffset = this.theInputFileReader.ReadInt32();
			this.theVvdFileData.tangentDataOffset = this.theInputFileReader.ReadInt32();

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VVD File Header");
		}

		public void ReadVertexes(int mdlVersion = 0)
		{
			if (this.theVvdFileData.lodCount <= 0)
			{
				return;
			}

			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			this.theInputFileReader.BaseStream.Seek(this.theVvdFileOffsetStart + this.theVvdFileData.vertexDataOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			//Dim boneWeightingIsIncorrect As Boolean
			float weight = 0;
			byte boneIndex = 0;

			int vertexCount = this.theVvdFileData.lodVertexCount[0];
			this.theVvdFileData.theVertexes = new List<SourceVertex>(vertexCount);
			for (int j = 0; j < vertexCount; j++)
			{
				SourceVertex aStudioVertex = new SourceVertex();

				SourceBoneWeight boneWeight = new SourceBoneWeight();
				//boneWeightingIsIncorrect = False
				for (int x = 0; x < SourceConstants.MAX_NUM_BONES_PER_VERT; x++)
				{
					weight = this.theInputFileReader.ReadSingle();
					boneWeight.weight[x] = weight;
					//If weight > 1 Then
					//	boneWeightingIsIncorrect = True
					//End If
				}
				for (int x = 0; x < SourceConstants.MAX_NUM_BONES_PER_VERT; x++)
				{
					boneIndex = this.theInputFileReader.ReadByte();
					boneWeight.bone[x] = boneIndex;
					//If boneIndex > 127 Then
					//	boneWeightingIsIncorrect = True
					//End If
				}
				boneWeight.boneCount = this.theInputFileReader.ReadByte();
				//'TODO: ReadVertexes() -- boneWeight.boneCount > MAX_NUM_BONES_PER_VERT, which seems like incorrect vvd format 
				//If boneWeight.boneCount > MAX_NUM_BONES_PER_VERT Then
				//	boneWeight.boneCount = CByte(MAX_NUM_BONES_PER_VERT)
				//End If
				//If boneWeightingIsIncorrect Then
				//	boneWeight.boneCount = 0
				//End If
				aStudioVertex.boneWeight = boneWeight;

				aStudioVertex.positionX = this.theInputFileReader.ReadSingle();
				aStudioVertex.positionY = this.theInputFileReader.ReadSingle();
				aStudioVertex.positionZ = this.theInputFileReader.ReadSingle();
				aStudioVertex.normalX = this.theInputFileReader.ReadSingle();
				aStudioVertex.normalY = this.theInputFileReader.ReadSingle();
				aStudioVertex.normalZ = this.theInputFileReader.ReadSingle();
				aStudioVertex.texCoordX = this.theInputFileReader.ReadSingle();
				aStudioVertex.texCoordY = this.theInputFileReader.ReadSingle();
				if (mdlVersion >= 54 && mdlVersion <= 59)
				{
					this.theInputFileReader.ReadSingle();
					this.theInputFileReader.ReadSingle();
					this.theInputFileReader.ReadSingle();
					this.theInputFileReader.ReadSingle();
				}
				this.theVvdFileData.theVertexes.Add(aStudioVertex);
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVertexes " + this.theVvdFileData.theVertexes.Count.ToString());
		}

		public void ReadFixups()
		{
			if (this.theVvdFileData.fixupCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				this.theInputFileReader.BaseStream.Seek(this.theVvdFileOffsetStart + this.theVvdFileData.fixupTableOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theVvdFileData.theFixups = new List<SourceVvdFixup04>(this.theVvdFileData.fixupCount);
				for (int fixupIndex = 0; fixupIndex < this.theVvdFileData.fixupCount; fixupIndex++)
				{
					SourceVvdFixup04 aFixup = new SourceVvdFixup04();

					aFixup.lodIndex = this.theInputFileReader.ReadInt32();
					aFixup.vertexIndex = this.theInputFileReader.ReadInt32();
					aFixup.vertexCount = this.theInputFileReader.ReadInt32();
					this.theVvdFileData.theFixups.Add(aFixup);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theVvdFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theFixups " + this.theVvdFileData.theFixups.Count.ToString());

				if (this.theVvdFileData.lodCount > 0)
				{
					this.theInputFileReader.BaseStream.Seek(this.theVvdFileOffsetStart + this.theVvdFileData.vertexDataOffset, SeekOrigin.Begin);

					for (int lodIndex = 0; lodIndex < this.theVvdFileData.lodCount; lodIndex++)
					{
						this.SetupFixedVertexes(lodIndex);
					}
				}
			}
		}

		public void ReadUnreadBytes()
		{
			this.theVvdFileData.theFileSeekLog.LogUnreadBytes(this.theInputFileReader);
		}

#endregion

#region Private Methods

		private void SetupFixedVertexes(int lodIndex)
		{
			SourceVvdFixup04 aFixup = null;
			SourceVertex aStudioVertex = null;

			try
			{
				this.theVvdFileData.theFixedVertexesByLod[lodIndex] = new List<SourceVertex>();
				for (int fixupIndex = 0; fixupIndex < this.theVvdFileData.theFixups.Count; fixupIndex++)
				{
					aFixup = this.theVvdFileData.theFixups[fixupIndex];

					if (aFixup.lodIndex >= lodIndex)
					{
						for (int j = 0; j < aFixup.vertexCount; j++)
						{
							aStudioVertex = this.theVvdFileData.theVertexes[aFixup.vertexIndex + j];
							this.theVvdFileData.theFixedVertexesByLod[lodIndex].Add(aStudioVertex);
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