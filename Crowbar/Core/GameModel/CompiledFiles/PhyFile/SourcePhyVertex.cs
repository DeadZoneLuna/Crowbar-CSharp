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
	public class SourcePhyVertex
	{

		public SourcePhyVertex()
		{
			this.theNormal = new SourceVector();
			this.theNormalIsNormalized = false;
		}

		public SourceVector vertex = new SourceVector();

		public SourceVector Normal
		{
			get
			{
				if (!this.theNormalIsNormalized)
				{
					MathModule.VectorNormalize(ref this.theNormal);
					this.theNormalIsNormalized = true;
				}
				return this.theNormal;
			}
		}

		public SourceVector UnnormalizedNormal
		{
			get
			{
				return this.theNormal;
			}
		}

		private SourceVector theNormal;
		private bool theNormalIsNormalized;

	}

}