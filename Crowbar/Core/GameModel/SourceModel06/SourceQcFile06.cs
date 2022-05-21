//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Text;

namespace Crowbar
{
	public class SourceQcFile06
	{

		//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
		//      void ParseScript (void)
		//		if (!strcmp (token, "$modelname"))
		//		else if (!strcmp (token, "$cd"))
		//		else if (!strcmp (token, "$cdtexture"))
		//		else if (!strcmp (token, "$scale"))
		//		else if (!strcmp (token, "$root"))
		//		else if (!strcmp (token, "$pivot"))
		//		else if (!strcmp (token, "$controller"))
		//		else if (!strcmp (token, "$body"))
		//		else if (!strcmp (token, "$bodygroup"))
		//		else if (!strcmp (token, "$sequence"))
		//		else if (!strcmp (token, "$sequencegroup"))
		//		else if (!strcmp (token, "$sequencegroupsize"))
		//		else if (!strcmp (token, "$eyeposition"))
		//		else if (!strcmp (token, "$origin"))
		//		else if (!strcmp (token, "$bbox"))
		//		else if (!strcmp (token, "$cbox"))
		//		else if (!strcmp (token, "$mirrorbone"))
		//		else if (!strcmp (token, "$gamma"))
		//		else if (!strcmp (token, "$flags"))
		//		else if (!strcmp (token, "$texturegroup"))
		//		else if (!strcmp (token, "$hgroup"))
		//		else if (!strcmp (token, "$hbox"))
		//		else if (!strcmp (token, "$attachment"))
		//		else if (!strcmp (token, "$externaltextures"))
		//		else if (!strcmp (token, "$cliptotextures"))
		//		else if (!strcmp (token, "$renamebone"))
		//FROM: [1999] HLStandardSDK\SourceCode\utils\common\scriplib.c
		//      qboolean GetToken (qboolean crossline)
		//	if (!strcmp (token, "$include"))
		//------
		// Commands that can be decompiled: 
		///  $attachment
		///  $bbox
		///  $body   (can be decompiled as a single-model $bodygroup)
		///  $bodygroup
		///  $cbox
		//X  $cdtexture   (not stored and don't need if all texture BMP files written to same folder as QC file)
		///  $controller
		///  $externaltextures
		///  $eyeposition
		///  $flags
		///  $hbox
		//X $hgroup   (this autogenerates the same data that $hbox command does, so this will decompile as $hbox commands)
		//  $include
		///  $modelname
		//  $sequence
		//  $sequencegroup
		//X  $sequencegroupsize  (this autogenerates the same data that $sequencegroup does, so this will decompile as $sequencegroup commands)
		///  $texturegroup

#region Creation and Destruction

		public SourceQcFile06(StreamWriter outputFileStream, string outputPathFileName, SourceMdlFileData06 mdlFileData, string modelName)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			theModelName = modelName;

			theOutputPath = FileManager.GetPath(outputPathFileName);
			theOutputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPathFileName);
		}

#endregion

