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
	public partial class ViewUserControl
	{

#region Creation and Destruction

		public ViewUserControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

		//UserControl overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					Free();
					if (components != null)
					{
						components.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

#endregion

#region Init and Free

		private void Init()
		{
			theModelViewers = new List<Viewer>();

			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.SupportedMdlVersion));
			OverrideMdlVersionComboBox.DisplayMember = "Value";
			OverrideMdlVersionComboBox.ValueMember = "Key";
			OverrideMdlVersionComboBox.DataSource = anEnumList;
			OverrideMdlVersionComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, NameOfAppSettingOverrideMdlVersionName, false, DataSourceUpdateMode.OnPropertyChanged);

			UpdateDataBindings();

			UpdateWidgets(false);

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
		}

		private void Free()
		{
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;

			//RemoveHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
			if (MdlPathFileNameTextBox.DataBindings["Text"] != null)
			{
				MdlPathFileNameTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			}
			MdlPathFileNameTextBox.DataBindings.Clear();

			OverrideMdlVersionComboBox.DataBindings.Clear();

			FreeDataViewer();
			FreeModelViewerWithModel();
			if (theModelViewers != null)
			{
				foreach (Viewer aModelViewer in theModelViewers)
				{
					FreeModelViewer(aModelViewer);
				}
			}
		}

#endregion

#region Properties

		public AppEnums.ViewerType ViewerType
		{
			get
			{
				return theViewerType;
			}
			set
			{
				theViewerType = value;
			}
		}

#endregion

#region Methods

		public void RunDataViewer()
		{
			if (!AppSettingDataViewerIsRunning)
			{
				theDataViewer = new Viewer();
				theDataViewer.ProgressChanged += DataViewerBackgroundWorker_ProgressChanged;
				theDataViewer.RunWorkerCompleted += DataViewerBackgroundWorker_RunWorkerCompleted;
				AppSettingDataViewerIsRunning = true;
				AppEnums.SupportedMdlVersion mdlVersionOverride = 0;
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					mdlVersionOverride = MainCROWBAR.TheApp.Settings.PreviewOverrideMdlVersion;
				}
				else
				{
					mdlVersionOverride = MainCROWBAR.TheApp.Settings.ViewOverrideMdlVersion;
				}
				theDataViewer.Run(AppSettingMdlPathFileName, mdlVersionOverride);

				//TODO: If viewer is not running, give user indication of what prevents viewing.
			}
		}

#endregion

#region Widget Event Handlers

		private void UpdateUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(MdlPathFileNameTextBox, BrowseForMdlFileButton);

			if (!DesignMode)
			{
				Init();
			}
		}

#endregion

