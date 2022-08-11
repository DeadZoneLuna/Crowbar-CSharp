using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlFileData14 : SourceMdlFileDataBase
	{
		public SourceMdlFileData14()
		{
			theChecksumIsValid = false;

			eyePosition = new SourceVector();
			hullMinPosition = new SourceVector();
			hullMaxPosition = new SourceVector();
			viewBoundingBoxMinPosition = new SourceVector();
			viewBoundingBoxMaxPosition = new SourceVector();
		}

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

		public int unknown01;

		public int subModelCount;

		public int vertexCount;
		public int indexCount;
		public int indexOffset;
		public int vertexOffset;
		public int normalOffset;
		public int uvOffset;
		public int unknown08;
		//Public unknown09 As Integer
		public int weightingWeightOffset;
		//Public unknown10 As Integer
		public int weightingBoneOffset;
		public int unknown11;

		public int[] subModelOffsets = new int[48];


		//Public theID As String
		//Public theName As String

		public List<SourceMdlAttachment10> theAttachments;
		public List<SourceMdlBodyPart14> theBodyParts;
		public List<SourceMdlBone10> theBones;
		//Public theBones As List(Of SourceMdlBone10Single)
		public List<SourceMdlBoneController10> theBoneControllers;
		public List<SourceMdlHitbox10> theHitboxes;
		public List<UInt16> theIndexes;
		public List<SourceVector> theNormals;
		public List<SourceMdlSequenceDesc10> theSequences;
		public List<SourceMdlSequenceGroupFileHeader10> theSequenceGroupFileHeaders;
		public List<SourceMdlSequenceGroup10> theSequenceGroups;
		public List<List<short>> theSkinFamilies;
		public List<SourceMdlTexture14> theTextures;
		public List<List<byte>> theTransitions;
		public List<SourceVector> theUVs;
		public List<SourceVector> theVertexes;
		public List<SourceMdlWeighting14> theWeightings;

		public List<SourceBoneTransform10> theBoneTransforms;
		//Public theBoneTransforms As List(Of SourceBoneTransform10Single)

		public List<string> theSmdFileNames;

	}

}