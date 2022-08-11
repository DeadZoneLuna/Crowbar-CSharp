using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlSequenceDesc31 : SourceMdlSequenceDescBase
	{
		public SourceMdlSequenceDesc31()
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

		public int blendCount;
		public int blendOffset;

		public List<List<short>> anim;

		public int movementIndex;

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
		//Public theAnimDescIndexes As List(Of Short)
		//Public theAutoLayers As List(Of SourceMdlAutoLayer37)
		//Public theBoneWeights As List(Of Double)
		//Public theEvents As List(Of SourceMdlEvent37)
		//Public theIkLocks As List(Of SourceMdlIkLock37)
		//Public theKeyValues As String
		public string theName;
		//Public thePoseKeys As List(Of Double)
		//Public theWeightListIndex As Integer

		//Public theBoneWeightsAreDefault As Boolean

	}

}