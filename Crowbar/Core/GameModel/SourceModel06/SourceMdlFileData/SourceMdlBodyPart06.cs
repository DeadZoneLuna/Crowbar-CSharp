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
	public class SourceMdlBodyPart06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	char				name[64];
		//	int					nummodels;
		//	int					base;
		//	int					modelindex; // index into models array (->mstudiomodel_t)
		//} mstudiobodyparts_t;

		public char[] name = new char[64];
		public int modelCount;
		public int @base;
		public int modelOffset;

		public string theName;
		public List<SourceMdlModel06> theModels;

	}

}