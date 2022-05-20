//----------------------------------------------------------------------------------------
//	Copyright © 2003 - 2022 Tangible Software Solutions, Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	The methods in this class replicate the behavior of miscellaneous VB features.
//----------------------------------------------------------------------------------------
using System;

public static class ConversionHelper
{
	public static void MidStatement(ref string target, int oneBasedStart, char insert)
	{
		//These 'MidStatement' method overloads replicate the behavior of the VB 'Mid' statement (which is unrelated to the VB 'Mid' function)

		if (target == null)
			return;

		target = target.Remove(oneBasedStart - 1, 1).Insert(oneBasedStart - 1, insert.ToString());
	}

	public static void MidStatement(ref string target, int oneBasedStart, string insert)
	{
		//These 'MidStatement' method overloads replicate the behavior of the VB 'Mid' statement (which is unrelated to the VB 'Mid' function)

		if (target == null || insert == null)
			return;

		target = target.PadRight(target.Length + insert.Length).Remove(oneBasedStart - 1, insert.Length).Insert(oneBasedStart - 1, insert).Substring(0, target.Length);
	}

	public static void MidStatement(ref string target, int oneBasedStart, string insert, int length)
	{
		//These 'MidStatement' method overloads replicate the behavior of the VB 'Mid' statement (which is unrelated to the VB 'Mid' function)

		if (target == null || insert == null)
			return;

		int minLength = Math.Min(insert.Length, length);
		target = target.PadRight(target.Length + insert.Length).Remove(oneBasedStart - 1, minLength).Insert(oneBasedStart - 1, insert.Substring(0, minLength)).Substring(0, target.Length);
	}

	public static string StrReverse(string expression)
	{
		if (string.IsNullOrEmpty(expression))
			return string.Empty;

		System.Text.StringBuilder reversedString = new System.Text.StringBuilder(expression.Length);
		for (int charIndex = expression.Length - 1; charIndex >= 0; charIndex--)
		{
			reversedString.Append(expression[charIndex]);
		}
		return reversedString.ToString();
	}

	public static string RSet(string source, int length)
	{
		if (source == null)
			return string.Empty.PadLeft(length);
		else if (length < source.Length)
			return source.Substring(0, length);
		else
			return source.PadLeft(length);
	}

	public static string LSet(string source, int length)
	{
		if (source == null)
			return string.Empty.PadRight(length);
		else if (length < source.Length)
			return source.Substring(0, length);
		else
			return source.PadRight(length);
	}

	public static System.Collections.ObjectModel.ReadOnlyCollection<string> CommandLineArgs
	{
		get
		{
			var args = Environment.GetCommandLineArgs();
			string[] argsWithoutProgramName = new string[args.Length - 1];
			for (int arg = 1; arg < args.Length; arg++)
			{
				argsWithoutProgramName[arg - 1] = args[arg];
			}
			return Array.AsReadOnly(argsWithoutProgramName);
		}
	}
}