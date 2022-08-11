using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Web.Script.Serialization;

namespace Crowbar
{
	public partial class PackUserControl
	{

#region Creation and Destruction

		public PackUserControl() : base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			theGmaGarrysModTagsUserControlIsBeingChangedByMe = false;
		}

#endregion

#region Init and Free

		private void Init()
		{
			InputComboBox.DisplayMember = "Value";
			InputComboBox.ValueMember = "Key";
			InputComboBox.DataSource = EnumHelper.ToList(typeof(AppEnums.PackInputOptions));
			InputComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "PackMode", false, DataSourceUpdateMode.OnPropertyChanged);

			InputPathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "PackInputPath", false, DataSourceUpdateMode.OnValidation);

			OutputPathTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "PackOutputPath", false, DataSourceUpdateMode.OnValidation);
			OutputParentPathTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "PackOutputParentPath", false, DataSourceUpdateMode.OnValidation);
			InitOutputPathComboBox();
			UpdateOutputPathWidgets();

			//NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
			GameSetupComboBox.DisplayMember = "GameName";
			GameSetupComboBox.ValueMember = "GameName";
			GameSetupComboBox.DataSource = MainCROWBAR.TheApp.Settings.GameSetups;
			GameSetupComboBox.DataBindings.Add("SelectedIndex", MainCROWBAR.TheApp.Settings, "PackGameSetupSelectedIndex", false, DataSourceUpdateMode.OnPropertyChanged);

			InitCrowbarOptions();
			InitPackerOptions();

			thePackedRelativePathFileNames = new BindingListEx<string>();
			PackedFilesComboBox.DataSource = thePackedRelativePathFileNames;

			UpdateWidgets(false);
			UpdatePackerOptions();

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Packer.ProgressChanged += PackerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Packer.RunWorkerCompleted += PackerBackgroundWorker_RunWorkerCompleted;

			InputPathFileNameTextBox.DataBindings["Text"].Parse += FileManager.ParsePath;
			OutputPathTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
		}

		private void InitOutputPathComboBox()
		{
			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.PackOutputPathOptions));

			OutputPathComboBox.DataBindings.Clear();
			try
			{
				OutputPathComboBox.DisplayMember = "Value";
				OutputPathComboBox.ValueMember = "Key";
				OutputPathComboBox.DataSource = anEnumList;
				OutputPathComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "PackOutputFolderOption", false, DataSourceUpdateMode.OnPropertyChanged);

				// Do not use this line because it will override the value automatically assigned by the data bindings above.
				//Me.OutputPathComboBox.SelectedIndex = 0
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void InitCrowbarOptions()
		{
			LogFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "PackLogFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void InitPackerOptions()
		{
			theSelectedPackerOptions = new List<string>();
			MultiFileVpkCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "PackOptionMultiFileVpkIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			GmaTitleTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "PackGmaTitle", false, DataSourceUpdateMode.OnValidation);
			//NOTE: There is no automatic data-binding with TagsWidget, so manually bind from object to widget here.
			theGmaGarrysModTagsUserControlIsBeingChangedByMe = true;
			GmaGarrysModTagsUserControl.ItemTags = MainCROWBAR.TheApp.Settings.PackGmaItemTags;
			theGmaGarrysModTagsUserControlIsBeingChangedByMe = false;
			GmaGarrysModTagsUserControl.TagsPropertyChanged += GmaGarrysModTagsUserControl_TagsPropertyChanged;
		}

		private void Free()
		{
			InputPathFileNameTextBox.DataBindings["Text"].Parse -= FileManager.ParsePath;
			OutputPathTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Packer.ProgressChanged -= PackerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Packer.RunWorkerCompleted -= PackerBackgroundWorker_RunWorkerCompleted;

			InputPathFileNameTextBox.DataBindings.Clear();

			OutputPathTextBox.DataBindings.Clear();
			OutputParentPathTextBox.DataBindings.Clear();

			GameSetupComboBox.DataSource = null;
			GameSetupComboBox.DataBindings.Clear();

			FreeCrowbarOptions();
			FreePackerOptions();

			InputComboBox.DataBindings.Clear();

			PackedFilesComboBox.DataBindings.Clear();
		}

		private void FreeCrowbarOptions()
		{
			LogFileCheckBox.DataBindings.Clear();
		}

		private void FreePackerOptions()
		{
			MultiFileVpkCheckBox.DataBindings.Clear();

			GmaTitleTextBox.DataBindings.Clear();
			if (GmaGarrysModTagsUserControl != null)
			{
				GmaGarrysModTagsUserControl.TagsPropertyChanged -= GmaGarrysModTagsUserControl_TagsPropertyChanged;
			}
		}

#endregion

#region Properties

#endregion

#region Widget Event Handlers

		private void PackUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(InputPathFileNameTextBox, BrowseForInputFolderOrFileNameButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(OutputPathTextBox, BrowseForOutputPathButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(OutputParentPathTextBox, BrowseForOutputPathButton);

			if (!DesignMode)
			{
				Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		private void BrowseForInputFolderOrFileNameButton_Click(System.Object sender, System.EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			openFileWdw.Title = "Open the folder you want to pack";
			if (Directory.Exists(MainCROWBAR.TheApp.Settings.PackInputPath))
			{
				openFileWdw.InitialDirectory = MainCROWBAR.TheApp.Settings.PackInputPath;
			}
			else
			{
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.PackInputPath);
				if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
				{
					openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
			openFileWdw.FileName = "[Folder Selection]";
			openFileWdw.AddExtension = false;
			openFileWdw.CheckFileExists = false;
			openFileWdw.Multiselect = false;
			openFileWdw.ValidateNames = false;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				MainCROWBAR.TheApp.Settings.PackInputPath = FileManager.GetPath(openFileWdw.FileName);
			}
		}

		private void GotoInputPathButton_Click(System.Object sender, System.EventArgs e)
		{
			FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.PackInputPath);
		}

		private void OutputPathTextBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] pathFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
			string pathFileName = pathFileNames[0];
			if (Directory.Exists(pathFileName))
			{
				MainCROWBAR.TheApp.Settings.PackOutputPath = pathFileName;
			}
		}

		private void OutputPathTextBox_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void OutputPathTextBox_Validated(object sender, EventArgs e)
		{
			UpdateOutputPathTextBox();
		}

		private void BrowseForOutputPathButton_Click(object sender, EventArgs e)
		{
			BrowseForOutputPath();
		}

		private void GotoOutputPathButton_Click(object sender, EventArgs e)
		{
			GotoFolder();
		}

		//NOTE: There is no automatic data-binding with TagsWidget, so manually bind from widget to object here.
		private void GmaGarrysModTagsUserControl_TagsPropertyChanged(object sender, EventArgs e)
		{
			if (!theGmaGarrysModTagsUserControlIsBeingChangedByMe)
			{
				MainCROWBAR.TheApp.Settings.PackGmaItemTags = GmaGarrysModTagsUserControl.ItemTags;
			}
		}

		private void DirectPackerOptionsTextBox_TextChanged(System.Object sender, System.EventArgs e)
		{
			SetPackerOptionsText();
		}

		private void PackButton_Click(System.Object sender, System.EventArgs e)
		{
			RunPacker();
		}

		private void SkipCurrentFolderButtonButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Packer.SkipCurrentFolder();
		}

		private void CancelPackButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Packer.CancelAsync();
		}

		private void UseAllInPublishButton_Click(System.Object sender, System.EventArgs e)
		{
			//NOTE: It might not be good idea to try to auto-publish more than one workshop item at a time.
		}

		private void UseInPublishButton_Click(object sender, EventArgs e)
		{
			//TODO: Use the output folder (including file name when needed) as the Publish tab's input file or folder.
			//Dim pathFileName As String
			//pathFileName = TheApp.Packer.GetOutputPathFileName(Me.thePackedRelativePathFileNames(Me.PackedFilesComboBox.SelectedIndex))
			//TheApp.Settings.Publish = pathFileName
		}

		private void GotoPackedFileButton_Click(System.Object sender, System.EventArgs e)
		{
			string pathFileName = MainCROWBAR.TheApp.Packer.GetOutputPathFileName(thePackedRelativePathFileNames[PackedFilesComboBox.SelectedIndex]);
			FileManager.OpenWindowsExplorer(pathFileName);
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "PackInputPath")
			{
				SetPackerOptionsText();
				UpdateOutputPathWidgets();
			}
			else if (e.PropertyName == "PackMode")
			{
				UpdateOutputPathWidgets();
			}
			else if (e.PropertyName == "PackOutputFolderOption")
			{
				UpdateOutputPathWidgets();
			}
			else if (e.PropertyName == "PackGameSetupSelectedIndex")
			{
				UpdatePackerOptions();
			}
			else if (e.PropertyName.StartsWith("Pack") && e.PropertyName.EndsWith("IsChecked"))
			{
				UpdateWidgets(MainCROWBAR.TheApp.Settings.PackerIsRunning);
			}
		}

		private void PackerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				LogRichTextBox.Text = "";
				LogRichTextBox.AppendText(line + "\r");
				UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				LogRichTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				LogRichTextBox.AppendText(line + "\r");
			}
		}

		private void PackerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			string statusText = null;

			if (e.Cancelled)
			{
				statusText = "Pack canceled";
			}
			else
			{
				PackerOutputInfo packResultInfo = (PackerOutputInfo)e.Result;
				if (packResultInfo.theStatus == AppEnums.StatusMessage.Error)
				{
					statusText = "Pack failed; check the log";
				}
				else
				{
					statusText = "Pack succeeded";
				}
				UpdatePackedRelativePathFileNames(packResultInfo.thePackedRelativePathFileNames);
			}

			UpdateWidgets(false);
		}

