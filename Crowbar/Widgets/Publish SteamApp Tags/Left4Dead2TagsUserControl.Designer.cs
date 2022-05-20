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
	public partial class Left4Dead2TagsUserControl : Base_TagsUserControl
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
			this.CampaignsCheckBox = new Crowbar.CheckBoxEx();
			this.SurvivalCheckBox = new Crowbar.CheckBoxEx();
			this.CoopCheckBox = new Crowbar.CheckBoxEx();
			this.SinglePlayerCheckBox = new Crowbar.CheckBoxEx();
			this.VersusCheckBox = new Crowbar.CheckBoxEx();
			this.MutationsCheckBox = new Crowbar.CheckBoxEx();
			this.ScriptsCheckBox = new Crowbar.CheckBoxEx();
			this.WeaponsCheckBox = new Crowbar.CheckBoxEx();
			this.ItemsCheckBox = new Crowbar.CheckBoxEx();
			this.SoundsCheckBox = new Crowbar.CheckBoxEx();
			this.MiscellaneousCheckBox = new Crowbar.CheckBoxEx();
			this.UICheckBox = new Crowbar.CheckBoxEx();
			this.BillCheckBox = new Crowbar.CheckBoxEx();
			this.ScavengeCheckBox = new Crowbar.CheckBoxEx();
			this.FrancisCheckBox = new Crowbar.CheckBoxEx();
			this.LouisCheckBox = new Crowbar.CheckBoxEx();
			this.ZoeyCheckBox = new Crowbar.CheckBoxEx();
			this.CoachCheckBox = new Crowbar.CheckBoxEx();
			this.EllisCheckBox = new Crowbar.CheckBoxEx();
			this.NickCheckBox = new Crowbar.CheckBoxEx();
			this.RochelleCheckBox = new Crowbar.CheckBoxEx();
			this.WitchCheckBox = new Crowbar.CheckBoxEx();
			this.TankCheckBox = new Crowbar.CheckBoxEx();
			this.SpitterCheckBox = new Crowbar.CheckBoxEx();
			this.SmokerCheckBox = new Crowbar.CheckBoxEx();
			this.JockeyCheckBox = new Crowbar.CheckBoxEx();
			this.HunterCheckBox = new Crowbar.CheckBoxEx();
			this.ChargerCheckBox = new Crowbar.CheckBoxEx();
			this.BoomerCheckBox = new Crowbar.CheckBoxEx();
			this.CommonInfectedCheckBox = new Crowbar.CheckBoxEx();
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label6 = new System.Windows.Forms.Label();
			this.SpecialInfectedCheckBox = new Crowbar.CheckBoxEx();
			this.ModelsCheckBox = new Crowbar.CheckBoxEx();
			this.TexturesCheckBox = new Crowbar.CheckBoxEx();
			this.RealismCheckBox = new Crowbar.CheckBoxEx();
			this.RealismVersusCheckBox = new Crowbar.CheckBoxEx();
			this.GrenadeLauncherCheckBox = new Crowbar.CheckBoxEx();
			this.M60CheckBox = new Crowbar.CheckBoxEx();
			this.MeleeCheckBox = new Crowbar.CheckBoxEx();
			this.PistolCheckBox = new Crowbar.CheckBoxEx();
			this.RifleCheckBox = new Crowbar.CheckBoxEx();
			this.ShotgunCheckBox = new Crowbar.CheckBoxEx();
			this.SMGCheckBox = new Crowbar.CheckBoxEx();
			this.SniperCheckBox = new Crowbar.CheckBoxEx();
			this.ThrowableCheckBox = new Crowbar.CheckBoxEx();
			this.AdrenalineCheckBox = new Crowbar.CheckBoxEx();
			this.DefibrillatorCheckBox = new Crowbar.CheckBoxEx();
			this.MedkitCheckBox = new Crowbar.CheckBoxEx();
			this.OtherCheckBox = new Crowbar.CheckBoxEx();
			this.PillsCheckBox = new Crowbar.CheckBoxEx();
			this.SurvivorsCheckBox = new Crowbar.CheckBoxEx();
			this.SuspendLayout();
			//
			//CampaignsCheckBox
			//
			this.CampaignsCheckBox.AutoSize = true;
			this.CampaignsCheckBox.IsReadOnly = false;
			this.CampaignsCheckBox.Location = new System.Drawing.Point(196, 16);
			this.CampaignsCheckBox.Name = "CampaignsCheckBox";
			this.CampaignsCheckBox.Size = new System.Drawing.Size(78, 17);
			this.CampaignsCheckBox.TabIndex = 22;
			this.CampaignsCheckBox.Tag = "Campaigns";
			this.CampaignsCheckBox.Text = "Campaigns";
			this.CampaignsCheckBox.UseVisualStyleBackColor = true;
			//
			//SurvivalCheckBox
			//
			this.SurvivalCheckBox.AutoSize = true;
			this.SurvivalCheckBox.IsReadOnly = false;
			this.SurvivalCheckBox.Location = new System.Drawing.Point(293, 154);
			this.SurvivalCheckBox.Name = "SurvivalCheckBox";
			this.SurvivalCheckBox.Size = new System.Drawing.Size(64, 17);
			this.SurvivalCheckBox.TabIndex = 36;
			this.SurvivalCheckBox.Tag = "Survival";
			this.SurvivalCheckBox.Text = "Survival";
			this.SurvivalCheckBox.UseVisualStyleBackColor = true;
			//
			//CoopCheckBox
			//
			this.CoopCheckBox.AutoSize = true;
			this.CoopCheckBox.IsReadOnly = false;
			this.CoopCheckBox.Location = new System.Drawing.Point(293, 16);
			this.CoopCheckBox.Name = "CoopCheckBox";
			this.CoopCheckBox.Size = new System.Drawing.Size(55, 17);
			this.CoopCheckBox.TabIndex = 30;
			this.CoopCheckBox.Tag = "Co-op";
			this.CoopCheckBox.Text = "Co-op";
			this.CoopCheckBox.UseVisualStyleBackColor = true;
			//
			//SinglePlayerCheckBox
			//
			this.SinglePlayerCheckBox.AutoSize = true;
			this.SinglePlayerCheckBox.IsReadOnly = false;
			this.SinglePlayerCheckBox.Location = new System.Drawing.Point(293, 131);
			this.SinglePlayerCheckBox.Name = "SinglePlayerCheckBox";
			this.SinglePlayerCheckBox.Size = new System.Drawing.Size(87, 17);
			this.SinglePlayerCheckBox.TabIndex = 35;
			this.SinglePlayerCheckBox.Tag = "Single Player";
			this.SinglePlayerCheckBox.Text = "Single Player";
			this.SinglePlayerCheckBox.UseVisualStyleBackColor = true;
			//
			//VersusCheckBox
			//
			this.VersusCheckBox.AutoSize = true;
			this.VersusCheckBox.IsReadOnly = false;
			this.VersusCheckBox.Location = new System.Drawing.Point(293, 177);
			this.VersusCheckBox.Name = "VersusCheckBox";
			this.VersusCheckBox.Size = new System.Drawing.Size(58, 17);
			this.VersusCheckBox.TabIndex = 37;
			this.VersusCheckBox.Tag = "Versus";
			this.VersusCheckBox.Text = "Versus";
			this.VersusCheckBox.UseVisualStyleBackColor = true;
			//
			//MutationsCheckBox
			//
			this.MutationsCheckBox.AutoSize = true;
			this.MutationsCheckBox.IsReadOnly = false;
			this.MutationsCheckBox.Location = new System.Drawing.Point(293, 39);
			this.MutationsCheckBox.Name = "MutationsCheckBox";
			this.MutationsCheckBox.Size = new System.Drawing.Size(73, 17);
			this.MutationsCheckBox.TabIndex = 31;
			this.MutationsCheckBox.Tag = "Mutations";
			this.MutationsCheckBox.Text = "Mutations";
			this.MutationsCheckBox.UseVisualStyleBackColor = true;
			//
			//ScriptsCheckBox
			//
			this.ScriptsCheckBox.AutoSize = true;
			this.ScriptsCheckBox.IsReadOnly = false;
			this.ScriptsCheckBox.Location = new System.Drawing.Point(196, 85);
			this.ScriptsCheckBox.Name = "ScriptsCheckBox";
			this.ScriptsCheckBox.Size = new System.Drawing.Size(58, 17);
			this.ScriptsCheckBox.TabIndex = 25;
			this.ScriptsCheckBox.Tag = "Scripts";
			this.ScriptsCheckBox.Text = "Scripts";
			this.ScriptsCheckBox.UseVisualStyleBackColor = true;
			//
			//WeaponsCheckBox
			//
			this.WeaponsCheckBox.AutoSize = true;
			this.WeaponsCheckBox.IsReadOnly = false;
			this.WeaponsCheckBox.Location = new System.Drawing.Point(399, 16);
			this.WeaponsCheckBox.Name = "WeaponsCheckBox";
			this.WeaponsCheckBox.Size = new System.Drawing.Size(71, 17);
			this.WeaponsCheckBox.TabIndex = 39;
			this.WeaponsCheckBox.Tag = "Weapons";
			this.WeaponsCheckBox.Text = "Weapons";
			this.WeaponsCheckBox.UseVisualStyleBackColor = true;
			//
			//ItemsCheckBox
			//
			this.ItemsCheckBox.AutoSize = true;
			this.ItemsCheckBox.IsReadOnly = false;
			this.ItemsCheckBox.Location = new System.Drawing.Point(519, 16);
			this.ItemsCheckBox.Name = "ItemsCheckBox";
			this.ItemsCheckBox.Size = new System.Drawing.Size(53, 17);
			this.ItemsCheckBox.TabIndex = 50;
			this.ItemsCheckBox.Tag = "Items";
			this.ItemsCheckBox.Text = "Items";
			this.ItemsCheckBox.UseVisualStyleBackColor = true;
			//
			//SoundsCheckBox
			//
			this.SoundsCheckBox.AutoSize = true;
			this.SoundsCheckBox.IsReadOnly = false;
			this.SoundsCheckBox.Location = new System.Drawing.Point(196, 108);
			this.SoundsCheckBox.Name = "SoundsCheckBox";
			this.SoundsCheckBox.Size = new System.Drawing.Size(61, 17);
			this.SoundsCheckBox.TabIndex = 26;
			this.SoundsCheckBox.Tag = "Sounds";
			this.SoundsCheckBox.Text = "Sounds";
			this.SoundsCheckBox.UseVisualStyleBackColor = true;
			//
			//MiscellaneousCheckBox
			//
			this.MiscellaneousCheckBox.AutoSize = true;
			this.MiscellaneousCheckBox.IsReadOnly = false;
			this.MiscellaneousCheckBox.Location = new System.Drawing.Point(196, 39);
			this.MiscellaneousCheckBox.Name = "MiscellaneousCheckBox";
			this.MiscellaneousCheckBox.Size = new System.Drawing.Size(91, 17);
			this.MiscellaneousCheckBox.TabIndex = 23;
			this.MiscellaneousCheckBox.Tag = "Miscellaneous";
			this.MiscellaneousCheckBox.Text = "Miscellaneous";
			this.MiscellaneousCheckBox.UseVisualStyleBackColor = true;
			//
			//UICheckBox
			//
			this.UICheckBox.AutoSize = true;
			this.UICheckBox.IsReadOnly = false;
			this.UICheckBox.Location = new System.Drawing.Point(196, 154);
			this.UICheckBox.Name = "UICheckBox";
			this.UICheckBox.Size = new System.Drawing.Size(37, 17);
			this.UICheckBox.TabIndex = 28;
			this.UICheckBox.Tag = "UI";
			this.UICheckBox.Text = "UI";
			this.UICheckBox.UseVisualStyleBackColor = true;
			//
			//BillCheckBox
			//
			this.BillCheckBox.AutoSize = true;
			this.BillCheckBox.IsReadOnly = false;
			this.BillCheckBox.Location = new System.Drawing.Point(3, 39);
			this.BillCheckBox.Name = "BillCheckBox";
			this.BillCheckBox.Size = new System.Drawing.Size(38, 17);
			this.BillCheckBox.TabIndex = 2;
			this.BillCheckBox.Tag = "Bill";
			this.BillCheckBox.Text = "Bill";
			this.BillCheckBox.UseVisualStyleBackColor = true;
			//
			//ScavengeCheckBox
			//
			this.ScavengeCheckBox.AutoSize = true;
			this.ScavengeCheckBox.IsReadOnly = false;
			this.ScavengeCheckBox.Location = new System.Drawing.Point(293, 108);
			this.ScavengeCheckBox.Name = "ScavengeCheckBox";
			this.ScavengeCheckBox.Size = new System.Drawing.Size(73, 17);
			this.ScavengeCheckBox.TabIndex = 34;
			this.ScavengeCheckBox.Tag = "Scavenge";
			this.ScavengeCheckBox.Text = "Scavenge";
			this.ScavengeCheckBox.UseVisualStyleBackColor = true;
			//
			//FrancisCheckBox
			//
			this.FrancisCheckBox.AutoSize = true;
			this.FrancisCheckBox.IsReadOnly = false;
			this.FrancisCheckBox.Location = new System.Drawing.Point(3, 62);
			this.FrancisCheckBox.Name = "FrancisCheckBox";
			this.FrancisCheckBox.Size = new System.Drawing.Size(60, 17);
			this.FrancisCheckBox.TabIndex = 3;
			this.FrancisCheckBox.Tag = "Francis";
			this.FrancisCheckBox.Text = "Francis";
			this.FrancisCheckBox.UseVisualStyleBackColor = true;
			//
			//LouisCheckBox
			//
			this.LouisCheckBox.AutoSize = true;
			this.LouisCheckBox.IsReadOnly = false;
			this.LouisCheckBox.Location = new System.Drawing.Point(3, 85);
			this.LouisCheckBox.Name = "LouisCheckBox";
			this.LouisCheckBox.Size = new System.Drawing.Size(50, 17);
			this.LouisCheckBox.TabIndex = 4;
			this.LouisCheckBox.Tag = "Louis";
			this.LouisCheckBox.Text = "Louis";
			this.LouisCheckBox.UseVisualStyleBackColor = true;
			//
			//ZoeyCheckBox
			//
			this.ZoeyCheckBox.AutoSize = true;
			this.ZoeyCheckBox.IsReadOnly = false;
			this.ZoeyCheckBox.Location = new System.Drawing.Point(3, 108);
			this.ZoeyCheckBox.Name = "ZoeyCheckBox";
			this.ZoeyCheckBox.Size = new System.Drawing.Size(50, 17);
			this.ZoeyCheckBox.TabIndex = 5;
			this.ZoeyCheckBox.Tag = "Zoey";
			this.ZoeyCheckBox.Text = "Zoey";
			this.ZoeyCheckBox.UseVisualStyleBackColor = true;
			//
			//CoachCheckBox
			//
			this.CoachCheckBox.AutoSize = true;
			this.CoachCheckBox.IsReadOnly = false;
			this.CoachCheckBox.Location = new System.Drawing.Point(3, 131);
			this.CoachCheckBox.Name = "CoachCheckBox";
			this.CoachCheckBox.Size = new System.Drawing.Size(56, 17);
			this.CoachCheckBox.TabIndex = 6;
			this.CoachCheckBox.Tag = "Coach";
			this.CoachCheckBox.Text = "Coach";
			this.CoachCheckBox.UseVisualStyleBackColor = true;
			//
			//EllisCheckBox
			//
			this.EllisCheckBox.AutoSize = true;
			this.EllisCheckBox.IsReadOnly = false;
			this.EllisCheckBox.Location = new System.Drawing.Point(3, 154);
			this.EllisCheckBox.Name = "EllisCheckBox";
			this.EllisCheckBox.Size = new System.Drawing.Size(43, 17);
			this.EllisCheckBox.TabIndex = 7;
			this.EllisCheckBox.Tag = "Ellis";
			this.EllisCheckBox.Text = "Ellis";
			this.EllisCheckBox.UseVisualStyleBackColor = true;
			//
			//NickCheckBox
			//
			this.NickCheckBox.AutoSize = true;
			this.NickCheckBox.IsReadOnly = false;
			this.NickCheckBox.Location = new System.Drawing.Point(3, 177);
			this.NickCheckBox.Name = "NickCheckBox";
			this.NickCheckBox.Size = new System.Drawing.Size(45, 17);
			this.NickCheckBox.TabIndex = 8;
			this.NickCheckBox.Tag = "Nick";
			this.NickCheckBox.Text = "Nick";
			this.NickCheckBox.UseVisualStyleBackColor = true;
			//
			//RochelleCheckBox
			//
			this.RochelleCheckBox.AutoSize = true;
			this.RochelleCheckBox.IsReadOnly = false;
			this.RochelleCheckBox.Location = new System.Drawing.Point(3, 200);
			this.RochelleCheckBox.Name = "RochelleCheckBox";
			this.RochelleCheckBox.Size = new System.Drawing.Size(66, 17);
			this.RochelleCheckBox.TabIndex = 9;
			this.RochelleCheckBox.Tag = "Rochelle";
			this.RochelleCheckBox.Text = "Rochelle";
			this.RochelleCheckBox.UseVisualStyleBackColor = true;
			//
			//WitchCheckBox
			//
			this.WitchCheckBox.AutoSize = true;
			this.WitchCheckBox.IsReadOnly = false;
			this.WitchCheckBox.Location = new System.Drawing.Point(76, 223);
			this.WitchCheckBox.Name = "WitchCheckBox";
			this.WitchCheckBox.Size = new System.Drawing.Size(53, 17);
			this.WitchCheckBox.TabIndex = 20;
			this.WitchCheckBox.Tag = "Witch";
			this.WitchCheckBox.Text = "Witch";
			this.WitchCheckBox.UseVisualStyleBackColor = true;
			//
			//TankCheckBox
			//
			this.TankCheckBox.AutoSize = true;
			this.TankCheckBox.IsReadOnly = false;
			this.TankCheckBox.Location = new System.Drawing.Point(76, 200);
			this.TankCheckBox.Name = "TankCheckBox";
			this.TankCheckBox.Size = new System.Drawing.Size(49, 17);
			this.TankCheckBox.TabIndex = 19;
			this.TankCheckBox.Tag = "Tank";
			this.TankCheckBox.Text = "Tank";
			this.TankCheckBox.UseVisualStyleBackColor = true;
			//
			//SpitterCheckBox
			//
			this.SpitterCheckBox.AutoSize = true;
			this.SpitterCheckBox.IsReadOnly = false;
			this.SpitterCheckBox.Location = new System.Drawing.Point(76, 177);
			this.SpitterCheckBox.Name = "SpitterCheckBox";
			this.SpitterCheckBox.Size = new System.Drawing.Size(58, 17);
			this.SpitterCheckBox.TabIndex = 18;
			this.SpitterCheckBox.Tag = "Spitter";
			this.SpitterCheckBox.Text = "Spitter";
			this.SpitterCheckBox.UseVisualStyleBackColor = true;
			//
			//SmokerCheckBox
			//
			this.SmokerCheckBox.AutoSize = true;
			this.SmokerCheckBox.IsReadOnly = false;
			this.SmokerCheckBox.Location = new System.Drawing.Point(76, 154);
			this.SmokerCheckBox.Name = "SmokerCheckBox";
			this.SmokerCheckBox.Size = new System.Drawing.Size(61, 17);
			this.SmokerCheckBox.TabIndex = 17;
			this.SmokerCheckBox.Tag = "Smoker";
			this.SmokerCheckBox.Text = "Smoker";
			this.SmokerCheckBox.UseVisualStyleBackColor = true;
			//
			//JockeyCheckBox
			//
			this.JockeyCheckBox.AutoSize = true;
			this.JockeyCheckBox.IsReadOnly = false;
			this.JockeyCheckBox.Location = new System.Drawing.Point(76, 131);
			this.JockeyCheckBox.Name = "JockeyCheckBox";
			this.JockeyCheckBox.Size = new System.Drawing.Size(59, 17);
			this.JockeyCheckBox.TabIndex = 16;
			this.JockeyCheckBox.Tag = "Jockey";
			this.JockeyCheckBox.Text = "Jockey";
			this.JockeyCheckBox.UseVisualStyleBackColor = true;
			//
			//HunterCheckBox
			//
			this.HunterCheckBox.AutoSize = true;
			this.HunterCheckBox.IsReadOnly = false;
			this.HunterCheckBox.Location = new System.Drawing.Point(76, 108);
			this.HunterCheckBox.Name = "HunterCheckBox";
			this.HunterCheckBox.Size = new System.Drawing.Size(59, 17);
			this.HunterCheckBox.TabIndex = 15;
			this.HunterCheckBox.Tag = "Hunter";
			this.HunterCheckBox.Text = "Hunter";
			this.HunterCheckBox.UseVisualStyleBackColor = true;
			//
			//ChargerCheckBox
			//
			this.ChargerCheckBox.AutoSize = true;
			this.ChargerCheckBox.IsReadOnly = false;
			this.ChargerCheckBox.Location = new System.Drawing.Point(76, 85);
			this.ChargerCheckBox.Name = "ChargerCheckBox";
			this.ChargerCheckBox.Size = new System.Drawing.Size(65, 17);
			this.ChargerCheckBox.TabIndex = 14;
			this.ChargerCheckBox.Tag = "Charger";
			this.ChargerCheckBox.Text = "Charger";
			this.ChargerCheckBox.UseVisualStyleBackColor = true;
			//
			//BoomerCheckBox
			//
			this.BoomerCheckBox.AutoSize = true;
			this.BoomerCheckBox.IsReadOnly = false;
			this.BoomerCheckBox.Location = new System.Drawing.Point(76, 62);
			this.BoomerCheckBox.Name = "BoomerCheckBox";
			this.BoomerCheckBox.Size = new System.Drawing.Size(62, 17);
			this.BoomerCheckBox.TabIndex = 13;
			this.BoomerCheckBox.Tag = "Boomer";
			this.BoomerCheckBox.Text = "Boomer";
			this.BoomerCheckBox.UseVisualStyleBackColor = true;
			//
			//CommonInfectedCheckBox
			//
			this.CommonInfectedCheckBox.AutoSize = true;
			this.CommonInfectedCheckBox.IsReadOnly = false;
			this.CommonInfectedCheckBox.Location = new System.Drawing.Point(76, 16);
			this.CommonInfectedCheckBox.Name = "CommonInfectedCheckBox";
			this.CommonInfectedCheckBox.Size = new System.Drawing.Size(111, 17);
			this.CommonInfectedCheckBox.TabIndex = 11;
			this.CommonInfectedCheckBox.Tag = "Common Infected";
			this.CommonInfectedCheckBox.Text = "Common Infected";
			this.CommonInfectedCheckBox.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(1, 0);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(64, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "SURVIVORS";
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(73, 0);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(56, 13);
			this.Label2.TabIndex = 10;
			this.Label2.Text = "INFECTED";
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(193, 0);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(85, 13);
			this.Label3.TabIndex = 21;
			this.Label3.Text = "GAME CONTENT";
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(290, 0);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(73, 13);
			this.Label4.TabIndex = 29;
			this.Label4.Text = "GAME MODES";
			//
			//Label5
			//
			this.Label5.AutoSize = true;
			this.Label5.Location = new System.Drawing.Point(396, 0);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(57, 13);
			this.Label5.TabIndex = 38;
			this.Label5.Text = "WEAPONS";
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(516, 0);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(37, 13);
			this.Label6.TabIndex = 49;
			this.Label6.Text = "ITEMS";
			//
			//SpecialInfectedCheckBox
			//
			this.SpecialInfectedCheckBox.AutoSize = true;
			this.SpecialInfectedCheckBox.IsReadOnly = false;
			this.SpecialInfectedCheckBox.Location = new System.Drawing.Point(76, 39);
			this.SpecialInfectedCheckBox.Name = "SpecialInfectedCheckBox";
			this.SpecialInfectedCheckBox.Size = new System.Drawing.Size(103, 17);
			this.SpecialInfectedCheckBox.TabIndex = 12;
			this.SpecialInfectedCheckBox.Tag = "Special Infected";
			this.SpecialInfectedCheckBox.Text = "Special Infected";
			this.SpecialInfectedCheckBox.UseVisualStyleBackColor = true;
			//
			//ModelsCheckBox
			//
			this.ModelsCheckBox.AutoSize = true;
			this.ModelsCheckBox.IsReadOnly = false;
			this.ModelsCheckBox.Location = new System.Drawing.Point(196, 62);
			this.ModelsCheckBox.Name = "ModelsCheckBox";
			this.ModelsCheckBox.Size = new System.Drawing.Size(59, 17);
			this.ModelsCheckBox.TabIndex = 24;
			this.ModelsCheckBox.Tag = "Models";
			this.ModelsCheckBox.Text = "Models";
			this.ModelsCheckBox.UseVisualStyleBackColor = true;
			//
			//TexturesCheckBox
			//
			this.TexturesCheckBox.AutoSize = true;
			this.TexturesCheckBox.IsReadOnly = false;
			this.TexturesCheckBox.Location = new System.Drawing.Point(196, 131);
			this.TexturesCheckBox.Name = "TexturesCheckBox";
			this.TexturesCheckBox.Size = new System.Drawing.Size(69, 17);
			this.TexturesCheckBox.TabIndex = 27;
			this.TexturesCheckBox.Tag = "Textures";
			this.TexturesCheckBox.Text = "Textures";
			this.TexturesCheckBox.UseVisualStyleBackColor = true;
			//
			//RealismCheckBox
			//
			this.RealismCheckBox.AutoSize = true;
			this.RealismCheckBox.IsReadOnly = false;
			this.RealismCheckBox.Location = new System.Drawing.Point(293, 62);
			this.RealismCheckBox.Name = "RealismCheckBox";
			this.RealismCheckBox.Size = new System.Drawing.Size(62, 17);
			this.RealismCheckBox.TabIndex = 32;
			this.RealismCheckBox.Tag = "Realism";
			this.RealismCheckBox.Text = "Realism";
			this.RealismCheckBox.UseVisualStyleBackColor = true;
			//
			//RealismVersusCheckBox
			//
			this.RealismVersusCheckBox.AutoSize = true;
			this.RealismVersusCheckBox.IsReadOnly = false;
			this.RealismVersusCheckBox.Location = new System.Drawing.Point(293, 85);
			this.RealismVersusCheckBox.Name = "RealismVersusCheckBox";
			this.RealismVersusCheckBox.Size = new System.Drawing.Size(97, 17);
			this.RealismVersusCheckBox.TabIndex = 33;
			this.RealismVersusCheckBox.Tag = "Realism Versus";
			this.RealismVersusCheckBox.Text = "Realism Versus";
			this.RealismVersusCheckBox.UseVisualStyleBackColor = true;
			//
			//GrenadeLauncherCheckBox
			//
			this.GrenadeLauncherCheckBox.AutoSize = true;
			this.GrenadeLauncherCheckBox.IsReadOnly = false;
			this.GrenadeLauncherCheckBox.Location = new System.Drawing.Point(399, 39);
			this.GrenadeLauncherCheckBox.Name = "GrenadeLauncherCheckBox";
			this.GrenadeLauncherCheckBox.Size = new System.Drawing.Size(114, 17);
			this.GrenadeLauncherCheckBox.TabIndex = 40;
			this.GrenadeLauncherCheckBox.Tag = "Grenade Launcher";
			this.GrenadeLauncherCheckBox.Text = "Grenade Launcher";
			this.GrenadeLauncherCheckBox.UseVisualStyleBackColor = true;
			//
			//M60CheckBox
			//
			this.M60CheckBox.AutoSize = true;
			this.M60CheckBox.IsReadOnly = false;
			this.M60CheckBox.Location = new System.Drawing.Point(399, 62);
			this.M60CheckBox.Name = "M60CheckBox";
			this.M60CheckBox.Size = new System.Drawing.Size(46, 17);
			this.M60CheckBox.TabIndex = 41;
			this.M60CheckBox.Tag = "M60";
			this.M60CheckBox.Text = "M60";
			this.M60CheckBox.UseVisualStyleBackColor = true;
			//
			//MeleeCheckBox
			//
			this.MeleeCheckBox.AutoSize = true;
			this.MeleeCheckBox.IsReadOnly = false;
			this.MeleeCheckBox.Location = new System.Drawing.Point(399, 85);
			this.MeleeCheckBox.Name = "MeleeCheckBox";
			this.MeleeCheckBox.Size = new System.Drawing.Size(54, 17);
			this.MeleeCheckBox.TabIndex = 42;
			this.MeleeCheckBox.Tag = "Melee";
			this.MeleeCheckBox.Text = "Melee";
			this.MeleeCheckBox.UseVisualStyleBackColor = true;
			//
			//PistolCheckBox
			//
			this.PistolCheckBox.AutoSize = true;
			this.PistolCheckBox.IsReadOnly = false;
			this.PistolCheckBox.Location = new System.Drawing.Point(399, 108);
			this.PistolCheckBox.Name = "PistolCheckBox";
			this.PistolCheckBox.Size = new System.Drawing.Size(51, 17);
			this.PistolCheckBox.TabIndex = 43;
			this.PistolCheckBox.Tag = "Pistol";
			this.PistolCheckBox.Text = "Pistol";
			this.PistolCheckBox.UseVisualStyleBackColor = true;
			//
			//RifleCheckBox
			//
			this.RifleCheckBox.AutoSize = true;
			this.RifleCheckBox.IsReadOnly = false;
			this.RifleCheckBox.Location = new System.Drawing.Point(399, 131);
			this.RifleCheckBox.Name = "RifleCheckBox";
			this.RifleCheckBox.Size = new System.Drawing.Size(47, 17);
			this.RifleCheckBox.TabIndex = 44;
			this.RifleCheckBox.Tag = "Rifle";
			this.RifleCheckBox.Text = "Rifle";
			this.RifleCheckBox.UseVisualStyleBackColor = true;
			//
			//ShotgunCheckBox
			//
			this.ShotgunCheckBox.AutoSize = true;
			this.ShotgunCheckBox.IsReadOnly = false;
			this.ShotgunCheckBox.Location = new System.Drawing.Point(399, 154);
			this.ShotgunCheckBox.Name = "ShotgunCheckBox";
			this.ShotgunCheckBox.Size = new System.Drawing.Size(66, 17);
			this.ShotgunCheckBox.TabIndex = 45;
			this.ShotgunCheckBox.Tag = "Shotgun";
			this.ShotgunCheckBox.Text = "Shotgun";
			this.ShotgunCheckBox.UseVisualStyleBackColor = true;
			//
			//SMGCheckBox
			//
			this.SMGCheckBox.AutoSize = true;
			this.SMGCheckBox.IsReadOnly = false;
			this.SMGCheckBox.Location = new System.Drawing.Point(399, 177);
			this.SMGCheckBox.Name = "SMGCheckBox";
			this.SMGCheckBox.Size = new System.Drawing.Size(47, 17);
			this.SMGCheckBox.TabIndex = 46;
			this.SMGCheckBox.Tag = "SMG";
			this.SMGCheckBox.Text = "SMG";
			this.SMGCheckBox.UseVisualStyleBackColor = true;
			//
			//SniperCheckBox
			//
			this.SniperCheckBox.AutoSize = true;
			this.SniperCheckBox.IsReadOnly = false;
			this.SniperCheckBox.Location = new System.Drawing.Point(399, 200);
			this.SniperCheckBox.Name = "SniperCheckBox";
			this.SniperCheckBox.Size = new System.Drawing.Size(56, 17);
			this.SniperCheckBox.TabIndex = 47;
			this.SniperCheckBox.Tag = "Sniper";
			this.SniperCheckBox.Text = "Sniper";
			this.SniperCheckBox.UseVisualStyleBackColor = true;
			//
			//ThrowableCheckBox
			//
			this.ThrowableCheckBox.AutoSize = true;
			this.ThrowableCheckBox.IsReadOnly = false;
			this.ThrowableCheckBox.Location = new System.Drawing.Point(399, 223);
			this.ThrowableCheckBox.Name = "ThrowableCheckBox";
			this.ThrowableCheckBox.Size = new System.Drawing.Size(76, 17);
			this.ThrowableCheckBox.TabIndex = 48;
			this.ThrowableCheckBox.Tag = "Throwable";
			this.ThrowableCheckBox.Text = "Throwable";
			this.ThrowableCheckBox.UseVisualStyleBackColor = true;
			//
			//AdrenalineCheckBox
			//
			this.AdrenalineCheckBox.AutoSize = true;
			this.AdrenalineCheckBox.IsReadOnly = false;
			this.AdrenalineCheckBox.Location = new System.Drawing.Point(519, 39);
			this.AdrenalineCheckBox.Name = "AdrenalineCheckBox";
			this.AdrenalineCheckBox.Size = new System.Drawing.Size(77, 17);
			this.AdrenalineCheckBox.TabIndex = 51;
			this.AdrenalineCheckBox.Tag = "Adrenaline";
			this.AdrenalineCheckBox.Text = "Adrenaline";
			this.AdrenalineCheckBox.UseVisualStyleBackColor = true;
			//
			//DefibrillatorCheckBox
			//
			this.DefibrillatorCheckBox.AutoSize = true;
			this.DefibrillatorCheckBox.IsReadOnly = false;
			this.DefibrillatorCheckBox.Location = new System.Drawing.Point(519, 62);
			this.DefibrillatorCheckBox.Name = "DefibrillatorCheckBox";
			this.DefibrillatorCheckBox.Size = new System.Drawing.Size(81, 17);
			this.DefibrillatorCheckBox.TabIndex = 52;
			this.DefibrillatorCheckBox.Tag = "Defibrillator";
			this.DefibrillatorCheckBox.Text = "Defibrillator";
			this.DefibrillatorCheckBox.UseVisualStyleBackColor = true;
			//
			//MedkitCheckBox
			//
			this.MedkitCheckBox.AutoSize = true;
			this.MedkitCheckBox.IsReadOnly = false;
			this.MedkitCheckBox.Location = new System.Drawing.Point(519, 85);
			this.MedkitCheckBox.Name = "MedkitCheckBox";
			this.MedkitCheckBox.Size = new System.Drawing.Size(57, 17);
			this.MedkitCheckBox.TabIndex = 53;
			this.MedkitCheckBox.Tag = "Medkit";
			this.MedkitCheckBox.Text = "Medkit";
			this.MedkitCheckBox.UseVisualStyleBackColor = true;
			//
			//OtherCheckBox
			//
			this.OtherCheckBox.AutoSize = true;
			this.OtherCheckBox.IsReadOnly = false;
			this.OtherCheckBox.Location = new System.Drawing.Point(519, 108);
			this.OtherCheckBox.Name = "OtherCheckBox";
			this.OtherCheckBox.Size = new System.Drawing.Size(54, 17);
			this.OtherCheckBox.TabIndex = 54;
			this.OtherCheckBox.Tag = "Other";
			this.OtherCheckBox.Text = "Other";
			this.OtherCheckBox.UseVisualStyleBackColor = true;
			//
			//PillsCheckBox
			//
			this.PillsCheckBox.AutoSize = true;
			this.PillsCheckBox.IsReadOnly = false;
			this.PillsCheckBox.Location = new System.Drawing.Point(519, 131);
			this.PillsCheckBox.Name = "PillsCheckBox";
			this.PillsCheckBox.Size = new System.Drawing.Size(43, 17);
			this.PillsCheckBox.TabIndex = 55;
			this.PillsCheckBox.Tag = "Pills";
			this.PillsCheckBox.Text = "Pills";
			this.PillsCheckBox.UseVisualStyleBackColor = true;
			//
			//SurvivorsCheckBox
			//
			this.SurvivorsCheckBox.AutoSize = true;
			this.SurvivorsCheckBox.IsReadOnly = false;
			this.SurvivorsCheckBox.Location = new System.Drawing.Point(3, 16);
			this.SurvivorsCheckBox.Name = "SurvivorsCheckBox";
			this.SurvivorsCheckBox.Size = new System.Drawing.Size(71, 17);
			this.SurvivorsCheckBox.TabIndex = 1;
			this.SurvivorsCheckBox.Tag = "Survivors";
			this.SurvivorsCheckBox.Text = "Survivors";
			this.SurvivorsCheckBox.UseVisualStyleBackColor = true;
			//
			//Left4Dead2TagsUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.SurvivorsCheckBox);
			this.Controls.Add(this.PillsCheckBox);
			this.Controls.Add(this.OtherCheckBox);
			this.Controls.Add(this.MedkitCheckBox);
			this.Controls.Add(this.DefibrillatorCheckBox);
			this.Controls.Add(this.AdrenalineCheckBox);
			this.Controls.Add(this.ThrowableCheckBox);
			this.Controls.Add(this.SniperCheckBox);
			this.Controls.Add(this.SMGCheckBox);
			this.Controls.Add(this.ShotgunCheckBox);
			this.Controls.Add(this.RifleCheckBox);
			this.Controls.Add(this.PistolCheckBox);
			this.Controls.Add(this.MeleeCheckBox);
			this.Controls.Add(this.M60CheckBox);
			this.Controls.Add(this.GrenadeLauncherCheckBox);
			this.Controls.Add(this.RealismVersusCheckBox);
			this.Controls.Add(this.RealismCheckBox);
			this.Controls.Add(this.TexturesCheckBox);
			this.Controls.Add(this.ModelsCheckBox);
			this.Controls.Add(this.SpecialInfectedCheckBox);
			this.Controls.Add(this.Label6);
			this.Controls.Add(this.Label5);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.CommonInfectedCheckBox);
			this.Controls.Add(this.WitchCheckBox);
			this.Controls.Add(this.TankCheckBox);
			this.Controls.Add(this.SpitterCheckBox);
			this.Controls.Add(this.SmokerCheckBox);
			this.Controls.Add(this.JockeyCheckBox);
			this.Controls.Add(this.HunterCheckBox);
			this.Controls.Add(this.ChargerCheckBox);
			this.Controls.Add(this.BoomerCheckBox);
			this.Controls.Add(this.RochelleCheckBox);
			this.Controls.Add(this.NickCheckBox);
			this.Controls.Add(this.EllisCheckBox);
			this.Controls.Add(this.CoachCheckBox);
			this.Controls.Add(this.ZoeyCheckBox);
			this.Controls.Add(this.LouisCheckBox);
			this.Controls.Add(this.FrancisCheckBox);
			this.Controls.Add(this.ScavengeCheckBox);
			this.Controls.Add(this.BillCheckBox);
			this.Controls.Add(this.UICheckBox);
			this.Controls.Add(this.MiscellaneousCheckBox);
			this.Controls.Add(this.SoundsCheckBox);
			this.Controls.Add(this.ItemsCheckBox);
			this.Controls.Add(this.WeaponsCheckBox);
			this.Controls.Add(this.ScriptsCheckBox);
			this.Controls.Add(this.MutationsCheckBox);
			this.Controls.Add(this.VersusCheckBox);
			this.Controls.Add(this.SinglePlayerCheckBox);
			this.Controls.Add(this.CoopCheckBox);
			this.Controls.Add(this.SurvivalCheckBox);
			this.Controls.Add(this.CampaignsCheckBox);
			this.Name = "Left4Dead2TagsUserControl";
			this.Size = new System.Drawing.Size(808, 304);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		internal CheckBoxEx CampaignsCheckBox;
		internal CheckBoxEx SurvivalCheckBox;
		internal CheckBoxEx CoopCheckBox;
		internal CheckBoxEx SinglePlayerCheckBox;
		internal CheckBoxEx VersusCheckBox;
		internal CheckBoxEx MutationsCheckBox;
		internal CheckBoxEx ScriptsCheckBox;
		internal CheckBoxEx WeaponsCheckBox;
		internal CheckBoxEx ItemsCheckBox;
		internal CheckBoxEx SoundsCheckBox;
		internal CheckBoxEx MiscellaneousCheckBox;
		internal CheckBoxEx UICheckBox;
		internal CheckBoxEx BillCheckBox;
		internal CheckBoxEx ScavengeCheckBox;
		internal CheckBoxEx FrancisCheckBox;
		internal CheckBoxEx LouisCheckBox;
		internal CheckBoxEx ZoeyCheckBox;
		internal CheckBoxEx CoachCheckBox;
		internal CheckBoxEx EllisCheckBox;
		internal CheckBoxEx NickCheckBox;
		internal CheckBoxEx RochelleCheckBox;
		internal CheckBoxEx WitchCheckBox;
		internal CheckBoxEx TankCheckBox;
		internal CheckBoxEx SpitterCheckBox;
		internal CheckBoxEx SmokerCheckBox;
		internal CheckBoxEx JockeyCheckBox;
		internal CheckBoxEx HunterCheckBox;
		internal CheckBoxEx ChargerCheckBox;
		internal CheckBoxEx BoomerCheckBox;
		internal CheckBoxEx CommonInfectedCheckBox;
		internal Label Label1;
		internal Label Label2;
		internal Label Label3;
		internal Label Label4;
		internal Label Label5;
		internal Label Label6;
		internal CheckBoxEx SpecialInfectedCheckBox;
		internal CheckBoxEx ModelsCheckBox;
		internal CheckBoxEx TexturesCheckBox;
		internal CheckBoxEx RealismCheckBox;
		internal CheckBoxEx RealismVersusCheckBox;
		internal CheckBoxEx GrenadeLauncherCheckBox;
		internal CheckBoxEx M60CheckBox;
		internal CheckBoxEx MeleeCheckBox;
		internal CheckBoxEx PistolCheckBox;
		internal CheckBoxEx RifleCheckBox;
		internal CheckBoxEx ShotgunCheckBox;
		internal CheckBoxEx SMGCheckBox;
		internal CheckBoxEx SniperCheckBox;
		internal CheckBoxEx ThrowableCheckBox;
		internal CheckBoxEx AdrenalineCheckBox;
		internal CheckBoxEx DefibrillatorCheckBox;
		internal CheckBoxEx MedkitCheckBox;
		internal CheckBoxEx OtherCheckBox;
		internal CheckBoxEx PillsCheckBox;
		internal CheckBoxEx SurvivorsCheckBox;
	}

}