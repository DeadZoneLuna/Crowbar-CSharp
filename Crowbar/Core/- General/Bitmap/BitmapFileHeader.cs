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
	public class BitmapFileHeader
	{

		//FROM: c:\Program Files (x86)\Windows Kits\8.1\Include\um\wingdi.h
		//typedef struct tagBITMAPFILEHEADER {
		//        WORD    bfType;
		//        DWORD   bfSize;
		//        WORD    bfReserved1;
		//        WORD    bfReserved2;
		//        DWORD   bfOffBits;
		//} BITMAPFILEHEADER, FAR *LPBITMAPFILEHEADER, *PBITMAPFILEHEADER;

		public UInt16 type;
		public UInt32 size;
		public UInt16 reserved1;
		public UInt16 reserved2;
		public UInt32 dataOffset;

	}

}