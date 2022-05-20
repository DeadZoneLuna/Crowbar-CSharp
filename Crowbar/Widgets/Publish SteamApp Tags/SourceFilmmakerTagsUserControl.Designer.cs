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
	public partial class SourceFilmmakerTagsUserControl : Base_TagsUserControl
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
			this.ComboBox2 = new System.Windows.Forms.ComboBox();
			this.ComboBox3 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			//
			//ComboBox1
			//
			this.ComboBox1.FormattingEnabled = true;
			this.ComboBox1.Location = new System.Drawing.Point(3, 3);
			this.ComboBox1.Name = "ComboBox1";
			this.ComboBox1.Size = new System.Drawing.Size(121, 21);
			this.ComboBox1.TabIndex = 0;
			//
			//ComboBox2
			//
			this.ComboBox2.FormattingEnabled = true;
			this.ComboBox2.Location = new System.Drawing.Point(3, 30);
			this.ComboBox2.Name = "ComboBox2";
			this.ComboBox2.Size = new System.Drawing.Size(121, 21);
			this.ComboBox2.TabIndex = 1;
			//
			//ComboBox3
			//
			this.ComboBox3.FormattingEnabled = true;
			this.ComboBox3.Location = new System.Drawing.Point(3, 57);
			this.ComboBox3.Name = "ComboBox3";
			this.ComboBox3.Size = new System.Drawing.Size(121, 21);
			this.ComboBox3.TabIndex = 2;
			//
			//SourceFilmmakerTagsUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ComboBox3);
			this.Controls.Add(this.ComboBox2);
			this.Controls.Add(this.ComboBox1);
			this.Name = "SourceFilmmakerTagsUserControl";
			this.Size = new System.Drawing.Size(292, 418);
			this.ResumeLayout(false);

		}

		internal ComboBox ComboBox1;
		internal ComboBox ComboBox2;
		internal ComboBox ComboBox3;
	}

}