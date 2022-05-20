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
					this.Free();
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
			this.theModelViewers = new List<Viewer>();

			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.SupportedMdlVersion));
			this.OverrideMdlVersionComboBox.DisplayMember = "Value";
			this.OverrideMdlVersionComboBox.ValueMember = "Key";
			this.OverrideMdlVersionComboBox.DataSource = anEnumList;
			this.OverrideMdlVersionComboBox.DataBindings.Add("SelectedValue", MainCROWBAR.TheApp.Settings, this.NameOfAppSettingOverrideMdlVersionName, false, DataSourceUpdateMode.OnPropertyChanged);

			this.UpdateDataBindings();

			this.UpdateWidgets(false);

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
		}

		private void Free()
		{
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;

			//RemoveHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
			if (this.MdlPathFileNameTextBox.DataBindings["Text"] != null)
			{
				this.MdlPathFileNameTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;
			}
			this.MdlPathFileNameTextBox.DataBindings.Clear();

			this.OverrideMdlVersionComboBox.DataBindings.Clear();

			this.FreeDataViewer();
			this.FreeModelViewerWithModel();
			if (this.theModelViewers != null)
			{
				foreach (Viewer aModelViewer in this.theModelViewers)
				{
					this.FreeModelViewer(aModelViewer);
				}
			}
		}

#endregion

#region Properties

		public AppEnums.ViewerType ViewerType
		{
			get
			{
				return this.theViewerType;
			}
			set
			{
				this.theViewerType = value;
			}
		}

#endregion

#region Methods

		public void RunDataViewer()
		{
			if (!this.AppSettingDataViewerIsRunning)
			{
				this.theDataViewer = new Viewer();
				this.theDataViewer.ProgressChanged += this.DataViewerBackgroundWorker_ProgressChanged;
				this.theDataViewer.RunWorkerCompleted += this.DataViewerBackgroundWorker_RunWorkerCompleted;
				this.AppSettingDataViewerIsRunning = true;
				AppEnums.SupportedMdlVersion mdlVersionOverride = 0;
				if (this.theViewerType == AppEnums.ViewerType.Preview)
				{
					mdlVersionOverride = MainCROWBAR.TheApp.Settings.PreviewOverrideMdlVersion;
				}
				else
				{
					mdlVersionOverride = MainCROWBAR.TheApp.Settings.ViewOverrideMdlVersion;
				}
				this.theDataViewer.Run(this.AppSettingMdlPathFileName, mdlVersionOverride);

				//TODO: If viewer is not running, give user indication of what prevents viewing.
			}
		}

#endregion

