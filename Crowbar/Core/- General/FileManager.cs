using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Crowbar
{
	public class FileManager
	{
		#region Read Methods
		public static bool FilePathHasInvalidChars(string path)
		{
			if (string.IsNullOrEmpty(path))
				return true;

			try
			{
				string fileName = Path.GetFileName(path);
				string fileDirectory = Path.GetDirectoryName(path);
			}
			catch (ArgumentException)
			{
			}

			bool ret = (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0);
			if (ret == false)
				ret = (path.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0);

			return ret;
		}

		public static string ReadNullTerminatedString(BinaryReader inputFileReader)
		{
			StringBuilder text = new StringBuilder();
			while (inputFileReader.PeekChar() > 0)
			{
				text.Append(inputFileReader.ReadChar());
			}
			// Read the null character.
			inputFileReader.ReadChar();
			return text.ToString();
		}

		static char[] m_delimitersKVLine = { '\"' };
		public static bool ReadKeyValueLine(BinaryReader inputFileReader, ref string oKey, ref string oValue)
		{
			string line = ReadTextLine(inputFileReader);
			if (line != null)
			{
				string[] tokens = line.Split(m_delimitersKVLine, StringSplitOptions.RemoveEmptyEntries);
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

		static char[] m_delimitersKVToken = { '\"', '{', '}' };
		public static string ReadKeyValueToken(ref string buffer)
		{
			do
			{
				buffer = buffer.TrimStart();
				if (!buffer.StartsWith("/"))
					break;

				int pos = buffer.IndexOf((char)0xA, 1);
				if (pos > -1)
					buffer = buffer.Substring(pos + 1);
				else
				{
					buffer = string.Empty;
					break;
				}
			} while (!string.IsNullOrEmpty(buffer));

			if (string.IsNullOrEmpty(buffer))
				return string.Empty;

			string token = string.Empty;
			if (buffer.StartsWith("\""))
			{
				int pos = buffer.IndexOf("\"", 1);
				if (pos > -1)
				{
					//NOTE: Remove the double-quotes.
					token = buffer.Substring(1, pos - 1);
					buffer = buffer.Substring(pos + 1);
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
				string[] tokens = buffer.Split(m_delimitersKVToken);
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
			try
			{
				while (true)
				{
					char aChar = inputFileReader.ReadChar();
					if (aChar == '\0' || aChar == (char)0xA)
						break;

					line.Append(aChar);
				}
			}
			catch (Exception)
			{
				int debug = 4242;
			}

			if (line.Length > 0)
				return line.ToString();

			return null;
		}
		#endregion

		#region Path
		public static string GetPathFileNameWithoutExtension(string pathFileName)
		{
			try
			{
				return Path.Combine(GetPath(pathFileName), Path.GetFileNameWithoutExtension(pathFileName));
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		//NOTE: Replacement for Path.GetDirectoryName, because GetDirectoryName returns "Nothing" when something like "C:\" is the path.
		public static string GetPath(string pathFileName)
		{
			try
			{
				pathFileName = GetNormalizedPathFileName(pathFileName);

				int length = pathFileName.LastIndexOf(Path.DirectorySeparatorChar);
				if (length < 1)
					pathFileName = string.Empty;
				else if (length > 0)
					pathFileName = pathFileName.Substring(0, length);
				if (pathFileName.Length == 2 && pathFileName[1] == ':')
					pathFileName += Path.DirectorySeparatorChar.ToString();

				return pathFileName;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		public static void CreatePath(string path)
		{
			try
			{
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool PathExistsAfterTryToCreate(string aPath)
		{
			if (Directory.Exists(aPath))
				return true;

			try
			{
				Directory.CreateDirectory(aPath);
			}
			catch (Exception)
			{
			}

			return Directory.Exists(aPath);
		}

		private static string GetFullPath(string maybeRelativePath, string baseDirectory)
		{
			if (baseDirectory == null)
				baseDirectory = Environment.CurrentDirectory;

			string root = Path.GetPathRoot(maybeRelativePath);
			if (string.IsNullOrEmpty(root))
				return Path.GetFullPath(Path.Combine(baseDirectory, maybeRelativePath));
			else if (root == "\\")
				return Path.GetFullPath(Path.Combine(Path.GetPathRoot(baseDirectory), maybeRelativePath.Remove(0, 1)));

			return maybeRelativePath;
		}

		public static string GetRelativePathFileName(string fromPath, string toPath)
		{
			if (string.IsNullOrEmpty(fromPath))
				return toPath;

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

			Uri path1 = new Uri(Path.GetFullPath(fromPath) + Path.DirectorySeparatorChar);
			Uri path2 = new Uri(Path.GetFullPath(toPath) + Path.DirectorySeparatorChar);
			Uri diff = path1.MakeRelativeUri(path2);
			// Convert Uri escaped characters and convert Uri forward slash to default directory separator.
			string newPathFileName = Uri.UnescapeDataString(diff.OriginalString).Replace("/", Path.DirectorySeparatorChar.ToString());

			string cleanedPath = newPathFileName.ToString();
			if (cleanedPath.StartsWith("." + Path.DirectorySeparatorChar))
				cleanedPath = cleanedPath.Remove(0, 2);

			//NOTE: Remove the ending path separator that is there because of modified inputs to MakeRelativeUri() earlier.
			return cleanedPath.TrimEnd(Path.DirectorySeparatorChar);
		}

		public static string GetCleanPath(string givenPath, bool returnFullPath)
		{
			string cleanPath = givenPath;
			foreach (char invalidChar in Path.GetInvalidPathChars())
				cleanPath = cleanPath.Replace(invalidChar.ToString(), "");

			if (returnFullPath)
			{
				try
				{
					cleanPath = Path.GetFullPath(cleanPath);
				}
				catch (Exception)
				{
					cleanPath = cleanPath.Replace(":", "");
				}
			}

			return cleanPath;
		}

		public static string GetCleanPathFileName(string givenPathFileName, bool returnFullPathFileName)
		{
			string cleanedPathGivenPathFileName = givenPathFileName;
			foreach (char invalidChar in Path.GetInvalidPathChars())
				cleanedPathGivenPathFileName = cleanedPathGivenPathFileName.Replace(invalidChar.ToString(), "");

			if (returnFullPathFileName)
			{
				try
				{
					cleanedPathGivenPathFileName = Path.GetFullPath(cleanedPathGivenPathFileName);
				}
				catch (Exception)
				{
					cleanedPathGivenPathFileName = cleanedPathGivenPathFileName.Replace(":", "");
				}
			}

			string cleanedGivenFileName = Path.GetFileName(cleanedPathGivenPathFileName);
			foreach (char invalidChar in Path.GetInvalidFileNameChars())
				cleanedGivenFileName = cleanedGivenFileName.Replace(invalidChar.ToString(), "");

			if (string.IsNullOrEmpty(cleanedGivenFileName))
				return cleanedPathGivenPathFileName;

			return Path.Combine(GetPath(cleanedPathGivenPathFileName), cleanedGivenFileName);
		}

		public static void ParsePath(object sender, ConvertEventArgs e)
		{
			if (e.DesiredType != typeof(string))
				return;

			if (!string.IsNullOrEmpty(e.Value == null ? null : Convert.ToString(e.Value)))
				e.Value = GetCleanPath(e.Value == null ? null : Convert.ToString(e.Value), true);
		}

		public static void ParsePathFileName(object sender, ConvertEventArgs e)
		{
			if (e.DesiredType != typeof(string))
				return;

			if (!string.IsNullOrEmpty(e.Value == null ? null : Convert.ToString(e.Value)))
				e.Value = GetCleanPathFileName(e.Value == null ? null : Convert.ToString(e.Value), true);
		}

		public static string GetNormalizedPathFileName(string givenPathFileName)
		{
			if (Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar)
				return givenPathFileName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

			return givenPathFileName;
		}

		public static string GetTestedPathFileName(string iPathFileName)
		{
			string testedPathFileName = iPathFileName;
			string pathFileNameWithoutExtension = GetPathFileNameWithoutExtension(iPathFileName);
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
			string tempVar = string.Empty;
			return GetLongestExtantPath(iPath, ref tempVar);
		}

		public static string GetLongestExtantPath(string iPath, ref string topNonextantPath)
		{
			if (!string.IsNullOrEmpty(iPath) && !Directory.Exists(iPath))
			{
				topNonextantPath = iPath;
				string shorterPath = GetPath(iPath);
				//NOTE: This "If" handles cases such as iPath = "F:\" when "F" is not a valid drive.
				if (shorterPath == iPath)
					iPath = string.Empty;
				else
					iPath = GetLongestExtantPath(shorterPath, ref topNonextantPath);
			}

			return iPath;
		}

		// Example: "C:\folder\subfolder\temp" returns "C:\folder"
		// Example: "subfolder\temp"           returns "subfolder"
		// Example: "temp"                     returns ""
		public static string GetTopFolderPath(string iPathFileName)
		{
			if (string.IsNullOrEmpty(GetPath(iPathFileName)))
				return string.Empty;

			iPathFileName = GetNormalizedPathFileName(iPathFileName);
			string[] splitPathArray = iPathFileName.Split(Path.DirectorySeparatorChar);
			if (iPathFileName != Path.GetFullPath(iPathFileName))
				return splitPathArray[0];

			//NOTE: Path.Combine does not put in the DirectorySeparatorChar, so combine directly.
			return splitPathArray[0] + Path.DirectorySeparatorChar + splitPathArray[1];
		}

		// Delete the path if all recursive subfolders are empty.
		// Example: "C:\folder\subfolder\temp" where temp contains "subtemp\subsubtemp".
		// Returns the top-most folder path that was deleted.
		public static string DeleteEmptySubpath(string fullPath)
		{
			string fullPathDeleted = string.Empty;
			if (!string.IsNullOrEmpty(fullPath))
			{
				try
				{
					foreach (string aFullPath in Directory.EnumerateDirectories(fullPath))
						fullPathDeleted = DeleteEmptySubpath(aFullPath);

					string[] entries = Directory.GetFileSystemEntries(fullPath);
					if (entries.Length == 0)
					{
						Directory.Delete(fullPath);
						fullPathDeleted = fullPath;
					}
				}
				catch (Exception)
				{
					int debug = 4242;
				}
			}

			return fullPathDeleted;
		}

		private static int GetPathAttribute(string path)
		{
			DirectoryInfo di = new DirectoryInfo(path);
			if (di.Exists)
				return FILE_ATTRIBUTE_DIRECTORY;

			FileInfo fi = new FileInfo(path);
			if (fi.Exists)
				return FILE_ATTRIBUTE_NORMAL;

			throw new FileNotFoundException();
		}

		private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
		private const int FILE_ATTRIBUTE_NORMAL = 0x80;

		//Unused?
		//[DllImport("shlwapi.dll", SetLastError = true)]
		//private extern static int PathRelativePathTo(StringBuilder pszPath, string pszFrom, int dwAttrFrom, string pszTo, int dwAttrTo);
		#endregion

		#region Folder
		public static void CopyFolder(string source, string destination, bool overwrite)
		{
			// Create the destination folder if missing.
			if (!Directory.Exists(destination))
				Directory.CreateDirectory(destination);

			DirectoryInfo dirInfo = new DirectoryInfo(source);

			// Copy all files.
			foreach (FileInfo fileInfo in dirInfo.GetFiles())
				fileInfo.CopyTo(Path.Combine(destination, fileInfo.Name), overwrite);

			// Recursively copy all sub-directories.
			foreach (DirectoryInfo subDirectoryInfo in dirInfo.GetDirectories())
				CopyFolder(subDirectoryInfo.FullName, Path.Combine(destination, subDirectoryInfo.Name), overwrite);
		}

		public static ulong GetFolderSize(string aFolder)
		{
			ulong size = 0;
			DirectoryInfo FolderInfo = new DirectoryInfo(aFolder);
			foreach (FileInfo File in FolderInfo.GetFiles())
				size += (ulong)File.Length;
			foreach (DirectoryInfo SubFolderInfo in FolderInfo.GetDirectories())
				size += GetFolderSize(SubFolderInfo.FullName);

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
					catch (Exception)
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
		//TODO: Replace it with a cross-platform solution
		public static void OpenWindowsExplorer(string pathFileName)
		{
			if (File.Exists(pathFileName))
				Process.Start("explorer.exe", "/select,\"" + pathFileName + "\"");
			else if (Directory.Exists(pathFileName))
				Process.Start("explorer.exe", "/e,\"" + pathFileName + "\"");
			else
			{
				string shorterPathFileName = GetPath(pathFileName);
				if (!string.IsNullOrWhiteSpace(shorterPathFileName))
					OpenWindowsExplorer(shorterPathFileName);
			}
		}
		#endregion
	}
}