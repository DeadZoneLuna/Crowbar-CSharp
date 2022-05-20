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
				this.Init();
			}
			catch
			{
			}
		}

#endregion

#region Init and Free

		private void Init()
		{
			this.SingleInstanceCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "AppIsSingleInstance", false, DataSourceUpdateMode.OnPropertyChanged);

			// Auto-Open

			this.AutoOpenVpkFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenVpkFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AutoOpenGmaFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenGmaFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AutoOpenFpxFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenFpxFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AutoOpenMdlFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenMdlFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AutoOpenMdlFileForPreviewCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenMdlFileForPreviewIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AutoOpenMdlFileForDecompileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenMdlFileForDecompileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AutoOpenMdlFileForViewCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenMdlFileForViewIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AutoOpenQcFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsAutoOpenQcFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.InitAutoOpenRadioButtons();

			// Drag and Drop

			this.DragAndDropMdlFileForPreviewCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDragAndDropMdlFileForPreviewIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.DragAndDropMdlFileForDecompileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDragAndDropMdlFileForDecompileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.DragAndDropMdlFileForViewCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDragAndDropMdlFileForViewIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.InitDragAndDropRadioButtons();

			// Context Menu

			this.IntegrateContextMenuItemsCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsContextMenuIntegrateMenuItemsIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.IntegrateAsSubmenuCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsContextMenuIntegrateSubMenuIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			//Me.OptionsContextMenuDecompileVpkFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackVpkFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
			//Me.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
			//Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackFolderAndSubfoldersIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

			this.OptionsContextMenuOpenWithCrowbarCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsOpenWithCrowbarIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.OptionsContextMenuViewMdlFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsViewMdlFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.OptionsContextMenuDecompileMdlFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDecompileMdlFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDecompileFolderIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsDecompileFolderAndSubfoldersIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.OptionsContextMenuCompileQcFileCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsCompileQcFileIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.OptionsContextMenuCompileFolderCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsCompileFolderIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", MainCROWBAR.TheApp.Settings, "OptionsCompileFolderAndSubfoldersIsChecked", false, DataSourceUpdateMode.OnPropertyChanged);

			this.UpdateApplyPanel();

			MainCROWBAR.TheApp.Settings.PropertyChanged += AppSettings_PropertyChanged;
		}

		private void InitAutoOpenRadioButtons()
		{
			this.AutoOpenVpkFileForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileOption == AppEnums.ActionType.Unpack);
			this.AutoOpenVpkFileForPublishRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenVpkFileOption == AppEnums.ActionType.Publish);
			this.AutoOpenGmaFileForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileOption == AppEnums.ActionType.Unpack);
			this.AutoOpenGmaFileForPublishRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenGmaFileOption == AppEnums.ActionType.Publish);

			this.AutoOpenMdlFileForPreviewingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption == AppEnums.ActionType.Preview);
			this.AutoOpenMdlFileForDecompilingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption == AppEnums.ActionType.Decompile);
			this.AutoOpenMdlFileForViewingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption == AppEnums.ActionType.View);

			this.AutoOpenFolderForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption == AppEnums.ActionType.Unpack);
			this.AutoOpenFolderForDecompileRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption == AppEnums.ActionType.Decompile);
			this.AutoOpenFolderForCompileRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption == AppEnums.ActionType.Compile);
			this.AutoOpenFolderForPackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption == AppEnums.ActionType.Pack);
		}

		private void InitDragAndDropRadioButtons()
		{
			this.DragAndDropVpkFileForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropVpkFileOption == AppEnums.ActionType.Unpack);
			this.DragAndDropVpkFileForPublishRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropVpkFileOption == AppEnums.ActionType.Publish);
			this.DragAndDropGmaFileForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropGmaFileOption == AppEnums.ActionType.Unpack);
			this.DragAndDropGmaFileForPublishRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropGmaFileOption == AppEnums.ActionType.Publish);

			this.DragAndDropMdlFileForPreviewingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption == AppEnums.ActionType.Preview);
			this.DragAndDropMdlFileForDecompilingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption == AppEnums.ActionType.Decompile);
			this.DragAndDropMdlFileForViewingRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption == AppEnums.ActionType.View);

			this.DragAndDropFolderForUnpackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption == AppEnums.ActionType.Unpack);
			this.DragAndDropFolderForDecompileRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption == AppEnums.ActionType.Decompile);
			this.DragAndDropFolderForCompileRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption == AppEnums.ActionType.Compile);
			this.DragAndDropFolderForPackRadioButton.Checked = (MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption == AppEnums.ActionType.Pack);
		}

		private void Free()
		{
			MainCROWBAR.TheApp.Settings.PropertyChanged -= AppSettings_PropertyChanged;

			this.SingleInstanceCheckBox.DataBindings.Clear();

			// Auto-Open

			this.AutoOpenVpkFileCheckBox.DataBindings.Clear();
			this.AutoOpenGmaFileCheckBox.DataBindings.Clear();
			this.AutoOpenFpxFileCheckBox.DataBindings.Clear();
			this.AutoOpenMdlFileCheckBox.DataBindings.Clear();
			this.AutoOpenMdlFileForPreviewCheckBox.DataBindings.Clear();
			this.AutoOpenMdlFileForDecompileCheckBox.DataBindings.Clear();
			this.AutoOpenMdlFileForViewCheckBox.DataBindings.Clear();
			this.AutoOpenQcFileCheckBox.DataBindings.Clear();

			// Drag and Drop

			this.DragAndDropMdlFileForPreviewCheckBox.DataBindings.Clear();
			this.DragAndDropMdlFileForDecompileCheckBox.DataBindings.Clear();
			this.DragAndDropMdlFileForViewCheckBox.DataBindings.Clear();

			// Context Menu

			this.IntegrateContextMenuItemsCheckBox.DataBindings.Clear();
			this.IntegrateAsSubmenuCheckBox.DataBindings.Clear();

			this.OptionsContextMenuOpenWithCrowbarCheckBox.DataBindings.Clear();
			this.OptionsContextMenuViewMdlFileCheckBox.DataBindings.Clear();

			this.OptionsContextMenuDecompileMdlFileCheckBox.DataBindings.Clear();
			this.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Clear();
			this.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Clear();

			this.OptionsContextMenuCompileQcFileCheckBox.DataBindings.Clear();
			this.OptionsContextMenuCompileFolderCheckBox.DataBindings.Clear();
			this.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.DataBindings.Clear();
		}

