//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.Xml.Serialization;

namespace Crowbar
{
	public class WorkshopItem : ICloneable, IDisposable, INotifyPropertyChanged
	{
#region Create and Destroy

		public WorkshopItem() : base()
		{
			this.Create();
		}

		private void Create()
		{
			this.theItemIsDisposed = false;

			this.theCreatorAppID = "0";

			this.theID = WorkshopItem.DraftItemIDText;
			this.theOwnerSteamID = 0;
			this.theOwnerName = "";
			this.thePosted = MathModule.DateTimeToUnixTimeStamp(DateTime.Now);
			this.theUpdated = this.thePosted;

			this.theTitle = "";
			this.theTitleIsChanged = false;
			this.theDescription = "";
			this.theDescriptionIsChanged = false;
			this.theChangeNote = "";
			this.theChangeNoteIsChanged = false;

			this.theContentSize = 0;
			this.theContentPathFolderOrFileName = "";
			this.theContentPathFolderOrFileNameIsChanged = false;

			this.thePreviewImageSize = 0;
			this.thePreviewImagePathFileName = "";
			this.thePreviewImagePathFileNameIsChanged = false;

			this.theVisibility = SteamUGCPublishedItemVisibility.Hidden;
			this.theVisibilityIsChanged = false;

			this.theTags = new BindingListEx<string>();
			this.theTagsAsTextLine = "";
			this.theTagsIsChanged = false;

			this.theItemIsChanged = false;
		}

		protected WorkshopItem(WorkshopItem originalObject)
		{
			this.theCreatorAppID = originalObject.theCreatorAppID;

			//NOTE: Clone becomes a draft item.
			this.theID = WorkshopItem.DraftItemIDText;
			this.theOwnerSteamID = originalObject.OwnerID;
			this.theOwnerName = originalObject.OwnerName;
			this.thePosted = originalObject.Posted;
			this.theUpdated = originalObject.Updated;

			this.theTitle = originalObject.Title;
			this.theTitleIsChanged = false;
			this.theDescription = originalObject.Description;
			this.theDescriptionIsChanged = false;
			this.theChangeNote = originalObject.ChangeNote;
			this.theChangeNoteIsChanged = false;

			this.theContentSize = originalObject.ContentSize;
			this.theContentPathFolderOrFileName = originalObject.ContentPathFolderOrFileName;
			this.theContentPathFolderOrFileNameIsChanged = false;

			this.thePreviewImageSize = originalObject.PreviewImageSize;
			this.thePreviewImagePathFileName = originalObject.PreviewImagePathFileName;
			this.thePreviewImagePathFileNameIsChanged = false;

			this.theVisibility = originalObject.Visibility;
			this.theVisibilityIsChanged = false;

			this.theTags = new BindingListEx<string>();
			foreach (string originalTag in originalObject.Tags)
			{
				this.theTags.Add(originalTag);
			}
			this.theTagsAsTextLine = originalObject.TagsAsTextLine;
			this.theTagsIsChanged = false;

			//NOTE: Clone becomes a draft item; thus theItemIsChanged is always False.
			this.theItemIsChanged = false;
		}

		public object Clone()
		{
			return new WorkshopItem(this);
		}

#region IDisposable Support

		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.theItemIsDisposed)
			{
				if (disposing)
				{
					this.Free();
				}
				//NOTE: free shared unmanaged resources
			}
			this.theItemIsDisposed = true;
		}

		//Protected Overrides Sub Finalize()
		//	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		//	Dispose(False)
		//	MyBase.Finalize()
		//End Sub

#endregion

#endregion

#region Init and Free

		//Private Sub Init()
		//End Sub

		private void Free()
		{
			//RemoveHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
		}

#endregion

