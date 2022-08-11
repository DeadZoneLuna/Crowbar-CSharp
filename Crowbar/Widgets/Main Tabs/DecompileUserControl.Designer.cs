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
	public partial class DecompileUserControl : BaseUserControl
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
			ToolTip1 = new System.Windows.Forms.ToolTip(components);
			DebugInfoCheckBox = new System.Windows.Forms.CheckBox();
			LogFileCheckBox = new System.Windows.Forms.CheckBox();
			DeclareSequenceQciCheckBox = new System.Windows.Forms.CheckBox();
			FormatForStricterImportersCheckBox = new System.Windows.Forms.CheckBox();
			UseMixedCaseForKeywordsCheckBox = new System.Windows.Forms.CheckBox();
			RemovePathFromMaterialFileNamesCheckBox = new System.Windows.Forms.CheckBox();
			UseNonValveUvConversionCheckBox = new System.Windows.Forms.CheckBox();
			OverrideMdlVersionLabel = new System.Windows.Forms.Label();
			OverrideMdlVersionComboBox = new System.Windows.Forms.ComboBox();
			PrefixMeshFileNamesWithModelNameCheckBox = new System.Windows.Forms.CheckBox();
			Panel2 = new System.Windows.Forms.Panel();
			Label1 = new System.Windows.Forms.Label();
			DecompileComboBox = new System.Windows.Forms.ComboBox();
			MdlPathFileNameTextBox = new Crowbar.TextBoxEx();
			BrowseForMdlPathFolderOrFileNameButton = new System.Windows.Forms.Button();
			GotoMdlButton = new System.Windows.Forms.Button();
			GotoOutputPathButton = new System.Windows.Forms.Button();
			BrowseForOutputPathButton = new System.Windows.Forms.Button();
			OutputPathTextBox = new Crowbar.TextBoxEx();
			OutputSubfolderTextBox = new Crowbar.TextBoxEx();
			OutputPathComboBox = new System.Windows.Forms.ComboBox();
			Label3 = new System.Windows.Forms.Label();
			UseDefaultOutputSubfolderButton = new System.Windows.Forms.Button();
			Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			DecompileOptionsPanel = new System.Windows.Forms.Panel();
			ReCreateFilesGroupBox = new System.Windows.Forms.GroupBox();
			Panel1 = new System.Windows.Forms.Panel();
			OnlyChangedMaterialsInTextureGroupLinesCheckBox = new System.Windows.Forms.CheckBox();
			SkinFamilyOnSingleLineCheckBox = new System.Windows.Forms.CheckBox();
			TextureBmpFilesCheckBox = new System.Windows.Forms.CheckBox();
			DecompileOptionsUseDefaultsButton = new System.Windows.Forms.Button();
			ComboBox2 = new System.Windows.Forms.ComboBox();
			Label2 = new System.Windows.Forms.Label();
			IncludeDefineBoneLinesCheckBox = new System.Windows.Forms.CheckBox();
			GroupIntoQciFilesCheckBox = new System.Windows.Forms.CheckBox();
			PlaceInAnimsSubfolderCheckBox = new System.Windows.Forms.CheckBox();
			LodMeshSmdFilesCheckBox = new System.Windows.Forms.CheckBox();
			ProceduralBonesVrdFileCheckBox = new System.Windows.Forms.CheckBox();
			BoneAnimationSmdFilesCheckBox = new System.Windows.Forms.CheckBox();
			VertexAnimationVtaFileCheckBox = new System.Windows.Forms.CheckBox();
			PhysicsMeshSmdFileCheckBox = new System.Windows.Forms.CheckBox();
			ReferenceMeshSmdFileCheckBox = new System.Windows.Forms.CheckBox();
			QcFileCheckBox = new System.Windows.Forms.CheckBox();
			OptionsGroupBox = new System.Windows.Forms.GroupBox();
			Panel3 = new System.Windows.Forms.Panel();
			FolderForEachModelCheckBox = new System.Windows.Forms.CheckBox();
			DecompilerLogTextBox = new Crowbar.RichTextBoxEx();
			DecompileButtonsPanel = new System.Windows.Forms.Panel();
			DecompileButton = new System.Windows.Forms.Button();
			SkipCurrentModelButton = new System.Windows.Forms.Button();
			CancelDecompileButton = new System.Windows.Forms.Button();
			UseAllInCompileButton = new System.Windows.Forms.Button();
			Panel4 = new System.Windows.Forms.Panel();
			DecompiledFilesComboBox = new System.Windows.Forms.ComboBox();
			UseInEditButton = new System.Windows.Forms.Button();
			UseInCompileButton = new System.Windows.Forms.Button();
			GotoDecompiledFileButton = new System.Windows.Forms.Button();
			Panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).BeginInit();
			Options_LogSplitContainer.Panel1.SuspendLayout();
			Options_LogSplitContainer.Panel2.SuspendLayout();
			Options_LogSplitContainer.SuspendLayout();
			DecompileOptionsPanel.SuspendLayout();
			ReCreateFilesGroupBox.SuspendLayout();
			Panel1.SuspendLayout();
			OptionsGroupBox.SuspendLayout();
			Panel3.SuspendLayout();
			DecompileButtonsPanel.SuspendLayout();
			Panel4.SuspendLayout();
			SuspendLayout();
			//
			//DebugInfoCheckBox
			//
			DebugInfoCheckBox.AutoSize = true;
			DebugInfoCheckBox.Location = new System.Drawing.Point(3, 118);
			DebugInfoCheckBox.Name = "DebugInfoCheckBox";
			DebugInfoCheckBox.Size = new System.Drawing.Size(207, 17);
			DebugInfoCheckBox.TabIndex = 14;
			DebugInfoCheckBox.Text = "Decompile-info comments and files";
			ToolTip1.SetToolTip(DebugInfoCheckBox, "Write comments and extra files that include decompile info useful in debugging.");
			DebugInfoCheckBox.UseVisualStyleBackColor = true;
			//
			//LogFileCheckBox
			//
			LogFileCheckBox.AutoSize = true;
			LogFileCheckBox.Location = new System.Drawing.Point(3, 95);
			LogFileCheckBox.Name = "LogFileCheckBox";
			LogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			LogFileCheckBox.TabIndex = 13;
			LogFileCheckBox.Text = "Write log to a file";
			ToolTip1.SetToolTip(LogFileCheckBox, "Write the decompile log to a file.");
			LogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//DeclareSequenceQciCheckBox
			//
			DeclareSequenceQciCheckBox.AutoSize = true;
			DeclareSequenceQciCheckBox.Location = new System.Drawing.Point(3, 164);
			DeclareSequenceQciCheckBox.Name = "DeclareSequenceQciCheckBox";
			DeclareSequenceQciCheckBox.Size = new System.Drawing.Size(160, 17);
			DeclareSequenceQciCheckBox.TabIndex = 40;
			DeclareSequenceQciCheckBox.Text = "$DeclareSequence QCI file";
			ToolTip1.SetToolTip(DeclareSequenceQciCheckBox, "Write a QCI file with a $DeclareSequence line for each sequence in the MDL file. " + "Useful for getting sequences in correct order for multiplayer.");
			DeclareSequenceQciCheckBox.UseVisualStyleBackColor = true;
			//
			//FormatForStricterImportersCheckBox
			//
			FormatForStricterImportersCheckBox.AutoSize = true;
			FormatForStricterImportersCheckBox.Location = new System.Drawing.Point(3, 49);
			FormatForStricterImportersCheckBox.Name = "FormatForStricterImportersCheckBox";
			FormatForStricterImportersCheckBox.Size = new System.Drawing.Size(170, 17);
			FormatForStricterImportersCheckBox.TabIndex = 12;
			FormatForStricterImportersCheckBox.Text = "Format for stricter importers";
			ToolTip1.SetToolTip(FormatForStricterImportersCheckBox, "Write decompiled files in a format that some importers expect, but is not as easy" + " to read.");
			FormatForStricterImportersCheckBox.UseVisualStyleBackColor = true;
			//
			//UseMixedCaseForKeywordsCheckBox
			//
			UseMixedCaseForKeywordsCheckBox.AutoSize = true;
			UseMixedCaseForKeywordsCheckBox.Location = new System.Drawing.Point(20, 95);
			UseMixedCaseForKeywordsCheckBox.Name = "UseMixedCaseForKeywordsCheckBox";
			UseMixedCaseForKeywordsCheckBox.Size = new System.Drawing.Size(217, 17);
			UseMixedCaseForKeywordsCheckBox.TabIndex = 42;
			UseMixedCaseForKeywordsCheckBox.Text = "Use MixedCase for keywords (Source)";
			ToolTip1.SetToolTip(UseMixedCaseForKeywordsCheckBox, "$CommandLikeThis instead of $commandlikethis");
			UseMixedCaseForKeywordsCheckBox.UseVisualStyleBackColor = true;
			//
			//RemovePathFromMaterialFileNamesCheckBox
			//
			RemovePathFromMaterialFileNamesCheckBox.AutoSize = true;
			RemovePathFromMaterialFileNamesCheckBox.Location = new System.Drawing.Point(20, 141);
			RemovePathFromMaterialFileNamesCheckBox.Name = "RemovePathFromMaterialFileNamesCheckBox";
			RemovePathFromMaterialFileNamesCheckBox.Size = new System.Drawing.Size(219, 17);
			RemovePathFromMaterialFileNamesCheckBox.TabIndex = 41;
			RemovePathFromMaterialFileNamesCheckBox.Text = "Remove path from material file names";
			ToolTip1.SetToolTip(RemovePathFromMaterialFileNamesCheckBox, "Write only the file name in the SMD, even if a path was stored. This might cause " + "problem with $CDMaterials in QC file.");
			RemovePathFromMaterialFileNamesCheckBox.UseVisualStyleBackColor = true;
			//
			//UseNonValveUvConversionCheckBox
			//
			UseNonValveUvConversionCheckBox.AutoSize = true;
			UseNonValveUvConversionCheckBox.Location = new System.Drawing.Point(20, 164);
			UseNonValveUvConversionCheckBox.Name = "UseNonValveUvConversionCheckBox";
			UseNonValveUvConversionCheckBox.Size = new System.Drawing.Size(245, 17);
			UseNonValveUvConversionCheckBox.TabIndex = 44;
			UseNonValveUvConversionCheckBox.Text = "Use non-Valve UV conversion (GoldSource)";
			ToolTip1.SetToolTip(UseNonValveUvConversionCheckBox, "[ u=s/width ] and [ v=1-(t/height) ] instead of Valve's [ u=s/(width-1) ] and [ v" + "=1-(t/(height-1)) ]");
			UseNonValveUvConversionCheckBox.UseVisualStyleBackColor = true;
			//
			//OverrideMdlVersionLabel
			//
			OverrideMdlVersionLabel.AutoSize = true;
			OverrideMdlVersionLabel.Location = new System.Drawing.Point(3, 202);
			OverrideMdlVersionLabel.Name = "OverrideMdlVersionLabel";
			OverrideMdlVersionLabel.Size = new System.Drawing.Size(120, 13);
			OverrideMdlVersionLabel.TabIndex = 46;
			OverrideMdlVersionLabel.Text = "Override MDL version:";
			ToolTip1.SetToolTip(OverrideMdlVersionLabel, "Decompile based on this selected version instead of what is stored in MDL file.");
			//
			//OverrideMdlVersionComboBox
			//
			OverrideMdlVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			OverrideMdlVersionComboBox.FormattingEnabled = true;
			OverrideMdlVersionComboBox.Location = new System.Drawing.Point(123, 199);
			OverrideMdlVersionComboBox.Name = "OverrideMdlVersionComboBox";
			OverrideMdlVersionComboBox.Size = new System.Drawing.Size(110, 21);
			OverrideMdlVersionComboBox.TabIndex = 45;
			ToolTip1.SetToolTip(OverrideMdlVersionComboBox, "Decompile based on this selected version instead of what is stored in MDL file.");
			//
			//PrefixMeshFileNamesWithModelNameCheckBox
			//
			PrefixMeshFileNamesWithModelNameCheckBox.AutoSize = true;
			PrefixMeshFileNamesWithModelNameCheckBox.Location = new System.Drawing.Point(3, 26);
			PrefixMeshFileNamesWithModelNameCheckBox.Name = "PrefixMeshFileNamesWithModelNameCheckBox";
			PrefixMeshFileNamesWithModelNameCheckBox.Size = new System.Drawing.Size(231, 17);
			PrefixMeshFileNamesWithModelNameCheckBox.TabIndex = 47;
			PrefixMeshFileNamesWithModelNameCheckBox.Text = "Prefix mesh file names with model name";
			ToolTip1.SetToolTip(PrefixMeshFileNamesWithModelNameCheckBox, "Avoid file name conflicts.");
			PrefixMeshFileNamesWithModelNameCheckBox.UseVisualStyleBackColor = true;
			//
			//Panel2
			//
			Panel2.Controls.Add(Label1);
			Panel2.Controls.Add(DecompileComboBox);
			Panel2.Controls.Add(MdlPathFileNameTextBox);
			Panel2.Controls.Add(BrowseForMdlPathFolderOrFileNameButton);
			Panel2.Controls.Add(GotoMdlButton);
			Panel2.Controls.Add(GotoOutputPathButton);
			Panel2.Controls.Add(BrowseForOutputPathButton);
			Panel2.Controls.Add(OutputPathTextBox);
			Panel2.Controls.Add(OutputSubfolderTextBox);
			Panel2.Controls.Add(OutputPathComboBox);
			Panel2.Controls.Add(Label3);
			Panel2.Controls.Add(UseDefaultOutputSubfolderButton);
			Panel2.Controls.Add(Options_LogSplitContainer);
			Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel2.Location = new System.Drawing.Point(0, 0);
			Panel2.Margin = new System.Windows.Forms.Padding(2);
			Panel2.Name = "Panel2";
			Panel2.Size = new System.Drawing.Size(776, 536);
			Panel2.TabIndex = 8;
			//
			//Label1
			//
			Label1.Location = new System.Drawing.Point(3, 8);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(64, 13);
			Label1.TabIndex = 0;
			Label1.Text = "MDL input:";
			//
			//DecompileComboBox
			//
			DecompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			DecompileComboBox.FormattingEnabled = true;
			DecompileComboBox.Location = new System.Drawing.Point(73, 4);
			DecompileComboBox.Name = "DecompileComboBox";
			DecompileComboBox.Size = new System.Drawing.Size(140, 21);
			DecompileComboBox.TabIndex = 1;
			//
			//MdlPathFileNameTextBox
			//
			MdlPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			MdlPathFileNameTextBox.CueBannerText = "";
			MdlPathFileNameTextBox.Location = new System.Drawing.Point(219, 3);
			MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox";
			MdlPathFileNameTextBox.Size = new System.Drawing.Size(435, 22);
			MdlPathFileNameTextBox.TabIndex = 1;
			//
			//BrowseForMdlPathFolderOrFileNameButton
			//
			BrowseForMdlPathFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForMdlPathFolderOrFileNameButton.Location = new System.Drawing.Point(660, 3);
			BrowseForMdlPathFolderOrFileNameButton.Name = "BrowseForMdlPathFolderOrFileNameButton";
			BrowseForMdlPathFolderOrFileNameButton.Size = new System.Drawing.Size(64, 23);
			BrowseForMdlPathFolderOrFileNameButton.TabIndex = 2;
			BrowseForMdlPathFolderOrFileNameButton.Text = "Browse...";
			BrowseForMdlPathFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//GotoMdlButton
			//
			GotoMdlButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoMdlButton.Location = new System.Drawing.Point(730, 3);
			GotoMdlButton.Name = "GotoMdlButton";
			GotoMdlButton.Size = new System.Drawing.Size(43, 23);
			GotoMdlButton.TabIndex = 3;
			GotoMdlButton.Text = "Goto";
			GotoMdlButton.UseVisualStyleBackColor = true;
			//
			//GotoOutputPathButton
			//
			GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			GotoOutputPathButton.Name = "GotoOutputPathButton";
			GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			GotoOutputPathButton.TabIndex = 18;
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
			BrowseForOutputPathButton.TabIndex = 17;
			BrowseForOutputPathButton.Text = "Browse...";
			BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OutputPathTextBox
			//
			OutputPathTextBox.AllowDrop = true;
			OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputPathTextBox.CueBannerText = "";
			OutputPathTextBox.Location = new System.Drawing.Point(219, 32);
			OutputPathTextBox.Name = "OutputPathTextBox";
			OutputPathTextBox.Size = new System.Drawing.Size(435, 22);
			OutputPathTextBox.TabIndex = 16;
			//
			//OutputSubfolderTextBox
			//
			OutputSubfolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OutputSubfolderTextBox.CueBannerText = "";
			OutputSubfolderTextBox.Location = new System.Drawing.Point(219, 32);
			OutputSubfolderTextBox.Name = "OutputSubfolderTextBox";
			OutputSubfolderTextBox.Size = new System.Drawing.Size(435, 22);
			OutputSubfolderTextBox.TabIndex = 20;
			OutputSubfolderTextBox.Visible = false;
			//
			//OutputPathComboBox
			//
			OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			OutputPathComboBox.FormattingEnabled = true;
			OutputPathComboBox.Location = new System.Drawing.Point(73, 33);
			OutputPathComboBox.Name = "OutputPathComboBox";
			OutputPathComboBox.Size = new System.Drawing.Size(140, 21);
			OutputPathComboBox.TabIndex = 14;
			//
			//Label3
			//
			Label3.Location = new System.Drawing.Point(3, 37);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(62, 13);
			Label3.TabIndex = 13;
			Label3.Text = "Output to:";
			//
			//UseDefaultOutputSubfolderButton
			//
			UseDefaultOutputSubfolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseDefaultOutputSubfolderButton.Location = new System.Drawing.Point(660, 32);
			UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton";
			UseDefaultOutputSubfolderButton.Size = new System.Drawing.Size(113, 23);
			UseDefaultOutputSubfolderButton.TabIndex = 19;
			UseDefaultOutputSubfolderButton.Text = "Use Default";
			UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = true;
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
			Options_LogSplitContainer.Panel1.Controls.Add(DecompileOptionsPanel);
			Options_LogSplitContainer.Panel1MinSize = 45;
			//
			//Options_LogSplitContainer.Panel2
			//
			Options_LogSplitContainer.Panel2.Controls.Add(DecompilerLogTextBox);
			Options_LogSplitContainer.Panel2.Controls.Add(DecompileButtonsPanel);
			Options_LogSplitContainer.Panel2.Controls.Add(Panel4);
			Options_LogSplitContainer.Panel2MinSize = 45;
			Options_LogSplitContainer.Size = new System.Drawing.Size(770, 472);
			Options_LogSplitContainer.SplitterDistance = 250;
			Options_LogSplitContainer.TabIndex = 12;
			//
			//DecompileOptionsPanel
			//
			DecompileOptionsPanel.Controls.Add(ReCreateFilesGroupBox);
			DecompileOptionsPanel.Controls.Add(OptionsGroupBox);
			DecompileOptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			DecompileOptionsPanel.Location = new System.Drawing.Point(0, 0);
			DecompileOptionsPanel.Name = "DecompileOptionsPanel";
			DecompileOptionsPanel.Size = new System.Drawing.Size(770, 250);
			DecompileOptionsPanel.TabIndex = 8;
			//
			//ReCreateFilesGroupBox
			//
			ReCreateFilesGroupBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left);
			ReCreateFilesGroupBox.Controls.Add(Panel1);
			ReCreateFilesGroupBox.Location = new System.Drawing.Point(0, 0);
			ReCreateFilesGroupBox.Name = "ReCreateFilesGroupBox";
			ReCreateFilesGroupBox.Size = new System.Drawing.Size(522, 247);
			ReCreateFilesGroupBox.TabIndex = 0;
			ReCreateFilesGroupBox.TabStop = false;
			ReCreateFilesGroupBox.Text = "Re-Create Files";
			//
			//Panel1
			//
			Panel1.AutoScroll = true;
			Panel1.Controls.Add(UseNonValveUvConversionCheckBox);
			Panel1.Controls.Add(OnlyChangedMaterialsInTextureGroupLinesCheckBox);
			Panel1.Controls.Add(UseMixedCaseForKeywordsCheckBox);
			Panel1.Controls.Add(RemovePathFromMaterialFileNamesCheckBox);
			Panel1.Controls.Add(SkinFamilyOnSingleLineCheckBox);
			Panel1.Controls.Add(TextureBmpFilesCheckBox);
			Panel1.Controls.Add(DecompileOptionsUseDefaultsButton);
			Panel1.Controls.Add(ComboBox2);
			Panel1.Controls.Add(Label2);
			Panel1.Controls.Add(IncludeDefineBoneLinesCheckBox);
			Panel1.Controls.Add(GroupIntoQciFilesCheckBox);
			Panel1.Controls.Add(PlaceInAnimsSubfolderCheckBox);
			Panel1.Controls.Add(LodMeshSmdFilesCheckBox);
			Panel1.Controls.Add(ProceduralBonesVrdFileCheckBox);
			Panel1.Controls.Add(BoneAnimationSmdFilesCheckBox);
			Panel1.Controls.Add(VertexAnimationVtaFileCheckBox);
			Panel1.Controls.Add(PhysicsMeshSmdFileCheckBox);
			Panel1.Controls.Add(ReferenceMeshSmdFileCheckBox);
			Panel1.Controls.Add(QcFileCheckBox);
			Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel1.Location = new System.Drawing.Point(3, 18);
			Panel1.Name = "Panel1";
			Panel1.Size = new System.Drawing.Size(516, 226);
			Panel1.TabIndex = 11;
			//
			//OnlyChangedMaterialsInTextureGroupLinesCheckBox
			//
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.AutoSize = true;
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.Location = new System.Drawing.Point(20, 49);
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.Name = "OnlyChangedMaterialsInTextureGroupLinesCheckBox";
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.Size = new System.Drawing.Size(264, 17);
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.TabIndex = 43;
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.Text = "Only changed materials in $texturegroup lines";
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.UseVisualStyleBackColor = true;
			//
			//SkinFamilyOnSingleLineCheckBox
			//
			SkinFamilyOnSingleLineCheckBox.AutoSize = true;
			SkinFamilyOnSingleLineCheckBox.Location = new System.Drawing.Point(20, 26);
			SkinFamilyOnSingleLineCheckBox.Name = "SkinFamilyOnSingleLineCheckBox";
			SkinFamilyOnSingleLineCheckBox.Size = new System.Drawing.Size(258, 17);
			SkinFamilyOnSingleLineCheckBox.TabIndex = 39;
			SkinFamilyOnSingleLineCheckBox.Text = "Each $texturegroup skin-family on single line";
			SkinFamilyOnSingleLineCheckBox.UseVisualStyleBackColor = true;
			//
			//TextureBmpFilesCheckBox
			//
			TextureBmpFilesCheckBox.AutoSize = true;
			TextureBmpFilesCheckBox.Location = new System.Drawing.Point(318, 3);
			TextureBmpFilesCheckBox.Name = "TextureBmpFilesCheckBox";
			TextureBmpFilesCheckBox.Size = new System.Drawing.Size(181, 17);
			TextureBmpFilesCheckBox.TabIndex = 38;
			TextureBmpFilesCheckBox.Text = "Texture BMP files (GoldSource)";
			TextureBmpFilesCheckBox.UseVisualStyleBackColor = true;
			//
			//DecompileOptionsUseDefaultsButton
			//
			DecompileOptionsUseDefaultsButton.Location = new System.Drawing.Point(213, 197);
			DecompileOptionsUseDefaultsButton.Name = "DecompileOptionsUseDefaultsButton";
			DecompileOptionsUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			DecompileOptionsUseDefaultsButton.TabIndex = 37;
			DecompileOptionsUseDefaultsButton.Text = "Use Defaults";
			DecompileOptionsUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//ComboBox2
			//
			ComboBox2.FormattingEnabled = true;
			ComboBox2.Location = new System.Drawing.Point(80, 199);
			ComboBox2.Name = "ComboBox2";
			ComboBox2.Size = new System.Drawing.Size(125, 21);
			ComboBox2.TabIndex = 15;
			ComboBox2.Visible = false;
			//
			//Label2
			//
			Label2.AutoSize = true;
			Label2.Location = new System.Drawing.Point(3, 202);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(80, 13);
			Label2.TabIndex = 23;
			Label2.Text = "Model format:";
			Label2.Visible = false;
			//
			//IncludeDefineBoneLinesCheckBox
			//
			IncludeDefineBoneLinesCheckBox.AutoSize = true;
			IncludeDefineBoneLinesCheckBox.Location = new System.Drawing.Point(20, 72);
			IncludeDefineBoneLinesCheckBox.Name = "IncludeDefineBoneLinesCheckBox";
			IncludeDefineBoneLinesCheckBox.Size = new System.Drawing.Size(286, 17);
			IncludeDefineBoneLinesCheckBox.TabIndex = 2;
			IncludeDefineBoneLinesCheckBox.Text = "Include $definebone lines (typical for view models)";
			IncludeDefineBoneLinesCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupIntoQciFilesCheckBox
			//
			GroupIntoQciFilesCheckBox.AutoSize = true;
			GroupIntoQciFilesCheckBox.Location = new System.Drawing.Point(80, 3);
			GroupIntoQciFilesCheckBox.Name = "GroupIntoQciFilesCheckBox";
			GroupIntoQciFilesCheckBox.Size = new System.Drawing.Size(128, 17);
			GroupIntoQciFilesCheckBox.TabIndex = 1;
			GroupIntoQciFilesCheckBox.Text = "Group into QCI files";
			GroupIntoQciFilesCheckBox.UseVisualStyleBackColor = true;
			//
			//PlaceInAnimsSubfolderCheckBox
			//
			PlaceInAnimsSubfolderCheckBox.AutoSize = true;
			PlaceInAnimsSubfolderCheckBox.Location = new System.Drawing.Point(335, 141);
			PlaceInAnimsSubfolderCheckBox.Name = "PlaceInAnimsSubfolderCheckBox";
			PlaceInAnimsSubfolderCheckBox.Size = new System.Drawing.Size(159, 17);
			PlaceInAnimsSubfolderCheckBox.TabIndex = 9;
			PlaceInAnimsSubfolderCheckBox.Text = "Place in \"anims\" subfolder";
			PlaceInAnimsSubfolderCheckBox.UseVisualStyleBackColor = true;
			//
			//LodMeshSmdFilesCheckBox
			//
			LodMeshSmdFilesCheckBox.AutoSize = true;
			LodMeshSmdFilesCheckBox.Location = new System.Drawing.Point(318, 26);
			LodMeshSmdFilesCheckBox.Name = "LodMeshSmdFilesCheckBox";
			LodMeshSmdFilesCheckBox.Size = new System.Drawing.Size(129, 17);
			LodMeshSmdFilesCheckBox.TabIndex = 5;
			LodMeshSmdFilesCheckBox.Text = "LOD mesh SMD files";
			LodMeshSmdFilesCheckBox.UseVisualStyleBackColor = true;
			//
			//ProceduralBonesVrdFileCheckBox
			//
			ProceduralBonesVrdFileCheckBox.AutoSize = true;
			ProceduralBonesVrdFileCheckBox.Location = new System.Drawing.Point(318, 95);
			ProceduralBonesVrdFileCheckBox.Name = "ProceduralBonesVrdFileCheckBox";
			ProceduralBonesVrdFileCheckBox.Size = new System.Drawing.Size(160, 17);
			ProceduralBonesVrdFileCheckBox.TabIndex = 10;
			ProceduralBonesVrdFileCheckBox.Text = "Procedural bones VRD file";
			ProceduralBonesVrdFileCheckBox.UseVisualStyleBackColor = true;
			//
			//BoneAnimationSmdFilesCheckBox
			//
			BoneAnimationSmdFilesCheckBox.AutoSize = true;
			BoneAnimationSmdFilesCheckBox.Location = new System.Drawing.Point(318, 118);
			BoneAnimationSmdFilesCheckBox.Name = "BoneAnimationSmdFilesCheckBox";
			BoneAnimationSmdFilesCheckBox.Size = new System.Drawing.Size(158, 17);
			BoneAnimationSmdFilesCheckBox.TabIndex = 8;
			BoneAnimationSmdFilesCheckBox.Text = "Bone animation SMD files";
			BoneAnimationSmdFilesCheckBox.UseVisualStyleBackColor = true;
			//
			//VertexAnimationVtaFileCheckBox
			//
			VertexAnimationVtaFileCheckBox.AutoSize = true;
			VertexAnimationVtaFileCheckBox.Location = new System.Drawing.Point(318, 72);
			VertexAnimationVtaFileCheckBox.Name = "VertexAnimationVtaFileCheckBox";
			VertexAnimationVtaFileCheckBox.Size = new System.Drawing.Size(191, 17);
			VertexAnimationVtaFileCheckBox.TabIndex = 7;
			VertexAnimationVtaFileCheckBox.Text = "Vertex animation VTA file (flexes)";
			VertexAnimationVtaFileCheckBox.UseVisualStyleBackColor = true;
			//
			//PhysicsMeshSmdFileCheckBox
			//
			PhysicsMeshSmdFileCheckBox.AutoSize = true;
			PhysicsMeshSmdFileCheckBox.Location = new System.Drawing.Point(318, 49);
			PhysicsMeshSmdFileCheckBox.Name = "PhysicsMeshSmdFileCheckBox";
			PhysicsMeshSmdFileCheckBox.Size = new System.Drawing.Size(138, 17);
			PhysicsMeshSmdFileCheckBox.TabIndex = 6;
			PhysicsMeshSmdFileCheckBox.Text = "Physics mesh SMD file";
			PhysicsMeshSmdFileCheckBox.UseVisualStyleBackColor = true;
			//
			//ReferenceMeshSmdFileCheckBox
			//
			ReferenceMeshSmdFileCheckBox.AutoSize = true;
			ReferenceMeshSmdFileCheckBox.Location = new System.Drawing.Point(3, 118);
			ReferenceMeshSmdFileCheckBox.Name = "ReferenceMeshSmdFileCheckBox";
			ReferenceMeshSmdFileCheckBox.Size = new System.Drawing.Size(153, 17);
			ReferenceMeshSmdFileCheckBox.TabIndex = 3;
			ReferenceMeshSmdFileCheckBox.Text = "Reference mesh SMD file";
			ReferenceMeshSmdFileCheckBox.UseVisualStyleBackColor = true;
			//
			//QcFileCheckBox
			//
			QcFileCheckBox.AutoSize = true;
			QcFileCheckBox.Location = new System.Drawing.Point(3, 3);
			QcFileCheckBox.Name = "QcFileCheckBox";
			QcFileCheckBox.Size = new System.Drawing.Size(60, 17);
			QcFileCheckBox.TabIndex = 0;
			QcFileCheckBox.Text = "QC file";
			QcFileCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsGroupBox
			//
			OptionsGroupBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			OptionsGroupBox.Controls.Add(Panel3);
			OptionsGroupBox.Location = new System.Drawing.Point(528, 0);
			OptionsGroupBox.Name = "OptionsGroupBox";
			OptionsGroupBox.Size = new System.Drawing.Size(242, 247);
			OptionsGroupBox.TabIndex = 6;
			OptionsGroupBox.TabStop = false;
			OptionsGroupBox.Text = "Options";
			//
			//Panel3
			//
			Panel3.AutoScroll = true;
			Panel3.Controls.Add(PrefixMeshFileNamesWithModelNameCheckBox);
			Panel3.Controls.Add(OverrideMdlVersionLabel);
			Panel3.Controls.Add(OverrideMdlVersionComboBox);
			Panel3.Controls.Add(FolderForEachModelCheckBox);
			Panel3.Controls.Add(DebugInfoCheckBox);
			Panel3.Controls.Add(LogFileCheckBox);
			Panel3.Controls.Add(DeclareSequenceQciCheckBox);
			Panel3.Controls.Add(FormatForStricterImportersCheckBox);
			Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel3.Location = new System.Drawing.Point(3, 18);
			Panel3.Name = "Panel3";
			Panel3.Size = new System.Drawing.Size(236, 226);
			Panel3.TabIndex = 0;
			//
			//FolderForEachModelCheckBox
			//
			FolderForEachModelCheckBox.AutoSize = true;
			FolderForEachModelCheckBox.Location = new System.Drawing.Point(3, 3);
			FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox";
			FolderForEachModelCheckBox.Size = new System.Drawing.Size(139, 17);
			FolderForEachModelCheckBox.TabIndex = 11;
			FolderForEachModelCheckBox.Text = "Folder for each model";
			FolderForEachModelCheckBox.UseVisualStyleBackColor = true;
			//
			//DecompilerLogTextBox
			//
			DecompilerLogTextBox.CueBannerText = "";
			DecompilerLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			DecompilerLogTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			DecompilerLogTextBox.HideSelection = false;
			DecompilerLogTextBox.Location = new System.Drawing.Point(0, 26);
			DecompilerLogTextBox.Name = "DecompilerLogTextBox";
			DecompilerLogTextBox.ReadOnly = true;
			DecompilerLogTextBox.Size = new System.Drawing.Size(770, 166);
			DecompilerLogTextBox.TabIndex = 0;
			DecompilerLogTextBox.Text = "";
			DecompilerLogTextBox.WordWrap = false;
			//
			//DecompileButtonsPanel
			//
			DecompileButtonsPanel.Controls.Add(DecompileButton);
			DecompileButtonsPanel.Controls.Add(SkipCurrentModelButton);
			DecompileButtonsPanel.Controls.Add(CancelDecompileButton);
			DecompileButtonsPanel.Controls.Add(UseAllInCompileButton);
			DecompileButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			DecompileButtonsPanel.Location = new System.Drawing.Point(0, 0);
			DecompileButtonsPanel.Name = "DecompileButtonsPanel";
			DecompileButtonsPanel.Size = new System.Drawing.Size(770, 26);
			DecompileButtonsPanel.TabIndex = 7;
			//
			//DecompileButton
			//
			DecompileButton.Location = new System.Drawing.Point(0, 0);
			DecompileButton.Name = "DecompileButton";
			DecompileButton.Size = new System.Drawing.Size(120, 23);
			DecompileButton.TabIndex = 2;
			DecompileButton.Text = "&Decompile";
			DecompileButton.UseVisualStyleBackColor = true;
			//
			//SkipCurrentModelButton
			//
			SkipCurrentModelButton.Enabled = false;
			SkipCurrentModelButton.Location = new System.Drawing.Point(126, 0);
			SkipCurrentModelButton.Name = "SkipCurrentModelButton";
			SkipCurrentModelButton.Size = new System.Drawing.Size(120, 23);
			SkipCurrentModelButton.TabIndex = 3;
			SkipCurrentModelButton.Text = "Skip Current Model";
			SkipCurrentModelButton.UseVisualStyleBackColor = true;
			//
			//CancelDecompileButton
			//
			CancelDecompileButton.Enabled = false;
			CancelDecompileButton.Location = new System.Drawing.Point(252, 0);
			CancelDecompileButton.Name = "CancelDecompileButton";
			CancelDecompileButton.Size = new System.Drawing.Size(120, 23);
			CancelDecompileButton.TabIndex = 4;
			CancelDecompileButton.Text = "Cancel Decompile";
			CancelDecompileButton.UseVisualStyleBackColor = true;
			//
			//UseAllInCompileButton
			//
			UseAllInCompileButton.Enabled = false;
			UseAllInCompileButton.Location = new System.Drawing.Point(378, 0);
			UseAllInCompileButton.Name = "UseAllInCompileButton";
			UseAllInCompileButton.Size = new System.Drawing.Size(120, 23);
			UseAllInCompileButton.TabIndex = 5;
			UseAllInCompileButton.Text = "Use All in Compile";
			UseAllInCompileButton.UseVisualStyleBackColor = true;
			//
			//Panel4
			//
			Panel4.Controls.Add(DecompiledFilesComboBox);
			Panel4.Controls.Add(UseInEditButton);
			Panel4.Controls.Add(UseInCompileButton);
			Panel4.Controls.Add(GotoDecompiledFileButton);
			Panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			Panel4.Location = new System.Drawing.Point(0, 192);
			Panel4.Name = "Panel4";
			Panel4.Size = new System.Drawing.Size(770, 26);
			Panel4.TabIndex = 8;
			//
			//DecompiledFilesComboBox
			//
			DecompiledFilesComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			DecompiledFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			DecompiledFilesComboBox.FormattingEnabled = true;
			DecompiledFilesComboBox.Location = new System.Drawing.Point(0, 4);
			DecompiledFilesComboBox.Name = "DecompiledFilesComboBox";
			DecompiledFilesComboBox.Size = new System.Drawing.Size(621, 21);
			DecompiledFilesComboBox.TabIndex = 1;
			//
			//UseInEditButton
			//
			UseInEditButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseInEditButton.Enabled = false;
			UseInEditButton.Location = new System.Drawing.Point(551, 3);
			UseInEditButton.Name = "UseInEditButton";
			UseInEditButton.Size = new System.Drawing.Size(72, 23);
			UseInEditButton.TabIndex = 2;
			UseInEditButton.Text = "Use in Edit";
			UseInEditButton.UseVisualStyleBackColor = true;
			UseInEditButton.Visible = false;
			//
			//UseInCompileButton
			//
			UseInCompileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			UseInCompileButton.Enabled = false;
			UseInCompileButton.Location = new System.Drawing.Point(627, 3);
			UseInCompileButton.Name = "UseInCompileButton";
			UseInCompileButton.Size = new System.Drawing.Size(94, 23);
			UseInCompileButton.TabIndex = 3;
			UseInCompileButton.Text = "Use in Compile";
			UseInCompileButton.UseVisualStyleBackColor = true;
			//
			//GotoDecompiledFileButton
			//
			GotoDecompiledFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoDecompiledFileButton.Location = new System.Drawing.Point(727, 3);
			GotoDecompiledFileButton.Name = "GotoDecompiledFileButton";
			GotoDecompiledFileButton.Size = new System.Drawing.Size(43, 23);
			GotoDecompiledFileButton.TabIndex = 4;
			GotoDecompiledFileButton.Text = "Goto";
			GotoDecompiledFileButton.UseVisualStyleBackColor = true;
			//
			//DecompileUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Panel2);
			Name = "DecompileUserControl";
			Size = new System.Drawing.Size(776, 536);
			Panel2.ResumeLayout(false);
			Panel2.PerformLayout();
			Options_LogSplitContainer.Panel1.ResumeLayout(false);
			Options_LogSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)Options_LogSplitContainer).EndInit();
			Options_LogSplitContainer.ResumeLayout(false);
			DecompileOptionsPanel.ResumeLayout(false);
			ReCreateFilesGroupBox.ResumeLayout(false);
			Panel1.ResumeLayout(false);
			Panel1.PerformLayout();
			OptionsGroupBox.ResumeLayout(false);
			Panel3.ResumeLayout(false);
			Panel3.PerformLayout();
			DecompileButtonsPanel.ResumeLayout(false);
			Panel4.ResumeLayout(false);
			ResumeLayout(false);

			Load += new System.EventHandler(DecompileUserControl_Load);
			BrowseForMdlPathFolderOrFileNameButton.Click += new System.EventHandler(BrowseForMdlPathFolderOrFileNameButton_Click);
			GotoMdlButton.Click += new System.EventHandler(GotoMdlButton_Click);
			OutputPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragDrop);
			OutputPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(OutputPathTextBox_DragEnter);
			OutputPathTextBox.Validated += new System.EventHandler(OutputPathTextBox_Validated);
			BrowseForOutputPathButton.Click += new System.EventHandler(BrowseForOutputPathButton_Click);
			GotoOutputPathButton.Click += new System.EventHandler(GotoOutputPathButton_Click);
			UseDefaultOutputSubfolderButton.Click += new System.EventHandler(UseDefaultOutputSubfolderButton_Click);
			DecompileOptionsUseDefaultsButton.Click += new System.EventHandler(DecompileOptionsUseDefaultsButton_Click);
			DecompileButton.Click += new System.EventHandler(DecompileButton_Click);
			SkipCurrentModelButton.Click += new System.EventHandler(SkipCurrentModelButton_Click);
			CancelDecompileButton.Click += new System.EventHandler(CancelDecompileButton_Click);
			UseAllInCompileButton.Click += new System.EventHandler(UseAllInCompileButton_Click);
			UseInEditButton.Click += new System.EventHandler(UseInEditButton_Click);
			UseInCompileButton.Click += new System.EventHandler(UseInCompileButton_Click);
			GotoDecompiledFileButton.Click += new System.EventHandler(GotoDecompiledFileButton_Click);
		}
		internal System.Windows.Forms.Button DecompileButton;
		internal Crowbar.TextBoxEx MdlPathFileNameTextBox;
		internal System.Windows.Forms.Button BrowseForMdlPathFolderOrFileNameButton;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.GroupBox ReCreateFilesGroupBox;
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.CheckBox LodMeshSmdFilesCheckBox;
		internal System.Windows.Forms.CheckBox ProceduralBonesVrdFileCheckBox;
		internal System.Windows.Forms.CheckBox BoneAnimationSmdFilesCheckBox;
		internal System.Windows.Forms.CheckBox VertexAnimationVtaFileCheckBox;
		internal System.Windows.Forms.CheckBox PhysicsMeshSmdFileCheckBox;
		internal System.Windows.Forms.CheckBox DebugInfoCheckBox;
		internal System.Windows.Forms.CheckBox ReferenceMeshSmdFileCheckBox;
		internal System.Windows.Forms.CheckBox QcFileCheckBox;
		internal System.Windows.Forms.Panel Panel2;
		internal Crowbar.RichTextBoxEx DecompilerLogTextBox;
		internal System.Windows.Forms.Button CancelDecompileButton;
		internal System.Windows.Forms.Button SkipCurrentModelButton;
		internal System.Windows.Forms.ComboBox DecompileComboBox;
		internal System.Windows.Forms.SplitContainer Options_LogSplitContainer;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.ComboBox ComboBox2;
		internal System.Windows.Forms.CheckBox FormatForStricterImportersCheckBox;
		internal System.Windows.Forms.CheckBox GroupIntoQciFilesCheckBox;
		internal System.Windows.Forms.CheckBox PlaceInAnimsSubfolderCheckBox;
		internal System.Windows.Forms.CheckBox LogFileCheckBox;
		internal System.Windows.Forms.Button GotoMdlButton;
		internal System.Windows.Forms.CheckBox IncludeDefineBoneLinesCheckBox;
		internal System.Windows.Forms.CheckBox FolderForEachModelCheckBox;
		internal System.Windows.Forms.ComboBox DecompiledFilesComboBox;
		internal System.Windows.Forms.Button GotoDecompiledFileButton;
		internal System.Windows.Forms.Button UseInEditButton;
		internal System.Windows.Forms.Button UseInCompileButton;
		internal System.Windows.Forms.Button UseAllInCompileButton;
		internal System.Windows.Forms.Button DecompileOptionsUseDefaultsButton;
		internal System.Windows.Forms.ToolTip ToolTip1;
		internal System.Windows.Forms.CheckBox TextureBmpFilesCheckBox;
		internal System.Windows.Forms.CheckBox SkinFamilyOnSingleLineCheckBox;
		internal System.Windows.Forms.CheckBox DeclareSequenceQciCheckBox;
		internal System.Windows.Forms.CheckBox RemovePathFromMaterialFileNamesCheckBox;
		internal System.Windows.Forms.CheckBox UseMixedCaseForKeywordsCheckBox;
		internal System.Windows.Forms.Button GotoOutputPathButton;
		internal System.Windows.Forms.Button BrowseForOutputPathButton;
		internal Crowbar.TextBoxEx OutputPathTextBox;
		internal System.Windows.Forms.ComboBox OutputPathComboBox;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Button UseDefaultOutputSubfolderButton;
		internal System.Windows.Forms.GroupBox OptionsGroupBox;
		internal System.Windows.Forms.Panel Panel3;
		internal System.Windows.Forms.CheckBox OnlyChangedMaterialsInTextureGroupLinesCheckBox;
		internal Crowbar.TextBoxEx OutputSubfolderTextBox;
		internal System.Windows.Forms.CheckBox UseNonValveUvConversionCheckBox;
		internal System.Windows.Forms.Label OverrideMdlVersionLabel;
		internal System.Windows.Forms.ComboBox OverrideMdlVersionComboBox;
		internal CheckBox PrefixMeshFileNamesWithModelNameCheckBox;
		internal Panel DecompileButtonsPanel;
		internal Panel DecompileOptionsPanel;
		internal Panel Panel4;
	}

}