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
	public class IntermediateExpression
	{

		public IntermediateExpression(string iExpression, int iPrecedence)
		{
			this.theExpression = iExpression;
			this.thePrecedence = iPrecedence;
		}

		public string theExpression;
		public int thePrecedence;

	}

}