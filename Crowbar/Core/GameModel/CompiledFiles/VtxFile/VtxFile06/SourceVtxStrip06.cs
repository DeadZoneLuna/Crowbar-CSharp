﻿//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVtxStrip06
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
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

		public int indexCount;
		public int indexMeshIndex;
		public int vertexCount;
		public int vertexMeshIndex;

		public short boneCount;
		public byte flags;
		public int boneStateChangeCount;
		public int boneStateChangeOffset;

		public List<SourceVtxBoneStateChange06> theVtxBoneStateChanges;


		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
		//enum StripHeaderFlags_t {
		//	STRIP_IS_TRILIST	= 0x01,
		//	STRIP_IS_TRISTRIP	= 0x02
		//};

		public const byte STRIP_IS_TRILIST = 0x1;
		public const byte STRIP_IS_TRISTRIP = 0x2;

	}

}