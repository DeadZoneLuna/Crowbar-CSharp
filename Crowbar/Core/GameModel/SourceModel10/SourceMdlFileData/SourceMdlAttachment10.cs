using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlAttachment10
	{

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



		public char[] name = new char[32];
		public int type;
		public int boneIndex;
		public SourceVector attachmentPoint;
		public SourceVector[] vectors = new SourceVector[3];



		public string theName;

	}

}