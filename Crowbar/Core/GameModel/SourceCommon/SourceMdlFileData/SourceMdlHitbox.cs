using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlHitbox
	{

		public SourceMdlHitbox()
		{
			boundingBoxMin = new SourceVector();
			boundingBoxMax = new SourceVector();
			boundingBoxPitchYawRoll = new SourceVector();
		}



		//FROM: VERSION 10
		//// intersection boxes
		//typedef struct
		//{
		//	int					bone;
		//	int					group;			// intersection group
		//	vec3_t				bbmin;		// bounding box
		//	vec3_t				bbmax;		
		//} mstudiobbox_t;



		//FROM: public\studio.h
		//// intersection boxes
		//struct mstudiobbox_t
		//{
		//	int					bone;
		//	int					group;				// intersection group
		//	Vector				bbmin;				// bounding box
		//	Vector				bbmax;	
		//	int					szhitboxnameindex;	// offset to the name of the hitbox.
		//	int					unused[8];

		//	char* pszHitboxName()
		//	{
		//		if( szhitboxnameindex == 0 )
		//			return "";

		//		return ((char*)this) + szhitboxnameindex;
		//	}

		//	mstudiobbox_t() {}

		//private:
		//	// No copy constructors allowed
		//	mstudiobbox_t(const mstudiobbox_t& vOther);
		//};


		public int boneIndex;
		public int groupIndex;
		//Public boundingBoxMinX As Double
		//Public boundingBoxMinY As Double
		//Public boundingBoxMinZ As Double
		public SourceVector boundingBoxMin;
		//Public boundingBoxMaxX As Double
		//Public boundingBoxMaxY As Double
		//Public boundingBoxMaxZ As Double
		public SourceVector boundingBoxMax;
		public int nameOffset;

		public int[] unused = new int[8];
		//------
		//VERSION 49 CSGO compiler requires boundingBoxPitchYawRoll values be written to QC file, otherwise gives this error: "Line X is incomplete."
		//    L4D2 requires boundingBoxPitchYawRoll values NOT be written to QC file, otherwise gives this error: "ERROR: e:\l4d2modelwip\infected_tank\animations\decompiled 0.52\anim_hulk.qc(21): - bad command 0".
		public SourceVector boundingBoxPitchYawRoll;
		public double unknown;
		public int[] unused_VERSION49 = new int[4];



		public string theName;

	}

}