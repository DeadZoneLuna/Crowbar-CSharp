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
	public class SourceMdlTexture06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	char				name[64];
		//	int					flags;
		//	int					width;
		//	int					height;
		//	int					index;
		//} mstudiotexture_t;

		public char[] fileName = new char[64];
		public int flags;
		public UInt32 width;
		public UInt32 height;
		public UInt32 dataOffset;


		public string theFileName;
		public List<byte> theData;

	}

}