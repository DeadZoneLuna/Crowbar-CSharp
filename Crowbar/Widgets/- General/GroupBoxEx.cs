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
	public class GroupBoxEx : GroupBox
	{
		//NOTE: DataBind a group of RadioButtons to a DataSource property that is an Enum.
		//thisGroupBoxEx.DataBindings.Add("SelectedValue", theDataSourceThatHasTheEnumProperty, "NameOfEnumProperty", False, DataSourceUpdateMode.OnPropertyChanged)


		public GroupBoxEx() : base()
		{

			//Me.theSelectedIndex = -1
		}

		public event EventHandler SelectedValueChanged;

		//Public Property DataSource() As Object
		//	Get
		//		Return Me.theDataSource
		//	End Get
		//	Set(ByVal value As Object)
		//		If value Is Nothing Then
		//			If Me.theDataSource IsNot Nothing Then
		//				Me.theDataSource = Nothing
		//			End If
		//		ElseIf TypeOf value Is List(Of KeyValuePair(Of System.Enum, String)) Then
		//			Me.theDataSource = CType(value, List(Of KeyValuePair(Of System.Enum, String)))
		//			If Me.theDataSource.Count > 0 Then
		//				'Me.theSelectedIndex = 0
		//				Me.theSelectedValue = Me.theDataSource(0).Key()
		//				'For Each binding As Binding In Me.DataBindings
		//				'	binding.ReadValue()
		//				'Next
		//			End If
		//		End If
		//	End Set
		//End Property

		//Public Property DisplayMember() As String
		//	Get
		//		Return Me.theDisplayMember
		//	End Get
		//	Set(ByVal value As String)
		//		Me.theDisplayMember = value
		//	End Set
		//End Property

		//Public Property ValueMember() As String
		//	Get
		//		Return Me.theValueMember
		//	End Get
		//	Set(ByVal value As String)
		//		Me.theValueMember = value
		//	End Set
		//End Property

		public bool IsReadOnly
		{
			get
			{
				return this.theControlIsReadOnly;
			}
			set
			{
				if (this.theControlIsReadOnly != value)
				{
					this.theControlIsReadOnly = value;

					if (this.theControlIsReadOnly)
					{
						this.ForeColor = SystemColors.GrayText;
					}
					else
					{
						this.ForeColor = SystemColors.ControlText;
					}
				}
			}
		}

		public RadioButton[] RadioButtons
		{
			get
			{
				//If Me.theDataSource IsNot Nothing Then
				//	Return theRadioButtonList.ToArray()
				//Else
				//	Return Nothing
				//End If
				return this.theRadioButtonList.ToArray();
			}
		}

		//Public Property SelectedIndex() As Integer
		//	Get
		//		For i As Integer = 0 To list.Count - 1
		//			If list(i).Checked Then
		//				Return i
		//			End If
		//		Next
		//		Return -1
		//	End Get
		//	Set(ByVal value As Integer)
		//		If value <> SelectedIndex Then
		//			If value = -1 Then
		//				list(SelectedIndex).Checked = False
		//			Else
		//				list(value).Checked = True
		//			End If

		//			OnSelectedIndexChanged(New EventArgs())
		//		End If
		//	End Set
		//End Property

		public System.Enum SelectedValue
		{
			get
			{
				//If Me.theDataSource Is Nothing Then
				//	Return Nothing
				//End If
				//If Me.theSelectedIndex > -1 Then
				//	'For Each binding As Binding In Me.DataBindings
				//	'	If binding.PropertyName = "SelectedValue" Then
				//	'		binding.ReadValue()
				//	'	End If
				//	'Next
				//	Me.theRadioButtonList(Me.theSelectedIndex).Checked = True
				//	Return Me.theDataSource(Me.theSelectedIndex).Key
				//Else
				//	Return Nothing
				//End If
				return this.theSelectedValue;
			}
			set
			{
				//NOTE: This test is needed because Visual Studio Designer sets the property to Nothing in InitializeComponent().
				if (value == null)
				{
					return;
				}
				this.SetValue(value);
				RadioButton radioButton = null;
				for (int i = 0; i < this.theRadioButtonList.Count; i++)
				{
					radioButton = this.theRadioButtonList[i];
					if (value.Equals(radioButton.Tag))
					{
						radioButton.Checked = true;
					}
				}
			}
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			if (e.Control is RadioButton)
			{
				RadioButton radioButton = (RadioButton)e.Control;
				this.theRadioButtonList.Add(radioButton);
				radioButton.CheckedChanged += this.RadioButton_CheckedChanged;
			}
			base.OnControlAdded(e);
		}

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			if (e.Control is RadioButton)
			{
				RadioButton radioButton = (RadioButton)e.Control;
				this.theRadioButtonList.Remove(radioButton);
				radioButton.CheckedChanged -= this.RadioButton_CheckedChanged;
			}
			base.OnControlRemoved(e);
		}

		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			if (SelectedValueChanged != null)
				SelectedValueChanged(this, e);
		}

		private void RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (radioButton.Checked)
			{
				//For i As Integer = 0 To Me.theRadioButtonList.Count - 1
				//	If radioButton Is Me.theRadioButtonList(i) Then
				//		'Me.SelectedValue = Me.theDataSource(i).Key
				//		''For Each binding As Binding In Me.DataBindings
				//		''	If radioButton Is binding.Control Then
				//		''		binding.WriteValue()
				//		''	End If
				//		''Next
				//		'Me.SetValue(Me.theDataSource(i).Key)
				//		Return
				//	End If
				//Next
				this.SetValue((System.Enum)radioButton.Tag);
			}
		}

		private void SetValue(System.Enum value)
		{
			//If Me.theDataSource Is Nothing Then
			//	Return
			//End If
			//For i As Integer = 0 To Me.theDataSource.Count - 1
			//	If Me.theDataSource(i).Key.Equals(value) Then
			//		Me.theSelectedIndex = i
			//		OnSelectedValueChanged(New EventArgs())
			//		'For Each binding As Binding In Me.DataBindings
			//		'	If binding.PropertyName = "SelectedValue" Then
			//		'		binding.WriteValue()
			//		'	End If
			//		'Next
			//	End If
			//Next
			this.theSelectedValue = value;
			this.OnSelectedValueChanged(new EventArgs());
		}

		protected bool theControlIsReadOnly;
		//Private theDataSource As List(Of KeyValuePair(Of System.Enum, String))
		//Private theDisplayMember As String
		//Private theValueMember As String
		private System.Collections.Generic.List<RadioButton> theRadioButtonList = new System.Collections.Generic.List<RadioButton>();
		//Private theSelectedIndex As Integer
		private System.Enum theSelectedValue;

	}

}