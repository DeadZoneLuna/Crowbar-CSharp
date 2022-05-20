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
	public class SourceMdlModelVertexData
	{

		//struct mstudio_modelvertexdata_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	Vector				*Position( int i ) const;
		//	Vector				*Normal( int i ) const;
		//	Vector4D			*TangentS( int i ) const;
		//	Vector2D			*Texcoord( int i ) const;
		//	mstudioboneweight_t	*BoneWeights( int i ) const;
		//	mstudiovertex_t		*Vertex( int i ) const;
		//	bool				HasTangentData( void ) const;
		//	int					GetGlobalVertexIndex( int i ) const;
		//	int					GetGlobalTangentIndex( int i ) const;

		//	// base of external vertex data stores
		//	const void			*pVertexData;
		//	const void			*pTangentData;
		//};

		//	const void			*pVertexData;
		public int vertexDataP;
		//	const void			*pTangentData;
		public int tangentDataP;

	}

}