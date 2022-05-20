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
	public class AnarchyArcadeSteamAppInfo : SteamAppInfoBase
	{
		public AnarchyArcadeSteamAppInfo() : base()
		{

			this.ID = new AppId_t(266430);
			this.Name = "Anarchy Arcade";
			this.UsesSteamUGC = true;
			this.CanUseContentFolderOrFile = false;
			//Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
			this.TagsControlType = typeof(AnarchyArcadeTagsUserControl);
		}

	}

}