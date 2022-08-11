using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlCompressedIkError
	{

		//FROM: se2007_src\src_main\public\studio.h
		//struct mstudiocompressedikerror_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	float	scale[6];
		//	short	offset[6];
		//	inline mstudioanimvalue_t *pAnimvalue( int i ) const { if (offset[i] > 0) return  (mstudioanimvalue_t *)(((byte *)this) + offset[i]); else return NULL; };
		//	mstudiocompressedikerror_t(){}

		//private:
		//	// No copy constructors allowed
		//	mstudiocompressedikerror_t(const mstudiocompressedikerror_t& vOther);
		//};



		public double[] scale = new double[6];
		public short[] offset = new short[6];

		public List<SourceMdlAnimationValue>[] theAnimValues = new List<SourceMdlAnimationValue>[6];

	}

}