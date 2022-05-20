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
	public class SourceVtxModelLod07
	{

		//FROM: src/public/optimize.h
		//struct ModelLODHeader_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int numMeshes;
		//	int meshOffset;
		//	float switchPoint;
		//	inline MeshHeader_t *pMesh( int i ) const 
		//	{ 
		//		MeshHeader_t *pDebug = (MeshHeader_t *)(((byte *)this) + meshOffset) + i; 
		//		return pDebug;
		//	};
		//};

		public int meshCount;
		public int meshOffset;
		// Is this the "threshold" value for the QC file's $lod command? Seems to be, based on MDLDecompiler's conversion of producer.mdl.
		public float switchPoint;



		public List<SourceVtxMesh07> theVtxMeshes;
		public bool theVtxModelLodUsesFacial;

	}

}