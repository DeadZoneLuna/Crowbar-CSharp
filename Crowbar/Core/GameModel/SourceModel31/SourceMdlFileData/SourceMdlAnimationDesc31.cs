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
	public class SourceMdlAnimationDesc31 : SourceMdlAnimationDescBase
	{
		public int nameOffset;
		public double fps;
		public int flags;
		public int frameCount;
		public int movementCount;
		public int movementOffset;

		public SourceVector bbMin = new SourceVector();
		public SourceVector bbMax = new SourceVector();

		public int animOffset;

		public int ikRuleCount;
		public int ikRuleOffset;

		//Public unused(7) As Integer

		public List<SourceMdlAnimation31> theAnimations;
		//Public theIkRules As List(Of SourceMdlIkRule37)
		public List<SourceMdlMovement> theMovements;
		//'Public theName As String

		//Public theAnimIsLinkedToSequence As Boolean = False
		//Public theLinkedSequences As New List(Of SourceMdlSequenceDesc37)()

	}

}