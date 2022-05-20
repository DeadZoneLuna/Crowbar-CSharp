//INSTANT C# NOTE: Formerly VB project-level imports:
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
			this.MdlPathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "DecompileMdlPathFileName", false, DataSourceUpdateMode.OnValidation);

			this.OutputPathTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "DecompileOutputFullPath", false, DataSourceUpdateMode.OnValidation);
			this.OutputSubfolderTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "DecompileOutputSubfolderName", false, DataSourceUpdateMode.OnValidation);
			this.InitOutputPathComboBox();
			this.UpdateOutputPathWidgets();

			this.InitDecompilerOptions();

			this.theDecompiledRelativePathFileNames = new BindingListEx<string>();
			this.DecompiledFilesComboBox.DataSource = this.theDecompiledRelativePathFileNames;

			this.UpdateDecompileMode();
			this.UpdateWidgets(false);

			this.MdlPathFileNameTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
			this.OutputPathTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Decompiler.ProgressChanged += this.DecompilerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Decompiler.RunWorkerCompleted += this.DecompilerBackgroundWorker_RunWorkerCompleted;
		}

		private void InitDecompilerOptions()
		{
			this.QcFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.GroupIntoQciFilesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileGroupIntoQciFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.SkinFamilyOnSingleLineCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcSkinFamilyOnSingleLineIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.IncludeDefineBoneLinesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcIncludeDefineBoneLinesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.UseMixedCaseForKeywordsCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileQcUseMixedCaseForKeywordsIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.ReferenceMeshSmdFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileReferenceMeshSmdFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.RemovePathFromMaterialFileNamesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileRemovePathFromSmdMaterialFileNamesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.UseNonValveUvConversionCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileUseNonValveUvConversionIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.BoneAnimationSmdFilesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileBoneAnimationSmdFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.PlaceInAnimsSubfolderCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileBoneAnimationPlaceInSubfolderIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.TextureBmpFilesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileTextureBmpFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.LodMeshSmdFilesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileLodMeshSmdFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.PhysicsMeshSmdFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompilePhysicsMeshSmdFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.VertexAnimationVtaFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileVertexAnimationVtaFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.ProceduralBonesVrdFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileProceduralBonesVrdFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.FolderForEachModelCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileFolderForEachModelIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.PrefixMeshFileNamesWithModelNameCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompilePrefixFileNamesWithModelNameIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.FormatForStricterImportersCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileStricterFormatIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.LogFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileLogFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.DebugInfoCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileDebugInfoFilesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.DeclareSequenceQciCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "DecompileDeclareSequenceQciFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.SupportedMdlVersion));
			this.OverrideMdlVersionComboBox.DisplayMember = "Value";
			this.OverrideMdlVersionComboBox.ValueMember = "Key";
			this.OverrideMdlVersionComboBox.DataSource = anEnumList;
			this.OverrideMdlVersionComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "DecompileOverrideMdlVersion", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void Free()
		{
			this.MdlPathFileNameTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			this.OutputPathTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Decompiler.ProgressChanged -= this.DecompilerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Decompiler.RunWorkerCompleted -= this.DecompilerBackgroundWorker_RunWorkerCompleted;

			this.DecompileComboBox.DataBindings.Clear();
			this.MdlPathFileNameTextBox.DataBindings.Clear();

			this.OutputPathTextBox.DataBindings.Clear();
			this.OutputSubfolderTextBox.DataBindings.Clear();

			this.FreeDecompilerOptions();

			this.DecompiledFilesComboBox.DataSource = null;
		}

		private void FreeDecompilerOptions()
		{
			this.QcFileCheckBox.DataBindings.Clear();
			this.GroupIntoQciFilesCheckBox.DataBindings.Clear();
			this.SkinFamilyOnSingleLineCheckBox.DataBindings.Clear();
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.DataBindings.Clear();
			this.IncludeDefineBoneLinesCheckBox.DataBindings.Clear();
			this.ReferenceMeshSmdFileCheckBox.DataBindings.Clear();
			this.RemovePathFromMaterialFileNamesCheckBox.DataBindings.Clear();
			this.UseNonValveUvConversionCheckBox.DataBindings.Clear();
			this.BoneAnimationSmdFilesCheckBox.DataBindings.Clear();
			this.PlaceInAnimsSubfolderCheckBox.DataBindings.Clear();

			this.TextureBmpFilesCheckBox.DataBindings.Clear();
			this.LodMeshSmdFilesCheckBox.DataBindings.Clear();
			this.PhysicsMeshSmdFileCheckBox.DataBindings.Clear();
			this.VertexAnimationVtaFileCheckBox.DataBindings.Clear();
			this.ProceduralBonesVrdFileCheckBox.DataBindings.Clear();

			this.FolderForEachModelCheckBox.DataBindings.Clear();
			this.PrefixMeshFileNamesWithModelNameCheckBox.DataBindings.Clear();
			this.FormatForStricterImportersCheckBox.DataBindings.Clear();

			this.LogFileCheckBox.DataBindings.Clear();
			this.DebugInfoCheckBox.DataBindings.Clear();

			this.DeclareSequenceQciCheckBox.DataBindings.Clear();

			this.OverrideMdlVersionComboBox.DataBindings.Clear();
		}

#endregion

#region Properties

#endregion

#region Widget Event Handlers

		private void DecompileUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.MdlPathFileNameTextBox, this.BrowseForMdlPathFolderOrFileNameButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.OutputPathTextBox, this.BrowseForOutputPathButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.OutputSubfolderTextBox, this.BrowseForOutputPathButton);

			if (!this.DesignMode)
			{
				this.Init();
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
			this.UpdateOutputPathTextBox();
		}

		private void BrowseForOutputPathButton_Click(object sender, EventArgs e)
		{
			this.BrowseForOutputPath();
		}

		private void GotoOutputPathButton_Click(object sender, EventArgs e)
		{
			this.GotoFolder();
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
			this.RunDecompiler();
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
			MainCROWBAR.TheApp.Settings.CompileQcPathFileName = MainCROWBAR.TheApp.Decompiler.GetOutputPathFileName(this.theDecompiledRelativePathFileNames[this.DecompiledFilesComboBox.SelectedIndex]);
			MainCROWBAR.TheApp.Settings.CompileMode = AppEnums.InputOptions.File;
		}

		private void GotoDecompiledFileButton_Click(System.Object sender, System.EventArgs e)
		{
			string pathFileName = MainCROWBAR.TheApp.Decompiler.GetOutputPathFileName(this.theDecompiledRelativePathFileNames[this.DecompiledFilesComboBox.SelectedIndex]);
			FileManager.OpenWindowsExplorer(pathFileName);
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "DecompileMdlPathFileName")
			{
				this.UpdateDecompileMode();
			}
			else if (e.PropertyName == "DecompileOutputFolderOption")
			{
				this.UpdateOutputPathWidgets();
			}
			else if (e.PropertyName.StartsWith("Decompile") && e.PropertyName.EndsWith("IsChecked"))
			{
				this.UpdateWidgets(MainCROWBAR.TheApp.Settings.DecompilerIsRunning);
			}
		}

		private void DecompilerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				this.DecompilerLogTextBox.Text = "";
				this.DecompilerLogTextBox.AppendText(line + "\r");
				this.UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				this.DecompilerLogTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				this.DecompilerLogTextBox.AppendText(line + "\r");
			}
		}

		private void DecompilerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				DecompilerOutputInfo decompileResultInfo = (DecompilerOutputInfo)e.Result;
				this.UpdateDecompiledRelativePathFileNames(decompileResultInfo.theDecompiledRelativePathFileNames);
			}

			this.UpdateWidgets(false);
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

			this.OutputPathComboBox.DataBindings.Clear();
			try
			{
				this.OutputPathComboBox.DisplayMember = "Value";
				this.OutputPathComboBox.ValueMember = "Key";
				this.OutputPathComboBox.DataSource = anEnumList;
				this.OutputPathComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "DecompileOutputFolderOption", false, DataSourceUpdateMode.OnPropertyChanged);

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
			this.OutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			this.OutputSubfolderTextBox.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.Subfolder);
			this.BrowseForOutputPathButton.Enabled = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			this.BrowseForOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			this.GotoOutputPathButton.Enabled = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			this.GotoOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder);
			this.UseDefaultOutputSubfolderButton.Enabled = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.Subfolder);
			this.UseDefaultOutputSubfolderButton.Visible = (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.Subfolder);
		}

		private void UpdateOutputPathTextBox()
		{
			if (MainCROWBAR.TheApp.Settings.DecompileOutputFolderOption == AppEnums.DecompileOutputPathOptions.WorkFolder)
			{
				if (string.IsNullOrEmpty(this.OutputPathTextBox.Text))
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

			this.DecompileComboBox.Enabled = !decompilerIsRunning;
			this.MdlPathFileNameTextBox.Enabled = !decompilerIsRunning;
			this.BrowseForMdlPathFolderOrFileNameButton.Enabled = !decompilerIsRunning;

			//Me.OutputSubfolderNameRadioButton.Enabled = Not decompilerIsRunning
			//Me.OutputSubfolderNameTextBox.Enabled = Not decompilerIsRunning
			//Me.UseDefaultOutputSubfolderNameButton.Enabled = Not decompilerIsRunning
			//Me.OutputFullPathRadioButton.Enabled = Not decompilerIsRunning
			//Me.OutputFullPathTextBox.Enabled = Not decompilerIsRunning
			//Me.BrowseForOutputPathNameButton.Enabled = Not decompilerIsRunning
			this.OutputPathComboBox.Enabled = !decompilerIsRunning;
			this.OutputPathTextBox.Enabled = !decompilerIsRunning;
			this.OutputSubfolderTextBox.Enabled = !decompilerIsRunning;
			this.BrowseForOutputPathButton.Enabled = !decompilerIsRunning;
			this.GotoOutputPathButton.Enabled = !decompilerIsRunning;
			this.UseDefaultOutputSubfolderButton.Enabled = !decompilerIsRunning;

			this.ReCreateFilesGroupBox.Enabled = !decompilerIsRunning;
			this.GroupIntoQciFilesCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;
			this.SkinFamilyOnSingleLineCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;
			this.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;
			this.IncludeDefineBoneLinesCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;
			this.UseMixedCaseForKeywordsCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked;

			this.RemovePathFromMaterialFileNamesCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked;
			this.UseNonValveUvConversionCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked;

			this.PlaceInAnimsSubfolderCheckBox.Enabled = MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked;

			this.OptionsGroupBox.Enabled = !decompilerIsRunning;

			this.DecompileButton.Enabled = !decompilerIsRunning && (MainCROWBAR.TheApp.Settings.DecompileQcFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileLodMeshSmdFilesIsChecked || MainCROWBAR.TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked || MainCROWBAR.TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileTextureBmpFilesIsChecked || MainCROWBAR.TheApp.Settings.DecompileLogFileIsChecked || MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked);
			this.SkipCurrentModelButton.Enabled = decompilerIsRunning;
			this.CancelDecompileButton.Enabled = decompilerIsRunning;
			this.UseAllInCompileButton.Enabled = !decompilerIsRunning && this.theDecompiledRelativePathFileNames.Count > 0;

			this.DecompiledFilesComboBox.Enabled = !decompilerIsRunning && this.theDecompiledRelativePathFileNames.Count > 0;
			this.UseInEditButton.Enabled = !decompilerIsRunning && this.theDecompiledRelativePathFileNames.Count > 0;
			this.UseInCompileButton.Enabled = !decompilerIsRunning && this.theDecompiledRelativePathFileNames.Count > 0 && (Path.GetExtension(this.theDecompiledRelativePathFileNames[this.DecompiledFilesComboBox.SelectedIndex]) == ".qc");
			this.GotoDecompiledFileButton.Enabled = !decompilerIsRunning && this.theDecompiledRelativePathFileNames.Count > 0;
		}

		private void UpdateDecompiledRelativePathFileNames(BindingListEx<string> iDecompiledRelativePathFileNames)
		{
			//Me.theDecompiledRelativePathFileNames.Clear()
			//For Each pathFileName As String In iDecompiledRelativePathFileNames
			//	Me.theDecompiledRelativePathFileNames.Add(pathFileName)
			//Next
			if (iDecompiledRelativePathFileNames != null)
			{
				this.theDecompiledRelativePathFileNames = iDecompiledRelativePathFileNames;
				//NOTE: Do not sort because the list is already sorted by file and then by folder.
				//Me.theDecompiledRelativePathFileNames.Sort()
				//NOTE: Need to set to nothing first to force it to update.
				this.DecompiledFilesComboBox.DataSource = null;
				this.DecompiledFilesComboBox.DataSource = this.theDecompiledRelativePathFileNames;
			}
		}

		private void UpdateDecompileMode()
		{
			IList anEnumList = null;
			AppEnums.InputOptions previousSelectedInputOption = 0;

			anEnumList = EnumHelper.ToList(typeof(AppEnums.InputOptions));
			previousSelectedInputOption = MainCROWBAR.TheApp.Settings.DecompileMode;
			this.DecompileComboBox.DataBindings.Clear();
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

				this.DecompileComboBox.DisplayMember = "Value";
				this.DecompileComboBox.ValueMember = "Key";
				this.DecompileComboBox.DataSource = anEnumList;
				this.DecompileComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "DecompileMode", false, DataSourceUpdateMode.OnPropertyChanged);

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