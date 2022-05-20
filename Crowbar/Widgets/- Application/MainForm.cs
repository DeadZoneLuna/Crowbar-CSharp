//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Crowbar
{
	public partial class MainForm
	{

#region Create and Destroy

		public MainForm() : base()
		{

			//'DEBUG: Be sure to comment this out before release.
			//' Set the culture and UI culture before 
			//' the call to InitializeComponent.
			//Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("de-DE")
			//Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo("de-DE")

			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			//Me.InitWidgets(Me)
			//Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		}

#endregion

#region Init and Free

		private void Init()
		{
			try
			{
				this.Location = MainCROWBAR.TheApp.Settings.WindowLocation;
				this.Size = MainCROWBAR.TheApp.Settings.WindowSize;
				this.WindowState = MainCROWBAR.TheApp.Settings.WindowState;

				// Ensure minimum size of window. 
				//     Usually MinimumSize Handles this, but MinimumSize changes when Windows Theme Message Box Font Is something weird Like "SimSun-ExtB" 8pt.
				//     Thus, set the minimum size manually here.
				int minimumWidth = 800;
				int minimumHeight = 600;
				if (this.MinimumSize.Width < minimumWidth || this.MinimumSize.Height < minimumHeight)
				{
					this.MinimumSize = new Size(minimumWidth, minimumHeight);
				}
				if (this.Width < this.MinimumSize.Width)
				{
					this.Width = this.MinimumSize.Width;
				}
				if (this.Height < this.MinimumSize.Height)
				{
					this.Height = this.MinimumSize.Height;
				}

				if (MainCROWBAR.TheApp.CommandLineOption_Settings_IsEnabled)
				{
					MainCROWBAR.TheApp.Settings.MainWindowSelectedTabIndex = this.MainTabControl.TabPages.IndexOf(this.UpdateTabPage);
				}
				this.MainTabControl.SelectedIndex = MainCROWBAR.TheApp.Settings.MainWindowSelectedTabIndex;

				Screen aScreen = Screen.FromControl(this);
				//WorkingArea means the area of the screen without the Windows taskbar.
				Rectangle aScreenWorkingArea = aScreen.WorkingArea;
				// Ensure at least 60 px of Title Bar visible
				if (this.Location.X < aScreenWorkingArea.Left || this.Location.X + 60 > aScreenWorkingArea.Left + aScreenWorkingArea.Width)
				{
					this.Left = aScreenWorkingArea.Left;
				}
				// Ensure top visible
				if (this.Location.Y < aScreenWorkingArea.Top || this.Location.Y + this.Size.Height > aScreenWorkingArea.Top + aScreenWorkingArea.Height)
				{
					this.Top = aScreenWorkingArea.Top;
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			//'TEST:
			//JumpList()

			//Dim commandLineParams() As String = System.Environment.GetCommandLineArgs()
			//If commandLineParams.Length > 1 AndAlso commandLineParams(1) <> "" Then
			//	Me.SetDroppedPathFileName(True, commandLineParams(1))

			//	''TEST: Every file selected and dropped onto EXE is a string in the array, starting at index 1. Index 0 is the EXE path file name.
			//	'Dim text As New StringBuilder()
			//	'For Each arg As String In commandLineParams
			//	'	text.AppendLine(arg)
			//	'Next
			//	'MessageBox.Show(text.ToString())
			//End If
			ReadOnlyCollection<string> commandLineValues = new ReadOnlyCollection<string>(System.Environment.GetCommandLineArgs());
			this.Startup(commandLineValues);

			this.PreviewViewUserControl.RunDataViewer();
			this.ViewViewUserControl.RunDataViewer();

			this.SetUpGamesUserControl1.GoBackButton.Click += this.SetUpGamesGoBackButton_Click;
			this.DownloadUserControl1.UseInUnpackButton.Click += this.DownloadUserControl1_UseInUnpackButton_Click;
			this.UnpackUserControl1.UseAllInDecompileButton.Click += this.UnpackUserControl_UseAllInDecompileButton_Click;
			this.UnpackUserControl1.UseInPreviewButton.Click += this.UnpackUserControl_UseInPreviewButton_Click;
			this.UnpackUserControl1.UseInDecompileButton.Click += this.UnpackUserControl_UseInDecompileButton_Click;
			this.PreviewViewUserControl.SetUpGameButton.Click += this.PreviewSetUpGamesButton_Click;
			this.PreviewViewUserControl.UseInDecompileButton.Click += this.ViewUserControl_UseInDecompileButton_Click;
			this.DecompilerUserControl1.UseAllInCompileButton.Click += this.DecompilerUserControl1_UseAllInCompileButton_Click;
			//AddHandler Me.DecompilerUserControl1.UseInEditButton.Click, AddressOf Me.DecompilerUserControl1_UseInEditButton_Click
			this.DecompilerUserControl1.UseInCompileButton.Click += this.DecompilerUserControl1_UseInCompileButton_Click;
			this.CompilerUserControl1.EditGameSetupButton.Click += this.CompileSetUpGamesButton_Click;
			//AddHandler Me.CompilerUserControl1.UseAllInPackButton.Click, AddressOf Me.CompilerUserControl1_UseAllInPackButton_Click
			this.CompilerUserControl1.UseInViewButton.Click += this.CompilerUserControl1_UseInViewButton_Click;
			//AddHandler Me.CompilerUserControl1.UseInPackButton.Click, AddressOf Me.CompilerUserControl1_UseInPackButton_Click
			this.ViewViewUserControl.SetUpGameButton.Click += this.ViewSetUpGamesButton_Click;
			this.ViewViewUserControl.UseInDecompileButton.Click += this.ViewUserControl_UseInDecompileButton_Click;
			this.PackUserControl1.SetUpGamesButton.Click += this.PackSetUpGamesButton_Click;
			this.PackUserControl1.UseAllInPublishButton.Click += this.PackUserControl1_UseAllInPublishButton_Click;
			this.PublishUserControl1.UseInDownloadToolStripMenuItem.Click += this.PublishUserControl1_UseInDownloadToolStripMenuItem_Click;
			this.UpdateUserControl1.UpdateAvailable += this.UpdateUserControl1_UpdateAvailable;

			this.UpdateUserControl1.CheckForUpdate();
		}

		private void Free()
		{
			this.SetUpGamesUserControl1.GoBackButton.Click -= this.SetUpGamesGoBackButton_Click;
			this.DownloadUserControl1.UseInUnpackButton.Click -= this.DownloadUserControl1_UseInUnpackButton_Click;
			this.UnpackUserControl1.UseAllInDecompileButton.Click -= this.UnpackUserControl_UseAllInDecompileButton_Click;
			this.UnpackUserControl1.UseInPreviewButton.Click -= this.UnpackUserControl_UseInPreviewButton_Click;
			this.UnpackUserControl1.UseInDecompileButton.Click -= this.UnpackUserControl_UseInDecompileButton_Click;
			this.PreviewViewUserControl.SetUpGameButton.Click -= this.PreviewSetUpGamesButton_Click;
			this.PreviewViewUserControl.UseInDecompileButton.Click -= this.ViewUserControl_UseInDecompileButton_Click;
			this.DecompilerUserControl1.UseAllInCompileButton.Click -= this.DecompilerUserControl1_UseAllInCompileButton_Click;
			//RemoveHandler Me.DecompilerUserControl1.UseInEditButton.Click, AddressOf Me.DecompilerUserControl1_UseInEditButton_Click
			this.DecompilerUserControl1.UseInCompileButton.Click -= this.DecompilerUserControl1_UseInCompileButton_Click;
			this.CompilerUserControl1.EditGameSetupButton.Click -= this.CompileSetUpGamesButton_Click;
			//RemoveHandler Me.CompilerUserControl1.UseAllInPackButton.Click, AddressOf Me.CompilerUserControl1_UseAllInPackButton_Click
			this.CompilerUserControl1.UseInViewButton.Click -= this.CompilerUserControl1_UseInViewButton_Click;
			//RemoveHandler Me.CompilerUserControl1.UseInPackButton.Click, AddressOf Me.CompilerUserControl1_UseInPackButton_Click
			this.ViewViewUserControl.SetUpGameButton.Click -= this.ViewSetUpGamesButton_Click;
			this.ViewViewUserControl.UseInDecompileButton.Click -= this.ViewUserControl_UseInDecompileButton_Click;
			this.PackUserControl1.SetUpGamesButton.Click -= this.PackSetUpGamesButton_Click;
			this.PackUserControl1.UseAllInPublishButton.Click -= this.PackUserControl1_UseAllInPublishButton_Click;
			this.PublishUserControl1.UseInDownloadToolStripMenuItem.Click -= this.PublishUserControl1_UseInDownloadToolStripMenuItem_Click;
			this.UpdateUserControl1.UpdateAvailable -= this.UpdateUserControl1_UpdateAvailable;

			if (this.WindowState == FormWindowState.Normal)
			{
				MainCROWBAR.TheApp.Settings.WindowLocation = this.Location;
				MainCROWBAR.TheApp.Settings.WindowSize = this.Size;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.WindowLocation = this.RestoreBounds.Location;
				MainCROWBAR.TheApp.Settings.WindowSize = this.RestoreBounds.Size;
			}
			MainCROWBAR.TheApp.Settings.WindowState = this.WindowState;
			MainCROWBAR.TheApp.Settings.MainWindowSelectedTabIndex = this.MainTabControl.SelectedIndex;
		}

#endregion

#region Properties

#endregion

#region Methods

		public void Startup(ReadOnlyCollection<string> commandLineValues)
		{
			if (commandLineValues.Count > 1)
			{
				string command = commandLineValues[1];
				if (!string.IsNullOrEmpty(command) && !MainCROWBAR.TheApp.CommandLineValueIsAnAppSetting(command))
				{
					this.SetDroppedPathFileName(true, command);

					//'TEST: Every file selected and dropped onto EXE is a string in the array, starting at index 1. Index 0 is the EXE path file name.
					//Dim text As New StringBuilder()
					//For Each arg As String In commandLineParams
					//	text.AppendLine(arg)
					//Next
					//MessageBox.Show(text.ToString())
				}
			}
		}

#endregion

#region Widget Event Handlers

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			this.Init();

			//TEST [UNHANDLED EXCEPTION] Use these lines to raise an exception and show the unhandled exception window.
			//Dim documentsPath As String
			//documentsPath = Path.Combine("debug", "<")
		}

		private void MainForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			this.Free();
		}

		private void MainForm_DragEnter(System.Object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else if (e.Data.GetDataPresent(DataFormats.Html))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void MainForm_DragDrop(System.Object sender, System.Windows.Forms.DragEventArgs e)
		{
			//NOTE: Multiple pathFileNames is filled with all selected file names from Windows Explorer.
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] pathFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
				this.SetDroppedPathFileName(false, pathFileNames[0]);
			}
			else if (e.Data.GetDataPresent(DataFormats.Text))
			{
				string urlText = (e.Data.GetData(DataFormats.Text) == null ? null : Convert.ToString(e.Data.GetData(DataFormats.Text)));
				this.SetDroppedUrlText(false, urlText);
				//ElseIf e.Data.GetDataPresent(DataFormats.Html) Then
				//	'Version:0.9
				//	'StartHTML:00000145
				//	'EndHTML:00000392
				//	'StartFragment:00000179
				//	'EndFragment:00000356
				//	'SourceURL:chrome://browser/content/browser.xul
				//	'<html><body>
				//	'<!--StartFragment--><a href="https://steamcommunity.com/sharedfiles/filedetails/?id=1777079625&tscn=1561212913">https://steamcommunity.com/sharedfiles/filedetails/?id=1777079625&tscn=1561212913</a><!--EndFragment-->
				//	'</body>
				//	'</html>
				//	Dim urlText As String = CType(e.Data.GetData(DataFormats.Html).ToString(), String)
				//	Me.SetDroppedPathFileName(False, urlText)
			}
		}

#endregion

#region Child Widget Event Handlers

		private void SetUpGamesGoBackButton_Click(object sender, EventArgs e)
		{
			int gameSetupIndex = MainCROWBAR.TheApp.Settings.SetUpGamesGameSetupSelectedIndex;

			if (this.theTabThatCalledSetUpGames == this.PreviewTabPage)
			{
				MainCROWBAR.TheApp.Settings.PreviewGameSetupSelectedIndex = gameSetupIndex;
			}
			else if (this.theTabThatCalledSetUpGames == this.CompileTabPage)
			{
				MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex = gameSetupIndex;
			}
			else if (this.theTabThatCalledSetUpGames == this.ViewTabPage)
			{
				MainCROWBAR.TheApp.Settings.ViewGameSetupSelectedIndex = gameSetupIndex;
			}

			this.SetUpGamesUserControl1.GoBackButton.Enabled = false;
			this.MainTabControl.SelectTab(this.theTabThatCalledSetUpGames);
		}

		private void DownloadUserControl1_UseInUnpackButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.UnpackTabPage);
		}

		private void UnpackUserControl_UseAllInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.DecompileTabPage);
		}

		private void UnpackUserControl_UseInPreviewButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.PreviewTabPage);
		}

		private void UnpackUserControl_UseInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.DecompileTabPage);
		}

		private void PreviewSetUpGamesButton_Click(System.Object sender, System.EventArgs e)
		{
			this.theTabThatCalledSetUpGames = this.PreviewTabPage;
			this.SelectSetUpGamesFromAnotherTab(MainCROWBAR.TheApp.Settings.PreviewGameSetupSelectedIndex);
		}

		private void DecompilerUserControl1_UseAllInCompileButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.CompileTabPage);
		}

		//Private Sub DecompilerUserControl1_UseInEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	Me.MainTabControl.SelectTab(Me.EditTabPage)
		//End Sub

		private void DecompilerUserControl1_UseInCompileButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.CompileTabPage);
		}

		private void CompileSetUpGamesButton_Click(System.Object sender, System.EventArgs e)
		{
			this.theTabThatCalledSetUpGames = this.CompileTabPage;
			this.SelectSetUpGamesFromAnotherTab(MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex);
		}

		//Private Sub CompilerUserControl1_UseAllInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	Me.MainTabControl.SelectTab(Me.PackTabPage)
		//End Sub

		private void CompilerUserControl1_UseInViewButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.ViewTabPage);
		}

		//Private Sub CompilerUserControl1_UseInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	Me.MainTabControl.SelectTab(Me.PackTabPage)
		//End Sub

		private void ViewSetUpGamesButton_Click(System.Object sender, System.EventArgs e)
		{
			this.theTabThatCalledSetUpGames = this.ViewTabPage;
			this.SelectSetUpGamesFromAnotherTab(MainCROWBAR.TheApp.Settings.ViewGameSetupSelectedIndex);
		}

		private void ViewUserControl_UseInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.DecompileTabPage);
		}

		private void PackSetUpGamesButton_Click(System.Object sender, System.EventArgs e)
		{
			this.theTabThatCalledSetUpGames = this.PackTabPage;
			this.SelectSetUpGamesFromAnotherTab(MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex);
		}

		private void PackUserControl1_UseAllInPublishButton_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.PublishTabPage);
		}

		private void PublishUserControl1_UseInDownloadToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			this.MainTabControl.SelectTab(this.DownloadTabPage);
		}

		private void UpdateUserControl1_UpdateAvailable(System.Object sender, UpdateUserControl.UpdateAvailableEventArgs e)
		{
			if (e.UpdateIsAvailable)
			{
				this.UpdateTabPage.Text = "Update Available";
			}
			else
			{
				this.UpdateTabPage.Text = "Update";
			}
		}

