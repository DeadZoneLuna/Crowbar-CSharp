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
	public partial class PatchUserControl
	{

#region Creation and Destruction

		public PatchUserControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			//'NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
			//Try
			//	Me.Init()
			//Catch
			//End Try
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
			MdlPathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "PatchMdlPathFileName", false, DataSourceUpdateMode.OnValidation);

			UpdateDataBindings();
			UpdateWidgets(false);

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
			MdlPathFileNameTextBox.DataBindings["Text"].Parse += FileManager.ParsePathFileName;
		}

		private void Free()
		{
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
			MdlPathFileNameTextBox.DataBindings["Text"].Parse -= FileManager.ParsePathFileName;

			MdlPathFileNameTextBox.DataBindings.Clear();
		}

#endregion

#region Properties
#endregion

#region Methods

#endregion

#region Widget Event Handlers

#endregion

#region Child Widget Event Handlers

		private void BrowseForMdlFileButton_Click(System.Object sender, System.EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();

			openFileWdw.Title = "Open the MDL file you want to view";
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.PatchMdlPathFileName);
			if (string.IsNullOrEmpty(openFileWdw.InitialDirectory))
			{
				openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
			openFileWdw.FileName = Path.GetFileName(MainCROWBAR.TheApp.Settings.PatchMdlPathFileName);
			openFileWdw.Filter = "Source Engine Model Files (*.mdl) | *.mdl";
			openFileWdw.AddExtension = true;
			openFileWdw.Multiselect = false;
			openFileWdw.ValidateNames = true;

			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				MainCROWBAR.TheApp.Settings.PatchMdlPathFileName = openFileWdw.FileName;
			}
		}

		private void GotoMdlFileButton_Click(System.Object sender, System.EventArgs e)
		{
			FileManager.OpenWindowsExplorer(MainCROWBAR.TheApp.Settings.PatchMdlPathFileName);
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			//If e.PropertyName = Me.NameOfAppSettingMdlPathFileName Then
			//End If
		}

#endregion

#region Private Properties

#endregion

#region Private Methods

		private void UpdateDataBindings()
		{
		}

		private void UpdateWidgets(bool modelViewerIsRunning)
		{
		}

#endregion

#region Data

#endregion

	}

}