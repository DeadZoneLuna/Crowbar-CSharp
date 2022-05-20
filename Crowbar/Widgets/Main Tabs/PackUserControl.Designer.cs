using System.IO;
using System.Web.Script.Serialization;

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
	public partial class PackUserControl : BaseUserControl
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
			this.Panel1 = new System.Windows.Forms.Panel();
			this.GotoOutputPathButton = new System.Windows.Forms.Button();
			this.BrowseForOutputPathButton = new System.Windows.Forms.Button();
			this.OutputPathTextBox = new Crowbar.TextBoxEx();
			this.OutputParentPathTextBox = new Crowbar.TextBoxEx();
			this.OutputPathComboBox = new System.Windows.Forms.ComboBox();
			this.InputComboBox = new System.Windows.Forms.ComboBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.GotoInputPathButton = new System.Windows.Forms.Button();
			this.Label6 = new System.Windows.Forms.Label();
			this.InputPathFileNameTextBox = new Crowbar.TextBoxEx();
			this.BrowseForInputFolderOrFileNameButton = new System.Windows.Forms.Button();
			this.Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.OptionsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			this.PackerOptionsPanel = new System.Windows.Forms.Panel();
			this.MultiFileVpkCheckBox = new System.Windows.Forms.CheckBox();
			this.PackOptionsUseDefaultsButton = new System.Windows.Forms.Button();
			this.LogFileCheckBox = new System.Windows.Forms.CheckBox();
			this.Label3 = new System.Windows.Forms.Label();
			this.GameSetupComboBox = new System.Windows.Forms.ComboBox();
			this.SetUpGamesButton = new System.Windows.Forms.Button();
			this.GmaPanel = new System.Windows.Forms.Panel();
			this.GmaTitleTextBox = new Crowbar.TextBoxEx();
			this.GmaTitleLabel = new System.Windows.Forms.Label();
			this.GmaGarrysModTagsUserControl = new Crowbar.GarrysModTagsUserControl();
			this.DirectPackerOptionsLabel = new System.Windows.Forms.Label();
			this.DirectPackerOptionsTextBox = new System.Windows.Forms.TextBox();
			this.PackerOptionsTextBox = new System.Windows.Forms.TextBox();
			this.PackerOptionsTextBoxMinScrollPanel = new System.Windows.Forms.Panel();
			this.LogRichTextBox = new Crowbar.RichTextBoxEx();
			this.PackButtonsPanel = new System.Windows.Forms.Panel();
			this.PackButton = new System.Windows.Forms.Button();
			this.SkipCurrentFolderButton = new System.Windows.Forms.Button();
			this.CancelPackButton = new System.Windows.Forms.Button();
			this.UseAllInPublishButton = new System.Windows.Forms.Button();
			this.PostPackPanel = new System.Windows.Forms.Panel();
			this.PackedFilesComboBox = new System.Windows.Forms.ComboBox();
			this.UseInPublishButton = new System.Windows.Forms.Button();
			this.GotoPackedFileButton = new System.Windows.Forms.Button();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).BeginInit();
			this.Options_LogSplitContainer.Panel1.SuspendLayout();
			this.Options_LogSplitContainer.Panel2.SuspendLayout();
			this.Options_LogSplitContainer.SuspendLayout();
			this.OptionsGroupBox.SuspendLayout();
			this.OptionsGroupBoxFillPanel.SuspendLayout();
			this.PackerOptionsPanel.SuspendLayout();
			this.GmaPanel.SuspendLayout();
			this.PackButtonsPanel.SuspendLayout();
			this.PostPackPanel.SuspendLayout();
			this.SuspendLayout();
			//
			//Panel1
			//
			this.Panel1.Controls.Add(this.GotoOutputPathButton);
			this.Panel1.Controls.Add(this.BrowseForOutputPathButton);
			this.Panel1.Controls.Add(this.OutputPathTextBox);
			this.Panel1.Controls.Add(this.OutputParentPathTextBox);
			this.Panel1.Controls.Add(this.OutputPathComboBox);
			this.Panel1.Controls.Add(this.InputComboBox);
			this.Panel1.Controls.Add(this.Label1);
			this.Panel1.Controls.Add(this.GotoInputPathButton);
			this.Panel1.Controls.Add(this.Label6);
			this.Panel1.Controls.Add(this.InputPathFileNameTextBox);
			this.Panel1.Controls.Add(this.BrowseForInputFolderOrFileNameButton);
			this.Panel1.Controls.Add(this.Options_LogSplitContainer);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel1.Location = new System.Drawing.Point(0, 0);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(776, 536);
			this.Panel1.TabIndex = 3;
			//
			//GotoOutputPathButton
			//
			this.GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			this.GotoOutputPathButton.Name = "GotoOutputPathButton";
			this.GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			this.GotoOutputPathButton.TabIndex = 27;
			this.GotoOutputPathButton.Text = "Goto";
			this.GotoOutputPathButton.UseVisualStyleBackColor = true;
			//
			//BrowseForOutputPathButton
			//
			this.BrowseForOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForOutputPathButton.Location = new System.Drawing.Point(660, 32);
			this.BrowseForOutputPathButton.Name = "BrowseForOutputPathButton";
			this.BrowseForOutputPathButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForOutputPathButton.TabIndex = 26;
			this.BrowseForOutputPathButton.Text = "Browse...";
			this.BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OutputPathTextBox
			//
			this.OutputPathTextBox.AllowDrop = true;
			this.OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputPathTextBox.CueBannerText = "";
			this.OutputPathTextBox.Location = new System.Drawing.Point(223, 32);
			this.OutputPathTextBox.Name = "OutputPathTextBox";
			this.OutputPathTextBox.Size = new System.Drawing.Size(431, 22);
			this.OutputPathTextBox.TabIndex = 25;
			//
			//OutputParentPathTextBox
			//
			this.OutputParentPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputParentPathTextBox.CueBannerText = "";
			this.OutputParentPathTextBox.Location = new System.Drawing.Point(223, 32);
			this.OutputParentPathTextBox.Name = "OutputParentPathTextBox";
			this.OutputParentPathTextBox.ReadOnly = true;
			this.OutputParentPathTextBox.Size = new System.Drawing.Size(431, 22);
			this.OutputParentPathTextBox.TabIndex = 24;
			this.OutputParentPathTextBox.Visible = false;
			//
			//OutputPathComboBox
			//
			this.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OutputPathComboBox.FormattingEnabled = true;
			this.OutputPathComboBox.Location = new System.Drawing.Point(77, 33);
			this.OutputPathComboBox.Name = "OutputPathComboBox";
			this.OutputPathComboBox.Size = new System.Drawing.Size(140, 21);
			this.OutputPathComboBox.TabIndex = 23;
			//
			//InputComboBox
			//
			this.InputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.InputComboBox.FormattingEnabled = true;
			this.InputComboBox.Location = new System.Drawing.Point(77, 4);
			this.InputComboBox.Name = "InputComboBox";
			this.InputComboBox.Size = new System.Drawing.Size(140, 21);
			this.InputComboBox.TabIndex = 0;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(3, 37);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(62, 13);
			this.Label1.TabIndex = 22;
			this.Label1.Text = "Output to:";
			//
			//GotoInputPathButton
			//
			this.GotoInputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoInputPathButton.Location = new System.Drawing.Point(730, 3);
			this.GotoInputPathButton.Name = "GotoInputPathButton";
			this.GotoInputPathButton.Size = new System.Drawing.Size(43, 23);
			this.GotoInputPathButton.TabIndex = 21;
			this.GotoInputPathButton.Text = "Goto";
			this.GotoInputPathButton.UseVisualStyleBackColor = true;
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(3, 8);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(74, 13);
			this.Label6.TabIndex = 17;
			this.Label6.Text = "Folder input:";
			//
			//InputPathFileNameTextBox
			//
			this.InputPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.InputPathFileNameTextBox.CueBannerText = "";
			this.InputPathFileNameTextBox.Location = new System.Drawing.Point(223, 3);
			this.InputPathFileNameTextBox.Name = "InputPathFileNameTextBox";
			this.InputPathFileNameTextBox.Size = new System.Drawing.Size(431, 22);
			this.InputPathFileNameTextBox.TabIndex = 1;
			//
			//BrowseForInputFolderOrFileNameButton
			//
			this.BrowseForInputFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForInputFolderOrFileNameButton.Location = new System.Drawing.Point(660, 3);
			this.BrowseForInputFolderOrFileNameButton.Name = "BrowseForInputFolderOrFileNameButton";
			this.BrowseForInputFolderOrFileNameButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForInputFolderOrFileNameButton.TabIndex = 20;
			this.BrowseForInputFolderOrFileNameButton.Text = "Browse...";
			this.BrowseForInputFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//Options_LogSplitContainer
			//
			this.Options_LogSplitContainer.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.Options_LogSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Options_LogSplitContainer.Location = new System.Drawing.Point(3, 61);
			this.Options_LogSplitContainer.Name = "Options_LogSplitContainer";
			this.Options_LogSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//Options_LogSplitContainer.Panel1
			//
			this.Options_LogSplitContainer.Panel1.Controls.Add(this.OptionsGroupBox);
			this.Options_LogSplitContainer.Panel1MinSize = 45;
			//
			//Options_LogSplitContainer.Panel2
			//
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.LogRichTextBox);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.PackButtonsPanel);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.PostPackPanel);
			this.Options_LogSplitContainer.Panel2MinSize = 45;
			this.Options_LogSplitContainer.Size = new System.Drawing.Size(770, 472);
			this.Options_LogSplitContainer.SplitterDistance = 260;
			this.Options_LogSplitContainer.TabIndex = 29;
			//
			//OptionsGroupBox
			//
			this.OptionsGroupBox.Controls.Add(this.OptionsGroupBoxFillPanel);
			this.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.OptionsGroupBox.Name = "OptionsGroupBox";
			this.OptionsGroupBox.Size = new System.Drawing.Size(770, 260);
			this.OptionsGroupBox.TabIndex = 0;
			this.OptionsGroupBox.TabStop = false;
			this.OptionsGroupBox.Text = "Options";
			//
			//OptionsGroupBoxFillPanel
			//
			this.OptionsGroupBoxFillPanel.AutoScroll = true;
			this.OptionsGroupBoxFillPanel.Controls.Add(this.PackerOptionsPanel);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.DirectPackerOptionsLabel);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.DirectPackerOptionsTextBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.PackerOptionsTextBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.PackerOptionsTextBoxMinScrollPanel);
			this.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			this.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel";
			this.OptionsGroupBoxFillPanel.Size = new System.Drawing.Size(764, 239);
			this.OptionsGroupBoxFillPanel.TabIndex = 19;
			//
			//PackerOptionsPanel
			//
			this.PackerOptionsPanel.AutoScroll = true;
			this.PackerOptionsPanel.Controls.Add(this.MultiFileVpkCheckBox);
			this.PackerOptionsPanel.Controls.Add(this.PackOptionsUseDefaultsButton);
			this.PackerOptionsPanel.Controls.Add(this.LogFileCheckBox);
			this.PackerOptionsPanel.Controls.Add(this.Label3);
			this.PackerOptionsPanel.Controls.Add(this.GameSetupComboBox);
			this.PackerOptionsPanel.Controls.Add(this.SetUpGamesButton);
			this.PackerOptionsPanel.Controls.Add(this.GmaPanel);
			this.PackerOptionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.PackerOptionsPanel.Location = new System.Drawing.Point(0, 0);
			this.PackerOptionsPanel.Name = "PackerOptionsPanel";
			this.PackerOptionsPanel.Size = new System.Drawing.Size(764, 153);
			this.PackerOptionsPanel.TabIndex = 0;
			//
			//MultiFileVpkCheckBox
			//
			this.MultiFileVpkCheckBox.AutoSize = true;
			this.MultiFileVpkCheckBox.Location = new System.Drawing.Point(6, 51);
			this.MultiFileVpkCheckBox.Name = "MultiFileVpkCheckBox";
			this.MultiFileVpkCheckBox.Size = new System.Drawing.Size(125, 17);
			this.MultiFileVpkCheckBox.TabIndex = 13;
			this.MultiFileVpkCheckBox.Text = "Write multi-file VPK";
			this.MultiFileVpkCheckBox.UseVisualStyleBackColor = true;
			this.MultiFileVpkCheckBox.Visible = false;
			//
			//PackOptionsUseDefaultsButton
			//
			this.PackOptionsUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.PackOptionsUseDefaultsButton.Location = new System.Drawing.Point(674, 127);
			this.PackOptionsUseDefaultsButton.Name = "PackOptionsUseDefaultsButton";
			this.PackOptionsUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			this.PackOptionsUseDefaultsButton.TabIndex = 12;
			this.PackOptionsUseDefaultsButton.Text = "Use Defaults";
			this.PackOptionsUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//LogFileCheckBox
			//
			this.LogFileCheckBox.AutoSize = true;
			this.LogFileCheckBox.Location = new System.Drawing.Point(6, 28);
			this.LogFileCheckBox.Name = "LogFileCheckBox";
			this.LogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			this.LogFileCheckBox.TabIndex = 4;
			this.LogFileCheckBox.Text = "Write log to a file";
			this.LogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(0, 5);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(165, 13);
			this.Label3.TabIndex = 0;
			this.Label3.Text = "Game that has the packer tool:";
			//
			//GameSetupComboBox
			//
			this.GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GameSetupComboBox.FormattingEnabled = true;
			this.GameSetupComboBox.Location = new System.Drawing.Point(171, 1);
			this.GameSetupComboBox.Name = "GameSetupComboBox";
			this.GameSetupComboBox.Size = new System.Drawing.Size(497, 21);
			this.GameSetupComboBox.TabIndex = 1;
			//
			//SetUpGamesButton
			//
			this.SetUpGamesButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.SetUpGamesButton.Location = new System.Drawing.Point(674, 0);
			this.SetUpGamesButton.Name = "SetUpGamesButton";
			this.SetUpGamesButton.Size = new System.Drawing.Size(90, 23);
			this.SetUpGamesButton.TabIndex = 2;
			this.SetUpGamesButton.Text = "Set Up Games";
			this.SetUpGamesButton.UseVisualStyleBackColor = true;
			//
			//GmaPanel
			//
			this.GmaPanel.Controls.Add(this.GmaTitleTextBox);
			this.GmaPanel.Controls.Add(this.GmaTitleLabel);
			this.GmaPanel.Controls.Add(this.GmaGarrysModTagsUserControl);
			this.GmaPanel.Location = new System.Drawing.Point(217, 29);
			this.GmaPanel.Name = "GmaPanel";
			this.GmaPanel.Size = new System.Drawing.Size(423, 122);
			this.GmaPanel.TabIndex = 0;
			//
			//GmaTitleTextBox
			//
			this.GmaTitleTextBox.CueBannerText = "";
			this.GmaTitleTextBox.Location = new System.Drawing.Point(42, 1);
			this.GmaTitleTextBox.Name = "GmaTitleTextBox";
			this.GmaTitleTextBox.Size = new System.Drawing.Size(317, 22);
			this.GmaTitleTextBox.TabIndex = 14;
			//
			//GmaTitleLabel
			//
			this.GmaTitleLabel.AutoSize = true;
			this.GmaTitleLabel.Location = new System.Drawing.Point(3, 6);
			this.GmaTitleLabel.Name = "GmaTitleLabel";
			this.GmaTitleLabel.Size = new System.Drawing.Size(32, 13);
			this.GmaTitleLabel.TabIndex = 4;
			this.GmaTitleLabel.Text = "Title:";
			//
			//GmaGarrysModTagsUserControl
			//
			this.GmaGarrysModTagsUserControl.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.GmaGarrysModTagsUserControl.Location = new System.Drawing.Point(0, 28);
			this.GmaGarrysModTagsUserControl.Name = "GmaGarrysModTagsUserControl";
			this.GmaGarrysModTagsUserControl.Orientation = Crowbar.AppEnums.OrientationType.Horizontal;
			this.GmaGarrysModTagsUserControl.Size = new System.Drawing.Size(362, 94);
			this.GmaGarrysModTagsUserControl.TabIndex = 15;
			//
			//DirectPackerOptionsLabel
			//
			this.DirectPackerOptionsLabel.Location = new System.Drawing.Point(0, 154);
			this.DirectPackerOptionsLabel.Name = "DirectPackerOptionsLabel";
			this.DirectPackerOptionsLabel.Size = new System.Drawing.Size(764, 13);
			this.DirectPackerOptionsLabel.TabIndex = 16;
			this.DirectPackerOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):";
			//
			//DirectPackerOptionsTextBox
			//
			this.DirectPackerOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DirectPackerOptionsTextBox.Location = new System.Drawing.Point(0, 170);
			this.DirectPackerOptionsTextBox.Name = "DirectPackerOptionsTextBox";
			this.DirectPackerOptionsTextBox.Size = new System.Drawing.Size(764, 22);
			this.DirectPackerOptionsTextBox.TabIndex = 17;
			//
			//PackerOptionsTextBox
			//
			this.PackerOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.PackerOptionsTextBox.Location = new System.Drawing.Point(0, 198);
			this.PackerOptionsTextBox.Multiline = true;
			this.PackerOptionsTextBox.Name = "PackerOptionsTextBox";
			this.PackerOptionsTextBox.ReadOnly = true;
			this.PackerOptionsTextBox.Size = new System.Drawing.Size(764, 37);
			this.PackerOptionsTextBox.TabIndex = 18;
			//
			//PackerOptionsTextBoxMinScrollPanel
			//
			this.PackerOptionsTextBoxMinScrollPanel.Location = new System.Drawing.Point(0, 198);
			this.PackerOptionsTextBoxMinScrollPanel.Name = "PackerOptionsTextBoxMinScrollPanel";
			this.PackerOptionsTextBoxMinScrollPanel.Size = new System.Drawing.Size(764, 37);
			this.PackerOptionsTextBoxMinScrollPanel.TabIndex = 42;
			//
			//LogRichTextBox
			//
			this.LogRichTextBox.CueBannerText = "";
			this.LogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LogRichTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.LogRichTextBox.HideSelection = false;
			this.LogRichTextBox.Location = new System.Drawing.Point(0, 26);
			this.LogRichTextBox.Name = "LogRichTextBox";
			this.LogRichTextBox.ReadOnly = true;
			this.LogRichTextBox.Size = new System.Drawing.Size(770, 156);
			this.LogRichTextBox.TabIndex = 0;
			this.LogRichTextBox.Text = "";
			this.LogRichTextBox.WordWrap = false;
			//
			//PackButtonsPanel
			//
			this.PackButtonsPanel.Controls.Add(this.PackButton);
			this.PackButtonsPanel.Controls.Add(this.SkipCurrentFolderButton);
			this.PackButtonsPanel.Controls.Add(this.CancelPackButton);
			this.PackButtonsPanel.Controls.Add(this.UseAllInPublishButton);
			this.PackButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.PackButtonsPanel.Location = new System.Drawing.Point(0, 0);
			this.PackButtonsPanel.Name = "PackButtonsPanel";
			this.PackButtonsPanel.Size = new System.Drawing.Size(770, 26);
			this.PackButtonsPanel.TabIndex = 5;
			//
			//PackButton
			//
			this.PackButton.Location = new System.Drawing.Point(0, 0);
			this.PackButton.Name = "PackButton";
			this.PackButton.Size = new System.Drawing.Size(120, 23);
			this.PackButton.TabIndex = 1;
			this.PackButton.Text = "Pack";
			this.PackButton.UseVisualStyleBackColor = true;
			//
			//SkipCurrentFolderButton
			//
			this.SkipCurrentFolderButton.Enabled = false;
			this.SkipCurrentFolderButton.Location = new System.Drawing.Point(126, 0);
			this.SkipCurrentFolderButton.Name = "SkipCurrentFolderButton";
			this.SkipCurrentFolderButton.Size = new System.Drawing.Size(120, 23);
			this.SkipCurrentFolderButton.TabIndex = 2;
			this.SkipCurrentFolderButton.Text = "Skip Current Folder";
			this.SkipCurrentFolderButton.UseVisualStyleBackColor = true;
			//
			//CancelPackButton
			//
			this.CancelPackButton.Enabled = false;
			this.CancelPackButton.Location = new System.Drawing.Point(252, 0);
			this.CancelPackButton.Name = "CancelPackButton";
			this.CancelPackButton.Size = new System.Drawing.Size(120, 23);
			this.CancelPackButton.TabIndex = 3;
			this.CancelPackButton.Text = "Cancel Pack";
			this.CancelPackButton.UseVisualStyleBackColor = true;
			//
			//UseAllInPublishButton
			//
			this.UseAllInPublishButton.Enabled = false;
			this.UseAllInPublishButton.Location = new System.Drawing.Point(378, 0);
			this.UseAllInPublishButton.Name = "UseAllInPublishButton";
			this.UseAllInPublishButton.Size = new System.Drawing.Size(120, 23);
			this.UseAllInPublishButton.TabIndex = 4;
			this.UseAllInPublishButton.Text = "Use All in Publish";
			this.UseAllInPublishButton.UseVisualStyleBackColor = true;
			this.UseAllInPublishButton.Visible = false;
			//
			//PostPackPanel
			//
			this.PostPackPanel.Controls.Add(this.PackedFilesComboBox);
			this.PostPackPanel.Controls.Add(this.UseInPublishButton);
			this.PostPackPanel.Controls.Add(this.GotoPackedFileButton);
			this.PostPackPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PostPackPanel.Location = new System.Drawing.Point(0, 182);
			this.PostPackPanel.Name = "PostPackPanel";
			this.PostPackPanel.Size = new System.Drawing.Size(770, 26);
			this.PostPackPanel.TabIndex = 6;
			//
			//PackedFilesComboBox
			//
			this.PackedFilesComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.PackedFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PackedFilesComboBox.FormattingEnabled = true;
			this.PackedFilesComboBox.Location = new System.Drawing.Point(0, 4);
			this.PackedFilesComboBox.Name = "PackedFilesComboBox";
			this.PackedFilesComboBox.Size = new System.Drawing.Size(721, 21);
			this.PackedFilesComboBox.TabIndex = 1;
			//
			//UseInPublishButton
			//
			this.UseInPublishButton.Enabled = false;
			this.UseInPublishButton.Location = new System.Drawing.Point(632, 3);
			this.UseInPublishButton.Name = "UseInPublishButton";
			this.UseInPublishButton.Size = new System.Drawing.Size(89, 23);
			this.UseInPublishButton.TabIndex = 3;
			this.UseInPublishButton.Text = "Use in Publish";
			this.UseInPublishButton.UseVisualStyleBackColor = true;
			this.UseInPublishButton.Visible = false;
			//
			//GotoPackedFileButton
			//
			this.GotoPackedFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoPackedFileButton.Location = new System.Drawing.Point(727, 3);
			this.GotoPackedFileButton.Name = "GotoPackedFileButton";
			this.GotoPackedFileButton.Size = new System.Drawing.Size(43, 23);
			this.GotoPackedFileButton.TabIndex = 4;
			this.GotoPackedFileButton.Text = "Goto";
			this.GotoPackedFileButton.UseVisualStyleBackColor = true;
			//
			//PackUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel1);
			this.Name = "PackUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			this.Options_LogSplitContainer.Panel1.ResumeLayout(false);
			this.Options_LogSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).EndInit();
			this.Options_LogSplitContainer.ResumeLayout(false);
			this.OptionsGroupBox.ResumeLayout(false);
			this.OptionsGroupBoxFillPanel.ResumeLayout(false);
			this.OptionsGroupBoxFillPanel.PerformLayout();
			this.PackerOptionsPanel.ResumeLayout(false);
			this.PackerOptionsPanel.PerformLayout();
			this.GmaPanel.ResumeLayout(false);
			this.GmaPanel.PerformLayout();
			this.PackButtonsPanel.ResumeLayout(false);
			this.PostPackPanel.ResumeLayout(false);
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			this.Load += new System.EventHandler(PackUserControl_Load);
			BrowseForInputFolderOrFileNameButton.Click += new System.EventHandler(BrowseForInputFolderOrFileNameButton_Click);
			GotoInputPathButton.Click += new System.EventHandler(GotoInputPathButton_Click);
			OutputPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragDrop);
			OutputPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragEnter);
			OutputPathTextBox.Validated += new System.EventHandler(OutputPathTextBox_Validated);
			BrowseForOutputPathButton.Click += new System.EventHandler(BrowseForOutputPathButton_Click);
			GotoOutputPathButton.Click += new System.EventHandler(GotoOutputPathButton_Click);
			DirectPackerOptionsTextBox.TextChanged += new System.EventHandler(DirectPackerOptionsTextBox_TextChanged);
			PackButton.Click += new System.EventHandler(PackButton_Click);
			SkipCurrentFolderButton.Click += new System.EventHandler(SkipCurrentFolderButtonButton_Click);
			CancelPackButton.Click += new System.EventHandler(CancelPackButton_Click);
			UseAllInPublishButton.Click += new System.EventHandler(UseAllInPublishButton_Click);
			UseInPublishButton.Click += new System.EventHandler(UseInPublishButton_Click);
			GotoPackedFileButton.Click += new System.EventHandler(GotoPackedFileButton_Click);
		}
		internal System.Windows.Forms.Panel Panel1;
		internal Crowbar.TextBoxEx OutputParentPathTextBox;
		internal System.Windows.Forms.Button GotoOutputPathButton;
		internal System.Windows.Forms.Button BrowseForOutputPathButton;
		internal Crowbar.TextBoxEx OutputPathTextBox;
		internal System.Windows.Forms.ComboBox OutputPathComboBox;
		internal System.Windows.Forms.ComboBox InputComboBox;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Button GotoInputPathButton;
		internal System.Windows.Forms.Label Label6;
		internal Crowbar.TextBoxEx InputPathFileNameTextBox;
		internal System.Windows.Forms.Button BrowseForInputFolderOrFileNameButton;
		internal System.Windows.Forms.SplitContainer Options_LogSplitContainer;
		internal System.Windows.Forms.Button UseAllInPublishButton;
		internal System.Windows.Forms.GroupBox OptionsGroupBox;
		internal System.Windows.Forms.Panel PackerOptionsPanel;
		internal System.Windows.Forms.Button PackOptionsUseDefaultsButton;
		internal System.Windows.Forms.CheckBox LogFileCheckBox;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.ComboBox GameSetupComboBox;
		internal System.Windows.Forms.Button SetUpGamesButton;
		internal System.Windows.Forms.Button CancelPackButton;
		internal System.Windows.Forms.Button SkipCurrentFolderButton;
		internal System.Windows.Forms.Button PackButton;
		internal System.Windows.Forms.Button UseInPublishButton;
		internal Crowbar.RichTextBoxEx LogRichTextBox;
		internal System.Windows.Forms.ComboBox PackedFilesComboBox;
		internal System.Windows.Forms.Button GotoPackedFileButton;
		internal Label DirectPackerOptionsLabel;
		internal TextBox DirectPackerOptionsTextBox;
		internal TextBox PackerOptionsTextBox;
		internal ToolTip ToolTip1;
		internal CheckBox MultiFileVpkCheckBox;
		internal Panel PackButtonsPanel;
		internal Panel PostPackPanel;
		internal Panel OptionsGroupBoxFillPanel;
		internal Panel PackerOptionsTextBoxMinScrollPanel;
		internal Label GmaTitleLabel;
		internal TextBoxEx GmaTitleTextBox;
		internal GarrysModTagsUserControl GmaGarrysModTagsUserControl;
		internal Panel GmaPanel;
	}

}