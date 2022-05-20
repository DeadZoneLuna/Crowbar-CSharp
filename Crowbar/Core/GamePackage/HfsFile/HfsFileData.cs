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
	public class HfsFileData : BasePackageFileData
	{
		public HfsFileData() : base()
		{

		}

		public override bool IsSourcePackage
		{
			get
			{
				return (this.id == HfsFileData.HFS_ID);
			}
		}

		public override string FileExtension
		{
			get
			{
				return HfsFileData.TheHfsFileExtension;
			}
		}

		public override string DirectoryFileNameSuffix
		{
			get
			{
				return "";
			}
		}

		public override string DirectoryFileNameSuffixWithExtension
		{
			get
			{
				return "";
			}
		}

		public UInt32 id;

		public long theMainDirectoryHeaderOffset;

		private const UInt32 HFS_ID = 0x2014648;
		private const string TheHfsFileExtension = ".hfs";

	}

}