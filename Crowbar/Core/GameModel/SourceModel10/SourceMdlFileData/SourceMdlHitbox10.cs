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
	public class SourceMdlHitbox10
	{

		public SourceMdlHitbox10()
		{
			boundingBoxMin = new SourceVector();
			boundingBoxMax = new SourceVector();
		}



		//// intersection boxes
		//typedef struct
		//{
		//	int					bone;
		//	int					group;			// intersection group
		//	vec3_t				bbmin;		// bounding box
		//	vec3_t				bbmax;		
		//} mstudiobbox_t;



		public int boneIndex;
		public int groupIndex;
		public SourceVector boundingBoxMin;
		public SourceVector boundingBoxMax;

	}

}