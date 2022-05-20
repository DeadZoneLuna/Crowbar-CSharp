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
	public class SourceMdlVertex37
	{

		//FROM: The Axel Project - source [MDL v37]\TAPSRC\src\Public\studio.h
		//// NOTE: This is exactly 64 bytes, two cache lines
		//struct mstudiovertex_t
		//{
		//	mstudioboneweight_t	m_BoneWeights;
		//	Vector			m_vecPosition;
		//	Vector			m_vecNormal;
		//	Vector2D		m_vecTexCoord;
		//};

		public SourceMdlVertex37()
		{
			this.boneWeight = new SourceMdlBoneWeight37();
			this.position = new SourceVector();
			this.normal = new SourceVector();
		}

		public SourceMdlBoneWeight37 boneWeight;
		public SourceVector position;
		public SourceVector normal;
		public double texCoordX;
		public double texCoordY;

	}

}