using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlAnimationDesc37 : SourceMdlAnimationDescBase
	{
		//struct mstudioanimdesc_t
		//{
		//	int	sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//
		//	float fps;		// frames per second	
		//	int	flags;		// looping/non-looping flags
		//
		//	int	numframes;
		//
		//	// piecewise movement
		//	int	nummovements;
		//	int	movementindex;
		//	inline mstudiomovement_t * const pMovement( int i ) const { return (mstudiomovement_t *)(((byte *)this) + movementindex) + i; };
		//
		//	Vector	bbmin;		// per animation bounding box
		//	Vector	bbmax;		
		//
		//	int	animindex;	// mstudioanim_t pointer relative to start of mstudioanimdesc_t data
		//					// [bone][X, Y, Z, XR, YR, ZR]
		//	inline mstudioanim_t		*pAnim( int i ) const { return  (mstudioanim_t *)(((byte *)this) + animindex) + i; };
		//
		//	int	numikrules;
		//	int	ikruleindex;
		//	inline mstudioikrule_t *pIKRule( int i ) const { return (mstudioikrule_t *)(((byte *)this) + ikruleindex) + i; };
		//
		//	int	unused[8];	// remove as appropriate
		//};

		public int nameOffset;
		public double fps;
		public int flags;
		public int frameCount;
		public int movementCount;
		public int movementOffset;

		public SourceVector bbMin = new SourceVector();
		public SourceVector bbMax = new SourceVector();

		public int animOffset;

		public int ikRuleCount;
		public int ikRuleOffset;

		public int[] unused = new int[8];

		public List<SourceMdlAnimation37> theAnimations;
		public List<SourceMdlIkRule37> theIkRules;
		public List<SourceMdlMovement> theMovements;
		//Public theName As String

		public bool theAnimIsLinkedToSequence = false;
		public List<SourceMdlSequenceDesc37> theLinkedSequences = new List<SourceMdlSequenceDesc37>();

	}

}