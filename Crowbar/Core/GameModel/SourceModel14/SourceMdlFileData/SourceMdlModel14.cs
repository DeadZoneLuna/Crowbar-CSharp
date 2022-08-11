using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlModel14
	{

		public char[] name = new char[33];
		public int modelIndex;

		public int[] weightingHeaderOffsets = new int[24];

		public string theSmdFileName;
		public string theName;
		public List<SourceMdlWeightingHeader14> theWeightingHeaders;

	}

}