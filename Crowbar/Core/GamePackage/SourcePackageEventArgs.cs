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
	public class SourcePackageEventArgs : System.EventArgs
	{
		public SourcePackageEventArgs(BasePackageDirectoryEntry entry, string entryDataOutputText) : base()
		{

			theEntry = entry;
			theEntryDataOutputText = entryDataOutputText;
		}

		public BasePackageDirectoryEntry Entry
		{
			get
			{
				return theEntry;
			}
		}

		public string EntryDataOutputText
		{
			get
			{
				return theEntryDataOutputText;
			}
			set
			{
				theEntryDataOutputText = value;
			}
		}

		private BasePackageDirectoryEntry theEntry;
		private string theEntryDataOutputText;

	}

}