using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;
using Steamworks;

namespace Crowbar
{
	public class ZombiePanicSourceSteamAppInfo : SteamAppInfoBase
	{
		public ZombiePanicSourceSteamAppInfo() : base()
		{

			ID = new AppId_t(17500);
			Name = "Zombie Panic! Source";
			UsesSteamUGC = true;
			CanUseContentFolderOrFile = false;
			//Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
			TagsControlType = typeof(ZombiePanicSourceTagsUserControl);
		}

		public enum ZombiePanicSourceTypeTags
		{
			[Description("GameMode")]
			GameMode,
			[Description("Custom Models")]
			CustomModels,
			[Description("Custom Sounds")]
			CustomSounds,
			[Description("Miscellaneous")]
			Miscellaneous
		}

	}

}