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
	public class SourceMdlBoneDesc37
	{

		//struct mstudiobonedesc_t
		//{
		//	int 	sznameindex;
		//	inline  char * const	pszName( void ) const {	return ((char *)this) + sznameindex; };
		//
		//	int parent;		// parent bone
		//
		//	// FIXME: remove the damn default value fields and put in pos
		//	float value[6]; // default DoF values
		//	float scale[6]; // scale for delta DoF values
		//	matrix3x4_t poseToBone;
		//
		//	float fivefloat[5];
		////	Quaternion	qAlignment;
		//
		////	int		unused[3];		// remove as appropriate
		//};

		public int nameOffset;
		public int parentBoneIndex;

		//Public value(5) As Double
		//Public scale(5) As Double
		public SourceVector position;
		public SourceVector rotation;
		public SourceVector positionScale;
		public SourceVector rotationScale;

		//	matrix3x4_t			poseToBone;
		public SourceVector poseToBoneColumn0;
		public SourceVector poseToBoneColumn1;
		public SourceVector poseToBoneColumn2;
		public SourceVector poseToBoneColumn3;

		public float[] unused = new float[5];

		public string theName;

	}

}