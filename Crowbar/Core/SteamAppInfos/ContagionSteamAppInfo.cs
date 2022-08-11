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
	public class ContagionSteamAppInfo : SteamAppInfoBase
	{
		public ContagionSteamAppInfo() : base()
		{

			ID = new AppId_t(238430);
			Name = "Contagion";
			UsesSteamUGC = true;
			CanUseContentFolderOrFile = true;
			ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files");
			TagsControlType = typeof(ContagionTagsUserControl);
		}

	}

}