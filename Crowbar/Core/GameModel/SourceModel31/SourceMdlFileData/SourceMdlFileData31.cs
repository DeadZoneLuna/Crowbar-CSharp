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
	public class SourceMdlFileData31 : SourceMdlFileDataBase
	{
		public SourceMdlFileData31() : base()
		{

			this.theModelCommandIsUsed = false;
			//Me.theProceduralBonesCommandIsUsed = False
		}

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
		//struct studiohdr_t
		//{
		//	int	id;
		//	int	version;
		//
		//	long checksum;	// this has to be the same in the phy and vtx files to load!
		//
		//	char name[64];
		//	int	length;
		//
		//	Vector	eyeposition;	// ideal eye position
		//
		//	Vector	illumposition;	// illumination center
		//
		//	Vector	hull_min;	// ideal movement hull size
		//	Vector	hull_max;			
		//
		//	Vector	view_bbmin;	// clipping bounding box
		//	Vector	view_bbmax;	
		//
		//	int	flags;
		//
		//	int	numbones;	// bones
		//	int	boneindex;
		//	inline mstudiobone_t *pBone( int i ) const { return (mstudiobone_t *)(((byte *)this) + boneindex) + i; };
		//
		//	int	numbonecontrollers;	// bone controllers
		//	int	bonecontrollerindex;
		//	inline mstudiobonecontroller_t *pBonecontroller( int i ) const { return (mstudiobonecontroller_t *)(((byte *)this) + bonecontrollerindex) + i; };
		//
		//	int	numhitboxsets;
		//	int	hitboxsetindex; 
		//
		//	// Look up hitbox set by index
		//	mstudiohitboxset_t  *pHitboxSet( int i ) const 
		//	{ 
		//		return (mstudiohitboxset_t *)(((byte *)this) + hitboxsetindex ) + i; 
		//	};
		//
		//	// Calls through to hitbox to determine size of specified set
		//	inline mstudiobbox_t *pHitbox( int i, int set ) const 
		//	{ 
		//		mstudiohitboxset_t const *s = pHitboxSet( set );
		//		if ( !s )
		//			return NULL;
		//
		//		return s->pHitbox( i );
		//	};
		//
		//	// Calls through to set to get hitbox count for set
		//	inline int  iHitboxCount( int set ) const
		//	{
		//		mstudiohitboxset_t const *s = pHitboxSet( set );
		//		if ( !s )
		//			return 0;
		//
		//		return s->numhitboxes;
		//	};
		//
		//	int	numanim;	// animations/poses
		//	int	animdescindex;	// animation descriptions
		//	inline mstudioanimdesc_t *pAnimdesc( int i ) const { return (mstudioanimdesc_t *)(((byte *)this) + animdescindex) + i; };
		//
		//	int 	numanimgroup;
		//	int 	animgroupindex;
		//	inline  mstudioanimgroup_t *pAnimGroup(int i) const	{ return (mstudioanimgroup_t *)(((byte *)this) + animgroupindex) + i; };
		//
		//	int 	numbonedesc;
		//	int 	bonedescindex;
		//	inline  mstudiobonedesc_t *pBoneDesc(int i) const { return (mstudiobonedesc_t *)(((byte *)this) + bonedescindex) + i; };
		//
		//	int	numseq;		// sequences
		//	int	seqindex;
		//	inline mstudioseqdesc_t *pSeqdesc( int i ) const { if (i < 0 || i >= numseq) i = 0; return (mstudioseqdesc_t *)(((byte *)this) + seqindex) + i; };
		//	int	sequencesindexed;	// initialization flag - have the sequences been indexed?
		//
		//	int	numseqgroups;		// demand loaded sequences
		//	int	seqgroupindex;
		//	inline  mstudioseqgroup_t *pSeqgroup(int i) const { return (mstudioseqgroup_t *)(((byte *)this) + seqgroupindex) + i; };
		//
		//	int	numtextures;		// raw textures
		//	int	textureindex;
		//	inline mstudiotexture_t *pTexture( int i ) const { return (mstudiotexture_t *)(((byte *)this) + textureindex) + i; }; 
		//
		//	int	numcdtextures;		// raw textures search paths
		//	int	cdtextureindex;
		//	inline char			*pCdtexture( int i ) const { return (((char *)this) + *((int *)(((byte *)this) + cdtextureindex) + i)); };
		//
		//	int	numskinref;		// replaceable textures tables
		//	int	numskinfamilies;
		//	int	skinindex;
		//	inline short		*pSkinref( int i ) const { return (short *)(((byte *)this) + skinindex) + i; };
		//
		//	int	numbodyparts;		
		//	int	bodypartindex;
		//	inline mstudiobodyparts_t	*pBodypart( int i ) const { return (mstudiobodyparts_t *)(((byte *)this) + bodypartindex) + i; };
		//
		//	int	numattachments;		// queryable attachable points
		//	int	attachmentindex;
		//	inline mstudioattachment_t	*pAttachment( int i ) const { return (mstudioattachment_t *)(((byte *)this) + attachmentindex) + i; };
		//
		//	int	numtransitions;		// animation node to animation node transition graph
		//	int	transitionindex;
		//	inline byte	*pTransition( int i ) const { return (byte *)(((byte *)this) + transitionindex) + i; };
		//
		//	int	numflexdesc;
		//	int	flexdescindex;
		//	inline mstudioflexdesc_t *pFlexdesc( int i ) const { return (mstudioflexdesc_t *)(((byte *)this) + flexdescindex) + i; };
		//
		//	int	numflexcontrollers;
		//	int	flexcontrollerindex;
		//	inline mstudioflexcontroller_t *pFlexcontroller( int i ) const { return (mstudioflexcontroller_t *)(((byte *)this) + flexcontrollerindex) + i; };
		//
		//	int	numflexrules;
		//	int	flexruleindex;
		//	inline mstudioflexrule_t *pFlexRule( int i ) const { return (mstudioflexrule_t *)(((byte *)this) + flexruleindex) + i; };
		//
		//	int	numikchains;
		//	int	ikchainindex;
		//	inline mstudioikchain_t *pIKChain( int i ) const { return (mstudioikchain_t *)(((byte *)this) + ikchainindex) + i; };
		//
		//	int	nummouths;
		//	int	mouthindex;
		//	inline mstudiomouth_t *pMouth( int i ) const { return (mstudiomouth_t *)(((byte *)this) + mouthindex) + i; };
		//
		//	int	numposeparameters;
		//	int	poseparamindex;
		//	inline mstudioposeparamdesc_t *pPoseParameter( int i ) const { return (mstudioposeparamdesc_t *)(((byte *)this) + poseparamindex) + i; };
		//
		//	int	surfacepropindex;
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropindex; }
		//
		//	// Key values
		//	int	keyvalueindex;
		//	int	keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
		//
		//	int	numikautoplaylocks;
		//	int	ikautoplaylockindex;
		//	inline mstudioiklock_t *pIKAutoplayLock( int i ) const { return (mstudioiklock_t *)(((byte *)this) + ikautoplaylockindex) + i; };
		//
		//	float mass;		// The collision model mass that jay wanted
		//	int	contents;
		//	int	unused[9];	// remove as appropriate
		//};

		public char[] name = new char[64];

		public SourceVector eyePosition = new SourceVector();
		public SourceVector illuminationPosition = new SourceVector();

		public SourceVector hullMinPosition = new SourceVector();
		public SourceVector hullMaxPosition = new SourceVector();
		public SourceVector viewBoundingBoxMinPosition = new SourceVector();
		public SourceVector viewBoundingBoxMaxPosition = new SourceVector();

		public int flags;

		public int boneCount;
		public int boneOffset;
		public int boneControllerCount;
		public int boneControllerOffset;

		// MDL27to29
		public int hitboxCount_MDL27to30;
		public int hitboxOffset_MDL27to30;
		//------
		// MDL31
		public int hitboxSetCount;
		public int hitboxSetOffset;

		public int animationCount;
		public int animationOffset;
		//Public animationGroupCount As Integer
		//Public animationGroupOffset As Integer

		//Public boneDescCount As Integer
		//Public boneDescOffset As Integer

		public int localSequenceCount;
		public int localSequenceOffset;
		public int sequencesIndexedFlag;
		public int sequenceGroupCount;
		public int sequenceGroupOffset;

		public int textureCount;
		public int textureOffset;
		public int texturePathCount;
		public int texturePathOffset;
		public int skinReferenceCount;
		public int skinFamilyCount;
		public int skinOffset;

		public int bodyPartCount;
		public int bodyPartOffset;

		public int localAttachmentCount;
		public int localAttachmentOffset;

		public int transitionCount;
		public int transitionOffset;

		public int flexDescCount;
		public int flexDescOffset;
		public int flexControllerCount;
		public int flexControllerOffset;
		public int flexRuleCount;
		public int flexRuleOffset;

		public int ikChainCount;
		public int ikChainOffset;
		public int mouthCount;
		public int mouthOffset;
		public int localPoseParamaterCount;
		public int localPoseParameterOffset;

		public int surfacePropOffset;

		//Public keyValueOffset As Integer
		//Public keyValueSize As Integer

		//Public localIkAutoPlayLockCount As Integer
		//Public localIkAutoPlayLockOffset As Integer

		//Public mass As Double
		//Public contents As Integer

		//Public unused(8) As Integer

		public List<SourceMdlAnimationDesc31> theAnimationDescs;
		//Public theAnimGroups As List(Of SourceMdlAnimGroup31)
		//Public theAttachments As List(Of SourceMdlAttachment37)
		public List<SourceMdlBodyPart31> theBodyParts;
		//Public theBoneControllers As List(Of SourceMdlBoneController37)
		//Public theBoneDescs As List(Of SourceMdlBoneDesc37)
		public List<SourceMdlBone31> theBones;
		//Public theFlexControllers As List(Of SourceMdlFlexController)
		//Public theFlexDescs As List(Of SourceMdlFlexDesc)
		//Public theFlexRules As List(Of SourceMdlFlexRule)
		public List<SourceMdlHitbox31> theHitboxes_MDL27to30;
		public List<SourceMdlHitboxSet31> theHitboxSets;
		//Public theIkChains As List(Of SourceMdlIkChain37)
		//Public theIkLocks As List(Of SourceMdlIkLock37)
		//Public theMouths As List(Of SourceMdlMouth)
		//Public thePoseParamDescs As List(Of SourceMdlPoseParamDesc)
		public List<SourceMdlSequenceDesc31> theSequenceDescs;
		public List<SourceMdlSequenceGroup31> theSequenceGroups;
		public List<List<short>> theSkinFamilies;
		public string theSurfacePropName;
		public List<string> theTexturePaths;
		public List<SourceMdlTexture31> theTextures;
		//Public theTransitions As List(Of List(Of Integer))

		public bool theModelCommandIsUsed;
		//Public theProceduralBonesCommandIsUsed As Boolean

		public SortedList<string, int> theBoneNameToBoneIndexMap = new SortedList<string, int>();
		//Public theEyelidFlexFrameIndexes As List(Of Integer)
		public SourceMdlAnimationDesc31 theFirstAnimationDesc;
		public SortedList<int, AnimationFrameLine> theFirstAnimationDescFrameLines = new SortedList<int, AnimationFrameLine>();
		//Public theFlexFrames As List(Of FlexFrame)
		//Public theWeightLists As New List(Of SourceMdlWeightList)()

	}

}