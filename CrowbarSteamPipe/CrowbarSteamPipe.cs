//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using Steamworks;

namespace CrowbarSteamPipe
{
	public static class CrowbarSteamPipe
	{
		public static void Main()
		{
			string pipeNameSuffix = "";
			if (ConversionHelper.CommandLineArgs.Count > 0)
				pipeNameSuffix = ConversionHelper.CommandLineArgs[0];

			NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "CrowbarSteamPipe" + pipeNameSuffix, PipeDirection.InOut, PipeOptions.WriteThrough);

			// Connect to the pipe or wait until the pipe is available.
			Console.WriteLine("Attempting to connect to the pipe ...");
			pipeClient.Connect();
			Console.WriteLine("... Connected to pipe.");

			sw = new StreamWriter(pipeClient);
			sw.AutoFlush = true;
			sr = new StreamReader(pipeClient);
			string command = null;
			try
			{
				while (pipeClient.IsConnected)
				{
					theItemIsUploading = false;

					command = sr.ReadLine();
					Console.WriteLine("Command from server: " + command);

					if (command == "Init")
					{
						InitSteam();
					}
					else if (command == "Free")
					{
						FreeSteam();
						Console.WriteLine("Client is closing pipe due to command Free.");
						pipeClient.Close();

					}
					else if (command == "Crowbar_DeleteContentFile")
					{
						Crowbar_DeleteContentFile();
					}
					else if (command == "Crowbar_DownloadContentFolderOrFile")
					{
						Crowbar_DownloadContentFolderOrFile();
					}
					else if (command == "Crowbar_DownloadPreviewFile")
					{
						Crowbar_DownloadPreviewFile();

					}
					else if (command == "SteamApps_BIsSubscribedApp")
					{
						SteamApps_BIsSubscribedApp();
					}
					else if (command == "SteamApps_GetAppInstallDir")
					{
						SteamApps_GetAppInstallDir();

					}
					else if (command == "SteamRemoteStorage_DeletePublishedFile")
					{
						SteamRemoteStorage_DeletePublishedFile();
					}
					else if (command == "SteamRemoteStorage_FileDelete")
					{
						SteamRemoteStorage_FileDelete();
						//ElseIf command = "SteamRemoteStorage_FileRead" Then
						//	SteamRemoteStorage_FileRead()
					}
					else if (command == "SteamRemoteStorage_FileWrite")
					{
						SteamRemoteStorage_FileWrite();
					}
					else if (command == "SteamRemoteStorage_CommitPublishedFileUpdate")
					{
						SteamRemoteStorage_CommitPublishedFileUpdate();
					}
					else if (command == "SteamRemoteStorage_CreatePublishedFileUpdateRequest")
					{
						SteamRemoteStorage_CreatePublishedFileUpdateRequest();
					}
					else if (command == "SteamRemoteStorage_GetPublishedFileDetails")
					{
						SteamRemoteStorage_GetPublishedFileDetails(new PublishedFileId_t(0));
					}
					else if (command == "SteamRemoteStorage_GetQuota")
					{
						SteamRemoteStorage_GetQuota();
					}
					else if (command == "SteamRemoteStorage_PublishWorkshopFile")
					{
						SteamRemoteStorage_PublishWorkshopFile();
						//ElseIf command = "SteamRemoteStorage_UGCDownload" Then
						//	SteamRemoteStorage_UGCDownload()
						//ElseIf command = "SteamRemoteStorage_UGCDownloadToLocation" Then
						//	SteamRemoteStorage_UGCDownloadToLocation()
						//ElseIf command = "SteamRemoteStorage_UGCRead" Then
						//	SteamRemoteStorage_UGCRead()
					}
					else if (command == "SteamRemoteStorage_UpdatePublishedFileFile")
					{
						SteamRemoteStorage_UpdatePublishedFileFile();
					}
					else if (command == "SteamRemoteStorage_UpdatePublishedFileSetChangeDescription")
					{
						SteamRemoteStorage_UpdatePublishedFileSetChangeDescription();

					}
					else if (command == "SteamUGC_CreateItem")
					{
						SteamUGC_CreateItem();
					}
					else if (command == "SteamUGC_CreateQueryUGCDetailsRequest")
					{
						SteamUGC_CreateQueryUGCDetailsRequest();
					}
					else if (command == "SteamUGC_CreateQueryUserUGCRequest")
					{
						SteamUGC_CreateQueryUserUGCRequest();
					}
					else if (command == "SteamUGC_DeleteItem")
					{
						SteamUGC_DeleteItem();
						//ElseIf command = "SteamUGC_DownloadItem" Then
						//	SteamUGC_DownloadItem()
						//ElseIf command = "SteamUGC_GetItemUpdateProgress" Then
						//	SteamUGC_GetItemUpdateProgress()
					}
					else if (command == "SteamUGC_SendQueryUGCRequest")
					{
						SteamUGC_SendQueryUGCRequest();
					}
					else if (command == "SteamUGC_SetItemContent")
					{
						SteamUGC_SetItemContent();
					}
					else if (command == "SteamUGC_SetItemDescription")
					{
						SteamUGC_SetItemDescription();
					}
					else if (command == "SteamUGC_SetItemPreview")
					{
						SteamUGC_SetItemPreview();
					}
					else if (command == "SteamUGC_SetItemTags")
					{
						SteamUGC_SetItemTags();
					}
					else if (command == "SteamUGC_SetItemTitle")
					{
						SteamUGC_SetItemTitle();
					}
					else if (command == "SteamUGC_SetItemUpdateLanguage")
					{
						SteamUGC_SetItemUpdateLanguage();
					}
					else if (command == "SteamUGC_SetItemVisibility")
					{
						SteamUGC_SetItemVisibility();
					}
					else if (command == "SteamUGC_StartItemUpdate")
					{
						SteamUGC_StartItemUpdate();
					}
					else if (command == "SteamUGC_SubmitItemUpdate")
					{
						SteamUGC_SubmitItemUpdate();
					}
					else if (command == "SteamUGC_UnsubscribeItem")
					{
						SteamUGC_UnsubscribeItem();

					}
					else if (command == "SteamUser_GetSteamID")
					{
						SteamUser_GetSteamID();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
			}
			finally
			{
				if (pipeClient != null)
				{
					Console.WriteLine("Client is closing pipe.");
					pipeClient.Close();
					pipeClient = null;
				}

#if DEBUG
				//For debugging, keep console open until Enter Is pressed.
				Console.ReadLine();
#endif
			}
		}

#region Init and Free

		private static void InitSteam()
		{
			string result = null;

			if (SteamAPI.Init())
			{
				Console.WriteLine("SteamAPI.Init()");
				result = "success";
			}
			else
			{
				Console.WriteLine("SteamAPI.Init() failed.");
				result = "error";
			}

			sw.WriteLine(result);
		}

		private static void FreeSteam()
		{
			SteamAPI.Shutdown();
			Console.WriteLine("SteamAPI.Shutdown()");
		}

#endregion

#region Crowbar

		//NOTE: These are convenience functions that combine calls to Steam functions to save time 
		//      by avoiding the passing of several values over the named pipes.

#region Crowbar_DeleteContentFile

		private static void Crowbar_DeleteContentFile()
		{
			PublishedFileId_t itemID = new PublishedFileId_t();
			string itemID_text = sr.ReadLine();
			itemID.m_PublishedFileId = ulong.Parse(itemID_text);

			SteamAPICall_t result = SteamRemoteStorage.GetPublishedFileDetails(itemID, 0);
			CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageGetPublishedFileDetailsResult_t>(OnGetPublishedFileDetailsForInternalResults, result);

			if (theResultMessage == "success")
			{
				bool publishedFileExists = SteamRemoteStorage_FileExists(theItemContentFileName);
				SteamRemoteStorage_FileDelete(theItemContentFileName);
			}
			else
			{
				sw.WriteLine(theResultMessage);
			}
		}

		private static void OnGetPublishedFileDetailsForInternalResults(RemoteStorageGetPublishedFileDetailsResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					Console.WriteLine("OnGetPublishedFileDetailsForInternalResults - success");

					theUGCHandleForContentFile = pCallResult.m_hFile;
					theUGCHandleForPreviewImageFile = pCallResult.m_hPreviewFile;
					//theUGCPreviewImageFileSize = pCallResult.m_nPreviewFileSize

					//theAppID = pCallResult.m_nConsumerAppID
					theItemTitle = pCallResult.m_rgchTitle;
					theItemUpdated = pCallResult.m_rtimeUpdated;
					theItemContentFileName = pCallResult.m_pchFileName;
					theAppID = pCallResult.m_nConsumerAppID;

					theResultMessage = "success";
				}
				else
				{
					Console.WriteLine("OnGetPublishedFileDetailsForInternalResults ERROR: " + pCallResult.m_eResult.ToString());
					theResultMessage = GetErrorMessage(pCallResult.m_eResult);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				theResultMessage = "EXCEPTION: " + ex.Message;
			}

			theCallResultIsFinished = true;
		}

#endregion

#region Crowbar_DownloadContentFolderOrFile

