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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AboutCrowbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutCrowbarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.SetUpGamesTabPage = new System.Windows.Forms.TabPage();
            this.SetUpGamesUserControl1 = new Crowbar.SetUpGamesUserControl();
            this.DownloadTabPage = new System.Windows.Forms.TabPage();
            this.DownloadUserControl1 = new Crowbar.DownloadUserControl();
            this.UnpackTabPage = new System.Windows.Forms.TabPage();
            this.UnpackUserControl1 = new Crowbar.UnpackUserControl();
            this.PreviewTabPage = new System.Windows.Forms.TabPage();
            this.PreviewViewUserControl = new Crowbar.ViewUserControl();
            this.DecompileTabPage = new System.Windows.Forms.TabPage();
            this.DecompilerUserControl1 = new Crowbar.DecompileUserControl();
            this.CompileTabPage = new System.Windows.Forms.TabPage();
            this.CompilerUserControl1 = new Crowbar.CompileUserControl();
            this.ViewTabPage = new System.Windows.Forms.TabPage();
            this.ViewViewUserControl = new Crowbar.ViewUserControl();
            this.PackTabPage = new System.Windows.Forms.TabPage();
            this.PackUserControl1 = new Crowbar.PackUserControl();
            this.PublishTabPage = new System.Windows.Forms.TabPage();
            this.PublishUserControl1 = new Crowbar.PublishUserControl();
            this.OptionsTabPage = new System.Windows.Forms.TabPage();
            this.OptionsUserControl1 = new Crowbar.OptionsUserControl();
            this.HelpTabPage = new System.Windows.Forms.TabPage();
            this.HelpUserControl1 = new Crowbar.HelpUserControl();
            this.AboutTabPage = new System.Windows.Forms.TabPage();
            this.AboutUserControl1 = new Crowbar.AboutUserControl();
            this.UpdateTabPage = new System.Windows.Forms.TabPage();
            this.UpdateUserControl1 = new Crowbar.UpdateUserControl();
            this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutCrowbarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MainTabControl.SuspendLayout();
            this.SetUpGamesTabPage.SuspendLayout();
            this.DownloadTabPage.SuspendLayout();
            this.UnpackTabPage.SuspendLayout();
            this.PreviewTabPage.SuspendLayout();
            this.DecompileTabPage.SuspendLayout();
            this.CompileTabPage.SuspendLayout();
            this.ViewTabPage.SuspendLayout();
            this.PackTabPage.SuspendLayout();
            this.PublishTabPage.SuspendLayout();
            this.OptionsTabPage.SuspendLayout();
            this.HelpTabPage.SuspendLayout();
            this.AboutTabPage.SuspendLayout();
            this.UpdateTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // AboutCrowbarToolStripMenuItem
            // 
            this.AboutCrowbarToolStripMenuItem.Name = "AboutCrowbarToolStripMenuItem";
            this.AboutCrowbarToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.AboutCrowbarToolStripMenuItem.Text = "About Crowbar";
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutCrowbarToolStripMenuItem1});
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(40, 20);
            this.ToolStripMenuItem1.Text = "Help";
            // 
            // AboutCrowbarToolStripMenuItem1
            // 
            this.AboutCrowbarToolStripMenuItem1.Name = "AboutCrowbarToolStripMenuItem1";
            this.AboutCrowbarToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.AboutCrowbarToolStripMenuItem1.Text = "About Crowbar";
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.SetUpGamesTabPage);
            this.MainTabControl.Controls.Add(this.DownloadTabPage);
            this.MainTabControl.Controls.Add(this.UnpackTabPage);
            this.MainTabControl.Controls.Add(this.PreviewTabPage);
            this.MainTabControl.Controls.Add(this.DecompileTabPage);
            this.MainTabControl.Controls.Add(this.CompileTabPage);
            this.MainTabControl.Controls.Add(this.ViewTabPage);
            this.MainTabControl.Controls.Add(this.PackTabPage);
            this.MainTabControl.Controls.Add(this.PublishTabPage);
            this.MainTabControl.Controls.Add(this.OptionsTabPage);
            this.MainTabControl.Controls.Add(this.HelpTabPage);
            this.MainTabControl.Controls.Add(this.AboutTabPage);
            this.MainTabControl.Controls.Add(this.UpdateTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(792, 572);
            this.MainTabControl.TabIndex = 12;
            // 
            // SetUpGamesTabPage
            // 
            this.SetUpGamesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.SetUpGamesTabPage.Controls.Add(this.SetUpGamesUserControl1);
            this.SetUpGamesTabPage.Location = new System.Drawing.Point(4, 22);
            this.SetUpGamesTabPage.Name = "SetUpGamesTabPage";
            this.SetUpGamesTabPage.Size = new System.Drawing.Size(784, 546);
            this.SetUpGamesTabPage.TabIndex = 15;
            this.SetUpGamesTabPage.Text = "Set Up Games";
            // 
            // SetUpGamesUserControl1
            // 
            this.SetUpGamesUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetUpGamesUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.SetUpGamesUserControl1.Location = new System.Drawing.Point(0, 0);
            this.SetUpGamesUserControl1.Name = "SetUpGamesUserControl1";
            this.SetUpGamesUserControl1.Size = new System.Drawing.Size(784, 546);
            this.SetUpGamesUserControl1.TabIndex = 0;
            // 
            // DownloadTabPage
            // 
            this.DownloadTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.DownloadTabPage.Controls.Add(this.DownloadUserControl1);
            this.DownloadTabPage.Location = new System.Drawing.Point(4, 22);
            this.DownloadTabPage.Name = "DownloadTabPage";
            this.DownloadTabPage.Size = new System.Drawing.Size(784, 546);
            this.DownloadTabPage.TabIndex = 0;
            this.DownloadTabPage.Text = "Download";
            // 
            // DownloadUserControl1
            // 
            this.DownloadUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownloadUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.DownloadUserControl1.Location = new System.Drawing.Point(0, 0);
            this.DownloadUserControl1.Name = "DownloadUserControl1";
            this.DownloadUserControl1.Size = new System.Drawing.Size(784, 546);
            this.DownloadUserControl1.TabIndex = 0;
            // 
            // UnpackTabPage
            // 
            this.UnpackTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.UnpackTabPage.Controls.Add(this.UnpackUserControl1);
            this.UnpackTabPage.Location = new System.Drawing.Point(4, 22);
            this.UnpackTabPage.Name = "UnpackTabPage";
            this.UnpackTabPage.Size = new System.Drawing.Size(784, 546);
            this.UnpackTabPage.TabIndex = 13;
            this.UnpackTabPage.Text = "Unpack";
            // 
            // UnpackUserControl1
            // 
            this.UnpackUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnpackUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.UnpackUserControl1.Location = new System.Drawing.Point(0, 0);
            this.UnpackUserControl1.Name = "UnpackUserControl1";
            this.UnpackUserControl1.Size = new System.Drawing.Size(784, 546);
            this.UnpackUserControl1.TabIndex = 0;
            // 
            // PreviewTabPage
            // 
            this.PreviewTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.PreviewTabPage.Controls.Add(this.PreviewViewUserControl);
            this.PreviewTabPage.Location = new System.Drawing.Point(4, 22);
            this.PreviewTabPage.Name = "PreviewTabPage";
            this.PreviewTabPage.Size = new System.Drawing.Size(784, 546);
            this.PreviewTabPage.TabIndex = 12;
            this.PreviewTabPage.Text = "Preview";
            // 
            // PreviewViewUserControl
            // 
            this.PreviewViewUserControl.BackColor = System.Drawing.SystemColors.Control;
            this.PreviewViewUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewViewUserControl.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.PreviewViewUserControl.Location = new System.Drawing.Point(0, 0);
            this.PreviewViewUserControl.Name = "PreviewViewUserControl";
            this.PreviewViewUserControl.Size = new System.Drawing.Size(784, 546);
            this.PreviewViewUserControl.TabIndex = 1;
            this.PreviewViewUserControl.ViewerType = Crowbar.AppEnums.ViewerType.Preview;
            // 
            // DecompileTabPage
            // 
            this.DecompileTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.DecompileTabPage.Controls.Add(this.DecompilerUserControl1);
            this.DecompileTabPage.Location = new System.Drawing.Point(4, 22);
            this.DecompileTabPage.Name = "DecompileTabPage";
            this.DecompileTabPage.Size = new System.Drawing.Size(784, 546);
            this.DecompileTabPage.TabIndex = 0;
            this.DecompileTabPage.Text = "Decompile";
            // 
            // DecompilerUserControl1
            // 
            this.DecompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DecompilerUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.DecompilerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.DecompilerUserControl1.Name = "DecompilerUserControl1";
            this.DecompilerUserControl1.Size = new System.Drawing.Size(784, 546);
            this.DecompilerUserControl1.TabIndex = 0;
            // 
            // CompileTabPage
            // 
            this.CompileTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.CompileTabPage.Controls.Add(this.CompilerUserControl1);
            this.CompileTabPage.Location = new System.Drawing.Point(4, 22);
            this.CompileTabPage.Name = "CompileTabPage";
            this.CompileTabPage.Size = new System.Drawing.Size(784, 546);
            this.CompileTabPage.TabIndex = 1;
            this.CompileTabPage.Text = "Compile";
            // 
            // CompilerUserControl1
            // 
            this.CompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompilerUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.CompilerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.CompilerUserControl1.Name = "CompilerUserControl1";
            this.CompilerUserControl1.Size = new System.Drawing.Size(784, 546);
            this.CompilerUserControl1.TabIndex = 0;
            // 
            // ViewTabPage
            // 
            this.ViewTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.ViewTabPage.Controls.Add(this.ViewViewUserControl);
            this.ViewTabPage.Location = new System.Drawing.Point(4, 22);
            this.ViewTabPage.Name = "ViewTabPage";
            this.ViewTabPage.Size = new System.Drawing.Size(784, 546);
            this.ViewTabPage.TabIndex = 5;
            this.ViewTabPage.Text = "View";
            // 
            // ViewViewUserControl
            // 
            this.ViewViewUserControl.BackColor = System.Drawing.SystemColors.Control;
            this.ViewViewUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewViewUserControl.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ViewViewUserControl.Location = new System.Drawing.Point(0, 0);
            this.ViewViewUserControl.Name = "ViewViewUserControl";
            this.ViewViewUserControl.Size = new System.Drawing.Size(784, 546);
            this.ViewViewUserControl.TabIndex = 0;
            this.ViewViewUserControl.ViewerType = Crowbar.AppEnums.ViewerType.View;
            // 
            // PackTabPage
            // 
            this.PackTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.PackTabPage.Controls.Add(this.PackUserControl1);
            this.PackTabPage.Location = new System.Drawing.Point(4, 22);
            this.PackTabPage.Name = "PackTabPage";
            this.PackTabPage.Size = new System.Drawing.Size(784, 546);
            this.PackTabPage.TabIndex = 16;
            this.PackTabPage.Text = "Pack";
            // 
            // PackUserControl1
            // 
            this.PackUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PackUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.PackUserControl1.Location = new System.Drawing.Point(0, 0);
            this.PackUserControl1.Name = "PackUserControl1";
            this.PackUserControl1.Size = new System.Drawing.Size(784, 546);
            this.PackUserControl1.TabIndex = 0;
            // 
            // PublishTabPage
            // 
            this.PublishTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.PublishTabPage.Controls.Add(this.PublishUserControl1);
            this.PublishTabPage.Location = new System.Drawing.Point(4, 22);
            this.PublishTabPage.Name = "PublishTabPage";
            this.PublishTabPage.Size = new System.Drawing.Size(784, 546);
            this.PublishTabPage.TabIndex = 1;
            this.PublishTabPage.Text = "Publish";
            // 
            // PublishUserControl1
            // 
            this.PublishUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PublishUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.PublishUserControl1.Location = new System.Drawing.Point(0, 0);
            this.PublishUserControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PublishUserControl1.Name = "PublishUserControl1";
            this.PublishUserControl1.Size = new System.Drawing.Size(784, 546);
            this.PublishUserControl1.TabIndex = 0;
            // 
            // OptionsTabPage
            // 
            this.OptionsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.OptionsTabPage.Controls.Add(this.OptionsUserControl1);
            this.OptionsTabPage.Location = new System.Drawing.Point(4, 22);
            this.OptionsTabPage.Name = "OptionsTabPage";
            this.OptionsTabPage.Size = new System.Drawing.Size(784, 546);
            this.OptionsTabPage.TabIndex = 10;
            this.OptionsTabPage.Text = "Options";
            // 
            // OptionsUserControl1
            // 
            this.OptionsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionsUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.OptionsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.OptionsUserControl1.Name = "OptionsUserControl1";
            this.OptionsUserControl1.Size = new System.Drawing.Size(784, 546);
            this.OptionsUserControl1.TabIndex = 0;
            // 
            // HelpTabPage
            // 
            this.HelpTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.HelpTabPage.Controls.Add(this.HelpUserControl1);
            this.HelpTabPage.Location = new System.Drawing.Point(4, 22);
            this.HelpTabPage.Name = "HelpTabPage";
            this.HelpTabPage.Size = new System.Drawing.Size(784, 546);
            this.HelpTabPage.TabIndex = 14;
            this.HelpTabPage.Text = "Help";
            // 
            // HelpUserControl1
            // 
            this.HelpUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HelpUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.HelpUserControl1.Location = new System.Drawing.Point(0, 0);
            this.HelpUserControl1.Name = "HelpUserControl1";
            this.HelpUserControl1.Size = new System.Drawing.Size(784, 546);
            this.HelpUserControl1.TabIndex = 0;
            // 
            // AboutTabPage
            // 
            this.AboutTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.AboutTabPage.Controls.Add(this.AboutUserControl1);
            this.AboutTabPage.Location = new System.Drawing.Point(4, 22);
            this.AboutTabPage.Name = "AboutTabPage";
            this.AboutTabPage.Size = new System.Drawing.Size(784, 546);
            this.AboutTabPage.TabIndex = 11;
            this.AboutTabPage.Text = "About";
            // 
            // AboutUserControl1
            // 
            this.AboutUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AboutUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.AboutUserControl1.Location = new System.Drawing.Point(0, 0);
            this.AboutUserControl1.Name = "AboutUserControl1";
            this.AboutUserControl1.Size = new System.Drawing.Size(784, 546);
            this.AboutUserControl1.TabIndex = 1;
            // 
            // UpdateTabPage
            // 
            this.UpdateTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.UpdateTabPage.Controls.Add(this.UpdateUserControl1);
            this.UpdateTabPage.Location = new System.Drawing.Point(4, 22);
            this.UpdateTabPage.Name = "UpdateTabPage";
            this.UpdateTabPage.Size = new System.Drawing.Size(784, 546);
            this.UpdateTabPage.TabIndex = 19;
            this.UpdateTabPage.Text = "Update";
            // 
            // UpdateUserControl1
            // 
            this.UpdateUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpdateUserControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.UpdateUserControl1.Location = new System.Drawing.Point(0, 0);
            this.UpdateUserControl1.Name = "UpdateUserControl1";
            this.UpdateUserControl1.Size = new System.Drawing.Size(784, 546);
            this.UpdateUserControl1.TabIndex = 0;
            // 
            // ToolStripMenuItem2
            // 
            this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
            this.ToolStripMenuItem2.Size = new System.Drawing.Size(40, 20);
            this.ToolStripMenuItem2.Text = "Help";
            // 
            // AboutCrowbarToolStripMenuItem2
            // 
            this.AboutCrowbarToolStripMenuItem2.Name = "AboutCrowbarToolStripMenuItem2";
            this.AboutCrowbarToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.AboutCrowbarToolStripMenuItem2.Text = "About Crowbar";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 572);
            this.Controls.Add(this.MainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Crowbar";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainTabControl.ResumeLayout(false);
            this.SetUpGamesTabPage.ResumeLayout(false);
            this.DownloadTabPage.ResumeLayout(false);
            this.UnpackTabPage.ResumeLayout(false);
            this.PreviewTabPage.ResumeLayout(false);
            this.DecompileTabPage.ResumeLayout(false);
            this.CompileTabPage.ResumeLayout(false);
            this.ViewTabPage.ResumeLayout(false);
            this.PackTabPage.ResumeLayout(false);
            this.PublishTabPage.ResumeLayout(false);
            this.OptionsTabPage.ResumeLayout(false);
            this.HelpTabPage.ResumeLayout(false);
            this.AboutTabPage.ResumeLayout(false);
            this.UpdateTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

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