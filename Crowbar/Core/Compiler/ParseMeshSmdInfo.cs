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
	public class ParseMeshSmdInfo
	{

		public ParseMeshSmdInfo()
		{
			messages = new List<string>();
			boneNames = new List<string>();
		}

		public List<string> messages;
		public int lineCount;
		public int boneCount;
		public List<string> boneNames;

	}

}