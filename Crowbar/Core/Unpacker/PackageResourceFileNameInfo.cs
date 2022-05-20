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
			this.thePathFileName = originalObject.PathFileName;
			this.theName = originalObject.Name;
			this.theSize = originalObject.Size;
			this.theCount = originalObject.Count;
			this.theType = originalObject.Type;
			this.theExtension = originalObject.Extension;
			this.theResourceFileIsFolder = originalObject.IsFolder;
			this.theArchivePathFileName = originalObject.ArchivePathFileName;
			this.thePathFileNameExists = originalObject.ArchivePathFileNameExists;
			this.theEntryIndex = originalObject.EntryIndex;
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
				return this.thePathFileName;
			}
			set
			{
				this.thePathFileName = value;
				NotifyPropertyChanged("PathFileName");
			}
		}

		public string Name
		{
			get
			{
				return this.theName;
			}
			set
			{
				this.theName = value;
				NotifyPropertyChanged("Name");
			}
		}

		public UInt64 Size
		{
			get
			{
				return this.theSize;
			}
			set
			{
				this.theSize = value;
				NotifyPropertyChanged("Size");
			}
		}

		public UInt64 Count
		{
			get
			{
				return this.theCount;
			}
			set
			{
				this.theCount = value;
				NotifyPropertyChanged("Count");
			}
		}

		public string Type
		{
			get
			{
				return this.theType;
			}
			set
			{
				this.theType = value;
				NotifyPropertyChanged("Type");
			}
		}

		public string Extension
		{
			get
			{
				return this.theExtension;
			}
			set
			{
				this.theExtension = value;
				NotifyPropertyChanged("Extension");
			}
		}

		public bool IsFolder
		{
			get
			{
				return this.theResourceFileIsFolder;
			}
			set
			{
				this.theResourceFileIsFolder = value;
				NotifyPropertyChanged("IsFolder");
			}
		}

		public string ArchivePathFileName
		{
			get
			{
				return this.theArchivePathFileName;
			}
			set
			{
				this.theArchivePathFileName = value;
				NotifyPropertyChanged("ArchivePathFileName");
			}
		}

		public bool ArchivePathFileNameExists
		{
			get
			{
				return this.thePathFileNameExists;
			}
			set
			{
				this.thePathFileNameExists = value;
				NotifyPropertyChanged("PathFileNameExists");
			}
		}

		public int EntryIndex
		{
			get
			{
				return this.theEntryIndex;
			}
			set
			{
				this.theEntryIndex = value;
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