#region Child Widget Event Handlers

		//Private Sub ParsePathFileName(ByVal sender As Object, ByVal e As ConvertEventArgs)
		//	If e.DesiredType IsNot GetType(String) Then
		//		Exit Sub
		//	End If
		//	e.Value = FileManager.GetCleanPathFileName(CStr(e.Value))
		//End Sub

		//Private Sub MdlPathFileNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MdlPathFileNameTextBox.Validated
		//	Me.MdlPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.MdlPathFileNameTextBox.Text)
		//End Sub

		private void BrowseForMdlFileButton_Click(System.Object sender, System.EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			openFileWdw.Title = "Open the MDL file you want to view";
			//openFileWdw.InitialDirectory = FileManager.GetPath(Me.AppSettingMdlPathFileName)
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(AppSettingMdlPathFileName);
			if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
			{
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
			openFileWdw.FileName = Path.GetFileName(AppSettingMdlPathFileName);
			openFileWdw.Filter = "Source Engine Model Files (*.mdl) | *.mdl";
			openFileWdw.AddExtension = true;
			openFileWdw.Multiselect = false;
			openFileWdw.ValidateNames = true;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				AppSettingMdlPathFileName = openFileWdw.FileName;
			}
		}

		private void GotoMdlFileButton_Click(System.Object sender, System.EventArgs e)
		{
			FileManager.OpenWindowsExplorer(AppSettingMdlPathFileName);
		}

		//Private Sub FromDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	Me.AppSettingMdlPathFileName = TheApp.Settings.DecompileMdlPathFileName
		//End Sub

		//Private Sub FromCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		//	If TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName Then
		//		Me.AppSettingMdlPathFileName = TheApp.Settings.CompileOutputSubfolderName
		//	Else
		//		Me.AppSettingMdlPathFileName = TheApp.Settings.CompileOutputFullPathName
		//	End If
		//End Sub

		//Private Sub SetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditGameSetupButton.Click
		//	Dim gameSetupWdw As New GameSetupForm()
		//	Dim gameSetupFormInfo As New GameSetupFormInfo()

		//	gameSetupFormInfo.GameSetupIndex = Me.AppSettingGameSetupSelectedIndex
		//	gameSetupFormInfo.GameSetups = TheApp.Settings.GameSetups
		//	gameSetupWdw.DataSource = gameSetupFormInfo

		//	gameSetupWdw.ShowDialog()

		//	Me.AppSettingGameSetupSelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
		//End Sub

		private void ViewButton_Click(System.Object sender, System.EventArgs e)
		{
			RunViewer(false);
		}

		private void ViewAsReplacementButton_Click(System.Object sender, System.EventArgs e)
		{
			RunViewer(true);
		}

		private void UseInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = AppSettingMdlPathFileName;
		}

		private void OpenViewerButton_Click(object sender, EventArgs e)
		{
			OpenViewer();
		}

		private void OpenMappingToolButton_Click(object sender, EventArgs e)
		{
			OpenMappingTool();
		}

		private void RunGameButton_Click(object sender, EventArgs e)
		{
			RunGame();
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (!AppSettingDataViewerIsRunning)
			{
				if (e.PropertyName == NameOfAppSettingMdlPathFileName || e.PropertyName == NameOfAppSettingOverrideMdlVersionName)
				{
					UpdateWidgets(AppSettingViewerIsRunning);
					RunDataViewer();
				}
			}
		}

		private void DataViewerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				InfoRichTextBox.Text = "";
				//Me.InfoRichTextBox.AppendText(line + vbCr)
				//Me.AppSettingDataViewerIsRunning = True
				MessageTextBox.Text = "";
				MessageTextBox.AppendText(line + "\r\n");
			}
			else if (e.ProgressPercentage == 1)
			{
				InfoRichTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				//Me.InfoRichTextBox.AppendText(line + vbCr)
				MessageTextBox.AppendText(line + "\r\n");
			}
		}

		private void DataViewerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			FreeDataViewer();
			AppSettingDataViewerIsRunning = false;
		}

		private void ViewerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				MessageTextBox.Text = "";
				MessageTextBox.AppendText(line + "\r\n");

				Viewer modelViewer = (Viewer)sender;
				if (modelViewer == theModelViewerWithModel)
				{
					UpdateWidgets(true);
				}
			}
			else if (e.ProgressPercentage == 1)
			{
				MessageTextBox.AppendText(line + "\r\n");
			}
			else if (e.ProgressPercentage == 100)
			{
				MessageTextBox.AppendText(line + "\r\n");
			}
		}

		private void ViewerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			Viewer modelViewer = (Viewer)sender;
			if (modelViewer == theModelViewerWithModel)
			{
				FreeModelViewerWithModel();
				UpdateWidgets(false);
			}
			else
			{
				FreeModelViewer(modelViewer);
			}
		}

		private void MappingToolBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				MessageTextBox.Text = "";
				MessageTextBox.AppendText(line + "\r");
				UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				MessageTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				MessageTextBox.AppendText(line + "\r");
			}
		}

		private void MappingToolBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			MappingTool mappingTool = (MappingTool)sender;
			mappingTool.ProgressChanged -= MappingToolBackgroundWorker_ProgressChanged;
			mappingTool.RunWorkerCompleted -= MappingToolBackgroundWorker_RunWorkerCompleted;

			UpdateWidgets(false);
		}

		private void GameAppBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				MessageTextBox.Text = "";
				MessageTextBox.AppendText(line + "\r");
				UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				MessageTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				MessageTextBox.AppendText(line + "\r");
			}
		}

		private void GameAppBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			GameApp gameApp = (GameApp)sender;
			gameApp.ProgressChanged -= GameAppBackgroundWorker_ProgressChanged;
			gameApp.RunWorkerCompleted -= GameAppBackgroundWorker_RunWorkerCompleted;

			UpdateWidgets(false);
		}

