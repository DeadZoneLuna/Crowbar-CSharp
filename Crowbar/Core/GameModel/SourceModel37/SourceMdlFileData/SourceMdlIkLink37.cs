using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlIkLink37
	{

		//struct mstudioiklink_t
		//{
		//	int	bone;
		//	Vector	contact;
		//	Vector	limits;
		//};

		public int boneIndex;
		public SourceVector contact = new SourceVector();
		public SourceVector limits = new SourceVector();

	}

}