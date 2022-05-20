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
	public class SourceSmdFile2531
	{

#region Creation and Destruction

		public SourceSmdFile2531(StreamWriter outputFileStream, SourceMdlFileData2531 mdlFileData)
		{
			this.theOutputFileStreamWriter = outputFileStream;
			this.theMdlFileData = mdlFileData;
		}

		public SourceSmdFile2531(StreamWriter outputFileStream, SourceMdlFileData2531 mdlFileData, SourcePhyFileData phyFileData)
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

		public void WriteNodesSection()
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

		public void WriteSkeletonSection()
		{
			string line = "";

			SourceVector rotation = null;

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
				rotation = MathModule.ToEulerAngles(this.theMdlFileData.theBones[boneIndex].rotation);

				line = "    ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += this.theMdlFileData.theBones[boneIndex].position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += rotation.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += rotation.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += rotation.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSkeletonSectionForAnimation(SourceMdlSequenceDescBase aSequenceDescBase, SourceMdlAnimationDescBase anAnimationDescBase)
		{
			string line = "";
			int boneIndex = 0;
			AnimationFrameLine aFrameLine = null;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();
			SourceMdlSequenceDesc2531 aSequenceDesc = null;
			SourceMdlAnimationDesc2531 anAnimationDesc = null;

			aSequenceDesc = (SourceMdlSequenceDesc2531)aSequenceDescBase;
			anAnimationDesc = (SourceMdlAnimationDesc2531)anAnimationDescBase;

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

					//If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
					//	line += "   # "
					//	line += "pos: "
					//	line += aFrameLine.position.debug_text
					//	line += "   "
					//	line += "rot: "
					//	line += aFrameLine.rotation.debug_text
					//End If

					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSection(int lodIndex, SourceVtxModel107 aVtxModel, SourceMdlModel2531 aBodyModel, int bodyPartVertexIndexStart)
		{
			string line = "";
			string materialLine = "";
			string vertex1Line = "";
			string vertex2Line = "";
			string vertex3Line = "";

			//triangles
			line = "triangles";
			this.theOutputFileStreamWriter.WriteLine(line);

			SourceVtxModelLod107 aVtxLod = null;
			SourceVtxMesh107 aVtxMesh = null;
			SourceVtxStripGroup107 aStripGroup = null;
			SourceVtxStrip107 aStrip = null;
			int materialIndex = 0;
			string materialName = null;
			int meshVertexIndexStart = 0;

			try
			{
				aVtxLod = aVtxModel.theVtxModelLods[lodIndex];

				if (aVtxLod.theVtxMeshes != null)
				{
					for (int meshIndex = 0; meshIndex < aVtxLod.theVtxMeshes.Count; meshIndex++)
					{
						aVtxMesh = aVtxLod.theVtxMeshes[meshIndex];
						materialIndex = aBodyModel.theMeshes[meshIndex].materialIndex;
						materialName = this.theMdlFileData.theTextures[materialIndex].theFileName;
						//TODO: This was used in previous versions, but maybe should leave as above.
						//materialName = Path.GetFileName(Me.theSourceEngineModel.theMdlFileHeader.theTextures(materialIndex).theName)

						meshVertexIndexStart = aBodyModel.theMeshes[meshIndex].vertexIndexStart;

						if (aVtxMesh.theVtxStripGroups != null)
						{
							for (int groupIndex = 0; groupIndex < aVtxMesh.theVtxStripGroups.Count; groupIndex++)
							{
								aStripGroup = aVtxMesh.theVtxStripGroups[groupIndex];

								if (aStripGroup.theVtxStrips != null && aStripGroup.theVtxIndexes != null && (aStripGroup.theVtxVertexesForStaticProp != null || aStripGroup.theVtxVertexes != null))
								{
									for (int vtxStripIndex = 0; vtxStripIndex < aStripGroup.theVtxStrips.Count; vtxStripIndex++)
									{
										aStrip = aStripGroup.theVtxStrips[vtxStripIndex];

//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of aStrip.indexCount - 3 for every iteration:
										int tempVar = aStrip.indexCount - 3;
										for (int vtxIndexIndex = 0; vtxIndexIndex <= tempVar; vtxIndexIndex += 3)
										{
											materialLine = materialName;
											vertex1Line = this.GetVertexLine(aBodyModel, aStripGroup, vtxIndexIndex + aStrip.indexMeshIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
											vertex2Line = this.GetVertexLine(aBodyModel, aStripGroup, vtxIndexIndex + aStrip.indexMeshIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
											vertex3Line = this.GetVertexLine(aBodyModel, aStripGroup, vtxIndexIndex + aStrip.indexMeshIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart);
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
			SourceMdlBone2531 aBone = null;
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
							//aBone = Me.theMdlFileData.theBones(faceSection.theBoneIndex)

							for (int triangleIndex = 0; triangleIndex < faceSection.theFaces.Count; triangleIndex++)
							{
								aTriangle = faceSection.theFaces[triangleIndex];

								line = "  phy";
								this.theOutputFileStreamWriter.WriteLine(line);

								for (int vertexIndex = 0; vertexIndex < aTriangle.vertexIndex.Length; vertexIndex++)
								{
									//phyVertex = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex))
									phyVertex = faceSection.theVertices[aTriangle.vertexIndex[vertexIndex]];

									aVectorTransformed = this.TransformPhyVertex(aBone, phyVertex.vertex, aSourcePhysCollisionModel);
									//aVectorTransformed = Me.TransformPhyVertex(aBone, phyVertex.vertex)

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

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private string GetVertexLine(SourceMdlModel2531 aBodyModel, SourceVtxStripGroup107 aStripGroup, int aVtxIndexIndex, int lodIndex, int meshVertexIndexStart, int bodyPartVertexIndexStart)
		{
			string line = null;

			ushort aVtxVertexIndex = 0;
			UInt16 originalMeshVertexIndex = 0;
			int vertexIndex = 0;
			List<int> boneIndexes = new List<int>(4);
			SourceVector position = new SourceVector();
			SourceVector normal = new SourceVector();
			double texCoordU = 0;
			double texCoordV = 0;
			List<double> weights = new List<double>(4);

			line = "";
			try
			{
				aVtxVertexIndex = aStripGroup.theVtxIndexes[aVtxIndexIndex];
				if ((aStripGroup.flags & SourceVtxStripGroup107.STRIPGROUP_USES_STATIC_PROP_VERTEXES) > 0)
				{
					originalMeshVertexIndex = aStripGroup.theVtxVertexesForStaticProp[aVtxVertexIndex];
				}
				else
				{
					originalMeshVertexIndex = aStripGroup.theVtxVertexes[aVtxVertexIndex].originalMeshVertexIndex;
				}
				//vertexIndex = originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart
				vertexIndex = originalMeshVertexIndex + meshVertexIndexStart;

				if (aBodyModel.vertexListType == 0)
				{
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of aBodyModel.theVertexesType0(vertexIndex).boneIndex.Length for every iteration:
					int tempVar = aBodyModel.theVertexesType0[vertexIndex].boneIndex.Length;
					for (int i = 0; i < tempVar; i++)
					{
						weights.Add(aBodyModel.theVertexesType0[vertexIndex].weight[i] / 255.0);
						if (weights[i] > 0)
						{
							boneIndexes.Add(aBodyModel.theVertexesType0[vertexIndex].boneIndex[i]);
						}
					}
					position.x = aBodyModel.theVertexesType0[vertexIndex].position.x;
					position.y = aBodyModel.theVertexesType0[vertexIndex].position.y;
					position.z = aBodyModel.theVertexesType0[vertexIndex].position.z;
					normal.x = aBodyModel.theVertexesType0[vertexIndex].normal.x;
					normal.y = aBodyModel.theVertexesType0[vertexIndex].normal.y;
					normal.z = aBodyModel.theVertexesType0[vertexIndex].normal.z;
					texCoordU = aBodyModel.theVertexesType0[vertexIndex].texCoordU;
					texCoordV = aBodyModel.theVertexesType0[vertexIndex].texCoordV;
				}
				else if (aBodyModel.vertexListType == 1)
				{
					boneIndexes.Add(0);
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.x
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.y
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.z
					//'position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.y
					//'position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.z
					//'position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.x
					//normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 65535) * Me.theMdlFileData.hullMaxPosition.x
					//normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535) * Me.theMdlFileData.hullMaxPosition.y
					//normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535) * Me.theMdlFileData.hullMaxPosition.z
					//texCoordU = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535)
					//texCoordV = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535)

					// Too big compared to Therese.
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ)
					// Too big compared to Therese.
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 255)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 255)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 255)
					// Not correct scale for some models and car_idle seems shortened on one axis. Thus, it seems 3 scale values should be used.
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 4095)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 4095)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 4095)
					// Too small compared to Therese.
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 32767)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 32767)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 32767)
					// Too small compared to Therese.
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535)
					// Flattens car_idle
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.unknown01
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.unknown02
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.unknown03

					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.x
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.y
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.z
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 255) * Me.theMdlFileData.hullMinPosition.x
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 255) * Me.theMdlFileData.hullMinPosition.y
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 255) * Me.theMdlFileData.hullMinPosition.z
					//normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 65535)
					//normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535)
					//normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535)
					//texCoordU = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535)
					//texCoordV = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535)
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.unknown02
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.unknown02
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.unknown02
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMaxPosition.x
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMaxPosition.y
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMaxPosition.z
					//Dim aBone As SourceMdlBone2531
					//aBone = Me.theMdlFileData.theBones(0)
					//Dim vecin As New SourceVector
					//vecin.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535)
					//vecin.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535)
					//vecin.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535)
					//position = MathModule.VectorTransform(vecin, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (aBodyModel.theVertexesType1(vertexIndex).scaleX)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (aBodyModel.theVertexesType1(vertexIndex).scaleY)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (aBodyModel.theVertexesType1(vertexIndex).scaleZ)
					//NOTE: This seems to work for many models, but seems to be wrong for:  
					//      models\character\npc\common\freshcorpses\femalefreshcorpse\femalefreshcorpse.mdl
					//      models\character\monster\werewolf\werewolf_head\werewolf_head.mdl
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z)
					//NOTE: This seems to work for many models, but seems to be wrong for:  
					//      models\character\npc\common\freshcorpses\femalefreshcorpse\femalefreshcorpse.mdl
					position.x = (aBodyModel.theVertexesType1[vertexIndex].positionX / 65535.0) * (this.theMdlFileData.hullMaxPosition.x - this.theMdlFileData.theBodyParts[0].theModels[0].unknown01[0]);
					position.y = (aBodyModel.theVertexesType1[vertexIndex].positionY / 65535.0) * (this.theMdlFileData.hullMaxPosition.y - this.theMdlFileData.theBodyParts[0].theModels[0].unknown01[1]);
					position.z = (aBodyModel.theVertexesType1[vertexIndex].positionZ / 65535.0) * (this.theMdlFileData.hullMaxPosition.z - this.theMdlFileData.theBodyParts[0].theModels[0].unknown01[2]);
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x - Me.theMdlFileData.unknown01)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y - Me.theMdlFileData.unknown02)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z - Me.theMdlFileData.unknown03)
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x + Me.theMdlFileData.unknown01)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y + Me.theMdlFileData.unknown02)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z + Me.theMdlFileData.unknown03)
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) + Me.theMdlFileData.unknown01)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) + Me.theMdlFileData.unknown02)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) + Me.theMdlFileData.unknown03)
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) + Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) + Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) + Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))
					// This puts the corpse facing up, but still stretched along z-axis.
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))
					// This is close, but scale is probably wrong and slightly stretched along z-axis.
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2)
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2)
					// Completely flat.
					//position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX) * (Me.theMdlFileData.hullMaxPosition.x) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0)
					//position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY) * (Me.theMdlFileData.hullMaxPosition.y) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1)
					//position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ) * (Me.theMdlFileData.hullMaxPosition.z) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2)

					//TODO: Verify these.
					normal.x = (aBodyModel.theVertexesType1[vertexIndex].normalX / 255.0);
					normal.y = (aBodyModel.theVertexesType1[vertexIndex].normalY / 255.0);
					normal.z = (aBodyModel.theVertexesType1[vertexIndex].normalZ / 255.0);

					// These are correct.
					texCoordU = (aBodyModel.theVertexesType1[vertexIndex].texCoordU / 255.0);
					texCoordV = (aBodyModel.theVertexesType1[vertexIndex].texCoordV / 255.0);
				}
				else if (aBodyModel.vertexListType == 2)
				{
					boneIndexes.Add(0);
					//position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x)
					//position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y)
					//position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z)
					//position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 127.5) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x)
					//position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 127.5) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y)
					//position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 127.5) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z)
					//position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0))
					//position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1))
					//position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2))
					//position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
					//position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
					//position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))
					//position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.hullMinPosition.x)
					//position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.hullMinPosition.y)
					//position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.hullMinPosition.z)
					// This makes candelabra really close to candelabra_candles.
					position.x = ((aBodyModel.theVertexesType2[vertexIndex].positionX - 128) / 255.0) * (this.theMdlFileData.hullMaxPosition.x - this.theMdlFileData.theBodyParts[0].theModels[0].unknown01[0]);
					position.y = ((aBodyModel.theVertexesType2[vertexIndex].positionY - 128) / 255.0) * (this.theMdlFileData.hullMaxPosition.y - this.theMdlFileData.theBodyParts[0].theModels[0].unknown01[1]);
					position.z = (aBodyModel.theVertexesType2[vertexIndex].positionZ / 255.0) * (this.theMdlFileData.hullMaxPosition.z - this.theMdlFileData.theBodyParts[0].theModels[0].unknown01[2]);
					// Too big for candelabra.
					//position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.x - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0)
					//position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.hullMaxPosition.y - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1)
					//position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.hullMaxPosition.z - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2)) * Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2)
					// Too big for candelabra.
					//position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0))
					//position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1))
					//position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2))
					// Candelabra: Too big and upside-down and under candelabra_candles.
					//position.x = ((aBodyModel.theVertexesType2(vertexIndex).positionX - 128) / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(0) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(0))
					//position.y = ((aBodyModel.theVertexesType2(vertexIndex).positionY - 128) / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(1) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(1))
					//position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * (Me.theMdlFileData.theBodyParts(0).theModels(0).unknown01(2) - Me.theMdlFileData.theBodyParts(0).theModels(0).unknown02(2))

					//TODO: Verify these.
					normal.x = (aBodyModel.theVertexesType2[vertexIndex].normalX / 255.0);
					normal.y = (aBodyModel.theVertexesType2[vertexIndex].normalY / 255.0);
					normal.z = (aBodyModel.theVertexesType2[vertexIndex].normalZ / 255.0);

					// These are correct.
					texCoordU = (aBodyModel.theVertexesType2[vertexIndex].texCoordU / 255.0);
					texCoordV = (aBodyModel.theVertexesType2[vertexIndex].texCoordV / 255.0);
				}
				else
				{
					int debug = 4242;
				}

				//NOTE: Clamp the UV coords so they are always between 0 and 1.
				//      Example that has U > 1: "models\items\jadedragon\info\info_jadedragon.mdl"
				if (texCoordU < 0 || texCoordU > 1)
				{
					texCoordU -= Math.Truncate(texCoordU);
				}
				if (texCoordV < 0 || texCoordV > 1)
				{
					texCoordV -= Math.Truncate(texCoordV);
				}

				line = "  ";
				line += boneIndexes[0].ToString(MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				line += position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				line += normal.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normal.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normal.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				line += texCoordU.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += (1 - texCoordV).ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				if (aBodyModel.vertexListType == 0)
				{
					line += " ";
					line += boneIndexes.Count.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					for (int i = 0; i < boneIndexes.Count; i++)
					{
						line += " ";
						line += boneIndexes[i].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += weights[i].ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					}
				}
			}
			catch (Exception ex)
			{
				line = "// " + line;
			}

			return line;
		}

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
		private void CalcAnimation(SourceMdlSequenceDesc2531 aSequenceDesc, SourceMdlAnimationDesc2531 anAnimationDesc, int frameIndex)
		{
			double s = 0;
			SourceMdlBone2531 aBone = null;
			SourceMdlAnimation2531 anAnimation = null;
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
		//Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone2531, ByVal anAnimation As SourceMdlAnimation2531, ByRef rotationQuat As SourceQuaternion) As SourceVector
		//	'Dim rot As New SourceQuaternion()
		//	Dim angleVector As New SourceVector()

		//	If anAnimation.theOffsets(3) <= 0 Then
		//		angleVector.x = aBone.rotation.x
		//	Else
		//		angleVector.x = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationXValues, aBone.rotationScale.x, aBone.rotation.x)
		//	End If
		//	If anAnimation.theOffsets(4) <= 0 Then
		//		angleVector.y = aBone.rotation.y
		//	Else
		//		angleVector.y = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationYValues, aBone.rotationScale.y, aBone.rotation.y)
		//	End If
		//	If anAnimation.theOffsets(5) <= 0 Then
		//		angleVector.z = aBone.rotation.z
		//	Else
		//		angleVector.z = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationZValues, aBone.rotationScale.z, aBone.rotation.z)
		//	End If

		//	angleVector.debug_text = "anim"

		//	rotationQuat = MathModule.EulerAnglesToQuaternion(angleVector)
		//	Return angleVector
		//End Function
		private SourceVector CalcBoneRotation(int frameIndex, double s, SourceMdlBone2531 aBone, SourceMdlAnimation2531 anAnimation, ref SourceQuaternion rotationQuat)
		{
			SourceQuaternion rot = new SourceQuaternion();
			SourceVector angleVector = new SourceVector();

			if ((aBone.flags & SourceMdlBone2531.STUDIO_PROC_AXISINTERP) > 0)
			{
				angleVector.x = 0;
				angleVector.y = 0;
				angleVector.z = 0;

				angleVector.debug_text = "anim";
			}
			else
			{
				if (anAnimation.theOffsets[3] <= 0 || (aBone.flags & SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0)
				{
					rot.x = aBone.rotation.x;
				}
				else
				{
					//rot.x = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationXValues, aBone.rotationScale.x, aBone.rotation.x)
					rot.x = this.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationXValues, aBone.rotationScale.x, 0);
				}
				if (anAnimation.theOffsets[4] <= 0 || (aBone.flags & SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0)
				{
					rot.y = aBone.rotation.y;
				}
				else
				{
					//rot.y = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationYValues, aBone.rotationScale.y, aBone.rotation.y)
					rot.y = this.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationYValues, aBone.rotationScale.y, 0);
				}
				if (anAnimation.theOffsets[5] <= 0 || (aBone.flags & SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0)
				{
					rot.z = aBone.rotation.z;
				}
				else
				{
					//rot.z = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationZValues, aBone.rotationScale.z, aBone.rotation.z)
					rot.z = this.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationZValues, aBone.rotationScale.z, 0);
				}
				if (anAnimation.theOffsets[6] <= 0 || (aBone.flags & SourceMdlBone2531.STUDIO_PROC_QUATINTERP) > 0)
				{
					rot.w = aBone.rotation.w;
				}
				else
				{
					//rot.w = Me.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationWValues, aBone.rotationScale.w, aBone.rotation.w)
					rot.w = this.ExtractAnimValue(frameIndex, anAnimation.theRotationAnimationWValues, aBone.rotationScale.w, 0);
				}

				angleVector = MathModule.ToEulerAngles2531(rot);
				angleVector.debug_text = "anim";
			}

			rotationQuat = rot;
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
		private SourceVector CalcBonePosition(int frameIndex, double s, SourceMdlBone2531 aBone, SourceMdlAnimation2531 anAnimation)
		{
			SourceVector pos = new SourceVector();

			if (anAnimation.theOffsets[0] <= 0)
			{
				//pos.x = 0
				pos.x = aBone.position.x;
			}
			else
			{
				pos.x = this.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationXValues, aBone.positionScale.x, aBone.position.x);
			}

			if (anAnimation.theOffsets[1] <= 0)
			{
				//pos.y = 0
				pos.y = aBone.position.y;
			}
			else
			{
				pos.y = this.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationYValues, aBone.positionScale.y, aBone.position.y);
			}

			if (anAnimation.theOffsets[2] <= 0)
			{
				//pos.z = 0
				pos.z = aBone.position.z;
			}
			else
			{
				pos.z = this.ExtractAnimValue(frameIndex, anAnimation.thePositionAnimationZValues, aBone.positionScale.z, aBone.position.z);
			}

			pos.debug_text = "anim";

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

		private SourceVector TransformPhyVertex(SourceMdlBone2531 aBone, SourceVector vertex, SourcePhyPhysCollisionModel aSourcePhysCollisionModel)
		{
			//Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone2531, ByVal vertex As SourceVector) As SourceVector
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
			//------
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

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData2531 theMdlFileData;
		private SourcePhyFileData thePhyFileData;

		private SortedList<int, AnimationFrameLine> theAnimationFrameLines;

#endregion


	}

}