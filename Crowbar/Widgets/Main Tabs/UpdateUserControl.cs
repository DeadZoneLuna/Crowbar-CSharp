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
			theUpdater = new Updater();
		}

#endregion

#region Init and Free

		private void Init()
		{
			DownloadFolderTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "UpdateDownloadPath", false, DataSourceUpdateMode.OnValidation);

			UpdateToNewPathCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "UpdateUpdateToNewPathIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			UpdateFolderTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "UpdateUpdateDownloadPath", false, DataSourceUpdateMode.OnValidation);
			UpdateCopySettingsCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "UpdateCopySettingsIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			DownloadFolderTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
			UpdateFolderTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;

			CurrentVersionLabel.Text = "Current Version: " + ConversionHelper.VersionName;
		}

		private void Free()
		{
			DownloadFolderTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			UpdateFolderTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;

			DownloadFolderTextBox.DataBindings.Clear();

			UpdateToNewPathCheckBox.DataBindings.Clear();
			UpdateFolderTextBox.DataBindings.Clear();
			UpdateCopySettingsCheckBox.DataBindings.Clear();
		}

#endregion

#region Methods

		public void CheckForUpdate()
		{
			CheckForUpdateTextBox.Text = "Checking for update...";
			UpdateCommandWidgets(true);
			CancelCheckButton.Enabled = true;
			theUpdater.CheckForUpdate(CheckForUpdate_ProgressChanged, CheckForUpdate_RunWorkerCompleted);
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
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(DownloadFolderTextBox, BrowseForDownloadFolderButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(DownloadProgressBarEx, CancelDownloadButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(UpdateFolderTextBox, BrowseForUpdateFolderButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(UpdateProgressBarEx, CancelUpdateButton);

			if (!DesignMode)
			{
				Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		private void CheckForUpdateButton_Click(object sender, EventArgs e)
		{
			CheckForUpdate();
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
			BrowseForDownloadPath();
		}

		private void CancelCheckButton_Click(object sender, EventArgs e)
		{
			CancelCheckForUpdate();
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			Download();
		}

		private void CancelDownloadButton_Click(object sender, EventArgs e)
		{
			CancelDownload();
		}

		private void GotoDownloadFileButton_Click(object sender, EventArgs e)
		{
			GotoDownloadFile();
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
			BrowseForUpdateDownloadPath();
		}

		private void UpdateButton_Click(object sender, EventArgs e)
		{
			UpdateApp();
		}

		private void CancelUpdateButton_Click(object sender, EventArgs e)
		{
			CancelUpdate();
		}

#endregion

#region Core Event Handlers

		private void CheckForUpdate_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				// Changelog
				ChangelogTextBox.Text = (e.UserState == null ? null : Convert.ToString(e.UserState));
			}
		}

		private void CheckForUpdate_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				CheckForUpdateTextBox.Text = "Check canceled.";
			}
			else
			{
				Updater.StatusOutputInfo outputInfo = (Updater.StatusOutputInfo)e.Result;
				CheckForUpdateTextBox.Text = Convert.ToString(outputInfo.StatusMessage);
				NotifyUpdateAvailable(outputInfo.UpdateIsAvailable);

				if (outputInfo.UpdateIsEnabled && !outputInfo.UpdateIsAvailable)
				{
					theCurrentProgressBar.Text = "No available update.";
					theCurrentProgressBar.Value = 0;
				}
				else if (outputInfo.DownloadIsEnabled && !(outputInfo.UpdateIsEnabled && !outputInfo.UpdateIsAvailable))
				{
					theCurrentProgressBar.Text = "Starting download...";
					theCurrentProgressBar.Value = 0;
				}
			}

			UpdateCommandWidgets(false);
			CancelCheckButton.Enabled = false;
		}

		private void Download_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			UpdateProgressBar(theCurrentProgressBar, e.BytesReceived, e.TotalBytesToReceive);
		}

		private void Download_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			string pathFileName = (e.UserState == null ? null : Convert.ToString(e.UserState));
			if (e.Cancelled)
			{
				theCurrentProgressBar.Text = "Download failed.";
				theCurrentProgressBar.Value = 0;

				if (File.Exists(pathFileName))
				{
					try
					{
						File.Delete(pathFileName);
					}
					catch (Exception ex)
					{
						theCurrentProgressBar.Text += "WARNING: Problem deleting incomplete downloaded file: \"" + Path.GetFileName(pathFileName) + "\"";
					}
				}
			}
			else
			{
				if (File.Exists(pathFileName))
				{
					theCurrentProgressBar.Text = "Downloaded file: \"" + Path.GetFileName(pathFileName) + "\"   " + theCurrentProgressBar.Text;
					GotoDownloadFileButton.Enabled = true;
					theDownloadedPathFileName = pathFileName;
				}
				else
				{
					theCurrentProgressBar.Text = "Download failed.";
				}
			}

			WebClient client = (WebClient)sender;
			client.DownloadProgressChanged -= Download_DownloadProgressChanged;
			client.DownloadFileCompleted -= Download_DownloadFileCompleted;
			client = null;

			UpdateCommandWidgets(false);
			CancelDownloadButton.Enabled = false;
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
			theUpdater.CancelCheckForUpdate();
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
				DownloadProgressBarEx.Text = "Checking for update...";
				DownloadProgressBarEx.Value = 0;
				theCurrentProgressBar = DownloadProgressBarEx;

				UpdateCommandWidgets(true);
				CancelDownloadButton.Enabled = true;
				GotoDownloadFileButton.Enabled = false;
				theUpdater.Download(CheckForUpdate_ProgressChanged, CheckForUpdate_RunWorkerCompleted, Download_DownloadProgressChanged, Download_DownloadFileCompleted, MainCROWBAR.TheApp.Settings.UpdateDownloadPath);
			}
			else
			{
				DownloadProgressBarEx.Text = "Download failed to start because folder does not exist";
				DownloadProgressBarEx.Value = 0;
			}
		}

		private void CancelDownload()
		{
			theUpdater.CancelDownload();
		}

		public void GotoDownloadFile()
		{
			FileManager.OpenWindowsExplorer(theDownloadedPathFileName);
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
			UpdateProgressBarEx.Text = "Checking for update...";
			UpdateProgressBarEx.Value = 0;
			theCurrentProgressBar = UpdateProgressBarEx;

			UpdateCommandWidgets(true);
			CancelUpdateButton.Enabled = true;
			string localPath = null;
			if (MainCROWBAR.TheApp.Settings.UpdateUpdateToNewPathIsChecked)
			{
				localPath = MainCROWBAR.TheApp.Settings.UpdateUpdateDownloadPath;
			}
			else
			{
				localPath = MainCROWBAR.TheApp.GetCustomDataPath();
			}
			theUpdater.Update(CheckForUpdate_ProgressChanged, CheckForUpdate_RunWorkerCompleted, Download_DownloadProgressChanged, Download_DownloadFileCompleted, localPath, Update_ProgressChanged, Update_RunWorkerCompleted);
		}

		private void CancelUpdate()
		{
			theUpdater.CancelUpdate();
		}

		private void UpdateCommandWidgets(bool taskIsRunning)
		{
			CheckForUpdateButton.Enabled = !taskIsRunning;
			//Me.CancelCheckButton.Enabled = taskIsRunning

			BrowseForDownloadFolderButton.Enabled = !taskIsRunning;
			DownloadButton.Enabled = !taskIsRunning;
			//Me.CancelDownloadButton.Enabled = taskIsRunning

			BrowseForUpdateFolderButton.Enabled = !taskIsRunning;
			UpdateButton.Enabled = !taskIsRunning;
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

				theUpdateIsAvailable = updateIsAvailable;
			}

			public bool UpdateIsAvailable
			{
				get
				{
					return theUpdateIsAvailable;
				}
			}

			private bool theUpdateIsAvailable;

		}

	}

}