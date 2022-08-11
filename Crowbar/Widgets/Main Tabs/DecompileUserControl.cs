using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Text;

namespace Crowbar
{
	public partial class DecompileUserControl
	{

#region Creation and Destruction

		public DecompileUserControl() : base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

#endregion

#region Init and Free

		private void Init()
		{
			MdlPathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "DecompileMdlPathFileName", false, DataSourceUpdateMode.OnValidation);

			OutputPathTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "DecompileOutputFullPath", false, DataSourceUpdateMode.OnValidation);
			OutputSubfolderTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "DecompileOutputSubfolderName", false, DataSourceUpdateMode.OnValidation);
			InitOutputPathComboBox();
			UpdateOutputPathWidgets();

			InitDecompilerOptions();

			theDecompiledRelativePathFileNames = new BindingListEx<string>();
			DecompiledFilesComboBox.DataSource = theDecompiledRelativePathFileNames;

			UpdateDecompileMode();
			UpdateWidgets(false);

			MdlPathFileNameTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
			OutputPathTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Decompiler.ProgressChanged += DecompilerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Decompiler.RunWorkerCompleted += DecompilerBackgroundWorker_RunWorkerCompleted;
		}

		private void InitDecompilerOptions()
		{
			QcFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			GroupIntoQciFilesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileGroupIntoQciFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			SkinFamilyOnSingleLineCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcSkinFamilyOnSingleLineIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			IncludeDefineBoneLinesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcIncludeDefineBoneLinesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			UseMixedCaseForKeywordsCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcUseMixedCaseForKeywordsIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			ReferenceMeshSmdFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileReferenceMeshSmdFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			RemovePathFromMaterialFileNamesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileRemovePathFromSmdMaterialFileNamesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			UseNonValveUvConversionCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileUseNonValveUvConversionIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			BoneAnimationSmdFilesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileBoneAnimationSmdFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			PlaceInAnimsSubfolderCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileBoneAnimationPlaceInSubfolderIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			TextureBmpFilesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileTextureBmpFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			LodMeshSmdFilesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileLodMeshSmdFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			PhysicsMeshSmdFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompilePhysicsMeshSmdFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			VertexAnimationVtaFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileVertexAnimationVtaFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			ProceduralBonesVrdFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileProceduralBonesVrdFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			FolderForEachModelCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileFolderForEachModelIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			PrefixMeshFileNamesWithModelNameCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompilePrefixFileNamesWithModelNameIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			FormatForStricterImportersCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileStricterFormatIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			LogFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileLogFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			DebugInfoCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileDebugInfoFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			DeclareSequenceQciCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileDeclareSequenceQciFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.SupportedMdlVersion));
			OverrideMdlVersionComboBox.DisplayMember = "Value";
			OverrideMdlVersionComboBox.ValueMember = "Key";
			OverrideMdlVersionComboBox.DataSource = anEnumList;
			OverrideMdlVersionComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "DecompileOverrideMdlVersion", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void Free()
		{
			MdlPathFileNameTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			OutputPathTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Decompiler.ProgressChanged -= DecompilerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Decompiler.RunWorkerCompleted -= DecompilerBackgroundWorker_RunWorkerCompleted;

			DecompileComboBox.DataBindings.Clear();
			MdlPathFileNameTextBox.DataBindings.Clear();

			OutputPathTextBox.DataBindings.Clear();
			OutputSubfolderTextBox.DataBindings.Clear();

			FreeDecompilerOptions();

			DecompiledFilesComboBox.DataSource = null;
		}

		private void FreeDecompilerOptions()
		{
			QcFileCheckBox.DataBindings.Clear();
			GroupIntoQciFilesCheckBox.DataBindings.Clear();
			SkinFamilyOnSingleLineCheckBox.DataBindings.Clear();
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.DataBindings.Clear();
			IncludeDefineBoneLinesCheckBox.DataBindings.Clear();
			ReferenceMeshSmdFileCheckBox.DataBindings.Clear();
			RemovePathFromMaterialFileNamesCheckBox.DataBindings.Clear();
			UseNonValveUvConversionCheckBox.DataBindings.Clear();
			BoneAnimationSmdFilesCheckBox.DataBindings.Clear();
			PlaceInAnimsSubfolderCheckBox.DataBindings.Clear();

			TextureBmpFilesCheckBox.DataBindings.Clear();
			LodMeshSmdFilesCheckBox.DataBindings.Clear();
			PhysicsMeshSmdFileCheckBox.DataBindings.Clear();
			VertexAnimationVtaFileCheckBox.DataBindings.Clear();
			ProceduralBonesVrdFileCheckBox.DataBindings.Clear();

			FolderForEachModelCheckBox.DataBindings.Clear();
			PrefixMeshFileNamesWithModelNameCheckBox.DataBindings.Clear();
			FormatForStricterImportersCheckBox.DataBindings.Clear();

			LogFileCheckBox.DataBindings.Clear();
			DebugInfoCheckBox.DataBindings.Clear();

			DeclareSequenceQciCheckBox.DataBindings.Clear();

			OverrideMdlVersionComboBox.DataBindings.Clear();
		}

