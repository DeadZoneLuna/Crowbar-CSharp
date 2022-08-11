using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlIncludeModel2531
	{

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudiomodelgroup_t	// f64: +
		//{
		//	int					szlabelindex;
		//	inline char * const	pszLabel( void ) const { return ((char *)this) + szlabelindex; }
		//
		//	int					unk;
		//	int					unk2;
		//
		//	int					unknum;
		//	int					unkindex;
		//
		//	int					minone[24];
		//};

		public int fileNameOffset;
		public int[] unknown = new int[28];

		public string theFileName;
		public string theLabel;

	}

}