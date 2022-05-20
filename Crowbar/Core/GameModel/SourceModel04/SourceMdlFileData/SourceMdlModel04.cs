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
	public class SourceMdlModel04
	{

		public double unknownSingle;
		public int vertexCount;
		public int normalCount;
		public int meshCount;

		public string theSmdFileName;
		public List<SourceMdlMesh04> theMeshes = new List<SourceMdlMesh04>();
		public List<SourceMdlNormal04> theNormals = new List<SourceMdlNormal04>();
		public List<SourceMdlVertex04> theVertexes = new List<SourceMdlVertex04>();

	}

}