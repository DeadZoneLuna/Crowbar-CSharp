//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;

namespace Crowbar
{
	public class FileManager
	{

#region Read Methods

		public static bool FilePathHasInvalidChars(string path)
		{
			bool ret = false;

			if (string.IsNullOrEmpty(path))
			{
				ret = true;
			}
			else
			{
				try
				{
					string fileName = System.IO.Path.GetFileName(path);
					string fileDirectory = System.IO.Path.GetDirectoryName(path);
				}
				catch (ArgumentException generatedExceptionName)
				{
					// Path functions will throw this 
					// if path contains invalid chars
					ret = true;
				}
				ret = (path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0);
				if (ret == false)
				{
					ret = (path.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0);
				}
			}

			return ret;
		}

		public static string ReadNullTerminatedString(BinaryReader inputFileReader)
		{
			StringBuilder text = new StringBuilder();
			text.Length = 0;
			while (inputFileReader.PeekChar() > 0)
			{
				text.Append(inputFileReader.ReadChar());
			}
			// Read the null character.
			inputFileReader.ReadChar();
			return text.ToString();
		}

		//Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByVal key As String) As String
		//	Dim line As String
		//	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
		//	Dim tokens As String()
		//	line = textFileReader.ReadLine()
		//	If line IsNot Nothing Then
		//		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
		//		If tokens.Length = 2 AndAlso tokens(0) = key Then
		//			Return tokens(1)
		//		End If
		//	End If
		//	Throw New Exception()
		//End Function

		//Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByVal key1 As String, ByVal key2 As String, ByRef oKey As String, ByRef oValue As String) As Boolean
		//	Dim line As String
		//	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
		//	Dim tokens As String()
		//	line = textFileReader.ReadLine()
		//	If line IsNot Nothing Then
		//		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
		//		If tokens.Length = 2 AndAlso (tokens(0) = key1 OrElse tokens(0) = key2) Then
		//			oKey = tokens(0)
		//			oValue = tokens(1)
		//			Return True
		//		End If
		//	End If
		//	Return False
		//End Function

		//Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByRef oKey As String, ByRef oValue As String) As Boolean
		//	Dim line As String
		//	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
		//	Dim tokens As String() = {""}
		//	line = textFileReader.ReadLine()
		//	If line IsNot Nothing Then
		//		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
		//		If tokens.Length = 2 Then
		//			oKey = tokens(0)
		//			oValue = tokens(1)
		//			Return True
		//		ElseIf tokens.Length = 1 Then
		//			oKey = tokens(0)
		//			oValue = tokens(0)
		//			Return False
		//		End If
		//	End If
		//	oKey = line
		//	oValue = line
		//	Return False
		//End Function

		//Public Shared Function ReadKeyValueLine(ByVal inputFileReader As BinaryReader, ByRef oKey As String, ByRef oValue As String) As Boolean
		//	Dim line As String
		//	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
		//	Dim tokens As String() = {""}
		//	line = ReadTextLine(inputFileReader)
		//	If line IsNot Nothing Then
		//		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
		//		If tokens.Length = 2 Then
		//			oKey = tokens(0)
		//			oValue = tokens(1)
		//			Return True
		//		ElseIf tokens.Length = 1 Then
		//			oKey = tokens(0)
		//			oValue = tokens(0)
		//			Return False
		//		End If
		//	End If
		//	oKey = line
		//	oValue = line
		//	Return False
		//End Function

		public static bool ReadKeyValueLine(BinaryReader inputFileReader, ref string oKey, ref string oValue)
		{
			string line = null;
			char[] delimiters = {'\"'};
			string[] tokens = {""};
			line = ReadTextLine(inputFileReader);
			if (line != null)
			{
				tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length == 3)
				{
					oKey = tokens[0];
					oValue = tokens[2];
					return true;
				}
				else if (tokens.Length == 2)
				{
					oKey = tokens[0];
					oValue = tokens[1];
					return true;
				}
				else if (tokens.Length == 1)
				{
					oKey = tokens[0];
					oValue = tokens[0];
					return false;
				}
			}
			oKey = line;
			oValue = line;
			return false;
		}

