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
	public partial class AboutUserControl : BaseUserControl
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
			this.ProductInfoTextBox = new System.Windows.Forms.TextBox();
			this.ProductDescriptionTextBox = new System.Windows.Forms.TextBox();
			this.ProductLogoButton = new System.Windows.Forms.Button();
			this.AuthorIconButton = new System.Windows.Forms.Button();
			this.CreditsTextBox = new System.Windows.Forms.TextBox();
			this.AuthorLinkLabel = new System.Windows.Forms.LinkLabel();
			this.ProductNameLinkLabel = new System.Windows.Forms.LinkLabel();
			this.Panel1 = new System.Windows.Forms.Panel();
			this.GotoSteamGroupLinkLabel = new System.Windows.Forms.LinkLabel();
			this.GotoSteamProfileLinkLabel = new System.Windows.Forms.LinkLabel();
			this.PayPalPictureBox = new System.Windows.Forms.PictureBox();
			this.SpecialThanksGroupBox = new System.Windows.Forms.GroupBox();
			this.Credits3TextBox = new System.Windows.Forms.TextBox();
			this.Credits2TextBox = new System.Windows.Forms.TextBox();
			this.Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.PayPalPictureBox).BeginInit();
			this.SpecialThanksGroupBox.SuspendLayout();
			this.SuspendLayout();
			//
			//ProductInfoTextBox
			//
			this.ProductInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ProductInfoTextBox.Location = new System.Drawing.Point(3, 183);
			this.ProductInfoTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ProductInfoTextBox.Multiline = true;
			this.ProductInfoTextBox.Name = "ProductInfoTextBox";
			this.ProductInfoTextBox.ReadOnly = true;
			this.ProductInfoTextBox.Size = new System.Drawing.Size(165, 48);
			this.ProductInfoTextBox.TabIndex = 3;
			this.ProductInfoTextBox.Text = "Version" + "\r" + "\n" + "Copyright" + "\r" + "\n" + "Company Name";
			this.ProductInfoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.ProductInfoTextBox.WordWrap = false;
			//
			//ProductDescriptionTextBox
			//
			this.ProductDescriptionTextBox.Location = new System.Drawing.Point(175, 3);
			this.ProductDescriptionTextBox.Multiline = true;
			this.ProductDescriptionTextBox.Name = "ProductDescriptionTextBox";
			this.ProductDescriptionTextBox.ReadOnly = true;
			this.ProductDescriptionTextBox.Size = new System.Drawing.Size(598, 136);
			this.ProductDescriptionTextBox.TabIndex = 7;
			this.ProductDescriptionTextBox.TabStop = false;
			//
			//ProductLogoButton
			//
			this.ProductLogoButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ProductLogoButton.Image = global::Crowbar.Properties.Resources.crowbar_icon_large;
			this.ProductLogoButton.Location = new System.Drawing.Point(21, 3);
			this.ProductLogoButton.Name = "ProductLogoButton";
			this.ProductLogoButton.Size = new System.Drawing.Size(128, 128);
			this.ProductLogoButton.TabIndex = 0;
			this.ProductLogoButton.UseVisualStyleBackColor = true;
			//
			//AuthorIconButton
			//
			this.AuthorIconButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.AuthorIconButton.Image = global::Crowbar.Properties.Resources.macaw;
			this.AuthorIconButton.Location = new System.Drawing.Point(21, 236);
			this.AuthorIconButton.Name = "AuthorIconButton";
			this.AuthorIconButton.Size = new System.Drawing.Size(128, 128);
			this.AuthorIconButton.TabIndex = 4;
			this.AuthorIconButton.UseVisualStyleBackColor = true;
			//
			//AuthorLinkLabel
			//
			this.AuthorLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen;
			this.AuthorLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.AuthorLinkLabel.LinkColor = System.Drawing.Color.Green;
			this.AuthorLinkLabel.Location = new System.Drawing.Point(3, 367);
			this.AuthorLinkLabel.Name = "AuthorLinkLabel";
			this.AuthorLinkLabel.Size = new System.Drawing.Size(165, 20);
			this.AuthorLinkLabel.TabIndex = 5;
			this.AuthorLinkLabel.TabStop = true;
			this.AuthorLinkLabel.Text = "Author";
			this.AuthorLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.AuthorLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//ProductNameLinkLabel
			//
			this.ProductNameLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen;
			this.ProductNameLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.ProductNameLinkLabel.LinkColor = System.Drawing.Color.Green;
			this.ProductNameLinkLabel.Location = new System.Drawing.Point(3, 134);
			this.ProductNameLinkLabel.Name = "ProductNameLinkLabel";
			this.ProductNameLinkLabel.Size = new System.Drawing.Size(165, 23);
			this.ProductNameLinkLabel.TabIndex = 1;
			this.ProductNameLinkLabel.TabStop = true;
			this.ProductNameLinkLabel.Text = "Product Name";
			this.ProductNameLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ProductNameLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//Panel1
			//
			this.Panel1.Controls.Add(this.ProductLogoButton);
			this.Panel1.Controls.Add(this.ProductNameLinkLabel);
			this.Panel1.Controls.Add(this.ProductInfoTextBox);
			this.Panel1.Controls.Add(this.GotoSteamGroupLinkLabel);
			this.Panel1.Controls.Add(this.AuthorIconButton);
			this.Panel1.Controls.Add(this.AuthorLinkLabel);
			this.Panel1.Controls.Add(this.GotoSteamProfileLinkLabel);
			this.Panel1.Controls.Add(this.PayPalPictureBox);
			this.Panel1.Controls.Add(this.ProductDescriptionTextBox);
			this.Panel1.Controls.Add(this.SpecialThanksGroupBox);
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel1.Location = new System.Drawing.Point(0, 0);
			this.Panel1.Margin = new System.Windows.Forms.Padding(2);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new System.Drawing.Size(776, 536);
			this.Panel1.TabIndex = 0;
			//
			//GotoSteamGroupLinkLabel
			//
			this.GotoSteamGroupLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen;
			this.GotoSteamGroupLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.GotoSteamGroupLinkLabel.LinkColor = System.Drawing.Color.Green;
			this.GotoSteamGroupLinkLabel.Location = new System.Drawing.Point(3, 160);
			this.GotoSteamGroupLinkLabel.Name = "GotoSteamGroupLinkLabel";
			this.GotoSteamGroupLinkLabel.Size = new System.Drawing.Size(165, 21);
			this.GotoSteamGroupLinkLabel.TabIndex = 2;
			this.GotoSteamGroupLinkLabel.TabStop = true;
			this.GotoSteamGroupLinkLabel.Text = "Goto Steam Group";
			this.GotoSteamGroupLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.GotoSteamGroupLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//GotoSteamProfileLinkLabel
			//
			this.GotoSteamProfileLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen;
			this.GotoSteamProfileLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.GotoSteamProfileLinkLabel.LinkColor = System.Drawing.Color.Green;
			this.GotoSteamProfileLinkLabel.Location = new System.Drawing.Point(3, 389);
			this.GotoSteamProfileLinkLabel.Name = "GotoSteamProfileLinkLabel";
			this.GotoSteamProfileLinkLabel.Size = new System.Drawing.Size(165, 20);
			this.GotoSteamProfileLinkLabel.TabIndex = 6;
			this.GotoSteamProfileLinkLabel.TabStop = true;
			this.GotoSteamProfileLinkLabel.Text = "Goto Steam Profile";
			this.GotoSteamProfileLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.GotoSteamProfileLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//PayPalPictureBox
			//
			this.PayPalPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PayPalPictureBox.Image = global::Crowbar.Properties.Resources._26_Grey_PayPal_Pill_Button;
			this.PayPalPictureBox.Location = new System.Drawing.Point(43, 412);
			this.PayPalPictureBox.Name = "PayPalPictureBox";
			this.PayPalPictureBox.Size = new System.Drawing.Size(84, 26);
			this.PayPalPictureBox.TabIndex = 11;
			this.PayPalPictureBox.TabStop = false;
			//
			//SpecialThanksGroupBox
			//
			this.SpecialThanksGroupBox.Controls.Add(this.CreditsTextBox);
			this.SpecialThanksGroupBox.Controls.Add(this.Credits2TextBox);
			this.SpecialThanksGroupBox.Controls.Add(this.Credits3TextBox);
			this.SpecialThanksGroupBox.Location = new System.Drawing.Point(175, 145);
			this.SpecialThanksGroupBox.Name = "SpecialThanksGroupBox";
			this.SpecialThanksGroupBox.Size = new System.Drawing.Size(598, 232);
			this.SpecialThanksGroupBox.TabIndex = 8;
			this.SpecialThanksGroupBox.TabStop = false;
			this.SpecialThanksGroupBox.Text = "Special Thanks";
			//
			//CreditsTextBox
			//
			this.CreditsTextBox.Location = new System.Drawing.Point(6, 20);
			this.CreditsTextBox.Multiline = true;
			this.CreditsTextBox.Name = "CreditsTextBox";
			this.CreditsTextBox.ReadOnly = true;
			this.CreditsTextBox.Size = new System.Drawing.Size(191, 206);
			this.CreditsTextBox.TabIndex = 0;
			this.CreditsTextBox.TabStop = false;
			this.CreditsTextBox.Text = "arby26" + "\r" + "\n" + "Artfunkel" + "\r" + "\n" + "atrblizzard" + "\r" + "\n" + "Avengito" + "\r" + "\n" + "BANG!" + "\r" + "\n" + "BinaryRifle" + "\r" + "\n" + "Cra0kalo" + "\r" + "\n" + "CrazyBubb" + "a" + "\r" + "\n" + "da1barker" + "\r" + "\n" + "Doktor haus" + "\r" + "\n" + "Drsalvador" + "\r" + "\n" + "E7ajamy" + "\r" + "\n" + "Funreal" + "\r" + "\n" + "Game Zombie" + "\r" + "\n" + "GeckoN";
			this.CreditsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//Credits2TextBox
			//
			this.Credits2TextBox.Location = new System.Drawing.Point(203, 20);
			this.Credits2TextBox.Multiline = true;
			this.Credits2TextBox.Name = "Credits2TextBox";
			this.Credits2TextBox.ReadOnly = true;
			this.Credits2TextBox.Size = new System.Drawing.Size(191, 206);
			this.Credits2TextBox.TabIndex = 1;
			this.Credits2TextBox.TabStop = false;
			this.Credits2TextBox.Text = "GPZ" + "\r" + "\n" + "Kerry [Valve employee]" + "\r" + "\n" + "k@rt" + "\r" + "\n" + "K1CHWA" + "\r" + "\n" + "Lt. Rocky" + "\r" + "\n" + "MARK2580" + "\r" + "\n" + "Mayhem" + "\r" + "\n" + "Mr. Brigh" + "tside" + "\r" + "\n" + "mrlanky" + "\r" + "\n" + "Nicknine" + "\r" + "\n" + "Pacagma" + "\r" + "\n" + "Pajama" + "\r" + "\n" + "pappaskurtz" + "\r" + "\n" + "Pte Jack";
			this.Credits2TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//Credits3TextBox
			//
			this.Credits3TextBox.Location = new System.Drawing.Point(400, 20);
			this.Credits3TextBox.Multiline = true;
			this.Credits3TextBox.Name = "Credits3TextBox";
			this.Credits3TextBox.ReadOnly = true;
			this.Credits3TextBox.Size = new System.Drawing.Size(191, 206);
			this.Credits3TextBox.TabIndex = 2;
			this.Credits3TextBox.TabStop = false;
			this.Credits3TextBox.Text = "Rantis" + "\r" + "\n" + "RED_EYE" + "\r" + "\n" + "Sage J. Fox" + "\r" + "\n" + "Salad" + "\r" + "\n" + "Seraphim" + "\r" + "\n" + "SherlockHolmes9�" + "\r" + "\n" + "Splinks" + "\r" + "\n" + "Stiffy360" + "\r" + "\n" + "Stay Puft" + "\r" + "\n" + "The" + " Freakin' Scout's A Spy!" + "\r" + "\n" + "The303" + "\r" + "\n" + "���VanderAGSN���" + "\r" + "\n" + "Vincentor" + "\r" + "\n" + "YuRaNnNzZZ";
			this.Credits3TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//AboutUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel1);
			this.Name = "AboutUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.PayPalPictureBox).EndInit();
			this.SpecialThanksGroupBox.ResumeLayout(false);
			this.SpecialThanksGroupBox.PerformLayout();
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			this.Load += new System.EventHandler(AboutUserControl_Load);
			ProductLogoButton.Click += new System.EventHandler(ProductLogoButton_Click);
			AuthorIconButton.Click += new System.EventHandler(AuthorIconButton_Click);
			ProductNameLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
			AuthorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
			GotoSteamGroupLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
			GotoSteamProfileLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
			PayPalPictureBox.Click += new System.EventHandler(PayPalPictureBox_Click);
		}
		internal System.Windows.Forms.TextBox ProductInfoTextBox;
		internal System.Windows.Forms.TextBox ProductDescriptionTextBox;
		internal System.Windows.Forms.Button ProductLogoButton;
		internal System.Windows.Forms.Button AuthorIconButton;
		internal System.Windows.Forms.TextBox CreditsTextBox;
		internal System.Windows.Forms.LinkLabel AuthorLinkLabel;
		internal System.Windows.Forms.LinkLabel ProductNameLinkLabel;
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.LinkLabel GotoSteamProfileLinkLabel;
		internal System.Windows.Forms.LinkLabel GotoSteamGroupLinkLabel;
		internal System.Windows.Forms.GroupBox SpecialThanksGroupBox;
		internal System.Windows.Forms.TextBox Credits3TextBox;
		internal System.Windows.Forms.TextBox Credits2TextBox;
		internal PictureBox PayPalPictureBox;
	}

}