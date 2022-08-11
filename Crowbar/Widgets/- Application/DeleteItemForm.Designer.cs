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
			TextBox1 = new Crowbar.TextBoxEx();
			DeleteButton = new System.Windows.Forms.Button();
			CancelDeleteButton = new System.Windows.Forms.Button();
			SuspendLayout();
			//
			//TextBox1
			//
			TextBox1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			TextBox1.CueBannerText = "";
			TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			TextBox1.Location = new System.Drawing.Point(12, 12);
			TextBox1.Multiline = true;
			TextBox1.Name = "TextBox1";
			TextBox1.ReadOnly = true;
			TextBox1.Size = new System.Drawing.Size(420, 50);
			TextBox1.TabIndex = 0;
			TextBox1.TabStop = false;
			TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//DeleteButton
			//
			DeleteButton.Location = new System.Drawing.Point(144, 68);
			DeleteButton.Name = "DeleteButton";
			DeleteButton.Size = new System.Drawing.Size(75, 23);
			DeleteButton.TabIndex = 1;
			DeleteButton.Text = "Delete";
			DeleteButton.UseVisualStyleBackColor = true;
			//
			//CancelDeleteButton
			//
			CancelDeleteButton.Location = new System.Drawing.Point(225, 68);
			CancelDeleteButton.Name = "CancelDeleteButton";
			CancelDeleteButton.Size = new System.Drawing.Size(75, 23);
			CancelDeleteButton.TabIndex = 2;
			CancelDeleteButton.Text = "Cancel";
			CancelDeleteButton.UseVisualStyleBackColor = true;
			//
			//DeleteItemForm
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(444, 113);
			ControlBox = false;
			Controls.Add(CancelDeleteButton);
			Controls.Add(DeleteButton);
			Controls.Add(TextBox1);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			Name = "DeleteItemForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Delete Item";
			TopMost = true;
			ResumeLayout(false);
			PerformLayout();

			DeleteButton.Click += new System.EventHandler(DeleteButton_Click);
			CancelDeleteButton.Click += new System.EventHandler(CancelDeleteButton_Click);
		}

		internal TextBoxEx TextBox1;
		internal Button DeleteButton;
		internal Button CancelDeleteButton;
	}

}