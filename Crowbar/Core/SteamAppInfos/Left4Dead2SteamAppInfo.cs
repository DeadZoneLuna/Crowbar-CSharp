//INSTANT C# NOTE: Formerly VB project-level imports:
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
	public class Left4Dead2SteamAppInfo : SteamAppInfoBase
	{
		public Left4Dead2SteamAppInfo() : base()
		{

			this.ID = new AppId_t(550);
			this.Name = "Left 4 Dead 2";
			this.UsesSteamUGC = false;
			this.CanUseContentFolderOrFile = false;
			this.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files");
			this.TagsControlType = typeof(Left4Dead2TagsUserControl);
		}

	}

}