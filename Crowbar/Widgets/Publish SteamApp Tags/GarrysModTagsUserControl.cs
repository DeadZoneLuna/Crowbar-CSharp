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
	public partial class GarrysModTagsUserControl
	{

		public GarrysModTagsUserControl() : base()
		{

			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			this.theOrientation = AppEnums.OrientationType.Vertical;
			this.ChangeOrientation();
		}

		protected override void Init()
		{
			base.Init();

			IList anEnumList = EnumHelper.ToList(typeof(GarrysModSteamAppInfo.GarrysModTypeTags));
			this.ComboBox1.DisplayMember = "Value";
			this.ComboBox1.ValueMember = "Key";
			this.ComboBox1.DataSource = anEnumList;
			this.ComboBox1.SelectedValue = GarrysModSteamAppInfo.GarrysModTypeTags.ServerContent;

			this.theCheckBoxes = new List<CheckBoxEx>();
			this.GetAllCheckboxes(this.GroupBox1.Controls);
			this.theCheckmarkedCheckBoxes = new List<CheckBoxEx>(2);
		}

		private void GetAllCheckboxes(ControlCollection iWidgets)
		{
			foreach (Control widget in iWidgets)
			{
				if (widget is CheckBoxEx && widget != this.AddonTagCheckBox)
				{
					CheckBoxEx aCheckBox = (CheckBoxEx)widget;
					this.theCheckBoxes.Add(aCheckBox);
				}
			}
		}

		public AppEnums.OrientationType Orientation
		{
			get
			{
				return this.theOrientation;
			}
			set
			{
				this.theOrientation = value;
				this.ChangeOrientation();
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
					this.AddonTagCheckBox.Checked = true;
					base.RaiseTagsPropertyChanged();
				}

				this.theCheckmarkedCheckBoxes.Clear();
				foreach (CheckBoxEx selectedCheckBox in this.theCheckBoxes)
				{
					if (selectedCheckBox.Checked)
					{
						if (this.theCheckmarkedCheckBoxes.Count < 2)
						{
							this.theCheckmarkedCheckBoxes.Add(selectedCheckBox);
						}
						if (this.theCheckmarkedCheckBoxes.Count == 2)
						{
							foreach (CheckBoxEx aCheckBox in this.theCheckBoxes)
							{
								aCheckBox.Enabled = aCheckBox.Checked;
							}
							break;
						}
					}
				}
				if (this.theCheckmarkedCheckBoxes.Count < 2)
				{
					foreach (CheckBoxEx aCheckBox in this.theCheckBoxes)
					{
						aCheckBox.Enabled = true;
					}
				}
			}
		}

		protected override void OnCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.theCheckBoxesAreChangingViaMe)
			{
				CheckBoxEx selectedCheckBox = (CheckBoxEx)sender;
				if (selectedCheckBox.Checked)
				{
					if (this.theCheckmarkedCheckBoxes.Count < 2)
					{
						this.theCheckmarkedCheckBoxes.Add(selectedCheckBox);
					}
					if (this.theCheckmarkedCheckBoxes.Count == 2)
					{
						foreach (CheckBoxEx aCheckBox in this.theCheckBoxes)
						{
							aCheckBox.Enabled = aCheckBox.Checked;
						}
					}
				}
				else
				{
					this.theCheckmarkedCheckBoxes.Remove(selectedCheckBox);
					foreach (CheckBoxEx aCheckBox in this.theCheckBoxes)
					{
						aCheckBox.Enabled = true;
					}
				}
			}

			base.OnCheckBox_CheckedChanged(sender, e);
		}

		private void ChangeOrientation()
		{
			if (this.theOrientation == AppEnums.OrientationType.Horizontal)
			{
				//'Build
				//Me.CheckBox1.Location = New System.Drawing.Point(6, 20)
				//'Cartoon
				//Me.CheckBox2.Location = New System.Drawing.Point(6, 43)
				//Comic
				this.CheckBox3.Location = new System.Drawing.Point(78, 20);
				//Fun
				this.CheckBox4.Location = new System.Drawing.Point(78, 43);
				//Movie
				this.CheckBox5.Location = new System.Drawing.Point(150, 20);
				//Realism
				this.CheckBox6.Location = new System.Drawing.Point(150, 43);
				//Roleplay
				this.CheckBox7.Location = new System.Drawing.Point(222, 20);
				//Scenic
				this.CheckBox8.Location = new System.Drawing.Point(222, 43);
				//Water
				this.CheckBox9.Location = new System.Drawing.Point(294, 20);
				//GroupBox1
				this.GroupBox1.Size = new System.Drawing.Size(356, 68);
			}
			else
			{
				//'Build
				//Me.CheckBox1.Location = New System.Drawing.Point(6, 20)
				//'Cartoon
				//Me.CheckBox2.Location = New System.Drawing.Point(6, 43)
				//Comic
				this.CheckBox3.Location = new System.Drawing.Point(6, 66);
				//Fun
				this.CheckBox4.Location = new System.Drawing.Point(6, 89);
				//Movie
				this.CheckBox5.Location = new System.Drawing.Point(6, 112);
				//Realism
				this.CheckBox6.Location = new System.Drawing.Point(6, 135);
				//Roleplay
				this.CheckBox7.Location = new System.Drawing.Point(6, 158);
				//Scenic
				this.CheckBox8.Location = new System.Drawing.Point(6, 181);
				//Water
				this.CheckBox9.Location = new System.Drawing.Point(6, 204);
				//GroupBox1
				this.GroupBox1.Size = new System.Drawing.Size(161, 235);
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