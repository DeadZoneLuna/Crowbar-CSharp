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

namespace Crowbar
{
	public class BackgroundSteamPipe
	{

		public BackgroundSteamPipe() : base()
		{

			theActiveSteamPipes = new List<SteamPipe>();
			theActiveBackgroundWorkers = new List<BackgroundWorkerEx>();
		}

		public void Kill()
		{
			//NOTE: Prevent handlers from doing stuff because some of them might access widgets that are disposed.
			for (int i = theActiveBackgroundWorkers.Count - 1; i >= 0; i--)
			{
				BackgroundWorkerEx aBackgroundWorker = theActiveBackgroundWorkers[i];
				aBackgroundWorker.Kill();
				theActiveBackgroundWorkers.Remove(aBackgroundWorker);
			}
			for (int i = theActiveSteamPipes.Count - 1; i >= 0; i--)
			{
				SteamPipe aSteamPipe = theActiveSteamPipes[i];
				aSteamPipe.Kill();
				theActiveSteamPipes.Remove(aSteamPipe);
			}
		}

#region Download Item

		public class DownloadItemInputInfo
		{
			public uint AppID;
			public string PublishedItemID;
			public string TargetPath;
		}

		public class DownloadItemOutputInfo
		{
			public string Result;
			public string SteamAgreementStatus;
			public uint AppID;
			public string PublishedItemID;
			public string ItemUpdated_Text;
			public string ItemTitle;
			public byte[] ContentFile;
			public string ContentFolderOrFileName;
			public long BytesReceived;
			public long TotalBytesToReceive;
		}

		public void DownloadItem(ProgressChangedEventHandler given_ProgressChanged, RunWorkerCompletedEventHandler given_RunWorkerCompleted, DownloadItemInputInfo inputInfo)
		{
			theDownloadItemBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theDownloadItemBackgroundWorker, DownloadItem_DoWork, given_ProgressChanged, given_RunWorkerCompleted, inputInfo);
		}

		//NOTE: This is run in a background thread.
		private void DownloadItem_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			BackgroundWorkerEx bw = (BackgroundWorkerEx)sender;
			theActiveBackgroundWorkers.Add(bw);
			DownloadItemInputInfo inputInfo = (DownloadItemInputInfo)e.Argument;
			BackgroundSteamPipe.DownloadItemOutputInfo outputInfo = new BackgroundSteamPipe.DownloadItemOutputInfo();

			if (inputInfo.AppID == 0)
			{
				//NOTE: Use appID for "Source SDK" when item's appID is unknown.
				MainCROWBAR.TheApp.WriteSteamAppIdFile(211);

				SteamPipe steamPipeToGetAppID = new SteamPipe();
				theActiveSteamPipes.Add(steamPipeToGetAppID);
				string resultOfGetAppID = steamPipeToGetAppID.Open("GetItemAppID", bw, "Getting item app ID");
				if (resultOfGetAppID != "success")
				{
					outputInfo.Result = "error";
					e.Result = outputInfo;
					theActiveSteamPipes.Remove(steamPipeToGetAppID);
					theActiveBackgroundWorkers.Remove(bw);
					return;
				}

				if (bw.CancellationPending)
				{
					steamPipeToGetAppID.Kill();
					e.Cancel = true;
					theActiveSteamPipes.Remove(steamPipeToGetAppID);
					theActiveBackgroundWorkers.Remove(bw);
					return;
				}

				string appID_text = "";
				WorkshopItem publishedItem = steamPipeToGetAppID.SteamRemoteStorage_GetPublishedFileDetails(inputInfo.PublishedItemID, inputInfo.AppID.ToString(), ref appID_text);

				if (bw.CancellationPending)
				{
					steamPipeToGetAppID.Kill();
					e.Cancel = true;
					theActiveSteamPipes.Remove(steamPipeToGetAppID);
					theActiveBackgroundWorkers.Remove(bw);
					return;
				}

				steamPipeToGetAppID.Shut();
				theActiveSteamPipes.Remove(steamPipeToGetAppID);

				if (!string.IsNullOrEmpty(appID_text))
				{
					MainCROWBAR.TheApp.WriteSteamAppIdFile(uint.Parse(appID_text));
				}
				else
				{
					//NOTE: Error message is stored in publishedItem.Title.
					bw.ReportProgress(0, "ERROR: " + publishedItem.Title + "\r\n");
					outputInfo.Result = "error";
					e.Result = outputInfo;
					theActiveBackgroundWorkers.Remove(bw);
					return;
				}
			}
			else
			{
				MainCROWBAR.TheApp.WriteSteamAppIdFile(inputInfo.AppID);
			}
			outputInfo.AppID = inputInfo.AppID;

