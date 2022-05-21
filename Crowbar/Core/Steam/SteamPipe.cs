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
			theBackgroundWorker = bw;
			theReasonText = reasonText;

			if (theBackgroundWorker != null)
			{
				string logMessage = theReasonText + " - opening connection to Steam Workshop." + "\r\n";
				theBackgroundWorker.ReportProgress(0, logMessage);
			}

			theCrowbarSteamPipeProcess = new Process();
			try
			{
				theCrowbarSteamPipeProcess.StartInfo.UseShellExecute = false;
				theCrowbarSteamPipeProcess.StartInfo.FileName = App.CrowbarSteamPipeFileName;
				theCrowbarSteamPipeProcess.StartInfo.Arguments = pipeNameSuffix;
#if DEBUG
				theCrowbarSteamPipeProcess.StartInfo.CreateNoWindow = false;
#else
				theCrowbarSteamPipeProcess.StartInfo.CreateNoWindow = true;
#endif
				theCrowbarSteamPipeProcess.Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
			}
			finally
			{
			}

			theCrowbarSteamPipeServer = new NamedPipeServerStream("CrowbarSteamPipe" + pipeNameSuffix, PipeDirection.InOut, 1);
			Console.WriteLine("Waiting for client to connect to pipe ...");
			theCrowbarSteamPipeServer.WaitForConnection();
			Console.WriteLine("... Client connected to pipe.");

			theStreamWriter = new StreamWriter(theCrowbarSteamPipeServer);
			theStreamWriter.AutoFlush = true;
			theStreamReader = new StreamReader(theCrowbarSteamPipeServer);
			try
			{
				//If Me.theBackgroundWorker IsNot Nothing Then
				//	Dim logMessage As String = "Connecting to Steam Workshop." + vbCrLf
				//	Me.theBackgroundWorker.ReportProgress(0, logMessage)
				//End If
				theStreamWriter.WriteLine("Init");
				Console.WriteLine("Command: Init");

				string result = theStreamReader.ReadLine();
				Console.WriteLine("Result: " + result);
				if (theBackgroundWorker != null)
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
						theBackgroundWorker.ReportProgress(0, logMessage);
						if (theCrowbarSteamPipeServer != null)
						{
							Console.WriteLine("Closing pipe due to error.");
							theCrowbarSteamPipeServer.Close();
							theCrowbarSteamPipeServer = null;
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
				if (theCrowbarSteamPipeServer != null)
				{
					Console.WriteLine("Closing pipe due to error.");
					theCrowbarSteamPipeServer.Close();
					theCrowbarSteamPipeServer = null;
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
				theStreamWriter.WriteLine("Free");
				Console.WriteLine("Command: Free");
				if (theBackgroundWorker != null)
				{
					string logMessage = theReasonText + " finished - closing connection to Steam Workshop." + "\r\n";
					theBackgroundWorker.ReportProgress(0, logMessage);
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				if (theCrowbarSteamPipeServer != null)
				{
					Console.WriteLine("Closing pipe due to error.");
					theCrowbarSteamPipeServer.Close();
					theCrowbarSteamPipeServer = null;
				}
			}
			finally
			{
#if DEBUG
				//NOTE: This 'If DEBUG Then' block allows the CrowbarSteamPipe console window to stay open, if it is set to do that. 
				if (theCrowbarSteamPipeServer != null)
				{
					Console.WriteLine("Closing pipe.");
					theCrowbarSteamPipeServer.Close();
					theCrowbarSteamPipeServer = null;
				}
				if (theCrowbarSteamPipeProcess != null)
				{
					theCrowbarSteamPipeProcess.Close();
					theCrowbarSteamPipeProcess = null;
				}
#else
				Kill();
#endif
			}
		}

		public void Kill()
		{
			if (theCrowbarSteamPipeServer != null)
			{
				Console.WriteLine("Closing pipe.");
				theCrowbarSteamPipeServer.Close();
				theCrowbarSteamPipeServer = null;
			}
			if (theCrowbarSteamPipeProcess != null)
			{
				try
				{
					if (!theCrowbarSteamPipeProcess.HasExited && !theCrowbarSteamPipeProcess.CloseMainWindow())
					{
						Console.WriteLine("Killing pipe process.");
						theCrowbarSteamPipeProcess.Kill();
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
				finally
				{
					if (theCrowbarSteamPipeProcess != null)
					{
						theCrowbarSteamPipeProcess.Close();
						theCrowbarSteamPipeProcess = null;
					}
				}
			}
		}

#region Crowbar

		public string Crowbar_DeleteContentFile(string itemID_text)
		{
			theStreamWriter.WriteLine("Crowbar_DeleteContentFile");
			theStreamWriter.WriteLine(itemID_text);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string Crowbar_DownloadContentFolderOrFile(string itemID_text, string targetPath, ref byte[] contentFileBytes, ref string itemUpdated_Text, ref string itemTitle, ref string contentFolderOrFileName, ref string appID_Text)
		{
			theStreamWriter.WriteLine("Crowbar_DownloadContentFolderOrFile");
			theStreamWriter.WriteLine(itemID_text);
			theStreamWriter.WriteLine(targetPath);

			string result = theStreamReader.ReadLine();
			if (result == "success")
			{
				itemUpdated_Text = theStreamReader.ReadLine();
				itemTitle = ReadMultipleLinesOfText(theStreamReader);
				contentFolderOrFileName = theStreamReader.ReadLine();
				int byteCount = int.Parse(theStreamReader.ReadLine());
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
							length = theStreamReader.BaseStream.Read(batchData, 0, batchByteCount);
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
							theBackgroundWorker.ReportProgress(1, outputInfo);

							byteOffset += length;
							bytesRemaining -= batchByteCount;
						}
					}
					catch (Exception ex)
					{
						theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get content folder or file. Exception raised: " + ex.Message + "\r\n");
					}
				}
			}
			else if (result == "success_SteamUGC")
			{
				itemUpdated_Text = theStreamReader.ReadLine();
				itemTitle = ReadMultipleLinesOfText(theStreamReader);
				contentFolderOrFileName = theStreamReader.ReadLine();
				appID_Text = theStreamReader.ReadLine();

				int debug = 4242;
			}
			else
			{
				theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get content folder or file. Steam message: " + result + "\r\n");
			}

			return result;
		}

		public string Crowbar_DownloadPreviewFile(ref string previewImagePathFileName)
		{
			theStreamWriter.WriteLine("Crowbar_DownloadPreviewFile");

			string result = theStreamReader.ReadLine();
			if (result == "success")
			{
				previewImagePathFileName = theStreamReader.ReadLine();
				int byteCount = int.Parse(theStreamReader.ReadLine());
				if (byteCount > 0)
				{
					byte[] data = new byte[byteCount + 1];
					try
					{
						theStreamReader.BaseStream.Read(data, 0, data.Length);

						if (theBackgroundWorker.CancellationPending)
						{
							return "cancelled";
						}

						MemoryStream pictureBytes = new MemoryStream(data);
						theBackgroundWorker.ReportProgress(1, Image.FromStream(pictureBytes));
					}
					catch (Exception ex)
					{
						theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get preview image." + "\r\n");
					}
				}
			}
			else
			{
				theBackgroundWorker.ReportProgress(0, "WARNING: Unable to get preview image. Steam message: " + result + "\r\n");
			}

			return result;
		}

#endregion

#region SteamApps

		public string GetAppInstallPath(string appID_text)
		{
			theStreamWriter.WriteLine("SteamApps_GetAppInstallDir");
			theStreamWriter.WriteLine(appID_text);
			string result = theStreamReader.ReadLine();
			if (result == "success")
			{
				string appInstallPath = theStreamReader.ReadLine();
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
			theStreamWriter.WriteLine("SteamRemoteStorage_CommitPublishedFileUpdate");
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_CreatePublishedFileUpdateRequest(string itemID_text)
		{
			theStreamWriter.WriteLine("SteamRemoteStorage_CreatePublishedFileUpdateRequest");
			theStreamWriter.WriteLine(itemID_text);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_DeletePublishedFile(string itemID_text)
		{
			theStreamWriter.WriteLine("SteamRemoteStorage_DeletePublishedFile");
			theStreamWriter.WriteLine(itemID_text);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_FileDelete(string targetFileName)
		{
			theStreamWriter.WriteLine("SteamRemoteStorage_FileDelete");
			theStreamWriter.WriteLine(targetFileName);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_FileWrite(string localPathFileName, string remotePathFileName)
		{
			theStreamWriter.WriteLine("SteamRemoteStorage_FileWrite");
			theStreamWriter.WriteLine(localPathFileName);
			theStreamWriter.WriteLine(remotePathFileName);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public WorkshopItem SteamRemoteStorage_GetPublishedFileDetails(string itemID_text, string appID_text, ref string oAppID_text)
		{
			WorkshopItem publishedItem = new WorkshopItem();

			theStreamWriter.WriteLine("SteamRemoteStorage_GetPublishedFileDetails");
			theStreamWriter.WriteLine(itemID_text);

			string result = theStreamReader.ReadLine();
			if (result == "success")
			{
				string itemAppID = "";
				string unixTimeStampText = null;
				string ownerSteamIDText = null;

				publishedItem.ID = theStreamReader.ReadLine();
				Console.WriteLine("Item ID: " + publishedItem.ID);

				publishedItem.CreatorAppID = theStreamReader.ReadLine();

				itemAppID = theStreamReader.ReadLine();
				oAppID_text = itemAppID;

				ownerSteamIDText = theStreamReader.ReadLine();
				publishedItem.OwnerID = ulong.Parse(ownerSteamIDText);
				publishedItem.OwnerName = theStreamReader.ReadLine();

				unixTimeStampText = theStreamReader.ReadLine();
				publishedItem.Posted = long.Parse(unixTimeStampText);
				unixTimeStampText = theStreamReader.ReadLine();
				publishedItem.Updated = long.Parse(unixTimeStampText);

				publishedItem.Title = ReadMultipleLinesOfText(theStreamReader);
				publishedItem.Description = ReadMultipleLinesOfText(theStreamReader);

				publishedItem.ContentSize = int.Parse(theStreamReader.ReadLine());
				publishedItem.ContentPathFolderOrFileName = theStreamReader.ReadLine();
				publishedItem.PreviewImageSize = int.Parse(theStreamReader.ReadLine());
				//NOTE: This is URL and is probably not preview file name. There does not seem to be a way to get preview file name.
				publishedItem.PreviewImagePathFileName = theStreamReader.ReadLine();
				publishedItem.VisibilityText = theStreamReader.ReadLine();

				publishedItem.TagsAsTextLine = theStreamReader.ReadLine();

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

			theStreamWriter.WriteLine("SteamRemoteStorage_GetQuota");

			string resultIsSuccess = theStreamReader.ReadLine();
			if (resultIsSuccess == "success")
			{
				availableBytesText = theStreamReader.ReadLine();
				availableBytes = ulong.Parse(availableBytesText);
				totalBytesText = theStreamReader.ReadLine();
				totalBytes = ulong.Parse(totalBytesText);
			}
		}

		public string SteamRemoteStorage_PublishWorkshopFile(string contentFileName, string previewFileName, string appID_text, string title, string description, string visibility_text, BindingListEx<string> tags, ref string returnedPublishedItemID)
		{
			theStreamWriter.WriteLine("SteamRemoteStorage_PublishWorkshopFile");
			theStreamWriter.WriteLine(contentFileName);
			theStreamWriter.WriteLine(previewFileName);
			theStreamWriter.WriteLine(appID_text);
			WriteTextThatMightHaveMultipleLines(theStreamWriter, title);
			WriteTextThatMightHaveMultipleLines(theStreamWriter, description);
			theStreamWriter.WriteLine(visibility_text);

			theStreamWriter.WriteLine(tags.Count.ToString());
			foreach (string tag in tags)
			{
				theStreamWriter.WriteLine(tag);
			}

			string result = theStreamReader.ReadLine();
			if (result.StartsWith("success"))
			{
				returnedPublishedItemID = theStreamReader.ReadLine();
			}
			return result;
		}

		public string SteamRemoteStorage_UpdatePublishedFileFile(string contentFileName)
		{
			theStreamWriter.WriteLine("SteamRemoteStorage_UpdatePublishedFileFile");
			theStreamWriter.WriteLine(contentFileName);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamRemoteStorage_UpdatePublishedFileSetChangeDescription(string changeNote)
		{
			theStreamWriter.WriteLine("SteamRemoteStorage_UpdatePublishedFileSetChangeDescription");
			WriteTextThatMightHaveMultipleLines(theStreamWriter, changeNote);
			string result = theStreamReader.ReadLine();
			return result;
		}

#endregion

#region SteamUGC

#region QueryViaSteamUGC

		public string SteamUGC_CreateQueryUGCDetailsRequest(string itemID_Text)
		{
			theStreamWriter.WriteLine("SteamUGC_CreateQueryUGCDetailsRequest");
			theStreamWriter.WriteLine(itemID_Text);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_CreateQueryUserUGCRequest(string appID_text, uint pageNumber)
		{
			theStreamWriter.WriteLine("SteamUGC_CreateQueryUserUGCRequest");
			theStreamWriter.WriteLine(appID_text);
			theStreamWriter.WriteLine(pageNumber);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SendQueryUGCRequest()
		{
			theStreamWriter.WriteLine("SteamUGC_SendQueryUGCRequest");

			string result = theStreamReader.ReadLine();
			if (result == "success")
			{
				string resultsCountText = null;
				uint resultsCount = 0;
				resultsCountText = theStreamReader.ReadLine();
				resultsCount = uint.Parse(resultsCountText);
				if (resultsCount == 0)
				{
					result = "done";
				}
				else
				{
					string totalCountText = null;
					uint totalCount = 0;
					totalCountText = theStreamReader.ReadLine();
					totalCount = uint.Parse(totalCountText);

					WorkshopItem item = null;
					string unixTimeStampText = null;
					string ownerSteamIDText = null;
					//Dim steamID As Steamworks.CSteamID = Steamworks.SteamUser.GetSteamID()

					theBackgroundWorker.ReportProgress(1, totalCount);
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of (uint)(resultsCount - 1) for every iteration:
					uint tempVar = (uint)(resultsCount - 1);
					for (uint i = 0; i <= tempVar; i++)
					{
						item = new WorkshopItem();

						item.ID = theStreamReader.ReadLine();

						//If item.ID <> "0" Then
						ownerSteamIDText = theStreamReader.ReadLine();
						item.OwnerID = ulong.Parse(ownerSteamIDText);
						item.OwnerName = theStreamReader.ReadLine();
						unixTimeStampText = theStreamReader.ReadLine();
						item.Posted = long.Parse(unixTimeStampText);
						unixTimeStampText = theStreamReader.ReadLine();
						item.Updated = long.Parse(unixTimeStampText);

						//item.Title = Me.streamReaderForQuerying.ReadLine()
						//======
						item.Title = ReadMultipleLinesOfText(theStreamReader);

						//item.Description = Me.streamReaderForQuerying.ReadLine()

						item.ContentPathFolderOrFileName = theStreamReader.ReadLine();
						item.PreviewImagePathFileName = theStreamReader.ReadLine();
						item.VisibilityText = theStreamReader.ReadLine();
						item.TagsAsTextLine = theStreamReader.ReadLine();

						//publishedItems.Add(item)
						theBackgroundWorker.ReportProgress(2, item);
						//End If

						if (theBackgroundWorker.CancellationPending)
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
			theStreamWriter.WriteLine("SteamUGC_CreateItem");
			theStreamWriter.WriteLine(appID_text);

			string result = theStreamReader.ReadLine();
			if (result.StartsWith("success"))
			{
				returnedPublishedItemID = theStreamReader.ReadLine();
			}
			return result;
		}

		public string SteamUGC_StartItemUpdate(string appID_text, string itemID_text)
		{
			theStreamWriter.WriteLine("SteamUGC_StartItemUpdate");
			theStreamWriter.WriteLine(appID_text);
			theStreamWriter.WriteLine(itemID_text);
			string result = theStreamReader.ReadLine();
			return result;
		}

#region SteamUGC_SetItem Details

		public string SteamUGC_SetItemTitle(string title)
		{
			theStreamWriter.WriteLine("SteamUGC_SetItemTitle");
			WriteTextThatMightHaveMultipleLines(theStreamWriter, title);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemDescription(string description)
		{
			theStreamWriter.WriteLine("SteamUGC_SetItemDescription");
			WriteTextThatMightHaveMultipleLines(theStreamWriter, description);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemContent(string contentPathFolderOrFileName)
		{
			theStreamWriter.WriteLine("SteamUGC_SetItemContent");
			theStreamWriter.WriteLine(contentPathFolderOrFileName);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemPreview(string previewPathFileName)
		{
			theStreamWriter.WriteLine("SteamUGC_SetItemPreview");
			theStreamWriter.WriteLine(previewPathFileName);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemVisibility(string visibility_text)
		{
			theStreamWriter.WriteLine("SteamUGC_SetItemVisibility");
			theStreamWriter.WriteLine(visibility_text);
			string result = theStreamReader.ReadLine();
			return result;
		}

		public string SteamUGC_SetItemTags(BindingListEx<string> tags)
		{
			theStreamWriter.WriteLine("SteamUGC_SetItemTags");
			theStreamWriter.WriteLine(tags.Count.ToString());
			foreach (string tag in tags)
			{
				theStreamWriter.WriteLine(tag);
			}
			string result = theStreamReader.ReadLine();
			return result;
		}

#endregion

		public string SteamUGC_SubmitItemUpdate(string changeNote)
		{
			theStreamWriter.WriteLine("SteamUGC_SubmitItemUpdate");
			WriteTextThatMightHaveMultipleLines(theStreamWriter, changeNote);

			string result = "";
			BackgroundSteamPipe.PublishItemProgressInfo outputInfo = new BackgroundSteamPipe.PublishItemProgressInfo();
			BackgroundSteamPipe.PublishItemProgressInfo previousOutputInfo = new BackgroundSteamPipe.PublishItemProgressInfo();

			while (true)
			{
				result = theStreamReader.ReadLine();
				//NOTE: The Sleep() is needed to prevent Crowbar Publish locking up when publishing to a workshop via SteamUGC.
				//      Unfortunately, I do not understand how this prevents lock up considering that each ReadLine() waits for available input.
				System.Threading.Thread.Sleep(1);
				if (result == "OnSubmitItemUpdate")
				{
					result = theStreamReader.ReadLine();
					break;
				}
				else
				{
					//Threading.Thread.Sleep(1)
					outputInfo.Status = result;
					outputInfo.UploadedByteCount = ulong.Parse(theStreamReader.ReadLine());
					outputInfo.TotalUploadedByteCount = ulong.Parse(theStreamReader.ReadLine());
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
							theBackgroundWorker.ReportProgress(2, outputInfo);

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
			theStreamWriter.WriteLine("SteamUGC_DeleteItem");
			theStreamWriter.WriteLine(itemID_text);
			string result = theStreamReader.ReadLine();
			return result;
		}

#endregion

#region UnsubscribeItem

		public string SteamUGC_UnsubscribeItem(string itemID_text)
		{
			theStreamWriter.WriteLine("SteamUGC_UnsubscribeItem");
			theStreamWriter.WriteLine(itemID_text);
			string result = theStreamReader.ReadLine();
			return result;
		}

#endregion

#endregion

#region SteamUser

		public ulong GetUserSteamID()
		{
			theStreamWriter.WriteLine("SteamUser_GetSteamID");
			string idText = theStreamReader.ReadLine();
			return !string.IsNullOrEmpty(idText) ? ulong.Parse(idText) : 0ul;
		}

#endregion

#region Private Functions

		//NOTE: WriteLine only writes string until first LF or CR, so need to adjust how to send 
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