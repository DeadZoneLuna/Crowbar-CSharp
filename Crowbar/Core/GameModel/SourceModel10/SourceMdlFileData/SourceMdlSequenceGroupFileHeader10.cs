using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlSequenceGroupFileHeader10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// header for demand loaded sequence group data
		//typedef struct 
		//{
		//	int					id;
		//	int					version;
		//
		//	char				name[64];
		//	int					length;
		//} studioseqhdr_t;

		public char[] id = new char[4];
		public int version;
		public char[] name = new char[64];
		public int fileSize;

		public long theActualFileSize;

	}

}