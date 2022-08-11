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
	public class SourceVrdFile37
	{

#region Creation and Destruction

		public SourceVrdFile37(StreamWriter outputFileStream, SourceMdlFileData37 mdlFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
		}

#endregion

#region Methods

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(theOutputFileStreamWriter);
		}

		public void WriteCommands()
		{
			if (theMdlFileData.theBones != null)
			{
				string line = "";
				SourceMdlBone37 aBone = null;
				SourceMdlBone37 aParentBone = null;
				SourceMdlBone37 aControlBone = null;
				SourceMdlBone37 aParentControlBone = null;
				SourceMdlQuatInterpBoneInfo aTrigger = null;
				SourceVector aTriggerTrigger = null;
				SourceVector aTriggerQuat = null;
				string aBoneName = null;
				string aParentBoneName = null;
				string aParentControlBoneName = null;
				string aControlBoneName = null;

				for (int i = 0; i < theMdlFileData.theBones.Count; i++)
				{
					aBone = theMdlFileData.theBones[i];

					if (aBone.proceduralRuleOffset != 0)
					{
						if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_AXISINTERP)
						{
						}
						else if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_QUATINTERP)
						{
							//<helper> Bip01_L_Elbow Bip01_L_UpperArm Bip01_L_UpperArm Bip01_L_Forearm
							//<display> 1.5 3 3 100
							//<basepos> 0 0 0
							//<trigger> 90 0 0 0 0 0 0 0 0 0
							//<trigger> 90 0 0 -90 0 0 -45 0 0 0

							//int i = sscanf( g_szLine, "%s %s %s %s %s", cmd, pBone->bonename, pBone->parentname, pBone->controlparentname, pBone->controlname );
							aParentBone = theMdlFileData.theBones[aBone.parentBoneIndex];
							aControlBone = theMdlFileData.theBones[aBone.theQuatInterpBone.controlBoneIndex];
							aParentControlBone = theMdlFileData.theBones[aControlBone.parentBoneIndex];

							//NOTE: A bone name in a VRD file must have its characters up to and including the first dot removed.
							//aBoneName = aBone.theName.Replace("ValveBiped.", "")
							//aParentBoneName = aParentBone.theName.Replace("ValveBiped.", "")
							//aParentControlBoneName = aParentControlBone.theName.Replace("ValveBiped.", "")
							//aControlBoneName = aControlBone.theName.Replace("ValveBiped.", "")
							aBoneName = StringClass.RemoveUptoAndIncludingFirstDotCharacterFromString(aBone.theName);
							aParentBoneName = StringClass.RemoveUptoAndIncludingFirstDotCharacterFromString(aParentBone.theName);
							aParentControlBoneName = StringClass.RemoveUptoAndIncludingFirstDotCharacterFromString(aParentControlBone.theName);
							aControlBoneName = StringClass.RemoveUptoAndIncludingFirstDotCharacterFromString(aControlBone.theName);

							theOutputFileStreamWriter.WriteLine();

							line = "<helper>";
							line += " ";
							line += aBoneName;
							line += " ";
							line += aParentBoneName;
							line += " ";
							line += aParentControlBoneName;
							line += " ";
							line += aControlBoneName;
							theOutputFileStreamWriter.WriteLine(line);

							//'NOTE: Use "1" for the 3 size values because it looks like they are not used in compile.
							//line = "<display>"
							//line += " "
							//line += "1"
							//line += " "
							//line += "1"
							//line += " "
							//line += "1"
							//line += " "
							//'TODO: Reverse this to decompile.
							//'pAxis->percentage = distance / 100.0;
							//'tmp = pInterp->pos[k] + pInterp->basepos + g_bonetable[pInterp->control].pos * pInterp->percentage;
							//line += "100"
							//Me.theOutputFileStreamWriter.WriteLine(line)

							line = "<basepos>";
							line += " ";
							line += "0";
							line += " ";
							line += "0";
							line += " ";
							line += "0";
							theOutputFileStreamWriter.WriteLine(line);

							for (int triggerIndex = 0; triggerIndex < aBone.theQuatInterpBone.theTriggers.Count; triggerIndex++)
							{
								aTrigger = aBone.theQuatInterpBone.theTriggers[triggerIndex];

								aTriggerTrigger = MathModule.ToEulerAngles(aTrigger.trigger);
								aTriggerQuat = MathModule.ToEulerAngles(aTrigger.quat);

								line = "<trigger>";
								line += " ";
								//pAxis->tolerance[j] = DEG2RAD( tolerance );
								line += MathModule.RadiansToDegrees(1 / aTrigger.inverseToleranceAngle).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

								//trigger.x = DEG2RAD( trigger.x );
								//trigger.y = DEG2RAD( trigger.y );
								//trigger.z = DEG2RAD( trigger.z );
								//AngleQuaternion( trigger, pAxis->trigger[j] );
								line += " ";
								line += MathModule.RadiansToDegrees(aTriggerTrigger.x).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								line += " ";
								line += MathModule.RadiansToDegrees(aTriggerTrigger.y).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								line += " ";
								line += MathModule.RadiansToDegrees(aTriggerTrigger.z).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								//line += " "
								//line += MathModule.RadiansToDegrees(aTriggerTrigger.z).ToString("0.######", TheApp.InternalNumberFormat)
								//line += " "
								//line += MathModule.RadiansToDegrees(aTriggerTrigger.y).ToString("0.######", TheApp.InternalNumberFormat)
								//line += " "
								//line += MathModule.RadiansToDegrees(aTriggerTrigger.x).ToString("0.######", TheApp.InternalNumberFormat)

								//ang.x = DEG2RAD( ang.x );
								//ang.y = DEG2RAD( ang.y );
								//ang.z = DEG2RAD( ang.z );
								//AngleQuaternion( ang, pAxis->quat[j] );
								line += " ";
								line += MathModule.RadiansToDegrees(aTriggerQuat.x).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								line += " ";
								line += MathModule.RadiansToDegrees(aTriggerQuat.y).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								line += " ";
								line += MathModule.RadiansToDegrees(aTriggerQuat.z).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								//line += " "
								//line += MathModule.RadiansToDegrees(aTriggerQuat.z).ToString("0.######", TheApp.InternalNumberFormat)
								//line += " "
								//line += MathModule.RadiansToDegrees(aTriggerQuat.y).ToString("0.######", TheApp.InternalNumberFormat)
								//line += " "
								//line += MathModule.RadiansToDegrees(aTriggerQuat.x).ToString("0.######", TheApp.InternalNumberFormat)

								//VectorAdd( basepos, pos, pAxis->pos[j] );
								line += " ";
								line += aTrigger.pos.x.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								line += " ";
								line += aTrigger.pos.y.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								line += " ";
								line += aTrigger.pos.z.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
								theOutputFileStreamWriter.WriteLine(line);
							}
						}
					}
				}
			}
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData37 theMdlFileData;

#endregion

	}

}