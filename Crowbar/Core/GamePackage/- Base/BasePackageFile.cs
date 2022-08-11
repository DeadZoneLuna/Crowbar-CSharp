using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;

namespace Crowbar
{
	public abstract class BasePackageFile
	{

#region Shared

		public static BasePackageFile Create(string mdlPathFileName, BinaryReader packageDirectoryFileReader, BinaryReader packageFileReader, ref BasePackageFileData packageFileData)
		{
			BasePackageFile packageFile = null;

			try
			{
				string extension = Path.GetExtension(mdlPathFileName);

				if (extension == ".apk")
				{
					if (packageFileData == null)
					{
						packageFileData = new ApkFileData();
					}
					packageFile = new ApkFile(packageDirectoryFileReader, packageFileReader, (ApkFileData)packageFileData);
				}
				else if (extension == ".fpx" || extension == ".vpk")
				{
					if (packageFileData == null)
					{
						packageFileData = new VpkFileData();
					}
					packageFile = new VpkFile(packageDirectoryFileReader, packageFileReader, (VpkFileData)packageFileData);
				}
				else if (extension == ".gma")
				{
					if (packageFileData == null)
					{
						packageFileData = new GmaFileData();
					}
					packageFile = new GmaFile(packageDirectoryFileReader, packageFileReader, (GmaFileData)packageFileData);
				}
				else if (extension == ".hfs")
				{
					if (packageFileData == null)
					{
						packageFileData = new HfsFileData();
					}
					packageFile = new HfsFile(packageDirectoryFileReader, packageFileReader, (HfsFileData)packageFileData);
				}
				else
				{
					// Package not implemented.
					packageFileData = null;
					packageFile = null;
				}
			}
			catch (Exception ex)
			{
				throw;
			}

			return packageFile;
		}

		public static List<string> GetListOfPackageExtensions()
		{
			List<string> packageExtensions = new List<string>();
			packageExtensions.Add("*.apk");
			packageExtensions.Add("*.fpx");
			packageExtensions.Add("*.gma");
			//packageExtensions.Add("*.hfs")
			packageExtensions.Add("*.vpk");
			return packageExtensions;
		}

#endregion

		public abstract void ReadHeader();
		public abstract void ReadEntries(BackgroundWorker bw);
		public abstract void UnpackEntryDataToFile(BasePackageDirectoryEntry iEntry, string outputPathFileName);

#region Events

		public delegate void PackEntryReadEventHandler(object sender, SourcePackageEventArgs e);
		public event PackEntryReadEventHandler PackEntryRead;
		protected void NotifyPackEntryRead(BasePackageDirectoryEntry entry, string entryDataOutputText)
		{
			if (PackEntryRead != null)
				PackEntryRead(this, new SourcePackageEventArgs(entry, entryDataOutputText.ToString()));
		}

#endregion

	}

}