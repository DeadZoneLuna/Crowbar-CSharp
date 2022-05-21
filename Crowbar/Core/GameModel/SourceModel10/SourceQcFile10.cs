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
	public class SourceQcFile10 : SourceQcFile
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
		//-  $sequencegroupsize  (this might be determined by looking at the largest file size of the seq group MDL files [all but the main MDL file])
		///  $texturegroup

#region Creation and Destruction

		public SourceQcFile10(StreamWriter outputFileStream, string outputPathFileName, SourceMdlFileData10 mdlFileData, string modelName)
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

		public void WriteAttachmentCommand()
		{
			string line = "";

			if (theMdlFileData.theAttachments != null)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.theAttachments.Count; i++)
				{
					SourceMdlAttachment10 anAttachment = theMdlFileData.theAttachments[i];

					line = "$attachment ";
					line += i.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " \"";
					line += theMdlFileData.theBones[anAttachment.boneIndex].theName;
					line += "\"";
					line += " ";

					line += anAttachment.attachmentPoint.x.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += anAttachment.attachmentPoint.y.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += anAttachment.attachmentPoint.z.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteBBoxCommand()
		{
			string line = "";
			double minX = 0;
			double minY = 0;
			double minZ = 0;
			double maxX = 0;
			double maxY = 0;
			double maxZ = 0;

			theOutputFileStreamWriter.WriteLine();

			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				line = "// Bounding box or hull. Used for collision with a world object.";
				theOutputFileStreamWriter.WriteLine(line);
			}

			//FROM: VDC wiki: 
			//$bbox (min x) (min y) (min z) (max x) (max y) (max z)
			minX = Math.Round(theMdlFileData.hullMinPosition.x, 3);
			minY = Math.Round(theMdlFileData.hullMinPosition.y, 3);
			minZ = Math.Round(theMdlFileData.hullMinPosition.z, 3);
			maxX = Math.Round(theMdlFileData.hullMaxPosition.x, 3);
			maxY = Math.Round(theMdlFileData.hullMaxPosition.y, 3);
			maxZ = Math.Round(theMdlFileData.hullMaxPosition.z, 3);

			line = "";
			line += "$bbox ";
			line += minX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += minY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += minZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += maxX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += maxY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += maxZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteBodyGroupCommand()
		{
			string line = "";
			SourceMdlBodyPart10 aBodyPart = null;
			SourceMdlModel10 aBodyModel = null;

			if (theMdlFileData.theBodyParts != null && theMdlFileData.theBodyParts.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];

					line = "$bodygroup ";
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
								line += Path.GetFileNameWithoutExtension(aBodyModel.theSmdFileName);
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

		public void WriteCBoxCommand()
		{
			string line = "";
			double minX = 0;
			double minY = 0;
			double minZ = 0;
			double maxX = 0;
			double maxY = 0;
			double maxZ = 0;

			theOutputFileStreamWriter.WriteLine();

			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				line = "// Clipping box or view bounding box.";
				theOutputFileStreamWriter.WriteLine(line);
			}

			//FROM: VDC wiki: 
			//$cbox <float|minx> <float|miny> <float|minz> <float|maxx> <float|maxy> <float|maxz> 
			minX = Math.Round(theMdlFileData.viewBoundingBoxMinPosition.x, 3);
			minY = Math.Round(theMdlFileData.viewBoundingBoxMinPosition.y, 3);
			minZ = Math.Round(theMdlFileData.viewBoundingBoxMinPosition.z, 3);
			maxX = Math.Round(theMdlFileData.viewBoundingBoxMaxPosition.x, 3);
			maxY = Math.Round(theMdlFileData.viewBoundingBoxMaxPosition.y, 3);
			maxZ = Math.Round(theMdlFileData.viewBoundingBoxMaxPosition.z, 3);

			line = "$cbox ";
			line += minX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += minY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += minZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += maxX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += maxY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += maxZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteCDCommand()
		{
			string line = "";

			line = "$cd \".\"";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteCDTextureCommand()
		{
			string line = "";

			line = "$cdtexture \".\"";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteClipToTexturesCommand()
		{
			string line = "";

			line = "$cliptotextures";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteControllerCommand()
		{
			string line = "";
			SourceMdlBoneController10 boneController = null;

			//$controller mouth "jaw" X 0 20
			//$controller 0 "tracker" LYR -1 1
			try
			{
				if (theMdlFileData.theBoneControllers != null)
				{
					if (theMdlFileData.theBoneControllers.Count > 0)
					{
						theOutputFileStreamWriter.WriteLine();
					}

					for (int i = 0; i < theMdlFileData.theBoneControllers.Count; i++)
					{
						boneController = theMdlFileData.theBoneControllers[i];

						line = "$controller ";
						if (boneController.index == 4)
						{
							line += "Mouth";
						}
						else
						{
							line += boneController.index.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
						}
						line += " \"";
						line += theMdlFileData.theBones[boneController.boneIndex].theName;
						line += "\" ";
						line += SourceModule10.GetControlText(boneController.type);
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

		public void WriteExternalTexturesCommand()
		{
			string line = "";

			if (theMdlFileData.textureCount == 0)
			{
				theOutputFileStreamWriter.WriteLine();

				line = "$externaltextures";
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteEyePositionCommand()
		{
			string line = "";
			double offsetX = 0;
			double offsetY = 0;
			double offsetZ = 0;

			offsetX = Math.Round(theMdlFileData.eyePosition.y, 3);
			offsetY = -Math.Round(theMdlFileData.eyePosition.x, 3);
			offsetZ = Math.Round(theMdlFileData.eyePosition.z, 3);

			if (offsetX == 0 && offsetY == 0 && offsetZ == 0)
			{
				return;
			}

			line = "";
			theOutputFileStreamWriter.WriteLine(line);

			line = "$eyeposition ";
			line += offsetX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += offsetY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += offsetZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteFlagsCommand()
		{
			string line = "";

			theOutputFileStreamWriter.WriteLine();

			line = "$flags ";
			line += theMdlFileData.flags.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteHBoxCommands()
		{
			string line = "";
			SourceMdlHitbox10 aHitbox = null;

			if (theMdlFileData.theHitboxes.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				for (int j = 0; j < theMdlFileData.theHitboxes.Count; j++)
				{
					aHitbox = theMdlFileData.theHitboxes[j];

					line = "$hbox ";
					line += aHitbox.groupIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += "\"";
					line += theMdlFileData.theBones[aHitbox.boneIndex].theName;
					line += "\"";
					line += " ";
					line += aHitbox.boundingBoxMin.x.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aHitbox.boundingBoxMin.y.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aHitbox.boundingBoxMin.z.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aHitbox.boundingBoxMax.x.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aHitbox.boundingBoxMax.y.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aHitbox.boundingBoxMax.z.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteModelNameCommand()
		{
			string line = "";
			string modelPathFileName;

			//NOTE: Ignore internal model name ($modelname) and instead use file name of MDL file.
			//      This seems to be how it is handled by Half-Life and all the tools for it.
			//modelPathFileName = Me.theMdlFileData.theModelName
			modelPathFileName = theMdlFileData.theFileName + ".mdl";

			theOutputFileStreamWriter.WriteLine();

			//If Path.GetExtension(modelPathFileName) <> ".mdl" Then
			if (modelPathFileName != theMdlFileData.theModelName)
			{
				line = "// Stored modelname (without quotes): \"" + theMdlFileData.theModelName + "\"";
				theOutputFileStreamWriter.WriteLine(line);

				//modelPathFileName = Me.theMdlFileData.theFileName + ".mdl"
			}

			line = "$modelname ";
			line += "\"";
			line += modelPathFileName;
			line += "\"";
			theOutputFileStreamWriter.WriteLine(line);
		}

		//NOTE: Although this code is correct, the $sequencegroup command seems completely pointless; it just labels each group, but is not used for anything.
		//Public Sub WriteSequenceGroupCommands()
		//	Dim line As String = ""
		//	Dim aSequenceGroup As SourceMdlSequenceGroup10

		//	If Me.theMdlFileData.theSequenceGroups.Count > 1 Then
		//		Me.theOutputFileStreamWriter.WriteLine()

		//		For sequenceGroupIndex As Integer = 0 To Me.theMdlFileData.theSequenceGroups.Count - 1
		//			aSequenceGroup = Me.theMdlFileData.theSequenceGroups(sequenceGroupIndex)

		//				line = "$sequencegroup "
		//			line += """"
		//			line += aSequenceGroup.theName
		//			line += """"

		//			Me.theOutputFileStreamWriter.WriteLine(line)
		//		Next
		//	End If
		//End Sub

		public void WriteSequenceGroupSizeCommand()
		{
			string line = "";
			long fileSize = 0;
			long largestFileSize = 0;
			long groupSize = 0;
			double remainder = 0;

			if (theMdlFileData.theSequenceGroups.Count > 1)
			{
				for (int sequenceGroupIndex = 0; sequenceGroupIndex < theMdlFileData.theSequenceGroups.Count; sequenceGroupIndex++)
				{
					fileSize = theMdlFileData.theSequenceGroupFileHeaders[sequenceGroupIndex].theActualFileSize;
					if (largestFileSize < fileSize)
					{
						largestFileSize = fileSize;
					}
				}

				if (largestFileSize > 0)
				{
					groupSize = largestFileSize / 1024;
					remainder = largestFileSize % 1024;
					if (remainder > 0)
					{
						groupSize += 1;
					}

					theOutputFileStreamWriter.WriteLine();

					line = "$sequencegroupsize ";
					line += groupSize.ToString(MainCROWBAR.TheApp.InternalNumberFormat);

					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteScaleCommand()
		{
			string line = "";

			line = "$scale 1.0";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSequenceCommands()
		{
			string line = "";
			SourceMdlSequenceDesc10 aSequence = null;

			if (theMdlFileData.theSequences != null && theMdlFileData.theSequences.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				for (int sequenceGroupIndex = 0; sequenceGroupIndex < theMdlFileData.theSequences.Count; sequenceGroupIndex++)
				{
					aSequence = theMdlFileData.theSequences[sequenceGroupIndex];

					line = "$sequence ";
					line += "\"";
					line += aSequence.theName;
					line += "\"";
					//NOTE: Opening brace must be on same line as the command.
					line += " {";
					theOutputFileStreamWriter.WriteLine(line);

					try
					{
						WriteSequenceOptions(aSequence);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}

					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteTexRenderMode()
		{
			if (theMdlFileData.theTextures != null && theMdlFileData.theTextures.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				foreach (SourceMdlTexture10 texture in theMdlFileData.theTextures)
				{
					if (((texture.flags & SourceMdlTexture10.STUDIO_NF_FLATSHADE) > 0) && (!(texture.theFileName.ToLower().Contains("chrome"))))
					{
						WriteTexRenderModeLine(texture.theFileName, "flatshade", true);
					}
					if (((texture.flags & SourceMdlTexture10.STUDIO_NF_CHROME) > 0) && (!(texture.theFileName.ToLower().Contains("chrome"))))
					{
						WriteTexRenderModeLine(texture.theFileName, "chrome", false);
					}
					if ((texture.flags & SourceMdlTexture10.STUDIO_NF_FULLBRIGHT) > 0)
					{
						WriteTexRenderModeLine(texture.theFileName, "fullbright", false);
					}
					if ((texture.flags & SourceMdlTexture10.STUDIO_NF_NOMIPS) > 0)
					{
						WriteTexRenderModeLine(texture.theFileName, "nomips", false);
					}
					if ((texture.flags & SourceMdlTexture10.STUDIO_NF_ALPHA) > 0)
					{
						WriteTexRenderModeLine(texture.theFileName, "alpha", false);
					}
					if ((texture.flags & SourceMdlTexture10.STUDIO_NF_ADDITIVE) > 0)
					{
						WriteTexRenderModeLine(texture.theFileName, "additive", false);
					}
					if ((texture.flags & SourceMdlTexture10.STUDIO_NF_MASKED) > 0)
					{
						WriteTexRenderModeLine(texture.theFileName, "masked", false);
					}
				}
			}
		}

		public void WriteTextureGroupCommand()
		{
			string line = "";

			if (theMdlFileData.theSkinFamilies != null && theMdlFileData.theSkinFamilies.Count > 0 && theMdlFileData.theTextures != null && theMdlFileData.theTextures.Count > 0 && theMdlFileData.skinReferenceCount > 0)
			{
				//Me.theOutputFileStreamWriter.WriteLine()

				//	line = "$texturegroup ""skinfamilies"""
				//Me.theOutputFileStreamWriter.WriteLine(line)
				//line = "{"
				//Me.theOutputFileStreamWriter.WriteLine(line)

				//Dim skinFamilies As New List(Of List(Of String))(Me.theMdlFileData.theSkinFamilies.Count)
				//For i As Integer = 0 To Me.theMdlFileData.theSkinFamilies.Count - 1
				//	Dim aSkinFamily As List(Of Short)
				//	aSkinFamily = Me.theMdlFileData.theSkinFamilies(i)

				//	Dim textureFileNames As New List(Of String)(Me.theMdlFileData.skinReferenceCount)
				//	For j As Integer = 0 To Me.theMdlFileData.skinReferenceCount - 1
				//		Dim aTexture As SourceMdlTexture10
				//		aTexture = Me.theMdlFileData.theTextures(aSkinFamily(j))

				//		textureFileNames.Add(aTexture.theFileName)
				//	Next

				//	skinFamilies.Add(textureFileNames)
				//Next
				//======
				List<List<short>> processedSkinFamilies = null;
				if (MainCROWBAR.TheApp.Settings.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked)
				{
					processedSkinFamilies = GetSkinFamiliesOfChangedMaterials(theMdlFileData.theSkinFamilies);
				}
				else
				{
					processedSkinFamilies = theMdlFileData.theSkinFamilies;
				}

				List<List<string>> skinFamiliesOfTextureFileNames = new List<List<string>>(processedSkinFamilies.Count);
				int skinReferenceCount = processedSkinFamilies[0].Count;
				for (int i = 0; i < processedSkinFamilies.Count; i++)
				{
					List<short> aSkinFamily = processedSkinFamilies[i];

					List<string> textureFileNames = new List<string>(skinReferenceCount);
					for (int j = 0; j < skinReferenceCount; j++)
					{
						SourceMdlTexture10 aTexture = theMdlFileData.theTextures[aSkinFamily[j]];

						textureFileNames.Add(aTexture.theFileName);
					}

					skinFamiliesOfTextureFileNames.Add(textureFileNames);
				}

				if ((!MainCROWBAR.TheApp.Settings.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked) || (skinFamiliesOfTextureFileNames.Count > 1))
				{
					theOutputFileStreamWriter.WriteLine();

					line = "$texturegroup \"skinfamilies\"";
					theOutputFileStreamWriter.WriteLine(line);
					line = "{";
					theOutputFileStreamWriter.WriteLine(line);

					List<string> skinFamilyLines = GetTextureGroupSkinFamilyLines(skinFamiliesOfTextureFileNames);
					for (int skinFamilyLineIndex = 0; skinFamilyLineIndex < skinFamilyLines.Count; skinFamilyLineIndex++)
					{
						theOutputFileStreamWriter.WriteLine(skinFamilyLines[skinFamilyLineIndex]);
					}

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
		private void WriteSequenceOptions(SourceMdlSequenceDesc10 aSequenceDesc)
		{
			string line = "";

			for (int blendIndex = 0; blendIndex < aSequenceDesc.blendCount; blendIndex++)
			{
				if (aSequenceDesc.blendCount == 1)
				{
					aSequenceDesc.theSmdRelativePathFileNames[blendIndex] = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames[blendIndex], theModelName, aSequenceDesc.theName, -1);
				}
				else
				{
					aSequenceDesc.theSmdRelativePathFileNames[blendIndex] = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(aSequenceDesc.theSmdRelativePathFileNames[blendIndex], theModelName, aSequenceDesc.theName, blendIndex);
				}

				line = "\t";
				line += "\"";
				line += FileManager.GetPathFileNameWithoutExtension(aSequenceDesc.theSmdRelativePathFileNames[blendIndex]);
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}

			if (aSequenceDesc.activityId > 0)
			{
				string activityName = null;
				if (aSequenceDesc.activityId < SourceModule10.activityMap.Length)
				{
					activityName = SourceModule10.activityMap[aSequenceDesc.activityId];
				}
				else
				{
					activityName = "ACT_" + aSequenceDesc.activityId.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				}
				line = "\t";
				line += activityName;
				line += " ";
				line += aSequenceDesc.activityWeight.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			for (int i = 0; i <= 1; i++)
			{
				if (aSequenceDesc.blendType[i] != 0)
				{
					line = "\t";
					line += "blend ";
					line += SourceModule10.GetControlText(aSequenceDesc.blendType[i]);
					line += " ";
					line += aSequenceDesc.blendStart[i].ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aSequenceDesc.blendEnd[i].ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

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
					line += aSequenceDesc.theEvents[j].eventIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += frameIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					if (!string.IsNullOrEmpty(aSequenceDesc.theEvents[j].theOptions))
					{
						line += " \"";
						line += aSequenceDesc.theEvents[j].theOptions;
						line += "\"";
					}
					line += " }";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "\t";
			line += "fps ";
			line += aSequenceDesc.fps.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);

			if ((aSequenceDesc.flags & SourceMdlSequenceDesc10.STUDIO_LOOPING) > 0)
			{
				line = "\t";
				line += "loop";
				theOutputFileStreamWriter.WriteLine(line);
			}

			if (aSequenceDesc.motiontype > 0)
			{
				line = "\t";
				line += SourceModule10.GetMultipleControlText(aSequenceDesc.motiontype);
				theOutputFileStreamWriter.WriteLine(line);
			}

			if (aSequenceDesc.thePivots != null && aSequenceDesc.thePivots.Count > 0)
			{
				for (int pivotIndex = 0; pivotIndex < aSequenceDesc.thePivots.Count; pivotIndex++)
				{
					line = "\t";
					line += "pivot ";
					line += pivotIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aSequenceDesc.thePivots[0].pivotStart.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aSequenceDesc.thePivots[0].pivotEnd.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			WriteSequenceNodeInfo(aSequenceDesc);

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

		private void WriteSequenceNodeInfo(SourceMdlSequenceDesc10 aSeqDesc)
		{
			string line = "";

			if (aSeqDesc.entryNodeIndex > 0)
			{
				if (aSeqDesc.entryNodeIndex == aSeqDesc.exitNodeIndex)
				{
					//node (name)
					line = "\t";
					line += "node";
					line += " ";
					line += (aSeqDesc.entryNodeIndex).ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
				else if ((aSeqDesc.nodeFlags & 1) == 0)
				{
					//transition (from) (to) 
					line = "\t";
					line += "transition";
					line += " ";
					line += (aSeqDesc.entryNodeIndex).ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += (aSeqDesc.exitNodeIndex).ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
				else
				{
					//rtransition (name1) (name2) 
					line = "\t";
					line += "rtransition";
					line += " ";
					line += (aSeqDesc.entryNodeIndex).ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += (aSeqDesc.exitNodeIndex).ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteTexRenderModeLine(string textureFileName, string renderMode, bool lineIsCommented)
		{
			string line = "";

			if (lineIsCommented)
			{
				line = "//";
			}
			line += "$texrendermode ";
			line += "\"";
			line += textureFileName;
			line += "\"";
			line += " ";
			line += renderMode;
			theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Constants

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData10 theMdlFileData;
		private string theModelName;

		private string theOutputPath;
		private string theOutputFileNameWithoutExtension;

#endregion

	}

}