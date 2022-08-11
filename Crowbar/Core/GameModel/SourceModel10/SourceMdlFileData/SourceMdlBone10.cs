using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBone10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// bones
		//typedef struct 
		//{
		//	char				name[32];	// bone name for symbolic links
		//	int		 			parent;		// parent bone
		//	int					flags;		// ??
		//	int					bonecontroller[6];	// bone controller index, -1 == none
		//	float				value[6];	// default DoF values
		//	float				scale[6];   // scale for delta DoF values
		//} mstudiobone_t;



		//	char				name[32];	// bone name for symbolic links
		public char[] name = new char[32];

		//	int		 			parent;		// parent bone
		public int parentBoneIndex;

		//	int					flags;		// ??
		public int flags;

		//	int					bonecontroller[6];	// bone controller index, -1 == none
		public int[] boneControllerIndex = new int[6];

		//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\write.c
		//		pbone[i].value[0]		= bonetable[i].pos[0];
		//		pbone[i].value[1]		= bonetable[i].pos[1];
		//		pbone[i].value[2]		= bonetable[i].pos[2];
		//		pbone[i].value[3]		= bonetable[i].rot[0];
		//		pbone[i].value[4]		= bonetable[i].rot[1];
		//		pbone[i].value[5]		= bonetable[i].rot[2];
		//		pbone[i].scale[0]		= bonetable[i].posscale[0];
		//		pbone[i].scale[1]		= bonetable[i].posscale[1];
		//		pbone[i].scale[2]		= bonetable[i].posscale[2];
		//		pbone[i].scale[3]		= bonetable[i].rotscale[0];
		//		pbone[i].scale[4]		= bonetable[i].rotscale[1];
		//		pbone[i].scale[5]		= bonetable[i].rotscale[2];
		//------
		//	float				value[6];	// default DoF values
		//	float				scale[6];   // scale for delta DoF values
		public SourceVector position;
		public SourceVector rotation;
		public SourceVector positionScale;
		public SourceVector rotationScale;



		public string theName;

	}

}