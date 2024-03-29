﻿//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;

namespace Crowbar
{
	public partial class SetUpGamesUserControl
	{

#region Creation and Destruction

		public SetUpGamesUserControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			//NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
			try
			{
				Init();
			}
			catch
			{
			}
		}

#endregion

#region Init and Free

		protected void Init()
		{
			GameSetupComboBox.DisplayMember = "GameName";
			GameSetupComboBox.ValueMember = "GameName";
			GameSetupComboBox.DataSource = MainCROWBAR.TheApp.Settings.GameSetups;
			GameSetupComboBox.DataBindings.Add("SelectedIndex", MainCROWBAR.TheApp.Settings, "SetUpGamesGameSetupSelectedIndex", false, DataSourceUpdateMode.OnPropertyChanged);

			DataGridViewTextBoxColumn textColumn = null;
			DataGridViewButtonColumn buttonColumn = null;
			SteamLibraryPathsDataGridView.AutoGenerateColumns = false;
			SteamLibraryPathsDataGridView.DataSource = MainCROWBAR.TheApp.Settings.SteamLibraryPaths;

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "Macro";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.DisplayIndex = 0;
			textColumn.HeaderText = "Macro";
			textColumn.Name = "Macro";
			textColumn.ReadOnly = true;
			textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			SteamLibraryPathsDataGridView.Columns.Add(textColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.DataPropertyName = "LibraryPath";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.DisplayIndex = 1;
			textColumn.FillWeight = 100;
			textColumn.HeaderText = "Library Path";
			textColumn.Name = "LibraryPath";
			textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			SteamLibraryPathsDataGridView.Columns.Add(textColumn);

			buttonColumn = new DataGridViewButtonColumn();
			buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			buttonColumn.DisplayIndex = 2;
			buttonColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			buttonColumn.HeaderText = "Browse";
			buttonColumn.Name = "Browse";
			buttonColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			buttonColumn.Text = "Browse...";
			buttonColumn.UseColumnTextForButtonValue = true;
			SteamLibraryPathsDataGridView.Columns.Add(buttonColumn);

			textColumn = new DataGridViewTextBoxColumn();
			textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			textColumn.DataPropertyName = "UseCount";
			textColumn.DefaultCellStyle.BackColor = SystemColors.Control;
			textColumn.DisplayIndex = 4;
			textColumn.HeaderText = "Use Count";
			textColumn.Name = "UseCount";
			textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			SteamLibraryPathsDataGridView.Columns.Add(textColumn);

			SteamAppPathFileNameTextBox.DataBindings.Add("Text", MainCROWBAR.TheApp.Settings, "SteamAppPathFileNameUnprocessed", false, DataSourceUpdateMode.OnValidation);

			UpdateWidgets();
			UpdateUseCounts();

			if (MainCROWBAR.TheApp.Settings.SteamLibraryPaths.Count == 0)
			{
				MainCROWBAR.TheApp.Settings.SteamLibraryPaths.AddNew();
			}

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Settings.GameSetups.ListChanged += GameSetups_ListChanged;
			SteamLibraryPathsDataGridView.SetMacroInSelectedGameSetupToolStripMenuItem.Click += SetMacroInSelectedGameSetupToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.SetMacroInAllGameSetupsToolStripMenuItem.Click += SetMacroInAllGameSetupsToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.ClearMacroInSelectedGameSetupToolStripMenuItem.Click += ClearMacroInSelectedGameSetupToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.ClearMacroInAllGameSetupsToolStripMenuItem.Click += ClearMacroInAllGameSetupsToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Click += ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Click += ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click;
		}

		protected void Free()
		{
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;
			MainCROWBAR.TheApp.Settings.GameSetups.ListChanged -= GameSetups_ListChanged;
			GamePathFileNameTextBox.DataBindings["Text"].Parse -= ParsePathFileName;
			SteamLibraryPathsDataGridView.SetMacroInSelectedGameSetupToolStripMenuItem.Click -= SetMacroInSelectedGameSetupToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.SetMacroInAllGameSetupsToolStripMenuItem.Click -= SetMacroInAllGameSetupsToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.ClearMacroInSelectedGameSetupToolStripMenuItem.Click -= ClearMacroInSelectedGameSetupToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.ClearMacroInAllGameSetupsToolStripMenuItem.Click -= ClearMacroInAllGameSetupsToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Click -= ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click;
			SteamLibraryPathsDataGridView.ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Click -= ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click;

			GameSetupComboBox.DataSource = null;
			GameSetupComboBox.DataBindings.Clear();
			SteamAppPathFileNameTextBox.DataBindings.Clear();
			SteamLibraryPathsDataGridView.DataSource = null;
		}

#endregion

#region Properties

#endregion

#region Widget Event Handlers

#endregion

#region Child Widget Event Handlers

		private void GameSetupComboBox_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			UpdateWidgets();
			UpdateWidgetsBasedOnGameEngine();
		}

