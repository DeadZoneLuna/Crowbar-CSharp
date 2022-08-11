using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public partial class DownloadUserControl : BaseUserControl
	{
		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			ItemIdTextBox = new Crowbar.TextBoxEx();
			DownloadButton = new System.Windows.Forms.Button();
			LogTextBox = new Crowbar.RichTextBoxEx();
			ItemIdOrLinkLabel = new System.Windows.Forms.Label();
			OuputToLabel = new System.Windows.Forms.Label();
			OutputPathComboBox = new System.Windows.Forms.ComboBox();
			OutputPathTextBox = new Crowbar.TextBoxEx();
			GotoOutputPathButton = new System.Windows.Forms.Button();
			BrowseForOutputPathButton = new System.Windows.Forms.Button();
			OptionsGroupBox = new Crowbar.GroupBoxEx();
			OptionsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			UseIdCheckBox = new Crowbar.CheckBoxEx();
			PrependTitleCheckBox = new Crowbar.CheckBoxEx();
			AppendDateTimeCheckBox = new Crowbar.CheckBoxEx();
			ReplaceSpacesWithUnderscoresCheckBox = new Crowbar.CheckBoxEx();
			OptionsUseDefaultsButton = new System.Windows.Forms.Button();
			ConvertToExpectedFileOrFolderCheckBox = new Crowbar.CheckBoxEx();
			ExampleOutputFileNameLabel = new System.Windows.Forms.Label();
			ExampleOutputFileNameTextBox = new Crowbar.TextBoxEx();
			CancelDownloadButton = new System.Windows.Forms.Button();
			DownloadProgressBar = new Crowbar.ProgressBarEx();
			OpenWorkshopPageButton = new System.Windows.Forms.Button();
			DocumentsOutputPathTextBox = new Crowbar.TextBoxEx();
			DownloadedItemTextBox = new Crowbar.TextBoxEx();
			DownloadedLabel = new System.Windows.Forms.Label();
			GotoDownloadedItemButton = new System.Windows.Forms.Button();
			ToolTip1 = new System.Windows.Forms.ToolTip(components);
			DownloadUserControlFillPanel = new System.Windows.Forms.Panel();
			Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			DownloadButtonsPanel = new System.Windows.Forms.Panel();
			PostDownloadPanel = new System.Windows.Forms.Panel();
			UseInUnpackButton = new System.Windows.Forms.Button();
			Timer1 = new System.Windows.Forms.Timer(components);
			OptionsGroupBox.SuspendLayout();
			OptionsGroupBoxFillPanel.SuspendLayout();
			DownloadUserControlFillPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).BeginInit();
			Options_LogSplitContainer.Panel1.SuspendLayout();
			Options_LogSplitContainer.Panel2.SuspendLayout();
			Options_LogSplitContainer.SuspendLayout();
			DownloadButtonsPanel.SuspendLayout();
			PostDownloadPanel.SuspendLayout();
			SuspendLayout();
			//
			//ItemIdTextBox
			//
			ItemIdTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemIdTextBox.CueBannerText = "";
			ItemIdTextBox.Location = new System.Drawing.Point(87, 3);
			ItemIdTextBox.Name = "ItemIdTextBox";
			ItemIdTextBox.Size = new System.Drawing.Size(616, 22);
			ItemIdTextBox.TabIndex = 1;
			//
			//DownloadButton
			//
			DownloadButton.Location = new System.Drawing.Point(0, 0);
			DownloadButton.Name = "DownloadButton";
			DownloadButton.Size = new System.Drawing.Size(120, 23);
			DownloadButton.TabIndex = 10;
			DownloadButton.Text = "Download";
			DownloadButton.UseVisualStyleBackColor = true;
			//
			//LogTextBox
			//
			LogTextBox.CueBannerText = "";
			LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			LogTextBox.HideSelection = false;
			LogTextBox.Location = new System.Drawing.Point(0, 26);
			LogTextBox.Name = "LogTextBox";
			LogTextBox.ReadOnly = true;
			LogTextBox.Size = new System.Drawing.Size(770, 226);
			LogTextBox.TabIndex = 13;
			LogTextBox.Text = "";
			//
			//ItemIdOrLinkLabel
			//
			ItemIdOrLinkLabel.AutoSize = true;
			ItemIdOrLinkLabel.Location = new System.Drawing.Point(3, 8);
			ItemIdOrLinkLabel.Name = "ItemIdOrLinkLabel";
			ItemIdOrLinkLabel.Size = new System.Drawing.Size(82, 13);
			ItemIdOrLinkLabel.TabIndex = 0;
			ItemIdOrLinkLabel.Text = "Item ID or link:";
			//
			//OuputToLabel
			//
			OuputToLabel.AutoSize = true;
			OuputToLabel.Location = new System.Drawing.Point(3, 37);
			OuputToLabel.Name = "OuputToLabel";
			OuputToLabel.Size = new System.Drawing.Size(62, 13);
			OuputToLabel.TabIndex = 3;
			OuputToLabel.Text = "Output to:";
			//
			//OutputPathComboBox
			//
			OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			OutputPathComboBox.FormattingEnabled = true;
			OutputPathComboBox.Location = new System.Drawing.Point(87, 33);
			OutputPathComboBox.Name = "OutputPathComboBox";
			OutputPathComboBox.Size = new System.Drawing.Size(140, 21);
			OutputPathComboBox.TabIndex = 4;
			//
			//OutputPathTextBox
			//
			OutputPathTextBox.AllowDrop = true;
			OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputPathTextBox.CueBannerText = "";
			OutputPathTextBox.Location = new System.Drawing.Point(233, 32);
			OutputPathTextBox.Name = "OutputPathTextBox";
			OutputPathTextBox.Size = new System.Drawing.Size(421, 22);
			OutputPathTextBox.TabIndex = 5;
			//
			//GotoOutputPathButton
			//
			GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			GotoOutputPathButton.Name = "GotoOutputPathButton";
			GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			GotoOutputPathButton.TabIndex = 8;
			GotoOutputPathButton.Text = "Goto";
			GotoOutputPathButton.UseVisualStyleBackColor = true;
			//
			//BrowseForOutputPathButton
			//
			BrowseForOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForOutputPathButton.Location = new System.Drawing.Point(660, 32);
			BrowseForOutputPathButton.Name = "BrowseForOutputPathButton";
			BrowseForOutputPathButton.Size = new System.Drawing.Size(64, 23);
			BrowseForOutputPathButton.TabIndex = 7;
			BrowseForOutputPathButton.Text = "Browse...";
			BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OptionsGroupBox
			//
			OptionsGroupBox.Controls.Add(OptionsGroupBoxFillPanel);
			OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsGroupBox.IsReadOnly = false;
			OptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			OptionsGroupBox.Name = "OptionsGroupBox";
			OptionsGroupBox.SelectedValue = null;
			OptionsGroupBox.Size = new System.Drawing.Size(770, 193);
			OptionsGroupBox.TabIndex = 9;
			OptionsGroupBox.TabStop = false;
			OptionsGroupBox.Text = "Output File Name Options";
			//
			//OptionsGroupBoxFillPanel
			//
			OptionsGroupBoxFillPanel.AutoScroll = true;
			OptionsGroupBoxFillPanel.Controls.Add(UseIdCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(PrependTitleCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(AppendDateTimeCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(ReplaceSpacesWithUnderscoresCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(OptionsUseDefaultsButton);
			OptionsGroupBoxFillPanel.Controls.Add(ConvertToExpectedFileOrFolderCheckBox);
			OptionsGroupBoxFillPanel.Controls.Add(ExampleOutputFileNameLabel);
			OptionsGroupBoxFillPanel.Controls.Add(ExampleOutputFileNameTextBox);
			OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel";
			OptionsGroupBoxFillPanel.Size = new System.Drawing.Size(764, 172);
			OptionsGroupBoxFillPanel.TabIndex = 8;
			//
			//UseIdCheckBox
			//
			UseIdCheckBox.AutoSize = true;
			UseIdCheckBox.IsReadOnly = false;
			UseIdCheckBox.Location = new System.Drawing.Point(3, 3);
			UseIdCheckBox.Name = "UseIdCheckBox";
			UseIdCheckBox.Size = new System.Drawing.Size(201, 17);
			UseIdCheckBox.TabIndex = 0;
			UseIdCheckBox.Text = "Use item ID instead of given name";
			UseIdCheckBox.UseVisualStyleBackColor = true;
			//
			//PrependTitleCheckBox
			//
			PrependTitleCheckBox.AutoSize = true;
			PrependTitleCheckBox.IsReadOnly = false;
			PrependTitleCheckBox.Location = new System.Drawing.Point(3, 26);
			PrependTitleCheckBox.Name = "PrependTitleCheckBox";
			PrependTitleCheckBox.Size = new System.Drawing.Size(117, 17);
			PrependTitleCheckBox.TabIndex = 1;
			PrependTitleCheckBox.Text = "Prepend item title";
			PrependTitleCheckBox.UseVisualStyleBackColor = true;
			//
			//AppendDateTimeCheckBox
			//
			AppendDateTimeCheckBox.AutoSize = true;
			AppendDateTimeCheckBox.IsReadOnly = false;
			AppendDateTimeCheckBox.Location = new System.Drawing.Point(3, 49);
			AppendDateTimeCheckBox.Name = "AppendDateTimeCheckBox";
			AppendDateTimeCheckBox.Size = new System.Drawing.Size(204, 17);
			AppendDateTimeCheckBox.TabIndex = 2;
			AppendDateTimeCheckBox.Text = "Append the item update date-time";
			AppendDateTimeCheckBox.UseVisualStyleBackColor = true;
			//
			//ReplaceSpacesWithUnderscoresCheckBox
			//
			ReplaceSpacesWithUnderscoresCheckBox.AutoSize = true;
			ReplaceSpacesWithUnderscoresCheckBox.IsReadOnly = false;
			ReplaceSpacesWithUnderscoresCheckBox.Location = new System.Drawing.Point(3, 72);
			ReplaceSpacesWithUnderscoresCheckBox.Name = "ReplaceSpacesWithUnderscoresCheckBox";
			ReplaceSpacesWithUnderscoresCheckBox.Size = new System.Drawing.Size(195, 17);
			ReplaceSpacesWithUnderscoresCheckBox.TabIndex = 3;
			ReplaceSpacesWithUnderscoresCheckBox.Text = "Replace spaces with underscores";
			ReplaceSpacesWithUnderscoresCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsUseDefaultsButton
			//
			OptionsUseDefaultsButton.Location = new System.Drawing.Point(3, 95);
			OptionsUseDefaultsButton.Name = "OptionsUseDefaultsButton";
			OptionsUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			OptionsUseDefaultsButton.TabIndex = 4;
			OptionsUseDefaultsButton.Text = "Use Defaults";
			OptionsUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//ConvertToExpectedFileOrFolderCheckBox
			//
			ConvertToExpectedFileOrFolderCheckBox.AutoSize = true;
			ConvertToExpectedFileOrFolderCheckBox.IsReadOnly = false;
			ConvertToExpectedFileOrFolderCheckBox.Location = new System.Drawing.Point(230, 3);
			ConvertToExpectedFileOrFolderCheckBox.Name = "ConvertToExpectedFileOrFolderCheckBox";
			ConvertToExpectedFileOrFolderCheckBox.Size = new System.Drawing.Size(196, 17);
			ConvertToExpectedFileOrFolderCheckBox.TabIndex = 7;
			ConvertToExpectedFileOrFolderCheckBox.Text = "Convert to expected file or folder";
			ToolTip1.SetToolTip(ConvertToExpectedFileOrFolderCheckBox, "Example: Garry's Mod uses compressed GMA (LZMA) instead of GMA.");
			ConvertToExpectedFileOrFolderCheckBox.UseVisualStyleBackColor = true;
			//
			//ExampleOutputFileNameLabel
			//
			ExampleOutputFileNameLabel.AutoSize = true;
			ExampleOutputFileNameLabel.Location = new System.Drawing.Point(3, 131);
			ExampleOutputFileNameLabel.Name = "ExampleOutputFileNameLabel";
			ExampleOutputFileNameLabel.Size = new System.Drawing.Size(141, 13);
			ExampleOutputFileNameLabel.TabIndex = 5;
			ExampleOutputFileNameLabel.Text = "Example output file name:";
			//
			//ExampleOutputFileNameTextBox
			//
			ExampleOutputFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ExampleOutputFileNameTextBox.CueBannerText = "";
			ExampleOutputFileNameTextBox.Location = new System.Drawing.Point(3, 147);
			ExampleOutputFileNameTextBox.Name = "ExampleOutputFileNameTextBox";
			ExampleOutputFileNameTextBox.ReadOnly = true;
			ExampleOutputFileNameTextBox.Size = new System.Drawing.Size(758, 22);
			ExampleOutputFileNameTextBox.TabIndex = 6;
			//
			//CancelDownloadButton
			//
			CancelDownloadButton.Enabled = false;
			CancelDownloadButton.Location = new System.Drawing.Point(126, 0);
			CancelDownloadButton.Name = "CancelDownloadButton";
			CancelDownloadButton.Size = new System.Drawing.Size(120, 23);
			CancelDownloadButton.TabIndex = 11;
			CancelDownloadButton.Text = "Cancel Download";
			CancelDownloadButton.UseVisualStyleBackColor = true;
			//
			//DownloadProgressBar
			//
			DownloadProgressBar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			DownloadProgressBar.ForeColor = System.Drawing.SystemColors.ControlText;
			DownloadProgressBar.Location = new System.Drawing.Point(252, 0);
			DownloadProgressBar.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			DownloadProgressBar.Name = "DownloadProgressBar";
			DownloadProgressBar.Size = new System.Drawing.Size(515, 23);
			DownloadProgressBar.TabIndex = 12;
			//
			//OpenWorkshopPageButton
			//
			OpenWorkshopPageButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			OpenWorkshopPageButton.Location = new System.Drawing.Point(709, 3);
			OpenWorkshopPageButton.Name = "OpenWorkshopPageButton";
			OpenWorkshopPageButton.Size = new System.Drawing.Size(64, 23);
			OpenWorkshopPageButton.TabIndex = 2;
			OpenWorkshopPageButton.Text = "Open";
			OpenWorkshopPageButton.UseVisualStyleBackColor = true;
			//
			//DocumentsOutputPathTextBox
			//
			DocumentsOutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			DocumentsOutputPathTextBox.CueBannerText = "";
			DocumentsOutputPathTextBox.Location = new System.Drawing.Point(233, 32);
			DocumentsOutputPathTextBox.Name = "DocumentsOutputPathTextBox";
			DocumentsOutputPathTextBox.ReadOnly = true;
			DocumentsOutputPathTextBox.Size = new System.Drawing.Size(421, 22);
			DocumentsOutputPathTextBox.TabIndex = 6;
			//
			//DownloadedItemTextBox
			//
			DownloadedItemTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			DownloadedItemTextBox.CueBannerText = "";
			DownloadedItemTextBox.Location = new System.Drawing.Point(83, 3);
			DownloadedItemTextBox.Name = "DownloadedItemTextBox";
			DownloadedItemTextBox.ReadOnly = true;
			DownloadedItemTextBox.Size = new System.Drawing.Size(542, 22);
			DownloadedItemTextBox.TabIndex = 15;
			//
			//DownloadedLabel
			//
			DownloadedLabel.AutoSize = true;
			DownloadedLabel.Location = new System.Drawing.Point(0, 8);
			DownloadedLabel.Name = "DownloadedLabel";
			DownloadedLabel.Size = new System.Drawing.Size(77, 13);
			DownloadedLabel.TabIndex = 14;
			DownloadedLabel.Text = "Downloaded:";
			//
			//GotoDownloadedItemButton
			//
			GotoDownloadedItemButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoDownloadedItemButton.Location = new System.Drawing.Point(727, 3);
			GotoDownloadedItemButton.Name = "GotoDownloadedItemButton";
			GotoDownloadedItemButton.Size = new System.Drawing.Size(43, 23);
			GotoDownloadedItemButton.TabIndex = 16;
			GotoDownloadedItemButton.Text = "Goto";
			GotoDownloadedItemButton.UseVisualStyleBackColor = true;
			//
			//DownloadUserControlFillPanel
			//
			DownloadUserControlFillPanel.Controls.Add(ItemIdOrLinkLabel);
			DownloadUserControlFillPanel.Controls.Add(ItemIdTextBox);
			DownloadUserControlFillPanel.Controls.Add(OpenWorkshopPageButton);
			DownloadUserControlFillPanel.Controls.Add(OuputToLabel);
			DownloadUserControlFillPanel.Controls.Add(OutputPathComboBox);
			DownloadUserControlFillPanel.Controls.Add(OutputPathTextBox);
			DownloadUserControlFillPanel.Controls.Add(DocumentsOutputPathTextBox);
			DownloadUserControlFillPanel.Controls.Add(BrowseForOutputPathButton);
			DownloadUserControlFillPanel.Controls.Add(GotoOutputPathButton);
			DownloadUserControlFillPanel.Controls.Add(Options_LogSplitContainer);
			DownloadUserControlFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			DownloadUserControlFillPanel.Location = new System.Drawing.Point(0, 0);
			DownloadUserControlFillPanel.Name = "DownloadUserControlFillPanel";
			DownloadUserControlFillPanel.Size = new System.Drawing.Size(776, 536);
			DownloadUserControlFillPanel.TabIndex = 17;
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
			//
			//Options_LogSplitContainer.Panel2
			//
			Options_LogSplitContainer.Panel2.Controls.Add(LogTextBox);
			Options_LogSplitContainer.Panel2.Controls.Add(DownloadButtonsPanel);
			Options_LogSplitContainer.Panel2.Controls.Add(PostDownloadPanel);
			Options_LogSplitContainer.Size = new System.Drawing.Size(770, 475);
			Options_LogSplitContainer.SplitterDistance = 193;
			Options_LogSplitContainer.TabIndex = 17;
			//
			//DownloadButtonsPanel
			//
			DownloadButtonsPanel.Controls.Add(DownloadButton);
			DownloadButtonsPanel.Controls.Add(CancelDownloadButton);
			DownloadButtonsPanel.Controls.Add(DownloadProgressBar);
			DownloadButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			DownloadButtonsPanel.Location = new System.Drawing.Point(0, 0);
			DownloadButtonsPanel.Name = "DownloadButtonsPanel";
			DownloadButtonsPanel.Size = new System.Drawing.Size(770, 26);
			DownloadButtonsPanel.TabIndex = 19;
			//
			//PostDownloadPanel
			//
			PostDownloadPanel.Controls.Add(UseInUnpackButton);
			PostDownloadPanel.Controls.Add(DownloadedLabel);
			PostDownloadPanel.Controls.Add(DownloadedItemTextBox);
			PostDownloadPanel.Controls.Add(GotoDownloadedItemButton);
			PostDownloadPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			PostDownloadPanel.Location = new System.Drawing.Point(0, 252);
			PostDownloadPanel.Name = "PostDownloadPanel";
			PostDownloadPanel.Size = new System.Drawing.Size(770, 26);
			PostDownloadPanel.TabIndex = 18;
			//
			//UseInUnpackButton
			//
			UseInUnpackButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseInUnpackButton.Location = new System.Drawing.Point(631, 3);
			UseInUnpackButton.Name = "UseInUnpackButton";
			UseInUnpackButton.Size = new System.Drawing.Size(90, 23);
			UseInUnpackButton.TabIndex = 17;
			UseInUnpackButton.Text = "Use In Unpack";
			UseInUnpackButton.UseVisualStyleBackColor = true;
			//
			//Timer1
			//
			Timer1.Interval = 1000;
			//
			//DownloadUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(DownloadUserControlFillPanel);
			Name = "DownloadUserControl";
			Size = new System.Drawing.Size(776, 536);
			OptionsGroupBox.ResumeLayout(false);
			OptionsGroupBoxFillPanel.ResumeLayout(false);
			OptionsGroupBoxFillPanel.PerformLayout();
			DownloadUserControlFillPanel.ResumeLayout(false);
			DownloadUserControlFillPanel.PerformLayout();
			Options_LogSplitContainer.Panel1.ResumeLayout(false);
			Options_LogSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).EndInit();
			Options_LogSplitContainer.ResumeLayout(false);
			DownloadButtonsPanel.ResumeLayout(false);
			PostDownloadPanel.ResumeLayout(false);
			PostDownloadPanel.PerformLayout();
			ResumeLayout(false);

			base.Load += new System.EventHandler(DownloadUserControl_Load);
			OpenWorkshopPageButton.Click += new System.EventHandler(OpenWorkshopPageButton_Click);
			OutputPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragDrop);
			OutputPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragEnter);
			OutputPathTextBox.Validated += new System.EventHandler(OutputPathTextBox_Validated);
			BrowseForOutputPathButton.Click += new System.EventHandler(BrowseForOutputPathButton_Click);
			GotoOutputPathButton.Click += new System.EventHandler(GotoOutputPathButton_Click);
			OptionsUseDefaultsButton.Click += new System.EventHandler(OptionsUseDefaultsButton_Click);
			DownloadButton.Click += new System.EventHandler(DownloadButton_Click);
			CancelDownloadButton.Click += new System.EventHandler(CancelDownloadButton_Click);
			UseInUnpackButton.Click += new System.EventHandler(UseInUnpackButton_Click);
			GotoDownloadedItemButton.Click += new System.EventHandler(GotoDownloadedItemButton_Click);
			Timer1.Tick += new System.EventHandler(Timer1_Tick);
		}

		internal TextBoxEx ItemIdTextBox;
		internal Button DownloadButton;
		internal RichTextBoxEx LogTextBox;
		internal Label ItemIdOrLinkLabel;
		internal Label OuputToLabel;
		internal ComboBox OutputPathComboBox;
		internal TextBoxEx OutputPathTextBox;
		internal Button GotoOutputPathButton;
		internal Button BrowseForOutputPathButton;
		internal GroupBoxEx OptionsGroupBox;
		internal Button CancelDownloadButton;
		internal Label ExampleOutputFileNameLabel;
		internal CheckBoxEx AppendDateTimeCheckBox;
		internal CheckBoxEx PrependTitleCheckBox;
		internal CheckBoxEx UseIdCheckBox;
		internal CheckBoxEx ReplaceSpacesWithUnderscoresCheckBox;
		internal Button OptionsUseDefaultsButton;
		internal ProgressBarEx DownloadProgressBar;
		internal Button OpenWorkshopPageButton;
		internal TextBoxEx DocumentsOutputPathTextBox;
		internal TextBoxEx DownloadedItemTextBox;
		internal Label DownloadedLabel;
		internal Button GotoDownloadedItemButton;
		internal TextBoxEx ExampleOutputFileNameTextBox;
		internal CheckBoxEx ConvertToExpectedFileOrFolderCheckBox;
		internal ToolTip ToolTip1;
		internal Panel DownloadUserControlFillPanel;
		internal Timer Timer1;
		internal SplitContainer Options_LogSplitContainer;
		internal Panel PostDownloadPanel;
		internal Panel DownloadButtonsPanel;
		internal Panel OptionsGroupBoxFillPanel;
		internal Button UseInUnpackButton;
	}

}