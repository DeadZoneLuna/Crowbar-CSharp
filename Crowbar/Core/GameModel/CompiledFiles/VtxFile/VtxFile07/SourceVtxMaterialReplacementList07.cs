using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVtxMaterialReplacementList07
	{

		//FROM: [48] SourceEngine2007_source se2007_src\src_main\public\optimize.h
		//struct MaterialReplacementListHeader_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int numReplacements;
		//	int replacementOffset;
		//	inline MaterialReplacementHeader_t *pMaterialReplacement( int i ) const
		//	{
		//		MaterialReplacementHeader_t *pDebug = ( MaterialReplacementHeader_t *)(((byte *)this) + replacementOffset) + i; 
		//		return pDebug;
		//	}
		//};

		public int replacementCount;
		public int replacementOffset;

		public List<SourceVtxMaterialReplacement07> theVtxMaterialReplacements;

	}

}