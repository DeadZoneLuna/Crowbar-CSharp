using System;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	public class FlexFrame
	{
		public string flexName;
		public string flexDescription;
		public bool flexHasPartner;
		public string flexPartnerName;
		public double flexSplit;
		public List<int> bodyAndMeshVertexIndexStarts;
		public List<SourceMdlFlex> flexes;
		public List<int> meshIndexes;
	}

}