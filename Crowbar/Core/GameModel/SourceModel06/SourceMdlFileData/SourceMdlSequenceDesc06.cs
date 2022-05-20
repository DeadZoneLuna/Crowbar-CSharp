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
	public class SourceMdlSequenceDesc06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	char				label[32];		// sequence label
		//
		//	float				fps;			// frames per second
		//	int					flags;			// looping/non-looping flags
		//
		//	int					numevents;		// TOMAS: USED (not always 0)
		//	int					eventindex;
		//
		//	int					numframes;		// number of frames per sequence
		//
		//	int					unused01;		// TOMAS: UNUSED (checked)
		//
		//	int					numpivots;		// number of foot pivots
		//	// TOMAS: polyrobo.mdl use this (4)
		//	int					pivotindex;
		//
		//	int					motiontype;		// TOMAS: USED (not always 0)
		//	int					motionbone;		// motion bone id (0)
		//
		//	int					unused02;		// TOMAS: UNUSED (checked)
		//	vec3_t				linearmovement;	// TOMAS: USED (not always 0)
		//
		//	int					numblends;		// TOMAS: UNUSED (checked)
		//	int					animindex;		// (->mstudioanim_t)
		//
		//	int					unused03[ 2 ];	// TOMAS: UNUSED (checked)
		//
		//} mstudioseqdesc_t;

		public char[] name = new char[32];
		public double fps;

		public int flags;
		public int eventCount;
		public int eventOffset;
		public int frameCount;
		public int unused01;
		public int pivotCount;
		public int pivotOffset;

		public int motiontype;
		public int motionbone;
		public int unused02;
		public SourceVector linearmovement = new SourceVector();

		public int blendCount;
		public int animOffset;

		public int[] unused03 = new int[2];


		public string theName;
		public string theSmdRelativePathFileName;
		public List<SourceMdlAnimation06> theAnimations;
		public List<SourceMdlEvent06> theEvents;
		public List<SourceMdlPivot06> thePivots;


		// For the flags field
		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// sequence flags
		//#define STUDIO_LOOPING	0x0001

		public const int STUDIO_LOOPING = 0x1;

	}

}