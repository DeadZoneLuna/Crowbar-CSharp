using System;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
	{
		public int Compare(TKey x, TKey y)
		{
			int result = x.CompareTo(y);
			return result == 0 ? 1 : result;
		}
	}
}