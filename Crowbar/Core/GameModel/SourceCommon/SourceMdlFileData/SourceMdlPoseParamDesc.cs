using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlPoseParamDesc
	{

		//struct mstudioposeparamdesc_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int					flags;	// ????
		//	float				start;	// starting value
		//	float				end;	// ending value
		//	float				loop;	// looping range, 0 for no looping, 360 for rotations, etc.
		//};

		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		public int nameOffset;
		//	int					flags;	// ????
		public int flags;
		//	float				start;	// starting value
		public float startingValue;
		//	float				end;	// ending value
		public float endingValue;
		//	float				loop;	// looping range, 0 for no looping, 360 for rotations, etc.
		public float loopingRange;

		public string theName;

	}

}