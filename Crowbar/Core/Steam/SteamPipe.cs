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
using System.IO.Pipes;

namespace Crowbar
{
	public class SteamPipe
	{

#region Create and Destroy

#endregion

#region Init and Free

#endregion

#region Properties

#endregion

#region Methods

		//NOTE: Start a process that does the steamworks stuff, so that when that process ends, 
		//      Steam will stop showing the game as status and the AppID can be changed.
		//NOTE: Use Named Pipes to send info between the processes.
		// Parameters:
		//      reasontext - A short phrase such as "Publishing item" or "Getting item details" to be used in opening and closing connection messages.
		public string Open(string pipeNameSuffix, BackgroundWorker bw, string reasonText)
		{
			this.theBackgroundWorker = bw;
			this.theReasonText = reasonText;

			if (this.theBackgroundWorker != null)
			{
				string logMessage = this.theReasonText + " - opening connection to Steam Workshop." + "\r\n";
				this.theBackgroundWorker.ReportProgress(0, logMessage);
			}

			this.theCrowbarSteamPipeProcess = new Process();
			try
			{
				this.theCrowbarSteamPipeProcess.StartInfo.UseShellExecute = false;
				this.theCrowbarSteamPipeProcess.StartInfo.FileName = App.CrowbarSteamPipeFileName;
				this.theCrowbarSteamPipeProcess.StartInfo.Arguments = pipeNameSuffix;
#if DEBUG
				this.theCrowbarSteamPipeProcess.StartInfo.CreateNoWindow = false;
#else
				this.theCrowbarSteamPipeProcess.StartInfo.CreateNoWindow = true;
#endif
				this.theCrowbarSteamPipeProcess.Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
			}
			finally
			{
			}

			this.theCrowbarSteamPipeServer = new NamedPipeServerStream("CrowbarSteamPipe" + pipeNameSuffix, PipeDirection.InOut, 1);
			Console.WriteLine("Waiting for client to connect to pipe ...");
			this.theCrowbarSteamPipeServer.WaitForConnection();
			Console.WriteLine("... Client connected to pipe.");

			this.theStreamWriter = new StreamWriter(this.theCrowbarSteamPipeServer);
			this.theStreamWriter.AutoFlush = true;
			this.theStreamReader = new StreamReader(this.theCrowbarSteamPipeServer);
			try
			{
				//If Me.theBackgroundWorker IsNot Nothing Then
				//	Dim logMessage As String = "Connecting to Steam Workshop." + vbCrLf
				//	Me.theBackgroundWorker.ReportProgress(0, logMessage)
				//End If
				this.theStreamWriter.WriteLine("Init");
				Console.WriteLine("Command: Init");

				string result = this.theStreamReader.ReadLine();
				Console.WriteLine("Result: " + result);
				if (this.theBackgroundWorker != null)
				{
					if (result != "success")
					{
						string logMessage;
						//NOTE: Reaches this code when user tries to download a workshop item of a game user does not own.
						//      Can not determine if user owns game here because can not Init Steam.
						//Me.theStreamWriter.WriteLine("SteamApps_BIsSubscribedApp")
						//Dim resultOfBIsSubscribedApp As String = Me.theStreamReader.ReadLine()
						//If resultOfBIsSubscribedApp = "success" Then
						//	logMessage = "Connection to Steam Workshop failed. This most likely means you need to login to Steam." + vbCrLf
						//Else
						//	logMessage = "Connection to Steam Workshop failed because you do not own the app or game to which this item belongs." + vbCrLf
						//End If
						logMessage = "Connection to Steam Workshop failed. This most likely means you need to login to Steam or you do not own the app or game to which this item belongs." + "\r\n";
						this.theBackgroundWorker.ReportProgress(0, logMessage);
						if (this.theCrowbarSteamPipeServer != null)
						{
							Console.WriteLine("Closing pipe due to error.");
							this.theCrowbarSteamPipeServer.Close();
							this.theCrowbarSteamPipeServer = null;
						}
						return "error";
						//Else
						//	Dim logMessage As String = "Connection to Steam Workshop initialized." + vbCrLf
						//	Me.theBackgroundWorker.ReportProgress(0, logMessage)
					}
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				if (this.theCrowbarSteamPipeServer != null)
				{
					Console.WriteLine("Closing pipe due to error.");
					this.theCrowbarSteamPipeServer.Close();
					this.theCrowbarSteamPipeServer = null;
				}
			}
			finally
			{
			}

			return "success";
		}

		public void Shut()
		{
			try
			{
				this.theStreamWriter.WriteLine("Free");
				Console.WriteLine("Command: Free");
				if (this.theBackgroundWorker != null)
				{
					string logMessage = this.theReasonText + " finished - closing connection to Steam Workshop." + "\r\n";
					this.theBackgroundWorker.ReportProgress(0, logMessage);
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				if (this.theCrowbarSteamPipeServer != null)
				{
					Console.WriteLine("Closing pipe due to error.");
					this.theCrowbarSteamPipeServer.Close();
					this.theCrowbarSteamPipeServer = null;
				}
			}
			finally
			{
#if DEBUG
				//NOTE: This 'If DEBUG Then' block allows the CrowbarSteamPipe console window to stay open, if it is set to do that. 
				if (this.theCrowbarSteamPipeServer != null)
				{
					Console.WriteLine("Closing pipe.");
					this.theCrowbarSteamPipeServer.Close();
					this.theCrowbarSteamPipeServer = null;
				}
				if (this.theCrowbarSteamPipeProcess != null)
				{
					this.theCrowbarSteamPipeProcess.Close();
					this.theCrowbarSteamPipeProcess = null;
				}
#else
				this.Kill();
#endif
			}
		}

		public void Kill()
		{
			if (this.theCrowbarSteamPipeServer != null)
			{
				Console.WriteLine("Closing pipe.");
				this.theCrowbarSteamPipeServer.Close();
				this.theCrowbarSteamPipeServer = null;
			}
			if (this.theCrowbarSteamPipeProcess != null)
			{
				try
				{
					if (!this.theCrowbarSteamPipeProcess.HasExited && !this.theCrowbarSteamPipeProcess.CloseMainWindow())
					{
						Console.WriteLine("Killing pipe process.");
						this.theCrowbarSteamPipeProcess.Kill();
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
				finally
				{
					if (this.theCrowbarSteamPipeProcess != null)
					{
						this.theCrowbarSteamPipeProcess.Close();
						this.theCrowbarSteamPipeProcess = null;
					}
				}
			}
		}

#region Crowbar

		public string Crowbar_DeleteContentFile(string itemID_text)
		{
			this.theStreamWriter.WriteLine("Crowbar_DeleteContentFile");
			this.theStreamWriter.WriteLine(itemID_text);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string Crowbar_DownloadContentFolderOrFile(string itemID_text, string targetPath, ref byte[] contentFileBytes, ref string itemUpdated_Text, ref string itemTitle, ref string contentFolderOrFileName, ref string appID_Text)
		{
			this.theStreamWriter.WriteLine("Crowbar_DownloadContentFolderOrFile");
			this.theStreamWriter.WriteLine(itemID_text);
			this.theStreamWriter.WriteLine(targetPath);

			string result = this.theStreamReader.ReadLine();
			if (result == "success")
			{
				itemUpdated_Text = this.theStreamReader.ReadLine();
				itemTitle = this.ReadMultipleLinesOfText(this.theStreamReader);
				contentFolderOrFileName = this.theStreamReader.ReadLine();
				int byteCount = int.Parse(this.theStreamReader.ReadLine());
				if (byteCount > 0)
				{
					int batchByteCount = 1024;
					byte[] batchData = new byte[batchByteCount];
					contentFileBytes = new byte[byteCount];
					int byteOffset = 0;
					int bytesRemaining = byteCount;
					int length = 0;
					try
					{
						BackgroundSteamPipe.DownloadItemOutputInfo outputInfo = new BackgroundSteamPipe.DownloadItemOutputInfo();
						//Me.theStreamReader.BaseStream.Read(data, 0, data.Length)
						while (bytesRemaining > 0)
						{
							//bw.ReportProgress(0, "Read" + vbCrLf)
							//length = Me.theStreamReader.BaseStream.Read(batchData, byteOffset, batchByteCount)
							length = this.theStreamReader.BaseStream.Read(batchData, 0, batchByteCount);
							//bw.ReportProgress(0, "CopyTo: " + contentFileBytes.Length.ToString() + " offset = " + byteOffset.ToString() + vbCrLf)
							Array.Copy(batchData, 0, contentFileBytes, byteOffset, length);

							if (bytesRemaining < batchByteCount)
							{
								outputInfo.BytesReceived = bytesRemaining;
							}
							else
							{
								outputInfo.BytesReceived = batchByteCount;
							}
							outputInfo.TotalBytesToReceive = byteCount;
							this.theBackgroundWorker.ReportProgress(1, outputInfo);

							byteOffset += length;
							bytesRemaining -= batchByteCount;
						}
					}
					catch (Exception ex)
					{
						this.theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get content folder or file. Exception raised: " + ex.Message + "\r\n");
					}
				}
			}
			else if (result == "success_SteamUGC")
			{
				itemUpdated_Text = this.theStreamReader.ReadLine();
				itemTitle = this.ReadMultipleLinesOfText(this.theStreamReader);
				contentFolderOrFileName = this.theStreamReader.ReadLine();
				appID_Text = this.theStreamReader.ReadLine();

				int debug = 4242;
			}
			else
			{
				this.theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get content folder or file. Steam message: " + result + "\r\n");
			}

			return result;
		}

		public string Crowbar_DownloadPreviewFile(ref string previewImagePathFileName)
		{
			this.theStreamWriter.WriteLine("Crowbar_DownloadPreviewFile");

			string result = this.theStreamReader.ReadLine();
			if (result == "success")
			{
				previewImagePathFileName = this.theStreamReader.ReadLine();
				int byteCount = int.Parse(this.theStreamReader.ReadLine());
				if (byteCount > 0)
				{
					byte[] data = new byte[byteCount + 1];
					try
					{
						this.theStreamReader.BaseStream.Read(data, 0, data.Length);

						if (this.theBackgroundWorker.CancellationPending)
						{
							return "cancelled";
						}

						MemoryStream pictureBytes = new MemoryStream(data);
						this.theBackgroundWorker.ReportProgress(1, Image.FromStream(pictureBytes));
					}
					catch (Exception ex)
					{
						this.theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get preview image." + "\r\n");
					}
				}
			}
			else
			{
				this.theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get preview image. Steam message: " + result + "\r\n");
			}

			return result;
		}

#endregion

#region SteamApps

		public string GetAppInstallPath(string appID_text)
		{
			this.theStreamWriter.WriteLine("SteamApps_GetAppInstallDir");
			this.theStreamWriter.WriteLine(appID_text);
			string result = this.theStreamReader.ReadLine();
			if (result == "success")
			{
				string appInstallPath = this.theStreamReader.ReadLine();
				return appInstallPath;
			}
			else
			{
				return "";
			}
		}

#endregion

#region SteamRemoteStorage

		public string SteamRemoteStorage_CommitPublishedFileUpdate()
		{
			this.theStreamWriter.WriteLine("SteamRemoteStorage_CommitPublishedFileUpdate");
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_CreatePublishedFileUpdateRequest(string itemID_text)
		{
			this.theStreamWriter.WriteLine("SteamRemoteStorage_CreatePublishedFileUpdateRequest");
			this.theStreamWriter.WriteLine(itemID_text);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_DeletePublishedFile(string itemID_text)
		{
			this.theStreamWriter.WriteLine("SteamRemoteStorage_DeletePublishedFile");
			this.theStreamWriter.WriteLine(itemID_text);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_FileDelete(string targetFileName)
		{
			this.theStreamWriter.WriteLine("SteamRemoteStorage_FileDelete");
			this.theStreamWriter.WriteLine(targetFileName);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_FileWrite(string localPathFileName, string remotePathFileName)
		{
			this.theStreamWriter.WriteLine("SteamRemoteStorage_FileWrite");
			this.theStreamWriter.WriteLine(localPathFileName);
			this.theStreamWriter.WriteLine(remotePathFileName);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public WorkshopItem SteamRemoteStorage_GetPublishedFileDetails(string itemID_text, string appID_text, ref string oAppID_text)
		{
			WorkshopItem publishedItem = new WorkshopItem();

			this.theStreamWriter.WriteLine("SteamRemoteStorage_GetPublishedFileDetails");
			this.theStreamWriter.WriteLine(itemID_text);

			string result = this.theStreamReader.ReadLine();
			if (result == "success")
			{
				string itemAppID = "";
				string unixTimeStampText = null;
				string ownerSteamIDText = null;

				publishedItem.ID = this.theStreamReader.ReadLine();
				Console.WriteLine("Item ID: " + publishedItem.ID);

				publishedItem.CreatorAppID = this.theStreamReader.ReadLine();

				itemAppID = this.theStreamReader.ReadLine();
				oAppID_text = itemAppID;

				ownerSteamIDText = this.theStreamReader.ReadLine();
				publishedItem.OwnerID = ulong.Parse(ownerSteamIDText);
				publishedItem.OwnerName = this.theStreamReader.ReadLine();

				unixTimeStampText = this.theStreamReader.ReadLine();
				publishedItem.Posted = long.Parse(unixTimeStampText);
				unixTimeStampText = this.theStreamReader.ReadLine();
				publishedItem.Updated = long.Parse(unixTimeStampText);

				publishedItem.Title = this.ReadMultipleLinesOfText(this.theStreamReader);
				publishedItem.Description = this.ReadMultipleLinesOfText(this.theStreamReader);

				publishedItem.ContentSize = int.Parse(this.theStreamReader.ReadLine());
				publishedItem.ContentPathFolderOrFileName = this.theStreamReader.ReadLine();
				publishedItem.PreviewImageSize = int.Parse(this.theStreamReader.ReadLine());
				//NOTE: This is URL and is probably not preview file name. There does not seem to be a way to get preview file name.
				publishedItem.PreviewImagePathFileName = this.theStreamReader.ReadLine();
				publishedItem.VisibilityText = this.theStreamReader.ReadLine();

				publishedItem.TagsAsTextLine = this.theStreamReader.ReadLine();

				if (itemAppID != appID_text)
				{
					publishedItem.ID = "0";
					publishedItem.Title = "Item is not published under selected game.";
				}
			}
			else
			{
				publishedItem.ID = "0";
				publishedItem.Title = result;
			}

			return publishedItem;
		}

		//NOTE: [10-Mar-2019] Not using GetQuota because it is unclear if it shows quota for Workshop items.
		//      [10-Mar-2019] There are indications on forums that Workshop items no longer affect user's quota.
		public void GetQuota(ref ulong availableBytes, ref ulong totalBytes)
		{
			string availableBytesText = null;
			string totalBytesText = null;

			this.theStreamWriter.WriteLine("SteamRemoteStorage_GetQuota");

			string resultIsSuccess = this.theStreamReader.ReadLine();
			if (resultIsSuccess == "success")
			{
				availableBytesText = this.theStreamReader.ReadLine();
				availableBytes = ulong.Parse(availableBytesText);
				totalBytesText = this.theStreamReader.ReadLine();
				totalBytes = ulong.Parse(totalBytesText);
			}
		}

		public string SteamRemoteStorage_PublishWorkshopFile(string contentFileName, string previewFileName, string appID_text, string title, string description, string visibility_text, BindingListEx<string> tags, ref string returnedPublishedItemID)
		{
			this.theStreamWriter.WriteLine("SteamRemoteStorage_PublishWorkshopFile");
			this.theStreamWriter.WriteLine(contentFileName);
			this.theStreamWriter.WriteLine(previewFileName);
			this.theStreamWriter.WriteLine(appID_text);
			this.WriteTextThatMightHaveMultipleLines(this.theStreamWriter, title);
			this.WriteTextThatMightHaveMultipleLines(this.theStreamWriter, description);
			this.theStreamWriter.WriteLine(visibility_text);

			this.theStreamWriter.WriteLine(tags.Count.ToString());
			foreach (string tag in tags)
			{
				this.theStreamWriter.WriteLine(tag);
			}

			string result = this.theStreamReader.ReadLine();
			if (result.StartsWith("success"))
			{
				returnedPublishedItemID = this.theStreamReader.ReadLine();
			}
			return result;
		}

		public string SteamRemoteStorage_UpdatePublishedFileFile(string contentFileName)
		{
			this.theStreamWriter.WriteLine("SteamRemoteStorage_UpdatePublishedFileFile");
			this.theStreamWriter.WriteLine(contentFileName);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_UpdatePublishedFileSetChangeDescription(string changeNote)
		{
			this.theStreamWriter.WriteLine("SteamRemoteStorage_UpdatePublishedFileSetChangeDescription");
			this.WriteTextThatMightHaveMultipleLines(this.theStreamWriter, changeNote);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

#endregion

#region SteamUGC

#region QueryViaSteamUGC

		public string SteamUGC_CreateQueryUGCDetailsRequest(string itemID_Text)
		{
			this.theStreamWriter.WriteLine("SteamUGC_CreateQueryUGCDetailsRequest");
			this.theStreamWriter.WriteLine(itemID_Text);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_CreateQueryUserUGCRequest(string appID_text, uint pageNumber)
		{
			this.theStreamWriter.WriteLine("SteamUGC_CreateQueryUserUGCRequest");
			this.theStreamWriter.WriteLine(appID_text);
			this.theStreamWriter.WriteLine(pageNumber);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SendQueryUGCRequest()
		{
			this.theStreamWriter.WriteLine("SteamUGC_SendQueryUGCRequest");

			string result = this.theStreamReader.ReadLine();
			if (result == "success")
			{
				string resultsCountText = null;
				uint resultsCount = 0;
				resultsCountText = this.theStreamReader.ReadLine();
				resultsCount = uint.Parse(resultsCountText);
				if (resultsCount == 0)
				{
					result = "done";
				}
				else
				{
					string totalCountText = null;
					uint totalCount = 0;
					totalCountText = this.theStreamReader.ReadLine();
					totalCount = uint.Parse(totalCountText);

					WorkshopItem item = null;
					string unixTimeStampText = null;
					string ownerSteamIDText = null;
					//Dim steamID As Steamworks.CSteamID = Steamworks.SteamUser.GetSteamID()

					this.theBackgroundWorker.ReportProgress(1, totalCount);
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of (uint)(resultsCount - 1) for every iteration:
					uint tempVar = (uint)(resultsCount - 1);
					for (uint i = 0; i <= tempVar; i++)
					{
						item = new WorkshopItem();

						item.ID = this.theStreamReader.ReadLine();

						//If item.ID <> "0" Then
						ownerSteamIDText = this.theStreamReader.ReadLine();
						item.OwnerID = ulong.Parse(ownerSteamIDText);
						item.OwnerName = this.theStreamReader.ReadLine();
						unixTimeStampText = this.theStreamReader.ReadLine();
						item.Posted = long.Parse(unixTimeStampText);
						unixTimeStampText = this.theStreamReader.ReadLine();
						item.Updated = long.Parse(unixTimeStampText);

						//item.Title = Me.streamReaderForQuerying.ReadLine()
						//======
						item.Title = this.ReadMultipleLinesOfText(this.theStreamReader);

						//item.Description = Me.streamReaderForQuerying.ReadLine()

						item.ContentPathFolderOrFileName = this.theStreamReader.ReadLine();
						item.PreviewImagePathFileName = this.theStreamReader.ReadLine();
						item.VisibilityText = this.theStreamReader.ReadLine();
						item.TagsAsTextLine = this.theStreamReader.ReadLine();

						//publishedItems.Add(item)
						this.theBackgroundWorker.ReportProgress(2, item);
						//End If

						if (this.theBackgroundWorker.CancellationPending)
						{
							break;
						}
					}
				}
			}

			return result;
		}

		//Public Function SteamUGC_SendQueryUGCRequest_ContentPathOrFileName() As String
		//	Me.theStreamWriter.WriteLine("SteamUGC_SendQueryUGCRequest_ContentPathOrFileName")
		//	Dim contentPathOrFileName As String = ""
		//	Dim result As String = Me.theStreamReader.ReadLine()
		//	If result = "success" Then
		//		contentPathOrFileName = Me.theStreamReader.ReadLine()
		//	End If
		//	Return contentPathOrFileName
		//End Function

#endregion

#region PublishViaSteamUGC

		public string SteamUGC_CreateItem(string appID_text, ref string returnedPublishedItemID)
		{
			this.theStreamWriter.WriteLine("SteamUGC_CreateItem");
			this.theStreamWriter.WriteLine(appID_text);

			string result = this.theStreamReader.ReadLine();
			if (result.StartsWith("success"))
			{
				returnedPublishedItemID = this.theStreamReader.ReadLine();
			}
			return result;
		}

		public string SteamUGC_StartItemUpdate(string appID_text, string itemID_text)
		{
			this.theStreamWriter.WriteLine("SteamUGC_StartItemUpdate");
			this.theStreamWriter.WriteLine(appID_text);
			this.theStreamWriter.WriteLine(itemID_text);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

#region SteamUGC_SetItem Details

		public string SteamUGC_SetItemTitle(string title)
		{
			this.theStreamWriter.WriteLine("SteamUGC_SetItemTitle");
			this.WriteTextThatMightHaveMultipleLines(this.theStreamWriter, title);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemDescription(string description)
		{
			this.theStreamWriter.WriteLine("SteamUGC_SetItemDescription");
			this.WriteTextThatMightHaveMultipleLines(this.theStreamWriter, description);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemContent(string contentPathFolderOrFileName)
		{
			this.theStreamWriter.WriteLine("SteamUGC_SetItemContent");
			this.theStreamWriter.WriteLine(contentPathFolderOrFileName);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemPreview(string previewPathFileName)
		{
			this.theStreamWriter.WriteLine("SteamUGC_SetItemPreview");
			this.theStreamWriter.WriteLine(previewPathFileName);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemVisibility(string visibility_text)
		{
			this.theStreamWriter.WriteLine("SteamUGC_SetItemVisibility");
			this.theStreamWriter.WriteLine(visibility_text);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemTags(BindingListEx<string> tags)
		{
			this.theStreamWriter.WriteLine("SteamUGC_SetItemTags");
			this.theStreamWriter.WriteLine(tags.Count.ToString());
			foreach (string tag in tags)
			{
				this.theStreamWriter.WriteLine(tag);
			}
			string result = this.theStreamReader.ReadLine();
			return result;
		}

#endregion

		public string SteamUGC_SubmitItemUpdate(string changeNote)
		{
			this.theStreamWriter.WriteLine("SteamUGC_SubmitItemUpdate");
			this.WriteTextThatMightHaveMultipleLines(this.theStreamWriter, changeNote);

			string result = "";
			BackgroundSteamPipe.PublishItemProgressInfo outputInfo = new BackgroundSteamPipe.PublishItemProgressInfo();
			BackgroundSteamPipe.PublishItemProgressInfo previousOutputInfo = new BackgroundSteamPipe.PublishItemProgressInfo();

			while (true)
			{
				result = this.theStreamReader.ReadLine();
				//NOTE: The Sleep() is needed to prevent Crowbar Publish locking up when publishing to a workshop via SteamUGC.
				//      Unfortunately, I do not understand how this prevents lock up considering that each ReadLine() waits for available input.
				System.Threading.Thread.Sleep(1);
				if (result == "OnSubmitItemUpdate")
				{
					result = this.theStreamReader.ReadLine();
					break;
				}
				else
				{
					//Threading.Thread.Sleep(1)
					outputInfo.Status = result;
					outputInfo.UploadedByteCount = ulong.Parse(this.theStreamReader.ReadLine());
					outputInfo.TotalUploadedByteCount = ulong.Parse(this.theStreamReader.ReadLine());
					//Threading.Thread.Sleep(1)
					if (outputInfo.Status == "invalid")
					{
						int debug = 4242;
					}
					else
					{
						//Threading.Thread.Sleep(1)
						if (previousOutputInfo.Status != outputInfo.Status || previousOutputInfo.UploadedByteCount != outputInfo.UploadedByteCount || previousOutputInfo.TotalUploadedByteCount != outputInfo.TotalUploadedByteCount)
						{
							//Threading.Thread.Sleep(1)
							//If outputInfo.TotalUploadedByteCount > 0 Then
							this.theBackgroundWorker.ReportProgress(2, outputInfo);

							//Threading.Thread.Sleep(1)
							previousOutputInfo.Status = outputInfo.Status;
							previousOutputInfo.UploadedByteCount = outputInfo.UploadedByteCount;
							previousOutputInfo.TotalUploadedByteCount = outputInfo.TotalUploadedByteCount;
							//End If
						}
					}
				}
			}

			return result;
		}

#endregion

#region DeleteItem

		public string SteamUGC_DeleteItem(string itemID_text)
		{
			this.theStreamWriter.WriteLine("SteamUGC_DeleteItem");
			this.theStreamWriter.WriteLine(itemID_text);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

#endregion

#region UnsubscribeItem

		public string SteamUGC_UnsubscribeItem(string itemID_text)
		{
			this.theStreamWriter.WriteLine("SteamUGC_UnsubscribeItem");
			this.theStreamWriter.WriteLine(itemID_text);
			string result = this.theStreamReader.ReadLine();
			return result;
		}

#endregion

#endregion

#region SteamUser

		public ulong GetUserSteamID()
		{
			this.theStreamWriter.WriteLine("SteamUser_GetSteamID");
			string idText = this.theStreamReader.ReadLine();
			return ulong.Parse(idText);
		}

#endregion

#region Private Functions

		//NOTE: WriteLine only writes string until first LF or CR, so need to adjust how to send this.
		//NOTE: From TextReader.ReadLine: A line is defined as a sequence of characters followed by 
		//      a carriage return (0x000d), a line feed (0x000a), a carriage return followed by a line feed, Environment.NewLine, or the end-of-stream marker.
		//      https://docs.microsoft.com/en-us/dotnet/api/system.io.textreader.readline?view=netframework-4.0
		public void WriteTextThatMightHaveMultipleLines(StreamWriter sw, string text)
		{
			//NOTE: Replace all CR in text because they are not needed and will show as blank characters in Windows TextBox.
			text = text.Replace("\r", "");
			string[] stringSeparators = {"\n"};
			string[] textList = text.Split(stringSeparators, StringSplitOptions.None);
			sw.WriteLine(textList.Length);
			Console.WriteLine("    Line count: " + textList.Length.ToString());
			int i = 1;
			foreach (string line in textList)
			{
				sw.WriteLine(line);
				Console.WriteLine("    Line " + i.ToString() + ": " + line);
				i += 1;
			}
		}

		public string ReadMultipleLinesOfText(StreamReader sr)
		{
			string text = "";

			int textLineCount = int.Parse(sr.ReadLine());
			if (textLineCount > 0)
			{
				//NOTE: Do not add CRLF to last line.
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of textLineCount - 2 for every iteration:
				int tempVar = textLineCount - 2;
				for (int i = 0; i <= tempVar; i++)
				{
					//NOTE: Add CRLF because TextBoxes use it for newlines.
					text += sr.ReadLine() + "\r\n";
				}
				text += sr.ReadLine();
			}

			return text;
		}

#endregion

#endregion

#region Core Event Handlers

#endregion

#region Private Methods

#endregion

#region Events

#endregion

#region Constants

		public const string AgreementMessageForCreate = "WARNING: Created workshop item, but is unavailable to anyone else until you accept" + "\r\n" + "    the Steam Subscriber Agreement. (NOTE: This can occur when the agreement" + "\r\n" + "    has been updated.) Click \"Steam Subscriber Agreement\" button above to open the web page" + "\r\n" + "    where you can accept the agreement.";
		public const string AgreementMessageForUpdate = "WARNING: Updated workshop item, but is unavailable to anyone else until you accept" + "\r\n" + "    the Steam Subscriber Agreement. (NOTE: This can occur when the agreement" + "\r\n" + "    has been updated.) Click \"Steam Subscriber Agreement\" button above to open the web page" + "\r\n" + "    where you can accept the agreement.";

#endregion

#region Data

		private BackgroundWorker theBackgroundWorker;
		private Process theCrowbarSteamPipeProcess;
		private NamedPipeServerStream theCrowbarSteamPipeServer = null;
		private StreamWriter theStreamWriter = null;
		private StreamReader theStreamReader = null;
		private string theReasonText;

#endregion

	}

}