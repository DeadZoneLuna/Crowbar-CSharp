using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBoneController06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	int					bone;			// -1 == 0 (TOMAS??)
		//	int					type;			// X, Y, Z, XR, YR, ZR, M
		//	float				start;
		//	float				end;
		//} mstudiobonecontroller_t;

		public int boneIndex;
		public int type;
		public double startAngleDegrees;
		public double endAngleDegrees;

	}

}