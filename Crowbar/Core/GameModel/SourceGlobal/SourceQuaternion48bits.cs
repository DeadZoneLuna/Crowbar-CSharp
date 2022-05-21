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
	public class SourceQuaternion48bits
	{

		//FROM: SourceEngine2006_source\public\compressed_vector.h
		////=========================================================
		//// 48 bit Quaternion
		////=========================================================

		//	Class Quaternion48
		//{
		//public:
		//	// Construction/destruction:
		//	Quaternion48(void); 
		//	Quaternion48(vec_t X, vec_t Y, vec_t Z);

		//	// assignment
		//	// Quaternion& operator=(const Quaternion48 &vOther);
		//	Quaternion48& operator=(const Quaternion &vOther);
		//	operator Quaternion ();
		//private:
		//	unsigned short x:16;
		//	unsigned short y:16;
		//	unsigned short z:15;
		//	unsigned short wneg:1;
		//};



		//Public theBytes(5) As Byte
		public ushort theXInput;
		public ushort theYInput;
		public ushort theZWInput;


		//FROM: SourceEngine2006_source\public\compressed_vector.h
		//inline Quaternion48::operator Quaternion ()	
		//{
		//	static Quaternion tmp;

		//	tmp.x = ((int)x - 32768) * (1 / 32768.0);
		//	tmp.y = ((int)y - 32768) * (1 / 32768.0);
		//	tmp.z = ((int)z - 16384) * (1 / 16384.0);
		//	tmp.w = sqrt( 1 - tmp.x * tmp.x - tmp.y * tmp.y - tmp.z * tmp.z );
		//		If (wneg) Then
		//		tmp.w = -tmp.w;
		//	return tmp; 
		//}

		public double x
		{
			get
			{
				double result;

				//result = (Me.theXInput - 32768) * (1 / 32768)
				result = ((int)theXInput - 32768) * (1 / 32768.0);
				return result;
			}
		}

		public double y
		{
			get
			{
				double result;

				//result = (Me.theYInput - 32768) * (1 / 32768)
				result = ((int)theYInput - 32768) * (1 / 32768.0);
				return result;
			}
		}

		public double z
		{
			get
			{
				int zInput = 0;
				double result = 0;

				zInput = theZWInput & 0x7FFF;
				result = (zInput - 16384) * (1 / 16384.0);
				return result;
			}
		}

		public double w
		{
			get
			{
				return Math.Sqrt(1 - x * x - y * y - z * z) * wneg;
			}
		}

		public double wneg
		{
			get
			{
				if ((theZWInput & 0x8000) > 0)
				{
					return -1;
				}
				else
				{
					return 1;
				}
			}
		}

		public SourceQuaternion quaternion
		{
			get
			{
				SourceQuaternion aQuaternion = new SourceQuaternion();
				aQuaternion.x = x;
				aQuaternion.y = y;
				aQuaternion.z = z;
				aQuaternion.w = w;
				return aQuaternion;
			}
		}

		//<StructLayout(LayoutKind.Explicit)> _
		//Public Structure IntegerAndSingleUnion
		//	<FieldOffset(0)> Public i As Integer
		//	<FieldOffset(0)> Public s As Single
		//End Structure

	}

}