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
	public class SourceMdlBoneController2531
	{

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudiobonecontroller_t
		//{
		//	int					bone;	// -1 == 0
		//	int					type;	// X, Y, Z, XR, YR, ZR, M
		//	float				start;
		//	float				end;
		//	int					rest;	// byte index value at rest
		//	int					inputfield;	// 0-3 user set controller, 4 mouth
		//	char				padding[32];	// future expansion.
		//};

		public int boneIndex;
		public int type;
		public double startAngleDegrees;
		public double endAngleDegrees;
		public int restIndex;
		public int inputField;
		public int[] unused = new int[32];


	}

}