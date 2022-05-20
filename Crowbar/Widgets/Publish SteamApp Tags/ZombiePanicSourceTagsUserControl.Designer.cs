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
	public partial class ZombiePanicSourceTagsUserControl : Base_TagsUserControl
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
			this.SurvivalCheckBox = new CheckBoxEx();
			this.ObjectiveCheckBox = new CheckBoxEx();
			this.HardcoreCheckBox = new CheckBoxEx();
			this.CustomCheckBox = new CheckBoxEx();
			this.GameModeRadioButton = new System.Windows.Forms.RadioButton();
			this.GUIsCheckBox = new CheckBoxEx();
			this.WeaponsCheckBox = new CheckBoxEx();
			this.PropsCheckBox = new CheckBoxEx();
			this.CharactersCheckBox = new CheckBoxEx();
			this.CustomModelsRadioButton = new System.Windows.Forms.RadioButton();
			this.SoundPackCheckBox = new CheckBoxEx();
			this.ModelPackCheckBox = new CheckBoxEx();
			this.WeaponSoundsCheckBox = new CheckBoxEx();
			this.CharacterSoundsCheckBox = new CheckBoxEx();
			this.CustomSoundsRadioButton = new System.Windows.Forms.RadioButton();
			this.MiscellaneousRadioButton = new System.Windows.Forms.RadioButton();
			this.GameModePanel = new System.Windows.Forms.Panel();
			this.CustomModelsPanel = new System.Windows.Forms.Panel();
			this.CustomSoundsPanel = new System.Windows.Forms.Panel();
			this.MiscellaneousPanel = new System.Windows.Forms.Panel();
			this.GameModePanel.SuspendLayout();
			this.CustomModelsPanel.SuspendLayout();
			this.CustomSoundsPanel.SuspendLayout();
			this.MiscellaneousPanel.SuspendLayout();
			this.SuspendLayout();
			//
			//SurvivalCheckBox
			//
			this.SurvivalCheckBox.AutoSize = true;
			this.SurvivalCheckBox.Location = new System.Drawing.Point(0, 0);
			this.SurvivalCheckBox.Name = "SurvivalCheckBox";
			this.SurvivalCheckBox.Size = new System.Drawing.Size(64, 17);
			this.SurvivalCheckBox.TabIndex = 0;
			this.SurvivalCheckBox.Tag = "Survival";
			this.SurvivalCheckBox.Text = "Survival";
			this.SurvivalCheckBox.UseVisualStyleBackColor = true;
			//
			//ObjectiveCheckBox
			//
			this.ObjectiveCheckBox.AutoSize = true;
			this.ObjectiveCheckBox.Location = new System.Drawing.Point(0, 23);
			this.ObjectiveCheckBox.Name = "ObjectiveCheckBox";
			this.ObjectiveCheckBox.Size = new System.Drawing.Size(72, 17);
			this.ObjectiveCheckBox.TabIndex = 1;
			this.ObjectiveCheckBox.Tag = "Objective";
			this.ObjectiveCheckBox.Text = "Objective";
			this.ObjectiveCheckBox.UseVisualStyleBackColor = true;
			//
			//HardcoreCheckBox
			//
			this.HardcoreCheckBox.AutoSize = true;
			this.HardcoreCheckBox.Location = new System.Drawing.Point(0, 46);
			this.HardcoreCheckBox.Name = "HardcoreCheckBox";
			this.HardcoreCheckBox.Size = new System.Drawing.Size(70, 17);
			this.HardcoreCheckBox.TabIndex = 2;
			this.HardcoreCheckBox.Tag = "Hardcore";
			this.HardcoreCheckBox.Text = "Hardcore";
			this.HardcoreCheckBox.UseVisualStyleBackColor = true;
			//
			//CustomCheckBox
			//
			this.CustomCheckBox.AutoSize = true;
			this.CustomCheckBox.Location = new System.Drawing.Point(0, 69);
			this.CustomCheckBox.Name = "CustomCheckBox";
			this.CustomCheckBox.Size = new System.Drawing.Size(62, 17);
			this.CustomCheckBox.TabIndex = 3;
			this.CustomCheckBox.Tag = "Custom";
			this.CustomCheckBox.Text = "Custom";
			this.CustomCheckBox.UseVisualStyleBackColor = true;
			//
			//GameModeRadioButton
			//
			this.GameModeRadioButton.AutoSize = true;
			this.GameModeRadioButton.Checked = true;
			this.GameModeRadioButton.Location = new System.Drawing.Point(3, 3);
			this.GameModeRadioButton.Name = "GameModeRadioButton";
			this.GameModeRadioButton.Size = new System.Drawing.Size(85, 17);
			this.GameModeRadioButton.TabIndex = 4;
			this.GameModeRadioButton.TabStop = true;
			this.GameModeRadioButton.Text = "Game Mode:";
			this.GameModeRadioButton.UseVisualStyleBackColor = true;
			//
			//GUIsCheckBox
			//
			this.GUIsCheckBox.AutoSize = true;
			this.GUIsCheckBox.Location = new System.Drawing.Point(0, 0);
			this.GUIsCheckBox.Name = "GUIsCheckBox";
			this.GUIsCheckBox.Size = new System.Drawing.Size(49, 17);
			this.GUIsCheckBox.TabIndex = 7;
			this.GUIsCheckBox.Tag = "GUIs";
			this.GUIsCheckBox.Text = "GUIs";
			this.GUIsCheckBox.UseVisualStyleBackColor = true;
			//
			//WeaponsCheckBox
			//
			this.WeaponsCheckBox.AutoSize = true;
			this.WeaponsCheckBox.Location = new System.Drawing.Point(0, 46);
			this.WeaponsCheckBox.Name = "WeaponsCheckBox";
			this.WeaponsCheckBox.Size = new System.Drawing.Size(71, 17);
			this.WeaponsCheckBox.TabIndex = 6;
			this.WeaponsCheckBox.Tag = "Weapons";
			this.WeaponsCheckBox.Text = "Weapons";
			this.WeaponsCheckBox.UseVisualStyleBackColor = true;
			//
			//PropsCheckBox
			//
			this.PropsCheckBox.AutoSize = true;
			this.PropsCheckBox.Location = new System.Drawing.Point(0, 23);
			this.PropsCheckBox.Name = "PropsCheckBox";
			this.PropsCheckBox.Size = new System.Drawing.Size(53, 17);
			this.PropsCheckBox.TabIndex = 5;
			this.PropsCheckBox.Tag = "Props";
			this.PropsCheckBox.Text = "Props";
			this.PropsCheckBox.UseVisualStyleBackColor = true;
			//
			//CharactersCheckBox
			//
			this.CharactersCheckBox.AutoSize = true;
			this.CharactersCheckBox.Location = new System.Drawing.Point(0, 0);
			this.CharactersCheckBox.Name = "CharactersCheckBox";
			this.CharactersCheckBox.Size = new System.Drawing.Size(79, 17);
			this.CharactersCheckBox.TabIndex = 4;
			this.CharactersCheckBox.Tag = "Characters";
			this.CharactersCheckBox.Text = "Characters";
			this.CharactersCheckBox.UseVisualStyleBackColor = true;
			//
			//CustomModelsRadioButton
			//
			this.CustomModelsRadioButton.AutoSize = true;
			this.CustomModelsRadioButton.Location = new System.Drawing.Point(3, 118);
			this.CustomModelsRadioButton.Name = "CustomModelsRadioButton";
			this.CustomModelsRadioButton.Size = new System.Drawing.Size(101, 17);
			this.CustomModelsRadioButton.TabIndex = 7;
			this.CustomModelsRadioButton.Text = "Custom Models:";
			this.CustomModelsRadioButton.UseVisualStyleBackColor = true;
			//
			//SoundPackCheckBox
			//
			this.SoundPackCheckBox.AutoSize = true;
			this.SoundPackCheckBox.Location = new System.Drawing.Point(1, 46);
			this.SoundPackCheckBox.Name = "SoundPackCheckBox";
			this.SoundPackCheckBox.Size = new System.Drawing.Size(81, 17);
			this.SoundPackCheckBox.TabIndex = 11;
			this.SoundPackCheckBox.Tag = "Sound Pack";
			this.SoundPackCheckBox.Text = "Sound Pack";
			this.SoundPackCheckBox.UseVisualStyleBackColor = true;
			//
			//ModelPackCheckBox
			//
			this.ModelPackCheckBox.AutoSize = true;
			this.ModelPackCheckBox.Location = new System.Drawing.Point(1, 23);
			this.ModelPackCheckBox.Name = "ModelPackCheckBox";
			this.ModelPackCheckBox.Size = new System.Drawing.Size(79, 17);
			this.ModelPackCheckBox.TabIndex = 10;
			this.ModelPackCheckBox.Tag = "Model Pack";
			this.ModelPackCheckBox.Text = "Model Pack";
			this.ModelPackCheckBox.UseVisualStyleBackColor = true;
			//
			//WeaponSoundsCheckBox
			//
			this.WeaponSoundsCheckBox.AutoSize = true;
			this.WeaponSoundsCheckBox.Location = new System.Drawing.Point(0, 23);
			this.WeaponSoundsCheckBox.Name = "WeaponSoundsCheckBox";
			this.WeaponSoundsCheckBox.Size = new System.Drawing.Size(104, 17);
			this.WeaponSoundsCheckBox.TabIndex = 9;
			this.WeaponSoundsCheckBox.Tag = "Weapon Sounds";
			this.WeaponSoundsCheckBox.Text = "Weapon Sounds";
			this.WeaponSoundsCheckBox.UseVisualStyleBackColor = true;
			//
			//CharacterSoundsCheckBox
			//
			this.CharacterSoundsCheckBox.AutoSize = true;
			this.CharacterSoundsCheckBox.Location = new System.Drawing.Point(0, 0);
			this.CharacterSoundsCheckBox.Name = "CharacterSoundsCheckBox";
			this.CharacterSoundsCheckBox.Size = new System.Drawing.Size(112, 17);
			this.CharacterSoundsCheckBox.TabIndex = 8;
			this.CharacterSoundsCheckBox.Tag = "Characters Sounds";
			this.CharacterSoundsCheckBox.Text = "Character Sounds";
			this.CharacterSoundsCheckBox.UseVisualStyleBackColor = true;
			//
			//CustomSoundsRadioButton
			//
			this.CustomSoundsRadioButton.AutoSize = true;
			this.CustomSoundsRadioButton.Location = new System.Drawing.Point(3, 210);
			this.CustomSoundsRadioButton.Name = "CustomSoundsRadioButton";
			this.CustomSoundsRadioButton.Size = new System.Drawing.Size(103, 17);
			this.CustomSoundsRadioButton.TabIndex = 12;
			this.CustomSoundsRadioButton.Text = "Custom Sounds:";
			this.CustomSoundsRadioButton.UseVisualStyleBackColor = true;
			//
			//MiscellaneousRadioButton
			//
			this.MiscellaneousRadioButton.AutoSize = true;
			this.MiscellaneousRadioButton.Location = new System.Drawing.Point(3, 279);
			this.MiscellaneousRadioButton.Name = "MiscellaneousRadioButton";
			this.MiscellaneousRadioButton.Size = new System.Drawing.Size(94, 17);
			this.MiscellaneousRadioButton.TabIndex = 13;
			this.MiscellaneousRadioButton.Text = "Miscellaneous:";
			this.MiscellaneousRadioButton.UseVisualStyleBackColor = true;
			//
			//GameModePanel
			//
			this.GameModePanel.Controls.Add(this.SurvivalCheckBox);
			this.GameModePanel.Controls.Add(this.ObjectiveCheckBox);
			this.GameModePanel.Controls.Add(this.HardcoreCheckBox);
			this.GameModePanel.Controls.Add(this.CustomCheckBox);
			this.GameModePanel.Location = new System.Drawing.Point(22, 26);
			this.GameModePanel.Name = "GameModePanel";
			this.GameModePanel.Size = new System.Drawing.Size(150, 86);
			this.GameModePanel.TabIndex = 14;
			//
			//CustomModelsPanel
			//
			this.CustomModelsPanel.Controls.Add(this.CharactersCheckBox);
			this.CustomModelsPanel.Controls.Add(this.PropsCheckBox);
			this.CustomModelsPanel.Controls.Add(this.WeaponsCheckBox);
			this.CustomModelsPanel.Enabled = false;
			this.CustomModelsPanel.Location = new System.Drawing.Point(22, 141);
			this.CustomModelsPanel.Name = "CustomModelsPanel";
			this.CustomModelsPanel.Size = new System.Drawing.Size(150, 63);
			this.CustomModelsPanel.TabIndex = 0;
			//
			//CustomSoundsPanel
			//
			this.CustomSoundsPanel.Controls.Add(this.CharacterSoundsCheckBox);
			this.CustomSoundsPanel.Controls.Add(this.WeaponSoundsCheckBox);
			this.CustomSoundsPanel.Enabled = false;
			this.CustomSoundsPanel.Location = new System.Drawing.Point(22, 233);
			this.CustomSoundsPanel.Name = "CustomSoundsPanel";
			this.CustomSoundsPanel.Size = new System.Drawing.Size(150, 40);
			this.CustomSoundsPanel.TabIndex = 0;
			//
			//MiscellaneousPanel
			//
			this.MiscellaneousPanel.Controls.Add(this.GUIsCheckBox);
			this.MiscellaneousPanel.Controls.Add(this.ModelPackCheckBox);
			this.MiscellaneousPanel.Controls.Add(this.SoundPackCheckBox);
			this.MiscellaneousPanel.Enabled = false;
			this.MiscellaneousPanel.Location = new System.Drawing.Point(22, 302);
			this.MiscellaneousPanel.Name = "MiscellaneousPanel";
			this.MiscellaneousPanel.Size = new System.Drawing.Size(150, 63);
			this.MiscellaneousPanel.TabIndex = 0;
			//
			//ZombiePanicSourceTagsUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.CustomModelsPanel);
			this.Controls.Add(this.CustomSoundsPanel);
			this.Controls.Add(this.MiscellaneousPanel);
			this.Controls.Add(this.GameModePanel);
			this.Controls.Add(this.MiscellaneousRadioButton);
			this.Controls.Add(this.CustomSoundsRadioButton);
			this.Controls.Add(this.CustomModelsRadioButton);
			this.Controls.Add(this.GameModeRadioButton);
			this.Name = "ZombiePanicSourceTagsUserControl";
			this.Size = new System.Drawing.Size(350, 481);
			this.GameModePanel.ResumeLayout(false);
			this.GameModePanel.PerformLayout();
			this.CustomModelsPanel.ResumeLayout(false);
			this.CustomModelsPanel.PerformLayout();
			this.CustomSoundsPanel.ResumeLayout(false);
			this.CustomSoundsPanel.PerformLayout();
			this.MiscellaneousPanel.ResumeLayout(false);
			this.MiscellaneousPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

