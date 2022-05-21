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
			TutorialLinkLabel = new System.Windows.Forms.LinkLabel();
			ContentsLinkLabel = new System.Windows.Forms.LinkLabel();
			IndexLinkLabel = new System.Windows.Forms.LinkLabel();
			TipsLinkLabel = new System.Windows.Forms.LinkLabel();
			GroupBox1 = new System.Windows.Forms.GroupBox();
			Label4 = new System.Windows.Forms.Label();
			Label3 = new System.Windows.Forms.Label();
			Label2 = new System.Windows.Forms.Label();
			Label1 = new System.Windows.Forms.Label();
			Label5 = new System.Windows.Forms.Label();
			Label6 = new System.Windows.Forms.Label();
			CrowbarGuideButton = new System.Windows.Forms.Button();
			GroupBox1.SuspendLayout();
			SuspendLayout();
			//
			//TutorialLinkLabel
			//
			TutorialLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime;
			TutorialLinkLabel.AutoSize = true;
			TutorialLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			TutorialLinkLabel.LinkColor = System.Drawing.Color.Green;
			TutorialLinkLabel.Location = new System.Drawing.Point(6, 16);
			TutorialLinkLabel.Name = "TutorialLinkLabel";
			TutorialLinkLabel.Size = new System.Drawing.Size(84, 25);
			TutorialLinkLabel.TabIndex = 1;
			TutorialLinkLabel.TabStop = true;
			TutorialLinkLabel.Text = "Tutorial";
			TutorialLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//ContentsLinkLabel
			//
			ContentsLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime;
			ContentsLinkLabel.AutoSize = true;
			ContentsLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			ContentsLinkLabel.LinkColor = System.Drawing.Color.Green;
			ContentsLinkLabel.Location = new System.Drawing.Point(6, 41);
			ContentsLinkLabel.Name = "ContentsLinkLabel";
			ContentsLinkLabel.Size = new System.Drawing.Size(98, 25);
			ContentsLinkLabel.TabIndex = 2;
			ContentsLinkLabel.TabStop = true;
			ContentsLinkLabel.Text = "Contents";
			ContentsLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//IndexLinkLabel
			//
			IndexLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime;
			IndexLinkLabel.AutoSize = true;
			IndexLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			IndexLinkLabel.LinkColor = System.Drawing.Color.Green;
			IndexLinkLabel.Location = new System.Drawing.Point(6, 66);
			IndexLinkLabel.Name = "IndexLinkLabel";
			IndexLinkLabel.Size = new System.Drawing.Size(64, 25);
			IndexLinkLabel.TabIndex = 3;
			IndexLinkLabel.TabStop = true;
			IndexLinkLabel.Text = "Index";
			IndexLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//TipsLinkLabel
			//
			TipsLinkLabel.ActiveLinkColor = System.Drawing.Color.Lime;
			TipsLinkLabel.AutoSize = true;
			TipsLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			TipsLinkLabel.LinkColor = System.Drawing.Color.Green;
			TipsLinkLabel.Location = new System.Drawing.Point(6, 91);
			TipsLinkLabel.Name = "TipsLinkLabel";
			TipsLinkLabel.Size = new System.Drawing.Size(53, 25);
			TipsLinkLabel.TabIndex = 4;
			TipsLinkLabel.TabStop = true;
			TipsLinkLabel.Text = "Tips";
			TipsLinkLabel.VisitedLinkColor = System.Drawing.Color.Green;
			//
			//GroupBox1
			//
			GroupBox1.Controls.Add(Label4);
			GroupBox1.Controls.Add(Label3);
			GroupBox1.Controls.Add(Label2);
			GroupBox1.Controls.Add(Label1);
			GroupBox1.Controls.Add(TutorialLinkLabel);
			GroupBox1.Controls.Add(TipsLinkLabel);
			GroupBox1.Controls.Add(ContentsLinkLabel);
			GroupBox1.Controls.Add(IndexLinkLabel);
			GroupBox1.Location = new System.Drawing.Point(49, 388);
			GroupBox1.Name = "GroupBox1";
			GroupBox1.Size = new System.Drawing.Size(640, 132);
			GroupBox1.TabIndex = 5;
			GroupBox1.TabStop = false;
			GroupBox1.Visible = false;
			//
			//Label4
			//
			Label4.AutoSize = true;
			Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			Label4.Location = new System.Drawing.Point(110, 92);
			Label4.Name = "Label4";
			Label4.Size = new System.Drawing.Size(407, 24);
			Label4.TabIndex = 8;
			Label4.Text = "Ways to use Crowbar that might not be obvious.";
			//
			//Label3
			//
			Label3.AutoSize = true;
			Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			Label3.Location = new System.Drawing.Point(110, 67);
			Label3.Name = "Label3";
			Label3.Size = new System.Drawing.Size(462, 24);
			Label3.TabIndex = 7;
			Label3.Text = "Links to where important words and phrases are used.";
			//
			//Label2
			//
			Label2.AutoSize = true;
			Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			Label2.Location = new System.Drawing.Point(110, 42);
			Label2.Name = "Label2";
			Label2.Size = new System.Drawing.Size(317, 24);
			Label2.TabIndex = 6;
			Label2.Text = "Documentation arranged in sections.";
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			Label1.Location = new System.Drawing.Point(110, 17);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(464, 24);
			Label1.TabIndex = 5;
			Label1.Text = "Step-by-step guide on how to use most of the features.";
			//
			//Label5
			//
			Label5.AutoSize = true;
			Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			Label5.Location = new System.Drawing.Point(49, 337);
			Label5.Name = "Label5";
			Label5.Size = new System.Drawing.Size(577, 16);
			Label5.TabIndex = 7;
			Label5.Text = "Crowbar allows you to quickly access many tools for modding models for Source-eng" + "ine games.";
			Label5.Visible = false;
			//
			//Label6
			//
			Label6.AutoSize = true;
			Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
			Label6.Location = new System.Drawing.Point(49, 361);
			Label6.Name = "Label6";
			Label6.Size = new System.Drawing.Size(419, 16);
			Label6.TabIndex = 8;
			Label6.Text = "(The links below will open as web pages in your default web browser.)";
			Label6.Visible = false;
			//
			//CrowbarGuideButton
			//
			CrowbarGuideButton.Cursor = System.Windows.Forms.Cursors.Hand;
			CrowbarGuideButton.Image = global::Crowbar.Properties.Resources.CrowbarGuideBanner;
			CrowbarGuideButton.Location = new System.Drawing.Point(3, 3);
			CrowbarGuideButton.Name = "CrowbarGuideButton";
			CrowbarGuideButton.Size = new System.Drawing.Size(530, 147);
			CrowbarGuideButton.TabIndex = 9;
			CrowbarGuideButton.UseVisualStyleBackColor = true;
			//
			//HelpUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(CrowbarGuideButton);
			Controls.Add(Label6);
			Controls.Add(Label5);
			Controls.Add(GroupBox1);
			Name = "HelpUserControl";
			Size = new System.Drawing.Size(776, 536);
			GroupBox1.ResumeLayout(false);
			GroupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();

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