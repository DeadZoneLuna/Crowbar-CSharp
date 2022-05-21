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
	public partial class OptionsUserControl : BaseUserControl
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
			IntegrateContextMenuItemsCheckBox = new System.Windows.Forms.CheckBox();
			Label1 = new System.Windows.Forms.Label();
			IntegrateAsSubmenuCheckBox = new System.Windows.Forms.CheckBox();
			GroupBox1 = new System.Windows.Forms.GroupBox();
			Panel7 = new System.Windows.Forms.Panel();
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox = new System.Windows.Forms.CheckBox();
			OptionsContextMenuCompileFolderCheckBox = new System.Windows.Forms.CheckBox();
			OptionsContextMenuCompileQcFileCheckBox = new System.Windows.Forms.CheckBox();
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox = new System.Windows.Forms.CheckBox();
			OptionsContextMenuDecompileFolderCheckBox = new System.Windows.Forms.CheckBox();
			OptionsContextMenuDecompileMdlFileCheckBox = new System.Windows.Forms.CheckBox();
			OptionsContextMenuViewMdlFileCheckBox = new System.Windows.Forms.CheckBox();
			OptionsContextMenuOpenWithCrowbarCheckBox = new System.Windows.Forms.CheckBox();
			ContextMenuUseDefaultsButton = new System.Windows.Forms.Button();
			AutoOpenMdlFileCheckBox = new System.Windows.Forms.CheckBox();
			AutoOpenQcFileCheckBox = new System.Windows.Forms.CheckBox();
			GroupBox2 = new System.Windows.Forms.GroupBox();
			Panel2 = new System.Windows.Forms.Panel();
			AutoOpenGmaFileForPublishRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenGmaFileForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenVpkPanel = new System.Windows.Forms.Panel();
			AutoOpenVpkFileForPublishRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenVpkFileForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			Label14 = new System.Windows.Forms.Label();
			AutoOpenFpxFileCheckBox = new System.Windows.Forms.CheckBox();
			AutoOpenGmaFileCheckBox = new System.Windows.Forms.CheckBox();
			Label5 = new System.Windows.Forms.Label();
			AutoOpenFolderPanel = new System.Windows.Forms.Panel();
			AutoOpenFolderForPackRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenFolderForCompileRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenFolderForDecompileRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenFolderForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			Label4 = new System.Windows.Forms.Label();
			Label2 = new System.Windows.Forms.Label();
			AutoOpenUseDefaultsButton = new System.Windows.Forms.Button();
			Panel1 = new System.Windows.Forms.Panel();
			Label9 = new System.Windows.Forms.Label();
			Label8 = new System.Windows.Forms.Label();
			AutoOpenMdlFileForDecompileCheckBox = new System.Windows.Forms.CheckBox();
			AutoOpenMdlFileForViewCheckBox = new System.Windows.Forms.CheckBox();
			AutoOpenMdlFileForPreviewCheckBox = new System.Windows.Forms.CheckBox();
			AutoOpenMdlFileForViewingRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenMdlFileForPreviewingRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenMdlFileForDecompilingRadioButton = new System.Windows.Forms.RadioButton();
			AutoOpenVpkFileCheckBox = new System.Windows.Forms.CheckBox();
			GroupBox3 = new System.Windows.Forms.GroupBox();
			Panel5 = new System.Windows.Forms.Panel();
			DragAndDropGmaFileForPublishRadioButton = new System.Windows.Forms.RadioButton();
			DragAndDropGmaFileForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			Panel6 = new System.Windows.Forms.Panel();
			DragAndDropVpkFileForPublishRadioButton = new System.Windows.Forms.RadioButton();
			DragAndDropVpkFileForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			Label7 = new System.Windows.Forms.Label();
			Label6 = new System.Windows.Forms.Label();
			Label13 = new System.Windows.Forms.Label();
			Label12 = new System.Windows.Forms.Label();
			Panel3 = new System.Windows.Forms.Panel();
			Label10 = new System.Windows.Forms.Label();
			Label11 = new System.Windows.Forms.Label();
			DragAndDropMdlFileForDecompileCheckBox = new System.Windows.Forms.CheckBox();
			DragAndDropMdlFileForViewCheckBox = new System.Windows.Forms.CheckBox();
			DragAndDropMdlFileForPreviewCheckBox = new System.Windows.Forms.CheckBox();
			DragAndDropMdlFileForPreviewingRadioButton = new System.Windows.Forms.RadioButton();
			DragAndDropMdlFileForDecompilingRadioButton = new System.Windows.Forms.RadioButton();
			DragAndDropMdlFileForViewingRadioButton = new System.Windows.Forms.RadioButton();
			Label3 = new System.Windows.Forms.Label();
			DragAndDropUseDefaultsButton = new System.Windows.Forms.Button();
			Panel4 = new System.Windows.Forms.Panel();
			DragAndDropFolderForPackRadioButton = new System.Windows.Forms.RadioButton();
			DragAndDropFolderForCompileRadioButton = new System.Windows.Forms.RadioButton();
			DragAndDropFolderForDecompileRadioButton = new System.Windows.Forms.RadioButton();
			DragAndDropFolderForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			ApplyLabel = new System.Windows.Forms.Label();
			ApplyButton = new System.Windows.Forms.Button();
			ApplyPanel = new System.Windows.Forms.Panel();
			SingleInstanceCheckBox = new System.Windows.Forms.CheckBox();
			GroupBox1.SuspendLayout();
			Panel7.SuspendLayout();
			GroupBox2.SuspendLayout();
			Panel2.SuspendLayout();
			AutoOpenVpkPanel.SuspendLayout();
			AutoOpenFolderPanel.SuspendLayout();
			Panel1.SuspendLayout();
			GroupBox3.SuspendLayout();
			Panel5.SuspendLayout();
			Panel6.SuspendLayout();
			Panel3.SuspendLayout();
			Panel4.SuspendLayout();
			ApplyPanel.SuspendLayout();
			SuspendLayout();
			//
			//IntegrateContextMenuItemsCheckBox
			//
			IntegrateContextMenuItemsCheckBox.AutoSize = true;
			IntegrateContextMenuItemsCheckBox.Location = new System.Drawing.Point(6, 19);
			IntegrateContextMenuItemsCheckBox.Name = "IntegrateContextMenuItemsCheckBox";
			IntegrateContextMenuItemsCheckBox.Size = new System.Drawing.Size(223, 17);
			IntegrateContextMenuItemsCheckBox.TabIndex = 0;
			IntegrateContextMenuItemsCheckBox.Text = "Integrate Crowbar context menu items";
			IntegrateContextMenuItemsCheckBox.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(24, 68);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(112, 13);
			Label1.TabIndex = 2;
			Label1.Text = "Context menu items:";
			//
			//IntegrateAsSubmenuCheckBox
			//
			IntegrateAsSubmenuCheckBox.AutoSize = true;
			IntegrateAsSubmenuCheckBox.Location = new System.Drawing.Point(24, 42);
			IntegrateAsSubmenuCheckBox.Name = "IntegrateAsSubmenuCheckBox";
			IntegrateAsSubmenuCheckBox.Size = new System.Drawing.Size(202, 17);
			IntegrateAsSubmenuCheckBox.TabIndex = 4;
			IntegrateAsSubmenuCheckBox.Text = "Integrate as a \"Crowbar\" submenu";
			IntegrateAsSubmenuCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupBox1
			//
			GroupBox1.Controls.Add(Panel7);
			GroupBox1.Controls.Add(ContextMenuUseDefaultsButton);
			GroupBox1.Controls.Add(IntegrateContextMenuItemsCheckBox);
			GroupBox1.Controls.Add(Label1);
			GroupBox1.Controls.Add(IntegrateAsSubmenuCheckBox);
			GroupBox1.Location = new System.Drawing.Point(415, 26);
			GroupBox1.Name = "GroupBox1";
			GroupBox1.Size = new System.Drawing.Size(309, 467);
			GroupBox1.TabIndex = 2;
			GroupBox1.TabStop = false;
			GroupBox1.Text = "Windows Explorer Context Menu";
			GroupBox1.Visible = false;
			//
			//Panel7
			//
			Panel7.BackColor = System.Drawing.SystemColors.Control;
			Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Panel7.Controls.Add(OptionsContextMenuCompileFolderAndSubfoldersCheckBox);
			Panel7.Controls.Add(OptionsContextMenuCompileFolderCheckBox);
			Panel7.Controls.Add(OptionsContextMenuCompileQcFileCheckBox);
			Panel7.Controls.Add(OptionsContextMenuDecompileFolderAndSubfoldersCheckBox);
			Panel7.Controls.Add(OptionsContextMenuDecompileFolderCheckBox);
			Panel7.Controls.Add(OptionsContextMenuDecompileMdlFileCheckBox);
			Panel7.Controls.Add(OptionsContextMenuViewMdlFileCheckBox);
			Panel7.Controls.Add(OptionsContextMenuOpenWithCrowbarCheckBox);
			Panel7.Location = new System.Drawing.Point(27, 84);
			Panel7.Name = "Panel7";
			Panel7.Size = new System.Drawing.Size(270, 124);
			Panel7.TabIndex = 20;
			//
			//OptionsContextMenuCompileFolderAndSubfoldersCheckBox
			//
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.AutoSize = true;
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Location = new System.Drawing.Point(3, 106);
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Name = "OptionsContextMenuCompileFolderAndSubfoldersCheckBox";
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Size = new System.Drawing.Size(247, 17);
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.TabIndex = 7;
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Text = "Compile folder and subfolders to <folder>";
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuCompileFolderCheckBox
			//
			OptionsContextMenuCompileFolderCheckBox.AutoSize = true;
			OptionsContextMenuCompileFolderCheckBox.Location = new System.Drawing.Point(3, 91);
			OptionsContextMenuCompileFolderCheckBox.Name = "OptionsContextMenuCompileFolderCheckBox";
			OptionsContextMenuCompileFolderCheckBox.Size = new System.Drawing.Size(166, 17);
			OptionsContextMenuCompileFolderCheckBox.TabIndex = 6;
			OptionsContextMenuCompileFolderCheckBox.Text = "Compile folder to <folder>";
			OptionsContextMenuCompileFolderCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuCompileQcFileCheckBox
			//
			OptionsContextMenuCompileQcFileCheckBox.AutoSize = true;
			OptionsContextMenuCompileQcFileCheckBox.Location = new System.Drawing.Point(3, 76);
			OptionsContextMenuCompileQcFileCheckBox.Name = "OptionsContextMenuCompileQcFileCheckBox";
			OptionsContextMenuCompileQcFileCheckBox.Size = new System.Drawing.Size(105, 17);
			OptionsContextMenuCompileQcFileCheckBox.TabIndex = 5;
			OptionsContextMenuCompileQcFileCheckBox.Text = "Compile QC file";
			OptionsContextMenuCompileQcFileCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuDecompileFolderAndSubfoldersCheckBox
			//
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.AutoSize = true;
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Location = new System.Drawing.Point(3, 61);
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Name = "OptionsContextMenuDecompileFolderAndSubfoldersCheckBox";
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Size = new System.Drawing.Size(259, 17);
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.TabIndex = 4;
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Text = "Decompile folder and subfolders to <folder>";
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuDecompileFolderCheckBox
			//
			OptionsContextMenuDecompileFolderCheckBox.AutoSize = true;
			OptionsContextMenuDecompileFolderCheckBox.Location = new System.Drawing.Point(3, 46);
			OptionsContextMenuDecompileFolderCheckBox.Name = "OptionsContextMenuDecompileFolderCheckBox";
			OptionsContextMenuDecompileFolderCheckBox.Size = new System.Drawing.Size(178, 17);
			OptionsContextMenuDecompileFolderCheckBox.TabIndex = 3;
			OptionsContextMenuDecompileFolderCheckBox.Text = "Decompile folder to <folder>";
			OptionsContextMenuDecompileFolderCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuDecompileMdlFileCheckBox
			//
			OptionsContextMenuDecompileMdlFileCheckBox.AutoSize = true;
			OptionsContextMenuDecompileMdlFileCheckBox.Location = new System.Drawing.Point(3, 31);
			OptionsContextMenuDecompileMdlFileCheckBox.Name = "OptionsContextMenuDecompileMdlFileCheckBox";
			OptionsContextMenuDecompileMdlFileCheckBox.Size = new System.Drawing.Size(189, 17);
			OptionsContextMenuDecompileMdlFileCheckBox.TabIndex = 2;
			OptionsContextMenuDecompileMdlFileCheckBox.Text = "Decompile MDL file to <folder>";
			OptionsContextMenuDecompileMdlFileCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuViewMdlFileCheckBox
			//
			OptionsContextMenuViewMdlFileCheckBox.AutoSize = true;
			OptionsContextMenuViewMdlFileCheckBox.Location = new System.Drawing.Point(3, 16);
			OptionsContextMenuViewMdlFileCheckBox.Name = "OptionsContextMenuViewMdlFileCheckBox";
			OptionsContextMenuViewMdlFileCheckBox.Size = new System.Drawing.Size(96, 17);
			OptionsContextMenuViewMdlFileCheckBox.TabIndex = 1;
			OptionsContextMenuViewMdlFileCheckBox.Text = "View MDL file";
			OptionsContextMenuViewMdlFileCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuOpenWithCrowbarCheckBox
			//
			OptionsContextMenuOpenWithCrowbarCheckBox.AutoSize = true;
			OptionsContextMenuOpenWithCrowbarCheckBox.Location = new System.Drawing.Point(3, 1);
			OptionsContextMenuOpenWithCrowbarCheckBox.Name = "OptionsContextMenuOpenWithCrowbarCheckBox";
			OptionsContextMenuOpenWithCrowbarCheckBox.Size = new System.Drawing.Size(128, 17);
			OptionsContextMenuOpenWithCrowbarCheckBox.TabIndex = 0;
			OptionsContextMenuOpenWithCrowbarCheckBox.Text = "Open with Crowbar";
			OptionsContextMenuOpenWithCrowbarCheckBox.UseVisualStyleBackColor = true;
			//
			//ContextMenuUseDefaultsButton
			//
			ContextMenuUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			ContextMenuUseDefaultsButton.Location = new System.Drawing.Point(104, 438);
			ContextMenuUseDefaultsButton.Name = "ContextMenuUseDefaultsButton";
			ContextMenuUseDefaultsButton.Size = new System.Drawing.Size(100, 23);
			ContextMenuUseDefaultsButton.TabIndex = 19;
			ContextMenuUseDefaultsButton.Text = "Use Defaults";
			ContextMenuUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileCheckBox
			//
			AutoOpenMdlFileCheckBox.AutoSize = true;
			AutoOpenMdlFileCheckBox.Location = new System.Drawing.Point(6, 214);
			AutoOpenMdlFileCheckBox.Name = "AutoOpenMdlFileCheckBox";
			AutoOpenMdlFileCheckBox.Size = new System.Drawing.Size(71, 17);
			AutoOpenMdlFileCheckBox.TabIndex = 2;
			AutoOpenMdlFileCheckBox.Text = "MDL file:";
			AutoOpenMdlFileCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenQcFileCheckBox
			//
			AutoOpenQcFileCheckBox.AutoSize = true;
			AutoOpenQcFileCheckBox.Location = new System.Drawing.Point(6, 307);
			AutoOpenQcFileCheckBox.Name = "AutoOpenQcFileCheckBox";
			AutoOpenQcFileCheckBox.Size = new System.Drawing.Size(63, 17);
			AutoOpenQcFileCheckBox.TabIndex = 4;
			AutoOpenQcFileCheckBox.Text = "QC file:";
			AutoOpenQcFileCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupBox2
			//
			GroupBox2.Controls.Add(Panel2);
			GroupBox2.Controls.Add(AutoOpenVpkPanel);
			GroupBox2.Controls.Add(Label14);
			GroupBox2.Controls.Add(AutoOpenFpxFileCheckBox);
			GroupBox2.Controls.Add(AutoOpenGmaFileCheckBox);
			GroupBox2.Controls.Add(Label5);
			GroupBox2.Controls.Add(AutoOpenFolderPanel);
			GroupBox2.Controls.Add(Label4);
			GroupBox2.Controls.Add(Label2);
			GroupBox2.Controls.Add(AutoOpenUseDefaultsButton);
			GroupBox2.Controls.Add(Panel1);
			GroupBox2.Controls.Add(AutoOpenMdlFileCheckBox);
			GroupBox2.Controls.Add(AutoOpenQcFileCheckBox);
			GroupBox2.Controls.Add(AutoOpenVpkFileCheckBox);
			GroupBox2.Location = new System.Drawing.Point(3, 26);
			GroupBox2.Name = "GroupBox2";
			GroupBox2.Size = new System.Drawing.Size(200, 467);
			GroupBox2.TabIndex = 0;
			GroupBox2.TabStop = false;
			GroupBox2.Text = "Windows Explorer Auto-Open";
			//
			//Panel2
			//
			Panel2.BackColor = System.Drawing.SystemColors.Control;
			Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Panel2.Controls.Add(AutoOpenGmaFileForPublishRadioButton);
			Panel2.Controls.Add(AutoOpenGmaFileForUnpackRadioButton);
			Panel2.Location = new System.Drawing.Point(81, 149);
			Panel2.Name = "Panel2";
			Panel2.Size = new System.Drawing.Size(89, 36);
			Panel2.TabIndex = 17;
			//
			//AutoOpenGmaFileForPublishRadioButton
			//
			AutoOpenGmaFileForPublishRadioButton.AutoSize = true;
			AutoOpenGmaFileForPublishRadioButton.Location = new System.Drawing.Point(3, 15);
			AutoOpenGmaFileForPublishRadioButton.Name = "AutoOpenGmaFileForPublishRadioButton";
			AutoOpenGmaFileForPublishRadioButton.Size = new System.Drawing.Size(63, 17);
			AutoOpenGmaFileForPublishRadioButton.TabIndex = 13;
			AutoOpenGmaFileForPublishRadioButton.TabStop = true;
			AutoOpenGmaFileForPublishRadioButton.Text = "Publish";
			AutoOpenGmaFileForPublishRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenGmaFileForUnpackRadioButton
			//
			AutoOpenGmaFileForUnpackRadioButton.AutoSize = true;
			AutoOpenGmaFileForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			AutoOpenGmaFileForUnpackRadioButton.Name = "AutoOpenGmaFileForUnpackRadioButton";
			AutoOpenGmaFileForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			AutoOpenGmaFileForUnpackRadioButton.TabIndex = 12;
			AutoOpenGmaFileForUnpackRadioButton.TabStop = true;
			AutoOpenGmaFileForUnpackRadioButton.Text = "Unpack";
			AutoOpenGmaFileForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenVpkPanel
			//
			AutoOpenVpkPanel.BackColor = System.Drawing.SystemColors.Control;
			AutoOpenVpkPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			AutoOpenVpkPanel.Controls.Add(AutoOpenVpkFileForPublishRadioButton);
			AutoOpenVpkPanel.Controls.Add(AutoOpenVpkFileForUnpackRadioButton);
			AutoOpenVpkPanel.Location = new System.Drawing.Point(81, 107);
			AutoOpenVpkPanel.Name = "AutoOpenVpkPanel";
			AutoOpenVpkPanel.Size = new System.Drawing.Size(89, 36);
			AutoOpenVpkPanel.TabIndex = 16;
			//
			//AutoOpenVpkFileForPublishRadioButton
			//
			AutoOpenVpkFileForPublishRadioButton.AutoSize = true;
			AutoOpenVpkFileForPublishRadioButton.Location = new System.Drawing.Point(3, 15);
			AutoOpenVpkFileForPublishRadioButton.Name = "AutoOpenVpkFileForPublishRadioButton";
			AutoOpenVpkFileForPublishRadioButton.Size = new System.Drawing.Size(63, 17);
			AutoOpenVpkFileForPublishRadioButton.TabIndex = 13;
			AutoOpenVpkFileForPublishRadioButton.TabStop = true;
			AutoOpenVpkFileForPublishRadioButton.Text = "Publish";
			AutoOpenVpkFileForPublishRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenVpkFileForUnpackRadioButton
			//
			AutoOpenVpkFileForUnpackRadioButton.AutoSize = true;
			AutoOpenVpkFileForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			AutoOpenVpkFileForUnpackRadioButton.Name = "AutoOpenVpkFileForUnpackRadioButton";
			AutoOpenVpkFileForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			AutoOpenVpkFileForUnpackRadioButton.TabIndex = 12;
			AutoOpenVpkFileForUnpackRadioButton.TabStop = true;
			AutoOpenVpkFileForUnpackRadioButton.Text = "Unpack";
			AutoOpenVpkFileForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//Label14
			//
			Label14.BackColor = System.Drawing.SystemColors.Control;
			Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Label14.Location = new System.Drawing.Point(81, 192);
			Label14.Margin = new System.Windows.Forms.Padding(3);
			Label14.Name = "Label14";
			Label14.Size = new System.Drawing.Size(89, 17);
			Label14.TabIndex = 18;
			Label14.Text = "Unpack";
			//
			//AutoOpenFpxFileCheckBox
			//
			AutoOpenFpxFileCheckBox.AutoSize = true;
			AutoOpenFpxFileCheckBox.Location = new System.Drawing.Point(6, 191);
			AutoOpenFpxFileCheckBox.Name = "AutoOpenFpxFileCheckBox";
			AutoOpenFpxFileCheckBox.Size = new System.Drawing.Size(66, 17);
			AutoOpenFpxFileCheckBox.TabIndex = 17;
			AutoOpenFpxFileCheckBox.Text = "FPX file:";
			AutoOpenFpxFileCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenGmaFileCheckBox
			//
			AutoOpenGmaFileCheckBox.AutoSize = true;
			AutoOpenGmaFileCheckBox.Location = new System.Drawing.Point(6, 151);
			AutoOpenGmaFileCheckBox.Name = "AutoOpenGmaFileCheckBox";
			AutoOpenGmaFileCheckBox.Size = new System.Drawing.Size(73, 17);
			AutoOpenGmaFileCheckBox.TabIndex = 15;
			AutoOpenGmaFileCheckBox.Text = "GMA file:";
			AutoOpenGmaFileCheckBox.UseVisualStyleBackColor = true;
			//
			//Label5
			//
			Label5.AutoSize = true;
			Label5.Location = new System.Drawing.Point(6, 330);
			Label5.Margin = new System.Windows.Forms.Padding(3);
			Label5.Name = "Label5";
			Label5.Size = new System.Drawing.Size(43, 13);
			Label5.TabIndex = 6;
			Label5.Text = "Folder:";
			//
			//AutoOpenFolderPanel
			//
			AutoOpenFolderPanel.BackColor = System.Drawing.SystemColors.Control;
			AutoOpenFolderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			AutoOpenFolderPanel.Controls.Add(AutoOpenFolderForPackRadioButton);
			AutoOpenFolderPanel.Controls.Add(AutoOpenFolderForCompileRadioButton);
			AutoOpenFolderPanel.Controls.Add(AutoOpenFolderForDecompileRadioButton);
			AutoOpenFolderPanel.Controls.Add(AutoOpenFolderForUnpackRadioButton);
			AutoOpenFolderPanel.Location = new System.Drawing.Point(81, 330);
			AutoOpenFolderPanel.Name = "AutoOpenFolderPanel";
			AutoOpenFolderPanel.Size = new System.Drawing.Size(89, 66);
			AutoOpenFolderPanel.TabIndex = 7;
			//
			//AutoOpenFolderForPackRadioButton
			//
			AutoOpenFolderForPackRadioButton.AutoSize = true;
			AutoOpenFolderForPackRadioButton.Location = new System.Drawing.Point(3, 45);
			AutoOpenFolderForPackRadioButton.Name = "AutoOpenFolderForPackRadioButton";
			AutoOpenFolderForPackRadioButton.Size = new System.Drawing.Size(48, 17);
			AutoOpenFolderForPackRadioButton.TabIndex = 15;
			AutoOpenFolderForPackRadioButton.TabStop = true;
			AutoOpenFolderForPackRadioButton.Text = "Pack";
			AutoOpenFolderForPackRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenFolderForCompileRadioButton
			//
			AutoOpenFolderForCompileRadioButton.AutoSize = true;
			AutoOpenFolderForCompileRadioButton.Location = new System.Drawing.Point(3, 30);
			AutoOpenFolderForCompileRadioButton.Name = "AutoOpenFolderForCompileRadioButton";
			AutoOpenFolderForCompileRadioButton.Size = new System.Drawing.Size(67, 17);
			AutoOpenFolderForCompileRadioButton.TabIndex = 14;
			AutoOpenFolderForCompileRadioButton.TabStop = true;
			AutoOpenFolderForCompileRadioButton.Text = "Compile";
			AutoOpenFolderForCompileRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenFolderForDecompileRadioButton
			//
			AutoOpenFolderForDecompileRadioButton.AutoSize = true;
			AutoOpenFolderForDecompileRadioButton.Location = new System.Drawing.Point(3, 15);
			AutoOpenFolderForDecompileRadioButton.Name = "AutoOpenFolderForDecompileRadioButton";
			AutoOpenFolderForDecompileRadioButton.Size = new System.Drawing.Size(79, 17);
			AutoOpenFolderForDecompileRadioButton.TabIndex = 13;
			AutoOpenFolderForDecompileRadioButton.TabStop = true;
			AutoOpenFolderForDecompileRadioButton.Text = "Decompile";
			AutoOpenFolderForDecompileRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenFolderForUnpackRadioButton
			//
			AutoOpenFolderForUnpackRadioButton.AutoSize = true;
			AutoOpenFolderForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			AutoOpenFolderForUnpackRadioButton.Name = "AutoOpenFolderForUnpackRadioButton";
			AutoOpenFolderForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			AutoOpenFolderForUnpackRadioButton.TabIndex = 12;
			AutoOpenFolderForUnpackRadioButton.TabStop = true;
			AutoOpenFolderForUnpackRadioButton.Text = "Unpack";
			AutoOpenFolderForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//Label4
			//
			Label4.BackColor = System.Drawing.SystemColors.Control;
			Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Label4.Location = new System.Drawing.Point(81, 307);
			Label4.Margin = new System.Windows.Forms.Padding(3);
			Label4.Name = "Label4";
			Label4.Size = new System.Drawing.Size(89, 17);
			Label4.TabIndex = 5;
			Label4.Text = "Compile";
			//
			//Label2
			//
			Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			Label2.Location = new System.Drawing.Point(6, 20);
			Label2.Margin = new System.Windows.Forms.Padding(3);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(188, 83);
			Label2.TabIndex = 14;
			Label2.Text = "Change the default program to Crowbar for the following file extensions and which" + " tab to set up. This includes when files or folders are dragged onto the \"Crowba" + "r.exe\" file.";
			//
			//AutoOpenUseDefaultsButton
			//
			AutoOpenUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			AutoOpenUseDefaultsButton.Location = new System.Drawing.Point(44, 438);
			AutoOpenUseDefaultsButton.Name = "AutoOpenUseDefaultsButton";
			AutoOpenUseDefaultsButton.Size = new System.Drawing.Size(100, 23);
			AutoOpenUseDefaultsButton.TabIndex = 8;
			AutoOpenUseDefaultsButton.Text = "Use Defaults";
			AutoOpenUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//Panel1
			//
			Panel1.BackColor = System.Drawing.SystemColors.Control;
			Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Panel1.Controls.Add(Label9);
			Panel1.Controls.Add(Label8);
			Panel1.Controls.Add(AutoOpenMdlFileForViewCheckBox);
			Panel1.Controls.Add(AutoOpenMdlFileForDecompileCheckBox);
			Panel1.Controls.Add(AutoOpenMdlFileForPreviewCheckBox);
			Panel1.Controls.Add(AutoOpenMdlFileForViewingRadioButton);
			Panel1.Controls.Add(AutoOpenMdlFileForPreviewingRadioButton);
			Panel1.Controls.Add(AutoOpenMdlFileForDecompilingRadioButton);
			Panel1.Location = new System.Drawing.Point(25, 231);
			Panel1.Name = "Panel1";
			Panel1.Size = new System.Drawing.Size(145, 70);
			Panel1.TabIndex = 3;
			//
			//Label9
			//
			Label9.AutoSize = true;
			Label9.Location = new System.Drawing.Point(2, 2);
			Label9.Name = "Label9";
			Label9.Size = new System.Drawing.Size(44, 13);
			Label9.TabIndex = 20;
			Label9.Text = "Set Up:";
			//
			//Label8
			//
			Label8.AutoSize = true;
			Label8.Location = new System.Drawing.Point(94, 2);
			Label8.Name = "Label8";
			Label8.Size = new System.Drawing.Size(39, 13);
			Label8.TabIndex = 19;
			Label8.Text = "Open:";
			//
			//AutoOpenMdlFileForDecompileCheckBox
			//
			AutoOpenMdlFileForDecompileCheckBox.AutoSize = true;
			AutoOpenMdlFileForDecompileCheckBox.Location = new System.Drawing.Point(3, 33);
			AutoOpenMdlFileForDecompileCheckBox.Name = "AutoOpenMdlFileForDecompileCheckBox";
			AutoOpenMdlFileForDecompileCheckBox.Size = new System.Drawing.Size(80, 17);
			AutoOpenMdlFileForDecompileCheckBox.TabIndex = 1;
			AutoOpenMdlFileForDecompileCheckBox.Text = "Decompile";
			AutoOpenMdlFileForDecompileCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForViewCheckBox
			//
			AutoOpenMdlFileForViewCheckBox.AutoSize = true;
			AutoOpenMdlFileForViewCheckBox.Location = new System.Drawing.Point(3, 48);
			AutoOpenMdlFileForViewCheckBox.Name = "AutoOpenMdlFileForViewCheckBox";
			AutoOpenMdlFileForViewCheckBox.Size = new System.Drawing.Size(51, 17);
			AutoOpenMdlFileForViewCheckBox.TabIndex = 2;
			AutoOpenMdlFileForViewCheckBox.Text = "View";
			AutoOpenMdlFileForViewCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForPreviewCheckBox
			//
			AutoOpenMdlFileForPreviewCheckBox.AutoSize = true;
			AutoOpenMdlFileForPreviewCheckBox.Location = new System.Drawing.Point(3, 18);
			AutoOpenMdlFileForPreviewCheckBox.Name = "AutoOpenMdlFileForPreviewCheckBox";
			AutoOpenMdlFileForPreviewCheckBox.Size = new System.Drawing.Size(65, 17);
			AutoOpenMdlFileForPreviewCheckBox.TabIndex = 0;
			AutoOpenMdlFileForPreviewCheckBox.Text = "Preview";
			AutoOpenMdlFileForPreviewCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForViewingRadioButton
			//
			AutoOpenMdlFileForViewingRadioButton.Location = new System.Drawing.Point(104, 49);
			AutoOpenMdlFileForViewingRadioButton.Name = "AutoOpenMdlFileForViewingRadioButton";
			AutoOpenMdlFileForViewingRadioButton.Size = new System.Drawing.Size(14, 13);
			AutoOpenMdlFileForViewingRadioButton.TabIndex = 5;
			AutoOpenMdlFileForViewingRadioButton.TabStop = true;
			AutoOpenMdlFileForViewingRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForPreviewingRadioButton
			//
			AutoOpenMdlFileForPreviewingRadioButton.Location = new System.Drawing.Point(104, 19);
			AutoOpenMdlFileForPreviewingRadioButton.Name = "AutoOpenMdlFileForPreviewingRadioButton";
			AutoOpenMdlFileForPreviewingRadioButton.Size = new System.Drawing.Size(14, 13);
			AutoOpenMdlFileForPreviewingRadioButton.TabIndex = 3;
			AutoOpenMdlFileForPreviewingRadioButton.TabStop = true;
			AutoOpenMdlFileForPreviewingRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForDecompilingRadioButton
			//
			AutoOpenMdlFileForDecompilingRadioButton.Location = new System.Drawing.Point(104, 34);
			AutoOpenMdlFileForDecompilingRadioButton.Name = "AutoOpenMdlFileForDecompilingRadioButton";
			AutoOpenMdlFileForDecompilingRadioButton.Size = new System.Drawing.Size(14, 13);
			AutoOpenMdlFileForDecompilingRadioButton.TabIndex = 4;
			AutoOpenMdlFileForDecompilingRadioButton.TabStop = true;
			AutoOpenMdlFileForDecompilingRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenVpkFileCheckBox
			//
			AutoOpenVpkFileCheckBox.AutoSize = true;
			AutoOpenVpkFileCheckBox.Location = new System.Drawing.Point(6, 108);
			AutoOpenVpkFileCheckBox.Name = "AutoOpenVpkFileCheckBox";
			AutoOpenVpkFileCheckBox.Size = new System.Drawing.Size(67, 17);
			AutoOpenVpkFileCheckBox.TabIndex = 0;
			AutoOpenVpkFileCheckBox.Text = "VPK file:";
			AutoOpenVpkFileCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupBox3
			//
			GroupBox3.Controls.Add(Panel5);
			GroupBox3.Controls.Add(Panel6);
			GroupBox3.Controls.Add(Label7);
			GroupBox3.Controls.Add(Label6);
			GroupBox3.Controls.Add(Label13);
			GroupBox3.Controls.Add(Label12);
			GroupBox3.Controls.Add(Panel3);
			GroupBox3.Controls.Add(Label3);
			GroupBox3.Controls.Add(DragAndDropUseDefaultsButton);
			GroupBox3.Controls.Add(Panel4);
			GroupBox3.Location = new System.Drawing.Point(209, 26);
			GroupBox3.Name = "GroupBox3";
			GroupBox3.Size = new System.Drawing.Size(200, 467);
			GroupBox3.TabIndex = 1;
			GroupBox3.TabStop = false;
			GroupBox3.Text = "Windows Explorer Drag-and-Drop";
			//
			//Panel5
			//
			Panel5.BackColor = System.Drawing.SystemColors.Control;
			Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Panel5.Controls.Add(DragAndDropGmaFileForPublishRadioButton);
			Panel5.Controls.Add(DragAndDropGmaFileForUnpackRadioButton);
			Panel5.Location = new System.Drawing.Point(81, 149);
			Panel5.Name = "Panel5";
			Panel5.Size = new System.Drawing.Size(89, 36);
			Panel5.TabIndex = 23;
			//
			//DragAndDropGmaFileForPublishRadioButton
			//
			DragAndDropGmaFileForPublishRadioButton.AutoSize = true;
			DragAndDropGmaFileForPublishRadioButton.Location = new System.Drawing.Point(3, 15);
			DragAndDropGmaFileForPublishRadioButton.Name = "DragAndDropGmaFileForPublishRadioButton";
			DragAndDropGmaFileForPublishRadioButton.Size = new System.Drawing.Size(63, 17);
			DragAndDropGmaFileForPublishRadioButton.TabIndex = 13;
			DragAndDropGmaFileForPublishRadioButton.TabStop = true;
			DragAndDropGmaFileForPublishRadioButton.Text = "Publish";
			DragAndDropGmaFileForPublishRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropGmaFileForUnpackRadioButton
			//
			DragAndDropGmaFileForUnpackRadioButton.AutoSize = true;
			DragAndDropGmaFileForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			DragAndDropGmaFileForUnpackRadioButton.Name = "DragAndDropGmaFileForUnpackRadioButton";
			DragAndDropGmaFileForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			DragAndDropGmaFileForUnpackRadioButton.TabIndex = 12;
			DragAndDropGmaFileForUnpackRadioButton.TabStop = true;
			DragAndDropGmaFileForUnpackRadioButton.Text = "Unpack";
			DragAndDropGmaFileForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//Panel6
			//
			Panel6.BackColor = System.Drawing.SystemColors.Control;
			Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Panel6.Controls.Add(DragAndDropVpkFileForPublishRadioButton);
			Panel6.Controls.Add(DragAndDropVpkFileForUnpackRadioButton);
			Panel6.Location = new System.Drawing.Point(81, 107);
			Panel6.Name = "Panel6";
			Panel6.Size = new System.Drawing.Size(89, 36);
			Panel6.TabIndex = 22;
			//
			//DragAndDropVpkFileForPublishRadioButton
			//
			DragAndDropVpkFileForPublishRadioButton.AutoSize = true;
			DragAndDropVpkFileForPublishRadioButton.Location = new System.Drawing.Point(3, 15);
			DragAndDropVpkFileForPublishRadioButton.Name = "DragAndDropVpkFileForPublishRadioButton";
			DragAndDropVpkFileForPublishRadioButton.Size = new System.Drawing.Size(63, 17);
			DragAndDropVpkFileForPublishRadioButton.TabIndex = 13;
			DragAndDropVpkFileForPublishRadioButton.TabStop = true;
			DragAndDropVpkFileForPublishRadioButton.Text = "Publish";
			DragAndDropVpkFileForPublishRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropVpkFileForUnpackRadioButton
			//
			DragAndDropVpkFileForUnpackRadioButton.AutoSize = true;
			DragAndDropVpkFileForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			DragAndDropVpkFileForUnpackRadioButton.Name = "DragAndDropVpkFileForUnpackRadioButton";
			DragAndDropVpkFileForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			DragAndDropVpkFileForUnpackRadioButton.TabIndex = 12;
			DragAndDropVpkFileForUnpackRadioButton.TabStop = true;
			DragAndDropVpkFileForUnpackRadioButton.Text = "Unpack";
			DragAndDropVpkFileForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//Label7
			//
			Label7.AutoSize = true;
			Label7.Location = new System.Drawing.Point(6, 152);
			Label7.Margin = new System.Windows.Forms.Padding(3);
			Label7.Name = "Label7";
			Label7.Size = new System.Drawing.Size(54, 13);
			Label7.TabIndex = 21;
			Label7.Text = "GMA file:";
			//
			//Label6
			//
			Label6.AutoSize = true;
			Label6.Location = new System.Drawing.Point(6, 109);
			Label6.Margin = new System.Windows.Forms.Padding(3);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(48, 13);
			Label6.TabIndex = 20;
			Label6.Text = "VPK file:";
			//
			//Label13
			//
			Label13.AutoSize = true;
			Label13.Location = new System.Drawing.Point(6, 287);
			Label13.Margin = new System.Windows.Forms.Padding(3);
			Label13.Name = "Label13";
			Label13.Size = new System.Drawing.Size(43, 13);
			Label13.TabIndex = 2;
			Label13.Text = "Folder:";
			//
			//Label12
			//
			Label12.AutoSize = true;
			Label12.Location = new System.Drawing.Point(6, 192);
			Label12.Margin = new System.Windows.Forms.Padding(3);
			Label12.Name = "Label12";
			Label12.Size = new System.Drawing.Size(52, 13);
			Label12.TabIndex = 0;
			Label12.Text = "MDL file:";
			//
			//Panel3
			//
			Panel3.BackColor = System.Drawing.SystemColors.Control;
			Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Panel3.Controls.Add(Label10);
			Panel3.Controls.Add(Label11);
			Panel3.Controls.Add(DragAndDropMdlFileForViewCheckBox);
			Panel3.Controls.Add(DragAndDropMdlFileForDecompileCheckBox);
			Panel3.Controls.Add(DragAndDropMdlFileForPreviewCheckBox);
			Panel3.Controls.Add(DragAndDropMdlFileForPreviewingRadioButton);
			Panel3.Controls.Add(DragAndDropMdlFileForDecompilingRadioButton);
			Panel3.Controls.Add(DragAndDropMdlFileForViewingRadioButton);
			Panel3.Location = new System.Drawing.Point(25, 207);
			Panel3.Name = "Panel3";
			Panel3.Size = new System.Drawing.Size(145, 70);
			Panel3.TabIndex = 1;
			//
			//Label10
			//
			Label10.AutoSize = true;
			Label10.Location = new System.Drawing.Point(2, 2);
			Label10.Name = "Label10";
			Label10.Size = new System.Drawing.Size(44, 13);
			Label10.TabIndex = 20;
			Label10.Text = "Set Up:";
			//
			//Label11
			//
			Label11.AutoSize = true;
			Label11.Location = new System.Drawing.Point(94, 2);
			Label11.Name = "Label11";
			Label11.Size = new System.Drawing.Size(39, 13);
			Label11.TabIndex = 19;
			Label11.Text = "Open:";
			//
			//DragAndDropMdlFileForDecompileCheckBox
			//
			DragAndDropMdlFileForDecompileCheckBox.AutoSize = true;
			DragAndDropMdlFileForDecompileCheckBox.Location = new System.Drawing.Point(3, 33);
			DragAndDropMdlFileForDecompileCheckBox.Name = "DragAndDropMdlFileForDecompileCheckBox";
			DragAndDropMdlFileForDecompileCheckBox.Size = new System.Drawing.Size(80, 17);
			DragAndDropMdlFileForDecompileCheckBox.TabIndex = 1;
			DragAndDropMdlFileForDecompileCheckBox.Text = "Decompile";
			DragAndDropMdlFileForDecompileCheckBox.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForViewCheckBox
			//
			DragAndDropMdlFileForViewCheckBox.AutoSize = true;
			DragAndDropMdlFileForViewCheckBox.Location = new System.Drawing.Point(3, 48);
			DragAndDropMdlFileForViewCheckBox.Name = "DragAndDropMdlFileForViewCheckBox";
			DragAndDropMdlFileForViewCheckBox.Size = new System.Drawing.Size(51, 17);
			DragAndDropMdlFileForViewCheckBox.TabIndex = 2;
			DragAndDropMdlFileForViewCheckBox.Text = "View";
			DragAndDropMdlFileForViewCheckBox.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForPreviewCheckBox
			//
			DragAndDropMdlFileForPreviewCheckBox.AutoSize = true;
			DragAndDropMdlFileForPreviewCheckBox.Location = new System.Drawing.Point(3, 18);
			DragAndDropMdlFileForPreviewCheckBox.Name = "DragAndDropMdlFileForPreviewCheckBox";
			DragAndDropMdlFileForPreviewCheckBox.Size = new System.Drawing.Size(65, 17);
			DragAndDropMdlFileForPreviewCheckBox.TabIndex = 0;
			DragAndDropMdlFileForPreviewCheckBox.Text = "Preview";
			DragAndDropMdlFileForPreviewCheckBox.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForPreviewingRadioButton
			//
			DragAndDropMdlFileForPreviewingRadioButton.Location = new System.Drawing.Point(104, 19);
			DragAndDropMdlFileForPreviewingRadioButton.Name = "DragAndDropMdlFileForPreviewingRadioButton";
			DragAndDropMdlFileForPreviewingRadioButton.Size = new System.Drawing.Size(14, 13);
			DragAndDropMdlFileForPreviewingRadioButton.TabIndex = 3;
			DragAndDropMdlFileForPreviewingRadioButton.TabStop = true;
			DragAndDropMdlFileForPreviewingRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForDecompilingRadioButton
			//
			DragAndDropMdlFileForDecompilingRadioButton.Location = new System.Drawing.Point(104, 34);
			DragAndDropMdlFileForDecompilingRadioButton.Name = "DragAndDropMdlFileForDecompilingRadioButton";
			DragAndDropMdlFileForDecompilingRadioButton.Size = new System.Drawing.Size(14, 13);
			DragAndDropMdlFileForDecompilingRadioButton.TabIndex = 4;
			DragAndDropMdlFileForDecompilingRadioButton.TabStop = true;
			DragAndDropMdlFileForDecompilingRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForViewingRadioButton
			//
			DragAndDropMdlFileForViewingRadioButton.Location = new System.Drawing.Point(104, 49);
			DragAndDropMdlFileForViewingRadioButton.Name = "DragAndDropMdlFileForViewingRadioButton";
			DragAndDropMdlFileForViewingRadioButton.Size = new System.Drawing.Size(14, 13);
			DragAndDropMdlFileForViewingRadioButton.TabIndex = 5;
			DragAndDropMdlFileForViewingRadioButton.TabStop = true;
			DragAndDropMdlFileForViewingRadioButton.UseVisualStyleBackColor = true;
			//
			//Label3
			//
			Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			Label3.Location = new System.Drawing.Point(6, 20);
			Label3.Margin = new System.Windows.Forms.Padding(3);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(188, 83);
			Label3.TabIndex = 19;
			Label3.Text = "Choose which tabs Crowbar sets up and which tab opens when a file or folder is dr" + "opped on Crowbar. Dropping on any tab left unset below will only set up that tab" + ".";
			//
			//DragAndDropUseDefaultsButton
			//
			DragAndDropUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			DragAndDropUseDefaultsButton.Location = new System.Drawing.Point(50, 438);
			DragAndDropUseDefaultsButton.Name = "DragAndDropUseDefaultsButton";
			DragAndDropUseDefaultsButton.Size = new System.Drawing.Size(100, 23);
			DragAndDropUseDefaultsButton.TabIndex = 4;
			DragAndDropUseDefaultsButton.Text = "Use Defaults";
			DragAndDropUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//Panel4
			//
			Panel4.BackColor = System.Drawing.SystemColors.Control;
			Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			Panel4.Controls.Add(DragAndDropFolderForPackRadioButton);
			Panel4.Controls.Add(DragAndDropFolderForCompileRadioButton);
			Panel4.Controls.Add(DragAndDropFolderForDecompileRadioButton);
			Panel4.Controls.Add(DragAndDropFolderForUnpackRadioButton);
			Panel4.Location = new System.Drawing.Point(81, 283);
			Panel4.Name = "Panel4";
			Panel4.Size = new System.Drawing.Size(89, 66);
			Panel4.TabIndex = 3;
			//
			//DragAndDropFolderForPackRadioButton
			//
			DragAndDropFolderForPackRadioButton.AutoSize = true;
			DragAndDropFolderForPackRadioButton.Location = new System.Drawing.Point(3, 45);
			DragAndDropFolderForPackRadioButton.Name = "DragAndDropFolderForPackRadioButton";
			DragAndDropFolderForPackRadioButton.Size = new System.Drawing.Size(48, 17);
			DragAndDropFolderForPackRadioButton.TabIndex = 3;
			DragAndDropFolderForPackRadioButton.TabStop = true;
			DragAndDropFolderForPackRadioButton.Text = "Pack";
			DragAndDropFolderForPackRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropFolderForCompileRadioButton
			//
			DragAndDropFolderForCompileRadioButton.AutoSize = true;
			DragAndDropFolderForCompileRadioButton.Location = new System.Drawing.Point(3, 30);
			DragAndDropFolderForCompileRadioButton.Name = "DragAndDropFolderForCompileRadioButton";
			DragAndDropFolderForCompileRadioButton.Size = new System.Drawing.Size(67, 17);
			DragAndDropFolderForCompileRadioButton.TabIndex = 2;
			DragAndDropFolderForCompileRadioButton.TabStop = true;
			DragAndDropFolderForCompileRadioButton.Text = "Compile";
			DragAndDropFolderForCompileRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropFolderForDecompileRadioButton
			//
			DragAndDropFolderForDecompileRadioButton.AutoSize = true;
			DragAndDropFolderForDecompileRadioButton.Location = new System.Drawing.Point(3, 15);
			DragAndDropFolderForDecompileRadioButton.Name = "DragAndDropFolderForDecompileRadioButton";
			DragAndDropFolderForDecompileRadioButton.Size = new System.Drawing.Size(79, 17);
			DragAndDropFolderForDecompileRadioButton.TabIndex = 1;
			DragAndDropFolderForDecompileRadioButton.TabStop = true;
			DragAndDropFolderForDecompileRadioButton.Text = "Decompile";
			DragAndDropFolderForDecompileRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropFolderForUnpackRadioButton
			//
			DragAndDropFolderForUnpackRadioButton.AutoSize = true;
			DragAndDropFolderForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			DragAndDropFolderForUnpackRadioButton.Name = "DragAndDropFolderForUnpackRadioButton";
			DragAndDropFolderForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			DragAndDropFolderForUnpackRadioButton.TabIndex = 0;
			DragAndDropFolderForUnpackRadioButton.TabStop = true;
			DragAndDropFolderForUnpackRadioButton.Text = "Unpack";
			DragAndDropFolderForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//ApplyLabel
			//
			ApplyLabel.AutoSize = true;
			ApplyLabel.Location = new System.Drawing.Point(6, 8);
			ApplyLabel.Name = "ApplyLabel";
			ApplyLabel.Size = new System.Drawing.Size(509, 13);
			ApplyLabel.TabIndex = 18;
			ApplyLabel.Text = "Windows is not using what is specified above. Click the Apply button to apply the" + " options above.";
			//
			//ApplyButton
			//
			ApplyButton.Location = new System.Drawing.Point(521, 3);
			ApplyButton.Name = "ApplyButton";
			ApplyButton.Size = new System.Drawing.Size(50, 23);
			ApplyButton.TabIndex = 20;
			ApplyButton.Text = "Apply";
			ApplyButton.UseVisualStyleBackColor = true;
			//
			//ApplyPanel
			//
			ApplyPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			ApplyPanel.Controls.Add(ApplyLabel);
			ApplyPanel.Controls.Add(ApplyButton);
			ApplyPanel.Location = new System.Drawing.Point(3, 499);
			ApplyPanel.Name = "ApplyPanel";
			ApplyPanel.Size = new System.Drawing.Size(585, 34);
			ApplyPanel.TabIndex = 3;
			ApplyPanel.Visible = false;
			//
			//SingleInstanceCheckBox
			//
			SingleInstanceCheckBox.AutoSize = true;
			SingleInstanceCheckBox.Location = new System.Drawing.Point(3, 3);
			SingleInstanceCheckBox.Name = "SingleInstanceCheckBox";
			SingleInstanceCheckBox.Size = new System.Drawing.Size(336, 17);
			SingleInstanceCheckBox.TabIndex = 4;
			SingleInstanceCheckBox.Text = "Restrict to single instance (only one Crowbar open at a time)";
			SingleInstanceCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(SingleInstanceCheckBox);
			Controls.Add(ApplyPanel);
			Controls.Add(GroupBox3);
			Controls.Add(GroupBox2);
			Controls.Add(GroupBox1);
			Name = "OptionsUserControl";
			Size = new System.Drawing.Size(776, 536);
			GroupBox1.ResumeLayout(false);
			GroupBox1.PerformLayout();
			Panel7.ResumeLayout(false);
			Panel7.PerformLayout();
			GroupBox2.ResumeLayout(false);
			GroupBox2.PerformLayout();
			Panel2.ResumeLayout(false);
			Panel2.PerformLayout();
			AutoOpenVpkPanel.ResumeLayout(false);
			AutoOpenVpkPanel.PerformLayout();
			AutoOpenFolderPanel.ResumeLayout(false);
			AutoOpenFolderPanel.PerformLayout();
			Panel1.ResumeLayout(false);
			Panel1.PerformLayout();
			GroupBox3.ResumeLayout(false);
			GroupBox3.PerformLayout();
			Panel5.ResumeLayout(false);
			Panel5.PerformLayout();
			Panel6.ResumeLayout(false);
			Panel6.PerformLayout();
			Panel3.ResumeLayout(false);
			Panel3.PerformLayout();
			Panel4.ResumeLayout(false);
			Panel4.PerformLayout();
			ApplyPanel.ResumeLayout(false);
			ApplyPanel.PerformLayout();
			ResumeLayout(false);
			PerformLayout();

