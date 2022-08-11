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

			theMacro = "<library1>";
			thePath = "C:\\Program Files (x86)\\Steam";
			theUseCount = 0;
		}

		protected SteamLibraryPath(SteamLibraryPath originalObject)
		{
			theMacro = originalObject.Macro;
			thePath = originalObject.LibraryPath;
			theUseCount = originalObject.UseCount;
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
				return theMacro;
			}
			set
			{
				if (theMacro != value)
				{
					theMacro = value;
					NotifyPropertyChanged("Macro");
				}
			}
		}

		public string LibraryPath
		{
			get
			{
				return thePath;
			}
			set
			{
				thePath = value;
				NotifyPropertyChanged("LibraryPath");
			}
		}

		public int UseCount
		{
			get
			{
				return theUseCount;
			}
			set
			{
				theUseCount = value;
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