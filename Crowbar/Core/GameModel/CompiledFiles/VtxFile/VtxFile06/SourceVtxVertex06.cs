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
	public class SourceVtxVertex06
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
		//#define MAX_NUM_BONES_PER_VERT 4
		//#define MAX_NUM_BONES_PER_TRI ( MAX_NUM_BONES_PER_VERT * 3 )
		//#define MAX_NUM_BONES_PER_STRIP 16
		public static int MAX_NUM_BONES_PER_VERT = 4;

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
		//struct Vertex_t
		//{
		//	// these index into the mesh's vert[origMeshVertID]'s bones
		//	unsigned char boneWeightIndex[MAX_NUM_BONES_PER_VERT];
		////	unsigned char boneWeights[MAX_NUM_BONES_PER_VERT];
		//	// for sw skinned verts, these are indices into the global list of bones
		//	// for hw skinned verts, these are hardware bone indices
		//	short boneID[MAX_NUM_BONES_PER_VERT];
		//	short origMeshVertID;
		//	unsigned char numBones;
		//};

		public byte[] boneWeightIndex = new byte[SourceVtxVertex06.MAX_NUM_BONES_PER_VERT];
		public short[] boneIndex = new short[SourceVtxVertex06.MAX_NUM_BONES_PER_VERT];
		public short originalMeshVertexIndex;
		public byte boneCount;

	}

}