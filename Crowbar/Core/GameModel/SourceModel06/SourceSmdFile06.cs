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
	public class SourceSmdFile06
	{

#region Creation and Destruction

		public SourceSmdFile06(StreamWriter outputFileStream, SourceMdlFileData06 mdlFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
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

		public void WriteSkeletonSection()
		{
			string line = "";
			SourceMdlBone06 aBone = null;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();

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

				if (aBone.parentBoneIndex == -1)
				{
					position.x = aBone.position.y;
					position.y = -aBone.position.x;
					position.z = aBone.position.z;

					rotation.x = aBone.rotation.x;
					rotation.y = aBone.rotation.y;
					rotation.z = aBone.rotation.z + MathModule.DegreesToRadians(-90);
					//angleVector.y += MathModule.DegreesToRadians(-90)
				}
				else
				{
					position.x = aBone.position.x;
					position.y = aBone.position.y;
					position.z = aBone.position.z;

					rotation.x = aBone.rotation.x;
					rotation.y = aBone.rotation.y;
					rotation.z = aBone.rotation.z;
				}

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
				theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSkeletonSectionForAnimation(SourceMdlSequenceDesc06 aSequenceDesc, int blendIndex)
		{
			string line = "";
			SourceMdlBone06 aBone = null;
			SourceMdlAnimation06 anAnimation = null;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();
			double scale = 0;
			double tempValue = 0;

			//skeleton
			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			for (int frameIndex = 0; frameIndex < aSequenceDesc.frameCount; frameIndex++)
			{
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

				for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
				{
					aBone = theMdlFileData.theBones[boneIndex];
					anAnimation = aSequenceDesc.theAnimations[boneIndex];

					if (aBone.parentBoneIndex == -1)
					{
						//position.x = anAnimation.theBonePositionsAndRotations(frameIndex).position.y
						//position.y = -anAnimation.theBonePositionsAndRotations(frameIndex).position.x
						//position.z = anAnimation.theBonePositionsAndRotations(frameIndex).position.z
						//======
						position.x = anAnimation.theBonePositionsAndRotations[frameIndex].position.x;
						position.y = anAnimation.theBonePositionsAndRotations[frameIndex].position.y;
						position.z = anAnimation.theBonePositionsAndRotations[frameIndex].position.z;
						scale = frameIndex / (double)(aSequenceDesc.frameCount - 1);
						if ((aSequenceDesc.motiontype & SourceModule10.STUDIO_LX) == SourceModule10.STUDIO_LX)
						{
							position.x += scale * aSequenceDesc.linearmovement.x;
						}
						if ((aSequenceDesc.motiontype & SourceModule10.STUDIO_LY) == SourceModule10.STUDIO_LY)
						{
							position.y += scale * aSequenceDesc.linearmovement.y;
						}
						if ((aSequenceDesc.motiontype & SourceModule10.STUDIO_LZ) == SourceModule10.STUDIO_LZ)
						{
							position.z += scale * aSequenceDesc.linearmovement.z;
						}
						//NOTE: cos(90) = 0; sin(90) = 1
						tempValue = position.x;
						position.x = position.y;
						position.y = -tempValue;

						rotation.x = anAnimation.theBonePositionsAndRotations[frameIndex].rotation.x;
						rotation.y = anAnimation.theBonePositionsAndRotations[frameIndex].rotation.y;
						rotation.z = anAnimation.theBonePositionsAndRotations[frameIndex].rotation.z + MathModule.DegreesToRadians(-90);
					}
					else
					{
						position.x = anAnimation.theBonePositionsAndRotations[frameIndex].position.x;
						position.y = anAnimation.theBonePositionsAndRotations[frameIndex].position.y;
						position.z = anAnimation.theBonePositionsAndRotations[frameIndex].position.z;

						rotation.x = anAnimation.theBonePositionsAndRotations[frameIndex].rotation.x;
						rotation.y = anAnimation.theBonePositionsAndRotations[frameIndex].rotation.y;
						rotation.z = anAnimation.theBonePositionsAndRotations[frameIndex].rotation.z;
					}

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

					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSection(SourceMdlModel06 aBodyModel)
		{
			string line = "";
			string materialLine = "";
			string vertex1Line = "";
			string vertex2Line = "";
			string vertex3Line = "";
			int materialIndex = 0;
			string materialName = null;
			SourceMdlMesh06 aMesh = null;
			SourceMdlTexture06 aTexture = null;

			//triangles
			line = "triangles";
			theOutputFileStreamWriter.WriteLine(line);

			try
			{
				if (aBodyModel.theMeshes != null)
				{
					for (int meshIndex = 0; meshIndex < aBodyModel.theMeshes.Count; meshIndex++)
					{
						aMesh = aBodyModel.theMeshes[meshIndex];
						materialIndex = aMesh.skinref;
						aTexture = theMdlFileData.theTextures[materialIndex];
						materialName = aTexture.theFileName;

						if (aMesh.theVertexAndNormalIndexes != null)
						{
							for (int groupIndex = 0; groupIndex <= aMesh.theVertexAndNormalIndexes.Count - 3; groupIndex += 3)
							{
								materialLine = materialName;

								//NOTE: Reverse the order of the vertices so the normals will be on the correct side of the face.
								vertex1Line = GetVertexLine(aBodyModel, aMesh.theVertexAndNormalIndexes[groupIndex + 2], aTexture);
								vertex2Line = GetVertexLine(aBodyModel, aMesh.theVertexAndNormalIndexes[groupIndex + 1], aTexture);
								vertex3Line = GetVertexLine(aBodyModel, aMesh.theVertexAndNormalIndexes[groupIndex], aTexture);

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
			catch (Exception ex)
			{
				int debug = 4242;
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private string GetVertexLine(SourceMdlModel06 aBodyModel, SourceMdlTriangleVertex06 aStripGroup, SourceMdlTexture06 aTexture)
		{
			string line = null;
			int boneIndex = 0;
			SourceVector vecin = new SourceVector();
			SourceVector rawPosition = null;
			SourceVector rawNormal = null;
			SourceVector position = new SourceVector();
			SourceVector normal = new SourceVector();
			double texCoordX = 0;
			double texCoordY = 0;

			line = "";
			try
			{
				boneIndex = aBodyModel.theVertexBoneInfos[aStripGroup.vertexIndex];
				SourceBoneTransform06 boneTransform = theMdlFileData.theBoneTransforms[boneIndex];

				// Reverse these.
				//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
				//      void Grab_Triangles( s_model_t *pmodel )
				//	// move vertex position to object space.
				//	VectorSubtract( p.org, bonefixup[p.bone].worldorg, tmp );
				//	VectorTransform(tmp, bonefixup[p.bone].im, p.org );
				//------
				//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\mdldec\smdfile.cpp
				//      void WriteTriangles( FILE * pFile, mstudiomodel_t * pmodel )
				//			// Transform vertex position
				//			vec3_t vecin, vecpos, vecnorm;
				//			VectorCopy( pvert[ ptrivert->vertindex ], vecin );
				//			VectorTransform( vecin,
				//				g_bonetransform[ pvertbone[ ptrivert->vertindex ] ], vecpos );
				MathModule.VectorCopy(aBodyModel.theModelDatas[0].theVertexes[aStripGroup.vertexIndex], ref vecin);
				rawPosition = MathModule.VectorTransform(vecin, boneTransform.matrixColumn0, boneTransform.matrixColumn1, boneTransform.matrixColumn2, boneTransform.matrixColumn3);

				position.x = rawPosition.y;
				position.y = -rawPosition.x;
				position.z = rawPosition.z;

				// Reverse these.
				//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
				//      void Grab_Triangles( s_model_t *pmodel )
				//	// move normal to object space.
				//	VectorCopy( normal.org, tmp );
				//	VectorTransform(tmp, bonefixup[p.bone].im, normal.org );
				//	VectorNormalize( normal.org );
				//------
				//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\mdldec\smdfile.cpp
				//      void WriteTriangles( FILE * pFile, mstudiomodel_t * pmodel )
				//			// Transform vertex normal
				//			VectorCopy( pnorm[ ptrivert->normindex ], vecin );
				//			VectorRotate( vecin,
				//				g_bonetransform[ pnormbone[ ptrivert->normindex ] ], vecnorm );
				//			VectorNormalize( vecnorm );
				MathModule.VectorCopy(aBodyModel.theModelDatas[0].theNormals[aStripGroup.normalIndex], ref vecin);
				rawNormal = MathModule.VectorRotate(vecin, boneTransform.matrixColumn0, boneTransform.matrixColumn1, boneTransform.matrixColumn2, boneTransform.matrixColumn3);
				MathModule.VectorNormalize(ref rawNormal);

				normal.x = rawNormal.y;
				normal.y = -rawNormal.x;
				normal.z = rawNormal.z;

				// Reverse these.
				//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\mdldec\smdfile.cpp
				//      void WriteTriangles( FILE * pFile, mstudiomodel_t * pmodel )
				//		// Texture "scale" factor
				//		float ss = 1.0f / ( float )pcurtexture->width;
				//		float st = 1.0f / ( float )pcurtexture->height;
				//				( float )ptrivert->s * ss,
				//				1.0f - ( float )ptrivert->t * st );
				texCoordX = aStripGroup.s / (double)aTexture.width;
				texCoordY = 1 - aStripGroup.t / (double)aTexture.height;

				line = "  ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);

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
				line += texCoordX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				//NOTE: Unlike all other versions, MDL v06 does not use (1 - texCoordY).
				line += texCoordY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
			}
			catch (Exception ex)
			{
				line = "// " + line;
			}

			return line;
		}

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData06 theMdlFileData;

#endregion

	}

}