		private void AddGameSetupButton_Click(System.Object sender, System.EventArgs e)
		{
			GameSetup gamesetup = new GameSetup();
			gamesetup.GameName = "<New Game>";
			MainCROWBAR.TheApp.Settings.GameSetups.Add(gamesetup);

			GameSetupComboBox.SelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.IndexOf(gamesetup);

			UpdateWidgets();
			UpdateUseCounts();
		}

		private void BrowseForGamePathFileNameButton_Click(System.Object sender, System.EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();
			if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				openFileWdw.Title = "Select GoldSource Engine LibList.gam File";
				openFileWdw.Filter = "GoldSource Engine LibList.gam File|liblist.gam|GAM Files (*.gam)|*.txt|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				openFileWdw.Title = "Select Source Engine GameInfo.txt File";
				openFileWdw.Filter = "Source Engine GameInfo.txt File|gameinfo.txt|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source2)
			{
				openFileWdw.Title = "Select Source 2 Engine GameInfo.gi File";
				openFileWdw.Filter = "Source 2 Engine GameInfo.gi File|gameinfo.gi|GI Files (*.gi)|*.txt|All Files (*.*)|*.*";
			}
			openFileWdw.AddExtension = true;
			openFileWdw.ValidateNames = true;
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(theSelectedGameSetup.GamePathFileName);
			openFileWdw.FileName = Path.GetFileName(theSelectedGameSetup.GamePathFileName);
			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				string tempVar = theSelectedGameSetup.GamePathFileNameUnprocessed;
				SetPathFileNameField(openFileWdw.FileName, ref tempVar);
					theSelectedGameSetup.GamePathFileNameUnprocessed = tempVar;
			}
		}

		private void BrowseForGameAppPathFileNameButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();
			if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				openFileWdw.Title = "Select GoldSource Engine Game's Executable File";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				openFileWdw.Title = "Select Source Engine Game's Executable File";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source2)
			{
				openFileWdw.Title = "Select Source 2 Engine Game's Executable File";
			}
			openFileWdw.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			openFileWdw.AddExtension = true;
			openFileWdw.ValidateNames = true;
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(theSelectedGameSetup.GameAppPathFileName);
			openFileWdw.FileName = Path.GetFileName(theSelectedGameSetup.GameAppPathFileName);
			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				string tempVar = theSelectedGameSetup.GameAppPathFileNameUnprocessed;
				SetPathFileNameField(openFileWdw.FileName, ref tempVar);
					theSelectedGameSetup.GameAppPathFileNameUnprocessed = tempVar;
			}
		}

		private void ClearGameAppOptionsButton_Click(object sender, EventArgs e)
		{
			GameAppOptionsTextBox.Text = "";
		}

		private void BrowseForCompilerPathFileNameButton_Click(System.Object sender, System.EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();
			if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				openFileWdw.Title = "Select GoldSource Engine Model Compiler Tool";
				openFileWdw.Filter = "GoldSource Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				openFileWdw.Title = "Select Source Engine Model Compiler Tool";
				openFileWdw.Filter = "Source Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source2)
			{
				openFileWdw.Title = "Select Source 2 Engine Model Compiler Tool";
				openFileWdw.Filter = "Source 2 Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			openFileWdw.AddExtension = true;
			openFileWdw.ValidateNames = true;
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(theSelectedGameSetup.CompilerPathFileName);
			openFileWdw.FileName = Path.GetFileName(theSelectedGameSetup.CompilerPathFileName);
			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				string tempVar = theSelectedGameSetup.CompilerPathFileNameUnprocessed;
				SetPathFileNameField(openFileWdw.FileName, ref tempVar);
					theSelectedGameSetup.CompilerPathFileNameUnprocessed = tempVar;
			}
		}

		private void BrowseForViewerPathFileNameButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();
			if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				openFileWdw.Title = "Select GoldSource Engine Model Viewer Tool";
				openFileWdw.Filter = "GoldSource Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				openFileWdw.Title = "Select Source Engine Model Viewer Tool";
				openFileWdw.Filter = "Source Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source2)
			{
				openFileWdw.Title = "Select Source 2 Engine Model Viewer Tool";
				openFileWdw.Filter = "Source 2 Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			openFileWdw.AddExtension = true;
			openFileWdw.ValidateNames = true;
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(theSelectedGameSetup.ViewerPathFileName);
			openFileWdw.FileName = Path.GetFileName(theSelectedGameSetup.ViewerPathFileName);
			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				string tempVar = theSelectedGameSetup.ViewerPathFileNameUnprocessed;
				SetPathFileNameField(openFileWdw.FileName, ref tempVar);
					theSelectedGameSetup.ViewerPathFileNameUnprocessed = tempVar;
			}
		}

		private void BrowseForMappingToolPathFileNameButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();
			if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				openFileWdw.Title = "Select GoldSource Engine Mapping Tool";
				openFileWdw.Filter = "GoldSource Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				openFileWdw.Title = "Select Source Engine Mapping Tool";
				openFileWdw.Filter = "Source Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source2)
			{
				openFileWdw.Title = "Select Source 2 Engine Mapping Tool";
				openFileWdw.Filter = "Source 2 Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			openFileWdw.AddExtension = true;
			openFileWdw.ValidateNames = true;
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(theSelectedGameSetup.MappingToolPathFileName);
			openFileWdw.FileName = Path.GetFileName(theSelectedGameSetup.MappingToolPathFileName);
			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				string tempVar = theSelectedGameSetup.MappingToolPathFileNameUnprocessed;
				SetPathFileNameField(openFileWdw.FileName, ref tempVar);
					theSelectedGameSetup.MappingToolPathFileNameUnprocessed = tempVar;
			}
		}

		private void BrowseForUnpackerPathFileNameButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();
			if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				openFileWdw.Title = "Select GoldSource Engine Packer/Unpacker Tool";
				openFileWdw.Filter = "GoldSource Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				openFileWdw.Title = "Select Source Engine Packer/Unpacker Tool";
				openFileWdw.Filter = "Source Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source2)
			{
				openFileWdw.Title = "Select Source 2 Engine Packer/Unpacker Tool";
				openFileWdw.Filter = "Source 2 Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
			}
			openFileWdw.AddExtension = true;
			openFileWdw.ValidateNames = true;
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(theSelectedGameSetup.PackerPathFileName);
			openFileWdw.FileName = Path.GetFileName(theSelectedGameSetup.PackerPathFileName);
			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				string tempVar = theSelectedGameSetup.PackerPathFileNameUnprocessed;
				SetPathFileNameField(openFileWdw.FileName, ref tempVar);
					theSelectedGameSetup.PackerPathFileNameUnprocessed = tempVar;
			}
		}

		private void CloneGameSetupButton_Click(object sender, EventArgs e)
		{
			GameSetup cloneGameSetup = (GameSetup)theSelectedGameSetup.Clone();
			cloneGameSetup.GameName = "Clone of " + theSelectedGameSetup.GameName;
			MainCROWBAR.TheApp.Settings.GameSetups.Add(cloneGameSetup);

			GameSetupComboBox.SelectedIndex = MainCROWBAR.TheApp.Settings.GameSetups.IndexOf(cloneGameSetup);

			UpdateWidgets();
			UpdateUseCounts();
		}

		private void DeleteGameSetupButton_Click(System.Object sender, System.EventArgs e)
		{
			//Dim selectedIndex As Integer

			//selectedIndex = Me.GameSetupComboBox.SelectedIndex
			//If selectedIndex >= 0 AndAlso TheApp.Settings.GameSetups.Count > 1 Then
			//	TheApp.Settings.GameSetups.RemoveAt(selectedIndex)
			//End If
			MainCROWBAR.TheApp.Settings.GameSetups.Remove(theSelectedGameSetup);

			UpdateWidgets();
			UpdateUseCounts();
		}

		private void CreateModelsFolderTreeButton_Click(object sender, EventArgs e)
		{
			//TODO: [CreateModelsFolderTreeButton_Click] Call a function in Unpacker to do the unpacking.
			string gamePath = FileManager.GetPath(theSelectedGameSetup.GamePathFileName);
			MainCROWBAR.TheApp.Unpacker.UnpackFolderTreeFromVPK(gamePath);
		}

		private void BrowseForSteamAppPathFileNameButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileWdw = new OpenFileDialog();
			openFileWdw.Title = "Select Steam Executable File";
			openFileWdw.AddExtension = true;
			openFileWdw.ValidateNames = true;
			openFileWdw.Filter = "Steam Executable File|steam.exe|All Files (*.*)|*.*";
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.SteamAppPathFileName);
			openFileWdw.FileName = Path.GetFileName(MainCROWBAR.TheApp.Settings.SteamAppPathFileName);
			if (openFileWdw.ShowDialog() == DialogResult.OK)
			{
				// Allow dialog window to completely disappear.
				Application.DoEvents();

				MainCROWBAR.TheApp.Settings.SteamAppPathFileNameUnprocessed = openFileWdw.FileName;
			}
		}

		private void SteamLibraryPathsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView senderGrid = (DataGridView)sender;

			if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
			{
				OpenFileDialog openFileWdw = new OpenFileDialog();
				openFileWdw.Title = "Select a Steam Library Folder";
				openFileWdw.CheckFileExists = false;
				openFileWdw.Multiselect = false;
				openFileWdw.ValidateNames = true;
				//openFileWdw.Filter = "Source Engine Packer/Unpacker Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
				openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(MainCROWBAR.TheApp.Settings.SteamLibraryPaths[e.RowIndex].LibraryPath);
				//If Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
				//	openFileWdw.InitialDirectory = TheApp.Settings.DecompileMdlPathFileName
				//Else
				//	openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				//End If
				openFileWdw.FileName = "[Folder Selection]";
				if (openFileWdw.ShowDialog() == DialogResult.OK)
				{
					// Allow dialog window to completely disappear.
					Application.DoEvents();

					try
					{
						MainCROWBAR.TheApp.Settings.SteamLibraryPaths[e.RowIndex].LibraryPath = FileManager.GetLongestExtantPath(openFileWdw.FileName);
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
		}

		private void SetMacroInSelectedGameSetupToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			SetMacroInSelectedGameSetup();
		}

		private void SetMacroInAllGameSetupsToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			SetMacroInAllGameSetups();
		}

		private void ClearMacroInSelectedGameSetupToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			ClearMacroInSelectedGameSetup();
		}

		private void ClearMacroInAllGameSetupsToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			ClearMacroInAllGameSetups();
		}

		private void ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			ChangeToThisMacroInSelectedGameSetup();
		}

		private void ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			ChangeToThisMacroInAllGameSetups();
		}

		private void AddLibraryPathButton_Click(object sender, EventArgs e)
		{
			SteamLibraryPath libraryPath = MainCROWBAR.TheApp.Settings.SteamLibraryPaths.AddNew();
			libraryPath.Macro = "<library" + (MainCROWBAR.TheApp.Settings.SteamLibraryPaths.IndexOf(libraryPath) + 1).ToString() + ">";
		}

		private void DeleteLibraryPathButton_Click(object sender, EventArgs e)
		{
			// Do not allow first item to be deleted.
			if (MainCROWBAR.TheApp.Settings.SteamLibraryPaths.Count > 1)
			{
				int itemIndex = 0;
				SteamLibraryPath aSteamLibraryPath = null;
				itemIndex = MainCROWBAR.TheApp.Settings.SteamLibraryPaths.Count - 1;
				aSteamLibraryPath = MainCROWBAR.TheApp.Settings.SteamLibraryPaths[itemIndex];
				if (aSteamLibraryPath.UseCount == 0)
				{
					MainCROWBAR.TheApp.Settings.SteamLibraryPaths.Remove(aSteamLibraryPath);
				}
			}
		}

		//Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
		//	TheApp.SaveAppSettings()
		//End Sub

		//Private Sub GoBackButton_Click(sender As Object, e As EventArgs) Handles GoBackButton.Click
		//	'TODO: Go back to the tab that opened the Set Up Games tab.
		//	Dim debug As Integer = 4242
		//End Sub

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SteamAppPathFileName")
			{
				UpdateUseCounts();
			}
		}

		protected void GameSetups_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
		{
			//If e.ListChangedType = ListChangedType.ItemAdded Then
			//ElseIf e.ListChangedType = ListChangedType.ItemDeleted AndAlso e.OldIndex = -2 Then
			//ElseIf e.ListChangedType = ListChangedType.ItemChanged Then
			if (e.ListChangedType == ListChangedType.ItemChanged)
			{
				if (e.PropertyDescriptor != null)
				{
					if (e.PropertyDescriptor.Name == "GamePathFileName" || e.PropertyDescriptor.Name == "GameAppPathFileName" || e.PropertyDescriptor.Name == "CompilerPathFileName" || e.PropertyDescriptor.Name == "ViewerPathFileName" || e.PropertyDescriptor.Name == "MappingToolPathFileName" || e.PropertyDescriptor.Name == "PackerPathFileName")
					{
						UpdateUseCounts();
					}
					else if (e.PropertyDescriptor.Name == "GameEngine")
					{
						UpdateWidgetsBasedOnGameEngine();
					}
				}
			}
		}

		private void ParsePathFileName(object sender, ConvertEventArgs e)
		{
			e.Value = ParsePathFileName((e.Value == null ? null : Convert.ToString(e.Value)));
		}

		private string ParsePathFileName(string iPathFileName)
		{
			string originalText = iPathFileName;
			iPathFileName = MainCROWBAR.TheApp.GetProcessedPathFileName(iPathFileName);
			if (!string.IsNullOrEmpty(iPathFileName))
			{
				iPathFileName = FileManager.GetCleanPathFileName(iPathFileName, true);
			}
			SetPathFileNameField(iPathFileName, ref originalText);
			return originalText;
		}

