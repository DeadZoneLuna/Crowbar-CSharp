using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBone31
	{

		public int nameOffset;
		public int parentBoneIndex;

		public int[] boneControllerIndex = new int[6];

		public SourceVector position = new SourceVector();
		public SourceVector rotation = new SourceVector();
		public SourceVector positionScale = new SourceVector();
		public SourceVector rotationScale = new SourceVector();

		public SourceVector poseToBoneColumn0 = new SourceVector();
		public SourceVector poseToBoneColumn1 = new SourceVector();
		public SourceVector poseToBoneColumn2 = new SourceVector();
		public SourceVector poseToBoneColumn3 = new SourceVector();

		//Public qAlignment As SourceQuaternion

		public int flags;

		public int proceduralRuleType;
		public int proceduralRuleOffset;
		public int physicsBoneIndex;
		public int surfacePropNameOffset;

		//Public quat As SourceQuaternion

		//Public contents As Integer

		//Public unused(2) As Integer

		public SourceMdlAxisInterpBone theAxisInterpBone;
		public string theName;
		public SourceMdlQuatInterpBone theQuatInterpBone;
		public string theSurfacePropName;


		//#define STUDIO_PROC_AXISINTERP	1
		//#define STUDIO_PROC_QUATINTERP	2
		public const int STUDIO_PROC_AXISINTERP = 1;
		public const int STUDIO_PROC_QUATINTERP = 2;

	}

}