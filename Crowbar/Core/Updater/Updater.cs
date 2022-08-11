using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Xml;

namespace Crowbar
{
	public class Updater
	{

#region Creation and Destruction

		public Updater() : base()
		{

		}

#endregion

#region CheckForUpdate

		public class StatusOutputInfo
		{
			public string StatusMessage;
			public bool UpdateIsAvailable;
			public bool DownloadIsEnabled;
			public bool UpdateIsEnabled;
		}

		public void CheckForUpdate(ProgressChangedEventHandler given_ProgressChanged, RunWorkerCompletedEventHandler given_RunWorkerCompleted)
		{
			theDownloadTaskIsEnabled = false;
			theCheckForUpdateBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theCheckForUpdateBackgroundWorker, CheckForUpdate_DoWork, given_ProgressChanged, given_RunWorkerCompleted, null);
		}

		public void CancelCheckForUpdate()
		{
			if (theCheckForUpdateBackgroundWorker != null && theCheckForUpdateBackgroundWorker.IsBusy)
			{
				theCheckForUpdateBackgroundWorker.CancelAsync();
			}
		}

		private BackgroundWorkerEx theCheckForUpdateBackgroundWorker;

		//NOTE: This is run in a background thread.
		private void CheckForUpdate_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			BackgroundWorkerEx bw = (BackgroundWorkerEx)sender;

			theAppVersion = null;
			ulong fileSize = 0;
			bool securityProtocolIsSupported = true;

			//FROM: https://www.codeproject.com/Questions/1255767/Could-not-create-SSL-TLS-secure-channel
			//FROM: https://blogs.perficient.com/2016/04/28/tsl-1-2-and-net-support/
			//      .NET 4.0 does not support TLS 1.2 and does not have an SecurityProtocolType Enum value for TLS1.2.
			//      To use in .NET 4.0, use the number: CType(3072, SecurityProtocolType)
			//      [Not sure] Need .NET 4.5 (or above) installed.
			//NOTE: GitHub API requires 
			try
			{
				ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
			}
			catch (NotSupportedException ex)
			{
				securityProtocolIsSupported = false;
			}

			if (securityProtocolIsSupported)
			{
				HttpWebRequest request = null;
				try
				{
					// Get data from latest release page via GitHub API.
					//FROM: https://developer.github.com/v3/repos/releases
					//      All API access is over HTTPS, and accessed from https://api.github.com. All data is sent and received as JSON.
					//FROM: https://developer.github.com/v3/repos/releases/#get-the-latest-release
					//      Get the latest release: https://api.github.com/repos/ZeqMacaw/Crowbar/releases/latest
					request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/ZeqMacaw/Crowbar/releases/latest");
					request.Method = "GET";
					//NOTE: GitHub API suggests using something like 
					request.UserAgent = "ZeqMacaw_Crowbar";
				}
				catch (Exception ex)
				{
					request = null;
				}

				if (request != null)
				{
					HttpWebResponse response = null;
					Stream dataStream = null;
					StreamReader reader = null;
					string remoteFileLink = "";
					string localFileName = "";
					try
					{
						response = (HttpWebResponse)request.GetResponse();
						dataStream = response.GetResponseStream();
						reader = new StreamReader(dataStream);
						string responseFromServer = reader.ReadToEnd();

						JavaScriptSerializer jss = new JavaScriptSerializer();
						Dictionary<string, object> root = jss.Deserialize<Dictionary<string, object>>(responseFromServer);

						string appVersionTag = (root["tag_name"] == null ? null : Convert.ToString(root["tag_name"]));
						if (!string.IsNullOrEmpty(appVersionTag))
						{
							if (appVersionTag[0] == 'v')
							{
								appVersionTag = appVersionTag.Remove(0, 1);
							}
							//NOTE: Must append ".0.0" to version so that Version comparisons are correct.
							string appVersionText = appVersionTag + ".0.0";
							theAppVersion = new Version(appVersionText);

							//Dim appVersionIsNewer As Boolean = appVersion > My.Application.Info.Version
							//Dim appVersionIsOlder As Boolean = appVersion < My.Application.Info.Version
							//Dim appVersionIsEqual As Boolean = appVersion = My.Application.Info.Version

							bw.ReportProgress(0, "Crowbar " + appVersionTag + "\r\n" + (root["body"] == null ? null : Convert.ToString(root["body"])));
						}
						else
						{
							theAppVersion = null;
						}

						//NOTE: File name needs to be in this form: "Crowbar_" + whatever; usually date + "_" + app version (e.g. 0.68) + ".7z"
						theRemoteFileLink = "";
						ArrayList assets = (ArrayList)root["assets"];
						Dictionary<string, object> asset = null;
						string assetName = null;
						for (int assetIndex = 0; assetIndex < assets.Count; assetIndex++)
						{
							asset = (Dictionary<string, object>)assets[assetIndex];
							assetName = (asset["name"] == null ? null : Convert.ToString(asset["name"]));
							if (assetName.StartsWith("Crowbar_") && assetName.EndsWith("_" + appVersionTag + ".7z"))
							{
								theRemoteFileLink = (asset["browser_download_url"] == null ? null : Convert.ToString(asset["browser_download_url"]));
								theLocalFileName = (asset["name"] == null ? null : Convert.ToString(asset["name"]));
								fileSize = Convert.ToUInt64(asset["size"]);
								break;
							}
						}
					}
					catch (Exception ex)
					{
						theAppVersion = null;
					}
					finally
					{
						if (reader != null)
						{
							reader.Close();
						}
						if (response != null)
						{
							response.Close();
						}
					}
				}
			}

