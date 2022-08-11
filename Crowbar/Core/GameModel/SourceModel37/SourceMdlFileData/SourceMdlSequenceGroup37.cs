using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlSequenceGroup37
	{

		//struct mstudioseqgroup_t
		//{
		//	int	szlabelindex;	// textual name
		//	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
		//
		//	int	sznameindex;	// file name
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	
		//	int					cache;			// cache index in the shared model cache
		//	int					data;			// hack for group 0
		//};

		public int nameOffset;
		public int fileNameOffset;

		public int cacheOffset;
		public int data;

		//For MDL v35.
		public int[] unknown = new int[8];

		public string theName;
		public string theFileName;

	}

}