			SteamPipe steamPipe = new SteamPipe();
			theActiveSteamPipes.Add(steamPipe);
			string result = steamPipe.Open("DownloadItem", bw, "Downloading item");
			if (result != "success")
			{
				outputInfo.Result = "error";
				e.Result = outputInfo;
				theActiveSteamPipes.Remove(steamPipe);
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			outputInfo.PublishedItemID = inputInfo.PublishedItemID;
			outputInfo.ContentFolderOrFileName = "";
			byte[] contentFile = {0};
			string returned_AppID_Text = "0";
			result = steamPipe.Crowbar_DownloadContentFolderOrFile(inputInfo.PublishedItemID, inputInfo.TargetPath, ref contentFile, ref outputInfo.ItemUpdated_Text, ref outputInfo.ItemTitle, ref outputInfo.ContentFolderOrFileName, ref returned_AppID_Text);
			if (result == "success")
			{
				outputInfo.ContentFile = contentFile;
			}
			else if (result == "success_SteamUGC")
			{
				outputInfo.AppID = uint.Parse(returned_AppID_Text);
			}
			else
			{
				bw.ReportProgress(0, "ERROR: Unable to download the content file name from Steam." + "\r\n");
			}
			outputInfo.Result = result;

			steamPipe.Shut();
			theActiveSteamPipes.Remove(steamPipe);
			theActiveBackgroundWorkers.Remove(bw);

			e.Result = outputInfo;
		}

		public void UnsubscribeItem(ProgressChangedEventHandler given_ProgressChanged, RunWorkerCompletedEventHandler given_RunWorkerCompleted, DownloadItemInputInfo inputInfo)
		{
			theUnsubscribeItemBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theUnsubscribeItemBackgroundWorker, UnsubscribeItem_DoWork, given_ProgressChanged, given_RunWorkerCompleted, inputInfo);
		}

		//NOTE: This is run in a background thread.
		private void UnsubscribeItem_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			BackgroundWorkerEx bw = (BackgroundWorkerEx)sender;
			theActiveBackgroundWorkers.Add(bw);
			DownloadItemInputInfo inputInfo = (DownloadItemInputInfo)e.Argument;
			BackgroundSteamPipe.DownloadItemOutputInfo outputInfo = new BackgroundSteamPipe.DownloadItemOutputInfo();

			MainCROWBAR.TheApp.WriteSteamAppIdFile(inputInfo.AppID);

			SteamPipe steamPipe = new SteamPipe();
			theActiveSteamPipes.Add(steamPipe);
			string result = steamPipe.Open("UnsubscribeItem", bw, "Unsubscribing from item");
			if (result != "success")
			{
				outputInfo.Result = "error";
				e.Result = outputInfo;
				theActiveSteamPipes.Remove(steamPipe);
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			result = steamPipe.SteamUGC_UnsubscribeItem(inputInfo.PublishedItemID);
			if (result != "success")
			{
				bw.ReportProgress(0, "ERROR: Unable to download the content file name from Steam." + "\r\n");
			}
			outputInfo.Result = result;

			steamPipe.Shut();
			theActiveSteamPipes.Remove(steamPipe);
			theActiveBackgroundWorkers.Remove(bw);

			e.Result = outputInfo;
		}

		private BackgroundWorkerEx theDownloadItemBackgroundWorker;
		private BackgroundWorkerEx theUnsubscribeItemBackgroundWorker;

#endregion

#region GetPublishedItems

		public void GetPublishedItems(ProgressChangedEventHandler given_ProgressChanged, RunWorkerCompletedEventHandler given_RunWorkerCompleted, string appID_text)
		{
			theGetPublishedItemsBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theGetPublishedItemsBackgroundWorker, GetPublishedItems_DoWork, given_ProgressChanged, given_RunWorkerCompleted, appID_text);
		}

		//NOTE: This is run in a background thread.
		private void GetPublishedItems_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			BackgroundWorkerEx bw = (BackgroundWorkerEx)sender;
			theActiveBackgroundWorkers.Add(bw);
			string appID_text = (e.Argument == null ? null : Convert.ToString(e.Argument));

			SteamPipe steamPipe = new SteamPipe();
			theActiveSteamPipes.Add(steamPipe);
			string result = steamPipe.Open("GetPublishedItems", bw, "Getting list of published items");
			if (result != "success")
			{
				e.Cancel = true;
				theActiveSteamPipes.Remove(steamPipe);
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			string resultOfSendRequest = null;
			uint pageNumber = 1U;
			while (pageNumber > 0)
			{
				string result_SteamUGC_CreateQueryUserUGCRequest = steamPipe.SteamUGC_CreateQueryUserUGCRequest(appID_text, pageNumber);
				if (result_SteamUGC_CreateQueryUserUGCRequest != "success")
				{
					bw.ReportProgress(0, "ERROR: " + result_SteamUGC_CreateQueryUserUGCRequest + "\r\n");
					break;
				}

				if (bw.CancellationPending)
				{
					steamPipe.Kill();
					e.Cancel = true;
					theActiveSteamPipes.Remove(steamPipe);
					theActiveBackgroundWorkers.Remove(bw);
					return;
				}

				resultOfSendRequest = steamPipe.SteamUGC_SendQueryUGCRequest();
				if (resultOfSendRequest == "success")
				{
					pageNumber += 1U;
				}
				else
				{
					pageNumber = 0;
				}

				if (bw.CancellationPending)
				{
					steamPipe.Kill();
					e.Cancel = true;
					theActiveSteamPipes.Remove(steamPipe);
					theActiveBackgroundWorkers.Remove(bw);
					return;
				}
			}

			steamPipe.Shut();
			theActiveSteamPipes.Remove(steamPipe);
			theActiveBackgroundWorkers.Remove(bw);
		}

		private BackgroundWorkerEx theGetPublishedItemsBackgroundWorker;

