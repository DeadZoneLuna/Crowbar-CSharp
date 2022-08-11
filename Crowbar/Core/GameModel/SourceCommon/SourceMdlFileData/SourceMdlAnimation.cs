using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Crowbar
{
	public class SourceMdlAnimation
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//// per bone per animation DOF and weight pointers
		//struct mstudioanim_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	byte				bone;
		//	byte				flags;		// weighing options

		//	// valid for animating data only
		//	inline byte				*pData( void ) const { return (((byte *)this) + sizeof( struct mstudioanim_t )); };
		//	inline mstudioanim_valueptr_t	*pRotV( void ) const { return (mstudioanim_valueptr_t *)(pData()); };
		//	inline mstudioanim_valueptr_t	*pPosV( void ) const { return (mstudioanim_valueptr_t *)(pData()) + ((flags & STUDIO_ANIM_ANIMROT) != 0); };

		//	// valid if animation unvaring over timeline
		//	inline Quaternion48		*pQuat48( void ) const { return (Quaternion48 *)(pData()); };
		//	inline Quaternion64		*pQuat64( void ) const { return (Quaternion64 *)(pData()); };
		//	inline Vector48			*pPos( void ) const { return (Vector48 *)(pData() + ((flags & STUDIO_ANIM_RAWROT) != 0) * sizeof( *pQuat48() ) + ((flags & STUDIO_ANIM_RAWROT2) != 0) * sizeof( *pQuat64() ) ); };

		//	short				nextoffset;
		//	inline mstudioanim_t	*pNext( void ) const { if (nextoffset != 0) return  (mstudioanim_t *)(((byte *)this) + nextoffset); else return NULL; };
		//};


		//	byte				bone;
		public byte boneIndex;
		//	byte				flags;		// weighing options
		public byte flags;
		//	short				nextoffset;
		public short nextSourceMdlAnimationOffset;


		// Values for the field, flags:
		//#define STUDIO_ANIM_RAWPOS	0x01 // Vector48
		//#define STUDIO_ANIM_RAWROT	0x02 // Quaternion48
		//#define STUDIO_ANIM_ANIMPOS	0x04 // mstudioanim_valueptr_t
		//#define STUDIO_ANIM_ANIMROT	0x08 // mstudioanim_valueptr_t
		//#define STUDIO_ANIM_DELTA	0x10
		//#define STUDIO_ANIM_RAWROT2	0x20 // Quaternion64
		public const int STUDIO_ANIM_RAWPOS = 0x1;
		public const int STUDIO_ANIM_RAWROT = 0x2;
		public const int STUDIO_ANIM_ANIMPOS = 0x4;
		public const int STUDIO_ANIM_ANIMROT = 0x8;
		public const int STUDIO_ANIM_DELTA = 0x10;
		public const int STUDIO_ANIM_RAWROT2 = 0x20;


		// Do not use union, because it will have to rely on size of a .NET Framework data type.
		//<StructLayout(LayoutKind.Explicit)> _
		//Public Structure theData
		//	<FieldOffset(0)> Public theRotV As SourceMdlAnimationValuePointer
		//	<FieldOffset(0)> Public thePosV As SourceMdlAnimationValuePointer
		//	<FieldOffset(0)> Public theQuat48 As SourceQuaternion48
		//	<FieldOffset(0)> Public theQuat64 As SourceQuaternion64
		//	<FieldOffset(8)> Public thePosV2 As SourceMdlAnimationValuePointer
		//End Structure


		// if (flags & STUDIO_ANIM_ANIMROT) <> 0 then this field is filled
		public SourceMdlAnimationValuePointer theRotV;
		// if (flags & STUDIO_ANIM_ANIMPOS) <> 0 then this field is filled
		public SourceMdlAnimationValuePointer thePosV;
		// if (flags & STUDIO_ANIM_RAWROT) <> 0 then this field is filled
		public SourceQuaternion48bits theRot48bits;
		// if (flags & STUDIO_ANIM_RAWROT2) <> 0 then this field is filled
		public SourceQuaternion64bits theRot64bits;
		// if (flags & STUDIO_ANIM_RAWPOS) <> 0 then this field is filled
		public SourceVector48bits thePos;

	}

}