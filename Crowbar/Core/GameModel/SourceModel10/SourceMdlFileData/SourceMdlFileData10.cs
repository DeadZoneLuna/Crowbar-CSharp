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
	public class SourceMdlFileData10 : SourceMdlFileDataBase
	{
		public SourceMdlFileData10()
		{
			theChecksumIsValid = false;

			eyePosition = new SourceVector();
			hullMinPosition = new SourceVector();
			hullMaxPosition = new SourceVector();
			viewBoundingBoxMinPosition = new SourceVector();
			viewBoundingBoxMaxPosition = new SourceVector();
		}

		//FROM: GoldSourceEngine2002_source\halflife-master\utils\studiomdl\studiomdl.h
		//#define STUDIO_VERSION	10
		//#define IDSTUDIOHEADER	(('T'<<24)+('S'<<16)+('D'<<8)+'I')
		//														// little-endian "IDST"

		//FROM: GoldSourceEngine2002_source\halflife-master\engine\studio.h
		//#define MAXSTUDIOTRIANGLES	20000	// TODO: tune this
		//#define MAXSTUDIOVERTS		2048	// TODO: tune this
		//#define MAXSTUDIOSEQUENCES	2048	// total animation sequences -- KSH incremented
		//#define MAXSTUDIOSKINS		100		// total textures
		//#define MAXSTUDIOSRCBONES	512		// bones allowed at source movement
		//#define MAXSTUDIOBONES		128		// total bones actually used
		//#define MAXSTUDIOMODELS		32		// sub-models per model
		//#define MAXSTUDIOBODYPARTS	32
		//#define MAXSTUDIOGROUPS		16
		//#define MAXSTUDIOANIMATIONS	2048		
		//#define MAXSTUDIOMESHES		256
		//#define MAXSTUDIOEVENTS		1024
		//#define MAXSTUDIOPIVOTS		256
		//#define MAXSTUDIOCONTROLLERS 8

		//FROM: GoldSourceEngine2002_source\halflife-master\engine\studio.h
		//typedef struct 
		//{
		//	int					id;
		//	int					version;
		//
		//	char				name[64];
		//	int					length;
		//
		//	vec3_t				eyeposition;	// ideal eye position
		//	vec3_t				min;			// ideal movement hull size
		//	vec3_t				max;			
		//
		//	vec3_t				bbmin;			// clipping bounding box
		//	vec3_t				bbmax;		
		//
		//	int					flags;
		//
		//	int					numbones;			// bones
		//	int					boneindex;
		//
		//	int					numbonecontrollers;		// bone controllers
		//	int					bonecontrollerindex;
		//
		//	int					numhitboxes;			// complex bounding boxes
		//	int					hitboxindex;			
		//
		//	int					numseq;				// animation sequences
		//	int					seqindex;
		//
		//	int					numseqgroups;		// demand loaded sequences
		//	int					seqgroupindex;
		//
		//	int					numtextures;		// raw textures
		//	int					textureindex;
		//	int					texturedataindex;
		//
		//	int					numskinref;			// replaceable textures
		//	int					numskinfamilies;
		//	int					skinindex;
		//
		//	int					numbodyparts;		
		//	int					bodypartindex;
		//
		//	int					numattachments;		// queryable attachable points
		//	int					attachmentindex;
		//
		//	int					soundtable;
		//	int					soundindex;
		//	int					soundgroups;
		//	int					soundgroupindex;
		//
		//	int					numtransitions;		// animation node to animation node transition graph
		//	int					transitionindex;
		//} studiohdr_t;



		//Public id(3) As Char
		//Public version As Integer

		public char[] name = new char[64];
		//Public fileSize As Integer

		//	vec3_t				eyeposition;	// ideal eye position
		public SourceVector eyePosition;
		//	vec3_t				min;			// ideal movement hull size
		public SourceVector hullMinPosition;
		//	vec3_t				max;			
		public SourceVector hullMaxPosition;

		//	vec3_t				bbmin;			// clipping bounding box
		public SourceVector viewBoundingBoxMinPosition;
		//	vec3_t				bbmax;		
		public SourceVector viewBoundingBoxMaxPosition;

		//	int					flags;
		public int flags;

		//	int					numbones;			// bones
		public int boneCount;
		//	int					boneindex;
		public int boneOffset;

		//	int					numbonecontrollers;		// bone controllers
		public int boneControllerCount;
		//	int					bonecontrollerindex;
		public int boneControllerOffset;

		//	int					numhitboxes;			// complex bounding boxes
		public int hitboxCount;
		//	int					hitboxindex;			
		public int hitboxOffset;

		//	int					numseq;				// animation sequences
		public int sequenceCount;
		//	int					seqindex;
		public int sequenceOffset;

		//	int					numseqgroups;		// demand loaded sequences
		public int sequenceGroupCount;
		//	int					seqgroupindex;
		public int sequenceGroupOffset;

		//	int					numtextures;		// raw textures
		public int textureCount;
		//	int					textureindex;
		public int textureOffset;
		//	int					texturedataindex;
		public int textureDataOffset;

		//	int					numskinref;			// replaceable textures
		public int skinReferenceCount;
		//	int					numskinfamilies;
		public int skinFamilyCount;
		//	int					skinindex;
		public int skinOffset;

		//	int					numbodyparts;		
		public int bodyPartCount;
		//	int					bodypartindex;
		public int bodyPartOffset;

		//	int					numattachments;		// queryable attachable points
		public int attachmentCount;
		//	int					attachmentindex;
		public int attachmentOffset;

		//	int					soundtable;
		//	int					soundindex;
		//	int					soundgroups;
		//	int					soundgroupindex;
		public int soundTable;
		public int soundOffset;
		public int soundGroups;
		public int soundGroupOffset;

		//	int					numtransitions;		// animation node to animation node transition graph
		//	int					transitionindex;
		public int transitionCount;
		public int transitionOffset;



		//Public theID As String
		//Public theName As String

		public List<SourceMdlAttachment10> theAttachments;
		public List<SourceMdlBodyPart10> theBodyParts;
		public List<SourceMdlBone10> theBones;
		//Public theBones As List(Of SourceMdlBone10Single)
		public List<SourceMdlBoneController10> theBoneControllers;
		public List<SourceMdlHitbox10> theHitboxes;
		public List<SourceMdlSequenceDesc10> theSequences;
		public List<SourceMdlSequenceGroupFileHeader10> theSequenceGroupFileHeaders;
		public List<SourceMdlSequenceGroup10> theSequenceGroups;
		public List<List<short>> theSkinFamilies;
		public List<SourceMdlTexture10> theTextures;
		public List<List<byte>> theTransitions;

		public List<SourceBoneTransform10> theBoneTransforms;
		//Public theBoneTransforms As List(Of SourceBoneTransform10Single)

		public List<string> theSmdFileNames;

	}

}