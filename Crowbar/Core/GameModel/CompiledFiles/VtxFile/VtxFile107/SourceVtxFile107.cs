﻿//INSTANT C# NOTE: Formerly VB project-level imports:
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
	public class SourceVtxFile107
	{

#region Creation and Destruction

		public SourceVtxFile107(BinaryReader vtxFileReader, SourceVtxFileData107 vtxFileData)
		{
			theInputFileReader = vtxFileReader;
			theVtxFileData = vtxFileData;

			theVtxFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

#endregion

#region Methods

		public void ReadSourceVtxHeader()
		{
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theVtxFileData.version = theInputFileReader.ReadInt32();

			theVtxFileData.vertexCacheSize = theInputFileReader.ReadInt32();
			theVtxFileData.maxBonesPerStrip = theInputFileReader.ReadUInt16();
			theVtxFileData.maxBonesPerTri = theInputFileReader.ReadUInt16();
			theVtxFileData.maxBonesPerVertex = theInputFileReader.ReadInt32();

			theVtxFileData.checksum = theInputFileReader.ReadInt32();

			theVtxFileData.lodCount = theInputFileReader.ReadInt32();

			theVtxFileData.materialReplacementListOffset = theInputFileReader.ReadInt32();

			theVtxFileData.bodyPartCount = theInputFileReader.ReadInt32();
			theVtxFileData.bodyPartOffset = theInputFileReader.ReadInt32();

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VTX File Header (Actual version: " + theVtxFileData.version.ToString() + "; override version: 107)");
		}

		public void ReadSourceVtxBodyParts()
		{
			if (theVtxFileData.bodyPartCount > 0)
			{
				//'NOTE: Stuff that is part of determining vtx strip group size.
				//Me.theFirstMeshWithStripGroups = Nothing
				//Me.theFirstMeshWithStripGroupsInputFileStreamPosition = -1
				//Me.theSecondMeshWithStripGroups = Nothing
				//Me.theExpectedStartOfSecondStripGroupList = -1
				//Me.theStripGroupUsesExtra8Bytes = False

				long bodyPartInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theVtxFileData.bodyPartOffset, SeekOrigin.Begin);

					theVtxFileData.theVtxBodyParts = new List<SourceVtxBodyPart107>(theVtxFileData.bodyPartCount);
					for (int i = 0; i < theVtxFileData.bodyPartCount; i++)
					{
						bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceVtxBodyPart107 aBodyPart = new SourceVtxBodyPart107();

						aBodyPart.modelCount = theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = theInputFileReader.ReadInt32();

						theVtxFileData.theVtxBodyParts.Add(aBodyPart);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart");

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
		}

		public void ReadUnreadBytes()
		{
			theVtxFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

#endregion

#region Private Methods

		private void ReadSourceVtxModels(long bodyPartInputFileStreamPosition, SourceVtxBodyPart107 aBodyPart)
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

					aBodyPart.theVtxModels = new List<SourceVtxModel107>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceVtxModel107 aModel = new SourceVtxModel107();

						aModel.lodCount = theInputFileReader.ReadInt32();
						aModel.lodOffset = theInputFileReader.ReadInt32();

						aBodyPart.theVtxModels.Add(aModel);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxModelLods(long modelInputFileStreamPosition, SourceVtxModel107 aModel)
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

					aModel.theVtxModelLods = new List<SourceVtxModelLod107>(aModel.lodCount);
					for (int j = 0; j < aModel.lodCount; j++)
					{
						modelLodInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceVtxModelLod107 aModelLod = new SourceVtxModelLod107();

						aModelLod.meshCount = theInputFileReader.ReadInt32();
						aModelLod.meshOffset = theInputFileReader.ReadInt32();
						aModelLod.switchPoint = theInputFileReader.ReadSingle();

						aModel.theVtxModelLods.Add(aModelLod);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModelLod");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadSourceVtxMeshes(modelLodInputFileStreamPosition, aModelLod);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSourceVtxMeshes(long modelLodInputFileStreamPosition, SourceVtxModelLod107 aModelLod)
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

					aModelLod.theVtxMeshes = new List<SourceVtxMesh107>(aModelLod.meshCount);
					for (int j = 0; j < aModelLod.meshCount; j++)
					{
						meshInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceVtxMesh107 aMesh = new SourceVtxMesh107();

						//aMesh.stripGroupCount = Me.theInputFileReader.ReadInt32()
						//aMesh.stripGroupOffset = Me.theInputFileReader.ReadInt32()
						//'aMesh.flags = Me.theInputFileReader.ReadByte()
						//------
						aMesh.stripGroupCount = theInputFileReader.ReadInt16();
						aMesh.flags = theInputFileReader.ReadByte();
						aMesh.unknown = theInputFileReader.ReadByte();
						aMesh.stripGroupOffset = theInputFileReader.ReadInt32();

						aModelLod.theVtxMeshes.Add(aMesh);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aMesh.stripGroupCount > 0 && aMesh.stripGroupOffset != 0)
						{
							//If Me.theFirstMeshWithStripGroups Is Nothing Then
							//	Me.theFirstMeshWithStripGroups = aMesh
							//	Me.theFirstMeshWithStripGroupsInputFileStreamPosition = meshInputFileStreamPosition
							//	Me.AnalyzeVtxStripGroups(meshInputFileStreamPosition, aMesh)
							//	Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh)
							//ElseIf Me.theSecondMeshWithStripGroups Is Nothing Then
							//	Me.theSecondMeshWithStripGroups = aMesh
							//	If Me.theExpectedStartOfSecondStripGroupList <> (meshInputFileStreamPosition + aMesh.stripGroupOffset) Then
							//		Me.theStripGroupUsesExtra8Bytes = True

							//		If aMesh.theVtxStripGroups IsNot Nothing Then
							//			aMesh.theVtxStripGroups.Clear()
							//		End If

							//		Me.ReadSourceVtxStripGroups(Me.theFirstMeshWithStripGroupsInputFileStreamPosition, Me.theFirstMeshWithStripGroups)
							//	End If
							//	Me.ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh)
							//Else
							ReadSourceVtxStripGroups(meshInputFileStreamPosition, aMesh);
							//End If
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		//'TEST: / Save the first mesh that has strip groups and loop through the mesh's strip groups.
		//'      / Get the file offset and store as Me.theExpectedStartOfSecondStripGroupList.
		//'      / When the next strip group's offset is read in, compare with Me.theExpectedStartOfSecondStripGroupList.
		//'      If equal, then read from first mesh with strip groups without further checking.
		//'      Else (if unequal), then read from first mesh with strip groups 
		//'          and continue reading remaining data using larger strip group size.
		//'      WORKS for the SFM, Dota 2, and L4D2 models I tested.
		//Private Sub AnalyzeVtxStripGroups(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceVtxMesh107)
		//	Try
		//		Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.stripGroupOffset, SeekOrigin.Begin)
		//		aMesh.theVtxStripGroups = New List(Of SourceVtxStripGroup107)(aMesh.stripGroupCount)
		//		For j As Integer = 0 To aMesh.stripGroupCount - 1
		//			Dim aStripGroup As New SourceVtxStripGroup107()
		//			aStripGroup.vertexCount = Me.theInputFileReader.ReadInt32()
		//			aStripGroup.vertexOffset = Me.theInputFileReader.ReadInt32()
		//			aStripGroup.indexCount = Me.theInputFileReader.ReadInt32()
		//			aStripGroup.indexOffset = Me.theInputFileReader.ReadInt32()
		//			aStripGroup.stripCount = Me.theInputFileReader.ReadInt32()
		//			aStripGroup.stripOffset = Me.theInputFileReader.ReadInt32()
		//			aStripGroup.flags = Me.theInputFileReader.ReadByte()
		//		Next

		//		Me.theExpectedStartOfSecondStripGroupList = Me.theInputFileReader.BaseStream.Position
		//	Catch ex As Exception
		//		'NOTE: It can reach here if Crowbar is still trying to figure out if the extra 8 bytes are needed.
		//		Dim debug As Integer = 4242
		//	End Try
		//End Sub

		private void ReadSourceVtxStripGroups(long meshInputFileStreamPosition, SourceVtxMesh107 aMesh)
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

				aMesh.theVtxStripGroups = new List<SourceVtxStripGroup107>(aMesh.stripGroupCount);
				for (int j = 0; j < aMesh.stripGroupCount; j++)
				{
					stripGroupInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					fileOffsetStart = theInputFileReader.BaseStream.Position;
					SourceVtxStripGroup107 aStripGroup = new SourceVtxStripGroup107();

					aStripGroup.vertexCount = theInputFileReader.ReadInt16();
					aStripGroup.indexCount = theInputFileReader.ReadInt16();
					aStripGroup.stripCount = theInputFileReader.ReadInt16();
					aStripGroup.flags = theInputFileReader.ReadByte();
					aStripGroup.unknown = theInputFileReader.ReadByte();
					aStripGroup.vertexOffset = theInputFileReader.ReadInt32();
					aStripGroup.indexOffset = theInputFileReader.ReadInt32();
					aStripGroup.stripOffset = theInputFileReader.ReadInt32();

					aMesh.theVtxStripGroups.Add(aStripGroup);

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup");

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aStripGroup.vertexCount > 0 && aStripGroup.vertexOffset != 0)
					{
						if ((aStripGroup.flags & SourceVtxStripGroup107.STRIPGROUP_USES_STATIC_PROP_VERTEXES) > 0)
						{
							ReadSourceVtxVertexesForStaticProp(stripGroupInputFileStreamPosition, aStripGroup);
						}
						else
						{
							ReadSourceVtxVertexes(stripGroupInputFileStreamPosition, aStripGroup);
						}
					}
					if (aStripGroup.indexCount > 0 && aStripGroup.indexOffset != 0)
					{
						ReadSourceVtxIndexes(stripGroupInputFileStreamPosition, aStripGroup);
					}
					if (aStripGroup.stripCount > 0 && aStripGroup.stripOffset != 0)
					{
						ReadSourceVtxStrips(stripGroupInputFileStreamPosition, aStripGroup);
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadSourceVtxVertexesForStaticProp(long stripGroupInputFileStreamPosition, SourceVtxStripGroup107 aStripGroup)
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

				aStripGroup.theVtxVertexesForStaticProp = new List<UInt16>(aStripGroup.vertexCount);
				for (int j = 0; j < aStripGroup.vertexCount; j++)
				{
					//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					UInt16 aVertexMeshIndex = theInputFileReader.ReadUInt16();


					aStripGroup.theVtxVertexesForStaticProp.Add(aVertexMeshIndex);

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStripGroup.theVtxVertexesForStaticProp " + aStripGroup.theVtxVertexesForStaticProp.Count.ToString());
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadSourceVtxVertexes(long stripGroupInputFileStreamPosition, SourceVtxStripGroup107 aStripGroup)
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

				aStripGroup.theVtxVertexes = new List<SourceVtxVertex107>(aStripGroup.vertexCount);
				for (int j = 0; j < aStripGroup.vertexCount; j++)
				{
					//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					SourceVtxVertex107 aVertex = new SourceVtxVertex107();

					aVertex.unknown01 = theInputFileReader.ReadInt16();
					for (int x = 0; x < aVertex.boneIndex.Length; x++)
					{
						aVertex.boneIndex[x] = theInputFileReader.ReadInt16();
					}
					aVertex.unknown02 = theInputFileReader.ReadInt16();
					aVertex.originalMeshVertexIndex = theInputFileReader.ReadUInt16();

					aStripGroup.theVtxVertexes.Add(aVertex);

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

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

		private void ReadSourceVtxIndexes(long stripGroupInputFileStreamPosition, SourceVtxStripGroup107 aStripGroup)
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
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					ushort anIndex = theInputFileReader.ReadUInt16();


					aStripGroup.theVtxIndexes.Add(anIndex);

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIndex")

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//If aModel.lodCount > 0 Then
					//	Me.ReadSourceVtxModelLods(modelInputFileStreamPosition, aModel)
					//End If

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

		private void ReadSourceVtxStrips(long stripGroupInputFileStreamPosition, SourceVtxStripGroup107 aStripGroup)
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

				aStripGroup.theVtxStrips = new List<SourceVtxStrip107>(aStripGroup.stripCount);
				for (int j = 0; j < aStripGroup.stripCount; j++)
				{
					stripInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					SourceVtxStrip107 aStrip = new SourceVtxStrip107();

					aStrip.indexCount = theInputFileReader.ReadInt16();
					aStrip.indexMeshIndex = theInputFileReader.ReadInt16();
					aStrip.vertexCount = theInputFileReader.ReadInt16();
					aStrip.vertexMeshIndex = theInputFileReader.ReadInt16();

					aStrip.boneCount = theInputFileReader.ReadByte();
					aStrip.flags = theInputFileReader.ReadByte();
					aStrip.boneStateChangeCount = theInputFileReader.ReadInt16();
					aStrip.boneStateChangeOffset = theInputFileReader.ReadInt32();

					aStripGroup.theVtxStrips.Add(aStrip);

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theVtxFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aStrip")

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aStrip.boneStateChangeCount > 0 && aStrip.boneStateChangeOffset != 0)
					{
						ReadSourceVtxBoneStateChanges(stripInputFileStreamPosition, aStrip);
					}

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

		private void ReadSourceVtxBoneStateChanges(long stripInputFileStreamPosition, SourceVtxStrip107 aStrip)
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

				aStrip.theVtxBoneStateChanges = new List<SourceVtxBoneStateChange107>(aStrip.boneStateChangeCount);
				for (int j = 0; j < aStrip.boneStateChangeCount; j++)
				{
					//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					SourceVtxBoneStateChange107 aBoneStateChange = new SourceVtxBoneStateChange107();

					aBoneStateChange.hardwareId = theInputFileReader.ReadInt16();
					aBoneStateChange.newBoneId = theInputFileReader.ReadInt16();

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

#endregion

#region Data

		private BinaryReader theInputFileReader;
		private SourceVtxFileData107 theVtxFileData;

		//Private theFirstMeshWithStripGroups As SourceVtxMesh107
		//Private theFirstMeshWithStripGroupsInputFileStreamPosition As Long
		//Private theSecondMeshWithStripGroups As SourceVtxMesh107
		//Private theExpectedStartOfSecondStripGroupList As Long
		//Private theStripGroupUsesExtra8Bytes As Boolean

#endregion

	}

}