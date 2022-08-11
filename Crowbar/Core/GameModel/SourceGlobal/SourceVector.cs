using System;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	public class SourceVector
	{
		public SourceVector()
		{
			x = 0;
			y = 0;
			z = 0;
		}

		public SourceVector(double iX, double iY, double iZ)
		{
			x = iX;
			y = iY;
			z = iZ;
		}

		public double x;
		public double y;
		public double z;

		public string debug_text;

		public static readonly SourceVector zero =		new SourceVector(0, 0, 0);
		public static readonly SourceVector one =		new SourceVector(1, 1, 1);
		public static readonly SourceVector up =		new SourceVector(0, 0, 1);
		public static readonly SourceVector down =		new SourceVector(0, 0, -1);
		public static readonly SourceVector left =		new SourceVector(0, 1, 0);
		public static readonly SourceVector right =		new SourceVector(0, -1, 0);
		public static readonly SourceVector forward =	new SourceVector(1, 0, 0);
		public static readonly SourceVector back =		new SourceVector(-1, 0, 0);

		public SourceVector CrossProduct(SourceVector otherVector)
		{
			SourceVector crossVector = new SourceVector();

			crossVector.x = y * otherVector.z - z * otherVector.y;
			crossVector.y = z * otherVector.x - x * otherVector.z;
			crossVector.z = x * otherVector.y - y * otherVector.x;

			return crossVector;
		}

		public SourceVector Normalize()
		{
			double magnitude = 0;
			SourceVector normalVector = new SourceVector();

			magnitude = Math.Sqrt(x * x + y * y + z * z);
			normalVector.x = x / magnitude;
			normalVector.y = y / magnitude;
			normalVector.z = z / magnitude;

			return normalVector;
		}

		public static SourceVector operator+(SourceVector a, SourceVector b)
		{
			return new SourceVector
			{
				x = a.x + b.x,
				y = a.y + b.y,
				z = a.z + b.z,
			};
		}

		public static SourceVector operator-(SourceVector a, SourceVector b)
		{
			return new SourceVector
			{
				x = a.x - b.x,
				y = a.y - b.y,
				z = a.z - b.z,
			};
		}

		public static SourceVector operator -(SourceVector a)
		{
			return new SourceVector
			{
				x = -a.x,
				y = -a.y,
				z = -a.z,
			};
		}

		public static SourceVector operator*(SourceVector a, double d) 
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			return a;
		}

		public static SourceVector operator*(double d, SourceVector a)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			return a;
		}

		public static SourceVector operator *(SourceVector a, SourceVector b)
		{
			a.x *= b.x;
			a.y *= b.y;
			a.y *= b.z;
			return a;
		}

		public static SourceVector operator /(SourceVector a, SourceVector b)
		{
			a.x /= b.x;
			a.y /= b.y;
			a.y /= b.z;
			return a;
		}

		public override string ToString()
		{
			return $"{x.ToString("0.0000", Common.m_InvCulture)} {y.ToString("0.0000", Common.m_InvCulture)} {z.ToString("0.0000", Common.m_InvCulture)}";
		}
	}
}