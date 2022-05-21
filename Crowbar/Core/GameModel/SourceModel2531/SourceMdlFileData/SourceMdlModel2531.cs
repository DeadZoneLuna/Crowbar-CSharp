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
	public class SourceMdlModel2531
	{

		public SourceMdlModel2531()
		{
			//MyBase.New()

			theSmdFileNames = new List<string>(SourceConstants.MAX_NUM_LODS);
			for (int i = 0; i < SourceConstants.MAX_NUM_LODS; i++)
			{
				theSmdFileNames.Add("");
			}
		}

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudiomodel_t
		//{
		//	char				name[128];	// f64: ~
		//
		//	int					type;
		//
		//	float				boundingradius;
		//
		//	int					nummeshes;	
		//	int					meshindex;
		//	inline mstudiomesh_t *pMesh( int i ) const { return (mstudiomesh_t *)(((byte *)this) + meshindex) + i; };
		//
		//	// cache purposes
		//	int					numvertices;		// number of unique vertices/normals/texcoords
		//	int					vertexindex;		// vertex Vector
		//	int					tangentsindex;		// tangents Vector
		//
		//	int					filetype;	// f64: for type vertex
		//
		//	Vector				*Position( int i ) const;
		//	Vector				*Normal( int i ) const;
		//	Vector4D			*TangentS( int i ) const;
		//	Vector2D			*Texcoord( int i ) const;
		//	mstudioboneweight_t *BoneWeights( int i ) const;
		//	mstudiovertex_t		*Vertex( int i ) const;
		//
		//// f64: add new structs..
		//	mstudiovertex0_t	*pVertex0( int i ) const;	// behar: +
		//	mstudiovertex1_t	*pVertex1( int i ) const;
		//	mstudiovertex2_t	*pVertex2( int i ) const;
		//
		//	float				unkvect[6];
		//
		//	int					unk[2];
		//
		//	int					unknum;
		//	int					unkindex;
		//
		//	int					hz[2];
		//// ----
		//	int					numattachments;
		//	int					attachmentindex;
		//
		//	int					numeyeballs;
		//	int					eyeballindex;
		//	inline  mstudioeyeball_t *pEyeball( int i ) { return (mstudioeyeball_t *)(((byte *)this) + eyeballindex) + i; };
		//
		////	int					unused[8];		// remove as appropriate	// f64: -
		//};

		public char[] name = new char[128];
		public int type;
		public double boundingRadius;

		public int meshCount;
		public int meshOffset;
		public int vertexCount;
		public int vertexOffset;
		public int tangentOffset;

		public int vertexListType;

		public double[] unknown01 = new double[3];
		//Public unknownCount As Integer
		//Public unknownOffset As Integer

		public double[] unknown02 = new double[3];
		//Public unknown03(5) As Integer

		public int attachmentCount;
		public int attachmentOffset;
		public int eyeballCount;
		public int eyeballOffset;
		public int[] unknown03 = new int[2];

		public int unknown01Count;
		public int unknown01Offset;
		public int unknown02Count;
		public int unknown02Offset;


		public List<string> theSmdFileNames;
		public string theName;
		public List<SourceMdlEyeball2531> theEyeballs;
		public List<SourceMdlMesh2531> theMeshes;
		public List<SourceMdlTangent2531> theTangents;
		public List<SourceMdlType0Vertex2531> theVertexesType0;
		public List<SourceMdlType1Vertex2531> theVertexesType1;
		public List<SourceMdlType2Vertex2531> theVertexesType2;


		//FROM: VAMPTools-master from atrblizzard\VAMPTools-master\MDLConverter\inc\external\studio.h
		// For vertexListType.
		//enum
		//{
		//	VLIST_TYPE_SKINNED = 0,
		//	VLIST_TYPE_UNSKINNED = 1,
		//	VLIST_TYPE_COMPRESSED = 2,
		//};
		public const int VLIST_TYPE_SKINNED = 0x0;
		public const int VLIST_TYPE_UNSKINNED = 0x1;
		public const int VLIST_TYPE_COMPRESSED = 0x2;

	}

}