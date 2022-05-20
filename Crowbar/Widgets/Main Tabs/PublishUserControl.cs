//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crowbar
{
	public partial class PublishUserControl
	{

#region Create and Destroy

		public PublishUserControl() : base()
		{
			// This call is required by the designer.
			InitializeComponent();

			// Set the ToolStrip and its child controls to use same default FontSize as the other controls. 
			//    Inexplicably, the default FontSize for them is 9 instead of 8.25 like all other controls.
			this.ToolStrip1.Font = this.Font;
			foreach (Control widget in this.ToolStrip1.Controls)
			{
				widget.Font = this.Font;
			}

			this.UseInDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.UseInDownloadToolStripMenuItem.Name = "ItemsDataGridViewUseInDownloadToolStripMenuItem";
			this.UseInDownloadToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.UseInDownloadToolStripMenuItem.Text = "Use in Download";

			this.ItemContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ItemContextMenuStrip.Items.Add(this.UseInDownloadToolStripMenuItem);
			this.ItemContextMenuStrip.Name = "ItemsDataGridViewContextMenuStrip";
			this.ItemContextMenuStrip.Size = new System.Drawing.Size(177, 114);
			this.ContextMenuStrip = this.ItemContextMenuStrip;

			//Me.ItemsDataGridViewUseInDownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			//Me.ItemsDataGridViewUseInDownloadToolStripMenuItem.Name = "ItemsDataGridViewUseInDownloadToolStripMenuItem"
			//Me.ItemsDataGridViewUseInDownloadToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
			//Me.ItemsDataGridViewUseInDownloadToolStripMenuItem.Text = "Use in Download"

			//Me.ItemsDataGridViewContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
			//Me.ItemsDataGridViewContextMenuStrip.Items.Add(Me.ItemsDataGridViewUseInDownloadToolStripMenuItem)
			//Me.ItemsDataGridViewContextMenuStrip.Name = "ItemsDataGridViewContextMenuStrip"
			//Me.ItemsDataGridViewContextMenuStrip.Size = New System.Drawing.Size(177, 114)
			//Me.ItemsDataGridView.ContextMenuStrip = Me.ItemsDataGridViewContextMenuStrip

			//Me.ItemIdLabelUseInDownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			//Me.ItemIdLabelUseInDownloadToolStripMenuItem.Name = "ItemIdLabelUseInDownloadToolStripMenuItem"
			//Me.ItemIdLabelUseInDownloadToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
			//Me.ItemIdLabelUseInDownloadToolStripMenuItem.Text = "Use in Download"

			//Me.ItemIdLabelContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
			//Me.ItemIdLabelContextMenuStrip.Items.Add(Me.ItemIdLabelUseInDownloadToolStripMenuItem)
			//Me.ItemIdLabelContextMenuStrip.Name = "ItemIdLabelContextMenuStrip"
			//Me.ItemIdLabelContextMenuStrip.Size = New System.Drawing.Size(177, 114)
			//Me.ItemIDLabel.ContextMenuStrip = Me.ItemIdLabelContextMenuStrip

			//Me.ItemIdTextBoxUseInDownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			//Me.ItemIdTextBoxUseInDownloadToolStripMenuItem.Name = "ItemIdTextBoxUseInDownloadToolStripMenuItem"
			//Me.ItemIdTextBoxUseInDownloadToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
			//Me.ItemIdTextBoxUseInDownloadToolStripMenuItem.Text = "Use in Download"

			//Me.ItemIdTextBoxContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
			//Me.ItemIdTextBoxContextMenuStrip.Items.Add(Me.ItemIdTextBoxUseInDownloadToolStripMenuItem)
			//Me.ItemIdTextBoxContextMenuStrip.Name = "ItemIdTextBoxContextMenuStrip"
			//Me.ItemIdTextBoxContextMenuStrip.Size = New System.Drawing.Size(177, 114)
			//Me.ItemIDTextBox.ContextMenuStrip = Me.ItemIdTextBoxContextMenuStrip
		}

		//UserControl overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Free();
					if (components != null)
					{
						components.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

#endregion

#region Init and Free

		private void Init()
		{
			MainCROWBAR.TheApp.InitAppInfo();

			if (MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex >= MainCROWBAR.TheApp.SteamAppInfos.Count)
			{
				MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex = 0;
			}
			this.AppIdComboBox.DisplayMember = "Name";
			this.AppIdComboBox.ValueMember = "ID";
			this.AppIdComboBox.DataSource = MainCROWBAR.TheApp.SteamAppInfos;
			this.AppIdComboBox.DataBindings.Add("SelectedIndex", MainCROWBAR.TheApp.Settings, "PublishGameSelectedIndex", false, DataSourceUpdateMode.OnPropertyChanged);

			this.theBackgroundSteamPipe = new BackgroundSteamPipe();

			this.GetUserSteamID();

			this.theSelectedItemIsChangingViaMe = true;
			this.theItemBindingSource = new BindingSource();
			this.InitItemListWidgets();
			this.theItemBindingSource.DataSource = this.theDisplayedItems;
			this.InitItemDetailWidgets();
			this.theSelectedItemIsChangingViaMe = false;

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;

			this.theSelectedGameIsStillUpdatingInterface = false;
			this.UpdateSteamAppWidgets();
		}

		//NOTE: This is called after all child widgets (created via designer) are disposed but before this UserControl is disposed.
		private void Free()
		{
			if (this.theBackgroundSteamPipe != null)
			{
				this.theBackgroundSteamPipe.Kill();
			}

			if (this.theTagsWidget != null)
			{
				this.theTagsWidget.TagsPropertyChanged -= this.TagsWidget_TagsPropertyChanged;
			}

			if (this.theSelectedItem != null)
			{
				if (this.theSelectedItem.IsTemplate && this.theSelectedItem.IsChanged)
				{
					this.SaveChangedTemplateToDraft();
				}
				this.theSelectedItem.PropertyChanged -= this.WorkshopItem_PropertyChanged;
			}

			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
		}

		private void GetUserSteamID()
		{
			SteamPipe steamPipe = new SteamPipe();
			string result = steamPipe.Open("GetUserSteamID", null, "");
			if (result != "success")
			{
				this.theUserSteamID = 0;
				return;
			}
			this.theUserSteamID = steamPipe.GetUserSteamID();
			steamPipe.Shut();
		}

		//NOTE: Gets the quota for the logged-in Steam user for the selected SteamApp. 
		private void GetUserSteamAppCloudQuota()
		{
			if (this.theSteamAppInfo.UsesSteamUGC)
			{
				this.QuotaProgressBar.Text = "";
				this.QuotaProgressBar.Value = 0;
				this.ToolTip1.SetToolTip(this.QuotaProgressBar, "");
			}
			else
			{
				SteamPipe steamPipe = new SteamPipe();
				string result = steamPipe.Open("GetQuota", null, "");
				if (result != "success")
				{
					this.theUserSteamID = 0;
					return;
				}
				ulong availableBytes = 0;
				ulong totalBytes = 0;
				steamPipe.GetQuota(ref availableBytes, ref totalBytes);
				steamPipe.Shut();

				if (totalBytes == 0)
				{
					this.QuotaProgressBar.Text = "unknown";
					this.QuotaProgressBar.Value = 0;
					this.ToolTip1.SetToolTip(this.QuotaProgressBar, "Quota (unknown)");
				}
				else
				{
					ulong usedBytes = totalBytes - availableBytes;
					int progressPercentage = Convert.ToInt32(usedBytes * (ulong)this.QuotaProgressBar.Maximum / totalBytes);
					string availableBytesText = MathModule.ByteUnitsConversion(availableBytes);
					string usedBytesText = MathModule.ByteUnitsConversion(usedBytes);
					string totalBytesText = MathModule.ByteUnitsConversion(totalBytes);
					this.QuotaProgressBar.Text = availableBytesText + " available ";
					this.QuotaProgressBar.Value = progressPercentage;
					this.ToolTip1.SetToolTip(this.QuotaProgressBar, "Quota: " + usedBytesText + " used of " + totalBytesText + " total (" + progressPercentage.ToString() + "% used)");
				}
			}
		}

		private void InitItemListWidgets()
		{
			this.theDisplayedItems = new WorkshopItemBindingList();
			this.theEntireListOfItems = new WorkshopItemBindingList();

			this.ItemsDataGridView.AutoGenerateColumns = false;
			this.ItemsDataGridView.DataSource = this.theItemBindingSource;

			DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();

			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "IsChanged";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			//textColumn.Frozen = True
			textColumn.HeaderText = "*";
			textColumn.MinimumWidth = 20;
			textColumn.Name = "IsChanged";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 20;
			this.ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "ID";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Item ID";
			textColumn.Name = "ID";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 100;
			this.ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Title";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Title";
			textColumn.Name = "Title";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 200;
			this.ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Posted";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Posted";
			textColumn.Name = "Posted";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 110;
			this.ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Updated";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Updated";
			textColumn.Name = "Updated";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 110;
			this.ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Visibility";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Visibility";
			textColumn.Name = "Visibility";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 75;
			this.ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "OwnerName";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Owner";
			textColumn.Name = "Owner";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 120;
			this.ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.DataPropertyName = "";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.FillWeight = 100;
			textColumn.HeaderText = "";
			textColumn.Name = "";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.ItemsDataGridView.Columns.Add(textColumn);

			this.SearchItemsToolStripComboBox.ComboBox.DisplayMember = "Value";
			this.SearchItemsToolStripComboBox.ComboBox.ValueMember = "Key";
			this.SearchItemsToolStripComboBox.ComboBox.DataSource = EnumHelper.ToList(typeof(AppEnums.PublishSearchFieldOptions));
			this.SearchItemsToolStripComboBox.ComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "PublishSearchField", false, DataSourceUpdateMode.OnPropertyChanged);
			this.SearchItemsToolStripTextBox.TextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "PublishSearchText", false, DataSourceUpdateMode.OnValidation);
		}

		private void InitItemDetailWidgets()
		{
			this.ItemTitleTextBox.MaxLength = (int)Steamworks.Constants.k_cchPublishedDocumentTitleMax;
			this.ItemDescriptionTextBox.MaxLength = (int)Steamworks.Constants.k_cchPublishedDocumentDescriptionMax;
			this.ItemChangeNoteTextBox.MaxLength = (int)Steamworks.Constants.k_cchPublishedDocumentChangeDescriptionMax;

			this.ItemIDTextBox.DataBindings.Add("Text", this.theItemBindingSource, "ID", false, DataSourceUpdateMode.OnValidation);
			//TODO: Change ID textbox to combobox dropdownlist that lists most-recently accessed IDs, including those selected via list.
			//Dim anEnumList As IList
			//anEnumList = EnumHelper.ToList(GetType(SteamUGCPublishedFileVisibility))
			//Me.ItemIDComboBox.DisplayMember = "Value"
			//Me.ItemIDComboBox.ValueMember = "Key"
			//Me.ItemIDComboBox.DataSource = anEnumList
			//Me.ItemIDComboBox.DataBindings.Add("SelectedValue", Me.theItemBindingSource, "ID", False, DataSourceUpdateMode.OnPropertyChanged)

			this.ItemOwnerTextBox.DataBindings.Add("Text", this.theItemBindingSource, "OwnerName", false, DataSourceUpdateMode.OnValidation);
			this.ItemPostedTextBox.DataBindings.Add("Text", this.theItemBindingSource, "Posted", false, DataSourceUpdateMode.OnValidation);
			this.ItemUpdatedTextBox.DataBindings.Add("Text", this.theItemBindingSource, "Updated", false, DataSourceUpdateMode.OnValidation);
			this.ItemTitleTextBox.DataBindings.Add("Text", this.theItemBindingSource, "Title", false, DataSourceUpdateMode.OnPropertyChanged);
			//NOTE: For RichTextBox, set the Formatting argument to True when DataSourceUpdateMode.OnPropertyChanged is used, to prevent characters being entered in reverse order.
			this.ItemDescriptionTextBox.DataBindings.Add("Text", this.theItemBindingSource, "Description", true, DataSourceUpdateMode.OnPropertyChanged);
			this.ItemChangeNoteTextBox.DataBindings.Add("Text", this.theItemBindingSource, "ChangeNote", true, DataSourceUpdateMode.OnPropertyChanged);
			this.ItemContentPathFileNameTextBox.DataBindings.Add("Text", this.theItemBindingSource, "ContentPathFolderOrFileName", false, DataSourceUpdateMode.OnValidation);
			this.ItemPreviewImagePathFileNameTextBox.DataBindings.Add("Text", this.theItemBindingSource, "PreviewImagePathFileName", false, DataSourceUpdateMode.OnValidation);

			this.ItemVisibilityComboBox.DisplayMember = "Value";
			this.ItemVisibilityComboBox.ValueMember = "Key";
			this.ItemVisibilityComboBox.DataSource = EnumHelper.ToList(typeof(WorkshopItem.SteamUGCPublishedItemVisibility));
			this.ItemVisibilityComboBox.DataBindings.Add("SelectedValue", this.theItemBindingSource, "Visibility", false, DataSourceUpdateMode.OnPropertyChanged);
		}

#endregion

#region Properties

#endregion

#region Methods

#endregion

#region Widget Event Handlers

		private void PublishUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.AppIdComboBox, this.RefreshGameItemsButton);

			if (!this.DesignMode)
			{
				this.Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		private void RefreshGameItemsButton_Click(object sender, EventArgs e)
		{
			this.UpdateSteamAppWidgets();
		}

		private void OpenSteamSubscriberAgreementButton_Click(object sender, EventArgs e)
		{
			this.OpenSteamSubscriberAgreement();
		}

		private void ItemsDataGridView_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
		{
			if (e.FormattingApplied)
			{
				return;
			}

			DataGridView dgv = (DataGridView)sender;
			DataGridViewCell cell = dgv[e.ColumnIndex, e.RowIndex];

			if (cell.Value != null)
			{
				if (dgv.Columns[e.ColumnIndex].Name == "IsChanged")
				{
					bool itemIsChanged = Convert.ToBoolean(cell.Value);
					if (itemIsChanged)
					{
						e.Value = "*";
					}
					else
					{
						e.Value = "";
					}
					e.FormattingApplied = true;
				}
				else if ((dgv.Columns[e.ColumnIndex].Name == "Posted") || (dgv.Columns[e.ColumnIndex].Name == "Updated"))
				{
					DateTime aDateTime = MathModule.UnixTimeStampToDateTime(long.Parse((cell.Value == null ? null : Convert.ToString(cell.Value))));
					e.Value = aDateTime.ToShortDateString() + " " + aDateTime.ToShortTimeString();
					e.FormattingApplied = true;
				}
				else if (dgv.Columns[e.ColumnIndex].Name == "Visibility")
				{
					WorkshopItem.SteamUGCPublishedItemVisibility vis = (WorkshopItem.SteamUGCPublishedItemVisibility)Enum.Parse(typeof(WorkshopItem.SteamUGCPublishedItemVisibility), (cell.Value == null ? null : Convert.ToString(cell.Value)));
					e.Value = EnumHelper.GetDescription(vis);
					e.FormattingApplied = true;
				}
			}
		}

		// Prevent the inexplicable showing of an error window, even though the previous Crowbar version did not ever show the window.
		private void ItemsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = true;
		}

		private void ItemsDataGridView_SelectionChanged(object sender, EventArgs e)
		{
			if (!this.theSelectedItemIsChangingViaMe && this.ItemsDataGridView.SelectedRows.Count > 0)
			{
				//If Me.ItemsDataGridView.SelectedRows.Count > 0 Then
				//NOTE: Allow the highlight to show in the grid before updating item details.
				Application.DoEvents();
				if (this.theSelectedItem != null && this.theSelectedItem.IsTemplate && this.theSelectedItem.IsChanged)
				{
					this.SaveChangedTemplateToDraft();
				}
				this.UpdateItemDetails();
			}
		}

		//NOTE: Without this handler, when Items grid is sorted, the selection stays at list index instead of with the item.
		private void ItemsDataGridView_Sorted(object sender, EventArgs e)
		{
			this.theSelectedItemIsChangingViaMe = true;
			this.theItemBindingSource.Position = this.theItemBindingSource.IndexOf(this.theSelectedItem);
			this.theSelectedItemIsChangingViaMe = false;
		}

		//Private Sub UseInDownloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsDataGridViewUseInDownloadToolStripMenuItem.Click, ItemIdLabelUseInDownloadToolStripMenuItem.Click, ItemIdTextBoxUseInDownloadToolStripMenuItem.Click
		//	Me.UseItemIdInDownload()
		//End Sub
		private void UseInDownloadToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			this.UseItemIdInDownload();
		}

		//Private Sub SearchItemsToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SearchItemsToolStripComboBox.SelectedIndexChanged

		//End Sub

		private void SearchItemsToolStripTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!SearchItemsToolStripTextBox.Multiline && e.KeyChar == (char)Keys.Return)
			{
				try
				{
					//' Cause validation, which means Validating and Validated events are raised.
					//Me.FindForm().Validate()
					//If TypeOf Me.Parent Is ContainerControl Then
					//	CType(Me.Parent, ContainerControl).Validate()
					//End If
					//'NOTE: Prevent annoying beep when textbox is single line.
					//e.Handled = True
					this.SearchItems();
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void SearchItemsToolStripButton_Click(object sender, EventArgs e)
		{
			this.SearchItems();
		}

		private void AddItemButton_Click(object sender, EventArgs e)
		{
			this.AddDraftItem(null);
			this.SelectItemInGrid(this.ItemsDataGridView.Rows.Count - 1);
		}

		private void OwnerLabel_DoubleClick(object sender, EventArgs e)
		{
			this.SwapBetweenOwnerNameAndID();
		}

		private void ToggleWordWrapForDescriptionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.ToggleWordWrapImageOnCheckbox((CheckBox)sender);
			this.ItemDescriptionTextBox.WordWrap = this.ToggleWordWrapForDescriptionCheckBox.Checked;
		}

		private void ToggleWordWrapForChangeNoteCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.ToggleWordWrapImageOnCheckbox((CheckBox)sender);
			this.ItemChangeNoteTextBox.WordWrap = this.ToggleWordWrapForChangeNoteCheckBox.Checked;
		}

		private void BrowseContentPathFileNameButton_Click(object sender, EventArgs e)
		{
			this.BrowseForContentPathFolderOrFileName();
		}

		private void BrowsePreviewImageButton_Click(object sender, EventArgs e)
		{
			this.BrowseForPreviewImage();
		}

		private void SaveAsTemplateOrDraftItemButton_Click(object sender, EventArgs e)
		{
			if (SaveAsTemplateOrDraftItemButton.Text == "Save as Template")
			{
				this.SaveItemAsTemplate();
			}
			else
			{
				this.AddDraftItem(this.theSelectedItem);
				if (this.theSelectedItem.IsTemplate && this.theSelectedItem.IsChanged)
				{
					this.RevertChangedTemplate();
				}
				this.SelectItemInGrid(this.ItemsDataGridView.Rows.Count - 1);
			}
		}

		private void RefreshOrRevertButton_Click(object sender, EventArgs e)
		{
			this.RefreshOrRevertItem();
		}

		private void OpenWorkshopPageButton_Click(object sender, EventArgs e)
		{
			this.OpenWorkshopPage();
		}

		private void SaveTemplateButton_Click(object sender, EventArgs e)
		{
			this.SaveTemplate();
		}

		private void DeleteItemButton_Click(object sender, EventArgs e)
		{
			this.DeleteItem();
		}

		//NOTE: There is no automatic data-binding with TagsWidget, so manually bind from widget to object here.
		private void TagsWidget_TagsPropertyChanged(object sender, EventArgs e)
		{
			this.theSelectedItem.Tags = this.theTagsWidget.ItemTags;
		}

		private void PublishItemButton_Click(object sender, EventArgs e)
		{
			this.PublishItem();
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "PublishGameSelectedIndex")
			{
				MainCROWBAR.TheApp.Settings.PublishSearchField = AppEnums.PublishSearchFieldOptions.ID;
				MainCROWBAR.TheApp.Settings.PublishSearchText = "";

				this.UpdateSteamAppWidgets();
			}
		}

		private void WorkshopItem_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (this.theSelectedItemDetailsIsChangingViaMe)
			{
				return;
			}

			if (e.PropertyName == "ID")
			{
				this.theWorkshopPageLink = AppConstants.WorkshopLinkStart + this.theSelectedItem.ID;
			}
			else if (e.PropertyName == "Title")
			{
				this.UpdateItemTitleLabel();
				this.UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "Description")
			{
				this.UpdateItemDescriptionLabel();
				this.UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "ChangeNote")
			{
				this.UpdateItemChangeNoteLabel();
				this.UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "ContentSize")
			{
				this.UpdateItemContentLabel();
			}
			else if (e.PropertyName == "ContentPathFolderOrFileName")
			{
				this.UpdateItemContentLabel();
				this.UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "PreviewImageSize")
			{
				this.UpdateItemPreviewImageLabel();
				this.UpdateItemPreviewImageBox();
			}
			else if (e.PropertyName == "PreviewImagePathFileName")
			{
				this.UpdateItemPreviewImageLabel();
				this.UpdateItemPreviewImageBox();
				this.UpdateItemChangedStatus();
				//NOTE: Using this property raises an exception, possibly because the DataGridView gets confused by the property being a list, so use "TagsAsTextLine" property.
				//ElseIf e.PropertyName = "Tags" Then
			}
			else if (e.PropertyName == "TagsAsTextLine")
			{
				this.UpdateItemTagsLabel();
				this.UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "Visibility")
			{
				this.UpdateItemVisibilityLabel();
				this.UpdateItemChangedStatus();
			}

			if (this.theSelectedItem.IsDraft)
			{
				this.theSelectedItem.Updated = MathModule.DateTimeToUnixTimeStamp(DateTime.Now);
			}
		}

		private void GetPublishedItems_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				this.LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 1)
			{
				this.theExpectedPublishedItemCount = Convert.ToUInt32(e.UserState);
			}
			else if (e.ProgressPercentage == 2)
			{
				WorkshopItem publishedItem = (WorkshopItem)e.UserState;

				bool itemHasBeenFound = false;
				foreach (WorkshopItem item in this.theDisplayedItems)
				{
					if (item.ID == publishedItem.ID)
					{
						itemHasBeenFound = true;
						break;
					}
				}
				if (!itemHasBeenFound)
				{
					this.theDisplayedItems.Add(publishedItem);
				}

				itemHasBeenFound = false;
				foreach (WorkshopItem item in this.theEntireListOfItems)
				{
					if (item.ID == publishedItem.ID)
					{
						itemHasBeenFound = true;
						break;
					}
				}
				if (!itemHasBeenFound)
				{
					this.theEntireListOfItems.Add(publishedItem);
				}
				//ElseIf e.ProgressPercentage = 3 Then
				//	Dim availableBytes As ULong = CULng(e.UserState)
				//	Me.SteamCloudSizeLabel.Text = "(" + availableBytes.ToString("N") + " used /"
				//ElseIf e.ProgressPercentage = 4 Then
				//	Dim totalBytes As ULong = CULng(e.UserState)
				//	Me.SteamCloudSizeLabel.Text += totalBytes.ToString("N") + " total)"
			}

			this.UpdateItemListWidgets(true);
		}

		private void GetPublishedItems_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				int debug = 4242;
			}
			else
			{
				this.UpdateItemListWidgets(false);
			}
			this.theSelectedGameIsStillUpdatingInterface = false;
		}

		private void GetPublishedItemDetails_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				this.LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 1)
			{
				if (this.ItemPreviewImagePictureBox.Image != null)
				{
					this.ItemPreviewImagePictureBox.Image.Dispose();
				}
				this.ItemPreviewImagePictureBox.Image = (Image)e.UserState;
				//Application.DoEvents()
				this.ItemPreviewImagePictureBox.Refresh();
			}
		}

		private void GetPublishedItemDetails_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				int debug = 4242;
			}
			else
			{
				BackgroundSteamPipe.GetPublishedFileDetailsOutputInfo output = (BackgroundSteamPipe.GetPublishedFileDetailsOutputInfo)e.Result;
				WorkshopItem publishedItem = output.PublishedItem;
				if (output.Action == "Updated")
				{
					if (publishedItem.ID != "0" && publishedItem.ID == this.theSelectedItem.ID)
					{
						this.theSelectedItem.Updated = publishedItem.Updated;
					}
				}
				else
				{
					if (publishedItem.ID != "0")
					{
						if (publishedItem.ID == this.theSelectedItem.ID)
						{
							string previewImagePathFileName = this.theSelectedItem.PreviewImagePathFileName;

							this.theSelectedItem.CreatorAppID = publishedItem.CreatorAppID;
							this.theSelectedItem.ID = publishedItem.ID;
							this.theSelectedItem.OwnerID = publishedItem.OwnerID;
							this.theSelectedItem.OwnerName = publishedItem.OwnerName;
							this.theSelectedItem.Posted = publishedItem.Posted;
							this.theSelectedItem.Updated = publishedItem.Updated;
							this.theSelectedItem.Title = publishedItem.Title;
							this.theSelectedItem.Description = publishedItem.Description;
							this.theSelectedItem.ContentSize = publishedItem.ContentSize;
							if (this.theSteamAppInfo.UsesSteamUGC && string.IsNullOrEmpty(publishedItem.ContentPathFolderOrFileName))
							{
								this.theSelectedItem.ContentPathFolderOrFileName = "Folder_" + publishedItem.ID;
							}
							else
							{
								this.theSelectedItem.ContentPathFolderOrFileName = publishedItem.ContentPathFolderOrFileName;
							}
							this.theSelectedItem.PreviewImageSize = publishedItem.PreviewImageSize;
							this.theSelectedItem.PreviewImagePathFileName = publishedItem.PreviewImagePathFileName;
							this.theSelectedItem.Visibility = publishedItem.Visibility;
							this.theSelectedItem.TagsAsTextLine = publishedItem.TagsAsTextLine;

							this.theSelectedItem.IsChanged = false;
							this.DeleteTempPreviewImageFile(previewImagePathFileName, this.theSelectedItem.ID);
							//Me.UpdateItemDetailWidgets()
						}
						else
						{
							//NOTE: This is an item from SearchItemIDs().
							if (output.Action == "FindAll")
							{
								this.theDisplayedItems.Add(publishedItem);
								this.theEntireListOfItems.Add(publishedItem);
								this.theSelectedItemIsChangingViaMe = true;
								this.SelectItemInGrid(this.ItemsDataGridView.Rows.Count - 1);
								this.theSelectedItemIsChangingViaMe = false;
							}
						}
					}
				}

				//Me.theSelectedItemIsChangingViaMe = False
			}

			//Me.theSelectedItemIsChangingViaMe = False
			this.theSelectedItemDetailsIsChangingViaMe = false;

			this.AppIdComboBox.Enabled = true;
			this.ItemsDataGridView.Enabled = true;
			this.ItemTitleTextBox.Enabled = true;
			this.ItemDescriptionTextBox.Enabled = true;
			this.ItemChangeNoteTextBox.Enabled = true;
			this.ItemContentPathFileNameTextBox.Enabled = true;
			this.ItemPreviewImagePathFileNameTextBox.Enabled = true;
			this.ItemTagsGroupBox.Enabled = true;
			this.UpdateItemDetailWidgets();
		}

		private void DeletePublishedItemFromWorkshop_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			int debug = 4242;
		}

		private void DeletePublishedItemFromWorkshop_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				int debug = 4242;
			}
			else
			{
				string result = (e.Result == null ? null : Convert.ToString(e.Result));
				if (result == "success")
				{
					this.LogTextBox.AppendText("Delete of published item succeeded." + "\r\n");
					if (this.theExpectedPublishedItemCount > 0)
					{
						this.theExpectedPublishedItemCount -= 1U;
					}
					else
					{
						//TODO: When testing, somehow got to here.
						int debug = 4242;
					}
					this.UpdateAfterDeleteItem();
				}
				else
				{
					this.LogTextBox.AppendText("ERROR: " + result + "\r\n");
					this.UpdateItemDetailButtons();
				}
			}
			this.AppIdComboBox.Enabled = true;
			this.ItemsDataGridView.Enabled = true;
			this.ItemTitleTextBox.Enabled = true;
			this.ItemDescriptionTextBox.Enabled = true;
			this.ItemChangeNoteTextBox.Enabled = true;
			this.ItemContentPathFileNameTextBox.Enabled = true;
			this.ItemPreviewImagePathFileNameTextBox.Enabled = true;
			this.ItemTagsGroupBox.Enabled = true;
		}

		private void PublishItem_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				this.LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 1)
			{
				this.LogTextBox.AppendText("\t" + (e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 2)
			{
				BackgroundSteamPipe.PublishItemProgressInfo outputInfo = (BackgroundSteamPipe.PublishItemProgressInfo)e.UserState;
				//TODO: Change to using a progressbar.
				if (outputInfo.TotalUploadedByteCount > 0)
				{
					int progressPercentage = Convert.ToInt32(outputInfo.UploadedByteCount * 100 / outputInfo.TotalUploadedByteCount);
					this.LogTextBox.AppendText("\t" + "\t" + outputInfo.Status + ": " + outputInfo.UploadedByteCount.ToString("N0") + " / " + outputInfo.TotalUploadedByteCount.ToString("N0") + "   " + progressPercentage.ToString() + " %" + "\r\n");
				}
				else
				{
					this.LogTextBox.AppendText("\t" + outputInfo.Status + "." + "\r\n");
				}
			}
		}

		private void PublishItem_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			bool agreementWindowShouldBeShown = false;

			if (e.Cancelled)
			{
				int debug = 4242;
			}
			else
			{
				BackgroundSteamPipe.PublishItemOutputInfo outputInfo = (BackgroundSteamPipe.PublishItemOutputInfo)e.Result;
				string result = outputInfo.Result;
				string publishedItemID = outputInfo.PublishedItemID;

				if (result == "Succeeded")
				{
					if (this.theSelectedItem.IsDraft)
					{
						this.ChangeDraftItemIntoPublishedItem(this.theSelectedItem);
						this.theSelectedItem.ID = publishedItemID;
					}
					else
					{
						this.ChangeChangedItemIntoPublishedItem(this.theSelectedItem);
					}

					this.theSelectedItem.OwnerID = outputInfo.PublishedItemOwnerID;
					this.theSelectedItem.OwnerName = outputInfo.PublishedItemOwnerName;
					this.theSelectedItem.Posted = outputInfo.PublishedItemPosted;
					this.theSelectedItem.Updated = outputInfo.PublishedItemUpdated;
					this.theSelectedItem.ChangeNote = "";

					if (outputInfo.SteamAgreementStatus == "NotAccepted")
					{
						agreementWindowShouldBeShown = true;
					}

					this.DeleteInUseTempPreviewImageFile(this.theSelectedItem.PreviewImagePathFileName, this.theSelectedItem.ID);
					this.theSelectedItem.PreviewImagePathFileName = Path.GetFileName(this.theSelectedItem.PreviewImagePathFileName);
					this.theSelectedItem.PreviewImagePathFileNameIsChanged = false;
				}
				else if (result == "FailedContentAndChangeNote")
				{
					//NOTE: Content file and change note were not updated. Keep their changed status.
					if (this.theSelectedItem.IsDraft)
					{
						bool contentPathFolderOrFileNameIsChanged = this.theSelectedItem.ContentPathFolderOrFileNameIsChanged;
						bool changeNoteIsChanged = this.theSelectedItem.ChangeNoteIsChanged;

						this.ChangeDraftItemIntoPublishedItem(this.theSelectedItem);

						this.theSelectedItem.ContentPathFolderOrFileNameIsChanged = contentPathFolderOrFileNameIsChanged;
						this.theSelectedItem.ChangeNoteIsChanged = changeNoteIsChanged;
						this.theSelectedItem.IsChanged = contentPathFolderOrFileNameIsChanged || changeNoteIsChanged;
					}

					this.theSelectedItem.TitleIsChanged = false;
					this.theSelectedItem.DescriptionIsChanged = false;
					this.theSelectedItem.PreviewImagePathFileNameIsChanged = false;
					this.theSelectedItem.VisibilityIsChanged = false;
					this.theSelectedItem.TagsIsChanged = false;
				}
			}

			//Me.AppIdComboBox.Enabled = True
			//Me.ItemsDataGridView.Enabled = True
			//Me.ItemTitleTextBox.Enabled = True
			//Me.ItemDescriptionTextBox.Enabled = True
			//Me.ItemChangeNoteTextBox.Enabled = True
			//Me.ItemContentPathFileNameTextBox.Enabled = True
			//Me.ItemPreviewImagePathFileNameTextBox.Enabled = True
			//Me.ItemTagsGroupBox.Enabled = True
			//Me.UpdateItemDetailWidgets()
			this.UpdateWidgetsAfterPublish();

			this.GetUserSteamAppCloudQuota();

			this.theSelectedItemDetailsIsChangingViaMe = false;

			if (agreementWindowShouldBeShown)
			{
				this.OpenAgreementRequiresAcceptanceWindow();
			}
		}

