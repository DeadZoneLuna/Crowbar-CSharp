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
	public class FlexFrame37
	{

		public string flexName;
		public string flexDescription;
		//NOTE: Confirmed that MDL v37 does not have "flexpair" option. 
		//      No "flexpair" found in MDL v37 studiomdl.exe but was found in MDL v44 (Half-Life 2) studiomdl.exe.
		//      Also, SourceMdlFlex37 does not have the "flexDescPartnerIndex" field.
		//Public flexHasPartner As Boolean
		public double flexSplit;
		public List<int> bodyAndMeshVertexIndexStarts;
		public List<SourceMdlFlex37> flexes;
		public List<int> meshIndexes;

	}

}