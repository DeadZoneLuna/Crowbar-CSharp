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
			this.EscapeCoopCheckBox = new CheckBoxEx();
			this.ExtractionCoopCheckBox = new CheckBoxEx();
			this.HuntedPvpCheckBox = new CheckBoxEx();
			this.ContagionPanicClassicCheckBox = new CheckBoxEx();
			this.ContagionPanicObjectiveCheckBox = new CheckBoxEx();
			this.WeaponsModelTextureCheckBox = new CheckBoxEx();
			this.SurvivorsModelTextureCheckBox = new CheckBoxEx();
			this.ZombiesModelTextureCheckBox = new CheckBoxEx();
			this.UserInterfaceCheckBox = new CheckBoxEx();
			this.SoundsCheckBox = new CheckBoxEx();
			this.FlashlightCheckBox = new CheckBoxEx();
			this.SmartphoneWallpapersCheckBox = new CheckBoxEx();
			this.MiscCheckBox = new CheckBoxEx();
			this.FlatlineCheckBox = new CheckBoxEx();
			this.SuspendLayout();
			//
			//EscapeCoopCheckBox
			//
			this.EscapeCoopCheckBox.AutoSize = true;
			this.EscapeCoopCheckBox.Location = new System.Drawing.Point(3, 3);
			this.EscapeCoopCheckBox.Name = "EscapeCoopCheckBox";
			this.EscapeCoopCheckBox.Size = new System.Drawing.Size(100, 17);
			this.EscapeCoopCheckBox.TabIndex = 0;
			this.EscapeCoopCheckBox.Text = "Escape (Co-op)";
			this.EscapeCoopCheckBox.Tag = "Escape (Co-op)";
			this.EscapeCoopCheckBox.UseVisualStyleBackColor = true;
			//
			//ExtractionCoopCheckBox
			//
			this.ExtractionCoopCheckBox.AutoSize = true;
			this.ExtractionCoopCheckBox.Location = new System.Drawing.Point(3, 26);
			this.ExtractionCoopCheckBox.Name = "ExtractionCoopCheckBox";
			this.ExtractionCoopCheckBox.Size = new System.Drawing.Size(115, 17);
			this.ExtractionCoopCheckBox.TabIndex = 1;
			this.ExtractionCoopCheckBox.Text = "Extraction (Co-op)";
			this.ExtractionCoopCheckBox.Tag = "Extraction (Co-op)";
			this.ExtractionCoopCheckBox.UseVisualStyleBackColor = true;
			//
			//HuntedPvpCheckBox
			//
			this.HuntedPvpCheckBox.AutoSize = true;
			this.HuntedPvpCheckBox.Location = new System.Drawing.Point(3, 49);
			this.HuntedPvpCheckBox.Name = "HuntedPvpCheckBox";
			this.HuntedPvpCheckBox.Size = new System.Drawing.Size(90, 17);
			this.HuntedPvpCheckBox.TabIndex = 2;
			this.HuntedPvpCheckBox.Text = "Hunted (PVP)";
			this.HuntedPvpCheckBox.Tag = "Hunted (PVP)";
			this.HuntedPvpCheckBox.UseVisualStyleBackColor = true;
			//
			//ContagionPanicClassicCheckBox
			//
			this.ContagionPanicClassicCheckBox.AutoSize = true;
			this.ContagionPanicClassicCheckBox.Location = new System.Drawing.Point(3, 95);
			this.ContagionPanicClassicCheckBox.Name = "ContagionPanicClassicCheckBox";
			this.ContagionPanicClassicCheckBox.Size = new System.Drawing.Size(169, 17);
			this.ContagionPanicClassicCheckBox.TabIndex = 4;
			this.ContagionPanicClassicCheckBox.Text = "Contagion Panic Classic (CPC)";
			this.ContagionPanicClassicCheckBox.Tag = "Contagion Panic Classic (CPC)";
			this.ContagionPanicClassicCheckBox.UseVisualStyleBackColor = true;
			//
			//ContagionPanicObjectiveCheckBox
			//
			this.ContagionPanicObjectiveCheckBox.AutoSize = true;
			this.ContagionPanicObjectiveCheckBox.Location = new System.Drawing.Point(3, 118);
			this.ContagionPanicObjectiveCheckBox.Name = "ContagionPanicObjectiveCheckBox";
			this.ContagionPanicObjectiveCheckBox.Size = new System.Drawing.Size(184, 17);
			this.ContagionPanicObjectiveCheckBox.TabIndex = 5;
			this.ContagionPanicObjectiveCheckBox.Text = "Contagion Panic Objective (CPO)";
			this.ContagionPanicObjectiveCheckBox.Tag = "Contagion Panic Objective (CPO)";
			this.ContagionPanicObjectiveCheckBox.UseVisualStyleBackColor = true;
			//
			//WeaponsModelTextureCheckBox
			//
			this.WeaponsModelTextureCheckBox.AutoSize = true;
			this.WeaponsModelTextureCheckBox.Location = new System.Drawing.Point(3, 141);
			this.WeaponsModelTextureCheckBox.Name = "WeaponsModelTextureCheckBox";
			this.WeaponsModelTextureCheckBox.Size = new System.Drawing.Size(155, 17);
			this.WeaponsModelTextureCheckBox.TabIndex = 6;
			this.WeaponsModelTextureCheckBox.Text = "Weapons (Model/Texture) ";
			this.WeaponsModelTextureCheckBox.Tag = "Weapons (Model/Texture) ";
			this.WeaponsModelTextureCheckBox.UseVisualStyleBackColor = true;
			//
			//SurvivorsModelTextureCheckBox
			//
			this.SurvivorsModelTextureCheckBox.AutoSize = true;
			this.SurvivorsModelTextureCheckBox.Location = new System.Drawing.Point(3, 164);
			this.SurvivorsModelTextureCheckBox.Name = "SurvivorsModelTextureCheckBox";
			this.SurvivorsModelTextureCheckBox.Size = new System.Drawing.Size(155, 17);
			this.SurvivorsModelTextureCheckBox.TabIndex = 7;
			this.SurvivorsModelTextureCheckBox.Text = "Survivors (Model/Texture) ";
			this.SurvivorsModelTextureCheckBox.Tag = "Survivors (Model/Texture) ";
			this.SurvivorsModelTextureCheckBox.UseVisualStyleBackColor = true;
			//
			//ZombiesModelTextureCheckBox
			//
			this.ZombiesModelTextureCheckBox.AutoSize = true;
			this.ZombiesModelTextureCheckBox.Location = new System.Drawing.Point(3, 187);
			this.ZombiesModelTextureCheckBox.Name = "ZombiesModelTextureCheckBox";
			this.ZombiesModelTextureCheckBox.Size = new System.Drawing.Size(146, 17);
			this.ZombiesModelTextureCheckBox.TabIndex = 8;
			this.ZombiesModelTextureCheckBox.Text = "Zombies (Model/Texture)";
			this.ZombiesModelTextureCheckBox.Tag = "Zombies (Model/Texture)";
			this.ZombiesModelTextureCheckBox.UseVisualStyleBackColor = true;
			//
			//UserInterfaceCheckBox
			//
			this.UserInterfaceCheckBox.AutoSize = true;
			this.UserInterfaceCheckBox.Location = new System.Drawing.Point(3, 210);
			this.UserInterfaceCheckBox.Name = "UserInterfaceCheckBox";
			this.UserInterfaceCheckBox.Size = new System.Drawing.Size(96, 17);
			this.UserInterfaceCheckBox.TabIndex = 9;
			this.UserInterfaceCheckBox.Text = "User Interface";
			this.UserInterfaceCheckBox.Tag = "User Interface";
			this.UserInterfaceCheckBox.UseVisualStyleBackColor = true;
			//
			//SoundsCheckBox
			//
			this.SoundsCheckBox.AutoSize = true;
			this.SoundsCheckBox.Location = new System.Drawing.Point(3, 233);
			this.SoundsCheckBox.Name = "SoundsCheckBox";
			this.SoundsCheckBox.Size = new System.Drawing.Size(61, 17);
			this.SoundsCheckBox.TabIndex = 10;
			this.SoundsCheckBox.Text = "Sounds";
			this.SoundsCheckBox.Tag = "Sounds";
			this.SoundsCheckBox.UseVisualStyleBackColor = true;
			//
			//FlashlightCheckBox
			//
			this.FlashlightCheckBox.AutoSize = true;
			this.FlashlightCheckBox.Location = new System.Drawing.Point(3, 256);
			this.FlashlightCheckBox.Name = "FlashlightCheckBox";
			this.FlashlightCheckBox.Size = new System.Drawing.Size(71, 17);
			this.FlashlightCheckBox.TabIndex = 11;
			this.FlashlightCheckBox.Text = "Flashlight";
			this.FlashlightCheckBox.Tag = "Flashlight";
			this.FlashlightCheckBox.UseVisualStyleBackColor = true;
			//
			//SmartphoneWallpapersCheckBox
			//
			this.SmartphoneWallpapersCheckBox.AutoSize = true;
			this.SmartphoneWallpapersCheckBox.Location = new System.Drawing.Point(3, 279);
			this.SmartphoneWallpapersCheckBox.Name = "SmartphoneWallpapersCheckBox";
			this.SmartphoneWallpapersCheckBox.Size = new System.Drawing.Size(140, 17);
			this.SmartphoneWallpapersCheckBox.TabIndex = 12;
			this.SmartphoneWallpapersCheckBox.Text = "Smartphone Wallpapers";
			this.SmartphoneWallpapersCheckBox.Tag = "Smartphone Wallpapers";
			this.SmartphoneWallpapersCheckBox.UseVisualStyleBackColor = true;
			//
			//MiscCheckBox
			//
			this.MiscCheckBox.AutoSize = true;
			this.MiscCheckBox.Location = new System.Drawing.Point(3, 302);
			this.MiscCheckBox.Name = "MiscCheckBox";
			this.MiscCheckBox.Size = new System.Drawing.Size(50, 17);
			this.MiscCheckBox.TabIndex = 13;
			this.MiscCheckBox.Text = "Misc.";
			this.MiscCheckBox.Tag = "Misc";
			this.MiscCheckBox.UseVisualStyleBackColor = true;
			//
			//FlatlineCheckBox
			//
			this.FlatlineCheckBox.AutoSize = true;
			this.FlatlineCheckBox.Location = new System.Drawing.Point(3, 72);
			this.FlatlineCheckBox.Name = "FlatlineCheckBox";
			this.FlatlineCheckBox.Size = new System.Drawing.Size(60, 17);
			this.FlatlineCheckBox.TabIndex = 3;
			this.FlatlineCheckBox.Text = "Flatline";
			this.FlatlineCheckBox.Tag = "Flatline";
			this.FlatlineCheckBox.UseVisualStyleBackColor = true;
			//
			//ContagionTagsUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.FlatlineCheckBox);
			this.Controls.Add(this.MiscCheckBox);
			this.Controls.Add(this.SmartphoneWallpapersCheckBox);
			this.Controls.Add(this.FlashlightCheckBox);
			this.Controls.Add(this.SoundsCheckBox);
			this.Controls.Add(this.UserInterfaceCheckBox);
			this.Controls.Add(this.ZombiesModelTextureCheckBox);
			this.Controls.Add(this.SurvivorsModelTextureCheckBox);
			this.Controls.Add(this.WeaponsModelTextureCheckBox);
			this.Controls.Add(this.ContagionPanicObjectiveCheckBox);
			this.Controls.Add(this.ContagionPanicClassicCheckBox);
			this.Controls.Add(this.HuntedPvpCheckBox);
			this.Controls.Add(this.ExtractionCoopCheckBox);
			this.Controls.Add(this.EscapeCoopCheckBox);
			this.Name = "ContagionTagsUserControl";
			this.Size = new System.Drawing.Size(188, 351);
			this.ResumeLayout(false);
			this.PerformLayout();

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