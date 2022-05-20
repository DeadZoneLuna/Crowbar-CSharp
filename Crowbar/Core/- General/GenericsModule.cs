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
	internal static class GenericsModule
	{

		public static bool ListsAreEqual(List<double> list1, List<double> list2)
		{
			bool theListsAreEqual = true;

			if (list1.Count != list2.Count)
			{
				theListsAreEqual = false;
			}
			else
			{
				for (int i = 0; i < list1.Count; i++)
				{
					if (list1[i] != list2[i])
					{
						theListsAreEqual = false;
						break;
					}
				}
			}

			return theListsAreEqual;
		}

	}

}