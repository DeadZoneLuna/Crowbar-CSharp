using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlJiggleBone
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//struct mstudiojigglebone_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();

		//	int				flags;

		//	// general params
		//	float			length;					// how from from bone base, along bone, is tip
		//	float			tipMass;

		//	// flexible params
		//	float			yawStiffness;
		//	float			yawDamping;	
		//	float			pitchStiffness;
		//	float			pitchDamping;	
		//	float			alongStiffness;
		//	float			alongDamping;	

		//	// angle constraint
		//	float			angleLimit;				// maximum deflection of tip in radians

		//	// yaw constraint
		//	float			minYaw;					// in radians
		//	float			maxYaw;					// in radians
		//	float			yawFriction;
		//	float			yawBounce;

		//	// pitch constraint
		//	float			minPitch;				// in radians
		//	float			maxPitch;				// in radians
		//	float			pitchFriction;
		//	float			pitchBounce;

		//	// base spring
		//	float			baseMass;
		//	float			baseStiffness;
		//	float			baseDamping;	
		//	float			baseMinLeft;
		//	float			baseMaxLeft;
		//	float			baseLeftFriction;
		//	float			baseMinUp;
		//	float			baseMaxUp;
		//	float			baseUpFriction;
		//	float			baseMinForward;
		//	float			baseMaxForward;
		//	float			baseForwardFriction;

		//private:
		//	// No copy constructors allowed
		//	//mstudiojigglebone_t(const mstudiojigglebone_t& vOther);
		//};



		public int flags;
		public double length;
		public double tipMass;

		public double yawStiffness;
		public double yawDamping;
		public double pitchStiffness;
		public double pitchDamping;
		public double alongStiffness;
		public double alongDamping;

		public double angleLimit;

		public double minYaw;
		public double maxYaw;
		public double yawFriction;
		public double yawBounce;

		public double minPitch;
		public double maxPitch;
		public double pitchFriction;
		public double pitchBounce;

		public double baseMass;
		public double baseStiffness;
		public double baseDamping;
		public double baseMinLeft;
		public double baseMaxLeft;
		public double baseLeftFriction;
		public double baseMinUp;
		public double baseMaxUp;
		public double baseUpFriction;
		public double baseMinForward;
		public double baseMaxForward;
		public double baseForwardFriction;



		// flags values:
		//#define JIGGLE_IS_FLEXIBLE				0x01
		//#define JIGGLE_IS_RIGID					0x02
		//#define JIGGLE_HAS_YAW_CONSTRAINT		0x04
		//#define JIGGLE_HAS_PITCH_CONSTRAINT		0x08
		//#define JIGGLE_HAS_ANGLE_CONSTRAINT		0x10
		//#define JIGGLE_HAS_LENGTH_CONSTRAINT	0x20
		//#define JIGGLE_HAS_BASE_SPRING			0x40
		public const int JIGGLE_IS_FLEXIBLE = 0x1;
		public const int JIGGLE_IS_RIGID = 0x2;
		public const int JIGGLE_HAS_YAW_CONSTRAINT = 0x4;
		public const int JIGGLE_HAS_PITCH_CONSTRAINT = 0x8;
		public const int JIGGLE_HAS_ANGLE_CONSTRAINT = 0x10;
		public const int JIGGLE_HAS_LENGTH_CONSTRAINT = 0x20;
		public const int JIGGLE_HAS_BASE_SPRING = 0x40;

	}

}