			Updater.StatusOutputInfo outputInfo = new Updater.StatusOutputInfo();
			outputInfo.UpdateIsAvailable = false;
			string updateCheckStatusMessage = null;
			if (!securityProtocolIsSupported)
			{
				updateCheckStatusMessage = "Unable to get update info because \"TLS 1.2\" protocol unavailable.";
			}
			else if (theAppVersion == null || string.IsNullOrEmpty(theRemoteFileLink))
			{
				updateCheckStatusMessage = "Unable to get update info. Please try again later.";
			}
			else if (theAppVersion == ConversionHelper.Version)
			{
				updateCheckStatusMessage = "Crowbar is up to date.";
			}
			else if (theAppVersion > ConversionHelper.Version)
			{
				updateCheckStatusMessage = "Update to version " + theAppVersion.ToString(2) + " available.   Size: " + MathModule.ByteUnitsConversion(fileSize);
				outputInfo.UpdateIsAvailable = true;
			}
			else
			{
				//NOTE: Should not get here if versioning is done correctly.
				updateCheckStatusMessage = "Crowbar is up to date.";
			}
			DateTime now = DateTime.Now;
			string lastCheckedMessage = "   Last checked: " + now.ToLongDateString() + " " + now.ToShortTimeString();

			outputInfo.StatusMessage = updateCheckStatusMessage + lastCheckedMessage;
			outputInfo.DownloadIsEnabled = theDownloadTaskIsEnabled;
			outputInfo.UpdateIsEnabled = theUpdateTaskIsEnabled;
			e.Result = outputInfo;
		}

#endregion

#region Download

		public void Download(ProgressChangedEventHandler checkForUpdate_ProgressChanged, RunWorkerCompletedEventHandler checkForUpdate_RunWorkerCompleted, DownloadProgressChangedEventHandler download_DownloadProgressChanged, AsyncCompletedEventHandler download_DownloadFileCompleted, string localPath)
		{
			theDownloadProgressChangedHandler = download_DownloadProgressChanged;
			theDownloadFileCompletedHandler = download_DownloadFileCompleted;
			theLocalPath = localPath;
			theDownloadTaskIsEnabled = true;
			theUpdateTaskIsEnabled = false;
			theCheckForUpdateRunWorkerCompletedHandler = checkForUpdate_RunWorkerCompleted;
			theCheckForUpdateBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theCheckForUpdateBackgroundWorker, CheckForUpdate_DoWork, checkForUpdate_ProgressChanged, CheckForUpdate_RunWorkerCompleted, null);
		}

		public void CancelDownload()
		{
			CancelCheckForUpdate();
			if (theWebClient != null && theWebClient.IsBusy)
			{
				theWebClient.CancelAsync();
			}
		}

		private void DownloadAfterCheckForUpdate()
		{
			if (theUpdateTaskIsEnabled && theAppVersion <= ConversionHelper.Version)
			{
				return;
			}

			theLocalPathFileName = Path.Combine(theLocalPath, theLocalFileName);
			theLocalPathFileName = FileManager.GetTestedPathFileName(theLocalPathFileName);

			if (FileManager.PathExistsAfterTryToCreate(theLocalPath))
			{
				Uri remoteFileUri = new Uri(theRemoteFileLink);

				theWebClient = new WebClient();
				theWebClient.DownloadProgressChanged += theDownloadProgressChangedHandler;
				//AddHandler Me.theWebClient.DownloadFileCompleted, Me.theDownloadFileCompletedHandler
				theWebClient.DownloadFileCompleted += Download_DownloadFileCompleted;
				theWebClient.DownloadFileAsync(remoteFileUri, theLocalPathFileName, theLocalPathFileName);
			}
		}

