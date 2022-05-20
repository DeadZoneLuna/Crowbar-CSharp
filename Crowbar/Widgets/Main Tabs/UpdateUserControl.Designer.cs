using System.ComponentModel;
using System.IO;
using System.Net;

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
			this.components = new System.ComponentModel.Container();
			this.UpdateUserControlFillPanel = new System.Windows.Forms.Panel();
			this.CheckForUpdateGroupBox = new System.Windows.Forms.GroupBox();
			this.Panel1 = new System.Windows.Forms.Panel();
			this.ChangelogTextBox = new Crowbar.RichTextBoxEx();
			this.CurrentVersionLabel = new System.Windows.Forms.Label();
			this.CheckForUpdateTopPanel = new System.Windows.Forms.Panel();
			this.CheckForUpdateButton = new System.Windows.Forms.Button();
			this.CheckForUpdateTextBox = new Crowbar.TextBoxEx();
			this.CheckForUpdateProgressBar = new Crowbar.ProgressBarEx();
			this.CancelCheckButton = new System.Windows.Forms.Button();
			this.DownloadGroupBox = new System.Windows.Forms.GroupBox();
			this.DownloadFolderTextBox = new Crowbar.TextBoxEx();
			this.DownloadProgressBarEx = new Crowbar.ProgressBarEx();
			this.BrowseForDownloadFolderButton = new System.Windows.Forms.Button();
			this.DownloadFolderLabel = new System.Windows.Forms.Label();
			this.GotoDownloadFileButton = new System.Windows.Forms.Button();
			this.CancelDownloadButton = new System.Windows.Forms.Button();
			this.DownloadButton = new System.Windows.Forms.Button();
			this.UpdateGroupBox = new System.Windows.Forms.GroupBox();
			this.CancelUpdateButton = new System.Windows.Forms.Button();
			this.BrowseForUpdateFolderButton = new System.Windows.Forms.Button();
			this.UpdateFolderTextBox = new Crowbar.TextBoxEx();
			this.UpdateProgressBarEx = new Crowbar.ProgressBarEx();
			this.UpdateButton = new System.Windows.Forms.Button();
			this.UpdateToNewPathCheckBox = new System.Windows.Forms.CheckBox();
			this.UpdateCopySettingsCheckBox = new System.Windows.Forms.CheckBox();
			this.UpdateUserControlFillPanel.SuspendLayout();
			this.CheckForUpdateGroupBox.SuspendLayout();
			this.Panel1.SuspendLayout();
			this.CheckForUpdateTopPanel.SuspendLayout();
			this.DownloadGroupBox.SuspendLayout();
			this.UpdateGroupBox.SuspendLayout();
			this.SuspendLayout();
			//
			//UpdateUserControlFillPanel
			//
			this.UpdateUserControlFillPanel.Controls.Add(this.CheckForUpdateGroupBox);
			this.UpdateUserControlFillPanel.Controls.Add(this.DownloadGroupBox);
			this.UpdateUserControlFillPanel.Controls.Add(this.UpdateGroupBox);
			this.UpdateUserControlFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UpdateUserControlFillPanel.Location = new System.Drawing.Point(0, 0);
			this.UpdateUserControlFillPanel.Name = "UpdateUserControlFillPanel";
			this.UpdateUserControlFillPanel.Size = new System.Drawing.Size(776, 536);
			this.UpdateUserControlFillPanel.TabIndex = 17;
			//
			//CheckForUpdateGroupBox
			//
			this.CheckForUpdateGroupBox.Controls.Add(this.Panel1);
			this.CheckForUpdateGroupBox.Controls.Add(this.CurrentVersionLabel);
			this.CheckForUpdateGroupBox.Controls.Add(this.CheckForUpdateTopPanel);
			this.CheckForUpdateGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CheckForUpdateGroupBox.Location = new System.Drawing.Point(0, 0);
			this.CheckForUpdateGroupBox.Name = "CheckForUpdateGroupBox";
			this.CheckForUpdateGroupBox.Size = new System.Drawing.Size(776, 365);
			this.CheckForUpdateGroupBox.TabIndex = 14;
			this.CheckForUpdateGroupBox.TabStop = false;
			this.CheckForUpdateGroupBox.Text = "Check for Update - Check for latest version and get changelog";
			//
			//Panel1
			//
			this.Panel1.Controls.Add(this.ChangelogTextBox);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel1.Location = new System.Drawing.Point(3, 44);
			this.Panel1.Name = "Panel1";
			this.Panel1.Padding = new System.Windows.Forms.Padding(3);
			this.Panel1.Size = new System.Drawing.Size(770, 318);
			this.Panel1.TabIndex = 16;
			//
			//ChangelogTextBox
			//
			this.ChangelogTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.ChangelogTextBox.CueBannerText = "";
			this.ChangelogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChangelogTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.ChangelogTextBox.Location = new System.Drawing.Point(3, 3);
			this.ChangelogTextBox.Name = "ChangelogTextBox";
			this.ChangelogTextBox.Size = new System.Drawing.Size(764, 312);
			this.ChangelogTextBox.TabIndex = 6;
			this.ChangelogTextBox.Text = "";
			//
			//CurrentVersionLabel
			//
			this.CurrentVersionLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CurrentVersionLabel.AutoSize = true;
			this.CurrentVersionLabel.Location = new System.Drawing.Point(659, 0);
			this.CurrentVersionLabel.Name = "CurrentVersionLabel";
			this.CurrentVersionLabel.Size = new System.Drawing.Size(114, 13);
			this.CurrentVersionLabel.TabIndex = 14;
			this.CurrentVersionLabel.Text = "Current Version: 0.00";
			//
			//CheckForUpdateTopPanel
			//
			this.CheckForUpdateTopPanel.Controls.Add(this.CheckForUpdateButton);
			this.CheckForUpdateTopPanel.Controls.Add(this.CheckForUpdateTextBox);
			this.CheckForUpdateTopPanel.Controls.Add(this.CheckForUpdateProgressBar);
			this.CheckForUpdateTopPanel.Controls.Add(this.CancelCheckButton);
			this.CheckForUpdateTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.CheckForUpdateTopPanel.Location = new System.Drawing.Point(3, 18);
			this.CheckForUpdateTopPanel.Name = "CheckForUpdateTopPanel";
			this.CheckForUpdateTopPanel.Size = new System.Drawing.Size(770, 26);
			this.CheckForUpdateTopPanel.TabIndex = 15;
			//
			//CheckForUpdateButton
			//
			this.CheckForUpdateButton.Location = new System.Drawing.Point(3, 0);
			this.CheckForUpdateButton.Name = "CheckForUpdateButton";
			this.CheckForUpdateButton.Size = new System.Drawing.Size(69, 23);
			this.CheckForUpdateButton.TabIndex = 1;
			this.CheckForUpdateButton.Text = "Check";
			this.CheckForUpdateButton.UseVisualStyleBackColor = true;
			//
			//CheckForUpdateTextBox
			//
			this.CheckForUpdateTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CheckForUpdateTextBox.CueBannerText = "";
			this.CheckForUpdateTextBox.Location = new System.Drawing.Point(78, 0);
			this.CheckForUpdateTextBox.Name = "CheckForUpdateTextBox";
			this.CheckForUpdateTextBox.ReadOnly = true;
			this.CheckForUpdateTextBox.Size = new System.Drawing.Size(614, 22);
			this.CheckForUpdateTextBox.TabIndex = 9;
			this.CheckForUpdateTextBox.Text = "[not checked yet]";
			//
			//CheckForUpdateProgressBar
			//
			this.CheckForUpdateProgressBar.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CheckForUpdateProgressBar.Location = new System.Drawing.Point(78, 0);
			this.CheckForUpdateProgressBar.Name = "CheckForUpdateProgressBar";
			this.CheckForUpdateProgressBar.Size = new System.Drawing.Size(614, 22);
			this.CheckForUpdateProgressBar.TabIndex = 10;
			this.CheckForUpdateProgressBar.Visible = false;
			//
			//CancelCheckButton
			//
			this.CancelCheckButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CancelCheckButton.Enabled = false;
			this.CancelCheckButton.Location = new System.Drawing.Point(698, 0);
			this.CancelCheckButton.Name = "CancelCheckButton";
			this.CancelCheckButton.Size = new System.Drawing.Size(69, 23);
			this.CancelCheckButton.TabIndex = 11;
			this.CancelCheckButton.Text = "Cancel";
			this.CancelCheckButton.UseVisualStyleBackColor = true;
			//
			//DownloadGroupBox
			//
			this.DownloadGroupBox.Controls.Add(this.DownloadFolderTextBox);
			this.DownloadGroupBox.Controls.Add(this.DownloadProgressBarEx);
			this.DownloadGroupBox.Controls.Add(this.BrowseForDownloadFolderButton);
			this.DownloadGroupBox.Controls.Add(this.DownloadFolderLabel);
			this.DownloadGroupBox.Controls.Add(this.GotoDownloadFileButton);
			this.DownloadGroupBox.Controls.Add(this.CancelDownloadButton);
			this.DownloadGroupBox.Controls.Add(this.DownloadButton);
			this.DownloadGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.DownloadGroupBox.Location = new System.Drawing.Point(0, 365);
			this.DownloadGroupBox.Name = "DownloadGroupBox";
			this.DownloadGroupBox.Size = new System.Drawing.Size(776, 76);
			this.DownloadGroupBox.TabIndex = 8;
			this.DownloadGroupBox.TabStop = false;
			this.DownloadGroupBox.Text = "Download - Download the new version (compressed file) for manually updating";
			//
			//DownloadFolderTextBox
			//
			this.DownloadFolderTextBox.AllowDrop = true;
			this.DownloadFolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DownloadFolderTextBox.CueBannerText = "";
			this.DownloadFolderTextBox.Location = new System.Drawing.Point(107, 15);
			this.DownloadFolderTextBox.Name = "DownloadFolderTextBox";
			this.DownloadFolderTextBox.Size = new System.Drawing.Size(582, 22);
			this.DownloadFolderTextBox.TabIndex = 7;
			//
			//DownloadProgressBarEx
			//
			this.DownloadProgressBarEx.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DownloadProgressBarEx.Location = new System.Drawing.Point(87, 44);
			this.DownloadProgressBarEx.Name = "DownloadProgressBarEx";
			this.DownloadProgressBarEx.Size = new System.Drawing.Size(521, 23);
			this.DownloadProgressBarEx.TabIndex = 6;
			//
			//BrowseForDownloadFolderButton
			//
			this.BrowseForDownloadFolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForDownloadFolderButton.Location = new System.Drawing.Point(695, 15);
			this.BrowseForDownloadFolderButton.Name = "BrowseForDownloadFolderButton";
			this.BrowseForDownloadFolderButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForDownloadFolderButton.TabIndex = 8;
			this.BrowseForDownloadFolderButton.Text = "Browse...";
			this.BrowseForDownloadFolderButton.UseVisualStyleBackColor = true;
			//
			//DownloadFolderLabel
			//
			this.DownloadFolderLabel.AutoSize = true;
			this.DownloadFolderLabel.Location = new System.Drawing.Point(3, 20);
			this.DownloadFolderLabel.Name = "DownloadFolderLabel";
			this.DownloadFolderLabel.Size = new System.Drawing.Size(98, 13);
			this.DownloadFolderLabel.TabIndex = 9;
			this.DownloadFolderLabel.Text = "Download folder:";
			//
			//GotoDownloadFileButton
			//
			this.GotoDownloadFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoDownloadFileButton.Enabled = false;
			this.GotoDownloadFileButton.Location = new System.Drawing.Point(695, 44);
			this.GotoDownloadFileButton.Name = "GotoDownloadFileButton";
			this.GotoDownloadFileButton.Size = new System.Drawing.Size(75, 23);
			this.GotoDownloadFileButton.TabIndex = 13;
			this.GotoDownloadFileButton.Text = "Goto";
			this.GotoDownloadFileButton.UseVisualStyleBackColor = true;
			//
			//CancelDownloadButton
			//
			this.CancelDownloadButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CancelDownloadButton.Enabled = false;
			this.CancelDownloadButton.Location = new System.Drawing.Point(614, 44);
			this.CancelDownloadButton.Name = "CancelDownloadButton";
			this.CancelDownloadButton.Size = new System.Drawing.Size(75, 23);
			this.CancelDownloadButton.TabIndex = 12;
			this.CancelDownloadButton.Text = "Cancel";
			this.CancelDownloadButton.UseVisualStyleBackColor = true;
			//
			//DownloadButton
			//
			this.DownloadButton.Location = new System.Drawing.Point(6, 44);
			this.DownloadButton.Name = "DownloadButton";
			this.DownloadButton.Size = new System.Drawing.Size(75, 23);
			this.DownloadButton.TabIndex = 2;
			this.DownloadButton.Text = "Download";
			this.DownloadButton.UseVisualStyleBackColor = true;
			//
			//UpdateGroupBox
			//
			this.UpdateGroupBox.Controls.Add(this.CancelUpdateButton);
			this.UpdateGroupBox.Controls.Add(this.BrowseForUpdateFolderButton);
			this.UpdateGroupBox.Controls.Add(this.UpdateFolderTextBox);
			this.UpdateGroupBox.Controls.Add(this.UpdateProgressBarEx);
			this.UpdateGroupBox.Controls.Add(this.UpdateButton);
			this.UpdateGroupBox.Controls.Add(this.UpdateToNewPathCheckBox);
			this.UpdateGroupBox.Controls.Add(this.UpdateCopySettingsCheckBox);
			this.UpdateGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.UpdateGroupBox.Location = new System.Drawing.Point(0, 441);
			this.UpdateGroupBox.Name = "UpdateGroupBox";
			this.UpdateGroupBox.Size = new System.Drawing.Size(776, 95);
			this.UpdateGroupBox.TabIndex = 7;
			this.UpdateGroupBox.TabStop = false;
			this.UpdateGroupBox.Text = "Update - Update current version to latest version - Crowbar will close and reopen" + "";
			//
			//CancelUpdateButton
			//
			this.CancelUpdateButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CancelUpdateButton.Enabled = false;
			this.CancelUpdateButton.Location = new System.Drawing.Point(695, 66);
			this.CancelUpdateButton.Name = "CancelUpdateButton";
			this.CancelUpdateButton.Size = new System.Drawing.Size(75, 23);
			this.CancelUpdateButton.TabIndex = 13;
			this.CancelUpdateButton.Text = "Cancel";
			this.CancelUpdateButton.UseVisualStyleBackColor = true;
			//
			//BrowseForUpdateFolderButton
			//
			this.BrowseForUpdateFolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForUpdateFolderButton.Location = new System.Drawing.Point(695, 16);
			this.BrowseForUpdateFolderButton.Name = "BrowseForUpdateFolderButton";
			this.BrowseForUpdateFolderButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForUpdateFolderButton.TabIndex = 10;
			this.BrowseForUpdateFolderButton.Text = "Browse...";
			this.BrowseForUpdateFolderButton.UseVisualStyleBackColor = true;
			//
			//UpdateFolderTextBox
			//
			this.UpdateFolderTextBox.AllowDrop = true;
			this.UpdateFolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.UpdateFolderTextBox.CueBannerText = "";
			this.UpdateFolderTextBox.Location = new System.Drawing.Point(266, 16);
			this.UpdateFolderTextBox.Name = "UpdateFolderTextBox";
			this.UpdateFolderTextBox.Size = new System.Drawing.Size(423, 22);
			this.UpdateFolderTextBox.TabIndex = 9;
			//
			//UpdateProgressBarEx
			//
			this.UpdateProgressBarEx.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.UpdateProgressBarEx.Location = new System.Drawing.Point(87, 66);
			this.UpdateProgressBarEx.Name = "UpdateProgressBarEx";
			this.UpdateProgressBarEx.Size = new System.Drawing.Size(602, 23);
			this.UpdateProgressBarEx.TabIndex = 5;
			//
			//UpdateButton
			//
			this.UpdateButton.Location = new System.Drawing.Point(6, 66);
			this.UpdateButton.Name = "UpdateButton";
			this.UpdateButton.Size = new System.Drawing.Size(75, 23);
			this.UpdateButton.TabIndex = 0;
			this.UpdateButton.Text = "Update";
			this.UpdateButton.UseVisualStyleBackColor = true;
			//
			//UpdateToNewPathCheckBox
			//
			this.UpdateToNewPathCheckBox.AutoSize = true;
			this.UpdateToNewPathCheckBox.Location = new System.Drawing.Point(6, 20);
			this.UpdateToNewPathCheckBox.Name = "UpdateToNewPathCheckBox";
			this.UpdateToNewPathCheckBox.Size = new System.Drawing.Size(254, 17);
			this.UpdateToNewPathCheckBox.TabIndex = 4;
			this.UpdateToNewPathCheckBox.Text = "Update to new folder (keep current version):";
			this.UpdateToNewPathCheckBox.UseVisualStyleBackColor = true;
			//
			//UpdateCopySettingsCheckBox
			//
			this.UpdateCopySettingsCheckBox.AutoSize = true;
			this.UpdateCopySettingsCheckBox.Checked = true;
			this.UpdateCopySettingsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.UpdateCopySettingsCheckBox.Location = new System.Drawing.Point(6, 43);
			this.UpdateCopySettingsCheckBox.Name = "UpdateCopySettingsCheckBox";
			this.UpdateCopySettingsCheckBox.Size = new System.Drawing.Size(282, 17);
			this.UpdateCopySettingsCheckBox.TabIndex = 3;
			this.UpdateCopySettingsCheckBox.Text = "Copy settings from current version to new version";
			this.UpdateCopySettingsCheckBox.UseVisualStyleBackColor = true;
			//
			//UpdateUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.UpdateUserControlFillPanel);
			this.Name = "UpdateUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.UpdateUserControlFillPanel.ResumeLayout(false);
			this.CheckForUpdateGroupBox.ResumeLayout(false);
			this.CheckForUpdateGroupBox.PerformLayout();
			this.Panel1.ResumeLayout(false);
			this.CheckForUpdateTopPanel.ResumeLayout(false);
			this.CheckForUpdateTopPanel.PerformLayout();
			this.DownloadGroupBox.ResumeLayout(false);
			this.DownloadGroupBox.PerformLayout();
			this.UpdateGroupBox.ResumeLayout(false);
			this.UpdateGroupBox.PerformLayout();
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
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