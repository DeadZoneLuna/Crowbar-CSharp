using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlActivityModifier
	{

		//FROM: AlienSwarm_source\src\public\studio.h
		//struct mstudioactivitymodifier_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();

		//	int					sznameindex;
		//	inline char			*pszName() { return (sznameindex) ? (char *)(((byte *)this) + sznameindex ) : NULL; }
		//};



		public int nameOffset;


		public string theName;

	}

}