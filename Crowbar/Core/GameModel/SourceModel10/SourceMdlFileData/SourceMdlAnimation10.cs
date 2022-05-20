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
	public class SourceMdlAnimation10
	{

		public SourceMdlAnimation10() : base()
		{

			for (int offsetIndex = 0; offsetIndex < this.animationValueOffsets.Length; offsetIndex++)
			{
				this.theAnimationValues[offsetIndex] = new List<SourceMdlAnimationValue10>();
			}
		}

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//typedef struct
		//{
		//	unsigned short	offset[6];
		//} mstudioanim_t;

		public ushort[] animationValueOffsets = new ushort[6];



		public List<SourceMdlAnimationValue10>[] theAnimationValues = new List<SourceMdlAnimationValue10>[6];

	}

}