#region Methods

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(theOutputFileStreamWriter);
		}

		public void WriteBodyGroupCommand()
		{
			string line = "";
			SourceMdlBodyPart06 aBodyPart = null;
			SourceMdlModel06 aBodyModel = null;

			if (theMdlFileData.theBodyParts != null && theMdlFileData.theBodyParts.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$BodyGroup ";
					}
					else
					{
						line = "$bodygroup ";
					}
					line += "\"";
					line += aBodyPart.theName;
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					theOutputFileStreamWriter.WriteLine(line);

					if (aBodyPart.theModels != null && aBodyPart.theModels.Count > 0)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theModels.Count; modelIndex++)
						{
							aBodyModel = aBodyPart.theModels[modelIndex];

							line = "\t";
							if (aBodyModel.theName == "blank")
							{
								line += "blank";
							}
							else
							{
								aBodyModel.theSmdFileName = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileName, bodyPartIndex, modelIndex, 0, theModelName, theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].theName);
								line += "studio ";
								line += "\"";
								line += aBodyModel.theSmdFileName;
								line += "\"";
							}
							theOutputFileStreamWriter.WriteLine(line);
						}
					}

					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteControllerCommand()
		{
			string line = "";
			SourceMdlBoneController06 boneController = null;

			try
			{
				if (theMdlFileData.theBoneControllers != null)
				{
					if (theMdlFileData.theBoneControllers.Count > 0)
					{
						theOutputFileStreamWriter.WriteLine();
					}

					for (int boneControllerIndex = 0; boneControllerIndex < theMdlFileData.theBoneControllers.Count; boneControllerIndex++)
					{
						boneController = theMdlFileData.theBoneControllers[boneControllerIndex];

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$Controller ";
						}
						else
						{
							line = "$controller ";
						}
						line += boneControllerIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
						line += " \"";
						line += theMdlFileData.theBones[boneController.boneIndex].theName;
						line += "\" ";
						line += SourceModule06.GetControlText(boneController.type);
						line += " ";
						line += boneController.startAngleDegrees.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += boneController.endAngleDegrees.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		public void WriteModelNameCommand()
		{
			string line = "";
			string modelPathFileName = theMdlFileData.theModelName;


			theOutputFileStreamWriter.WriteLine();

			if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
			{
				line = "$ModelName ";
			}
			else
			{
				line = "$modelname ";
			}
			line += "\"";
			line += modelPathFileName;
			line += "\"";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSequenceCommands()
		{
			string line = "";
			SourceMdlSequenceDesc06 aSequence = null;

			if (theMdlFileData.theSequences != null && theMdlFileData.theSequences.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				for (int sequenceGroupIndex = 0; sequenceGroupIndex < theMdlFileData.theSequences.Count; sequenceGroupIndex++)
				{
					aSequence = theMdlFileData.theSequences[sequenceGroupIndex];

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$Sequence ";
					}
					else
					{
						line = "$sequence ";
					}
					line += "\"";
					line += aSequence.theName;
					line += "\"";
					//NOTE: Opening brace must be on same line as the command.
					line += " {";
					theOutputFileStreamWriter.WriteLine(line);

					WriteSequenceOptions(aSequence);

					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		//		else if (stricmp("deform", token ) == 0)
		//		else if (stricmp("event", token ) == 0)
		//		else if (stricmp("pivot", token ) == 0)
		//		else if (stricmp("fps", token ) == 0)
		//		else if (stricmp("origin", token ) == 0)
		//		else if (stricmp("rotate", token ) == 0)
		//		else if (stricmp("scale", token ) == 0)
		//		else if (strnicmp("loop", token, 4 ) == 0)
		//		else if (strnicmp("frame", token, 5 ) == 0)
		//		else if (strnicmp("blend", token, 5 ) == 0)
		//		else if (strnicmp("node", token, 4 ) == 0)
		//		else if (strnicmp("transition", token, 4 ) == 0)
		//		else if (strnicmp("rtransition", token, 4 ) == 0)
		//		else if (lookupControl( token ) != -1)
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
		//		else if (stricmp("animation", token ) == 0)
		//		else if ((i = lookupActivity( token )) != 0)
		//int lookupActivity( char *szActivity )
		//{
		//	int i;
		//
		//	for (i = 0; activity_map[i].name; i++)
		//	{
		//		if (stricmp( szActivity, activity_map[i].name ) == 0)
		//			return activity_map[i].type;
		//	}
		//	// match ACT_#
		//	if (strnicmp( szActivity, "ACT_", 4 ) == 0)
		//	{
		//		return atoi( &szActivity[4] );
		//	}
		//	return 0;
		//}
		//		else
		//		{
		//			strcpyn( smdfilename[numblends++], token );
		//		}
		//------
		//  [activity_name or ACT_#]
		//X  animation   (same as using "smdfilename" by itself)
		///  blend
		//X  deform   (seems to be a deleted command)
		///  event
		///  fps
		//X  frame   (not decompilable and not needed; when used the frames will decompile as a separate SMD file)
		///  loop
		//  node
		//X  origin   (baked in)
		//  pivot
		//X  rotate   (baked in)
		//  rtransition
		//X  scale   (baked in)
		//  transition
		//  [X, Y, Z, XR, YR, ZR, LX, LY, LZ, AX, AY, AZ, AXR, AYR, AZR]
		///  ["smdFileName"]
		private void WriteSequenceOptions(SourceMdlSequenceDesc06 aSequenceDesc)
		{
			string line = "";

			for (int blendIndex = 0; blendIndex < aSequenceDesc.blendCount; blendIndex++)
			{
				if (aSequenceDesc.blendCount == 1)
				{
					aSequenceDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileName, theModelName, aSequenceDesc.theName, -1);
				}
				else
				{
					aSequenceDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileName, theModelName, aSequenceDesc.theName, blendIndex);
				}

				line = "\t";
				line += "\"";
				line += aSequenceDesc.theSmdRelativePathFileName;
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}

			//For i As Integer = 0 To 1
			//	If aSequenceDesc.blendType(i) <> 0 Then
			//		line = vbTab
			//		line += "blend "
			//		line += """"
			//		line += SourceModule06.GetControlText(aSequenceDesc.blendType(i))
			//		line += """"
			//		line += " "
			//		line += aSequenceDesc.blendStart(i).ToString("0.######", TheApp.InternalNumberFormat)
			//		line += " "
			//		line += aSequenceDesc.blendEnd(i).ToString("0.######", TheApp.InternalNumberFormat)
			//		Me.theOutputFileStreamWriter.WriteLine(line)
			//	End If
			//Next

			if (aSequenceDesc.theEvents != null)
			{
				int frameIndex = 0;
				for (int j = 0; j < aSequenceDesc.theEvents.Count; j++)
				{
					if (aSequenceDesc.frameCount <= 1)
					{
						frameIndex = 0;
					}
					else
					{
						frameIndex = aSequenceDesc.theEvents[j].frameIndex;
					}

					line = "\t";
					line += "{ ";
					line += "event ";
					//line += aSequenceDesc.theEvents(j).eventIndex.ToString(TheApp.InternalNumberFormat)
					line += aSequenceDesc.theEvents[j].eventType.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += frameIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					//If aSequenceDesc.theEvents(j).theOptions <> "" Then
					//	line += " """
					//	line += aSequenceDesc.theEvents(j).theOptions
					//	line += """"
					//End If
					line += " }";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "\t";
			line += "fps ";
			line += aSequenceDesc.fps.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);

			if ((aSequenceDesc.flags & SourceMdlSequenceDesc06.STUDIO_LOOPING) > 0)
			{
				line = "\t";
				line += "loop";
				theOutputFileStreamWriter.WriteLine(line);
			}

			if (aSequenceDesc.motiontype > 0)
			{
				line = "\t";
				line += SourceModule06.GetMultipleControlText(aSequenceDesc.motiontype);
				theOutputFileStreamWriter.WriteLine(line);
			}

			//Me.WriteSequenceNodeInfo(aSequenceDesc)

			//If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_AUTOPLAY) > 0 Then
			//	line = vbTab
			//	line += "autoplay"
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If

			//If blah Then
			//	line = vbTab
			//	line += ""
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If
		}

		//Private Sub WriteSequenceNodeInfo(ByVal aSeqDesc As SourceMdlSequenceDesc)
		//	Dim line As String = ""

		//	'If aSeqDesc.localEntryNodeIndex > 0 Then
		//	'	If aSeqDesc.localEntryNodeIndex = aSeqDesc.localExitNodeIndex Then
		//	'		'node (name)
		//	'		line = vbTab
		//	'		line += "node"
		//	'		line += " """
		//	'		'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
		//	'		line += Me.theMdlFileData.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
		//	'		line += """"
		//	'		Me.theOutputFileStreamWriter.WriteLine(line)
		//	'	ElseIf (aSeqDesc.nodeFlags And 1) = 0 Then
		//	'		'transition (from) (to) 
		//	'		line = vbTab
		//	'		line += "transition"
		//	'		line += " """
		//	'		'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
		//	'		line += Me.theMdlFileData.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
		//	'		line += """ """
		//	'		'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
		//	'		line += Me.theMdlFileData.theLocalNodeNames(aSeqDesc.localExitNodeIndex - 1)
		//	'		line += """"
		//	'		Me.theOutputFileStreamWriter.WriteLine(line)
		//	'	Else
		//	'		'rtransition (name1) (name2) 
		//	'		line = vbTab
		//	'		line += "rtransition"
		//	'		line += " """
		//	'		'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
		//	'		line += Me.theMdlFileData.theLocalNodeNames(aSeqDesc.localEntryNodeIndex - 1)
		//	'		line += """ """
		//	'		'NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
		//	'		line += Me.theMdlFileData.theLocalNodeNames(aSeqDesc.localExitNodeIndex - 1)
		//	'		line += """"
		//	'		Me.theOutputFileStreamWriter.WriteLine(line)
		//	'	End If
		//	'End If
		//End Sub

#endregion

#region Constants

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData06 theMdlFileData;
		private string theModelName;

		private string theOutputPath;
		private string theOutputFileNameWithoutExtension;

#endregion

	}

}