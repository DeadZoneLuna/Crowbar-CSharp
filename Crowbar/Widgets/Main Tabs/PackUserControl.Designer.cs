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
			components = new System.ComponentModel.Container();
			Panel1 = new System.Windows.Forms.Panel();
			GotoOutputPathButton = new System.Windows.Forms.Button();
			BrowseForOutputPathButton = new System.Windows.Forms.Button();
			OutputPathTextBox = new Crowbar.TextBoxEx();
			OutputParentPathTextBox = new Crowbar.TextBoxEx();
			OutputPathComboBox = new System.Windows.Forms.ComboBox();
			InputComboBox = new System.Windows.Forms.ComboBox();
			Label1 = new System.Windows.Forms.Label();
			GotoInputPathButton = new System.Windows.Forms.Button();
			Label6 = new System.Windows.Forms.Label();
			InputPathFileNameTextBox = new Crowbar.TextBoxEx();
			BrowseForInputFolderOrFileNameButton = new System.Windows.Forms.Button();
			Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			OptionsGroupBox = new System.Windows.Forms.GroupBox();
			OptionsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			PackerOptionsPanel = new System.Windows.Forms.Panel();
			MultiFileVpkCheckBox = new System.Windows.Forms.CheckBox();
			PackOptionsUseDefaultsButton = new System.Windows.Forms.Button();
			LogFileCheckBox = new System.Windows.Forms.CheckBox();
			Label3 = new System.Windows.Forms.Label();
			GameSetupComboBox = new System.Windows.Forms.ComboBox();
			SetUpGamesButton = new System.Windows.Forms.Button();
			GmaPanel = new System.Windows.Forms.Panel();
			GmaTitleTextBox = new Crowbar.TextBoxEx();
			GmaTitleLabel = new System.Windows.Forms.Label();
			GmaGarrysModTagsUserControl = new Crowbar.GarrysModTagsUserControl();
			DirectPackerOptionsLabel = new System.Windows.Forms.Label();
			DirectPackerOptionsTextBox = new System.Windows.Forms.TextBox();
			PackerOptionsTextBox = new System.Windows.Forms.TextBox();
			PackerOptionsTextBoxMinScrollPanel = new System.Windows.Forms.Panel();
			LogRichTextBox = new Crowbar.RichTextBoxEx();
			PackButtonsPanel = new System.Windows.Forms.Panel();
			PackButton = new System.Windows.Forms.Button();
			SkipCurrentFolderButton = new System.Windows.Forms.Button();
			CancelPackButton = new System.Windows.Forms.Button();
			UseAllInPublishButton = new System.Windows.Forms.Button();
			PostPackPanel = new System.Windows.Forms.Panel();
			PackedFilesComboBox = new System.Windows.Forms.ComboBox();
			UseInPublishButton = new System.Windows.Forms.Button();
			GotoPackedFileButton = new System.Windows.Forms.Button();
			ToolTip1 = new System.Windows.Forms.ToolTip(components);
			Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).BeginInit();
			Options_LogSplitContainer.Panel1.SuspendLayout();
			Options_LogSplitContainer.Panel2.SuspendLayout();
			Options_LogSplitContainer.SuspendLayout();
			OptionsGroupBox.SuspendLayout();
			OptionsGroupBoxFillPanel.SuspendLayout();
			PackerOptionsPanel.SuspendLayout();
			GmaPanel.SuspendLayout();
			PackButtonsPanel.SuspendLayout();
			PostPackPanel.SuspendLayout();
			SuspendLayout();
			//
			//Panel1
			//
			Panel1.Controls.Add(GotoOutputPathButton);
			Panel1.Controls.Add(BrowseForOutputPathButton);
			Panel1.Controls.Add(OutputPathTextBox);
			Panel1.Controls.Add(OutputParentPathTextBox);
			Panel1.Controls.Add(OutputPathComboBox);
			Panel1.Controls.Add(InputComboBox);
			Panel1.Controls.Add(Label1);
			Panel1.Controls.Add(GotoInputPathButton);
			Panel1.Controls.Add(Label6);
			Panel1.Controls.Add(InputPathFileNameTextBox);
			Panel1.Controls.Add(BrowseForInputFolderOrFileNameButton);
			Panel1.Controls.Add(Options_LogSplitContainer);
			Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel1.Location = new System.Drawing.Point(0, 0);
			Panel1.Name = "Panel1";
			Panel1.Size = new System.Drawing.Size(776, 536);
			Panel1.TabIndex = 3;
			//
			//GotoOutputPathButton
			//
			GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			GotoOutputPathButton.Name = "GotoOutputPathButton";
			GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			GotoOutputPathButton.TabIndex = 27;
			GotoOutputPathButton.Text = "Goto";
			GotoOutputPathButton.UseVisualStyleBackColor = true;
			//
			//BrowseForOutputPathButton
			//
			BrowseForOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForOutputPathButton.Location = new System.Drawing.Point(660, 32);
			BrowseForOutputPathButton.Name = "BrowseForOutputPathButton";
			BrowseForOutputPathButton.Size = new System.Drawing.Size(64, 23);
			BrowseForOutputPathButton.TabIndex = 26;
			BrowseForOutputPathButton.Text = "Browse...";
			BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OutputPathTextBox
			//
			OutputPathTextBox.AllowDrop = true;
			OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputPathTextBox.CueBannerText = "";
			OutputPathTextBox.Location = new System.Drawing.Point(223, 32);
			OutputPathTextBox.Name = "OutputPathTextBox";
			OutputPathTextBox.Size = new System.Drawing.Size(431, 22);
			OutputPathTextBox.TabIndex = 25;
			//
			//OutputParentPathTextBox
			//
			OutputParentPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputParentPathTextBox.CueBannerText = "";
			OutputParentPathTextBox.Location = new System.Drawing.Point(223, 32);
			OutputParentPathTextBox.Name = "OutputParentPathTextBox";
			OutputParentPathTextBox.ReadOnly = true;
			OutputParentPathTextBox.Size = new System.Drawing.Size(431, 22);
			OutputParentPathTextBox.TabIndex = 24;
			OutputParentPathTextBox.Visible = false;
			//
			//OutputPathComboBox
			//
			OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			OutputPathComboBox.FormattingEnabled = true;
			OutputPathComboBox.Location = new System.Drawing.Point(77, 33);
			OutputPathComboBox.Name = "OutputPathComboBox";
			OutputPathComboBox.Size = new System.Drawing.Size(140, 21);
			OutputPathComboBox.TabIndex = 23;
			//
			//InputComboBox
			//
			InputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			InputComboBox.FormattingEnabled = true;
			InputComboBox.Location = new System.Drawing.Point(77, 4);
			InputComboBox.Name = "InputComboBox";
			InputComboBox.Size = new System.Drawing.Size(140, 21);
			InputComboBox.TabIndex = 0;
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(3, 37);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(62, 13);
			Label1.TabIndex = 22;
			Label1.Text = "Output to:";
			//
			//GotoInputPathButton
			//
			GotoInputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoInputPathButton.Location = new System.Drawing.Point(730, 3);
			GotoInputPathButton.Name = "GotoInputPathButton";
			GotoInputPathButton.Size = new System.Drawing.Size(43, 23);
			GotoInputPathButton.TabIndex = 21;
			GotoInputPathButton.Text = "Goto";
			GotoInputPathButton.UseVisualStyleBackColor = true;
			//
			//Label6
			//
			Label6.AutoSize = true;
			Label6.Location = new System.Drawing.Point(3, 8);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(74, 13);
			Label6.TabIndex = 17;
			Label6.Text = "Folder input:";
			//
			//InputPathFileNameTextBox
			//
			InputPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			InputPathFileNameTextBox.CueBannerText = "";
			InputPathFileNameTextBox.Location = new System.Drawing.Point(223, 3);
			InputPathFileNameTextBox.Name = "InputPathFileNameTextBox";
			InputPathFileNameTextBox.Size = new System.Drawing.Size(431, 22);
			InputPathFileNameTextBox.TabIndex = 1;
			//
			//BrowseForInputFolderOrFileNameButton
			//
			BrowseForInputFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForInputFolderOrFileNameButton.Location = new System.Drawing.Point(660, 3);
			BrowseForInputFolderOrFileNameButton.Name = "BrowseForInputFolderOrFileNameButton";
			BrowseForInputFolderOrFileNameButton.Size = new System.Drawing.Size(64, 23);
			BrowseForInputFolderOrFileNameButton.TabIndex = 20;
			BrowseForInputFolderOrFileNameButton.Text = "Browse...";
			BrowseForInputFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//Options_LogSplitContainer
			//
			Options_LogSplitContainer.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			Options_LogSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			Options_LogSplitContainer.Location = new System.Drawing.Point(3, 61);
			Options_LogSplitContainer.Name = "Options_LogSplitContainer";
			Options_LogSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//Options_LogSplitContainer.Panel1
			//
			Options_LogSplitContainer.Panel1.Controls.Add(OptionsGroupBox);
			Options_LogSplitContainer.Panel1MinSize = 45;
			//
			//Options_LogSplitContainer.Panel2
			//
			Options_LogSplitContainer.Panel2.Controls.Add(LogRichTextBox);
			Options_LogSplitContainer.Panel2.Controls.Add(PackButtonsPanel);
			Options_LogSplitContainer.Panel2.Controls.Add(PostPackPanel);
			Options_LogSplitContainer.Panel2MinSize = 45;
			Options_LogSplitContainer.Size = new System.Drawing.Size(770, 472);
			Options_LogSplitContainer.SplitterDistance = 260;
			Options_LogSplitContainer.TabIndex = 29;
			//
			//OptionsGroupBox
			//
			OptionsGroupBox.Controls.Add(OptionsGroupBoxFillPanel);
			OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			OptionsGroupBox.Name = "OptionsGroupBox";
			OptionsGroupBox.Size = new System.Drawing.Size(770, 260);
			OptionsGroupBox.TabIndex = 0;
			OptionsGroupBox.TabStop = false;
			OptionsGroupBox.Text = "Options";
			//
			//OptionsGroupBoxFillPanel
			//
			OptionsGroupBoxFillPanel.AutoScroll = true;
			OptionsGroupBoxFillPanel.Controls.Add(PackerOptionsPanel);
			OptionsGroupBoxFillPanel.Controls.Add(DirectPackerOptionsLabel);
			OptionsGroupBoxFillPanel.Controls.Add(DirectPackerOptionsTextBox);
			OptionsGroupBoxFillPanel.Controls.Add(PackerOptionsTextBox);
			OptionsGroupBoxFillPanel.Controls.Add(PackerOptionsTextBoxMinScrollPanel);
			OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel";
			OptionsGroupBoxFillPanel.Size = new System.Drawing.Size(764, 239);
			OptionsGroupBoxFillPanel.TabIndex = 19;
			//
			//PackerOptionsPanel
			//
			PackerOptionsPanel.AutoScroll = true;
			PackerOptionsPanel.Controls.Add(MultiFileVpkCheckBox);
			PackerOptionsPanel.Controls.Add(PackOptionsUseDefaultsButton);
			PackerOptionsPanel.Controls.Add(LogFileCheckBox);
			PackerOptionsPanel.Controls.Add(Label3);
			PackerOptionsPanel.Controls.Add(GameSetupComboBox);
			PackerOptionsPanel.Controls.Add(SetUpGamesButton);
			PackerOptionsPanel.Controls.Add(GmaPanel);
			PackerOptionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			PackerOptionsPanel.Location = new System.Drawing.Point(0, 0);
			PackerOptionsPanel.Name = "PackerOptionsPanel";
			PackerOptionsPanel.Size = new System.Drawing.Size(764, 153);
			PackerOptionsPanel.TabIndex = 0;
			//
			//MultiFileVpkCheckBox
			//
			MultiFileVpkCheckBox.AutoSize = true;
			MultiFileVpkCheckBox.Location = new System.Drawing.Point(6, 51);
			MultiFileVpkCheckBox.Name = "MultiFileVpkCheckBox";
			MultiFileVpkCheckBox.Size = new System.Drawing.Size(125, 17);
			MultiFileVpkCheckBox.TabIndex = 13;
			MultiFileVpkCheckBox.Text = "Write multi-file VPK";
			MultiFileVpkCheckBox.UseVisualStyleBackColor = true;
			MultiFileVpkCheckBox.Visible = false;
			//
			//PackOptionsUseDefaultsButton
			//
			PackOptionsUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			PackOptionsUseDefaultsButton.Location = new System.Drawing.Point(674, 127);
			PackOptionsUseDefaultsButton.Name = "PackOptionsUseDefaultsButton";
			PackOptionsUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			PackOptionsUseDefaultsButton.TabIndex = 12;
			PackOptionsUseDefaultsButton.Text = "Use Defaults";
			PackOptionsUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//LogFileCheckBox
			//
			LogFileCheckBox.AutoSize = true;
			LogFileCheckBox.Location = new System.Drawing.Point(6, 28);
			LogFileCheckBox.Name = "LogFileCheckBox";
			LogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			LogFileCheckBox.TabIndex = 4;
			LogFileCheckBox.Text = "Write log to a file";
			LogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//Label3
			//
			Label3.AutoSize = true;
			Label3.Location = new System.Drawing.Point(0, 5);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(165, 13);
			Label3.TabIndex = 0;
			Label3.Text = "Game that has the packer tool:";
			//
			//GameSetupComboBox
			//
			GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			GameSetupComboBox.FormattingEnabled = true;
			GameSetupComboBox.Location = new System.Drawing.Point(171, 1);
			GameSetupComboBox.Name = "GameSetupComboBox";
			GameSetupComboBox.Size = new System.Drawing.Size(497, 21);
			GameSetupComboBox.TabIndex = 1;
			//
			//SetUpGamesButton
			//
			SetUpGamesButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			SetUpGamesButton.Location = new System.Drawing.Point(674, 0);
			SetUpGamesButton.Name = "SetUpGamesButton";
			SetUpGamesButton.Size = new System.Drawing.Size(90, 23);
			SetUpGamesButton.TabIndex = 2;
			SetUpGamesButton.Text = "Set Up Games";
			SetUpGamesButton.UseVisualStyleBackColor = true;
			//
			//GmaPanel
			//
			GmaPanel.Controls.Add(GmaTitleTextBox);
			GmaPanel.Controls.Add(GmaTitleLabel);
			GmaPanel.Controls.Add(GmaGarrysModTagsUserControl);
			GmaPanel.Location = new System.Drawing.Point(217, 29);
			GmaPanel.Name = "GmaPanel";
			GmaPanel.Size = new System.Drawing.Size(423, 122);
			GmaPanel.TabIndex = 0;
			//
			//GmaTitleTextBox
			//
			GmaTitleTextBox.CueBannerText = "";
			GmaTitleTextBox.Location = new System.Drawing.Point(42, 1);
			GmaTitleTextBox.Name = "GmaTitleTextBox";
			GmaTitleTextBox.Size = new System.Drawing.Size(317, 22);
			GmaTitleTextBox.TabIndex = 14;
			//
			//GmaTitleLabel
			//
			GmaTitleLabel.AutoSize = true;
			GmaTitleLabel.Location = new System.Drawing.Point(3, 6);
			GmaTitleLabel.Name = "GmaTitleLabel";
			GmaTitleLabel.Size = new System.Drawing.Size(32, 13);
			GmaTitleLabel.TabIndex = 4;
			GmaTitleLabel.Text = "Title:";
			//
			//GmaGarrysModTagsUserControl
			//
			GmaGarrysModTagsUserControl.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			GmaGarrysModTagsUserControl.Location = new System.Drawing.Point(0, 28);
			GmaGarrysModTagsUserControl.Name = "GmaGarrysModTagsUserControl";
			GmaGarrysModTagsUserControl.Orientation = Crowbar.AppEnums.OrientationType.Horizontal;
			GmaGarrysModTagsUserControl.Size = new System.Drawing.Size(362, 94);
			GmaGarrysModTagsUserControl.TabIndex = 15;
			//
			//DirectPackerOptionsLabel
			//
			DirectPackerOptionsLabel.Location = new System.Drawing.Point(0, 154);
			DirectPackerOptionsLabel.Name = "DirectPackerOptionsLabel";
			DirectPackerOptionsLabel.Size = new System.Drawing.Size(764, 13);
			DirectPackerOptionsLabel.TabIndex = 16;
			DirectPackerOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):";
			//
			//DirectPackerOptionsTextBox
			//
			DirectPackerOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			DirectPackerOptionsTextBox.Location = new System.Drawing.Point(0, 170);
			DirectPackerOptionsTextBox.Name = "DirectPackerOptionsTextBox";
			DirectPackerOptionsTextBox.Size = new System.Drawing.Size(764, 22);
			DirectPackerOptionsTextBox.TabIndex = 17;
			//
			//PackerOptionsTextBox
			//
			PackerOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			PackerOptionsTextBox.Location = new System.Drawing.Point(0, 198);
			PackerOptionsTextBox.Multiline = true;
			PackerOptionsTextBox.Name = "PackerOptionsTextBox";
			PackerOptionsTextBox.ReadOnly = true;
			PackerOptionsTextBox.Size = new System.Drawing.Size(764, 37);
			PackerOptionsTextBox.TabIndex = 18;
			//
			//PackerOptionsTextBoxMinScrollPanel
			//
			PackerOptionsTextBoxMinScrollPanel.Location = new System.Drawing.Point(0, 198);
			PackerOptionsTextBoxMinScrollPanel.Name = "PackerOptionsTextBoxMinScrollPanel";
			PackerOptionsTextBoxMinScrollPanel.Size = new System.Drawing.Size(764, 37);
			PackerOptionsTextBoxMinScrollPanel.TabIndex = 42;
			//
			//LogRichTextBox
			//
			LogRichTextBox.CueBannerText = "";
			LogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			LogRichTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			LogRichTextBox.HideSelection = false;
			LogRichTextBox.Location = new System.Drawing.Point(0, 26);
			LogRichTextBox.Name = "LogRichTextBox";
			LogRichTextBox.ReadOnly = true;
			LogRichTextBox.Size = new System.Drawing.Size(770, 156);
			LogRichTextBox.TabIndex = 0;
			LogRichTextBox.Text = "";
			LogRichTextBox.WordWrap = false;
			//
			//PackButtonsPanel
			//
			PackButtonsPanel.Controls.Add(PackButton);
			PackButtonsPanel.Controls.Add(SkipCurrentFolderButton);
			PackButtonsPanel.Controls.Add(CancelPackButton);
			PackButtonsPanel.Controls.Add(UseAllInPublishButton);
			PackButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			PackButtonsPanel.Location = new System.Drawing.Point(0, 0);
			PackButtonsPanel.Name = "PackButtonsPanel";
			PackButtonsPanel.Size = new System.Drawing.Size(770, 26);
			PackButtonsPanel.TabIndex = 5;
			//
			//PackButton
			//
			PackButton.Location = new System.Drawing.Point(0, 0);
			PackButton.Name = "PackButton";
			PackButton.Size = new System.Drawing.Size(120, 23);
			PackButton.TabIndex = 1;
			PackButton.Text = "Pack";
			PackButton.UseVisualStyleBackColor = true;
			//
			//SkipCurrentFolderButton
			//
			SkipCurrentFolderButton.Enabled = false;
			SkipCurrentFolderButton.Location = new System.Drawing.Point(126, 0);
			SkipCurrentFolderButton.Name = "SkipCurrentFolderButton";
			SkipCurrentFolderButton.Size = new System.Drawing.Size(120, 23);
			SkipCurrentFolderButton.TabIndex = 2;
			SkipCurrentFolderButton.Text = "Skip Current Folder";
			SkipCurrentFolderButton.UseVisualStyleBackColor = true;
			//
			//CancelPackButton
			//
			CancelPackButton.Enabled = false;
			CancelPackButton.Location = new System.Drawing.Point(252, 0);
			CancelPackButton.Name = "CancelPackButton";
			CancelPackButton.Size = new System.Drawing.Size(120, 23);
			CancelPackButton.TabIndex = 3;
			CancelPackButton.Text = "Cancel Pack";
			CancelPackButton.UseVisualStyleBackColor = true;
			//
			//UseAllInPublishButton
			//
			UseAllInPublishButton.Enabled = false;
			UseAllInPublishButton.Location = new System.Drawing.Point(378, 0);
			UseAllInPublishButton.Name = "UseAllInPublishButton";
			UseAllInPublishButton.Size = new System.Drawing.Size(120, 23);
			UseAllInPublishButton.TabIndex = 4;
			UseAllInPublishButton.Text = "Use All in Publish";
			UseAllInPublishButton.UseVisualStyleBackColor = true;
			UseAllInPublishButton.Visible = false;
			//
			//PostPackPanel
			//
			PostPackPanel.Controls.Add(PackedFilesComboBox);
			PostPackPanel.Controls.Add(UseInPublishButton);
			PostPackPanel.Controls.Add(GotoPackedFileButton);
			PostPackPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			PostPackPanel.Location = new System.Drawing.Point(0, 182);
			PostPackPanel.Name = "PostPackPanel";
			PostPackPanel.Size = new System.Drawing.Size(770, 26);
			PostPackPanel.TabIndex = 6;
			//
			//PackedFilesComboBox
			//
			PackedFilesComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			PackedFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			PackedFilesComboBox.FormattingEnabled = true;
			PackedFilesComboBox.Location = new System.Drawing.Point(0, 4);
			PackedFilesComboBox.Name = "PackedFilesComboBox";
			PackedFilesComboBox.Size = new System.Drawing.Size(721, 21);
			PackedFilesComboBox.TabIndex = 1;
			//
			//UseInPublishButton
			//
			UseInPublishButton.Enabled = false;
			UseInPublishButton.Location = new System.Drawing.Point(632, 3);
			UseInPublishButton.Name = "UseInPublishButton";
			UseInPublishButton.Size = new System.Drawing.Size(89, 23);
			UseInPublishButton.TabIndex = 3;
			UseInPublishButton.Text = "Use in Publish";
			UseInPublishButton.UseVisualStyleBackColor = true;
			UseInPublishButton.Visible = false;
			//
			//GotoPackedFileButton
			//
			GotoPackedFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoPackedFileButton.Location = new System.Drawing.Point(727, 3);
			GotoPackedFileButton.Name = "GotoPackedFileButton";
			GotoPackedFileButton.Size = new System.Drawing.Size(43, 23);
			GotoPackedFileButton.TabIndex = 4;
			GotoPackedFileButton.Text = "Goto";
			GotoPackedFileButton.UseVisualStyleBackColor = true;
			//
			//PackUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Panel1);
			Name = "PackUserControl";
			Size = new System.Drawing.Size(776, 536);
			Panel1.ResumeLayout(false);
			Panel1.PerformLayout();
			Options_LogSplitContainer.Panel1.ResumeLayout(false);
			Options_LogSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).EndInit();
			Options_LogSplitContainer.ResumeLayout(false);
			OptionsGroupBox.ResumeLayout(false);
			OptionsGroupBoxFillPanel.ResumeLayout(false);
			OptionsGroupBoxFillPanel.PerformLayout();
			PackerOptionsPanel.ResumeLayout(false);
			PackerOptionsPanel.PerformLayout();
			GmaPanel.ResumeLayout(false);
			GmaPanel.PerformLayout();
			PackButtonsPanel.ResumeLayout(false);
			PostPackPanel.ResumeLayout(false);
			ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			Load += new System.EventHandler(PackUserControl_Load);
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