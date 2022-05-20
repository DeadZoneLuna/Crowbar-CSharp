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
	internal static class SourceModule10
	{

		//Public Function GetAnimationSmdRelativePathFileName(ByVal modelName As String, ByVal iAnimationName As String, ByVal blendIndex As Integer, Optional ByVal includeExtension As Boolean = True) As String
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

		//	If includeExtension Then
		//		If Path.GetExtension(animationSmdRelativePathFileName) <> ".smd" Then
		//			'NOTE: Add the ".smd" extension, keeping the existing extension in file name, which is often ".dmx" for newer models. 
		//			'      Thus, user can see that model might have newer features that Crowbar does not yet handle.
		//			animationSmdRelativePathFileName += ".smd"
		//		End If
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

			//Type can be -1 if original QC did not use any of the control texts above, so use a control text that is not one of the above.
			return "N";
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

		// For the SourceMdlSequenceDesc10.activityId field.
		//FROM: [1999] HLStandardSDK\SourceCode\dlls\activity.h
		//typedef enum {
		//	ACT_RESET = 0,		// Set m_Activity to this invalid value to force a reset to m_IdealActivity
		//	ACT_IDLE = 1,
		//	ACT_GUARD,
		//	ACT_WALK,
		//	ACT_RUN,
		//	ACT_FLY,				// Fly (and flap if appropriate)
		//	ACT_SWIM,
		//	ACT_HOP,				// vertical jump
		//	ACT_LEAP,				// long forward jump
		//	ACT_FALL,
		//	ACT_LAND,
		//	ACT_STRAFE_LEFT,
		//	ACT_STRAFE_RIGHT,
		//	ACT_ROLL_LEFT,			// tuck and roll, left
		//	ACT_ROLL_RIGHT,			// tuck and roll, right
		//	ACT_TURN_LEFT,			// turn quickly left (stationary)
		//	ACT_TURN_RIGHT,			// turn quickly right (stationary)
		//	ACT_CROUCH,				// the act of crouching down from a standing position
		//	ACT_CROUCHIDLE,			// holding body in crouched position (loops)
		//	ACT_STAND,				// the act of standing from a crouched position
		//	ACT_USE,
		//	ACT_SIGNAL1,
		//	ACT_SIGNAL2,
		//	ACT_SIGNAL3,
		//	ACT_TWITCH,
		//	ACT_COWER,
		//	ACT_SMALL_FLINCH,
		//	ACT_BIG_FLINCH,
		//	ACT_RANGE_ATTACK1,
		//	ACT_RANGE_ATTACK2,
		//	ACT_MELEE_ATTACK1,
		//	ACT_MELEE_ATTACK2,
		//	ACT_RELOAD,
		//	ACT_ARM,				// pull out gun, for instance
		//	ACT_DISARM,				// reholster gun
		//	ACT_EAT,				// monster chowing on a large food item (loop)
		//	ACT_DIESIMPLE,
		//	ACT_DIEBACKWARD,
		//	ACT_DIEFORWARD,
		//	ACT_DIEVIOLENT,
		//	ACT_BARNACLE_HIT,		// barnacle tongue hits a monster
		//	ACT_BARNACLE_PULL,		// barnacle is lifting the monster ( loop )
		//	ACT_BARNACLE_CHOMP,		// barnacle latches on to the monster
		//	ACT_BARNACLE_CHEW,		// barnacle is holding the monster in its mouth ( loop )
		//	ACT_SLEEP,
		//	ACT_INSPECT_FLOOR,		// for active idles, look at something on or near the floor
		//	ACT_INSPECT_WALL,		// for active idles, look at something directly ahead of you ( doesn't HAVE to be a wall or on a wall )
		//	ACT_IDLE_ANGRY,			// alternate idle animation in which the monster is clearly agitated. (loop)
		//	ACT_WALK_HURT,			// limp  (loop)
		//	ACT_RUN_HURT,			// limp  (loop)
		//	ACT_HOVER,				// Idle while in flight
		//	ACT_GLIDE,				// Fly (don't flap)
		//	ACT_FLY_LEFT,			// Turn left in flight
		//	ACT_FLY_RIGHT,			// Turn right in flight
		//	ACT_DETECT_SCENT,		// this means the monster smells a scent carried by the air
		//	ACT_SNIFF,				// this is the act of actually sniffing an item in front of the monster
		//	ACT_BITE,				// some large monsters can eat small things in one bite. This plays one time, EAT loops.
		//	ACT_THREAT_DISPLAY,		// without attacking, monster demonstrates that it is angry. (Yell, stick out chest, etc )
		//	ACT_FEAR_DISPLAY,		// monster just saw something that it is afraid of
		//	ACT_EXCITED,			// for some reason, monster is excited. Sees something he really likes to eat, or whatever.
		//	ACT_SPECIAL_ATTACK1,	// very monster specific special attacks.
		//	ACT_SPECIAL_ATTACK2,	
		//	ACT_COMBAT_IDLE,		// agitated idle.
		//	ACT_WALK_SCARED,
		//	ACT_RUN_SCARED,
		//	ACT_VICTORY_DANCE,		// killed a player, do a victory dance.
		//	ACT_DIE_HEADSHOT,		// die, hit in head. 
		//	ACT_DIE_CHESTSHOT,		// die, hit in chest
		//	ACT_DIE_GUTSHOT,		// die, hit in gut
		//	ACT_DIE_BACKSHOT,		// die, hit in back
		//	ACT_FLINCH_HEAD,
		//	ACT_FLINCH_CHEST,
		//	ACT_FLINCH_STOMACH,
		//	ACT_FLINCH_LEFTARM,
		//	ACT_FLINCH_RIGHTARM,
		//	ACT_FLINCH_LEFTLEG,
		//	ACT_FLINCH_RIGHTLEG,
		//} Activity;

		public static string[] activityMap = {"ACT_RESET", "ACT_IDLE", "ACT_GUARD", "ACT_WALK", "ACT_RUN", "ACT_FLY", "ACT_SWIM", "ACT_HOP", "ACT_LEAP", "ACT_FALL", "ACT_LAND", "ACT_STRAFE_LEFT", "ACT_STRAFE_RIGHT", "ACT_ROLL_LEFT", "ACT_ROLL_RIGHT", "ACT_TURN_LEFT", "ACT_TURN_RIGHT", "ACT_CROUCH", "ACT_CROUCHIDLE", "ACT_STAND", "ACT_USE", "ACT_SIGNAL1", "ACT_SIGNAL2", "ACT_SIGNAL3", "ACT_TWITCH", "ACT_COWER", "ACT_SMALL_FLINCH", "ACT_BIG_FLINCH", "ACT_RANGE_ATTACK1", "ACT_RANGE_ATTACK2", "ACT_MELEE_ATTACK1", "ACT_MELEE_ATTACK2", "ACT_RELOAD", "ACT_ARM", "ACT_DISARM", "ACT_EAT", "ACT_DIESIMPLE", "ACT_DIEBACKWARD", "ACT_DIEFORWARD", "ACT_DIEVIOLENT", "ACT_BARNACLE_HIT", "ACT_BARNACLE_PULL", "ACT_BARNACLE_CHOMP", "ACT_BARNACLE_CHEW", "ACT_SLEEP", "ACT_INSPECT_FLOOR", "ACT_INSPECT_WALL", "ACT_IDLE_ANGRY", "ACT_WALK_HURT", "ACT_RUN_HURT", "ACT_HOVER", "ACT_GLIDE", "ACT_FLY_LEFT", "ACT_FLY_RIGHT", "ACT_DETECT_SCENT", "ACT_SNIFF", "ACT_BITE", "ACT_THREAT_DISPLAY", "ACT_FEAR_DISPLAY", "ACT_EXCITED", "ACT_SPECIAL_ATTACK1", "ACT_SPECIAL_ATTACK2", "ACT_COMBAT_IDLE", "ACT_WALK_SCARED", "ACT_RUN_SCARED", "ACT_VICTORY_DANCE", "ACT_DIE_HEADSHOT", "ACT_DIE_CHESTSHOT", "ACT_DIE_GUTSHOT", "ACT_DIE_BACKSHOT", "ACT_FLINCH_HEAD", "ACT_FLINCH_CHEST", "ACT_FLINCH_STOMACH", "ACT_FLINCH_LEFTARM", "ACT_FLINCH_RIGHTARM", "ACT_FLINCH_LEFTLEG", "ACT_FLINCH_RIGHTLEG"};

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