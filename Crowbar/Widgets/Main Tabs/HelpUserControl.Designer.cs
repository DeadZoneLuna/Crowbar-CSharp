using System.IO;

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
	public partial class HelpUserControl : BaseUserControl
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
			this.TutorialLinkLabel = new System.Windows.Forms.LinkLabel();
			this.ContentsLinkLabel = new System.Windows.Forms.LinkLabel();
			this.IndexLinkLabel = new System.Windows.Forms.LinkLabel();
			this.TipsLinkLabel = new System.Windows.Forms.LinkLabel();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label6 = new System.Windows.Forms.Label();
			this.CrowbarGuideButton = new System.Windows.Forms.Button();
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			//TutorialLinkLabel
			//
			this.TutorialLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime;
			this.TutorialLinkLabel.AutoSize = true;
			this.TutorialLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.TutorialLinkLabel.LinkColor = System.Drawing.Color.Green;
			this.TutorialLinkLabel.Location = new System.Drawing.Point(6, 16);
			this.TutorialLinkLabel.Name = "TutorialLinkLabel";
			this.TutorialLinkLabel.Size = new System.Drawing.Size(84, 25);
			this.TutorialLinkLabel.TabIndex = 1;
			this.TutorialLinkLabel.TabStop = true;
			this.TutorialLinkLabel.Text = "Tutorial";
			this.TutorialLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//ContentsLinkLabel
			//
			this.ContentsLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime;
			this.ContentsLinkLabel.AutoSize = true;
			this.ContentsLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.ContentsLinkLabel.LinkColor = System.Drawing.Color.Green;
			this.ContentsLinkLabel.Location = new System.Drawing.Point(6, 41);
			this.ContentsLinkLabel.Name = "ContentsLinkLabel";
			this.ContentsLinkLabel.Size = new System.Drawing.Size(98, 25);
			this.ContentsLinkLabel.TabIndex = 2;
			this.ContentsLinkLabel.TabStop = true;
			this.ContentsLinkLabel.Text = "Contents";
			this.ContentsLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//IndexLinkLabel
			//
			this.IndexLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime;
			this.IndexLinkLabel.AutoSize = true;
			this.IndexLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.IndexLinkLabel.LinkColor = System.Drawing.Color.Green;
			this.IndexLinkLabel.Location = new System.Drawing.Point(6, 66);
			this.IndexLinkLabel.Name = "IndexLinkLabel";
			this.IndexLinkLabel.Size = new System.Drawing.Size(64, 25);
			this.IndexLinkLabel.TabIndex = 3;
			this.IndexLinkLabel.TabStop = true;
			this.IndexLinkLabel.Text = "Index";
			this.IndexLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//TipsLinkLabel
			//
			this.TipsLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime;
			this.TipsLinkLabel.AutoSize = true;
			this.TipsLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.TipsLinkLabel.LinkColor = System.Drawing.Color.Green;
			this.TipsLinkLabel.Location = new System.Drawing.Point(6, 91);
			this.TipsLinkLabel.Name = "TipsLinkLabel";
			this.TipsLinkLabel.Size = new System.Drawing.Size(53, 25);
			this.TipsLinkLabel.TabIndex = 4;
			this.TipsLinkLabel.TabStop = true;
			this.TipsLinkLabel.Text = "Tips";
			this.TipsLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.Label4);
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Controls.Add(this.Label2);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Controls.Add(this.TutorialLinkLabel);
			this.GroupBox1.Controls.Add(this.TipsLinkLabel);
			this.GroupBox1.Controls.Add(this.ContentsLinkLabel);
			this.GroupBox1.Controls.Add(this.IndexLinkLabel);
			this.GroupBox1.Location = new System.Drawing.Point(49, 388);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(640, 132);
			this.GroupBox1.TabIndex = 5;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Visible = false;
			//
			//Label4
			//
			this.Label4.AutoSize = true;
			this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.Label4.Location = new System.Drawing.Point(110, 92);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(407, 24);
			this.Label4.TabIndex = 8;
			this.Label4.Text = "Ways to use Crowbar that might not be obvious.";
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.Label3.Location = new System.Drawing.Point(110, 67);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(462, 24);
			this.Label3.TabIndex = 7;
			this.Label3.Text = "Links to where important words and phrases are used.";
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.Label2.Location = new System.Drawing.Point(110, 42);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(317, 24);
			this.Label2.TabIndex = 6;
			this.Label2.Text = "Documentation arranged in sections.";
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.Label1.Location = new System.Drawing.Point(110, 17);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(464, 24);
			this.Label1.TabIndex = 5;
			this.Label1.Text = "Step-by-step guide on how to use most of the features.";
			//
			//Label5
			//
			this.Label5.AutoSize = true;
			this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.Label5.Location = new System.Drawing.Point(49, 337);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(577, 16);
			this.Label5.TabIndex = 7;
			this.Label5.Text = "Crowbar allows you to quickly access many tools for modding models for Source-eng" + "ine games.";
			this.Label5.Visible = false;
			//
			//Label6
			//
			this.Label6.AutoSize = true;
			this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			this.Label6.Location = new System.Drawing.Point(49, 361);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(419, 16);
			this.Label6.TabIndex = 8;
			this.Label6.Text = "(The links below will open as web pages in your default web browser.)";
			this.Label6.Visible = false;
			//
			//CrowbarGuideButton
			//
			this.CrowbarGuideButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CrowbarGuideButton.Image = global::Crowbar.Properties.Resources.CrowbarGuideBanner;
			this.CrowbarGuideButton.Location = new System.Drawing.Point(3, 3);
			this.CrowbarGuideButton.Name = "CrowbarGuideButton";
			this.CrowbarGuideButton.Size = new System.Drawing.Size(530, 147);
			this.CrowbarGuideButton.TabIndex = 9;
			this.CrowbarGuideButton.UseVisualStyleBackColor = true;
			//
			//HelpUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CrowbarGuideButton);
			this.Controls.Add(this.Label6);
			this.Controls.Add(this.Label5);
			this.Controls.Add(this.GroupBox1);
			this.Name = "HelpUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

//INSTANT C# NOTE: Converted design-time event handler wireups:
			CrowbarGuideButton.Click += new System.EventHandler(CrowbarGuideButton_Click);
			TutorialLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
			ContentsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
			IndexLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
			TipsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
		}
		internal System.Windows.Forms.LinkLabel TutorialLinkLabel;
		internal System.Windows.Forms.LinkLabel ContentsLinkLabel;
		internal System.Windows.Forms.LinkLabel IndexLinkLabel;
		internal System.Windows.Forms.LinkLabel TipsLinkLabel;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Button CrowbarGuideButton;

	}

}