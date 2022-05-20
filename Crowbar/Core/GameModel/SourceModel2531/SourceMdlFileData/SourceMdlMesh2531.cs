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
	public class SourceMdlMesh2531
	{

		//FROM: VAMPTools-master\MDLConverter\inc\external\studio.h
		//struct StudioMesh
		//{
		//	int	Material;
		//
		//	int ModelIndex;
		//	StudioModel *GetModel() const; 
		//
		//	// number of unique vertices/normals/texcoords
		//	int NumVertices;
		//	// vertex mstudiovertex_t
		//	int VertexOffset;
		//
		//	// Access thin/fat mesh vertex data (only one will return a non-NULL result)
		//	const MeshVertexData *GetVertexData( void *ModelData = 0 );
		//	const ThinModelVertices *GetThinVertexData(	void *ModelData = 0 );
		//
		//	// vertex animation
		//	int	NumFlexes;
		//	int FlexIndex;
		//	StudioFlex *GetFlex( int Index ) const;
		//
		//	// - BKH - Sep 7, 2012 - Best guess at which vars aren't used?
		//	// special codes for material operations
		//	//int MaterialType;
		//	//int MaterialParam;
		//
		//	// a unique ordinal for this mesh
		//	//int MeshID;
		//
		//	//Vector Center;
		//
		//	MeshVertexData VertexData;
		//
		//	// BKH - Offsets don't match in data
		//	//int	unused[8]; // remove as appropriate
		//};

		public int materialIndex;
		public int modelOffset;

		public int vertexCount;
		public int vertexIndexStart;

		public int flexCount;
		public int flexOffset;

		//Public materialType As Integer
		//Public materialParam As Integer

		//Public id As Integer

		//Public centerX As Single
		//Public centerY As Single
		//Public centerZ As Single

		public SourceMdlMeshVertexData vertexData;

		//Public unused(7) As Integer


		public List<SourceMdlFlex2531> theFlexes;

	}

}