using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Crowbar
{
	public partial class UnpackUserControl
	{

		private bool InstanceFieldsInitialized = false;

			private void InitializeInstanceFields()
			{
				DeleteSearchToolStripMenuItem = new ToolStripMenuItem("Delete search");
				DeleteAllSearchesToolStripMenuItem = new ToolStripMenuItem("Delete all searches");
			}

#region Creation and Destruction

		public UnpackUserControl()
		{
			// This call is required by the Windows Form Designer.
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			InitializeComponent();

			CustomMenu = new ContextMenuStrip();
			CustomMenu.Items.Add(DeleteSearchToolStripMenuItem);
			CustomMenu.Items.Add(DeleteAllSearchesToolStripMenuItem);
			PackageTreeView.ContextMenuStrip = CustomMenu;

			theSearchCount = 0;

			//NOTE: Try-Catch is needed so that widget will be shown in MainForm Designer without raising exception.
			try
			{
				Init();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

#endregion

#region Init and Free

		private void Init()
		{
			thePackageFileNames = new BindingListEx<PackagePathFileNameInfo>();

			PackagePathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "UnpackPackagePathFolderOrFileName", false, DataSourceUpdateMode.OnValidation);

			OutputPathTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "UnpackOutputFullPath", false, DataSourceUpdateMode.OnValidation);
			OutputSamePathTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "UnpackOutputSamePath", false, DataSourceUpdateMode.OnValidation);
			OutputSubfolderTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "UnpackOutputSubfolderName", false, DataSourceUpdateMode.OnValidation);
			UpdateOutputPathComboBox();
			UpdateOutputPathWidgets();

			//NOTE: Adding folder icon here means it is first in the image list, which is the icon used by default 
			Bitmap anIcon = Win32Api.GetShellIcon("folder", Win32Api.FILE_ATTRIBUTE_DIRECTORY);
			ImageList1.Images.Add("<Folder>", anIcon);
			//NOTE: The TreeView.Sorted property does not show in Intellisense or Properties window.
			PackageTreeView.Sorted = true;
			PackageTreeView.TreeViewNodeSorter = new NodeSorter();
			//Me.PackageTreeView.Nodes.Add("<root>", "<root>")

			PackageListView.Columns.Add("Name", 100);
			PackageListView.Columns.Add("Size (bytes)", 100);
			PackageListView.Columns.Add("Count", 50);
			PackageListView.Columns.Add("Type", 100);
			PackageListView.Columns.Add("Extension", 100);
			PackageListView.Columns.Add("Archive", 100);
			theSortColumnIndex = 0;
			PackageListView.ListViewItemSorter = new FolderAndFileListViewItemComparer(theSortColumnIndex, PackageListView.Sorting);

			SearchToolStripComboBox.ComboBox.DisplayMember = "Value";
			SearchToolStripComboBox.ComboBox.ValueMember = "Key";
			SearchToolStripComboBox.ComboBox.DataSource = EnumHelper.ToList(typeof(AppEnums.UnpackSearchFieldOptions));
			SearchToolStripComboBox.ComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "UnpackSearchField", false, DataSourceUpdateMode.OnPropertyChanged);

			//'NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
			//Me.GameSetupComboBox.DisplayMember = "GameName"
			//Me.GameSetupComboBox.ValueMember = "GameName"
			//Me.GameSetupComboBox.DataSource = TheApp.Settings.GameSetups
			//Me.GameSetupComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, "UnpackGameSetupSelectedIndex", False, DataSourceUpdateMode.OnPropertyChanged)

			InitUnpackerOptions();

			theOutputPathOrOutputFileName = "";
			theUnpackedRelativePathFileNames = new BindingListEx<string>();
			UnpackedFilesComboBox.DataSource = theUnpackedRelativePathFileNames;

			UpdateUnpackMode();
			UpdateWidgets(false);

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;

			PackagePathFileNameTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
			OutputPathTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
		}

		private void InitUnpackerOptions()
		{
			FolderForEachPackageCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "UnpackFolderForEachPackageIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			KeepFullPathCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "UnpackKeepFullPathIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			LogFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "UnpackLogFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void Free()
		{
			PackagePathFileNameTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			OutputPathTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Unpacker.ProgressChanged -= ListerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Unpacker.RunWorkerCompleted -= ListerBackgroundWorker_RunWorkerCompleted;
			theSearchBackgroundWorker.ProgressChanged -= SearchBackgroundWorker_ProgressChanged;
			theSearchBackgroundWorker.RunWorkerCompleted -= SearchBackgroundWorker_RunWorkerCompleted;
			MainCROWBAR.TheApp.Unpacker.ProgressChanged -= UnpackerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Unpacker.RunWorkerCompleted -= UnpackerBackgroundWorker_RunWorkerCompleted;

			UnpackComboBox.DataBindings.Clear();
			PackagePathFileNameTextBox.DataBindings.Clear();

			OutputPathTextBox.DataBindings.Clear();
			OutputSamePathTextBox.DataBindings.Clear();
			OutputSubfolderTextBox.DataBindings.Clear();

			FreeUnpackerOptions();

			UnpackedFilesComboBox.DataSource = null;
		}

		private void FreeUnpackerOptions()
		{
			FolderForEachPackageCheckBox.DataBindings.Clear();
			KeepFullPathCheckBox.DataBindings.Clear();
			LogFileCheckBox.DataBindings.Clear();
		}

#endregion

#region Properties

#endregion

#region Methods

		public void RunUnpackerToGetListOfPackageContents()
		{
			//NOTE: This is needed to handle when Crowbar is opened by double-clicking a vpk file.
			//      Every test on my dev computer without this code raised this exception: "This BackgroundWorker is currently busy and cannot run multiple tasks concurrently."
			if (MainCROWBAR.TheApp.Unpacker.IsBusy)
			{
				MainCROWBAR.TheApp.Unpacker.CancelAsync();
				while (MainCROWBAR.TheApp.Unpacker.IsBusy)
				{
					Application.DoEvents();
				}
			}

			if (MainCROWBAR.TheApp.Settings.UnpackerIsRunning)
			{
				return;
			}

			PackageTreeView.Nodes.Clear();
			PackageTreeView.Nodes.Add("<root>", "<refreshing>");
			theUnpackedRelativePathFileNames.Clear();
			UpdateWidgets(true);
			//Me.PackageTreeView.Nodes(0).Text = "<refreshing>"
			PackageTreeView.Nodes[0].Nodes.Clear();
			PackageTreeView.Nodes[0].Tag = null;
			PackageListView.Items.Clear();
			RefreshListingToolStripButton.Image = Properties.Resources.CancelRefresh;
			RefreshListingToolStripButton.Text = "Cancel";
			SkipCurrentPackageButton.Enabled = false;
			//Me.CancelUnpackButton.Text = "Cancel Listing"
			CancelUnpackButton.Enabled = false;
			UnpackerLogTextBox.Text = "";
			thePackageCount = 0;
			UpdateSelectionCounts();

			MainCROWBAR.TheApp.Unpacker.ProgressChanged += ListerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Unpacker.RunWorkerCompleted += ListerBackgroundWorker_RunWorkerCompleted;

			//TODO: Change to using a separate "Unpacker" object; maybe create a new class specifically for listing.
			//      Want to use a separate object so the gui isn't disabled and enabled while running, 
			//      which causes a flicker and deselects the vpk file name 
			//      if selecting the vpk file name was the cause of the listing action.
			//      [24-Jun-2019: Is this still relevant? It makes sense to use same object because it can not unpack at same time as list.]
			//TODO: What happens if the listing takes a long time and what should the gui look like when it does?
			//      Maybe the DataGridView should be swapped with a textbox that shows something like "Getting a list."
			MainCROWBAR.TheApp.Unpacker.Run(AppEnums.ArchiveAction.List, null, false, "");
		}

#endregion

#region Widget Event Handlers

		private void UnpackUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio often inexplicably extending the right side of these textboxes.
			PackagePathFileNameTextBox.Size = new System.Drawing.Size(BrowseForPackagePathFolderOrFileNameButton.Left - BrowseForPackagePathFolderOrFileNameButton.Margin.Left - PackagePathFileNameTextBox.Margin.Right - PackagePathFileNameTextBox.Left, 21);
			OutputPathTextBox.Size = new System.Drawing.Size(BrowseForOutputPathButton.Left - BrowseForOutputPathButton.Margin.Left - OutputPathTextBox.Margin.Right - OutputPathTextBox.Left, 21);
			OutputSamePathTextBox.Size = new System.Drawing.Size(BrowseForOutputPathButton.Left - BrowseForOutputPathButton.Margin.Left - OutputSamePathTextBox.Margin.Right - OutputSamePathTextBox.Left, 21);
			OutputSubfolderTextBox.Size = new System.Drawing.Size(BrowseForOutputPathButton.Left - BrowseForOutputPathButton.Margin.Left - OutputSubfolderTextBox.Margin.Right - OutputSubfolderTextBox.Left, 21);
		}

