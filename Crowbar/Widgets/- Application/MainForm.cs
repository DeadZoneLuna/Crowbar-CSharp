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
				Location = MainCROWBAR.TheApp.Settings.WindowLocation;
				Size = MainCROWBAR.TheApp.Settings.WindowSize;
				WindowState = MainCROWBAR.TheApp.Settings.WindowState;

				// Ensure minimum size of window. 
				//     Usually MinimumSize Handles this, but MinimumSize changes when Windows Theme Message Box Font Is something weird Like "SimSun-ExtB" 8pt.
				//     Thus, set the minimum size manually here.
				int minimumWidth = 800;
				int minimumHeight = 600;
				if (MinimumSize.Width < minimumWidth || MinimumSize.Height < minimumHeight)
				{
					MinimumSize = new Size(minimumWidth, minimumHeight);
				}
				if (Width < MinimumSize.Width)
				{
					Width = MinimumSize.Width;
				}
				if (Height < MinimumSize.Height)
				{
					Height = MinimumSize.Height;
				}

				if (MainCROWBAR.TheApp.CommandLineOption_Settings_IsEnabled)
				{
					MainCROWBAR.TheApp.Settings.MainWindowSelectedTabIndex = MainTabControl.TabPages.IndexOf(UpdateTabPage);
				}
				MainTabControl.SelectedIndex = MainCROWBAR.TheApp.Settings.MainWindowSelectedTabIndex;

				Screen aScreen = Screen.FromControl(this);
				//WorkingArea means the area of the screen without the Windows taskbar.
				Rectangle aScreenWorkingArea = aScreen.WorkingArea;
				// Ensure at least 60 px of Title Bar visible
				if (Location.X < aScreenWorkingArea.Left || Location.X + 60 > aScreenWorkingArea.Left + aScreenWorkingArea.Width)
				{
					Left = aScreenWorkingArea.Left;
				}
				// Ensure top visible
				if (Location.Y < aScreenWorkingArea.Top || Location.Y + Size.Height > aScreenWorkingArea.Top + aScreenWorkingArea.Height)
				{
					Top = aScreenWorkingArea.Top;
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
			Startup(commandLineValues);

			PreviewViewUserControl.RunDataViewer();
			ViewViewUserControl.RunDataViewer();

			SetUpGamesUserControl1.GoBackButton.Click += SetUpGamesGoBackButton_Click;
			DownloadUserControl1.UseInUnpackButton.Click += DownloadUserControl1_UseInUnpackButton_Click;
			UnpackUserControl1.UseAllInDecompileButton.Click += UnpackUserControl_UseAllInDecompileButton_Click;
			UnpackUserControl1.UseInPreviewButton.Click += UnpackUserControl_UseInPreviewButton_Click;
			UnpackUserControl1.UseInDecompileButton.Click += UnpackUserControl_UseInDecompileButton_Click;
			PreviewViewUserControl.SetUpGameButton.Click += PreviewSetUpGamesButton_Click;
			PreviewViewUserControl.UseInDecompileButton.Click += ViewUserControl_UseInDecompileButton_Click;
			DecompilerUserControl1.UseAllInCompileButton.Click += DecompilerUserControl1_UseAllInCompileButton_Click;
			//AddHandler Me.DecompilerUserControl1.UseInEditButton.Click, AddressOf Me.DecompilerUserControl1_UseInEditButton_Click
			DecompilerUserControl1.UseInCompileButton.Click += DecompilerUserControl1_UseInCompileButton_Click;
			CompilerUserControl1.EditGameSetupButton.Click += CompileSetUpGamesButton_Click;
			//AddHandler Me.CompilerUserControl1.UseAllInPackButton.Click, AddressOf Me.CompilerUserControl1_UseAllInPackButton_Click
			CompilerUserControl1.UseInViewButton.Click += CompilerUserControl1_UseInViewButton_Click;
			//AddHandler Me.CompilerUserControl1.UseInPackButton.Click, AddressOf Me.CompilerUserControl1_UseInPackButton_Click
			ViewViewUserControl.SetUpGameButton.Click += ViewSetUpGamesButton_Click;
			ViewViewUserControl.UseInDecompileButton.Click += ViewUserControl_UseInDecompileButton_Click;
			PackUserControl1.SetUpGamesButton.Click += PackSetUpGamesButton_Click;
			PackUserControl1.UseAllInPublishButton.Click += PackUserControl1_UseAllInPublishButton_Click;
			PublishUserControl1.UseInDownloadToolStripMenuItem.Click += PublishUserControl1_UseInDownloadToolStripMenuItem_Click;
			UpdateUserControl1.UpdateAvailable += UpdateUserControl1_UpdateAvailable;

			UpdateUserControl1.CheckForUpdate();
		}

		private void Free()
		{
			SetUpGamesUserControl1.GoBackButton.Click -= SetUpGamesGoBackButton_Click;
			DownloadUserControl1.UseInUnpackButton.Click -= DownloadUserControl1_UseInUnpackButton_Click;
			UnpackUserControl1.UseAllInDecompileButton.Click -= UnpackUserControl_UseAllInDecompileButton_Click;
			UnpackUserControl1.UseInPreviewButton.Click -= UnpackUserControl_UseInPreviewButton_Click;
			UnpackUserControl1.UseInDecompileButton.Click -= UnpackUserControl_UseInDecompileButton_Click;
			PreviewViewUserControl.SetUpGameButton.Click -= PreviewSetUpGamesButton_Click;
			PreviewViewUserControl.UseInDecompileButton.Click -= ViewUserControl_UseInDecompileButton_Click;
			DecompilerUserControl1.UseAllInCompileButton.Click -= DecompilerUserControl1_UseAllInCompileButton_Click;
			//RemoveHandler Me.DecompilerUserControl1.UseInEditButton.Click, AddressOf Me.DecompilerUserControl1_UseInEditButton_Click
			DecompilerUserControl1.UseInCompileButton.Click -= DecompilerUserControl1_UseInCompileButton_Click;
			CompilerUserControl1.EditGameSetupButton.Click -= CompileSetUpGamesButton_Click;
			//RemoveHandler Me.CompilerUserControl1.UseAllInPackButton.Click, AddressOf Me.CompilerUserControl1_UseAllInPackButton_Click
			CompilerUserControl1.UseInViewButton.Click -= CompilerUserControl1_UseInViewButton_Click;
			//RemoveHandler Me.CompilerUserControl1.UseInPackButton.Click, AddressOf Me.CompilerUserControl1_UseInPackButton_Click
			ViewViewUserControl.SetUpGameButton.Click -= ViewSetUpGamesButton_Click;
			ViewViewUserControl.UseInDecompileButton.Click -= ViewUserControl_UseInDecompileButton_Click;
			PackUserControl1.SetUpGamesButton.Click -= PackSetUpGamesButton_Click;
			PackUserControl1.UseAllInPublishButton.Click -= PackUserControl1_UseAllInPublishButton_Click;
			PublishUserControl1.UseInDownloadToolStripMenuItem.Click -= PublishUserControl1_UseInDownloadToolStripMenuItem_Click;
			UpdateUserControl1.UpdateAvailable -= UpdateUserControl1_UpdateAvailable;

			if (WindowState == FormWindowState.Normal)
			{
				MainCROWBAR.TheApp.Settings.WindowLocation = Location;
				MainCROWBAR.TheApp.Settings.WindowSize = Size;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.WindowLocation = RestoreBounds.Location;
				MainCROWBAR.TheApp.Settings.WindowSize = RestoreBounds.Size;
			}
			MainCROWBAR.TheApp.Settings.WindowState = WindowState;
			MainCROWBAR.TheApp.Settings.MainWindowSelectedTabIndex = MainTabControl.SelectedIndex;
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
					SetDroppedPathFileName(true, command);

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
			Init();

			//TEST [UNHANDLED EXCEPTION] Use these lines to raise an exception and show the unhandled exception window.
			//Dim documentsPath As String
			//documentsPath = Path.Combine("debug", "<")
		}

		private void MainForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			Free();
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
				SetDroppedPathFileName(false, pathFileNames[0]);
			}
			else if (e.Data.GetDataPresent(DataFormats.Text))
			{
				string urlText = (e.Data.GetData(DataFormats.Text) == null ? null : Convert.ToString(e.Data.GetData(DataFormats.Text)));
				SetDroppedUrlText(false, urlText);
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

			if (theTabThatCalledSetUpGames == PreviewTabPage)
			{
				MainCROWBAR.TheApp.Settings.PreviewGameSetupSelectedIndex = gameSetupIndex;
			}
			else if (theTabThatCalledSetUpGames == CompileTabPage)
			{
				MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex = gameSetupIndex;
			}
			else if (theTabThatCalledSetUpGames == ViewTabPage)
			{
				MainCROWBAR.TheApp.Settings.ViewGameSetupSelectedIndex = gameSetupIndex;
			}

			SetUpGamesUserControl1.GoBackButton.Enabled = false;
			MainTabControl.SelectTab(theTabThatCalledSetUpGames);
		}

		private void DownloadUserControl1_UseInUnpackButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(UnpackTabPage);
		}

		private void UnpackUserControl_UseAllInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(DecompileTabPage);
		}

		private void UnpackUserControl_UseInPreviewButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(PreviewTabPage);
		}

		private void UnpackUserControl_UseInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(DecompileTabPage);
		}

		private void PreviewSetUpGamesButton_Click(System.Object sender, System.EventArgs e)
		{
			theTabThatCalledSetUpGames = PreviewTabPage;
			SelectSetUpGamesFromAnotherTab(MainCROWBAR.TheApp.Settings.PreviewGameSetupSelectedIndex);
		}

		private void DecompilerUserControl1_UseAllInCompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(CompileTabPage);
		}

		//Private Sub DecompilerUserControl1_UseInEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	Me.MainTabControl.SelectTab(Me.EditTabPage)
		//End Sub

		private void DecompilerUserControl1_UseInCompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(CompileTabPage);
		}

		private void CompileSetUpGamesButton_Click(System.Object sender, System.EventArgs e)
		{
			theTabThatCalledSetUpGames = CompileTabPage;
			SelectSetUpGamesFromAnotherTab(MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex);
		}

		//Private Sub CompilerUserControl1_UseAllInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	Me.MainTabControl.SelectTab(Me.PackTabPage)
		//End Sub

		private void CompilerUserControl1_UseInViewButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(ViewTabPage);
		}

		//Private Sub CompilerUserControl1_UseInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	Me.MainTabControl.SelectTab(Me.PackTabPage)
		//End Sub

		private void ViewSetUpGamesButton_Click(System.Object sender, System.EventArgs e)
		{
			theTabThatCalledSetUpGames = ViewTabPage;
			SelectSetUpGamesFromAnotherTab(MainCROWBAR.TheApp.Settings.ViewGameSetupSelectedIndex);
		}

		private void ViewUserControl_UseInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(DecompileTabPage);
		}

		private void PackSetUpGamesButton_Click(System.Object sender, System.EventArgs e)
		{
			theTabThatCalledSetUpGames = PackTabPage;
			SelectSetUpGamesFromAnotherTab(MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex);
		}

		private void PackUserControl1_UseAllInPublishButton_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(PublishTabPage);
		}

		private void PublishUserControl1_UseInDownloadToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			MainTabControl.SelectTab(DownloadTabPage);
		}

		private void UpdateUserControl1_UpdateAvailable(System.Object sender, UpdateUserControl.UpdateAvailableEventArgs e)
		{
			if (e.UpdateIsAvailable)
			{
				UpdateTabPage.Text = "Update Available";
			}
			else
			{
				UpdateTabPage.Text = "Update";
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
							MainTabControl.SelectTab(DownloadTabPage);
							DownloadUserControl1.ItemIdTextBox.Text = urlText;
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
					if (MainTabControl.SelectedTab == UnpackTabPage)
					{
						vpkAction = AppEnums.ActionType.Unpack;
					}
					else if (MainTabControl.SelectedTab == PublishTabPage)
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
					MainTabControl.SelectTab(UnpackTabPage);
					UnpackUserControl1.RunUnpackerToGetListOfPackageContents();
				}
				else if (vpkAction == AppEnums.ActionType.Publish)
				{
					MainTabControl.SelectTab(PublishTabPage);
					PublishUserControl1.ItemContentPathFileNameTextBox.Text = pathFileName;
					PublishUserControl1.ItemContentPathFileNameTextBox.DataBindings["Text"].WriteValue();
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
					if (MainTabControl.SelectedTab == UnpackTabPage)
					{
						vpkAction = AppEnums.ActionType.Unpack;
					}
					else if (MainTabControl.SelectedTab == PublishTabPage)
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
					MainTabControl.SelectTab(UnpackTabPage);
					UnpackUserControl1.RunUnpackerToGetListOfPackageContents();
				}
				else if (vpkAction == AppEnums.ActionType.Publish)
				{
					MainTabControl.SelectTab(PublishTabPage);
					PublishUserControl1.ItemContentPathFileNameTextBox.Text = pathFileName;
					PublishUserControl1.ItemContentPathFileNameTextBox.DataBindings["Text"].WriteValue();
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
					MainTabControl.SelectTab(UnpackTabPage);
					UnpackUserControl1.RunUnpackerToGetListOfPackageContents();
				}
			}
			else if (extension == ".mdl")
			{
				SetMdlDrop(setViaAutoOpen, pathFileName);
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
					MainTabControl.SelectTab(CompileTabPage);
				}
			}
			else if (extension == ".bmp" || extension == ".gif" || extension == ".jpeg" || extension == ".jpg" || extension == ".png" || extension == ".wmf")
			{
				MainTabControl.SelectTab(PublishTabPage);
				PublishUserControl1.ItemPreviewImagePathFileNameTextBox.Text = pathFileName;
				PublishUserControl1.ItemPreviewImagePathFileNameTextBox.DataBindings["Text"].WriteValue();
			}
			else
			{
				SetFolderDrop(setViaAutoOpen, pathFileName);
			}
		}

		private void SetDroppedUrlText(bool setViaAutoOpen, string urlText)
		{
			MainTabControl.SelectTab(DownloadTabPage);
			DownloadUserControl1.ItemIdTextBox.Text = urlText;
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
				if (MainTabControl.SelectedTab == PreviewTabPage)
				{
					mdlAction = AppEnums.ActionType.Preview;
					MainCROWBAR.TheApp.Settings.PreviewMdlPathFileName = pathFileName;
					if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForPreviewIsChecked)
					{
						SetMdlDropSetUp(pathFileName);
					}
				}
				else if (MainTabControl.SelectedTab == DecompileTabPage)
				{
					mdlAction = AppEnums.ActionType.Decompile;
					MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = pathFileName;
					if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForDecompileIsChecked)
					{
						SetMdlDropSetUp(pathFileName);
					}
				}
				else if (MainTabControl.SelectedTab == ViewTabPage)
				{
					mdlAction = AppEnums.ActionType.View;
					MainCROWBAR.TheApp.Settings.ViewMdlPathFileName = pathFileName;
					if (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileForViewIsChecked)
					{
						SetMdlDropSetUp(pathFileName);
					}
				}
				else
				{
					mdlAction = MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption;
					SetMdlDropSetUp(pathFileName);
				}
			}

			if (mdlAction == AppEnums.ActionType.Preview)
			{
				MainTabControl.SelectTab(PreviewTabPage);
			}
			else if (mdlAction == AppEnums.ActionType.Decompile)
			{
				MainTabControl.SelectTab(DecompileTabPage);
			}
			else if (mdlAction == AppEnums.ActionType.View)
			{
				MainTabControl.SelectTab(ViewTabPage);
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
					selectedTab = MainTabControl.SelectedTab;
					if (selectedTab == PublishTabPage)
					{
						folderAction = AppEnums.ActionType.Publish;
					}
					else if (selectedTab == UnpackTabPage)
					{
						folderAction = AppEnums.ActionType.Unpack;
					}
					else if (selectedTab == DecompileTabPage)
					{
						folderAction = AppEnums.ActionType.Decompile;
					}
					else if (selectedTab == CompileTabPage)
					{
						folderAction = AppEnums.ActionType.Compile;
					}
					else if (selectedTab == PackTabPage)
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
				MainTabControl.SelectTab(UnpackTabPage);
			}
			else if (folderAction == AppEnums.ActionType.Decompile)
			{
				MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = pathFileName;
				MainTabControl.SelectTab(DecompileTabPage);
			}
			else if (folderAction == AppEnums.ActionType.Compile)
			{
				MainCROWBAR.TheApp.Settings.CompileQcPathFileName = pathFileName;
				MainTabControl.SelectTab(CompileTabPage);
			}
			else if (folderAction == AppEnums.ActionType.Pack)
			{
				MainCROWBAR.TheApp.Settings.PackInputPath = pathFileName;
				MainTabControl.SelectTab(PackTabPage);
			}
			else if (folderAction == AppEnums.ActionType.Publish)
			{
				PublishUserControl1.ItemContentPathFileNameTextBox.Text = pathFileName;
				PublishUserControl1.ItemContentPathFileNameTextBox.DataBindings["Text"].WriteValue();
				MainTabControl.SelectTab(PublishTabPage);
			}
		}

		private void SelectSetUpGamesFromAnotherTab(int gameSetupIndex)
		{
			MainCROWBAR.TheApp.Settings.SetUpGamesGameSetupSelectedIndex = gameSetupIndex;

			SetUpGamesUserControl1.GoBackButton.Enabled = true;
			MainTabControl.SelectTab(SetUpGamesTabPage);
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