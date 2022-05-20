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
	public class SourceMdlHitbox32
	{

		//struct mstudiobbox_t
		//{
		//	int	bone;
		//	int	group;			// intersection group
		//	Vector	bbmin;			// bounding box
		//	Vector	bbmax;	
		//	int	szhitboxnameindex;	// offset to the name of the hitbox.
		//	char	padding[32];		// future expansion.
		//
		//	char* pszHitboxName()
		//	{
		//		if( szhitboxnameindex == 0 )
		//			return "";
		//
		//		return ((char*)this) + szhitboxnameindex;
		//	}
		//};

		public int boneIndex;
		public int groupIndex;
		public SourceVector boundingBoxMin = new SourceVector();
		public SourceVector boundingBoxMax = new SourceVector();
		//Public nameOffset As Integer
		//Public unused(31) As Byte

		public string theName;

	}

}