#endregion

#region Update

		public void Update(ProgressChangedEventHandler checkForUpdate_ProgressChanged, RunWorkerCompletedEventHandler checkForUpdate_RunWorkerCompleted, DownloadProgressChangedEventHandler download_DownloadProgressChanged, AsyncCompletedEventHandler download_DownloadFileCompleted, string localPath, ProgressChangedEventHandler update_ProgressChanged, RunWorkerCompletedEventHandler update_RunWorkerCompleted)
		{
			theDownloadProgressChangedHandler = download_DownloadProgressChanged;
			theDownloadFileCompletedHandler = download_DownloadFileCompleted;
			theUpdateProgressChangedHandler = update_ProgressChanged;
			theUpdateRunWorkerCompletedHandler = update_RunWorkerCompleted;
			theLocalPath = localPath;
			theDownloadTaskIsEnabled = true;
			theUpdateTaskIsEnabled = true;
			theCheckForUpdateRunWorkerCompletedHandler = checkForUpdate_RunWorkerCompleted;
			theCheckForUpdateBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theCheckForUpdateBackgroundWorker, CheckForUpdate_DoWork, checkForUpdate_ProgressChanged, CheckForUpdate_RunWorkerCompleted, null);
		}

		public void CancelUpdate()
		{
			CancelCheckForUpdate();
			CancelDownload();
			if (theUpdateBackgroundWorker != null && theUpdateBackgroundWorker.IsBusy)
			{
				theUpdateBackgroundWorker.CancelAsync();
			}
		}

		private BackgroundWorkerEx theUpdateBackgroundWorker;

		private void UpdateAfterDownload()
		{
			theUpdateBackgroundWorker = BackgroundWorkerEx.RunBackgroundWorker(theUpdateBackgroundWorker, Update_DoWork, theUpdateProgressChangedHandler, theUpdateRunWorkerCompletedHandler, null);
		}

		//NOTE: This is run in a background thread.
		private void Update_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			MainCROWBAR.TheApp.WriteUpdaterFiles();

			string currentFolder = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(theLocalPath);

			Decompress();
			OpenNewVersion();

			Directory.SetCurrentDirectory(currentFolder);

			MainCROWBAR.TheApp.DeleteUpdaterFiles();
		}

		//NOTE: This is run in a background thread.
		private void Decompress()
		{
			Process sevenZrExeProcess = new Process();
			try
			{
				sevenZrExeProcess.StartInfo.UseShellExecute = false;
				//NOTE: From Microsoft website: 
				//      On Windows Vista and earlier versions of the Windows operating system, 
				//      the length of the arguments added to the length of the full path to the process must be less than 2080. 
				//      On Windows 7 and later versions, the length must be less than 32699. 
				sevenZrExeProcess.StartInfo.FileName = MainCROWBAR.TheApp.SevenZrExePathFileName;
				sevenZrExeProcess.StartInfo.Arguments = "x \"" + theLocalFileName + "\"";
#if DEBUG
				sevenZrExeProcess.StartInfo.CreateNoWindow = false;
#else
				sevenZrExeProcess.StartInfo.CreateNoWindow = true;
#endif
				sevenZrExeProcess.Start();
				sevenZrExeProcess.WaitForExit();
			}
			catch (Exception ex)
			{
				throw new System.Exception("Crowbar tried to decompress the file \"" + theLocalPathFileName + "\" but Windows gave this message: " + ex.Message);
			}
			finally
			{
				sevenZrExeProcess.Close();
			}

			try
			{
				if (File.Exists(theLocalPathFileName))
				{
					File.Delete(theLocalPathFileName);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		//NOTE: This is run in a background thread.
		private void OpenNewVersion()
		{
			string newCrowbarPathFileName = Path.Combine(theLocalPath, "Crowbar.exe");
			if (File.Exists(newCrowbarPathFileName))
			{
				// Run CrowbarLauncher.exe and exit Crowbar.
				Process crowbarOrLauncherExeProcess = new Process();
				string startupPath = Application.StartupPath;
				string currentCrowbarExePathFileName = Path.Combine(startupPath, "Crowbar.exe");

				try
				{
					crowbarOrLauncherExeProcess.StartInfo.UseShellExecute = false;
					if (MainCROWBAR.TheApp.Settings.UpdateUpdateToNewPathIsChecked)
					{
						//NOTE: From Microsoft website: 
						//      On Windows Vista and earlier versions of the Windows operating system, 
						//      the length of the arguments added to the length of the full path to the process must be less than 2080. 
						//      On Windows 7 and later versions, the length must be less than 32699. 
						crowbarOrLauncherExeProcess.StartInfo.FileName = newCrowbarPathFileName;
						//If TheApp.Settings.UpdateCopySettingsIsChecked Then
						//	crowbarOrLauncherExeProcess.StartInfo.Arguments = App.SettingsParameter + """" + TheApp.GetAppSettingsPathFileName() + """"
						//Else
						//	crowbarOrLauncherExeProcess.StartInfo.Arguments = ""
						//End If
						crowbarOrLauncherExeProcess.StartInfo.Arguments = "";
					}
					else
					{
						crowbarOrLauncherExeProcess.StartInfo.FileName = MainCROWBAR.TheApp.CrowbarLauncherExePathFileName;
						crowbarOrLauncherExeProcess.StartInfo.Arguments = Process.GetCurrentProcess().Id.ToString() + " \"" + currentCrowbarExePathFileName + "\"";
					}
					if (MainCROWBAR.TheApp.Settings.UpdateCopySettingsIsChecked)
					{
						crowbarOrLauncherExeProcess.StartInfo.Arguments += " " + App.SettingsParameter + "\"" + MainCROWBAR.TheApp.GetAppSettingsPathFileName() + "\"";
					}
#if DEBUG
					crowbarOrLauncherExeProcess.StartInfo.CreateNoWindow = false;
#else
					crowbarOrLauncherExeProcess.StartInfo.CreateNoWindow = true;
#endif
					crowbarOrLauncherExeProcess.Start();
					Application.Exit();
				}
				catch (Exception ex)
				{
					int debug = 4242;
					//Throw New System.Exception("Crowbar tried to open new version but Windows gave this message: " + ex.Message)
				}
				finally
				{
				}
			}
		}

#endregion

#region Core Event Handlers

		private void CheckForUpdate_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				//Me.CheckForUpdateTextBox.Text = "Check canceled."
			}
			else
			{
				if (theDownloadTaskIsEnabled)
				{
					DownloadAfterCheckForUpdate();
				}
			}
			theCheckForUpdateRunWorkerCompletedHandler(sender, e);
		}

		private void Download_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
			}
			else
			{
				if (theUpdateTaskIsEnabled)
				{
					UpdateAfterDownload();
				}
			}

			WebClient client = (WebClient)sender;
			client.DownloadFileCompleted -= Download_DownloadFileCompleted;
			client = null;
			theDownloadFileCompletedHandler(sender, e);
		}

#endregion

#region Events

#endregion

#region Data

		private WebClient theWebClient;
		private RunWorkerCompletedEventHandler theCheckForUpdateRunWorkerCompletedHandler;
		private DownloadProgressChangedEventHandler theDownloadProgressChangedHandler;
		private AsyncCompletedEventHandler theDownloadFileCompletedHandler;
		private bool theDownloadTaskIsEnabled;
		private string theRemoteFileLink;
		private string theLocalPath;
		private string theLocalFileName;
		private string theLocalPathFileName;
		private ProgressChangedEventHandler theUpdateProgressChangedHandler;
		private RunWorkerCompletedEventHandler theUpdateRunWorkerCompletedHandler;
		private bool theUpdateTaskIsEnabled;

		private Version theAppVersion = null;

#endregion

	}

}