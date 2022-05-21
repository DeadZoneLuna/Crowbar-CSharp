//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace Crowbar
{
	public class PackageResourceFileNameInfo : ICloneable, INotifyPropertyChanged
	{
		public PackageResourceFileNameInfo()
		{
			//MyBase.New()
		}

		protected PackageResourceFileNameInfo(PackageResourceFileNameInfo originalObject)
		{
			thePathFileName = originalObject.PathFileName;
			theName = originalObject.Name;
			theSize = originalObject.Size;
			theCount = originalObject.Count;
			theType = originalObject.Type;
			theExtension = originalObject.Extension;
			theResourceFileIsFolder = originalObject.IsFolder;
			theArchivePathFileName = originalObject.ArchivePathFileName;
			thePathFileNameExists = originalObject.ArchivePathFileNameExists;
			theEntryIndex = originalObject.EntryIndex;
		}

		public object Clone()
		{
			return new PackageResourceFileNameInfo(this);
		}

#region Properties

		public string PathFileName
		{
			get
			{
				return thePathFileName;
			}
			set
			{
				thePathFileName = value;
				NotifyPropertyChanged("PathFileName");
			}
		}

		public string Name
		{
			get
			{
				return theName;
			}
			set
			{
				theName = value;
				NotifyPropertyChanged("Name");
			}
		}

		public UInt64 Size
		{
			get
			{
				return theSize;
			}
			set
			{
				theSize = value;
				NotifyPropertyChanged("Size");
			}
		}

		public UInt64 Count
		{
			get
			{
				return theCount;
			}
			set
			{
				theCount = value;
				NotifyPropertyChanged("Count");
			}
		}

		public string Type
		{
			get
			{
				return theType;
			}
			set
			{
				theType = value;
				NotifyPropertyChanged("Type");
			}
		}

		public string Extension
		{
			get
			{
				return theExtension;
			}
			set
			{
				theExtension = value;
				NotifyPropertyChanged("Extension");
			}
		}

		public bool IsFolder
		{
			get
			{
				return theResourceFileIsFolder;
			}
			set
			{
				theResourceFileIsFolder = value;
				NotifyPropertyChanged("IsFolder");
			}
		}

		public string ArchivePathFileName
		{
			get
			{
				return theArchivePathFileName;
			}
			set
			{
				theArchivePathFileName = value;
				NotifyPropertyChanged("ArchivePathFileName");
			}
		}

		public bool ArchivePathFileNameExists
		{
			get
			{
				return thePathFileNameExists;
			}
			set
			{
				thePathFileNameExists = value;
				NotifyPropertyChanged("PathFileNameExists");
			}
		}

		public int EntryIndex
		{
			get
			{
				return theEntryIndex;
			}
			set
			{
				theEntryIndex = value;
				NotifyPropertyChanged("EntryIndex");
			}
		}

#endregion

#region Events

		public event PropertyChangedEventHandler PropertyChanged;

#endregion

#region Private Methods

		protected void NotifyPropertyChanged(string info)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(info));
		}

#endregion

#region Data

		private string theArchivePathFileName;

		private string thePathFileName;
		private bool thePathFileNameExists;

		private string theName;
		private UInt64 theSize;
		private UInt64 theCount;
		private string theType;
		private string theExtension;

		private bool theResourceFileIsFolder;

		private int theEntryIndex;

#endregion

	}

}