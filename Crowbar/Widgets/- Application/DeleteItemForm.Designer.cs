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
	public partial class DeleteItemForm : System.Windows.Forms.Form
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
			this.TextBox1 = new Crowbar.TextBoxEx();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.CancelDeleteButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//TextBox1
			//
			this.TextBox1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox1.CueBannerText = "";
			this.TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.TextBox1.Location = new System.Drawing.Point(12, 12);
			this.TextBox1.Multiline = true;
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.ReadOnly = true;
			this.TextBox1.Size = new System.Drawing.Size(420, 50);
			this.TextBox1.TabIndex = 0;
			this.TextBox1.TabStop = false;
			this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//DeleteButton
			//
			this.DeleteButton.Location = new System.Drawing.Point(144, 68);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(75, 23);
			this.DeleteButton.TabIndex = 1;
			this.DeleteButton.Text = "Delete";
			this.DeleteButton.UseVisualStyleBackColor = true;
			//
			//CancelDeleteButton
			//
			this.CancelDeleteButton.Location = new System.Drawing.Point(225, 68);
			this.CancelDeleteButton.Name = "CancelDeleteButton";
			this.CancelDeleteButton.Size = new System.Drawing.Size(75, 23);
			this.CancelDeleteButton.TabIndex = 2;
			this.CancelDeleteButton.Text = "Cancel";
			this.CancelDeleteButton.UseVisualStyleBackColor = true;
			//
			//DeleteItemForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 113);
			this.ControlBox = false;
			this.Controls.Add(this.CancelDeleteButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.TextBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DeleteItemForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Delete Item";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

//INSTANT C# NOTE: Converted design-time event handler wireups:
			DeleteButton.Click += new System.EventHandler(DeleteButton_Click);
			CancelDeleteButton.Click += new System.EventHandler(CancelDeleteButton_Click);
		}

		internal TextBoxEx TextBox1;
		internal Button DeleteButton;
		internal Button CancelDeleteButton;
	}

}