#endregion

#region Child Widget Event Handlers

		//Private Sub VpkPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VpkPathFileNameTextBox.Validated
		//	Me.VpkPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.VpkPathFileNameTextBox.Text)
		//End Sub

		private void BrowseForPackagePathFolderOrFileNameButton_Click(System.Object sender, System.EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			openFileWdw.Title = "Open the file or folder you want to unpack";
			if (File.Exists(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
			{
				openFileWdw.InitialDirectory = FileManager.GetPath(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName);
				//ElseIf Directory.Exists(TheApp.Settings.UnpackPackagePathFolderOrFileName) Then
				//	openFileWdw.InitialDirectory = TheApp.Settings.UnpackPackagePathFolderOrFileName
			}
			else
			{
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName);
				if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
				{
					openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
			openFileWdw.FileName = "[Folder Selection]";
			openFileWdw.Filter = "Source Engine Package Files (*.apk;*.fpx;*.gma;*.vpk)|*.apk;*.fpx;*.gma;*.vpk|Fairy Tale Busters APK Files (*.apk)|*.apk|Tactical Intervention FPX Files (*.fpx)|*.fpx|Garry's Mod GMA Files (*.gma)|*.gma|Source Engine VPK Files (*.vpk)|*.vpk";
			//openFileWdw.Filter = "Source Engine Package Files (*.vpk;*.fpx;*.gma;*.hfs)|*.vpk;*.fpx;*.gma;*.hfs|Source Engine VPK Files (*.vpk)|*.vpk|Tactical Intervention FPX Files (*.fpx)|*.fpx|Garry's Mod GMA Files (*.gma)|*.gma|Vindictus HFS Files (*.hfs)|*.hfs"
			openFileWdw.AddExtension = true;
			openFileWdw.CheckFileExists = false;
			openFileWdw.Multiselect = false;
			openFileWdw.ValidateNames = true;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				try
				{
					if (Path.GetFileName(openFileWdw.FileName).StartsWith("[Folder Selection]"))
					{
						MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName = FileManager.GetPath(openFileWdw.FileName);
					}
					else
					{
						MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName = openFileWdw.FileName;
					}
				}
				catch (System.IO.PathTooLongException ex)
				{
					MessageBox.Show("The file or folder you tried to select has too many characters in it. Try shortening it by moving the model files somewhere else or by renaming folders or files." + "\r\n" + "\r\n" + "Error message generated by Windows: " + "\r\n" + ex.Message, "The File or Folder You Tried to Select Is Too Long", MessageBoxButtons.OK);
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void GotoPackageButton_Click(System.Object sender, System.EventArgs e)
		{
			FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName);
		}

		private void OutputPathTextBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] pathFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
			string pathFileName = pathFileNames[0];
			if (Directory.Exists(pathFileName))
			{
				MainCROWBAR.TheApp.Settings.UnpackOutputFullPath = pathFileName;
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
			UpdateOutputPathTextBox();
		}

		private void BrowseForOutputPathButton_Click(object sender, EventArgs e)
		{
			BrowseForOutputPath();
		}

		private void GotoOutputPathButton_Click(object sender, EventArgs e)
		{
			GotoFolder();
		}

		private void UseDefaultOutputSubfolderButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultUnpackOutputSubfolderName();
		}

		//TODO: Change this to detect pressing of Enter key.
		//Private Sub FindToolStripTextBox_Validated(sender As Object, e As EventArgs) Handles FindToolStripTextBox.Validated
		//	Me.FindTextInPackageFiles(FindDirection.Next)
		//End Sub

		private void PackageTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			UpdateSelectionPathText();
			ShowFilesInSelectedFolder();
		}

		private void PackageTreeView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (PackageTreeView.SelectedNode != null)
			{
				RunUnpackerToExtractFilesInternal(AppEnums.ArchiveAction.ExtractToTemp, null);
			}
		}

		private void PackageTreeView_MouseDown(object sender, MouseEventArgs e)
		{
			TreeView treeView = null;
			TreeNode clickedNode = null;

			treeView = (System.Windows.Forms.TreeView)sender;
			clickedNode = treeView.GetNodeAt(e.X, e.Y);
			if (clickedNode == null)
			{
				//clickedNode = Me.PackageTreeView.Nodes(0)
				return;
			}

			//'NOTE: Right-clicking on a node does not select the node. Need to select the node so context menu will work.
			//If e.Button = MouseButtons.Right Then
			//	treeView.SelectedNode = clickedNode
			//End If
			//NOTE: This selects the node before dragging starts; otherwise dragging would use whatever was selected before the mousedown.
			treeView.SelectedNode = clickedNode;

			//Me.UpdateSelectionPathText()
			//Me.ShowFilesInSelectedFolder()
			PackageListView.SelectedItems.Clear();
		}

		//'NOTE: Need this because listview item stays selected when selecting its parent folder.
		//'      That is, PackageTreeView.AfterSelect event is not raised.
		//Private Sub PackageTreeView_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles PackageTreeView.NodeMouseClick
		//	If Me.PackageTreeView.SelectedNode Is e.Node Then
		//		Me.UpdateSelectionPathText()
		//		Me.ShowFilesInSelectedFolder()
		//	End If
		//End Sub

		//NOTE: This is only needed because TreeView BackColor does not automatically change when Windows Theme is switched.
		private void PackageTreeView_SystemColorsChanged(object sender, EventArgs e)
		{
			PackageTreeView.BackColor = SystemColors.Control;
		}

		private void CustomMenu_Opening(System.Object sender, System.ComponentModel.CancelEventArgs e)
		{
			DeleteSearchToolStripMenuItem.Enabled = PackageTreeView.SelectedNode != null && PackageTreeView.SelectedNode.Text.StartsWith("<Found>");
			DeleteAllSearchesToolStripMenuItem.Enabled = theSearchCount > 0;
		}

		private void DeleteSearchToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			DeleteSearch();
		}

		private void CopyAllToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			DeleteAllSearches();
		}

		private void PackageListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column != theSortColumnIndex)
			{
				theSortColumnIndex = e.Column;
				PackageListView.Sorting = SortOrder.Ascending;
			}
			else
			{
				if (PackageListView.Sorting == SortOrder.Ascending)
				{
					PackageListView.Sorting = SortOrder.Descending;
				}
				else
				{
					PackageListView.Sorting = SortOrder.Ascending;
				}
			}

			PackageListView.ListViewItemSorter = new FolderAndFileListViewItemComparer(e.Column, PackageListView.Sorting);
		}

		private void PackageListView_DoubleClick(object sender, EventArgs e)
		{
			OpenSelectedFolderOrFile();
		}

		private void PackageListView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (PackageListView.SelectedItems.Count > 0)
			{
				RunUnpackerToExtractFiles(AppEnums.ArchiveAction.ExtractToTemp, PackageListView.SelectedItems);
			}
		}

		//NOTE: Tried to show the highlight in TreeView when clicking empty space in ListView, but it did not work.
		//Private Sub PackageListView_MouseDown(sender As Object, e As MouseEventArgs) Handles PackageListView.MouseDown
		//	Dim listView As ListView
		//	Dim clickedItem As ListViewItem

		//	listView = CType(sender, Windows.Forms.ListView)
		//	clickedItem = listView.GetItemAt(e.X, e.Y)
		//	If clickedItem Is Nothing Then
		//		Me.PackageTreeView.Select()
		//	End If
		//End Sub

		private void PackageListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.A && e.Control)
			{
				PackageListView.BeginUpdate();
				foreach (ListViewItem i in PackageListView.Items)
				{
					i.Selected = true;
				}
				PackageListView.EndUpdate();
			}
		}

		private void PackageListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateSelectionCounts();
		}

		private void FindToolStripTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				FindSubstringInFileNames();
			}
		}

		private void FindToolStripButton_Click(object sender, EventArgs e)
		{
			FindSubstringInFileNames();
		}

		private void RefreshListingToolStripButton_Click(object sender, EventArgs e)
		{
			if (RefreshListingToolStripButton.Text == "Refresh")
			{
				RunUnpackerToGetListOfPackageContents();
			}
			else
			{
				MainCROWBAR.TheApp.Unpacker.CancelAsync();
			}
		}

		private void UnpackOptionsUseDefaultsButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultUnpackOptions();
		}

		private void UnpackButton_Click(System.Object sender, System.EventArgs e)
		{
			if (PackageListView.SelectedItems.Count > 0)
			{
				RunUnpackerToExtractFiles(AppEnums.ArchiveAction.Unpack, PackageListView.SelectedItems);
			}
			else
			{
				RunUnpackerToExtractFilesInternal(AppEnums.ArchiveAction.Unpack, null);
			}
		}

		private void SkipCurrentPackageButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Unpacker.SkipCurrentPackage();
		}

		private void CancelUnpackButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Unpacker.CancelAsync();
		}

		private void UseAllInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = MainCROWBAR.TheApp.Unpacker.GetOutputPathOrOutputFileName();
		}

		private void UseInPreviewButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.PreviewMdlPathFileName = MainCROWBAR.TheApp.Unpacker.GetOutputPathFileName(theUnpackedRelativePathFileNames[UnpackedFilesComboBox.SelectedIndex]);
			//TheApp.Settings.PreviewGameSetupSelectedIndex = TheApp.Settings.UnpackGameSetupSelectedIndex
		}

		private void UseInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = MainCROWBAR.TheApp.Unpacker.GetOutputPathFileName(theUnpackedRelativePathFileNames[UnpackedFilesComboBox.SelectedIndex]);
		}

		private void GotoUnpackedFileButton_Click(System.Object sender, System.EventArgs e)
		{
			string pathFileName = MainCROWBAR.TheApp.Unpacker.GetOutputPathFileName(theUnpackedRelativePathFileNames[UnpackedFilesComboBox.SelectedIndex]);
			FileManager.OpenWindowsExplorer(pathFileName);
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "UnpackPackagePathFolderOrFileName")
			{
				UpdateUnpackMode();
				UpdateOutputPathWidgets();
				RunUnpackerToGetListOfPackageContents();
			}
			else if (e.PropertyName == "UnpackMode")
			{
				RunUnpackerToGetListOfPackageContents();
			}
			else if (e.PropertyName == "UnpackOutputFolderOption")
			{
				UpdateOutputPathWidgets();
			}
			else if (e.PropertyName == "UnpackGameSetupSelectedIndex")
			{
				UpdateGameModelsOutputPathTextBox();
			}
			else if (e.PropertyName.StartsWith("Unpack") && e.PropertyName.EndsWith("IsChecked"))
			{
				UpdateWidgets(MainCROWBAR.TheApp.Settings.UnpackerIsRunning);
			}
		}

		private void ListerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				//TODO: Having the updating of disabled widgets here is unusual, so why not move this to before calling the backgroundworker?
				//      One advantage to doing before call: Indicates to user that action has started even when opening file takes a while.
				//Me.UpdateWidgets(True)
				//Me.PackageTreeView.Nodes(0).Nodes.Clear()
				//Me.PackageTreeView.Nodes(0).Tag = Nothing
				//Me.PackageListView.Items.Clear()
				//Me.RefreshListingToolStripButton.Text = "Cancel"
				//Me.SkipCurrentPackageButton.Enabled = False
				//'Me.CancelUnpackButton.Text = "Cancel Listing"
				//Me.CancelUnpackButton.Enabled = False
				//Me.UnpackerLogTextBox.Text = ""
				//'Me.theEntryIndex = -1
			}
			else if (e.ProgressPercentage == 1)
			{
				theEntryIndex = -1;
				thePackageCount += 1;
				UpdateContentsGroupBox();
			}
			else if (e.ProgressPercentage == 2)
			{
				theArchivePathFileName = line;
			}
			else if (e.ProgressPercentage == 3)
			{
				theArchivePathFileNameExists = (line == "True");
			}
			else if (e.ProgressPercentage == 4)
			{
				theEntryIndex += 1;

				//Example output:
				//addonimage.jpg crc=0x50ea4a15 metadatasz=0 fnumber=32767 ofs=0x0 sz=10749
				//addonimage.vtf crc=0xc75861f5 metadatasz=0 fnumber=32767 ofs=0x29fd sz=8400
				//addoninfo.txt crc=0xb3d2b571 metadatasz=0 fnumber=32767 ofs=0x4acd sz=1677
				//materials/models/weapons/melee/crowbar.vmt crc=0x4aaf5f0 metadatasz=0 fnumber=32767 ofs=0x515a sz=566
				//materials/models/weapons/melee/crowbar.vtf crc=0xded2e058 metadatasz=0 fnumber=32767 ofs=0x5390 sz=174920
				//materials/models/weapons/melee/crowbar_normal.vtf crc=0x7ac0e054 metadatasz=0 fnumber=32767 ofs=0x2fed8 sz=1398196

				//Try
				string[] fields = line.Split(' ');

				string pathFileName = fields[0];
				//NOTE: The last 5 fields should not have any spaces, but the path+filename field might.
				for (int fieldIndex = 1; fieldIndex <= fields.Length - 6; fieldIndex++)
				{
					pathFileName = pathFileName + " " + fields[fieldIndex];
				}
				UInt64 fileSize = (ulong)long.Parse(fields[fields.Length - 1].Remove(0, 3));

				string[] foldersAndFileName = pathFileName.Split('/');
				TreeNode parentTreeNode = null;
				TreeNode treeNode = null;
				List<PackageResourceFileNameInfo> list = null;
				if (foldersAndFileName.Length == 1)
				{
					treeNode = PackageTreeView.Nodes[0];
				}
				else
				{
					parentTreeNode = PackageTreeView.Nodes[0];
					string resourcePathFileName = "";
					for (int nameIndex = 0; nameIndex <= foldersAndFileName.Length - 2; nameIndex++)
					{
						string name = foldersAndFileName[nameIndex];

						if (nameIndex == 0)
						{
							resourcePathFileName = name;
						}
						else
						{
							resourcePathFileName += Path.DirectorySeparatorChar + name;
						}

						if (parentTreeNode.Nodes.ContainsKey(name))
						{
							treeNode = parentTreeNode.Nodes[parentTreeNode.Nodes.IndexOfKey(name)];
							list = (List<PackageResourceFileNameInfo>)parentTreeNode.Tag;
							foreach (PackageResourceFileNameInfo info in list)
							{
								if (info.IsFolder && info.Name == name)
								{
									info.Count += 1UL;
									info.Size += fileSize;
									if (theArchivePathFileNameExists)
									{
										info.ArchivePathFileNameExists = theArchivePathFileNameExists;
										TreeNode temp = new TreeNode();
										treeNode.ForeColor = temp.ForeColor;
									}
								}
							}
						}
						else
						{
							treeNode = parentTreeNode.Nodes.Add(name);
							treeNode.Name = name;

							PackageResourceFileNameInfo resourceInfo = new PackageResourceFileNameInfo();
							//resourceInfo.PathFileName = name
							resourceInfo.PathFileName = resourcePathFileName;
							resourceInfo.Name = name;
							resourceInfo.Size = fileSize;
							resourceInfo.Count = 1;
							resourceInfo.Type = "Folder";
							resourceInfo.Extension = "<Folder>";
							resourceInfo.IsFolder = true;
							//resourceInfo.ArchivePathFileName = Me.theArchivePathFileName
							//NOTE: Because same folder can be in multiple archives, don't bother showing which archive the folder is in. Crowbar only shows the first one added to the list.
							resourceInfo.ArchivePathFileName = "";
							// Using this field to determine when to dim the folder in the treeview and listview.
							resourceInfo.ArchivePathFileNameExists = theArchivePathFileNameExists;
							if (!resourceInfo.ArchivePathFileNameExists)
							{
								treeNode.ForeColor = SystemColors.GrayText;
							}

							if (parentTreeNode.Tag == null)
							{
								list = new List<PackageResourceFileNameInfo>();
								list.Add(resourceInfo);
								parentTreeNode.Tag = list;
							}
							else
							{
								list = (List<PackageResourceFileNameInfo>)parentTreeNode.Tag;
								list.Add(resourceInfo);
							}
						}
						parentTreeNode = treeNode;
					}
				}
				if (treeNode != null)
				{
					string fileName = null;
					string fileExtension = null;
					string fileExtensionWithDot = "";
					if (pathFileName.StartsWith("<"))
					{
						fileName = pathFileName;
						fileExtension = "";
					}
					else
					{
						fileName = Path.GetFileName(pathFileName);

						fileExtension = Path.GetExtension(pathFileName);
						if (!string.IsNullOrEmpty(fileExtension) && fileExtension[0] == '.')
						{
							fileExtensionWithDot = fileExtension;
							fileExtension = fileExtension.Substring(1);
						}
					}
					//Dim fileSize As UInt64
					//fileSize = CULng(CLng(fields(fields.Length - 1).Remove(0, 3)))
					string fileType = "<type>";

					PackageResourceFileNameInfo resourceInfo = new PackageResourceFileNameInfo();
					resourceInfo.PathFileName = pathFileName;
					resourceInfo.Name = fileName;
					resourceInfo.Size = fileSize;
					resourceInfo.Count = 1;
					if (pathFileName.StartsWith("<"))
					{
						resourceInfo.Type = "<internal data>";
					}
					else
					{
						resourceInfo.Type = Win32Api.GetFileTypeDescription(fileExtensionWithDot);
					}
					resourceInfo.Extension = fileExtension;
					resourceInfo.IsFolder = false;
					resourceInfo.ArchivePathFileName = theArchivePathFileName;
					resourceInfo.ArchivePathFileNameExists = theArchivePathFileNameExists;
					resourceInfo.EntryIndex = theEntryIndex;

					if (treeNode.Tag == null)
					{
						list = new List<PackageResourceFileNameInfo>();
						list.Add(resourceInfo);
						treeNode.Tag = list;
					}
					else
					{
						list = (List<PackageResourceFileNameInfo>)treeNode.Tag;
						list.Add(resourceInfo);
					}

					//Me.SetNodeText(treeNode, list.Count)
				}
				PackageTreeView.Nodes[0].Expand();
				//Catch ex As Exception
				//	'TODO: Try to catch an out-of-memory exception. Probably not going to work, though.
				//	Dim worker As Unpacker = CType(sender, Unpacker)
				//	worker.CancelAsync()
				//	Dim debug As Integer = 4242
				//End Try
			}
			else if (e.ProgressPercentage == 50)
			{
				UnpackerLogTextBox.Text = "";
				UnpackerLogTextBox.AppendText(line + "\r");
				//NOTE: Set the textbox to show first line of text.
				UnpackerLogTextBox.Select(0, 0);
			}
			else if (e.ProgressPercentage == 51)
			{
				UnpackerLogTextBox.AppendText(line + "\r");
				//NOTE: Set the textbox to show first line of text.
				UnpackerLogTextBox.Select(0, 0);
			}
			else if (e.ProgressPercentage == 100)
			{
			}
		}

		private void ListerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			MainCROWBAR.TheApp.Unpacker.ProgressChanged -= ListerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Unpacker.RunWorkerCompleted -= ListerBackgroundWorker_RunWorkerCompleted;

			if (!e.Cancelled)
			{
				UnpackerOutputInfo unpackResultInfo = (UnpackerOutputInfo)e.Result;
				if (PackageTreeView.Nodes[0].Nodes.Count == 0 && PackageTreeView.Nodes[0].Tag == null)
				{
					PackageTreeView.Nodes.Clear();
				}
				else
				{
					PackageTreeView.Nodes[0].Text = "<root>";
				}
			}
			else
			{
				PackageTreeView.Nodes[0].Text = "<root-incomplete>";
			}

			if (PackageTreeView.Nodes.Count > 0)
			{
				PackageTreeView.Nodes[0].Expand();
				PackageTreeView.SelectedNode = PackageTreeView.Nodes[0];
				ShowFilesInSelectedFolder();
			}
			UpdateSelectionPathText();
			RefreshListingToolStripButton.Image = Properties.Resources.Refresh;
			RefreshListingToolStripButton.Text = "Refresh";
			//IMPORTANT: Update the toolstrip so the Refresh button does not disappear. Not sure why it disappears without 
			ToolStrip1.PerformLayout();
			UpdateWidgets(false);
		}

		private void SearchBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 1)
			{
				theResultsRootTreeNode.Text = "<Found> " + theTextToFind + " (" + theResultsCount.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo) + ")";
			}
		}

		private void SearchBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			string resultsText = "<Found> " + theTextToFind + " (" + theResultsCount.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo) + ")";
			if (e.Cancelled)
			{
				resultsText += " <incomplete>";
			}
			theResultsRootTreeNode.Text = resultsText;

			theSearchBackgroundWorker.DoWork -= CreateTreeNodesThatMatchTextToFind;
			theSearchBackgroundWorker.ProgressChanged -= SearchBackgroundWorker_ProgressChanged;
			theSearchBackgroundWorker.RunWorkerCompleted -= SearchBackgroundWorker_RunWorkerCompleted;

			FindToolStripButton.Image = Properties.Resources.Find;
			FindToolStripButton.Text = "Find";
			theSelectedTreeNode.Nodes.Add(theResultsRootTreeNode);
			PackageTreeView.SelectedNode = theResultsRootTreeNode;

			theSearchCount += 1;
		}

		private void UnpackerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			//If e.ProgressPercentage = 75 Then
			//	Me.DoDragAndDrop(CType(e.UserState, BindingListEx(Of String)))
			//	Exit Sub
			//End If

			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				//TODO: Having the updating of disabled widgets here is unusual, so why not move this to before calling the backgroundworker?
				//      One advantage to doing before call: Indicates to user that action has started even when opening file takes a while.
				UnpackerLogTextBox.Text = "";
				UnpackerLogTextBox.AppendText(line + "\r");
				theOutputPathOrOutputFileName = "";
				UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				UnpackerLogTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				UnpackerLogTextBox.AppendText(line + "\r");
			}
		}

		private void UnpackerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (!e.Cancelled && e.Result != null)
			{
				UnpackerOutputInfo unpackResultInfo = (UnpackerOutputInfo)e.Result;

				UpdateUnpackedRelativePathFileNames(unpackResultInfo.theUnpackedRelativePathFileNames);
				theOutputPathOrOutputFileName = MainCROWBAR.TheApp.Unpacker.GetOutputPathOrOutputFileName();
			}

			MainCROWBAR.TheApp.Unpacker.ProgressChanged -= UnpackerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Unpacker.RunWorkerCompleted -= UnpackerBackgroundWorker_RunWorkerCompleted;

			UpdateWidgets(false);
		}

