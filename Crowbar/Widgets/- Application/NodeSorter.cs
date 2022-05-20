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
	public class NodeSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			TreeNode tx = (TreeNode)x;
			TreeNode ty = (TreeNode)y;

			if (tx.Text.StartsWith("<") && ty.Text.StartsWith("<"))
			{
				return string.Compare(tx.Text, ty.Text);
			}
			else if (tx.Text.StartsWith("<"))
			{
				return 1;
			}
			else if (ty.Text.StartsWith("<"))
			{
				return -1;
			}
			else
			{
				return string.Compare(tx.Text, ty.Text);
			}

		}

	}

}