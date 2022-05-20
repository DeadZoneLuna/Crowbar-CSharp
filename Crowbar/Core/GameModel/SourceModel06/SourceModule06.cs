//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Crowbar
{
	internal static class SourceModule06
	{

		//Public Function GetAnimationSmdRelativePathFileName(ByVal modelName As String, ByVal iAnimationName As String, ByVal blendIndex As Integer) As String
		//	Dim animationName As String
		//	Dim animationSmdRelativePathFileName As String

		//	'NOTE: The animation or blend name is not stored, so use sequence name or variation of it.
		//	If blendIndex = -1 Then
		//		animationName = iAnimationName
		//	Else
		//		animationName = iAnimationName + "_blend" + (blendIndex + 1).ToString("00")
		//	End If

		//	If Not TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked Then
		//		animationName = modelName + "_anim_" + animationName
		//	End If
		//	animationSmdRelativePathFileName = Path.Combine(GetAnimationSmdRelativePath(modelName), animationName)

		//	If Path.GetExtension(animationSmdRelativePathFileName) <> ".smd" Then
		//		'NOTE: Add the ".smd" extension, keeping the existing extension in file name, which is often ".dmx" for newer models. 
		//		'      Thus, user can see that model might have newer features that Crowbar does not yet handle.
		//		animationSmdRelativePathFileName += ".smd"
		//	End If

		//	Return animationSmdRelativePathFileName
		//End Function

		// For the SourceMdlBoneController10.type and SourceMdlSequenceDesc10.activityId.blendType fields.
		//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
		//int lookupControl( char *string )
		//{
		//	if (stricmp(string,"X")==0) return STUDIO_X;
		//	if (stricmp(string,"Y")==0) return STUDIO_Y;
		//	if (stricmp(string,"Z")==0) return STUDIO_Z;
		//	if (stricmp(string,"XR")==0) return STUDIO_XR;
		//	if (stricmp(string,"YR")==0) return STUDIO_YR;
		//	if (stricmp(string,"ZR")==0) return STUDIO_ZR;
		//	if (stricmp(string,"LX")==0) return STUDIO_LX;
		//	if (stricmp(string,"LY")==0) return STUDIO_LY;
		//	if (stricmp(string,"LZ")==0) return STUDIO_LZ;
		//	if (stricmp(string,"AX")==0) return STUDIO_AX;
		//	if (stricmp(string,"AY")==0) return STUDIO_AY;
		//	if (stricmp(string,"AZ")==0) return STUDIO_AZ;
		//	if (stricmp(string,"AXR")==0) return STUDIO_AXR;
		//	if (stricmp(string,"AYR")==0) return STUDIO_AYR;
		//	if (stricmp(string,"AZR")==0) return STUDIO_AZR;
		//	return -1;
		//}
		public static string GetControlText(int type)
		{
			if (type == STUDIO_X)
			{
				return "X";
			}
			else if (type == STUDIO_Y)
			{
				return "Y";
			}
			else if (type == STUDIO_Z)
			{
				return "Z";
			}
			else if (type == STUDIO_XR)
			{
				return "XR";
			}
			else if (type == STUDIO_YR)
			{
				return "YR";
			}
			else if (type == STUDIO_ZR)
			{
				return "ZR";
			}
			else if (type == STUDIO_LX)
			{
				return "LX";
			}
			else if (type == STUDIO_LY)
			{
				return "LY";
			}
			else if (type == STUDIO_LZ)
			{
				return "LZ";
			}
			else if (type == STUDIO_AX)
			{
				return "AX";
			}
			else if (type == STUDIO_AY)
			{
				return "AY";
			}
			else if (type == STUDIO_AZ)
			{
				return "AZ";
			}
			else if (type == STUDIO_AXR)
			{
				return "AXR";
			}
			else if (type == STUDIO_AYR)
			{
				return "AYR";
			}
			else if (type == STUDIO_AZR)
			{
				return "AZR";
				//			if (bonecontroller[numbonecontrollers].type & (STUDIO_XR | STUDIO_YR | STUDIO_ZR))
				//			{
				//				if (((int)(bonecontroller[numbonecontrollers].start + 360) % 360) == ((int)(bonecontroller[numbonecontrollers].end + 360) % 360))
				//				{
				//					bonecontroller[numbonecontrollers].type |= STUDIO_RLOOP;
				//				}
				//			}
			}
			else if (type == (STUDIO_XR | STUDIO_RLOOP))
			{
				return "XR";
			}
			else if (type == (STUDIO_YR | STUDIO_RLOOP))
			{
				return "YR";
			}
			else if (type == (STUDIO_ZR | STUDIO_RLOOP))
			{
				return "ZR";
			}

			return "";
		}

		public static string GetMultipleControlText(int type)
		{
			string result = "";

			if ((type & STUDIO_X) == STUDIO_X)
			{
				result += " X";
			}
			if ((type & STUDIO_Y) == STUDIO_Y)
			{
				result += " Y";
			}
			if ((type & STUDIO_Z) == STUDIO_Z)
			{
				result += " Z";
			}
			if ((type & STUDIO_XR) == STUDIO_XR)
			{
				result += " XR";
			}
			if ((type & STUDIO_YR) == STUDIO_YR)
			{
				result += " YR";
			}
			if ((type & STUDIO_ZR) == STUDIO_ZR)
			{
				result += " ZR";
			}
			if ((type & STUDIO_LX) == STUDIO_LX)
			{
				result += " LX";
			}
			if ((type & STUDIO_LY) == STUDIO_LY)
			{
				result += " LY";
			}
			if ((type & STUDIO_LZ) == STUDIO_LZ)
			{
				result += " LZ";
			}
			if ((type & STUDIO_AX) == STUDIO_AX)
			{
				result += " AX";
			}
			if ((type & STUDIO_AY) == STUDIO_AY)
			{
				result += " AY";
			}
			if ((type & STUDIO_AZ) == STUDIO_AZ)
			{
				result += " AZ";
			}
			if ((type & STUDIO_AXR) == STUDIO_AXR)
			{
				result += " AXR";
			}
			if ((type & STUDIO_AYR) == STUDIO_AYR)
			{
				result += " AYR";
			}
			if ((type & STUDIO_AZR) == STUDIO_AZR)
			{
				result += " AZR";
			}

			return result.Trim();
		}

		//FROM: [1999] HLStandardSDK\SourceCode\engine\studio.h
		//// motion flags
		//#define STUDIO_X		0x0001
		//#define STUDIO_Y		0x0002	
		//#define STUDIO_Z		0x0004
		//#define STUDIO_XR		0x0008
		//#define STUDIO_YR		0x0010
		//#define STUDIO_ZR		0x0020
		//#define STUDIO_LX		0x0040
		//#define STUDIO_LY		0x0080
		//#define STUDIO_LZ		0x0100
		//#define STUDIO_AX		0x0200
		//#define STUDIO_AY		0x0400
		//#define STUDIO_AZ		0x0800
		//#define STUDIO_AXR		0x1000
		//#define STUDIO_AYR		0x2000
		//#define STUDIO_AZR		0x4000
		//#define STUDIO_TYPES	0x7FFF
		//#define STUDIO_RLOOP	0x8000	// controller that wraps shortest distance

		public static int STUDIO_X = 0x1;
		public static int STUDIO_Y = 0x2;
		public static int STUDIO_Z = 0x4;
		public static int STUDIO_XR = 0x8;
		public static int STUDIO_YR = 0x10;
		public static int STUDIO_ZR = 0x20;

		public static int STUDIO_LX = 0x40;
		public static int STUDIO_LY = 0x80;
		public static int STUDIO_LZ = 0x100;

		public static int STUDIO_AX = 0x200;
		public static int STUDIO_AY = 0x400;
		public static int STUDIO_AZ = 0x800;
		public static int STUDIO_AXR = 0x1000;
		public static int STUDIO_AYR = 0x2000;
		public static int STUDIO_AZR = 0x4000;

		public static int STUDIO_RLOOP = 0x8000;

	}

}