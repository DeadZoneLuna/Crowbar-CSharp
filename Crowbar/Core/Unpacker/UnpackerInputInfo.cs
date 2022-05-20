//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace Crowbar
{
	public class UnpackerInputInfo
	{

		public AppEnums.ArchiveAction theArchiveAction;
		//Public theArchiveEntryIndexes As List(Of Integer)
		//Public theEntries As List(Of VpkDirectoryEntry)
		public SortedList<string, List<int>> theArchivePathFileNameToEntryIndexesMap;
		public string theGamePath;
		public bool theOutputPathIsExtendedWithPackageName;
		public string theSelectedRelativeOutputPath;

	}

}