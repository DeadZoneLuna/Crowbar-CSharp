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
	public sealed class EnumHelper
	{

		public static string GetDescription(System.Enum value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string description = value.ToString();
			System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(description);
			System.ComponentModel.DescriptionAttribute[] attributes = (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
			if (attributes != null && attributes.Length > 0)
			{
				description = attributes[0].Description;
			}
			return description;
		}

		public static IList ToList(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			//Dim list As ArrayList = New ArrayList()
			List<KeyValuePair<System.Enum, string>> list = new List<KeyValuePair<System.Enum, string>>();
			Array enumValues = System.Enum.GetValues(type);
			foreach (System.Enum value in enumValues)
			{
				list.Add(new KeyValuePair<System.Enum, string>(value, GetDescription(value)));
			}
			return list;
		}

		public static void InsertIntoList(int index, System.Enum value, ref IList list)
		{
			list.Insert(index, new KeyValuePair<System.Enum, string>(value, GetDescription(value)));
		}

		public static void RemoveFromList(System.Enum value, ref IList list)
		{
			list.Remove(new KeyValuePair<System.Enum, string>(value, GetDescription(value)));
		}

		public static bool Contains(System.Enum value, IList list)
		{
			return list.Contains(new KeyValuePair<System.Enum, string>(value, GetDescription(value)));
		}

		public static System.Enum FindKeyFromDescription(string description, IList list)
		{
			System.Enum key = null;
			foreach (KeyValuePair<System.Enum, string> pair in list)
			{
				if (pair.Value == description)
				{
					key = pair.Key;
					break;
				}
			}
			return key;
		}

		public static int IndexOf(System.Enum key, IList list)
		{
			return list.IndexOf(new KeyValuePair<System.Enum, string>(key, GetDescription(key)));
		}

		public static int IndexOfKeyAsString(string keyText, IList list)
		{
			int index = -1;
			for (int pairIndex = 0; pairIndex < list.Count; pairIndex++)
			{
				KeyValuePair<System.Enum, string> pair = (KeyValuePair<System.Enum, string>)list[pairIndex];
				if (pair.Key.ToString() == keyText)
				{
					index = pairIndex;
					break;
				}
			}
			return index;
		}

		public static int IndexOfKeyAsCaseInsensitiveString(string keyText, IList list)
		{
			int index = -1;
			for (int pairIndex = 0; pairIndex < list.Count; pairIndex++)
			{
				KeyValuePair<System.Enum, string> pair = (KeyValuePair<System.Enum, string>)list[pairIndex];
				if (pair.Key.ToString().ToLower() == keyText.ToLower())
				{
					index = pairIndex;
					break;
				}
			}
			return index;
		}

		public static int IndexOf(string description, IList list)
		{
			int index = -1;
			System.Enum key = FindKeyFromDescription(description, list);
			if (key != null)
			{
				index = list.IndexOf(new KeyValuePair<System.Enum, string>(key, GetDescription(key)));
			}
			return index;
		}

		public static System.Enum Key(int index, IList list)
		{
			KeyValuePair<System.Enum, string> pair = (KeyValuePair<System.Enum, string>)list[index];
			return pair.Key;
		}

		public static string Value(int index, IList list)
		{
			KeyValuePair<System.Enum, string> pair = (KeyValuePair<System.Enum, string>)list[index];
			return pair.Value;
		}

	}

}