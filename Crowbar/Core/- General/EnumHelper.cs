using System;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	public sealed class EnumHelper
	{
		public static string GetDescription(Enum value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			string description = value.ToString();
			System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(description);
			System.ComponentModel.DescriptionAttribute[] attributes = (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
			if (attributes != null && attributes.Length > 0)
				description = attributes[0].Description;

			return description;
		}

		public static IList ToList(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			Array enumValues = Enum.GetValues(type);
			List<KeyValuePair<Enum, string>> list = new List<KeyValuePair<Enum, string>>();
			foreach (Enum value in enumValues)
				list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));

			return list;
		}

		public static void InsertIntoList(int index, Enum value, ref IList list)
		{
			list.Insert(index, new KeyValuePair<Enum, string>(value, GetDescription(value)));
		}

		public static void RemoveFromList(Enum value, ref IList list)
		{
			list.Remove(new KeyValuePair<Enum, string>(value, GetDescription(value)));
		}

		public static bool Contains(Enum value, IList list)
		{
			return list.Contains(new KeyValuePair<Enum, string>(value, GetDescription(value)));
		}

		public static Enum FindKeyFromDescription(string description, IList list)
		{
			foreach (KeyValuePair<Enum, string> pair in list)
			{
				if (pair.Value == description)
					return pair.Key;
			}

			return null;
		}

		public static int IndexOf(Enum key, IList list)
		{
			return list.IndexOf(new KeyValuePair<Enum, string>(key, GetDescription(key)));
		}

		public static int IndexOfKeyAsString(string keyText, IList list)
		{
			for (int pairIndex = 0; pairIndex < list.Count; pairIndex++)
			{
				KeyValuePair<Enum, string> pair = (KeyValuePair<Enum, string>)list[pairIndex];
				if (pair.Key.ToString() == keyText)
					return pairIndex;
			}

			return -1;
		}

		public static int IndexOfKeyAsCaseInsensitiveString(string keyText, IList list)
		{
			for (int pairIndex = 0; pairIndex < list.Count; pairIndex++)
			{
				KeyValuePair<Enum, string> pair = (KeyValuePair<Enum, string>)list[pairIndex];
				if (pair.Key.ToString().ToLower() == keyText.ToLower())
					return pairIndex;
			}

			return -1;
		}

		public static int IndexOf(string description, IList list)
		{
			Enum key = FindKeyFromDescription(description, list);
			if (key != null)
				return list.IndexOf(new KeyValuePair<Enum, string>(key, GetDescription(key)));

			return -1;
		}

		public static Enum Key(int index, IList list)
		{
			return ((KeyValuePair<Enum, string>)list[index]).Key;
		}

		public static string Value(int index, IList list)
		{
			return ((KeyValuePair<Enum, string>)list[index]).Value;
		}
	}
}