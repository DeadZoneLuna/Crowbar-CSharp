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
using System.Xml.Serialization;

namespace Crowbar
{
	public class GameSetup : ICloneable, INotifyPropertyChanged
	{
#region Create and Destroy

		public GameSetup()
		{
			//MyBase.New()

			this.theGameName = "Left 4 Dead 2";
			this.theGameEngine = AppEnums.GameEngine.Source;
			this.theGamePathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\left4dead2\\gameinfo.txt";
			this.theGameAppPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\left4dead2.exe";
			this.theGameAppOptions = "";
			this.theCompilerPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\bin\\studiomdl.exe";
			this.theViewerPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\bin\\hlmv.exe";
			this.theMappingToolPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\bin\\hammer.exe";
			this.thePackerPathFileName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\left 4 dead 2\\bin\\vpk.exe";
		}

		protected GameSetup(GameSetup originalObject)
		{
			this.theGameName = originalObject.GameName;
			this.theGameEngine = originalObject.GameEngine;
			//Me.theGamePathFileName = originalObject.GamePathFileName
			//Me.theGameAppPathFileName = originalObject.GameAppPathFileName
			this.theGamePathFileName = originalObject.GamePathFileNameUnprocessed;
			this.theGameAppPathFileName = originalObject.GameAppPathFileNameUnprocessed;
			this.theGameAppOptions = originalObject.GameAppOptions;
			//Me.theCompilerPathFileName = originalObject.CompilerPathFileName
			//Me.theViewerPathFileName = originalObject.ViewerPathFileName
			//Me.theMappingToolPathFileName = originalObject.MappingToolPathFileName
			//Me.theUnpackerPathFileName = originalObject.UnpackerPathFileName
			this.theCompilerPathFileName = originalObject.CompilerPathFileNameUnprocessed;
			this.theViewerPathFileName = originalObject.ViewerPathFileNameUnprocessed;
			this.theMappingToolPathFileName = originalObject.MappingToolPathFileNameUnprocessed;
			this.thePackerPathFileName = originalObject.PackerPathFileNameUnprocessed;
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
				return this.theGameName;
			}
			set
			{
				if (this.theGameName != value)
				{
					this.theGameName = value;
					NotifyPropertyChanged("GameName");
				}
			}
		}

		public AppEnums.GameEngine GameEngine
		{
			get
			{
				return this.theGameEngine;
			}
			set
			{
				if (this.theGameEngine != value)
				{
					this.theGameEngine = value;
					NotifyPropertyChanged("GameEngine");
				}
			}
		}

		[XmlIgnore()]
		public string GamePathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(this.theGamePathFileName);
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
				return this.theGamePathFileName;
			}
			set
			{
				this.theGamePathFileName = value;
				NotifyPropertyChanged("GamePathFileName");
				NotifyPropertyChanged("GamePathFileNameUnprocessed");
			}
		}

		[XmlIgnore()]
		public string GameAppPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(this.theGameAppPathFileName);
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
				return this.theGameAppPathFileName;
			}
			set
			{
				this.theGameAppPathFileName = value;
				NotifyPropertyChanged("GameAppPathFileName");
				NotifyPropertyChanged("GameAppPathFileNameUnprocessed");
			}
		}

		public string GameAppOptions
		{
			get
			{
				return this.theGameAppOptions;
			}
			set
			{
				this.theGameAppOptions = value;
				NotifyPropertyChanged("GameAppOptions");
			}
		}

		[XmlIgnore()]
		public string CompilerPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(this.theCompilerPathFileName);
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
				return this.theCompilerPathFileName;
			}
			set
			{
				this.theCompilerPathFileName = value;
				NotifyPropertyChanged("CompilerPathFileName");
				NotifyPropertyChanged("CompilerPathFileNameUnprocessed");
			}
		}

		[XmlIgnore()]
		public string ViewerPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(this.theViewerPathFileName);
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
				return this.theViewerPathFileName;
			}
			set
			{
				this.theViewerPathFileName = value;
				NotifyPropertyChanged("ViewerPathFileName");
				NotifyPropertyChanged("ViewerPathFileNameUnprocessed");
			}
		}

		[XmlIgnore()]
		public string MappingToolPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(this.theMappingToolPathFileName);
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
				return this.theMappingToolPathFileName;
			}
			set
			{
				this.theMappingToolPathFileName = value;
				NotifyPropertyChanged("MappingToolPathFileName");
				NotifyPropertyChanged("MappingToolPathFileNameUnprocessed");
			}
		}

		[XmlIgnore()]
		public string PackerPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(this.thePackerPathFileName);
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
				return this.thePackerPathFileName;
			}
			set
			{
				this.thePackerPathFileName = value;
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