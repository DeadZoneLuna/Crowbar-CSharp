using System.IO;
using System.Text;

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
	public partial class ViewUserControl : BaseUserControl
	{
		//'UserControl overrides dispose to clean up the component list.
		//<System.Diagnostics.DebuggerNonUserCode()> _
		//Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		//	Try
		//		If disposing AndAlso components IsNot Nothing Then
		//			components.Dispose()
		//		End If
		//	Finally
		//		MyBase.Dispose(disposing)
		//	End Try
		//End Sub

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ViewButton = new System.Windows.Forms.Button();
			this.MdlPathFileNameTextBox = new Crowbar.TextBoxEx();
			this.BrowseForMdlFileButton = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.Panel2 = new System.Windows.Forms.Panel();
			this.OverrideMdlVersionLabel = new System.Windows.Forms.Label();
			this.OverrideMdlVersionComboBox = new System.Windows.Forms.ComboBox();
			this.GotoMdlFileButton = new System.Windows.Forms.Button();
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.InfoRichTextBox = new Crowbar.RichTextBoxEx();
			this.GameLabel = new System.Windows.Forms.Label();
			this.GameSetupComboBox = new System.Windows.Forms.ComboBox();
			this.SetUpGameButton = new System.Windows.Forms.Button();
			this.ViewAsReplacementButton = new System.Windows.Forms.Button();
			this.UseInDecompileButton = new System.Windows.Forms.Button();
			this.OpenViewerButton = new System.Windows.Forms.Button();
			this.OpenMappingToolButton = new System.Windows.Forms.Button();
			this.RunGameButton = new System.Windows.Forms.Button();
			this.MessageTextBox = new Crowbar.TextBoxEx();
			this.Panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			//ViewButton
			//
			this.ViewButton.Enabled = false;
			this.ViewButton.Location = new System.Drawing.Point(0, 32);
			this.ViewButton.Name = "ViewButton";
			this.ViewButton.Size = new System.Drawing.Size(40, 23);
			this.ViewButton.TabIndex = 8;
			this.ViewButton.Text = "View";
			this.ViewButton.UseVisualStyleBackColor = true;
			//
			//MdlPathFileNameTextBox
			//
			this.MdlPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.MdlPathFileNameTextBox.CueBannerText = "";
			this.MdlPathFileNameTextBox.Location = new System.Drawing.Point(58, 3);
			this.MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox";
			this.MdlPathFileNameTextBox.Size = new System.Drawing.Size(596, 22);
			this.MdlPathFileNameTextBox.TabIndex = 1;
			//
			//BrowseForMdlFileButton
			//
			this.BrowseForMdlFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseForMdlFileButton.Location = new System.Drawing.Point(660, 3);
			this.BrowseForMdlFileButton.Name = "BrowseForMdlFileButton";
			this.BrowseForMdlFileButton.Size = new System.Drawing.Size(64, 23);
			this.BrowseForMdlFileButton.TabIndex = 2;
			this.BrowseForMdlFileButton.Text = "Browse...";
			this.BrowseForMdlFileButton.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(3, 8);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(52, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "MDL file:";
			//
			//Panel2
			//
			this.Panel2.Controls.Add(this.OverrideMdlVersionLabel);
			this.Panel2.Controls.Add(this.Label1);
			this.Panel2.Controls.Add(this.MdlPathFileNameTextBox);
			this.Panel2.Controls.Add(this.OverrideMdlVersionComboBox);
			this.Panel2.Controls.Add(this.BrowseForMdlFileButton);
			this.Panel2.Controls.Add(this.GotoMdlFileButton);
			this.Panel2.Controls.Add(this.SplitContainer1);
			this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel2.Location = new System.Drawing.Point(0, 0);
			this.Panel2.Margin = new System.Windows.Forms.Padding(2);
			this.Panel2.Name = "Panel2";
			this.Panel2.Size = new System.Drawing.Size(776, 536);
			this.Panel2.TabIndex = 8;
			//
			//OverrideMdlVersionLabel
			//
			this.OverrideMdlVersionLabel.AutoSize = true;
			this.OverrideMdlVersionLabel.Location = new System.Drawing.Point(3, 36);
			this.OverrideMdlVersionLabel.Name = "OverrideMdlVersionLabel";
			this.OverrideMdlVersionLabel.Size = new System.Drawing.Size(120, 13);
			this.OverrideMdlVersionLabel.TabIndex = 48;
			this.OverrideMdlVersionLabel.Text = "Override MDL version:";
			//
			//OverrideMdlVersionComboBox
			//
			this.OverrideMdlVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OverrideMdlVersionComboBox.FormattingEnabled = true;
			this.OverrideMdlVersionComboBox.Location = new System.Drawing.Point(123, 32);
			this.OverrideMdlVersionComboBox.Name = "OverrideMdlVersionComboBox";
			this.OverrideMdlVersionComboBox.Size = new System.Drawing.Size(110, 21);
			this.OverrideMdlVersionComboBox.TabIndex = 47;
			//
			//GotoMdlFileButton
			//
			this.GotoMdlFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GotoMdlFileButton.Location = new System.Drawing.Point(730, 3);
			this.GotoMdlFileButton.Name = "GotoMdlFileButton";
			this.GotoMdlFileButton.Size = new System.Drawing.Size(43, 23);
			this.GotoMdlFileButton.TabIndex = 3;
			this.GotoMdlFileButton.Text = "Goto";
			this.GotoMdlFileButton.UseVisualStyleBackColor = true;
			//
			//SplitContainer1
			//
			this.SplitContainer1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.SplitContainer1.Location = new System.Drawing.Point(3, 59);
			this.SplitContainer1.Name = "SplitContainer1";
			this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//SplitContainer1.Panel1
			//
			this.SplitContainer1.Panel1.Controls.Add(this.GroupBox1);
			this.SplitContainer1.Panel1MinSize = 45;
			//
			//SplitContainer1.Panel2
			//
			this.SplitContainer1.Panel2.Controls.Add(this.GameLabel);
			this.SplitContainer1.Panel2.Controls.Add(this.GameSetupComboBox);
			this.SplitContainer1.Panel2.Controls.Add(this.SetUpGameButton);
			this.SplitContainer1.Panel2.Controls.Add(this.ViewButton);
			this.SplitContainer1.Panel2.Controls.Add(this.ViewAsReplacementButton);
			this.SplitContainer1.Panel2.Controls.Add(this.UseInDecompileButton);
			this.SplitContainer1.Panel2.Controls.Add(this.OpenViewerButton);
			this.SplitContainer1.Panel2.Controls.Add(this.OpenMappingToolButton);
			this.SplitContainer1.Panel2.Controls.Add(this.RunGameButton);
			this.SplitContainer1.Panel2.Controls.Add(this.MessageTextBox);
			this.SplitContainer1.Panel2MinSize = 45;
			this.SplitContainer1.Size = new System.Drawing.Size(770, 474);
			this.SplitContainer1.SplitterDistance = 363;
			this.SplitContainer1.TabIndex = 13;
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.InfoRichTextBox);
			this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GroupBox1.Location = new System.Drawing.Point(0, 0);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(770, 363);
			this.GroupBox1.TabIndex = 4;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Info";
			//
			//InfoRichTextBox
			//
			this.InfoRichTextBox.CueBannerText = "";
			this.InfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InfoRichTextBox.Location = new System.Drawing.Point(3, 18);
			this.InfoRichTextBox.Name = "InfoRichTextBox";
			this.InfoRichTextBox.ReadOnly = true;
			this.InfoRichTextBox.Size = new System.Drawing.Size(764, 342);
			this.InfoRichTextBox.TabIndex = 0;
			this.InfoRichTextBox.Text = "";
			this.InfoRichTextBox.WordWrap = false;
			//
			//GameLabel
			//
			this.GameLabel.AutoSize = true;
			this.GameLabel.Location = new System.Drawing.Point(0, 8);
			this.GameLabel.Name = "GameLabel";
			this.GameLabel.Size = new System.Drawing.Size(175, 13);
			this.GameLabel.TabIndex = 5;
			this.GameLabel.Text = "Game that has the model viewer:";
			//
			//GameSetupComboBox
			//
			this.GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GameSetupComboBox.FormattingEnabled = true;
			this.GameSetupComboBox.Location = new System.Drawing.Point(181, 4);
			this.GameSetupComboBox.Name = "GameSetupComboBox";
			this.GameSetupComboBox.Size = new System.Drawing.Size(493, 21);
			this.GameSetupComboBox.TabIndex = 6;
			//
			//SetUpGameButton
			//
			this.SetUpGameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.SetUpGameButton.Location = new System.Drawing.Point(680, 3);
			this.SetUpGameButton.Name = "SetUpGameButton";
			this.SetUpGameButton.Size = new System.Drawing.Size(90, 23);
			this.SetUpGameButton.TabIndex = 7;
			this.SetUpGameButton.Text = "Set Up Games";
			this.SetUpGameButton.UseVisualStyleBackColor = true;
			//
			//ViewAsReplacementButton
			//
			this.ViewAsReplacementButton.Enabled = false;
			this.ViewAsReplacementButton.Location = new System.Drawing.Point(46, 32);
			this.ViewAsReplacementButton.Name = "ViewAsReplacementButton";
			this.ViewAsReplacementButton.Size = new System.Drawing.Size(125, 23);
			this.ViewAsReplacementButton.TabIndex = 9;
			this.ViewAsReplacementButton.Text = "View as Replacement";
			this.ViewAsReplacementButton.UseVisualStyleBackColor = true;
			//
			//UseInDecompileButton
			//
			this.UseInDecompileButton.Enabled = false;
			this.UseInDecompileButton.Location = new System.Drawing.Point(177, 32);
			this.UseInDecompileButton.Name = "UseInDecompileButton";
			this.UseInDecompileButton.Size = new System.Drawing.Size(120, 23);
			this.UseInDecompileButton.TabIndex = 10;
			this.UseInDecompileButton.Text = "Use in Decompile";
			this.UseInDecompileButton.UseVisualStyleBackColor = true;
			//
			//OpenViewerButton
			//
			this.OpenViewerButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.OpenViewerButton.Location = new System.Drawing.Point(488, 32);
			this.OpenViewerButton.Name = "OpenViewerButton";
			this.OpenViewerButton.Size = new System.Drawing.Size(90, 23);
			this.OpenViewerButton.TabIndex = 11;
			this.OpenViewerButton.Text = "Open Viewer";
			this.OpenViewerButton.UseVisualStyleBackColor = true;
			//
			//OpenMappingToolButton
			//
			this.OpenMappingToolButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.OpenMappingToolButton.Location = new System.Drawing.Point(584, 32);
			this.OpenMappingToolButton.Name = "OpenMappingToolButton";
			this.OpenMappingToolButton.Size = new System.Drawing.Size(90, 23);
			this.OpenMappingToolButton.TabIndex = 14;
			this.OpenMappingToolButton.Text = "Open Mapper";
			this.OpenMappingToolButton.UseVisualStyleBackColor = true;
			//
			//RunGameButton
			//
			this.RunGameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.RunGameButton.Location = new System.Drawing.Point(680, 32);
			this.RunGameButton.Name = "RunGameButton";
			this.RunGameButton.Size = new System.Drawing.Size(90, 23);
			this.RunGameButton.TabIndex = 13;
			this.RunGameButton.Text = "Run Game";
			this.RunGameButton.UseVisualStyleBackColor = true;
			//
			//MessageTextBox
			//
			this.MessageTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.MessageTextBox.CueBannerText = "";
			this.MessageTextBox.Location = new System.Drawing.Point(0, 61);
			this.MessageTextBox.Multiline = true;
			this.MessageTextBox.Name = "MessageTextBox";
			this.MessageTextBox.ReadOnly = true;
			this.MessageTextBox.Size = new System.Drawing.Size(770, 45);
			this.MessageTextBox.TabIndex = 12;
			//
			//ViewUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel2);
			this.Name = "ViewUserControl";
			this.Size = new System.Drawing.Size(776, 536);
			this.Panel2.ResumeLayout(false);
			this.Panel2.PerformLayout();
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			this.SplitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
			this.SplitContainer1.ResumeLayout(false);
			this.GroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			base.Load += new System.EventHandler(UpdateUserControl_Load);
			BrowseForMdlFileButton.Click += new System.EventHandler(BrowseForMdlFileButton_Click);
			GotoMdlFileButton.Click += new System.EventHandler(GotoMdlFileButton_Click);
			ViewButton.Click += new System.EventHandler(ViewButton_Click);
			ViewAsReplacementButton.Click += new System.EventHandler(ViewAsReplacementButton_Click);
			UseInDecompileButton.Click += new System.EventHandler(UseInDecompileButton_Click);
			OpenViewerButton.Click += new System.EventHandler(OpenViewerButton_Click);
			OpenMappingToolButton.Click += new System.EventHandler(OpenMappingToolButton_Click);
			RunGameButton.Click += new System.EventHandler(RunGameButton_Click);
		}
		internal System.Windows.Forms.Button ViewButton;
		internal TextBoxEx MdlPathFileNameTextBox;
		internal System.Windows.Forms.Button BrowseForMdlFileButton;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Panel Panel2;
		internal System.Windows.Forms.Label GameLabel;
		internal System.Windows.Forms.Button SetUpGameButton;
		internal System.Windows.Forms.ComboBox GameSetupComboBox;
		internal System.Windows.Forms.Button GotoMdlFileButton;
		internal System.Windows.Forms.Button ViewAsReplacementButton;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal Crowbar.RichTextBoxEx InfoRichTextBox;
		internal System.Windows.Forms.Button UseInDecompileButton;
		internal System.Windows.Forms.Button OpenViewerButton;
		internal Crowbar.TextBoxEx MessageTextBox;
		internal System.Windows.Forms.SplitContainer SplitContainer1;
		internal System.Windows.Forms.Button RunGameButton;
		internal System.Windows.Forms.Button OpenMappingToolButton;
		internal Label OverrideMdlVersionLabel;
		internal ComboBox OverrideMdlVersionComboBox;
	}

}