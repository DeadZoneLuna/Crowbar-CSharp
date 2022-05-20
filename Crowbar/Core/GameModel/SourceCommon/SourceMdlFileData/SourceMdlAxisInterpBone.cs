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
	public class SourceMdlAxisInterpBone
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//struct mstudioaxisinterpbone_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int				control;// local transformation of this bone used to calc 3 point blend
		//	int				axis;	// axis to check
		//	Vector			pos[6];	// X+, X-, Y+, Y-, Z+, Z-
		//	Quaternion		quat[6];// X+, X-, Y+, Y-, Z+, Z-
		//
		//	mstudioaxisinterpbone_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudioaxisinterpbone_t(const mstudioaxisinterpbone_t& vOther);
		//};



		public SourceMdlAxisInterpBone()
		{
			for (int i = 0; i < pos.Length; i++)
			{
				pos[i] = new SourceVector();
			}
			for (int i = 0; i < quat.Length; i++)
			{
				quat[i] = new SourceQuaternion();
			}
		}



		public int control;
		public int axis;
		public SourceVector[] pos = new SourceVector[6];
		public SourceQuaternion[] quat = new SourceQuaternion[6];

	}

}