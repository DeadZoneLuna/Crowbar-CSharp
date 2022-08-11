using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlMesh37
	{

		//struct mstudiomesh_t
		//{
		//	int		material;
		//
		//	int		modelindex;
		//	mstudiomodel_t *pModel() const; // { return (mstudiomodel_t *)(((byte *)this) + modelindex); }
		//
		//	int		numvertices;		// number of unique vertices/normals/texcoords
		//	int		vertexoffset;		// vertex mstudiovertex_t
		//
		//	Vector		*Position( int i ) const;
		//	Vector		*Normal( int i ) const;
		//	Vector4D	*TangentS( int i ) const;
		//	Vector2D	*Texcoord( int i ) const;
		//	mstudioboneweight_t 	*BoneWeights( int i ) const;
		//	mstudiovertex_t	 	*Vertex( int i ) const;
		//
		//	int		numflexes;			// vertex animation
		//	int		flexindex;
		//	inline mstudioflex_t *pFlex( int i ) const { return (mstudioflex_t *)(((byte *)this) + flexindex) + i; };
		//
		//	//int		numresolutionupdates;
		//	//int		resolutionupdateindex;
		//
		//	//int		numfaceupdates;
		//	//int		faceupdateindex;
		//
		//	// special codes for material operations
		//	int		materialtype;
		//	int		materialparam;
		//
		//	// a unique ordinal for this mesh
		//	int		meshid;
		//
		//	Vector		center;
		//
		//	int		unused[5]; // remove as appropriate
		//};

		public int materialIndex;
		public int modelOffset;

		public int vertexCount;
		public int vertexIndexStart;
		public int flexCount;
		public int flexOffset;
		public int materialType;
		public int materialParam;

		public int id;
		public SourceVector center = new SourceVector();
		public int[] unused = new int[5];

		public List<SourceMdlFlex37> theFlexes;

	}

}