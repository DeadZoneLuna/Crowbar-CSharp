using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVtxVertex07
	{

		//FROM: src/public/studio.h
		//// NOTE!!! : Changing this number also changes the vtx file format!!!!!
		//#define MAX_NUM_BONES_PER_VERT 3

		//FROM: src/public/optimize.h
		//struct Vertex_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	// these index into the mesh's vert[origMeshVertID]'s bones
		//	unsigned char boneWeightIndex[MAX_NUM_BONES_PER_VERT];
		//	unsigned char numBones;

		//	unsigned short origMeshVertID;

		//	// for sw skinned verts, these are indices into the global list of bones
		//	// for hw skinned verts, these are hardware bone indices
		//	char boneID[MAX_NUM_BONES_PER_VERT];
		//};

		public byte[] boneWeightIndex = new byte[SourceConstants.MAX_NUM_BONES_PER_VERT];
		public byte boneCount;

		public ushort originalMeshVertexIndex;

		public byte[] boneId = new byte[SourceConstants.MAX_NUM_BONES_PER_VERT];

	}

}