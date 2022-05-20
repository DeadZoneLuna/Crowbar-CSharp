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
	public class VpkFile : BasePackageFile
	{
#region Creation and Destruction

		public VpkFile(BinaryReader archiveDirectoryFileReader, BinaryReader vpkFileReader, VpkFileData vpkFileData)
		{
			this.theArchiveDirectoryInputFileReader = archiveDirectoryFileReader;
			this.theInputFileReader = vpkFileReader;
			this.theVpkFileData = vpkFileData;
		}

#endregion

#region Properties

		public VpkFileData FileData
		{
			get
			{
				return this.theVpkFileData;
			}
		}

#endregion

#region Methods

		public override void ReadHeader()
		{
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			this.theVpkFileData.id = this.theInputFileReader.ReadUInt32();

			//NOTE: The arrangement of this 'if" block is weird, but it keeps the order of checks like this: Valve VPK, Vtmb VPK, non-directory multi-file Valve VPK.
			inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
			if (this.theVpkFileData.PackageHasID)
			{
				this.ReadValveVpkHeader();
			}
			else if (!this.IsVtmbVpk())
			{
				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				this.ReadValveVpkHeader();
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theVpkFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VPK File Header");
		}

		private void ReadValveVpkHeader()
		{
			this.theVpkFileData.version = this.theInputFileReader.ReadUInt32();
			this.theVpkFileData.directoryLength = this.theInputFileReader.ReadUInt32();

			if (this.theVpkFileData.version == 2)
			{
				this.theVpkFileData.unused01 = this.theInputFileReader.ReadUInt32();
				this.theVpkFileData.archiveHashLength = this.theInputFileReader.ReadUInt32();
				this.theVpkFileData.extraLength = this.theInputFileReader.ReadUInt32();
				this.theVpkFileData.unused01 = this.theInputFileReader.ReadUInt32();
			}
			else if (this.theVpkFileData.version == 196610)
			{
				this.theVpkFileData.unused01 = this.theInputFileReader.ReadUInt32();
			}

			this.theVpkFileData.theDirectoryOffset = this.theInputFileReader.BaseStream.Position;
		}

		private bool IsVtmbVpk()
		{
			bool theVpkIsVtmb = false;

			this.theInputFileReader.BaseStream.Seek(-1, SeekOrigin.End);
			int vtmbVpkType = this.theInputFileReader.ReadByte();
			//NOTE: Skip reading vtmbVpkType = 1 because it is just a directory of entries with no data.
			if (vtmbVpkType == 0 || vtmbVpkType == 1)
			{
				long directoryEndOffset = this.theInputFileReader.BaseStream.Seek(-9, SeekOrigin.End);
				this.theVpkFileData.theEntryCount = this.theInputFileReader.ReadUInt32();
				this.theVpkFileData.theDirectoryOffset = this.theInputFileReader.ReadUInt32();
				//TODO: It is VTMB VPK package if offsets and lengths match in the directory at end of file.
				//      Would need to check that offsets and lengths are within file length boundaries.
				theVpkIsVtmb = true;
				uint entryPathFileNameLength = 0;
				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theVpkFileData.theDirectoryOffset, SeekOrigin.Begin);
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of (uint)(this.theVpkFileData.theEntryCount - 1) for every iteration:
					uint tempVar = (uint)(this.theVpkFileData.theEntryCount - 1);
					for (uint i = 0; i <= tempVar; i++)
					{
						entryPathFileNameLength = this.theInputFileReader.ReadUInt32();
						//entry.thePathFileName = Me.theInputFileReader.ReadChars(CInt(entryPathFileNameLength))
						//entry.dataOffset = Me.theInputFileReader.ReadUInt32()
						//entry.dataLength = Me.theInputFileReader.ReadUInt32()
						this.theInputFileReader.BaseStream.Seek(entryPathFileNameLength + 8, SeekOrigin.Current);
					}
					//NOTE: Do not accept 'vtmbVpkType = 1' as a valid VtmbVpk because it is just a directory of entries with no data.
					if (this.theInputFileReader.BaseStream.Position != directoryEndOffset || vtmbVpkType == 1)
					{
						this.theVpkFileData.theEntryCount = 0;
						theVpkIsVtmb = false;
					}
				}
				catch (Exception ex)
				{
					this.theVpkFileData.theEntryCount = 0;
					theVpkIsVtmb = false;
				}
			}

			return theVpkIsVtmb;
		}

		//Example output:
		//addonimage.jpg crc=0x50ea4a15 metadatasz=0 fnumber=32767 ofs=0x0 sz=10749
		//addonimage.vtf crc=0xc75861f5 metadatasz=0 fnumber=32767 ofs=0x29fd sz=8400
		//addoninfo.txt crc=0xb3d2b571 metadatasz=0 fnumber=32767 ofs=0x4acd sz=1677
		//materials/models/weapons/melee/crowbar.vmt crc=0x4aaf5f0 metadatasz=0 fnumber=32767 ofs=0x515a sz=566
		//materials/models/weapons/melee/crowbar.vtf crc=0xded2e058 metadatasz=0 fnumber=32767 ofs=0x5390 sz=174920
		//materials/models/weapons/melee/crowbar_normal.vtf crc=0x7ac0e054 metadatasz=0 fnumber=32767 ofs=0x2fed8 sz=1398196
		public override void ReadEntries(BackgroundWorker bw)
		{
			//Dim inputFileStreamPosition As Long
			//Dim fileOffsetStart As Long
			//Dim fileOffsetEnd As Long
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			//If Me.theVpkFileData.id <> VpkFileData.VPK_ID OrElse Me.theVpkFileData.id <> VpkFileData.FPX_ID Then
			//	Exit Sub
			//End If
			if (!this.theVpkFileData.IsSourcePackage)
			{
				return;
			}

			if (!this.theVpkFileData.PackageHasID)
			{
				ReadVtmbEntries(bw);
				return;
			}

			bool vpkFileHasMoreToRead = true;
			string entryExtension = "";
			string entryPath = "";
			string entryFileName = "";
			VpkDirectoryEntry entry = null;
			StringBuilder entryDataOutputText = new StringBuilder();
			while (vpkFileHasMoreToRead)
			{
				try
				{
					entryExtension = FileManager.ReadNullTerminatedString(this.theInputFileReader);
					if (string.IsNullOrEmpty(entryExtension))
					{
						break;
					}
					if (bw != null && bw.CancellationPending)
					{
						vpkFileHasMoreToRead = false;
					}
				}
				catch (Exception ex)
				{
					//vpkFileHasMoreToRead = False
					break;
				}

				while (vpkFileHasMoreToRead)
				{
					try
					{
						entryPath = FileManager.ReadNullTerminatedString(this.theInputFileReader);
						if (string.IsNullOrEmpty(entryPath))
						{
							break;
						}
						if (bw != null && bw.CancellationPending)
						{
							vpkFileHasMoreToRead = false;
						}
					}
					catch (Exception ex)
					{
						vpkFileHasMoreToRead = false;
						break;
					}

					while (vpkFileHasMoreToRead)
					{
						try
						{
							entryFileName = FileManager.ReadNullTerminatedString(this.theInputFileReader);
							if (string.IsNullOrEmpty(entryFileName))
							{
								break;
							}
							if (bw != null && bw.CancellationPending)
							{
								vpkFileHasMoreToRead = false;
							}
						}
						catch (Exception ex)
						{
							vpkFileHasMoreToRead = false;
							break;
						}

						entry = new VpkDirectoryEntry();
						entry.crc = this.theInputFileReader.ReadUInt32();
						entry.preloadByteCount = this.theInputFileReader.ReadUInt16();
						entry.archiveIndex = this.theInputFileReader.ReadUInt16();
						if (this.theVpkFileData.version == 196610)
						{
							//TODO: Exit for now so Crowbar does not freeze.
							return;
							//' 01 01
							//entry.unknown01 = Me.theInputFileReader.ReadUInt16()
							//' 00 00 00 80 
							//entry.unknown02 = Me.theInputFileReader.ReadUInt32()
							//entry.dataOffset = Me.theInputFileReader.ReadUInt32()
							//entry.unknown03 = Me.theInputFileReader.ReadUInt32()
							//entry.dataLength = Me.theInputFileReader.ReadUInt32()
							//entry.unknown04 = Me.theInputFileReader.ReadUInt32()
							//entry.fileSize = Me.theInputFileReader.ReadUInt32()
							//entry.unknown05 = Me.theInputFileReader.ReadUInt32()
							//' FF FF
							//entry.endOfEntryBytes = Me.theInputFileReader.ReadUInt16()
						}
						else
						{
							entry.dataOffset = this.theInputFileReader.ReadUInt32();
							entry.dataLength = this.theInputFileReader.ReadUInt32();
							entry.endBytes = this.theInputFileReader.ReadUInt16();

							if (entry.preloadByteCount > 0)
							{
								entry.preloadBytesOffset = this.theInputFileReader.BaseStream.Position;
								//Me.theInputFileReader.ReadBytes(entry.preloadByteCount)
								this.theInputFileReader.BaseStream.Position += entry.preloadByteCount;

								if (entry.dataLength == 0)
								{
									//NOTE: Reaches here when a packed file is actually stored in the "_dir" vpk file, so override whatever archiveIndex was assigned.
									entry.archiveIndex = 0x7FFF;
								}
							}
						}

						if (entryPath == " ")
						{
							entry.thePathFileName = entryFileName + "." + entryExtension;
						}
						else
						{
							entry.thePathFileName = entryPath + "/" + entryFileName + "." + entryExtension;
						}
						this.theVpkFileData.theEntries.Add(entry);

						entryDataOutputText.Append(entry.thePathFileName);
						entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"));
						entryDataOutputText.Append(" metadatasz=" + entry.preloadByteCount.ToString("G0"));
						entryDataOutputText.Append(" fnumber=" + entry.archiveIndex.ToString("G0"));
						entryDataOutputText.Append(" ofs=0x" + entry.dataOffset.ToString("X8"));
						entryDataOutputText.Append(" sz=" + (entry.preloadByteCount + entry.dataLength).ToString("G0"));

						this.theVpkFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString());
						NotifyPackEntryRead(entry, entryDataOutputText.ToString());

						entryDataOutputText.Clear();

						if (bw != null && bw.CancellationPending)
						{
							vpkFileHasMoreToRead = false;
						}
					}
				}
			}

			//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			//Me.theVpkFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "VPK File Header")
		}

		private void ReadVtmbEntries(BackgroundWorker bw)
		{
			uint entryPathFileNameLength = 0;
			string entryFileName = "";
			VpkDirectoryEntry entry = null;
			StringBuilder entryDataOutputText = new StringBuilder();

			this.theInputFileReader.BaseStream.Seek(this.theVpkFileData.theDirectoryOffset, SeekOrigin.Begin);
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of (uint)(this.theVpkFileData.theEntryCount - 1) for every iteration:
			uint tempVar = (uint)(this.theVpkFileData.theEntryCount - 1);
			for (uint i = 0; i <= tempVar; i++)
			{
				entry = new VpkDirectoryEntry();

				entryPathFileNameLength = this.theInputFileReader.ReadUInt32();
				entry.thePathFileName = new string(this.theInputFileReader.ReadChars((int)entryPathFileNameLength));
				entry.dataOffset = this.theInputFileReader.ReadUInt32();
				entry.dataLength = this.theInputFileReader.ReadUInt32();

				entry.crc = 0;
				entry.preloadByteCount = 0;
				//entry.archiveIndex = &H7FFF
				entry.endBytes = 0;
				entry.isVtmbVpk = true;

				this.theVpkFileData.theEntries.Add(entry);

				entryDataOutputText.Append(entry.thePathFileName);
				entryDataOutputText.Append(" crc=0x" + entry.crc.ToString("X8"));
				entryDataOutputText.Append(" metadatasz=" + entry.preloadByteCount.ToString("G0"));
				entryDataOutputText.Append(" fnumber=" + entry.archiveIndex.ToString("G0"));
				entryDataOutputText.Append(" ofs=0x" + entry.dataOffset.ToString("X8"));
				entryDataOutputText.Append(" sz=" + (entry.preloadByteCount + entry.dataLength).ToString("G0"));

				this.theVpkFileData.theEntryDataOutputTexts.Add(entryDataOutputText.ToString());
				NotifyPackEntryRead(entry, entryDataOutputText.ToString());

				entryDataOutputText.Clear();

				if (bw != null && bw.CancellationPending)
				{
					break;
				}
			}
		}

		public override void UnpackEntryDataToFile(BasePackageDirectoryEntry iEntry, string outputPathFileName)
		{
			VpkDirectoryEntry entry = (VpkDirectoryEntry)iEntry;

			FileStream outputFileStream = null;
			try
			{
				outputFileStream = new FileStream(outputPathFileName, FileMode.Create);
				if (outputFileStream != null)
				{
					try
					{
						this.theOutputFileWriter = new BinaryWriter(outputFileStream, System.Text.Encoding.ASCII);

						if (entry.preloadByteCount > 0)
						{
							this.theArchiveDirectoryInputFileReader.BaseStream.Seek(entry.preloadBytesOffset, SeekOrigin.Begin);
							byte[] preloadBytes = this.theArchiveDirectoryInputFileReader.ReadBytes((int)entry.preloadByteCount);
							this.theOutputFileWriter.Write(preloadBytes);
						}
						if (entry.archiveIndex == 0x7FFF && !entry.isVtmbVpk)
						{
							this.theInputFileReader.BaseStream.Seek(this.theVpkFileData.theDirectoryOffset + this.theVpkFileData.directoryLength + entry.dataOffset, SeekOrigin.Begin);
						}
						else
						{
							this.theInputFileReader.BaseStream.Seek(entry.dataOffset, SeekOrigin.Begin);
						}
						byte[] bytes = this.theInputFileReader.ReadBytes((int)entry.dataLength);
						this.theOutputFileWriter.Write(bytes);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
					finally
					{
						if (this.theOutputFileWriter != null)
						{
							this.theOutputFileWriter.Close();
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

		private BinaryReader theArchiveDirectoryInputFileReader;
		private BinaryReader theInputFileReader;
		private BinaryWriter theOutputFileWriter;
		private VpkFileData theVpkFileData;

#endregion

	}

}