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
	public class SourceFloat16bits
	{

		//FROM: SourceEngine2006_source\public\compressed_vector.h

		////=========================================================
		//// 16 bit float
		////=========================================================


		//const int float32bias = 127;
		//const int float16bias = 15;

		//const float maxfloat16bits = 65504.0f;

		//Class float16
		//{
		//public:
		//	//float16() {}
		//	//float16( float f ) { m_storage.rawWord = ConvertFloatTo16bits(f); }

		//	void Init() { m_storage.rawWord = 0; }
		////	float16& operator=(const float16 &other) { m_storage.rawWord = other.m_storage.rawWord; return *this; }
		////	float16& operator=(const float &other) { m_storage.rawWord = ConvertFloatTo16bits(other); return *this; }
		////	operator unsigned short () { return m_storage.rawWord; }
		////	operator float () { return Convert16bitFloatTo32bits( m_storage.rawWord ); }
		//	unsigned short GetBits() const 
		//	{ 
		//		return m_storage.rawWord; 
		//	}
		//	float GetFloat() const 
		//	{ 
		//		return Convert16bitFloatTo32bits( m_storage.rawWord ); 
		//	}
		//	void SetFloat( float in ) 
		//	{ 
		//		m_storage.rawWord = ConvertFloatTo16bits( in ); 
		//	}

		//	bool IsInfinity() const
		//	{
		//		return m_storage.bits.biased_exponent == 31 && m_storage.bits.mantissa == 0;
		//	}
		//	bool IsNaN() const
		//	{
		//		return m_storage.bits.biased_exponent == 31 && m_storage.bits.mantissa != 0;
		//	}

		//	bool operator==(const float16 other) const { return m_storage.rawWord == other.m_storage.rawWord; }
		//	bool operator!=(const float16 other) const { return m_storage.rawWord != other.m_storage.rawWord; }

		////	bool operator< (const float other) const	   { return GetFloat() < other; }
		////	bool operator> (const float other) const	   { return GetFloat() > other; }

		//protected:
		//	union float32bits
		//	{
		//		float rawFloat;
		//		struct 
		//		{
		//			unsigned int mantissa : 23;
		//			unsigned int biased_exponent : 8;
		//			unsigned int sign : 1;
		//		} bits;
		//	};

		//	union float16bits
		//	{
		//		unsigned short rawWord;
		//		struct
		//		{
		//			unsigned short mantissa : 10;
		//			unsigned short biased_exponent : 5;
		//			unsigned short sign : 1;
		//		} bits;
		//	};

		//	static bool IsNaN( float16bits in )
		//	{
		//		return in.bits.biased_exponent == 31 && in.bits.mantissa != 0;
		//	}
		//	static bool IsInfinity( float16bits in )
		//	{
		//		return in.bits.biased_exponent == 31 && in.bits.mantissa == 0;
		//	}

		//	// 0x0001 - 0x03ff
		//	static unsigned short ConvertFloatTo16bits( float input )
		//	{
		//		if ( input > maxfloat16bits )
		//			input = maxfloat16bits;
		//		else if ( input < -maxfloat16bits )
		//			input = -maxfloat16bits;

		//		float16bits output;
		//		float32bits inFloat;

		//		inFloat.rawFloat = input;

		//		output.bits.sign = inFloat.bits.sign;

		//		if ( (inFloat.bits.biased_exponent==0) && (inFloat.bits.mantissa==0) ) 
		//		{ 
		//			// zero
		//			output.bits.mantissa = 0;
		//			output.bits.biased_exponent = 0;
		//		}
		//		else if ( (inFloat.bits.biased_exponent==0) && (inFloat.bits.mantissa!=0) ) 
		//		{  
		//			// denorm -- denorm float maps to 0 half
		//			output.bits.mantissa = 0;
		//			output.bits.biased_exponent = 0;
		//		}
		//		else if ( (inFloat.bits.biased_exponent==0xff) && (inFloat.bits.mantissa==0) ) 
		//		{ 
		//#If 0 Then
		//			// infinity
		//			output.bits.mantissa = 0;
		//			output.bits.biased_exponent = 31;
		//#Else
		//			// infinity maps to maxfloat
		//			output.bits.mantissa = 0x3ff;
		//			output.bits.biased_exponent = 0x1e;
		//#End If
		//		}
		//		else if ( (inFloat.bits.biased_exponent==0xff) && (inFloat.bits.mantissa!=0) ) 
		//		{ 
		//#If 0 Then
		//			// NaN
		//			output.bits.mantissa = 1;
		//			output.bits.biased_exponent = 31;
		//#Else
		//			// NaN maps to zero
		//			output.bits.mantissa = 0;
		//			output.bits.biased_exponent = 0;
		//#End If
		//		}
		//		else 
		//		{ 
		//			// regular number
		//			int new_exp = inFloat.bits.biased_exponent-127;

		//			if (new_exp<-24) 
		//			{ 
		//				// this maps to 0
		//				output.bits.mantissa = 0;
		//				output.bits.biased_exponent = 0;
		//			}

		//			if (new_exp<-14) 
		//			{
		//				// this maps to a denorm
		//				output.bits.biased_exponent = 0;
		//				unsigned int exp_val = ( unsigned int )( -14 - ( inFloat.bits.biased_exponent - float32bias ) );
		//				if( exp_val > 0 && exp_val < 11 )
		//				{
		//					output.bits.mantissa = ( 1 << ( 10 - exp_val ) ) + ( inFloat.bits.mantissa >> ( 13 + exp_val ) );
		//				}
		//			}
		//			else if (new_exp>15) 
		//			{ 
		//#If 0 Then
		//				// map this value to infinity
		//				output.bits.mantissa = 0;
		//				output.bits.biased_exponent = 31;
		//#Else
		//				// to big. . . maps to maxfloat
		//				output.bits.mantissa = 0x3ff;
		//				output.bits.biased_exponent = 0x1e;
		//#End If
		//			}
		//			else 
		//			{
		//				output.bits.biased_exponent = new_exp+15;
		//				output.bits.mantissa = (inFloat.bits.mantissa >> 13);
		//			}
		//		}
		//		return output.rawWord;
		//	}

		//	static float Convert16bitFloatTo32bits( unsigned short input )
		//	{
		//		float32bits output;
		//		float16bits inFloat;
		//		inFloat.rawWord = input;

		//		if( IsInfinity( inFloat ) )
		//		{
		//			return maxfloat16bits * ( ( inFloat.bits.sign == 1 ) ? -1.0f : 1.0f );
		//		}
		//		if( IsNaN( inFloat ) )
		//		{
		//			return 0.0;
		//		}
		//		if( inFloat.bits.biased_exponent == 0 && inFloat.bits.mantissa != 0 )
		//		{
		//			// denorm
		//			const float half_denorm = (1.0f/16384.0f); // 2^-14
		//			float mantissa = ((float)(inFloat.bits.mantissa)) / 1024.0f;
		//			float sgn = (inFloat.bits.sign)? -1.0f :1.0f;
		//			output.rawFloat = sgn*mantissa*half_denorm;
		//		}
		//		else
		//		{
		//			// regular number
		//			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
		//			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
		//			output.bits.sign = inFloat.bits.sign;
		//		}

		//		return output.rawFloat;
		//	}


		//	float16bits m_storage;
		//};

		public double TheFloatValue
		{
			get
			{
				//		float32bits output;
				//Dim result As Single
				double result = 0;
				//		float16bits inFloat;
				//		inFloat.rawWord = input;

				int sign = 0;
				int floatSign = 0;
				sign = this.GetSign(this.the16BitValue);
				if (sign == 1)
				{
					floatSign = -1;
				}
				else
				{
					floatSign = 1;
				}

				//		if( IsInfinity( inFloat ) )
				//		{
				//			return maxfloat16bits * ( ( inFloat.bits.sign == 1 ) ? -1.0f : 1.0f );
				//		}
				if (this.IsInfinity(this.the16BitValue))
				{
					return maxfloat16bits * floatSign;
				}

				//		if( IsNaN( inFloat ) )
				//		{
				//			return 0.0;
				//		}
				if (this.IsNaN(this.the16BitValue))
				{
					return 0;
				}

				//		if( inFloat.bits.biased_exponent == 0 && inFloat.bits.mantissa != 0 )
				//		{
				//			// denorm
				//			const float half_denorm = (1.0f/16384.0f); // 2^-14
				//			float mantissa = ((float)(inFloat.bits.mantissa)) / 1024.0f;
				//			float sgn = (inFloat.bits.sign)? -1.0f :1.0f;
				//			output.rawFloat = sgn*mantissa*half_denorm;
				//		}
				//		else
				//		{
				//			// regular number
				//			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
				//			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
				//			output.bits.sign = inFloat.bits.sign;
				//		}
				int mantissa = 0;
				int biased_exponent = 0;
				//Dim anInteger32 As Integer
				//Dim anInteger32Bytes() As Byte
				mantissa = this.GetMantissa(this.the16BitValue);
				biased_exponent = this.GetBiasedExponent(this.the16BitValue);
				if (biased_exponent == 0 && mantissa != 0)
				{
					float floatMantissa = mantissa / 1024.0F;
					result = floatSign * floatMantissa * half_denorm;
				}
				else
				{
					//anInteger32 = Me.Get32BitFloat(Me.the16BitValue)
					//anInteger32Bytes = BitConverter.GetBytes(anInteger32)
					//'Array.Reverse(anInteger32Bytes)
					//result = BitConverter.ToSingle(anInteger32Bytes, 0)
					//------
					result = this.GetSingle(this.the16BitValue);

					// For debugging the conversion.
					//result = CType(anInteger32, Single)
				}

				//		return output.rawFloat;
				return result;
			}
		}

		//			unsigned short mantissa : 10;
		//			unsigned short biased_exponent : 5;
		//			unsigned short sign : 1;

		private int GetMantissa(ushort value)
		{
			return (value & 0x3FF);
		}

		private int GetBiasedExponent(ushort value)
		{
			return (value & 0x7C00) >> 10;
		}

		private int GetSign(ushort value)
		{
			return (value & 0x8000) >> 15;
		}

		//Private Function Get32BitFloat(ByVal value As UShort) As Integer
		//	'FROM:
		//	'			unsigned short mantissa : 10;
		//	'			unsigned short biased_exponent : 5;
		//	'			unsigned short sign : 1;
		//	'TO:
		//	'			unsigned int mantissa : 23;
		//	'			unsigned int biased_exponent : 8;
		//	'			unsigned int sign : 1;
		//	Dim bitsResult As IntegerAndSingleUnion
		//	Dim mantissa As Integer
		//	Dim biased_exponent As Integer
		//	Dim sign As Integer
		//	Dim resultMantissa As Integer
		//	Dim resultBiasedExponent As Integer
		//	Dim resultSign As Integer
		//	bitsResult = 0

		//	mantissa = Me.GetMantissa(Me.the16BitValue)
		//	biased_exponent = Me.GetBiasedExponent(Me.the16BitValue)
		//	sign = Me.GetSign(Me.the16BitValue)

		//	'			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
		//	'			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
		//	'			output.bits.sign = inFloat.bits.sign;
		//	resultMantissa = mantissa
		//	If biased_exponent = 0 Then
		//		resultBiasedExponent = 0
		//	Else
		//		resultBiasedExponent = (biased_exponent - float16bias + float32bias) << 23
		//	End If
		//	resultSign = sign << 31

		//	' For debugging.
		//	'------
		//	' TEST PASSED:
		//	'If (resultMantissa Or &H7FFFFF) <> &H7FFFFF Then
		//	'	Dim i As Integer = 42
		//	'End If
		//	'------
		//	' TEST PASSED:
		//	'If (resultBiasedExponent Or &H7F800000) <> &H7F800000 Then
		//	'	Dim i As Integer = 42
		//	'End If
		//	'------
		//	' TEST PASSED:
		//	'If resultSign <> &H80000000 AndAlso resultSign <> 0 Then
		//	'	Dim i As Integer = 42
		//	'End If

		//	bitsResult = resultSign Or resultBiasedExponent Or resultMantissa

		//	Return bitsResult
		//End Function

		private float GetSingle(ushort value)
		{
			//FROM:
			//			unsigned short mantissa : 10;
			//			unsigned short biased_exponent : 5;
			//			unsigned short sign : 1;
			//TO:
			//			unsigned int mantissa : 23;
			//			unsigned int biased_exponent : 8;
			//			unsigned int sign : 1;
			IntegerAndSingleUnion bitsResult = new IntegerAndSingleUnion();
			int mantissa = 0;
			int biased_exponent = 0;
			int sign = 0;
			int resultMantissa = 0;
			int resultBiasedExponent = 0;
			int resultSign = 0;
			bitsResult.i = 0;

			mantissa = this.GetMantissa(this.the16BitValue);
			biased_exponent = this.GetBiasedExponent(this.the16BitValue);
			sign = this.GetSign(this.the16BitValue);

			//			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
			//			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
			//			output.bits.sign = inFloat.bits.sign;
			resultMantissa = mantissa << (23 - 10);
			if (biased_exponent == 0)
			{
				resultBiasedExponent = 0;
			}
			else
			{
				resultBiasedExponent = (biased_exponent - float16bias + float32bias) << 23;
			}
			resultSign = sign << 31;

			// For debugging.
			//------
			// TEST PASSED:
			//If (resultMantissa Or &H7FFFFF) <> &H7FFFFF Then
			//	Dim i As Integer = 42
			//End If
			//------
			// TEST PASSED:
			//If (resultBiasedExponent Or &H7F800000) <> &H7F800000 Then
			//	Dim i As Integer = 42
			//End If
			//------
			// TEST PASSED:
			//If resultSign <> &H80000000 AndAlso resultSign <> 0 Then
			//	Dim i As Integer = 42
			//End If

			bitsResult.i = resultSign | resultBiasedExponent | resultMantissa;

			return bitsResult.s;
		}

		//Private Function GetDouble(ByVal value As UShort) As Double
		//	'FROM:
		//	'			unsigned short mantissa : 10;
		//	'			unsigned short biased_exponent : 5;
		//	'			unsigned short sign : 1;
		//	'TO:
		//	'			unsigned int mantissa : 23;
		//	'			unsigned int biased_exponent : 8;
		//	'			unsigned int sign : 1;
		//	Dim bitsResult As Integer
		//	Dim mantissa As Integer
		//	Dim biased_exponent As Integer
		//	Dim sign As Integer
		//	Dim resultMantissa As Integer
		//	Dim resultBiasedExponent As Integer
		//	Dim resultSign As Integer
		//	bitsResult = 0

		//	mantissa = Me.GetMantissa(Me.the16BitValue)
		//	biased_exponent = Me.GetBiasedExponent(Me.the16BitValue)
		//	sign = Me.GetSign(Me.the16BitValue)

		//	'			output.bits.mantissa = inFloat.bits.mantissa << (23-10);
		//	'			output.bits.biased_exponent = (inFloat.bits.biased_exponent - float16bias + float32bias) * (inFloat.bits.biased_exponent != 0);
		//	'			output.bits.sign = inFloat.bits.sign;
		//	resultMantissa = mantissa << (23 - 10)
		//	If biased_exponent = 0 Then
		//		resultBiasedExponent = 0
		//	Else
		//		resultBiasedExponent = (biased_exponent - float16bias + float32bias) << 23
		//	End If
		//	resultSign = sign << 31

		//	'bitsResult = resultSign Or resultBiasedExponent Or resultMantissa

		//	'Return bitsResult
		//	'Return resultSign * Math.Pow(2, resultBiasedExponent - 127) * resultMantissa
		//	Return sign * Math.Pow(2, biased_exponent - 1023) * mantissa
		//End Function

		//	static bool IsInfinity( float16bits in )
		//	{
		//		return in.bits.biased_exponent == 31 && in.bits.mantissa == 0;
		//	}
		private bool IsInfinity(ushort value)
		{
			int mantissa = 0;
			int biased_exponent = 0;

			mantissa = this.GetMantissa(value);
			biased_exponent = this.GetBiasedExponent(value);
			return ((biased_exponent == 31) && (mantissa == 0));
		}

		//	static bool IsNaN( float16bits in )
		//	{
		//		return in.bits.biased_exponent == 31 && in.bits.mantissa != 0;
		//	}
		private bool IsNaN(ushort value)
		{
			int mantissa = 0;
			int biased_exponent = 0;

			mantissa = this.GetMantissa(value);
			biased_exponent = this.GetBiasedExponent(value);
			return ((biased_exponent == 31) && (mantissa != 0));
		}

		//const int float32bias = 127;
		//const int float16bias = 15;
		//const float maxfloat16bits = 65504.0f;
		//const float half_denorm = (1.0f/16384.0f); // 2^-14
		private const int float32bias = 127;
		private const int float16bias = 15;
		private const float maxfloat16bits = 65504.0F;
		private const float half_denorm = (1.0F / 16384.0F);

		public ushort the16BitValue;

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