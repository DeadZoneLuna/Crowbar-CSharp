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
using System.Net;

namespace Crowbar
{
	public partial class UpdateUserControl
	{

#region Creation and Destruction

		public UpdateUserControl() : base()
		{

			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			this.theUpdater = new Updater();
		}

#endregion

#region Init and Free

		private void Init()
		{
			this.DownloadFolderTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "UpdateDownloadPath", false, DataSourceUpdateMode.OnValidation);

			this.UpdateToNewPathCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "UpdateUpdateToNewPathIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.UpdateFolderTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "UpdateUpdateDownloadPath", false, DataSourceUpdateMode.OnValidation);
			this.UpdateCopySettingsCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "UpdateCopySettingsIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.DownloadFolderTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
			this.UpdateFolderTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;

			this.CurrentVersionLabel.Text = "Current Version: " + Version.Parse(Application.ProductVersion).ToString(2);
		}

		private void Free()
		{
			this.DownloadFolderTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			this.UpdateFolderTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;

			this.DownloadFolderTextBox.DataBindings.Clear();

			this.UpdateToNewPathCheckBox.DataBindings.Clear();
			this.UpdateFolderTextBox.DataBindings.Clear();
			this.UpdateCopySettingsCheckBox.DataBindings.Clear();
		}

#endregion

#region Methods

		public void CheckForUpdate()
		{
			this.CheckForUpdateTextBox.Text = "Checking for update...";
			this.UpdateCommandWidgets(true);
			this.CancelCheckButton.Enabled = true;
			this.theUpdater.CheckForUpdate(CheckForUpdate_ProgressChanged, CheckForUpdate_RunWorkerCompleted);
		}

#endregion

#region Events

		public delegate void UpdateAvailableEventHandler(object sender, UpdateAvailableEventArgs e);
		public event UpdateAvailableEventHandler UpdateAvailable;
		protected void NotifyUpdateAvailable(bool updateIsAvailable)
		{
			if (UpdateAvailable != null)
				UpdateAvailable(this, new UpdateAvailableEventArgs(updateIsAvailable));
		}

#endregion

#region Widget Event Handlers

		private void UpdateUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.DownloadFolderTextBox, this.BrowseForDownloadFolderButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.DownloadProgressBarEx, this.CancelDownloadButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.UpdateFolderTextBox, this.BrowseForUpdateFolderButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.UpdateProgressBarEx, this.CancelUpdateButton);

			if (!this.DesignMode)
			{
				this.Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		private void CheckForUpdateButton_Click(object sender, EventArgs e)
		{
			this.CheckForUpdate();
		}

		private void DownloadFolderTextBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] pathFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
			string pathFileName = pathFileNames[0];
			if (Directory.Exists(pathFileName))
			{
				MainCROWBAR.TheApp.Settings.UpdateDownloadPath = pathFileName;
			}
		}

		private void DownloadFolderTextBox_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void BrowseForDownloadFolderButton_Click(object sender, EventArgs e)
		{
			this.BrowseForDownloadPath();
		}

		private void CancelCheckButton_Click(object sender, EventArgs e)
		{
			this.CancelCheckForUpdate();
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			this.Download();
		}

		private void CancelDownloadButton_Click(object sender, EventArgs e)
		{
			this.CancelDownload();
		}

		private void GotoDownloadFileButton_Click(object sender, EventArgs e)
		{
			this.GotoDownloadFile();
		}

		private void UpdateFolderTextBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] pathFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
			string pathFileName = pathFileNames[0];
			if (Directory.Exists(pathFileName))
			{
				MainCROWBAR.TheApp.Settings.UpdateUpdateDownloadPath = pathFileName;
			}
		}

		private void UpdateFolderTextBox_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void BrowseForUpdateFolderButton_Click(object sender, EventArgs e)
		{
			this.BrowseForUpdateDownloadPath();
		}

		private void UpdateButton_Click(object sender, EventArgs e)
		{
			this.UpdateApp();
		}

		private void CancelUpdateButton_Click(object sender, EventArgs e)
		{
			this.CancelUpdate();
		}

