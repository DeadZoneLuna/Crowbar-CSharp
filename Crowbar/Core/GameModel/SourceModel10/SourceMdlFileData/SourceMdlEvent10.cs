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
	public class SourceMdlEvent10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// events
		//typedef struct 
		//{
		//	int 				frame;
		//	int					event;
		//	int					type;
		//	char				options[64];
		//} mstudioevent_t;

		public int frameIndex;
		public int eventIndex;
		//NOTE: Based on the studiomdl.exe source code, this does not seem to be used.
		public int eventType;
		public char[] options = new char[64];



		public string theOptions;

	}

}