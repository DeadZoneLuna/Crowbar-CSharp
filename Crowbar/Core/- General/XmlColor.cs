using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;

// This class, along with XmlElement(Type) tag, allows XML serializing/deserializing 
//     of the System.Drawing.Color object into a readable color name.
//     Without this class, would have to use two properties instead of the one.
//
// Example usage:
//
//<XmlElement(Type:=GetType(XmlColor))>
//Public Property SomeColor() As Color
//	Get
//		Return Me.theSomeColor
//	End Get
//	Set(ByVal value As Color)
//		Me.theSomeColor = value
//	End Set
//End Property

namespace Crowbar
{
	public class XmlColor
	{
		private Color theColor;

		public XmlColor()
		{
		}

		public XmlColor(Color color)
		{
			theColor = color;
		}

		[XmlText()]
		public string ColorName
		{
			get
			{
				return ColorTranslator.ToHtml(theColor);
			}
			set
			{
				theColor = ColorTranslator.FromHtml(value);
			}
		}

		[XmlIgnore()]
		public int Argb
		{
			get
			{
				return theColor.ToArgb();
			}
			set
			{
				theColor = Color.FromArgb(value);
			}
		}

		public static implicit operator Color(XmlColor cw)
		{
			return cw.theColor;
		}

		public static explicit operator XmlColor(Color b)
		{
			return new XmlColor(b);
		}

	}

}