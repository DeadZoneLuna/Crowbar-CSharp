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
	public class SourceMdlAnimationDesc49 : SourceMdlAnimationDescBase
	{
		public SourceMdlAnimationDesc49()
		{
			this.theLinkedSequences = new List<SourceMdlSequenceDesc>();
			//Me.theCorrectiveSubtractAnimationOptionIsUsed = False
			this.theCorrectiveAnimationName = "";
		}

		//FROM: AlienSwarm_source\src\public\studio.h
		//struct mstudioanimdesc_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					baseptr;
		//	inline studiohdr_t	*pStudiohdr( void ) const { return (studiohdr_t *)(((byte *)this) + baseptr); }
		//
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//
		//	float				fps;		// frames per second	
		//	int					flags;		// looping/non-looping flags
		//
		//	int					numframes;
		//
		//	// piecewise movement
		//	int					nummovements;
		//	int					movementindex;
		//	inline mstudiomovement_t * const pMovement( int i ) const { return (mstudiomovement_t *)(((byte *)this) + movementindex) + i; };
		//
		//	int					ikrulezeroframeindex;
		//	mstudioikrulezeroframe_t *pIKRuleZeroFrame( int i ) const { if (ikrulezeroframeindex) return (mstudioikrulezeroframe_t *)(((byte *)this) + ikrulezeroframeindex) + i; else return NULL; };
		//
		//	int					unused1[5];			// remove as appropriate (and zero if loading older versions)	
		//
		//	int					animblock;
		//	int					animindex;	 // non-zero when anim data isn't in sections
		//	byte *pAnimBlock( int block, int index ) const; // returns pointer to a specific anim block (local or external)
		//	byte *pAnim( int *piFrame, float &flStall ) const; // returns pointer to data and new frame index
		//	byte *pAnim( int *piFrame ) const; // returns pointer to data and new frame index
		//
		//	int					numikrules;
		//	int					ikruleindex;	// non-zero when IK rule is stored in the mdl
		//	int					animblockikruleindex; // non-zero when IK data is stored in animblock file
		//	mstudioikrule_t *pIKRule( int i ) const;
		//
		//	int					numlocalhierarchy;
		//	int					localhierarchyindex;
		//	mstudiolocalhierarchy_t *pHierarchy( int i ) const;
		//
		//	int					sectionindex;
		//	int					sectionframes; // number of frames used in each fast lookup section, zero if not used
		//	inline mstudioanimsections_t * const pSection( int i ) const { return (mstudioanimsections_t *)(((byte *)this) + sectionindex) + i; }
		//
		//	short				zeroframespan;	// frames per span
		//	short				zeroframecount; // number of spans
		//	int					zeroframeindex;
		//	byte				*pZeroFrameData( ) const { if (zeroframeindex) return (((byte *)this) + zeroframeindex); else return NULL; };
		//	mutable float		zeroframestalltime;		// saved during read stalls
		//
		//	mstudioanimdesc_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudioanimdesc_t(const mstudioanimdesc_t& vOther);
		//};

		//NOTE: Size of this struct: 100 bytes.

		//	int					baseptr;
		//	inline studiohdr_t	*pStudiohdr( void ) const { return (studiohdr_t *)(((byte *)this) + baseptr); }
		public int baseHeaderOffset;

		//	int					sznameindex;
		public int nameOffset;

		//	float				fps;		// frames per second	
		public float fps;
		//	int					flags;		// looping/non-looping flags
		public int flags;

		//	int					numframes;
		public int frameCount;

		//	// piecewise movement
		//	int					nummovements;
		public int movementCount;
		//	int					movementindex;
		public int movementOffset;
		//	inline mstudiomovement_t * const pMovement( int i ) const { return (mstudiomovement_t *)(((byte *)this) + movementindex) + i; };

		//	int					ikrulezeroframeindex;
		//	mstudioikrulezeroframe_t *pIKRuleZeroFrame( int i ) const { if (ikrulezeroframeindex) return (mstudioikrulezeroframe_t *)(((byte *)this) + ikrulezeroframeindex) + i; else return NULL; };
		public int ikRuleZeroFrameOffset_VERSION49;

		//	int					unused1[5];			// remove as appropriate (and zero if loading older versions)	
		public int[] unused1 = new int[5];

		//	int					animblock;
		public int animBlock;
		//	int					animindex;	 // non-zero when anim data isn't in sections
		public int animOffset;
		//	mstudioanim_t *pAnimBlock( int block, int index ) const; // returns pointer to a specific anim block (local or external)
		//	mstudioanim_t *pAnim( int *piFrame, float &flStall ) const; // returns pointer to data and new frame index
		//	mstudioanim_t *pAnim( int *piFrame ) const; // returns pointer to data and new frame index

		//	int					numikrules;
		public int ikRuleCount;
		//	int					ikruleindex;	// non-zero when IK data is stored in the mdl
		public int ikRuleOffset;
		//	int					animblockikruleindex; // non-zero when IK data is stored in animblock file
		public int animblockIkRuleOffset;
		//	mstudioikrule_t *pIKRule( int i ) const;

		//	int					numlocalhierarchy;
		public int localHierarchyCount;
		//	int					localhierarchyindex;
		public int localHierarchyOffset;
		//	mstudiolocalhierarchy_t *pHierarchy( int i ) const;

		//	int					sectionindex;
		public int sectionOffset;
		//	int					sectionframes; // number of frames used in each fast lookup section, zero if not used
		public int sectionFrameCount;
		//	inline mstudioanimsections_t * const pSection( int i ) const { return (mstudioanimsections_t *)(((byte *)this) + sectionindex) + i; }

		//	short				zeroframespan;	// frames per span
		public short spanFrameCount;
		//	short				zeroframecount; // number of spans
		public short spanCount;
		//	int					zeroframeindex;
		public int spanOffset;
		//	byte				*pZeroFrameData( ) const { if (zeroframeindex) return (((byte *)this) + zeroframeindex); else return NULL; };
		//	mutable float		zeroframestalltime;		// saved during read stalls
		public float spanStallTime;


		// Moved to SourceMdlAnimationDescBase
		//'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//Public theName As String

		public List<List<SourceMdlAnimation>> theSectionsOfAnimations;
		public List<SourceAniFrameAnim49> theSectionsOfFrameAnim;
		public List<SourceMdlIkRule> theIkRules;
		public List<SourceMdlAnimationSection> theSections;
		public List<SourceMdlMovement> theMovements;
		public List<SourceMdlLocalHierarchy> theLocalHierarchies;

		public bool theAnimIsLinkedToSequence;
		public List<SourceMdlSequenceDesc> theLinkedSequences;
		public long theOffsetStart;
		//Public theCorrectiveSubtractAnimationOptionIsUsed As Boolean
		public string theCorrectiveAnimationName;



		// Values for the field, flags:
		//FROM: SourceEngineXXXX_source\public\studio.h
		//// sequence and autolayer flags
		//#define STUDIO_LOOPING	0x0001		// ending frame should be the same as the starting frame
		//#define STUDIO_SNAP		0x0002		// do not interpolate between previous animation and this one
		//#define STUDIO_DELTA	0x0004		// this sequence "adds" to the base sequences, not slerp blends
		//#define STUDIO_AUTOPLAY	0x0008		// temporary flag that forces the sequence to always play
		//#define STUDIO_POST		0x0010		// 
		//#define STUDIO_ALLZEROS	0x0020		// this animation/sequence has no real animation data
		////						0x0040
		//#define STUDIO_CYCLEPOSE 0x0080		// cycle index is taken from a pose parameter index
		//#define STUDIO_REALTIME	0x0100		// cycle index is taken from a real-time clock, not the animations cycle index
		//#define STUDIO_LOCAL	0x0200		// sequence has a local context sequence
		//#define STUDIO_HIDDEN	0x0400		// don't show in default selection views
		//#define STUDIO_OVERRIDE	0x0800		// a forward declared sequence (empty)
		//#define STUDIO_ACTIVITY	0x1000		// Has been updated at runtime to activity index
		//#define STUDIO_EVENT	0x2000		// Has been updated at runtime to event index
		//#define STUDIO_WORLD	0x4000		// sequence blends in worldspace
		//------
		//VERSION 49
		//FROM: AlienSwarm_source\src\public\studio.h
		//      Adds these to the above.
		//#define STUDIO_FRAMEANIM 0x0040		// animation is encoded as by frame x bone instead of RLE bone x frame
		//#define STUDIO_NOFORCELOOP 0x8000	// do not force the animation loop
		//#define STUDIO_EVENT_CLIENT 0x10000	// Has been updated at runtime to event index on client


		public const int STUDIO_LOOPING = 0x1;
		public const int STUDIO_SNAP = 0x2;
		public const int STUDIO_DELTA = 0x4;
		public const int STUDIO_AUTOPLAY = 0x8;
		public const int STUDIO_POST = 0x10;
		public const int STUDIO_ALLZEROS = 0x20;
		// &H40
		public const int STUDIO_CYCLEPOSE = 0x80;
		public const int STUDIO_REALTIME = 0x100;
		//NOTE: STUDIO_LOCAL used internally by studiomdl and not needed by Crowbar.
		public const int STUDIO_LOCAL = 0x200;
		public const int STUDIO_HIDDEN = 0x400;
		public const int STUDIO_OVERRIDE = 0x800;
		//NOTE: STUDIO_ACTIVITY used internally by game engine and not needed by Crowbar.
		public const int STUDIO_ACTIVITY = 0x1000;
		//NOTE: STUDIO_EVENT used internally by game engine and not needed by Crowbar.
		public const int STUDIO_EVENT = 0x2000;
		public const int STUDIO_WORLD = 0x4000;
		//------
		//VERSION 49
		public const int STUDIO_FRAMEANIM = 0x40;
		public const int STUDIO_NOFORCELOOP = 0x8000;
		public const int STUDIO_EVENT_CLIENT = 0x10000;

	}

}