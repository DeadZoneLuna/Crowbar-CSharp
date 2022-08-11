using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBonePosition06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	int					frame;			// frame id (frame <= numframes)
		//	vec3_t				pos;
		//} mstudiobonepos_t;

		public int frameIndex;
		public SourceVector position = new SourceVector();

	}

}