#region Properties

		public string CreatorAppID
		{
			get
			{
				return this.theCreatorAppID;
			}
			set
			{
				if (this.theCreatorAppID != value)
				{
					this.theCreatorAppID = value;
					NotifyPropertyChanged("CreatorAppID");
				}
			}
		}

		public string ID
		{
			get
			{
				return this.theID;
			}
			set
			{
				if (this.theID != value)
				{
					this.theID = value;
					NotifyPropertyChanged("ID");
				}
			}
		}

		public ulong OwnerID
		{
			get
			{
				return this.theOwnerSteamID;
			}
			set
			{
				if (this.theOwnerSteamID != value)
				{
					this.theOwnerSteamID = value;
					NotifyPropertyChanged("OwnerID");
				}
			}
		}

		public string OwnerName
		{
			get
			{
				return this.theOwnerName;
			}
			set
			{
				if (this.theOwnerName != value)
				{
					this.theOwnerName = value;
					NotifyPropertyChanged("OwnerName");
				}
			}
		}

		// UnixTimeStamp
		public long Posted
		{
			get
			{
				return this.thePosted;
			}
			set
			{
				if (this.thePosted != value)
				{
					this.thePosted = value;
					NotifyPropertyChanged("Posted");
				}
			}
		}

		// UnixTimeStamp
		public long Updated
		{
			get
			{
				return this.theUpdated;
			}
			set
			{
				if (this.theUpdated != value)
				{
					this.theUpdated = value;
					NotifyPropertyChanged("Updated");
				}
			}
		}

		public string Title
		{
			get
			{
				return this.theTitle;
			}
			set
			{
				if (this.theTitle != value)
				{
					this.theTitle = value;
					this.theTitleIsChanged = true;
					NotifyPropertyChanged("Title");
				}
			}
		}

		public bool TitleIsChanged
		{
			get
			{
				return this.theTitleIsChanged;
			}
			set
			{
				if (this.theTitleIsChanged != value)
				{
					this.theTitleIsChanged = value;
				}
			}
		}

		public string Description
		{
			get
			{
				return this.theDescription;
			}
			set
			{
				if (this.theDescription != value)
				{
					this.theDescription = value;
					this.theDescriptionIsChanged = true;
					NotifyPropertyChanged("Description");
				}
			}
		}

		public bool DescriptionIsChanged
		{
			get
			{
				return this.theDescriptionIsChanged;
			}
			set
			{
				if (this.theDescriptionIsChanged != value)
				{
					this.theDescriptionIsChanged = value;
				}
			}
		}

		public string ChangeNote
		{
			get
			{
				return this.theChangeNote;
			}
			set
			{
				if (this.theChangeNote != value)
				{
					this.theChangeNote = value;
					this.theChangeNoteIsChanged = true;
					NotifyPropertyChanged("ChangeNote");
				}
			}
		}

		public bool ChangeNoteIsChanged
		{
			get
			{
				return this.theChangeNoteIsChanged;
			}
			set
			{
				if (this.theChangeNoteIsChanged != value)
				{
					this.theChangeNoteIsChanged = value;
				}
			}
		}

		public int ContentSize
		{
			get
			{
				return this.theContentSize;
			}
			set
			{
				if (this.theContentSize != value)
				{
					this.theContentSize = value;
					NotifyPropertyChanged("ContentSize");
				}
			}
		}

		public string ContentPathFolderOrFileName
		{
			get
			{
				return this.theContentPathFolderOrFileName;
			}
			set
			{
				if (this.theContentPathFolderOrFileName != value)
				{
					this.theContentPathFolderOrFileName = value;
					this.theContentPathFolderOrFileNameIsChanged = true;
					NotifyPropertyChanged("ContentPathFolderOrFileName");
				}
			}
		}

		public bool ContentPathFolderOrFileNameIsChanged
		{
			get
			{
				return this.theContentPathFolderOrFileNameIsChanged;
			}
			set
			{
				if (this.theContentPathFolderOrFileNameIsChanged != value)
				{
					this.theContentPathFolderOrFileNameIsChanged = value;
				}
			}
		}

		public int PreviewImageSize
		{
			get
			{
				return this.thePreviewImageSize;
			}
			set
			{
				if (this.thePreviewImageSize != value)
				{
					this.thePreviewImageSize = value;
					NotifyPropertyChanged("PreviewImageSize");
				}
			}
		}

		public string PreviewImagePathFileName
		{
			get
			{
				return this.thePreviewImagePathFileName;
			}
			set
			{
				if (this.thePreviewImagePathFileName != value)
				{
					this.thePreviewImagePathFileName = value;
					this.thePreviewImagePathFileNameIsChanged = true;
					NotifyPropertyChanged("PreviewImagePathFileName");
				}
			}
		}

		public bool PreviewImagePathFileNameIsChanged
		{
			get
			{
				return this.thePreviewImagePathFileNameIsChanged;
			}
			set
			{
				if (this.thePreviewImagePathFileNameIsChanged != value)
				{
					this.thePreviewImagePathFileNameIsChanged = value;
				}
			}
		}

		public SteamUGCPublishedItemVisibility Visibility
		{
			get
			{
				return this.theVisibility;
			}
			set
			{
				if (this.theVisibility != value)
				{
					this.theVisibility = value;
					this.theVisibilityIsChanged = true;
					NotifyPropertyChanged("Visibility");
				}
			}
		}

		[XmlIgnore()]
		public string VisibilityText
		{
			get
			{
				return this.theVisibility.ToString();
			}
			set
			{
				if (this.theVisibility.ToString() != value)
				{
					this.theVisibility = (SteamUGCPublishedItemVisibility)Enum.Parse(typeof(SteamUGCPublishedItemVisibility), value);
					this.theVisibilityIsChanged = true;
					NotifyPropertyChanged("Visibility");
				}
			}
		}

		public bool VisibilityIsChanged
		{
			get
			{
				return this.theVisibilityIsChanged;
			}
			set
			{
				if (this.theVisibilityIsChanged != value)
				{
					this.theVisibilityIsChanged = value;
				}
			}
		}

		public BindingListEx<string> Tags
		{
			get
			{
				return this.theTags;
			}
			set
			{
				string givenTagsAsTextLine = "";
				foreach (string word in value)
				{
					givenTagsAsTextLine += word + ",";
				}
				givenTagsAsTextLine.TrimEnd(',');

				if (this.theTagsAsTextLine != givenTagsAsTextLine)
				{
					this.theTags = value;
					this.theTagsAsTextLine = givenTagsAsTextLine;
					this.theTagsIsChanged = true;
					//NOTE: This line raises exception, possibly because the DataGridView gets confused by the property being a list, so use "TagsAsTextLine" property.
					//System.ArgumentOutOfRangeException
					//  HResult=0x80131502
					//  Message=Specified argument was out of the range of valid values.
					//Parameter name: rowIndex
					//  Source=System.Windows.Forms
					//  StackTrace:
					//   at System.Windows.Forms.DataGridView.InvalidateCell(Int32 columnIndex, Int32 rowIndex)
					//   at System.Windows.Forms.DataGridView.DataGridViewDataConnection.ProcessListChanged(ListChangedEventArgs e)
					//   at System.Windows.Forms.DataGridView.DataGridViewDataConnection.currencyManager_ListChanged(Object sender, ListChangedEventArgs e)
					//   at System.Windows.Forms.CurrencyManager.OnListChanged(ListChangedEventArgs e)
					//   at System.Windows.Forms.CurrencyManager.List_ListChanged(Object sender, ListChangedEventArgs e)
					//   at System.Windows.Forms.BindingSource.OnListChanged(ListChangedEventArgs e)
					//   at System.Windows.Forms.BindingSource.InnerList_ListChanged(Object sender, ListChangedEventArgs e)
					//   at System.ComponentModel.BindingList`1.OnListChanged(ListChangedEventArgs e)
					//   at System.ComponentModel.BindingList`1.Child_PropertyChanged(Object sender, PropertyChangedEventArgs e)
					//   at System.ComponentModel.PropertyChangedEventHandler.Invoke(Object sender, PropertyChangedEventArgs e)
					//   at Crowbar.WorkshopItem.NotifyPropertyChanged(String info) in E:\Users\ZeqMacaw\Documents\Visual Studio 2017\Projects\CrowbarSteamworks\CrowbarSteamworks\Core\WorkshopItem.vb:line 500
					//   at Crowbar.WorkshopItem.set_Tags(BindingListEx`1 value) in E:\Users\ZeqMacaw\Documents\Visual Studio 2017\Projects\CrowbarSteamworks\CrowbarSteamworks\Core\WorkshopItem.vb:line 423
					//   at Crowbar.PublishUserControl.UpdateItemDetails() in E:\Users\ZeqMacaw\Documents\Visual Studio 2017\Projects\CrowbarSteamworks\CrowbarSteamworks\Widgets\Main Tabs\PublishUserControl.vb:line 897
					//NotifyPropertyChanged("Tags")
					//RaiseEvent TagsPropertyChanged(Me, New PropertyChangedEventArgs("Tags"))
					//RemoveHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
					//AddHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
					NotifyPropertyChanged("TagsAsTextLine");
				}
			}
		}

		[XmlIgnore()]
		public string TagsAsTextLine
		{
			get
			{
				return this.theTagsAsTextLine;
			}
			set
			{
				if (this.theTagsAsTextLine != value)
				{
					this.theTags = new BindingListEx<string>();
					char[] charSeparators = {','};
					string[] words = value.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
					foreach (string word in words)
					{
						this.theTags.Add(word);
					}
					this.theTagsAsTextLine = value;
					this.theTagsIsChanged = true;
					//NOTE: This line raises exception, possibly because the DataGridView gets confused by the property being a list, so use "TagsAsTextLine" property.
					//NotifyPropertyChanged("Tags")
					//RaiseEvent TagsPropertyChanged(Me, New PropertyChangedEventArgs("Tags"))
					//RemoveHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
					//AddHandler Me.theTags.ListChanged, AddressOf Me.Tags_ListChanged
					NotifyPropertyChanged("TagsAsTextLine");
				}
			}
		}

		public bool TagsIsChanged
		{
			get
			{
				return this.theTagsIsChanged;
			}
			set
			{
				if (this.theTagsIsChanged != value)
				{
					this.theTagsIsChanged = value;
				}
			}
		}

		[XmlIgnore()]
		public bool IsChanged
		{
			get
			{
				return this.theItemIsChanged;
			}
			set
			{
				//NOTE: Set these whenever set to False and not just when it changes.
				if (!value)
				{
					this.theTitleIsChanged = false;
					this.theDescriptionIsChanged = false;
					this.theChangeNoteIsChanged = false;
					this.theContentPathFolderOrFileNameIsChanged = false;
					this.thePreviewImagePathFileNameIsChanged = false;
					this.theVisibilityIsChanged = false;
					this.theTagsIsChanged = false;
				}

				if (this.theItemIsChanged != value)
				{
					this.theItemIsChanged = value;
					NotifyPropertyChanged("IsChanged");
				}
			}
		}

		[XmlIgnore()]
		public bool IsDraft
		{
			get
			{
				return this.theID == WorkshopItem.DraftItemIDText;
			}
		}

		[XmlIgnore()]
		public bool IsTemplate
		{
			get
			{
				return this.theID == WorkshopItem.TemplateItemIDText;
			}
			set
			{
				this.theID = WorkshopItem.TemplateItemIDText;
			}
		}

		[XmlIgnore()]
		public bool IsPublished
		{
			get
			{
				return this.theID != WorkshopItem.DraftItemIDText && this.theID != WorkshopItem.TemplateItemIDText;
			}
		}

