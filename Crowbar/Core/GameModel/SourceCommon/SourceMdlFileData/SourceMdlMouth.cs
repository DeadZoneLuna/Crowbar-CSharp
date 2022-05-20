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
	public class SourceMdlMouth
	{

		//struct mstudiomouth_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					bone;
		//	Vector				forward;
		//	int					flexdesc;

		//	mstudiomouth_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudiomouth_t(const mstudiomouth_t& vOther);
		//};

		//	int					bone;
		public int boneIndex;
		//	Vector				forward;
		public SourceVector forward = new SourceVector();
		//	int					flexdesc;
		public int flexDescIndex;

	}

}