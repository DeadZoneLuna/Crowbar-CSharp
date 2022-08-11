using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBoneTransform
	{

		//FROM: SourceEngine2007\src_main\public\studio.h
		////-----------------------------------------------------------------------------
		//// Src bone transforms are transformations that will convert .dmx or .smd-based animations into .mdl-based animations
		//// NOTE: The operation you should apply is: pretransform * bone transform * posttransform
		////-----------------------------------------------------------------------------
		//struct mstudiosrcbonetransform_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();

		//	int			sznameindex;
		//	inline const char *pszName( void ) const { return ((char *)this) + sznameindex; }
		//	matrix3x4_t	pretransform;	
		//	matrix3x4_t	posttransform;	
		//};

		//	int			sznameindex;
		public int nameOffset;

		//	matrix3x4_t	pretransform;	
		public SourceVector preTransformColumn0;
		public SourceVector preTransformColumn1;
		public SourceVector preTransformColumn2;
		public SourceVector preTransformColumn3;
		//	matrix3x4_t	posttransform;	
		public SourceVector postTransformColumn0;
		public SourceVector postTransformColumn1;
		public SourceVector postTransformColumn2;
		public SourceVector postTransformColumn3;



		public string theName;

	}

}