#endregion

#region Core Event Handlers

		private void CheckForUpdate_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				// Changelog
				this.ChangelogTextBox.Text = (e.UserState == null ? null : Convert.ToString(e.UserState));
			}
		}

		private void CheckForUpdate_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				this.CheckForUpdateTextBox.Text = "Check canceled.";
			}
			else
			{
				Updater.StatusOutputInfo outputInfo = (Updater.StatusOutputInfo)e.Result;
				this.CheckForUpdateTextBox.Text = Convert.ToString(outputInfo.StatusMessage);
				NotifyUpdateAvailable(outputInfo.UpdateIsAvailable);

				if (outputInfo.UpdateIsEnabled && !outputInfo.UpdateIsAvailable)
				{
					this.theCurrentProgressBar.Text = "No available update.";
					this.theCurrentProgressBar.Value = 0;
				}
				else if (outputInfo.DownloadIsEnabled && !(outputInfo.UpdateIsEnabled && !outputInfo.UpdateIsAvailable))
				{
					this.theCurrentProgressBar.Text = "Starting download...";
					this.theCurrentProgressBar.Value = 0;
				}
			}

			this.UpdateCommandWidgets(false);
			this.CancelCheckButton.Enabled = false;
		}

		private void Download_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			this.UpdateProgressBar(this.theCurrentProgressBar, e.BytesReceived, e.TotalBytesToReceive);
		}

		private void Download_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			string pathFileName = (e.UserState == null ? null : Convert.ToString(e.UserState));
			if (e.Cancelled)
			{
				this.theCurrentProgressBar.Text = "Download failed.";
				this.theCurrentProgressBar.Value = 0;

				if (File.Exists(pathFileName))
				{
					try
					{
						File.Delete(pathFileName);
					}
					catch (Exception ex)
					{
						this.theCurrentProgressBar.Text += "WARNING: Problem deleting incomplete downloaded file: \"" + Path.GetFileName(pathFileName) + "\"";
					}
				}
			}
			else
			{
				if (File.Exists(pathFileName))
				{
					this.theCurrentProgressBar.Text = "Downloaded file: \"" + Path.GetFileName(pathFileName) + "\"   " + this.theCurrentProgressBar.Text;
					this.GotoDownloadFileButton.Enabled = true;
					this.theDownloadedPathFileName = pathFileName;
				}
				else
				{
					this.theCurrentProgressBar.Text = "Download failed.";
				}
			}

			WebClient client = (WebClient)sender;
			client.DownloadProgressChanged -= this.Download_DownloadProgressChanged;
			client.DownloadFileCompleted -= this.Download_DownloadFileCompleted;
			client = null;

			this.UpdateCommandWidgets(false);
			this.CancelDownloadButton.Enabled = false;
		}

		private void Update_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
			}
			else if (e.ProgressPercentage == 1)
			{
			}
		}

		private void Update_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
			}
			else
			{
			}
		}

#endregion

