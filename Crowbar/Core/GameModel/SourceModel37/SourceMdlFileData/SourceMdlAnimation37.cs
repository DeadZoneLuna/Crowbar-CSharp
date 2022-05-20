//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlAnimation37
	{

		//// per bone per animation DOF and weight pointers
		//struct mstudioanim_t
		//{
		//	// float		weight;		// bone influence
		//	int	flags;		// weighing options
		//	union
		//	{
		//		int	offset[6];	// pointers to animation 
		//		struct
		//		{
		//			float	pos[3];
		//			float	q[4];
		//		} pose;
		//	} u;
		//	inline mstudioanimvalue_t *pAnimvalue( int i ) const { return  (mstudioanimvalue_t *)(((byte *)this) + u.offset[i]); };
		//};

		public int flags;

		public int[] animationValueOffsets = new int[6];
		public int unused;
		//---
		public SourceVector position;
		public SourceQuaternion rotationQuat;

		public List<SourceMdlAnimationValue10>[] theAnimationValues = new List<SourceMdlAnimationValue10>[6];

		////=============================================================================
		//// Animation flag macros
		////=============================================================================
		//#define STUDIO_POS_ANIMATED		0x0001
		//#define STUDIO_ROT_ANIMATED		0x0002
		public const int STUDIO_POS_ANIMATED = 0x1;
		public const int STUDIO_ROT_ANIMATED = 0x2;

	}

}