#endregion

#region Properties

#endregion

#region Widget Event Handlers

		private void DecompileUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(MdlPathFileNameTextBox, BrowseForMdlPathFolderOrFileNameButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(OutputPathTextBox, BrowseForOutputPathButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(OutputSubfolderTextBox, BrowseForOutputPathButton);

			if (!DesignMode)
			{
				Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		//Private Sub MdlPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MdlPathFileNameTextBox.Validated
		//	Me.MdlPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.MdlPathFileNameTextBox.Text)
		//End Sub

		private void BrowseForMdlPathFolderOrFileNameButton_Click(System.Object sender, System.EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			openFileWdw.Title = "Open the file or folder you want to decompile";
			if (File.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
			{
				openFileWdw.InitialDirectory = FileManager.GetPath(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName);
				//ElseIf Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
				//	openFileWdw.InitialDirectory = TheApp.Settings.DecompileMdlPathFileName
			}
			else
			{
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName);
				if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
				{
					openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
			openFileWdw.FileName = "[Folder Selection]";
			openFileWdw.Filter = "Source Engine MDL Files (*.mdl) | *.mdl";
			openFileWdw.AddExtension = true;
			openFileWdw.CheckFileExists = false;
			openFileWdw.Multiselect = false;
			openFileWdw.ValidateNames = true;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				try
				{
					if (Path.GetFileName(openFileWdw.FileName) == "[Folder Selection].mdl")
					{
						MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = FileManager.GetPath(openFileWdw.FileName);
					}
					else
					{
						MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = openFileWdw.FileName;
					}
				}
				catch (System.IO.PathTooLongException ex)
				{
					MessageBox.Show("The file or folder you tried to select has too many characters in it. Try shortening it by moving the model files somewhere else or by renaming folders or files." + "\r\n" + "\r\n" + "Error message generated by Windows: " + "\r\n" + ex.Message, "The File or Folder You Tried to Select Is Too Long", MessageBoxButtons.OK);
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void GotoMdlButton_Click(System.Object sender, System.EventArgs e)
		{
			FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName);
		}

		//Private Sub OutputSubfolderNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	If Me.OutputSubfolderNameRadioButton.Checked Then
		//		TheApp.Settings.DecompileOutputFolderOption = OutputFolderOptions.SubfolderName
		//	Else
		//		TheApp.Settings.DecompileOutputFolderOption = OutputFolderOptions.PathName
		//	End If

		//	Me.UpdateOutputFolderWidgets()
		//End Sub

		//Private Sub UseDefaultOutputSubfolderNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	TheApp.Settings.SetDefaultDecompileOutputSubfolderName()
		//	'Me.OutputSubfolderNameTextBox.DataBindings("Text").ReadValue()
		//End Sub

		//Private Sub OutputPathNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	Me.UpdateOutputFolderWidgets()
		//End Sub

		//Private Sub OutputPathNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	'Me.OutputFullPathTextBox.Text = FileManager.GetCleanPathFileName(Me.OutputFullPathTextBox.Text)
		//	Me.UpdateOutputFullPathTextBox()
		//End Sub

		//Private Sub BrowseForOutputPathNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
		//	'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
		//	Dim outputPathWdw As New OpenFileDialog()

		//	outputPathWdw.Title = "Open the folder you want as Output Folder"
		//	If Directory.Exists(TheApp.Settings.DecompileOutputFullPath) Then
		//		outputPathWdw.InitialDirectory = TheApp.Settings.DecompileOutputFullPath
		//	ElseIf File.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
		//		outputPathWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName)
		//	ElseIf Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
		//		outputPathWdw.InitialDirectory = TheApp.Settings.DecompileMdlPathFileName
		//	Else
		//		outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		//	End If
		//	outputPathWdw.FileName = "[Folder Selection]"
		//	outputPathWdw.AddExtension = False
		//	outputPathWdw.CheckFileExists = False
		//	outputPathWdw.Multiselect = False
		//	outputPathWdw.ValidateNames = False

		//	If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
		//		' Allow dialog window to completely disappear.
		//		Application.DoEvents()

		//		TheApp.Settings.DecompileOutputFullPath = FileManager.GetPath(outputPathWdw.FileName)
		//	End If
		//End Sub

		//Private Sub GotoOutputButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	FileManager.OpenWindowsExplorer(Me.OutputFullPathTextBox.Text)
		//End Sub

		private void OutputPathTextBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] pathFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
			string pathFileName = pathFileNames[0];
			if (Directory.Exists(pathFileName))
			{
				MainCROWBAR.TheApp.Settings.DecompileOutputFullPath = pathFileName;
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

		private void UseDefaultOutputSubfolderButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultDecompileOutputSubfolderName();
		}

		private void DecompileOptionsUseDefaultsButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultDecompileReCreateFilesOptions();
		}

		private void DecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			RunDecompiler();
		}

		private void SkipCurrentModelButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Decompiler.SkipCurrentModel();
		}

		private void CancelDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Decompiler.CancelAsync();
		}

		private void UseAllInCompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.CompileQcPathFileName = MainCROWBAR.TheApp.Decompiler.GetOutputPathFolderOrFileName();
			MainCROWBAR.TheApp.Settings.CompileMode = AppEnums.InputOptions.FolderRecursion;
		}

		private void UseInEditButton_Click(System.Object sender, System.EventArgs e)
		{
			//TODO: Use the selected decompiled file as Edit tab's input file.
		}

		private void UseInCompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.CompileQcPathFileName = MainCROWBAR.TheApp.Decompiler.GetOutputPathFileName(theDecompiledRelativePathFileNames[DecompiledFilesComboBox.SelectedIndex]);
			MainCROWBAR.TheApp.Settings.CompileMode = AppEnums.InputOptions.File;
		}

		private void GotoDecompiledFileButton_Click(System.Object sender, System.EventArgs e)
		{
			string pathFileName = MainCROWBAR.TheApp.Decompiler.GetOutputPathFileName(theDecompiledRelativePathFileNames[DecompiledFilesComboBox.SelectedIndex]);
			FileManager.OpenWindowsExplorer(pathFileName);
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "DecompileMdlPathFileName")
			{
				UpdateDecompileMode();
			}
			else if (e.PropertyName == "DecompileOutputFolderOption")
			{
				UpdateOutputPathWidgets();
			}
			else if (e.PropertyName.StartsWith("Decompile") && e.PropertyName.EndsWith("IsChecked"))
			{
				UpdateWidgets(MainCROWBAR.TheApp.Settings.DecompilerIsRunning);
			}
		}

		private void DecompilerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				DecompilerLogTextBox.Text = "";
				DecompilerLogTextBox.AppendText(line + "\r");
				UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				DecompilerLogTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				DecompilerLogTextBox.AppendText(line + "\r");
			}
		}

		private void DecompilerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				DecompilerOutputInfo decompileResultInfo = (DecompilerOutputInfo)e.Result;
				UpdateDecompiledRelativePathFileNames(decompileResultInfo.theDecompiledRelativePathFileNames);
			}

			UpdateWidgets(false);
		}