#endregion

#region Private Methods

		private void UpdateOutputPathComboBox()
		{
			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.UnpackOutputPathOptions));

			OutputPathComboBox.DataBindings.Clear();
			try
			{
				//TODO: Delete this line when game addons folder option is implemented.
				anEnumList.RemoveAt((System.Int32)AppEnums.UnpackOutputPathOptions.GameAddonsFolder);

				OutputPathComboBox.DisplayMember = "Value";
				OutputPathComboBox.ValueMember = "Key";
				OutputPathComboBox.DataSource = anEnumList;
				OutputPathComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "UnpackOutputFolderOption", false, DataSourceUpdateMode.OnPropertyChanged);

				// Do not use this line because it will override the value automatically assigned by the data bindings above.
				//Me.OutputPathComboBox.SelectedIndex = 0
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void UpdateOutputPathWidgets()
		{
			GameModelsOutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.GameAddonsFolder);
			OutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.WorkFolder);
			OutputSamePathTextBox.Visible = (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.SameFolder);
			OutputSubfolderTextBox.Visible = (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.Subfolder);
			BrowseForOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.SameFolder) || (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.WorkFolder) || (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.GameAddonsFolder);
			GotoOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.SameFolder) || (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.WorkFolder) || (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.GameAddonsFolder);
			UseDefaultOutputSubfolderButton.Enabled = (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.Subfolder);
			UseDefaultOutputSubfolderButton.Visible = (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.Subfolder);
			UpdateOutputPathWidgets(MainCROWBAR.TheApp.Settings.UnpackerIsRunning);

			if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.SameFolder)
			{
				string parentPath = FileManager.GetPath(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName);
				MainCROWBAR.TheApp.Settings.UnpackOutputSamePath = parentPath;
			}
			else if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.GameAddonsFolder)
			{
				UpdateGameModelsOutputPathTextBox();
			}
		}

		private void UpdateOutputPathWidgets(bool unpackerIsRunning)
		{
			BrowseForOutputPathButton.Enabled = (!unpackerIsRunning) && (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.WorkFolder);
			GotoOutputPathButton.Enabled = (!unpackerIsRunning);
		}

		private void UpdateGameModelsOutputPathTextBox()
		{
			if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.GameAddonsFolder)
			{
				GameSetup gameSetup = null;
				string gamePath = null;
				string gameModelsPath = null;

				gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.UnpackGameSetupSelectedIndex];
				gamePath = FileManager.GetPath(gameSetup.GamePathFileName);
				gameModelsPath = Path.Combine(gamePath, "models");

				GameModelsOutputPathTextBox.Text = gameModelsPath;
			}
		}

		private void UpdateOutputPathTextBox()
		{
			if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.WorkFolder)
			{
				if (string.IsNullOrEmpty(OutputPathTextBox.Text))
				{
					try
					{
						MainCROWBAR.TheApp.Settings.UnpackOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
				}
			}
		}

		private void BrowseForOutputPath()
		{
			if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.WorkFolder)
			{
				//NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
				//      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
				OpenFileDialog outputPathWdw = new OpenFileDialog();

				outputPathWdw.Title = "Open the folder you want as Output Folder";
				outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.UnpackOutputFullPath);
				if (string.IsNullOrEmpty(outputPathWdw.InitialDirectory))
				{
					if (File.Exists(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
					{
						outputPathWdw.InitialDirectory = FileManager.GetPath(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName);
					}
					else if (Directory.Exists(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
					{
						outputPathWdw.InitialDirectory = MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName;
					}
					else
					{
						outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					}
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

					MainCROWBAR.TheApp.Settings.UnpackOutputFullPath = FileManager.GetPath(outputPathWdw.FileName);
				}
			}
		}

		private void GotoFolder()
		{
			if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.SameFolder)
			{
				FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.UnpackOutputSamePath);
			}
			else if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.GameAddonsFolder)
			{
				GameSetup gameSetup = null;
				string gamePath = null;
				string gameModelsPath = null;

				gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.UnpackGameSetupSelectedIndex];
				gamePath = FileManager.GetPath(gameSetup.GamePathFileName);
				gameModelsPath = Path.Combine(gamePath, "models");

				if (FileManager.PathExistsAfterTryToCreate(gameModelsPath))
				{
					FileManager.OpenWindowsExplorer(gameModelsPath);
				}
			}
			else if (MainCROWBAR.TheApp.Settings.UnpackOutputFolderOption == AppEnums.UnpackOutputPathOptions.WorkFolder)
			{
				FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.UnpackOutputFullPath);
			}
		}

		private void UpdateContentsGroupBox()
		{
			if (thePackageCount > 1)
			{
				ContentsGroupBox.Text = "Contents of " + thePackageCount.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo) + " packages";
			}
			else
			{
				ContentsGroupBox.Text = "Contents of package";
			}
		}

		private void UpdateWidgets(bool unpackerIsRunning)
		{
			MainCROWBAR.TheApp.Settings.UnpackerIsRunning = unpackerIsRunning;

			UnpackComboBox.Enabled = !unpackerIsRunning;
			PackagePathFileNameTextBox.Enabled = !unpackerIsRunning;
			BrowseForPackagePathFolderOrFileNameButton.Enabled = !unpackerIsRunning;

			OutputPathComboBox.Enabled = !unpackerIsRunning;
			OutputPathTextBox.Enabled = !unpackerIsRunning;
			OutputSamePathTextBox.Enabled = !unpackerIsRunning;
			OutputSubfolderTextBox.Enabled = !unpackerIsRunning;
			UseDefaultOutputSubfolderButton.Enabled = !unpackerIsRunning;
			UpdateOutputPathWidgets(unpackerIsRunning);

			//Me.SelectionGroupBox.Enabled = Not unpackerIsRunning

			OptionsGroupBox.Enabled = !unpackerIsRunning;

			//Me.UnpackButton.Enabled = (Not unpackerIsRunning) AndAlso (Me.PackageTreeView.Nodes(0).Nodes.Count > 0)
			List<PackageResourceFileNameInfo> folderResourceInfos = null;
			if (PackageTreeView.Nodes.Count > 0)
			{
				folderResourceInfos = (List<PackageResourceFileNameInfo>)(PackageTreeView.Nodes[0].Tag);
			}
			UnpackButton.Enabled = (!unpackerIsRunning) && (folderResourceInfos != null) && (folderResourceInfos.Count > 0);
			SkipCurrentPackageButton.Enabled = unpackerIsRunning;
			CancelUnpackButton.Enabled = unpackerIsRunning;
			UseAllInDecompileButton.Enabled = !unpackerIsRunning && !string.IsNullOrEmpty(theOutputPathOrOutputFileName) && theUnpackedRelativePathFileNames.Count > 0;

			UnpackedFilesComboBox.Enabled = !unpackerIsRunning && theUnpackedRelativePathFileNames.Count > 0;
			UseInPreviewButton.Enabled = !unpackerIsRunning && !string.IsNullOrEmpty(theOutputPathOrOutputFileName) && theUnpackedRelativePathFileNames.Count > 0;
			UseInDecompileButton.Enabled = !unpackerIsRunning && !string.IsNullOrEmpty(theOutputPathOrOutputFileName) && theUnpackedRelativePathFileNames.Count > 0;
			GotoUnpackedFileButton.Enabled = !unpackerIsRunning && theUnpackedRelativePathFileNames.Count > 0;
		}

		private void UpdateUnpackedRelativePathFileNames(BindingListEx<string> iUnpackedRelativePathFileNames)
		{
			if (iUnpackedRelativePathFileNames != null)
			{
				theUnpackedRelativePathFileNames = iUnpackedRelativePathFileNames;
				theUnpackedRelativePathFileNames.Sort();
				//NOTE: Need to set to nothing first to force it to update.
				UnpackedFilesComboBox.DataSource = null;
				UnpackedFilesComboBox.DataSource = theUnpackedRelativePathFileNames;
			}
		}

		private void UpdateUnpackMode()
		{
			IList anEnumList = null;
			AppEnums.InputOptions previousSelectedInputOption = 0;

			anEnumList = EnumHelper.ToList(typeof(AppEnums.InputOptions));
			previousSelectedInputOption = MainCROWBAR.TheApp.Settings.UnpackMode;
			UnpackComboBox.DataBindings.Clear();
			try
			{
				if (File.Exists(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
				{
					// Set file mode when a file is selected.
					previousSelectedInputOption = AppEnums.InputOptions.File;
				}
				else if (Directory.Exists(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName))
				{
					//NOTE: Remove in reverse index order.
					List<string> packageExtensions = BasePackageFile.GetListOfPackageExtensions();
					foreach (string packageExtension in packageExtensions)
					{
						foreach (string anArchivePathFileName in Directory.GetFiles(MainCROWBAR.TheApp.Settings.UnpackPackagePathFolderOrFileName, packageExtension))
						{
							if (anArchivePathFileName.Length == 0)
							{
								anEnumList.RemoveAt((System.Int32)AppEnums.InputOptions.Folder);
								break;
							}
						}
						if (!anEnumList.Contains(AppEnums.InputOptions.Folder))
						{
							break;
						}
					}
					anEnumList.RemoveAt((System.Int32)AppEnums.InputOptions.File);
					//Else
					//	Exit Try
				}

				UnpackComboBox.DisplayMember = "Value";
				UnpackComboBox.ValueMember = "Key";
				UnpackComboBox.DataSource = anEnumList;
				UnpackComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "UnpackMode", false, DataSourceUpdateMode.OnPropertyChanged);

				if (EnumHelper.Contains(previousSelectedInputOption, anEnumList))
				{
					MainCROWBAR.TheApp.Settings.UnpackMode = previousSelectedInputOption;
				}
				else
				{
					MainCROWBAR.TheApp.Settings.UnpackMode = (AppEnums.InputOptions)EnumHelper.Key(0, anEnumList);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void UpdateSelectionPathText()
		{
			string selectionPathText = "";
			TreeNode aTreeNode = PackageTreeView.SelectedNode;
			while (aTreeNode != null)
			{
				if (!aTreeNode.Text.StartsWith("<Found>"))
				{
					selectionPathText = aTreeNode.Name + "/" + selectionPathText;
				}
				aTreeNode = aTreeNode.Parent;
			}
			SelectionPathTextBox.Text = selectionPathText;
		}

		//Private Sub SetNodeText(ByVal treeNode As TreeNode, ByVal fileCount As Integer)
		//	Dim folderCountText As String
		//	If treeNode.Nodes.Count = 1 Then
		//		folderCountText = "1 folder "
		//	Else
		//		folderCountText = treeNode.Nodes.Count.ToString("N0", TheApp.InternalCultureInfo) + " folders "
		//	End If
		//	Dim fileCountText As String
		//	If fileCount = 1 Then
		//		fileCountText = "1 file"
		//	Else
		//		fileCountText = fileCount.ToString("N0", TheApp.InternalCultureInfo) + " files"
		//	End If
		//	treeNode.Text = treeNode.Name + " <" + folderCountText + fileCountText + ">"
		//End Sub

		private void ShowFilesInSelectedFolder()
		{
			PackageListView.Items.Clear();

			TreeNode selectedTreeNode = PackageTreeView.SelectedNode;
			if (selectedTreeNode != null && selectedTreeNode.Tag != null)
			{
				List<PackageResourceFileNameInfo> list = null;
				list = (List<PackageResourceFileNameInfo>)selectedTreeNode.Tag;

				ListViewItem item = null;
				Bitmap anIcon = null;
				foreach (PackageResourceFileNameInfo info in list)
				{
					item = new ListViewItem(info.Name);
					item.Tag = info;
					if (info.IsFolder)
					{
						//Dim treeNodeForFolder As TreeNode
						//Dim listForFolder As List(Of PackageResourceFileNameInfo)
						//Dim itemCountText As String
						//treeNodeForFolder = selectedTreeNode.Nodes.Find(info.Name, False)(0)
						//listForFolder = CType(treeNodeForFolder.Tag, List(Of PackageResourceFileNameInfo))
						//itemCountText = listForFolder.Count.ToString("N0", TheApp.InternalCultureInfo)
						//'If listForFolder.Count = 1 Then
						//'	itemCountText += " item"
						//'Else
						//'	itemCountText += " items"
						//'End If
						//item.SubItems.Add(itemCountText)
						item.SubItems.Add(info.Size.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo));
						item.SubItems.Add(info.Count.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo));
					}
					else
					{
						item.SubItems.Add(info.Size.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo));
						item.SubItems.Add(info.Count.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo));
					}
					item.SubItems.Add(info.Type);
					item.SubItems.Add(info.Extension);
					item.SubItems.Add(info.ArchivePathFileName);

					if (!ImageList1.Images.ContainsKey(info.Extension))
					{
						if (info.IsFolder)
						{
							anIcon = Win32Api.GetShellIcon(info.Name, Win32Api.FILE_ATTRIBUTE_DIRECTORY);
						}
						else
						{
							anIcon = Win32Api.GetShellIcon(info.Name);
						}
						ImageList1.Images.Add(info.Extension, anIcon);
					}
					item.ImageKey = info.Extension;

					if (!info.ArchivePathFileNameExists)
					{
						item.ForeColor = SystemColors.GrayText;
						//item.BackColor = SystemColors
					}

					PackageListView.Items.Add(item);
				}

				PackageListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			}

			UpdateSelectionCounts();
		}

		//NOTE: Searches the folder (and its subfolders) selected in treeview.
		private void FindSubstringInFileNames()
		{
			theTextToFind = FindToolStripTextBox.Text;
			if (!string.IsNullOrWhiteSpace(theTextToFind))
			{
				theSelectedTreeNode = PackageTreeView.SelectedNode;
				if (theSelectedTreeNode == null)
				{
					theSelectedTreeNode = PackageTreeView.Nodes[0];
				}

				FindToolStripButton.Image = Properties.Resources.CancelSearch;
				FindToolStripButton.Text = "Cancel";

				theResultsFileCount = 0;
				theResultsFolderCount = 0;
				theResultsCount = 0;
				string resultsRootTreeNodeText = "<Found> " + theTextToFind + " (" + theResultsCount.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo) + ") <searching>";
				theResultsRootTreeNode = new TreeNode(resultsRootTreeNodeText);

				if (MainCROWBAR.TheApp.Settings.UnpackSearchField == AppEnums.UnpackSearchFieldOptions.FilesAndFolders)
				{
					string resultsFoldersTreeNodeText = "<Folders found> (0)";
					theResultsFoldersTreeNode = new TreeNode(resultsFoldersTreeNodeText);
					theResultsRootTreeNode.Nodes.Add(theResultsFoldersTreeNode);
				}

				theSearchBackgroundWorker = new BackgroundWorker();
				theSearchBackgroundWorker.WorkerReportsProgress = true;
				theSearchBackgroundWorker.WorkerSupportsCancellation = true;
				theSearchBackgroundWorker.DoWork += CreateTreeNodesThatMatchTextToFind;
				theSearchBackgroundWorker.ProgressChanged += SearchBackgroundWorker_ProgressChanged;
				theSearchBackgroundWorker.RunWorkerCompleted += SearchBackgroundWorker_RunWorkerCompleted;
				theSearchBackgroundWorker.RunWorkerAsync(theResultsCount);
			}
		}

		//NOTE: This is run in a background thread.
		private void CreateTreeNodesThatMatchTextToFind(object sender, DoWorkEventArgs e)
		{
			CreateTreeNodesThatMatchTextToFind(e, theSelectedTreeNode, theResultsRootTreeNode);
		}

		//NOTE: This is run in a background thread.
		private void CreateTreeNodesThatMatchTextToFind(DoWorkEventArgs e, TreeNode treeNodeToSearch, TreeNode currentResultsTreeNode)
		{
			List<PackageResourceFileNameInfo> list = null;
			list = (List<PackageResourceFileNameInfo>)treeNodeToSearch.Tag;

			if (theSearchBackgroundWorker.CancellationPending)
			{
				e.Cancel = true;
				return;
			}

			if (list != null)
			{
				string infoName = null;
				List<PackageResourceFileNameInfo> currentResultsTreeNodeList = (List<PackageResourceFileNameInfo>)currentResultsTreeNode.Tag;
				List<PackageResourceFileNameInfo> currentResultsFolderTreeNodeList = (List<PackageResourceFileNameInfo>)theResultsFoldersTreeNode.Tag;

				TreeNode nodeClone = null;
				foreach (PackageResourceFileNameInfo info in list)
				{
					if (!info.IsFolder)
					{
						infoName = info.Name.ToLower();
						if (infoName.Contains(theTextToFind.ToLower()))
						{
							if (currentResultsTreeNodeList == null)
							{
								currentResultsTreeNodeList = new List<PackageResourceFileNameInfo>();
								currentResultsTreeNode.Tag = currentResultsTreeNodeList;
							}
							currentResultsTreeNodeList.Add(info);

							theResultsFileCount += 1;
							theSearchBackgroundWorker.ReportProgress(1);
						}
					}
					else if (MainCROWBAR.TheApp.Settings.UnpackSearchField == AppEnums.UnpackSearchFieldOptions.FilesAndFolders)
					{
						infoName = info.Name.ToLower();
						if (infoName.Contains(theTextToFind.ToLower()))
						{
							if (currentResultsFolderTreeNodeList == null)
							{
								currentResultsFolderTreeNodeList = new List<PackageResourceFileNameInfo>();
								theResultsFoldersTreeNode.Tag = currentResultsFolderTreeNodeList;
							}
							PackageResourceFileNameInfo infoClone = (PackageResourceFileNameInfo)info.Clone();
							infoClone.Name = infoClone.PathFileName;
							currentResultsFolderTreeNodeList.Add(infoClone);

							theResultsFolderCount += 1;

							//If Not Me.theResultsFoldersTreeNode.Nodes.ContainsKey(info.Name) Then
							theResultsFoldersTreeNode.Text = "<Folders found> (" + theResultsFolderCount.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo) + ")";

							//TODO: Add a special Tag to above node that allows double-clicking on it to go to real folder.
							//End If

							//Me.theResultsCount += 1
							theSearchBackgroundWorker.ReportProgress(1);
						}
					}

					if (theSearchBackgroundWorker.CancellationPending)
					{
						e.Cancel = true;
						return;
					}
				}

				int count = 0;
				foreach (TreeNode node in treeNodeToSearch.Nodes)
				{
					if (!node.Text.StartsWith("<Found>"))
					{
						if (!currentResultsTreeNode.Nodes.ContainsKey(node.Name))
						{
							//NOTE: Do not use node.Clone() because it includes the cloning of child nodes.
							//nodeClone = CType(node.Clone(), TreeNode)
							nodeClone = new TreeNode(node.Text);
							nodeClone.Name = node.Name;
							currentResultsTreeNode.Nodes.Add(nodeClone);
							count = theResultsFileCount;

							CreateTreeNodesThatMatchTextToFind(e, node, nodeClone);

							if (theSearchBackgroundWorker.CancellationPending)
							{
								e.Cancel = true;
								return;
							}

							theResultsCount = theResultsFileCount + theResultsFolderCount;
							if (count == theResultsFileCount)
							{
								currentResultsTreeNode.Nodes.Remove(nodeClone);
							}
							else
							{
								foreach (PackageResourceFileNameInfo info in list)
								{
									if (info.IsFolder)
									{
										infoName = info.Name.ToLower();

										if (infoName == nodeClone.Name.ToLower())
										{
											if (currentResultsTreeNodeList == null)
											{
												currentResultsTreeNodeList = new List<PackageResourceFileNameInfo>();
												currentResultsTreeNode.Tag = currentResultsTreeNodeList;
											}
											currentResultsTreeNodeList.Add(info);
										}
									}

									if (theSearchBackgroundWorker.CancellationPending)
									{
										e.Cancel = true;
										return;
									}
								}
							}
						}
					}

					if (theSearchBackgroundWorker.CancellationPending)
					{
						e.Cancel = true;
						return;
					}
				}
			}
		}

		private void UpdateSelectionCounts()
		{
			UInt64 selectedFileCount = 0;
			UInt64 totalFileCount = 0;
			UInt64 selectedByteCount = 0;

			TreeNode selectedTreeNode = PackageTreeView.SelectedNode;
			if (selectedTreeNode != null && selectedTreeNode.Tag != null)
			{
				List<PackageResourceFileNameInfo> list = null;
				list = (List<PackageResourceFileNameInfo>)selectedTreeNode.Tag;

				//fileCount = list.Count
				foreach (ListViewItem item in PackageListView.Items)
				{
					totalFileCount += ((PackageResourceFileNameInfo)item.Tag).Count;
				}

				foreach (ListViewItem item in PackageListView.SelectedItems)
				{
					selectedFileCount += ((PackageResourceFileNameInfo)item.Tag).Count;
					selectedByteCount += ((PackageResourceFileNameInfo)item.Tag).Size;
				}
			}
			//Me.UpdateSelectionCountsRecursive(selectedTreeNode, fileCount, sizeTotal)

			//Me.FilesSelectedCountToolStripLabel.Text = Me.PackageListView.SelectedItems.Count.ToString("N0", TheApp.InternalCultureInfo) + "/" + fileCount.ToString("N0", TheApp.InternalCultureInfo)
			FilesSelectedCountToolStripLabel.Text = selectedFileCount.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo) + "/" + totalFileCount.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo);
			SizeSelectedTotalToolStripLabel.Text = selectedByteCount.ToString("N0", MainCROWBAR.TheApp.InternalCultureInfo);

			//IMPORTANT: Update the toolstrip so the items are resized properly. Needed because of the 'springing' textbox.
			ToolStrip1.PerformLayout();
		}

		//Private Sub UpdateSelectionCountsRecursive(ByVal currentTreeNode As TreeNode, ByRef fileCount As Integer, ByRef sizeTotal As Long)
		//	If currentTreeNode IsNot Nothing AndAlso currentTreeNode.Tag IsNot Nothing Then
		//		Dim list As List(Of PackageResourceFileNameInfo)
		//		list = CType(currentTreeNode.Tag, List(Of PackageResourceFileNameInfo))

		//		fileCount += list.Count

		//		For Each item As ListViewItem In Me.PackageListView.SelectedItems
		//			sizeTotal += CType(item.Tag, PackageResourceFileNameInfo).Size
		//		Next

		//		For Each childNode As TreeNode In currentTreeNode.Nodes
		//			Me.UpdateSelectionCountsRecursive(childNode, fileCount, sizeTotal)
		//		Next
		//	End If
		//End Sub

		private SortedList<string, List<int>> GetEntriesFromFolderEntry(List<PackageResourceFileNameInfo> resourceInfos, TreeNode treeNode, SortedList<string, List<int>> archivePathFileNameToEntryIndexMap)
		{
			if (resourceInfos == null)
				return archivePathFileNameToEntryIndexMap;

			foreach (PackageResourceFileNameInfo resourceInfo in resourceInfos)
			{
				if (resourceInfo.IsFolder)
				{
					TreeNode folderNode = GetNodeFromPath(PackageTreeView.Nodes[0], treeNode.FullPath + "\\" + resourceInfo.Name);
					List<PackageResourceFileNameInfo> folderResourceInfos = (List<PackageResourceFileNameInfo>)folderNode.Tag;
					archivePathFileNameToEntryIndexMap = GetEntriesFromFolderEntry(folderResourceInfos, folderNode, archivePathFileNameToEntryIndexMap);
				}
				else
				{
					List<int> archiveEntryIndexes;
					string archivePathFileName = resourceInfo.ArchivePathFileName;
					int archiveEntryIndex = resourceInfo.EntryIndex;
					if (archivePathFileNameToEntryIndexMap.Keys.Contains(archivePathFileName))
					{
						archiveEntryIndexes = archivePathFileNameToEntryIndexMap[archivePathFileName];
						archiveEntryIndexes.Add(archiveEntryIndex);
					}
					else
					{
						archiveEntryIndexes = new List<int>();
						archiveEntryIndexes.Add(archiveEntryIndex);
						archivePathFileNameToEntryIndexMap.Add(archivePathFileName, archiveEntryIndexes);
					}
				}
			}

			return archivePathFileNameToEntryIndexMap;
		}

		private TreeNode GetNodeFromPath(TreeNode node, string path)
		{
			TreeNode foundNode = null;
			if (node.FullPath == path)
			{
				return node;
			}
			foreach (TreeNode tn in node.Nodes)
			{
				if (tn.FullPath == path)
				{
					return tn;
				}
				else if (tn.Nodes.Count > 0)
				{
					foundNode = GetNodeFromPath(tn, path);
				}
				if (foundNode != null)
				{
					return foundNode;
				}
			}
			return null;
		}

		private void OpenSelectedFolderOrFile()
		{
			ListViewItem selectedItem = PackageListView.SelectedItems[0];

			PackageResourceFileNameInfo resourceInfo = (PackageResourceFileNameInfo)selectedItem.Tag;

			if (resourceInfo.IsFolder)
			{
				TreeNode selectedTreeNode = PackageTreeView.SelectedNode;
				if (selectedTreeNode == null)
				{
					selectedTreeNode = PackageTreeView.Nodes[0];
				}
				PackageTreeView.SelectedNode = selectedTreeNode.Nodes[resourceInfo.Name];
			}
			else
			{
				// Extract the file to the user's temp folder and open it as if it were opened in File Explorer.
				SortedList<string, List<int>> archivePathFileNameToEntryIndexMap = new SortedList<string, List<int>>();
				List<int> archiveEntryIndexes = new List<int>();
				archiveEntryIndexes.Add(resourceInfo.EntryIndex);
				archivePathFileNameToEntryIndexMap.Add(resourceInfo.ArchivePathFileName, archiveEntryIndexes);
				MainCROWBAR.TheApp.Unpacker.Run(AppEnums.ArchiveAction.ExtractAndOpen, archivePathFileNameToEntryIndexMap, false, "");
			}
		}

		private void RunUnpackerToExtractFiles(AppEnums.ArchiveAction unpackerAction, ListView.SelectedListViewItemCollection selectedItems)
		{
			PackageResourceFileNameInfo selectedResourceInfo = null;
			List<PackageResourceFileNameInfo> selectedResourceInfos = new List<PackageResourceFileNameInfo>();
			foreach (ListViewItem selectedItem in selectedItems)
			{
				selectedResourceInfo = (PackageResourceFileNameInfo)selectedItem.Tag;
				selectedResourceInfos.Add(selectedResourceInfo);
			}

			RunUnpackerToExtractFilesInternal(unpackerAction, selectedResourceInfos);
		}

		private void RunUnpackerToExtractFilesInternal(AppEnums.ArchiveAction unpackerAction, List<PackageResourceFileNameInfo> selectedResourceInfos)
		{
			SortedList<string, List<int>> archivePathFileNameToEntryIndexMap = new SortedList<string, List<int>>();
			TreeNode selectedNode = null;
			bool outputPathIsExtendedWithPackageName = false;
			string selectedRelativeOutputPath = null;

			selectedNode = PackageTreeView.SelectedNode;
			if (selectedNode == null)
			{
				selectedNode = PackageTreeView.Nodes[0];
			}

			if (selectedResourceInfos == null)
			{
				selectedResourceInfos = (List<PackageResourceFileNameInfo>)selectedNode.Tag;

				if (selectedResourceInfos == null)
				{
					// This is reached when trying to Unpack a search folder with 0 results.
					return;
				}

				if (!MainCROWBAR.TheApp.Settings.UnpackFolderForEachPackageIsChecked && selectedNode.FullPath == "<root>")
				{
					outputPathIsExtendedWithPackageName = true;
				}

				selectedRelativeOutputPath = selectedNode.FullPath.Replace("<root>\\", "");
				selectedRelativeOutputPath = FileManager.GetPath(selectedRelativeOutputPath);
			}
			else
			{
				selectedRelativeOutputPath = FileManager.GetPath(selectedResourceInfos[0].PathFileName);
			}

			archivePathFileNameToEntryIndexMap = GetEntriesFromFolderEntry(selectedResourceInfos, selectedNode, archivePathFileNameToEntryIndexMap);

			MainCROWBAR.TheApp.Unpacker.ProgressChanged += UnpackerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Unpacker.RunWorkerCompleted += UnpackerBackgroundWorker_RunWorkerCompleted;

			if (unpackerAction == AppEnums.ArchiveAction.ExtractToTemp)
			{
				string message = MainCROWBAR.TheApp.Unpacker.RunSynchronous(unpackerAction, archivePathFileNameToEntryIndexMap, outputPathIsExtendedWithPackageName, selectedRelativeOutputPath);
				if (!string.IsNullOrEmpty(message))
				{
					UnpackerLogTextBox.AppendText(message + "\r");
				}

				List<string> tempRelativePathsAndFileNames = MainCROWBAR.TheApp.Unpacker.GetTempRelativePathsAndFileNames();

				DoDragAndDrop(tempRelativePathsAndFileNames);
			}
			else
			{
				MainCROWBAR.TheApp.Unpacker.Run(unpackerAction, archivePathFileNameToEntryIndexMap, outputPathIsExtendedWithPackageName, selectedRelativeOutputPath);
			}
		}

		private void DoDragAndDrop(List<string> iUnpackedRelativePathsAndFileNames)
		{
			if (iUnpackedRelativePathsAndFileNames.Count > 0)
			{
				StringCollection pathAndFileNameCollection = new StringCollection();
				foreach (string pathOrFileName in iUnpackedRelativePathsAndFileNames)
				{
					if (!pathAndFileNameCollection.Contains(pathOrFileName))
					{
						pathAndFileNameCollection.Add(pathOrFileName);
					}
				}

				DataObject dragDropDataObject = new DataObject();

				dragDropDataObject.SetFileDropList(pathAndFileNameCollection);

				DragDropEffects result = PackageListView.DoDragDrop(dragDropDataObject, DragDropEffects.Move);
				MainCROWBAR.TheApp.Unpacker.DeleteTempUnpackFolder();

				MainCROWBAR.TheApp.Unpacker.ProgressChanged -= UnpackerBackgroundWorker_ProgressChanged;
				MainCROWBAR.TheApp.Unpacker.RunWorkerCompleted -= UnpackerBackgroundWorker_RunWorkerCompleted;

				UpdateWidgets(false);
			}
		}

		private void DeleteSearch()
		{
			PackageTreeView.SelectedNode.Parent.Nodes.Remove(PackageTreeView.SelectedNode);
			theSearchCount -= 1;
		}

		private void DeleteAllSearches()
		{
			RecursivelyDeleteSearchNodes(PackageTreeView.Nodes);
			theSearchCount = 0;
		}

		private void RecursivelyDeleteSearchNodes(TreeNodeCollection nodes)
		{
			TreeNode aNode = null;
			for (int i = nodes.Count - 1; i >= 0; i--)
			{
				aNode = nodes[i];
				if (aNode.Text.StartsWith("<Found>"))
				{
					nodes.Remove(aNode);
				}
				else
				{
					RecursivelyDeleteSearchNodes(aNode.Nodes);
				}
			}
		}

