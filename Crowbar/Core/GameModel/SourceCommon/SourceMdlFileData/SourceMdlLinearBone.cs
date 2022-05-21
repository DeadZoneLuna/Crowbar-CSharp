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
	public class SourceMdlLinearBone
	{

		public SourceMdlLinearBone()
		{
			theFlags = new List<int>();
			theParents = new List<int>();
			thePositions = new List<SourceVector>();
			theQuaternions = new List<SourceQuaternion>();
			theRotations = new List<SourceVector>();
			thePoseToBoneDataColumn0s = new List<SourceVector>();
			thePoseToBoneDataColumn1s = new List<SourceVector>();
			thePoseToBoneDataColumn2s = new List<SourceVector>();
			thePoseToBoneDataColumn3s = new List<SourceVector>();
			thePositionScales = new List<SourceVector>();
			theRotationScales = new List<SourceVector>();
			theQAlignments = new List<SourceQuaternion>();
		}

		//FROM: SourceEngine2007\src_main\public\studio.h
		//struct mstudiolinearbone_t	
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//
		//	int numbones;
		//
		//	int flagsindex;
		//	inline int flags( int i ) const { Assert( i >= 0 && i < numbones); return *((int *)(((byte *)this) + flagsindex) + i); };
		//	inline int *pflags( int i ) { Assert( i >= 0 && i < numbones); return ((int *)(((byte *)this) + flagsindex) + i); };
		//
		//	int	parentindex;
		//	inline int parent( int i ) const { Assert( i >= 0 && i < numbones); return *((int *)(((byte *)this) + parentindex) + i); };
		//
		//	int	posindex;
		//	inline Vector pos( int i ) const { Assert( i >= 0 && i < numbones); return *((Vector *)(((byte *)this) + posindex) + i); };
		//
		//	int quatindex;
		//	inline Quaternion quat( int i ) const { Assert( i >= 0 && i < numbones); return *((Quaternion *)(((byte *)this) + quatindex) + i); };
		//
		//	int rotindex;
		//	inline RadianEuler rot( int i ) const { Assert( i >= 0 && i < numbones); return *((RadianEuler *)(((byte *)this) + rotindex) + i); };
		//
		//	int posetoboneindex;
		//	inline matrix3x4_t poseToBone( int i ) const { Assert( i >= 0 && i < numbones); return *((matrix3x4_t *)(((byte *)this) + posetoboneindex) + i); };
		//
		//	int	posscaleindex;
		//	inline Vector posscale( int i ) const { Assert( i >= 0 && i < numbones); return *((Vector *)(((byte *)this) + posscaleindex) + i); };
		//
		//	int	rotscaleindex;
		//	inline Vector rotscale( int i ) const { Assert( i >= 0 && i < numbones); return *((Vector *)(((byte *)this) + rotscaleindex) + i); };
		//
		//	int	qalignmentindex;
		//	inline Quaternion qalignment( int i ) const { Assert( i >= 0 && i < numbones); return *((Quaternion *)(((byte *)this) + qalignmentindex) + i); };
		//
		//	int unused[6];
		//
		//	mstudiolinearbone_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudiolinearbone_t(const mstudiolinearbone_t& vOther);
		//};

		//	int numbones;
		public int boneCount;

		//	int flagsindex;
		public int flagsOffset;

		//	int	parentindex;
		public int parentOffset;

		//	int	posindex;
		public int posOffset;

		//	int quatindex;
		public int quatOffset;

		//	int rotindex;
		public int rotOffset;

		//	int posetoboneindex;
		public int poseToBoneOffset;

		//	int	posscaleindex;
		public int posScaleOffset;

		//	int	rotscaleindex;
		public int rotScaleOffset;

		//	int	qalignmentindex;
		public int qAlignmentOffset;

		//	int unused[6];
		public int[] unused = new int[6];



		//FROM: SourceEngine2007\src_main\utils\studiomdl\write.cpp
		//      static void WriteBoneTransforms( studiohdr2_t *phdr, mstudiobone_t *pBone )
		//
		//#define WRITE_BONE_BLOCK( type, srcfield, dest, destindex ) \
		//		type *##dest = (type *)pData; \
		//		pLinearBone->##destindex = pData - (byte *)pLinearBone; \
		//		pData += g_numbones * sizeof( *##dest ); \
		//		ALIGN4( pData ); \
		//		for ( int i = 0; i < g_numbones; i++) \
		//			dest##[i] = pBone[i].##srcfield;

		//		WRITE_BONE_BLOCK( int, flags, pFlags, flagsindex );
		//		WRITE_BONE_BLOCK( int, parent, pParent, parentindex );
		//		WRITE_BONE_BLOCK( Vector, pos, pPos, posindex );
		//		WRITE_BONE_BLOCK( Quaternion, quat, pQuat, quatindex );
		//		WRITE_BONE_BLOCK( RadianEuler, rot, pRot, rotindex );
		//		WRITE_BONE_BLOCK( matrix3x4_t, poseToBone, pPoseToBone, posetoboneindex );
		//		WRITE_BONE_BLOCK( Vector, posscale, pPoseScale, posscaleindex );
		//		WRITE_BONE_BLOCK( Vector, rotscale, pRotScale, rotscaleindex );
		//		WRITE_BONE_BLOCK( Quaternion, qAlignment, pQAlignment, qalignmentindex );
		public List<int> theFlags;
		public List<int> theParents;
		public List<SourceVector> thePositions;
		public List<SourceQuaternion> theQuaternions;
		public List<SourceVector> theRotations;
		public List<SourceVector> thePoseToBoneDataColumn0s;
		public List<SourceVector> thePoseToBoneDataColumn1s;
		public List<SourceVector> thePoseToBoneDataColumn2s;
		public List<SourceVector> thePoseToBoneDataColumn3s;
		public List<SourceVector> thePositionScales;
		public List<SourceVector> theRotationScales;
		public List<SourceQuaternion> theQAlignments;

	}

}