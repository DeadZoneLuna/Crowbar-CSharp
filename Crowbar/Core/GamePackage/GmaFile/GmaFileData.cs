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
	public class GmaFileData : BasePackageFileData
	{
		public GmaFileData() : base()
		{

		}

		public override bool IsSourcePackage
		{
			get
			{
				return (this.id == GmaFileData.GMA_ID);
			}
		}

		public override string FileExtension
		{
			get
			{
				return GmaFileData.TheGmaFileExtension;
			}
		}

		public override string DirectoryFileNameSuffix
		{
			get
			{
				//Return GmaFileData.TheGmaDirectoryFileNameSuffix
				return "";
			}
		}

		public override string DirectoryFileNameSuffixWithExtension
		{
			get
			{
				//Return GmaFileData.TheGmaDirectoryFileNameSuffix + GmaFileData.TheGmaFileExtension
				return "";
			}
		}

		//FROM: Garry's Mod Addon Tool source gmad-master\gmad-master\src\create_gmad.cpp   Create()
		//		buffer.Write( Addon::Ident, 4 );				// Ident (4)
		//		buffer.WriteType( ( char ) Addon::Version );		// Version (1)
		//		// SteamID (8) [unused]
		//		buffer.WriteType( ( unsigned long long ) 0ULL );
		//		// TimeStamp (8)
		//		buffer.WriteType( ( unsigned long long ) Bootil::Time::UnixTimestamp() );
		//		// Required content (a list of strings)
		//		buffer.WriteType( ( char ) 0 ); // signifies nothing
		//		// Addon Name (n)
		//		buffer.WriteString( strTitle );
		//		// Addon Description (n)
		//		buffer.WriteString( strDescription );
		//		// Addon Author (n) [unused]
		//		buffer.WriteString( "Author Name" );
		//		// Addon Version (4) [unused]
		//		buffer.WriteType( ( int ) 1 );

		public string id;
		public byte version;
		public byte[] steamID = new byte[8];
		public byte[] timestamp = new byte[8];
		public string requiredContent;
		public string addonName;
		public string addonDescription;
		public string addonAuthor;
		public UInt32 addonVersion;

		public UInt32 crc;

		private const string GMA_ID = "GMAD";
		//Private Const TheGmaDirectoryFileNameSuffix As String = "_dir"
		private const string TheGmaFileExtension = ".gma";
		public long theFileDataOffset;

	}

}