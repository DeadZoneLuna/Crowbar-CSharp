using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVtxStripGroup07
	{

		//FROM: src/public/optimize.h
		//// a locking group
		//// a single vertex buffer
		//// a single index buffer
		//struct StripGroupHeader_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	// These are the arrays of all verts and indices for this mesh.  strips index into 
		//	int numVerts;
		//	int vertOffset;
		//	inline Vertex_t *pVertex( int i ) const 
		//	{ 
		//		return (Vertex_t *)(((byte *)this) + vertOffset) + i; 
		//	};

		//	int numIndices;
		//	int indexOffset;
		//	inline unsigned short *pIndex( int i ) const 
		//	{ 
		//		return (unsigned short *)(((byte *)this) + indexOffset) + i; 
		//	};

		//	int numStrips;
		//	int stripOffset;
		//	inline StripHeader_t *pStrip( int i ) const 
		//	{ 
		//		return (StripHeader_t *)(((byte *)this) + stripOffset) + i; 
		//	};

		//	unsigned char flags;
		//};
		//======
		// VERSION 49
		//FROM: AlienSwarm_source\src\public\optimize.h
		//// a locking group
		//// a single vertex buffer
		//// a single index buffer
		//struct StripGroupHeader_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	// These are the arrays of all verts and indices for this mesh.  strips index into 
		//	int numVerts;
		//	int vertOffset;
		//	inline Vertex_t *pVertex( int i ) const 
		//	{ 
		//		return (Vertex_t *)(((byte *)this) + vertOffset) + i; 
		//	};

		//	int numIndices;
		//	int indexOffset;
		//	inline unsigned short *pIndex( int i ) const 
		//	{ 
		//		return (unsigned short *)(((byte *)this) + indexOffset) + i; 
		//	};

		//	int numStrips;
		//	int stripOffset;
		//	inline StripHeader_t *pStrip( int i ) const 
		//	{ 
		//		return (StripHeader_t *)(((byte *)this) + stripOffset) + i; 
		//	};

		//	unsigned char flags;

		//	int numTopologyIndices;
		//	int topologyOffset;
		//	inline unsigned short *pTopologyIndex( int i ) const 
		//	{ 
		//		return (unsigned short *)(((byte *)this) + topologyOffset) + i; 
		//	};
		//};


		public int vertexCount;
		public int vertexOffset;

		public int indexCount;
		public int indexOffset;

		public int stripCount;
		public int stripOffset;

		public byte flags;

		//------
		// MDL VERSION 49 (except L4D and L4D2?) adds these two fields
		//	int numTopologyIndices;
		//	int topologyOffset;
		public int topologyIndexCount;
		public int topologyIndexOffset;



		public List<SourceVtxVertex07> theVtxVertexes;
		public List<ushort> theVtxIndexes;
		public List<SourceVtxStrip07> theVtxStrips;
		public List<ushort> theVtxTopologyIndexes;


		//FROM: src/public/optimize.h
		//	Enum StripGroupFlags_t
		//{
		//	STRIPGROUP_IS_FLEXED		= 0x01,
		//	STRIPGROUP_IS_HWSKINNED		= 0x02,
		//	STRIPGROUP_IS_DELTA_FLEXED	= 0x04,
		//	STRIPGROUP_SUPPRESS_HW_MORPH = 0x08,	// NOTE: This is a temporary flag used at run time.
		//};

		public const byte SourceStripGroupFlexed = 0x1;
		public const byte SourceStripGroupHwSkinned = 0x2;
		public const byte SourceStripGroupDeltaFixed = 0x4;
		public const byte SourceStripGroupSuppressHwMorph = 0x8;

	}

}