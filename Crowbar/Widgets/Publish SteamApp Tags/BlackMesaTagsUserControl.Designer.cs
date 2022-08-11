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
			MapTagsGroupBox = new System.Windows.Forms.GroupBox();
			MultiplayerTagCheckBox = new Crowbar.CheckBoxEx();
			SingleplayerTagCheckBox = new Crowbar.CheckBoxEx();
			ModelTagsGroupBox = new System.Windows.Forms.GroupBox();
			WeaponTagCheckBox = new Crowbar.CheckBoxEx();
			NPCTagCheckBox = new Crowbar.CheckBoxEx();
			EnvironmentTagCheckBox = new Crowbar.CheckBoxEx();
			CreatureTagCheckBox = new Crowbar.CheckBoxEx();
			GroupBox1 = new System.Windows.Forms.GroupBox();
			ExtraTag1TextBox = new System.Windows.Forms.TextBox();
			ExtraTag2TextBox = new System.Windows.Forms.TextBox();
			ExtraTag3TextBox = new System.Windows.Forms.TextBox();
			ExtraTag4TextBox = new System.Windows.Forms.TextBox();
			ExtraTag5TextBox = new System.Windows.Forms.TextBox();
			Label5 = new System.Windows.Forms.Label();
			Label4 = new System.Windows.Forms.Label();
			Label3 = new System.Windows.Forms.Label();
			Label2 = new System.Windows.Forms.Label();
			Label1 = new System.Windows.Forms.Label();
			MapTagsGroupBox.SuspendLayout();
			ModelTagsGroupBox.SuspendLayout();
			GroupBox1.SuspendLayout();
			SuspendLayout();
			//
			//MapTagsGroupBox
			//
			MapTagsGroupBox.Controls.Add(MultiplayerTagCheckBox);
			MapTagsGroupBox.Controls.Add(SingleplayerTagCheckBox);
			MapTagsGroupBox.Location = new System.Drawing.Point(3, 0);
			MapTagsGroupBox.Name = "MapTagsGroupBox";
			MapTagsGroupBox.Size = new System.Drawing.Size(114, 77);
			MapTagsGroupBox.TabIndex = 0;
			MapTagsGroupBox.TabStop = false;
			MapTagsGroupBox.Text = "Map";
			//
			//MultiplayerTagCheckBox
			//
			MultiplayerTagCheckBox.AutoSize = true;
			MultiplayerTagCheckBox.IsReadOnly = false;
			MultiplayerTagCheckBox.Location = new System.Drawing.Point(6, 43);
			MultiplayerTagCheckBox.Name = "MultiplayerTagCheckBox";
			MultiplayerTagCheckBox.Size = new System.Drawing.Size(78, 17);
			MultiplayerTagCheckBox.TabIndex = 1;
			MultiplayerTagCheckBox.Tag = "Multiplayer";
			MultiplayerTagCheckBox.Text = "Multiplayer";
			MultiplayerTagCheckBox.UseVisualStyleBackColor = true;
			//
			//SingleplayerTagCheckBox
			//
			SingleplayerTagCheckBox.AutoSize = true;
			SingleplayerTagCheckBox.IsReadOnly = false;
			SingleplayerTagCheckBox.Location = new System.Drawing.Point(6, 20);
			SingleplayerTagCheckBox.Name = "SingleplayerTagCheckBox";
			SingleplayerTagCheckBox.Size = new System.Drawing.Size(84, 17);
			SingleplayerTagCheckBox.TabIndex = 0;
			SingleplayerTagCheckBox.Tag = "Singleplayer";
			SingleplayerTagCheckBox.Text = "Singleplayer";
			SingleplayerTagCheckBox.UseVisualStyleBackColor = true;
			//
			//ModelTagsGroupBox
			//
			ModelTagsGroupBox.Controls.Add(WeaponTagCheckBox);
			ModelTagsGroupBox.Controls.Add(NPCTagCheckBox);
			ModelTagsGroupBox.Controls.Add(EnvironmentTagCheckBox);
			ModelTagsGroupBox.Controls.Add(CreatureTagCheckBox);
			ModelTagsGroupBox.Location = new System.Drawing.Point(3, 83);
			ModelTagsGroupBox.Name = "ModelTagsGroupBox";
			ModelTagsGroupBox.Size = new System.Drawing.Size(114, 123);
			ModelTagsGroupBox.TabIndex = 1;
			ModelTagsGroupBox.TabStop = false;
			ModelTagsGroupBox.Text = "Model";
			//
			//WeaponTagCheckBox
			//
			WeaponTagCheckBox.AutoSize = true;
			WeaponTagCheckBox.IsReadOnly = false;
			WeaponTagCheckBox.Location = new System.Drawing.Point(6, 89);
			WeaponTagCheckBox.Name = "WeaponTagCheckBox";
			WeaponTagCheckBox.Size = new System.Drawing.Size(66, 17);
			WeaponTagCheckBox.TabIndex = 3;
			WeaponTagCheckBox.Tag = "Weapon";
			WeaponTagCheckBox.Text = "Weapon";
			WeaponTagCheckBox.UseVisualStyleBackColor = true;
			//
			//NPCTagCheckBox
			//
			NPCTagCheckBox.AutoSize = true;
			NPCTagCheckBox.IsReadOnly = false;
			NPCTagCheckBox.Location = new System.Drawing.Point(6, 66);
			NPCTagCheckBox.Name = "NPCTagCheckBox";
			NPCTagCheckBox.Size = new System.Drawing.Size(46, 17);
			NPCTagCheckBox.TabIndex = 2;
			NPCTagCheckBox.Tag = "NPC";
			NPCTagCheckBox.Text = "NPC";
			NPCTagCheckBox.UseVisualStyleBackColor = true;
			//
			//EnvironmentTagCheckBox
			//
			EnvironmentTagCheckBox.AutoSize = true;
			EnvironmentTagCheckBox.IsReadOnly = false;
			EnvironmentTagCheckBox.Location = new System.Drawing.Point(6, 43);
			EnvironmentTagCheckBox.Name = "EnvironmentTagCheckBox";
			EnvironmentTagCheckBox.Size = new System.Drawing.Size(86, 17);
			EnvironmentTagCheckBox.TabIndex = 1;
			EnvironmentTagCheckBox.Tag = "Environment";
			EnvironmentTagCheckBox.Text = "Environment";
			EnvironmentTagCheckBox.UseVisualStyleBackColor = true;
			//
			//CreatureTagCheckBox
			//
			CreatureTagCheckBox.AutoSize = true;
			CreatureTagCheckBox.IsReadOnly = false;
			CreatureTagCheckBox.Location = new System.Drawing.Point(6, 20);
			CreatureTagCheckBox.Name = "CreatureTagCheckBox";
			CreatureTagCheckBox.Size = new System.Drawing.Size(69, 17);
			CreatureTagCheckBox.TabIndex = 0;
			CreatureTagCheckBox.Tag = "Creature";
			CreatureTagCheckBox.Text = "Creature";
			CreatureTagCheckBox.UseVisualStyleBackColor = true;
			//
			//GroupBox1
			//
			GroupBox1.Controls.Add(ExtraTag1TextBox);
			GroupBox1.Controls.Add(ExtraTag2TextBox);
			GroupBox1.Controls.Add(ExtraTag3TextBox);
			GroupBox1.Controls.Add(ExtraTag4TextBox);
			GroupBox1.Controls.Add(ExtraTag5TextBox);
			GroupBox1.Controls.Add(Label5);
			GroupBox1.Controls.Add(Label4);
			GroupBox1.Controls.Add(Label3);
			GroupBox1.Controls.Add(Label2);
			GroupBox1.Controls.Add(Label1);
			GroupBox1.Location = new System.Drawing.Point(123, 0);
			GroupBox1.Name = "GroupBox1";
			GroupBox1.Size = new System.Drawing.Size(167, 206);
			GroupBox1.TabIndex = 1;
			GroupBox1.TabStop = false;
			GroupBox1.Text = "Extras";
			//
			//ExtraTag1TextBox
			//
			ExtraTag1TextBox.Location = new System.Drawing.Point(58, 20);
			ExtraTag1TextBox.Name = "ExtraTag1TextBox";
			ExtraTag1TextBox.Size = new System.Drawing.Size(100, 21);
			ExtraTag1TextBox.TabIndex = 1;
			ExtraTag1TextBox.Tag = "TagsEnabled";
			//
			//ExtraTag2TextBox
			//
			ExtraTag2TextBox.Location = new System.Drawing.Point(58, 47);
			ExtraTag2TextBox.Name = "ExtraTag2TextBox";
			ExtraTag2TextBox.Size = new System.Drawing.Size(100, 21);
			ExtraTag2TextBox.TabIndex = 3;
			ExtraTag2TextBox.Tag = "TagsEnabled";
			//
			//ExtraTag3TextBox
			//
			ExtraTag3TextBox.Location = new System.Drawing.Point(58, 74);
			ExtraTag3TextBox.Name = "ExtraTag3TextBox";
			ExtraTag3TextBox.Size = new System.Drawing.Size(100, 21);
			ExtraTag3TextBox.TabIndex = 5;
			ExtraTag3TextBox.Tag = "TagsEnabled";
			//
			//ExtraTag4TextBox
			//
			ExtraTag4TextBox.Location = new System.Drawing.Point(58, 101);
			ExtraTag4TextBox.Name = "ExtraTag4TextBox";
			ExtraTag4TextBox.Size = new System.Drawing.Size(100, 21);
			ExtraTag4TextBox.TabIndex = 7;
			ExtraTag4TextBox.Tag = "TagsEnabled";
			//
			//ExtraTag5TextBox
			//
			ExtraTag5TextBox.Location = new System.Drawing.Point(58, 128);
			ExtraTag5TextBox.Name = "ExtraTag5TextBox";
			ExtraTag5TextBox.Size = new System.Drawing.Size(100, 21);
			ExtraTag5TextBox.TabIndex = 9;
			ExtraTag5TextBox.Tag = "TagsEnabled";
			//
			//Label5
			//
			Label5.AutoSize = true;
			Label5.Location = new System.Drawing.Point(6, 131);
			Label5.Name = "Label5";
			Label5.Size = new System.Drawing.Size(46, 13);
			Label5.TabIndex = 8;
			Label5.Text = "Extra 5:";
			//
			//Label4
			//
			Label4.AutoSize = true;
			Label4.Location = new System.Drawing.Point(6, 104);
			Label4.Name = "Label4";
			Label4.Size = new System.Drawing.Size(46, 13);
			Label4.TabIndex = 6;
			Label4.Text = "Extra 4:";
			//
			//Label3
			//
			Label3.AutoSize = true;
			Label3.Location = new System.Drawing.Point(6, 77);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(46, 13);
			Label3.TabIndex = 4;
			Label3.Text = "Extra 3:";
			//
			//Label2
			//
			Label2.AutoSize = true;
			Label2.Location = new System.Drawing.Point(6, 50);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(46, 13);
			Label2.TabIndex = 2;
			Label2.Text = "Extra 2:";
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(6, 23);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(46, 13);
			Label1.TabIndex = 0;
			Label1.Text = "Extra 1:";
			//
			//BlackMesaTagsUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(GroupBox1);
			Controls.Add(ModelTagsGroupBox);
			Controls.Add(MapTagsGroupBox);
			Name = "BlackMesaTagsUserControl";
			Size = new System.Drawing.Size(318, 215);
			MapTagsGroupBox.ResumeLayout(false);
			MapTagsGroupBox.PerformLayout();
			ModelTagsGroupBox.ResumeLayout(false);
			ModelTagsGroupBox.PerformLayout();
			GroupBox1.ResumeLayout(false);
			GroupBox1.PerformLayout();
			ResumeLayout(false);

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