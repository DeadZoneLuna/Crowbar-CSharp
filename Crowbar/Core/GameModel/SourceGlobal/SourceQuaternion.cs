using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceQuaternion
	{

		//FROM: SourceEngine2006_source\public\vector.h
		//class Quaternion				// same data-layout as engine's vec4_t,
		//{								//		which is a vec_t[4]
		//public:
		//	inline Quaternion(void)	{ 
		//
		//	// Initialize to NAN to catch errors
		//#ifdef _DEBUG
		//#ifdef VECTOR_PARANOIA
		//		x = y = z = w = VEC_T_NAN;
		//#endif
		//#endif
		//	}
		//	inline Quaternion(vec_t ix, vec_t iy, vec_t iz, vec_t iw) : x(ix), y(iy), z(iz), w(iw) { }
		//	inline Quaternion(RadianEuler const &angle);	// evil auto type promotion!!!
		//
		//	inline void Init(vec_t ix=0.0f, vec_t iy=0.0f, vec_t iz=0.0f, vec_t iw=0.0f)	{ x = ix; y = iy; z = iz; w = iw; }
		//
		//	bool IsValid() const;
		//
		//	bool operator==( const Quaternion &src ) const;
		//	bool operator!=( const Quaternion &src ) const;
		//
		//	// array access...
		//	vec_t operator[](int i) const;
		//	vec_t& operator[](int i);
		//
		//	vec_t x, y, z, w;
		//};

		//FROM: SourceEngine2006_source\public\tier0\basetypes.h
		//typedef float vec_t;

		public double x;
		public double y;
		public double z;
		public double w;

		public SourceQuaternion(double x, double y, double z, double w) { this.x = x; this.y = y; this.z = z; this.w = w; }

		public SourceQuaternion() { x = 0; y = 0; z = 0; w = 0; }

		public static SourceQuaternion Euler(SourceVector angles, bool deg2rad = false)
		{
			return Euler(angles.x, angles.y, angles.z, deg2rad);
		}

		public static SourceQuaternion Euler(double pitch, double yaw, double roll, bool DegToRad = false)
		{
			if(DegToRad)
			{
				pitch = MathModule.DegreesToRadians(pitch);
				yaw = MathModule.DegreesToRadians(yaw);
				roll = MathModule.DegreesToRadians(roll);
			}

			pitch *= 0.5;
			yaw *= 0.5;
			roll *= 0.5;

			var c1 = Math.Cos(pitch);
			var c2 = Math.Cos(yaw);
			var c3 = Math.Cos(roll);
			var s1 = Math.Sin(pitch);
			var s2 = Math.Sin(yaw);
			var s3 = Math.Sin(roll);

			return new SourceQuaternion
			{
				w = (c1 * c2 * c3) - (s1 * s2 * s3),
				x = (s1 * c2 * c3) + (c1 * s2 * s3),
				y = (c1 * s2 * c3) - (s1 * c2 * s3),
				z = (c1 * c2 * s3) + (s1 * s2 * c3),
			};
		}

		public static SourceQuaternion AngleAxis(double angle, SourceVector axis, bool DegToRad = false)
		{
			SourceQuaternion q = new SourceQuaternion();
			double mag = Math.Sqrt(axis.x * axis.x + axis.y * axis.y + axis.z * axis.z);
			if (mag > 0.000001)
			{
				double halfAngle = (DegToRad ? MathModule.DegreesToRadians(angle) : angle) * 0.5;

				q.w = Math.Cos(halfAngle);

				double s = Math.Sin(halfAngle) / mag;
				q.x = s * axis.x;
				q.y = s * axis.y;
				q.z = s * axis.z;
				return q;
			}
			else
			{
				return new SourceQuaternion(0, 0, 0, 1);
			}
		}

		public static SourceQuaternion ConvertToRightHand(SourceVector Euler, bool DegToRad = false)
		{
			SourceQuaternion x = AngleAxis(Euler.x, SourceVector.down.Swap(Common.VecOrder.XZY).InvertY(), DegToRad);
			SourceQuaternion y = AngleAxis(Euler.y, SourceVector.left.Swap(Common.VecOrder.XZY).InvertY(), DegToRad);
			SourceQuaternion z = AngleAxis(Euler.z, SourceVector.right.Swap(Common.VecOrder.XZY).InvertY(), DegToRad);

			Common.VecOrder order = Common.VecOrder.XYZ;
			switch (order)
			{
				default: return x * y * z;
				case Common.VecOrder.YXZ: return y * x * z;
				case Common.VecOrder.YZX: return y * z * x;
				case Common.VecOrder.XZY: return x * z * y;
				case Common.VecOrder.ZXY: return z * x * y;
				case Common.VecOrder.ZYX: return z * y * x;
			}
		}

		// Combines rotations /lhs/ and /rhs/.
		public static SourceQuaternion operator *(SourceQuaternion lhs, SourceQuaternion rhs)
		{
			return new SourceQuaternion(
				lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
				lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
				lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
				lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Rotates the point /point/ with /rotation/.
		public static SourceVector operator *(SourceQuaternion rotation, SourceVector point)
		{
			double x = rotation.x * 2d;
			double y = rotation.y * 2d;
			double z = rotation.z * 2d;
			double xx = rotation.x * x;
			double yy = rotation.y * y;
			double zz = rotation.z * z;
			double xy = rotation.x * y;
			double xz = rotation.x * z;
			double yz = rotation.y * z;
			double wx = rotation.w * x;
			double wy = rotation.w * y;
			double wz = rotation.w * z;

			SourceVector res = new SourceVector();
			res.x = (1.0 - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
			res.y = (xy + wz) * point.x + (1.0 - (xx + zz)) * point.y + (yz - wx) * point.z;
			res.z = (xz - wy) * point.x + (yz + wx) * point.y + (1.0 - (xx + yy)) * point.z;
			return res;
		}
	}
}