//INSTANT C# NOTE: Converted design-time event handler wireups:
			Disposed += new System.EventHandler(OptionsUserControl_Disposed);
			AutoOpenVpkFileForUnpackRadioButton.CheckedChanged += new System.EventHandler(AutoOpenVpkFileRadioButton_CheckedChanged);
			AutoOpenVpkFileForPublishRadioButton.CheckedChanged += new System.EventHandler(AutoOpenVpkFileRadioButton_CheckedChanged);
			AutoOpenGmaFileForUnpackRadioButton.CheckedChanged += new System.EventHandler(AutoOpenGmaFileRadioButton_CheckedChanged);
			AutoOpenGmaFileForPublishRadioButton.CheckedChanged += new System.EventHandler(AutoOpenGmaFileRadioButton_CheckedChanged);
			AutoOpenMdlFileForPreviewingRadioButton.CheckedChanged += new System.EventHandler(AutoOpenMdlFileRadioButton_CheckedChanged);
			AutoOpenMdlFileForDecompilingRadioButton.CheckedChanged += new System.EventHandler(AutoOpenMdlFileRadioButton_CheckedChanged);
			AutoOpenMdlFileForViewingRadioButton.CheckedChanged += new System.EventHandler(AutoOpenMdlFileRadioButton_CheckedChanged);
			AutoOpenUseDefaultsButton.Click += new System.EventHandler(AutoOpenUseDefaultsButton_Click);
			AutoOpenFolderForUnpackRadioButton.CheckedChanged += new System.EventHandler(AutoOpenFolderRadioButton_CheckedChanged);
			AutoOpenFolderForDecompileRadioButton.CheckedChanged += new System.EventHandler(AutoOpenFolderRadioButton_CheckedChanged);
			AutoOpenFolderForCompileRadioButton.CheckedChanged += new System.EventHandler(AutoOpenFolderRadioButton_CheckedChanged);
			AutoOpenFolderForPackRadioButton.CheckedChanged += new System.EventHandler(AutoOpenFolderRadioButton_CheckedChanged);
			DragAndDropVpkFileForUnpackRadioButton.CheckedChanged += new System.EventHandler(DragAndDropVpkFileRadioButton_CheckedChanged);
			DragAndDropVpkFileForPublishRadioButton.CheckedChanged += new System.EventHandler(DragAndDropVpkFileRadioButton_CheckedChanged);
			DragAndDropGmaFileForUnpackRadioButton.CheckedChanged += new System.EventHandler(DragAndDropGmaFileRadioButton_CheckedChanged);
			DragAndDropGmaFileForPublishRadioButton.CheckedChanged += new System.EventHandler(DragAndDropGmaFileRadioButton_CheckedChanged);
			DragAndDropMdlFileForPreviewingRadioButton.CheckedChanged += new System.EventHandler(DragAndDropMdlFileRadioButton_CheckedChanged);
			DragAndDropMdlFileForDecompilingRadioButton.CheckedChanged += new System.EventHandler(DragAndDropMdlFileRadioButton_CheckedChanged);
			DragAndDropMdlFileForViewingRadioButton.CheckedChanged += new System.EventHandler(DragAndDropMdlFileRadioButton_CheckedChanged);
			DragAndDropFolderForUnpackRadioButton.CheckedChanged += new System.EventHandler(DragAndDropFolderRadioButton_CheckedChanged);
			DragAndDropFolderForDecompileRadioButton.CheckedChanged += new System.EventHandler(DragAndDropFolderRadioButton_CheckedChanged);
			DragAndDropFolderForCompileRadioButton.CheckedChanged += new System.EventHandler(DragAndDropFolderRadioButton_CheckedChanged);
			DragAndDropFolderForPackRadioButton.CheckedChanged += new System.EventHandler(DragAndDropFolderRadioButton_CheckedChanged);
			DragAndDropUseDefaultsButton.Click += new System.EventHandler(DragAndDropUseDefaultsButton_Click);
			ContextMenuUseDefaultsButton.Click += new System.EventHandler(ContextMenuUseDefaultsButton_Click);
			ApplyButton.Click += new System.EventHandler(ApplyButton_Click);
		}
		internal System.Windows.Forms.CheckBox IntegrateContextMenuItemsCheckBox;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.CheckBox IntegrateAsSubmenuCheckBox;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.CheckBox AutoOpenMdlFileCheckBox;
		internal System.Windows.Forms.CheckBox AutoOpenQcFileCheckBox;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.CheckBox AutoOpenVpkFileCheckBox;
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.RadioButton AutoOpenMdlFileForDecompilingRadioButton;
		internal System.Windows.Forms.RadioButton AutoOpenMdlFileForViewingRadioButton;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.Panel Panel4;
		internal System.Windows.Forms.RadioButton DragAndDropFolderForCompileRadioButton;
		internal System.Windows.Forms.RadioButton DragAndDropFolderForDecompileRadioButton;
		internal System.Windows.Forms.RadioButton DragAndDropFolderForUnpackRadioButton;
		internal System.Windows.Forms.Button AutoOpenUseDefaultsButton;
		internal System.Windows.Forms.Button DragAndDropUseDefaultsButton;
		internal System.Windows.Forms.Button ContextMenuUseDefaultsButton;
		internal System.Windows.Forms.Panel Panel7;
		internal System.Windows.Forms.CheckBox OptionsContextMenuCompileFolderAndSubfoldersCheckBox;
		internal System.Windows.Forms.CheckBox OptionsContextMenuCompileFolderCheckBox;
		internal System.Windows.Forms.CheckBox OptionsContextMenuCompileQcFileCheckBox;
		internal System.Windows.Forms.CheckBox OptionsContextMenuDecompileFolderAndSubfoldersCheckBox;
		internal System.Windows.Forms.CheckBox OptionsContextMenuDecompileFolderCheckBox;
		internal System.Windows.Forms.CheckBox OptionsContextMenuDecompileMdlFileCheckBox;
		internal System.Windows.Forms.CheckBox OptionsContextMenuViewMdlFileCheckBox;
		internal System.Windows.Forms.CheckBox OptionsContextMenuOpenWithCrowbarCheckBox;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.RadioButton AutoOpenMdlFileForPreviewingRadioButton;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.CheckBox AutoOpenMdlFileForDecompileCheckBox;
		internal System.Windows.Forms.CheckBox AutoOpenMdlFileForViewCheckBox;
		internal System.Windows.Forms.CheckBox AutoOpenMdlFileForPreviewCheckBox;
		internal System.Windows.Forms.Panel Panel3;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.CheckBox DragAndDropMdlFileForDecompileCheckBox;
		internal System.Windows.Forms.CheckBox DragAndDropMdlFileForViewCheckBox;
		internal System.Windows.Forms.CheckBox DragAndDropMdlFileForPreviewCheckBox;
		internal System.Windows.Forms.RadioButton DragAndDropMdlFileForPreviewingRadioButton;
		internal System.Windows.Forms.RadioButton DragAndDropMdlFileForDecompilingRadioButton;
		internal System.Windows.Forms.RadioButton DragAndDropMdlFileForViewingRadioButton;
		internal System.Windows.Forms.Label Label13;
		internal System.Windows.Forms.Label Label12;
		internal System.Windows.Forms.Label ApplyLabel;
		internal System.Windows.Forms.Button ApplyButton;
		internal System.Windows.Forms.Panel ApplyPanel;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Panel AutoOpenFolderPanel;
		internal System.Windows.Forms.RadioButton AutoOpenFolderForCompileRadioButton;
		internal System.Windows.Forms.RadioButton AutoOpenFolderForDecompileRadioButton;
		internal System.Windows.Forms.RadioButton AutoOpenFolderForUnpackRadioButton;
		internal System.Windows.Forms.CheckBox SingleInstanceCheckBox;
		internal System.Windows.Forms.Label Label14;
		internal System.Windows.Forms.CheckBox AutoOpenFpxFileCheckBox;
		internal System.Windows.Forms.CheckBox AutoOpenGmaFileCheckBox;
		internal RadioButton AutoOpenFolderForPackRadioButton;
		internal RadioButton DragAndDropFolderForPackRadioButton;
		internal Panel AutoOpenVpkPanel;
		internal RadioButton AutoOpenVpkFileForPublishRadioButton;
		internal RadioButton AutoOpenVpkFileForUnpackRadioButton;
		internal Panel Panel2;
		internal RadioButton AutoOpenGmaFileForPublishRadioButton;
		internal RadioButton AutoOpenGmaFileForUnpackRadioButton;
		internal Panel Panel5;
		internal RadioButton DragAndDropGmaFileForPublishRadioButton;
		internal RadioButton DragAndDropGmaFileForUnpackRadioButton;
		internal Panel Panel6;
		internal RadioButton DragAndDropVpkFileForPublishRadioButton;
		internal RadioButton DragAndDropVpkFileForUnpackRadioButton;
		internal Label Label7;
		internal Label Label6;
	}

}