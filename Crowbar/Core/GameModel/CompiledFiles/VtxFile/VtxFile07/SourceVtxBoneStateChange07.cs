using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVtxBoneStateChange07
	{

		//FROM: src/public/optimize.h
		//struct BoneStateChangeHeader_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int hardwareID;
		//	int newBoneID;
		//};

		public int hardwareId;
		public int newBoneId;

	}

}