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
	public class SourceVtaFile52
	{

#region Creation and Destruction

		public SourceVtaFile52(StreamWriter outputFileStream, SourceMdlFileData52 mdlFileData, SourceVvdFileData04 vvdFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			theVvdFileData = vvdFileData;
		}

#endregion

#region Methods

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(theOutputFileStreamWriter);
		}

		public void WriteHeaderSection()
		{
			string line = "";

			//version 1
			line = "version 1";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteNodesSection()
		{
			string line = "";
			string name = null;

			//nodes
			line = "nodes";
			theOutputFileStreamWriter.WriteLine(line);

			for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
			{
				name = theMdlFileData.theBones[boneIndex].theName;

				line = "  ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " \"";
				line += name;
				line += "\" ";
				line += theMdlFileData.theBones[boneIndex].parentBoneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSkeletonSectionForVertexAnimation()
		{
			string line = "";
			//Dim timeIndex As Integer
			//Dim flexDescHasBeenWritten As List(Of Integer)
			//Dim meshVertexIndexStart As Integer

			//skeleton
			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			//line = "time 0"
			//Me.theOutputFileStreamWriter.WriteLine(line)
			//line = "time 1"
			//Me.theOutputFileStreamWriter.WriteLine(line)
			//======
			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0 # basis shape key";
			}
			else
			{
				line = "  time 0 # basis shape key";
			}
			theOutputFileStreamWriter.WriteLine(line);

			//timeIndex = 0
			//flexDescHasBeenWritten = New List(Of Integer)
			//If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
			//	For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
			//		Dim aBodyPart As SourceMdlBodyPart
			//		aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

			//		If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
			//			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
			//				Dim aModel As SourceMdlModel
			//				aModel = aBodyPart.theModels(modelIndex)

			//				If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
			//					For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
			//						Dim aMesh As SourceMdlMesh
			//						aMesh = aModel.theMeshes(meshIndex)

			//						If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
			//							For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
			//								Dim aFlex As SourceMdlFlex
			//								aFlex = aMesh.theFlexes(flexIndex)

			//								If flexDescHasBeenWritten.Contains(aFlex.flexDescIndex) Then
			//									Continue For
			//								Else
			//									flexDescHasBeenWritten.Add(aFlex.flexDescIndex)
			//								End If

			//								timeIndex += 1
			//								line = "time "
			//								line += timeIndex.ToString()
			//								line += " # "
			//								line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName

			//								Dim aFlexDescPartnerIndex As Integer
			//								aFlexDescPartnerIndex = aFlex.flexDescPartnerIndex
			//								If aFlexDescPartnerIndex > 0 AndAlso Not flexDescHasBeenWritten.Contains(aFlexDescPartnerIndex) Then
			//									flexDescHasBeenWritten.Add(aFlexDescPartnerIndex)
			//									line += " and "
			//									line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexDescPartnerIndex).theName
			//								End If

			//								Me.theOutputFileStreamWriter.WriteLine(line)
			//							Next
			//						End If
			//					Next
			//				End If
			//			Next
			//		End If
			//	Next
			//End If

			//======

			//Dim aFlexTimeStruct As FlexTimeStruct
			//Dim bodyPartVertexIndexStart As Integer
			//Dim flexDescIndexesAlreadyAdded As List(Of Integer)

			//bodyPartVertexIndexStart = 0
			//flexTimes = New List(Of FlexTimeStruct)()
			//flexDescIndexesAlreadyAdded = New List(Of Integer)()

			//If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
			//	For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
			//		Dim aBodyPart As SourceMdlBodyPart
			//		aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

			//		If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
			//			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
			//				Dim aModel As SourceMdlModel
			//				aModel = aBodyPart.theModels(modelIndex)

			//				If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
			//					For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
			//						Dim aMesh As SourceMdlMesh
			//						aMesh = aModel.theMeshes(meshIndex)

			//						meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

			//						If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
			//							For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
			//								Dim aFlex As SourceMdlFlex
			//								aFlex = aMesh.theFlexes(flexIndex)

			//								If aFlex.theVertAnims IsNot Nothing AndAlso aFlex.theVertAnims.Count > 0 Then
			//									'If flexDescIndexesAlreadyAdded.Contains(aFlex.flexDescIndex) Then
			//									'	aFlexTimeStruct = flexTimes(flexDescIndexesAlreadyAdded.IndexOf(aFlex.flexDescIndex))

			//									'	'If aFlex.flexDescPartnerIndex = 0 Then
			//									'	aFlexTimeStruct.flexDescriptiveName += " and "
			//									'	aFlexTimeStruct.flexDescriptiveName += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			//									'	'End If

			//									'	aFlexTimeStruct.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart)
			//									'	aFlexTimeStruct.flexes.Add(aFlex)
			//									'Else
			//									aFlexTimeStruct = New FlexTimeStruct()
			//									aFlexTimeStruct.bodyAndMeshVertexIndexStarts = New List(Of Integer)()
			//									aFlexTimeStruct.flexes = New List(Of SourceMdlFlex)()

			//									aFlexTimeStruct.flexDescriptiveName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			//									If aFlex.flexDescPartnerIndex > 0 Then
			//										aFlexTimeStruct.flexDescriptiveName += "+"
			//										aFlexTimeStruct.flexDescriptiveName += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescPartnerIndex).theName
			//									End If
			//									aFlexTimeStruct.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart)
			//									aFlexTimeStruct.flexes.Add(aFlex)

			//									flexTimes.Add(aFlexTimeStruct)
			//									flexDescIndexesAlreadyAdded.Add(aFlex.flexDescIndex)
			//									'End If
			//								End If
			//							Next
			//						End If
			//					Next
			//				End If
			//				bodyPartVertexIndexStart += aModel.vertexCount
			//			Next
			//		End If
			//	Next
			//End If

			//Dim timeIndex As Integer
			//Dim flexTimeIndex As Integer
			//timeIndex = 1
			//For flexTimeIndex = 0 To flexTimes.Count - 1
			//	aFlexTimeStruct = flexTimes(flexTimeIndex)

			//	line = "  time "
			//	line += timeIndex.ToString()
			//	line += " # "
			//	line += aFlexTimeStruct.flexDescriptiveName
			//	Me.theOutputFileStreamWriter.WriteLine(line)

			//	timeIndex += 1
			//Next

			//'For flexIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theFlexDescs.Count - 1
			//'	line = "time "
			//'	line += flexIndex.ToString()
			//'	line += " # "
			//'	line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(flexIndex).theName
			//'	Me.theOutputFileStreamWriter.WriteLine(line)
			//'Next

			//======

			int timeIndex = 0;
			int flexTimeIndex = 0;
			FlexFrame aFlexFrame = null;

			timeIndex = 1;
			//NOTE: The first frame was written in code above.
			for (flexTimeIndex = 1; flexTimeIndex < theMdlFileData.theFlexFrames.Count; flexTimeIndex++)
			{
				aFlexFrame = theMdlFileData.theFlexFrames[flexTimeIndex];

				if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
				{
					line = "time ";
				}
				else
				{
					line = "  time ";
				}
				line += timeIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " # ";
				line += aFlexFrame.flexDescription;
				theOutputFileStreamWriter.WriteLine(line);

				timeIndex += 1;
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteVertexAnimationSection()
		{
			string line = "";
			//Dim aVtxBodyPart As SourceVtxBodyPart
			//Dim aVtxModel As SourceVtxModel
			//Dim aVtxLod As SourceVtxModelLod
			//Dim aVtxMesh As SourceVtxMesh
			//Dim aVtxStripGroup As SourceVtxStripGroup
			//Dim cumulativeVertexCount As Integer
			//Dim maxIndexForMesh As Integer
			//Dim cumulativeMaxIndex As Integer
			//Dim meshVertexIndexStart As Integer
			//Dim vertexIndex As Integer
			//Dim mappedVertexIndex As Integer
			//Dim aVertex As SourceVertex
			//Dim positionX As Double
			//Dim positionY As Double
			//Dim positionZ As Double
			//Dim normalX As Double
			//Dim normalY As Double
			//Dim normalZ As Double

			//vertexanimation
			line = "vertexanimation";
			theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0 # basis shape key";
			}
			else
			{
				line = "  time 0 # basis shape key";
			}
			theOutputFileStreamWriter.WriteLine(line);

			//Try
			//	If Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts IsNot Nothing Then
			//		vertexIndex = 0
			//		For vtxBodyPartIndex As Integer = 0 To Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts.Count - 1
			//			aVtxBodyPart = Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(vtxBodyPartIndex)

			//			If aVtxBodyPart.theVtxModels IsNot Nothing Then
			//				For vtxModelIndex As Integer = 0 To aVtxBodyPart.theVtxModels.Count - 1
			//					aVtxModel = aVtxBodyPart.theVtxModels(vtxModelIndex)

			//					If aVtxModel.theVtxModelLods IsNot Nothing Then
			//						''For lodIndex As Integer = 0 To aModel.theVtxModelLods.Count - 1
			//						Dim vtxLodIndex As Integer = 0
			//						aVtxLod = aVtxModel.theVtxModelLods(vtxLodIndex)

			//						If aVtxLod.theVtxMeshes IsNot Nothing Then
			//							cumulativeVertexCount = 0
			//							maxIndexForMesh = 0
			//							cumulativeMaxIndex = 0
			//							For vtxMeshIndex As Integer = 0 To aVtxLod.theVtxMeshes.Count - 1
			//								aVtxMesh = aVtxLod.theVtxMeshes(vtxMeshIndex)

			//								meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(vtxBodyPartIndex).theModels(vtxModelIndex).theMeshes(vtxMeshIndex).vertexIndexStart

			//								If aVtxMesh.theVtxStripGroups IsNot Nothing Then
			//									For vtxStripGroupIndex As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
			//										aVtxStripGroup = aVtxMesh.theVtxStripGroups(vtxStripGroupIndex)

			//										If aVtxStripGroup.theVtxStrips IsNot Nothing AndAlso aVtxStripGroup.theVtxIndexes IsNot Nothing Then
			//											For vtxIndexIndex As Integer = 0 To aVtxStripGroup.theVtxIndexes.Count - 3 Step 3
			//												Me.WriteBasisVertexAnimLine(aVtxStripGroup, vtxIndexIndex, vertexIndex, meshVertexIndexStart)
			//												vertexIndex += 1
			//												Me.WriteBasisVertexAnimLine(aVtxStripGroup, vtxIndexIndex + 2, vertexIndex, meshVertexIndexStart)
			//												vertexIndex += 1
			//												Me.WriteBasisVertexAnimLine(aVtxStripGroup, vtxIndexIndex + 1, vertexIndex, meshVertexIndexStart)
			//												vertexIndex += 1
			//											Next
			//											'======
			//											'For stripIndex As Integer = 0 To aStripGroup.stripCount - 1
			//											'	Dim aStrip As SourceVtxStrip = aStripGroup.theVtxStrips(stripIndex)

			//											'	For aStripIndexIndex As Integer = 0 To aStrip.indexCount - 3 Step 3
			//											'		Me.theOutputFileStreamWriter.WriteLine(materialName)
			//											'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex)
			//											'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex + 2)
			//											'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex + 1)
			//											'	Next
			//											'Next
			//										End If
			//									Next
			//								End If
			//							Next
			//						End If
			//						'Next
			//					End If
			//				Next
			//			End If
			//		Next
			//	End If
			//Catch
			//End Try

			//======

			try
			{
				SourceVertex aVertex = null;
				for (int vertexIndex = 0; vertexIndex < theVvdFileData.theVertexes.Count; vertexIndex++)
				{
					if (theVvdFileData.fixupCount == 0)
					{
						aVertex = theVvdFileData.theVertexes[vertexIndex];
					}
					else
					{
						//NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
						//      Maybe the listing by lodIndex is only needed internally by graphics engine.
						aVertex = theVvdFileData.theFixedVertexesByLod[0][vertexIndex];
					}

					line = "    ";
					line += vertexIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.positionX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.positionY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.positionZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normalX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normalY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normalZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
			catch
			{
			}

			//######

			//Dim flexTimes As List(Of FlexTimeStruct)
			//Dim aFlexTimeStruct As FlexTimeStruct
			//Dim aFlexPartnerIndex As Integer

			//flexTimes = New List(Of FlexTimeStruct)()
			//For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theFlexDescs.Count - 1
			//	aFlexTimeStruct = New FlexTimeStruct()
			//	'aFlexTimeStruct.isValid = False
			//	aFlexTimeStruct.meshVertexIndexStarts = New List(Of Integer)()
			//	aFlexTimeStruct.flexes = New List(Of SourceMdlFlex)()
			//	flexTimes.Add(aFlexTimeStruct)
			//Next

			//flexTimeIndex = 0
			//If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
			//	For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
			//		Dim aBodyPart As SourceMdlBodyPart
			//		aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)
			//		'Dim aVtxBodyPart As SourceVtxBodyPart
			//		'aVtxBodyPart = TheSourceEngineModel.theVtxFileHeader.theVtxBodyParts(bodyPartIndex)

			//		If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
			//			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
			//				Dim aModel As SourceMdlModel
			//				aModel = aBodyPart.theModels(modelIndex)
			//				'Dim aVtxModel As SourceVtxModel
			//				'aVtxModel = aVtxBodyPart.theVtxModels(modelIndex)

			//				If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
			//					For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
			//						Dim aMesh As SourceMdlMesh
			//						aMesh = aModel.theMeshes(meshIndex)
			//						'Dim aVtxMesh As SourceVtxMesh
			//						'aVtxMesh = aVtxModel.theVtxModelLods(0).theVtxMeshes(meshIndex)

			//						meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

			//						If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
			//							For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
			//								Dim aFlex As SourceMdlFlex
			//								aFlex = aMesh.theFlexes(flexIndex)

			//								If aFlex.theVertAnims IsNot Nothing AndAlso aFlex.theVertAnims.Count > 0 Then
			//									aFlexTimeStruct = flexTimes(aFlex.flexDescIndex)
			//									aFlexPartnerIndex = aFlex.flexDescPartnerIndex
			//									'If Not aFlexTimeStruct.isValid Then
			//									If aFlexTimeStruct.flexes.Count > 0 Then
			//										'NOTE: More than one flex can point to same flexDesc.
			//										'      This might only occur for eyelids.
			//										If aFlexPartnerIndex = 0 Then
			//											aFlexTimeStruct.flexDescriptiveName += " and "
			//											aFlexTimeStruct.flexDescriptiveName += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			//										End If
			//									Else
			//										'aFlexTimeStruct.isValid = True
			//										aFlexTimeStruct.flexDescriptiveName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			//									End If
			//									aFlexTimeStruct.meshVertexIndexStarts.Add(meshVertexIndexStart)
			//									aFlexTimeStruct.flexes.Add(aFlex)

			//									'If aFlexPartnerIndex > 0 AndAlso aFlexTimeStruct.isValid Then
			//									If aFlexPartnerIndex > 0 AndAlso aFlexTimeStruct.flexes.Count > 0 Then
			//										'NOTE: A partner flex should be added to same flexTimeStruct.
			//										aFlexTimeStruct = flexTimes(aFlex.flexDescIndex)
			//										'If aFlexTimeStruct.flexes.Count < 2 Then
			//										aFlexTimeStruct.flexDescriptiveName += " and "
			//										aFlexTimeStruct.flexDescriptiveName += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexPartnerIndex).theName
			//										'End If
			//										aFlexTimeStruct.meshVertexIndexStarts.Add(meshVertexIndexStart)
			//										aFlexTimeStruct.flexes.Add(aFlex)
			//									End If
			//								End If
			//							Next
			//						End If
			//					Next
			//				End If
			//			Next
			//		End If
			//	Next
			//End If

			//Dim timeIndex As Integer
			//timeIndex = 1
			//For flexTimeIndex = 0 To flexTimes.Count - 1
			//	aFlexTimeStruct = flexTimes(flexTimeIndex)

			//	'If aFlexTimeStruct.isValid Then
			//	If aFlexTimeStruct.flexes.Count > 0 Then
			//		line = "time "
			//		line += timeIndex.ToString()
			//		line += " # "
			//		line += aFlexTimeStruct.flexDescriptiveName
			//		Me.theOutputFileStreamWriter.WriteLine(line)

			//		For x As Integer = 0 To aFlexTimeStruct.flexes.Count - 1
			//			Me.WriteVertexAnimLines(aFlexTimeStruct.flexes(x), aFlexTimeStruct.meshVertexIndexStarts(x))
			//		Next

			//		timeIndex += 1
			//	End If
			//Next

			//======

			//Dim flexTimes As List(Of FlexTimeStruct)
			//Dim aFlexTimeStruct As FlexTimeStruct
			//Dim bodyPartVertexIndexStart As Integer

			//bodyPartVertexIndexStart = 0
			//flexTimes = New List(Of FlexTimeStruct)()

			//If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
			//	For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
			//		Dim aBodyPart As SourceMdlBodyPart
			//		aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

			//		If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
			//			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
			//				Dim aModel As SourceMdlModel
			//				aModel = aBodyPart.theModels(modelIndex)

			//				If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
			//					For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
			//						Dim aMesh As SourceMdlMesh
			//						aMesh = aModel.theMeshes(meshIndex)

			//						meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

			//						If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
			//							For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
			//								Dim aFlex As SourceMdlFlex
			//								aFlex = aMesh.theFlexes(flexIndex)

			//								If aFlex.theVertAnims IsNot Nothing AndAlso aFlex.theVertAnims.Count > 0 Then
			//									aFlexTimeStruct = New FlexTimeStruct()

			//									aFlexTimeStruct.flexDescriptiveName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			//									aFlexTimeStruct.bodyAndMeshVertexIndexStart = meshVertexIndexStart
			//									aFlexTimeStruct.flex = aFlex

			//									flexTimes.Add(aFlexTimeStruct)
			//								End If
			//							Next
			//						End If
			//					Next
			//				End If
			//				bodyPartVertexIndexStart += aModel.vertexCount
			//			Next
			//		End If
			//	Next
			//End If

			//Dim timeIndex As Integer
			//timeIndex = 1
			//For flexTimeIndex = 0 To flexTimes.Count - 1
			//	aFlexTimeStruct = flexTimes(flexTimeIndex)

			//	line = "time "
			//	line += timeIndex.ToString()
			//	line += " # "
			//	line += aFlexTimeStruct.flexDescriptiveName
			//	Me.theOutputFileStreamWriter.WriteLine(line)

			//	Me.WriteVertexAnimLines(aFlexTimeStruct.flex, aFlexTimeStruct.bodyAndMeshVertexIndexStart)

			//	timeIndex += 1
			//Next

			//======

			//Dim timeIndex As Integer
			//Dim flexTimeIndex As Integer
			//Dim aFlexTimeStruct As FlexTimeStruct

			//timeIndex = 1
			//For flexTimeIndex = 0 To flexTimes.Count - 1
			//	aFlexTimeStruct = flexTimes(flexTimeIndex)

			//	line = "  time "
			//	line += timeIndex.ToString()
			//	line += " # "
			//	line += aFlexTimeStruct.flexDescriptiveName
			//	Me.theOutputFileStreamWriter.WriteLine(line)

			//	For x As Integer = 0 To aFlexTimeStruct.flexes.Count - 1
			//		Me.WriteVertexAnimLines(aFlexTimeStruct.flexes(x), aFlexTimeStruct.bodyAndMeshVertexIndexStarts(x))
			//	Next

			//	timeIndex += 1
			//Next

			//======

			int timeIndex = 0;
			int flexTimeIndex = 0;
			FlexFrame aFlexFrame = null;

			timeIndex = 1;
			//NOTE: The first frame was written in code above.
			for (flexTimeIndex = 1; flexTimeIndex < theMdlFileData.theFlexFrames.Count; flexTimeIndex++)
			{
				aFlexFrame = theMdlFileData.theFlexFrames[flexTimeIndex];

				if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
				{
					line = "time ";
				}
				else
				{
					line = "  time ";
				}
				line += timeIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " # ";
				line += aFlexFrame.flexDescription;
				theOutputFileStreamWriter.WriteLine(line);

				for (int x = 0; x < aFlexFrame.flexes.Count; x++)
				{
					WriteVertexAnimLines(aFlexFrame.flexes[x], aFlexFrame.bodyAndMeshVertexIndexStarts[x]);
				}

				timeIndex += 1;
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		//Private Sub WriteBasisVertexAnimLine(ByVal aStripGroup As SourceVtxStripGroup, ByVal aVtxIndexIndex As Integer, ByVal vertexIndex As Integer, ByVal meshVertexIndexStart As Integer)
		//	Dim aVtxVertexIndex As UShort
		//	Dim aVtxVertex As SourceVtxVertex
		//	Dim aVertex As SourceVertex
		//	Dim line As String

		//	Try
		//		aVtxVertexIndex = aStripGroup.theVtxIndexes(aVtxIndexIndex)
		//		aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)
		//		If Me.theSourceEngineModel.theVvdFileHeader.fixupCount = 0 Then
		//			aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
		//			'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(vertexIndex)
		//		Else
		//			'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
		//			'      Maybe the listing by lodIndex is only needed internally by graphics engine.
		//			'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
		//			aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
		//			'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(vertexIndex)
		//		End If

		//		''NOTE: Add the vertex to a list to be accessed by index for subsequent VTA anim writing.
		//		'Me.theVtaVertexes.Add(aVertex)

		//		line = vertexIndex.ToString()
		//		line += " "
		//		line += aVertex.positionX.ToString("0.000000", TheApp.InternalNumberFormat)
		//		line += " "
		//		line += aVertex.positionY.ToString("0.000000", TheApp.InternalNumberFormat)
		//		line += " "
		//		line += aVertex.positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
		//		line += " "
		//		line += aVertex.normalX.ToString("0.000000", TheApp.InternalNumberFormat)
		//		line += " "
		//		line += aVertex.normalY.ToString("0.000000", TheApp.InternalNumberFormat)
		//		line += " "
		//		line += aVertex.normalZ.ToString("0.000000", TheApp.InternalNumberFormat)
		//		Me.theOutputFileStreamWriter.WriteLine(line)
		//	Catch
		//	End Try
		//End Sub

		private void WriteVertexAnimLines(SourceMdlFlex aFlex, int bodyAndMeshVertexIndexStart)
		{
			string line = null;
			SourceVertex aVertex = null;
			int vertexIndex = 0;
			//Dim mappedVertexIndex As Integer
			double positionX = 0;
			double positionY = 0;
			double positionZ = 0;
			double normalX = 0;
			double normalY = 0;
			double normalZ = 0;

			for (int i = 0; i < aFlex.theVertAnims.Count; i++)
			{
				SourceMdlVertAnim aVertAnim = aFlex.theVertAnims[i];

				//TODO: Figure out why decompiling teen angst zoey (which has 39 shape keys) gives 55 shapekeys.
				//      - Probably extra ones are related to flexpairs (right and left).
				//      - Eyelids are combined, e.g. second shapekey from source vta is upper_lid_lowerer
				//        that contains both upper_right_lowerer and upper_left_lowerer.

				//NOTE: Figure out which list of vertexes to index.
				//aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(aVertAnim.index)
				//vertexIndex = aVertAnim.index
				//======
				//aVertex = Me.theVtaVertexes(aVertAnim.index)
				//vertexIndex = aVertAnim.index
				//======
				//NOTE: This list of vertexes works; it imports into Blender correctly.
				vertexIndex = aVertAnim.index + bodyAndMeshVertexIndexStart;
				//vertexIndex = aVertAnim.index
				if (theVvdFileData.fixupCount == 0)
				{
					aVertex = theVvdFileData.theVertexes[vertexIndex];
				}
				else
				{
					//NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
					//      Maybe the listing by lodIndex is only needed internally by graphics engine.
					//aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
					aVertex = theVvdFileData.theFixedVertexesByLod[0][vertexIndex];
				}
				//mappedVertexIndex = Me.theVtaVertexes.IndexOf(aVertex)
				//If mappedVertexIndex < 0 Then
				//	mappedVertexIndex = 0
				//End If

				positionX = aVertex.positionX + aVertAnim.flDelta(0).TheFloatValue;
				positionY = aVertex.positionY + aVertAnim.flDelta(1).TheFloatValue;
				positionZ = aVertex.positionZ + aVertAnim.flDelta(2).TheFloatValue;
				normalX = aVertex.normalX + aVertAnim.flNDelta(0).TheFloatValue;
				normalY = aVertex.normalY + aVertAnim.flNDelta(1).TheFloatValue;
				normalZ = aVertex.normalZ + aVertAnim.flNDelta(2).TheFloatValue;
				//NOTE: This matches values given by MDL Decompiler 0.5.
				//line = aVertAnim.index.ToString()
				line = "    ";
				line += vertexIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				//line = mappedVertexIndex.ToString()
				line += " ";
				line += positionX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += positionY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += positionZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normalX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normalY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normalZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				//TEST:
				//If aFlex.vertAnimType = aFlex.STUDIO_VERT_ANIM_WRINKLE Then
				//	CType(aVertAnim, SourceMdlVertAnimWrinkle).wrinkleDelta = Me.theInputFileReader.ReadInt16()
				//End If
				//If blah Then
				//	line += " // wrinkle value: "
				//	line += aVertAnim.flDelta(0).the16BitValue.ToString()
				//End If

				// For debugging.
				//line += " // "
				//line += aVertAnim.flDelta(0).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flDelta(1).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flDelta(2).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flNDelta(0).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flNDelta(1).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flNDelta(2).the16BitValue.ToString()

				theOutputFileStreamWriter.WriteLine(line);
			}
		}

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData52 theMdlFileData;
		private SourceVvdFileData04 theVvdFileData;

#endregion

	}

}