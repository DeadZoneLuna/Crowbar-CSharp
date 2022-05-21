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
	public class BindingListExAutoSort<T> : BindingListEx<T>
	{
		public BindingListExAutoSort(string nProperty)
		{
			theSortedPropertyName = nProperty;
			theSortedProperty = FindPropertyDescriptor(nProperty);
		}

		protected override void InsertItem(int index, T item)
		{
			base.InsertItemSorted(index, item, theSortedProperty);
		}

		//Public Overloads Sub ResetItem(ByVal index As Integer)
		//	MyBase.ResetItem(index)
		//End Sub

		protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
		{
			//If e.ListChangedType = ListChangedType.ItemChanged AndAlso e.PropertyDescriptor IsNot Nothing AndAlso e.PropertyDescriptor.Name = Me.theSortedPropertyName Then
			//	Dim obj As Object = Me.Items(e.NewIndex)
			//	MyBase.ApplySortCore(Me.theSortedProperty, ListSortDirection.Ascending)
			//	Dim aEventArgs As New ListChangedEventArgs(ListChangedType.ItemMoved, Me.IndexOf(CType(obj, T)), e.NewIndex)
			//	MyBase.OnListChanged(aEventArgs)
			//Else
			//	MyBase.OnListChanged(e)
			//End If
			//======
			//NOTE: Raise an extra new event, ItemMoved, so that widgets can know when an item moved because of auto-sorting.
			if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == theSortedPropertyName)
			{
				object obj = Items[e.NewIndex];
				Items.RemoveAt(e.NewIndex);
//INSTANT C# WARNING: Casting to a generic type parameter may result in a runtime exception:
//ORIGINAL LINE: Dim insertionIndex As Integer
				int insertionIndex = FindInsertionIndex(0, (T)obj, theSortedProperty);
//INSTANT C# WARNING: Casting to a generic type parameter may result in a runtime exception:
//ORIGINAL LINE: Me.Items.Insert(insertionIndex, CType(obj, T))
				Items.Insert(insertionIndex, (T)obj);
				ListChangedEventArgs aEventArgs = new ListChangedEventArgs(ListChangedType.ItemMoved, insertionIndex, e.NewIndex);
				base.OnListChanged(aEventArgs);
				//Else
				//	MyBase.OnListChanged(e)
			}
			base.OnListChanged(e);
		}

		private string theSortedPropertyName;
		private PropertyDescriptor theSortedProperty;

	}

}