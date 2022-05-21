using System.Collections.ObjectModel;
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
	public partial class MainForm : BaseForm
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			AboutCrowbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			AboutCrowbarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			MainTabControl = new System.Windows.Forms.TabControl();
			SetUpGamesTabPage = new System.Windows.Forms.TabPage();
			SetUpGamesUserControl1 = new Crowbar.SetUpGamesUserControl();
			DownloadTabPage = new System.Windows.Forms.TabPage();
			DownloadUserControl1 = new Crowbar.DownloadUserControl();
			UnpackTabPage = new System.Windows.Forms.TabPage();
			UnpackUserControl1 = new Crowbar.UnpackUserControl();
			PreviewTabPage = new System.Windows.Forms.TabPage();
			PreviewViewUserControl = new Crowbar.ViewUserControl();
			DecompileTabPage = new System.Windows.Forms.TabPage();
			DecompilerUserControl1 = new Crowbar.DecompileUserControl();
			CompileTabPage = new System.Windows.Forms.TabPage();
			CompilerUserControl1 = new Crowbar.CompileUserControl();
			ViewTabPage = new System.Windows.Forms.TabPage();
			ViewViewUserControl = new Crowbar.ViewUserControl();
			PackTabPage = new System.Windows.Forms.TabPage();
			PackUserControl1 = new Crowbar.PackUserControl();
			PublishTabPage = new System.Windows.Forms.TabPage();
			PublishUserControl1 = new Crowbar.PublishUserControl();
			OptionsTabPage = new System.Windows.Forms.TabPage();
			OptionsUserControl1 = new Crowbar.OptionsUserControl();
			HelpTabPage = new System.Windows.Forms.TabPage();
			HelpUserControl1 = new Crowbar.HelpUserControl();
			AboutTabPage = new System.Windows.Forms.TabPage();
			AboutUserControl1 = new Crowbar.AboutUserControl();
			UpdateTabPage = new System.Windows.Forms.TabPage();
			UpdateUserControl1 = new Crowbar.UpdateUserControl();
			ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			AboutCrowbarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			MainToolTip = new System.Windows.Forms.ToolTip(components);
			MainTabControl.SuspendLayout();
			SetUpGamesTabPage.SuspendLayout();
			DownloadTabPage.SuspendLayout();
			UnpackTabPage.SuspendLayout();
			PreviewTabPage.SuspendLayout();
			DecompileTabPage.SuspendLayout();
			CompileTabPage.SuspendLayout();
			ViewTabPage.SuspendLayout();
			PackTabPage.SuspendLayout();
			PublishTabPage.SuspendLayout();
			OptionsTabPage.SuspendLayout();
			HelpTabPage.SuspendLayout();
			AboutTabPage.SuspendLayout();
			UpdateTabPage.SuspendLayout();
			SuspendLayout();
			//
			//AboutCrowbarToolStripMenuItem
			//
			AboutCrowbarToolStripMenuItem.Name = "AboutCrowbarToolStripMenuItem";
			AboutCrowbarToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			AboutCrowbarToolStripMenuItem.Text = "About Crowbar";
			//
			//ToolStripMenuItem1
			//
			ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {AboutCrowbarToolStripMenuItem1});
			ToolStripMenuItem1.Name = "ToolStripMenuItem1";
			ToolStripMenuItem1.Size = new System.Drawing.Size(40, 20);
			ToolStripMenuItem1.Text = "Help";
			//
			//AboutCrowbarToolStripMenuItem1
			//
			AboutCrowbarToolStripMenuItem1.Name = "AboutCrowbarToolStripMenuItem1";
			AboutCrowbarToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
			AboutCrowbarToolStripMenuItem1.Text = "About Crowbar";
			//
			//MainTabControl
			//
			MainTabControl.Controls.Add(SetUpGamesTabPage);
			MainTabControl.Controls.Add(DownloadTabPage);
			MainTabControl.Controls.Add(UnpackTabPage);
			MainTabControl.Controls.Add(PreviewTabPage);
			MainTabControl.Controls.Add(DecompileTabPage);
			MainTabControl.Controls.Add(CompileTabPage);
			MainTabControl.Controls.Add(ViewTabPage);
			MainTabControl.Controls.Add(PackTabPage);
			MainTabControl.Controls.Add(PublishTabPage);
			MainTabControl.Controls.Add(OptionsTabPage);
			MainTabControl.Controls.Add(HelpTabPage);
			MainTabControl.Controls.Add(AboutTabPage);
			MainTabControl.Controls.Add(UpdateTabPage);
			MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			MainTabControl.Location = new System.Drawing.Point(0, 0);
			MainTabControl.Name = "MainTabControl";
			MainTabControl.SelectedIndex = 0;
			MainTabControl.Size = new System.Drawing.Size(792, 572);
			MainTabControl.TabIndex = 12;
			//
			//SetUpGamesTabPage
			//
			SetUpGamesTabPage.BackColor = System.Drawing.SystemColors.Control;
			SetUpGamesTabPage.Controls.Add(SetUpGamesUserControl1);
			SetUpGamesTabPage.Location = new System.Drawing.Point(4, 22);
			SetUpGamesTabPage.Name = "SetUpGamesTabPage";
			SetUpGamesTabPage.Size = new System.Drawing.Size(784, 546);
			SetUpGamesTabPage.TabIndex = 15;
			SetUpGamesTabPage.Text = "Set Up Games";
			//
			//SetUpGamesUserControl1
			//
			SetUpGamesUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			SetUpGamesUserControl1.Location = new System.Drawing.Point(0, 0);
			SetUpGamesUserControl1.Name = "SetUpGamesUserControl1";
			SetUpGamesUserControl1.Size = new System.Drawing.Size(784, 546);
			SetUpGamesUserControl1.TabIndex = 0;
			//
			//DownloadTabPage
			//
			DownloadTabPage.BackColor = System.Drawing.SystemColors.Control;
			DownloadTabPage.Controls.Add(DownloadUserControl1);
			DownloadTabPage.Location = new System.Drawing.Point(4, 22);
			DownloadTabPage.Name = "DownloadTabPage";
			DownloadTabPage.Size = new System.Drawing.Size(192, 74);
			DownloadTabPage.TabIndex = 0;
			DownloadTabPage.Text = "Download";
			//
			//DownloadUserControl1
			//
			DownloadUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			DownloadUserControl1.Location = new System.Drawing.Point(0, 0);
			DownloadUserControl1.Name = "DownloadUserControl1";
			DownloadUserControl1.Size = new System.Drawing.Size(192, 74);
			DownloadUserControl1.TabIndex = 0;
			//
			//UnpackTabPage
			//
			UnpackTabPage.BackColor = System.Drawing.SystemColors.Control;
			UnpackTabPage.Controls.Add(UnpackUserControl1);
			UnpackTabPage.Location = new System.Drawing.Point(4, 22);
			UnpackTabPage.Name = "UnpackTabPage";
			UnpackTabPage.Size = new System.Drawing.Size(192, 74);
			UnpackTabPage.TabIndex = 13;
			UnpackTabPage.Text = "Unpack";
			//
			//UnpackUserControl1
			//
			UnpackUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			UnpackUserControl1.Location = new System.Drawing.Point(0, 0);
			UnpackUserControl1.Name = "UnpackUserControl1";
			UnpackUserControl1.Size = new System.Drawing.Size(192, 74);
			UnpackUserControl1.TabIndex = 0;
			//
			//PreviewTabPage
			//
			PreviewTabPage.BackColor = System.Drawing.SystemColors.Control;
			PreviewTabPage.Controls.Add(PreviewViewUserControl);
			PreviewTabPage.Location = new System.Drawing.Point(4, 22);
			PreviewTabPage.Name = "PreviewTabPage";
			PreviewTabPage.Size = new System.Drawing.Size(192, 74);
			PreviewTabPage.TabIndex = 12;
			PreviewTabPage.Text = "Preview";
			//
			//PreviewViewUserControl
			//
			PreviewViewUserControl.BackColor = System.Drawing.SystemColors.Control;
			PreviewViewUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
			PreviewViewUserControl.Location = new System.Drawing.Point(0, 0);
			PreviewViewUserControl.Name = "PreviewViewUserControl";
			PreviewViewUserControl.Size = new System.Drawing.Size(192, 74);
			PreviewViewUserControl.TabIndex = 1;
			PreviewViewUserControl.ViewerType = Crowbar.AppEnums.ViewerType.Preview;
			//
			//DecompileTabPage
			//
			DecompileTabPage.BackColor = System.Drawing.SystemColors.Control;
			DecompileTabPage.Controls.Add(DecompilerUserControl1);
			DecompileTabPage.Location = new System.Drawing.Point(4, 22);
			DecompileTabPage.Name = "DecompileTabPage";
			DecompileTabPage.Size = new System.Drawing.Size(192, 74);
			DecompileTabPage.TabIndex = 0;
			DecompileTabPage.Text = "Decompile";
			//
			//DecompilerUserControl1
			//
			DecompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			DecompilerUserControl1.Location = new System.Drawing.Point(0, 0);
			DecompilerUserControl1.Name = "DecompilerUserControl1";
			DecompilerUserControl1.Size = new System.Drawing.Size(192, 74);
			DecompilerUserControl1.TabIndex = 0;
			//
			//CompileTabPage
			//
			CompileTabPage.BackColor = System.Drawing.SystemColors.Control;
			CompileTabPage.Controls.Add(CompilerUserControl1);
			CompileTabPage.Location = new System.Drawing.Point(4, 22);
			CompileTabPage.Name = "CompileTabPage";
			CompileTabPage.Size = new System.Drawing.Size(192, 74);
			CompileTabPage.TabIndex = 1;
			CompileTabPage.Text = "Compile";
			//
			//CompilerUserControl1
			//
			CompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			CompilerUserControl1.Location = new System.Drawing.Point(0, 0);
			CompilerUserControl1.Name = "CompilerUserControl1";
			CompilerUserControl1.Size = new System.Drawing.Size(192, 74);
			CompilerUserControl1.TabIndex = 0;
			//
			//ViewTabPage
			//
			ViewTabPage.BackColor = System.Drawing.SystemColors.Control;
			ViewTabPage.Controls.Add(ViewViewUserControl);
			ViewTabPage.Location = new System.Drawing.Point(4, 22);
			ViewTabPage.Name = "ViewTabPage";
			ViewTabPage.Size = new System.Drawing.Size(192, 74);
			ViewTabPage.TabIndex = 5;
			ViewTabPage.Text = "View";
			//
			//ViewViewUserControl
			//
			ViewViewUserControl.BackColor = System.Drawing.SystemColors.Control;
			ViewViewUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
			ViewViewUserControl.Location = new System.Drawing.Point(0, 0);
			ViewViewUserControl.Name = "ViewViewUserControl";
			ViewViewUserControl.Size = new System.Drawing.Size(192, 74);
			ViewViewUserControl.TabIndex = 0;
			ViewViewUserControl.ViewerType = Crowbar.AppEnums.ViewerType.View;
			//
			//PackTabPage
			//
			PackTabPage.BackColor = System.Drawing.SystemColors.Control;
			PackTabPage.Controls.Add(PackUserControl1);
			PackTabPage.Location = new System.Drawing.Point(4, 22);
			PackTabPage.Name = "PackTabPage";
			PackTabPage.Size = new System.Drawing.Size(192, 74);
			PackTabPage.TabIndex = 16;
			PackTabPage.Text = "Pack";
			//
			//PackUserControl1
			//
			PackUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			PackUserControl1.Location = new System.Drawing.Point(0, 0);
			PackUserControl1.Name = "PackUserControl1";
			PackUserControl1.Size = new System.Drawing.Size(192, 74);
			PackUserControl1.TabIndex = 0;
			//
			//PublishTabPage
			//
			PublishTabPage.BackColor = System.Drawing.SystemColors.Control;
			PublishTabPage.Controls.Add(PublishUserControl1);
			PublishTabPage.Location = new System.Drawing.Point(4, 22);
			PublishTabPage.Name = "PublishTabPage";
			PublishTabPage.Size = new System.Drawing.Size(192, 74);
			PublishTabPage.TabIndex = 1;
			PublishTabPage.Text = "Publish";
			//
			//PublishUserControl1
			//
			PublishUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			PublishUserControl1.Location = new System.Drawing.Point(0, 0);
			PublishUserControl1.Name = "PublishUserControl1";
			PublishUserControl1.Size = new System.Drawing.Size(192, 74);
			PublishUserControl1.TabIndex = 0;
			//
			//OptionsTabPage
			//
			OptionsTabPage.BackColor = System.Drawing.SystemColors.Control;
			OptionsTabPage.Controls.Add(OptionsUserControl1);
			OptionsTabPage.Location = new System.Drawing.Point(4, 22);
			OptionsTabPage.Name = "OptionsTabPage";
			OptionsTabPage.Size = new System.Drawing.Size(192, 74);
			OptionsTabPage.TabIndex = 10;
			OptionsTabPage.Text = "Options";
			//
			//OptionsUserControl1
			//
			OptionsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			OptionsUserControl1.Location = new System.Drawing.Point(0, 0);
			OptionsUserControl1.Name = "OptionsUserControl1";
			OptionsUserControl1.Size = new System.Drawing.Size(192, 74);
			OptionsUserControl1.TabIndex = 0;
			//
			//HelpTabPage
			//
			HelpTabPage.BackColor = System.Drawing.SystemColors.Control;
			HelpTabPage.Controls.Add(HelpUserControl1);
			HelpTabPage.Location = new System.Drawing.Point(4, 22);
			HelpTabPage.Name = "HelpTabPage";
			HelpTabPage.Size = new System.Drawing.Size(192, 74);
			HelpTabPage.TabIndex = 14;
			HelpTabPage.Text = "Help";
			//
			//HelpUserControl1
			//
			HelpUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			HelpUserControl1.Location = new System.Drawing.Point(0, 0);
			HelpUserControl1.Name = "HelpUserControl1";
			HelpUserControl1.Size = new System.Drawing.Size(192, 74);
			HelpUserControl1.TabIndex = 0;
			//
			//AboutTabPage
			//
			AboutTabPage.BackColor = System.Drawing.SystemColors.Control;
			AboutTabPage.Controls.Add(AboutUserControl1);
			AboutTabPage.Location = new System.Drawing.Point(4, 22);
			AboutTabPage.Name = "AboutTabPage";
			AboutTabPage.Size = new System.Drawing.Size(192, 74);
			AboutTabPage.TabIndex = 11;
			AboutTabPage.Text = "About";
			//
			//AboutUserControl1
			//
			AboutUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			AboutUserControl1.Location = new System.Drawing.Point(0, 0);
			AboutUserControl1.Name = "AboutUserControl1";
			AboutUserControl1.Size = new System.Drawing.Size(192, 74);
			AboutUserControl1.TabIndex = 1;
			//
			//UpdateTabPage
			//
			UpdateTabPage.BackColor = System.Drawing.SystemColors.Control;
			UpdateTabPage.Controls.Add(UpdateUserControl1);
			UpdateTabPage.Location = new System.Drawing.Point(4, 22);
			UpdateTabPage.Name = "UpdateTabPage";
			UpdateTabPage.Size = new System.Drawing.Size(192, 74);
			UpdateTabPage.TabIndex = 19;
			UpdateTabPage.Text = "Update";
			//
			//UpdateUserControl1
			//
			UpdateUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			UpdateUserControl1.Location = new System.Drawing.Point(0, 0);
			UpdateUserControl1.Name = "UpdateUserControl1";
			UpdateUserControl1.Size = new System.Drawing.Size(192, 74);
			UpdateUserControl1.TabIndex = 0;
			//
			//ToolStripMenuItem2
			//
			ToolStripMenuItem2.Name = "ToolStripMenuItem2";
			ToolStripMenuItem2.Size = new System.Drawing.Size(40, 20);
			ToolStripMenuItem2.Text = "Help";
			//
			//AboutCrowbarToolStripMenuItem2
			//
			AboutCrowbarToolStripMenuItem2.Name = "AboutCrowbarToolStripMenuItem2";
			AboutCrowbarToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
			AboutCrowbarToolStripMenuItem2.Text = "About Crowbar";
			//
			//MainForm
			//
			AllowDrop = true;
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(792, 572);
			Controls.Add(MainTabControl);
			Icon = (System.Drawing.Icon)resources.GetObject("$Icon");
			MinimumSize = new System.Drawing.Size(800, 600);
			Name = "MainForm";
			Text = "Crowbar";
			MainTabControl.ResumeLayout(false);
			SetUpGamesTabPage.ResumeLayout(false);
			DownloadTabPage.ResumeLayout(false);
			UnpackTabPage.ResumeLayout(false);
			PreviewTabPage.ResumeLayout(false);
			DecompileTabPage.ResumeLayout(false);
			CompileTabPage.ResumeLayout(false);
			ViewTabPage.ResumeLayout(false);
			PackTabPage.ResumeLayout(false);
			PublishTabPage.ResumeLayout(false);
			OptionsTabPage.ResumeLayout(false);
			HelpTabPage.ResumeLayout(false);
			AboutTabPage.ResumeLayout(false);
			UpdateTabPage.ResumeLayout(false);
			ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			Load += new System.EventHandler(MainForm_Load);
			FormClosed += MainForm_FormClosed;//new System.EventHandler(MainForm_FormClosed);
			base.DragEnter += MainForm_DragEnter;//new System.EventHandler(MainForm_DragEnter);
			base.DragDrop += MainForm_DragDrop;//new System.EventHandler(MainForm_DragDrop);
		}
		internal System.Windows.Forms.TabControl MainTabControl;
		internal System.Windows.Forms.ToolTip MainToolTip;
		internal System.Windows.Forms.TabPage SetUpGamesTabPage;
		internal Crowbar.SetUpGamesUserControl SetUpGamesUserControl1;
		internal TabPage DownloadTabPage;
		internal DownloadUserControl DownloadUserControl1;
		internal System.Windows.Forms.TabPage UnpackTabPage;
		internal Crowbar.UnpackUserControl UnpackUserControl1;
		internal System.Windows.Forms.TabPage PreviewTabPage;
		internal Crowbar.ViewUserControl PreviewViewUserControl;
		internal System.Windows.Forms.TabPage DecompileTabPage;
		internal Crowbar.DecompileUserControl DecompilerUserControl1;
		internal System.Windows.Forms.TabPage CompileTabPage;
		internal Crowbar.CompileUserControl CompilerUserControl1;
		internal System.Windows.Forms.TabPage ViewTabPage;
		internal Crowbar.ViewUserControl ViewViewUserControl;
		internal TabPage PackTabPage;
		internal PackUserControl PackUserControl1;
		internal TabPage PublishTabPage;
		internal PublishUserControl PublishUserControl1;
		internal System.Windows.Forms.TabPage OptionsTabPage;
		internal Crowbar.OptionsUserControl OptionsUserControl1;
		internal System.Windows.Forms.TabPage HelpTabPage;
		internal Crowbar.HelpUserControl HelpUserControl1;
		internal System.Windows.Forms.TabPage AboutTabPage;
		internal Crowbar.AboutUserControl AboutUserControl1;
		internal System.Windows.Forms.MenuStrip MenuStrip1;
		internal System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem AboutCrowbarToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem AboutCrowbarToolStripMenuItem1;
		internal System.Windows.Forms.ToolStripMenuItem AboutCrowbarToolStripMenuItem2;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
		internal TabPage UpdateTabPage;
		internal UpdateUserControl UpdateUserControl1;
	}

}