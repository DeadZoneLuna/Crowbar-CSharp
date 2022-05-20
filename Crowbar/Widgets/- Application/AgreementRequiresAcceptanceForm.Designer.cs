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
	public partial class AgreementRequiresAcceptanceForm : System.Windows.Forms.Form
	{
		//Form overrides dispose to clean up the component list.
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgreementRequiresAcceptanceForm));
			this.TextBox1 = new Crowbar.TextBoxEx();
			this.OpenSteamSubscriberAgreementButton = new System.Windows.Forms.Button();
			this.CancelDeleteButton = new System.Windows.Forms.Button();
			this.OpenWorkshopPageButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//TextBox1
			//
			this.TextBox1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox1.CueBannerText = "";
			this.TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.TextBox1.Location = new System.Drawing.Point(12, 12);
			this.TextBox1.Multiline = true;
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.ReadOnly = true;
			this.TextBox1.Size = new System.Drawing.Size(333, 110);
			this.TextBox1.TabIndex = 0;
			this.TextBox1.TabStop = false;
			this.TextBox1.Text = resources.GetString("TextBox1.Text");
			this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//OpenSteamSubscriberAgreementButton
			//
			this.OpenSteamSubscriberAgreementButton.Location = new System.Drawing.Point(78, 128);
			this.OpenSteamSubscriberAgreementButton.Name = "OpenSteamSubscriberAgreementButton";
			this.OpenSteamSubscriberAgreementButton.Size = new System.Drawing.Size(200, 23);
			this.OpenSteamSubscriberAgreementButton.TabIndex = 1;
			this.OpenSteamSubscriberAgreementButton.Text = "Open Steam Subscriber Agreement";
			this.OpenSteamSubscriberAgreementButton.UseVisualStyleBackColor = true;
			//
			//CancelDeleteButton
			//
			this.CancelDeleteButton.Location = new System.Drawing.Point(141, 186);
			this.CancelDeleteButton.Name = "CancelDeleteButton";
			this.CancelDeleteButton.Size = new System.Drawing.Size(75, 23);
			this.CancelDeleteButton.TabIndex = 3;
			this.CancelDeleteButton.Text = "Cancel";
			this.CancelDeleteButton.UseVisualStyleBackColor = true;
			//
			//OpenWorkshopPageButton
			//
			this.OpenWorkshopPageButton.Location = new System.Drawing.Point(116, 157);
			this.OpenWorkshopPageButton.Name = "OpenWorkshopPageButton";
			this.OpenWorkshopPageButton.Size = new System.Drawing.Size(125, 23);
			this.OpenWorkshopPageButton.TabIndex = 2;
			this.OpenWorkshopPageButton.Text = "Open Workshop Page";
			this.OpenWorkshopPageButton.UseVisualStyleBackColor = true;
			//
			//AgreementRequiresAcceptanceForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(357, 221);
			this.ControlBox = false;
			this.Controls.Add(this.OpenWorkshopPageButton);
			this.Controls.Add(this.CancelDeleteButton);
			this.Controls.Add(this.OpenSteamSubscriberAgreementButton);
			this.Controls.Add(this.TextBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AgreementRequiresAcceptanceForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Steam Subscriber Agreement Requires Acceptance";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

//INSTANT C# NOTE: Converted design-time event handler wireups:
			OpenSteamSubscriberAgreementButton.Click += new System.EventHandler(OpenSteamSubscriberAgreementButton_Click);
			OpenWorkshopPageButton.Click += new System.EventHandler(OpenWorkshopPageButton_Click);
			CancelDeleteButton.Click += new System.EventHandler(CancelDeleteButton_Click);
		}

		internal TextBoxEx TextBox1;
		internal Button OpenSteamSubscriberAgreementButton;
		internal Button CancelDeleteButton;
		internal Button OpenWorkshopPageButton;
	}

}