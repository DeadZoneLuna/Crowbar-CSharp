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
	public class SourceMdlIkChain37
	{

		//struct mstudioikchain_t
		//{
		//	int	sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int	linktype;
		//	int	numlinks;
		//	int	linkindex;
		//	inline mstudioiklink_t *pLink( int i ) const { return (mstudioiklink_t *)(((byte *)this) + linkindex) + i; };
		//};

		public int nameOffset;
		public int linkType;
		public int linkCount;
		public int linkOffset;

		public List<SourceMdlIkLink37> theLinks;
		public string theName;

	}

}