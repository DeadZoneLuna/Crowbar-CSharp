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
	public class SourceVtxFile07
	{

#region Creation and Destruction

		public SourceVtxFile07(BinaryReader vtxFileReader, SourceVtxFileData07 vtxFileData, long vtxFileOffsetStart = 0)
		{
			theInputFileReader = vtxFileReader;
			theVtxFileOffsetStart = vtxFileOffsetStart;
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
			theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VTX File Header (Actual version: " + theVtxFileData.version.ToString() + "; override version: 7)");
		}

		public void ReadSourceVtxBodyParts()
		{
			if (theVtxFileData.bodyPartCount > 0)
			{
				//NOTE: Stuff that is part of determining vtx strip group size.
				theFirstMeshWithStripGroups = null;
				theFirstMeshWithStripGroupsInputFileStreamPosition = -1;
				theModelLodHasOnlyOneMesh = false;
				theSecondMeshWithStripGroups = null;
				theExpectedStartOfDataAfterFirstStripGroupList = -1;
				theStripGroupAndStripUseExtraFields = false;

				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theVtxFileOffsetStart + theVtxFileData.bodyPartOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					ReadSourceVtxBodyParts_Internal();

					//TODO: If there is no second mesh with strip groups, then check if calculated offset matches actual offset.
					//      If does not match, then read again with larger sizes.
					if (theSecondMeshWithStripGroups == null)
					{
						if (theStartOfStripGroupVtxStrips != -1 && theExpectedStartOfDataAfterFirstStripGroupList != theStartOfStripGroupVtxStrips)
						{
							theStripGroupAndStripUseExtraFields = true;
							theInputFileReader.BaseStream.Seek(theVtxFileOffsetStart + theVtxFileData.bodyPartOffset, SeekOrigin.Begin);
							ReadSourceVtxBodyParts_Internal();
						}
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
					theInputFileReader.BaseStream.Seek(theVtxFileOffsetStart + theVtxFileData.materialReplacementListOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theVtxFileData.theVtxMaterialReplacementLists = new List<SourceVtxMaterialReplacementList07>(theVtxFileData.lodCount);
					for (int i = 0; i < theVtxFileData.lodCount; i++)
					{
						materialReplacementListInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxMaterialReplacementList07 aMaterialReplacementList = new SourceVtxMaterialReplacementList07();

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

		private void ReadSourceVtxBodyParts_Internal()
		{
			long bodyPartInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;

			try
			{
				theVtxFileData.theVtxBodyParts = new List<SourceVtxBodyPart07>(theVtxFileData.bodyPartCount);
				for (int i = 0; i < theVtxFileData.bodyPartCount; i++)
				{
					bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceVtxBodyPart07 aBodyPart = new SourceVtxBodyPart07();

					aBodyPart.modelCount = theInputFileReader.ReadInt32();
					aBodyPart.modelOffset = theInputFileReader.ReadInt32();

					theVtxFileData.theVtxBodyParts.Add(aBodyPart);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					ReadSourceVtxModels(bodyPartInputFileStreamPosition, aBodyPart);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadSourceVtxModels(long bodyPartInputFileStreamPosition, SourceVtxBodyPart07 aBodyPart)
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

					aBodyPart.theVtxModels = new List<SourceVtxModel07>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxModel07 aModel = new SourceVtxModel07();

						aModel.lodCount = theInputFileReader.ReadInt32();
						aModel.lodOffset = theInputFileReader.ReadInt32();

						aBodyPart.theVtxModels.Add(aModel);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aModel.lodCount > 0 && aModel.lodOffset != 0)
						{
							ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel);
						}

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

		private void ReadSourceVtxModelLods(long modelInputFileStreamPosition, SourceVtxModel07 aModel)
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

				aModel.theVtxModelLods = new List<SourceVtxModelLod07>(aModel.lodCount);
				for (int j = 0; j < aModel.lodCount; j++)
				{
					modelLodInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceVtxModelLod07 aModelLod = new SourceVtxModelLod07();

					aModelLod.meshCount = theInputFileReader.ReadInt32();
					aModelLod.meshOffset = theInputFileReader.ReadInt32();
					aModelLod.switchPoint = theInputFileReader.ReadSingle();
					aModelLod.theVtxModelLodUsesFacial = false;

					aModel.theVtxModelLods.Add(aModelLod);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aModelLod.meshCount > 0 && aModelLod.meshOffset != 0)
					{
						ReadSourceVtxMeshes(modelLodInputFileStreamPosition, aModelLod);
					}

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

		private void ReadSourceVtxMeshes(long modelLodInputFileStreamPosition, SourceVtxModelLod07 aModelLod)
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

				aModelLod.theVtxMeshes = new List<SourceVtxMesh07>(aModelLod.meshCount);
				for (int j = 0; j < aModelLod.meshCount; j++)
				{
					meshInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceVtxMesh07 aMesh = new SourceVtxMesh07();

					aMesh.stripGroupCount = theInputFileReader.ReadInt32();
					aMesh.stripGroupOffset = theInputFileReader.ReadInt32();
					aMesh.flags = theInputFileReader.ReadByte();

					aModelLod.theVtxMeshes.Add(aMesh);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aMesh.stripGroupCount > 0 && aMesh.stripGroupOffset != 0)
					{
						if (theFirstMeshWithStripGroups == null)
						{
							theFirstMeshWithStripGroups = aMesh;
							theFirstMeshWithStripGroupsInputFileStreamPosition = meshInputFileStreamPosition;
							AnalyzeVtxStripGroups(meshInputFileStreamPosition, aMesh);

							//'NOTE: If there is only one mesh, then compare end offset of VtxStripGroups with start of VtxStrips.
							//If aModelLod.meshCount = 1 Then
							//	If aMesh.theVtxStripGroups(0).stripCount > 0 AndAlso aMesh.theVtxStripGroups(0).stripOffset <> 0 Then
							//		If Me.theExpectedStartOfDataAfterFirstStripGroupList <> (meshInputFileStreamPosition + aMesh.stripGroupOffset + aMesh.theVtxStripGroups(0).stripOffset) Then
							//			Me.theStripGroupAndStripUseExtraFields = True
							//		End If
							//	End If
							//End If
						}
						else if (theSecondMeshWithStripGroups == null)
						{
							theSecondMeshWithStripGroups = aMesh;
							if (theExpectedStartOfDataAfterFirstStripGroupList != (meshInputFileStreamPosition + aMesh.stripGroupOffset))
							{
								theStripGroupAndStripUseExtraFields = true;

								//If aMesh.theVtxStripGroups IsNot Nothing Then
								//	aMesh.theVtxStripGroups.Clear()
								//End If
								theVtxFileData.theFileSeekLog.Remove(theFirstMeshWithStripGroupsInputFileStreamPosition + theFirstMeshWithStripGroups.stripGroupOffset);
								theVtxFileData.theFileSeekLog.Remove(theFirstMeshWithStripGroupsInputFileStreamPosition + theFirstMeshWithStripGroups.stripGroupOffset + theFirstMeshWithStripGroups.theVtxStripGroups[0].stripOffset);

								ReadSourceVtxStripGroups(theFirstMeshWithStripGroupsInputFileStreamPosition, aModelLod, theFirstMeshWithStripGroups);
							}
						}

						ReadSourceVtxStripGroups(meshInputFileStreamPosition, aModelLod, aMesh);
					}

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

		//TEST: / Save the first mesh that has strip groups and loop through the mesh's strip groups.
		//      / Get the file offset and store as Me.theExpectedStartOfSecondStripGroupList.
		//      / When the next strip group's offset is read in, compare with Me.theExpectedStartOfSecondStripGroupList.
		//      If equal, then read from first mesh with strip groups without further checking.
		//      Else (if unequal), then read from first mesh with strip groups 
		//          and continue reading remaining data using larger strip group size.
		//      WORKS for the SFM, Dota 2, and L4D2 models I tested.
		private void AnalyzeVtxStripGroups(long meshInputFileStreamPosition, SourceVtxMesh07 aMesh)
		{
			try
			{
				theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin);
				aMesh.theVtxStripGroups = new List<SourceVtxStripGroup07>(aMesh.stripGroupCount);
				for (int j = 0; j < aMesh.stripGroupCount; j++)
				{
					SourceVtxStripGroup07 aStripGroup = new SourceVtxStripGroup07();
					aStripGroup.vertexCount = theInputFileReader.ReadInt32();
					aStripGroup.vertexOffset = theInputFileReader.ReadInt32();
					aStripGroup.indexCount = theInputFileReader.ReadInt32();
					aStripGroup.indexOffset = theInputFileReader.ReadInt32();
					aStripGroup.stripCount = theInputFileReader.ReadInt32();
					aStripGroup.stripOffset = theInputFileReader.ReadInt32();
					aStripGroup.flags = theInputFileReader.ReadByte();

					aMesh.theVtxStripGroups.Add(aStripGroup);
				}

				theExpectedStartOfDataAfterFirstStripGroupList = theInputFileReader.BaseStream.Position;
			}
			catch (Exception ex)
			{
				//NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
				int debug = 4242;
			}
		}

		private void ReadSourceVtxStripGroups(long meshInputFileStreamPosition, SourceVtxModelLod07 aModelLod, SourceVtxMesh07 aMesh)
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

				aMesh.theVtxStripGroups = new List<SourceVtxStripGroup07>(aMesh.stripGroupCount);
				for (int j = 0; j < aMesh.stripGroupCount; j++)
				{
					stripGroupInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceVtxStripGroup07 aStripGroup = new SourceVtxStripGroup07();

					aStripGroup.vertexCount = theInputFileReader.ReadInt32();
					aStripGroup.vertexOffset = theInputFileReader.ReadInt32();
					aStripGroup.indexCount = theInputFileReader.ReadInt32();
					aStripGroup.indexOffset = theInputFileReader.ReadInt32();
					aStripGroup.stripCount = theInputFileReader.ReadInt32();
					aStripGroup.stripOffset = theInputFileReader.ReadInt32();
					aStripGroup.flags = theInputFileReader.ReadByte();

					//'TEST: Did not work for both Engineeer and doom.
					//'If (aStripGroup.flags And SourceVtxStripGroup.SourceStripGroupDeltaFixed) > 0 Then
					//'TEST: Works with models I tested from SFM, TF2 and Dota 2.
					//'      Failed on models compiled for L4D2.
					//If Me.theStripGroupUsesExtra8Bytes Then
					//	''TEST: Needed for SFM model, Barabus.
					//	'      Skip for TF2 Engineer and Heavy.
					//	Me.theInputFileReader.ReadInt32()
					//	Me.theInputFileReader.ReadInt32()
					//End If
					//TEST:
					if (theStripGroupAndStripUseExtraFields)
					{
						aStripGroup.topologyIndexCount = theInputFileReader.ReadInt32();
						aStripGroup.topologyIndexOffset = theInputFileReader.ReadInt32();
					}

					aMesh.theVtxStripGroups.Add(aStripGroup);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aStripGroup.vertexCount > 0 && aStripGroup.vertexOffset != 0)
					{
						ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup);
						//TEST: Did not correct the missing SFM HWM Sniper left arm mesh.
						//ElseIf j > 0 Then
						//	aStripGroup.vertexCount = aMesh.theVtxStripGroups(j - 1).vertexCount
						//	Me.ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup)
						//TEST: Did not correct the missing SFM HWM Sniper left arm mesh.
						//ElseIf j > 0 Then
						//	aStripGroup.theVtxVertexes = aMesh.theVtxStripGroups(j - 1).theVtxVertexes
					}
					if (aStripGroup.indexCount > 0 && aStripGroup.indexOffset != 0)
					{
						ReadSourceVtxIndexes(stripGroupInputFileStreamPosition, aStripGroup);
					}
					if (aStripGroup.stripCount > 0 && aStripGroup.stripOffset != 0)
					{
						ReadSourceVtxStrips(stripGroupInputFileStreamPosition, aStripGroup);
					}
					//TEST:
					if (theStripGroupAndStripUseExtraFields)
					{
						if (aStripGroup.topologyIndexCount > 0 && aStripGroup.topologyIndexOffset != 0)
						{
							ReadSourceVtxTopologyIndexes(stripGroupInputFileStreamPosition, aStripGroup);
						}
					}

					// Set whether stripgroup has flex vertexes in it or not for $lod facial and nofacial options.
					if ((aStripGroup.flags & SourceVtxStripGroup07.SourceStripGroupFlexed) > 0 || (aStripGroup.flags & SourceVtxStripGroup07.SourceStripGroupDeltaFixed) > 0)
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
				//NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
				int debug = 4242;
			}
		}

		private void ReadSourceVtxVertexes(long stripGroupInputFileStreamPosition, SourceVtxStripGroup07 aStripGroup)
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

				aStripGroup.theVtxVertexes = new List<SourceVtxVertex07>(aStripGroup.vertexCount);
				for (int j = 0; j < aStripGroup.vertexCount; j++)
				{
					//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceVtxVertex07 aVertex = new SourceVtxVertex07();

					for (int i = 0; i < SourceConstants.MAX_NUM_BONES_PER_VERT; i++)
					{
						aVertex.boneWeightIndex[i] = theInputFileReader.ReadByte();
					}
					aVertex.boneCount = theInputFileReader.ReadByte();
					aVertex.originalMeshVertexIndex = theInputFileReader.ReadUInt16();
					for (int i = 0; i < SourceConstants.MAX_NUM_BONES_PER_VERT; i++)
					{
						aVertex.boneId[i] = theInputFileReader.ReadByte();
					}

					aStripGroup.theVtxVertexes.Add(aVertex);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxVertexes " + aStripGroup.theVtxVertexes.Count.ToString());
			}
			catch (Exception ex)
			{
				//NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
				int debug = 4242;
			}
		}

		private void ReadSourceVtxIndexes(long stripGroupInputFileStreamPosition, SourceVtxStripGroup07 aStripGroup)
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

		private void ReadSourceVtxStrips(long stripGroupInputFileStreamPosition, SourceVtxStripGroup07 aStripGroup)
		{
			long stripInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				if (theStartOfStripGroupVtxStrips == -1)
				{
					theStartOfStripGroupVtxStrips = stripGroupInputFileStreamPosition + aStripGroup.stripOffset;
				}

				theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.stripOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aStripGroup.theVtxStrips = new List<SourceVtxStrip07>(aStripGroup.stripCount);
				for (int j = 0; j < aStripGroup.stripCount; j++)
				{
					stripInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceVtxStrip07 aStrip = new SourceVtxStrip07();

					aStrip.indexCount = theInputFileReader.ReadInt32();
					aStrip.indexMeshIndex = theInputFileReader.ReadInt32();
					aStrip.vertexCount = theInputFileReader.ReadInt32();
					aStrip.vertexMeshIndex = theInputFileReader.ReadInt32();
					aStrip.boneCount = theInputFileReader.ReadInt16();
					aStrip.flags = theInputFileReader.ReadByte();
					aStrip.boneStateChangeCount = theInputFileReader.ReadInt32();
					aStrip.boneStateChangeOffset = theInputFileReader.ReadInt32();
					//TEST:
					if (theStripGroupAndStripUseExtraFields)
					{
						aStrip.unknownBytes01 = theInputFileReader.ReadInt32();
						aStrip.unknownBytes02 = theInputFileReader.ReadInt32();
					}

					aStripGroup.theVtxStrips.Add(aStrip);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					//TODO: Commented-out due to using a ton of memory and long time to read incorrectly the model 
					//      from this bug report: 2019-03-08 bad decompile\alyx_sfm
					//Me.ReadSourceVtxBoneStateChanges(stripInputFileStreamPosition, aStrip)

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxStrips " + aStripGroup.theVtxStrips.Count.ToString());
			}
			catch (Exception ex)
			{
				//NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
				int debug = 4242;
			}
		}

		private void ReadSourceVtxTopologyIndexes(long stripGroupInputFileStreamPosition, SourceVtxStripGroup07 aStripGroup)
		{
			//Dim topologyInputFileStreamPosition As Long
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				theInputFileReader.BaseStream.Seek(stripGroupInputFileStreamPosition + aStripGroup.topologyIndexOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aStripGroup.theVtxTopologyIndexes = new List<ushort>(aStripGroup.topologyIndexCount);
				for (int j = 0; j < aStripGroup.topologyIndexCount; j++)
				{
					//topologyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					ushort topologyIndex = theInputFileReader.ReadUInt16();


					aStripGroup.theVtxTopologyIndexes.Add(topologyIndex);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxTopologies " + aStripGroup.theVtxTopologyIndexes.Count.ToString());
			}
			catch (Exception ex)
			{
				//NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
				int debug = 4242;
			}
		}

		private void ReadSourceVtxBoneStateChanges(long stripInputFileStreamPosition, SourceVtxStrip07 aStrip)
		{
			//TODO: On an alyx.mdl from SFM, aStrip.boneStateChangeCount is over 800,000, so maybe when astrip.flag has bit flag 4 set, skip this reading.
			if (((aStrip.flags & 1) == 0) || ((aStrip.flags & 4) > 0))
			{
				return;
			}

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

					//aStrip.theVtxBoneStateChanges = New List(Of SourceVtxBoneStateChange)(aStrip.boneStateChangeCount)
					aStrip.theVtxBoneStateChanges = new List<SourceVtxBoneStateChange07>(boneStateChangeCount);
					for (int j = 0; j < boneStateChangeCount; j++)
					{
						//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceVtxBoneStateChange07 aBoneStateChange = new SourceVtxBoneStateChange07();

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

		private void ReadSourceVtxMaterialReplacements(long materialReplacementListInputFileStreamPosition, SourceVtxMaterialReplacementList07 aMaterialReplacementList)
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

					aMaterialReplacementList.theVtxMaterialReplacements = new List<SourceVtxMaterialReplacement07>(aMaterialReplacementList.replacementCount);
					for (int j = 0; j < aMaterialReplacementList.replacementCount; j++)
					{
						materialReplacementInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVtxMaterialReplacement07 aMaterialReplacement = new SourceVtxMaterialReplacement07();

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
		private long theVtxFileOffsetStart;
		private SourceVtxFileData07 theVtxFileData;

		private SourceVtxMesh07 theFirstMeshWithStripGroups;
		private long theFirstMeshWithStripGroupsInputFileStreamPosition;
		private bool theModelLodHasOnlyOneMesh;
		private SourceVtxMesh07 theSecondMeshWithStripGroups;
		private long theExpectedStartOfDataAfterFirstStripGroupList;
		private long theStartOfStripGroupVtxStrips;
		private bool theStripGroupAndStripUseExtraFields;

#endregion

	}

}