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
	public class SourceMdlEvent
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//// events
		//struct mstudioevent_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	float				cycle;
		//	int					event;
		//	int					type;
		//	inline const char * pszOptions( void ) const { return options; }
		//	char				options[64];

		//	int					szeventindex;
		//	inline char * const pszEventName( void ) const { return ((char *)this) + szeventindex; }
		//};


		public double cycle;
		public int eventIndex;
		public int eventType;
		public char[] options = new char[64];
		public int nameOffset;



		public string theName;



		//FROM: SourceEngineXXXX_source\public\studio.h
		//#define NEW_EVENT_STYLE ( 1 << 10 )
		public const int NEW_EVENT_STYLE = 1 << 10;

	}

}