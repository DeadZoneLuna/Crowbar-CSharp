using System.IO;
using System.Text;

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
	public partial class CompileUserControl : BaseUserControl
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
			this.CompilerOptionsTextBox = new System.Windows.Forms.TextBox();
			this.GameSetupComboBox = new System.Windows.Forms.ComboBox();
			this.FolderForEachModelCheckBox = new System.Windows.Forms.CheckBox();
			this.SourceEngineLogFileCheckBox = new System.Windows.Forms.CheckBox();
			this.CompilerOptionDefineBonesCheckBox = new System.Windows.Forms.CheckBox();
			this.CompilerOptionNoP4CheckBox = new System.Windows.Forms.CheckBox();
			this.CompilerOptionVerboseCheckBox = new System.Windows.Forms.CheckBox();
			this.CompilerOptionDefineBonesModifyQcFileCheckBox = new System.Windows.Forms.CheckBox();
			this.CompilerOptionDefineBonesWriteQciFileCheckBox = new System.Windows.Forms.CheckBox();
			this.CompilerOptionDefineBonesFileNameTextBox = new System.Windows.Forms.TextBox();
			this.DirectCompilerOptionsLabel = new System.Windows.Forms.Label();
			this.DirectCompilerOptionsTextBox = new System.Windows.Forms.TextBox();
			this.BrowseForQcPathFolderOrFileNameButton = new System.Windows.Forms.Button();
			this.Label6 = new System.Windows.Forms.Label();
			this.EditGameSetupButton = new System.Windows.Forms.Button();
			this.GameSetupLabel = new System.Windows.Forms.Label();
			this.CompileButton = new System.Windows.Forms.Button();
			this.Panel1 = new System.Windows.Forms.Panel();
			this.QcPathFileNameTextBox = new Crowbar.TextBoxEx();
			this.OutputPathTextBox = new Crowbar.TextBoxEx();
			this.GameModelsOutputPathTextBox = new Crowbar.TextBoxEx();
			this.OutputSubfolderTextBox = new Crowbar.TextBoxEx();
			this.GotoOutputPathButton = new System.Windows.Forms.Button();
			this.BrowseForOutputPathButton = new System.Windows.Forms.Button();
			this.OutputPathComboBox = new System.Windows.Forms.ComboBox();
			this.CompileComboBox = new System.Windows.Forms.ComboBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.GotoQcButton = new System.Windows.Forms.Button();
			this.Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.OptionsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			this.GameSetupPanel = new System.Windows.Forms.Panel();
			this.CompilerOptionsSourceEnginePanel = new System.Windows.Forms.Panel();
			this.DefineBonesGroupBox = new System.Windows.Forms.GroupBox();
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox = new System.Windows.Forms.CheckBox();
			this.CompileOptionsSourceEngineUseDefaultsButton = new System.Windows.Forms.Button();
			this.CompilerOptionsGoldSourceEnginePanel = new System.Windows.Forms.Panel();
			this.GoldSourceEngineLogFileCheckBox = new System.Windows.Forms.CheckBox();
			this.CompileOptionsGoldSourceEngineUseDefaultsButton = new System.Windows.Forms.Button();
			this.CompilerOptionsTextBoxMinScrollPanel = new System.Windows.Forms.Panel();
			this.CompileLogRichTextBox = new Crowbar.RichTextBoxEx();
			this.CompileButtonsPanel = new System.Windows.Forms.Panel();
			this.SkipCurrentModelButton = new System.Windows.Forms.Button();
			this.CancelCompileButton = new System.Windows.Forms.Button();
			this.UseAllInPackButton = new System.Windows.Forms.Button();
			this.PostCompilePanel = new System.Windows.Forms.Panel();
			this.CompiledFilesComboBox = new System.Windows.Forms.ComboBox();
			this.UseInViewButton = new System.Windows.Forms.Button();
			this.RecompileButton = new System.Windows.Forms.Button();
			this.UseInPackButton = new System.Windows.Forms.Button();
			this.GotoCompiledMdlButton = new System.Windows.Forms.Button();
			this.UseDefaultOutputSubfolderButton = new System.Windows.Forms.Button();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).BeginInit();
			this.Options_LogSplitContainer.Panel1.SuspendLayout();
			this.Options_LogSplitContainer.Panel2.SuspendLayout();
			this.Options_LogSplitContainer.SuspendLayout();
			this.OptionsGroupBox.SuspendLayout();
			this.OptionsGroupBoxFillPanel.SuspendLayout();
			this.GameSetupPanel.SuspendLayout();
			this.CompilerOptionsSourceEnginePanel.SuspendLayout();
			this.DefineBonesGroupBox.SuspendLayout();
			this.CompilerOptionsGoldSourceEnginePanel.SuspendLayout();
			this.CompileButtonsPanel.SuspendLayout();
			this.PostCompilePanel.SuspendLayout();
			this.SuspendLayout();
			//
			//CompilerOptionsTextBox
			//
			this.CompilerOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CompilerOptionsTextBox.Location = new System.Drawing.Point(0, 169);
			this.CompilerOptionsTextBox.Multiline = true;
			this.CompilerOptionsTextBox.Name = "CompilerOptionsTextBox";
			this.CompilerOptionsTextBox.ReadOnly = true;
			this.CompilerOptionsTextBox.Size = new System.Drawing.Size(764, 37);
			this.CompilerOptionsTextBox.TabIndex = 15;
			//
			//GameSetupComboBox
			//
			this.GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GameSetupComboBox.FormattingEnabled = true;
			this.GameSetupComboBox.Location = new System.Drawing.Point(192, 1);
			this.GameSetupComboBox.Name = "GameSetupComboBox";
			this.GameSetupComboBox.Size = new System.Drawing.Size(476, 21);
			this.GameSetupComboBox.TabIndex = 1;
			//
			//FolderForEachModelCheckBox
			//
			this.FolderForEachModelCheckBox.AutoSize = true;
			this.FolderForEachModelCheckBox.Location = new System.Drawing.Point(502, 74);
			this.FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox";
			this.FolderForEachModelCheckBox.Size = new System.Drawing.Size(139, 17);
			this.FolderForEachModelCheckBox.TabIndex = 3;
			this.FolderForEachModelCheckBox.Text = "Folder for each model";
			this.FolderForEachModelCheckBox.UseVisualStyleBackColor = true;
			this.FolderForEachModelCheckBox.Visible = false;
			//
			//SourceEngineLogFileCheckBox
			//
			this.SourceEngineLogFileCheckBox.AutoSize = true;
			this.SourceEngineLogFileCheckBox.Location = new System.Drawing.Point(6, 3);
			this.SourceEngineLogFileCheckBox.Name = "SourceEngineLogFileCheckBox";
			this.SourceEngineLogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			this.SourceEngineLogFileCheckBox.TabIndex = 4;
			this.SourceEngineLogFileCheckBox.Text = "Write log to a file";
			this.ToolTip1.SetToolTip(this.SourceEngineLogFileCheckBox, "Write compile log to a file (in same folder as QC file).");
			this.SourceEngineLogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionDefineBonesCheckBox
			//
			this.CompilerOptionDefineBonesCheckBox.AutoSize = true;
			this.CompilerOptionDefineBonesCheckBox.Location = new System.Drawing.Point(179, 4);
			this.CompilerOptionDefineBonesCheckBox.Name = "CompilerOptionDefineBonesCheckBox";
			this.CompilerOptionDefineBonesCheckBox.Size = new System.Drawing.Size(91, 17);
			this.CompilerOptionDefineBonesCheckBox.TabIndex = 7;
			this.CompilerOptionDefineBonesCheckBox.Text = "DefineBones";
			this.CompilerOptionDefineBonesCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionNoP4CheckBox
			//
			this.CompilerOptionNoP4CheckBox.AutoSize = true;
			this.CompilerOptionNoP4CheckBox.Location = new System.Drawing.Point(6, 26);
			this.CompilerOptionNoP4CheckBox.Name = "CompilerOptionNoP4CheckBox";
			this.CompilerOptionNoP4CheckBox.Size = new System.Drawing.Size(56, 17);
			this.CompilerOptionNoP4CheckBox.TabIndex = 5;
			this.CompilerOptionNoP4CheckBox.Text = "No P4";
			this.ToolTip1.SetToolTip(this.CompilerOptionNoP4CheckBox, "No Perforce integration (modders do not usually have Perforce software).");
			this.CompilerOptionNoP4CheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionVerboseCheckBox
			//
			this.CompilerOptionVerboseCheckBox.AutoSize = true;
			this.CompilerOptionVerboseCheckBox.Location = new System.Drawing.Point(6, 49);
			this.CompilerOptionVerboseCheckBox.Name = "CompilerOptionVerboseCheckBox";
			this.CompilerOptionVerboseCheckBox.Size = new System.Drawing.Size(67, 17);
			this.CompilerOptionVerboseCheckBox.TabIndex = 6;
			this.CompilerOptionVerboseCheckBox.Text = "Verbose";
			this.ToolTip1.SetToolTip(this.CompilerOptionVerboseCheckBox, "Write more info in compile log.");
			this.CompilerOptionVerboseCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionDefineBonesModifyQcFileCheckBox
			//
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.AutoSize = true;
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.Enabled = false;
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.Location = new System.Drawing.Point(19, 65);
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.Name = "CompilerOptionDefineBonesModifyQcFileCheckBox";
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.Size = new System.Drawing.Size(238, 17);
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.TabIndex = 11;
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.Text = "Put in QC file: $include \"<QCI file name>\"";
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionDefineBonesWriteQciFileCheckBox
			//
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.AutoSize = true;
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.Enabled = false;
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.Location = new System.Drawing.Point(6, 22);
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.Name = "CompilerOptionDefineBonesWriteQciFileCheckBox";
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.Size = new System.Drawing.Size(97, 17);
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.TabIndex = 8;
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.Text = "Write QCI file:";
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionDefineBonesFileNameTextBox
			//
			this.CompilerOptionDefineBonesFileNameTextBox.Enabled = false;
			this.CompilerOptionDefineBonesFileNameTextBox.Location = new System.Drawing.Point(109, 18);
			this.CompilerOptionDefineBonesFileNameTextBox.Name = "CompilerOptionDefineBonesFileNameTextBox";
			this.CompilerOptionDefineBonesFileNameTextBox.Size = new System.Drawing.Size(140, 22);
			this.CompilerOptionDefineBonesFileNameTextBox.TabIndex = 10;
			//
			//DirectCompilerOptionsLabel
			//
			this.DirectCompilerOptionsLabel.Location = new System.Drawing.Point(0, 125);
			this.DirectCompilerOptionsLabel.Name = "DirectCompilerOptionsLabel";
			this.DirectCompilerOptionsLabel.Size = new System.Drawing.Size(764, 13);
			this.DirectCompilerOptionsLabel.TabIndex = 13;
			this.DirectCompilerOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):";
			//
			//DirectCompilerOptionsTextBox
			//
			this.DirectCompilerOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DirectCompilerOptionsTextBox.Location = new System.Drawing.Point(0, 141);
			this.DirectCompilerOptionsTextBox.Name = "DirectCompilerOptionsTextBox";
			this.DirectCompilerOptionsTextBox.Size = new System.Drawing.Size(764, 22);
			this.DirectCompilerOptionsTextBox.TabIndex = 14;
			//
			//BrowseForQcPathFolderOrFileNameButton
			//
			this.BrowseForQcPathFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForQcPathFolderOrFileNameButton.Location = new System.Drawing.Point(660, 3);
			this.BrowseForQcPathFolderOrFileNameButton.Name = "BrowseForQcPathFolderOrFileNameButton";
			this.BrowseForQcPathFolderOrFileNameButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForQcPathFolderOrFileNameButton.TabIndex = 3;
			this.BrowseForQcPathFolderOrFileNameButton.Text = "Browse...";
			this.BrowseForQcPathFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//Label6
			//
			this.Label6.Location = new System.Drawing.Point(3, 8);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(57, 13);
			this.Label6.TabIndex = 0;
			this.Label6.Text = "QC input:";
			//
			//EditGameSetupButton
			//
			this.EditGameSetupButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.EditGameSetupButton.Location = new System.Drawing.Point(674, 0);
			this.EditGameSetupButton.Name = "EditGameSetupButton";
			this.EditGameSetupButton.Size = new System.Drawing.Size(90, 23);
			this.EditGameSetupButton.TabIndex = 2;
			this.EditGameSetupButton.Text = "Set Up Games";
			this.EditGameSetupButton.UseVisualStyleBackColor = true;
			//
			//GameSetupLabel
			//
			this.GameSetupLabel.Location = new System.Drawing.Point(0, 5);
			this.GameSetupLabel.Name = "GameSetupLabel";
			this.GameSetupLabel.Size = new System.Drawing.Size(186, 13);
			this.GameSetupLabel.TabIndex = 0;
			this.GameSetupLabel.Text = "Game that has the model compiler:";
			//
			//CompileButton
			//
			this.CompileButton.Location = new System.Drawing.Point(0, 0);
			this.CompileButton.Name = "CompileButton";
			this.CompileButton.Size = new System.Drawing.Size(125, 23);
			this.CompileButton.TabIndex = 1;
			this.CompileButton.Text = "&Compile DefineBones";
			this.CompileButton.UseVisualStyleBackColor = true;
			//
			//Panel1
			//
			this.Panel1.Controls.Add(this.QcPathFileNameTextBox);
			this.Panel1.Controls.Add(this.OutputPathTextBox);
			this.Panel1.Controls.Add(this.GameModelsOutputPathTextBox);
			this.Panel1.Controls.Add(this.OutputSubfolderTextBox);
			this.Panel1.Controls.Add(this.GotoOutputPathButton);
			this.Panel1.Controls.Add(this.BrowseForOutputPathButton);
			this.Panel1.Controls.Add(this.OutputPathComboBox);
			this.Panel1.Controls.Add(this.CompileComboBox);
			this.Panel1.Controls.Add(this.Label1);
			this.Panel1.Controls.Add(this.GotoQcButton);
			this.Panel1.Controls.Add(this.Label6);
			this.Panel1.Controls.Add(this.BrowseForQcPathFolderOrFileNameButton);
			this.Panel1.Controls.Add(this.Options_LogSplitContainer);
			this.Panel1.Controls.Add(this.UseDefaultOutputSubfolderButton);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel1.Location = new System.Drawing.Point(0, 0);
			this.Panel1.Margin = new System.Windows.Forms.Padding(2);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(776, 536);
			this.Panel1.TabIndex = 15;
			//
			//QcPathFileNameTextBox
			//
			this.QcPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.QcPathFileNameTextBox.CueBannerText = "";
			this.QcPathFileNameTextBox.Location = new System.Drawing.Point(209, 3);
			this.QcPathFileNameTextBox.Name = "QcPathFileNameTextBox";
			this.QcPathFileNameTextBox.Size = new System.Drawing.Size(445, 22);
			this.QcPathFileNameTextBox.TabIndex = 22;
			//
			//OutputPathTextBox
			//
			this.OutputPathTextBox.AllowDrop = true;
			this.OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputPathTextBox.CueBannerText = "";
			this.OutputPathTextBox.Location = new System.Drawing.Point(209, 32);
			this.OutputPathTextBox.Name = "OutputPathTextBox";
			this.OutputPathTextBox.Size = new System.Drawing.Size(445, 22);
			this.OutputPathTextBox.TabIndex = 9;
			//
			//GameModelsOutputPathTextBox
			//
			this.GameModelsOutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameModelsOutputPathTextBox.CueBannerText = "";
			this.GameModelsOutputPathTextBox.Location = new System.Drawing.Point(209, 32);
			this.GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox";
			this.GameModelsOutputPathTextBox.ReadOnly = true;
			this.GameModelsOutputPathTextBox.Size = new System.Drawing.Size(445, 22);
			this.GameModelsOutputPathTextBox.TabIndex = 8;
			//
			//OutputSubfolderTextBox
			//
			this.OutputSubfolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputSubfolderTextBox.CueBannerText = "";
			this.OutputSubfolderTextBox.Location = new System.Drawing.Point(209, 32);
			this.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox";
			this.OutputSubfolderTextBox.Size = new System.Drawing.Size(445, 22);
			this.OutputSubfolderTextBox.TabIndex = 21;
			this.OutputSubfolderTextBox.Visible = false;
			//
			//GotoOutputPathButton
			//
			this.GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			this.GotoOutputPathButton.Name = "GotoOutputPathButton";
			this.GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			this.GotoOutputPathButton.TabIndex = 11;
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
			this.BrowseForOutputPathButton.TabIndex = 10;
			this.BrowseForOutputPathButton.Text = "Browse...";
			this.BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OutputPathComboBox
			//
			this.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OutputPathComboBox.FormattingEnabled = true;
			this.OutputPathComboBox.Location = new System.Drawing.Point(63, 33);
			this.OutputPathComboBox.Name = "OutputPathComboBox";
			this.OutputPathComboBox.Size = new System.Drawing.Size(140, 21);
			this.OutputPathComboBox.TabIndex = 6;
			//
			//CompileComboBox
			//
			this.CompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CompileComboBox.FormattingEnabled = true;
			this.CompileComboBox.Location = new System.Drawing.Point(63, 4);
			this.CompileComboBox.Name = "CompileComboBox";
			this.CompileComboBox.Size = new System.Drawing.Size(140, 21);
			this.CompileComboBox.TabIndex = 1;
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(3, 37);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(62, 13);
			this.Label1.TabIndex = 5;
			this.Label1.Text = "Output to:";
			//
			//GotoQcButton
			//
			this.GotoQcButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoQcButton.Location = new System.Drawing.Point(730, 3);
			this.GotoQcButton.Name = "GotoQcButton";
			this.GotoQcButton.Size = new System.Drawing.Size(43, 23);
			this.GotoQcButton.TabIndex = 4;
			this.GotoQcButton.Text = "Goto";
			this.GotoQcButton.UseVisualStyleBackColor = true;
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
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.CompileLogRichTextBox);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.CompileButtonsPanel);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.PostCompilePanel);
			this.Options_LogSplitContainer.Panel2MinSize = 45;
			this.Options_LogSplitContainer.Size = new System.Drawing.Size(770, 472);
			this.Options_LogSplitContainer.SplitterDistance = 230;
			this.Options_LogSplitContainer.TabIndex = 16;
			//
			//OptionsGroupBox
			//
			this.OptionsGroupBox.Controls.Add(this.OptionsGroupBoxFillPanel);
			this.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.OptionsGroupBox.Name = "OptionsGroupBox";
			this.OptionsGroupBox.Size = new System.Drawing.Size(770, 230);
			this.OptionsGroupBox.TabIndex = 0;
			this.OptionsGroupBox.TabStop = false;
			this.OptionsGroupBox.Text = "Options";
			//
			//OptionsGroupBoxFillPanel
			//
			this.OptionsGroupBoxFillPanel.AutoScroll = true;
			this.OptionsGroupBoxFillPanel.Controls.Add(this.GameSetupPanel);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.CompilerOptionsSourceEnginePanel);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.CompilerOptionsGoldSourceEnginePanel);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.DirectCompilerOptionsLabel);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.DirectCompilerOptionsTextBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.CompilerOptionsTextBox);
			this.OptionsGroupBoxFillPanel.Controls.Add(this.CompilerOptionsTextBoxMinScrollPanel);
			this.OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OptionsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			this.OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel";
			this.OptionsGroupBoxFillPanel.Size = new System.Drawing.Size(764, 209);
			this.OptionsGroupBoxFillPanel.TabIndex = 0;
			//
			//GameSetupPanel
			//
			this.GameSetupPanel.Controls.Add(this.GameSetupLabel);
			this.GameSetupPanel.Controls.Add(this.GameSetupComboBox);
			this.GameSetupPanel.Controls.Add(this.EditGameSetupButton);
			this.GameSetupPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.GameSetupPanel.Location = new System.Drawing.Point(0, 0);
			this.GameSetupPanel.Name = "GameSetupPanel";
			this.GameSetupPanel.Size = new System.Drawing.Size(764, 26);
			this.GameSetupPanel.TabIndex = 40;
			//
			//CompilerOptionsSourceEnginePanel
			//
			this.CompilerOptionsSourceEnginePanel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CompilerOptionsSourceEnginePanel.Controls.Add(this.CompilerOptionDefineBonesCheckBox);
			this.CompilerOptionsSourceEnginePanel.Controls.Add(this.DefineBonesGroupBox);
			this.CompilerOptionsSourceEnginePanel.Controls.Add(this.SourceEngineLogFileCheckBox);
			this.CompilerOptionsSourceEnginePanel.Controls.Add(this.CompilerOptionVerboseCheckBox);
			this.CompilerOptionsSourceEnginePanel.Controls.Add(this.CompilerOptionNoP4CheckBox);
			this.CompilerOptionsSourceEnginePanel.Controls.Add(this.FolderForEachModelCheckBox);
			this.CompilerOptionsSourceEnginePanel.Controls.Add(this.CompileOptionsSourceEngineUseDefaultsButton);
			this.CompilerOptionsSourceEnginePanel.Location = new System.Drawing.Point(0, 24);
			this.CompilerOptionsSourceEnginePanel.Name = "CompilerOptionsSourceEnginePanel";
			this.CompilerOptionsSourceEnginePanel.Size = new System.Drawing.Size(764, 100);
			this.CompilerOptionsSourceEnginePanel.TabIndex = 38;
			//
			//DefineBonesGroupBox
			//
			this.DefineBonesGroupBox.Controls.Add(this.CompilerOptionDefineBonesFileNameTextBox);
			this.DefineBonesGroupBox.Controls.Add(this.CompilerOptionDefineBonesModifyQcFileCheckBox);
			this.DefineBonesGroupBox.Controls.Add(this.CompilerOptionDefineBonesOverwriteQciFileCheckBox);
			this.DefineBonesGroupBox.Controls.Add(this.CompilerOptionDefineBonesWriteQciFileCheckBox);
			this.DefineBonesGroupBox.Location = new System.Drawing.Point(173, 3);
			this.DefineBonesGroupBox.Name = "DefineBonesGroupBox";
			this.DefineBonesGroupBox.Size = new System.Drawing.Size(259, 95);
			this.DefineBonesGroupBox.TabIndex = 14;
			this.DefineBonesGroupBox.TabStop = false;
			//
			//CompilerOptionDefineBonesOverwriteQciFileCheckBox
			//
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.AutoSize = true;
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Enabled = false;
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Location = new System.Drawing.Point(19, 45);
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Name = "CompilerOptionDefineBonesOverwriteQciFileCheckBox";
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Size = new System.Drawing.Size(116, 17);
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.TabIndex = 13;
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Text = "Overwrite QCI file";
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompileOptionsSourceEngineUseDefaultsButton
			//
			this.CompileOptionsSourceEngineUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CompileOptionsSourceEngineUseDefaultsButton.Location = new System.Drawing.Point(674, 68);
			this.CompileOptionsSourceEngineUseDefaultsButton.Name = "CompileOptionsSourceEngineUseDefaultsButton";
			this.CompileOptionsSourceEngineUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			this.CompileOptionsSourceEngineUseDefaultsButton.TabIndex = 12;
			this.CompileOptionsSourceEngineUseDefaultsButton.Text = "Use Defaults";
			this.ToolTip1.SetToolTip(this.CompileOptionsSourceEngineUseDefaultsButton, "Set the compiler options back to default settings");
			this.CompileOptionsSourceEngineUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//CompilerOptionsGoldSourceEnginePanel
			//
			this.CompilerOptionsGoldSourceEnginePanel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CompilerOptionsGoldSourceEnginePanel.Controls.Add(this.GoldSourceEngineLogFileCheckBox);
			this.CompilerOptionsGoldSourceEnginePanel.Controls.Add(this.CompileOptionsGoldSourceEngineUseDefaultsButton);
			this.CompilerOptionsGoldSourceEnginePanel.Location = new System.Drawing.Point(0, 24);
			this.CompilerOptionsGoldSourceEnginePanel.Name = "CompilerOptionsGoldSourceEnginePanel";
			this.CompilerOptionsGoldSourceEnginePanel.Size = new System.Drawing.Size(764, 100);
			this.CompilerOptionsGoldSourceEnginePanel.TabIndex = 13;
			//
			//GoldSourceEngineLogFileCheckBox
			//
			this.GoldSourceEngineLogFileCheckBox.AutoSize = true;
			this.GoldSourceEngineLogFileCheckBox.Location = new System.Drawing.Point(6, 3);
			this.GoldSourceEngineLogFileCheckBox.Name = "GoldSourceEngineLogFileCheckBox";
			this.GoldSourceEngineLogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			this.GoldSourceEngineLogFileCheckBox.TabIndex = 14;
			this.GoldSourceEngineLogFileCheckBox.Text = "Write log to a file";
			this.ToolTip1.SetToolTip(this.GoldSourceEngineLogFileCheckBox, "Write compile log to a file (in same folder as QC file).");
			this.GoldSourceEngineLogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompileOptionsGoldSourceEngineUseDefaultsButton
			//
			this.CompileOptionsGoldSourceEngineUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CompileOptionsGoldSourceEngineUseDefaultsButton.Location = new System.Drawing.Point(674, 68);
			this.CompileOptionsGoldSourceEngineUseDefaultsButton.Name = "CompileOptionsGoldSourceEngineUseDefaultsButton";
			this.CompileOptionsGoldSourceEngineUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			this.CompileOptionsGoldSourceEngineUseDefaultsButton.TabIndex = 13;
			this.CompileOptionsGoldSourceEngineUseDefaultsButton.Text = "Use Defaults";
			this.ToolTip1.SetToolTip(this.CompileOptionsGoldSourceEngineUseDefaultsButton, "Set the compiler options back to default settings");
			this.CompileOptionsGoldSourceEngineUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//CompilerOptionsTextBoxMinScrollPanel
			//
			this.CompilerOptionsTextBoxMinScrollPanel.Location = new System.Drawing.Point(0, 169);
			this.CompilerOptionsTextBoxMinScrollPanel.Name = "CompilerOptionsTextBoxMinScrollPanel";
			this.CompilerOptionsTextBoxMinScrollPanel.Size = new System.Drawing.Size(764, 37);
			this.CompilerOptionsTextBoxMinScrollPanel.TabIndex = 41;
			//
			//CompileLogRichTextBox
			//
			this.CompileLogRichTextBox.CueBannerText = "";
			this.CompileLogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CompileLogRichTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.CompileLogRichTextBox.HideSelection = false;
			this.CompileLogRichTextBox.Location = new System.Drawing.Point(0, 26);
			this.CompileLogRichTextBox.Name = "CompileLogRichTextBox";
			this.CompileLogRichTextBox.ReadOnly = true;
			this.CompileLogRichTextBox.Size = new System.Drawing.Size(770, 186);
			this.CompileLogRichTextBox.TabIndex = 0;
			this.CompileLogRichTextBox.Text = "";
			this.CompileLogRichTextBox.WordWrap = false;
			//
			//CompileButtonsPanel
			//
			this.CompileButtonsPanel.Controls.Add(this.CompileButton);
			this.CompileButtonsPanel.Controls.Add(this.SkipCurrentModelButton);
			this.CompileButtonsPanel.Controls.Add(this.CancelCompileButton);
			this.CompileButtonsPanel.Controls.Add(this.UseAllInPackButton);
			this.CompileButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.CompileButtonsPanel.Location = new System.Drawing.Point(0, 0);
			this.CompileButtonsPanel.Name = "CompileButtonsPanel";
			this.CompileButtonsPanel.Size = new System.Drawing.Size(770, 26);
			this.CompileButtonsPanel.TabIndex = 39;
			//
			//SkipCurrentModelButton
			//
			this.SkipCurrentModelButton.Enabled = false;
			this.SkipCurrentModelButton.Location = new System.Drawing.Point(131, 0);
			this.SkipCurrentModelButton.Name = "SkipCurrentModelButton";
			this.SkipCurrentModelButton.Size = new System.Drawing.Size(120, 23);
			this.SkipCurrentModelButton.TabIndex = 2;
			this.SkipCurrentModelButton.Text = "Skip Current Model";
			this.SkipCurrentModelButton.UseVisualStyleBackColor = true;
			//
			//CancelCompileButton
			//
			this.CancelCompileButton.Enabled = false;
			this.CancelCompileButton.Location = new System.Drawing.Point(257, 0);
			this.CancelCompileButton.Name = "CancelCompileButton";
			this.CancelCompileButton.Size = new System.Drawing.Size(120, 23);
			this.CancelCompileButton.TabIndex = 3;
			this.CancelCompileButton.Text = "Cancel Compile";
			this.CancelCompileButton.UseVisualStyleBackColor = true;
			//
			//UseAllInPackButton
			//
			this.UseAllInPackButton.Enabled = false;
			this.UseAllInPackButton.Location = new System.Drawing.Point(383, 0);
			this.UseAllInPackButton.Name = "UseAllInPackButton";
			this.UseAllInPackButton.Size = new System.Drawing.Size(120, 23);
			this.UseAllInPackButton.TabIndex = 4;
			this.UseAllInPackButton.Text = "Use All in Pack";
			this.UseAllInPackButton.UseVisualStyleBackColor = true;
			this.UseAllInPackButton.Visible = false;
			//
			//PostCompilePanel
			//
			this.PostCompilePanel.Controls.Add(this.CompiledFilesComboBox);
			this.PostCompilePanel.Controls.Add(this.UseInViewButton);
			this.PostCompilePanel.Controls.Add(this.RecompileButton);
			this.PostCompilePanel.Controls.Add(this.UseInPackButton);
			this.PostCompilePanel.Controls.Add(this.GotoCompiledMdlButton);
			this.PostCompilePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PostCompilePanel.Location = new System.Drawing.Point(0, 212);
			this.PostCompilePanel.Name = "PostCompilePanel";
			this.PostCompilePanel.Size = new System.Drawing.Size(770, 26);
			this.PostCompilePanel.TabIndex = 40;
			//
			//CompiledFilesComboBox
			//
			this.CompiledFilesComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CompiledFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CompiledFilesComboBox.FormattingEnabled = true;
			this.CompiledFilesComboBox.Location = new System.Drawing.Point(0, 4);
			this.CompiledFilesComboBox.Name = "CompiledFilesComboBox";
			this.CompiledFilesComboBox.Size = new System.Drawing.Size(559, 21);
			this.CompiledFilesComboBox.TabIndex = 1;
			//
			//UseInViewButton
			//
			this.UseInViewButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseInViewButton.Enabled = false;
			this.UseInViewButton.Location = new System.Drawing.Point(565, 3);
			this.UseInViewButton.Name = "UseInViewButton";
			this.UseInViewButton.Size = new System.Drawing.Size(75, 23);
			this.UseInViewButton.TabIndex = 2;
			this.UseInViewButton.Text = "Use in View";
			this.UseInViewButton.UseVisualStyleBackColor = true;
			//
			//RecompileButton
			//
			this.RecompileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.RecompileButton.Enabled = false;
			this.RecompileButton.Location = new System.Drawing.Point(646, 3);
			this.RecompileButton.Name = "RecompileButton";
			this.RecompileButton.Size = new System.Drawing.Size(75, 23);
			this.RecompileButton.TabIndex = 5;
			this.RecompileButton.Text = "Recompile";
			this.RecompileButton.UseVisualStyleBackColor = true;
			//
			//UseInPackButton
			//
			this.UseInPackButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseInPackButton.Enabled = false;
			this.UseInPackButton.Location = new System.Drawing.Point(646, 3);
			this.UseInPackButton.Name = "UseInPackButton";
			this.UseInPackButton.Size = new System.Drawing.Size(75, 23);
			this.UseInPackButton.TabIndex = 3;
			this.UseInPackButton.Text = "Use in Pack";
			this.UseInPackButton.UseVisualStyleBackColor = true;
			this.UseInPackButton.Visible = false;
			//
			//GotoCompiledMdlButton
			//
			this.GotoCompiledMdlButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoCompiledMdlButton.Location = new System.Drawing.Point(727, 3);
			this.GotoCompiledMdlButton.Name = "GotoCompiledMdlButton";
			this.GotoCompiledMdlButton.Size = new System.Drawing.Size(43, 23);
			this.GotoCompiledMdlButton.TabIndex = 4;
			this.GotoCompiledMdlButton.Text = "Goto";
			this.GotoCompiledMdlButton.UseVisualStyleBackColor = true;
			//
			//UseDefaultOutputSubfolderButton
			//
			this.UseDefaultOutputSubfolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseDefaultOutputSubfolderButton.Location = new System.Drawing.Point(660, 32);
			this.UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton";
			this.UseDefaultOutputSubfolderButton.Size = new System.Drawing.Size(113, 23);
			this.UseDefaultOutputSubfolderButton.TabIndex = 12;
			this.UseDefaultOutputSubfolderButton.Text = "Use Default";
			this.UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = true;
			//
			//CompileUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.Controls.Add(this.Panel1);
			this.Name = "CompileUserControl";
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
			this.GameSetupPanel.ResumeLayout(false);
			this.CompilerOptionsSourceEnginePanel.ResumeLayout(false);
			this.CompilerOptionsSourceEnginePanel.PerformLayout();
			this.DefineBonesGroupBox.ResumeLayout(false);
			this.DefineBonesGroupBox.PerformLayout();
			this.CompilerOptionsGoldSourceEnginePanel.ResumeLayout(false);
			this.CompilerOptionsGoldSourceEnginePanel.PerformLayout();
			this.CompileButtonsPanel.ResumeLayout(false);
			this.PostCompilePanel.ResumeLayout(false);
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			this.Load += new System.EventHandler(CompileUserControl_Load);
			BrowseForQcPathFolderOrFileNameButton.Click += new System.EventHandler(BrowseForQcPathFolderOrFileNameButton_Click);
			GotoQcButton.Click += new System.EventHandler(GotoQcButton_Click);
			OutputPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragDrop);
			OutputPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragEnter);
			OutputPathTextBox.Validated += new System.EventHandler(OutputPathTextBox_Validated);
			BrowseForOutputPathButton.Click += new System.EventHandler(BrowseForOutputPathButton_Click);
			GotoOutputPathButton.Click += new System.EventHandler(GotoOutputPathButton_Click);
			UseDefaultOutputSubfolderButton.Click += new System.EventHandler(UseDefaultOutputSubfolderButton_Click);
			CompileOptionsSourceEngineUseDefaultsButton.Click += new System.EventHandler(CompileOptionsUseDefaultsButton_Click);
			DirectCompilerOptionsTextBox.TextChanged += new System.EventHandler(DirectCompilerOptionsTextBox_TextChanged);
			CompileButton.Click += new System.EventHandler(CompileButton_Click);
			SkipCurrentModelButton.Click += new System.EventHandler(SkipCurrentModelButton_Click);
			CancelCompileButton.Click += new System.EventHandler(CancelCompileButton_Click);
			UseAllInPackButton.Click += new System.EventHandler(UseAllInPackButton_Click);
			UseInViewButton.Click += new System.EventHandler(UseInViewButton_Click);
			RecompileButton.Click += new System.EventHandler(RecompileButton_Click);
			UseInPackButton.Click += new System.EventHandler(UseInPackButton_Click);
			GotoCompiledMdlButton.Click += new System.EventHandler(GotoCompiledMdlButton_Click);
		}
		internal System.Windows.Forms.TextBox CompilerOptionsTextBox;
		internal System.Windows.Forms.ComboBox GameSetupComboBox;
		internal System.Windows.Forms.Button BrowseForQcPathFolderOrFileNameButton;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Button EditGameSetupButton;
		internal System.Windows.Forms.Label GameSetupLabel;
		internal System.Windows.Forms.Button CompileButton;
		internal System.Windows.Forms.CheckBox CompilerOptionNoP4CheckBox;
		internal System.Windows.Forms.CheckBox CompilerOptionVerboseCheckBox;
		internal System.Windows.Forms.Label DirectCompilerOptionsLabel;
		internal System.Windows.Forms.TextBox DirectCompilerOptionsTextBox;
		internal System.Windows.Forms.SplitContainer Options_LogSplitContainer;
		internal Crowbar.RichTextBoxEx CompileLogRichTextBox;
		internal System.Windows.Forms.Button CancelCompileButton;
		internal System.Windows.Forms.Button SkipCurrentModelButton;
		internal System.Windows.Forms.ComboBox CompileComboBox;
		internal System.Windows.Forms.Button RecompileButton;
		internal System.Windows.Forms.ComboBox CompiledFilesComboBox;
		internal System.Windows.Forms.Button GotoQcButton;
		internal System.Windows.Forms.Button GotoCompiledMdlButton;
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.CheckBox SourceEngineLogFileCheckBox;
		internal System.Windows.Forms.GroupBox OptionsGroupBox;
		internal System.Windows.Forms.Panel OptionsGroupBoxFillPanel;
		internal System.Windows.Forms.Button UseInViewButton;
		internal System.Windows.Forms.Button UseInPackButton;
		internal System.Windows.Forms.Button UseAllInPackButton;
		internal System.Windows.Forms.CheckBox FolderForEachModelCheckBox;
		internal System.Windows.Forms.TextBox CompilerOptionDefineBonesFileNameTextBox;
		internal System.Windows.Forms.CheckBox CompilerOptionDefineBonesCheckBox;
		internal System.Windows.Forms.CheckBox CompilerOptionDefineBonesWriteQciFileCheckBox;
		internal System.Windows.Forms.CheckBox CompilerOptionDefineBonesModifyQcFileCheckBox;
		internal System.Windows.Forms.ToolTip ToolTip1;
		internal System.Windows.Forms.Button CompileOptionsSourceEngineUseDefaultsButton;
		internal System.Windows.Forms.Button GotoOutputPathButton;
		internal System.Windows.Forms.Button BrowseForOutputPathButton;
		internal System.Windows.Forms.Button UseDefaultOutputSubfolderButton;
		internal Crowbar.TextBoxEx OutputPathTextBox;
		internal System.Windows.Forms.ComboBox OutputPathComboBox;
		internal System.Windows.Forms.Label Label1;
		internal Crowbar.TextBoxEx GameModelsOutputPathTextBox;
		internal System.Windows.Forms.Panel CompilerOptionsSourceEnginePanel;
		internal System.Windows.Forms.Panel CompilerOptionsGoldSourceEnginePanel;
		internal System.Windows.Forms.CheckBox GoldSourceEngineLogFileCheckBox;
		internal System.Windows.Forms.Button CompileOptionsGoldSourceEngineUseDefaultsButton;
		internal Crowbar.TextBoxEx OutputSubfolderTextBox;
		internal Crowbar.TextBoxEx QcPathFileNameTextBox;
		internal Panel GameSetupPanel;
		internal Panel CompileButtonsPanel;
		internal Panel CompilerOptionsTextBoxMinScrollPanel;
		internal Panel PostCompilePanel;
		internal GroupBox DefineBonesGroupBox;
		internal CheckBox CompilerOptionDefineBonesOverwriteQciFileCheckBox;
	}

}