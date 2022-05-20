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
	public class SourceMdlFlexController
	{

		//struct mstudioflexcontroller_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					sztypeindex;
		//	inline char * const pszType( void ) const { return ((char *)this) + sztypeindex; }
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	mutable int			localToGlobal;	// remapped at load time to master list
		//	float				min;
		//	float				max;
		//};

		//	int					sztypeindex;
		public int typeOffset;
		//	int					sznameindex;
		public int nameOffset;
		//	mutable int			localToGlobal;	// remapped at load time to master list
		public int localToGlobal;
		//	float				min;
		public float min;
		//	float				max;
		public float max;

		public string theName;
		public string theType;

	}

}