#endregion

#region GetPublishedItemDetails

		public class GetPublishedFileDetailsInputInfo
		{
			public string ItemID_text;
			public string AppID_text;
			public string Action;

			public GetPublishedFileDetailsInputInfo(string iItemID_text, string iAppID_text, string action)
			{
				ItemID_text = iItemID_text;
				AppID_text = iAppID_text;
				Action = action;
			}
		}

		public class GetPublishedFileDetailsOutputInfo
		{
			public WorkshopItem PublishedItem;
			public string Action;

			public GetPublishedFileDetailsOutputInfo(WorkshopItem publishedItem, string action)
			{
				PublishedItem = publishedItem;
				Action = action;
			}
		}

		public void GetPublishedItemDetails(ProgressChangedEventHandler given_ProgressChanged, RunWorkerCompletedEventHandler given_RunWorkerCompleted, GetPublishedFileDetailsInputInfo input)
		{
			theGetPublishedItemDetailsBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theGetPublishedItemDetailsBackgroundWorker, GetPublishedItemDetails_DoWork, given_ProgressChanged, given_RunWorkerCompleted, input);
		}

		//NOTE: This is run in a background thread.
		private void GetPublishedItemDetails_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			BackgroundWorkerEx bw = (BackgroundWorkerEx)sender;
			theActiveBackgroundWorkers.Add(bw);
			GetPublishedFileDetailsInputInfo input = (GetPublishedFileDetailsInputInfo)e.Argument;

			if (theGetItemDetailsSteamPipe != null)
			{
				theGetItemDetailsSteamPipe.Kill();
			}
			theGetItemDetailsSteamPipe = new SteamPipe();
			theActiveSteamPipes.Add(theGetItemDetailsSteamPipe);

			string result = theGetItemDetailsSteamPipe.Open("GetItemDetails", bw, "Getting item details");
			if (result != "success")
			{
				e.Cancel = true;
				theActiveSteamPipes.Remove(theGetItemDetailsSteamPipe);
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			if (bw.CancellationPending)
			{
				theGetItemDetailsSteamPipe.Kill();
				e.Cancel = true;
				theActiveSteamPipes.Remove(theGetItemDetailsSteamPipe);
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			string appID_text = "";
			WorkshopItem publishedItem = theGetItemDetailsSteamPipe.SteamRemoteStorage_GetPublishedFileDetails(input.ItemID_text, input.AppID_text, ref appID_text);

			if (bw.CancellationPending)
			{
				theGetItemDetailsSteamPipe.Kill();
				e.Cancel = true;
				theActiveSteamPipes.Remove(theGetItemDetailsSteamPipe);
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			if (publishedItem.ID != "0")
			{
				string tempVar = publishedItem.PreviewImagePathFileName;
				result = theGetItemDetailsSteamPipe.Crowbar_DownloadPreviewFile(ref tempVar);
					publishedItem.PreviewImagePathFileName = tempVar;
			}
			else
			{
				//NOTE: Error message is stored in publishedItem.Title.
				bw.ReportProgress(0, "ERROR: " + publishedItem.Title + "\r\n");
				bw.ReportProgress(1, null);
			}

			theGetItemDetailsSteamPipe.Shut();
			theActiveSteamPipes.Remove(theGetItemDetailsSteamPipe);
			theGetItemDetailsSteamPipe = null;

			if (bw.CancellationPending)
			{
				e.Cancel = true;
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			theActiveBackgroundWorkers.Remove(bw);
			GetPublishedFileDetailsOutputInfo output = new GetPublishedFileDetailsOutputInfo(publishedItem, input.Action);
			e.Result = output;
		}

		private BackgroundWorkerEx theGetPublishedItemDetailsBackgroundWorker;
		private SteamPipe theGetItemDetailsSteamPipe;

#endregion

#region DeletePublishedItemFromWorkshop

		public void DeletePublishedItemFromWorkshop(ProgressChangedEventHandler given_ProgressChanged, RunWorkerCompletedEventHandler given_RunWorkerCompleted, string itemID_text)
		{
			theDeletePublishedItemFromWorkshopBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theDeletePublishedItemFromWorkshopBackgroundWorker, DeletePublishedItemFromWorkshop_DoWork, given_ProgressChanged, given_RunWorkerCompleted, itemID_text);
		}

		//NOTE: This is run in a background thread.
		private void DeletePublishedItemFromWorkshop_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			BackgroundWorkerEx bw = (BackgroundWorkerEx)sender;
			theActiveBackgroundWorkers.Add(bw);
			string itemID_text = (e.Argument == null ? null : Convert.ToString(e.Argument));

			SteamPipe steamPipe = new SteamPipe();
			theActiveSteamPipes.Add(steamPipe);
			string result = steamPipe.Open("DeleteItem", bw, "Deleting item");
			if (result != "success")
			{
				e.Cancel = true;
				theActiveSteamPipes.Remove(steamPipe);
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			result = "failed";
			if (MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex].UsesSteamUGC)
			{
				result = steamPipe.SteamUGC_DeleteItem(itemID_text);
			}
			else
			{
				result = steamPipe.SteamRemoteStorage_DeletePublishedFile(itemID_text);
			}

			steamPipe.Shut();
			theActiveSteamPipes.Remove(steamPipe);
			theActiveBackgroundWorkers.Remove(bw);

			e.Result = result;
		}

		private BackgroundWorkerEx theDeletePublishedItemFromWorkshopBackgroundWorker;

#endregion

#region PublishItem

		public class PublishItemInputInfo
		{
			public SteamAppInfoBase AppInfo;
			public WorkshopItem Item;
		}

		public class PublishItemProgressInfo
		{
			public string Status;
			public ulong UploadedByteCount;
			public ulong TotalUploadedByteCount;
		}

		public class PublishItemOutputInfo
		{
			public string Result;
			public string SteamAgreementStatus;
			public string PublishedItemID;
			public ulong PublishedItemOwnerID;
			public string PublishedItemOwnerName;
			public long PublishedItemPosted;
			public long PublishedItemUpdated;
		}

		public void PublishItem(ProgressChangedEventHandler given_ProgressChanged, RunWorkerCompletedEventHandler given_RunWorkerCompleted, PublishItemInputInfo inputInfo)
		{
			thePublishItemBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(thePublishItemBackgroundWorker, PublishItem_DoWork, given_ProgressChanged, given_RunWorkerCompleted, inputInfo);
		}

		//NOTE: This is run in a background thread.
		private void PublishItem_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			BackgroundWorkerEx bw = (BackgroundWorkerEx)sender;
			theActiveBackgroundWorkers.Add(bw);
			PublishItemInputInfo inputInfo = (PublishItemInputInfo)e.Argument;

			//TODO: Process content folder or file before accessing Steam.
			string processedContentPathFolderOrFileName = "";
			if (inputInfo.Item.ContentPathFolderOrFileNameIsChanged && !string.IsNullOrEmpty(inputInfo.Item.ContentPathFolderOrFileName))
			{
				bool processedFileCheckIsSuccessful = true;
				try
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Processing content for upload." + "\r\n");
					processedContentPathFolderOrFileName = inputInfo.AppInfo.ProcessFileBeforeUpload(inputInfo.Item, bw);

					if (inputInfo.AppInfo.CanUseContentFolderOrFile)
					{
						if (!Directory.Exists(processedContentPathFolderOrFileName) && !File.Exists(processedContentPathFolderOrFileName))
						{
							processedFileCheckIsSuccessful = false;
						}
					}
					else if (inputInfo.AppInfo.UsesSteamUGC)
					{
						if (!Directory.Exists(processedContentPathFolderOrFileName))
						{
							processedFileCheckIsSuccessful = false;
						}
					}
					else
					{
						if (!File.Exists(processedContentPathFolderOrFileName))
						{
							processedFileCheckIsSuccessful = false;
						}
					}
				}
				catch (Exception ex)
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: " + ex.Message + "\r\n");
					processedFileCheckIsSuccessful = false;
				}

				if (!processedFileCheckIsSuccessful)
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Processing content failed. Review log messages above for reason." + "\r\n");
					e.Cancel = true;
					theActiveBackgroundWorkers.Remove(bw);
					return;
				}
			}

			if (inputInfo.Item.CreatorAppID == "0")
			{
				MainCROWBAR.TheApp.WriteSteamAppIdFile(inputInfo.AppInfo.ID.m_AppId);
			}
			else
			{
				MainCROWBAR.TheApp.WriteSteamAppIdFile(inputInfo.Item.CreatorAppID);
			}

			SteamPipe steamPipe = new SteamPipe();
			theActiveSteamPipes.Add(steamPipe);
			string result = steamPipe.Open("PublishItem", bw, "Publishing item");
			if (result != "success")
			{
				e.Cancel = true;
				theActiveSteamPipes.Remove(steamPipe);
				theActiveBackgroundWorkers.Remove(bw);
				return;
			}

			BackgroundSteamPipe.PublishItemOutputInfo outputInfo = null;
			if (inputInfo.AppInfo.UsesSteamUGC)
			{
				outputInfo = PublishViaSteamUGC(steamPipe, inputInfo, processedContentPathFolderOrFileName);
			}
			else
			{
				outputInfo = PublishViaRemoteStorage(steamPipe, inputInfo, processedContentPathFolderOrFileName);
			}

			if (outputInfo.PublishedItemID != "0")
			{
				string appID_text = "";
				WorkshopItem publishedItem = steamPipe.SteamRemoteStorage_GetPublishedFileDetails(outputInfo.PublishedItemID, inputInfo.AppInfo.ID.ToString(), ref appID_text);
				outputInfo.PublishedItemOwnerID = publishedItem.OwnerID;
				outputInfo.PublishedItemOwnerName = publishedItem.OwnerName;
				outputInfo.PublishedItemPosted = publishedItem.Posted;
				outputInfo.PublishedItemUpdated = publishedItem.Updated;
			}

			steamPipe.Shut();
			theActiveSteamPipes.Remove(steamPipe);
			theActiveBackgroundWorkers.Remove(bw);

			e.Result = outputInfo;
		}

		//NOTE: This is run in a background thread.
		private BackgroundSteamPipe.PublishItemOutputInfo PublishViaSteamUGC(SteamPipe steamPipe, PublishItemInputInfo inputInfo, string processedContentPathFolderOrFileName)
		{
			string changeNote = null;
			string appID_text = inputInfo.AppInfo.ID.ToString();
			string itemID_text = "0";

			BackgroundSteamPipe.PublishItemOutputInfo outputInfo = new BackgroundSteamPipe.PublishItemOutputInfo();
			outputInfo.PublishedItemID = "0";
			outputInfo.SteamAgreementStatus = "Accepted";

			if (inputInfo.Item.IsDraft)
			{
				string returnedPublishedItemID = "";
				string resultOfCreateItem = steamPipe.SteamUGC_CreateItem(appID_text, ref returnedPublishedItemID);

				if (resultOfCreateItem == "success")
				{
					itemID_text = returnedPublishedItemID;
					outputInfo.PublishedItemID = returnedPublishedItemID;
				}
				else if (resultOfCreateItem == "success_agreement")
				{
					thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForCreate + "\r\n");
					itemID_text = returnedPublishedItemID;
					outputInfo.PublishedItemID = returnedPublishedItemID;
					outputInfo.SteamAgreementStatus = "NotAccepted";
				}
				else
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Unable to create workshop item. Steam error message: " + resultOfCreateItem + "\r\n");
					outputInfo.Result = "Failed";
					//Return outputInfo
				}

				changeNote = "";
			}
			else
			{
				itemID_text = inputInfo.Item.ID;
				changeNote = inputInfo.Item.ChangeNote;
			}

			if (outputInfo.Result != "Failed")
			{
				string result = "";

				result = StartItemUpdate(steamPipe, appID_text, itemID_text);
				if (result != "success")
				{
					outputInfo.Result = "Failed";
					//Return outputInfo
				}
				else
				{
					result = UpdateNonContentFileOptions(steamPipe, inputInfo);
					if (result != "success")
					{
						outputInfo.Result = "Failed";
						//Return outputInfo
					}
					else
					{
						if (inputInfo.Item.ContentPathFolderOrFileNameIsChanged && !string.IsNullOrEmpty(inputInfo.Item.ContentPathFolderOrFileName))
						{
							if (outputInfo.Result != "Failed")
							{
								if (Directory.Exists(processedContentPathFolderOrFileName) || (inputInfo.AppInfo.CanUseContentFolderOrFile && File.Exists(processedContentPathFolderOrFileName)))
								{
									string setItemContentWasSuccessful = steamPipe.SteamUGC_SetItemContent(processedContentPathFolderOrFileName);
									if (setItemContentWasSuccessful == "success")
									{
										thePublishItemBackgroundWorker.ReportProgress(0, "Set item content completed." + "\r\n");
									}
									else
									{
										thePublishItemBackgroundWorker.ReportProgress(0, "Set item content failed." + "\r\n");
										outputInfo.Result = "Failed";
										//Return outputInfo
									}
								}
							}
						}

						if (outputInfo.Result != "Failed")
						{
							if (inputInfo.Item.IsDraft)
							{
								thePublishItemBackgroundWorker.ReportProgress(0, "Publishing new item." + "\r\n");
							}
							else
							{
								thePublishItemBackgroundWorker.ReportProgress(0, "Publishing the update." + "\r\n");
							}

							result = SubmitItemUpdate(steamPipe, changeNote);
							if (result == "success")
							{
								thePublishItemBackgroundWorker.ReportProgress(0, "Publishing succeeded." + "\r\n");
								outputInfo.Result = "Succeeded";
								if (outputInfo.PublishedItemID == "0")
								{
									outputInfo.PublishedItemID = inputInfo.Item.ID;
								}
							}
							else if (result == "success_agreement")
							{
								if (outputInfo.PublishedItemID == "0")
								{
									thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForUpdate + "\r\n");
									outputInfo.PublishedItemID = inputInfo.Item.ID;
								}
								else if (outputInfo.SteamAgreementStatus == "Accepted")
								{
									thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForCreate + "\r\n");
								}
								outputInfo.SteamAgreementStatus = "NotAccepted";
								outputInfo.Result = "Succeeded";
								//Return outputInfo
							}
							else
							{
								outputInfo.Result = "Failed";
							}
						}

						if (inputInfo.Item.ContentPathFolderOrFileNameIsChanged && !string.IsNullOrEmpty(inputInfo.Item.ContentPathFolderOrFileName))
						{
							inputInfo.AppInfo.CleanUpAfterUpload(thePublishItemBackgroundWorker);
						}
					}
				}
			}

			return outputInfo;
		}

		//NOTE: This is run in a background thread.
		private BackgroundSteamPipe.PublishItemOutputInfo PublishViaRemoteStorage(SteamPipe steamPipe, PublishItemInputInfo inputInfo, string processedContentPathFolderOrFileName)
		{
			BackgroundSteamPipe.PublishItemOutputInfo outputInfo = null;

			if (inputInfo.Item.IsDraft)
			{
				outputInfo = CreateViaRemoteStorage(steamPipe, inputInfo, processedContentPathFolderOrFileName);
			}
			else
			{
				outputInfo = UpdateViaRemoteStorage(steamPipe, inputInfo, processedContentPathFolderOrFileName);
			}

			return outputInfo;
		}

		//NOTE: SteamRemoteStorage_PublishWorkshopFile requires Item to have a Title, a Description, a Content File, and a Preview Image.
		//NOTE: This is run in a background thread.
		private BackgroundSteamPipe.PublishItemOutputInfo CreateViaRemoteStorage(SteamPipe steamPipe, PublishItemInputInfo inputInfo, string processedContentPathFolderOrFileName)
		{
			BackgroundSteamPipe.PublishItemOutputInfo outputInfo = new BackgroundSteamPipe.PublishItemOutputInfo();
			outputInfo.PublishedItemID = "0";
			outputInfo.SteamAgreementStatus = "Accepted";

			string previewFileName = Path.GetFileName(inputInfo.Item.PreviewImagePathFileName);
			string resultForPreview_SteamRemoteStorage_FileWrite = steamPipe.SteamRemoteStorage_FileWrite(inputInfo.Item.PreviewImagePathFileName, previewFileName);
			if (resultForPreview_SteamRemoteStorage_FileWrite != "success")
			{
				thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: " + resultForPreview_SteamRemoteStorage_FileWrite + "\r\n");
				outputInfo.Result = "Failed";
				//Return outputInfo
			}

			if (outputInfo.Result != "Failed")
			{
				if (outputInfo.Result != "Failed" && File.Exists(processedContentPathFolderOrFileName))
				{
					string fileName = Path.GetFileName(processedContentPathFolderOrFileName);
					string resultForContent_SteamRemoteStorage_FileWrite = steamPipe.SteamRemoteStorage_FileWrite(processedContentPathFolderOrFileName, fileName);
					if (resultForContent_SteamRemoteStorage_FileWrite != "success")
					{
						thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: " + resultForContent_SteamRemoteStorage_FileWrite + "\r\n");
						outputInfo.Result = "Failed";
						//Return outputInfo
					}
					else
					{
						string appID_text = inputInfo.AppInfo.ID.ToString();

						thePublishItemBackgroundWorker.ReportProgress(0, "Publishing new item." + "\r\n");
						string returnedPublishedItemID = "";
						string resultOfCreateItem = steamPipe.SteamRemoteStorage_PublishWorkshopFile(fileName, previewFileName, appID_text, inputInfo.Item.Title, inputInfo.Item.Description, inputInfo.Item.VisibilityText, inputInfo.Item.Tags, ref returnedPublishedItemID);

						if (resultOfCreateItem == "success")
						{
							thePublishItemBackgroundWorker.ReportProgress(0, "Publishing succeeded." + "\r\n");
							outputInfo.PublishedItemID = returnedPublishedItemID;
							outputInfo.Result = "Succeeded";
						}
						else if (resultOfCreateItem == "success_agreement")
						{
							thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForCreate + "\r\n");
							outputInfo.Result = "Succeeded";
							outputInfo.PublishedItemID = returnedPublishedItemID;
							outputInfo.SteamAgreementStatus = "NotAccepted";
						}
						else
						{
							thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Unable to publish workshop item. Steam error message: " + resultOfCreateItem + "\r\n");
							outputInfo.Result = "Failed";
						}
					}
				}

				inputInfo.AppInfo.CleanUpAfterUpload(thePublishItemBackgroundWorker);
			}

			return outputInfo;
		}

		//NOTE: This is run in a background thread.
		private BackgroundSteamPipe.PublishItemOutputInfo UpdateViaRemoteStorage(SteamPipe steamPipe, PublishItemInputInfo inputInfo, string processedContentPathFolderOrFileName)
		{
			BackgroundSteamPipe.PublishItemOutputInfo outputInfo = new BackgroundSteamPipe.PublishItemOutputInfo();
			outputInfo.PublishedItemID = "0";
			outputInfo.SteamAgreementStatus = "Accepted";

			string changeNote = inputInfo.Item.ChangeNote;

			string result = "";

			thePublishItemBackgroundWorker.ReportProgress(0, "Publishing non-content parts of update." + "\r\n");
			result = StartItemUpdate(steamPipe, inputInfo.AppInfo.ID.ToString(), inputInfo.Item.ID);
			if (result != "success")
			{
				outputInfo.Result = "Failed";
				//Return outputInfo
			}
			else
			{
				result = UpdateNonContentFileOptions(steamPipe, inputInfo);
				if (result != "success")
				{
					outputInfo.Result = "Failed";
					//Return outputInfo
				}
				else
				{
					//NOTE: The changeNote will not be changed via this SteamUGC function call because updated item is in SteamRemoteStorage.
					result = SubmitItemUpdate(steamPipe, changeNote);
					if (result == "success")
					{
						thePublishItemBackgroundWorker.ReportProgress(0, "Publishing non-content parts of update succeeded." + "\r\n");
						outputInfo.PublishedItemID = inputInfo.Item.ID;
						outputInfo.Result = "Succeeded";
					}
					else if (result == "success_agreement")
					{
						thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForUpdate + "\r\n");
						outputInfo.PublishedItemID = inputInfo.Item.ID;
						outputInfo.SteamAgreementStatus = "NotAccepted";
						outputInfo.Result = "Succeeded";
						//Return outputInfo
					}
					else
					{
						outputInfo.Result = "Failed";
					}

					if (outputInfo.Result != "Failed" && inputInfo.Item.ContentPathFolderOrFileNameIsChanged && !string.IsNullOrEmpty(inputInfo.Item.ContentPathFolderOrFileName))
					{
						//Delete old content file.
						//NOTE: This deletion does not seem to be needed, so do not bother user with any messages related to 
						//If result_Crowbar_DeleteContentFile <> "success" Then
						//	Me.LogTextBox.AppendText("WARNING: " + result_Crowbar_DeleteContentFile + vbCrLf)
						//End If
						string result_Crowbar_DeleteContentFile_BeforeWrite = steamPipe.Crowbar_DeleteContentFile(inputInfo.Item.ID);

						if (outputInfo.Result != "Failed" && File.Exists(processedContentPathFolderOrFileName))
						{
							// Write/upload content file to RemoteStorage (Steam Cloud).
							string fileName = Path.GetFileName(processedContentPathFolderOrFileName);
							string result_SteamRemoteStorage_FileWrite = steamPipe.SteamRemoteStorage_FileWrite(processedContentPathFolderOrFileName, fileName);
							if (result_SteamRemoteStorage_FileWrite != "success")
							{
								//TODO: This error seems to occur when content file is bigger than available space for app's Steam Cloud.
								thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Not enough space on this game's Steam Cloud for content file." + result_SteamRemoteStorage_FileWrite + "\r\n");
								outputInfo.Result = "Failed";
								//Return outputInfo
							}
							else
							{
								string result_SteamRemoteStorage_CreatePublishedFileUpdateRequest = steamPipe.SteamRemoteStorage_CreatePublishedFileUpdateRequest(inputInfo.Item.ID);
								if (result_SteamRemoteStorage_CreatePublishedFileUpdateRequest != "success")
								{
									thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: " + result_SteamRemoteStorage_CreatePublishedFileUpdateRequest + "\r\n");
									outputInfo.Result = "Failed";
									//Return outputInfo
								}
								else
								{
									string result_SteamRemoteStorage_UpdatePublishedFileFile = steamPipe.SteamRemoteStorage_UpdatePublishedFileFile(fileName);
									if (result_SteamRemoteStorage_UpdatePublishedFileFile != "success")
									{
										thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Update of content file failed." + "\r\n");
										outputInfo.Result = "Failed";
										//Return outputInfo
									}
									else
									{
										string updateCompletedLogText = "Update of content file";

										string result_SteamRemoteStorage_UpdatePublishedFileSetChangeDescription = steamPipe.SteamRemoteStorage_UpdatePublishedFileSetChangeDescription(changeNote);
										if (result_SteamRemoteStorage_UpdatePublishedFileSetChangeDescription != "success")
										{
											thePublishItemBackgroundWorker.ReportProgress(0, "WARNING: Update of change note failed." + "\r\n");
										}
										else
										{
											updateCompletedLogText += " and change note";
										}

										// Copy content file from RemoteStorage (Steam Cloud) to SteamUGC storage. The copy might actually occur in an earlier function call.
										thePublishItemBackgroundWorker.ReportProgress(0, "Publishing the content part of update." + "\r\n");
										string result_SteamRemoteStorage_CommitPublishedFileUpdate = steamPipe.SteamRemoteStorage_CommitPublishedFileUpdate();
										if (result_SteamRemoteStorage_CommitPublishedFileUpdate == "success")
										{
											//Me.thePublishItemBackgroundWorker.ReportProgress(0, updateCompletedLogText + " completed." + vbCrLf)
											thePublishItemBackgroundWorker.ReportProgress(0, "Publishing the content part of update succeeded." + "\r\n");
											outputInfo.PublishedItemID = inputInfo.Item.ID;
											outputInfo.Result = "Succeeded";
										}
										else if (result_SteamRemoteStorage_CommitPublishedFileUpdate == "success_agreement")
										{
											thePublishItemBackgroundWorker.ReportProgress(0, SteamPipe.AgreementMessageForUpdate + "\r\n");
											outputInfo.PublishedItemID = inputInfo.Item.ID;
											outputInfo.Result = "FailedContentAndChangeNote";
											outputInfo.SteamAgreementStatus = "NotAccepted";
											outputInfo.Result = "Succeeded";
											//Return outputInfo
										}
										else
										{
											string result_SteamRemoteStorage_FileDelete = steamPipe.SteamRemoteStorage_FileDelete(fileName);
											thePublishItemBackgroundWorker.ReportProgress(0, result_SteamRemoteStorage_CommitPublishedFileUpdate + "\r\n");
											outputInfo.Result = "FailedContentAndChangeNote";
											//Return outputInfo
										}
									}
								}

								// Delete content file from RemoteStorage (Steam Cloud). No need to bother user with any messages related to 
								string result_Crowbar_DeleteContentFile = steamPipe.Crowbar_DeleteContentFile(inputInfo.Item.ID);
							}
						}
					}
				}
			}

			inputInfo.AppInfo.CleanUpAfterUpload(thePublishItemBackgroundWorker);

			return outputInfo;
		}

		//NOTE: The SteamUGC API can be used to update all but the content-file and change note options for both RemoteStorage API and SteamUGC API.
		//NOTE: This is run in a background thread.
		private string UpdateNonContentFileOptions(SteamPipe steamPipe, PublishItemInputInfo inputInfo)
		{
			string result = "success";

			if (inputInfo.Item.TitleIsChanged && !string.IsNullOrEmpty(inputInfo.Item.Title))
			{
				string setItemTitleWasSuccessful = steamPipe.SteamUGC_SetItemTitle(inputInfo.Item.Title);
				if (setItemTitleWasSuccessful == "success")
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item title completed." + "\r\n");
				}
				else
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item title failed." + "\r\n");
					return "error";
				}
			}

			if (inputInfo.Item.DescriptionIsChanged && !string.IsNullOrEmpty(inputInfo.Item.Description))
			{
				string setItemDescriptionWasSuccessful = steamPipe.SteamUGC_SetItemDescription(inputInfo.Item.Description);
				if (setItemDescriptionWasSuccessful == "success")
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item description completed." + "\r\n");
				}
				else
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item description failed." + "\r\n");
					return "error";
				}
			}

			if (inputInfo.Item.PreviewImagePathFileNameIsChanged && !string.IsNullOrEmpty(inputInfo.Item.PreviewImagePathFileName))
			{
				string setItemPreviewWasSuccessful = steamPipe.SteamUGC_SetItemPreview(inputInfo.Item.PreviewImagePathFileName);
				if (setItemPreviewWasSuccessful == "success")
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item preview completed." + "\r\n");
				}
				else
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item preview failed." + "\r\n");
					return "error";
				}
			}

			if (inputInfo.Item.VisibilityIsChanged)
			{
				string visibility_text = inputInfo.Item.VisibilityText;
				string setItemVisibilityWasSuccessful = steamPipe.SteamUGC_SetItemVisibility(visibility_text);
				if (setItemVisibilityWasSuccessful == "success")
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item visibility completed." + "\r\n");
				}
				else
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item visibility failed." + "\r\n");
					return "error";
				}
			}

			if (inputInfo.Item.TagsIsChanged)
			{
				string setItemTagsWasSuccessful = steamPipe.SteamUGC_SetItemTags(inputInfo.Item.Tags);
				if (setItemTagsWasSuccessful == "success")
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item tags completed." + "\r\n");
				}
				else
				{
					thePublishItemBackgroundWorker.ReportProgress(0, "Set item tags failed." + "\r\n");
					return "error";
				}
			}

			return result;
		}

		//NOTE: This is run in a background thread.
		private string StartItemUpdate(SteamPipe steamPipe, string appID_Text, string itemID_text)
		{
			string result = steamPipe.SteamUGC_StartItemUpdate(appID_Text, itemID_text);
			if (result != "success")
			{
				thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Unable to start the update of item." + "\r\n");
			}
			return result;
		}

		//NOTE: This is run in a background thread.
		private string SubmitItemUpdate(SteamPipe steamPipe, string changeNote)
		{
			string result = steamPipe.SteamUGC_SubmitItemUpdate(changeNote);
			if (!result.StartsWith("success"))
			{
				thePublishItemBackgroundWorker.ReportProgress(0, "ERROR: Unable to submit the update of item. Steam error: " + result + "\r\n");
			}
			return result;
		}

		private BackgroundWorkerEx thePublishItemBackgroundWorker;

#endregion

#region Private Methods

#endregion

#region Data

		private List<SteamPipe> theActiveSteamPipes;
		private List<BackgroundWorkerEx> theActiveBackgroundWorkers;

#endregion

	}

}