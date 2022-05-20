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
	public partial class PatchUserControl : BaseUserControl
	{
		//'UserControl overrides dispose to clean up the component list.
		//<System.Diagnostics.DebuggerNonUserCode()> _
		//Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		//	Try
		//		If disposing AndAlso components IsNot Nothing Then
		//			components.Dispose()
		//		End If
		//	Finally
		//		MyBase.Dispose(disposing)
		//	End Try
		//End Sub

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.ViewButton = new System.Windows.Forms.Button();
			this.MdlPathFileNameTextBox = new Crowbar.TextBoxEx();
			this.BrowseForMdlFileButton = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.Panel2 = new System.Windows.Forms.Panel();
			this.DecompileComboBox = new System.Windows.Forms.ComboBox();
			this.GotoMdlFileButton = new System.Windows.Forms.Button();
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			this.PatchableValuesForSingleMDLGroupBox = new System.Windows.Forms.GroupBox();
			this.GroupBox5 = new System.Windows.Forms.GroupBox();
			this.DataGridView4 = new System.Windows.Forms.DataGridView();
			this.Label9 = new System.Windows.Forms.Label();
			this.TextBoxEx6 = new Crowbar.TextBoxEx();
			this.TextBoxEx5 = new Crowbar.TextBoxEx();
			this.TextBoxEx4 = new Crowbar.TextBoxEx();
			this.Label8 = new System.Windows.Forms.Label();
			this.GroupBox4 = new System.Windows.Forms.GroupBox();
			this.DataGridView3 = new System.Windows.Forms.DataGridView();
			this.Button9 = new System.Windows.Forms.Button();
			this.Button11 = new System.Windows.Forms.Button();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label7 = new System.Windows.Forms.Label();
			this.TextBoxEx3 = new Crowbar.TextBoxEx();
			this.GroupBox3 = new System.Windows.Forms.GroupBox();
			this.DataGridView2 = new System.Windows.Forms.DataGridView();
			this.Button5 = new System.Windows.Forms.Button();
			this.Button6 = new System.Windows.Forms.Button();
			this.Button7 = new System.Windows.Forms.Button();
			this.Button8 = new System.Windows.Forms.Button();
			this.GroupBox2 = new System.Windows.Forms.GroupBox();
			this.DataGridView1 = new System.Windows.Forms.DataGridView();
			this.Button4 = new System.Windows.Forms.Button();
			this.Button1 = new System.Windows.Forms.Button();
			this.Button3 = new System.Windows.Forms.Button();
			this.Button2 = new System.Windows.Forms.Button();
			this.CheckBox3 = new System.Windows.Forms.CheckBox();
			this.CheckBox2 = new System.Windows.Forms.CheckBox();
			this.CheckBox1 = new System.Windows.Forms.CheckBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.TextBoxEx2 = new Crowbar.TextBoxEx();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.TextBoxEx1 = new Crowbar.TextBoxEx();
			this.CancelDecompileButton = new System.Windows.Forms.Button();
			this.SkipCurrentModelButton = new System.Windows.Forms.Button();
			this.MessageTextBox = new Crowbar.TextBoxEx();
			this.Label10 = new System.Windows.Forms.Label();
			this.Label11 = new System.Windows.Forms.Label();
			this.RefreshOrRevertButton = new System.Windows.Forms.Button();
			this.PatchableValuesForMultipleMDLsGroupBox = new System.Windows.Forms.GroupBox();
			this.Button10 = new System.Windows.Forms.Button();
			this.GroupBox6 = new System.Windows.Forms.GroupBox();
			this.DataGridView5 = new System.Windows.Forms.DataGridView();
			this.Label14 = new System.Windows.Forms.Label();
			this.TextBoxEx7 = new Crowbar.TextBoxEx();
			this.TextBoxEx8 = new Crowbar.TextBoxEx();
			this.TextBoxEx9 = new Crowbar.TextBoxEx();
			this.Label15 = new System.Windows.Forms.Label();
			this.GroupBox7 = new System.Windows.Forms.GroupBox();
			this.DataGridView6 = new System.Windows.Forms.DataGridView();
			this.Button12 = new System.Windows.Forms.Button();
			this.Button13 = new System.Windows.Forms.Button();
			this.Label16 = new System.Windows.Forms.Label();
			this.Label17 = new System.Windows.Forms.Label();
			this.TextBoxEx10 = new Crowbar.TextBoxEx();
			this.GroupBox8 = new System.Windows.Forms.GroupBox();
			this.DataGridView7 = new System.Windows.Forms.DataGridView();
			this.Button14 = new System.Windows.Forms.Button();
			this.Button15 = new System.Windows.Forms.Button();
			this.Button16 = new System.Windows.Forms.Button();
			this.Button17 = new System.Windows.Forms.Button();
			this.GroupBox9 = new System.Windows.Forms.GroupBox();
			this.DataGridView8 = new System.Windows.Forms.DataGridView();
			this.Button18 = new System.Windows.Forms.Button();
			this.Button19 = new System.Windows.Forms.Button();
			this.Button20 = new System.Windows.Forms.Button();
			this.Button21 = new System.Windows.Forms.Button();
			this.CheckBox4 = new System.Windows.Forms.CheckBox();
			this.CheckBox5 = new System.Windows.Forms.CheckBox();
			this.CheckBox6 = new System.Windows.Forms.CheckBox();
			this.Label12 = new System.Windows.Forms.Label();
			this.Panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			this.PatchableValuesForSingleMDLGroupBox.SuspendLayout();
			this.GroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DataGridView4).BeginInit();
			this.GroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DataGridView3).BeginInit();
			this.GroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DataGridView2).BeginInit();
			this.GroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DataGridView1).BeginInit();
			this.PatchableValuesForMultipleMDLsGroupBox.SuspendLayout();
			this.GroupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DataGridView5).BeginInit();
			this.GroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DataGridView6).BeginInit();
			this.GroupBox8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DataGridView7).BeginInit();
			this.GroupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DataGridView8).BeginInit();
			this.SuspendLayout();
			//
			//ViewButton
			//
			this.ViewButton.Enabled = false;
			this.ViewButton.Location = new System.Drawing.Point(0, 2);
			this.ViewButton.Name = "ViewButton";
			this.ViewButton.Size = new System.Drawing.Size(50, 23);
			this.ViewButton.TabIndex = 8;
			this.ViewButton.Text = "Patch";
			this.ViewButton.UseVisualStyleBackColor = true;
			//
			//MdlPathFileNameTextBox
			//
			this.MdlPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.MdlPathFileNameTextBox.Location = new System.Drawing.Point(209, 5);
			this.MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox";
			this.MdlPathFileNameTextBox.Size = new System.Drawing.Size(441, 21);
			this.MdlPathFileNameTextBox.TabIndex = 1;
			//
			//BrowseForMdlFileButton
			//
			this.BrowseForMdlFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForMdlFileButton.Location = new System.Drawing.Point(660, 3);
			this.BrowseForMdlFileButton.Name = "BrowseForMdlFileButton";
			this.BrowseForMdlFileButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForMdlFileButton.TabIndex = 2;
			this.BrowseForMdlFileButton.Text = "Browse...";
			this.BrowseForMdlFileButton.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(3, 8);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(58, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "MDL input:";
			//
			//Panel2
			//
			this.Panel2.Controls.Add(this.DecompileComboBox);
			this.Panel2.Controls.Add(this.Label1);
			this.Panel2.Controls.Add(this.MdlPathFileNameTextBox);
			this.Panel2.Controls.Add(this.BrowseForMdlFileButton);
			this.Panel2.Controls.Add(this.GotoMdlFileButton);
			this.Panel2.Controls.Add(this.SplitContainer1);
			this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel2.Location = new System.Drawing.Point(0, 0);
			this.Panel2.Margin = new System.Windows.Forms.Padding(2);
			this.Panel2.Name = "Panel2";
			this.Panel2.Size = new System.Drawing.Size(776, 536);
			this.Panel2.TabIndex = 8;
			//
			//DecompileComboBox
			//
			this.DecompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DecompileComboBox.FormattingEnabled = true;
			this.DecompileComboBox.Location = new System.Drawing.Point(63, 4);
			this.DecompileComboBox.Name = "DecompileComboBox";
			this.DecompileComboBox.Size = new System.Drawing.Size(140, 21);
			this.DecompileComboBox.TabIndex = 14;
			//
			//GotoMdlFileButton
			//
			this.GotoMdlFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoMdlFileButton.Location = new System.Drawing.Point(730, 3);
			this.GotoMdlFileButton.Name = "GotoMdlFileButton";
			this.GotoMdlFileButton.Size = new System.Drawing.Size(43, 23);
			this.GotoMdlFileButton.TabIndex = 3;
			this.GotoMdlFileButton.Text = "Goto";
			this.GotoMdlFileButton.UseVisualStyleBackColor = true;
			//
			//SplitContainer1
			//
			this.SplitContainer1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.SplitContainer1.Location = new System.Drawing.Point(3, 32);
			this.SplitContainer1.Name = "SplitContainer1";
			this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//SplitContainer1.Panel1
			//
			this.SplitContainer1.Panel1.Controls.Add(this.PatchableValuesForSingleMDLGroupBox);
			this.SplitContainer1.Panel1.Controls.Add(this.PatchableValuesForMultipleMDLsGroupBox);
			this.SplitContainer1.Panel1MinSize = 45;
			//
			//SplitContainer1.Panel2
			//
			this.SplitContainer1.Panel2.Controls.Add(this.Label12);
			this.SplitContainer1.Panel2.Controls.Add(this.CancelDecompileButton);
			this.SplitContainer1.Panel2.Controls.Add(this.SkipCurrentModelButton);
			this.SplitContainer1.Panel2.Controls.Add(this.ViewButton);
			this.SplitContainer1.Panel2.Controls.Add(this.MessageTextBox);
			this.SplitContainer1.Panel2MinSize = 45;
			this.SplitContainer1.Size = new System.Drawing.Size(770, 501);
			this.SplitContainer1.SplitterDistance = 384;
			this.SplitContainer1.TabIndex = 13;
			//
			//PatchableValuesForSingleMDLGroupBox
			//
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.RefreshOrRevertButton);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label11);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label10);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.GroupBox5);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label9);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.TextBoxEx6);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.TextBoxEx5);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.TextBoxEx4);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label8);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.GroupBox4);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label6);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label7);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.TextBoxEx3);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.GroupBox3);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.GroupBox2);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.CheckBox3);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.CheckBox2);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.CheckBox1);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label4);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label5);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.TextBoxEx2);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label3);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.Label2);
			this.PatchableValuesForSingleMDLGroupBox.Controls.Add(this.TextBoxEx1);
			this.PatchableValuesForSingleMDLGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PatchableValuesForSingleMDLGroupBox.Location = new System.Drawing.Point(0, 0);
			this.PatchableValuesForSingleMDLGroupBox.Name = "PatchableValuesForSingleMDLGroupBox";
			this.PatchableValuesForSingleMDLGroupBox.Size = new System.Drawing.Size(770, 384);
			this.PatchableValuesForSingleMDLGroupBox.TabIndex = 4;
			this.PatchableValuesForSingleMDLGroupBox.TabStop = false;
			this.PatchableValuesForSingleMDLGroupBox.Text = "Patchable Values (for single MDL)";
			//
			//GroupBox5
			//
			this.GroupBox5.Controls.Add(this.DataGridView4);
			this.GroupBox5.Location = new System.Drawing.Point(323, 137);
			this.GroupBox5.Name = "GroupBox5";
			this.GroupBox5.Size = new System.Drawing.Size(280, 100);
			this.GroupBox5.TabIndex = 19;
			this.GroupBox5.TabStop = false;
			this.GroupBox5.Text = "Hitboxes";
			//
			//DataGridView4
			//
			this.DataGridView4.AllowUserToAddRows = false;
			this.DataGridView4.AllowUserToDeleteRows = false;
			this.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView4.Location = new System.Drawing.Point(6, 13);
			this.DataGridView4.Name = "DataGridView4";
			this.DataGridView4.Size = new System.Drawing.Size(271, 81);
			this.DataGridView4.TabIndex = 11;
			//
			//Label9
			//
			this.Label9.AutoSize = true;
			this.Label9.Location = new System.Drawing.Point(223, 98);
			this.Label9.Name = "Label9";
			this.Label9.Size = new System.Drawing.Size(39, 13);
			this.Label9.TabIndex = 26;
			this.Label9.Text = "(X Y Z)";
			//
			//TextBoxEx6
			//
			this.TextBoxEx6.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx6.Location = new System.Drawing.Point(177, 95);
			this.TextBoxEx6.Name = "TextBoxEx6";
			this.TextBoxEx6.Size = new System.Drawing.Size(40, 21);
			this.TextBoxEx6.TabIndex = 25;
			//
			//TextBoxEx5
			//
			this.TextBoxEx5.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx5.Location = new System.Drawing.Point(131, 95);
			this.TextBoxEx5.Name = "TextBoxEx5";
			this.TextBoxEx5.Size = new System.Drawing.Size(40, 21);
			this.TextBoxEx5.TabIndex = 24;
			//
			//TextBoxEx4
			//
			this.TextBoxEx4.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx4.Location = new System.Drawing.Point(85, 95);
			this.TextBoxEx4.Name = "TextBoxEx4";
			this.TextBoxEx4.Size = new System.Drawing.Size(40, 21);
			this.TextBoxEx4.TabIndex = 23;
			//
			//Label8
			//
			this.Label8.AutoSize = true;
			this.Label8.Location = new System.Drawing.Point(6, 98);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(73, 13);
			this.Label8.TabIndex = 22;
			this.Label8.Text = "Illum Position:";
			//
			//GroupBox4
			//
			this.GroupBox4.Controls.Add(this.DataGridView3);
			this.GroupBox4.Controls.Add(this.Button9);
			this.GroupBox4.Controls.Add(this.Button11);
			this.GroupBox4.Location = new System.Drawing.Point(323, 243);
			this.GroupBox4.Name = "GroupBox4";
			this.GroupBox4.Size = new System.Drawing.Size(280, 100);
			this.GroupBox4.TabIndex = 18;
			this.GroupBox4.TabStop = false;
			this.GroupBox4.Text = "Body Group Names";
			//
			//DataGridView3
			//
			this.DataGridView3.AllowUserToAddRows = false;
			this.DataGridView3.AllowUserToDeleteRows = false;
			this.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView3.Location = new System.Drawing.Point(6, 13);
			this.DataGridView3.Name = "DataGridView3";
			this.DataGridView3.Size = new System.Drawing.Size(240, 81);
			this.DataGridView3.TabIndex = 11;
			//
			//Button9
			//
			this.Button9.Location = new System.Drawing.Point(252, 42);
			this.Button9.Name = "Button9";
			this.Button9.Size = new System.Drawing.Size(25, 23);
			this.Button9.TabIndex = 16;
			this.Button9.Text = "Dn";
			this.Button9.UseVisualStyleBackColor = true;
			//
			//Button11
			//
			this.Button11.Location = new System.Drawing.Point(252, 13);
			this.Button11.Name = "Button11";
			this.Button11.Size = new System.Drawing.Size(25, 23);
			this.Button11.TabIndex = 15;
			this.Button11.Text = "Up";
			this.Button11.UseVisualStyleBackColor = true;
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(698, 71);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(66, 13);
			this.Label6.TabIndex = 21;
			this.Label6.Text = "(any length)";
			//
			//Label7
			//
			this.Label7.AutoSize = true;
			this.Label7.Location = new System.Drawing.Point(6, 71);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(75, 13);
			this.Label7.TabIndex = 20;
			this.Label7.Text = "ANI file name:";
			//
			//TextBoxEx3
			//
			this.TextBoxEx3.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx3.Location = new System.Drawing.Point(122, 68);
			this.TextBoxEx3.Name = "TextBoxEx3";
			this.TextBoxEx3.Size = new System.Drawing.Size(570, 21);
			this.TextBoxEx3.TabIndex = 19;
			//
			//GroupBox3
			//
			this.GroupBox3.Controls.Add(this.DataGridView2);
			this.GroupBox3.Controls.Add(this.Button5);
			this.GroupBox3.Controls.Add(this.Button6);
			this.GroupBox3.Controls.Add(this.Button7);
			this.GroupBox3.Controls.Add(this.Button8);
			this.GroupBox3.Location = new System.Drawing.Point(6, 243);
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.Size = new System.Drawing.Size(311, 100);
			this.GroupBox3.TabIndex = 18;
			this.GroupBox3.TabStop = false;
			this.GroupBox3.Text = "Include Models";
			//
			//DataGridView2
			//
			this.DataGridView2.AllowUserToAddRows = false;
			this.DataGridView2.AllowUserToDeleteRows = false;
			this.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView2.Location = new System.Drawing.Point(6, 13);
			this.DataGridView2.Name = "DataGridView2";
			this.DataGridView2.Size = new System.Drawing.Size(240, 81);
			this.DataGridView2.TabIndex = 11;
			//
			//Button5
			//
			this.Button5.Location = new System.Drawing.Point(283, 71);
			this.Button5.Name = "Button5";
			this.Button5.Size = new System.Drawing.Size(25, 23);
			this.Button5.TabIndex = 16;
			this.Button5.Text = "Dn";
			this.Button5.UseVisualStyleBackColor = true;
			//
			//Button6
			//
			this.Button6.Location = new System.Drawing.Point(252, 13);
			this.Button6.Name = "Button6";
			this.Button6.Size = new System.Drawing.Size(56, 23);
			this.Button6.TabIndex = 13;
			this.Button6.Text = "Add";
			this.Button6.UseVisualStyleBackColor = true;
			//
			//Button7
			//
			this.Button7.Location = new System.Drawing.Point(252, 71);
			this.Button7.Name = "Button7";
			this.Button7.Size = new System.Drawing.Size(25, 23);
			this.Button7.TabIndex = 15;
			this.Button7.Text = "Up";
			this.Button7.UseVisualStyleBackColor = true;
			//
			//Button8
			//
			this.Button8.Location = new System.Drawing.Point(252, 42);
			this.Button8.Name = "Button8";
			this.Button8.Size = new System.Drawing.Size(56, 23);
			this.Button8.TabIndex = 14;
			this.Button8.Text = "Delete";
			this.Button8.UseVisualStyleBackColor = true;
			//
			//GroupBox2
			//
			this.GroupBox2.Controls.Add(this.DataGridView1);
			this.GroupBox2.Controls.Add(this.Button4);
			this.GroupBox2.Controls.Add(this.Button1);
			this.GroupBox2.Controls.Add(this.Button3);
			this.GroupBox2.Controls.Add(this.Button2);
			this.GroupBox2.Location = new System.Drawing.Point(6, 137);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new System.Drawing.Size(311, 100);
			this.GroupBox2.TabIndex = 17;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "CD Materials";
			//
			//DataGridView1
			//
			this.DataGridView1.AllowUserToAddRows = false;
			this.DataGridView1.AllowUserToDeleteRows = false;
			this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView1.Location = new System.Drawing.Point(6, 13);
			this.DataGridView1.Name = "DataGridView1";
			this.DataGridView1.Size = new System.Drawing.Size(240, 81);
			this.DataGridView1.TabIndex = 11;
			//
			//Button4
			//
			this.Button4.Location = new System.Drawing.Point(283, 71);
			this.Button4.Name = "Button4";
			this.Button4.Size = new System.Drawing.Size(25, 23);
			this.Button4.TabIndex = 16;
			this.Button4.Text = "Dn";
			this.Button4.UseVisualStyleBackColor = true;
			//
			//Button1
			//
			this.Button1.Location = new System.Drawing.Point(252, 13);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(56, 23);
			this.Button1.TabIndex = 13;
			this.Button1.Text = "Add";
			this.Button1.UseVisualStyleBackColor = true;
			//
			//Button3
			//
			this.Button3.Location = new System.Drawing.Point(252, 71);
			this.Button3.Name = "Button3";
			this.Button3.Size = new System.Drawing.Size(25, 23);
			this.Button3.TabIndex = 15;
			this.Button3.Text = "Up";
			this.Button3.UseVisualStyleBackColor = true;
			//
			//Button2
			//
			this.Button2.Location = new System.Drawing.Point(252, 42);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(56, 23);
			this.Button2.TabIndex = 14;
			this.Button2.Text = "Delete";
			this.Button2.UseVisualStyleBackColor = true;
			//
			//CheckBox3
			//
			this.CheckBox3.AutoSize = true;
			this.CheckBox3.Location = new System.Drawing.Point(515, 97);
			this.CheckBox3.Name = "CheckBox3";
			this.CheckBox3.Size = new System.Drawing.Size(98, 17);
			this.CheckBox3.TabIndex = 10;
			this.CheckBox3.Text = "Mostly Opaque";
			this.CheckBox3.UseVisualStyleBackColor = true;
			//
			//CheckBox2
			//
			this.CheckBox2.AutoSize = true;
			this.CheckBox2.Location = new System.Drawing.Point(434, 97);
			this.CheckBox2.Name = "CheckBox2";
			this.CheckBox2.Size = new System.Drawing.Size(64, 17);
			this.CheckBox2.TabIndex = 9;
			this.CheckBox2.Text = "Opaque";
			this.CheckBox2.UseVisualStyleBackColor = true;
			//
			//CheckBox1
			//
			this.CheckBox1.AutoSize = true;
			this.CheckBox1.Location = new System.Drawing.Point(320, 97);
			this.CheckBox1.Name = "CheckBox1";
			this.CheckBox1.Size = new System.Drawing.Size(95, 17);
			this.CheckBox1.TabIndex = 8;
			this.CheckBox1.Text = "Ambient Boost";
			this.CheckBox1.UseVisualStyleBackColor = true;
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(698, 44);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(66, 13);
			this.Label4.TabIndex = 7;
			this.Label4.Text = "(any length)";
			//
			//Label5
			//
			this.Label5.AutoSize = true;
			this.Label5.Location = new System.Drawing.Point(6, 44);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(110, 13);
			this.Label5.TabIndex = 6;
			this.Label5.Text = "Internal MDL name 2:";
			//
			//TextBoxEx2
			//
			this.TextBoxEx2.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx2.Location = new System.Drawing.Point(122, 41);
			this.TextBoxEx2.Name = "TextBoxEx2";
			this.TextBoxEx2.Size = new System.Drawing.Size(570, 21);
			this.TextBoxEx2.TabIndex = 5;
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(569, 17);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(56, 13);
			this.Label3.TabIndex = 4;
			this.Label3.Text = "(64 chars)";
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(6, 17);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(101, 13);
			this.Label2.TabIndex = 3;
			this.Label2.Text = "Internal MDL name:";
			//
			//TextBoxEx1
			//
			this.TextBoxEx1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx1.Location = new System.Drawing.Point(122, 14);
			this.TextBoxEx1.Name = "TextBoxEx1";
			this.TextBoxEx1.Size = new System.Drawing.Size(441, 21);
			this.TextBoxEx1.TabIndex = 2;
			//
			//CancelDecompileButton
			//
			this.CancelDecompileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.CancelDecompileButton.Enabled = false;
			this.CancelDecompileButton.Location = new System.Drawing.Point(182, 2);
			this.CancelDecompileButton.Name = "CancelDecompileButton";
			this.CancelDecompileButton.Size = new System.Drawing.Size(80, 23);
			this.CancelDecompileButton.TabIndex = 14;
			this.CancelDecompileButton.Text = "Cancel Patch";
			this.CancelDecompileButton.UseVisualStyleBackColor = true;
			//
			//SkipCurrentModelButton
			//
			this.SkipCurrentModelButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.SkipCurrentModelButton.Enabled = false;
			this.SkipCurrentModelButton.Location = new System.Drawing.Point(56, 2);
			this.SkipCurrentModelButton.Name = "SkipCurrentModelButton";
			this.SkipCurrentModelButton.Size = new System.Drawing.Size(120, 23);
			this.SkipCurrentModelButton.TabIndex = 13;
			this.SkipCurrentModelButton.Text = "Skip Current Model";
			this.SkipCurrentModelButton.UseVisualStyleBackColor = true;
			//
			//MessageTextBox
			//
			this.MessageTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.MessageTextBox.Location = new System.Drawing.Point(0, 31);
			this.MessageTextBox.Multiline = true;
			this.MessageTextBox.Name = "MessageTextBox";
			this.MessageTextBox.ReadOnly = true;
			this.MessageTextBox.Size = new System.Drawing.Size(770, 81);
			this.MessageTextBox.TabIndex = 12;
			//
			//Label10
			//
			this.Label10.AutoSize = true;
			this.Label10.Location = new System.Drawing.Point(9, 357);
			this.Label10.Name = "Label10";
			this.Label10.Size = new System.Drawing.Size(212, 13);
			this.Label10.TabIndex = 27;
			this.Label10.Text = "TODO: Show this groupbox for single MDL.";
			//
			//Label11
			//
			this.Label11.AutoSize = true;
			this.Label11.Location = new System.Drawing.Point(227, 357);
			this.Label11.Name = "Label11";
			this.Label11.Size = new System.Drawing.Size(539, 13);
			this.Label11.TabIndex = 28;
			this.Label11.Text = "TODO: Show a different groupbox (already started) for multiple MDLs showing optio" + "ns such as Mostly Opaque.";
			//
			//RefreshOrRevertButton
			//
			this.RefreshOrRevertButton.Enabled = false;
			this.RefreshOrRevertButton.Location = new System.Drawing.Point(657, 256);
			this.RefreshOrRevertButton.Name = "RefreshOrRevertButton";
			this.RefreshOrRevertButton.Size = new System.Drawing.Size(92, 23);
			this.RefreshOrRevertButton.TabIndex = 15;
			this.RefreshOrRevertButton.Text = "Refresh/Revert";
			this.RefreshOrRevertButton.UseVisualStyleBackColor = true;
			//
			//PatchableValuesForMultipleMDLsGroupBox
			//
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.Button10);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.GroupBox6);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.Label14);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.TextBoxEx7);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.TextBoxEx8);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.TextBoxEx9);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.Label15);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.GroupBox7);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.Label16);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.Label17);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.TextBoxEx10);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.GroupBox8);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.GroupBox9);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.CheckBox4);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.CheckBox5);
			this.PatchableValuesForMultipleMDLsGroupBox.Controls.Add(this.CheckBox6);
			this.PatchableValuesForMultipleMDLsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PatchableValuesForMultipleMDLsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.PatchableValuesForMultipleMDLsGroupBox.Name = "PatchableValuesForMultipleMDLsGroupBox";
			this.PatchableValuesForMultipleMDLsGroupBox.Size = new System.Drawing.Size(770, 384);
			this.PatchableValuesForMultipleMDLsGroupBox.TabIndex = 29;
			this.PatchableValuesForMultipleMDLsGroupBox.TabStop = false;
			this.PatchableValuesForMultipleMDLsGroupBox.Text = "Patchable Values (for multiple MDLs)";
			//
			//Button10
			//
			this.Button10.Enabled = false;
			this.Button10.Location = new System.Drawing.Point(657, 256);
			this.Button10.Name = "Button10";
			this.Button10.Size = new System.Drawing.Size(92, 23);
			this.Button10.TabIndex = 15;
			this.Button10.Text = "Refresh/Revert";
			this.Button10.UseVisualStyleBackColor = true;
			//
			//GroupBox6
			//
			this.GroupBox6.Controls.Add(this.DataGridView5);
			this.GroupBox6.Location = new System.Drawing.Point(323, 137);
			this.GroupBox6.Name = "GroupBox6";
			this.GroupBox6.Size = new System.Drawing.Size(280, 100);
			this.GroupBox6.TabIndex = 19;
			this.GroupBox6.TabStop = false;
			this.GroupBox6.Text = "Hitboxes";
			//
			//DataGridView5
			//
			this.DataGridView5.AllowUserToAddRows = false;
			this.DataGridView5.AllowUserToDeleteRows = false;
			this.DataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView5.Location = new System.Drawing.Point(6, 13);
			this.DataGridView5.Name = "DataGridView5";
			this.DataGridView5.Size = new System.Drawing.Size(271, 81);
			this.DataGridView5.TabIndex = 11;
			//
			//Label14
			//
			this.Label14.AutoSize = true;
			this.Label14.Location = new System.Drawing.Point(223, 98);
			this.Label14.Name = "Label14";
			this.Label14.Size = new System.Drawing.Size(39, 13);
			this.Label14.TabIndex = 26;
			this.Label14.Text = "(X Y Z)";
			//
			//TextBoxEx7
			//
			this.TextBoxEx7.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx7.Location = new System.Drawing.Point(177, 95);
			this.TextBoxEx7.Name = "TextBoxEx7";
			this.TextBoxEx7.Size = new System.Drawing.Size(40, 21);
			this.TextBoxEx7.TabIndex = 25;
			//
			//TextBoxEx8
			//
			this.TextBoxEx8.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx8.Location = new System.Drawing.Point(131, 95);
			this.TextBoxEx8.Name = "TextBoxEx8";
			this.TextBoxEx8.Size = new System.Drawing.Size(40, 21);
			this.TextBoxEx8.TabIndex = 24;
			//
			//TextBoxEx9
			//
			this.TextBoxEx9.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx9.Location = new System.Drawing.Point(85, 95);
			this.TextBoxEx9.Name = "TextBoxEx9";
			this.TextBoxEx9.Size = new System.Drawing.Size(40, 21);
			this.TextBoxEx9.TabIndex = 23;
			//
			//Label15
			//
			this.Label15.AutoSize = true;
			this.Label15.Location = new System.Drawing.Point(6, 98);
			this.Label15.Name = "Label15";
			this.Label15.Size = new System.Drawing.Size(73, 13);
			this.Label15.TabIndex = 22;
			this.Label15.Text = "Illum Position:";
			//
			//GroupBox7
			//
			this.GroupBox7.Controls.Add(this.DataGridView6);
			this.GroupBox7.Controls.Add(this.Button12);
			this.GroupBox7.Controls.Add(this.Button13);
			this.GroupBox7.Location = new System.Drawing.Point(323, 243);
			this.GroupBox7.Name = "GroupBox7";
			this.GroupBox7.Size = new System.Drawing.Size(280, 100);
			this.GroupBox7.TabIndex = 18;
			this.GroupBox7.TabStop = false;
			this.GroupBox7.Text = "Body Group Names";
			//
			//DataGridView6
			//
			this.DataGridView6.AllowUserToAddRows = false;
			this.DataGridView6.AllowUserToDeleteRows = false;
			this.DataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView6.Location = new System.Drawing.Point(6, 13);
			this.DataGridView6.Name = "DataGridView6";
			this.DataGridView6.Size = new System.Drawing.Size(240, 81);
			this.DataGridView6.TabIndex = 11;
			//
			//Button12
			//
			this.Button12.Location = new System.Drawing.Point(252, 42);
			this.Button12.Name = "Button12";
			this.Button12.Size = new System.Drawing.Size(25, 23);
			this.Button12.TabIndex = 16;
			this.Button12.Text = "Dn";
			this.Button12.UseVisualStyleBackColor = true;
			//
			//Button13
			//
			this.Button13.Location = new System.Drawing.Point(252, 13);
			this.Button13.Name = "Button13";
			this.Button13.Size = new System.Drawing.Size(25, 23);
			this.Button13.TabIndex = 15;
			this.Button13.Text = "Up";
			this.Button13.UseVisualStyleBackColor = true;
			//
			//Label16
			//
			this.Label16.AutoSize = true;
			this.Label16.Location = new System.Drawing.Point(698, 71);
			this.Label16.Name = "Label16";
			this.Label16.Size = new System.Drawing.Size(66, 13);
			this.Label16.TabIndex = 21;
			this.Label16.Text = "(any length)";
			//
			//Label17
			//
			this.Label17.AutoSize = true;
			this.Label17.Location = new System.Drawing.Point(6, 71);
			this.Label17.Name = "Label17";
			this.Label17.Size = new System.Drawing.Size(75, 13);
			this.Label17.TabIndex = 20;
			this.Label17.Text = "ANI file name:";
			//
			//TextBoxEx10
			//
			this.TextBoxEx10.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBoxEx10.Location = new System.Drawing.Point(122, 68);
			this.TextBoxEx10.Name = "TextBoxEx10";
			this.TextBoxEx10.Size = new System.Drawing.Size(570, 21);
			this.TextBoxEx10.TabIndex = 19;
			//
			//GroupBox8
			//
			this.GroupBox8.Controls.Add(this.DataGridView7);
			this.GroupBox8.Controls.Add(this.Button14);
			this.GroupBox8.Controls.Add(this.Button15);
			this.GroupBox8.Controls.Add(this.Button16);
			this.GroupBox8.Controls.Add(this.Button17);
			this.GroupBox8.Location = new System.Drawing.Point(6, 243);
			this.GroupBox8.Name = "GroupBox8";
			this.GroupBox8.Size = new System.Drawing.Size(311, 100);
			this.GroupBox8.TabIndex = 18;
			this.GroupBox8.TabStop = false;
			this.GroupBox8.Text = "Include Models";
			//
			//DataGridView7
			//
			this.DataGridView7.AllowUserToAddRows = false;
			this.DataGridView7.AllowUserToDeleteRows = false;
			this.DataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView7.Location = new System.Drawing.Point(6, 13);
			this.DataGridView7.Name = "DataGridView7";
			this.DataGridView7.Size = new System.Drawing.Size(240, 81);
			this.DataGridView7.TabIndex = 11;
			//
			//Button14
			//
			this.Button14.Location = new System.Drawing.Point(283, 71);
			this.Button14.Name = "Button14";
			this.Button14.Size = new System.Drawing.Size(25, 23);
			this.Button14.TabIndex = 16;
			this.Button14.Text = "Dn";
			this.Button14.UseVisualStyleBackColor = true;
			//
			//Button15
			//
			this.Button15.Location = new System.Drawing.Point(252, 13);
			this.Button15.Name = "Button15";
			this.Button15.Size = new System.Drawing.Size(56, 23);
			this.Button15.TabIndex = 13;
			this.Button15.Text = "Add";
			this.Button15.UseVisualStyleBackColor = true;
			//
			//Button16
			//
			this.Button16.Location = new System.Drawing.Point(252, 71);
			this.Button16.Name = "Button16";
			this.Button16.Size = new System.Drawing.Size(25, 23);
			this.Button16.TabIndex = 15;
			this.Button16.Text = "Up";
			this.Button16.UseVisualStyleBackColor = true;
			//
			//Button17
			//
			this.Button17.Location = new System.Drawing.Point(252, 42);
			this.Button17.Name = "Button17";
			this.Button17.Size = new System.Drawing.Size(56, 23);
			this.Button17.TabIndex = 14;
			this.Button17.Text = "Delete";
			this.Button17.UseVisualStyleBackColor = true;
			//
			//GroupBox9
			//
			this.GroupBox9.Controls.Add(this.DataGridView8);
			this.GroupBox9.Controls.Add(this.Button18);
			this.GroupBox9.Controls.Add(this.Button19);
			this.GroupBox9.Controls.Add(this.Button20);
			this.GroupBox9.Controls.Add(this.Button21);
			this.GroupBox9.Location = new System.Drawing.Point(6, 137);
			this.GroupBox9.Name = "GroupBox9";
			this.GroupBox9.Size = new System.Drawing.Size(311, 100);
			this.GroupBox9.TabIndex = 17;
			this.GroupBox9.TabStop = false;
			this.GroupBox9.Text = "CD Materials";
			//
			//DataGridView8
			//
			this.DataGridView8.AllowUserToAddRows = false;
			this.DataGridView8.AllowUserToDeleteRows = false;
			this.DataGridView8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridView8.Location = new System.Drawing.Point(6, 13);
			this.DataGridView8.Name = "DataGridView8";
			this.DataGridView8.Size = new System.Drawing.Size(240, 81);
			this.DataGridView8.TabIndex = 11;
			//
			//Button18
			//
			this.Button18.Location = new System.Drawing.Point(283, 71);
			this.Button18.Name = "Button18";
			this.Button18.Size = new System.Drawing.Size(25, 23);
			this.Button18.TabIndex = 16;
			this.Button18.Text = "Dn";
			this.Button18.UseVisualStyleBackColor = true;
			//
			//Button19
			//
			this.Button19.Location = new System.Drawing.Point(252, 13);
			this.Button19.Name = "Button19";
			this.Button19.Size = new System.Drawing.Size(56, 23);
			this.Button19.TabIndex = 13;
			this.Button19.Text = "Add";
			this.Button19.UseVisualStyleBackColor = true;
			//
			//Button20
			//
			this.Button20.Location = new System.Drawing.Point(252, 71);
			this.Button20.Name = "Button20";
			this.Button20.Size = new System.Drawing.Size(25, 23);
			this.Button20.TabIndex = 15;
			this.Button20.Text = "Up";
			this.Button20.UseVisualStyleBackColor = true;
			//
			//Button21
			//
			this.Button21.Location = new System.Drawing.Point(252, 42);
			this.Button21.Name = "Button21";
			this.Button21.Size = new System.Drawing.Size(56, 23);
			this.Button21.TabIndex = 14;
			this.Button21.Text = "Delete";
			this.Button21.UseVisualStyleBackColor = true;
			//
			//CheckBox4
			//
			this.CheckBox4.AutoSize = true;
			this.CheckBox4.Location = new System.Drawing.Point(515, 97);
			this.CheckBox4.Name = "CheckBox4";
			this.CheckBox4.Size = new System.Drawing.Size(98, 17);
			this.CheckBox4.TabIndex = 10;
			this.CheckBox4.Text = "Mostly Opaque";
			this.CheckBox4.UseVisualStyleBackColor = true;
			//
			//CheckBox5
			//
			this.CheckBox5.AutoSize = true;
			this.CheckBox5.Location = new System.Drawing.Point(434, 97);
			this.CheckBox5.Name = "CheckBox5";
			this.CheckBox5.Size = new System.Drawing.Size(64, 17);
			this.CheckBox5.TabIndex = 9;
			this.CheckBox5.Text = "Opaque";
			this.CheckBox5.UseVisualStyleBackColor = true;
			//
			//CheckBox6
			//
			this.CheckBox6.AutoSize = true;
			this.CheckBox6.Location = new System.Drawing.Point(320, 97);
			this.CheckBox6.Name = "CheckBox6";
			this.CheckBox6.Size = new System.Drawing.Size(95, 17);
			this.CheckBox6.TabIndex = 8;
			this.CheckBox6.Text = "Ambient Boost";
			this.CheckBox6.UseVisualStyleBackColor = true;
			//
			//Label12
			//
			this.Label12.AutoSize = true;
			this.Label12.Location = new System.Drawing.Point(268, 7);
			this.Label12.Name = "Label12";
			this.Label12.Size = new System.Drawing.Size(269, 13);
			this.Label12.TabIndex = 28;
			this.Label12.Text = "TODO: Disable Skip and Cancel buttons for single MDL.";
			//
			//PatchUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel2);
			this.Name = "PatchUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.Panel2.ResumeLayout(false);
			this.Panel2.PerformLayout();
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			this.SplitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
			this.SplitContainer1.ResumeLayout(false);
			this.PatchableValuesForSingleMDLGroupBox.ResumeLayout(false);
			this.PatchableValuesForSingleMDLGroupBox.PerformLayout();
			this.GroupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DataGridView4).EndInit();
			this.GroupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DataGridView3).EndInit();
			this.GroupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DataGridView2).EndInit();
			this.GroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DataGridView1).EndInit();
			this.PatchableValuesForMultipleMDLsGroupBox.ResumeLayout(false);
			this.PatchableValuesForMultipleMDLsGroupBox.PerformLayout();
			this.GroupBox6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DataGridView5).EndInit();
			this.GroupBox7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DataGridView6).EndInit();
			this.GroupBox8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DataGridView7).EndInit();
			this.GroupBox9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DataGridView8).EndInit();
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			BrowseForMdlFileButton.Click += new System.EventHandler(BrowseForMdlFileButton_Click);
			GotoMdlFileButton.Click += new System.EventHandler(GotoMdlFileButton_Click);
		}
		internal System.Windows.Forms.Button ViewButton;
		internal TextBoxEx MdlPathFileNameTextBox;
		internal System.Windows.Forms.Button BrowseForMdlFileButton;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Panel Panel2;
		internal System.Windows.Forms.Button GotoMdlFileButton;
		internal System.Windows.Forms.GroupBox PatchableValuesForSingleMDLGroupBox;
		internal Crowbar.TextBoxEx MessageTextBox;
		internal System.Windows.Forms.SplitContainer SplitContainer1;
		internal System.Windows.Forms.ComboBox DecompileComboBox;
		internal System.Windows.Forms.Button CancelDecompileButton;
		internal System.Windows.Forms.Button SkipCurrentModelButton;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label5;
		internal Crowbar.TextBoxEx TextBoxEx2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal Crowbar.TextBoxEx TextBoxEx1;
		internal System.Windows.Forms.CheckBox CheckBox3;
		internal System.Windows.Forms.CheckBox CheckBox2;
		internal System.Windows.Forms.CheckBox CheckBox1;
		internal System.Windows.Forms.Button Button4;
		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.DataGridView DataGridView1;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.DataGridView DataGridView2;
		internal System.Windows.Forms.Button Button5;
		internal System.Windows.Forms.Button Button6;
		internal System.Windows.Forms.Button Button7;
		internal System.Windows.Forms.Button Button8;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label7;
		internal Crowbar.TextBoxEx TextBoxEx3;
		internal System.Windows.Forms.GroupBox GroupBox4;
		internal System.Windows.Forms.DataGridView DataGridView3;
		internal System.Windows.Forms.Button Button9;
		internal System.Windows.Forms.Button Button11;
		internal System.Windows.Forms.Label Label9;
		internal Crowbar.TextBoxEx TextBoxEx6;
		internal Crowbar.TextBoxEx TextBoxEx5;
		internal Crowbar.TextBoxEx TextBoxEx4;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.GroupBox GroupBox5;
		internal System.Windows.Forms.DataGridView DataGridView4;
		internal Label Label11;
		internal Label Label10;
		internal Button RefreshOrRevertButton;
		internal GroupBox PatchableValuesForMultipleMDLsGroupBox;
		internal Button Button10;
		internal GroupBox GroupBox6;
		internal DataGridView DataGridView5;
		internal Label Label14;
		internal TextBoxEx TextBoxEx7;
		internal TextBoxEx TextBoxEx8;
		internal TextBoxEx TextBoxEx9;
		internal Label Label15;
		internal GroupBox GroupBox7;
		internal DataGridView DataGridView6;
		internal Button Button12;
		internal Button Button13;
		internal Label Label16;
		internal Label Label17;
		internal TextBoxEx TextBoxEx10;
		internal GroupBox GroupBox8;
		internal DataGridView DataGridView7;
		internal Button Button14;
		internal Button Button15;
		internal Button Button16;
		internal Button Button17;
		internal GroupBox GroupBox9;
		internal DataGridView DataGridView8;
		internal Button Button18;
		internal Button Button19;
		internal Button Button20;
		internal Button Button21;
		internal CheckBox CheckBox4;
		internal CheckBox CheckBox5;
		internal CheckBox CheckBox6;
		internal Label Label12;
	}

}