using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlModelGroup
	{

		//// demand loaded sequence groups
		//struct mstudiomodelgroup_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					szlabelindex;	// textual name
		//	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
		//	int					sznameindex;	// file name
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//};

		//	int					szlabelindex;	// textual name
		public int labelOffset;
		//	int					sznameindex;	// file name
		public int fileNameOffset;

		public string theLabel;
		public string theFileName;



		public SourceMdlFileData theMdlFileData;

	}

}