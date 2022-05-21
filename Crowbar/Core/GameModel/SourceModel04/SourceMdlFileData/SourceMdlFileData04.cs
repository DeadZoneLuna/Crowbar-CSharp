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
	public class SourceMdlFileData04 : SourceMdlFileDataBase
	{
		public SourceMdlFileData04() : base()
		{

			theChecksumIsValid = false;
		}

		//Public id(3) As Char
		//Public version As Integer
		public int unknown01;

		public int boneCount;
		public int bodyPartCount;
		// modelCount? Why would this value be here if it's already in SourceMdlBodyPart04?
		public int unknownCount;
		public int sequenceDescCount;
		// Total frames from all sequences + an extra for each sequence.
		public int sequenceFrameCount;

		public int unknown02;

		public List<SourceMdlBodyPart04> theBodyParts;
		public List<SourceMdlBone04> theBones;
		public List<SourceMdlSequenceDesc04> theSequenceDescs;

	}

}