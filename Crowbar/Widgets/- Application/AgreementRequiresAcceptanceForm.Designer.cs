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
			TextBox1 = new Crowbar.TextBoxEx();
			OpenSteamSubscriberAgreementButton = new System.Windows.Forms.Button();
			CancelDeleteButton = new System.Windows.Forms.Button();
			OpenWorkshopPageButton = new System.Windows.Forms.Button();
			SuspendLayout();
			//
			//TextBox1
			//
			TextBox1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TextBox1.CueBannerText = "";
			TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			TextBox1.Location = new System.Drawing.Point(12, 12);
			TextBox1.Multiline = true;
			TextBox1.Name = "TextBox1";
			TextBox1.ReadOnly = true;
			TextBox1.Size = new System.Drawing.Size(333, 110);
			TextBox1.TabIndex = 0;
			TextBox1.TabStop = false;
			TextBox1.Text = resources.GetString("TextBox1.Text");
			TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//OpenSteamSubscriberAgreementButton
			//
			OpenSteamSubscriberAgreementButton.Location = new System.Drawing.Point(78, 128);
			OpenSteamSubscriberAgreementButton.Name = "OpenSteamSubscriberAgreementButton";
			OpenSteamSubscriberAgreementButton.Size = new System.Drawing.Size(200, 23);
			OpenSteamSubscriberAgreementButton.TabIndex = 1;
			OpenSteamSubscriberAgreementButton.Text = "Open Steam Subscriber Agreement";
			OpenSteamSubscriberAgreementButton.UseVisualStyleBackColor = true;
			//
			//CancelDeleteButton
			//
			CancelDeleteButton.Location = new System.Drawing.Point(141, 186);
			CancelDeleteButton.Name = "CancelDeleteButton";
			CancelDeleteButton.Size = new System.Drawing.Size(75, 23);
			CancelDeleteButton.TabIndex = 3;
			CancelDeleteButton.Text = "Cancel";
			CancelDeleteButton.UseVisualStyleBackColor = true;
			//
			//OpenWorkshopPageButton
			//
			OpenWorkshopPageButton.Location = new System.Drawing.Point(116, 157);
			OpenWorkshopPageButton.Name = "OpenWorkshopPageButton";
			OpenWorkshopPageButton.Size = new System.Drawing.Size(125, 23);
			OpenWorkshopPageButton.TabIndex = 2;
			OpenWorkshopPageButton.Text = "Open Workshop Page";
			OpenWorkshopPageButton.UseVisualStyleBackColor = true;
			//
			//AgreementRequiresAcceptanceForm
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(357, 221);
			ControlBox = false;
			Controls.Add(OpenWorkshopPageButton);
			Controls.Add(CancelDeleteButton);
			Controls.Add(OpenSteamSubscriberAgreementButton);
			Controls.Add(TextBox1);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			Name = "AgreementRequiresAcceptanceForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Steam Subscriber Agreement Requires Acceptance";
			TopMost = true;
			ResumeLayout(false);
			PerformLayout();

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