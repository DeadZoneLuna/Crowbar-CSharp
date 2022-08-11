using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlVertex31
	{

		public SourceMdlVertex31()
		{
			boneWeight = new SourceMdlBoneWeight31();
			position = new SourceVector();
			normal = new SourceVector();
		}

		public SourceMdlBoneWeight31 boneWeight;
		public SourceVector position;
		public SourceVector normal;
		public double texCoordX;
		public double texCoordY;

	}

}