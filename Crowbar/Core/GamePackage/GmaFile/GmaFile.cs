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
	public class GmaFile : BasePackageFile
	{
#region Creation and Destruction

		public GmaFile(BinaryReader archiveDirectoryFileReader, BinaryReader archiveFileReader, GmaFileData gmaFileData)
		{
			theArchiveDirectoryInputFileReader = archiveDirectoryFileReader;
			theInputFileReader = archiveFileReader;
			theGmaFileData = gmaFileData;
		}

#endregion

#region Properties

		public GmaFileData FileData
		{
			get
			{
				return theGmaFileData;
			}
		}

#endregion

#region Methods

		public override void ReadHeader()
		{
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theGmaFileData.id = new string(theInputFileReader.ReadChars(4));
			theGmaFileData.version = theInputFileReader.ReadByte();
			theGmaFileData.steamID = theInputFileReader.ReadBytes(8);
			theGmaFileData.timestamp = theInputFileReader.ReadBytes(8);

			//				if ( m_fmtversion > 1 )
			//				{
			//					Bootil::BString strContent = m_buffer.ReadString();
			//
			//					while ( !strContent.empty() )
			//					{
			//						strContent = m_buffer.ReadString();
			//					}
			//				}
			if (theGmaFileData.version > 1)
			{
				theGmaFileData.requiredContent = FileManager.ReadNullTerminatedString(theInputFileReader);
			}

			theGmaFileData.addonName = FileManager.ReadNullTerminatedString(theInputFileReader);
			theGmaFileData.addonDescription = FileManager.ReadNullTerminatedString(theInputFileReader);
			theGmaFileData.addonAuthor = FileManager.ReadNullTerminatedString(theInputFileReader);
			theGmaFileData.addonVersion = theInputFileReader.ReadUInt32();

			//Me.theGmaFileData.theDirectoryOffset = Me.theInputFileReader.BaseStream.Position

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theGmaFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "GMA File Header");
		}

		//WRITE: 
		//		// File list
		//		unsigned int iFileNum = 0;
		//		BOOTIL_FOREACH( f, files, String::List )
		//		{
		//			unsigned long	iCRC = Bootil::File::CRC( strFolder + *f );
		//			long long		iSize = Bootil::File::Size( strFolder + *f );
		//			iFileNum++;
		//			buffer.WriteType( ( unsigned int ) iFileNum );					// File number (4)
		//			buffer.WriteString( String::GetLower( *f ) );					// File name (all lower case!) (n)
		//			buffer.WriteType( ( long long ) iSize );							// File size (8)
		//			buffer.WriteType( ( unsigned long ) iCRC );						// File CRC (4)
		//			Output::Msg( "File index: %s [CRC:%u] [Size:%s]\n", f->c_str(), iCRC, String::Format::Memory( iSize ).c_str() );
		//		}
		//		// Zero to signify end of files
		//		iFileNum = 0;
		//		buffer.WriteType( ( unsigned int ) iFileNum );
		//READ: 
		//				int iFileNumber = 1;
		//				int iOffset = 0;
		//				while ( m_buffer.ReadType<unsigned int>() != 0 )
		//				{
		//					Addon::FileEntry entry;
		//					entry.strName		= m_buffer.ReadString();
		//					entry.iSize			= m_buffer.ReadType<long long>();
		//					entry.iCRC			= m_buffer.ReadType<unsigned long>();
		//					entry.iOffset		= iOffset;
		//					entry.iFileNumber	= iFileNumber;
		//					m_index.push_back( entry );
		//					iOffset += entry.iSize;
		//					iFileNumber++;
		//				}
		public override void ReadEntries(BackgroundWorker bw)
		{
			if (!theGmaFileData.IsSourcePackage)
			{
				return;
			}

			GmaDirectoryEntry entry = null;
			UInt32 fileNumber = 1;
			Int64 offset = 0;
			UInt32 fileNumberStored = 0;

			try
			{
				StringBuilder entryDataOutputText = new StringBuilder();

				// Make a fake entry for the "addon.json" file.
				entry = new GmaDirectoryEntry();

				entry.fileNumberStored = 0;
				entry.thePathFileName = "<addon.json>";
				entry.theRealPathFileName = "addon.json";
				entry.size = GetAddonJsonText().Length;
				entry.crc = 0;
				entry.offset = 0;
				entry.fileNumberUsed = 0;

				theGmaFileData.theEntries.Add(entry);

				entryDataOutputText.Append(entry.thePathFileName);
				entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"));
				entryDataOutputText.Append(" metadatasz=0");
				entryDataOutputText.Append(" fnumber=0");
				entryDataOutputText.Append(" ofs=0x" + entry.offset.ToString("X8"));
				entryDataOutputText.Append(" sz=" + entry.size.ToString("G0"));

				theGmaFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString());
				NotifyPackEntryRead(entry, entryDataOutputText.ToString());

				entryDataOutputText.Clear();

				fileNumberStored = theInputFileReader.ReadUInt32();
				while (fileNumberStored != 0)
				{
					entry = new GmaDirectoryEntry();

					entry.fileNumberStored = fileNumberStored;
					entry.thePathFileName = FileManager.ReadNullTerminatedString(theInputFileReader);
					entry.size = theInputFileReader.ReadInt64();
					entry.crc = theInputFileReader.ReadUInt32();
					entry.offset = offset;
					entry.fileNumberUsed = fileNumber;

					theGmaFileData.theEntries.Add(entry);

					offset += entry.size;
					fileNumber = (uint)(fileNumber + 1);
					fileNumberStored = theInputFileReader.ReadUInt32();

					entryDataOutputText.Append(entry.thePathFileName);
					entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"));
					entryDataOutputText.Append(" metadatasz=0");
					entryDataOutputText.Append(" fnumber=0");
					entryDataOutputText.Append(" ofs=0x" + entry.offset.ToString("X8"));
					entryDataOutputText.Append(" sz=" + entry.size.ToString("G0"));

					theGmaFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString());
					NotifyPackEntryRead(entry, entryDataOutputText.ToString());

					entryDataOutputText.Clear();
				}

				theGmaFileData.theFileDataOffset = theInputFileReader.BaseStream.Position;
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		public override void UnpackEntryDataToFile(BasePackageDirectoryEntry iEntry, string outputPathFileName)
		{
			GmaDirectoryEntry entry = (GmaDirectoryEntry)iEntry;

			FileStream outputFileStream = null;
			try
			{
				outputFileStream = new FileStream(outputPathFileName, FileMode.Create);
				if (outputFileStream != null)
				{
					try
					{
						theOutputFileWriter = new BinaryWriter(outputFileStream, System.Text.Encoding.ASCII);

						if (entry.thePathFileName == "<addon.json>")
						{
							WriteAddonJsonData();
						}
						else
						{
							theInputFileReader.BaseStream.Seek(theGmaFileData.theFileDataOffset + entry.offset, SeekOrigin.Begin);
							byte[] bytes = theInputFileReader.ReadBytes((int)entry.size);
							theOutputFileWriter.Write(bytes);
						}
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

		private string GetAddonJsonText()
		{
			string addonJsonText = theGmaFileData.addonDescription.Replace("\"description\":", "\"title\":");
			addonJsonText = addonJsonText.Replace("\"Description\",", "\"" + theGmaFileData.addonName + "\",");
			addonJsonText = addonJsonText.Trim();
			addonJsonText = addonJsonText.Replace("\n", "\r\n");
			//addonJsonText = addonJsonText.Trim(Chr(&HA))
			return addonJsonText;
		}

		private void WriteAddonJsonData()
		{
			// Need to convert string to byte array to avoid length prefix value when using BinaryWriter.
			byte[] text = System.Text.Encoding.ASCII.GetBytes(GetAddonJsonText());
			theOutputFileWriter.Write(text);
		}

#endregion

#region Data

		private BinaryReader theArchiveDirectoryInputFileReader;
		private BinaryReader theInputFileReader;
		private BinaryWriter theOutputFileWriter;
		private GmaFileData theGmaFileData;

#endregion

	}

}