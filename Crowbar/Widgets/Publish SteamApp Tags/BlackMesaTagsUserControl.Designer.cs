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
	public partial class BlackMesaTagsUserControl : Base_TagsUserControl
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
			this.MapTagsGroupBox = new System.Windows.Forms.GroupBox();
			this.MultiplayerTagCheckBox = new Crowbar.CheckBoxEx();
			this.SingleplayerTagCheckBox = new Crowbar.CheckBoxEx();
			this.ModelTagsGroupBox = new System.Windows.Forms.GroupBox();
			this.WeaponTagCheckBox = new Crowbar.CheckBoxEx();
			this.NPCTagCheckBox = new Crowbar.CheckBoxEx();
			this.EnvironmentTagCheckBox = new Crowbar.CheckBoxEx();
			this.CreatureTagCheckBox = new Crowbar.CheckBoxEx();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.ExtraTag1TextBox = new System.Windows.Forms.TextBox();
			this.ExtraTag2TextBox = new System.Windows.Forms.TextBox();
			this.ExtraTag3TextBox = new System.Windows.Forms.TextBox();
			this.ExtraTag4TextBox = new System.Windows.Forms.TextBox();
			this.ExtraTag5TextBox = new System.Windows.Forms.TextBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.MapTagsGroupBox.SuspendLayout();
			this.ModelTagsGroupBox.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			//MapTagsGroupBox
			//
			this.MapTagsGroupBox.Controls.Add(this.MultiplayerTagCheckBox);
			this.MapTagsGroupBox.Controls.Add(this.SingleplayerTagCheckBox);
			this.MapTagsGroupBox.Location = new System.Drawing.Point(3, 0);
			this.MapTagsGroupBox.Name = "MapTagsGroupBox";
			this.MapTagsGroupBox.Size = new System.Drawing.Size(114, 77);
			this.MapTagsGroupBox.TabIndex = 0;
			this.MapTagsGroupBox.TabStop = false;
			this.MapTagsGroupBox.Text = "Map";
			//
			//MultiplayerTagCheckBox
			//
			this.MultiplayerTagCheckBox.AutoSize = true;
			this.MultiplayerTagCheckBox.IsReadOnly = false;
			this.MultiplayerTagCheckBox.Location = new System.Drawing.Point(6, 43);
			this.MultiplayerTagCheckBox.Name = "MultiplayerTagCheckBox";
			this.MultiplayerTagCheckBox.Size = new System.Drawing.Size(78, 17);
			this.MultiplayerTagCheckBox.TabIndex = 1;
			this.MultiplayerTagCheckBox.Tag = "Multiplayer";
			this.MultiplayerTagCheckBox.Text = "Multiplayer";
			this.MultiplayerTagCheckBox.UseVisualStyleBackColor = true;
			//
			//SingleplayerTagCheckBox
			//
			this.SingleplayerTagCheckBox.AutoSize = true;
			this.SingleplayerTagCheckBox.IsReadOnly = false;
			this.SingleplayerTagCheckBox.Location = new System.Drawing.Point(6, 20);
			this.SingleplayerTagCheckBox.Name = "SingleplayerTagCheckBox";
			this.SingleplayerTagCheckBox.Size = new System.Drawing.Size(84, 17);
			this.SingleplayerTagCheckBox.TabIndex = 0;
			this.SingleplayerTagCheckBox.Tag = "Singleplayer";
			this.SingleplayerTagCheckBox.Text = "Singleplayer";
			this.SingleplayerTagCheckBox.UseVisualStyleBackColor = true;
			//
			//ModelTagsGroupBox
			//
			this.ModelTagsGroupBox.Controls.Add(this.WeaponTagCheckBox);
			this.ModelTagsGroupBox.Controls.Add(this.NPCTagCheckBox);
			this.ModelTagsGroupBox.Controls.Add(this.EnvironmentTagCheckBox);
			this.ModelTagsGroupBox.Controls.Add(this.CreatureTagCheckBox);
			this.ModelTagsGroupBox.Location = new System.Drawing.Point(3, 83);
			this.ModelTagsGroupBox.Name = "ModelTagsGroupBox";
			this.ModelTagsGroupBox.Size = new System.Drawing.Size(114, 123);
			this.ModelTagsGroupBox.TabIndex = 1;
			this.ModelTagsGroupBox.TabStop = false;
			this.ModelTagsGroupBox.Text = "Model";
			//
			//WeaponTagCheckBox
			//
			this.WeaponTagCheckBox.AutoSize = true;
			this.WeaponTagCheckBox.IsReadOnly = false;
			this.WeaponTagCheckBox.Location = new System.Drawing.Point(6, 89);
			this.WeaponTagCheckBox.Name = "WeaponTagCheckBox";
			this.WeaponTagCheckBox.Size = new System.Drawing.Size(66, 17);
			this.WeaponTagCheckBox.TabIndex = 3;
			this.WeaponTagCheckBox.Tag = "Weapon";
			this.WeaponTagCheckBox.Text = "Weapon";
			this.WeaponTagCheckBox.UseVisualStyleBackColor = true;
			//
			//NPCTagCheckBox
			//
			this.NPCTagCheckBox.AutoSize = true;
			this.NPCTagCheckBox.IsReadOnly = false;
			this.NPCTagCheckBox.Location = new System.Drawing.Point(6, 66);
			this.NPCTagCheckBox.Name = "NPCTagCheckBox";
			this.NPCTagCheckBox.Size = new System.Drawing.Size(46, 17);
			this.NPCTagCheckBox.TabIndex = 2;
			this.NPCTagCheckBox.Tag = "NPC";
			this.NPCTagCheckBox.Text = "NPC";
			this.NPCTagCheckBox.UseVisualStyleBackColor = true;
			//
			//EnvironmentTagCheckBox
			//
			this.EnvironmentTagCheckBox.AutoSize = true;
			this.EnvironmentTagCheckBox.IsReadOnly = false;
			this.EnvironmentTagCheckBox.Location = new System.Drawing.Point(6, 43);
			this.EnvironmentTagCheckBox.Name = "EnvironmentTagCheckBox";
			this.EnvironmentTagCheckBox.Size = new System.Drawing.Size(86, 17);
			this.EnvironmentTagCheckBox.TabIndex = 1;
			this.EnvironmentTagCheckBox.Tag = "Environment";
			this.EnvironmentTagCheckBox.Text = "Environment";
			this.EnvironmentTagCheckBox.UseVisualStyleBackColor = true;
			//
			//CreatureTagCheckBox
			//
			this.CreatureTagCheckBox.AutoSize = true;
			this.CreatureTagCheckBox.IsReadOnly = false;
			this.CreatureTagCheckBox.Location = new System.Drawing.Point(6, 20);
			this.CreatureTagCheckBox.Name = "CreatureTagCheckBox";
			this.CreatureTagCheckBox.Size = new System.Drawing.Size(69, 17);
			this.CreatureTagCheckBox.TabIndex = 0;
			this.CreatureTagCheckBox.Tag = "Creature";
			this.CreatureTagCheckBox.Text = "Creature";
			this.CreatureTagCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.ExtraTag1TextBox);
			this.GroupBox1.Controls.Add(this.ExtraTag2TextBox);
			this.GroupBox1.Controls.Add(this.ExtraTag3TextBox);
			this.GroupBox1.Controls.Add(this.ExtraTag4TextBox);
			this.GroupBox1.Controls.Add(this.ExtraTag5TextBox);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.Label4);
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Controls.Add(this.Label2);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Location = new System.Drawing.Point(123, 0);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(167, 206);
			this.GroupBox1.TabIndex = 1;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Extras";
			//
			//ExtraTag1TextBox
			//
			this.ExtraTag1TextBox.Location = new System.Drawing.Point(58, 20);
			this.ExtraTag1TextBox.Name = "ExtraTag1TextBox";
			this.ExtraTag1TextBox.Size = new System.Drawing.Size(100, 21);
			this.ExtraTag1TextBox.TabIndex = 1;
			this.ExtraTag1TextBox.Tag = "TagsEnabled";
			//
			//ExtraTag2TextBox
			//
			this.ExtraTag2TextBox.Location = new System.Drawing.Point(58, 47);
			this.ExtraTag2TextBox.Name = "ExtraTag2TextBox";
			this.ExtraTag2TextBox.Size = new System.Drawing.Size(100, 21);
			this.ExtraTag2TextBox.TabIndex = 3;
			this.ExtraTag2TextBox.Tag = "TagsEnabled";
			//
			//ExtraTag3TextBox
			//
			this.ExtraTag3TextBox.Location = new System.Drawing.Point(58, 74);
			this.ExtraTag3TextBox.Name = "ExtraTag3TextBox";
			this.ExtraTag3TextBox.Size = new System.Drawing.Size(100, 21);
			this.ExtraTag3TextBox.TabIndex = 5;
			this.ExtraTag3TextBox.Tag = "TagsEnabled";
			//
			//ExtraTag4TextBox
			//
			this.ExtraTag4TextBox.Location = new System.Drawing.Point(58, 101);
			this.ExtraTag4TextBox.Name = "ExtraTag4TextBox";
			this.ExtraTag4TextBox.Size = new System.Drawing.Size(100, 21);
			this.ExtraTag4TextBox.TabIndex = 7;
			this.ExtraTag4TextBox.Tag = "TagsEnabled";
			//
			//ExtraTag5TextBox
			//
			this.ExtraTag5TextBox.Location = new System.Drawing.Point(58, 128);
			this.ExtraTag5TextBox.Name = "ExtraTag5TextBox";
			this.ExtraTag5TextBox.Size = new System.Drawing.Size(100, 21);
			this.ExtraTag5TextBox.TabIndex = 9;
			this.ExtraTag5TextBox.Tag = "TagsEnabled";
			//
			//Label5
			//
			this.Label5.AutoSize = true;
			this.Label5.Location = new System.Drawing.Point(6, 131);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(46, 13);
			this.Label5.TabIndex = 8;
			this.Label5.Text = "Extra 5:";
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(6, 104);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(46, 13);
			this.Label4.TabIndex = 6;
			this.Label4.Text = "Extra 4:";
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(6, 77);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(46, 13);
			this.Label3.TabIndex = 4;
			this.Label3.Text = "Extra 3:";
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(6, 50);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(46, 13);
			this.Label2.TabIndex = 2;
			this.Label2.Text = "Extra 2:";
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(6, 23);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(46, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Extra 1:";
			//
			//BlackMesaTagsUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.ModelTagsGroupBox);
			this.Controls.Add(this.MapTagsGroupBox);
			this.Name = "BlackMesaTagsUserControl";
			this.Size = new System.Drawing.Size(318, 215);
			this.MapTagsGroupBox.ResumeLayout(false);
			this.MapTagsGroupBox.PerformLayout();
			this.ModelTagsGroupBox.ResumeLayout(false);
			this.ModelTagsGroupBox.PerformLayout();
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		internal GroupBox MapTagsGroupBox;
		internal CheckBoxEx MultiplayerTagCheckBox;
		internal CheckBoxEx SingleplayerTagCheckBox;
		internal GroupBox ModelTagsGroupBox;
		internal CheckBoxEx WeaponTagCheckBox;
		internal CheckBoxEx NPCTagCheckBox;
		internal CheckBoxEx EnvironmentTagCheckBox;
		internal CheckBoxEx CreatureTagCheckBox;
		internal GroupBox GroupBox1;
		internal TextBox ExtraTag5TextBox;
		internal TextBox ExtraTag4TextBox;
		internal TextBox ExtraTag3TextBox;
		internal TextBox ExtraTag2TextBox;
		internal TextBox ExtraTag1TextBox;
		internal Label Label5;
		internal Label Label4;
		internal Label Label3;
		internal Label Label2;
		internal Label Label1;
	}

}