//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public partial class OptionsUserControl
	{

#region Creation and Destruction

		public OptionsUserControl()
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

		private void Init()
		{
			SingleInstanceCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "AppIsSingleInstance", false, DataSourceUpdateMode.OnPropertyChanged);

			// Auto-Open

			AutoOpenVpkFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenVpkFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			AutoOpenGmaFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenGmaFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			AutoOpenFpxFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenFpxFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			AutoOpenMdlFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenMdlFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			AutoOpenMdlFileForPreviewCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenMdlFileForPreviewIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			AutoOpenMdlFileForDecompileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenMdlFileForDecompileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			AutoOpenMdlFileForViewCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenMdlFileForViewIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			AutoOpenQcFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenQcFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			InitAutoOpenRadioButtons();

			// Drag and Drop

			DragAndDropMdlFileForPreviewCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDragAndDropMdlFileForPreviewIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			DragAndDropMdlFileForDecompileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDragAndDropMdlFileForDecompileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			DragAndDropMdlFileForViewCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDragAndDropMdlFileForViewIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			InitDragAndDropRadioButtons();

			// Context Menu

			IntegrateContextMenuItemsCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsContextMenuIntegrateMenuItemsIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			IntegrateAsSubmenuCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsContextMenuIntegrateSubMenuIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			//Me.OptionsContextMenuDecompileVpkFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackVpkFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
			//Me.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
			//Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackFolderAndSubfoldersIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

			OptionsContextMenuOpenWithCrowbarCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsOpenWithCrowbarIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			OptionsContextMenuViewMdlFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsViewMdlFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			OptionsContextMenuDecompileMdlFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDecompileMdlFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			OptionsContextMenuDecompileFolderCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDecompileFolderIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDecompileFolderAndSubfoldersIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			OptionsContextMenuCompileQcFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsCompileQcFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			OptionsContextMenuCompileFolderCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsCompileFolderIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsCompileFolderAndSubfoldersIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			UpdateApplyPanel();

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
		}

		private void InitAutoOpenRadioButtons()
		{
			AutoOpenVpkFileForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileOption == AppEnums.ActionType.Unpack);
			AutoOpenVpkFileForPublishRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileOption == AppEnums.ActionType.Publish);
			AutoOpenGmaFileForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileOption == AppEnums.ActionType.Unpack);
			AutoOpenGmaFileForPublishRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileOption == AppEnums.ActionType.Publish);

			AutoOpenMdlFileForPreviewingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption == AppEnums.ActionType.Preview);
			AutoOpenMdlFileForDecompilingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption == AppEnums.ActionType.Decompile);
			AutoOpenMdlFileForViewingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption == AppEnums.ActionType.View);

			AutoOpenFolderForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption == AppEnums.ActionType.Unpack);
			AutoOpenFolderForDecompileRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption == AppEnums.ActionType.Decompile);
			AutoOpenFolderForCompileRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption == AppEnums.ActionType.Compile);
			AutoOpenFolderForPackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption == AppEnums.ActionType.Pack);
		}

		private void InitDragAndDropRadioButtons()
		{
			DragAndDropVpkFileForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropVpkFileOption == AppEnums.ActionType.Unpack);
			DragAndDropVpkFileForPublishRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropVpkFileOption == AppEnums.ActionType.Publish);
			DragAndDropGmaFileForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropGmaFileOption == AppEnums.ActionType.Unpack);
			DragAndDropGmaFileForPublishRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropGmaFileOption == AppEnums.ActionType.Publish);

			DragAndDropMdlFileForPreviewingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption == AppEnums.ActionType.Preview);
			DragAndDropMdlFileForDecompilingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption == AppEnums.ActionType.Decompile);
			DragAndDropMdlFileForViewingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption == AppEnums.ActionType.View);

			DragAndDropFolderForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption == AppEnums.ActionType.Unpack);
			DragAndDropFolderForDecompileRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption == AppEnums.ActionType.Decompile);
			DragAndDropFolderForCompileRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption == AppEnums.ActionType.Compile);
			DragAndDropFolderForPackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption == AppEnums.ActionType.Pack);
		}

		private void Free()
		{
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;

			SingleInstanceCheckBox.DataBindings.Clear();

			// Auto-Open

			AutoOpenVpkFileCheckBox.DataBindings.Clear();
			AutoOpenGmaFileCheckBox.DataBindings.Clear();
			AutoOpenFpxFileCheckBox.DataBindings.Clear();
			AutoOpenMdlFileCheckBox.DataBindings.Clear();
			AutoOpenMdlFileForPreviewCheckBox.DataBindings.Clear();
			AutoOpenMdlFileForDecompileCheckBox.DataBindings.Clear();
			AutoOpenMdlFileForViewCheckBox.DataBindings.Clear();
			AutoOpenQcFileCheckBox.DataBindings.Clear();

			// Drag and Drop

			DragAndDropMdlFileForPreviewCheckBox.DataBindings.Clear();
			DragAndDropMdlFileForDecompileCheckBox.DataBindings.Clear();
			DragAndDropMdlFileForViewCheckBox.DataBindings.Clear();

			// Context Menu

			IntegrateContextMenuItemsCheckBox.DataBindings.Clear();
			IntegrateAsSubmenuCheckBox.DataBindings.Clear();

			OptionsContextMenuOpenWithCrowbarCheckBox.DataBindings.Clear();
			OptionsContextMenuViewMdlFileCheckBox.DataBindings.Clear();

			OptionsContextMenuDecompileMdlFileCheckBox.DataBindings.Clear();
			OptionsContextMenuDecompileFolderCheckBox.DataBindings.Clear();
			OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Clear();

			OptionsContextMenuCompileQcFileCheckBox.DataBindings.Clear();
			OptionsContextMenuCompileFolderCheckBox.DataBindings.Clear();
			OptionsContextMenuCompileFolderAndSubfoldersCheckBox.DataBindings.Clear();
		}