#endregion

#region Private Properties

		private string NameOfAppSettingMdlPathFileName
		{
			get
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					return "PreviewMdlPathFileName";
				}
				else
				{
					return "ViewMdlPathFileName";
				}
			}
		}

		private string NameOfAppSettingOverrideMdlVersionName
		{
			get
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					return "PreviewOverrideMdlVersion";
				}
				else
				{
					return "ViewOverrideMdlVersion";
				}
			}
		}

		private string NameOfAppSettingGameSetupSelectedIndex
		{
			get
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					return "PreviewGameSetupSelectedIndex";
				}
				else
				{
					return "ViewGameSetupSelectedIndex";
				}
			}
		}

		private int AppSettingGameSetupSelectedIndex
		{
			get
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					return MainCROWBAR.TheApp.Settings.PreviewGameSetupSelectedIndex;
				}
				else
				{
					return MainCROWBAR.TheApp.Settings.ViewGameSetupSelectedIndex;
				}
			}
			set
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					MainCROWBAR.TheApp.Settings.PreviewGameSetupSelectedIndex = value;
				}
				else
				{
					MainCROWBAR.TheApp.Settings.ViewGameSetupSelectedIndex = value;
				}
			}
		}

		private string AppSettingMdlPathFileName
		{
			get
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					return MainCROWBAR.TheApp.Settings.PreviewMdlPathFileName;
				}
				else
				{
					return MainCROWBAR.TheApp.Settings.ViewMdlPathFileName;
				}
			}
			set
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					MainCROWBAR.TheApp.Settings.PreviewMdlPathFileName = value;
				}
				else
				{
					MainCROWBAR.TheApp.Settings.ViewMdlPathFileName = value;
				}
			}
		}

		private bool AppSettingDataViewerIsRunning
		{
			get
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					return MainCROWBAR.TheApp.Settings.PreviewDataViewerIsRunning;
				}
				else
				{
					return MainCROWBAR.TheApp.Settings.ViewDataViewerIsRunning;
				}
			}
			set
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					MainCROWBAR.TheApp.Settings.PreviewDataViewerIsRunning = value;
				}
				else
				{
					MainCROWBAR.TheApp.Settings.ViewDataViewerIsRunning = value;
				}
			}
		}

		private bool AppSettingViewerIsRunning
		{
			get
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					return MainCROWBAR.TheApp.Settings.PreviewViewerIsRunning;
				}
				else
				{
					return MainCROWBAR.TheApp.Settings.ViewViewerIsRunning;
				}
			}
			set
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					MainCROWBAR.TheApp.Settings.PreviewViewerIsRunning = value;
				}
				else
				{
					MainCROWBAR.TheApp.Settings.ViewViewerIsRunning = value;
				}
			}
		}

		private string ViewAsReplacementSubfolderName
		{
			get
			{
				if (theViewerType == AppEnums.ViewerType.Preview)
				{
					return "-preview";
				}
				else
				{
					return "-view";
				}
			}
		}

#endregion

