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
			Create();
		}

		private void Create()
		{
			theItemIsDisposed = false;

			theCreatorAppID = "0";

			theID = WorkshopItem.DraftItemIDText;
			theOwnerSteamID = 0;
			theOwnerName = "";
			thePosted = MathModule.DateTimeToUnixTimeStamp(DateTime.Now);
			theUpdated = thePosted;

			theTitle = "";
			theTitleIsChanged = false;
			theDescription = "";
			theDescriptionIsChanged = false;
			theChangeNote = "";
			theChangeNoteIsChanged = false;

			theContentSize = 0;
			theContentPathFolderOrFileName = "";
			theContentPathFolderOrFileNameIsChanged = false;

			thePreviewImageSize = 0;
			thePreviewImagePathFileName = "";
			thePreviewImagePathFileNameIsChanged = false;

			theVisibility = SteamUGCPublishedItemVisibility.Hidden;
			theVisibilityIsChanged = false;

			theTags = new BindingListEx<string>();
			theTagsAsTextLine = "";
			theTagsIsChanged = false;

			theItemIsChanged = false;
		}

		protected WorkshopItem(WorkshopItem originalObject)
		{
			theCreatorAppID = originalObject.theCreatorAppID;

			//NOTE: Clone becomes a draft item.
			theID = WorkshopItem.DraftItemIDText;
			theOwnerSteamID = originalObject.OwnerID;
			theOwnerName = originalObject.OwnerName;
			thePosted = originalObject.Posted;
			theUpdated = originalObject.Updated;

			theTitle = originalObject.Title;
			theTitleIsChanged = false;
			theDescription = originalObject.Description;
			theDescriptionIsChanged = false;
			theChangeNote = originalObject.ChangeNote;
			theChangeNoteIsChanged = false;

			theContentSize = originalObject.ContentSize;
			theContentPathFolderOrFileName = originalObject.ContentPathFolderOrFileName;
			theContentPathFolderOrFileNameIsChanged = false;

			thePreviewImageSize = originalObject.PreviewImageSize;
			thePreviewImagePathFileName = originalObject.PreviewImagePathFileName;
			thePreviewImagePathFileNameIsChanged = false;

			theVisibility = originalObject.Visibility;
			theVisibilityIsChanged = false;

			theTags = new BindingListEx<string>();
			foreach (string originalTag in originalObject.Tags)
			{
				theTags.Add(originalTag);
			}
			theTagsAsTextLine = originalObject.TagsAsTextLine;
			theTagsIsChanged = false;

			//NOTE: Clone becomes a draft item; thus theItemIsChanged is always False.
			theItemIsChanged = false;
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
			if (!theItemIsDisposed)
			{
				if (disposing)
				{
					Free();
				}
				//NOTE: free shared unmanaged resources
			}
			theItemIsDisposed = true;
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
				return theCreatorAppID;
			}
			set
			{
				if (theCreatorAppID != value)
				{
					theCreatorAppID = value;
					NotifyPropertyChanged("CreatorAppID");
				}
			}
		}

		public string ID
		{
			get
			{
				return theID;
			}
			set
			{
				if (theID != value)
				{
					theID = value;
					NotifyPropertyChanged("ID");
				}
			}
		}

		public ulong OwnerID
		{
			get
			{
				return theOwnerSteamID;
			}
			set
			{
				if (theOwnerSteamID != value)
				{
					theOwnerSteamID = value;
					NotifyPropertyChanged("OwnerID");
				}
			}
		}

		public string OwnerName
		{
			get
			{
				return theOwnerName;
			}
			set
			{
				if (theOwnerName != value)
				{
					theOwnerName = value;
					NotifyPropertyChanged("OwnerName");
				}
			}
		}

		// UnixTimeStamp
		public long Posted
		{
			get
			{
				return thePosted;
			}
			set
			{
				if (thePosted != value)
				{
					thePosted = value;
					NotifyPropertyChanged("Posted");
				}
			}
		}

		// UnixTimeStamp
		public long Updated
		{
			get
			{
				return theUpdated;
			}
			set
			{
				if (theUpdated != value)
				{
					theUpdated = value;
					NotifyPropertyChanged("Updated");
				}
			}
		}

		public string Title
		{
			get
			{
				return theTitle;
			}
			set
			{
				if (theTitle != value)
				{
					theTitle = value;
					theTitleIsChanged = true;
					NotifyPropertyChanged("Title");
				}
			}
		}

		public bool TitleIsChanged
		{
			get
			{
				return theTitleIsChanged;
			}
			set
			{
				if (theTitleIsChanged != value)
				{
					theTitleIsChanged = value;
				}
			}
		}

		public string Description
		{
			get
			{
				return theDescription;
			}
			set
			{
				if (theDescription != value)
				{
					theDescription = value;
					theDescriptionIsChanged = true;
					NotifyPropertyChanged("Description");
				}
			}
		}

		public bool DescriptionIsChanged
		{
			get
			{
				return theDescriptionIsChanged;
			}
			set
			{
				if (theDescriptionIsChanged != value)
				{
					theDescriptionIsChanged = value;
				}
			}
		}

		public string ChangeNote
		{
			get
			{
				return theChangeNote;
			}
			set
			{
				if (theChangeNote != value)
				{
					theChangeNote = value;
					theChangeNoteIsChanged = true;
					NotifyPropertyChanged("ChangeNote");
				}
			}
		}

		public bool ChangeNoteIsChanged
		{
			get
			{
				return theChangeNoteIsChanged;
			}
			set
			{
				if (theChangeNoteIsChanged != value)
				{
					theChangeNoteIsChanged = value;
				}
			}
		}

		public int ContentSize
		{
			get
			{
				return theContentSize;
			}
			set
			{
				if (theContentSize != value)
				{
					theContentSize = value;
					NotifyPropertyChanged("ContentSize");
				}
			}
		}

		public string ContentPathFolderOrFileName
		{
			get
			{
				return theContentPathFolderOrFileName;
			}
			set
			{
				if (theContentPathFolderOrFileName != value)
				{
					theContentPathFolderOrFileName = value;
					theContentPathFolderOrFileNameIsChanged = true;
					NotifyPropertyChanged("ContentPathFolderOrFileName");
				}
			}
		}

		public bool ContentPathFolderOrFileNameIsChanged
		{
			get
			{
				return theContentPathFolderOrFileNameIsChanged;
			}
			set
			{
				if (theContentPathFolderOrFileNameIsChanged != value)
				{
					theContentPathFolderOrFileNameIsChanged = value;
				}
			}
		}

		public int PreviewImageSize
		{
			get
			{
				return thePreviewImageSize;
			}
			set
			{
				if (thePreviewImageSize != value)
				{
					thePreviewImageSize = value;
					NotifyPropertyChanged("PreviewImageSize");
				}
			}
		}

		public string PreviewImagePathFileName
		{
			get
			{
				return thePreviewImagePathFileName;
			}
			set
			{
				if (thePreviewImagePathFileName != value)
				{
					thePreviewImagePathFileName = value;
					thePreviewImagePathFileNameIsChanged = true;
					NotifyPropertyChanged("PreviewImagePathFileName");
				}
			}
		}

		public bool PreviewImagePathFileNameIsChanged
		{
			get
			{
				return thePreviewImagePathFileNameIsChanged;
			}
			set
			{
				if (thePreviewImagePathFileNameIsChanged != value)
				{
					thePreviewImagePathFileNameIsChanged = value;
				}
			}
		}

		public SteamUGCPublishedItemVisibility Visibility
		{
			get
			{
				return theVisibility;
			}
			set
			{
				if (theVisibility != value)
				{
					theVisibility = value;
					theVisibilityIsChanged = true;
					NotifyPropertyChanged("Visibility");
				}
			}
		}

		[XmlIgnore()]
		public string VisibilityText
		{
			get
			{
				return theVisibility.ToString();
			}
			set
			{
				if (theVisibility.ToString() != value)
				{
					theVisibility = (SteamUGCPublishedItemVisibility)Enum.Parse(typeof(SteamUGCPublishedItemVisibility), value);
					theVisibilityIsChanged = true;
					NotifyPropertyChanged("Visibility");
				}
			}
		}

		public bool VisibilityIsChanged
		{
			get
			{
				return theVisibilityIsChanged;
			}
			set
			{
				if (theVisibilityIsChanged != value)
				{
					theVisibilityIsChanged = value;
				}
			}
		}

		public BindingListEx<string> Tags
		{
			get
			{
				return theTags;
			}
			set
			{
				string givenTagsAsTextLine = "";
				foreach (string word in value)
				{
					givenTagsAsTextLine += word + ",";
				}
				givenTagsAsTextLine.TrimEnd(',');

				if (theTagsAsTextLine != givenTagsAsTextLine)
				{
					theTags = value;
					theTagsAsTextLine = givenTagsAsTextLine;
					theTagsIsChanged = true;
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
				return theTagsAsTextLine;
			}
			set
			{
				if (theTagsAsTextLine != value)
				{
					theTags = new BindingListEx<string>();
					char[] charSeparators = {','};
					string[] words = value.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
					foreach (string word in words)
					{
						theTags.Add(word);
					}
					theTagsAsTextLine = value;
					theTagsIsChanged = true;
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
				return theTagsIsChanged;
			}
			set
			{
				if (theTagsIsChanged != value)
				{
					theTagsIsChanged = value;
				}
			}
		}

		[XmlIgnore()]
		public bool IsChanged
		{
			get
			{
				return theItemIsChanged;
			}
			set
			{
				//NOTE: Set these whenever set to False and not just when it changes.
				if (!value)
				{
					theTitleIsChanged = false;
					theDescriptionIsChanged = false;
					theChangeNoteIsChanged = false;
					theContentPathFolderOrFileNameIsChanged = false;
					thePreviewImagePathFileNameIsChanged = false;
					theVisibilityIsChanged = false;
					theTagsIsChanged = false;
				}

				if (theItemIsChanged != value)
				{
					theItemIsChanged = value;
					NotifyPropertyChanged("IsChanged");
				}
			}
		}

		[XmlIgnore()]
		public bool IsDraft
		{
			get
			{
				return theID == WorkshopItem.DraftItemIDText;
			}
		}

		[XmlIgnore()]
		public bool IsTemplate
		{
			get
			{
				return theID == WorkshopItem.TemplateItemIDText;
			}
			set
			{
				theID = WorkshopItem.TemplateItemIDText;
			}
		}

		[XmlIgnore()]
		public bool IsPublished
		{
			get
			{
				return theID != WorkshopItem.DraftItemIDText && theID != WorkshopItem.TemplateItemIDText;
			}
		}

#endregion

#region Methods

		public void SetAllChangedForNonEmptyFields()
		{
			if (!string.IsNullOrEmpty(theTitle))
			{
				theTitleIsChanged = true;
			}
			if (!string.IsNullOrEmpty(theDescription))
			{
				theDescriptionIsChanged = true;
			}
			if (!string.IsNullOrEmpty(theChangeNote))
			{
				theChangeNoteIsChanged = true;
			}
			if (!string.IsNullOrEmpty(theContentPathFolderOrFileName))
			{
				theContentPathFolderOrFileNameIsChanged = true;
			}
			if (!string.IsNullOrEmpty(thePreviewImagePathFileName))
			{
				thePreviewImagePathFileNameIsChanged = true;
			}

			//NOTE: Always set IsChanged for Visibility and Tags.
			theVisibilityIsChanged = true;
			theTagsIsChanged = true;

			//NOTE: Always set IsChanged for item.
			theItemIsChanged = true;
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