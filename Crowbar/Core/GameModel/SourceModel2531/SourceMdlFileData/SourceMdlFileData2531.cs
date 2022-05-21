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
	public class SourceMdlFileData2531 : SourceMdlFileDataBase
	{
		public SourceMdlFileData2531() : base()
		{

			theModelCommandIsUsed = false;
			theProceduralBonesCommandIsUsed = false;
		}

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//#define STUDIO_VERSION		2531	// READ ABOVE!!!	// f64: ~
		//#define MAXSTUDIOTRIANGLES	25000	// TODO: tune this
		//#define MAXSTUDIOVERTS		25000	// TODO: tune this
		//#define MAXSTUDIOSKINS		32		// total textures
		//#define MAXSTUDIOBONES		1024	// total bones actually used (Psycho-A: < 128)
		//#define MAXSTUDIOBLENDS		16		// f64: ~New
		//#define MAXSTUDIOFLEXDESC	128
		//#define MAXSTUDIOFLEXCTRL	64
		//#define MAXSTUDIOPOSEPARAM	24
		//#define MAXSTUDIOBONECTRLS	4
		//#define MAXSTUDIOBONEBITS	10		// NOTE: MUST MATCH MAXSTUDIOBONES (Psycho-A: < 7)
		//struct studiohdr_t
		//{
		//	int					id;
		//	int					version;
		//
		//	long				checksum;		// this has to be the same in the phy and vtx files to load!
		//	
		//	char				name[128];		// f64: ~
		//	int					length;
		//
		//	Vector				eyeposition;	// ideal eye position
		//
		//	Vector				illumposition;	// illumination center
		//
		//// f64: add new vars
		//	int					gh[2];
		//
		//	float				PosZ; // 176
		//	float				PosX; // 180
		//	float				PosY; // 184
		//
		//	float				RotX; // 188
		//	float				RotY; // 192
		//	float				RotZ; // 196
		//
		////	Vector				unkvec;
		//// ---
		//	
		////	Vector				hull_min;			// f64: - // ideal movement hull size
		////	Vector				hull_max;			// f64: - 
		//
		//	Vector				view_bbmin;			// clipping bounding box
		//	Vector				view_bbmax;		
		//
		//	int					yty;	// f64: +
		//	
		//	int					flags;	// f64: 228
		//	
		//	int					unz[2];	// f64: +
		//
		//	int					numbones;			// bones
		//	int					boneindex;
		//	inline mstudiobone_t *pBone( int i ) const { return (mstudiobone_t *)(((byte *)this) + boneindex) + i; };
		//
		//	int					numbonecontrollers;		// bone controllers
		//	int					bonecontrollerindex;
		//	inline mstudiobonecontroller_t *pBonecontroller( int i ) const { return (mstudiobonecontroller_t *)(((byte *)this) + bonecontrollerindex) + i; };
		//
		//	int					numhitboxsets;
		//	int					hitboxsetindex;
		//
		//
		//
		//	// Look up hitbox set by index
		//	mstudiohitboxset_t	*pHitboxSet( int i ) const 
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
		//	inline int			iHitboxCount( int set ) const
		//	{
		//		mstudiohitboxset_t const *s = pHitboxSet( set );
		//		if ( !s )
		//			return 0;
		//
		//		return s->numhitboxes;
		//	};
		//
		//	/*
		//	int					numhitboxes;			// complex bounding boxes
		//	int					hitboxindex;			
		//	inline mstudiobbox_t *pHitbox( int i ) const { return (mstudiobbox_t *)(((byte *)this) + hitboxindex) + i; };
		//	*/
		//	
		//	int					numanim;			// animations/poses
		//	int					animdescindex;		// animation descriptions
		//	inline mstudioanimdesc_t *pAnimdesc( int i ) const { return (mstudioanimdesc_t *)(((byte *)this) + animdescindex) + i; };
		//
		//	int					numseq;				// sequences
		//	int					seqindex;
		//	inline mstudioseqdesc_t *pSeqdesc( int i ) const { if (i < 0 || i >= numseq) i = 0; return (mstudioseqdesc_t *)(((byte *)this) + seqindex) + i; };
		//	int					sequencesindexed;	// initialization flag - have the sequences been indexed?
		//
		//	int					numseqgroups;		// demand loaded sequences
		//	int					seqgroupindex;
		//
		//	int					numtextures;		// raw textures
		//	int					textureindex;
		//	inline mstudiotexture_t *pTexture( int i ) const { return (mstudiotexture_t *)(((byte *)this) + textureindex) + i; }; 
		//
		//	int					numcdtextures;		// raw textures search paths
		//	int					cdtextureindex;
		//	inline char			*pCdtexture( int i ) const { return (((char *)this) + *((int *)(((byte *)this) + cdtextureindex) + i)); };
		//
		//	int					numskinref;			// replaceable textures tables
		//	int					numskinfamilies;
		//	int					skinindex;
		//	inline short		*pSkinref( int i ) const { return (short *)(((byte *)this) + skinindex) + i; };
		//
		//	int					numbodyparts;		
		//	int					bodypartindex;
		//	inline mstudiobodyparts_t	*pBodypart( int i ) const { return (mstudiobodyparts_t *)(((byte *)this) + bodypartindex) + i; };
		//
		//	int					numattachments;		// queryable attachable points
		//	int					attachmentindex;
		//	inline mstudioattachment_t	*pAttachment( int i ) const { return (mstudioattachment_t *)(((byte *)this) + attachmentindex) + i; };
		//
		//	int					numtransitions;		// animation node to animation node transition graph
		//	int					transitionindex;
		//	inline byte	*pTransition( int i ) const { return (byte *)(((byte *)this) + transitionindex) + i; };
		//
		//	int					numflexdesc;
		//	int					flexdescindex;
		//	inline mstudioflexdesc_t *pFlexdesc( int i ) const { return (mstudioflexdesc_t *)(((byte *)this) + flexdescindex) + i; };
		//
		//	int					numflexcontrollers;
		//	int					flexcontrollerindex;
		//	inline mstudioflexcontroller_t *pFlexcontroller( int i ) const { return (mstudioflexcontroller_t *)(((byte *)this) + flexcontrollerindex) + i; };
		//
		//	int					numflexrules;
		//	int					flexruleindex;
		//	inline mstudioflexrule_t *pFlexRule( int i ) const { return (mstudioflexrule_t *)(((byte *)this) + flexruleindex) + i; };
		//
		//	int					numikchains;
		//	int					ikchainindex;
		//	inline mstudioikchain_t *pIKChain( int i ) const { return (mstudioikchain_t *)(((byte *)this) + ikchainindex) + i; };
		//
		//	int					nummouths;
		//	int					mouthindex;
		//	inline mstudiomouth_t *pMouth( int i ) const { return (mstudiomouth_t *)(((byte *)this) + mouthindex) + i; };
		//
		//	int					numposeparameters;
		//	int					poseparamindex;
		//	inline mstudioposeparamdesc_t *pPoseParameter( int i ) const { return (mstudioposeparamdesc_t *)(((byte *)this) + poseparamindex) + i; };
		//
		//	int					surfacepropindex;
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropindex; }
		//
		//// f64: Unknown
		//	int					numunk;
		//	int					unkindex;
		//	inline mstudiounk_t *pUnk( int i ) const { return (mstudiounk_t *)(((byte *)this) + unkindex) + i; };
		//
		//	// external animations, models, etc. 
		//	int					numincludemodels;
		//	int					includemodelindex;
		//	inline mstudiomodelgroup_t *pModelGroup( int i ) const { return (mstudiomodelgroup_t *)(((byte *)this) + includemodelindex) + i; };
		//
		//
		//	int					unhz[3];
		//// ----
		//
		//// f64: -
		////	// Key values
		////	int					keyvalueindex;
		////	int					keyvaluesize;
		////	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }
		//
		////	int					numikautoplaylocks;
		////	int					ikautoplaylockindex;
		////	inline mstudioiklock_t *pIKAutoplayLock( int i ) const { return (mstudioiklock_t *)(((byte *)this) + ikautoplaylockindex) + i; };
		//
		////	float				mass;				// The collision model mass that jay wanted
		////	int					contents;
		////	int					unused[9];		// remove as appropriate
		//};


		//Public id(3) As Char
		//Public version As Integer
		//Public checksum As Integer
		public char[] name = new char[128];
		//Public fileSize As Integer

		public SourceVector eyePosition = new SourceVector();
		public SourceVector illuminationPosition = new SourceVector();

		public double unknown01;
		public double unknown02;
		public double unknown03;

		public SourceVector hullMinPosition = new SourceVector();
		public SourceVector hullMaxPosition = new SourceVector();
		public SourceVector viewBoundingBoxMinPosition = new SourceVector();
		public SourceVector viewBoundingBoxMaxPosition = new SourceVector();

		public int flags;

		public int unknown04;
		public int unknown05;

		public int boneCount;
		public int boneOffset;
		public int boneControllerCount;
		public int boneControllerOffset;

		public int hitBoxSetCount;
		public int hitBoxSetOffset;

		public int localAnimationCount;
		public int localAnimationOffset;
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

		public int unknownCount;
		public int unknownOffset;

		//Public localIkAutoPlayLockCount As Integer
		//Public localIkAutoPlayLockOffset As Integer

		//'	int AnimblockNameIndex;
		//Public animBlockNameOffset As Integer

		//'	int NumFlexControllerUI;
		//'	int FlexControllerUIIndex;
		//Public flexControllerUiCount As Integer
		//Public flexControllerUiOffset As Integer

		public int includeModelCount;
		public int includeModelOffset;

		public int unknown06;
		public int unknown07;
		public int unknown08;

		//Public boneTableByNameOffset As Integer

		//'	int NumAnimblocks;
		//'	int AnimblockIndex;
		//Public animBlockCount As Integer
		//Public animBlockOffset As Integer

		//'	byte ConstDirectionalLightDOT;
		//'	byte RootLOD;
		//'	byte NumAllowedRootLODs;
		//'	byte				unused[1];
		//Public directionalLightDot As Byte
		//Public rootLod As Byte
		//Public allowedRootLodCount As Byte
		//Public unused As Byte



		//Public theID As String
		//Public theName As String

		public List<SourceMdlAnimationDesc2531> theAnimationDescs;
		public List<SourceMdlAttachment2531> theAttachments;
		public List<SourceMdlBodyPart2531> theBodyParts;
		public List<SourceMdlBone2531> theBones;
		public List<SourceMdlBoneController2531> theBoneControllers;
		public List<SourceMdlFlexDesc> theFlexDescs;
		public List<SourceMdlFlexController> theFlexControllers;
		//Public theFlexControllerUis As List(Of SourceMdlFlexControllerUi)
		public List<SourceMdlFlexRule> theFlexRules;
		public List<SourceMdlHitboxSet2531> theHitboxSets;
		public List<SourceMdlIncludeModel2531> theIncludeModels;
		//Public theNodes As List(Of SourceMdlNode2531)
		public List<SourceMdlPoseParamDesc2531> thePoseParamDescs;
		//Public theSequenceGroupFileHeaders As List(Of SourceMdlSequenceGroupFileHeader2531)
		public List<SourceMdlSequenceGroup2531> theSequenceGroups;
		public List<SourceMdlSequenceDesc2531> theSequences;
		public List<List<short>> theSkinFamilies;
		public string theSurfacePropName;
		public List<string> theTexturePaths;
		public List<SourceMdlTexture2531> theTextures;

		public List<FlexFrame2531> theFlexFrames;
		public bool theModelCommandIsUsed;
		public bool theProceduralBonesCommandIsUsed;

		public SortedList<string, int> theBoneNameToBoneIndexMap = new SortedList<string, int>();

		//#define STUDIOHDR_FLAGS_STATIC_PROP				( 1 << 4 )
		public const int STUDIOHDR_FLAGS_STATIC_PROP = 1 << 4;

	}

}