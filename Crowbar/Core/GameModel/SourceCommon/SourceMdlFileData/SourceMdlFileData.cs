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
	public class SourceMdlFileData : SourceFileData
	{
		public SourceMdlFileData()
		{
			//NOTE: Set an extremely large number so that the calculations later will work well.
			theSectionFrameMinFrameCount = 2000000;

			theMdlFileOnlyHasAnimations = false;
			theFirstAnimationDesc = null;
			theFirstAnimationDescFrameLines = new SortedList<int, AnimationFrameLine>();
			theWeightLists = new List<SourceMdlWeightList>();
		}

		//FROM: SourceEngine2006_source\public\studio.h
		//#define STUDIO_VERSION		44
		//struct studiohdr_t
		//{
		//	int					id;
		//	int					version;

		//	long				checksum;		// this has to be the same in the phy and vtx files to load!

		//	inline const char *	pszName( void ) const { return name; }
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
		//	int					GetActivityListVersion( void ) const;
		//	void				SetActivityListVersion( int version ) const;
		//	int					GetEventListVersion( void ) const;
		//	void				SetEventListVersion( int version ) const;

		//	// raw textures
		//	int					numtextures;
		//	int					textureindex;
		//	inline mstudiotexture_t *pTexture( int i ) const { return (mstudiotexture_t *)(((byte *)this) + textureindex) + i; }; 


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
		//	int					GetAttachmentBone( int i ) const;
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
		//	int					EntryNode( int iSequence ) const;
		//	int					ExitNode( int iSequence ) const;
		//	char				*pszNodeName( int iNode ) const;
		//	int					GetTransition( int iFrom, int iTo ) const;

		//	int					numflexdesc;
		//	int					flexdescindex;
		//	inline mstudioflexdesc_t *pFlexdesc( int i ) const { Assert( i >= 0 && i < numflexdesc); return (mstudioflexdesc_t *)(((byte *)this) + flexdescindex) + i; };

		//	int					numflexcontrollers;
		//	int					flexcontrollerindex;
		//	inline mstudioflexcontroller_t *pFlexcontroller( int i ) const { Assert( i >= 0 && i < numflexcontrollers); return (mstudioflexcontroller_t *)(((byte *)this) + flexcontrollerindex) + i; };

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
		//	const mstudioposeparamdesc_t &pPoseParameter( int i ) const;
		//	int					GetSharedPoseParameter( int iSequence, int iLocalPose ) const;

		//	int					surfacepropindex;
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropindex; }

		//	// Key values
		//	int					keyvalueindex;
		//	int					keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }

		//	int					numlocalikautoplaylocks;
		//	int					localikautoplaylockindex;
		//	inline mstudioiklock_t *pLocalIKAutoplayLock( int i ) const { Assert( i >= 0 && i < numlocalikautoplaylocks); return (mstudioiklock_t *)(((byte *)this) + localikautoplaylockindex) + i; };

		//	int					GetNumIKAutoplayLocks( void ) const;
		//	const mstudioiklock_t &pIKAutoplayLock( int i ) const;
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

		//	byte				unused[2];

		//	int					zeroframecacheindex;
		//	byte				*pZeroframeCache( int i ) const { if (zeroframecacheindex) return (byte *)this + ((int *)(((byte *)this) + zeroframecacheindex))[i]; else return NULL; }

		//	int					unused2[6];		// remove as appropriate

		//	studiohdr_t() {}

		//private:
		//	// No copy constructors allowed
		//	studiohdr_t(const studiohdr_t& vOther);

		//	friend struct virtualmodel_t;
		//};

		//===========================================================================

		//FROM: SourceEngineXXXX_source\public\studio.h
		//#define STUDIO_VERSION		48
		//struct studiohdr_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					id;
		//	int					version;

		//	long				checksum;		// this has to be the same in the phy and vtx files to load!

		//	inline const char *	pszName( void ) const { return name; }
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
		//	int					GetActivityListVersion( void ) const;
		//	void				SetActivityListVersion( int version ) const;
		//	int					GetEventListVersion( void ) const;
		//	void				SetEventListVersion( int version ) const;

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
		//	int					GetAttachmentBone( int i ) const;
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
		//	int					EntryNode( int iSequence ) const;
		//	int					ExitNode( int iSequence ) const;
		//	char				*pszNodeName( int iNode ) const;
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
		//	const mstudioposeparamdesc_t &pPoseParameter( int i ) const;
		//	int					GetSharedPoseParameter( int iSequence, int iLocalPose ) const;

		//	int					surfacepropindex;
		//	inline char * const pszSurfaceProp( void ) const { return ((char *)this) + surfacepropindex; }

		//	// Key values
		//	int					keyvalueindex;
		//	int					keyvaluesize;
		//	inline const char * KeyValueText( void ) const { return keyvaluesize != 0 ? ((char *)this) + keyvalueindex : NULL; }

		//	int					numlocalikautoplaylocks;
		//	int					localikautoplaylockindex;
		//	inline mstudioiklock_t *pLocalIKAutoplayLock( int i ) const { Assert( i >= 0 && i < numlocalikautoplaylocks); return (mstudioiklock_t *)(((byte *)this) + localikautoplaylockindex) + i; };

		//	int					GetNumIKAutoplayLocks( void ) const;
		//	const mstudioiklock_t &pIKAutoplayLock( int i ) const;
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

		//	int					unused3[2];

		//	// FIXME: Remove when we up the model version. Move all fields of studiohdr2_t into studiohdr_t.
		//	int					studiohdr2index;
		//	studiohdr2_t*		pStudioHdr2() const { return (studiohdr2_t *)( ( (byte *)this ) + studiohdr2index ); }

		//	// Src bone transforms are transformations that will convert .dmx or .smd-based animations into .mdl-based animations
		//	int					NumSrcBoneTransforms() const { return studiohdr2index ? pStudioHdr2()->numsrcbonetransform : 0; }
		//	const mstudiosrcbonetransform_t* SrcBoneTransform( int i ) const { Assert( i >= 0 && i < NumSrcBoneTransforms()); return (mstudiosrcbonetransform_t *)(((byte *)this) + pStudioHdr2()->srcbonetransformindex) + i; }

		//	inline int			IllumPositionAttachmentIndex() const { return studiohdr2index ? pStudioHdr2()->IllumPositionAttachmentIndex() : 0; }

		//	inline float		MaxEyeDeflection() const { return studiohdr2index ? pStudioHdr2()->MaxEyeDeflection() : 0.866f; } // default to cos(30) if not set

		//	inline mstudiolinearbone_t *pLinearBones() const { return studiohdr2index ? pStudioHdr2()->pLinearBones() : NULL; }

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

		//NOTE: studiohdr2_t is 256 bytes
		//// NOTE! Next time we up the .mdl file format, remove studiohdr2_t
		//// and insert all fields in this structure into studiohdr_t.
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

		//	int reserved[59];
		//};


		// Field names are based on those found in Source engine's studio.h file for the struct, studiohdr_t.

		// 00  4 bytes
		public char[] id = new char[4];
		// 04  4-byte integer
		public int version;
		// 08  4-byte integer
		public int checksum;
		// 0C  64 bytes
		public char[] name = new char[64];
		//FROM: VERSION 2531 (VtMB)
		public char[] nameForVtmb = new char[129];
		// 4C  length of mdl file in bytes
		public int fileSize;

		//50  Vector				eyeposition;	// ideal eye position
		public float eyePositionX;
		public float eyePositionY;
		public float eyePositionZ;
		//5C  Vector				illumposition;	// illumination center
		public float illuminationPositionX;
		public float illuminationPositionY;
		public float illuminationPositionZ;
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

		//FROM: VERSION 10
		//int					numhitboxes;			// complex bounding boxes
		//int					hitboxindex;			
		//======
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

		//FROM: VERSION 10
		//int					numseqgroups;		// demand loaded sequences
		//int					seqgroupindex;
		public int sequenceGroupCount;
		public int sequenceGroupOffset;

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

		// VERSION 10:
		//int					soundtable;
		//int					soundindex;
		//int					soundgroups;
		//int					soundgroupindex;
		public int soundtable;
		public int soundindex;
		public int soundgroups;
		public int soundgroupindex;

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



		//============
		// Different between version 44 and 48.


		// VERSION 44: 
		//	byte				unused[2];
		//------------
		// VERSION 48:
		//	byte				numAllowedRootLODs;
		//	byte				unused[1];
		public byte allowedRootLodCount_VERSION48;
		public byte unused;


		// VERSION 44: 
		//	int					zeroframecacheindex;
		public int zeroframecacheindex_VERSION44_47;
		//------------
		// VERSION 48:
		//	int					unused4; // zero out if version < 47
		public int unused4;


		// VERSION 44: 
		//	int					unused2[6];		// remove as appropriate
		//------------
		// VERSION 48:
		//	int					numflexcontrollerui;
		public int flexControllerUiCount_VERSION48;
		//	int					flexcontrolleruiindex;
		public int flexControllerUiOffset_VERSION48;
		//	int					unused3[2];
		public int[] unused3 = new int[2];
		//	int					studiohdr2index;
		public int studioHeader2Offset_VERSION48;
		//	int					unused2[1];
		public int unused2;


		//============



		// studiohdr2:
		public int sourceBoneTransformCount;
		public int sourceBoneTransformOffset;
		public int illumPositionAttachmentIndex;
		public double maxEyeDeflection;
		public int linearBoneOffset;
		public int[] reserved = new int[59];
		//======
		//Public studiohdr2(63) As Integer


		public string theID;
		public string theName;

		public List<SourceMdlAnimationDesc> theAnimationDescs;
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
		public List<List<int>> theSkinFamilies;
		public string theSurfacePropName;
		public List<string> theTexturePaths;
		public List<SourceMdlTexture> theTextures;

		//Public theUnknownValues As List(Of UnknownValue)

		public int theSectionFrameCount;
		public int theSectionFrameMinFrameCount;

		public long theActualFileSize;
		public bool theModelCommandIsUsed;
		public List<FlexFrame> theFlexFrames;
		public List<int> theEyelidFlexFrameIndexes;
		//Public theUpperEyelidFlexFrameIndexes As List(Of Integer)

		public SourceMdlAnimationDesc theFirstAnimationDesc;
		public SortedList<int, AnimationFrameLine> theFirstAnimationDescFrameLines;
		public bool theMdlFileOnlyHasAnimations;
		public bool theProceduralBonesCommandIsUsed;
		public List<SourceMdlWeightList> theWeightLists;

		//Public theFileSeekLog As FileSeekLog





		//// This flag is set if no hitbox information was specified
		//#define STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX	( 1 << 0 )

		//// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
		//// models when we change materials.
		//#define STUDIOHDR_FLAGS_USES_ENV_CUBEMAP		( 1 << 1 )

		//// Use this when there are translucent parts to the model but we're not going to sort it 
		//#define STUDIOHDR_FLAGS_FORCE_OPAQUE			( 1 << 2 )

		//// Use this when we want to render the opaque parts during the opaque pass
		//// and the translucent parts during the translucent pass
		//#define STUDIOHDR_FLAGS_TRANSLUCENT_TWOPASS		( 1 << 3 )

		//// This is set any time the .qc files has $staticprop in it
		//// Means there's no bones and no transforms
		//#define STUDIOHDR_FLAGS_STATIC_PROP				( 1 << 4 )

		//// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
		//// models when we change materials.
		//#define STUDIOHDR_FLAGS_USES_FB_TEXTURE		    ( 1 << 5 )

		//// This flag is set by studiomdl.exe if a separate "$shadowlod" entry was present
		////  for the .mdl (the shadow lod is the last entry in the lod list if present)
		//#define STUDIOHDR_FLAGS_HASSHADOWLOD			( 1 << 6 )

		//// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
		//// models when we change materials.
		//#define STUDIOHDR_FLAGS_USES_BUMPMAPPING		( 1 << 7 )

		//// NOTE:  This flag is set when we should use the actual materials on the shadow LOD
		//// instead of overriding them with the default one (necessary for translucent shadows)
		//#define STUDIOHDR_FLAGS_USE_SHADOWLOD_MATERIALS	( 1 << 8 )

		//// NOTE:  This flag is set when we should use the actual materials on the shadow LOD
		//// instead of overriding them with the default one (necessary for translucent shadows)
		//#define STUDIOHDR_FLAGS_OBSOLETE				( 1 << 9 )

		//#define STUDIOHDR_FLAGS_UNUSED					( 1 << 10 )

		//// NOTE:  This flag is set at mdl build time
		//#define STUDIOHDR_FLAGS_NO_FORCED_FADE			( 1 << 11 )

		//// NOTE:  The npc will lengthen the viseme check to always include two phonemes
		//#define STUDIOHDR_FLAGS_FORCE_PHONEME_CROSSFADE	( 1 << 12 )

		//// This flag is set when the .qc has $constantdirectionallight in it
		//// If set, we use constantdirectionallightdot to calculate light intensity
		//// rather than the normal directional dot product
		//// only valid if STUDIOHDR_FLAGS_STATIC_PROP is also set
		//#define STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT ( 1 << 13 )

		//// Flag to mark delta flexes as already converted from disk format to memory format
		//#define STUDIOHDR_FLAGS_FLEXES_CONVERTED		( 1 << 14 )

		// VERSION 44:
		//NOTE: This is obvious-wrong copy-pasted comment and the value for the constant is different for v48, but the flag is set when $ambientboost QC command is used.
		//// Flag to mark delta flexes as already converted from disk format to memory format
		//#define STUDIOHDR_FLAGS_AMBIENT_BOOST			( 1 << 15 )
		//======
		// VERSION 48:
		//// Indicates the studiomdl was built in preview mode
		//#define STUDIOHDR_FLAGS_BUILT_IN_PREVIEW_MODE	( 1 << 15 )
		//// Ambient boost (runtime flag)
		//#define STUDIOHDR_FLAGS_AMBIENT_BOOST			( 1 << 16 )
		//// Don't cast shadows from this model (useful on first-person models)
		//#define STUDIOHDR_FLAGS_DO_NOT_CAST_SHADOWS		( 1 << 17 )
		//// alpha textures should cast shadows in vrad on this model (ONLY prop_static!)
		//#define STUDIOHDR_FLAGS_CAST_TEXTURE_SHADOWS	( 1 << 18 )

		// VERSION 49:
		//FROM: [49] AlienSwarm_source\src\public\studio.h
		//// Model has a quad-only Catmull-Clark SubD cage
		//#define STUDIOHDR_FLAGS_SUBDIVISION_SURFACE		( 1 << 19 )
		//
		//// flagged on load to indicate no animation events on this model
		//#define STUDIOHDR_FLAGS_NO_ANIM_EVENTS			( 1 << 20 )
		//
		//// If flag is set then studiohdr_t.flVertAnimFixedPointScale contains the
		//// scale value for fixed point vert anim data, if not set then the
		//// scale value is the default of 1.0 / 4096.0.  Regardless use
		//// studiohdr_t::VertAnimFixedPointScale() to always retrieve the scale value
		//#define STUDIOHDR_FLAGS_VERT_ANIM_FIXED_POINT_SCALE	( 1 << 21 )



		public const int STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX = 1 << 0;
		//Public Const STUDIOHDR_FLAGS_USES_ENV_CUBEMAP As Integer = 1 << 1
		public const int STUDIOHDR_FLAGS_FORCE_OPAQUE = 1 << 2;
		public const int STUDIOHDR_FLAGS_TRANSLUCENT_TWOPASS = 1 << 3;
		public const int STUDIOHDR_FLAGS_STATIC_PROP = 1 << 4;
		//Public Const STUDIOHDR_FLAGS_USES_FB_TEXTURE As Integer = 1 << 5
		//NOTE: Not needed because the LOD "switchPoint" is always -1 for a shadowlod.
		public const int STUDIOHDR_FLAGS_HASSHADOWLOD = 1 << 6;
		//Public Const STUDIOHDR_FLAGS_USES_BUMPMAPPING As Integer = 1 << 7
		public const int STUDIOHDR_FLAGS_USE_SHADOWLOD_MATERIALS = 1 << 8;
		public const int STUDIOHDR_FLAGS_OBSOLETE = 1 << 9;
		//Public Const STUDIOHDR_FLAGS_UNUSED As Integer = 1 << 10
		public const int STUDIOHDR_FLAGS_NO_FORCED_FADE = 1 << 11;
		public const int STUDIOHDR_FLAGS_FORCE_PHONEME_CROSSFADE = 1 << 12;
		public const int STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT = 1 << 13;
		//Public Const STUDIOHDR_FLAGS_FLEXES_CONVERTED As Integer = 1 << 14
		//TODO: Give user a warning message if this is used by the model.
		//FROM: [48] SourceEngine2007_source se2007_src\src_main\utils\studiomdl\optimize.cpp
		//      COptimizedModel::Stripify()
		//// Skip the tristripping phase if we're building in preview mode
		public const int STUDIOHDR_FLAGS_BUILT_IN_PREVIEW_MODE = 1 << 15;
		public const int STUDIOHDR_FLAGS_AMBIENT_BOOST_MDL44 = 1 << 15;
		public const int STUDIOHDR_FLAGS_AMBIENT_BOOST = 1 << 16;
		public const int STUDIOHDR_FLAGS_DO_NOT_CAST_SHADOWS = 1 << 17;
		public const int STUDIOHDR_FLAGS_CAST_TEXTURE_SHADOWS = 1 << 18;
		public const int STUDIOHDR_FLAGS_SUBDIVISION_SURFACE = 1 << 19;
		public const int STUDIOHDR_FLAGS_NO_ANIM_EVENTS = 1 << 20;
		public const int STUDIOHDR_FLAGS_VERT_ANIM_FIXED_POINT_SCALE = 1 << 21;

	}

}