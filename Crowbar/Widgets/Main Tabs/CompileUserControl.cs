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
	public partial class CompileUserControl
	{

#region Creation and Destruction

		public CompileUserControl() : base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

#endregion

#region Init and Free

		private void Init()
		{
			this.QcPathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "CompileQcPathFileName", false, DataSourceUpdateMode.OnValidation);

			this.OutputPathTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "CompileOutputFullPath", false, DataSourceUpdateMode.OnValidation);
			this.OutputSubfolderTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "CompileOutputSubfolderName", false, DataSourceUpdateMode.OnValidation);
			this.InitOutputPathComboBox();
			this.UpdateOutputPathWidgets();

			//NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
			this.GameSetupComboBox.DisplayMember = "GameName";
			this.GameSetupComboBox.ValueMember = "GameName";
			this.GameSetupComboBox.DataSource = MainCROWBAR.TheApp.Settings.GameSetups;
			this.GameSetupComboBox.DataBindings.Add("SelectedIndex", MainCROWBAR.TheApp.Settings, "CompileGameSetupSelectedIndex", false, DataSourceUpdateMode.OnPropertyChanged);

			this.InitCrowbarOptions();
			this.InitCompilerOptions();

			this.theCompiledRelativePathFileNames = new BindingListEx<string>();
			this.CompiledFilesComboBox.DataSource = this.theCompiledRelativePathFileNames;

			this.UpdateCompileMode();
			this.UpdateWidgets(false);
			this.UpdateCompilerOptions();

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Compiler.ProgressChanged += this.CompilerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Compiler.RunWorkerCompleted += this.CompilerBackgroundWorker_RunWorkerCompleted;

			this.QcPathFileNameTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
			this.OutputPathTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
		}

		private void InitOutputPathComboBox()
		{
			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.CompileOutputPathOptions));

			this.OutputPathComboBox.DataBindings.Clear();
			try
			{
				this.OutputPathComboBox.DisplayMember = "Value";
				this.OutputPathComboBox.ValueMember = "Key";
				this.OutputPathComboBox.DataSource = anEnumList;
				this.OutputPathComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "CompileOutputFolderOption", false, DataSourceUpdateMode.OnPropertyChanged);

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
			this.GoldSourceEngineLogFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "CompileGoldSourceLogFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.SourceEngineLogFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "CompileSourceLogFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void InitCompilerOptions()
		{
			this.theSelectedCompilerOptions = new List<string>();

			// GoldSource

			// Source

			this.CompilerOptionDefineBonesCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "CompileOptionDefineBonesIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "CompileOptionDefineBonesCreateFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.CompilerOptionDefineBonesFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "CompileOptionDefineBonesQciFileName", false, DataSourceUpdateMode.OnValidation);
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "CompileOptionDefineBonesOverwriteQciFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "CompileOptionDefineBonesModifyQcFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.CompilerOptionNoP4CheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "CompileOptionNoP4IsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.CompilerOptionVerboseCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "CompileOptionVerboseIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.UpdateCompilerOptionDefineBonesWidgets();
		}

		private void Free()
		{
			this.QcPathFileNameTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			this.OutputPathTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Compiler.ProgressChanged -= this.CompilerBackgroundWorker_ProgressChanged;
			MainCROWBAR.TheApp.Compiler.RunWorkerCompleted -= this.CompilerBackgroundWorker_RunWorkerCompleted;

			this.QcPathFileNameTextBox.DataBindings.Clear();

			this.OutputPathTextBox.DataBindings.Clear();
			this.OutputSubfolderTextBox.DataBindings.Clear();

			this.GameSetupComboBox.DataSource = null;
			this.GameSetupComboBox.DataBindings.Clear();

			this.FreeCrowbarOptions();
			this.FreeCompilerOptions();

			this.CompileComboBox.DataBindings.Clear();

			this.CompiledFilesComboBox.DataBindings.Clear();
		}

		private void FreeCrowbarOptions()
		{
			this.GoldSourceEngineLogFileCheckBox.DataBindings.Clear();
			this.SourceEngineLogFileCheckBox.DataBindings.Clear();
		}

		private void FreeCompilerOptions()
		{
			// GoldSource

			// Source
			//RemoveHandler Me.CompilerOptionDefineBonesCheckBox.CheckedChanged, AddressOf Me.CompilerOptionDefineBonesCheckBox_CheckedChanged
			//RemoveHandler Me.CompilerOptionNoP4CheckBox.CheckedChanged, AddressOf Me.CompilerOptionNoP4CheckBox_CheckedChanged
			//RemoveHandler Me.CompilerOptionVerboseCheckBox.CheckedChanged, AddressOf Me.CompilerOptionVerboseCheckBox_CheckedChanged

			this.CompilerOptionDefineBonesCheckBox.DataBindings.Clear();
			this.CompilerOptionDefineBonesFileNameTextBox.DataBindings.Clear();
			this.CompilerOptionNoP4CheckBox.DataBindings.Clear();
			this.CompilerOptionVerboseCheckBox.DataBindings.Clear();
		}

