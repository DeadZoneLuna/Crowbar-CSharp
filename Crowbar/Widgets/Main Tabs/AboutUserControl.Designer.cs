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
            ProductInfoTextBox = new System.Windows.Forms.TextBox();
            ProductDescriptionTextBox = new System.Windows.Forms.TextBox();
            ProductLogoButton = new System.Windows.Forms.Button();
            AuthorIconButton = new System.Windows.Forms.Button();
            CreditsTextBox = new System.Windows.Forms.TextBox();
            AuthorLinkLabel = new System.Windows.Forms.LinkLabel();
            ProductNameLinkLabel = new System.Windows.Forms.LinkLabel();
            Panel1 = new System.Windows.Forms.Panel();
            GotoSteamGroupLinkLabel = new System.Windows.Forms.LinkLabel();
            GotoSteamProfileLinkLabel = new System.Windows.Forms.LinkLabel();
            PayPalPictureBox = new System.Windows.Forms.PictureBox();
            SpecialThanksGroupBox = new System.Windows.Forms.GroupBox();
            Credits2TextBox = new System.Windows.Forms.TextBox();
            Credits3TextBox = new System.Windows.Forms.TextBox();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(PayPalPictureBox)).BeginInit();
            SpecialThanksGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // ProductInfoTextBox
            // 
            ProductInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ProductInfoTextBox.Location = new System.Drawing.Point(3, 183);
            ProductInfoTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            ProductInfoTextBox.Multiline = true;
            ProductInfoTextBox.Name = "ProductInfoTextBox";
            ProductInfoTextBox.ReadOnly = true;
            ProductInfoTextBox.Size = new System.Drawing.Size(165, 48);
            ProductInfoTextBox.TabIndex = 3;
            ProductInfoTextBox.Text = "Version\r\nCopyright\r\nCompany Name";
            ProductInfoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ProductInfoTextBox.WordWrap = false;
            // 
            // ProductDescriptionTextBox
            // 
            ProductDescriptionTextBox.Location = new System.Drawing.Point(175, 3);
            ProductDescriptionTextBox.Multiline = true;
            ProductDescriptionTextBox.Name = "ProductDescriptionTextBox";
            ProductDescriptionTextBox.ReadOnly = true;
            ProductDescriptionTextBox.Size = new System.Drawing.Size(598, 136);
            ProductDescriptionTextBox.TabIndex = 7;
            ProductDescriptionTextBox.TabStop = false;
            // 
            // ProductLogoButton
            // 
            ProductLogoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            ProductLogoButton.Image = global::Crowbar.Properties.Resources.crowbar_icon_large;
            ProductLogoButton.Location = new System.Drawing.Point(21, 3);
            ProductLogoButton.Name = "ProductLogoButton";
            ProductLogoButton.Size = new System.Drawing.Size(128, 128);
            ProductLogoButton.TabIndex = 0;
            ProductLogoButton.UseVisualStyleBackColor = true;
            ProductLogoButton.Click += new System.EventHandler(ProductLogoButton_Click);
            // 
            // AuthorIconButton
            // 
            AuthorIconButton.Cursor = System.Windows.Forms.Cursors.Hand;
            AuthorIconButton.Image = global::Crowbar.Properties.Resources.macaw;
            AuthorIconButton.Location = new System.Drawing.Point(21, 236);
            AuthorIconButton.Name = "AuthorIconButton";
            AuthorIconButton.Size = new System.Drawing.Size(128, 128);
            AuthorIconButton.TabIndex = 4;
            AuthorIconButton.UseVisualStyleBackColor = true;
            AuthorIconButton.Click += new System.EventHandler(AuthorIconButton_Click);
            // 
            // CreditsTextBox
            // 
            CreditsTextBox.Location = new System.Drawing.Point(6, 20);
            CreditsTextBox.Multiline = true;
            CreditsTextBox.Name = "CreditsTextBox";
            CreditsTextBox.ReadOnly = true;
            CreditsTextBox.Size = new System.Drawing.Size(191, 206);
            CreditsTextBox.TabIndex = 0;
            CreditsTextBox.TabStop = false;
            CreditsTextBox.Text = "arby26\r\nArtfunkel\r\natrblizzard\r\nAvengito\r\nBANG!\r\nBinaryRifle\r\nCra0kalo\r\nCrazyBubb" +
    "a\r\nda1barker\r\nDoktor haus\r\nDrsalvador\r\nE7ajamy\r\nFunreal\r\nGame Zombie\r\nGeckoN";
            CreditsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AuthorLinkLabel
            // 
            AuthorLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen;
            AuthorLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            AuthorLinkLabel.LinkColor = System.Drawing.Color.Green;
            AuthorLinkLabel.Location = new System.Drawing.Point(3, 367);
            AuthorLinkLabel.Name = "AuthorLinkLabel";
            AuthorLinkLabel.Size = new System.Drawing.Size(165, 20);
            AuthorLinkLabel.TabIndex = 5;
            AuthorLinkLabel.TabStop = true;
            AuthorLinkLabel.Text = "Author";
            AuthorLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            AuthorLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
            AuthorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
            // 
            // ProductNameLinkLabel
            // 
            ProductNameLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen;
            ProductNameLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ProductNameLinkLabel.LinkColor = System.Drawing.Color.Green;
            ProductNameLinkLabel.Location = new System.Drawing.Point(3, 134);
            ProductNameLinkLabel.Name = "ProductNameLinkLabel";
            ProductNameLinkLabel.Size = new System.Drawing.Size(165, 23);
            ProductNameLinkLabel.TabIndex = 1;
            ProductNameLinkLabel.TabStop = true;
            ProductNameLinkLabel.Text = "Product Name";
            ProductNameLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            ProductNameLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
            ProductNameLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
            // 
            // Panel1
            // 
            Panel1.Controls.Add(ProductLogoButton);
            Panel1.Controls.Add(ProductNameLinkLabel);
            Panel1.Controls.Add(ProductInfoTextBox);
            Panel1.Controls.Add(GotoSteamGroupLinkLabel);
            Panel1.Controls.Add(AuthorIconButton);
            Panel1.Controls.Add(AuthorLinkLabel);
            Panel1.Controls.Add(GotoSteamProfileLinkLabel);
            Panel1.Controls.Add(PayPalPictureBox);
            Panel1.Controls.Add(ProductDescriptionTextBox);
            Panel1.Controls.Add(SpecialThanksGroupBox);
            Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            Panel1.Location = new System.Drawing.Point(0, 0);
            Panel1.Margin = new System.Windows.Forms.Padding(2);
            Panel1.Name = "Panel1";
            Panel1.Size = new System.Drawing.Size(776, 536);
            Panel1.TabIndex = 0;
            // 
            // GotoSteamGroupLinkLabel
            // 
            GotoSteamGroupLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen;
            GotoSteamGroupLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            GotoSteamGroupLinkLabel.LinkColor = System.Drawing.Color.Green;
            GotoSteamGroupLinkLabel.Location = new System.Drawing.Point(3, 160);
            GotoSteamGroupLinkLabel.Name = "GotoSteamGroupLinkLabel";
            GotoSteamGroupLinkLabel.Size = new System.Drawing.Size(165, 21);
            GotoSteamGroupLinkLabel.TabIndex = 2;
            GotoSteamGroupLinkLabel.TabStop = true;
            GotoSteamGroupLinkLabel.Text = "Goto Steam Group";
            GotoSteamGroupLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            GotoSteamGroupLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
            GotoSteamGroupLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
            // 
            // GotoSteamProfileLinkLabel
            // 
            GotoSteamProfileLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen;
            GotoSteamProfileLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            GotoSteamProfileLinkLabel.LinkColor = System.Drawing.Color.Green;
            GotoSteamProfileLinkLabel.Location = new System.Drawing.Point(3, 389);
            GotoSteamProfileLinkLabel.Name = "GotoSteamProfileLinkLabel";
            GotoSteamProfileLinkLabel.Size = new System.Drawing.Size(165, 20);
            GotoSteamProfileLinkLabel.TabIndex = 6;
            GotoSteamProfileLinkLabel.TabStop = true;
            GotoSteamProfileLinkLabel.Text = "Goto Steam Profile";
            GotoSteamProfileLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            GotoSteamProfileLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
            GotoSteamProfileLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
            // 
            // PayPalPictureBox
            // 
            PayPalPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            PayPalPictureBox.Image = global::Crowbar.Properties.Resources._26_Grey_PayPal_Pill_Button;
            PayPalPictureBox.Location = new System.Drawing.Point(43, 412);
            PayPalPictureBox.Name = "PayPalPictureBox";
            PayPalPictureBox.Size = new System.Drawing.Size(84, 26);
            PayPalPictureBox.TabIndex = 11;
            PayPalPictureBox.TabStop = false;
            PayPalPictureBox.Click += new System.EventHandler(PayPalPictureBox_Click);
            // 
            // SpecialThanksGroupBox
            // 
            SpecialThanksGroupBox.Controls.Add(CreditsTextBox);
            SpecialThanksGroupBox.Controls.Add(Credits2TextBox);
            SpecialThanksGroupBox.Controls.Add(Credits3TextBox);
            SpecialThanksGroupBox.Location = new System.Drawing.Point(175, 145);
            SpecialThanksGroupBox.Name = "SpecialThanksGroupBox";
            SpecialThanksGroupBox.Size = new System.Drawing.Size(598, 232);
            SpecialThanksGroupBox.TabIndex = 8;
            SpecialThanksGroupBox.TabStop = false;
            SpecialThanksGroupBox.Text = "Special Thanks";
            // 
            // Credits2TextBox
            // 
            Credits2TextBox.Location = new System.Drawing.Point(203, 20);
            Credits2TextBox.Multiline = true;
            Credits2TextBox.Name = "Credits2TextBox";
            Credits2TextBox.ReadOnly = true;
            Credits2TextBox.Size = new System.Drawing.Size(191, 206);
            Credits2TextBox.TabIndex = 1;
            Credits2TextBox.TabStop = false;
            Credits2TextBox.Text = "GPZ\r\nKerry [Valve employee]\r\nk@rt\r\nK1CHWA\r\nLt. Rocky\r\nMARK2580\r\nMayhem\r\nMr. Brigh" +
    "tside\r\nmrlanky\r\nNicknine\r\nPacagma\r\nPajama\r\npappaskurtz\r\nPte Jack";
            Credits2TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Credits3TextBox
            // 
            Credits3TextBox.Location = new System.Drawing.Point(400, 20);
            Credits3TextBox.Multiline = true;
            Credits3TextBox.Name = "Credits3TextBox";
            Credits3TextBox.ReadOnly = true;
            Credits3TextBox.Size = new System.Drawing.Size(191, 206);
            Credits3TextBox.TabIndex = 2;
            Credits3TextBox.TabStop = false;
            Credits3TextBox.Text = "Rantis\r\nRED_EYE\r\nSage J. Fox\r\nSalad\r\nSeraphim\r\nSherlockHolmes9™\r\nSplinks\r\nStiffy3" +
    "60\r\nStay Puft\r\nThe Freakin\' Scout\'s A Spy!\r\nThe303\r\n»»»VanderAGSN«««\r\nVincentor\r" +
    "\nYuRaNnNzZZ";
            Credits3TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AboutUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(Panel1);
            Name = "AboutUserControl";
            Size = new System.Drawing.Size(776, 536);
            Load += new System.EventHandler(AboutUserControl_Load);
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(PayPalPictureBox)).EndInit();
            SpecialThanksGroupBox.ResumeLayout(false);
            SpecialThanksGroupBox.PerformLayout();
            ResumeLayout(false);

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