#endregion

#region Core Event Handlers

#endregion

#region Private Methods

		private void SetDroppedPathFileName(bool setViaAutoOpen, string pathFileName)
		{
			string extension = "";

			extension = Path.GetExtension(pathFileName).ToLower();
			if (extension == ".url")
			{
				string[] fileLines = File.ReadAllLines(pathFileName);
				if (fileLines[0] == "[InternetShortcut]")
				{
					foreach (string line in fileLines)
					{
						if (line.StartsWith("URL="))
						{
							string urlText = line.Replace("URL=", "");
							this.MainTabControl.SelectTab(this.DownloadTabPage);
							this.DownloadUserControl1.ItemIdTextBox.Text = urlText;
							break;
						}
					}
				}
			}
			else if (extension == ".vpk")
			{
				AppEnums.ActionType vpkAction = AppEnums.ActionType.Unknown;
				if (setViaAutoOpen)
				{
					if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileIsChecked)
					{
						vpkAction = MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileOption;
					}
				}
				else
				{
					if (this.MainTabControl.SelectedTab == this.UnpackTabPage)
					{
						vpkAction = AppEnums.ActionType.Unpack;
					}
					else if (this.MainTabControl.SelectedTab == this.PublishTabPage)
					{
						vpkAction = AppEnums.ActionType.Publish;
					}
					else
					{
						vpkAction = MainCROWBAR.TheApp.Settings.OptionsDragAndDropVpkFileOption;
					}
				}
				if (vpkAction == AppEnums.ActionType.Unpack)
				{
					MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName = pathFileName;
					this.MainTabControl.SelectTab(this.UnpackTabPage);
					this.UnpackUserControl1.RunUnpackerToGetListOfPackageContents();
				}
				else if (vpkAction == AppEnums.ActionType.Publish)
				{
					this.MainTabControl.SelectTab(this.PublishTabPage);
					this.PublishUserControl1.ItemContentPathFileNameTextBox.Text = pathFileName;
					this.PublishUserControl1.ItemContentPathFileNameTextBox.DataBindings["Text"].WriteValue();
				}
			}
			else if (extension == ".gma")
			{
				AppEnums.ActionType vpkAction = AppEnums.ActionType.Unknown;
				if (setViaAutoOpen)
				{
					if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileIsChecked)
					{
						vpkAction = MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileOption;
					}
				}
				else
				{
					if (this.MainTabControl.SelectedTab == this.UnpackTabPage)
					{
						vpkAction = AppEnums.ActionType.Unpack;
					}
					else if (this.MainTabControl.SelectedTab == this.PublishTabPage)
					{
						vpkAction = AppEnums.ActionType.Publish;
					}
					else
					{
						vpkAction = MainCROWBAR.TheApp.Settings.OptionsDragAndDropGmaFileOption;
					}
				}
				if (vpkAction == AppEnums.ActionType.Unpack)
				{
					MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName = pathFileName;
					this.MainTabControl.SelectTab(this.UnpackTabPage);
					this.UnpackUserControl1.RunUnpackerToGetListOfPackageContents();
				}
				else if (vpkAction == AppEnums.ActionType.Publish)
				{
					this.MainTabControl.SelectTab(this.PublishTabPage);
					this.PublishUserControl1.ItemContentPathFileNameTextBox.Text = pathFileName;
					this.PublishUserControl1.ItemContentPathFileNameTextBox.DataBindings["Text"].WriteValue();
				}
			}
			else if (extension == ".fpx")
			{
				AppEnums.ActionType vpkAction = AppEnums.ActionType.Unknown;
				if (setViaAutoOpen)
				{
					if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFpxFileIsChecked)
					{
						vpkAction = AppEnums.ActionType.Unpack;
					}
				}
				else
				{
					vpkAction = AppEnums.ActionType.Unpack;
				}
				if (vpkAction == AppEnums.ActionType.Unpack)
				{
					MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName = pathFileName;
					this.MainTabControl.SelectTab(this.UnpackTabPage);
					this.UnpackUserControl1.RunUnpackerToGetListOfPackageContents();
				}
			}
			else if (extension == ".mdl")
			{
				this.SetMdlDrop(setViaAutoOpen, pathFileName);
				//ElseIf extension = ".exe" Then
				//	Me.SetCompilerExePathFileName(pathFileName)
			}
			else if (extension == ".qc")
			{
				AppEnums.ActionType qcAction = AppEnums.ActionType.Unknown;
				if (setViaAutoOpen)
				{
					if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenQcFileIsChecked)
					{
						qcAction = AppEnums.ActionType.Compile;
					}
				}
				else
				{
					qcAction = AppEnums.ActionType.Compile;
				}
				if (qcAction == AppEnums.ActionType.Compile)
				{
					MainCROWBAR.TheApp.Settings.CompileQcPathFileName = pathFileName;
					this.MainTabControl.SelectTab(this.CompileTabPage);
				}
			}
			else if (extension == ".bmp" || extension == ".gif" || extension == ".jpeg" || extension == ".jpg" || extension == ".png" || extension == ".wmf")
			{
				this.MainTabControl.SelectTab(this.PublishTabPage);
				this.PublishUserControl1.ItemPreviewImagePathFileNameTextBox.Text = pathFileName;
				this.PublishUserControl1.ItemPreviewImagePathFileNameTextBox.DataBindings["Text"].WriteValue();
			}
			else
			{
				this.SetFolderDrop(setViaAutoOpen, pathFileName);
			}
		}

		private void SetDroppedUrlText(bool setViaAutoOpen, string urlText)
		{
			this.MainTabControl.SelectTab(this.DownloadTabPage);
			this.DownloadUserControl1.ItemIdTextBox.Text = urlText;
		}

		private void SetMdlDrop(bool setViaAutoOpen, string pathFileName)
		{
			AppEnums.ActionType mdlAction = AppEnums.ActionType.Unknown;

			if (setViaAutoOpen)
			{
				if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileIsChecked)
				{
					mdlAction = MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption;

					if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileForPreviewIsChecked)
					{
						MainCROWBAR.TheApp.Settings.PreviewMdlPathFileName = pathFileName;
					}
					if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileForDecompileIsChecked)
					{
						MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = pathFileName;
					}
					if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileForViewIsChecked)
					{
						MainCROWBAR.TheApp.Settings.ViewMdlPathFileName = pathFileName;
					}
				}
			}
			else
			{
				if (this.MainTabControl.SelectedTab == this.PreviewTabPage)
				{
					mdlAction = AppEnums.ActionType.Preview;
					MainCROWBAR.TheApp.Settings.PreviewMdlPathFileName = pathFileName;
					if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForPreviewIsChecked)
					{
						this.SetMdlDropSetUp(pathFileName);
					}
				}
				else if (this.MainTabControl.SelectedTab == this.DecompileTabPage)
				{
					mdlAction = AppEnums.ActionType.Decompile;
					MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = pathFileName;
					if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForDecompileIsChecked)
					{
						this.SetMdlDropSetUp(pathFileName);
					}
				}
				else if (this.MainTabControl.SelectedTab == this.ViewTabPage)
				{
					mdlAction = AppEnums.ActionType.View;
					MainCROWBAR.TheApp.Settings.ViewMdlPathFileName = pathFileName;
					if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForViewIsChecked)
					{
						this.SetMdlDropSetUp(pathFileName);
					}
				}
				else
				{
					mdlAction = MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption;
					this.SetMdlDropSetUp(pathFileName);
				}
			}

			if (mdlAction == AppEnums.ActionType.Preview)
			{
				this.MainTabControl.SelectTab(this.PreviewTabPage);
			}
			else if (mdlAction == AppEnums.ActionType.Decompile)
			{
				this.MainTabControl.SelectTab(this.DecompileTabPage);
			}
			else if (mdlAction == AppEnums.ActionType.View)
			{
				this.MainTabControl.SelectTab(this.ViewTabPage);
			}
		}

		private void SetMdlDropSetUp(string pathFileName)
		{
			if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForPreviewIsChecked)
			{
				MainCROWBAR.TheApp.Settings.PreviewMdlPathFileName = pathFileName;
			}
			if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForDecompileIsChecked)
			{
				MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = pathFileName;
			}
			if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForViewIsChecked)
			{
				MainCROWBAR.TheApp.Settings.ViewMdlPathFileName = pathFileName;
			}
		}

		private void SetFolderDrop(bool setViaAutoOpen, string pathFileName)
		{
			AppEnums.ActionType folderAction = AppEnums.ActionType.Unknown;
			TabPage selectedTab = null;

			try
			{
				if (setViaAutoOpen)
				{
					folderAction = MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption;
				}
				else
				{
					folderAction = MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption;
					selectedTab = this.MainTabControl.SelectedTab;
					if (selectedTab == this.PublishTabPage)
					{
						folderAction = AppEnums.ActionType.Publish;
					}
					else if (selectedTab == this.UnpackTabPage)
					{
						folderAction = AppEnums.ActionType.Unpack;
					}
					else if (selectedTab == this.DecompileTabPage)
					{
						folderAction = AppEnums.ActionType.Decompile;
					}
					else if (selectedTab == this.CompileTabPage)
					{
						folderAction = AppEnums.ActionType.Compile;
					}
					else if (selectedTab == this.PackTabPage)
					{
						folderAction = AppEnums.ActionType.Pack;
					}
					else
					{
						selectedTab = null;
					}
				}

				if (selectedTab == null && Directory.Exists(pathFileName))
				{
					List<string> packageExtensions = BasePackageFile.GetListOfPackageExtensions();
					foreach (string packageExtension in packageExtensions)
					{
						foreach (string anArchivePathFileName in Directory.GetFiles(pathFileName, packageExtension))
						{
							if (anArchivePathFileName.Length > 0)
							{
								folderAction = AppEnums.ActionType.Unpack;
								break;
							}
						}
						if (folderAction == AppEnums.ActionType.Unpack)
						{
							break;
						}
					}
					if (folderAction != AppEnums.ActionType.Unpack)
					{
						if (Directory.GetFiles(pathFileName, "*.mdl").Length > 0)
						{
							folderAction = AppEnums.ActionType.Decompile;
						}
						else
						{
							if (Directory.GetFiles(pathFileName, "*.qc").Length > 0)
							{
								folderAction = AppEnums.ActionType.Compile;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			if (folderAction == AppEnums.ActionType.Unpack)
			{
				MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName = pathFileName;
				this.MainTabControl.SelectTab(this.UnpackTabPage);
			}
			else if (folderAction == AppEnums.ActionType.Decompile)
			{
				MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = pathFileName;
				this.MainTabControl.SelectTab(this.DecompileTabPage);
			}
			else if (folderAction == AppEnums.ActionType.Compile)
			{
				MainCROWBAR.TheApp.Settings.CompileQcPathFileName = pathFileName;
				this.MainTabControl.SelectTab(this.CompileTabPage);
			}
			else if (folderAction == AppEnums.ActionType.Pack)
			{
				MainCROWBAR.TheApp.Settings.PackInputPath = pathFileName;
				this.MainTabControl.SelectTab(this.PackTabPage);
			}
			else if (folderAction == AppEnums.ActionType.Publish)
			{
				this.PublishUserControl1.ItemContentPathFileNameTextBox.Text = pathFileName;
				this.PublishUserControl1.ItemContentPathFileNameTextBox.DataBindings["Text"].WriteValue();
				this.MainTabControl.SelectTab(this.PublishTabPage);
			}
		}

		private void SelectSetUpGamesFromAnotherTab(int gameSetupIndex)
		{
			MainCROWBAR.TheApp.Settings.SetUpGamesGameSetupSelectedIndex = gameSetupIndex;

			this.SetUpGamesUserControl1.GoBackButton.Enabled = true;
			this.MainTabControl.SelectTab(this.SetUpGamesTabPage);
		}

#endregion

#region Data

		private TabPage theTabThatCalledSetUpGames;

#endregion


		private static MainForm _DefaultInstance;
		public static MainForm DefaultInstance
		{
			get
			{
				if (_DefaultInstance == null || _DefaultInstance.IsDisposed)
					_DefaultInstance = new MainForm();

				return _DefaultInstance;
			}
		}
	}

}