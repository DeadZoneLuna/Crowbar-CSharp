using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;

// Purpose: Stores application-related settings, such as UI widget locations and auto-recover data.
namespace Crowbar
{
	public class AppSettings : INotifyPropertyChanged
	{
		#region Data
		// General
		private bool theAppIsSingleInstance;
		private Point theWindowLocation;
		private Size theWindowSize;
		private FormWindowState theWindowState;
		private Color theAboutTabBackgroundColor;
		private int theMainWindowSelectedTabIndex;

		// Set Up Games tab

		private BindingListExAutoSort<GameSetup> theGameSetups;
		private string theSteamAppPathFileName;
		private BindingListEx<SteamLibraryPath> theSteamLibraryPaths;
		private int theSetUpGamesGameSetupSelectedIndex;

		// Download tab

		private string theDownloadItemIdOrLink;
		private AppEnums.DownloadOutputPathOptions theDownloadOutputFolderOption;
		private string theDownloadOutputWorkPath;
		private bool theDownloadUseItemIdIsChecked;
		private bool theDownloadPrependItemTitleIsChecked;
		private bool theDownloadAppendItemUpdateDateTimeIsChecked;
		private bool theDownloadReplaceSpacesWithUnderscoresIsChecked;
		private bool theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked;

		// Unpack tab

		private AppEnums.ContainerType theUnpackContainerType;
		private string theUnpackPackagePathFolderOrFileName;
		//Private theUnpackOutputFolderOption As OutputFolderOptions
		private AppEnums.UnpackOutputPathOptions theUnpackOutputFolderOption;
		private string theUnpackOutputSamePath;
		private string theUnpackOutputSubfolderName;
		private string theUnpackOutputFullPath;
		private string theUnpackPackagePathFileName;
		private int theUnpackGameSetupSelectedIndex;
		private AppEnums.UnpackSearchFieldOptions theUnpackSearchField;
		private string theUnpackSearchText;

		private bool theUnpackFolderForEachPackageIsChecked;
		private bool theUnpackKeepFullPathIsChecked;
		private bool theUnpackLogFileIsChecked;

		private AppEnums.InputOptions theUnpackMode;
		private bool theUnpackerIsRunning;

		// Preview tab

		private string thePreviewMdlPathFileName;
		private AppEnums.SupportedMdlVersion thePreviewOverrideMdlVersion;
		private int thePreviewGameSetupSelectedIndex;

		private bool thePreviewDataViewerIsRunning;
		private bool thePreviewViewerIsRunning;

		// Decompile tab

		private string theDecompileMdlPathFileName;
		//Private theDecompileOutputFolderOption As OutputFolderOptions
		private AppEnums.DecompileOutputPathOptions theDecompileOutputFolderOption;
		private string theDecompileOutputSubfolderName;
		private string theDecompileOutputFullPath;
		//Private theDecompileOutputPathName As String

		private bool theDecompileQcFileIsChecked;
		private bool theDecompileGroupIntoQciFilesIsChecked;
		private bool theDecompileQcSkinFamilyOnSingleLineIsChecked;
		private bool theDecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked;
		private bool theDecompileQcIncludeDefineBoneLinesIsChecked;
		private bool theDecompileQcUseMixedCaseForKeywordsIsChecked;

		private bool theDecompileReferenceMeshSmdFileIsChecked;
		private bool theDecompileRemovePathFromSmdMaterialFileNamesIsChecked;
		private bool theDecompileUseNonValveUvConversionIsChecked;

		private bool theDecompileBoneAnimationSmdFilesIsChecked;
		private bool theDecompileBoneAnimationPlaceInSubfolderIsChecked;

		private bool theDecompileTextureBmpFileIsChecked;
		private bool theDecompileLodMeshSmdFilesIsChecked;
		private bool theDecompilePhysicsMeshSmdFileIsChecked;
		private bool theDecompileVertexAnimationVtaFileIsChecked;
		private bool theDecompileProceduralBonesVrdFileIsChecked;

		private bool theDecompileDeclareSequenceQciFileIsChecked;

		private bool theDecompileFolderForEachModelIsChecked;
		private bool theDecompilePrefixFileNamesWithModelNameIsChecked;
		private bool theDecompileStricterFormatIsChecked;
		private bool theDecompileLogFileIsChecked;
		private bool theDecompileDebugInfoFilesIsChecked;

		private AppEnums.SupportedMdlVersion theDecompileOverrideMdlVersion;

		private AppEnums.InputOptions theDecompileMode;
		private bool theDecompilerIsRunning;

		// Compile tab

		private string theCompileQcPathFileName;
		private AppEnums.InputOptions theCompileMode;

		private bool theCompileOutputFolderIsChecked;
		//Private theCompileOutputFolderOption As OutputFolderOptions
		private AppEnums.CompileOutputPathOptions theCompileOutputFolderOption;
		private string theCompileOutputSubfolderName;
		private string theCompileOutputFullPath;

		private int theCompileGameSetupSelectedIndex;

		// GoldSource engine
		private bool theCompileGoldSourceLogFileIsChecked;

		// Source engine
		private bool theCompileSourceLogFileIsChecked;
		private bool theCompileOptionDefineBonesIsChecked;
		private bool theCompileOptionDefineBonesCreateFileIsChecked;
		private string theCompileOptionDefineBonesQciFileName;
		private bool theCompileOptionDefineBonesOverwriteQciFileIsChecked;
		private bool theCompileOptionDefineBonesModifyQcFileIsChecked;
		private bool theCompileOptionNoP4IsChecked;
		private bool theCompileOptionVerboseIsChecked;

		private string theCompileOptionsText;

		private bool theCompilerIsRunning;

		// Patch tab

		private AppEnums.InputOptions thePatchMode;
		private string thePatchMdlPathFileName;

		// View tab

		private string theViewMdlPathFileName;
		private AppEnums.SupportedMdlVersion theViewOverrideMdlVersion;
		private int theViewGameSetupSelectedIndex;

		private bool theViewDataViewerIsRunning;
		private bool theViewViewerIsRunning;

		// Pack tab

		private AppEnums.PackInputOptions thePackMode;
		private string thePackInputPathFileName;

		private AppEnums.PackOutputPathOptions thePackOutputFolderOption;
		private string thePackOutputParentPath;
		private string thePackOutputPath;

		private int thePackGameSetupSelectedIndex;

		private bool thePackLogFileIsChecked;
		private bool thePackOptionMultiFileVpkIsChecked;

		private string thePackGmaTitle;
		private BindingListEx<string> thePackGmaItemTags;

		private string thePackOptionsText;

		private bool thePackerIsRunning;

		// Publish tab

		private int thePublishGameSelectedIndex;
		private BindingListExAutoSort<SteamAppUserInfo> thePublishSteamAppUserInfos;
		private AppEnums.PublishSearchFieldOptions thePublishSearchField;
		private string thePublishSearchText;
		//Private thePublishDragDroppedContentPath As String

		// Options tab

		private bool theOptionsAutoOpenVpkFileIsChecked;
		private AppEnums.ActionType theOptionsAutoOpenVpkFileOption;
		private bool theOptionsAutoOpenGmaFileIsChecked;
		private AppEnums.ActionType theOptionsAutoOpenGmaFileOption;
		private bool theOptionsAutoOpenFpxFileIsChecked;

		private bool theOptionsAutoOpenMdlFileIsChecked;
		private bool theOptionsAutoOpenMdlFileForPreviewIsChecked;
		private bool theOptionsAutoOpenMdlFileForDecompileIsChecked;
		private bool theOptionsAutoOpenMdlFileForViewIsChecked;
		private AppEnums.ActionType theOptionsAutoOpenMdlFileOption;

		private bool theOptionsAutoOpenQcFileIsChecked;
		//Private theOptionsAutoOpenQcFileOption As ActionType

		private AppEnums.ActionType theOptionsAutoOpenFolderOption;

		private AppEnums.ActionType theOptionsDragAndDropVpkFileOption;
		private AppEnums.ActionType theOptionsDragAndDropGmaFileOption;

