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
	public class SteamAppUserInfo
	{

		//NOTE: Only here because XMLSerializer uses it.
		public SteamAppUserInfo() : base()
		{
			this.Create();
		}

		public SteamAppUserInfo(uint iAppID)
		{
			this.theAppID = iAppID;
			this.Create();
		}

		private void Create()
		{
			this.theDraftItems = new BindingListEx<WorkshopItem>();
		}

		public uint AppID
		{
			get
			{
				return this.theAppID;
			}
			set
			{
				this.theAppID = value;
			}
		}

		public BindingListEx<WorkshopItem> DraftTemplateAndChangedItems
		{
			get
			{
				return this.theDraftItems;
			}
			set
			{
				this.theDraftItems = value;
			}
		}

		private uint theAppID;
		private BindingListEx<WorkshopItem> theDraftItems;

	}

}