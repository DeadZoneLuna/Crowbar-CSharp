using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	public class AccessedBytesDebugFile
	{
		#region Data
		private StreamWriter theOutputFileStreamWriter;
		#endregion

		#region Creation and Destruction
		public AccessedBytesDebugFile(StreamWriter outputFileStream)
		{
			theOutputFileStreamWriter = outputFileStream;
		}
		#endregion

		#region Methods
		public void WriteHeaderComment()
		{
			theOutputFileStreamWriter.WriteLine("// " + MainCROWBAR.TheApp.GetHeaderComment());
		}

		public void WriteFileSeekLog(FileSeekLog aFileSeekLog)
		{
			WriteLogLine(0, "====== File Size ======");
			WriteLogLine(1, aFileSeekLog.theFileSize.ToString("N0"));
			WriteLogLine(0, "====== File Seek Log ======");
			for (int i = 0; i < aFileSeekLog.theFileSeekList.Count; i++)
			{
				long offsetStart = aFileSeekLog.theFileSeekList.Keys[i];
				long offsetEnd = aFileSeekLog.theFileSeekList.Values[i];
				WriteLogLine(1, offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0") + " " + aFileSeekLog.theFileSeekDescriptionList.Values[i]);
			}
			WriteLogLine(0, "========================");
		}
		#endregion

		#region Private Methods
		private void WriteFileSeparatorLines()
		{
			WriteLogLine(0, "");
			WriteLogLine(0, "");
			WriteLogLine(0, "################################################################################");
			WriteLogLine(0, "");
			WriteLogLine(0, "");
		}

		private void WriteLogLine(int indentLevel, string line)
		{
			string indentedLine = string.Empty;
			for (int i = 1; i <= indentLevel; i++)
			{
				indentedLine += "\t";
			}
			indentedLine += line;
			theOutputFileStreamWriter.WriteLine(indentedLine);
			theOutputFileStreamWriter.Flush();
		}
		#endregion
	}
}