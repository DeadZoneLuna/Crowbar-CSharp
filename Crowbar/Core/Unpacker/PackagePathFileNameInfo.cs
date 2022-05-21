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
	public class PackagePathFileNameInfo : INotifyPropertyChanged
	{
#region Properties

		public string RelativePathFileName
		{
			get
			{
				return theRelativePathFileName;
			}
			set
			{
				theRelativePathFileName = value;
				NotifyPropertyChanged("RelativePathFileName");
			}
		}

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

		private string theRelativePathFileName;
		private string thePathFileName;

#endregion

	}

}