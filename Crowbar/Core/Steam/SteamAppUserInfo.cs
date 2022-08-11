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
			Create();
		}

		public SteamAppUserInfo(uint iAppID)
		{
			theAppID = iAppID;
			Create();
		}

		private void Create()
		{
			theDraftItems = new BindingListEx<WorkshopItem>();
		}

		public uint AppID
		{
			get
			{
				return theAppID;
			}
			set
			{
				theAppID = value;
			}
		}

		public BindingListEx<WorkshopItem> DraftTemplateAndChangedItems
		{
			get
			{
				return theDraftItems;
			}
			set
			{
				theDraftItems = value;
			}
		}

		private uint theAppID;
		private BindingListEx<WorkshopItem> theDraftItems;

	}

}