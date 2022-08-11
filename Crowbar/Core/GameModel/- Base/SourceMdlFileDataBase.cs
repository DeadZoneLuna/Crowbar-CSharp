using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlFileDataBase : SourceFileData
	{
		public SourceMdlFileDataBase() : base()
		{

			// Set to -1 so versions without fileSize field will have this value.
			fileSize = -1;

			theChecksumIsValid = true;
			theMdlFileOnlyHasAnimations = false;
			theUpAxisYCommandWasUsed = false;
		}

		public char[] id = new char[4];
		public int version;
		public int fileSize;
		public int checksum;

		public long theActualFileSize;
		public string theID;
		public string theModelName;
		public string theFileName;
		public List<string> theModifiedTexturePaths = new List<string>();
		public List<string> theModifiedTextureFileNames = new List<string>();

		public bool theChecksumIsValid;
		public bool theMdlFileOnlyHasAnimations;
		public bool theUpAxisYCommandWasUsed;

	}

}