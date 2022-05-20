﻿//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlSequenceDesc32 : SourceMdlSequenceDescBase
	{
		//Public Sub New()
		//	'	short				anim[MAXSTUDIOBLENDS][MAXSTUDIOBLENDS];	// f64: 16x16x2 = 512 bytes each anim a short
		//	Me.anim = New List(Of List(Of Short))(MAXSTUDIOBLENDS)
		//	For rowIndex As Integer = 0 To MAXSTUDIOBLENDS - 1
		//		Dim animRow As New List(Of Short)(MAXSTUDIOBLENDS)
		//		For columnIndex As Integer = 0 To MAXSTUDIOBLENDS - 1
		//			animRow.Add(0)
		//		Next
		//		Me.anim.Add(animRow)
		//	Next
		//End Sub

		//// sequence descriptions
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
		//	int					anim[MAXSTUDIOBLENDS][MAXSTUDIOBLENDS];	// animation number
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
		//	int					entrynode;		// transition node at entry
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
		//	int					numiklocks;
		//	int					iklockindex;
		//	inline mstudioiklock_t *pIKLock( int i ) const { return (mstudioiklock_t *)(((byte *)this) + iklockindex) + i; };
		//
		//	// Key values
		//	int					keyvalueindex;
		//	int					keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
		//
		//	int					unused[3];		// remove/add as appropriate (grow back to 8 ints on version change!)
		//};

		//Public nameOffset As Integer
		//Public activityNameOffset As Integer
		//Public flags As Integer
		//Public activity As Integer
		//Public activityWeight As Integer
		//Public eventCount As Integer
		//Public eventOffset As Integer

		//Public bbMin As New SourceVector()
		//Public bbMax As New SourceVector()

		//Public blendCount As Integer

		//Public anim As List(Of List(Of Short))

		//Public movementIndex As Integer
		//Public groupSize(1) As Integer
		//Public paramIndex(1) As Integer
		//Public paramStart(1) As Single
		//Public paramEnd(1) As Single
		//Public paramParent As Integer

		//Public sequenceGroup As Integer

		//Public fadeInTime As Single
		//Public fadeOutTime As Single

		//Public entryNodeIndex As Integer
		//Public exitNodeIndex As Integer
		//Public nodeFlags As Integer

		//Public entryPhase As Single
		//Public exitPhase As Single
		//Public lastFrame As Single

		//Public nextSeq As Integer
		//Public pose As Integer

		//Public ikRuleCount As Integer
		//Public autoLayerCount As Integer
		//Public autoLayerOffset As Integer
		//Public weightOffset As Integer
		//Public poseKeyOffset As Integer

		//Public ikLockCount As Integer
		//Public ikLockOffset As Integer
		//Public keyValueOffset As Integer
		//Public keyValueSize As Integer

		//Public unused(2) As Integer

		//======

		public int nameOffset;
		public int activityNameOffset;
		public int flags;
		public int activity;
		public int activityWeight;
		public int eventCount;
		public int eventOffset;

		public SourceVector bbMin = new SourceVector();
		public SourceVector bbMax = new SourceVector();

		public int frameCount;

		public int animDescIndex;
		public int unknown;

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

		public int[] unused = new int[2];

		public string theActivityName;
		//Public theAnimDescIndexes As List(Of Short)
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