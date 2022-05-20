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
	internal static class MathModule
	{

		public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
		{
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}

		public static long DateTimeToUnixTimeStamp(DateTime iDateTime)
		{
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			TimeSpan span = (iDateTime.ToUniversalTime() - dtDateTime);
			return Convert.ToInt64(span.TotalSeconds);
		}

		//Public Function ByteUnitsConversion(ByVal iBytes As ULong) As String
		//	Dim value As Double = iBytes
		//	Dim suffix() As String = {"Bytes", "KB", "MB", "TB", "PB", "EB"}
		//	Dim factor As ULong = 1024
		//	Dim index As Integer = 0
		//	Dim suffixIndex As Integer = 0

		//	While iBytes > factor And index < suffix.Length
		//		value = iBytes / factor
		//		suffixIndex = index

		//		factor *= 1024UL
		//		index = index + 1
		//	End While
		//	'If index > 0 Then
		//	'	value = iBytes / factor
		//	'End If

		//	Return value.ToString("####.##") + " " + suffix(suffixIndex)
		//End Function

		public static string ByteUnitsConversion(ulong iBytes)
		{
			double value = iBytes;
			string suffix = "Bytes";

			if (iBytes >= 1073741824)
			{
				value = Convert.ToDouble(iBytes / (decimal)(1024 * 1024 * 1024));
				suffix = "GB";
			}
			else if (iBytes >= 1048576)
			{
				value = Convert.ToDouble(iBytes / (decimal)(1024 * 1024));
				suffix = "MB";
			}
			else if (iBytes >= 1024)
			{
				value = iBytes / 1024.0;
				suffix = "KB";
			}

			//Return value.ToString("####.##") + " " + suffix
			return value.ToString("N2") + " " + suffix;
		}

		public static long AlignLong(long currentValue, long alignmentValue)
		{
			// File seek to next nearest start of 4-byte block. 
			//      In C++: #define ALIGN4( a ) a = (byte *)((int)((byte *)a + 3) & ~ 3)
			long result = (currentValue + alignmentValue - 1) & ~(alignmentValue - 1);
			return result;
		}

		public static double DegreesToRadians(double degrees)
		{
			// 57.29578 = 180 / pi
			//Return degrees / 57.29578
			return degrees * 3.1415926535897931 / 180;
		}

		public static double RadiansToDegrees(double radians)
		{
			// 57.29578 = 180 / pi
			//Return radians * 57.29578
			return radians * 180 / 3.1415926535897931;
		}

		////Purpose: Converts 3x3 rotation matrix to degrees (XYZ).
		////Requires: 0, 1, 2, 3, 4, 5, 8 (floats) from matrix.
		//function vmd_rotmatrix2deg($m0, $m1, $m2, $m3, $m4, $m5, $m8)
		//{
		//	$angle_y = -asin(round($m2, 6));
		//	$c = cos($angle_y);
		//	$angle_y = rad2deg($angle_y);
		//	if(abs($c) > 0.005)
		//	{
		//		$trx = round($m8, 6) / $c;
		//		$try = round(-$m5, 6) / $c;
		//		$angle_x = rad2deg(atan2($try, $trx));
		//		$trx = round($m0, 6) / $c;
		//		$try = round(-$m1, 6) / $c;
		//		$angle_z = rad2deg(atan2($try, $trx));
		//	}
		//		Else
		//	{
		//		$angle_x = 0;
		//		$trx = round($m4, 6);
		//		$try = round($m3, 6);
		//		$angle_z = rad2deg(atan2($try, $trx));
		//	}
		//	return array(vmd_rf2p($angle_x), vmd_rf2p($angle_y), vmd_rf2p($angle_z));
		//}
		public static void ConvertRotationMatrixToDegrees(float m0, float m1, float m2, float m3, float m4, float m5, float m8, ref double angleX, ref double angleY, ref double angleZ)
		{
			double c = 0;
			double translateX = 0;
			double translateY = 0;

			//NOTE: For Math.Asin, return value is NaN if d < -1 or d > 1 or d equals NaN.
			//      Therefore, change value outside of domain to edge of domain.
			if (m2 < -1)
			{
				m2 = -1;
			}
			else if (m2 > 1)
			{
				m2 = 1;
			}
			angleY = -Math.Asin(Math.Round(m2, 6));

			c = Math.Cos(angleY);
			angleY = RadiansToDegrees(angleY);
			if (Math.Abs(c) > 0.005)
			{
				translateX = Math.Round(m8, 6) / c;
				translateY = Math.Round(-m5, 6) / c;
				angleX = RadiansToDegrees(Math.Atan2(translateY, translateX));
				translateX = Math.Round(m0, 6) / c;
				translateY = Math.Round(-m1, 6) / c;
				angleZ = RadiansToDegrees(Math.Atan2(translateY, translateX));
			}
			else
			{
				angleX = 0;
				translateX = Math.Round(m4, 6);
				translateY = Math.Round(m3, 6);
				angleZ = RadiansToDegrees(Math.Atan2(translateY, translateX));
			}
		}

		//FROM: http://www.j3d.org/matrix_faq/matrfaq_latest.html#Q60
		//The following more or less comes from:
		// http://vered.rose.utoronto.ca/people/david_dir/GEMS/GEMS.html

		//  //Pitch->X axis, Yaw->Y axis, Roll->Z axis
		//   Quaternion::Quaternion(float fPitch, float fYaw, float fRoll)
		//   {
		//      const float fSinPitch(sin(fPitch*0.5F));
		//      const float fCosPitch(cos(fPitch*0.5F));
		//      const float fSinYaw(sin(fYaw*0.5F));
		//      const float fCosYaw(cos(fYaw*0.5F));
		//      const float fSinRoll(sin(fRoll*0.5F));
		//      const float fCosRoll(cos(fRoll*0.5F));
		//      const float fCosPitchCosYaw(fCosPitch*fCosYaw);
		//      const float fSinPitchSinYaw(fSinPitch*fSinYaw);
		//      X = fSinRoll * fCosPitchCosYaw     - fCosRoll * fSinPitchSinYaw;
		//      Y = fCosRoll * fSinPitch * fCosYaw + fSinRoll * fCosPitch * fSinYaw;
		//      Z = fCosRoll * fCosPitch * fSinYaw - fSinRoll * fSinPitch * fCosYaw;
		//      W = fCosRoll * fCosPitchCosYaw     + fSinRoll * fSinPitchSinYaw;
		//   }
		public static SourceQuaternion EulerAnglesToQuaternion(SourceVector angleVector)
		{
			double fPitch = 0;
			double fYaw = 0;
			double fRoll = 0;
			SourceQuaternion rot = new SourceQuaternion();

			fPitch = angleVector.x;
			fYaw = angleVector.y;
			fRoll = angleVector.z;

			double fSinPitch = Math.Sin(fPitch * 0.5F);
			double fCosPitch = Math.Cos(fPitch * 0.5F);
			double fSinYaw = Math.Sin(fYaw * 0.5F);
			double fCosYaw = Math.Cos(fYaw * 0.5F);
			double fSinRoll = Math.Sin(fRoll * 0.5F);
			double fCosRoll = Math.Cos(fRoll * 0.5F);
			double fCosPitchCosYaw = fCosPitch * fCosYaw;
			double fSinPitchSinYaw = fSinPitch * fSinYaw;

			rot.x = fSinRoll * fCosPitchCosYaw - fCosRoll * fSinPitchSinYaw;
			rot.y = fCosRoll * fSinPitch * fCosYaw + fSinRoll * fCosPitch * fSinYaw;
			rot.z = fCosRoll * fCosPitch * fSinYaw - fSinRoll * fSinPitch * fCosYaw;
			rot.w = fCosRoll * fCosPitchCosYaw + fSinRoll * fSinPitchSinYaw;

			return rot;
		}

		//FROM: http://code.google.com/p/hl2sources/source/browse/trunk/public/mathlib.cpp
		//NOTE: This seems to confirm that matrix arrays are [row][column].
		//void MatrixGetColumn( const matrix3x4_t& in, int column, Vector &out )
		//{
		//	out.x = in[0][column];
		//	out.y = in[1][column];
		//	out.z = in[2][column];
		//}

		//FROM: math_base.h
		// SinCos computes the sine and the cosine of the source angle value in ST(0).
		//// Math routines done in optimized assembly math package routines
		//void inline SinCos( float radians, float *sine, float *cosine )
		//{
		//#ifdef _WIN32
		//	_asm
		//	{
		//		fld		DWORD PTR [radians]
		//		fsincos

		//		mov edx, DWORD PTR [cosine]
		//		mov eax, DWORD PTR [sine]

		//		fstp DWORD PTR [edx]
		//		fstp DWORD PTR [eax]
		//	}
		//#elif _LINUX
		//	register double __cosr, __sinr;
		// 	__asm __volatile__
		//    		("fsincos"
		//     	: "=t" (__cosr), "=u" (__sinr) : "0" (radians));

		//  	*sine = __sinr;
		//  	*cosine = __cosr;
		//#endif
		//}
		//FROM: http://code.google.com/p/hl2sources/source/browse/trunk/public/mathlib.cpp
		//void AngleMatrix( const QAngle &angles, matrix3x4_t& matrix )
		//{
		//	Assert( s_bMathlibInitialized );
		//	float sr, sp, sy, cr, cp, cy;

		//	SinCos( DEG2RAD( angles[YAW] ), &sy, &cy );
		//	SinCos( DEG2RAD( angles[PITCH] ), &sp, &cp );
		//	SinCos( DEG2RAD( angles[ROLL] ), &sr, &cr );

		//	// matrix = (YAW * PITCH) * ROLL
		//	matrix[0][0] = cp*cy;
		//	matrix[1][0] = cp*sy;
		//	matrix[2][0] = -sp;
		//	matrix[0][1] = sr*sp*cy+cr*-sy;
		//	matrix[1][1] = sr*sp*sy+cr*cy;
		//	matrix[2][1] = sr*cp;
		//	matrix[0][2] = (cr*sp*cy+-sr*-sy);
		//	matrix[1][2] = (cr*sp*sy+-sr*cy);
		//	matrix[2][2] = cr*cp;
		//	matrix[0][3] = 0.f;
		//	matrix[1][3] = 0.f;
		//	matrix[2][3] = 0.f;
		//}
		//'Public Sub AngleMatrix(ByVal pitchDegrees As Double, ByVal yawDegrees As Double, ByVal rollDegrees As Double, ByRef matrixColumn0 As SourceVector, ByRef matrixColumn1 As SourceVector, ByRef matrixColumn2 As SourceVector)
		public static void AngleMatrix(double pitchRadians, double yawRadians, double rollRadians, ref SourceVector matrixColumn0, ref SourceVector matrixColumn1, ref SourceVector matrixColumn2, ref SourceVector matrixColumn3)
		{
			//Dim pitchRadians As Double
			//Dim yawRadians As Double
			//Dim rollRadians As Double
			double sr = 0;
			double sp = 0;
			double sy = 0;
			double cr = 0;
			double cp = 0;
			double cy = 0;

			//pitchRadians = DegreesToRadians(pitchDegrees)
			//yawRadians = DegreesToRadians(yawDegrees)
			//rollRadians = DegreesToRadians(rollDegrees)

			sy = Math.Sin(yawRadians);
			cy = Math.Cos(yawRadians);
			sp = Math.Sin(pitchRadians);
			cp = Math.Cos(pitchRadians);
			sr = Math.Sin(rollRadians);
			cr = Math.Cos(rollRadians);

			matrixColumn0.x = cp * cy;
			matrixColumn0.y = cp * sy;
			matrixColumn0.z = -sp;
			matrixColumn1.x = sr * sp * cy + cr * -sy;
			matrixColumn1.y = sr * sp * sy + cr * cy;
			matrixColumn1.z = sr * cp;
			matrixColumn2.x = (cr * sp * cy + -sr * -sy);
			matrixColumn2.y = (cr * sp * sy + -sr * cy);
			matrixColumn2.z = cr * cp;
			matrixColumn3.x = 0;
			matrixColumn3.y = 0;
			matrixColumn3.z = 0;
		}

		//FROM: [36] SourceEngine2003_source HL2 Beta 2003\src_main\Public\Mathlib.cpp
		//void MatrixAngles( const matrix3x4_t& matrix, float *angles )
		//{ 
		//	Assert( s_bMathlibInitialized );
		//	float forward[3];
		//	float left[3];
		//	float up[3];
		//
		//	//
		//	// Extract the basis vectors from the matrix. Since we only need the Z
		//	// component of the up vector, we don't get X and Y.
		//	//
		//	forward[0] = matrix[0][0];
		//	forward[1] = matrix[1][0];
		//	forward[2] = matrix[2][0];
		//	left[0] = matrix[0][1];
		//	left[1] = matrix[1][1];
		//	left[2] = matrix[2][1];
		//	up[2] = matrix[2][2];
		//
		//	float xyDist = sqrtf( forward[0] * forward[0] + forward[1] * forward[1] );
		//	
		//	// enough here to get angles?
		//	if ( xyDist > 0.001f )
		//	{
		//		// (yaw)	y = ATAN( forward.y, forward.x );		-- in our space, forward is the X axis
		//		angles[1] = RAD2DEG( atan2f( forward[1], forward[0] ) );
		//
		//		// The engine does pitch inverted from this, but we always end up negating it in the DLL
		//		// UNDONE: Fix the engine to make it consistent
		//		// (pitch)	x = ATAN( -forward.z, sqrt(forward.x*forward.x+forward.y*forward.y) );
		//		angles[0] = RAD2DEG( atan2f( -forward[2], xyDist ) );
		//
		//		// (roll)	z = ATAN( left.z, up.z );
		//		angles[2] = RAD2DEG( atan2f( left[2], up[2] ) );
		//	}
		//	else	// forward is mostly Z, gimbal lock-
		//	{
		//		// (yaw)	y = ATAN( -left.x, left.y );			-- forward is mostly z, so use right for yaw
		//		angles[1] = RAD2DEG( atan2f( -left[0], left[1] ) );
		//
		//		// The engine does pitch inverted from this, but we always end up negating it in the DLL
		//		// UNDONE: Fix the engine to make it consistent
		//		// (pitch)	x = ATAN( -forward.z, sqrt(forward.x*forward.x+forward.y*forward.y) );
		//		angles[0] = RAD2DEG( atan2f( -forward[2], xyDist ) );
		//
		//		// Assume no roll in this case as one degree of freedom has been lost (i.e. yaw == roll)
		//		angles[2] = 0;
		//	}
		//}
		public static void MatrixAnglesInDegrees(SourceVector matrixColumn0, SourceVector matrixColumn1, SourceVector matrixColumn2, SourceVector matrixColumn3, ref double pitchDegrees, ref double yawDegrees, ref double rollDegrees)
		{
			SourceVector forward = new SourceVector();
			SourceVector left = new SourceVector();
			SourceVector up = new SourceVector();

			forward.x = matrixColumn0.x;
			forward.y = matrixColumn0.y;
			forward.z = matrixColumn0.z;
			left.x = matrixColumn1.x;
			left.y = matrixColumn1.y;
			left.z = matrixColumn1.z;
			up.z = matrixColumn2.z;

			double xyDist = Math.Sqrt(forward.x * forward.x + forward.y * forward.y);

			if (xyDist > 0.001)
			{
				yawDegrees = MathModule.RadiansToDegrees(Math.Atan2(forward.y, forward.x));
				pitchDegrees = MathModule.RadiansToDegrees(Math.Atan2(-forward.z, xyDist));
				rollDegrees = MathModule.RadiansToDegrees(Math.Atan2(left.z, up.z));
			}
			else
			{
				yawDegrees = MathModule.RadiansToDegrees(Math.Atan2(-left.x, left.y));
				pitchDegrees = MathModule.RadiansToDegrees(Math.Atan2(-forward.z, xyDist));
				rollDegrees = 0;
			}
		}

		public static void MatrixAnglesInRadians(SourceVector matrixColumn0, SourceVector matrixColumn1, SourceVector matrixColumn2, SourceVector matrixColumn3, ref double pitchRadians, ref double yawRadians, ref double rollRadians)
		{
			SourceVector forward = new SourceVector();
			SourceVector left = new SourceVector();
			SourceVector up = new SourceVector();

			forward.x = matrixColumn0.x;
			forward.y = matrixColumn0.y;
			forward.z = matrixColumn0.z;
			left.x = matrixColumn1.x;
			left.y = matrixColumn1.y;
			left.z = matrixColumn1.z;
			up.z = matrixColumn2.z;

			double xyDist = Math.Sqrt(forward.x * forward.x + forward.y * forward.y);

			if (xyDist > 0.001)
			{
				yawRadians = Math.Atan2(forward.y, forward.x);
				pitchRadians = Math.Atan2(-forward.z, xyDist);
				rollRadians = Math.Atan2(left.z, up.z);
			}
			else
			{
				yawRadians = Math.Atan2(-left.x, left.y);
				pitchRadians = Math.Atan2(-forward.z, xyDist);
				rollRadians = 0;
			}
		}

		//FROM: [1999] HLStandardSDK\SourceCode\utils\common\mathlib.c
		//void R_ConcatTransforms (const float in1[3][4], const float in2[3][4], float out[3][4])
		//{
		//	out[0][0] = in1[0][0] * in2[0][0] + in1[0][1] * in2[1][0] +
		//				in1[0][2] * in2[2][0];
		//	out[0][1] = in1[0][0] * in2[0][1] + in1[0][1] * in2[1][1] +
		//				in1[0][2] * in2[2][1];
		//	out[0][2] = in1[0][0] * in2[0][2] + in1[0][1] * in2[1][2] +
		//				in1[0][2] * in2[2][2];
		//	out[0][3] = in1[0][0] * in2[0][3] + in1[0][1] * in2[1][3] +
		//				in1[0][2] * in2[2][3] + in1[0][3];
		//	out[1][0] = in1[1][0] * in2[0][0] + in1[1][1] * in2[1][0] +
		//				in1[1][2] * in2[2][0];
		//	out[1][1] = in1[1][0] * in2[0][1] + in1[1][1] * in2[1][1] +
		//				in1[1][2] * in2[2][1];
		//	out[1][2] = in1[1][0] * in2[0][2] + in1[1][1] * in2[1][2] +
		//				in1[1][2] * in2[2][2];
		//	out[1][3] = in1[1][0] * in2[0][3] + in1[1][1] * in2[1][3] +
		//				in1[1][2] * in2[2][3] + in1[1][3];
		//	out[2][0] = in1[2][0] * in2[0][0] + in1[2][1] * in2[1][0] +
		//				in1[2][2] * in2[2][0];
		//	out[2][1] = in1[2][0] * in2[0][1] + in1[2][1] * in2[1][1] +
		//				in1[2][2] * in2[2][1];
		//	out[2][2] = in1[2][0] * in2[0][2] + in1[2][1] * in2[1][2] +
		//				in1[2][2] * in2[2][2];
		//	out[2][3] = in1[2][0] * in2[0][3] + in1[2][1] * in2[1][3] +
		//				in1[2][2] * in2[2][3] + in1[2][3];
		//}
		public static void R_ConcatTransforms(SourceVector in1_matrixColumn0, SourceVector in1_matrixColumn1, SourceVector in1_matrixColumn2, SourceVector in1_matrixColumn3, SourceVector in2_matrixColumn0, SourceVector in2_matrixColumn1, SourceVector in2_matrixColumn2, SourceVector in2_matrixColumn3, ref SourceVector out_matrixColumn0, ref SourceVector out_matrixColumn1, ref SourceVector out_matrixColumn2, ref SourceVector out_matrixColumn3)
		{
			out_matrixColumn0.x = in1_matrixColumn0.x * in2_matrixColumn0.x + in1_matrixColumn1.x * in2_matrixColumn0.y + in1_matrixColumn2.x * in2_matrixColumn0.z;
			out_matrixColumn1.x = in1_matrixColumn0.x * in2_matrixColumn1.x + in1_matrixColumn1.x * in2_matrixColumn1.y + in1_matrixColumn2.x * in2_matrixColumn1.z;
			out_matrixColumn2.x = in1_matrixColumn0.x * in2_matrixColumn2.x + in1_matrixColumn1.x * in2_matrixColumn2.y + in1_matrixColumn2.x * in2_matrixColumn2.z;
			out_matrixColumn3.x = in1_matrixColumn0.x * in2_matrixColumn3.x + in1_matrixColumn1.x * in2_matrixColumn3.y + in1_matrixColumn2.x * in2_matrixColumn3.z + in1_matrixColumn3.x;

			out_matrixColumn0.y = in1_matrixColumn0.y * in2_matrixColumn0.x + in1_matrixColumn1.y * in2_matrixColumn0.y + in1_matrixColumn2.y * in2_matrixColumn0.z;
			out_matrixColumn1.y = in1_matrixColumn0.y * in2_matrixColumn1.x + in1_matrixColumn1.y * in2_matrixColumn1.y + in1_matrixColumn2.y * in2_matrixColumn1.z;
			out_matrixColumn2.y = in1_matrixColumn0.y * in2_matrixColumn2.x + in1_matrixColumn1.y * in2_matrixColumn2.y + in1_matrixColumn2.y * in2_matrixColumn2.z;
			out_matrixColumn3.y = in1_matrixColumn0.y * in2_matrixColumn3.x + in1_matrixColumn1.y * in2_matrixColumn3.y + in1_matrixColumn2.y * in2_matrixColumn3.z + in1_matrixColumn3.y;

			out_matrixColumn0.z = in1_matrixColumn0.z * in2_matrixColumn0.x + in1_matrixColumn1.z * in2_matrixColumn0.y + in1_matrixColumn2.z * in2_matrixColumn0.z;
			out_matrixColumn1.z = in1_matrixColumn0.z * in2_matrixColumn1.x + in1_matrixColumn1.z * in2_matrixColumn1.y + in1_matrixColumn2.z * in2_matrixColumn1.z;
			out_matrixColumn2.z = in1_matrixColumn0.z * in2_matrixColumn2.x + in1_matrixColumn1.z * in2_matrixColumn2.y + in1_matrixColumn2.z * in2_matrixColumn2.z;
			out_matrixColumn3.z = in1_matrixColumn0.z * in2_matrixColumn3.x + in1_matrixColumn1.z * in2_matrixColumn3.y + in1_matrixColumn2.z * in2_matrixColumn3.z + in1_matrixColumn3.z;
		}

		//FROM: [36] SourceEngine2003_source HL2 Beta 2003\src_main\Public\Mathlib.cpp
		//void MatrixInvert( const matrix3x4_t& in, matrix3x4_t& out )
		//{
		//	Assert( s_bMathlibInitialized );
		//	if ( &in == &out )
		//	{
		//		matrix3x4_t in2;
		//		MatrixCopy( in, in2 );
		//		MatrixInvert( in2, out );
		//		return;
		//	}
		//	float tmp[3];
		//
		//	// I'm guessing this only works on a 3x4 orthonormal matrix
		//	out[0][0] = in[0][0];
		//	out[0][1] = in[1][0];
		//	out[0][2] = in[2][0];
		//
		//	out[1][0] = in[0][1];
		//	out[1][1] = in[1][1];
		//	out[1][2] = in[2][1];
		//
		//	out[2][0] = in[0][2];
		//	out[2][1] = in[1][2];
		//	out[2][2] = in[2][2];
		//
		//	tmp[0] = in[0][3];
		//	tmp[1] = in[1][3];
		//	tmp[2] = in[2][3];
		//
		//	out[0][3] = -DotProduct( tmp, out[0] );
		//	out[1][3] = -DotProduct( tmp, out[1] );
		//	out[2][3] = -DotProduct( tmp, out[2] );
		//}
		public static void MatrixInvert(SourceVector in_matrixColumn0, SourceVector in_matrixColumn1, SourceVector in_matrixColumn2, SourceVector in_matrixColumn3, ref SourceVector out_matrixColumn0, ref SourceVector out_matrixColumn1, ref SourceVector out_matrixColumn2, ref SourceVector out_matrixColumn3)
		{
			out_matrixColumn0.x = in_matrixColumn0.x;
			out_matrixColumn1.x = in_matrixColumn0.y;
			out_matrixColumn2.x = in_matrixColumn0.z;

			out_matrixColumn0.y = in_matrixColumn1.x;
			out_matrixColumn1.y = in_matrixColumn1.y;
			out_matrixColumn2.y = in_matrixColumn1.z;

			out_matrixColumn0.z = in_matrixColumn2.x;
			out_matrixColumn1.z = in_matrixColumn2.y;
			out_matrixColumn2.z = in_matrixColumn2.z;

			SourceVector temp = new SourceVector();

			temp.x = in_matrixColumn3.x;
			temp.y = in_matrixColumn3.y;
			temp.z = in_matrixColumn3.z;

			out_matrixColumn3.x = -DotProduct(temp, out_matrixColumn0);
			out_matrixColumn3.y = -DotProduct(temp, out_matrixColumn1);
			out_matrixColumn3.z = -DotProduct(temp, out_matrixColumn2);
		}



		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\public\mathlib.h
		//FORCEINLINE vec_t DotProduct(const vec_t *v1, const vec_t *v2)
		//{
		//	return v1[0]*v2[0] + v1[1]*v2[1] + v1[2]*v2[2];
		//}
		public static double DotProduct(SourceVector vector1, SourceVector vector2)
		{
			return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
		}


		public static void VectorCopy(SourceVector input, ref SourceVector output)
		{
			output.x = input.x;
			output.y = input.y;
			output.z = input.z;
		}

		//FROM: HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\sdk\mathlib.c
		//void VectorRotate (const vec3_t in1, const float in2[3][4], vec3_t out)
		//{
		//	out[0] = DotProduct(in1, in2[0]);
		//	out[1] = DotProduct(in1, in2[1]);
		//	out[2] = DotProduct(in1, in2[2]);
		//}
		public static SourceVector VectorRotate(SourceVector input, SourceVector matrixColumn0, SourceVector matrixColumn1, SourceVector matrixColumn2, SourceVector matrixColumn3)
		{
			SourceVector output = null;
			SourceVector matrixRow0 = null;
			SourceVector matrixRow1 = null;
			SourceVector matrixRow2 = null;

			output = new SourceVector();
			matrixRow0 = new SourceVector();
			matrixRow1 = new SourceVector();
			matrixRow2 = new SourceVector();

			matrixRow0.x = matrixColumn0.x;
			matrixRow0.y = matrixColumn1.x;
			matrixRow0.z = matrixColumn2.x;

			matrixRow1.x = matrixColumn0.y;
			matrixRow1.y = matrixColumn1.y;
			matrixRow1.z = matrixColumn2.y;

			matrixRow2.x = matrixColumn0.z;
			matrixRow2.y = matrixColumn1.z;
			matrixRow2.z = matrixColumn2.z;

			output.x = DotProduct(input, matrixRow0);
			output.y = DotProduct(input, matrixRow1);
			output.z = DotProduct(input, matrixRow2);

			return output;
		}

		//// rotate by the inverse of the matrix
		//void VectorIRotate (const vec3_t in1, const float in2[3][4], vec3_t out)
		//{
		//	out[0] = in1[0]*in2[0][0] + in1[1]*in2[1][0] + in1[2]*in2[2][0];
		//	out[1] = in1[0]*in2[0][1] + in1[1]*in2[1][1] + in1[2]*in2[2][1];
		//	out[2] = in1[0]*in2[0][2] + in1[1]*in2[1][2] + in1[2]*in2[2][2];
		//}
		public static SourceVector VectorIRotate(SourceVector input, SourceVector matrixColumn0, SourceVector matrixColumn1, SourceVector matrixColumn2, SourceVector matrixColumn3)
		{
			SourceVector output = new SourceVector();


			output.x = input.x * matrixColumn0.x + input.y * matrixColumn0.y + input.z * matrixColumn0.z;
			output.y = input.x * matrixColumn1.x + input.y * matrixColumn1.y + input.z * matrixColumn1.z;
			output.z = input.x * matrixColumn2.x + input.y * matrixColumn2.y + input.z * matrixColumn2.z;

			return output;
		}

		//FROM: HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\sdk\mathlib.c
		//vec_t VectorNormalize (vec3_t v)
		//{
		//	int		i;
		//	double	length;
		//
		//	if ( fabs(v[1] - 0.000215956) < 0.0001)
		//		i=1;
		//
		//	length = 0;
		//	for (i=0 ; i< 3 ; i++)
		//		length += v[i]*v[i];
		//	length = sqrt (length);
		//	if (length == 0)
		//		return 0;
		//		
		//	for (i=0 ; i< 3 ; i++)
		//		v[i] /= length;	
		//
		//	return length;
		//}
		public static double VectorNormalize(ref SourceVector ioVector)
		{
			double length = 0;

			length += ioVector.x * ioVector.x;
			length += ioVector.y * ioVector.y;
			length += ioVector.z * ioVector.z;
			length = Math.Sqrt(length);
			if (length == 0)
			{
				return 0;
			}

			ioVector.x /= length;
			ioVector.y /= length;
			ioVector.z /= length;

			return length;
		}



		//FROM: SourceEngine2003_source HL2 Beta 2003\src_main\public\mathlib.cpp
		//void VectorTransform (const float *in1, const matrix3x4_t& in2, float *out)
		//{
		//	Assert( s_bMathlibInitialized );
		//	Assert( in1 != out );
		//	out[0] = DotProduct(in1, in2[0]) + in2[0][3];
		//	out[1] = DotProduct(in1, in2[1]) + in2[1][3];
		//	out[2] = DotProduct(in1, in2[2]) + in2[2][3];
		//}
		public static SourceVector VectorTransform(SourceVector input, SourceVector matrixColumn0, SourceVector matrixColumn1, SourceVector matrixColumn2, SourceVector matrixColumn3)
		{
			SourceVector output = null;
			SourceVector matrixRow0 = null;
			SourceVector matrixRow1 = null;
			SourceVector matrixRow2 = null;

			output = new SourceVector();
			matrixRow0 = new SourceVector();
			matrixRow1 = new SourceVector();
			matrixRow2 = new SourceVector();

			matrixRow0.x = matrixColumn0.x;
			matrixRow0.y = matrixColumn1.x;
			matrixRow0.z = matrixColumn2.x;

			matrixRow1.x = matrixColumn0.y;
			matrixRow1.y = matrixColumn1.y;
			matrixRow1.z = matrixColumn2.y;

			matrixRow2.x = matrixColumn0.z;
			matrixRow2.y = matrixColumn1.z;
			matrixRow2.z = matrixColumn2.z;

			output.x = DotProduct(input, matrixRow0) + matrixColumn3.x;
			output.y = DotProduct(input, matrixRow1) + matrixColumn3.y;
			output.z = DotProduct(input, matrixRow2) + matrixColumn3.z;

			return output;
		}



		//FROM: http://code.google.com/p/hl2sources/source/browse/trunk/public/mathlib.cpp
		//void VectorITransform (const float *in1, const matrix3x4_t& in2, float *out)
		//{
		//	Assert( s_bMathlibInitialized );
		//	float in1t[3];

		//	in1t[0] = in1[0] - in2[0][3];
		//	in1t[1] = in1[1] - in2[1][3];
		//	in1t[2] = in1[2] - in2[2][3];

		//	out[0] = in1t[0] * in2[0][0] + in1t[1] * in2[1][0] + in1t[2] * in2[2][0];
		//	out[1] = in1t[0] * in2[0][1] + in1t[1] * in2[1][1] + in1t[2] * in2[2][1];
		//	out[2] = in1t[0] * in2[0][2] + in1t[1] * in2[1][2] + in1t[2] * in2[2][2];
		//}
		public static SourceVector VectorITransform(SourceVector input, SourceVector matrixColumn0, SourceVector matrixColumn1, SourceVector matrixColumn2, SourceVector matrixColumn3)
		{
			SourceVector output = null;
			SourceVector temp = null;

			output = new SourceVector();
			temp = new SourceVector();

			temp.x = input.x - matrixColumn3.x;
			temp.y = input.y - matrixColumn3.y;
			temp.z = input.z - matrixColumn3.z;

			output.x = temp.x * matrixColumn0.x + temp.y * matrixColumn0.y + temp.z * matrixColumn0.z;
			output.y = temp.x * matrixColumn1.x + temp.y * matrixColumn1.y + temp.z * matrixColumn1.z;
			output.z = temp.x * matrixColumn2.x + temp.y * matrixColumn2.y + temp.z * matrixColumn2.z;

			return output;
		}

		//// Rotate a vector around the Z axis (YAW)
		//void VectorYawRotate( const Vector &in, float flYaw, Vector &out)
		//{
		//	Assert( s_bMathlibInitialized );
		//	if (&in == &out )
		//	{
		//		Vector tmp = in;
		//		VectorYawRotate( tmp, flYaw, out );
		//		return;
		//	}
		//
		//	float sy, cy;
		//
		//	SinCos( DEG2RAD(flYaw), &sy, &cy );
		//
		//	out.x = in.x * cy - in.y * sy;
		//	out.y = in.x * sy + in.y * cy;
		//	out.z = in.z;
		//}
		public static SourceVector VectorYawRotate(SourceVector input, double yawInRadians)
		{
			SourceVector output = new SourceVector();
			double sy = 0;
			double cy = 0;

			sy = Math.Sin(yawInRadians);
			cy = Math.Cos(yawInRadians);

			output.x = input.x * cy - input.y * sy;
			output.y = input.x * sy + input.y * cy;
			output.z = input.z;

			return output;
		}
		public static SourceVector VectorYawRotateXandYSwap(SourceVector input, double yawInRadians)
		{
			SourceVector output = new SourceVector();
			double sy = 0;
			double cy = 0;

			sy = Math.Sin(yawInRadians);
			cy = Math.Cos(yawInRadians);

			output.x = input.x * sy + input.y * cy;
			output.y = input.x * cy - input.y * sy;
			output.z = input.z;

			return output;
		}
		//------
		//Public Function RotateAboutZAxis(ByVal input As SourceVector, ByVal angleInRadians As Double) As SourceVector
		//	Dim poseToBoneColumn0 As New SourceVector()
		//	Dim poseToBoneColumn1 As New SourceVector()
		//	Dim poseToBoneColumn2 As New SourceVector()
		//	Dim poseToBoneColumn3 As New SourceVector()

		//	poseToBoneColumn0.x = Math.Cos(angleInRadians)
		//	poseToBoneColumn0.y = Math.Sin(angleInRadians)
		//	poseToBoneColumn0.z = 0

		//	poseToBoneColumn1.x = -poseToBoneColumn0.y
		//	poseToBoneColumn1.y = poseToBoneColumn0.x
		//	poseToBoneColumn1.z = 0

		//	poseToBoneColumn2.x = 0
		//	poseToBoneColumn2.y = 0
		//	poseToBoneColumn2.z = 1

		//	poseToBoneColumn3.x = 0
		//	poseToBoneColumn3.y = 0
		//	poseToBoneColumn3.z = 0

		//	Return MathModule.VectorITransform(input, poseToBoneColumn0, poseToBoneColumn1, poseToBoneColumn2, poseToBoneColumn3)
		//End Function

		//Public Function NormalizeQuaternion(ByVal quat As SourceQuaternion) As SourceQuaternion
		//	Dim resultQuat As New SourceQuaternion()
		//	Dim magnitude As Double

		//	magnitude = Math.Sqrt(quat.w ^ 2 + quat.x ^ 2 + quat.y ^ 2 + quat.z ^ 2)
		//	resultQuat.w = quat.w / magnitude
		//	resultQuat.x = quat.x / magnitude
		//	resultQuat.y = quat.y / magnitude
		//	resultQuat.z = quat.z / magnitude

		//	Return resultQuat
		//End Function

		//TODO: Why did I not use Valve's code?
		//FROM: https://github.com/ValveSoftware/source-sdk-2013/blob/master/sp/src/mathlib/mathlib_base.cpp
		////-----------------------------------------------------------------------------
		//// Purpose: Converts a quaternion into engine angles
		//// Input : *quaternion - q3 + q0.i + q1.j + q2.k
		//// *outAngles - PITCH, YAW, ROLL
		////-----------------------------------------------------------------------------
		//void QuaternionAngles( const Quaternion &q, QAngle &angles )

		//'FROM: http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
		//Public Function ToEulerAngles(ByVal q As SourceQuaternion) As SourceVector
		//	' Store the Euler angles in radians
		//	Dim pitchYawRoll As New SourceVector()

		//	Dim sqx As Double = q.x * q.x
		//	Dim sqy As Double = q.y * q.y
		//	Dim sqz As Double = q.z * q.z
		//	Dim sqw As Double = q.w * q.w

		//	' If quaternion is normalised the unit is one, otherwise it is the correction factor
		//	Dim unit As Double = sqx + sqy + sqz + sqw

		//	Dim test As Double = q.x * q.y + q.z * q.w
		//	'double test = q.X * q.Z - q.W * q.Y;

		//	If test > 0.4999F * unit Then
		//		' 0.4999f OR 0.5f - EPSILON
		//		' Singularity at north pole
		//		pitchYawRoll.y = 2.0F * CSng(Math.Atan2(q.x, q.w))
		//		' Yaw
		//		'pitchYawRoll.X = PIOVER2
		//		pitchYawRoll.x = Math.PI * 0.5
		//		' Pitch
		//		pitchYawRoll.z = 0.0F
		//		' Roll
		//	ElseIf test < -0.4999F * unit Then
		//		' -0.4999f OR -0.5f + EPSILON
		//		' Singularity at south pole
		//		pitchYawRoll.y = -2.0F * CSng(Math.Atan2(q.x, q.w))
		//		' Yaw
		//		'pitchYawRoll.X = -PIOVER2
		//		pitchYawRoll.x = -Math.PI * 0.5
		//		' Pitch
		//		pitchYawRoll.z = 0.0F
		//		' Roll
		//	Else
		//		pitchYawRoll.y = CSng(Math.Atan2(2.0F * q.y * q.w - 2.0F * q.x * q.z, sqx - sqy - sqz + sqw))
		//		' Yaw
		//		pitchYawRoll.x = CSng(Math.Asin(2.0F * test / unit))
		//		' Pitch
		//		' Roll
		//		'pitchYawRoll.Y = (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (sqz + sqw));      // Yaw 
		//		'pitchYawRoll.X = (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y));                                // Pitch 
		//		'pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (sqy + sqz));      // Roll 
		//		pitchYawRoll.z = CSng(Math.Atan2(2.0F * q.x * q.w - 2.0F * q.y * q.z, -sqx + sqy - sqz + sqw))
		//	End If

		//	Return pitchYawRoll
		//End Function

		//===========================================================================

		//FROM: http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
		//	double test = q1.x*q1.y + q1.z*q1.w;
		//if (test > 0.499) { // singularity at north pole
		//	heading = 2 * atan2(q1.x,q1.w);
		//	attitude = Math.PI/2;
		//	bank = 0;
		//	return;
		//}
		//if (test < -0.499) { // singularity at south pole
		//	heading = -2 * atan2(q1.x,q1.w);
		//	attitude = - Math.PI/2;
		//	bank = 0;
		//	return;
		//}
		//   double sqx = q1.x*q1.x;
		//   double sqy = q1.y*q1.y;
		//   double sqz = q1.z*q1.z;
		//   heading = atan2(2*q1.y*q1.w-2*q1.x*q1.z , 1 - 2*sqy - 2*sqz);
		//attitude = asin(2*test);
		//bank = atan2(2*q1.x*q1.w-2*q1.y*q1.z , 1 - 2*sqx - 2*sqz)
		//------
		//Public Function ToEulerAngles(ByVal q As SourceQuaternion) As SourceVector
		//	' Store the Euler angles in radians
		//	Dim pitchYawRoll As New SourceVector()

		//	Dim test As Double = q.x * q.y + q.z * q.w

		//	If test > 0.4999F Then
		//		pitchYawRoll.y = 2.0F * CSng(Math.Atan2(q.x, q.w))
		//		pitchYawRoll.x = Math.PI * 0.5
		//		pitchYawRoll.z = 0.0F
		//		'------
		//		'pitchYawRoll.x = 2.0F * CSng(Math.Atan2(q.x, q.w))
		//		'pitchYawRoll.y = Math.PI * 0.5
		//		'pitchYawRoll.z = 0.0F
		//		'------
		//		'pitchYawRoll.x = 2.0F * CSng(Math.Atan2(q.x, q.w))
		//		'pitchYawRoll.z = Math.PI * 0.5
		//		'pitchYawRoll.y = 0.0F
		//		'------
		//		'pitchYawRoll.y = 2.0F * CSng(Math.Atan2(q.x, q.w))
		//		'pitchYawRoll.z = Math.PI * 0.5
		//		'pitchYawRoll.x = 0.0F
		//		'------
		//		'pitchYawRoll.z = 2.0F * CSng(Math.Atan2(q.x, q.w))
		//		'pitchYawRoll.y = Math.PI * 0.5
		//		'pitchYawRoll.x = 0.0F
		//	ElseIf test < -0.4999F Then
		//		pitchYawRoll.y = -2.0F * CSng(Math.Atan2(q.x, q.w))
		//		pitchYawRoll.x = -Math.PI * 0.5
		//		pitchYawRoll.z = 0.0F
		//		'------
		//		'pitchYawRoll.x = -2.0F * CSng(Math.Atan2(q.x, q.w))
		//		'pitchYawRoll.y = -Math.PI * 0.5
		//		'pitchYawRoll.z = 0.0F
		//		'------
		//		'pitchYawRoll.x = -2.0F * CSng(Math.Atan2(q.x, q.w))
		//		'pitchYawRoll.z = -Math.PI * 0.5
		//		'pitchYawRoll.y = 0.0F
		//		'------
		//		'pitchYawRoll.y = -2.0F * CSng(Math.Atan2(q.x, q.w))
		//		'pitchYawRoll.z = -Math.PI * 0.5
		//		'pitchYawRoll.x = 0.0F
		//		'------
		//		'pitchYawRoll.z = -2.0F * CSng(Math.Atan2(q.x, q.w))
		//		'pitchYawRoll.y = -Math.PI * 0.5
		//		'pitchYawRoll.x = 0.0F
		//	Else
		//		Dim sqx As Double = q.x * q.x
		//		Dim sqy As Double = q.y * q.y
		//		Dim sqz As Double = q.z * q.z

		//		pitchYawRoll.y = CSng(Math.Atan2(2.0F * q.y * q.w - 2.0F * q.x * q.z, 1 - 2 * sqy - 2 * sqz))
		//		pitchYawRoll.x = CSng(Math.Asin(2.0F * test))
		//		pitchYawRoll.z = CSng(Math.Atan2(2.0F * q.x * q.w - 2.0F * q.y * q.z, 1 - 2 * sqx - 2 * sqz))
		//		'------
		//		'pitchYawRoll.x = CSng(Math.Atan2(2.0F * q.y * q.w - 2.0F * q.x * q.z, 1 - 2 * sqy - 2 * sqz))
		//		'pitchYawRoll.y = CSng(Math.Asin(2.0F * test))
		//		'pitchYawRoll.z = CSng(Math.Atan2(2.0F * q.x * q.w - 2.0F * q.y * q.z, 1 - 2 * sqx - 2 * sqz))
		//		'------
		//		'pitchYawRoll.x = CSng(Math.Atan2(2.0F * q.y * q.w - 2.0F * q.x * q.z, 1 - 2 * sqy - 2 * sqz))
		//		'pitchYawRoll.z = CSng(Math.Asin(2.0F * test))
		//		'pitchYawRoll.y = CSng(Math.Atan2(2.0F * q.x * q.w - 2.0F * q.y * q.z, 1 - 2 * sqx - 2 * sqz))
		//		'------
		//		'pitchYawRoll.y = CSng(Math.Atan2(2.0F * q.y * q.w - 2.0F * q.x * q.z, 1 - 2 * sqy - 2 * sqz))
		//		'pitchYawRoll.z = CSng(Math.Asin(2.0F * test))
		//		'pitchYawRoll.x = CSng(Math.Atan2(2.0F * q.x * q.w - 2.0F * q.y * q.z, 1 - 2 * sqx - 2 * sqz))
		//		'------
		//		'pitchYawRoll.z = CSng(Math.Atan2(2.0F * q.y * q.w - 2.0F * q.x * q.z, 1 - 2 * sqy - 2 * sqz))
		//		'pitchYawRoll.y = CSng(Math.Asin(2.0F * test))
		//		'pitchYawRoll.x = CSng(Math.Atan2(2.0F * q.x * q.w - 2.0F * q.y * q.z, 1 - 2 * sqx - 2 * sqz))
		//		'------
		//		'pitchYawRoll.z = CSng(Math.Atan2(2.0F * q.y * q.w - 2.0F * q.x * q.z, 1 - 2 * sqy - 2 * sqz))
		//		'pitchYawRoll.x = CSng(Math.Asin(2.0F * test))
		//		'pitchYawRoll.y = -CSng(Math.Atan2(2.0F * q.x * q.w - 2.0F * q.y * q.z, 1 - 2 * sqx - 2 * sqz))
		//	End If

		//	Return pitchYawRoll
		//End Function
		//------
		//Public Function ToEulerAngles(ByVal q As SourceQuaternion) As SourceVector
		//	' Store the Euler angles in radians
		//	Dim pitchYawRoll As New SourceVector()

		//	Dim test As Double = q.x * q.y + q.z * q.w

		//	Dim sqx As Double = q.x * q.x
		//	Dim sqy As Double = q.y * q.y
		//	Dim sqz As Double = q.z * q.z
		//	Dim sqw As Double = q.w * q.w

		//	' If quaternion is normalised the unit is one, otherwise it is the correction factor
		//	Dim unit As Double = sqx + sqy + sqz + sqw
		//	Dim epsilon As Double = 0.4999F

		//	If test > epsilon * unit Then
		//		pitchYawRoll.x = 2.0F * Math.Atan2(q.x, q.w)
		//		pitchYawRoll.y = Math.PI * 0.5
		//		pitchYawRoll.z = 0.0F
		//	ElseIf test < -epsilon * unit Then
		//		pitchYawRoll.x = -2.0F * Math.Atan2(q.x, q.w)
		//		pitchYawRoll.y = -Math.PI * 0.5
		//		pitchYawRoll.z = 0.0F
		//	Else
		//		'pitchYawRoll.x = Math.Atan2(2.0F * q.y * q.w - 2.0F * q.x * q.z, sqx - sqy - sqz + sqw)
		//		'pitchYawRoll.y = Math.Asin(2.0F * test / unit)
		//		'pitchYawRoll.z = Math.Atan2(2.0F * q.x * q.w - 2.0F * q.y * q.z, -sqx + sqy - sqz + sqw)

		//		'TEST: Code from Cra0kalo
		//		'pitchYawRoll.z = rad2deg(Math.Atan2(2.0 * (quat.j * quat.k + quat.i * quat.real), (-sqx - sqy + sqz + sqw)))
		//		'pitchYawRoll.x = rad2deg(Math.Asin(-2.0 * (quat.i * quat.k - quat.j * quat.real)))
		//		'pitchYawRoll.y = rad2deg(Math.Atan2(2.0 * (quat.i * quat.j + quat.k * quat.real), (sqx - sqy - sqz + sqw)))
		//		pitchYawRoll.x = Math.Atan2(2.0F * (q.y * q.z + q.x * q.w), -sqx - sqy + sqz + sqw)
		//		pitchYawRoll.y = Math.Asin(-2.0F * (q.x * q.z - q.y * q.w))
		//		pitchYawRoll.z = Math.Atan2(2.0F * (q.x * q.y + q.z * q.w), sqx - sqy - sqz + sqw)
		//	End If

		//	Return pitchYawRoll
		//End Function
		//------
		public static SourceVector ToEulerAngles(SourceQuaternion q)
		{
			//NOTE: v_dual_pistola.mdl
			//NOTE: Bad arms, bad clip, good view.
			//Return MathModule.Eul_FromQuat(q, 0, 1, 2, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.R)
			//NOTE: Bad arms, good clip, good view.
			//Return MathModule.Eul_FromQuat(q, 1, 2, 0, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.R)
			//NOTE: Good arms, bad clip, view.
			//Return MathModule.Eul_FromQuat(q, 2, 0, 1, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.R)

			//NOTE:  arms, bad clip (reload* anims), view.
			//Return MathModule.Eul_FromQuat(q, 0, 1, 2, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.R)
			//NOTE: Bad arms, clip, bad view.
			//Return MathModule.Eul_FromQuat(q, 1, 2, 0, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.R)
			//NOTE: Bad arms, bad clip, bad view.
			//Return MathModule.Eul_FromQuat(q, 2, 0, 1, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.R)

			//NOTE: Good arms, good clip, good view.
			return MathModule.Eul_FromQuat(q, 0, 1, 2, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.S);
			//NOTE: Bad arms, bad clip, good view.
			//Return MathModule.Eul_FromQuat(q, 1, 2, 0, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.S)
			//NOTE:  arms, clip,  view.
			//Return MathModule.Eul_FromQuat(q, 2, 0, 1, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.S)
			//TEST: Did not work for L4D2 v_huntingrifle.
			//Return MathModule.Eul_FromQuat(q, 1, 0, 2, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.S)

			//NOTE:  arms, clip,  view.
			//Return MathModule.Eul_FromQuat(q, 0, 1, 2, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.S)
			//NOTE: Bad arms, bad clip, bad view.
			//Return MathModule.Eul_FromQuat(q, 1, 2, 0, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.S)
			//NOTE:  arms, clip,  view.
			//Return MathModule.Eul_FromQuat(q, 2, 0, 1, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.S)
		}

		public static SourceVector ToEulerAngles2531(SourceQuaternion q)
		{
			//NOTE: Incorrect.
			//Return MathModule.Eul_FromQuat(q, 0, 1, 2, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.R)
			//NOTE: Incorrect.
			//Return MathModule.Eul_FromQuat(q, 1, 2, 0, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.R)
			//Return MathModule.Eul_FromQuat(q, 2, 0, 1, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.R)

			//NOTE: Incorrect.
			//Return MathModule.Eul_FromQuat(q, 0, 1, 2, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.R)
			//Return MathModule.Eul_FromQuat(q, 1, 2, 0, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.R)
			//Return MathModule.Eul_FromQuat(q, 2, 0, 1, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.R)

			//NOTE: Not completely correct.
			return MathModule.Eul_FromQuat(q, 0, 1, 2, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.S);
			//NOTE: Incorrect.
			//Return MathModule.Eul_FromQuat(q, 1, 2, 0, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.S)
			//Return MathModule.Eul_FromQuat(q, 2, 0, 1, 0, EulerParity.Even, EulerRepeat.No, EulerFrame.S)

			//NOTE: Incorrect.
			//Return MathModule.Eul_FromQuat(q, 0, 1, 2, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.S)
			//Return MathModule.Eul_FromQuat(q, 1, 2, 0, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.S)
			//Return MathModule.Eul_FromQuat(q, 2, 0, 1, 0, EulerParity.Odd, EulerRepeat.No, EulerFrame.S)
		}

		//FROM: http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/indexLocal.htm
		//double sqw = q1.w*q1.w;
		//double sqx = q1.x*q1.x;
		//double sqy = q1.y*q1.y;
		//double sqz = q1.z*q1.z;
		//heading = Math.atan2(2.0 * (q1.x*q1.y + q1.z*q1.w),(sqx - sqy - sqz + sqw));
		//bank = Math.atan2(2.0 * (q1.y*q1.z + q1.x*q1.w),(-sqx - sqy + sqz + sqw));
		//attitude = Math.asin(-2.0 * (q1.x*q1.z - q1.y*q1.w));

		//Public Function ToEulerAngles(ByVal q As SourceQuaternion) As SourceVector
		//	Dim pitchYawRoll As New SourceVector()

		//	Dim sqx As Double = q.x * q.x
		//	Dim sqy As Double = q.y * q.y
		//	Dim sqz As Double = q.z * q.z
		//	Dim sqw As Double = q.w * q.w

		//	pitchYawRoll.y = CSng(Math.Atan2(2 * (q.x * q.y + q.z * q.w), sqx - sqy - sqz + sqw))
		//	pitchYawRoll.x = CSng(Math.Atan2(2 * (q.y * q.z + q.x * q.w), -sqx - sqy + sqz + sqw))
		//	pitchYawRoll.z = CSng(Math.Asin(-2 * (q.x * q.z - q.y * q.w)))

		//	Return pitchYawRoll
		//End Function

		//===========================================================================



		//FROM: http://tog.acm.org/resources/GraphicsGems/gemsiv/euler_angle/
		//          QuatTypes.h
		//          EulerAngles.h
		//          EulerAngles.c



		///**** QuatTypes.h - Basic type declarations ****/
		//#ifndef _H_QuatTypes
		//#define _H_QuatTypes
		///*** Definitions ***/
		//typedef struct {float x, y, z, w;} Quat; /* Quaternion */
		//enum QuatPart {X, Y, Z, W};
		//typedef float HMatrix[4][4]; /* Right-handed, for column vectors */
		//typedef Quat EulerAngles;    /* (x,y,z)=ang 1,2,3, w=order code  */
		//#endif



		///**** EulerAngles.h - Support for 24 angle schemes ****/
		///* Ken Shoemake, 1993 */
		//#ifndef _H_EulerAngles
		//#define _H_EulerAngles
		//#include "QuatTypes.h"
		///*** Order type constants, constructors, extractors ***/
		//    /* There are 24 possible conventions, designated by:    */
		//    /*	  o EulAxI = axis used initially		    */
		//    /*	  o EulPar = parity of axis permutation		    */
		//    /*	  o EulRep = repetition of initial axis as last	    */
		//    /*	  o EulFrm = frame from which axes are taken	    */
		//    /* Axes I,J,K will be a permutation of X,Y,Z.	    */
		//    /* Axis H will be either I or K, depending on EulRep.   */
		//    /* Frame S takes axes from initial static frame.	    */
		//    /* If ord = (AxI=X, Par=Even, Rep=No, Frm=S), then	    */
		//    /* {a,b,c,ord} means Rz(c)Ry(b)Rx(a), where Rz(c)v	    */
		//    /* rotates v around Z by c radians.			    */
		//#define EulFrmS	     0
		//#define EulFrmR	     1
		//#define EulFrm(ord)  ((unsigned)(ord)&1)
		//#define EulRepNo     0
		//#define EulRepYes    1
		//#define EulRep(ord)  (((unsigned)(ord)>>1)&1)
		//#define EulParEven   0
		//#define EulParOdd    1
		//#define EulPar(ord)  (((unsigned)(ord)>>2)&1)
		//#define EulSafe	     "\000\001\002\000"
		//#define EulNext	     "\001\002\000\001"
		//#define EulAxI(ord)  ((int)(EulSafe[(((unsigned)(ord)>>3)&3)]))
		//#define EulAxJ(ord)  ((int)(EulNext[EulAxI(ord)+(EulPar(ord)==EulParOdd)]))
		//#define EulAxK(ord)  ((int)(EulNext[EulAxI(ord)+(EulPar(ord)!=EulParOdd)]))
		//#define EulAxH(ord)  ((EulRep(ord)==EulRepNo)?EulAxK(ord):EulAxI(ord))
		//    /* EulGetOrd unpacks all useful information about order simultaneously. */
		//#define EulGetOrd(ord,i,j,k,h,n,s,f) {unsigned o=ord;f=o&1;o>>=1;s=o&1;o>>=1;\
		//    n=o&1;o>>=1;i=EulSafe[o&3];j=EulNext[i+n];k=EulNext[i+1-n];h=s?k:i;}
		//    /* EulOrd creates an order value between 0 and 23 from 4-tuple choices. */
		//#define EulOrd(i,p,r,f)	   (((((((i)<<1)+(p))<<1)+(r))<<1)+(f))
		//    /* Static axes */
		//#define EulOrdXYZs    EulOrd(X,EulParEven,EulRepNo,EulFrmS)
		//#define EulOrdXYXs    EulOrd(X,EulParEven,EulRepYes,EulFrmS)
		//#define EulOrdXZYs    EulOrd(X,EulParOdd,EulRepNo,EulFrmS)
		//#define EulOrdXZXs    EulOrd(X,EulParOdd,EulRepYes,EulFrmS)
		//#define EulOrdYZXs    EulOrd(Y,EulParEven,EulRepNo,EulFrmS)
		//#define EulOrdYZYs    EulOrd(Y,EulParEven,EulRepYes,EulFrmS)
		//#define EulOrdYXZs    EulOrd(Y,EulParOdd,EulRepNo,EulFrmS)
		//#define EulOrdYXYs    EulOrd(Y,EulParOdd,EulRepYes,EulFrmS)
		//#define EulOrdZXYs    EulOrd(Z,EulParEven,EulRepNo,EulFrmS)
		//#define EulOrdZXZs    EulOrd(Z,EulParEven,EulRepYes,EulFrmS)
		//#define EulOrdZYXs    EulOrd(Z,EulParOdd,EulRepNo,EulFrmS)
		//#define EulOrdZYZs    EulOrd(Z,EulParOdd,EulRepYes,EulFrmS)
		//    /* Rotating axes */
		//#define EulOrdZYXr    EulOrd(X,EulParEven,EulRepNo,EulFrmR)
		//#define EulOrdXYXr    EulOrd(X,EulParEven,EulRepYes,EulFrmR)
		//#define EulOrdYZXr    EulOrd(X,EulParOdd,EulRepNo,EulFrmR)
		//#define EulOrdXZXr    EulOrd(X,EulParOdd,EulRepYes,EulFrmR)
		//#define EulOrdXZYr    EulOrd(Y,EulParEven,EulRepNo,EulFrmR)
		//#define EulOrdYZYr    EulOrd(Y,EulParEven,EulRepYes,EulFrmR)
		//#define EulOrdZXYr    EulOrd(Y,EulParOdd,EulRepNo,EulFrmR)
		//#define EulOrdYXYr    EulOrd(Y,EulParOdd,EulRepYes,EulFrmR)
		//#define EulOrdYXZr    EulOrd(Z,EulParEven,EulRepNo,EulFrmR)
		//#define EulOrdZXZr    EulOrd(Z,EulParEven,EulRepYes,EulFrmR)
		//#define EulOrdXYZr    EulOrd(Z,EulParOdd,EulRepNo,EulFrmR)
		//#define EulOrdZYZr    EulOrd(Z,EulParOdd,EulRepYes,EulFrmR)

		//EulerAngles Eul_(float ai, float aj, float ah, int order);
		//Quat Eul_ToQuat(EulerAngles ea);
		//void Eul_ToHMatrix(EulerAngles ea, HMatrix M);
		//EulerAngles Eul_FromHMatrix(HMatrix M, int order);
		//EulerAngles Eul_FromQuat(Quat q, int order);
		//#endif



		///**** EulerAngles.c - Convert Euler angles to/from matrix or quat ****/
		///* Ken Shoemake, 1993 */
		//#include <math.h>
		//#include <float.h>
		//#include "EulerAngles.h"

		//EulerAngles Eul_(float ai, float aj, float ah, int order)
		//{
		//    EulerAngles ea;
		//    ea.x = ai; ea.y = aj; ea.z = ah;
		//    ea.w = order;
		//    return (ea);
		//}
		///* Construct quaternion from Euler angles (in radians). */
		//Quat Eul_ToQuat(EulerAngles ea)
		//{
		//    Quat qu;
		//    double a[3], ti, tj, th, ci, cj, ch, si, sj, sh, cc, cs, sc, ss;
		//    int i,j,k,h,n,s,f;
		//    EulGetOrd(ea.w,i,j,k,h,n,s,f);
		//    if (f==EulFrmR) {float t = ea.x; ea.x = ea.z; ea.z = t;}
		//    if (n==EulParOdd) ea.y = -ea.y;
		//    ti = ea.x*0.5; tj = ea.y*0.5; th = ea.z*0.5;
		//    ci = cos(ti);  cj = cos(tj);  ch = cos(th);
		//    si = sin(ti);  sj = sin(tj);  sh = sin(th);
		//    cc = ci*ch; cs = ci*sh; sc = si*ch; ss = si*sh;
		//    if (s==EulRepYes) {
		//	a[i] = cj*(cs + sc);	/* Could speed up with */
		//	a[j] = sj*(cc + ss);	/* trig identities. */
		//	a[k] = sj*(cs - sc);
		//	qu.w = cj*(cc - ss);
		//    } else {
		//	a[i] = cj*sc - sj*cs;
		//	a[j] = cj*ss + sj*cc;
		//	a[k] = cj*cs - sj*sc;
		//	qu.w = cj*cc + sj*ss;
		//    }
		//    if (n==EulParOdd) a[j] = -a[j];
		//    qu.x = a[X]; qu.y = a[Y]; qu.z = a[Z];
		//    return (qu);
		//}

		///* Construct matrix from Euler angles (in radians). */
		//void Eul_ToHMatrix(EulerAngles ea, HMatrix M)
		//{
		//    double ti, tj, th, ci, cj, ch, si, sj, sh, cc, cs, sc, ss;
		//    int i,j,k,h,n,s,f;
		//    EulGetOrd(ea.w,i,j,k,h,n,s,f);
		//    if (f==EulFrmR) {float t = ea.x; ea.x = ea.z; ea.z = t;}
		//    if (n==EulParOdd) {ea.x = -ea.x; ea.y = -ea.y; ea.z = -ea.z;}
		//    ti = ea.x;	  tj = ea.y;	th = ea.z;
		//    ci = cos(ti); cj = cos(tj); ch = cos(th);
		//    si = sin(ti); sj = sin(tj); sh = sin(th);
		//    cc = ci*ch; cs = ci*sh; sc = si*ch; ss = si*sh;
		//    if (s==EulRepYes) {
		//	M[i][i] = cj;	  M[i][j] =  sj*si;    M[i][k] =  sj*ci;
		//	M[j][i] = sj*sh;  M[j][j] = -cj*ss+cc; M[j][k] = -cj*cs-sc;
		//	M[k][i] = -sj*ch; M[k][j] =  cj*sc+cs; M[k][k] =  cj*cc-ss;
		//    } else {
		//	M[i][i] = cj*ch; M[i][j] = sj*sc-cs; M[i][k] = sj*cc+ss;
		//	M[j][i] = cj*sh; M[j][j] = sj*ss+cc; M[j][k] = sj*cs-sc;
		//	M[k][i] = -sj;	 M[k][j] = cj*si;    M[k][k] = cj*ci;
		//    }
		//    M[W][X]=M[W][Y]=M[W][Z]=M[X][W]=M[Y][W]=M[Z][W]=0.0; M[W][W]=1.0;
		//}

		///* Convert matrix to Euler angles (in radians). */
		//EulerAngles Eul_FromHMatrix(HMatrix M, int order)
		//{
		//    EulerAngles ea;
		//    int i,j,k,h,n,s,f;
		//    EulGetOrd(order,i,j,k,h,n,s,f);
		//    if (s==EulRepYes) {
		//	double sy = sqrt(M[i][j]*M[i][j] + M[i][k]*M[i][k]);
		//	if (sy > 16*FLT_EPSILON) {
		//	    ea.x = atan2(M[i][j], M[i][k]);
		//	    ea.y = atan2(sy, M[i][i]);
		//	    ea.z = atan2(M[j][i], -M[k][i]);
		//	} else {
		//	    ea.x = atan2(-M[j][k], M[j][j]);
		//	    ea.y = atan2(sy, M[i][i]);
		//	    ea.z = 0;
		//	}
		//    } else {
		//	double cy = sqrt(M[i][i]*M[i][i] + M[j][i]*M[j][i]);
		//	if (cy > 16*FLT_EPSILON) {
		//	    ea.x = atan2(M[k][j], M[k][k]);
		//	    ea.y = atan2(-M[k][i], cy);
		//	    ea.z = atan2(M[j][i], M[i][i]);
		//	} else {
		//	    ea.x = atan2(-M[j][k], M[j][j]);
		//	    ea.y = atan2(-M[k][i], cy);
		//	    ea.z = 0;
		//	}
		//    }
		//    if (n==EulParOdd) {ea.x = -ea.x; ea.y = - ea.y; ea.z = -ea.z;}
		//    if (f==EulFrmR) {float t = ea.x; ea.x = ea.z; ea.z = t;}
		//    ea.w = order;
		//    return (ea);
		//}

		///* Convert quaternion to Euler angles (in radians). */
		//EulerAngles Eul_FromQuat(Quat q, int order)
		//{
		//    HMatrix M;
		//    double Nq = q.x*q.x+q.y*q.y+q.z*q.z+q.w*q.w;
		//    double s = (Nq > 0.0) ? (2.0 / Nq) : 0.0;
		//    double xs = q.x*s,	  ys = q.y*s,	 zs = q.z*s;
		//    double wx = q.w*xs,	  wy = q.w*ys,	 wz = q.w*zs;
		//    double xx = q.x*xs,	  xy = q.x*ys,	 xz = q.x*zs;
		//    double yy = q.y*ys,	  yz = q.y*zs,	 zz = q.z*zs;
		//    M[X][X] = 1.0 - (yy + zz); M[X][Y] = xy - wz; M[X][Z] = xz + wy;
		//    M[Y][X] = xy + wz; M[Y][Y] = 1.0 - (xx + zz); M[Y][Z] = yz - wx;
		//    M[Z][X] = xz - wy; M[Z][Y] = yz + wx; M[Z][Z] = 1.0 - (xx + yy);
		//    M[W][X]=M[W][Y]=M[W][Z]=M[X][W]=M[Y][W]=M[Z][W]=0.0; M[W][W]=1.0;
		//    return (Eul_FromHMatrix(M, order));
		//}

		private static SourceVector Eul_FromHMatrix(double[,] M, int i, int j, int k, int h, EulerParity parity, EulerRepeat repeat, EulerFrame frame)
		{
			SourceVector ea = new SourceVector();

			//Dim i As Integer, j As Integer, k As Integer, h As Integer, n As Integer, r As Integer, _
			// f As Integer
			//EulGetOrd(order, i, j, k, h, n, _
			// r, f)

			if (repeat == EulerRepeat.Yes)
			{
				double sy = Math.Sqrt(M[i, j] * M[i, j] + M[i, k] * M[i, k]);
				if (sy > 16 * FLT_EPSILON)
				{
					ea.x = Math.Atan2(M[i, j], M[i, k]);
					ea.y = Math.Atan2(sy, M[i, i]);
					ea.z = Math.Atan2(M[j, i], -M[k, i]);
				}
				else
				{
					ea.x = Math.Atan2(-M[j, k], M[j, j]);
					ea.y = Math.Atan2(sy, M[i, i]);
					ea.z = 0;
				}
			}
			else
			{
				double cy = Math.Sqrt(M[i, i] * M[i, i] + M[j, i] * M[j, i]);
				if (cy > 16 * FLT_EPSILON)
				{
					ea.x = Math.Atan2(M[k, j], M[k, k]);
					ea.y = Math.Atan2(-M[k, i], cy);
					ea.z = Math.Atan2(M[j, i], M[i, i]);
				}
				else
				{
					ea.x = Math.Atan2(-M[j, k], M[j, j]);
					ea.y = Math.Atan2(-M[k, i], cy);
					ea.z = 0;
				}
			}

			if (parity == EulerParity.Odd)
			{
				ea.x = -ea.x;
				ea.y = -ea.y;
				ea.z = -ea.z;
			}

			if (frame == EulerFrame.R)
			{
				double t = ea.x;
				ea.x = ea.z;
				ea.z = t;
			}

			//ea.w = order
			return ea;
		}

		private static SourceVector Eul_FromQuat(SourceQuaternion q, int i, int j, int k, int h, EulerParity parity, EulerRepeat repeat, EulerFrame frame)
		{
			double[,] M =
			{
				{0, 0, 0, 0},
				{0, 0, 0, 0},
				{0, 0, 0, 0},
				{0, 0, 0, 0}
			};
			double Nq = 0;
			double s = 0;
			double xs = 0;
			double ys = 0;
			double zs = 0;
			double wx = 0;
			double wy = 0;
			double wz = 0;
			double xx = 0;
			double xy = 0;
			double xz = 0;
			double yy = 0;
			double yz = 0;
			double zz = 0;

			Nq = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
			if (Nq > 0)
			{
				s = 2.0 / Nq;
			}
			else
			{
				s = 0;
			}
			xs = q.x * s;
			ys = q.y * s;
			zs = q.z * s;

			wx = q.w * xs;
			wy = q.w * ys;
			wz = q.w * zs;
			xx = q.x * xs;
			xy = q.x * ys;
			xz = q.x * zs;
			yy = q.y * ys;
			yz = q.y * zs;
			zz = q.z * zs;

			M[0, 0] = 1.0 - (yy + zz);
			M[0, 1] = xy - wz;
			M[0, 2] = xz + wy;
			M[1, 0] = xy + wz;
			M[1, 1] = 1.0 - (xx + zz);
			M[1, 2] = yz - wx;
			M[2, 0] = xz - wy;
			M[2, 1] = yz + wx;
			M[2, 2] = 1.0 - (xx + yy);
			M[3, 3] = 1.0;

			return Eul_FromHMatrix(M, i, j, k, h, parity, repeat, frame);
		}

		private enum EulerParity
		{
			Even,
			Odd
		}

		private enum EulerRepeat
		{
			No,
			Yes
		}

		private enum EulerFrame
		{
			S,
			R
		}

		private const double FLT_EPSILON = 0.00001;

	}

}