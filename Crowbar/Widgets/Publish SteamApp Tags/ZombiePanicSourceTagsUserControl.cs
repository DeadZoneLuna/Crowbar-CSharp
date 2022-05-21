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

				theCheckBoxesAreChangingViaMe = true;

				bool sectionIsSet = false;
				sectionIsSet = SetSection(sectionIsSet, GameModeRadioButton, GameModePanel);
				if (!sectionIsSet)
				{
					sectionIsSet = SetSection(sectionIsSet, CustomModelsRadioButton, CustomModelsPanel);
					if (!sectionIsSet)
					{
						sectionIsSet = SetSection(sectionIsSet, CustomSoundsRadioButton, CustomSoundsPanel);
						if (!sectionIsSet)
						{
							sectionIsSet = SetSection(sectionIsSet, MiscellaneousRadioButton, MiscellaneousPanel);
						}
					}
				}
				if (!sectionIsSet)
				{
					GameModeRadioButton.Checked = true;
				}

				theCheckBoxesAreChangingViaMe = false;
			}
		}

		private void GameModeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				ClearAllPanelsExceptGivenPanel(GameModePanel);
			}
		}

		private void CustomModelsRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				ClearAllPanelsExceptGivenPanel(CustomModelsPanel);
			}
		}

		private void CustomSoundsRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				ClearAllPanelsExceptGivenPanel(CustomSoundsPanel);
			}
		}

		private void MiscellaneousRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				ClearAllPanelsExceptGivenPanel(MiscellaneousPanel);
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
			foreach (Control widget in Controls)
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