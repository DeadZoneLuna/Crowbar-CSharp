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
			ToolStrip1.Font = Font;
			foreach (Control widget in ToolStrip1.Controls)
			{
				widget.Font = Font;
			}

			UseInDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			UseInDownloadToolStripMenuItem.Name = "ItemsDataGridViewUseInDownloadToolStripMenuItem";
			UseInDownloadToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			UseInDownloadToolStripMenuItem.Text = "Use in Download";

			ItemContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
			ItemContextMenuStrip.Items.Add(UseInDownloadToolStripMenuItem);
			ItemContextMenuStrip.Name = "ItemsDataGridViewContextMenuStrip";
			ItemContextMenuStrip.Size = new System.Drawing.Size(177, 114);
			ContextMenuStrip = ItemContextMenuStrip;

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
					Free();
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
			AppIdComboBox.DisplayMember = "Name";
			AppIdComboBox.ValueMember = "ID";
			AppIdComboBox.DataSource = MainCROWBAR.TheApp.SteamAppInfos;
			AppIdComboBox.DataBindings.Add("SelectedIndex", MainCROWBAR.TheApp.Settings, "PublishGameSelectedIndex", false, DataSourceUpdateMode.OnPropertyChanged);

			theBackgroundSteamPipe = new BackgroundSteamPipe();

			GetUserSteamID();

			theSelectedItemIsChangingViaMe = true;
			theItemBindingSource = new BindingSource();
			InitItemListWidgets();
			theItemBindingSource.DataSource = theDisplayedItems;
			InitItemDetailWidgets();
			theSelectedItemIsChangingViaMe = false;

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;

			theSelectedGameIsStillUpdatingInterface = false;
			UpdateSteamAppWidgets();
		}

		//NOTE: This is called after all child widgets (created via designer) are disposed but before this UserControl is disposed.
		private void Free()
		{
			if (theBackgroundSteamPipe != null)
			{
				theBackgroundSteamPipe.Kill();
			}

			if (theTagsWidget != null)
			{
				theTagsWidget.TagsPropertyChanged -= TagsWidget_TagsPropertyChanged;
			}

			if (theSelectedItem != null)
			{
				if (theSelectedItem.IsTemplate && theSelectedItem.IsChanged)
				{
					SaveChangedTemplateToDraft();
				}
				theSelectedItem.PropertyChanged -= WorkshopItem_PropertyChanged;
			}

			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
		}

		private void GetUserSteamID()
		{
			SteamPipe steamPipe = new SteamPipe();
			string result = steamPipe.Open("GetUserSteamID", null, "");
			if (result != "success")
			{
				theUserSteamID = 0;
				return;
			}
			theUserSteamID = steamPipe.GetUserSteamID();
			steamPipe.Shut();
		}

		//NOTE: Gets the quota for the logged-in Steam user for the selected SteamApp. 
		private void GetUserSteamAppCloudQuota()
		{
			if (theSteamAppInfo.UsesSteamUGC)
			{
				QuotaProgressBar.Text = "";
				QuotaProgressBar.Value = 0;
				ToolTip1.SetToolTip(QuotaProgressBar, "");
			}
			else
			{
				SteamPipe steamPipe = new SteamPipe();
				string result = steamPipe.Open("GetQuota", null, "");
				if (result != "success")
				{
					theUserSteamID = 0;
					return;
				}
				ulong availableBytes = 0;
				ulong totalBytes = 0;
				steamPipe.GetQuota(ref availableBytes, ref totalBytes);
				steamPipe.Shut();

				if (totalBytes == 0)
				{
					QuotaProgressBar.Text = "unknown";
					QuotaProgressBar.Value = 0;
					ToolTip1.SetToolTip(QuotaProgressBar, "Quota (unknown)");
				}
				else
				{
					ulong usedBytes = totalBytes - availableBytes;
					int progressPercentage = Convert.ToInt32(usedBytes * (ulong)QuotaProgressBar.Maximum / totalBytes);
					string availableBytesText = MathModule.ByteUnitsConversion(availableBytes);
					string usedBytesText = MathModule.ByteUnitsConversion(usedBytes);
					string totalBytesText = MathModule.ByteUnitsConversion(totalBytes);
					QuotaProgressBar.Text = availableBytesText + " available ";
					QuotaProgressBar.Value = progressPercentage;
					ToolTip1.SetToolTip(QuotaProgressBar, "Quota: " + usedBytesText + " used of " + totalBytesText + " total (" + progressPercentage.ToString() + "% used)");
				}
			}
		}

		private void InitItemListWidgets()
		{
			theDisplayedItems = new WorkshopItemBindingList();
			theEntireListOfItems = new WorkshopItemBindingList();

			ItemsDataGridView.AutoGenerateColumns = false;
			ItemsDataGridView.DataSource = theItemBindingSource;

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
			ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "ID";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Item ID";
			textColumn.Name = "ID";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 100;
			ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Title";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Title";
			textColumn.Name = "Title";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 200;
			ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Posted";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Posted";
			textColumn.Name = "Posted";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 110;
			ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Updated";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Updated";
			textColumn.Name = "Updated";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 110;
			ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Visibility";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Visibility";
			textColumn.Name = "Visibility";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 75;
			ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "OwnerName";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.HeaderText = "Owner";
			textColumn.Name = "Owner";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			textColumn.Width = 120;
			ItemsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.DataPropertyName = "";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.FillWeight = 100;
			textColumn.HeaderText = "";
			textColumn.Name = "";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			ItemsDataGridView.Columns.Add(textColumn);

			SearchItemsToolStripComboBox.ComboBox.DisplayMember = "Value";
			SearchItemsToolStripComboBox.ComboBox.ValueMember = "Key";
			SearchItemsToolStripComboBox.ComboBox.DataSource = EnumHelper.ToList(typeof(AppEnums.PublishSearchFieldOptions));
			SearchItemsToolStripComboBox.ComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "PublishSearchField", false, DataSourceUpdateMode.OnPropertyChanged);
			SearchItemsToolStripTextBox.TextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "PublishSearchText", false, DataSourceUpdateMode.OnValidation);
		}

		private void InitItemDetailWidgets()
		{
			ItemTitleTextBox.MaxLength = (int)Steamworks.Constants.k_cchPublishedDocumentTitleMax;
			ItemDescriptionTextBox.MaxLength = (int)Steamworks.Constants.k_cchPublishedDocumentDescriptionMax;
			ItemChangeNoteTextBox.MaxLength = (int)Steamworks.Constants.k_cchPublishedDocumentChangeDescriptionMax;

			ItemIDTextBox.DataBindings.Add("Text", theItemBindingSource, "ID", false, DataSourceUpdateMode.OnValidation);
			//TODO: Change ID textbox to combobox dropdownlist that lists most-recently accessed IDs, including those selected via list.
			//Dim anEnumList As IList
			//anEnumList = EnumHelper.ToList(GetType(SteamUGCPublishedFileVisibility))
			//Me.ItemIDComboBox.DisplayMember = "Value"
			//Me.ItemIDComboBox.ValueMember = "Key"
			//Me.ItemIDComboBox.DataSource = anEnumList
			//Me.ItemIDComboBox.DataBindings.Add("SelectedValue", Me.theItemBindingSource, "ID", False, DataSourceUpdateMode.OnPropertyChanged)

			ItemOwnerTextBox.DataBindings.Add("Text", theItemBindingSource, "OwnerName", false, DataSourceUpdateMode.OnValidation);
			ItemPostedTextBox.DataBindings.Add("Text", theItemBindingSource, "Posted", false, DataSourceUpdateMode.OnValidation);
			ItemUpdatedTextBox.DataBindings.Add("Text", theItemBindingSource, "Updated", false, DataSourceUpdateMode.OnValidation);
			ItemTitleTextBox.DataBindings.Add("Text", theItemBindingSource, "Title", false, DataSourceUpdateMode.OnPropertyChanged);
			//NOTE: For RichTextBox, set the Formatting argument to True when DataSourceUpdateMode.OnPropertyChanged is used, to prevent characters being entered in reverse order.
			ItemDescriptionTextBox.DataBindings.Add("Text", theItemBindingSource, "Description", true, DataSourceUpdateMode.OnPropertyChanged);
			ItemChangeNoteTextBox.DataBindings.Add("Text", theItemBindingSource, "ChangeNote", true, DataSourceUpdateMode.OnPropertyChanged);
			ItemContentPathFileNameTextBox.DataBindings.Add("Text", theItemBindingSource, "ContentPathFolderOrFileName", false, DataSourceUpdateMode.OnValidation);
			ItemPreviewImagePathFileNameTextBox.DataBindings.Add("Text", theItemBindingSource, "PreviewImagePathFileName", false, DataSourceUpdateMode.OnValidation);

			ItemVisibilityComboBox.DisplayMember = "Value";
			ItemVisibilityComboBox.ValueMember = "Key";
			ItemVisibilityComboBox.DataSource = EnumHelper.ToList(typeof(WorkshopItem.SteamUGCPublishedItemVisibility));
			ItemVisibilityComboBox.DataBindings.Add("SelectedValue", theItemBindingSource, "Visibility", false, DataSourceUpdateMode.OnPropertyChanged);
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
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(AppIdComboBox, RefreshGameItemsButton);

			if (!DesignMode)
			{
				Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		private void RefreshGameItemsButton_Click(object sender, EventArgs e)
		{
			UpdateSteamAppWidgets();
		}

		private void OpenSteamSubscriberAgreementButton_Click(object sender, EventArgs e)
		{
			OpenSteamSubscriberAgreement();
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
			if (!theSelectedItemIsChangingViaMe && ItemsDataGridView.SelectedRows.Count > 0)
			{
				//If Me.ItemsDataGridView.SelectedRows.Count > 0 Then
				//NOTE: Allow the highlight to show in the grid before updating item details.
				Application.DoEvents();
				if (theSelectedItem != null && theSelectedItem.IsTemplate && theSelectedItem.IsChanged)
				{
					SaveChangedTemplateToDraft();
				}
				UpdateItemDetails();
			}
		}

		//NOTE: Without this handler, when Items grid is sorted, the selection stays at list index instead of with the item.
		private void ItemsDataGridView_Sorted(object sender, EventArgs e)
		{
			theSelectedItemIsChangingViaMe = true;
			theItemBindingSource.Position = theItemBindingSource.IndexOf(theSelectedItem);
			theSelectedItemIsChangingViaMe = false;
		}

		//Private Sub UseInDownloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsDataGridViewUseInDownloadToolStripMenuItem.Click, ItemIdLabelUseInDownloadToolStripMenuItem.Click, ItemIdTextBoxUseInDownloadToolStripMenuItem.Click
		//	Me.UseItemIdInDownload()
		//End Sub
		private void UseInDownloadToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			UseItemIdInDownload();
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
					SearchItems();
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void SearchItemsToolStripButton_Click(object sender, EventArgs e)
		{
			SearchItems();
		}

		private void AddItemButton_Click(object sender, EventArgs e)
		{
			AddDraftItem(null);
			SelectItemInGrid(ItemsDataGridView.Rows.Count - 1);
		}

		private void OwnerLabel_DoubleClick(object sender, EventArgs e)
		{
			SwapBetweenOwnerNameAndID();
		}

		private void ToggleWordWrapForDescriptionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			ToggleWordWrapImageOnCheckbox((CheckBox)sender);
			ItemDescriptionTextBox.WordWrap = ToggleWordWrapForDescriptionCheckBox.Checked;
		}

		private void ToggleWordWrapForChangeNoteCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			ToggleWordWrapImageOnCheckbox((CheckBox)sender);
			ItemChangeNoteTextBox.WordWrap = ToggleWordWrapForChangeNoteCheckBox.Checked;
		}

		private void BrowseContentPathFileNameButton_Click(object sender, EventArgs e)
		{
			BrowseForContentPathFolderOrFileName();
		}

		private void BrowsePreviewImageButton_Click(object sender, EventArgs e)
		{
			BrowseForPreviewImage();
		}

		private void SaveAsTemplateOrDraftItemButton_Click(object sender, EventArgs e)
		{
			if (SaveAsTemplateOrDraftItemButton.Text == "Save as Template")
			{
				SaveItemAsTemplate();
			}
			else
			{
				AddDraftItem(theSelectedItem);
				if (theSelectedItem.IsTemplate && theSelectedItem.IsChanged)
				{
					RevertChangedTemplate();
				}
				SelectItemInGrid(ItemsDataGridView.Rows.Count - 1);
			}
		}

		private void RefreshOrRevertButton_Click(object sender, EventArgs e)
		{
			RefreshOrRevertItem();
		}

		private void OpenWorkshopPageButton_Click(object sender, EventArgs e)
		{
			OpenWorkshopPage();
		}

		private void SaveTemplateButton_Click(object sender, EventArgs e)
		{
			SaveTemplate();
		}

		private void DeleteItemButton_Click(object sender, EventArgs e)
		{
			DeleteItem();
		}

		//NOTE: There is no automatic data-binding with TagsWidget, so manually bind from widget to object here.
		private void TagsWidget_TagsPropertyChanged(object sender, EventArgs e)
		{
			theSelectedItem.Tags = theTagsWidget.ItemTags;
		}

		private void PublishItemButton_Click(object sender, EventArgs e)
		{
			PublishItem();
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "PublishGameSelectedIndex")
			{
				MainCROWBAR.TheApp.Settings.PublishSearchField = AppEnums.PublishSearchFieldOptions.ID;
				MainCROWBAR.TheApp.Settings.PublishSearchText = "";

				UpdateSteamAppWidgets();
			}
		}

		private void WorkshopItem_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (theSelectedItemDetailsIsChangingViaMe)
			{
				return;
			}

			if (e.PropertyName == "ID")
			{
				theWorkshopPageLink = AppConstants.WorkshopLinkStart + theSelectedItem.ID;
			}
			else if (e.PropertyName == "Title")
			{
				UpdateItemTitleLabel();
				UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "Description")
			{
				UpdateItemDescriptionLabel();
				UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "ChangeNote")
			{
				UpdateItemChangeNoteLabel();
				UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "ContentSize")
			{
				UpdateItemContentLabel();
			}
			else if (e.PropertyName == "ContentPathFolderOrFileName")
			{
				UpdateItemContentLabel();
				UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "PreviewImageSize")
			{
				UpdateItemPreviewImageLabel();
				UpdateItemPreviewImageBox();
			}
			else if (e.PropertyName == "PreviewImagePathFileName")
			{
				UpdateItemPreviewImageLabel();
				UpdateItemPreviewImageBox();
				UpdateItemChangedStatus();
				//NOTE: Using this property raises an exception, possibly because the DataGridView gets confused by the property being a list, so use "TagsAsTextLine" property.
				//ElseIf e.PropertyName = "Tags" Then
			}
			else if (e.PropertyName == "TagsAsTextLine")
			{
				UpdateItemTagsLabel();
				UpdateItemChangedStatus();
			}
			else if (e.PropertyName == "Visibility")
			{
				UpdateItemVisibilityLabel();
				UpdateItemChangedStatus();
			}

			if (theSelectedItem.IsDraft)
			{
				theSelectedItem.Updated = MathModule.DateTimeToUnixTimeStamp(DateTime.Now);
			}
		}

		private void GetPublishedItems_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 1)
			{
				theExpectedPublishedItemCount = Convert.ToUInt32(e.UserState);
			}
			else if (e.ProgressPercentage == 2)
			{
				WorkshopItem publishedItem = (WorkshopItem)e.UserState;

				bool itemHasBeenFound = false;
				foreach (WorkshopItem item in theDisplayedItems)
				{
					if (item.ID == publishedItem.ID)
					{
						itemHasBeenFound = true;
						break;
					}
				}
				if (!itemHasBeenFound)
				{
					theDisplayedItems.Add(publishedItem);
				}

				itemHasBeenFound = false;
				foreach (WorkshopItem item in theEntireListOfItems)
				{
					if (item.ID == publishedItem.ID)
					{
						itemHasBeenFound = true;
						break;
					}
				}
				if (!itemHasBeenFound)
				{
					theEntireListOfItems.Add(publishedItem);
				}
				//ElseIf e.ProgressPercentage = 3 Then
				//	Dim availableBytes As ULong = CULng(e.UserState)
				//	Me.SteamCloudSizeLabel.Text = "(" + availableBytes.ToString("N") + " used /"
				//ElseIf e.ProgressPercentage = 4 Then
				//	Dim totalBytes As ULong = CULng(e.UserState)
				//	Me.SteamCloudSizeLabel.Text += totalBytes.ToString("N") + " total)"
			}

			UpdateItemListWidgets(true);
		}

		private void GetPublishedItems_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				int debug = 4242;
			}
			else
			{
				UpdateItemListWidgets(false);
			}
			theSelectedGameIsStillUpdatingInterface = false;
		}

		private void GetPublishedItemDetails_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 1)
			{
				if (ItemPreviewImagePictureBox.Image != null)
				{
					ItemPreviewImagePictureBox.Image.Dispose();
				}
				ItemPreviewImagePictureBox.Image = (Image)e.UserState;
				//Application.DoEvents()
				ItemPreviewImagePictureBox.Refresh();
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
					if (publishedItem.ID != "0" && publishedItem.ID == theSelectedItem.ID)
					{
						theSelectedItem.Updated = publishedItem.Updated;
					}
				}
				else
				{
					if (publishedItem.ID != "0")
					{
						if (publishedItem.ID == theSelectedItem.ID)
						{
							string previewImagePathFileName = theSelectedItem.PreviewImagePathFileName;

							theSelectedItem.CreatorAppID = publishedItem.CreatorAppID;
							theSelectedItem.ID = publishedItem.ID;
							theSelectedItem.OwnerID = publishedItem.OwnerID;
							theSelectedItem.OwnerName = publishedItem.OwnerName;
							theSelectedItem.Posted = publishedItem.Posted;
							theSelectedItem.Updated = publishedItem.Updated;
							theSelectedItem.Title = publishedItem.Title;
							theSelectedItem.Description = publishedItem.Description;
							theSelectedItem.ContentSize = publishedItem.ContentSize;
							if (theSteamAppInfo.UsesSteamUGC && string.IsNullOrEmpty(publishedItem.ContentPathFolderOrFileName))
							{
								theSelectedItem.ContentPathFolderOrFileName = "Folder_" + publishedItem.ID;
							}
							else
							{
								theSelectedItem.ContentPathFolderOrFileName = publishedItem.ContentPathFolderOrFileName;
							}
							theSelectedItem.PreviewImageSize = publishedItem.PreviewImageSize;
							theSelectedItem.PreviewImagePathFileName = publishedItem.PreviewImagePathFileName;
							theSelectedItem.Visibility = publishedItem.Visibility;
							theSelectedItem.TagsAsTextLine = publishedItem.TagsAsTextLine;

							theSelectedItem.IsChanged = false;
							DeleteTempPreviewImageFile(previewImagePathFileName, theSelectedItem.ID);
							//Me.UpdateItemDetailWidgets()
						}
						else
						{
							//NOTE: This is an item from SearchItemIDs().
							if (output.Action == "FindAll")
							{
								theDisplayedItems.Add(publishedItem);
								theEntireListOfItems.Add(publishedItem);
								theSelectedItemIsChangingViaMe = true;
								SelectItemInGrid(ItemsDataGridView.Rows.Count - 1);
								theSelectedItemIsChangingViaMe = false;
							}
						}
					}
				}

				//Me.theSelectedItemIsChangingViaMe = False
			}

			//Me.theSelectedItemIsChangingViaMe = False
			theSelectedItemDetailsIsChangingViaMe = false;

			AppIdComboBox.Enabled = true;
			ItemsDataGridView.Enabled = true;
			ItemTitleTextBox.Enabled = true;
			ItemDescriptionTextBox.Enabled = true;
			ItemChangeNoteTextBox.Enabled = true;
			ItemContentPathFileNameTextBox.Enabled = true;
			ItemPreviewImagePathFileNameTextBox.Enabled = true;
			ItemTagsGroupBox.Enabled = true;
			UpdateItemDetailWidgets();
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
					LogTextBox.AppendText("Delete of published item succeeded." + "\r\n");
					if (theExpectedPublishedItemCount > 0)
					{
						theExpectedPublishedItemCount -= 1U;
					}
					else
					{
						//TODO: When testing, somehow got to here.
						int debug = 4242;
					}
					UpdateAfterDeleteItem();
				}
				else
				{
					LogTextBox.AppendText("ERROR: " + result + "\r\n");
					UpdateItemDetailButtons();
				}
			}
			AppIdComboBox.Enabled = true;
			ItemsDataGridView.Enabled = true;
			ItemTitleTextBox.Enabled = true;
			ItemDescriptionTextBox.Enabled = true;
			ItemChangeNoteTextBox.Enabled = true;
			ItemContentPathFileNameTextBox.Enabled = true;
			ItemPreviewImagePathFileNameTextBox.Enabled = true;
			ItemTagsGroupBox.Enabled = true;
		}

		private void PublishItem_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				LogTextBox.AppendText((e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 1)
			{
				LogTextBox.AppendText("\t" + (e.UserState == null ? null : Convert.ToString(e.UserState)));
			}
			else if (e.ProgressPercentage == 2)
			{
				BackgroundSteamPipe.PublishItemProgressInfo outputInfo = (BackgroundSteamPipe.PublishItemProgressInfo)e.UserState;
				//TODO: Change to using a progressbar.
				if (outputInfo.TotalUploadedByteCount > 0)
				{
					int progressPercentage = Convert.ToInt32(outputInfo.UploadedByteCount * 100 / outputInfo.TotalUploadedByteCount);
					LogTextBox.AppendText("\t" + "\t" + outputInfo.Status + ": " + outputInfo.UploadedByteCount.ToString("N0") + " / " + outputInfo.TotalUploadedByteCount.ToString("N0") + "   " + progressPercentage.ToString() + " %" + "\r\n");
				}
				else
				{
					LogTextBox.AppendText("\t" + outputInfo.Status + "." + "\r\n");
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
					if (theSelectedItem.IsDraft)
					{
						ChangeDraftItemIntoPublishedItem(theSelectedItem);
						theSelectedItem.ID = publishedItemID;
					}
					else
					{
						ChangeChangedItemIntoPublishedItem(theSelectedItem);
					}

					theSelectedItem.OwnerID = outputInfo.PublishedItemOwnerID;
					theSelectedItem.OwnerName = outputInfo.PublishedItemOwnerName;
					theSelectedItem.Posted = outputInfo.PublishedItemPosted;
					theSelectedItem.Updated = outputInfo.PublishedItemUpdated;
					theSelectedItem.ChangeNote = "";

					if (outputInfo.SteamAgreementStatus == "NotAccepted")
					{
						agreementWindowShouldBeShown = true;
					}

					DeleteInUseTempPreviewImageFile(theSelectedItem.PreviewImagePathFileName, theSelectedItem.ID);
					theSelectedItem.PreviewImagePathFileName = Path.GetFileName(theSelectedItem.PreviewImagePathFileName);
					theSelectedItem.PreviewImagePathFileNameIsChanged = false;
				}
				else if (result == "FailedContentAndChangeNote")
				{
					//NOTE: Content file and change note were not updated. Keep their changed status.
					if (theSelectedItem.IsDraft)
					{
						bool contentPathFolderOrFileNameIsChanged = theSelectedItem.ContentPathFolderOrFileNameIsChanged;
						bool changeNoteIsChanged = theSelectedItem.ChangeNoteIsChanged;

						ChangeDraftItemIntoPublishedItem(theSelectedItem);

						theSelectedItem.ContentPathFolderOrFileNameIsChanged = contentPathFolderOrFileNameIsChanged;
						theSelectedItem.ChangeNoteIsChanged = changeNoteIsChanged;
						theSelectedItem.IsChanged = contentPathFolderOrFileNameIsChanged || changeNoteIsChanged;
					}

					theSelectedItem.TitleIsChanged = false;
					theSelectedItem.DescriptionIsChanged = false;
					theSelectedItem.PreviewImagePathFileNameIsChanged = false;
					theSelectedItem.VisibilityIsChanged = false;
					theSelectedItem.TagsIsChanged = false;
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
			UpdateWidgetsAfterPublish();

			GetUserSteamAppCloudQuota();

			theSelectedItemDetailsIsChangingViaMe = false;

			if (agreementWindowShouldBeShown)
			{
				OpenAgreementRequiresAcceptanceWindow();
			}
		}

#endregion

#region Private Methods

		private void UpdateSteamAppWidgets()
		{
			//NOTE: If this has not been created, then app is in not far enough in Init() and not ready for update.
			if (theEntireListOfItems == null || theSelectedGameIsStillUpdatingInterface)
			{
				return;
			}
			theSelectedGameIsStillUpdatingInterface = true;

			if (!string.IsNullOrEmpty(LogTextBox.Text))
			{
				LogTextBox.AppendText("------" + "\r\n");
			}

			theSteamAppInfo = MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex];
			theSteamAppId = theSteamAppInfo.ID;
			MainCROWBAR.TheApp.WriteSteamAppIdFile(theSteamAppId.m_AppId);

			theSteamAppUserInfo = null;
			try
			{
				if (MainCROWBAR.TheApp.Settings.PublishSteamAppUserInfos.Count > 0)
				{
					//NOTE: Using FirstOrDefault() instead of First() to avoid an exception when no item is found.
					theSteamAppUserInfo = MainCROWBAR.TheApp.Settings.PublishSteamAppUserInfos.FirstOrDefault((info) => info.AppID == (int)theSteamAppId.m_AppId);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			if (theSteamAppUserInfo == null)
			{
				//NOTE: Value was not found, so set to new info.
				theSteamAppUserInfo = new SteamAppUserInfo(theSteamAppId.m_AppId);
				MainCROWBAR.TheApp.Settings.PublishSteamAppUserInfos.Add(theSteamAppUserInfo);
			}

			//NOTE: Swap the Tags widget before selecting an item so when item is selected tags will set correctly.
			SwapSteamAppTagsWidget();

			int selectedRowIndex = 0;
			theDisplayedItems.Clear();
			theEntireListOfItems.Clear();
			//Me.theTemplateItemTotalCount = 0
			//Me.theChangedItemTotalCount = 0
			if (theSteamAppUserInfo.DraftTemplateAndChangedItems.Count == 0)
			{
				AddDraftItem(null);
			}
			else
			{
				WorkshopItem draftItem = null;
				long mostRecentlyUpdatedDraftItemDateTime = 0;
				for (int draftItemIndex = 0; draftItemIndex < theSteamAppUserInfo.DraftTemplateAndChangedItems.Count; draftItemIndex++)
				{
					draftItem = theSteamAppUserInfo.DraftTemplateAndChangedItems[draftItemIndex];
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
					theDisplayedItems.Add(draftItem);
					theEntireListOfItems.Add(draftItem);
				}
			}
			SelectItemInGrid(selectedRowIndex);

			GetUserSteamAppCloudQuota();

			theBackgroundSteamPipe.GetPublishedItems(GetPublishedItems_ProgressChanged, GetPublishedItems_RunWorkerCompleted, theSteamAppId.ToString());
		}

		private void SwapSteamAppTagsWidget()
		{
			if (theTagsWidget != null)
			{
				theTagsWidget.TagsPropertyChanged -= TagsWidget_TagsPropertyChanged;
			}

			//Me.theTagsWidget = CType(Me.AppIdComboBox.SelectedItem, SteamAppInfo).TagsWidget
			//Dim info As SteamAppInfo = CType(Me.AppIdComboBox.SelectedItem, SteamAppInfo)
			SteamAppInfoBase info = MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex];
			Type t = info.TagsControlType;
			theTagsWidget = (Base_TagsUserControl)(t.GetConstructor(new System.Type[0]).Invoke(new object[0]));
			if (ItemTagsGroupBox.Controls.Count > 0)
			{
				ItemTagsGroupBox.Controls.RemoveAt(0);
			}
			ItemTagsGroupBox.Controls.Add(theTagsWidget);
			theTagsWidget.AutoScroll = true;
			theTagsWidget.Dock = System.Windows.Forms.DockStyle.Fill;
			//Me.theTagsWidget.ItemTags = CType(Resources.GetObject("ContagionTagsUserControl1.ItemTags"), System.Collections.Generic.List(Of String))
			theTagsWidget.Location = new System.Drawing.Point(3, 17);
			theTagsWidget.Name = "TagsUserControl";
			theTagsWidget.Size = new System.Drawing.Size(193, 307);
			theTagsWidget.TabIndex = 0;

			theTagsWidget.TagsPropertyChanged += TagsWidget_TagsPropertyChanged;
		}

		private void SelectItemInGrid()
		{
			int selectedRowIndex = 0;
			if (ItemsDataGridView.SelectedRows.Count > 0)
			{
				selectedRowIndex = ItemsDataGridView.SelectedRows[0].Index;
			}
			else
			{
				selectedRowIndex = ItemsDataGridView.Rows.Count - 1;
			}
			SelectItemInGrid(selectedRowIndex);
		}

		private void SelectItemInGrid(int selectedRowIndex)
		{
			if (selectedRowIndex >= ItemsDataGridView.Rows.Count)
			{
				selectedRowIndex = ItemsDataGridView.Rows.Count - 1;
			}
			//NOTE: This line does not update the widgets connected to the list fields.
			ItemsDataGridView.Rows[selectedRowIndex].Selected = true;
			//NOTE: This line is required so that the item detail widgets update when the gird selection is changed programmatically.
			ItemsDataGridView.CurrentCell = ItemsDataGridView.Rows[selectedRowIndex].Cells[0];
			ItemsDataGridView.FirstDisplayedScrollingRowIndex = selectedRowIndex;
		}

		private void UseItemIdInDownload()
		{
			MainCROWBAR.TheApp.Settings.DownloadItemIdOrLink = theSelectedItem.ID;
		}

		private void SearchItems()
		{
			if (string.IsNullOrEmpty(SearchItemsToolStripTextBox.Text))
			{
				ClearSearch();
			}
			else
			{
				if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.ID)
				{
					SearchItemIDs();
				}
				else if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.Owner)
				{
					SearchItemOwnerNames();
				}
				else if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.Title)
				{
					SearchItemTitles();
				}
				else if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.Description)
				{
					SearchItemDescriptions();
				}
				else if (MainCROWBAR.TheApp.Settings.PublishSearchField == AppEnums.PublishSearchFieldOptions.AllFields)
				{
					SearchItemAllFields();
				}
			}
			UpdateItemListWidgets(false);
		}

		private void ClearSearch()
		{
			theDisplayedItems.Clear();
			foreach (WorkshopItem item in theEntireListOfItems)
			{
				theDisplayedItems.Add(item);
			}
		}

		private void SearchItemIDs()
		{
			string itemTextToFind = SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			theDisplayedItems.Clear();
			foreach (WorkshopItem item in theEntireListOfItems)
			{
				if (item.ID.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					theDisplayedItems.Add(item);
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
			string itemTextToFind = SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			theDisplayedItems.Clear();
			foreach (WorkshopItem item in theEntireListOfItems)
			{
				if (item.OwnerName.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					theDisplayedItems.Add(item);
				}
			}
		}

		private void SearchItemTitles()
		{
			string itemTextToFind = SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			theDisplayedItems.Clear();
			foreach (WorkshopItem item in theEntireListOfItems)
			{
				if (item.Title.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					theDisplayedItems.Add(item);
				}
			}
		}

		private void SearchItemDescriptions()
		{
			string itemTextToFind = SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			theDisplayedItems.Clear();
			foreach (WorkshopItem item in theEntireListOfItems)
			{
				if (item.Description.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					theDisplayedItems.Add(item);
				}
			}
		}

		private void SearchItemAllFields()
		{
			string itemTextToFind = SearchItemsToolStripTextBox.Text.ToLower();
			bool itemHasBeenFound = false;

			theDisplayedItems.Clear();
			foreach (WorkshopItem item in theEntireListOfItems)
			{
				if (item.ID.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					theDisplayedItems.Add(item);
				}
				else if (item.OwnerName.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					theDisplayedItems.Add(item);
				}
				else if (item.Title.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					theDisplayedItems.Add(item);
				}
				else if (item.Description.ToLower().Contains(itemTextToFind))
				{
					itemHasBeenFound = true;
					theDisplayedItems.Add(item);
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
			theDisplayedItems.Add(draftItem);
			theEntireListOfItems.Add(draftItem);
			theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(draftItem);
			UpdateItemListWidgets(false);
		}

		private void ChangeDraftItemIntoPublishedItem(WorkshopItem item)
		{
			theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(item);
			item.IsChanged = false;
			UpdateItemListWidgets(false);
		}

		private void ChangePublishedItemIntoChangedItem(WorkshopItem item)
		{
			theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(item);
			//Me.theChangedItemTotalCount += 1UI
			UpdateItemListWidgets(false);
			SaveCopyOfPreviewImageFile(item);
		}

		private void ChangeChangedItemIntoPublishedItem(WorkshopItem item)
		{
			theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(item);
			//Me.theChangedItemTotalCount -= 1UI
			item.ChangeNote = "";
			item.IsChanged = false;
			UpdateItemListWidgets(false);
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
				DataGridViewColumn sortedColumn = ItemsDataGridView.SortedColumn;
				if (sortedColumn != null)
				{
					ListSortDirection direction = 0;
					if (ItemsDataGridView.SortOrder == SortOrder.Ascending)
					{
						direction = ListSortDirection.Ascending;
					}
					else
					{
						direction = ListSortDirection.Descending;
					}
					ItemsDataGridView.Sort(sortedColumn, direction);
				}
			}

			//Dim draftItemsDisplayedCount As UInteger = CUInt(Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count - Me.theTemplateItemDisplayedCount - Me.theChangedItemDisplayedCount)
			//Dim publishedItemsDisplayedCount As UInteger = CUInt(Me.theDisplayedItems.Count - Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count)
			//Dim draftItemsTotalCount As UInteger = CUInt(Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count - Me.theTemplateItemTotalCount - Me.theChangedItemTotalCount)
			//Dim publishedItemsTotalCount As UInteger = CUInt(Me.theEntireListOfItems.Count - Me.theSteamAppUserInfo.DraftTemplateAndChangedItems.Count)
			uint draftItemsDisplayedCount = theDisplayedItems.DraftItemCount;
			uint publishedItemsDisplayedCount = theDisplayedItems.PublishedItemCount;
			uint draftItemsTotalCount = theEntireListOfItems.DraftItemCount;
			uint publishedItemsTotalCount = theEntireListOfItems.PublishedItemCount;

			ItemCountsToolStripLabel.Text = "";
			if (theDisplayedItems.Count != theEntireListOfItems.Count)
			{
				ItemCountsToolStripLabel.Text += draftItemsDisplayedCount.ToString() + "/";
			}
			ItemCountsToolStripLabel.Text += draftItemsTotalCount.ToString() + " draft + ";
			if (theDisplayedItems.Count != theEntireListOfItems.Count)
			{
				//Me.ItemCountsToolStripLabel.Text += Me.theTemplateItemDisplayedCount.ToString() + "/"
				ItemCountsToolStripLabel.Text += theDisplayedItems.TemplateItemCount.ToString() + "/";
			}
			//Me.ItemCountsToolStripLabel.Text += Me.theTemplateItemTotalCount.ToString() + " template + "
			ItemCountsToolStripLabel.Text += theEntireListOfItems.TemplateItemCount.ToString() + " template + ";
			if (theDisplayedItems.Count != theEntireListOfItems.Count)
			{
				//Me.ItemCountsToolStripLabel.Text += Me.theChangedItemDisplayedCount.ToString() + "/"
				ItemCountsToolStripLabel.Text += theDisplayedItems.ChangedItemCount.ToString() + "/";
			}
			//Me.ItemCountsToolStripLabel.Text += Me.theChangedItemTotalCount.ToString() + " changed + "
			ItemCountsToolStripLabel.Text += theEntireListOfItems.ChangedItemCount.ToString() + " changed + ";
			if (theDisplayedItems.Count != theEntireListOfItems.Count)
			{
				ItemCountsToolStripLabel.Text += publishedItemsDisplayedCount.ToString() + "/";
			}
			ItemCountsToolStripLabel.Text += publishedItemsTotalCount.ToString() + " published";
			if (isProgress)
			{
				uint remainingPublishedItemsCount = theExpectedPublishedItemCount - publishedItemsTotalCount;
				ItemCountsToolStripLabel.Text += " (" + remainingPublishedItemsCount.ToString() + " more to get)";
			}
			else
			{
				//If (publishedItemsTotalCount + Me.theChangedItemTotalCount) <> Me.theExpectedPublishedItemCount Then
				if ((publishedItemsTotalCount + theEntireListOfItems.ChangedItemCount) != theExpectedPublishedItemCount)
				{
					ItemCountsToolStripLabel.Text += " (" + theExpectedPublishedItemCount.ToString() + " expected)";
				}
			}
			ItemCountsToolStripLabel.Text += " = ";
			if (theDisplayedItems.Count != theEntireListOfItems.Count)
			{
				ItemCountsToolStripLabel.Text += theDisplayedItems.Count.ToString() + "/";
			}
			ItemCountsToolStripLabel.Text += theEntireListOfItems.Count.ToString() + " total";
		}

		private void UpdateItemDetails()
		{
			if (theSelectedItem != null)
			{
				theSelectedItem.PropertyChanged -= WorkshopItem_PropertyChanged;
			}
			theSelectedItem = theDisplayedItems[ItemsDataGridView.SelectedRows[0].Index];
			theSelectedItem.PropertyChanged += WorkshopItem_PropertyChanged;

			if (theSelectedItem.IsDraft)
			{
				UpdateItemDetailWidgets();
			}
			else if (theSelectedItem.IsTemplate)
			{
				theUnchangedSelectedTemplateItem = (WorkshopItem)theSelectedItem.Clone();
				theUnchangedSelectedTemplateItem.IsTemplate = true;
				UpdateItemDetailWidgets();
			}
			else if (theSelectedItem.IsChanged)
			{
				UpdateItemDetailWidgets();
			}
			else
			{
				//NOTE: UpdateItemDetailWidgets() will be called from the 'bw_completed' handler.
				GetPublishedItemDetailsViaSteamRemoteStorage(theSelectedItem.ID, "All");
				return;
			}
		}

		private void UpdateItemDetailWidgets()
		{
			if (theUserSteamID == 0)
			{
				GetUserSteamID();
			}

						//Dim editableTextBoxesAreReadOnly As Boolean = (theSelectedItem.IsPublished) AndAlso (theSelectedItem.OwnerID < > theUserSteamID)
			bool editableTextBoxesAreReadOnly = theSelectedItem.IsPublished && theSelectedItem.OwnerID != theUserSteamID;
			bool editableNonTextWidgetsAreEnabled = (theSelectedItem.IsDraft) || (theSelectedItem.IsTemplate) || (theSelectedItem.OwnerID == theUserSteamID);

			ItemGroupBox.Enabled = true;
			UpdateItemGroupBoxLabel();

			UpdateItemTitleLabel();
			ItemTitleTextBox.ReadOnly = editableTextBoxesAreReadOnly;

			UpdateItemDescriptionLabel();
			ItemDescriptionTextBox.ReadOnly = editableTextBoxesAreReadOnly;

			UpdateItemChangeNoteLabel();
			ItemChangeNoteTextBox.ReadOnly = editableTextBoxesAreReadOnly;

			UpdateItemContentLabel();
			ItemContentPathFileNameTextBox.ReadOnly = editableTextBoxesAreReadOnly;

			UpdateItemPreviewImageLabel();
			ItemPreviewImagePathFileNameTextBox.ReadOnly = editableTextBoxesAreReadOnly;
			if (!theSelectedItem.IsPublished || theSelectedItem.IsChanged)
			{
				UpdateItemPreviewImageBox();
			}

			UpdateItemVisibilityLabel();
			ItemVisibilityComboBox.Enabled = editableNonTextWidgetsAreEnabled;

			ItemTagsGroupBox.Enabled = true;
			//NOTE: There is no automatic data-binding with TagsWidget, so manually bind from object to widget here.
			theTagsWidget.ItemTags = theSelectedItem.Tags;
			UpdateItemTagsLabel();
			theTagsWidget.Enabled = editableNonTextWidgetsAreEnabled;

			theWorkshopPageLink = AppConstants.WorkshopLinkStart + theSelectedItem.ID;

			UpdateItemDetailButtons();
		}

		private void UpdateItemChangedStatus()
		{
			if (!theSelectedItem.IsChanged)
			{
				if (theSelectedItem.IsTemplate)
				{
					theSelectedItem.IsChanged = true;
					UpdateItemGroupBoxLabel();
				}
				else if (theSelectedItem.IsPublished)
				{
					theSelectedItem.IsChanged = true;
					ChangePublishedItemIntoChangedItem(theSelectedItem);
					UpdateItemGroupBoxLabel();
				}
			}
			theSelectedItem.Updated = MathModule.DateTimeToUnixTimeStamp(DateTime.Now);
			UpdateItemDetailButtons();
		}

		private void UpdateItemGroupBoxLabel()
		{
			string changedMarker = "";
			if (theSelectedItem.IsChanged && !theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			ItemGroupBox.Text = "Item" + changedMarker;
		}

		private void UpdateItemTitleLabel()
		{
			int titleSize = theSelectedItem.Title.Length;
			int titleSizeMax = (int)Steamworks.Constants.k_cchPublishedDocumentTitleMax;
			string changedMarker = "";
			if (theSelectedItem.TitleIsChanged && !theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			ItemTitleLabel.Text = "Title" + changedMarker + " (" + titleSize.ToString() + " / " + titleSizeMax.ToString() + " characters max):";
		}

		private void UpdateItemDescriptionLabel()
		{
			int descriptionSize = theSelectedItem.Description.Length;
			int descriptionSizeMax = (int)Steamworks.Constants.k_cchPublishedDocumentDescriptionMax;
			string changedMarker = "";
			if (theSelectedItem.DescriptionIsChanged && !theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			ItemDescriptionLabel.Text = "Description" + changedMarker + " (" + descriptionSize.ToString() + " / " + descriptionSizeMax.ToString() + " characters max):";
		}

		private void UpdateItemChangeNoteLabel()
		{
			int changeNoteSize = theSelectedItem.ChangeNote.Length;
			int changeNoteSizeMax = (int)Steamworks.Constants.k_cchPublishedDocumentChangeDescriptionMax;
			string changedMarker = "";
			if (theSelectedItem.ChangeNoteIsChanged && !theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			ItemChangeNoteLabel.Text = "Change Note" + changedMarker + " (" + changeNoteSize.ToString() + " / " + changeNoteSizeMax.ToString() + " characters max):";
		}

		private void UpdateItemContentLabel()
		{
			string contentFileSizeText = "0";
			if (MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex].CanUseContentFolderOrFile)
			{
				ItemContentFolderOrFileLabel.Text = "Content Folder or File";
				if (Directory.Exists(theSelectedItem.ContentPathFolderOrFileName))
				{
					ulong folderSize = FileManager.GetFolderSize(theSelectedItem.ContentPathFolderOrFileName);
					contentFileSizeText = MathModule.ByteUnitsConversion(folderSize);
				}
				else if (File.Exists(theSelectedItem.ContentPathFolderOrFileName))
				{
					FileInfo aFile = new FileInfo(theSelectedItem.ContentPathFolderOrFileName);
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)aFile.Length);
				}
				else if (theSelectedItem.ContentSize > 0 && theSelectedItem.IsPublished)
				{
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)theSelectedItem.ContentSize);
				}
			}
			else if (MainCROWBAR.TheApp.SteamAppInfos[MainCROWBAR.TheApp.Settings.PublishGameSelectedIndex].UsesSteamUGC)
			{
				ItemContentFolderOrFileLabel.Text = "Content Folder";
				if (Directory.Exists(theSelectedItem.ContentPathFolderOrFileName))
				{
					ulong folderSize = FileManager.GetFolderSize(theSelectedItem.ContentPathFolderOrFileName);
					contentFileSizeText = MathModule.ByteUnitsConversion(folderSize);
				}
				else if (theSelectedItem.ContentSize > 0 && theSelectedItem.IsPublished)
				{
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)theSelectedItem.ContentSize);
				}
			}
			else
			{
				ItemContentFolderOrFileLabel.Text = "Content File";
				if (File.Exists(theSelectedItem.ContentPathFolderOrFileName))
				{
					FileInfo aFile = new FileInfo(theSelectedItem.ContentPathFolderOrFileName);
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)aFile.Length);
				}
				else if (theSelectedItem.ContentSize > 0 && theSelectedItem.IsPublished)
				{
					contentFileSizeText = MathModule.ByteUnitsConversion((ulong)theSelectedItem.ContentSize);
				}
			}
			//Dim contentFileSizeMaxText As String = "<unknown>"
			//'Dim contentFileSizeMax As Integer = CInt(Steamworks.Constants.k_unMaxCloudFileChunkSize / 1048576)
			//'contentFileSizeMaxText = contentFileSizeMax.ToString()
			string changedMarker = "";
			if (theSelectedItem.ContentPathFolderOrFileNameIsChanged && !theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			//NOTE: Not sure what max size is, so do not show it.
			//Me.ItemContentFolderOrFileLabel.Text += changedMarker + " (" + contentFileSizeText + " / " + contentFileSizeMaxText + " MB max):"
			ItemContentFolderOrFileLabel.Text += changedMarker;
			if (contentFileSizeText != "0")
			{
				ItemContentFolderOrFileLabel.Text += " (" + contentFileSizeText + ")";
			}
			ItemContentFolderOrFileLabel.Text += ":";
		}

		private void UpdateItemPreviewImageLabel()
		{
			string previewImageSizeText = "0";
			if (File.Exists(theSelectedItem.PreviewImagePathFileName))
			{
				FileInfo aFile = new FileInfo(theSelectedItem.PreviewImagePathFileName);
				previewImageSizeText = MathModule.ByteUnitsConversion((ulong)aFile.Length);
			}
			else if (theSelectedItem.PreviewImageSize > 0 && theSelectedItem.IsPublished)
			{
				previewImageSizeText = MathModule.ByteUnitsConversion((ulong)theSelectedItem.PreviewImageSize);
			}
			//Dim previewImageSizeMaxText As String = "<unknown>"
			string changedMarker = "";
			if (theSelectedItem.PreviewImagePathFileNameIsChanged && !theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			//NOTE: Not sure what max size is, so do not show it.
			//Me.ItemPreviewImageLabel.Text = "Preview Image" + changedMarker + " (" + previewImageSizeText + " / " + previewImageSizeMaxText + " MB max):"
			ItemPreviewImageLabel.Text = "Preview Image" + changedMarker;
			if (previewImageSizeText != "0")
			{
				ItemPreviewImageLabel.Text += " (" + previewImageSizeText + ")";
			}
			ItemPreviewImageLabel.Text += ":";
		}

		private void UpdateItemPreviewImageBox()
		{
			if (File.Exists(theSelectedItem.PreviewImagePathFileName))
			{
				try
				{
					if (ItemPreviewImagePictureBox.Image != null)
					{
						ItemPreviewImagePictureBox.Image.Dispose();
					}
					ItemPreviewImagePictureBox.Image = Image.FromFile(theSelectedItem.PreviewImagePathFileName);
				}
				catch (Exception ex)
				{
					// Problem setting Image, so reset the textbox text.
					theSelectedItem.PreviewImagePathFileName = theSavedPreviewImagePathFileName;
				}
			}
			else
			{
				if (ItemPreviewImagePictureBox.Image != null)
				{
					ItemPreviewImagePictureBox.Image.Dispose();
				}
				ItemPreviewImagePictureBox.Image = null;
			}
		}

		//NOTE: When item is changed, save the preview image to a file in Crowbar's appdata folder.
		//      Save using file name like "<ID>_preview_00.bmp" [00 for primary].
		//      Need to delete the file when published or reverted.
		private void SaveCopyOfPreviewImageFile(WorkshopItem item)
		{
			if (ItemPreviewImagePictureBox.Image == null)
			{
				return;
			}

			System.Drawing.Imaging.ImageFormat anImageFormat = ItemPreviewImagePictureBox.Image.RawFormat;
			string previewImagePathFileName = "";
			if (anImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
			{
				previewImagePathFileName = GetPreviewImagePathFileName("temp.gif", item.ID, 0);
			}
			else
			{
				previewImagePathFileName = GetPreviewImagePathFileName("temp.png", item.ID, 0);
				anImageFormat = System.Drawing.Imaging.ImageFormat.Png;
			}
			if (!string.IsNullOrEmpty(previewImagePathFileName))
			{
				try
				{
					ItemPreviewImagePictureBox.Image.Save(previewImagePathFileName, anImageFormat);
				}
				catch (Exception ex)
				{
					if (!File.Exists(previewImagePathFileName))
					{
						LogTextBox.AppendText("ERROR: Crowbar tried to save preview image to temp file \"" + previewImagePathFileName + "\" but Windows gave this message: " + ex.Message);
					}
				}
				if (File.Exists(previewImagePathFileName))
				{
					bool selectedItemDetailsIsChangingViaMe = theSelectedItemDetailsIsChangingViaMe;
					bool selectedItemPreviewImagePathFileNameIsChanged = theSelectedItem.PreviewImagePathFileNameIsChanged;
					theSelectedItemDetailsIsChangingViaMe = true;

					theSelectedItem.PreviewImagePathFileName = previewImagePathFileName;

					theSelectedItemDetailsIsChangingViaMe = selectedItemDetailsIsChangingViaMe;
					theSelectedItem.PreviewImagePathFileNameIsChanged = selectedItemPreviewImagePathFileNameIsChanged;
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
					if (ItemPreviewImagePictureBox.Image != null)
					{
						ItemPreviewImagePictureBox.Image.Dispose();
					}
					ItemPreviewImagePictureBox.Image = new Bitmap(img);
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

			DeleteTempPreviewImageFile(itemPreviewImagePathFileName, itemID);
		}

		private void DeleteTempPreviewImageFile(string itemPreviewImagePathFileName, string itemID)
		{
			string previewImagePathFileName = GetPreviewImagePathFileName(itemPreviewImagePathFileName, itemID, 0);
			if (!string.IsNullOrEmpty(previewImagePathFileName) && File.Exists(previewImagePathFileName))
			{
				try
				{
					File.Delete(previewImagePathFileName);
				}
				catch (Exception ex)
				{
					LogTextBox.AppendText("ERROR: Crowbar tried to delete an old temp file \"" + previewImagePathFileName + "\" but Windows gave this message: " + ex.Message);
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
			if (theSelectedItem.VisibilityIsChanged && !theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			ItemVisibilityLabel.Text = "Visibility" + changedMarker + ":";
		}

		private void UpdateItemTagsLabel()
		{
			string changedMarker = "";
			if (theSelectedItem.TagsIsChanged && !theSelectedItem.IsDraft)
			{
				changedMarker = AppConstants.ChangedMarker;
			}
			ItemTagsGroupBox.Text = "Tags" + changedMarker;
		}

		private void UpdateItemDetailButtons()
		{
			if (theUserSteamID == 0)
			{
				GetUserSteamID();
			}

			bool editableNonTextWidgetsAreEnabled = (theSelectedItem.IsDraft) || (theSelectedItem.IsTemplate) || (theSelectedItem.OwnerID == theUserSteamID);

			BrowseItemContentPathFileNameButton.Enabled = editableNonTextWidgetsAreEnabled;
			BrowseItemPreviewImagePathFileNameButton.Enabled = editableNonTextWidgetsAreEnabled;

			SaveAsTemplateOrDraftItemButton.Enabled = true;
			if (theSelectedItem.IsTemplate)
			{
				SaveAsTemplateOrDraftItemButton.Text = "Save as Draft";
			}
			else
			{
				SaveAsTemplateOrDraftItemButton.Text = "Save as Template";
			}

			//Me.RefreshOrRevertItemButton.Visible = True
			//Me.RefreshOrRevertItemButton.Enabled = (Me.theSelectedItem.ID <> "" AndAlso Not Me.theSelectedItem.IsDraft)
			RefreshOrRevertItemButton.Enabled = (theSelectedItem.IsPublished) || (theSelectedItem.IsTemplate && theSelectedItem.IsChanged);
			if ((theSelectedItem.IsTemplate) || (theSelectedItem.IsPublished && theSelectedItem.IsChanged))
			{
				RefreshOrRevertItemButton.Text = "Revert";
			}
			else
			{
				RefreshOrRevertItemButton.Text = "Refresh";
			}

			OpenWorkshopPageButton.Visible = (!theSelectedItem.IsTemplate);
			OpenWorkshopPageButton.Enabled = (!theSelectedItem.IsDraft);

			SaveTemplateButton.Visible = (theSelectedItem.IsTemplate);
			SaveTemplateButton.Enabled = (theSelectedItem.IsChanged);

			DeleteItemButton.Enabled = editableNonTextWidgetsAreEnabled;

			//NOTE: SteamRemoteStorage_PublishWorkshopFile requires Item to have a Title, a Description, a Content File, and a Preview Image.
			PublishItemButton.Enabled = (((theSelectedItem.IsDraft) && (!string.IsNullOrEmpty(theSelectedItem.Title) && !string.IsNullOrEmpty(theSelectedItem.Description) && !string.IsNullOrEmpty(theSelectedItem.ContentPathFolderOrFileName) && !string.IsNullOrEmpty(theSelectedItem.PreviewImagePathFileName))) || (theSelectedItem.IsChanged && (theUserSteamID == theSelectedItem.OwnerID)) || (theSelectedItem.IsTemplate));
		}

		private void SwapBetweenOwnerNameAndID()
		{
			if (ItemOwnerTextBox.DataBindings["Text"].BindingMemberInfo.BindingMember == "OwnerName")
			{
				ItemOwnerTextBox.DataBindings.Remove(ItemOwnerTextBox.DataBindings["Text"]);
				ItemOwnerTextBox.DataBindings.Add("Text", theItemBindingSource, "OwnerID", false, DataSourceUpdateMode.OnValidation);
			}
			else
			{
				ItemOwnerTextBox.DataBindings.Remove(ItemOwnerTextBox.DataBindings["Text"]);
				ItemOwnerTextBox.DataBindings.Add("Text", theItemBindingSource, "OwnerName", false, DataSourceUpdateMode.OnValidation);
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

			if (theSteamAppInfo.CanUseContentFolderOrFile || theSteamAppInfo.UsesSteamUGC)
			{
				if (theSteamAppInfo.ContentFileExtensionsAndDescriptions.Count > 0)
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
			if (File.Exists(theSelectedItem.ContentPathFolderOrFileName))
			{
				openFileWdw.InitialDirectory = FileManager.GetPath(theSelectedItem.ContentPathFolderOrFileName);
			}
			else
			{
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(theSelectedItem.ContentPathFolderOrFileName);
				if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
				{
					openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}

			//NOTE: Must use temp string because openFileWdw.Filter validates itself on assignment.
			string fileFilter = "";
			for (int i = 0; i < theSteamAppInfo.ContentFileExtensionsAndDescriptions.Count; i++)
			{
				fileFilter += theSteamAppInfo.ContentFileExtensionsAndDescriptions.Values[i];
				fileFilter += " (*.";
				fileFilter += theSteamAppInfo.ContentFileExtensionsAndDescriptions.Keys[i];
				fileFilter += ")|*.";
				fileFilter += theSteamAppInfo.ContentFileExtensionsAndDescriptions.Keys[i];
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

				if (theSteamAppInfo.CanUseContentFolderOrFile || theSteamAppInfo.UsesSteamUGC)
				{
					if (Path.GetFileNameWithoutExtension(openFileWdw.FileName) == "[Folder Selection]")
					{
						theSelectedItem.ContentPathFolderOrFileName = FileManager.GetPath(openFileWdw.FileName);
					}
					else
					{
						theSelectedItem.ContentPathFolderOrFileName = openFileWdw.FileName;
					}
				}
				else
				{
					theSelectedItem.ContentPathFolderOrFileName = openFileWdw.FileName;
				}
				UpdateItemContentLabel();
			}
		}

		private void BrowseForPreviewImage()
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			openFileWdw.Title = "Open the image file you want to use for preview image";
			if (File.Exists(theSelectedItem.PreviewImagePathFileName))
			{
				openFileWdw.InitialDirectory = FileManager.GetPath(theSelectedItem.PreviewImagePathFileName);
			}
			else
			{
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(theSelectedItem.PreviewImagePathFileName);
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
				theSavedPreviewImagePathFileName = theSelectedItem.PreviewImagePathFileName;
				//NOTE: Changing the file name field also changes the preview picturebox.
				theSelectedItem.PreviewImagePathFileName = openFileWdw.FileName;
			}
		}

		//- If draft, then change draft to template.
		//X If template. This should not occur.
		//- If changed or published, then copy item as template, add it to list, and select it in list.
		private void SaveItemAsTemplate()
		{
			WorkshopItem anItem = null;
			if (theSelectedItem.IsDraft)
			{
				anItem = theSelectedItem;
			}
			else
			{
				anItem = (WorkshopItem)theSelectedItem.Clone();
			}

			theSelectedItemDetailsIsChangingViaMe = true;
			anItem.IsTemplate = true;
			//anItem.ContentPathFolderOrFileName = ""
			//anItem.PreviewImagePathFileName = ""
			anItem.IsChanged = false;
			//NOTE: Without this line, the ID field in the widgets does not change until a different item is selected.
			theItemBindingSource.ResetCurrentItem();
			theSelectedItemDetailsIsChangingViaMe = false;
			//Me.theTemplateItemTotalCount += 1UI

			if (anItem != theSelectedItem)
			{
				theDisplayedItems.Add(anItem);
				theEntireListOfItems.Add(anItem);
				theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(anItem);
				SaveCopyOfPreviewImageFile(anItem);
				SelectItemInGrid(ItemsDataGridView.Rows.Count - 1);
			}

			UpdateItemListWidgets(false);
			UpdateItemDetails();
		}

		private void RefreshOrRevertItem()
		{
			if (theSelectedItem.IsChanged)
			{
				if (theSelectedItem.IsTemplate)
				{
					//Me.theSelectedItemDetailsIsChangingViaMe = True
					//'NOTE: Change the item in the list (and not the Me.theSelectedItem) so that list and selected item stay synced.
					//Dim selectedItemIndex As Integer = Me.theItemBindingSource.IndexOf(Me.theSelectedItem)
					//Me.theDisplayedItems(selectedItemIndex) = Me.theUnchangedSelectedTemplateItem
					//Me.theSelectedItemDetailsIsChangingViaMe = False
					RevertChangedTemplate();
				}
				else if (theSelectedItem.IsPublished)
				{
					ChangeChangedItemIntoPublishedItem(theSelectedItem);
				}
			}
			UpdateItemDetails();
		}

		private void OpenWorkshopPage()
		{
			System.Diagnostics.Process.Start(theWorkshopPageLink);
		}

		private void SaveTemplate()
		{
			theSelectedItemDetailsIsChangingViaMe = true;
			theSelectedItem.IsChanged = false;
			theSelectedItemDetailsIsChangingViaMe = false;
			//Me.UpdateItemListWidgets(False)
			UpdateItemDetails();
		}

		private void DeleteItem()
		{
			DeleteItemForm deleteItemWindow = new DeleteItemForm();
			if (theSelectedItem.IsPublished)
			{
				deleteItemWindow.TextBox1.Text = "Deleting will remove the item from the Workshop permanently." + "\r\n" + "Backup anything you want to save before deleting.";
				if (deleteItemWindow.ShowDialog() == DialogResult.OK)
				{
					DeletePublishedItemFromWorkshop();
				}
			}
			else
			{
				deleteItemWindow.TextBox1.Text = "Deleting will remove the item from your saved items permanently." + "\r\n" + "Backup anything you want to save before deleting.";
				if (deleteItemWindow.ShowDialog() == DialogResult.OK)
				{
					UpdateAfterDeleteItem();
				}
			}
		}

		private void UpdateAfterDeleteItem()
		{
			//NOTE: Need to make a temp variable because Me.theSelectedItem will change between function calls.
			WorkshopItem deletedItem = theSelectedItem;
			//If deletedItem.IsTemplate Then
			//	Me.theTemplateItemTotalCount -= 1UI
			//ElseIf Not deletedItem.IsDraft AndAlso deletedItem.IsChanged Then
			//	Me.theChangedItemTotalCount -= 1UI
			//Else
			//End If
			//NOTE: No exception is raised if item is not in any of these lists.
			theDisplayedItems.Remove(deletedItem);
			theEntireListOfItems.Remove(deletedItem);
			theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(deletedItem);
			UpdateItemListWidgets(false);
			SelectItemInGrid();

			GetUserSteamAppCloudQuota();
		}

		private void DeletePublishedItemFromWorkshop()
		{
			AppIdComboBox.Enabled = false;
			ItemsDataGridView.Enabled = false;
			ItemGroupBox.Enabled = false;
			//Me.DeleteItemButton.Enabled = False
			PublishItemButton.Enabled = false;
			if (!string.IsNullOrEmpty(LogTextBox.Text))
			{
				LogTextBox.AppendText("------" + "\r\n");
			}

			theBackgroundSteamPipe.DeletePublishedItemFromWorkshop(DeletePublishedItemFromWorkshop_ProgressChanged, DeletePublishedItemFromWorkshop_RunWorkerCompleted, theSelectedItem.ID);
		}

		private void GetPublishedItemDetailsViaSteamRemoteStorage(string itemID, string action)
		{
			AppIdComboBox.Enabled = false;
			ItemsDataGridView.Enabled = false;
			ItemGroupBox.Enabled = false;
			PublishItemButton.Enabled = false;
			if (!string.IsNullOrEmpty(LogTextBox.Text))
			{
				LogTextBox.AppendText("------" + "\r\n");
			}

			theSelectedItemDetailsIsChangingViaMe = true;

			BackgroundSteamPipe.GetPublishedFileDetailsInputInfo input = new BackgroundSteamPipe.GetPublishedFileDetailsInputInfo(itemID, theSteamAppId.ToString(), action);
			theBackgroundSteamPipe.GetPublishedItemDetails(GetPublishedItemDetails_ProgressChanged, GetPublishedItemDetails_RunWorkerCompleted, input);
		}

		private void PublishItem()
		{
			if (theSelectedItem.IsTemplate)
			{
				SaveChangedTemplateToDraft();
				SelectItemInGrid(ItemsDataGridView.Rows.Count - 1);
			}

			//NOTE: Need to do this after the template-to-draft change above.
			AppIdComboBox.Enabled = false;
			ItemsDataGridView.Enabled = false;
			ItemGroupBox.Enabled = false;
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
			PublishItemButton.Enabled = false;
			if (!string.IsNullOrEmpty(LogTextBox.Text))
			{
				LogTextBox.AppendText("------" + "\r\n");
			}

			bool prePublishChecksAreSuccessful = true;
			if (theSelectedItem.ContentPathFolderOrFileNameIsChanged)
			{
				if (theSteamAppInfo.CanUseContentFolderOrFile)
				{
					if (!Directory.Exists(theSelectedItem.ContentPathFolderOrFileName) && !File.Exists(theSelectedItem.ContentPathFolderOrFileName))
					{
						LogTextBox.AppendText("ERROR: Item content folder or file does not exist." + "\r\n");
						prePublishChecksAreSuccessful = false;
					}
				}
				else if (theSteamAppInfo.UsesSteamUGC)
				{
					if (!Directory.Exists(theSelectedItem.ContentPathFolderOrFileName))
					{
						LogTextBox.AppendText("ERROR: Item content folder does not exist." + "\r\n");
						prePublishChecksAreSuccessful = false;
					}
				}
				else
				{
					if (!File.Exists(theSelectedItem.ContentPathFolderOrFileName))
					{
						LogTextBox.AppendText("ERROR: Item content file does not exist." + "\r\n");
						prePublishChecksAreSuccessful = false;
					}
				}
			}
			if (theSelectedItem.PreviewImagePathFileNameIsChanged)
			{
				if (!File.Exists(theSelectedItem.PreviewImagePathFileName))
				{
					LogTextBox.AppendText("ERROR: Item preview image file does not exist." + "\r\n");
					prePublishChecksAreSuccessful = false;
				}
			}
			if (!prePublishChecksAreSuccessful)
			{
				//Me.UpdateItemDetailWidgets()
				UpdateWidgetsAfterPublish();
				return;
			}

			theSelectedItemDetailsIsChangingViaMe = true;

			BackgroundSteamPipe.PublishItemInputInfo inputInfo = new BackgroundSteamPipe.PublishItemInputInfo();
			inputInfo.AppInfo = theSteamAppInfo;
			inputInfo.Item = theSelectedItem;
			theBackgroundSteamPipe.PublishItem(PublishItem_ProgressChanged, PublishItem_RunWorkerCompleted, inputInfo);
		}

		private void UpdateWidgetsAfterPublish()
		{
			AppIdComboBox.Enabled = true;
			ItemsDataGridView.Enabled = true;
			ItemTitleTextBox.Enabled = true;
			ItemDescriptionTextBox.Enabled = true;
			ItemChangeNoteTextBox.Enabled = true;
			ItemContentPathFileNameTextBox.Enabled = true;
			ItemPreviewImagePathFileNameTextBox.Enabled = true;
			ItemTagsGroupBox.Enabled = true;
			UpdateItemDetailWidgets();
		}

		private void OpenAgreementRequiresAcceptanceWindow()
		{
			AgreementRequiresAcceptanceForm agreementRequiresAcceptanceWindow = new AgreementRequiresAcceptanceForm();
			if (agreementRequiresAcceptanceWindow.ShowDialog() == DialogResult.OK)
			{
				OpenSteamSubscriberAgreement();
			}
			else if (agreementRequiresAcceptanceWindow.ShowDialog() == DialogResult.Ignore)
			{
				OpenWorkshopPage();
			}
		}

		private void SaveChangedTemplateToDraft()
		{
			AddDraftItem(theSelectedItem);
			if (theSelectedItem.IsChanged)
			{
				RevertChangedTemplate();
			}
		}

		private void RevertChangedTemplate()
		{
			theSelectedItemDetailsIsChangingViaMe = true;
			//NOTE: Change the item in the list (and not the Me.theSlectedItem) so that list and selected item stay synced.
			int selectedItemIndex = theItemBindingSource.IndexOf(theSelectedItem);
			theDisplayedItems[selectedItemIndex] = theUnchangedSelectedTemplateItem;
			theSteamAppUserInfo.DraftTemplateAndChangedItems.Remove(theSelectedItem);
			theSteamAppUserInfo.DraftTemplateAndChangedItems.Add(theUnchangedSelectedTemplateItem);
			theSelectedItem.PropertyChanged -= WorkshopItem_PropertyChanged;
			theSelectedItem = theUnchangedSelectedTemplateItem;
			theSelectedItem.PropertyChanged += WorkshopItem_PropertyChanged;
			theSelectedItemDetailsIsChangingViaMe = false;
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
			int width = ItemPreviewImagePictureBox.Width;
			int height = ItemPreviewImagePictureBox.Height;
			if (width != height)
			{
				int length = Math.Min(width, height);
				ItemPreviewImagePictureBox.Size = new System.Drawing.Size(length, length);
			}
		}

#endregion

	}

}