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
	public class SourceMdlAttachment
	{

		//FROM: VERSION 10
		//// attachment
		//typedef struct 
		//{
		//	char				name[32];
		//	int					type;
		//	int					bone;
		//	vec3_t				org;	// attachment point
		//	vec3_t				vectors[3];
		//} mstudioattachment_t;



		public char[] name = new char[32];
		public int type;
		public int bone;
		public SourceVector attachmentPoint;
		public SourceVector[] vectors = new SourceVector[3];



		//struct mstudioattachment_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	unsigned int		flags;
		//	int					localbone;
		//	matrix3x4_t			local; // attachment point
		//	int					unused[8];
		//};



		//	int					sznameindex;
		public int nameOffset;
		//	unsigned int		flags;
		public int flags;
		//	int					localbone;
		public int localBoneIndex;
		//	matrix3x4_t			local; // attachment point
		//NOTE: Not sure this is correct row-column order.
		public float localM11;
		public float localM12;
		public float localM13;
		public float localM14;
		public float localM21;
		public float localM22;
		public float localM23;
		public float localM24;
		public float localM31;
		public float localM32;
		public float localM33;
		public float localM34;
		//	int					unused[8];
		public int[] unused = new int[8];



		public string theName;

	}

}