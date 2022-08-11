using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBoneController32
	{

		//struct mstudiobonecontroller_t
		//{
		//	int	bone;	// -1 == 0
		//	int	type;	// X, Y, Z, XR, YR, ZR, M
		//	float	start;
		//	float	end;
		//	int	rest;	// byte index value at rest
		//	int	inputfield;	// 0-3 user set controller, 4 mouth
		//	char	padding[32];	// future expansion.
		//};

		public int boneIndex;
		public int type;
		public double startBlah;
		public double endBlah;
		public int restIndex;
		public int inputField;
		//Public unused(31) As Byte


		//#define STUDIO_X	0x00000001
		//#define STUDIO_Y	0x00000002
		//#define STUDIO_Z	0x00000004
		//#define STUDIO_XR	0x00000008
		//#define STUDIO_YR	0x00000010
		//#define STUDIO_ZR	0x00000020
		//
		//#define STUDIO_LX	0x00000040
		//#define STUDIO_LY	0x00000080
		//#define STUDIO_LZ	0x00000100
		//#define STUDIO_LXR	0x00000200
		//#define STUDIO_LYR	0x00000400
		//#define STUDIO_LZR	0x00000800
		//
		//#define STUDIO_TYPES	0x0003FFFF
		//#define STUDIO_RLOOP	0x00040000	// controller that wraps shortest distance

		public int STUDIO_X = 0x1;
		public int STUDIO_Y = 0x2;
		public int STUDIO_Z = 0x4;
		public int STUDIO_XR = 0x8;
		public int STUDIO_YR = 0x10;
		public int STUDIO_ZR = 0x20;

		public int STUDIO_LX = 0x40;
		public int STUDIO_LY = 0x80;
		public int STUDIO_LZ = 0x100;
		public int STUDIO_LXR = 0x200;
		public int STUDIO_LYR = 0x400;
		public int STUDIO_LZR = 0x800;

		public int STUDIO_TYPES = 0x3FFFF;
		public int STUDIO_RLOOP = 0x40000;

		public string TypeName
		{
			get
			{
				if ((type & STUDIO_X) > 0)
				{
					return "X";
				}
				else if ((type & STUDIO_Y) > 0)
				{
					return "Y";
				}
				else if ((type & STUDIO_Z) > 0)
				{
					return "Z";
				}
				else if ((type & STUDIO_XR) > 0)
				{
					return "XR";
				}
				else if ((type & STUDIO_YR) > 0)
				{
					return "YR";
				}
				else if ((type & STUDIO_ZR) > 0)
				{
					return "ZR";
				}
				else if ((type & STUDIO_LX) > 0)
				{
					return "LX";
				}
				else if ((type & STUDIO_LY) > 0)
				{
					return "LY";
				}
				else if ((type & STUDIO_LZ) > 0)
				{
					return "LZ";
				}
				else if ((type & STUDIO_LXR) > 0)
				{
					return "LXR";
				}
				else if ((type & STUDIO_LYR) > 0)
				{
					return "LYR";
				}
				else if ((type & STUDIO_LZR) > 0)
				{
					return "LZR";
					//ElseIf (Me.type And STUDIO_LINEAR) > 0 Then
					//	Return "LM"
					//ElseIf (Me.type And STUDIO_QUADRATIC_MOTION) > 0 Then
					//	Return "LQ"
				}

				return "";
			}
			//Set(ByVal value As String)

			//End Set
		}

	}

}