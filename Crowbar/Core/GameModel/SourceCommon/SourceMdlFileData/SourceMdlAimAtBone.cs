using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlAimAtBone
	{

		//struct mstudioaimatbone_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//
		//	int				parent;
		//	int				aim;		// Might be bone or attach
		//	Vector			aimvector;
		//	Vector			upvector;
		//	Vector			basepos;
		//
		//	mstudioaimatbone_t() {}
		//private:
		//	// No copy constructors allowed
		//	mstudioaimatbone_t(const mstudioaimatbone_t& vOther);
		//};

		public int parentBoneIndex;
		public int aimBoneOrAttachmentIndex;

		public SourceVector aim;
		public SourceVector up;
		public SourceVector basePos;

	}

}