#endregion

#region Private Methods

		private void UpdateOutputPathWidgets()
		{
			OutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.WorkFolder);
			OutputParentPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.ParentFolder);
			BrowseForOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.ParentFolder) || (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.WorkFolder);
			//Me.GotoOutputPathButton.Visible = (TheApp.Settings.PackOutputFolderOption = PackOutputPathOptions.ParentFolder) OrElse (TheApp.Settings.PackOutputFolderOption = PackOutputPathOptions.WorkFolder)
			UpdateOutputPathWidgets(MainCROWBAR.TheApp.Settings.PackerIsRunning);

			if (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.ParentFolder)
			{
				if (MainCROWBAR.TheApp.Settings.PackMode == AppEnums.PackInputOptions.ParentFolder)
				{
					MainCROWBAR.TheApp.Settings.PackOutputParentPath = MainCROWBAR.TheApp.Settings.PackInputPath;
				}
				else if (MainCROWBAR.TheApp.Settings.PackMode == AppEnums.PackInputOptions.Folder)
				{
					string parentPath = FileManager.GetPath(MainCROWBAR.TheApp.Settings.PackInputPath);
					MainCROWBAR.TheApp.Settings.PackOutputParentPath = parentPath;
				}
			}
		}

		private void UpdateOutputPathWidgets(bool packerIsRunning)
		{
			BrowseForOutputPathButton.Enabled = (!packerIsRunning) && (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.WorkFolder);
			GotoOutputPathButton.Enabled = (!packerIsRunning);
		}

		private void UpdateOutputPathTextBox()
		{
			if (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.WorkFolder)
			{
				if (string.IsNullOrEmpty(OutputPathTextBox.Text))
				{
					try
					{
						MainCROWBAR.TheApp.Settings.PackOutputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
				}
			}
		}

		private void UpdateWidgets(bool packerIsRunning)
		{
			MainCROWBAR.TheApp.Settings.PackerIsRunning = packerIsRunning;

			InputComboBox.Enabled = !packerIsRunning;
			InputPathFileNameTextBox.Enabled = !packerIsRunning;
			BrowseForInputFolderOrFileNameButton.Enabled = !packerIsRunning;

			OutputPathComboBox.Enabled = !packerIsRunning;
			OutputPathTextBox.Enabled = !packerIsRunning;
			OutputParentPathTextBox.Enabled = !packerIsRunning;
			UpdateOutputPathWidgets(packerIsRunning);

			OptionsGroupBox.Enabled = !packerIsRunning;

			PackButton.Enabled = !packerIsRunning;
			SkipCurrentFolderButton.Enabled = packerIsRunning;
			CancelPackButton.Enabled = packerIsRunning;
			SkipCurrentFolderButton.Enabled = packerIsRunning;
			UseAllInPublishButton.Enabled = !packerIsRunning && thePackedRelativePathFileNames.Count > 0;

			PackedFilesComboBox.Enabled = !packerIsRunning && thePackedRelativePathFileNames.Count > 0;
			//TODO: Check for the various Pack extensions instead of just for "vpk".
			UseInPublishButton.Enabled = !packerIsRunning && thePackedRelativePathFileNames.Count > 0 && (Path.GetExtension(thePackedRelativePathFileNames[PackedFilesComboBox.SelectedIndex]) == ".vpk");
			GotoPackedFileButton.Enabled = !packerIsRunning && thePackedRelativePathFileNames.Count > 0;
		}

		private void UpdatePackedRelativePathFileNames(BindingListEx<string> iPackedRelativePathFileNames)
		{
			if (iPackedRelativePathFileNames != null)
			{
				thePackedRelativePathFileNames = iPackedRelativePathFileNames;
				//NOTE: Do not sort because the list is already sorted by file and then by folder.
				//Me.theCompiledRelativePathFileNames.Sort()
				//NOTE: Need to set to nothing first to force it to update.
				PackedFilesComboBox.DataSource = null;
				PackedFilesComboBox.DataSource = thePackedRelativePathFileNames;
			}
		}

		private void UpdatePackerOptions()
		{
			//TODO: Add 'Write multi-file VPK' option.
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex];
			if (Path.GetFileName(gameSetup.PackerPathFileName) == "gmad.exe")
			{
				MultiFileVpkCheckBox.Visible = false;
				EditPackerOptionsText("M", false);

				GmaPanel.Visible = true;
			}
			else
			{
				//Me.MultiFileVpkCheckBox.Visible = True
				//Me.EditPackerOptionsText("M", TheApp.Settings.PackOptionMultiFileVpkIsChecked)

				GmaPanel.Visible = false;
			}

			SetPackerOptionsText();
		}

		private void EditPackerOptionsText(string iCompilerOption, bool optionIsEnabled)
		{
			string compilerOption = "-" + iCompilerOption;


			if (optionIsEnabled)
			{
				if (!theSelectedPackerOptions.Contains(compilerOption))
				{
					theSelectedPackerOptions.Add(compilerOption);
					theSelectedPackerOptions.Sort();
				}
			}
			else
			{
				if (theSelectedPackerOptions.Contains(compilerOption))
				{
					theSelectedPackerOptions.Remove(compilerOption);
				}
			}
		}

		private void SetPackerOptionsText()
		{
			MainCROWBAR.TheApp.Settings.PackOptionsText = "";
			PackerOptionsTextBox.Text = "";
			if (MainCROWBAR.TheApp.Settings.PackMode == AppEnums.PackInputOptions.ParentFolder)
			{
				foreach (string aChildPath in Directory.GetDirectories(MainCROWBAR.TheApp.Settings.PackInputPath))
				{
					SetPackerOptionsTextPerFolder(aChildPath);
				}
			}
			else
			{
				SetPackerOptionsTextPerFolder(MainCROWBAR.TheApp.Settings.PackInputPath);
			}
		}

		private void SetPackerOptionsTextPerFolder(string inputPath)
		{
			int selectedIndex = MainCROWBAR.TheApp.Settings.PackGameSetupSelectedIndex;
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[selectedIndex];
			string gamePackerFileName = Path.GetFileName(gameSetup.PackerPathFileName);
			string inputFolder = Path.GetFileName(inputPath);

			string packOptionsText = "";
			//NOTE: Available in Framework 4.0:
			//TheApp.Settings.PackOptionsText = String.Join(" ", Me.packerOptions)
			//------
			foreach (string packerOption in theSelectedPackerOptions)
			{
				packOptionsText += " ";
				packOptionsText += packerOption;

				//TODO: Special case for multi-file VPK option. Need to use "response" file.
				//If packerOption = "M" AndAlso gamePackerFileName <> "gmad.exe" Then
				//	'a <vpkfile> @<filename>
				//	If TheApp.Settings.PackMode = PackInputOptions.ParentFolder Then
				//	ElseIf TheApp.Settings.PackMode = PackInputOptions.Folder Then
				//	End If
				//End If
			}
			if (!string.IsNullOrEmpty(DirectPackerOptionsTextBox.Text.Trim()))
			{
				packOptionsText += " ";
				packOptionsText += DirectPackerOptionsTextBox.Text;
			}

			MainCROWBAR.TheApp.Settings.PackOptionsText = packOptionsText;

			PackerOptionsTextBox.Text += "\"";
			PackerOptionsTextBox.Text += gameSetup.PackerPathFileName;
			PackerOptionsTextBox.Text += "\"";
			PackerOptionsTextBox.Text += " ";
			if (gamePackerFileName == "gmad.exe")
			{
				PackerOptionsTextBox.Text += "create -folder ";

				string pathFileName = Path.Combine(inputPath, "addon.json");
				GarrysModSteamAppInfo garrysModAppInfo = new GarrysModSteamAppInfo();
				string tempVar = MainCROWBAR.TheApp.Settings.PackGmaTitle;
				var tempVar2 = MainCROWBAR.TheApp.Settings.PackGmaItemTags;
				garrysModAppInfo.ReadDataFromAddonJsonFile(pathFileName, ref tempVar, ref tempVar2);
					MainCROWBAR.TheApp.Settings.PackGmaItemTags = tempVar2;
					MainCROWBAR.TheApp.Settings.PackGmaTitle = tempVar;
				theGmaGarrysModTagsUserControlIsBeingChangedByMe = true;
				GmaGarrysModTagsUserControl.ItemTags = MainCROWBAR.TheApp.Settings.PackGmaItemTags;
				theGmaGarrysModTagsUserControlIsBeingChangedByMe = false;
			}
			PackerOptionsTextBox.Text += "\"";
			PackerOptionsTextBox.Text += inputFolder;
			PackerOptionsTextBox.Text += "\"";
			PackerOptionsTextBox.Text += " ";
			if (!string.IsNullOrEmpty(MainCROWBAR.TheApp.Settings.PackOptionsText.Trim()))
			{
				PackerOptionsTextBox.Text += packOptionsText;
			}
			PackerOptionsTextBox.Text += "\r\n";
		}

		private void CreateVpkResponseFile()
		{
			StreamWriter vpkResponseFileStream = null;
			string listFileName = null;
			string listPathFileName = null;

			if (Directory.Exists(MainCROWBAR.TheApp.Settings.PackInputPath))
			{
				try
				{
					// Create a folder in the Windows Temp path, to prevent potential file collisions and to provide user a more obvious folder name.
					Guid guid = new Guid();
					guid = Guid.NewGuid();
					string tempCrowbarFolder = "Crowbar_" + guid.ToString();
					string tempPath = Path.GetTempPath();
					string tempCrowbarPath = Path.Combine(tempPath, tempCrowbarFolder);
					try
					{
						FileManager.CreatePath(tempCrowbarPath);
					}
					catch (Exception ex)
					{
						throw new System.Exception("Crowbar tried to create folder path \"" + tempCrowbarPath + "\", but Windows gave this message: " + ex.Message);
					}

					listFileName = "crowbar_vpk_file_list.txt";
					listPathFileName = Path.Combine(tempCrowbarPath, listFileName);

					vpkResponseFileStream = File.CreateText(listPathFileName);
					vpkResponseFileStream.AutoFlush = true;

					vpkResponseFileStream.WriteLine("// ");
					vpkResponseFileStream.Flush();
				}
				catch (Exception ex)
				{
					LogRichTextBox.AppendText("ERROR: Crowbar tried to write the VPK response file for multi-file VPK packing, but the system gave this message: " + ex.Message + "\r");
				}
				finally
				{
					if (vpkResponseFileStream != null)
					{
						vpkResponseFileStream.Flush();
						vpkResponseFileStream.Close();
						vpkResponseFileStream = null;
					}
				}
			}
		}

		private void BrowseForOutputPath()
		{
			if (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.WorkFolder)
			{
				//NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
				//      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
				OpenFileDialog outputPathWdw = new OpenFileDialog();

				outputPathWdw.Title = "Open the folder you want as Output Folder";
				outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.PackOutputPath);
				if (string.IsNullOrEmpty(outputPathWdw.InitialDirectory))
				{
					if (File.Exists(MainCROWBAR.TheApp.Settings.PackInputPath))
					{
						outputPathWdw.InitialDirectory = FileManager.GetPath(MainCROWBAR.TheApp.Settings.PackInputPath);
					}
					else if (Directory.Exists(MainCROWBAR.TheApp.Settings.PackInputPath))
					{
						outputPathWdw.InitialDirectory = MainCROWBAR.TheApp.Settings.PackInputPath;
					}
					else
					{
						outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					}
				}
				outputPathWdw.FileName = "[Folder Selection]";
				outputPathWdw.AddExtension = false;
				outputPathWdw.CheckFileExists = false;
				outputPathWdw.Multiselect = false;
				outputPathWdw.ValidateNames = false;

				if (outputPathWdw.ShowDialog() == DialogResult.OK)
				{
					// Allow dialog window to completely disappear.
					Application.DoEvents();

					MainCROWBAR.TheApp.Settings.PackOutputPath = FileManager.GetPath(outputPathWdw.FileName);
				}
			}
		}

		private void GotoFolder()
		{
			if (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.ParentFolder)
			{
				FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.PackOutputParentPath);
			}
			else if (MainCROWBAR.TheApp.Settings.PackOutputFolderOption == AppEnums.PackOutputPathOptions.WorkFolder)
			{
				FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.PackOutputPath);
			}
		}

		private void RunPacker()
		{
			MainCROWBAR.TheApp.Packer.Run();
		}

#endregion

#region Data

		private List<string> theSelectedPackerOptions;

		private BindingListEx<string> thePackedRelativePathFileNames;

		private bool theGmaGarrysModTagsUserControlIsBeingChangedByMe;

#endregion

	}

}