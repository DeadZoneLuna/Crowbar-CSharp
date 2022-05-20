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
	public class SourceVtxModelLod06
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
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
		public float switchPoint;

		public List<SourceVtxMesh06> theVtxMeshes;
		public bool theVtxModelLodUsesFacial;

	}

}