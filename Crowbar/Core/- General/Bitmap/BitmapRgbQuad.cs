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
	public class BitmapRgbQuad
	{

		//FROM: c:\Program Files (x86)\Windows Kits\8.1\Include\um\wingdi.h
		//typedef struct tagRGBQUAD {
		//        BYTE    rgbBlue;
		//        BYTE    rgbGreen;
		//        BYTE    rgbRed;
		//        BYTE    rgbReserved;
		//} RGBQUAD;

		public byte rbgBlue;
		public byte rgbGreen;
		public byte rgbRed;
		public byte rgbReserved;

	}

}