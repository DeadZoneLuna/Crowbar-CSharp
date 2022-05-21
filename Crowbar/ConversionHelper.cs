//----------------------------------------------------------------------------------------
//	Copyright © 2003 - 2022 Tangible Software Solutions, Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	The methods in this class replicate the behavior of miscellaneous VB features.
//----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Reflection;

public static class ConversionHelper
{
	static string m_assemblyTitle;
	public static string AssemblyTitle
	{
		get
		{
			if (!string.IsNullOrEmpty(m_assemblyTitle))
				return m_assemblyTitle;

			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
			if (attributes.Length == 0)
				return string.Empty;

			return m_assemblyTitle = ((AssemblyTitleAttribute)attributes[0]).Title;
		}
	}

	static string m_assemblyCompany;
	public static string AssemblyCompany
	{
		get
		{
			if (!string.IsNullOrEmpty(m_assemblyCompany))
				return m_assemblyCompany;

			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
			if (attributes.Length == 0)
				return string.Empty;

			return m_assemblyCompany = ((AssemblyCompanyAttribute)attributes[0]).Company;
		}
	}

	static string m_assemblyProduct;
	public static string AssemblyProduct
	{
		get
		{
			if (!string.IsNullOrEmpty(m_assemblyProduct))
				return m_assemblyProduct;

			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			if (attributes.Length == 0)
				return string.Empty;

			return m_assemblyProduct = ((AssemblyProductAttribute)attributes[0]).Product;
		}
	}

	static string m_assemblyCopyright;
	public static string AssemblyCopyright
	{
		get
		{
			if (!string.IsNullOrEmpty(m_assemblyCopyright))
				return m_assemblyCopyright;

			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
			if (attributes.Length == 0)
				return string.Empty;

			return m_assemblyCopyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
		}
	}

	static Version m_appVersion;
	public static Version Version
	{
		get
		{
			if (m_appVersion != null)
				return m_appVersion;

			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
			Console.WriteLine(attributes.Length);
			if (attributes.Length == 0)
				return new Version(0, 0, 0, 0);

			return m_appVersion = new Version(((AssemblyFileVersionAttribute)attributes[0]).Version);
		}
	}

	static string m_appVersionName;
	public static string VersionName
	{
		get
		{
			if (!string.IsNullOrEmpty(m_appVersionName))
				return m_appVersionName;

			return m_appVersionName = Version.ToString(2);
		}
	}

	public static void CopyDirectory(string sourceDirectory, string targetDirectory)
	{
		var diSource = new DirectoryInfo(sourceDirectory);
		var diTarget = new DirectoryInfo(targetDirectory);

		CopyDirectory(diSource, diTarget);
	}

	public static void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
	{
		Directory.CreateDirectory(target.FullName);

		// Copy each file into the new directory.
		foreach (FileInfo fi in source.GetFiles())
		{
			Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
			fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
		}

		// Copy each subdirectory using recursion.
		foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
		{
			DirectoryInfo nextTargetSubDir =
				target.CreateSubdirectory(diSourceSubDir.Name);
			CopyDirectory(diSourceSubDir, nextTargetSubDir);
		}
	}

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