//INSTANT C# NOTE: Converted design-time event handler wireups:
			GameModeRadioButton.CheckedChanged += new System.EventHandler(GameModeRadioButton_CheckedChanged);
			CustomModelsRadioButton.CheckedChanged += new System.EventHandler(CustomModelsRadioButton_CheckedChanged);
			CustomSoundsRadioButton.CheckedChanged += new System.EventHandler(CustomSoundsRadioButton_CheckedChanged);
			MiscellaneousRadioButton.CheckedChanged += new System.EventHandler(MiscellaneousRadioButton_CheckedChanged);
		}
		internal CheckBoxEx CustomCheckBox;
		internal CheckBoxEx HardcoreCheckBox;
		internal CheckBoxEx ObjectiveCheckBox;
		internal CheckBoxEx SurvivalCheckBox;
		internal RadioButton GameModeRadioButton;
		internal CheckBoxEx GUIsCheckBox;
		internal CheckBoxEx WeaponsCheckBox;
		internal CheckBoxEx PropsCheckBox;
		internal CheckBoxEx CharactersCheckBox;
		internal RadioButton CustomModelsRadioButton;
		internal CheckBoxEx SoundPackCheckBox;
		internal CheckBoxEx ModelPackCheckBox;
		internal CheckBoxEx WeaponSoundsCheckBox;
		internal CheckBoxEx CharacterSoundsCheckBox;
		internal RadioButton CustomSoundsRadioButton;
		internal RadioButton MiscellaneousRadioButton;
		internal Panel GameModePanel;
		internal Panel CustomModelsPanel;
		internal Panel CustomSoundsPanel;
		internal Panel MiscellaneousPanel;
	}

}