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
	public class SourceMdlModel31
	{

		public SourceMdlModel31()
		{
			//MyBase.New()

			this.theSmdFileNames = new List<string>(SourceConstants.MAX_NUM_LODS);
			for (int i = 0; i < SourceConstants.MAX_NUM_LODS; i++)
			{
				this.theSmdFileNames.Add("");
			}
		}

		public char[] name = new char[64];
		public int type;
		public double boundingRadius;
		public int meshCount;
		public int meshOffset;

		public int vertexCount;
		public int vertexOffset;

		// MDL 27 to 30 
		public int normalOffset_MDL27to30;

		public int tangentOffset;

		// MDL 27 to 30 
		public int texCoordOffset_MDL27to30;
		public int boneWeightsOffset_MDL27to30;

		public int attachmentCount;
		public int attachmentOffset;
		public int eyeballCount;
		public int eyeballOffset;

		//Public unused(7) As Integer

		public List<string> theSmdFileNames;
		//Public theEyeballs As List(Of SourceMdlEyeball37)
		public List<SourceMdlMesh31> theMeshes;
		public string theName;
		public List<SourceVector4D> theTangents;
		public List<SourceMdlVertex31> theVertexes;

	}

}