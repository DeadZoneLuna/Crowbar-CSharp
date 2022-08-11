using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class GmaDirectoryEntry : BasePackageDirectoryEntry
	{
		//FROM: Garry's Mod Addon Tool source gmad-master\gmad-master\include\AddonFormat.h
		//	struct FileEntry
		//	{
		//		Bootil::BString	strName;
		//		long long		iSize;
		//		unsigned long	iCRC;
		//		unsigned int	iFileNumber;
		//		long long		iOffset;
		//
		//		typedef std::list< FileEntry > List;
		//	};

		public UInt32 fileNumberStored;
		public Int64 size;

		public Int64 offset;
		public UInt32 fileNumberUsed;

	}

}