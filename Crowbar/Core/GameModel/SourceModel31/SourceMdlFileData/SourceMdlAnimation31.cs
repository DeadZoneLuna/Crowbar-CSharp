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
	public class SourceMdlAnimation31
	{

		//Public flags As Integer

		//Public animationValueOffsets(5) As Integer
		//Public unused As Integer
		//'---
		//Public position As SourceVector
		//Public rotationQuat As SourceQuaternion

		//Public theAnimationValues(5) As List(Of SourceMdlAnimationValue)

		public double unknown;
		public int[] theOffsets = new int[7];
		public List<SourceMdlAnimationValue2531> thePositionAnimationXValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> thePositionAnimationYValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> thePositionAnimationZValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> theRotationAnimationXValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> theRotationAnimationYValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> theRotationAnimationZValues = new List<SourceMdlAnimationValue2531>();
		public List<SourceMdlAnimationValue2531> theRotationAnimationWValues = new List<SourceMdlAnimationValue2531>();

		////=============================================================================
		//// Animation flag macros
		////=============================================================================
		//#define STUDIO_POS_ANIMATED		0x0001
		//#define STUDIO_ROT_ANIMATED		0x0002
		public const int STUDIO_POS_ANIMATED = 0x1;
		public const int STUDIO_ROT_ANIMATED = 0x2;

	}

}