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
	public class BitmapInfoHeader
	{

		//FROM: c:\Program Files (x86)\Windows Kits\8.1\Include\um\wingdi.h
		//typedef struct tagBITMAPINFOHEADER{
		//        DWORD      biSize;
		//        LONG       biWidth;
		//        LONG       biHeight;
		//        WORD       biPlanes;
		//        WORD       biBitCount;
		//        DWORD      biCompression;
		//        DWORD      biSizeImage;
		//        LONG       biXPelsPerMeter;
		//        LONG       biYPelsPerMeter;
		//        DWORD      biClrUsed;
		//        DWORD      biClrImportant;
		//} BITMAPINFOHEADER, FAR *LPBITMAPINFOHEADER, *PBITMAPINFOHEADER;

		public UInt32 size;
		public Int32 width;
		public Int32 height;
		public UInt16 planes;
		public UInt16 bitCount;
		public UInt32 compression;
		public UInt32 sizeImage;
		public Int32 xPelsPerMeter;
		public Int32 yPelsPerMeter;
		public UInt32 clrUsed;
		public UInt32 clrImportant;

	}

}