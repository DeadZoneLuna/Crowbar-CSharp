using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlEvent37
	{

		//struct mstudioevent_t
		//{
		//	float	cycle;
		//	int	event;
		//	int	type;
		//	char	options[64];
		//};

		public double cycle;
		public int eventIndex;
		//NOTE: Does not seem to be used, even though it takes up space in the MDL file.
		public int eventType;
		public char[] options = new char[64];

	}

}