using System;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	internal static class GenericsModule
	{
		public static bool ListsAreEqual(List<double> list1, List<double> list2)
		{
			if (list1.Count != list2.Count)
				return false;

			for (int i = 0; i < list1.Count; i++)
			{
				if (list1[i] != list2[i])
					return false;
			}

			return true;
		}
	}
}