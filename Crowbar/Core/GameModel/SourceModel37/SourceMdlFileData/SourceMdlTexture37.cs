using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlTexture37
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
		//struct mstudiotexture_t
		//{
		//	int	sznameindex;
		//	inline char * const		pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int	flags;
		//	float	width;		// portion used
		//	float	height;		// portion used
		//	mutable IMaterial  *material;  // fixme: this needs to go away . .isn't used by the engine, but is used by studiomdl
		//	mutable void	   *clientmaterial;	// gary, replace with client material pointer if used
		//	float	dPdu;		// world units per u
		//	float	dPdv;		// world units per v
		//};

		public int fileNameOffset;
		public int flags;
		public double width;
		public double height;
		public double worldUnitsPerU;
		public double worldUnitsPerV;
		public int[] unknown = new int[2];

		public string theFileName;

	}

}