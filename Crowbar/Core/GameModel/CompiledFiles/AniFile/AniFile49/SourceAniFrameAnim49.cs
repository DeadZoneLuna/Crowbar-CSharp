﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceAniFrameAnim49
	{

		//FROM: AlienSwarm_source\src\public\studio.h
		//struct mstudio_frame_anim_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();

		//	inline byte		*pBoneFlags( void ) const { return (((byte *)this) + sizeof( struct mstudio_frame_anim_t )); };

		//	int				constantsoffset;
		//	inline byte		*pConstantData( void ) const { return (((byte *)this) + constantsoffset); };

		//	int				frameoffset;
		//	int 			framelength;
		//	inline byte		*pFrameData( int iFrame  ) const { return (((byte *)this) + frameoffset + iFrame * framelength); };

		//	int				unused[3];
		//};


		public int constantsOffset;
		public int frameOffset;
		public int frameLength;
		public int[] unused = new int[3];


		//NOTE: These are indexed by global bone index.
		public List<byte> theBoneFlags;
		public List<BoneConstantInfo49> theBoneConstantInfos;
		//NOTE: This is indexed by frame index and global bone index.
		public List<List<BoneFrameDataInfo49>> theBoneFrameDataInfos;

		//FROM: AlienSwarm_source\src\public\studio.h
		// Values for the field, theBoneFlags:
		//#define STUDIO_FRAME_RAWPOS		0x01 // Vector48 in constants
		//#define STUDIO_FRAME_RAWROT		0x02 // Quaternion48 in constants
		//#define STUDIO_FRAME_ANIMPOS	0x04 // Vector48 in framedata
		//#define STUDIO_FRAME_ANIMROT	0x08 // Quaternion48 in framedata
		//#define STUDIO_FRAME_FULLANIMPOS	0x10 // Vector in framedata
		public const int STUDIO_FRAME_RAWPOS = 0x1;
		public const int STUDIO_FRAME_RAWROT = 0x2;
		public const int STUDIO_FRAME_ANIMPOS = 0x4;
		public const int STUDIO_FRAME_ANIMROT = 0x8;
		public const int STUDIO_FRAME_FULLANIMPOS = 0x10;

		//Public Const STUDIO_FRAME_UNKNOWN01 As Integer = &H40	' Seems to be 6 rotation bytes in constants based on tests. New format that is not Quaternion48. Maybe Quaternion48Smallest3?
		//Public Const STUDIO_FRAME_UNKNOWN02 As Integer = &H80	' Seems to be 6 rotation bytes in framedata based on tests. New format that is not Quaternion48. Maybe Quaternion48Smallest3?
		//FROM: Kerry at Valve via Splinks on 24-Apr-2017
		//#define STUDIO_FRAME_CONST_ROT2   0x40 // Quaternion48S in constants
		//#define STUDIO_FRAME_ANIM_ROT2    0x80 // Quaternion48S in framedata
		public const int STUDIO_FRAME_CONST_ROT2 = 0x40;
		public const int STUDIO_FRAME_ANIM_ROT2 = 0x80;


	}

}