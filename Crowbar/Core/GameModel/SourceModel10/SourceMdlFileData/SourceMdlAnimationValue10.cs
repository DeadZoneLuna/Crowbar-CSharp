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
	[StructLayout(LayoutKind.Explicit)]
	public class SourceMdlAnimationValue10
	{

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// animation frames
		//typedef union 
		//{
		//	struct {
		//		byte	valid;
		//		byte	total;
		//	} num;
		//	short		value;
		//} mstudioanimvalue_t;


		[FieldOffset(0)]
		public byte valid;
		[FieldOffset(1)]
		public byte total;

		[FieldOffset(0)]
		public short value;

	}

}