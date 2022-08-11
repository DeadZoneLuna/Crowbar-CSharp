using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlFace04
	{

		public SourceMdlFace04()
		{
			for (int i = 0; i < vertexInfo.Length; i++)
			{
				vertexInfo[i] = new SourceMdlFaceVertexInfo04();
			}
		}

		//Public vertexIndex(11) As Integer
		//------
		public SourceMdlFaceVertexInfo04[] vertexInfo = new SourceMdlFaceVertexInfo04[3];

	}

}