#endregion

#region Properties

#endregion

#region Widget Event Handlers

		private void CompileUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.QcPathFileNameTextBox, this.BrowseForQcPathFolderOrFileNameButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.OutputPathTextBox, this.BrowseForOutputPathButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.OutputSubfolderTextBox, this.BrowseForOutputPathButton);
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.GameModelsOutputPathTextBox, this.BrowseForOutputPathButton);

			if (!this.DesignMode)
			{
				this.Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		//Private Sub QcPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	'Me.QcPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.QcPathFileNameTextBox.Text)
		//	Me.SetCompilerOptionsText()
		//End Sub

		private void BrowseForQcPathFolderOrFileNameButton_Click(System.Object sender, System.EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			openFileWdw.Title = "Open the file or folder you want to compile";
			if (File.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
			{
				openFileWdw.InitialDirectory = FileManager.GetPath(MainCROWBAR.TheApp.Settings.CompileQcPathFileName);
				//ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
				//	openFileWdw.InitialDirectory = TheApp.Settings.CompileQcPathFileName
			}
			else
			{
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.CompileQcPathFileName);
				if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
				{
					openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
			}
			openFileWdw.FileName = "[Folder Selection]";
			//openFileWdw.Filter = "Source Engine QC Files (*.qc)|*.qc|All Files (*.*)|*.*"
			openFileWdw.Filter = "Source Engine QC Files (*.qc)|*.qc";
			openFileWdw.AddExtension = true;
			openFileWdw.CheckFileExists = false;
			openFileWdw.Multiselect = false;
			//openFileWdw.Multiselect = True
			openFileWdw.ValidateNames = true;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				if (Path.GetFileName(openFileWdw.FileName) == "[Folder Selection].qc")
				{
					MainCROWBAR.TheApp.Settings.CompileQcPathFileName = FileManager.GetPath(openFileWdw.FileName);
				}
				else
				{
					MainCROWBAR.TheApp.Settings.CompileQcPathFileName = openFileWdw.FileName;
				}

				//Me.SetCompilerOptionsText()
			}
		}

		private void GotoQcButton_Click(System.Object sender, System.EventArgs e)
		{
			FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.CompileQcPathFileName);
		}

		//Private Sub OutputFolderCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles OutputFolderCheckBox.CheckedChanged
		//	Me.UpdateOutputFolderWidgets()
		//End Sub

		//Private Sub OutputSubfolderNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputSubfolderNameRadioButton.CheckedChanged
		//       If Me.OutputSubfolderNameRadioButton.Checked Then
		//           TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName
		//       Else
		//           TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.PathName
		//       End If

		//       Me.UpdateOutputFolderWidgets()
		//End Sub

		//Private Sub OutputFolderPathNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputFullPathRadioButton.CheckedChanged
		//	Me.UpdateOutputFolderWidgets()
		//End Sub

		//Private Sub UseDefaultOutputSubfolderNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseDefaultOutputSubfolderNameButton.Click
		//	TheApp.Settings.SetDefaultCompileOutputSubfolderName()
		//	'Me.OutputSubfolderNameTextBox.DataBindings("Text").ReadValue()
		//End Sub

		//Private Sub OutputPathNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputFullPathTextBox.Validated
		//	'Me.OutputFullPathTextBox.Text = FileManager.GetCleanPathFileName(Me.OutputFullPathTextBox.Text)
		//	Me.UpdateOutputFullPathTextBox()
		//End Sub

		//Private Sub BrowseForOutputPathNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForOutputPathNameButton.Click
		//	'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
		//	'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
		//       Dim outputPathWdw As New OpenFileDialog()

		//       outputPathWdw.Title = "Open the folder you want as Output Folder"
		//	If Directory.Exists(TheApp.Settings.CompileOutputFullPath) Then
		//		outputPathWdw.InitialDirectory = TheApp.Settings.CompileOutputFullPath
		//	ElseIf File.Exists(TheApp.Settings.CompileQcPathFileName) Then
		//		outputPathWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.CompileQcPathFileName)
		//	ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
		//		outputPathWdw.InitialDirectory = TheApp.Settings.CompileQcPathFileName
		//	Else
		//		outputPathWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		//	End If
		//       outputPathWdw.FileName = "[Folder Selection]"
		//       outputPathWdw.AddExtension = False
		//       outputPathWdw.CheckFileExists = False
		//       outputPathWdw.Multiselect = False
		//       outputPathWdw.ValidateNames = False

		//       If outputPathWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
		//           ' Allow dialog window to completely disappear.
		//           Application.DoEvents()

		//           TheApp.Settings.CompileOutputFullPath = FileManager.GetPath(outputPathWdw.FileName)
		//       End If
		//End Sub

		//Private Sub GotoOutputButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoOutputButton.Click
		//	FileManager.OpenWindowsExplorer(Me.OutputFullPathTextBox.Text)
		//End Sub

		private void OutputPathTextBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] pathFileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
			string pathFileName = pathFileNames[0];
			if (Directory.Exists(pathFileName))
			{
				MainCROWBAR.TheApp.Settings.CompileOutputFullPath = pathFileName;
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
			MainCROWBAR.TheApp.Settings.SetDefaultCompileOutputSubfolderName();
		}

		//Private Sub GameSetupComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	'Me.SetCompilerOptionsText()
		//	Me.UpdateCompilerOptions(TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex), TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex))
		//End Sub

		//Private Sub SetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditGameSetupButton.Click
		//	Dim gameSetupWdw As New GameSetupForm()
		//	Dim gameSetupFormInfo As New GameSetupFormInfo()

		//	'gameSetupFormInfo.GameSetupIndex = Me.GameSetupComboBox.SelectedIndex
		//	gameSetupFormInfo.GameSetupIndex = TheApp.Settings.CompileGameSetupSelectedIndex
		//	gameSetupFormInfo.GameSetups = TheApp.Settings.GameSetups
		//	gameSetupWdw.DataSource = gameSetupFormInfo

		//	gameSetupWdw.ShowDialog()

		//	'Me.GameSetupComboBox.SelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
		//	'TheApp.Settings.SelectedGameSetup = TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex).GameName
		//	TheApp.Settings.CompileGameSetupSelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
		//End Sub

		//Private Sub CompilerOptionNoP4CheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompilerOptionNoP4CheckBox.CheckedChanged
		//	Me.EditCompilerOptionsText("nop4", Me.CompilerOptionNoP4CheckBox.Checked)
		//	Me.SetCompilerOptionsText()
		//End Sub

		//Private Sub CompilerOptionVerboseCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompilerOptionVerboseCheckBox.CheckedChanged
		//	Me.EditCompilerOptionsText("verbose", Me.CompilerOptionVerboseCheckBox.Checked)
		//	Me.SetCompilerOptionsText()
		//End Sub

		//Private Sub CompilerOptionDefineBonesCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompilerOptionDefineBonesCheckBox.CheckedChanged
		//	Me.EditCompilerOptionsText("definebones", Me.CompilerOptionDefineBonesCheckBox.Checked)
		//	Me.SetCompilerOptionsText()
		//	Me.UpdateCompilerOptionDefineBonesWidgets()
		//End Sub

		//Private Sub CompilerOptionDefineBonesCreateFileCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CompilerOptionDefineBonesCreateFileCheckBox.CheckedChanged
		//	Me.UpdateCompilerOptionDefineBonesWidgets()
		//End Sub

		private void CompileOptionsUseDefaultsButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultCompileOptions();
		}

		private void DirectCompilerOptionsTextBox_TextChanged(System.Object sender, System.EventArgs e)
		{
			this.SetCompilerOptionsText();
		}

		private void CompileButton_Click(System.Object sender, System.EventArgs e)
		{
			this.RunCompiler();
		}

		private void SkipCurrentModelButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Compiler.SkipCurrentModel();
		}

		private void CancelCompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Compiler.CancelAsync();
		}

		private void UseAllInPackButton_Click(System.Object sender, System.EventArgs e)
		{
			//TODO: Use the output folder (including file name when needed) as the pack tab's input file or folder.
		}

		private void UseInViewButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.ViewMdlPathFileName = MainCROWBAR.TheApp.Compiler.GetOutputPathFileName(this.theCompiledRelativePathFileNames[this.CompiledFilesComboBox.SelectedIndex]);
			MainCROWBAR.TheApp.Settings.ViewGameSetupSelectedIndex = MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex;
		}

		private void RecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			this.Recompile();
		}

		private void UseInPackButton_Click(System.Object sender, System.EventArgs e)
		{
			//TODO: Use the selected compiled file as Pack tab's input file.
		}

		private void GotoCompiledMdlButton_Click(System.Object sender, System.EventArgs e)
		{
			string pathFileName = MainCROWBAR.TheApp.Compiler.GetOutputPathFileName(this.theCompiledRelativePathFileNames[this.CompiledFilesComboBox.SelectedIndex]);
			FileManager.OpenWindowsExplorer(pathFileName);
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "CompileQcPathFileName")
			{
				this.UpdateCompileMode();
				this.SetCompilerOptionsText();
			}
			else if (e.PropertyName == "CompileOutputFolderOption")
			{
				this.UpdateOutputPathWidgets();
			}
			else if (e.PropertyName == "CompileGameSetupSelectedIndex")
			{
				this.UpdateGameModelsOutputPathTextBox();
				this.UpdateCompilerOptions();
				this.UpdateCompileButton();
			}
			else if (e.PropertyName == "CompileOptionDefineBonesIsChecked")
			{
				this.EditCompilerOptionsText("definebones", MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked);
				this.SetCompilerOptionsText();
				this.UpdateCompilerOptionDefineBonesWidgets();
			}
			else if (e.PropertyName == "CompileOptionDefineBonesCreateFileIsChecked")
			{
				this.UpdateCompilerOptionDefineBonesWidgets();
				//ElseIf e.PropertyName = "CompileOptionDefineBonesModifyQcFileIsChecked" Then
				//	Me.UpdateCompilerOptionDefineBonesWidgets()
			}
			else if (e.PropertyName == "CompileOptionNoP4IsChecked")
			{
				this.EditCompilerOptionsText("nop4", MainCROWBAR.TheApp.Settings.CompileOptionNoP4IsChecked);
				this.SetCompilerOptionsText();
			}
			else if (e.PropertyName == "CompileOptionVerboseIsChecked")
			{
				this.EditCompilerOptionsText("verbose", MainCROWBAR.TheApp.Settings.CompileOptionVerboseIsChecked);
				this.SetCompilerOptionsText();
			}
			else if (e.PropertyName.StartsWith("Compile") && e.PropertyName.EndsWith("IsChecked"))
			{
				this.UpdateWidgets(MainCROWBAR.TheApp.Settings.CompilerIsRunning);
			}
		}

		private void CompilerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			//If e.ProgressPercentage = 0 Then
			//	Me.CompileLogRichTextBox.Text = ""
			//	Me.UpdateWidgets(True)
			//ElseIf e.ProgressPercentage = 1 Then
			//	Me.CompileLogRichTextBox.AppendText(line + vbCr + vbCr)
			//ElseIf e.ProgressPercentage = 2 Then
			//	Me.CompileLogRichTextBox.AppendText(line + vbCr)
			//ElseIf e.ProgressPercentage = 3 Then
			//	Me.CompileLogRichTextBox.AppendText(vbCr + line + vbCr)
			//ElseIf e.ProgressPercentage = 4 Then
			//	Me.CompileLogRichTextBox.AppendText(vbCr + vbCr + vbCr + line + vbCr)
			//ElseIf e.ProgressPercentage = 5 Then
			//	Me.CompileLogRichTextBox.AppendText(vbCr + vbCr + vbCr)
			//End If
			if (e.ProgressPercentage == 0)
			{
				this.CompileLogRichTextBox.Text = "";
				this.CompileLogRichTextBox.AppendText(line + "\r");
				this.UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				this.CompileLogRichTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				this.CompileLogRichTextBox.AppendText(line + "\r");
			}
		}

		private void CompilerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			string statusText = null;

			if (e.Cancelled)
			{
				statusText = "Compile canceled";
			}
			else
			{
				CompilerOutputInfo compileResultInfo = (CompilerOutputInfo)e.Result;
				if (compileResultInfo.theStatus == AppEnums.StatusMessage.Error)
				{
					statusText = "Compile failed; check the log";
				}
				else
				{
					statusText = "Compile succeeded";
				}
				this.UpdateCompiledRelativePathFileNames(compileResultInfo.theCompiledRelativePathFileNames);
			}

			this.UpdateWidgets(false);
		}

