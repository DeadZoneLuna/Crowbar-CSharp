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
	public class SourceVtxMaterialReplacement06
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
		//struct MaterialReplacementHeader_t
		//{
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