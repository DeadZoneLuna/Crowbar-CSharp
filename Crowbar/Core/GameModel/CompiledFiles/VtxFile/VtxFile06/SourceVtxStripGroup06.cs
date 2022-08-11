using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVtxStripGroup06
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
		//// a locking group
		//// a single vertex buffer
		//// a single index buffer
		//struct StripGroupHeader_t
		//{
		//	// These are the arrays of all verts and indices for this mesh.  strips index into 
		//	int numVerts;
		//	int vertOffset;
		//	inline Vertex_t *pVertex( int i ) const 
		//	{ 
		//		return (Vertex_t *)(((byte *)this) + vertOffset) + i; 
		//	};
		//
		//	int numIndices;
		//	int indexOffset;
		//	inline unsigned short *pIndex( int i ) const 
		//	{ 
		//		return (unsigned short *)(((byte *)this) + indexOffset) + i; 
		//	};
		//
		//	int numStrips;
		//	int stripOffset;
		//	inline StripHeader_t *pStrip( int i ) const 
		//	{ 
		//		return (StripHeader_t *)(((byte *)this) + stripOffset) + i; 
		//	};
		//
		//	unsigned char flags;
		//};

		public int vertexCount;
		public int vertexOffset;
		public int indexCount;
		public int indexOffset;
		public int stripCount;
		public int stripOffset;
		public byte flags;

		public List<ushort> theVtxIndexes;
		public List<SourceVtxStrip06> theVtxStrips;
		public List<SourceVtxVertex06> theVtxVertexes;


		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
		//      [Version 36]
		//enum StripGroupFlags_t {
		//	STRIPGROUP_IS_FLEXED	= 0x01,
		//	STRIPGROUP_IS_HWSKINNED	= 0x02
		//};
		//------
		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
		//enum StripGroupFlags_t {
		//	STRIPGROUP_IS_FLEXED	= 0x01,
		//	STRIPGROUP_IS_HWSKINNED	= 0x02
		//};

		public const byte STRIPGROUP_IS_FLEXED = 0x1;
		public const byte STRIPGROUP_IS_HWSKINNED = 0x2;

	}

}