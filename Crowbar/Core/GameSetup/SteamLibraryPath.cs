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
	public class SteamLibraryPath : ICloneable, INotifyPropertyChanged
	{
#region Create and Destroy

		public SteamLibraryPath()
		{
			//MyBase.New()

			this.theMacro = "<library1>";
			this.thePath = "C:\\Program Files (x86)\\Steam";
			this.theUseCount = 0;
		}

		protected SteamLibraryPath(SteamLibraryPath originalObject)
		{
			this.theMacro = originalObject.Macro;
			this.thePath = originalObject.LibraryPath;
			this.theUseCount = originalObject.UseCount;
		}

		public object Clone()
		{
			return new SteamLibraryPath(this);
		}

#endregion

#region Properties

		public string Macro
		{
			get
			{
				return this.theMacro;
			}
			set
			{
				if (this.theMacro != value)
				{
					this.theMacro = value;
					NotifyPropertyChanged("Macro");
				}
			}
		}

		public string LibraryPath
		{
			get
			{
				return this.thePath;
			}
			set
			{
				this.thePath = value;
				NotifyPropertyChanged("LibraryPath");
			}
		}

		public int UseCount
		{
			get
			{
				return this.theUseCount;
			}
			set
			{
				this.theUseCount = value;
				NotifyPropertyChanged("UseCount");
			}
		}

#endregion

#region Methods

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

		private string theMacro;
		private string thePath;
		private int theUseCount;

#endregion

	}

}