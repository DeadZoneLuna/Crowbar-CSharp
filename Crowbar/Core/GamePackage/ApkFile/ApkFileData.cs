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
	public class ApkFileData : BasePackageFileData
	{
		public ApkFileData() : base()
		{

		}

		public override bool IsSourcePackage
		{
			get
			{
				return (this.id == ApkFileData.APK_ID);
			}
		}

		public override string FileExtension
		{
			get
			{
				return ApkFileData.TheApkFileExtension;
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

		//FROM: Fairy Tale Busters "models00.apk"
		//57 23 00 00   ID of APK
		//10 00 00 00   offset of files
		//DE 05 00 00   
		//85 EB D9 0A   offset of directory

		public UInt32 id;
		public long offsetOfFiles;
		public UInt32 fileCount;
		public long offsetOfDirectory;

		private const UInt32 APK_ID = 0x2357;
		private const string TheApkFileExtension = ".apk";

	}

}