using System.IO;
using System.Text;

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
			components = new System.ComponentModel.Container();
			CompilerOptionsTextBox = new System.Windows.Forms.TextBox();
			GameSetupComboBox = new System.Windows.Forms.ComboBox();
			FolderForEachModelCheckBox = new System.Windows.Forms.CheckBox();
			SourceEngineLogFileCheckBox = new System.Windows.Forms.CheckBox();
			CompilerOptionDefineBonesCheckBox = new System.Windows.Forms.CheckBox();
			CompilerOptionNoP4CheckBox = new System.Windows.Forms.CheckBox();
			CompilerOptionVerboseCheckBox = new System.Windows.Forms.CheckBox();
			CompilerOptionDefineBonesModifyQcFileCheckBox = new System.Windows.Forms.CheckBox();
			CompilerOptionDefineBonesWriteQciFileCheckBox = new System.Windows.Forms.CheckBox();
			CompilerOptionDefineBonesFileNameTextBox = new System.Windows.Forms.TextBox();
			DirectCompilerOptionsLabel = new System.Windows.Forms.Label();
			DirectCompilerOptionsTextBox = new System.Windows.Forms.TextBox();
			BrowseForQcPathFolderOrFileNameButton = new System.Windows.Forms.Button();
			Label6 = new System.Windows.Forms.Label();
			EditGameSetupButton = new System.Windows.Forms.Button();
			GameSetupLabel = new System.Windows.Forms.Label();
			CompileButton = new System.Windows.Forms.Button();
			Panel1 = new System.Windows.Forms.Panel();
			QcPathFileNameTextBox = new Crowbar.TextBoxEx();
			OutputPathTextBox = new Crowbar.TextBoxEx();
			GameModelsOutputPathTextBox = new Crowbar.TextBoxEx();
			OutputSubfolderTextBox = new Crowbar.TextBoxEx();
			GotoOutputPathButton = new System.Windows.Forms.Button();
			BrowseForOutputPathButton = new System.Windows.Forms.Button();
			OutputPathComboBox = new System.Windows.Forms.ComboBox();
			CompileComboBox = new System.Windows.Forms.ComboBox();
			Label1 = new System.Windows.Forms.Label();
			GotoQcButton = new System.Windows.Forms.Button();
			Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			OptionsGroupBox = new System.Windows.Forms.GroupBox();
			OptionsGroupBoxFillPanel = new System.Windows.Forms.Panel();
			GameSetupPanel = new System.Windows.Forms.Panel();
			CompilerOptionsSourceEnginePanel = new System.Windows.Forms.Panel();
			DefineBonesGroupBox = new System.Windows.Forms.GroupBox();
			CompilerOptionDefineBonesOverwriteQciFileCheckBox = new System.Windows.Forms.CheckBox();
			CompileOptionsSourceEngineUseDefaultsButton = new System.Windows.Forms.Button();
			CompilerOptionsGoldSourceEnginePanel = new System.Windows.Forms.Panel();
			GoldSourceEngineLogFileCheckBox = new System.Windows.Forms.CheckBox();
			CompileOptionsGoldSourceEngineUseDefaultsButton = new System.Windows.Forms.Button();
			CompilerOptionsTextBoxMinScrollPanel = new System.Windows.Forms.Panel();
			CompileLogRichTextBox = new Crowbar.RichTextBoxEx();
			CompileButtonsPanel = new System.Windows.Forms.Panel();
			SkipCurrentModelButton = new System.Windows.Forms.Button();
			CancelCompileButton = new System.Windows.Forms.Button();
			UseAllInPackButton = new System.Windows.Forms.Button();
			PostCompilePanel = new System.Windows.Forms.Panel();
			CompiledFilesComboBox = new System.Windows.Forms.ComboBox();
			UseInViewButton = new System.Windows.Forms.Button();
			RecompileButton = new System.Windows.Forms.Button();
			UseInPackButton = new System.Windows.Forms.Button();
			GotoCompiledMdlButton = new System.Windows.Forms.Button();
			UseDefaultOutputSubfolderButton = new System.Windows.Forms.Button();
			ToolTip1 = new System.Windows.Forms.ToolTip(components);
			Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).BeginInit();
			Options_LogSplitContainer.Panel1.SuspendLayout();
			Options_LogSplitContainer.Panel2.SuspendLayout();
			Options_LogSplitContainer.SuspendLayout();
			OptionsGroupBox.SuspendLayout();
			OptionsGroupBoxFillPanel.SuspendLayout();
			GameSetupPanel.SuspendLayout();
			CompilerOptionsSourceEnginePanel.SuspendLayout();
			DefineBonesGroupBox.SuspendLayout();
			CompilerOptionsGoldSourceEnginePanel.SuspendLayout();
			CompileButtonsPanel.SuspendLayout();
			PostCompilePanel.SuspendLayout();
			SuspendLayout();
			//
			//CompilerOptionsTextBox
			//
			CompilerOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			CompilerOptionsTextBox.Location = new System.Drawing.Point(0, 169);
			CompilerOptionsTextBox.Multiline = true;
			CompilerOptionsTextBox.Name = "CompilerOptionsTextBox";
			CompilerOptionsTextBox.ReadOnly = true;
			CompilerOptionsTextBox.Size = new System.Drawing.Size(764, 37);
			CompilerOptionsTextBox.TabIndex = 15;
			//
			//GameSetupComboBox
			//
			GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			GameSetupComboBox.FormattingEnabled = true;
			GameSetupComboBox.Location = new System.Drawing.Point(192, 1);
			GameSetupComboBox.Name = "GameSetupComboBox";
			GameSetupComboBox.Size = new System.Drawing.Size(476, 21);
			GameSetupComboBox.TabIndex = 1;
			//
			//FolderForEachModelCheckBox
			//
			FolderForEachModelCheckBox.AutoSize = true;
			FolderForEachModelCheckBox.Location = new System.Drawing.Point(502, 74);
			FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox";
			FolderForEachModelCheckBox.Size = new System.Drawing.Size(139, 17);
			FolderForEachModelCheckBox.TabIndex = 3;
			FolderForEachModelCheckBox.Text = "Folder for each model";
			FolderForEachModelCheckBox.UseVisualStyleBackColor = true;
			FolderForEachModelCheckBox.Visible = false;
			//
			//SourceEngineLogFileCheckBox
			//
			SourceEngineLogFileCheckBox.AutoSize = true;
			SourceEngineLogFileCheckBox.Location = new System.Drawing.Point(6, 3);
			SourceEngineLogFileCheckBox.Name = "SourceEngineLogFileCheckBox";
			SourceEngineLogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			SourceEngineLogFileCheckBox.TabIndex = 4;
			SourceEngineLogFileCheckBox.Text = "Write log to a file";
			ToolTip1.SetToolTip(SourceEngineLogFileCheckBox, "Write compile log to a file (in same folder as QC file).");
			SourceEngineLogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionDefineBonesCheckBox
			//
			CompilerOptionDefineBonesCheckBox.AutoSize = true;
			CompilerOptionDefineBonesCheckBox.Location = new System.Drawing.Point(179, 4);
			CompilerOptionDefineBonesCheckBox.Name = "CompilerOptionDefineBonesCheckBox";
			CompilerOptionDefineBonesCheckBox.Size = new System.Drawing.Size(91, 17);
			CompilerOptionDefineBonesCheckBox.TabIndex = 7;
			CompilerOptionDefineBonesCheckBox.Text = "DefineBones";
			CompilerOptionDefineBonesCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionNoP4CheckBox
			//
			CompilerOptionNoP4CheckBox.AutoSize = true;
			CompilerOptionNoP4CheckBox.Location = new System.Drawing.Point(6, 26);
			CompilerOptionNoP4CheckBox.Name = "CompilerOptionNoP4CheckBox";
			CompilerOptionNoP4CheckBox.Size = new System.Drawing.Size(56, 17);
			CompilerOptionNoP4CheckBox.TabIndex = 5;
			CompilerOptionNoP4CheckBox.Text = "No P4";
			ToolTip1.SetToolTip(CompilerOptionNoP4CheckBox, "No Perforce integration (modders do not usually have Perforce software).");
			CompilerOptionNoP4CheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionVerboseCheckBox
			//
			CompilerOptionVerboseCheckBox.AutoSize = true;
			CompilerOptionVerboseCheckBox.Location = new System.Drawing.Point(6, 49);
			CompilerOptionVerboseCheckBox.Name = "CompilerOptionVerboseCheckBox";
			CompilerOptionVerboseCheckBox.Size = new System.Drawing.Size(67, 17);
			CompilerOptionVerboseCheckBox.TabIndex = 6;
			CompilerOptionVerboseCheckBox.Text = "Verbose";
			ToolTip1.SetToolTip(CompilerOptionVerboseCheckBox, "Write more info in compile log.");
			CompilerOptionVerboseCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionDefineBonesModifyQcFileCheckBox
			//
			CompilerOptionDefineBonesModifyQcFileCheckBox.AutoSize = true;
			CompilerOptionDefineBonesModifyQcFileCheckBox.Enabled = false;
			CompilerOptionDefineBonesModifyQcFileCheckBox.Location = new System.Drawing.Point(19, 65);
			CompilerOptionDefineBonesModifyQcFileCheckBox.Name = "CompilerOptionDefineBonesModifyQcFileCheckBox";
			CompilerOptionDefineBonesModifyQcFileCheckBox.Size = new System.Drawing.Size(238, 17);
			CompilerOptionDefineBonesModifyQcFileCheckBox.TabIndex = 11;
			CompilerOptionDefineBonesModifyQcFileCheckBox.Text = "Put in QC file: $include \"<QCI file name>\"";
			CompilerOptionDefineBonesModifyQcFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionDefineBonesWriteQciFileCheckBox
			//
			CompilerOptionDefineBonesWriteQciFileCheckBox.AutoSize = true;
			CompilerOptionDefineBonesWriteQciFileCheckBox.Enabled = false;
			CompilerOptionDefineBonesWriteQciFileCheckBox.Location = new System.Drawing.Point(6, 22);
			CompilerOptionDefineBonesWriteQciFileCheckBox.Name = "CompilerOptionDefineBonesWriteQciFileCheckBox";
			CompilerOptionDefineBonesWriteQciFileCheckBox.Size = new System.Drawing.Size(97, 17);
			CompilerOptionDefineBonesWriteQciFileCheckBox.TabIndex = 8;
			CompilerOptionDefineBonesWriteQciFileCheckBox.Text = "Write QCI file:";
			CompilerOptionDefineBonesWriteQciFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompilerOptionDefineBonesFileNameTextBox
			//
			CompilerOptionDefineBonesFileNameTextBox.Enabled = false;
			CompilerOptionDefineBonesFileNameTextBox.Location = new System.Drawing.Point(109, 18);
			CompilerOptionDefineBonesFileNameTextBox.Name = "CompilerOptionDefineBonesFileNameTextBox";
			CompilerOptionDefineBonesFileNameTextBox.Size = new System.Drawing.Size(140, 22);
			CompilerOptionDefineBonesFileNameTextBox.TabIndex = 10;
			//
			//DirectCompilerOptionsLabel
			//
			DirectCompilerOptionsLabel.Location = new System.Drawing.Point(0, 125);
			DirectCompilerOptionsLabel.Name = "DirectCompilerOptionsLabel";
			DirectCompilerOptionsLabel.Size = new System.Drawing.Size(764, 13);
			DirectCompilerOptionsLabel.TabIndex = 13;
			DirectCompilerOptionsLabel.Text = "Direct entry of command-line options (in case they are not included above):";
			//
			//DirectCompilerOptionsTextBox
			//
			DirectCompilerOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			DirectCompilerOptionsTextBox.Location = new System.Drawing.Point(0, 141);
			DirectCompilerOptionsTextBox.Name = "DirectCompilerOptionsTextBox";
			DirectCompilerOptionsTextBox.Size = new System.Drawing.Size(764, 22);
			DirectCompilerOptionsTextBox.TabIndex = 14;
			//
			//BrowseForQcPathFolderOrFileNameButton
			//
			BrowseForQcPathFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForQcPathFolderOrFileNameButton.Location = new System.Drawing.Point(660, 3);
			BrowseForQcPathFolderOrFileNameButton.Name = "BrowseForQcPathFolderOrFileNameButton";
			BrowseForQcPathFolderOrFileNameButton.Size = new System.Drawing.Size(64, 23);
			BrowseForQcPathFolderOrFileNameButton.TabIndex = 3;
			BrowseForQcPathFolderOrFileNameButton.Text = "Browse...";
			BrowseForQcPathFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//Label6
			//
			Label6.Location = new System.Drawing.Point(3, 8);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(57, 13);
			Label6.TabIndex = 0;
			Label6.Text = "QC input:";
			//
			//EditGameSetupButton
			//
			EditGameSetupButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			EditGameSetupButton.Location = new System.Drawing.Point(674, 0);
			EditGameSetupButton.Name = "EditGameSetupButton";
			EditGameSetupButton.Size = new System.Drawing.Size(90, 23);
			EditGameSetupButton.TabIndex = 2;
			EditGameSetupButton.Text = "Set Up Games";
			EditGameSetupButton.UseVisualStyleBackColor = true;
			//
			//GameSetupLabel
			//
			GameSetupLabel.Location = new System.Drawing.Point(0, 5);
			GameSetupLabel.Name = "GameSetupLabel";
			GameSetupLabel.Size = new System.Drawing.Size(186, 13);
			GameSetupLabel.TabIndex = 0;
			GameSetupLabel.Text = "Game that has the model compiler:";
			//
			//CompileButton
			//
			CompileButton.Location = new System.Drawing.Point(0, 0);
			CompileButton.Name = "CompileButton";
			CompileButton.Size = new System.Drawing.Size(125, 23);
			CompileButton.TabIndex = 1;
			CompileButton.Text = "&Compile DefineBones";
			CompileButton.UseVisualStyleBackColor = true;
			//
			//Panel1
			//
			Panel1.Controls.Add(QcPathFileNameTextBox);
			Panel1.Controls.Add(OutputPathTextBox);
			Panel1.Controls.Add(GameModelsOutputPathTextBox);
			Panel1.Controls.Add(OutputSubfolderTextBox);
			Panel1.Controls.Add(GotoOutputPathButton);
			Panel1.Controls.Add(BrowseForOutputPathButton);
			Panel1.Controls.Add(OutputPathComboBox);
			Panel1.Controls.Add(CompileComboBox);
			Panel1.Controls.Add(Label1);
			Panel1.Controls.Add(GotoQcButton);
			Panel1.Controls.Add(Label6);
			Panel1.Controls.Add(BrowseForQcPathFolderOrFileNameButton);
			Panel1.Controls.Add(Options_LogSplitContainer);
			Panel1.Controls.Add(UseDefaultOutputSubfolderButton);
			Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel1.Location = new System.Drawing.Point(0, 0);
			Panel1.Margin = new System.Windows.Forms.Padding(2);
			Panel1.Name = "Panel1";
			Panel1.Size = new System.Drawing.Size(776, 536);
			Panel1.TabIndex = 15;
			//
			//QcPathFileNameTextBox
			//
			QcPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			QcPathFileNameTextBox.CueBannerText = "";
			QcPathFileNameTextBox.Location = new System.Drawing.Point(209, 3);
			QcPathFileNameTextBox.Name = "QcPathFileNameTextBox";
			QcPathFileNameTextBox.Size = new System.Drawing.Size(445, 22);
			QcPathFileNameTextBox.TabIndex = 22;
			//
			//OutputPathTextBox
			//
			OutputPathTextBox.AllowDrop = true;
			OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputPathTextBox.CueBannerText = "";
			OutputPathTextBox.Location = new System.Drawing.Point(209, 32);
			OutputPathTextBox.Name = "OutputPathTextBox";
			OutputPathTextBox.Size = new System.Drawing.Size(445, 22);
			OutputPathTextBox.TabIndex = 9;
			//
			//GameModelsOutputPathTextBox
			//
			GameModelsOutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameModelsOutputPathTextBox.CueBannerText = "";
			GameModelsOutputPathTextBox.Location = new System.Drawing.Point(209, 32);
			GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox";
			GameModelsOutputPathTextBox.ReadOnly = true;
			GameModelsOutputPathTextBox.Size = new System.Drawing.Size(445, 22);
			GameModelsOutputPathTextBox.TabIndex = 8;
			//
			//OutputSubfolderTextBox
			//
			OutputSubfolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputSubfolderTextBox.CueBannerText = "";
			OutputSubfolderTextBox.Location = new System.Drawing.Point(209, 32);
			OutputSubfolderTextBox.Name = "OutputSubfolderTextBox";
			OutputSubfolderTextBox.Size = new System.Drawing.Size(445, 22);
			OutputSubfolderTextBox.TabIndex = 21;
			OutputSubfolderTextBox.Visible = false;
			//
			//GotoOutputPathButton
			//
			GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			GotoOutputPathButton.Name = "GotoOutputPathButton";
			GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			GotoOutputPathButton.TabIndex = 11;
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
			BrowseForOutputPathButton.TabIndex = 10;
			BrowseForOutputPathButton.Text = "Browse...";
			BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OutputPathComboBox
			//
			OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			OutputPathComboBox.FormattingEnabled = true;
			OutputPathComboBox.Location = new System.Drawing.Point(63, 33);
			OutputPathComboBox.Name = "OutputPathComboBox";
			OutputPathComboBox.Size = new System.Drawing.Size(140, 21);
			OutputPathComboBox.TabIndex = 6;
			//
			//CompileComboBox
			//
			CompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			CompileComboBox.FormattingEnabled = true;
			CompileComboBox.Location = new System.Drawing.Point(63, 4);
			CompileComboBox.Name = "CompileComboBox";
			CompileComboBox.Size = new System.Drawing.Size(140, 21);
			CompileComboBox.TabIndex = 1;
			//
			//Label1
			//
			Label1.Location = new System.Drawing.Point(3, 37);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(62, 13);
			Label1.TabIndex = 5;
			Label1.Text = "Output to:";
			//
			//GotoQcButton
			//
			GotoQcButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoQcButton.Location = new System.Drawing.Point(730, 3);
			GotoQcButton.Name = "GotoQcButton";
			GotoQcButton.Size = new System.Drawing.Size(43, 23);
			GotoQcButton.TabIndex = 4;
			GotoQcButton.Text = "Goto";
			GotoQcButton.UseVisualStyleBackColor = true;
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
			Options_LogSplitContainer.Panel2.Controls.Add(CompileLogRichTextBox);
			Options_LogSplitContainer.Panel2.Controls.Add(CompileButtonsPanel);
			Options_LogSplitContainer.Panel2.Controls.Add(PostCompilePanel);
			Options_LogSplitContainer.Panel2MinSize = 45;
			Options_LogSplitContainer.Size = new System.Drawing.Size(770, 472);
			Options_LogSplitContainer.SplitterDistance = 230;
			Options_LogSplitContainer.TabIndex = 16;
			//
			//OptionsGroupBox
			//
			OptionsGroupBox.Controls.Add(OptionsGroupBoxFillPanel);
			OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			OptionsGroupBox.Name = "OptionsGroupBox";
			OptionsGroupBox.Size = new System.Drawing.Size(770, 230);
			OptionsGroupBox.TabIndex = 0;
			OptionsGroupBox.TabStop = false;
			OptionsGroupBox.Text = "Options";
			//
			//OptionsGroupBoxFillPanel
			//
			OptionsGroupBoxFillPanel.AutoScroll = true;
			OptionsGroupBoxFillPanel.Controls.Add(GameSetupPanel);
			OptionsGroupBoxFillPanel.Controls.Add(CompilerOptionsSourceEnginePanel);
			OptionsGroupBoxFillPanel.Controls.Add(CompilerOptionsGoldSourceEnginePanel);
			OptionsGroupBoxFillPanel.Controls.Add(DirectCompilerOptionsLabel);
			OptionsGroupBoxFillPanel.Controls.Add(DirectCompilerOptionsTextBox);
			OptionsGroupBoxFillPanel.Controls.Add(CompilerOptionsTextBox);
			OptionsGroupBoxFillPanel.Controls.Add(CompilerOptionsTextBoxMinScrollPanel);
			OptionsGroupBoxFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsGroupBoxFillPanel.Location = new System.Drawing.Point(3, 18);
			OptionsGroupBoxFillPanel.Name = "OptionsGroupBoxFillPanel";
			OptionsGroupBoxFillPanel.Size = new System.Drawing.Size(764, 209);
			OptionsGroupBoxFillPanel.TabIndex = 0;
			//
			//GameSetupPanel
			//
			GameSetupPanel.Controls.Add(GameSetupLabel);
			GameSetupPanel.Controls.Add(GameSetupComboBox);
			GameSetupPanel.Controls.Add(EditGameSetupButton);
			GameSetupPanel.Dock = System.Windows.Forms.DockStyle.Top;
			GameSetupPanel.Location = new System.Drawing.Point(0, 0);
			GameSetupPanel.Name = "GameSetupPanel";
			GameSetupPanel.Size = new System.Drawing.Size(764, 26);
			GameSetupPanel.TabIndex = 40;
			//
			//CompilerOptionsSourceEnginePanel
			//
			CompilerOptionsSourceEnginePanel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			CompilerOptionsSourceEnginePanel.Controls.Add(CompilerOptionDefineBonesCheckBox);
			CompilerOptionsSourceEnginePanel.Controls.Add(DefineBonesGroupBox);
			CompilerOptionsSourceEnginePanel.Controls.Add(SourceEngineLogFileCheckBox);
			CompilerOptionsSourceEnginePanel.Controls.Add(CompilerOptionVerboseCheckBox);
			CompilerOptionsSourceEnginePanel.Controls.Add(CompilerOptionNoP4CheckBox);
			CompilerOptionsSourceEnginePanel.Controls.Add(FolderForEachModelCheckBox);
			CompilerOptionsSourceEnginePanel.Controls.Add(CompileOptionsSourceEngineUseDefaultsButton);
			CompilerOptionsSourceEnginePanel.Location = new System.Drawing.Point(0, 24);
			CompilerOptionsSourceEnginePanel.Name = "CompilerOptionsSourceEnginePanel";
			CompilerOptionsSourceEnginePanel.Size = new System.Drawing.Size(764, 100);
			CompilerOptionsSourceEnginePanel.TabIndex = 38;
			//
			//DefineBonesGroupBox
			//
			DefineBonesGroupBox.Controls.Add(CompilerOptionDefineBonesFileNameTextBox);
			DefineBonesGroupBox.Controls.Add(CompilerOptionDefineBonesModifyQcFileCheckBox);
			DefineBonesGroupBox.Controls.Add(CompilerOptionDefineBonesOverwriteQciFileCheckBox);
			DefineBonesGroupBox.Controls.Add(CompilerOptionDefineBonesWriteQciFileCheckBox);
			DefineBonesGroupBox.Location = new System.Drawing.Point(173, 3);
			DefineBonesGroupBox.Name = "DefineBonesGroupBox";
			DefineBonesGroupBox.Size = new System.Drawing.Size(259, 95);
			DefineBonesGroupBox.TabIndex = 14;
			DefineBonesGroupBox.TabStop = false;
			//
			//CompilerOptionDefineBonesOverwriteQciFileCheckBox
			//
			CompilerOptionDefineBonesOverwriteQciFileCheckBox.AutoSize = true;
			CompilerOptionDefineBonesOverwriteQciFileCheckBox.Enabled = false;
			CompilerOptionDefineBonesOverwriteQciFileCheckBox.Location = new System.Drawing.Point(19, 45);
			CompilerOptionDefineBonesOverwriteQciFileCheckBox.Name = "CompilerOptionDefineBonesOverwriteQciFileCheckBox";
			CompilerOptionDefineBonesOverwriteQciFileCheckBox.Size = new System.Drawing.Size(116, 17);
			CompilerOptionDefineBonesOverwriteQciFileCheckBox.TabIndex = 13;
			CompilerOptionDefineBonesOverwriteQciFileCheckBox.Text = "Overwrite QCI file";
			CompilerOptionDefineBonesOverwriteQciFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompileOptionsSourceEngineUseDefaultsButton
			//
			CompileOptionsSourceEngineUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			CompileOptionsSourceEngineUseDefaultsButton.Location = new System.Drawing.Point(674, 68);
			CompileOptionsSourceEngineUseDefaultsButton.Name = "CompileOptionsSourceEngineUseDefaultsButton";
			CompileOptionsSourceEngineUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			CompileOptionsSourceEngineUseDefaultsButton.TabIndex = 12;
			CompileOptionsSourceEngineUseDefaultsButton.Text = "Use Defaults";
			ToolTip1.SetToolTip(CompileOptionsSourceEngineUseDefaultsButton, "Set the compiler options back to default settings");
			CompileOptionsSourceEngineUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//CompilerOptionsGoldSourceEnginePanel
			//
			CompilerOptionsGoldSourceEnginePanel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			CompilerOptionsGoldSourceEnginePanel.Controls.Add(GoldSourceEngineLogFileCheckBox);
			CompilerOptionsGoldSourceEnginePanel.Controls.Add(CompileOptionsGoldSourceEngineUseDefaultsButton);
			CompilerOptionsGoldSourceEnginePanel.Location = new System.Drawing.Point(0, 24);
			CompilerOptionsGoldSourceEnginePanel.Name = "CompilerOptionsGoldSourceEnginePanel";
			CompilerOptionsGoldSourceEnginePanel.Size = new System.Drawing.Size(764, 100);
			CompilerOptionsGoldSourceEnginePanel.TabIndex = 13;
			//
			//GoldSourceEngineLogFileCheckBox
			//
			GoldSourceEngineLogFileCheckBox.AutoSize = true;
			GoldSourceEngineLogFileCheckBox.Location = new System.Drawing.Point(6, 3);
			GoldSourceEngineLogFileCheckBox.Name = "GoldSourceEngineLogFileCheckBox";
			GoldSourceEngineLogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			GoldSourceEngineLogFileCheckBox.TabIndex = 14;
			GoldSourceEngineLogFileCheckBox.Text = "Write log to a file";
			ToolTip1.SetToolTip(GoldSourceEngineLogFileCheckBox, "Write compile log to a file (in same folder as QC file).");
			GoldSourceEngineLogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//CompileOptionsGoldSourceEngineUseDefaultsButton
			//
			CompileOptionsGoldSourceEngineUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			CompileOptionsGoldSourceEngineUseDefaultsButton.Location = new System.Drawing.Point(674, 68);
			CompileOptionsGoldSourceEngineUseDefaultsButton.Name = "CompileOptionsGoldSourceEngineUseDefaultsButton";
			CompileOptionsGoldSourceEngineUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			CompileOptionsGoldSourceEngineUseDefaultsButton.TabIndex = 13;
			CompileOptionsGoldSourceEngineUseDefaultsButton.Text = "Use Defaults";
			ToolTip1.SetToolTip(CompileOptionsGoldSourceEngineUseDefaultsButton, "Set the compiler options back to default settings");
			CompileOptionsGoldSourceEngineUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//CompilerOptionsTextBoxMinScrollPanel
			//
			CompilerOptionsTextBoxMinScrollPanel.Location = new System.Drawing.Point(0, 169);
			CompilerOptionsTextBoxMinScrollPanel.Name = "CompilerOptionsTextBoxMinScrollPanel";
			CompilerOptionsTextBoxMinScrollPanel.Size = new System.Drawing.Size(764, 37);
			CompilerOptionsTextBoxMinScrollPanel.TabIndex = 41;
			//
			//CompileLogRichTextBox
			//
			CompileLogRichTextBox.CueBannerText = "";
			CompileLogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			CompileLogRichTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			CompileLogRichTextBox.HideSelection = false;
			CompileLogRichTextBox.Location = new System.Drawing.Point(0, 26);
			CompileLogRichTextBox.Name = "CompileLogRichTextBox";
			CompileLogRichTextBox.ReadOnly = true;
			CompileLogRichTextBox.Size = new System.Drawing.Size(770, 186);
			CompileLogRichTextBox.TabIndex = 0;
			CompileLogRichTextBox.Text = "";
			CompileLogRichTextBox.WordWrap = false;
			//
			//CompileButtonsPanel
			//
			CompileButtonsPanel.Controls.Add(CompileButton);
			CompileButtonsPanel.Controls.Add(SkipCurrentModelButton);
			CompileButtonsPanel.Controls.Add(CancelCompileButton);
			CompileButtonsPanel.Controls.Add(UseAllInPackButton);
			CompileButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			CompileButtonsPanel.Location = new System.Drawing.Point(0, 0);
			CompileButtonsPanel.Name = "CompileButtonsPanel";
			CompileButtonsPanel.Size = new System.Drawing.Size(770, 26);
			CompileButtonsPanel.TabIndex = 39;
			//
			//SkipCurrentModelButton
			//
			SkipCurrentModelButton.Enabled = false;
			SkipCurrentModelButton.Location = new System.Drawing.Point(131, 0);
			SkipCurrentModelButton.Name = "SkipCurrentModelButton";
			SkipCurrentModelButton.Size = new System.Drawing.Size(120, 23);
			SkipCurrentModelButton.TabIndex = 2;
			SkipCurrentModelButton.Text = "Skip Current Model";
			SkipCurrentModelButton.UseVisualStyleBackColor = true;
			//
			//CancelCompileButton
			//
			CancelCompileButton.Enabled = false;
			CancelCompileButton.Location = new System.Drawing.Point(257, 0);
			CancelCompileButton.Name = "CancelCompileButton";
			CancelCompileButton.Size = new System.Drawing.Size(120, 23);
			CancelCompileButton.TabIndex = 3;
			CancelCompileButton.Text = "Cancel Compile";
			CancelCompileButton.UseVisualStyleBackColor = true;
			//
			//UseAllInPackButton
			//
			UseAllInPackButton.Enabled = false;
			UseAllInPackButton.Location = new System.Drawing.Point(383, 0);
			UseAllInPackButton.Name = "UseAllInPackButton";
			UseAllInPackButton.Size = new System.Drawing.Size(120, 23);
			UseAllInPackButton.TabIndex = 4;
			UseAllInPackButton.Text = "Use All in Pack";
			UseAllInPackButton.UseVisualStyleBackColor = true;
			UseAllInPackButton.Visible = false;
			//
			//PostCompilePanel
			//
			PostCompilePanel.Controls.Add(CompiledFilesComboBox);
			PostCompilePanel.Controls.Add(UseInViewButton);
			PostCompilePanel.Controls.Add(RecompileButton);
			PostCompilePanel.Controls.Add(UseInPackButton);
			PostCompilePanel.Controls.Add(GotoCompiledMdlButton);
			PostCompilePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			PostCompilePanel.Location = new System.Drawing.Point(0, 212);
			PostCompilePanel.Name = "PostCompilePanel";
			PostCompilePanel.Size = new System.Drawing.Size(770, 26);
			PostCompilePanel.TabIndex = 40;
			//
			//CompiledFilesComboBox
			//
			CompiledFilesComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			CompiledFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			CompiledFilesComboBox.FormattingEnabled = true;
			CompiledFilesComboBox.Location = new System.Drawing.Point(0, 4);
			CompiledFilesComboBox.Name = "CompiledFilesComboBox";
			CompiledFilesComboBox.Size = new System.Drawing.Size(559, 21);
			CompiledFilesComboBox.TabIndex = 1;
			//
			//UseInViewButton
			//
			UseInViewButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseInViewButton.Enabled = false;
			UseInViewButton.Location = new System.Drawing.Point(565, 3);
			UseInViewButton.Name = "UseInViewButton";
			UseInViewButton.Size = new System.Drawing.Size(75, 23);
			UseInViewButton.TabIndex = 2;
			UseInViewButton.Text = "Use in View";
			UseInViewButton.UseVisualStyleBackColor = true;
			//
			//RecompileButton
			//
			RecompileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			RecompileButton.Enabled = false;
			RecompileButton.Location = new System.Drawing.Point(646, 3);
			RecompileButton.Name = "RecompileButton";
			RecompileButton.Size = new System.Drawing.Size(75, 23);
			RecompileButton.TabIndex = 5;
			RecompileButton.Text = "Recompile";
			RecompileButton.UseVisualStyleBackColor = true;
			//
			//UseInPackButton
			//
			UseInPackButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseInPackButton.Enabled = false;
			UseInPackButton.Location = new System.Drawing.Point(646, 3);
			UseInPackButton.Name = "UseInPackButton";
			UseInPackButton.Size = new System.Drawing.Size(75, 23);
			UseInPackButton.TabIndex = 3;
			UseInPackButton.Text = "Use in Pack";
			UseInPackButton.UseVisualStyleBackColor = true;
			UseInPackButton.Visible = false;
			//
			//GotoCompiledMdlButton
			//
			GotoCompiledMdlButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoCompiledMdlButton.Location = new System.Drawing.Point(727, 3);
			GotoCompiledMdlButton.Name = "GotoCompiledMdlButton";
			GotoCompiledMdlButton.Size = new System.Drawing.Size(43, 23);
			GotoCompiledMdlButton.TabIndex = 4;
			GotoCompiledMdlButton.Text = "Goto";
			GotoCompiledMdlButton.UseVisualStyleBackColor = true;
			//
			//UseDefaultOutputSubfolderButton
			//
			UseDefaultOutputSubfolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseDefaultOutputSubfolderButton.Location = new System.Drawing.Point(660, 32);
			UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton";
			UseDefaultOutputSubfolderButton.Size = new System.Drawing.Size(113, 23);
			UseDefaultOutputSubfolderButton.TabIndex = 12;
			UseDefaultOutputSubfolderButton.Text = "Use Default";
			UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = true;
			//
			//CompileUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			Controls.Add(Panel1);
			Name = "CompileUserControl";
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
			GameSetupPanel.ResumeLayout(false);
			CompilerOptionsSourceEnginePanel.ResumeLayout(false);
			CompilerOptionsSourceEnginePanel.PerformLayout();
			DefineBonesGroupBox.ResumeLayout(false);
			DefineBonesGroupBox.PerformLayout();
			CompilerOptionsGoldSourceEnginePanel.ResumeLayout(false);
			CompilerOptionsGoldSourceEnginePanel.PerformLayout();
			CompileButtonsPanel.ResumeLayout(false);
			PostCompilePanel.ResumeLayout(false);
			ResumeLayout(false);

			Load += new System.EventHandler(CompileUserControl_Load);
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