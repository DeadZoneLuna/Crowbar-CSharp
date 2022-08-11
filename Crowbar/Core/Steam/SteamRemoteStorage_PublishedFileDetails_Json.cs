﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SteamRemoteStorage_PublishedFileDetails_Json
	{

		public SteamRemoteStorage_PublishedFileDetails_Response response {get; set;}

		public class SteamRemoteStorage_PublishedFileDetails_Response
		{
			public int result {get; set;}
			public int resultcount {get; set;}
			public List<SteamRemoteStorage_PublishedFileDetails_ItemDetail> publishedfiledetails {get; set;}
		}

		public class SteamRemoteStorage_PublishedFileDetails_ItemDetail
		{
			public string publishedfileid {get; set;}
			public int result {get; set;}
			public string creator {get; set;}
			public int creator_app_id {get; set;}
			public int consumer_app_id {get; set;}
			public string filename {get; set;}
			public int file_size {get; set;}
			public string file_url {get; set;}
			public string hcontent_file {get; set;}
			public string preview_url {get; set;}
			public string hcontent_preview {get; set;}
			public string title {get; set;}
			public string description {get; set;}
			public int time_created {get; set;}
			public int time_updated {get; set;}
			public int visibility {get; set;}
			public int banned {get; set;}
			public string ban_reason {get; set;}
			public int subscriptions {get; set;}
			public int favorited {get; set;}
			public int lifetime_subscriptions {get; set;}
			public int lifetime_favorited {get; set;}
			public int views {get; set;}
			public object tags {get; set;}
		}

	}

}