#endregion

#region Private Methods

		//Private Sub UpdateOutputFolderWidgets()
		//	Me.OutputSubfolderNameTextBox.ReadOnly = Not Me.OutputSubfolderNameRadioButton.Checked
		//	Me.OutputFullPathTextBox.ReadOnly = Me.OutputSubfolderNameRadioButton.Checked
		//	Me.BrowseForOutputPathNameButton.Enabled = Not Me.OutputSubfolderNameRadioButton.Checked
		//End Sub

		//Private Sub UpdateOutputFullPathTextBox()
		//	If String.IsNullOrEmpty(Me.OutputFullPathTextBox.Text) Then
		//		Try
		//			TheApp.Settings.DecompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		private void InitOutputPathComboBox()
		{
			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.DecompileOutputPathOptions));

			OutputPathComboBox.DataBindings.Clear();
			try
			{
				OutputPathComboBox.DisplayMember = "Value";
				OutputPathComboBox.ValueMember = "Key";
				OutputPathComboBox.DataSource = anEnumList;
				OutputPathComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "DecompileOutputFolderOption", false, DataSourceUpdateMode.OnPropertyChanged);

				// Do not use this line because it will override the value automatically assigned by the data bindings above.
				//Me.OutputPathComboBox.SelectedIndex = 0
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void UpdateOutputPathWidgets()
		{
			OutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			OutputSubfolderTextBox.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.Subfolder);
			BrowseForOutputPathButton.Enabled = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			BrowseForOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			GotoOutputPathButton.Enabled = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			GotoOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			UseDefaultOutputSubfolderButton.Enabled = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.Subfolder);
			UseDefaultOutputSubfolderButton.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.Subfolder);
		}

		private void UpdateOutputPathTextBox()
		{
			if (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder)
			{
				if (string.IsNullOrEmpty(OutputPathTextBox.Text))
				{
					try
					{
						MainCROWBAR.TheApp.Settings.DecompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
				}
			}
		}

		private void BrowseForOutputPath()
		{
			if (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder)
			{
				//NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
				//      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
				OpenFileDialog outputPathWdw = new OpenFileDialog();

				outputPathWdw.Title = "Open the folder you want as Output Folder";
				//If Directory.Exists(TheApp.Settings.DecompileOutputFullPath) Then
				//	outputPathWdw.InitialDirectory = TheApp.Settings.DecompileOutputFullPath
				//Else
				outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.DecompileOutputFullPath);
				if (string.IsNullOrEmpty(outputPathWdw.InitialDirectory))
				{
					if (File.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
					{
						outputPathWdw.InitialDirectory = FileManager.GetPath(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName);
					}
					else if (Directory.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
					{
						outputPathWdw.InitialDirectory = MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName;
					}
					else
					{
						outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					}
				}
				//End If
				outputPathWdw.FileName = "[Folder Selection]";
				outputPathWdw.AddExtension = false;
				outputPathWdw.CheckFileExists = false;
				outputPathWdw.Multiselect = false;
				outputPathWdw.ValidateNames = false;

				if (outputPathWdw.ShowDialog() == DialogResult.OK)
				{
					// Allow dialog window to completely disappear.
					Application.DoEvents();

					MainCROWBAR.TheApp.Settings.DecompileOutputFullPath = FileManager.GetPath(outputPathWdw.FileName);
				}
			}
		}

		private void GotoFolder()
		{
			if (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder)
			{
				//FileManager.OpenWindowsExplorer(Me.OutputPathTextBox.Text)
				FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.DecompileOutputFullPath);
			}
		}

		private void UpdateWidgets(bool decompilerIsRunning)
		{
			MainCROWBAR.TheApp.Settings.DecompilerIsRunning = decompilerIsRunning;

			DecompileComboBox.Enabled = !decompilerIsRunning;
			MdlPathFileNameTextBox.Enabled = !decompilerIsRunning;
			BrowseForMdlPathFolderOrFileNameButton.Enabled = !decompilerIsRunning;

			//Me.OutputSubfolderNameRadioButton.Enabled = Not decompilerIsRunning
			//Me.OutputSubfolderNameTextBox.Enabled = Not decompilerIsRunning
			//Me.UseDefaultOutputSubfolderNameButton.Enabled = Not decompilerIsRunning
			//Me.OutputFullPathRadioButton.Enabled = Not decompilerIsRunning
			//Me.OutputFullPathTextBox.Enabled = Not decompilerIsRunning
			//Me.BrowseForOutputPathNameButton.Enabled = Not decompilerIsRunning
			OutputPathComboBox.Enabled = !decompilerIsRunning;
			OutputPathTextBox.Enabled = !decompilerIsRunning;
			OutputSubfolderTextBox.Enabled = !decompilerIsRunning;
			BrowseForOutputPathButton.Enabled = !decompilerIsRunning;
			GotoOutputPathButton.Enabled = !decompilerIsRunning;
			UseDefaultOutputSubfolderButton.Enabled = !decompilerIsRunning;

			ReCreateFilesGroupBox.Enabled = !decompilerIsRunning;
			GroupIntoQciFilesCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;
			SkinFamilyOnSingleLineCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;
			OnlyChangedMaterialsInTextureGroupLinesCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;
			IncludeDefineBoneLinesCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;
			UseMixedCaseForKeywordsCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;

			RemovePathFromMaterialFileNamesCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked;
			UseNonValveUvConversionCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked;

			PlaceInAnimsSubfolderCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked;

			OptionsGroupBox.Enabled = !decompilerIsRunning;

			DecompileButton.Enabled = !decompilerIsRunning && (MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileLodMeshSmdFilesIsChecked || MainCROWBAR.TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked || MainCROWBAR.TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileTextureBmpFilesIsChecked || MainCROWBAR.TheApp.Settings.DecompileLogFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked);
			SkipCurrentModelButton.Enabled = decompilerIsRunning;
			CancelDecompileButton.Enabled = decompilerIsRunning;
			UseAllInCompileButton.Enabled = !decompilerIsRunning && theDecompiledRelativePathFileNames.Count > 0;

			DecompiledFilesComboBox.Enabled = !decompilerIsRunning && theDecompiledRelativePathFileNames.Count > 0;
			UseInEditButton.Enabled = !decompilerIsRunning && theDecompiledRelativePathFileNames.Count > 0;
			UseInCompileButton.Enabled = !decompilerIsRunning && theDecompiledRelativePathFileNames.Count > 0 && (Path.GetExtension(theDecompiledRelativePathFileNames[DecompiledFilesComboBox.SelectedIndex]) == ".qc");
			GotoDecompiledFileButton.Enabled = !decompilerIsRunning && theDecompiledRelativePathFileNames.Count > 0;
		}

		private void UpdateDecompiledRelativePathFileNames(BindingListEx<string> iDecompiledRelativePathFileNames)
		{
			//Me.theDecompiledRelativePathFileNames.Clear()
			//For Each pathFileName As String In iDecompiledRelativePathFileNames
			//	Me.theDecompiledRelativePathFileNames.Add(pathFileName)
			//Next
			if (iDecompiledRelativePathFileNames != null)
			{
				theDecompiledRelativePathFileNames = iDecompiledRelativePathFileNames;
				//NOTE: Do not sort because the list is already sorted by file and then by folder.
				//Me.theDecompiledRelativePathFileNames.Sort()
				//NOTE: Need to set to nothing first to force it to update.
				DecompiledFilesComboBox.DataSource = null;
				DecompiledFilesComboBox.DataSource = theDecompiledRelativePathFileNames;
			}
		}

		private void UpdateDecompileMode()
		{
			IList anEnumList = null;
			AppEnums.InputOptions previousSelectedInputOption = 0;

			anEnumList = EnumHelper.ToList(typeof(AppEnums.InputOptions));
			previousSelectedInputOption = MainCROWBAR.TheApp.Settings.DecompileMode;
			DecompileComboBox.DataBindings.Clear();
			try
			{
				if (File.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
				{
					// Set file mode when a file is selected.
					previousSelectedInputOption = AppEnums.InputOptions.File;
				}
				else if (Directory.Exists(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName))
				{
					//NOTE: Remove in reverse index order.
					if (Directory.GetFiles(MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName, "*.mdl").Length == 0)
					{
						anEnumList.RemoveAt((System.Int32)AppEnums.InputOptions.Folder);
					}
					anEnumList.RemoveAt((System.Int32)AppEnums.InputOptions.File);
					//Else
					//	Exit Try
				}

				DecompileComboBox.DisplayMember = "Value";
				DecompileComboBox.ValueMember = "Key";
				DecompileComboBox.DataSource = anEnumList;
				DecompileComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "DecompileMode", false, DataSourceUpdateMode.OnPropertyChanged);

				if (EnumHelper.Contains(previousSelectedInputOption, anEnumList))
				{
					MainCROWBAR.TheApp.Settings.DecompileMode = previousSelectedInputOption;
				}
				else
				{
					MainCROWBAR.TheApp.Settings.DecompileMode = (AppEnums.InputOptions)EnumHelper.Key(0, anEnumList);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void RunDecompiler()
		{
			MainCROWBAR.TheApp.Decompiler.Run();
		}

#endregion

#region Data

		private BindingListEx<string> theDecompiledRelativePathFileNames;

#endregion

	}

}