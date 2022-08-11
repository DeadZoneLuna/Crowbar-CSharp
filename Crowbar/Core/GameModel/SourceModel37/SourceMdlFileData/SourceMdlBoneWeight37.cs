using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlBoneWeight37
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
		//// 16 bytes
		//struct mstudioboneweight_t
		//{
		//	float	weight[4];
		//	short	bone[4]; 
		//
		//	short	numbones;
		//	short	material;
		//
		//	short	firstref;
		//	short	lastref;
		//};

		public double[] weight = new double[4];
		public short[] bone = new short[4];
		public short boneCount;
		public short material;
		public short firstRef;
		public short lastRef;

	}

}