#endregion

#region Methods

		public void SetAllChangedForNonEmptyFields()
		{
			if (!string.IsNullOrEmpty(this.theTitle))
			{
				this.theTitleIsChanged = true;
			}
			if (!string.IsNullOrEmpty(this.theDescription))
			{
				this.theDescriptionIsChanged = true;
			}
			if (!string.IsNullOrEmpty(this.theChangeNote))
			{
				this.theChangeNoteIsChanged = true;
			}
			if (!string.IsNullOrEmpty(this.theContentPathFolderOrFileName))
			{
				this.theContentPathFolderOrFileNameIsChanged = true;
			}
			if (!string.IsNullOrEmpty(this.thePreviewImagePathFileName))
			{
				this.thePreviewImagePathFileNameIsChanged = true;
			}

			//NOTE: Always set IsChanged for Visibility and Tags.
			this.theVisibilityIsChanged = true;
			this.theTagsIsChanged = true;

			//NOTE: Always set IsChanged for item.
			this.theItemIsChanged = true;
		}

#endregion

#region Event Handlers

		//Private Sub Tags_ListChanged(sender As Object, e As ListChangedEventArgs)
		//	'If e.ListChangedType = ListChangedType.ItemDeleted AndAlso e.OldIndex = -2 Then
		//	'	'NOTE: Ignore the "pre-delete" event.
		//	'	Exit Sub
		//	'End If
		//	'If e.ListChangedType = ListChangedType.ItemChanged Then
		//	'End If
		//	RaiseEvent TagsPropertyChanged(Me, New PropertyChangedEventArgs("Tags"))
		//End Sub

