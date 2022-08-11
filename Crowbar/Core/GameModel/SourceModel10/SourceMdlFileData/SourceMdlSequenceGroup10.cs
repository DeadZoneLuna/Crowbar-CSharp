using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlSequenceGroup10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//#ifndef ZONE_H
		//typedef void *cache_user_t;
		//#endif
		//
		//// demand loaded sequence groups
		//typedef struct
		//{
		//	char				label[32];	// textual name
		//	char				name[64];	// file name
		//	cache_user_t		cache;		// cache index pointer
		//	int					data;		// hack for group 0
		//} mstudioseqgroup_t;

		public char[] name = new char[32];
		public char[] fileName = new char[64];

		//NOTE: Based on the studiomdl.exe source code, these fields do not seem to be used.
		public int cacheOffset;
		public int data;


		public string theName;
		public string theFileName;

	}

}