#endregion

#region Private Methods

		private void UpdateWidgets()
		{
			int gameSetupCount = MainCROWBAR.TheApp.Settings.GameSetups.Count;

			GameSetupComboBox.Enabled = (gameSetupCount > 0);

			GamePathFileNameTextBox.Enabled = (gameSetupCount > 0);
			BrowseForGamePathFileNameButton.Enabled = (gameSetupCount > 0);
			GameAppPathFileNameTextBox.Enabled = (gameSetupCount > 0);
			GameAppOptionsTextBox.Enabled = (gameSetupCount > 0);
			ClearGameAppOptionsButton.Enabled = (gameSetupCount > 0);
			BrowseForGameAppPathFileNameButton.Enabled = (gameSetupCount > 0);
			CompilerPathFileNameTextBox.Enabled = (gameSetupCount > 0);
			BrowseForCompilerPathFileNameButton.Enabled = (gameSetupCount > 0);

			ViewerPathFileNameTextBox.Enabled = (gameSetupCount > 0);
			BrowseForViewerPathFileNameButton.Enabled = (gameSetupCount > 0);

			MappingToolPathFileNameTextBox.Enabled = (gameSetupCount > 0);
			BrowseForMappingToolPathFileNameButton.Enabled = (gameSetupCount > 0);

			PackerPathFileNameTextBox.Enabled = (gameSetupCount > 0);
			BrowseForUnpackerPathFileNameButton.Enabled = (gameSetupCount > 0);

			CloneGameSetupButton.Enabled = (gameSetupCount > 0);
			DeleteGameSetupButton.Enabled = (gameSetupCount > 1);

			//NOTE: Reset the bindings, because a new game setup has been chosen.

			theSelectedGameSetup = MainCROWBAR.TheApp.Settings.GameSetups[GameSetupComboBox.SelectedIndex];

			GameNameTextBox.DataBindings.Clear();
			GameNameTextBox.DataBindings.Add("Text", theSelectedGameSetup, "GameName", false, DataSourceUpdateMode.OnValidation);

			UpdateGameEngineComboBox();

			GamePathFileNameTextBox.DataBindings.Clear();
			GamePathFileNameTextBox.DataBindings.Add("Text", theSelectedGameSetup, "GamePathFileNameUnprocessed", false, DataSourceUpdateMode.OnValidation);
			GamePathFileNameTextBox.DataBindings["Text"].Parse -= ParsePathFileName;
			GamePathFileNameTextBox.DataBindings["Text"].Parse += ParsePathFileName;
			//TEST: Was testing these lines for converting bad text found in Settings file. Problem is that Me.theSelectedGameSetup.GamePathFileNameUnprocessed
			//      always raises events when changed and ends up back here to do it again, thus leading to stack overflow.
			//Me.GamePathFileNameTextBox.Text = Me.ParsePathFileName(Me.theSelectedGameSetup.GamePathFileNameUnprocessed)
			//Me.GamePathFileNameTextBox.DataBindings("Text").WriteValue()

			GameAppPathFileNameTextBox.DataBindings.Clear();
			GameAppPathFileNameTextBox.DataBindings.Add("Text", theSelectedGameSetup, "GameAppPathFileNameUnprocessed", false, DataSourceUpdateMode.OnValidation);
			GameAppPathFileNameTextBox.DataBindings["Text"].Parse -= ParsePathFileName;
			GameAppPathFileNameTextBox.DataBindings["Text"].Parse += ParsePathFileName;
			GameAppOptionsTextBox.DataBindings.Clear();
			GameAppOptionsTextBox.DataBindings.Add("Text", theSelectedGameSetup, "GameAppOptions", false, DataSourceUpdateMode.OnValidation);

			CompilerPathFileNameTextBox.DataBindings.Clear();
			CompilerPathFileNameTextBox.DataBindings.Add("Text", theSelectedGameSetup, "CompilerPathFileNameUnprocessed", false, DataSourceUpdateMode.OnValidation);
			CompilerPathFileNameTextBox.DataBindings["Text"].Parse -= ParsePathFileName;
			CompilerPathFileNameTextBox.DataBindings["Text"].Parse += ParsePathFileName;

			ViewerPathFileNameTextBox.DataBindings.Clear();
			ViewerPathFileNameTextBox.DataBindings.Add("Text", theSelectedGameSetup, "ViewerPathFileNameUnprocessed", false, DataSourceUpdateMode.OnValidation);
			ViewerPathFileNameTextBox.DataBindings["Text"].Parse -= ParsePathFileName;
			ViewerPathFileNameTextBox.DataBindings["Text"].Parse += ParsePathFileName;

			MappingToolPathFileNameTextBox.DataBindings.Clear();
			MappingToolPathFileNameTextBox.DataBindings.Add("Text", theSelectedGameSetup, "MappingToolPathFileNameUnprocessed", false, DataSourceUpdateMode.OnValidation);
			MappingToolPathFileNameTextBox.DataBindings["Text"].Parse -= ParsePathFileName;
			MappingToolPathFileNameTextBox.DataBindings["Text"].Parse += ParsePathFileName;

			PackerPathFileNameTextBox.DataBindings.Clear();
			PackerPathFileNameTextBox.DataBindings.Add("Text", theSelectedGameSetup, "PackerPathFileNameUnprocessed", false, DataSourceUpdateMode.OnValidation);
			PackerPathFileNameTextBox.DataBindings["Text"].Parse -= ParsePathFileName;
			PackerPathFileNameTextBox.DataBindings["Text"].Parse += ParsePathFileName;
		}

		private void UpdateGameEngineComboBox()
		{
			IList anEnumList = EnumHelper.ToList(typeof(AppEnums.GameEngine));

			//NOTE: For now, remove the Source 2 value.
			EnumHelper.RemoveFromList(AppEnums.GameEngine.Source2, ref anEnumList);

			EngineComboBox.DataBindings.Clear();
			try
			{
				EngineComboBox.DisplayMember = "Value";
				EngineComboBox.ValueMember = "Key";
				EngineComboBox.DataSource = anEnumList;
				EngineComboBox.DataBindings.Add("SelectedValue", theSelectedGameSetup, "GameEngine", false, DataSourceUpdateMode.OnPropertyChanged);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void UpdateWidgetsBasedOnGameEngine()
		{
			if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.GoldSource)
			{
				GamePathLabel.Text = "LibList.gam:";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source)
			{
				GamePathLabel.Text = "GameInfo.txt:";
			}
			else if (theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source2)
			{
				GamePathLabel.Text = "GameInfo.gi:";
			}

			PackerLabel.Visible = theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source;
			PackerPathFileNameTextBox.Visible = theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source;
			BrowseForUnpackerPathFileNameButton.Visible = theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source;
			CreateModelsFolderTreeButton.Visible = theSelectedGameSetup.GameEngine == AppEnums.GameEngine.Source;
		}

		private void SetPathFileNameField(string inputText, ref string outputText)
		{
			if (outputText.Length > 0 && outputText[0] == '<')
			{
				foreach (SteamLibraryPath aSteamLibraryPath in MainCROWBAR.TheApp.Settings.SteamLibraryPaths)
				{
					SetMacroInText(aSteamLibraryPath.LibraryPath, aSteamLibraryPath.Macro, ref inputText);
				}
			}
			outputText = inputText;
		}

		private void UpdateUseCounts()
		{
			int useCount = 0;
			string aMacro = null;

			foreach (SteamLibraryPath aSteamLibraryPath in MainCROWBAR.TheApp.Settings.SteamLibraryPaths)
			{
				aMacro = aSteamLibraryPath.Macro;

				useCount = 0;
				foreach (GameSetup aGameSetup in MainCROWBAR.TheApp.Settings.GameSetups)
				{
					if (aGameSetup.GamePathFileNameUnprocessed.StartsWith(aMacro))
					{
						useCount += 1;
					}
					if (aGameSetup.GameAppPathFileNameUnprocessed.StartsWith(aMacro))
					{
						useCount += 1;
					}
					if (aGameSetup.CompilerPathFileNameUnprocessed.StartsWith(aMacro))
					{
						useCount += 1;
					}
					if (aGameSetup.ViewerPathFileNameUnprocessed.StartsWith(aMacro))
					{
						useCount += 1;
					}
					if (aGameSetup.MappingToolPathFileNameUnprocessed.StartsWith(aMacro))
					{
						useCount += 1;
					}
					if (aGameSetup.PackerPathFileNameUnprocessed.StartsWith(aMacro))
					{
						useCount += 1;
					}
				}
				if (MainCROWBAR.TheApp.Settings.SteamAppPathFileNameUnprocessed.StartsWith(aMacro))
				{
					useCount += 1;
				}
				aSteamLibraryPath.UseCount = useCount;
			}
		}

		private void SetMacroInSelectedGameSetup()
		{
			SteamLibraryPath aSteamLibraryPath = GetSelectedSteamLibraryPath();

			SetMacroInOneGameSetup(aSteamLibraryPath.LibraryPath, aSteamLibraryPath.Macro, theSelectedGameSetup);
		}

		private void SetMacroInAllGameSetups()
		{
			SteamLibraryPath aSteamLibraryPath = GetSelectedSteamLibraryPath();

			foreach (GameSetup aGameSetup in MainCROWBAR.TheApp.Settings.GameSetups)
			{
				SetMacroInOneGameSetup(aSteamLibraryPath.LibraryPath, aSteamLibraryPath.Macro, aGameSetup);
			}
		}

		private void ClearMacroInSelectedGameSetup()
		{
			SteamLibraryPath aSteamLibraryPath = GetSelectedSteamLibraryPath();

			SetMacroInOneGameSetup(aSteamLibraryPath.Macro, aSteamLibraryPath.LibraryPath, theSelectedGameSetup);
		}

		private void ClearMacroInAllGameSetups()
		{
			SteamLibraryPath aSteamLibraryPath = GetSelectedSteamLibraryPath();

			foreach (GameSetup aGameSetup in MainCROWBAR.TheApp.Settings.GameSetups)
			{
				SetMacroInOneGameSetup(aSteamLibraryPath.Macro, aSteamLibraryPath.LibraryPath, aGameSetup);
			}
		}

		private void ChangeToThisMacroInSelectedGameSetup()
		{
			SteamLibraryPath aSteamLibraryPath = GetSelectedSteamLibraryPath();

			SetMacroInOneGameSetup("<>", aSteamLibraryPath.Macro, theSelectedGameSetup);
		}

		private void ChangeToThisMacroInAllGameSetups()
		{
			SteamLibraryPath aSteamLibraryPath = GetSelectedSteamLibraryPath();

			foreach (GameSetup aGameSetup in MainCROWBAR.TheApp.Settings.GameSetups)
			{
				SetMacroInOneGameSetup("<>", aSteamLibraryPath.Macro, aGameSetup);
			}
		}

		private SteamLibraryPath GetSelectedSteamLibraryPath()
		{
			SteamLibraryPath aSteamLibraryPath = null;
			int selectedRowIndex = 0;

			if (SteamLibraryPathsDataGridView.SelectedCells.Count > 0)
			{
				selectedRowIndex = SteamLibraryPathsDataGridView.SelectedCells[0].RowIndex;
			}
			else
			{
				selectedRowIndex = 0;
			}
			aSteamLibraryPath = MainCROWBAR.TheApp.Settings.SteamLibraryPaths[selectedRowIndex];

			return aSteamLibraryPath;
		}

		private void SetMacroInOneGameSetup(string oldText, string newText, GameSetup aGameSetup)
		{
			string tempVar = aGameSetup.GamePathFileNameUnprocessed;
			SetMacroInText(oldText, newText, ref tempVar);
				aGameSetup.GamePathFileNameUnprocessed = tempVar;
			string tempVar2 = aGameSetup.GameAppPathFileNameUnprocessed;
			SetMacroInText(oldText, newText, ref tempVar2);
				aGameSetup.GameAppPathFileNameUnprocessed = tempVar2;
			string tempVar3 = aGameSetup.CompilerPathFileNameUnprocessed;
			SetMacroInText(oldText, newText, ref tempVar3);
				aGameSetup.CompilerPathFileNameUnprocessed = tempVar3;
			string tempVar4 = aGameSetup.ViewerPathFileNameUnprocessed;
			SetMacroInText(oldText, newText, ref tempVar4);
				aGameSetup.ViewerPathFileNameUnprocessed = tempVar4;
			string tempVar5 = aGameSetup.MappingToolPathFileNameUnprocessed;
			SetMacroInText(oldText, newText, ref tempVar5);
				aGameSetup.MappingToolPathFileNameUnprocessed = tempVar5;
			string tempVar6 = aGameSetup.PackerPathFileNameUnprocessed;
			SetMacroInText(oldText, newText, ref tempVar6);
				aGameSetup.PackerPathFileNameUnprocessed = tempVar6;
		}

		private void SetMacroInText(string oldText, string newText, ref string fullText)
		{
			if (oldText == "<>" && fullText.StartsWith("<"))
			{
				int index = fullText.IndexOf(">");
				if (index >= 1)
				{
					string nonMacroText = fullText.Substring(index + 1);
					fullText = newText + nonMacroText;
				}
			}
			else if (fullText.StartsWith(oldText))
			{
				string remainingText = fullText.Substring(oldText.Length);
				fullText = newText + remainingText;
			}
		}

#endregion

#region Data

		private GameSetup theSelectedGameSetup;

#endregion

	}

}