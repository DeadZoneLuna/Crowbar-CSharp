using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlAttachment37
	{

		//struct mstudioattachment_t
		//{
		//	int		sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int		type;
		//	int		bone;
		//	matrix3x4_t	local; // attachment point
		//};

		public int nameOffset;
		public int type;
		public int boneIndex;
		public float localM11;
		public float localM12;
		public float localM13;
		public float localM14;
		public float localM21;
		public float localM22;
		public float localM23;
		public float localM24;
		public float localM31;
		public float localM32;
		public float localM33;
		public float localM34;

		public string theName;

	}

}