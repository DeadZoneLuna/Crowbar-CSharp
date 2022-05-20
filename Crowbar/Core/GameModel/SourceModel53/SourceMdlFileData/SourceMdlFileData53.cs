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
	public class SourceMdlFileData53 : SourceMdlFileDataBase
	{
		public SourceMdlFileData53() : base()
		{

			//NOTE: Set an extremely large number so that the calculations later will work well.
			this.theSectionFrameMinFrameCount = 2000000;

			//Me.theMdlFileOnlyHasAnimations = False
			this.theFirstAnimationDesc = null;
			this.theFirstAnimationDescFrameLines = new SortedList<int, AnimationFrameLine>();
			this.theWeightLists = new List<SourceMdlWeightList>();
		}

		//FROM: SourceEngineAlienSwarm_source\public\studio.h
		//#define STUDIO_VERSION		49
		//struct studiohdr_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					id;
		//	int					version;

		//	long				checksum;		// this has to be the same in the phy and vtx files to load!

		//	inline const char *	pszName( void ) const { if (studiohdr2index && pStudioHdr2()->pszName()) return pStudioHdr2()->pszName(); else return name; }
		//	char				name[64];

		//	int					length;

		//	Vector				eyeposition;	// ideal eye position

		//	Vector				illumposition;	// illumination center

		//	Vector				hull_min;		// ideal movement hull size
		//	Vector				hull_max;			

		//	Vector				view_bbmin;		// clipping bounding box
		//	Vector				view_bbmax;		

		//	int					flags;

		//	int					numbones;			// bones
		//	int					boneindex;
		//	inline mstudiobone_t *pBone( int i ) const { Assert( i >= 0 && i < numbones); return (mstudiobone_t *)(((byte *)this) + boneindex) + i; };
		//	int					RemapSeqBone( int iSequence, int iLocalBone ) const;	// maps local sequence bone to global bone
		//	int					RemapAnimBone( int iAnim, int iLocalBone ) const;		// maps local animations bone to global bone

		//	int					numbonecontrollers;		// bone controllers
		//	int					bonecontrollerindex;
		//	inline mstudiobonecontroller_t *pBonecontroller( int i ) const { Assert( i >= 0 && i < numbonecontrollers); return (mstudiobonecontroller_t *)(((byte *)this) + bonecontrollerindex) + i; };

		//	int					numhitboxsets;
		//	int					hitboxsetindex;

		//	// Look up hitbox set by index
		//	mstudiohitboxset_t	*pHitboxSet( int i ) const 
		//	{ 
		//		Assert( i >= 0 && i < numhitboxsets); 
		//		return (mstudiohitboxset_t *)(((byte *)this) + hitboxsetindex ) + i; 
		//	};

		//	// Calls through to hitbox to determine size of specified set
		//	inline mstudiobbox_t *pHitbox( int i, int set ) const 
		//	{ 
		//		mstudiohitboxset_t const *s = pHitboxSet( set );
		//		if ( !s )
		//			return NULL;

		//		return s->pHitbox( i );
		//	};

		//	// Calls through to set to get hitbox count for set
		//	inline int			iHitboxCount( int set ) const
		//	{
		//		mstudiohitboxset_t const *s = pHitboxSet( set );
		//		if ( !s )
		//			return 0;

		//		return s->numhitboxes;
		//	};

		//	// file local animations? and sequences
		////private:
		//	int					numlocalanim;			// animations/poses
		//	int					localanimindex;		// animation descriptions
		//  	inline mstudioanimdesc_t *pLocalAnimdesc( int i ) const { if (i < 0 || i >= numlocalanim) i = 0; return (mstudioanimdesc_t *)(((byte *)this) + localanimindex) + i; };

		//	int					numlocalseq;				// sequences
		//	int					localseqindex;
		//  	inline mstudioseqdesc_t *pLocalSeqdesc( int i ) const { if (i < 0 || i >= numlocalseq) i = 0; return (mstudioseqdesc_t *)(((byte *)this) + localseqindex) + i; };

		////public:
		//	bool				SequencesAvailable() const;
		//	int					GetNumSeq() const;
		//	mstudioanimdesc_t	&pAnimdesc( int i ) const;
		//	mstudioseqdesc_t	&pSeqdesc( int i ) const;
		//	int					iRelativeAnim( int baseseq, int relanim ) const;	// maps seq local anim reference to global anim index
		//	int					iRelativeSeq( int baseseq, int relseq ) const;		// maps seq local seq reference to global seq index

		////private:
		//	mutable int			activitylistversion;	// initialization flag - have the sequences been indexed?
		//	mutable int			eventsindexed;
		////public:
		//	int					GetSequenceActivity( int iSequence );
		//	void				SetSequenceActivity( int iSequence, int iActivity );
		//	int					GetActivityListVersion( void );
		//	void				SetActivityListVersion( int version ) const;
		//	int					GetEventListVersion( void );
		//	void				SetEventListVersion( int version );

		//	// raw textures
		//	int					numtextures;
		//	int					textureindex;
		//	inline mstudiotexture_t *pTexture( int i ) const { Assert( i >= 0 && i < numtextures ); return (mstudiotexture_t *)(((byte *)this) + textureindex) + i; }; 


		//	// raw textures search paths
		//	int					numcdtextures;
		//	int					cdtextureindex;
		//	inline char			*pCdtexture( int i ) const { return (((char *)this) + *((int *)(((byte *)this) + cdtextureindex) + i)); };

		//	// replaceable textures tables
		//	int					numskinref;
		//	int					numskinfamilies;
		//	int					skinindex;
		//	inline short		*pSkinref( int i ) const { return (short *)(((byte *)this) + skinindex) + i; };

		//	int					numbodyparts;		
		//	int					bodypartindex;
		//	inline mstudiobodyparts_t	*pBodypart( int i ) const { return (mstudiobodyparts_t *)(((byte *)this) + bodypartindex) + i; };

		//	// queryable attachable points
		////private:
		//	int					numlocalattachments;
		//	int					localattachmentindex;
		//	inline mstudioattachment_t	*pLocalAttachment( int i ) const { Assert( i >= 0 && i < numlocalattachments); return (mstudioattachment_t *)(((byte *)this) + localattachmentindex) + i; };
		////public:
		//	int					GetNumAttachments( void ) const;
		//	const mstudioattachment_t &pAttachment( int i ) const;
		//	int					GetAttachmentBone( int i );
		//	// used on my tools in hlmv, not persistant
		//	void				SetAttachmentBone( int iAttachment, int iBone );

		//	// animation node to animation node transition graph
		////private:
		//	int					numlocalnodes;
		//	int					localnodeindex;
		//	int					localnodenameindex;
		//	inline char			*pszLocalNodeName( int iNode ) const { Assert( iNode >= 0 && iNode < numlocalnodes); return (((char *)this) + *((int *)(((byte *)this) + localnodenameindex) + iNode)); }
		//	inline byte			*pLocalTransition( int i ) const { Assert( i >= 0 && i < (numlocalnodes * numlocalnodes)); return (byte *)(((byte *)this) + localnodeindex) + i; };

		////public:
		//	int					EntryNode( int iSequence );
		//	int					ExitNode( int iSequence );
		//	char				*pszNodeName( int iNode );
		//	int					GetTransition( int iFrom, int iTo ) const;

		//	int					numflexdesc;
		//	int					flexdescindex;
		//	inline mstudioflexdesc_t *pFlexdesc( int i ) const { Assert( i >= 0 && i < numflexdesc); return (mstudioflexdesc_t *)(((byte *)this) + flexdescindex) + i; };

		//	int					numflexcontrollers;
		//	int					flexcontrollerindex;
		//	inline mstudioflexcontroller_t *pFlexcontroller( LocalFlexController_t i ) const { Assert( i >= 0 && i < numflexcontrollers); return (mstudioflexcontroller_t *)(((byte *)this) + flexcontrollerindex) + i; };

		//	int					numflexrules;
		//	int					flexruleindex;
		//	inline mstudioflexrule_t *pFlexRule( int i ) const { Assert( i >= 0 && i < numflexrules); return (mstudioflexrule_t *)(((byte *)this) + flexruleindex) + i; };

		//	int					numikchains;
		//	int					ikchainindex;
		//	inline mstudioikchain_t *pIKChain( int i ) const { Assert( i >= 0 && i < numikchains); return (mstudioikchain_t *)(((byte *)this) + ikchainindex) + i; };

		//	int					nummouths;
		//	int					mouthindex;
		//	inline mstudiomouth_t *pMouth( int i ) const { Assert( i >= 0 && i < nummouths); return (mstudiomouth_t *)(((byte *)this) + mouthindex) + i; };

		////private:
		//	int					numlocalposeparameters;
		//	int					localposeparamindex;
		//	inline mstudioposeparamdesc_t *pLocalPoseParameter( int i ) const { Assert( i >= 0 && i < numlocalposeparameters); return (mstudioposeparamdesc_t *)(((byte *)this) + localposeparamindex) + i; };
		////public:
		//	int					GetNumPoseParameters( void ) const;
		//	const mstudioposeparamdesc_t &pPoseParameter( int i );
		//	int					GetSharedPoseParameter( int iSequence, int iLocalPose ) const;

		//	int					surfacepropindex;
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropindex; }
		//	inline int			GetSurfaceProp() const { return surfacepropLookup; }

		//	// Key values
		//	int					keyvalueindex;
		//	int					keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }

		//	int					numlocalikautoplaylocks;
		//	int					localikautoplaylockindex;
		//	inline mstudioiklock_t *pLocalIKAutoplayLock( int i ) const { Assert( i >= 0 && i < numlocalikautoplaylocks); return (mstudioiklock_t *)(((byte *)this) + localikautoplaylockindex) + i; };

		//	int					GetNumIKAutoplayLocks( void ) const;
		//	const mstudioiklock_t &pIKAutoplayLock( int i );
		//	int					CountAutoplaySequences() const;
		//	int					CopyAutoplaySequences( unsigned short *pOut, int outCount ) const;
		//	int					GetAutoplayList( unsigned short **pOut ) const;

		//	// The collision model mass that jay wanted
		//	float				mass;
		//	int					contents;

		//	// external animations, models, etc.
		//	int					numincludemodels;
		//	int					includemodelindex;
		//	inline mstudiomodelgroup_t *pModelGroup( int i ) const { Assert( i >= 0 && i < numincludemodels); return (mstudiomodelgroup_t *)(((byte *)this) + includemodelindex) + i; };
		//	// implementation specific call to get a named model
		//	const studiohdr_t	*FindModel( void **cache, char const *modelname ) const;

		//	// implementation specific back pointer to virtual data
		//	mutable void		*virtualModel;
		//	virtualmodel_t		*GetVirtualModel( void ) const;

		//	// for demand loaded animation blocks
		//	int					szanimblocknameindex;	
		//	inline char * const pszAnimBlockName( void ) const { return ((char *)this) + szanimblocknameindex; }
		//	int					numanimblocks;
		//	int					animblockindex;
		//	inline mstudioanimblock_t *pAnimBlock( int i ) const { Assert( i > 0 && i < numanimblocks); return (mstudioanimblock_t *)(((byte *)this) + animblockindex) + i; };
		//	mutable void		*animblockModel;
		//	byte *				GetAnimBlock( int i ) const;

		//	int					bonetablebynameindex;
		//	inline const byte	*GetBoneTableSortedByName() const { return (byte *)this + bonetablebynameindex; }

		//	// used by tools only that don't cache, but persist mdl's peer data
		//	// engine uses virtualModel to back link to cache pointers
		//	void				*pVertexBase;
		//	void				*pIndexBase;

		//	// if STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT is set,
		//	// this value is used to calculate directional components of lighting 
		//	// on static props
		//	byte				constdirectionallightdot;

		//	// set during load of mdl data to track *desired* lod configuration (not actual)
		//	// the *actual* clamped root lod is found in studiohwdata
		//	// this is stored here as a global store to ensure the staged loading matches the rendering
		//	byte				rootLOD;

		//	// set in the mdl data to specify that lod configuration should only allow first numAllowRootLODs
		//	// to be set as root LOD:
		//	//	numAllowedRootLODs = 0	means no restriction, any lod can be set as root lod.
		//	//	numAllowedRootLODs = N	means that lod0 - lod(N-1) can be set as root lod, but not lodN or lower.
		//	byte				numAllowedRootLODs;

		//	byte				unused[1];

		//	int					unused4; // zero out if version < 47

		//	int					numflexcontrollerui;
		//	int					flexcontrolleruiindex;
		//	mstudioflexcontrollerui_t *pFlexControllerUI( int i ) const { Assert( i >= 0 && i < numflexcontrollerui); return (mstudioflexcontrollerui_t *)(((byte *)this) + flexcontrolleruiindex) + i; }

		//	float				flVertAnimFixedPointScale;
		//	inline float		VertAnimFixedPointScale() const { return ( flags & STUDIOHDR_FLAGS_VERT_ANIM_FIXED_POINT_SCALE ) ? flVertAnimFixedPointScale : 1.0f / 4096.0f; }

		//	mutable int			surfacepropLookup;	// this index must be cached by the loader, not saved in the file

		//	// FIXME: Remove when we up the model version. Move all fields of studiohdr2_t into studiohdr_t.
		//	int					studiohdr2index;
		//	studiohdr2_t*		pStudioHdr2() const { return (studiohdr2_t *)( ( (byte *)this ) + studiohdr2index ); }

		//	// Src bone transforms are transformations that will convert .dmx or .smd-based animations into .mdl-based animations
		//	int					NumSrcBoneTransforms() const { return studiohdr2index ? pStudioHdr2()->numsrcbonetransform : 0; }
		//	const mstudiosrcbonetransform_t* SrcBoneTransform( int i ) const { Assert( i >= 0 && i < NumSrcBoneTransforms()); return (mstudiosrcbonetransform_t *)(((byte *)this) + pStudioHdr2()->srcbonetransformindex) + i; }

		//	inline int			IllumPositionAttachmentIndex() const { return studiohdr2index ? pStudioHdr2()->IllumPositionAttachmentIndex() : 0; }

		//	inline float		MaxEyeDeflection() const { return studiohdr2index ? pStudioHdr2()->MaxEyeDeflection() : 0.866f; } // default to cos(30) if not set

		//	inline mstudiolinearbone_t *pLinearBones() const { return studiohdr2index ? pStudioHdr2()->pLinearBones() : NULL; }

		//	inline int			BoneFlexDriverCount() const { return studiohdr2index ? pStudioHdr2()->m_nBoneFlexDriverCount : 0; }
		//	inline const mstudioboneflexdriver_t* BoneFlexDriver( int i ) const { Assert( i >= 0 && i < BoneFlexDriverCount() ); return studiohdr2index ? pStudioHdr2()->pBoneFlexDriver( i ) : NULL; }

		//	// NOTE: No room to add stuff? Up the .mdl file format version 
		//	// [and move all fields in studiohdr2_t into studiohdr_t and kill studiohdr2_t],
		//	// or add your stuff to studiohdr2_t. See NumSrcBoneTransforms/SrcBoneTransform for the pattern to use.
		//	int					unused2[1];

		//	studiohdr_t() {}

		//private:
		//	// No copy constructors allowed
		//	studiohdr_t(const studiohdr_t& vOther);

		//	friend struct virtualmodel_t;
		//};

		//struct studiohdr2_t
		//{
		//	// NOTE: For forward compat, make sure any methods in this struct
		//	// are also available in studiohdr_t so no leaf code ever directly references
		//	// a studiohdr2_t structure
		//	DECLARE_BYTESWAP_DATADESC();
		//	int numsrcbonetransform;
		//	int srcbonetransformindex;

		//	int	illumpositionattachmentindex;
		//	inline int			IllumPositionAttachmentIndex() const { return illumpositionattachmentindex; }

		//	float flMaxEyeDeflection;
		//	inline float		MaxEyeDeflection() const { return flMaxEyeDeflection != 0.0f ? flMaxEyeDeflection : 0.866f; } // default to cos(30) if not set

		//	int linearboneindex;
		//	inline mstudiolinearbone_t *pLinearBones() const { return (linearboneindex) ? (mstudiolinearbone_t *)(((byte *)this) + linearboneindex) : NULL; }

		//	int sznameindex;
		//	inline char *pszName() { return (sznameindex) ? (char *)(((byte *)this) + sznameindex ) : NULL; }

		//	int m_nBoneFlexDriverCount;
		//	int m_nBoneFlexDriverIndex;
		//	inline mstudioboneflexdriver_t *pBoneFlexDriver( int i ) const { Assert( i >= 0 && i < m_nBoneFlexDriverCount ); return (mstudioboneflexdriver_t *)(((byte *)this) + m_nBoneFlexDriverIndex) + i; }

		//	int reserved[56];
		//};



		//Public id(3) As Char
		//Public version As Integer
		//Public checksum As Integer
		public int nameCopyOffset;
		public char[] name = new char[64];
		//Public fileSize As Integer

		//50  Vector				eyeposition;	// ideal eye position
		public float eyePositionX;
		public float eyePositionY;
		public float eyePositionZ;
		//5C  Vector				illumposition;	// illumination center
		public SourceVector illuminationPosition = new SourceVector();
		//68  Vector				hull_min;		// ideal movement hull size
		public float hullMinPositionX;
		public float hullMinPositionY;
		public float hullMinPositionZ;
		//74  Vector				hull_max;			
		public float hullMaxPositionX;
		public float hullMaxPositionY;
		public float hullMaxPositionZ;
		//80  Vector				view_bbmin;		// clipping bounding box
		public float viewBoundingBoxMinPositionX;
		public float viewBoundingBoxMinPositionY;
		public float viewBoundingBoxMinPositionZ;
		//8C  Vector				view_bbmax;		
		public float viewBoundingBoxMaxPositionX;
		public float viewBoundingBoxMaxPositionY;
		public float viewBoundingBoxMaxPositionZ;

		//98  int					flags;
		public int flags;

		//9C  int					numbones;			// bones
		public int boneCount;
		//A0  int					boneindex;
		public int boneOffset;

		//A4  int					numbonecontrollers;		// bone controllers
		public int boneControllerCount;
		//A8  int					bonecontrollerindex;
		public int boneControllerOffset;

		//AC  int					numhitboxsets;
		//B0  int					hitboxsetindex;
		public int hitboxSetCount;
		public int hitboxSetOffset;

		//B4 	int					numlocalanim;			// animations/poses
		public int localAnimationCount;
		//B8 	int					localanimindex;		// animation descriptions
		public int localAnimationOffset;
		//  	inline mstudioanimdesc_t *pLocalAnimdesc( int i ) const { if (i < 0 || i >= numlocalanim) i = 0; return (mstudioanimdesc_t *)(((byte *)this) + localanimindex) + i; };

		//BC 	int					numlocalseq;				// sequences
		public int localSequenceCount;
		//C0 	int					localseqindex;
		public int localSequenceOffset;
		//  	inline mstudioseqdesc_t *pLocalSeqdesc( int i ) const { if (i < 0 || i >= numlocalseq) i = 0; return (mstudioseqdesc_t *)(((byte *)this) + localseqindex) + i; };

		//C4 	mutable int			activitylistversion;	// initialization flag - have the sequences been indexed?
		public int activityListVersion;
		//C8 	mutable int			eventsindexed;
		public int eventsIndexed;

		//	// raw textures
		//CC 	int					numtextures;
		public int textureCount;
		//D0 	int					textureindex;
		public int textureOffset;
		//	inline mstudiotexture_t *pTexture( int i ) const { Assert( i >= 0 && i < numtextures ); return (mstudiotexture_t *)(((byte *)this) + textureindex) + i; }; 

		//	// raw textures search paths
		//D4 	int					numcdtextures;
		public int texturePathCount;
		//D8 	int					cdtextureindex;
		public int texturePathOffset;
		//	inline char			*pCdtexture( int i ) const { return (((char *)this) + *((int *)(((byte *)this) + cdtextureindex) + i)); };

		//	// replaceable textures tables
		//DC 	int					numskinref;
		public int skinReferenceCount;
		//E0 	int					numskinfamilies;
		public int skinFamilyCount;
		//E4 	int					skinindex;
		public int skinFamilyOffset;
		//	inline short		*pSkinref( int i ) const { return (short *)(((byte *)this) + skinindex) + i; };

		//E8 	int					numbodyparts;		
		public int bodyPartCount;
		//EC 	int					bodypartindex;
		public int bodyPartOffset;
		//	inline mstudiobodyparts_t	*pBodypart( int i ) const { return (mstudiobodyparts_t *)(((byte *)this) + bodypartindex) + i; };

		//F0 	int					numlocalattachments;
		public int localAttachmentCount;
		//F4 	int					localattachmentindex;
		public int localAttachmentOffset;

		//F8 	int					numlocalnodes;
		public int localNodeCount;
		//FC 	int					localnodeindex;
		public int localNodeOffset;
		//0100	int					localnodenameindex;
		public int localNodeNameOffset;

		//0104	int					numflexdesc;
		public int flexDescCount;
		//0108	int					flexdescindex;
		public int flexDescOffset;

		//010C	int					numflexcontrollers;
		public int flexControllerCount;
		//0110	int					flexcontrollerindex;
		public int flexControllerOffset;

		//0114	int					numflexrules;
		public int flexRuleCount;
		//0118	int					flexruleindex;
		public int flexRuleOffset;

		//011C	int					numikchains;
		public int ikChainCount;
		//0120	int					ikchainindex;
		public int ikChainOffset;

		//0124	int					nummouths;
		public int mouthCount;
		//0128	int					mouthindex;
		public int mouthOffset;

		//012C	int					numlocalposeparameters;
		public int localPoseParamaterCount;
		//0130	int					localposeparamindex;
		public int localPoseParameterOffset;

		//	int					surfacepropindex;
		public int surfacePropOffset;

		//	int					keyvalueindex;
		public int keyValueOffset;
		//	int					keyvaluesize;
		public int keyValueSize;

		//	int					numlocalikautoplaylocks;
		public int localIkAutoPlayLockCount;
		//	int					localikautoplaylockindex;
		public int localIkAutoPlayLockOffset;

		//	float				mass;
		public float mass;
		//	int					contents;
		public int contents;

		//	int					numincludemodels;
		public int includeModelCount;
		//	int					includemodelindex;
		public int includeModelOffset;

		//	mutable void		*virtualModel;
		public int virtualModelP;

		//	int					szanimblocknameindex;	
		public int animBlockNameOffset;
		//	int					numanimblocks;
		public int animBlockCount;
		//	int					animblockindex;
		public int animBlockOffset;
		//	mutable void		*animblockModel;
		public int animBlockModelP;

		//NOTE: Probably not used or used for something else.
		//	int					bonetablebynameindex;
		public int boneTableByNameOffset;

		//	void				*pVertexBase;
		public int vertexBaseP;
		//	void				*pIndexBase;
		public int indexBaseP;

		//	byte				constdirectionallightdot;
		public byte directionalLightDot;

		//	byte				rootLOD;
		public byte rootLod;

		//	byte				numAllowedRootLODs;
		public byte allowedRootLodCount;

		//	byte				unused[1];
		public byte unused;
		//	int					unused4; // zero out if version < 47
		public int unused4;

		//	int					numflexcontrollerui;
		public int flexControllerUiCount;
		//	int					flexcontrolleruiindex;
		public int flexControllerUiOffset;

		//	float				flVertAnimFixedPointScale;
		//	inline float		VertAnimFixedPointScale() const { return ( flags & STUDIOHDR_FLAGS_VERT_ANIM_FIXED_POINT_SCALE ) ? flVertAnimFixedPointScale : 1.0f / 4096.0f; }
		public double vertAnimFixedPointScale;
		//	mutable int			surfacepropLookup;	// this index must be cached by the loader, not saved in the file
		public int surfacePropLookup;

		//	// FIXME: Remove when we up the model version. Move all fields of studiohdr2_t into studiohdr_t.
		//	int					studiohdr2index;
		public int studioHeader2Offset;
		//	int					unused2[1];
		public int unknownOffset01;

		// sutdiohdr2:
		public int sourceBoneTransformCount;
		public int sourceBoneTransformOffset;
		public int illumPositionAttachmentIndex;
		public double maxEyeDeflection;
		public int linearBoneOffset;

		public int nameOffset;
		public int boneFlexDriverCount;
		public int boneFlexDriverOffset;

		//Public reserved(55) As Integer
		//======
		//Public studiohdr2(63) As Integer
		//======
		public int unknown01;
		public int unknown02;
		public int unknown03;
		public int unknown04;
		public int vtxOffset;
		public int vvdOffset;
		public int unknown05;
		public int phyOffset;

		public int unknown06;
		public int unknown07;
		public int unknown08;
		public int unknown09;
		public int unknownOffset02;
		public int[] unknown = new int[59];



		//Public theID As String
		//Public theName As String
		public string theNameCopy;

		public List<SourceMdlAnimationDesc52> theAnimationDescs;
		public List<SourceMdlAnimBlock> theAnimBlocks;
		public string theAnimBlockRelativePathFileName;
		public List<SourceMdlAttachment> theAttachments;
		public List<SourceMdlBodyPart> theBodyParts;
		public List<SourceMdlBone> theBones;
		public List<SourceMdlBoneController> theBoneControllers;
		public List<int> theBoneTableByName;
		public List<SourceMdlFlexDesc> theFlexDescs;
		public List<SourceMdlFlexController> theFlexControllers;
		public List<SourceMdlFlexControllerUi> theFlexControllerUis;
		public List<SourceMdlFlexRule> theFlexRules;
		public List<SourceMdlHitboxSet> theHitboxSets;
		public List<SourceMdlIkChain> theIkChains;
		public List<SourceMdlIkLock> theIkLocks;
		public string theKeyValuesText;
		public List<string> theLocalNodeNames;
		public List<SourceMdlModelGroup> theModelGroups;
		public List<SourceMdlMouth> theMouths;
		public List<SourceMdlPoseParamDesc> thePoseParamDescs;
		public List<SourceMdlSequenceDesc> theSequenceDescs;
		public List<List<short>> theSkinFamilies;
		public string theSurfacePropName;
		public List<string> theTexturePaths;
		public List<SourceMdlTexture> theTextures;

		public int theSectionFrameCount;
		public int theSectionFrameMinFrameCount;

		//Public theActualFileSize As Long
		public bool theModelCommandIsUsed;
		public int theBodyPartIndexThatShouldUseModelCommand;
		public List<FlexFrame> theFlexFrames;
		public List<int> theEyelidFlexFrameIndexes;
		//Public theUpperEyelidFlexFrameIndexes As List(Of Integer)

		public SourceMdlAnimationDesc52 theFirstAnimationDesc;
		public SortedList<int, AnimationFrameLine> theFirstAnimationDescFrameLines;
		//Public theMdlFileOnlyHasAnimations As Boolean
		public bool theProceduralBonesCommandIsUsed;
		public List<SourceMdlWeightList> theWeightLists;

		public List<SourceMdlBoneTransform> theBoneTransforms;
		public SourceMdlLinearBone theLinearBoneTable;

	}

}