#region Widget Event Handlers

		private void UpdateUserControl_Load(object sender, EventArgs e)
		{
			//NOTE: This code prevents Visual Studio or Windows often inexplicably extending the right side of these widgets.
			Workarounds.WorkaroundForFrameworkAnchorRightSizingBug(this.MdlPathFileNameTextBox, this.BrowseForMdlFileButton);

			if (!this.DesignMode)
			{
				this.Init();
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
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(this.AppSettingMdlPathFileName);
			if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
			{
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
			openFileWdw.FileName = Path.GetFileName(this.AppSettingMdlPathFileName);
			openFileWdw.Filter = "Source Engine Model Files (*.mdl) | *.mdl";
			openFileWdw.AddExtension = true;
			openFileWdw.Multiselect = false;
			openFileWdw.ValidateNames = true;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				this.AppSettingMdlPathFileName = openFileWdw.FileName;
			}
		}

		private void GotoMdlFileButton_Click(System.Object sender, System.EventArgs e)
		{
			FileManager.OpenWindowsExplorer(this.AppSettingMdlPathFileName);
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
			this.RunViewer(false);
		}

		private void ViewAsReplacementButton_Click(System.Object sender, System.EventArgs e)
		{
			this.RunViewer(true);
		}

		private void UseInDecompileButton_Click(System.Object sender, System.EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName = this.AppSettingMdlPathFileName;
		}

		private void OpenViewerButton_Click(object sender, EventArgs e)
		{
			this.OpenViewer();
		}

		private void OpenMappingToolButton_Click(object sender, EventArgs e)
		{
			this.OpenMappingTool();
		}

		private void RunGameButton_Click(object sender, EventArgs e)
		{
			this.RunGame();
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (!this.AppSettingDataViewerIsRunning)
			{
				if (e.PropertyName == this.NameOfAppSettingMdlPathFileName || e.PropertyName == this.NameOfAppSettingOverrideMdlVersionName)
				{
					this.UpdateWidgets(this.AppSettingViewerIsRunning);
					this.RunDataViewer();
				}
			}
		}

		private void DataViewerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				this.InfoRichTextBox.Text = "";
				//Me.InfoRichTextBox.AppendText(line + vbCr)
				//Me.AppSettingDataViewerIsRunning = True
				this.MessageTextBox.Text = "";
				this.MessageTextBox.AppendText(line + "\r\n");
			}
			else if (e.ProgressPercentage == 1)
			{
				this.InfoRichTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				//Me.InfoRichTextBox.AppendText(line + vbCr)
				this.MessageTextBox.AppendText(line + "\r\n");
			}
		}

		private void DataViewerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			this.FreeDataViewer();
			this.AppSettingDataViewerIsRunning = false;
		}

		private void ViewerBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				this.MessageTextBox.Text = "";
				this.MessageTextBox.AppendText(line + "\r\n");

				Viewer modelViewer = (Viewer)sender;
				if (modelViewer == this.theModelViewerWithModel)
				{
					this.UpdateWidgets(true);
				}
			}
			else if (e.ProgressPercentage == 1)
			{
				this.MessageTextBox.AppendText(line + "\r\n");
			}
			else if (e.ProgressPercentage == 100)
			{
				this.MessageTextBox.AppendText(line + "\r\n");
			}
		}

		private void ViewerBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			Viewer modelViewer = (Viewer)sender;
			if (modelViewer == this.theModelViewerWithModel)
			{
				this.FreeModelViewerWithModel();
				this.UpdateWidgets(false);
			}
			else
			{
				this.FreeModelViewer(modelViewer);
			}
		}

		private void MappingToolBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				this.MessageTextBox.Text = "";
				this.MessageTextBox.AppendText(line + "\r");
				this.UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				this.MessageTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				this.MessageTextBox.AppendText(line + "\r");
			}
		}

		private void MappingToolBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			MappingTool mappingTool = (MappingTool)sender;
			mappingTool.ProgressChanged -= this.MappingToolBackgroundWorker_ProgressChanged;
			mappingTool.RunWorkerCompleted -= this.MappingToolBackgroundWorker_RunWorkerCompleted;

			this.UpdateWidgets(false);
		}

		private void GameAppBackgroundWorker_ProgressChanged(System.Object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			string line = (e.UserState == null ? null : Convert.ToString(e.UserState));

			if (e.ProgressPercentage == 0)
			{
				this.MessageTextBox.Text = "";
				this.MessageTextBox.AppendText(line + "\r");
				this.UpdateWidgets(true);
			}
			else if (e.ProgressPercentage == 1)
			{
				this.MessageTextBox.AppendText(line + "\r");
			}
			else if (e.ProgressPercentage == 100)
			{
				this.MessageTextBox.AppendText(line + "\r");
			}
		}

		private void GameAppBackgroundWorker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			GameApp gameApp = (GameApp)sender;
			gameApp.ProgressChanged -= this.GameAppBackgroundWorker_ProgressChanged;
			gameApp.RunWorkerCompleted -= this.GameAppBackgroundWorker_RunWorkerCompleted;

			this.UpdateWidgets(false);
		}

#endregion