		//const char *KeyValues::ReadToken( CUtlBuffer &buf, bool &wasQuoted, bool &wasConditional )
		//{
		//	wasQuoted = false;
		//	wasConditional = false;
		//
		//	if ( !buf.IsValid() )
		//		return NULL; 
		//
		//	// eating white spaces and remarks loop
		//	while ( true )
		//	{
		//		buf.EatWhiteSpace();
		//		if ( !buf.IsValid() )
		//			return NULL;	// file ends after reading whitespaces
		//
		//		// stop if it's not a comment; a new token starts here
		//		if ( !buf.EatCPPComment() )
		//			break;
		//	}
		//
		//	const char *c = (const char*)buf.PeekGet( sizeof(char), 0 );
		//	if ( !c )
		//		return NULL;
		//
		//	// read quoted strings specially
		//	if ( *c == '\"' )
		//	{
		//		wasQuoted = true;
		//		buf.GetDelimitedString( m_bHasEscapeSequences ? GetCStringCharConversion() : GetNoEscCharConversion(), 
		//			s_pTokenBuf, KEYVALUES_TOKEN_SIZE );
		//		return s_pTokenBuf;
		//	}
		//
		//	if ( *c == '{' || *c == '}' )
		//	{
		//		// it's a control char, just add this one char and stop reading
		//		s_pTokenBuf[0] = *c;
		//		s_pTokenBuf[1] = 0;
		//		buf.SeekGet( CUtlBuffer::SEEK_CURRENT, 1 );
		//		return s_pTokenBuf;
		//	}
		//
		//	// read in the token until we hit a whitespace or a control character
		//	bool bReportedError = false;
		//	bool bConditionalStart = false;
		//	int nCount = 0;
		//	while ( ( c = (const char*)buf.PeekGet( sizeof(char), 0 ) ) )
		//	{
		//		// end of file
		//		if ( *c == 0 )
		//			break;
		//
		//		// break if any control character appears in non quoted tokens
		//		if ( *c == '"' || *c == '{' || *c == '}' )
		//			break;
		//
		//		if ( *c == '[' )
		//			bConditionalStart = true;
		//
		//		if ( *c == ']' && bConditionalStart )
		//		{
		//			wasConditional = true;
		//		}
		//
		//		// break on whitespace
		//		if ( isspace(*c) )
		//			break;
		//
		//		if (nCount < (KEYVALUES_TOKEN_SIZE-1) )
		//		{
		//			s_pTokenBuf[nCount++] = *c;	// add char to buffer
		//		}
		//		else if ( !bReportedError )
		//		{
		//			bReportedError = true;
		//			g_KeyValuesErrorStack.ReportError(" ReadToken overflow" );
		//		}
		//
		//		buf.SeekGet( CUtlBuffer::SEEK_CURRENT, 1 );
		//	}
		//	s_pTokenBuf[ nCount ] = 0;
		//	return s_pTokenBuf;
		//}
		public static string ReadKeyValueToken(ref string buffer)
		{
			string token = null;

			do
			{
				buffer = buffer.TrimStart();
				if (buffer.StartsWith("/"))
				{
					int pos = buffer.IndexOf((char)0xA, 1);
					if (pos > -1)
					{
						buffer = buffer.Substring(pos + 1);
					}
					else
					{
						buffer = "";
						break;
					}
				}
				else
				{
					break;
				}
			} while (!(string.IsNullOrEmpty(buffer)));

			if (string.IsNullOrEmpty(buffer))
			{
				token = "";
			}
			else if (buffer.StartsWith("\""))
			{
				int pos = buffer.IndexOf("\"", 1);
				if (pos > -1)
				{
					//NOTE: Remove the double-quotes.
					token = buffer.Substring(1, pos - 1);
					buffer = buffer.Substring(pos + 1);
				}
				else
				{
					token = "";
				}
			}
			else if (buffer.StartsWith("{") || buffer.StartsWith("}"))
			{
				token = buffer.Substring(0, 1);
				buffer = buffer.Substring(1);
			}
			else
			{
				// Read in the token characters until a control character.
				char[] delimiters = {'\"', '{', '}'};
				string[] tokens = {""};
				tokens = buffer.Split(delimiters);
				token = tokens[0];

				tokens = token.Split();
				token = tokens[0];
				buffer = buffer.Substring(token.Length);
			}

			return token;
		}

