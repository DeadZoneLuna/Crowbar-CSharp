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
	public class SourceMdlMeshVertexData
	{

		//struct mstudio_meshvertexdata_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	Vector				*Position( int i ) const;
		//	Vector				*Normal( int i ) const;
		//	Vector4D			*TangentS( int i ) const;
		//	Vector2D			*Texcoord( int i ) const;
		//	mstudioboneweight_t *BoneWeights( int i ) const;
		//	mstudiovertex_t		*Vertex( int i ) const;
		//	bool				HasTangentData( void ) const;
		//	int					GetModelVertexIndex( int i ) const;
		//	int					GetGlobalVertexIndex( int i ) const;

		//	// indirection to this mesh's model's vertex data
		//	const mstudio_modelvertexdata_t	*modelvertexdata;

		//	// used for fixup calcs when culling top level lods
		//	// expected number of mesh verts at desired lod
		//	int					numLODVertexes[MAX_NUM_LODS];
		//};

		//	const mstudio_modelvertexdata_t	*modelvertexdata;
		public int modelVertexDataP;

		//	int					numLODVertexes[MAX_NUM_LODS];
		//  MAX_NUM_LODS = 8
		public int[] lodVertexCount = new int[SourceConstants.MAX_NUM_LODS];

	}

}