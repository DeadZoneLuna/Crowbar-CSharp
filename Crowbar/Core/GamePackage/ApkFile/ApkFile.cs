//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;
using System.Text;

namespace Crowbar
{
	public class ApkFile : BasePackageFile
	{
#region Creation and Destruction

		public ApkFile(BinaryReader packageDirectoryFileReader, BinaryReader packageFileReader, ApkFileData apkFileData)
		{
			thePackageDirectoryInputFileReader = packageDirectoryFileReader;
			theInputFileReader = packageFileReader;
			theApkFileData = apkFileData;
		}

#endregion

#region Properties

		public ApkFileData FileData
		{
			get
			{
				return theApkFileData;
			}
		}

#endregion

#region Methods

		public override void ReadHeader()
		{
			theInputFileReader.BaseStream.Seek(0, SeekOrigin.Begin);

			theApkFileData.id = theInputFileReader.ReadUInt32();
			theApkFileData.offsetOfFiles = theInputFileReader.ReadUInt32();
			theApkFileData.fileCount = theInputFileReader.ReadUInt32();
			theApkFileData.offsetOfDirectory = theInputFileReader.ReadUInt32();
		}

		public override void ReadEntries(BackgroundWorker bw)
		{
			if (!theApkFileData.IsSourcePackage)
			{
				return;
			}

			try
			{
				ApkDirectoryEntry entry = null;
				StringBuilder entryDataOutputText = new StringBuilder();
				string pathFileName = null;

				theInputFileReader.BaseStream.Seek(theApkFileData.offsetOfDirectory, SeekOrigin.Begin);

//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of (uint)(theApkFileData.fileCount - 1) for every iteration:
				UInt32 tempVar = (uint)(theApkFileData.fileCount - 1);
				for (UInt32 directoryEntryIndex = 0; directoryEntryIndex <= tempVar; directoryEntryIndex++)
				{
					entry = new ApkDirectoryEntry();
					entryDataOutputText.Clear();

					entry.pathFileNameSize = theInputFileReader.ReadUInt32();
					pathFileName = FileManager.ReadNullTerminatedString(theInputFileReader);
					entry.thePathFileName = pathFileName.Replace('\\', '/');
					entry.offsetOfFile = theInputFileReader.ReadUInt32();
					entry.fileSize = theInputFileReader.ReadUInt32();
					entry.offsetOfNextDirectoryEntry = theInputFileReader.ReadUInt32();
					theInputFileReader.ReadUInt32();
					entry.crc = 0;
					theApkFileData.theEntries.Add(entry);

					entryDataOutputText.Append(entry.thePathFileName);
					entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"));
					entryDataOutputText.Append(" metadatasz=0");
					entryDataOutputText.Append(" fnumber=0");
					entryDataOutputText.Append(" ofs=0x" + entry.offsetOfFile.ToString("X8"));
					entryDataOutputText.Append(" sz=" + entry.fileSize.ToString("G0"));
					theApkFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString());

					NotifyPackEntryRead(entry, entryDataOutputText.ToString());
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		public override void UnpackEntryDataToFile(BasePackageDirectoryEntry iEntry, string outputPathFileName)
		{
			ApkDirectoryEntry entry = (ApkDirectoryEntry)iEntry;

			FileStream outputFileStream = null;
			try
			{
				outputFileStream = new FileStream(outputPathFileName, FileMode.Create);
				if (outputFileStream != null)
				{
					try
					{
						theOutputFileWriter = new BinaryWriter(outputFileStream, System.Text.Encoding.ASCII);

						theInputFileReader.BaseStream.Seek(entry.offsetOfFile, SeekOrigin.Begin);
						byte[] bytes = theInputFileReader.ReadBytes((int)entry.fileSize);
						theOutputFileWriter.Write(bytes);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
					finally
					{
						if (theOutputFileWriter != null)
						{
							theOutputFileWriter.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (outputFileStream != null)
				{
					outputFileStream.Close();
				}
			}
		}

#endregion

#region Private Methods

#endregion

#region Data

		private BinaryReader thePackageDirectoryInputFileReader;
		private BinaryReader theInputFileReader;
		private BinaryWriter theOutputFileWriter;
		private ApkFileData theApkFileData;

#endregion

	}

}