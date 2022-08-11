using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace Crowbar
{
	public class GameSetup : ICloneable, INotifyPropertyChanged
	{
#region Create and Destroy

		public GameSetup()
		{
			//MyBase.New()

			theGameName = "Left 4 Dead 2";
			theGameEngine = AppEnums.GameEngine.Source;
			theGamePathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\left4dead2\\gameinfo.txt";
			theGameAppPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\left4dead2.exe";
			theGameAppOptions = "";
			theCompilerPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\bin\\studiomdl.exe";
			theViewerPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\bin\\hlmv.exe";
			theMappingToolPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\bin\\hammer.exe";
			thePackerPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\bin\\vpk.exe";
		}

		protected GameSetup(GameSetup originalObject)
		{
			theGameName = originalObject.GameName;
			theGameEngine = originalObject.GameEngine;
			//Me.theGamePathFileName = originalObject.GamePathFileName
			//Me.theGameAppPathFileName = originalObject.GameAppPathFileName
			theGamePathFileName = originalObject.GamePathFileNameUnprocessed;
			theGameAppPathFileName = originalObject.GameAppPathFileNameUnprocessed;
			theGameAppOptions = originalObject.GameAppOptions;
			//Me.theCompilerPathFileName = originalObject.CompilerPathFileName
			//Me.theViewerPathFileName = originalObject.ViewerPathFileName
			//Me.theMappingToolPathFileName = originalObject.MappingToolPathFileName
			//Me.theUnpackerPathFileName = originalObject.UnpackerPathFileName
			theCompilerPathFileName = originalObject.CompilerPathFileNameUnprocessed;
			theViewerPathFileName = originalObject.ViewerPathFileNameUnprocessed;
			theMappingToolPathFileName = originalObject.MappingToolPathFileNameUnprocessed;
			thePackerPathFileName = originalObject.PackerPathFileNameUnprocessed;
		}

		public object Clone()
		{
			return new GameSetup(this);
		}

#endregion

#region Properties

		public string GameName
		{
			get
			{
				return theGameName;
			}
			set
			{
				if (theGameName != value)
				{
					theGameName = value;
					NotifyPropertyChanged("GameName");
				}
			}
		}

		public AppEnums.GameEngine GameEngine
		{
			get
			{
				return theGameEngine;
			}
			set
			{
				if (theGameEngine != value)
				{
					theGameEngine = value;
					NotifyPropertyChanged("GameEngine");
				}
			}
		}

		[XmlIgnore()]
		public string GamePathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(theGamePathFileName);
			}
			//Set(ByVal value As String)
			//	Me.theGamePathFileName = value
			//	NotifyPropertyChanged("GamePathFileName")
			//End Set
		}

		[XmlElement("GamePathFileName")]
		public string GamePathFileNameUnprocessed
		{
			get
			{
				return theGamePathFileName;
			}
			set
			{
				theGamePathFileName = value;
				NotifyPropertyChanged("GamePathFileName");
				NotifyPropertyChanged("GamePathFileNameUnprocessed");
			}
		}

		[XmlIgnore()]
		public string GameAppPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(theGameAppPathFileName);
			}
			//Set(ByVal value As String)
			//	Me.theGameAppPathFileName = value
			//	NotifyPropertyChanged("GameAppPathFileName")
			//End Set
		}

		[XmlElement("GameAppPathFileName")]
		public string GameAppPathFileNameUnprocessed
		{
			get
			{
				return theGameAppPathFileName;
			}
			set
			{
				theGameAppPathFileName = value;
				NotifyPropertyChanged("GameAppPathFileName");
				NotifyPropertyChanged("GameAppPathFileNameUnprocessed");
			}
		}

		public string GameAppOptions
		{
			get
			{
				return theGameAppOptions;
			}
			set
			{
				theGameAppOptions = value;
				NotifyPropertyChanged("GameAppOptions");
			}
		}

		[XmlIgnore()]
		public string CompilerPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(theCompilerPathFileName);
			}
			//Set(ByVal value As String)
			//	Me.theCompilerPathFileName = value
			//	NotifyPropertyChanged("CompilerPathFileName")
			//End Set
		}

		[XmlElement("CompilerPathFileName")]
		public string CompilerPathFileNameUnprocessed
		{
			get
			{
				return theCompilerPathFileName;
			}
			set
			{
				theCompilerPathFileName = value;
				NotifyPropertyChanged("CompilerPathFileName");
				NotifyPropertyChanged("CompilerPathFileNameUnprocessed");
			}
		}

		[XmlIgnore()]
		public string ViewerPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(theViewerPathFileName);
			}
			//Set(ByVal value As String)
			//	Me.theViewerPathFileName = value
			//	NotifyPropertyChanged("ViewerPathFileName")
			//End Set
		}

		[XmlElement("ViewerPathFileName")]
		public string ViewerPathFileNameUnprocessed
		{
			get
			{
				return theViewerPathFileName;
			}
			set
			{
				theViewerPathFileName = value;
				NotifyPropertyChanged("ViewerPathFileName");
				NotifyPropertyChanged("ViewerPathFileNameUnprocessed");
			}
		}

		[XmlIgnore()]
		public string MappingToolPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(theMappingToolPathFileName);
			}
			//Set(ByVal value As String)
			//	Me.theMappingToolPathFileName = value
			//	NotifyPropertyChanged("MappingToolPathFileName")
			//End Set
		}

		[XmlElement("MappingToolPathFileName")]
		public string MappingToolPathFileNameUnprocessed
		{
			get
			{
				return theMappingToolPathFileName;
			}
			set
			{
				theMappingToolPathFileName = value;
				NotifyPropertyChanged("MappingToolPathFileName");
				NotifyPropertyChanged("MappingToolPathFileNameUnprocessed");
			}
		}

		[XmlIgnore()]
		public string PackerPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(thePackerPathFileName);
			}
			//Set(ByVal value As String)
			//	Me.theUnpackerPathFileName = value
			//	NotifyPropertyChanged("PackerPathFileName")
			//End Set
		}

		[XmlElement("PackerPathFileName")]
		public string PackerPathFileNameUnprocessed
		{
			get
			{
				return thePackerPathFileName;
			}
			set
			{
				thePackerPathFileName = value;
				NotifyPropertyChanged("PackerPathFileName");
				NotifyPropertyChanged("PackerPathFileNameUnprocessed");
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

		private string theGameName;
		private AppEnums.GameEngine theGameEngine;
		private string theGamePathFileName;
		private string theGameAppPathFileName;
		private string theGameAppId;
		private string theGameAppOptions;
		private string theCompilerPathFileName;
		private string theViewerPathFileName;
		private string theMappingToolPathFileName;
		private string thePackerPathFileName;

#endregion

	}

}