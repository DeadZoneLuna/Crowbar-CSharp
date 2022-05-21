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
	public class SourceMdlSequenceDesc : SourceMdlSequenceDescBase
	{
		public SourceMdlSequenceDesc()
		{
			theWeightListIndex = -1;
			//Me.theCorrectiveSubtractAnimationOptionIsUsed = False
			theCorrectiveAnimationName = null;
		}

		//// sequence descriptions
		//struct mstudioseqdesc_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					baseptr;
		//	inline studiohdr_t	*pStudiohdr( void ) const { return (studiohdr_t *)(((byte *)this) + baseptr); }

		//	int					szlabelindex;
		//	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }

		//	int					szactivitynameindex;
		//	inline char * const pszActivityName( void ) const { return ((char *)this) + szactivitynameindex; }

		//	int					flags;		// looping/non-looping flags

		//	int					activity;	// initialized at loadtime to game DLL values
		//	int					actweight;

		//	int					numevents;
		//	int					eventindex;
		//	inline mstudioevent_t *pEvent( int i ) const { Assert( i >= 0 && i < numevents); return (mstudioevent_t *)(((byte *)this) + eventindex) + i; };

		//	Vector				bbmin;		// per sequence bounding box
		//	Vector				bbmax;		

		//	int					numblends;

		//	// Index into array of shorts which is groupsize[0] x groupsize[1] in length
		//	int					animindexindex;

		//	inline int			anim( int x, int y ) const
		//	{
		//		if ( x >= groupsize[0] )
		//		{
		//			x = groupsize[0] - 1;
		//		}

		//		if ( y >= groupsize[1] )
		//		{
		//			y = groupsize[ 1 ] - 1;
		//		}

		//		int offset = y * groupsize[0] + x;
		//		short *blends = (short *)(((byte *)this) + animindexindex);
		//		int value = (int)blends[ offset ];
		//		return value;
		//	}

		//	int					movementindex;	// [blend] float array for blended movement
		//	int					groupsize[2];
		//	int					paramindex[2];	// X, Y, Z, XR, YR, ZR
		//	float				paramstart[2];	// local (0..1) starting value
		//	float				paramend[2];	// local (0..1) ending value
		//	int					paramparent;

		//	float				fadeintime;		// ideal cross fate in time (0.2 default)
		//	float				fadeouttime;	// ideal cross fade out time (0.2 default)

		//	int					localentrynode;		// transition node at entry
		//	int					localexitnode;		// transition node at exit
		//	int					nodeflags;		// transition rules

		//	float				entryphase;		// used to match entry gait
		//	float				exitphase;		// used to match exit gait

		//	float				lastframe;		// frame that should generation EndOfSequence

		//	int					nextseq;		// auto advancing sequences
		//	int					pose;			// index of delta animation between end and nextseq

		//	int					numikrules;

		//	int					numautolayers;	//
		//	int					autolayerindex;
		//	inline mstudioautolayer_t *pAutolayer( int i ) const { Assert( i >= 0 && i < numautolayers); return (mstudioautolayer_t *)(((byte *)this) + autolayerindex) + i; };

		//	int					weightlistindex;
		//	inline float		*pBoneweight( int i ) const { return ((float *)(((byte *)this) + weightlistindex) + i); };
		//	inline float		weight( int i ) const { return *(pBoneweight( i)); };

		//	// FIXME: make this 2D instead of 2x1D arrays
		//	int					posekeyindex;
		//	float				*pPoseKey( int iParam, int iAnim ) const { return (float *)(((byte *)this) + posekeyindex) + iParam * groupsize[0] + iAnim; }
		//	float				poseKey( int iParam, int iAnim ) const { return *(pPoseKey( iParam, iAnim )); }

		//	int					numiklocks;
		//	int					iklockindex;
		//	inline mstudioiklock_t *pIKLock( int i ) const { Assert( i >= 0 && i < numiklocks); return (mstudioiklock_t *)(((byte *)this) + iklockindex) + i; };

		//	// Key values
		//	int					keyvalueindex;
		//	int					keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }

		//	int					cycleposeindex;		// index of pose parameter to use as cycle index

		//	int					unused[7];		// remove/add as appropriate (grow back to 8 ints on version change!)
		//======
		//FROM: VERSION 49
		//	int					activitymodifierindex;
		//	int					numactivitymodifiers;
		//	inline mstudioactivitymodifier_t *pActivityModifier( int i ) const { Assert( i >= 0 && i < numactivitymodifiers); return activitymodifierindex != 0 ? (mstudioactivitymodifier_t *)(((byte *)this) + activitymodifierindex) + i : NULL; };
		//	int					unused[5];		// remove/add as appropriate (grow back to 8 ints on version change!)

		//	mstudioseqdesc_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudioseqdesc_t(const mstudioseqdesc_t& vOther);
		//};

		//NOTE: Size of this class is 212 bytes.

		//	int					baseptr;
		//	inline studiohdr_t	*pStudiohdr( void ) const { return (studiohdr_t *)(((byte *)this) + baseptr); }
		public int baseHeaderOffset;

		//	int					szlabelindex;
		//	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
		public int nameOffset;

		//	int					szactivitynameindex;
		//	inline char * const pszActivityName( void ) const { return ((char *)this) + szactivitynameindex; }
		public int activityNameOffset;

		//	int					flags;		// looping/non-looping flags
		public int flags;

		//	int					activity;	// initialized at loadtime to game DLL values
		public int activity;
		//	int					actweight;
		public int activityWeight;

		//	int					numevents;
		public int eventCount;
		//	int					eventindex;
		//	inline mstudioevent_t *pEvent( int i ) const { Assert( i >= 0 && i < numevents); return (mstudioevent_t *)(((byte *)this) + eventindex) + i; };
		public int eventOffset;

		//	Vector				bbmin;		// per sequence bounding box
		public SourceVector bbMin = new SourceVector();
		//	Vector				bbmax;		
		public SourceVector bbMax = new SourceVector();

		//	int					numblends;
		public int blendCount;

		//	// Index into array of shorts which is groupsize[0] x groupsize[1] in length
		//	int					animindexindex;
		public int animIndexOffset;

		//	int					movementindex;	// [blend] float array for blended movement
		public int movementIndex;
		//	int					groupsize[2];
		public int[] groupSize = new int[2];
		//	int					paramindex[2];	// X, Y, Z, XR, YR, ZR
		public int[] paramIndex = new int[2];
		//	float				paramstart[2];	// local (0..1) starting value
		public float[] paramStart = new float[2];
		//	float				paramend[2];	// local (0..1) ending value
		public float[] paramEnd = new float[2];
		//	int					paramparent;
		public int paramParent;

		//	float				fadeintime;		// ideal cross fate in time (0.2 default)
		public float fadeInTime;
		//	float				fadeouttime;	// ideal cross fade out time (0.2 default)
		public float fadeOutTime;

		//	int					localentrynode;		// transition node at entry
		public int localEntryNodeIndex;
		//	int					localexitnode;		// transition node at exit
		public int localExitNodeIndex;
		//	int					nodeflags;		// transition rules
		public int nodeFlags;

		//	float				entryphase;		// used to match entry gait
		public float entryPhase;
		//	float				exitphase;		// used to match exit gait
		public float exitPhase;

		//	float				lastframe;		// frame that should generation EndOfSequence
		public float lastFrame;

		//	int					nextseq;		// auto advancing sequences
		public int nextSeq;
		//	int					pose;			// index of delta animation between end and nextseq
		public int pose;

		//	int					numikrules;
		public int ikRuleCount;

		//	int					numautolayers;	//
		public int autoLayerCount;
		//	int					autolayerindex;
		//	inline mstudioautolayer_t *pAutolayer( int i ) const { Assert( i >= 0 && i < numautolayers); return (mstudioautolayer_t *)(((byte *)this) + autolayerindex) + i; };
		public int autoLayerOffset;

		//	int					weightlistindex;
		public int weightOffset;
		//	inline float		*pBoneweight( int i ) const { return ((float *)(((byte *)this) + weightlistindex) + i); };

		//	// FIXME: make this 2D instead of 2x1D arrays
		//	int					posekeyindex;
		public int poseKeyOffset;
		//	float				*pPoseKey( int iParam, int iAnim ) const { return (float *)(((byte *)this) + posekeyindex) + iParam * groupsize[0] + iAnim; }

		//	int					numiklocks;
		public int ikLockCount;
		//	int					iklockindex;
		//	inline mstudioiklock_t *pIKLock( int i ) const { Assert( i >= 0 && i < numiklocks); return (mstudioiklock_t *)(((byte *)this) + iklockindex) + i; };
		public int ikLockOffset;

		//	// Key values
		//	int					keyvalueindex;
		public int keyValueOffset;
		//	int					keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
		public int keyValueSize;

		//	int					cycleposeindex;		// index of pose parameter to use as cycle index
		public int cyclePoseIndex;

		//	int					unused[7];		// remove/add as appropriate (grow back to 8 ints on version change!)
		//======
		// Some Version 48 (such as Team Fortress 2 and Source SDK Base Multiplayer 2013, but not Garry's Mod)
		//FROM: VERSION 49
		//	int					activitymodifierindex;
		//	int					numactivitymodifiers;
		//	inline mstudioactivitymodifier_t *pActivityModifier( int i ) const { Assert( i >= 0 && i < numactivitymodifiers); return activitymodifierindex != 0 ? (mstudioactivitymodifier_t *)(((byte *)this) + activitymodifierindex) + i : NULL; };
		//	int					unused[5];		// remove/add as appropriate (grow back to 8 ints on version change!)
		public int activityModifierOffset;
		public int activityModifierCount;
		public int[] unused = new int[7];


		public string theName;
		public string theActivityName;
		public List<double> thePoseKeys;
		public List<SourceMdlEvent> theEvents;
		public List<SourceMdlAutoLayer> theAutoLayers;
		public List<SourceMdlIkLock> theIkLocks;
		//NOTE: In the file, a bone weight is a 32-bit float, i.e. a Single, but is stored as Double for better writing to file.
		public List<double> theBoneWeights;
		public int theWeightListIndex;
		public List<short> theAnimDescIndexes;
		public string theKeyValues;
		public List<SourceMdlActivityModifier> theActivityModifiers;


		public bool theBoneWeightsAreDefault;
		//Public theCorrectiveSubtractAnimationOptionIsUsed As Boolean
		public string theCorrectiveAnimationName;

	}

}