		private static void Crowbar_DownloadContentFolderOrFile()
		{
			string itemID_text = sr.ReadLine();
			theItemID.m_PublishedFileId = ulong.Parse(itemID_text);
			string targetPath = sr.ReadLine();

			Steamworks.EItemState itemState = (EItemState)SteamUGC.GetItemState(theItemID);
			Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemState " + GetItemStateText(itemState));
			if ((itemState & Steamworks.EItemState.k_EItemStateLegacyItem) != 0)
			{
				Console.WriteLine("Crowbar_DownloadContentFolderOrFile - Download via RemoteStorage");

				SteamAPICall_t result = SteamRemoteStorage.GetPublishedFileDetails(theItemID, 0);
				CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageGetPublishedFileDetailsResult_t>(OnGetPublishedFileDetailsForInternalResults, result);

				//Console.WriteLine("Crowbar_DownloadContentFolderOrFile - theUGCHandleForContentFile: " + theUGCHandleForContentFile.ToString())

				result = SteamRemoteStorage.UGCDownload(theUGCHandleForContentFile, 0);
				CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageDownloadUGCResult_t>(OnDownloadUGC_ContentFile, result);
				//ElseIf (itemState And Steamworks.EItemState.k_EItemStateInstalled) <> 0 Then
				//	'NOTE: Must init these 4 vars with values; otherwise only zeroes and an empty string are returned in them from GetItemInstallInfo().
				//	'NOTE: The value for itemFolderNameLength must be greater than 0, presumably to tell Steam how many characters itemFolderName can hold.
				//	Dim itemSize As ULong = 0
				//	Dim itemFolderName As String = ""
				//	Dim itemFolderNameLength As UInteger = 1024
				//	Dim itemUpdated As UInteger = 0
				//	Dim resultOfGetItemInstallInfoIsSuccess As Boolean = SteamUGC.GetItemInstallInfo(theItemID, itemSize, itemFolderName, itemFolderNameLength, itemUpdated)
				//	Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemInstallInfo: " + theItemID.ToString() + " " + itemSize.ToString() + " """ + itemFolderName + """ " + itemUpdated.ToString())
				//	If resultOfGetItemInstallInfoIsSuccess Then
				//		sw.WriteLine("success_SteamUGC")
				//		sw.WriteLine(itemUpdated)
				//		Dim result As SteamAPICall_t = SteamRemoteStorage.GetPublishedFileDetails(theItemID, 0)
				//		CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageGetPublishedFileDetailsResult_t)(AddressOf OnGetPublishedFileDetailsForInternalResults, result)
				//		WriteTextThatMightHaveMultipleLines(theItemTitle)
				//		sw.WriteLine(itemFolderName)
				//		sw.WriteLine(theAppID.ToString())
				//	Else
				//		sw.WriteLine("error")
				//	End If

			}
			else if ((itemState & Steamworks.EItemState.k_EItemStateInstalled) != 0 || (itemState & Steamworks.EItemState.k_EItemStateNeedsUpdate) != 0 || itemState == EItemState.k_EItemStateNone)
			{
				//NOTE: Even if Steam thinks the item is installed, download it anyway.
				Console.WriteLine("Crowbar_DownloadContentFolderOrFile - Download via SteamUGC");

				SteamAPICall_t result = SteamRemoteStorage.GetPublishedFileDetails(theItemID, 0);
				CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageGetPublishedFileDetailsResult_t>(OnGetPublishedFileDetailsForInternalResults, result);

				//NOTE: Using SteamUGC.BInitWorkshopForGameServer made extra folders in the target path 
				//      and also copied entire content folder that includes subscribed and other installed workshop items.
				//Console.WriteLine("Crowbar_DownloadContentFolderOrFile - BInitWorkshopForGameServer: """ + targetPath + """")
				//Dim depotID As DepotId_t
				//depotID.m_DepotId = theAppID.m_AppId
				//Dim resultOfInitIsSuccess As Boolean = SteamUGC.BInitWorkshopForGameServer(depotID, targetPath)

				//If resultOfInitIsSuccess Then
				//	Console.WriteLine("Crowbar_DownloadContentFolderOrFile - BInitWorkshopForGameServer success")

				Console.WriteLine("Crowbar_DownloadContentFolderOrFile - itemID: " + theItemID.ToString());
				bool resultIsSuccess = SteamUGC.DownloadItem(theItemID, true);

				if (resultIsSuccess)
				{
					Console.WriteLine("Crowbar_DownloadContentFolderOrFile - DownloadItem success");
					Callback<DownloadItemResult_t> DownloadItemCallback = null;
					Callback<ItemInstalled_t> InstalledItemCallback = null;
					DownloadItemCallback = Callback<DownloadItemResult_t>.Create(OnDownloadItem);
					InstalledItemCallback = Callback<ItemInstalled_t>.Create(OnInstalledItem);

					CrowbarSteamPipe.RunCallbacks();

					if (theResultMessage == "success")
					{
						Console.WriteLine("Crowbar_DownloadContentFolderOrFile - OnDownloadItem success");

						itemState = (EItemState)SteamUGC.GetItemState(theItemID);
						Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemState " + GetItemStateText(itemState));

						//NOTE: Must init these 4 vars with values; otherwise only zeroes and an empty string are returned in them from GetItemInstallInfo().
						//NOTE: The value for itemFolderNameLength must be greater than 0, presumably to tell Steam how many characters folderName can hold.
						ulong itemSize = 0;
						string folderName = "";
						uint folderNameLength = 1024;
						uint updated = 0;
						bool resultOfGetItemInstallInfoIsSuccess = SteamUGC.GetItemInstallInfo(theItemID, out itemSize, out folderName, folderNameLength, out updated);
						Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemInstallInfo: " + theItemID.ToString() + " " + itemSize.ToString() + " \"" + folderName + "\" " + updated.ToString());
						bool fileExists = SteamRemoteStorage_FileExists(theItemContentFileName);
						Console.WriteLine("Crowbar_DownloadContentFolderOrFile - publishedFileExists = " + fileExists.ToString());
						if (resultOfGetItemInstallInfoIsSuccess)
						{
							Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemInstallInfo success");
							sw.WriteLine("success_SteamUGC");
							sw.WriteLine(updated);
							//Dim result As SteamAPICall_t = SteamRemoteStorage.GetPublishedFileDetails(theItemID, 0)
							//CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStorageGetPublishedFileDetailsResult_t)(AddressOf OnGetPublishedFileDetailsForInternalResults, result)
							WriteTextThatMightHaveMultipleLines(theItemTitle);
							sw.WriteLine(folderName);
							sw.WriteLine(theAppID.ToString());
						}
						else
						{
							Console.WriteLine("Crowbar_DownloadContentFolderOrFile - GetItemInstallInfo error");
							sw.WriteLine("error");
						}
					}
					else
					{
						Console.WriteLine("Crowbar_DownloadContentFolderOrFile - OnDownloadItem error");
						sw.WriteLine("error");
					}
				}
				else
				{
					Console.WriteLine("Crowbar_DownloadContentFolderOrFile - DownloadItem error");
					sw.WriteLine("error");
				}
				//Else
				//	Console.WriteLine("Crowbar_DownloadContentFolderOrFile - BInitWorkshopForGameServer error")
				//	sw.WriteLine("error")
				//End If
			}
			Console.WriteLine("Crowbar_DownloadContentFolderOrFile - End Sub");
		}

