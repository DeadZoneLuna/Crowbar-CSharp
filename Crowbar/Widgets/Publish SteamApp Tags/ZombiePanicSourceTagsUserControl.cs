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
	public partial class ZombiePanicSourceTagsUserControl
	{

		public ZombiePanicSourceTagsUserControl() : base()
		{

			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
		}

		//Protected Overrides Sub Init()
		//	MyBase.Init()

		//End Sub

		public override BindingListEx<string> ItemTags
		{
			get
			{
				return base.ItemTags;
			}
			set
			{
				base.ItemTags = value;

				this.theCheckBoxesAreChangingViaMe = true;

				bool sectionIsSet = false;
				sectionIsSet = this.SetSection(sectionIsSet, this.GameModeRadioButton, this.GameModePanel);
				if (!sectionIsSet)
				{
					sectionIsSet = this.SetSection(sectionIsSet, this.CustomModelsRadioButton, this.CustomModelsPanel);
					if (!sectionIsSet)
					{
						sectionIsSet = this.SetSection(sectionIsSet, this.CustomSoundsRadioButton, this.CustomSoundsPanel);
						if (!sectionIsSet)
						{
							sectionIsSet = this.SetSection(sectionIsSet, this.MiscellaneousRadioButton, this.MiscellaneousPanel);
						}
					}
				}
				if (!sectionIsSet)
				{
					this.GameModeRadioButton.Checked = true;
				}

				this.theCheckBoxesAreChangingViaMe = false;
			}
		}

		private void GameModeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				this.ClearAllPanelsExceptGivenPanel(this.GameModePanel);
			}
		}

		private void CustomModelsRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				this.ClearAllPanelsExceptGivenPanel(this.CustomModelsPanel);
			}
		}

		private void CustomSoundsRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				this.ClearAllPanelsExceptGivenPanel(this.CustomSoundsPanel);
			}
		}

		private void MiscellaneousRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				this.ClearAllPanelsExceptGivenPanel(this.MiscellaneousPanel);
			}
		}

		private bool SetSection(bool sectionIsSet, RadioButton givenRadioButton, Panel givenPanel)
		{
			foreach (CheckBox aCheckBox in givenPanel.Controls)
			{
				aCheckBox.Checked = base.ItemTags.Contains((aCheckBox.Tag == null ? null : Convert.ToString(aCheckBox.Tag)));
				if (!sectionIsSet && aCheckBox.Checked)
				{
					givenRadioButton.Checked = true;
					givenPanel.Enabled = true;
					sectionIsSet = true;
				}
			}
			return sectionIsSet;
		}

		private void ClearAllPanelsExceptGivenPanel(Panel givenPanel)
		{
			givenPanel.Enabled = true;
			Panel aPanel = null;
			foreach (Control widget in this.Controls)
			{
				if (widget is Panel)
				{
					aPanel = (Panel)widget;
					if (aPanel != givenPanel)
					{
						aPanel.Enabled = false;
						foreach (CheckBox aCheckBox in aPanel.Controls)
						{
							aCheckBox.Checked = false;
						}
					}
				}
			}
		}

	}

}