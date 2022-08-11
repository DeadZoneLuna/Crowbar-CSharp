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
	public class SourceQuaternion48bitsSmallest3
	{

		public SourceQuaternion48bitsSmallest3()
		{
			theQuaternion = new SourceQuaternion();
			theQuaternionIsComputed = false;
		}

		public ushort AInput
		{
			get
			{
				return theAInput;
			}
			set
			{
				if (theAInput != value)
				{
					theAInput = value;
					theQuaternionIsComputed = false;
				}
			}
		}

		public ushort BInput
		{
			get
			{
				return theBInput;
			}
			set
			{
				if (theBInput != value)
				{
					theBInput = value;
					theQuaternionIsComputed = false;
				}
			}
		}

		public ushort CInput
		{
			get
			{
				return theCInput;
			}
			set
			{
				if (theCInput != value)
				{
					theCInput = value;
					theQuaternionIsComputed = false;
				}
			}
		}

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

		private void ComputeQuaternion()
		{
			if (!theQuaternionIsComputed)
			{
				int missingComponentType = 0;
				int integerA = 0;
				int integerB = 0;
				int integerC = 0;
				double a = 0;
				double b = 0;
				double c = 0;



				// Get component type from first 2 bits.
				missingComponentType = (theAInput & 0xC000) >> 14;

				// Get A from last 13 bits of A and first 2 bits of B.
				integerA = (theAInput & 0x1FFF) | ((theBInput & 0xC000) >> 14);

				// Get B from last 14 bits of B and first 1 bits of C.
				integerB = (theBInput & 0x3FFF) | ((theCInput & 0x8000) >> 15);

				// Get C from last 15 bits of C.
				integerC = (theCInput & 0x7FFF) >> 1;

				a = integerA / 1024.0F * (Maximum - Minimum) + Minimum;
				b = integerB / 1024.0F * (Maximum - Minimum) + Minimum;
				c = integerC / 1024.0F * (Maximum - Minimum) + Minimum;
				//======
				//' Get A from first 15 bits of A.
				//integerA = (Me.theAInput And &HFFFE) >> 1

				//' Get B from last 1 bits of A and first 14 bits of B.
				//integerB = ((Me.theAInput And &H1) << 15) Or ((Me.theBInput And &H8000) >> 15)

				//' Get C from last 2 bits of B and first 13 bits of C.
				//integerC = (Me.theBInput And &H1FFF) Or ((Me.theCInput And &HC000) >> 14)

				//' Get component type from last 3 bits.
				//missingComponentType = (Me.theCInput And &HC000) >> 14

				//a = integerA / 1024.0F * (Maximum - Minimum) + Minimum
				//b = integerB / 1024.0F * (Maximum - Minimum) + Minimum
				//c = integerC / 1024.0F * (Maximum - Minimum) + Minimum
				//======
				//3-15-15-15 in axisFlag s2 s1 s0 (High to low)
				//so:
				//730b 5dFB c15F
				//011 100110000101101 011101111110111 100000101011111
				//3 4C2D 3BF7 415F
				//a = 1.41421 * (integerA - &H3FFF) / &H7FFF
				//b = 1.41421 * (integerB - &H3FFF) / &H7FFF
				//c = 1.41421 * (integerC - &H3FFF) / &H7FFF




				if (missingComponentType == SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_X)
				{
					theQuaternion.x = GetMissingComponent(a, b, c);
					theQuaternion.y = a;
					theQuaternion.z = b;
					theQuaternion.w = c;
				}
				else if (missingComponentType == SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_Y)
				{
					theQuaternion.x = a;
					theQuaternion.y = GetMissingComponent(a, b, c);
					theQuaternion.z = b;
					theQuaternion.w = c;
				}
				else if (missingComponentType == SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_Z)
				{
					theQuaternion.x = a;
					theQuaternion.y = b;
					theQuaternion.z = GetMissingComponent(a, b, c);
					theQuaternion.w = c;
				}
				else if (missingComponentType == SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_W)
				{
					theQuaternion.x = a;
					theQuaternion.y = b;
					theQuaternion.z = c;
					theQuaternion.w = GetMissingComponent(a, b, c);
				}

				theQuaternionIsComputed = true;
			}
		}

		private double GetMissingComponent(double a, double b, double c)
		{
			return Math.Sqrt(1 - a * a - b * b - c * c);
		}

		private const int MISSING_COMPONENT_X = 0;
		private const int MISSING_COMPONENT_Y = 1;
		private const int MISSING_COMPONENT_Z = 2;
		private const int MISSING_COMPONENT_W = 3;
		//private const float Minimum = -1.0f / 1.414214f; // note: 1.0f / sqrt(2)
		//private const float Maximum = +1.0f / 1.414214f;
		private const double Minimum = -1 / 1.414214;
		private const double Maximum = 1 / 1.414214;

		private ushort theAInput;
		private ushort theBInput;
		private ushort theCInput;

		private SourceQuaternion theQuaternion;

		private bool theQuaternionIsComputed;

		//<StructLayout(LayoutKind.Explicit)> _
		//Public Structure IntegerAndSingleUnion
		//	<FieldOffset(0)> Public i As Int32
		//	<FieldOffset(0)> Public s As Single
		//End Structure

	}

}