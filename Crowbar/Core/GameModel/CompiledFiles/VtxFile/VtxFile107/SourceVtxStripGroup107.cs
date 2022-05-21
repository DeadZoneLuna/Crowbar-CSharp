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
	public class SourceVtxStripGroup107
	{

		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
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
		//------
		//FROM: VAMPTools-master\MDLConverter\inc\external\optimize.h
		//	short numVerts;
		//	short vertOffset;
		//	short numStrips;
		//	short padding;
		//	int vertTableOffset;
		//	int indexOffset;
		//	int stripOffset;

		//Public vertexCount As Integer
		//Public vertexOffset As Integer
		//Public indexCount As Integer
		//Public indexOffset As Integer
		//Public stripCount As Integer
		//Public stripOffset As Integer
		//Public flags As Byte
		//------
		public short vertexCount;
		public short indexCount;
		public short stripCount;
		public byte flags;
		public byte unknown;
		public int vertexOffset;
		public int indexOffset;
		public int stripOffset;


		public List<ushort> theVtxIndexes;
		public List<SourceVtxStrip107> theVtxStrips;
		public List<SourceVtxVertex107> theVtxVertexes;
		public List<UInt16> theVtxVertexesForStaticProp;
		//Public theVtxVertexesType2 As List(Of UInt16)


		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
		//enum StripGroupFlags_t {
		//	STRIPGROUP_IS_FLEXED	= 0x01,
		//	STRIPGROUP_IS_HWSKINNED	= 0x02
		//};
		//------
		//FROM: VAMPTools-master\MDLConverter\inc\external\optimize.h
		//enum StripGroupFlags_t 
		//{
		//	STRIPGROUP_IS_FLEXED		= 0x01,
		//	STRIPGROUP_IS_HWSKINNED		= 0x02,
		//	STRIPGROUP_IS_DELTA_FLEXED	= 0x04,
		//	STRIPGROUP_SUPPRESS_HW_MORPH = 0x08,	// NOTE: This is a temporary flag used at run time.
		//};

		public const byte STRIPGROUP_IS_FLEXED = 0x1;
		public const byte STRIPGROUP_IS_HWSKINNED = 0x2;
		public const byte STRIPGROUP_IS_DELTA_FLEXED = 0x4;
		public const byte STRIPGROUP_SUPPRESS_HW_MORPH = 0x8;
		//NOTE: This is completely made up based on seeing the flag set for static props.
		public const byte STRIPGROUP_USES_STATIC_PROP_VERTEXES = 0x10;

	}

}