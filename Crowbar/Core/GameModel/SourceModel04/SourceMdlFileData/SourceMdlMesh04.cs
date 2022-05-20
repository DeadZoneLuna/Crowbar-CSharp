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
	public class SourceMdlMesh04
	{

		public char[] name = new char[32];
		public int faceCount;
		public int unknownCount;
		public uint textureWidth;
		public uint textureHeight;

		public string theName;
		public List<SourceMdlFace04> theFaces;
		public List<byte> theTextureBmpData;
		public string theTextureFileName;

	}

}