#region Private Properties

		private string NameOfAppSettingMdlPathFileName
		{
			get
			{
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
				if (this.theViewerType == AppEnums.ViewerType.Preview)
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
			this.MdlPathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, this.NameOfAppSettingMdlPathFileName, false, DataSourceUpdateMode.OnValidation);
			//AddHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf Me.ParsePathFileName
			this.MdlPathFileNameTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;

			//NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
			this.GameSetupComboBox.DisplayMember = "GameName";
			this.GameSetupComboBox.ValueMember = "GameName";
			this.GameSetupComboBox.DataSource = MainCROWBAR.TheApp.Settings.GameSetups;
			this.GameSetupComboBox.DataBindings.Add("SelectedIndex", MainCROWBAR.TheApp.Settings, this.NameOfAppSettingGameSetupSelectedIndex, false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void UpdateWidgets(bool modelViewerIsRunning)
		{
			this.AppSettingViewerIsRunning = modelViewerIsRunning;

			if (string.IsNullOrEmpty(this.AppSettingMdlPathFileName) || !(Path.GetExtension(this.AppSettingMdlPathFileName).ToLower() == ".mdl") || !File.Exists(this.AppSettingMdlPathFileName))
			{
				this.ViewButton.Enabled = false;
				this.ViewAsReplacementButton.Enabled = false;
				this.UseInDecompileButton.Enabled = false;
			}
			else
			{
				this.ViewButton.Enabled = !modelViewerIsRunning;
				this.ViewAsReplacementButton.Enabled = !modelViewerIsRunning;
				this.UseInDecompileButton.Enabled = true;
			}
		}

		private void RunViewer(bool viewAsReplacement)
		{
			this.theModelViewerWithModel = new Viewer();
			this.theModelViewerWithModel.ProgressChanged += this.ViewerBackgroundWorker_ProgressChanged;
			this.theModelViewerWithModel.RunWorkerCompleted += this.ViewerBackgroundWorker_RunWorkerCompleted;
			this.theModelViewerWithModel.Run(this.AppSettingGameSetupSelectedIndex, this.AppSettingMdlPathFileName, viewAsReplacement, ViewAsReplacementSubfolderName);

			//TODO: If viewer is not running, give user indication of what prevents viewing.
		}

		private void OpenViewer()
		{
			Viewer aModelViewer = new Viewer();
			aModelViewer.ProgressChanged += this.ViewerBackgroundWorker_ProgressChanged;
			aModelViewer.RunWorkerCompleted += this.ViewerBackgroundWorker_RunWorkerCompleted;
			aModelViewer.Run(this.AppSettingGameSetupSelectedIndex);

			this.theModelViewers.Add(aModelViewer);

			//TODO: If viewer is not running, give user indication of what prevents viewing.
		}

		private void OpenMappingTool()
		{
			MappingTool mappingTool = null;
			mappingTool = new MappingTool();
			mappingTool.ProgressChanged += this.MappingToolBackgroundWorker_ProgressChanged;
			mappingTool.RunWorkerCompleted += this.MappingToolBackgroundWorker_RunWorkerCompleted;
			mappingTool.Run(this.AppSettingGameSetupSelectedIndex);

			//TODO: If viewer is not running, give user indication of what prevents viewing.
		}

		private void RunGame()
		{
			GameApp gameApp = null;
			gameApp = new GameApp();
			gameApp.ProgressChanged += this.GameAppBackgroundWorker_ProgressChanged;
			gameApp.RunWorkerCompleted += this.GameAppBackgroundWorker_RunWorkerCompleted;
			gameApp.Run(this.AppSettingGameSetupSelectedIndex);

			//TODO: If gameApp is not running, give user indication of what prevents viewing.
		}

		private void FreeDataViewer()
		{
			if (this.theDataViewer != null)
			{
				this.theDataViewer.ProgressChanged -= this.DataViewerBackgroundWorker_ProgressChanged;
				this.theDataViewer.RunWorkerCompleted -= this.DataViewerBackgroundWorker_RunWorkerCompleted;
				this.theDataViewer.Dispose();
				this.theDataViewer = null;
			}
		}

		private void FreeModelViewerWithModel()
		{
			if (this.theModelViewerWithModel != null)
			{
				this.theModelViewerWithModel.ProgressChanged -= this.ViewerBackgroundWorker_ProgressChanged;
				this.theModelViewerWithModel.RunWorkerCompleted -= this.ViewerBackgroundWorker_RunWorkerCompleted;
				this.theModelViewerWithModel.Dispose();
				this.theModelViewerWithModel = null;
			}
		}

		private void FreeModelViewer(Viewer aModelViewer)
		{
			if (aModelViewer != null)
			{
				aModelViewer.ProgressChanged -= this.ViewerBackgroundWorker_ProgressChanged;
				aModelViewer.RunWorkerCompleted -= this.ViewerBackgroundWorker_RunWorkerCompleted;
				aModelViewer.Dispose();
				aModelViewer = null;

				this.theModelViewers.Remove(aModelViewer);
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