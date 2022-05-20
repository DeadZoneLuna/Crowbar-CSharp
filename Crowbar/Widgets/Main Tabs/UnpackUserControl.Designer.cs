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
			this.components = new System.ComponentModel.Container();
			this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.LogFileCheckBox = new System.Windows.Forms.CheckBox();
			this.Panel2 = new System.Windows.Forms.Panel();
			this.OutputSamePathTextBox = new Crowbar.TextBoxEx();
			this.GameModelsOutputPathTextBox = new Crowbar.TextBoxEx();
			this.UnpackComboBox = new System.Windows.Forms.ComboBox();
			this.GotoOutputPathButton = new System.Windows.Forms.Button();
			this.BrowseForOutputPathButton = new System.Windows.Forms.Button();
			this.OutputPathTextBox = new Crowbar.TextBoxEx();
			this.OutputSubfolderTextBox = new Crowbar.TextBoxEx();
			this.OutputPathComboBox = new System.Windows.Forms.ComboBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.UseDefaultOutputSubfolderButton = new System.Windows.Forms.Button();
			this.PackagesLabel = new System.Windows.Forms.Label();
			this.PackagePathFileNameTextBox = new Crowbar.TextBoxEx();
			this.BrowseForPackagePathFolderOrFileNameButton = new System.Windows.Forms.Button();
			this.GotoPackageButton = new System.Windows.Forms.Button();
			this.Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
			this.ContentsGroupBox = new System.Windows.Forms.GroupBox();
			this.ContentsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			this.SplitContainer3 = new System.Windows.Forms.SplitContainer();
			this.PackageTreeView = new Crowbar.TreeViewEx();
			this.PackageListView = new System.Windows.Forms.ListView();
			this.Panel3 = new System.Windows.Forms.Panel();
			this.SelectionPathTextBox = new System.Windows.Forms.TextBox();
			this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
			this.SearchToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.FindToolStripTextBox = new Crowbar.ToolStripSpringTextBox();
			this.FindToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.FilesSelectedCountToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.SizeSelectedTotalToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.RefreshListingToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.ContentsMinScrollerPanel = new System.Windows.Forms.Panel();
			this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.OptionsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			this.KeepFullPathCheckBox = new System.Windows.Forms.CheckBox();
			this.FolderForEachPackageCheckBox = new System.Windows.Forms.CheckBox();
			this.Label3 = new System.Windows.Forms.Label();
			this.EditGameSetupButton = new System.Windows.Forms.Button();
			this.GameSetupComboBox = new System.Windows.Forms.ComboBox();
			this.SelectAllModelsAndMaterialsFoldersCheckBox = new System.Windows.Forms.CheckBox();
			this.UnpackOptionsUseDefaultsButton = new System.Windows.Forms.Button();
			this.UnpackerLogTextBox = new Crowbar.RichTextBoxEx();
			this.UnpackButtonsPanel = new System.Windows.Forms.Panel();
			this.UnpackButton = new System.Windows.Forms.Button();
			this.SkipCurrentPackageButton = new System.Windows.Forms.Button();
			this.CancelUnpackButton = new System.Windows.Forms.Button();
			this.UseAllInDecompileButton = new System.Windows.Forms.Button();
			this.PostUnpackPanel = new System.Windows.Forms.Panel();
			this.UnpackedFilesComboBox = new System.Windows.Forms.ComboBox();
			this.UseInPreviewButton = new System.Windows.Forms.Button();
			this.UseInDecompileButton = new System.Windows.Forms.Button();
			this.GotoUnpackedFileButton = new System.Windows.Forms.Button();
			this.Panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).BeginInit();
			this.Options_LogSplitContainer.Panel1.SuspendLayout();
			this.Options_LogSplitContainer.Panel2.SuspendLayout();
			this.Options_LogSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.SplitContainer2).BeginInit();
			this.SplitContainer2.Panel1.SuspendLayout();
			this.SplitContainer2.Panel2.SuspendLayout();
			this.SplitContainer2.SuspendLayout();
			this.ContentsGroupBox.SuspendLayout();
			this.ContentsGroupBoxFillPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.SplitContainer3).BeginInit();
			this.SplitContainer3.Panel1.SuspendLayout();
			this.SplitContainer3.Panel2.SuspendLayout();
			this.SplitContainer3.SuspendLayout();
			this.Panel3.SuspendLayout();
			this.ToolStrip1.SuspendLayout();
			this.OptionsGroupBox.SuspendLayout();
			this.OptionsGroupBoxFillPanel.SuspendLayout();
			this.UnpackButtonsPanel.SuspendLayout();
			this.PostUnpackPanel.SuspendLayout();
			this.SuspendLayout();
			//
			//ImageList1
			//
			this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ImageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
			//
			//LogFileCheckBox
			//
			this.LogFileCheckBox.AutoSize = true;
			this.LogFileCheckBox.Location = new System.Drawing.Point(3, 72);
			this.LogFileCheckBox.Name = "LogFileCheckBox";
			this.LogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			this.LogFileCheckBox.TabIndex = 5;
			this.LogFileCheckBox.Text = "Write log to a file";
			this.ToolTip1.SetToolTip(this.LogFileCheckBox, "Write unpack log to a file.");
			this.LogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//Panel2
			//
			this.Panel2.Controls.Add(this.OutputSamePathTextBox);
			this.Panel2.Controls.Add(this.GameModelsOutputPathTextBox);
			this.Panel2.Controls.Add(this.UnpackComboBox);
			this.Panel2.Controls.Add(this.GotoOutputPathButton);
			this.Panel2.Controls.Add(this.BrowseForOutputPathButton);
			this.Panel2.Controls.Add(this.OutputPathTextBox);
			this.Panel2.Controls.Add(this.OutputSubfolderTextBox);
			this.Panel2.Controls.Add(this.OutputPathComboBox);
			this.Panel2.Controls.Add(this.Label2);
			this.Panel2.Controls.Add(this.UseDefaultOutputSubfolderButton);
			this.Panel2.Controls.Add(this.PackagesLabel);
			this.Panel2.Controls.Add(this.PackagePathFileNameTextBox);
			this.Panel2.Controls.Add(this.BrowseForPackagePathFolderOrFileNameButton);
			this.Panel2.Controls.Add(this.GotoPackageButton);
			this.Panel2.Controls.Add(this.Options_LogSplitContainer);
			this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel2.Location = new System.Drawing.Point(0, 0);
			this.Panel2.Name = "Panel2";
			this.Panel2.Size = new System.Drawing.Size(776, 536);
			this.Panel2.TabIndex = 0;
			//
			//OutputSamePathTextBox
			//
			this.OutputSamePathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputSamePathTextBox.CueBannerText = "";
			this.OutputSamePathTextBox.Location = new System.Drawing.Point(209, 32);
			this.OutputSamePathTextBox.Name = "OutputSamePathTextBox";
			this.OutputSamePathTextBox.ReadOnly = true;
			this.OutputSamePathTextBox.Size = new System.Drawing.Size(445, 22);
			this.OutputSamePathTextBox.TabIndex = 26;
			//
			//GameModelsOutputPathTextBox
			//
			this.GameModelsOutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameModelsOutputPathTextBox.CueBannerText = "";
			this.GameModelsOutputPathTextBox.Location = new System.Drawing.Point(209, 32);
			this.GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox";
			this.GameModelsOutputPathTextBox.ReadOnly = true;
			this.GameModelsOutputPathTextBox.Size = new System.Drawing.Size(445, 22);
			this.GameModelsOutputPathTextBox.TabIndex = 15;
			//
			//UnpackComboBox
			//
			this.UnpackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UnpackComboBox.FormattingEnabled = true;
			this.UnpackComboBox.Location = new System.Drawing.Point(71, 4);
			this.UnpackComboBox.Name = "UnpackComboBox";
			this.UnpackComboBox.Size = new System.Drawing.Size(132, 21);
			this.UnpackComboBox.TabIndex = 1;
			//
			//GotoOutputPathButton
			//
			this.GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			this.GotoOutputPathButton.Name = "GotoOutputPathButton";
			this.GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			this.GotoOutputPathButton.TabIndex = 18;
			this.GotoOutputPathButton.Text = "Goto";
			this.GotoOutputPathButton.UseVisualStyleBackColor = true;
			//
			//BrowseForOutputPathButton
			//
			this.BrowseForOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForOutputPathButton.Enabled = false;
			this.BrowseForOutputPathButton.Location = new System.Drawing.Point(660, 32);
			this.BrowseForOutputPathButton.Name = "BrowseForOutputPathButton";
			this.BrowseForOutputPathButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForOutputPathButton.TabIndex = 17;
			this.BrowseForOutputPathButton.Text = "Browse...";
			this.BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OutputPathTextBox
			//
			this.OutputPathTextBox.AllowDrop = true;
			this.OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputPathTextBox.CueBannerText = "";
			this.OutputPathTextBox.Location = new System.Drawing.Point(209, 32);
			this.OutputPathTextBox.Name = "OutputPathTextBox";
			this.OutputPathTextBox.Size = new System.Drawing.Size(445, 22);
			this.OutputPathTextBox.TabIndex = 16;
			//
			//OutputSubfolderTextBox
			//
			this.OutputSubfolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputSubfolderTextBox.CueBannerText = "";
			this.OutputSubfolderTextBox.Location = new System.Drawing.Point(209, 32);
			this.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox";
			this.OutputSubfolderTextBox.Size = new System.Drawing.Size(445, 22);
			this.OutputSubfolderTextBox.TabIndex = 22;
			this.OutputSubfolderTextBox.Visible = false;
			//
			//OutputPathComboBox
			//
			this.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OutputPathComboBox.FormattingEnabled = true;
			this.OutputPathComboBox.Location = new System.Drawing.Point(71, 33);
			this.OutputPathComboBox.Name = "OutputPathComboBox";
			this.OutputPathComboBox.Size = new System.Drawing.Size(132, 21);
			this.OutputPathComboBox.TabIndex = 14;
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(3, 37);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(62, 13);
			this.Label2.TabIndex = 13;
			this.Label2.Text = "Output to:";
			//
			//UseDefaultOutputSubfolderButton
			//
			this.UseDefaultOutputSubfolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseDefaultOutputSubfolderButton.Location = new System.Drawing.Point(660, 32);
			this.UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton";
			this.UseDefaultOutputSubfolderButton.Size = new System.Drawing.Size(113, 23);
			this.UseDefaultOutputSubfolderButton.TabIndex = 19;
			this.UseDefaultOutputSubfolderButton.Text = "Use Default";
			this.UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = true;
			//
			//PackagesLabel
			//
			this.PackagesLabel.AutoSize = true;
			this.PackagesLabel.Location = new System.Drawing.Point(3, 8);
			this.PackagesLabel.Name = "PackagesLabel";
			this.PackagesLabel.Size = new System.Drawing.Size(57, 13);
			this.PackagesLabel.TabIndex = 1;
			this.PackagesLabel.Text = "Packages:";
			//
			//PackagePathFileNameTextBox
			//
			this.PackagePathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.PackagePathFileNameTextBox.CueBannerText = "";
			this.PackagePathFileNameTextBox.Location = new System.Drawing.Point(209, 3);
			this.PackagePathFileNameTextBox.Name = "PackagePathFileNameTextBox";
			this.PackagePathFileNameTextBox.Size = new System.Drawing.Size(445, 22);
			this.PackagePathFileNameTextBox.TabIndex = 2;
			//
			//BrowseForPackagePathFolderOrFileNameButton
			//
			this.BrowseForPackagePathFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForPackagePathFolderOrFileNameButton.Location = new System.Drawing.Point(660, 3);
			this.BrowseForPackagePathFolderOrFileNameButton.Name = "BrowseForPackagePathFolderOrFileNameButton";
			this.BrowseForPackagePathFolderOrFileNameButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForPackagePathFolderOrFileNameButton.TabIndex = 3;
			this.BrowseForPackagePathFolderOrFileNameButton.Text = "Browse...";
			this.BrowseForPackagePathFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//GotoPackageButton
			//
			this.GotoPackageButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoPackageButton.Location = new System.Drawing.Point(730, 3);
			this.GotoPackageButton.Name = "GotoPackageButton";
			this.GotoPackageButton.Size = new System.Drawing.Size(43, 23);
			this.GotoPackageButton.TabIndex = 4;
			this.GotoPackageButton.Text = "Goto";
			this.GotoPackageButton.UseVisualStyleBackColor = true;
			//
			//Options_LogSplitContainer
			//
			this.Options_LogSplitContainer.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.Options_LogSplitContainer.Location = new System.Drawing.Point(3, 61);
			this.Options_LogSplitContainer.Name = "Options_LogSplitContainer";
			this.Options_LogSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//Options_LogSplitContainer.Panel1
			//
			this.Options_LogSplitContainer.Panel1.Controls.Add(this.SplitContainer2);
			this.Options_LogSplitContainer.Panel1MinSize = 45;
			//
			//Options_LogSplitContainer.Panel2
			//
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.UnpackerLogTextBox);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.UnpackButtonsPanel);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.PostUnpackPanel);
			this.Options_LogSplitContainer.Panel2MinSize = 90;
			this.Options_LogSplitContainer.Size = new System.Drawing.Size(770, 472);
			this.Options_LogSplitContainer.SplitterDistance = 347;
			this.Options_LogSplitContainer.TabIndex = 6;
			//
			//SplitContainer2
			//
			this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
			this.SplitContainer2.Name = "SplitContainer2";
			//
			//SplitContainer2.Panel1
			//
			this.SplitContainer2.Panel1.Controls.Add(this.ContentsGroupBox);
			//
			//SplitContainer2.Panel2
			//
			this.SplitContainer2.Panel2.Controls.Add(this.OptionsGroupBox);
			this.SplitContainer2.Size = new System.Drawing.Size(770, 347);
			this.SplitContainer2.SplitterDistance = 600;
			this.SplitContainer2.SplitterWidth = 6;
			this.SplitContainer2.TabIndex = 0;
			//
			//ContentsGroupBox
			//
			this.ContentsGroupBox.Controls.Add(this.ContentsGroupBoxFillPanel);
			this.ContentsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ContentsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.ContentsGroupBox.Name = "ContentsGroupBox";
			this.ContentsGroupBox.Size = new System.Drawing.Size(600, 347);
			this.ContentsGroupBox.TabIndex = 0;
			this.ContentsGroupBox.TabStop = false;
			this.ContentsGroupBox.Text = "Contents of package";
			//
			//ContentsGroupBoxFillPanel
			//
			this.ContentsGroupBoxFillPanel.AutoScroll = true;
			this.ContentsGroupBoxFillPanel.Controls.Add(this.SplitContainer3);
			this.ContentsGroupBoxFillPanel.Controls.Add(this.Panel3);
			this.ContentsGroupBoxFillPanel.Controls.Add(this.ToolStrip1);
			this.ContentsGroupBoxFillPanel.Controls.Add(this.ContentsMinScrollerPanel);
			this.ContentsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ContentsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			this.ContentsGroupBoxFillPanel.Name = "ContentsGroupBoxFillPanel";
			this.ContentsGroupBoxFillPanel.Size = new System.Drawing.Size(594, 326);
			this.ContentsGroupBoxFillPanel.TabIndex = 12;
			//
			//SplitContainer3
			//
			this.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.SplitContainer3.Location = new System.Drawing.Point(0, 26);
			this.SplitContainer3.Name = "SplitContainer3";
			//
			//SplitContainer3.Panel1
			//
			this.SplitContainer3.Panel1.Controls.Add(this.PackageTreeView);
			//
			//SplitContainer3.Panel2
			//
			this.SplitContainer3.Panel2.Controls.Add(this.PackageListView);
			this.SplitContainer3.Size = new System.Drawing.Size(594, 275);
			this.SplitContainer3.SplitterDistance = 250;
			this.SplitContainer3.TabIndex = 6;
			//
			//PackageTreeView
			//
			this.PackageTreeView.BackColor = System.Drawing.SystemColors.Control;
			this.PackageTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PackageTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.PackageTreeView.HideSelection = false;
			this.PackageTreeView.ImageIndex = 0;
			this.PackageTreeView.ImageList = this.ImageList1;
			this.PackageTreeView.Location = new System.Drawing.Point(0, 0);
			this.PackageTreeView.Name = "PackageTreeView";
			this.PackageTreeView.SelectedImageIndex = 0;
			this.PackageTreeView.Size = new System.Drawing.Size(250, 275);
			this.PackageTreeView.TabIndex = 0;
			//
			//PackageListView
			//
			this.PackageListView.AllowColumnReorder = true;
			this.PackageListView.BackColor = System.Drawing.SystemColors.Control;
			this.PackageListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PackageListView.HideSelection = false;
			this.PackageListView.Location = new System.Drawing.Point(0, 0);
			this.PackageListView.Name = "PackageListView";
			this.PackageListView.ShowGroups = false;
			this.PackageListView.Size = new System.Drawing.Size(340, 275);
			this.PackageListView.SmallImageList = this.ImageList1;
			this.PackageListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.PackageListView.TabIndex = 1;
			this.PackageListView.UseCompatibleStateImageBehavior = false;
			this.PackageListView.View = System.Windows.Forms.View.Details;
			//
			//Panel3
			//
			this.Panel3.Controls.Add(this.SelectionPathTextBox);
			this.Panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.Panel3.Location = new System.Drawing.Point(0, 0);
			this.Panel3.Name = "Panel3";
			this.Panel3.Size = new System.Drawing.Size(594, 26);
			this.Panel3.TabIndex = 11;
			//
			//SelectionPathTextBox
			//
			this.SelectionPathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SelectionPathTextBox.Location = new System.Drawing.Point(0, 0);
			this.SelectionPathTextBox.Name = "SelectionPathTextBox";
			this.SelectionPathTextBox.ReadOnly = true;
			this.SelectionPathTextBox.Size = new System.Drawing.Size(594, 22);
			this.SelectionPathTextBox.TabIndex = 1;
			//
			//ToolStrip1
			//
			this.ToolStrip1.CanOverflow = false;
			this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.SearchToolStripComboBox, this.FindToolStripTextBox, this.FindToolStripButton, this.ToolStripSeparator1, this.FilesSelectedCountToolStripLabel, this.ToolStripSeparator3, this.SizeSelectedTotalToolStripLabel, this.ToolStripSeparator2, this.RefreshListingToolStripButton});
			this.ToolStrip1.Location = new System.Drawing.Point(0, 301);
			this.ToolStrip1.Name = "ToolStrip1";
			this.ToolStrip1.Size = new System.Drawing.Size(594, 25);
			this.ToolStrip1.Stretch = true;
			this.ToolStrip1.TabIndex = 10;
			this.ToolStrip1.Text = "ToolStrip1";
			//
			//SearchToolStripComboBox
			//
			this.SearchToolStripComboBox.Name = "SearchToolStripComboBox";
			this.SearchToolStripComboBox.Size = new System.Drawing.Size(121, 25);
			this.SearchToolStripComboBox.ToolTipText = "What to search";
			//
			//FindToolStripTextBox
			//
			this.FindToolStripTextBox.Name = "FindToolStripTextBox";
			this.FindToolStripTextBox.Size = new System.Drawing.Size(336, 25);
			this.FindToolStripTextBox.ToolTipText = "Text to find";
			//
			//FindToolStripButton
			//
			this.FindToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FindToolStripButton.Image = global::Crowbar.Properties.Resources.Find;
			this.FindToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FindToolStripButton.Name = "FindToolStripButton";
			this.FindToolStripButton.RightToLeftAutoMirrorImage = true;
			this.FindToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.FindToolStripButton.Text = "Find";
			this.FindToolStripButton.ToolTipText = "Find";
			//
			//ToolStripSeparator1
			//
			this.ToolStripSeparator1.Name = "ToolStripSeparator1";
			this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			//
			//FilesSelectedCountToolStripLabel
			//
			this.FilesSelectedCountToolStripLabel.Name = "FilesSelectedCountToolStripLabel";
			this.FilesSelectedCountToolStripLabel.Size = new System.Drawing.Size(24, 22);
			this.FilesSelectedCountToolStripLabel.Text = "0/0";
			this.FilesSelectedCountToolStripLabel.ToolTipText = "Selected item count / Total item count";
			//
			//ToolStripSeparator3
			//
			this.ToolStripSeparator3.Name = "ToolStripSeparator3";
			this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			//
			//SizeSelectedTotalToolStripLabel
			//
			this.SizeSelectedTotalToolStripLabel.Name = "SizeSelectedTotalToolStripLabel";
			this.SizeSelectedTotalToolStripLabel.Size = new System.Drawing.Size(13, 22);
			this.SizeSelectedTotalToolStripLabel.Text = "0";
			this.SizeSelectedTotalToolStripLabel.ToolTipText = "Byte count of selected items";
			//
			//ToolStripSeparator2
			//
			this.ToolStripSeparator2.Name = "ToolStripSeparator2";
			this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			//
			//RefreshListingToolStripButton
			//
			this.RefreshListingToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RefreshListingToolStripButton.Image = global::Crowbar.Properties.Resources.Refresh;
			this.RefreshListingToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RefreshListingToolStripButton.Name = "RefreshListingToolStripButton";
			this.RefreshListingToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.RefreshListingToolStripButton.Text = "Refresh";
			//
			//ContentsMinScrollerPanel
			//
			this.ContentsMinScrollerPanel.Location = new System.Drawing.Point(0, 0);
			this.ContentsMinScrollerPanel.Name = "ContentsMinScrollerPanel";
			this.ContentsMinScrollerPanel.Size = new System.Drawing.Size(250, 110);
			this.ContentsMinScrollerPanel.TabIndex = 12;
			//
			//OptionsGroupBox
			//
			this.OptionsGroupBox.Controls.Add(this.OptionsGroupBoxFillPanel);
			this.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.OptionsGroupBox.Name = "OptionsGroupBox";
			this.OptionsGroupBox.Size = new System.Drawing.Size(164, 347);
			this.OptionsGroupBox.TabIndex = 0;
			this.OptionsGroupBox.TabStop = false;
			this.OptionsGroupBox.Text = "Options";
			//
			//OptionsGroupBoxFillPanel
			//
			this.OptionsGroupBoxFillPanel.AutoScroll = true;
			this.OptionsGroupBoxFillPanel.Controls.Add(this.KeepFullPathCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.FolderForEachPackageCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.Label3);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.EditGameSetupButton);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.GameSetupComboBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.SelectAllModelsAndMaterialsFoldersCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.LogFileCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.UnpackOptionsUseDefaultsButton);
			this.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			this.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel";
			this.OptionsGroupBoxFillPanel.Size = new System.Drawing.Size(158, 326);
			this.OptionsGroupBoxFillPanel.TabIndex = 0;
			//
			//KeepFullPathCheckBox
			//
			this.KeepFullPathCheckBox.AutoSize = true;
			this.KeepFullPathCheckBox.Location = new System.Drawing.Point(3, 26);
			this.KeepFullPathCheckBox.Name = "KeepFullPathCheckBox";
			this.KeepFullPathCheckBox.Size = new System.Drawing.Size(98, 17);
			this.KeepFullPathCheckBox.TabIndex = 13;
			this.KeepFullPathCheckBox.Text = "Keep full path";
			this.KeepFullPathCheckBox.UseVisualStyleBackColor = true;
			//
			//FolderForEachPackageCheckBox
			//
			this.FolderForEachPackageCheckBox.AutoSize = true;
			this.FolderForEachPackageCheckBox.Location = new System.Drawing.Point(3, 3);
			this.FolderForEachPackageCheckBox.Name = "FolderForEachPackageCheckBox";
			this.FolderForEachPackageCheckBox.Size = new System.Drawing.Size(150, 17);
			this.FolderForEachPackageCheckBox.TabIndex = 12;
			this.FolderForEachPackageCheckBox.Text = "Folder for each package";
			this.FolderForEachPackageCheckBox.UseVisualStyleBackColor = true;
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(3, 239);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(155, 13);
			this.Label3.TabIndex = 0;
			this.Label3.Text = "Game that has the unpacker:";
			this.Label3.Visible = false;
			//
			//EditGameSetupButton
			//
			this.EditGameSetupButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.EditGameSetupButton.Location = new System.Drawing.Point(6445, 229);
			this.EditGameSetupButton.Name = "EditGameSetupButton";
			this.EditGameSetupButton.Size = new System.Drawing.Size(90, 23);
			this.EditGameSetupButton.TabIndex = 1;
			this.EditGameSetupButton.Text = "Set Up Games";
			this.EditGameSetupButton.UseVisualStyleBackColor = true;
			this.EditGameSetupButton.Visible = false;
			//
			//GameSetupComboBox
			//
			this.GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GameSetupComboBox.FormattingEnabled = true;
			this.GameSetupComboBox.Location = new System.Drawing.Point(3, 255);
			this.GameSetupComboBox.Name = "GameSetupComboBox";
			this.GameSetupComboBox.Size = new System.Drawing.Size(6532, 21);
			this.GameSetupComboBox.TabIndex = 2;
			this.GameSetupComboBox.Visible = false;
			//
			//SelectAllModelsAndMaterialsFoldersCheckBox
			//
			this.SelectAllModelsAndMaterialsFoldersCheckBox.AutoSize = true;
			this.SelectAllModelsAndMaterialsFoldersCheckBox.Location = new System.Drawing.Point(33, 180);
			this.SelectAllModelsAndMaterialsFoldersCheckBox.Name = "SelectAllModelsAndMaterialsFoldersCheckBox";
			this.SelectAllModelsAndMaterialsFoldersCheckBox.Size = new System.Drawing.Size(238, 17);
			this.SelectAllModelsAndMaterialsFoldersCheckBox.TabIndex = 4;
			this.SelectAllModelsAndMaterialsFoldersCheckBox.Text = "Select all \"models\" and \"materials\" folders";
			this.SelectAllModelsAndMaterialsFoldersCheckBox.UseVisualStyleBackColor = true;
			this.SelectAllModelsAndMaterialsFoldersCheckBox.Visible = false;
			//
			//UnpackOptionsUseDefaultsButton
			//
			this.UnpackOptionsUseDefaultsButton.Location = new System.Drawing.Point(33, 203);
			this.UnpackOptionsUseDefaultsButton.Name = "UnpackOptionsUseDefaultsButton";
			this.UnpackOptionsUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			this.UnpackOptionsUseDefaultsButton.TabIndex = 6;
			this.UnpackOptionsUseDefaultsButton.Text = "Use Defaults";
			this.UnpackOptionsUseDefaultsButton.UseVisualStyleBackColor = true;
			this.UnpackOptionsUseDefaultsButton.Visible = false;
			//
			//UnpackerLogTextBox
			//
			this.UnpackerLogTextBox.CueBannerText = "";
			this.UnpackerLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UnpackerLogTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.UnpackerLogTextBox.HideSelection = false;
			this.UnpackerLogTextBox.Location = new System.Drawing.Point(0, 26);
			this.UnpackerLogTextBox.Name = "UnpackerLogTextBox";
			this.UnpackerLogTextBox.ReadOnly = true;
			this.UnpackerLogTextBox.Size = new System.Drawing.Size(770, 69);
			this.UnpackerLogTextBox.TabIndex = 0;
			this.UnpackerLogTextBox.Text = "";
			this.UnpackerLogTextBox.WordWrap = false;
			//
			//UnpackButtonsPanel
			//
			this.UnpackButtonsPanel.Controls.Add(this.UnpackButton);
			this.UnpackButtonsPanel.Controls.Add(this.SkipCurrentPackageButton);
			this.UnpackButtonsPanel.Controls.Add(this.CancelUnpackButton);
			this.UnpackButtonsPanel.Controls.Add(this.UseAllInDecompileButton);
			this.UnpackButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.UnpackButtonsPanel.Location = new System.Drawing.Point(0, 0);
			this.UnpackButtonsPanel.Name = "UnpackButtonsPanel";
			this.UnpackButtonsPanel.Size = new System.Drawing.Size(770, 26);
			this.UnpackButtonsPanel.TabIndex = 1;
			//
			//UnpackButton
			//
			this.UnpackButton.Enabled = false;
			this.UnpackButton.Location = new System.Drawing.Point(0, 0);
			this.UnpackButton.Name = "UnpackButton";
			this.UnpackButton.Size = new System.Drawing.Size(120, 23);
			this.UnpackButton.TabIndex = 2;
			this.UnpackButton.Text = "Unpack";
			this.UnpackButton.UseVisualStyleBackColor = true;
			//
			//SkipCurrentPackageButton
			//
			this.SkipCurrentPackageButton.Enabled = false;
			this.SkipCurrentPackageButton.Location = new System.Drawing.Point(126, 0);
			this.SkipCurrentPackageButton.Name = "SkipCurrentPackageButton";
			this.SkipCurrentPackageButton.Size = new System.Drawing.Size(120, 23);
			this.SkipCurrentPackageButton.TabIndex = 3;
			this.SkipCurrentPackageButton.Text = "Skip Current Package";
			this.SkipCurrentPackageButton.UseVisualStyleBackColor = true;
			//
			//CancelUnpackButton
			//
			this.CancelUnpackButton.Enabled = false;
			this.CancelUnpackButton.Location = new System.Drawing.Point(252, 0);
			this.CancelUnpackButton.Name = "CancelUnpackButton";
			this.CancelUnpackButton.Size = new System.Drawing.Size(120, 23);
			this.CancelUnpackButton.TabIndex = 4;
			this.CancelUnpackButton.Text = "Cancel Unpack";
			this.CancelUnpackButton.UseVisualStyleBackColor = true;
			//
			//UseAllInDecompileButton
			//
			this.UseAllInDecompileButton.Enabled = false;
			this.UseAllInDecompileButton.Location = new System.Drawing.Point(378, 0);
			this.UseAllInDecompileButton.Name = "UseAllInDecompileButton";
			this.UseAllInDecompileButton.Size = new System.Drawing.Size(120, 23);
			this.UseAllInDecompileButton.TabIndex = 5;
			this.UseAllInDecompileButton.Text = "Use All in Decompile";
			this.UseAllInDecompileButton.UseVisualStyleBackColor = true;
			//
			//PostUnpackPanel
			//
			this.PostUnpackPanel.Controls.Add(this.UnpackedFilesComboBox);
			this.PostUnpackPanel.Controls.Add(this.UseInPreviewButton);
			this.PostUnpackPanel.Controls.Add(this.UseInDecompileButton);
			this.PostUnpackPanel.Controls.Add(this.GotoUnpackedFileButton);
			this.PostUnpackPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PostUnpackPanel.Location = new System.Drawing.Point(0, 95);
			this.PostUnpackPanel.Name = "PostUnpackPanel";
			this.PostUnpackPanel.Size = new System.Drawing.Size(770, 26);
			this.PostUnpackPanel.TabIndex = 5;
			//
			//UnpackedFilesComboBox
			//
			this.UnpackedFilesComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.UnpackedFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UnpackedFilesComboBox.FormattingEnabled = true;
			this.UnpackedFilesComboBox.Location = new System.Drawing.Point(0, 4);
			this.UnpackedFilesComboBox.Name = "UnpackedFilesComboBox";
			this.UnpackedFilesComboBox.Size = new System.Drawing.Size(512, 21);
			this.UnpackedFilesComboBox.TabIndex = 1;
			//
			//UseInPreviewButton
			//
			this.UseInPreviewButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseInPreviewButton.Enabled = false;
			this.UseInPreviewButton.Location = new System.Drawing.Point(518, 3);
			this.UseInPreviewButton.Name = "UseInPreviewButton";
			this.UseInPreviewButton.Size = new System.Drawing.Size(91, 23);
			this.UseInPreviewButton.TabIndex = 2;
			this.UseInPreviewButton.Text = "Use in Preview";
			this.UseInPreviewButton.UseVisualStyleBackColor = true;
			//
			//UseInDecompileButton
			//
			this.UseInDecompileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseInDecompileButton.Enabled = false;
			this.UseInDecompileButton.Location = new System.Drawing.Point(615, 3);
			this.UseInDecompileButton.Name = "UseInDecompileButton";
			this.UseInDecompileButton.Size = new System.Drawing.Size(106, 23);
			this.UseInDecompileButton.TabIndex = 3;
			this.UseInDecompileButton.Text = "Use in Decompile";
			this.UseInDecompileButton.UseVisualStyleBackColor = true;
			//
			//GotoUnpackedFileButton
			//
			this.GotoUnpackedFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoUnpackedFileButton.Location = new System.Drawing.Point(727, 3);
			this.GotoUnpackedFileButton.Name = "GotoUnpackedFileButton";
			this.GotoUnpackedFileButton.Size = new System.Drawing.Size(43, 23);
			this.GotoUnpackedFileButton.TabIndex = 4;
			this.GotoUnpackedFileButton.Text = "Goto";
			this.GotoUnpackedFileButton.UseVisualStyleBackColor = true;
			//
			//UnpackUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel2);
			this.Name = "UnpackUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.Panel2.ResumeLayout(false);
			this.Panel2.PerformLayout();
			this.Options_LogSplitContainer.Panel1.ResumeLayout(false);
			this.Options_LogSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).EndInit();
			this.Options_LogSplitContainer.ResumeLayout(false);
			this.SplitContainer2.Panel1.ResumeLayout(false);
			this.SplitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.SplitContainer2).EndInit();
			this.SplitContainer2.ResumeLayout(false);
			this.ContentsGroupBox.ResumeLayout(false);
			this.ContentsGroupBoxFillPanel.ResumeLayout(false);
			this.ContentsGroupBoxFillPanel.PerformLayout();
			this.SplitContainer3.Panel1.ResumeLayout(false);
			this.SplitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.SplitContainer3).EndInit();
			this.SplitContainer3.ResumeLayout(false);
			this.Panel3.ResumeLayout(false);
			this.Panel3.PerformLayout();
			this.ToolStrip1.ResumeLayout(false);
			this.ToolStrip1.PerformLayout();
			this.OptionsGroupBox.ResumeLayout(false);
			this.OptionsGroupBoxFillPanel.ResumeLayout(false);
			this.OptionsGroupBoxFillPanel.PerformLayout();
			this.UnpackButtonsPanel.ResumeLayout(false);
			this.PostUnpackPanel.ResumeLayout(false);
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			this.Load += new System.EventHandler(UnpackUserControl_Load);
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