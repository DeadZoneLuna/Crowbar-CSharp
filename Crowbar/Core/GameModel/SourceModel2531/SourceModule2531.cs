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
	internal static class SourceModule2531
	{

		//Public Function GetBodyGroupSmdFileName(ByVal bodyPartIndex As Integer, ByVal modelIndex As Integer, ByVal lodIndex As Integer, ByVal theModelCommandIsUsed As Boolean, ByVal modelName As String, ByVal bodyModelName As String, ByVal bodyPartCount As Integer, ByVal bodyModelCount As Integer, ByVal sequenceGroupFileName As String) As String
		//	Dim bodyGroupSmdFileName As String

		//	If bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0 AndAlso Not String.IsNullOrEmpty(sequenceGroupFileName) AndAlso Not FileManager.FilePathHasInvalidChars(sequenceGroupFileName) Then
		//		bodyGroupSmdFileName = Path.GetFileName(sequenceGroupFileName.Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
		//		If Not bodyGroupSmdFileName.StartsWith(modelName) Then
		//			bodyGroupSmdFileName = modelName + "_" + bodyGroupSmdFileName
		//		End If
		//	Else
		//		bodyGroupSmdFileName = SourceFileNamesModule.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex, theModelCommandIsUsed, modelName, bodyModelName, bodyPartCount, bodyModelCount)
		//	End If

		//	Return bodyGroupSmdFileName
		//End Function

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

		//	Public activityMap As String() = {"ACT_RESET", _
		//"ACT_IDLE", _
		//"ACT_GUARD", _
		//"ACT_WALK", _
		//"ACT_RUN", _
		//"ACT_FLY", _
		//"ACT_SWIM", _
		//"ACT_HOP", _
		//"ACT_LEAP", _
		//"ACT_FALL", _
		//"ACT_LAND", _
		//"ACT_STRAFE_LEFT", _
		//"ACT_STRAFE_RIGHT", _
		//"ACT_ROLL_LEFT", _
		//"ACT_ROLL_RIGHT", _
		//"ACT_TURN_LEFT", _
		//"ACT_TURN_RIGHT", _
		//"ACT_CROUCH", _
		//"ACT_CROUCHIDLE", _
		//"ACT_STAND", _
		//"ACT_USE", _
		//"ACT_SIGNAL1", _
		//"ACT_SIGNAL2", _
		//"ACT_SIGNAL3", _
		//"ACT_TWITCH", _
		//"ACT_COWER", _
		//"ACT_SMALL_FLINCH", _
		//"ACT_BIG_FLINCH", _
		//"ACT_RANGE_ATTACK1", _
		//"ACT_RANGE_ATTACK2", _
		//"ACT_MELEE_ATTACK1", _
		//"ACT_MELEE_ATTACK2", _
		//"ACT_RELOAD", _
		//"ACT_ARM", _
		//"ACT_DISARM", _
		//"ACT_EAT", _
		//"ACT_DIESIMPLE", _
		//"ACT_DIEBACKWARD", _
		//"ACT_DIEFORWARD", _
		//"ACT_DIEVIOLENT", _
		//"ACT_BARNACLE_HIT", _
		//"ACT_BARNACLE_PULL", _
		//"ACT_BARNACLE_CHOMP", _
		//"ACT_BARNACLE_CHEW", _
		//"ACT_SLEEP", _
		//"ACT_INSPECT_FLOOR", _
		//"ACT_INSPECT_WALL", _
		//"ACT_IDLE_ANGRY", _
		//"ACT_WALK_HURT", _
		//"ACT_RUN_HURT", _
		//"ACT_HOVER", _
		//"ACT_GLIDE", _
		//"ACT_FLY_LEFT", _
		//"ACT_FLY_RIGHT", _
		//"ACT_DETECT_SCENT", _
		//"ACT_SNIFF", _
		//"ACT_BITE", _
		//"ACT_THREAT_DISPLAY", _
		//"ACT_FEAR_DISPLAY", _
		//"ACT_EXCITED", _
		//"ACT_SPECIAL_ATTACK1", _
		//"ACT_SPECIAL_ATTACK2", _
		//"ACT_COMBAT_IDLE", _
		//"ACT_WALK_SCARED", _
		//"ACT_RUN_SCARED", _
		//"ACT_VICTORY_DANCE", _
		//"ACT_DIE_HEADSHOT", _
		//"ACT_DIE_CHESTSHOT", _
		//"ACT_DIE_GUTSHOT", _
		//"ACT_DIE_BACKSHOT", _
		//"ACT_FLINCH_HEAD", _
		//"ACT_FLINCH_CHEST", _
		//"ACT_FLINCH_STOMACH", _
		//"ACT_FLINCH_LEFTARM", _
		//"ACT_FLINCH_RIGHTARM", _
		//"ACT_FLINCH_LEFTLEG", _
		//"ACT_FLINCH_RIGHTLEG"
		//}

		//#define MAXSTUDIOBLENDS		16		// f64: ~New
		public static int MAXSTUDIOBLENDS = 16;


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