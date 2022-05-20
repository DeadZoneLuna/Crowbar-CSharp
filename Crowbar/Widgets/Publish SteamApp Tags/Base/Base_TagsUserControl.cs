//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace Crowbar
{
	public partial class Base_TagsUserControl
	{

#region Create and Destroy

		public Base_TagsUserControl() : base()
		{

			//TEST: See if this prevents the overlapping or larger text on Chinese Windows.
			// This should allow Forms that inherit from this class and their widgets to use the system font instead of Visual Studio's default of Microsoft Sans Serif.
			this.Font = new Font(SystemFonts.MessageBoxFont.Name, 8.25f);

			// This call is required by the designer.
			InitializeComponent();
		}

		//UserControl overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (components != null)
					{
						components.Dispose();
					}
					this.Free();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

#endregion

#region Init and Free

		protected virtual void Init()
		{
			this.theWidgets = new List<Control>();
			this.GetAllWidgets(this.Controls);

			this.theCheckBoxesAreChangingViaMe = false;
			this.theComboBoxesAreChangingViaMe = false;
			this.theTextBoxesAreChangingViaMe = false;
		}

		private void GetAllWidgets(ControlCollection iWidgets)
		{
			foreach (Control widget in iWidgets)
			{
				if (widget.Tag is string)
				{
					if (widget is CheckBoxEx)
					{
						this.theWidgets.Add(widget);
					}
					else if (widget is ComboBox)
					{
						string aComboBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
						if (aComboBoxTag == "TagsEnabled")
						{
							this.theWidgets.Add(widget);
						}
					}
					else if (widget is TextBox)
					{
						string aTextBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
						if (aTextBoxTag == "TagsEnabled")
						{
							this.theWidgets.Add(widget);
						}
					}
				}
				else if (widget is GroupBox || widget is Panel)
				{
					this.GetAllWidgets(widget.Controls);
				}
			}
		}

		private void Free()
		{
			CheckBoxEx aCheckBox = null;
			ComboBox aComboBox = null;
			TextBox aTextBox = null;
			if (this.theWidgets != null)
			{
				foreach (Control widget in this.theWidgets)
				{
					if (widget.Tag is string)
					{
						if (widget is CheckBoxEx)
						{
							aCheckBox = (CheckBoxEx)widget;
							aCheckBox.CheckedChanged -= this.CheckBox_CheckedChanged;
						}
						else if (widget is ComboBox)
						{
							string aComboBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
							if (aComboBoxTag == "TagsEnabled")
							{
								aComboBox = (ComboBox)widget;
								aComboBox.SelectedIndexChanged -= this.ComboBox_SelectedIndexChanged;
							}
						}
						else if (widget is TextBox)
						{
							string aTextBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
							if (aTextBoxTag == "TagsEnabled")
							{
								aTextBox = (TextBox)widget;
								aTextBox.TextChanged -= this.TextBox_TextChanged;
							}
						}
					}
				}
			}
		}

#endregion

#region Properties

		public virtual BindingListEx<string> ItemTags
		{
			get
			{
				CheckBoxEx aCheckBox = null;
				ComboBox aComboBox = null;
				TextBox aTextBox = null;
				IList anEnumList = null;
				BindingListEx<string> itemTagsList = new BindingListEx<string>();
				foreach (Control widget in this.theWidgets)
				{
					if (widget.Tag is string)
					{
						if (widget is CheckBoxEx)
						{
							aCheckBox = (CheckBoxEx)widget;
							if (aCheckBox.Checked)
							{
								itemTagsList.Add((aCheckBox.Tag == null ? null : Convert.ToString(aCheckBox.Tag)));
							}
						}
						else if (widget is ComboBox)
						{
							string aComboBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
							if (aComboBoxTag == "TagsEnabled")
							{
								aComboBox = (ComboBox)widget;
								if (aComboBox.DataSource != null)
								{
									anEnumList = (IList)aComboBox.DataSource;
									itemTagsList.Add(aComboBox.SelectedValue.ToString());
								}
							}
						}
						else if (widget is TextBox)
						{
							aTextBox = (TextBox)widget;
							string aTextBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
							if (aTextBoxTag == "TagsEnabled" && !string.IsNullOrEmpty(aTextBox.Text))
							{
								itemTagsList.Add(Convert.ToString(aTextBox.Text));
							}
						}
					}
				}
				return itemTagsList;
			}
			set
			{
				this.theCheckBoxesAreChangingViaMe = true;
				this.theComboBoxesAreChangingViaMe = true;
				this.theTextBoxesAreChangingViaMe = true;

				CheckBoxEx aCheckBox = null;
				ComboBox aComboBox = null;
				TextBox aTextBox = null;

				foreach (Control widget in this.theWidgets)
				{
					if (widget.Tag is string)
					{
						if (widget is CheckBoxEx)
						{
							aCheckBox = (CheckBoxEx)widget;
							aCheckBox.Checked = false;
							aCheckBox.CheckedChanged -= this.CheckBox_CheckedChanged;
							aCheckBox.CheckedChanged += this.CheckBox_CheckedChanged;
						}
						else if (widget is ComboBox)
						{
							string aComboBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
							if (aComboBoxTag == "TagsEnabled")
							{
								aComboBox = (ComboBox)widget;
								aComboBox.SelectedIndexChanged -= this.ComboBox_SelectedIndexChanged;
								aComboBox.SelectedIndexChanged += this.ComboBox_SelectedIndexChanged;
							}
						}
						else if (widget is TextBox)
						{
							string aTextBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
							if (aTextBoxTag == "TagsEnabled")
							{
								aTextBox = (TextBox)widget;
								aTextBox.Text = "";
								aTextBox.TextChanged -= this.TextBox_TextChanged;
								aTextBox.TextChanged += this.TextBox_TextChanged;
							}
						}
					}
				}

				IList anEnumList = null;
				bool tagHasBeenAssigned = false;

				foreach (string tag in value)
				{
					tagHasBeenAssigned = false;

					foreach (Control widget in this.theWidgets)
					{
						if (widget.Tag is string)
						{
							if (widget is CheckBoxEx)
							{
								aCheckBox = (CheckBoxEx)widget;
								if (tag == (aCheckBox.Tag == null ? null : Convert.ToString(aCheckBox.Tag)))
								{
									aCheckBox.Checked = true;
									tagHasBeenAssigned = true;
									break;
								}
							}
							else if (widget is ComboBox)
							{
								string aComboBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
								if (aComboBoxTag == "TagsEnabled")
								{
									aComboBox = (ComboBox)widget;
									if (aComboBox.DataSource != null)
									{
										anEnumList = (IList)aComboBox.DataSource;
										int index = EnumHelper.IndexOfKeyAsCaseInsensitiveString(tag, anEnumList);
										if (index != -1)
										{
											aComboBox.SelectedIndex = index;
											tagHasBeenAssigned = true;
											break;
										}
									}
								}
							}
						}
					}
					//Loop through TextBoxes last because they will be filled with tags that do not belong to any other widget.
					if (!tagHasBeenAssigned)
					{
						foreach (Control widget in this.theWidgets)
						{
							if (widget.Tag is string)
							{
								if (widget is TextBox)
								{
									string aTextBoxTag = (widget.Tag == null ? null : Convert.ToString(widget.Tag));
									if (aTextBoxTag == "TagsEnabled")
									{
										aTextBox = (TextBox)widget;
										if (string.IsNullOrEmpty(aTextBox.Text))
										{
											aTextBox.Text = tag;
											break;
										}
									}
								}
							}
						}
					}
				}

				this.theCheckBoxesAreChangingViaMe = false;
				this.theComboBoxesAreChangingViaMe = false;
				this.theTextBoxesAreChangingViaMe = false;
			}
		}

#endregion

#region Widget Event Handlers

		private void TagsBaseUserControl_Load(object sender, EventArgs e)
		{
			this.Init();
		}

#endregion

#region Child Widget Event Handlers

		private void CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.OnCheckBox_CheckedChanged(sender, e);
		}

		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.theComboBoxesAreChangingViaMe)
			{
				if (TagsPropertyChanged != null)
					TagsPropertyChanged(this, new EventArgs());
			}
		}

		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			if (!this.theTextBoxesAreChangingViaMe)
			{
				if (TagsPropertyChanged != null)
					TagsPropertyChanged(this, new EventArgs());
			}
		}

#endregion

#region Events

		public event EventHandler TagsPropertyChanged;

#endregion

#region Private Methods

		protected virtual void OnCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.theCheckBoxesAreChangingViaMe)
			{
				if (TagsPropertyChanged != null)
					TagsPropertyChanged(this, new EventArgs());
			}
		}

		protected void RaiseTagsPropertyChanged()
		{
			if (TagsPropertyChanged != null)
				TagsPropertyChanged(this, new EventArgs());
		}

#endregion

#region Data

		private List<Control> theWidgets;
		protected bool theCheckBoxesAreChangingViaMe;
		private bool theComboBoxesAreChangingViaMe;
		private bool theTextBoxesAreChangingViaMe;

#endregion

	}

}