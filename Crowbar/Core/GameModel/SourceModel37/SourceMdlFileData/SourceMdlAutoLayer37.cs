using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlAutoLayer37
	{

		//struct mstudioautolayer_t
		//{
		//	int	iSequence;
		//	int	flags;
		//	float	start;	// beginning of influence
		//	float	peak;	// start of full influence
		//	float	tail;	// end of full influence
		//	float	end;	// end of all influence
		//};

		public int sequenceIndex;
		public int flags;
		public double influenceStart;
		public double influencePeak;
		public double influenceTail;
		public double influenceEnd;

	}

}