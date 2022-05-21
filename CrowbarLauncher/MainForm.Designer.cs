using System.Collections.ObjectModel;
using System.IO;

//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;

namespace CrowbarLauncher
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		//Form overrides dispose to clean up the component list.
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			TextBox1 = new System.Windows.Forms.TextBox();
			TextBox2 = new System.Windows.Forms.TextBox();
			SuspendLayout();
			//
			//TextBox1
			//
			TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TextBox1.Location = new System.Drawing.Point(12, 12);
			TextBox1.Name = "TextBox1";
			TextBox1.ReadOnly = true;
			TextBox1.Size = new System.Drawing.Size(268, 13);
			TextBox1.TabIndex = 0;
			TextBox1.Text = "Updating Crowbar.";
			//
			//TextBox2
			//
			TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TextBox2.Location = new System.Drawing.Point(12, 31);
			TextBox2.Name = "TextBox2";
			TextBox2.ReadOnly = true;
			TextBox2.Size = new System.Drawing.Size(268, 13);
			TextBox2.TabIndex = 1;
			TextBox2.Text = "This should only take a few moments.";
			//
			//MainForm
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(292, 97);
			Controls.Add(TextBox2);
			Controls.Add(TextBox1);
			Icon = (System.Drawing.Icon)resources.GetObject("$Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "MainForm";
			SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Crowbar Launcher";
			TopMost = true;
			ResumeLayout(false);
			PerformLayout();

//INSTANT C# NOTE: Converted design-time event handler wireups:
			Load += new System.EventHandler(MainForm_Load);
		}

		internal TextBox TextBox1;
		internal TextBox TextBox2;
	}

}