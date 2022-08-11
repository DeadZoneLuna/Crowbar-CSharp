using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlType0Vertex2531
	{

		//struct mstudiovertex_t // NOTE: This is exactly 64 bytes, two cache lines
		//{
		//	mstudioboneweight_t	m_BoneWeights;
		//	Vector		m_vecPosition;
		//	Vector		m_vecNormal;
		//	Vector2D	m_vecTexCoord;
		//};
		//
		//struct mstudioboneweight_t
		//{
		//	float	weight[4];
		//	short	bone[4]; 
		//
		//	short	numbones;
		//	short	material;
		//
		//	short	firstref;
		//	short	lastref;
		//};

		// Example: "character\pc\female\brujah\armor0\brujah_female_armor_0.mdl" 
		//          seems to use struct size of 12 + 32 = 44 bytes.
		//NOTE: The weight and bone fields are smaller than above. VtMB must only accept max of 3 bone weights per vertex.
		//Public weight(1) As Double
		//Public boneIndex(1) As Short
		//Public weight As Double
		//Public boneIndex As Short
		// Weights total to 255.
		public byte[] weight = new byte[3];
		public byte unknown1;
		public short[] boneIndex = new short[3];
		public short unknown2;
		//Public boneCount As Byte
		//Public materialIndex As Short
		//Public firstRef As Short
		//Public lastRef As Short

		public SourceVector position = new SourceVector();
		public SourceVector normal = new SourceVector();
		public double texCoordU;
		public double texCoordV;
		//Public test(7) As Double

	}

}