#endregion

#region Properties

#endregion

#region Widget Event Handlers

		private void OptionsUserControl_Disposed(object sender, EventArgs e)
		{
			Free();
		}

#endregion

#region Child Widget Event Handlers

		private void AutoOpenVpkFileRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (AutoOpenVpkFileForUnpackRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileOption = AppEnums.ActionType.Unpack;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileOption = AppEnums.ActionType.Publish;
			}
		}

		private void AutoOpenGmaFileRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (AutoOpenGmaFileForUnpackRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileOption = AppEnums.ActionType.Unpack;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileOption = AppEnums.ActionType.Publish;
			}
		}

		private void AutoOpenMdlFileRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (AutoOpenMdlFileForPreviewingRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption = AppEnums.ActionType.Preview;
			}
			else if (AutoOpenMdlFileForDecompilingRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption = AppEnums.ActionType.Decompile;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption = AppEnums.ActionType.View;
			}
		}

		private void AutoOpenUseDefaultsButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultOptionsAutoOpenOptions();
			InitAutoOpenRadioButtons();
		}

		private void AutoOpenFolderRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (AutoOpenFolderForUnpackRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption = AppEnums.ActionType.Unpack;
			}
			else if (AutoOpenFolderForDecompileRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption = AppEnums.ActionType.Decompile;
			}
			else if (AutoOpenFolderForCompileRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption = AppEnums.ActionType.Compile;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption = AppEnums.ActionType.Pack;
			}
		}

		private void DragAndDropVpkFileRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (DragAndDropVpkFileForUnpackRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropVpkFileOption = AppEnums.ActionType.Unpack;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropVpkFileOption = AppEnums.ActionType.Publish;
			}
		}

		private void DragAndDropGmaFileRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (DragAndDropGmaFileForUnpackRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropGmaFileOption = AppEnums.ActionType.Unpack;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropGmaFileOption = AppEnums.ActionType.Publish;
			}
		}

		private void DragAndDropMdlFileRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (DragAndDropMdlFileForPreviewingRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption = AppEnums.ActionType.Preview;
			}
			else if (DragAndDropMdlFileForDecompilingRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption = AppEnums.ActionType.Decompile;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption = AppEnums.ActionType.View;
			}
		}

		private void DragAndDropFolderRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (DragAndDropFolderForUnpackRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption = AppEnums.ActionType.Unpack;
			}
			else if (DragAndDropFolderForDecompileRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption = AppEnums.ActionType.Decompile;
			}
			else if (DragAndDropFolderForCompileRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption = AppEnums.ActionType.Compile;
			}
			else
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption = AppEnums.ActionType.Pack;
			}
		}

		private void DragAndDropUseDefaultsButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultOptionsDragAndDropOptions();
			InitDragAndDropRadioButtons();
		}

		private void ContextMenuUseDefaultsButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultOptionsContextMenuOptions();
		}

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			ApplyAllAutoOpenOptions();
		}

#endregion

