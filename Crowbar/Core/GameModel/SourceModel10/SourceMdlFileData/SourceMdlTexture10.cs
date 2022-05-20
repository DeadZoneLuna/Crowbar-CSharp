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
	public class SourceMdlTexture10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// skin info
		//typedef struct
		//{
		//	char					name[64];
		//	int						flags;
		//	int						width;
		//	int						height;
		//	int						index;
		//} mstudiotexture_t;

		public char[] fileName = new char[64];
		public int flags;
		public UInt32 width;
		public UInt32 height;
		public UInt32 dataOffset;


		public string theFileName;
		public List<byte> theData;


		//// lighting options
		//#define STUDIO_NF_FLATSHADE		0x0001
		//#define STUDIO_NF_CHROME		0x0002
		//#define STUDIO_NF_FULLBRIGHT	0x0004
		//#define STUDIO_NF_NOMIPS        0x0008
		//#define STUDIO_NF_ALPHA         0x0010
		//#define STUDIO_NF_ADDITIVE      0x0020
		//#define STUDIO_NF_MASKED        0x0040

		public const int STUDIO_NF_FLATSHADE = 0x1;
		public const int STUDIO_NF_CHROME = 0x2;
		public const int STUDIO_NF_FULLBRIGHT = 0x4;
		public const int STUDIO_NF_NOMIPS = 0x8;
		public const int STUDIO_NF_ALPHA = 0x10;
		public const int STUDIO_NF_ADDITIVE = 0x20;
		public const int STUDIO_NF_MASKED = 0x40;

	}

}