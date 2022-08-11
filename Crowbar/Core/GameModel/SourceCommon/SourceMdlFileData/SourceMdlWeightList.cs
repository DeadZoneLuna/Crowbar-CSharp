using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlWeightList
	{

		public SourceMdlWeightList()
		{
			theWeights = new List<double>();
		}

		public string theName;
		public List<double> theWeights;

	}

}