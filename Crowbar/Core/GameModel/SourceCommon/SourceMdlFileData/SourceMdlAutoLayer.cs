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
	public class SourceMdlAutoLayer
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//struct mstudioautolayer_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		////private:
		//	short				iSequence;
		//	short				iPose;
		////public:
		//	int					flags;
		//	float				start;	// beginning of influence
		//	float				peak;	// start of full influence
		//	float				tail;	// end of full influence
		//	float				end;	// end of all influence
		//};



		public short sequenceIndex;
		public short poseIndex;

		public int flags;
		public double influenceStart;
		public double influencePeak;
		public double influenceTail;
		public double influenceEnd;



		//FROM: SourceEngineXXXX_source\public\studio.h
		//// autolayer flags
		////							0x0001
		////							0x0002
		////							0x0004
		////							0x0008
		//#define STUDIO_AL_POST		0x0010		// 
		////							0x0020
		//#define STUDIO_AL_SPLINE	0x0040		// convert layer ramp in/out curve is a spline instead of linear
		//#define STUDIO_AL_XFADE		0x0080		// pre-bias the ramp curve to compense for a non-1 weight, assuming a second layer is also going to accumulate
		////							0x0100
		//#define STUDIO_AL_NOBLEND	0x0200		// animation always blends at 1.0 (ignores weight)
		////							0x0400
		////							0x0800
		//#define STUDIO_AL_LOCAL		0x1000		// layer is a local context sequence
		////							0x2000
		//#define STUDIO_AL_POSE		0x4000		// layer blends using a pose parameter instead of parent cycle

		public const int STUDIO_AL_SPLINE = 0x40;
		public const int STUDIO_AL_XFADE = 0x80;
		public const int STUDIO_AL_NOBLEND = 0x200;
		public const int STUDIO_AL_LOCAL = 0x1000;
		public const int STUDIO_AL_POSE = 0x4000;

	}

}