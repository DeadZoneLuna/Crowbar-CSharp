using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlFlexControllerUi
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//struct mstudioflexcontrollerui_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }

		//	// These are used like a union to save space
		//	// Here are the possible configurations for a UI controller
		//	//
		//	// SIMPLE NON-STEREO:	0: control	1: unused	2: unused
		//	// STEREO:				0: left		1: right	2: unused
		//	// NWAY NON-STEREO:		0: control	1: unused	2: value
		//	// NWAY STEREO:			0: left		1: right	2: value

		//	int					szindex0;
		//	int					szindex1;
		//	int					szindex2;

		//	inline const mstudioflexcontroller_t *pController( void ) const
		//	{
		//		return !stereo ? (mstudioflexcontroller_t *)( (char *)this + szindex0 ) : NULL;
		//	}
		//	inline char * const	pszControllerName( void ) const { return !stereo ? pController()->pszName() : NULL; }
		//	inline int			controllerIndex( const CStudioHdr &cStudioHdr ) const;

		//	inline const mstudioflexcontroller_t *pLeftController( void ) const
		//	{
		//		return stereo ? (mstudioflexcontroller_t *)( (char *)this + szindex0 ) : NULL;
		//	}
		//	inline char * const	pszLeftName( void ) const { return stereo ? pLeftController()->pszName() : NULL; }
		//	inline int			leftIndex( const CStudioHdr &cStudioHdr ) const;

		//	inline const mstudioflexcontroller_t *pRightController( void ) const
		//	{
		//		return stereo ? (mstudioflexcontroller_t *)( (char *)this + szindex1 ): NULL;
		//	}
		//	inline char * const	pszRightName( void ) const { return stereo ? pRightController()->pszName() : NULL; }
		//	inline int			rightIndex( const CStudioHdr &cStudioHdr ) const;

		//	inline const mstudioflexcontroller_t *pNWayValueController( void ) const
		//	{
		//		return remaptype == FLEXCONTROLLER_REMAP_NWAY ? (mstudioflexcontroller_t *)( (char *)this + szindex2 ) : NULL;
		//	}
		//	inline char * const	pszNWayValueName( void ) const { return remaptype == FLEXCONTROLLER_REMAP_NWAY ? pNWayValueController()->pszName() : NULL; }
		//	inline int			nWayValueIndex( const CStudioHdr &cStudioHdr ) const;

		//	// Number of controllers this ui description contains, 1, 2 or 3
		//	inline int			Count() const { return ( stereo ? 2 : 1 ) + ( remaptype == FLEXCONTROLLER_REMAP_NWAY ? 1 : 0 ); }
		//	inline const mstudioflexcontroller_t *pController( int index ) const;

		//	unsigned char		remaptype;	// See the FlexControllerRemapType_t enum
		//	bool				stereo;		// Is this a stereo control?
		//	byte				unused[2];
		//};


		public int nameOffset;
		public int config0;
		public int config1;
		public int config2;
		public byte remapType;
		public byte controlIsStereo;
		public byte[] unused = new byte[2];


		public string theName;

	}

}