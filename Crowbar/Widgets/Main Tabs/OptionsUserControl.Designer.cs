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
			this.IntegrateContextMenuItemsCheckBox = new System.Windows.Forms.CheckBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.IntegrateAsSubmenuCheckBox = new System.Windows.Forms.CheckBox();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.Panel7 = new System.Windows.Forms.Panel();
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsContextMenuCompileFolderCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsContextMenuCompileQcFileCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsContextMenuDecompileFolderCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsContextMenuDecompileMdlFileCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsContextMenuViewMdlFileCheckBox = new System.Windows.Forms.CheckBox();
			this.OptionsContextMenuOpenWithCrowbarCheckBox = new System.Windows.Forms.CheckBox();
			this.ContextMenuUseDefaultsButton = new System.Windows.Forms.Button();
			this.AutoOpenMdlFileCheckBox = new System.Windows.Forms.CheckBox();
			this.AutoOpenQcFileCheckBox = new System.Windows.Forms.CheckBox();
			this.GroupBox2 = new System.Windows.Forms.GroupBox();
			this.Panel2 = new System.Windows.Forms.Panel();
			this.AutoOpenGmaFileForPublishRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenGmaFileForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenVpkPanel = new System.Windows.Forms.Panel();
			this.AutoOpenVpkFileForPublishRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenVpkFileForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			this.Label14 = new System.Windows.Forms.Label();
			this.AutoOpenFpxFileCheckBox = new System.Windows.Forms.CheckBox();
			this.AutoOpenGmaFileCheckBox = new System.Windows.Forms.CheckBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.AutoOpenFolderPanel = new System.Windows.Forms.Panel();
			this.AutoOpenFolderForPackRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenFolderForCompileRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenFolderForDecompileRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenFolderForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.AutoOpenUseDefaultsButton = new System.Windows.Forms.Button();
			this.Panel1 = new System.Windows.Forms.Panel();
			this.Label9 = new System.Windows.Forms.Label();
			this.Label8 = new System.Windows.Forms.Label();
			this.AutoOpenMdlFileForDecompileCheckBox = new System.Windows.Forms.CheckBox();
			this.AutoOpenMdlFileForViewCheckBox = new System.Windows.Forms.CheckBox();
			this.AutoOpenMdlFileForPreviewCheckBox = new System.Windows.Forms.CheckBox();
			this.AutoOpenMdlFileForViewingRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenMdlFileForPreviewingRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenMdlFileForDecompilingRadioButton = new System.Windows.Forms.RadioButton();
			this.AutoOpenVpkFileCheckBox = new System.Windows.Forms.CheckBox();
			this.GroupBox3 = new System.Windows.Forms.GroupBox();
			this.Panel5 = new System.Windows.Forms.Panel();
			this.DragAndDropGmaFileForPublishRadioButton = new System.Windows.Forms.RadioButton();
			this.DragAndDropGmaFileForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			this.Panel6 = new System.Windows.Forms.Panel();
			this.DragAndDropVpkFileForPublishRadioButton = new System.Windows.Forms.RadioButton();
			this.DragAndDropVpkFileForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			this.Label7 = new System.Windows.Forms.Label();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label13 = new System.Windows.Forms.Label();
			this.Label12 = new System.Windows.Forms.Label();
			this.Panel3 = new System.Windows.Forms.Panel();
			this.Label10 = new System.Windows.Forms.Label();
			this.Label11 = new System.Windows.Forms.Label();
			this.DragAndDropMdlFileForDecompileCheckBox = new System.Windows.Forms.CheckBox();
			this.DragAndDropMdlFileForViewCheckBox = new System.Windows.Forms.CheckBox();
			this.DragAndDropMdlFileForPreviewCheckBox = new System.Windows.Forms.CheckBox();
			this.DragAndDropMdlFileForPreviewingRadioButton = new System.Windows.Forms.RadioButton();
			this.DragAndDropMdlFileForDecompilingRadioButton = new System.Windows.Forms.RadioButton();
			this.DragAndDropMdlFileForViewingRadioButton = new System.Windows.Forms.RadioButton();
			this.Label3 = new System.Windows.Forms.Label();
			this.DragAndDropUseDefaultsButton = new System.Windows.Forms.Button();
			this.Panel4 = new System.Windows.Forms.Panel();
			this.DragAndDropFolderForPackRadioButton = new System.Windows.Forms.RadioButton();
			this.DragAndDropFolderForCompileRadioButton = new System.Windows.Forms.RadioButton();
			this.DragAndDropFolderForDecompileRadioButton = new System.Windows.Forms.RadioButton();
			this.DragAndDropFolderForUnpackRadioButton = new System.Windows.Forms.RadioButton();
			this.ApplyLabel = new System.Windows.Forms.Label();
			this.ApplyButton = new System.Windows.Forms.Button();
			this.ApplyPanel = new System.Windows.Forms.Panel();
			this.SingleInstanceCheckBox = new System.Windows.Forms.CheckBox();
			this.GroupBox1.SuspendLayout();
			this.Panel7.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			this.Panel2.SuspendLayout();
			this.AutoOpenVpkPanel.SuspendLayout();
			this.AutoOpenFolderPanel.SuspendLayout();
			this.Panel1.SuspendLayout();
			this.GroupBox3.SuspendLayout();
			this.Panel5.SuspendLayout();
			this.Panel6.SuspendLayout();
			this.Panel3.SuspendLayout();
			this.Panel4.SuspendLayout();
			this.ApplyPanel.SuspendLayout();
			this.SuspendLayout();
			//
			//IntegrateContextMenuItemsCheckBox
			//
			this.IntegrateContextMenuItemsCheckBox.AutoSize = true;
			this.IntegrateContextMenuItemsCheckBox.Location = new System.Drawing.Point(6, 19);
			this.IntegrateContextMenuItemsCheckBox.Name = "IntegrateContextMenuItemsCheckBox";
			this.IntegrateContextMenuItemsCheckBox.Size = new System.Drawing.Size(223, 17);
			this.IntegrateContextMenuItemsCheckBox.TabIndex = 0;
			this.IntegrateContextMenuItemsCheckBox.Text = "Integrate Crowbar context menu items";
			this.IntegrateContextMenuItemsCheckBox.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(24, 68);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(112, 13);
			this.Label1.TabIndex = 2;
			this.Label1.Text = "Context menu items:";
			//
			//IntegrateAsSubmenuCheckBox
			//
			this.IntegrateAsSubmenuCheckBox.AutoSize = true;
			this.IntegrateAsSubmenuCheckBox.Location = new System.Drawing.Point(24, 42);
			this.IntegrateAsSubmenuCheckBox.Name = "IntegrateAsSubmenuCheckBox";
			this.IntegrateAsSubmenuCheckBox.Size = new System.Drawing.Size(202, 17);
			this.IntegrateAsSubmenuCheckBox.TabIndex = 4;
			this.IntegrateAsSubmenuCheckBox.Text = "Integrate as a \"Crowbar\" submenu";
			this.IntegrateAsSubmenuCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.Panel7);
			this.GroupBox1.Controls.Add(this.ContextMenuUseDefaultsButton);
			this.GroupBox1.Controls.Add(this.IntegrateContextMenuItemsCheckBox);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Controls.Add(this.IntegrateAsSubmenuCheckBox);
			this.GroupBox1.Location = new System.Drawing.Point(415, 26);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(309, 467);
			this.GroupBox1.TabIndex = 2;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Windows Explorer Context Menu";
			this.GroupBox1.Visible = false;
			//
			//Panel7
			//
			this.Panel7.BackColor = System.Drawing.SystemColors.Control;
			this.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel7.Controls.Add(this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox);
			this.Panel7.Controls.Add(this.OptionsContextMenuCompileFolderCheckBox);
			this.Panel7.Controls.Add(this.OptionsContextMenuCompileQcFileCheckBox);
			this.Panel7.Controls.Add(this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox);
			this.Panel7.Controls.Add(this.OptionsContextMenuDecompileFolderCheckBox);
			this.Panel7.Controls.Add(this.OptionsContextMenuDecompileMdlFileCheckBox);
			this.Panel7.Controls.Add(this.OptionsContextMenuViewMdlFileCheckBox);
			this.Panel7.Controls.Add(this.OptionsContextMenuOpenWithCrowbarCheckBox);
			this.Panel7.Location = new System.Drawing.Point(27, 84);
			this.Panel7.Name = "Panel7";
			this.Panel7.Size = new System.Drawing.Size(270, 124);
			this.Panel7.TabIndex = 20;
			//
			//OptionsContextMenuCompileFolderAndSubfoldersCheckBox
			//
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.AutoSize = true;
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Location = new System.Drawing.Point(3, 106);
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Name = "OptionsContextMenuCompileFolderAndSubfoldersCheckBox";
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Size = new System.Drawing.Size(247, 17);
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.TabIndex = 7;
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Text = "Compile folder and subfolders to <folder>";
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuCompileFolderCheckBox
			//
			this.OptionsContextMenuCompileFolderCheckBox.AutoSize = true;
			this.OptionsContextMenuCompileFolderCheckBox.Location = new System.Drawing.Point(3, 91);
			this.OptionsContextMenuCompileFolderCheckBox.Name = "OptionsContextMenuCompileFolderCheckBox";
			this.OptionsContextMenuCompileFolderCheckBox.Size = new System.Drawing.Size(166, 17);
			this.OptionsContextMenuCompileFolderCheckBox.TabIndex = 6;
			this.OptionsContextMenuCompileFolderCheckBox.Text = "Compile folder to <folder>";
			this.OptionsContextMenuCompileFolderCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuCompileQcFileCheckBox
			//
			this.OptionsContextMenuCompileQcFileCheckBox.AutoSize = true;
			this.OptionsContextMenuCompileQcFileCheckBox.Location = new System.Drawing.Point(3, 76);
			this.OptionsContextMenuCompileQcFileCheckBox.Name = "OptionsContextMenuCompileQcFileCheckBox";
			this.OptionsContextMenuCompileQcFileCheckBox.Size = new System.Drawing.Size(105, 17);
			this.OptionsContextMenuCompileQcFileCheckBox.TabIndex = 5;
			this.OptionsContextMenuCompileQcFileCheckBox.Text = "Compile QC file";
			this.OptionsContextMenuCompileQcFileCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuDecompileFolderAndSubfoldersCheckBox
			//
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.AutoSize = true;
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Location = new System.Drawing.Point(3, 61);
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Name = "OptionsContextMenuDecompileFolderAndSubfoldersCheckBox";
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Size = new System.Drawing.Size(259, 17);
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.TabIndex = 4;
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Text = "Decompile folder and subfolders to <folder>";
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuDecompileFolderCheckBox
			//
			this.OptionsContextMenuDecompileFolderCheckBox.AutoSize = true;
			this.OptionsContextMenuDecompileFolderCheckBox.Location = new System.Drawing.Point(3, 46);
			this.OptionsContextMenuDecompileFolderCheckBox.Name = "OptionsContextMenuDecompileFolderCheckBox";
			this.OptionsContextMenuDecompileFolderCheckBox.Size = new System.Drawing.Size(178, 17);
			this.OptionsContextMenuDecompileFolderCheckBox.TabIndex = 3;
			this.OptionsContextMenuDecompileFolderCheckBox.Text = "Decompile folder to <folder>";
			this.OptionsContextMenuDecompileFolderCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuDecompileMdlFileCheckBox
			//
			this.OptionsContextMenuDecompileMdlFileCheckBox.AutoSize = true;
			this.OptionsContextMenuDecompileMdlFileCheckBox.Location = new System.Drawing.Point(3, 31);
			this.OptionsContextMenuDecompileMdlFileCheckBox.Name = "OptionsContextMenuDecompileMdlFileCheckBox";
			this.OptionsContextMenuDecompileMdlFileCheckBox.Size = new System.Drawing.Size(189, 17);
			this.OptionsContextMenuDecompileMdlFileCheckBox.TabIndex = 2;
			this.OptionsContextMenuDecompileMdlFileCheckBox.Text = "Decompile MDL file to <folder>";
			this.OptionsContextMenuDecompileMdlFileCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuViewMdlFileCheckBox
			//
			this.OptionsContextMenuViewMdlFileCheckBox.AutoSize = true;
			this.OptionsContextMenuViewMdlFileCheckBox.Location = new System.Drawing.Point(3, 16);
			this.OptionsContextMenuViewMdlFileCheckBox.Name = "OptionsContextMenuViewMdlFileCheckBox";
			this.OptionsContextMenuViewMdlFileCheckBox.Size = new System.Drawing.Size(96, 17);
			this.OptionsContextMenuViewMdlFileCheckBox.TabIndex = 1;
			this.OptionsContextMenuViewMdlFileCheckBox.Text = "View MDL file";
			this.OptionsContextMenuViewMdlFileCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsContextMenuOpenWithCrowbarCheckBox
			//
			this.OptionsContextMenuOpenWithCrowbarCheckBox.AutoSize = true;
			this.OptionsContextMenuOpenWithCrowbarCheckBox.Location = new System.Drawing.Point(3, 1);
			this.OptionsContextMenuOpenWithCrowbarCheckBox.Name = "OptionsContextMenuOpenWithCrowbarCheckBox";
			this.OptionsContextMenuOpenWithCrowbarCheckBox.Size = new System.Drawing.Size(128, 17);
			this.OptionsContextMenuOpenWithCrowbarCheckBox.TabIndex = 0;
			this.OptionsContextMenuOpenWithCrowbarCheckBox.Text = "Open with Crowbar";
			this.OptionsContextMenuOpenWithCrowbarCheckBox.UseVisualStyleBackColor = true;
			//
			//ContextMenuUseDefaultsButton
			//
			this.ContextMenuUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.ContextMenuUseDefaultsButton.Location = new System.Drawing.Point(104, 438);
			this.ContextMenuUseDefaultsButton.Name = "ContextMenuUseDefaultsButton";
			this.ContextMenuUseDefaultsButton.Size = new System.Drawing.Size(100, 23);
			this.ContextMenuUseDefaultsButton.TabIndex = 19;
			this.ContextMenuUseDefaultsButton.Text = "Use Defaults";
			this.ContextMenuUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileCheckBox
			//
			this.AutoOpenMdlFileCheckBox.AutoSize = true;
			this.AutoOpenMdlFileCheckBox.Location = new System.Drawing.Point(6, 214);
			this.AutoOpenMdlFileCheckBox.Name = "AutoOpenMdlFileCheckBox";
			this.AutoOpenMdlFileCheckBox.Size = new System.Drawing.Size(71, 17);
			this.AutoOpenMdlFileCheckBox.TabIndex = 2;
			this.AutoOpenMdlFileCheckBox.Text = "MDL file:";
			this.AutoOpenMdlFileCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenQcFileCheckBox
			//
			this.AutoOpenQcFileCheckBox.AutoSize = true;
			this.AutoOpenQcFileCheckBox.Location = new System.Drawing.Point(6, 307);
			this.AutoOpenQcFileCheckBox.Name = "AutoOpenQcFileCheckBox";
			this.AutoOpenQcFileCheckBox.Size = new System.Drawing.Size(63, 17);
			this.AutoOpenQcFileCheckBox.TabIndex = 4;
			this.AutoOpenQcFileCheckBox.Text = "QC file:";
			this.AutoOpenQcFileCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupBox2
			//
			this.GroupBox2.Controls.Add(this.Panel2);
			this.GroupBox2.Controls.Add(this.AutoOpenVpkPanel);
			this.GroupBox2.Controls.Add(this.Label14);
			this.GroupBox2.Controls.Add(this.AutoOpenFpxFileCheckBox);
			this.GroupBox2.Controls.Add(this.AutoOpenGmaFileCheckBox);
			this.GroupBox2.Controls.Add(this.Label5);
			this.GroupBox2.Controls.Add(this.AutoOpenFolderPanel);
			this.GroupBox2.Controls.Add(this.Label4);
			this.GroupBox2.Controls.Add(this.Label2);
			this.GroupBox2.Controls.Add(this.AutoOpenUseDefaultsButton);
			this.GroupBox2.Controls.Add(this.Panel1);
			this.GroupBox2.Controls.Add(this.AutoOpenMdlFileCheckBox);
			this.GroupBox2.Controls.Add(this.AutoOpenQcFileCheckBox);
			this.GroupBox2.Controls.Add(this.AutoOpenVpkFileCheckBox);
			this.GroupBox2.Location = new System.Drawing.Point(3, 26);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new System.Drawing.Size(200, 467);
			this.GroupBox2.TabIndex = 0;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Windows Explorer Auto-Open";
			//
			//Panel2
			//
			this.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel2.Controls.Add(this.AutoOpenGmaFileForPublishRadioButton);
			this.Panel2.Controls.Add(this.AutoOpenGmaFileForUnpackRadioButton);
			this.Panel2.Location = new System.Drawing.Point(81, 149);
			this.Panel2.Name = "Panel2";
			this.Panel2.Size = new System.Drawing.Size(89, 36);
			this.Panel2.TabIndex = 17;
			//
			//AutoOpenGmaFileForPublishRadioButton
			//
			this.AutoOpenGmaFileForPublishRadioButton.AutoSize = true;
			this.AutoOpenGmaFileForPublishRadioButton.Location = new System.Drawing.Point(3, 15);
			this.AutoOpenGmaFileForPublishRadioButton.Name = "AutoOpenGmaFileForPublishRadioButton";
			this.AutoOpenGmaFileForPublishRadioButton.Size = new System.Drawing.Size(63, 17);
			this.AutoOpenGmaFileForPublishRadioButton.TabIndex = 13;
			this.AutoOpenGmaFileForPublishRadioButton.TabStop = true;
			this.AutoOpenGmaFileForPublishRadioButton.Text = "Publish";
			this.AutoOpenGmaFileForPublishRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenGmaFileForUnpackRadioButton
			//
			this.AutoOpenGmaFileForUnpackRadioButton.AutoSize = true;
			this.AutoOpenGmaFileForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			this.AutoOpenGmaFileForUnpackRadioButton.Name = "AutoOpenGmaFileForUnpackRadioButton";
			this.AutoOpenGmaFileForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			this.AutoOpenGmaFileForUnpackRadioButton.TabIndex = 12;
			this.AutoOpenGmaFileForUnpackRadioButton.TabStop = true;
			this.AutoOpenGmaFileForUnpackRadioButton.Text = "Unpack";
			this.AutoOpenGmaFileForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenVpkPanel
			//
			this.AutoOpenVpkPanel.BackColor = System.Drawing.SystemColors.Control;
			this.AutoOpenVpkPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AutoOpenVpkPanel.Controls.Add(this.AutoOpenVpkFileForPublishRadioButton);
			this.AutoOpenVpkPanel.Controls.Add(this.AutoOpenVpkFileForUnpackRadioButton);
			this.AutoOpenVpkPanel.Location = new System.Drawing.Point(81, 107);
			this.AutoOpenVpkPanel.Name = "AutoOpenVpkPanel";
			this.AutoOpenVpkPanel.Size = new System.Drawing.Size(89, 36);
			this.AutoOpenVpkPanel.TabIndex = 16;
			//
			//AutoOpenVpkFileForPublishRadioButton
			//
			this.AutoOpenVpkFileForPublishRadioButton.AutoSize = true;
			this.AutoOpenVpkFileForPublishRadioButton.Location = new System.Drawing.Point(3, 15);
			this.AutoOpenVpkFileForPublishRadioButton.Name = "AutoOpenVpkFileForPublishRadioButton";
			this.AutoOpenVpkFileForPublishRadioButton.Size = new System.Drawing.Size(63, 17);
			this.AutoOpenVpkFileForPublishRadioButton.TabIndex = 13;
			this.AutoOpenVpkFileForPublishRadioButton.TabStop = true;
			this.AutoOpenVpkFileForPublishRadioButton.Text = "Publish";
			this.AutoOpenVpkFileForPublishRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenVpkFileForUnpackRadioButton
			//
			this.AutoOpenVpkFileForUnpackRadioButton.AutoSize = true;
			this.AutoOpenVpkFileForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			this.AutoOpenVpkFileForUnpackRadioButton.Name = "AutoOpenVpkFileForUnpackRadioButton";
			this.AutoOpenVpkFileForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			this.AutoOpenVpkFileForUnpackRadioButton.TabIndex = 12;
			this.AutoOpenVpkFileForUnpackRadioButton.TabStop = true;
			this.AutoOpenVpkFileForUnpackRadioButton.Text = "Unpack";
			this.AutoOpenVpkFileForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//Label14
			//
			this.Label14.BackColor = System.Drawing.SystemColors.Control;
			this.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label14.Location = new System.Drawing.Point(81, 192);
			this.Label14.Margin = new System.Windows.Forms.Padding(3);
			this.Label14.Name = "Label14";
			this.Label14.Size = new System.Drawing.Size(89, 17);
			this.Label14.TabIndex = 18;
			this.Label14.Text = "Unpack";
			//
			//AutoOpenFpxFileCheckBox
			//
			this.AutoOpenFpxFileCheckBox.AutoSize = true;
			this.AutoOpenFpxFileCheckBox.Location = new System.Drawing.Point(6, 191);
			this.AutoOpenFpxFileCheckBox.Name = "AutoOpenFpxFileCheckBox";
			this.AutoOpenFpxFileCheckBox.Size = new System.Drawing.Size(66, 17);
			this.AutoOpenFpxFileCheckBox.TabIndex = 17;
			this.AutoOpenFpxFileCheckBox.Text = "FPX file:";
			this.AutoOpenFpxFileCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenGmaFileCheckBox
			//
			this.AutoOpenGmaFileCheckBox.AutoSize = true;
			this.AutoOpenGmaFileCheckBox.Location = new System.Drawing.Point(6, 151);
			this.AutoOpenGmaFileCheckBox.Name = "AutoOpenGmaFileCheckBox";
			this.AutoOpenGmaFileCheckBox.Size = new System.Drawing.Size(73, 17);
			this.AutoOpenGmaFileCheckBox.TabIndex = 15;
			this.AutoOpenGmaFileCheckBox.Text = "GMA file:";
			this.AutoOpenGmaFileCheckBox.UseVisualStyleBackColor = true;
			//
			//Label5
			//
			this.Label5.AutoSize = true;
			this.Label5.Location = new System.Drawing.Point(6, 330);
			this.Label5.Margin = new System.Windows.Forms.Padding(3);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(43, 13);
			this.Label5.TabIndex = 6;
			this.Label5.Text = "Folder:";
			//
			//AutoOpenFolderPanel
			//
			this.AutoOpenFolderPanel.BackColor = System.Drawing.SystemColors.Control;
			this.AutoOpenFolderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AutoOpenFolderPanel.Controls.Add(this.AutoOpenFolderForPackRadioButton);
			this.AutoOpenFolderPanel.Controls.Add(this.AutoOpenFolderForCompileRadioButton);
			this.AutoOpenFolderPanel.Controls.Add(this.AutoOpenFolderForDecompileRadioButton);
			this.AutoOpenFolderPanel.Controls.Add(this.AutoOpenFolderForUnpackRadioButton);
			this.AutoOpenFolderPanel.Location = new System.Drawing.Point(81, 330);
			this.AutoOpenFolderPanel.Name = "AutoOpenFolderPanel";
			this.AutoOpenFolderPanel.Size = new System.Drawing.Size(89, 66);
			this.AutoOpenFolderPanel.TabIndex = 7;
			//
			//AutoOpenFolderForPackRadioButton
			//
			this.AutoOpenFolderForPackRadioButton.AutoSize = true;
			this.AutoOpenFolderForPackRadioButton.Location = new System.Drawing.Point(3, 45);
			this.AutoOpenFolderForPackRadioButton.Name = "AutoOpenFolderForPackRadioButton";
			this.AutoOpenFolderForPackRadioButton.Size = new System.Drawing.Size(48, 17);
			this.AutoOpenFolderForPackRadioButton.TabIndex = 15;
			this.AutoOpenFolderForPackRadioButton.TabStop = true;
			this.AutoOpenFolderForPackRadioButton.Text = "Pack";
			this.AutoOpenFolderForPackRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenFolderForCompileRadioButton
			//
			this.AutoOpenFolderForCompileRadioButton.AutoSize = true;
			this.AutoOpenFolderForCompileRadioButton.Location = new System.Drawing.Point(3, 30);
			this.AutoOpenFolderForCompileRadioButton.Name = "AutoOpenFolderForCompileRadioButton";
			this.AutoOpenFolderForCompileRadioButton.Size = new System.Drawing.Size(67, 17);
			this.AutoOpenFolderForCompileRadioButton.TabIndex = 14;
			this.AutoOpenFolderForCompileRadioButton.TabStop = true;
			this.AutoOpenFolderForCompileRadioButton.Text = "Compile";
			this.AutoOpenFolderForCompileRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenFolderForDecompileRadioButton
			//
			this.AutoOpenFolderForDecompileRadioButton.AutoSize = true;
			this.AutoOpenFolderForDecompileRadioButton.Location = new System.Drawing.Point(3, 15);
			this.AutoOpenFolderForDecompileRadioButton.Name = "AutoOpenFolderForDecompileRadioButton";
			this.AutoOpenFolderForDecompileRadioButton.Size = new System.Drawing.Size(79, 17);
			this.AutoOpenFolderForDecompileRadioButton.TabIndex = 13;
			this.AutoOpenFolderForDecompileRadioButton.TabStop = true;
			this.AutoOpenFolderForDecompileRadioButton.Text = "Decompile";
			this.AutoOpenFolderForDecompileRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenFolderForUnpackRadioButton
			//
			this.AutoOpenFolderForUnpackRadioButton.AutoSize = true;
			this.AutoOpenFolderForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			this.AutoOpenFolderForUnpackRadioButton.Name = "AutoOpenFolderForUnpackRadioButton";
			this.AutoOpenFolderForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			this.AutoOpenFolderForUnpackRadioButton.TabIndex = 12;
			this.AutoOpenFolderForUnpackRadioButton.TabStop = true;
			this.AutoOpenFolderForUnpackRadioButton.Text = "Unpack";
			this.AutoOpenFolderForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//Label4
			//
			this.Label4.BackColor = System.Drawing.SystemColors.Control;
			this.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label4.Location = new System.Drawing.Point(81, 307);
			this.Label4.Margin = new System.Windows.Forms.Padding(3);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(89, 17);
			this.Label4.TabIndex = 5;
			this.Label4.Text = "Compile";
			//
			//Label2
			//
			this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Label2.Location = new System.Drawing.Point(6, 20);
			this.Label2.Margin = new System.Windows.Forms.Padding(3);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(188, 83);
			this.Label2.TabIndex = 14;
			this.Label2.Text = "Change the default program to Crowbar for the following file extensions and which" + " tab to set up. This includes when files or folders are dragged onto the \"Crowba" + "r.exe\" file.";
			//
			//AutoOpenUseDefaultsButton
			//
			this.AutoOpenUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.AutoOpenUseDefaultsButton.Location = new System.Drawing.Point(44, 438);
			this.AutoOpenUseDefaultsButton.Name = "AutoOpenUseDefaultsButton";
			this.AutoOpenUseDefaultsButton.Size = new System.Drawing.Size(100, 23);
			this.AutoOpenUseDefaultsButton.TabIndex = 8;
			this.AutoOpenUseDefaultsButton.Text = "Use Defaults";
			this.AutoOpenUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//Panel1
			//
			this.Panel1.BackColor = System.Drawing.SystemColors.Control;
			this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel1.Controls.Add(this.Label9);
			this.Panel1.Controls.Add(this.Label8);
			this.Panel1.Controls.Add(this.AutoOpenMdlFileForViewCheckBox);
			this.Panel1.Controls.Add(this.AutoOpenMdlFileForDecompileCheckBox);
			this.Panel1.Controls.Add(this.AutoOpenMdlFileForPreviewCheckBox);
			this.Panel1.Controls.Add(this.AutoOpenMdlFileForViewingRadioButton);
			this.Panel1.Controls.Add(this.AutoOpenMdlFileForPreviewingRadioButton);
			this.Panel1.Controls.Add(this.AutoOpenMdlFileForDecompilingRadioButton);
			this.Panel1.Location = new System.Drawing.Point(25, 231);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(145, 70);
			this.Panel1.TabIndex = 3;
			//
			//Label9
			//
			this.Label9.AutoSize = true;
			this.Label9.Location = new System.Drawing.Point(2, 2);
			this.Label9.Name = "Label9";
			this.Label9.Size = new System.Drawing.Size(44, 13);
			this.Label9.TabIndex = 20;
			this.Label9.Text = "Set Up:";
			//
			//Label8
			//
			this.Label8.AutoSize = true;
			this.Label8.Location = new System.Drawing.Point(94, 2);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(39, 13);
			this.Label8.TabIndex = 19;
			this.Label8.Text = "Open:";
			//
			//AutoOpenMdlFileForDecompileCheckBox
			//
			this.AutoOpenMdlFileForDecompileCheckBox.AutoSize = true;
			this.AutoOpenMdlFileForDecompileCheckBox.Location = new System.Drawing.Point(3, 33);
			this.AutoOpenMdlFileForDecompileCheckBox.Name = "AutoOpenMdlFileForDecompileCheckBox";
			this.AutoOpenMdlFileForDecompileCheckBox.Size = new System.Drawing.Size(80, 17);
			this.AutoOpenMdlFileForDecompileCheckBox.TabIndex = 1;
			this.AutoOpenMdlFileForDecompileCheckBox.Text = "Decompile";
			this.AutoOpenMdlFileForDecompileCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForViewCheckBox
			//
			this.AutoOpenMdlFileForViewCheckBox.AutoSize = true;
			this.AutoOpenMdlFileForViewCheckBox.Location = new System.Drawing.Point(3, 48);
			this.AutoOpenMdlFileForViewCheckBox.Name = "AutoOpenMdlFileForViewCheckBox";
			this.AutoOpenMdlFileForViewCheckBox.Size = new System.Drawing.Size(51, 17);
			this.AutoOpenMdlFileForViewCheckBox.TabIndex = 2;
			this.AutoOpenMdlFileForViewCheckBox.Text = "View";
			this.AutoOpenMdlFileForViewCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForPreviewCheckBox
			//
			this.AutoOpenMdlFileForPreviewCheckBox.AutoSize = true;
			this.AutoOpenMdlFileForPreviewCheckBox.Location = new System.Drawing.Point(3, 18);
			this.AutoOpenMdlFileForPreviewCheckBox.Name = "AutoOpenMdlFileForPreviewCheckBox";
			this.AutoOpenMdlFileForPreviewCheckBox.Size = new System.Drawing.Size(65, 17);
			this.AutoOpenMdlFileForPreviewCheckBox.TabIndex = 0;
			this.AutoOpenMdlFileForPreviewCheckBox.Text = "Preview";
			this.AutoOpenMdlFileForPreviewCheckBox.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForViewingRadioButton
			//
			this.AutoOpenMdlFileForViewingRadioButton.Location = new System.Drawing.Point(104, 49);
			this.AutoOpenMdlFileForViewingRadioButton.Name = "AutoOpenMdlFileForViewingRadioButton";
			this.AutoOpenMdlFileForViewingRadioButton.Size = new System.Drawing.Size(14, 13);
			this.AutoOpenMdlFileForViewingRadioButton.TabIndex = 5;
			this.AutoOpenMdlFileForViewingRadioButton.TabStop = true;
			this.AutoOpenMdlFileForViewingRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForPreviewingRadioButton
			//
			this.AutoOpenMdlFileForPreviewingRadioButton.Location = new System.Drawing.Point(104, 19);
			this.AutoOpenMdlFileForPreviewingRadioButton.Name = "AutoOpenMdlFileForPreviewingRadioButton";
			this.AutoOpenMdlFileForPreviewingRadioButton.Size = new System.Drawing.Size(14, 13);
			this.AutoOpenMdlFileForPreviewingRadioButton.TabIndex = 3;
			this.AutoOpenMdlFileForPreviewingRadioButton.TabStop = true;
			this.AutoOpenMdlFileForPreviewingRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenMdlFileForDecompilingRadioButton
			//
			this.AutoOpenMdlFileForDecompilingRadioButton.Location = new System.Drawing.Point(104, 34);
			this.AutoOpenMdlFileForDecompilingRadioButton.Name = "AutoOpenMdlFileForDecompilingRadioButton";
			this.AutoOpenMdlFileForDecompilingRadioButton.Size = new System.Drawing.Size(14, 13);
			this.AutoOpenMdlFileForDecompilingRadioButton.TabIndex = 4;
			this.AutoOpenMdlFileForDecompilingRadioButton.TabStop = true;
			this.AutoOpenMdlFileForDecompilingRadioButton.UseVisualStyleBackColor = true;
			//
			//AutoOpenVpkFileCheckBox
			//
			this.AutoOpenVpkFileCheckBox.AutoSize = true;
			this.AutoOpenVpkFileCheckBox.Location = new System.Drawing.Point(6, 108);
			this.AutoOpenVpkFileCheckBox.Name = "AutoOpenVpkFileCheckBox";
			this.AutoOpenVpkFileCheckBox.Size = new System.Drawing.Size(67, 17);
			this.AutoOpenVpkFileCheckBox.TabIndex = 0;
			this.AutoOpenVpkFileCheckBox.Text = "VPK file:";
			this.AutoOpenVpkFileCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupBox3
			//
			this.GroupBox3.Controls.Add(this.Panel5);
			this.GroupBox3.Controls.Add(this.Panel6);
			this.GroupBox3.Controls.Add(this.Label7);
			this.GroupBox3.Controls.Add(this.Label6);
			this.GroupBox3.Controls.Add(this.Label13);
			this.GroupBox3.Controls.Add(this.Label12);
			this.GroupBox3.Controls.Add(this.Panel3);
			this.GroupBox3.Controls.Add(this.Label3);
			this.GroupBox3.Controls.Add(this.DragAndDropUseDefaultsButton);
			this.GroupBox3.Controls.Add(this.Panel4);
			this.GroupBox3.Location = new System.Drawing.Point(209, 26);
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.Size = new System.Drawing.Size(200, 467);
			this.GroupBox3.TabIndex = 1;
			this.GroupBox3.TabStop = false;
			this.GroupBox3.Text = "Windows Explorer Drag-and-Drop";
			//
			//Panel5
			//
			this.Panel5.BackColor = System.Drawing.SystemColors.Control;
			this.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel5.Controls.Add(this.DragAndDropGmaFileForPublishRadioButton);
			this.Panel5.Controls.Add(this.DragAndDropGmaFileForUnpackRadioButton);
			this.Panel5.Location = new System.Drawing.Point(81, 149);
			this.Panel5.Name = "Panel5";
			this.Panel5.Size = new System.Drawing.Size(89, 36);
			this.Panel5.TabIndex = 23;
			//
			//DragAndDropGmaFileForPublishRadioButton
			//
			this.DragAndDropGmaFileForPublishRadioButton.AutoSize = true;
			this.DragAndDropGmaFileForPublishRadioButton.Location = new System.Drawing.Point(3, 15);
			this.DragAndDropGmaFileForPublishRadioButton.Name = "DragAndDropGmaFileForPublishRadioButton";
			this.DragAndDropGmaFileForPublishRadioButton.Size = new System.Drawing.Size(63, 17);
			this.DragAndDropGmaFileForPublishRadioButton.TabIndex = 13;
			this.DragAndDropGmaFileForPublishRadioButton.TabStop = true;
			this.DragAndDropGmaFileForPublishRadioButton.Text = "Publish";
			this.DragAndDropGmaFileForPublishRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropGmaFileForUnpackRadioButton
			//
			this.DragAndDropGmaFileForUnpackRadioButton.AutoSize = true;
			this.DragAndDropGmaFileForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			this.DragAndDropGmaFileForUnpackRadioButton.Name = "DragAndDropGmaFileForUnpackRadioButton";
			this.DragAndDropGmaFileForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			this.DragAndDropGmaFileForUnpackRadioButton.TabIndex = 12;
			this.DragAndDropGmaFileForUnpackRadioButton.TabStop = true;
			this.DragAndDropGmaFileForUnpackRadioButton.Text = "Unpack";
			this.DragAndDropGmaFileForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//Panel6
			//
			this.Panel6.BackColor = System.Drawing.SystemColors.Control;
			this.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel6.Controls.Add(this.DragAndDropVpkFileForPublishRadioButton);
			this.Panel6.Controls.Add(this.DragAndDropVpkFileForUnpackRadioButton);
			this.Panel6.Location = new System.Drawing.Point(81, 107);
			this.Panel6.Name = "Panel6";
			this.Panel6.Size = new System.Drawing.Size(89, 36);
			this.Panel6.TabIndex = 22;
			//
			//DragAndDropVpkFileForPublishRadioButton
			//
			this.DragAndDropVpkFileForPublishRadioButton.AutoSize = true;
			this.DragAndDropVpkFileForPublishRadioButton.Location = new System.Drawing.Point(3, 15);
			this.DragAndDropVpkFileForPublishRadioButton.Name = "DragAndDropVpkFileForPublishRadioButton";
			this.DragAndDropVpkFileForPublishRadioButton.Size = new System.Drawing.Size(63, 17);
			this.DragAndDropVpkFileForPublishRadioButton.TabIndex = 13;
			this.DragAndDropVpkFileForPublishRadioButton.TabStop = true;
			this.DragAndDropVpkFileForPublishRadioButton.Text = "Publish";
			this.DragAndDropVpkFileForPublishRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropVpkFileForUnpackRadioButton
			//
			this.DragAndDropVpkFileForUnpackRadioButton.AutoSize = true;
			this.DragAndDropVpkFileForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			this.DragAndDropVpkFileForUnpackRadioButton.Name = "DragAndDropVpkFileForUnpackRadioButton";
			this.DragAndDropVpkFileForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			this.DragAndDropVpkFileForUnpackRadioButton.TabIndex = 12;
			this.DragAndDropVpkFileForUnpackRadioButton.TabStop = true;
			this.DragAndDropVpkFileForUnpackRadioButton.Text = "Unpack";
			this.DragAndDropVpkFileForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//Label7
			//
			this.Label7.AutoSize = true;
			this.Label7.Location = new System.Drawing.Point(6, 152);
			this.Label7.Margin = new System.Windows.Forms.Padding(3);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(54, 13);
			this.Label7.TabIndex = 21;
			this.Label7.Text = "GMA file:";
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(6, 109);
			this.Label6.Margin = new System.Windows.Forms.Padding(3);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(48, 13);
			this.Label6.TabIndex = 20;
			this.Label6.Text = "VPK file:";
			//
			//Label13
			//
			this.Label13.AutoSize = true;
			this.Label13.Location = new System.Drawing.Point(6, 287);
			this.Label13.Margin = new System.Windows.Forms.Padding(3);
			this.Label13.Name = "Label13";
			this.Label13.Size = new System.Drawing.Size(43, 13);
			this.Label13.TabIndex = 2;
			this.Label13.Text = "Folder:";
			//
			//Label12
			//
			this.Label12.AutoSize = true;
			this.Label12.Location = new System.Drawing.Point(6, 192);
			this.Label12.Margin = new System.Windows.Forms.Padding(3);
			this.Label12.Name = "Label12";
			this.Label12.Size = new System.Drawing.Size(52, 13);
			this.Label12.TabIndex = 0;
			this.Label12.Text = "MDL file:";
			//
			//Panel3
			//
			this.Panel3.BackColor = System.Drawing.SystemColors.Control;
			this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel3.Controls.Add(this.Label10);
			this.Panel3.Controls.Add(this.Label11);
			this.Panel3.Controls.Add(this.DragAndDropMdlFileForViewCheckBox);
			this.Panel3.Controls.Add(this.DragAndDropMdlFileForDecompileCheckBox);
			this.Panel3.Controls.Add(this.DragAndDropMdlFileForPreviewCheckBox);
			this.Panel3.Controls.Add(this.DragAndDropMdlFileForPreviewingRadioButton);
			this.Panel3.Controls.Add(this.DragAndDropMdlFileForDecompilingRadioButton);
			this.Panel3.Controls.Add(this.DragAndDropMdlFileForViewingRadioButton);
			this.Panel3.Location = new System.Drawing.Point(25, 207);
			this.Panel3.Name = "Panel3";
			this.Panel3.Size = new System.Drawing.Size(145, 70);
			this.Panel3.TabIndex = 1;
			//
			//Label10
			//
			this.Label10.AutoSize = true;
			this.Label10.Location = new System.Drawing.Point(2, 2);
			this.Label10.Name = "Label10";
			this.Label10.Size = new System.Drawing.Size(44, 13);
			this.Label10.TabIndex = 20;
			this.Label10.Text = "Set Up:";
			//
			//Label11
			//
			this.Label11.AutoSize = true;
			this.Label11.Location = new System.Drawing.Point(94, 2);
			this.Label11.Name = "Label11";
			this.Label11.Size = new System.Drawing.Size(39, 13);
			this.Label11.TabIndex = 19;
			this.Label11.Text = "Open:";
			//
			//DragAndDropMdlFileForDecompileCheckBox
			//
			this.DragAndDropMdlFileForDecompileCheckBox.AutoSize = true;
			this.DragAndDropMdlFileForDecompileCheckBox.Location = new System.Drawing.Point(3, 33);
			this.DragAndDropMdlFileForDecompileCheckBox.Name = "DragAndDropMdlFileForDecompileCheckBox";
			this.DragAndDropMdlFileForDecompileCheckBox.Size = new System.Drawing.Size(80, 17);
			this.DragAndDropMdlFileForDecompileCheckBox.TabIndex = 1;
			this.DragAndDropMdlFileForDecompileCheckBox.Text = "Decompile";
			this.DragAndDropMdlFileForDecompileCheckBox.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForViewCheckBox
			//
			this.DragAndDropMdlFileForViewCheckBox.AutoSize = true;
			this.DragAndDropMdlFileForViewCheckBox.Location = new System.Drawing.Point(3, 48);
			this.DragAndDropMdlFileForViewCheckBox.Name = "DragAndDropMdlFileForViewCheckBox";
			this.DragAndDropMdlFileForViewCheckBox.Size = new System.Drawing.Size(51, 17);
			this.DragAndDropMdlFileForViewCheckBox.TabIndex = 2;
			this.DragAndDropMdlFileForViewCheckBox.Text = "View";
			this.DragAndDropMdlFileForViewCheckBox.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForPreviewCheckBox
			//
			this.DragAndDropMdlFileForPreviewCheckBox.AutoSize = true;
			this.DragAndDropMdlFileForPreviewCheckBox.Location = new System.Drawing.Point(3, 18);
			this.DragAndDropMdlFileForPreviewCheckBox.Name = "DragAndDropMdlFileForPreviewCheckBox";
			this.DragAndDropMdlFileForPreviewCheckBox.Size = new System.Drawing.Size(65, 17);
			this.DragAndDropMdlFileForPreviewCheckBox.TabIndex = 0;
			this.DragAndDropMdlFileForPreviewCheckBox.Text = "Preview";
			this.DragAndDropMdlFileForPreviewCheckBox.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForPreviewingRadioButton
			//
			this.DragAndDropMdlFileForPreviewingRadioButton.Location = new System.Drawing.Point(104, 19);
			this.DragAndDropMdlFileForPreviewingRadioButton.Name = "DragAndDropMdlFileForPreviewingRadioButton";
			this.DragAndDropMdlFileForPreviewingRadioButton.Size = new System.Drawing.Size(14, 13);
			this.DragAndDropMdlFileForPreviewingRadioButton.TabIndex = 3;
			this.DragAndDropMdlFileForPreviewingRadioButton.TabStop = true;
			this.DragAndDropMdlFileForPreviewingRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForDecompilingRadioButton
			//
			this.DragAndDropMdlFileForDecompilingRadioButton.Location = new System.Drawing.Point(104, 34);
			this.DragAndDropMdlFileForDecompilingRadioButton.Name = "DragAndDropMdlFileForDecompilingRadioButton";
			this.DragAndDropMdlFileForDecompilingRadioButton.Size = new System.Drawing.Size(14, 13);
			this.DragAndDropMdlFileForDecompilingRadioButton.TabIndex = 4;
			this.DragAndDropMdlFileForDecompilingRadioButton.TabStop = true;
			this.DragAndDropMdlFileForDecompilingRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropMdlFileForViewingRadioButton
			//
			this.DragAndDropMdlFileForViewingRadioButton.Location = new System.Drawing.Point(104, 49);
			this.DragAndDropMdlFileForViewingRadioButton.Name = "DragAndDropMdlFileForViewingRadioButton";
			this.DragAndDropMdlFileForViewingRadioButton.Size = new System.Drawing.Size(14, 13);
			this.DragAndDropMdlFileForViewingRadioButton.TabIndex = 5;
			this.DragAndDropMdlFileForViewingRadioButton.TabStop = true;
			this.DragAndDropMdlFileForViewingRadioButton.UseVisualStyleBackColor = true;
			//
			//Label3
			//
			this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Label3.Location = new System.Drawing.Point(6, 20);
			this.Label3.Margin = new System.Windows.Forms.Padding(3);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(188, 83);
			this.Label3.TabIndex = 19;
			this.Label3.Text = "Choose which tabs Crowbar sets up and which tab opens when a file or folder is dr" + "opped on Crowbar. Dropping on any tab left unset below will only set up that tab" + ".";
			//
			//DragAndDropUseDefaultsButton
			//
			this.DragAndDropUseDefaultsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.DragAndDropUseDefaultsButton.Location = new System.Drawing.Point(50, 438);
			this.DragAndDropUseDefaultsButton.Name = "DragAndDropUseDefaultsButton";
			this.DragAndDropUseDefaultsButton.Size = new System.Drawing.Size(100, 23);
			this.DragAndDropUseDefaultsButton.TabIndex = 4;
			this.DragAndDropUseDefaultsButton.Text = "Use Defaults";
			this.DragAndDropUseDefaultsButton.UseVisualStyleBackColor = true;
			//
			//Panel4
			//
			this.Panel4.BackColor = System.Drawing.SystemColors.Control;
			this.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel4.Controls.Add(this.DragAndDropFolderForPackRadioButton);
			this.Panel4.Controls.Add(this.DragAndDropFolderForCompileRadioButton);
			this.Panel4.Controls.Add(this.DragAndDropFolderForDecompileRadioButton);
			this.Panel4.Controls.Add(this.DragAndDropFolderForUnpackRadioButton);
			this.Panel4.Location = new System.Drawing.Point(81, 283);
			this.Panel4.Name = "Panel4";
			this.Panel4.Size = new System.Drawing.Size(89, 66);
			this.Panel4.TabIndex = 3;
			//
			//DragAndDropFolderForPackRadioButton
			//
			this.DragAndDropFolderForPackRadioButton.AutoSize = true;
			this.DragAndDropFolderForPackRadioButton.Location = new System.Drawing.Point(3, 45);
			this.DragAndDropFolderForPackRadioButton.Name = "DragAndDropFolderForPackRadioButton";
			this.DragAndDropFolderForPackRadioButton.Size = new System.Drawing.Size(48, 17);
			this.DragAndDropFolderForPackRadioButton.TabIndex = 3;
			this.DragAndDropFolderForPackRadioButton.TabStop = true;
			this.DragAndDropFolderForPackRadioButton.Text = "Pack";
			this.DragAndDropFolderForPackRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropFolderForCompileRadioButton
			//
			this.DragAndDropFolderForCompileRadioButton.AutoSize = true;
			this.DragAndDropFolderForCompileRadioButton.Location = new System.Drawing.Point(3, 30);
			this.DragAndDropFolderForCompileRadioButton.Name = "DragAndDropFolderForCompileRadioButton";
			this.DragAndDropFolderForCompileRadioButton.Size = new System.Drawing.Size(67, 17);
			this.DragAndDropFolderForCompileRadioButton.TabIndex = 2;
			this.DragAndDropFolderForCompileRadioButton.TabStop = true;
			this.DragAndDropFolderForCompileRadioButton.Text = "Compile";
			this.DragAndDropFolderForCompileRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropFolderForDecompileRadioButton
			//
			this.DragAndDropFolderForDecompileRadioButton.AutoSize = true;
			this.DragAndDropFolderForDecompileRadioButton.Location = new System.Drawing.Point(3, 15);
			this.DragAndDropFolderForDecompileRadioButton.Name = "DragAndDropFolderForDecompileRadioButton";
			this.DragAndDropFolderForDecompileRadioButton.Size = new System.Drawing.Size(79, 17);
			this.DragAndDropFolderForDecompileRadioButton.TabIndex = 1;
			this.DragAndDropFolderForDecompileRadioButton.TabStop = true;
			this.DragAndDropFolderForDecompileRadioButton.Text = "Decompile";
			this.DragAndDropFolderForDecompileRadioButton.UseVisualStyleBackColor = true;
			//
			//DragAndDropFolderForUnpackRadioButton
			//
			this.DragAndDropFolderForUnpackRadioButton.AutoSize = true;
			this.DragAndDropFolderForUnpackRadioButton.Location = new System.Drawing.Point(3, 0);
			this.DragAndDropFolderForUnpackRadioButton.Name = "DragAndDropFolderForUnpackRadioButton";
			this.DragAndDropFolderForUnpackRadioButton.Size = new System.Drawing.Size(64, 17);
			this.DragAndDropFolderForUnpackRadioButton.TabIndex = 0;
			this.DragAndDropFolderForUnpackRadioButton.TabStop = true;
			this.DragAndDropFolderForUnpackRadioButton.Text = "Unpack";
			this.DragAndDropFolderForUnpackRadioButton.UseVisualStyleBackColor = true;
			//
			//ApplyLabel
			//
			this.ApplyLabel.AutoSize = true;
			this.ApplyLabel.Location = new System.Drawing.Point(6, 8);
			this.ApplyLabel.Name = "ApplyLabel";
			this.ApplyLabel.Size = new System.Drawing.Size(509, 13);
			this.ApplyLabel.TabIndex = 18;
			this.ApplyLabel.Text = "Windows is not using what is specified above. Click the Apply button to apply the" + " options above.";
			//
			//ApplyButton
			//
			this.ApplyButton.Location = new System.Drawing.Point(521, 3);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new System.Drawing.Size(50, 23);
			this.ApplyButton.TabIndex = 20;
			this.ApplyButton.Text = "Apply";
			this.ApplyButton.UseVisualStyleBackColor = true;
			//
			//ApplyPanel
			//
			this.ApplyPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ApplyPanel.Controls.Add(this.ApplyLabel);
			this.ApplyPanel.Controls.Add(this.ApplyButton);
			this.ApplyPanel.Location = new System.Drawing.Point(3, 499);
			this.ApplyPanel.Name = "ApplyPanel";
			this.ApplyPanel.Size = new System.Drawing.Size(585, 34);
			this.ApplyPanel.TabIndex = 3;
			this.ApplyPanel.Visible = false;
			//
			//SingleInstanceCheckBox
			//
			this.SingleInstanceCheckBox.AutoSize = true;
			this.SingleInstanceCheckBox.Location = new System.Drawing.Point(3, 3);
			this.SingleInstanceCheckBox.Name = "SingleInstanceCheckBox";
			this.SingleInstanceCheckBox.Size = new System.Drawing.Size(336, 17);
			this.SingleInstanceCheckBox.TabIndex = 4;
			this.SingleInstanceCheckBox.Text = "Restrict to single instance (only one Crowbar open at a time)";
			this.SingleInstanceCheckBox.UseVisualStyleBackColor = true;
			//
			//OptionsUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SingleInstanceCheckBox);
			this.Controls.Add(this.ApplyPanel);
			this.Controls.Add(this.GroupBox3);
			this.Controls.Add(this.GroupBox2);
			this.Controls.Add(this.GroupBox1);
			this.Name = "OptionsUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			this.Panel7.ResumeLayout(false);
			this.Panel7.PerformLayout();
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox2.PerformLayout();
			this.Panel2.ResumeLayout(false);
			this.Panel2.PerformLayout();
			this.AutoOpenVpkPanel.ResumeLayout(false);
			this.AutoOpenVpkPanel.PerformLayout();
			this.AutoOpenFolderPanel.ResumeLayout(false);
			this.AutoOpenFolderPanel.PerformLayout();
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			this.GroupBox3.ResumeLayout(false);
			this.GroupBox3.PerformLayout();
			this.Panel5.ResumeLayout(false);
			this.Panel5.PerformLayout();
			this.Panel6.ResumeLayout(false);
			this.Panel6.PerformLayout();
			this.Panel3.ResumeLayout(false);
			this.Panel3.PerformLayout();
			this.Panel4.ResumeLayout(false);
			this.Panel4.PerformLayout();
			this.ApplyPanel.ResumeLayout(false);
			this.ApplyPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

//INSTANT C# NOTE: Converted design-time event handler wireups:
			this.Disposed += new System.EventHandler(OptionsUserControl_Disposed);
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