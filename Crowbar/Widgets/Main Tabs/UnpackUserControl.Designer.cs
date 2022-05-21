using System.IO;
using System.Collections.Specialized;
using System.ComponentModel;

//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public partial class UnpackUserControl : BaseUserControl
	{
		//UserControl overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			ImageList1 = new System.Windows.Forms.ImageList(components);
			ToolTip1 = new System.Windows.Forms.ToolTip(components);
			LogFileCheckBox = new System.Windows.Forms.CheckBox();
			Panel2 = new System.Windows.Forms.Panel();
			OutputSamePathTextBox = new Crowbar.TextBoxEx();
			GameModelsOutputPathTextBox = new Crowbar.TextBoxEx();
			UnpackComboBox = new System.Windows.Forms.ComboBox();
			GotoOutputPathButton = new System.Windows.Forms.Button();
			BrowseForOutputPathButton = new System.Windows.Forms.Button();
			OutputPathTextBox = new Crowbar.TextBoxEx();
			OutputSubfolderTextBox = new Crowbar.TextBoxEx();
			OutputPathComboBox = new System.Windows.Forms.ComboBox();
			Label2 = new System.Windows.Forms.Label();
			UseDefaultOutputSubfolderButton = new System.Windows.Forms.Button();
			PackagesLabel = new System.Windows.Forms.Label();
			PackagePathFileNameTextBox = new Crowbar.TextBoxEx();
			BrowseForPackagePathFolderOrFileNameButton = new System.Windows.Forms.Button();
			GotoPackageButton = new System.Windows.Forms.Button();
			Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			SplitContainer2 = new System.Windows.Forms.SplitContainer();
			ContentsGroupBox = new System.Windows.Forms.GroupBox();
			ContentsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			SplitContainer3 = new System.Windows.Forms.SplitContainer();
			PackageTreeView = new Crowbar.TreeViewEx();
			PackageListView = new System.Windows.Forms.ListView();
			Panel3 = new System.Windows.Forms.Panel();
			SelectionPathTextBox = new System.Windows.Forms.TextBox();
			ToolStrip1 = new System.Windows.Forms.ToolStrip();
			SearchToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			FindToolStripTextBox = new Crowbar.ToolStripSpringTextBox();
			FindToolStripButton = new System.Windows.Forms.ToolStripButton();
			ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			FilesSelectedCountToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			SizeSelectedTotalToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			RefreshListingToolStripButton = new System.Windows.Forms.ToolStripButton();
			ContentsMinScrollerPanel = new System.Windows.Forms.Panel();
			OptionsGroupBox = new System.Windows.Forms.GroupBox();
			OptionsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			KeepFullPathCheckBox = new System.Windows.Forms.CheckBox();
			FolderForEachPackageCheckBox = new System.Windows.Forms.CheckBox();
			Label3 = new System.Windows.Forms.Label();
			EditGameSetupButton = new System.Windows.Forms.Button();
			GameSetupComboBox = new System.Windows.Forms.ComboBox();
			SelectAllModelsAndMaterialsFoldersCheckBox = new System.Windows.Forms.CheckBox();
			UnpackOptionsUseDefaultsButton = new System.Windows.Forms.Button();
			UnpackerLogTextBox = new Crowbar.RichTextBoxEx();
			UnpackButtonsPanel = new System.Windows.Forms.Panel();
			UnpackButton = new System.Windows.Forms.Button();
			SkipCurrentPackageButton = new System.Windows.Forms.Button();
			CancelUnpackButton = new System.Windows.Forms.Button();
			UseAllInDecompileButton = new System.Windows.Forms.Button();
			PostUnpackPanel = new System.Windows.Forms.Panel();
			UnpackedFilesComboBox = new System.Windows.Forms.ComboBox();
			UseInPreviewButton = new System.Windows.Forms.Button();
			UseInDecompileButton = new System.Windows.Forms.Button();
			GotoUnpackedFileButton = new System.Windows.Forms.Button();
			Panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).BeginInit();
			Options_LogSplitContainer.Panel1.SuspendLayout();
			Options_LogSplitContainer.Panel2.SuspendLayout();
			Options_LogSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)SplitContainer2).BeginInit();
			SplitContainer2.Panel1.SuspendLayout();
			SplitContainer2.Panel2.SuspendLayout();
			SplitContainer2.SuspendLayout();
			ContentsGroupBox.SuspendLayout();
			ContentsGroupBoxFillPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)SplitContainer3).BeginInit();
			SplitContainer3.Panel1.SuspendLayout();
			SplitContainer3.Panel2.SuspendLayout();
			SplitContainer3.SuspendLayout();
			Panel3.SuspendLayout();
			ToolStrip1.SuspendLayout();
			OptionsGroupBox.SuspendLayout();
			OptionsGroupBoxFillPanel.SuspendLayout();
			UnpackButtonsPanel.SuspendLayout();
			PostUnpackPanel.SuspendLayout();
			SuspendLayout();
			//
			//ImageList1
			//
			ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			ImageList1.ImageSize = new System.Drawing.Size(16, 16);
			ImageList1.TransparentColor = System.Drawing.Color.Transparent;
			//
			//LogFileCheckBox
			//
			LogFileCheckBox.AutoSize = true;
			LogFileCheckBox.Location = new System.Drawing.Point(3, 72);
			LogFileCheckBox.Name = "LogFileCheckBox";
			LogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			LogFileCheckBox.TabIndex = 5;
			LogFileCheckBox.Text = "Write log to a file";
			ToolTip1.SetToolTip(LogFileCheckBox, "Write unpack log to a file.");
			LogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//Panel2
			//
			Panel2.Controls.Add(OutputSamePathTextBox);
			Panel2.Controls.Add(GameModelsOutputPathTextBox);
			Panel2.Controls.Add(UnpackComboBox);
			Panel2.Controls.Add(GotoOutputPathButton);
			Panel2.Controls.Add(BrowseForOutputPathButton);
			Panel2.Controls.Add(OutputPathTextBox);
			Panel2.Controls.Add(OutputSubfolderTextBox);
			Panel2.Controls.Add(OutputPathComboBox);
			Panel2.Controls.Add(Label2);
			Panel2.Controls.Add(UseDefaultOutputSubfolderButton);
			Panel2.Controls.Add(PackagesLabel);
			Panel2.Controls.Add(PackagePathFileNameTextBox);
			Panel2.Controls.Add(BrowseForPackagePathFolderOrFileNameButton);
			Panel2.Controls.Add(GotoPackageButton);
			Panel2.Controls.Add(Options_LogSplitContainer);
			Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel2.Location = new System.Drawing.Point(0, 0);
			Panel2.Name = "Panel2";
			Panel2.Size = new System.Drawing.Size(776, 536);
			Panel2.TabIndex = 0;
			//
			//OutputSamePathTextBox
			//
			OutputSamePathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputSamePathTextBox.CueBannerText = "";
			OutputSamePathTextBox.Location = new System.Drawing.Point(209, 32);
			OutputSamePathTextBox.Name = "OutputSamePathTextBox";
			OutputSamePathTextBox.ReadOnly = true;
			OutputSamePathTextBox.Size = new System.Drawing.Size(445, 22);
			OutputSamePathTextBox.TabIndex = 26;
			//
			//GameModelsOutputPathTextBox
			//
			GameModelsOutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameModelsOutputPathTextBox.CueBannerText = "";
			GameModelsOutputPathTextBox.Location = new System.Drawing.Point(209, 32);
			GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox";
			GameModelsOutputPathTextBox.ReadOnly = true;
			GameModelsOutputPathTextBox.Size = new System.Drawing.Size(445, 22);
			GameModelsOutputPathTextBox.TabIndex = 15;
			//
			//UnpackComboBox
			//
			UnpackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			UnpackComboBox.FormattingEnabled = true;
			UnpackComboBox.Location = new System.Drawing.Point(71, 4);
			UnpackComboBox.Name = "UnpackComboBox";
			UnpackComboBox.Size = new System.Drawing.Size(132, 21);
			UnpackComboBox.TabIndex = 1;
			//
			//GotoOutputPathButton
			//
			GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			GotoOutputPathButton.Name = "GotoOutputPathButton";
			GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			GotoOutputPathButton.TabIndex = 18;
			GotoOutputPathButton.Text = "Goto";
			GotoOutputPathButton.UseVisualStyleBackColor = true;
			//
			//BrowseForOutputPathButton
			//
			BrowseForOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForOutputPathButton.Enabled = false;
			BrowseForOutputPathButton.Location = new System.Drawing.Point(660, 32);
			BrowseForOutputPathButton.Name = "BrowseForOutputPathButton";
			BrowseForOutputPathButton.Size = new System.Drawing.Size(64, 23);
			BrowseForOutputPathButton.TabIndex = 17;
			BrowseForOutputPathButton.Text = "Browse...";
			BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OutputPathTextBox
			//
			OutputPathTextBox.AllowDrop = true;
			OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputPathTextBox.CueBannerText = "";
			OutputPathTextBox.Location = new System.Drawing.Point(209, 32);
			OutputPathTextBox.Name = "OutputPathTextBox";
			OutputPathTextBox.Size = new System.Drawing.Size(445, 22);
			OutputPathTextBox.TabIndex = 16;
			//
			//OutputSubfolderTextBox
			//
			OutputSubfolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputSubfolderTextBox.CueBannerText = "";
			OutputSubfolderTextBox.Location = new System.Drawing.Point(209, 32);
			OutputSubfolderTextBox.Name = "OutputSubfolderTextBox";
			OutputSubfolderTextBox.Size = new System.Drawing.Size(445, 22);
			OutputSubfolderTextBox.TabIndex = 22;
			OutputSubfolderTextBox.Visible = false;
			//
			//OutputPathComboBox
			//
			OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			OutputPathComboBox.FormattingEnabled = true;
			OutputPathComboBox.Location = new System.Drawing.Point(71, 33);
			OutputPathComboBox.Name = "OutputPathComboBox";
			OutputPathComboBox.Size = new System.Drawing.Size(132, 21);
			OutputPathComboBox.TabIndex = 14;
			//
			//Label2
			//
			Label2.AutoSize = true;
			Label2.Location = new System.Drawing.Point(3, 37);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(62, 13);
			Label2.TabIndex = 13;
			Label2.Text = "Output to:";
			//
			//UseDefaultOutputSubfolderButton
			//
			UseDefaultOutputSubfolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseDefaultOutputSubfolderButton.Location = new System.Drawing.Point(660, 32);
			UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton";
			UseDefaultOutputSubfolderButton.Size = new System.Drawing.Size(113, 23);
			UseDefaultOutputSubfolderButton.TabIndex = 19;
			UseDefaultOutputSubfolderButton.Text = "Use Default";
			UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = true;
			//
			//PackagesLabel
			//
			PackagesLabel.AutoSize = true;
			PackagesLabel.Location = new System.Drawing.Point(3, 8);
			PackagesLabel.Name = "PackagesLabel";
			PackagesLabel.Size = new System.Drawing.Size(57, 13);
			PackagesLabel.TabIndex = 1;
			PackagesLabel.Text = "Packages:";
			//
			//PackagePathFileNameTextBox
			//
			PackagePathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			PackagePathFileNameTextBox.CueBannerText = "";
			PackagePathFileNameTextBox.Location = new System.Drawing.Point(209, 3);
			PackagePathFileNameTextBox.Name = "PackagePathFileNameTextBox";
			PackagePathFileNameTextBox.Size = new System.Drawing.Size(445, 22);
			PackagePathFileNameTextBox.TabIndex = 2;
			//
			//BrowseForPackagePathFolderOrFileNameButton
			//
			BrowseForPackagePathFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForPackagePathFolderOrFileNameButton.Location = new System.Drawing.Point(660, 3);
			BrowseForPackagePathFolderOrFileNameButton.Name = "BrowseForPackagePathFolderOrFileNameButton";
			BrowseForPackagePathFolderOrFileNameButton.Size = new System.Drawing.Size(64, 23);
			BrowseForPackagePathFolderOrFileNameButton.TabIndex = 3;
			BrowseForPackagePathFolderOrFileNameButton.Text = "Browse...";
			BrowseForPackagePathFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//GotoPackageButton
			//
			GotoPackageButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoPackageButton.Location = new System.Drawing.Point(730, 3);
			GotoPackageButton.Name = "GotoPackageButton";
			GotoPackageButton.Size = new System.Drawing.Size(43, 23);
			GotoPackageButton.TabIndex = 4;
			GotoPackageButton.Text = "Goto";
			GotoPackageButton.UseVisualStyleBackColor = true;
			//
			//Options_LogSplitContainer
			//
			Options_LogSplitContainer.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			Options_LogSplitContainer.Location = new System.Drawing.Point(3, 61);
			Options_LogSplitContainer.Name = "Options_LogSplitContainer";
			Options_LogSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//Options_LogSplitContainer.Panel1
			//
			Options_LogSplitContainer.Panel1.Controls.Add(SplitContainer2);
			Options_LogSplitContainer.Panel1MinSize = 45;
			//
			//Options_LogSplitContainer.Panel2
			//
			Options_LogSplitContainer.Panel2.Controls.Add(UnpackerLogTextBox);
			Options_LogSplitContainer.Panel2.Controls.Add(UnpackButtonsPanel);
			Options_LogSplitContainer.Panel2.Controls.Add(PostUnpackPanel);
			Options_LogSplitContainer.Panel2MinSize = 90;
			Options_LogSplitContainer.Size = new System.Drawing.Size(770, 472);
			Options_LogSplitContainer.SplitterDistance = 347;
			Options_LogSplitContainer.TabIndex = 6;
			//
			//SplitContainer2
			//
			SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			SplitContainer2.Location = new System.Drawing.Point(0, 0);
			SplitContainer2.Name = "SplitContainer2";
			//
			//SplitContainer2.Panel1
			//
			SplitContainer2.Panel1.Controls.Add(ContentsGroupBox);
			//
			//SplitContainer2.Panel2
			//
			SplitContainer2.Panel2.Controls.Add(OptionsGroupBox);
			SplitContainer2.Size = new System.Drawing.Size(770, 347);
			SplitContainer2.SplitterDistance = 600;
			SplitContainer2.SplitterWidth = 6;
			SplitContainer2.TabIndex = 0;
			//
			//ContentsGroupBox
			//
			ContentsGroupBox.Controls.Add(ContentsGroupBoxFillPanel);
			ContentsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			ContentsGroupBox.Location = new System.Drawing.Point(0, 0);
			ContentsGroupBox.Name = "ContentsGroupBox";
			ContentsGroupBox.Size = new System.Drawing.Size(600, 347);
			ContentsGroupBox.TabIndex = 0;
			ContentsGroupBox.TabStop = false;
			ContentsGroupBox.Text = "Contents of package";
			//
			//ContentsGroupBoxFillPanel
			//
			ContentsGroupBoxFillPanel.AutoScroll = true;
			ContentsGroupBoxFillPanel.Controls.Add(SplitContainer3);
			ContentsGroupBoxFillPanel.Controls.Add(Panel3);
			ContentsGroupBoxFillPanel.Controls.Add(ToolStrip1);
			ContentsGroupBoxFillPanel.Controls.Add(ContentsMinScrollerPanel);
			ContentsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			ContentsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			ContentsGroupBoxFillPanel.Name = "ContentsGroupBoxFillPanel";
			ContentsGroupBoxFillPanel.Size = new System.Drawing.Size(594, 326);
			ContentsGroupBoxFillPanel.TabIndex = 12;
			//
			//SplitContainer3
			//
			SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			SplitContainer3.Location = new System.Drawing.Point(0, 26);
			SplitContainer3.Name = "SplitContainer3";
			//
			//SplitContainer3.Panel1
			//
			SplitContainer3.Panel1.Controls.Add(PackageTreeView);
			//
			//SplitContainer3.Panel2
			//
			SplitContainer3.Panel2.Controls.Add(PackageListView);
			SplitContainer3.Size = new System.Drawing.Size(594, 275);
			SplitContainer3.SplitterDistance = 250;
			SplitContainer3.TabIndex = 6;
			//
			//PackageTreeView
			//
			PackageTreeView.BackColor = System.Drawing.SystemColors.Control;
			PackageTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			PackageTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			PackageTreeView.HideSelection = false;
			PackageTreeView.ImageIndex = 0;
			PackageTreeView.ImageList = ImageList1;
			PackageTreeView.Location = new System.Drawing.Point(0, 0);
			PackageTreeView.Name = "PackageTreeView";
			PackageTreeView.SelectedImageIndex = 0;
			PackageTreeView.Size = new System.Drawing.Size(250, 275);
			PackageTreeView.TabIndex = 0;
			//
			//PackageListView
			//
			PackageListView.AllowColumnReorder = true;
			PackageListView.BackColor = System.Drawing.SystemColors.Control;
			PackageListView.Dock = System.Windows.Forms.DockStyle.Fill;
			PackageListView.HideSelection = false;
			PackageListView.Location = new System.Drawing.Point(0, 0);
			PackageListView.Name = "PackageListView";
			PackageListView.ShowGroups = false;
			PackageListView.Size = new System.Drawing.Size(340, 275);
			PackageListView.SmallImageList = ImageList1;
			PackageListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			PackageListView.TabIndex = 1;
			PackageListView.UseCompatibleStateImageBehavior = false;
			PackageListView.View = System.Windows.Forms.View.Details;
			//
			//Panel3
			//
			Panel3.Controls.Add(SelectionPathTextBox);
			Panel3.Dock = System.Windows.Forms.DockStyle.Top;
			Panel3.Location = new System.Drawing.Point(0, 0);
			Panel3.Name = "Panel3";
			Panel3.Size = new System.Drawing.Size(594, 26);
			Panel3.TabIndex = 11;
			//
			//SelectionPathTextBox
			//
			SelectionPathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			SelectionPathTextBox.Location = new System.Drawing.Point(0, 0);
			SelectionPathTextBox.Name = "SelectionPathTextBox";
			SelectionPathTextBox.ReadOnly = true;
			SelectionPathTextBox.Size = new System.Drawing.Size(594, 22);
			SelectionPathTextBox.TabIndex = 1;
			//
			//ToolStrip1
			//
			ToolStrip1.CanOverflow = false;
			ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {SearchToolStripComboBox, FindToolStripTextBox, FindToolStripButton, ToolStripSeparator1, FilesSelectedCountToolStripLabel, ToolStripSeparator3, SizeSelectedTotalToolStripLabel, ToolStripSeparator2, RefreshListingToolStripButton});
			ToolStrip1.Location = new System.Drawing.Point(0, 301);
			ToolStrip1.Name = "ToolStrip1";
			ToolStrip1.Size = new System.Drawing.Size(594, 25);
			ToolStrip1.Stretch = true;
			ToolStrip1.TabIndex = 10;
			ToolStrip1.Text = "ToolStrip1";
			//
			//SearchToolStripComboBox
			//
			SearchToolStripComboBox.Name = "SearchToolStripComboBox";
			SearchToolStripComboBox.Size = new System.Drawing.Size(121, 25);
			SearchToolStripComboBox.ToolTipText = "What to search";
			//
			//FindToolStripTextBox
			//
			FindToolStripTextBox.Name = "FindToolStripTextBox";
			FindToolStripTextBox.Size = new System.Drawing.Size(336, 25);
			FindToolStripTextBox.ToolTipText = "Text to find";
			//
			//FindToolStripButton
			//
			FindToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			FindToolStripButton.Image = global::Crowbar.Properties.Resources.Find;
			FindToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			FindToolStripButton.Name = "FindToolStripButton";
			FindToolStripButton.RightToLeftAutoMirrorImage = true;
			FindToolStripButton.Size = new System.Drawing.Size(23, 22);
			FindToolStripButton.Text = "Find";
			FindToolStripButton.ToolTipText = "Find";
			//
			//ToolStripSeparator1
			//
			ToolStripSeparator1.Name = "ToolStripSeparator1";
			ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			//
			//FilesSelectedCountToolStripLabel
			//
			FilesSelectedCountToolStripLabel.Name = "FilesSelectedCountToolStripLabel";
			FilesSelectedCountToolStripLabel.Size = new System.Drawing.Size(24, 22);
			FilesSelectedCountToolStripLabel.Text = "0/0";
			FilesSelectedCountToolStripLabel.ToolTipText = "Selected item count / Total item count";
			//
			//ToolStripSeparator3
			//
			ToolStripSeparator3.Name = "ToolStripSeparator3";
			ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			//
			//SizeSelectedTotalToolStripLabel
			//
			SizeSelectedTotalToolStripLabel.Name = "SizeSelectedTotalToolStripLabel";
			SizeSelectedTotalToolStripLabel.Size = new System.Drawing.Size(13, 22);
			SizeSelectedTotalToolStripLabel.Text = "0";
			SizeSelectedTotalToolStripLabel.ToolTipText = "Byte count of selected items";
			//
			//ToolStripSeparator2
			//
			ToolStripSeparator2.Name = "ToolStripSeparator2";
			ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			//
			//RefreshListingToolStripButton
			//
			RefreshListingToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			RefreshListingToolStripButton.Image = global::Crowbar.Properties.Resources.Refresh;
			RefreshListingToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			RefreshListingToolStripButton.Name = "RefreshListingToolStripButton";
			RefreshListingToolStripButton.Size = new System.Drawing.Size(23, 22);
			RefreshListingToolStripButton.Text = "Refresh";
			//
			//ContentsMinScrollerPanel
			//
			ContentsMinScrollerPanel.Location = new System.Drawing.Point(0, 0);
			ContentsMinScrollerPanel.Name = "ContentsMinScrollerPanel";
			ContentsMinScrollerPanel.Size = new System.Drawing.Size(250, 110);
			ContentsMinScrollerPanel.TabIndex = 12;
			//
			//OptionsGroupBox
			//
			OptionsGroupBox.Controls.Add(OptionsGroupBoxFillPanel);
			OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			OptionsGroupBox.Name = "OptionsGroupBox";
			OptionsGroupBox.Size = new System.Drawing.Size(164, 347);
			OptionsGroupBox.TabIndex = 0;
			OptionsGroupBox.TabStop = false;
			OptionsGroupBox.Text = "Options";
			//
			//OptionsGroupBoxFillPanel
			//
			OptionsGroupBoxFillPanel.AutoScroll = true;
			OptionsGroupBoxFillPanel.Controls.Add(KeepFullPathCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(FolderForEachPackageCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(Label3);
			OptionsGroupBoxFillPanel.Controls.Add(EditGameSetupButton);
			OptionsGroupBoxFillPanel.Controls.Add(GameSetupComboBox);
			OptionsGroupBoxFillPanel.Controls.Add(SelectAllModelsAndMaterialsFoldersCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(LogFileCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(UnpackOptionsUseDefaultsButton);
			OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel";
			OptionsGroupBoxFillPanel.Size = new System.Drawing.Size(158, 326);
			OptionsGroupBoxFillPanel.TabIndex = 0;
			//
			//KeepFullPathCheckBox
			//
			KeepFullPathCheckBox.AutoSize = true;
			KeepFullPathCheckBox.Location = new System.Drawing.Point(3, 26);
			KeepFullPathCheckBox.Name = "KeepFullPathCheckBox";
			KeepFullPathCheckBox.Size = new System.Drawing.Size(98, 17);
			KeepFullPathCheckBox.TabIndex = 13;
			KeepFullPathCheckBox.Text = "Keep full path";
			KeepFullPathCheckBox.UseVisualStyleBackColor = true;
			//
			//FolderForEachPackageCheckBox
			//
			FolderForEachPackageCheckBox.AutoSize = true;
			FolderForEachPackageCheckBox.Location = new System.Drawing.Point(3, 3);
			FolderForEachPackageCheckBox.Name = "FolderForEachPackageCheckBox";
			FolderForEachPackageCheckBox.Size = new System.Drawing.Size(150, 17);
			FolderForEachPackageCheckBox.TabIndex = 12;
			FolderForEachPackageCheckBox.Text = "Folder for each package";
			FolderForEachPackageCheckBox.UseVisualStyleBackColor = true;
			//
			//Label3
			//
			Label3.AutoSize = true;
			Label3.Location = new System.Drawing.Point(3, 239);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(155, 13);
			Label3.TabIndex = 0;
			Label3.Text = "Game that has the unpacker:";
			Label3.Visible = false;
			//
			//EditGameSetupButton
			//
			EditGameSetupButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			EditGameSetupButton.Location = new System.Drawing.Point(6445, 229);
			EditGameSetupButton.Name = "EditGameSetupButton";
			EditGameSetupButton.Size = new System.Drawing.Size(90, 23);
			EditGameSetupButton.TabIndex = 1;
			EditGameSetupButton.Text = "Set Up Games";
			EditGameSetupButton.UseVisualStyleBackColor = true;
			EditGameSetupButton.Visible = false;
			//
			//GameSetupComboBox
			//
			GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			GameSetupComboBox.FormattingEnabled = true;
			GameSetupComboBox.Location = new System.Drawing.Point(3, 255);
			GameSetupComboBox.Name = "GameSetupComboBox";
			GameSetupComboBox.Size = new System.Drawing.Size(6532, 21);
			GameSetupComboBox.TabIndex = 2;
			GameSetupComboBox.Visible = false;
			//
			//SelectAllModelsAndMaterialsFoldersCheckBox
			//
			SelectAllModelsAndMaterialsFoldersCheckBox.AutoSize = true;
			SelectAllModelsAndMaterialsFoldersCheckBox.Location = new System.Drawing.Point(33, 180);
			SelectAllModelsAndMaterialsFoldersCheckBox.Name = "SelectAllModelsAndMaterialsFoldersCheckBox";
			SelectAllModelsAndMaterialsFoldersCheckBox.Size = new System.Drawing.Size(238, 17);
			SelectAllModelsAndMaterialsFoldersCheckBox.TabIndex = 4;
			SelectAllModelsAndMaterialsFoldersCheckBox.Text = "Select all \"models\" and \"materials\" folders";
			SelectAllModelsAndMaterialsFoldersCheckBox.UseVisualStyleBackColor = true;
			SelectAllModelsAndMaterialsFoldersCheckBox.Visible = false;
			//
			//UnpackOptionsUseDefaultsButton
			//
			UnpackOptionsUseDefaultsButton.Location = new System.Drawing.Point(33, 203);
			UnpackOptionsUseDefaultsButton.Name = "UnpackOptionsUseDefaultsButton";
			UnpackOptionsUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			UnpackOptionsUseDefaultsButton.TabIndex = 6;
			UnpackOptionsUseDefaultsButton.Text = "Use Defaults";
			UnpackOptionsUseDefaultsButton.UseVisualStyleBackColor = true;
			UnpackOptionsUseDefaultsButton.Visible = false;
			//
			//UnpackerLogTextBox
			//
			UnpackerLogTextBox.CueBannerText = "";
			UnpackerLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			UnpackerLogTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			UnpackerLogTextBox.HideSelection = false;
			UnpackerLogTextBox.Location = new System.Drawing.Point(0, 26);
			UnpackerLogTextBox.Name = "UnpackerLogTextBox";
			UnpackerLogTextBox.ReadOnly = true;
			UnpackerLogTextBox.Size = new System.Drawing.Size(770, 69);
			UnpackerLogTextBox.TabIndex = 0;
			UnpackerLogTextBox.Text = "";
			UnpackerLogTextBox.WordWrap = false;
			//
			//UnpackButtonsPanel
			//
			UnpackButtonsPanel.Controls.Add(UnpackButton);
			UnpackButtonsPanel.Controls.Add(SkipCurrentPackageButton);
			UnpackButtonsPanel.Controls.Add(CancelUnpackButton);
			UnpackButtonsPanel.Controls.Add(UseAllInDecompileButton);
			UnpackButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			UnpackButtonsPanel.Location = new System.Drawing.Point(0, 0);
			UnpackButtonsPanel.Name = "UnpackButtonsPanel";
			UnpackButtonsPanel.Size = new System.Drawing.Size(770, 26);
			UnpackButtonsPanel.TabIndex = 1;
			//
			//UnpackButton
			//
			UnpackButton.Enabled = false;
			UnpackButton.Location = new System.Drawing.Point(0, 0);
			UnpackButton.Name = "UnpackButton";
			UnpackButton.Size = new System.Drawing.Size(120, 23);
			UnpackButton.TabIndex = 2;
			UnpackButton.Text = "Unpack";
			UnpackButton.UseVisualStyleBackColor = true;
			//
			//SkipCurrentPackageButton
			//
			SkipCurrentPackageButton.Enabled = false;
			SkipCurrentPackageButton.Location = new System.Drawing.Point(126, 0);
			SkipCurrentPackageButton.Name = "SkipCurrentPackageButton";
			SkipCurrentPackageButton.Size = new System.Drawing.Size(120, 23);
			SkipCurrentPackageButton.TabIndex = 3;
			SkipCurrentPackageButton.Text = "Skip Current Package";
			SkipCurrentPackageButton.UseVisualStyleBackColor = true;
			//
			//CancelUnpackButton
			//
			CancelUnpackButton.Enabled = false;
			CancelUnpackButton.Location = new System.Drawing.Point(252, 0);
			CancelUnpackButton.Name = "CancelUnpackButton";
			CancelUnpackButton.Size = new System.Drawing.Size(120, 23);
			CancelUnpackButton.TabIndex = 4;
			CancelUnpackButton.Text = "Cancel Unpack";
			CancelUnpackButton.UseVisualStyleBackColor = true;
			//
			//UseAllInDecompileButton
			//
			UseAllInDecompileButton.Enabled = false;
			UseAllInDecompileButton.Location = new System.Drawing.Point(378, 0);
			UseAllInDecompileButton.Name = "UseAllInDecompileButton";
			UseAllInDecompileButton.Size = new System.Drawing.Size(120, 23);
			UseAllInDecompileButton.TabIndex = 5;
			UseAllInDecompileButton.Text = "Use All in Decompile";
			UseAllInDecompileButton.UseVisualStyleBackColor = true;
			//
			//PostUnpackPanel
			//
			PostUnpackPanel.Controls.Add(UnpackedFilesComboBox);
			PostUnpackPanel.Controls.Add(UseInPreviewButton);
			PostUnpackPanel.Controls.Add(UseInDecompileButton);
			PostUnpackPanel.Controls.Add(GotoUnpackedFileButton);
			PostUnpackPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			PostUnpackPanel.Location = new System.Drawing.Point(0, 95);
			PostUnpackPanel.Name = "PostUnpackPanel";
			PostUnpackPanel.Size = new System.Drawing.Size(770, 26);
			PostUnpackPanel.TabIndex = 5;
			//
			//UnpackedFilesComboBox
			//
			UnpackedFilesComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			UnpackedFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			UnpackedFilesComboBox.FormattingEnabled = true;
			UnpackedFilesComboBox.Location = new System.Drawing.Point(0, 4);
			UnpackedFilesComboBox.Name = "UnpackedFilesComboBox";
			UnpackedFilesComboBox.Size = new System.Drawing.Size(512, 21);
			UnpackedFilesComboBox.TabIndex = 1;
			//
			//UseInPreviewButton
			//
			UseInPreviewButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseInPreviewButton.Enabled = false;
			UseInPreviewButton.Location = new System.Drawing.Point(518, 3);
			UseInPreviewButton.Name = "UseInPreviewButton";
			UseInPreviewButton.Size = new System.Drawing.Size(91, 23);
			UseInPreviewButton.TabIndex = 2;
			UseInPreviewButton.Text = "Use in Preview";
			UseInPreviewButton.UseVisualStyleBackColor = true;
			//
			//UseInDecompileButton
			//
			UseInDecompileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseInDecompileButton.Enabled = false;
			UseInDecompileButton.Location = new System.Drawing.Point(615, 3);
			UseInDecompileButton.Name = "UseInDecompileButton";
			UseInDecompileButton.Size = new System.Drawing.Size(106, 23);
			UseInDecompileButton.TabIndex = 3;
			UseInDecompileButton.Text = "Use in Decompile";
			UseInDecompileButton.UseVisualStyleBackColor = true;
			//
			//GotoUnpackedFileButton
			//
			GotoUnpackedFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoUnpackedFileButton.Location = new System.Drawing.Point(727, 3);
			GotoUnpackedFileButton.Name = "GotoUnpackedFileButton";
			GotoUnpackedFileButton.Size = new System.Drawing.Size(43, 23);
			GotoUnpackedFileButton.TabIndex = 4;
			GotoUnpackedFileButton.Text = "Goto";
			GotoUnpackedFileButton.UseVisualStyleBackColor = true;
			//
			//UnpackUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Panel2);
			Name = "UnpackUserControl";
			Size = new System.Drawing.Size(776, 536);
			Panel2.ResumeLayout(false);
			Panel2.PerformLayout();
			Options_LogSplitContainer.Panel1.ResumeLayout(false);
			Options_LogSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).EndInit();
			Options_LogSplitContainer.ResumeLayout(false);
			SplitContainer2.Panel1.ResumeLayout(false);
			SplitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)SplitContainer2).EndInit();
			SplitContainer2.ResumeLayout(false);
			ContentsGroupBox.ResumeLayout(false);
			ContentsGroupBoxFillPanel.ResumeLayout(false);
			ContentsGroupBoxFillPanel.PerformLayout();
			SplitContainer3.Panel1.ResumeLayout(false);
			SplitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)SplitContainer3).EndInit();
			SplitContainer3.ResumeLayout(false);
			Panel3.ResumeLayout(false);
			Panel3.PerformLayout();
			ToolStrip1.ResumeLayout(false);
			ToolStrip1.PerformLayout();
			OptionsGroupBox.ResumeLayout(false);
			OptionsGroupBoxFillPanel.ResumeLayout(false);
			OptionsGroupBoxFillPanel.PerformLayout();
			UnpackButtonsPanel.ResumeLayout(false);
			PostUnpackPanel.ResumeLayout(false);
			ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			Load += new System.EventHandler(UnpackUserControl_Load);
			BrowseForPackagePathFolderOrFileNameButton.Click += new System.EventHandler(BrowseForPackagePathFolderOrFileNameButton_Click);
			GotoPackageButton.Click += new System.EventHandler(GotoPackageButton_Click);
			OutputPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragDrop);
			OutputPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragEnter);
			OutputPathTextBox.Validated += new System.EventHandler(OutputPathTextBox_Validated);
			BrowseForOutputPathButton.Click += new System.EventHandler(BrowseForOutputPathButton_Click);
			GotoOutputPathButton.Click += new System.EventHandler(GotoOutputPathButton_Click);
			UseDefaultOutputSubfolderButton.Click += new System.EventHandler(UseDefaultOutputSubfolderButton_Click);
			PackageTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(PackageTreeView_AfterSelect);
			PackageTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(PackageTreeView_ItemDrag);
			PackageTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(PackageTreeView_MouseDown);
			PackageTreeView.SystemColorsChanged += new System.EventHandler(PackageTreeView_SystemColorsChanged);
			PackageListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(PackageListView_ColumnClick);
			PackageListView.DoubleClick += new System.EventHandler(PackageListView_DoubleClick);
			PackageListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(PackageListView_ItemDrag);
			PackageListView.KeyDown += new System.Windows.Forms.KeyEventHandler(PackageListView_KeyDown);
			PackageListView.SelectedIndexChanged += new System.EventHandler(PackageListView_SelectedIndexChanged);
			FindToolStripTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(FindToolStripTextBox_KeyPress);
			FindToolStripButton.Click += new System.EventHandler(FindToolStripButton_Click);
			RefreshListingToolStripButton.Click += new System.EventHandler(RefreshListingToolStripButton_Click);
			UnpackOptionsUseDefaultsButton.Click += new System.EventHandler(UnpackOptionsUseDefaultsButton_Click);
			UnpackButton.Click += new System.EventHandler(UnpackButton_Click);
			SkipCurrentPackageButton.Click += new System.EventHandler(SkipCurrentPackageButton_Click);
			CancelUnpackButton.Click += new System.EventHandler(CancelUnpackButton_Click);
			UseAllInDecompileButton.Click += new System.EventHandler(UseAllInDecompileButton_Click);
			UseInPreviewButton.Click += new System.EventHandler(UseInPreviewButton_Click);
			UseInDecompileButton.Click += new System.EventHandler(UseInDecompileButton_Click);
			GotoUnpackedFileButton.Click += new System.EventHandler(GotoUnpackedFileButton_Click);
		}
		internal System.Windows.Forms.Panel Panel2;
		internal System.Windows.Forms.Button GotoPackageButton;
		internal System.Windows.Forms.Label PackagesLabel;
		internal System.Windows.Forms.Button BrowseForPackagePathFolderOrFileNameButton;
		internal Crowbar.TextBoxEx PackagePathFileNameTextBox;
		internal System.Windows.Forms.SplitContainer Options_LogSplitContainer;
		internal System.Windows.Forms.Button UseAllInDecompileButton;
		internal System.Windows.Forms.ComboBox UnpackComboBox;
		internal System.Windows.Forms.Button CancelUnpackButton;
		internal System.Windows.Forms.Button SkipCurrentPackageButton;
		internal System.Windows.Forms.Button UnpackButton;
		internal System.Windows.Forms.GroupBox OptionsGroupBox;
		internal System.Windows.Forms.Button UseInDecompileButton;
		internal System.Windows.Forms.Button UseInPreviewButton;
		internal Crowbar.RichTextBoxEx UnpackerLogTextBox;
		internal System.Windows.Forms.ComboBox UnpackedFilesComboBox;
		internal System.Windows.Forms.Button GotoUnpackedFileButton;
		internal System.Windows.Forms.GroupBox ContentsGroupBox;
		internal Crowbar.TreeViewEx PackageTreeView;
		internal System.Windows.Forms.SplitContainer SplitContainer2;
		internal System.Windows.Forms.SplitContainer SplitContainer3;
		internal System.Windows.Forms.Button UnpackOptionsUseDefaultsButton;
		internal System.Windows.Forms.TextBox SelectionPathTextBox;
		internal System.Windows.Forms.CheckBox SelectAllModelsAndMaterialsFoldersCheckBox;
		internal System.Windows.Forms.ToolTip ToolTip1;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.ComboBox GameSetupComboBox;
		internal System.Windows.Forms.Button EditGameSetupButton;
		internal System.Windows.Forms.CheckBox LogFileCheckBox;
		internal System.Windows.Forms.ToolStrip ToolStrip1;
		internal System.Windows.Forms.ToolStripLabel FilesSelectedCountToolStripLabel;
		internal ToolStripSpringTextBox FindToolStripTextBox;
		internal System.Windows.Forms.ToolStripButton FindToolStripButton;
		internal System.Windows.Forms.ToolStripLabel SizeSelectedTotalToolStripLabel;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
		internal System.Windows.Forms.Panel OptionsGroupBoxFillPanel;
		internal System.Windows.Forms.ListView PackageListView;
		internal System.Windows.Forms.ImageList ImageList1;
		internal Crowbar.TextBoxEx GameModelsOutputPathTextBox;
		internal System.Windows.Forms.Button GotoOutputPathButton;
		internal System.Windows.Forms.Button BrowseForOutputPathButton;
		internal Crowbar.TextBoxEx OutputPathTextBox;
		internal System.Windows.Forms.ComboBox OutputPathComboBox;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Button UseDefaultOutputSubfolderButton;
		internal Crowbar.TextBoxEx OutputSubfolderTextBox;
		internal TextBoxEx OutputSamePathTextBox;
		internal CheckBox FolderForEachPackageCheckBox;
		internal ToolStripButton RefreshListingToolStripButton;
		internal Panel PostUnpackPanel;
		internal Panel UnpackButtonsPanel;
		internal Panel Panel3;
		internal Panel ContentsGroupBoxFillPanel;
		internal Panel ContentsMinScrollerPanel;
		internal CheckBox KeepFullPathCheckBox;
		internal ToolStripSeparator ToolStripSeparator3;
		internal ToolStripSeparator ToolStripSeparator2;
		internal ToolStripComboBox SearchToolStripComboBox;
	}

}