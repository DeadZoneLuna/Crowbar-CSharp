using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlModel10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// studio models
		//typedef struct
		//{
		//	char				name[64];
		//
		//	int					type;
		//
		//	float				boundingradius;
		//
		//	int					nummesh;
		//	int					meshindex;
		//
		//	int					numverts;		// number of unique vertices
		//	int					vertinfoindex;	// vertex bone info
		//	int					vertindex;		// vertex vec3_t
		//	int					numnorms;		// number of unique surface normals
		//	int					norminfoindex;	// normal bone info
		//	int					normindex;		// normal vec3_t
		//
		//	int					numgroups;		// deformation groups
		//	int					groupindex;
		//} mstudiomodel_t;

		public char[] name = new char[64];
		public int type;
		//	float				boundingradius;
		public double boundingRadius;
		public int meshCount;
		public int meshOffset;

		public int vertexCount;
		public int vertexBoneInfoOffset;
		public int vertexOffset;
		public int normalCount;
		public int normalBoneInfoOffset;
		public int normalOffset;

		// Based on source code, these two fields do not seem to be used.
		public int groupCount;
		public int groupOffset;


		public string theSmdFileName;
		public string theName;
		public List<SourceMdlMesh10> theMeshes;
		public List<SourceVector> theNormals;
		//Public theNormals As List(Of SourceVectorSingle)
		public List<int> theNormalBoneInfos;
		public List<SourceVector> theVertexes;
		//Public theVertexes As List(Of SourceVectorSingle)
		public List<int> theVertexBoneInfos;

	}

}