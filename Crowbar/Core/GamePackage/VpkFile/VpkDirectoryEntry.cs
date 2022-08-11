using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class VpkDirectoryEntry : BasePackageDirectoryEntry
	{
		public VpkDirectoryEntry()
		{
			isVtmbVpk = false;
		}

		//FROM: Nem's Tools\hllib245\HLLib\VPKFile.h
		//		struct VPKDirectoryEntry
		//		{
		//			hlUInt uiCRC;
		//			hlUShort uiPreloadBytes;
		//			hlUShort uiArchiveIndex;
		//			hlUInt uiEntryOffset;
		//			hlUInt uiEntryLength;
		//			hlUShort uiDummy0;			// Always 0xffff.
		//		};

		//FROM: VPKReader-master\VPKReader\VPKFile.cpp
		//struct VPKDirectoryEntry
		//{
		//    unsigned int CRC; // A 32bit CRC of the file's data.
		//    unsigned short PreloadBytes; // The number of bytes contained in the index file.
		//
		//    // A zero based index of the archive this file's data is contained in.
		//    // If 0x7fff, the data follows the directory.
		//    unsigned short ArchiveIndex;
		//
		//    // If ArchiveIndex is 0x7fff, the offset of the file data relative to the end of the directory (see the header for more details).
		//    // Otherwise, the offset of the data from the start of the specified archive.
		//    unsigned int EntryOffset;
		//
		//    // If zero, the entire file is stored in the preload data.
		//    // Otherwise, the number of bytes stored starting at EntryOffset.
		//    unsigned int EntryLength;
		//
		//    unsigned short Terminator;
		//};

		//Public crc As UInt32
		public UInt16 preloadByteCount;
		//Public archiveIndex As UInt16
		public UInt32 dataOffset;
		public UInt32 dataLength;
		public UInt16 endBytes;

		//TODO: Titanfall VPK
		public UInt16 unknown01;
		public UInt32 unknown02;
		public UInt32 unknown03;
		public UInt32 unknown04;
		public UInt32 fileSize;
		public UInt32 unknown05;
		public UInt16 endOfEntryBytes;

		//Public thePathFileName As String
		public long preloadBytesOffset;

		public bool isVtmbVpk;

	}

}