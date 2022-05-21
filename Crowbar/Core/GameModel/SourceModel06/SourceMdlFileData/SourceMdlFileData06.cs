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
	public class SourceMdlFileData06 : SourceMdlFileDataBase
	{
		public SourceMdlFileData06() : base()
		{

			theChecksumIsValid = false;
		}

		//Public Sub New()
		//	Me.eyePosition = New SourceVector()
		//	Me.hullMinPosition = New SourceVector()
		//	Me.hullMaxPosition = New SourceVector()
		//	Me.viewBoundingBoxMinPosition = New SourceVector()
		//	Me.viewBoundingBoxMaxPosition = New SourceVector()
		//End Sub

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//#define STUDIO_VERSION		6
		//// little-endian "IDST"
		//#define IDSTUDIOHEADER	(('T'<<24)+('S'<<16)+('D'<<8)+'I')
		//typedef struct
		//{
		//	int					id;
		//	int					version;
		//
		//	char				name[64];
		//	// 48h
		//	int					length;
		//
		//	// 4Ch
		//	int					numbones;				// bones
		//	int					boneindex;				// (->BCh)
		//
		//	// 54h
		//	int					numbonecontrollers;		// bone controllers
		//	// TOMAS: turret.mdl has 2
		//	int					bonecontrollerindex;	// if num == 0 then this points to bones! not controlers!
		//
		//	// 5Ch
		//	int					numseq;					// animation sequences
		//	int					seqindex;
		//
		//	// 64h
		//	int					numtextures;			// raw textures
		//	int					textureindex;
		//	int					texturedataindex;
		//
		//	// 70h
		//	int					numskinref;				// replaceable textures
		//	int					numskinfamilies;
		//	int					skinindex;
		//
		//	// 7Ch
		//	int					numbodyparts;
		//	int					bodypartindex;			// (->mstudiobodyparts_t)
		//
		//	int					unused[14];				// TOMAS: UNUSED (checked)
		//
		//} studiohdr_t;

		//Public id(3) As Char
		//Public version As Integer
		public char[] name = new char[64];
		//' length of mdl file in bytes
		//Public fileSize As Integer

		public int boneCount;
		public int boneOffset;

		public int boneControllerCount;
		public int boneControllerOffset;

		public int sequenceCount;
		public int sequenceOffset;

		public int textureCount;
		public int textureOffset;
		public int textureDataOffset;

		public int skinReferenceCount;
		public int skinFamilyCount;
		public int skinOffset;

		public int bodyPartCount;
		public int bodyPartOffset;

		public int[] unused = new int[14];


		//Public theID As String
		//Public theName As String

		public List<SourceMdlBodyPart06> theBodyParts;
		public List<SourceMdlBone06> theBones;
		public List<SourceMdlBoneController06> theBoneControllers;
		public List<SourceMdlSequenceDesc06> theSequences;
		public List<List<short>> theSkins;
		public List<SourceMdlTexture06> theTextures;

		public List<SourceBoneTransform06> theBoneTransforms;

	}

}