#endregion

#region Data

		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(CustomMenu))]
		private ContextMenuStrip _CustomMenu;
		private ContextMenuStrip CustomMenu
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _CustomMenu;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_CustomMenu != null)
				{
					_CustomMenu.Opening -= CustomMenu_Opening;
				}

				_CustomMenu = value;

				if (value != null)
				{
					_CustomMenu.Opening += CustomMenu_Opening;
				}
			}
		}

		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(DeleteSearchToolStripMenuItem))]
		private ToolStripMenuItem _DeleteSearchToolStripMenuItem;
		private ToolStripMenuItem DeleteSearchToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _DeleteSearchToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_DeleteSearchToolStripMenuItem != null)
				{
					_DeleteSearchToolStripMenuItem.Click -= DeleteSearchToolStripMenuItem_Click;
				}

				_DeleteSearchToolStripMenuItem = value;

				if (value != null)
				{
					_DeleteSearchToolStripMenuItem.Click += DeleteSearchToolStripMenuItem_Click;
				}
			}
		}
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(DeleteAllSearchesToolStripMenuItem))]
		private ToolStripMenuItem _DeleteAllSearchesToolStripMenuItem;
		private ToolStripMenuItem DeleteAllSearchesToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _DeleteAllSearchesToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_DeleteAllSearchesToolStripMenuItem != null)
				{
					_DeleteAllSearchesToolStripMenuItem.Click -= CopyAllToolStripMenuItem_Click;
				}

				_DeleteAllSearchesToolStripMenuItem = value;

				if (value != null)
				{
					_DeleteAllSearchesToolStripMenuItem.Click += CopyAllToolStripMenuItem_Click;
				}
			}
		}

		private BindingListEx<PackagePathFileNameInfo> thePackageFileNames;

		private BindingListEx<string> theUnpackedRelativePathFileNames;
		private string theOutputPathOrOutputFileName;

		private int theSortColumnIndex;

		private List<int> thePackEntries;
		private string theGivenHardLinkFileName;

		private int thePackageCount;
		private string theArchivePathFileName;
		private bool theArchivePathFileNameExists;
		private int theEntryIndex;

		private BackgroundWorker theSearchBackgroundWorker;
		private TreeNode theSelectedTreeNode;
		private TreeNode theResultsRootTreeNode;
		private TreeNode theResultsFoldersTreeNode;
		private string theTextToFind;
		private int theResultsFileCount;
		private int theResultsFolderCount;
		private int theResultsCount;
		private int theSearchCount;

#endregion

	}

}