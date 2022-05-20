using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
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
			this.components = new System.ComponentModel.Container();
			this.ItemIdTextBox = new Crowbar.TextBoxEx();
			this.DownloadButton = new System.Windows.Forms.Button();
			this.LogTextBox = new Crowbar.RichTextBoxEx();
			this.ItemIdOrLinkLabel = new System.Windows.Forms.Label();
			this.OuputToLabel = new System.Windows.Forms.Label();
			this.OutputPathComboBox = new System.Windows.Forms.ComboBox();
			this.OutputPathTextBox = new Crowbar.TextBoxEx();
			this.GotoOutputPathButton = new System.Windows.Forms.Button();
			this.BrowseForOutputPathButton = new System.Windows.Forms.Button();
			this.OptionsGroupBox = new Crowbar.GroupBoxEx();
			this.OptionsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			this.UseIdCheckBox = new Crowbar.CheckBoxEx();
			this.PrependTitleCheckBox = new Crowbar.CheckBoxEx();
			this.AppendDateTimeCheckBox = new Crowbar.CheckBoxEx();
			this.ReplaceSpacesWithUnderscoresCheckBox = new Crowbar.CheckBoxEx();
			this.OptionsUseDefaultsButton = new System.Windows.Forms.Button();
			this.ConvertToExpectedFileOrFolderCheckBox = new Crowbar.CheckBoxEx();
			this.ExampleOutputFileNameLabel = new System.Windows.Forms.Label();
			this.ExampleOutputFileNameTextBox = new Crowbar.TextBoxEx();
			this.CancelDownloadButton = new System.Windows.Forms.Button();
			this.DownloadProgressBar = new Crowbar.ProgressBarEx();
			this.OpenWorkshopPageButton = new System.Windows.Forms.Button();
			this.DocumentsOutputPathTextBox = new Crowbar.TextBoxEx();
			this.DownloadedItemTextBox = new Crowbar.TextBoxEx();
			this.DownloadedLabel = new System.Windows.Forms.Label();
			this.GotoDownloadedItemButton = new System.Windows.Forms.Button();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.DownloadUserControlFillPanel = new System.Windows.Forms.Panel();
			this.Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			this.DownloadButtonsPanel = new System.Windows.Forms.Panel();
			this.PostDownloadPanel = new System.Windows.Forms.Panel();
			this.UseInUnpackButton = new System.Windows.Forms.Button();
			this.Timer1 = new System.Windows.Forms.Timer(this.components);
			this.OptionsGroupBox.SuspendLayout();
			this.OptionsGroupBoxFillPanel.SuspendLayout();
			this.DownloadUserControlFillPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).BeginInit();
			this.Options_LogSplitContainer.Panel1.SuspendLayout();
			this.Options_LogSplitContainer.Panel2.SuspendLayout();
			this.Options_LogSplitContainer.SuspendLayout();
			this.DownloadButtonsPanel.SuspendLayout();
			this.PostDownloadPanel.SuspendLayout();
			this.SuspendLayout();
			//
			//ItemIdTextBox
			//
			this.ItemIdTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemIdTextBox.CueBannerText = "";
			this.ItemIdTextBox.Location = new System.Drawing.Point(87, 3);
			this.ItemIdTextBox.Name = "ItemIdTextBox";
			this.ItemIdTextBox.Size = new System.Drawing.Size(616, 22);
			this.ItemIdTextBox.TabIndex = 1;
			//
			//DownloadButton
			//
			this.DownloadButton.Location = new System.Drawing.Point(0, 0);
			this.DownloadButton.Name = "DownloadButton";
			this.DownloadButton.Size = new System.Drawing.Size(120, 23);
			this.DownloadButton.TabIndex = 10;
			this.DownloadButton.Text = "Download";
			this.DownloadButton.UseVisualStyleBackColor = true;
			//
			//LogTextBox
			//
			this.LogTextBox.CueBannerText = "";
			this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LogTextBox.HideSelection = false;
			this.LogTextBox.Location = new System.Drawing.Point(0, 26);
			this.LogTextBox.Name = "LogTextBox";
			this.LogTextBox.ReadOnly = true;
			this.LogTextBox.Size = new System.Drawing.Size(770, 226);
			this.LogTextBox.TabIndex = 13;
			this.LogTextBox.Text = "";
			//
			//ItemIdOrLinkLabel
			//
			this.ItemIdOrLinkLabel.AutoSize = true;
			this.ItemIdOrLinkLabel.Location = new System.Drawing.Point(3, 8);
			this.ItemIdOrLinkLabel.Name = "ItemIdOrLinkLabel";
			this.ItemIdOrLinkLabel.Size = new System.Drawing.Size(82, 13);
			this.ItemIdOrLinkLabel.TabIndex = 0;
			this.ItemIdOrLinkLabel.Text = "Item ID or link:";
			//
			//OuputToLabel
			//
			this.OuputToLabel.AutoSize = true;
			this.OuputToLabel.Location = new System.Drawing.Point(3, 37);
			this.OuputToLabel.Name = "OuputToLabel";
			this.OuputToLabel.Size = new System.Drawing.Size(62, 13);
			this.OuputToLabel.TabIndex = 3;
			this.OuputToLabel.Text = "Output to:";
			//
			//OutputPathComboBox
			//
			this.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OutputPathComboBox.FormattingEnabled = true;
			this.OutputPathComboBox.Location = new System.Drawing.Point(87, 33);
			this.OutputPathComboBox.Name = "OutputPathComboBox";
			this.OutputPathComboBox.Size = new System.Drawing.Size(140, 21);
			this.OutputPathComboBox.TabIndex = 4;
			//
			//OutputPathTextBox
			//
			this.OutputPathTextBox.AllowDrop = true;
			this.OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputPathTextBox.CueBannerText = "";
			this.OutputPathTextBox.Location = new System.Drawing.Point(233, 32);
			this.OutputPathTextBox.Name = "OutputPathTextBox";
			this.OutputPathTextBox.Size = new System.Drawing.Size(421, 22);
			this.OutputPathTextBox.TabIndex = 5;
			//
			//GotoOutputPathButton
			//
			this.GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			this.GotoOutputPathButton.Name = "GotoOutputPathButton";
			this.GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			this.GotoOutputPathButton.TabIndex = 8;
			this.GotoOutputPathButton.Text = "Goto";
			this.GotoOutputPathButton.UseVisualStyleBackColor = true;
			//
			//BrowseForOutputPathButton
			//
			this.BrowseForOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForOutputPathButton.Location = new System.Drawing.Point(660, 32);
			this.BrowseForOutputPathButton.Name = "BrowseForOutputPathButton";
			this.BrowseForOutputPathButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForOutputPathButton.TabIndex = 7;
			this.BrowseForOutputPathButton.Text = "Browse...";
			this.BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OptionsGroupBox
			//
			this.OptionsGroupBox.Controls.Add(this.OptionsGroupBoxFillPanel);
			this.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsGroupBox.IsReadOnly = false;
			this.OptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.OptionsGroupBox.Name = "OptionsGroupBox";
			this.OptionsGroupBox.SelectedValue = null;
			this.OptionsGroupBox.Size = new System.Drawing.Size(770, 193);
			this.OptionsGroupBox.TabIndex = 9;
			this.OptionsGroupBox.TabStop = false;
			this.OptionsGroupBox.Text = "Output File Name Options";
			//
			//OptionsGroupBoxFillPanel
			//
			this.OptionsGroupBoxFillPanel.AutoScroll = true;
			this.OptionsGroupBoxFillPanel.Controls.Add(this.UseIdCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.PrependTitleCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.AppendDateTimeCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.ReplaceSpacesWithUnderscoresCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.OptionsUseDefaultsButton);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.ConvertToExpectedFileOrFolderCheckBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.ExampleOutputFileNameLabel);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.ExampleOutputFileNameTextBox);
			this.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			this.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel";
			this.OptionsGroupBoxFillPanel.Size = new System.Drawing.Size(764, 172);
			this.OptionsGroupBoxFillPanel.TabIndex = 8;
			//
			//UseIdCheckBox
			//
			this.UseIdCheckBox.AutoSize = true;
			this.UseIdCheckBox.IsReadOnly = false;
			this.UseIdCheckBox.Location = new System.Drawing.Point(3, 3);
			this.UseIdCheckBox.Name = "UseIdCheckBox";
			this.UseIdCheckBox.Size = new System.Drawing.Size(201, 17);
			this.UseIdCheckBox.TabIndex = 0;
			this.UseIdCheckBox.Text = "Use item ID instead of given name";
			this.UseIdCheckBox.UseVisualStyleBackColor = true;
			//
			//PrependTitleCheckBox
			//
			this.PrependTitleCheckBox.AutoSize = true;
			this.PrependTitleCheckBox.IsReadOnly = false;
			this.PrependTitleCheckBox.Location = new System.Drawing.Point(3, 26);
			this.PrependTitleCheckBox.Name = "PrependTitleCheckBox";
			this.PrependTitleCheckBox.Size = new System.Drawing.Size(117, 17);
			this.PrependTitleCheckBox.TabIndex = 1;
			this.PrependTitleCheckBox.Text = "Prepend item title";
			this.PrependTitleCheckBox.UseVisualStyleBackColor = true;
			//
			//AppendDateTimeCheckBox
			//
			this.AppendDateTimeCheckBox.AutoSize = true;
			this.AppendDateTimeCheckBox.IsReadOnly = false;
			this.AppendDateTimeCheckBox.Location = new System.Drawing.Point(3, 49);
			this.AppendDateTimeCheckBox.Name = "AppendDateTimeCheckBox";
			this.AppendDateTimeCheckBox.Size = new System.Drawing.Size(204, 17);
			this.AppendDateTimeCheckBox.TabIndex = 2;
			this.AppendDateTimeCheckBox.Text = "Append the item update date-time";
			this.AppendDateTimeCheckBox.UseVisualStyleBackColor = true;
			//
			//ReplaceSpacesWithUnderscoresCheckBox
			//
			this.ReplaceSpacesWithUnderscoresCheckBox.AutoSize = true;
			this.ReplaceSpacesWithUnderscoresCheckBox.IsReadOnly = false;
			this.ReplaceSpacesWithUnderscoresCheckBox.Location = new System.Drawing.Point(3, 72);
			this.ReplaceSpacesWithUnderscoresCheckBox.Name = "ReplaceSpacesWithUnderscoresCheckBox";
			this.ReplaceSpacesWithUnderscoresCheckBox.Size = new System.Drawing.Size(195, 17);
			this.ReplaceSpacesWithUnderscoresCheckBox.TabIndex = 3;
			this.ReplaceSpacesWithUnderscoresCheckBox.Text = "Replace spaces with underscores";
			this.ReplaceSpacesWithUnderscoresCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsUseDefaultsButton
			//
			this.OptionsUseDefaultsButton.Location = new System.Drawing.Point(3, 95);
			this.OptionsUseDefaultsButton.Name = "OptionsUseDefaultsButton";
			this.OptionsUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			this.OptionsUseDefaultsButton.TabIndex = 4;
			this.OptionsUseDefaultsButton.Text = "Use Defaults";
			this.OptionsUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//ConvertToExpectedFileOrFolderCheckBox
			//
			this.ConvertToExpectedFileOrFolderCheckBox.AutoSize = true;
			this.ConvertToExpectedFileOrFolderCheckBox.IsReadOnly = false;
			this.ConvertToExpectedFileOrFolderCheckBox.Location = new System.Drawing.Point(230, 3);
			this.ConvertToExpectedFileOrFolderCheckBox.Name = "ConvertToExpectedFileOrFolderCheckBox";
			this.ConvertToExpectedFileOrFolderCheckBox.Size = new System.Drawing.Size(196, 17);
			this.ConvertToExpectedFileOrFolderCheckBox.TabIndex = 7;
			this.ConvertToExpectedFileOrFolderCheckBox.Text = "Convert to expected file or folder";
			this.ToolTip1.SetToolTip(this.ConvertToExpectedFileOrFolderCheckBox, "Example: Garry's Mod uses compressed GMA (LZMA) instead of GMA.");
			this.ConvertToExpectedFileOrFolderCheckBox.UseVisualStyleBackColor = true;
			//
			//ExampleOutputFileNameLabel
			//
			this.ExampleOutputFileNameLabel.AutoSize = true;
			this.ExampleOutputFileNameLabel.Location = new System.Drawing.Point(3, 131);
			this.ExampleOutputFileNameLabel.Name = "ExampleOutputFileNameLabel";
			this.ExampleOutputFileNameLabel.Size = new System.Drawing.Size(141, 13);
			this.ExampleOutputFileNameLabel.TabIndex = 5;
			this.ExampleOutputFileNameLabel.Text = "Example output file name:";
			//
			//ExampleOutputFileNameTextBox
			//
			this.ExampleOutputFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ExampleOutputFileNameTextBox.CueBannerText = "";
			this.ExampleOutputFileNameTextBox.Location = new System.Drawing.Point(3, 147);
			this.ExampleOutputFileNameTextBox.Name = "ExampleOutputFileNameTextBox";
			this.ExampleOutputFileNameTextBox.ReadOnly = true;
			this.ExampleOutputFileNameTextBox.Size = new System.Drawing.Size(758, 22);
			this.ExampleOutputFileNameTextBox.TabIndex = 6;
			//
			//CancelDownloadButton
			//
			this.CancelDownloadButton.Enabled = false;
			this.CancelDownloadButton.Location = new System.Drawing.Point(126, 0);
			this.CancelDownloadButton.Name = "CancelDownloadButton";
			this.CancelDownloadButton.Size = new System.Drawing.Size(120, 23);
			this.CancelDownloadButton.TabIndex = 11;
			this.CancelDownloadButton.Text = "Cancel Download";
			this.CancelDownloadButton.UseVisualStyleBackColor = true;
			//
			//DownloadProgressBar
			//
			this.DownloadProgressBar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DownloadProgressBar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.DownloadProgressBar.Location = new System.Drawing.Point(252, 0);
			this.DownloadProgressBar.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.DownloadProgressBar.Name = "DownloadProgressBar";
			this.DownloadProgressBar.Size = new System.Drawing.Size(515, 23);
			this.DownloadProgressBar.TabIndex = 12;
			//
			//OpenWorkshopPageButton
			//
			this.OpenWorkshopPageButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.OpenWorkshopPageButton.Location = new System.Drawing.Point(709, 3);
			this.OpenWorkshopPageButton.Name = "OpenWorkshopPageButton";
			this.OpenWorkshopPageButton.Size = new System.Drawing.Size(64, 23);
			this.OpenWorkshopPageButton.TabIndex = 2;
			this.OpenWorkshopPageButton.Text = "Open";
			this.OpenWorkshopPageButton.UseVisualStyleBackColor = true;
			//
			//DocumentsOutputPathTextBox
			//
			this.DocumentsOutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DocumentsOutputPathTextBox.CueBannerText = "";
			this.DocumentsOutputPathTextBox.Location = new System.Drawing.Point(233, 32);
			this.DocumentsOutputPathTextBox.Name = "DocumentsOutputPathTextBox";
			this.DocumentsOutputPathTextBox.ReadOnly = true;
			this.DocumentsOutputPathTextBox.Size = new System.Drawing.Size(421, 22);
			this.DocumentsOutputPathTextBox.TabIndex = 6;
			//
			//DownloadedItemTextBox
			//
			this.DownloadedItemTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DownloadedItemTextBox.CueBannerText = "";
			this.DownloadedItemTextBox.Location = new System.Drawing.Point(83, 3);
			this.DownloadedItemTextBox.Name = "DownloadedItemTextBox";
			this.DownloadedItemTextBox.ReadOnly = true;
			this.DownloadedItemTextBox.Size = new System.Drawing.Size(542, 22);
			this.DownloadedItemTextBox.TabIndex = 15;
			//
			//DownloadedLabel
			//
			this.DownloadedLabel.AutoSize = true;
			this.DownloadedLabel.Location = new System.Drawing.Point(0, 8);
			this.DownloadedLabel.Name = "DownloadedLabel";
			this.DownloadedLabel.Size = new System.Drawing.Size(77, 13);
			this.DownloadedLabel.TabIndex = 14;
			this.DownloadedLabel.Text = "Downloaded:";
			//
			//GotoDownloadedItemButton
			//
			this.GotoDownloadedItemButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoDownloadedItemButton.Location = new System.Drawing.Point(727, 3);
			this.GotoDownloadedItemButton.Name = "GotoDownloadedItemButton";
			this.GotoDownloadedItemButton.Size = new System.Drawing.Size(43, 23);
			this.GotoDownloadedItemButton.TabIndex = 16;
			this.GotoDownloadedItemButton.Text = "Goto";
			this.GotoDownloadedItemButton.UseVisualStyleBackColor = true;
			//
			//DownloadUserControlFillPanel
			//
			this.DownloadUserControlFillPanel.Controls.Add(this.ItemIdOrLinkLabel);
			this.DownloadUserControlFillPanel.Controls.Add(this.ItemIdTextBox);
			this.DownloadUserControlFillPanel.Controls.Add(this.OpenWorkshopPageButton);
			this.DownloadUserControlFillPanel.Controls.Add(this.OuputToLabel);
			this.DownloadUserControlFillPanel.Controls.Add(this.OutputPathComboBox);
			this.DownloadUserControlFillPanel.Controls.Add(this.OutputPathTextBox);
			this.DownloadUserControlFillPanel.Controls.Add(this.DocumentsOutputPathTextBox);
			this.DownloadUserControlFillPanel.Controls.Add(this.BrowseForOutputPathButton);
			this.DownloadUserControlFillPanel.Controls.Add(this.GotoOutputPathButton);
			this.DownloadUserControlFillPanel.Controls.Add(this.Options_LogSplitContainer);
			this.DownloadUserControlFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DownloadUserControlFillPanel.Location = new System.Drawing.Point(0, 0);
			this.DownloadUserControlFillPanel.Name = "DownloadUserControlFillPanel";
			this.DownloadUserControlFillPanel.Size = new System.Drawing.Size(776, 536);
			this.DownloadUserControlFillPanel.TabIndex = 17;
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
			//
			//Options_LogSplitContainer.Panel2
			//
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.LogTextBox);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.DownloadButtonsPanel);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.PostDownloadPanel);
			this.Options_LogSplitContainer.Size = new System.Drawing.Size(770, 475);
			this.Options_LogSplitContainer.SplitterDistance = 193;
			this.Options_LogSplitContainer.TabIndex = 17;
			//
			//DownloadButtonsPanel
			//
			this.DownloadButtonsPanel.Controls.Add(this.DownloadButton);
			this.DownloadButtonsPanel.Controls.Add(this.CancelDownloadButton);
			this.DownloadButtonsPanel.Controls.Add(this.DownloadProgressBar);
			this.DownloadButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.DownloadButtonsPanel.Location = new System.Drawing.Point(0, 0);
			this.DownloadButtonsPanel.Name = "DownloadButtonsPanel";
			this.DownloadButtonsPanel.Size = new System.Drawing.Size(770, 26);
			this.DownloadButtonsPanel.TabIndex = 19;
			//
			//PostDownloadPanel
			//
			this.PostDownloadPanel.Controls.Add(this.UseInUnpackButton);
			this.PostDownloadPanel.Controls.Add(this.DownloadedLabel);
			this.PostDownloadPanel.Controls.Add(this.DownloadedItemTextBox);
			this.PostDownloadPanel.Controls.Add(this.GotoDownloadedItemButton);
			this.PostDownloadPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PostDownloadPanel.Location = new System.Drawing.Point(0, 252);
			this.PostDownloadPanel.Name = "PostDownloadPanel";
			this.PostDownloadPanel.Size = new System.Drawing.Size(770, 26);
			this.PostDownloadPanel.TabIndex = 18;
			//
			//UseInUnpackButton
			//
			this.UseInUnpackButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseInUnpackButton.Location = new System.Drawing.Point(631, 3);
			this.UseInUnpackButton.Name = "UseInUnpackButton";
			this.UseInUnpackButton.Size = new System.Drawing.Size(90, 23);
			this.UseInUnpackButton.TabIndex = 17;
			this.UseInUnpackButton.Text = "Use In Unpack";
			this.UseInUnpackButton.UseVisualStyleBackColor = true;
			//
			//Timer1
			//
			this.Timer1.Interval = 1000;
			//
			//DownloadUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.DownloadUserControlFillPanel);
			this.Name = "DownloadUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.OptionsGroupBox.ResumeLayout(false);
			this.OptionsGroupBoxFillPanel.ResumeLayout(false);
			this.OptionsGroupBoxFillPanel.PerformLayout();
			this.DownloadUserControlFillPanel.ResumeLayout(false);
			this.DownloadUserControlFillPanel.PerformLayout();
			this.Options_LogSplitContainer.Panel1.ResumeLayout(false);
			this.Options_LogSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).EndInit();
			this.Options_LogSplitContainer.ResumeLayout(false);
			this.DownloadButtonsPanel.ResumeLayout(false);
			this.PostDownloadPanel.ResumeLayout(false);
			this.PostDownloadPanel.PerformLayout();
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
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