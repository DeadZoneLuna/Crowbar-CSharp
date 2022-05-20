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
	public class SourceMdlQuatInterpBone
	{

		//FROM: SourceEngineXXXX_source\public\studio.h

		//struct mstudioquatinterpbone_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int				control;// local transformation to check
		//	int				numtriggers;
		//	int				triggerindex;
		//	inline mstudioquatinterpinfo_t *pTrigger( int i ) const { return  (mstudioquatinterpinfo_t *)(((byte *)this) + triggerindex) + i; };
		//
		//	mstudioquatinterpbone_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudioquatinterpbone_t(const mstudioquatinterpbone_t& vOther);
		//};



		public int controlBoneIndex;
		public int triggerCount;
		public int triggerOffset;

		public List<SourceMdlQuatInterpBoneInfo> theTriggers;

	}

}