		private bool theOptionsDragAndDropMdlFileForPreviewIsChecked;
		private bool theOptionsDragAndDropMdlFileForDecompileIsChecked;
		private bool theOptionsDragAndDropMdlFileForViewIsChecked;
		private AppEnums.ActionType theOptionsDragAndDropMdlFileOption;

		//Private theOptionsDragAndDropQcFileOption As ActionType

		private AppEnums.ActionType theOptionsDragAndDropFolderOption;

		private bool theOptionsContextMenuIntegrateMenuItemsIsChecked;
		private bool theOptionsContextMenuIntegrateSubMenuIsChecked;

		private bool theOptionsOpenWithCrowbarIsChecked;
		private bool theOptionsViewMdlFileIsChecked;
		private bool theOptionsDecompileMdlFileIsChecked;
		private bool theOptionsDecompileFolderIsChecked;
		private bool theOptionsDecompileFolderAndSubfoldersIsChecked;
		private bool theOptionsCompileQcFileIsChecked;
		private bool theOptionsCompileFolderIsChecked;
		private bool theOptionsCompileFolderAndSubfoldersIsChecked;

		// Update tab

		private string theUpdateDownloadPath;
		private bool theUpdateUpdateToNewPathIsChecked;
		private string theUpdateUpdateDownloadPath;
		private bool theUpdateCopySettingsIsChecked;
		#endregion

		#region Create and Destroy
		public AppSettings()
		{
			//MyBase.New()

			theAppIsSingleInstance = false;
			theWindowLocation = new Point(0, 0);
			theWindowSize = new Size(800, 600);
			theWindowState = FormWindowState.Normal;
			//NOTE: 0 means the Set Up Games tab.
			theMainWindowSelectedTabIndex = 0;

			thePreviewDataViewerIsRunning = false;
			//Me.thePreviewerIsRunning = False
			theDecompilerIsRunning = false;
			theCompilerIsRunning = false;
			theViewDataViewerIsRunning = false;
			//Me.theViewerIsRunning = False
			thePackerIsRunning = false;

			theGameSetups = new BindingListExAutoSort<GameSetup>("GameName");
			theSteamAppPathFileName = "C:\\Program Files (x86)\\Steam\\Steam.exe";
			theSteamLibraryPaths = new BindingListEx<SteamLibraryPath>();
			theSetUpGamesGameSetupSelectedIndex = 0;

			theDownloadItemIdOrLink = string.Empty;
			theDownloadOutputFolderOption = AppEnums.DownloadOutputPathOptions.DocumentsFolder;
			theDownloadOutputWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			SetDefaultDownloadOptions();

			theUnpackContainerType = AppEnums.ContainerType.VPK;
			theUnpackPackagePathFolderOrFileName = "";
			//Me.theUnpackOutputFolderOption = OutputFolderOptions.SubfolderName
			theUnpackOutputFolderOption = AppEnums.UnpackOutputPathOptions.SameFolder;
			SetDefaultUnpackOutputSubfolderName();
			theUnpackOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			theUnpackGameSetupSelectedIndex = 0;
			theUnpackSearchField = AppEnums.UnpackSearchFieldOptions.Files;
			theUnpackSearchText = "";
			SetDefaultUnpackOptions();
			theUnpackMode = AppEnums.InputOptions.File;

			thePreviewMdlPathFileName = "";
			thePreviewOverrideMdlVersion = AppEnums.SupportedMdlVersion.DoNotOverride;
			thePreviewGameSetupSelectedIndex = 0;

			theDecompileMdlPathFileName = "";
			//Me.theDecompileOutputFolderOption = OutputFolderOptions.SubfolderName
			theDecompileOutputFolderOption = AppEnums.DecompileOutputPathOptions.WorkFolder;
			SetDefaultDecompileOutputSubfolderName();
			theDecompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			SetDefaultDecompileReCreateFilesOptions();
			theDecompileMode = AppEnums.InputOptions.File;
			theDecompileFolderForEachModelIsChecked = false;
			theDecompileStricterFormatIsChecked = false;
			theDecompileLogFileIsChecked = false;
			theDecompileDebugInfoFilesIsChecked = false;
			theDecompileOverrideMdlVersion = AppEnums.SupportedMdlVersion.DoNotOverride;

			theCompileQcPathFileName = "";
			theCompileOutputFolderIsChecked = true;
			//Me.theCompileOutputFolderOption = OutputFolderOptions.SubfolderName
			theCompileOutputFolderOption = AppEnums.CompileOutputPathOptions.GameModelsFolder;
			SetDefaultCompileOutputSubfolderName();
			theCompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			theCompileGameSetupSelectedIndex = 0;
			SetDefaultCompileOptions();
			theCompileMode = AppEnums.InputOptions.File;

			thePatchMdlPathFileName = "";
			thePatchMode = AppEnums.InputOptions.File;

			theViewMdlPathFileName = "";
			theViewOverrideMdlVersion = AppEnums.SupportedMdlVersion.DoNotOverride;
			theViewGameSetupSelectedIndex = 0;

			thePackInputPathFileName = "";
			//Me.theCompileOutputFolderIsChecked = True
			//'Me.theCompileOutputFolderOption = OutputFolderOptions.SubfolderName
			thePackOutputFolderOption = AppEnums.PackOutputPathOptions.ParentFolder;
			//Me.SetDefaultCompileOutputSubfolderName()
			thePackOutputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			thePackGameSetupSelectedIndex = 0;
			SetDefaultPackOptions();
			//Me.theCompileMode = InputOptions.File

			thePublishGameSelectedIndex = 0;
			thePublishSteamAppUserInfos = new BindingListExAutoSort<SteamAppUserInfo>("AppID");
			thePublishSearchField = AppEnums.PublishSearchFieldOptions.ID;
			thePublishSearchText = "";
			//Me.thePublishDragDroppedContentPath = ""

			SetDefaultOptionsAutoOpenOptions();
			SetDefaultOptionsDragAndDropOptions();
			SetDefaultOptionsContextMenuOptions();

			theUpdateDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			theUpdateUpdateToNewPathIsChecked = false;
			theUpdateUpdateDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			theUpdateCopySettingsIsChecked = true;

			//Me.Init()
		}
		#endregion

		#region Init and Free

		//Public Sub Init()
		//End Sub

		//Private Sub Free()
		//End Sub

		#endregion

		#region Properties
		public bool AppIsSingleInstance
		{
			get
			{
				return theAppIsSingleInstance;
			}
			set
			{
				theAppIsSingleInstance = value;
				NotifyPropertyChanged("AppIsSingleInstance");
			}
		}

		public Point WindowLocation
		{
			get
			{
				return theWindowLocation;
			}
			set
			{
				theWindowLocation = value;
			}
		}

		public Size WindowSize
		{
			get
			{
				return theWindowSize;
			}
			set
			{
				theWindowSize = value;
			}
		}

		public FormWindowState WindowState
		{
			get
			{
				return theWindowState;
			}
			set
			{
				theWindowState = value;
			}
		}

		public int MainWindowSelectedTabIndex
		{
			get
			{
				return theMainWindowSelectedTabIndex;
			}
			set
			{
				theMainWindowSelectedTabIndex = value;
			}
		}

		public BindingListExAutoSort<GameSetup> GameSetups
		{
			get
			{
				return theGameSetups;
			}
			set
			{
				theGameSetups = value;
				NotifyPropertyChanged("GameSetups");
			}
		}

