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
	public class SourceMdlPoseParamDesc2531
	{

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudioposeparamdesc_t
		//{
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int					flags;	// ????
		//	float				start;	// starting value
		//	float				end;	// ending value
		//	float				loop;	// looping range, 0 for no looping, 360 for rotations, etc.
		//};

		public int nameOffset;
		public int flags;
		public double startingValue;
		public double endingValue;
		public double loopingRange;


		public string theName;

	}

}