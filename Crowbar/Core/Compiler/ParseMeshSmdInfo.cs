using System;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	public class ParseMeshSmdInfo
	{
		public List<string> messages;
		public int lineCount;
		public int boneCount;
		public List<string> boneNames;

		public ParseMeshSmdInfo()
		{
			messages = new List<string>();
			boneNames = new List<string>();
		}
	}
}