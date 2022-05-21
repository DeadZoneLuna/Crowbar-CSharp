//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class VpkFileData : BasePackageFileData
	{
		public VpkFileData() : base()
		{

			theEntryCount = 0;
			//Me.theEntries = New List(Of VpkDirectoryEntry)()
			//Me.theEntryDataOutputTexts = New List(Of String)()
		}

		public override bool IsSourcePackage
		{
			get
			{
				return ((id == VpkFileData.VPK_ID) || (id == VpkFileData.FPX_ID) || theEntryCount > 0);
			}
		}

		public override string FileExtension
		{
			get
			{
				if (id == VpkFileData.FPX_ID)
				{
					return VpkFileData.TheFpxFileExtension;
				}
				else
				{
					return VpkFileData.TheVpkFileExtension;
				}
			}
		}

		public override string DirectoryFileNameSuffix
		{
			get
			{
				if (id == VpkFileData.FPX_ID)
				{
					return VpkFileData.TheFpxDirectoryFileNameSuffix;
				}
				else
				{
					return VpkFileData.TheVpkDirectoryFileNameSuffix;
				}
			}
		}

		public override string DirectoryFileNameSuffixWithExtension
		{
			get
			{
				if (id == VpkFileData.FPX_ID)
				{
					return VpkFileData.TheFpxDirectoryFileNameSuffix + VpkFileData.TheFpxFileExtension;
				}
				else
				{
					return VpkFileData.TheVpkDirectoryFileNameSuffix + VpkFileData.TheVpkFileExtension;
				}
			}
		}

		public bool PackageHasID
		{
			get
			{
				return ((id == VpkFileData.VPK_ID) || (id == VpkFileData.FPX_ID));
			}
		}

		//FROM: Nem's Tools\hllib245\HLLib\VPKFile.h
		//		struct VPKHeader
		//		{
		//			hlUInt uiSignature;			// Always 0x55aa1234.
		//			hlUInt uiVersion;
		//			hlUInt uiDirectoryLength;
		//		};
		//
		//		// Added in version 2.
		//		struct VPKExtendedHeader
		//		{
		//			hlUInt uiDummy0;
		//			hlUInt uiArchiveHashLength;
		//			hlUInt uiExtraLength;		// Looks like some more MD5 hashes.
		//			hlUInt uiDummy1;
		//		};
		//
		//		struct VPKDirectoryEntry
		//		{
		//			hlUInt uiCRC;
		//			hlUShort uiPreloadBytes;
		//			hlUShort uiArchiveIndex;
		//			hlUInt uiEntryOffset;
		//			hlUInt uiEntryLength;
		//			hlUShort uiDummy0;			// Always 0xffff.
		//		};
		//
		//		// Added in version 2.
		//		struct VPKArchiveHash
		//		{
		//			hlUInt uiArchiveIndex;
		//			hlUInt uiArchiveOffset;
		//			hlUInt uiLength;
		//			hlByte lpHash[16];			// MD5
		//		};

		//FROM: VDC
		//// How many bytes of file content are stored in this VPK file (0 in CSGO)
		//unsigned int FileDataSectionSize;

		//// The size, in bytes, of the section containing MD5 checksums for external archive content
		//unsigned int ArchiveMD5SectionSize;

		//// The size, in bytes, of the section containing MD5 checksums for content in this file (should always be 48)
		//unsigned int OtherMD5SectionSize;

		//// The size, in bytes, of the section containing the public key and signature. This is either 0 (CSGO & The Ship) or 296 (HL2, HL2:DM, HL2:EP1, HL2:EP2, HL2:LC, TF2, DOD:S & CS:S)
		//unsigned int SignatureSectionSize;


		public UInt32 id;
		public UInt32 version;
		public UInt32 directoryLength;

		public UInt32 unused01;
		public UInt32 archiveHashLength;
		public UInt32 extraLength;
		public UInt32 unused02;

		public UInt32 archiveIndex;
		public UInt32 archiveOffset;
		public UInt32 archiveLength;
		public byte[] md5Hash = new byte[16];


		public uint theEntryCount;
		public long theDirectoryOffset;
		//Public theEntries As List(Of VpkDirectoryEntry)
		//Public theEntryDataOutputTexts As List(Of String)


		//#define HL_VPK_SIGNATURE 0x55aa1234
		private const int VPK_ID = 0x55AA1234;
		private const string TheVpkDirectoryFileNameSuffix = "_dir";
		private const string TheVpkFileExtension = ".vpk";

		private const int FPX_ID = 0x33FF4132;
		private const string TheFpxDirectoryFileNameSuffix = "_fdr";
		private const string TheFpxFileExtension = ".fpx";

	}

}