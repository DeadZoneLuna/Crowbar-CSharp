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
	public class SourceSmdFile32
	{

#region Creation and Destruction

		public SourceSmdFile32(StreamWriter outputFileStream, SourceMdlFileData32 mdlFileData)
		{
			this.theOutputFileStreamWriter = outputFileStream;
			this.theMdlFileData = mdlFileData;
		}

		public SourceSmdFile32(StreamWriter outputFileStream, SourceMdlFileData32 mdlFileData, SourcePhyFileData phyFileData)
		{
			this.theOutputFileStreamWriter = outputFileStream;
			this.theMdlFileData = mdlFileData;
			this.thePhyFileData = phyFileData;
		}

#endregion

#region Methods

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(this.theOutputFileStreamWriter);
		}

		public void WriteHeaderSection()
		{
			string line = "";

			//version 1
			line = "version 1";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteNodesSection(int lodIndex)
		{
			string line = "";
			string name = null;

			//nodes
			line = "nodes";
			this.theOutputFileStreamWriter.WriteLine(line);

			for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
			{
				name = this.theMdlFileData.theBones[boneIndex].theName;

				line = "  ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " \"";
				line += name;
				line += "\" ";
				line += this.theMdlFileData.theBones[boneIndex].parentBoneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSkeletonSection(int lodIndex)
		{
			string line = "";

			//skeleton
			line = "skeleton";
			this.theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0";
			}
			else
			{
				line = "  time 0";
			}
			this.theOutputFileStreamWriter.WriteLine(line);
			for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
			{
				line = "    ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].rotation.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].rotation.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].rotation.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSection(int lodIndex, SourceVtxModel06 aVtxModel, SourceMdlModel37 aModel, int bodyPartVertexIndexStart)
		{
			string line = "";
			string materialLine = "";
			string vertex1Line = "";
			string vertex2Line = "";
			string vertex3Line = "";

			//triangles
			line = "triangles";
			this.theOutputFileStreamWriter.WriteLine(line);

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
						materialName = this.theMdlFileData.theTextures[materialIndex].theFileName;
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
										vertex1Line = this.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
										vertex2Line = this.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
										vertex3Line = this.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
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
										this.theOutputFileStreamWriter.WriteLine(materialLine);
										this.theOutputFileStreamWriter.WriteLine(vertex1Line);
										this.theOutputFileStreamWriter.WriteLine(vertex2Line);
										this.theOutputFileStreamWriter.WriteLine(vertex3Line);
									}
								}
							}
						}
					}
				}
			}
			catch
			{

			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSectionForPhysics()
		{
			string line = "";

			//triangles
			line = "triangles";
			this.theOutputFileStreamWriter.WriteLine(line);

			SourcePhyCollisionData collisionData = null;
			SourceMdlBone37 aBone = null;
			int boneIndex = 0;
			SourcePhyFace aTriangle = null;
			SourcePhyFaceSection faceSection = null;
			SourcePhyVertex phyVertex = null;
			SourceVector aVectorTransformed = null;
			SourcePhyPhysCollisionModel aSourcePhysCollisionModel = null;

			try
			{
				if (this.thePhyFileData.theSourcePhyCollisionDatas != null)
				{
					for (int collisionDataIndex = 0; collisionDataIndex < this.thePhyFileData.theSourcePhyCollisionDatas.Count; collisionDataIndex++)
					{
						collisionData = this.thePhyFileData.theSourcePhyCollisionDatas[collisionDataIndex];

						if (collisionDataIndex < this.thePhyFileData.theSourcePhyPhysCollisionModels.Count)
						{
							aSourcePhysCollisionModel = this.thePhyFileData.theSourcePhyPhysCollisionModels[collisionDataIndex];
						}
						else
						{
							aSourcePhysCollisionModel = null;
						}

						for (int faceSectionIndex = 0; faceSectionIndex < collisionData.theFaceSections.Count; faceSectionIndex++)
						{
							faceSection = collisionData.theFaceSections[faceSectionIndex];

							if (faceSection.theBoneIndex >= this.theMdlFileData.theBones.Count)
							{
								continue;
							}
							if (aSourcePhysCollisionModel != null && this.theMdlFileData.theBoneNameToBoneIndexMap.ContainsKey(aSourcePhysCollisionModel.theName))
							{
								boneIndex = this.theMdlFileData.theBoneNameToBoneIndexMap[aSourcePhysCollisionModel.theName];
							}
							else
							{
								boneIndex = faceSection.theBoneIndex;
							}
							aBone = this.theMdlFileData.theBones[boneIndex];

							for (int triangleIndex = 0; triangleIndex < faceSection.theFaces.Count; triangleIndex++)
							{
								aTriangle = faceSection.theFaces[triangleIndex];

								line = "  phy";
								//line = "  collisionDataBlock_" + collisionDataIndex.ToString()
								this.theOutputFileStreamWriter.WriteLine(line);

								for (int vertexIndex = 0; vertexIndex < aTriangle.vertexIndex.Length; vertexIndex++)
								{
									//phyVertex = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex))
									phyVertex = faceSection.theVertices[aTriangle.vertexIndex[vertexIndex]];

									aVectorTransformed = this.TransformPhyVertex(aBone, phyVertex.vertex, aSourcePhysCollisionModel);

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
									this.theOutputFileStreamWriter.WriteLine(line);
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
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		//'TODO: Write the firstAnimDesc's first frame's frameLines because it is used for "subtract" option.
		//Public Sub CalculateFirstAnimDescFrameLinesForSubtract()
		//	Dim boneIndex As Integer
		//	Dim aFrameLine As AnimationFrameLine
		//	Dim frameIndex As Integer
		//	Dim aSequenceDesc As SourceMdlSequenceDesc
		//	Dim anAnimationDesc As SourceMdlAnimationDesc32

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
			//Dim tempRotation As New SourceVector()
			SourceMdlSequenceDesc32 aSequenceDesc = null;
			SourceMdlAnimationDesc32 anAnimationDesc = null;

			aSequenceDesc = (SourceMdlSequenceDesc32)aSequenceDescBase;
			anAnimationDesc = (SourceMdlAnimationDesc32)anAnimationDescBase;

			//skeleton
			line = "skeleton";
			this.theOutputFileStreamWriter.WriteLine(line);

			this.theAnimationFrameLines = new SortedList<int, AnimationFrameLine>();
			for (int frameIndex = 0; frameIndex < anAnimationDesc.frameCount; frameIndex++)
			{
				this.theAnimationFrameLines.Clear();
				this.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex);

				if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
				{
					line = "time ";
				}
				else
				{
					line = "  time ";
				}
				line += frameIndex.ToString();
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theAnimationFrameLines.Count; i++)
				{
					boneIndex = this.theAnimationFrameLines.Keys[i];
					aFrameLine = this.theAnimationFrameLines.Values[i];

					position.x = aFrameLine.position.x;
					position.y = aFrameLine.position.y;
					position.z = aFrameLine.position.z;

					rotation.x = aFrameLine.rotation.x;
					rotation.y = aFrameLine.rotation.y;
					rotation.z = aFrameLine.rotation.z;

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

					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private string WriteVertexLine(SourceVtxStripGroup06 aStripGroup, int aVtxIndexIndex, int lodIndex, int meshVertexIndexStart, int bodyPartVertexIndexStart)
		{
			ushort aVtxVertexIndex = 0;
			SourceVtxVertex06 aVtxVertex = null;
			SourceMdlVertex37 aVertex = null;
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
				aVertex = this.theMdlFileData.theBodyParts[0].theModels[0].theVertexes[vertexIndex];

				line = "  ";
				line += aVertex.boneWeight.bone[0].ToString(MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
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
				if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
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

		private SourceVector TransformPhyVertex(SourceMdlBone37 aBone, SourceVector vertex, SourcePhyPhysCollisionModel aSourcePhysCollisionModel)
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
			//======
			//aVector.x = 1 / 0.0254 * vertex.x
			//aVector.y = 1 / 0.0254 * vertex.z
			//aVector.z = 1 / 0.0254 * -vertex.y
			//Dim aParentBone As SourceMdlBone37
			//Dim aChildBone As SourceMdlBone37
			//Dim parentBoneIndex As Integer
			//Dim inputBoneMatrixColumn0 As New SourceVector()
			//Dim inputBoneMatrixColumn1 As New SourceVector()
			//Dim inputBoneMatrixColumn2 As New SourceVector()
			//Dim inputBoneMatrixColumn3 As New SourceVector()
			//Dim boneMatrixColumn0 As New SourceVector()
			//Dim boneMatrixColumn1 As New SourceVector()
			//Dim boneMatrixColumn2 As New SourceVector()
			//Dim boneMatrixColumn3 As New SourceVector()

			//aChildBone = aBone
			//inputBoneMatrixColumn0.x = aChildBone.poseToBoneColumn0.x
			//inputBoneMatrixColumn0.y = aChildBone.poseToBoneColumn0.y
			//inputBoneMatrixColumn0.z = aChildBone.poseToBoneColumn0.z
			//inputBoneMatrixColumn1.x = aChildBone.poseToBoneColumn1.x
			//inputBoneMatrixColumn1.y = aChildBone.poseToBoneColumn1.y
			//inputBoneMatrixColumn1.z = aChildBone.poseToBoneColumn1.z
			//inputBoneMatrixColumn2.x = aChildBone.poseToBoneColumn2.x
			//inputBoneMatrixColumn2.y = aChildBone.poseToBoneColumn2.y
			//inputBoneMatrixColumn2.z = aChildBone.poseToBoneColumn2.z
			//inputBoneMatrixColumn3.x = aChildBone.poseToBoneColumn3.x
			//inputBoneMatrixColumn3.y = aChildBone.poseToBoneColumn3.y
			//inputBoneMatrixColumn3.z = aChildBone.poseToBoneColumn3.z
			//While True
			//	parentBoneIndex = aChildBone.parentBoneIndex
			//	If parentBoneIndex = -1 Then
			//		aVectorTransformed = MathModule.VectorITransform(aVector, inputBoneMatrixColumn0, inputBoneMatrixColumn1, inputBoneMatrixColumn2, inputBoneMatrixColumn3)
			//		Exit While
			//	Else
			//		aParentBone = Me.theMdlFileData.theBones(parentBoneIndex)
			//		MathModule.R_ConcatTransforms(aParentBone.poseToBoneColumn0, aParentBone.poseToBoneColumn1, aParentBone.poseToBoneColumn2, aParentBone.poseToBoneColumn3, inputBoneMatrixColumn0, inputBoneMatrixColumn1, inputBoneMatrixColumn2, inputBoneMatrixColumn3, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
			//		aChildBone = aParentBone
			//		inputBoneMatrixColumn0.x = boneMatrixColumn0.x
			//		inputBoneMatrixColumn0.y = boneMatrixColumn0.y
			//		inputBoneMatrixColumn0.z = boneMatrixColumn0.z
			//		inputBoneMatrixColumn1.x = boneMatrixColumn1.x
			//		inputBoneMatrixColumn1.y = boneMatrixColumn1.y
			//		inputBoneMatrixColumn1.z = boneMatrixColumn1.z
			//		inputBoneMatrixColumn2.x = boneMatrixColumn2.x
			//		inputBoneMatrixColumn2.y = boneMatrixColumn2.y
			//		inputBoneMatrixColumn2.z = boneMatrixColumn2.z
			//		inputBoneMatrixColumn3.x = boneMatrixColumn3.x
			//		inputBoneMatrixColumn3.y = boneMatrixColumn3.y
			//		inputBoneMatrixColumn3.z = boneMatrixColumn3.z
			//	End If
			//End While
			//======
			//TODO: Probably not the correct way, but it works for bullsquid and ship01.
			aVector.x = 1 / 0.0254 * vertex.x;
			aVector.y = 1 / 0.0254 * vertex.z;
			aVector.z = 1 / 0.0254 * -vertex.y;
			if (aSourcePhysCollisionModel != null)
			{
				if (this.theMdlFileData.theBoneNameToBoneIndexMap.ContainsKey(aSourcePhysCollisionModel.theName))
				{
					aBone = this.theMdlFileData.theBones[this.theMdlFileData.theBoneNameToBoneIndexMap[aSourcePhysCollisionModel.theName]];
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

		//'NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used in VPHYSICS.DLL for each face:
		//'      convertedZ = 1.0 / 0.0254 * lastVertex.position.z
		//'      convertedY = 1.0 / 0.0254 * -lastVertex.position.y
		//'      convertedX = 1.0 / 0.0254 * lastVertex.position.x
		//'NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used after above for each vertex:
		//'      newValue1 = unknownZ1 * convertedZ + unknownY1 * convertedY + unknownX1 * convertedX + unknownW1
		//'      newValue2 = unknownZ2 * convertedZ + unknownY2 * convertedY + unknownX2 * convertedX + unknownW2
		//'      newValue3 = unknownZ3 * convertedZ + unknownY3 * convertedY + unknownX3 * convertedX + unknownW3
		//'Seems to be same as this code:
		//'Dim aBone As SourceMdlBone
		//'aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(anEyeball.boneIndex)
		//'eyeballPosition = MathModule.VectorITransform(anEyeball.org, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone, ByVal vertex As SourceVector) As SourceVector
		//	Dim aVectorTransformed As New SourceVector
		//	Dim aVector As New SourceVector()

		//	'NOTE: Too small.
		//	'aVectorTransformed.x = vertex.x
		//	'aVectorTransformed.y = vertex.y
		//	'aVectorTransformed.z = vertex.z
		//	'------
		//	'NOTE: Rotated for:
		//	'      simple_shape
		//	'      L4D2 w_models\weapons\w_minigun
		//	'aVectorTransformed.x = 1 / 0.0254 * vertex.x
		//	'aVectorTransformed.y = 1 / 0.0254 * vertex.y
		//	'aVectorTransformed.z = 1 / 0.0254 * vertex.z
		//	'------
		//	'NOTE: Works for:
		//	'      simple_shape
		//	'      L4D2 w_models\weapons\w_minigun
		//	'      L4D2 w_models\weapons\w_smg_uzi
		//	'      L4D2 props_vehicles\van
		//	'aVectorTransformed.x = 1 / 0.0254 * vertex.z
		//	'aVectorTransformed.y = 1 / 0.0254 * -vertex.x
		//	'aVectorTransformed.z = 1 / 0.0254 * -vertex.y
		//	'------
		//	'NOTE: Rotated for:
		//	'      L4D2 w_models\weapons\w_minigun
		//	'aVectorTransformed.x = 1 / 0.0254 * vertex.x
		//	'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
		//	'aVectorTransformed.z = 1 / 0.0254 * vertex.z
		//	'------
		//	'NOTE: Rotated for:
		//	'      L4D2 props_vehicles\van
		//	'aVectorTransformed.x = 1 / 0.0254 * vertex.z
		//	'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
		//	'aVectorTransformed.z = 1 / 0.0254 * vertex.x
		//	'------
		//	'NOTE: Rotated for:
		//	'      L4D2 w_models\weapons\w_minigun
		//	'aVector.x = 1 / 0.0254 * vertex.x
		//	'aVector.y = 1 / 0.0254 * vertex.y
		//	'aVector.z = 1 / 0.0254 * vertex.z
		//	'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	'------
		//	'NOTE: Rotated for:
		//	'      L4D2 w_models\weapons\w_minigun
		//	'aVector.x = 1 / 0.0254 * vertex.x
		//	'aVector.y = 1 / 0.0254 * -vertex.y
		//	'aVector.z = 1 / 0.0254 * vertex.z
		//	'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	'------
		//	'NOTE: Works for:
		//	'      L4D2 w_models\weapons\w_minigun
		//	'      L4D2 w_models\weapons\w_smg_uzi
		//	'NOTE: Rotated for:
		//	'      simple_shape
		//	'      L4D2 props_vehicles\van
		//	'NOTE: Each mesh piece rotated for:
		//	'      L4D2 survivors\survivor_producer
		//	'aVector.x = 1 / 0.0254 * vertex.z
		//	'aVector.y = 1 / 0.0254 * -vertex.y
		//	'aVector.z = 1 / 0.0254 * vertex.x
		//	'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	'------
		//	'NOTE: Works for:
		//	'      simple_shape
		//	'      L4D2 props_vehicles\van
		//	'      L4D2 survivors\survivor_producer
		//	'      L4D2 w_models\weapons\w_autoshot_m4super
		//	'      L4D2 w_models\weapons\w_desert_eagle
		//	'      L4D2 w_models\weapons\w_minigun
		//	'      L4D2 w_models\weapons\w_rifle_m16a2
		//	'      L4D2 w_models\weapons\w_smg_uzi
		//	'NOTE: Rotated for:
		//	'      L4D2 w_models\weapons\w_desert_rifle
		//	'      L4D2 w_models\weapons\w_shotgun_spas
		//	If Me.thePhyFileData.theSourcePhyIsCollisionModel Then
		//		aVectorTransformed.x = 1 / 0.0254 * vertex.z
		//		aVectorTransformed.y = 1 / 0.0254 * -vertex.x
		//		aVectorTransformed.z = 1 / 0.0254 * -vertex.y
		//	Else
		//		aVector.x = 1 / 0.0254 * vertex.x
		//		aVector.y = 1 / 0.0254 * vertex.z
		//		aVector.z = 1 / 0.0254 * -vertex.y
		//		aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	End If



		//	'------
		//	'NOTE: Works for:
		//	'      survivor_producer
		//	'NOTE: Does not work for:
		//	'      w_smg_uzi()
		//	'phyVertex.x = 1 / 0.0254 * aVector.x
		//	'phyVertex.y = 1 / 0.0254 * aVector.z
		//	'phyVertex.z = 1 / 0.0254 * -aVector.y
		//	'------
		//	'NOTE: These two lines match orientation for cstrike it_lampholder1 model, 
		//	'      but still doesn't compile properly.
		//	'NOTE: Does not work for:
		//	'      w_smg_uzi()
		//	'phyVertex.x = 1 / 0.0254 * aVector.z
		//	'phyVertex.y = 1 / 0.0254 * -aVector.x
		//	'phyVertex.z = 1 / 0.0254 * -aVector.y
		//	'------
		//	'NOTE: Does not work for:
		//	'      w_smg_uzi()
		//	'phyVertex.x = 1 / 0.0254 * aVector.y
		//	'phyVertex.y = 1 / 0.0254 * aVector.x
		//	'phyVertex.z = 1 / 0.0254 * -aVector.z
		//	'------
		//	'NOTE: Does not work for:
		//	'      w_smg_uzi()
		//	'phyVertex.x = 1 / 0.0254 * aVector.x
		//	'phyVertex.y = 1 / 0.0254 * aVector.y
		//	'phyVertex.z = 1 / 0.0254 * -aVector.z
		//	'------
		//	'NOTE: Does not work for:
		//	'      w_smg_uzi()
		//	'phyVertex.x = 1 / 0.0254 * -aVector.y
		//	'phyVertex.y = 1 / 0.0254 * aVector.x
		//	'phyVertex.z = 1 / 0.0254 * aVector.z
		//	'------
		//	'NOTE: Does not work for:
		//	'      w_smg_uzi()
		//	'phyVertex.x = 1 / 0.0254 * -aVector.y
		//	'phyVertex.y = 1 / 0.0254 * aVector.x
		//	'phyVertex.z = 1 / 0.0254 * aVector.z
		//	'------
		//	'NOTE: Does not work for:
		//	'      w_smg_uzi()
		//	'phyVertex.x = 1 / 0.0254 * aVector.z
		//	'phyVertex.y = 1 / 0.0254 * aVector.y
		//	'phyVertex.z = 1 / 0.0254 * aVector.x
		//	'------
		//	'NOTE: Works for:
		//	'      w_smg_uzi()
		//	'NOTE: Does not work for:
		//	'      survivor_producer
		//	'phyVertex.x = 1 / 0.0254 * aVector.z
		//	'phyVertex.y = 1 / 0.0254 * -aVector.y
		//	'phyVertex.z = 1 / 0.0254 * aVector.x
		//	'------
		//	'phyVertex.x = 1 / 0.0254 * aVector.z
		//	'phyVertex.y = 1 / 0.0254 * -aVector.y
		//	'phyVertex.z = 1 / 0.0254 * -aVector.x
		//	'------
		//	''TODO: Find some rationale for why phys model is rotated differently for different models.
		//	''      Possibly due to rotation needed to transfrom from pose to bone position.
		//	''If Me.theSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count < 2 Then
		//	''If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
		//	'If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyIsCollisionModel Then
		//	'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		//	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		//	'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
		//	'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		//	'	'TEST:  Does not rotate L4D2's van phys mesh correctly.
		//	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
		//	'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
		//	'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		//	'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		//	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.z
		//	'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.y
		//	'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.x
		//	'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		//	'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		//	'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
		//	'	'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y
		//	'	'TEST: Works for L4D2's van phys mesh.
		//	'	'      Does not work for L4D2 w_model\weapons\w_minigun.mdl.
		//	'	aVector.x = 1 / 0.0254 * vertex.z
		//	'	aVector.y = 1 / 0.0254 * -vertex.x
		//	'	aVector.z = 1 / 0.0254 * -vertex.y

		//	'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

		//	'	'======

		//	'	'Dim aVectorTransformed2 As SourceVector
		//	'	''aVectorTransformed2 = New SourceVector()
		//	'	''aVectorTransformed2 = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	'	''aVectorTransformed2.x = aVector.x
		//	'	''aVectorTransformed2.y = aVector.y
		//	'	''aVectorTransformed2.z = aVector.z

		//	'	'aVectorTransformed = MathModule.VectorTransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	'	''aVectorTransformed = MathModule.VectorTransform(aVectorTransformed2, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	'	''aVectorTransformed = New SourceVector()
		//	'	''aVectorTransformed.x = aVectorTransformed2.x
		//	'	''aVectorTransformed.y = aVectorTransformed2.y
		//	'	''aVectorTransformed.z = aVectorTransformed2.z
		//	'Else
		//	'	'TEST: Does not work for L4D2 w_model\weapons\w_minigun.mdl.
		//	'	aVector.x = 1 / 0.0254 * vertex.x
		//	'	aVector.y = 1 / 0.0254 * vertex.z
		//	'	aVector.z = 1 / 0.0254 * -vertex.y

		//	'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	'End If
		//	'------
		//	'TEST: Does not rotate L4D2's van phys mesh correctly.
		//	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		//	'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
		//	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		//	'TEST: Does not rotate L4D2's van phys mesh correctly.
		//	'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
		//	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
		//	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		//	'TEST: works for survivor_producer; matches ref and phy meshes of van, but both are rotated 90 degrees on z-axis
		//	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		//	'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
		//	'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y

		//	'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		//	''------
		//	'''TEST: Only rotate by -90 deg if bone is a root bone.  Do not know why.
		//	''If aBone.parentBoneIndex = -1 Then
		//	''	aVectorTransformed = MathModule.RotateAboutZAxis(aVectorTransformed, MathModule.DegreesToRadians(-90), aBone)
		//	''End If

		//	Return aVectorTransformed
		//End Function

		//static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
		//	mstudioseqdesc_t &seqdesc,
		//	int sequence, int animation,
		//	float cycle, int boneMask )
		//{
		//	int					i;
		//
		//	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
		//	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
		//	mstudioanim_t *panim = animdesc.pAnim( );
		//
		//	int					iFrame;
		//	float				s;
		//
		//	float fFrame = cycle * (animdesc.numframes - 1);
		//
		//	iFrame = (int)fFrame;
		//	s = (fFrame - iFrame);
		//
		//	float *pweight = seqdesc.pBoneweight( 0 );
		//
		//	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
		//	{
		//		if (panim && panim->bone == i)
		//		{
		//			if (*pweight > 0 && (pbone->flags & boneMask))
		//			{
		//				CalcBoneQuaternion( iFrame, s, pbone, panim, q[i] );
		//				CalcBonePosition  ( iFrame, s, pbone, panim, pos[i] );
		//			}
		//			panim = panim->pNext();
		//		}
		//		else if (*pweight > 0 && (pbone->flags & boneMask))
		//		{
		//			if (animdesc.flags & STUDIO_DELTA)
		//			{
		//				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
		//				pos[i].Init( 0.0f, 0.0f, 0.0f );
		//			}
		//			else
		//			{
		//				q[i] = pbone->quat;
		//				pos[i] = pbone->pos;
		//			}
		//		}
		//	}
		//}
		//======
		//FROM: SourceEngine2007_source\src_main\public\bone_setup.cpp
		////-----------------------------------------------------------------------------
		//// Purpose: Find and decode a sub-frame of animation
		////-----------------------------------------------------------------------------
		//
		//static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
		//	mstudioseqdesc_t &seqdesc,
		//	int sequence, int animation,
		//	float cycle, int boneMask )
		//{
		//#ifdef STUDIO_ENABLE_PERF_COUNTERS
		//	pStudioHdr->m_nPerfAnimationLayers++;
		//#endif
		//
		//	virtualmodel_t *pVModel = pStudioHdr->GetVirtualModel();
		//
		//	if (pVModel)
		//	{
		//		CalcVirtualAnimation( pVModel, pStudioHdr, pos, q, seqdesc, sequence, animation, cycle, boneMask );
		//		return;
		//	}
		//
		//	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
		//	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
		//	const mstudiolinearbone_t *pLinearBones = pStudioHdr->pLinearBones();
		//
		//	int					i;
		//	int					iFrame;
		//	float				s;
		//
		//	float fFrame = cycle * (animdesc.numframes - 1);
		//
		//	iFrame = (int)fFrame;
		//	s = (fFrame - iFrame);
		//
		//	int iLocalFrame = iFrame;
		//	float flStall;
		//	mstudioanim_t *panim = animdesc.pAnim( &iLocalFrame, flStall );
		//
		//	float *pweight = seqdesc.pBoneweight( 0 );
		//
		//	// if the animation isn't available, look for the zero frame cache
		//	if (!panim)
		//	{
		//		// Msg("zeroframe %s\n", animdesc.pszName() );
		//		// pre initialize
		//		for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
		//		{
		//			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
		//			{
		//				if (animdesc.flags & STUDIO_DELTA)
		//				{
		//					q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
		//					pos[i].Init( 0.0f, 0.0f, 0.0f );
		//				}
		//				else
		//				{
		//					q[i] = pbone->quat;
		//					pos[i] = pbone->pos;
		//				}
		//			}
		//		}
		//
		//		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, 1.0 );
		//
		//		return;
		//	}
		//
		//	// BUGBUG: the sequence, the anim, and the model can have all different bone mappings.
		//	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
		//	{
		//		if (panim && panim->bone == i)
		//		{
		//			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
		//			{
		//				CalcBoneQuaternion( iLocalFrame, s, pbone, pLinearBones, panim, q[i] );
		//				CalcBonePosition  ( iLocalFrame, s, pbone, pLinearBones, panim, pos[i] );
		//#ifdef STUDIO_ENABLE_PERF_COUNTERS
		//				pStudioHdr->m_nPerfAnimatedBones++;
		//				pStudioHdr->m_nPerfUsedBones++;
		//#endif
		//			}
		//			panim = panim->pNext();
		//		}
		//		else if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
		//		{
		//			if (animdesc.flags & STUDIO_DELTA)
		//			{
		//				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
		//				pos[i].Init( 0.0f, 0.0f, 0.0f );
		//			}
		//			else
		//			{
		//				q[i] = pbone->quat;
		//				pos[i] = pbone->pos;
		//			}
		//#ifdef STUDIO_ENABLE_PERF_COUNTERS
		//			pStudioHdr->m_nPerfUsedBones++;
		//#endif
		//		}
		//	}
		//
		//	// cross fade in previous zeroframe data
		//	if (flStall > 0.0f)
		//	{
		//		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, flStall );
		//	}
		//
		//	if (animdesc.numlocalhierarchy)
		//	{
		//		matrix3x4_t *boneToWorld = g_MatrixPool.Alloc();
		//		CBoneBitList boneComputed;
		//
		//		int i;
		//		for (i = 0; i < animdesc.numlocalhierarchy; i++)
		//		{
		//			mstudiolocalhierarchy_t *pHierarchy = animdesc.pHierarchy( i );
		//
		//			if ( !pHierarchy )
		//				break;
		//
		//			if (pStudioHdr->boneFlags(pHierarchy->iBone) & boneMask)
		//			{
		//				if (pStudioHdr->boneFlags(pHierarchy->iNewParent) & boneMask)
		//				{
		//					CalcLocalHierarchyAnimation( pStudioHdr, boneToWorld, boneComputed, pos, q, pbone, pHierarchy, pHierarchy->iBone, pHierarchy->iNewParent, cycle, iFrame, s, boneMask );
		//				}
		//			}
		//		}
		//
		//		g_MatrixPool.Free( boneToWorld );
		//	}
		//
		//}
		private void CalcAnimation(SourceMdlSequenceDesc32 aSequenceDesc, SourceMdlAnimationDesc32 anAnimationDesc, int frameIndex)
		{
			double s = 0;
			SourceMdlBone37 aBone = null;
			SourceMdlAnimation37 anAnimation = null;
			SourceVector rot = null;
			SourceVector pos = null;
			AnimationFrameLine aFrameLine = null;

			s = 0;

			for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
			{
				aBone = this.theMdlFileData.theBones[boneIndex];
				anAnimation = anAnimationDesc.theAnimations[boneIndex];

				if (anAnimation != null)
				{
					if (this.theAnimationFrameLines.ContainsKey(boneIndex))
					{
						aFrameLine = this.theAnimationFrameLines[boneIndex];
					}
					else
					{
						aFrameLine = new AnimationFrameLine();
						this.theAnimationFrameLines.Add(boneIndex, aFrameLine);
					}

					aFrameLine.rotationQuat = new SourceQuaternion();
					rot = CalcBoneRotation(frameIndex, s, aBone, anAnimation, ref aFrameLine.rotationQuat);
					aFrameLine.rotation = new SourceVector();

					aFrameLine.rotation.x = rot.x;
					aFrameLine.rotation.y = rot.y;
					aFrameLine.rotation.z = rot.z;

					aFrameLine.rotation.debug_text = rot.debug_text;

					pos = this.CalcBonePosition(frameIndex, s, aBone, anAnimation);
					aFrameLine.position = new SourceVector();
					aFrameLine.position.x = pos.x;
					aFrameLine.position.y = pos.y;
					aFrameLine.position.z = pos.z;
					aFrameLine.position.debug_text = pos.debug_text;
				}
			}
		}

		private SourceVector CalcBoneRotation(int frameIndex, double s, SourceMdlBone37 aBone, SourceMdlAnimation37 anAnimation, ref SourceQuaternion rotationQuat)
		{
			SourceVector angleVector = new SourceVector();

			if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0)
			{
				if (anAnimation.animationValueOffsets[3] <= 0)
				{
					//angleVector.x = 0
					angleVector.x = aBone.rotation.x;
				}
				else
				{
					angleVector.x = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[3], aBone.rotationScale.x);
					//angle1[j] = pbone->value[j+3] + angle1[j] * pbone->scale[j+3];
					angleVector.x = aBone.rotation.x + angleVector.x * aBone.rotationScale.x;
				}
				if (anAnimation.animationValueOffsets[4] <= 0)
				{
					//angleVector.y = 0
					angleVector.y = aBone.rotation.y;
				}
				else
				{
					angleVector.y = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[4], aBone.rotationScale.y);
					angleVector.y = aBone.rotation.y + angleVector.y * aBone.rotationScale.y;
				}
				if (anAnimation.animationValueOffsets[5] <= 0)
				{
					//angleVector.z = 0
					angleVector.z = aBone.rotation.z;
				}
				else
				{
					angleVector.z = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[5], aBone.rotationScale.z);
					angleVector.z = aBone.rotation.z + angleVector.z * aBone.rotationScale.z;
				}

				rotationQuat = MathModule.EulerAnglesToQuaternion(angleVector);

				angleVector.debug_text = "anim";
			}
			else
			{
				rotationQuat = anAnimation.rotationQuat;
				angleVector = MathModule.ToEulerAngles(rotationQuat);

				angleVector.debug_text = "rot";
			}

			return angleVector;
		}

		//FROM: SourceEngine2007_source\public\bone_setup.cpp
		////-----------------------------------------------------------------------------
		//// Purpose: return a sub frame position for a single bone
		////-----------------------------------------------------------------------------
		//void CalcBonePosition(	int frame, float s,
		//						const Vector &basePos, const Vector &baseBoneScale, 
		//						const mstudioanim_t *panim, Vector &pos	)
		//{
		//	if (panim->flags & STUDIO_ANIM_RAWPOS)
		//	{
		//		pos = *(panim->pPos());
		//		Assert( pos.IsValid() );

		//		return;
		//	}
		//	else if (!(panim->flags & STUDIO_ANIM_ANIMPOS))
		//	{
		//		if (panim->flags & STUDIO_ANIM_DELTA)
		//		{
		//			pos.Init( 0.0f, 0.0f, 0.0f );
		//		}
		//		else
		//		{
		//			pos = basePos;
		//		}
		//		return;
		//	}

		//	mstudioanim_valueptr_t *pPosV = panim->pPosV();
		//	int					j;

		//	if (s > 0.001f)
		//	{
		//		float v1, v2;
		//		for (j = 0; j < 3; j++)
		//		{
		//			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], v1, v2 );
		//			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
		//			pos[j] = v1 * (1.0 - s) + v2 * s;
		//		}
		//	}
		//	else
		//	{
		//		for (j = 0; j < 3; j++)
		//		{
		//			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
		//			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], pos[j] );
		//		}
		//	}

		//	if (!(panim->flags & STUDIO_ANIM_DELTA))
		//	{
		//		pos.x = pos.x + basePos.x;
		//		pos.y = pos.y + basePos.y;
		//		pos.z = pos.z + basePos.z;
		//	}

		//	Assert( pos.IsValid() );
		//}
		private SourceVector CalcBonePosition(int frameIndex, double s, SourceMdlBone37 aBone, SourceMdlAnimation37 anAnimation)
		{
			SourceVector pos = new SourceVector();

			if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0)
			{
				if (anAnimation.animationValueOffsets[0] <= 0)
				{
					//pos.x = 0
					pos.x = aBone.position.x;
				}
				else
				{
					pos.x = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[0], aBone.positionScale.x);
					pos.x = aBone.position.x + pos.x * aBone.positionScale.x;
				}

				if (anAnimation.animationValueOffsets[1] <= 0)
				{
					//pos.y = 0
					pos.y = aBone.position.y;
				}
				else
				{
					pos.y = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[1], aBone.positionScale.y);
					pos.y = aBone.position.y + pos.y * aBone.positionScale.y;
				}

				if (anAnimation.animationValueOffsets[2] <= 0)
				{
					//pos.z = 0
					pos.z = aBone.position.z;
				}
				else
				{
					pos.z = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[2], aBone.positionScale.z);
					pos.z = aBone.position.z + pos.z * aBone.positionScale.z;
				}

				pos.debug_text = "anim";
			}
			else
			{
				pos = anAnimation.position;

				pos.debug_text = "pos";
			}

			return pos;
		}

		public double ExtractAnimValue(int frameIndex, List<SourceMdlAnimationValue10> animValues, double scale)
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
					//NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
					v1 = animValues[animValueIndex + k + 1].value;
				}
				else
				{
					//NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
					v1 = animValues[animValueIndex + animValues[animValueIndex].valid].value;
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
		private SourceMdlFileData32 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		//Private theVtxFileData As SourceVtxFileData44
		//Private theVvdFileData As SourceVvdFileData37
		//Private theModelName As String

		private SortedList<int, AnimationFrameLine> theAnimationFrameLines;

#endregion

	}

}