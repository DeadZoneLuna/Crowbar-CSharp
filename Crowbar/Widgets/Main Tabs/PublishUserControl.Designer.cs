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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublishUserControl));
			ToolTip1 = new System.Windows.Forms.ToolTip(components);
			RefreshGameItemsButton = new System.Windows.Forms.Button();
			ItemOwnerLabel = new System.Windows.Forms.Label();
			ItemPostedTextBox = new Crowbar.DateTimeTextBoxEx();
			ItemUpdatedTextBox = new Crowbar.DateTimeTextBoxEx();
			QuotaProgressBar = new Crowbar.ProgressBarEx();
			TopMiddleSplitContainer = new Crowbar.SplitContainerEx();
			ItemsPanel = new System.Windows.Forms.Panel();
			ItemsDataGridView = new System.Windows.Forms.DataGridView();
			ToolStrip1 = new System.Windows.Forms.ToolStrip();
			AddItemToolStripButton = new System.Windows.Forms.ToolStripButton();
			ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			SearchItemsToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			SearchItemsToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			SearchItemsToolStripButton = new System.Windows.Forms.ToolStripButton();
			ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			ItemCountsToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			FindItemToolStripTextBox = new Crowbar.ToolStripSpringTextBox();
			GamePanel = new System.Windows.Forms.Panel();
			GameLabel = new System.Windows.Forms.Label();
			AppIdComboBox = new System.Windows.Forms.ComboBox();
			PublishRequiresSteamLabel = new System.Windows.Forms.Label();
			OpenSteamSubscriberAgreementButton = new System.Windows.Forms.Button();
			MiddleBottomSplitContainer = new System.Windows.Forms.SplitContainer();
			ItemGroupBox = new Crowbar.GroupBoxEx();
			ItemTagsSplitContainer = new System.Windows.Forms.SplitContainer();
			DescriptionChangeNoteSplitContainer = new System.Windows.Forms.SplitContainer();
			ItemDescriptionTextBox = new Crowbar.RichTextBoxEx();
			ItemDescriptionTopPanel = new System.Windows.Forms.Panel();
			ToggleWordWrapForDescriptionCheckBox = new System.Windows.Forms.CheckBox();
			ItemDescriptionLabel = new System.Windows.Forms.Label();
			ItemChangeNoteTextBox = new Crowbar.RichTextBoxEx();
			ItemChangeNoteTopPanel = new System.Windows.Forms.Panel();
			ToggleWordWrapForChangeNotePanel = new System.Windows.Forms.Panel();
			ToggleWordWrapForChangeNoteCheckBox = new System.Windows.Forms.CheckBox();
			ItemChangeNoteLabel = new System.Windows.Forms.Label();
			ItemTopPanel = new System.Windows.Forms.Panel();
			ItemIDLabel = new System.Windows.Forms.Label();
			ItemIDTextBox = new Crowbar.TextBoxEx();
			ItemOwnerTextBox = new Crowbar.TextBoxEx();
			ItemTitleLabel = new System.Windows.Forms.Label();
			ItemTitleTextBox = new Crowbar.TextBoxEx();
			ItemBottomPanel = new System.Windows.Forms.Panel();
			ItemContentFolderOrFileLabel = new System.Windows.Forms.Label();
			ItemContentPathFileNameTextBox = new Crowbar.TextBoxEx();
			BrowseItemContentPathFileNameButton = new System.Windows.Forms.Button();
			ItemPreviewImageLabel = new System.Windows.Forms.Label();
			ItemPreviewImagePathFileNameTextBox = new Crowbar.TextBoxEx();
			BrowseItemPreviewImagePathFileNameButton = new System.Windows.Forms.Button();
			ItemPreviewImagePictureBox = new System.Windows.Forms.PictureBox();
			ItemVisibilityComboBox = new System.Windows.Forms.ComboBox();
			ItemVisibilityLabel = new System.Windows.Forms.Label();
			SaveAsTemplateOrDraftItemButton = new System.Windows.Forms.Button();
			RefreshOrRevertItemButton = new System.Windows.Forms.Button();
			SaveTemplateButton = new System.Windows.Forms.Button();
			OpenWorkshopPageButton = new System.Windows.Forms.Button();
			DeleteItemButton = new System.Windows.Forms.Button();
			ItemLeftMinScrollPanel = new System.Windows.Forms.Panel();
			ItemTagsGroupBox = new Crowbar.GroupBoxEx();
			LogTextBox = new Crowbar.RichTextBoxEx();
			PublishItemButton = new System.Windows.Forms.Button();
			QueueListView = new System.Windows.Forms.ListView();
			((System.ComponentModel.ISupportInitialize)TopMiddleSplitContainer).BeginInit();
			TopMiddleSplitContainer.Panel1.SuspendLayout();
			TopMiddleSplitContainer.Panel2.SuspendLayout();
			TopMiddleSplitContainer.SuspendLayout();
			ItemsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ItemsDataGridView).BeginInit();
			ToolStrip1.SuspendLayout();
			GamePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)MiddleBottomSplitContainer).BeginInit();
			MiddleBottomSplitContainer.Panel1.SuspendLayout();
			MiddleBottomSplitContainer.Panel2.SuspendLayout();
			MiddleBottomSplitContainer.SuspendLayout();
			ItemGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ItemTagsSplitContainer).BeginInit();
			ItemTagsSplitContainer.Panel1.SuspendLayout();
			ItemTagsSplitContainer.Panel2.SuspendLayout();
			ItemTagsSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DescriptionChangeNoteSplitContainer).BeginInit();
			DescriptionChangeNoteSplitContainer.Panel1.SuspendLayout();
			DescriptionChangeNoteSplitContainer.Panel2.SuspendLayout();
			DescriptionChangeNoteSplitContainer.SuspendLayout();
			ItemDescriptionTopPanel.SuspendLayout();
			ItemChangeNoteTopPanel.SuspendLayout();
			ToggleWordWrapForChangeNotePanel.SuspendLayout();
			ItemTopPanel.SuspendLayout();
			ItemBottomPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ItemPreviewImagePictureBox).BeginInit();
			SuspendLayout();
			//
			//RefreshGameItemsButton
			//
			RefreshGameItemsButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			RefreshGameItemsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			RefreshGameItemsButton.FlatAppearance.BorderSize = 0;
			RefreshGameItemsButton.Image = global::Crowbar.Properties.Resources.Refresh;
			RefreshGameItemsButton.Location = new System.Drawing.Point(394, 3);
			RefreshGameItemsButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			RefreshGameItemsButton.Name = "RefreshGameItemsButton";
			RefreshGameItemsButton.Padding = new System.Windows.Forms.Padding(0, 0, 1, 2);
			RefreshGameItemsButton.Size = new System.Drawing.Size(23, 22);
			RefreshGameItemsButton.TabIndex = 36;
			ToolTip1.SetToolTip(RefreshGameItemsButton, "Refresh Game Items");
			RefreshGameItemsButton.UseVisualStyleBackColor = true;
			//
			//ItemOwnerLabel
			//
			ItemOwnerLabel.Location = new System.Drawing.Point(150, 4);
			ItemOwnerLabel.Name = "ItemOwnerLabel";
			ItemOwnerLabel.Size = new System.Drawing.Size(45, 13);
			ItemOwnerLabel.TabIndex = 35;
			ItemOwnerLabel.Text = "Owner:";
			ToolTip1.SetToolTip(ItemOwnerLabel, "Double-click to swap between Steam Name and Steam ID.");
			//
			//ItemPostedTextBox
			//
			ItemPostedTextBox.CueBannerText = "";
			ItemPostedTextBox.Location = new System.Drawing.Point(334, 0);
			ItemPostedTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemPostedTextBox.Name = "ItemPostedTextBox";
			ItemPostedTextBox.ReadOnly = true;
			ItemPostedTextBox.Size = new System.Drawing.Size(120, 22);
			ItemPostedTextBox.TabIndex = 2;
			ToolTip1.SetToolTip(ItemPostedTextBox, "Posted");
			//
			//ItemUpdatedTextBox
			//
			ItemUpdatedTextBox.CueBannerText = "";
			ItemUpdatedTextBox.Location = new System.Drawing.Point(460, 0);
			ItemUpdatedTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemUpdatedTextBox.Name = "ItemUpdatedTextBox";
			ItemUpdatedTextBox.ReadOnly = true;
			ItemUpdatedTextBox.Size = new System.Drawing.Size(120, 22);
			ItemUpdatedTextBox.TabIndex = 3;
			ToolTip1.SetToolTip(ItemUpdatedTextBox, "Updated");
			//
			//QuotaProgressBar
			//
			QuotaProgressBar.ForeColor = System.Drawing.SystemColors.ControlText;
			QuotaProgressBar.Location = new System.Drawing.Point(3, 31);
			QuotaProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			QuotaProgressBar.Name = "QuotaProgressBar";
			QuotaProgressBar.Size = new System.Drawing.Size(125, 22);
			QuotaProgressBar.TabIndex = 37;
			ToolTip1.SetToolTip(QuotaProgressBar, "Quota");
			//
			//TopMiddleSplitContainer
			//
			TopMiddleSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			TopMiddleSplitContainer.Location = new System.Drawing.Point(0, 0);
			TopMiddleSplitContainer.Margin = new System.Windows.Forms.Padding(2);
			TopMiddleSplitContainer.Name = "TopMiddleSplitContainer";
			TopMiddleSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//TopMiddleSplitContainer.Panel1
			//
			TopMiddleSplitContainer.Panel1.Controls.Add(ItemsPanel);
			TopMiddleSplitContainer.Panel1.Controls.Add(GamePanel);
			TopMiddleSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			//
			//TopMiddleSplitContainer.Panel2
			//
			TopMiddleSplitContainer.Panel2.AutoScroll = true;
			TopMiddleSplitContainer.Panel2.Controls.Add(MiddleBottomSplitContainer);
			TopMiddleSplitContainer.Size = new System.Drawing.Size(770, 534);
			TopMiddleSplitContainer.SplitterDistance = 139;
			TopMiddleSplitContainer.TabIndex = 28;
			//
			//ItemsPanel
			//
			ItemsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			ItemsPanel.Controls.Add(ItemsDataGridView);
			ItemsPanel.Controls.Add(ToolStrip1);
			ItemsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			ItemsPanel.Location = new System.Drawing.Point(3, 26);
			ItemsPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemsPanel.Name = "ItemsPanel";
			ItemsPanel.Size = new System.Drawing.Size(764, 110);
			ItemsPanel.TabIndex = 31;
			//
			//ItemsDataGridView
			//
			ItemsDataGridView.AllowUserToAddRows = false;
			ItemsDataGridView.AllowUserToDeleteRows = false;
			ItemsDataGridView.AllowUserToOrderColumns = true;
			ItemsDataGridView.AllowUserToResizeRows = false;
			ItemsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			ItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			ItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			ItemsDataGridView.Location = new System.Drawing.Point(0, 0);
			ItemsDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemsDataGridView.MultiSelect = false;
			ItemsDataGridView.Name = "ItemsDataGridView";
			ItemsDataGridView.ReadOnly = true;
			ItemsDataGridView.RowHeadersVisible = false;
			ItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			ItemsDataGridView.ShowCellErrors = false;
			ItemsDataGridView.ShowRowErrors = false;
			ItemsDataGridView.Size = new System.Drawing.Size(760, 81);
			ItemsDataGridView.TabIndex = 3;
			//
			//ToolStrip1
			//
			ToolStrip1.CanOverflow = false;
			ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {AddItemToolStripButton, ToolStripSeparator1, SearchItemsToolStripComboBox, SearchItemsToolStripTextBox, SearchItemsToolStripButton, ToolStripSeparator2, ItemCountsToolStripLabel, FindItemToolStripTextBox});
			ToolStrip1.Location = new System.Drawing.Point(0, 81);
			ToolStrip1.Name = "ToolStrip1";
			ToolStrip1.Size = new System.Drawing.Size(760, 25);
			ToolStrip1.Stretch = true;
			ToolStrip1.TabIndex = 30;
			ToolStrip1.Text = "ToolStrip1";
			//
			//AddItemToolStripButton
			//
			AddItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			AddItemToolStripButton.Image = (System.Drawing.Image)resources.GetObject("AddItemToolStripButton.Image");
			AddItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			AddItemToolStripButton.Name = "AddItemToolStripButton";
			AddItemToolStripButton.Size = new System.Drawing.Size(60, 22);
			AddItemToolStripButton.Text = "Add Item";
			//
			//ToolStripSeparator1
			//
			ToolStripSeparator1.Name = "ToolStripSeparator1";
			ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			//
			//SearchItemsToolStripComboBox
			//
			SearchItemsToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			SearchItemsToolStripComboBox.Items.AddRange(new object[] {"ID:", "Title:", "Description:", "Owner:", "[All fields]:"});
			SearchItemsToolStripComboBox.Name = "SearchItemsToolStripComboBox";
			SearchItemsToolStripComboBox.Size = new System.Drawing.Size(80, 25);
			SearchItemsToolStripComboBox.ToolTipText = "Field to search";
			//
			//SearchItemsToolStripTextBox
			//
			SearchItemsToolStripTextBox.Name = "SearchItemsToolStripTextBox";
			SearchItemsToolStripTextBox.Size = new System.Drawing.Size(100, 25);
			SearchItemsToolStripTextBox.ToolTipText = "Text to search for";
			//
			//SearchItemsToolStripButton
			//
			SearchItemsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			SearchItemsToolStripButton.Image = global::Crowbar.Properties.Resources.Find;
			SearchItemsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			SearchItemsToolStripButton.Name = "SearchItemsToolStripButton";
			SearchItemsToolStripButton.Size = new System.Drawing.Size(23, 22);
			SearchItemsToolStripButton.Text = "Search";
			SearchItemsToolStripButton.ToolTipText = "Search";
			//
			//ToolStripSeparator2
			//
			ToolStripSeparator2.Name = "ToolStripSeparator2";
			ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			//
			//ItemCountsToolStripLabel
			//
			ItemCountsToolStripLabel.Name = "ItemCountsToolStripLabel";
			ItemCountsToolStripLabel.Size = new System.Drawing.Size(168, 22);
			ItemCountsToolStripLabel.Text = "0 drafts + 0 published = 0 total";
			//
			//FindItemToolStripTextBox
			//
			FindItemToolStripTextBox.Name = "FindItemToolStripTextBox";
			FindItemToolStripTextBox.Size = new System.Drawing.Size(279, 25);
			FindItemToolStripTextBox.ToolTipText = "Title to find";
			FindItemToolStripTextBox.Visible = false;
			//
			//GamePanel
			//
			GamePanel.Controls.Add(GameLabel);
			GamePanel.Controls.Add(AppIdComboBox);
			GamePanel.Controls.Add(RefreshGameItemsButton);
			GamePanel.Controls.Add(PublishRequiresSteamLabel);
			GamePanel.Controls.Add(OpenSteamSubscriberAgreementButton);
			GamePanel.Dock = System.Windows.Forms.DockStyle.Top;
			GamePanel.Location = new System.Drawing.Point(3, 0);
			GamePanel.Name = "GamePanel";
			GamePanel.Size = new System.Drawing.Size(764, 26);
			GamePanel.TabIndex = 37;
			//
			//GameLabel
			//
			GameLabel.Location = new System.Drawing.Point(0, 7);
			GameLabel.Name = "GameLabel";
			GameLabel.Size = new System.Drawing.Size(39, 13);
			GameLabel.TabIndex = 22;
			GameLabel.Text = "Game:";
			//
			//AppIdComboBox
			//
			AppIdComboBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			AppIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			AppIdComboBox.FormattingEnabled = true;
			AppIdComboBox.Location = new System.Drawing.Point(45, 3);
			AppIdComboBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			AppIdComboBox.Name = "AppIdComboBox";
			AppIdComboBox.Size = new System.Drawing.Size(349, 21);
			AppIdComboBox.TabIndex = 0;
			//
			//PublishRequiresSteamLabel
			//
			PublishRequiresSteamLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			PublishRequiresSteamLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			PublishRequiresSteamLabel.Location = new System.Drawing.Point(423, 3);
			PublishRequiresSteamLabel.Name = "PublishRequiresSteamLabel";
			PublishRequiresSteamLabel.Size = new System.Drawing.Size(136, 21);
			PublishRequiresSteamLabel.TabIndex = 1;
			PublishRequiresSteamLabel.Text = "Publish requires Steam";
			PublishRequiresSteamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			//OpenSteamSubscriberAgreementButton
			//
			OpenSteamSubscriberAgreementButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			OpenSteamSubscriberAgreementButton.Location = new System.Drawing.Point(565, 3);
			OpenSteamSubscriberAgreementButton.Name = "OpenSteamSubscriberAgreementButton";
			OpenSteamSubscriberAgreementButton.Size = new System.Drawing.Size(199, 22);
			OpenSteamSubscriberAgreementButton.TabIndex = 2;
			OpenSteamSubscriberAgreementButton.Text = "Open Steam Subscriber Agreement";
			OpenSteamSubscriberAgreementButton.UseVisualStyleBackColor = true;
			//
			//MiddleBottomSplitContainer
			//
			MiddleBottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			MiddleBottomSplitContainer.Location = new System.Drawing.Point(0, 0);
			MiddleBottomSplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			MiddleBottomSplitContainer.Name = "MiddleBottomSplitContainer";
			MiddleBottomSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//MiddleBottomSplitContainer.Panel1
			//
			MiddleBottomSplitContainer.Panel1.Controls.Add(ItemGroupBox);
			MiddleBottomSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			//
			//MiddleBottomSplitContainer.Panel2
			//
			MiddleBottomSplitContainer.Panel2.Controls.Add(LogTextBox);
			MiddleBottomSplitContainer.Panel2.Controls.Add(QuotaProgressBar);
			MiddleBottomSplitContainer.Panel2.Controls.Add(PublishItemButton);
			MiddleBottomSplitContainer.Panel2.Controls.Add(QueueListView);
			MiddleBottomSplitContainer.Panel2MinSize = 45;
			MiddleBottomSplitContainer.Size = new System.Drawing.Size(770, 391);
			MiddleBottomSplitContainer.SplitterDistance = 324;
			MiddleBottomSplitContainer.TabIndex = 26;
			//
			//ItemGroupBox
			//
			ItemGroupBox.Controls.Add(ItemTagsSplitContainer);
			ItemGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			ItemGroupBox.IsReadOnly = false;
			ItemGroupBox.Location = new System.Drawing.Point(3, 4);
			ItemGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemGroupBox.Name = "ItemGroupBox";
			ItemGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemGroupBox.SelectedValue = null;
			ItemGroupBox.Size = new System.Drawing.Size(764, 316);
			ItemGroupBox.TabIndex = 31;
			ItemGroupBox.TabStop = false;
			ItemGroupBox.Text = "Item";
			//
			//ItemTagsSplitContainer
			//
			ItemTagsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			ItemTagsSplitContainer.Location = new System.Drawing.Point(3, 19);
			ItemTagsSplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemTagsSplitContainer.Name = "ItemTagsSplitContainer";
			//
			//ItemTagsSplitContainer.Panel1
			//
			ItemTagsSplitContainer.Panel1.AutoScroll = true;
			ItemTagsSplitContainer.Panel1.Controls.Add(DescriptionChangeNoteSplitContainer);
			ItemTagsSplitContainer.Panel1.Controls.Add(ItemTopPanel);
			ItemTagsSplitContainer.Panel1.Controls.Add(ItemBottomPanel);
			ItemTagsSplitContainer.Panel1.Controls.Add(ItemLeftMinScrollPanel);
			//
			//ItemTagsSplitContainer.Panel2
			//
			ItemTagsSplitContainer.Panel2.Controls.Add(ItemTagsGroupBox);
			ItemTagsSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 4);
			ItemTagsSplitContainer.Size = new System.Drawing.Size(758, 293);
			ItemTagsSplitContainer.SplitterDistance = 580;
			ItemTagsSplitContainer.SplitterWidth = 3;
			ItemTagsSplitContainer.TabIndex = 25;
			//
			//DescriptionChangeNoteSplitContainer
			//
			DescriptionChangeNoteSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			DescriptionChangeNoteSplitContainer.Location = new System.Drawing.Point(0, 68);
			DescriptionChangeNoteSplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			DescriptionChangeNoteSplitContainer.Name = "DescriptionChangeNoteSplitContainer";
			//
			//DescriptionChangeNoteSplitContainer.Panel1
			//
			DescriptionChangeNoteSplitContainer.Panel1.Controls.Add(ItemDescriptionTextBox);
			DescriptionChangeNoteSplitContainer.Panel1.Controls.Add(ItemDescriptionTopPanel);
			DescriptionChangeNoteSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			//
			//DescriptionChangeNoteSplitContainer.Panel2
			//
			DescriptionChangeNoteSplitContainer.Panel2.Controls.Add(ItemChangeNoteTextBox);
			DescriptionChangeNoteSplitContainer.Panel2.Controls.Add(ItemChangeNoteTopPanel);
			DescriptionChangeNoteSplitContainer.Size = new System.Drawing.Size(580, 111);
			DescriptionChangeNoteSplitContainer.SplitterDistance = 295;
			DescriptionChangeNoteSplitContainer.TabIndex = 5;
			//
			//ItemDescriptionTextBox
			//
			ItemDescriptionTextBox.AcceptsTab = true;
			ItemDescriptionTextBox.CueBannerText = "required";
			ItemDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			ItemDescriptionTextBox.Location = new System.Drawing.Point(3, 19);
			ItemDescriptionTextBox.Name = "ItemDescriptionTextBox";
			ItemDescriptionTextBox.Size = new System.Drawing.Size(292, 92);
			ItemDescriptionTextBox.TabIndex = 5;
			ItemDescriptionTextBox.Text = "";
			ItemDescriptionTextBox.WordWrap = false;
			//
			//ItemDescriptionTopPanel
			//
			ItemDescriptionTopPanel.Controls.Add(ToggleWordWrapForDescriptionCheckBox);
			ItemDescriptionTopPanel.Controls.Add(ItemDescriptionLabel);
			ItemDescriptionTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			ItemDescriptionTopPanel.Location = new System.Drawing.Point(3, 0);
			ItemDescriptionTopPanel.Name = "ItemDescriptionTopPanel";
			ItemDescriptionTopPanel.Size = new System.Drawing.Size(292, 19);
			ItemDescriptionTopPanel.TabIndex = 17;
			//
			//ToggleWordWrapForDescriptionCheckBox
			//
			ToggleWordWrapForDescriptionCheckBox.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ToggleWordWrapForDescriptionCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			ToggleWordWrapForDescriptionCheckBox.BackgroundImage = global::Crowbar.Properties.Resources.WordWrapOff;
			ToggleWordWrapForDescriptionCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			ToggleWordWrapForDescriptionCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			ToggleWordWrapForDescriptionCheckBox.Location = new System.Drawing.Point(278, 4);
			ToggleWordWrapForDescriptionCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ToggleWordWrapForDescriptionCheckBox.Name = "ToggleWordWrapForDescriptionCheckBox";
			ToggleWordWrapForDescriptionCheckBox.Size = new System.Drawing.Size(13, 13);
			ToggleWordWrapForDescriptionCheckBox.TabIndex = 16;
			ToggleWordWrapForDescriptionCheckBox.UseVisualStyleBackColor = true;
			//
			//ItemDescriptionLabel
			//
			ItemDescriptionLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemDescriptionLabel.Location = new System.Drawing.Point(0, 4);
			ItemDescriptionLabel.Name = "ItemDescriptionLabel";
			ItemDescriptionLabel.Size = new System.Drawing.Size(275, 13);
			ItemDescriptionLabel.TabIndex = 9;
			ItemDescriptionLabel.Text = "Description (### / ### characters max):";
			//
			//ItemChangeNoteTextBox
			//
			ItemChangeNoteTextBox.AcceptsTab = true;
			ItemChangeNoteTextBox.CueBannerText = "";
			ItemChangeNoteTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			ItemChangeNoteTextBox.Location = new System.Drawing.Point(0, 19);
			ItemChangeNoteTextBox.Name = "ItemChangeNoteTextBox";
			ItemChangeNoteTextBox.Size = new System.Drawing.Size(281, 92);
			ItemChangeNoteTextBox.TabIndex = 6;
			ItemChangeNoteTextBox.Text = "";
			ItemChangeNoteTextBox.WordWrap = false;
			//
			//ItemChangeNoteTopPanel
			//
			ItemChangeNoteTopPanel.Controls.Add(ToggleWordWrapForChangeNotePanel);
			ItemChangeNoteTopPanel.Controls.Add(ItemChangeNoteLabel);
			ItemChangeNoteTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			ItemChangeNoteTopPanel.Location = new System.Drawing.Point(0, 0);
			ItemChangeNoteTopPanel.Name = "ItemChangeNoteTopPanel";
			ItemChangeNoteTopPanel.Size = new System.Drawing.Size(281, 19);
			ItemChangeNoteTopPanel.TabIndex = 18;
			//
			//ToggleWordWrapForChangeNotePanel
			//
			ToggleWordWrapForChangeNotePanel.Controls.Add(ToggleWordWrapForChangeNoteCheckBox);
			ToggleWordWrapForChangeNotePanel.Dock = System.Windows.Forms.DockStyle.Right;
			ToggleWordWrapForChangeNotePanel.Location = new System.Drawing.Point(267, 0);
			ToggleWordWrapForChangeNotePanel.Name = "ToggleWordWrapForChangeNotePanel";
			ToggleWordWrapForChangeNotePanel.Size = new System.Drawing.Size(14, 19);
			ToggleWordWrapForChangeNotePanel.TabIndex = 18;
			//
			//ToggleWordWrapForChangeNoteCheckBox
			//
			ToggleWordWrapForChangeNoteCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			ToggleWordWrapForChangeNoteCheckBox.BackgroundImage = global::Crowbar.Properties.Resources.WordWrapOff;
			ToggleWordWrapForChangeNoteCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			ToggleWordWrapForChangeNoteCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			ToggleWordWrapForChangeNoteCheckBox.Location = new System.Drawing.Point(0, 4);
			ToggleWordWrapForChangeNoteCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ToggleWordWrapForChangeNoteCheckBox.Name = "ToggleWordWrapForChangeNoteCheckBox";
			ToggleWordWrapForChangeNoteCheckBox.Size = new System.Drawing.Size(13, 13);
			ToggleWordWrapForChangeNoteCheckBox.TabIndex = 17;
			ToggleWordWrapForChangeNoteCheckBox.UseVisualStyleBackColor = true;
			//
			//ItemChangeNoteLabel
			//
			ItemChangeNoteLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemChangeNoteLabel.Location = new System.Drawing.Point(0, 4);
			ItemChangeNoteLabel.Name = "ItemChangeNoteLabel";
			ItemChangeNoteLabel.Size = new System.Drawing.Size(264, 13);
			ItemChangeNoteLabel.TabIndex = 11;
			ItemChangeNoteLabel.Text = "Content Changes (### / ### characters max):";
			//
			//ItemTopPanel
			//
			ItemTopPanel.Controls.Add(ItemIDLabel);
			ItemTopPanel.Controls.Add(ItemIDTextBox);
			ItemTopPanel.Controls.Add(ItemOwnerLabel);
			ItemTopPanel.Controls.Add(ItemOwnerTextBox);
			ItemTopPanel.Controls.Add(ItemPostedTextBox);
			ItemTopPanel.Controls.Add(ItemUpdatedTextBox);
			ItemTopPanel.Controls.Add(ItemTitleLabel);
			ItemTopPanel.Controls.Add(ItemTitleTextBox);
			ItemTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			ItemTopPanel.Location = new System.Drawing.Point(0, 0);
			ItemTopPanel.Name = "ItemTopPanel";
			ItemTopPanel.Size = new System.Drawing.Size(580, 68);
			ItemTopPanel.TabIndex = 36;
			//
			//ItemIDLabel
			//
			ItemIDLabel.Location = new System.Drawing.Point(3, 4);
			ItemIDLabel.Name = "ItemIDLabel";
			ItemIDLabel.Size = new System.Drawing.Size(25, 13);
			ItemIDLabel.TabIndex = 4;
			ItemIDLabel.Text = "ID:";
			//
			//ItemIDTextBox
			//
			ItemIDTextBox.CueBannerText = "";
			ItemIDTextBox.Location = new System.Drawing.Point(34, 0);
			ItemIDTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemIDTextBox.Name = "ItemIDTextBox";
			ItemIDTextBox.ReadOnly = true;
			ItemIDTextBox.Size = new System.Drawing.Size(110, 22);
			ItemIDTextBox.TabIndex = 0;
			//
			//ItemOwnerTextBox
			//
			ItemOwnerTextBox.CueBannerText = "";
			ItemOwnerTextBox.Location = new System.Drawing.Point(201, 0);
			ItemOwnerTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemOwnerTextBox.Name = "ItemOwnerTextBox";
			ItemOwnerTextBox.ReadOnly = true;
			ItemOwnerTextBox.Size = new System.Drawing.Size(120, 22);
			ItemOwnerTextBox.TabIndex = 1;
			//
			//ItemTitleLabel
			//
			ItemTitleLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemTitleLabel.Location = new System.Drawing.Point(3, 28);
			ItemTitleLabel.Name = "ItemTitleLabel";
			ItemTitleLabel.Size = new System.Drawing.Size(577, 12);
			ItemTitleLabel.TabIndex = 8;
			ItemTitleLabel.Text = "Title (### / ### characters max):";
			//
			//ItemTitleTextBox
			//
			ItemTitleTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemTitleTextBox.CueBannerText = "required";
			ItemTitleTextBox.Location = new System.Drawing.Point(3, 44);
			ItemTitleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemTitleTextBox.Name = "ItemTitleTextBox";
			ItemTitleTextBox.Size = new System.Drawing.Size(577, 22);
			ItemTitleTextBox.TabIndex = 4;
			ItemTitleTextBox.WordWrap = false;
			//
			//ItemBottomPanel
			//
			ItemBottomPanel.Controls.Add(ItemContentFolderOrFileLabel);
			ItemBottomPanel.Controls.Add(ItemContentPathFileNameTextBox);
			ItemBottomPanel.Controls.Add(BrowseItemContentPathFileNameButton);
			ItemBottomPanel.Controls.Add(ItemPreviewImageLabel);
			ItemBottomPanel.Controls.Add(ItemPreviewImagePathFileNameTextBox);
			ItemBottomPanel.Controls.Add(BrowseItemPreviewImagePathFileNameButton);
			ItemBottomPanel.Controls.Add(ItemPreviewImagePictureBox);
			ItemBottomPanel.Controls.Add(ItemVisibilityComboBox);
			ItemBottomPanel.Controls.Add(ItemVisibilityLabel);
			ItemBottomPanel.Controls.Add(SaveAsTemplateOrDraftItemButton);
			ItemBottomPanel.Controls.Add(RefreshOrRevertItemButton);
			ItemBottomPanel.Controls.Add(SaveTemplateButton);
			ItemBottomPanel.Controls.Add(OpenWorkshopPageButton);
			ItemBottomPanel.Controls.Add(DeleteItemButton);
			ItemBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			ItemBottomPanel.Location = new System.Drawing.Point(0, 179);
			ItemBottomPanel.Name = "ItemBottomPanel";
			ItemBottomPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			ItemBottomPanel.Size = new System.Drawing.Size(580, 114);
			ItemBottomPanel.TabIndex = 37;
			//
			//ItemContentFolderOrFileLabel
			//
			ItemContentFolderOrFileLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemContentFolderOrFileLabel.Location = new System.Drawing.Point(3, 3);
			ItemContentFolderOrFileLabel.Name = "ItemContentFolderOrFileLabel";
			ItemContentFolderOrFileLabel.Size = new System.Drawing.Size(415, 13);
			ItemContentFolderOrFileLabel.TabIndex = 18;
			ItemContentFolderOrFileLabel.Text = "Content Folder or File (### / ### MB max):";
			//
			//ItemContentPathFileNameTextBox
			//
			ItemContentPathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemContentPathFileNameTextBox.CueBannerText = "required";
			ItemContentPathFileNameTextBox.Location = new System.Drawing.Point(3, 19);
			ItemContentPathFileNameTextBox.Name = "ItemContentPathFileNameTextBox";
			ItemContentPathFileNameTextBox.Size = new System.Drawing.Size(415, 22);
			ItemContentPathFileNameTextBox.TabIndex = 7;
			ItemContentPathFileNameTextBox.WordWrap = false;
			//
			//BrowseItemContentPathFileNameButton
			//
			BrowseItemContentPathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseItemContentPathFileNameButton.Location = new System.Drawing.Point(424, 19);
			BrowseItemContentPathFileNameButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			BrowseItemContentPathFileNameButton.Name = "BrowseItemContentPathFileNameButton";
			BrowseItemContentPathFileNameButton.Size = new System.Drawing.Size(75, 22);
			BrowseItemContentPathFileNameButton.TabIndex = 8;
			BrowseItemContentPathFileNameButton.Text = "Browse...";
			BrowseItemContentPathFileNameButton.UseVisualStyleBackColor = true;
			//
			//ItemPreviewImageLabel
			//
			ItemPreviewImageLabel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemPreviewImageLabel.Location = new System.Drawing.Point(3, 45);
			ItemPreviewImageLabel.Name = "ItemPreviewImageLabel";
			ItemPreviewImageLabel.Size = new System.Drawing.Size(415, 13);
			ItemPreviewImageLabel.TabIndex = 13;
			ItemPreviewImageLabel.Text = "Preview Image (### / ### MB max |  Required resolution: 512x512):";
			//
			//ItemPreviewImagePathFileNameTextBox
			//
			ItemPreviewImagePathFileNameTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			ItemPreviewImagePathFileNameTextBox.CueBannerText = "required";
			ItemPreviewImagePathFileNameTextBox.Location = new System.Drawing.Point(3, 60);
			ItemPreviewImagePathFileNameTextBox.Name = "ItemPreviewImagePathFileNameTextBox";
			ItemPreviewImagePathFileNameTextBox.Size = new System.Drawing.Size(415, 22);
			ItemPreviewImagePathFileNameTextBox.TabIndex = 9;
			ItemPreviewImagePathFileNameTextBox.WordWrap = false;
			//
			//BrowseItemPreviewImagePathFileNameButton
			//
			BrowseItemPreviewImagePathFileNameButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			BrowseItemPreviewImagePathFileNameButton.Location = new System.Drawing.Point(424, 60);
			BrowseItemPreviewImagePathFileNameButton.Name = "BrowseItemPreviewImagePathFileNameButton";
			BrowseItemPreviewImagePathFileNameButton.Size = new System.Drawing.Size(75, 22);
			BrowseItemPreviewImagePathFileNameButton.TabIndex = 10;
			BrowseItemPreviewImagePathFileNameButton.Text = "Browse...";
			BrowseItemPreviewImagePathFileNameButton.UseVisualStyleBackColor = true;
			//
			//ItemPreviewImagePictureBox
			//
			ItemPreviewImagePictureBox.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ItemPreviewImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			ItemPreviewImagePictureBox.Location = new System.Drawing.Point(505, 7);
			ItemPreviewImagePictureBox.Name = "ItemPreviewImagePictureBox";
			ItemPreviewImagePictureBox.Size = new System.Drawing.Size(75, 75);
			ItemPreviewImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			ItemPreviewImagePictureBox.TabIndex = 27;
			ItemPreviewImagePictureBox.TabStop = false;
			//
			//ItemVisibilityComboBox
			//
			ItemVisibilityComboBox.FormattingEnabled = true;
			ItemVisibilityComboBox.Location = new System.Drawing.Point(63, 88);
			ItemVisibilityComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemVisibilityComboBox.Name = "ItemVisibilityComboBox";
			ItemVisibilityComboBox.Size = new System.Drawing.Size(100, 21);
			ItemVisibilityComboBox.TabIndex = 11;
			//
			//ItemVisibilityLabel
			//
			ItemVisibilityLabel.Location = new System.Drawing.Point(3, 92);
			ItemVisibilityLabel.Name = "ItemVisibilityLabel";
			ItemVisibilityLabel.Size = new System.Drawing.Size(54, 13);
			ItemVisibilityLabel.TabIndex = 10;
			ItemVisibilityLabel.Text = "Visibility:";
			//
			//SaveAsTemplateOrDraftItemButton
			//
			SaveAsTemplateOrDraftItemButton.Location = new System.Drawing.Point(169, 88);
			SaveAsTemplateOrDraftItemButton.Name = "SaveAsTemplateOrDraftItemButton";
			SaveAsTemplateOrDraftItemButton.Size = new System.Drawing.Size(111, 22);
			SaveAsTemplateOrDraftItemButton.TabIndex = 12;
			SaveAsTemplateOrDraftItemButton.Text = "Save as Template";
			SaveAsTemplateOrDraftItemButton.UseVisualStyleBackColor = true;
			//
			//RefreshOrRevertItemButton
			//
			RefreshOrRevertItemButton.Location = new System.Drawing.Point(286, 88);
			RefreshOrRevertItemButton.Name = "RefreshOrRevertItemButton";
			RefreshOrRevertItemButton.Size = new System.Drawing.Size(75, 22);
			RefreshOrRevertItemButton.TabIndex = 13;
			RefreshOrRevertItemButton.Text = "Refresh";
			RefreshOrRevertItemButton.UseVisualStyleBackColor = true;
			//
			//SaveTemplateButton
			//
			SaveTemplateButton.Location = new System.Drawing.Point(367, 88);
			SaveTemplateButton.Name = "SaveTemplateButton";
			SaveTemplateButton.Size = new System.Drawing.Size(75, 22);
			SaveTemplateButton.TabIndex = 14;
			SaveTemplateButton.Text = "Save";
			SaveTemplateButton.UseVisualStyleBackColor = true;
			//
			//OpenWorkshopPageButton
			//
			OpenWorkshopPageButton.Enabled = false;
			OpenWorkshopPageButton.Location = new System.Drawing.Point(367, 88);
			OpenWorkshopPageButton.Name = "OpenWorkshopPageButton";
			OpenWorkshopPageButton.Size = new System.Drawing.Size(132, 22);
			OpenWorkshopPageButton.TabIndex = 15;
			OpenWorkshopPageButton.Text = "Open Workshop Page";
			OpenWorkshopPageButton.UseVisualStyleBackColor = true;
			//
			//DeleteItemButton
			//
			DeleteItemButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			DeleteItemButton.Location = new System.Drawing.Point(505, 88);
			DeleteItemButton.Name = "DeleteItemButton";
			DeleteItemButton.Size = new System.Drawing.Size(75, 22);
			DeleteItemButton.TabIndex = 16;
			DeleteItemButton.Text = "Delete...";
			DeleteItemButton.UseVisualStyleBackColor = true;
			//
			//ItemLeftMinScrollPanel
			//
			ItemLeftMinScrollPanel.Location = new System.Drawing.Point(0, 0);
			ItemLeftMinScrollPanel.Name = "ItemLeftMinScrollPanel";
			ItemLeftMinScrollPanel.Size = new System.Drawing.Size(580, 1);
			ItemLeftMinScrollPanel.TabIndex = 38;
			//
			//ItemTagsGroupBox
			//
			ItemTagsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			ItemTagsGroupBox.IsReadOnly = false;
			ItemTagsGroupBox.Location = new System.Drawing.Point(0, 0);
			ItemTagsGroupBox.Margin = new System.Windows.Forms.Padding(0);
			ItemTagsGroupBox.Name = "ItemTagsGroupBox";
			ItemTagsGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			ItemTagsGroupBox.SelectedValue = null;
			ItemTagsGroupBox.Size = new System.Drawing.Size(172, 289);
			ItemTagsGroupBox.TabIndex = 17;
			ItemTagsGroupBox.TabStop = false;
			ItemTagsGroupBox.Text = "Tags";
			//
			//LogTextBox
			//
			LogTextBox.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			LogTextBox.CueBannerText = "";
			LogTextBox.HideSelection = false;
			LogTextBox.Location = new System.Drawing.Point(134, 0);
			LogTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			LogTextBox.Name = "LogTextBox";
			LogTextBox.ReadOnly = true;
			LogTextBox.Size = new System.Drawing.Size(633, 59);
			LogTextBox.TabIndex = 19;
			LogTextBox.Text = "";
			LogTextBox.WordWrap = false;
			//
			//PublishItemButton
			//
			PublishItemButton.Location = new System.Drawing.Point(3, 0);
			PublishItemButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			PublishItemButton.Name = "PublishItemButton";
			PublishItemButton.Size = new System.Drawing.Size(125, 22);
			PublishItemButton.TabIndex = 18;
			PublishItemButton.Text = "Publish";
			PublishItemButton.UseVisualStyleBackColor = true;
			//
			//QueueListView
			//
			QueueListView.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right);
			QueueListView.HideSelection = false;
			QueueListView.Location = new System.Drawing.Point(568, 0);
			QueueListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			QueueListView.Name = "QueueListView";
			QueueListView.Size = new System.Drawing.Size(199, 63);
			QueueListView.TabIndex = 20;
			QueueListView.UseCompatibleStateImageBehavior = false;
			QueueListView.Visible = false;
			//
			//PublishUserControl
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(TopMiddleSplitContainer);
			Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			Name = "PublishUserControl";
			Size = new System.Drawing.Size(770, 534);
			TopMiddleSplitContainer.Panel1.ResumeLayout(false);
			TopMiddleSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)TopMiddleSplitContainer).EndInit();
			TopMiddleSplitContainer.ResumeLayout(false);
			ItemsPanel.ResumeLayout(false);
			ItemsPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ItemsDataGridView).EndInit();
			ToolStrip1.ResumeLayout(false);
			ToolStrip1.PerformLayout();
			GamePanel.ResumeLayout(false);
			MiddleBottomSplitContainer.Panel1.ResumeLayout(false);
			MiddleBottomSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)MiddleBottomSplitContainer).EndInit();
			MiddleBottomSplitContainer.ResumeLayout(false);
			ItemGroupBox.ResumeLayout(false);
			ItemTagsSplitContainer.Panel1.ResumeLayout(false);
			ItemTagsSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ItemTagsSplitContainer).EndInit();
			ItemTagsSplitContainer.ResumeLayout(false);
			DescriptionChangeNoteSplitContainer.Panel1.ResumeLayout(false);
			DescriptionChangeNoteSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DescriptionChangeNoteSplitContainer).EndInit();
			DescriptionChangeNoteSplitContainer.ResumeLayout(false);
			ItemDescriptionTopPanel.ResumeLayout(false);
			ItemChangeNoteTopPanel.ResumeLayout(false);
			ToggleWordWrapForChangeNotePanel.ResumeLayout(false);
			ItemTopPanel.ResumeLayout(false);
			ItemTopPanel.PerformLayout();
			ItemBottomPanel.ResumeLayout(false);
			ItemBottomPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ItemPreviewImagePictureBox).EndInit();
			ResumeLayout(false);

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