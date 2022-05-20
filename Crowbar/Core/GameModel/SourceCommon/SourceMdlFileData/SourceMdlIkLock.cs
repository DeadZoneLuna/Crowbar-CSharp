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
	public class SourceMdlIkLock
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//struct mstudioiklock_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int			chain;
		//	float		flPosWeight;
		//	float		flLocalQWeight;
		//	int			flags;
		//
		//	int			unused[4];
		//};



		public int chainIndex;
		public double posWeight;
		public double localQWeight;
		public int flags;

		public int[] unused = new int[4];

	}

}