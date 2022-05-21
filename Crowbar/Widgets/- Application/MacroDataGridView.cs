//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace Crowbar
{
	public class MacroDataGridView : DataGridView
	{
#region Create and Destroy

		public MacroDataGridView() : base()
		{

			InitializeComponent();

			RowHeadersWidth = 25;
			RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

			//Me.theWidgetTempOfAllowUserToAddRows = Me.AllowUserToAddRows
			//Me.theWidgetTempOfAllowUserToDeleteRows = Me.AllowUserToDeleteRows
			theWidgetIsOpening = true;
			//Me.theMinimumRowCount = 0

			//'NOTE: This "if" is here only so Forms can show in VS Designer. DesignMode property doesn't work in New().
			//If TheApp IsNot Nothing Then
			//	Me.Init()
			//End If
		}

		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				if (disposing)
				{
					if (components != null)
					{
						components.Dispose();
					}
					//Me.Free()
				}
			}
			base.Dispose(disposing);
		}

#endregion

#region Init and Free

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			DataGridViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
			//Me.InsertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			//Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			//Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			DataGridViewContextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();

			SetMacroInSelectedGameSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			SetMacroInSelectedGameSetupToolStripMenuItem.Name = "SetMacroInSelectedGameSetupToolStripMenuItem";
			SetMacroInSelectedGameSetupToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			SetMacroInSelectedGameSetupToolStripMenuItem.Text = "Set macro in selected game setup(s)";

			ClearMacroInSelectedGameSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ClearMacroInSelectedGameSetupToolStripMenuItem.Name = "ClearMacroInSelectedGameSetupToolStripMenuItem";
			ClearMacroInSelectedGameSetupToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			ClearMacroInSelectedGameSetupToolStripMenuItem.Text = "Clear macro in selected game setup(s)";

			ChangeToThisMacroInSelectedGameSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Name = "ChangeToThisMacroInSelectedGameSetupToolStripMenuItem";
			ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Text = "Change to this macro in selected game setup(s)";

			MacroMenuToolStripSeparator0 = new System.Windows.Forms.ToolStripSeparator();
			MacroMenuToolStripSeparator0.Name = "MacroMenuToolStripSeparator0";

			SetMacroInAllGameSetupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			SetMacroInAllGameSetupsToolStripMenuItem.Name = "SetMacroInAllGameSetupsToolStripMenuItem";
			SetMacroInAllGameSetupsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			SetMacroInAllGameSetupsToolStripMenuItem.Text = "Set macro in all game setups";

			ClearMacroInAllGameSetupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ClearMacroInAllGameSetupsToolStripMenuItem.Name = "ClearMacroInAllGameSetupsToolStripMenuItem";
			ClearMacroInAllGameSetupsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			ClearMacroInAllGameSetupsToolStripMenuItem.Text = "Clear macro in all game setups";

			ChangeToThisMacroInAllGameSetupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Name = "ChangeToThisMacroInAllGameSetupsToolStripMenuItem";
			ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Text = "Change to this macro in all game setups";

			MacroMenuToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			MacroMenuToolStripSeparator1.Name = "MacroMenuToolStripSeparator1";

			//'
			//'InsertToolStripMenuItem
			//'
			//Me.InsertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			//Me.InsertToolStripMenuItem.Name = "InsertToolStripMenuItem"
			//Me.InsertToolStripMenuItem.ShortcutKeyDisplayString = "Insert"
			//Me.InsertToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert
			//Me.InsertToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
			//Me.InsertToolStripMenuItem.Text = "Insert Rows"
			//'
			//'DeleteToolStripMenuItem
			//'
			//Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			//Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
			//Me.DeleteToolStripMenuItem.ShortcutKeyDisplayString = "Delete"
			//Me.DeleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
			//Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
			//Me.DeleteToolStripMenuItem.Text = "Delete Rows"
			//
			//CopyToolStripMenuItem
			//
			CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
			//NOTE: Do not add in the standard CTRL-C shortcut key, because it works without adding, 
			//      and adding it causes a selection of text within a cell to return Nothing from Me.GetClipboardContent().
			//Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
			CopyToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			CopyToolStripMenuItem.Text = "Copy";
			//'
			//'PasteToolStripMenuItem
			//'
			//Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			//Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
			//Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
			//Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
			//Me.PasteToolStripMenuItem.Text = "Paste"
			//
			//DataGridViewContextMenuStrip
			//
			//Me.DataGridViewContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InsertToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem})
			DataGridViewContextMenuStrip.Items.Add(SetMacroInSelectedGameSetupToolStripMenuItem);
			DataGridViewContextMenuStrip.Items.Add(ClearMacroInSelectedGameSetupToolStripMenuItem);
			DataGridViewContextMenuStrip.Items.Add(ChangeToThisMacroInSelectedGameSetupToolStripMenuItem);
			DataGridViewContextMenuStrip.Items.Add(MacroMenuToolStripSeparator0);
			DataGridViewContextMenuStrip.Items.Add(SetMacroInAllGameSetupsToolStripMenuItem);
			DataGridViewContextMenuStrip.Items.Add(ClearMacroInAllGameSetupsToolStripMenuItem);
			DataGridViewContextMenuStrip.Items.Add(ChangeToThisMacroInAllGameSetupsToolStripMenuItem);
			DataGridViewContextMenuStrip.Items.Add(MacroMenuToolStripSeparator1);
			DataGridViewContextMenuStrip.Items.Add(CopyToolStripMenuItem);
			DataGridViewContextMenuStrip.Name = "DataGridViewContextMenuStrip";
			DataGridViewContextMenuStrip.Size = new System.Drawing.Size(177, 114);
			//
			//
			//
			ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			ContextMenuStrip = DataGridViewContextMenuStrip;
			DataGridViewContextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);

