using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.Collections.Specialized;
using System.IO;

// Code convereted from: http://www.codeproject.com/Articles/286326/Async-Drag-n-Drop-or-Drag-n-Drop-from-external-ser

namespace Crowbar
{
	public class FileDragDropHelper : DataObject
	{
		public FileDragDropHelper(StartupDelegate startupAction, CleanupDelegate cleanupAction)
		{
			theStartupAction = startupAction;
			theCleanupAction = cleanupAction;
			theFunctionHasBeenCalledOnce = false;
			pathFileNameIndex = 0;
		}

		//Public Overrides Function GetDataPresent(format As String) As Boolean
		//	'Dim fileDropList As StringCollection = GetFileDropList()

		//	'If fileDropList.Count > _downloadIndex Then
		//	'	Dim fileName As String = Path.GetFileName(fileDropList(_downloadIndex))

		//	'	If String.IsNullOrEmpty(fileName) Then
		//	'		Return False
		//	'	End If
		//	'	'_downloadAction(fileDropList(_downloadIndex))
		//	'	Me.theReadFileAction.Invoke()
		//	'	_downloadIndex += 1
		//	'End If
		//	'======
		//	Dim result As Boolean

		//	'If Not Me.theFunctionHasBeenCalledOnce Then
		//	'	Me.theStartupAction.Invoke()
		//	'	Me.theFunctionHasBeenCalledOnce = True
		//	'End If

		//	result = MyBase.GetDataPresent(format)

		//	'Dim pathFileNameCollection As StringCollection = GetFileDropList()
		//	'If pathFileNameCollection.Count > Me.pathFileNameIndex Then
		//	'	Me.pathFileNameIndex += 1
		//	'End If
		//	''If Me.pathFileNameIndex = pathFileNameCollection.Count Then
		//	''	Me.theCleanupAction.Invoke()
		//	''End If

		//	Return result
		//End Function

		public override object GetData(string format)
		{
			object result = null;

			//If Not Me.theFunctionHasBeenCalledOnce Then
			//	Me.theStartupAction.Invoke()
			//	Me.theFunctionHasBeenCalledOnce = True
			//End If

			//Dim pathFileNameCollection As StringCollection = GetFileDropList()
			//Dim test As String
			//test = pathFileNameCollection(0)

			if (string.Compare(format, Win32Api.CFSTR_FILEDESCRIPTORW, StringComparison.OrdinalIgnoreCase) == 0)
			{
				//MyBase.SetData(Win32Api.CFSTR_FILEDESCRIPTORW, GetFileDescriptor(m_SelectedItems))
				int debug = 4242;
			}

			result = base.GetData(format);

			return result;
		}

		//Public Overrides Function GetData(format As String) As Object
		//	Dim result As Object

		//	result = MyBase.GetData(format)

		//	Dim pathFileNameCollection As StringCollection = GetFileDropList()
		//	'If Me.pathFileNameIndex = pathFileNameCollection.Count Then
		//	'	Me.theCleanupAction.Invoke()
		//	'End If

		//	Return result
		//End Function

		//Public Overrides Sub SetData(obj As Object)
		//	MyBase.SetData(obj)
		//End Sub

		//Public Overrides Sub SetData(format As String, autoConvert As Boolean, obj As Object)
		//	MyBase.SetData(format, autoConvert, obj)


		//	If Not Me.theSetDataFunctionHasBeenCalledOnce Then
		//		Me.theSetDataFunctionHasBeenCalledOnce = True
		//	Else
		//		Me.theCleanupAction.Invoke()
		//	End If
		//End Sub

		//Public Overrides Sub SetData(format As String, obj As Object)
		//	MyBase.SetData(format, obj)
		//End Sub

		//Public Overrides Sub SetData(format As Type, obj As Object)
		//	MyBase.SetData(format, obj)
		//End Sub

		public delegate void StartupDelegate();
		public delegate void CleanupDelegate();

		private int pathFileNameIndex;
		private StartupDelegate theStartupAction;
		private CleanupDelegate theCleanupAction;
		private bool theFunctionHasBeenCalledOnce;
		private bool theSetDataFunctionHasBeenCalledOnce;

	}

}