		public static string ReadTextLine(BinaryReader inputFileReader)
		{
			StringBuilder line = new StringBuilder();
			char aChar = ' ';
			try
			{
				while (true)
				{
					aChar = inputFileReader.ReadChar();
					if (aChar == '\0' || aChar == (char)0xA)
					{
						break;
					}
					line.Append(aChar);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			if (line.Length > 0)
			{
				return line.ToString();
			}
			return null;
		}

#endregion

#region Path

		public static string GetPathFileNameWithoutExtension(string pathFileName)
		{
			try
			{
				return Path.Combine(FileManager.GetPath(pathFileName), Path.GetFileNameWithoutExtension(pathFileName));
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}

		//NOTE: Replacement for Path.GetDirectoryName, because GetDirectoryName returns "Nothing" when something like "C:\" is the path.
		public static string GetPath(string pathFileName)
		{
			try
			{
				pathFileName = FileManager.GetNormalizedPathFileName(pathFileName);
				int length = pathFileName.LastIndexOf(Path.DirectorySeparatorChar);
				if (length < 1)
				{
					pathFileName = "";
				}
				else if (length > 0)
				{
					pathFileName = pathFileName.Substring(0, length);
				}
				if (pathFileName.Length == 2 && pathFileName[1] == ':')
				{
					pathFileName += Path.DirectorySeparatorChar.ToString();
				}
				return pathFileName;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}

		public static void CreatePath(string path)
		{
			try
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool PathExistsAfterTryToCreate(string aPath)
		{
			if (!Directory.Exists(aPath))
			{
				try
				{
					Directory.CreateDirectory(aPath);
				}
				catch (Exception ex)
				{
				}
			}
			return Directory.Exists(aPath);
		}

		private static string GetFullPath(string maybeRelativePath, string baseDirectory)
		{
			if (baseDirectory == null)
			{
				baseDirectory = Environment.CurrentDirectory;
			}

			string root = Path.GetPathRoot(maybeRelativePath);
			if (string.IsNullOrEmpty(root))
			{
				return Path.GetFullPath(Path.Combine(baseDirectory, maybeRelativePath));
			}
			else if (root == "\\")
			{
				return Path.GetFullPath(Path.Combine(Path.GetPathRoot(baseDirectory), maybeRelativePath.Remove(0, 1)));
			}

			return maybeRelativePath;
		}

		public static string GetRelativePathFileName(string fromPath, string toPath)
		{
			string fromPathAbsolute = null;
			string toPathAbsolute = null;

			if (string.IsNullOrEmpty(fromPath))
			{
				return toPath;
			}

			fromPathAbsolute = Path.GetFullPath(fromPath);
			toPathAbsolute = Path.GetFullPath(toPath);

			//Dim fromAttr As Integer = GetPathAttribute(fromPathAbsolute)
			//Dim toAttr As Integer = GetPathAttribute(toPathAbsolute)

			//IMPORTANT: Use Uri.MakeRelativeUri() instead of PathRelativePathTo(), 
			//      because PathRelativePathTo() does not handle unicode characters properly.
			// MAX_PATH = 260
			//Dim newPathFileName As New StringBuilder(260)
			//If PathRelativePathTo(newPathFileName, fromPathAbsolute, fromAttr, toPathAbsolute, toAttr) = 0 Then
			//	'Throw New ArgumentException("Paths must have a common prefix")
			//	Return toPathAbsolute
			//End If
			//NOTE: Need to add the Path.DirectorySeparatorChar to force MakeRelativeUri() to treat the paths as folder names, not file names.
			//      Otherwise, for example, this happens:
			//      path1 = "C:\temp\Crowbar"
			//      path2 = "C:\temp\Crowbar\addon.txt"
			//      diff  = "Crowbar\addon.txt"
			//      WANT: diff = "addon.txt"
			Uri path1 = new Uri(fromPathAbsolute + Path.DirectorySeparatorChar);
			Uri path2 = new Uri(toPathAbsolute + Path.DirectorySeparatorChar);
			Uri diff = path1.MakeRelativeUri(path2);
			// Convert Uri escaped characters and convert Uri forward slash to default directory separator.
			string newPathFileName = Uri.UnescapeDataString(diff.OriginalString).Replace("/", Path.DirectorySeparatorChar.ToString());

			string cleanedPath = newPathFileName.ToString();
			if (cleanedPath.StartsWith("." + Path.DirectorySeparatorChar))
			{
				cleanedPath = cleanedPath.Remove(0, 2);
			}
			//NOTE: Remove the ending path separator that is there because of modified inputs to MakeRelativeUri() earlier.
			cleanedPath = cleanedPath.TrimEnd(Path.DirectorySeparatorChar);
			return cleanedPath;
		}

		public static string GetCleanPath(string givenPath, bool returnFullPath)
		{
			string cleanPath = givenPath;
			foreach (char invalidChar in Path.GetInvalidPathChars())
			{
				cleanPath = cleanPath.Replace(invalidChar.ToString(), "");
			}
			if (returnFullPath)
			{
				try
				{
					cleanPath = Path.GetFullPath(cleanPath);
				}
				catch (Exception ex)
				{
					cleanPath = cleanPath.Replace(":", "");
				}
			}

			return cleanPath;
		}

		public static string GetCleanPathFileName(string givenPathFileName, bool returnFullPathFileName)
		{
			string cleanPathFileName = null;

			string cleanedPathGivenPathFileName = givenPathFileName;
			foreach (char invalidChar in Path.GetInvalidPathChars())
			{
				cleanedPathGivenPathFileName = cleanedPathGivenPathFileName.Replace(invalidChar.ToString(), "");
			}
			if (returnFullPathFileName)
			{
				try
				{
					cleanedPathGivenPathFileName = Path.GetFullPath(cleanedPathGivenPathFileName);
				}
				catch (Exception ex)
				{
					cleanedPathGivenPathFileName = cleanedPathGivenPathFileName.Replace(":", "");
				}
			}

			string cleanedGivenFileName = Path.GetFileName(cleanedPathGivenPathFileName);
			foreach (char invalidChar in Path.GetInvalidFileNameChars())
			{
				cleanedGivenFileName = cleanedGivenFileName.Replace(invalidChar.ToString(), "");
			}

			string cleanedGivenPath = FileManager.GetPath(cleanedPathGivenPathFileName);

			if (string.IsNullOrEmpty(cleanedGivenFileName))
			{
				cleanPathFileName = cleanedPathGivenPathFileName;
			}
			else
			{
				cleanPathFileName = Path.Combine(cleanedGivenPath, cleanedGivenFileName);
			}

			return cleanPathFileName;
		}

		public static void ParsePath(object sender, ConvertEventArgs e)
		{
			if (e.DesiredType != typeof(string))
			{
				return;
			}
			if (!string.IsNullOrEmpty((e.Value == null ? null : Convert.ToString(e.Value))))
			{
				e.Value = FileManager.GetCleanPath((e.Value == null ? null : Convert.ToString(e.Value)), true);
			}
		}

		public static void ParsePathFileName(object sender, ConvertEventArgs e)
		{
			if (e.DesiredType != typeof(string))
			{
				return;
			}
			if (!string.IsNullOrEmpty((e.Value == null ? null : Convert.ToString(e.Value))))
			{
				e.Value = FileManager.GetCleanPathFileName((e.Value == null ? null : Convert.ToString(e.Value)), true);
			}
		}

		public static string GetNormalizedPathFileName(string givenPathFileName)
		{
			string cleanPathFileName = givenPathFileName;

			if (Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar)
			{
				cleanPathFileName = givenPathFileName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			}

			return cleanPathFileName;
		}

		public static string GetTestedPathFileName(string iPathFileName)
		{
			string testedPathFileName = iPathFileName;
			string pathFileNameWithoutExtension = FileManager.GetPathFileNameWithoutExtension(iPathFileName);
			string extension = Path.GetExtension(iPathFileName);
			int number = 1;
			while (File.Exists(testedPathFileName))
			{
				testedPathFileName = pathFileNameWithoutExtension + "(" + number.ToString() + ")" + extension;
				number += 1;
			}
			return testedPathFileName;
		}

		public static string GetTestedPath(string iPath)
		{
			string testedPathFileName = iPath;
			int number = 1;
			while (Directory.Exists(testedPathFileName))
			{
				testedPathFileName = iPath + "(" + number.ToString() + ")";
				number += 1;
			}
			return testedPathFileName;
		}

		public static string GetLongestExtantPath(string iPath)
		{
			string tempVar = "";
			return GetLongestExtantPath(iPath, ref tempVar);
		}

//INSTANT C# NOTE: Overloaded method(s) are created above to convert the following method having optional parameters:
//ORIGINAL LINE: Public Shared Function GetLongestExtantPath(ByVal iPath As String, Optional ByRef topNonextantPath As String = "") As String
		public static string GetLongestExtantPath(string iPath, ref string topNonextantPath)
		{
			if (!string.IsNullOrEmpty(iPath) && !Directory.Exists(iPath))
			{
				topNonextantPath = iPath;
				string shorterPath = FileManager.GetPath(iPath);
				//NOTE: This "If" handles cases such as iPath = "F:\" when "F" is not a valid drive.
				if (shorterPath == iPath)
				{
					iPath = "";
				}
				else
				{
					iPath = FileManager.GetLongestExtantPath(shorterPath, ref topNonextantPath);
				}
			}
			return iPath;
		}

		// Example: "C:\folder\subfolder\temp" returns "C:\folder"
		// Example: "subfolder\temp"           returns "subfolder"
		// Example: "temp"                     returns ""
		public static string GetTopFolderPath(string iPathFileName)
		{
			string topFolderPath = "";
			string fullPath = null;
			string[] splitPathArray = null;

			if (string.IsNullOrEmpty(FileManager.GetPath(iPathFileName)))
			{
				return "";
			}

			iPathFileName = FileManager.GetNormalizedPathFileName(iPathFileName);
			fullPath = Path.GetFullPath(iPathFileName);
			splitPathArray = iPathFileName.Split(Path.DirectorySeparatorChar);
			if (iPathFileName == fullPath)
			{
				//NOTE: Path.Combine does not put in the DirectorySeparatorChar, so combine directly.
				topFolderPath = splitPathArray[0] + (Path.DirectorySeparatorChar + splitPathArray[1]);
			}
			else
			{
				topFolderPath = splitPathArray[0];
			}

			return topFolderPath;
		}

		// Delete the path if all recursive subfolders are empty.
		// Example: "C:\folder\subfolder\temp" where temp contains "subtemp\subsubtemp".
		// Returns the top-most folder path that was deleted.
		public static string DeleteEmptySubpath(string fullPath)
		{
			string fullPathDeleted = "";

			if (!string.IsNullOrEmpty(fullPath))
			{
				try
				{
					foreach (string aFullPath in Directory.EnumerateDirectories(fullPath))
					{
						fullPathDeleted = FileManager.DeleteEmptySubpath(aFullPath);
					}

					string[] entries = Directory.GetFileSystemEntries(fullPath);
					if (entries.Length == 0)
					{
						Directory.Delete(fullPath);
						fullPathDeleted = fullPath;
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}

			return fullPathDeleted;
		}

		//This is the code that works like GetTempFileName, but instead creates a folder:
		//public string GetTempDirectory() {
		//	string path = Path.GetRandomFileName();
		//	Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), path));
		//	return path;
		//}

		private static int GetPathAttribute(string path)
		{
			DirectoryInfo di = new DirectoryInfo(path);
			if (di.Exists)
			{
				return FILE_ATTRIBUTE_DIRECTORY;
			}

			FileInfo fi = new FileInfo(path);
			if (fi.Exists)
			{
				return FILE_ATTRIBUTE_NORMAL;
			}

			throw new FileNotFoundException();
		}

		private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
		private const int FILE_ATTRIBUTE_NORMAL = 0x80;

		[DllImport("shlwapi.dll", SetLastError=true)]
		private extern static int PathRelativePathTo(StringBuilder pszPath, string pszFrom, int dwAttrFrom, string pszTo, int dwAttrTo);

#endregion

#region Folder

		public static void CopyFolder(string source, string destination, bool overwrite)
		{
			// Create the destination folder if missing.
			if (!Directory.Exists(destination))
			{
				Directory.CreateDirectory(destination);
			}

			DirectoryInfo dirInfo = new DirectoryInfo(source);

			// Copy all files.
			foreach (FileInfo fileInfo in dirInfo.GetFiles())
			{
				fileInfo.CopyTo(Path.Combine(destination, fileInfo.Name), overwrite);
			}

			// Recursively copy all sub-directories.
			foreach (DirectoryInfo subDirectoryInfo in dirInfo.GetDirectories())
			{
				CopyFolder(subDirectoryInfo.FullName, Path.Combine(destination, subDirectoryInfo.Name), overwrite);
			}
		}

		public static ulong GetFolderSize(string aFolder)
		{
			ulong size = 0;
			DirectoryInfo FolderInfo = new System.IO.DirectoryInfo(aFolder);
			foreach (FileInfo File in FolderInfo.GetFiles())
			{
				size += (ulong)File.Length;
			}
			foreach (DirectoryInfo SubFolderInfo in FolderInfo.GetDirectories())
			{
				size += GetFolderSize(SubFolderInfo.FullName);
			}
			return size;
		}

#endregion

#region XML Serialization

		public static object ReadXml(Type theType, string rootElementName, string fileName)
		{
			XmlSerializer x = new XmlSerializer(theType, new XmlRootAttribute(rootElementName));
			return ReadXml(x, fileName);
		}

		public static object ReadXml(Type theType, string fileName)
		{
			XmlSerializer x = new XmlSerializer(theType);

			//Dim objStreamReader As New StreamReader(fileName)
			//Dim iObject As Object = Nothing
			//Try
			//	iObject = x.Deserialize(objStreamReader)
			//Catch
			//	'TODO: Rename the corrupted file.
			//	Throw
			//Finally
			//	objStreamReader.Close()
			//End Try
			//Return iObject
			//======
			return ReadXml(x, fileName);
		}

		public static object ReadXml(XmlSerializer x, string fileName)
		{
			StreamReader objStreamReader = new StreamReader(fileName);
			object iObject = null;
			bool thereWasReadError = false;

			try
			{
				iObject = x.Deserialize(objStreamReader);
			}
			catch
			{
				thereWasReadError = true;
				throw;
			}
			finally
			{
				objStreamReader.Close();

				if (thereWasReadError)
				{
					try
					{
						string newFileName = Path.Combine(FileManager.GetPath(fileName), Path.GetFileNameWithoutExtension(fileName));
						newFileName += "[corrupted]";
						newFileName += Path.GetExtension(fileName);
						File.Move(fileName, newFileName);
					}
					catch (Exception ex)
					{
						//NOTE: Ignore what is likely a File.Move exception, because do not care if it fails.
					}
				}
			}
			return iObject;
		}

		public static void WriteXml(object iObject, string rootElementName, string fileName)
		{
			XmlSerializer x = new XmlSerializer(iObject.GetType(), new XmlRootAttribute(rootElementName));
			WriteXml(iObject, x, fileName);
		}

		public static void WriteXml(object iObject, string fileName)
		{
			XmlSerializer x = new XmlSerializer(iObject.GetType());

			//Dim objStreamWriter As New StreamWriter(fileName)
			//x.Serialize(objStreamWriter, iObject)
			//objStreamWriter.Close()
			//======
			WriteXml(iObject, x, fileName);
		}

		public static void WriteXml(object iObject, XmlSerializer x, string fileName)
		{
			//Dim objStreamWriter As New StreamWriter(fileName)
			//NOTE: Use Xml.XmlWriterSettings to preserve CRLF line endings used by multi-line textboxes.
			System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";
			settings.OmitXmlDeclaration = false;
			settings.NewLineHandling = System.Xml.NewLineHandling.Entitize;
			System.Xml.XmlWriter objStreamWriter = System.Xml.XmlWriter.Create(fileName, settings);
			x.Serialize(objStreamWriter, iObject);
			objStreamWriter.Close();
		}

#endregion

#region Process

		public static void OpenWindowsExplorer(string pathFileName)
		{
			if (File.Exists(pathFileName))
			{
				Process.Start("explorer.exe", "/select,\"" + pathFileName + "\"");
			}
			else if (Directory.Exists(pathFileName))
			{
				Process.Start("explorer.exe", "/e,\"" + pathFileName + "\"");
			}
			else
			{
				string shorterPathFileName = FileManager.GetPath(pathFileName);
				if (!string.IsNullOrWhiteSpace(shorterPathFileName))
				{
					FileManager.OpenWindowsExplorer(shorterPathFileName);
				}
			}
		}

#endregion

	}

}