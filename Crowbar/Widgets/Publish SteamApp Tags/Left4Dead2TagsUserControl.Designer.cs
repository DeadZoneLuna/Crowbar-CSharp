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
			CampaignsCheckBox = new Crowbar.CheckBoxEx();
			SurvivalCheckBox = new Crowbar.CheckBoxEx();
			CoopCheckBox = new Crowbar.CheckBoxEx();
			SinglePlayerCheckBox = new Crowbar.CheckBoxEx();
			VersusCheckBox = new Crowbar.CheckBoxEx();
			MutationsCheckBox = new Crowbar.CheckBoxEx();
			ScriptsCheckBox = new Crowbar.CheckBoxEx();
			WeaponsCheckBox = new Crowbar.CheckBoxEx();
			ItemsCheckBox = new Crowbar.CheckBoxEx();
			SoundsCheckBox = new Crowbar.CheckBoxEx();
			MiscellaneousCheckBox = new Crowbar.CheckBoxEx();
			UICheckBox = new Crowbar.CheckBoxEx();
			BillCheckBox = new Crowbar.CheckBoxEx();
			ScavengeCheckBox = new Crowbar.CheckBoxEx();
			FrancisCheckBox = new Crowbar.CheckBoxEx();
			LouisCheckBox = new Crowbar.CheckBoxEx();
			ZoeyCheckBox = new Crowbar.CheckBoxEx();
			CoachCheckBox = new Crowbar.CheckBoxEx();
			EllisCheckBox = new Crowbar.CheckBoxEx();
			NickCheckBox = new Crowbar.CheckBoxEx();
			RochelleCheckBox = new Crowbar.CheckBoxEx();
			WitchCheckBox = new Crowbar.CheckBoxEx();
			TankCheckBox = new Crowbar.CheckBoxEx();
			SpitterCheckBox = new Crowbar.CheckBoxEx();
			SmokerCheckBox = new Crowbar.CheckBoxEx();
			JockeyCheckBox = new Crowbar.CheckBoxEx();
			HunterCheckBox = new Crowbar.CheckBoxEx();
			ChargerCheckBox = new Crowbar.CheckBoxEx();
			BoomerCheckBox = new Crowbar.CheckBoxEx();
			CommonInfectedCheckBox = new Crowbar.CheckBoxEx();
			Label1 = new System.Windows.Forms.Label();
			Label2 = new System.Windows.Forms.Label();
			Label3 = new System.Windows.Forms.Label();
			Label4 = new System.Windows.Forms.Label();
			Label5 = new System.Windows.Forms.Label();
			Label6 = new System.Windows.Forms.Label();
			SpecialInfectedCheckBox = new Crowbar.CheckBoxEx();
			ModelsCheckBox = new Crowbar.CheckBoxEx();
			TexturesCheckBox = new Crowbar.CheckBoxEx();
			RealismCheckBox = new Crowbar.CheckBoxEx();
			RealismVersusCheckBox = new Crowbar.CheckBoxEx();
			GrenadeLauncherCheckBox = new Crowbar.CheckBoxEx();
			M60CheckBox = new Crowbar.CheckBoxEx();
			MeleeCheckBox = new Crowbar.CheckBoxEx();
			PistolCheckBox = new Crowbar.CheckBoxEx();
			RifleCheckBox = new Crowbar.CheckBoxEx();
			ShotgunCheckBox = new Crowbar.CheckBoxEx();
			SMGCheckBox = new Crowbar.CheckBoxEx();
			SniperCheckBox = new Crowbar.CheckBoxEx();
			ThrowableCheckBox = new Crowbar.CheckBoxEx();
			AdrenalineCheckBox = new Crowbar.CheckBoxEx();
			DefibrillatorCheckBox = new Crowbar.CheckBoxEx();
			MedkitCheckBox = new Crowbar.CheckBoxEx();
			OtherCheckBox = new Crowbar.CheckBoxEx();
			PillsCheckBox = new Crowbar.CheckBoxEx();
			SurvivorsCheckBox = new Crowbar.CheckBoxEx();
			SuspendLayout();
			//
			//CampaignsCheckBox
			//
			CampaignsCheckBox.AutoSize = true;
			CampaignsCheckBox.IsReadOnly = false;
			CampaignsCheckBox.Location = new System.Drawing.Point(196, 16);
			CampaignsCheckBox.Name = "CampaignsCheckBox";
			CampaignsCheckBox.Size = new System.Drawing.Size(78, 17);
			CampaignsCheckBox.TabIndex = 22;
			CampaignsCheckBox.Tag = "Campaigns";
			CampaignsCheckBox.Text = "Campaigns";
			CampaignsCheckBox.UseVisualStyleBackColor = true;
			//
			//SurvivalCheckBox
			//
			SurvivalCheckBox.AutoSize = true;
			SurvivalCheckBox.IsReadOnly = false;
			SurvivalCheckBox.Location = new System.Drawing.Point(293, 154);
			SurvivalCheckBox.Name = "SurvivalCheckBox";
			SurvivalCheckBox.Size = new System.Drawing.Size(64, 17);
			SurvivalCheckBox.TabIndex = 36;
			SurvivalCheckBox.Tag = "Survival";
			SurvivalCheckBox.Text = "Survival";
			SurvivalCheckBox.UseVisualStyleBackColor = true;
			//
			//CoopCheckBox
			//
			CoopCheckBox.AutoSize = true;
			CoopCheckBox.IsReadOnly = false;
			CoopCheckBox.Location = new System.Drawing.Point(293, 16);
			CoopCheckBox.Name = "CoopCheckBox";
			CoopCheckBox.Size = new System.Drawing.Size(55, 17);
			CoopCheckBox.TabIndex = 30;
			CoopCheckBox.Tag = "Co-op";
			CoopCheckBox.Text = "Co-op";
			CoopCheckBox.UseVisualStyleBackColor = true;
			//
			//SinglePlayerCheckBox
			//
			SinglePlayerCheckBox.AutoSize = true;
			SinglePlayerCheckBox.IsReadOnly = false;
			SinglePlayerCheckBox.Location = new System.Drawing.Point(293, 131);
			SinglePlayerCheckBox.Name = "SinglePlayerCheckBox";
			SinglePlayerCheckBox.Size = new System.Drawing.Size(87, 17);
			SinglePlayerCheckBox.TabIndex = 35;
			SinglePlayerCheckBox.Tag = "Single Player";
			SinglePlayerCheckBox.Text = "Single Player";
			SinglePlayerCheckBox.UseVisualStyleBackColor = true;
			//
			//VersusCheckBox
			//
			VersusCheckBox.AutoSize = true;
			VersusCheckBox.IsReadOnly = false;
			VersusCheckBox.Location = new System.Drawing.Point(293, 177);
			VersusCheckBox.Name = "VersusCheckBox";
			VersusCheckBox.Size = new System.Drawing.Size(58, 17);
			VersusCheckBox.TabIndex = 37;
			VersusCheckBox.Tag = "Versus";
			VersusCheckBox.Text = "Versus";
			VersusCheckBox.UseVisualStyleBackColor = true;
			//
			//MutationsCheckBox
			//
			MutationsCheckBox.AutoSize = true;
			MutationsCheckBox.IsReadOnly = false;
			MutationsCheckBox.Location = new System.Drawing.Point(293, 39);
			MutationsCheckBox.Name = "MutationsCheckBox";
			MutationsCheckBox.Size = new System.Drawing.Size(73, 17);
			MutationsCheckBox.TabIndex = 31;
			MutationsCheckBox.Tag = "Mutations";
			MutationsCheckBox.Text = "Mutations";
			MutationsCheckBox.UseVisualStyleBackColor = true;
			//
			//ScriptsCheckBox
			//
			ScriptsCheckBox.AutoSize = true;
			ScriptsCheckBox.IsReadOnly = false;
			ScriptsCheckBox.Location = new System.Drawing.Point(196, 85);
			ScriptsCheckBox.Name = "ScriptsCheckBox";
			ScriptsCheckBox.Size = new System.Drawing.Size(58, 17);
			ScriptsCheckBox.TabIndex = 25;
			ScriptsCheckBox.Tag = "Scripts";
			ScriptsCheckBox.Text = "Scripts";
			ScriptsCheckBox.UseVisualStyleBackColor = true;
			//
			//WeaponsCheckBox
			//
			WeaponsCheckBox.AutoSize = true;
			WeaponsCheckBox.IsReadOnly = false;
			WeaponsCheckBox.Location = new System.Drawing.Point(399, 16);
			WeaponsCheckBox.Name = "WeaponsCheckBox";
			WeaponsCheckBox.Size = new System.Drawing.Size(71, 17);
			WeaponsCheckBox.TabIndex = 39;
			WeaponsCheckBox.Tag = "Weapons";
			WeaponsCheckBox.Text = "Weapons";
			WeaponsCheckBox.UseVisualStyleBackColor = true;
			//
			//ItemsCheckBox
			//
			ItemsCheckBox.AutoSize = true;
			ItemsCheckBox.IsReadOnly = false;
			ItemsCheckBox.Location = new System.Drawing.Point(519, 16);
			ItemsCheckBox.Name = "ItemsCheckBox";
			ItemsCheckBox.Size = new System.Drawing.Size(53, 17);
			ItemsCheckBox.TabIndex = 50;
			ItemsCheckBox.Tag = "Items";
			ItemsCheckBox.Text = "Items";
			ItemsCheckBox.UseVisualStyleBackColor = true;
			//
			//SoundsCheckBox
			//
			SoundsCheckBox.AutoSize = true;
			SoundsCheckBox.IsReadOnly = false;
			SoundsCheckBox.Location = new System.Drawing.Point(196, 108);
			SoundsCheckBox.Name = "SoundsCheckBox";
			SoundsCheckBox.Size = new System.Drawing.Size(61, 17);
			SoundsCheckBox.TabIndex = 26;
			SoundsCheckBox.Tag = "Sounds";
			SoundsCheckBox.Text = "Sounds";
			SoundsCheckBox.UseVisualStyleBackColor = true;
			//
			//MiscellaneousCheckBox
			//
			MiscellaneousCheckBox.AutoSize = true;
			MiscellaneousCheckBox.IsReadOnly = false;
			MiscellaneousCheckBox.Location = new System.Drawing.Point(196, 39);
			MiscellaneousCheckBox.Name = "MiscellaneousCheckBox";
			MiscellaneousCheckBox.Size = new System.Drawing.Size(91, 17);
			MiscellaneousCheckBox.TabIndex = 23;
			MiscellaneousCheckBox.Tag = "Miscellaneous";
			MiscellaneousCheckBox.Text = "Miscellaneous";
			MiscellaneousCheckBox.UseVisualStyleBackColor = true;
			//
			//UICheckBox
			//
			UICheckBox.AutoSize = true;
			UICheckBox.IsReadOnly = false;
			UICheckBox.Location = new System.Drawing.Point(196, 154);
			UICheckBox.Name = "UICheckBox";
			UICheckBox.Size = new System.Drawing.Size(37, 17);
			UICheckBox.TabIndex = 28;
			UICheckBox.Tag = "UI";
			UICheckBox.Text = "UI";
			UICheckBox.UseVisualStyleBackColor = true;
			//
			//BillCheckBox
			//
			BillCheckBox.AutoSize = true;
			BillCheckBox.IsReadOnly = false;
			BillCheckBox.Location = new System.Drawing.Point(3, 39);
			BillCheckBox.Name = "BillCheckBox";
			BillCheckBox.Size = new System.Drawing.Size(38, 17);
			BillCheckBox.TabIndex = 2;
			BillCheckBox.Tag = "Bill";
			BillCheckBox.Text = "Bill";
			BillCheckBox.UseVisualStyleBackColor = true;
			//
			//ScavengeCheckBox
			//
			ScavengeCheckBox.AutoSize = true;
			ScavengeCheckBox.IsReadOnly = false;
			ScavengeCheckBox.Location = new System.Drawing.Point(293, 108);
			ScavengeCheckBox.Name = "ScavengeCheckBox";
			ScavengeCheckBox.Size = new System.Drawing.Size(73, 17);
			ScavengeCheckBox.TabIndex = 34;
			ScavengeCheckBox.Tag = "Scavenge";
			ScavengeCheckBox.Text = "Scavenge";
			ScavengeCheckBox.UseVisualStyleBackColor = true;
			//
			//FrancisCheckBox
			//
			FrancisCheckBox.AutoSize = true;
			FrancisCheckBox.IsReadOnly = false;
			FrancisCheckBox.Location = new System.Drawing.Point(3, 62);
			FrancisCheckBox.Name = "FrancisCheckBox";
			FrancisCheckBox.Size = new System.Drawing.Size(60, 17);
			FrancisCheckBox.TabIndex = 3;
			FrancisCheckBox.Tag = "Francis";
			FrancisCheckBox.Text = "Francis";
			FrancisCheckBox.UseVisualStyleBackColor = true;
			//
			//LouisCheckBox
			//
			LouisCheckBox.AutoSize = true;
			LouisCheckBox.IsReadOnly = false;
			LouisCheckBox.Location = new System.Drawing.Point(3, 85);
			LouisCheckBox.Name = "LouisCheckBox";
			LouisCheckBox.Size = new System.Drawing.Size(50, 17);
			LouisCheckBox.TabIndex = 4;
			LouisCheckBox.Tag = "Louis";
			LouisCheckBox.Text = "Louis";
			LouisCheckBox.UseVisualStyleBackColor = true;
			//
			//ZoeyCheckBox
			//
			ZoeyCheckBox.AutoSize = true;
			ZoeyCheckBox.IsReadOnly = false;
			ZoeyCheckBox.Location = new System.Drawing.Point(3, 108);
			ZoeyCheckBox.Name = "ZoeyCheckBox";
			ZoeyCheckBox.Size = new System.Drawing.Size(50, 17);
			ZoeyCheckBox.TabIndex = 5;
			ZoeyCheckBox.Tag = "Zoey";
			ZoeyCheckBox.Text = "Zoey";
			ZoeyCheckBox.UseVisualStyleBackColor = true;
			//
			//CoachCheckBox
			//
			CoachCheckBox.AutoSize = true;
			CoachCheckBox.IsReadOnly = false;
			CoachCheckBox.Location = new System.Drawing.Point(3, 131);
			CoachCheckBox.Name = "CoachCheckBox";
			CoachCheckBox.Size = new System.Drawing.Size(56, 17);
			CoachCheckBox.TabIndex = 6;
			CoachCheckBox.Tag = "Coach";
			CoachCheckBox.Text = "Coach";
			CoachCheckBox.UseVisualStyleBackColor = true;
			//
			//EllisCheckBox
			//
			EllisCheckBox.AutoSize = true;
			EllisCheckBox.IsReadOnly = false;
			EllisCheckBox.Location = new System.Drawing.Point(3, 154);
			EllisCheckBox.Name = "EllisCheckBox";
			EllisCheckBox.Size = new System.Drawing.Size(43, 17);
			EllisCheckBox.TabIndex = 7;
			EllisCheckBox.Tag = "Ellis";
			EllisCheckBox.Text = "Ellis";
			EllisCheckBox.UseVisualStyleBackColor = true;
			//
			//NickCheckBox
			//
			NickCheckBox.AutoSize = true;
			NickCheckBox.IsReadOnly = false;
			NickCheckBox.Location = new System.Drawing.Point(3, 177);
			NickCheckBox.Name = "NickCheckBox";
			NickCheckBox.Size = new System.Drawing.Size(45, 17);
			NickCheckBox.TabIndex = 8;
			NickCheckBox.Tag = "Nick";
			NickCheckBox.Text = "Nick";
			NickCheckBox.UseVisualStyleBackColor = true;
			//
			//RochelleCheckBox
			//
			RochelleCheckBox.AutoSize = true;
			RochelleCheckBox.IsReadOnly = false;
			RochelleCheckBox.Location = new System.Drawing.Point(3, 200);
			RochelleCheckBox.Name = "RochelleCheckBox";
			RochelleCheckBox.Size = new System.Drawing.Size(66, 17);
			RochelleCheckBox.TabIndex = 9;
			RochelleCheckBox.Tag = "Rochelle";
			RochelleCheckBox.Text = "Rochelle";
			RochelleCheckBox.UseVisualStyleBackColor = true;
			//
			//WitchCheckBox
			//
			WitchCheckBox.AutoSize = true;
			WitchCheckBox.IsReadOnly = false;
			WitchCheckBox.Location = new System.Drawing.Point(76, 223);
			WitchCheckBox.Name = "WitchCheckBox";
			WitchCheckBox.Size = new System.Drawing.Size(53, 17);
			WitchCheckBox.TabIndex = 20;
			WitchCheckBox.Tag = "Witch";
			WitchCheckBox.Text = "Witch";
			WitchCheckBox.UseVisualStyleBackColor = true;
			//
			//TankCheckBox
			//
			TankCheckBox.AutoSize = true;
			TankCheckBox.IsReadOnly = false;
			TankCheckBox.Location = new System.Drawing.Point(76, 200);
			TankCheckBox.Name = "TankCheckBox";
			TankCheckBox.Size = new System.Drawing.Size(49, 17);
			TankCheckBox.TabIndex = 19;
			TankCheckBox.Tag = "Tank";
			TankCheckBox.Text = "Tank";
			TankCheckBox.UseVisualStyleBackColor = true;
			//
			//SpitterCheckBox
			//
			SpitterCheckBox.AutoSize = true;
			SpitterCheckBox.IsReadOnly = false;
			SpitterCheckBox.Location = new System.Drawing.Point(76, 177);
			SpitterCheckBox.Name = "SpitterCheckBox";
			SpitterCheckBox.Size = new System.Drawing.Size(58, 17);
			SpitterCheckBox.TabIndex = 18;
			SpitterCheckBox.Tag = "Spitter";
			SpitterCheckBox.Text = "Spitter";
			SpitterCheckBox.UseVisualStyleBackColor = true;
			//
			//SmokerCheckBox
			//
			SmokerCheckBox.AutoSize = true;
			SmokerCheckBox.IsReadOnly = false;
			SmokerCheckBox.Location = new System.Drawing.Point(76, 154);
			SmokerCheckBox.Name = "SmokerCheckBox";
			SmokerCheckBox.Size = new System.Drawing.Size(61, 17);
			SmokerCheckBox.TabIndex = 17;
			SmokerCheckBox.Tag = "Smoker";
			SmokerCheckBox.Text = "Smoker";
			SmokerCheckBox.UseVisualStyleBackColor = true;
			//
			//JockeyCheckBox
			//
			JockeyCheckBox.AutoSize = true;
			JockeyCheckBox.IsReadOnly = false;
			JockeyCheckBox.Location = new System.Drawing.Point(76, 131);
			JockeyCheckBox.Name = "JockeyCheckBox";
			JockeyCheckBox.Size = new System.Drawing.Size(59, 17);
			JockeyCheckBox.TabIndex = 16;
			JockeyCheckBox.Tag = "Jockey";
			JockeyCheckBox.Text = "Jockey";
			JockeyCheckBox.UseVisualStyleBackColor = true;
			//
			//HunterCheckBox
			//
			HunterCheckBox.AutoSize = true;
			HunterCheckBox.IsReadOnly = false;
			HunterCheckBox.Location = new System.Drawing.Point(76, 108);
			HunterCheckBox.Name = "HunterCheckBox";
			HunterCheckBox.Size = new System.Drawing.Size(59, 17);
			HunterCheckBox.TabIndex = 15;
			HunterCheckBox.Tag = "Hunter";
			HunterCheckBox.Text = "Hunter";
			HunterCheckBox.UseVisualStyleBackColor = true;
			//
			//ChargerCheckBox
			//
			ChargerCheckBox.AutoSize = true;
			ChargerCheckBox.IsReadOnly = false;
			ChargerCheckBox.Location = new System.Drawing.Point(76, 85);
			ChargerCheckBox.Name = "ChargerCheckBox";
			ChargerCheckBox.Size = new System.Drawing.Size(65, 17);
			ChargerCheckBox.TabIndex = 14;
			ChargerCheckBox.Tag = "Charger";
			ChargerCheckBox.Text = "Charger";
			ChargerCheckBox.UseVisualStyleBackColor = true;
			//
			//BoomerCheckBox
			//
			BoomerCheckBox.AutoSize = true;
			BoomerCheckBox.IsReadOnly = false;
			BoomerCheckBox.Location = new System.Drawing.Point(76, 62);
			BoomerCheckBox.Name = "BoomerCheckBox";
			BoomerCheckBox.Size = new System.Drawing.Size(62, 17);
			BoomerCheckBox.TabIndex = 13;
			BoomerCheckBox.Tag = "Boomer";
			BoomerCheckBox.Text = "Boomer";
			BoomerCheckBox.UseVisualStyleBackColor = true;
			//
			//CommonInfectedCheckBox
			//
			CommonInfectedCheckBox.AutoSize = true;
			CommonInfectedCheckBox.IsReadOnly = false;
			CommonInfectedCheckBox.Location = new System.Drawing.Point(76, 16);
			CommonInfectedCheckBox.Name = "CommonInfectedCheckBox";
			CommonInfectedCheckBox.Size = new System.Drawing.Size(111, 17);
			CommonInfectedCheckBox.TabIndex = 11;
			CommonInfectedCheckBox.Tag = "Common Infected";
			CommonInfectedCheckBox.Text = "Common Infected";
			CommonInfectedCheckBox.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(1, 0);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(64, 13);
			Label1.TabIndex = 0;
			Label1.Text = "SURVIVORS";
			//
			//Label2
			//
			Label2.AutoSize = true;
			Label2.Location = new System.Drawing.Point(73, 0);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(56, 13);
			Label2.TabIndex = 10;
			Label2.Text = "INFECTED";
			//
			//Label3
			//
			Label3.AutoSize = true;
			Label3.Location = new System.Drawing.Point(193, 0);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(85, 13);
			Label3.TabIndex = 21;
			Label3.Text = "GAME CONTENT";
			//
			//Label4
			//
			Label4.AutoSize = true;
			Label4.Location = new System.Drawing.Point(290, 0);
			Label4.Name = "Label4";
			Label4.Size = new System.Drawing.Size(73, 13);
			Label4.TabIndex = 29;
			Label4.Text = "GAME MODES";
			//
			//Label5
			//
			Label5.AutoSize = true;
			Label5.Location = new System.Drawing.Point(396, 0);
			Label5.Name = "Label5";
			Label5.Size = new System.Drawing.Size(57, 13);
			Label5.TabIndex = 38;
			Label5.Text = "WEAPONS";
			//
			//Label6
			//
			Label6.AutoSize = true;
			Label6.Location = new System.Drawing.Point(516, 0);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(37, 13);
			Label6.TabIndex = 49;
			Label6.Text = "ITEMS";
			//
			//SpecialInfectedCheckBox
			//
			SpecialInfectedCheckBox.AutoSize = true;
			SpecialInfectedCheckBox.IsReadOnly = false;
			SpecialInfectedCheckBox.Location = new System.Drawing.Point(76, 39);
			SpecialInfectedCheckBox.Name = "SpecialInfectedCheckBox";
			SpecialInfectedCheckBox.Size = new System.Drawing.Size(103, 17);
			SpecialInfectedCheckBox.TabIndex = 12;
			SpecialInfectedCheckBox.Tag = "Special Infected";
			SpecialInfectedCheckBox.Text = "Special Infected";
			SpecialInfectedCheckBox.UseVisualStyleBackColor = true;
			//
			//ModelsCheckBox
			//
			ModelsCheckBox.AutoSize = true;
			ModelsCheckBox.IsReadOnly = false;
			ModelsCheckBox.Location = new System.Drawing.Point(196, 62);
			ModelsCheckBox.Name = "ModelsCheckBox";
			ModelsCheckBox.Size = new System.Drawing.Size(59, 17);
			ModelsCheckBox.TabIndex = 24;
			ModelsCheckBox.Tag = "Models";
			ModelsCheckBox.Text = "Models";
			ModelsCheckBox.UseVisualStyleBackColor = true;
			//
			//TexturesCheckBox
			//
			TexturesCheckBox.AutoSize = true;
			TexturesCheckBox.IsReadOnly = false;
			TexturesCheckBox.Location = new System.Drawing.Point(196, 131);
			TexturesCheckBox.Name = "TexturesCheckBox";
			TexturesCheckBox.Size = new System.Drawing.Size(69, 17);
			TexturesCheckBox.TabIndex = 27;
			TexturesCheckBox.Tag = "Textures";
			TexturesCheckBox.Text = "Textures";
			TexturesCheckBox.UseVisualStyleBackColor = true;
			//
			//RealismCheckBox
			//
			RealismCheckBox.AutoSize = true;
			RealismCheckBox.IsReadOnly = false;
			RealismCheckBox.Location = new System.Drawing.Point(293, 62);
			RealismCheckBox.Name = "RealismCheckBox";
			RealismCheckBox.Size = new System.Drawing.Size(62, 17);
			RealismCheckBox.TabIndex = 32;
			RealismCheckBox.Tag = "Realism";
			RealismCheckBox.Text = "Realism";
			RealismCheckBox.UseVisualStyleBackColor = true;
			//
			//RealismVersusCheckBox
			//
			RealismVersusCheckBox.AutoSize = true;
			RealismVersusCheckBox.IsReadOnly = false;
			RealismVersusCheckBox.Location = new System.Drawing.Point(293, 85);
			RealismVersusCheckBox.Name = "RealismVersusCheckBox";
			RealismVersusCheckBox.Size = new System.Drawing.Size(97, 17);
			RealismVersusCheckBox.TabIndex = 33;
			RealismVersusCheckBox.Tag = "Realism Versus";
			RealismVersusCheckBox.Text = "Realism Versus";
			RealismVersusCheckBox.UseVisualStyleBackColor = true;
			//
			//GrenadeLauncherCheckBox
			//
			GrenadeLauncherCheckBox.AutoSize = true;
			GrenadeLauncherCheckBox.IsReadOnly = false;
			GrenadeLauncherCheckBox.Location = new System.Drawing.Point(399, 39);
			GrenadeLauncherCheckBox.Name = "GrenadeLauncherCheckBox";
			GrenadeLauncherCheckBox.Size = new System.Drawing.Size(114, 17);
			GrenadeLauncherCheckBox.TabIndex = 40;
			GrenadeLauncherCheckBox.Tag = "Grenade Launcher";
			GrenadeLauncherCheckBox.Text = "Grenade Launcher";
			GrenadeLauncherCheckBox.UseVisualStyleBackColor = true;
			//
			//M60CheckBox
			//
			M60CheckBox.AutoSize = true;
			M60CheckBox.IsReadOnly = false;
			M60CheckBox.Location = new System.Drawing.Point(399, 62);
			M60CheckBox.Name = "M60CheckBox";
			M60CheckBox.Size = new System.Drawing.Size(46, 17);
			M60CheckBox.TabIndex = 41;
			M60CheckBox.Tag = "M60";
			M60CheckBox.Text = "M60";
			M60CheckBox.UseVisualStyleBackColor = true;
			//
			//MeleeCheckBox
			//
			MeleeCheckBox.AutoSize = true;
			MeleeCheckBox.IsReadOnly = false;
			MeleeCheckBox.Location = new System.Drawing.Point(399, 85);
			MeleeCheckBox.Name = "MeleeCheckBox";
			MeleeCheckBox.Size = new System.Drawing.Size(54, 17);
			MeleeCheckBox.TabIndex = 42;
			MeleeCheckBox.Tag = "Melee";
			MeleeCheckBox.Text = "Melee";
			MeleeCheckBox.UseVisualStyleBackColor = true;
			//
			//PistolCheckBox
			//
			PistolCheckBox.AutoSize = true;
			PistolCheckBox.IsReadOnly = false;
			PistolCheckBox.Location = new System.Drawing.Point(399, 108);
			PistolCheckBox.Name = "PistolCheckBox";
			PistolCheckBox.Size = new System.Drawing.Size(51, 17);
			PistolCheckBox.TabIndex = 43;
			PistolCheckBox.Tag = "Pistol";
			PistolCheckBox.Text = "Pistol";
			PistolCheckBox.UseVisualStyleBackColor = true;
			//
			//RifleCheckBox
			//
			RifleCheckBox.AutoSize = true;
			RifleCheckBox.IsReadOnly = false;
			RifleCheckBox.Location = new System.Drawing.Point(399, 131);
			RifleCheckBox.Name = "RifleCheckBox";
			RifleCheckBox.Size = new System.Drawing.Size(47, 17);
			RifleCheckBox.TabIndex = 44;
			RifleCheckBox.Tag = "Rifle";
			RifleCheckBox.Text = "Rifle";
			RifleCheckBox.UseVisualStyleBackColor = true;
			//
			//ShotgunCheckBox
			//
			ShotgunCheckBox.AutoSize = true;
			ShotgunCheckBox.IsReadOnly = false;
			ShotgunCheckBox.Location = new System.Drawing.Point(399, 154);
			ShotgunCheckBox.Name = "ShotgunCheckBox";
			ShotgunCheckBox.Size = new System.Drawing.Size(66, 17);
			ShotgunCheckBox.TabIndex = 45;
			ShotgunCheckBox.Tag = "Shotgun";
			ShotgunCheckBox.Text = "Shotgun";
			ShotgunCheckBox.UseVisualStyleBackColor = true;
			//
			//SMGCheckBox
			//
			SMGCheckBox.AutoSize = true;
			SMGCheckBox.IsReadOnly = false;
			SMGCheckBox.Location = new System.Drawing.Point(399, 177);
			SMGCheckBox.Name = "SMGCheckBox";
			SMGCheckBox.Size = new System.Drawing.Size(47, 17);
			SMGCheckBox.TabIndex = 46;
			SMGCheckBox.Tag = "SMG";
			SMGCheckBox.Text = "SMG";
			SMGCheckBox.UseVisualStyleBackColor = true;
			//
			//SniperCheckBox
			//
			SniperCheckBox.AutoSize = true;
			SniperCheckBox.IsReadOnly = false;
			SniperCheckBox.Location = new System.Drawing.Point(399, 200);
			SniperCheckBox.Name = "SniperCheckBox";
			SniperCheckBox.Size = new System.Drawing.Size(56, 17);
			SniperCheckBox.TabIndex = 47;
			SniperCheckBox.Tag = "Sniper";
			SniperCheckBox.Text = "Sniper";
			SniperCheckBox.UseVisualStyleBackColor = true;
			//
			//ThrowableCheckBox
			//
			ThrowableCheckBox.AutoSize = true;
			ThrowableCheckBox.IsReadOnly = false;
			ThrowableCheckBox.Location = new System.Drawing.Point(399, 223);
			ThrowableCheckBox.Name = "ThrowableCheckBox";
			ThrowableCheckBox.Size = new System.Drawing.Size(76, 17);
			ThrowableCheckBox.TabIndex = 48;
			ThrowableCheckBox.Tag = "Throwable";
			ThrowableCheckBox.Text = "Throwable";
			ThrowableCheckBox.UseVisualStyleBackColor = true;
			//
			//AdrenalineCheckBox
			//
			AdrenalineCheckBox.AutoSize = true;
			AdrenalineCheckBox.IsReadOnly = false;
			AdrenalineCheckBox.Location = new System.Drawing.Point(519, 39);
			AdrenalineCheckBox.Name = "AdrenalineCheckBox";
			AdrenalineCheckBox.Size = new System.Drawing.Size(77, 17);
			AdrenalineCheckBox.TabIndex = 51;
			AdrenalineCheckBox.Tag = "Adrenaline";
			AdrenalineCheckBox.Text = "Adrenaline";
			AdrenalineCheckBox.UseVisualStyleBackColor = true;
			//
			//DefibrillatorCheckBox
			//
			DefibrillatorCheckBox.AutoSize = true;
			DefibrillatorCheckBox.IsReadOnly = false;
			DefibrillatorCheckBox.Location = new System.Drawing.Point(519, 62);
			DefibrillatorCheckBox.Name = "DefibrillatorCheckBox";
			DefibrillatorCheckBox.Size = new System.Drawing.Size(81, 17);
			DefibrillatorCheckBox.TabIndex = 52;
			DefibrillatorCheckBox.Tag = "Defibrillator";
			DefibrillatorCheckBox.Text = "Defibrillator";
			DefibrillatorCheckBox.UseVisualStyleBackColor = true;
			//
			//MedkitCheckBox
			//
			MedkitCheckBox.AutoSize = true;
			MedkitCheckBox.IsReadOnly = false;
			MedkitCheckBox.Location = new System.Drawing.Point(519, 85);
			MedkitCheckBox.Name = "MedkitCheckBox";
			MedkitCheckBox.Size = new System.Drawing.Size(57, 17);
			MedkitCheckBox.TabIndex = 53;
			MedkitCheckBox.Tag = "Medkit";
			MedkitCheckBox.Text = "Medkit";
			MedkitCheckBox.UseVisualStyleBackColor = true;
			//
			//OtherCheckBox
			//
			OtherCheckBox.AutoSize = true;
			OtherCheckBox.IsReadOnly = false;
			OtherCheckBox.Location = new System.Drawing.Point(519, 108);
			OtherCheckBox.Name = "OtherCheckBox";
			OtherCheckBox.Size = new System.Drawing.Size(54, 17);
			OtherCheckBox.TabIndex = 54;
			OtherCheckBox.Tag = "Other";
			OtherCheckBox.Text = "Other";
			OtherCheckBox.UseVisualStyleBackColor = true;
			//
			//PillsCheckBox
			//
			PillsCheckBox.AutoSize = true;
			PillsCheckBox.IsReadOnly = false;
			PillsCheckBox.Location = new System.Drawing.Point(519, 131);
			PillsCheckBox.Name = "PillsCheckBox";
			PillsCheckBox.Size = new System.Drawing.Size(43, 17);
			PillsCheckBox.TabIndex = 55;
			PillsCheckBox.Tag = "Pills";
			PillsCheckBox.Text = "Pills";
			PillsCheckBox.UseVisualStyleBackColor = true;
			//
			//SurvivorsCheckBox
			//
			SurvivorsCheckBox.AutoSize = true;
			SurvivorsCheckBox.IsReadOnly = false;
			SurvivorsCheckBox.Location = new System.Drawing.Point(3, 16);
			SurvivorsCheckBox.Name = "SurvivorsCheckBox";
			SurvivorsCheckBox.Size = new System.Drawing.Size(71, 17);
			SurvivorsCheckBox.TabIndex = 1;
			SurvivorsCheckBox.Tag = "Survivors";
			SurvivorsCheckBox.Text = "Survivors";
			SurvivorsCheckBox.UseVisualStyleBackColor = true;
			//
			//Left4Dead2TagsUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoScroll = true;
			Controls.Add(SurvivorsCheckBox);
			Controls.Add(PillsCheckBox);
			Controls.Add(OtherCheckBox);
			Controls.Add(MedkitCheckBox);
			Controls.Add(DefibrillatorCheckBox);
			Controls.Add(AdrenalineCheckBox);
			Controls.Add(ThrowableCheckBox);
			Controls.Add(SniperCheckBox);
			Controls.Add(SMGCheckBox);
			Controls.Add(ShotgunCheckBox);
			Controls.Add(RifleCheckBox);
			Controls.Add(PistolCheckBox);
			Controls.Add(MeleeCheckBox);
			Controls.Add(M60CheckBox);
			Controls.Add(GrenadeLauncherCheckBox);
			Controls.Add(RealismVersusCheckBox);
			Controls.Add(RealismCheckBox);
			Controls.Add(TexturesCheckBox);
			Controls.Add(ModelsCheckBox);
			Controls.Add(SpecialInfectedCheckBox);
			Controls.Add(Label6);
			Controls.Add(Label5);
			Controls.Add(Label4);
			Controls.Add(Label3);
			Controls.Add(Label2);
			Controls.Add(Label1);
			Controls.Add(CommonInfectedCheckBox);
			Controls.Add(WitchCheckBox);
			Controls.Add(TankCheckBox);
			Controls.Add(SpitterCheckBox);
			Controls.Add(SmokerCheckBox);
			Controls.Add(JockeyCheckBox);
			Controls.Add(HunterCheckBox);
			Controls.Add(ChargerCheckBox);
			Controls.Add(BoomerCheckBox);
			Controls.Add(RochelleCheckBox);
			Controls.Add(NickCheckBox);
			Controls.Add(EllisCheckBox);
			Controls.Add(CoachCheckBox);
			Controls.Add(ZoeyCheckBox);
			Controls.Add(LouisCheckBox);
			Controls.Add(FrancisCheckBox);
			Controls.Add(ScavengeCheckBox);
			Controls.Add(BillCheckBox);
			Controls.Add(UICheckBox);
			Controls.Add(MiscellaneousCheckBox);
			Controls.Add(SoundsCheckBox);
			Controls.Add(ItemsCheckBox);
			Controls.Add(WeaponsCheckBox);
			Controls.Add(ScriptsCheckBox);
			Controls.Add(MutationsCheckBox);
			Controls.Add(VersusCheckBox);
			Controls.Add(SinglePlayerCheckBox);
			Controls.Add(CoopCheckBox);
			Controls.Add(SurvivalCheckBox);
			Controls.Add(CampaignsCheckBox);
			Name = "Left4Dead2TagsUserControl";
			Size = new System.Drawing.Size(808, 304);
			ResumeLayout(false);
			PerformLayout();

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