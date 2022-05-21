using System.ComponentModel;
using System.IO;

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
	public partial class SetUpGamesUserControl : BaseUserControl
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
			AddLibraryPathButton = new System.Windows.Forms.Button();
			DeleteLibraryPathButton = new System.Windows.Forms.Button();
			Label11 = new System.Windows.Forms.Label();
			Label10 = new System.Windows.Forms.Label();
			BrowseForSteamAppPathFileNameButton = new System.Windows.Forms.Button();
			SteamAppPathFileNameTextBox = new System.Windows.Forms.TextBox();
			AddGameSetupButton = new System.Windows.Forms.Button();
			GameSetupComboBox = new System.Windows.Forms.ComboBox();
			GameGroupBox = new System.Windows.Forms.GroupBox();
			EngineLabel = new System.Windows.Forms.Label();
			EngineComboBox = new System.Windows.Forms.ComboBox();
			CreateModelsFolderTreeButton = new System.Windows.Forms.Button();
			BrowseForMappingToolPathFileNameButton = new System.Windows.Forms.Button();
			MappingToolPathFileNameTextBox = new System.Windows.Forms.TextBox();
			MappingToolLabel = new System.Windows.Forms.Label();
			GameAppOptionsTextBox = new System.Windows.Forms.TextBox();
			ExecutableOptionsLabel = new System.Windows.Forms.Label();
			ClearGameAppOptionsButton = new System.Windows.Forms.Button();
			BrowseForGameAppPathFileNameButton = new System.Windows.Forms.Button();
			GameAppPathFileNameTextBox = new System.Windows.Forms.TextBox();
			ExecutableLabel = new System.Windows.Forms.Label();
			PackerLabel = new System.Windows.Forms.Label();
			BrowseForUnpackerPathFileNameButton = new System.Windows.Forms.Button();
			PackerPathFileNameTextBox = new System.Windows.Forms.TextBox();
			ModelViewerLabel = new System.Windows.Forms.Label();
			BrowseForViewerPathFileNameButton = new System.Windows.Forms.Button();
			ViewerPathFileNameTextBox = new System.Windows.Forms.TextBox();
			CloneGameSetupButton = new System.Windows.Forms.Button();
			GameNameTextBox = new Crowbar.TextBoxEx();
			NameLabel = new System.Windows.Forms.Label();
			DeleteGameSetupButton = new System.Windows.Forms.Button();
			BrowseForGamePathFileNameButton = new System.Windows.Forms.Button();
			GamePathFileNameTextBox = new System.Windows.Forms.TextBox();
			ModelCompilerLabel = new System.Windows.Forms.Label();
			BrowseForCompilerPathFileNameButton = new System.Windows.Forms.Button();
			CompilerPathFileNameTextBox = new System.Windows.Forms.TextBox();
			GamePathLabel = new System.Windows.Forms.Label();
			GoBackButton = new System.Windows.Forms.Button();
			SteamLibraryPathsDataGridView = new Crowbar.MacroDataGridView();
			ToolTip1 = new System.Windows.Forms.ToolTip(components);
			Panel1 = new System.Windows.Forms.Panel();
			GameGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)SteamLibraryPathsDataGridView).BeginInit();
			Panel1.SuspendLayout();
			SuspendLayout();
			//
			//AddLibraryPathButton
			//
			AddLibraryPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			AddLibraryPathButton.Location = new System.Drawing.Point(613, 412);
			AddLibraryPathButton.Name = "AddLibraryPathButton";
			AddLibraryPathButton.Size = new System.Drawing.Size(75, 23);
			AddLibraryPathButton.TabIndex = 51;
			AddLibraryPathButton.Text = "Add Macro";
			AddLibraryPathButton.UseVisualStyleBackColor = true;
			//
			//DeleteLibraryPathButton
			//
			DeleteLibraryPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			DeleteLibraryPathButton.Location = new System.Drawing.Point(613, 441);
			DeleteLibraryPathButton.Name = "DeleteLibraryPathButton";
			DeleteLibraryPathButton.Size = new System.Drawing.Size(75, 50);
			DeleteLibraryPathButton.TabIndex = 50;
			DeleteLibraryPathButton.Text = "Delete Last Macro If Not Used";
			DeleteLibraryPathButton.UseVisualStyleBackColor = true;
			//
			//Label11
			//
			Label11.AutoSize = true;
			Label11.Location = new System.Drawing.Point(3, 396);
			Label11.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			Label11.Name = "Label11";
			Label11.Size = new System.Drawing.Size(573, 13);
			Label11.TabIndex = 48;
			Label11.Text = "Steam Library folders (<library#> macros for placing at start of fields above; ri" + "ght-click a macro for commands):";
			//
			//Label10
			//
			Label10.AutoSize = true;
			Label10.Location = new System.Drawing.Point(3, 348);
			Label10.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			Label10.Name = "Label10";
			Label10.Size = new System.Drawing.Size(314, 13);
			Label10.TabIndex = 45;
			Label10.Text = "Steam executable (steam.exe) [Used for \"Run Game\" button]:";
			//
			//BrowseForSteamAppPathFileNameButton
			//
			BrowseForSteamAppPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForSteamAppPathFileNameButton.Location = new System.Drawing.Point(613, 364);
			BrowseForSteamAppPathFileNameButton.Name = "BrowseForSteamAppPathFileNameButton";
			BrowseForSteamAppPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			BrowseForSteamAppPathFileNameButton.TabIndex = 47;
			BrowseForSteamAppPathFileNameButton.Text = "Browse...";
			BrowseForSteamAppPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//SteamAppPathFileNameTextBox
			//
			SteamAppPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			SteamAppPathFileNameTextBox.Location = new System.Drawing.Point(3, 364);
			SteamAppPathFileNameTextBox.Name = "SteamAppPathFileNameTextBox";
			SteamAppPathFileNameTextBox.Size = new System.Drawing.Size(604, 22);
			SteamAppPathFileNameTextBox.TabIndex = 46;
			//
			//AddGameSetupButton
			//
			AddGameSetupButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			AddGameSetupButton.Location = new System.Drawing.Point(706, 3);
			AddGameSetupButton.Name = "AddGameSetupButton";
			AddGameSetupButton.Size = new System.Drawing.Size(75, 23);
			AddGameSetupButton.TabIndex = 43;
			AddGameSetupButton.Text = "Add";
			AddGameSetupButton.UseVisualStyleBackColor = true;
			//
			//GameSetupComboBox
			//
			GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			GameSetupComboBox.FormattingEnabled = true;
			GameSetupComboBox.Location = new System.Drawing.Point(3, 4);
			GameSetupComboBox.Name = "GameSetupComboBox";
			GameSetupComboBox.Size = new System.Drawing.Size(697, 21);
			GameSetupComboBox.TabIndex = 42;
			//
			//GameGroupBox
			//
			GameGroupBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameGroupBox.Controls.Add(EngineLabel);
			GameGroupBox.Controls.Add(EngineComboBox);
			GameGroupBox.Controls.Add(CreateModelsFolderTreeButton);
			GameGroupBox.Controls.Add(BrowseForMappingToolPathFileNameButton);
			GameGroupBox.Controls.Add(MappingToolPathFileNameTextBox);
			GameGroupBox.Controls.Add(MappingToolLabel);
			GameGroupBox.Controls.Add(GameAppOptionsTextBox);
			GameGroupBox.Controls.Add(ExecutableOptionsLabel);
			GameGroupBox.Controls.Add(ClearGameAppOptionsButton);
			GameGroupBox.Controls.Add(BrowseForGameAppPathFileNameButton);
			GameGroupBox.Controls.Add(GameAppPathFileNameTextBox);
			GameGroupBox.Controls.Add(ExecutableLabel);
			GameGroupBox.Controls.Add(PackerLabel);
			GameGroupBox.Controls.Add(BrowseForUnpackerPathFileNameButton);
			GameGroupBox.Controls.Add(PackerPathFileNameTextBox);
			GameGroupBox.Controls.Add(ModelViewerLabel);
			GameGroupBox.Controls.Add(BrowseForViewerPathFileNameButton);
			GameGroupBox.Controls.Add(ViewerPathFileNameTextBox);
			GameGroupBox.Controls.Add(CloneGameSetupButton);
			GameGroupBox.Controls.Add(GameNameTextBox);
			GameGroupBox.Controls.Add(NameLabel);
			GameGroupBox.Controls.Add(DeleteGameSetupButton);
			GameGroupBox.Controls.Add(BrowseForGamePathFileNameButton);
			GameGroupBox.Controls.Add(GamePathFileNameTextBox);
			GameGroupBox.Controls.Add(ModelCompilerLabel);
			GameGroupBox.Controls.Add(BrowseForCompilerPathFileNameButton);
			GameGroupBox.Controls.Add(CompilerPathFileNameTextBox);
			GameGroupBox.Controls.Add(GamePathLabel);
			GameGroupBox.Location = new System.Drawing.Point(3, 32);
			GameGroupBox.Name = "GameGroupBox";
			GameGroupBox.Size = new System.Drawing.Size(778, 304);
			GameGroupBox.TabIndex = 44;
			GameGroupBox.TabStop = false;
			GameGroupBox.Text = "Game Setup";
			//
			//EngineLabel
			//
			EngineLabel.AutoSize = true;
			EngineLabel.Location = new System.Drawing.Point(6, 49);
			EngineLabel.Name = "EngineLabel";
			EngineLabel.Size = new System.Drawing.Size(46, 13);
			EngineLabel.TabIndex = 43;
			EngineLabel.Text = "Engine:";
			//
			//EngineComboBox
			//
			EngineComboBox.FormattingEnabled = true;
			EngineComboBox.Items.AddRange(new object[] {"GoldSource", "Source"});
			EngineComboBox.Location = new System.Drawing.Point(55, 45);
			EngineComboBox.Name = "EngineComboBox";
			EngineComboBox.Size = new System.Drawing.Size(121, 21);
			EngineComboBox.TabIndex = 42;
			//
			//CreateModelsFolderTreeButton
			//
			CreateModelsFolderTreeButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			CreateModelsFolderTreeButton.Location = new System.Drawing.Point(502, 275);
			CreateModelsFolderTreeButton.Name = "CreateModelsFolderTreeButton";
			CreateModelsFolderTreeButton.Size = new System.Drawing.Size(270, 23);
			CreateModelsFolderTreeButton.TabIndex = 40;
			CreateModelsFolderTreeButton.Text = "Create \"models\" folder tree from this game's VPKs";
			ToolTip1.SetToolTip(CreateModelsFolderTreeButton, "Use this so HLMV can view models found in VPKs.");
			CreateModelsFolderTreeButton.UseVisualStyleBackColor = true;
			//
			//BrowseForMappingToolPathFileNameButton
			//
			BrowseForMappingToolPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForMappingToolPathFileNameButton.Location = new System.Drawing.Point(697, 217);
			BrowseForMappingToolPathFileNameButton.Name = "BrowseForMappingToolPathFileNameButton";
			BrowseForMappingToolPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			BrowseForMappingToolPathFileNameButton.TabIndex = 39;
			BrowseForMappingToolPathFileNameButton.Text = "Browse...";
			BrowseForMappingToolPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//MappingToolPathFileNameTextBox
			//
			MappingToolPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			MappingToolPathFileNameTextBox.Location = new System.Drawing.Point(102, 217);
			MappingToolPathFileNameTextBox.Name = "MappingToolPathFileNameTextBox";
			MappingToolPathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			MappingToolPathFileNameTextBox.TabIndex = 38;
			//
			//MappingToolLabel
			//
			MappingToolLabel.AutoSize = true;
			MappingToolLabel.Location = new System.Drawing.Point(6, 222);
			MappingToolLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			MappingToolLabel.Name = "MappingToolLabel";
			MappingToolLabel.Size = new System.Drawing.Size(81, 13);
			MappingToolLabel.TabIndex = 37;
			MappingToolLabel.Text = "Mapping tool:";
			//
			//GameAppOptionsTextBox
			//
			GameAppOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameAppOptionsTextBox.Location = new System.Drawing.Point(112, 101);
			GameAppOptionsTextBox.Name = "GameAppOptionsTextBox";
			GameAppOptionsTextBox.Size = new System.Drawing.Size(579, 22);
			GameAppOptionsTextBox.TabIndex = 32;
			//
			//ExecutableOptionsLabel
			//
			ExecutableOptionsLabel.AutoSize = true;
			ExecutableOptionsLabel.Location = new System.Drawing.Point(6, 106);
			ExecutableOptionsLabel.Name = "ExecutableOptionsLabel";
			ExecutableOptionsLabel.Size = new System.Drawing.Size(108, 13);
			ExecutableOptionsLabel.TabIndex = 31;
			ExecutableOptionsLabel.Text = "Executable options:";
			//
			//ClearGameAppOptionsButton
			//
			ClearGameAppOptionsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ClearGameAppOptionsButton.Location = new System.Drawing.Point(697, 101);
			ClearGameAppOptionsButton.Name = "ClearGameAppOptionsButton";
			ClearGameAppOptionsButton.Size = new System.Drawing.Size(75, 23);
			ClearGameAppOptionsButton.TabIndex = 33;
			ClearGameAppOptionsButton.Text = "Clear";
			ClearGameAppOptionsButton.UseVisualStyleBackColor = true;
			//
			//BrowseForGameAppPathFileNameButton
			//
			BrowseForGameAppPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForGameAppPathFileNameButton.Location = new System.Drawing.Point(697, 72);
			BrowseForGameAppPathFileNameButton.Name = "BrowseForGameAppPathFileNameButton";
			BrowseForGameAppPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			BrowseForGameAppPathFileNameButton.TabIndex = 30;
			BrowseForGameAppPathFileNameButton.Text = "Browse...";
			BrowseForGameAppPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//GameAppPathFileNameTextBox
			//
			GameAppPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameAppPathFileNameTextBox.Location = new System.Drawing.Point(112, 72);
			GameAppPathFileNameTextBox.Name = "GameAppPathFileNameTextBox";
			GameAppPathFileNameTextBox.Size = new System.Drawing.Size(579, 22);
			GameAppPathFileNameTextBox.TabIndex = 29;
			//
			//ExecutableLabel
			//
			ExecutableLabel.AutoSize = true;
			ExecutableLabel.Location = new System.Drawing.Point(6, 77);
			ExecutableLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			ExecutableLabel.Name = "ExecutableLabel";
			ExecutableLabel.Size = new System.Drawing.Size(99, 13);
			ExecutableLabel.TabIndex = 28;
			ExecutableLabel.Text = "Executable (*.exe):";
			//
			//PackerLabel
			//
			PackerLabel.AutoSize = true;
			PackerLabel.Location = new System.Drawing.Point(6, 251);
			PackerLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			PackerLabel.Name = "PackerLabel";
			PackerLabel.Size = new System.Drawing.Size(67, 13);
			PackerLabel.TabIndex = 16;
			PackerLabel.Text = "Packer tool:";
			//
			//BrowseForUnpackerPathFileNameButton
			//
			BrowseForUnpackerPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForUnpackerPathFileNameButton.Location = new System.Drawing.Point(697, 246);
			BrowseForUnpackerPathFileNameButton.Name = "BrowseForUnpackerPathFileNameButton";
			BrowseForUnpackerPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			BrowseForUnpackerPathFileNameButton.TabIndex = 18;
			BrowseForUnpackerPathFileNameButton.Text = "Browse...";
			BrowseForUnpackerPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//PackerPathFileNameTextBox
			//
			PackerPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			PackerPathFileNameTextBox.Location = new System.Drawing.Point(102, 246);
			PackerPathFileNameTextBox.Name = "PackerPathFileNameTextBox";
			PackerPathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			PackerPathFileNameTextBox.TabIndex = 17;
			//
			//ModelViewerLabel
			//
			ModelViewerLabel.AutoSize = true;
			ModelViewerLabel.Location = new System.Drawing.Point(6, 193);
			ModelViewerLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			ModelViewerLabel.Name = "ModelViewerLabel";
			ModelViewerLabel.Size = new System.Drawing.Size(79, 13);
			ModelViewerLabel.TabIndex = 13;
			ModelViewerLabel.Text = "Model viewer:";
			//
			//BrowseForViewerPathFileNameButton
			//
			BrowseForViewerPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForViewerPathFileNameButton.Location = new System.Drawing.Point(697, 188);
			BrowseForViewerPathFileNameButton.Name = "BrowseForViewerPathFileNameButton";
			BrowseForViewerPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			BrowseForViewerPathFileNameButton.TabIndex = 15;
			BrowseForViewerPathFileNameButton.Text = "Browse...";
			BrowseForViewerPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//ViewerPathFileNameTextBox
			//
			ViewerPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ViewerPathFileNameTextBox.Location = new System.Drawing.Point(102, 188);
			ViewerPathFileNameTextBox.Name = "ViewerPathFileNameTextBox";
			ViewerPathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			ViewerPathFileNameTextBox.TabIndex = 14;
			//
			//CloneGameSetupButton
			//
			CloneGameSetupButton.Location = new System.Drawing.Point(6, 275);
			CloneGameSetupButton.Name = "CloneGameSetupButton";
			CloneGameSetupButton.Size = new System.Drawing.Size(75, 23);
			CloneGameSetupButton.TabIndex = 12;
			CloneGameSetupButton.Text = "Clone";
			CloneGameSetupButton.UseVisualStyleBackColor = true;
			//
			//GameNameTextBox
			//
			GameNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameNameTextBox.CueBannerText = "";
			GameNameTextBox.Location = new System.Drawing.Point(55, 19);
			GameNameTextBox.Name = "GameNameTextBox";
			GameNameTextBox.Size = new System.Drawing.Size(717, 22);
			GameNameTextBox.TabIndex = 1;
			//
			//NameLabel
			//
			NameLabel.AutoSize = true;
			NameLabel.Location = new System.Drawing.Point(6, 24);
			NameLabel.Name = "NameLabel";
			NameLabel.Size = new System.Drawing.Size(39, 13);
			NameLabel.TabIndex = 0;
			NameLabel.Text = "Name:";
			//
			//DeleteGameSetupButton
			//
			DeleteGameSetupButton.Location = new System.Drawing.Point(87, 275);
			DeleteGameSetupButton.Name = "DeleteGameSetupButton";
			DeleteGameSetupButton.Size = new System.Drawing.Size(75, 23);
			DeleteGameSetupButton.TabIndex = 8;
			DeleteGameSetupButton.Text = "Delete";
			DeleteGameSetupButton.UseVisualStyleBackColor = true;
			//
			//BrowseForGamePathFileNameButton
			//
			BrowseForGamePathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForGamePathFileNameButton.Location = new System.Drawing.Point(697, 130);
			BrowseForGamePathFileNameButton.Name = "BrowseForGamePathFileNameButton";
			BrowseForGamePathFileNameButton.Size = new System.Drawing.Size(75, 23);
			BrowseForGamePathFileNameButton.TabIndex = 4;
			BrowseForGamePathFileNameButton.Text = "Browse...";
			BrowseForGamePathFileNameButton.UseVisualStyleBackColor = true;
			//
			//GamePathFileNameTextBox
			//
			GamePathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GamePathFileNameTextBox.Location = new System.Drawing.Point(102, 130);
			GamePathFileNameTextBox.Name = "GamePathFileNameTextBox";
			GamePathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			GamePathFileNameTextBox.TabIndex = 3;
			//
			//ModelCompilerLabel
			//
			ModelCompilerLabel.AutoSize = true;
			ModelCompilerLabel.Location = new System.Drawing.Point(6, 164);
			ModelCompilerLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			ModelCompilerLabel.Name = "ModelCompilerLabel";
			ModelCompilerLabel.Size = new System.Drawing.Size(90, 13);
			ModelCompilerLabel.TabIndex = 5;
			ModelCompilerLabel.Text = "Model compiler:";
			//
			//BrowseForCompilerPathFileNameButton
			//
			BrowseForCompilerPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForCompilerPathFileNameButton.Location = new System.Drawing.Point(697, 159);
			BrowseForCompilerPathFileNameButton.Name = "BrowseForCompilerPathFileNameButton";
			BrowseForCompilerPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			BrowseForCompilerPathFileNameButton.TabIndex = 7;
			BrowseForCompilerPathFileNameButton.Text = "Browse...";
			BrowseForCompilerPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//CompilerPathFileNameTextBox
			//
			CompilerPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			CompilerPathFileNameTextBox.Location = new System.Drawing.Point(102, 159);
			CompilerPathFileNameTextBox.Name = "CompilerPathFileNameTextBox";
			CompilerPathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			CompilerPathFileNameTextBox.TabIndex = 6;
			//
			//GamePathLabel
			//
			GamePathLabel.AutoSize = true;
			GamePathLabel.Location = new System.Drawing.Point(6, 135);
			GamePathLabel.Name = "GamePathLabel";
			GamePathLabel.Size = new System.Drawing.Size(76, 13);
			GamePathLabel.TabIndex = 2;
			GamePathLabel.Text = "GameInfo.txt:";
			//
			//GoBackButton
			//
			GoBackButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			GoBackButton.Enabled = false;
			GoBackButton.Location = new System.Drawing.Point(706, 520);
			GoBackButton.Name = "GoBackButton";
			GoBackButton.Size = new System.Drawing.Size(75, 23);
			GoBackButton.TabIndex = 52;
			GoBackButton.Text = "Go Back";
			GoBackButton.UseVisualStyleBackColor = true;
			//
			//SteamLibraryPathsDataGridView
			//
			SteamLibraryPathsDataGridView.AllowUserToAddRows = false;
			SteamLibraryPathsDataGridView.AllowUserToDeleteRows = false;
			SteamLibraryPathsDataGridView.AllowUserToResizeRows = false;
			SteamLibraryPathsDataGridView.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			SteamLibraryPathsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			SteamLibraryPathsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlDark;
			SteamLibraryPathsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			SteamLibraryPathsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			SteamLibraryPathsDataGridView.Location = new System.Drawing.Point(3, 412);
			SteamLibraryPathsDataGridView.MultiSelect = false;
			SteamLibraryPathsDataGridView.Name = "SteamLibraryPathsDataGridView";
			SteamLibraryPathsDataGridView.RowHeadersVisible = false;
			SteamLibraryPathsDataGridView.RowHeadersWidth = 25;
			SteamLibraryPathsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			SteamLibraryPathsDataGridView.Size = new System.Drawing.Size(604, 131);
			SteamLibraryPathsDataGridView.TabIndex = 49;
			//
			//Panel1
			//
			Panel1.Controls.Add(GoBackButton);
			Panel1.Controls.Add(GameSetupComboBox);
			Panel1.Controls.Add(AddLibraryPathButton);
			Panel1.Controls.Add(DeleteLibraryPathButton);
			Panel1.Controls.Add(SteamLibraryPathsDataGridView);
			Panel1.Controls.Add(Label11);
			Panel1.Controls.Add(Label10);
			Panel1.Controls.Add(BrowseForSteamAppPathFileNameButton);
			Panel1.Controls.Add(SteamAppPathFileNameTextBox);
			Panel1.Controls.Add(AddGameSetupButton);
			Panel1.Controls.Add(GameGroupBox);
			Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel1.Location = new System.Drawing.Point(0, 0);
			Panel1.Name = "Panel1";
			Panel1.Size = new System.Drawing.Size(784, 546);
			Panel1.TabIndex = 17;
			//
			//SetUpGamesUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Panel1);
			Name = "SetUpGamesUserControl";
			Size = new System.Drawing.Size(784, 546);
			GameGroupBox.ResumeLayout(false);
			GameGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)SteamLibraryPathsDataGridView).EndInit();
			Panel1.ResumeLayout(false);
			Panel1.PerformLayout();
			ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			GameSetupComboBox.SelectedIndexChanged += new System.EventHandler(GameSetupComboBox_SelectedIndexChanged);
			AddGameSetupButton.Click += new System.EventHandler(AddGameSetupButton_Click);
			BrowseForGamePathFileNameButton.Click += new System.EventHandler(BrowseForGamePathFileNameButton_Click);
			BrowseForGameAppPathFileNameButton.Click += new System.EventHandler(BrowseForGameAppPathFileNameButton_Click);
			ClearGameAppOptionsButton.Click += new System.EventHandler(ClearGameAppOptionsButton_Click);
			BrowseForCompilerPathFileNameButton.Click += new System.EventHandler(BrowseForCompilerPathFileNameButton_Click);
			BrowseForViewerPathFileNameButton.Click += new System.EventHandler(BrowseForViewerPathFileNameButton_Click);
			BrowseForMappingToolPathFileNameButton.Click += new System.EventHandler(BrowseForMappingToolPathFileNameButton_Click);
			BrowseForUnpackerPathFileNameButton.Click += new System.EventHandler(BrowseForUnpackerPathFileNameButton_Click);
			CloneGameSetupButton.Click += new System.EventHandler(CloneGameSetupButton_Click);
			DeleteGameSetupButton.Click += new System.EventHandler(DeleteGameSetupButton_Click);
			CreateModelsFolderTreeButton.Click += new System.EventHandler(CreateModelsFolderTreeButton_Click);
			BrowseForSteamAppPathFileNameButton.Click += new System.EventHandler(BrowseForSteamAppPathFileNameButton_Click);
			SteamLibraryPathsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(SteamLibraryPathsDataGridView_CellContentClick);
			AddLibraryPathButton.Click += new System.EventHandler(AddLibraryPathButton_Click);
			DeleteLibraryPathButton.Click += new System.EventHandler(DeleteLibraryPathButton_Click);
		}
		internal System.Windows.Forms.Button AddLibraryPathButton;
		internal System.Windows.Forms.Button DeleteLibraryPathButton;
		internal Crowbar.MacroDataGridView SteamLibraryPathsDataGridView;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.Button BrowseForSteamAppPathFileNameButton;
		internal System.Windows.Forms.TextBox SteamAppPathFileNameTextBox;
		internal System.Windows.Forms.Button AddGameSetupButton;
		internal System.Windows.Forms.ComboBox GameSetupComboBox;
		internal System.Windows.Forms.GroupBox GameGroupBox;
		internal System.Windows.Forms.Button CreateModelsFolderTreeButton;
		internal System.Windows.Forms.Button BrowseForMappingToolPathFileNameButton;
		internal System.Windows.Forms.TextBox MappingToolPathFileNameTextBox;
		internal System.Windows.Forms.Label MappingToolLabel;
		internal System.Windows.Forms.TextBox GameAppOptionsTextBox;
		internal System.Windows.Forms.Label ExecutableOptionsLabel;
		internal System.Windows.Forms.Button ClearGameAppOptionsButton;
		internal System.Windows.Forms.Button BrowseForGameAppPathFileNameButton;
		internal System.Windows.Forms.TextBox GameAppPathFileNameTextBox;
		internal System.Windows.Forms.Label ExecutableLabel;
		internal System.Windows.Forms.Label PackerLabel;
		internal System.Windows.Forms.Button BrowseForUnpackerPathFileNameButton;
		internal System.Windows.Forms.TextBox PackerPathFileNameTextBox;
		internal System.Windows.Forms.Label ModelViewerLabel;
		internal System.Windows.Forms.Button BrowseForViewerPathFileNameButton;
		internal System.Windows.Forms.TextBox ViewerPathFileNameTextBox;
		internal System.Windows.Forms.Button CloneGameSetupButton;
		internal Crowbar.TextBoxEx GameNameTextBox;
		internal System.Windows.Forms.Label NameLabel;
		internal System.Windows.Forms.Button DeleteGameSetupButton;
		internal System.Windows.Forms.Button BrowseForGamePathFileNameButton;
		internal System.Windows.Forms.TextBox GamePathFileNameTextBox;
		internal System.Windows.Forms.Label ModelCompilerLabel;
		internal System.Windows.Forms.Button BrowseForCompilerPathFileNameButton;
		internal System.Windows.Forms.TextBox CompilerPathFileNameTextBox;
		internal System.Windows.Forms.Label GamePathLabel;
		internal System.Windows.Forms.ComboBox EngineComboBox;
		internal System.Windows.Forms.Label EngineLabel;
		internal System.Windows.Forms.Button GoBackButton;
		internal ToolTip ToolTip1;
		internal Panel Panel1;
	}

}