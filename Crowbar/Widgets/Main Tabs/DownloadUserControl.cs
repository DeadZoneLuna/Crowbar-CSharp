//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Crowbar
{
	public partial class DownloadUserControl
	{

#region Creation and Destruction

		public DownloadUserControl() : base()
		{
			// This call is required by the designer.
			InitializeComponent();
		}

		//'UserControl overrides dispose to clean up the component list.
		//<System.Diagnostics.DebuggerNonUserCode()>
		//Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		//	Try
		//		If disposing Then
		//			Me.Free()
		//			If components IsNot Nothing Then
		//				components.Dispose()
		//			End If
		//		End If
		//	Finally
		//		MyBase.Dispose(disposing)
		//	End Try
		//End Sub

#endregion

#region Init and Free

		private void Init()
		{
			MainCROWBAR.TheApp.InitAppInfo();

			this.ItemIdTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "DownloadItemIdOrLink", false, DataSourceUpdateMode.OnValidation);

			this.InitOutputPathComboBox();
			this.DocumentsOutputPathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.OutputPathTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "DownloadOutputWorkPath", false, DataSourceUpdateMode.OnValidation);
			this.UpdateOutputPathWidgets();

			this.InitDownloadOptions();
			this.UpdateExampleOutputFileNameTextBox();

			this.theBackgroundSteamPipe = new BackgroundSteamPipe();

			this.OutputPathTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
		}

		public void Free()
		{
			//NOTE: Anything related to widgets raises exception because the widgets seem to have already been disposed. Not sure why though.

			//Me.CancelDownload()

			if (this.theBackgroundSteamPipe != null)
			{
				this.theBackgroundSteamPipe.Kill();
			}

			this.OutputPathTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;

			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;

			this.FreeDownloadOptions();

			this.FreeOutputPathComboBox();

			this.ItemIdTextBox.DataBindings.Clear();
		}

		private void InitOutputPathComboBox()
		{
			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.DownloadOutputPathOptions));

			try
			{
				this.OutputPathComboBox.DisplayMember = "Value";
				this.OutputPathComboBox.ValueMember = "Key";
				this.OutputPathComboBox.DataSource = anEnumList;
				this.OutputPathComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "DownloadOutputFolderOption", false, DataSourceUpdateMode.OnPropertyChanged);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void FreeOutputPathComboBox()
		{
			this.OutputPathComboBox.DataBindings.Clear();
		}

		private void InitDownloadOptions()
		{
			this.UseIdCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DownloadUseItemIdIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.PrependTitleCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DownloadPrependItemTitleIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AppendDateTimeCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DownloadAppendItemUpdateDateTimeIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.ReplaceSpacesWithUnderscoresCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DownloadReplaceSpacesWithUnderscoresIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.ConvertToExpectedFileOrFolderCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void FreeDownloadOptions()
		{
			this.UseIdCheckBox.DataBindings.Clear();
			this.PrependTitleCheckBox.DataBindings.Clear();
			this.AppendDateTimeCheckBox.DataBindings.Clear();
			this.ReplaceSpacesWithUnderscoresCheckBox.DataBindings.Clear();
			this.ConvertToExpectedFileOrFolderCheckBox.DataBindings.Clear();
		}

#endregion

#region Widget Event Handlers

		private void DownloadUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.ItemIdTextBox, this.OpenWorkshopPageButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.OutputPathTextBox, this.BrowseForOutputPathButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.DocumentsOutputPathTextBox, this.BrowseForOutputPathButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.DownloadProgressBar, this.DownloadProgressBar.Parent, true);

			if (!this.DesignMode)
			{
				this.Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		private void OpenWorkshopPageButton_Click(object sender, EventArgs e)
		{
			this.OpenWorkshopPage();
		}

		private void OutputPathTextBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] pathFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
			string pathFileName = pathFileNames[0];
			if (Directory.Exists(pathFileName))
			{
				MainCROWBAR.TheApp.Settings.DownloadOutputWorkPath = pathFileName;
			}
		}

		private void OutputPathTextBox_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void OutputPathTextBox_Validated(object sender, EventArgs e)
		{
			this.UpdateOutputPathTextBox();
		}

		private void BrowseForOutputPathButton_Click(object sender, EventArgs e)
		{
			this.BrowseForOutputPath();
		}

		private void GotoOutputPathButton_Click(object sender, EventArgs e)
		{
			this.GotoOutputPath();
		}

		private void OptionsUseDefaultsButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultDownloadOptions();
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			this.DownloadFromLink();
		}

		private void CancelDownloadButton_Click(object sender, EventArgs e)
		{
			this.CancelDownload();
		}

		private void UseInUnpackButton_Click(object sender, EventArgs e)
		{
			this.UseInUnpack();
		}

		private void GotoDownloadedItemButton_Click(object sender, EventArgs e)
		{
			this.GotoDownloadedItem();
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			this.LogTextBox.AppendText(".");
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "DownloadOutputFolderOption")
			{
				this.UpdateOutputPathWidgets();
			}
			else if (e.PropertyName == "DownloadUseItemIdIsChecked")
			{
				this.UpdateExampleOutputFileNameTextBox();
			}
			else if (e.PropertyName == "DownloadPrependItemTitleIsChecked")
			{
				this.UpdateExampleOutputFileNameTextBox();
			}
			else if (e.PropertyName == "DownloadAppendItemUpdateDateTimeIsChecked")
			{
				this.UpdateExampleOutputFileNameTextBox();
			}
			else if (e.PropertyName == "DownloadReplaceSpacesWithUnderscoresIsChecked")
			{
				this.UpdateExampleOutputFileNameTextBox();
			}
		}

		private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
		{

		}

		private void GetResponseCallback(IAsyncResult asynchronousResult)
		{

		}

		private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			//Me.DownloadProgressBar.Text = e.BytesReceived.ToString("N0") + " / " + e.TotalBytesToReceive.ToString("N0") + " bytes   " + e.ProgressPercentage.ToString() + " %"
			//Me.DownloadProgressBar.Value = CInt(e.BytesReceived * Me.DownloadProgressBar.Maximum / e.TotalBytesToReceive)
			this.UpdateProgressBar(e.BytesReceived, e.TotalBytesToReceive);
		}

		private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			string pathFileName = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.Cancelled)
			{
				this.LogTextBox.AppendText("Download cancelled." + "\r\n");
				this.DownloadProgressBar.Text = "";
				this.DownloadProgressBar.Value = 0;

				if (File.Exists(pathFileName))
				{
					try
					{
						File.Delete(pathFileName);
					}
					catch (Exception ex)
					{
						this.LogTextBox.AppendText("WARNING: Problem deleting incomplete downloaded file." + "\r\n");
					}
				}
			}
			else
			{
				if (File.Exists(pathFileName))
				{
					this.LogTextBox.AppendText("Download complete." + "\r\n" + "Downloaded file: \"" + pathFileName + "\"" + "\r\n");
					this.DownloadedItemTextBox.Text = pathFileName;
				}
				else
				{
					this.LogTextBox.AppendText("Download failed." + "\r\n");
				}
			}

			this.theWebClient.DownloadProgressChanged -= this.WebClient_DownloadProgressChanged;
			this.theWebClient.DownloadFileCompleted -= this.WebClient_DownloadFileCompleted;
			this.theWebClient = null;

			//Me.DownloadButton.Enabled = True
			//Me.CancelDownloadButton.Enabled = False

			if (!e.Cancelled && File.Exists(pathFileName))
			{
				this.ProcessFolderOrFileAfterDownload(ref pathFileName);
			}

			this.DownloadButton.Enabled = true;
			this.CancelDownloadButton.Enabled = false;
		}

		private void DownloadItem_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				this.LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 1)
			{
				BackgroundSteamPipe.DownloadItemOutputInfo outputInfo = (BackgroundSteamPipe.DownloadItemOutputInfo)e.UserState;
				this.theDownloadBytesReceived += outputInfo.BytesReceived;
				//Dim progressPercentage As Integer
				//'If Me.theDownloadBytesReceived < outputInfo.TotalBytesToReceive Then
				//progressPercentage = CInt(Me.theDownloadBytesReceived * Me.DownloadProgressBar.Maximum / outputInfo.TotalBytesToReceive)
				//'Else
				//'	progressPercentage = 100
				//'End If
				//Me.DownloadProgressBar.Text = Me.theDownloadBytesReceived.ToString() + " / " + outputInfo.TotalBytesToReceive.ToString() + "   " + progressPercentage.ToString() + " %"
				//Me.DownloadProgressBar.Value = progressPercentage
				this.UpdateProgressBar(this.theDownloadBytesReceived, outputInfo.TotalBytesToReceive);
			}
		}

		private void DownloadItem_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			string outputPathFileName = null;
			string targetOutputPath = null;
			BackgroundSteamPipe.DownloadItemOutputInfo outputInfo = null;

			if (e.Cancelled)
			{
				this.LogTextBox.AppendText("Download cancelled." + "\r\n");
				this.DownloadProgressBar.Text = "";
				this.DownloadProgressBar.Value = 0;
			}
			else
			{
				outputInfo = (BackgroundSteamPipe.DownloadItemOutputInfo)e.Result;
				if (outputInfo.Result == "success")
				{
					// Me.theDownloadBytesReceived does not have the full byte count and outputInfo.TotalBytesToReceive = 0.
					//Me.UpdateProgressBar(Me.theDownloadBytesReceived, outputInfo.TotalBytesToReceive)
					this.UpdateProgressBar(outputInfo.ContentFile.Length, outputInfo.ContentFile.Length);

					string outputPath = this.GetOutputPath();

					string outputFileName = this.GetOutputFileName(outputInfo.ItemTitle, outputInfo.PublishedItemID, outputInfo.ContentFolderOrFileName, outputInfo.ItemUpdated_Text);

					outputPathFileName = Path.Combine(outputPath, outputFileName);
					outputPathFileName = FileManager.GetTestedPathFileName(outputPathFileName);

					File.WriteAllBytes(outputPathFileName, outputInfo.ContentFile);
					if (File.Exists(outputPathFileName))
					{
						this.LogTextBox.AppendText("Download complete." + "\r\n" + "Downloaded file: \"" + outputPathFileName + "\"" + "\r\n");
						this.DownloadedItemTextBox.Text = outputPathFileName;
						//Me.ProcessFolderOrFileAfterDownload(outputPathFileName)
					}
					else
					{
						this.LogTextBox.AppendText("Download failed." + "\r\n");
					}
				}
				else if (outputInfo.Result == "success_SteamUGC")
				{
					string outputPath = this.GetOutputPath();

					string outputFolder = this.GetOutputFileName(outputInfo.ItemTitle, outputInfo.PublishedItemID, outputInfo.ContentFolderOrFileName, outputInfo.ItemUpdated_Text);

					targetOutputPath = Path.Combine(outputPath, outputFolder);
					targetOutputPath = FileManager.GetTestedPath(targetOutputPath);

					if (Directory.Exists(outputInfo.ContentFolderOrFileName))
					{
						//FileManager.CopyFolder(outputInfo.ContentFolderOrFileName, targetOutputPath, True)
						//' [DownloadItem_RunWorkerCompleted] Delete Steam's cached item after downloading SteamUGC item.
						//'NOTE: Deleting the folder makes the item un-downloadable for later attempts because Steam still thinks it is installed.
						//'      This only occurred because Crowbar used different Steamworks functions calls to download when EItemState.k_EItemStateInstalled was set. 
						//'TODO: [DownloadItem_RunWorkerCompleted] Delete Steam's cached item manifest file and cached acf info after downloading SteamUGC item.
						//Directory.Delete(outputInfo.ContentFolderOrFileName, True)
						//'======
						//'NOTE: UnsubscribeItem() does not delete the folder.
						//'Me.UnsubscribeItem(outputInfo.AppID, outputInfo.PublishedItemID)
						//======
						//NOTE: File remains: "C:\Program Files (x86)\Steam\depotcache\<app_id>_<manifest_id>.manifest"
						//NOTE: Data for the downloaded file remains in: "<steam_folder_on_drive_where_game_is_installed>\steamapps\workshop\appworkshop_<app_id>.acf"
						//NOTE: Do not use Directory.Move() because it raises exception when trying to move between drives.
						//Directory.Move(outputInfo.ContentFolderOrFileName, targetOutputPath)
						//======
						System.IO.Directory.Move(outputInfo.ContentFolderOrFileName, targetOutputPath);

						if (Directory.Exists(targetOutputPath))
						{
							//Me.ProcessFolderOrFileAfterDownload(targetOutputPath)
							this.LogTextBox.AppendText("Download complete." + "\r\n" + "Downloaded folder: \"" + targetOutputPath + "\"" + "\r\n");
							this.DownloadedItemTextBox.Text = targetOutputPath;
						}
						else
						{
							this.LogTextBox.AppendText("Download failed." + "\r\n");
						}
					}
					else
					{
						this.LogTextBox.AppendText("Download failed." + "\r\n");
					}
				}
			}

			//Me.DownloadButton.Enabled = True
			//Me.CancelDownloadButton.Enabled = False

			if (!e.Cancelled && outputInfo != null)
			{
				if (outputInfo.Result == "success")
				{
					if (File.Exists(outputPathFileName))
					{
						this.ProcessFolderOrFileAfterDownload(ref outputPathFileName);
					}
				}
				else if (outputInfo.Result == "success_SteamUGC")
				{
					if (Directory.Exists(targetOutputPath))
					{
						try
						{
							if (MainCROWBAR.TheApp.SteamAppInfos.Count > 0)
							{
								//NOTE: Use this temp var because appID as a ByRef var can not be used in a lambda expression used in next line.
								Steamworks.AppId_t steamAppID = new Steamworks.AppId_t(outputInfo.AppID);
								this.theSteamAppInfo = MainCROWBAR.TheApp.SteamAppInfos.First((info) => info.ID == steamAppID);
								this.ProcessFolderOrFileAfterDownload(ref targetOutputPath);
							}
						}
						catch (Exception ex)
						{
							int debug = 4242;
						}
					}
				}
			}

			this.DownloadButton.Enabled = true;
			this.CancelDownloadButton.Enabled = false;
		}

		private void UnsubscribeItem_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				this.LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
		}

		private void UnsubscribeItem_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			//If e.Cancelled Then
			//	Me.LogTextBox.AppendText("Download cancelled." + vbCrLf)
			//Else
			//	Dim outputInfo As BackgroundSteamPipe.DownloadItemOutputInfo = CType(e.Result, BackgroundSteamPipe.DownloadItemOutputInfo)
			//	If outputInfo.Result = "success" Then
			//		Me.LogTextBox.AppendText("Download complete." + vbCrLf)
			//	End If
			//End If

			int placeholder = 4242;
		}

