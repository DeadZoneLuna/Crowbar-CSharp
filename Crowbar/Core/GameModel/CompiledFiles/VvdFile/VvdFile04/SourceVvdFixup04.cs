using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVvdFixup04
	{

		//FROM: public/studio.h
		//// apply sequentially to lod sorted vertex and tangent pools to re-establish mesh order
		//struct vertexFileFixup_t
		//{
		//	int		lod;				// used to skip culled root lod
		//	int		sourceVertexID;		// absolute index from start of vertex/tangent blocks
		//	int		numVertexes;
		//};

		public int lodIndex;
		public int vertexIndex;
		public int vertexCount;

	}

}