//INSTANT C# NOTE: Converted design-time event handler wireups:
			DataGridViewContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(DataGridViewContextMenuStrip_Opening);
			CopyToolStripMenuItem.Click += new System.EventHandler(CopyToolStripMenuItem_Click);
		}

#endregion

#region Properties

		//Public Overloads Property [ReadOnly]() As Boolean
		//	Get
		//		Return MyBase.ReadOnly
		//	End Get
		//	Set(ByVal value As Boolean)
		//		If Me.ReadOnly <> value Then
		//			MyBase.ReadOnly = value
		//			'If MyBase.ReadOnly Then
		//			'	Me.ContextMenuStrip = Nothing
		//			'Else
		//			Me.ContextMenuStrip = Me.DataGridViewContextMenuStrip
		//			'End If
		//		End If
		//	End Set
		//End Property

		//Public Overloads Property MinimumRowCount() As Integer
		//	Get
		//		Return Me.theMinimumRowCount
		//	End Get
		//	Set(ByVal value As Integer)
		//		If Me.theMinimumRowCount <> value And value >= 0 Then
		//			Me.theMinimumRowCount = value
		//		End If
		//	End Set
		//End Property

#endregion

#region Methods

		//Public Overridable Sub AddRows(ByVal insertRowCount As Integer)
		//	Me.InsertRows(insertRowCount, False)
		//End Sub

		//Public Overridable Sub InsertRows(ByVal insertRowCount As Integer)
		//	Me.InsertRows(insertRowCount, True)
		//End Sub

		//Public Overridable Sub InsertSelectedRows()
		//	If Me.SelectedRows.Count > 0 Then
		//		Dim insertRowCount As Integer = 1
		//		insertRowCount = Me.SelectedRows.Count
		//		Me.InsertRows(insertRowCount)
		//	End If
		//End Sub

		//Public Overridable Sub DeleteSelectedRows()
		//	If Me.SelectedRows.Count > 0 Then
		//		' Prevent going under minimum row count.
		//		If (Me.Rows.Count - Me.SelectedRows.Count) >= Me.theMinimumRowCount Then
		//			Dim clickedRowIndex As Integer
		//			Dim clickedColumnIndex As Integer
		//			Me.GetFirstSelectedRowAndColumnIndexes(clickedRowIndex, clickedColumnIndex)

		//			Dim dataSourceList As IList
		//			dataSourceList = CType(Me.DataSource, Collections.IList)
		//			For Each row As DataGridViewRow In Me.SelectedRows
		//				If Not row.IsNewRow Then
		//					dataSourceList.RemoveAt(row.Index)
		//				End If
		//			Next

		//			Me.ClearSelection()
		//			If Me.RowCount > clickedRowIndex Then
		//				Me.Rows(clickedRowIndex).Selected = True
		//			End If
		//		End If
		//	End If
		//End Sub

#endregion

