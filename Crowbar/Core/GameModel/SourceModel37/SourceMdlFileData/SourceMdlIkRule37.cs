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
	public class SourceMdlIkRule37
	{

		//struct mstudioikrule_t
		//{
		//	int		index;
		//
		//	int		type;
		//	int		chain;
		//
		//	int		bone;
		//
		//	int			slot;	// iktarget slot.  Usually same as chain.
		//	float		height;
		//	float		radius;
		//	float		floor;
		//	Vector		pos;
		//	Quaternion	q;
		//
		//	float		flWeight;
		//
		//	int		group; // match sub-sequence IK rules together
		//
		//	int		iStart;
		//	int		ikerrorindex;
		//	inline mstudioikerror_t *pError( int i ) const { return  (mstudioikerror_t *)(((byte *)this) + ikerrorindex) + (i - iStart); };
		//
		//	float		start;	// beginning of influence
		//	float		peak;	// start of full influence
		//	float		tail;	// end of full influence
		//	float		end;	// end of all influence
		//
		//	float		commit;		// unused: frame footstep target should be committed
		//	float		contact;	// unused: frame footstep makes ground concact
		//	float		pivot;		// unused: frame ankle can begin rotation from latched orientation
		//	float		release;	// unused: frame ankle should end rotation from latched orientation
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

		public double weight;
		public int group;
		public int ikErrorIndexStart;
		public int ikErrorOffset;

		public double influenceStart;
		public double influencePeak;
		public double influenceTail;
		public double influenceEnd;

		public double commit;
		public double contact;
		public double pivot;
		public double release;

		public List<SourceMdlIkError37> theIkErrors;

	}

}