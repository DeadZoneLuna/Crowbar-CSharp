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
			ViewButton = new System.Windows.Forms.Button();
			MdlPathFileNameTextBox = new Crowbar.TextBoxEx();
			BrowseForMdlFileButton = new System.Windows.Forms.Button();
			Label1 = new System.Windows.Forms.Label();
			Panel2 = new System.Windows.Forms.Panel();
			DecompileComboBox = new System.Windows.Forms.ComboBox();
			GotoMdlFileButton = new System.Windows.Forms.Button();
			SplitContainer1 = new System.Windows.Forms.SplitContainer();
			PatchableValuesForSingleMDLGroupBox = new System.Windows.Forms.GroupBox();
			GroupBox5 = new System.Windows.Forms.GroupBox();
			DataGridView4 = new System.Windows.Forms.DataGridView();
			Label9 = new System.Windows.Forms.Label();
			TextBoxEx6 = new Crowbar.TextBoxEx();
			TextBoxEx5 = new Crowbar.TextBoxEx();
			TextBoxEx4 = new Crowbar.TextBoxEx();
			Label8 = new System.Windows.Forms.Label();
			GroupBox4 = new System.Windows.Forms.GroupBox();
			DataGridView3 = new System.Windows.Forms.DataGridView();
			Button9 = new System.Windows.Forms.Button();
			Button11 = new System.Windows.Forms.Button();
			Label6 = new System.Windows.Forms.Label();
			Label7 = new System.Windows.Forms.Label();
			TextBoxEx3 = new Crowbar.TextBoxEx();
			GroupBox3 = new System.Windows.Forms.GroupBox();
			DataGridView2 = new System.Windows.Forms.DataGridView();
			Button5 = new System.Windows.Forms.Button();
			Button6 = new System.Windows.Forms.Button();
			Button7 = new System.Windows.Forms.Button();
			Button8 = new System.Windows.Forms.Button();
			GroupBox2 = new System.Windows.Forms.GroupBox();
			DataGridView1 = new System.Windows.Forms.DataGridView();
			Button4 = new System.Windows.Forms.Button();
			Button1 = new System.Windows.Forms.Button();
			Button3 = new System.Windows.Forms.Button();
			Button2 = new System.Windows.Forms.Button();
			CheckBox3 = new System.Windows.Forms.CheckBox();
			CheckBox2 = new System.Windows.Forms.CheckBox();
			CheckBox1 = new System.Windows.Forms.CheckBox();
			Label4 = new System.Windows.Forms.Label();
			Label5 = new System.Windows.Forms.Label();
			TextBoxEx2 = new Crowbar.TextBoxEx();
			Label3 = new System.Windows.Forms.Label();
			Label2 = new System.Windows.Forms.Label();
			TextBoxEx1 = new Crowbar.TextBoxEx();
			CancelDecompileButton = new System.Windows.Forms.Button();
			SkipCurrentModelButton = new System.Windows.Forms.Button();
			MessageTextBox = new Crowbar.TextBoxEx();
			Label10 = new System.Windows.Forms.Label();
			Label11 = new System.Windows.Forms.Label();
			RefreshOrRevertButton = new System.Windows.Forms.Button();
			PatchableValuesForMultipleMDLsGroupBox = new System.Windows.Forms.GroupBox();
			Button10 = new System.Windows.Forms.Button();
			GroupBox6 = new System.Windows.Forms.GroupBox();
			DataGridView5 = new System.Windows.Forms.DataGridView();
			Label14 = new System.Windows.Forms.Label();
			TextBoxEx7 = new Crowbar.TextBoxEx();
			TextBoxEx8 = new Crowbar.TextBoxEx();
			TextBoxEx9 = new Crowbar.TextBoxEx();
			Label15 = new System.Windows.Forms.Label();
			GroupBox7 = new System.Windows.Forms.GroupBox();
			DataGridView6 = new System.Windows.Forms.DataGridView();
			Button12 = new System.Windows.Forms.Button();
			Button13 = new System.Windows.Forms.Button();
			Label16 = new System.Windows.Forms.Label();
			Label17 = new System.Windows.Forms.Label();
			TextBoxEx10 = new Crowbar.TextBoxEx();
			GroupBox8 = new System.Windows.Forms.GroupBox();
			DataGridView7 = new System.Windows.Forms.DataGridView();
			Button14 = new System.Windows.Forms.Button();
			Button15 = new System.Windows.Forms.Button();
			Button16 = new System.Windows.Forms.Button();
			Button17 = new System.Windows.Forms.Button();
			GroupBox9 = new System.Windows.Forms.GroupBox();
			DataGridView8 = new System.Windows.Forms.DataGridView();
			Button18 = new System.Windows.Forms.Button();
			Button19 = new System.Windows.Forms.Button();
			Button20 = new System.Windows.Forms.Button();
			Button21 = new System.Windows.Forms.Button();
			CheckBox4 = new System.Windows.Forms.CheckBox();
			CheckBox5 = new System.Windows.Forms.CheckBox();
			CheckBox6 = new System.Windows.Forms.CheckBox();
			Label12 = new System.Windows.Forms.Label();
			Panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)SplitContainer1).BeginInit();
			SplitContainer1.Panel1.SuspendLayout();
			SplitContainer1.Panel2.SuspendLayout();
			SplitContainer1.SuspendLayout();
			PatchableValuesForSingleMDLGroupBox.SuspendLayout();
			GroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView4).BeginInit();
			GroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView3).BeginInit();
			GroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView2).BeginInit();
			GroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView1).BeginInit();
			PatchableValuesForMultipleMDLsGroupBox.SuspendLayout();
			GroupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView5).BeginInit();
			GroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView6).BeginInit();
			GroupBox8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView7).BeginInit();
			GroupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView8).BeginInit();
			SuspendLayout();
			//
			//ViewButton
			//
			ViewButton.Enabled = false;
			ViewButton.Location = new System.Drawing.Point(0, 2);
			ViewButton.Name = "ViewButton";
			ViewButton.Size = new System.Drawing.Size(50, 23);
			ViewButton.TabIndex = 8;
			ViewButton.Text = "Patch";
			ViewButton.UseVisualStyleBackColor = true;
			//
			//MdlPathFileNameTextBox
			//
			MdlPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			MdlPathFileNameTextBox.Location = new System.Drawing.Point(209, 5);
			MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox";
			MdlPathFileNameTextBox.Size = new System.Drawing.Size(441, 21);
			MdlPathFileNameTextBox.TabIndex = 1;
			//
			//BrowseForMdlFileButton
			//
			BrowseForMdlFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForMdlFileButton.Location = new System.Drawing.Point(660, 3);
			BrowseForMdlFileButton.Name = "BrowseForMdlFileButton";
			BrowseForMdlFileButton.Size = new System.Drawing.Size(64, 23);
			BrowseForMdlFileButton.TabIndex = 2;
			BrowseForMdlFileButton.Text = "Browse...";
			BrowseForMdlFileButton.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(3, 8);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(58, 13);
			Label1.TabIndex = 0;
			Label1.Text = "MDL input:";
			//
			//Panel2
			//
			Panel2.Controls.Add(DecompileComboBox);
			Panel2.Controls.Add(Label1);
			Panel2.Controls.Add(MdlPathFileNameTextBox);
			Panel2.Controls.Add(BrowseForMdlFileButton);
			Panel2.Controls.Add(GotoMdlFileButton);
			Panel2.Controls.Add(SplitContainer1);
			Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel2.Location = new System.Drawing.Point(0, 0);
			Panel2.Margin = new System.Windows.Forms.Padding(2);
			Panel2.Name = "Panel2";
			Panel2.Size = new System.Drawing.Size(776, 536);
			Panel2.TabIndex = 8;
			//
			//DecompileComboBox
			//
			DecompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			DecompileComboBox.FormattingEnabled = true;
			DecompileComboBox.Location = new System.Drawing.Point(63, 4);
			DecompileComboBox.Name = "DecompileComboBox";
			DecompileComboBox.Size = new System.Drawing.Size(140, 21);
			DecompileComboBox.TabIndex = 14;
			//
			//GotoMdlFileButton
			//
			GotoMdlFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoMdlFileButton.Location = new System.Drawing.Point(730, 3);
			GotoMdlFileButton.Name = "GotoMdlFileButton";
			GotoMdlFileButton.Size = new System.Drawing.Size(43, 23);
			GotoMdlFileButton.TabIndex = 3;
			GotoMdlFileButton.Text = "Goto";
			GotoMdlFileButton.UseVisualStyleBackColor = true;
			//
			//SplitContainer1
			//
			SplitContainer1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			SplitContainer1.Location = new System.Drawing.Point(3, 32);
			SplitContainer1.Name = "SplitContainer1";
			SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//SplitContainer1.Panel1
			//
			SplitContainer1.Panel1.Controls.Add(PatchableValuesForSingleMDLGroupBox);
			SplitContainer1.Panel1.Controls.Add(PatchableValuesForMultipleMDLsGroupBox);
			SplitContainer1.Panel1MinSize = 45;
			//
			//SplitContainer1.Panel2
			//
			SplitContainer1.Panel2.Controls.Add(Label12);
			SplitContainer1.Panel2.Controls.Add(CancelDecompileButton);
			SplitContainer1.Panel2.Controls.Add(SkipCurrentModelButton);
			SplitContainer1.Panel2.Controls.Add(ViewButton);
			SplitContainer1.Panel2.Controls.Add(MessageTextBox);
			SplitContainer1.Panel2MinSize = 45;
			SplitContainer1.Size = new System.Drawing.Size(770, 501);
			SplitContainer1.SplitterDistance = 384;
			SplitContainer1.TabIndex = 13;
			//
			//PatchableValuesForSingleMDLGroupBox
			//
			PatchableValuesForSingleMDLGroupBox.Controls.Add(RefreshOrRevertButton);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label11);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label10);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(GroupBox5);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label9);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(TextBoxEx6);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(TextBoxEx5);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(TextBoxEx4);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label8);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(GroupBox4);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label6);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label7);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(TextBoxEx3);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(GroupBox3);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(GroupBox2);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(CheckBox3);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(CheckBox2);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(CheckBox1);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label4);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label5);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(TextBoxEx2);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label3);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(Label2);
			PatchableValuesForSingleMDLGroupBox.Controls.Add(TextBoxEx1);
			PatchableValuesForSingleMDLGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			PatchableValuesForSingleMDLGroupBox.Location = new System.Drawing.Point(0, 0);
			PatchableValuesForSingleMDLGroupBox.Name = "PatchableValuesForSingleMDLGroupBox";
			PatchableValuesForSingleMDLGroupBox.Size = new System.Drawing.Size(770, 384);
			PatchableValuesForSingleMDLGroupBox.TabIndex = 4;
			PatchableValuesForSingleMDLGroupBox.TabStop = false;
			PatchableValuesForSingleMDLGroupBox.Text = "Patchable Values (for single MDL)";
			//
			//GroupBox5
			//
			GroupBox5.Controls.Add(DataGridView4);
			GroupBox5.Location = new System.Drawing.Point(323, 137);
			GroupBox5.Name = "GroupBox5";
			GroupBox5.Size = new System.Drawing.Size(280, 100);
			GroupBox5.TabIndex = 19;
			GroupBox5.TabStop = false;
			GroupBox5.Text = "Hitboxes";
			//
			//DataGridView4
			//
			DataGridView4.AllowUserToAddRows = false;
			DataGridView4.AllowUserToDeleteRows = false;
			DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView4.Location = new System.Drawing.Point(6, 13);
			DataGridView4.Name = "DataGridView4";
			DataGridView4.Size = new System.Drawing.Size(271, 81);
			DataGridView4.TabIndex = 11;
			//
			//Label9
			//
			Label9.AutoSize = true;
			Label9.Location = new System.Drawing.Point(223, 98);
			Label9.Name = "Label9";
			Label9.Size = new System.Drawing.Size(39, 13);
			Label9.TabIndex = 26;
			Label9.Text = "(X Y Z)";
			//
			//TextBoxEx6
			//
			TextBoxEx6.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx6.Location = new System.Drawing.Point(177, 95);
			TextBoxEx6.Name = "TextBoxEx6";
			TextBoxEx6.Size = new System.Drawing.Size(40, 21);
			TextBoxEx6.TabIndex = 25;
			//
			//TextBoxEx5
			//
			TextBoxEx5.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx5.Location = new System.Drawing.Point(131, 95);
			TextBoxEx5.Name = "TextBoxEx5";
			TextBoxEx5.Size = new System.Drawing.Size(40, 21);
			TextBoxEx5.TabIndex = 24;
			//
			//TextBoxEx4
			//
			TextBoxEx4.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx4.Location = new System.Drawing.Point(85, 95);
			TextBoxEx4.Name = "TextBoxEx4";
			TextBoxEx4.Size = new System.Drawing.Size(40, 21);
			TextBoxEx4.TabIndex = 23;
			//
			//Label8
			//
			Label8.AutoSize = true;
			Label8.Location = new System.Drawing.Point(6, 98);
			Label8.Name = "Label8";
			Label8.Size = new System.Drawing.Size(73, 13);
			Label8.TabIndex = 22;
			Label8.Text = "Illum Position:";
			//
			//GroupBox4
			//
			GroupBox4.Controls.Add(DataGridView3);
			GroupBox4.Controls.Add(Button9);
			GroupBox4.Controls.Add(Button11);
			GroupBox4.Location = new System.Drawing.Point(323, 243);
			GroupBox4.Name = "GroupBox4";
			GroupBox4.Size = new System.Drawing.Size(280, 100);
			GroupBox4.TabIndex = 18;
			GroupBox4.TabStop = false;
			GroupBox4.Text = "Body Group Names";
			//
			//DataGridView3
			//
			DataGridView3.AllowUserToAddRows = false;
			DataGridView3.AllowUserToDeleteRows = false;
			DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView3.Location = new System.Drawing.Point(6, 13);
			DataGridView3.Name = "DataGridView3";
			DataGridView3.Size = new System.Drawing.Size(240, 81);
			DataGridView3.TabIndex = 11;
			//
			//Button9
			//
			Button9.Location = new System.Drawing.Point(252, 42);
			Button9.Name = "Button9";
			Button9.Size = new System.Drawing.Size(25, 23);
			Button9.TabIndex = 16;
			Button9.Text = "Dn";
			Button9.UseVisualStyleBackColor = true;
			//
			//Button11
			//
			Button11.Location = new System.Drawing.Point(252, 13);
			Button11.Name = "Button11";
			Button11.Size = new System.Drawing.Size(25, 23);
			Button11.TabIndex = 15;
			Button11.Text = "Up";
			Button11.UseVisualStyleBackColor = true;
			//
			//Label6
			//
			Label6.AutoSize = true;
			Label6.Location = new System.Drawing.Point(698, 71);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(66, 13);
			Label6.TabIndex = 21;
			Label6.Text = "(any length)";
			//
			//Label7
			//
			Label7.AutoSize = true;
			Label7.Location = new System.Drawing.Point(6, 71);
			Label7.Name = "Label7";
			Label7.Size = new System.Drawing.Size(75, 13);
			Label7.TabIndex = 20;
			Label7.Text = "ANI file name:";
			//
			//TextBoxEx3
			//
			TextBoxEx3.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx3.Location = new System.Drawing.Point(122, 68);
			TextBoxEx3.Name = "TextBoxEx3";
			TextBoxEx3.Size = new System.Drawing.Size(570, 21);
			TextBoxEx3.TabIndex = 19;
			//
			//GroupBox3
			//
			GroupBox3.Controls.Add(DataGridView2);
			GroupBox3.Controls.Add(Button5);
			GroupBox3.Controls.Add(Button6);
			GroupBox3.Controls.Add(Button7);
			GroupBox3.Controls.Add(Button8);
			GroupBox3.Location = new System.Drawing.Point(6, 243);
			GroupBox3.Name = "GroupBox3";
			GroupBox3.Size = new System.Drawing.Size(311, 100);
			GroupBox3.TabIndex = 18;
			GroupBox3.TabStop = false;
			GroupBox3.Text = "Include Models";
			//
			//DataGridView2
			//
			DataGridView2.AllowUserToAddRows = false;
			DataGridView2.AllowUserToDeleteRows = false;
			DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView2.Location = new System.Drawing.Point(6, 13);
			DataGridView2.Name = "DataGridView2";
			DataGridView2.Size = new System.Drawing.Size(240, 81);
			DataGridView2.TabIndex = 11;
			//
			//Button5
			//
			Button5.Location = new System.Drawing.Point(283, 71);
			Button5.Name = "Button5";
			Button5.Size = new System.Drawing.Size(25, 23);
			Button5.TabIndex = 16;
			Button5.Text = "Dn";
			Button5.UseVisualStyleBackColor = true;
			//
			//Button6
			//
			Button6.Location = new System.Drawing.Point(252, 13);
			Button6.Name = "Button6";
			Button6.Size = new System.Drawing.Size(56, 23);
			Button6.TabIndex = 13;
			Button6.Text = "Add";
			Button6.UseVisualStyleBackColor = true;
			//
			//Button7
			//
			Button7.Location = new System.Drawing.Point(252, 71);
			Button7.Name = "Button7";
			Button7.Size = new System.Drawing.Size(25, 23);
			Button7.TabIndex = 15;
			Button7.Text = "Up";
			Button7.UseVisualStyleBackColor = true;
			//
			//Button8
			//
			Button8.Location = new System.Drawing.Point(252, 42);
			Button8.Name = "Button8";
			Button8.Size = new System.Drawing.Size(56, 23);
			Button8.TabIndex = 14;
			Button8.Text = "Delete";
			Button8.UseVisualStyleBackColor = true;
			//
			//GroupBox2
			//
			GroupBox2.Controls.Add(DataGridView1);
			GroupBox2.Controls.Add(Button4);
			GroupBox2.Controls.Add(Button1);
			GroupBox2.Controls.Add(Button3);
			GroupBox2.Controls.Add(Button2);
			GroupBox2.Location = new System.Drawing.Point(6, 137);
			GroupBox2.Name = "GroupBox2";
			GroupBox2.Size = new System.Drawing.Size(311, 100);
			GroupBox2.TabIndex = 17;
			GroupBox2.TabStop = false;
			GroupBox2.Text = "CD Materials";
			//
			//DataGridView1
			//
			DataGridView1.AllowUserToAddRows = false;
			DataGridView1.AllowUserToDeleteRows = false;
			DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView1.Location = new System.Drawing.Point(6, 13);
			DataGridView1.Name = "DataGridView1";
			DataGridView1.Size = new System.Drawing.Size(240, 81);
			DataGridView1.TabIndex = 11;
			//
			//Button4
			//
			Button4.Location = new System.Drawing.Point(283, 71);
			Button4.Name = "Button4";
			Button4.Size = new System.Drawing.Size(25, 23);
			Button4.TabIndex = 16;
			Button4.Text = "Dn";
			Button4.UseVisualStyleBackColor = true;
			//
			//Button1
			//
			Button1.Location = new System.Drawing.Point(252, 13);
			Button1.Name = "Button1";
			Button1.Size = new System.Drawing.Size(56, 23);
			Button1.TabIndex = 13;
			Button1.Text = "Add";
			Button1.UseVisualStyleBackColor = true;
			//
			//Button3
			//
			Button3.Location = new System.Drawing.Point(252, 71);
			Button3.Name = "Button3";
			Button3.Size = new System.Drawing.Size(25, 23);
			Button3.TabIndex = 15;
			Button3.Text = "Up";
			Button3.UseVisualStyleBackColor = true;
			//
			//Button2
			//
			Button2.Location = new System.Drawing.Point(252, 42);
			Button2.Name = "Button2";
			Button2.Size = new System.Drawing.Size(56, 23);
			Button2.TabIndex = 14;
			Button2.Text = "Delete";
			Button2.UseVisualStyleBackColor = true;
			//
			//CheckBox3
			//
			CheckBox3.AutoSize = true;
			CheckBox3.Location = new System.Drawing.Point(515, 97);
			CheckBox3.Name = "CheckBox3";
			CheckBox3.Size = new System.Drawing.Size(98, 17);
			CheckBox3.TabIndex = 10;
			CheckBox3.Text = "Mostly Opaque";
			CheckBox3.UseVisualStyleBackColor = true;
			//
			//CheckBox2
			//
			CheckBox2.AutoSize = true;
			CheckBox2.Location = new System.Drawing.Point(434, 97);
			CheckBox2.Name = "CheckBox2";
			CheckBox2.Size = new System.Drawing.Size(64, 17);
			CheckBox2.TabIndex = 9;
			CheckBox2.Text = "Opaque";
			CheckBox2.UseVisualStyleBackColor = true;
			//
			//CheckBox1
			//
			CheckBox1.AutoSize = true;
			CheckBox1.Location = new System.Drawing.Point(320, 97);
			CheckBox1.Name = "CheckBox1";
			CheckBox1.Size = new System.Drawing.Size(95, 17);
			CheckBox1.TabIndex = 8;
			CheckBox1.Text = "Ambient Boost";
			CheckBox1.UseVisualStyleBackColor = true;
			//
			//Label4
			//
			Label4.AutoSize = true;
			Label4.Location = new System.Drawing.Point(698, 44);
			Label4.Name = "Label4";
			Label4.Size = new System.Drawing.Size(66, 13);
			Label4.TabIndex = 7;
			Label4.Text = "(any length)";
			//
			//Label5
			//
			Label5.AutoSize = true;
			Label5.Location = new System.Drawing.Point(6, 44);
			Label5.Name = "Label5";
			Label5.Size = new System.Drawing.Size(110, 13);
			Label5.TabIndex = 6;
			Label5.Text = "Internal MDL name 2:";
			//
			//TextBoxEx2
			//
			TextBoxEx2.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx2.Location = new System.Drawing.Point(122, 41);
			TextBoxEx2.Name = "TextBoxEx2";
			TextBoxEx2.Size = new System.Drawing.Size(570, 21);
			TextBoxEx2.TabIndex = 5;
			//
			//Label3
			//
			Label3.AutoSize = true;
			Label3.Location = new System.Drawing.Point(569, 17);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(56, 13);
			Label3.TabIndex = 4;
			Label3.Text = "(64 chars)";
			//
			//Label2
			//
			Label2.AutoSize = true;
			Label2.Location = new System.Drawing.Point(6, 17);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(101, 13);
			Label2.TabIndex = 3;
			Label2.Text = "Internal MDL name:";
			//
			//TextBoxEx1
			//
			TextBoxEx1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx1.Location = new System.Drawing.Point(122, 14);
			TextBoxEx1.Name = "TextBoxEx1";
			TextBoxEx1.Size = new System.Drawing.Size(441, 21);
			TextBoxEx1.TabIndex = 2;
			//
			//CancelDecompileButton
			//
			CancelDecompileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			CancelDecompileButton.Enabled = false;
			CancelDecompileButton.Location = new System.Drawing.Point(182, 2);
			CancelDecompileButton.Name = "CancelDecompileButton";
			CancelDecompileButton.Size = new System.Drawing.Size(80, 23);
			CancelDecompileButton.TabIndex = 14;
			CancelDecompileButton.Text = "Cancel Patch";
			CancelDecompileButton.UseVisualStyleBackColor = true;
			//
			//SkipCurrentModelButton
			//
			SkipCurrentModelButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			SkipCurrentModelButton.Enabled = false;
			SkipCurrentModelButton.Location = new System.Drawing.Point(56, 2);
			SkipCurrentModelButton.Name = "SkipCurrentModelButton";
			SkipCurrentModelButton.Size = new System.Drawing.Size(120, 23);
			SkipCurrentModelButton.TabIndex = 13;
			SkipCurrentModelButton.Text = "Skip Current Model";
			SkipCurrentModelButton.UseVisualStyleBackColor = true;
			//
			//MessageTextBox
			//
			MessageTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			MessageTextBox.Location = new System.Drawing.Point(0, 31);
			MessageTextBox.Multiline = true;
			MessageTextBox.Name = "MessageTextBox";
			MessageTextBox.ReadOnly = true;
			MessageTextBox.Size = new System.Drawing.Size(770, 81);
			MessageTextBox.TabIndex = 12;
			//
			//Label10
			//
			Label10.AutoSize = true;
			Label10.Location = new System.Drawing.Point(9, 357);
			Label10.Name = "Label10";
			Label10.Size = new System.Drawing.Size(212, 13);
			Label10.TabIndex = 27;
			Label10.Text = "TODO: Show this groupbox for single MDL.";
			//
			//Label11
			//
			Label11.AutoSize = true;
			Label11.Location = new System.Drawing.Point(227, 357);
			Label11.Name = "Label11";
			Label11.Size = new System.Drawing.Size(539, 13);
			Label11.TabIndex = 28;
			Label11.Text = "TODO: Show a different groupbox (already started) for multiple MDLs showing optio" + "ns such as Mostly Opaque.";
			//
			//RefreshOrRevertButton
			//
			RefreshOrRevertButton.Enabled = false;
			RefreshOrRevertButton.Location = new System.Drawing.Point(657, 256);
			RefreshOrRevertButton.Name = "RefreshOrRevertButton";
			RefreshOrRevertButton.Size = new System.Drawing.Size(92, 23);
			RefreshOrRevertButton.TabIndex = 15;
			RefreshOrRevertButton.Text = "Refresh/Revert";
			RefreshOrRevertButton.UseVisualStyleBackColor = true;
			//
			//PatchableValuesForMultipleMDLsGroupBox
			//
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(Button10);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(GroupBox6);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(Label14);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(TextBoxEx7);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(TextBoxEx8);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(TextBoxEx9);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(Label15);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(GroupBox7);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(Label16);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(Label17);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(TextBoxEx10);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(GroupBox8);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(GroupBox9);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(CheckBox4);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(CheckBox5);
			PatchableValuesForMultipleMDLsGroupBox.Controls.Add(CheckBox6);
			PatchableValuesForMultipleMDLsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			PatchableValuesForMultipleMDLsGroupBox.Location = new System.Drawing.Point(0, 0);
			PatchableValuesForMultipleMDLsGroupBox.Name = "PatchableValuesForMultipleMDLsGroupBox";
			PatchableValuesForMultipleMDLsGroupBox.Size = new System.Drawing.Size(770, 384);
			PatchableValuesForMultipleMDLsGroupBox.TabIndex = 29;
			PatchableValuesForMultipleMDLsGroupBox.TabStop = false;
			PatchableValuesForMultipleMDLsGroupBox.Text = "Patchable Values (for multiple MDLs)";
			//
			//Button10
			//
			Button10.Enabled = false;
			Button10.Location = new System.Drawing.Point(657, 256);
			Button10.Name = "Button10";
			Button10.Size = new System.Drawing.Size(92, 23);
			Button10.TabIndex = 15;
			Button10.Text = "Refresh/Revert";
			Button10.UseVisualStyleBackColor = true;
			//
			//GroupBox6
			//
			GroupBox6.Controls.Add(DataGridView5);
			GroupBox6.Location = new System.Drawing.Point(323, 137);
			GroupBox6.Name = "GroupBox6";
			GroupBox6.Size = new System.Drawing.Size(280, 100);
			GroupBox6.TabIndex = 19;
			GroupBox6.TabStop = false;
			GroupBox6.Text = "Hitboxes";
			//
			//DataGridView5
			//
			DataGridView5.AllowUserToAddRows = false;
			DataGridView5.AllowUserToDeleteRows = false;
			DataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView5.Location = new System.Drawing.Point(6, 13);
			DataGridView5.Name = "DataGridView5";
			DataGridView5.Size = new System.Drawing.Size(271, 81);
			DataGridView5.TabIndex = 11;
			//
			//Label14
			//
			Label14.AutoSize = true;
			Label14.Location = new System.Drawing.Point(223, 98);
			Label14.Name = "Label14";
			Label14.Size = new System.Drawing.Size(39, 13);
			Label14.TabIndex = 26;
			Label14.Text = "(X Y Z)";
			//
			//TextBoxEx7
			//
			TextBoxEx7.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx7.Location = new System.Drawing.Point(177, 95);
			TextBoxEx7.Name = "TextBoxEx7";
			TextBoxEx7.Size = new System.Drawing.Size(40, 21);
			TextBoxEx7.TabIndex = 25;
			//
			//TextBoxEx8
			//
			TextBoxEx8.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx8.Location = new System.Drawing.Point(131, 95);
			TextBoxEx8.Name = "TextBoxEx8";
			TextBoxEx8.Size = new System.Drawing.Size(40, 21);
			TextBoxEx8.TabIndex = 24;
			//
			//TextBoxEx9
			//
			TextBoxEx9.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx9.Location = new System.Drawing.Point(85, 95);
			TextBoxEx9.Name = "TextBoxEx9";
			TextBoxEx9.Size = new System.Drawing.Size(40, 21);
			TextBoxEx9.TabIndex = 23;
			//
			//Label15
			//
			Label15.AutoSize = true;
			Label15.Location = new System.Drawing.Point(6, 98);
			Label15.Name = "Label15";
			Label15.Size = new System.Drawing.Size(73, 13);
			Label15.TabIndex = 22;
			Label15.Text = "Illum Position:";
			//
			//GroupBox7
			//
			GroupBox7.Controls.Add(DataGridView6);
			GroupBox7.Controls.Add(Button12);
			GroupBox7.Controls.Add(Button13);
			GroupBox7.Location = new System.Drawing.Point(323, 243);
			GroupBox7.Name = "GroupBox7";
			GroupBox7.Size = new System.Drawing.Size(280, 100);
			GroupBox7.TabIndex = 18;
			GroupBox7.TabStop = false;
			GroupBox7.Text = "Body Group Names";
			//
			//DataGridView6
			//
			DataGridView6.AllowUserToAddRows = false;
			DataGridView6.AllowUserToDeleteRows = false;
			DataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView6.Location = new System.Drawing.Point(6, 13);
			DataGridView6.Name = "DataGridView6";
			DataGridView6.Size = new System.Drawing.Size(240, 81);
			DataGridView6.TabIndex = 11;
			//
			//Button12
			//
			Button12.Location = new System.Drawing.Point(252, 42);
			Button12.Name = "Button12";
			Button12.Size = new System.Drawing.Size(25, 23);
			Button12.TabIndex = 16;
			Button12.Text = "Dn";
			Button12.UseVisualStyleBackColor = true;
			//
			//Button13
			//
			Button13.Location = new System.Drawing.Point(252, 13);
			Button13.Name = "Button13";
			Button13.Size = new System.Drawing.Size(25, 23);
			Button13.TabIndex = 15;
			Button13.Text = "Up";
			Button13.UseVisualStyleBackColor = true;
			//
			//Label16
			//
			Label16.AutoSize = true;
			Label16.Location = new System.Drawing.Point(698, 71);
			Label16.Name = "Label16";
			Label16.Size = new System.Drawing.Size(66, 13);
			Label16.TabIndex = 21;
			Label16.Text = "(any length)";
			//
			//Label17
			//
			Label17.AutoSize = true;
			Label17.Location = new System.Drawing.Point(6, 71);
			Label17.Name = "Label17";
			Label17.Size = new System.Drawing.Size(75, 13);
			Label17.TabIndex = 20;
			Label17.Text = "ANI file name:";
			//
			//TextBoxEx10
			//
			TextBoxEx10.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBoxEx10.Location = new System.Drawing.Point(122, 68);
			TextBoxEx10.Name = "TextBoxEx10";
			TextBoxEx10.Size = new System.Drawing.Size(570, 21);
			TextBoxEx10.TabIndex = 19;
			//
			//GroupBox8
			//
			GroupBox8.Controls.Add(DataGridView7);
			GroupBox8.Controls.Add(Button14);
			GroupBox8.Controls.Add(Button15);
			GroupBox8.Controls.Add(Button16);
			GroupBox8.Controls.Add(Button17);
			GroupBox8.Location = new System.Drawing.Point(6, 243);
			GroupBox8.Name = "GroupBox8";
			GroupBox8.Size = new System.Drawing.Size(311, 100);
			GroupBox8.TabIndex = 18;
			GroupBox8.TabStop = false;
			GroupBox8.Text = "Include Models";
			//
			//DataGridView7
			//
			DataGridView7.AllowUserToAddRows = false;
			DataGridView7.AllowUserToDeleteRows = false;
			DataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView7.Location = new System.Drawing.Point(6, 13);
			DataGridView7.Name = "DataGridView7";
			DataGridView7.Size = new System.Drawing.Size(240, 81);
			DataGridView7.TabIndex = 11;
			//
			//Button14
			//
			Button14.Location = new System.Drawing.Point(283, 71);
			Button14.Name = "Button14";
			Button14.Size = new System.Drawing.Size(25, 23);
			Button14.TabIndex = 16;
			Button14.Text = "Dn";
			Button14.UseVisualStyleBackColor = true;
			//
			//Button15
			//
			Button15.Location = new System.Drawing.Point(252, 13);
			Button15.Name = "Button15";
			Button15.Size = new System.Drawing.Size(56, 23);
			Button15.TabIndex = 13;
			Button15.Text = "Add";
			Button15.UseVisualStyleBackColor = true;
			//
			//Button16
			//
			Button16.Location = new System.Drawing.Point(252, 71);
			Button16.Name = "Button16";
			Button16.Size = new System.Drawing.Size(25, 23);
			Button16.TabIndex = 15;
			Button16.Text = "Up";
			Button16.UseVisualStyleBackColor = true;
			//
			//Button17
			//
			Button17.Location = new System.Drawing.Point(252, 42);
			Button17.Name = "Button17";
			Button17.Size = new System.Drawing.Size(56, 23);
			Button17.TabIndex = 14;
			Button17.Text = "Delete";
			Button17.UseVisualStyleBackColor = true;
			//
			//GroupBox9
			//
			GroupBox9.Controls.Add(DataGridView8);
			GroupBox9.Controls.Add(Button18);
			GroupBox9.Controls.Add(Button19);
			GroupBox9.Controls.Add(Button20);
			GroupBox9.Controls.Add(Button21);
			GroupBox9.Location = new System.Drawing.Point(6, 137);
			GroupBox9.Name = "GroupBox9";
			GroupBox9.Size = new System.Drawing.Size(311, 100);
			GroupBox9.TabIndex = 17;
			GroupBox9.TabStop = false;
			GroupBox9.Text = "CD Materials";
			//
			//DataGridView8
			//
			DataGridView8.AllowUserToAddRows = false;
			DataGridView8.AllowUserToDeleteRows = false;
			DataGridView8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView8.Location = new System.Drawing.Point(6, 13);
			DataGridView8.Name = "DataGridView8";
			DataGridView8.Size = new System.Drawing.Size(240, 81);
			DataGridView8.TabIndex = 11;
			//
			//Button18
			//
			Button18.Location = new System.Drawing.Point(283, 71);
			Button18.Name = "Button18";
			Button18.Size = new System.Drawing.Size(25, 23);
			Button18.TabIndex = 16;
			Button18.Text = "Dn";
			Button18.UseVisualStyleBackColor = true;
			//
			//Button19
			//
			Button19.Location = new System.Drawing.Point(252, 13);
			Button19.Name = "Button19";
			Button19.Size = new System.Drawing.Size(56, 23);
			Button19.TabIndex = 13;
			Button19.Text = "Add";
			Button19.UseVisualStyleBackColor = true;
			//
			//Button20
			//
			Button20.Location = new System.Drawing.Point(252, 71);
			Button20.Name = "Button20";
			Button20.Size = new System.Drawing.Size(25, 23);
			Button20.TabIndex = 15;
			Button20.Text = "Up";
			Button20.UseVisualStyleBackColor = true;
			//
			//Button21
			//
			Button21.Location = new System.Drawing.Point(252, 42);
			Button21.Name = "Button21";
			Button21.Size = new System.Drawing.Size(56, 23);
			Button21.TabIndex = 14;
			Button21.Text = "Delete";
			Button21.UseVisualStyleBackColor = true;
			//
			//CheckBox4
			//
			CheckBox4.AutoSize = true;
			CheckBox4.Location = new System.Drawing.Point(515, 97);
			CheckBox4.Name = "CheckBox4";
			CheckBox4.Size = new System.Drawing.Size(98, 17);
			CheckBox4.TabIndex = 10;
			CheckBox4.Text = "Mostly Opaque";
			CheckBox4.UseVisualStyleBackColor = true;
			//
			//CheckBox5
			//
			CheckBox5.AutoSize = true;
			CheckBox5.Location = new System.Drawing.Point(434, 97);
			CheckBox5.Name = "CheckBox5";
			CheckBox5.Size = new System.Drawing.Size(64, 17);
			CheckBox5.TabIndex = 9;
			CheckBox5.Text = "Opaque";
			CheckBox5.UseVisualStyleBackColor = true;
			//
			//CheckBox6
			//
			CheckBox6.AutoSize = true;
			CheckBox6.Location = new System.Drawing.Point(320, 97);
			CheckBox6.Name = "CheckBox6";
			CheckBox6.Size = new System.Drawing.Size(95, 17);
			CheckBox6.TabIndex = 8;
			CheckBox6.Text = "Ambient Boost";
			CheckBox6.UseVisualStyleBackColor = true;
			//
			//Label12
			//
			Label12.AutoSize = true;
			Label12.Location = new System.Drawing.Point(268, 7);
			Label12.Name = "Label12";
			Label12.Size = new System.Drawing.Size(269, 13);
			Label12.TabIndex = 28;
			Label12.Text = "TODO: Disable Skip and Cancel buttons for single MDL.";
			//
			//PatchUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Panel2);
			Name = "PatchUserControl";
			Size = new System.Drawing.Size(776, 536);
			Panel2.ResumeLayout(false);
			Panel2.PerformLayout();
			SplitContainer1.Panel1.ResumeLayout(false);
			SplitContainer1.Panel2.ResumeLayout(false);
			SplitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)SplitContainer1).EndInit();
			SplitContainer1.ResumeLayout(false);
			PatchableValuesForSingleMDLGroupBox.ResumeLayout(false);
			PatchableValuesForSingleMDLGroupBox.PerformLayout();
			GroupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DataGridView4).EndInit();
			GroupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DataGridView3).EndInit();
			GroupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DataGridView2).EndInit();
			GroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DataGridView1).EndInit();
			PatchableValuesForMultipleMDLsGroupBox.ResumeLayout(false);
			PatchableValuesForMultipleMDLsGroupBox.PerformLayout();
			GroupBox6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DataGridView5).EndInit();
			GroupBox7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DataGridView6).EndInit();
			GroupBox8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DataGridView7).EndInit();
			GroupBox9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DataGridView8).EndInit();
			ResumeLayout(false);

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