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

// Purpose: Stores application-related settings, such as UI widget locations and auto-recover data.

namespace Crowbar
{
	public class AppSettings : INotifyPropertyChanged
	{
#region Create and Destroy

		public AppSettings()
		{
			//MyBase.New()

			this.theAppIsSingleInstance = false;
			this.theWindowLocation = new Point(0, 0);
			this.theWindowSize = new Size(800, 600);
			this.theWindowState = FormWindowState.Normal;
			//NOTE: 0 means the Set Up Games tab.
			this.theMainWindowSelectedTabIndex = 0;

			this.thePreviewDataViewerIsRunning = false;
			//Me.thePreviewerIsRunning = False
			this.theDecompilerIsRunning = false;
			this.theCompilerIsRunning = false;
			this.theViewDataViewerIsRunning = false;
			//Me.theViewerIsRunning = False
			this.thePackerIsRunning = false;

			this.theGameSetups = new BindingListExAutoSort<GameSetup>("GameName");
			this.theSteamAppPathFileName = "C:\\Program Files (x86)\\Steam\\Steam.exe";
			this.theSteamLibraryPaths = new BindingListEx<SteamLibraryPath>();
			this.theSetUpGamesGameSetupSelectedIndex = 0;

			this.theDownloadItemIdOrLink = "";
			this.theDownloadOutputFolderOption = AppEnums.DownloadOutputPathOptions.DocumentsFolder;
			this.theDownloadOutputWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.SetDefaultDownloadOptions();

			this.theUnpackContainerType = AppEnums.ContainerType.VPK;
			this.theUnpackPackagePathFolderOrFileName = "";
			//Me.theUnpackOutputFolderOption = OutputFolderOptions.SubfolderName
			this.theUnpackOutputFolderOption = AppEnums.UnpackOutputPathOptions.SameFolder;
			this.SetDefaultUnpackOutputSubfolderName();
			this.theUnpackOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.theUnpackGameSetupSelectedIndex = 0;
			this.theUnpackSearchField = AppEnums.UnpackSearchFieldOptions.Files;
			this.theUnpackSearchText = "";
			this.SetDefaultUnpackOptions();
			this.theUnpackMode = AppEnums.InputOptions.File;

			this.thePreviewMdlPathFileName = "";
			this.thePreviewOverrideMdlVersion = AppEnums.SupportedMdlVersion.DoNotOverride;
			this.thePreviewGameSetupSelectedIndex = 0;

			this.theDecompileMdlPathFileName = "";
			//Me.theDecompileOutputFolderOption = OutputFolderOptions.SubfolderName
			this.theDecompileOutputFolderOption = AppEnums.DecompileOutputPathOptions.WorkFolder;
			this.SetDefaultDecompileOutputSubfolderName();
			this.theDecompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.SetDefaultDecompileReCreateFilesOptions();
			this.theDecompileMode = AppEnums.InputOptions.File;
			this.theDecompileFolderForEachModelIsChecked = false;
			this.theDecompileStricterFormatIsChecked = false;
			this.theDecompileLogFileIsChecked = false;
			this.theDecompileDebugInfoFilesIsChecked = false;
			this.theDecompileOverrideMdlVersion = AppEnums.SupportedMdlVersion.DoNotOverride;

			this.theCompileQcPathFileName = "";
			this.theCompileOutputFolderIsChecked = true;
			//Me.theCompileOutputFolderOption = OutputFolderOptions.SubfolderName
			this.theCompileOutputFolderOption = AppEnums.CompileOutputPathOptions.GameModelsFolder;
			this.SetDefaultCompileOutputSubfolderName();
			this.theCompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.theCompileGameSetupSelectedIndex = 0;
			this.SetDefaultCompileOptions();
			this.theCompileMode = AppEnums.InputOptions.File;

			this.thePatchMdlPathFileName = "";
			this.thePatchMode = AppEnums.InputOptions.File;

			this.theViewMdlPathFileName = "";
			this.theViewOverrideMdlVersion = AppEnums.SupportedMdlVersion.DoNotOverride;
			this.theViewGameSetupSelectedIndex = 0;

			this.thePackInputPathFileName = "";
			//Me.theCompileOutputFolderIsChecked = True
			//'Me.theCompileOutputFolderOption = OutputFolderOptions.SubfolderName
			this.thePackOutputFolderOption = AppEnums.PackOutputPathOptions.ParentFolder;
			//Me.SetDefaultCompileOutputSubfolderName()
			this.thePackOutputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.thePackGameSetupSelectedIndex = 0;
			this.SetDefaultPackOptions();
			//Me.theCompileMode = InputOptions.File

			this.thePublishGameSelectedIndex = 0;
			this.thePublishSteamAppUserInfos = new BindingListExAutoSort<SteamAppUserInfo>("AppID");
			this.thePublishSearchField = AppEnums.PublishSearchFieldOptions.ID;
			this.thePublishSearchText = "";
			//Me.thePublishDragDroppedContentPath = ""

			this.SetDefaultOptionsAutoOpenOptions();
			this.SetDefaultOptionsDragAndDropOptions();
			this.SetDefaultOptionsContextMenuOptions();

			this.theUpdateDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.theUpdateUpdateToNewPathIsChecked = false;
			this.theUpdateUpdateDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			this.theUpdateCopySettingsIsChecked = true;

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
				return this.theMainWindowSelectedTabIndex;
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
				return this.theGameSetups;
			}
			set
			{
				this.theGameSetups = value;
				NotifyPropertyChanged("GameSetups");
			}
		}

		[XmlIgnore()]
		public string SteamAppPathFileName
		{
			get
			{
				return MainCROWBAR.TheApp.GetProcessedPathFileName(this.theSteamAppPathFileName);
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
				return this.theSteamAppPathFileName;
			}
			set
			{
				this.theSteamAppPathFileName = value;
				NotifyPropertyChanged("SteamAppPathFileName");
				NotifyPropertyChanged("SteamAppPathFileNameUnprocessed");
			}
		}

		public BindingListEx<SteamLibraryPath> SteamLibraryPaths
		{
			get
			{
				return this.theSteamLibraryPaths;
			}
			set
			{
				this.theSteamLibraryPaths = value;
				NotifyPropertyChanged("SteamLibraryPaths");
			}
		}

