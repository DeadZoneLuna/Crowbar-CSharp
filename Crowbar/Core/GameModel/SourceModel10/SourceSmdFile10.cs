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
	public class SourceSmdFile10
	{

#region Creation and Destruction

		public SourceSmdFile10(StreamWriter outputFileStream, SourceMdlFileData10 mdlFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
		}

#endregion

#region Methods

		//NOTE: Half-Life SDK model compiler does not allow comments in an SMD file.
		//Public Sub WriteHeaderComment()
		//	Common.WriteHeaderComment(Me.theOutputFileStreamWriter)
		//End Sub

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
				//line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionY.ToString("0.000000", TheApp.InternalNumberFormat)
				//line += " "
				//line += (-Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionX).ToString("0.000000", TheApp.InternalNumberFormat)
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

		public void WriteSkeletonSectionForAnimation(SourceMdlSequenceDesc10 aSequenceDesc, int blendIndex)
		{
			string line = "";
			int boneIndex = 0;
			AnimationFrameLine aFrameLine = null;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();
			double scale = 0;
			double tempValue = 0;

			//skeleton
			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			theAnimationFrameLines = new SortedList<int, AnimationFrameLine>();
			for (int frameIndex = 0; frameIndex < aSequenceDesc.frameCount; frameIndex++)
			{
				theAnimationFrameLines.Clear();
				CalcAnimation(aSequenceDesc, blendIndex, frameIndex);

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

					position.x = aFrameLine.position.x;
					position.y = aFrameLine.position.y;
					position.z = aFrameLine.position.z;

					if (theMdlFileData.theBones[boneIndex].parentBoneIndex == -1)
					{
						// Extract root-bone motion.
						if (aSequenceDesc.frameCount > 1)
						{
							//void ExtractMotion( )
							//[...]
							//			for (j = 0; j < sequence[i].numframes; j++)
							//[...]
							//						VectorScale( motion, j * 1.0 / (sequence[i].numframes - 1), adj );
							//[...]
							//							VectorSubtract( sequence[i].panim[q]->pos[k][j], adj, sequence[i].panim[q]->pos[k][j] );
							//[...]
							//			VectorCopy( motion, sequence[i].linearmovement );
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
						}

						//	defaultzrotation = Q_PI / 2;
						//int Cmd_Sequence( )
						//[...]
						//	zrotation = defaultzrotation;
						//void Option_Rotate(void )
						//{
						//	GetToken (false);
						//	zrotation = (atof( token ) + 90) * (Q_PI / 180.0);
						//}
						//void Grab_Animation( s_animation_t *panim)
						//[...]
						//	cz = cos( zrotation );
						//	sz = sin( zrotation );
						//[...]
						//				if (panim->node[index].parent == -1) {
						//[...]
						//					panim->pos[index][t][0] = cz * pos[0] - sz * pos[1];
						//					panim->pos[index][t][1] = sz * pos[0] + cz * pos[1];
						//					panim->pos[index][t][2] = pos[2];
						//[...]
						//				}
						//NOTE: cos(90) = 0; sin(90) = 1
						tempValue = position.x;
						position.x = position.y;
						position.y = -tempValue;
					}

					rotation.x = aFrameLine.rotation.x;
					rotation.y = aFrameLine.rotation.y;
					if (theMdlFileData.theBones[boneIndex].parentBoneIndex == -1)
					{
						//	defaultzrotation = Q_PI / 2;
						//int Cmd_Sequence( )
						//[...]
						//	zrotation = defaultzrotation;
						//void Option_Rotate(void )
						//{
						//	GetToken (false);
						//	zrotation = (atof( token ) + 90) * (Q_PI / 180.0);
						//}
						//void Grab_Animation( s_animation_t *panim)
						//[...]
						//				if (panim->node[index].parent == -1) {
						//[...]
						//					// rotate model
						//					rot[2]			+= zrotation;
						//				}
						rotation.z = aFrameLine.rotation.z + MathModule.DegreesToRadians(-90);
					}
					else
					{
						rotation.z = aFrameLine.rotation.z;
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

		public void WriteTrianglesSection(SourceMdlModel10 aBodyModel)
		{
			string line = "";
			string materialLine = "";
			string vertex1Line = "";
			string vertex2Line = "";
			string vertex3Line = "";
			int materialIndex = 0;
			string materialName = null;
			SourceMdlMesh10 aMesh = null;
			SourceMdlTexture10 aTexture = null;

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

						if (aMesh.theStripsAndFans != null)
						{
							for (int stripsAndFansIndex = 0; stripsAndFansIndex < aMesh.theStripsAndFans.Count; stripsAndFansIndex++)
							{
								SourceMeshTriangleStripOrFan10 aStripOrFan = aMesh.theStripsAndFans[stripsAndFansIndex];

								if (aStripOrFan.theFacesAreStoredAsTriangleStrips)
								{
									for (int vertexInfoIndex = 0; vertexInfoIndex <= aStripOrFan.theVertexInfos.Count - 3; vertexInfoIndex++)
									{
										materialLine = materialName;

										if (vertexInfoIndex % 2 == 0)
										{
											// even
											vertex1Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[vertexInfoIndex], aTexture);
											vertex2Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[vertexInfoIndex + 2], aTexture);
											vertex3Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[vertexInfoIndex + 1], aTexture);
										}
										else
										{
											// odd
											vertex1Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[vertexInfoIndex], aTexture);
											vertex2Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[vertexInfoIndex + 1], aTexture);
											vertex3Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[vertexInfoIndex + 2], aTexture);
										}

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
								else
								{
									for (int groupIndex = 1; groupIndex <= aStripOrFan.theVertexInfos.Count - 2; groupIndex++)
									{
										materialLine = materialName;

										vertex1Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[0], aTexture);
										vertex2Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[groupIndex + 1], aTexture);
										vertex3Line = GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos[groupIndex], aTexture);

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

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private string GetVertexLine(SourceMdlModel10 aBodyModel, SourceMdlVertexInfo10 aVertexInfo, SourceMdlTexture10 aTexture)
		{
			string line = null;
			int boneIndex = 0;
			SourceVector vecin = new SourceVector();
			SourceVector position = null;
			SourceVector normal = null;
			double texCoordX = 0;
			double texCoordY = 0;

			line = "";
			try
			{
				boneIndex = aBodyModel.theVertexBoneInfos[aVertexInfo.vertexIndex];
				SourceBoneTransform10 boneTransform = theMdlFileData.theBoneTransforms[boneIndex];

				// Reverse these.
				//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
				//      void Grab_Triangles( s_model_t *pmodel )
				//	// move vertex position to object space.
				//	VectorSubtract( p.org, bonefixup[p.bone].worldorg, tmp );
				//	VectorTransform(tmp, bonefixup[p.bone].im, p.org );
				//------
				//FROM: HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\mdldec\smdfile.cpp
				//      void WriteTriangles( FILE * pFile, mstudiomodel_t * pmodel )
				//			// Transform vertex position
				//			vec3_t vecin, vecpos, vecnorm;
				//			VectorCopy( pvert[ ptrivert->vertindex ], vecin );
				//			VectorTransform( vecin,
				//				g_bonetransform[ pvertbone[ ptrivert->vertindex ] ], vecpos );
				MathModule.VectorCopy(aBodyModel.theVertexes[aVertexInfo.vertexIndex], ref vecin);
				position = MathModule.VectorTransform(vecin, boneTransform.matrixColumn0, boneTransform.matrixColumn1, boneTransform.matrixColumn2, boneTransform.matrixColumn3);

				// Reverse these.
				//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
				//      void Grab_Triangles( s_model_t *pmodel )
				//	// move normal to object space.
				//	VectorCopy( normal.org, tmp );
				//	VectorTransform(tmp, bonefixup[p.bone].im, normal.org );
				//	VectorNormalize( normal.org );
				//------
				//FROM: HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\mdldec\smdfile.cpp
				//      void WriteTriangles( FILE * pFile, mstudiomodel_t * pmodel )
				//			// Transform vertex normal
				//			VectorCopy( pnorm[ ptrivert->normindex ], vecin );
				//			VectorRotate( vecin,
				//				g_bonetransform[ pnormbone[ ptrivert->normindex ] ], vecnorm );
				//			VectorNormalize( vecnorm );
				MathModule.VectorCopy(aBodyModel.theNormals[aVertexInfo.normalIndex], ref vecin);
				normal = MathModule.VectorRotate(vecin, boneTransform.matrixColumn0, boneTransform.matrixColumn1, boneTransform.matrixColumn2, boneTransform.matrixColumn3);
				MathModule.VectorNormalize(ref normal);

				// Reverse these.
				//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
				//      void TextureCoordRanges( s_mesh_t *pmesh, s_texture_t *ptexture  )
				//	pmesh->triangle[i][j].s = pmesh->triangle[i][j].u * (ptexture->srcwidth - 1);
				//	pmesh->triangle[i][j].t = pmesh->triangle[i][j].v * (ptexture->srcheight - 1);
				//FROM: StudioMDL with Texture Shifting fix v1.02\sources\studiomdl.c
				//      void TextureCoordRanges( s_mesh_t *pmesh, s_texture_t *ptexture  )
				//			pmesh->triangle[i][j].s = adjust_texcoord(pmesh->triangle[i][j].u*(ptexture->srcwidth));
				//			pmesh->triangle[i][j].t = adjust_texcoord(pmesh->triangle[i][j].v*(ptexture->srcheight));
				if (aTexture.width == 1 || aTexture.height == 1)
				{
					if (aTexture.theFileName[0] == '#')
					{
						uint width = 0;
						uint height = 0;
						width = uint.Parse(aTexture.theFileName.Substring(1, 3));
						height = uint.Parse(aTexture.theFileName.Substring(4, 3));
						texCoordX = aVertexInfo.s / (double)width;
						texCoordY = aVertexInfo.t / (double)height;
					}
					else
					{
						texCoordX = aVertexInfo.s;
						texCoordY = aVertexInfo.t;
					}
				}
				else
				{
					if (MainCROWBAR.TheApp.Settings.DecompileUseNonValveUvConversionIsChecked)
					{
						// Match DoomMusic's StudioMDL (StudioMDL with Texture Shifting fix v1.02) because that compiler fixes texture shifting as well as some other stuff.
						//TODO: Is DoomMusic's texture UV calculation incorrect? Should it be using the "- 1" like the original Valve compiler?
						texCoordX = aVertexInfo.s / (double)(aTexture.width);
						texCoordY = aVertexInfo.t / (double)(aTexture.height);
					}
					else
					{
						texCoordX = aVertexInfo.s / (double)(aTexture.width - 1);
						texCoordY = aVertexInfo.t / (double)(aTexture.height - 1);
					}
				}

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
				//line += aVertex.texCoordY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += (1 - texCoordY).ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
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
		private void CalcAnimation(SourceMdlSequenceDesc10 aSequenceDesc, int blendIndex, int frameIndex)
		{
			double s = 0;
			SourceMdlBone10 aBone = null;
			SourceMdlAnimation10 anAnimation = null;
			SourceVector rot = null;
			SourceVector pos = null;
			AnimationFrameLine aFrameLine = null;

			s = 0;

			for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
			{
				aBone = theMdlFileData.theBones[boneIndex];
				anAnimation = aSequenceDesc.theAnimations[blendIndex * theMdlFileData.theBones.Count + boneIndex];

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
		private SourceVector CalcBoneRotation(int frameIndex, double s, SourceMdlBone10 aBone, SourceMdlAnimation10 anAnimation, ref SourceQuaternion rotationQuat)
		{
			SourceQuaternion rot = new SourceQuaternion();
			SourceVector angleVector = new SourceVector();

			if (anAnimation.animationValueOffsets[3] <= 0)
			{
				angleVector.x = aBone.rotation.x;
			}
			else
			{
				angleVector.x = ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[3], aBone.rotationScale.x, aBone.rotation.x);
			}
			if (anAnimation.animationValueOffsets[4] <= 0)
			{
				angleVector.y = aBone.rotation.y;
			}
			else
			{
				angleVector.y = ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[4], aBone.rotationScale.y, aBone.rotation.y);
			}
			if (anAnimation.animationValueOffsets[5] <= 0)
			{
				angleVector.z = aBone.rotation.z;
			}
			else
			{
				angleVector.z = ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[5], aBone.rotationScale.z, aBone.rotation.z);
			}

			angleVector.debug_text = "anim";

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
		private SourceVector CalcBonePosition(int frameIndex, double s, SourceMdlBone10 aBone, SourceMdlAnimation10 anAnimation)
		{
			SourceVector pos = new SourceVector();

			if (anAnimation.animationValueOffsets[0] <= 0)
			{
				//pos.x = 0
				pos.x = aBone.position.x;
			}
			else
			{
				pos.x = ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[0], aBone.positionScale.x, aBone.position.x);
			}

			if (anAnimation.animationValueOffsets[1] <= 0)
			{
				//pos.y = 0
				pos.y = aBone.position.y;
			}
			else
			{
				pos.y = ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[1], aBone.positionScale.y, aBone.position.y);
			}

			if (anAnimation.animationValueOffsets[2] <= 0)
			{
				//pos.z = 0
				pos.z = aBone.position.z;
			}
			else
			{
				pos.z = ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[2], aBone.positionScale.z, aBone.position.z);
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
		public double ExtractAnimValue(int frameIndex, List<SourceMdlAnimationValue10> animValues, double scale, double adjustment)
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
		private SourceMdlFileData10 theMdlFileData;

		private SortedList<int, AnimationFrameLine> theAnimationFrameLines;

#endregion

	}

}