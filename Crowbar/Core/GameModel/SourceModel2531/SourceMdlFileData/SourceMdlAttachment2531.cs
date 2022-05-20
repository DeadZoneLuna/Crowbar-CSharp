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
	public class SourceMdlAttachment2531
	{

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudioattachment_t
		//{
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int					type;
		//	int					bone;
		//	matrix3x4_t			local; // attachment point
		//};
		//------
		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// attachment
		//typedef struct 
		//{
		//	char				name[32];
		//	int					type;
		//	int					bone;
		//	vec3_t				org;	// attachment point
		//	vec3_t				vectors[3];
		//} mstudioattachment_t;

		public int nameOffset;
		public int type;
		public int boneIndex;

		//Public attachmentPointColumn0 As New SourceVector()
		//Public attachmentPointColumn1 As New SourceVector()
		//Public attachmentPointColumn2 As New SourceVector()
		//Public attachmentPointColumn3 As New SourceVector()
		//Public attachmentPoint As New SourceVector()
		//Public vector01 As New SourceVector()
		//Public vector02 As New SourceVector()
		//Public vector03 As New SourceVector()
		//------
		//      float cX = bytesToFloat(this.file, cOffset + 24);
		//      float cY = bytesToFloat(this.file, cOffset + 40);
		//      float cZ = bytesToFloat(this.file, cOffset + 56);
		//      float cXX = bytesToFloat(this.file, cOffset + 12);
		//      float cYX = bytesToFloat(this.file, cOffset + 28);
		//      float cZX = bytesToFloat(this.file, cOffset + 44);
		//      float cZY = bytesToFloat(this.file, cOffset + 48);
		//      float cZZ = bytesToFloat(this.file, cOffset + 52);
		public double cXX; //12
		public double unused01; //16
		public double unused02; //20
		public double posX; //24

		public double cYX; //28
		public double unused03; //32
		public double unused04; //36
		public double posY; //40

		public double cZX; //44
		public double cZY; //48
		public double cZZ; //52
		public double posZ; //56

		public string theName;

	}

}