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
	public partial class GarrysModTagsUserControl
	{

		public GarrysModTagsUserControl() : base()
		{

			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			theOrientation = AppEnums.OrientationType.Vertical;
			ChangeOrientation();
		}

		protected override void Init()
		{
			base.Init();

			IList anEnumList = EnumHelper.ToList(typeof(GarrysModSteamAppInfo.GarrysModTypeTags));
			ComboBox1.DisplayMember = "Value";
			ComboBox1.ValueMember = "Key";
			ComboBox1.DataSource = anEnumList;
			ComboBox1.SelectedValue = GarrysModSteamAppInfo.GarrysModTypeTags.ServerContent;

			theCheckBoxes = new List<CheckBoxEx>();
			GetAllCheckboxes(GroupBox1.Controls);
			theCheckmarkedCheckBoxes = new List<CheckBoxEx>(2);
		}

		private void GetAllCheckboxes(ControlCollection iWidgets)
		{
			foreach (Control widget in iWidgets)
			{
				if (widget is CheckBoxEx && widget != AddonTagCheckBox)
				{
					CheckBoxEx aCheckBox = (CheckBoxEx)widget;
					theCheckBoxes.Add(aCheckBox);
				}
			}
		}

		public AppEnums.OrientationType Orientation
		{
			get
			{
				return theOrientation;
			}
			set
			{
				theOrientation = value;
				ChangeOrientation();
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override BindingListEx<string> ItemTags
		{
			get
			{
				//Dim tags As BindingListEx(Of String) = MyBase.ItemTags
				//If Not tags.Contains("Addon") Then
				//	tags.Add("Addon")
				//	Me.AddonTagCheckBox.Checked = True
				//	MyBase.RaiseTagsPropertyChanged()
				//End If
				//Return tags
				return base.ItemTags;
			}
			set
			{
				base.ItemTags = value;

				BindingListEx<string> tags = base.ItemTags;
				if (!tags.Contains("Addon"))
				{
					tags.Add("Addon");
					AddonTagCheckBox.Checked = true;
					base.RaiseTagsPropertyChanged();
				}

				theCheckmarkedCheckBoxes.Clear();
				foreach (CheckBoxEx selectedCheckBox in theCheckBoxes)
				{
					if (selectedCheckBox.Checked)
					{
						if (theCheckmarkedCheckBoxes.Count < 2)
						{
							theCheckmarkedCheckBoxes.Add(selectedCheckBox);
						}
						if (theCheckmarkedCheckBoxes.Count == 2)
						{
							foreach (CheckBoxEx aCheckBox in theCheckBoxes)
							{
								aCheckBox.Enabled = aCheckBox.Checked;
							}
							break;
						}
					}
				}
				if (theCheckmarkedCheckBoxes.Count < 2)
				{
					foreach (CheckBoxEx aCheckBox in theCheckBoxes)
					{
						aCheckBox.Enabled = true;
					}
				}
			}
		}

		protected override void OnCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!theCheckBoxesAreChangingViaMe)
			{
				CheckBoxEx selectedCheckBox = (CheckBoxEx)sender;
				if (selectedCheckBox.Checked)
				{
					if (theCheckmarkedCheckBoxes.Count < 2)
					{
						theCheckmarkedCheckBoxes.Add(selectedCheckBox);
					}
					if (theCheckmarkedCheckBoxes.Count == 2)
					{
						foreach (CheckBoxEx aCheckBox in theCheckBoxes)
						{
							aCheckBox.Enabled = aCheckBox.Checked;
						}
					}
				}
				else
				{
					theCheckmarkedCheckBoxes.Remove(selectedCheckBox);
					foreach (CheckBoxEx aCheckBox in theCheckBoxes)
					{
						aCheckBox.Enabled = true;
					}
				}
			}

			base.OnCheckBox_CheckedChanged(sender, e);
		}

		private void ChangeOrientation()
		{
			if (theOrientation == AppEnums.OrientationType.Horizontal)
			{
				//'Build
				//Me.CheckBox1.Location = New System.Drawing.Point(6, 20)
				//'Cartoon
				//Me.CheckBox2.Location = New System.Drawing.Point(6, 43)
				//Comic
				CheckBox3.Location = new System.Drawing.Point(78, 20);
				//Fun
				CheckBox4.Location = new System.Drawing.Point(78, 43);
				//Movie
				CheckBox5.Location = new System.Drawing.Point(150, 20);
				//Realism
				CheckBox6.Location = new System.Drawing.Point(150, 43);
				//Roleplay
				CheckBox7.Location = new System.Drawing.Point(222, 20);
				//Scenic
				CheckBox8.Location = new System.Drawing.Point(222, 43);
				//Water
				CheckBox9.Location = new System.Drawing.Point(294, 20);
				//GroupBox1
				GroupBox1.Size = new System.Drawing.Size(356, 68);
			}
			else
			{
				//'Build
				//Me.CheckBox1.Location = New System.Drawing.Point(6, 20)
				//'Cartoon
				//Me.CheckBox2.Location = New System.Drawing.Point(6, 43)
				//Comic
				CheckBox3.Location = new System.Drawing.Point(6, 66);
				//Fun
				CheckBox4.Location = new System.Drawing.Point(6, 89);
				//Movie
				CheckBox5.Location = new System.Drawing.Point(6, 112);
				//Realism
				CheckBox6.Location = new System.Drawing.Point(6, 135);
				//Roleplay
				CheckBox7.Location = new System.Drawing.Point(6, 158);
				//Scenic
				CheckBox8.Location = new System.Drawing.Point(6, 181);
				//Water
				CheckBox9.Location = new System.Drawing.Point(6, 204);
				//GroupBox1
				GroupBox1.Size = new System.Drawing.Size(161, 235);
			}
		}

		private List<CheckBoxEx> theCheckBoxes;
		private List<CheckBoxEx> theCheckmarkedCheckBoxes;
		private AppEnums.OrientationType theOrientation;

		// From Garry's Mod web page "Workshop Addon Creation" [ https://wiki.garrysmod.com/page/Workshop_Addon_Creation ]: 
		//type is the type of addon, one of:
		//"ServerContent"
		//"gamemode"
		//"map"
		//"weapon"
		//"vehicle"
		//"npc"
		//"tool"
		//"effects"
		//"model"
		//
		//tags is up to two of these:
		//"fun"
		//"roleplay"
		//"scenic"
		//"movie"
		//"realism"
		//"cartoon"
		//"water"
		//"comic"
		//"build"

	}

}