#region Private Methods

		private void UpdateDataBindings()
		{
			MdlPathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, NameOfAppSettingMdlPathFileName, false, DataSourceUpdateMode.OnValidation);
			//AddHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
			MdlPathFileNameTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;

			//NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
			GameSetupComboBox.DisplayMember = "GameName";
			GameSetupComboBox.ValueMember = "GameName";
			GameSetupComboBox.DataSource = MainCROWBAR.TheApp.Settings.GameSetups;
			GameSetupComboBox.DataBindings.Add("SelectedIndex", MainCROWBAR.TheApp.Settings, NameOfAppSettingGameSetupSelectedIndex, false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void UpdateWidgets(bool modelViewerIsRunning)
		{
			AppSettingViewerIsRunning = modelViewerIsRunning;

			if (string.IsNullOrEmpty(AppSettingMdlPathFileName) || !(Path.GetExtension(AppSettingMdlPathFileName).ToLower() == ".mdl") || !File.Exists(AppSettingMdlPathFileName))
			{
				ViewButton.Enabled = false;
				ViewAsReplacementButton.Enabled = false;
				UseInDecompileButton.Enabled = false;
			}
			else
			{
				ViewButton.Enabled = !modelViewerIsRunning;
				ViewAsReplacementButton.Enabled = !modelViewerIsRunning;
				UseInDecompileButton.Enabled = true;
			}
		}

		private void RunViewer(bool viewAsReplacement)
		{
			theModelViewerWithModel = new Viewer();
			theModelViewerWithModel.ProgressChanged += ViewerBackgroundWorker_ProgressChanged;
			theModelViewerWithModel.RunWorkerCompleted += ViewerBackgroundWorker_RunWorkerCompleted;
			theModelViewerWithModel.Run(AppSettingGameSetupSelectedIndex, AppSettingMdlPathFileName, viewAsReplacement, ViewAsReplacementSubfolderName);

			//TODO: If viewer is not running, give user indication of what prevents viewing.
		}

		private void OpenViewer()
		{
			Viewer aModelViewer = new Viewer();
			aModelViewer.ProgressChanged += ViewerBackgroundWorker_ProgressChanged;
			aModelViewer.RunWorkerCompleted += ViewerBackgroundWorker_RunWorkerCompleted;
			aModelViewer.Run(AppSettingGameSetupSelectedIndex);

			theModelViewers.Add(aModelViewer);

			//TODO: If viewer is not running, give user indication of what prevents viewing.
		}

		private void OpenMappingTool()
		{
			MappingTool mappingTool = null;
			mappingTool = new MappingTool();
			mappingTool.ProgressChanged += MappingToolBackgroundWorker_ProgressChanged;
			mappingTool.RunWorkerCompleted += MappingToolBackgroundWorker_RunWorkerCompleted;
			mappingTool.Run(AppSettingGameSetupSelectedIndex);

			//TODO: If viewer is not running, give user indication of what prevents viewing.
		}

		private void RunGame()
		{
			GameApp gameApp = null;
			gameApp = new GameApp();
			gameApp.ProgressChanged += GameAppBackgroundWorker_ProgressChanged;
			gameApp.RunWorkerCompleted += GameAppBackgroundWorker_RunWorkerCompleted;
			gameApp.Run(AppSettingGameSetupSelectedIndex);

			//TODO: If gameApp is not running, give user indication of what prevents viewing.
		}

		private void FreeDataViewer()
		{
			if (theDataViewer != null)
			{
				theDataViewer.ProgressChanged -= DataViewerBackgroundWorker_ProgressChanged;
				theDataViewer.RunWorkerCompleted -= DataViewerBackgroundWorker_RunWorkerCompleted;
				theDataViewer.Dispose();
				theDataViewer = null;
			}
		}

		private void FreeModelViewerWithModel()
		{
			if (theModelViewerWithModel != null)
			{
				theModelViewerWithModel.ProgressChanged -= ViewerBackgroundWorker_ProgressChanged;
				theModelViewerWithModel.RunWorkerCompleted -= ViewerBackgroundWorker_RunWorkerCompleted;
				theModelViewerWithModel.Dispose();
				theModelViewerWithModel = null;
			}
		}

		private void FreeModelViewer(Viewer aModelViewer)
		{
			if (aModelViewer != null)
			{
				aModelViewer.ProgressChanged -= ViewerBackgroundWorker_ProgressChanged;
				aModelViewer.RunWorkerCompleted -= ViewerBackgroundWorker_RunWorkerCompleted;
				aModelViewer.Dispose();
				aModelViewer = null;

				theModelViewers.Remove(aModelViewer);
			}
		}

#endregion

#region Data

		private AppEnums.ViewerType theViewerType;

		private Viewer theDataViewer;
		private Viewer theModelViewerWithModel;
		private List<Viewer> theModelViewers;

#endregion

	}

}