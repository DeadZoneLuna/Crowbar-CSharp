using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceVector48bits
	{

		//FROM: SourceEngine2006_source\public\compressed_vector.h
		////=========================================================
		//// Fit a 3D vector in 48 bits
		////=========================================================

		//	Class Vector48
		//{
		//public:
		//	// Construction/destruction:
		//	Vector48(void) {}
		//	Vector48(vec_t X, vec_t Y, vec_t Z) { x.SetFloat( X ); y.SetFloat( Y ); z.SetFloat( Z ); }

		//	// assignment
		//	Vector48& operator=(const Vector &vOther);
		//	operator Vector ();

		//	const float operator[]( int i ) const { return (((float16 *)this)[i]).GetFloat(); }

		//	float16 x;
		//	float16 y;
		//	float16 z;
		//};



		public SourceVector48bits()
		{
			theXInput = new SourceFloat16bits();
			theYInput = new SourceFloat16bits();
			theZInput = new SourceFloat16bits();
		}

		//Public theBytes(5) As Byte
		public SourceFloat16bits theXInput;
		public SourceFloat16bits theYInput;
		public SourceFloat16bits theZInput;

		public double x
		{
			get
			{
				return theXInput.TheFloatValue;
			}
		}

		public double y
		{
			get
			{
				return theYInput.TheFloatValue;
			}
		}

		public double z
		{
			get
			{
				return theZInput.TheFloatValue;
			}
		}
	}

}