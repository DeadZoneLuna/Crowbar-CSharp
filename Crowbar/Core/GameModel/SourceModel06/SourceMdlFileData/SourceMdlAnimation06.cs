using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlAnimation06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	int					numpos;			// count of mstudiobnonepos_t
		//	int					posindex;		// (->mstudiobnonepos_t)
		//	int					numrot;			// count of mstudiobonerot_t
		//	int					rotindex;		// (->mstudiobonerot_t)
		//} mstudioanim_t;

		public int bonePositionCount;
		public int bonePositionOffset;
		public int boneRotationCount;
		public int boneRotationOffset;


		public List<SourceMdlBonePosition06> theRawBonePositions;
		public List<SourceMdlBoneRotation06> theRawBoneRotations;

		public List<SourceBonePostionAndRotation06> theBonePositionsAndRotations;

	}

}