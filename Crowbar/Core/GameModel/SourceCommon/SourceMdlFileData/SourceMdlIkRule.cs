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
	public class SourceMdlIkRule
	{

		//FROM: SourceEngine2006+_source\public\studio.h
		//struct mstudioikrule_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int			index;

		//	int			type;
		//	int			chain;

		//	int			bone;

		//	int			slot;	// iktarget slot.  Usually same as chain.
		//	float		height;
		//	float		radius;
		//	float		floor;
		//	Vector		pos;
		//	Quaternion	q;

		//	int			compressedikerrorindex;
		//	inline mstudiocompressedikerror_t *pCompressedError() const { return (mstudiocompressedikerror_t *)(((byte *)this) + compressedikerrorindex); };
		//	int			unused2;

		//	int			iStart;
		//	int			ikerrorindex;
		//	inline mstudioikerror_t *pError( int i ) const { return  (ikerrorindex) ? (mstudioikerror_t *)(((byte *)this) + ikerrorindex) + (i - iStart) : NULL; };

		//	float		start;	// beginning of influence
		//	float		peak;	// start of full influence
		//	float		tail;	// end of full influence
		//	float		end;	// end of all influence

		//	float		unused3;	// 
		//	float		contact;	// frame footstep makes ground concact
		//	float		drop;		// how far down the foot should drop when reaching for IK
		//	float		top;		// top of the foot box

		//	int			unused6;
		//	int			unused7;
		//	int			unused8;

		//	int			szattachmentindex;		// name of world attachment
		//	inline char * const pszAttachment( void ) const { return ((char *)this) + szattachmentindex; }

		//	int			unused[7];

		//	mstudioikrule_t() {}

		//private:
		//	// No copy constructors allowed
		//	mstudioikrule_t(const mstudioikrule_t& vOther);
		//};



		public int index;
		public int type;
		public int chain;
		public int bone;

		public int slot;
		public double height;
		public double radius;
		public double floor;
		public SourceVector pos;
		public SourceQuaternion q;

		public int compressedIkErrorOffset;
		public int unused2;
		public int ikErrorIndexStart;
		public int ikErrorOffset;

		public double influenceStart;
		public double influencePeak;
		public double influenceTail;
		public double influenceEnd;

		public double unused3;
		public double contact;
		public double drop;
		public double top;

		public int unused6;
		public int unused7;
		public int unused8;

		public int attachmentNameOffset;

		public int[] unused = new int[7];



		public string theAttachmentName;
		public SourceMdlCompressedIkError theCompressedIkError;



		// For the 'type' field:
		//FROM: se2007_src\src_main\public\studio.h
		//#define IK_SELF 1
		//#define IK_WORLD 2
		//#define IK_GROUND 3
		//#define IK_RELEASE 4
		//#define IK_ATTACHMENT 5
		//#define IK_UNLATCH 6
		public const int IK_SELF = 1;
		public const int IK_WORLD = 2;
		public const int IK_GROUND = 3;
		public const int IK_RELEASE = 4;
		public const int IK_ATTACHMENT = 5;
		public const int IK_UNLATCH = 6;

	}

}