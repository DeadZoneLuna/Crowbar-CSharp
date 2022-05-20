// Base class for Source___FileData classes.

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
	public class SourceFileData
	{

#region Creation and Destruction

		public SourceFileData()
		{
			this.theFileSeekLog = new FileSeekLog();
			this.theUnknownValues = new List<UnknownValue>();
		}

#endregion

#region Data

		public FileSeekLog theFileSeekLog;
		public List<UnknownValue> theUnknownValues;

#endregion

	}

}