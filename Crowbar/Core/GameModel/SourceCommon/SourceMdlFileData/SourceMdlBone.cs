using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBone
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//// bones
		//struct mstudiobone_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int		 			parent;		// parent bone
		//	int					bonecontroller[6];	// bone controller index, -1 == none

		//	// default values
		//	Vector				pos;
		//	Quaternion			quat;
		//	RadianEuler			rot;
		//	// compression scale
		//	Vector				posscale;
		//	Vector				rotscale;

		//	matrix3x4_t			poseToBone;
		//	Quaternion			qAlignment;
		//	int					flags;
		//	int					proctype;
		//	int					procindex;		// procedural rule
		//	mutable int			physicsbone;	// index into physically simulated bone
		//	inline void *pProcedure( ) const { if (procindex == 0) return NULL; else return  (void *)(((byte *)this) + procindex); };
		//	int					surfacepropidx;	// index into string tablefor property name
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropidx; }
		//	int					contents;		// See BSPFlags.h for the contents flags

		//	int					unused[8];		// remove as appropriate

		//	mstudiobone_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudiobone_t(const mstudiobone_t& vOther);
		//};

		// VERSION 10: 
		//	char				name[32];	// bone name for symbolic links
		public char[] name = new char[32];
		//------------
		// VERSION > 10: 
		//	int					sznameindex;
		public int nameOffset;

		//	int		 			parent;		// parent bone
		public int parentBoneIndex;
		//	int					bonecontroller[6];	// bone controller index, -1 == none
		public int[] boneControllerIndex = new int[6];

		// VERSION 10: 
		//	float				value[6];	// default DoF values
		//	float				scale[6];   // scale for delta DoF values
		public double[] value = new double[6];
		public double[] scale = new double[6];
		//------------
		// VERSION > 10: [All the remainingm except flags.]

		//NOTE: Changed to Double, so that the values will be properly written to file with 6 decimal digits.
		//	Vector				pos;
		public SourceVector position;

		//	Quaternion			quat;
		public SourceQuaternion quat;

		// VERSION 2531: 
		//FROM: MDLConverter for VtMB
		//	float AnimChannels[NUMANIMCHANNELS];
		public double[] animChannels = new double[7];

		//NOTE: Changed to Double, so that the values will be properly written to file with 6 decimal digits.
		//	RadianEuler			rot;
		public SourceVector rotation;
		//	Vector				posscale;
		public SourceVector positionScale;
		//	Vector				rotscale;
		public SourceVector rotationScale;

		//	matrix3x4_t			poseToBone;
		public SourceVector poseToBoneColumn0;
		public SourceVector poseToBoneColumn1;
		public SourceVector poseToBoneColumn2;
		public SourceVector poseToBoneColumn3;
		//	Quaternion			qAlignment;
		public SourceQuaternion qAlignment;
		//	int					flags;
		public int flags;
		//	int					proctype;
		public int proceduralRuleType;
		//	int					procindex;		// procedural rule
		//	inline void *pProcedure( ) const { if (procindex == 0) return NULL; else return  (void *)(((byte *)this) + procindex); };
		public int proceduralRuleOffset;
		//	mutable int			physicsbone;	// index into physically simulated bone
		public int physicsBoneIndex;
		//	int					surfacepropidx;	// index into string tablefor property name
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropidx; }
		public int surfacePropNameOffset;
		//	int					contents;		// See BSPFlags.h for the contents flags
		public int contents;

		//	int					unused[8];		// remove as appropriate
		public int[] unused = new int[8];

		//54 words = 54 * 4 bytes = 216 (0xD8) bytes

		public string theName;
		public SourceMdlAxisInterpBone theAxisInterpBone;
		public SourceMdlQuatInterpBone theQuatInterpBone;
		public SourceMdlAimAtBone theAimAtBone;
		public SourceMdlJiggleBone theJiggleBone;
		public string theSurfacePropName;



		// flag values:
		//FROM: SourceEngine2006_source\public\studio.h
		//#define BONE_SCREEN_ALIGN_SPHERE	0x08	// bone aligns to the screen, not constrained in motion.
		//#define BONE_SCREEN_ALIGN_CYLINDER	0x10	// bone aligns to the screen, constrained by it's own axis.
		//
		//#define BONE_USED_MASK				0x0007FF00
		//#define BONE_USED_BY_ANYTHING		0x0007FF00
		//#define BONE_USED_BY_HITBOX			0x00000100	// bone (or child) is used by a hit box
		//#define BONE_USED_BY_ATTACHMENT		0x00000200	// bone (or child) is used by an attachment point
		//#define BONE_USED_BY_VERTEX_MASK	0x0003FC00
		//#define BONE_USED_BY_VERTEX_LOD0	0x00000400	// bone (or child) is used by the toplevel model via skinned vertex
		//#define BONE_USED_BY_VERTEX_LOD1	0x00000800	
		//#define BONE_USED_BY_VERTEX_LOD2	0x00001000  
		//#define BONE_USED_BY_VERTEX_LOD3	0x00002000
		//#define BONE_USED_BY_VERTEX_LOD4	0x00004000
		//#define BONE_USED_BY_VERTEX_LOD5	0x00008000
		//#define BONE_USED_BY_VERTEX_LOD6	0x00010000
		//#define BONE_USED_BY_VERTEX_LOD7	0x00020000
		//#define BONE_USED_BY_BONE_MERGE		0x00040000	// bone is available for bone merge to occur against it
		//
		//#define BONE_USED_BY_VERTEX_AT_LOD(lod) ( BONE_USED_BY_VERTEX_LOD0 << (lod) )
		//#define BONE_USED_BY_ANYTHING_AT_LOD(lod) ( ( BONE_USED_BY_ANYTHING & ~BONE_USED_BY_VERTEX_MASK ) | BONE_USED_BY_VERTEX_AT_LOD(lod) )
		//
		//#define BONE_TYPE_MASK				0x00F00000
		//#define BONE_FIXED_ALIGNMENT		0x00100000	// bone can't spin 360 degrees, all interpolation is normalized around a fixed orientation
		//
		//#define BONE_HAS_SAVEFRAME_POS		0x00200000
		//#define BONE_HAS_SAVEFRAME_ROT		0x00400000
		public const int BONE_SCREEN_ALIGN_SPHERE = 0x8;
		public const int BONE_SCREEN_ALIGN_CYLINDER = 0x10;
		public const int BONE_USED_BY_VERTEX_LOD0 = 0x400;
		public const int BONE_USED_BY_VERTEX_LOD1 = 0x800;
		public const int BONE_USED_BY_VERTEX_LOD2 = 0x1000;
		public const int BONE_USED_BY_VERTEX_LOD3 = 0x2000;
		public const int BONE_USED_BY_VERTEX_LOD4 = 0x4000;
		public const int BONE_USED_BY_VERTEX_LOD5 = 0x8000;
		public const int BONE_USED_BY_VERTEX_LOD6 = 0x10000;
		public const int BONE_USED_BY_VERTEX_LOD7 = 0x20000;
		public const int BONE_USED_BY_BONE_MERGE = 0x40000;
		public const int BONE_FIXED_ALIGNMENT = 0x100000;
		public const int BONE_HAS_SAVEFRAME_POS = 0x200000;
		public const int BONE_HAS_SAVEFRAME_ROT = 0x400000;
		// MDL v49: 
		//Public Const BONE_HAS_SAVEFRAME_ROT64 As Integer = &H400000
		public const int BONE_HAS_SAVEFRAME_ROT32 = 0x800000;



		// proceduralRuleType values:
		//#define STUDIO_PROC_AXISINTERP	1
		//#define STUDIO_PROC_QUATINTERP	2
		//#define STUDIO_PROC_AIMATBONE	3
		//#define STUDIO_PROC_AIMATATTACH 4
		//#define STUDIO_PROC_JIGGLE 5
		public const int STUDIO_PROC_AXISINTERP = 1;
		public const int STUDIO_PROC_QUATINTERP = 2;
		public const int STUDIO_PROC_AIMATBONE = 3;
		public const int STUDIO_PROC_AIMATATTACH = 4;
		public const int STUDIO_PROC_JIGGLE = 5;



		// contents values:
		//FROM: SourceEngine2006_source\public\bspflags.h

		//// contents flags are seperate bits
		//// a given brush can contribute multiple content bits
		//// multiple brushes can be in a single leaf

		//// lower bits are stronger, and will eat weaker brushes completely
		//#define	CONTENTS_EMPTY			0		// No contents

		//#define	CONTENTS_SOLID			0x1		// an eye is never valid in a solid
		//#define	CONTENTS_WINDOW			0x2		// translucent, but not watery (glass)
		//#define	CONTENTS_AUX			0x4
		//#define	CONTENTS_GRATE			0x8		// alpha-tested "grate" textures.  Bullets/sight pass through, but solids don't
		//#define	CONTENTS_SLIME			0x10
		//#define	CONTENTS_WATER			0x20
		//#define	CONTENTS_MIST			0x40
		//#define CONTENTS_OPAQUE			0x80	// things that cannot be seen through (may be non-solid though)
		//#define	LAST_VISIBLE_CONTENTS	0x80

		//#define ALL_VISIBLE_CONTENTS (LAST_VISIBLE_CONTENTS | (LAST_VISIBLE_CONTENTS-1))

		//#define CONTENTS_TESTFOGVOLUME	0x100

		//// unused 
		//// NOTE: If it's visible, grab from the top + update LAST_VISIBLE_CONTENTS
		//// if not visible, then grab from the bottom.
		//#define CONTENTS_UNUSED5		0x200
		//#define CONTENTS_UNUSED6		0x4000

		//#define CONTENTS_TEAM1			0x800	// per team contents used to differentiate collisions 
		//#define CONTENTS_TEAM2			0x1000	// between players and objects on different teams

		//// ignore CONTENTS_OPAQUE on surfaces that have SURF_NODRAW
		//#define CONTENTS_IGNORE_NODRAW_OPAQUE	0x2000

		//// hits entities which are MOVETYPE_PUSH (doors, plats, etc.)
		//#define CONTENTS_MOVEABLE		0x4000

		//// remaining contents are non-visible, and don't eat brushes
		//#define	CONTENTS_AREAPORTAL		0x8000

		//#define	CONTENTS_PLAYERCLIP		0x10000
		//#define	CONTENTS_MONSTERCLIP	0x20000

		//// currents can be added to any other contents, and may be mixed
		//#define	CONTENTS_CURRENT_0		0x40000
		//#define	CONTENTS_CURRENT_90		0x80000
		//#define	CONTENTS_CURRENT_180	0x100000
		//#define	CONTENTS_CURRENT_270	0x200000
		//#define	CONTENTS_CURRENT_UP		0x400000
		//#define	CONTENTS_CURRENT_DOWN	0x800000

		//#define	CONTENTS_ORIGIN			0x1000000	// removed before bsping an entity

		//#define	CONTENTS_MONSTER		0x2000000	// should never be on a brush, only in game
		//#define	CONTENTS_DEBRIS			0x4000000
		//#define	CONTENTS_DETAIL			0x8000000	// brushes to be added after vis leafs
		//#define	CONTENTS_TRANSLUCENT	0x10000000	// auto set if any surface has trans
		//#define	CONTENTS_LADDER			0x20000000
		//#define CONTENTS_HITBOX			0x40000000	// use accurate hitboxes on trace

		public const int CONTENTS_SOLID = 0x1;
		public const int CONTENTS_GRATE = 0x8;
		public const int CONTENTS_MONSTER = 0x2000000;
		public const int CONTENTS_LADDER = 0x20000000;

	}

}