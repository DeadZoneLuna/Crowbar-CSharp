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
	public class SourceMdlIkLink
	{

		//// ikinfo
		//struct mstudioiklink_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int		bone;
		//	Vector	kneeDir;	// ideal bending direction (per link, if applicable)
		//	Vector	unused0;	// unused

		//	mstudioiklink_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudioiklink_t(const mstudioiklink_t& vOther);
		//};


		//	int		bone;
		public int boneIndex;
		//	Vector	kneeDir;	// ideal bending direction (per link, if applicable)
		public SourceVector idealBendingDirection = new SourceVector();
		//	Vector	unused0;	// unused
		public SourceVector unused0 = new SourceVector();

	}

}