#endregion

#region Events

		public event PropertyChangedEventHandler PropertyChanged;
		//Public Event TagsPropertyChanged As PropertyChangedEventHandler

#endregion

#region Private Methods

		protected void NotifyPropertyChanged(string info)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(info));
		}

#endregion

#region Constants

		public enum SteamUGCPublishedItemVisibility
		{
			//<Description("<no change>")> NoChange = -1
			[Description("Public")]
			Public = 0,
			[Description("Friends-Only")]
			FriendsOnly = 1,
			[Description("Hidden")]
			Hidden = 2,
			[Description("Unlisted")]
			Unlisted = 3
		}

#endregion

#region Data

		private const string DraftItemIDText = "<draft>";
		private const string TemplateItemIDText = "<template>";

		private bool theItemIsDisposed;

		private string theCreatorAppID;

		private string theID;
		private ulong theOwnerSteamID;
		private string theOwnerName;
		private long thePosted;
		private long theUpdated;

		private string theTitle;
		private bool theTitleIsChanged;
		private string theDescription;
		private bool theDescriptionIsChanged;
		private string theChangeNote;
		private bool theChangeNoteIsChanged;

		private int theContentSize;
		private string theContentPathFolderOrFileName;
		private bool theContentPathFolderOrFileNameIsChanged;

		private int thePreviewImageSize;
		private string thePreviewImagePathFileName;
		private bool thePreviewImagePathFileNameIsChanged;

		private SteamUGCPublishedItemVisibility theVisibility;
		private bool theVisibilityIsChanged;

		private BindingListEx<string> theTags;
		private string theTagsAsTextLine;
		private bool theTagsIsChanged;

		private bool theItemIsChanged;

#endregion

	}

}