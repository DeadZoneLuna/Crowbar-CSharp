using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Crowbar
{
	internal class PropertyComparer<TKey> : Comparer<TKey>
	{
		private PropertyDescriptor _property;
		private ListSortDirection _direction;

		public PropertyComparer(PropertyDescriptor nProperty, ListSortDirection direction) : base()
		{
			_property = nProperty;
			_direction = direction;
		}

		public override int Compare(TKey xVal, TKey yVal)
		{
			object xValue = GetPropertyValue(xVal, _property.Name);
			object yValue = GetPropertyValue(yVal, _property.Name);
			if (_direction == ListSortDirection.Ascending)
				return CompareAscending(xValue, yValue);

			return CompareDescending(xValue, yValue);
		}

		public new bool Equals(TKey xVal, TKey yVal)
		{
			return xVal.Equals(yVal);
		}

		public new int GetHashCode(TKey obj)
		{
			return obj.GetHashCode();
		}

		private int CompareAscending(object xValue, object yValue)
		{
			if (xValue == null)
				return yValue == null ? 0 : -1;

			return yValue == null ? 1 : ((IComparable)xValue).CompareTo(yValue);
		}

		private int CompareDescending(object xValue, object yValue)
		{
			return (CompareAscending(xValue, yValue) * -1);
		}

		private object GetPropertyValue(TKey value, string nProperty)
		{
			return value.GetType().GetProperty(nProperty).GetValue(value, null);
		}
	}
}