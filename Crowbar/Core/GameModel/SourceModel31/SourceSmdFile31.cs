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
	public class SourceSmdFile31
	{

#region Creation and Destruction

		public SourceSmdFile31(StreamWriter outputFileStream, SourceMdlFileData31 mdlFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
		}

		public SourceSmdFile31(StreamWriter outputFileStream, SourceMdlFileData31 mdlFileData, SourcePhyFileData phyFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			thePhyFileData = phyFileData;
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

		public void WriteNodesSection(int lodIndex)
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

		public void WriteSkeletonSection(int lodIndex)
		{
			string line = "";

			//skeleton
			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0";
			}
			else
			{
				line = "  time 0";
			}
			theOutputFileStreamWriter.WriteLine(line);
			for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
			{
				line = "    ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += theMdlFileData.theBones[boneIndex].position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += theMdlFileData.theBones[boneIndex].position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += theMdlFileData.theBones[boneIndex].position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += theMdlFileData.theBones[boneIndex].rotation.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += theMdlFileData.theBones[boneIndex].rotation.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += theMdlFileData.theBones[boneIndex].rotation.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSection(int lodIndex, SourceVtxModel06 aVtxModel, SourceMdlModel31 aModel, int bodyPartVertexIndexStart)
		{
			string line = "";
			string materialLine = "";
			string vertex1Line = "";
			string vertex2Line = "";
			string vertex3Line = "";

			//triangles
			line = "triangles";
			theOutputFileStreamWriter.WriteLine(line);

			SourceVtxModelLod06 aVtxLod = null;
			SourceVtxMesh06 aVtxMesh = null;
			SourceVtxStripGroup06 aStripGroup = null;
			//Dim cumulativeVertexCount As Integer
			//Dim maxIndexForMesh As Integer
			//Dim cumulativeMaxIndex As Integer
			int materialIndex = 0;
			string materialName = null;
			int meshVertexIndexStart = 0;

			try
			{
				aVtxLod = aVtxModel.theVtxModelLods[lodIndex];

				if (aVtxLod.theVtxMeshes != null)
				{
					//cumulativeVertexCount = 0
					//maxIndexForMesh = 0
					//cumulativeMaxIndex = 0
					for (int meshIndex = 0; meshIndex < aVtxLod.theVtxMeshes.Count; meshIndex++)
					{
						aVtxMesh = aVtxLod.theVtxMeshes[meshIndex];
						materialIndex = aModel.theMeshes[meshIndex].materialIndex;
						materialName = theMdlFileData.theTextures[materialIndex].thePathFileName;
						//TODO: This was used in previous versions, but maybe should leave as above.
						//materialName = Path.GetFileName(Me.theSourceEngineModel.theMdlFileHeader.theTextures(materialIndex).theName)

						meshVertexIndexStart = aModel.theMeshes[meshIndex].vertexIndexStart;

						if (aVtxMesh.theVtxStripGroups != null)
						{
							for (int groupIndex = 0; groupIndex < aVtxMesh.theVtxStripGroups.Count; groupIndex++)
							{
								aStripGroup = aVtxMesh.theVtxStripGroups[groupIndex];

								if (aStripGroup.theVtxStrips != null && aStripGroup.theVtxIndexes != null && aStripGroup.theVtxVertexes != null)
								{
									for (int vtxIndexIndex = 0; vtxIndexIndex <= aStripGroup.theVtxIndexes.Count - 3; vtxIndexIndex += 3)
									{
										//'NOTE: studiomdl.exe will complain if texture name for eyeball is not at start of line.
										//line = materialName
										//Me.theOutputFileStreamWriter.WriteLine(line)
										//Me.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
										//Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
										//Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
										//------
										//NOTE: studiomdl.exe will complain if texture name for eyeball is not at start of line.
										materialLine = materialName;
										vertex1Line = WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
										vertex2Line = WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
										vertex3Line = WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
										if (vertex1Line.StartsWith("// ") || vertex2Line.StartsWith("// ") || vertex3Line.StartsWith("// "))
										{
											materialLine = "// " + materialLine;
											if (!vertex1Line.StartsWith("// "))
											{
												vertex1Line = "// " + vertex1Line;
											}
											if (!vertex2Line.StartsWith("// "))
											{
												vertex2Line = "// " + vertex2Line;
											}
											if (!vertex3Line.StartsWith("// "))
											{
												vertex3Line = "// " + vertex3Line;
											}
										}
										theOutputFileStreamWriter.WriteLine(materialLine);
										theOutputFileStreamWriter.WriteLine(vertex1Line);
										theOutputFileStreamWriter.WriteLine(vertex2Line);
										theOutputFileStreamWriter.WriteLine(vertex3Line);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSectionForPhysics()
		{
			string line = "";

			//triangles
			line = "triangles";
			theOutputFileStreamWriter.WriteLine(line);

			SourcePhyCollisionData collisionData = null;
			SourceMdlBone31 aBone = null;
			int boneIndex = 0;
			SourcePhyFace aTriangle = null;
			SourcePhyFaceSection faceSection = null;
			SourcePhyVertex phyVertex = null;
			SourceVector aVectorTransformed = null;
			SourcePhyPhysCollisionModel aSourcePhysCollisionModel = null;

			try
			{
				if (thePhyFileData.theSourcePhyCollisionDatas != null)
				{
					for (int collisionDataIndex = 0; collisionDataIndex < thePhyFileData.theSourcePhyCollisionDatas.Count; collisionDataIndex++)
					{
						collisionData = thePhyFileData.theSourcePhyCollisionDatas[collisionDataIndex];

						if (collisionDataIndex < thePhyFileData.theSourcePhyPhysCollisionModels.Count)
						{
							aSourcePhysCollisionModel = thePhyFileData.theSourcePhyPhysCollisionModels[collisionDataIndex];
						}
						else
						{
							aSourcePhysCollisionModel = null;
						}

						for (int faceSectionIndex = 0; faceSectionIndex < collisionData.theFaceSections.Count; faceSectionIndex++)
						{
							faceSection = collisionData.theFaceSections[faceSectionIndex];

							if (faceSection.theBoneIndex >= theMdlFileData.theBones.Count)
							{
								continue;
							}
							if (aSourcePhysCollisionModel != null && theMdlFileData.theBoneNameToBoneIndexMap.ContainsKey(aSourcePhysCollisionModel.theName))
							{
								boneIndex = theMdlFileData.theBoneNameToBoneIndexMap[aSourcePhysCollisionModel.theName];
							}
							else
							{
								boneIndex = faceSection.theBoneIndex;
							}
							aBone = theMdlFileData.theBones[faceSection.theBoneIndex];

							for (int triangleIndex = 0; triangleIndex < faceSection.theFaces.Count; triangleIndex++)
							{
								aTriangle = faceSection.theFaces[triangleIndex];

								line = "  phy";
								theOutputFileStreamWriter.WriteLine(line);

								//  19 -0.000009 0.000001 0.999953 0.0 0.0 0.0 1 0
								//  19 -0.000005 1.000002 -0.000043 0.0 0.0 0.0 1 0
								//  19 -0.008333 0.997005 1.003710 0.0 0.0 0.0 1 0
								//NOTE: MDL Decompiler 0.4.1 lists the vertices in reverse order than they are stored, and this seems to match closely with the teenangst source file.
								//For vertexIndex As Integer = aTriangle.vertexIndex.Length - 1 To 0 Step -1
								for (int vertexIndex = 0; vertexIndex < aTriangle.vertexIndex.Length; vertexIndex++)
								{
									phyVertex = faceSection.theVertices[aTriangle.vertexIndex[vertexIndex]];

									aVectorTransformed = TransformPhyVertex(aBone, phyVertex.vertex, aSourcePhysCollisionModel);

									line = "    ";
									line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
									line += " ";
									line += aVectorTransformed.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
									line += " ";
									line += aVectorTransformed.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
									line += " ";
									line += aVectorTransformed.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

									//line += " 0 0 0"
									//------
									line += " ";
									line += phyVertex.Normal.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
									line += " ";
									line += phyVertex.Normal.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
									line += " ";
									line += phyVertex.Normal.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

									line += " 0 0";
									//NOTE: The studiomdl.exe doesn't need the integer values at end.
									//line += " 1 0"
									theOutputFileStreamWriter.WriteLine(line);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		//'TODO: Write the firstAnimDesc's first frame's frameLines because it is used for "subtract" option.
		//Public Sub CalculateFirstAnimDescFrameLinesForSubtract()
		//	Dim boneIndex As Integer
		//	Dim aFrameLine As AnimationFrameLine
		//	Dim frameIndex As Integer
		//	Dim aSequenceDesc As SourceMdlSequenceDesc31
		//	Dim anAnimationDesc As SourceMdlAnimationDesc31

		//	aSequenceDesc = Nothing
		//	anAnimationDesc = Me.theMdlFileData.theFirstAnimationDesc

		//	Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		//	frameIndex = 0
		//	Me.theAnimationFrameLines.Clear()
		//	If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
		//		Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
		//	End If

		//	For i As Integer = 0 To Me.theAnimationFrameLines.Count - 1
		//		boneIndex = Me.theAnimationFrameLines.Keys(i)
		//		aFrameLine = Me.theAnimationFrameLines.Values(i)

		//		Dim aFirstAnimationDescFrameLine As New AnimationFrameLine()
		//		aFirstAnimationDescFrameLine.rotation = New SourceVector()
		//		aFirstAnimationDescFrameLine.position = New SourceVector()

		//		'NOTE: Only rotate by -90 deg if bone is a root bone.  Do not know why.
		//		'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
		//		'TEST: Try this version, because of "sequence_blend from Game Zombie" model.
		//		aFirstAnimationDescFrameLine.rotation.x = aFrameLine.rotation.x
		//		aFirstAnimationDescFrameLine.rotation.y = aFrameLine.rotation.y
		//		If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.rotation.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
		//			Dim z As Double
		//			z = aFrameLine.rotation.z
		//			z += MathModule.DegreesToRadians(-90)
		//			aFirstAnimationDescFrameLine.rotation.z = z
		//		Else
		//			aFirstAnimationDescFrameLine.rotation.z = aFrameLine.rotation.z
		//		End If

		//		'NOTE: Only adjust position if bone is a root bone. Do not know why.
		//		'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
		//		'TEST: Try this version, because of "sequence_blend from Game Zombie" model.
		//		If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
		//			aFirstAnimationDescFrameLine.position.x = aFrameLine.position.y
		//			aFirstAnimationDescFrameLine.position.y = (-aFrameLine.position.x)
		//			aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
		//		Else
		//			aFirstAnimationDescFrameLine.position.x = aFrameLine.position.x
		//			aFirstAnimationDescFrameLine.position.y = aFrameLine.position.y
		//			aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
		//		End If

		//		Me.theMdlFileData.theFirstAnimationDescFrameLines.Add(boneIndex, aFirstAnimationDescFrameLine)
		//	Next
		//End Sub

		public void WriteSkeletonSectionForAnimation(SourceMdlSequenceDescBase aSequenceDescBase, SourceMdlAnimationDescBase anAnimationDescBase)
		{
			string line = "";
			int boneIndex = 0;
			AnimationFrameLine aFrameLine = null;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();
			SourceMdlSequenceDesc31 aSequenceDesc = null;
			SourceMdlAnimationDesc31 anAnimationDesc = null;

			aSequenceDesc = (SourceMdlSequenceDesc31)aSequenceDescBase;
			anAnimationDesc = (SourceMdlAnimationDesc31)anAnimationDescBase;

			//skeleton
			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			theAnimationFrameLines = new SortedList<int, AnimationFrameLine>();
			for (int frameIndex = 0; frameIndex < anAnimationDesc.frameCount; frameIndex++)
			{
				theAnimationFrameLines.Clear();
				CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex);

				if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
				{
					line = "time ";
				}
				else
				{
					line = "  time ";
				}
				line += frameIndex.ToString();
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theAnimationFrameLines.Count; i++)
				{
					boneIndex = theAnimationFrameLines.Keys[i];
					aFrameLine = theAnimationFrameLines.Values[i];

					//position.x = aFrameLine.position.x
					//position.y = aFrameLine.position.y
					//position.z = aFrameLine.position.z

					//rotation.x = aFrameLine.rotation.x
					//rotation.y = aFrameLine.rotation.y
					//rotation.z = aFrameLine.rotation.z
					//------
					SourceVector adjustedPosition = new SourceVector();
					SourceVector adjustedRotation = new SourceVector();
					AdjustPositionAndRotationByPiecewiseMovement(frameIndex, boneIndex, anAnimationDesc.theMovements, aFrameLine.position, aFrameLine.rotation, ref adjustedPosition, ref adjustedRotation);
					AdjustPositionAndRotation(boneIndex, adjustedPosition, adjustedRotation, ref position, ref rotation);

					line = "    ";
					line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);

					line += " ";
					line += position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " ";
					line += rotation.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += rotation.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += rotation.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

					if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
					{
						line += "   # ";
						line += "pos: ";
						line += aFrameLine.position.debug_text;
						line += "   ";
						line += "rot: ";
						line += aFrameLine.rotation.debug_text;
					}

					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private void AdjustPositionAndRotationByPiecewiseMovement(int frameIndex, int boneIndex, List<SourceMdlMovement> movements, SourceVector iPosition, SourceVector iRotation, ref SourceVector oPosition, ref SourceVector oRotation)
		{
			SourceMdlBone31 aBone = theMdlFileData.theBones[boneIndex];

			oPosition.x = iPosition.x;
			oPosition.y = iPosition.y;
			oPosition.z = iPosition.z;
			oPosition.debug_text = iPosition.debug_text;
			oRotation.x = iRotation.x;
			oRotation.y = iRotation.y;
			oRotation.z = iRotation.z;
			oRotation.debug_text = iRotation.debug_text;

			if (aBone.parentBoneIndex == -1)
			{
				if (movements != null && frameIndex > 0)
				{
					int previousFrameIndex = 0;
					SourceVector vecPos = new SourceVector();
					SourceVector vecAngle = new SourceVector();
					foreach (SourceMdlMovement aMovement in movements)
					{
						if (frameIndex <= aMovement.endframeIndex)
						{
							double f = (frameIndex - previousFrameIndex) / (double)(aMovement.endframeIndex - previousFrameIndex);
							double d = aMovement.v0 * f + 0.5 * (aMovement.v1 - aMovement.v0) * f * f;
							vecPos.x = vecPos.x + d * aMovement.vector.x;
							vecPos.y = vecPos.y + d * aMovement.vector.y;
							vecPos.z = vecPos.z + d * aMovement.vector.z;
							vecAngle.y = vecAngle.y * (1 - f) + MathModule.DegreesToRadians(aMovement.angle) * f;

							break;
						}
						else
						{
							previousFrameIndex = aMovement.endframeIndex;
							vecPos.x = aMovement.position.x;
							vecPos.y = aMovement.position.y;
							vecPos.z = aMovement.position.z;
							vecAngle.y = MathModule.DegreesToRadians(aMovement.angle);
						}
					}

					//SourceVector tmp = new SourceVector();
					//tmp.x = iPosition.x + vecPos.x;
					//tmp.y = iPosition.y + vecPos.y;
					//tmp.z = iPosition.z + vecPos.z;
					//oRotation.z = iRotation.z + vecAngle.y
					//oPosition = MathModule.VectorYawRotate(tmp, -vecAngle.y)
				}
			}
		}

		private void AdjustPositionAndRotation(int boneIndex, SourceVector iPosition, SourceVector iRotation, ref SourceVector oPosition, ref SourceVector oRotation)
		{
			SourceMdlBone31 aBone = theMdlFileData.theBones[boneIndex];

			if (aBone.parentBoneIndex == -1)
			{
				oPosition.x = iPosition.y;
				oPosition.y = -iPosition.x;
				oPosition.z = iPosition.z;
			}
			else
			{
				oPosition.x = iPosition.x;
				oPosition.y = iPosition.y;
				oPosition.z = iPosition.z;
			}

			if (aBone.parentBoneIndex == -1)
			{
				oRotation.x = iRotation.x;
				oRotation.y = iRotation.y;
				oRotation.z = iRotation.z + MathModule.DegreesToRadians(-90);
			}
			else
			{
				oRotation.x = iRotation.x;
				oRotation.y = iRotation.y;
				oRotation.z = iRotation.z;
			}
		}

		private string WriteVertexLine(SourceVtxStripGroup06 aStripGroup, int aVtxIndexIndex, int lodIndex, int meshVertexIndexStart, int bodyPartVertexIndexStart)
		{
			ushort aVtxVertexIndex = 0;
			SourceVtxVertex06 aVtxVertex = null;
			SourceMdlVertex31 aVertex = null;
			int vertexIndex = 0;
			string line = "";

			try
			{
				aVtxVertexIndex = aStripGroup.theVtxIndexes[aVtxIndexIndex];
				aVtxVertex = aStripGroup.theVtxVertexes[aVtxVertexIndex];
				vertexIndex = aVtxVertex.originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart;
				//If Me.theVvdFileData.fixupCount = 0 Then
				//	aVertex = Me.theVvdFileData.theVertexes(vertexIndex)
				//Else
				//	'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
				//	'      Maybe the listing by lodIndex is only needed internally by graphics engine.
				//	'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
				//	aVertex = Me.theVvdFileData.theFixedVertexesByLod(0)(vertexIndex)
				//	'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
				//End If
				aVertex = theMdlFileData.theBodyParts[0].theModels[0].theVertexes[vertexIndex];

				line = "  ";
				line += aVertex.boneWeight.bone[0].ToString(MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
				{
					line += aVertex.position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += (-aVertex.position.x).ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				else
				{
					line += aVertex.position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				line += " ";
				line += aVertex.position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
				{
					line += aVertex.normal.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += (-aVertex.normal.x).ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				else
				{
					line += aVertex.normal.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normal.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				line += " ";
				line += aVertex.normal.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				line += aVertex.texCoordX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				//line += aVertex.texCoordY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += (1 - aVertex.texCoordY).ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				line += aVertex.boneWeight.boneCount.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				for (int boneWeightBoneIndex = 0; boneWeightBoneIndex < aVertex.boneWeight.boneCount; boneWeightBoneIndex++)
				{
					line += " ";
					line += aVertex.boneWeight.bone[boneWeightBoneIndex].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.boneWeight.weight[boneWeightBoneIndex].ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				//Me.theOutputFileStreamWriter.WriteLine(line)
			}
			catch (Exception ex)
			{
				line = "// " + line;
			}

			return line;
		}

		private SourceVector TransformPhyVertex(SourceMdlBone31 aBone, SourceVector vertex, SourcePhyPhysCollisionModel aSourcePhysCollisionModel)
		{
			SourceVector aVectorTransformed = new SourceVector();
			SourceVector aVector = new SourceVector();

			//If Me.thePhyFileData.theSourcePhyIsCollisionModel Then
			//	aVectorTransformed.x = 1 / 0.0254 * vertex.z
			//	aVectorTransformed.y = 1 / 0.0254 * -vertex.x
			//	aVectorTransformed.z = 1 / 0.0254 * -vertex.y
			//Else
			//	aVector.x = 1 / 0.0254 * vertex.x
			//	aVector.y = 1 / 0.0254 * vertex.z
			//	aVector.z = 1 / 0.0254 * -vertex.y
			//	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			//End If
			aVector.x = 1 / 0.0254 * vertex.x;
			aVector.y = 1 / 0.0254 * vertex.z;
			aVector.z = 1 / 0.0254 * -vertex.y;
			if (aSourcePhysCollisionModel != null)
			{
				if (theMdlFileData.theBoneNameToBoneIndexMap.ContainsKey(aSourcePhysCollisionModel.theName))
				{
					aBone = theMdlFileData.theBones[theMdlFileData.theBoneNameToBoneIndexMap[aSourcePhysCollisionModel.theName]];
				}
				else
				{
					aVectorTransformed.x = 1 / 0.0254 * vertex.z;
					aVectorTransformed.y = 1 / 0.0254 * -vertex.x;
					aVectorTransformed.z = 1 / 0.0254 * -vertex.y;
					return aVectorTransformed;
				}
			}
			aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3);

			return aVectorTransformed;
		}

		private void CalcAnimation(SourceMdlSequenceDesc31 aSequenceDesc, SourceMdlAnimationDesc31 anAnimationDesc, int frameIndex)
		{
			double s = 0;
			SourceMdlBone31 aBone = null;
			SourceMdlAnimation31 anAnimation = null;
			SourceVector rot = null;
			SourceVector pos = null;
			AnimationFrameLine aFrameLine = null;

			s = 0;

			for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
			{
				aBone = theMdlFileData.theBones[boneIndex];
				anAnimation = anAnimationDesc.theAnimations[boneIndex];

				if (anAnimation != null)
				{
					if (theAnimationFrameLines.ContainsKey(boneIndex))
					{
						aFrameLine = theAnimationFrameLines[boneIndex];
					}
					else
					{
						aFrameLine = new AnimationFrameLine();
						theAnimationFrameLines.Add(boneIndex, aFrameLine);
					}

					aFrameLine.rotationQuat = new SourceQuaternion();
					rot = CalcBoneRotation(frameIndex, s, aBone, anAnimation, ref aFrameLine.rotationQuat);
					aFrameLine.rotation = new SourceVector();

					aFrameLine.rotation.x = rot.x;
					aFrameLine.rotation.y = rot.y;
					aFrameLine.rotation.z = rot.z;

					aFrameLine.rotation.debug_text = rot.debug_text;

					pos = CalcBonePosition(frameIndex, s, aBone, anAnimation);
					aFrameLine.position = new SourceVector();
					aFrameLine.position.x = pos.x;
					aFrameLine.position.y = pos.y;
					aFrameLine.position.z = pos.z;
					aFrameLine.position.debug_text = pos.debug_text;
				}
			}
		}

		private SourceVector CalcBoneRotation(int frameIndex, double s, SourceMdlBone31 aBone, SourceMdlAnimation31 anAnimation, ref SourceQuaternion rotationQuat)
		{
			SourceQuaternion rot = new SourceQuaternion();
			SourceVector angleVector = new SourceVector();

			if ((aBone.flags & SourceMdlBone2531.STUDIO_PROC_AXISINTERP) > 0)
			{
				angleVector.x = 0;
				angleVector.y = 0;
				angleVector.z = 0;

				angleVector.debug_text = "AXISINTERP";
			}
			else
			{
				if (anAnimation.theOffsets[3] <= 0 || (aBone.flags & SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0)
				{
					rot.x = aBone.rotation.x;
				}
				else
				{
					rot.x = ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationXValues, aBone.rotationScale.x, 0);
				}
				if (anAnimation.theOffsets[4] <= 0 || (aBone.flags & SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0)
				{
					rot.y = aBone.rotation.y;
				}
				else
				{
					rot.y = ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationYValues, aBone.rotationScale.y, 0);
				}
				if (anAnimation.theOffsets[5] <= 0 || (aBone.flags & SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0)
				{
					rot.z = aBone.rotation.z;
				}
				else
				{
					rot.z = ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationZValues, aBone.rotationScale.z, 0);
				}
				//rot.w = 1

				//angleVector = MathModule.ToEulerAngles2531(rot)

				angleVector.x = rot.x;
				angleVector.y = rot.y;
				angleVector.z = rot.z;
				angleVector.debug_text = "anim";
			}

			rotationQuat = rot;

			return angleVector;
		}

		private SourceVector CalcBonePosition(int frameIndex, double s, SourceMdlBone31 aBone, SourceMdlAnimation31 anAnimation)
		{
			SourceVector pos = new SourceVector();

			if (anAnimation.theOffsets[0] <= 0)
			{
				pos.x = aBone.position.x;
			}
			else
			{
				pos.x = ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationXValues, aBone.positionScale.x, aBone.position.x);
			}

			if (anAnimation.theOffsets[1] <= 0)
			{
				pos.y = aBone.position.y;
			}
			else
			{
				pos.y = ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationYValues, aBone.positionScale.y, aBone.position.y);
			}

			if (anAnimation.theOffsets[2] <= 0)
			{
				pos.z = aBone.position.z;
			}
			else
			{
				pos.z = ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationZValues, aBone.positionScale.z, aBone.position.z);
			}

			pos.debug_text = "anim";

			return pos;
		}

		public double ExtractAnimValue(int frameIndex, List<SourceMdlAnimationValue2531> animValues, double scale, double adjustment)
		{
			double v1 = 0;
			// k is frameCountRemainingToBeChecked
			int k = 0;
			int animValueIndex = 0;

			try
			{
				k = frameIndex;
				animValueIndex = 0;
				while (animValues[animValueIndex].total <= k)
				{
					k -= animValues[animValueIndex].total;
					animValueIndex += animValues[animValueIndex].valid + 1;
					if (animValueIndex >= animValues.Count || animValues[animValueIndex].total == 0)
					{
						//NOTE: Bad if it reaches here. This means maybe a newer format of the anim data was used for the model.
						v1 = 0;
						return v1;
					}
				}

				if (animValues[animValueIndex].valid > k)
				{
					//NOTE: The animValues index needs to be offset from current animValues index to match the C++ code above in comment.
					// value[n] = ( sequence[i].panim[q]->pos[j][n][k] - bonetable[j].pos[k] ) / bonetable[j].posscale[k]; 
					//	v = ( sequence[i].panim[q]->rot[j][n][k-3] - bonetable[j].rot[k-3] ); 
					//	if (v >= Q_PI)
					//		v -= Q_PI * 2;
					//	if (v < -Q_PI)
					//		v += Q_PI * 2;
					//	value[n] = v / bonetable[j].rotscale[k-3]; 
					v1 = animValues[animValueIndex + k + 1].value * scale + adjustment;
				}
				else
				{
					//NOTE: The animValues index needs to be offset from current animValues index to match the C++ code above in comment.
					v1 = animValues[animValueIndex + animValues[animValueIndex].valid].value * scale + adjustment;
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			return v1;
		}

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		//Private theAniFileData As SourceAniFileData44
		private SourceMdlFileData31 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		//Private theVtxFileData As SourceVtxFileData44
		//Private theVvdFileData As SourceVvdFileData37
		//Private theModelName As String

		private SortedList<int, AnimationFrameLine> theAnimationFrameLines;

#endregion

	}

}