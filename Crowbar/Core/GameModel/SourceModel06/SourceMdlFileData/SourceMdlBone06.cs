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
	public class SourceMdlBone06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	char				name[32];		// bone name for symbolic links
		//	int		 			parent;			// parent bone
		//	int		 			unused[6];
		//} mstudiobone_t;

		public char[] name = new char[32];
		public int parentBoneIndex;
		//Public unused(5) As Integer
		public SourceVector position = new SourceVector();
		public SourceVector rotation = new SourceVector();


		public string theName;

	}

}