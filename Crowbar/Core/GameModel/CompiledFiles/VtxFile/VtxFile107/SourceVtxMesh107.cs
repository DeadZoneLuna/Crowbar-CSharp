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
	public class SourceVtxMesh107
	{

		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
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
		//	int numStripGroups;
		//	int stripGroupHeaderOffset;
		//	inline StripGroupHeader_t *pStripGroup( int i ) const 
		//	{ 
		//		StripGroupHeader_t *pDebug = (StripGroupHeader_t *)(((byte *)this) + stripGroupHeaderOffset) + i; 
		//		return pDebug;
		//	};
		//	unsigned char flags;
		//};

		//Public stripGroupCount As Integer
		//Public stripGroupOffset As Integer
		//'Public flags As Byte

		//------

		//	short numStripGroups;
		//	short padding;
		//	int stripGroupHeaderOffset;

		public short stripGroupCount;
		public byte flags;
		public byte unknown;
		public int stripGroupOffset;



		public List<SourceVtxStripGroup107> theVtxStripGroups;



		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
		//enum MeshFlags_t { 
		//	// these are both material properties, and a mesh has a single material.
		//	MESH_IS_TEETH	= 0x01, 
		//	MESH_IS_EYES	= 0x02
		//};

		public const byte MESH_IS_TEETH = 0x1;
		public const byte MESH_IS_EYES = 0x2;

	}

}