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
	public class SourceSmdFile53
	{

#region Creation and Destruction

		public SourceSmdFile53(StreamWriter outputFileStream, SourceMdlFileData53 mdlFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
		}

		public SourceSmdFile53(StreamWriter outputFileStream, SourceMdlFileData53 mdlFileData, SourceVvdFileData04 vvdFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			theVvdFileData = vvdFileData;
		}

		public SourceSmdFile53(StreamWriter outputFileStream, SourceMdlFileData53 mdlFileData, SourcePhyFileData phyFileData)
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
								//  19 -0.000005 1.000002 -0.000043 0.0 0.0 0.0 1 0
								//  19 -0.008333 0.997005 1.003710 0.0 0.0 0.0 1 0
								for (int vertexIndex = 0; vertexIndex < aTriangle.vertexIndex.Length; vertexIndex++)
								{
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
			string line = "";
			SourceMdlBone aBone = null;
			int boneIndex = 0;
			AnimationFrameLine aFrameLine = null;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();
			//Dim tempRotation As New SourceVector()
			SourceMdlSequenceDesc aSequenceDesc = null;
			SourceMdlAnimationDesc52 anAnimationDesc = null;

			aSequenceDesc = (SourceMdlSequenceDesc)aSequenceDescBase;
			anAnimationDesc = (SourceMdlAnimationDesc52)anAnimationDescBase;

			//skeleton
			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			if (theMdlFileData.theBones != null)
			{
				theAnimationFrameLines = new SortedList<int, AnimationFrameLine>();
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionFrameIndex = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionIndex = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				SourceAniFrameAnim52 aSectionOfAnimation = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				BoneConstantInfo49 aBoneConstantInfo = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				BoneFrameDataInfo49 aBoneFrameDataInfo = null;
				for (int frameIndex = 0; frameIndex < anAnimationDesc.frameCount; frameIndex++)
				{
					theAnimationFrameLines.Clear();

					if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_FRAMEANIM) != 0)
					{
	//					Dim sectionFrameIndex As Integer
	//					Dim sectionIndex As Integer
	//					Dim aSectionOfAnimation As SourceAniFrameAnim52
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
						aSectionOfAnimation = anAnimationDesc.theSectionsOfFrameAnim[sectionIndex];

	//					Dim aBoneConstantInfo As BoneConstantInfo49
	//					Dim aBoneFrameDataInfo As BoneFrameDataInfo49

						for (boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
						{
							aBone = theMdlFileData.theBones[boneIndex];

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
								aBoneConstantInfo = aSectionOfAnimation.theBoneConstantInfos[boneIndex];
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
								aBoneFrameDataInfo = aSectionOfAnimation.theBoneFrameDataInfos[sectionFrameIndex][boneIndex];
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
					{
						CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex);
					}

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
					SourceVector vecPos = null;
					SourceVector vecAngle = null;

					previousFrameIndex = 0;
					vecPos = new SourceVector();
					vecAngle = new SourceVector();

//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
					double f = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
					double d = 0;
					foreach (SourceMdlMovement aMovement in movements)
					{
						if (frameIndex <= aMovement.endframeIndex)
						{
	//						Dim f As Double
	//						Dim d As Double
							f = (frameIndex - previousFrameIndex) / (double)(aMovement.endframeIndex - previousFrameIndex);
							d = aMovement.v0 * f + 0.5 * (aMovement.v1 - aMovement.v0) * f * f;
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

		private SourceVector TransformPhyVertex(SourceMdlBone aBone, SourceVector vertex)
		{
			SourceVector aVectorTransformed = new SourceVector();
			SourceVector aVector = new SourceVector();

			if (thePhyFileData.theSourcePhyIsCollisionModel)
			{
				aVectorTransformed.x = 1 / 0.0254 * vertex.z;
				aVectorTransformed.y = 1 / 0.0254 * -vertex.x;
				aVectorTransformed.z = 1 / 0.0254 * -vertex.y;
			}
			else
			{
				//TEST: Shows blocks, but all seem to be at origin.
				aVectorTransformed.x = 1 / 0.0254 * vertex.z;
				aVectorTransformed.y = 1 / 0.0254 * -vertex.x;
				aVectorTransformed.z = 1 / 0.0254 * -vertex.y;
				//------
				//TEST: Strecthed flat planes.
				//aVector.x = 1 / 0.0254 * vertex.x
				//aVector.y = 1 / 0.0254 * vertex.z
				//aVector.z = 1 / 0.0254 * -vertex.y
				//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				//------
				//TEST: Strecthed flat planes.
				//aVector.x = 1 / 0.0254 * vertex.z
				//aVector.y = 1 / 0.0254 * -vertex.x
				//aVector.z = 1 / 0.0254 * -vertex.y
				//aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			}

			return aVectorTransformed;
		}

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
		private SourceMdlFileData53 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		private SourceVvdFileData04 theVvdFileData;

		private SortedList<int, AnimationFrameLine> theAnimationFrameLines;

#endregion

	}

}