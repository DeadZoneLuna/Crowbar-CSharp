//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Crowbar
{
	public class SourceQuaternion48bitsViaBytes
	{

		//FROM: Kerry at Valve via Splinks on 24-Apr-2017
		////=========================================================
		//// 48 bit sorted Quaternion
		////=========================================================
		// 
		//class Quaternion48S
		//{
		//public:
		//       // Construction/destruction:
		//       Quaternion48S(void);
		//       Quaternion48S(vec_t X, vec_t Y, vec_t Z);
		// 
		//       // assignment
		//       // Quaternion& operator=(const Quaternion48 &vOther);
		//       Quaternion48S& operator=(const Quaternion &vOther);
		//       operator Quaternion () const;
		////private:
		//       // shift the quaternion so that the largest value is recreated by the sqrt()
		//       // abcd maps modulo into quaternion xyzw starting at "offset"
		//       // "offset" is split into two 1 bit fields so that the data packs into 6 bytes (3 shorts)
		//       unsigned short a:15;       // first of the 3 consecutive smallest quaternion elements
		//       unsigned short offsetH:1;  // high bit of "offset"
		//       unsigned short b:15;
		//       unsigned short offsetL:1;  // low bit of "offset"
		//       unsigned short c:15;
		//       unsigned short dneg:1;            // sign of the largest quaternion element
		//};
		// 
		//#define SCALE48S 23168.0f         // needs to fit 2*sqrt(0.5) into 15 bits.
		//#define SHIFT48S 16384                   // half of 2^15 bits.
		// 
		//inline Quaternion48S::operator Quaternion ()    const
		//{
		//       Quaternion tmp;
		// 
		//       COMPILE_TIME_ASSERT( sizeof( Quaternion48S ) == 6 );
		// 
		//       float *ptmp = &tmp.x;
		//       int ia = offsetL + offsetH * 2;
		//       int ib = ( ia + 1 ) % 4;
		//       int ic = ( ia + 2 ) % 4;
		//       int id = ( ia + 3 ) % 4;
		//       ptmp[ia] = ( (int)a - SHIFT48S ) * ( 1.0f / SCALE48S );
		//       ptmp[ib] = ( (int)b - SHIFT48S ) * ( 1.0f / SCALE48S );
		//       ptmp[ic] = ( (int)c - SHIFT48S ) * ( 1.0f / SCALE48S );
		//       ptmp[id] = sqrt( 1.0f - ptmp[ia] * ptmp[ia] - ptmp[ib] * ptmp[ib] - ptmp[ic] * ptmp[ic] );
		//       if (dneg)
		//              ptmp[id] = -ptmp[id];
		// 
		//       return tmp;
		//}
		// 
		//inline Quaternion48S& Quaternion48S::operator=(const Quaternion &vOther)  
		//{
		//       CHECK_VALID(vOther);
		// 
		//       const float *ptmp = &vOther.x;
		// 
		//       // find largest field, make sure that one is recreated by the sqrt to minimize error
		//       int i = 0;
		//       if ( fabs( ptmp[i] ) < fabs( ptmp[1] ) )
		//       {
		//              i = 1;
		//       }
		//       if ( fabs( ptmp[i] ) < fabs( ptmp[2] ) )
		//       {
		//              i = 2;
		//       }
		//       if ( fabs( ptmp[i] ) < fabs( ptmp[3] ) )
		//       {
		//              i = 3;
		//       }
		// 
		//       int offset = ( i + 1 ) % 4; // make "a" so that "d" is the largest element
		//       offsetL = offset & 1;
		//       offsetH = offset > 1;
		//       a = clamp( (int)(ptmp[ offset ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
		//       b = clamp( (int)(ptmp[ ( offset + 1 ) % 4 ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
		//       c = clamp( (int)(ptmp[ ( offset + 2 ) % 4 ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
		//       dneg = ( ptmp[ ( offset + 3 ) % 4 ] < 0.0f );
		// 
		//       return *this;
		//}

		public SourceQuaternion48bitsViaBytes()
		{
			theQuaternion = new SourceQuaternion();
			theQuaternionIsComputed = false;
		}

		public byte[] theBytes = new byte[6];

		public double x
		{
			get
			{
				ComputeQuaternion();
				return theQuaternion.x;
			}
		}

		public double y
		{
			get
			{
				ComputeQuaternion();
				return theQuaternion.y;
			}
		}

		public double z
		{
			get
			{
				ComputeQuaternion();
				return theQuaternion.z;
			}
		}

		public double w
		{
			get
			{
				ComputeQuaternion();
				return theQuaternion.w;
			}
		}

		public SourceQuaternion quaternion
		{
			get
			{
				ComputeQuaternion();
				return theQuaternion;
			}
		}

		private void ComputeQuaternion()
		{
			if (!theQuaternionIsComputed)
			{
				//1a-15-1b-15-1c-15 where 1a << 1 + 1b is index of missing component and 1c is sign of missing component 

				UInt32 tempInteger = 0;
				UInt32 tempInteger2 = 0;
				UInt32 uIntegerA = 0;
				UInt32 uIntegerB = 0;
				UInt32 uIntegerC = 0;
				double missingComponentSign = 0;
				UInt32 missingComponentIndex = 0;
				tempInteger = (uint)(theBytes[1] & 0x7F);
				tempInteger2 = (uint)theBytes[0];
				uIntegerA = (tempInteger << 8) | (tempInteger2);

				tempInteger = (uint)(theBytes[3] & 0x7F);
				tempInteger2 = (uint)theBytes[2];
				uIntegerB = (tempInteger << 8) | (tempInteger2);

				tempInteger = (uint)(theBytes[5] & 0x7F);
				tempInteger2 = (uint)theBytes[4];
				uIntegerC = (tempInteger << 8) | (tempInteger2);

				tempInteger = (uint)(theBytes[1] & 0x80);
				tempInteger2 = (uint)(theBytes[3] & 0x80);
				missingComponentIndex = (tempInteger >> 6) | (tempInteger2 >> 7);
				if ((theBytes[5] & 0x80) > 0)
				{
					missingComponentSign = -1;
				}
				else
				{
					missingComponentSign = 1;
				}

				double a = 0;
				double b = 0;
				double c = 0;

				a = (uIntegerA - 16384) / 23168.0;
				b = (uIntegerB - 16384) / 23168.0;
				c = (uIntegerC - 16384) / 23168.0;

				if (missingComponentIndex == SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_X)
				{
					theQuaternion.x = GetMissingComponent(a, b, c, missingComponentSign);
					theQuaternion.y = a;
					theQuaternion.z = b;
					theQuaternion.w = c;
				}
				else if (missingComponentIndex == SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Y)
				{
					theQuaternion.x = c;
					theQuaternion.y = GetMissingComponent(a, b, c, missingComponentSign);
					theQuaternion.z = a;
					theQuaternion.w = b;
				}
				else if (missingComponentIndex == SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Z)
				{
					theQuaternion.x = b;
					theQuaternion.y = c;
					theQuaternion.z = GetMissingComponent(a, b, c, missingComponentSign);
					theQuaternion.w = a;
				}
				else if (missingComponentIndex == SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_W)
				{
					theQuaternion.x = a;
					theQuaternion.y = b;
					theQuaternion.z = c;
					theQuaternion.w = GetMissingComponent(a, b, c, missingComponentSign);
				}
			}
		}

		private double GetMissingComponent(double a, double b, double c, double missingComponentSign)
		{
			return Math.Sqrt(1 - a * a - b * b - c * c) * missingComponentSign;
		}

		public const int MISSING_COMPONENT_W = 0;
		public const int MISSING_COMPONENT_X = 1;
		public const int MISSING_COMPONENT_Y = 2;
		public const int MISSING_COMPONENT_Z = 3;

		private SourceQuaternion theQuaternion;

		private bool theQuaternionIsComputed;

	}

	public enum EndianType
	{
		[Description("LittleEndian")]
		Little,
		[Description("BigEndian")]
		Big
	}

}