#endregion

#region Properties

#endregion

#region Widget Event Handlers

		private void OptionsUserControl_Disposed(object sender, EventArgs e)
		{
			this.Free();
		}

#endregion

#region Child Widget Event Handlers

		private void AutoOpenVpkFileRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.AutoOpenVpkFileForUnpackRadioButton.Checked)
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
			if (this.AutoOpenGmaFileForUnpackRadioButton.Checked)
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
			if (this.AutoOpenMdlFileForPreviewingRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenMdlFileOption = AppEnums.ActionType.Preview;
			}
			else if (this.AutoOpenMdlFileForDecompilingRadioButton.Checked)
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
			this.InitAutoOpenRadioButtons();
		}

		private void AutoOpenFolderRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (this.AutoOpenFolderForUnpackRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption = AppEnums.ActionType.Unpack;
			}
			else if (this.AutoOpenFolderForDecompileRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsAutoOpenFolderOption = AppEnums.ActionType.Decompile;
			}
			else if (this.AutoOpenFolderForCompileRadioButton.Checked)
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
			if (this.DragAndDropVpkFileForUnpackRadioButton.Checked)
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
			if (this.DragAndDropGmaFileForUnpackRadioButton.Checked)
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
			if (this.DragAndDropMdlFileForPreviewingRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropMdlFileOption = AppEnums.ActionType.Preview;
			}
			else if (this.DragAndDropMdlFileForDecompilingRadioButton.Checked)
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
			if (this.DragAndDropFolderForUnpackRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption = AppEnums.ActionType.Unpack;
			}
			else if (this.DragAndDropFolderForDecompileRadioButton.Checked)
			{
				MainCROWBAR.TheApp.Settings.OptionsDragAndDropFolderOption = AppEnums.ActionType.Decompile;
			}
			else if (this.DragAndDropFolderForCompileRadioButton.Checked)
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
			this.InitDragAndDropRadioButtons();
		}

		private void ContextMenuUseDefaultsButton_Click(object sender, EventArgs e)
		{
			MainCROWBAR.TheApp.Settings.SetDefaultOptionsContextMenuOptions();
		}

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			this.ApplyAllAutoOpenOptions();
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
				this.ApplyAutoOpenVpkFileOptions();
			}
			else if (e.PropertyName == "OptionsAutoOpenGmaFileIsChecked")
			{
				this.ApplyAutoOpenGmaFileOptions();
			}
			else if (e.PropertyName == "OptionsAutoOpenFpxFileIsChecked")
			{
				this.ApplyAutoOpenFpxFileOptions();
			}
			else if (e.PropertyName == "OptionsAutoOpenMdlFileIsChecked")
			{
				this.ApplyAutoOpenMdlFileOptions();
			}
			else if (e.PropertyName == "OptionsAutoOpenQcFileIsChecked")
			{
				this.ApplyAutoOpenQcFileOptions();
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
			this.ApplyAutoOpenVpkFileOptions();
			this.ApplyAutoOpenGmaFileOptions();
			this.ApplyAutoOpenFpxFileOptions();
			this.ApplyAutoOpenMdlFileOptions();
			this.ApplyAutoOpenQcFileOptions();

			this.UpdateApplyPanel();
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
			this.ApplyPanel.Visible = applyPanelShouldBeVisible;
		}

#endregion

#region Data

#endregion

	}

}