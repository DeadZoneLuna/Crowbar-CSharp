using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBoneRotation06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	short 				frame;			// frame id (frame <= numframes)
		//	short 				angle[3];		// (values: +/-18000; 18000 = 180deg)
		//} mstudiobonerot_t;

		public short frameIndex;
		public short[] angle = new short[3];

	}

}