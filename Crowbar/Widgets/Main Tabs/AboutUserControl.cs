using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public partial class AboutUserControl
	{

#region Creation and Destruction

		public AboutUserControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

#endregion

#region Init and Free

		private void Init()
		{
			//NOTE: Customize the application's assembly information in the "Application" pane of the project 
			//    properties dialog (under the "Project" menu).

			ProductNameLinkLabel.Text = ConversionHelper.AssemblyProduct;
			ProductNameLinkLabel.Links.Add(0, ConversionHelper.AssemblyProduct.Length, Properties.Resources.About_ProductLink);

			GotoSteamGroupLinkLabel.Text = Properties.Resources.About_GotoSteamGroupText;
			GotoSteamGroupLinkLabel.Links.Add(0, Properties.Resources.About_GotoSteamGroupText.Length, Properties.Resources.About_ProductLink);

			ProductInfoTextBox.Text = "Version " + ConversionHelper.VersionName + "\r\n";
			ProductInfoTextBox.Text += ConversionHelper.AssemblyCopyright + "\r\n";
			ProductInfoTextBox.Text += ConversionHelper.AssemblyCompany;

			AuthorLinkLabel.Text = ConversionHelper.AssemblyCompany;
			AuthorLinkLabel.Links.Add(0, ConversionHelper.AssemblyCompany.Length, Properties.Resources.About_AuthorLink);

			ModifiedAuthorName.Links.Add(0, ModifiedAuthorName.Text.Length, Properties.Resources.About_ModifiedAuthorSteam);
			ModifiedGithub.Links.Add(0, ModifiedGithub.Text.Length, Properties.Resources.About_ModifiedGithub);

			GotoSteamProfileLinkLabel.Text = Properties.Resources.About_GotoSteamProfileText;
			GotoSteamProfileLinkLabel.Links.Add(0, Properties.Resources.About_GotoSteamProfileText.Length, Properties.Resources.About_AuthorLink);

			ProductDescriptionTextBox.Text = Properties.Resources.About_ProductDescription;

			//Me.Panel1.DataBindings.Add("BackColor", TheApp.Settings, "AboutTabBackgroundColor", False, DataSourceUpdateMode.OnPropertyChanged)
		}

		//Private Sub Free()

		//End Sub

#endregion

#region Properties

#endregion

#region Widget Event Handlers

		private void AboutUserControl_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		private void ProductLogoButton_Click(System.Object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start(Properties.Resources.About_ProductLink);
		}

		private void AuthorIconButton_Click(System.Object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start(Properties.Resources.About_AuthorLink);
		}

		private void LinkLabel_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkLabel aLinkLabel = (LinkLabel)sender;

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				aLinkLabel.LinkVisited = true;
				string target = (e.Link.LinkData == null ? null : Convert.ToString(e.Link.LinkData));
				System.Diagnostics.Process.Start(target);
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				//TODO: Show context menu with: Copy Link, Copy Text
			}
		}

		private void PayPalPictureBox_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Properties.Resources.About_PayPalLink);
		}

#endregion

#region Core Event Handlers

#endregion

#region Private Methods

#endregion

#region Data

#endregion

	}

}