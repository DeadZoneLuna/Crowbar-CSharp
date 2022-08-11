using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlModel37
	{

		public SourceMdlModel37()
		{
			//MyBase.New()

			theSmdFileNames = new List<string>(SourceConstants.MAX_NUM_LODS);
			for (int i = 0; i < SourceConstants.MAX_NUM_LODS; i++)
			{
				theSmdFileNames.Add("");
			}
		}

		//struct mstudiomodel_t
		//{
		//	char		name[64];
		//
		//	int		type;
		//
		//	float		boundingradius;
		//
		//	int		nummeshes;
		//	int		meshindex;
		//	inline mstudiomesh_t *pMesh( int i ) const { return (mstudiomesh_t *)(((byte *)this) + meshindex) + i; };
		//
		//	// cache purposes
		//	int		numvertices;		// number of unique vertices/normals/texcoords
		//	int		vertexindex;		// vertex Vector
		//	int		tangentsindex;		// tangents Vector
		//
		//	Vector		*Position( int i ) const;
		//	Vector		*Normal( int i ) const;
		//	Vector4D	*TangentS( int i ) const;
		//	Vector2D	*Texcoord( int i ) const;
		//	mstudioboneweight_t 	*BoneWeights( int i ) const;
		//	mstudiovertex_t		*Vertex( int i ) const;
		//
		//	int		numattachments;
		//	int		attachmentindex;
		//
		//	int		numeyeballs;
		//	int		eyeballindex;
		//	inline  mstudioeyeball_t *pEyeball( int i ) { return (mstudioeyeball_t *)(((byte *)this) + eyeballindex) + i; };
		//
		//	int		unused[8];		// remove as appropriate
		//};

		public char[] name = new char[64];
		public int type;
		public double boundingRadius;
		public int meshCount;
		public int meshOffset;

		public int vertexCount;
		public int vertexOffset;
		public int tangentOffset;

		public int attachmentCount;
		public int attachmentOffset;
		public int eyeballCount;
		public int eyeballOffset;

		public int[] unused = new int[8];

		public List<string> theSmdFileNames;
		public List<SourceMdlEyeball37> theEyeballs;
		public List<SourceMdlMesh37> theMeshes;
		public string theName;
		public List<SourceVector4D> theTangents;
		public List<SourceMdlVertex37> theVertexes;

	}

}