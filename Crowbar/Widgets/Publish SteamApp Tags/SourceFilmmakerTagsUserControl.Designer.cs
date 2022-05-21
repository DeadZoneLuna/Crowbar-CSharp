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
			ComboBox1 = new System.Windows.Forms.ComboBox();
			ComboBox2 = new System.Windows.Forms.ComboBox();
			ComboBox3 = new System.Windows.Forms.ComboBox();
			SuspendLayout();
			//
			//ComboBox1
			//
			ComboBox1.FormattingEnabled = true;
			ComboBox1.Location = new System.Drawing.Point(3, 3);
			ComboBox1.Name = "ComboBox1";
			ComboBox1.Size = new System.Drawing.Size(121, 21);
			ComboBox1.TabIndex = 0;
			//
			//ComboBox2
			//
			ComboBox2.FormattingEnabled = true;
			ComboBox2.Location = new System.Drawing.Point(3, 30);
			ComboBox2.Name = "ComboBox2";
			ComboBox2.Size = new System.Drawing.Size(121, 21);
			ComboBox2.TabIndex = 1;
			//
			//ComboBox3
			//
			ComboBox3.FormattingEnabled = true;
			ComboBox3.Location = new System.Drawing.Point(3, 57);
			ComboBox3.Name = "ComboBox3";
			ComboBox3.Size = new System.Drawing.Size(121, 21);
			ComboBox3.TabIndex = 2;
			//
			//SourceFilmmakerTagsUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(ComboBox3);
			Controls.Add(ComboBox2);
			Controls.Add(ComboBox1);
			Name = "SourceFilmmakerTagsUserControl";
			Size = new System.Drawing.Size(292, 418);
			ResumeLayout(false);

		}

		internal ComboBox ComboBox1;
		internal ComboBox ComboBox2;
		internal ComboBox ComboBox3;
	}

}