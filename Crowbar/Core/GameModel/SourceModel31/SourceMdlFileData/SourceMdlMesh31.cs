using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlMesh31
	{

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

		// MDL 27
		public int[] unused_MDL27 = new int[12];
		//------
		// MDL 28 to 31
		public int[] unused = new int[5];

		//Public theFlexes As List(Of SourceMdlFlex37)

	}

}