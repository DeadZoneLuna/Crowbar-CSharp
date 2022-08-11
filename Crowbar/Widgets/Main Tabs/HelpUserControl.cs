using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Crowbar
{
	public partial class HelpUserControl
	{

#region Creation and Destruction

		public HelpUserControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			//NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
			try
			{
				Init();
			}
			catch
			{
			}
		}

#endregion

#region Init and Free

		private void Init()
		{
			TutorialLinkLabel.Links.Add(0, TutorialLinkLabel.Text.Length, AppConstants.HelpTutorialLink);
			ContentsLinkLabel.Links.Add(0, ContentsLinkLabel.Text.Length, AppConstants.HelpContentsLink);
			IndexLinkLabel.Links.Add(0, IndexLinkLabel.Text.Length, AppConstants.HelpIndexLink);
			TipsLinkLabel.Links.Add(0, TipsLinkLabel.Text.Length, AppConstants.HelpTipsLink);
		}

		//Private Sub Free()

		//End Sub

#endregion

#region Properties

#endregion

#region Widget Event Handlers

#endregion

#region Child Widget Event Handlers

		private void CrowbarGuideButton_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Properties.Resources.Help_CrowbarGuideLink);
		}

		private void LinkLabel_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LinkLabel aLinkLabel = (LinkLabel)sender;

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				aLinkLabel.LinkVisited = true;
				string target = (e.Link.LinkData == null ? null : Convert.ToString(e.Link.LinkData));
				try
				{
					System.Diagnostics.Process.Start(target);
				}
				catch (Exception ex)
				{
					//TODO: Tell user what went wrong.
				}
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				//TODO: Show context menu with: Copy Link, Copy Text
			}
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