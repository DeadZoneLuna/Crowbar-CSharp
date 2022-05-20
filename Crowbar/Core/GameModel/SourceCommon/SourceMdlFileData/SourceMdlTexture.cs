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
	public class SourceMdlTexture
	{

		//// skin info
		//struct mstudiotexture_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int						sznameindex;
		//	inline char * const		pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int						flags;
		//	int						used;
		//    int						unused1;
		//	mutable IMaterial		*material;  // fixme: this needs to go away . .isn't used by the engine, but is used by studiomdl
		//	mutable void			*clientmaterial;	// gary, replace with client material pointer if used

		//	int						unused[10];
		//};

		//	int						sznameindex;
		public int nameOffset;
		//	int						flags;
		public int flags;
		//	int						used;
		public int used;
		//    int						unused1;
		public int unused1;
		//	mutable IMaterial		*material;  // fixme: this needs to go away . .isn't used by the engine, but is used by studiomdl
		public int materialP;
		//	mutable void			*clientmaterial;	// gary, replace with client material pointer if used
		public int clientMaterialP;
		//	int						unused[10];
		public int[] unused = new int[10];

		//	inline char * const		pszName( void ) const { return ((char *)this) + sznameindex; }
		public string thePathFileName;

	}

}