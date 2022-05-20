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
			this.components = new System.ComponentModel.Container();
			this.AddLibraryPathButton = new System.Windows.Forms.Button();
			this.DeleteLibraryPathButton = new System.Windows.Forms.Button();
			this.Label11 = new System.Windows.Forms.Label();
			this.Label10 = new System.Windows.Forms.Label();
			this.BrowseForSteamAppPathFileNameButton = new System.Windows.Forms.Button();
			this.SteamAppPathFileNameTextBox = new System.Windows.Forms.TextBox();
			this.AddGameSetupButton = new System.Windows.Forms.Button();
			this.GameSetupComboBox = new System.Windows.Forms.ComboBox();
			this.GameGroupBox = new System.Windows.Forms.GroupBox();
			this.EngineLabel = new System.Windows.Forms.Label();
			this.EngineComboBox = new System.Windows.Forms.ComboBox();
			this.CreateModelsFolderTreeButton = new System.Windows.Forms.Button();
			this.BrowseForMappingToolPathFileNameButton = new System.Windows.Forms.Button();
			this.MappingToolPathFileNameTextBox = new System.Windows.Forms.TextBox();
			this.MappingToolLabel = new System.Windows.Forms.Label();
			this.GameAppOptionsTextBox = new System.Windows.Forms.TextBox();
			this.ExecutableOptionsLabel = new System.Windows.Forms.Label();
			this.ClearGameAppOptionsButton = new System.Windows.Forms.Button();
			this.BrowseForGameAppPathFileNameButton = new System.Windows.Forms.Button();
			this.GameAppPathFileNameTextBox = new System.Windows.Forms.TextBox();
			this.ExecutableLabel = new System.Windows.Forms.Label();
			this.PackerLabel = new System.Windows.Forms.Label();
			this.BrowseForUnpackerPathFileNameButton = new System.Windows.Forms.Button();
			this.PackerPathFileNameTextBox = new System.Windows.Forms.TextBox();
			this.ModelViewerLabel = new System.Windows.Forms.Label();
			this.BrowseForViewerPathFileNameButton = new System.Windows.Forms.Button();
			this.ViewerPathFileNameTextBox = new System.Windows.Forms.TextBox();
			this.CloneGameSetupButton = new System.Windows.Forms.Button();
			this.GameNameTextBox = new Crowbar.TextBoxEx();
			this.NameLabel = new System.Windows.Forms.Label();
			this.DeleteGameSetupButton = new System.Windows.Forms.Button();
			this.BrowseForGamePathFileNameButton = new System.Windows.Forms.Button();
			this.GamePathFileNameTextBox = new System.Windows.Forms.TextBox();
			this.ModelCompilerLabel = new System.Windows.Forms.Label();
			this.BrowseForCompilerPathFileNameButton = new System.Windows.Forms.Button();
			this.CompilerPathFileNameTextBox = new System.Windows.Forms.TextBox();
			this.GamePathLabel = new System.Windows.Forms.Label();
			this.GoBackButton = new System.Windows.Forms.Button();
			this.SteamLibraryPathsDataGridView = new Crowbar.MacroDataGridView();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.Panel1 = new System.Windows.Forms.Panel();
			this.GameGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.SteamLibraryPathsDataGridView).BeginInit();
			this.Panel1.SuspendLayout();
			this.SuspendLayout();
			//
			//AddLibraryPathButton
			//
			this.AddLibraryPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.AddLibraryPathButton.Location = new System.Drawing.Point(613, 412);
			this.AddLibraryPathButton.Name = "AddLibraryPathButton";
			this.AddLibraryPathButton.Size = new System.Drawing.Size(75, 23);
			this.AddLibraryPathButton.TabIndex = 51;
			this.AddLibraryPathButton.Text = "Add Macro";
			this.AddLibraryPathButton.UseVisualStyleBackColor = true;
			//
			//DeleteLibraryPathButton
			//
			this.DeleteLibraryPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.DeleteLibraryPathButton.Location = new System.Drawing.Point(613, 441);
			this.DeleteLibraryPathButton.Name = "DeleteLibraryPathButton";
			this.DeleteLibraryPathButton.Size = new System.Drawing.Size(75, 50);
			this.DeleteLibraryPathButton.TabIndex = 50;
			this.DeleteLibraryPathButton.Text = "Delete Last Macro If Not Used";
			this.DeleteLibraryPathButton.UseVisualStyleBackColor = true;
			//
			//Label11
			//
			this.Label11.AutoSize = true;
			this.Label11.Location = new System.Drawing.Point(3, 396);
			this.Label11.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			this.Label11.Name = "Label11";
			this.Label11.Size = new System.Drawing.Size(573, 13);
			this.Label11.TabIndex = 48;
			this.Label11.Text = "Steam Library folders (<library#> macros for placing at start of fields above; ri" + "ght-click a macro for commands):";
			//
			//Label10
			//
			this.Label10.AutoSize = true;
			this.Label10.Location = new System.Drawing.Point(3, 348);
			this.Label10.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			this.Label10.Name = "Label10";
			this.Label10.Size = new System.Drawing.Size(314, 13);
			this.Label10.TabIndex = 45;
			this.Label10.Text = "Steam executable (steam.exe) [Used for \"Run Game\" button]:";
			//
			//BrowseForSteamAppPathFileNameButton
			//
			this.BrowseForSteamAppPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForSteamAppPathFileNameButton.Location = new System.Drawing.Point(613, 364);
			this.BrowseForSteamAppPathFileNameButton.Name = "BrowseForSteamAppPathFileNameButton";
			this.BrowseForSteamAppPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForSteamAppPathFileNameButton.TabIndex = 47;
			this.BrowseForSteamAppPathFileNameButton.Text = "Browse...";
			this.BrowseForSteamAppPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//SteamAppPathFileNameTextBox
			//
			this.SteamAppPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.SteamAppPathFileNameTextBox.Location = new System.Drawing.Point(3, 364);
			this.SteamAppPathFileNameTextBox.Name = "SteamAppPathFileNameTextBox";
			this.SteamAppPathFileNameTextBox.Size = new System.Drawing.Size(604, 22);
			this.SteamAppPathFileNameTextBox.TabIndex = 46;
			//
			//AddGameSetupButton
			//
			this.AddGameSetupButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.AddGameSetupButton.Location = new System.Drawing.Point(706, 3);
			this.AddGameSetupButton.Name = "AddGameSetupButton";
			this.AddGameSetupButton.Size = new System.Drawing.Size(75, 23);
			this.AddGameSetupButton.TabIndex = 43;
			this.AddGameSetupButton.Text = "Add";
			this.AddGameSetupButton.UseVisualStyleBackColor = true;
			//
			//GameSetupComboBox
			//
			this.GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GameSetupComboBox.FormattingEnabled = true;
			this.GameSetupComboBox.Location = new System.Drawing.Point(3, 4);
			this.GameSetupComboBox.Name = "GameSetupComboBox";
			this.GameSetupComboBox.Size = new System.Drawing.Size(697, 21);
			this.GameSetupComboBox.TabIndex = 42;
			//
			//GameGroupBox
			//
			this.GameGroupBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameGroupBox.Controls.Add(this.EngineLabel);
			this.GameGroupBox.Controls.Add(this.EngineComboBox);
			this.GameGroupBox.Controls.Add(this.CreateModelsFolderTreeButton);
			this.GameGroupBox.Controls.Add(this.BrowseForMappingToolPathFileNameButton);
			this.GameGroupBox.Controls.Add(this.MappingToolPathFileNameTextBox);
			this.GameGroupBox.Controls.Add(this.MappingToolLabel);
			this.GameGroupBox.Controls.Add(this.GameAppOptionsTextBox);
			this.GameGroupBox.Controls.Add(this.ExecutableOptionsLabel);
			this.GameGroupBox.Controls.Add(this.ClearGameAppOptionsButton);
			this.GameGroupBox.Controls.Add(this.BrowseForGameAppPathFileNameButton);
			this.GameGroupBox.Controls.Add(this.GameAppPathFileNameTextBox);
			this.GameGroupBox.Controls.Add(this.ExecutableLabel);
			this.GameGroupBox.Controls.Add(this.PackerLabel);
			this.GameGroupBox.Controls.Add(this.BrowseForUnpackerPathFileNameButton);
			this.GameGroupBox.Controls.Add(this.PackerPathFileNameTextBox);
			this.GameGroupBox.Controls.Add(this.ModelViewerLabel);
			this.GameGroupBox.Controls.Add(this.BrowseForViewerPathFileNameButton);
			this.GameGroupBox.Controls.Add(this.ViewerPathFileNameTextBox);
			this.GameGroupBox.Controls.Add(this.CloneGameSetupButton);
			this.GameGroupBox.Controls.Add(this.GameNameTextBox);
			this.GameGroupBox.Controls.Add(this.NameLabel);
			this.GameGroupBox.Controls.Add(this.DeleteGameSetupButton);
			this.GameGroupBox.Controls.Add(this.BrowseForGamePathFileNameButton);
			this.GameGroupBox.Controls.Add(this.GamePathFileNameTextBox);
			this.GameGroupBox.Controls.Add(this.ModelCompilerLabel);
			this.GameGroupBox.Controls.Add(this.BrowseForCompilerPathFileNameButton);
			this.GameGroupBox.Controls.Add(this.CompilerPathFileNameTextBox);
			this.GameGroupBox.Controls.Add(this.GamePathLabel);
			this.GameGroupBox.Location = new System.Drawing.Point(3, 32);
			this.GameGroupBox.Name = "GameGroupBox";
			this.GameGroupBox.Size = new System.Drawing.Size(778, 304);
			this.GameGroupBox.TabIndex = 44;
			this.GameGroupBox.TabStop = false;
			this.GameGroupBox.Text = "Game Setup";
			//
			//EngineLabel
			//
			this.EngineLabel.AutoSize = true;
			this.EngineLabel.Location = new System.Drawing.Point(6, 49);
			this.EngineLabel.Name = "EngineLabel";
			this.EngineLabel.Size = new System.Drawing.Size(46, 13);
			this.EngineLabel.TabIndex = 43;
			this.EngineLabel.Text = "Engine:";
			//
			//EngineComboBox
			//
			this.EngineComboBox.FormattingEnabled = true;
			this.EngineComboBox.Items.AddRange(new object[] {"GoldSource", "Source"});
			this.EngineComboBox.Location = new System.Drawing.Point(55, 45);
			this.EngineComboBox.Name = "EngineComboBox";
			this.EngineComboBox.Size = new System.Drawing.Size(121, 21);
			this.EngineComboBox.TabIndex = 42;
			//
			//CreateModelsFolderTreeButton
			//
			this.CreateModelsFolderTreeButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.CreateModelsFolderTreeButton.Location = new System.Drawing.Point(502, 275);
			this.CreateModelsFolderTreeButton.Name = "CreateModelsFolderTreeButton";
			this.CreateModelsFolderTreeButton.Size = new System.Drawing.Size(270, 23);
			this.CreateModelsFolderTreeButton.TabIndex = 40;
			this.CreateModelsFolderTreeButton.Text = "Create \"models\" folder tree from this game's VPKs";
			this.ToolTip1.SetToolTip(this.CreateModelsFolderTreeButton, "Use this so HLMV can view models found in VPKs.");
			this.CreateModelsFolderTreeButton.UseVisualStyleBackColor = true;
			//
			//BrowseForMappingToolPathFileNameButton
			//
			this.BrowseForMappingToolPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForMappingToolPathFileNameButton.Location = new System.Drawing.Point(697, 217);
			this.BrowseForMappingToolPathFileNameButton.Name = "BrowseForMappingToolPathFileNameButton";
			this.BrowseForMappingToolPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForMappingToolPathFileNameButton.TabIndex = 39;
			this.BrowseForMappingToolPathFileNameButton.Text = "Browse...";
			this.BrowseForMappingToolPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//MappingToolPathFileNameTextBox
			//
			this.MappingToolPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.MappingToolPathFileNameTextBox.Location = new System.Drawing.Point(102, 217);
			this.MappingToolPathFileNameTextBox.Name = "MappingToolPathFileNameTextBox";
			this.MappingToolPathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			this.MappingToolPathFileNameTextBox.TabIndex = 38;
			//
			//MappingToolLabel
			//
			this.MappingToolLabel.AutoSize = true;
			this.MappingToolLabel.Location = new System.Drawing.Point(6, 222);
			this.MappingToolLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			this.MappingToolLabel.Name = "MappingToolLabel";
			this.MappingToolLabel.Size = new System.Drawing.Size(81, 13);
			this.MappingToolLabel.TabIndex = 37;
			this.MappingToolLabel.Text = "Mapping tool:";
			//
			//GameAppOptionsTextBox
			//
			this.GameAppOptionsTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameAppOptionsTextBox.Location = new System.Drawing.Point(112, 101);
			this.GameAppOptionsTextBox.Name = "GameAppOptionsTextBox";
			this.GameAppOptionsTextBox.Size = new System.Drawing.Size(579, 22);
			this.GameAppOptionsTextBox.TabIndex = 32;
			//
			//ExecutableOptionsLabel
			//
			this.ExecutableOptionsLabel.AutoSize = true;
			this.ExecutableOptionsLabel.Location = new System.Drawing.Point(6, 106);
			this.ExecutableOptionsLabel.Name = "ExecutableOptionsLabel";
			this.ExecutableOptionsLabel.Size = new System.Drawing.Size(108, 13);
			this.ExecutableOptionsLabel.TabIndex = 31;
			this.ExecutableOptionsLabel.Text = "Executable options:";
			//
			//ClearGameAppOptionsButton
			//
			this.ClearGameAppOptionsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.ClearGameAppOptionsButton.Location = new System.Drawing.Point(697, 101);
			this.ClearGameAppOptionsButton.Name = "ClearGameAppOptionsButton";
			this.ClearGameAppOptionsButton.Size = new System.Drawing.Size(75, 23);
			this.ClearGameAppOptionsButton.TabIndex = 33;
			this.ClearGameAppOptionsButton.Text = "Clear";
			this.ClearGameAppOptionsButton.UseVisualStyleBackColor = true;
			//
			//BrowseForGameAppPathFileNameButton
			//
			this.BrowseForGameAppPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForGameAppPathFileNameButton.Location = new System.Drawing.Point(697, 72);
			this.BrowseForGameAppPathFileNameButton.Name = "BrowseForGameAppPathFileNameButton";
			this.BrowseForGameAppPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForGameAppPathFileNameButton.TabIndex = 30;
			this.BrowseForGameAppPathFileNameButton.Text = "Browse...";
			this.BrowseForGameAppPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//GameAppPathFileNameTextBox
			//
			this.GameAppPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameAppPathFileNameTextBox.Location = new System.Drawing.Point(112, 72);
			this.GameAppPathFileNameTextBox.Name = "GameAppPathFileNameTextBox";
			this.GameAppPathFileNameTextBox.Size = new System.Drawing.Size(579, 22);
			this.GameAppPathFileNameTextBox.TabIndex = 29;
			//
			//ExecutableLabel
			//
			this.ExecutableLabel.AutoSize = true;
			this.ExecutableLabel.Location = new System.Drawing.Point(6, 77);
			this.ExecutableLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			this.ExecutableLabel.Name = "ExecutableLabel";
			this.ExecutableLabel.Size = new System.Drawing.Size(99, 13);
			this.ExecutableLabel.TabIndex = 28;
			this.ExecutableLabel.Text = "Executable (*.exe):";
			//
			//PackerLabel
			//
			this.PackerLabel.AutoSize = true;
			this.PackerLabel.Location = new System.Drawing.Point(6, 251);
			this.PackerLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			this.PackerLabel.Name = "PackerLabel";
			this.PackerLabel.Size = new System.Drawing.Size(67, 13);
			this.PackerLabel.TabIndex = 16;
			this.PackerLabel.Text = "Packer tool:";
			//
			//BrowseForUnpackerPathFileNameButton
			//
			this.BrowseForUnpackerPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForUnpackerPathFileNameButton.Location = new System.Drawing.Point(697, 246);
			this.BrowseForUnpackerPathFileNameButton.Name = "BrowseForUnpackerPathFileNameButton";
			this.BrowseForUnpackerPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForUnpackerPathFileNameButton.TabIndex = 18;
			this.BrowseForUnpackerPathFileNameButton.Text = "Browse...";
			this.BrowseForUnpackerPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//PackerPathFileNameTextBox
			//
			this.PackerPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.PackerPathFileNameTextBox.Location = new System.Drawing.Point(102, 246);
			this.PackerPathFileNameTextBox.Name = "PackerPathFileNameTextBox";
			this.PackerPathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			this.PackerPathFileNameTextBox.TabIndex = 17;
			//
			//ModelViewerLabel
			//
			this.ModelViewerLabel.AutoSize = true;
			this.ModelViewerLabel.Location = new System.Drawing.Point(6, 193);
			this.ModelViewerLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			this.ModelViewerLabel.Name = "ModelViewerLabel";
			this.ModelViewerLabel.Size = new System.Drawing.Size(79, 13);
			this.ModelViewerLabel.TabIndex = 13;
			this.ModelViewerLabel.Text = "Model viewer:";
			//
			//BrowseForViewerPathFileNameButton
			//
			this.BrowseForViewerPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForViewerPathFileNameButton.Location = new System.Drawing.Point(697, 188);
			this.BrowseForViewerPathFileNameButton.Name = "BrowseForViewerPathFileNameButton";
			this.BrowseForViewerPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForViewerPathFileNameButton.TabIndex = 15;
			this.BrowseForViewerPathFileNameButton.Text = "Browse...";
			this.BrowseForViewerPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//ViewerPathFileNameTextBox
			//
			this.ViewerPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ViewerPathFileNameTextBox.Location = new System.Drawing.Point(102, 188);
			this.ViewerPathFileNameTextBox.Name = "ViewerPathFileNameTextBox";
			this.ViewerPathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			this.ViewerPathFileNameTextBox.TabIndex = 14;
			//
			//CloneGameSetupButton
			//
			this.CloneGameSetupButton.Location = new System.Drawing.Point(6, 275);
			this.CloneGameSetupButton.Name = "CloneGameSetupButton";
			this.CloneGameSetupButton.Size = new System.Drawing.Size(75, 23);
			this.CloneGameSetupButton.TabIndex = 12;
			this.CloneGameSetupButton.Text = "Clone";
			this.CloneGameSetupButton.UseVisualStyleBackColor = true;
			//
			//GameNameTextBox
			//
			this.GameNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameNameTextBox.CueBannerText = "";
			this.GameNameTextBox.Location = new System.Drawing.Point(55, 19);
			this.GameNameTextBox.Name = "GameNameTextBox";
			this.GameNameTextBox.Size = new System.Drawing.Size(717, 22);
			this.GameNameTextBox.TabIndex = 1;
			//
			//NameLabel
			//
			this.NameLabel.AutoSize = true;
			this.NameLabel.Location = new System.Drawing.Point(6, 24);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(39, 13);
			this.NameLabel.TabIndex = 0;
			this.NameLabel.Text = "Name:";
			//
			//DeleteGameSetupButton
			//
			this.DeleteGameSetupButton.Location = new System.Drawing.Point(87, 275);
			this.DeleteGameSetupButton.Name = "DeleteGameSetupButton";
			this.DeleteGameSetupButton.Size = new System.Drawing.Size(75, 23);
			this.DeleteGameSetupButton.TabIndex = 8;
			this.DeleteGameSetupButton.Text = "Delete";
			this.DeleteGameSetupButton.UseVisualStyleBackColor = true;
			//
			//BrowseForGamePathFileNameButton
			//
			this.BrowseForGamePathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForGamePathFileNameButton.Location = new System.Drawing.Point(697, 130);
			this.BrowseForGamePathFileNameButton.Name = "BrowseForGamePathFileNameButton";
			this.BrowseForGamePathFileNameButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForGamePathFileNameButton.TabIndex = 4;
			this.BrowseForGamePathFileNameButton.Text = "Browse...";
			this.BrowseForGamePathFileNameButton.UseVisualStyleBackColor = true;
			//
			//GamePathFileNameTextBox
			//
			this.GamePathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GamePathFileNameTextBox.Location = new System.Drawing.Point(102, 130);
			this.GamePathFileNameTextBox.Name = "GamePathFileNameTextBox";
			this.GamePathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			this.GamePathFileNameTextBox.TabIndex = 3;
			//
			//ModelCompilerLabel
			//
			this.ModelCompilerLabel.AutoSize = true;
			this.ModelCompilerLabel.Location = new System.Drawing.Point(6, 164);
			this.ModelCompilerLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			this.ModelCompilerLabel.Name = "ModelCompilerLabel";
			this.ModelCompilerLabel.Size = new System.Drawing.Size(90, 13);
			this.ModelCompilerLabel.TabIndex = 5;
			this.ModelCompilerLabel.Text = "Model compiler:";
			//
			//BrowseForCompilerPathFileNameButton
			//
			this.BrowseForCompilerPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForCompilerPathFileNameButton.Location = new System.Drawing.Point(697, 159);
			this.BrowseForCompilerPathFileNameButton.Name = "BrowseForCompilerPathFileNameButton";
			this.BrowseForCompilerPathFileNameButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseForCompilerPathFileNameButton.TabIndex = 7;
			this.BrowseForCompilerPathFileNameButton.Text = "Browse...";
			this.BrowseForCompilerPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//CompilerPathFileNameTextBox
			//
			this.CompilerPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CompilerPathFileNameTextBox.Location = new System.Drawing.Point(102, 159);
			this.CompilerPathFileNameTextBox.Name = "CompilerPathFileNameTextBox";
			this.CompilerPathFileNameTextBox.Size = new System.Drawing.Size(589, 22);
			this.CompilerPathFileNameTextBox.TabIndex = 6;
			//
			//GamePathLabel
			//
			this.GamePathLabel.AutoSize = true;
			this.GamePathLabel.Location = new System.Drawing.Point(6, 135);
			this.GamePathLabel.Name = "GamePathLabel";
			this.GamePathLabel.Size = new System.Drawing.Size(76, 13);
			this.GamePathLabel.TabIndex = 2;
			this.GamePathLabel.Text = "GameInfo.txt:";
			//
			//GoBackButton
			//
			this.GoBackButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.GoBackButton.Enabled = false;
			this.GoBackButton.Location = new System.Drawing.Point(706, 520);
			this.GoBackButton.Name = "GoBackButton";
			this.GoBackButton.Size = new System.Drawing.Size(75, 23);
			this.GoBackButton.TabIndex = 52;
			this.GoBackButton.Text = "Go Back";
			this.GoBackButton.UseVisualStyleBackColor = true;
			//
			//SteamLibraryPathsDataGridView
			//
			this.SteamLibraryPathsDataGridView.AllowUserToAddRows = false;
			this.SteamLibraryPathsDataGridView.AllowUserToDeleteRows = false;
			this.SteamLibraryPathsDataGridView.AllowUserToResizeRows = false;
			this.SteamLibraryPathsDataGridView.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.SteamLibraryPathsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.SteamLibraryPathsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlDark;
			this.SteamLibraryPathsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.SteamLibraryPathsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.SteamLibraryPathsDataGridView.Location = new System.Drawing.Point(3, 412);
			this.SteamLibraryPathsDataGridView.MultiSelect = false;
			this.SteamLibraryPathsDataGridView.Name = "SteamLibraryPathsDataGridView";
			this.SteamLibraryPathsDataGridView.RowHeadersVisible = false;
			this.SteamLibraryPathsDataGridView.RowHeadersWidth = 25;
			this.SteamLibraryPathsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.SteamLibraryPathsDataGridView.Size = new System.Drawing.Size(604, 131);
			this.SteamLibraryPathsDataGridView.TabIndex = 49;
			//
			//Panel1
			//
			this.Panel1.Controls.Add(this.GoBackButton);
			this.Panel1.Controls.Add(this.GameSetupComboBox);
			this.Panel1.Controls.Add(this.AddLibraryPathButton);
			this.Panel1.Controls.Add(this.DeleteLibraryPathButton);
			this.Panel1.Controls.Add(this.SteamLibraryPathsDataGridView);
			this.Panel1.Controls.Add(this.Label11);
			this.Panel1.Controls.Add(this.Label10);
			this.Panel1.Controls.Add(this.BrowseForSteamAppPathFileNameButton);
			this.Panel1.Controls.Add(this.SteamAppPathFileNameTextBox);
			this.Panel1.Controls.Add(this.AddGameSetupButton);
			this.Panel1.Controls.Add(this.GameGroupBox);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel1.Location = new System.Drawing.Point(0, 0);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(784, 546);
			this.Panel1.TabIndex = 17;
			//
			//SetUpGamesUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel1);
			this.Name = "SetUpGamesUserControl";
			this.Size = new System.Drawing.Size(784, 546);
			this.GameGroupBox.ResumeLayout(false);
			this.GameGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.SteamLibraryPathsDataGridView).EndInit();
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			this.ResumeLayout(false);

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