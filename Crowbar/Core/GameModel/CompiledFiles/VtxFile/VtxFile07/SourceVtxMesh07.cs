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
	public class SourceVtxMesh07
	{

		//FROM: src/public/optimize.h
		//// a collection of locking groups:
		//// up to 4:
		//// non-flexed, hardware skinned
		//// flexed, hardware skinned
		//// non-flexed, software skinned
		//// flexed, software skinned
		////
		//// A mesh has a material associated with it.
		//struct MeshHeader_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int numStripGroups;
		//	int stripGroupHeaderOffset;
		//	inline StripGroupHeader_t *pStripGroup( int i ) const 
		//	{ 
		//		StripGroupHeader_t *pDebug = (StripGroupHeader_t *)(((byte *)this) + stripGroupHeaderOffset) + i; 
		//		return pDebug;
		//	};
		//	unsigned char flags;
		//};

		public int stripGroupCount;
		public int stripGroupOffset;
		public byte flags;



		public List<SourceVtxStripGroup07> theVtxStripGroups;



		//FROM: src/public/optimize.h
		//enum MeshFlags_t { 
		//	// these are both material properties, and a mesh has a single material.
		//	MESH_IS_TEETH	= 0x01, 
		//	MESH_IS_EYES	= 0x02
		//};

		public const byte SourceMeshTeeth = 0x1;
		public const byte SourceMeshEyes = 0x2;

	}

}