		[XmlIgnore()]
		public string SteamAppPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(theSteamAppPathFileName);
			}
			//Set(ByVal value As String)
			//	Me.theSteamAppPathFileName = value
			//	NotifyPropertyChanged("SteamAppPathFileName")
			//End Set
		}

		[XmlElement("SteamAppPathFileName")]
		public string SteamAppPathFileNameUnprocessed
		{
			get
			{
				return theSteamAppPathFileName;
			}
			set
			{
				theSteamAppPathFileName = value;
				NotifyPropertyChanged("SteamAppPathFileName");
				NotifyPropertyChanged("SteamAppPathFileNameUnprocessed");
			}
		}

		public BindingListEx<SteamLibraryPath> SteamLibraryPaths
		{
			get
			{
				return theSteamLibraryPaths;
			}
			set
			{
				theSteamLibraryPaths = value;
				NotifyPropertyChanged("SteamLibraryPaths");
			}
		}

		public int SetUpGamesGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (theSetUpGamesGameSetupSelectedIndex >= theGameSetups.Count)
				{
					theSetUpGamesGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return theSetUpGamesGameSetupSelectedIndex;
			}
			set
			{
				theSetUpGamesGameSetupSelectedIndex = value;
				NotifyPropertyChanged("SetUpGamesGameSetupSelectedIndex");
			}
		}

		public string DownloadItemIdOrLink
		{
			get
			{
				return theDownloadItemIdOrLink;
			}
			set
			{
				if (theDownloadItemIdOrLink != value)
				{
					theDownloadItemIdOrLink = value;
					NotifyPropertyChanged("DownloadItemIdOrLink");
				}
			}
		}

		public AppEnums.DownloadOutputPathOptions DownloadOutputFolderOption
		{
			get
			{
				return theDownloadOutputFolderOption;
			}
			set
			{
				if (theDownloadOutputFolderOption != value)
				{
					theDownloadOutputFolderOption = value;
					NotifyPropertyChanged("DownloadOutputFolderOption");
				}
			}
		}

		public string DownloadOutputWorkPath
		{
			get
			{
				return theDownloadOutputWorkPath;
			}
			set
			{
				if (theDownloadOutputWorkPath != value)
				{
					theDownloadOutputWorkPath = value;
					NotifyPropertyChanged("DownloadOutputWorkPath");
				}
			}
		}

		public bool DownloadUseItemIdIsChecked
		{
			get
			{
				return theDownloadUseItemIdIsChecked;
			}
			set
			{
				if (theDownloadUseItemIdIsChecked != value)
				{
					theDownloadUseItemIdIsChecked = value;
					NotifyPropertyChanged("DownloadUseItemIdIsChecked");
				}
			}
		}

		public bool DownloadPrependItemTitleIsChecked
		{
			get
			{
				return theDownloadPrependItemTitleIsChecked;
			}
			set
			{
				if (theDownloadPrependItemTitleIsChecked != value)
				{
					theDownloadPrependItemTitleIsChecked = value;
					NotifyPropertyChanged("DownloadPrependItemTitleIsChecked");
				}
			}
		}

		public bool DownloadAppendItemUpdateDateTimeIsChecked
		{
			get
			{
				return theDownloadAppendItemUpdateDateTimeIsChecked;
			}
			set
			{
				if (theDownloadAppendItemUpdateDateTimeIsChecked != value)
				{
					theDownloadAppendItemUpdateDateTimeIsChecked = value;
					NotifyPropertyChanged("DownloadAppendItemUpdateDateTimeIsChecked");
				}
			}
		}

		public bool DownloadReplaceSpacesWithUnderscoresIsChecked
		{
			get
			{
				return theDownloadReplaceSpacesWithUnderscoresIsChecked;
			}
			set
			{
				if (theDownloadReplaceSpacesWithUnderscoresIsChecked != value)
				{
					theDownloadReplaceSpacesWithUnderscoresIsChecked = value;
					NotifyPropertyChanged("DownloadReplaceSpacesWithUnderscoresIsChecked");
				}
			}
		}

		public bool DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked
		{
			get
			{
				return theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked;
			}
			set
			{
				if (theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked != value)
				{
					theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked = value;
					NotifyPropertyChanged("DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked");
				}
			}
		}

		public string UnpackPackagePathFolderOrFileName
		{
			get
			{
				return theUnpackPackagePathFolderOrFileName;
			}
			set
			{
				theUnpackPackagePathFolderOrFileName = value;
				NotifyPropertyChanged("UnpackPackagePathFolderOrFileName");
			}
		}

		public AppEnums.UnpackOutputPathOptions UnpackOutputFolderOption
		{
			get
			{
				return theUnpackOutputFolderOption;
			}
			set
			{
				theUnpackOutputFolderOption = value;
				NotifyPropertyChanged("UnpackOutputFolderOption");
			}
		}

		public string UnpackOutputSamePath
		{
			get
			{
				return theUnpackOutputSamePath;
			}
			set
			{
				theUnpackOutputSamePath = value;
				NotifyPropertyChanged("UnpackOutputSamePath");
			}
		}

		public string UnpackOutputSubfolderName
		{
			get
			{
				return theUnpackOutputSubfolderName;
			}
			set
			{
				theUnpackOutputSubfolderName = value;
				NotifyPropertyChanged("UnpackOutputSubfolderName");
			}
		}

		public string UnpackOutputFullPath
		{
			get
			{
				return theUnpackOutputFullPath;
			}
			set
			{
				theUnpackOutputFullPath = value;
				NotifyPropertyChanged("UnpackOutputFullPath");
			}
		}

		public int UnpackGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (theUnpackGameSetupSelectedIndex >= theGameSetups.Count)
				{
					theUnpackGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return theUnpackGameSetupSelectedIndex;
			}
			set
			{
				theUnpackGameSetupSelectedIndex = value;
				NotifyPropertyChanged("UnpackGameSetupSelectedIndex");
			}
		}

		public AppEnums.UnpackSearchFieldOptions UnpackSearchField
		{
			get
			{
				return theUnpackSearchField;
			}
			set
			{
				if (theUnpackSearchField != value)
				{
					theUnpackSearchField = value;
					NotifyPropertyChanged("UnpackSearchField");
				}
			}
		}

		public string UnpackSearchText
		{
			get
			{
				return theUnpackSearchText;
			}
			set
			{
				if (theUnpackSearchText != value)
				{
					theUnpackSearchText = value;
					NotifyPropertyChanged("UnpackSearchText");
				}
			}
		}

		public bool UnpackFolderForEachPackageIsChecked
		{
			get
			{
				return theUnpackFolderForEachPackageIsChecked;
			}
			set
			{
				theUnpackFolderForEachPackageIsChecked = value;
				NotifyPropertyChanged("UnpackFolderForEachPackageIsChecked");
			}
		}

		public bool UnpackKeepFullPathIsChecked
		{
			get
			{
				return theUnpackKeepFullPathIsChecked;
			}
			set
			{
				theUnpackKeepFullPathIsChecked = value;
				NotifyPropertyChanged("UnpackKeepFullPathIsChecked");
			}
		}

		public bool UnpackLogFileIsChecked
		{
			get
			{
				return theUnpackLogFileIsChecked;
			}
			set
			{
				theUnpackLogFileIsChecked = value;
				NotifyPropertyChanged("UnpackLogFileIsChecked");
			}
		}

		public AppEnums.InputOptions UnpackMode
		{
			get
			{
				return theUnpackMode;
			}
			set
			{
				if (theUnpackMode != value)
				{
					theUnpackMode = value;
					NotifyPropertyChanged("UnpackMode");
				}
			}
		}

		[XmlIgnore()]
		public bool UnpackerIsRunning
		{
			get
			{
				return theUnpackerIsRunning;
			}
			set
			{
				theUnpackerIsRunning = value;
				NotifyPropertyChanged("UnpackerIsRunning");
			}
		}

		public string PreviewMdlPathFileName
		{
			get
			{
				return thePreviewMdlPathFileName;
			}
			set
			{
				thePreviewMdlPathFileName = value;
				NotifyPropertyChanged("PreviewMdlPathFileName");
			}
		}

		public AppEnums.SupportedMdlVersion PreviewOverrideMdlVersion
		{
			get
			{
				return thePreviewOverrideMdlVersion;
			}
			set
			{
				thePreviewOverrideMdlVersion = value;
				NotifyPropertyChanged("PreviewOverrideMdlVersion");
			}
		}

		public int PreviewGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (thePreviewGameSetupSelectedIndex >= theGameSetups.Count)
				{
					thePreviewGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return thePreviewGameSetupSelectedIndex;
			}
			set
			{
				thePreviewGameSetupSelectedIndex = value;
				NotifyPropertyChanged("PreviewGameSetupSelectedIndex");
			}
		}

		[XmlIgnore()]
		public bool PreviewDataViewerIsRunning
		{
			get
			{
				return thePreviewDataViewerIsRunning;
			}
			set
			{
				thePreviewDataViewerIsRunning = value;
				NotifyPropertyChanged("PreviewDataViewerIsRunning");
			}
		}

		[XmlIgnore()]
		public bool PreviewViewerIsRunning
		{
			get
			{
				return thePreviewViewerIsRunning;
			}
			set
			{
				thePreviewViewerIsRunning = value;
				NotifyPropertyChanged("PreviewViewerIsRunning");
			}
		}

		public string DecompileMdlPathFileName
		{
			get
			{
				return theDecompileMdlPathFileName;
			}
			set
			{
				theDecompileMdlPathFileName = value;
				NotifyPropertyChanged("DecompileMdlPathFileName");
			}
		}

		//Public Property DecompileOutputFolderOption() As OutputFolderOptions
		//	Get
		//		Return Me.theDecompileOutputFolderOption
		//	End Get
		//	Set(ByVal value As OutputFolderOptions)
		//		Me.theDecompileOutputFolderOption = value
		//		NotifyPropertyChanged("DecompileOutputFolderOption")
		//	End Set
		//End Property

		public AppEnums.DecompileOutputPathOptions DecompileOutputFolderOption
		{
			get
			{
				return theDecompileOutputFolderOption;
			}
			set
			{
				theDecompileOutputFolderOption = value;
				NotifyPropertyChanged("DecompileOutputFolderOption");
			}
		}

		public string DecompileOutputSubfolderName
		{
			get
			{
				return theDecompileOutputSubfolderName;
			}
			set
			{
				theDecompileOutputSubfolderName = value;
				NotifyPropertyChanged("DecompileOutputSubfolderName");
			}
		}

		public string DecompileOutputFullPath
		{
			get
			{
				return theDecompileOutputFullPath;
			}
			set
			{
				theDecompileOutputFullPath = value;
				NotifyPropertyChanged("DecompileOutputFullPath");
			}
		}

		public bool DecompileQcFileIsChecked
		{
			get
			{
				return theDecompileQcFileIsChecked;
			}
			set
			{
				theDecompileQcFileIsChecked = value;
				NotifyPropertyChanged("DecompileQcFileIsChecked");
			}
		}

		public bool DecompileGroupIntoQciFilesIsChecked
		{
			get
			{
				return theDecompileGroupIntoQciFilesIsChecked;
			}
			set
			{
				theDecompileGroupIntoQciFilesIsChecked = value;
				NotifyPropertyChanged("DecompileGroupIntoQciFilesIsChecked");
			}
		}

		public bool DecompileQcSkinFamilyOnSingleLineIsChecked
		{
			get
			{
				return theDecompileQcSkinFamilyOnSingleLineIsChecked;
			}
			set
			{
				theDecompileQcSkinFamilyOnSingleLineIsChecked = value;
				NotifyPropertyChanged("DecompileQcSkinFamilyOnSingleLineIsChecked");
			}
		}

		public bool DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked
		{
			get
			{
				return theDecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked;
			}
			set
			{
				theDecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked = value;
				NotifyPropertyChanged("DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked");
			}
		}

		public bool DecompileQcIncludeDefineBoneLinesIsChecked
		{
			get
			{
				return theDecompileQcIncludeDefineBoneLinesIsChecked;
			}
			set
			{
				theDecompileQcIncludeDefineBoneLinesIsChecked = value;
				NotifyPropertyChanged("DecompileQcIncludeDefineBoneLinesIsChecked");
			}
		}

		public bool DecompileQcUseMixedCaseForKeywordsIsChecked
		{
			get
			{
				return theDecompileQcUseMixedCaseForKeywordsIsChecked;
			}
			set
			{
				theDecompileQcUseMixedCaseForKeywordsIsChecked = value;
				NotifyPropertyChanged("DecompileQcUseMixedCaseForKeywordsIsChecked");
			}
		}

		public bool DecompileReferenceMeshSmdFileIsChecked
		{
			get
			{
				return theDecompileReferenceMeshSmdFileIsChecked;
			}
			set
			{
				theDecompileReferenceMeshSmdFileIsChecked = value;
				NotifyPropertyChanged("DecompileReferenceMeshSmdFileIsChecked");
			}
		}

		public bool DecompileBoneAnimationSmdFilesIsChecked
		{
			get
			{
				return theDecompileBoneAnimationSmdFilesIsChecked;
			}
			set
			{
				theDecompileBoneAnimationSmdFilesIsChecked = value;
				NotifyPropertyChanged("DecompileBoneAnimationSmdFilesIsChecked");
			}
		}

		public bool DecompileBoneAnimationPlaceInSubfolderIsChecked
		{
			get
			{
				return theDecompileBoneAnimationPlaceInSubfolderIsChecked;
			}
			set
			{
				theDecompileBoneAnimationPlaceInSubfolderIsChecked = value;
				NotifyPropertyChanged("DecompileBoneAnimationPlaceInSubfolderIsChecked");
			}
		}

		public bool DecompileTextureBmpFilesIsChecked
		{
			get
			{
				return theDecompileTextureBmpFileIsChecked;
			}
			set
			{
				theDecompileTextureBmpFileIsChecked = value;
				NotifyPropertyChanged("DecompileTextureBmpFileIsChecked");
			}
		}

		public bool DecompileLodMeshSmdFilesIsChecked
		{
			get
			{
				return theDecompileLodMeshSmdFilesIsChecked;
			}
			set
			{
				theDecompileLodMeshSmdFilesIsChecked = value;
				NotifyPropertyChanged("DecompileLodMeshSmdFilesIsChecked");
			}
		}

		public bool DecompilePhysicsMeshSmdFileIsChecked
		{
			get
			{
				return theDecompilePhysicsMeshSmdFileIsChecked;
			}
			set
			{
				theDecompilePhysicsMeshSmdFileIsChecked = value;
				NotifyPropertyChanged("DecompilePhysicsMeshSmdFileIsChecked");
			}
		}

		public bool DecompileVertexAnimationVtaFileIsChecked
		{
			get
			{
				return theDecompileVertexAnimationVtaFileIsChecked;
			}
			set
			{
				theDecompileVertexAnimationVtaFileIsChecked = value;
				NotifyPropertyChanged("DecompileVertexAnimationVtaFileIsChecked");
			}
		}

		public bool DecompileProceduralBonesVrdFileIsChecked
		{
			get
			{
				return theDecompileProceduralBonesVrdFileIsChecked;
			}
			set
			{
				theDecompileProceduralBonesVrdFileIsChecked = value;
				NotifyPropertyChanged("DecompileProceduralBonesVrdFileIsChecked");
			}
		}

		public bool DecompileFolderForEachModelIsChecked
		{
			get
			{
				return theDecompileFolderForEachModelIsChecked;
			}
			set
			{
				theDecompileFolderForEachModelIsChecked = value;
				NotifyPropertyChanged("DecompileFolderForEachModelIsChecked");
			}
		}

		public bool DecompilePrefixFileNamesWithModelNameIsChecked
		{
			get
			{
				return theDecompilePrefixFileNamesWithModelNameIsChecked;
			}
			set
			{
				theDecompilePrefixFileNamesWithModelNameIsChecked = value;
				NotifyPropertyChanged("DecompilePrefixFileNamesWithModelNameIsChecked");
			}
		}

		public bool DecompileStricterFormatIsChecked
		{
			get
			{
				return theDecompileStricterFormatIsChecked;
			}
			set
			{
				theDecompileStricterFormatIsChecked = value;
				NotifyPropertyChanged("DecompileStricterFormatIsChecked");
			}
		}

		public bool DecompileLogFileIsChecked
		{
			get
			{
				return theDecompileLogFileIsChecked;
			}
			set
			{
				theDecompileLogFileIsChecked = value;
				NotifyPropertyChanged("DecompileLogFileIsChecked");
			}
		}

		public bool DecompileDebugInfoFilesIsChecked
		{
			get
			{
				return theDecompileDebugInfoFilesIsChecked;
			}
			set
			{
				theDecompileDebugInfoFilesIsChecked = value;
				NotifyPropertyChanged("DecompileDebugInfoFilesIsChecked");
			}
		}

		public bool DecompileDeclareSequenceQciFileIsChecked
		{
			get
			{
				return theDecompileDeclareSequenceQciFileIsChecked;
			}
			set
			{
				theDecompileDeclareSequenceQciFileIsChecked = value;
				NotifyPropertyChanged("DecompileDeclareSequenceQciFileIsChecked");
			}
		}

		public bool DecompileRemovePathFromSmdMaterialFileNamesIsChecked
		{
			get
			{
				return theDecompileRemovePathFromSmdMaterialFileNamesIsChecked;
			}
			set
			{
				theDecompileRemovePathFromSmdMaterialFileNamesIsChecked = value;
				NotifyPropertyChanged("DecompileRemovePathFromSmdMaterialFileNamesIsChecked");
			}
		}

		public bool DecompileUseNonValveUvConversionIsChecked
		{
			get
			{
				return theDecompileUseNonValveUvConversionIsChecked;
			}
			set
			{
				theDecompileUseNonValveUvConversionIsChecked = value;
				NotifyPropertyChanged("DecompileUseNonValveUvConversionIsChecked");
			}
		}

		public AppEnums.SupportedMdlVersion DecompileOverrideMdlVersion
		{
			get
			{
				return theDecompileOverrideMdlVersion;
			}
			set
			{
				theDecompileOverrideMdlVersion = value;
				NotifyPropertyChanged("DecompileOverrideMdlVersion");
			}
		}

		public AppEnums.InputOptions DecompileMode
		{
			get
			{
				return theDecompileMode;
			}
			set
			{
				theDecompileMode = value;
				NotifyPropertyChanged("DecompileMode");
			}
		}

		[XmlIgnore()]
		public bool DecompilerIsRunning
		{
			get
			{
				return theDecompilerIsRunning;
			}
			set
			{
				theDecompilerIsRunning = value;
				NotifyPropertyChanged("DecompilerIsRunning");
			}
		}

		public string CompileQcPathFileName
		{
			get
			{
				return theCompileQcPathFileName;
			}
			set
			{
				theCompileQcPathFileName = value;
				NotifyPropertyChanged("CompileQcPathFileName");
			}
		}

		//Public Property CompileOutputFolderIsChecked() As Boolean
		//	Get
		//		Return Me.theCompileOutputFolderIsChecked
		//	End Get
		//	Set(ByVal value As Boolean)
		//		Me.theCompileOutputFolderIsChecked = value
		//		NotifyPropertyChanged("CompileOutputFolderIsChecked")
		//	End Set
		//End Property

		//Public Property CompileOutputFolderOption() As OutputFolderOptions
		//	Get
		//		Return Me.theCompileOutputFolderOption
		//	End Get
		//	Set(ByVal value As OutputFolderOptions)
		//		Me.theCompileOutputFolderOption = value
		//		NotifyPropertyChanged("CompileOutputFolderOption")
		//	End Set
		//End Property

		public AppEnums.CompileOutputPathOptions CompileOutputFolderOption
		{
			get
			{
				return theCompileOutputFolderOption;
			}
			set
			{
				theCompileOutputFolderOption = value;
				NotifyPropertyChanged("CompileOutputFolderOption");
			}
		}

		public string CompileOutputSubfolderName
		{
			get
			{
				return theCompileOutputSubfolderName;
			}
			set
			{
				theCompileOutputSubfolderName = value;
				NotifyPropertyChanged("CompileOutputSubfolderName");
			}
		}

		public string CompileOutputFullPath
		{
			get
			{
				return theCompileOutputFullPath;
			}
			set
			{
				theCompileOutputFullPath = value;
				NotifyPropertyChanged("CompileOutputFullPath");
			}
		}

		//Public Property CompileOutputPathName() As String
		//	Get
		//		Return Me.theCompileOutputPathName
		//	End Get
		//	Set(ByVal value As String)
		//		Me.theCompileOutputPathName = value
		//		NotifyPropertyChanged("CompileOutputPathName")
		//	End Set
		//End Property

		public int CompileGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (theCompileGameSetupSelectedIndex >= theGameSetups.Count)
				{
					theCompileGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return theCompileGameSetupSelectedIndex;
			}
			set
			{
				theCompileGameSetupSelectedIndex = value;
				NotifyPropertyChanged("CompileGameSetupSelectedIndex");
			}
		}

		public bool CompileGoldSourceLogFileIsChecked
		{
			get
			{
				return theCompileGoldSourceLogFileIsChecked;
			}
			set
			{
				theCompileGoldSourceLogFileIsChecked = value;
				NotifyPropertyChanged("CompileGoldSourceLogFileIsChecked");
			}
		}

		public bool CompileSourceLogFileIsChecked
		{
			get
			{
				return theCompileSourceLogFileIsChecked;
			}
			set
			{
				theCompileSourceLogFileIsChecked = value;
				NotifyPropertyChanged("CompileSourceLogFileIsChecked");
			}
		}

		public bool CompileOptionDefineBonesIsChecked
		{
			get
			{
				return theCompileOptionDefineBonesIsChecked;
			}
			set
			{
				theCompileOptionDefineBonesIsChecked = value;
				NotifyPropertyChanged("CompileOptionDefineBonesIsChecked");
			}
		}

		public bool CompileOptionDefineBonesCreateFileIsChecked
		{
			get
			{
				return theCompileOptionDefineBonesCreateFileIsChecked;
			}
			set
			{
				theCompileOptionDefineBonesCreateFileIsChecked = value;
				NotifyPropertyChanged("CompileOptionDefineBonesCreateFileIsChecked");
			}
		}

		public string CompileOptionDefineBonesQciFileName
		{
			get
			{
				return theCompileOptionDefineBonesQciFileName;
			}
			set
			{
				theCompileOptionDefineBonesQciFileName = value;
				NotifyPropertyChanged("CompileOptionDefineBonesQciFileName");
			}
		}

		public bool CompileOptionDefineBonesOverwriteQciFileIsChecked
		{
			get
			{
				return theCompileOptionDefineBonesOverwriteQciFileIsChecked;
			}
			set
			{
				theCompileOptionDefineBonesOverwriteQciFileIsChecked = value;
				NotifyPropertyChanged("CompileOptionDefineBonesOverwriteQciFileIsChecked");
			}
		}

		public bool CompileOptionDefineBonesModifyQcFileIsChecked
		{
			get
			{
				return theCompileOptionDefineBonesModifyQcFileIsChecked;
			}
			set
			{
				theCompileOptionDefineBonesModifyQcFileIsChecked = value;
				NotifyPropertyChanged("CompileOptionDefineBonesModifyQcFileIsChecked");
			}
		}

		public bool CompileOptionNoP4IsChecked
		{
			get
			{
				return theCompileOptionNoP4IsChecked;
			}
			set
			{
				theCompileOptionNoP4IsChecked = value;
				NotifyPropertyChanged("CompileOptionNoP4IsChecked");
			}
		}

		public bool CompileOptionVerboseIsChecked
		{
			get
			{
				return theCompileOptionVerboseIsChecked;
			}
			set
			{
				theCompileOptionVerboseIsChecked = value;
				NotifyPropertyChanged("CompileOptionVerboseIsChecked");
			}
		}

		[XmlIgnore()]
		public string CompileOptionsText
		{
			get
			{
				return theCompileOptionsText;
			}
			set
			{
				theCompileOptionsText = value;
				NotifyPropertyChanged("CompileOptionsText");
			}
		}

		public AppEnums.InputOptions CompileMode
		{
			get
			{
				return theCompileMode;
			}
			set
			{
				theCompileMode = value;
				NotifyPropertyChanged("CompileMode");
			}
		}

		[XmlIgnore()]
		public bool CompilerIsRunning
		{
			get
			{
				return theCompilerIsRunning;
			}
			set
			{
				theCompilerIsRunning = value;
				NotifyPropertyChanged("CompilerIsRunning");
			}
		}

		public string PatchMdlPathFileName
		{
			get
			{
				return thePatchMdlPathFileName;
			}
			set
			{
				thePatchMdlPathFileName = value;
				NotifyPropertyChanged("PatchMdlPathFileName");
			}
		}

		public AppEnums.InputOptions PatchMode
		{
			get
			{
				return thePatchMode;
			}
			set
			{
				thePatchMode = value;
				NotifyPropertyChanged("PatchMode");
			}
		}

		public string ViewMdlPathFileName
		{
			get
			{
				return theViewMdlPathFileName;
			}
			set
			{
				theViewMdlPathFileName = value;
				NotifyPropertyChanged("ViewMdlPathFileName");
			}
		}

		public AppEnums.SupportedMdlVersion ViewOverrideMdlVersion
		{
			get
			{
				return theViewOverrideMdlVersion;
			}
			set
			{
				theViewOverrideMdlVersion = value;
				NotifyPropertyChanged("ViewOverrideMdlVersion");
			}
		}

		public int ViewGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (theViewGameSetupSelectedIndex >= theGameSetups.Count)
				{
					theViewGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return theViewGameSetupSelectedIndex;
			}
			set
			{
				theViewGameSetupSelectedIndex = value;
				NotifyPropertyChanged("ViewGameSetupSelectedIndex");
			}
		}

		[XmlIgnore()]
		public bool ViewDataViewerIsRunning
		{
			get
			{
				return theViewDataViewerIsRunning;
			}
			set
			{
				theViewDataViewerIsRunning = value;
				NotifyPropertyChanged("ViewDataViewerIsRunning");
			}
		}

		[XmlIgnore()]
		public bool ViewViewerIsRunning
		{
			get
			{
				return theViewViewerIsRunning;
			}
			set
			{
				theViewViewerIsRunning = value;
				NotifyPropertyChanged("ViewerIsRunning");
			}
		}

		public AppEnums.PackInputOptions PackMode
		{
			get
			{
				return thePackMode;
			}
			set
			{
				thePackMode = value;
				NotifyPropertyChanged("PackMode");
			}
		}

		public string PackInputPath
		{
			get
			{
				return thePackInputPathFileName;
			}
			set
			{
				thePackInputPathFileName = value;
				NotifyPropertyChanged("PackInputPath");
			}
		}

		public AppEnums.PackOutputPathOptions PackOutputFolderOption
		{
			get
			{
				return thePackOutputFolderOption;
			}
			set
			{
				thePackOutputFolderOption = value;
				NotifyPropertyChanged("PackOutputFolderOption");
			}
		}

		public string PackOutputParentPath
		{
			get
			{
				return thePackOutputParentPath;
			}
			set
			{
				thePackOutputParentPath = value;
				NotifyPropertyChanged("PackOutputParentPath");
			}
		}

		public string PackOutputPath
		{
			get
			{
				return thePackOutputPath;
			}
			set
			{
				thePackOutputPath = value;
				NotifyPropertyChanged("PackOutputPath");
			}
		}

		public int PackGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (thePackGameSetupSelectedIndex >= theGameSetups.Count)
				{
					thePackGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return thePackGameSetupSelectedIndex;
			}
			set
			{
				thePackGameSetupSelectedIndex = value;
				NotifyPropertyChanged("PackGameSetupSelectedIndex");
			}
		}

		public bool PackLogFileIsChecked
		{
			get
			{
				return thePackLogFileIsChecked;
			}
			set
			{
				thePackLogFileIsChecked = value;
				NotifyPropertyChanged("PackLogFileIsChecked");
			}
		}

		public bool PackOptionMultiFileVpkIsChecked
		{
			get
			{
				return thePackOptionMultiFileVpkIsChecked;
			}
			set
			{
				thePackOptionMultiFileVpkIsChecked = value;
				NotifyPropertyChanged("PackOptionMultiFileVpkIsChecked");
			}
		}

		public string PackGmaTitle
		{
			get
			{
				return thePackGmaTitle;
			}
			set
			{
				thePackGmaTitle = value;
				NotifyPropertyChanged("PackGmaTitle");
			}
		}

		public BindingListEx<string> PackGmaItemTags
		{
			get
			{
				return thePackGmaItemTags;
			}
			set
			{
				thePackGmaItemTags = value;
				NotifyPropertyChanged("PackGmaItemTags");
			}
		}

		[XmlIgnore()]
		public string PackOptionsText
		{
			get
			{
				return thePackOptionsText;
			}
			set
			{
				thePackOptionsText = value;
				NotifyPropertyChanged("PackOptionsText");
			}
		}

		[XmlIgnore()]
		public bool PackerIsRunning
		{
			get
			{
				return thePackerIsRunning;
			}
			set
			{
				thePackerIsRunning = value;
				NotifyPropertyChanged("PackerIsRunning");
			}
		}

		public int PublishGameSelectedIndex
		{
			get
			{
				return thePublishGameSelectedIndex;
			}
			set
			{
				if (thePublishGameSelectedIndex != value)
				{
					thePublishGameSelectedIndex = value;
					NotifyPropertyChanged("PublishGameSelectedIndex");
				}
			}
		}

		public BindingListExAutoSort<SteamAppUserInfo> PublishSteamAppUserInfos
		{
			get
			{
				return thePublishSteamAppUserInfos;
			}
			set
			{
				if (thePublishSteamAppUserInfos != value)
				{
					thePublishSteamAppUserInfos = value;
					NotifyPropertyChanged("PublishSteamAppUserInfos");
				}
			}
		}

		public AppEnums.PublishSearchFieldOptions PublishSearchField
		{
			get
			{
				return thePublishSearchField;
			}
			set
			{
				if (thePublishSearchField != value)
				{
					thePublishSearchField = value;
					NotifyPropertyChanged("PublishSearchField");
				}
			}
		}

		public string PublishSearchText
		{
			get
			{
				return thePublishSearchText;
			}
			set
			{
				if (thePublishSearchText != value)
				{
					thePublishSearchText = value;
					NotifyPropertyChanged("PublishSearchText");
				}
			}
		}

		//<XmlIgnore()>
		//Public Property PublishDragDroppedContentPath() As String
		//	Get
		//		Return Me.thePublishDragDroppedContentPath
		//	End Get
		//	Set(ByVal value As String)
		//		If Me.thePublishDragDroppedContentPath <> value Then
		//			Me.thePublishDragDroppedContentPath = value
		//			NotifyPropertyChanged("PublishDragDroppedContentPath")
		//		End If
		//	End Set
		//End Property

		public bool OptionsAutoOpenVpkFileIsChecked
		{
			get
			{
				return theOptionsAutoOpenVpkFileIsChecked;
			}
			set
			{
				if (theOptionsAutoOpenVpkFileIsChecked != value)
				{
					theOptionsAutoOpenVpkFileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenVpkFileIsChecked");
				}
			}
		}

		public AppEnums.ActionType OptionsAutoOpenVpkFileOption
		{
			get
			{
				return theOptionsAutoOpenVpkFileOption;
			}
			set
			{
				theOptionsAutoOpenVpkFileOption = value;
				NotifyPropertyChanged("OptionsAutoOpenVpkFileOption");
			}
		}

		public bool OptionsAutoOpenGmaFileIsChecked
		{
			get
			{
				return theOptionsAutoOpenGmaFileIsChecked;
			}
			set
			{
				if (theOptionsAutoOpenGmaFileIsChecked != value)
				{
					theOptionsAutoOpenGmaFileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenGmaFileIsChecked");
				}
			}
		}

		public AppEnums.ActionType OptionsAutoOpenGmaFileOption
		{
			get
			{
				return theOptionsAutoOpenGmaFileOption;
			}
			set
			{
				theOptionsAutoOpenGmaFileOption = value;
				NotifyPropertyChanged("OptionsAutoOpenGmaFileOption");
			}
		}

		public bool OptionsAutoOpenFpxFileIsChecked
		{
			get
			{
				return theOptionsAutoOpenFpxFileIsChecked;
			}
			set
			{
				if (theOptionsAutoOpenFpxFileIsChecked != value)
				{
					theOptionsAutoOpenFpxFileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenFpxFileIsChecked");
				}
			}
		}

		public bool OptionsAutoOpenMdlFileIsChecked
		{
			get
			{
				return theOptionsAutoOpenMdlFileIsChecked;
			}
			set
			{
				if (theOptionsAutoOpenMdlFileIsChecked != value)
				{
					theOptionsAutoOpenMdlFileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenMdlFileIsChecked");
				}
			}
		}

		public bool OptionsAutoOpenMdlFileForPreviewIsChecked
		{
			get
			{
				return theOptionsAutoOpenMdlFileForPreviewIsChecked;
			}
			set
			{
				if (theOptionsAutoOpenMdlFileForPreviewIsChecked != value)
				{
					theOptionsAutoOpenMdlFileForPreviewIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenMdlFileForPreviewIsChecked");
				}
			}
		}

		public bool OptionsAutoOpenMdlFileForDecompileIsChecked
		{
			get
			{
				return theOptionsAutoOpenMdlFileForDecompileIsChecked;
			}
			set
			{
				if (theOptionsAutoOpenMdlFileForDecompileIsChecked != value)
				{
					theOptionsAutoOpenMdlFileForDecompileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenMdlFileForDecompileIsChecked");
				}
			}
		}

		public bool OptionsAutoOpenMdlFileForViewIsChecked
		{
			get
			{
				return theOptionsAutoOpenMdlFileForViewIsChecked;
			}
			set
			{
				if (theOptionsAutoOpenMdlFileForViewIsChecked != value)
				{
					theOptionsAutoOpenMdlFileForViewIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenMdlFileForViewIsChecked");
				}
			}
		}

		public AppEnums.ActionType OptionsAutoOpenMdlFileOption
		{
			get
			{
				return theOptionsAutoOpenMdlFileOption;
			}
			set
			{
				theOptionsAutoOpenMdlFileOption = value;
				NotifyPropertyChanged("OptionsAutoOpenMdlFileOption");
			}
		}

		public bool OptionsAutoOpenQcFileIsChecked
		{
			get
			{
				return theOptionsAutoOpenQcFileIsChecked;
			}
			set
			{
				theOptionsAutoOpenQcFileIsChecked = value;
				NotifyPropertyChanged("OptionsAutoOpenQcFileIsChecked");
			}
		}

		public AppEnums.ActionType OptionsAutoOpenFolderOption
		{
			get
			{
				return theOptionsAutoOpenFolderOption;
			}
			set
			{
				theOptionsAutoOpenFolderOption = value;
				NotifyPropertyChanged("OptionsAutoOpenFolderOption");
			}
		}

		public AppEnums.ActionType OptionsDragAndDropVpkFileOption
		{
			get
			{
				return theOptionsDragAndDropVpkFileOption;
			}
			set
			{
				theOptionsDragAndDropVpkFileOption = value;
				NotifyPropertyChanged("OptionsDragAndDropVpkFileOption");
			}
		}

		public AppEnums.ActionType OptionsDragAndDropGmaFileOption
		{
			get
			{
				return theOptionsDragAndDropGmaFileOption;
			}
			set
			{
				theOptionsDragAndDropGmaFileOption = value;
				NotifyPropertyChanged("OptionsDragAndDropGmaFileOption");
			}
		}

		public bool OptionsDragAndDropMdlFileForPreviewIsChecked
		{
			get
			{
				return theOptionsDragAndDropMdlFileForPreviewIsChecked;
			}
			set
			{
				if (theOptionsDragAndDropMdlFileForPreviewIsChecked != value)
				{
					theOptionsDragAndDropMdlFileForPreviewIsChecked = value;
					NotifyPropertyChanged("OptionsDragAndDropMdlFileForPreviewIsChecked");
				}
			}
		}

		public bool OptionsDragAndDropMdlFileForDecompileIsChecked
		{
			get
			{
				return theOptionsDragAndDropMdlFileForDecompileIsChecked;
			}
			set
			{
				if (theOptionsDragAndDropMdlFileForDecompileIsChecked != value)
				{
					theOptionsDragAndDropMdlFileForDecompileIsChecked = value;
					NotifyPropertyChanged("OptionsDragAndDropMdlFileForDecompileIsChecked");
				}
			}
		}

		public bool OptionsDragAndDropMdlFileForViewIsChecked
		{
			get
			{
				return theOptionsDragAndDropMdlFileForViewIsChecked;
			}
			set
			{
				if (theOptionsDragAndDropMdlFileForViewIsChecked != value)
				{
					theOptionsDragAndDropMdlFileForViewIsChecked = value;
					NotifyPropertyChanged("OptionsDragAndDropMdlFileForViewIsChecked");
				}
			}
		}

		public AppEnums.ActionType OptionsDragAndDropMdlFileOption
		{
			get
			{
				return theOptionsDragAndDropMdlFileOption;
			}
			set
			{
				theOptionsDragAndDropMdlFileOption = value;
				NotifyPropertyChanged("OptionsDragAndDropMdlFileOption");
			}
		}

		public AppEnums.ActionType OptionsDragAndDropFolderOption
		{
			get
			{
				return theOptionsDragAndDropFolderOption;
			}
			set
			{
				theOptionsDragAndDropFolderOption = value;
				NotifyPropertyChanged("OptionsDragAndDropFolderOption");
			}
		}

		public bool OptionsContextMenuIntegrateMenuItemsIsChecked
		{
			get
			{
				return theOptionsContextMenuIntegrateMenuItemsIsChecked;
			}
			set
			{
				theOptionsContextMenuIntegrateMenuItemsIsChecked = value;
				NotifyPropertyChanged("OptionsContextMenuIntegrateMenuItemsIsChecked");
			}
		}

		public bool OptionsContextMenuIntegrateSubMenuIsChecked
		{
			get
			{
				return theOptionsContextMenuIntegrateSubMenuIsChecked;
			}
			set
			{
				theOptionsContextMenuIntegrateSubMenuIsChecked = value;
				NotifyPropertyChanged("OptionsContextMenuIntegrateSubMenuIsChecked");
			}
		}

		public bool OptionsOpenWithCrowbarIsChecked
		{
			get
			{
				return theOptionsOpenWithCrowbarIsChecked;
			}
			set
			{
				theOptionsOpenWithCrowbarIsChecked = value;
				NotifyPropertyChanged("OptionsOpenWithCrowbarIsChecked");
			}
		}

		public bool OptionsViewMdlFileIsChecked
		{
			get
			{
				return theOptionsViewMdlFileIsChecked;
			}
			set
			{
				theOptionsViewMdlFileIsChecked = value;
				NotifyPropertyChanged("OptionsViewMdlFileIsChecked");
			}
		}

		public bool OptionsDecompileMdlFileIsChecked
		{
			get
			{
				return theOptionsDecompileMdlFileIsChecked;
			}
			set
			{
				theOptionsDecompileMdlFileIsChecked = value;
				NotifyPropertyChanged("OptionsDecompileMdlFileIsChecked");
			}
		}

		public bool OptionsDecompileFolderIsChecked
		{
			get
			{
				return theOptionsDecompileFolderIsChecked;
			}
			set
			{
				theOptionsDecompileFolderIsChecked = value;
				NotifyPropertyChanged("OptionsDecompileFolderIsChecked");
			}
		}

		public bool OptionsDecompileFolderAndSubfoldersIsChecked
		{
			get
			{
				return theOptionsDecompileFolderAndSubfoldersIsChecked;
			}
			set
			{
				theOptionsDecompileFolderAndSubfoldersIsChecked = value;
				NotifyPropertyChanged("OptionsDecompileFolderAndSubfoldersIsChecked");
			}
		}

		public bool OptionsCompileQcFileIsChecked
		{
			get
			{
				return theOptionsCompileQcFileIsChecked;
			}
			set
			{
				theOptionsCompileQcFileIsChecked = value;
				NotifyPropertyChanged("OptionsCompileQcFileIsChecked");
			}
		}

		public bool OptionsCompileFolderIsChecked
		{
			get
			{
				return theOptionsCompileFolderIsChecked;
			}
			set
			{
				theOptionsCompileFolderIsChecked = value;
				NotifyPropertyChanged("OptionsCompileFolderIsChecked");
			}
		}

		public bool OptionsCompileFolderAndSubfoldersIsChecked
		{
			get
			{
				return theOptionsCompileFolderAndSubfoldersIsChecked;
			}
			set
			{
				theOptionsCompileFolderAndSubfoldersIsChecked = value;
				NotifyPropertyChanged("OptionsCompileFolderAndSubfoldersIsChecked");
			}
		}

		//<XmlElement(Type:=GetType(XmlColor))>
		//Public Property AboutTabBackgroundColor() As Color
		//	Get
		//		Return Me.theAboutTabBackgroundColor
		//	End Get
		//	Set(ByVal value As Color)
		//		Me.theAboutTabBackgroundColor = value
		//	End Set
		//End Property

		public string UpdateDownloadPath
		{
			get
			{
				return theUpdateDownloadPath;
			}
			set
			{
				theUpdateDownloadPath = value;
				NotifyPropertyChanged("UpdateDownloadPath");
			}
		}

		public bool UpdateUpdateToNewPathIsChecked
		{
			get
			{
				return theUpdateUpdateToNewPathIsChecked;
			}
			set
			{
				theUpdateUpdateToNewPathIsChecked = value;
				NotifyPropertyChanged("UpdateUpdateToNewPathIsChecked");
			}
		}

		public string UpdateUpdateDownloadPath
		{
			get
			{
				return theUpdateUpdateDownloadPath;
			}
			set
			{
				theUpdateUpdateDownloadPath = value;
				NotifyPropertyChanged("UpdateUpdateDownloadPath");
			}
		}

		public bool UpdateCopySettingsIsChecked
		{
			get
			{
				return theUpdateCopySettingsIsChecked;
			}
			set
			{
				theUpdateCopySettingsIsChecked = value;
				NotifyPropertyChanged("UpdateCopySettingsIsChecked");
			}
		}
		#endregion

		#region Core Event Handlers

		#endregion

		#region Methods
		public void SetDefaultDownloadOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			DownloadUseItemIdIsChecked = true;
			DownloadPrependItemTitleIsChecked = true;
			DownloadAppendItemUpdateDateTimeIsChecked = true;
			DownloadReplaceSpacesWithUnderscoresIsChecked = true;
			DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked = true;
		}

		public void SetDefaultUnpackOutputSubfolderName()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			UnpackOutputSubfolderName = "unpacked " + ConversionHelper.VersionName;
		}

		public void SetDefaultUnpackOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			UnpackFolderForEachPackageIsChecked = false;
			UnpackKeepFullPathIsChecked = false;
			UnpackLogFileIsChecked = false;
		}

		public void SetDefaultDecompileOutputSubfolderName()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			DecompileOutputSubfolderName = "decompiled " + ConversionHelper.VersionName;
		}

		public void SetDefaultDecompileReCreateFilesOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			DecompileQcFileIsChecked = true;
			DecompileGroupIntoQciFilesIsChecked = false;
			DecompileQcSkinFamilyOnSingleLineIsChecked = true;
			DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked = true;
			DecompileQcIncludeDefineBoneLinesIsChecked = true;
			DecompileQcUseMixedCaseForKeywordsIsChecked = false;

			DecompileReferenceMeshSmdFileIsChecked = true;
			DecompileRemovePathFromSmdMaterialFileNamesIsChecked = true;
			DecompileUseNonValveUvConversionIsChecked = false;

			DecompileBoneAnimationSmdFilesIsChecked = true;
			DecompileBoneAnimationPlaceInSubfolderIsChecked = true;

			DecompileTextureBmpFilesIsChecked = true;
			DecompileLodMeshSmdFilesIsChecked = true;
			DecompilePhysicsMeshSmdFileIsChecked = true;
			DecompileVertexAnimationVtaFileIsChecked = true;
			DecompileProceduralBonesVrdFileIsChecked = true;
		}

		public void SetDefaultCompileOutputSubfolderName()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			CompileOutputSubfolderName = "compiled " + ConversionHelper.VersionName;
		}

		public void SetDefaultCompileOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			CompileGoldSourceLogFileIsChecked = false;
			CompileSourceLogFileIsChecked = false;

			CompileOptionNoP4IsChecked = true;
			CompileOptionVerboseIsChecked = true;

			CompileOptionDefineBonesIsChecked = false;
			CompileOptionDefineBonesCreateFileIsChecked = false;
			CompileOptionDefineBonesQciFileName = "DefineBones";
			CompileOptionDefineBonesOverwriteQciFileIsChecked = false;
			CompileOptionDefineBonesModifyQcFileIsChecked = false;

			CompileOptionsText = string.Empty;
		}

		public void SetDefaultPackOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			PackLogFileIsChecked = false;

			PackOptionMultiFileVpkIsChecked = false;

			PackGmaTitle = string.Empty;
			PackGmaItemTags = new BindingListEx<string>();

			PackOptionsText = string.Empty;
		}

		public void SetDefaultOptionsAutoOpenOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			OptionsAutoOpenVpkFileIsChecked = true;
			OptionsAutoOpenVpkFileOption = AppEnums.ActionType.Unpack;
			OptionsAutoOpenGmaFileIsChecked = true;
			OptionsAutoOpenGmaFileOption = AppEnums.ActionType.Unpack;
			OptionsAutoOpenFpxFileIsChecked = true;

			OptionsAutoOpenMdlFileIsChecked = true;
			OptionsAutoOpenMdlFileForPreviewIsChecked = false;
			OptionsAutoOpenMdlFileForDecompileIsChecked = true;
			OptionsAutoOpenMdlFileForViewIsChecked = false;
			OptionsAutoOpenMdlFileOption = AppEnums.ActionType.Decompile;

			OptionsAutoOpenQcFileIsChecked = true;

			OptionsAutoOpenFolderOption = AppEnums.ActionType.Decompile;
		}

		public void SetDefaultOptionsDragAndDropOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.

			OptionsDragAndDropVpkFileOption = AppEnums.ActionType.Unpack;
			OptionsDragAndDropGmaFileOption = AppEnums.ActionType.Unpack;

			OptionsDragAndDropMdlFileForPreviewIsChecked = false;
			OptionsDragAndDropMdlFileForDecompileIsChecked = true;
			OptionsDragAndDropMdlFileForViewIsChecked = false;
			OptionsDragAndDropMdlFileOption = AppEnums.ActionType.Decompile;

			OptionsDragAndDropFolderOption = AppEnums.ActionType.Decompile;
		}

		public void SetDefaultOptionsContextMenuOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			OptionsContextMenuIntegrateMenuItemsIsChecked = true;
			OptionsContextMenuIntegrateSubMenuIsChecked = true;

			OptionsOpenWithCrowbarIsChecked = true;
			OptionsViewMdlFileIsChecked = true;

			OptionsDecompileMdlFileIsChecked = true;
			OptionsDecompileFolderIsChecked = true;
			OptionsDecompileFolderAndSubfoldersIsChecked = true;

			OptionsCompileQcFileIsChecked = true;
			OptionsCompileFolderIsChecked = true;
			OptionsCompileFolderAndSubfoldersIsChecked = true;
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
	}
}