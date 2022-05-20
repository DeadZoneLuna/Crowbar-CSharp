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
	public class SourceMdlHitboxSet2531
	{

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudiohitboxset_t
		//{
		//	int					sznameindex;
		//	inline char * const	pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int					numhitboxes;
		//	int					hitboxindex;
		//	inline mstudiobbox_t *pHitbox( int i ) const { return (mstudiobbox_t *)(((byte *)this) + hitboxindex) + i; };
		//};

		public int nameOffset;
		public int hitboxCount;
		public int hitboxOffset;


		public string theName;
		public List<SourceMdlHitbox2531> theHitboxes;

	}

}