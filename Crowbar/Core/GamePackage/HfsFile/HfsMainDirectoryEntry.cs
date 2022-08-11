using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class HfsMainDirectoryEntry
	{

		//    char {4}     - Signature (HF) (0x02014648)
		//    uint16 {2}   - Version created with (seen as 20)
		//    uint16 {2}   - Version needed to extract (seen as 20)
		//    uint16 {2}   - General purpose bit flag
		//    uint16 {2}   - Compression method (always 'Stored')
		//    uint16 {2}   - Last mod file time
		//    uint16 {2}   - Last mod file date
		//    uint32 {4}   - CRC32
		//    uint32 {4}   - Compressed File Size
		//    uint32 {4}   - Decompressed File Size
		//    uint16 {2}   - Filename Length
		//    uint16 {2}   - Extra Field Length
		//    uint16 {2}   - Comment Length
		//    uint16 {2}   - Disk number
		//    uint16 {2}   - Internal attributes
		//    uint32 {4}   - External attributes
		//    uint32 {4}   - Relative offset of local file header 
		//    char {X}     - Filename (Obfuscated)
		//    char {X}     - Extra field
		//    char {X}     - comment
		public UInt32 signature;
		public UInt16 unused01;
		public UInt16 unused02;
		public UInt16 unused03;
		public UInt16 unused04;
		public UInt16 fileLastModificationTime;
		public UInt16 fileLastModificationDate;
		public UInt32 crc;
		public UInt32 compressedFileSize;
		public UInt32 decompressedFileSize;
		public UInt16 fileNameSize;
		public UInt16 extraFieldSize;
		public UInt16 commentSize;
		public UInt16 unused05;
		public UInt16 unused06;
		public UInt32 unused07;
		public UInt32 fileDataHeaderOffset;
		public string fileName;

	}

}