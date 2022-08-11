using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlLocalHierarchy
	{

		//FROM: se2007_src\src_main\public\studio.h
		//struct mstudiolocalhierarchy_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int			iBone;			// bone being adjusted
		//	int			iNewParent;		// the bones new parent

		//	float		start;			// beginning of influence
		//	float		peak;			// start of full influence
		//	float		tail;			// end of full influence
		//	float		end;			// end of all influence

		//	int			iStart;			// first frame 

		//	int			localanimindex;
		//	inline mstudiocompressedikerror_t *pLocalAnim() const { return (mstudiocompressedikerror_t *)(((byte *)this) + localanimindex); };

		//	int			unused[4];
		//};



		public int boneIndex;
		public int boneNewParentIndex;

		public float startInfluence;
		public float peakInfluence;
		public float tailInfluence;
		public float endInfluence;

		public int startFrameIndex;

		public int localAnimOffset;

		public int[] unused = new int[4];


		public List<SourceMdlCompressedIkError> theLocalAnims;

	}

}