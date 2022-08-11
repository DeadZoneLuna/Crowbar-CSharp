using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class BasePackageDirectoryEntry
	{

		public BasePackageDirectoryEntry() : base()
		{

			// Write this value to avoid using multi-chunk stuff.
			archiveIndex = 0x7FFF;
		}

		public UInt32 crc;
		public UInt16 archiveIndex;

		// The text that should be shown in user interface, for example, in Garry's Mod, the meta data as a file (without quotes): "<addon.json>"
		//    This field should start with a "<" to signify that theRealPathFileName has the real text. 
		public string thePathFileName;
		// The text that should be used for an actual file name, for example (without quotes): "addon.json"
		public string theRealPathFileName;

	}

}