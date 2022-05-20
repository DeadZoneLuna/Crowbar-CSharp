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
	public class SourceSmdFile14
	{

#region Creation and Destruction

		public SourceSmdFile14(StreamWriter outputFileStream, SourceMdlFileData14 mdlFileData)
		{
			this.theOutputFileStreamWriter = outputFileStream;
			this.theMdlFileData = mdlFileData;
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
				//line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionY.ToString("0.000000", TheApp.InternalNumberFormat)
				//line += " "
				//line += (-Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionX).ToString("0.000000", TheApp.InternalNumberFormat)
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
			this.theOutputFileStreamWriter.WriteLine(line);

			this.theAnimationFrameLines = new SortedList<int, AnimationFrameLine>();
			for (int frameIndex = 0; frameIndex < aSequenceDesc.frameCount; frameIndex++)
			{
				this.theAnimationFrameLines.Clear();
				this.CalcAnimation(aSequenceDesc, blendIndex, frameIndex);

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
					if (this.theMdlFileData.theBones[boneIndex].parentBoneIndex == -1)
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
					if (this.theMdlFileData.theBones[boneIndex].parentBoneIndex == -1)
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

					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSection(SourceMdlModel14 aBodyModel)
		{
			string line = "";
			string materialLine = "";
			string vertex1Line = "";
			string vertex2Line = "";
			string vertex3Line = "";
			//Dim materialIndex As Integer
			//Dim materialName As String
			//Dim aMesh As SourceMdlMesh14
			//Dim aTexture As SourceMdlTexture14
			int boneIndex = 0;
			int aVertexIndex = 0;

			//triangles
			line = "triangles";
			this.theOutputFileStreamWriter.WriteLine(line);

			try
			{
				//If aBodyModel.theMeshes IsNot Nothing Then
				//	For meshIndex As Integer = 0 To aBodyModel.theMeshes.Count - 1
				//		aMesh = aBodyModel.theMeshes(meshIndex)
				//		materialIndex = aMesh.skinref
				//		aTexture = Me.theMdlFileData.theTextures(materialIndex)
				//		materialName = aTexture.theFileName

				//		'For groupIndex As Integer = 1 To aStripOrFan.theVertexInfos.Count - 2
				//		'	materialLine = materialName

				//		'	vertex1Line = Me.GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos(0), aTexture)
				//		'	vertex2Line = Me.GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos(groupIndex + 1), aTexture)
				//		'	vertex3Line = Me.GetVertexLine(aBodyModel, aStripOrFan.theVertexInfos(groupIndex), aTexture)

				//		'	If vertex1Line.StartsWith("// ") OrElse vertex2Line.StartsWith("// ") OrElse vertex3Line.StartsWith("// ") Then
				//		'		materialLine = "// " + materialLine
				//		'		If Not vertex1Line.StartsWith("// ") Then
				//		'			vertex1Line = "// " + vertex1Line
				//		'		End If
				//		'		If Not vertex2Line.StartsWith("// ") Then
				//		'			vertex2Line = "// " + vertex2Line
				//		'		End If
				//		'		If Not vertex3Line.StartsWith("// ") Then
				//		'			vertex3Line = "// " + vertex3Line
				//		'		End If
				//		'	End If
				//		'	Me.theOutputFileStreamWriter.WriteLine(materialLine)
				//		'	Me.theOutputFileStreamWriter.WriteLine(vertex1Line)
				//		'	Me.theOutputFileStreamWriter.WriteLine(vertex2Line)
				//		'	Me.theOutputFileStreamWriter.WriteLine(vertex3Line)
				//		'Next
				//	Next
				//End If
				for (int aVertexIndexIndex = 0; aVertexIndexIndex <= this.theMdlFileData.theIndexes.Count - 3; aVertexIndexIndex += 3)
				{
					materialLine = this.theMdlFileData.theTextures[0].theTextureName;

					boneIndex = 0;

					aVertexIndex = this.theMdlFileData.theIndexes[aVertexIndexIndex];
					vertex1Line = this.GetVertexLine(boneIndex, this.theMdlFileData.theVertexes[aVertexIndex], this.theMdlFileData.theNormals[aVertexIndex], this.theMdlFileData.theUVs[aVertexIndex], aBodyModel.theWeightingHeaders[0], this.theMdlFileData.theWeightings[aVertexIndex]);
					aVertexIndex = this.theMdlFileData.theIndexes[aVertexIndexIndex + 2];
					vertex2Line = this.GetVertexLine(boneIndex, this.theMdlFileData.theVertexes[aVertexIndex], this.theMdlFileData.theNormals[aVertexIndex], this.theMdlFileData.theUVs[aVertexIndex], aBodyModel.theWeightingHeaders[0], this.theMdlFileData.theWeightings[aVertexIndex]);
					aVertexIndex = this.theMdlFileData.theIndexes[aVertexIndexIndex + 1];
					vertex3Line = this.GetVertexLine(boneIndex, this.theMdlFileData.theVertexes[aVertexIndex], this.theMdlFileData.theNormals[aVertexIndex], this.theMdlFileData.theUVs[aVertexIndex], aBodyModel.theWeightingHeaders[0], this.theMdlFileData.theWeightings[aVertexIndex]);

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

		private string GetVertexLine(int boneIndex, SourceVector position, SourceVector normal, SourceVector uv, SourceMdlWeightingHeader14 weightingHeader, SourceMdlWeighting14 weighting)
		{
			string line = "";

			try
			{
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
				line += uv.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += (-uv.y).ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				if (weighting.boneCount > 0)
				{
					line += " ";
					line += weighting.boneCount.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					for (int x = 0; x < weighting.boneCount; x++)
					{
						line += " ";
						line += weightingHeader.theWeightingBoneDatas[0].theWeightingBoneIndexes[weighting.bones[x]].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += weighting.weights[x].ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
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
		private void CalcAnimation(SourceMdlSequenceDesc10 aSequenceDesc, int blendIndex, int frameIndex)
		{
			double s = 0;
			SourceMdlBone10 aBone = null;
			SourceMdlAnimation10 anAnimation = null;
			SourceVector rot = null;
			SourceVector pos = null;
			AnimationFrameLine aFrameLine = null;

			s = 0;

			for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
			{
				aBone = this.theMdlFileData.theBones[boneIndex];
				anAnimation = aSequenceDesc.theAnimations[blendIndex * this.theMdlFileData.theBones.Count + boneIndex];

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
				angleVector.x = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[3], aBone.rotationScale.x, aBone.rotation.x);
			}
			if (anAnimation.animationValueOffsets[4] <= 0)
			{
				angleVector.y = aBone.rotation.y;
			}
			else
			{
				angleVector.y = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[4], aBone.rotationScale.y, aBone.rotation.y);
			}
			if (anAnimation.animationValueOffsets[5] <= 0)
			{
				angleVector.z = aBone.rotation.z;
			}
			else
			{
				angleVector.z = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[5], aBone.rotationScale.z, aBone.rotation.z);
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
				pos.x = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[0], aBone.positionScale.x, aBone.position.x);
			}

			if (anAnimation.animationValueOffsets[1] <= 0)
			{
				//pos.y = 0
				pos.y = aBone.position.y;
			}
			else
			{
				pos.y = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[1], aBone.positionScale.y, aBone.position.y);
			}

			if (anAnimation.animationValueOffsets[2] <= 0)
			{
				//pos.z = 0
				pos.z = aBone.position.z;
			}
			else
			{
				pos.z = this.ExtractAnimValue(frameIndex, anAnimation.theAnimationValues[2], aBone.positionScale.z, aBone.position.z);
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
		private SourceMdlFileData14 theMdlFileData;

		private SortedList<int, AnimationFrameLine> theAnimationFrameLines;

#endregion

	}

}