#region Private Methods

		private void CancelCheckForUpdate()
		{
			this.theUpdater.CancelCheckForUpdate();
		}

		private void BrowseForDownloadPath()
		{
			//NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
			//      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
			OpenFileDialog outputPathWdw = new OpenFileDialog();

			outputPathWdw.Title = "Open the folder you want as Download Folder";
			outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.UpdateDownloadPath);
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

				MainCROWBAR.TheApp.Settings.UpdateDownloadPath = FileManager.GetPath(outputPathWdw.FileName);
			}
		}

		private void Download()
		{
			if (FileManager.PathExistsAfterTryToCreate(MainCROWBAR.TheApp.Settings.UpdateDownloadPath))
			{
				this.DownloadProgressBarEx.Text = "Checking for update...";
				this.DownloadProgressBarEx.Value = 0;
				this.theCurrentProgressBar = this.DownloadProgressBarEx;

				this.UpdateCommandWidgets(true);
				this.CancelDownloadButton.Enabled = true;
				this.GotoDownloadFileButton.Enabled = false;
				this.theUpdater.Download(CheckForUpdate_ProgressChanged, CheckForUpdate_RunWorkerCompleted, Download_DownloadProgressChanged, Download_DownloadFileCompleted, MainCROWBAR.TheApp.Settings.UpdateDownloadPath);
			}
			else
			{
				this.DownloadProgressBarEx.Text = "Download failed to start because folder does not exist";
				this.DownloadProgressBarEx.Value = 0;
			}
		}

		private void CancelDownload()
		{
			this.theUpdater.CancelDownload();
		}

		public void GotoDownloadFile()
		{
			FileManager.OpenWindowsExplorer(this.theDownloadedPathFileName);
		}

		private void BrowseForUpdateDownloadPath()
		{
			//NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
			//      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
			OpenFileDialog outputPathWdw = new OpenFileDialog();

			outputPathWdw.Title = "Open the folder you want as Update Download Folder";
			outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.UpdateUpdateDownloadPath);
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

				MainCROWBAR.TheApp.Settings.UpdateUpdateDownloadPath = FileManager.GetPath(outputPathWdw.FileName);
			}
		}

		// Named UpdateApp to avoid confusion with existing UserControl.Update().
		private void UpdateApp()
		{
			this.UpdateProgressBarEx.Text = "Checking for update...";
			this.UpdateProgressBarEx.Value = 0;
			this.theCurrentProgressBar = this.UpdateProgressBarEx;

			this.UpdateCommandWidgets(true);
			this.CancelUpdateButton.Enabled = true;
			string localPath = null;
			if (MainCROWBAR.TheApp.Settings.UpdateUpdateToNewPathIsChecked)
			{
				localPath = MainCROWBAR.TheApp.Settings.UpdateUpdateDownloadPath;
			}
			else
			{
				localPath = MainCROWBAR.TheApp.GetCustomDataPath();
			}
			this.theUpdater.Update(CheckForUpdate_ProgressChanged, CheckForUpdate_RunWorkerCompleted, Download_DownloadProgressChanged, Download_DownloadFileCompleted, localPath, Update_ProgressChanged, Update_RunWorkerCompleted);
		}

		private void CancelUpdate()
		{
			this.theUpdater.CancelUpdate();
		}

		private void UpdateCommandWidgets(bool taskIsRunning)
		{
			this.CheckForUpdateButton.Enabled = !taskIsRunning;
			//Me.CancelCheckButton.Enabled = taskIsRunning

			this.BrowseForDownloadFolderButton.Enabled = !taskIsRunning;
			this.DownloadButton.Enabled = !taskIsRunning;
			//Me.CancelDownloadButton.Enabled = taskIsRunning

			this.BrowseForUpdateFolderButton.Enabled = !taskIsRunning;
			this.UpdateButton.Enabled = !taskIsRunning;
			//Me.CancelUpdateButton.Enabled = taskIsRunning
		}

		private void UpdateProgressBar(ProgressBarEx aProgressBar, long bytesReceived, long totalBytesToReceive)
		{
			int progressPercentage = Convert.ToInt32(bytesReceived * aProgressBar.Maximum / (double)totalBytesToReceive);
			aProgressBar.Text = bytesReceived.ToString("N0") + " / " + totalBytesToReceive.ToString("N0") + " bytes   " + progressPercentage.ToString() + " %";
			aProgressBar.Value = progressPercentage;
		}

#endregion

#region Data

		private ProgressBarEx theCurrentProgressBar;
		private Updater theUpdater;
		private string theDownloadedPathFileName;

#endregion

		public class UpdateAvailableEventArgs : System.EventArgs
		{
			public UpdateAvailableEventArgs(bool updateIsAvailable) : base()
			{

				this.theUpdateIsAvailable = updateIsAvailable;
			}

			public bool UpdateIsAvailable
			{
				get
				{
					return this.theUpdateIsAvailable;
				}
			}

			private bool theUpdateIsAvailable;

		}

	}

}