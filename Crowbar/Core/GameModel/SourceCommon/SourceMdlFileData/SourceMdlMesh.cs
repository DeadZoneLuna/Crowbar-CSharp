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
	public class SourceMdlMesh
	{

		//struct mstudiomesh_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					material;

		//	int					modelindex;
		//	mstudiomodel_t *pModel() const; 

		//	int					numvertices;		// number of unique vertices/normals/texcoords
		//	int					vertexoffset;		// vertex mstudiovertex_t

		//	// Access thin/fat mesh vertex data (only one will return a non-NULL result)
		//	const mstudio_meshvertexdata_t	*GetVertexData(		void *pModelData = NULL );
		//	const thinModelVertices_t		*GetThinVertexData(	void *pModelData = NULL );

		//	int					numflexes;			// vertex animation
		//	int					flexindex;
		//	inline mstudioflex_t *pFlex( int i ) const { return (mstudioflex_t *)(((byte *)this) + flexindex) + i; };

		//	// special codes for material operations
		//	int					materialtype;
		//	int					materialparam;

		//	// a unique ordinal for this mesh
		//	int					meshid;

		//	Vector				center;

		//	mstudio_meshvertexdata_t vertexdata;

		//	int					unused[8]; // remove as appropriate

		//	mstudiomesh_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudiomesh_t(const mstudiomesh_t& vOther);
		//};

		//	int					material;
		public int materialIndex;

		//	int					modelindex;
		public int modelOffset;

		//	int					numvertices;		// number of unique vertices/normals/texcoords
		public int vertexCount;
		//	int					vertexoffset;		// vertex mstudiovertex_t
		public int vertexIndexStart;

		//	int					numflexes;			// vertex animation
		public int flexCount;
		//	int					flexindex;
		public int flexOffset;

		//	int					materialtype;
		public int materialType;
		//	int					materialparam;
		public int materialParam;

		//	int					meshid;
		public int id;

		//	Vector				center;
		public float centerX;
		public float centerY;
		public float centerZ;

		//	mstudio_meshvertexdata_t vertexdata;
		public SourceMdlMeshVertexData vertexData;

		//	int					unused[8]; // remove as appropriate
		public int[] unused = new int[8];


		public List<SourceMdlFlex> theFlexes;
		//Public theVertexIds As List(Of Byte)
		//Public theVertexes As List(Of StudioVertex)

	}

}