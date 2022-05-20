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
	public class SourceVtxFileData07 : SourceFileData
	{



		//FROM: SourceEngine2007_source\src/public/optimize.h
		//struct FileHeader_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	// file version as defined by OPTIMIZED_MODEL_FILE_VERSION
		//	int version;

		//	// hardware params that affect how the model is to be optimized.
		//	int vertCacheSize;
		//	unsigned short maxBonesPerStrip;
		//	unsigned short maxBonesPerTri;
		//	int maxBonesPerVert;

		//	// must match checkSum in the .mdl
		//	long checkSum;

		//	int numLODs; // garymcthack - this is also specified in ModelHeader_t and should match

		//	// one of these for each LOD
		//	int materialReplacementListOffset;
		//	MaterialReplacementListHeader_t *pMaterialReplacementList( int lodID ) const
		//	{ 
		//		MaterialReplacementListHeader_t *pDebug = 
		//			(MaterialReplacementListHeader_t *)(((byte *)this) + materialReplacementListOffset) + lodID;
		//		return pDebug;
		//	}

		//	int numBodyParts;
		//	int bodyPartOffset;
		//	inline BodyPartHeader_t *pBodyPart( int i ) const 
		//	{
		//		BodyPartHeader_t *pDebug = (BodyPartHeader_t *)(((byte *)this) + bodyPartOffset) + i;
		//		return pDebug;
		//	};	
		//};



		public int version;

		public int vertexCacheSize;
		public ushort maxBonesPerStrip;
		public ushort maxBonesPerTri;
		public int maxBonesPerVertex;

		public int checksum;

		public int lodCount;

		public int materialReplacementListOffset;

		public int bodyPartCount;
		public int bodyPartOffset;



		public List<SourceVtxBodyPart07> theVtxBodyParts;
		public List<SourceVtxMaterialReplacementList07> theVtxMaterialReplacementLists;

	}

}