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
	public class SourceVtxFile06
	{

#region Creation and Destruction

		public SourceVtxFile06(BinaryReader vtxFileReader, SourceVtxFileData06 vtxFileData)
		{
			this.theInputFileReader = vtxFileReader;
			this.theVtxFileData = vtxFileData;

			this.theVtxFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;
		}

#endregion

#region Methods

		public void ReadSourceVtxHeader()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			// Offset: 0x00
			this.theVtxFileData.version = this.theInputFileReader.ReadInt32();

			// Offsets: 0x04, 0x08, 0x0A (10), 0x0C (12)
			this.theVtxFileData.vertexCacheSize = this.theInputFileReader.ReadInt32();
			this.theVtxFileData.maxBonesPerStrip = this.theInputFileReader.ReadUInt16();
			this.theVtxFileData.maxBonesPerTri = this.theInputFileReader.ReadUInt16();
			this.theVtxFileData.maxBonesPerVertex = this.theInputFileReader.ReadInt32();

			// Offset: 0x10 (16)
			this.theVtxFileData.checksum = this.theInputFileReader.ReadInt32();

			// Offset: 0x14 (20)
			this.theVtxFileData.lodCount = this.theInputFileReader.ReadInt32();

			// Offset: 0x18 (24)
			this.theVtxFileData.materialReplacementListOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0x1C (28), 0x20 (32)
			this.theVtxFileData.bodyPartCount = this.theInputFileReader.ReadInt32();
			this.theVtxFileData.bodyPartOffset = this.theInputFileReader.ReadInt32();

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VTX File Header (Actual version: " + this.theVtxFileData.version.ToString() + "; override version: 6)");
		}

		public void ReadSourceVtxBodyParts()
		{
			if (this.theVtxFileData.bodyPartCount > 0 && this.theVtxFileData.bodyPartOffset != 0)
			{
				long bodyPartInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theVtxFileData.bodyPartOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theVtxFileData.theVtxBodyParts = new List<SourceVtxBodyPart06>(this.theVtxFileData.bodyPartCount);
					for (int i = 0; i < this.theVtxFileData.bodyPartCount; i++)
					{
						bodyPartInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVtxBodyPart06 aBodyPart = new SourceVtxBodyPart06();

						aBodyPart.modelCount = this.theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = this.theInputFileReader.ReadInt32();

						this.theVtxFileData.theVtxBodyParts.Add(aBodyPart);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadSourceVtxModels(bodyPartInputFileStreamPosition, aBodyPart);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVtxFileData.theVtxBodyParts " + this.theVtxFileData.theVtxBodyParts.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSourceVtxMaterialReplacementLists()
		{
			if (this.theVtxFileData.materialReplacementListOffset != 0)
			{
				long materialReplacementListInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theVtxFileData.materialReplacementListOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theVtxFileData.theVtxMaterialReplacementLists = new List<SourceVtxMaterialReplacementList06>(this.theVtxFileData.bodyPartCount);
					//For i As Integer = 0 To Me.theVtxFileData.bodyPartCount - 1
					for (int i = 0; i < this.theVtxFileData.lodCount; i++)
					{
						materialReplacementListInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVtxMaterialReplacementList06 aMaterialReplacementList = new SourceVtxMaterialReplacementList06();

						aMaterialReplacementList.replacementCount = this.theInputFileReader.ReadInt32();
						aMaterialReplacementList.replacementOffset = this.theInputFileReader.ReadInt32();

						this.theVtxFileData.theVtxMaterialReplacementLists.Add(aMaterialReplacementList);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadSourceVtxMaterialReplacements(materialReplacementListInputFileStreamPosition, aMaterialReplacementList);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVtxFileData.theVtxMaterialReplacementLists " + this.theVtxFileData.theVtxMaterialReplacementLists.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadUnreadBytes()
		{
			this.theVtxFileData.theFileSeekLog.LogUnreadBytes(this.theInputFileReader);
		}

#endregion

#region Private Methods

		private void ReadSourceVtxModels(long bodyPartInputFileStreamPosition, SourceVtxBodyPart06 aBodyPart)
		{
			if (aBodyPart.modelCount > 0 && aBodyPart.modelOffset != 0)
			{
				long modelInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aBodyPart.theVtxModels = new List<SourceVtxModel06>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVtxModel06 aModel = new SourceVtxModel06();

						aModel.lodCount = this.theInputFileReader.ReadInt32();
						aModel.lodOffset = this.theInputFileReader.ReadInt32();

						aBodyPart.theVtxModels.Add(aModel);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theVtxModels " + aBodyPart.theVtxModels.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxModelLods(long modelInputFileStreamPosition, SourceVtxModel06 aModel)
		{
			if (aModel.lodCount > 0 && aModel.lodOffset != 0)
			{
				long modelLodInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.lodOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theVtxModelLods = new List<SourceVtxModelLod06>(aModel.lodCount);
					for (int j = 0; j < aModel.lodCount; j++)
					{
						modelLodInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVtxModelLod06 aModelLod = new SourceVtxModelLod06();

						aModelLod.meshCount = this.theInputFileReader.ReadInt32();
						aModelLod.meshOffset = this.theInputFileReader.ReadInt32();
						aModelLod.switchPoint = this.theInputFileReader.ReadSingle();
						aModelLod.theVtxModelLodUsesFacial = false;

						aModel.theVtxModelLods.Add(aModelLod);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadSourceVtxMeshes(modelLodInputFileStreamPosition, aModelLod);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVtxModelLods " + aModel.theVtxModelLods.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxMeshes(long modelLodInputFileStreamPosition, SourceVtxModelLod06 aModelLod)
		{
			if (aModelLod.meshCount > 0 && aModelLod.meshOffset != 0)
			{
				long meshInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(modelLodInputFileStreamPosition + aModelLod.meshOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModelLod.theVtxMeshes = new List<SourceVtxMesh06>(aModelLod.meshCount);
					for (int j = 0; j < aModelLod.meshCount; j++)
					{
						meshInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVtxMesh06 aMesh = new SourceVtxMesh06();

						aMesh.stripGroupCount = this.theInputFileReader.ReadInt32();
						aMesh.stripGroupOffset = this.theInputFileReader.ReadInt32();
						aMesh.flags = this.theInputFileReader.ReadByte();

						aModelLod.theVtxMeshes.Add(aMesh);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aModelLod, aMesh);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModelLod.theVtxMeshes " + aModelLod.theVtxMeshes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxStripGroups(long meshInputFileStreamPosition, SourceVtxModelLod06 aModelLod, SourceVtxMesh06 aMesh)
		{
			if (aMesh.stripGroupCount > 0 && aMesh.stripGroupOffset != 0)
			{
				long stripGroupInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aMesh.theVtxStripGroups = new List<SourceVtxStripGroup06>(aMesh.stripGroupCount);
					for (int j = 0; j < aMesh.stripGroupCount; j++)
					{
						stripGroupInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVtxStripGroup06 aStripGroup = new SourceVtxStripGroup06();

						aStripGroup.vertexCount = this.theInputFileReader.ReadInt32();
						aStripGroup.vertexOffset = this.theInputFileReader.ReadInt32();
						aStripGroup.indexCount = this.theInputFileReader.ReadInt32();
						aStripGroup.indexOffset = this.theInputFileReader.ReadInt32();
						aStripGroup.stripCount = this.theInputFileReader.ReadInt32();
						aStripGroup.stripOffset = this.theInputFileReader.ReadInt32();
						aStripGroup.flags = this.theInputFileReader.ReadByte();

						aMesh.theVtxStripGroups.Add(aStripGroup);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup);
						this.ReadSourceVtxIndexes(stripGroupInputFileStreamPosition, aStripGroup);
						this.ReadSourceVtxStrips(stripGroupInputFileStreamPosition, aStripGroup);

						//TODO: Set whether stripgroup has flex vertexes in it or not for $lod facial and nofacial options.
						if ((aStripGroup.flags & SourceVtxStripGroup06.STRIPGROUP_IS_FLEXED) > 0)
						{
							aModelLod.theVtxModelLodUsesFacial = true;
							//------
							//Dim aVtxVertex As SourceVtxVertex
							//For Each aVtxVertexIndex As UShort In aStripGroup.theVtxIndexes
							//	aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)

							//	' for (i = 0; i < pStudioMesh->numflexes; i++)
							//	' for (j = 0; j < pflex[i].numverts; j++)
							//	'The meshflexes are found in the MDL file > bodypart > model > mesh.theFlexes
							//	For Each meshFlex As SourceMdlFlex In meshflexes

							//	Next
							//Next
							//'Dim debug As Integer = 4242
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theVtxStripGroups " + aMesh.theVtxStripGroups.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxVertexes(long stripGroupInputFileStreamPosition, SourceVtxStripGroup06 aStripGroup)
		{
			if (aStripGroup.vertexCount > 0 && aStripGroup.vertexOffset != 0)
			{
				//Dim modelInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aStripGroup.theVtxVertexes = new List<SourceVtxVertex06>(aStripGroup.vertexCount);
					for (int j = 0; j < aStripGroup.vertexCount; j++)
					{
						//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceVtxVertex06 aVertex = new SourceVtxVertex06();

						for (int x = 0; x < aVertex.boneWeightIndex.Length; x++)
						{
							aVertex.boneWeightIndex[x] = this.theInputFileReader.ReadByte();
						}
						for (int x = 0; x < aVertex.boneIndex.Length; x++)
						{
							aVertex.boneIndex[x] = this.theInputFileReader.ReadInt16();
						}
						aVertex.originalMeshVertexIndex = this.theInputFileReader.ReadInt16();
						aVertex.boneCount = this.theInputFileReader.ReadByte();

						aStripGroup.theVtxVertexes.Add(aVertex);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxVertexes " + aStripGroup.theVtxVertexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxIndexes(long stripGroupInputFileStreamPosition, SourceVtxStripGroup06 aStripGroup)
		{
			if (aStripGroup.indexCount > 0 && aStripGroup.indexOffset != 0)
			{
				//Dim modelInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.indexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aStripGroup.theVtxIndexes = new List<ushort>(aStripGroup.indexCount);
					for (int j = 0; j < aStripGroup.indexCount; j++)
					{
						//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						aStripGroup.theVtxIndexes.Add(this.theInputFileReader.ReadUInt16());

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxIndexes " + aStripGroup.theVtxIndexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxStrips(long stripGroupInputFileStreamPosition, SourceVtxStripGroup06 aStripGroup)
		{
			if (aStripGroup.stripCount > 0 && aStripGroup.stripOffset != 0)
			{
				long stripInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.stripOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aStripGroup.theVtxStrips = new List<SourceVtxStrip06>(aStripGroup.stripCount);
					for (int j = 0; j < aStripGroup.stripCount; j++)
					{
						stripInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVtxStrip06 aStrip = new SourceVtxStrip06();

						aStrip.indexCount = this.theInputFileReader.ReadInt32();
						aStrip.indexMeshIndex = this.theInputFileReader.ReadInt32();
						aStrip.vertexCount = this.theInputFileReader.ReadInt32();
						aStrip.vertexMeshIndex = this.theInputFileReader.ReadInt32();
						aStrip.boneCount = this.theInputFileReader.ReadInt16();
						aStrip.flags = this.theInputFileReader.ReadByte();
						aStrip.boneStateChangeCount = this.theInputFileReader.ReadInt32();
						aStrip.boneStateChangeOffset = this.theInputFileReader.ReadInt32();

						aStripGroup.theVtxStrips.Add(aStrip);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadSourceVtxBoneStateChanges(stripInputFileStreamPosition, aStrip);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxStrips " + aStripGroup.theVtxStrips.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxBoneStateChanges(long stripInputFileStreamPosition, SourceVtxStrip06 aStrip)
		{
			//TEST: 
			//NOTE: It seems that if boneCount = 0 then a SourceVtxBoneStateChange is stored.
			int boneStateChangeCount = aStrip.boneStateChangeCount;
			if (aStrip.boneCount == 0 && aStrip.boneStateChangeOffset != 0)
			{
				boneStateChangeCount = 1;
			}

			if (boneStateChangeCount > 0 && aStrip.boneStateChangeOffset != 0)
			{
				//Dim modelInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(stripInputFileStreamPosition + aStrip.boneStateChangeOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					//aStrip.theVtxBoneStateChanges = New List(Of SourceVtxBoneStateChange06)(aStrip.boneStateChangeCount)
					aStrip.theVtxBoneStateChanges = new List<SourceVtxBoneStateChange06>(boneStateChangeCount);
					//For j As Integer = 0 To aStrip.boneStateChangeCount - 1
					for (int j = 0; j < boneStateChangeCount; j++)
					{
						//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceVtxBoneStateChange06 aBoneStateChange = new SourceVtxBoneStateChange06();

						aBoneStateChange.hardwareId = this.theInputFileReader.ReadInt32();
						aBoneStateChange.newBoneId = this.theInputFileReader.ReadInt32();

						aStrip.theVtxBoneStateChanges.Add(aBoneStateChange);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStrip")

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStrip.theVtxBoneStateChanges " + aStrip.theVtxBoneStateChanges.Count.ToString());
				}
				catch (Exception ex)
				{
					//NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxMaterialReplacements(long materialReplacementListInputFileStreamPosition, SourceVtxMaterialReplacementList06 aMaterialReplacementList)
		{
			if (aMaterialReplacementList.replacementCount > 0 && aMaterialReplacementList.replacementOffset != 0)
			{
				long materialReplacementInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(materialReplacementListInputFileStreamPosition + aMaterialReplacementList.replacementOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aMaterialReplacementList.theVtxMaterialReplacements = new List<SourceVtxMaterialReplacement06>(aMaterialReplacementList.replacementCount);
					for (int j = 0; j < aMaterialReplacementList.replacementCount; j++)
					{
						materialReplacementInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVtxMaterialReplacement06 aMaterialReplacement = new SourceVtxMaterialReplacement06();

						aMaterialReplacement.materialIndex = this.theInputFileReader.ReadInt16();
						aMaterialReplacement.nameOffset = this.theInputFileReader.ReadInt32();

						aMaterialReplacementList.theVtxMaterialReplacements.Add(aMaterialReplacement);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aMaterialReplacement.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(materialReplacementInputFileStreamPosition + aMaterialReplacement.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aMaterialReplacement.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theVtxFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aMaterialReplacement.theName = " + aMaterialReplacement.theName);
							}
						}
						else if (aMaterialReplacement.theName == null)
						{
							aMaterialReplacement.theName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMaterialReplacementList.theVtxMaterialReplacements " + aMaterialReplacementList.theVtxMaterialReplacements.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

#endregion

#region Data

		private BinaryReader theInputFileReader;
		private SourceVtxFileData06 theVtxFileData;

#endregion

	}

}