using System.ComponentModel;
using System.IO;
using System.Net;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public partial class UpdateUserControl : BaseUserControl
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
			UpdateUserControlFillPanel = new System.Windows.Forms.Panel();
			CheckForUpdateGroupBox = new System.Windows.Forms.GroupBox();
			Panel1 = new System.Windows.Forms.Panel();
			ChangelogTextBox = new Crowbar.RichTextBoxEx();
			CurrentVersionLabel = new System.Windows.Forms.Label();
			CheckForUpdateTopPanel = new System.Windows.Forms.Panel();
			CheckForUpdateButton = new System.Windows.Forms.Button();
			CheckForUpdateTextBox = new Crowbar.TextBoxEx();
			CheckForUpdateProgressBar = new Crowbar.ProgressBarEx();
			CancelCheckButton = new System.Windows.Forms.Button();
			DownloadGroupBox = new System.Windows.Forms.GroupBox();
			DownloadFolderTextBox = new Crowbar.TextBoxEx();
			DownloadProgressBarEx = new Crowbar.ProgressBarEx();
			BrowseForDownloadFolderButton = new System.Windows.Forms.Button();
			DownloadFolderLabel = new System.Windows.Forms.Label();
			GotoDownloadFileButton = new System.Windows.Forms.Button();
			CancelDownloadButton = new System.Windows.Forms.Button();
			DownloadButton = new System.Windows.Forms.Button();
			UpdateGroupBox = new System.Windows.Forms.GroupBox();
			CancelUpdateButton = new System.Windows.Forms.Button();
			BrowseForUpdateFolderButton = new System.Windows.Forms.Button();
			UpdateFolderTextBox = new Crowbar.TextBoxEx();
			UpdateProgressBarEx = new Crowbar.ProgressBarEx();
			UpdateButton = new System.Windows.Forms.Button();
			UpdateToNewPathCheckBox = new System.Windows.Forms.CheckBox();
			UpdateCopySettingsCheckBox = new System.Windows.Forms.CheckBox();
			UpdateUserControlFillPanel.SuspendLayout();
			CheckForUpdateGroupBox.SuspendLayout();
			Panel1.SuspendLayout();
			CheckForUpdateTopPanel.SuspendLayout();
			DownloadGroupBox.SuspendLayout();
			UpdateGroupBox.SuspendLayout();
			SuspendLayout();
			//
			//UpdateUserControlFillPanel
			//
			UpdateUserControlFillPanel.Controls.Add(CheckForUpdateGroupBox);
			UpdateUserControlFillPanel.Controls.Add(DownloadGroupBox);
			UpdateUserControlFillPanel.Controls.Add(UpdateGroupBox);
			UpdateUserControlFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			UpdateUserControlFillPanel.Location = new System.Drawing.Point(0, 0);
			UpdateUserControlFillPanel.Name = "UpdateUserControlFillPanel";
			UpdateUserControlFillPanel.Size = new System.Drawing.Size(776, 536);
			UpdateUserControlFillPanel.TabIndex = 17;
			//
			//CheckForUpdateGroupBox
			//
			CheckForUpdateGroupBox.Controls.Add(Panel1);
			CheckForUpdateGroupBox.Controls.Add(CurrentVersionLabel);
			CheckForUpdateGroupBox.Controls.Add(CheckForUpdateTopPanel);
			CheckForUpdateGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			CheckForUpdateGroupBox.Location = new System.Drawing.Point(0, 0);
			CheckForUpdateGroupBox.Name = "CheckForUpdateGroupBox";
			CheckForUpdateGroupBox.Size = new System.Drawing.Size(776, 365);
			CheckForUpdateGroupBox.TabIndex = 14;
			CheckForUpdateGroupBox.TabStop = false;
			CheckForUpdateGroupBox.Text = "Check for Update - Check for latest version and get changelog";
			//
			//Panel1
			//
			Panel1.Controls.Add(ChangelogTextBox);
			Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel1.Location = new System.Drawing.Point(3, 44);
			Panel1.Name = "Panel1";
			Panel1.Padding = new System.Windows.Forms.Padding(3);
			Panel1.Size = new System.Drawing.Size(770, 318);
			Panel1.TabIndex = 16;
			//
			//ChangelogTextBox
			//
			ChangelogTextBox.BackColor = System.Drawing.SystemColors.Control;
			ChangelogTextBox.CueBannerText = "";
			ChangelogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			ChangelogTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			ChangelogTextBox.Location = new System.Drawing.Point(3, 3);
			ChangelogTextBox.Name = "ChangelogTextBox";
			ChangelogTextBox.Size = new System.Drawing.Size(764, 312);
			ChangelogTextBox.TabIndex = 6;
			ChangelogTextBox.Text = "";
			//
			//CurrentVersionLabel
			//
			CurrentVersionLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			CurrentVersionLabel.AutoSize = true;
			CurrentVersionLabel.Location = new System.Drawing.Point(659, 0);
			CurrentVersionLabel.Name = "CurrentVersionLabel";
			CurrentVersionLabel.Size = new System.Drawing.Size(114, 13);
			CurrentVersionLabel.TabIndex = 14;
			CurrentVersionLabel.Text = "Current Version: 0.00";
			//
			//CheckForUpdateTopPanel
			//
			CheckForUpdateTopPanel.Controls.Add(CheckForUpdateButton);
			CheckForUpdateTopPanel.Controls.Add(CheckForUpdateTextBox);
			CheckForUpdateTopPanel.Controls.Add(CheckForUpdateProgressBar);
			CheckForUpdateTopPanel.Controls.Add(CancelCheckButton);
			CheckForUpdateTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			CheckForUpdateTopPanel.Location = new System.Drawing.Point(3, 18);
			CheckForUpdateTopPanel.Name = "CheckForUpdateTopPanel";
			CheckForUpdateTopPanel.Size = new System.Drawing.Size(770, 26);
			CheckForUpdateTopPanel.TabIndex = 15;
			//
			//CheckForUpdateButton
			//
			CheckForUpdateButton.Location = new System.Drawing.Point(3, 0);
			CheckForUpdateButton.Name = "CheckForUpdateButton";
			CheckForUpdateButton.Size = new System.Drawing.Size(69, 23);
			CheckForUpdateButton.TabIndex = 1;
			CheckForUpdateButton.Text = "Check";
			CheckForUpdateButton.UseVisualStyleBackColor = true;
			//
			//CheckForUpdateTextBox
			//
			CheckForUpdateTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			CheckForUpdateTextBox.CueBannerText = "";
			CheckForUpdateTextBox.Location = new System.Drawing.Point(78, 0);
			CheckForUpdateTextBox.Name = "CheckForUpdateTextBox";
			CheckForUpdateTextBox.ReadOnly = true;
			CheckForUpdateTextBox.Size = new System.Drawing.Size(614, 22);
			CheckForUpdateTextBox.TabIndex = 9;
			CheckForUpdateTextBox.Text = "[not checked yet]";
			//
			//CheckForUpdateProgressBar
			//
			CheckForUpdateProgressBar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			CheckForUpdateProgressBar.Location = new System.Drawing.Point(78, 0);
			CheckForUpdateProgressBar.Name = "CheckForUpdateProgressBar";
			CheckForUpdateProgressBar.Size = new System.Drawing.Size(614, 22);
			CheckForUpdateProgressBar.TabIndex = 10;
			CheckForUpdateProgressBar.Visible = false;
			//
			//CancelCheckButton
			//
			CancelCheckButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			CancelCheckButton.Enabled = false;
			CancelCheckButton.Location = new System.Drawing.Point(698, 0);
			CancelCheckButton.Name = "CancelCheckButton";
			CancelCheckButton.Size = new System.Drawing.Size(69, 23);
			CancelCheckButton.TabIndex = 11;
			CancelCheckButton.Text = "Cancel";
			CancelCheckButton.UseVisualStyleBackColor = true;
			//
			//DownloadGroupBox
			//
			DownloadGroupBox.Controls.Add(DownloadFolderTextBox);
			DownloadGroupBox.Controls.Add(DownloadProgressBarEx);
			DownloadGroupBox.Controls.Add(BrowseForDownloadFolderButton);
			DownloadGroupBox.Controls.Add(DownloadFolderLabel);
			DownloadGroupBox.Controls.Add(GotoDownloadFileButton);
			DownloadGroupBox.Controls.Add(CancelDownloadButton);
			DownloadGroupBox.Controls.Add(DownloadButton);
			DownloadGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			DownloadGroupBox.Location = new System.Drawing.Point(0, 365);
			DownloadGroupBox.Name = "DownloadGroupBox";
			DownloadGroupBox.Size = new System.Drawing.Size(776, 76);
			DownloadGroupBox.TabIndex = 8;
			DownloadGroupBox.TabStop = false;
			DownloadGroupBox.Text = "Download - Download the new version (compressed file) for manually updating";
			//
			//DownloadFolderTextBox
			//
			DownloadFolderTextBox.AllowDrop = true;
			DownloadFolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			DownloadFolderTextBox.CueBannerText = "";
			DownloadFolderTextBox.Location = new System.Drawing.Point(107, 15);
			DownloadFolderTextBox.Name = "DownloadFolderTextBox";
			DownloadFolderTextBox.Size = new System.Drawing.Size(582, 22);
			DownloadFolderTextBox.TabIndex = 7;
			//
			//DownloadProgressBarEx
			//
			DownloadProgressBarEx.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			DownloadProgressBarEx.Location = new System.Drawing.Point(87, 44);
			DownloadProgressBarEx.Name = "DownloadProgressBarEx";
			DownloadProgressBarEx.Size = new System.Drawing.Size(521, 23);
			DownloadProgressBarEx.TabIndex = 6;
			//
			//BrowseForDownloadFolderButton
			//
			BrowseForDownloadFolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForDownloadFolderButton.Location = new System.Drawing.Point(695, 15);
			BrowseForDownloadFolderButton.Name = "BrowseForDownloadFolderButton";
			BrowseForDownloadFolderButton.Size = new System.Drawing.Size(75, 23);
			BrowseForDownloadFolderButton.TabIndex = 8;
			BrowseForDownloadFolderButton.Text = "Browse...";
			BrowseForDownloadFolderButton.UseVisualStyleBackColor = true;
			//
			//DownloadFolderLabel
			//
			DownloadFolderLabel.AutoSize = true;
			DownloadFolderLabel.Location = new System.Drawing.Point(3, 20);
			DownloadFolderLabel.Name = "DownloadFolderLabel";
			DownloadFolderLabel.Size = new System.Drawing.Size(98, 13);
			DownloadFolderLabel.TabIndex = 9;
			DownloadFolderLabel.Text = "Download folder:";
			//
			//GotoDownloadFileButton
			//
			GotoDownloadFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoDownloadFileButton.Enabled = false;
			GotoDownloadFileButton.Location = new System.Drawing.Point(695, 44);
			GotoDownloadFileButton.Name = "GotoDownloadFileButton";
			GotoDownloadFileButton.Size = new System.Drawing.Size(75, 23);
			GotoDownloadFileButton.TabIndex = 13;
			GotoDownloadFileButton.Text = "Goto";
			GotoDownloadFileButton.UseVisualStyleBackColor = true;
			//
			//CancelDownloadButton
			//
			CancelDownloadButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			CancelDownloadButton.Enabled = false;
			CancelDownloadButton.Location = new System.Drawing.Point(614, 44);
			CancelDownloadButton.Name = "CancelDownloadButton";
			CancelDownloadButton.Size = new System.Drawing.Size(75, 23);
			CancelDownloadButton.TabIndex = 12;
			CancelDownloadButton.Text = "Cancel";
			CancelDownloadButton.UseVisualStyleBackColor = true;
			//
			//DownloadButton
			//
			DownloadButton.Location = new System.Drawing.Point(6, 44);
			DownloadButton.Name = "DownloadButton";
			DownloadButton.Size = new System.Drawing.Size(75, 23);
			DownloadButton.TabIndex = 2;
			DownloadButton.Text = "Download";
			DownloadButton.UseVisualStyleBackColor = true;
			//
			//UpdateGroupBox
			//
			UpdateGroupBox.Controls.Add(CancelUpdateButton);
			UpdateGroupBox.Controls.Add(BrowseForUpdateFolderButton);
			UpdateGroupBox.Controls.Add(UpdateFolderTextBox);
			UpdateGroupBox.Controls.Add(UpdateProgressBarEx);
			UpdateGroupBox.Controls.Add(UpdateButton);
			UpdateGroupBox.Controls.Add(UpdateToNewPathCheckBox);
			UpdateGroupBox.Controls.Add(UpdateCopySettingsCheckBox);
			UpdateGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			UpdateGroupBox.Location = new System.Drawing.Point(0, 441);
			UpdateGroupBox.Name = "UpdateGroupBox";
			UpdateGroupBox.Size = new System.Drawing.Size(776, 95);
			UpdateGroupBox.TabIndex = 7;
			UpdateGroupBox.TabStop = false;
			UpdateGroupBox.Text = "Update - Update current version to latest version - Crowbar will close and reopen" + "";
			//
			//CancelUpdateButton
			//
			CancelUpdateButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			CancelUpdateButton.Enabled = false;
			CancelUpdateButton.Location = new System.Drawing.Point(695, 66);
			CancelUpdateButton.Name = "CancelUpdateButton";
			CancelUpdateButton.Size = new System.Drawing.Size(75, 23);
			CancelUpdateButton.TabIndex = 13;
			CancelUpdateButton.Text = "Cancel";
			CancelUpdateButton.UseVisualStyleBackColor = true;
			//
			//BrowseForUpdateFolderButton
			//
			BrowseForUpdateFolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForUpdateFolderButton.Location = new System.Drawing.Point(695, 16);
			BrowseForUpdateFolderButton.Name = "BrowseForUpdateFolderButton";
			BrowseForUpdateFolderButton.Size = new System.Drawing.Size(75, 23);
			BrowseForUpdateFolderButton.TabIndex = 10;
			BrowseForUpdateFolderButton.Text = "Browse...";
			BrowseForUpdateFolderButton.UseVisualStyleBackColor = true;
			//
			//UpdateFolderTextBox
			//
			UpdateFolderTextBox.AllowDrop = true;
			UpdateFolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			UpdateFolderTextBox.CueBannerText = "";
			UpdateFolderTextBox.Location = new System.Drawing.Point(266, 16);
			UpdateFolderTextBox.Name = "UpdateFolderTextBox";
			UpdateFolderTextBox.Size = new System.Drawing.Size(423, 22);
			UpdateFolderTextBox.TabIndex = 9;
			//
			//UpdateProgressBarEx
			//
			UpdateProgressBarEx.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			UpdateProgressBarEx.Location = new System.Drawing.Point(87, 66);
			UpdateProgressBarEx.Name = "UpdateProgressBarEx";
			UpdateProgressBarEx.Size = new System.Drawing.Size(602, 23);
			UpdateProgressBarEx.TabIndex = 5;
			//
			//UpdateButton
			//
			UpdateButton.Location = new System.Drawing.Point(6, 66);
			UpdateButton.Name = "UpdateButton";
			UpdateButton.Size = new System.Drawing.Size(75, 23);
			UpdateButton.TabIndex = 0;
			UpdateButton.Text = "Update";
			UpdateButton.UseVisualStyleBackColor = true;
			//
			//UpdateToNewPathCheckBox
			//
			UpdateToNewPathCheckBox.AutoSize = true;
			UpdateToNewPathCheckBox.Location = new System.Drawing.Point(6, 20);
			UpdateToNewPathCheckBox.Name = "UpdateToNewPathCheckBox";
			UpdateToNewPathCheckBox.Size = new System.Drawing.Size(254, 17);
			UpdateToNewPathCheckBox.TabIndex = 4;
			UpdateToNewPathCheckBox.Text = "Update to new folder (keep current version):";
			UpdateToNewPathCheckBox.UseVisualStyleBackColor = true;
			//
			//UpdateCopySettingsCheckBox
			//
			UpdateCopySettingsCheckBox.AutoSize = true;
			UpdateCopySettingsCheckBox.Checked = true;
			UpdateCopySettingsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			UpdateCopySettingsCheckBox.Location = new System.Drawing.Point(6, 43);
			UpdateCopySettingsCheckBox.Name = "UpdateCopySettingsCheckBox";
			UpdateCopySettingsCheckBox.Size = new System.Drawing.Size(282, 17);
			UpdateCopySettingsCheckBox.TabIndex = 3;
			UpdateCopySettingsCheckBox.Text = "Copy settings from current version to new version";
			UpdateCopySettingsCheckBox.UseVisualStyleBackColor = true;
			//
			//UpdateUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(UpdateUserControlFillPanel);
			Name = "UpdateUserControl";
			Size = new System.Drawing.Size(776, 536);
			UpdateUserControlFillPanel.ResumeLayout(false);
			CheckForUpdateGroupBox.ResumeLayout(false);
			CheckForUpdateGroupBox.PerformLayout();
			Panel1.ResumeLayout(false);
			CheckForUpdateTopPanel.ResumeLayout(false);
			CheckForUpdateTopPanel.PerformLayout();
			DownloadGroupBox.ResumeLayout(false);
			DownloadGroupBox.PerformLayout();
			UpdateGroupBox.ResumeLayout(false);
			UpdateGroupBox.PerformLayout();
			ResumeLayout(false);

			base.Load += new System.EventHandler(UpdateUserControl_Load);
			CheckForUpdateButton.Click += new System.EventHandler(CheckForUpdateButton_Click);
			DownloadFolderTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(DownloadFolderTextBox_DragDrop);
			DownloadFolderTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(DownloadFolderTextBox_DragEnter);
			BrowseForDownloadFolderButton.Click += new System.EventHandler(BrowseForDownloadFolderButton_Click);
			CancelCheckButton.Click += new System.EventHandler(CancelCheckButton_Click);
			DownloadButton.Click += new System.EventHandler(DownloadButton_Click);
			CancelDownloadButton.Click += new System.EventHandler(CancelDownloadButton_Click);
			GotoDownloadFileButton.Click += new System.EventHandler(GotoDownloadFileButton_Click);
			UpdateFolderTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(UpdateFolderTextBox_DragDrop);
			UpdateFolderTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(UpdateFolderTextBox_DragEnter);
			BrowseForUpdateFolderButton.Click += new System.EventHandler(BrowseForUpdateFolderButton_Click);
			UpdateButton.Click += new System.EventHandler(UpdateButton_Click);
			CancelUpdateButton.Click += new System.EventHandler(CancelUpdateButton_Click);
		}

		internal Button UpdateButton;
		internal Button CheckForUpdateButton;
		internal Button DownloadButton;
		internal CheckBox UpdateCopySettingsCheckBox;
		internal CheckBox UpdateToNewPathCheckBox;
		internal RichTextBoxEx ChangelogTextBox;
		internal GroupBox UpdateGroupBox;
		internal ProgressBarEx UpdateProgressBarEx;
		internal GroupBox DownloadGroupBox;
		internal ProgressBarEx DownloadProgressBarEx;
		internal Label DownloadFolderLabel;
		internal Button BrowseForDownloadFolderButton;
		internal TextBoxEx DownloadFolderTextBox;
		internal Button BrowseForUpdateFolderButton;
		internal TextBoxEx UpdateFolderTextBox;
		internal TextBoxEx CheckForUpdateTextBox;
		internal ProgressBarEx CheckForUpdateProgressBar;
		internal Button CancelUpdateButton;
		internal Button CancelDownloadButton;
		internal Button CancelCheckButton;
		internal GroupBox CheckForUpdateGroupBox;
		internal Label CurrentVersionLabel;
		internal Button GotoDownloadFileButton;
		internal Panel UpdateUserControlFillPanel;
		internal Panel CheckForUpdateTopPanel;
		internal Panel Panel1;
	}

}