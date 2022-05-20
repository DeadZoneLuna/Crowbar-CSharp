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
	public class SourceMdlSequenceDesc10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// sequence descriptions
		//typedef struct
		//{
		//	char				label[32];	// sequence label
		//
		//	float				fps;		// frames per second	
		//	int					flags;		// looping/non-looping flags
		//
		//	int					activity;
		//	int					actweight;
		//
		//	int					numevents;
		//	int					eventindex;
		//
		//	int					numframes;	// number of frames per sequence
		//
		//	int					numpivots;	// number of foot pivots
		//	int					pivotindex;
		//
		//	int					motiontype;	
		//	int					motionbone;
		//	vec3_t				linearmovement;
		//	int					automoveposindex;
		//	int					automoveangleindex;
		//
		//	vec3_t				bbmin;		// per sequence bounding box
		//	vec3_t				bbmax;		
		//
		//	int					numblends;
		//	int					animindex;		// mstudioanim_t pointer relative to start of sequence group data
		//										// [blend][bone][X, Y, Z, XR, YR, ZR]
		//
		//	int					blendtype[2];	// X, Y, Z, XR, YR, ZR
		//	float				blendstart[2];	// starting value
		//	float				blendend[2];	// ending value
		//	int					blendparent;
		//
		//	int					seqgroup;		// sequence group for demand loading
		//
		//	int					entrynode;		// transition node at entry
		//	int					exitnode;		// transition node at exit
		//	int					nodeflags;		// transition rules
		//	
		//	int					nextseq;		// auto advancing sequences
		//} mstudioseqdesc_t;



		//	char				label[32];	// sequence label
		public char[] name = new char[32];

		//	float				fps;		// frames per second	
		public double fps;

		public int flags;
		public int activityId;
		public int activityWeight;
		public int eventCount;
		public int eventOffset;
		public int frameCount;
		public int pivotCount;
		public int pivotOffset;

		public int motiontype;
		public int motionbone;
		public SourceVector linearmovement = new SourceVector();
		public int automoveposindex;
		public int automoveangleindex;

		public SourceVector bbMin = new SourceVector();
		public SourceVector bbMax = new SourceVector();

		public int blendCount;

		//	int					animindex;		// mstudioanim_t pointer relative to start of sequence group data
		//										// [blend][bone][X, Y, Z, XR, YR, ZR]
		public int animOffset;

		//	int					blendtype[2];	// X, Y, Z, XR, YR, ZR
		//	float				blendstart[2];	// starting value
		//	float				blendend[2];	// ending value
		//	int					blendparent;
		public int[] blendType = new int[2];
		public double[] blendStart = new double[2];
		public double[] blendEnd = new double[2];
		public int blendParent;

		//	int					seqgroup;		// sequence group for demand loading
		public int groupIndex;

		//	int					entrynode;		// transition node at entry
		//	int					exitnode;		// transition node at exit
		//	int					nodeflags;		// transition rules
		public int entryNodeIndex;
		public int exitNodeIndex;
		public int nodeFlags;

		//	int					nextseq;		// auto advancing sequences
		public int nextSeq;



		public string theName;
		// There are blendCount file names.
		public List<string> theSmdRelativePathFileNames;
		public List<SourceMdlAnimation10> theAnimations;
		public List<SourceMdlEvent10> theEvents;
		public List<SourceMdlPivot10> thePivots;


		// For the flags field
		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// sequence flags
		//#define STUDIO_LOOPING	0x0001

		public const int STUDIO_LOOPING = 0x1;



	}

}