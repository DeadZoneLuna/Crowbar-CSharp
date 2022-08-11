// Base class for Source___FileData classes.

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
			theFileSeekLog = new FileSeekLog();
			theUnknownValues = new List<UnknownValue>();
		}

#endregion

#region Data

		public FileSeekLog theFileSeekLog;
		public List<UnknownValue> theUnknownValues;

#endregion

	}

}