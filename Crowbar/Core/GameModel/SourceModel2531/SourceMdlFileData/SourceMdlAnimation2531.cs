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
	public class SourceMdlAnimation2531
	{

		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\Public\studio.h
		//// per bone per animation DOF and weight pointers
		//struct mstudioanim_t
		//{
		//	// float			weight;		// bone influence
		//	int				flags;		// weighing options
		//	union
		//	{
		//		int				offset[6];	// pointers to animation 
		//		struct
		//		{
		//			float			pos[3];
		//			float			q[4];
		//		} pose;
		//	} u;
		//	inline mstudioanimvalue_t *pAnimvalue( int i ) const { return  (mstudioanimvalue_t *)(((byte *)this) + u.offset[i]); };
		//};

		//Public flags As Integer
		public double unknown;

		public int[] theOffsets = new int[7];
		public List<SourceMdlAnimationValue2531> thePositionAnimationXValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> thePositionAnimationYValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> thePositionAnimationZValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> theRotationAnimationXValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> theRotationAnimationYValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> theRotationAnimationZValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> theRotationAnimationWValues = new List<SourceMdlAnimationValue2531>();

		//Public thePosition As SourceVector
		//Public theRotation As SourceQuaternion



		// For the flags field.
		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\Public\studio.h
		//#define STUDIO_POS_ANIMATED		0x0001
		//#define STUDIO_ROT_ANIMATED		0x0002
		public const int STUDIO_POS_ANIMATED = 0x1;
		public const int STUDIO_ROT_ANIMATED = 0x2;

	}

}