using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlModel06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	char				name[64];
		//
		//	int					type;
		//
		//	int					unk01;			// TOMAS: (==1)
		//	int					unused01;		// TOMAS: UNUSED (checked)
		//
		//	int					nummesh;
		//	int					meshindex;
		//
		//	// vertex bone info
		//	int					numverts;		// number of unique vertices
		//	int					vertinfoindex;	// vertex bone info
		//
		//	// normal bone info
		//	int					numnorms;		// number of unique surface normals
		//	int					norminfoindex;	// normal bone info
		//
		//	// TOMAS: NEW IN MDL v6
		//	int					unused02;		// TOMAS: UNUSED (checked)
		//	int					modeldataindex;	// (->mstudiomodeldata_t)
		//} mstudiomodel_t;

		public char[] name = new char[64];
		public int type;

		public int unknown01;
		public int unused01;

		public int meshCount;
		public int meshOffset;

		public int vertexCount;
		public int vertexBoneInfoOffset;

		public int normalCount;
		public int normalBoneInfoOffset;

		public int unused02;
		public int modelDataOffset;


		public string theSmdFileName;
		public string theName;
		public List<SourceMdlMesh06> theMeshes = new List<SourceMdlMesh06>();
		public List<int> theNormalBoneInfos = new List<int>();
		public List<int> theVertexBoneInfos = new List<int>();
		public List<SourceMdlModelData06> theModelDatas = new List<SourceMdlModelData06>();

	}

}