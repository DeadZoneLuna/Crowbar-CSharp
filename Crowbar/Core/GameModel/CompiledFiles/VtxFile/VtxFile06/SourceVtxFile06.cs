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
			theInputFileReader = vtxFileReader;
			theVtxFileData = vtxFileData;

			theVtxFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

#endregion

#region Methods

		public void ReadSourceVtxHeader()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			// Offset: 0x00
			theVtxFileData.version = theInputFileReader.ReadInt32();

			// Offsets: 0x04, 0x08, 0x0A (10), 0x0C (12)
			theVtxFileData.vertexCacheSize = theInputFileReader.ReadInt32();
			theVtxFileData.maxBonesPerStrip = theInputFileReader.ReadUInt16();
			theVtxFileData.maxBonesPerTri = theInputFileReader.ReadUInt16();
			theVtxFileData.maxBonesPerVertex = theInputFileReader.ReadInt32();

			// Offset: 0x10 (16)
			theVtxFileData.checksum = theInputFileReader.ReadInt32();

			// Offset: 0x14 (20)
			theVtxFileData.lodCount = theInputFileReader.ReadInt32();

			// Offset: 0x18 (24)
			theVtxFileData.materialReplacementListOffset = theInputFileReader.ReadInt32();

			// Offsets: 0x1C (28), 0x20 (32)
			theVtxFileData.bodyPartCount = theInputFileReader.ReadInt32();
			theVtxFileData.bodyPartOffset = theInputFileReader.ReadInt32();

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VTX File Header (Actual version: " + theVtxFileData.version.ToString() + "; override version: 6)");
		}

		public void ReadSourceVtxBodyParts()
		{
			if (theVtxFileData.bodyPartCount > 0 && theVtxFileData.bodyPartOffset != 0)
			{
				long bodyPartInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theVtxFileData.bodyPartOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theVtxFileData.theVtxBodyParts = new List<SourceVtxBodyPart06>(theVtxFileData.bodyPartCount);
					for (int i = 0; i < theVtxFileData.bodyPartCount; i++)
					{
						bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxBodyPart06 aBodyPart = new SourceVtxBodyPart06();

						aBodyPart.modelCount = theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = theInputFileReader.ReadInt32();

						theVtxFileData.theVtxBodyParts.Add(aBodyPart);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxModels(bodyPartInputFileStreamPosition, aBodyPart);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVtxFileData.theVtxBodyParts " + theVtxFileData.theVtxBodyParts.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSourceVtxMaterialReplacementLists()
		{
			if (theVtxFileData.materialReplacementListOffset != 0)
			{
				long materialReplacementListInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theVtxFileData.materialReplacementListOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theVtxFileData.theVtxMaterialReplacementLists = new List<SourceVtxMaterialReplacementList06>(theVtxFileData.bodyPartCount);
					//For i As Integer = 0 To Me.theVtxFileData.bodyPartCount - 1
					for (int i = 0; i < theVtxFileData.lodCount; i++)
					{
						materialReplacementListInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxMaterialReplacementList06 aMaterialReplacementList = new SourceVtxMaterialReplacementList06();

						aMaterialReplacementList.replacementCount = theInputFileReader.ReadInt32();
						aMaterialReplacementList.replacementOffset = theInputFileReader.ReadInt32();

						theVtxFileData.theVtxMaterialReplacementLists.Add(aMaterialReplacementList);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxMaterialReplacements(materialReplacementListInputFileStreamPosition, aMaterialReplacementList);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theVtxFileData.theVtxMaterialReplacementLists " + theVtxFileData.theVtxMaterialReplacementLists.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadUnreadBytes()
		{
			theVtxFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
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
					theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aBodyPart.theVtxModels = new List<SourceVtxModel06>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxModel06 aModel = new SourceVtxModel06();

						aModel.lodCount = theInputFileReader.ReadInt32();
						aModel.lodOffset = theInputFileReader.ReadInt32();

						aBodyPart.theVtxModels.Add(aModel);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theVtxModels " + aBodyPart.theVtxModels.Count.ToString());
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
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.lodOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theVtxModelLods = new List<SourceVtxModelLod06>(aModel.lodCount);
					for (int j = 0; j < aModel.lodCount; j++)
					{
						modelLodInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxModelLod06 aModelLod = new SourceVtxModelLod06();

						aModelLod.meshCount = theInputFileReader.ReadInt32();
						aModelLod.meshOffset = theInputFileReader.ReadInt32();
						aModelLod.switchPoint = theInputFileReader.ReadSingle();
						aModelLod.theVtxModelLodUsesFacial = false;

						aModel.theVtxModelLods.Add(aModelLod);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxMeshes(modelLodInputFileStreamPosition, aModelLod);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVtxModelLods " + aModel.theVtxModelLods.Count.ToString());
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
					theInputFileReader.BaseStream.Seek(modelLodInputFileStreamPosition + aModelLod.meshOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModelLod.theVtxMeshes = new List<SourceVtxMesh06>(aModelLod.meshCount);
					for (int j = 0; j < aModelLod.meshCount; j++)
					{
						meshInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxMesh06 aMesh = new SourceVtxMesh06();

						aMesh.stripGroupCount = theInputFileReader.ReadInt32();
						aMesh.stripGroupOffset = theInputFileReader.ReadInt32();
						aMesh.flags = theInputFileReader.ReadByte();

						aModelLod.theVtxMeshes.Add(aMesh);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxStripGroups(meshInputFileStreamPosition, aModelLod, aMesh);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModelLod.theVtxMeshes " + aModelLod.theVtxMeshes.Count.ToString());
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
					theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aMesh.theVtxStripGroups = new List<SourceVtxStripGroup06>(aMesh.stripGroupCount);
					for (int j = 0; j < aMesh.stripGroupCount; j++)
					{
						stripGroupInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxStripGroup06 aStripGroup = new SourceVtxStripGroup06();

						aStripGroup.vertexCount = theInputFileReader.ReadInt32();
						aStripGroup.vertexOffset = theInputFileReader.ReadInt32();
						aStripGroup.indexCount = theInputFileReader.ReadInt32();
						aStripGroup.indexOffset = theInputFileReader.ReadInt32();
						aStripGroup.stripCount = theInputFileReader.ReadInt32();
						aStripGroup.stripOffset = theInputFileReader.ReadInt32();
						aStripGroup.flags = theInputFileReader.ReadByte();

						aMesh.theVtxStripGroups.Add(aStripGroup);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup);
						ReadSourceVtxIndexes(stripGroupInputFileStreamPosition, aStripGroup);
						ReadSourceVtxStrips(stripGroupInputFileStreamPosition, aStripGroup);

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

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theVtxStripGroups " + aMesh.theVtxStripGroups.Count.ToString());
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
					theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aStripGroup.theVtxVertexes = new List<SourceVtxVertex06>(aStripGroup.vertexCount);
					for (int j = 0; j < aStripGroup.vertexCount; j++)
					{
						//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceVtxVertex06 aVertex = new SourceVtxVertex06();

						for (int x = 0; x < aVertex.boneWeightIndex.Length; x++)
						{
							aVertex.boneWeightIndex[x] = theInputFileReader.ReadByte();
						}
						for (int x = 0; x < aVertex.boneIndex.Length; x++)
						{
							aVertex.boneIndex[x] = theInputFileReader.ReadInt16();
						}
						aVertex.originalMeshVertexIndex = theInputFileReader.ReadInt16();
						aVertex.boneCount = theInputFileReader.ReadByte();

						aStripGroup.theVtxVertexes.Add(aVertex);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxVertexes " + aStripGroup.theVtxVertexes.Count.ToString());
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
					theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.indexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aStripGroup.theVtxIndexes = new List<ushort>(aStripGroup.indexCount);
					for (int j = 0; j < aStripGroup.indexCount; j++)
					{
						//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						aStripGroup.theVtxIndexes.Add(theInputFileReader.ReadUInt16());

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxIndexes " + aStripGroup.theVtxIndexes.Count.ToString());
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
					theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.stripOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aStripGroup.theVtxStrips = new List<SourceVtxStrip06>(aStripGroup.stripCount);
					for (int j = 0; j < aStripGroup.stripCount; j++)
					{
						stripInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxStrip06 aStrip = new SourceVtxStrip06();

						aStrip.indexCount = theInputFileReader.ReadInt32();
						aStrip.indexMeshIndex = theInputFileReader.ReadInt32();
						aStrip.vertexCount = theInputFileReader.ReadInt32();
						aStrip.vertexMeshIndex = theInputFileReader.ReadInt32();
						aStrip.boneCount = theInputFileReader.ReadInt16();
						aStrip.flags = theInputFileReader.ReadByte();
						aStrip.boneStateChangeCount = theInputFileReader.ReadInt32();
						aStrip.boneStateChangeOffset = theInputFileReader.ReadInt32();

						aStripGroup.theVtxStrips.Add(aStrip);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxBoneStateChanges(stripInputFileStreamPosition, aStrip);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxStrips " + aStripGroup.theVtxStrips.Count.ToString());
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
					theInputFileReader.BaseStream.Seek(stripInputFileStreamPosition + aStrip.boneStateChangeOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					//aStrip.theVtxBoneStateChanges = New List(Of SourceVtxBoneStateChange06)(aStrip.boneStateChangeCount)
					aStrip.theVtxBoneStateChanges = new List<SourceVtxBoneStateChange06>(boneStateChangeCount);
					//For j As Integer = 0 To aStrip.boneStateChangeCount - 1
					for (int j = 0; j < boneStateChangeCount; j++)
					{
						//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceVtxBoneStateChange06 aBoneStateChange = new SourceVtxBoneStateChange06();

						aBoneStateChange.hardwareId = theInputFileReader.ReadInt32();
						aBoneStateChange.newBoneId = theInputFileReader.ReadInt32();

						aStrip.theVtxBoneStateChanges.Add(aBoneStateChange);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStrip")

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStrip.theVtxBoneStateChanges " + aStrip.theVtxBoneStateChanges.Count.ToString());
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
					theInputFileReader.BaseStream.Seek(materialReplacementListInputFileStreamPosition + aMaterialReplacementList.replacementOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aMaterialReplacementList.theVtxMaterialReplacements = new List<SourceVtxMaterialReplacement06>(aMaterialReplacementList.replacementCount);
					for (int j = 0; j < aMaterialReplacementList.replacementCount; j++)
					{
						materialReplacementInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxMaterialReplacement06 aMaterialReplacement = new SourceVtxMaterialReplacement06();

						aMaterialReplacement.materialIndex = theInputFileReader.ReadInt16();
						aMaterialReplacement.nameOffset = theInputFileReader.ReadInt32();

						aMaterialReplacementList.theVtxMaterialReplacements.Add(aMaterialReplacement);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aMaterialReplacement.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(materialReplacementInputFileStreamPosition + aMaterialReplacement.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aMaterialReplacement.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theVtxFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theVtxFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aMaterialReplacement.theName = " + aMaterialReplacement.theName);
							}
						}
						else if (aMaterialReplacement.theName == null)
						{
							aMaterialReplacement.theName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMaterialReplacementList.theVtxMaterialReplacements " + aMaterialReplacementList.theVtxMaterialReplacements.Count.ToString());
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