		private static void OnDownloadUGC_ContentFile(RemoteStorageDownloadUGCResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					sw.WriteLine("success");
					Console.WriteLine("OnDownloadUGC_ContentFile - file name: " + pCallResult.m_pchFileName);
					Console.WriteLine("OnDownloadUGC_ContentFile - byte size: " + pCallResult.m_nSizeInBytes.ToString());

					sw.WriteLine(theItemUpdated);
					WriteTextThatMightHaveMultipleLines(theItemTitle);
					sw.WriteLine(pCallResult.m_pchFileName);

					byte[] data = new byte[pCallResult.m_nSizeInBytes];
					int byteCountRead = SteamRemoteStorage.UGCRead(theUGCHandleForContentFile, data, pCallResult.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished);
					sw.WriteLine(data.Length.ToString());
					sw.BaseStream.Write(data, 0, data.Length);
				}
				else
				{
					Console.WriteLine("OnDownloadUGC_ContentFile ERROR: " + pCallResult.m_eResult.ToString());
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("OnDownloadUGC_ContentFile EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

		private static void OnDownloadItem(DownloadItemResult_t pCallback)
		{
			Console.WriteLine("OnDownloadItem");
			try
			{
				theResultMessage = "error";
				if (pCallback.m_nPublishedFileId == theItemID)
				{
					if (pCallback.m_eResult == EResult.k_EResultOK)
					{
						Console.WriteLine("OnDownloadItem - success");

						theResultMessage = "success";
					}
					else
					{
						Console.WriteLine("OnDownloadItem ERROR: " + pCallback.m_eResult.ToString());
						theResultMessage = pCallback.m_eResult.ToString();
					}

					theCallResultIsFinished = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("OnDownloadItem EXCEPTION: " + ex.Message);
				theResultMessage = "OnDownloadItem EXCEPTION: " + ex.Message;
			}
		}

		private static void OnInstalledItem(ItemInstalled_t pCallback)
		{
			Console.WriteLine("OnInstalledItem");
			try
			{
				//theResultMessage = "error"
				if (pCallback.m_nPublishedFileId == theItemID)
				{
					Console.WriteLine("OnInstalledItem");
					//theResultMessage = "success - success"

					//theCallResultIsFinished = True
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("OnInstalledItem EXCEPTION: " + ex.Message);
				//theResultMessage = "OnInstalledItem EXCEPTION: " + ex.Message
			}
		}

#endregion

#region Crowbar_DownloadPreviewFile

		private static void Crowbar_DownloadPreviewFile()
		{
			//Dim fileID As PublishedFileId_t
			//Dim fileID_Text As String
			//fileID_Text = sr.ReadLine()
			//fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

			//theCallResultIsFinished = False
			//GetPublishedFileDetailsCallResult = CallResult(Of RemoteStorageGetPublishedFileDetailsResult_t).Create(AddressOf OnGetPublishedFileDetailsForInternalResults)
			//Dim result As SteamAPICall_t
			//result = SteamRemoteStorage.GetPublishedFileDetails(fileID, 0)
			//GetPublishedFileDetailsCallResult.Set(result)

			//While Not theCallResultIsFinished
			//	SteamAPI.RunCallbacks()
			//End While

			SteamAPICall_t result = SteamRemoteStorage.UGCDownload(theUGCHandleForPreviewImageFile, 0);
			CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageDownloadUGCResult_t>(OnUGCDownload_PreviewFile, result);
		}

		private static void OnUGCDownload_PreviewFile(RemoteStorageDownloadUGCResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					Console.WriteLine("OnDownloadUGC_PreviewFile - file name: " + pCallResult.m_pchFileName);
					Console.WriteLine("OnDownloadUGC_PreviewFile - byte size: " + pCallResult.m_nSizeInBytes.ToString());

					byte[] data = new byte[pCallResult.m_nSizeInBytes];
					//data(0) = 1
					//data(1) = 2
					//data(2) = 3
					int byteCountRead = SteamRemoteStorage.UGCRead(theUGCHandleForPreviewImageFile, data, pCallResult.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished);

					//Console.WriteLine("OnDownloadUGC_PreviewFile UGCRead - size: " + pCallResult.m_nSizeInBytes.ToString())
					//Console.WriteLine("OnDownloadUGC_PreviewFile UGCRead - byteCountRead: " + byteCountRead.ToString())
					//Console.WriteLine("OnDownloadUGC_PreviewFile UGCRead - data size: " + data.Length.ToString())
					//Console.WriteLine("OnDownloadUGC_PreviewFile UGCRead - data: " + data.ToString())
					//Console.WriteLine(data(0))
					//Console.WriteLine(data(1))
					//Console.WriteLine(data(2))

					sw.WriteLine("success");
					sw.WriteLine(pCallResult.m_pchFileName);
					sw.WriteLine(data.Length.ToString());
					sw.BaseStream.Write(data, 0, data.Length);
				}
				else
				{
					Console.WriteLine("OnDownloadUGC_PreviewFile ERROR: " + pCallResult.m_eResult.ToString());
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("OnDownloadUGC_PreviewFile EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#endregion

#region SteamApps

#region SteamApps_BIsSubscribedApp

		private static void SteamApps_BIsSubscribedApp()
		{
			string appID_Text = sr.ReadLine();
			AppId_t appID = new AppId_t();
			appID.m_AppId = uint.Parse(appID_Text);

			Console.WriteLine("SteamApps_BIsSubscribedApp appID_Text: " + appID_Text);
			bool resultIsSuccess = SteamApps.BIsSubscribedApp(appID);
			Console.WriteLine("SteamApps_BIsSubscribedApp = " + resultIsSuccess.ToString());
			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamApps_GetAppInstallDir

		// SteamApps.GetAppInstallDir(AppId_t appID, char *pchFolder, uint32 cchFolderBufferSize)
		//    Gets the install folder for a specific AppID.
		//    This works even if the application is not installed, based on where the game would be installed with the default Steam library location.
		//    Returns: uint32
		//    Returns the install directory path as a string into the buffer provided in pchFolder and returns the number of bytes that were copied into that buffer.
		private static void SteamApps_GetAppInstallDir()
		{
			string appID_Text = sr.ReadLine();
			AppId_t appID = new AppId_t();
			appID.m_AppId = uint.Parse(appID_Text);

			string appInstallPath = "";
			uint appInstallPathLength = 1024;
			Console.WriteLine("SteamApps_GetAppInstallDir appID_Text: " + appID_Text);
			uint appInstallPathActualLength = SteamApps.GetAppInstallDir(appID, out appInstallPath, appInstallPathLength);
			Console.WriteLine("SteamApps_GetAppInstallDir appInstallPath(" + appInstallPathActualLength.ToString() + "): " + appInstallPath);
			if (appInstallPathActualLength > 0)
			{
				sw.WriteLine("success");
				sw.WriteLine(appInstallPath);
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#endregion

#region SteamRemoteStorage

#region SteamRemoteStorage_DeletePublishedFile

		private static void SteamRemoteStorage_DeletePublishedFile()
		{
			string itemID_text = sr.ReadLine();
			PublishedFileId_t itemID = new PublishedFileId_t();
			itemID.m_PublishedFileId = ulong.Parse(itemID_text);

			SteamAPICall_t result = SteamRemoteStorage.DeletePublishedFile(itemID);
			CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageDeletePublishedFileResult_t>(OnDeletePublishedFile, result);
		}

		private static void OnDeletePublishedFile(RemoteStorageDeletePublishedFileResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					Console.WriteLine("OnDeletePublishedFile - success");
					sw.WriteLine("success");
				}
				else
				{
					Console.WriteLine("OnDeletePublishedFile ERROR: " + pCallResult.m_eResult.ToString());
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#region SteamRemoteStorage_FileDelete

		private static void SteamRemoteStorage_FileDelete()
		{
			string targetFileName = sr.ReadLine();

			SteamRemoteStorage_FileDelete(targetFileName);
		}

		private static void SteamRemoteStorage_FileDelete(string targetFileName)
		{
			Console.WriteLine("SteamRemoteStorage_FileDelete targetFileName: " + targetFileName);
			bool resultIsSuccess = SteamRemoteStorage.FileDelete(targetFileName);

			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamRemoteStorage_FileExists

		private static bool SteamRemoteStorage_FileExists(string targetFileName)
		{
			Console.WriteLine("SteamRemoteStorage_FileExists targetFileName: " + targetFileName);
			bool resultIsSuccess = SteamRemoteStorage.FileExists(targetFileName);
			Console.WriteLine("SteamRemoteStorage_FileExists = " + resultIsSuccess.ToString());
			return resultIsSuccess;
		}

#endregion

#region SteamRemoteStorage_FileRead

		//Private Sub SteamRemoteStorage_FileRead()
		//	Dim fileName As String
		//	fileName = sr.ReadLine()

		//	Dim size As Int32 = SteamRemoteStorage.GetFileSize(fileName)

		//	Dim data As Byte() = New Byte(size) {}
		//	Dim byteCountRead As Integer = SteamRemoteStorage.FileRead(fileName, data, size)

		//	Console.WriteLine("fileName: " + fileName)
		//	Console.WriteLine("size: " + size.ToString())
		//	Console.WriteLine("byteCountRead: " + byteCountRead.ToString())
		//	Console.WriteLine("data: " + data.ToString())

		//	sw.WriteLine(byteCountRead.ToString())
		//	sw.Write(data)
		//	'For Each aByte In data
		//	'	sw.Write(aByte)
		//	'Next
		//End Sub

#endregion

#region SteamRemoteStorage_FileWrite

		private static void SteamRemoteStorage_FileWrite()
		{
			string sourcePathFileName = sr.ReadLine();
			string targetFileName = sr.ReadLine();

			bool resultIsSuccess = false;
			FileInfo sourceFileInfo = new FileInfo(sourcePathFileName);
			if (sourceFileInfo.Length < Steamworks.Constants.k_unMaxCloudFileChunkSize)
			{
				byte[] data = File.ReadAllBytes(sourcePathFileName);
				resultIsSuccess = SteamRemoteStorage.FileWrite(targetFileName, data, data.Length);
				if (resultIsSuccess)
				{
					sw.WriteLine("success");
				}
				else
				{
					sw.WriteLine("error");
				}
			}
			else
			{
				using (FileStream sourceStream = new FileStream(sourcePathFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					//Returns k_UGCFileStreamHandleInvalid under the following conditions:
					//    You tried to write to an invalid path or filename. Because Steam Cloud is cross platform the files need to have valid names on all supported OSes and file systems. See Microsoft's documentation on Naming Files, Paths, and Namespaces.
					//    The current user's Steam Cloud storage quota has been exceeded. They may have run out of space, or have too many files.
					Console.WriteLine("targetFileName: " + targetFileName.ToString());
					UGCFileWriteStreamHandle_t writeHandle = SteamRemoteStorage.FileWriteStreamOpen(targetFileName);
					if (writeHandle != UGCFileWriteStreamHandle_t.Invalid)
					{
						int byteCountRead = 1;
						byte[] data = new byte[Steamworks.Constants.k_unMaxCloudFileChunkSize];
						while (byteCountRead > 0)
						{
							//Console.WriteLine("byteCountRead: " + byteCountRead.ToString())
							Console.WriteLine("data.Length: " + data.Length.ToString());
							byteCountRead = sourceStream.Read(data, 0, data.Length);
							if (byteCountRead > 0)
							{
								//true if the data was successfully written to the file write stream.
								//false if writeHandle is not a valid file write stream, cubData is negative or larger than k_unMaxCloudFileChunkSize, or the current user's Steam Cloud storage quota has been exceeded. They may have run out of space, or have too many files.
								resultIsSuccess = SteamRemoteStorage.FileWriteStreamWriteChunk(writeHandle, data, byteCountRead);
								Console.WriteLine("FileWriteStreamWriteChunk: " + resultIsSuccess.ToString());
								if (!resultIsSuccess)
								{
									break;
								}
							}
						}

						//resultIsSuccess = SteamRemoteStorage.FileWriteStreamClose(writeHandle)
						//If resultIsSuccess Then
						//	sw.WriteLine("success")
						//Else
						//	sw.WriteLine("error")
						//End If
						Console.WriteLine("FileWriteStreamClose");
						SteamRemoteStorage.FileWriteStreamClose(writeHandle);
						if (resultIsSuccess)
						{
							sw.WriteLine("success");
						}
						else
						{
							sw.WriteLine("error");
						}
					}
					else
					{
						//TODO: Check if file already exists. If so, tell user about it. Maybe it is locked by another app.
						sw.WriteLine("error");
					}
				}
			}
		}

#endregion

#region SteamRemoteStorage_CommitPublishedFileUpdate

		private static void SteamRemoteStorage_CommitPublishedFileUpdate()
		{
			SteamAPICall_t result = SteamRemoteStorage.CommitPublishedFileUpdate(thePublishedFileUpdateHandle);
			CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageUpdatePublishedFileResult_t>(OnCommitPublishedFileUpdate, result);
		}

		private static void OnCommitPublishedFileUpdate(RemoteStorageUpdatePublishedFileResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					if (pCallResult.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						sw.WriteLine("success_agreement");
					}
					else
					{
						sw.WriteLine("success");
					}
				}
				else
				{
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#region SteamRemoteStorage_CreatePublishedFileUpdateRequest

		private static void SteamRemoteStorage_CreatePublishedFileUpdateRequest()
		{
			string fileID_Text = sr.ReadLine();
			PublishedFileId_t fileID = new PublishedFileId_t();
			fileID.m_PublishedFileId = ulong.Parse(fileID_Text);

			thePublishedFileUpdateHandle = SteamRemoteStorage.CreatePublishedFileUpdateRequest(fileID);
			if (thePublishedFileUpdateHandle != PublishedFileUpdateHandle_t.Invalid)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamRemoteStorage_GetPublishedFileDetails

		private static void SteamRemoteStorage_GetPublishedFileDetails(PublishedFileId_t itemID)
		{
			if (itemID.m_PublishedFileId == 0)
			{
				string itemID_text = sr.ReadLine();
				itemID.m_PublishedFileId = ulong.Parse(itemID_text);
			}

			Console.WriteLine("SteamRemoteStorage_GetPublishedFileDetails - fileID = " + itemID.ToString());

			SteamAPICall_t result = SteamRemoteStorage.GetPublishedFileDetails(itemID, 0);
			CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageGetPublishedFileDetailsResult_t>(OnGetPublishedFileDetails, result);
		}

		private static void OnGetPublishedFileDetails(RemoteStorageGetPublishedFileDetailsResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				Console.WriteLine("OnGetPublishedFileDetails");
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					sw.WriteLine("success");
					sw.WriteLine(pCallResult.m_nPublishedFileId);
					sw.WriteLine(pCallResult.m_nCreatorAppID);
					sw.WriteLine(pCallResult.m_nConsumerAppID);
					sw.WriteLine(pCallResult.m_ulSteamIDOwner);
					sw.WriteLine(SteamFriends.GetFriendPersonaName(new CSteamID(pCallResult.m_ulSteamIDOwner)));
					sw.WriteLine(pCallResult.m_rtimeCreated);
					sw.WriteLine(pCallResult.m_rtimeUpdated);
					WriteTextThatMightHaveMultipleLines(pCallResult.m_rgchTitle);
					WriteTextThatMightHaveMultipleLines(pCallResult.m_rgchDescription);
					Console.WriteLine("OnGetPublishedFileDetails - pCallResult.m_rgchDescription: " + pCallResult.m_rgchDescription);
					sw.WriteLine(pCallResult.m_nFileSize);
					sw.WriteLine(pCallResult.m_pchFileName);
					sw.WriteLine(pCallResult.m_nPreviewFileSize);
					//NOTE: URL is probably not preview file name. There does not seem to be a way to get preview file name.
					sw.WriteLine(pCallResult.m_rgchURL);
					sw.WriteLine(((Steamworks.ERemoteStoragePublishedFileVisibility)pCallResult.m_eVisibility).ToString("d"));
					sw.WriteLine(pCallResult.m_rgchTags);

					theUGCHandleForContentFile = pCallResult.m_hFile;
					Console.WriteLine("OnGetPublishedFileDetails - pCallResult.m_hFile: " + pCallResult.m_hFile.ToString());
					theUGCHandleForPreviewImageFile = pCallResult.m_hPreviewFile;
				}
				else
				{
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#region SteamRemoteStorage_GetQuota

		private static void SteamRemoteStorage_GetQuota()
		{
			ulong availableBytes = 0;
			ulong totalBytes = 0;

			bool resultIsSuccess = SteamRemoteStorage.GetQuota(out totalBytes, out availableBytes);
			if (resultIsSuccess)
			{
				sw.WriteLine("success");
				sw.WriteLine(availableBytes);
				sw.WriteLine(totalBytes);
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamRemoteStorage_PublishWorkshopFile

		private static void SteamRemoteStorage_PublishWorkshopFile()
		{
			Console.WriteLine("SteamRemoteStorage_PublishWorkshopFile");
			string contentFileName = sr.ReadLine();
			Console.WriteLine("SteamRemoteStorage_PublishWorkshopFile - contentFileName: " + contentFileName);
			string previewFileName = sr.ReadLine();
			string appID_Text = sr.ReadLine();
			AppId_t appID = new AppId_t();
			appID.m_AppId = uint.Parse(appID_Text);
			string title = ReadMultipleLinesOfText(sr);
			string description = ReadMultipleLinesOfText(sr);
			string visibility_text = sr.ReadLine();
			ERemoteStoragePublishedFileVisibility visibility;
			//visibility = CType([Enum].Parse(GetType(ERemoteStoragePublishedFileVisibility), visibility_text), ERemoteStoragePublishedFileVisibility)
			visibility = ConvertVisibilityFromCrowbarToSteamworks(visibility_text);

			string tagCountText = sr.ReadLine();
			int tagCount = int.Parse(tagCountText);
			List<string> tags = new List<string>(tagCount);
			string tag = null;
			for (int i = 0; i < tagCount; i++)
			{
				tag = sr.ReadLine();
				tags.Add(tag);
			}

			// Sending Nothing for the Tags param causes an exception.
			SteamAPICall_t result = SteamRemoteStorage.PublishWorkshopFile(contentFileName, previewFileName, appID, title, description, visibility, tags, EWorkshopFileType.k_EWorkshopFileTypeCommunity);
			//NOTE: Using RemoteStoragePublishFileResult_t even though docs indicate using RemoteStoragePublishFileProgress_t, 
			//      but RemoteStoragePublishFileProgress_t has no return value for the published file ID.
			//      In testing, RemoteStoragePublishFileProgress_t handler is only called once with progress of 0 
			//      and seems to be called right before RemoteStoragePublishFileResult_t handler;
			//      also, RemoteStoragePublishFileResult_t handler gets an error even though item uploaded.
			//      Using RemoteStoragePublishFileResult_t has been tested and it does indeed return the published file ID.
			//CrowbarSteamPipe.SetResultAndRunCallbacks(Of RemoteStoragePublishFileProgress_t)(AddressOf OnPublishFileProgress, result)
			CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStoragePublishFileResult_t>(OnPublishFileResult, result);
		}

		private static void OnPublishFileProgress(RemoteStoragePublishFileProgress_t pCallResult, bool bIOFailure)
		{
			//If pCallResult.m_bPreview Then
			//    Me.UploadResultsTextBox.Text += "PublishFileProgress: Preview is True" + vbCrLf
			//End If
			//sw.WriteLine("Done")

			//theCallResultIsFinished = True

			//======

			Console.WriteLine("OnPublishFileProgress: " + pCallResult.m_dPercentFile.ToString());
			//If pCallResult.m_dPercentFile >= 100 Then
			//	theCallResultIsFinished = True
			//End If
		}

		private static void OnPublishFileResult(RemoteStoragePublishFileResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				Console.WriteLine("OnPublishFileResult");
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					if (pCallResult.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						sw.WriteLine("success_agreement");
					}
					else
					{
						sw.WriteLine("success");
					}
					Console.WriteLine("OnPublishFileResult ItemID: " + pCallResult.m_nPublishedFileId.ToString());
					sw.WriteLine(pCallResult.m_nPublishedFileId.ToString());
				}
				else
				{
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#region SteamRemoteStorage_UGCDownload

		//Private Sub SteamRemoteStorage_UGCDownload()
		//	'Dim fileID As PublishedFileId_t
		//	'Dim fileID_Text As String
		//	'fileID_Text = sr.ReadLine()
		//	'fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

		//	'theCallResultIsFinished = False
		//	'GetPublishedFileDetailsCallResult = CallResult(Of RemoteStorageGetPublishedFileDetailsResult_t).Create(AddressOf OnGetPublishedFileDetailsForInternalResults)
		//	'Dim result As SteamAPICall_t
		//	'result = SteamRemoteStorage.GetPublishedFileDetails(fileID, 0)
		//	'GetPublishedFileDetailsCallResult.Set(result)

		//	'While Not theCallResultIsFinished
		//	'	SteamAPI.RunCallbacks()
		//	'End While

		//	theCallResultIsFinished = False
		//	DownloadUGCCallResult = CallResult(Of RemoteStorageDownloadUGCResult_t).Create(AddressOf OnDownloadUGC)
		//	Dim result As SteamAPICall_t
		//	result = SteamRemoteStorage.UGCDownload(theUGCHandleForPreviewImageFile, 0)
		//	DownloadUGCCallResult.Set(result)

		//	While Not theCallResultIsFinished
		//		SteamAPI.RunCallbacks()
		//	End While
		//End Sub

		//Private Sub OnDownloadUGC(ByVal pCallResult As RemoteStorageDownloadUGCResult_t, ByVal bIOFailure As Boolean)
		//	If pCallResult.m_eResult = EResult.k_EResultOK Then
		//		sw.WriteLine("success")
		//		Console.WriteLine("OnDownloadUGC - file name: " + pCallResult.m_pchFileName)
		//		Console.WriteLine("OnDownloadUGC - byte size: " + pCallResult.m_nSizeInBytes.ToString())


		//		Dim data As Byte() = New Byte(pCallResult.m_nSizeInBytes - 1) {}
		//		'data(0) = 1
		//		'data(1) = 2
		//		'data(2) = 3
		//		Dim byteCountRead As Integer = SteamRemoteStorage.UGCRead(theUGCHandleForPreviewImageFile, data, pCallResult.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished)

		//		'Console.WriteLine("OnDownloadUGC UGCRead - size: " + pCallResult.m_nSizeInBytes.ToString())
		//		'Console.WriteLine("OnDownloadUGC UGCRead - byteCountRead: " + byteCountRead.ToString())
		//		'Console.WriteLine("OnDownloadUGC UGCRead - data size: " + data.Length.ToString())
		//		'Console.WriteLine("OnDownloadUGC UGCRead - data: " + data.ToString())
		//		'Console.WriteLine(data(0))
		//		'Console.WriteLine(data(1))
		//		'Console.WriteLine(data(2))

		//		sw.WriteLine(pCallResult.m_pchFileName)
		//		sw.WriteLine(data.Length.ToString())
		//		sw.BaseStream.Write(data, 0, data.Length)
		//	Else
		//		Console.WriteLine("OnDownloadUGC ERROR: " + pCallResult.m_eResult.ToString())
		//		sw.WriteLine("error")
		//	End If

		//	theCallResultIsFinished = True
		//End Sub

#endregion

#region SteamRemoteStorage_UGCDownloadToLocation

		//Private Sub SteamRemoteStorage_UGCDownloadToLocation()
		//	Dim fileID As PublishedFileId_t
		//	Dim fileID_Text As String
		//	fileID_Text = sr.ReadLine()
		//	fileID.m_PublishedFileId = ULong.Parse(fileID_Text)
		//	Dim fileName As String
		//	fileName = sr.ReadLine()

		//	theCallResultIsFinished = False
		//	GetPublishedFileDetailsCallResult = CallResult(Of RemoteStorageGetPublishedFileDetailsResult_t).Create(AddressOf OnGetPublishedFileDetailsForInternalResults)
		//	Dim result As SteamAPICall_t
		//	result = SteamRemoteStorage.GetPublishedFileDetails(fileID, 0)
		//	GetPublishedFileDetailsCallResult.Set(result)

		//	While Not theCallResultIsFinished
		//		SteamAPI.RunCallbacks()
		//	End While

		//	theCallResultIsFinished = False
		//	DownloadUGCCallResult = CallResult(Of RemoteStorageDownloadUGCResult_t).Create(AddressOf OnDownloadUGC)
		//	result = SteamRemoteStorage.UGCDownloadToLocation(theUGCHandleForPreviewImageFile, fileName, 0)
		//	DownloadUGCCallResult.Set(result)

		//	While Not theCallResultIsFinished
		//		SteamAPI.RunCallbacks()
		//	End While
		//End Sub

#endregion

#region SteamRemoteStorage_UGCRead

		//Private Sub SteamRemoteStorage_UGCRead()
		//	Dim fileID As PublishedFileId_t
		//	Dim fileID_Text As String
		//	fileID_Text = sr.ReadLine()
		//	fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

		//	theCallResultIsFinished = False
		//	GetPublishedFileDetailsCallResult = CallResult(Of RemoteStorageGetPublishedFileDetailsResult_t).Create(AddressOf OnGetPublishedFileDetailsForInternalResults)
		//	Dim result As SteamAPICall_t
		//	result = SteamRemoteStorage.GetPublishedFileDetails(fileID, 0)
		//	GetPublishedFileDetailsCallResult.Set(result)

		//	While Not theCallResultIsFinished
		//		SteamAPI.RunCallbacks()
		//	End While

		//	Dim data As Byte() = New Byte(theUGCPreviewImageFileSize - 1) {}
		//	Dim byteCountRead As Integer = SteamRemoteStorage.UGCRead(theUGCHandleForPreviewImageFile, data, theUGCPreviewImageFileSize, 0, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished)
		//	sw.WriteLine(data.Length.ToString())
		//	sw.BaseStream.Write(data, 0, data.Length)
		//End Sub

#endregion

#region SteamRemoteStorage_UpdatePublishedFileFile

		private static void SteamRemoteStorage_UpdatePublishedFileFile()
		{
			string targetFileName = sr.ReadLine();

			bool setItemContentWasSuccessful = SteamRemoteStorage.UpdatePublishedFileFile(thePublishedFileUpdateHandle, targetFileName);
			if (setItemContentWasSuccessful)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamRemoteStorage_UpdatePublishedFileSetChangeDescription

		private static void SteamRemoteStorage_UpdatePublishedFileSetChangeDescription()
		{
			string changeNote = ReadMultipleLinesOfText(sr);

			bool setItemContentWasSuccessful = SteamRemoteStorage.UpdatePublishedFileSetChangeDescription(thePublishedFileUpdateHandle, changeNote);
			if (setItemContentWasSuccessful)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#endregion

#region SteamUGC

#region SteamUGC_DeleteItem

		private static void SteamUGC_DeleteItem()
		{
			string itemID_text = sr.ReadLine();
			PublishedFileId_t itemID = new PublishedFileId_t();
			itemID.m_PublishedFileId = ulong.Parse(itemID_text);

			SteamAPICall_t result = SteamUGC.DeleteItem(itemID);
			CrowbarSteamPipe.SetResultAndRunCallbacks<DeleteItemResult_t>(OnDeleteItem, result);
		}

		private static void OnDeleteItem(DeleteItemResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					Console.WriteLine("OnDeleteItem - success");
					sw.WriteLine("success");
				}
				else
				{
					Console.WriteLine("OnDeleteItem ERROR: " + pCallResult.m_eResult.ToString());
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#region SteamUGC_DownloadItem

		//'DownloadItem
		//'bool DownloadItem( PublishedFileId_t nPublishedFileID, bool bHighPriority );
		//'Name	Type	Description
		//'nPublishedFileID	PublishedFileId_t	The workshop item to download.
		//'bHighPriority	bool	Start the download in high priority mode, pausing any existing in-progress Steam downloads and immediately begin downloading this workshop item.
		//'
		//'Download or update a workshop item.
		//'
		//'If the return value is true then register and wait for the Callback DownloadItemResult_t before calling GetItemInstallInfo or accessing the workshop item on disk.
		//'
		//'If the user is not subscribed to the item (e.g. a Game Server using anonymous login), the workshop item will be downloaded and cached temporarily.
		//'
		//'If the workshop item has an item state of k_EItemStateNeedsUpdate, then this function can be called to initiate the update. Do not access the workshop item on disk until the Callback DownloadItemResult_t is called.
		//'
		//'NOTE: This method only works with workshop items created with ISteamUGC. It will not work with the legacy ISteamRemoteStorage workshop items.
		//'
		//'The DownloadItemResult_t callback contains the app ID associated with the workshop item. It should be compared against the running app ID as the handler will be called for all item downloads regardless of the running application.
		//'
		//'Returns: bool
		//'Triggers a DownloadItemResult_t callback.
		//'true if the download was successfully started; otherwise, false if nPublishedFileID is invalid or the user is not logged on.
		//Private Sub SteamUGC_DownloadItem()
		//	Dim appID_Text As String
		//	appID_Text = sr.ReadLine()
		//	theAppID.m_AppId = UInteger.Parse(appID_Text)
		//	Dim fileID_Text As String
		//	fileID_Text = sr.ReadLine()
		//	Dim fileID As PublishedFileId_t
		//	fileID.m_PublishedFileId = ULong.Parse(fileID_Text)

		//	Dim resultIsSuccess As Boolean = SteamUGC.DownloadItem(fileID, True)

		//	If resultIsSuccess Then
		//		theCallResultIsFinished = False
		//		DownloadItemCallback = Callback(Of DownloadItemResult_t).Create(AddressOf OnDownloadItem)

		//		While Not theCallResultIsFinished
		//			SteamAPI.RunCallbacks()
		//		End While
		//	End If
		//End Sub

		//Private Sub OnDownloadItem(ByVal pCallback As DownloadItemResult_t)
		//	Try
		//		If pCallback.m_unAppID = theAppID Then
		//			If pCallback.m_eResult = EResult.k_EResultOK Then
		//				Console.WriteLine("OnDownloadItem - success")

		//				theResultMessage = "success"
		//			Else
		//				Console.WriteLine("OnDownloadItem ERROR: " + pCallback.m_eResult.ToString())
		//				theResultMessage = pCallback.m_eResult.ToString()
		//			End If
		//		End If
		//	Catch ex As Exception
		//		Console.WriteLine("OnDownloadItem EXCEPTION: " + ex.Message)
		//		theResultMessage = "OnDownloadItem EXCEPTION: " + ex.Message
		//	End Try

		//	sw.WriteLine(theResultMessage)

		//	theCallResultIsFinished = True
		//End Sub

		//Private DownloadItemCallback As Callback(Of DownloadItemResult_t)

#endregion

#region QueryViaSteamUGC

#region SteamUGC_CreateQueryUGCDetailsRequest

		private static void SteamUGC_CreateQueryUGCDetailsRequest()
		{
			string itemID_Text = sr.ReadLine();
			PublishedFileId_t itemID = new PublishedFileId_t();
			itemID.m_PublishedFileId = ulong.Parse(itemID_Text);
			PublishedFileId_t[] publishedFileIDList = {itemID};

			theUGCQueryHandle = SteamUGC.CreateQueryUGCDetailsRequest(publishedFileIDList, 1);
			if (theUGCQueryHandle != UGCQueryHandle_t.Invalid)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamUGC_CreateQueryUserUGCRequest

		private static void SteamUGC_CreateQueryUserUGCRequest()
		{
			string appID_Text = sr.ReadLine();
			AppId_t appID = new AppId_t();
			appID.m_AppId = uint.Parse(appID_Text);
			string pageNumberText = sr.ReadLine();
			uint pageNumber = uint.Parse(pageNumberText);

			//#If DEBUG Then
			//		'NOTE: Use account that has over 750 items in L4D2 workshop.
			//		Dim steamID As CSteamID = New CSteamID(76561198006306928)
			//#Else
			CSteamID steamID = SteamUser.GetSteamID();
			//#End If

			AccountID_t accountID = steamID.GetAccountID();
			Console.WriteLine("steamID: " + steamID.ToString());
			Console.WriteLine("accountID: " + accountID.ToString());
			Console.WriteLine("appID: " + appID.m_AppId.ToString());

			//NOTE: Use this invalid AppID to get all addons for the game instead of just ones created by a separate tool or just ones by the game.
			AppId_t nullCreatorAppID = new AppId_t(0);

			Console.WriteLine("SteamUGC_CreateQueryUserUGCRequest - pageNumber: " + pageNumber.ToString());
			theUGCQueryHandle = SteamUGC.CreateQueryUserUGCRequest(accountID, EUserUGCList.k_EUserUGCList_Published, EUGCMatchingUGCType.k_EUGCMatchingUGCType_Items_ReadyToUse, EUserUGCListSortOrder.k_EUserUGCListSortOrder_LastUpdatedDesc, nullCreatorAppID, appID, pageNumber);

			if (theUGCQueryHandle != UGCQueryHandle_t.Invalid)
			{
				//Console.WriteLine("SteamUGC_CreateQueryUserUGCRequest - good UGCQueryHandle_t")
				sw.WriteLine("success");

				// Disable getting unwanted fields.
				//ISteamUGC: SetReturnKeyValueTags -Sets whether to return any key-value tags for the items on a pending UGC Query.
				//ISteamUGC: SetReturnLongDescription -Sets whether to return the full description for the items on a pending UGC Query.
				//ISteamUGC: SetReturnMetadata -Sets whether to return the developer specified metadata for the items on a pending UGC Query.
				//ISteamUGC: SetReturnChildren -Sets whether to return the IDs of the child items of the items on a pending UGC Query.
				//ISteamUGC: SetReturnAdditionalPreviews -Sets whether to return any additional images/videos attached to the items on a pending UGC Query.
				//Console.WriteLine("SteamUGC_CreateQueryUserUGCRequest - Disable some fields.")
				SteamUGC.SetReturnKeyValueTags(theUGCQueryHandle, false);
				SteamUGC.SetReturnLongDescription(theUGCQueryHandle, false);
				//SteamUGC.SetReturnLongDescription(theUGCQueryHandle, True)
				SteamUGC.SetReturnMetadata(theUGCQueryHandle, false);
				SteamUGC.SetReturnChildren(theUGCQueryHandle, false);
				SteamUGC.SetReturnAdditionalPreviews(theUGCQueryHandle, false);
			}
			else
			{
				//Console.WriteLine("SteamUGC_CreateQueryUserUGCRequest - invalid UGCQueryHandle_t")
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamUGC_SendQueryUGCRequest

		private static void SteamUGC_SendQueryUGCRequest()
		{
			SteamAPICall_t result = SteamUGC.SendQueryUGCRequest(theUGCQueryHandle);
			CrowbarSteamPipe.SetResultAndRunCallbacks<SteamUGCQueryCompleted_t>(OnSteamUGCQueryCompleted, result);

			Console.WriteLine("SteamUGC_ReleaseQueryUGCRequest");
			SteamUGC.ReleaseQueryUGCRequest(theUGCQueryHandle);
		}

		//NOTE: Will return max of kNumUGCResultsPerPage pages. Steamworks.Constants.kNumUGCResultsPerPage
		//m_eResult   EResult	The result of the operation.
		//m_unNumResultsReturned  UInt32	The number of items returned.
		//m_unTotalMatchingResults    UInt32	The total number of items that matched the query.
		private static void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					sw.WriteLine("success");
					sw.WriteLine(pCallResult.m_unNumResultsReturned.ToString());
					if (pCallResult.m_unNumResultsReturned > 0)
					{
						sw.WriteLine(pCallResult.m_unTotalMatchingResults.ToString());
						Console.WriteLine("OnSteamUGCQueryCompleted (" + pCallResult.m_unNumResultsReturned.ToString() + "):");

						bool queryResult = false;
						SteamUGCDetails_t itemDetails = new SteamUGCDetails_t();
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of (uint)(pCallResult.m_unNumResultsReturned - 1) for every iteration:
						uint tempVar = (uint)(pCallResult.m_unNumResultsReturned - 1);
						for (uint resultItemIndex = 0; resultItemIndex <= tempVar; resultItemIndex++)
						{
							queryResult = SteamUGC.GetQueryUGCResult(theUGCQueryHandle, resultItemIndex, out itemDetails);

							//'NOTE: Only send items that the user created.
							//Dim steamID As CSteamID = SteamUser.GetSteamID()
							//If steamID.m_SteamID = itemDetails.m_ulSteamIDOwner Then
							Console.WriteLine("OnSteamUGCQueryCompleted ItemID: " + itemDetails.m_nPublishedFileId.ToString());
							sw.WriteLine(itemDetails.m_nPublishedFileId);
							sw.WriteLine(itemDetails.m_ulSteamIDOwner);
							sw.WriteLine(SteamFriends.GetFriendPersonaName(new CSteamID(itemDetails.m_ulSteamIDOwner)));
							sw.WriteLine(itemDetails.m_rtimeCreated);
							sw.WriteLine(itemDetails.m_rtimeUpdated);

							WriteTextThatMightHaveMultipleLines(itemDetails.m_rgchTitle);
							//WriteTextThatMightHaveMultipleLines(itemDetails.m_rgchDescription)

							sw.WriteLine(itemDetails.m_pchFileName);
							sw.WriteLine(itemDetails.m_hPreviewFile);
							sw.WriteLine(((Steamworks.ERemoteStoragePublishedFileVisibility)itemDetails.m_eVisibility).ToString("d"));
							sw.WriteLine(itemDetails.m_rgchTags);

							//Console.WriteLine("  " + resultItemIndex.ToString())
							//Console.WriteLine("    OwnerName: " + SteamFriends.GetFriendPersonaName(New CSteamID(itemDetails.m_ulSteamIDOwner)))
							//Console.WriteLine("    Created: " + itemDetails.m_rtimeCreated.ToString())
							//Console.WriteLine("    Updated: " + itemDetails.m_rtimeUpdated.ToString())
							//Console.WriteLine("    Title: " + itemDetails.m_rgchTitle)
							//Console.WriteLine("    Description: " + itemDetails.m_rgchDescription)
							//Console.WriteLine("    " + itemDetails.m_rgchTags)
							//Console.WriteLine("    " + itemDetails.m_rgchURL)
							//Console.WriteLine("    m_hFile: " + itemDetails.m_hFile.ToString())

							//queryResult = SteamUGC.GetQueryUGCPreviewURL(theUGCQueryHandle, resultItemIndex, itemDetails)

							// Test use of meta data. Need to change SteamUGC.SetReturnMetadata() second param from False to True.
							//Dim metaData As String = ""
							//Dim metaDataSize As UInteger
							//queryResult = SteamUGC.GetQueryUGCMetadata(theUGCQueryHandle, resultItemIndex, metaData, metaDataSize)
							//Console.WriteLine("    metaDataSize: " + metaDataSize.ToString() + " END")
							//Console.WriteLine("    metaData(" + metaData.Length().ToString() + "): " + metaData + " END")
						}
					}
				}
				else
				{
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#endregion

#region PublishViaSteamUGC

#region SteamUGC_CreateItem

		private static void SteamUGC_CreateItem()
		{
			string appID_Text = sr.ReadLine();
			AppId_t appID = new AppId_t();
			appID.m_AppId = uint.Parse(appID_Text);

			SteamAPICall_t result = SteamUGC.CreateItem(appID, EWorkshopFileType.k_EWorkshopFileTypeCommunity);
			CrowbarSteamPipe.SetResultAndRunCallbacks<CreateItemResult_t>(OnCreateItem, result);
		}

		//m_eResult	EResult	The result of the operation. Some of the possible return values include:
		//    k_EResultOK - The operation completed successfully.
		//    k_EResultInsufficientPrivilege - The user creating the item is currently banned in the community.
		//    k_EResultTimeout - The operation took longer than expected, have the user retry the create process.
		//    k_EResultNotLoggedOn - The user is not currently logged into Steam.
		//m_nPublishedFileId	PublishedFileId_t	The new items unique ID.
		//m_bUserNeedsToAcceptWorkshopLegalAgreement	bool	Does the user need to accept the Steam Workshop legal agreement (true) or not (false)? See the Workshop Legal Agreement for more information.
		private static void OnCreateItem(CreateItemResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					if (pCallResult.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						sw.WriteLine("success_agreement");
					}
					else
					{
						sw.WriteLine("success");
					}
					sw.WriteLine(pCallResult.m_nPublishedFileId.ToString());
				}
				else
				{
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("EXCEPTION: " + ex.Message);
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

		private static void SteamUGC_GetItemUpdateProgress()
		{
			ulong uploadedByteCount = 0;
			ulong totalUploadedByteCount = 0;

			EItemUpdateStatus status = SteamUGC.GetItemUpdateProgress(theUGCUpdateHandle, out uploadedByteCount, out totalUploadedByteCount);

			//k_EItemUpdateStatusInvalid	0	The item update handle was invalid, the job might be finished, a SubmitItemUpdateResult_t call result should have been returned for it.
			//k_EItemUpdateStatusPreparingConfig	1	The item update is processing configuration data.
			//k_EItemUpdateStatusPreparingContent	2	The item update is reading and processing content files.
			//k_EItemUpdateStatusUploadingContent	3	The item update is uploading content changes to Steam.
			//k_EItemUpdateStatusUploadingPreviewFile	4	The item update is uploading new preview file image.
			//k_EItemUpdateStatusCommittingChanges	5	The item update is committing all changes.
			if (status == EItemUpdateStatus.k_EItemUpdateStatusPreparingConfig)
			{
				//           Console.WriteLine("Preparing config")
				sw.WriteLine("Preparing config");
			}
			else if (status == EItemUpdateStatus.k_EItemUpdateStatusPreparingContent)
			{
				//           Console.WriteLine("Preparing content")
				sw.WriteLine("Preparing content");
			}
			else if (status == EItemUpdateStatus.k_EItemUpdateStatusUploadingContent)
			{
				//           Console.WriteLine("Uploading content")
				sw.WriteLine("Uploading content");
				if (totalUploadedByteCount > 0 && uploadedByteCount == totalUploadedByteCount)
				{
					theItemIsUploading = false;
				}
			}
			else if (status == EItemUpdateStatus.k_EItemUpdateStatusUploadingPreviewFile)
			{
				//           Console.WriteLine("Uploading preview")
				sw.WriteLine("Uploading preview");
			}
			else if (status == EItemUpdateStatus.k_EItemUpdateStatusCommittingChanges)
			{
				//           Console.WriteLine("Committing changes")
				sw.WriteLine("Committing changes");
			}
			else
			{
				//           Console.WriteLine("invalid")
				sw.WriteLine("invalid");
				theItemIsUploading = false;
			}
			sw.WriteLine(uploadedByteCount);
			sw.WriteLine(totalUploadedByteCount);
		}

#region SteamUGC_StartItemUpdate

		private static void SteamUGC_StartItemUpdate()
		{
			string appID_Text = sr.ReadLine();
			AppId_t appID = new AppId_t();
			appID.m_AppId = uint.Parse(appID_Text);
			string itemID_Text = sr.ReadLine();
			PublishedFileId_t itemID = new PublishedFileId_t();
			itemID.m_PublishedFileId = ulong.Parse(itemID_Text);

			theUGCUpdateHandle = SteamUGC.StartItemUpdate(appID, itemID);

			if (theUGCUpdateHandle != UGCUpdateHandle_t.Invalid)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamUGC_SetItem Details

		private static void SteamUGC_SetItemUpdateLanguage()
		{
			string language = sr.ReadLine();

			bool resultIsSuccess = SteamUGC.SetItemUpdateLanguage(theUGCUpdateHandle, language);

			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

		private static void SteamUGC_SetItemTitle()
		{
			string titleText = ReadMultipleLinesOfText(sr);

			bool resultIsSuccess = SteamUGC.SetItemTitle(theUGCUpdateHandle, titleText);

			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

		private static void SteamUGC_SetItemDescription()
		{
			string descriptionText = ReadMultipleLinesOfText(sr);

			bool resultIsSuccess = SteamUGC.SetItemDescription(theUGCUpdateHandle, descriptionText);

			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

		private static void SteamUGC_SetItemContent()
		{
			string contentPathFileName = sr.ReadLine();

			bool resultIsSuccess = SteamUGC.SetItemContent(theUGCUpdateHandle, contentPathFileName);

			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

		private static void SteamUGC_SetItemPreview()
		{
			string previewPathFileName = sr.ReadLine();

			bool resultIsSuccess = SteamUGC.SetItemPreview(theUGCUpdateHandle, previewPathFileName);

			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

		private static void SteamUGC_SetItemVisibility()
		{
			string visibility_text = sr.ReadLine();
			ERemoteStoragePublishedFileVisibility visibility;
			//visibility = CType([Enum].Parse(GetType(ERemoteStoragePublishedFileVisibility), visibility_text), ERemoteStoragePublishedFileVisibility)
			visibility = ConvertVisibilityFromCrowbarToSteamworks(visibility_text);

			bool resultIsSuccess = SteamUGC.SetItemVisibility(theUGCUpdateHandle, visibility);

			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

		private static void SteamUGC_SetItemTags()
		{
			string tagCountText = sr.ReadLine();
			int tagCount = int.Parse(tagCountText);
			List<string> tags = new List<string>(tagCount);
			string tag = null;
			for (int i = 0; i < tagCount; i++)
			{
				tag = sr.ReadLine();
				tags.Add(tag);
			}

			bool resultIsSuccess = SteamUGC.SetItemTags(theUGCUpdateHandle, tags);

			if (resultIsSuccess)
			{
				sw.WriteLine("success");
			}
			else
			{
				sw.WriteLine("error");
			}
		}

#endregion

#region SteamUGC_SubmitItemUpdate

		private static void SteamUGC_SubmitItemUpdate()
		{
			string changeNote = ReadMultipleLinesOfText(sr);

			theItemIsUploading = true;

			SteamAPICall_t result = SteamUGC.SubmitItemUpdate(theUGCUpdateHandle, changeNote);
			CrowbarSteamPipe.SetResultAndRunCallbacks<SubmitItemUpdateResult_t>(OnSubmitItemUpdate, result);
		}

		private static void OnSubmitItemUpdate(SubmitItemUpdateResult_t pCallResult, bool bIOFailure)
		{
			theItemIsUploading = false;
			//Console.WriteLine("OnSubmitItemUpdate")
			sw.WriteLine("OnSubmitItemUpdate");
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					if (pCallResult.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						sw.WriteLine("success_agreement");
					}
					else
					{
						//Console.WriteLine("OnSubmitItemUpdate success")
						sw.WriteLine("success");
					}
				}
				else
				{
					//Console.WriteLine("OnSubmitItemUpdate " + GetErrorMessage(pCallResult.m_eResult))
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
				//Console.WriteLine("OnSubmitItemUpdate result: " + pCallResult.m_eResult.ToString())
			}
			catch (Exception ex)
			{
				//Console.WriteLine("OnSubmitItemUpdate EXCEPTION: " + ex.Message)
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#region SteamUGC_UnsubscribeItem

		private static void SteamUGC_UnsubscribeItem()
		{
			string itemID_Text = sr.ReadLine();
			PublishedFileId_t itemID = new PublishedFileId_t();
			itemID.m_PublishedFileId = ulong.Parse(itemID_Text);

			SteamAPICall_t result = SteamUGC.UnsubscribeItem(itemID);
			CrowbarSteamPipe.SetResultAndRunCallbacks<RemoteStorageUnsubscribePublishedFileResult_t>(OnUnsubscribeItem, result);
		}

		private static void OnUnsubscribeItem(RemoteStorageUnsubscribePublishedFileResult_t pCallResult, bool bIOFailure)
		{
			try
			{
				if (pCallResult.m_eResult == EResult.k_EResultOK)
				{
					sw.WriteLine("success");
				}
				else
				{
					sw.WriteLine(GetErrorMessage(pCallResult.m_eResult));
				}
				Console.WriteLine("OnUnsubscribeItem result: " + pCallResult.m_eResult.ToString());
			}
			catch (Exception ex)
			{
				Console.WriteLine("OnUnsubscribeItem EXCEPTION: " + ex.Message);
				sw.WriteLine("EXCEPTION: " + ex.Message);
			}

			theCallResultIsFinished = true;
		}

#endregion

#endregion

#endregion

#region SteamUser

		private static void SteamUser_GetSteamID()
		{
			CSteamID steamID = SteamUser.GetSteamID();
			sw.WriteLine(steamID.ToString());
		}

#endregion

#region Private Functions

		private static Steamworks.ERemoteStoragePublishedFileVisibility ConvertVisibilityFromCrowbarToSteamworks(string input)
		{
			Steamworks.ERemoteStoragePublishedFileVisibility output = 0;
			if (input == "Public")
			{
				output = Steamworks.ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic;
			}
			else if (input == "FriendsOnly")
			{
				output = Steamworks.ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityFriendsOnly;
			}
			else if (input == "Unlisted")
			{
				output = Steamworks.ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityUnlisted;
			}
			else
			{
				output = Steamworks.ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate;
			}
			return output;
		}

		private static void SetResultAndRunCallbacks<T>(CallResult<T>.APIDispatchDelegate func, SteamAPICall_t result)
		{
			CallResult<T> aCallResult = CallResult<T>.Create(func);
			aCallResult.Set(result);
			CrowbarSteamPipe.RunCallbacks();
		}

		private static void RunCallbacks()
		{
			theCallResultIsFinished = false;
			while (!theCallResultIsFinished)
			{
				if (theItemIsUploading)
				{
					SteamUGC_GetItemUpdateProgress();
				}
				SteamAPI.RunCallbacks();
			}
		}

		//NOTE: WriteLine only writes string until first LF or CR, so need to adjust how to send 
		//NOTE: From TextReader.ReadLine: A line is defined as a sequence of characters followed by 
		//      a carriage return (0x000d), a line feed (0x000a), a carriage return followed by a line feed, Environment.NewLine, or the end-of-stream marker.
		//      https://docs.microsoft.com/en-us/dotnet/api/system.io.textreader.readline?view=netframework-4.0
		private static void WriteTextThatMightHaveMultipleLines(string text)
		{
			text = ConvertText(text);

			//NOTE: Delete all CR in text because they are not needed and will show as blank characters in Windows TextBox.
			text = text.Replace("\r", "");
			string[] stringSeparators = {"\n"};
			string[] textLines_array = text.Split(stringSeparators, StringSplitOptions.None);
			List<string> textLines = new List<string>(textLines_array);
			//NOTE: Delete last line if empty because it is always added to what user actually published.
			if (string.IsNullOrEmpty(textLines[textLines.Count - 1]))
			{
				textLines.RemoveAt(textLines.Count - 1);
			}
			sw.WriteLine(textLines.Count);
			//Console.WriteLine("    Line count: " + textLines.Count.ToString())
			int i = 1;
			foreach (string line in textLines)
			{
				sw.WriteLine(line);
				//Console.WriteLine("    Line " + i.ToString() + ": " + line)
				i += 1;
			}
		}

		public static string ConvertText(string strData)
		{
			byte[] bytes = System.Text.Encoding.Default.GetBytes(strData);
			return System.Text.Encoding.UTF8.GetString(bytes);
		}

		private static string ReadMultipleLinesOfText(StreamReader sr)
		{
			string text = "";

			int textLineCount = int.Parse(sr.ReadLine());
			for (int i = 0; i < textLineCount; i++)
			{
				//NOTE: Add LF because web page needs them for newlines.
				text += sr.ReadLine() + "\n";
			}
			//Console.WriteLine("ReadMultipleLinesOfText: " + text)

			return text;
		}

		//k_EResultOK -The operation completed successfully.
		//k_EResultFail -Generic failure.
		//k_EResultInvalidParam -Either the provided app ID Is invalid Or doesn't match the consumer app ID of the item or, you have not enabled ISteamUGC for the provided app ID on the Steam Workshop Configuration App Admin page.
		//    The preview file Is smaller than 16 bytes.
		//k_EResultAccessDenied -The user doesn't own a license for the provided app ID.
		//k_EResultFileNotFound -Failed to get the workshop info for the item Or failed to read the preview file.
		//k_EResultLockingFailed -Failed to aquire UGC Lock.
		//k_EResultFileNotFound -The provided content folder Is Not valid.
		//k_EResultLimitExceeded -The preview image Is too large, it must be less than 1 Megabyte; Or there Is Not enough space available on the users Steam Cloud.
		private static string GetErrorMessage(EResult steamErrorResult)
		{
			string errorMessage = "ERROR: ";

			if (steamErrorResult == EResult.k_EResultAccessDenied)
			{
				//sw.WriteLine("Access denied. The AppID in the steam_api.txt file is incorrect or the steam_api.txt file is not beside the CrowbarSteamPipe.exe file.")
				errorMessage += "Access denied. The user's Steam account does not own a license for the provided App ID.";
			}
			else if (steamErrorResult == EResult.k_EResultFileNotFound)
			{
				//errorMessage += "File not found. The provided content folder, content file, or preview image file is invalid."
				errorMessage += "File not found. The provided content folder, content file, or preview image file is invalid.";
			}
			else if (steamErrorResult == EResult.k_EResultInsufficientPrivilege)
			{
				errorMessage += "Insufficient privilege. The user's Steam account is currently restricted from uploading content due to a hub ban, account lock, or community ban.";
			}
			else if (steamErrorResult == EResult.k_EResultInvalidParam)
			{
				errorMessage += "Invalid paramater. Content file too big, invalid App ID, or the preview file is smaller than 16 bytes.";
			}
			else if (steamErrorResult == EResult.k_EResultLimitExceeded)
			{
				errorMessage += "Limit exceeded. The preview image is too large, it must be less than 1 megabyte; or there is not enough space available on the user's Steam Cloud.";
			}
			else if (steamErrorResult == EResult.k_EResultLockingFailed)
			{
				errorMessage += "Locking failed. Failed to aquire UGC Lock.";
			}
			else if (steamErrorResult == EResult.k_EResultNotLoggedOn)
			{
				errorMessage += "Not logged on. The user's Steam account is not currently logged in.";
			}
			else if (steamErrorResult == EResult.k_EResultTimeout)
			{
				errorMessage += "Timeout. Action timed-out and did not complete.";
			}
			else
			{
				errorMessage += steamErrorResult.ToString();
			}

			return errorMessage;
		}

		private static string GetItemStateText(Steamworks.EItemState itemState)
		{
			string itemStateText = "";

			if (itemState == Steamworks.EItemState.k_EItemStateNone)
			{
				itemStateText += " k_EItemStateNone";
			}
			else
			{
				if ((itemState & Steamworks.EItemState.k_EItemStateDownloading) != 0)
				{
					itemStateText += " k_EItemStateDownloading";
				}
				if ((itemState & Steamworks.EItemState.k_EItemStateDownloadPending) != 0)
				{
					itemStateText += " k_EItemStateDownloadPending";
				}
				if ((itemState & Steamworks.EItemState.k_EItemStateInstalled) != 0)
				{
					itemStateText += " k_EItemStateInstalled";
				}
				if ((itemState & Steamworks.EItemState.k_EItemStateLegacyItem) != 0)
				{
					itemStateText += " k_EItemStateLegacyItem";
				}
				if ((itemState & Steamworks.EItemState.k_EItemStateNeedsUpdate) != 0)
				{
					itemStateText += " k_EItemStateNeedsUpdate";
				}
				if ((itemState & Steamworks.EItemState.k_EItemStateSubscribed) != 0)
				{
					itemStateText += " k_EItemStateSubscribed";
				}
			}
			itemStateText = itemStateText.TrimStart();

			return itemStateText;
		}

#endregion

#region Data

		private static StreamWriter sw;
		private static StreamReader sr;

		private static bool theItemIsUploading;
		private static bool theCallResultIsFinished;
		private static UGCQueryHandle_t theUGCQueryHandle;
		private static UGCUpdateHandle_t theUGCUpdateHandle;

		private static PublishedFileUpdateHandle_t thePublishedFileUpdateHandle;
		private static UGCHandle_t theUGCHandleForContentFile;
		private static UGCHandle_t theUGCHandleForPreviewImageFile;
		//Private theUGCPreviewImageFileSize As Integer

		private static AppId_t theAppID;
		private static PublishedFileId_t theItemID;
		private static uint theItemUpdated;
		private static string theItemTitle;
		private static string theItemContentFileName;

		private static string theResultMessage;

#endregion

	}

}