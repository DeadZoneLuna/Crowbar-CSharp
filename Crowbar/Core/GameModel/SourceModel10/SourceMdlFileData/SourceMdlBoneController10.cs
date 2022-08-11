using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBoneController10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// bone controllers
		//typedef struct 
		//{
		//	int					bone;	// -1 == 0
		//	int					type;	// X, Y, Z, XR, YR, ZR, M
		//	float				start;
		//	float				end;
		//	int					rest;	// byte index value at rest
		//	int					index;	// 0-3 user set controller, 4 mouth
		//} mstudiobonecontroller_t;


		//	int					bone;	// -1 == 0
		public int boneIndex;

		//	int					type;	// X, Y, Z, XR, YR, ZR, M
		public int type;

		//	float				start;
		//	float				end;
		public double startAngleDegrees;
		public double endAngleDegrees;

		//	int					rest;	// byte index value at rest
		public int restIndex;

		//	int					index;	// 0-3 user set controller, 4 mouth
		public int index;



	}

}