		public int SetUpGamesGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (this.theSetUpGamesGameSetupSelectedIndex >= this.theGameSetups.Count)
				{
					this.theSetUpGamesGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return this.theSetUpGamesGameSetupSelectedIndex;
			}
			set
			{
				this.theSetUpGamesGameSetupSelectedIndex = value;
				NotifyPropertyChanged("SetUpGamesGameSetupSelectedIndex");
			}
		}

		public string DownloadItemIdOrLink
		{
			get
			{
				return this.theDownloadItemIdOrLink;
			}
			set
			{
				if (this.theDownloadItemIdOrLink != value)
				{
					this.theDownloadItemIdOrLink = value;
					NotifyPropertyChanged("DownloadItemIdOrLink");
				}
			}
		}

		public AppEnums.DownloadOutputPathOptions DownloadOutputFolderOption
		{
			get
			{
				return this.theDownloadOutputFolderOption;
			}
			set
			{
				if (this.theDownloadOutputFolderOption != value)
				{
					this.theDownloadOutputFolderOption = value;
					NotifyPropertyChanged("DownloadOutputFolderOption");
				}
			}
		}

		public string DownloadOutputWorkPath
		{
			get
			{
				return this.theDownloadOutputWorkPath;
			}
			set
			{
				if (this.theDownloadOutputWorkPath != value)
				{
					this.theDownloadOutputWorkPath = value;
					NotifyPropertyChanged("DownloadOutputWorkPath");
				}
			}
		}

		public bool DownloadUseItemIdIsChecked
		{
			get
			{
				return this.theDownloadUseItemIdIsChecked;
			}
			set
			{
				if (this.theDownloadUseItemIdIsChecked != value)
				{
					this.theDownloadUseItemIdIsChecked = value;
					NotifyPropertyChanged("DownloadUseItemIdIsChecked");
				}
			}
		}

		public bool DownloadPrependItemTitleIsChecked
		{
			get
			{
				return this.theDownloadPrependItemTitleIsChecked;
			}
			set
			{
				if (this.theDownloadPrependItemTitleIsChecked != value)
				{
					this.theDownloadPrependItemTitleIsChecked = value;
					NotifyPropertyChanged("DownloadPrependItemTitleIsChecked");
				}
			}
		}

		public bool DownloadAppendItemUpdateDateTimeIsChecked
		{
			get
			{
				return this.theDownloadAppendItemUpdateDateTimeIsChecked;
			}
			set
			{
				if (this.theDownloadAppendItemUpdateDateTimeIsChecked != value)
				{
					this.theDownloadAppendItemUpdateDateTimeIsChecked = value;
					NotifyPropertyChanged("DownloadAppendItemUpdateDateTimeIsChecked");
				}
			}
		}

		public bool DownloadReplaceSpacesWithUnderscoresIsChecked
		{
			get
			{
				return this.theDownloadReplaceSpacesWithUnderscoresIsChecked;
			}
			set
			{
				if (this.theDownloadReplaceSpacesWithUnderscoresIsChecked != value)
				{
					this.theDownloadReplaceSpacesWithUnderscoresIsChecked = value;
					NotifyPropertyChanged("DownloadReplaceSpacesWithUnderscoresIsChecked");
				}
			}
		}

		public bool DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked
		{
			get
			{
				return this.theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked;
			}
			set
			{
				if (this.theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked != value)
				{
					this.theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked = value;
					NotifyPropertyChanged("DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked");
				}
			}
		}

		public string UnpackPackagePathFolderOrFileName
		{
			get
			{
				return this.theUnpackPackagePathFolderOrFileName;
			}
			set
			{
				this.theUnpackPackagePathFolderOrFileName = value;
				NotifyPropertyChanged("UnpackPackagePathFolderOrFileName");
			}
		}

		public AppEnums.UnpackOutputPathOptions UnpackOutputFolderOption
		{
			get
			{
				return this.theUnpackOutputFolderOption;
			}
			set
			{
				this.theUnpackOutputFolderOption = value;
				NotifyPropertyChanged("UnpackOutputFolderOption");
			}
		}

		public string UnpackOutputSamePath
		{
			get
			{
				return this.theUnpackOutputSamePath;
			}
			set
			{
				this.theUnpackOutputSamePath = value;
				NotifyPropertyChanged("UnpackOutputSamePath");
			}
		}

		public string UnpackOutputSubfolderName
		{
			get
			{
				return this.theUnpackOutputSubfolderName;
			}
			set
			{
				this.theUnpackOutputSubfolderName = value;
				NotifyPropertyChanged("UnpackOutputSubfolderName");
			}
		}

		public string UnpackOutputFullPath
		{
			get
			{
				return this.theUnpackOutputFullPath;
			}
			set
			{
				this.theUnpackOutputFullPath = value;
				NotifyPropertyChanged("UnpackOutputFullPath");
			}
		}

		public int UnpackGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (this.theUnpackGameSetupSelectedIndex >= this.theGameSetups.Count)
				{
					this.theUnpackGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return this.theUnpackGameSetupSelectedIndex;
			}
			set
			{
				this.theUnpackGameSetupSelectedIndex = value;
				NotifyPropertyChanged("UnpackGameSetupSelectedIndex");
			}
		}

		public AppEnums.UnpackSearchFieldOptions UnpackSearchField
		{
			get
			{
				return this.theUnpackSearchField;
			}
			set
			{
				if (this.theUnpackSearchField != value)
				{
					this.theUnpackSearchField = value;
					NotifyPropertyChanged("UnpackSearchField");
				}
			}
		}

		public string UnpackSearchText
		{
			get
			{
				return this.theUnpackSearchText;
			}
			set
			{
				if (this.theUnpackSearchText != value)
				{
					this.theUnpackSearchText = value;
					NotifyPropertyChanged("UnpackSearchText");
				}
			}
		}

		public bool UnpackFolderForEachPackageIsChecked
		{
			get
			{
				return this.theUnpackFolderForEachPackageIsChecked;
			}
			set
			{
				this.theUnpackFolderForEachPackageIsChecked = value;
				NotifyPropertyChanged("UnpackFolderForEachPackageIsChecked");
			}
		}

		public bool UnpackKeepFullPathIsChecked
		{
			get
			{
				return this.theUnpackKeepFullPathIsChecked;
			}
			set
			{
				this.theUnpackKeepFullPathIsChecked = value;
				NotifyPropertyChanged("UnpackKeepFullPathIsChecked");
			}
		}

		public bool UnpackLogFileIsChecked
		{
			get
			{
				return this.theUnpackLogFileIsChecked;
			}
			set
			{
				this.theUnpackLogFileIsChecked = value;
				NotifyPropertyChanged("UnpackLogFileIsChecked");
			}
		}

		public AppEnums.InputOptions UnpackMode
		{
			get
			{
				return this.theUnpackMode;
			}
			set
			{
				if (this.theUnpackMode != value)
				{
					this.theUnpackMode = value;
					NotifyPropertyChanged("UnpackMode");
				}
			}
		}

		[XmlIgnore()]
		public bool UnpackerIsRunning
		{
			get
			{
				return this.theUnpackerIsRunning;
			}
			set
			{
				this.theUnpackerIsRunning = value;
				NotifyPropertyChanged("UnpackerIsRunning");
			}
		}

		public string PreviewMdlPathFileName
		{
			get
			{
				return this.thePreviewMdlPathFileName;
			}
			set
			{
				this.thePreviewMdlPathFileName = value;
				NotifyPropertyChanged("PreviewMdlPathFileName");
			}
		}

		public AppEnums.SupportedMdlVersion PreviewOverrideMdlVersion
		{
			get
			{
				return this.thePreviewOverrideMdlVersion;
			}
			set
			{
				this.thePreviewOverrideMdlVersion = value;
				NotifyPropertyChanged("PreviewOverrideMdlVersion");
			}
		}

		public int PreviewGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (this.thePreviewGameSetupSelectedIndex >= this.theGameSetups.Count)
				{
					this.thePreviewGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return this.thePreviewGameSetupSelectedIndex;
			}
			set
			{
				this.thePreviewGameSetupSelectedIndex = value;
				NotifyPropertyChanged("PreviewGameSetupSelectedIndex");
			}
		}

		[XmlIgnore()]
		public bool PreviewDataViewerIsRunning
		{
			get
			{
				return this.thePreviewDataViewerIsRunning;
			}
			set
			{
				this.thePreviewDataViewerIsRunning = value;
				NotifyPropertyChanged("PreviewDataViewerIsRunning");
			}
		}

		[XmlIgnore()]
		public bool PreviewViewerIsRunning
		{
			get
			{
				return this.thePreviewViewerIsRunning;
			}
			set
			{
				this.thePreviewViewerIsRunning = value;
				NotifyPropertyChanged("PreviewViewerIsRunning");
			}
		}

		public string DecompileMdlPathFileName
		{
			get
			{
				return this.theDecompileMdlPathFileName;
			}
			set
			{
				this.theDecompileMdlPathFileName = value;
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
				return this.theDecompileOutputFolderOption;
			}
			set
			{
				this.theDecompileOutputFolderOption = value;
				NotifyPropertyChanged("DecompileOutputFolderOption");
			}
		}

		public string DecompileOutputSubfolderName
		{
			get
			{
				return this.theDecompileOutputSubfolderName;
			}
			set
			{
				this.theDecompileOutputSubfolderName = value;
				NotifyPropertyChanged("DecompileOutputSubfolderName");
			}
		}

		public string DecompileOutputFullPath
		{
			get
			{
				return this.theDecompileOutputFullPath;
			}
			set
			{
				this.theDecompileOutputFullPath = value;
				NotifyPropertyChanged("DecompileOutputFullPath");
			}
		}

		public bool DecompileQcFileIsChecked
		{
			get
			{
				return this.theDecompileQcFileIsChecked;
			}
			set
			{
				this.theDecompileQcFileIsChecked = value;
				NotifyPropertyChanged("DecompileQcFileIsChecked");
			}
		}

		public bool DecompileGroupIntoQciFilesIsChecked
		{
			get
			{
				return this.theDecompileGroupIntoQciFilesIsChecked;
			}
			set
			{
				this.theDecompileGroupIntoQciFilesIsChecked = value;
				NotifyPropertyChanged("DecompileGroupIntoQciFilesIsChecked");
			}
		}

		public bool DecompileQcSkinFamilyOnSingleLineIsChecked
		{
			get
			{
				return this.theDecompileQcSkinFamilyOnSingleLineIsChecked;
			}
			set
			{
				this.theDecompileQcSkinFamilyOnSingleLineIsChecked = value;
				NotifyPropertyChanged("DecompileQcSkinFamilyOnSingleLineIsChecked");
			}
		}

		public bool DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked
		{
			get
			{
				return this.theDecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked;
			}
			set
			{
				this.theDecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked = value;
				NotifyPropertyChanged("DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked");
			}
		}

		public bool DecompileQcIncludeDefineBoneLinesIsChecked
		{
			get
			{
				return this.theDecompileQcIncludeDefineBoneLinesIsChecked;
			}
			set
			{
				this.theDecompileQcIncludeDefineBoneLinesIsChecked = value;
				NotifyPropertyChanged("DecompileQcIncludeDefineBoneLinesIsChecked");
			}
		}

		public bool DecompileQcUseMixedCaseForKeywordsIsChecked
		{
			get
			{
				return this.theDecompileQcUseMixedCaseForKeywordsIsChecked;
			}
			set
			{
				this.theDecompileQcUseMixedCaseForKeywordsIsChecked = value;
				NotifyPropertyChanged("DecompileQcUseMixedCaseForKeywordsIsChecked");
			}
		}

		public bool DecompileReferenceMeshSmdFileIsChecked
		{
			get
			{
				return this.theDecompileReferenceMeshSmdFileIsChecked;
			}
			set
			{
				this.theDecompileReferenceMeshSmdFileIsChecked = value;
				NotifyPropertyChanged("DecompileReferenceMeshSmdFileIsChecked");
			}
		}

		public bool DecompileBoneAnimationSmdFilesIsChecked
		{
			get
			{
				return this.theDecompileBoneAnimationSmdFilesIsChecked;
			}
			set
			{
				this.theDecompileBoneAnimationSmdFilesIsChecked = value;
				NotifyPropertyChanged("DecompileBoneAnimationSmdFilesIsChecked");
			}
		}

		public bool DecompileBoneAnimationPlaceInSubfolderIsChecked
		{
			get
			{
				return this.theDecompileBoneAnimationPlaceInSubfolderIsChecked;
			}
			set
			{
				this.theDecompileBoneAnimationPlaceInSubfolderIsChecked = value;
				NotifyPropertyChanged("DecompileBoneAnimationPlaceInSubfolderIsChecked");
			}
		}

		public bool DecompileTextureBmpFilesIsChecked
		{
			get
			{
				return this.theDecompileTextureBmpFileIsChecked;
			}
			set
			{
				this.theDecompileTextureBmpFileIsChecked = value;
				NotifyPropertyChanged("DecompileTextureBmpFileIsChecked");
			}
		}

		public bool DecompileLodMeshSmdFilesIsChecked
		{
			get
			{
				return this.theDecompileLodMeshSmdFilesIsChecked;
			}
			set
			{
				this.theDecompileLodMeshSmdFilesIsChecked = value;
				NotifyPropertyChanged("DecompileLodMeshSmdFilesIsChecked");
			}
		}

		public bool DecompilePhysicsMeshSmdFileIsChecked
		{
			get
			{
				return this.theDecompilePhysicsMeshSmdFileIsChecked;
			}
			set
			{
				this.theDecompilePhysicsMeshSmdFileIsChecked = value;
				NotifyPropertyChanged("DecompilePhysicsMeshSmdFileIsChecked");
			}
		}

		public bool DecompileVertexAnimationVtaFileIsChecked
		{
			get
			{
				return this.theDecompileVertexAnimationVtaFileIsChecked;
			}
			set
			{
				this.theDecompileVertexAnimationVtaFileIsChecked = value;
				NotifyPropertyChanged("DecompileVertexAnimationVtaFileIsChecked");
			}
		}

		public bool DecompileProceduralBonesVrdFileIsChecked
		{
			get
			{
				return this.theDecompileProceduralBonesVrdFileIsChecked;
			}
			set
			{
				this.theDecompileProceduralBonesVrdFileIsChecked = value;
				NotifyPropertyChanged("DecompileProceduralBonesVrdFileIsChecked");
			}
		}

		public bool DecompileFolderForEachModelIsChecked
		{
			get
			{
				return this.theDecompileFolderForEachModelIsChecked;
			}
			set
			{
				this.theDecompileFolderForEachModelIsChecked = value;
				NotifyPropertyChanged("DecompileFolderForEachModelIsChecked");
			}
		}

		public bool DecompilePrefixFileNamesWithModelNameIsChecked
		{
			get
			{
				return this.theDecompilePrefixFileNamesWithModelNameIsChecked;
			}
			set
			{
				this.theDecompilePrefixFileNamesWithModelNameIsChecked = value;
				NotifyPropertyChanged("DecompilePrefixFileNamesWithModelNameIsChecked");
			}
		}

		public bool DecompileStricterFormatIsChecked
		{
			get
			{
				return this.theDecompileStricterFormatIsChecked;
			}
			set
			{
				this.theDecompileStricterFormatIsChecked = value;
				NotifyPropertyChanged("DecompileStricterFormatIsChecked");
			}
		}

		public bool DecompileLogFileIsChecked
		{
			get
			{
				return this.theDecompileLogFileIsChecked;
			}
			set
			{
				this.theDecompileLogFileIsChecked = value;
				NotifyPropertyChanged("DecompileLogFileIsChecked");
			}
		}

		public bool DecompileDebugInfoFilesIsChecked
		{
			get
			{
				return this.theDecompileDebugInfoFilesIsChecked;
			}
			set
			{
				this.theDecompileDebugInfoFilesIsChecked = value;
				NotifyPropertyChanged("DecompileDebugInfoFilesIsChecked");
			}
		}

		public bool DecompileDeclareSequenceQciFileIsChecked
		{
			get
			{
				return this.theDecompileDeclareSequenceQciFileIsChecked;
			}
			set
			{
				this.theDecompileDeclareSequenceQciFileIsChecked = value;
				NotifyPropertyChanged("DecompileDeclareSequenceQciFileIsChecked");
			}
		}

		public bool DecompileRemovePathFromSmdMaterialFileNamesIsChecked
		{
			get
			{
				return this.theDecompileRemovePathFromSmdMaterialFileNamesIsChecked;
			}
			set
			{
				this.theDecompileRemovePathFromSmdMaterialFileNamesIsChecked = value;
				NotifyPropertyChanged("DecompileRemovePathFromSmdMaterialFileNamesIsChecked");
			}
		}

		public bool DecompileUseNonValveUvConversionIsChecked
		{
			get
			{
				return this.theDecompileUseNonValveUvConversionIsChecked;
			}
			set
			{
				this.theDecompileUseNonValveUvConversionIsChecked = value;
				NotifyPropertyChanged("DecompileUseNonValveUvConversionIsChecked");
			}
		}

		public AppEnums.SupportedMdlVersion DecompileOverrideMdlVersion
		{
			get
			{
				return this.theDecompileOverrideMdlVersion;
			}
			set
			{
				this.theDecompileOverrideMdlVersion = value;
				NotifyPropertyChanged("DecompileOverrideMdlVersion");
			}
		}

		public AppEnums.InputOptions DecompileMode
		{
			get
			{
				return this.theDecompileMode;
			}
			set
			{
				this.theDecompileMode = value;
				NotifyPropertyChanged("DecompileMode");
			}
		}

		[XmlIgnore()]
		public bool DecompilerIsRunning
		{
			get
			{
				return this.theDecompilerIsRunning;
			}
			set
			{
				this.theDecompilerIsRunning = value;
				NotifyPropertyChanged("DecompilerIsRunning");
			}
		}

		public string CompileQcPathFileName
		{
			get
			{
				return this.theCompileQcPathFileName;
			}
			set
			{
				this.theCompileQcPathFileName = value;
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
				return this.theCompileOutputFolderOption;
			}
			set
			{
				this.theCompileOutputFolderOption = value;
				NotifyPropertyChanged("CompileOutputFolderOption");
			}
		}

		public string CompileOutputSubfolderName
		{
			get
			{
				return this.theCompileOutputSubfolderName;
			}
			set
			{
				this.theCompileOutputSubfolderName = value;
				NotifyPropertyChanged("CompileOutputSubfolderName");
			}
		}

		public string CompileOutputFullPath
		{
			get
			{
				return this.theCompileOutputFullPath;
			}
			set
			{
				this.theCompileOutputFullPath = value;
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
				if (this.theCompileGameSetupSelectedIndex >= this.theGameSetups.Count)
				{
					this.theCompileGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return this.theCompileGameSetupSelectedIndex;
			}
			set
			{
				this.theCompileGameSetupSelectedIndex = value;
				NotifyPropertyChanged("CompileGameSetupSelectedIndex");
			}
		}

		public bool CompileGoldSourceLogFileIsChecked
		{
			get
			{
				return this.theCompileGoldSourceLogFileIsChecked;
			}
			set
			{
				this.theCompileGoldSourceLogFileIsChecked = value;
				NotifyPropertyChanged("CompileGoldSourceLogFileIsChecked");
			}
		}

		public bool CompileSourceLogFileIsChecked
		{
			get
			{
				return this.theCompileSourceLogFileIsChecked;
			}
			set
			{
				this.theCompileSourceLogFileIsChecked = value;
				NotifyPropertyChanged("CompileSourceLogFileIsChecked");
			}
		}

		public bool CompileOptionDefineBonesIsChecked
		{
			get
			{
				return this.theCompileOptionDefineBonesIsChecked;
			}
			set
			{
				this.theCompileOptionDefineBonesIsChecked = value;
				NotifyPropertyChanged("CompileOptionDefineBonesIsChecked");
			}
		}

		public bool CompileOptionDefineBonesCreateFileIsChecked
		{
			get
			{
				return this.theCompileOptionDefineBonesCreateFileIsChecked;
			}
			set
			{
				this.theCompileOptionDefineBonesCreateFileIsChecked = value;
				NotifyPropertyChanged("CompileOptionDefineBonesCreateFileIsChecked");
			}
		}

		public string CompileOptionDefineBonesQciFileName
		{
			get
			{
				return this.theCompileOptionDefineBonesQciFileName;
			}
			set
			{
				this.theCompileOptionDefineBonesQciFileName = value;
				NotifyPropertyChanged("CompileOptionDefineBonesQciFileName");
			}
		}

		public bool CompileOptionDefineBonesOverwriteQciFileIsChecked
		{
			get
			{
				return this.theCompileOptionDefineBonesOverwriteQciFileIsChecked;
			}
			set
			{
				this.theCompileOptionDefineBonesOverwriteQciFileIsChecked = value;
				NotifyPropertyChanged("CompileOptionDefineBonesOverwriteQciFileIsChecked");
			}
		}

		public bool CompileOptionDefineBonesModifyQcFileIsChecked
		{
			get
			{
				return this.theCompileOptionDefineBonesModifyQcFileIsChecked;
			}
			set
			{
				this.theCompileOptionDefineBonesModifyQcFileIsChecked = value;
				NotifyPropertyChanged("CompileOptionDefineBonesModifyQcFileIsChecked");
			}
		}

		public bool CompileOptionNoP4IsChecked
		{
			get
			{
				return this.theCompileOptionNoP4IsChecked;
			}
			set
			{
				this.theCompileOptionNoP4IsChecked = value;
				NotifyPropertyChanged("CompileOptionNoP4IsChecked");
			}
		}

		public bool CompileOptionVerboseIsChecked
		{
			get
			{
				return this.theCompileOptionVerboseIsChecked;
			}
			set
			{
				this.theCompileOptionVerboseIsChecked = value;
				NotifyPropertyChanged("CompileOptionVerboseIsChecked");
			}
		}

		[XmlIgnore()]
		public string CompileOptionsText
		{
			get
			{
				return this.theCompileOptionsText;
			}
			set
			{
				this.theCompileOptionsText = value;
				NotifyPropertyChanged("CompileOptionsText");
			}
		}

		public AppEnums.InputOptions CompileMode
		{
			get
			{
				return this.theCompileMode;
			}
			set
			{
				this.theCompileMode = value;
				NotifyPropertyChanged("CompileMode");
			}
		}

		[XmlIgnore()]
		public bool CompilerIsRunning
		{
			get
			{
				return this.theCompilerIsRunning;
			}
			set
			{
				this.theCompilerIsRunning = value;
				NotifyPropertyChanged("CompilerIsRunning");
			}
		}

		public string PatchMdlPathFileName
		{
			get
			{
				return this.thePatchMdlPathFileName;
			}
			set
			{
				this.thePatchMdlPathFileName = value;
				NotifyPropertyChanged("PatchMdlPathFileName");
			}
		}

		public AppEnums.InputOptions PatchMode
		{
			get
			{
				return this.thePatchMode;
			}
			set
			{
				this.thePatchMode = value;
				NotifyPropertyChanged("PatchMode");
			}
		}

		public string ViewMdlPathFileName
		{
			get
			{
				return this.theViewMdlPathFileName;
			}
			set
			{
				this.theViewMdlPathFileName = value;
				NotifyPropertyChanged("ViewMdlPathFileName");
			}
		}

		public AppEnums.SupportedMdlVersion ViewOverrideMdlVersion
		{
			get
			{
				return this.theViewOverrideMdlVersion;
			}
			set
			{
				this.theViewOverrideMdlVersion = value;
				NotifyPropertyChanged("ViewOverrideMdlVersion");
			}
		}

		public int ViewGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (this.theViewGameSetupSelectedIndex >= this.theGameSetups.Count)
				{
					this.theViewGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return this.theViewGameSetupSelectedIndex;
			}
			set
			{
				this.theViewGameSetupSelectedIndex = value;
				NotifyPropertyChanged("ViewGameSetupSelectedIndex");
			}
		}

		[XmlIgnore()]
		public bool ViewDataViewerIsRunning
		{
			get
			{
				return this.theViewDataViewerIsRunning;
			}
			set
			{
				this.theViewDataViewerIsRunning = value;
				NotifyPropertyChanged("ViewDataViewerIsRunning");
			}
		}

		[XmlIgnore()]
		public bool ViewViewerIsRunning
		{
			get
			{
				return this.theViewViewerIsRunning;
			}
			set
			{
				this.theViewViewerIsRunning = value;
				NotifyPropertyChanged("ViewerIsRunning");
			}
		}

		public AppEnums.PackInputOptions PackMode
		{
			get
			{
				return this.thePackMode;
			}
			set
			{
				this.thePackMode = value;
				NotifyPropertyChanged("PackMode");
			}
		}

		public string PackInputPath
		{
			get
			{
				return this.thePackInputPathFileName;
			}
			set
			{
				this.thePackInputPathFileName = value;
				NotifyPropertyChanged("PackInputPath");
			}
		}

		public AppEnums.PackOutputPathOptions PackOutputFolderOption
		{
			get
			{
				return this.thePackOutputFolderOption;
			}
			set
			{
				this.thePackOutputFolderOption = value;
				NotifyPropertyChanged("PackOutputFolderOption");
			}
		}

		public string PackOutputParentPath
		{
			get
			{
				return this.thePackOutputParentPath;
			}
			set
			{
				this.thePackOutputParentPath = value;
				NotifyPropertyChanged("PackOutputParentPath");
			}
		}

		public string PackOutputPath
		{
			get
			{
				return this.thePackOutputPath;
			}
			set
			{
				this.thePackOutputPath = value;
				NotifyPropertyChanged("PackOutputPath");
			}
		}

		public int PackGameSetupSelectedIndex
		{
			get
			{
				//NOTE: Must change in the Get() because theGameSetups might not have been read-in yet (i.e. GameSetups appear *after* this setting in XML file).
				if (this.thePackGameSetupSelectedIndex >= this.theGameSetups.Count)
				{
					this.thePackGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.Count - 1;
				}
				return this.thePackGameSetupSelectedIndex;
			}
			set
			{
				this.thePackGameSetupSelectedIndex = value;
				NotifyPropertyChanged("PackGameSetupSelectedIndex");
			}
		}

		public bool PackLogFileIsChecked
		{
			get
			{
				return this.thePackLogFileIsChecked;
			}
			set
			{
				this.thePackLogFileIsChecked = value;
				NotifyPropertyChanged("PackLogFileIsChecked");
			}
		}

		public bool PackOptionMultiFileVpkIsChecked
		{
			get
			{
				return this.thePackOptionMultiFileVpkIsChecked;
			}
			set
			{
				this.thePackOptionMultiFileVpkIsChecked = value;
				NotifyPropertyChanged("PackOptionMultiFileVpkIsChecked");
			}
		}

		public string PackGmaTitle
		{
			get
			{
				return this.thePackGmaTitle;
			}
			set
			{
				this.thePackGmaTitle = value;
				NotifyPropertyChanged("PackGmaTitle");
			}
		}

		public BindingListEx<string> PackGmaItemTags
		{
			get
			{
				return this.thePackGmaItemTags;
			}
			set
			{
				this.thePackGmaItemTags = value;
				NotifyPropertyChanged("PackGmaItemTags");
			}
		}

		[XmlIgnore()]
		public string PackOptionsText
		{
			get
			{
				return this.thePackOptionsText;
			}
			set
			{
				this.thePackOptionsText = value;
				NotifyPropertyChanged("PackOptionsText");
			}
		}

		[XmlIgnore()]
		public bool PackerIsRunning
		{
			get
			{
				return this.thePackerIsRunning;
			}
			set
			{
				this.thePackerIsRunning = value;
				NotifyPropertyChanged("PackerIsRunning");
			}
		}

		public int PublishGameSelectedIndex
		{
			get
			{
				return this.thePublishGameSelectedIndex;
			}
			set
			{
				if (this.thePublishGameSelectedIndex != value)
				{
					this.thePublishGameSelectedIndex = value;
					NotifyPropertyChanged("PublishGameSelectedIndex");
				}
			}
		}

		public BindingListExAutoSort<SteamAppUserInfo> PublishSteamAppUserInfos
		{
			get
			{
				return this.thePublishSteamAppUserInfos;
			}
			set
			{
				if (this.thePublishSteamAppUserInfos != value)
				{
					this.thePublishSteamAppUserInfos = value;
					NotifyPropertyChanged("PublishSteamAppUserInfos");
				}
			}
		}

		public AppEnums.PublishSearchFieldOptions PublishSearchField
		{
			get
			{
				return this.thePublishSearchField;
			}
			set
			{
				if (this.thePublishSearchField != value)
				{
					this.thePublishSearchField = value;
					NotifyPropertyChanged("PublishSearchField");
				}
			}
		}

		public string PublishSearchText
		{
			get
			{
				return this.thePublishSearchText;
			}
			set
			{
				if (this.thePublishSearchText != value)
				{
					this.thePublishSearchText = value;
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
				return this.theOptionsAutoOpenVpkFileIsChecked;
			}
			set
			{
				if (this.theOptionsAutoOpenVpkFileIsChecked != value)
				{
					this.theOptionsAutoOpenVpkFileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenVpkFileIsChecked");
				}
			}
		}

		public AppEnums.ActionType OptionsAutoOpenVpkFileOption
		{
			get
			{
				return this.theOptionsAutoOpenVpkFileOption;
			}
			set
			{
				this.theOptionsAutoOpenVpkFileOption = value;
				NotifyPropertyChanged("OptionsAutoOpenVpkFileOption");
			}
		}

		public bool OptionsAutoOpenGmaFileIsChecked
		{
			get
			{
				return this.theOptionsAutoOpenGmaFileIsChecked;
			}
			set
			{
				if (this.theOptionsAutoOpenGmaFileIsChecked != value)
				{
					this.theOptionsAutoOpenGmaFileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenGmaFileIsChecked");
				}
			}
		}

		public AppEnums.ActionType OptionsAutoOpenGmaFileOption
		{
			get
			{
				return this.theOptionsAutoOpenGmaFileOption;
			}
			set
			{
				this.theOptionsAutoOpenGmaFileOption = value;
				NotifyPropertyChanged("OptionsAutoOpenGmaFileOption");
			}
		}

		public bool OptionsAutoOpenFpxFileIsChecked
		{
			get
			{
				return this.theOptionsAutoOpenFpxFileIsChecked;
			}
			set
			{
				if (this.theOptionsAutoOpenFpxFileIsChecked != value)
				{
					this.theOptionsAutoOpenFpxFileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenFpxFileIsChecked");
				}
			}
		}

		public bool OptionsAutoOpenMdlFileIsChecked
		{
			get
			{
				return this.theOptionsAutoOpenMdlFileIsChecked;
			}
			set
			{
				if (this.theOptionsAutoOpenMdlFileIsChecked != value)
				{
					this.theOptionsAutoOpenMdlFileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenMdlFileIsChecked");
				}
			}
		}

		public bool OptionsAutoOpenMdlFileForPreviewIsChecked
		{
			get
			{
				return this.theOptionsAutoOpenMdlFileForPreviewIsChecked;
			}
			set
			{
				if (this.theOptionsAutoOpenMdlFileForPreviewIsChecked != value)
				{
					this.theOptionsAutoOpenMdlFileForPreviewIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenMdlFileForPreviewIsChecked");
				}
			}
		}

		public bool OptionsAutoOpenMdlFileForDecompileIsChecked
		{
			get
			{
				return this.theOptionsAutoOpenMdlFileForDecompileIsChecked;
			}
			set
			{
				if (this.theOptionsAutoOpenMdlFileForDecompileIsChecked != value)
				{
					this.theOptionsAutoOpenMdlFileForDecompileIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenMdlFileForDecompileIsChecked");
				}
			}
		}

		public bool OptionsAutoOpenMdlFileForViewIsChecked
		{
			get
			{
				return this.theOptionsAutoOpenMdlFileForViewIsChecked;
			}
			set
			{
				if (this.theOptionsAutoOpenMdlFileForViewIsChecked != value)
				{
					this.theOptionsAutoOpenMdlFileForViewIsChecked = value;
					NotifyPropertyChanged("OptionsAutoOpenMdlFileForViewIsChecked");
				}
			}
		}

		public AppEnums.ActionType OptionsAutoOpenMdlFileOption
		{
			get
			{
				return this.theOptionsAutoOpenMdlFileOption;
			}
			set
			{
				this.theOptionsAutoOpenMdlFileOption = value;
				NotifyPropertyChanged("OptionsAutoOpenMdlFileOption");
			}
		}

		public bool OptionsAutoOpenQcFileIsChecked
		{
			get
			{
				return this.theOptionsAutoOpenQcFileIsChecked;
			}
			set
			{
				this.theOptionsAutoOpenQcFileIsChecked = value;
				NotifyPropertyChanged("OptionsAutoOpenQcFileIsChecked");
			}
		}

		public AppEnums.ActionType OptionsAutoOpenFolderOption
		{
			get
			{
				return this.theOptionsAutoOpenFolderOption;
			}
			set
			{
				this.theOptionsAutoOpenFolderOption = value;
				NotifyPropertyChanged("OptionsAutoOpenFolderOption");
			}
		}

		public AppEnums.ActionType OptionsDragAndDropVpkFileOption
		{
			get
			{
				return this.theOptionsDragAndDropVpkFileOption;
			}
			set
			{
				this.theOptionsDragAndDropVpkFileOption = value;
				NotifyPropertyChanged("OptionsDragAndDropVpkFileOption");
			}
		}

		public AppEnums.ActionType OptionsDragAndDropGmaFileOption
		{
			get
			{
				return this.theOptionsDragAndDropGmaFileOption;
			}
			set
			{
				this.theOptionsDragAndDropGmaFileOption = value;
				NotifyPropertyChanged("OptionsDragAndDropGmaFileOption");
			}
		}

		public bool OptionsDragAndDropMdlFileForPreviewIsChecked
		{
			get
			{
				return this.theOptionsDragAndDropMdlFileForPreviewIsChecked;
			}
			set
			{
				if (this.theOptionsDragAndDropMdlFileForPreviewIsChecked != value)
				{
					this.theOptionsDragAndDropMdlFileForPreviewIsChecked = value;
					NotifyPropertyChanged("OptionsDragAndDropMdlFileForPreviewIsChecked");
				}
			}
		}

		public bool OptionsDragAndDropMdlFileForDecompileIsChecked
		{
			get
			{
				return this.theOptionsDragAndDropMdlFileForDecompileIsChecked;
			}
			set
			{
				if (this.theOptionsDragAndDropMdlFileForDecompileIsChecked != value)
				{
					this.theOptionsDragAndDropMdlFileForDecompileIsChecked = value;
					NotifyPropertyChanged("OptionsDragAndDropMdlFileForDecompileIsChecked");
				}
			}
		}

		public bool OptionsDragAndDropMdlFileForViewIsChecked
		{
			get
			{
				return this.theOptionsDragAndDropMdlFileForViewIsChecked;
			}
			set
			{
				if (this.theOptionsDragAndDropMdlFileForViewIsChecked != value)
				{
					this.theOptionsDragAndDropMdlFileForViewIsChecked = value;
					NotifyPropertyChanged("OptionsDragAndDropMdlFileForViewIsChecked");
				}
			}
		}

		public AppEnums.ActionType OptionsDragAndDropMdlFileOption
		{
			get
			{
				return this.theOptionsDragAndDropMdlFileOption;
			}
			set
			{
				this.theOptionsDragAndDropMdlFileOption = value;
				NotifyPropertyChanged("OptionsDragAndDropMdlFileOption");
			}
		}

		public AppEnums.ActionType OptionsDragAndDropFolderOption
		{
			get
			{
				return this.theOptionsDragAndDropFolderOption;
			}
			set
			{
				this.theOptionsDragAndDropFolderOption = value;
				NotifyPropertyChanged("OptionsDragAndDropFolderOption");
			}
		}

		public bool OptionsContextMenuIntegrateMenuItemsIsChecked
		{
			get
			{
				return this.theOptionsContextMenuIntegrateMenuItemsIsChecked;
			}
			set
			{
				this.theOptionsContextMenuIntegrateMenuItemsIsChecked = value;
				NotifyPropertyChanged("OptionsContextMenuIntegrateMenuItemsIsChecked");
			}
		}

		public bool OptionsContextMenuIntegrateSubMenuIsChecked
		{
			get
			{
				return this.theOptionsContextMenuIntegrateSubMenuIsChecked;
			}
			set
			{
				this.theOptionsContextMenuIntegrateSubMenuIsChecked = value;
				NotifyPropertyChanged("OptionsContextMenuIntegrateSubMenuIsChecked");
			}
		}

		public bool OptionsOpenWithCrowbarIsChecked
		{
			get
			{
				return this.theOptionsOpenWithCrowbarIsChecked;
			}
			set
			{
				this.theOptionsOpenWithCrowbarIsChecked = value;
				NotifyPropertyChanged("OptionsOpenWithCrowbarIsChecked");
			}
		}

		public bool OptionsViewMdlFileIsChecked
		{
			get
			{
				return this.theOptionsViewMdlFileIsChecked;
			}
			set
			{
				this.theOptionsViewMdlFileIsChecked = value;
				NotifyPropertyChanged("OptionsViewMdlFileIsChecked");
			}
		}

		public bool OptionsDecompileMdlFileIsChecked
		{
			get
			{
				return this.theOptionsDecompileMdlFileIsChecked;
			}
			set
			{
				this.theOptionsDecompileMdlFileIsChecked = value;
				NotifyPropertyChanged("OptionsDecompileMdlFileIsChecked");
			}
		}

		public bool OptionsDecompileFolderIsChecked
		{
			get
			{
				return this.theOptionsDecompileFolderIsChecked;
			}
			set
			{
				this.theOptionsDecompileFolderIsChecked = value;
				NotifyPropertyChanged("OptionsDecompileFolderIsChecked");
			}
		}

		public bool OptionsDecompileFolderAndSubfoldersIsChecked
		{
			get
			{
				return this.theOptionsDecompileFolderAndSubfoldersIsChecked;
			}
			set
			{
				this.theOptionsDecompileFolderAndSubfoldersIsChecked = value;
				NotifyPropertyChanged("OptionsDecompileFolderAndSubfoldersIsChecked");
			}
		}

		public bool OptionsCompileQcFileIsChecked
		{
			get
			{
				return this.theOptionsCompileQcFileIsChecked;
			}
			set
			{
				this.theOptionsCompileQcFileIsChecked = value;
				NotifyPropertyChanged("OptionsCompileQcFileIsChecked");
			}
		}

		public bool OptionsCompileFolderIsChecked
		{
			get
			{
				return this.theOptionsCompileFolderIsChecked;
			}
			set
			{
				this.theOptionsCompileFolderIsChecked = value;
				NotifyPropertyChanged("OptionsCompileFolderIsChecked");
			}
		}

		public bool OptionsCompileFolderAndSubfoldersIsChecked
		{
			get
			{
				return this.theOptionsCompileFolderAndSubfoldersIsChecked;
			}
			set
			{
				this.theOptionsCompileFolderAndSubfoldersIsChecked = value;
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
				return this.theUpdateDownloadPath;
			}
			set
			{
				this.theUpdateDownloadPath = value;
				NotifyPropertyChanged("UpdateDownloadPath");
			}
		}

		public bool UpdateUpdateToNewPathIsChecked
		{
			get
			{
				return this.theUpdateUpdateToNewPathIsChecked;
			}
			set
			{
				this.theUpdateUpdateToNewPathIsChecked = value;
				NotifyPropertyChanged("UpdateUpdateToNewPathIsChecked");
			}
		}

		public string UpdateUpdateDownloadPath
		{
			get
			{
				return this.theUpdateUpdateDownloadPath;
			}
			set
			{
				this.theUpdateUpdateDownloadPath = value;
				NotifyPropertyChanged("UpdateUpdateDownloadPath");
			}
		}

		public bool UpdateCopySettingsIsChecked
		{
			get
			{
				return this.theUpdateCopySettingsIsChecked;
			}
			set
			{
				this.theUpdateCopySettingsIsChecked = value;
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
			this.DownloadUseItemIdIsChecked = true;
			this.DownloadPrependItemTitleIsChecked = true;
			this.DownloadAppendItemUpdateDateTimeIsChecked = true;
			this.DownloadReplaceSpacesWithUnderscoresIsChecked = true;
			this.DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked = true;
		}

		public void SetDefaultUnpackOutputSubfolderName()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.UnpackOutputSubfolderName = "unpacked " + My.MyApplication.Application.Info.Version.ToString(2);
		}

		public void SetDefaultUnpackOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.UnpackFolderForEachPackageIsChecked = false;
			this.UnpackKeepFullPathIsChecked = false;
			this.UnpackLogFileIsChecked = false;
		}

		public void SetDefaultDecompileOutputSubfolderName()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.DecompileOutputSubfolderName = "decompiled " + My.MyApplication.Application.Info.Version.ToString(2);
		}

		public void SetDefaultDecompileReCreateFilesOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.DecompileQcFileIsChecked = true;
			this.DecompileGroupIntoQciFilesIsChecked = false;
			this.DecompileQcSkinFamilyOnSingleLineIsChecked = true;
			this.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked = true;
			this.DecompileQcIncludeDefineBoneLinesIsChecked = true;
			this.DecompileQcUseMixedCaseForKeywordsIsChecked = false;

			this.DecompileReferenceMeshSmdFileIsChecked = true;
			this.DecompileRemovePathFromSmdMaterialFileNamesIsChecked = true;
			this.DecompileUseNonValveUvConversionIsChecked = false;

			this.DecompileBoneAnimationSmdFilesIsChecked = true;
			this.DecompileBoneAnimationPlaceInSubfolderIsChecked = true;

			this.DecompileTextureBmpFilesIsChecked = true;
			this.DecompileLodMeshSmdFilesIsChecked = true;
			this.DecompilePhysicsMeshSmdFileIsChecked = true;
			this.DecompileVertexAnimationVtaFileIsChecked = true;
			this.DecompileProceduralBonesVrdFileIsChecked = true;
		}

		public void SetDefaultCompileOutputSubfolderName()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.CompileOutputSubfolderName = "compiled " + My.MyApplication.Application.Info.Version.ToString(2);
		}

		public void SetDefaultCompileOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.CompileGoldSourceLogFileIsChecked = false;
			this.CompileSourceLogFileIsChecked = false;

			this.CompileOptionNoP4IsChecked = true;
			this.CompileOptionVerboseIsChecked = true;

			this.CompileOptionDefineBonesIsChecked = false;
			this.CompileOptionDefineBonesCreateFileIsChecked = false;
			this.CompileOptionDefineBonesQciFileName = "DefineBones";
			this.CompileOptionDefineBonesOverwriteQciFileIsChecked = false;
			this.CompileOptionDefineBonesModifyQcFileIsChecked = false;

			this.CompileOptionsText = "";
		}

		public void SetDefaultPackOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.PackLogFileIsChecked = false;

			this.PackOptionMultiFileVpkIsChecked = false;

			this.PackGmaTitle = "";
			this.PackGmaItemTags = new BindingListEx<string>();

			this.PackOptionsText = "";
		}

		public void SetDefaultOptionsAutoOpenOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.OptionsAutoOpenVpkFileIsChecked = true;
			this.OptionsAutoOpenVpkFileOption = AppEnums.ActionType.Unpack;
			this.OptionsAutoOpenGmaFileIsChecked = true;
			this.OptionsAutoOpenGmaFileOption = AppEnums.ActionType.Unpack;
			this.OptionsAutoOpenFpxFileIsChecked = true;

			this.OptionsAutoOpenMdlFileIsChecked = true;
			this.OptionsAutoOpenMdlFileForPreviewIsChecked = false;
			this.OptionsAutoOpenMdlFileForDecompileIsChecked = true;
			this.OptionsAutoOpenMdlFileForViewIsChecked = false;
			this.OptionsAutoOpenMdlFileOption = AppEnums.ActionType.Decompile;

			this.OptionsAutoOpenQcFileIsChecked = true;

			this.OptionsAutoOpenFolderOption = AppEnums.ActionType.Decompile;
		}

		public void SetDefaultOptionsDragAndDropOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.

			this.OptionsDragAndDropVpkFileOption = AppEnums.ActionType.Unpack;
			this.OptionsDragAndDropGmaFileOption = AppEnums.ActionType.Unpack;

			this.OptionsDragAndDropMdlFileForPreviewIsChecked = false;
			this.OptionsDragAndDropMdlFileForDecompileIsChecked = true;
			this.OptionsDragAndDropMdlFileForViewIsChecked = false;
			this.OptionsDragAndDropMdlFileOption = AppEnums.ActionType.Decompile;

			this.OptionsDragAndDropFolderOption = AppEnums.ActionType.Decompile;
		}

		public void SetDefaultOptionsContextMenuOptions()
		{
			//NOTE: Call the properties so the NotifyPropertyChanged events are raised.
			this.OptionsContextMenuIntegrateMenuItemsIsChecked = true;
			this.OptionsContextMenuIntegrateSubMenuIsChecked = true;

			this.OptionsOpenWithCrowbarIsChecked = true;
			this.OptionsViewMdlFileIsChecked = true;

			this.OptionsDecompileMdlFileIsChecked = true;
			this.OptionsDecompileFolderIsChecked = true;
			this.OptionsDecompileFolderAndSubfoldersIsChecked = true;

			this.OptionsCompileQcFileIsChecked = true;
			this.OptionsCompileFolderIsChecked = true;
			this.OptionsCompileFolderAndSubfoldersIsChecked = true;
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

	}

}