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
	public class SourceMdlSequenceDesc37
	{

		//struct mstudioseqdesc_t
		//{
		//	int	szlabelindex;
		//	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
		//
		//	int	szactivitynameindex;
		//	inline char * const pszActivityName( void ) const { return ((char *)this) + szactivitynameindex; }
		//
		//	int	flags;		// looping/non-looping flags
		//
		//	int	activity;	// initialized at loadtime to game DLL values
		//	int	actweight;
		//
		//	int	numevents;
		//	int	eventindex;
		//	inline mstudioevent_t *pEvent( int i ) const { return (mstudioevent_t *)(((byte *)this) + eventindex) + i; };
		//
		//	Vector	bbmin;		// per sequence bounding box
		//	Vector	bbmax;		
		//
		//	//-------------------------------------------------------------------------
		//	// Purpose: returns a model animation from the sequence group size and
		//	//          blend index
		//	// Note: this replaces GetAnimValue() that was previously in bone_setup
		//	// Note: this also acts as a SetAnimValue() as it returns a reference to
		//	//       the anim value in question
		//	//-------------------------------------------------------------------------
		//	inline unsigned short& pAnimValue( int nIndex0, int nIndex1 ) const
		//	{
		//		// Clamp indexes
		//		if ( nIndex0 >= groupsize[0] )
		//			nIndex0 = groupsize[0] - 1;
		//
		//		if ( nIndex1 >= groupsize[1] )
		//			nIndex1 = groupsize[1] - 1;
		//		
		//		return *pBlend(nIndex1 * groupsize[0] + nIndex0);
		//	}
		//
		//	int	numblends;
		//
		//	int blendindex;
		//	inline unsigned short *pBlend( int i ) const { return (unsigned short *)(((byte *)this) + blendindex) + i; };
		//
		//	int seqgroup; // sequence group for demand loading
		//
		//	int	groupsize[2];
		//	int	paramindex[2];	// X, Y, Z, XR, YR, ZR
		//	float	paramstart[2];	// local (0..1) starting value
		//	float	paramend[2];	// local (0..1) ending value
		//	int	paramparent;
		//
		//	float	fadeintime;	// ideal cross fate in time (0.2 default)
		//	float	fadeouttime;	// ideal cross fade out time (0.2 default)
		//
		//	int	entrynode;	// transition node at entry
		//	int	exitnode;	// transition node at exit
		//	int	nodeflags;	// transition rules
		//
		//	float	entryphase;	// used to match entry gait
		//	float	exitphase;	// used to match exit gait
		//	
		//	float	lastframe;	// frame that should generation EndOfSequence
		//
		//	int	nextseq;	// auto advancing sequences
		//	int	pose;		// index of delta animation between end and nextseq
		//
		//	int	numikrules;
		//
		//	int	numautolayers;
		//	int	autolayerindex;
		//	inline mstudioautolayer_t *pAutolayer( int i ) const { return (mstudioautolayer_t *)(((byte *)this) + autolayerindex) + i; };
		//
		//	int	weightlistindex;
		//	float	*pBoneweight( int i ) const { return ((float *)(((byte *)this) + weightlistindex) + i); };
		//	float	weight( int i ) const { return *(pBoneweight( i)); };
		//
		//	int	posekeyindex;
		//	float				*pPoseKey( int iParam, int iAnim ) const { return (float *)(((byte *)this) + posekeyindex) + iParam * groupsize[0] + iAnim; }
		//	float				poseKey( int iParam, int iAnim ) const { return *(pPoseKey( iParam, iAnim )); }
		//
		//	int	numiklocks;
		//	int	iklockindex;
		//	inline mstudioiklock_t *pIKLock( int i ) const { return (mstudioiklock_t *)(((byte *)this) + iklockindex) + i; };
		//
		//	// Key values
		//	int	keyvalueindex;
		//	int	keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
		//	
		//	int	unused[3];		// remove/add as appropriate
		//};

		public int nameOffset;
		public int activityNameOffset;
		public int flags;
		public int activity;
		public int activityWeight;
		public int eventCount;
		public int eventOffset;

		public SourceVector bbMin = new SourceVector();
		public SourceVector bbMax = new SourceVector();

		public int blendCount;
		public int blendOffset;

		public int sequenceGroup;

		public int[] groupSize = new int[2];
		public int[] paramIndex = new int[2];
		public float[] paramStart = new float[2];
		public float[] paramEnd = new float[2];
		public int paramParent;

		public float fadeInTime;
		public float fadeOutTime;

		public int entryNodeIndex;
		public int exitNodeIndex;
		public int nodeFlags;

		public float entryPhase;
		public float exitPhase;
		public float lastFrame;

		public int nextSeq;
		public int pose;

		public int ikRuleCount;
		public int autoLayerCount;
		public int autoLayerOffset;
		public int weightOffset;
		public int poseKeyOffset;

		public int ikLockCount;
		public int ikLockOffset;
		public int keyValueOffset;
		public int keyValueSize;

		public int[] unused = new int[3];

		public string theActivityName;
		public List<short> theAnimDescIndexes;
		public List<SourceMdlAutoLayer37> theAutoLayers;
		public List<double> theBoneWeights;
		public List<SourceMdlEvent37> theEvents;
		public List<SourceMdlIkLock37> theIkLocks;
		public string theKeyValues;
		public string theName;
		public List<double> thePoseKeys;
		public int theWeightListIndex;

		public bool theBoneWeightsAreDefault;

	}

}