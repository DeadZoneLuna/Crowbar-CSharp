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
	public class SourceMdlSequenceDesc2531 : SourceMdlSequenceDescBase
	{
		public SourceMdlSequenceDesc2531()
		{
			//	short				anim[MAXSTUDIOBLENDS][MAXSTUDIOBLENDS];	// f64: 16x16x2 = 512 bytes each anim a short
			anim = new List<List<short>>(SourceModule2531.MAXSTUDIOBLENDS);
			for (int rowIndex = 0; rowIndex < SourceModule2531.MAXSTUDIOBLENDS; rowIndex++)
			{
				List<short> animRow = new List<short>(SourceModule2531.MAXSTUDIOBLENDS);
				for (int columnIndex = 0; columnIndex < SourceModule2531.MAXSTUDIOBLENDS; columnIndex++)
				{
					animRow.Add(0);
				}
				anim.Add(animRow);
			}
		}

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudioseqdesc_t
		//{
		//	int					szlabelindex;
		//	inline char * const pszLabel( void ) const { return ((char *)this) + szlabelindex; }
		//
		//	int					szactivitynameindex;
		//	inline char * const pszActivityName( void ) const { return ((char *)this) + szactivitynameindex; }
		//
		//	int					flags;		// looping/non-looping flags
		//
		//	int					activity;	// initialized at loadtime to game DLL values
		//	int					actweight;
		//
		//	int					numevents;
		//	int					eventindex;
		//	inline mstudioevent_t *pEvent( int i ) const { return (mstudioevent_t *)(((byte *)this) + eventindex) + i; };
		//
		//	Vector				bbmin;		// per sequence bounding box
		//	Vector				bbmax;		
		//
		//	int					numblends;
		//
		//	short				anim[MAXSTUDIOBLENDS][MAXSTUDIOBLENDS];	// f64: 16x16x2 = 512 bytes each anim a short
		//
		//	int					movementindex;	// [blend] float array for blended movement
		//	int					groupsize[2];
		//	int					paramindex[2];	// X, Y, Z, XR, YR, ZR
		//	float				paramstart[2];	// local (0..1) starting value
		//	float				paramend[2];	// local (0..1) ending value
		//	int					paramparent;
		//
		//	int					seqgroup;		// sequence group for demand loading
		//
		//	float				fadeintime;		// ideal cross fate in time (0.2 default)
		//	float				fadeouttime;	// ideal cross fade out time (0.2 default)
		//
		//	float				entrynode;	// f64: ~int, FIXME: this is a placeholder not transition node at entry
		//	int					exitnode;		// transition node at exit
		//	int					nodeflags;		// transition rules
		//
		//	float				entryphase;		// used to match entry gait
		//	float				exitphase;		// used to match exit gait
		//	
		//	float				lastframe;		// frame that should generation EndOfSequence
		//
		//	int					nextseq;		// auto advancing sequences
		//	int					pose;			// index of delta animation between end and nextseq
		//
		//	int					numikrules;
		//
		//	int					numautolayers;	//
		//	int					autolayerindex;
		//	inline mstudioautolayer_t *pAutolayer( int i ) const { return (mstudioautolayer_t *)(((byte *)this) + autolayerindex) + i; };
		//
		//	int					weightlistindex;
		//	float				*pBoneweight( int i ) const { return ((float *)(((byte *)this) + weightlistindex) + i); };
		//	float				weight( int i ) const { return *(pBoneweight( i)); };
		//
		//	int					posekeyindex;
		//	float				*pPoseKey( int iParam, int iAnim ) const { return (float *)(((byte *)this) + posekeyindex) + iParam * groupsize[0] + iAnim; }
		//	float				poseKey( int iParam, int iAnim ) const { return *(pPoseKey( iParam, iAnim )); }
		//
		//	Vector				bbmin2;		// f64: +
		//	Vector				bbmax2;		// f64: +
		//
		//	int					numiklocks;
		//	int					iklockindex;
		//	inline mstudioiklock_t *pIKLock( int i ) const { return (mstudioiklock_t *)(((byte *)this) + iklockindex) + i; };
		//
		//	// Key values
		//	int					keyvalueindex;
		//	int					keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
		//
		//	int unkindex1;	// f64: +
		//
		//	unsigned int unkunsigned1;	// f64: +
		//	unsigned int unkunsigned2;	// f64: +
		//
		//	int unkint[7];		// f64: +
		//	float unkfloat[3];	// f64: +
		//
		////	int					unused[3];	// f64: -	// remove/add as appropriate (grow back to 8 ints on version change!)
		//};

		public int nameOffset;
		public int activityNameOffset;
		public int flags;
		public int activityId;
		public int activityWeight;
		public int eventCount;
		public int eventOffset;

		public SourceVector bbMin = new SourceVector();
		public SourceVector bbMax = new SourceVector();

		public int blendCount;

		public List<List<short>> anim;

		public int movementIndex;
		public int[] groupSize = new int[2];
		public int[] paramIndex = new int[2];
		public double[] paramStart = new double[2];
		public double[] paramEnd = new double[2];
		public int paramParent;

		//	int					seqgroup;		// sequence group for demand loading
		public int sequenceGroup;

		public double fadeInTime;
		public double fadeOutTime;

		public double test; // same value as fadeInTime and fadeOutTime: 0.2xxxxx

		public int localEntryNodeIndex;
		public int localExitNodeIndex;
		public int nodeFlags;

		public double entryPhase;
		public double exitPhase;
		public double lastFrame;

		public int nextSeq;
		public int pose;

		public int ikRuleCount;
		public int autoLayerCount;
		public int autoLayerOffset;
		//Public weightOffset As Integer
		//Public poseKeyOffset As Integer
		public int unknown01;

		//'Vector				bbmin2;		// f64: +
		//'Vector				bbmax2;		// f64: +
		//Public bbMin2 As New SourceVector()
		//Public bbMax2 As New SourceVector()
		public double[] test02 = new double[6];
		public int test03;

		public int ikLockCount;
		public int ikLockOffset;
		//Public keyValueOffset As Integer
		//Public keyValueSize As Integer
		public int keyValueSize;
		public int keyValueOffset;

		//'int unkindex1;	// f64: +
		//Public unknown01 As Integer

		//unsigned int unkunsigned1;	// f64: +
		//unsigned int unkunsigned2;	// f64: +
		//Public unknown02 As UInt32
		//Public unknown03 As UInt32
		public double unknown02;
		public double unknown03;

		//int unkint[7];		// f64: +
		//float unkfloat[3];	// f64: +
		public int[] unknown04 = new int[7];
		public double[] unknown05 = new double[3];


		public string theName;
		public string theActivityName;
		public List<double> thePoseKeys;
		public List<SourceMdlEvent> theEvents;
		public List<SourceMdlAutoLayer> theAutoLayers;
		public List<SourceMdlIkLock> theIkLocks;
		//NOTE: In the file, a bone weight is a 32-bit float, i.e. a Single, but is stored as Double for better writing to file.
		public List<double> theBoneWeights;
		public int theWeightListIndex;
		//Public theAnimDescIndexes As List(Of Short)
		public string theKeyValues;


		public bool theBoneWeightsAreDefault;

	}

}