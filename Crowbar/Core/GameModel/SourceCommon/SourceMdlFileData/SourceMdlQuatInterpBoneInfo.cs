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
	public class SourceMdlQuatInterpBoneInfo
	{

		//FROM: SourceEngineXXXX_source\public\studio.h

		//struct mstudioquatinterpinfo_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	float			inv_tolerance;	// 1 / radian angle of trigger influence
		//	Quaternion		trigger;	// angle to match
		//	Vector			pos;		// new position
		//	Quaternion		quat;		// new angle
		//
		//	mstudioquatinterpinfo_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudioquatinterpinfo_t(const mstudioquatinterpinfo_t& vOther);
		//};



		public double inverseToleranceAngle;
		public SourceQuaternion trigger;
		public SourceVector pos;
		public SourceQuaternion quat;

	}

}