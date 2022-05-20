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
	public class SourceVertex
	{

		//// NOTE: This is exactly 48 bytes
		//struct mstudiovertex_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	mstudioboneweight_t	m_BoneWeights;
		//	Vector				m_vecPosition;
		//	Vector				m_vecNormal;
		//	Vector2D			m_vecTexCoord;

		//	mstudiovertex_t() {}

		//private:
		//	// No copy constructors allowed
		//	mstudiovertex_t(const mstudiovertex_t& vOther);
		//};

		//	mstudioboneweight_t	m_BoneWeights;
		public SourceBoneWeight boneWeight;

		//NOTE: Changed to Double, so that the values will be properly written to file with 6 decimal digits.
		//'	Vector				m_vecPosition;
		//Public positionX As Single
		//Public positionY As Single
		//Public positionZ As Single
		//'	Vector				m_vecNormal;
		//Public normalX As Single
		//Public normalY As Single
		//Public normalZ As Single
		//'	Vector2D			m_vecTexCoord;
		//Public texCoordX As Single
		//Public texCoordY As Single
		//	Vector				m_vecPosition;
		public double positionX;
		public double positionY;
		public double positionZ;
		//	Vector				m_vecNormal;
		public double normalX;
		public double normalY;
		public double normalZ;
		//	Vector2D			m_vecTexCoord;
		public double texCoordX;
		public double texCoordY;

	}

}