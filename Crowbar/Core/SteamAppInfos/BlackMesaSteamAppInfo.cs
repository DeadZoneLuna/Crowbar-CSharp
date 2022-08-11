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
	public class BlackMesaSteamAppInfo : SteamAppInfoBase
	{
		public BlackMesaSteamAppInfo() : base()
		{

			ID = new AppId_t(362890);
			Name = "Black Mesa";
			UsesSteamUGC = true;
			CanUseContentFolderOrFile = false;
			//Me.ContentFileExtensionsAndDescriptions.Add("vpk", "Source Engine VPK Files")
			TagsControlType = typeof(BlackMesaTagsUserControl);
		}

	}

}