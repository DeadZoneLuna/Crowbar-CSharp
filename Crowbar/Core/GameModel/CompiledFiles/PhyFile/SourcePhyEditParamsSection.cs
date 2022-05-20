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
	public class SourcePhyEditParamsSection
	{

		//fprintf( fp, "editparams {\n" );
		//KeyWriteString( fp, "rootname", g_JointedModel.m_rootName );
		//KeyWriteFloat( fp, "totalmass", g_JointedModel.m_totalMass );

		// Example: 
		//editparams {
		//"rootname" "valvebiped.bip01_pelvis"
		//"totalmass" "100.000000"
		//}

		// Example from HL2 beta leak alyx.phy:
		//editparams {
		//"rootname" "valvebiped.bip01_pelvis"
		//"totalmass" "60.000000"
		//"jointmerge" "valvebiped.bip01_pelvis,valvebiped.bip01"
		//"jointmerge" "valvebiped.bip01_pelvis,valvebiped.bip01_spine1"
		//}

		public string concave;
		public Dictionary<string, List<string>> jointMergeMap;
		public string rootName;
		public float totalMass;

	}

}