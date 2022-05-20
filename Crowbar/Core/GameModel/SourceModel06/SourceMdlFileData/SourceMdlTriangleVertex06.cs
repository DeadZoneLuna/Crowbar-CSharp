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
	public class SourceMdlTriangleVertex06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	short				vertindex;		// index into vertex array (relative)
		//	short				normindex;		// index into normal array (relative)
		//	short				s, t;			// s,t position on skin
		//} mstudiotrivert_t;

		public UInt16 vertexIndex;
		public UInt16 normalIndex;
		public Int16 s;
		public Int16 t;

	}

}