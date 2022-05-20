//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Crowbar
{
	public class SourceMdlFlexOp
	{

		//FROM: SourceEngine2006+_source\public\studio.h
		//struct mstudioflexop_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int		op;
		//	union 
		//	{
		//		int		index;
		//		float	value;
		//	} d;
		//};

		//FROM: SourceEngine2006+_source\public\studio.h
		//#define STUDIO_CONST	1	// get float
		//#define STUDIO_FETCH1	2	// get Flexcontroller value
		//#define STUDIO_FETCH2	3	// get flex weight
		//#define STUDIO_ADD		4
		//#define STUDIO_SUB		5
		//#define STUDIO_MUL		6
		//#define STUDIO_DIV		7
		//#define STUDIO_NEG		8	// not implemented
		//#define STUDIO_EXP		9	// not implemented
		//#define STUDIO_OPEN	10	// only used in token parsing
		//#define STUDIO_CLOSE	11
		//#define STUDIO_COMMA	12	// only used in token parsing
		//#define STUDIO_MAX		13
		//#define STUDIO_MIN		14
		//#define STUDIO_2WAY_0	15	// Fetch a value from a 2 Way slider for the 1st value RemapVal( 0.0, 0.5, 0.0, 1.0 )
		//#define STUDIO_2WAY_1	16	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
		//#define STUDIO_NWAY	17	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
		//#define STUDIO_COMBO	18	// Perform a combo operation (essentially multiply the last N values on the stack)
		//#define STUDIO_DOMINATE	19	// Performs a combination domination operation
		//#define STUDIO_DME_LOWER_EYELID 20	// 
		//#define STUDIO_DME_UPPER_EYELID 21	// 
		public const int STUDIO_CONST = 1;
		public const int STUDIO_FETCH1 = 2;
		public const int STUDIO_FETCH2 = 3;
		public const int STUDIO_ADD = 4;
		public const int STUDIO_SUB = 5;
		public const int STUDIO_MUL = 6;
		public const int STUDIO_DIV = 7;
		public const int STUDIO_NEG = 8;
		public const int STUDIO_EXP = 9;
		public const int STUDIO_OPEN = 10;
		public const int STUDIO_CLOSE = 11;
		public const int STUDIO_COMMA = 12;
		public const int STUDIO_MAX = 13;
		public const int STUDIO_MIN = 14;
		public const int STUDIO_2WAY_0 = 15;
		public const int STUDIO_2WAY_1 = 16;
		public const int STUDIO_NWAY = 17;
		public const int STUDIO_COMBO = 18;
		public const int STUDIO_DOMINATE = 19;
		public const int STUDIO_DME_LOWER_EYELID = 20;
		public const int STUDIO_DME_UPPER_EYELID = 21;


		public int op;

		public int index;
		//------
		public double value;

	}

}