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
	public class SourceVtxBodyPart06
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
		//struct BodyPartHeader_t
		//{
		//	int numModels;
		//	int modelOffset;
		//	inline ModelHeader_t *pModel( int i ) const 
		//	{ 
		//		ModelHeader_t *pDebug = (ModelHeader_t *)(((byte *)this) + modelOffset) + i;
		//		return pDebug;
		//	};
		//};

		public int modelCount;
		public int modelOffset;

		public List<SourceVtxModel06> theVtxModels;

	}

}