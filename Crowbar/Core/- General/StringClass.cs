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
	public class StringClass
	{

		public static string ConvertFromNullTerminatedOrFullLengthString(string input)
		{
			string output = null;
			int positionOfFirstNullChar = input.IndexOf('\0');
			if (positionOfFirstNullChar == -1)
			{
				output = input;
			}
			else
			{
				output = input.Substring(0, positionOfFirstNullChar);
			}
			return output;
		}

		public static string RemoveUptoAndIncludingFirstDotCharacterFromString(string input)
		{
			string output = null;
			int positionOfFirstDotChar = input.IndexOf(".");
			if (positionOfFirstDotChar >= 0)
			{
				output = input.Substring(positionOfFirstDotChar + 1);
			}
			else
			{
				output = input;
			}
			return output;
		}

	}

}