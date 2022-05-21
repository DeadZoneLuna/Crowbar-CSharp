using System.ComponentModel;

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
	public partial class GarrysModTagsUserControl : Base_TagsUserControl
	{
		//UserControl overrides dispose to clean up the component list.
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
			ComboBox1 = new System.Windows.Forms.ComboBox();
			Label1 = new System.Windows.Forms.Label();
			CheckBox1 = new Crowbar.CheckBoxEx();
			CheckBox2 = new Crowbar.CheckBoxEx();
			CheckBox3 = new Crowbar.CheckBoxEx();
			CheckBox4 = new Crowbar.CheckBoxEx();
			CheckBox5 = new Crowbar.CheckBoxEx();
			CheckBox6 = new Crowbar.CheckBoxEx();
			CheckBox7 = new Crowbar.CheckBoxEx();
			CheckBox8 = new Crowbar.CheckBoxEx();
			CheckBox9 = new Crowbar.CheckBoxEx();
			GroupBox1 = new Crowbar.GroupBoxEx();
			AddonTagCheckBox = new Crowbar.CheckBoxEx();
			Label2 = new System.Windows.Forms.Label();
			GroupBox1.SuspendLayout();
			SuspendLayout();
			//
			//ComboBox1
			//
			ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			ComboBox1.FormattingEnabled = true;
			ComboBox1.Location = new System.Drawing.Point(42, 0);
			ComboBox1.Name = "ComboBox1";
			ComboBox1.Size = new System.Drawing.Size(110, 21);
			ComboBox1.TabIndex = 1;
			ComboBox1.Tag = "TagsEnabled";
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(3, 4);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(33, 13);
			Label1.TabIndex = 0;
			Label1.Text = "Type:";
			//
			//CheckBox1
			//
			CheckBox1.AutoSize = true;
			CheckBox1.IsReadOnly = false;
			CheckBox1.Location = new System.Drawing.Point(6, 20);
			CheckBox1.Name = "CheckBox1";
			CheckBox1.Size = new System.Drawing.Size(52, 17);
			CheckBox1.TabIndex = 0;
			CheckBox1.Tag = "Build";
			CheckBox1.Text = "Build";
			CheckBox1.UseVisualStyleBackColor = true;
			//
			//CheckBox2
			//
			CheckBox2.AutoSize = true;
			CheckBox2.IsReadOnly = false;
			CheckBox2.Location = new System.Drawing.Point(6, 43);
			CheckBox2.Name = "CheckBox2";
			CheckBox2.Size = new System.Drawing.Size(68, 17);
			CheckBox2.TabIndex = 1;
			CheckBox2.Tag = "Cartoon";
			CheckBox2.Text = "Cartoon";
			CheckBox2.UseVisualStyleBackColor = true;
			//
			//CheckBox3
			//
			CheckBox3.AutoSize = true;
			CheckBox3.IsReadOnly = false;
			CheckBox3.Location = new System.Drawing.Point(6, 66);
			CheckBox3.Name = "CheckBox3";
			CheckBox3.Size = new System.Drawing.Size(57, 17);
			CheckBox3.TabIndex = 2;
			CheckBox3.Tag = "Comic";
			CheckBox3.Text = "Comic";
			CheckBox3.UseVisualStyleBackColor = true;
			//
			//CheckBox4
			//
			CheckBox4.AutoSize = true;
			CheckBox4.IsReadOnly = false;
			CheckBox4.Location = new System.Drawing.Point(6, 89);
			CheckBox4.Name = "CheckBox4";
			CheckBox4.Size = new System.Drawing.Size(46, 17);
			CheckBox4.TabIndex = 3;
			CheckBox4.Tag = "Fun";
			CheckBox4.Text = "Fun";
			CheckBox4.UseVisualStyleBackColor = true;
			//
			//CheckBox5
			//
			CheckBox5.AutoSize = true;
			CheckBox5.IsReadOnly = false;
			CheckBox5.Location = new System.Drawing.Point(6, 112);
			CheckBox5.Name = "CheckBox5";
			CheckBox5.Size = new System.Drawing.Size(57, 17);
			CheckBox5.TabIndex = 4;
			CheckBox5.Tag = "Movie";
			CheckBox5.Text = "Movie";
			CheckBox5.UseVisualStyleBackColor = true;
			//
			//CheckBox6
			//
			CheckBox6.AutoSize = true;
			CheckBox6.IsReadOnly = false;
			CheckBox6.Location = new System.Drawing.Point(6, 135);
			CheckBox6.Name = "CheckBox6";
			CheckBox6.Size = new System.Drawing.Size(65, 17);
			CheckBox6.TabIndex = 5;
			CheckBox6.Tag = "Realism";
			CheckBox6.Text = "Realism";
			CheckBox6.UseVisualStyleBackColor = true;
			//
			//CheckBox7
			//
			CheckBox7.AutoSize = true;
			CheckBox7.IsReadOnly = false;
			CheckBox7.Location = new System.Drawing.Point(6, 158);
			CheckBox7.Name = "CheckBox7";
			CheckBox7.Size = new System.Drawing.Size(70, 17);
			CheckBox7.TabIndex = 6;
			CheckBox7.Tag = "Roleplay";
			CheckBox7.Text = "Roleplay";
			CheckBox7.UseVisualStyleBackColor = true;
			//
			//CheckBox8
			//
			CheckBox8.AutoSize = true;
			CheckBox8.IsReadOnly = false;
			CheckBox8.Location = new System.Drawing.Point(6, 181);
			CheckBox8.Name = "CheckBox8";
			CheckBox8.Size = new System.Drawing.Size(58, 17);
			CheckBox8.TabIndex = 7;
			CheckBox8.Tag = "Scenic";
			CheckBox8.Text = "Scenic";
			CheckBox8.UseVisualStyleBackColor = true;
			//
			//CheckBox9
			//
			CheckBox9.AutoSize = true;
			CheckBox9.IsReadOnly = false;
			CheckBox9.Location = new System.Drawing.Point(6, 204);
			CheckBox9.Name = "CheckBox9";
			CheckBox9.Size = new System.Drawing.Size(57, 17);
			CheckBox9.TabIndex = 8;
			CheckBox9.Tag = "Water";
			CheckBox9.Text = "Water";
			CheckBox9.UseVisualStyleBackColor = true;
			//
			//GroupBox1
			//
			GroupBox1.Controls.Add(CheckBox1);
			GroupBox1.Controls.Add(CheckBox9);
			GroupBox1.Controls.Add(CheckBox2);
			GroupBox1.Controls.Add(CheckBox8);
			GroupBox1.Controls.Add(CheckBox3);
			GroupBox1.Controls.Add(CheckBox7);
			GroupBox1.Controls.Add(CheckBox4);
			GroupBox1.Controls.Add(CheckBox6);
			GroupBox1.Controls.Add(CheckBox5);
			GroupBox1.IsReadOnly = false;
			GroupBox1.Location = new System.Drawing.Point(3, 27);
			GroupBox1.Name = "GroupBox1";
			GroupBox1.SelectedValue = null;
			GroupBox1.Size = new System.Drawing.Size(151, 228);
			GroupBox1.TabIndex = 2;
			GroupBox1.TabStop = false;
			GroupBox1.Text = "Choose up to two";
			//
			//AddonTagCheckBox
			//
			AddonTagCheckBox.AutoSize = true;
			AddonTagCheckBox.Enabled = false;
			AddonTagCheckBox.IsReadOnly = false;
			AddonTagCheckBox.Location = new System.Drawing.Point(72, 261);
			AddonTagCheckBox.Name = "AddonTagCheckBox";
			AddonTagCheckBox.Size = new System.Drawing.Size(61, 17);
			AddonTagCheckBox.TabIndex = 9;
			AddonTagCheckBox.Tag = "Addon";
			AddonTagCheckBox.Text = "Addon";
			AddonTagCheckBox.UseVisualStyleBackColor = true;
			//
			//Label2
			//
			Label2.AutoSize = true;
			Label2.Location = new System.Drawing.Point(3, 262);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(63, 13);
			Label2.TabIndex = 10;
			Label2.Text = "Always set:";
			//
			//GarrysModTagsUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Label2);
			Controls.Add(AddonTagCheckBox);
			Controls.Add(GroupBox1);
			Controls.Add(Label1);
			Controls.Add(ComboBox1);
			Name = "GarrysModTagsUserControl";
			Size = new System.Drawing.Size(168, 299);
			GroupBox1.ResumeLayout(false);
			GroupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();

		}

		internal ComboBox ComboBox1;
		internal Label Label1;
		internal CheckBoxEx CheckBox1;
		internal CheckBoxEx CheckBox2;
		internal CheckBoxEx CheckBox3;
		internal CheckBoxEx CheckBox4;
		internal CheckBoxEx CheckBox5;
		internal CheckBoxEx CheckBox6;
		internal CheckBoxEx CheckBox7;
		internal CheckBoxEx CheckBox8;
		internal CheckBoxEx CheckBox9;
		internal GroupBoxEx GroupBox1;
		internal CheckBoxEx AddonTagCheckBox;
		internal Label Label2;
	}

}