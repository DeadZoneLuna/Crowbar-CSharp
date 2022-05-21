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
	public partial class ContagionTagsUserControl : Base_TagsUserControl
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
			EscapeCoopCheckBox = new CheckBoxEx();
			ExtractionCoopCheckBox = new CheckBoxEx();
			HuntedPvpCheckBox = new CheckBoxEx();
			ContagionPanicClassicCheckBox = new CheckBoxEx();
			ContagionPanicObjectiveCheckBox = new CheckBoxEx();
			WeaponsModelTextureCheckBox = new CheckBoxEx();
			SurvivorsModelTextureCheckBox = new CheckBoxEx();
			ZombiesModelTextureCheckBox = new CheckBoxEx();
			UserInterfaceCheckBox = new CheckBoxEx();
			SoundsCheckBox = new CheckBoxEx();
			FlashlightCheckBox = new CheckBoxEx();
			SmartphoneWallpapersCheckBox = new CheckBoxEx();
			MiscCheckBox = new CheckBoxEx();
			FlatlineCheckBox = new CheckBoxEx();
			SuspendLayout();
			//
			//EscapeCoopCheckBox
			//
			EscapeCoopCheckBox.AutoSize = true;
			EscapeCoopCheckBox.Location = new System.Drawing.Point(3, 3);
			EscapeCoopCheckBox.Name = "EscapeCoopCheckBox";
			EscapeCoopCheckBox.Size = new System.Drawing.Size(100, 17);
			EscapeCoopCheckBox.TabIndex = 0;
			EscapeCoopCheckBox.Text = "Escape (Co-op)";
			EscapeCoopCheckBox.Tag = "Escape (Co-op)";
			EscapeCoopCheckBox.UseVisualStyleBackColor = true;
			//
			//ExtractionCoopCheckBox
			//
			ExtractionCoopCheckBox.AutoSize = true;
			ExtractionCoopCheckBox.Location = new System.Drawing.Point(3, 26);
			ExtractionCoopCheckBox.Name = "ExtractionCoopCheckBox";
			ExtractionCoopCheckBox.Size = new System.Drawing.Size(115, 17);
			ExtractionCoopCheckBox.TabIndex = 1;
			ExtractionCoopCheckBox.Text = "Extraction (Co-op)";
			ExtractionCoopCheckBox.Tag = "Extraction (Co-op)";
			ExtractionCoopCheckBox.UseVisualStyleBackColor = true;
			//
			//HuntedPvpCheckBox
			//
			HuntedPvpCheckBox.AutoSize = true;
			HuntedPvpCheckBox.Location = new System.Drawing.Point(3, 49);
			HuntedPvpCheckBox.Name = "HuntedPvpCheckBox";
			HuntedPvpCheckBox.Size = new System.Drawing.Size(90, 17);
			HuntedPvpCheckBox.TabIndex = 2;
			HuntedPvpCheckBox.Text = "Hunted (PVP)";
			HuntedPvpCheckBox.Tag = "Hunted (PVP)";
			HuntedPvpCheckBox.UseVisualStyleBackColor = true;
			//
			//ContagionPanicClassicCheckBox
			//
			ContagionPanicClassicCheckBox.AutoSize = true;
			ContagionPanicClassicCheckBox.Location = new System.Drawing.Point(3, 95);
			ContagionPanicClassicCheckBox.Name = "ContagionPanicClassicCheckBox";
			ContagionPanicClassicCheckBox.Size = new System.Drawing.Size(169, 17);
			ContagionPanicClassicCheckBox.TabIndex = 4;
			ContagionPanicClassicCheckBox.Text = "Contagion Panic Classic (CPC)";
			ContagionPanicClassicCheckBox.Tag = "Contagion Panic Classic (CPC)";
			ContagionPanicClassicCheckBox.UseVisualStyleBackColor = true;
			//
			//ContagionPanicObjectiveCheckBox
			//
			ContagionPanicObjectiveCheckBox.AutoSize = true;
			ContagionPanicObjectiveCheckBox.Location = new System.Drawing.Point(3, 118);
			ContagionPanicObjectiveCheckBox.Name = "ContagionPanicObjectiveCheckBox";
			ContagionPanicObjectiveCheckBox.Size = new System.Drawing.Size(184, 17);
			ContagionPanicObjectiveCheckBox.TabIndex = 5;
			ContagionPanicObjectiveCheckBox.Text = "Contagion Panic Objective (CPO)";
			ContagionPanicObjectiveCheckBox.Tag = "Contagion Panic Objective (CPO)";
			ContagionPanicObjectiveCheckBox.UseVisualStyleBackColor = true;
			//
			//WeaponsModelTextureCheckBox
			//
			WeaponsModelTextureCheckBox.AutoSize = true;
			WeaponsModelTextureCheckBox.Location = new System.Drawing.Point(3, 141);
			WeaponsModelTextureCheckBox.Name = "WeaponsModelTextureCheckBox";
			WeaponsModelTextureCheckBox.Size = new System.Drawing.Size(155, 17);
			WeaponsModelTextureCheckBox.TabIndex = 6;
			WeaponsModelTextureCheckBox.Text = "Weapons (Model/Texture) ";
			WeaponsModelTextureCheckBox.Tag = "Weapons (Model/Texture) ";
			WeaponsModelTextureCheckBox.UseVisualStyleBackColor = true;
			//
			//SurvivorsModelTextureCheckBox
			//
			SurvivorsModelTextureCheckBox.AutoSize = true;
			SurvivorsModelTextureCheckBox.Location = new System.Drawing.Point(3, 164);
			SurvivorsModelTextureCheckBox.Name = "SurvivorsModelTextureCheckBox";
			SurvivorsModelTextureCheckBox.Size = new System.Drawing.Size(155, 17);
			SurvivorsModelTextureCheckBox.TabIndex = 7;
			SurvivorsModelTextureCheckBox.Text = "Survivors (Model/Texture) ";
			SurvivorsModelTextureCheckBox.Tag = "Survivors (Model/Texture) ";
			SurvivorsModelTextureCheckBox.UseVisualStyleBackColor = true;
			//
			//ZombiesModelTextureCheckBox
			//
			ZombiesModelTextureCheckBox.AutoSize = true;
			ZombiesModelTextureCheckBox.Location = new System.Drawing.Point(3, 187);
			ZombiesModelTextureCheckBox.Name = "ZombiesModelTextureCheckBox";
			ZombiesModelTextureCheckBox.Size = new System.Drawing.Size(146, 17);
			ZombiesModelTextureCheckBox.TabIndex = 8;
			ZombiesModelTextureCheckBox.Text = "Zombies (Model/Texture)";
			ZombiesModelTextureCheckBox.Tag = "Zombies (Model/Texture)";
			ZombiesModelTextureCheckBox.UseVisualStyleBackColor = true;
			//
			//UserInterfaceCheckBox
			//
			UserInterfaceCheckBox.AutoSize = true;
			UserInterfaceCheckBox.Location = new System.Drawing.Point(3, 210);
			UserInterfaceCheckBox.Name = "UserInterfaceCheckBox";
			UserInterfaceCheckBox.Size = new System.Drawing.Size(96, 17);
			UserInterfaceCheckBox.TabIndex = 9;
			UserInterfaceCheckBox.Text = "User Interface";
			UserInterfaceCheckBox.Tag = "User Interface";
			UserInterfaceCheckBox.UseVisualStyleBackColor = true;
			//
			//SoundsCheckBox
			//
			SoundsCheckBox.AutoSize = true;
			SoundsCheckBox.Location = new System.Drawing.Point(3, 233);
			SoundsCheckBox.Name = "SoundsCheckBox";
			SoundsCheckBox.Size = new System.Drawing.Size(61, 17);
			SoundsCheckBox.TabIndex = 10;
			SoundsCheckBox.Text = "Sounds";
			SoundsCheckBox.Tag = "Sounds";
			SoundsCheckBox.UseVisualStyleBackColor = true;
			//
			//FlashlightCheckBox
			//
			FlashlightCheckBox.AutoSize = true;
			FlashlightCheckBox.Location = new System.Drawing.Point(3, 256);
			FlashlightCheckBox.Name = "FlashlightCheckBox";
			FlashlightCheckBox.Size = new System.Drawing.Size(71, 17);
			FlashlightCheckBox.TabIndex = 11;
			FlashlightCheckBox.Text = "Flashlight";
			FlashlightCheckBox.Tag = "Flashlight";
			FlashlightCheckBox.UseVisualStyleBackColor = true;
			//
			//SmartphoneWallpapersCheckBox
			//
			SmartphoneWallpapersCheckBox.AutoSize = true;
			SmartphoneWallpapersCheckBox.Location = new System.Drawing.Point(3, 279);
			SmartphoneWallpapersCheckBox.Name = "SmartphoneWallpapersCheckBox";
			SmartphoneWallpapersCheckBox.Size = new System.Drawing.Size(140, 17);
			SmartphoneWallpapersCheckBox.TabIndex = 12;
			SmartphoneWallpapersCheckBox.Text = "Smartphone Wallpapers";
			SmartphoneWallpapersCheckBox.Tag = "Smartphone Wallpapers";
			SmartphoneWallpapersCheckBox.UseVisualStyleBackColor = true;
			//
			//MiscCheckBox
			//
			MiscCheckBox.AutoSize = true;
			MiscCheckBox.Location = new System.Drawing.Point(3, 302);
			MiscCheckBox.Name = "MiscCheckBox";
			MiscCheckBox.Size = new System.Drawing.Size(50, 17);
			MiscCheckBox.TabIndex = 13;
			MiscCheckBox.Text = "Misc.";
			MiscCheckBox.Tag = "Misc";
			MiscCheckBox.UseVisualStyleBackColor = true;
			//
			//FlatlineCheckBox
			//
			FlatlineCheckBox.AutoSize = true;
			FlatlineCheckBox.Location = new System.Drawing.Point(3, 72);
			FlatlineCheckBox.Name = "FlatlineCheckBox";
			FlatlineCheckBox.Size = new System.Drawing.Size(60, 17);
			FlatlineCheckBox.TabIndex = 3;
			FlatlineCheckBox.Text = "Flatline";
			FlatlineCheckBox.Tag = "Flatline";
			FlatlineCheckBox.UseVisualStyleBackColor = true;
			//
			//ContagionTagsUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoScroll = true;
			Controls.Add(FlatlineCheckBox);
			Controls.Add(MiscCheckBox);
			Controls.Add(SmartphoneWallpapersCheckBox);
			Controls.Add(FlashlightCheckBox);
			Controls.Add(SoundsCheckBox);
			Controls.Add(UserInterfaceCheckBox);
			Controls.Add(ZombiesModelTextureCheckBox);
			Controls.Add(SurvivorsModelTextureCheckBox);
			Controls.Add(WeaponsModelTextureCheckBox);
			Controls.Add(ContagionPanicObjectiveCheckBox);
			Controls.Add(ContagionPanicClassicCheckBox);
			Controls.Add(HuntedPvpCheckBox);
			Controls.Add(ExtractionCoopCheckBox);
			Controls.Add(EscapeCoopCheckBox);
			Name = "ContagionTagsUserControl";
			Size = new System.Drawing.Size(188, 351);
			ResumeLayout(false);
			PerformLayout();

		}

		internal CheckBoxEx EscapeCoopCheckBox;
		internal CheckBoxEx ExtractionCoopCheckBox;
		internal CheckBoxEx HuntedPvpCheckBox;
		internal CheckBoxEx ContagionPanicClassicCheckBox;
		internal CheckBoxEx ContagionPanicObjectiveCheckBox;
		internal CheckBoxEx WeaponsModelTextureCheckBox;
		internal CheckBoxEx SurvivorsModelTextureCheckBox;
		internal CheckBoxEx ZombiesModelTextureCheckBox;
		internal CheckBoxEx UserInterfaceCheckBox;
		internal CheckBoxEx SoundsCheckBox;
		internal CheckBoxEx FlashlightCheckBox;
		internal CheckBoxEx SmartphoneWallpapersCheckBox;
		internal CheckBoxEx MiscCheckBox;
		internal CheckBoxEx FlatlineCheckBox;
	}

}