#region Core Event Handlers

		private void AppSettings_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "AppIsSingleInstance")
			{
				MainCROWBAR.TheApp.SaveAppSettings();
			}
			else if (e.PropertyName == "OptionsAutoOpenVpkFileIsChecked")
			{
				ApplyAutoOpenVpkFileOptions();
			}
			else if (e.PropertyName == "OptionsAutoOpenGmaFileIsChecked")
			{
				ApplyAutoOpenGmaFileOptions();
			}
			else if (e.PropertyName == "OptionsAutoOpenFpxFileIsChecked")
			{
				ApplyAutoOpenFpxFileOptions();
			}
			else if (e.PropertyName == "OptionsAutoOpenMdlFileIsChecked")
			{
				ApplyAutoOpenMdlFileOptions();
			}
			else if (e.PropertyName == "OptionsAutoOpenQcFileIsChecked")
			{
				ApplyAutoOpenQcFileOptions();
			}
		}

#endregion

#region Private Methods

		private void ApplyAutoOpenVpkFileOptions()
		{
			if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileIsChecked)
			{
				Win32Api.CreateFileAssociation("vpk", "vpkFile", "VPK File", Application.ExecutablePath);
			}
			else
			{
				Win32Api.DeleteFileAssociation("vpk", "vpkFile", "VPK File", Application.ExecutablePath);
			}
		}

		private void ApplyAutoOpenGmaFileOptions()
		{
			if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileIsChecked)
			{
				Win32Api.CreateFileAssociation("gma", "gmaFile", "GMA File", Application.ExecutablePath);
			}
			else
			{
				Win32Api.DeleteFileAssociation("gma", "gmaFile", "GMA File", Application.ExecutablePath);
			}
		}

		private void ApplyAutoOpenFpxFileOptions()
		{
			if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFpxFileIsChecked)
			{
				Win32Api.CreateFileAssociation("fpx", "fpxFile", "FPX File", Application.ExecutablePath);
			}
			else
			{
				Win32Api.DeleteFileAssociation("fpx", "fpxFile", "FPX File", Application.ExecutablePath);
			}
		}

		private void ApplyAutoOpenMdlFileOptions()
		{
			if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileIsChecked)
			{
				Win32Api.CreateFileAssociation("mdl", "mdlFile", "MDL File", Application.ExecutablePath);
			}
			else
			{
				Win32Api.DeleteFileAssociation("mdl", "mdlFile", "MDL File", Application.ExecutablePath);
			}
		}

		private void ApplyAutoOpenQcFileOptions()
		{
			if (MainCROWBAR.TheApp.Settings.OptionsAutoOpenQcFileIsChecked)
			{
				Win32Api.CreateFileAssociation("qc", "qcFile", "QC File", Application.ExecutablePath);
			}
			else
			{
				Win32Api.DeleteFileAssociation("qc", "qcFile", "QC File", Application.ExecutablePath);
			}
		}

		private void ApplyAllAutoOpenOptions()
		{
			ApplyAutoOpenVpkFileOptions();
			ApplyAutoOpenGmaFileOptions();
			ApplyAutoOpenFpxFileOptions();
			ApplyAutoOpenMdlFileOptions();
			ApplyAutoOpenQcFileOptions();

			UpdateApplyPanel();
		}

		private void UpdateApplyPanel()
		{
			bool vpkFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("vpk", "vpkFile", "VPK File", Application.ExecutablePath);

			bool gmaFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("gma", "gmaFile", "GMA File", Application.ExecutablePath);

			bool fpxFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("fpx", "fpxFile", "FPX File", Application.ExecutablePath);

			bool mdlFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("mdl", "mdlFile", "MDL File", Application.ExecutablePath);

			bool qcFileAssociationIsAlreadyAssigned = Win32Api.FileAssociationIsAlreadyAssigned("qc", "qcFile", "QC File", Application.ExecutablePath);

			//Me.ApplyPanel.Visible = (Not vpkFileAssociationIsAlreadyAssigned) OrElse (Not mdlFileAssociationIsAlreadyAssigned) OrElse (Not qcFileAssociationIsAlreadyAssigned)
			bool applyPanelShouldBeVisible = false;
			if (vpkFileAssociationIsAlreadyAssigned != MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileIsChecked)
			{
				applyPanelShouldBeVisible = true;
			}
			else if (gmaFileAssociationIsAlreadyAssigned != MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileIsChecked)
			{
				applyPanelShouldBeVisible = true;
			}
			else if (fpxFileAssociationIsAlreadyAssigned != MainCROWBAR.TheApp.Settings.OptionsAutoOpenFpxFileIsChecked)
			{
				applyPanelShouldBeVisible = true;
			}
			else if (mdlFileAssociationIsAlreadyAssigned != MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileIsChecked)
			{
				applyPanelShouldBeVisible = true;
			}
			else if (qcFileAssociationIsAlreadyAssigned != MainCROWBAR.TheApp.Settings.OptionsAutoOpenQcFileIsChecked)
			{
				applyPanelShouldBeVisible = true;
			}
			ApplyPanel.Visible = applyPanelShouldBeVisible;
		}

#endregion

#region Data

#endregion

	}

}