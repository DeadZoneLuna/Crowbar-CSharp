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
	public class SourceMdlBone37
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
		//struct mstudiobone_t
		//{
		//	int		sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int		parent;		// parent bone
		//	int		bonecontroller[6];	// bone controller index, -1 == none
		//	// FIXME: remove the damn default value fields and put in pos
		//	float		value[6];	// default DoF values
		//	float		scale[6];   // scale for delta DoF values
		//	matrix3x4_t	poseToBone;
		//	Quaternion	qAlignment;
		//	int		flags;
		//	int		proctype;
		//	int		procindex;	// procedural rule
		//	mutable int	physicsbone;	// index into physically simulated bone
		//	inline void *pProcedure( ) const { if (procindex == 0) return NULL; else return  (void *)(((byte *)this) + procindex); };
		//	int		surfacepropidx;	// index into string table for property name
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropidx; }
		//	Quaternion	quat;
		//	int		contents;		// See BSPFlags.h for the contents flags
		//	int		unused[3];		// remove as appropriate
		//};

		public int nameOffset;
		public int parentBoneIndex;

		public int[] boneControllerIndex = new int[6];

		//Public value(6) As Double
		//Public scale(6) As Double
		public SourceVector position = new SourceVector();
		public SourceVector rotation = new SourceVector();
		public SourceVector positionScale = new SourceVector();
		public SourceVector rotationScale = new SourceVector();

		public SourceVector poseToBoneColumn0 = new SourceVector();
		public SourceVector poseToBoneColumn1 = new SourceVector();
		public SourceVector poseToBoneColumn2 = new SourceVector();
		public SourceVector poseToBoneColumn3 = new SourceVector();

		public SourceQuaternion qAlignment;

		public int flags;

		public int proceduralRuleType;
		public int proceduralRuleOffset;
		public int physicsBoneIndex;
		public int surfacePropNameOffset;

		public SourceQuaternion quat;

		public int contents;

		public int[] unused = new int[3];

		public SourceMdlAxisInterpBone theAxisInterpBone;
		public string theName;
		public SourceMdlQuatInterpBone theQuatInterpBone;
		public string theSurfacePropName;


		//#define STUDIO_PROC_AXISINTERP	1
		//#define STUDIO_PROC_QUATINTERP	2
		public const int STUDIO_PROC_AXISINTERP = 1;
		public const int STUDIO_PROC_QUATINTERP = 2;

	}

}