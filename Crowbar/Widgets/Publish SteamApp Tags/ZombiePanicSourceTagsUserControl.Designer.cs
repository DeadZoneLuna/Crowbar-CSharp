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
			SurvivalCheckBox = new CheckBoxEx();
			ObjectiveCheckBox = new CheckBoxEx();
			HardcoreCheckBox = new CheckBoxEx();
			CustomCheckBox = new CheckBoxEx();
			GameModeRadioButton = new System.Windows.Forms.RadioButton();
			GUIsCheckBox = new CheckBoxEx();
			WeaponsCheckBox = new CheckBoxEx();
			PropsCheckBox = new CheckBoxEx();
			CharactersCheckBox = new CheckBoxEx();
			CustomModelsRadioButton = new System.Windows.Forms.RadioButton();
			SoundPackCheckBox = new CheckBoxEx();
			ModelPackCheckBox = new CheckBoxEx();
			WeaponSoundsCheckBox = new CheckBoxEx();
			CharacterSoundsCheckBox = new CheckBoxEx();
			CustomSoundsRadioButton = new System.Windows.Forms.RadioButton();
			MiscellaneousRadioButton = new System.Windows.Forms.RadioButton();
			GameModePanel = new System.Windows.Forms.Panel();
			CustomModelsPanel = new System.Windows.Forms.Panel();
			CustomSoundsPanel = new System.Windows.Forms.Panel();
			MiscellaneousPanel = new System.Windows.Forms.Panel();
			GameModePanel.SuspendLayout();
			CustomModelsPanel.SuspendLayout();
			CustomSoundsPanel.SuspendLayout();
			MiscellaneousPanel.SuspendLayout();
			SuspendLayout();
			//
			//SurvivalCheckBox
			//
			SurvivalCheckBox.AutoSize = true;
			SurvivalCheckBox.Location = new System.Drawing.Point(0, 0);
			SurvivalCheckBox.Name = "SurvivalCheckBox";
			SurvivalCheckBox.Size = new System.Drawing.Size(64, 17);
			SurvivalCheckBox.TabIndex = 0;
			SurvivalCheckBox.Tag = "Survival";
			SurvivalCheckBox.Text = "Survival";
			SurvivalCheckBox.UseVisualStyleBackColor = true;
			//
			//ObjectiveCheckBox
			//
			ObjectiveCheckBox.AutoSize = true;
			ObjectiveCheckBox.Location = new System.Drawing.Point(0, 23);
			ObjectiveCheckBox.Name = "ObjectiveCheckBox";
			ObjectiveCheckBox.Size = new System.Drawing.Size(72, 17);
			ObjectiveCheckBox.TabIndex = 1;
			ObjectiveCheckBox.Tag = "Objective";
			ObjectiveCheckBox.Text = "Objective";
			ObjectiveCheckBox.UseVisualStyleBackColor = true;
			//
			//HardcoreCheckBox
			//
			HardcoreCheckBox.AutoSize = true;
			HardcoreCheckBox.Location = new System.Drawing.Point(0, 46);
			HardcoreCheckBox.Name = "HardcoreCheckBox";
			HardcoreCheckBox.Size = new System.Drawing.Size(70, 17);
			HardcoreCheckBox.TabIndex = 2;
			HardcoreCheckBox.Tag = "Hardcore";
			HardcoreCheckBox.Text = "Hardcore";
			HardcoreCheckBox.UseVisualStyleBackColor = true;
			//
			//CustomCheckBox
			//
			CustomCheckBox.AutoSize = true;
			CustomCheckBox.Location = new System.Drawing.Point(0, 69);
			CustomCheckBox.Name = "CustomCheckBox";
			CustomCheckBox.Size = new System.Drawing.Size(62, 17);
			CustomCheckBox.TabIndex = 3;
			CustomCheckBox.Tag = "Custom";
			CustomCheckBox.Text = "Custom";
			CustomCheckBox.UseVisualStyleBackColor = true;
			//
			//GameModeRadioButton
			//
			GameModeRadioButton.AutoSize = true;
			GameModeRadioButton.Checked = true;
			GameModeRadioButton.Location = new System.Drawing.Point(3, 3);
			GameModeRadioButton.Name = "GameModeRadioButton";
			GameModeRadioButton.Size = new System.Drawing.Size(85, 17);
			GameModeRadioButton.TabIndex = 4;
			GameModeRadioButton.TabStop = true;
			GameModeRadioButton.Text = "Game Mode:";
			GameModeRadioButton.UseVisualStyleBackColor = true;
			//
			//GUIsCheckBox
			//
			GUIsCheckBox.AutoSize = true;
			GUIsCheckBox.Location = new System.Drawing.Point(0, 0);
			GUIsCheckBox.Name = "GUIsCheckBox";
			GUIsCheckBox.Size = new System.Drawing.Size(49, 17);
			GUIsCheckBox.TabIndex = 7;
			GUIsCheckBox.Tag = "GUIs";
			GUIsCheckBox.Text = "GUIs";
			GUIsCheckBox.UseVisualStyleBackColor = true;
			//
			//WeaponsCheckBox
			//
			WeaponsCheckBox.AutoSize = true;
			WeaponsCheckBox.Location = new System.Drawing.Point(0, 46);
			WeaponsCheckBox.Name = "WeaponsCheckBox";
			WeaponsCheckBox.Size = new System.Drawing.Size(71, 17);
			WeaponsCheckBox.TabIndex = 6;
			WeaponsCheckBox.Tag = "Weapons";
			WeaponsCheckBox.Text = "Weapons";
			WeaponsCheckBox.UseVisualStyleBackColor = true;
			//
			//PropsCheckBox
			//
			PropsCheckBox.AutoSize = true;
			PropsCheckBox.Location = new System.Drawing.Point(0, 23);
			PropsCheckBox.Name = "PropsCheckBox";
			PropsCheckBox.Size = new System.Drawing.Size(53, 17);
			PropsCheckBox.TabIndex = 5;
			PropsCheckBox.Tag = "Props";
			PropsCheckBox.Text = "Props";
			PropsCheckBox.UseVisualStyleBackColor = true;
			//
			//CharactersCheckBox
			//
			CharactersCheckBox.AutoSize = true;
			CharactersCheckBox.Location = new System.Drawing.Point(0, 0);
			CharactersCheckBox.Name = "CharactersCheckBox";
			CharactersCheckBox.Size = new System.Drawing.Size(79, 17);
			CharactersCheckBox.TabIndex = 4;
			CharactersCheckBox.Tag = "Characters";
			CharactersCheckBox.Text = "Characters";
			CharactersCheckBox.UseVisualStyleBackColor = true;
			//
			//CustomModelsRadioButton
			//
			CustomModelsRadioButton.AutoSize = true;
			CustomModelsRadioButton.Location = new System.Drawing.Point(3, 118);
			CustomModelsRadioButton.Name = "CustomModelsRadioButton";
			CustomModelsRadioButton.Size = new System.Drawing.Size(101, 17);
			CustomModelsRadioButton.TabIndex = 7;
			CustomModelsRadioButton.Text = "Custom Models:";
			CustomModelsRadioButton.UseVisualStyleBackColor = true;
			//
			//SoundPackCheckBox
			//
			SoundPackCheckBox.AutoSize = true;
			SoundPackCheckBox.Location = new System.Drawing.Point(1, 46);
			SoundPackCheckBox.Name = "SoundPackCheckBox";
			SoundPackCheckBox.Size = new System.Drawing.Size(81, 17);
			SoundPackCheckBox.TabIndex = 11;
			SoundPackCheckBox.Tag = "Sound Pack";
			SoundPackCheckBox.Text = "Sound Pack";
			SoundPackCheckBox.UseVisualStyleBackColor = true;
			//
			//ModelPackCheckBox
			//
			ModelPackCheckBox.AutoSize = true;
			ModelPackCheckBox.Location = new System.Drawing.Point(1, 23);
			ModelPackCheckBox.Name = "ModelPackCheckBox";
			ModelPackCheckBox.Size = new System.Drawing.Size(79, 17);
			ModelPackCheckBox.TabIndex = 10;
			ModelPackCheckBox.Tag = "Model Pack";
			ModelPackCheckBox.Text = "Model Pack";
			ModelPackCheckBox.UseVisualStyleBackColor = true;
			//
			//WeaponSoundsCheckBox
			//
			WeaponSoundsCheckBox.AutoSize = true;
			WeaponSoundsCheckBox.Location = new System.Drawing.Point(0, 23);
			WeaponSoundsCheckBox.Name = "WeaponSoundsCheckBox";
			WeaponSoundsCheckBox.Size = new System.Drawing.Size(104, 17);
			WeaponSoundsCheckBox.TabIndex = 9;
			WeaponSoundsCheckBox.Tag = "Weapon Sounds";
			WeaponSoundsCheckBox.Text = "Weapon Sounds";
			WeaponSoundsCheckBox.UseVisualStyleBackColor = true;
			//
			//CharacterSoundsCheckBox
			//
			CharacterSoundsCheckBox.AutoSize = true;
			CharacterSoundsCheckBox.Location = new System.Drawing.Point(0, 0);
			CharacterSoundsCheckBox.Name = "CharacterSoundsCheckBox";
			CharacterSoundsCheckBox.Size = new System.Drawing.Size(112, 17);
			CharacterSoundsCheckBox.TabIndex = 8;
			CharacterSoundsCheckBox.Tag = "Characters Sounds";
			CharacterSoundsCheckBox.Text = "Character Sounds";
			CharacterSoundsCheckBox.UseVisualStyleBackColor = true;
			//
			//CustomSoundsRadioButton
			//
			CustomSoundsRadioButton.AutoSize = true;
			CustomSoundsRadioButton.Location = new System.Drawing.Point(3, 210);
			CustomSoundsRadioButton.Name = "CustomSoundsRadioButton";
			CustomSoundsRadioButton.Size = new System.Drawing.Size(103, 17);
			CustomSoundsRadioButton.TabIndex = 12;
			CustomSoundsRadioButton.Text = "Custom Sounds:";
			CustomSoundsRadioButton.UseVisualStyleBackColor = true;
			//
			//MiscellaneousRadioButton
			//
			MiscellaneousRadioButton.AutoSize = true;
			MiscellaneousRadioButton.Location = new System.Drawing.Point(3, 279);
			MiscellaneousRadioButton.Name = "MiscellaneousRadioButton";
			MiscellaneousRadioButton.Size = new System.Drawing.Size(94, 17);
			MiscellaneousRadioButton.TabIndex = 13;
			MiscellaneousRadioButton.Text = "Miscellaneous:";
			MiscellaneousRadioButton.UseVisualStyleBackColor = true;
			//
			//GameModePanel
			//
			GameModePanel.Controls.Add(SurvivalCheckBox);
			GameModePanel.Controls.Add(ObjectiveCheckBox);
			GameModePanel.Controls.Add(HardcoreCheckBox);
			GameModePanel.Controls.Add(CustomCheckBox);
			GameModePanel.Location = new System.Drawing.Point(22, 26);
			GameModePanel.Name = "GameModePanel";
			GameModePanel.Size = new System.Drawing.Size(150, 86);
			GameModePanel.TabIndex = 14;
			//
			//CustomModelsPanel
			//
			CustomModelsPanel.Controls.Add(CharactersCheckBox);
			CustomModelsPanel.Controls.Add(PropsCheckBox);
			CustomModelsPanel.Controls.Add(WeaponsCheckBox);
			CustomModelsPanel.Enabled = false;
			CustomModelsPanel.Location = new System.Drawing.Point(22, 141);
			CustomModelsPanel.Name = "CustomModelsPanel";
			CustomModelsPanel.Size = new System.Drawing.Size(150, 63);
			CustomModelsPanel.TabIndex = 0;
			//
			//CustomSoundsPanel
			//
			CustomSoundsPanel.Controls.Add(CharacterSoundsCheckBox);
			CustomSoundsPanel.Controls.Add(WeaponSoundsCheckBox);
			CustomSoundsPanel.Enabled = false;
			CustomSoundsPanel.Location = new System.Drawing.Point(22, 233);
			CustomSoundsPanel.Name = "CustomSoundsPanel";
			CustomSoundsPanel.Size = new System.Drawing.Size(150, 40);
			CustomSoundsPanel.TabIndex = 0;
			//
			//MiscellaneousPanel
			//
			MiscellaneousPanel.Controls.Add(GUIsCheckBox);
			MiscellaneousPanel.Controls.Add(ModelPackCheckBox);
			MiscellaneousPanel.Controls.Add(SoundPackCheckBox);
			MiscellaneousPanel.Enabled = false;
			MiscellaneousPanel.Location = new System.Drawing.Point(22, 302);
			MiscellaneousPanel.Name = "MiscellaneousPanel";
			MiscellaneousPanel.Size = new System.Drawing.Size(150, 63);
			MiscellaneousPanel.TabIndex = 0;
			//
			//ZombiePanicSourceTagsUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoScroll = true;
			Controls.Add(CustomModelsPanel);
			Controls.Add(CustomSoundsPanel);
			Controls.Add(MiscellaneousPanel);
			Controls.Add(GameModePanel);
			Controls.Add(MiscellaneousRadioButton);
			Controls.Add(CustomSoundsRadioButton);
			Controls.Add(CustomModelsRadioButton);
			Controls.Add(GameModeRadioButton);
			Name = "ZombiePanicSourceTagsUserControl";
			Size = new System.Drawing.Size(350, 481);
			GameModePanel.ResumeLayout(false);
			GameModePanel.PerformLayout();
			CustomModelsPanel.ResumeLayout(false);
			CustomModelsPanel.PerformLayout();
			CustomSoundsPanel.ResumeLayout(false);
			CustomSoundsPanel.PerformLayout();
			MiscellaneousPanel.ResumeLayout(false);
			MiscellaneousPanel.PerformLayout();
			ResumeLayout(false);
			PerformLayout();

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