#endregion

#region Private Methods

		//Private Sub UpdateOutputFolderWidgets()
		//	Me.CompileOutputFolderGroupBox.Enabled = Me.OutputFolderCheckBox.Checked
		//	Me.OutputSubfolderNameTextBox.ReadOnly = Not Me.OutputSubfolderNameRadioButton.Checked
		//	Me.OutputFullPathTextBox.ReadOnly = Me.OutputSubfolderNameRadioButton.Checked
		//	Me.BrowseForOutputPathNameButton.Enabled = Not Me.OutputSubfolderNameRadioButton.Checked
		//End Sub

		//Private Sub UpdateOutputFullPathTextBox()
		//	If String.IsNullOrEmpty(Me.OutputFullPathTextBox.Text) Then
		//		Try
		//			TheApp.Settings.CompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		//		Catch
		//		End Try
		//	End If
		//End Sub

		private void UpdateOutputPathWidgets()
		{
			this.GameModelsOutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder);
			this.OutputPathTextBox.Visible = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.WorkFolder);
			this.OutputSubfolderTextBox.Visible = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.Subfolder);
			this.BrowseForOutputPathButton.Enabled = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.WorkFolder);
			this.BrowseForOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder) || (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.WorkFolder);
			this.GotoOutputPathButton.Enabled = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder) || (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.WorkFolder);
			this.GotoOutputPathButton.Visible = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder) || (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.WorkFolder);
			this.UseDefaultOutputSubfolderButton.Enabled = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.Subfolder);
			this.UseDefaultOutputSubfolderButton.Visible = (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.Subfolder);

			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder)
			{
				this.UpdateGameModelsOutputPathTextBox();
			}
		}

		private void UpdateGameModelsOutputPathTextBox()
		{
			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder)
			{
				GameSetup gameSetup = null;
				string gamePath = null;
				string gameModelsPath = null;

				gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
				gamePath = FileManager.GetPath(gameSetup.GamePathFileName);
				gameModelsPath = Path.Combine(gamePath, "models");

				this.GameModelsOutputPathTextBox.Text = gameModelsPath;
			}
		}

		private void UpdateOutputPathTextBox()
		{
			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.WorkFolder)
			{
				if (string.IsNullOrEmpty(this.OutputPathTextBox.Text))
				{
					try
					{
						MainCROWBAR.TheApp.Settings.CompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.WorkFolder)
			{
				//NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
				//      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
				OpenFileDialog outputPathWdw = new OpenFileDialog();

				outputPathWdw.Title = "Open the folder you want as Output Folder";
				//If Directory.Exists(TheApp.Settings.CompileOutputFullPath) Then
				//	outputPathWdw.InitialDirectory = TheApp.Settings.CompileOutputFullPath
				//Else
				outputPathWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.CompileOutputFullPath);
				if (string.IsNullOrEmpty(outputPathWdw.InitialDirectory))
				{
					if (File.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
					{
						outputPathWdw.InitialDirectory = FileManager.GetPath(MainCROWBAR.TheApp.Settings.CompileQcPathFileName);
					}
					else if (Directory.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
					{
						outputPathWdw.InitialDirectory = MainCROWBAR.TheApp.Settings.CompileQcPathFileName;
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

					MainCROWBAR.TheApp.Settings.CompileOutputFullPath = FileManager.GetPath(outputPathWdw.FileName);
				}
			}
		}

		private void GotoFolder()
		{
			if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.GameModelsFolder)
			{
				GameSetup gameSetup = null;
				string gamePath = null;
				string gameModelsPath = null;

				gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
				gamePath = FileManager.GetPath(gameSetup.GamePathFileName);
				gameModelsPath = Path.Combine(gamePath, "models");

				if (FileManager.PathExistsAfterTryToCreate(gameModelsPath))
				{
					FileManager.OpenWindowsExplorer(gameModelsPath);
				}
			}
			else if (MainCROWBAR.TheApp.Settings.CompileOutputFolderOption == AppEnums.CompileOutputPathOptions.WorkFolder)
			{
				//FileManager.OpenWindowsExplorer(Me.OutputPathTextBox.Text)
				FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.CompileOutputFullPath);
			}
		}

		private void UpdateCompilerOptionDefineBonesWidgets()
		{
			this.CompilerOptionDefineBonesWriteQciFileCheckBox.Enabled = this.CompilerOptionDefineBonesCheckBox.Checked;
			this.CompilerOptionDefineBonesFileNameTextBox.Enabled = this.CompilerOptionDefineBonesWriteQciFileCheckBox.Enabled && this.CompilerOptionDefineBonesWriteQciFileCheckBox.Checked;
			this.CompilerOptionDefineBonesOverwriteQciFileCheckBox.Enabled = this.CompilerOptionDefineBonesWriteQciFileCheckBox.Enabled && this.CompilerOptionDefineBonesWriteQciFileCheckBox.Checked;
			this.CompilerOptionDefineBonesModifyQcFileCheckBox.Enabled = this.CompilerOptionDefineBonesWriteQciFileCheckBox.Enabled && this.CompilerOptionDefineBonesWriteQciFileCheckBox.Checked;

			this.UpdateCompileButton();
		}

		private void UpdateCompileButton()
		{
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];
			if (this.CompilerOptionDefineBonesCheckBox.Checked && gameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				this.CompileButton.Text = "&Compile DefineBones";
			}
			else
			{
				this.CompileButton.Text = "&Compile";
			}
		}

		private void UpdateWidgets(bool compilerIsRunning)
		{
			MainCROWBAR.TheApp.Settings.CompilerIsRunning = compilerIsRunning;

			this.CompileComboBox.Enabled = !compilerIsRunning;
			this.QcPathFileNameTextBox.Enabled = !compilerIsRunning;
			this.BrowseForQcPathFolderOrFileNameButton.Enabled = !compilerIsRunning;

			//Me.OutputSubfolderNameRadioButton.Enabled = Not compilerIsRunning
			//Me.OutputSubfolderNameTextBox.Enabled = Not compilerIsRunning
			//Me.UseDefaultOutputSubfolderNameButton.Enabled = Not compilerIsRunning
			//Me.OutputFullPathRadioButton.Enabled = Not compilerIsRunning
			//Me.OutputFullPathTextBox.Enabled = Not compilerIsRunning
			//Me.BrowseForOutputPathNameButton.Enabled = Not compilerIsRunning
			this.OutputPathComboBox.Enabled = !compilerIsRunning;
			this.OutputPathTextBox.Enabled = !compilerIsRunning;
			this.OutputSubfolderTextBox.Enabled = !compilerIsRunning;
			this.BrowseForOutputPathButton.Enabled = !compilerIsRunning;
			this.GotoOutputPathButton.Enabled = !compilerIsRunning;
			this.UseDefaultOutputSubfolderButton.Enabled = !compilerIsRunning;

			this.OptionsGroupBox.Enabled = !compilerIsRunning;

			this.CompileButton.Enabled = !compilerIsRunning;
			this.SkipCurrentModelButton.Enabled = compilerIsRunning;
			this.CancelCompileButton.Enabled = compilerIsRunning;
			this.UseAllInPackButton.Enabled = !compilerIsRunning && this.theCompiledRelativePathFileNames.Count > 0;

			this.CompiledFilesComboBox.Enabled = !compilerIsRunning && this.theCompiledRelativePathFileNames.Count > 0;
			this.UseInViewButton.Enabled = !compilerIsRunning && this.theCompiledRelativePathFileNames.Count > 0 && (Path.GetExtension(this.theCompiledRelativePathFileNames[this.CompiledFilesComboBox.SelectedIndex]) == ".mdl");
			this.RecompileButton.Enabled = !compilerIsRunning && this.theCompiledRelativePathFileNames.Count > 0;
			this.UseInPackButton.Enabled = !compilerIsRunning && this.theCompiledRelativePathFileNames.Count > 0;
			this.GotoCompiledMdlButton.Enabled = !compilerIsRunning && this.theCompiledRelativePathFileNames.Count > 0;
		}

		private void UpdateCompiledRelativePathFileNames(BindingListEx<string> iCompiledRelativePathFileNames)
		{
			//Me.theCompiledRelativePathFileNames.Clear()
			if (iCompiledRelativePathFileNames != null)
			{
				//For Each pathFileName As String In iCompiledRelativePathFileNames
				//	Me.theCompiledRelativePathFileNames.Add(pathFileName)
				//Next
				this.theCompiledRelativePathFileNames = iCompiledRelativePathFileNames;
				//NOTE: Do not sort because the list is already sorted by file and then by folder.
				//Me.theCompiledRelativePathFileNames.Sort()
				//NOTE: Need to set to nothing first to force it to update.
				this.CompiledFilesComboBox.DataSource = null;
				this.CompiledFilesComboBox.DataSource = this.theCompiledRelativePathFileNames;
			}
		}

		private void UpdateCompileMode()
		{
			IList anEnumList = null;
			AppEnums.InputOptions previousSelectedInputOption = 0;

			anEnumList = EnumHelper.ToList(typeof(AppEnums.InputOptions));
			previousSelectedInputOption = MainCROWBAR.TheApp.Settings.CompileMode;
			this.CompileComboBox.DataBindings.Clear();
			try
			{
				if (File.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
				{
					// Set file mode when a file is selected.
					previousSelectedInputOption = AppEnums.InputOptions.File;
				}
				else if (Directory.Exists(MainCROWBAR.TheApp.Settings.CompileQcPathFileName))
				{
					//NOTE: Remove in reverse index order.
					if (Directory.GetFiles(MainCROWBAR.TheApp.Settings.CompileQcPathFileName, "*.qc").Length == 0)
					{
						anEnumList.RemoveAt((System.Int32)AppEnums.InputOptions.Folder);
					}
					anEnumList.RemoveAt((System.Int32)AppEnums.InputOptions.File);
					//Else
					//	Exit Try
				}

				this.CompileComboBox.DisplayMember = "Value";
				this.CompileComboBox.ValueMember = "Key";
				this.CompileComboBox.DataSource = anEnumList;
				this.CompileComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, "CompileMode", false, DataSourceUpdateMode.OnPropertyChanged);

				if (EnumHelper.Contains(previousSelectedInputOption, anEnumList))
				{
					MainCROWBAR.TheApp.Settings.CompileMode = previousSelectedInputOption;
				}
				else
				{
					MainCROWBAR.TheApp.Settings.CompileMode = (AppEnums.InputOptions)EnumHelper.Key(0, anEnumList);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void RunCompiler()
		{
			MainCROWBAR.TheApp.Compiler.Run();
		}

		//Private Sub OpenDefineBonesFile(ByVal modelOutputPath As String)
		//    Try
		//        Dim fileName As String
		//        If String.IsNullOrEmpty(Path.GetExtension(TheApp.Settings.CompileOptionDefineBonesQciFileName)) Then
		//            fileName = TheApp.Settings.CompileOptionDefineBonesQciFileName + ".qci"
		//        Else
		//            fileName = TheApp.Settings.CompileOptionDefineBonesQciFileName
		//        End If
		//        Dim pathFileName As String
		//        pathFileName = Path.Combine(modelOutputPath, fileName)
		//        Me.theDefineBonesFileStream = File.CreateText(pathFileName)
		//    Catch ex As Exception
		//        Me.theDefineBonesFileStream = Nothing
		//    End Try
		//End Sub

		private void UpdateCompilerOptions()
		{
			GameSetup gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex];

			if (gameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				this.CompilerOptionsGoldSourceEnginePanel.Visible = true;
				this.CompilerOptionsSourceEnginePanel.Visible = false;

				//Me.EditCompilerOptionsText("definebones", TheApp.Settings.CompileOptionDefineBonesIsChecked)

				this.EditCompilerOptionsText("definebones", false);
				this.EditCompilerOptionsText("nop4", false);
				this.EditCompilerOptionsText("verbose", false);
			}
			else
			{
				this.CompilerOptionsGoldSourceEnginePanel.Visible = false;
				this.CompilerOptionsSourceEnginePanel.Visible = true;

				this.EditCompilerOptionsText("definebones", MainCROWBAR.TheApp.Settings.CompileOptionDefineBonesIsChecked);
				this.EditCompilerOptionsText("nop4", MainCROWBAR.TheApp.Settings.CompileOptionNoP4IsChecked);
				this.EditCompilerOptionsText("verbose", MainCROWBAR.TheApp.Settings.CompileOptionVerboseIsChecked);
			}

			this.SetCompilerOptionsText();
		}

		private void EditCompilerOptionsText(string iCompilerOption, bool optionIsEnabled)
		{
			string compilerOption = "-" + iCompilerOption;


			if (optionIsEnabled)
			{
				if (!this.theSelectedCompilerOptions.Contains(compilerOption))
				{
					this.theSelectedCompilerOptions.Add(compilerOption);
					this.theSelectedCompilerOptions.Sort();
				}
			}
			else
			{
				if (this.theSelectedCompilerOptions.Contains(compilerOption))
				{
					this.theSelectedCompilerOptions.Remove(compilerOption);
				}
			}
		}

		//SET Left4Dead2PathRootFolder=C:\Program Files (x86)\Steam\SteamApps\common\left 4 dead 2\
		//SET StudiomdlPathName=%Left4Dead2PathRootFolder%bin\studiomdl.exe
		//SET Left4Dead2PathSubFolder=%Left4Dead2PathRootFolder%left4dead2
		//SET StudiomdlParams=-game "%Left4Dead2PathSubFolder%" -nop4 -verbose -nox360
		//SET FileName=%ModelName%_%TargetApp%
		//"%StudiomdlPathName%" %StudiomdlParams% .\%FileName%.qc > .\%FileName%.log
		private void SetCompilerOptionsText()
		{
			string qcFileName = null;
			string gamePathFileName = null;
			int selectedIndex = 0;
			GameSetup gameSetup = null;

			//'NOTE: Must use Me.GameSetupComboBox.SelectedIndex because TheApp.Settings.SelectedGameSetupIndex might not be updated yet.
			//selectedIndex = Me.GameSetupComboBox.SelectedIndex
			selectedIndex = MainCROWBAR.TheApp.Settings.CompileGameSetupSelectedIndex;

			gameSetup = MainCROWBAR.TheApp.Settings.GameSetups[selectedIndex];

			qcFileName = Path.GetFileName(MainCROWBAR.TheApp.Settings.CompileQcPathFileName);
			gamePathFileName = gameSetup.GamePathFileName;

			MainCROWBAR.TheApp.Settings.CompileOptionsText = "";
			//NOTE: Available in Framework 4.0:
			//TheApp.Settings.CompilerOptionsText = String.Join(" ", Me.compilerOptions)
			//------
			foreach (string compilerOption in this.theSelectedCompilerOptions)
			{
				MainCROWBAR.TheApp.Settings.CompileOptionsText += " ";
				MainCROWBAR.TheApp.Settings.CompileOptionsText += compilerOption;
			}
			if (!string.IsNullOrEmpty(this.DirectCompilerOptionsTextBox.Text.Trim()))
			{
				MainCROWBAR.TheApp.Settings.CompileOptionsText += " ";
				MainCROWBAR.TheApp.Settings.CompileOptionsText += this.DirectCompilerOptionsTextBox.Text;
			}

			this.CompilerOptionsTextBox.Text = "\"";
			this.CompilerOptionsTextBox.Text += gameSetup.CompilerPathFileName;
			this.CompilerOptionsTextBox.Text += "\"";

			if (gameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				this.CompilerOptionsTextBox.Text += " ";
				this.CompilerOptionsTextBox.Text += "-game";
				this.CompilerOptionsTextBox.Text += " ";
				this.CompilerOptionsTextBox.Text += "\"";
				this.CompilerOptionsTextBox.Text += FileManager.GetPath(gamePathFileName);
				this.CompilerOptionsTextBox.Text += "\"";
			}

			if (!string.IsNullOrEmpty(MainCROWBAR.TheApp.Settings.CompileOptionsText.Trim()))
			{
				this.CompilerOptionsTextBox.Text += MainCROWBAR.TheApp.Settings.CompileOptionsText;
			}

			this.CompilerOptionsTextBox.Text += " ";
			this.CompilerOptionsTextBox.Text += "\"";
			this.CompilerOptionsTextBox.Text += qcFileName;
			this.CompilerOptionsTextBox.Text += "\"";
		}

		private void Recompile()
		{
			//TODO: Compile the selected QC.
			this.RunCompiler();
		}

#endregion

#region Data

		private List<string> theSelectedCompilerOptions;
		private string theModelRelativePathFileName;

		private BindingListEx<string> theCompiledRelativePathFileNames;

#endregion

	}

}