#endregion

#region Private Methods

		private void UpdateSteamAppWidgets()
		{
			//NOTE: If this has not been created, then app is in not far enough in Init() and not ready for update.
			if (this.theEntireListOfItems == null || this.theSelectedGameIsStillUpdatingInterface)
			{
				return;
			}
			this.theSelectedGameIsStillUpdatingInterface = true;

			if (!string.IsNullOrEmpty(this.LogTextBox.Text))
			{
				this.LogTextBox.AppendText("------" + "\r\n");
			}

			this.theSteamAppInfo = MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex];
			this.theSteamAppId = this.theSteamAppInfo.ID;
			MainCROWBAR.TheApp.WriteSteamAppIdFile(this.theSteamAppId.m_AppId);

			this.theSteamAppUserInfo = null;
			try
			{
				if (MainCROWBAR.TheApp.Settings.PublishSteamAppUserInfos.Count > 0)
				{
					//NOTE: Using FirstOrDefault() instead of First() to avoid an exception when no item is found.
					this.theSteamAppUserInfo = MainCROWBAR.TheApp.Settings.PublishSteamAppUserInfos.FirstOrDefault((info) => info.AppID == (int)this.theSteamAppId.m_AppId);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			if (this.theSteamAppUserInfo == null)
			{
				//NOTE: Value was not found, so set to new info.
				this.theSteamAppUserInfo = new SteamAppUserInfo(this.theSteamAppId.m_AppId);
				MainCROWBAR.TheApp.Settings.PublishSteamAppUserInfos.Add(this.theSteamAppUserInfo);
			}

			//NOTE: Swap the Tags widget before selecting an item so when item is selected tags will set correctly.
			this.SwapSteamAppTagsWidget();

			int selectedRowIndex = 0;
			this.theDisplayedItems.Clear();
			this.theEntireListOfItems.Clear();
			//Me.theTemplateItemTotalCount = 0
			//Me.theChangedItemTotalCount = 0
			if (this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count == 0)
			{
				this.AddDraftItem(null);
			}
			else
			{
				WorkshopItem draftItem = null;
				long mostRecentlyUpdatedDraftItemDateTime = 0;
				for (int draftItemIndex = 0; draftItemIndex < this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count; draftItemIndex++)
				{
					draftItem = this.theSteamAppUserInfo.DraftTemplateAndChangedItems[draftItemIndex];
					if (draftItem.IsTemplate)
					{
						//Me.theTemplateItemTotalCount += 1UI
					}
					else if (draftItem.IsPublished)
					{
						draftItem.IsChanged = true;
						//Me.theChangedItemTotalCount += 1UI
					}
					if (mostRecentlyUpdatedDraftItemDateTime < draftItem.Updated)
					{
						mostRecentlyUpdatedDraftItemDateTime = draftItem.Updated;
						selectedRowIndex = draftItemIndex;
					}
					this.theDisplayedItems.Add(draftItem);
					this.theEntireListOfItems.Add(draftItem);
				}
			}
			this.SelectItemInGrid(selectedRowIndex);

			this.GetUserSteamAppCloudQuota();

			this.theBackgroundSteamPipe.GetPublishedItems(this.GetPublishedItems_ProgressChanged, this.GetPublishedItems_RunWorkerCompleted, this.theSteamAppId.ToString());
		}

		private void SwapSteamAppTagsWidget()
		{
			if (theTagsWidget != null)
			{
				this.theTagsWidget.TagsPropertyChanged -= this.TagsWidget_TagsPropertyChanged;
			}

			//Me.theTagsWidget = CType(Me.AppIdComboBox.SelectedItem, SteamAppInfo).TagsWidget
			//Dim info As SteamAppInfo = CType(Me.AppIdComboBox.SelectedItem, SteamAppInfo)
			SteamAppInfoBase info = MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex];
			Type t = info.TagsControlType;
			this.theTagsWidget = (Base_TagsUserControl)(t.GetConstructor(new System.Type[0]).Invoke(new object[0]));
			if (this.ItemTagsGroupBox.Controls.Count > 0)
			{
				this.ItemTagsGroupBox.Controls.RemoveAt(0);
			}
			this.ItemTagsGroupBox.Controls.Add(this.theTagsWidget);
			this.theTagsWidget.AutoScroll = true;
			this.theTagsWidget.Dock = System.Windows.Forms.DockStyle.Fill;
			//Me.theTagsWidget.ItemTags = CType(Resources.GetObject("ContagionTagsUserControl1.ItemTags"), System.Collections.Generic.List(Of String))
			this.theTagsWidget.Location = new System.Drawing.Point(3, 17);
			this.theTagsWidget.Name = "TagsUserControl";
			this.theTagsWidget.Size = new System.Drawing.Size(193, 307);
			this.theTagsWidget.TabIndex = 0;

			this.theTagsWidget.TagsPropertyChanged += this.TagsWidget_TagsPropertyChanged;
		}

		private void SelectItemInGrid()
		{
			int selectedRowIndex = 0;
			if (this.ItemsDataGridView.SelectedRows.Count > 0)
			{
				selectedRowIndex = this.ItemsDataGridView.SelectedRows[0].Index;
			}
			else
			{
				selectedRowIndex = this.ItemsDataGridView.Rows.Count - 1;
			}
			this.SelectItemInGrid(selectedRowIndex);
		}

		private void SelectItemInGrid(int selectedRowIndex)
		{
			if (selectedRowIndex >= this.ItemsDataGridView.Rows.Count)
			{
				selectedRowIndex = this.ItemsDataGridView.Rows.Count - 1;
			}
			//NOTE: This line does not update the widgets connected to the list fields.
			this.ItemsDataGridView.Rows[selectedRowIndex].Selected = true;
			//NOTE: This line is required so that the item detail widgets update when the gird selection is changed programmatically.
			this.ItemsDataGridView.CurrentCell = this.ItemsDataGridView.Rows[selectedRowIndex].Cells[0];
			this.ItemsDataGridView.FirstDisplayedScrollingRowIndex = selectedRowIndex;
		}

		private void UseItemIdInDownload()
		{
			MainCROWBAR.TheApp.Settings.DownloadItemIdOrLink = this.theSelectedItem.ID;
		}

		private void SearchItems()
		{
			if (string.IsNullOrEmpty(this.SearchItemsToolStripTextBox.Text))
			{
				this.ClearSearch();
			}
			else
			{
				if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.ID)
				{
					this.SearchItemIDs();
				}
				else if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.Owner)
				{
					this.SearchItemOwnerNames();
				}
				else if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.Title)
				{
					this.SearchItemTitles();
				}
				else if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.Description)
				{
					this.SearchItemDescriptions();
				}
				else if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.AllFields)
				{
					this.SearchItemAllFields();
				}
			}
			this.UpdateItemListWidgets(false);
		}

		private void ClearSearch()
		{
			this.theDisplayedItems.Clear();
			foreach (WorkshopItem item in this.theEntireListOfItems)
			{
				this.theDisplayedItems.Add(item);
			}
		}

		private void SearchItemIDs()
		{
			string itemTextToFind = this.SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			this.theDisplayedItems.Clear();
			foreach (WorkshopItem item in this.theEntireListOfItems)
			{
				if (item.ID.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					this.theDisplayedItems.Add(item);
				}
			}

			if (!itemHasBeenFound)
			{
				try
				{
					System.UInt64 tempVar = 0;
					if (ulong.TryParse(itemTextToFind, out tempVar))
					{
						GetPublishedItemDetailsViaSteamRemoteStorage(itemTextToFind, "FindAll");
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void SearchItemOwnerNames()
		{
			string itemTextToFind = this.SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			this.theDisplayedItems.Clear();
			foreach (WorkshopItem item in this.theEntireListOfItems)
			{
				if (item.OwnerName.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					this.theDisplayedItems.Add(item);
				}
			}
		}

		private void SearchItemTitles()
		{
			string itemTextToFind = this.SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			this.theDisplayedItems.Clear();
			foreach (WorkshopItem item in this.theEntireListOfItems)
			{
				if (item.Title.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					this.theDisplayedItems.Add(item);
				}
			}
		}

		private void SearchItemDescriptions()
		{
			string itemTextToFind = this.SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			this.theDisplayedItems.Clear();
			foreach (WorkshopItem item in this.theEntireListOfItems)
			{
				if (item.Description.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					this.theDisplayedItems.Add(item);
				}
			}
		}

		private void SearchItemAllFields()
		{
			string itemTextToFind = this.SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			this.theDisplayedItems.Clear();
			foreach (WorkshopItem item in this.theEntireListOfItems)
			{
				if (item.ID.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					this.theDisplayedItems.Add(item);
				}
				else if (item.OwnerName.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					this.theDisplayedItems.Add(item);
				}
				else if (item.Title.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					this.theDisplayedItems.Add(item);
				}
				else if (item.Description.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					this.theDisplayedItems.Add(item);
				}
			}
		}

		private void AddDraftItem(WorkshopItem itemToCopy)
		{
			WorkshopItem draftItem = null;
			if (itemToCopy == null)
			{
				draftItem = new WorkshopItem();
			}
			else
			{
				draftItem = (WorkshopItem)itemToCopy.Clone();
				draftItem.SetAllChangedForNonEmptyFields();
			}
			this.theDisplayedItems.Add(draftItem);
			this.theEntireListOfItems.Add(draftItem);
			this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(draftItem);
			this.UpdateItemListWidgets(false);
		}

		private void ChangeDraftItemIntoPublishedItem(WorkshopItem item)
		{
			this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(item);
			item.IsChanged = false;
			this.UpdateItemListWidgets(false);
		}

		private void ChangePublishedItemIntoChangedItem(WorkshopItem item)
		{
			this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(item);
			//Me.theChangedItemTotalCount += 1UI
			this.UpdateItemListWidgets(false);
			this.SaveCopyOfPreviewImageFile(item);
		}

		private void ChangeChangedItemIntoPublishedItem(WorkshopItem item)
		{
			this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(item);
			//Me.theChangedItemTotalCount -= 1UI
			item.ChangeNote = "";
			item.IsChanged = false;
			this.UpdateItemListWidgets(false);
		}

		private void OpenSteamSubscriberAgreement()
		{
			System.Diagnostics.Process.Start(Properties.Resources.Link_SteamSubscriberAgreement);
		}

		private void UpdateItemListWidgets(bool isProgress)
		{
			if (!isProgress)
			{
				// If the DataGridView is not currently sorted, then sortedColumn is Nothing.
				DataGridViewColumn sortedColumn = this.ItemsDataGridView.SortedColumn;
				if (sortedColumn != null)
				{
					ListSortDirection direction = 0;
					if (this.ItemsDataGridView.SortOrder == SortOrder.Ascending)
					{
						direction = ListSortDirection.Ascending;
					}
					else
					{
						direction = ListSortDirection.Descending;
					}
					this.ItemsDataGridView.Sort(sortedColumn, direction);
				}
			}

			//Dim draftItemsDisplayedCount As UInteger = CUInt(Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count - Me.theTemplateItemDisplayedCount - Me.theChangedItemDisplayedCount)
			//Dim publishedItemsDisplayedCount As UInteger = CUInt(Me.theDisplayedItems.Count - Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count)
			//Dim draftItemsTotalCount As UInteger = CUInt(Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count - Me.theTemplateItemTotalCount - Me.theChangedItemTotalCount)
			//Dim publishedItemsTotalCount As UInteger = CUInt(Me.theEntireListOfItems.Count - Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count)
			uint draftItemsDisplayedCount = this.theDisplayedItems.DraftItemCount;
			uint publishedItemsDisplayedCount = this.theDisplayedItems.PublishedItemCount;
			uint draftItemsTotalCount = this.theEntireListOfItems.DraftItemCount;
			uint publishedItemsTotalCount = this.theEntireListOfItems.PublishedItemCount;

			this.ItemCountsToolStripLabel.Text = "";
			if (this.theDisplayedItems.Count != this.theEntireListOfItems.Count)
			{
				this.ItemCountsToolStripLabel.Text += draftItemsDisplayedCount.ToString() + "/";
			}
			this.ItemCountsToolStripLabel.Text += draftItemsTotalCount.ToString() + " draft + ";
			if (this.theDisplayedItems.Count != this.theEntireListOfItems.Count)
			{
				//Me.ItemCountsToolStripLabel.Text += Me.theTemplateItemDisplayedCount.ToString() + "/"
				this.ItemCountsToolStripLabel.Text += this.theDisplayedItems.TemplateItemCount.ToString() + "/";
			}
			//Me.ItemCountsToolStripLabel.Text += Me.theTemplateItemTotalCount.ToString() + " template + "
			this.ItemCountsToolStripLabel.Text += this.theEntireListOfItems.TemplateItemCount.ToString() + " template + ";
			if (this.theDisplayedItems.Count != this.theEntireListOfItems.Count)
			{
				//Me.ItemCountsToolStripLabel.Text += Me.theChangedItemDisplayedCount.ToString() + "/"
				this.ItemCountsToolStripLabel.Text += this.theDisplayedItems.ChangedItemCount.ToString() + "/";
			}
			//Me.ItemCountsToolStripLabel.Text += Me.theChangedItemTotalCount.ToString() + " changed + "
			this.ItemCountsToolStripLabel.Text += this.theEntireListOfItems.ChangedItemCount.ToString() + " changed + ";
			if (this.theDisplayedItems.Count != this.theEntireListOfItems.Count)
			{
				this.ItemCountsToolStripLabel.Text += publishedItemsDisplayedCount.ToString() + "/";
			}
			this.ItemCountsToolStripLabel.Text += publishedItemsTotalCount.ToString() + " published";
			if (isProgress)
			{
				uint remainingPublishedItemsCount = this.theExpectedPublishedItemCount - publishedItemsTotalCount;
				this.ItemCountsToolStripLabel.Text += " (" + remainingPublishedItemsCount.ToString() + " more to get)";
			}
			else
			{
				//If (publishedItemsTotalCount + Me.theChangedItemTotalCount) <> Me.theExpectedPublishedItemCount Then
				if ((publishedItemsTotalCount + this.theEntireListOfItems.ChangedItemCount) != this.theExpectedPublishedItemCount)
				{
					this.ItemCountsToolStripLabel.Text += " (" + this.theExpectedPublishedItemCount.ToString() + " expected)";
				}
			}
			this.ItemCountsToolStripLabel.Text += " = ";
			if (this.theDisplayedItems.Count != this.theEntireListOfItems.Count)
			{
				this.ItemCountsToolStripLabel.Text += this.theDisplayedItems.Count.ToString() + "/";
			}
			this.ItemCountsToolStripLabel.Text += this.theEntireListOfItems.Count.ToString() + " total";
		}

		private void UpdateItemDetails()
		{
			if (this.theSelectedItem != null)
			{
				this.theSelectedItem.PropertyChanged -= this.WorkshopItem_PropertyChanged;
			}
			this.theSelectedItem = this.theDisplayedItems[this.ItemsDataGridView.SelectedRows[0].Index];
			this.theSelectedItem.PropertyChanged += this.WorkshopItem_PropertyChanged;

			if (this.theSelectedItem.IsDraft)
			{
				this.UpdateItemDetailWidgets();
			}
			else if (this.theSelectedItem.IsTemplate)
			{
				this.theUnchangedSelectedTemplateItem = (WorkshopItem)this.theSelectedItem.Clone();
				this.theUnchangedSelectedTemplateItem.IsTemplate = true;
				this.UpdateItemDetailWidgets();
			}
			else if (this.theSelectedItem.IsChanged)
			{
				this.UpdateItemDetailWidgets();
			}
			else
			{
				//NOTE: UpdateItemDetailWidgets() will be called from the 'bw_completed' handler.
				this.GetPublishedItemDetailsViaSteamRemoteStorage(this.theSelectedItem.ID, "All");
				return;
			}
		}

		private void UpdateItemDetailWidgets()
		{
			if (this.theUserSteamID == 0)
			{
				this.GetUserSteamID();
			}

			//INSTANT C# TODO TASK: The following line could not be converted:
			//Dim editableTextBoxesAreReadOnly As Boolean = (this.theSelectedItem.IsPublished) AndAlso (this.theSelectedItem.OwnerID < > this.theUserSteamID)
			bool editableTextBoxesAreReadOnly = theSelectedItem.IsPublished && theSelectedItem.OwnerID != theUserSteamID;
			bool editableNonTextWidgetsAreEnabled = (this.theSelectedItem.IsDraft) || (this.theSelectedItem.IsTemplate) || (this.theSelectedItem.OwnerID == this.theUserSteamID);

			this.ItemGroupBox.Enabled = true;
			this.UpdateItemGroupBoxLabel();

			this.UpdateItemTitleLabel();
			this.ItemTitleTextBox.ReadOnly = editableTextBoxesAreReadOnly;

			this.UpdateItemDescriptionLabel();
			this.ItemDescriptionTextBox.ReadOnly = editableTextBoxesAreReadOnly;

			this.UpdateItemChangeNoteLabel();
			this.ItemChangeNoteTextBox.ReadOnly = editableTextBoxesAreReadOnly;

			this.UpdateItemContentLabel();
			this.ItemContentPathFileNameTextBox.ReadOnly = editableTextBoxesAreReadOnly;

			this.UpdateItemPreviewImageLabel();
			this.ItemPreviewImagePathFileNameTextBox.ReadOnly = editableTextBoxesAreReadOnly;
			if (!this.theSelectedItem.IsPublished || this.theSelectedItem.IsChanged)
			{
				this.UpdateItemPreviewImageBox();
			}

			this.UpdateItemVisibilityLabel();
			this.ItemVisibilityComboBox.Enabled = editableNonTextWidgetsAreEnabled;

			this.ItemTagsGroupBox.Enabled = true;
			//NOTE: There is no automatic data-binding with TagsWidget, so manually bind from object to widget here.
			this.theTagsWidget.ItemTags = this.theSelectedItem.Tags;
			this.UpdateItemTagsLabel();
			this.theTagsWidget.Enabled = editableNonTextWidgetsAreEnabled;

			this.theWorkshopPageLink = AppConstants.WorkshopLinkStart + this.theSelectedItem.ID;

			this.UpdateItemDetailButtons();
		}

		private void UpdateItemChangedStatus()
		{
			if (!this.theSelectedItem.IsChanged)
			{
				if (this.theSelectedItem.IsTemplate)
				{
					this.theSelectedItem.IsChanged = true;
					this.UpdateItemGroupBoxLabel();
				}
				else if (this.theSelectedItem.IsPublished)
				{
					this.theSelectedItem.IsChanged = true;
					this.ChangePublishedItemIntoChangedItem(this.theSelectedItem);
					this.UpdateItemGroupBoxLabel();
				}
			}
			this.theSelectedItem.Updated = MathModule.DateTimeToUnixTimeStamp(DateTime.Now);
			this.UpdateItemDetailButtons();
		}

		private void UpdateItemGroupBoxLabel()
		{
			string changedMarker = "";
			if (this.theSelectedItem.IsChanged && !this.theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			this.ItemGroupBox.Text = "Item" + changedMarker;
		}

		private void UpdateItemTitleLabel()
		{
			int titleSize = this.theSelectedItem.Title.Length;
			int titleSizeMax = (int)Steamworks.Constants.k_cchPublishedDocumentTitleMax;
			string changedMarker = "";
			if (this.theSelectedItem.TitleIsChanged && !this.theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			this.ItemTitleLabel.Text = "Title" + changedMarker + " (" + titleSize.ToString() + " / " + titleSizeMax.ToString() + " characters max):";
		}

		private void UpdateItemDescriptionLabel()
		{
			int descriptionSize = this.theSelectedItem.Description.Length;
			int descriptionSizeMax = (int)Steamworks.Constants.k_cchPublishedDocumentDescriptionMax;
			string changedMarker = "";
			if (this.theSelectedItem.DescriptionIsChanged && !this.theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			this.ItemDescriptionLabel.Text = "Description" + changedMarker + " (" + descriptionSize.ToString() + " / " + descriptionSizeMax.ToString() + " characters max):";
		}

		private void UpdateItemChangeNoteLabel()
		{
			int changeNoteSize = this.theSelectedItem.ChangeNote.Length;
			int changeNoteSizeMax = (int)Steamworks.Constants.k_cchPublishedDocumentChangeDescriptionMax;
			string changedMarker = "";
			if (this.theSelectedItem.ChangeNoteIsChanged && !this.theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			this.ItemChangeNoteLabel.Text = "Change Note" + changedMarker + " (" + changeNoteSize.ToString() + " / " + changeNoteSizeMax.ToString() + " characters max):";
		}

		private void UpdateItemContentLabel()
		{
			string contentFileSizeText = "0";
			if (MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex].CanUseContentFolderOrFile)
			{
				this.ItemContentFolderOrFileLabel.Text = "Content Folder or File";
				if (Directory.Exists(this.theSelectedItem.ContentPathFolderOrFileName))
				{
					ulong folderSize = FileManager.GetFolderSize(this.theSelectedItem.ContentPathFolderOrFileName);
					contentFileSizeText = MathModule.ByteUnitsConversion(folderSize);
				}
				else if (File.Exists(this.theSelectedItem.ContentPathFolderOrFileName))
				{
					FileInfo aFile = new FileInfo(this.theSelectedItem.ContentPathFolderOrFileName);
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)aFile.Length);
				}
				else if (this.theSelectedItem.ContentSize > 0 && this.theSelectedItem.IsPublished)
				{
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)this.theSelectedItem.ContentSize);
				}
			}
			else if (MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex].UsesSteamUGC)
			{
				this.ItemContentFolderOrFileLabel.Text = "Content Folder";
				if (Directory.Exists(this.theSelectedItem.ContentPathFolderOrFileName))
				{
					ulong folderSize = FileManager.GetFolderSize(this.theSelectedItem.ContentPathFolderOrFileName);
					contentFileSizeText = MathModule.ByteUnitsConversion(folderSize);
				}
				else if (this.theSelectedItem.ContentSize > 0 && this.theSelectedItem.IsPublished)
				{
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)this.theSelectedItem.ContentSize);
				}
			}
			else
			{
				this.ItemContentFolderOrFileLabel.Text = "Content File";
				if (File.Exists(this.theSelectedItem.ContentPathFolderOrFileName))
				{
					FileInfo aFile = new FileInfo(this.theSelectedItem.ContentPathFolderOrFileName);
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)aFile.Length);
				}
				else if (this.theSelectedItem.ContentSize > 0 && this.theSelectedItem.IsPublished)
				{
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)this.theSelectedItem.ContentSize);
				}
			}
			//Dim contentFileSizeMaxText As String = "<unknown>"
			//'Dim contentFileSizeMax As Integer = CInt(Steamworks.Constants.k_unMaxCloudFileChunkSize / 1048576)
			//'contentFileSizeMaxText = contentFileSizeMax.ToString()
			string changedMarker = "";
			if (this.theSelectedItem.ContentPathFolderOrFileNameIsChanged && !this.theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			//NOTE: Not sure what max size is, so do not show it.
			//Me.ItemContentFolderOrFileLabel.Text += changedMarker + " (" + contentFileSizeText + " / " + contentFileSizeMaxText + " MB max):"
			this.ItemContentFolderOrFileLabel.Text += changedMarker;
			if (contentFileSizeText != "0")
			{
				this.ItemContentFolderOrFileLabel.Text += " (" + contentFileSizeText + ")";
			}
			this.ItemContentFolderOrFileLabel.Text += ":";
		}

		private void UpdateItemPreviewImageLabel()
		{
			string previewImageSizeText = "0";
			if (File.Exists(this.theSelectedItem.PreviewImagePathFileName))
			{
				FileInfo aFile = new FileInfo(this.theSelectedItem.PreviewImagePathFileName);
				previewImageSizeText = MathModule.ByteUnitsConversion((ulong)aFile.Length);
			}
			else if (this.theSelectedItem.PreviewImageSize > 0 && this.theSelectedItem.IsPublished)
			{
				previewImageSizeText = MathModule.ByteUnitsConversion((ulong)this.theSelectedItem.PreviewImageSize);
			}
			//Dim previewImageSizeMaxText As String = "<unknown>"
			string changedMarker = "";
			if (this.theSelectedItem.PreviewImagePathFileNameIsChanged && !this.theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			//NOTE: Not sure what max size is, so do not show it.
			//Me.ItemPreviewImageLabel.Text = "Preview Image" + changedMarker + " (" + previewImageSizeText + " / " + previewImageSizeMaxText + " MB max):"
			this.ItemPreviewImageLabel.Text = "Preview Image" + changedMarker;
			if (previewImageSizeText != "0")
			{
				this.ItemPreviewImageLabel.Text += " (" + previewImageSizeText + ")";
			}
			this.ItemPreviewImageLabel.Text += ":";
		}

		private void UpdateItemPreviewImageBox()
		{
			if (File.Exists(this.theSelectedItem.PreviewImagePathFileName))
			{
				try
				{
					if (this.ItemPreviewImagePictureBox.Image != null)
					{
						this.ItemPreviewImagePictureBox.Image.Dispose();
					}
					this.ItemPreviewImagePictureBox.Image = Image.FromFile(this.theSelectedItem.PreviewImagePathFileName);
				}
				catch (Exception ex)
				{
					// Problem setting Image, so reset the textbox text.
					this.theSelectedItem.PreviewImagePathFileName = this.theSavedPreviewImagePathFileName;
				}
			}
			else
			{
				if (this.ItemPreviewImagePictureBox.Image != null)
				{
					this.ItemPreviewImagePictureBox.Image.Dispose();
				}
				this.ItemPreviewImagePictureBox.Image = null;
			}
		}

		//NOTE: When item is changed, save the preview image to a file in Crowbar's appdata folder.
		//      Save using file name like "<ID>_preview_00.bmp" [00 for primary].
		//      Need to delete the file when published or reverted.
		private void SaveCopyOfPreviewImageFile(WorkshopItem item)
		{
			if (this.ItemPreviewImagePictureBox.Image == null)
			{
				return;
			}

			System.Drawing.Imaging.ImageFormat anImageFormat = this.ItemPreviewImagePictureBox.Image.RawFormat;
			string previewImagePathFileName = "";
			if (anImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
			{
				previewImagePathFileName = this.GetPreviewImagePathFileName("temp.gif", item.ID, 0);
			}
			else
			{
				previewImagePathFileName = this.GetPreviewImagePathFileName("temp.png", item.ID, 0);
				anImageFormat = System.Drawing.Imaging.ImageFormat.Png;
			}
			if (!string.IsNullOrEmpty(previewImagePathFileName))
			{
				try
				{
					this.ItemPreviewImagePictureBox.Image.Save(previewImagePathFileName, anImageFormat);
				}
				catch (Exception ex)
				{
					if (!File.Exists(previewImagePathFileName))
					{
						this.LogTextBox.AppendText("ERROR: Crowbar tried to save preview image to temp file \"" + previewImagePathFileName + "\" but Windows gave this message: " + ex.Message);
					}
				}
				if (File.Exists(previewImagePathFileName))
				{
					bool selectedItemDetailsIsChangingViaMe = this.theSelectedItemDetailsIsChangingViaMe;
					bool selectedItemPreviewImagePathFileNameIsChanged = this.theSelectedItem.PreviewImagePathFileNameIsChanged;
					this.theSelectedItemDetailsIsChangingViaMe = true;

					this.theSelectedItem.PreviewImagePathFileName = previewImagePathFileName;

					this.theSelectedItemDetailsIsChangingViaMe = selectedItemDetailsIsChangingViaMe;
					this.theSelectedItem.PreviewImagePathFileNameIsChanged = selectedItemPreviewImagePathFileNameIsChanged;
				}
			}
		}

		// Copy the image into memory, so the image file can be deleted.
		private void DeleteInUseTempPreviewImageFile(string itemPreviewImagePathFileName, string itemID)
		{
			Image img = null;

			try
			{
				if (File.Exists(itemPreviewImagePathFileName))
				{
					img = Image.FromFile(itemPreviewImagePathFileName);
					if (this.ItemPreviewImagePictureBox.Image != null)
					{
						this.ItemPreviewImagePictureBox.Image.Dispose();
					}
					this.ItemPreviewImagePictureBox.Image = new Bitmap(img);
					img.Dispose();
				}
			}
			catch (Exception ex)
			{
				if (img != null)
				{
					img.Dispose();
					img = null;
				}
			}

			this.DeleteTempPreviewImageFile(itemPreviewImagePathFileName, itemID);
		}

		private void DeleteTempPreviewImageFile(string itemPreviewImagePathFileName, string itemID)
		{
			string previewImagePathFileName = this.GetPreviewImagePathFileName(itemPreviewImagePathFileName, itemID, 0);
			if (!string.IsNullOrEmpty(previewImagePathFileName) && File.Exists(previewImagePathFileName))
			{
				try
				{
					File.Delete(previewImagePathFileName);
				}
				catch (Exception ex)
				{
					this.LogTextBox.AppendText("ERROR: Crowbar tried to delete an old temp file \"" + previewImagePathFileName + "\" but Windows gave this message: " + ex.Message);
				}
			}
		}

		private string GetPreviewImagePathFileName(string sourcePathFileName, string itemID, int previewIndex)
		{
			string extension = Path.GetExtension(sourcePathFileName);
			string targetFileName = itemID + "_" + previewIndex.ToString("00") + extension;
			string previewsPath = MainCROWBAR.TheApp.GetPreviewsPath();
			string targetPathFileName = null;
			if (!string.IsNullOrEmpty(previewsPath))
			{
				try
				{
					targetPathFileName = Path.Combine(previewsPath, targetFileName);
				}
				catch (Exception ex)
				{
					targetPathFileName = "";
				}
			}
			else
			{
				targetPathFileName = "";
			}

			return targetPathFileName;
		}

		private void UpdateItemVisibilityLabel()
		{
			string changedMarker = "";
			if (this.theSelectedItem.VisibilityIsChanged && !this.theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			this.ItemVisibilityLabel.Text = "Visibility" + changedMarker + ":";
		}

		private void UpdateItemTagsLabel()
		{
			string changedMarker = "";
			if (this.theSelectedItem.TagsIsChanged && !this.theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			this.ItemTagsGroupBox.Text = "Tags" + changedMarker;
		}

		private void UpdateItemDetailButtons()
		{
			if (this.theUserSteamID == 0)
			{
				this.GetUserSteamID();
			}

			bool editableNonTextWidgetsAreEnabled = (this.theSelectedItem.IsDraft) || (this.theSelectedItem.IsTemplate) || (this.theSelectedItem.OwnerID == this.theUserSteamID);

			this.BrowseItemContentPathFileNameButton.Enabled = editableNonTextWidgetsAreEnabled;
			this.BrowseItemPreviewImagePathFileNameButton.Enabled = editableNonTextWidgetsAreEnabled;

			this.SaveAsTemplateOrDraftItemButton.Enabled = true;
			if (this.theSelectedItem.IsTemplate)
			{
				this.SaveAsTemplateOrDraftItemButton.Text = "Save as Draft";
			}
			else
			{
				this.SaveAsTemplateOrDraftItemButton.Text = "Save as Template";
			}

			//Me.RefreshOrRevertItemButton.Visible = True
			//Me.RefreshOrRevertItemButton.Enabled = (Me.theSelectedItem.ID <> "" AndAlso Not Me.theSelectedItem.IsDraft)
			this.RefreshOrRevertItemButton.Enabled = (this.theSelectedItem.IsPublished) || (this.theSelectedItem.IsTemplate && this.theSelectedItem.IsChanged);
			if ((this.theSelectedItem.IsTemplate) || (this.theSelectedItem.IsPublished && this.theSelectedItem.IsChanged))
			{
				this.RefreshOrRevertItemButton.Text = "Revert";
			}
			else
			{
				this.RefreshOrRevertItemButton.Text = "Refresh";
			}

			this.OpenWorkshopPageButton.Visible = (!this.theSelectedItem.IsTemplate);
			this.OpenWorkshopPageButton.Enabled = (!this.theSelectedItem.IsDraft);

			this.SaveTemplateButton.Visible = (this.theSelectedItem.IsTemplate);
			this.SaveTemplateButton.Enabled = (this.theSelectedItem.IsChanged);

			this.DeleteItemButton.Enabled = editableNonTextWidgetsAreEnabled;

			//NOTE: SteamRemoteStorage_PublishWorkshopFile requires Item to have a Title, a Description, a Content File, and a Preview Image.
			this.PublishItemButton.Enabled = (((this.theSelectedItem.IsDraft) && (!string.IsNullOrEmpty(this.theSelectedItem.Title) && !string.IsNullOrEmpty(this.theSelectedItem.Description) && !string.IsNullOrEmpty(this.theSelectedItem.ContentPathFolderOrFileName) && !string.IsNullOrEmpty(this.theSelectedItem.PreviewImagePathFileName))) || (this.theSelectedItem.IsChanged && (this.theUserSteamID == this.theSelectedItem.OwnerID)) || (this.theSelectedItem.IsTemplate));
		}

		private void SwapBetweenOwnerNameAndID()
		{
			if (this.ItemOwnerTextBox.DataBindings["Text"].BindingMemberInfo.BindingMember == "OwnerName")
			{
				this.ItemOwnerTextBox.DataBindings.Remove(this.ItemOwnerTextBox.DataBindings["Text"]);
				this.ItemOwnerTextBox.DataBindings.Add("Text", this.theItemBindingSource, "OwnerID", false, DataSourceUpdateMode.OnValidation);
			}
			else
			{
				this.ItemOwnerTextBox.DataBindings.Remove(this.ItemOwnerTextBox.DataBindings["Text"]);
				this.ItemOwnerTextBox.DataBindings.Add("Text", this.theItemBindingSource, "OwnerName", false, DataSourceUpdateMode.OnValidation);
			}
		}

		private void ToggleWordWrapImageOnCheckbox(CheckBox aCheckBox)
		{
			if (aCheckBox.Checked)
			{
				aCheckBox.BackgroundImage = Properties.Resources.WordWrap;
			}
			else
			{
				aCheckBox.BackgroundImage = Properties.Resources.WordWrapOff;
			}
		}

		private void BrowseForContentPathFolderOrFileName()
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			if (this.theSteamAppInfo.CanUseContentFolderOrFile || this.theSteamAppInfo.UsesSteamUGC)
			{
				if (this.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Count > 0)
				{
					openFileWdw.Title = "Open the folder or the package file you want to upload";
				}
				else
				{
					openFileWdw.Title = "Open the folder you want to upload";
				}
				openFileWdw.FileName = "[Folder Selection]";
			}
			else
			{
				openFileWdw.Title = "Open the file you want to upload";
			}
			if (File.Exists(this.theSelectedItem.ContentPathFolderOrFileName))
			{
				openFileWdw.InitialDirectory = FileManager.GetPath(this.theSelectedItem.ContentPathFolderOrFileName);
			}
			else
			{
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(this.theSelectedItem.ContentPathFolderOrFileName);
				if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
				{
					openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}

			//NOTE: Must use temp string because openFileWdw.Filter validates itself on assignment.
			string fileFilter = "";
			for (int i = 0; i < this.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Count; i++)
			{
				fileFilter += this.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Values[i];
				fileFilter += " (*.";
				fileFilter += this.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Keys[i];
				fileFilter += ")|*.";
				fileFilter += this.theSteamAppInfo.ContentFileExtensionsAndDescriptions.Keys[i];
				fileFilter += "|";
			}
			fileFilter += "All Files (*.*)|*.*";
			openFileWdw.Filter = fileFilter;

			openFileWdw.AddExtension = false;
			openFileWdw.CheckFileExists = false;
			openFileWdw.Multiselect = false;
			openFileWdw.ValidateNames = true;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				if (this.theSteamAppInfo.CanUseContentFolderOrFile || this.theSteamAppInfo.UsesSteamUGC)
				{
					if (Path.GetFileNameWithoutExtension(openFileWdw.FileName) == "[Folder Selection]")
					{
						this.theSelectedItem.ContentPathFolderOrFileName = FileManager.GetPath(openFileWdw.FileName);
					}
					else
					{
						this.theSelectedItem.ContentPathFolderOrFileName = openFileWdw.FileName;
					}
				}
				else
				{
					this.theSelectedItem.ContentPathFolderOrFileName = openFileWdw.FileName;
				}
				this.UpdateItemContentLabel();
			}
		}

		private void BrowseForPreviewImage()
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			openFileWdw.Title = "Open the image file you want to use for preview image";
			if (File.Exists(this.theSelectedItem.PreviewImagePathFileName))
			{
				openFileWdw.InitialDirectory = FileManager.GetPath(this.theSelectedItem.PreviewImagePathFileName);
			}
			else
			{
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(this.theSelectedItem.PreviewImagePathFileName);
				if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
				{
					openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
			openFileWdw.Filter = "Image Files (*.bmp;*.gif;*.jpeg;*.jpg;*.png;*.wmf)|*.bmp;*.gif;*.jpeg;*.jpg;*.png;*.wmf|All Files (*.*)|*.*";
			openFileWdw.AddExtension = true;
			openFileWdw.CheckFileExists = false;
			openFileWdw.Multiselect = false;
			openFileWdw.ValidateNames = true;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				// Save the pathFileName in case the PictureBox.Image does not like the file.
				this.theSavedPreviewImagePathFileName = this.theSelectedItem.PreviewImagePathFileName;
				//NOTE: Changing the file name field also changes the preview picturebox.
				this.theSelectedItem.PreviewImagePathFileName = openFileWdw.FileName;
			}
		}

		//- If draft, then change draft to template.
		//X If template. This should not occur.
		//- If changed or published, then copy item as template, add it to list, and select it in list.
		private void SaveItemAsTemplate()
		{
			WorkshopItem anItem = null;
			if (this.theSelectedItem.IsDraft)
			{
				anItem = this.theSelectedItem;
			}
			else
			{
				anItem = (WorkshopItem)this.theSelectedItem.Clone();
			}

			this.theSelectedItemDetailsIsChangingViaMe = true;
			anItem.IsTemplate = true;
			//anItem.ContentPathFolderOrFileName = ""
			//anItem.PreviewImagePathFileName = ""
			anItem.IsChanged = false;
			//NOTE: Without this line, the ID field in the widgets does not change until a different item is selected.
			this.theItemBindingSource.ResetCurrentItem();
			this.theSelectedItemDetailsIsChangingViaMe = false;
			//Me.theTemplateItemTotalCount += 1UI

			if (anItem != this.theSelectedItem)
			{
				this.theDisplayedItems.Add(anItem);
				this.theEntireListOfItems.Add(anItem);
				this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(anItem);
				this.SaveCopyOfPreviewImageFile(anItem);
				this.SelectItemInGrid(this.ItemsDataGridView.Rows.Count - 1);
			}

			this.UpdateItemListWidgets(false);
			this.UpdateItemDetails();
		}

		private void RefreshOrRevertItem()
		{
			if (this.theSelectedItem.IsChanged)
			{
				if (this.theSelectedItem.IsTemplate)
				{
					//Me.theSelectedItemDetailsIsChangingViaMe = True
					//'NOTE: Change the item in the list (and not the Me.theSelectedItem) so that list and selected item stay synced.
					//Dim selectedItemIndex As Integer = Me.theItemBindingSource.IndexOf(Me.theSelectedItem)
					//Me.theDisplayedItems(selectedItemIndex) = Me.theUnchangedSelectedTemplateItem
					//Me.theSelectedItemDetailsIsChangingViaMe = False
					this.RevertChangedTemplate();
				}
				else if (this.theSelectedItem.IsPublished)
				{
					this.ChangeChangedItemIntoPublishedItem(this.theSelectedItem);
				}
			}
			this.UpdateItemDetails();
		}

		private void OpenWorkshopPage()
		{
			System.Diagnostics.Process.Start(this.theWorkshopPageLink);
		}

		private void SaveTemplate()
		{
			this.theSelectedItemDetailsIsChangingViaMe = true;
			this.theSelectedItem.IsChanged = false;
			this.theSelectedItemDetailsIsChangingViaMe = false;
			//Me.UpdateItemListWidgets(False)
			this.UpdateItemDetails();
		}

		private void DeleteItem()
		{
			DeleteItemForm deleteItemWindow = new DeleteItemForm();
			if (this.theSelectedItem.IsPublished)
			{
				deleteItemWindow.TextBox1.Text = "Deleting will remove the item from the Workshop permanently." + "\r\n" + "Backup anything you want to save before deleting.";
				if (deleteItemWindow.ShowDialog() == DialogResult.OK)
				{
					this.DeletePublishedItemFromWorkshop();
				}
			}
			else
			{
				deleteItemWindow.TextBox1.Text = "Deleting will remove the item from your saved items permanently." + "\r\n" + "Backup anything you want to save before deleting.";
				if (deleteItemWindow.ShowDialog() == DialogResult.OK)
				{
					this.UpdateAfterDeleteItem();
				}
			}
		}

		private void UpdateAfterDeleteItem()
		{
			//NOTE: Need to make a temp variable because Me.theSelectedItem will change between function calls.
			WorkshopItem deletedItem = this.theSelectedItem;
			//If deletedItem.IsTemplate Then
			//	Me.theTemplateItemTotalCount -= 1UI
			//ElseIf Not deletedItem.IsDraft AndAlso deletedItem.IsChanged Then
			//	Me.theChangedItemTotalCount -= 1UI
			//Else
			//End If
			//NOTE: No exception is raised if item is not in any of these lists.
			this.theDisplayedItems.Remove(deletedItem);
			this.theEntireListOfItems.Remove(deletedItem);
			this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(deletedItem);
			this.UpdateItemListWidgets(false);
			this.SelectItemInGrid();

			this.GetUserSteamAppCloudQuota();
		}

		private void DeletePublishedItemFromWorkshop()
		{
			this.AppIdComboBox.Enabled = false;
			this.ItemsDataGridView.Enabled = false;
			this.ItemGroupBox.Enabled = false;
			//Me.DeleteItemButton.Enabled = False
			this.PublishItemButton.Enabled = false;
			if (!string.IsNullOrEmpty(this.LogTextBox.Text))
			{
				this.LogTextBox.AppendText("------" + "\r\n");
			}

			this.theBackgroundSteamPipe.DeletePublishedItemFromWorkshop(this.DeletePublishedItemFromWorkshop_ProgressChanged, this.DeletePublishedItemFromWorkshop_RunWorkerCompleted, this.theSelectedItem.ID);
		}

		private void GetPublishedItemDetailsViaSteamRemoteStorage(string itemID, string action)
		{
			this.AppIdComboBox.Enabled = false;
			this.ItemsDataGridView.Enabled = false;
			this.ItemGroupBox.Enabled = false;
			this.PublishItemButton.Enabled = false;
			if (!string.IsNullOrEmpty(this.LogTextBox.Text))
			{
				this.LogTextBox.AppendText("------" + "\r\n");
			}

			this.theSelectedItemDetailsIsChangingViaMe = true;

			BackgroundSteamPipe.GetPublishedFileDetailsInputInfo input = new BackgroundSteamPipe.GetPublishedFileDetailsInputInfo(itemID, this.theSteamAppId.ToString(), action);
			this.theBackgroundSteamPipe.GetPublishedItemDetails(this.GetPublishedItemDetails_ProgressChanged, this.GetPublishedItemDetails_RunWorkerCompleted, input);
		}

		private void PublishItem()
		{
			if (this.theSelectedItem.IsTemplate)
			{
				this.SaveChangedTemplateToDraft();
				this.SelectItemInGrid(this.ItemsDataGridView.Rows.Count - 1);
			}

			//NOTE: Need to do this after the template-to-draft change above.
			this.AppIdComboBox.Enabled = false;
			this.ItemsDataGridView.Enabled = false;
			this.ItemGroupBox.Enabled = false;
			//Me.ItemTitleTextBox.Enabled = False
			//Me.ItemDescriptionTextBox.Enabled = False
			//Me.ItemChangeNoteTextBox.Enabled = False
			//Me.ItemContentPathFileNameTextBox.Enabled = False
			//Me.ItemPreviewImagePathFileNameTextBox.Enabled = False
			//Me.SaveAsTemplateOrDraftItemButton.Enabled = False
			//Me.RefreshOrRevertItemButton.Enabled = False
			//Me.OpenWorkshopPageButton.Enabled = True
			//Me.DeleteItemButton.Enabled = False
			//Me.ItemTagsGroupBox.Enabled = False
			this.PublishItemButton.Enabled = false;
			if (!string.IsNullOrEmpty(this.LogTextBox.Text))
			{
				this.LogTextBox.AppendText("------" + "\r\n");
			}

			bool prePublishChecksAreSuccessful = true;
			if (this.theSelectedItem.ContentPathFolderOrFileNameIsChanged)
			{
				if (this.theSteamAppInfo.CanUseContentFolderOrFile)
				{
					if (!Directory.Exists(this.theSelectedItem.ContentPathFolderOrFileName) && !File.Exists(this.theSelectedItem.ContentPathFolderOrFileName))
					{
						this.LogTextBox.AppendText("ERROR: Item content folder or file does not exist." + "\r\n");
						prePublishChecksAreSuccessful = false;
					}
				}
				else if (this.theSteamAppInfo.UsesSteamUGC)
				{
					if (!Directory.Exists(this.theSelectedItem.ContentPathFolderOrFileName))
					{
						this.LogTextBox.AppendText("ERROR: Item content folder does not exist." + "\r\n");
						prePublishChecksAreSuccessful = false;
					}
				}
				else
				{
					if (!File.Exists(this.theSelectedItem.ContentPathFolderOrFileName))
					{
						this.LogTextBox.AppendText("ERROR: Item content file does not exist." + "\r\n");
						prePublishChecksAreSuccessful = false;
					}
				}
			}
			if (this.theSelectedItem.PreviewImagePathFileNameIsChanged)
			{
				if (!File.Exists(this.theSelectedItem.PreviewImagePathFileName))
				{
					this.LogTextBox.AppendText("ERROR: Item preview image file does not exist." + "\r\n");
					prePublishChecksAreSuccessful = false;
				}
			}
			if (!prePublishChecksAreSuccessful)
			{
				//Me.UpdateItemDetailWidgets()
				this.UpdateWidgetsAfterPublish();
				return;
			}

			this.theSelectedItemDetailsIsChangingViaMe = true;

			BackgroundSteamPipe.PublishItemInputInfo inputInfo = new BackgroundSteamPipe.PublishItemInputInfo();
			inputInfo.AppInfo = this.theSteamAppInfo;
			inputInfo.Item = this.theSelectedItem;
			this.theBackgroundSteamPipe.PublishItem(this.PublishItem_ProgressChanged, this.PublishItem_RunWorkerCompleted, inputInfo);
		}

		private void UpdateWidgetsAfterPublish()
		{
			this.AppIdComboBox.Enabled = true;
			this.ItemsDataGridView.Enabled = true;
			this.ItemTitleTextBox.Enabled = true;
			this.ItemDescriptionTextBox.Enabled = true;
			this.ItemChangeNoteTextBox.Enabled = true;
			this.ItemContentPathFileNameTextBox.Enabled = true;
			this.ItemPreviewImagePathFileNameTextBox.Enabled = true;
			this.ItemTagsGroupBox.Enabled = true;
			this.UpdateItemDetailWidgets();
		}

		private void OpenAgreementRequiresAcceptanceWindow()
		{
			AgreementRequiresAcceptanceForm agreementRequiresAcceptanceWindow = new AgreementRequiresAcceptanceForm();
			if (agreementRequiresAcceptanceWindow.ShowDialog() == DialogResult.OK)
			{
				this.OpenSteamSubscriberAgreement();
			}
			else if (agreementRequiresAcceptanceWindow.ShowDialog() == DialogResult.Ignore)
			{
				this.OpenWorkshopPage();
			}
		}

		private void SaveChangedTemplateToDraft()
		{
			this.AddDraftItem(this.theSelectedItem);
			if (this.theSelectedItem.IsChanged)
			{
				this.RevertChangedTemplate();
			}
		}

		private void RevertChangedTemplate()
		{
			this.theSelectedItemDetailsIsChangingViaMe = true;
			//NOTE: Change the item in the list (and not the Me.theSlectedItem) so that list and selected item stay synced.
			int selectedItemIndex = this.theItemBindingSource.IndexOf(this.theSelectedItem);
			this.theDisplayedItems[selectedItemIndex] = this.theUnchangedSelectedTemplateItem;
			this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(this.theSelectedItem);
			this.theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(this.theUnchangedSelectedTemplateItem);
			this.theSelectedItem.PropertyChanged -= this.WorkshopItem_PropertyChanged;
			this.theSelectedItem = this.theUnchangedSelectedTemplateItem;
			this.theSelectedItem.PropertyChanged += this.WorkshopItem_PropertyChanged;
			this.theSelectedItemDetailsIsChangingViaMe = false;
		}

#endregion

#region Data

		protected System.Windows.Forms.ContextMenuStrip ItemContextMenuStrip;
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(UseInDownloadToolStripMenuItem))]
		private System.Windows.Forms.ToolStripMenuItem _UseInDownloadToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem UseInDownloadToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _UseInDownloadToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_UseInDownloadToolStripMenuItem != null)
				{
					_UseInDownloadToolStripMenuItem.Click -= UseInDownloadToolStripMenuItem_Click;
				}

				_UseInDownloadToolStripMenuItem = value;

				if (value != null)
				{
					_UseInDownloadToolStripMenuItem.Click += UseInDownloadToolStripMenuItem_Click;
				}
			}
		}

		//Protected WithEvents ItemsDataGridViewContextMenuStrip As System.Windows.Forms.ContextMenuStrip
		//Public WithEvents ItemsDataGridViewUseInDownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

		//Protected WithEvents ItemIdLabelContextMenuStrip As System.Windows.Forms.ContextMenuStrip
		//Public WithEvents ItemIdLabelUseInDownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

		//Protected WithEvents ItemIdTextBoxContextMenuStrip As System.Windows.Forms.ContextMenuStrip
		//Public WithEvents ItemIdTextBoxUseInDownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

		private SteamAppUserInfo theSteamAppUserInfo;

		private ulong theUserSteamID;
		private SteamAppInfoBase theSteamAppInfo;
		private Steamworks.AppId_t theSteamAppId;

		//Private theTemplateItemDisplayedCount As UInteger
		//Private theTemplateItemTotalCount As UInteger
		//Private theChangedItemDisplayedCount As UInteger
		//Private theChangedItemTotalCount As UInteger
		private uint theExpectedPublishedItemCount;
		private WorkshopItemBindingList theDisplayedItems;
		private WorkshopItemBindingList theEntireListOfItems;
		private bool theSelectedGameIsStillUpdatingInterface;

		private Base_TagsUserControl theTagsWidget;

		private BindingSource theItemBindingSource;

		private WorkshopItem theSelectedItem;
		private string theWorkshopPageLink;
		private bool theSelectedItemIsChangingViaMe;
		private bool theSelectedItemDetailsIsChangingViaMe;
		private string theSavedPreviewImagePathFileName;
		private WorkshopItem theUnchangedSelectedTemplateItem;

		private BackgroundSteamPipe theBackgroundSteamPipe;

		private void ItemPreviewImagePictureBox_Resize(object sender, EventArgs e)
		{
			// Make sure size stays a square even when theme font changes it.
			int width = this.ItemPreviewImagePictureBox.Width;
			int height = this.ItemPreviewImagePictureBox.Height;
			if (width != height)
			{
				int length = Math.Min(width, height);
				this.ItemPreviewImagePictureBox.Size = new System.Drawing.Size(length, length);
			}
		}

#endregion

	}

}