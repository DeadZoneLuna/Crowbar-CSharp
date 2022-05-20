//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;

namespace Crowbar
{
	public class FolderAndFileListViewItemComparer : IComparer
	{
		//Public Sub New()
		//	col = 0
		//End Sub

		public FolderAndFileListViewItemComparer(int column, SortOrder order)
		{
			this.col = column;
			this.order = order;
		}

		public int Compare(object x, object y)
		{
			int returnVal = -1;
			ListViewItem xItem = null;
			ListViewItem yItem = null;

			xItem = (ListViewItem)x;
			yItem = (ListViewItem)y;

			//NOTE: Must use "And" so that both expressions are evaluated.
			// The SubItems(4) means "Extension" column; using the 4 here because already using several item.SubItems.Add() that are dependent on order of columns anyway.
			if (xItem.SubItems[4].Text == "<Folder>" && yItem.SubItems[4].Text != "<Folder>")
			{
				returnVal = -1;
			}
			else if (xItem.SubItems[4].Text != "<Folder>" && yItem.SubItems[4].Text == "<Folder>")
			{
				returnVal = 1;
			}
			else
			{
				if (this.col == 1 && xItem.SubItems[4].Text != "<Folder>")
				{
					if (Int32.Parse(xItem.SubItems[this.col].Text, NumberStyles.Integer | NumberStyles.AllowThousands, MainCROWBAR.TheApp.InternalCultureInfo) < Int32.Parse(yItem.SubItems[this.col].Text, NumberStyles.Integer | NumberStyles.AllowThousands, MainCROWBAR.TheApp.InternalCultureInfo))
					{
						returnVal = -1;
					}
					else
					{
						returnVal = 1;
					}
				}
				else
				{
					returnVal = string.Compare(xItem.SubItems[this.col].Text, yItem.SubItems[this.col].Text);
				}
				if (this.order == SortOrder.Descending)
				{
					returnVal *= -1;
				}
			}

			return returnVal;
		}

		private int col;
		private SortOrder order;

	}

}