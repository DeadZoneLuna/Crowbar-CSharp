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
			this.components = new System.ComponentModel.Container();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.DebugInfoCheckBox = new System.Windows.Forms.CheckBox();
			this.LogFileCheckBox = new System.Windows.Forms.CheckBox();
			this.DeclareSequenceQciCheckBox = new System.Windows.Forms.CheckBox();
			this.FormatForStricterImportersCheckBox = new System.Windows.Forms.CheckBox();
			this.UseMixedCaseForKeywordsCheckBox = new System.Windows.Forms.CheckBox();
			this.RemovePathFromMaterialFileNamesCheckBox = new System.Windows.Forms.CheckBox();
			this.UseNonValveUvConversionCheckBox = new System.Windows.Forms.CheckBox();
			this.OverrideMdlVersionLabel = new System.Windows.Forms.Label();
			this.OverrideMdlVersionComboBox = new System.Windows.Forms.ComboBox();
			this.PrefixMeshFileNamesWithModelNameCheckBox = new System.Windows.Forms.CheckBox();
			this.Panel2 = new System.Windows.Forms.Panel();
			this.Label1 = new System.Windows.Forms.Label();
			this.DecompileComboBox = new System.Windows.Forms.ComboBox();
			this.MdlPathFileNameTextBox = new Crowbar.TextBoxEx();
			this.BrowseForMdlPathFolderOrFileNameButton = new System.Windows.Forms.Button();
			this.GotoMdlButton = new System.Windows.Forms.Button();
			this.GotoOutputPathButton = new System.Windows.Forms.Button();
			this.BrowseForOutputPathButton = new System.Windows.Forms.Button();
			this.OutputPathTextBox = new Crowbar.TextBoxEx();
			this.OutputSubfolderTextBox = new Crowbar.TextBoxEx();
			this.OutputPathComboBox = new System.Windows.Forms.ComboBox();
			this.Label3 = new System.Windows.Forms.Label();
			this.UseDefaultOutputSubfolderButton = new System.Windows.Forms.Button();
			this.Options_LogSplitContainer = new System.Windows.Forms.SplitContainer();
			this.DecompileOptionsPanel = new System.Windows.Forms.Panel();
			this.ReCreateFilesGroupBox = new System.Windows.Forms.GroupBox();
			this.Panel1 = new System.Windows.Forms.Panel();
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox = new System.Windows.Forms.CheckBox();
			this.SkinFamilyOnSingleLineCheckBox = new System.Windows.Forms.CheckBox();
			this.TextureBmpFilesCheckBox = new System.Windows.Forms.CheckBox();
			this.DecompileOptionsUseDefaultsButton = new System.Windows.Forms.Button();
			this.ComboBox2 = new System.Windows.Forms.ComboBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.IncludeDefineBoneLinesCheckBox = new System.Windows.Forms.CheckBox();
			this.GroupIntoQciFilesCheckBox = new System.Windows.Forms.CheckBox();
			this.PlaceInAnimsSubfolderCheckBox = new System.Windows.Forms.CheckBox();
			this.LodMeshSmdFilesCheckBox = new System.Windows.Forms.CheckBox();
			this.ProceduralBonesVrdFileCheckBox = new System.Windows.Forms.CheckBox();
			this.BoneAnimationSmdFilesCheckBox = new System.Windows.Forms.CheckBox();
			this.VertexAnimationVtaFileCheckBox = new System.Windows.Forms.CheckBox();
			this.PhysicsMeshSmdFileCheckBox = new System.Windows.Forms.CheckBox();
			this.ReferenceMeshSmdFileCheckBox = new System.Windows.Forms.CheckBox();
			this.QcFileCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.Panel3 = new System.Windows.Forms.Panel();
			this.FolderForEachModelCheckBox = new System.Windows.Forms.CheckBox();
			this.DecompilerLogTextBox = new Crowbar.RichTextBoxEx();
			this.DecompileButtonsPanel = new System.Windows.Forms.Panel();
			this.DecompileButton = new System.Windows.Forms.Button();
			this.SkipCurrentModelButton = new System.Windows.Forms.Button();
			this.CancelDecompileButton = new System.Windows.Forms.Button();
			this.UseAllInCompileButton = new System.Windows.Forms.Button();
			this.Panel4 = new System.Windows.Forms.Panel();
			this.DecompiledFilesComboBox = new System.Windows.Forms.ComboBox();
			this.UseInEditButton = new System.Windows.Forms.Button();
			this.UseInCompileButton = new System.Windows.Forms.Button();
			this.GotoDecompiledFileButton = new System.Windows.Forms.Button();
			this.Panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).BeginInit();
			this.Options_LogSplitContainer.Panel1.SuspendLayout();
			this.Options_LogSplitContainer.Panel2.SuspendLayout();
			this.Options_LogSplitContainer.SuspendLayout();
			this.DecompileOptionsPanel.SuspendLayout();
			this.ReCreateFilesGroupBox.SuspendLayout();
			this.Panel1.SuspendLayout();
			this.OptionsGroupBox.SuspendLayout();
			this.Panel3.SuspendLayout();
			this.DecompileButtonsPanel.SuspendLayout();
			this.Panel4.SuspendLayout();
			this.SuspendLayout();
			//
			//DebugInfoCheckBox
			//
			this.DebugInfoCheckBox.AutoSize = true;
			this.DebugInfoCheckBox.Location = new System.Drawing.Point(3, 118);
			this.DebugInfoCheckBox.Name = "DebugInfoCheckBox";
			this.DebugInfoCheckBox.Size = new System.Drawing.Size(207, 17);
			this.DebugInfoCheckBox.TabIndex = 14;
			this.DebugInfoCheckBox.Text = "Decompile-info comments and files";
			this.ToolTip1.SetToolTip(this.DebugInfoCheckBox, "Write comments and extra files that include decompile info useful in debugging.");
			this.DebugInfoCheckBox.UseVisualStyleBackColor = true;
			//
			//LogFileCheckBox
			//
			this.LogFileCheckBox.AutoSize = true;
			this.LogFileCheckBox.Location = new System.Drawing.Point(3, 95);
			this.LogFileCheckBox.Name = "LogFileCheckBox";
			this.LogFileCheckBox.Size = new System.Drawing.Size(116, 17);
			this.LogFileCheckBox.TabIndex = 13;
			this.LogFileCheckBox.Text = "Write log to a file";
			this.ToolTip1.SetToolTip(this.LogFileCheckBox, "Write the decompile log to a file.");
			this.LogFileCheckBox.UseVisualStyleBackColor = true;
			//
			//DeclareSequenceQciCheckBox
			//
			this.DeclareSequenceQciCheckBox.AutoSize = true;
			this.DeclareSequenceQciCheckBox.Location = new System.Drawing.Point(3, 164);
			this.DeclareSequenceQciCheckBox.Name = "DeclareSequenceQciCheckBox";
			this.DeclareSequenceQciCheckBox.Size = new System.Drawing.Size(160, 17);
			this.DeclareSequenceQciCheckBox.TabIndex = 40;
			this.DeclareSequenceQciCheckBox.Text = "$DeclareSequence QCI file";
			this.ToolTip1.SetToolTip(this.DeclareSequenceQciCheckBox, "Write a QCI file with a $DeclareSequence line for each sequence in the MDL file. " + "Useful for getting sequences in correct order for multiplayer.");
			this.DeclareSequenceQciCheckBox.UseVisualStyleBackColor = true;
			//
			//FormatForStricterImportersCheckBox
			//
			this.FormatForStricterImportersCheckBox.AutoSize = true;
			this.FormatForStricterImportersCheckBox.Location = new System.Drawing.Point(3, 49);
			this.FormatForStricterImportersCheckBox.Name = "FormatForStricterImportersCheckBox";
			this.FormatForStricterImportersCheckBox.Size = new System.Drawing.Size(170, 17);
			this.FormatForStricterImportersCheckBox.TabIndex = 12;
			this.FormatForStricterImportersCheckBox.Text = "Format for stricter importers";
			this.ToolTip1.SetToolTip(this.FormatForStricterImportersCheckBox, "Write decompiled files in a format that some importers expect, but is not as easy" + " to read.");
			this.FormatForStricterImportersCheckBox.UseVisualStyleBackColor = true;
			//
			//UseMixedCaseForKeywordsCheckBox
			//
			this.UseMixedCaseForKeywordsCheckBox.AutoSize = true;
			this.UseMixedCaseForKeywordsCheckBox.Location = new System.Drawing.Point(20, 95);
			this.UseMixedCaseForKeywordsCheckBox.Name = "UseMixedCaseForKeywordsCheckBox";
			this.UseMixedCaseForKeywordsCheckBox.Size = new System.Drawing.Size(217, 17);
			this.UseMixedCaseForKeywordsCheckBox.TabIndex = 42;
			this.UseMixedCaseForKeywordsCheckBox.Text = "Use MixedCase for keywords (Source)";
			this.ToolTip1.SetToolTip(this.UseMixedCaseForKeywordsCheckBox, "$CommandLikeThis instead of $commandlikethis");
			this.UseMixedCaseForKeywordsCheckBox.UseVisualStyleBackColor = true;
			//
			//RemovePathFromMaterialFileNamesCheckBox
			//
			this.RemovePathFromMaterialFileNamesCheckBox.AutoSize = true;
			this.RemovePathFromMaterialFileNamesCheckBox.Location = new System.Drawing.Point(20, 141);
			this.RemovePathFromMaterialFileNamesCheckBox.Name = "RemovePathFromMaterialFileNamesCheckBox";
			this.RemovePathFromMaterialFileNamesCheckBox.Size = new System.Drawing.Size(219, 17);
			this.RemovePathFromMaterialFileNamesCheckBox.TabIndex = 41;
			this.RemovePathFromMaterialFileNamesCheckBox.Text = "Remove path from material file names";
			this.ToolTip1.SetToolTip(this.RemovePathFromMaterialFileNamesCheckBox, "Write only the file name in the SMD, even if a path was stored. This might cause " + "problem with $CDMaterials in QC file.");
			this.RemovePathFromMaterialFileNamesCheckBox.UseVisualStyleBackColor = true;
			//
			//UseNonValveUvConversionCheckBox
			//
			this.UseNonValveUvConversionCheckBox.AutoSize = true;
			this.UseNonValveUvConversionCheckBox.Location = new System.Drawing.Point(20, 164);
			this.UseNonValveUvConversionCheckBox.Name = "UseNonValveUvConversionCheckBox";
			this.UseNonValveUvConversionCheckBox.Size = new System.Drawing.Size(245, 17);
			this.UseNonValveUvConversionCheckBox.TabIndex = 44;
			this.UseNonValveUvConversionCheckBox.Text = "Use non-Valve UV conversion (GoldSource)";
			this.ToolTip1.SetToolTip(this.UseNonValveUvConversionCheckBox, "[ u=s/width ] and [ v=1-(t/height) ] instead of Valve's [ u=s/(width-1) ] and [ v" + "=1-(t/(height-1)) ]");
			this.UseNonValveUvConversionCheckBox.UseVisualStyleBackColor = true;
			//
			//OverrideMdlVersionLabel
			//
			this.OverrideMdlVersionLabel.AutoSize = true;
			this.OverrideMdlVersionLabel.Location = new System.Drawing.Point(3, 202);
			this.OverrideMdlVersionLabel.Name = "OverrideMdlVersionLabel";
			this.OverrideMdlVersionLabel.Size = new System.Drawing.Size(120, 13);
			this.OverrideMdlVersionLabel.TabIndex = 46;
			this.OverrideMdlVersionLabel.Text = "Override MDL version:";
			this.ToolTip1.SetToolTip(this.OverrideMdlVersionLabel, "Decompile based on this selected version instead of what is stored in MDL file.");
			//
			//OverrideMdlVersionComboBox
			//
			this.OverrideMdlVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OverrideMdlVersionComboBox.FormattingEnabled = true;
			this.OverrideMdlVersionComboBox.Location = new System.Drawing.Point(123, 199);
			this.OverrideMdlVersionComboBox.Name = "OverrideMdlVersionComboBox";
			this.OverrideMdlVersionComboBox.Size = new System.Drawing.Size(110, 21);
			this.OverrideMdlVersionComboBox.TabIndex = 45;
			this.ToolTip1.SetToolTip(this.OverrideMdlVersionComboBox, "Decompile based on this selected version instead of what is stored in MDL file.");
			//
			//PrefixMeshFileNamesWithModelNameCheckBox
			//
			this.PrefixMeshFileNamesWithModelNameCheckBox.AutoSize = true;
			this.PrefixMeshFileNamesWithModelNameCheckBox.Location = new System.Drawing.Point(3, 26);
			this.PrefixMeshFileNamesWithModelNameCheckBox.Name = "PrefixMeshFileNamesWithModelNameCheckBox";
			this.PrefixMeshFileNamesWithModelNameCheckBox.Size = new System.Drawing.Size(231, 17);
			this.PrefixMeshFileNamesWithModelNameCheckBox.TabIndex = 47;
			this.PrefixMeshFileNamesWithModelNameCheckBox.Text = "Prefix mesh file names with model name";
			this.ToolTip1.SetToolTip(this.PrefixMeshFileNamesWithModelNameCheckBox, "Avoid file name conflicts.");
			this.PrefixMeshFileNamesWithModelNameCheckBox.UseVisualStyleBackColor = true;
			//
			//Panel2
			//
			this.Panel2.Controls.Add(this.Label1);
			this.Panel2.Controls.Add(this.DecompileComboBox);
			this.Panel2.Controls.Add(this.MdlPathFileNameTextBox);
			this.Panel2.Controls.Add(this.BrowseForMdlPathFolderOrFileNameButton);
			this.Panel2.Controls.Add(this.GotoMdlButton);
			this.Panel2.Controls.Add(this.GotoOutputPathButton);
			this.Panel2.Controls.Add(this.BrowseForOutputPathButton);
			this.Panel2.Controls.Add(this.OutputPathTextBox);
			this.Panel2.Controls.Add(this.OutputSubfolderTextBox);
			this.Panel2.Controls.Add(this.OutputPathComboBox);
			this.Panel2.Controls.Add(this.Label3);
			this.Panel2.Controls.Add(this.UseDefaultOutputSubfolderButton);
			this.Panel2.Controls.Add(this.Options_LogSplitContainer);
			this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel2.Location = new System.Drawing.Point(0, 0);
			this.Panel2.Margin = new System.Windows.Forms.Padding(2);
			this.Panel2.Name = "Panel2";
			this.Panel2.Size = new System.Drawing.Size(776, 536);
			this.Panel2.TabIndex = 8;
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(3, 8);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(64, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "MDL input:";
			//
			//DecompileComboBox
			//
			this.DecompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DecompileComboBox.FormattingEnabled = true;
			this.DecompileComboBox.Location = new System.Drawing.Point(73, 4);
			this.DecompileComboBox.Name = "DecompileComboBox";
			this.DecompileComboBox.Size = new System.Drawing.Size(140, 21);
			this.DecompileComboBox.TabIndex = 1;
			//
			//MdlPathFileNameTextBox
			//
			this.MdlPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.MdlPathFileNameTextBox.CueBannerText = "";
			this.MdlPathFileNameTextBox.Location = new System.Drawing.Point(219, 3);
			this.MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox";
			this.MdlPathFileNameTextBox.Size = new System.Drawing.Size(435, 22);
			this.MdlPathFileNameTextBox.TabIndex = 1;
			//
			//BrowseForMdlPathFolderOrFileNameButton
			//
			this.BrowseForMdlPathFolderOrFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForMdlPathFolderOrFileNameButton.Location = new System.Drawing.Point(660, 3);
			this.BrowseForMdlPathFolderOrFileNameButton.Name = "BrowseForMdlPathFolderOrFileNameButton";
			this.BrowseForMdlPathFolderOrFileNameButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForMdlPathFolderOrFileNameButton.TabIndex = 2;
			this.BrowseForMdlPathFolderOrFileNameButton.Text = "Browse...";
			this.BrowseForMdlPathFolderOrFileNameButton.UseVisualStyleBackColor = true;
			//
			//GotoMdlButton
			//
			this.GotoMdlButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoMdlButton.Location = new System.Drawing.Point(730, 3);
			this.GotoMdlButton.Name = "GotoMdlButton";
			this.GotoMdlButton.Size = new System.Drawing.Size(43, 23);
			this.GotoMdlButton.TabIndex = 3;
			this.GotoMdlButton.Text = "Goto";
			this.GotoMdlButton.UseVisualStyleBackColor = true;
			//
			//GotoOutputPathButton
			//
			this.GotoOutputPathButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoOutputPathButton.Location = new System.Drawing.Point(730, 32);
			this.GotoOutputPathButton.Name = "GotoOutputPathButton";
			this.GotoOutputPathButton.Size = new System.Drawing.Size(43, 23);
			this.GotoOutputPathButton.TabIndex = 18;
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
			this.BrowseForOutputPathButton.TabIndex = 17;
			this.BrowseForOutputPathButton.Text = "Browse...";
			this.BrowseForOutputPathButton.UseVisualStyleBackColor = true;
			//
			//OutputPathTextBox
			//
			this.OutputPathTextBox.AllowDrop = true;
			this.OutputPathTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputPathTextBox.CueBannerText = "";
			this.OutputPathTextBox.Location = new System.Drawing.Point(219, 32);
			this.OutputPathTextBox.Name = "OutputPathTextBox";
			this.OutputPathTextBox.Size = new System.Drawing.Size(435, 22);
			this.OutputPathTextBox.TabIndex = 16;
			//
			//OutputSubfolderTextBox
			//
			this.OutputSubfolderTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OutputSubfolderTextBox.CueBannerText = "";
			this.OutputSubfolderTextBox.Location = new System.Drawing.Point(219, 32);
			this.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox";
			this.OutputSubfolderTextBox.Size = new System.Drawing.Size(435, 22);
			this.OutputSubfolderTextBox.TabIndex = 20;
			this.OutputSubfolderTextBox.Visible = false;
			//
			//OutputPathComboBox
			//
			this.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OutputPathComboBox.FormattingEnabled = true;
			this.OutputPathComboBox.Location = new System.Drawing.Point(73, 33);
			this.OutputPathComboBox.Name = "OutputPathComboBox";
			this.OutputPathComboBox.Size = new System.Drawing.Size(140, 21);
			this.OutputPathComboBox.TabIndex = 14;
			//
			//Label3
			//
			this.Label3.Location = new System.Drawing.Point(3, 37);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(62, 13);
			this.Label3.TabIndex = 13;
			this.Label3.Text = "Output to:";
			//
			//UseDefaultOutputSubfolderButton
			//
			this.UseDefaultOutputSubfolderButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseDefaultOutputSubfolderButton.Location = new System.Drawing.Point(660, 32);
			this.UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton";
			this.UseDefaultOutputSubfolderButton.Size = new System.Drawing.Size(113, 23);
			this.UseDefaultOutputSubfolderButton.TabIndex = 19;
			this.UseDefaultOutputSubfolderButton.Text = "Use Default";
			this.UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = true;
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
			this.Options_LogSplitContainer.Panel1.Controls.Add(this.DecompileOptionsPanel);
			this.Options_LogSplitContainer.Panel1MinSize = 45;
			//
			//Options_LogSplitContainer.Panel2
			//
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.DecompilerLogTextBox);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.DecompileButtonsPanel);
			this.Options_LogSplitContainer.Panel2.Controls.Add(this.Panel4);
			this.Options_LogSplitContainer.Panel2MinSize = 45;
			this.Options_LogSplitContainer.Size = new System.Drawing.Size(770, 472);
			this.Options_LogSplitContainer.SplitterDistance = 250;
			this.Options_LogSplitContainer.TabIndex = 12;
			//
			//DecompileOptionsPanel
			//
			this.DecompileOptionsPanel.Controls.Add(this.ReCreateFilesGroupBox);
			this.DecompileOptionsPanel.Controls.Add(this.OptionsGroupBox);
			this.DecompileOptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DecompileOptionsPanel.Location = new System.Drawing.Point(0, 0);
			this.DecompileOptionsPanel.Name = "DecompileOptionsPanel";
			this.DecompileOptionsPanel.Size = new System.Drawing.Size(770, 250);
			this.DecompileOptionsPanel.TabIndex = 8;
			//
			//ReCreateFilesGroupBox
			//
			this.ReCreateFilesGroupBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left);
			this.ReCreateFilesGroupBox.Controls.Add(this.Panel1);
			this.ReCreateFilesGroupBox.Location = new System.Drawing.Point(0, 0);
			this.ReCreateFilesGroupBox.Name = "ReCreateFilesGroupBox";
			this.ReCreateFilesGroupBox.Size = new System.Drawing.Size(522, 247);
			this.ReCreateFilesGroupBox.TabIndex = 0;
			this.ReCreateFilesGroupBox.TabStop = false;
			this.ReCreateFilesGroupBox.Text = "Re-Create Files";
			//
			//Panel1
			//
			this.Panel1.AutoScroll = true;
			this.Panel1.Controls.Add(this.UseNonValveUvConversionCheckBox);
			this.Panel1.Controls.Add(this.OnlyChangedMaterialsInTextureGroupLinesCheckBox);
			this.Panel1.Controls.Add(this.UseMixedCaseForKeywordsCheckBox);
			this.Panel1.Controls.Add(this.RemovePathFromMaterialFileNamesCheckBox);
			this.Panel1.Controls.Add(this.SkinFamilyOnSingleLineCheckBox);
			this.Panel1.Controls.Add(this.TextureBmpFilesCheckBox);
			this.Panel1.Controls.Add(this.DecompileOptionsUseDefaultsButton);
			this.Panel1.Controls.Add(this.ComboBox2);
			this.Panel1.Controls.Add(this.Label2);
			this.Panel1.Controls.Add(this.IncludeDefineBoneLinesCheckBox);
			this.Panel1.Controls.Add(this.GroupIntoQciFilesCheckBox);
			this.Panel1.Controls.Add(this.PlaceInAnimsSubfolderCheckBox);
			this.Panel1.Controls.Add(this.LodMeshSmdFilesCheckBox);
			this.Panel1.Controls.Add(this.ProceduralBonesVrdFileCheckBox);
			this.Panel1.Controls.Add(this.BoneAnimationSmdFilesCheckBox);
			this.Panel1.Controls.Add(this.VertexAnimationVtaFileCheckBox);
			this.Panel1.Controls.Add(this.PhysicsMeshSmdFileCheckBox);
			this.Panel1.Controls.Add(this.ReferenceMeshSmdFileCheckBox);
			this.Panel1.Controls.Add(this.QcFileCheckBox);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel1.Location = new System.Drawing.Point(3, 18);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(516, 226);
			this.Panel1.TabIndex = 11;
			//
			//OnlyChangedMaterialsInTextureGroupLinesCheckBox
			//
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.AutoSize = true;
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Location = new System.Drawing.Point(20, 49);
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Name = "OnlyChangedMaterialsInTextureGroupLinesCheckBox";
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Size = new System.Drawing.Size(264, 17);
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.TabIndex = 43;
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Text = "Only changed materials in $texturegroup lines";
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.UseVisualStyleBackColor = true;
			//
			//SkinFamilyOnSingleLineCheckBox
			//
			this.SkinFamilyOnSingleLineCheckBox.AutoSize = true;
			this.SkinFamilyOnSingleLineCheckBox.Location = new System.Drawing.Point(20, 26);
			this.SkinFamilyOnSingleLineCheckBox.Name = "SkinFamilyOnSingleLineCheckBox";
			this.SkinFamilyOnSingleLineCheckBox.Size = new System.Drawing.Size(258, 17);
			this.SkinFamilyOnSingleLineCheckBox.TabIndex = 39;
			this.SkinFamilyOnSingleLineCheckBox.Text = "Each $texturegroup skin-family on single line";
			this.SkinFamilyOnSingleLineCheckBox.UseVisualStyleBackColor = true;
			//
			//TextureBmpFilesCheckBox
			//
			this.TextureBmpFilesCheckBox.AutoSize = true;
			this.TextureBmpFilesCheckBox.Location = new System.Drawing.Point(318, 3);
			this.TextureBmpFilesCheckBox.Name = "TextureBmpFilesCheckBox";
			this.TextureBmpFilesCheckBox.Size = new System.Drawing.Size(181, 17);
			this.TextureBmpFilesCheckBox.TabIndex = 38;
			this.TextureBmpFilesCheckBox.Text = "Texture BMP files (GoldSource)";
			this.TextureBmpFilesCheckBox.UseVisualStyleBackColor = true;
			//
			//DecompileOptionsUseDefaultsButton
			//
			this.DecompileOptionsUseDefaultsButton.Location = new System.Drawing.Point(213, 197);
			this.DecompileOptionsUseDefaultsButton.Name = "DecompileOptionsUseDefaultsButton";
			this.DecompileOptionsUseDefaultsButton.Size = new System.Drawing.Size(90, 23);
			this.DecompileOptionsUseDefaultsButton.TabIndex = 37;
			this.DecompileOptionsUseDefaultsButton.Text = "Use Defaults";
			this.DecompileOptionsUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//ComboBox2
			//
			this.ComboBox2.FormattingEnabled = true;
			this.ComboBox2.Location = new System.Drawing.Point(80, 199);
			this.ComboBox2.Name = "ComboBox2";
			this.ComboBox2.Size = new System.Drawing.Size(125, 21);
			this.ComboBox2.TabIndex = 15;
			this.ComboBox2.Visible = false;
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(3, 202);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(80, 13);
			this.Label2.TabIndex = 23;
			this.Label2.Text = "Model format:";
			this.Label2.Visible = false;
			//
			//IncludeDefineBoneLinesCheckBox
			//
			this.IncludeDefineBoneLinesCheckBox.AutoSize = true;
			this.IncludeDefineBoneLinesCheckBox.Location = new System.Drawing.Point(20, 72);
			this.IncludeDefineBoneLinesCheckBox.Name = "IncludeDefineBoneLinesCheckBox";
			this.IncludeDefineBoneLinesCheckBox.Size = new System.Drawing.Size(286, 17);
			this.IncludeDefineBoneLinesCheckBox.TabIndex = 2;
			this.IncludeDefineBoneLinesCheckBox.Text = "Include $definebone lines (typical for view models)";
			this.IncludeDefineBoneLinesCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupIntoQciFilesCheckBox
			//
			this.GroupIntoQciFilesCheckBox.AutoSize = true;
			this.GroupIntoQciFilesCheckBox.Location = new System.Drawing.Point(80, 3);
			this.GroupIntoQciFilesCheckBox.Name = "GroupIntoQciFilesCheckBox";
			this.GroupIntoQciFilesCheckBox.Size = new System.Drawing.Size(128, 17);
			this.GroupIntoQciFilesCheckBox.TabIndex = 1;
			this.GroupIntoQciFilesCheckBox.Text = "Group into QCI files";
			this.GroupIntoQciFilesCheckBox.UseVisualStyleBackColor = true;
			//
			//PlaceInAnimsSubfolderCheckBox
			//
			this.PlaceInAnimsSubfolderCheckBox.AutoSize = true;
			this.PlaceInAnimsSubfolderCheckBox.Location = new System.Drawing.Point(335, 141);
			this.PlaceInAnimsSubfolderCheckBox.Name = "PlaceInAnimsSubfolderCheckBox";
			this.PlaceInAnimsSubfolderCheckBox.Size = new System.Drawing.Size(159, 17);
			this.PlaceInAnimsSubfolderCheckBox.TabIndex = 9;
			this.PlaceInAnimsSubfolderCheckBox.Text = "Place in \"anims\" subfolder";
			this.PlaceInAnimsSubfolderCheckBox.UseVisualStyleBackColor = true;
			//
			//LodMeshSmdFilesCheckBox
			//
			this.LodMeshSmdFilesCheckBox.AutoSize = true;
			this.LodMeshSmdFilesCheckBox.Location = new System.Drawing.Point(318, 26);
			this.LodMeshSmdFilesCheckBox.Name = "LodMeshSmdFilesCheckBox";
			this.LodMeshSmdFilesCheckBox.Size = new System.Drawing.Size(129, 17);
			this.LodMeshSmdFilesCheckBox.TabIndex = 5;
			this.LodMeshSmdFilesCheckBox.Text = "LOD mesh SMD files";
			this.LodMeshSmdFilesCheckBox.UseVisualStyleBackColor = true;
			//
			//ProceduralBonesVrdFileCheckBox
			//
			this.ProceduralBonesVrdFileCheckBox.AutoSize = true;
			this.ProceduralBonesVrdFileCheckBox.Location = new System.Drawing.Point(318, 95);
			this.ProceduralBonesVrdFileCheckBox.Name = "ProceduralBonesVrdFileCheckBox";
			this.ProceduralBonesVrdFileCheckBox.Size = new System.Drawing.Size(160, 17);
			this.ProceduralBonesVrdFileCheckBox.TabIndex = 10;
			this.ProceduralBonesVrdFileCheckBox.Text = "Procedural bones VRD file";
			this.ProceduralBonesVrdFileCheckBox.UseVisualStyleBackColor = true;
			//
			//BoneAnimationSmdFilesCheckBox
			//
			this.BoneAnimationSmdFilesCheckBox.AutoSize = true;
			this.BoneAnimationSmdFilesCheckBox.Location = new System.Drawing.Point(318, 118);
			this.BoneAnimationSmdFilesCheckBox.Name = "BoneAnimationSmdFilesCheckBox";
			this.BoneAnimationSmdFilesCheckBox.Size = new System.Drawing.Size(158, 17);
			this.BoneAnimationSmdFilesCheckBox.TabIndex = 8;
			this.BoneAnimationSmdFilesCheckBox.Text = "Bone animation SMD files";
			this.BoneAnimationSmdFilesCheckBox.UseVisualStyleBackColor = true;
			//
			//VertexAnimationVtaFileCheckBox
			//
			this.VertexAnimationVtaFileCheckBox.AutoSize = true;
			this.VertexAnimationVtaFileCheckBox.Location = new System.Drawing.Point(318, 72);
			this.VertexAnimationVtaFileCheckBox.Name = "VertexAnimationVtaFileCheckBox";
			this.VertexAnimationVtaFileCheckBox.Size = new System.Drawing.Size(191, 17);
			this.VertexAnimationVtaFileCheckBox.TabIndex = 7;
			this.VertexAnimationVtaFileCheckBox.Text = "Vertex animation VTA file (flexes)";
			this.VertexAnimationVtaFileCheckBox.UseVisualStyleBackColor = true;
			//
			//PhysicsMeshSmdFileCheckBox
			//
			this.PhysicsMeshSmdFileCheckBox.AutoSize = true;
			this.PhysicsMeshSmdFileCheckBox.Location = new System.Drawing.Point(318, 49);
			this.PhysicsMeshSmdFileCheckBox.Name = "PhysicsMeshSmdFileCheckBox";
			this.PhysicsMeshSmdFileCheckBox.Size = new System.Drawing.Size(138, 17);
			this.PhysicsMeshSmdFileCheckBox.TabIndex = 6;
			this.PhysicsMeshSmdFileCheckBox.Text = "Physics mesh SMD file";
			this.PhysicsMeshSmdFileCheckBox.UseVisualStyleBackColor = true;
			//
			//ReferenceMeshSmdFileCheckBox
			//
			this.ReferenceMeshSmdFileCheckBox.AutoSize = true;
			this.ReferenceMeshSmdFileCheckBox.Location = new System.Drawing.Point(3, 118);
			this.ReferenceMeshSmdFileCheckBox.Name = "ReferenceMeshSmdFileCheckBox";
			this.ReferenceMeshSmdFileCheckBox.Size = new System.Drawing.Size(153, 17);
			this.ReferenceMeshSmdFileCheckBox.TabIndex = 3;
			this.ReferenceMeshSmdFileCheckBox.Text = "Reference mesh SMD file";
			this.ReferenceMeshSmdFileCheckBox.UseVisualStyleBackColor = true;
			//
			//QcFileCheckBox
			//
			this.QcFileCheckBox.AutoSize = true;
			this.QcFileCheckBox.Location = new System.Drawing.Point(3, 3);
			this.QcFileCheckBox.Name = "QcFileCheckBox";
			this.QcFileCheckBox.Size = new System.Drawing.Size(60, 17);
			this.QcFileCheckBox.TabIndex = 0;
			this.QcFileCheckBox.Text = "QC file";
			this.QcFileCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsGroupBox
			//
			this.OptionsGroupBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.OptionsGroupBox.Controls.Add(this.Panel3);
			this.OptionsGroupBox.Location = new System.Drawing.Point(528, 0);
			this.OptionsGroupBox.Name = "OptionsGroupBox";
			this.OptionsGroupBox.Size = new System.Drawing.Size(242, 247);
			this.OptionsGroupBox.TabIndex = 6;
			this.OptionsGroupBox.TabStop = false;
			this.OptionsGroupBox.Text = "Options";
			//
			//Panel3
			//
			this.Panel3.AutoScroll = true;
			this.Panel3.Controls.Add(this.PrefixMeshFileNamesWithModelNameCheckBox);
			this.Panel3.Controls.Add(this.OverrideMdlVersionLabel);
			this.Panel3.Controls.Add(this.OverrideMdlVersionComboBox);
			this.Panel3.Controls.Add(this.FolderForEachModelCheckBox);
			this.Panel3.Controls.Add(this.DebugInfoCheckBox);
			this.Panel3.Controls.Add(this.LogFileCheckBox);
			this.Panel3.Controls.Add(this.DeclareSequenceQciCheckBox);
			this.Panel3.Controls.Add(this.FormatForStricterImportersCheckBox);
			this.Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel3.Location = new System.Drawing.Point(3, 18);
			this.Panel3.Name = "Panel3";
			this.Panel3.Size = new System.Drawing.Size(236, 226);
			this.Panel3.TabIndex = 0;
			//
			//FolderForEachModelCheckBox
			//
			this.FolderForEachModelCheckBox.AutoSize = true;
			this.FolderForEachModelCheckBox.Location = new System.Drawing.Point(3, 3);
			this.FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox";
			this.FolderForEachModelCheckBox.Size = new System.Drawing.Size(139, 17);
			this.FolderForEachModelCheckBox.TabIndex = 11;
			this.FolderForEachModelCheckBox.Text = "Folder for each model";
			this.FolderForEachModelCheckBox.UseVisualStyleBackColor = true;
			//
			//DecompilerLogTextBox
			//
			this.DecompilerLogTextBox.CueBannerText = "";
			this.DecompilerLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DecompilerLogTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.DecompilerLogTextBox.HideSelection = false;
			this.DecompilerLogTextBox.Location = new System.Drawing.Point(0, 26);
			this.DecompilerLogTextBox.Name = "DecompilerLogTextBox";
			this.DecompilerLogTextBox.ReadOnly = true;
			this.DecompilerLogTextBox.Size = new System.Drawing.Size(770, 166);
			this.DecompilerLogTextBox.TabIndex = 0;
			this.DecompilerLogTextBox.Text = "";
			this.DecompilerLogTextBox.WordWrap = false;
			//
			//DecompileButtonsPanel
			//
			this.DecompileButtonsPanel.Controls.Add(this.DecompileButton);
			this.DecompileButtonsPanel.Controls.Add(this.SkipCurrentModelButton);
			this.DecompileButtonsPanel.Controls.Add(this.CancelDecompileButton);
			this.DecompileButtonsPanel.Controls.Add(this.UseAllInCompileButton);
			this.DecompileButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.DecompileButtonsPanel.Location = new System.Drawing.Point(0, 0);
			this.DecompileButtonsPanel.Name = "DecompileButtonsPanel";
			this.DecompileButtonsPanel.Size = new System.Drawing.Size(770, 26);
			this.DecompileButtonsPanel.TabIndex = 7;
			//
			//DecompileButton
			//
			this.DecompileButton.Location = new System.Drawing.Point(0, 0);
			this.DecompileButton.Name = "DecompileButton";
			this.DecompileButton.Size = new System.Drawing.Size(120, 23);
			this.DecompileButton.TabIndex = 2;
			this.DecompileButton.Text = "&Decompile";
			this.DecompileButton.UseVisualStyleBackColor = true;
			//
			//SkipCurrentModelButton
			//
			this.SkipCurrentModelButton.Enabled = false;
			this.SkipCurrentModelButton.Location = new System.Drawing.Point(126, 0);
			this.SkipCurrentModelButton.Name = "SkipCurrentModelButton";
			this.SkipCurrentModelButton.Size = new System.Drawing.Size(120, 23);
			this.SkipCurrentModelButton.TabIndex = 3;
			this.SkipCurrentModelButton.Text = "Skip Current Model";
			this.SkipCurrentModelButton.UseVisualStyleBackColor = true;
			//
			//CancelDecompileButton
			//
			this.CancelDecompileButton.Enabled = false;
			this.CancelDecompileButton.Location = new System.Drawing.Point(252, 0);
			this.CancelDecompileButton.Name = "CancelDecompileButton";
			this.CancelDecompileButton.Size = new System.Drawing.Size(120, 23);
			this.CancelDecompileButton.TabIndex = 4;
			this.CancelDecompileButton.Text = "Cancel Decompile";
			this.CancelDecompileButton.UseVisualStyleBackColor = true;
			//
			//UseAllInCompileButton
			//
			this.UseAllInCompileButton.Enabled = false;
			this.UseAllInCompileButton.Location = new System.Drawing.Point(378, 0);
			this.UseAllInCompileButton.Name = "UseAllInCompileButton";
			this.UseAllInCompileButton.Size = new System.Drawing.Size(120, 23);
			this.UseAllInCompileButton.TabIndex = 5;
			this.UseAllInCompileButton.Text = "Use All in Compile";
			this.UseAllInCompileButton.UseVisualStyleBackColor = true;
			//
			//Panel4
			//
			this.Panel4.Controls.Add(this.DecompiledFilesComboBox);
			this.Panel4.Controls.Add(this.UseInEditButton);
			this.Panel4.Controls.Add(this.UseInCompileButton);
			this.Panel4.Controls.Add(this.GotoDecompiledFileButton);
			this.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Panel4.Location = new System.Drawing.Point(0, 192);
			this.Panel4.Name = "Panel4";
			this.Panel4.Size = new System.Drawing.Size(770, 26);
			this.Panel4.TabIndex = 8;
			//
			//DecompiledFilesComboBox
			//
			this.DecompiledFilesComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.DecompiledFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DecompiledFilesComboBox.FormattingEnabled = true;
			this.DecompiledFilesComboBox.Location = new System.Drawing.Point(0, 4);
			this.DecompiledFilesComboBox.Name = "DecompiledFilesComboBox";
			this.DecompiledFilesComboBox.Size = new System.Drawing.Size(621, 21);
			this.DecompiledFilesComboBox.TabIndex = 1;
			//
			//UseInEditButton
			//
			this.UseInEditButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseInEditButton.Enabled = false;
			this.UseInEditButton.Location = new System.Drawing.Point(551, 3);
			this.UseInEditButton.Name = "UseInEditButton";
			this.UseInEditButton.Size = new System.Drawing.Size(72, 23);
			this.UseInEditButton.TabIndex = 2;
			this.UseInEditButton.Text = "Use in Edit";
			this.UseInEditButton.UseVisualStyleBackColor = true;
			this.UseInEditButton.Visible = false;
			//
			//UseInCompileButton
			//
			this.UseInCompileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UseInCompileButton.Enabled = false;
			this.UseInCompileButton.Location = new System.Drawing.Point(627, 3);
			this.UseInCompileButton.Name = "UseInCompileButton";
			this.UseInCompileButton.Size = new System.Drawing.Size(94, 23);
			this.UseInCompileButton.TabIndex = 3;
			this.UseInCompileButton.Text = "Use in Compile";
			this.UseInCompileButton.UseVisualStyleBackColor = true;
			//
			//GotoDecompiledFileButton
			//
			this.GotoDecompiledFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoDecompiledFileButton.Location = new System.Drawing.Point(727, 3);
			this.GotoDecompiledFileButton.Name = "GotoDecompiledFileButton";
			this.GotoDecompiledFileButton.Size = new System.Drawing.Size(43, 23);
			this.GotoDecompiledFileButton.TabIndex = 4;
			this.GotoDecompiledFileButton.Text = "Goto";
			this.GotoDecompiledFileButton.UseVisualStyleBackColor = true;
			//
			//DecompileUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel2);
			this.Name = "DecompileUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.Panel2.ResumeLayout(false);
			this.Panel2.PerformLayout();
			this.Options_LogSplitContainer.Panel1.ResumeLayout(false);
			this.Options_LogSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.Options_LogSplitContainer).EndInit();
			this.Options_LogSplitContainer.ResumeLayout(false);
			this.DecompileOptionsPanel.ResumeLayout(false);
			this.ReCreateFilesGroupBox.ResumeLayout(false);
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			this.OptionsGroupBox.ResumeLayout(false);
			this.Panel3.ResumeLayout(false);
			this.Panel3.PerformLayout();
			this.DecompileButtonsPanel.ResumeLayout(false);
			this.Panel4.ResumeLayout(false);
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			this.Load += new System.EventHandler(DecompileUserControl_Load);
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