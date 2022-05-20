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
	public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey: IComparable
	{
		public int Compare(TKey x, TKey y)
		{
			int result = x.CompareTo(y);

			if (result == 0)
			{
				return 1;
			}
			else
			{
				// Handle equality as being greater
				return result;
			}
		}

	}

}