#endregion

#region Private Methods

		private void OpenWorkshopPage()
		{
			string itemIdOrLink = this.ItemIdTextBox.Text;
			string itemlink = "";
			if (itemIdOrLink.StartsWith(AppConstants.WorkshopLinkStart))
			{
				itemlink = itemIdOrLink;
			}
			else
			{
				itemlink = AppConstants.WorkshopLinkStart + itemIdOrLink;
			}
			try
			{
				System.Diagnostics.Process.Start(itemlink);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void UpdateOutputPathTextBox()
		{
			if (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.WorkFolder)
			{
				if (string.IsNullOrEmpty(this.OutputPathTextBox.Text))
				{
					try
					{
						MainCROWBAR.TheApp.Settings.DownloadOutputWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
				}
			}
		}

		private void UpdateOutputPathWidgets()
		{
			this.DocumentsOutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.DocumentsFolder);
			this.OutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.WorkFolder);
			this.BrowseForOutputPathButton.Enabled = (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.WorkFolder);
			//Me.GotoOutputPathButton.Enabled = (TheApp.Settings.DownloadOutputFolderOption = DownloadOutputPathOptions.WorkFolder)
		}

		private void BrowseForOutputPath()
		{
			if (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.WorkFolder)
			{
				//NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
				//      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
				OpenFileDialog outputPathWdw = new OpenFileDialog();

				outputPathWdw.Title = "Open the folder you want as Output Folder";
				outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.DownloadOutputWorkPath);
				if (string.IsNullOrEmpty(outputPathWdw.InitialDirectory))
				{
					outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
				outputPathWdw.FileName = "[Folder Selection]";
				outputPathWdw.AddExtension = false;
				outputPathWdw.CheckFileExists = false;
				outputPathWdw.Multiselect = false;
				outputPathWdw.ValidateNames = false;

				if (outputPathWdw.ShowDialog() == DialogResult.OK)
				{
					// Allow dialog window to completely disappear.
					Application.DoEvents();

					MainCROWBAR.TheApp.Settings.DownloadOutputWorkPath = FileManager.GetPath(outputPathWdw.FileName);
				}
			}
		}

		private void GotoOutputPath()
		{
			//If TheApp.Settings.DownloadOutputFolderOption = DownloadOutputPathOptions.DownloadsFolder Then
			//	'TODO: Find way to get the Downloads path. Note that Windows XP does not have a Downloads special folder.
			//	'FileManager.OpenWindowsExplorer(Environment.GetFolderPath(Environment.SpecialFolder.Downloads))
			//Else
			if (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.DocumentsFolder)
			{
				FileManager.OpenWindowsExplorer(this.DocumentsOutputPathTextBox.Text);
			}
			else if (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.WorkFolder)
			{
				FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.DownloadOutputWorkPath);
			}
		}

		private void UseInUnpack()
		{
			string extension = Path.GetExtension(this.DownloadedItemTextBox.Text);
			if (extension == ".gma" || extension == ".vpk")
			{

			}
			MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName = this.DownloadedItemTextBox.Text;
		}

		private void GotoDownloadedItem()
		{
			if (!string.IsNullOrEmpty(this.DownloadedItemTextBox.Text))
			{
				FileManager.OpenWindowsExplorer(this.DownloadedItemTextBox.Text);
			}
		}

		private void DownloadFromLink()
		{
			this.LogTextBox.Text = "";
			this.DownloadProgressBar.Text = "";
			this.DownloadProgressBar.Value = 0;
			this.theDownloadBytesReceived = 0;
			this.DownloadedItemTextBox.Text = "";
			this.DownloadButton.Enabled = false;
			this.CancelDownloadButton.Enabled = true;

			string itemLink = "";
			string itemID = this.GetItemID();
			uint appID = 0;
			if (itemID == "0")
			{
				this.LogTextBox.AppendText("ERROR: Item ID is invalid." + "\r\n");
				return;
			}
			else
			{
				//Me.LogTextBox.AppendText("Getting item content download link." + vbCrLf)
				this.LogTextBox.AppendText("Getting item content download link...");
				Application.DoEvents();
				this.Timer1.Interval = 1000;
				this.Timer1.Start();
				itemLink = this.GetDownloadLink(itemID, ref appID);
				this.Timer1.Stop();
			}
			if (!string.IsNullOrEmpty(itemLink))
			{
				this.LogTextBox.AppendText("Item content download link found. Downloading file via web." + "\r\n");
				this.DownloadViaWeb(itemLink, this.theItemContentPathFileName);
			}
			else
			{
				//Me.LogTextBox.AppendText("Item content download link not found. Probably an item that uses newer Steam API or a Friends-only item not downloadable via web." + vbCrLf)
				this.LogTextBox.AppendText("Item content download link not found. Downloading file via Steam." + "\r\n");

				string outputPath = this.GetOutputPath();

				//Dim outputFolder As String
				//outputFolder = Me.GetOutputFileName(outputInfo.ItemTitle, outputInfo.PublishedItemID, outputInfo.ContentFolderOrFileName, outputInfo.ItemUpdated_Text)

				string targetPath;
				//targetPath = Path.Combine(outputPath, outputFolder)
				//targetPath = FileManager.GetTestedPath(targetPath)
				//------
				targetPath = outputPath;

				this.DownloadViaSteam(appID, itemID, targetPath);
			}
		}

		private void CancelDownload()
		{
			if (this.theWebClient != null)
			{
				this.theWebClient.CancelAsync();
			}
		}

		private string GetItemID()
		{
			NameValueCollection qscoll = null;
			string itemID = "0";
			try
			{
				Uri uri = new Uri(this.ItemIdTextBox.Text);
				string querystring = uri.Query;
				//Dim separators() = {"="}
				//id = querystring.Split()
				qscoll = HttpUtility.ParseQueryString(querystring);
				itemID = qscoll["id"];
			}
			catch (UriFormatException ex1)
			{
				string text = this.ItemIdTextBox.Text;
				itemID = "";
				int pos = text.IndexOf("id=");
				if (pos >= 0)
				{
					text = text.Remove(0, pos + 3);
					foreach (char c in text)
					{
						if (NumericHelper.IsNumeric(c))
						{
							itemID += c.ToString();
						}
						else
						{
							break;
						}
					}
				}
				else
				{
					//NOTE: Get first run of numeric characters.
					bool foundNumeric = false;
					foreach (char c in text)
					{
						if (NumericHelper.IsNumeric(c))
						{
							itemID += c.ToString();
							foundNumeric = true;
						}
						else if (foundNumeric)
						{
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			if (string.IsNullOrEmpty(itemID))
			{
				itemID = "0";
			}

			return itemID;
		}

		private string GetDownloadLink(string itemID, ref uint appID)
		{
			string itemLink = "";
			this.theItemContentPathFileName = "";

			//Dim downloadHasStarted As Boolean = SteamUGC.DownloadItem(371699674, True)
			//If downloadHasStarted Then
			//    Me.TextBox1.Text = "Download started."
			//    'TODO: Set the handler.
			//Else
			//    Me.TextBox1.Text = "ERROR: Download did not start."
			//End If
			//======
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.steampowered.com/ISteamRemoteStorage/GetPublishedFileDetails/v0001/");
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";

			byte[] byteData = null;
			string data;
			//data = "itemcount=1&publishedfileids[0]=" + id + "&format=json"
			data = "itemcount=1&publishedfileids[0]=" + itemID;
			byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
			request.ContentLength = byteData.Length;
			//request.Timeout = 5000

			//TODO: request.BeginGetRequestStream(AddressOf GetRequestStreamCallback, request)
			//      https://docs.microsoft.com/en-us/dotnet/api/system.net.httpwebrequest.begingetrequeststream?view=netframework-4.0
			Stream postStream = null;
			try
			{
				postStream = request.GetRequestStream();
				postStream.Write(byteData, 0, byteData.Length);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (postStream != null)
				{
					postStream.Close();
				}
			}

			HttpWebResponse response = null;
			Stream dataStream = null;
			StreamReader reader = null;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
				dataStream = response.GetResponseStream();
				reader = new StreamReader(dataStream);
				string responseFromServer = reader.ReadToEnd();

				JavaScriptSerializer jss = new JavaScriptSerializer();
				SteamRemoteStorage_PublishedFileDetails_Json root = jss.Deserialize<SteamRemoteStorage_PublishedFileDetails_Json>(responseFromServer);
				string file_url = root.response.publishedfiledetails[0].file_url;
				if (file_url != null && !string.IsNullOrEmpty(file_url))
				{
					itemLink = file_url;

					this.theItemTitle = root.response.publishedfiledetails[0].title;
					string fileName = root.response.publishedfiledetails[0].filename;
					this.theItemContentPathFileName = fileName;
					this.theItemIdText = root.response.publishedfiledetails[0].publishedfileid;
					this.theItemTimeUpdatedText = root.response.publishedfiledetails[0].time_updated.ToString();
				}

				appID = Convert.ToUInt32(root.response.publishedfiledetails[0].consumer_app_id);
				this.theAppIdText = appID.ToString();
				this.theSteamAppInfo = null;
				try
				{
					if (MainCROWBAR.TheApp.SteamAppInfos.Count > 0)
					{
						//NOTE: Use this temp var because appID as a ByRef var can not be used in a lambda expression used in next line.
						Steamworks.AppId_t steamAppID = new Steamworks.AppId_t(appID);
						this.theSteamAppInfo = MainCROWBAR.TheApp.SteamAppInfos.First((info) => info.ID == steamAppID);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
				if (this.theSteamAppInfo == null)
				{
					//NOTE: Value was not found, so unable to download.
					appID = 0;
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
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

				this.LogTextBox.AppendText("\r\n");
			}

			return itemLink;
		}

		private void DownloadViaWeb(string link, string givenFileName)
		{
			Uri uri = new Uri(link);

			string outputPath = this.GetOutputPath();
			try
			{
				FileManager.CreatePath(outputPath);
			}
			catch (Exception ex)
			{
				this.LogTextBox.AppendText("Crowbar tried to create folder path \"" + outputPath + "\", but Windows gave this message: " + ex.Message + "\r\n");
				return;
			}

			string outputFileName = this.GetOutputFileName(this.theItemTitle, this.theItemIdText, givenFileName, this.theItemTimeUpdatedText);

			string outputPathFileName = Path.Combine(outputPath, outputFileName);
			outputPathFileName = FileManager.GetTestedPathFileName(outputPathFileName);

			this.LogTextBox.AppendText("Downloading workshop item as: \"" + outputPathFileName + "\"" + "\r\n");

			//Me.DownloadButton.Enabled = False
			//Me.CancelDownloadButton.Enabled = True

			this.theWebClient = new WebClient();
			this.theWebClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
			this.theWebClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
			this.theWebClient.DownloadFileAsync(uri, outputPathFileName, outputPathFileName);
		}

		private void DownloadViaSteam(uint appID, string itemID, string targetPath)
		{
			//Me.theDownloadBytesReceived = 0
			//Me.DownloadedItemTextBox.Text = ""
			//Me.DownloadButton.Enabled = False
			//Me.CancelDownloadButton.Enabled = True

			BackgroundSteamPipe.DownloadItemInputInfo inputInfo = new BackgroundSteamPipe.DownloadItemInputInfo();
			inputInfo.AppID = appID;
			inputInfo.PublishedItemID = itemID;
			inputInfo.TargetPath = targetPath;
			this.theBackgroundSteamPipe.DownloadItem(this.DownloadItem_ProgressChanged, this.DownloadItem_RunWorkerCompleted, inputInfo);
		}

		private void UnsubscribeItem(uint appID, string itemID)
		{
			BackgroundSteamPipe.DownloadItemInputInfo inputInfo = new BackgroundSteamPipe.DownloadItemInputInfo();
			inputInfo.AppID = appID;
			inputInfo.PublishedItemID = itemID;
			this.theBackgroundSteamPipe.UnsubscribeItem(this.UnsubscribeItem_ProgressChanged, this.UnsubscribeItem_RunWorkerCompleted, inputInfo);
		}

		private string GetOutputPath()
		{
			string outputPath = "";

			if (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.DocumentsFolder)
			{
				outputPath = this.DocumentsOutputPathTextBox.Text;
			}
			else if (MainCROWBAR.TheApp.Settings.DownloadOutputFolderOption == AppEnums.DownloadOutputPathOptions.WorkFolder)
			{
				outputPath = MainCROWBAR.TheApp.Settings.DownloadOutputWorkPath;
			}

			//This will change a relative path to an absolute path.
			outputPath = Path.GetFullPath(outputPath);
			return outputPath;
		}

		private void UpdateExampleOutputFileNameTextBox()
		{
			this.ExampleOutputFileNameTextBox.Text = this.GetOutputFileName("Example Title With Spaces", "00000000", "ExampleFileName.vpk", "0");
		}

		private string GetOutputFileName(string givenTitle, string givenID, string givenFileName, string givenTimeUpdatedText)
		{
			string outputFileNamePrefix = null;
			if (MainCROWBAR.TheApp.Settings.DownloadPrependItemTitleIsChecked)
			{
				outputFileNamePrefix = givenTitle + "_";
			}
			else
			{
				outputFileNamePrefix = "";
			}

			string outputFileNameBase = null;
			if (MainCROWBAR.TheApp.Settings.DownloadUseItemIdIsChecked)
			{
				outputFileNameBase = givenID;
			}
			else
			{
				outputFileNameBase = Path.GetFileNameWithoutExtension(givenFileName);
			}

			string outputFileNameSuffix = null;
			if (MainCROWBAR.TheApp.Settings.DownloadAppendItemUpdateDateTimeIsChecked)
			{
				DateTime fileDateTime = MathModule.UnixTimeStampToDateTime(long.Parse(givenTimeUpdatedText));
				outputFileNameSuffix = "_" + fileDateTime.ToString("yyyy-MM-dd-HHmm");
			}
			else
			{
				outputFileNameSuffix = "";
			}

			string fileExtension = "";
			fileExtension = Path.GetExtension(givenFileName);

			string outputFileName = outputFileNamePrefix + outputFileNameBase + outputFileNameSuffix + fileExtension;
			if (MainCROWBAR.TheApp.Settings.DownloadReplaceSpacesWithUnderscoresIsChecked)
			{
				outputFileName = outputFileName.Replace(" ", "_");
			}

			//NOTE: Remove colons here to prevent GetCleanPathFileName() from removing everything up to first colon.
			outputFileName = outputFileName.Replace(":", "_");
			outputFileName = FileManager.GetCleanPathFileName(outputFileName, false);
			outputFileName = outputFileName.Replace("\\", "_");

			return outputFileName;
		}

		private void UpdateProgressBar(long bytesReceived, long totalBytesToReceive)
		{
			try
			{
				int progressPercentage = Convert.ToInt32(bytesReceived * this.DownloadProgressBar.Maximum / (double)totalBytesToReceive);
				this.DownloadProgressBar.Text = bytesReceived.ToString("N0") + " / " + totalBytesToReceive.ToString("N0") + " bytes   " + progressPercentage.ToString() + " %";
				this.DownloadProgressBar.Value = progressPercentage;
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ProcessFolderOrFileAfterDownload(ref string pathFileName)
		{
			if (this.theSteamAppInfo != null && MainCROWBAR.TheApp.Settings.DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked)
			{
				try
				{
					//Me.DownloadButton.Enabled = False
					//Me.CancelDownloadButton.Enabled = True

					this.theProcessAfterDownloadWorker = new BackgroundWorkerEx();
					this.theProcessAfterDownloadWorker.WorkerSupportsCancellation = true;
					this.theProcessAfterDownloadWorker.WorkerReportsProgress = true;
					this.theProcessAfterDownloadWorker.DoWork += ProcessAfterDownloadWorker_DoWork;
					this.theProcessAfterDownloadWorker.ProgressChanged += ProcessAfterDownloadWorker_ProgressChanged;
					this.theProcessAfterDownloadWorker.RunWorkerCompleted += ProcessAfterDownloadWorker_RunWorkerCompleted;
					this.theProcessAfterDownloadWorker.RunWorkerAsync(pathFileName);
				}
				catch (Exception ex)
				{
					this.LogTextBox.AppendText("ERROR: " + ex.Message + "\r\n");
				}
			}
		}

		//NOTE: This is run in a background thread.
		private void ProcessAfterDownloadWorker_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			string givenPathFileName = (e.Argument == null ? null : Convert.ToString(e.Argument));
			string convertedPathFileName = this.theSteamAppInfo.ProcessFileAfterDownload(givenPathFileName, this.theProcessAfterDownloadWorker);
			if (convertedPathFileName == givenPathFileName)
			{
				e.Result = "";
			}
			else
			{
				e.Result = convertedPathFileName;
			}
		}

		private void ProcessAfterDownloadWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				this.LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
				//ElseIf e.ProgressPercentage = 1 Then
				//	Me.LogTextBox.AppendText(vbTab + CStr(e.UserState))
			}
		}

		private void ProcessAfterDownloadWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
			}
			else
			{
				string pathFileName = (e.Result == null ? null : Convert.ToString(e.Result));
				if (!string.IsNullOrEmpty(pathFileName))
				{
					this.LogTextBox.AppendText("Converted to file: \"" + pathFileName + "\"" + "\r\n");
					//Me.DownloadedItemTextBox.Text = pathFileName
				}
			}

			this.theProcessAfterDownloadWorker.DoWork -= ProcessAfterDownloadWorker_DoWork;
			this.theProcessAfterDownloadWorker.ProgressChanged -= ProcessAfterDownloadWorker_ProgressChanged;
			this.theProcessAfterDownloadWorker.RunWorkerCompleted -= ProcessAfterDownloadWorker_RunWorkerCompleted;
			this.theProcessAfterDownloadWorker = null;

			//Me.DownloadButton.Enabled = True
			//Me.CancelDownloadButton.Enabled = False
		}

#endregion

#region Data

		private WebClient theWebClient;
		private BackgroundWorkerEx theProcessAfterDownloadWorker;
		private string theAppIdText;
		private SteamAppInfoBase theSteamAppInfo;

		private BackgroundSteamPipe theBackgroundSteamPipe;

		private long theDownloadBytesReceived;

		private string theItemTitle;
		private string theItemContentPathFileName;
		private string theItemIdText;
		private string theItemTimeUpdatedText;

#endregion

	}

}