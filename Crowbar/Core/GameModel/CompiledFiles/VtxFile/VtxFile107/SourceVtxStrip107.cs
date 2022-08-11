using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVtxStrip107
	{

		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
		//// a strip is a piece of a stripgroup that is divided by bones 
		//// (and potentially tristrips if we remove some degenerates.)
		//struct StripHeader_t
		//{
		//	// indexOffset offsets into the mesh's index array.
		//	int numIndices;
		//	int indexOffset;
		//
		//	// vertexOffset offsets into the mesh's vert array.
		//	int numVerts;
		//	int vertOffset;
		//
		//	// use this to enable/disable skinning.  
		//	// May decide (in optimize.cpp) to put all with 1 bone in a different strip 
		//	// than those that need skinning.
		//	short numBones;  
		//	
		//	unsigned char flags;
		//	
		//	int numBoneStateChanges;
		//	int boneStateChangeOffset;
		//	inline BoneStateChangeHeader_t *pBoneStateChange( int i ) const 
		//	{ 
		//		return (BoneStateChangeHeader_t *)(((byte *)this) + boneStateChangeOffset) + i; 
		//	};
		//};
		//------
		//FROM: VAMPTools-master\MDLConverter\inc\external\optimize.h
		//	short numIndices;
		//	short indexOffset;
		//	short numVerts;
		//	short vertOffset;
		//	unsigned char flags; 
		//	char spacing[7];

		//Public indexCount As Integer
		//Public indexMeshIndex As Integer
		//Public vertexCount As Integer
		//Public vertexMeshIndex As Integer
		//Public boneCount As Short
		//Public flags As Byte
		//Public boneStateChangeCount As Integer
		//Public boneStateChangeOffset As Integer
		//------
		public short indexCount;
		public short indexMeshIndex;
		public short vertexCount;
		public short vertexMeshIndex;

		public byte boneCount;
		public byte flags;
		public short boneStateChangeCount;
		public int boneStateChangeOffset;



		public List<SourceVtxBoneStateChange107> theVtxBoneStateChanges;



		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\common\optimize.h
		//enum StripHeaderFlags_t {
		//	STRIP_IS_TRILIST	= 0x01,
		//	STRIP_IS_TRISTRIP	= 0x02
		//};

		public const byte SourceStripTriList = 0x1;
		public const byte SourceStripTriStrip = 0x2;

	}

}