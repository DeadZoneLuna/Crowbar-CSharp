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
	public class SourceSmdFile52
	{

#region Creation and Destruction

		public SourceSmdFile52(StreamWriter outputFileStream, SourceMdlFileData52 mdlFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
		}

		public SourceSmdFile52(StreamWriter outputFileStream, SourceMdlFileData52 mdlFileData, SourceVvdFileData04 vvdFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			theVvdFileData = vvdFileData;
		}

		public SourceSmdFile52(StreamWriter outputFileStream, SourceMdlFileData52 mdlFileData, SourcePhyFileData phyFileData)
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
			int boneCount = 0;

			//nodes
			line = "nodes";
			theOutputFileStreamWriter.WriteLine(line);

			if (theMdlFileData.theBones == null)
			{
				boneCount = 0;
			}
			else
			{
				boneCount = theMdlFileData.theBones.Count;
			}
			for (int boneIndex = 0; boneIndex < boneCount; boneIndex++)
			{
				//Dim aBone As SourceMdlBone
				//aBone = Me.theMdlFileData.theBones(boneIndex)
				//If lodIndex = -2 AndAlso aBone.proceduralRuleOffset <> 0 AndAlso aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
				//	Continue For
				//End If

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
			SourceMdlBone aBone = null;

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
				aBone = theMdlFileData.theBones[boneIndex];

				line = "    ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += aBone.position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += aBone.position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += aBone.position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += aBone.rotation.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += aBone.rotation.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += aBone.rotation.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSection(SourceVtxModel07 aVtxModel, int lodIndex, SourceMdlModel aModel, int bodyPartVertexIndexStart)
		{
			string line = "";
			string materialLine = "";
			string vertex1Line = "";
			string vertex2Line = "";
			string vertex3Line = "";

			//triangles
			line = "triangles";
			theOutputFileStreamWriter.WriteLine(line);

			SourceVtxModelLod07 aVtxLod = null;
			SourceVtxMesh07 aVtxMesh = null;
			SourceVtxStripGroup07 aStripGroup = null;
			//Dim cumulativeVertexCount As Integer
			//Dim maxIndexForMesh As Integer
			//Dim cumulativeMaxIndex As Integer
			int materialIndex = 0;
			string materialPathFileName = null;
			string materialFileName = null;
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
						materialPathFileName = theMdlFileData.theTextures[materialIndex].thePathFileName;
						materialFileName = theMdlFileData.theModifiedTextureFileNames[materialIndex];

						meshVertexIndexStart = aModel.theMeshes[meshIndex].vertexIndexStart;

						if (aVtxMesh.theVtxStripGroups != null)
						{
							if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked && materialPathFileName != materialFileName)
							{
								materialLine = "// In the MDL file as: " + materialPathFileName;
								theOutputFileStreamWriter.WriteLine(materialLine);
							}

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
										materialLine = materialFileName;
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
			SourceMdlBone aBone = null;
			SourcePhyFace aTriangle = null;
			SourcePhyFaceSection faceSection = null;
			SourcePhyVertex phyVertex = null;
			SourceVector aVectorTransformed = null;

			try
			{
				if (thePhyFileData.theSourcePhyCollisionDatas != null)
				{
					ProcessTransformsForPhysics();

					for (int collisionDataIndex = 0; collisionDataIndex < thePhyFileData.theSourcePhyCollisionDatas.Count; collisionDataIndex++)
					{
						collisionData = thePhyFileData.theSourcePhyCollisionDatas[collisionDataIndex];

						for (int faceSectionIndex = 0; faceSectionIndex < collisionData.theFaceSections.Count; faceSectionIndex++)
						{
							faceSection = collisionData.theFaceSections[faceSectionIndex];

							if (faceSection.theBoneIndex >= theMdlFileData.theBones.Count)
							{
								continue;
							}
							aBone = theMdlFileData.theBones[faceSection.theBoneIndex];

							for (int triangleIndex = 0; triangleIndex < faceSection.theFaces.Count; triangleIndex++)
							{
								aTriangle = faceSection.theFaces[triangleIndex];

								line = "  phy";
								theOutputFileStreamWriter.WriteLine(line);

								//  19 -0.000009 0.000001 0.999953 0.0 0.0 0.0 1 0
								for (int vertexIndex = 0; vertexIndex < aTriangle.vertexIndex.Length; vertexIndex++)
								{
									//phyVertex = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex))
									phyVertex = faceSection.theVertices[aTriangle.vertexIndex[vertexIndex]];

									aVectorTransformed = TransformPhyVertex(aBone, phyVertex.vertex);

									line = "    ";
									line += faceSection.theBoneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
									line += " ";
									line += aVectorTransformed.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
									line += " ";
									line += aVectorTransformed.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
									line += " ";
									line += aVectorTransformed.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

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

		//TODO: Write the firstAnimDesc's first frame's frameLines because it is used for "subtract" option.
		public void CalculateFirstAnimDescFrameLinesForSubtract()
		{
			int boneIndex = 0;
			AnimationFrameLine aFrameLine = null;
			int frameIndex = 0;
			SourceMdlSequenceDesc aSequenceDesc = null;
			SourceMdlAnimationDesc52 anAnimationDesc = null;

			aSequenceDesc = null;
			anAnimationDesc = theMdlFileData.theFirstAnimationDesc;

			theAnimationFrameLines = new SortedList<int, AnimationFrameLine>();
			frameIndex = 0;
			theAnimationFrameLines.Clear();
			//If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
			CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex);
			//End If

			for (int i = 0; i < theAnimationFrameLines.Count; i++)
			{
				boneIndex = theAnimationFrameLines.Keys[i];
				aFrameLine = theAnimationFrameLines.Values[i];

				AnimationFrameLine aFirstAnimationDescFrameLine = new AnimationFrameLine();
				aFirstAnimationDescFrameLine.rotation = new SourceVector();
				aFirstAnimationDescFrameLine.position = new SourceVector();

				//NOTE: Only rotate by -90 deg if bone is a root bone.  Do not know why.
				//If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
				//TEST: Try this version, because of "sequence_blend from Game Zombie" model.
				aFirstAnimationDescFrameLine.rotation.x = aFrameLine.rotation.x;
				aFirstAnimationDescFrameLine.rotation.y = aFrameLine.rotation.y;
				if (theMdlFileData.theBones[boneIndex].parentBoneIndex == -1 && (aFrameLine.rotation.debug_text.StartsWith("raw") || aFrameLine.rotation.debug_text == "anim+bone"))
				{
					double z = aFrameLine.rotation.z;
					z += MathModule.DegreesToRadians(-90);
					aFirstAnimationDescFrameLine.rotation.z = z;
				}
				else
				{
					aFirstAnimationDescFrameLine.rotation.z = aFrameLine.rotation.z;
				}

				//NOTE: Only adjust position if bone is a root bone. Do not know why.
				//If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
				//TEST: Try this version, because of "sequence_blend from Game Zombie" model.
				if (theMdlFileData.theBones[boneIndex].parentBoneIndex == -1 && (aFrameLine.position.debug_text.StartsWith("raw") || aFrameLine.rotation.debug_text == "anim+bone"))
				{
					aFirstAnimationDescFrameLine.position.x = aFrameLine.position.y;
					aFirstAnimationDescFrameLine.position.y = (-aFrameLine.position.x);
					aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z;
				}
				else
				{
					aFirstAnimationDescFrameLine.position.x = aFrameLine.position.x;
					aFirstAnimationDescFrameLine.position.y = aFrameLine.position.y;
					aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z;
				}

				theMdlFileData.theFirstAnimationDescFrameLines.Add(boneIndex, aFirstAnimationDescFrameLine);
			}
		}

		public void WriteSkeletonSectionForAnimation(SourceMdlSequenceDescBase aSequenceDescBase, SourceMdlAnimationDescBase anAnimationDescBase)
		{
			int boneIndex;
			AnimationFrameLine aFrameLine;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();
			SourceMdlSequenceDesc aSequenceDesc = (SourceMdlSequenceDesc)aSequenceDescBase;
			SourceMdlAnimationDesc52 anAnimationDesc = (SourceMdlAnimationDesc52)anAnimationDescBase;

			//skeleton
			string line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			if (theMdlFileData.theBones != null)
			{
				theAnimationFrameLines = new SortedList<int, AnimationFrameLine>();
				for (int frameIndex = 0; frameIndex < anAnimationDesc.frameCount; frameIndex++)
				{
					theAnimationFrameLines.Clear();

					if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_FRAMEANIM) != 0)
					{
						int sectionIndex;
						int sectionFrameIndex;
						if (anAnimationDesc.sectionFrameCount == 0)
						{
							sectionIndex = 0;
							sectionFrameIndex = frameIndex;
						}
						else
						{
							sectionIndex = Convert.ToInt32(Math.Truncate(frameIndex / (double)anAnimationDesc.sectionFrameCount));
							sectionFrameIndex = frameIndex - (sectionIndex * anAnimationDesc.sectionFrameCount);
						}

						SourceAniFrameAnim52 aSectionOfAnimation = anAnimationDesc.theSectionsOfFrameAnim[sectionIndex];
						for (boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
						{
							SourceMdlBone aBone = theMdlFileData.theBones[boneIndex];

							aFrameLine = new AnimationFrameLine();
							theAnimationFrameLines.Add(boneIndex, aFrameLine);

							aFrameLine.position = new SourceVector();
							aFrameLine.rotation = new SourceVector();

							if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_DELTA) > 0)
							{
								aFrameLine.position.x = 0;
								aFrameLine.position.y = 0;
								aFrameLine.position.z = 0;
								aFrameLine.position.debug_text = "ZERO";
								aFrameLine.rotation.x = 0;
								aFrameLine.rotation.y = 0;
								aFrameLine.rotation.z = 0;
								aFrameLine.rotation.debug_text = "ZERO";
							}
							else
							{
								aFrameLine.position.x = aBone.position.x;
								aFrameLine.position.y = aBone.position.y;
								aFrameLine.position.z = aBone.position.z;
								aFrameLine.position.debug_text = "BONE";
								aFrameLine.rotation.x = aBone.rotation.x;
								aFrameLine.rotation.y = aBone.rotation.y;
								aFrameLine.rotation.z = aBone.rotation.z;
								aFrameLine.rotation.debug_text = "BONE";
							}

							int boneFlag = aSectionOfAnimation.theBoneFlags[boneIndex];
							if (aSectionOfAnimation.theBoneConstantInfos != null)
							{
								BoneConstantInfo49 aBoneConstantInfo = aSectionOfAnimation.theBoneConstantInfos[boneIndex];
								if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_RAWROT) > 0)
								{
									aFrameLine.rotation = MathModule.ToEulerAngles(aBoneConstantInfo.theConstantRawRot.quaternion);
									aFrameLine.rotation.debug_text = "RAWROT";
								}
								if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_RAWPOS) > 0)
								{
									aFrameLine.position.x = aBoneConstantInfo.theConstantRawPos.x;
									aFrameLine.position.y = aBoneConstantInfo.theConstantRawPos.y;
									aFrameLine.position.z = aBoneConstantInfo.theConstantRawPos.z;
									aFrameLine.position.debug_text = "RAWPOS";
								}
								if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_CONST_ROT2) > 0)
								{
									aFrameLine.rotation = MathModule.ToEulerAngles(aBoneConstantInfo.theConstantRotationUnknown.quaternion);
									aFrameLine.rotation.debug_text = "FRAME_CONST_ROT2";
								}
							}
							if (aSectionOfAnimation.theBoneFrameDataInfos != null)
							{
								BoneFrameDataInfo49 aBoneFrameDataInfo = aSectionOfAnimation.theBoneFrameDataInfos[sectionFrameIndex][boneIndex];
								//If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIMROT) > 0 Then
								if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_FULLANIMPOS) > 0)
								{
									aFrameLine.rotation = MathModule.ToEulerAngles(aBoneFrameDataInfo.theAnimRotation.quaternion);
									aFrameLine.rotation.debug_text = "ANIMROT";
								}
								if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIMPOS) > 0)
								{
									aFrameLine.position.x = aBoneFrameDataInfo.theAnimPosition.x;
									aFrameLine.position.y = aBoneFrameDataInfo.theAnimPosition.y;
									aFrameLine.position.z = aBoneFrameDataInfo.theAnimPosition.z;
									aFrameLine.position.debug_text = "ANIMPOS";
								}
								//If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_FULLANIMPOS) > 0 Then
								if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIMROT) > 0)
								{
									//aFrameLine.position.x = aBoneFrameDataInfo.theFullAnimPosition.x
									//aFrameLine.position.y = aBoneFrameDataInfo.theFullAnimPosition.y
									//aFrameLine.position.z = aBoneFrameDataInfo.theFullAnimPosition.z
									aFrameLine.position.x = aBoneFrameDataInfo.theAnimPosition.x;
									aFrameLine.position.y = aBoneFrameDataInfo.theAnimPosition.y;
									aFrameLine.position.z = aBoneFrameDataInfo.theAnimPosition.z;
									aFrameLine.position.debug_text = "FULLANIMPOS";
								}
								if ((boneFlag & 0x20) > 0)
								{
									int unknownFlagIsUsed = 4242;
								}
								if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIM_ROT2) > 0)
								{
									aFrameLine.rotation = MathModule.ToEulerAngles(aBoneFrameDataInfo.theAnimRotationUnknown.quaternion);
									aFrameLine.rotation.debug_text = "FRAME_ANIM_ROT2";
								}
							}
						}
					}
					else
						CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex);

					if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
						line = "time ";
					else
						line = "  time ";
					line += frameIndex.ToString();
					theOutputFileStreamWriter.WriteLine(line);

					for (int i = 0; i < theAnimationFrameLines.Count; i++)
					{
						boneIndex = theAnimationFrameLines.Keys[i];
						aFrameLine = theAnimationFrameLines.Values[i];

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
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private void AdjustPositionAndRotation(int boneIndex, SourceVector iPosition, SourceVector iRotation, ref SourceVector oPosition, ref SourceVector oRotation)
		{
			SourceMdlBone aBone = theMdlFileData.theBones[boneIndex];

			//If iPosition.debug_text = "desc_delta" OrElse iPosition.debug_text.StartsWith("delta") Then
			//	Dim aFirstAnimationDescFrameLine As AnimationFrameLine
			//	aFirstAnimationDescFrameLine = Me.theMdlFileData.theFirstAnimationDescFrameLines(boneIndex)

			//	oPosition.x = iPosition.x + aFirstAnimationDescFrameLine.position.x
			//	oPosition.y = iPosition.y + aFirstAnimationDescFrameLine.position.y
			//	oPosition.z = iPosition.z + aFirstAnimationDescFrameLine.position.z
			//Else
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

			//If iRotation.debug_text = "desc_delta" OrElse iRotation.debug_text.StartsWith("delta") Then
			//	Dim aFirstAnimationDescFrameLine As AnimationFrameLine
			//	aFirstAnimationDescFrameLine = Me.theMdlFileData.theFirstAnimationDescFrameLines(boneIndex)

			//	Dim quat As New SourceQuaternion()
			//	Dim quat2 As New SourceQuaternion()
			//	Dim quatResult As New SourceQuaternion()
			//	Dim magnitude As Double
			//	quat = MathModule.EulerAnglesToQuaternion(iRotation)
			//	quat2 = MathModule.EulerAnglesToQuaternion(aFirstAnimationDescFrameLine.rotation)

			//	quat.x *= -1
			//	quat.y *= -1
			//	quat.z *= -1
			//	quatResult.x = quat.w * quat2.x + quat.x * quat2.w + quat.y * quat2.z - quat.z * quat2.y
			//	quatResult.y = quat.w * quat2.y - quat.x * quat2.z + quat.y * quat2.w + quat.z * quat2.x
			//	quatResult.z = quat.w * quat2.z + quat.x * quat2.y - quat.y * quat2.x + quat.z * quat2.w
			//	quatResult.w = quat.w * quat2.w + quat.x * quat2.x + quat.y * quat2.y - quat.z * quat2.z

			//	magnitude = Math.Sqrt(quatResult.w * quatResult.w + quatResult.x * quatResult.x + quatResult.y * quatResult.y + quatResult.z * quatResult.z)
			//	quatResult.x /= magnitude
			//	quatResult.y /= magnitude
			//	quatResult.z /= magnitude
			//	quatResult.w /= magnitude

			//	Dim tempRotation As New SourceVector()
			//	tempRotation = MathModule.ToEulerAngles(quatResult)
			//	oRotation.x = tempRotation.y
			//	oRotation.y = tempRotation.z
			//	oRotation.z = tempRotation.x
			//Else
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

		private void AdjustPositionAndRotationByPiecewiseMovement(int frameIndex, int boneIndex, List<SourceMdlMovement> movements, SourceVector iPosition, SourceVector iRotation, ref SourceVector oPosition, ref SourceVector oRotation)
		{
			SourceMdlBone aBone = theMdlFileData.theBones[boneIndex];

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

					//TEST: UNTESTED on MDL v52 - Works in MDL v49 for translation and maybe rotation on Z, but ignores other axis rotations because the above does not work for rotations.
					oPosition.x = iPosition.x + vecPos.x;
					oPosition.y = iPosition.y + vecPos.y;
					oPosition.z = iPosition.z + vecPos.z;
					oRotation.z = iRotation.z + vecAngle.y;
				}
			}
		}

		private void AdjustPositionAndRotationForDelta(SourceVector iPosition, SourceVector iRotation, ref SourceVector oPosition, ref SourceVector oRotation)
		{

		}

		private string WriteVertexLine(SourceVtxStripGroup07 aStripGroup, int aVtxIndexIndex, int lodIndex, int meshVertexIndexStart, int bodyPartVertexIndexStart)
		{
			ushort aVtxVertexIndex = 0;
			SourceVtxVertex07 aVtxVertex = null;
			SourceVertex aVertex = null;
			int vertexIndex = 0;
			string line = "";

			try
			{
				aVtxVertexIndex = aStripGroup.theVtxIndexes[aVtxIndexIndex];
				aVtxVertex = aStripGroup.theVtxVertexes[aVtxVertexIndex];
				vertexIndex = aVtxVertex.originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart;
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
					//aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
				}

				line = "  ";
				line += aVertex.boneWeight.bone[0].ToString(MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
				{
					//NOTE: This does not work for L4D2 w_models\weapons\w_minigun.mdl.
					line += aVertex.positionY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += (-aVertex.positionX).ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				else
				{
					//NOTE: This works for L4D2 w_models\weapons\w_minigun.mdl.
					line += aVertex.positionX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.positionY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				line += " ";
				line += aVertex.positionZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
				{
					line += aVertex.normalY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += (-aVertex.normalX).ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				else
				{
					line += aVertex.normalX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normalY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				line += " ";
				line += aVertex.normalZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

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

		private void CalculateFirstAnimDescFrameLinesForPhysics(ref AnimationFrameLine aFirstAnimationDescFrameLine)
		{
			int boneIndex = 0;
			AnimationFrameLine aFrameLine = null;
			int frameIndex = 0;
			int frameLineIndex = 0;
			SourceMdlSequenceDesc aSequenceDesc = null;
			SourceMdlAnimationDesc52 anAnimationDesc = null;

			aSequenceDesc = null;
			anAnimationDesc = theMdlFileData.theAnimationDescs[0];

			theAnimationFrameLines = new SortedList<int, AnimationFrameLine>();
			frameIndex = 0;
			theAnimationFrameLines.Clear();
			//If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
			CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex);
			//End If

			frameLineIndex = 0;
			boneIndex = theAnimationFrameLines.Keys[frameLineIndex];
			aFrameLine = theAnimationFrameLines.Values[frameLineIndex];

			aFirstAnimationDescFrameLine.rotation = new SourceVector();
			aFirstAnimationDescFrameLine.position = new SourceVector();

			aFirstAnimationDescFrameLine.rotation.x = aFrameLine.rotation.x;
			aFirstAnimationDescFrameLine.rotation.y = aFrameLine.rotation.y;
			//If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
			//	Dim z As Double
			//	z = aFrameLine.rotation.z
			//	z += MathModule.DegreesToRadians(-90)
			//	aFirstAnimationDescFrameLine.rotation.z = z
			//Else
			aFirstAnimationDescFrameLine.rotation.z = aFrameLine.rotation.z;
			//End If

			//'NOTE: Only adjust position if bone is a root bone. Do not know why.
			//'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
			//'TEST: Try this version, because of "sequence_blend from Game Zombie" model.
			//If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
			//	aFirstAnimationDescFrameLine.position.x = aFrameLine.position.y
			//	aFirstAnimationDescFrameLine.position.y = (-aFrameLine.position.x)
			//	aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
			//Else
			aFirstAnimationDescFrameLine.position.x = aFrameLine.position.x;
			aFirstAnimationDescFrameLine.position.y = aFrameLine.position.y;
			aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z;
			//End If
		}

		private void ProcessTransformsForPhysics()
		{
			if (thePhyFileData.theSourcePhyIsCollisionModel)
			{
				AnimationFrameLine aFirstAnimationDescFrameLine = new AnimationFrameLine();
				CalculateFirstAnimDescFrameLinesForPhysics(ref aFirstAnimationDescFrameLine);

				SourceVector position = null;
				SourceVector rotation = null;
				position = aFirstAnimationDescFrameLine.position;
				rotation = aFirstAnimationDescFrameLine.rotation;

				//MathModule.AngleMatrix(rotation.y, rotation.z, rotation.x, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				//MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				//Me.worldToPoseColumn3.x = position.x
				//Me.worldToPoseColumn3.y = position.y
				//Me.worldToPoseColumn3.z = position.z
				//------
				//MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
				//poseToWorldColumn3.x = position.x
				//poseToWorldColumn3.y = position.y
				//poseToWorldColumn3.z = position.z
				//MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				//------
				MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z + MathModule.DegreesToRadians(-90), ref poseToWorldColumn0, ref poseToWorldColumn1, ref poseToWorldColumn2, ref poseToWorldColumn3);
				poseToWorldColumn3.x = position.y;
				poseToWorldColumn3.y = -position.x;
				poseToWorldColumn3.z = position.z;
				MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, ref worldToPoseColumn0, ref worldToPoseColumn1, ref worldToPoseColumn2, ref worldToPoseColumn3);
			}
		}

		//NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used in VPHYSICS.DLL for each face:
		//      convertedZ = 1.0 / 0.0254 * lastVertex.position.z
		//      convertedY = 1.0 / 0.0254 * -lastVertex.position.y
		//      convertedX = 1.0 / 0.0254 * lastVertex.position.x
		//NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used after above for each vertex:
		//      newValue1 = unknownZ1 * convertedZ + unknownY1 * convertedY + unknownX1 * convertedX + unknownW1
		//      newValue2 = unknownZ2 * convertedZ + unknownY2 * convertedY + unknownX2 * convertedX + unknownW2
		//      newValue3 = unknownZ3 * convertedZ + unknownY3 * convertedY + unknownX3 * convertedX + unknownW3
		//Seems to be same as this code:
		//Dim aBone As SourceMdlBone
		//aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(anEyeball.boneIndex)
		//eyeballPosition = MathModule.VectorITransform(anEyeball.org, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		private SourceVector TransformPhyVertex(SourceMdlBone aBone, SourceVector vertex)
		{
			SourceVector aVectorTransformed = new SourceVector();
			SourceVector aVector = new SourceVector();

			//NOTE: Too small.
			//aVectorTransformed.x = vertex.x
			//aVectorTransformed.y = vertex.y
			//aVectorTransformed.z = vertex.z
			//------
			//NOTE: Rotated for:
			//      simple_shape
			//      L4D2 w_models\weapons\w_minigun
			//aVectorTransformed.x = 1 / 0.0254 * vertex.x
			//aVectorTransformed.y = 1 / 0.0254 * vertex.y
			//aVectorTransformed.z = 1 / 0.0254 * vertex.z
			//------
			//NOTE: Works for:
			//      simple_shape
			//      L4D2 w_models\weapons\w_minigun
			//      L4D2 w_models\weapons\w_smg_uzi
			//      L4D2 props_vehicles\van
			//aVectorTransformed.x = 1 / 0.0254 * vertex.z
			//aVectorTransformed.y = 1 / 0.0254 * -vertex.x
			//aVectorTransformed.z = 1 / 0.0254 * -vertex.y
			//------
			//NOTE: Rotated for:
			//      L4D2 w_models\weapons\w_minigun
			//aVectorTransformed.x = 1 / 0.0254 * vertex.x
			//aVectorTransformed.y = 1 / 0.0254 * -vertex.y
			//aVectorTransformed.z = 1 / 0.0254 * vertex.z
			//------
			//NOTE: Rotated for:
			//      L4D2 props_vehicles\van
			//aVectorTransformed.x = 1 / 0.0254 * vertex.z
			//aVectorTransformed.y = 1 / 0.0254 * -vertex.y
			//aVectorTransformed.z = 1 / 0.0254 * vertex.x
			//------
			//NOTE: Rotated for:
			//      L4D2 w_models\weapons\w_minigun
			//aVector.x = 1 / 0.0254 * vertex.x
			//aVector.y = 1 / 0.0254 * vertex.y
			//aVector.z = 1 / 0.0254 * vertex.z
			//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			//------
			//NOTE: Rotated for:
			//      L4D2 w_models\weapons\w_minigun
			//aVector.x = 1 / 0.0254 * vertex.x
			//aVector.y = 1 / 0.0254 * -vertex.y
			//aVector.z = 1 / 0.0254 * vertex.z
			//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			//------
			//NOTE: Works for:
			//      L4D2 w_models\weapons\w_minigun
			//      L4D2 w_models\weapons\w_smg_uzi
			//NOTE: Rotated for:
			//      simple_shape
			//      L4D2 props_vehicles\van
			//NOTE: Each mesh piece rotated for:
			//      L4D2 survivors\survivor_producer
			//aVector.x = 1 / 0.0254 * vertex.z
			//aVector.y = 1 / 0.0254 * -vertex.y
			//aVector.z = 1 / 0.0254 * vertex.x
			//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			//------
			//NOTE: Works for:
			//      simple_shape
			//      L4D2 props_vehicles\van
			//      L4D2 survivors\survivor_producer
			//      L4D2 w_models\weapons\w_autoshot_m4super
			//      L4D2 w_models\weapons\w_desert_eagle
			//      L4D2 w_models\weapons\w_minigun
			//      L4D2 w_models\weapons\w_rifle_m16a2
			//      L4D2 w_models\weapons\w_smg_uzi
			//NOTE: Rotated for:
			//      L4D2 w_models\weapons\w_desert_rifle
			//      L4D2 w_models\weapons\w_shotgun_spas
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
			//------
			if (thePhyFileData.theSourcePhyIsCollisionModel)
			{
				//Dim copyOfVector As New SourceVector()
				//'copyOfVector.x = 1 / 0.0254 * vertex.x
				//'copyOfVector.y = 1 / 0.0254 * vertex.y
				//'copyOfVector.z = 1 / 0.0254 * vertex.z
				//'copyOfVector.x = 1 / 0.0254 * vertex.x
				//'copyOfVector.y = 1 / 0.0254 * vertex.z
				//'copyOfVector.z = 1 / 0.0254 * -vertex.y
				//copyOfVector.x = 1 / 0.0254 * vertex.z
				//copyOfVector.y = 1 / 0.0254 * -vertex.x
				//copyOfVector.z = 1 / 0.0254 * -vertex.y
				//aVector = MathModule.VectorTransform(copyOfVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				//'aVector.x = 1 / 0.0254 * vertex.z
				//'aVector.y = 1 / 0.0254 * -vertex.x
				//'aVector.z = 1 / 0.0254 * -vertex.y
				//'aVectorTransformed.x = 1 / 0.0254 * vertex.z
				//'aVectorTransformed.y = 1 / 0.0254 * -vertex.x
				//'aVectorTransformed.z = 1 / 0.0254 * -vertex.y

				//Dim debug As Integer = 4242

				//'Dim temp As Double
				//'temp = aVector.y
				//'aVector.y = aVector.z
				//'aVector.z = -temp
				//'------
				//'aVector.y = -aVector.y
				//'------
				//'Dim temp As Double
				//'temp = aVector.x
				//'aVector.x = aVector.z
				//'aVector.z = -aVector.y
				//'aVector.y = -temp
				//'------
				//'Dim temp As Double
				//'temp = aVector.x
				//'aVectorTransformed.x = aVector.z
				//'aVectorTransformed.z = -aVector.y
				//'aVectorTransformed.y = -temp

				//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

				//[2017-12-22]
				// Correct for cube_like_mesh
				//aVectorTransformed.x = 1 / 0.0254 * vertex.z
				//aVectorTransformed.y = 1 / 0.0254 * -vertex.x
				//aVectorTransformed.z = 1 / 0.0254 * -vertex.y
				//------
				// Correct for L4D2 w_models/weapons/w_desert_rifle.mdl
				//aVectorTransformed.x = 1 / 0.0254 * vertex.z
				//aVectorTransformed.y = 1 / 0.0254 * -vertex.y
				//aVectorTransformed.z = 1 / 0.0254 * vertex.x
				//------
				//aVector.x = 1 / 0.0254 * vertex.z
				//aVector.y = 1 / 0.0254 * -vertex.x
				//aVector.z = 1 / 0.0254 * -vertex.y
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.z
				//aVector.z = 1 / 0.0254 * -vertex.y
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.y
				//aVector.z = 1 / 0.0254 * vertex.z
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * -vertex.z
				//aVector.z = 1 / 0.0254 * vertex.y
				//aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.z
				//aVector.z = 1 / 0.0254 * -vertex.y
				//aVector.x = 1 / 0.0254 * vertex.z
				//aVector.y = 1 / 0.0254 * -vertex.x
				//aVector.z = 1 / 0.0254 * -vertex.y
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.y
				//aVector.z = 1 / 0.0254 * vertex.z
				//aVectorTransformed = MathModule.VectorITransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.y
				//aVector.z = 1 / 0.0254 * vertex.z
				//aVector.x = 1 / 0.0254 * vertex.x  
				//aVector.y = 1 / 0.0254 * vertex.z  
				//aVector.z = 1 / 0.0254 * -vertex.y 
				//aVector.x = 1 / 0.0254 * -vertex.x
				//aVector.y = 1 / 0.0254 * vertex.z
				//aVector.z = 1 / 0.0254 * vertex.y
				//aVector.x = 1 / 0.0254 * vertex.y
				//aVector.y = 1 / 0.0254 * -vertex.x
				//aVector.z = 1 / 0.0254 * vertex.z
				//aVector.x = 1 / 0.0254 * -vertex.y
				//aVector.y = 1 / 0.0254 * vertex.x
				//aVector.z = 1 / 0.0254 * vertex.z
				//aVector.x = 1 / 0.0254 * vertex.y
				//aVector.y = 1 / 0.0254 * -vertex.x
				//aVector.z = 1 / 0.0254 * -vertex.z   
				//aVector.x = 1 / 0.0254 * vertex.y
				//aVector.y = 1 / 0.0254 * vertex.x
				//aVector.z = 1 / 0.0254 * -vertex.z
				// Correct for cube_like_mesh
				// Correct for L4D2 w_models/weapons/w_desert_rifle.mdl
				// Incorrect for L4D2 w_models/weapons/w_rifle_m16a2.mdl
				//aVector.x = 1 / 0.0254 * -vertex.y
				//aVector.y = 1 / 0.0254 * -vertex.x
				//aVector.z = 1 / 0.0254 * -vertex.z
				//aVectorTransformed = MathModule.VectorITransform(aVector, Me.poseToWorldColumn0, Me.poseToWorldColumn1, Me.poseToWorldColumn2, Me.poseToWorldColumn3)
				//======
				//'FROM: collisionmodel.cpp ConvertToWorldSpace()
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.z
				//aVector.z = 1 / 0.0254 * -vertex.y
				//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				//'Dim worldToBoneColumn0 As New SourceVector()
				//'Dim worldToBoneColumn1 As New SourceVector()
				//'Dim worldToBoneColumn2 As New SourceVector()
				//'Dim worldToBoneColumn3 As New SourceVector()
				//'MathModule.AngleMatrix(aBone.rotation.x, aBone.rotation.y, aBone.rotation.z, worldToBoneColumn0, worldToBoneColumn1, worldToBoneColumn2, worldToBoneColumn3)
				//'worldToBoneColumn3.x = aBone.position.x
				//'worldToBoneColumn3.y = aBone.position.y
				//'worldToBoneColumn3.z = aBone.position.z
				//'aVector.x = aVectorTransformed.x
				//'aVector.y = aVectorTransformed.y
				//'aVector.z = aVectorTransformed.z
				//'aVectorTransformed = MathModule.VectorTransform(aVector, worldToBoneColumn0, worldToBoneColumn1, worldToBoneColumn2, worldToBoneColumn3)
				//aVector.x = aVectorTransformed.x
				//aVector.y = aVectorTransformed.y
				//aVector.z = aVectorTransformed.z
				//aVectorTransformed = MathModule.VectorTransform(aVector, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
				//======
				//'FROM: collisionmodel.cpp ConvertToWorldSpace()
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.z
				//aVector.z = 1 / 0.0254 * -vertex.y
				//aVectorTransformed = MathModule.VectorTransform(aVector, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
				//aVector.x = aVectorTransformed.x
				//aVector.y = aVectorTransformed.y
				//aVector.z = aVectorTransformed.z
				//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				//======
				//'FROM: collisionmodel.cpp ConvertToWorldSpace()
				// ''NOTE: These 3 lines work for airport_fuel_truck, ambulance, and army_truck, but not w_desert_file and w_rifle_m16a2.
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.z
				//aVector.z = 1 / 0.0254 * -vertex.y
				// ''NOTE: These 3 lines work for w_desert_file and w_rifle_m16a2, but not ambulance and army_truck.
				//'aVector.x = 1 / 0.0254 * vertex.x
				//'aVector.y = 1 / 0.0254 * -vertex.y
				//'aVector.z = 1 / 0.0254 * vertex.z
				//aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				//aVector.x = aVectorTransformed.x
				//aVector.y = aVectorTransformed.y
				//aVector.z = aVectorTransformed.z
				//'aVector.x = aVectorTransformed.x
				//'aVector.y = aVectorTransformed.z
				//'aVector.z = -aVectorTransformed.y
				//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				//======
				//FROM: collisionmodel.cpp ConvertToWorldSpace()
				if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
				{
					//NOTE: These 3 lines do not work for airport_fuel_truck, ambulance, and army_truck.
					//aVector.x = 1 / 0.0254 * vertex.x
					//aVector.y = 1 / 0.0254 * vertex.z
					//aVector.z = 1 / 0.0254 * -vertex.y
					//aVector.x = 1 / 0.0254 * vertex.y
					//aVector.y = 1 / 0.0254 * -vertex.x
					//aVector.z = 1 / 0.0254 * vertex.z
					//aVector.x = 1 / 0.0254 * -vertex.y
					//aVector.y = 1 / 0.0254 * -vertex.x
					//aVector.z = 1 / 0.0254 * vertex.z
					//aVector.x = 1 / 0.0254 * vertex.x
					//aVector.y = 1 / 0.0254 * vertex.y
					//aVector.z = 1 / 0.0254 * vertex.z
					//aVector.x = 1 / 0.0254 * vertex.x
					//aVector.y = 1 / 0.0254 * vertex.z
					//aVector.z = 1 / 0.0254 * vertex.y
					//' Still need a rotate 90 on the z.
					//aVector.x = 1 / 0.0254 * vertex.x
					//aVector.y = 1 / 0.0254 * -vertex.y
					//aVector.z = 1 / 0.0254 * vertex.z
					//aVector.x = 1 / 0.0254 * -vertex.y
					//aVector.y = 1 / 0.0254 * vertex.x
					//aVector.z = 1 / 0.0254 * vertex.z
					//aVector.x = 1 / 0.0254 * vertex.x
					//aVector.y = 1 / 0.0254 * -vertex.z
					//aVector.z = 1 / 0.0254 * vertex.y
					//aVector.x = 1 / 0.0254 * vertex.z
					//aVector.y = 1 / 0.0254 * -vertex.x
					//aVector.z = 1 / 0.0254 * -vertex.y

					//aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
					//aVector.x = aVectorTransformed.x
					//aVector.y = aVectorTransformed.y
					//aVector.z = aVectorTransformed.z
					//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

					//------

					//TEST: Works for props_vehciles that use $staticprop.
					aVector.x = 1 / 0.0254 * vertex.z;
					aVector.y = 1 / 0.0254 * -vertex.x;
					aVector.z = 1 / 0.0254 * -vertex.y;
					aVectorTransformed = MathModule.VectorTransform(aVector, worldToPoseColumn0, worldToPoseColumn1, worldToPoseColumn2, worldToPoseColumn3);
					aVector.x = aVectorTransformed.x;
					aVector.y = aVectorTransformed.z;
					aVector.z = -aVectorTransformed.y;
					aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3);
				}
				else
				{
					//NOTE: These 3 lines work for w_desert_file and w_rifle_m16a2, but not ambulance and army_truck.
					//TEST: Did not work for 50_cal. Rotated 180. Original model has phys mesh rotated oddly, anyway.
					//TEST: Incorrect. Need to 180 on the Y and -1 on scale Z. Noticable on w_minigun.
					//aVector.x = 1 / 0.0254 * vertex.x
					//aVector.y = 1 / 0.0254 * -vertex.y
					//aVector.z = 1 / 0.0254 * vertex.z
					//TEST: Incorrect. Need to 180 on the Y. Noticable on w_minigun.
					//aVector.x = 1 / 0.0254 * vertex.x
					//aVector.y = 1 / 0.0254 * vertex.y
					//aVector.z = 1 / 0.0254 * vertex.z
					//TEST: Incorrect. Need to -1 on scale Z. Noticable on w_minigun.
					//aVector.x = 1 / 0.0254 * vertex.x
					//aVector.y = 1 / 0.0254 * vertex.y
					//aVector.z = 1 / 0.0254 * -vertex.z
					//TEST: Works for w_minigun.
					//TEST: Works for 50cal, but be aware that the phys mesh does not look right for the model. It does look like original model, though.
					//TEST: Does not work for Garry's Mod addon "dodge_daytona" 236224475.
					aVector.x = 1 / 0.0254 * vertex.x;
					aVector.y = 1 / 0.0254 * -vertex.y;
					aVector.z = 1 / 0.0254 * -vertex.z;

					aVectorTransformed = MathModule.VectorTransform(aVector, worldToPoseColumn0, worldToPoseColumn1, worldToPoseColumn2, worldToPoseColumn3);
					aVector.x = aVectorTransformed.x;
					aVector.y = aVectorTransformed.y;
					aVector.z = aVectorTransformed.z;
					aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3);
				}
			}
			else
			{
				//FROM: collisionmodel.cpp ConvertToBoneSpace()
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.y
				//aVector.z = 1 / 0.0254 * vertex.z
				aVector.x = 1 / 0.0254 * vertex.x;
				aVector.y = 1 / 0.0254 * vertex.z;
				aVector.z = 1 / 0.0254 * -vertex.y;
				aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3);
			}

			//If Me.theMdlFileData.theBoneTransforms IsNot Nothing Then
			//	Dim transform As SourceMdlBoneTransform
			//	Dim boneIndex As Integer
			//	Dim preToBoneColumn0 As New SourceVector()
			//	Dim preToBoneColumn1 As New SourceVector()
			//	Dim preToBoneColumn2 As New SourceVector()
			//	Dim preToBoneColumn3 As New SourceVector()
			//	Dim boneToPostColumn0 As New SourceVector()
			//	Dim boneToPostColumn1 As New SourceVector()
			//	Dim boneToPostColumn2 As New SourceVector()
			//	Dim boneToPostColumn3 As New SourceVector()

			//	'TODO: Find the real boneIndex based on boneName
			//	boneIndex = 0
			//	transform = Me.theMdlFileData.theBoneTransforms(boneIndex)

			//	'MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			//	MathModule.R_ConcatTransforms(transform.preTransformColumn0, transform.preTransformColumn1, transform.preTransformColumn2, transform.preTransformColumn3, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3, preToBoneColumn0, preToBoneColumn1, preToBoneColumn2, preToBoneColumn3)
			//	MathModule.R_ConcatTransforms(preToBoneColumn0, preToBoneColumn1, preToBoneColumn2, preToBoneColumn3, transform.postTransformColumn0, transform.postTransformColumn1, transform.postTransformColumn2, transform.postTransformColumn3, boneToPostColumn0, boneToPostColumn1, boneToPostColumn2, boneToPostColumn3)
			//	aVectorTransformed = MathModule.VectorITransform(aVector, boneToPostColumn0, boneToPostColumn1, boneToPostColumn2, boneToPostColumn3)
			//Else
			//	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			//End If



			//------
			//NOTE: Works for:
			//      survivor_producer
			//NOTE: Does not work for:
			//      w_smg_uzi()
			//phyVertex.x = 1 / 0.0254 * aVector.x
			//phyVertex.y = 1 / 0.0254 * aVector.z
			//phyVertex.z = 1 / 0.0254 * -aVector.y
			//------
			//NOTE: These two lines match orientation for cstrike it_lampholder1 model, 
			//      but still doesn't compile properly.
			//NOTE: Does not work for:
			//      w_smg_uzi()
			//phyVertex.x = 1 / 0.0254 * aVector.z
			//phyVertex.y = 1 / 0.0254 * -aVector.x
			//phyVertex.z = 1 / 0.0254 * -aVector.y
			//------
			//NOTE: Does not work for:
			//      w_smg_uzi()
			//phyVertex.x = 1 / 0.0254 * aVector.y
			//phyVertex.y = 1 / 0.0254 * aVector.x
			//phyVertex.z = 1 / 0.0254 * -aVector.z
			//------
			//NOTE: Does not work for:
			//      w_smg_uzi()
			//phyVertex.x = 1 / 0.0254 * aVector.x
			//phyVertex.y = 1 / 0.0254 * aVector.y
			//phyVertex.z = 1 / 0.0254 * -aVector.z
			//------
			//NOTE: Does not work for:
			//      w_smg_uzi()
			//phyVertex.x = 1 / 0.0254 * -aVector.y
			//phyVertex.y = 1 / 0.0254 * aVector.x
			//phyVertex.z = 1 / 0.0254 * aVector.z
			//------
			//NOTE: Does not work for:
			//      w_smg_uzi()
			//phyVertex.x = 1 / 0.0254 * -aVector.y
			//phyVertex.y = 1 / 0.0254 * aVector.x
			//phyVertex.z = 1 / 0.0254 * aVector.z
			//------
			//NOTE: Does not work for:
			//      w_smg_uzi()
			//phyVertex.x = 1 / 0.0254 * aVector.z
			//phyVertex.y = 1 / 0.0254 * aVector.y
			//phyVertex.z = 1 / 0.0254 * aVector.x
			//------
			//NOTE: Works for:
			//      w_smg_uzi()
			//NOTE: Does not work for:
			//      survivor_producer
			//phyVertex.x = 1 / 0.0254 * aVector.z
			//phyVertex.y = 1 / 0.0254 * -aVector.y
			//phyVertex.z = 1 / 0.0254 * aVector.x
			//------
			//phyVertex.x = 1 / 0.0254 * aVector.z
			//phyVertex.y = 1 / 0.0254 * -aVector.y
			//phyVertex.z = 1 / 0.0254 * -aVector.x
			//------
			//If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyIsCollisionModel Then
			//	'TEST: Does not rotate L4D2's van phys mesh correctly.
			//	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
			//	'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
			//	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
			//	'TEST:  Does not rotate L4D2's van phys mesh correctly.
			//	'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
			//	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
			//	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
			//	'TEST: Does not rotate L4D2's van phys mesh correctly.
			//	'aVector.x = 1 / 0.0254 * phyVertex.vertex.z
			//	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.y
			//	'aVector.z = 1 / 0.0254 * phyVertex.vertex.x
			//	'TEST: Does not rotate L4D2's van phys mesh correctly.
			//	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
			//	'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
			//	'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y
			//	'TEST: Works for L4D2's van phys mesh.
			//	'      Does not work for L4D2 w_model\weapons\w_minigun.mdl.
			//	aVector.x = 1 / 0.0254 * vertex.z
			//	aVector.y = 1 / 0.0254 * -vertex.x
			//	aVector.z = 1 / 0.0254 * -vertex.y
			//Else
			//	'TEST: Does not work for L4D2 w_model\weapons\w_minigun.mdl.
			//	aVector.x = 1 / 0.0254 * vertex.x
			//	aVector.y = 1 / 0.0254 * vertex.z
			//	aVector.z = 1 / 0.0254 * -vertex.y

			//	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			//End If
			//------
			//TEST: Does not rotate L4D2's van phys mesh correctly.
			//aVector.x = 1 / 0.0254 * phyVertex.vertex.x
			//aVector.y = 1 / 0.0254 * phyVertex.vertex.y
			//aVector.z = 1 / 0.0254 * phyVertex.vertex.z
			//TEST: Does not rotate L4D2's van phys mesh correctly.
			//aVector.x = 1 / 0.0254 * phyVertex.vertex.y
			//aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
			//aVector.z = 1 / 0.0254 * phyVertex.vertex.z
			//TEST: works for survivor_producer; matches ref and phy meshes of van, but both are rotated 90 degrees on z-axis
			//aVector.x = 1 / 0.0254 * phyVertex.vertex.x
			//aVector.y = 1 / 0.0254 * phyVertex.vertex.z
			//aVector.z = 1 / 0.0254 * -phyVertex.vertex.y

			//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

			return aVectorTransformed;
		}

		//Private Sub CreateAnimationFrameLines(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc52, ByVal frameIndex As Integer)

		//End Sub

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
		private void CalcAnimation(SourceMdlSequenceDesc aSequenceDesc, SourceMdlAnimationDesc52 anAnimationDesc, int frameIndex)
		{
			double s = 0;
			int animIndex = 0;
			SourceMdlBone aBone = null;
			double aWeight = 0;
			SourceMdlAnimation anAnimation = null;
			SourceVector rot = null;
			SourceVector pos = null;
			AnimationFrameLine aFrameLine = null;
			int sectionFrameIndex = 0;

			s = 0;

			animIndex = 0;

			//If anAnimationDesc.theAnimations Is Nothing OrElse animIndex >= anAnimationDesc.theAnimations.Count Then
			//	anAnimation = Nothing
			//Else
			//	anAnimation = anAnimationDesc.theAnimations(animIndex)
			//End If
			//------
			int sectionIndex = 0;
			List<SourceMdlAnimation> aSectionOfAnimation = null;
			if (anAnimationDesc.sectionFrameCount == 0)
			{
				sectionIndex = 0;
				sectionFrameIndex = frameIndex;
			}
			else
			{
				sectionIndex = Convert.ToInt32(Math.Truncate(frameIndex / (double)anAnimationDesc.sectionFrameCount));
				sectionFrameIndex = frameIndex - (sectionIndex * anAnimationDesc.sectionFrameCount);
			}
			//aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
			//If anAnimationDesc.theSectionsOfAnimations Is Nothing OrElse animIndex >= aSectionOfAnimation.Count Then
			//	anAnimation = Nothing
			//Else
			//	anAnimation = aSectionOfAnimation(animIndex)
			//End If
			anAnimation = null;
			aSectionOfAnimation = null;
			if (anAnimationDesc.theSectionsOfAnimations != null)
			{
				aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations[sectionIndex];
				if (animIndex >= 0 && animIndex < aSectionOfAnimation.Count)
				{
					anAnimation = aSectionOfAnimation[animIndex];
				}
			}

			for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
			{
				aBone = theMdlFileData.theBones[boneIndex];

				if (aSequenceDesc != null)
				{
					aWeight = aSequenceDesc.theBoneWeights[boneIndex];
				}
				else
				{
					//NOTE: This should only be needed for a delta $animation.
					//      Arbitrarily assign 1 so that the following code will add frame lines for this $animation.
					aWeight = 1;
				}

				if (anAnimation != null && anAnimation.boneIndex == boneIndex)
				{
					if (aWeight > 0)
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
						//rot = CalcBoneRotation(frameIndex, s, aBone, anAnimation)
						rot = CalcBoneRotation(sectionFrameIndex, s, aBone, anAnimation, ref aFrameLine.rotationQuat);
						aFrameLine.rotation = new SourceVector();

						//NOTE: z = z puts head-foot axis horizontally
						//      facing viewer
						aFrameLine.rotation.x = rot.x;
						aFrameLine.rotation.y = rot.y;
						aFrameLine.rotation.z = rot.z;
						//------
						//aFrameLine.rotation.x = rot.x
						//aFrameLine.rotation.y = rot.y - 1.570796
						//aFrameLine.rotation.z = rot.z
						//------
						//aFrameLine.rotation.x = rot.y
						//aFrameLine.rotation.y = rot.x
						//aFrameLine.rotation.z = rot.z
						//------
						//------
						//NOTE: x = z puts head-foot axis horizontally
						//      facing away from viewer
						//aFrameLine.rotation.x = rot.z
						//aFrameLine.rotation.y = rot.y
						//aFrameLine.rotation.z = rot.x
						//------
						//aFrameLine.rotation.x = rot.z
						//aFrameLine.rotation.y = rot.x
						//aFrameLine.rotation.z = rot.y
						//------
						//------
						//NOTE: y = z  : head-foot axis vertically correctly
						//      x = -x : upside-down
						//      z = y  : 
						// facing to window right
						//aFrameLine.rotation.x = rot.x
						//aFrameLine.rotation.y = rot.z
						//aFrameLine.rotation.z = rot.y
						//------
						//NOTE: Upside-down; facing to window left
						//aFrameLine.rotation.x = -rot.x
						//aFrameLine.rotation.y = rot.z
						//aFrameLine.rotation.z = rot.y
						//------
						// facing to window right
						//aFrameLine.rotation.x = rot.x
						//aFrameLine.rotation.y = -rot.z
						//aFrameLine.rotation.z = rot.y
						//------
						//NOTE: Upside-down; facing to window left
						//aFrameLine.rotation.x = -rot.x
						//aFrameLine.rotation.y = -rot.z
						//aFrameLine.rotation.z = rot.y
						//------
						// facing to window left
						//aFrameLine.rotation.x = rot.x
						//aFrameLine.rotation.y = rot.z
						//aFrameLine.rotation.z = -rot.y
						//------
						//NOTE: Upside-down; facing to window right
						//aFrameLine.rotation.x = -rot.x
						//aFrameLine.rotation.y = rot.z
						//aFrameLine.rotation.z = -rot.y
						//------
						// facing to window left
						//aFrameLine.rotation.x = rot.x
						//aFrameLine.rotation.y = -rot.z
						//aFrameLine.rotation.z = -rot.y
						//------
						//NOTE: Upside-down; facing to window right
						//aFrameLine.rotation.x = -rot.x
						//aFrameLine.rotation.y = -rot.z
						//aFrameLine.rotation.z = -rot.y
						//------
						//------
						// facing to window right
						//aFrameLine.rotation.x = rot.y
						//aFrameLine.rotation.y = rot.z
						//aFrameLine.rotation.z = rot.x
						//------
						//NOTE: Upside-down; facing to window left
						//aFrameLine.rotation.x = -rot.y
						//aFrameLine.rotation.y = rot.z
						//aFrameLine.rotation.z = rot.x
						//------
						// facing to window right
						//aFrameLine.rotation.x = rot.y
						//aFrameLine.rotation.y = -rot.z
						//aFrameLine.rotation.z = rot.x
						//------
						//aFrameLine.rotation.x = -rot.y
						//aFrameLine.rotation.y = -rot.z
						//aFrameLine.rotation.z = rot.x
						//------
						//aFrameLine.rotation.x = rot.y
						//aFrameLine.rotation.y = rot.z
						//aFrameLine.rotation.z = -rot.x
						//------
						//aFrameLine.rotation.x = -rot.y
						//aFrameLine.rotation.y = rot.z
						//aFrameLine.rotation.z = -rot.x
						//------
						//aFrameLine.rotation.x = rot.y
						//aFrameLine.rotation.y = -rot.z
						//aFrameLine.rotation.z = -rot.x
						//------
						//aFrameLine.rotation.x = -rot.y
						//aFrameLine.rotation.y = -rot.z
						//aFrameLine.rotation.z = -rot.x

						aFrameLine.rotation.debug_text = rot.debug_text;

						//pos = Me.CalcBonePosition(frameIndex, s, aBone, anAnimation)
						pos = CalcBonePosition(sectionFrameIndex, s, aBone, anAnimation);
						aFrameLine.position = new SourceVector();
						aFrameLine.position.x = pos.x;
						aFrameLine.position.y = pos.y;
						aFrameLine.position.z = pos.z;
						aFrameLine.position.debug_text = pos.debug_text;
					}

					animIndex += 1;
					//If animIndex >= anAnimationDesc.theAnimations.Count Then
					//	anAnimation = Nothing
					//Else
					//	anAnimation = anAnimationDesc.theAnimations(animIndex)
					//End If
					if (animIndex >= aSectionOfAnimation.Count)
					{
						anAnimation = null;
					}
					else
					{
						anAnimation = aSectionOfAnimation[animIndex];
					}
				}
				else if (aWeight > 0)
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

					//NOTE: Changed in v0.25.
					//If (anAnimationDesc.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
					if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_DELTA) > 0)
					{
						aFrameLine.rotation = new SourceVector();
						aFrameLine.rotation.x = 0;
						aFrameLine.rotation.y = 0;
						aFrameLine.rotation.z = 0;
						aFrameLine.rotation.debug_text = "desc_delta";

						aFrameLine.position = new SourceVector();
						aFrameLine.position.x = 0;
						aFrameLine.position.y = 0;
						aFrameLine.position.z = 0;
						aFrameLine.position.debug_text = "desc_delta";
					}
					else
					{
						aFrameLine.rotation = new SourceVector();
						aFrameLine.rotation.x = aBone.rotation.x;
						aFrameLine.rotation.y = aBone.rotation.y;
						aFrameLine.rotation.z = aBone.rotation.z;
						aFrameLine.rotation.debug_text = "desc_bone";

						aFrameLine.position = new SourceVector();
						aFrameLine.position.x = aBone.position.x;
						aFrameLine.position.y = aBone.position.y;
						aFrameLine.position.z = aBone.position.z;
						aFrameLine.position.debug_text = "desc_bone";
					}
				}
			}
		}

		//FROM: SourceEngine2007_source\public\bone_setup.cpp
		////-----------------------------------------------------------------------------
		//// Purpose: return a sub frame rotation for a single bone
		////-----------------------------------------------------------------------------
		//void CalcBoneQuaternion( int frame, float s, 
		//						const Quaternion &baseQuat, const RadianEuler &baseRot, const Vector &baseRotScale, 
		//						int iBaseFlags, const Quaternion &baseAlignment, 
		//						const mstudioanim_t *panim, Quaternion &q )
		//{
		//	if ( panim->flags & STUDIO_ANIM_RAWROT )
		//	{
		//		q = *(panim->pQuat48());
		//		Assert( q.IsValid() );
		//		return;
		//	} 

		//	if ( panim->flags & STUDIO_ANIM_RAWROT2 )
		//	{
		//		q = *(panim->pQuat64());
		//		Assert( q.IsValid() );
		//		return;
		//	}

		//	if ( !(panim->flags & STUDIO_ANIM_ANIMROT) )
		//	{
		//		if (panim->flags & STUDIO_ANIM_DELTA)
		//		{
		//			q.Init( 0.0f, 0.0f, 0.0f, 1.0f );
		//		}
		//		else
		//		{
		//			q = baseQuat;
		//		}
		//		return;
		//	}

		//	mstudioanim_valueptr_t *pValuesPtr = panim->pRotV();

		//	if (s > 0.001f)
		//	{
		//		QuaternionAligned	q1, q2;
		//		RadianEuler			angle1, angle2;

		//		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle1.x, angle2.x );
		//		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle1.y, angle2.y );
		//		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle1.z, angle2.z );

		//		if (!(panim->flags & STUDIO_ANIM_DELTA))
		//		{
		//			angle1.x = angle1.x + baseRot.x;
		//			angle1.y = angle1.y + baseRot.y;
		//			angle1.z = angle1.z + baseRot.z;
		//			angle2.x = angle2.x + baseRot.x;
		//			angle2.y = angle2.y + baseRot.y;
		//			angle2.z = angle2.z + baseRot.z;
		//		}

		//		Assert( angle1.IsValid() && angle2.IsValid() );
		//		if (angle1.x != angle2.x || angle1.y != angle2.y || angle1.z != angle2.z)
		//		{
		//			AngleQuaternion( angle1, q1 );
		//			AngleQuaternion( angle2, q2 );

		//	#ifdef _X360
		//			fltx4 q1simd, q2simd, qsimd;
		//			q1simd = LoadAlignedSIMD( q1 );
		//			q2simd = LoadAlignedSIMD( q2 );
		//			qsimd = QuaternionBlendSIMD( q1simd, q2simd, s );
		//			StoreUnalignedSIMD( q.Base(), qsimd );
		//	#else
		//			QuaternionBlend( q1, q2, s, q );
		//#End If
		//		}
		//		else
		//		{
		//			AngleQuaternion( angle1, q );
		//		}
		//	}
		//	else
		//	{
		//		RadianEuler			angle;

		//		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle.x );
		//		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle.y );
		//		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle.z );

		//		if (!(panim->flags & STUDIO_ANIM_DELTA))
		//		{
		//			angle.x = angle.x + baseRot.x;
		//			angle.y = angle.y + baseRot.y;
		//			angle.z = angle.z + baseRot.z;
		//		}

		//		Assert( angle.IsValid() );
		//		AngleQuaternion( angle, q );
		//	}

		//	Assert( q.IsValid() );

		//	// align to unified bone
		//	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
		//	{
		//		QuaternionAlign( baseAlignment, q, q );
		//	}
		//}
		//
		//inline void CalcBoneQuaternion( int frame, float s, 
		//						const mstudiobone_t *pBone,
		//						const mstudiolinearbone_t *pLinearBones,
		//						const mstudioanim_t *panim, Quaternion &q )
		//{
		//	if (pLinearBones)
		//	{
		//		CalcBoneQuaternion( frame, s, pLinearBones->quat(panim->bone), pLinearBones->rot(panim->bone), pLinearBones->rotscale(panim->bone), pLinearBones->flags(panim->bone), pLinearBones->qalignment(panim->bone), panim, q );
		//	}
		//	else
		//	{
		//		CalcBoneQuaternion( frame, s, pBone->quat, pBone->rot, pBone->rotscale, pBone->flags, pBone->qAlignment, panim, q );
		//	}
		//}
		//Private Function CalcBoneQuaternion(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceQuaternion
		//	Dim rot As New SourceQuaternion()
		//	Dim angleVector As New SourceVector()

		//	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
		//		rot.x = anAnimation.theRot48.x
		//		rot.y = anAnimation.theRot48.y
		//		rot.z = anAnimation.theRot48.z
		//		rot.w = anAnimation.theRot48.w
		//		Return rot
		//	ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
		//		rot.x = anAnimation.theRot64.x
		//		rot.y = anAnimation.theRot64.y
		//		rot.z = anAnimation.theRot64.z
		//		rot.w = anAnimation.theRot64.w
		//		Return rot
		//	End If

		//	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) = 0 Then
		//		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
		//			rot.x = 0
		//			rot.y = 0
		//			rot.z = 0
		//			rot.w = 1
		//		Else
		//			rot.x = aBone.quat.x
		//			rot.y = aBone.quat.y
		//			rot.z = aBone.quat.z
		//			rot.w = aBone.quat.w
		//		End If
		//		Return rot
		//	End If

		//	Dim rotV As SourceMdlAnimationValuePointer

		//	rotV = anAnimation.theRotV

		//	If rotV.animValueOffset(0) <= 0 Then
		//		angleVector.x = 0
		//	Else
		//		angleVector.x = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(0), aBone.rotationScaleX)
		//	End If
		//	If rotV.animValueOffset(1) <= 0 Then
		//		angleVector.y = 0
		//	Else
		//		angleVector.y = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(1), aBone.rotationScaleY)
		//	End If
		//	If rotV.animValueOffset(2) <= 0 Then
		//		angleVector.z = 0
		//	Else
		//		angleVector.z = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(2), aBone.rotationScaleZ)
		//	End If

		//	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
		//		angleVector.x += aBone.quat.x
		//		angleVector.y += aBone.quat.y
		//		angleVector.z += aBone.quat.z
		//	End If

		//	rot = MathModule.AngleQuaternion(angleVector)

		//	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
		//	'	{
		//	'		QuaternionAlign( baseAlignment, q, q );
		//	'	}

		//	Return rot
		//End Function
		private SourceVector CalcBoneRotation(int frameIndex, double s, SourceMdlBone aBone, SourceMdlAnimation anAnimation, ref SourceQuaternion rotationQuat)
		{
			SourceQuaternion rot = new SourceQuaternion();
			SourceVector angleVector = new SourceVector();

			if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0)
			{
				rot.x = anAnimation.theRot48bits.x;
				rot.y = anAnimation.theRot48bits.y;
				rot.z = anAnimation.theRot48bits.z;
				rot.w = anAnimation.theRot48bits.w;
				rotationQuat.x = rot.x;
				rotationQuat.y = rot.y;
				rotationQuat.z = rot.z;
				rotationQuat.w = rot.w;
				angleVector = MathModule.ToEulerAngles(rot);

				angleVector.debug_text = "raw48";
				return angleVector;
			}
			else if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0)
			{
				//angleVector.x = anAnimation.theRot64.xRadians
				//angleVector.y = anAnimation.theRot64.yRadians
				//angleVector.z = anAnimation.theRot64.zRadians
				//------
				rot.x = anAnimation.theRot64bits.x;
				rot.y = anAnimation.theRot64bits.y;
				rot.z = anAnimation.theRot64bits.z;
				rot.w = anAnimation.theRot64bits.w;
				rotationQuat.x = rot.x;
				rotationQuat.y = rot.y;
				rotationQuat.z = rot.z;
				rotationQuat.w = rot.w;
				angleVector = MathModule.ToEulerAngles(rot);

				//'TEST: Rotate z by -90 degrees.
				//'TEST: Rotate y by -90 degrees.
				//angleVector.y += MathModule.DegreesToRadians(-90)

				angleVector.debug_text = "raw64 (" + rot.x.ToString() + ", " + rot.y.ToString() + ", " + rot.z.ToString() + ", " + rot.w.ToString() + ")";
				return angleVector;
			}

			if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMROT) == 0)
			{
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0)
				{
					angleVector.x = 0;
					angleVector.y = 0;
					angleVector.z = 0;
					rotationQuat.x = 0;
					rotationQuat.y = 0;
					rotationQuat.z = 0;
					rotationQuat.w = 0;
					angleVector.debug_text = "delta";
				}
				else
				{
					angleVector.x = aBone.rotation.x;
					angleVector.y = aBone.rotation.y;
					angleVector.z = aBone.rotation.z;
					rotationQuat.x = 0;
					rotationQuat.y = 0;
					rotationQuat.z = 0;
					rotationQuat.w = 0;
					angleVector.debug_text = "bone";
				}
				return angleVector;
			}

			SourceMdlAnimationValuePointer rotV = anAnimation.theRotV;


			if (rotV.animXValueOffset <= 0)
			{
				angleVector.x = 0;
			}
			else
			{
				angleVector.x = ExtractAnimValue(frameIndex, rotV.theAnimXValues, aBone.rotationScale.x);
			}
			if (rotV.animYValueOffset <= 0)
			{
				angleVector.y = 0;
			}
			else
			{
				angleVector.y = ExtractAnimValue(frameIndex, rotV.theAnimYValues, aBone.rotationScale.y);
			}
			if (rotV.animZValueOffset <= 0)
			{
				angleVector.z = 0;
			}
			else
			{
				angleVector.z = ExtractAnimValue(frameIndex, rotV.theAnimZValues, aBone.rotationScale.z);
			}

			angleVector.debug_text = "anim";

			if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_DELTA) == 0)
			{
				angleVector.x += aBone.rotation.x;
				angleVector.y += aBone.rotation.y;
				angleVector.z += aBone.rotation.z;
				angleVector.debug_text += "+bone";
			}

			rotationQuat = MathModule.EulerAnglesToQuaternion(angleVector);
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
		private SourceVector CalcBonePosition(int frameIndex, double s, SourceMdlBone aBone, SourceMdlAnimation anAnimation)
		{
			SourceVector pos = new SourceVector();

			if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0)
			{
				//If aBone.parentBoneIndex = -1 Then
				//	pos.x = anAnimation.thePos.y
				//	pos.y = -anAnimation.thePos.x
				//	pos.z = anAnimation.thePos.z
				//Else
				pos.x = anAnimation.thePos.x;
				pos.y = anAnimation.thePos.y;
				pos.z = anAnimation.thePos.z;
				//	'------
				//	'pos.y = anAnimation.thePos.z
				//	'pos.z = anAnimation.thePos.y
				//	'------
				//	'pos.x = anAnimation.thePos.y
				//	'pos.y = -anAnimation.thePos.x
				//	'pos.z = anAnimation.thePos.z
				//End If

				pos.debug_text = "raw";
				return pos;
			}
			else if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) == 0)
			{
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0)
				{
					pos.x = 0;
					pos.y = 0;
					pos.z = 0;
					pos.debug_text = "delta";
				}
				else
				{
					pos.x = aBone.position.x;
					pos.y = aBone.position.y;
					pos.z = aBone.position.z;
					//pos.y = aBone.positionZ
					//pos.z = -aBone.positionY
					pos.debug_text = "bone";
				}
				return pos;
			}

			SourceMdlAnimationValuePointer posV = anAnimation.thePosV;


			if (posV.animXValueOffset <= 0)
			{
				pos.x = 0;
			}
			else
			{
				pos.x = ExtractAnimValue(frameIndex, posV.theAnimXValues, aBone.positionScale.x);
			}

			if (posV.animYValueOffset <= 0)
			{
				pos.y = 0;
			}
			else
			{
				pos.y = ExtractAnimValue(frameIndex, posV.theAnimYValues, aBone.positionScale.y);
			}

			if (posV.animZValueOffset <= 0)
			{
				pos.z = 0;
			}
			else
			{
				pos.z = ExtractAnimValue(frameIndex, posV.theAnimZValues, aBone.positionScale.z);
			}

			pos.debug_text = "anim";

			if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_DELTA) == 0)
			{
				pos.x += aBone.position.x;
				pos.y += aBone.position.y;
				pos.z += aBone.position.z;
				pos.debug_text += "+bone";
			}

			return pos;
		}

		//FROM: SourceEngine2007_source\public\bone_setup.cpp
		//void ExtractAnimValue( int frame, mstudioanimvalue_t *panimvalue, float scale, float &v1 )
		//{
		//	if ( !panimvalue )
		//	{
		//		v1 = 0;
		//		return;
		//	}

		//	int k = frame;

		//	while (panimvalue->num.total <= k)
		//	{
		//		k -= panimvalue->num.total;
		//		panimvalue += panimvalue->num.valid + 1;
		//		if ( panimvalue->num.total == 0 )
		//		{
		//			Assert( 0 ); // running off the end of the animation stream is bad
		//			v1 = 0;
		//			return;
		//		}
		//	}
		//	if (panimvalue->num.valid > k)
		//	{
		//		v1 = panimvalue[k+1].value * scale;
		//	}
		//	else
		//	{
		//		// get last valid data block
		//		v1 = panimvalue[panimvalue->num.valid].value * scale;
		//	}
		//}
		public double ExtractAnimValue(int frameIndex, List<SourceMdlAnimationValue> animValues, double scale)
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
					v1 = animValues[animValueIndex + k + 1].value * scale;
				}
				else
				{
					//NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
					v1 = animValues[animValueIndex + animValues[animValueIndex].valid].value * scale;
				}
			}
			catch
			{
			}

			return v1;
		}

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData52 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		private SourceVvdFileData04 theVvdFileData;

		private SortedList<int, AnimationFrameLine> theAnimationFrameLines;

		private SourceVector worldToPoseColumn0 = new SourceVector();
		private SourceVector worldToPoseColumn1 = new SourceVector();
		private SourceVector worldToPoseColumn2 = new SourceVector();
		private SourceVector worldToPoseColumn3 = new SourceVector();
		private SourceVector poseToWorldColumn0 = new SourceVector();
		private SourceVector poseToWorldColumn1 = new SourceVector();
		private SourceVector poseToWorldColumn2 = new SourceVector();
		private SourceVector poseToWorldColumn3 = new SourceVector();

		private SourceVector previousFrameRotation = new SourceVector();
		private SourceVector frame0Position = new SourceVector();

#endregion

	}

}