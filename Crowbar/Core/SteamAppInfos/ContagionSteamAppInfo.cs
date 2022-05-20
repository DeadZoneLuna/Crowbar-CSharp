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
	public class ContagionSteamAppInfo : SteamAppInfoBase
	{
		public ContagionSteamAppInfo() : base()
		{

			this.ID = new AppId_t(238430);
			this.Name = "Contagion";
			this.UsesSteamUGC = true;
			this.CanUseContentFolderOrFile = true;
			this.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files");
			this.TagsControlType = typeof(ContagionTagsUserControl);
		}

	}

}