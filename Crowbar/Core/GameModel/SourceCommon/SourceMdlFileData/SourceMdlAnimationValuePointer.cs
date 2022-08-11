using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlAnimationValuePointer
	{

		//FROM: SourceEngine2006_source\public\studio.h
		//struct mstudioanim_valueptr_t
		//{
		//	short	offset[3];
		//	inline mstudioanimvalue_t *pAnimvalue( int i ) const { if (offset[i] > 0) return  (mstudioanimvalue_t *)(((byte *)this) + offset[i]); else return NULL; };
		//};


		public short animXValueOffset;
		public short animYValueOffset;
		public short animZValueOffset;

		public List<SourceMdlAnimationValue> theAnimXValues;
		public List<SourceMdlAnimationValue> theAnimYValues;
		public List<SourceMdlAnimationValue> theAnimZValues;

	}

}