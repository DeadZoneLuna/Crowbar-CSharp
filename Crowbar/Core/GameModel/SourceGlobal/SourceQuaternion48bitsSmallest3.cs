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
	public class SourceQuaternion48bitsSmallest3
	{

		public SourceQuaternion48bitsSmallest3()
		{
			this.theQuaternion = new SourceQuaternion();
			this.theQuaternionIsComputed = false;
		}

		public ushort AInput
		{
			get
			{
				return this.theAInput;
			}
			set
			{
				if (this.theAInput != value)
				{
					this.theAInput = value;
					this.theQuaternionIsComputed = false;
				}
			}
		}

		public ushort BInput
		{
			get
			{
				return this.theBInput;
			}
			set
			{
				if (this.theBInput != value)
				{
					this.theBInput = value;
					this.theQuaternionIsComputed = false;
				}
			}
		}

		public ushort CInput
		{
			get
			{
				return this.theCInput;
			}
			set
			{
				if (this.theCInput != value)
				{
					this.theCInput = value;
					this.theQuaternionIsComputed = false;
				}
			}
		}

		public double x
		{
			get
			{
				this.ComputeQuaternion();
				return this.theQuaternion.x;
			}
		}

		public double y
		{
			get
			{
				this.ComputeQuaternion();
				return this.theQuaternion.y;
			}
		}

		public double z
		{
			get
			{
				this.ComputeQuaternion();
				return this.theQuaternion.z;
			}
		}

		public double w
		{
			get
			{
				this.ComputeQuaternion();
				return this.theQuaternion.w;
			}
		}

		private void ComputeQuaternion()
		{
			if (!this.theQuaternionIsComputed)
			{
				int missingComponentType = 0;
				int integerA = 0;
				int integerB = 0;
				int integerC = 0;
				double a = 0;
				double b = 0;
				double c = 0;



				// Get component type from first 2 bits.
				missingComponentType = (this.theAInput & 0xC000) >> 14;

				// Get A from last 13 bits of A and first 2 bits of B.
				integerA = (this.theAInput & 0x1FFF) | ((this.theBInput & 0xC000) >> 14);

				// Get B from last 14 bits of B and first 1 bits of C.
				integerB = (this.theBInput & 0x3FFF) | ((this.theCInput & 0x8000) >> 15);

				// Get C from last 15 bits of C.
				integerC = (this.theCInput & 0x7FFF) >> 1;

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
					this.theQuaternion.x = this.GetMissingComponent(a, b, c);
					this.theQuaternion.y = a;
					this.theQuaternion.z = b;
					this.theQuaternion.w = c;
				}
				else if (missingComponentType == SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_Y)
				{
					this.theQuaternion.x = a;
					this.theQuaternion.y = this.GetMissingComponent(a, b, c);
					this.theQuaternion.z = b;
					this.theQuaternion.w = c;
				}
				else if (missingComponentType == SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_Z)
				{
					this.theQuaternion.x = a;
					this.theQuaternion.y = b;
					this.theQuaternion.z = this.GetMissingComponent(a, b, c);
					this.theQuaternion.w = c;
				}
				else if (missingComponentType == SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_W)
				{
					this.theQuaternion.x = a;
					this.theQuaternion.y = b;
					this.theQuaternion.z = c;
					this.theQuaternion.w = this.GetMissingComponent(a, b, c);
				}

				this.theQuaternionIsComputed = true;
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