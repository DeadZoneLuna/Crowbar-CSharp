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
	public class SourceVtxModelLod107
	{

		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
		//struct ModelLODHeader_t
		//{
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
		public double switchPoint;



		public List<SourceVtxMesh107> theVtxMeshes;

	}

}