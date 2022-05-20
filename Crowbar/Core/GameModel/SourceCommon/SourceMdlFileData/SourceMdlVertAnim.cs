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
	public class SourceMdlVertAnim
	{

		//FROM: SourceEngineXXXX_source\public\studio.h
		//// this is the memory image of vertex anims (16-bit fixed point)
		//struct mstudiovertanim_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	unsigned short		index;
		//	byte				speed;	// 255/max_length_in_flex
		//	byte				side;	// 255/left_right
		//
		//protected:
		//	// JasonM changing this type a lot, to prefer fixed point 16 bit...
		//	union
		//	{
		//		short			delta[3];
		//		float16			flDelta[3];
		//	};
		//
		//	union
		//	{
		//		short			ndelta[3];
		//		float16			flNDelta[3];
		//	};
		//
		//public:
		//	inline Vector GetDeltaFixed()
		//	{
		//		return Vector( delta[0]*g_VertAnimFixedPointScale, delta[1]*g_VertAnimFixedPointScale, delta[2]*g_VertAnimFixedPointScale );
		//	}
		//	inline Vector GetNDeltaFixed()
		//	{
		//		return Vector( ndelta[0]*g_VertAnimFixedPointScale, ndelta[1]*g_VertAnimFixedPointScale, ndelta[2]*g_VertAnimFixedPointScale );
		//	}
		//	inline void GetDeltaFixed4DAligned( Vector4DAligned *vFillIn )
		//	{
		//		vFillIn->Set( delta[0]*g_VertAnimFixedPointScale, delta[1]*g_VertAnimFixedPointScale, delta[2]*g_VertAnimFixedPointScale, 0.0f );
		//	}
		//	inline void GetNDeltaFixed4DAligned( Vector4DAligned *vFillIn )
		//	{
		//		vFillIn->Set( ndelta[0]*g_VertAnimFixedPointScale, ndelta[1]*g_VertAnimFixedPointScale, ndelta[2]*g_VertAnimFixedPointScale, 0.0f );
		//	}
		//	inline Vector GetDeltaFloat()
		//	{
		//		return Vector (flDelta[0].GetFloat(), flDelta[1].GetFloat(), flDelta[2].GetFloat());
		//	}
		//	inline Vector GetNDeltaFloat()
		//	{
		//		return Vector (flNDelta[0].GetFloat(), flNDelta[1].GetFloat(), flNDelta[2].GetFloat());
		//	}
		//	inline void SetDeltaFixed( const Vector& vInput )
		//	{
		//		delta[0] = vInput.x * g_VertAnimFixedPointScaleInv;
		//		delta[1] = vInput.y * g_VertAnimFixedPointScaleInv;
		//		delta[2] = vInput.z * g_VertAnimFixedPointScaleInv;
		//	}
		//	inline void SetNDeltaFixed( const Vector& vInputNormal )
		//	{
		//		ndelta[0] = vInputNormal.x * g_VertAnimFixedPointScaleInv;
		//		ndelta[1] = vInputNormal.y * g_VertAnimFixedPointScaleInv;
		//		ndelta[2] = vInputNormal.z * g_VertAnimFixedPointScaleInv;
		//	}
		//
		//	// Ick...can also force fp16 data into this structure for writing to file in legacy format...
		//	inline void SetDeltaFloat( const Vector& vInput )
		//	{
		//		flDelta[0].SetFloat( vInput.x );
		//		flDelta[1].SetFloat( vInput.y );
		//		flDelta[2].SetFloat( vInput.z );
		//	}
		//	inline void SetNDeltaFloat( const Vector& vInputNormal )
		//	{
		//		flNDelta[0].SetFloat( vInputNormal.x );
		//		flNDelta[1].SetFloat( vInputNormal.y );
		//		flNDelta[2].SetFloat( vInputNormal.z );
		//	}
		//
		//	mstudiovertanim_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudiovertanim_t(const mstudiovertanim_t& vOther);
		//};

		//const float g_VertAnimFixedPointScale = 1.0f / 4096.0f;
		//const float g_VertAnimFixedPointScaleInv = 1.0f / g_VertAnimFixedPointScale;



		public ushort index;
		public byte speed;
		public byte side;

		//Public Property delta(ByVal index As Integer) As Short
		//	Get
		//		Return CShort(Me.theDelta(index).the16BitValue)
		//	End Get
		//	Set(ByVal value As Short)
		//		Me.theDelta(index).the16BitValue = CUShort(value)
		//	End Set
		//End Property

//INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
//ORIGINAL LINE: Public Property deltaUShort(ByVal index As Integer) As UShort
		public ushort get_deltaUShort(int index)
		{
			return this.theDelta[index].the16BitValue;
		}
			public void set_deltaUShort(int index, ushort value)
			{
				this.theDelta[index].the16BitValue = value;
			}

//INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
//ORIGINAL LINE: Public Property flDelta(ByVal index As Integer) As SourceFloat16bits
		public SourceFloat16bits get_flDelta(int index)
		{
			return this.theDelta[index];
		}
			public void set_flDelta(int index, SourceFloat16bits value)
			{
				this.theDelta[index] = value;
			}

		//Public Property nDelta(ByVal index As Integer) As Short
		//	Get
		//		Return CShort(Me.theNDelta(index).the16BitValue)
		//	End Get
		//	Set(ByVal value As Short)
		//		Me.theNDelta(index).the16BitValue = CUShort(value)
		//	End Set
		//End Property

//INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
//ORIGINAL LINE: Public Property nDeltaUShort(ByVal index As Integer) As UShort
		public ushort get_nDeltaUShort(int index)
		{
			return this.theNDelta[index].the16BitValue;
		}
			public void set_nDeltaUShort(int index, ushort value)
			{
				this.theNDelta[index].the16BitValue = value;
			}

//INSTANT C# NOTE: C# does not support parameterized properties - the following property has been divided into two methods:
//ORIGINAL LINE: Public Property flNDelta(ByVal index As Integer) As SourceFloat16bits
		public SourceFloat16bits get_flNDelta(int index)
		{
			return this.theNDelta[index];
		}
			public void set_flNDelta(int index, SourceFloat16bits value)
			{
				this.theNDelta[index] = value;
			}


		private SourceFloat16bits[] theDelta = new SourceFloat16bits[3];
		private SourceFloat16bits[] theNDelta = new SourceFloat16bits[3];

		public SourceMdlVertAnim()
		{
			for (int x = 0; x <= 2; x++)
			{
				this.theDelta[x] = new SourceFloat16bits();
			}
			for (int x = 0; x <= 2; x++)
			{
				this.theNDelta[x] = new SourceFloat16bits();
			}
		}

	}

}