//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Crowbar
{
	public class SourceQuaternion64bits
	{

		//FROM: https://bitbucket.org/VoiDeD/steamre/src/9214bf3b662b/Resources/NetHook/mathlib/compressed_vector.h
		////=========================================================
		//// 64 bit Quaternion
		////=========================================================

		//	Class Quaternion64
		//{
		//public:
		//        // Construction/destruction:
		//        Quaternion64(void); 
		//        Quaternion64(vec_t X, vec_t Y, vec_t Z);

		//        // assignment
		//        // Quaternion& operator=(const Quaternion64 &vOther);
		//        Quaternion64& operator=(const Quaternion &vOther);
		//        operator Quaternion ();
		//private:
		//        uint64 x:21;
		//        uint64 y:21;
		//        uint64 z:21;
		//        uint64 wneg:1;
		//};

		//inline Quaternion64::operator Quaternion ()     
		//{
		//        Quaternion tmp;
		//
		//        // shift to -1048576, + 1048575, then round down slightly to -1.0 < x < 1.0
		//        tmp.x = ((int)x - 1048576) * (1 / 1048576.5f);
		//        tmp.y = ((int)y - 1048576) * (1 / 1048576.5f);
		//        tmp.z = ((int)z - 1048576) * (1 / 1048576.5f);
		//        tmp.w = sqrt( 1 - tmp.x * tmp.x - tmp.y * tmp.y - tmp.z * tmp.z );
		//		If (wneg) Then
		//                tmp.w = -tmp.w;
		//        return tmp; 
		//}

		//inline Quaternion64& Quaternion64::operator=(const Quaternion &vOther)  
		//{
		//        CHECK_VALID(vOther);
		//
		//        x = clamp( (int)(vOther.x * 1048576) + 1048576, 0, 2097151 );
		//        y = clamp( (int)(vOther.y * 1048576) + 1048576, 0, 2097151 );
		//        z = clamp( (int)(vOther.z * 1048576) + 1048576, 0, 2097151 );
		//        wneg = (vOther.w < 0);
		//        return *this; 
		//}



		//FROM: www.j3d.org/matrix_faq/matrfaq_latest.html
		//Q57. How do I convert a quaternion to a rotation axis and angle?
		//----------------------------------------------------------------
		//  A quaternion can be converted back to a rotation axis and angle
		//  using the following algorithm:

		//    quaternion_normalise( |X,Y,Z,W| );
		//    cos_a = W;
		//    angle = acos( cos_a ) * 2;
		//    sin_a = sqrt( 1.0 - cos_a * cos_a );
		//    if ( fabs( sin_a ) < 0.0005 ) sin_a = 1;
		//    axis -> x = X / sin_a;
		//    axis -> y = Y / sin_a;
		//    axis -> z = Z / sin_a;



		public byte[] theBytes = new byte[8];

		public double x
		{
			get
			{
				int byte0 = 0;
				int byte1 = 0;
				int byte2 = 0;
				IntegerAndSingleUnion bitsResult = new IntegerAndSingleUnion();
				double result = 0;

				byte0 = ((int)theBytes[0] & 0xFF);
				byte1 = ((int)theBytes[1] & 0xFF) << 8;
				byte2 = ((int)theBytes[2] & 0x1F) << 16;
				//------
				//byte0 = (CInt(Me.theBytes(7)) And &HFF)
				//byte1 = (CInt(Me.theBytes(6)) And &HFF) << 8
				//byte2 = (CInt(Me.theBytes(5)) And &H1F) << 16

				bitsResult.i = byte2 | byte1 | byte0;
				//Return bitsResult.s
				//Return CUInt(((Me.theBytes(2) And &H1F) << 16) And ((Me.theBytes(1) And &HFF) << 8) And (Me.theBytes(0) And &HFF))
				result = (bitsResult.i - 1048576) * (1 / 1048576.5);
				return result;
			}
		}

		public double y
		{
			get
			{
				int byte2 = 0;
				int byte3 = 0;
				int byte4 = 0;
				int byte5 = 0;
				IntegerAndSingleUnion bitsResult = new IntegerAndSingleUnion();
				double result = 0;

				byte2 = ((int)theBytes[2] & 0xE0) >> 5;
				byte3 = ((int)theBytes[3] & 0xFF) << 3;
				byte4 = ((int)theBytes[4] & 0xFF) << 11;
				byte5 = ((int)theBytes[5] & 0x3) << 19;
				//------
				//byte2 = (CInt(Me.theBytes(5)) And &HE0) >> 5
				//byte3 = (CInt(Me.theBytes(4)) And &HFF) << 3
				//byte4 = (CInt(Me.theBytes(3)) And &HFF) << 11
				//byte5 = (CInt(Me.theBytes(2)) And &H3) << 19

				bitsResult.i = byte5 | byte4 | byte3 | byte2;
				//Return bitsResult.s
				//Return CUInt(((Me.theBytes(5) And &H3) << 19) And ((Me.theBytes(4) And &HFF) << 11) And ((Me.theBytes(3) And &HFF) << 3) And (Me.theBytes(2) And &HE0) >> 5)
				result = (bitsResult.i - 1048576) * (1 / 1048576.5);
				return result;
			}
		}

		public double z
		{
			get
			{
				int byte5 = 0;
				int byte6 = 0;
				int byte7 = 0;
				IntegerAndSingleUnion bitsResult = new IntegerAndSingleUnion();
				double result = 0;

				byte5 = ((int)theBytes[5] & 0xFC) >> 2;
				byte6 = ((int)theBytes[6] & 0xFF) << 6;
				byte7 = ((int)theBytes[7] & 0x7F) << 14;
				//------
				//byte5 = (CInt(Me.theBytes(2)) And &HFC) >> 2
				//byte6 = (CInt(Me.theBytes(1)) And &HFF) << 6
				//byte7 = (CInt(Me.theBytes(0)) And &H7F) << 14

				bitsResult.i = byte7 | byte6 | byte5;
				//Return bitsResult.s
				//Return CUInt(((Me.theBytes(7) And &H7F) << 14) And ((Me.theBytes(6) And &HFF) << 6) And ((Me.theBytes(5) And &HFC) >> 2))
				result = (bitsResult.i - 1048576) * (1 / 1048576.5);
				return result;
			}
		}

		public double w
		{
			get
			{
				double result;

				//result = Me.wneg
				result = Math.Sqrt(1 - x * x - y * y - z * z) * wneg;
				return result;
			}
		}

		public double wneg
		{
			get
			{
				if ((theBytes[7] & 0x80) > 0)
				{
					return -1;
				}
				else
				{
					return 1;
				}
			}
		}

		public double xRadians
		{
			get
			{
				//    cos_a = W;
				//    angle = acos( cos_a ) * 2;
				//    sin_a = sqrt( 1.0 - cos_a * cos_a );
				//    if ( fabs( sin_a ) < 0.0005 ) sin_a = 1;
				//    axis -> x = X / sin_a;
				//    axis -> y = Y / sin_a;
				//    axis -> z = Z / sin_a;
				double cos_a = 0;
				double angle = 0;
				double sin_a = 0;

				cos_a = w;
				angle = Math.Acos(cos_a) * 2;
				sin_a = Math.Sqrt(1.0 - cos_a * cos_a);
				if (Math.Abs(sin_a) < 0.000005)
				{
					sin_a = 1;
				}

				return x / sin_a;
			}
		}

		public double yRadians
		{
			get
			{
				double cos_a = 0;
				double angle = 0;
				double sin_a = 0;

				cos_a = w;
				angle = Math.Acos(cos_a) * 2;
				sin_a = Math.Sqrt(1.0 - cos_a * cos_a);
				if (Math.Abs(sin_a) < 0.000005)
				{
					sin_a = 1;
				}

				return y / sin_a;
			}
		}

		public double zRadians
		{
			get
			{
				double cos_a = 0;
				double angle = 0;
				double sin_a = 0;

				cos_a = w;
				angle = Math.Acos(cos_a) * 2;
				sin_a = Math.Sqrt(1.0 - cos_a * cos_a);
				if (Math.Abs(sin_a) < 0.000005)
				{
					sin_a = 1;
				}

				return z / sin_a;
			}
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct IntegerAndSingleUnion
		{
			[FieldOffset(0)]
			public int i;
			[FieldOffset(0)]
			public float s;
		}

	}

}