using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVtxModel06
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
		//// This maps one to one with models in the mdl file.
		//// There are a bunch of model LODs stored inside potentially due to the qc $lod command
		//struct ModelHeader_t
		//{
		//	int numLODs; // garymcthack - this is also specified in FileHeader_t
		//	int lodOffset;
		//	inline ModelLODHeader_t *pLOD( int i ) const 
		//	{ 
		//		ModelLODHeader_t *pDebug = ( ModelLODHeader_t *)(((byte *)this) + lodOffset) + i; 
		//		return pDebug;
		//	};
		//};

		public int lodCount;
		public int lodOffset;

		public List<SourceVtxModelLod06> theVtxModelLods;

	}

}