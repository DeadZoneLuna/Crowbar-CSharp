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
			this.ComboBox1 = new System.Windows.Forms.ComboBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.CheckBox1 = new Crowbar.CheckBoxEx();
			this.CheckBox2 = new Crowbar.CheckBoxEx();
			this.CheckBox3 = new Crowbar.CheckBoxEx();
			this.CheckBox4 = new Crowbar.CheckBoxEx();
			this.CheckBox5 = new Crowbar.CheckBoxEx();
			this.CheckBox6 = new Crowbar.CheckBoxEx();
			this.CheckBox7 = new Crowbar.CheckBoxEx();
			this.CheckBox8 = new Crowbar.CheckBoxEx();
			this.CheckBox9 = new Crowbar.CheckBoxEx();
			this.GroupBox1 = new Crowbar.GroupBoxEx();
			this.AddonTagCheckBox = new Crowbar.CheckBoxEx();
			this.Label2 = new System.Windows.Forms.Label();
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			//ComboBox1
			//
			this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBox1.FormattingEnabled = true;
			this.ComboBox1.Location = new System.Drawing.Point(42, 0);
			this.ComboBox1.Name = "ComboBox1";
			this.ComboBox1.Size = new System.Drawing.Size(110, 21);
			this.ComboBox1.TabIndex = 1;
			this.ComboBox1.Tag = "TagsEnabled";
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(3, 4);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(33, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Type:";
			//
			//CheckBox1
			//
			this.CheckBox1.AutoSize = true;
			this.CheckBox1.IsReadOnly = false;
			this.CheckBox1.Location = new System.Drawing.Point(6, 20);
			this.CheckBox1.Name = "CheckBox1";
			this.CheckBox1.Size = new System.Drawing.Size(52, 17);
			this.CheckBox1.TabIndex = 0;
			this.CheckBox1.Tag = "Build";
			this.CheckBox1.Text = "Build";
			this.CheckBox1.UseVisualStyleBackColor = true;
			//
			//CheckBox2
			//
			this.CheckBox2.AutoSize = true;
			this.CheckBox2.IsReadOnly = false;
			this.CheckBox2.Location = new System.Drawing.Point(6, 43);
			this.CheckBox2.Name = "CheckBox2";
			this.CheckBox2.Size = new System.Drawing.Size(68, 17);
			this.CheckBox2.TabIndex = 1;
			this.CheckBox2.Tag = "Cartoon";
			this.CheckBox2.Text = "Cartoon";
			this.CheckBox2.UseVisualStyleBackColor = true;
			//
			//CheckBox3
			//
			this.CheckBox3.AutoSize = true;
			this.CheckBox3.IsReadOnly = false;
			this.CheckBox3.Location = new System.Drawing.Point(6, 66);
			this.CheckBox3.Name = "CheckBox3";
			this.CheckBox3.Size = new System.Drawing.Size(57, 17);
			this.CheckBox3.TabIndex = 2;
			this.CheckBox3.Tag = "Comic";
			this.CheckBox3.Text = "Comic";
			this.CheckBox3.UseVisualStyleBackColor = true;
			//
			//CheckBox4
			//
			this.CheckBox4.AutoSize = true;
			this.CheckBox4.IsReadOnly = false;
			this.CheckBox4.Location = new System.Drawing.Point(6, 89);
			this.CheckBox4.Name = "CheckBox4";
			this.CheckBox4.Size = new System.Drawing.Size(46, 17);
			this.CheckBox4.TabIndex = 3;
			this.CheckBox4.Tag = "Fun";
			this.CheckBox4.Text = "Fun";
			this.CheckBox4.UseVisualStyleBackColor = true;
			//
			//CheckBox5
			//
			this.CheckBox5.AutoSize = true;
			this.CheckBox5.IsReadOnly = false;
			this.CheckBox5.Location = new System.Drawing.Point(6, 112);
			this.CheckBox5.Name = "CheckBox5";
			this.CheckBox5.Size = new System.Drawing.Size(57, 17);
			this.CheckBox5.TabIndex = 4;
			this.CheckBox5.Tag = "Movie";
			this.CheckBox5.Text = "Movie";
			this.CheckBox5.UseVisualStyleBackColor = true;
			//
			//CheckBox6
			//
			this.CheckBox6.AutoSize = true;
			this.CheckBox6.IsReadOnly = false;
			this.CheckBox6.Location = new System.Drawing.Point(6, 135);
			this.CheckBox6.Name = "CheckBox6";
			this.CheckBox6.Size = new System.Drawing.Size(65, 17);
			this.CheckBox6.TabIndex = 5;
			this.CheckBox6.Tag = "Realism";
			this.CheckBox6.Text = "Realism";
			this.CheckBox6.UseVisualStyleBackColor = true;
			//
			//CheckBox7
			//
			this.CheckBox7.AutoSize = true;
			this.CheckBox7.IsReadOnly = false;
			this.CheckBox7.Location = new System.Drawing.Point(6, 158);
			this.CheckBox7.Name = "CheckBox7";
			this.CheckBox7.Size = new System.Drawing.Size(70, 17);
			this.CheckBox7.TabIndex = 6;
			this.CheckBox7.Tag = "Roleplay";
			this.CheckBox7.Text = "Roleplay";
			this.CheckBox7.UseVisualStyleBackColor = true;
			//
			//CheckBox8
			//
			this.CheckBox8.AutoSize = true;
			this.CheckBox8.IsReadOnly = false;
			this.CheckBox8.Location = new System.Drawing.Point(6, 181);
			this.CheckBox8.Name = "CheckBox8";
			this.CheckBox8.Size = new System.Drawing.Size(58, 17);
			this.CheckBox8.TabIndex = 7;
			this.CheckBox8.Tag = "Scenic";
			this.CheckBox8.Text = "Scenic";
			this.CheckBox8.UseVisualStyleBackColor = true;
			//
			//CheckBox9
			//
			this.CheckBox9.AutoSize = true;
			this.CheckBox9.IsReadOnly = false;
			this.CheckBox9.Location = new System.Drawing.Point(6, 204);
			this.CheckBox9.Name = "CheckBox9";
			this.CheckBox9.Size = new System.Drawing.Size(57, 17);
			this.CheckBox9.TabIndex = 8;
			this.CheckBox9.Tag = "Water";
			this.CheckBox9.Text = "Water";
			this.CheckBox9.UseVisualStyleBackColor = true;
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.CheckBox1);
			this.GroupBox1.Controls.Add(this.CheckBox9);
			this.GroupBox1.Controls.Add(this.CheckBox2);
			this.GroupBox1.Controls.Add(this.CheckBox8);
			this.GroupBox1.Controls.Add(this.CheckBox3);
			this.GroupBox1.Controls.Add(this.CheckBox7);
			this.GroupBox1.Controls.Add(this.CheckBox4);
			this.GroupBox1.Controls.Add(this.CheckBox6);
			this.GroupBox1.Controls.Add(this.CheckBox5);
			this.GroupBox1.IsReadOnly = false;
			this.GroupBox1.Location = new System.Drawing.Point(3, 27);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.SelectedValue = null;
			this.GroupBox1.Size = new System.Drawing.Size(151, 228);
			this.GroupBox1.TabIndex = 2;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Choose up to two";
			//
			//AddonTagCheckBox
			//
			this.AddonTagCheckBox.AutoSize = true;
			this.AddonTagCheckBox.Enabled = false;
			this.AddonTagCheckBox.IsReadOnly = false;
			this.AddonTagCheckBox.Location = new System.Drawing.Point(72, 261);
			this.AddonTagCheckBox.Name = "AddonTagCheckBox";
			this.AddonTagCheckBox.Size = new System.Drawing.Size(61, 17);
			this.AddonTagCheckBox.TabIndex = 9;
			this.AddonTagCheckBox.Tag = "Addon";
			this.AddonTagCheckBox.Text = "Addon";
			this.AddonTagCheckBox.UseVisualStyleBackColor = true;
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(3, 262);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(63, 13);
			this.Label2.TabIndex = 10;
			this.Label2.Text = "Always set:";
			//
			//GarrysModTagsUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.AddonTagCheckBox);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.ComboBox1);
			this.Name = "GarrysModTagsUserControl";
			this.Size = new System.Drawing.Size(168, 299);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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