#region Private Methods

		//Private Sub GetFirstSelectedRowAndColumnIndexes(ByRef firstSelectedRowIndex As Integer, ByRef firstSelectedColumnIndex As Integer)
		//	firstSelectedRowIndex = Me.Rows.Count - 1
		//	'NOTE: Avoid counting "new row".
		//	If Me.AllowUserToAddRows Then
		//		firstSelectedRowIndex -= 1
		//	End If
		//	firstSelectedColumnIndex = 0
		//	If Me.SelectedRows.Count > 0 Then
		//		For i As Integer = 0 To Me.SelectedRows.Count - 1
		//			If Me.SelectedRows(i).Index < firstSelectedRowIndex Then
		//				firstSelectedRowIndex = Me.SelectedRows(i).Index
		//			End If
		//		Next
		//	ElseIf Me.SelectedCells.Count > 0 Then
		//		For i As Integer = 0 To Me.SelectedCells.Count - 1
		//			If Me.SelectedCells(i).RowIndex <= firstSelectedRowIndex Then
		//				firstSelectedRowIndex = Me.SelectedCells(i).RowIndex
		//				firstSelectedColumnIndex = Me.SelectedCells(i).ColumnIndex
		//			End If
		//		Next
		//	End If
		//End Sub

		//Private Sub InsertRows(ByVal insertRowCount As Integer, ByVal isInsertingRows As Boolean)
		//	If insertRowCount > 0 Then
		//		Dim clickedRowIndex As Integer
		//		Dim clickedColumnIndex As Integer
		//		Dim insertionRowIndex As Integer
		//		Me.GetFirstSelectedRowAndColumnIndexes(clickedRowIndex, clickedColumnIndex)
		//		If Not isInsertingRows Then
		//			insertionRowIndex = clickedRowIndex + 1
		//		ElseIf clickedRowIndex < 0 Then
		//			insertionRowIndex = 0
		//		Else
		//			insertionRowIndex = clickedRowIndex
		//		End If
		//		Me.InsertRows(insertRowCount, insertionRowIndex)

		//		Me.ClearSelection()
		//		Me.CurrentCell = Me.Rows(insertionRowIndex).Cells(0)
		//		For i As Integer = 0 To insertRowCount - 1
		//			Me.Rows(insertionRowIndex + i).Selected = True
		//		Next
		//	End If
		//End Sub

		//Private Sub InsertRows(ByVal insertRowCount As Integer, ByVal insertionRowIndex As Integer)
		//	If Me.Rows.Count = 0 Then
		//		If TypeOf Me.DataSource Is System.ComponentModel.IBindingList Then
		//			Dim dataSourceBindingList As System.ComponentModel.IBindingList
		//			dataSourceBindingList = CType(Me.DataSource, System.ComponentModel.IBindingList)
		//			For i As Integer = 1 To insertRowCount
		//				'NOTE: This doesn't work if class uses a New(boolean) constructor instead of default New() constructor.
		//				'      The New() constructor for classes should be changed back to Public, so that this will work, 
		//				'      but need to be aware that default New() will make object read-only.
		//				'      Avoid the creating of read-only by using this kind of event handler:
		//				'		Private Sub TemperatureProbes_AddingNew(ByVal sender As Object, ByVal e As AddingNewEventArgs) Handles theTemperatureProbes.AddingNew
		//				'		e.NewObject = New MaxbrgTemperatureProbe(True)
		//				'		End Sub
		//				'      Only need to change New() for classes that will be used in lists that allow zero items 
		//				'      (as indicated by the "If Me.Rows.Count = 0 Then" statement a few lines above). 
		//				dataSourceBindingList.AddNew()
		//				Me.EndEdit()
		//			Next
		//		Else
		//			Exit Sub
		//		End If
		//	Else
		//		Dim dataSourceList As IList
		//		dataSourceList = CType(Me.DataSource, Collections.IList)
		//		Dim copiedObject As ICloneable

		//		If insertionRowIndex < 1 Then
		//			copiedObject = CType(Me.Rows(0).DataBoundItem, ICloneable)
		//		Else
		//			copiedObject = CType(Me.Rows(insertionRowIndex - 1).DataBoundItem, ICloneable)
		//		End If

		//		For i As Integer = 1 To insertRowCount
		//			dataSourceList.Insert(insertionRowIndex, copiedObject.Clone())
		//			insertionRowIndex += 1
		//		Next
		//	End If
		//End Sub

		private void CopyData()
		{
			DataObject data = GetClipboardContent();
			try
			{
				if (data != null)
				{
					Clipboard.SetDataObject(data);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		//Private Sub PasteData()
		//	Dim copiedLines() As String
		//	Dim columnValues() As String
		//	Dim startColumnIndex As Integer
		//	Dim columnIndex As Integer
		//	Dim rowIndex As Integer
		//	Dim row As DataGridViewRow
		//	Dim cell As DataGridViewCell = Nothing
		//	Dim line As String

		//	Me.GetFirstSelectedRowAndColumnIndexes(rowIndex, startColumnIndex)

		//	copiedLines = Clipboard.GetText().Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)

		//	For i As Integer = 0 To copiedLines.Length - 1
		//		line = copiedLines(i)

		//		columnIndex = startColumnIndex

		//		If rowIndex = Me.NewRowIndex Then
		//			Me.InsertRows(1, rowIndex)
		//		End If
		//		If rowIndex >= Me.RowCount Then
		//			Exit For
		//		End If
		//		row = Me.Rows(rowIndex)

		//		columnValues = line.Split(CChar(vbTab))
		//		For Each columnValue As String In columnValues
		//			cell = row.Cells(columnIndex)

		//			Try
		//				If Not cell.ReadOnly Then
		//					'' Store the original value of a cell to help with raising a PropertyChanged event later.
		//					'If TypeOf cell.Value Is PhysicalProperty Then
		//					'	valueOfChangedCell = CType(cell.Value, PhysicalProperty).Clone()
		//					'Else
		//					'	valueOfChangedCell = cell.Value
		//					'End If

		//					Me.OnCellParsing(New DataGridViewCellParsingEventArgs(rowIndex, columnIndex, columnValue, cell.ValueType, cell.InheritedStyle))
		//					'NOTE: OnCellParsing above does not automatically set the value 
		//					'      because columnValue is a String type and cell.Value is usually a Double type.
		//					cell.Value = columnValue
		//				End If
		//			Catch ex As Exception
		//				Dim debug As Integer = 4242
		//			End Try

		//			columnIndex += 1
		//			If columnIndex >= Me.Columns.Count Then
		//				Exit For
		//			End If
		//		Next

		//		'If assembly IsNot Nothing Then
		//		'	part.IsReadOnly = dataItemIsReadOnly
		//		'End If
		//		rowIndex += 1
		//	Next

		//	'NOTE: Needed to show the new values.
		//	Me.Refresh()
		//End Sub

#endregion

#region Core Event Handlers

#endregion

#region Widget Event Handlers

		//Protected Overrides Sub OnCellBeginEdit(ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs)
		//	MyBase.OnCellBeginEdit(e)
		//	Me.ContextMenuStrip = Nothing
		//	Me.theWidgetTempOfAllowUserToDeleteRows = Me.AllowUserToDeleteRows
		//	Me.AllowUserToDeleteRows = False
		//End Sub

		//Protected Overrides Sub OnCellEndEdit(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
		//	Me.AllowUserToDeleteRows = Me.theWidgetTempOfAllowUserToDeleteRows
		//	Me.ContextMenuStrip = Me.DataGridViewContextMenuStrip
		//	MyBase.OnCellEndEdit(e)
		//End Sub

		protected override void OnDataSourceChanged(EventArgs e)
		{
			base.OnDataSourceChanged(e);

			AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Control)
			{
				if (e.KeyCode == Keys.C)
				{
					CopyData();
					//ElseIf e.KeyCode = Keys.V Then
					//	Me.PasteData()
				}
			}
			base.OnKeyDown(e);
		}

		//Protected Overloads Overrides Function ProcessDataGridViewKey(ByVal e As KeyEventArgs) As Boolean
		//	If e.KeyCode = Keys.Delete AndAlso Me.AllowUserToDeleteRows Then
		//		' Arrives here when user right-clicks to show context menu for selected row, cancels menu, then presses Del key.
		//		Me.theWidgetTempOfAllowUserToAddRows = Me.AllowUserToAddRows
		//		Me.AllowUserToAddRows = False
		//		Me.theWidgetTempOfAllowUserToDeleteRows = Me.AllowUserToDeleteRows
		//		Me.AllowUserToDeleteRows = False
		//		Me.DeleteSelectedRows()
		//		Me.AllowUserToAddRows = Me.theWidgetTempOfAllowUserToAddRows
		//		Me.AllowUserToDeleteRows = Me.theWidgetTempOfAllowUserToDeleteRows
		//		Return True
		//	End If
		//	Return MyBase.ProcessDataGridViewKey(e)
		//End Function

		protected override void OnVisibleChanged(System.EventArgs e)
		{
			//NOTE: If this is being disposed, don't bother updating and resizing. 
			//      I don't know how to detect that this is called while disposing.
			//      Maybe this is called when parent control is disposing.
			//      Using Me.theWidgetIsOpening to only update when first opening.

			if (theWidgetIsOpening && Visible && Columns.Count > 0)
			{
				theWidgetIsOpening = false;
				//NOTE: This needs to be done here instead of Init because the DataGridView must be visible to auto resize.
				AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
			}

			base.OnVisibleChanged(e);
		}

#endregion

#region Child Widget Event Handlers

		private void DataGridViewContextMenuStrip_Opening(System.Object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Prevent certain exceptions by making sure datagridview is not in edit mode.
			CancelEdit();

			SetMacroInSelectedGameSetupToolStripMenuItem.Enabled = SelectedCells.Count > 0;
			SetMacroInAllGameSetupsToolStripMenuItem.Enabled = SelectedCells.Count > 0;
			ClearMacroInSelectedGameSetupToolStripMenuItem.Enabled = SelectedCells.Count > 0;
			ClearMacroInAllGameSetupsToolStripMenuItem.Enabled = SelectedCells.Count > 0;
			ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Enabled = SelectedCells.Count > 0;
			ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Enabled = SelectedCells.Count > 0;

			//Me.InsertToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.AllowUserToAddRows

			//Me.theWidgetTempOfAllowUserToAddRows = Me.AllowUserToAddRows
			//Me.AllowUserToAddRows = False
			//Me.DeleteToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Me.AllowUserToDeleteRows AndAlso Me.SelectedRows.Count > 0 AndAlso Me.SelectedRows.Count <= Me.Rows.Count - Me.theMinimumRowCount
			//Me.AllowUserToAddRows = Me.theWidgetTempOfAllowUserToAddRows

			CopyToolStripMenuItem.Enabled = SelectedCells.Count > 0;
			//Me.PasteToolStripMenuItem.Enabled = Not Me.ReadOnly AndAlso Clipboard.ContainsText
		}

		//Private Sub InsertToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertToolStripMenuItem.Click
		//	Me.theWidgetTempOfAllowUserToAddRows = Me.AllowUserToAddRows
		//	Me.AllowUserToAddRows = False
		//	Me.InsertSelectedRows()
		//	Me.AllowUserToAddRows = Me.theWidgetTempOfAllowUserToAddRows
		//End Sub

		//Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
		//	Me.theWidgetTempOfAllowUserToAddRows = Me.AllowUserToAddRows
		//	Me.AllowUserToAddRows = False
		//	Me.DeleteSelectedRows()
		//	Me.AllowUserToAddRows = Me.theWidgetTempOfAllowUserToAddRows
		//End Sub

		private void CopyToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			CopyData();
		}

		//Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
		//	Me.PasteData()
		//End Sub

#endregion

#region Data

		private bool theWidgetIsOpening;
		//Private theWidgetTempOfAllowUserToAddRows As Boolean
		//Private theWidgetTempOfAllowUserToDeleteRows As Boolean
		//Private theMinimumRowCount As Integer

		private System.ComponentModel.IContainer components;
		protected System.Windows.Forms.ContextMenuStrip DataGridViewContextMenuStrip;
		internal System.Windows.Forms.ToolStripMenuItem SetMacroInSelectedGameSetupToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ClearMacroInSelectedGameSetupToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ChangeToThisMacroInSelectedGameSetupToolStripMenuItem;
		protected System.Windows.Forms.ToolStripSeparator MacroMenuToolStripSeparator0;
		internal System.Windows.Forms.ToolStripMenuItem SetMacroInAllGameSetupsToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ClearMacroInAllGameSetupsToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ChangeToThisMacroInAllGameSetupsToolStripMenuItem;
		protected System.Windows.Forms.ToolStripSeparator MacroMenuToolStripSeparator1;
		//Protected WithEvents InsertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		//Protected WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		protected System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
		//Protected WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

#endregion

	}

}