using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlFlex2531
	{

		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\Public\studio.h
		//struct mstudioflex_t
		//{
		//	int					flexdesc;	// input value
		//
		//	float				target0;	// zero
		//	float				target1;	// one
		//	float				target2;	// one
		//	float				target3;	// zero
		//
		//	int					numverts;
		//	int					vertindex;
		//	inline	mstudiovertanim_t *pVertanim( int i ) const { return  (mstudiovertanim_t *)(((byte *)this) + vertindex) + i; };
		//};

		public int flexDescIndex;

		public double target0;
		public double target1;
		public double target2;
		public double target3;

		public int vertCount;
		public int vertOffset;

		public int unknown;
		//------
		//Public flexDescPartnerIndex As Integer
		//Public vertAnimType As Byte
		//Public unusedChar(2) As Char
		//Public unused(5) As Integer

		public List<SourceMdlVertAnim2531> theVertAnims;

	}

}