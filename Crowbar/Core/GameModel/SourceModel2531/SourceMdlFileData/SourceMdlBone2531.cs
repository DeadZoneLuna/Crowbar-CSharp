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
	public class SourceMdlBone2531
	{

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudiobone_t
		//{
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int		 			parent;		// parent bone
		//	int					bonecontroller[6];	// bone controller index, -1 == none
		//	
		//	// FIXME: remove the damn default value fields and put in pos
		//	float				value[7];	// default DoF values			// f64: ~6
		//	float				scale[7];   // scale for delta DoF values	// f64: ~6
		//	matrix3x4_t			poseToBone;
		//
		////	Quaternion			qAlignment;		// f64: -
		//	int					flags;
		//	int					proctype;
		//	int					procindex;		// procedural rule
		//	mutable int			physicsbone;	// index into physically simulated bone
		//	inline void *pProcedure( ) const { if (procindex == 0) return NULL; else return  (void *)(((byte *)this) + procindex); };
		//	int					surfacepropidx;	// index into string tablefor property name
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropidx; }
		//
		////	Quaternion			quat;			// f64: -
		//	int					contents;		// See BSPFlags.h for the contents flags
		////	int					unused[3];		// remove as appropriate	// f64: -
		//};

		public int nameOffset;
		public int parentBoneIndex;

		public int[] boneControllerIndex = new int[6];

		//Public value(6) As Double
		//Public scale(6) As Double
		public SourceVector position = new SourceVector();
		public SourceQuaternion rotation = new SourceQuaternion();
		public SourceVector positionScale = new SourceVector();
		//Public rotationScale As New SourceVector()
		//Public unknown01 As Double
		public SourceQuaternion rotationScale = new SourceQuaternion();

		public SourceVector poseToBoneColumn0 = new SourceVector();
		public SourceVector poseToBoneColumn1 = new SourceVector();
		public SourceVector poseToBoneColumn2 = new SourceVector();
		public SourceVector poseToBoneColumn3 = new SourceVector();

		public int flags;

		public int proceduralRuleType;
		public int proceduralRuleOffset;

		public int physicsBoneIndex;
		public int surfacePropNameOffset;

		public int contents;


		public SourceMdlAxisInterpBone2531 theAxisInterpBone;
		public string theName;
		public SourceMdlQuatInterpBone2531 theQuatInterpBone;
		public string theSurfacePropName;


		//#define STUDIO_PROC_AXISINTERP	1
		//#define STUDIO_PROC_QUATINTERP	2
		public const int STUDIO_PROC_AXISINTERP = 1;
		public const int STUDIO_PROC_QUATINTERP = 2;

	}

}