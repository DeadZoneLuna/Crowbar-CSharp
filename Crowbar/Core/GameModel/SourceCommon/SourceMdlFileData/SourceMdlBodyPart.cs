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
	public class SourceMdlBodyPart
	{

		//struct mstudiobodyparts_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int					nummodels;
		//	int					base;
		//	int					modelindex; // index into models array
		//	inline mstudiomodel_t *pModel( int i ) const { return (mstudiomodel_t *)(((byte *)this) + modelindex) + i; };
		//};

		//   offset from start of this struct
		//	int					sznameindex;
		public int nameOffset;
		//	int					nummodels;
		public int modelCount;
		//	int					base;
		public int @base;
		//	int					modelindex; // index into models array
		public int modelOffset;

		public string theName;
		public List<SourceMdlModel> theModels;
		public bool theModelCommandIsUsed;
		public bool theEyeballOptionIsUsed;
		public List<FlexFrame> theFlexFrames;

	}

}