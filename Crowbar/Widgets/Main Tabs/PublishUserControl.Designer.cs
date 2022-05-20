using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
	public partial class PublishUserControl : BaseUserControl
	{
		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublishUserControl));
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.RefreshGameItemsButton = new System.Windows.Forms.Button();
			this.ItemOwnerLabel = new System.Windows.Forms.Label();
			this.ItemPostedTextBox = new Crowbar.DateTimeTextBoxEx();
			this.ItemUpdatedTextBox = new Crowbar.DateTimeTextBoxEx();
			this.QuotaProgressBar = new Crowbar.ProgressBarEx();
			this.TopMiddleSplitContainer = new Crowbar.SplitContainerEx();
			this.ItemsPanel = new System.Windows.Forms.Panel();
			this.ItemsDataGridView = new System.Windows.Forms.DataGridView();
			this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
			this.AddItemToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.SearchItemsToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.SearchItemsToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.SearchItemsToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ItemCountsToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.FindItemToolStripTextBox = new Crowbar.ToolStripSpringTextBox();
			this.GamePanel = new System.Windows.Forms.Panel();
			this.GameLabel = new System.Windows.Forms.Label();
			this.AppIdComboBox = new System.Windows.Forms.ComboBox();
			this.PublishRequiresSteamLabel = new System.Windows.Forms.Label();
			this.OpenSteamSubscriberAgreementButton = new System.Windows.Forms.Button();
			this.MiddleBottomSplitContainer = new System.Windows.Forms.SplitContainer();
			this.ItemGroupBox = new Crowbar.GroupBoxEx();
			this.ItemTagsSplitContainer = new System.Windows.Forms.SplitContainer();
			this.DescriptionChangeNoteSplitContainer = new System.Windows.Forms.SplitContainer();
			this.ItemDescriptionTextBox = new Crowbar.RichTextBoxEx();
			this.ItemDescriptionTopPanel = new System.Windows.Forms.Panel();
			this.ToggleWordWrapForDescriptionCheckBox = new System.Windows.Forms.CheckBox();
			this.ItemDescriptionLabel = new System.Windows.Forms.Label();
			this.ItemChangeNoteTextBox = new Crowbar.RichTextBoxEx();
			this.ItemChangeNoteTopPanel = new System.Windows.Forms.Panel();
			this.ToggleWordWrapForChangeNotePanel = new System.Windows.Forms.Panel();
			this.ToggleWordWrapForChangeNoteCheckBox = new System.Windows.Forms.CheckBox();
			this.ItemChangeNoteLabel = new System.Windows.Forms.Label();
			this.ItemTopPanel = new System.Windows.Forms.Panel();
			this.ItemIDLabel = new System.Windows.Forms.Label();
			this.ItemIDTextBox = new Crowbar.TextBoxEx();
			this.ItemOwnerTextBox = new Crowbar.TextBoxEx();
			this.ItemTitleLabel = new System.Windows.Forms.Label();
			this.ItemTitleTextBox = new Crowbar.TextBoxEx();
			this.ItemBottomPanel = new System.Windows.Forms.Panel();
			this.ItemContentFolderOrFileLabel = new System.Windows.Forms.Label();
			this.ItemContentPathFileNameTextBox = new Crowbar.TextBoxEx();
			this.BrowseItemContentPathFileNameButton = new System.Windows.Forms.Button();
			this.ItemPreviewImageLabel = new System.Windows.Forms.Label();
			this.ItemPreviewImagePathFileNameTextBox = new Crowbar.TextBoxEx();
			this.BrowseItemPreviewImagePathFileNameButton = new System.Windows.Forms.Button();
			this.ItemPreviewImagePictureBox = new System.Windows.Forms.PictureBox();
			this.ItemVisibilityComboBox = new System.Windows.Forms.ComboBox();
			this.ItemVisibilityLabel = new System.Windows.Forms.Label();
			this.SaveAsTemplateOrDraftItemButton = new System.Windows.Forms.Button();
			this.RefreshOrRevertItemButton = new System.Windows.Forms.Button();
			this.SaveTemplateButton = new System.Windows.Forms.Button();
			this.OpenWorkshopPageButton = new System.Windows.Forms.Button();
			this.DeleteItemButton = new System.Windows.Forms.Button();
			this.ItemLeftMinScrollPanel = new System.Windows.Forms.Panel();
			this.ItemTagsGroupBox = new Crowbar.GroupBoxEx();
			this.LogTextBox = new Crowbar.RichTextBoxEx();
			this.PublishItemButton = new System.Windows.Forms.Button();
			this.QueueListView = new System.Windows.Forms.ListView();
			((System.ComponentModel.ISupportInitialize)this.TopMiddleSplitContainer).BeginInit();
			this.TopMiddleSplitContainer.Panel1.SuspendLayout();
			this.TopMiddleSplitContainer.Panel2.SuspendLayout();
			this.TopMiddleSplitContainer.SuspendLayout();
			this.ItemsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.ItemsDataGridView).BeginInit();
			this.ToolStrip1.SuspendLayout();
			this.GamePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.MiddleBottomSplitContainer).BeginInit();
			this.MiddleBottomSplitContainer.Panel1.SuspendLayout();
			this.MiddleBottomSplitContainer.Panel2.SuspendLayout();
			this.MiddleBottomSplitContainer.SuspendLayout();
			this.ItemGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.ItemTagsSplitContainer).BeginInit();
			this.ItemTagsSplitContainer.Panel1.SuspendLayout();
			this.ItemTagsSplitContainer.Panel2.SuspendLayout();
			this.ItemTagsSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.DescriptionChangeNoteSplitContainer).BeginInit();
			this.DescriptionChangeNoteSplitContainer.Panel1.SuspendLayout();
			this.DescriptionChangeNoteSplitContainer.Panel2.SuspendLayout();
			this.DescriptionChangeNoteSplitContainer.SuspendLayout();
			this.ItemDescriptionTopPanel.SuspendLayout();
			this.ItemChangeNoteTopPanel.SuspendLayout();
			this.ToggleWordWrapForChangeNotePanel.SuspendLayout();
			this.ItemTopPanel.SuspendLayout();
			this.ItemBottomPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.ItemPreviewImagePictureBox).BeginInit();
			this.SuspendLayout();
			//
			//RefreshGameItemsButton
			//
			this.RefreshGameItemsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.RefreshGameItemsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.RefreshGameItemsButton.FlatAppearance.BorderSize = 0;
			this.RefreshGameItemsButton.Image = global::Crowbar.Properties.Resources.Refresh;
			this.RefreshGameItemsButton.Location = new System.Drawing.Point(394, 3);
			this.RefreshGameItemsButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.RefreshGameItemsButton.Name = "RefreshGameItemsButton";
			this.RefreshGameItemsButton.Padding = new System.Windows.Forms.Padding(0, 0, 1, 2);
			this.RefreshGameItemsButton.Size = new System.Drawing.Size(23, 22);
			this.RefreshGameItemsButton.TabIndex = 36;
			this.ToolTip1.SetToolTip(this.RefreshGameItemsButton, "Refresh Game Items");
			this.RefreshGameItemsButton.UseVisualStyleBackColor = true;
			//
			//ItemOwnerLabel
			//
			this.ItemOwnerLabel.Location = new System.Drawing.Point(150, 4);
			this.ItemOwnerLabel.Name = "ItemOwnerLabel";
			this.ItemOwnerLabel.Size = new System.Drawing.Size(45, 13);
			this.ItemOwnerLabel.TabIndex = 35;
			this.ItemOwnerLabel.Text = "Owner:";
			this.ToolTip1.SetToolTip(this.ItemOwnerLabel, "Double-click to swap between Steam Name and Steam ID.");
			//
			//ItemPostedTextBox
			//
			this.ItemPostedTextBox.CueBannerText = "";
			this.ItemPostedTextBox.Location = new System.Drawing.Point(334, 0);
			this.ItemPostedTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemPostedTextBox.Name = "ItemPostedTextBox";
			this.ItemPostedTextBox.ReadOnly = true;
			this.ItemPostedTextBox.Size = new System.Drawing.Size(120, 22);
			this.ItemPostedTextBox.TabIndex = 2;
			this.ToolTip1.SetToolTip(this.ItemPostedTextBox, "Posted");
			//
			//ItemUpdatedTextBox
			//
			this.ItemUpdatedTextBox.CueBannerText = "";
			this.ItemUpdatedTextBox.Location = new System.Drawing.Point(460, 0);
			this.ItemUpdatedTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemUpdatedTextBox.Name = "ItemUpdatedTextBox";
			this.ItemUpdatedTextBox.ReadOnly = true;
			this.ItemUpdatedTextBox.Size = new System.Drawing.Size(120, 22);
			this.ItemUpdatedTextBox.TabIndex = 3;
			this.ToolTip1.SetToolTip(this.ItemUpdatedTextBox, "Updated");
			//
			//QuotaProgressBar
			//
			this.QuotaProgressBar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.QuotaProgressBar.Location = new System.Drawing.Point(3, 31);
			this.QuotaProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.QuotaProgressBar.Name = "QuotaProgressBar";
			this.QuotaProgressBar.Size = new System.Drawing.Size(125, 22);
			this.QuotaProgressBar.TabIndex = 37;
			this.ToolTip1.SetToolTip(this.QuotaProgressBar, "Quota");
			//
			//TopMiddleSplitContainer
			//
			this.TopMiddleSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TopMiddleSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.TopMiddleSplitContainer.Margin = new System.Windows.Forms.Padding(2);
			this.TopMiddleSplitContainer.Name = "TopMiddleSplitContainer";
			this.TopMiddleSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//TopMiddleSplitContainer.Panel1
			//
			this.TopMiddleSplitContainer.Panel1.Controls.Add(this.ItemsPanel);
			this.TopMiddleSplitContainer.Panel1.Controls.Add(this.GamePanel);
			this.TopMiddleSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			//
			//TopMiddleSplitContainer.Panel2
			//
			this.TopMiddleSplitContainer.Panel2.AutoScroll = true;
			this.TopMiddleSplitContainer.Panel2.Controls.Add(this.MiddleBottomSplitContainer);
			this.TopMiddleSplitContainer.Size = new System.Drawing.Size(770, 534);
			this.TopMiddleSplitContainer.SplitterDistance = 139;
			this.TopMiddleSplitContainer.TabIndex = 28;
			//
			//ItemsPanel
			//
			this.ItemsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ItemsPanel.Controls.Add(this.ItemsDataGridView);
			this.ItemsPanel.Controls.Add(this.ToolStrip1);
			this.ItemsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemsPanel.Location = new System.Drawing.Point(3, 26);
			this.ItemsPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemsPanel.Name = "ItemsPanel";
			this.ItemsPanel.Size = new System.Drawing.Size(764, 110);
			this.ItemsPanel.TabIndex = 31;
			//
			//ItemsDataGridView
			//
			this.ItemsDataGridView.AllowUserToAddRows = false;
			this.ItemsDataGridView.AllowUserToDeleteRows = false;
			this.ItemsDataGridView.AllowUserToOrderColumns = true;
			this.ItemsDataGridView.AllowUserToResizeRows = false;
			this.ItemsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemsDataGridView.Location = new System.Drawing.Point(0, 0);
			this.ItemsDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemsDataGridView.MultiSelect = false;
			this.ItemsDataGridView.Name = "ItemsDataGridView";
			this.ItemsDataGridView.ReadOnly = true;
			this.ItemsDataGridView.RowHeadersVisible = false;
			this.ItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ItemsDataGridView.ShowCellErrors = false;
			this.ItemsDataGridView.ShowRowErrors = false;
			this.ItemsDataGridView.Size = new System.Drawing.Size(760, 81);
			this.ItemsDataGridView.TabIndex = 3;
			//
			//ToolStrip1
			//
			this.ToolStrip1.CanOverflow = false;
			this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.AddItemToolStripButton, this.ToolStripSeparator1, this.SearchItemsToolStripComboBox, this.SearchItemsToolStripTextBox, this.SearchItemsToolStripButton, this.ToolStripSeparator2, this.ItemCountsToolStripLabel, this.FindItemToolStripTextBox});
			this.ToolStrip1.Location = new System.Drawing.Point(0, 81);
			this.ToolStrip1.Name = "ToolStrip1";
			this.ToolStrip1.Size = new System.Drawing.Size(760, 25);
			this.ToolStrip1.Stretch = true;
			this.ToolStrip1.TabIndex = 30;
			this.ToolStrip1.Text = "ToolStrip1";
			//
			//AddItemToolStripButton
			//
			this.AddItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.AddItemToolStripButton.Image = (System.Drawing.Image)resources.GetObject("AddItemToolStripButton.Image");
			this.AddItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddItemToolStripButton.Name = "AddItemToolStripButton";
			this.AddItemToolStripButton.Size = new System.Drawing.Size(60, 22);
			this.AddItemToolStripButton.Text = "Add Item";
			//
			//ToolStripSeparator1
			//
			this.ToolStripSeparator1.Name = "ToolStripSeparator1";
			this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			//
			//SearchItemsToolStripComboBox
			//
			this.SearchItemsToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SearchItemsToolStripComboBox.Items.AddRange(new object[] {"ID:", "Title:", "Description:", "Owner:", "[All fields]:"});
			this.SearchItemsToolStripComboBox.Name = "SearchItemsToolStripComboBox";
			this.SearchItemsToolStripComboBox.Size = new System.Drawing.Size(80, 25);
			this.SearchItemsToolStripComboBox.ToolTipText = "Field to search";
			//
			//SearchItemsToolStripTextBox
			//
			this.SearchItemsToolStripTextBox.Name = "SearchItemsToolStripTextBox";
			this.SearchItemsToolStripTextBox.Size = new System.Drawing.Size(100, 25);
			this.SearchItemsToolStripTextBox.ToolTipText = "Text to search for";
			//
			//SearchItemsToolStripButton
			//
			this.SearchItemsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SearchItemsToolStripButton.Image = global::Crowbar.Properties.Resources.Find;
			this.SearchItemsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SearchItemsToolStripButton.Name = "SearchItemsToolStripButton";
			this.SearchItemsToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.SearchItemsToolStripButton.Text = "Search";
			this.SearchItemsToolStripButton.ToolTipText = "Search";
			//
			//ToolStripSeparator2
			//
			this.ToolStripSeparator2.Name = "ToolStripSeparator2";
			this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			//
			//ItemCountsToolStripLabel
			//
			this.ItemCountsToolStripLabel.Name = "ItemCountsToolStripLabel";
			this.ItemCountsToolStripLabel.Size = new System.Drawing.Size(168, 22);
			this.ItemCountsToolStripLabel.Text = "0 drafts + 0 published = 0 total";
			//
			//FindItemToolStripTextBox
			//
			this.FindItemToolStripTextBox.Name = "FindItemToolStripTextBox";
			this.FindItemToolStripTextBox.Size = new System.Drawing.Size(279, 25);
			this.FindItemToolStripTextBox.ToolTipText = "Title to find";
			this.FindItemToolStripTextBox.Visible = false;
			//
			//GamePanel
			//
			this.GamePanel.Controls.Add(this.GameLabel);
			this.GamePanel.Controls.Add(this.AppIdComboBox);
			this.GamePanel.Controls.Add(this.RefreshGameItemsButton);
			this.GamePanel.Controls.Add(this.PublishRequiresSteamLabel);
			this.GamePanel.Controls.Add(this.OpenSteamSubscriberAgreementButton);
			this.GamePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.GamePanel.Location = new System.Drawing.Point(3, 0);
			this.GamePanel.Name = "GamePanel";
			this.GamePanel.Size = new System.Drawing.Size(764, 26);
			this.GamePanel.TabIndex = 37;
			//
			//GameLabel
			//
			this.GameLabel.Location = new System.Drawing.Point(0, 7);
			this.GameLabel.Name = "GameLabel";
			this.GameLabel.Size = new System.Drawing.Size(39, 13);
			this.GameLabel.TabIndex = 22;
			this.GameLabel.Text = "Game:";
			//
			//AppIdComboBox
			//
			this.AppIdComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.AppIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.AppIdComboBox.FormattingEnabled = true;
			this.AppIdComboBox.Location = new System.Drawing.Point(45, 3);
			this.AppIdComboBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.AppIdComboBox.Name = "AppIdComboBox";
			this.AppIdComboBox.Size = new System.Drawing.Size(349, 21);
			this.AppIdComboBox.TabIndex = 0;
			//
			//PublishRequiresSteamLabel
			//
			this.PublishRequiresSteamLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.PublishRequiresSteamLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PublishRequiresSteamLabel.Location = new System.Drawing.Point(423, 3);
			this.PublishRequiresSteamLabel.Name = "PublishRequiresSteamLabel";
			this.PublishRequiresSteamLabel.Size = new System.Drawing.Size(136, 21);
			this.PublishRequiresSteamLabel.TabIndex = 1;
			this.PublishRequiresSteamLabel.Text = "Publish requires Steam";
			this.PublishRequiresSteamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//OpenSteamSubscriberAgreementButton
			//
			this.OpenSteamSubscriberAgreementButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.OpenSteamSubscriberAgreementButton.Location = new System.Drawing.Point(565, 3);
			this.OpenSteamSubscriberAgreementButton.Name = "OpenSteamSubscriberAgreementButton";
			this.OpenSteamSubscriberAgreementButton.Size = new System.Drawing.Size(199, 22);
			this.OpenSteamSubscriberAgreementButton.TabIndex = 2;
			this.OpenSteamSubscriberAgreementButton.Text = "Open Steam Subscriber Agreement";
			this.OpenSteamSubscriberAgreementButton.UseVisualStyleBackColor = true;
			//
			//MiddleBottomSplitContainer
			//
			this.MiddleBottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MiddleBottomSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.MiddleBottomSplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MiddleBottomSplitContainer.Name = "MiddleBottomSplitContainer";
			this.MiddleBottomSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//MiddleBottomSplitContainer.Panel1
			//
			this.MiddleBottomSplitContainer.Panel1.Controls.Add(this.ItemGroupBox);
			this.MiddleBottomSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			//
			//MiddleBottomSplitContainer.Panel2
			//
			this.MiddleBottomSplitContainer.Panel2.Controls.Add(this.LogTextBox);
			this.MiddleBottomSplitContainer.Panel2.Controls.Add(this.QuotaProgressBar);
			this.MiddleBottomSplitContainer.Panel2.Controls.Add(this.PublishItemButton);
			this.MiddleBottomSplitContainer.Panel2.Controls.Add(this.QueueListView);
			this.MiddleBottomSplitContainer.Panel2MinSize = 45;
			this.MiddleBottomSplitContainer.Size = new System.Drawing.Size(770, 391);
			this.MiddleBottomSplitContainer.SplitterDistance = 324;
			this.MiddleBottomSplitContainer.TabIndex = 26;
			//
			//ItemGroupBox
			//
			this.ItemGroupBox.Controls.Add(this.ItemTagsSplitContainer);
			this.ItemGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemGroupBox.IsReadOnly = false;
			this.ItemGroupBox.Location = new System.Drawing.Point(3, 4);
			this.ItemGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemGroupBox.Name = "ItemGroupBox";
			this.ItemGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemGroupBox.SelectedValue = null;
			this.ItemGroupBox.Size = new System.Drawing.Size(764, 316);
			this.ItemGroupBox.TabIndex = 31;
			this.ItemGroupBox.TabStop = false;
			this.ItemGroupBox.Text = "Item";
			//
			//ItemTagsSplitContainer
			//
			this.ItemTagsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemTagsSplitContainer.Location = new System.Drawing.Point(3, 19);
			this.ItemTagsSplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemTagsSplitContainer.Name = "ItemTagsSplitContainer";
			//
			//ItemTagsSplitContainer.Panel1
			//
			this.ItemTagsSplitContainer.Panel1.AutoScroll = true;
			this.ItemTagsSplitContainer.Panel1.Controls.Add(this.DescriptionChangeNoteSplitContainer);
			this.ItemTagsSplitContainer.Panel1.Controls.Add(this.ItemTopPanel);
			this.ItemTagsSplitContainer.Panel1.Controls.Add(this.ItemBottomPanel);
			this.ItemTagsSplitContainer.Panel1.Controls.Add(this.ItemLeftMinScrollPanel);
			//
			//ItemTagsSplitContainer.Panel2
			//
			this.ItemTagsSplitContainer.Panel2.Controls.Add(this.ItemTagsGroupBox);
			this.ItemTagsSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 4);
			this.ItemTagsSplitContainer.Size = new System.Drawing.Size(758, 293);
			this.ItemTagsSplitContainer.SplitterDistance = 580;
			this.ItemTagsSplitContainer.SplitterWidth = 3;
			this.ItemTagsSplitContainer.TabIndex = 25;
			//
			//DescriptionChangeNoteSplitContainer
			//
			this.DescriptionChangeNoteSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DescriptionChangeNoteSplitContainer.Location = new System.Drawing.Point(0, 68);
			this.DescriptionChangeNoteSplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DescriptionChangeNoteSplitContainer.Name = "DescriptionChangeNoteSplitContainer";
			//
			//DescriptionChangeNoteSplitContainer.Panel1
			//
			this.DescriptionChangeNoteSplitContainer.Panel1.Controls.Add(this.ItemDescriptionTextBox);
			this.DescriptionChangeNoteSplitContainer.Panel1.Controls.Add(this.ItemDescriptionTopPanel);
			this.DescriptionChangeNoteSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			//
			//DescriptionChangeNoteSplitContainer.Panel2
			//
			this.DescriptionChangeNoteSplitContainer.Panel2.Controls.Add(this.ItemChangeNoteTextBox);
			this.DescriptionChangeNoteSplitContainer.Panel2.Controls.Add(this.ItemChangeNoteTopPanel);
			this.DescriptionChangeNoteSplitContainer.Size = new System.Drawing.Size(580, 111);
			this.DescriptionChangeNoteSplitContainer.SplitterDistance = 295;
			this.DescriptionChangeNoteSplitContainer.TabIndex = 5;
			//
			//ItemDescriptionTextBox
			//
			this.ItemDescriptionTextBox.AcceptsTab = true;
			this.ItemDescriptionTextBox.CueBannerText = "required";
			this.ItemDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemDescriptionTextBox.Location = new System.Drawing.Point(3, 19);
			this.ItemDescriptionTextBox.Name = "ItemDescriptionTextBox";
			this.ItemDescriptionTextBox.Size = new System.Drawing.Size(292, 92);
			this.ItemDescriptionTextBox.TabIndex = 5;
			this.ItemDescriptionTextBox.Text = "";
			this.ItemDescriptionTextBox.WordWrap = false;
			//
			//ItemDescriptionTopPanel
			//
			this.ItemDescriptionTopPanel.Controls.Add(this.ToggleWordWrapForDescriptionCheckBox);
			this.ItemDescriptionTopPanel.Controls.Add(this.ItemDescriptionLabel);
			this.ItemDescriptionTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ItemDescriptionTopPanel.Location = new System.Drawing.Point(3, 0);
			this.ItemDescriptionTopPanel.Name = "ItemDescriptionTopPanel";
			this.ItemDescriptionTopPanel.Size = new System.Drawing.Size(292, 19);
			this.ItemDescriptionTopPanel.TabIndex = 17;
			//
			//ToggleWordWrapForDescriptionCheckBox
			//
			this.ToggleWordWrapForDescriptionCheckBox.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.ToggleWordWrapForDescriptionCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ToggleWordWrapForDescriptionCheckBox.BackgroundImage = global::Crowbar.Properties.Resources.WordWrapOff;
			this.ToggleWordWrapForDescriptionCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ToggleWordWrapForDescriptionCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ToggleWordWrapForDescriptionCheckBox.Location = new System.Drawing.Point(278, 4);
			this.ToggleWordWrapForDescriptionCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ToggleWordWrapForDescriptionCheckBox.Name = "ToggleWordWrapForDescriptionCheckBox";
			this.ToggleWordWrapForDescriptionCheckBox.Size = new System.Drawing.Size(13, 13);
			this.ToggleWordWrapForDescriptionCheckBox.TabIndex = 16;
			this.ToggleWordWrapForDescriptionCheckBox.UseVisualStyleBackColor = true;
			//
			//ItemDescriptionLabel
			//
			this.ItemDescriptionLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemDescriptionLabel.Location = new System.Drawing.Point(0, 4);
			this.ItemDescriptionLabel.Name = "ItemDescriptionLabel";
			this.ItemDescriptionLabel.Size = new System.Drawing.Size(275, 13);
			this.ItemDescriptionLabel.TabIndex = 9;
			this.ItemDescriptionLabel.Text = "Description (### / ### characters max):";
			//
			//ItemChangeNoteTextBox
			//
			this.ItemChangeNoteTextBox.AcceptsTab = true;
			this.ItemChangeNoteTextBox.CueBannerText = "";
			this.ItemChangeNoteTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemChangeNoteTextBox.Location = new System.Drawing.Point(0, 19);
			this.ItemChangeNoteTextBox.Name = "ItemChangeNoteTextBox";
			this.ItemChangeNoteTextBox.Size = new System.Drawing.Size(281, 92);
			this.ItemChangeNoteTextBox.TabIndex = 6;
			this.ItemChangeNoteTextBox.Text = "";
			this.ItemChangeNoteTextBox.WordWrap = false;
			//
			//ItemChangeNoteTopPanel
			//
			this.ItemChangeNoteTopPanel.Controls.Add(this.ToggleWordWrapForChangeNotePanel);
			this.ItemChangeNoteTopPanel.Controls.Add(this.ItemChangeNoteLabel);
			this.ItemChangeNoteTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ItemChangeNoteTopPanel.Location = new System.Drawing.Point(0, 0);
			this.ItemChangeNoteTopPanel.Name = "ItemChangeNoteTopPanel";
			this.ItemChangeNoteTopPanel.Size = new System.Drawing.Size(281, 19);
			this.ItemChangeNoteTopPanel.TabIndex = 18;
			//
			//ToggleWordWrapForChangeNotePanel
			//
			this.ToggleWordWrapForChangeNotePanel.Controls.Add(this.ToggleWordWrapForChangeNoteCheckBox);
			this.ToggleWordWrapForChangeNotePanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ToggleWordWrapForChangeNotePanel.Location = new System.Drawing.Point(267, 0);
			this.ToggleWordWrapForChangeNotePanel.Name = "ToggleWordWrapForChangeNotePanel";
			this.ToggleWordWrapForChangeNotePanel.Size = new System.Drawing.Size(14, 19);
			this.ToggleWordWrapForChangeNotePanel.TabIndex = 18;
			//
			//ToggleWordWrapForChangeNoteCheckBox
			//
			this.ToggleWordWrapForChangeNoteCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ToggleWordWrapForChangeNoteCheckBox.BackgroundImage = global::Crowbar.Properties.Resources.WordWrapOff;
			this.ToggleWordWrapForChangeNoteCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ToggleWordWrapForChangeNoteCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ToggleWordWrapForChangeNoteCheckBox.Location = new System.Drawing.Point(0, 4);
			this.ToggleWordWrapForChangeNoteCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ToggleWordWrapForChangeNoteCheckBox.Name = "ToggleWordWrapForChangeNoteCheckBox";
			this.ToggleWordWrapForChangeNoteCheckBox.Size = new System.Drawing.Size(13, 13);
			this.ToggleWordWrapForChangeNoteCheckBox.TabIndex = 17;
			this.ToggleWordWrapForChangeNoteCheckBox.UseVisualStyleBackColor = true;
			//
			//ItemChangeNoteLabel
			//
			this.ItemChangeNoteLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemChangeNoteLabel.Location = new System.Drawing.Point(0, 4);
			this.ItemChangeNoteLabel.Name = "ItemChangeNoteLabel";
			this.ItemChangeNoteLabel.Size = new System.Drawing.Size(264, 13);
			this.ItemChangeNoteLabel.TabIndex = 11;
			this.ItemChangeNoteLabel.Text = "Content Changes (### / ### characters max):";
			//
			//ItemTopPanel
			//
			this.ItemTopPanel.Controls.Add(this.ItemIDLabel);
			this.ItemTopPanel.Controls.Add(this.ItemIDTextBox);
			this.ItemTopPanel.Controls.Add(this.ItemOwnerLabel);
			this.ItemTopPanel.Controls.Add(this.ItemOwnerTextBox);
			this.ItemTopPanel.Controls.Add(this.ItemPostedTextBox);
			this.ItemTopPanel.Controls.Add(this.ItemUpdatedTextBox);
			this.ItemTopPanel.Controls.Add(this.ItemTitleLabel);
			this.ItemTopPanel.Controls.Add(this.ItemTitleTextBox);
			this.ItemTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ItemTopPanel.Location = new System.Drawing.Point(0, 0);
			this.ItemTopPanel.Name = "ItemTopPanel";
			this.ItemTopPanel.Size = new System.Drawing.Size(580, 68);
			this.ItemTopPanel.TabIndex = 36;
			//
			//ItemIDLabel
			//
			this.ItemIDLabel.Location = new System.Drawing.Point(3, 4);
			this.ItemIDLabel.Name = "ItemIDLabel";
			this.ItemIDLabel.Size = new System.Drawing.Size(25, 13);
			this.ItemIDLabel.TabIndex = 4;
			this.ItemIDLabel.Text = "ID:";
			//
			//ItemIDTextBox
			//
			this.ItemIDTextBox.CueBannerText = "";
			this.ItemIDTextBox.Location = new System.Drawing.Point(34, 0);
			this.ItemIDTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemIDTextBox.Name = "ItemIDTextBox";
			this.ItemIDTextBox.ReadOnly = true;
			this.ItemIDTextBox.Size = new System.Drawing.Size(110, 22);
			this.ItemIDTextBox.TabIndex = 0;
			//
			//ItemOwnerTextBox
			//
			this.ItemOwnerTextBox.CueBannerText = "";
			this.ItemOwnerTextBox.Location = new System.Drawing.Point(201, 0);
			this.ItemOwnerTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemOwnerTextBox.Name = "ItemOwnerTextBox";
			this.ItemOwnerTextBox.ReadOnly = true;
			this.ItemOwnerTextBox.Size = new System.Drawing.Size(120, 22);
			this.ItemOwnerTextBox.TabIndex = 1;
			//
			//ItemTitleLabel
			//
			this.ItemTitleLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemTitleLabel.Location = new System.Drawing.Point(3, 28);
			this.ItemTitleLabel.Name = "ItemTitleLabel";
			this.ItemTitleLabel.Size = new System.Drawing.Size(577, 12);
			this.ItemTitleLabel.TabIndex = 8;
			this.ItemTitleLabel.Text = "Title (### / ### characters max):";
			//
			//ItemTitleTextBox
			//
			this.ItemTitleTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemTitleTextBox.CueBannerText = "required";
			this.ItemTitleTextBox.Location = new System.Drawing.Point(3, 44);
			this.ItemTitleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemTitleTextBox.Name = "ItemTitleTextBox";
			this.ItemTitleTextBox.Size = new System.Drawing.Size(577, 22);
			this.ItemTitleTextBox.TabIndex = 4;
			this.ItemTitleTextBox.WordWrap = false;
			//
			//ItemBottomPanel
			//
			this.ItemBottomPanel.Controls.Add(this.ItemContentFolderOrFileLabel);
			this.ItemBottomPanel.Controls.Add(this.ItemContentPathFileNameTextBox);
			this.ItemBottomPanel.Controls.Add(this.BrowseItemContentPathFileNameButton);
			this.ItemBottomPanel.Controls.Add(this.ItemPreviewImageLabel);
			this.ItemBottomPanel.Controls.Add(this.ItemPreviewImagePathFileNameTextBox);
			this.ItemBottomPanel.Controls.Add(this.BrowseItemPreviewImagePathFileNameButton);
			this.ItemBottomPanel.Controls.Add(this.ItemPreviewImagePictureBox);
			this.ItemBottomPanel.Controls.Add(this.ItemVisibilityComboBox);
			this.ItemBottomPanel.Controls.Add(this.ItemVisibilityLabel);
			this.ItemBottomPanel.Controls.Add(this.SaveAsTemplateOrDraftItemButton);
			this.ItemBottomPanel.Controls.Add(this.RefreshOrRevertItemButton);
			this.ItemBottomPanel.Controls.Add(this.SaveTemplateButton);
			this.ItemBottomPanel.Controls.Add(this.OpenWorkshopPageButton);
			this.ItemBottomPanel.Controls.Add(this.DeleteItemButton);
			this.ItemBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ItemBottomPanel.Location = new System.Drawing.Point(0, 179);
			this.ItemBottomPanel.Name = "ItemBottomPanel";
			this.ItemBottomPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.ItemBottomPanel.Size = new System.Drawing.Size(580, 114);
			this.ItemBottomPanel.TabIndex = 37;
			//
			//ItemContentFolderOrFileLabel
			//
			this.ItemContentFolderOrFileLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemContentFolderOrFileLabel.Location = new System.Drawing.Point(3, 3);
			this.ItemContentFolderOrFileLabel.Name = "ItemContentFolderOrFileLabel";
			this.ItemContentFolderOrFileLabel.Size = new System.Drawing.Size(415, 13);
			this.ItemContentFolderOrFileLabel.TabIndex = 18;
			this.ItemContentFolderOrFileLabel.Text = "Content Folder or File (### / ### MB max):";
			//
			//ItemContentPathFileNameTextBox
			//
			this.ItemContentPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemContentPathFileNameTextBox.CueBannerText = "required";
			this.ItemContentPathFileNameTextBox.Location = new System.Drawing.Point(3, 19);
			this.ItemContentPathFileNameTextBox.Name = "ItemContentPathFileNameTextBox";
			this.ItemContentPathFileNameTextBox.Size = new System.Drawing.Size(415, 22);
			this.ItemContentPathFileNameTextBox.TabIndex = 7;
			this.ItemContentPathFileNameTextBox.WordWrap = false;
			//
			//BrowseItemContentPathFileNameButton
			//
			this.BrowseItemContentPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseItemContentPathFileNameButton.Location = new System.Drawing.Point(424, 19);
			this.BrowseItemContentPathFileNameButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BrowseItemContentPathFileNameButton.Name = "BrowseItemContentPathFileNameButton";
			this.BrowseItemContentPathFileNameButton.Size = new System.Drawing.Size(75, 22);
			this.BrowseItemContentPathFileNameButton.TabIndex = 8;
			this.BrowseItemContentPathFileNameButton.Text = "Browse...";
			this.BrowseItemContentPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//ItemPreviewImageLabel
			//
			this.ItemPreviewImageLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemPreviewImageLabel.Location = new System.Drawing.Point(3, 45);
			this.ItemPreviewImageLabel.Name = "ItemPreviewImageLabel";
			this.ItemPreviewImageLabel.Size = new System.Drawing.Size(415, 13);
			this.ItemPreviewImageLabel.TabIndex = 13;
			this.ItemPreviewImageLabel.Text = "Preview Image (### / ### MB max |  Required resolution: 512x512):";
			//
			//ItemPreviewImagePathFileNameTextBox
			//
			this.ItemPreviewImagePathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ItemPreviewImagePathFileNameTextBox.CueBannerText = "required";
			this.ItemPreviewImagePathFileNameTextBox.Location = new System.Drawing.Point(3, 60);
			this.ItemPreviewImagePathFileNameTextBox.Name = "ItemPreviewImagePathFileNameTextBox";
			this.ItemPreviewImagePathFileNameTextBox.Size = new System.Drawing.Size(415, 22);
			this.ItemPreviewImagePathFileNameTextBox.TabIndex = 9;
			this.ItemPreviewImagePathFileNameTextBox.WordWrap = false;
			//
			//BrowseItemPreviewImagePathFileNameButton
			//
			this.BrowseItemPreviewImagePathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.BrowseItemPreviewImagePathFileNameButton.Location = new System.Drawing.Point(424, 60);
			this.BrowseItemPreviewImagePathFileNameButton.Name = "BrowseItemPreviewImagePathFileNameButton";
			this.BrowseItemPreviewImagePathFileNameButton.Size = new System.Drawing.Size(75, 22);
			this.BrowseItemPreviewImagePathFileNameButton.TabIndex = 10;
			this.BrowseItemPreviewImagePathFileNameButton.Text = "Browse...";
			this.BrowseItemPreviewImagePathFileNameButton.UseVisualStyleBackColor = true;
			//
			//ItemPreviewImagePictureBox
			//
			this.ItemPreviewImagePictureBox.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.ItemPreviewImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ItemPreviewImagePictureBox.Location = new System.Drawing.Point(505, 7);
			this.ItemPreviewImagePictureBox.Name = "ItemPreviewImagePictureBox";
			this.ItemPreviewImagePictureBox.Size = new System.Drawing.Size(75, 75);
			this.ItemPreviewImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.ItemPreviewImagePictureBox.TabIndex = 27;
			this.ItemPreviewImagePictureBox.TabStop = false;
			//
			//ItemVisibilityComboBox
			//
			this.ItemVisibilityComboBox.FormattingEnabled = true;
			this.ItemVisibilityComboBox.Location = new System.Drawing.Point(63, 88);
			this.ItemVisibilityComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemVisibilityComboBox.Name = "ItemVisibilityComboBox";
			this.ItemVisibilityComboBox.Size = new System.Drawing.Size(100, 21);
			this.ItemVisibilityComboBox.TabIndex = 11;
			//
			//ItemVisibilityLabel
			//
			this.ItemVisibilityLabel.Location = new System.Drawing.Point(3, 92);
			this.ItemVisibilityLabel.Name = "ItemVisibilityLabel";
			this.ItemVisibilityLabel.Size = new System.Drawing.Size(54, 13);
			this.ItemVisibilityLabel.TabIndex = 10;
			this.ItemVisibilityLabel.Text = "Visibility:";
			//
			//SaveAsTemplateOrDraftItemButton
			//
			this.SaveAsTemplateOrDraftItemButton.Location = new System.Drawing.Point(169, 88);
			this.SaveAsTemplateOrDraftItemButton.Name = "SaveAsTemplateOrDraftItemButton";
			this.SaveAsTemplateOrDraftItemButton.Size = new System.Drawing.Size(111, 22);
			this.SaveAsTemplateOrDraftItemButton.TabIndex = 12;
			this.SaveAsTemplateOrDraftItemButton.Text = "Save as Template";
			this.SaveAsTemplateOrDraftItemButton.UseVisualStyleBackColor = true;
			//
			//RefreshOrRevertItemButton
			//
			this.RefreshOrRevertItemButton.Location = new System.Drawing.Point(286, 88);
			this.RefreshOrRevertItemButton.Name = "RefreshOrRevertItemButton";
			this.RefreshOrRevertItemButton.Size = new System.Drawing.Size(75, 22);
			this.RefreshOrRevertItemButton.TabIndex = 13;
			this.RefreshOrRevertItemButton.Text = "Refresh";
			this.RefreshOrRevertItemButton.UseVisualStyleBackColor = true;
			//
			//SaveTemplateButton
			//
			this.SaveTemplateButton.Location = new System.Drawing.Point(367, 88);
			this.SaveTemplateButton.Name = "SaveTemplateButton";
			this.SaveTemplateButton.Size = new System.Drawing.Size(75, 22);
			this.SaveTemplateButton.TabIndex = 14;
			this.SaveTemplateButton.Text = "Save";
			this.SaveTemplateButton.UseVisualStyleBackColor = true;
			//
			//OpenWorkshopPageButton
			//
			this.OpenWorkshopPageButton.Enabled = false;
			this.OpenWorkshopPageButton.Location = new System.Drawing.Point(367, 88);
			this.OpenWorkshopPageButton.Name = "OpenWorkshopPageButton";
			this.OpenWorkshopPageButton.Size = new System.Drawing.Size(132, 22);
			this.OpenWorkshopPageButton.TabIndex = 15;
			this.OpenWorkshopPageButton.Text = "Open Workshop Page";
			this.OpenWorkshopPageButton.UseVisualStyleBackColor = true;
			//
			//DeleteItemButton
			//
			this.DeleteItemButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.DeleteItemButton.Location = new System.Drawing.Point(505, 88);
			this.DeleteItemButton.Name = "DeleteItemButton";
			this.DeleteItemButton.Size = new System.Drawing.Size(75, 22);
			this.DeleteItemButton.TabIndex = 16;
			this.DeleteItemButton.Text = "Delete...";
			this.DeleteItemButton.UseVisualStyleBackColor = true;
			//
			//ItemLeftMinScrollPanel
			//
			this.ItemLeftMinScrollPanel.Location = new System.Drawing.Point(0, 0);
			this.ItemLeftMinScrollPanel.Name = "ItemLeftMinScrollPanel";
			this.ItemLeftMinScrollPanel.Size = new System.Drawing.Size(580, 1);
			this.ItemLeftMinScrollPanel.TabIndex = 38;
			//
			//ItemTagsGroupBox
			//
			this.ItemTagsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemTagsGroupBox.IsReadOnly = false;
			this.ItemTagsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.ItemTagsGroupBox.Margin = new System.Windows.Forms.Padding(0);
			this.ItemTagsGroupBox.Name = "ItemTagsGroupBox";
			this.ItemTagsGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ItemTagsGroupBox.SelectedValue = null;
			this.ItemTagsGroupBox.Size = new System.Drawing.Size(172, 289);
			this.ItemTagsGroupBox.TabIndex = 17;
			this.ItemTagsGroupBox.TabStop = false;
			this.ItemTagsGroupBox.Text = "Tags";
			//
			//LogTextBox
			//
			this.LogTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.LogTextBox.CueBannerText = "";
			this.LogTextBox.HideSelection = false;
			this.LogTextBox.Location = new System.Drawing.Point(134, 0);
			this.LogTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.LogTextBox.Name = "LogTextBox";
			this.LogTextBox.ReadOnly = true;
			this.LogTextBox.Size = new System.Drawing.Size(633, 59);
			this.LogTextBox.TabIndex = 19;
			this.LogTextBox.Text = "";
			this.LogTextBox.WordWrap = false;
			//
			//PublishItemButton
			//
			this.PublishItemButton.Location = new System.Drawing.Point(3, 0);
			this.PublishItemButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.PublishItemButton.Name = "PublishItemButton";
			this.PublishItemButton.Size = new System.Drawing.Size(125, 22);
			this.PublishItemButton.TabIndex = 18;
			this.PublishItemButton.Text = "Publish";
			this.PublishItemButton.UseVisualStyleBackColor = true;
			//
			//QueueListView
			//
			this.QueueListView.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right);
			this.QueueListView.HideSelection = false;
			this.QueueListView.Location = new System.Drawing.Point(568, 0);
			this.QueueListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.QueueListView.Name = "QueueListView";
			this.QueueListView.Size = new System.Drawing.Size(199, 63);
			this.QueueListView.TabIndex = 20;
			this.QueueListView.UseCompatibleStateImageBehavior = false;
			this.QueueListView.Visible = false;
			//
			//PublishUserControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TopMiddleSplitContainer);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PublishUserControl";
			this.Size = new System.Drawing.Size(770, 534);
			this.TopMiddleSplitContainer.Panel1.ResumeLayout(false);
			this.TopMiddleSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.TopMiddleSplitContainer).EndInit();
			this.TopMiddleSplitContainer.ResumeLayout(false);
			this.ItemsPanel.ResumeLayout(false);
			this.ItemsPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.ItemsDataGridView).EndInit();
			this.ToolStrip1.ResumeLayout(false);
			this.ToolStrip1.PerformLayout();
			this.GamePanel.ResumeLayout(false);
			this.MiddleBottomSplitContainer.Panel1.ResumeLayout(false);
			this.MiddleBottomSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.MiddleBottomSplitContainer).EndInit();
			this.MiddleBottomSplitContainer.ResumeLayout(false);
			this.ItemGroupBox.ResumeLayout(false);
			this.ItemTagsSplitContainer.Panel1.ResumeLayout(false);
			this.ItemTagsSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.ItemTagsSplitContainer).EndInit();
			this.ItemTagsSplitContainer.ResumeLayout(false);
			this.DescriptionChangeNoteSplitContainer.Panel1.ResumeLayout(false);
			this.DescriptionChangeNoteSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.DescriptionChangeNoteSplitContainer).EndInit();
			this.DescriptionChangeNoteSplitContainer.ResumeLayout(false);
			this.ItemDescriptionTopPanel.ResumeLayout(false);
			this.ItemChangeNoteTopPanel.ResumeLayout(false);
			this.ToggleWordWrapForChangeNotePanel.ResumeLayout(false);
			this.ItemTopPanel.ResumeLayout(false);
			this.ItemTopPanel.PerformLayout();
			this.ItemBottomPanel.ResumeLayout(false);
			this.ItemBottomPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.ItemPreviewImagePictureBox).EndInit();
			this.ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			base.Load += new System.EventHandler(PublishUserControl_Load);
			RefreshGameItemsButton.Click += new System.EventHandler(RefreshGameItemsButton_Click);
			OpenSteamSubscriberAgreementButton.Click += new System.EventHandler(OpenSteamSubscriberAgreementButton_Click);
			ItemsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(ItemsDataGridView_CellFormatting);
			ItemsDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(ItemsDataGridView_DataError);
			ItemsDataGridView.SelectionChanged += new System.EventHandler(ItemsDataGridView_SelectionChanged);
			ItemsDataGridView.Sorted += new System.EventHandler(ItemsDataGridView_Sorted);
			SearchItemsToolStripTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(SearchItemsToolStripTextBox_KeyPress);
			SearchItemsToolStripButton.Click += new System.EventHandler(SearchItemsToolStripButton_Click);
			AddItemToolStripButton.Click += new System.EventHandler(AddItemButton_Click);
			ItemOwnerLabel.DoubleClick += new System.EventHandler(OwnerLabel_DoubleClick);
			ToggleWordWrapForDescriptionCheckBox.CheckedChanged += new System.EventHandler(ToggleWordWrapForDescriptionCheckBox_CheckedChanged);
			ToggleWordWrapForChangeNoteCheckBox.CheckedChanged += new System.EventHandler(ToggleWordWrapForChangeNoteCheckBox_CheckedChanged);
			BrowseItemContentPathFileNameButton.Click += new System.EventHandler(BrowseContentPathFileNameButton_Click);
			BrowseItemPreviewImagePathFileNameButton.Click += new System.EventHandler(BrowsePreviewImageButton_Click);
			SaveAsTemplateOrDraftItemButton.Click += new System.EventHandler(SaveAsTemplateOrDraftItemButton_Click);
			RefreshOrRevertItemButton.Click += new System.EventHandler(RefreshOrRevertButton_Click);
			OpenWorkshopPageButton.Click += new System.EventHandler(OpenWorkshopPageButton_Click);
			SaveTemplateButton.Click += new System.EventHandler(SaveTemplateButton_Click);
			DeleteItemButton.Click += new System.EventHandler(DeleteItemButton_Click);
			PublishItemButton.Click += new System.EventHandler(PublishItemButton_Click);
			ItemPreviewImagePictureBox.Resize += new System.EventHandler(ItemPreviewImagePictureBox_Resize);
		}

		internal SplitContainerEx TopMiddleSplitContainer;
		internal DataGridView ItemsDataGridView;
		internal Label PublishRequiresSteamLabel;
		internal Button OpenSteamSubscriberAgreementButton;
		internal ComboBox AppIdComboBox;
		internal Label GameLabel;
		internal SplitContainer MiddleBottomSplitContainer;
		internal GroupBoxEx ItemGroupBox;
		internal SplitContainer ItemTagsSplitContainer;
		internal Button OpenWorkshopPageButton;
		internal Label ItemTitleLabel;
		internal Button DeleteItemButton;
		internal ComboBox ItemVisibilityComboBox;
		internal Button BrowseItemPreviewImagePathFileNameButton;
		internal TextBoxEx ItemTitleTextBox;
		internal Label ItemPreviewImageLabel;
		internal TextBoxEx ItemIDTextBox;
		internal TextBoxEx ItemPreviewImagePathFileNameTextBox;
		internal Label ItemVisibilityLabel;
		internal DateTimeTextBoxEx ItemPostedTextBox;
		internal Button BrowseItemContentPathFileNameButton;
		internal DateTimeTextBoxEx ItemUpdatedTextBox;
		internal TextBoxEx ItemContentPathFileNameTextBox;
		internal Label ItemContentFolderOrFileLabel;
		internal SplitContainer DescriptionChangeNoteSplitContainer;
		internal Label ItemDescriptionLabel;
		internal RichTextBoxEx ItemDescriptionTextBox;
		internal Label ItemChangeNoteLabel;
		internal RichTextBoxEx ItemChangeNoteTextBox;
		internal PictureBox ItemPreviewImagePictureBox;
		internal Label ItemIDLabel;
		internal GroupBoxEx ItemTagsGroupBox;
		internal ListView QueueListView;
		internal RichTextBoxEx LogTextBox;
		internal Button PublishItemButton;
		internal ToolStrip ToolStrip1;
		internal ToolStripLabel ItemCountsToolStripLabel;
		internal ToolStripSpringTextBox FindItemToolStripTextBox;
		internal ToolStripButton SearchItemsToolStripButton;
		internal Panel ItemsPanel;
		internal Button RefreshOrRevertItemButton;
		internal ToolStripTextBox SearchItemsToolStripTextBox;
		internal ToolStripButton AddItemToolStripButton;
		internal ToolStripSeparator ToolStripSeparator1;
		internal ToolStripSeparator ToolStripSeparator2;
		internal Button SaveAsTemplateOrDraftItemButton;
		internal TextBoxEx ItemOwnerTextBox;
		internal Label ItemOwnerLabel;
		internal ToolTip ToolTip1;
		internal ToolStripComboBox SearchItemsToolStripComboBox;
		internal Button SaveTemplateButton;
		internal ProgressBarEx QuotaProgressBar;
		internal CheckBox ToggleWordWrapForDescriptionCheckBox;
		internal CheckBox ToggleWordWrapForChangeNoteCheckBox;
		internal Button RefreshGameItemsButton;
		internal Panel GamePanel;
		internal Panel ItemTopPanel;
		internal Panel ItemBottomPanel;
		internal Panel ItemLeftMinScrollPanel;
		internal Panel ItemDescriptionTopPanel;
		internal Panel ItemChangeNoteTopPanel;
		internal Panel ToggleWordWrapForChangeNotePanel;
	}

}