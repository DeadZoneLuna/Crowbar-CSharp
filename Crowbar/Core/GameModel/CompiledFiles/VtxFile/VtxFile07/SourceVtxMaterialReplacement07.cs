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
	public class SourceVtxMaterialReplacement07
	{

		//FROM: [48] SourceEngine2007_source se2007_src\src_main\public\optimize.h
		//struct MaterialReplacementHeader_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	short materialID;
		//	int replacementMaterialNameOffset;
		//	inline const char *pMaterialReplacementName( void )
		//	{
		//		const char *pDebug = (const char *)(((byte *)this) + replacementMaterialNameOffset); 
		//		return pDebug;
		//	}
		//};

		public short materialIndex;
		public int nameOffset;

		public string theName;

	}

}