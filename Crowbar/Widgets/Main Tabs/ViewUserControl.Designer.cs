using System.IO;
using System.Text;

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
			components = new System.ComponentModel.Container();
			ViewButton = new System.Windows.Forms.Button();
			MdlPathFileNameTextBox = new Crowbar.TextBoxEx();
			BrowseForMdlFileButton = new System.Windows.Forms.Button();
			Label1 = new System.Windows.Forms.Label();
			Panel2 = new System.Windows.Forms.Panel();
			OverrideMdlVersionLabel = new System.Windows.Forms.Label();
			OverrideMdlVersionComboBox = new System.Windows.Forms.ComboBox();
			GotoMdlFileButton = new System.Windows.Forms.Button();
			SplitContainer1 = new System.Windows.Forms.SplitContainer();
			GroupBox1 = new System.Windows.Forms.GroupBox();
			InfoRichTextBox = new Crowbar.RichTextBoxEx();
			GameLabel = new System.Windows.Forms.Label();
			GameSetupComboBox = new System.Windows.Forms.ComboBox();
			SetUpGameButton = new System.Windows.Forms.Button();
			ViewAsReplacementButton = new System.Windows.Forms.Button();
			UseInDecompileButton = new System.Windows.Forms.Button();
			OpenViewerButton = new System.Windows.Forms.Button();
			OpenMappingToolButton = new System.Windows.Forms.Button();
			RunGameButton = new System.Windows.Forms.Button();
			MessageTextBox = new Crowbar.TextBoxEx();
			Panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)SplitContainer1).BeginInit();
			SplitContainer1.Panel1.SuspendLayout();
			SplitContainer1.Panel2.SuspendLayout();
			SplitContainer1.SuspendLayout();
			GroupBox1.SuspendLayout();
			SuspendLayout();
			//
			//ViewButton
			//
			ViewButton.Enabled = false;
			ViewButton.Location = new System.Drawing.Point(0, 32);
			ViewButton.Name = "ViewButton";
			ViewButton.Size = new System.Drawing.Size(40, 23);
			ViewButton.TabIndex = 8;
			ViewButton.Text = "View";
			ViewButton.UseVisualStyleBackColor = true;
			//
			//MdlPathFileNameTextBox
			//
			MdlPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			MdlPathFileNameTextBox.CueBannerText = "";
			MdlPathFileNameTextBox.Location = new System.Drawing.Point(58, 3);
			MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox";
			MdlPathFileNameTextBox.Size = new System.Drawing.Size(596, 22);
			MdlPathFileNameTextBox.TabIndex = 1;
			//
			//BrowseForMdlFileButton
			//
			BrowseForMdlFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseForMdlFileButton.Location = new System.Drawing.Point(660, 3);
			BrowseForMdlFileButton.Name = "BrowseForMdlFileButton";
			BrowseForMdlFileButton.Size = new System.Drawing.Size(64, 23);
			BrowseForMdlFileButton.TabIndex = 2;
			BrowseForMdlFileButton.Text = "Browse...";
			BrowseForMdlFileButton.UseVisualStyleBackColor = true;
			//
			//Label1
			//
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(3, 8);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(52, 13);
			Label1.TabIndex = 0;
			Label1.Text = "MDL file:";
			//
			//Panel2
			//
			Panel2.Controls.Add(OverrideMdlVersionLabel);
			Panel2.Controls.Add(Label1);
			Panel2.Controls.Add(MdlPathFileNameTextBox);
			Panel2.Controls.Add(OverrideMdlVersionComboBox);
			Panel2.Controls.Add(BrowseForMdlFileButton);
			Panel2.Controls.Add(GotoMdlFileButton);
			Panel2.Controls.Add(SplitContainer1);
			Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			Panel2.Location = new System.Drawing.Point(0, 0);
			Panel2.Margin = new System.Windows.Forms.Padding(2);
			Panel2.Name = "Panel2";
			Panel2.Size = new System.Drawing.Size(776, 536);
			Panel2.TabIndex = 8;
			//
			//OverrideMdlVersionLabel
			//
			OverrideMdlVersionLabel.AutoSize = true;
			OverrideMdlVersionLabel.Location = new System.Drawing.Point(3, 36);
			OverrideMdlVersionLabel.Name = "OverrideMdlVersionLabel";
			OverrideMdlVersionLabel.Size = new System.Drawing.Size(120, 13);
			OverrideMdlVersionLabel.TabIndex = 48;
			OverrideMdlVersionLabel.Text = "Override MDL version:";
			//
			//OverrideMdlVersionComboBox
			//
			OverrideMdlVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			OverrideMdlVersionComboBox.FormattingEnabled = true;
			OverrideMdlVersionComboBox.Location = new System.Drawing.Point(123, 32);
			OverrideMdlVersionComboBox.Name = "OverrideMdlVersionComboBox";
			OverrideMdlVersionComboBox.Size = new System.Drawing.Size(110, 21);
			OverrideMdlVersionComboBox.TabIndex = 47;
			//
			//GotoMdlFileButton
			//
			GotoMdlFileButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			GotoMdlFileButton.Location = new System.Drawing.Point(730, 3);
			GotoMdlFileButton.Name = "GotoMdlFileButton";
			GotoMdlFileButton.Size = new System.Drawing.Size(43, 23);
			GotoMdlFileButton.TabIndex = 3;
			GotoMdlFileButton.Text = "Goto";
			GotoMdlFileButton.UseVisualStyleBackColor = true;
			//
			//SplitContainer1
			//
			SplitContainer1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			SplitContainer1.Location = new System.Drawing.Point(3, 59);
			SplitContainer1.Name = "SplitContainer1";
			SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//SplitContainer1.Panel1
			//
			SplitContainer1.Panel1.Controls.Add(GroupBox1);
			SplitContainer1.Panel1MinSize = 45;
			//
			//SplitContainer1.Panel2
			//
			SplitContainer1.Panel2.Controls.Add(GameLabel);
			SplitContainer1.Panel2.Controls.Add(GameSetupComboBox);
			SplitContainer1.Panel2.Controls.Add(SetUpGameButton);
			SplitContainer1.Panel2.Controls.Add(ViewButton);
			SplitContainer1.Panel2.Controls.Add(ViewAsReplacementButton);
			SplitContainer1.Panel2.Controls.Add(UseInDecompileButton);
			SplitContainer1.Panel2.Controls.Add(OpenViewerButton);
			SplitContainer1.Panel2.Controls.Add(OpenMappingToolButton);
			SplitContainer1.Panel2.Controls.Add(RunGameButton);
			SplitContainer1.Panel2.Controls.Add(MessageTextBox);
			SplitContainer1.Panel2MinSize = 45;
			SplitContainer1.Size = new System.Drawing.Size(770, 474);
			SplitContainer1.SplitterDistance = 363;
			SplitContainer1.TabIndex = 13;
			//
			//GroupBox1
			//
			GroupBox1.Controls.Add(InfoRichTextBox);
			GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			GroupBox1.Location = new System.Drawing.Point(0, 0);
			GroupBox1.Name = "GroupBox1";
			GroupBox1.Size = new System.Drawing.Size(770, 363);
			GroupBox1.TabIndex = 4;
			GroupBox1.TabStop = false;
			GroupBox1.Text = "Info";
			//
			//InfoRichTextBox
			//
			InfoRichTextBox.CueBannerText = "";
			InfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			InfoRichTextBox.Location = new System.Drawing.Point(3, 18);
			InfoRichTextBox.Name = "InfoRichTextBox";
			InfoRichTextBox.ReadOnly = true;
			InfoRichTextBox.Size = new System.Drawing.Size(764, 342);
			InfoRichTextBox.TabIndex = 0;
			InfoRichTextBox.Text = "";
			InfoRichTextBox.WordWrap = false;
			//
			//GameLabel
			//
			GameLabel.AutoSize = true;
			GameLabel.Location = new System.Drawing.Point(0, 8);
			GameLabel.Name = "GameLabel";
			GameLabel.Size = new System.Drawing.Size(175, 13);
			GameLabel.TabIndex = 5;
			GameLabel.Text = "Game that has the model viewer:";
			//
			//GameSetupComboBox
			//
			GameSetupComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			GameSetupComboBox.FormattingEnabled = true;
			GameSetupComboBox.Location = new System.Drawing.Point(181, 4);
			GameSetupComboBox.Name = "GameSetupComboBox";
			GameSetupComboBox.Size = new System.Drawing.Size(493, 21);
			GameSetupComboBox.TabIndex = 6;
			//
			//SetUpGameButton
			//
			SetUpGameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			SetUpGameButton.Location = new System.Drawing.Point(680, 3);
			SetUpGameButton.Name = "SetUpGameButton";
			SetUpGameButton.Size = new System.Drawing.Size(90, 23);
			SetUpGameButton.TabIndex = 7;
			SetUpGameButton.Text = "Set Up Games";
			SetUpGameButton.UseVisualStyleBackColor = true;
			//
			//ViewAsReplacementButton
			//
			ViewAsReplacementButton.Enabled = false;
			ViewAsReplacementButton.Location = new System.Drawing.Point(46, 32);
			ViewAsReplacementButton.Name = "ViewAsReplacementButton";
			ViewAsReplacementButton.Size = new System.Drawing.Size(125, 23);
			ViewAsReplacementButton.TabIndex = 9;
			ViewAsReplacementButton.Text = "View as Replacement";
			ViewAsReplacementButton.UseVisualStyleBackColor = true;
			//
			//UseInDecompileButton
			//
			UseInDecompileButton.Enabled = false;
			UseInDecompileButton.Location = new System.Drawing.Point(177, 32);
			UseInDecompileButton.Name = "UseInDecompileButton";
			UseInDecompileButton.Size = new System.Drawing.Size(120, 23);
			UseInDecompileButton.TabIndex = 10;
			UseInDecompileButton.Text = "Use in Decompile";
			UseInDecompileButton.UseVisualStyleBackColor = true;
			//
			//OpenViewerButton
			//
			OpenViewerButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			OpenViewerButton.Location = new System.Drawing.Point(488, 32);
			OpenViewerButton.Name = "OpenViewerButton";
			OpenViewerButton.Size = new System.Drawing.Size(90, 23);
			OpenViewerButton.TabIndex = 11;
			OpenViewerButton.Text = "Open Viewer";
			OpenViewerButton.UseVisualStyleBackColor = true;
			//
			//OpenMappingToolButton
			//
			OpenMappingToolButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			OpenMappingToolButton.Location = new System.Drawing.Point(584, 32);
			OpenMappingToolButton.Name = "OpenMappingToolButton";
			OpenMappingToolButton.Size = new System.Drawing.Size(90, 23);
			OpenMappingToolButton.TabIndex = 14;
			OpenMappingToolButton.Text = "Open Mapper";
			OpenMappingToolButton.UseVisualStyleBackColor = true;
			//
			//RunGameButton
			//
			RunGameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			RunGameButton.Location = new System.Drawing.Point(680, 32);
			RunGameButton.Name = "RunGameButton";
			RunGameButton.Size = new System.Drawing.Size(90, 23);
			RunGameButton.TabIndex = 13;
			RunGameButton.Text = "Run Game";
			RunGameButton.UseVisualStyleBackColor = true;
			//
			//MessageTextBox
			//
			MessageTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			MessageTextBox.CueBannerText = "";
			MessageTextBox.Location = new System.Drawing.Point(0, 61);
			MessageTextBox.Multiline = true;
			MessageTextBox.Name = "MessageTextBox";
			MessageTextBox.ReadOnly = true;
			MessageTextBox.Size = new System.Drawing.Size(770, 45);
			MessageTextBox.TabIndex = 12;
			//
			//ViewUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(Panel2);
			Name = "ViewUserControl";
			Size = new System.Drawing.Size(776, 536);
			Panel2.ResumeLayout(false);
			Panel2.PerformLayout();
			SplitContainer1.Panel1.ResumeLayout(false);
			SplitContainer1.Panel2.ResumeLayout(false);
			SplitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)SplitContainer1).EndInit();
			SplitContainer1.ResumeLayout(false);
			GroupBox1.ResumeLayout(false);
			ResumeLayout(false);

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