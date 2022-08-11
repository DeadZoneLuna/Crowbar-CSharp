using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlQuatInterpInfo2531
	{

		//FROM: Bloodlines SDK source 2015-06-16\sdk-src (16.06.2015)\src\public\studio.h
		//struct mstudioquatinterpinfo_t
		//{
		//	float			inv_tolerance;	// 1 / radian angle of trigger influence
		//	Quaternion		trigger;	// angle to match
		//	Vector			pos;		// new position
		//	Quaternion		quat;		// new angle
		//};

		public double inverseToleranceAngle;
		public SourceQuaternion trigger = new SourceQuaternion();
		public SourceVector pos = new SourceVector();
		public SourceQuaternion quat = new SourceQuaternion();

	}

}