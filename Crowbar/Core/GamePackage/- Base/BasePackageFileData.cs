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
	public abstract class BasePackageFileData : SourceFileData
	{
		public BasePackageFileData() : base()
		{

			theEntries = new List<BasePackageDirectoryEntry>();
			theEntryDataOutputTexts = new List<string>();
		}

		public abstract bool IsSourcePackage {get;}
		public abstract string FileExtension {get;}
		public abstract string DirectoryFileNameSuffix {get;}
		public abstract string DirectoryFileNameSuffixWithExtension {get;}

		public List<BasePackageDirectoryEntry> theEntries;
		public List<string> theEntryDataOutputTexts;

	}

}