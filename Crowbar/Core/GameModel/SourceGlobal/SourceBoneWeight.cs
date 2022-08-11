using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceBoneWeight
	{

		//// 16 bytes
		//struct mstudioboneweight_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	float	weight[MAX_NUM_BONES_PER_VERT];
		//	char	bone[MAX_NUM_BONES_PER_VERT]; 
		//	byte	numbones;

		////	byte	material;
		////	short	firstref;
		////	short	lastref;
		//};

		//	float	weight[MAX_NUM_BONES_PER_VERT];
		public float[] weight = new float[SourceConstants.MAX_NUM_BONES_PER_VERT];
		//	char	bone[MAX_NUM_BONES_PER_VERT]; 
		public byte[] bone = new byte[SourceConstants.MAX_NUM_BONES_PER_VERT];
		//	byte	numbones;
		public byte boneCount;

	}

}