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
	public class SourceQcFile49 : SourceQcFile
	{
#region Creation and Destruction

		//Public Sub New(ByVal outputFileStream As StreamWriter, ByVal outputPathFileName As String, ByVal mdlFileData As SourceMdlFileData49, ByVal vtxFileData As SourceVtxFileData07, ByVal phyFileData As SourcePhyFileData, ByVal aniFileData As SourceAniFileData49, ByVal modelName As String)
		public SourceQcFile49(StreamWriter outputFileStream, string outputPathFileName, SourceMdlFileData49 mdlFileData, SourceVtxFileData07 vtxFileData, SourcePhyFileData phyFileData, string modelName)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			thePhyFileData = phyFileData;
			//Me.theAniFileData = aniFileData
			theVtxFileData = vtxFileData;
			theModelName = modelName;

			theOutputPath = FileManager.GetPath(outputPathFileName);
			theOutputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPathFileName);
		}

		public SourceQcFile49(StreamWriter outputFileStream, string outputPathFileName, SourceMdlFileData49 mdlFileData, string modelName)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			theModelName = modelName;

			theOutputPath = FileManager.GetPath(outputPathFileName);
			theOutputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPathFileName);
		}

		public SourceQcFile49(StreamWriter outputFileStream, SourceMdlFileData49 mdlFileData, string modelName)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			theModelName = modelName;
		}

#endregion

#region Delegates

		public delegate void WriteGroupDelegate();

#endregion

#region Methods

		public void WriteGroup(string qciGroupName, WriteGroupDelegate writeGroupAction, bool includeLineIsCommented, bool includeLineIsIndented)
		{
			if (MainCROWBAR.TheApp.Settings.DecompileGroupIntoQciFilesIsChecked)
			{
				string qciFileName = null;
				string qciPathFileName = null;
				StreamWriter mainOutputFileStream = theOutputFileStreamWriter;


				try
				{
					//qciPathFileName = Path.Combine(Me.theOutputPathName, Me.theOutputFileNameWithoutExtension + "_flexes.qci")
					qciFileName = theOutputFileNameWithoutExtension + "_" + qciGroupName + ".qci";
					qciPathFileName = Path.Combine(theOutputPath, qciFileName);

					theOutputFileStreamWriter = File.CreateText(qciPathFileName);

					//Me.WriteFlexLines()
					//Me.WriteFlexControllerLines()
					//Me.WriteFlexRuleLines()
					writeGroupAction.Invoke();
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					if (theOutputFileStreamWriter != null)
					{
						theOutputFileStreamWriter.Flush();
						theOutputFileStreamWriter.Close();

						theOutputFileStreamWriter = mainOutputFileStream;
					}
				}

				try
				{
					if (File.Exists(qciPathFileName))
					{
						FileInfo qciFileInfo = new FileInfo(qciPathFileName);
						if (qciFileInfo.Length > 0)
						{
							string line = "";

							theOutputFileStreamWriter.WriteLine();

							if (includeLineIsCommented)
							{
								line += "// ";
							}
							if (includeLineIsIndented)
							{
								line += "\t";
							}
							if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
							{
								line += "$Include ";
							}
							else
							{
								line += "$include ";
							}
							line += "\"";
							line += qciFileName;
							line += "\"";
							theOutputFileStreamWriter.WriteLine(line);
						}
					}
				}
				catch (Exception ex)
				{
					throw;
				}
			}
			else
			{
				//Me.WriteFlexLines()
				//Me.WriteFlexControllerLines()
				//Me.WriteFlexRuleLines()
				writeGroupAction.Invoke();
			}
		}

		public void WriteModelNameCommand()
		{
			string line = "";
			string modelPathFileName = theMdlFileData.theModelName;


			theOutputFileStreamWriter.WriteLine();

			//$modelname "survivors/survivor_producer.mdl"
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

		public void WriteUpAxisCommand()
		{
			if (theMdlFileData.theUpAxisYCommandWasUsed)
			{
				string line = "";

				theOutputFileStreamWriter.WriteLine();

				//$upaxis Y
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line += "$UpAxis Y";
				}
				else
				{
					line += "$upaxis Y";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteIncludeMainQcLine()
		{
			string line = "";

			theOutputFileStreamWriter.WriteLine();

			//$include "Rochelle_world.qci"
			if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
			{
				line += "$Include ";
			}
			else
			{
				line += "$include ";
			}
			line += "\"";
			line += "decompiled.qci";
			line += "\"";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(theOutputFileStreamWriter);
		}

		public void WriteStaticPropCommand()
		{
			string line = "";

			//$staticprop
			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$StaticProp";
				}
				else
				{
					line = "$staticprop";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteConstantDirectionalLightCommand()
		{
			string line = "";

			//$constantdirectionallight
			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_CONSTANT_DIRECTIONAL_LIGHT_DOT) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$ConstantDirectionalLight ";
				}
				else
				{
					line = "$constantdirectionallight ";
				}
				//FROM: studiomdl.cpp
				//g_constdirectionalightdot = (byte)( verify_atof(token) * 255.0f );
				line += (theMdlFileData.directionalLightDot / 255.0).ToString();
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		//Private Function GetModelPathFileName(ByVal aModel As SourceMdlModel) As String
		//	Dim pathFileName As String

		//	'NOTE: Use Path.GetFileName() to avoid writing relative-path file names: $model "TeenAngst" "../dmx/zoey_reference_wrinkle.dmx" {
		//	'NOTE: Avoid this example: 	replacemodel "../dmx/zoey_reference_wrinkle.dmx" "../dmx/zoey_reference_wrinkle.dmx_lod1"
		//	'line += CStr(theSourceEngineModel.theMdlFileData.theBodyParts(0).theModels(0).name).Trim(Chr(0))
		//	'line += Path.GetFileName(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
		//	'NOTE: In general, do not add the ".smd" because the MDL file will store it if it was compiled with it.
		//	'line += ".smd"
		//	pathFileName = Path.GetFileName(CStr(aModel.name).Trim(Chr(0)))

		//	'NOTE: Add the ".smd" when ends with ".dmx" or else the qc file won't be able to compile.
		//	'If modelFileName.EndsWith(".dmx") Then
		//	'	modelFileName += ".smd"
		//	'End If
		//	'------
		//	If Path.GetExtension(pathFileName) <> ".smd" Then
		//		pathFileName = Path.ChangeExtension(pathFileName, ".smd")
		//	End If

		//	Return pathFileName
		//End Function

		public void WriteModelCommand(SourceMdlBodyPart aBodyPart)
		{
			string line = "";
			//Dim aBodyPart As SourceMdlBodyPart
			int bodyPartIndex = 0;
			SourceMdlModel aBodyModel = null;
			List<string> eyeballNames;

			//$model "producer" "producer_model_merged.dmx.smd" {
			////-doesn't work     eyeball righteye ValveBiped.Bip01_Head1 -1.260 -0.086 64.594 eyeball_r 1.050  3.000 producer_head 0.530
			////-doesn't work     eyeball lefteye ValveBiped.Bip01_Head1 1.260 -0.086 64.594 eyeball_l 1.050  -3.000 producer_head 0.530
			//     mouth 0 "mouth"  ValveBiped.Bip01_Head1 0.000 1.000 0.000
			//}
			//If Me.theMdlFileData.theModelCommandIsUsed AndAlso Me.theMdlFileData.theBodyParts IsNot Nothing AndAlso Me.theMdlFileData.theBodyParts.Count > 0 Then
			eyeballNames = new List<string>();

			//aBodyPart = Me.theMdlFileData.theBodyParts(Me.theMdlFileData.theBodyPartIndexThatShouldUseModelCommand)
			aBodyModel = aBodyPart.theModels[0];
			//aBodyModel.theSmdFileNames(0) = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames(0), Me.theMdlFileData.theBodyPartIndexThatShouldUseModelCommand, 0, 0, Me.theModelName, aBodyPart.theModels(0).name)
			bodyPartIndex = theMdlFileData.theBodyParts.IndexOf(aBodyPart);
			aBodyModel.theSmdFileNames[0] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[0], bodyPartIndex, 0, 0, theModelName, new string(aBodyPart.theModels[0].name));

			if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
			{
				line = "$Model ";
			}
			else
			{
				line = "$model ";
			}
			line += "\"";
			line += aBodyPart.theName;
			line += "\" \"";
			line += aBodyModel.theSmdFileNames[0];
			line += "\"";

			line += " {";
			theOutputFileStreamWriter.WriteLine(line);

			//NOTE: Must call WriteEyeballLines() before WriteEyelidLines(), because eyeballNames are created in first and sent to other.
			WriteEyeballLines(aBodyPart, ref eyeballNames);
			WriteEyelidLines(aBodyPart, eyeballNames);

			if (bodyPartIndex == 0)
			{
				WriteMouthLines();
			}

			theBodyPartForFlexWriting = aBodyPart;
			WriteGroup("flex", WriteGroupFlex, false, true);

			line = "}";
			theOutputFileStreamWriter.WriteLine(line);
			//End If
		}

		private void WriteEyeballLines(SourceMdlBodyPart aBodyPart, ref List<string> eyeballNames)
		{
			string line = "";
			//Dim aBodyPart As SourceMdlBodyPart
			SourceMdlModel aModel = null;
			SourceMdlEyeball anEyeball = null;
			string eyeballTextureName = null;
			double diameter = 0;
			double angle = 0;
			double irisScale = 0;
			SourceVector poseToBone0 = null;
			SourceVector poseToBone1 = null;
			SourceVector poseToBone2 = null;
			SourceVector poseToBone3 = null;
			SourceVector eyeballPosition = null;

			poseToBone0 = new SourceVector();
			poseToBone1 = new SourceVector();
			poseToBone2 = new SourceVector();
			poseToBone3 = new SourceVector();
			eyeballPosition = new SourceVector();

			try
			{
				//eyeball righteye ValveBiped.Bip01_Head1 -1.160 -3.350 62.600 teenangst_eyeball_r 1.000  3.000 zoey_color 0.630
				//eyeball lefteye ValveBiped.Bip01_Head1 1.160 -3.350 62.600 teenangst_eyeball_l 1.000  -3.000 zoey_color 0.630
				//aBodyPart = Me.theMdlFileData.theBodyParts(0)
				if (aBodyPart.theModels != null && aBodyPart.theModels.Count > 0)
				{
					aModel = aBodyPart.theModels[0];
					if (aModel.theEyeballs != null && aModel.theEyeballs.Count > 0)
					{
						line = "";
						theOutputFileStreamWriter.WriteLine(line);

						for (int eyeballIndex = 0; eyeballIndex < aModel.theEyeballs.Count; eyeballIndex++)
						{
							anEyeball = aModel.theEyeballs[eyeballIndex];

							//eyeballPosition.x = CSng(Math.Round(anEyeball.org.x, 3))
							//eyeballPosition.y = CSng(Math.Round(anEyeball.org.y, 3))
							//eyeballPosition.z = CSng(Math.Round(anEyeball.org.z, 3))
							//======
							//DONE: Transform vertices from Pose to Bone space, i.e. reverse these operations.
							//FROM: studiomdl.cpp
							//For boneToPose[]:
							//AngleMatrix( psource->rawanim[0][i].rot, m );
							//m[0][3] = psource->rawanim[0][i].pos[0];
							//m[1][3] = psource->rawanim[0][i].pos[1];
							//m[2][3] = psource->rawanim[0][i].pos[2];
							//// translate eyeball into bone space
							//VectorITransform( tmp, pmodel->source->boneToPose[eyeball->bone], eyeball->org );
							//------
							// WORKS!
							SourceMdlBone aBone = theMdlFileData.theBones[anEyeball.boneIndex];
							//AngleMatrix(aBone.rotationX, aBone.rotationY, aBone.rotationZ, poseToBone0, poseToBone1, poseToBone2)
							//poseToBone3.x = -aBone.positionX
							//poseToBone3.y = -aBone.positionY
							//poseToBone3.z = -aBone.positionZ
							//eyeballPosition = MathModule.VectorITransform(anEyeball.org, poseToBone0, poseToBone1, poseToBone2, poseToBone3)
							eyeballPosition = MathModule.VectorITransform(anEyeball.org, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3);

							//FROM: studiomdl.cpp
							//eyeball->radius = verify_atof (token) / 2.0;
							diameter = anEyeball.radius * 2;
							//FROM: studiomdl.cpp
							//eyeball->zoffset = tan( DEG2RAD( verify_atof (token) ) );
							angle = Math.Round(MathModule.RadiansToDegrees(Math.Atan(anEyeball.zOffset)), 6);
							//FROM: studiomdl.cpp
							//eyeball->iris_scale = 1.0 / verify_atof( token );
							irisScale = 1 / anEyeball.irisScale;

							//NOTE: The mdl file does not store the eyeball name; studiomdl uses name once for checking eyelid info.
							//      So, just use an arbitrary name and guess which eyeball goes with which eyelid.
							//      Typically, there are only two eyeballs and right one has angle > 0 and left one has angle < 0.
							if (eyeballIndex == 0 && angle > 0)
							{
								eyeballNames.Add("eye_right");
							}
							else if (eyeballIndex == 1 && angle < 0)
							{
								eyeballNames.Add("eye_left");
							}
							else
							{
								eyeballNames.Add("eye_" + eyeballIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat));
							}

							if (anEyeball.theTextureIndex == -1)
							{
								eyeballTextureName = "[unknown_texture]";
								//NOTE: Use texture name from a different eyeball because this eyeball's texture index was not stored in the MDL file.
								for (int i = 0; i < aModel.theEyeballs.Count; i++)
								{
									if (i == eyeballIndex)
									{
										continue;
									}
									else if (aModel.theEyeballs[i].theTextureIndex > -1)
									{
										eyeballTextureName = theMdlFileData.theModifiedTextureFileNames[aModel.theEyeballs[i].theTextureIndex];
										break;
									}
								}
							}
							else
							{
								//eyeballTextureName = Me.theMdlFileData.theTextures(anEyeball.theTextureIndex).thePathFileName
								eyeballTextureName = theMdlFileData.theModifiedTextureFileNames[anEyeball.theTextureIndex];
							}

							line = "\t";
							line += "eyeball \"";
							line += eyeballNames[eyeballIndex];
							line += "\" \"";
							line += theMdlFileData.theBones[anEyeball.boneIndex].theName;
							line += "\" ";
							line += eyeballPosition.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += eyeballPosition.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += eyeballPosition.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " \"";
							line += eyeballTextureName;
							line += "\" ";
							line += diameter.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += angle.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							//Unused in later Source Engine versions.
							line += "\"iris_unused\"";
							line += " ";
							line += Math.Round(irisScale, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
							theOutputFileStreamWriter.WriteLine(line);

							//NOTE: Used to write frame indexes for eyelid lines and prevent eyelid flexes from being written in flex list in qc file.
							theMdlFileData.theFlexDescs[anEyeball.upperLidFlexDesc].theDescIsUsedByEyelid = true;
							theMdlFileData.theFlexDescs[anEyeball.lowerLidFlexDesc].theDescIsUsedByEyelid = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			//Me.CreateListOfEyelidFlexFrameIndexes()
		}

		//Private Sub CreateListOfEyelidFlexFrameIndexes()
		//	Dim aFlexFrame As FlexFrame

		//	Me.theMdlFileData.theEyelidFlexFrameIndexes = New List(Of Integer)()
		//	For frameIndex As Integer = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
		//		aFlexFrame = Me.theMdlFileData.theFlexFrames(frameIndex)
		//		If Not Me.theMdlFileData.theEyelidFlexFrameIndexes.Contains(frameIndex) Then
		//			If Me.theMdlFileData.theFlexDescs(aFlexFrame.flexes(0).flexDescIndex).theDescIsUsedByEyelid Then
		//				Me.theMdlFileData.theEyelidFlexFrameIndexes.Add(frameIndex)
		//			End If
		//		End If
		//	Next
		//End Sub

		private void WriteEyelidLines(SourceMdlBodyPart aBodyPart, List<string> eyeballNames)
		{
			string line = "";
			//Dim aBodyPart As SourceMdlBodyPart
			int bodyPartIndex = 0;
			SourceMdlModel aModel = null;
			SourceMdlEyeball anEyeball = null;
			int frameIndex = 0;
			string eyelidName = null;
			FlexFrame aFlexFrame = null;

			try
			{
				bodyPartIndex = theMdlFileData.theBodyParts.IndexOf(aBodyPart);
				// Write eyelid options.
				//$definevariable expressions "zoeyp.vta"
				//eyelid  upper_right $expressions$ lowerer 1 -0.19 neutral 0 0.13 raiser 2 0.27 split 0.1 eyeball righteye
				//eyelid  lower_right $expressions$ lowerer 3 -0.32 neutral 0 -0.19 raiser 4 -0.02 split 0.1 eyeball righteye
				//eyelid  upper_left $expressions$ lowerer 1 -0.19 neutral 0 0.13 raiser 2 0.27 split -0.1 eyeball lefteye
				//eyelid  lower_left $expressions$ lowerer 3 -0.32 neutral 0 -0.19 raiser 4 -0.02 split -0.1 eyeball lefteye
				//aBodyPart = Me.theMdlFileData.theBodyParts(0)
				//If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 AndAlso Me.theMdlFileData.theEyelidFlexFrameIndexes.Count > 0 Then
				if (aBodyPart.theModels != null && aBodyPart.theModels.Count > 0)
				{
					aModel = aBodyPart.theModels[0];
					if (aModel.theEyeballs != null && aModel.theEyeballs.Count > 0)
					{
						line = "";
						theOutputFileStreamWriter.WriteLine(line);

						//frameIndex = 0
						for (int eyeballIndex = 0; eyeballIndex < aModel.theEyeballs.Count; eyeballIndex++)
						{
							anEyeball = aModel.theEyeballs[eyeballIndex];

							if (anEyeball.upperLidFlexDesc == anEyeball.upperFlexDesc[0])
							{
								//NOTE: This means the eyeball uses "dummy_eyelid", made via DMX file.
								//      Comparing the two flexDescs seemed better than comparing flexDesc.theName with "dummy_eyelid", because the flexDescs can't be faked without hex-editing the MDL.
								continue;
							}

							//If frameIndex + 3 >= Me.theMdlFileData.theEyelidFlexFrameIndexes.Count Then
							//	frameIndex = 0
							//End If
							//TODO: For "Crowbar Bug Reports\2017-11-25\[...]\tfa_ow_mercy.mdl", theName is blank, but it should be "upper_right", because 3 flex rules use it (%upper_right_raiser, %upper_right_neutral, %upper_right_lowerer).
							//      Not sure how the model was compiled without the name, but maybe it was compiled via DMX instead of SMD.
							//      POSSIBLE FIX: If empty, then assign correct name.
							eyelidName = theMdlFileData.theFlexDescs[anEyeball.upperLidFlexDesc].theName;

							line = "\t";
							line += "eyelid ";
							line += eyelidName;
							//line += " """
							//line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
							//line += ".vta"" "
							line += " \"";
							line += SourceFileNamesModule.GetVtaFileName(theModelName, bodyPartIndex);
							line += "\" ";
							line += "lowerer ";
							//TODO: The frame indexes here and for raiser need correcting.
							//line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(0)).theVtaFrameIndex.ToString()
							//TEST:
							//line += anEyeball.upperFlexDesc(0).ToString()
							//TEST:
							//line += Me.theMdlFileData.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
							//frameIndex += 1
							//NOTE: Start at index 1 because defaultflex frame is at index 0.
							frameIndex = 0;
							//For flexFrameIndex As Integer = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
							//	aFlexFrame = Me.theMdlFileData.theFlexFrames(flexFrameIndex)
							for (int flexFrameIndex = 1; flexFrameIndex < aBodyPart.theFlexFrames.Count; flexFrameIndex++)
							{
								aFlexFrame = aBodyPart.theFlexFrames[flexFrameIndex];
								if (aFlexFrame.flexName == eyelidName || (aFlexFrame.flexHasPartner && aFlexFrame.flexPartnerName == eyelidName))
								{
									if (aFlexFrame.flexes[0].target0 == -11)
									{
										frameIndex = flexFrameIndex;
										break;
									}
								}
							}
							line += frameIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += anEyeball.upperTarget[0].ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += "neutral 0";
							//line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(1)).theVtaFrameIndex.ToString()
							line += " ";
							line += anEyeball.upperTarget[1].ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += "raiser ";
							//line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(2)).theVtaFrameIndex.ToString()
							//TEST:
							//line += anEyeball.upperFlexDesc(2).ToString()
							//TEST:
							//line += Me.theMdlFileData.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
							//frameIndex += 1
							//NOTE: Start at index 1 because defaultflex frame is at index 0.
							frameIndex = 0;
							//For flexFrameIndex As Integer = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
							//	aFlexFrame = Me.theMdlFileData.theFlexFrames(flexFrameIndex)
							for (int flexFrameIndex = 1; flexFrameIndex < aBodyPart.theFlexFrames.Count; flexFrameIndex++)
							{
								aFlexFrame = aBodyPart.theFlexFrames[flexFrameIndex];
								if (aFlexFrame.flexName == eyelidName || (aFlexFrame.flexHasPartner && aFlexFrame.flexPartnerName == eyelidName))
								{
									if (aFlexFrame.flexes[0].target3 == 11)
									{
										frameIndex = flexFrameIndex;
										break;
									}
								}
							}
							line += frameIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += anEyeball.upperTarget[2].ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += "split ";
							//TODO: simplify.cpp RemapVertexAnimations(); probably should call SourceMdlFile.GetSplit()?
							line += GetSplitNumber(eyelidName);
							line += " eyeball \"";
							line += eyeballNames[eyeballIndex];
							line += "\"";
							theOutputFileStreamWriter.WriteLine(line);

							theMdlFileData.theFlexDescs[anEyeball.upperLidFlexDesc].theDescIsUsedByFlex = true;
							theMdlFileData.theFlexDescs[anEyeball.upperFlexDesc[0]].theDescIsUsedByFlex = true;
							theMdlFileData.theFlexDescs[anEyeball.upperFlexDesc[1]].theDescIsUsedByFlex = true;
							theMdlFileData.theFlexDescs[anEyeball.upperFlexDesc[2]].theDescIsUsedByFlex = true;

							eyelidName = theMdlFileData.theFlexDescs[anEyeball.lowerLidFlexDesc].theName;

							line = "\t";
							line += "eyelid ";
							line += eyelidName;
							//line += " """
							//line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
							//line += ".vta"" "
							line += " \"";
							line += SourceFileNamesModule.GetVtaFileName(theModelName, bodyPartIndex);
							line += "\" ";
							line += "lowerer ";
							//line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(0)).theVtaFrameIndex.ToString()
							//TEST:
							//line += anEyeball.lowerFlexDesc(0).ToString()
							//TEST:
							//line += Me.theMdlFileData.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
							//frameIndex += 1
							//NOTE: Start at index 1 because defaultflex frame is at index 0.
							frameIndex = 0;
							//For flexFrameIndex As Integer = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
							//	aFlexFrame = Me.theMdlFileData.theFlexFrames(flexFrameIndex)
							for (int flexFrameIndex = 1; flexFrameIndex < aBodyPart.theFlexFrames.Count; flexFrameIndex++)
							{
								aFlexFrame = aBodyPart.theFlexFrames[flexFrameIndex];
								if (aFlexFrame.flexName == eyelidName || (aFlexFrame.flexHasPartner && aFlexFrame.flexPartnerName == eyelidName))
								{
									if (aFlexFrame.flexes[0].target0 == -11)
									{
										frameIndex = flexFrameIndex;
										break;
									}
								}
							}
							line += frameIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += anEyeball.lowerTarget[0].ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += "neutral 0";
							//line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(1)).theVtaFrameIndex.ToString()
							line += " ";
							line += anEyeball.lowerTarget[1].ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += "raiser ";
							//line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(2)).theVtaFrameIndex.ToString()
							//TEST:
							//line += anEyeball.lowerFlexDesc(2).ToString()
							//TEST:
							//line += Me.theMdlFileData.theEyelidFlexFrameIndexes(frameIndex).ToString(TheApp.InternalNumberFormat)
							//frameIndex += 1
							//NOTE: Start at index 1 because defaultflex frame is at index 0.
							frameIndex = 0;
							//For flexFrameIndex As Integer = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
							//	aFlexFrame = Me.theMdlFileData.theFlexFrames(flexFrameIndex)
							for (int flexFrameIndex = 1; flexFrameIndex < aBodyPart.theFlexFrames.Count; flexFrameIndex++)
							{
								aFlexFrame = aBodyPart.theFlexFrames[flexFrameIndex];
								if (aFlexFrame.flexName == eyelidName || (aFlexFrame.flexHasPartner && aFlexFrame.flexPartnerName == eyelidName))
								{
									if (aFlexFrame.flexes[0].target3 == 11)
									{
										frameIndex = flexFrameIndex;
										break;
									}
								}
							}
							line += frameIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += anEyeball.lowerTarget[2].ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += "split ";
							//TODO: simplify.cpp RemapVertexAnimations(); probably should call SourceMdlFile.GetSplit()?
							line += GetSplitNumber(eyelidName);
							line += " eyeball \"";
							line += eyeballNames[eyeballIndex];
							line += "\"";
							theOutputFileStreamWriter.WriteLine(line);

							theMdlFileData.theFlexDescs[anEyeball.lowerLidFlexDesc].theDescIsUsedByFlex = true;
							theMdlFileData.theFlexDescs[anEyeball.lowerFlexDesc[0]].theDescIsUsedByFlex = true;
							theMdlFileData.theFlexDescs[anEyeball.lowerFlexDesc[1]].theDescIsUsedByFlex = true;
							theMdlFileData.theFlexDescs[anEyeball.lowerFlexDesc[2]].theDescIsUsedByFlex = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private string GetSplitNumber(string eyelidName)
		{
			if (eyelidName.Contains("right"))
			{
				return "1";
			}
			else if (eyelidName.Contains("left"))
			{
				return "-1";
			}
			else
			{
				return "0";
			}
		}

		private void WriteMouthLines()
		{
			string line = "";
			double offsetX = 0;
			double offsetY = 0;
			double offsetZ = 0;

			//NOTE: Writes out mouth line correctly for teenangst zoey.
			if (theMdlFileData.theMouths != null && theMdlFileData.theMouths.Count > 0)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.theMouths.Count; i++)
				{
					SourceMdlMouth aMouth = theMdlFileData.theMouths[i];
					offsetX = Math.Round(aMouth.forward.x, 3);
					offsetY = Math.Round(aMouth.forward.y, 3);
					offsetZ = Math.Round(aMouth.forward.z, 3);

					line = "\t";
					line += "mouth ";
					line += i.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " \"";
					line += theMdlFileData.theFlexDescs[aMouth.flexDescIndex].theName;
					line += "\" \"";
					line += theMdlFileData.theBones[aMouth.boneIndex].theName;
					line += "\" ";
					line += offsetX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += offsetY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += offsetZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);

					theMdlFileData.theFlexDescs[aMouth.flexDescIndex].theDescIsUsedByFlex = true;
				}
			}
		}

		private void WriteGroupFlex()
		{
			if (theBodyPartForFlexWriting.theFlexFrames != null && theBodyPartForFlexWriting.theFlexFrames.Count > 1)
			{
				WriteFlexLines();
				WriteFlexControllerLines();
				WriteFlexRuleLines();
			}
		}

		private void WriteFlexLines()
		{
			string line = "";

			// Write flexfile (contains flexDescs).
			//If Me.theMdlFileData.theFlexFrames IsNot Nothing AndAlso Me.theMdlFileData.theFlexFrames.Count > 0 Then
			//NOTE: Count > 1 to avoid writing just a defaultflex frame.
			//If Me.theBodyPartForFlexWriting.theFlexFrames IsNot Nothing AndAlso Me.theBodyPartForFlexWriting.theFlexFrames.Count > 1 Then
			int bodyPartIndex = theMdlFileData.theBodyParts.IndexOf(theBodyPartForFlexWriting);

			line = "";
			theOutputFileStreamWriter.WriteLine(line);

			line = "\t";
			line += "flexfile";
			//line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
			//line += ".vta"""
			line += " \"";
			line += SourceFileNamesModule.GetVtaFileName(theModelName, bodyPartIndex);
			line += "\" ";
			theOutputFileStreamWriter.WriteLine(line);

			line = "\t";
			line += "{";
			theOutputFileStreamWriter.WriteLine(line);

			//======
			line = "\t";
			line += "\t";
			line += "defaultflex frame 0";
			theOutputFileStreamWriter.WriteLine(line);

			//NOTE: Start at index 1 because defaultflex frame is at index 0.
			FlexFrame aFlexFrame = null;
			//For frameIndex As Integer = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
			//	aFlexFrame = Me.theMdlFileData.theFlexFrames(frameIndex)
			for (int frameIndex = 1; frameIndex < theBodyPartForFlexWriting.theFlexFrames.Count; frameIndex++)
			{
				aFlexFrame = theBodyPartForFlexWriting.theFlexFrames[frameIndex];
				line = "\t";
				line += "\t";
				if (theMdlFileData.theFlexDescs[aFlexFrame.flexes[0].flexDescIndex].theDescIsUsedByEyelid)
				{
					line += "// Already in eyelid lines: ";
				}
				if (aFlexFrame.flexHasPartner)
				{
					line += "flexpair \"";
					line += aFlexFrame.flexName.Substring(0, aFlexFrame.flexName.Length - 1);
				}
				else
				{
					line += "flex \"";
					line += aFlexFrame.flexName;
				}
				line += "\"";
				if (aFlexFrame.flexHasPartner)
				{
					line += " ";
					line += aFlexFrame.flexSplit.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				line += " frame ";
				line += frameIndex.ToString();
				theOutputFileStreamWriter.WriteLine(line);
			}
			//======
			//Dim aBodyPart As SourceMdlBodyPart
			//Dim aModel As SourceMdlModel
			//Dim frameIndex As Integer
			//Dim flexDescHasBeenWritten As List(Of Integer)
			//Dim meshVertexIndexStart As Integer
			//frameIndex = 0
			//flexDescHasBeenWritten = New List(Of Integer)

			//line = vbTab
			//line += "defaultflex frame "
			//line += frameIndex.ToString()
			//Me.theOutputFileStreamWriter.WriteLine(line)

			//For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
			//	aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

			//	If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
			//		For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
			//			aModel = aBodyPart.theModels(modelIndex)

			//			If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
			//				For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
			//					Dim aMesh As SourceMdlMesh
			//					aMesh = aModel.theMeshes(meshIndex)

			//					meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

			//					If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
			//						For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
			//							Dim aFlex As SourceMdlFlex
			//							aFlex = aMesh.theFlexes(flexIndex)

			//							If flexDescHasBeenWritten.Contains(aFlex.flexDescIndex) Then
			//								Continue For
			//							Else
			//								flexDescHasBeenWritten.Add(aFlex.flexDescIndex)
			//							End If

			//							line = vbTab
			//							Dim aFlexDescPartnerIndex As Integer
			//							'Dim aFlexPartner As SourceMdlFlex
			//							aFlexDescPartnerIndex = aMesh.theFlexes(flexIndex).flexDescPartnerIndex
			//							If aFlexDescPartnerIndex > 0 Then
			//								'aFlexPartner = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexDescPartnerIndex)
			//								If Not flexDescHasBeenWritten.Contains(aFlex.flexDescPartnerIndex) Then
			//									flexDescHasBeenWritten.Add(aFlex.flexDescPartnerIndex)
			//								End If
			//								line += "flexpair """
			//								Dim flexName As String
			//								flexName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			//								line += flexName.Remove(flexName.Length - 1, 1)
			//								line += """"
			//								line += " "
			//								line += Me.GetSplit(aFlex, meshVertexIndexStart).ToString("0.######", TheApp.InternalNumberFormat)

			//								theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theDescIsUsedByFlex = True
			//								theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
			//							Else
			//								line += "flex """
			//								line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
			//								line += """"

			//								theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theDescIsUsedByFlex = True
			//							End If
			//							line += " frame "
			//							'NOTE: Start at second frame because first frame is "basis" frame.
			//							frameIndex += 1
			//							line += frameIndex.ToString()
			//							'line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex.ToString()
			//							Me.theOutputFileStreamWriter.WriteLine(line)
			//						Next
			//					End If
			//				Next
			//			End If
			//		Next
			//	End If
			//Next

			line = "\t";
			line += "}";
			theOutputFileStreamWriter.WriteLine(line);
			//End If
		}

		private void WriteFlexControllerLines()
		{
			string line = "";

			if (theMdlFileData.theFlexControllers != null && theMdlFileData.theFlexControllers.Count > 0)
			{
				SourceMdlFlexController aFlexController = null;

				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.theFlexControllers.Count; i++)
				{
					aFlexController = theMdlFileData.theFlexControllers[i];

					if (aFlexController.theType == "eyes" && (aFlexController.theName == "eyes_updown" || aFlexController.theName == "eyes_rightleft"))
					{
						if (!theBodyPartForFlexWriting.theEyeballOptionIsUsed)
						{
							continue;
						}
					}

					line = "\t";
					line += "flexcontroller ";
					line += aFlexController.theType;
					line += " ";
					line += "range ";
					line += aFlexController.min.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aFlexController.max.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " \"";
					line += aFlexController.theName;
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteFlexRuleLines()
		{
			string line = "";

			if (theMdlFileData.theFlexRules != null && theMdlFileData.theFlexRules.Count > 0)
			{
				SourceMdlFlexRule aFlexRule = null;

				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.theFlexDescs.Count; i++)
				{
					SourceMdlFlexDesc flexDesc = theMdlFileData.theFlexDescs[i];

					if (!flexDesc.theDescIsUsedByFlex && flexDesc.theDescIsUsedByFlexRule)
					{
						line = "\t";
						line += "localvar ";
						line += flexDesc.theName;
						theOutputFileStreamWriter.WriteLine(line);
					}
				}

				for (int i = 0; i < theMdlFileData.theFlexRules.Count; i++)
				{
					aFlexRule = theMdlFileData.theFlexRules[i];
					//line = Me.GetFlexRule(aFlexRule)
					line = Common.GetFlexRule(theMdlFileData.theFlexDescs, theMdlFileData.theFlexControllers, aFlexRule);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		//#define clamp(val, min, max) (((val) > (max)) ? (max) : (((val) < (min)) ? (min) : (val)))
		private double Clamp(double val, double min, double max)
		{
			if (val > max)
			{
				return max;
			}
			else if (val < min)
			{
				return min;
			}
			else
			{
				return val;
			}
		}

		//inline float RemapValClamped( float val, float A, float B, float C, float D)
		//{
		//	if ( A == B )
		//		return val >= B ? D : C;
		//	float cVal = (val - A) / (B - A);
		//	cVal = clamp( cVal, 0.0f, 1.0f );

		//	return C + (D - C) * cVal;
		//}
		private double RemapValClamped(double val, double A, double B, double C, double D)
		{
			if (A == B)
			{
				return 0;
			}

			double cVal = (val - A) / (B - A);
			cVal = Clamp(cVal, 0.0F, 1.0F);

			return C + (D - C) * cVal;
		}

		//Private Function GetFlexRule(ByVal aFlexRule As SourceMdlFlexRule) As String
		//	Dim flexRuleEquation As String
		//	flexRuleEquation = vbTab
		//	flexRuleEquation += "%"
		//	flexRuleEquation += Me.theMdlFileData.theFlexDescs(aFlexRule.flexIndex).theName
		//	flexRuleEquation += " = "
		//	If aFlexRule.theFlexOps IsNot Nothing AndAlso aFlexRule.theFlexOps.Count > 0 Then
		//		Dim aFlexOp As SourceMdlFlexOp

		//		' Convert to infix notation.

		//		Dim stack As Stack(Of IntermediateExpression) = New Stack(Of IntermediateExpression)()
		//		Dim rightExpr As String
		//		Dim leftExpr As String

		//		For i As Integer = 0 To aFlexRule.theFlexOps.Count - 1
		//			aFlexOp = aFlexRule.theFlexOps(i)
		//			If aFlexOp.op = SourceMdlFlexOp.STUDIO_CONST Then
		//				stack.Push(New IntermediateExpression(Math.Round(aFlexOp.value, 6).ToString("0.######", TheApp.InternalNumberFormat), 10))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH1 Then
		//				'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index)->localToGlobal;
		//				'stack[k] = src[m];
		//				'k++; 
		//				stack.Push(New IntermediateExpression(Me.theMdlFileData.theFlexControllers(aFlexOp.index).theName, 10))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH2 Then
		//				stack.Push(New IntermediateExpression("%" + Me.theMdlFileData.theFlexDescs(aFlexOp.index).theName, 10))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_ADD Then
		//				Dim rightIntermediate As IntermediateExpression = stack.Pop()
		//				Dim leftIntermediate As IntermediateExpression = stack.Pop()

		//				Dim newExpr As String = Convert.ToString(leftIntermediate.theExpression) + " + " + Convert.ToString(rightIntermediate.theExpression)
		//				stack.Push(New IntermediateExpression(newExpr, 1))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_SUB Then
		//				Dim rightIntermediate As IntermediateExpression = stack.Pop()
		//				Dim leftIntermediate As IntermediateExpression = stack.Pop()

		//				Dim newExpr As String = Convert.ToString(leftIntermediate.theExpression) + " - " + Convert.ToString(rightIntermediate.theExpression)
		//				stack.Push(New IntermediateExpression(newExpr, 1))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MUL Then
		//				Dim rightIntermediate As IntermediateExpression = stack.Pop()
		//				If rightIntermediate.thePrecedence < 2 Then
		//					rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
		//				Else
		//					rightExpr = rightIntermediate.theExpression
		//				End If

		//				Dim leftIntermediate As IntermediateExpression = stack.Pop()
		//				If leftIntermediate.thePrecedence < 2 Then
		//					leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
		//				Else
		//					leftExpr = leftIntermediate.theExpression
		//				End If

		//				Dim newExpr As String = leftExpr + " * " + rightExpr
		//				stack.Push(New IntermediateExpression(newExpr, 2))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DIV Then
		//				Dim rightIntermediate As IntermediateExpression = stack.Pop()
		//				If rightIntermediate.thePrecedence < 2 Then
		//					rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
		//				Else
		//					rightExpr = rightIntermediate.theExpression
		//				End If

		//				Dim leftIntermediate As IntermediateExpression = stack.Pop()
		//				If leftIntermediate.thePrecedence < 2 Then
		//					leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
		//				Else
		//					leftExpr = leftIntermediate.theExpression
		//				End If

		//				Dim newExpr As String = leftExpr + " / " + rightExpr
		//				stack.Push(New IntermediateExpression(newExpr, 2))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_NEG Then
		//				Dim rightIntermediate As IntermediateExpression = stack.Pop()

		//				Dim newExpr As String = "-" + rightIntermediate.theExpression
		//				stack.Push(New IntermediateExpression(newExpr, 10))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_EXP Then
		//				Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_OPEN Then
		//				Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_CLOSE Then
		//				Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_COMMA Then
		//				Dim ignoreThisOpBecauseItIsMistakeToBeHere As Integer = 4242
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MAX Then
		//				Dim rightIntermediate As IntermediateExpression = stack.Pop()
		//				If rightIntermediate.thePrecedence < 5 Then
		//					rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
		//				Else
		//					rightExpr = rightIntermediate.theExpression
		//				End If

		//				Dim leftIntermediate As IntermediateExpression = stack.Pop()
		//				If leftIntermediate.thePrecedence < 5 Then
		//					leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
		//				Else
		//					leftExpr = leftIntermediate.theExpression
		//				End If

		//				Dim newExpr As String = " max(" + leftExpr + ", " + rightExpr + ")"
		//				stack.Push(New IntermediateExpression(newExpr, 5))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_MIN Then
		//				Dim rightIntermediate As IntermediateExpression = stack.Pop()
		//				If rightIntermediate.thePrecedence < 5 Then
		//					rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")"
		//				Else
		//					rightExpr = rightIntermediate.theExpression
		//				End If

		//				Dim leftIntermediate As IntermediateExpression = stack.Pop()
		//				If leftIntermediate.thePrecedence < 5 Then
		//					leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")"
		//				Else
		//					leftExpr = leftIntermediate.theExpression
		//				End If

		//				Dim newExpr As String = " min(" + leftExpr + ", " + rightExpr + ")"
		//				stack.Push(New IntermediateExpression(newExpr, 5))
		//				'TODO: SourceMdlFlexOp.STUDIO_2WAY_0
		//				'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_2WAY_0 Then
		//				'	'	'#define STUDIO_2WAY_0	15	// Fetch a value from a 2 Way slider for the 1st value RemapVal( 0.0, 0.5, 0.0, 1.0 )
		//				'	'	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
		//				'	'	'stack[ k ] = RemapValClamped( src[m], -1.0f, 0.0f, 1.0f, 0.0f );
		//				'	'	'k++; 
		//				'	Dim newExpression As String
		//				'	'newExpression = CStr(Me.RemapValClamped(aFlexOp.value, -1, 0, 1, 0))
		//				'	newExpression = "RemapValClamped(" + theSourceEngineModel.theMdlFileHeader.theFlexControllers(aFlexOp.index).theName + ", -1, 0, 1, 0)"
		//				'	stack.Push(New IntermediateExpression(newExpression, 5))
		//				'TODO: SourceMdlFlexOp.STUDIO_2WAY_1
		//				'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_2WAY_1 Then
		//				'	'#define STUDIO_2WAY_1	16	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
		//				'	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
		//				'	'stack[ k ] = RemapValClamped( src[m], 0.0f, 1.0f, 0.0f, 1.0f );
		//				'	'k++; 
		//				'	Dim newExpression As String
		//				'	'newExpression = CStr(Me.RemapValClamped(aFlexOp.value, 0, 1, 0, 1))
		//				'	newExpression = "RemapValClamped(" + theSourceEngineModel.theMdlFileHeader.theFlexControllers(aFlexOp.index).theName + ", 0, 1, 0, 1)"
		//				'	stack.Push(New IntermediateExpression(newExpression, 5))
		//				'TODO: SourceMdlFlexOp.STUDIO_NWAY
		//				'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_NWAY Then
		//				'	Dim x As Integer = 42
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_COMBO Then
		//				'#define STUDIO_COMBO	18	// Perform a combo operation (essentially multiply the last N values on the stack)
		//				'int m = pops->d.index;
		//				'int km = k - m;
		//				'for ( int i = km + 1; i < k; ++i )
		//				'{
		//				'	stack[ km ] *= stack[ i ];
		//				'}
		//				'k = k - m + 1;
		//				Dim count As Integer
		//				Dim newExpression As String
		//				Dim intermediateExp As IntermediateExpression
		//				count = aFlexOp.index
		//				newExpression = ""
		//				intermediateExp = stack.Pop()
		//				newExpression += intermediateExp.theExpression
		//				For j As Integer = 2 To count
		//					intermediateExp = stack.Pop()
		//					newExpression += " * " + intermediateExp.theExpression
		//				Next
		//				newExpression = "(" + newExpression + ")"
		//				stack.Push(New IntermediateExpression(newExpression, 5))
		//			ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DOMINATE Then
		//				'int m = pops->d.index;
		//				'int km = k - m;
		//				'float dv = stack[ km ];
		//				'for ( int i = km + 1; i < k; ++i )
		//				'{
		//				'	dv *= stack[ i ];
		//				'}
		//				'stack[ km - 1 ] *= 1.0f - dv;
		//				'k -= m;
		//				Dim count As Integer
		//				Dim newExpression As String
		//				Dim intermediateExp As IntermediateExpression
		//				count = aFlexOp.index
		//				newExpression = ""
		//				intermediateExp = stack.Pop()
		//				newExpression += intermediateExp.theExpression
		//				For j As Integer = 2 To count
		//					intermediateExp = stack.Pop()
		//					newExpression += " * " + intermediateExp.theExpression
		//				Next
		//				intermediateExp = stack.Pop()
		//				newExpression = intermediateExp.theExpression + " * (1 - " + newExpression + ")"
		//				newExpression = "(" + newExpression + ")"
		//				stack.Push(New IntermediateExpression(newExpression, 5))
		//				'TODO: SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID
		//				'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID Then
		//				'	Dim x As Integer = 42
		//				'TODO: SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID
		//				'ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID Then
		//				'	Dim x As Integer = 42
		//			Else
		//				stack.Clear()
		//				Exit For
		//			End If
		//		Next

		//		' The loop above leaves the final expression on the top of the stack.
		//		If stack.Count = 1 Then
		//			flexRuleEquation += stack.Peek().theExpression
		//		ElseIf stack.Count = 0 OrElse stack.Count > 1 Then
		//			flexRuleEquation = "// [Decompiler failed to parse expression. Please report the error with the following info: this qc file, the mdl filename that was decompiled, and where the mdl file was found (e.g. the game's name or a web link).]"
		//		Else
		//			flexRuleEquation = "// [Empty flex rule found and ignored.]"
		//		End If
		//	End If
		//	Return flexRuleEquation
		//End Function

		public void WriteGroupLod()
		{
			WriteLodCommand();
		}

		private void WriteLodCommand()
		{
			string line = "";

			//NOTE: Data is from VTX file.
			//$lod 10
			//{
			//  replacemodel "producer_model_merged.dmx" "lod1_producer_model_merged.dmx"
			//}
			//$lod 15
			//{
			//  replacemodel "producer_model_merged.dmx" "lod2_producer_model_merged.dmx"
			//}
			//$lod 40
			//{
			//  replacemodel "producer_model_merged.dmx" "lod3_producer_model_merged.dmx"
			//}
			if (theVtxFileData != null && theMdlFileData.theBodyParts != null)
			{
				if (theVtxFileData.theVtxBodyParts == null)
				{
					return;
				}
				if (theVtxFileData.theVtxBodyParts[0].theVtxModels == null)
				{
					return;
				}
				if (theVtxFileData.theVtxBodyParts[0].theVtxModels[0].theVtxModelLods == null)
				{
					return;
				}

				SourceVtxBodyPart07 aBodyPart = null;
				SourceVtxModel07 aVtxModel = null;
				SourceMdlModel aBodyModel = null;
				int lodIndex = 0;
				LodQcInfo aLodQcInfo = null;
				List<LodQcInfo> aLodQcInfoList = null;
				SortedList<float, List<LodQcInfo>> aLodList = null;
				SortedList<float, bool> aLodListOfFacialFlags = null;
				float switchPoint = 0;

				aLodList = new SortedList<float, List<LodQcInfo>>();
				aLodListOfFacialFlags = new SortedList<float, bool>();
				for (int bodyPartIndex = 0; bodyPartIndex < theVtxFileData.theVtxBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = theVtxFileData.theVtxBodyParts[bodyPartIndex];

					if (aBodyPart.theVtxModels != null)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theVtxModels.Count; modelIndex++)
						{
							aVtxModel = aBodyPart.theVtxModels[modelIndex];

							if (aVtxModel.theVtxModelLods != null)
							{
								aBodyModel = theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex];
								//NOTE: This check is for skipping "blank" bodygroup. Example: the third bodygroup of L4D2's "infected/common_female_tshirt_skirt.mdl".
								if (aBodyModel.name[0] == '\0' && aVtxModel.theVtxModelLods[0].theVtxMeshes == null)
								{
									continue;
								}

								//NOTE: Start loop at 1 to skip first LOD, which isn't needed for the $lod command.
								for (lodIndex = 1; lodIndex < theVtxFileData.lodCount; lodIndex++)
								{
									//TODO: Why would this count be different than the file header count?
									if (lodIndex >= aVtxModel.theVtxModelLods.Count)
									{
										break;
									}

									switchPoint = aVtxModel.theVtxModelLods[lodIndex].switchPoint;
									if (!aLodList.ContainsKey(switchPoint))
									{
										aLodQcInfoList = new List<LodQcInfo>();
										aLodList.Add(switchPoint, aLodQcInfoList);
										aLodListOfFacialFlags.Add(switchPoint, aVtxModel.theVtxModelLods[lodIndex].theVtxModelLodUsesFacial);
									}
									else
									{
										aLodQcInfoList = aLodList[switchPoint];
									}

									aBodyModel.theSmdFileNames[0] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[0], bodyPartIndex, modelIndex, 0, theModelName, new string(theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name));
									aBodyModel.theSmdFileNames[lodIndex] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[lodIndex], bodyPartIndex, modelIndex, lodIndex, theModelName, new string(theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name));
									aLodQcInfo = new LodQcInfo();
									aLodQcInfo.referenceFileName = aBodyModel.theSmdFileNames[0];
									aLodQcInfo.lodFileName = aBodyModel.theSmdFileNames[lodIndex];
									aLodQcInfoList.Add(aLodQcInfo);
								}
							}
						}
					}
				}

				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				List<LodQcInfo> lodQcInfoListOfShadowLod = null;

				lodIndex = 0;
				for (int lodListIndex = 0; lodListIndex < aLodList.Count; lodListIndex++)
				{
					switchPoint = aLodList.Keys[lodListIndex];
					if (switchPoint == -1)
					{
						// Skip writing $shadowlod. Write it last after this loop.
						lodQcInfoListOfShadowLod = aLodList.Values[lodListIndex];
						continue;
					}
					lodIndex += 1;

					aLodQcInfoList = aLodList.Values[lodListIndex];

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$LOD ";
					}
					else
					{
						line = "$lod ";
					}
					line += switchPoint.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					theOutputFileStreamWriter.WriteLine(line);
					WriteLodOptions(lodIndex, aLodListOfFacialFlags.Values[lodListIndex], aLodQcInfoList);
					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}

				//NOTE: As a requirement for the compiler, write $shadowlod last.
				lodIndex = aLodList.Count - 1;
				if (lodQcInfoListOfShadowLod != null)
				{
					//// Shadow lod reserves -1 as switch value
					//// which uniquely identifies a shadow lod
					//newLOD.switchValue = -1.0f;
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$ShadowLOD";
					}
					else
					{
						line = "$shadowlod";
					}
					theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					theOutputFileStreamWriter.WriteLine(line);
					WriteLodOptions(lodIndex, false, lodQcInfoListOfShadowLod);
					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteLodOptions(int lodIndex, bool lodUsesFacial, List<LodQcInfo> aLodQcInfoList)
		{
			string line = "";
			LodQcInfo aLodQcInfo = null;

			for (int i = 0; i < aLodQcInfoList.Count; i++)
			{
				aLodQcInfo = aLodQcInfoList[i];

				if (string.IsNullOrEmpty(aLodQcInfo.lodFileName))
				{
					line = "\t";
					line += "removemodel ";
					line += "\"";
					line += aLodQcInfo.referenceFileName;
					line += "\"";
				}
				else
				{
					line = "\t";
					line += "replacemodel ";
					line += "\"";
					line += aLodQcInfo.referenceFileName;
					line += "\" \"";
					line += aLodQcInfo.lodFileName;
					line += "\"";
				}

				theOutputFileStreamWriter.WriteLine(line);
			}

			try
			{
				SourceVtxMaterialReplacementList07 materialReplacementList = theVtxFileData.theVtxMaterialReplacementLists[lodIndex];
				if (materialReplacementList.theVtxMaterialReplacements != null)
				{
					foreach (SourceVtxMaterialReplacement07 materialReplacement in materialReplacementList.theVtxMaterialReplacements)
					{
						line = "\t";
						line += "replacematerial ";
						line += "\"";
						line += theMdlFileData.theModifiedTextureFileNames[materialReplacement.materialIndex];
						line += "\" \"";
						line += materialReplacement.theName;
						line += "\"";

						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			foreach (SourceMdlBone aBone in theMdlFileData.theBones)
			{
				if (aBone.parentBoneIndex >= 0 && ((lodIndex == 1 && (aBone.flags & SourceMdlBone.BONE_USED_BY_VERTEX_LOD1) == 0) || (lodIndex == 2 && (aBone.flags & SourceMdlBone.BONE_USED_BY_VERTEX_LOD2) == 0) || (lodIndex == 3 && (aBone.flags & SourceMdlBone.BONE_USED_BY_VERTEX_LOD3) == 0) || (lodIndex == 4 && (aBone.flags & SourceMdlBone.BONE_USED_BY_VERTEX_LOD4) == 0) || (lodIndex == 5 && (aBone.flags & SourceMdlBone.BONE_USED_BY_VERTEX_LOD5) == 0) || (lodIndex == 6 && (aBone.flags & SourceMdlBone.BONE_USED_BY_VERTEX_LOD6) == 0) || (lodIndex == 7 && (aBone.flags & SourceMdlBone.BONE_USED_BY_VERTEX_LOD7) == 0)))
				{
					//replacebone "ValveBiped.Bip01_Neck1" "ValveBiped.Bip01_Head1"
					line = "\t";
					line += "replacebone ";
					line += "\"";
					line += aBone.theName;
					line += "\" \"";
					line += theMdlFileData.theBones[aBone.parentBoneIndex].theName;
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "\t";
			if (lodUsesFacial)
			{
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line += "Facial";
				}
				else
				{
					line += "facial";
				}
			}
			else
			{
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line += "NoFacial";
				}
				else
				{
					line += "nofacial";
				}
			}
			theOutputFileStreamWriter.WriteLine(line);

			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_USE_SHADOWLOD_MATERIALS) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "Use_ShadowLOD_Materials";
				}
				else
				{
					line = "use_shadowlod_materials";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteNoForcedFadeCommand()
		{
			string line = "";

			//$noforcedfade
			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_NO_FORCED_FADE) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$NoForcedFade";
				}
				else
				{
					line = "$noforcedfade";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteForcePhonemeCrossfadeCommand()
		{
			string line = "";

			//$forcephonemecrossfade
			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_FORCE_PHONEME_CROSSFADE) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$ForcePhonemeCrossFade";
				}
				else
				{
					line = "$forcephonemecrossfade";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WritePoseParameterCommand()
		{
			string line = "";

			//NOTE: According to source code for v44 and v48, not having "loop" means the last value is ignored:
			//$poseparameter body_pitch -90.00 90.00 360.00
			//$poseparameter body_yaw -90.00 90.00 360.00
			//$poseparameter head_pitch -90.00 90.00 360.00
			//$poseparameter head_yaw -90.00 90.00 360.00
			//NOTE: These 2 lines are equivalent:
			//$poseparameter head_yaw -90.00 90.00 loop 180.00
			//$poseparameter head_yaw -90.00 90.00 wrap
			if (theMdlFileData.thePoseParamDescs != null)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.thePoseParamDescs.Count; i++)
				{
					SourceMdlPoseParamDesc aPoseParamDesc = theMdlFileData.thePoseParamDescs[i];
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$PoseParameter \"";
					}
					else
					{
						line = "$poseparameter \"";
					}
					line += aPoseParamDesc.theName;
					line += "\" ";
					line += aPoseParamDesc.startingValue.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aPoseParamDesc.endingValue.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " loop ";
					line += aPoseParamDesc.loopingRange.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteAmbientBoostCommand()
		{
			string line = "";

			//$ambientboost
			if ((theMdlFileData.version == 44 && ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_AMBIENT_BOOST_MDL44) > 0)) || (theMdlFileData.version >= 45 && ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_AMBIENT_BOOST) > 0)))
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$AmbientBoost";
				}
				else
				{
					line = "$ambientboost";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteOpaqueCommand()
		{
			string line = "";

			//$mostlyopaque
			//$opaque
			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_FORCE_OPAQUE) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$Opaque";
				}
				else
				{
					line = "$opaque";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
			else if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_TRANSLUCENT_TWOPASS) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$MostlyOpaque";
				}
				else
				{
					line = "$mostlyopaque";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteObsoleteCommand()
		{
			string line = "";

			//$obsolete
			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_OBSOLETE) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$Obsolete";
				}
				else
				{
					line = "$obsolete";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteCastTextureShadowsCommand()
		{
			string line = "";

			//$casttextureshadows
			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_CAST_TEXTURE_SHADOWS) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$CastTextureShadows";
				}
				else
				{
					line = "$casttextureshadows";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteDoNotCastShadowsCommand()
		{
			string line = "";

			//$donotcastshadows
			if ((theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_DO_NOT_CAST_SHADOWS) > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$DoNotCastShadows";
				}
				else
				{
					line = "$donotcastshadows";
				}
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteCdMaterialsCommand()
		{
			string line = "";
			List<string> texturePaths = theMdlFileData.theModifiedTexturePaths;


			//$cdmaterials "models\survivors\producer\"
			//$cdmaterials "models\survivors\"
			//$cdmaterials ""
			if (texturePaths != null)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < texturePaths.Count; i++)
				{
					string aTexturePath = texturePaths[i];
					//NOTE: Write out all stored paths, even if null or empty strings, because Crowbar should show what was stored.
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$CDMaterials ";
					}
					else
					{
						line = "$cdmaterials ";
					}
					line += "\"";
					line += aTexturePath;
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteTextureGroupCommand()
		{
			string line = "";
			List<string> textureFileNames = theMdlFileData.theModifiedTextureFileNames;


			if (theMdlFileData.theSkinFamilies != null && theMdlFileData.theSkinFamilies.Count > 0 && textureFileNames != null && textureFileNames.Count > 0 && theMdlFileData.skinReferenceCount > 0)
			{
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

					List<string> textureFileNamesForSkinFamily = new List<string>(skinReferenceCount);
					for (int j = 0; j < skinReferenceCount; j++)
					{
						string aTextureFileName = textureFileNames[aSkinFamily[j]];

						textureFileNamesForSkinFamily.Add(aTextureFileName);
					}

					skinFamiliesOfTextureFileNames.Add(textureFileNamesForSkinFamily);
				}

				if ((!MainCROWBAR.TheApp.Settings.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked) || (skinFamiliesOfTextureFileNames.Count > 1))
				{
					theOutputFileStreamWriter.WriteLine();

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$TextureGroup \"skinfamilies\"";
					}
					else
					{
						line = "$texturegroup \"skinfamilies\"";
					}
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

		public void WriteTextureFileNameComments()
		{
			//// Model uses material "producer_head.vmt"
			//// Model uses material "producer_body.vmt"
			//// Model uses material "producer_head_it.vmt"
			//// Model uses material "producer_body_it.vmt"
			//// Model uses material "models/survivors/producer/producer_hair.vmt"
			//// Model uses material "models/survivors/producer/producer_eyeball_l.vmt"
			//// Model uses material "models/survivors/producer/producer_eyeball_r.vmt"
			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked && theMdlFileData.theTextures != null)
			{
				string line = "";

				theOutputFileStreamWriter.WriteLine(line);

				line = "// This list shows the VMT file names used in the SMD files.";
				theOutputFileStreamWriter.WriteLine(line);

				for (int j = 0; j < theMdlFileData.theTextures.Count; j++)
				{
					SourceMdlTexture aTexture = theMdlFileData.theTextures[j];
					line = "// \"";
					line += FileManager.GetCleanPathFileName(aTexture.thePathFileName, false);
					line += ".vmt\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteAttachmentCommand()
		{
			string line = "";
			double offsetX = 0;
			double offsetY = 0;
			double offsetZ = 0;
			double angleX = 0;
			double angleY = 0;
			double angleZ = 0;

			//$attachment "eyes" "ValveBiped.Bip01_Head1" 3.42 -2.36 0.05 rotate 0.00 -89.37 -90.00
			//$attachment "mouth" "ValveBiped.Bip01_Head1" 0.71 -5.15 -0.13 rotate -0.00 -80.00 -90.00
			//$attachment "survivor_light" "ValveBiped.Bip01_Spine2" 5.33 21.31 -0.00 rotate -0.00 -0.00 -0.00
			//$attachment "forward" "ValveBiped.forward" 0.00 -0.00 0.00 rotate 0.00 0.00 0.00
			//$attachment "pistol" "ValveBiped.Bip01_R_Thigh" -2.95 1.84 -4.61 rotate -3.66 -0.47 91.70
			//$attachment "L_weapon_bone" "ValveBiped.L_weapon_bone" 0.00 -0.00 0.00 rotate -0.00 0.00 -0.00
			//$attachment "weapon_bone" "ValveBiped.weapon_bone" 0.00 0.00 0.00 rotate 0.00 0.00 -0.00
			//$attachment "medkit" "ValveBiped.Bip01_Spine4" -0.65 -2.83 -1.16 rotate 5.03 77.16 0.00
			//$attachment "primary" "ValveBiped.Bip01_Spine4" 2.71 -4.36 -2.33 rotate -13.70 170.19 174.29
			//$attachment "attach_R_shoulderBladeAim" "ValveBiped.Bip01_Spine4" -8.88 0.88 -4.51 rotate -90.00 -102.85 0.00
			//$attachment "attach_L_shoulderBladeAim" "ValveBiped.Bip01_Spine4" -8.88 0.88 3.12 rotate -90.00 -102.85 0.00
			//$attachment "melee" "ValveBiped.Bip01_Spine4" 2.64 -3.12 4.45 rotate 24.08 175.37 97.14
			//$attachment "molotov" "ValveBiped.Bip01_Spine" -3.19 -2.44 7.01 rotate -63.44 -74.67 -101.41
			//$attachment "grenade" "ValveBiped.Bip01_Spine" -0.68 1.17 6.97 rotate -90.00 -175.23 0.00
			//$attachment "pills" "ValveBiped.Bip01_Spine" -2.63 0.63 -7.56 rotate -41.18 -88.49 -87.05
			//$attachment "lfoot" "ValveBiped.Bip01_L_Foot" 0.00 4.44 0.00 rotate -0.00 -0.00 -0.00
			//$attachment "rfoot" "ValveBiped.Bip01_R_Foot" 0.00 4.44 0.00 rotate -0.00 0.00 -0.00
			//$attachment "muzzle_flash" "ValveBiped.Bip01_L_Hand" 0.00 0.00 0.00 rotate -0.00 0.00 0.00
			//$attachment "survivor_neck" "ValveBiped.Bip01_Neck1" 0.00 0.00 0.00 rotate 0.00 0.00 -0.00
			//$attachment "forward" "ValveBiped.forward" 0.00 -0.00 0.00 rotate 0.00 0.00 0.00
			//$attachment "bleedout" "ValveBiped.Bip01_Pelvis" 8.44 8.88 4.44 rotate -0.00 0.00 0.00
			//$attachment "survivor_light" "ValveBiped.Bip01_Spine2" 5.33 21.31 -0.00 rotate -0.00 -0.00 -0.00
			//$attachment "legL_B" "ValveBiped.attachment_bandage_legL" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
			//$attachment "armL_B" "ValveBiped.attachment_bandage_armL" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
			//$attachment "armL_T" "ValveBiped.attachment_armL_T" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
			//$attachment "armR_T" "ValveBiped.attachment_armR_T" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
			//$attachment "armL" "ValveBiped.Bip01_L_Forearm" 0.00 0.00 -0.00 rotate -0.00 -0.00 0.00
			//$attachment "legL" "ValveBiped.Bip01_L_Calf" 0.00 0.00 0.00 rotate -0.00 -0.00 -0.00
			//$attachment "thighL" "ValveBiped.Bip01_L_Thigh" 0.00 0.00 0.00 rotate -0.00 -0.00 -0.00
			//$attachment "spine" "ValveBiped.Bip01_Spine" 0.00 0.00 0.00 rotate -90.00 -90.00 0.00
			if (theMdlFileData.theAttachments != null)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int attachmentIndex = 0; attachmentIndex < theMdlFileData.theAttachments.Count; attachmentIndex++)
				{
					SourceMdlAttachment anAttachment = theMdlFileData.theAttachments[attachmentIndex];

					// Do not write an attachment line for the $illumposition attachment.
					if (attachmentIndex == theMdlFileData.illumPositionAttachmentNumber - 1)
					{
						continue;
					}

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$Attachment ";
					}
					else
					{
						line = "$attachment ";
					}
					if (string.IsNullOrEmpty(anAttachment.theName))
					{
						line += attachmentIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					}
					else
					{
						line += "\"";
						line += anAttachment.theName;
						line += "\"";
					}
					line += " \"";
					line += theMdlFileData.theBones[anAttachment.localBoneIndex].theName;
					line += "\"";
					line += " ";

					//TheApp.ConvertRotationMatrixToDegrees(anAttachment.localM11, anAttachment.localM12, anAttachment.localM13, anAttachment.localM21, anAttachment.localM22, anAttachment.localM23, anAttachment.localM33, angleX, angleY, angleZ)
					//NOTE: This one works with the strange order below.
					MathModule.ConvertRotationMatrixToDegrees(anAttachment.localM11, anAttachment.localM21, anAttachment.localM31, anAttachment.localM12, anAttachment.localM22, anAttachment.localM32, anAttachment.localM33, ref angleX, ref angleY, ref angleZ);
					offsetX = Math.Round(anAttachment.localM14, 2);
					offsetY = Math.Round(anAttachment.localM24, 2);
					offsetZ = Math.Round(anAttachment.localM34, 2);
					angleX = Math.Round(angleX, 2);
					angleY = Math.Round(angleY, 2);
					angleZ = Math.Round(angleZ, 2);
					line += offsetX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += offsetY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += offsetZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " rotate ";
					//'NOTE: Intentionally z,y,x order.
					//line += angleZ.ToString()
					//line += " "
					//line += angleY.ToString()
					//line += " "
					//line += angleX.ToString()
					//NOTE: Intentionally in strange order.
					line += angleY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += (-angleZ).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += (-angleX).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteIncludeModelCommands()
		{
			string line = "";

			//$includemodel "survivors/anim_producer.mdl"
			//$includemodel "survivors/anim_gestures.mdl"
			if (theMdlFileData.theModelGroups != null)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.theModelGroups.Count; i++)
				{
					SourceMdlModelGroup aModelGroup = theMdlFileData.theModelGroups[i];
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$IncludeModel ";
					}
					else
					{
						line = "$includemodel ";
					}
					line += "\"";
					if (aModelGroup.theFileName.StartsWith("models/"))
					{
						line += aModelGroup.theFileName.Substring(7);
					}
					else
					{
						line += aModelGroup.theFileName;
					}
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteSurfacePropCommand()
		{
			string line = "";

			if (!string.IsNullOrEmpty(theMdlFileData.theSurfacePropName))
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				//$surfaceprop "flesh"
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$SurfaceProp ";
				}
				else
				{
					line = "$surfaceprop ";
				}
				line += "\"";
				line += theMdlFileData.theSurfacePropName;
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteJointSurfacePropCommand()
		{
			string line = "";

			//$jointsurfaceprop <bone name> <surfaceprop>
			//$jointsurfaceprop "ValveBiped.Bip01_L_Toe0"	 "flesh"
			if (theMdlFileData.theBones != null)
			{
				SourceMdlBone aBone = null;
				bool emptyLineIsAlreadyWritten = false;

				for (int i = 0; i < theMdlFileData.theBones.Count; i++)
				{
					aBone = theMdlFileData.theBones[i];

					if (aBone.theSurfacePropName != theMdlFileData.theSurfacePropName)
					{
						if (!emptyLineIsAlreadyWritten)
						{
							line = "";
							theOutputFileStreamWriter.WriteLine(line);
							emptyLineIsAlreadyWritten = true;
						}

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$JointSurfaceProp ";
						}
						else
						{
							line = "$jointsurfaceprop ";
						}
						line += "\"";
						line += aBone.theName;
						line += "\"";
						line += " ";
						line += "\"";
						line += aBone.theSurfacePropName;
						line += "\"";
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		public void WriteContentsCommand()
		{
			if (theMdlFileData.contents > 0)
			{
				string line = "";

				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				//$contents "monster" "grate"
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$Contents";
				}
				else
				{
					line = "$contents";
				}
				line += GetContentsFlags(theMdlFileData.contents);
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteJointContentsCommand()
		{
			string line = "";

			//$jointcontents "<bone_name>" "<content_type_1>" "<content_type_2>" "<content_type_3>"
			if (theMdlFileData.theBones != null)
			{
				SourceMdlBone aBone = null;
				bool emptyLineIsAlreadyWritten = false;

				for (int i = 0; i < theMdlFileData.theBones.Count; i++)
				{
					aBone = theMdlFileData.theBones[i];

					if (aBone.contents != theMdlFileData.contents)
					{
						if (!emptyLineIsAlreadyWritten)
						{
							line = "";
							theOutputFileStreamWriter.WriteLine(line);
							emptyLineIsAlreadyWritten = true;
						}

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$JointContents ";
						}
						else
						{
							line = "$jointcontents ";
						}
						line += "\"";
						line += aBone.theName;
						line += "\"";
						line += GetContentsFlags(aBone.contents);
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		////-----------------------------------------------------------------------------
		//// Parse contents flags
		////-----------------------------------------------------------------------------
		//static void ParseContents( int *pAddFlags, int *pRemoveFlags )
		//{
		//	*pAddFlags = 0;
		//	*pRemoveFlags = 0;
		//	do 
		//	{
		//		GetToken (false);

		//		if ( !stricmp( token, "grate" ) )
		//		{
		//			*pAddFlags |= CONTENTS_GRATE;
		//			*pRemoveFlags |= CONTENTS_SOLID;
		//		}
		//		else if ( !stricmp( token, "ladder" ) )
		//		{
		//			*pAddFlags |= CONTENTS_LADDER;
		//		}
		//		else if ( !stricmp( token, "solid" ) )
		//		{
		//			*pAddFlags |= CONTENTS_SOLID;
		//		}
		//		else if ( !stricmp( token, "monster" ) )
		//		{
		//			*pAddFlags |= CONTENTS_MONSTER;
		//		}
		//		else if ( !stricmp( token, "notsolid" ) )
		//		{
		//			*pRemoveFlags |= CONTENTS_SOLID;
		//		}
		//	} while (TokenAvailable());
		//}
		private string GetContentsFlags(int contentsFlags)
		{
			string flagNames = "";

			if ((contentsFlags & SourceMdlBone.CONTENTS_GRATE) > 0)
			{
				flagNames += " ";
				flagNames += "\"";
				flagNames += "grate";
				flagNames += "\"";
			}
			if ((contentsFlags & SourceMdlBone.CONTENTS_MONSTER) > 0)
			{
				flagNames += " ";
				flagNames += "\"";
				flagNames += "monster";
				flagNames += "\"";
			}
			if ((contentsFlags & SourceMdlBone.CONTENTS_LADDER) > 0)
			{
				flagNames += " ";
				flagNames += "\"";
				flagNames += "ladder";
				flagNames += "\"";
			}
			if ((contentsFlags & SourceMdlBone.CONTENTS_SOLID) > 0)
			{
				flagNames += " ";
				flagNames += "\"";
				flagNames += "solid";
				flagNames += "\"";
			}

			if (string.IsNullOrEmpty(flagNames))
			{
				flagNames += " ";
				flagNames += "\"";
				flagNames += "notsolid";
				flagNames += "\"";
			}

			return flagNames;
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

			//$eyeposition -0.000 0.000 70.000
			//NOTE: These are stored in different order in MDL file.
			//FROM: utils\studiomdl\studiomdl.cpp Cmd_Eyeposition()
			//eyeposition[1] = verify_atof (token);
			//eyeposition[0] = -verify_atof (token);
			//eyeposition[2] = verify_atof (token);
			if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
			{
				line = "$EyePosition ";
			}
			else
			{
				line = "$eyeposition ";
			}
			line += offsetX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += offsetY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += offsetZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteMaxEyeDeflectionCommand()
		{
			//FROM: [48] SourceEngine2007_source se2007_src\src_main\public\studio.h
			//	inline float		MaxEyeDeflection() const { return flMaxEyeDeflection != 0.0f ? flMaxEyeDeflection : 0.866f; } // default to cos(30) if not set
			//NOTE: Based on above comment, if the value stored is zero, then do not write the command to QC.
			if (theMdlFileData.maxEyeDeflection < -0.0000001F || theMdlFileData.maxEyeDeflection > 0.0000001F)
			{
				string line = "";
				double deflection;

				//FROM: SourceEngine2007_source\src_main\utils\studiomdl\studiomdl.cpp
				//	g_flMaxEyeDeflection = cosf( verify_atof( token ) * M_PI / 180.0f );
				deflection = Math.Acos(theMdlFileData.maxEyeDeflection);
				deflection = MathModule.RadiansToDegrees(deflection);
				deflection = Math.Round(deflection, 3);

				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				// Found in L4D2 file: "survivors\Biker\biker.qc".
				//// Eyes can look this many degrees up/down/to the sides
				//$maxeyedeflection 30
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$MaxEyeDeflection ";
				}
				else
				{
					line = "$maxeyedeflection ";
				}
				line += deflection.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteIllumPositionCommand()
		{
			string line = null;
			double offsetX = 0;
			double offsetY = 0;
			double offsetZ = 0;
			string illumPosBoneName = "";

			if (theMdlFileData.illumPositionAttachmentNumber == 0)
			{
				//FROM: [48] SourceEngine2007_source se2007_src\src_main\utils\studiomdl\studiomdl.cpp Cmd_Illumposition()
				//		g_illumpositionattachment = 0;
				//		float flTemp = illumposition[0];
				//		illumposition[0] = -illumposition[1];
				//		illumposition[1] = flTemp;
				offsetX = Math.Round(theMdlFileData.illuminationPosition.y, 3);
				offsetY = -Math.Round(theMdlFileData.illuminationPosition.x, 3);
				offsetZ = Math.Round(theMdlFileData.illuminationPosition.z, 3);
			}
			else
			{
				//FROM: [48] SourceEngine2007_source se2007_src\src_main\utils\studiomdl\studiomdl.cpp Cmd_Illumposition()
				//		Q_strncpy( g_attachment[g_numattachments].name, "__illumPosition", sizeof(g_attachment[g_numattachments].name) );
				//		Q_strncpy( g_attachment[g_numattachments].bonename, token, sizeof(g_attachment[g_numattachments].bonename) );
				//		AngleMatrix( QAngle( 0, 0, 0 ), illumposition, g_attachment[g_numattachments].local );
				//		g_attachment[g_numattachments].type |= IS_RIGID;
				//		g_illumpositionattachment = g_numattachments + 1;
				SourceMdlAttachment illumPosAttachment = theMdlFileData.theAttachments[theMdlFileData.illumPositionAttachmentNumber - 1];
				offsetX = Math.Round(illumPosAttachment.localM14, 2);
				offsetY = Math.Round(illumPosAttachment.localM24, 2);
				offsetZ = Math.Round(illumPosAttachment.localM34, 2);
				illumPosBoneName = theMdlFileData.theBones[illumPosAttachment.localBoneIndex].theName;
			}

			line = "";
			theOutputFileStreamWriter.WriteLine(line);

			line = "";
			if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
			{
				line += "$IllumPosition ";
			}
			else
			{
				line += "$illumposition ";
			}
			line += offsetX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += offsetY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line += " ";
			line += offsetZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			if (!string.IsNullOrEmpty(illumPosBoneName))
			{
				line += " \"";
				line += illumPosBoneName;
				line += "\"";
			}
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteGroupAnimation()
		{
			WriteAnimBlockSizeCommand();
			WriteBoneSaveFrameCommand();
			WriteSectionFramesCommand();
			WritePoseParameterCommand();
			//NOTE: IkChain commands must be before Animation and Sequence commands because either could refer to IkChain via ikrule option.
			WriteIkChainCommand();
			WriteIkAutoPlayLockCommand();
			FillInWeightLists();
			//NOTE: Must write $WeightList lines before animations or sequences that use them.
			WriteWeightListCommand();
			//NOTE: Must write $animation lines before $sequence lines that use them.
			try
			{
				WriteCorrectiveAnimationBlock();
				WriteAnimationOrDeclareAnimationCommand();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			try
			{
				WriteSequenceOrDeclareSequenceCommand();
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			WriteIncludeModelCommands();
		}

		private void WriteAnimBlockSizeCommand()
		{
			string line = "";

			//$animblocksize 32 nostall highres
			if (theMdlFileData.animBlockCount > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				line = "// The 32 below is a guess until further is known about the format.";
				theOutputFileStreamWriter.WriteLine(line);

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$AnimBlockSize";
				}
				else
				{
					line = "$animblocksize";
				}
				line += " ";
				line += "32";
				//TODO: Only use with MDL49 because HL2 zombie\classic.mdl (MDL48) recompiled nearly perfect without it. 
				//      With the nostall, it messed up some anims and seemed to put most anim data in the MDL file instead of the ANI file.
				if (theMdlFileData.theAnimBlockSizeNoStallOptionIsUsed && theMdlFileData.version == 49)
				{
					line += " ";
					line += "nostall";
				}
				//line += " "
				//line += "highres"
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		private void WriteSectionFramesCommand()
		{
			string line = "";

			//$sectionframes
			if (theMdlFileData.theSectionFrameCount > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$SectionFrames";
				}
				else
				{
					line = "$sectionframes";
				}
				line += " ";
				line += theMdlFileData.theSectionFrameCount.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += theMdlFileData.theSectionFrameMinFrameCount.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		private void FillInWeightLists()
		{
			if (theMdlFileData.theSequenceDescs != null)
			{
				SourceMdlSequenceDesc aSeqDesc = null;
				SourceMdlWeightList aWeightList = null;
				int aWeightListIndex = 0;

				for (int i = 0; i < theMdlFileData.theSequenceDescs.Count; i++)
				{
					aSeqDesc = theMdlFileData.theSequenceDescs[i];

					try
					{
						if (aSeqDesc.theBoneWeights != null && aSeqDesc.theBoneWeights.Count > 0 && !aSeqDesc.theBoneWeightsAreDefault)
						{
							for (aWeightListIndex = 0; aWeightListIndex < theMdlFileData.theWeightLists.Count; aWeightListIndex++)
							{
								aWeightList = theMdlFileData.theWeightLists[aWeightListIndex];

								if (GenericsModule.ListsAreEqual(aSeqDesc.theBoneWeights, aWeightList.theWeights))
								{
									break;
								}
							}

							if (aWeightListIndex < theMdlFileData.theWeightLists.Count)
							{
								aSeqDesc.theWeightListIndex = aWeightListIndex;
							}
							else
							{
								aWeightList = new SourceMdlWeightList();

								//NOTE: Name is not stored, so use something reasonable.
								aWeightList.theName = "weights_" + aSeqDesc.theName;
								foreach (double value in aSeqDesc.theBoneWeights)
								{
									aWeightList.theWeights.Add(value);
								}

								theMdlFileData.theWeightLists.Add(aWeightList);

								aSeqDesc.theWeightListIndex = theMdlFileData.theWeightLists.Count - 1;
							}
						}
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
				}
			}
		}

		private void WriteWeightListCommand()
		{
			string line = "";
			string commentTag = "";

			//'NOTE: Comment-out for now, because some models will not recompile with them.
			//commentTag = "// "

			//$weightlist top_bottom {
			//	"Bone_1" 0
			//	"Bone_2" 0.25
			//	"Bone_3" 0.5
			//	"Bone_4" 0.75
			//	"Bone_5" 1
			//}
			//If Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs IsNot Nothing Then
			//	Me.theOutputFileStreamWriter.WriteLine()

			//	For i As Integer = 0 To Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs.Count - 1
			//		Dim aSeqDesc As SourceMdlSequenceDesc
			//		aSeqDesc = Me.theSourceEngineModel.MdlFileHeader.theSequenceDescs(i)

			//		If aSeqDesc.theBoneWeights IsNot Nothing AndAlso aSeqDesc.theBoneWeights.Count > 0 AndAlso Not aSeqDesc.theBoneWeightsAreDefault Then
			//			line = "$WeightList "
			//			'NOTE: Name is not stored, so use something reasonable.
			//			line += """"
			//			line += "weights_"
			//			line += aSeqDesc.theName
			//			line += """"
			//			'NOTE: The opening brace must be on same line as the command.
			//			line += " {"
			//			Me.theOutputFileStreamWriter.WriteLine(commentTag + line)

			//			For boneWeightIndex As Integer = 0 To aSeqDesc.theBoneWeights.Count - 1
			//				line = vbTab
			//				line += " """
			//				line += Me.theSourceEngineModel.MdlFileHeader.theBones(boneWeightIndex).theName
			//				line += """ "
			//				line += aSeqDesc.theBoneWeights(boneWeightIndex).ToString("0.######", TheApp.InternalNumberFormat)
			//				Me.theOutputFileStreamWriter.WriteLine(commentTag + line)
			//			Next

			//			line = "}"
			//			Me.theOutputFileStreamWriter.WriteLine(commentTag + line)
			//		End If
			//	Next
			//End If
			foreach (SourceMdlWeightList aWeightList in theMdlFileData.theWeightLists)
			{
				theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$WeightList ";
				}
				else
				{
					line = "$weightlist ";
				}
				line += "\"";
				line += aWeightList.theName;
				line += "\"";
				//NOTE: The opening brace must be on same line as the command.
				line += " {";
				theOutputFileStreamWriter.WriteLine(commentTag + line);

				for (int boneWeightIndex = 0; boneWeightIndex < aWeightList.theWeights.Count; boneWeightIndex++)
				{
					line = "\t";
					line += " \"";
					line += theMdlFileData.theBones[boneWeightIndex].theName;
					line += "\" ";
					line += aWeightList.theWeights[boneWeightIndex].ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(commentTag + line);
				}

				line = "}";
				theOutputFileStreamWriter.WriteLine(commentTag + line);
			}
		}

		private void WriteCorrectiveAnimationBlock()
		{
			if (theMdlFileData.theCorrectiveAnimationDescs != null)
			{
				string line = "";
				string correctiveAnimationSmdRelativePathFileName = null;
				string animationName = null;

				foreach (SourceMdlAnimationDesc49 anAnimationDesc in theMdlFileData.theCorrectiveAnimationDescs)
				{
					correctiveAnimationSmdRelativePathFileName = SourceFileNamesModule.CreateCorrectiveAnimationSmdRelativePathFileName(anAnimationDesc.theName, theModelName);

					theOutputFileStreamWriter.WriteLine();
					//NOTE: The $Animation command must have name first and file name second and on same line as the command.
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$Animation";
					}
					else
					{
						line = "$animation";
					}
					animationName = Path.GetFileNameWithoutExtension(correctiveAnimationSmdRelativePathFileName);
					line += " \"";
					line += animationName;
					line += "\" \"";
					line += correctiveAnimationSmdRelativePathFileName;
					line += "\"";
					//NOTE: Opening brace must be on same line as the command.
					line += " {";
					theOutputFileStreamWriter.WriteLine(line);
					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteAnimationOrDeclareAnimationCommand()
		{
			if (theMdlFileData.theAnimationDescs != null)
			{
				for (int i = 0; i < theMdlFileData.theAnimationDescs.Count; i++)
				{
					SourceMdlAnimationDesc49 anAnimationDesc = theMdlFileData.theAnimationDescs[i];

					if (anAnimationDesc.theName[0] != '@')
					{
						WriteAnimationLine(anAnimationDesc);
					}
				}
			}
		}

		private void WriteAnimationLine(SourceMdlAnimationDesc49 anAnimationDesc)
		{
			string line = "";

			theOutputFileStreamWriter.WriteLine();

			if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0)
			{
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$DeclareAnimation";
				}
				else
				{
					line = "$declareanimation";
				}
				line += " \"";
				//TODO: Does this need to check and remove initial "@" from name?
				line += anAnimationDesc.theName;
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}
			else
			{
				//$animation a_reference "primary_idle.dmx" lx ly
				//NOTE: The $Animation command must have name first and file name second and on same line as the command.
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$Animation";
				}
				else
				{
					line = "$animation";
				}
				anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, theModelName, anAnimationDesc.theName);
				line += " \"";
				line += anAnimationDesc.theName;
				line += "\" \"";
				line += anAnimationDesc.theSmdRelativePathFileName;
				line += "\"";
				//NOTE: Opening brace must be on same line as the command.
				line += " {";
				theOutputFileStreamWriter.WriteLine(line);

				WriteAnimationOptions(null, anAnimationDesc, null);

				line = "}";
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		private void WriteSequenceOrDeclareSequenceCommand()
		{
			//$sequence producer "producer" fps 30.00
			//$sequence ragdoll "ragdoll" ACT_DIERAGDOLL 1 fps 30.00
			if (theMdlFileData.theSequenceDescs != null)
			{
				for (int i = 0; i < theMdlFileData.theSequenceDescs.Count; i++)
				{
					SourceMdlSequenceDesc aSequenceDesc = theMdlFileData.theSequenceDescs[i];

					WriteSequenceLine(aSequenceDesc);
				}
			}
		}

		private void WriteSequenceLine(SourceMdlSequenceDesc aSequenceDesc)
		{
			string line = "";

			theOutputFileStreamWriter.WriteLine();

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0)
			{
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$DeclareSequence";
				}
				else
				{
					line = "$declaresequence";
				}
				line += " \"";
				line += aSequenceDesc.theName;
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}
			else
			{
				if (aSequenceDesc.theAnimDescIndexes != null || aSequenceDesc.theAnimDescIndexes.Count > 0)
				{
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$Sequence ";
					}
					else
					{
						line = "$sequence ";
					}
					line += "\"";
					line += aSequenceDesc.theName;
					line += "\"";
					//NOTE: Opening brace must be on same line as the command.
					line += " {";
					theOutputFileStreamWriter.WriteLine(line);

					try
					{
						WriteSequenceOptions(aSequenceDesc);
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

		//activity           // done
		//activitymodifier   // v49; done
		//addlayer           // done
		//autoplay           // done
		//blend              // done
		//blendcenter        // baked-in
		//blendcomp          // baked-in
		//blendlayer         // done
		//blendwidth         // done
		//blendref           // baked-in
		//calcblend          // baked-in
		//delta              // done
		//event              // done
		//exitphase          // not used
		//fadein             // done
		//fadeout            // done
		//hidden             // done
		//iklock             // done
		//keyvalues          // done
		//node               // done
		//posecycle          // v49; done
		//post               // baked-in
		//predelta           // done
		//realtime           // done
		//rtransition        // done
		//snap               // done
		//transition         // done
		//worldspace         // done       
		//ParseAnimationToken( animations[0] )
		//Cmd_ImpliedAnimation( pseq, token )
		private void WriteSequenceOptions(SourceMdlSequenceDesc aSequenceDesc)
		{
			string line = "";
			string valueString = null;
			SourceMdlAnimationDesc49 impliedAnimDesc = null;

			SourceMdlAnimationDesc49 anAnimationDesc = null;
			string name = null;
			for (int j = 0; j < aSequenceDesc.theAnimDescIndexes.Count; j++)
			{
				anAnimationDesc = theMdlFileData.theAnimationDescs[aSequenceDesc.theAnimDescIndexes[j]];
				name = anAnimationDesc.theName;

				line = "\t";
				line += "\"";
				if (name[0] == '@')
				{
					//NOTE: There should only be one implied anim desc.
					impliedAnimDesc = anAnimationDesc;
					anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, theModelName, anAnimationDesc.theName);
					line += anAnimationDesc.theSmdRelativePathFileName;
				}
				else
				{
					line += name;
				}
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}

			if (!string.IsNullOrEmpty(aSequenceDesc.theActivityName))
			{
				//NOTE: Models can use weight of -1. Only 0 shows the error message.
				if (aSequenceDesc.activityWeight == 0)
				{
					line = "\t";
					line += "// The following line is commented-out because compiling shows this error: Activity ACT_IDLE has a zero weight (weights must be integers > 0)";
					theOutputFileStreamWriter.WriteLine(line);
				}

				line = "\t";
				if (aSequenceDesc.activityWeight == 0)
				{
					line += "//";
				}
				line += "activity ";
				line += "\"";
				line += aSequenceDesc.theActivityName;
				line += "\" ";
				line += aSequenceDesc.activityWeight.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			if (aSequenceDesc.theActivityModifiers != null)
			{
				foreach (SourceMdlActivityModifier activityModifier in aSequenceDesc.theActivityModifiers)
				{
					line = "\t";
					line += "activitymodifier ";
					line += activityModifier.theName;
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_AUTOPLAY) > 0)
			{
				line = "\t";
				line += "autoplay";
				theOutputFileStreamWriter.WriteLine(line);
			}

			WriteSequenceBlendInfo(aSequenceDesc);

			if (aSequenceDesc.groupSize[0] != aSequenceDesc.groupSize[1])
			{
				line = "\t";
				line += "blendwidth ";
				line += aSequenceDesc.groupSize[0].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			WriteSequenceDeltaInfo(aSequenceDesc);

			if (aSequenceDesc.theEvents != null)
			{
				int frameIndex = 0;
				int frameCount = theMdlFileData.theAnimationDescs[aSequenceDesc.theAnimDescIndexes[0]].frameCount;
				for (int j = 0; j < aSequenceDesc.theEvents.Count; j++)
				{
					if (frameCount <= 1)
					{
						frameIndex = 0;
					}
					else
					{
						frameIndex = Convert.ToInt32(aSequenceDesc.theEvents[j].cycle * (frameCount - 1));
					}
					line = "\t";
					line += "{ ";
					line += "event ";
					line += aSequenceDesc.theEvents[j].theName;
					line += " ";
					line += frameIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					if (aSequenceDesc.theEvents[j].options != "".ToCharArray())
					{
						line += " \"";
						line += (new string(aSequenceDesc.theEvents[j].options)).Trim('\0');
						line += "\"";
					}
					line += " }";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			valueString = aSequenceDesc.fadeInTime.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line = "\t";
			line += "fadein ";
			line += valueString;
			theOutputFileStreamWriter.WriteLine(line);

			valueString = aSequenceDesc.fadeOutTime.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line = "\t";
			line += "fadeout ";
			line += valueString;
			theOutputFileStreamWriter.WriteLine(line);

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_HIDDEN) > 0)
			{
				line = "\t";
				line += "hidden";
				theOutputFileStreamWriter.WriteLine(line);
			}

			//If aSeqDesc.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theIkChains IsNot Nothing Then
			if (aSequenceDesc.theIkLocks != null && theMdlFileData.theIkChains != null)
			{
				SourceMdlIkLock ikLock = null;

				for (int ikLockIndex = 0; ikLockIndex < aSequenceDesc.theIkLocks.Count; ikLockIndex++)
				{
					//If ikLockIndex >= Me.theSourceEngineModel.theMdlFileHeader.theIkLocks.Count Then
					//	Continue For
					//End If
					//ikLock = Me.theSourceEngineModel.theMdlFileHeader.theIkLocks(ikLockIndex)
					ikLock = aSequenceDesc.theIkLocks[ikLockIndex];

					//iklock <chain name> <pos lock> <angle lock>
					line = "\t";
					line += "iklock \"";
					line += theMdlFileData.theIkChains[ikLock.chainIndex].theName;
					line += "\"";
					line += " ";
					line += ikLock.posWeight.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += ikLock.localQWeight.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			WriteKeyValues(aSequenceDesc.theKeyValues, "keyvalues");

			WriteSequenceLayerInfo(aSequenceDesc);

			WriteSequenceNodeInfo(aSequenceDesc);

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_CYCLEPOSE) > 0)
			{
				line = "\t";
				line += "posecycle ";
				line += theMdlFileData.thePoseParamDescs[aSequenceDesc.cyclePoseIndex].theName;
				theOutputFileStreamWriter.WriteLine(line);
			}

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_REALTIME) > 0)
			{
				line = "\t";
				line += "realtime";
				theOutputFileStreamWriter.WriteLine(line);
			}

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_SNAP) > 0)
			{
				line = "\t";
				line += "snap";
				theOutputFileStreamWriter.WriteLine(line);
			}

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_WORLD) > 0)
			{
				line = "\t";
				line += "worldspace";
				theOutputFileStreamWriter.WriteLine(line);
			}

			//If blah Then
			//	line = vbTab
			//	line += ""
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If

			SourceMdlAnimationDesc49 firstAnimDesc = theMdlFileData.theAnimationDescs[aSequenceDesc.theAnimDescIndexes[0]];
			// Only write animation options if sequence has an impliedAnimDesc.
			if (impliedAnimDesc != null)
			{
				WriteAnimationOptions(aSequenceDesc, firstAnimDesc, impliedAnimDesc);
			}
		}

		//angles
		//autoik
		//blockname
		//cmdlist
		//fps                // done
		//frame              // baked-in and decompiles as separate anim smd files
		//fudgeloop
		//if
		//loop               // done
		//motionrollback
		//noanimblock
		//noanimblockstall
		//noautoik
		//origin
		//post               // baked-in 
		//rotate
		//scale
		//snap               // ($sequence version handled elsewhere)
		//startloop
		//ParseCmdlistToken( panim->numcmds, panim->cmds )
		//TODO: All these options (LX, LY, etc.) seem to be baked-in, but might need to be calculated for anims that have movement.
		//lookupControl( token )       
		private void WriteAnimationOptions(SourceMdlSequenceDesc aSequenceDesc, SourceMdlAnimationDesc49 anAnimationDesc, SourceMdlAnimationDesc49 impliedAnimDesc)
		{
			string line = "";

			line = "\t";
			line += "fps ";
			line += anAnimationDesc.fps.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);

			if (aSequenceDesc == null)
			{
				if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_LOOPING) > 0)
				{
					line = "\t";
					line += "loop";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
			else
			{
				if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_LOOPING) > 0)
				{
					line = "\t";
					line += "loop";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			if (!string.IsNullOrEmpty(anAnimationDesc.theCorrectiveAnimationName))
			{
				//NOTE: Using first linked sequence because the corrective animation SMD file should be the same no matter what sequence uses the animation.
				WriteAnimationOrSequenceSubtractOption(anAnimationDesc.theCorrectiveAnimationName);
			}

			WriteCmdListOptions(aSequenceDesc, anAnimationDesc, impliedAnimDesc);
		}

		//align
		//alignbone
		//alignboneto
		//alignto
		//compress
		//counterrotate
		//counterrotateto
		//derivative
		//fixuploop          // baked-in
		//ikfixup
		//ikrule
		//lineardelta
		//localhierarchy     // done
		//match
		//matchblend
		//noanimation        // done
		//numframes
		//presubtract
		//rotateto
		//splinedelta
		//subtract
		//walkalign
		//walkalignto
		//walkframe
		//weightlist         // done
		//worldspaceblend       //
		//worldspaceblendloop   // 
		//	while (TokenAvailable())
		//	{
		//		GetToken( false );
		//		if (stricmp( token, "height" ) == 0)
		//		{
		//			GetToken( false );
		//			pRule->height = verify_atof( token );
		//		}
		//		else if (stricmp( token, "target" ) == 0)
		//		{
		//			// slot
		//			GetToken( false );
		//			pRule->slot = verify_atoi( token );
		//		}
		//		else if (stricmp( token, "range" ) == 0)
		//		{
		//			// ramp
		//			GetToken( false );
		//			if (token[0] == '.')
		//				pRule->start = -1;
		//			else
		//				pRule->start = verify_atoi( token );
		//
		//			GetToken( false );
		//			if (token[0] == '.')
		//				pRule->peak = -1;
		//			else
		//				pRule->peak = verify_atoi( token );
		//	
		//			GetToken( false );
		//			if (token[0] == '.')
		//				pRule->tail = -1;
		//			else
		//				pRule->tail = verify_atoi( token );
		//
		//			GetToken( false );
		//			if (token[0] == '.')
		//				pRule->end = -1;
		//			else
		//				pRule->end = verify_atoi( token );
		//		}
		//		else if (stricmp( token, "floor" ) == 0)
		//		{
		//			GetToken( false );
		//			pRule->floor = verify_atof( token );
		//		}
		//		else if (stricmp( token, "pad" ) == 0)
		//		{
		//			GetToken( false );
		//			pRule->radius = verify_atof( token ) / 2.0f;
		//		}
		//		else if (stricmp( token, "radius" ) == 0)
		//		{
		//			GetToken( false );
		//			pRule->radius = verify_atof( token );
		//		}
		//		else if (stricmp( token, "contact" ) == 0)
		//		{
		//			GetToken( false );
		//			pRule->contact = verify_atoi( token );
		//		}
		//		else if (stricmp( token, "usesequence" ) == 0)
		//		{
		//			pRule->usesequence = true;
		//			pRule->usesource = false;
		//		}
		//		else if (stricmp( token, "usesource" ) == 0)
		//		{
		//			pRule->usesequence = false;
		//			pRule->usesource = true;
		//		}
		//		else if (stricmp( token, "fakeorigin" ) == 0)
		//		{
		//			GetToken( false );
		//			pRule->pos.x = verify_atof( token );
		//			GetToken( false );
		//			pRule->pos.y = verify_atof( token );
		//			GetToken( false );
		//			pRule->pos.z = verify_atof( token );
		//
		//			pRule->bone = -1;
		//		}
		//		else if (stricmp( token, "fakerotate" ) == 0)
		//		{
		//			QAngle ang;
		//
		//			GetToken( false );
		//			ang.x = verify_atof( token );
		//			GetToken( false );
		//			ang.y = verify_atof( token );
		//			GetToken( false );
		//			ang.z = verify_atof( token );
		//
		//			AngleQuaternion( ang, pRule->q );
		//
		//			pRule->bone = -1;
		//		}
		//		else if (stricmp( token, "bone" ) == 0)
		//		{
		//			strcpy( pRule->bonename, token );
		//		}
		//		else
		//		{
		//			UnGetToken();
		//			return;
		//		}
		//	}
		//------
		//		pikrule->slot	= srcanim->ikrule[j].slot;
		//		pikrule->pos	= srcanim->ikrule[j].pos;
		//		pikrule->q		= srcanim->ikrule[j].q;
		//		pikrule->height	= srcanim->ikrule[j].height;
		//		pikrule->floor	= srcanim->ikrule[j].floor;
		//		pikrule->radius = srcanim->ikrule[j].radius;
		//
		//		if (srcanim->numframes > 1.0)
		//		{
		//			pikrule->start	= srcanim->ikrule[j].start / (srcanim->numframes - 1.0f);
		//			pikrule->peak	= srcanim->ikrule[j].peak / (srcanim->numframes - 1.0f);
		//			pikrule->tail	= srcanim->ikrule[j].tail / (srcanim->numframes - 1.0f);
		//			pikrule->end	= srcanim->ikrule[j].end / (srcanim->numframes - 1.0f);
		//			pikrule->contact= srcanim->ikrule[j].contact / (srcanim->numframes - 1.0f);
		//		}
		//		else
		//		{
		//			pikrule->start	= 0.0f;
		//			pikrule->peak	= 0.0f;
		//			pikrule->tail	= 1.0f;
		//			pikrule->end	= 1.0f;
		//			pikrule->contact= 0.0f;
		//		}
		//
		//		pikrule->iStart = srcanim->ikrule[j].start;
		//
		//		if (strlen( srcanim->ikrule[j].attachment ) > 0)
		//		{
		//			// don't use string table, we're probably not in the same file.
		//			int size = strlen( srcanim->ikrule[j].attachment ) + 1;
		//			strcpy( (char *)pData, srcanim->ikrule[j].attachment );
		//			pikrule->szattachmentindex = pData - (byte *)pikrule;
		//			pData += size;
		//		}
		//------
		// Other sub-options for ikrule option.
		//	bone          - Looks like this data is not stored directly and might not be extractable from other data.
		//	contact       /
		//	fakeorigin    - 
		//	fakerotate    - 
		//	floor         /
		//	height        /
		//	pad           X redundant with radius; studiomdl radius = (pad / 2)
		//	radius        /
		//	range         /
		//	target        /
		//	usesequence   X baked-in   [converted into mstudiocompressedikerror_t?][Test to see if this is baked-in by doing test decompile+recompiles.]
		//	usesource     X baked-in   [converted into mstudiocompressedikerror_t?][Test to see if this is baked-in by doing test decompile+recompiles.]
		//If anIkRule.type = SourceMdlIkRule.IK_UNLATCH Then
		//	line += " "
		//	line += "usesource"
		//End If
		private void WriteCmdListOptions(SourceMdlSequenceDesc aSequenceDesc, SourceMdlAnimationDesc49 anAnimationDesc, SourceMdlAnimationDesc49 impliedAnimDesc)
		{
			string line = "";

			if (anAnimationDesc.theIkRules != null)
			{
				int endFrameIndex = 0;
				int tempInteger = 0;
				bool valueIsWrappedAround = false;
				endFrameIndex = anAnimationDesc.frameCount - 1;

//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				SourceMdlAutoLayer layer = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				SourceMdlSequenceDesc otherSequence = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				SourceMdlAnimationDesc49 otherAnimationDesc = null;
				foreach (SourceMdlIkRule anIkRule in anAnimationDesc.theIkRules)
				{
					line = "\t";
					line += "ikrule";
					line += " \"";
					line += theMdlFileData.theIkChains[anIkRule.chain].theName;
					line += "\"";
					if (anIkRule.type == SourceMdlIkRule.IK_SELF)
					{
						line += " ";
						line += "touch";
						line += " \"";
						if (anIkRule.bone >= 0)
						{
							line += theMdlFileData.theBones[anIkRule.bone].theName;
						}
						line += "\"";
						//ElseIf anIkRule.type = SourceMdlIkRule.IK_WORLD Then
						//line += " "
						//line += "world"
					}
					else if (anIkRule.type == SourceMdlIkRule.IK_GROUND)
					{
						line += " ";
						line += "footstep";
					}
					else if (anIkRule.type == SourceMdlIkRule.IK_RELEASE)
					{
						line += " ";
						line += "release";
					}
					else if (anIkRule.type == SourceMdlIkRule.IK_ATTACHMENT)
					{
						line += " ";
						line += "attachment";
						line += " \"";
						line += anIkRule.theAttachmentName;
						line += "\"";
					}
					else if (anIkRule.type == SourceMdlIkRule.IK_UNLATCH)
					{
						line += " ";
						line += "unlatch";
					}

					//NOTE: Writing all ikrule options because studiomdl will ignore any that are not used by a type.

					valueIsWrappedAround = false;
					tempInteger = Convert.ToInt32(Math.Round(anIkRule.contact * endFrameIndex));
					//NOTE: Subtract max frame from value if over max frame. 
					while (tempInteger > endFrameIndex)
					{

						tempInteger -= endFrameIndex;
						valueIsWrappedAround = true;
					}

					line += " contact ";
					line += tempInteger.ToString(MainCROWBAR.TheApp.InternalNumberFormat);

					line += " fakeorigin ";
					line += anIkRule.pos.x.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += anIkRule.pos.y.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += anIkRule.pos.z.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					SourceVector angles = MathModule.ToEulerAngles(anIkRule.q);
					line += " fakerotate ";
					line += angles.x.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += angles.y.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += angles.z.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " floor ";
					line += anIkRule.floor.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " height ";
					line += anIkRule.height.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					//NOTE: Not using pad because radius option can be used instead.
					//'pRule->radius = verify_atof( token ) / 2.0f;
					//line += " pad "
					//line += (anIkRule.radius * 2).ToString("0.##", TheApp.InternalNumberFormat)

					//pRule->radius = verify_atof( token );
					line += " radius ";
					line += anIkRule.radius.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " range ";
					tempInteger = Convert.ToInt32(Math.Round(anIkRule.influenceStart * endFrameIndex));
					//NOTE: Subtract max frame from value if over max frame. 
					while (tempInteger > endFrameIndex)
					{

						tempInteger -= endFrameIndex;
						valueIsWrappedAround = true;
					}

					line += tempInteger.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					tempInteger = Convert.ToInt32(Math.Round(anIkRule.influencePeak * endFrameIndex));
					//NOTE: Subtract max frame from value if over max frame. 
					while (tempInteger > endFrameIndex)
					{

						tempInteger -= endFrameIndex;
						valueIsWrappedAround = true;
					}

					line += tempInteger.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					tempInteger = Convert.ToInt32(Math.Round(anIkRule.influenceTail * endFrameIndex));
					//NOTE: Subtract max frame from value if over max frame. 
					//      Example model that needs this: "h3_hunter.mdl" (Compiled from source files from someone on Discord.)
					while (tempInteger > endFrameIndex)
					{
						tempInteger -= endFrameIndex;
						valueIsWrappedAround = true;
					}

					line += tempInteger.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					tempInteger = Convert.ToInt32(Math.Round(anIkRule.influenceEnd * endFrameIndex));
					//NOTE: Subtract max frame from value if over max frame. 
					//      Example model that needs this: Half-Life 2 Deathmatch > "models\combine_soldier_anims.mdl"
					while (tempInteger > endFrameIndex)
					{
						tempInteger -= endFrameIndex;
						valueIsWrappedAround = true;
					}

					line += tempInteger.ToString(MainCROWBAR.TheApp.InternalNumberFormat);

					line += " target ";
					line += anIkRule.slot.ToString(MainCROWBAR.TheApp.InternalNumberFormat);

					// Example model that needs this: "h3_hunter.mdl" [2019-06-19 decompile missing ikchain info] (Compiled from source files from someone on Discord.)
					// Another example: HL2 "alyx_animations.mdl"
					// A MDL v48 needs this, so most likely v49 does, too.
					if (aSequenceDesc != null)
					{
						if (valueIsWrappedAround)
						{
							line += " usesequence ";
						}
						else if (anAnimationDesc.theMovements != null && anAnimationDesc.theMovements.Count > 0)
						{
							line += " usesequence ";
						}
						else if (aSequenceDesc.theAutoLayers != null && aSequenceDesc.theAutoLayers.Count > 0)
						{
							//NOTE: Make this check 'ElseIf' check last so that other checks have chance to add the usesequence option.
	//						Dim layer As SourceMdlAutoLayer
	//						Dim otherSequence As SourceMdlSequenceDesc
	//						Dim otherAnimationDesc As SourceMdlAnimationDesc49
							for (int j = 0; j < aSequenceDesc.theAutoLayers.Count; j++)
							{
								layer = aSequenceDesc.theAutoLayers[j];
								otherSequence = theMdlFileData.theSequenceDescs[layer.sequenceIndex];
								for (int k = 0; k < aSequenceDesc.theAnimDescIndexes.Count; k++)
								{
									otherAnimationDesc = theMdlFileData.theAnimationDescs[otherSequence.theAnimDescIndexes[k]];
									if (otherAnimationDesc.frameCount > anAnimationDesc.frameCount)
									{
										line += " usesequence ";
										// Set j to max so outer loop stops.
										j = aSequenceDesc.theAutoLayers.Count;
										break;
									}
								}
							}
							//ElseIf tempCountertrippedMoreThanOnce Then
							//	line += " usesequence "
						}
					}

					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			//$sequence taunt01 "taunt01.dmx" fps 30 localhierarchy "weapon_bone" "bip_hand_L" range 0 5 80 90 {
			//if (srcanim->numframes > 1.0)
			//{
			//	pHierarchy->start	= srcanim->localhierarchy[j].start / (srcanim->numframes - 1.0f);
			//	pHierarchy->peak	= srcanim->localhierarchy[j].peak / (srcanim->numframes - 1.0f);
			//	pHierarchy->tail	= srcanim->localhierarchy[j].tail / (srcanim->numframes - 1.0f);
			//	pHierarchy->end		= srcanim->localhierarchy[j].end / (srcanim->numframes - 1.0f);
			//}
			//              Else
			//{
			//	pHierarchy->start	= 0.0f;
			//	pHierarchy->peak	= 0.0f;
			//	pHierarchy->tail	= 1.0f;
			//	pHierarchy->end		= 1.0f;
			//}
			//NOTE: Reverse calculation of above: qc = mdl * (srcanim->numframes - 1.0f)
			if (impliedAnimDesc == null)
			{
				WriteCmdListLocalHierarchyOption(anAnimationDesc);
			}
			else
			{
				WriteCmdListLocalHierarchyOption(impliedAnimDesc);
			}

			//If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) > 0 Then
			//	line = vbTab
			//	line += "noanimation"
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If

			// Commented-out the subtract option because recompile should work if left out, even though original QC would have it along with animation SMD before subtract operation.
			//'TODO: This seems valid according to source code, but it checks same flag (STUDIO_DELTA) as "delta" option.
			//'      Unsure how to determine which option is intended or if both are intended.
			//If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
			//	line = vbTab
			//	If Me.theMdlFileData.theFirstAnimationDesc IsNot Nothing Then
			//		line += "// The MDL does not store which $animation and which frame were used, so Crowbar uses the name of the first $animation. Change as needed."
			//	Else
			//		line += "// The MDL does not store which $animation or $sequence and which frame were used, so Crowbar uses the name of the first $sequence. Change as needed."
			//	End If
			//	Me.theOutputFileStreamWriter.WriteLine(line)

			//	line = vbTab
			//	line += "// The subtract line is commented-out because Crowbar does not undo the subtract used to compile the MDL. Recompiling should work, but the animation will likely look weird in an animation tool."
			//	Me.theOutputFileStreamWriter.WriteLine(line)

			//	line = vbTab
			//	line += "// "
			//	line += "subtract"
			//	line += " """
			//	'TODO: Change to writing anim_name.
			//	' Doesn't seem to be direct way to get this name.
			//	' For now, do what MDL Decompiler seems to do; use the first animation name.
			//	'line += "[anim_name]"
			//	'line += Me.theFirstAnimationDescName
			//	If Me.theMdlFileData.theFirstAnimationDesc IsNot Nothing Then
			//		line += Me.theMdlFileData.theFirstAnimationDesc.theName
			//	Else
			//		line += Me.theMdlFileData.theSequenceDescs(0).theName
			//	End If
			//	line += """ "
			//	'TODO: Change to writing frameIndex.
			//	' Doesn't seem to be direct way to get this value.
			//	' For now, do what MDL Decompiler seems to do; use zero for the frameIndex.
			//	'line += "[frameIndex]"
			//	line += "0"
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If

			//NOTE: L4D2 anim_hulk has several sequences that have walkframe option, so this commented-out line can not be correct.
			//If aSequenceDesc Is Nothing AndAlso anAnimationDesc.theMovements IsNot Nothing Then
			if (anAnimationDesc.theMovements != null && anAnimationDesc.theMovements.Count > 0)
			{
				foreach (SourceMdlMovement aMovement in anAnimationDesc.theMovements)
				{
					line = "\t";
					line += "walkframe";
					line += " ";
					line += aMovement.endframeIndex.ToString();
					if ((aMovement.motionFlags & SourceMdlMovement.STUDIO_LX) > 0)
					{
						line += " ";
						line += "LX";
					}
					if ((aMovement.motionFlags & SourceMdlMovement.STUDIO_LY) > 0)
					{
						line += " ";
						line += "LY";
					}
					if ((aMovement.motionFlags & SourceMdlMovement.STUDIO_LZ) > 0)
					{
						line += " ";
						line += "LZ";
					}
					if ((aMovement.motionFlags & SourceMdlMovement.STUDIO_LXR) > 0)
					{
						line += " ";
						line += "LXR";
					}
					if ((aMovement.motionFlags & SourceMdlMovement.STUDIO_LYR) > 0)
					{
						line += " ";
						line += "LYR";
					}
					if ((aMovement.motionFlags & SourceMdlMovement.STUDIO_LZR) > 0)
					{
						line += " ";
						line += "LZR";
					}
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			//TODO: Can probably reduce the info written in v0.24.
			// weightlist "top_bottom"
			SourceMdlSequenceDesc aSeqDesc = null;
			if (aSequenceDesc == null)
			{
				if (anAnimationDesc.theAnimIsLinkedToSequence)
				{
					//NOTE: Just get first one, because all should have same bone weights.
					aSeqDesc = anAnimationDesc.theLinkedSequences[0];
				}
			}
			else
			{
				aSeqDesc = aSequenceDesc;
			}
			//If aSeqDesc IsNot Nothing AndAlso aSeqDesc.theBoneWeights IsNot Nothing AndAlso aSeqDesc.theBoneWeights.Count > 0 AndAlso Not aSeqDesc.theBoneWeightsAreDefault Then
			//	Me.WriteSequenceWeightListLine(aSeqDesc)
			//End If
			if (aSeqDesc != null && aSeqDesc.theWeightListIndex > -1)
			{
				WriteSequenceWeightListLine(aSeqDesc);
			}
		}

		private void WriteSequenceWeightListLine(SourceMdlSequenceDesc aSeqDesc)
		{
			string line = "";

			line = "\t";
			line += "weightlist ";
			//NOTE: Name is not stored, so use something reasonable. Needs to be the same as used in $weightlist.
			line += "\"";
			//line += "weights_"
			//line += aSeqDesc.theName
			line += theMdlFileData.theWeightLists[aSeqDesc.theWeightListIndex].theName;
			line += "\"";
			theOutputFileStreamWriter.WriteLine(line);
		}

		private void WriteSequenceBlendInfo(SourceMdlSequenceDesc aSeqDesc)
		{
			string line = "";

			for (int i = 0; i <= 1; i++)
			{
				if (aSeqDesc.paramIndex[i] != -1)
				{
					line = "\t";
					line += "blend ";
					line += "\"";
					line += theMdlFileData.thePoseParamDescs[aSeqDesc.paramIndex[i]].theName;
					line += "\"";
					line += " ";
					line += aSeqDesc.paramStart[i].ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aSeqDesc.paramEnd[i].ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteSequenceDeltaInfo(SourceMdlSequenceDesc aSequenceDesc)
		{
			string line = "";

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_DELTA) > 0)
			{
				if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_POST) > 0)
				{
					line = "\t";
					//line += "// "
					line += "delta";
					theOutputFileStreamWriter.WriteLine(line);
				}
				else
				{
					line = "\t";
					line += "predelta";
					theOutputFileStreamWriter.WriteLine(line);
				}

				//'TODO: [2019-06-12] Probably should remove this in favor of different workaround as indicated by this workshop guide:
				//'      "Workaround to Recompile Models that Have Problems Due to Delta Animations"
				//'      https://steamcommunity.com/sharedfiles/filedetails/?id=1774302855
				///NOTE: [2017-12-15] Added this code to fix the issue of the jigglebones+delta problem when recompiling L4D2 survivor_teenangst_light.
				//'Dim anAnimationDesc As SourceMdlAnimationDesc49
				//'anAnimationDesc = Me.theMdlFileData.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(0))
				//'If anAnimationDesc.theName = "@" + aSequenceDesc.theName Then
				//'	Me.WriteSequenceSubtractOption(aSequenceDesc.theName)
				//'End If
				//======
				if (!string.IsNullOrEmpty(aSequenceDesc.theCorrectiveAnimationName))
				{
					WriteAnimationOrSequenceSubtractOption(aSequenceDesc.theCorrectiveAnimationName);
				}
			}
		}

		private void WriteAnimationOrSequenceSubtractOption(string anAnimationOrSequenceName)
		{
			string line = "";

			line = "\t";
			line += "// Crowbar writes this subtract option to prevent jigglebone and poseparameter problems when delta sequences are recompiled.";
			theOutputFileStreamWriter.WriteLine(line);

			line = "\t";
			line += "subtract";
			line += " \"";
			line += anAnimationOrSequenceName;
			line += "\" ";
			line += "0";
			theOutputFileStreamWriter.WriteLine(line);
		}

		private void WriteSequenceLayerInfo(SourceMdlSequenceDesc aSeqDesc)
		{
			if (aSeqDesc.autoLayerCount > 0)
			{
				string line = "";
				SourceMdlAutoLayer layer = null;
				string otherSequenceName = null;

//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				double influenceStart = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				double influencePeak = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				double influenceTail = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				double influenceEnd = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string influenceStartText = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string influencePeakText = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string influenceTailText = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string influenceEndText = null;
				for (int j = 0; j < aSeqDesc.theAutoLayers.Count; j++)
				{
					layer = aSeqDesc.theAutoLayers[j];
					otherSequenceName = theMdlFileData.theSequenceDescs[layer.sequenceIndex].theName;

					//NOTE: [WriteSequenceLayerInfo] [22-Jul-2018] Is there any reason to distinguish addlayer and blendlayer options by checking influnceStart, -Peak, -Tail, and -End?
					//      Yes! Example: blendlayer 1 2 3 4
					//      Instead of checking all influence* values, check instead "if influenceStart = influenceEnd" because of this line in the source code [simplify.cpp AccumulateSeqLayers() line 5365]:
					//          if (pLayer->start != pLayer->end)
					//If layer.flags = 0 AndAlso layer.influenceStart = 0 AndAlso layer.influencePeak = 0 AndAlso layer.influenceTail = 0 AndAlso layer.influenceEnd = 0 Then
					if (layer.flags == 0 && layer.influenceStart == layer.influenceEnd)
					{
						//addlayer <string|other $sequence name>
						line = "\t";
						//line += "// "
						line += "addlayer ";
						line += "\"";
						line += otherSequenceName;
						line += "\"";
						theOutputFileStreamWriter.WriteLine(line);
					}
					else
					{
						//blendlayer <string|other $sequence name> <int|startframe> <int|peakframe> <int|tailframe> <int|endframe> [spline] [xfade]
						line = "\t";
						line += "blendlayer ";
						line += "\"";
						line += otherSequenceName;
						line += "\"";

						SourceMdlAnimationDesc49 anAnimationDesc = theMdlFileData.theAnimationDescs[aSeqDesc.theAnimDescIndexes[0]];
						int endFrameIndex = (anAnimationDesc.frameCount - 1);
	//					Dim influenceStart As Double
	//					Dim influencePeak As Double
	//					Dim influenceTail As Double
	//					Dim influenceEnd As Double
	//					Dim influenceStartText As String
	//					Dim influencePeakText As String
	//					Dim influenceTailText As String
	//					Dim influenceEndText As String
						if ((layer.flags & SourceMdlAutoLayer.STUDIO_AL_POSE) == 0)
						{
							influenceStart = layer.influenceStart * endFrameIndex;
							//'NOTE: Subtract max frame from value if over max frame. 
							//While influenceStart > endFrameIndex
							//	influenceStart -= endFrameIndex
							//End While
							influenceStartText = influenceStart.ToString("0", MainCROWBAR.TheApp.InternalNumberFormat);

							influencePeak = layer.influencePeak * endFrameIndex;
							//'NOTE: Subtract max frame from value if over max frame. 
							//While influencePeak > endFrameIndex
							//	influencePeak -= endFrameIndex
							//End While
							influencePeakText = influencePeak.ToString("0", MainCROWBAR.TheApp.InternalNumberFormat);

							influenceTail = layer.influenceTail * endFrameIndex;
							//'NOTE: Subtract max frame from value if over max frame. 
							//While influenceTail > endFrameIndex
							//	influenceTail -= endFrameIndex
							//End While
							influenceTailText = influenceTail.ToString("0", MainCROWBAR.TheApp.InternalNumberFormat);

							influenceEnd = layer.influenceEnd * endFrameIndex;
							//NOTE: Limit to max frame. 
							//      Example model that needs this: Half-Life 2 Deathmatch > "models\combine_soldier_anims.mdl"
							//If influenceEnd > endFrameIndex Then
							//	influenceEnd = endFrameIndex
							//End If
							//======
							//'NOTE: Subtract max frame from value if over max frame. 
							//While influenceEnd > endFrameIndex
							//	influenceEnd -= endFrameIndex
							//End While
							influenceEndText = influenceEnd.ToString("0", MainCROWBAR.TheApp.InternalNumberFormat);
						}
						else
						{
							influenceStartText = layer.influenceStart.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
							influencePeakText = layer.influencePeak.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
							influenceTailText = layer.influenceTail.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
							influenceEndText = layer.influenceEnd.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						}
						line += " ";
						line += influenceStartText;
						line += " ";
						line += influencePeakText;
						line += " ";
						line += influenceTailText;
						line += " ";
						line += influenceEndText;

						if ((layer.flags & SourceMdlAutoLayer.STUDIO_AL_XFADE) > 0)
						{
							line += " xfade";
						}
						if ((layer.flags & SourceMdlAutoLayer.STUDIO_AL_SPLINE) > 0)
						{
							line += " spline";
						}
						if ((layer.flags & SourceMdlAutoLayer.STUDIO_AL_NOBLEND) > 0)
						{
							line += " noblend";
						}
						if ((layer.flags & SourceMdlAutoLayer.STUDIO_AL_POSE) > 0)
						{
							if (theMdlFileData.thePoseParamDescs != null && theMdlFileData.thePoseParamDescs.Count > layer.poseIndex)
							{
								line += " poseparameter";
								line += " ";
								line += theMdlFileData.thePoseParamDescs[layer.poseIndex].theName;
							}
						}
						if ((layer.flags & SourceMdlAutoLayer.STUDIO_AL_LOCAL) > 0)
						{
							line += " local";
						}

						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		private void WriteSequenceNodeInfo(SourceMdlSequenceDesc aSeqDesc)
		{
			string line = "";

			if (aSeqDesc.localEntryNodeIndex > 0)
			{
				if (aSeqDesc.localEntryNodeIndex == aSeqDesc.localExitNodeIndex)
				{
					//node (name)
					line = "\t";
					line += "node";
					line += " \"";
					//NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
					line += theMdlFileData.theLocalNodeNames[aSeqDesc.localEntryNodeIndex - 1];
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
				else if ((aSeqDesc.nodeFlags & 1) == 0)
				{
					//transition (from) (to) 
					line = "\t";
					line += "transition";
					line += " \"";
					//NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
					line += theMdlFileData.theLocalNodeNames[aSeqDesc.localEntryNodeIndex - 1];
					line += "\" \"";
					//NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
					line += theMdlFileData.theLocalNodeNames[aSeqDesc.localExitNodeIndex - 1];
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
				else
				{
					//rtransition (name1) (name2) 
					line = "\t";
					line += "rtransition";
					line += " \"";
					//NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
					line += theMdlFileData.theLocalNodeNames[aSeqDesc.localEntryNodeIndex - 1];
					line += "\" \"";
					//NOTE: Use the "-1" at end because the indexing is one-based in the mdl file.
					line += theMdlFileData.theLocalNodeNames[aSeqDesc.localExitNodeIndex - 1];
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteCmdListLocalHierarchyOption(SourceMdlAnimationDesc49 anAnimationDesc)
		{
			string line = "";

			if (anAnimationDesc.theLocalHierarchies != null)
			{
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				SourceMdlLocalHierarchy aLocalHierarchy = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int frameCount = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				double startInfluence = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				double peakInfluence = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				double tailInfluence = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				double endInfluence = 0;
				for (int hierarchyIndex = 0; hierarchyIndex < anAnimationDesc.theLocalHierarchies.Count; hierarchyIndex++)
				{
	//				Dim aLocalHierarchy As SourceMdlLocalHierarchy
	//				Dim frameCount As Integer
	//				Dim startInfluence As Double
	//				Dim peakInfluence As Double
	//				Dim tailInfluence As Double
	//				Dim endInfluence As Double

					aLocalHierarchy = anAnimationDesc.theLocalHierarchies[hierarchyIndex];
					frameCount = anAnimationDesc.frameCount;
					startInfluence = aLocalHierarchy.startInfluence * (frameCount - 1);
					peakInfluence = aLocalHierarchy.peakInfluence * (frameCount - 1);
					tailInfluence = aLocalHierarchy.tailInfluence * (frameCount - 1);
					endInfluence = aLocalHierarchy.endInfluence * (frameCount - 1);

					line = "\t";
					line += "localhierarchy";
					line += " \"";
					line += theMdlFileData.theBones[aLocalHierarchy.boneIndex].theName;
					line += "\"";
					line += " \"";
					if (aLocalHierarchy.boneNewParentIndex >= 0 && aLocalHierarchy.boneNewParentIndex < theMdlFileData.theBones.Count)
					{
						line += theMdlFileData.theBones[aLocalHierarchy.boneNewParentIndex].theName;
					}
					line += "\"";
					line += " range ";
					line += startInfluence.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += peakInfluence.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += tailInfluence.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += endInfluence.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteIkChainCommand()
		{
			string line = "";
			double offsetX = 0;
			double offsetY = 0;
			double offsetZ = 0;

			//$ikchain rhand ValveBiped.Bip01_R_Hand knee  0.707 0.707 0.000
			//$ikchain lhand ValveBiped.Bip01_L_Hand knee  0.707 0.707 0.000
			//$ikchain rfoot ValveBiped.Bip01_R_Foot knee  0.707 -0.707 0.000
			//$ikchain lfoot ValveBiped.Bip01_L_Foot knee  0.707 -0.707 0.000
			//$ikchain ikclip ValveBiped.weapon_bone_Clip knee  0.707 -0.707 0.000
			try
			{
				if (theMdlFileData.theIkChains != null)
				{
					line = "";
					theOutputFileStreamWriter.WriteLine(line);

					for (int i = 0; i < theMdlFileData.theIkChains.Count; i++)
					{
						int boneIndex = theMdlFileData.theIkChains[i].theLinks[theMdlFileData.theIkChains[i].theLinks.Count - 1].boneIndex;
						offsetX = Math.Round(theMdlFileData.theIkChains[i].theLinks[0].idealBendingDirection.x, 3);
						offsetY = Math.Round(theMdlFileData.theIkChains[i].theLinks[0].idealBendingDirection.y, 3);
						offsetZ = Math.Round(theMdlFileData.theIkChains[i].theLinks[0].idealBendingDirection.z, 3);

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$IKChain \"";
						}
						else
						{
							line = "$ikchain \"";
						}
						line += theMdlFileData.theIkChains[i].theName;
						line += "\" \"";
						line += theMdlFileData.theBones[boneIndex].theName;
						line += "\" knee ";
						line += offsetX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += offsetY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += offsetZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		private void WriteIkAutoPlayLockCommand()
		{
			string line = "";
			SourceMdlIkLock ikLock = null;

			//$ikautoplaylock <chain name> <pos lock> <angle lock>
			//$ikautoplaylock rfoot 1.0 0.1
			//$ikautoplaylock lfoot 1.0 0.1
			try
			{
				if (theMdlFileData.theIkLocks != null)
				{
					line = "";
					theOutputFileStreamWriter.WriteLine(line);

					for (int i = 0; i < theMdlFileData.theIkLocks.Count; i++)
					{
						ikLock = theMdlFileData.theIkLocks[i];

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$IKAutoPlayLock \"";
						}
						else
						{
							line = "$ikautoplaylock \"";
						}
						line += theMdlFileData.theIkChains[ikLock.chainIndex].theName;
						line += "\"";
						line += " ";
						line += ikLock.posWeight.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += ikLock.localQWeight.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		private void WriteBoneSaveFrameCommand()
		{
			string line = "";

			//$bonesaveframe <bone name> ["position"] ["rotation"]
			//$BoneSaveFrame "Dog_Model.Pelvis" position rotation
			//$BoneSaveFrame "Dog_Model.Leg1_L" rotation
			try
			{
				if (theMdlFileData.theBones != null)
				{
					SourceMdlBone aBone = null;
					bool emptyLineIsAlreadyWritten = false;

					for (int i = 0; i < theMdlFileData.theBones.Count; i++)
					{
						aBone = theMdlFileData.theBones[i];

						if ((aBone.flags & SourceMdlBone.BONE_HAS_SAVEFRAME_POS) > 0 || (aBone.flags & SourceMdlBone.BONE_HAS_SAVEFRAME_ROT) > 0)
						{
							if (!emptyLineIsAlreadyWritten)
							{
								line = "";
								theOutputFileStreamWriter.WriteLine(line);
								emptyLineIsAlreadyWritten = true;
							}

							if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
							{
								line = "$BoneSaveFrame ";
							}
							else
							{
								line = "$bonesaveframe ";
							}
							line += "\"";
							line += aBone.theName;
							line += "\"";
							if ((aBone.flags & SourceMdlBone.BONE_HAS_SAVEFRAME_POS) > 0)
							{
								line += " ";
								line += "position";
							}
							if ((aBone.flags & SourceMdlBone.BONE_HAS_SAVEFRAME_ROT) > 0)
							{
								line += " ";
								line += "rotation";
							}
							theOutputFileStreamWriter.WriteLine(line);
						}
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		public void WriteGroupCollision()
		{
			WriteCollisionModelOrCollisionJointsCommand();
			WriteCollisionTextCommand();
		}

		public void WriteCollisionModelOrCollisionJointsCommand()
		{
			string line = "";

			//NOTE: Data is from PHY file.
			//$collisionmodel "tree_deciduous_01a_physbox.smd"
			//{
			//	$mass 350.0
			//	$concave
			//}	
			//$collisionjoints "phymodel.smd"
			//{
			//	$mass 100.0
			//	$inertia 10.00
			//	$damping 0.05
			//	$rotdamping 5.00
			//	$rootbone "valvebiped.bip01_pelvis"
			//	$jointrotdamping "valvebiped.bip01_pelvis" 3.00
			//
			//	$jointmassbias "valvebiped.bip01_spine1" 8.00
			//	$jointconstrain "valvebiped.bip01_spine1" x limit -10.00 10.00 0.00
			//	$jointconstrain "valvebiped.bip01_spine1" y limit -16.00 16.00 0.00
			//	$jointconstrain "valvebiped.bip01_spine1" z limit -20.00 30.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_spine2" 9.00
			//	$jointconstrain "valvebiped.bip01_spine2" x limit -10.00 10.00 0.00
			//	$jointconstrain "valvebiped.bip01_spine2" y limit -10.00 10.00 0.00
			//	$jointconstrain "valvebiped.bip01_spine2" z limit -20.00 20.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_r_clavicle" 4.00
			//	$jointrotdamping "valvebiped.bip01_r_clavicle" 6.00
			//	$jointconstrain "valvebiped.bip01_r_clavicle" x limit -15.00 15.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_clavicle" y limit -10.00 10.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_clavicle" z limit -0.00 45.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_l_clavicle" 4.00
			//	$jointrotdamping "valvebiped.bip01_l_clavicle" 6.00
			//	$jointconstrain "valvebiped.bip01_l_clavicle" x limit -15.00 15.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_clavicle" y limit -10.00 10.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_clavicle" z limit -0.00 45.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_l_upperarm" 5.00
			//	$jointrotdamping "valvebiped.bip01_l_upperarm" 2.00
			//	$jointconstrain "valvebiped.bip01_l_upperarm" x limit -15.00 20.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_upperarm" y limit -40.00 32.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_upperarm" z limit -80.00 25.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_l_forearm" 4.00
			//	$jointrotdamping "valvebiped.bip01_l_forearm" 4.00
			//	$jointconstrain "valvebiped.bip01_l_forearm" x limit -40.00 15.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_forearm" y limit 0.00 0.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_forearm" z limit -120.00 10.00 0.00
			//
			//	$jointrotdamping "valvebiped.bip01_l_hand" 1.00
			//	$jointconstrain "valvebiped.bip01_l_hand" x limit -25.00 25.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_hand" y limit -35.00 35.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_hand" z limit -50.00 50.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_r_upperarm" 5.00
			//	$jointrotdamping "valvebiped.bip01_r_upperarm" 2.00
			//	$jointconstrain "valvebiped.bip01_r_upperarm" x limit -15.00 20.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_upperarm" y limit -40.00 32.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_upperarm" z limit -80.00 25.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_r_forearm" 4.00
			//	$jointrotdamping "valvebiped.bip01_r_forearm" 4.00
			//	$jointconstrain "valvebiped.bip01_r_forearm" x limit -40.00 15.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_forearm" y limit 0.00 0.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_forearm" z limit -120.00 10.00 0.00
			//
			//	$jointrotdamping "valvebiped.bip01_r_hand" 1.00
			//	$jointconstrain "valvebiped.bip01_r_hand" x limit -25.00 25.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_hand" y limit -35.00 35.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_hand" z limit -50.00 50.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_r_thigh" 7.00
			//	$jointrotdamping "valvebiped.bip01_r_thigh" 7.00
			//	$jointconstrain "valvebiped.bip01_r_thigh" x limit -25.00 25.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_thigh" y limit -10.00 15.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_thigh" z limit -55.00 25.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_r_calf" 4.00
			//	$jointconstrain "valvebiped.bip01_r_calf" x limit -10.00 25.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_calf" y limit -5.00 5.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_calf" z limit -10.00 115.00 0.00
			//
			//	$jointrotdamping "valvebiped.bip01_r_foot" 2.00
			//	$jointconstrain "valvebiped.bip01_r_foot" x limit -20.00 30.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_foot" y limit -30.00 20.00 0.00
			//	$jointconstrain "valvebiped.bip01_r_foot" z limit -30.00 50.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_l_thigh" 7.00
			//	$jointrotdamping "valvebiped.bip01_l_thigh" 7.00
			//	$jointconstrain "valvebiped.bip01_l_thigh" x limit -25.00 25.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_thigh" y limit -10.00 15.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_thigh" z limit -55.00 25.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_l_calf" 4.00
			//	$jointconstrain "valvebiped.bip01_l_calf" x limit -10.00 25.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_calf" y limit -5.00 5.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_calf" z limit -10.00 115.00 0.00
			//
			//	$jointrotdamping "valvebiped.bip01_l_foot" 2.00
			//	$jointconstrain "valvebiped.bip01_l_foot" x limit -20.00 30.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_foot" y limit -30.00 20.00 0.00
			//	$jointconstrain "valvebiped.bip01_l_foot" z limit -30.00 50.00 0.00
			//
			//	$jointmassbias "valvebiped.bip01_head1" 4.00
			//	$jointrotdamping "valvebiped.bip01_head1" 3.00
			//	$jointconstrain "valvebiped.bip01_head1" x limit -50.00 50.00 0.00
			//	$jointconstrain "valvebiped.bip01_head1" y limit -20.00 20.00 0.00
			//	$jointconstrain "valvebiped.bip01_head1" z limit -26.00 30.00 0.00
			//}
			if (thePhyFileData != null && thePhyFileData.solidCount > 0)
			{
				theOutputFileStreamWriter.WriteLine(line);

				//If Me.theSourceEngineModel.PhyFileHeader.checksum <> Me.theSourceEngineModel.MdlFileHeader.checksum Then
				//	line = "// The PHY file's checksum value is not the same as the MDL file's checksum value."
				//	Me.theOutputFileStreamWriter.WriteLine(line)
				//End If

				//NOTE: The smd file name for $collisionjoints is not stored in the mdl file, 
				//      so use the same name that MDL Decompiler uses.
				//TODO: Find a better way to determine which to use.
				//NOTE: "If Me.theSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count < 2" 
				//      works for survivors but not for witch (which has only one sequence).
				//If theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModels.Count < 2 Then
				//TODO: Why not use this "if" statement? It seems reasonable that a solid matches a set of convex shapes for one bone.
				//      For example, L4D2 van has several convex shapes, but only one solid and one bone.
				//      Same for w_minigun. Both use $concave.
				//If Me.theSourceEngineModel.thePhyFileHeader.solidCount = 1 Then
				if (thePhyFileData.theSourcePhyIsCollisionModel)
				{
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$CollisionModel ";
					}
					else
					{
						line = "$collisionmodel ";
					}
				}
				else
				{
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$CollisionJoints ";
					}
					else
					{
						line = "$collisionjoints ";
					}
				}
				//line += """phymodel.smd"""
				line += "\"";
				thePhyFileData.thePhysicsMeshSmdFileName = SourceFileNamesModule.CreatePhysicsSmdFileName(thePhyFileData.thePhysicsMeshSmdFileName, theModelName);
				line += thePhyFileData.thePhysicsMeshSmdFileName;
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
				line = "{";
				theOutputFileStreamWriter.WriteLine(line);

				WriteCollisionModelOrCollisionJointsOptions();

				line = "}";
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		//$animatedfriction
		//$assumeworldspace
		//$automass          // baked-in as mass
		//$concave           // done
		//$concaveperjoint
		//$damping           // done
		//$drag
		//$inertia           // done
		//$jointcollide
		//$jointconstrain    // done
		//$jointdamping      // done
		//$jointinertia      // done
		//$jointmassbias     // done
		//$jointmerge
		//$jointrotdamping   // done
		//$jointskip
		//$mass              // done
		//$masscenter
		//$maxconvexpieces
		//$noselfcollisions  //done
		//$remove2d
		//$rollingDrag
		//$rootbone          // done
		//$rotdamping        // done
		//$weldnormal
		//$weldposition
		private void WriteCollisionModelOrCollisionJointsOptions()
		{
			string line = "";

			line = "\t";
			line += "$mass ";
			line += thePhyFileData.theSourcePhyEditParamsSection.totalMass.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);
			line = "\t";
			line += "$inertia ";
			line += thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theInertia.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);
			line = "\t";
			line += "$damping ";
			line += thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);
			line = "\t";
			line += "$rotdamping ";
			line += thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);
			if (!string.IsNullOrEmpty(thePhyFileData.theSourcePhyEditParamsSection.rootName))
			{
				line = "\t";
				line += "$rootbone \"";
				line += thePhyFileData.theSourcePhyEditParamsSection.rootName;
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}
			if (thePhyFileData.theSourcePhyEditParamsSection.concave == "1")
			{
				line = "\t";
				line += "$concave";
				theOutputFileStreamWriter.WriteLine(line);
				line = "\t";
				line += "$maxconvexpieces ";
				line += thePhyFileData.theSourcePhyMaxConvexPieces.ToString();
				theOutputFileStreamWriter.WriteLine(line);
			}

			for (int i = 0; i < thePhyFileData.theSourcePhyPhysCollisionModels.Count; i++)
			{
				SourcePhyPhysCollisionModel aSourcePhysCollisionModel = thePhyFileData.theSourcePhyPhysCollisionModels[i];

				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				//If aSourcePhysCollisionModel.theDragCoefficientIsValid Then
				//End If

				if (aSourcePhysCollisionModel.theMassBiasIsValid)
				{
					line = "\t";
					line += "$jointmassbias \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" ";
					line += aSourcePhysCollisionModel.theMassBias.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}

				if (aSourcePhysCollisionModel.theDamping != thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theDamping)
				{
					line = "\t";
					line += "$jointdamping \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" ";
					line += aSourcePhysCollisionModel.theDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}

				if (aSourcePhysCollisionModel.theInertia != thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theInertia)
				{
					line = "\t";
					line += "$jointinertia \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" ";
					line += aSourcePhysCollisionModel.theInertia.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}

				if (aSourcePhysCollisionModel.theRotDamping != thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping)
				{
					line = "\t";
					line += "$jointrotdamping \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" ";
					line += aSourcePhysCollisionModel.theRotDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}

				if (thePhyFileData.theSourcePhyRagdollConstraintDescs.ContainsKey(aSourcePhysCollisionModel.theIndex))
				{
					SourcePhyRagdollConstraint aConstraint = thePhyFileData.theSourcePhyRagdollConstraintDescs[aSourcePhysCollisionModel.theIndex];
					line = "\t";
					line += "$jointconstrain \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" x limit ";
					line += aConstraint.theXMin.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theXMax.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theXFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
					line = "\t";
					line += "$jointconstrain \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" y limit ";
					line += aConstraint.theYMin.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theYMax.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theYFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
					line = "\t";
					line += "$jointconstrain \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" z limit ";
					line += aConstraint.theZMin.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theZMax.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theZFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			if (!thePhyFileData.theSourcePhySelfCollides)
			{
				line = "\t";
				line += "$noselfcollisions";
				theOutputFileStreamWriter.WriteLine(line);
			}
			else if (thePhyFileData.theSourcePhyCollisionPairs.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				foreach (SourcePhyCollisionPair aSourcePhyCollisionPair in thePhyFileData.theSourcePhyCollisionPairs)
				{
					line = "\t";
					line += "$jointcollide";
					line += " ";
					line += "\"";
					line += thePhyFileData.theSourcePhyPhysCollisionModels[aSourcePhyCollisionPair.obj0].theName;
					line += "\"";
					line += " ";
					line += "\"";
					line += thePhyFileData.theSourcePhyPhysCollisionModels[aSourcePhyCollisionPair.obj1].theName;
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteCollisionTextCommand()
		{
			string line = "";

			try
			{
				if (thePhyFileData != null && thePhyFileData.theSourcePhyCollisionText != null && thePhyFileData.theSourcePhyCollisionText.Length > 0)
				{
					line = "";
					theOutputFileStreamWriter.WriteLine(line);

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$CollisionText";
					}
					else
					{
						line = "$collisiontext";
					}
					theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					theOutputFileStreamWriter.WriteLine(line);

					WriteTextLines(thePhyFileData.theSourcePhyCollisionText, 1);

					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
			catch (Exception ex)
			{

			}
		}

		public void WriteGroupBone()
		{
			WriteDefineBoneCommand();
			WriteBoneMergeCommand();
			WriteLimitRotationCommand();

			WriteProceduralBonesCommand();
			WriteJiggleBoneCommand();
		}

		private void WriteDefineBoneCommand()
		{
			if (!MainCROWBAR.TheApp.Settings.DecompileQcIncludeDefineBoneLinesIsChecked)
			{
				return;
			}

			string line = "";

			//NOTE: Should not be used with L4D2 survivors, because it messes up the mesh in animations.
			//TODO: Need to figure out when to insert the lines, such as is typical for L4D2 view models.

			//$definebone "ValveBiped.root" "" 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000
			if (theMdlFileData.theBones != null)
			{
				SourceMdlBone aBone = null;
				string aParentBoneName = null;
				SourceVector aFixupPosition = new SourceVector();
				SourceVector aFixupRotation = new SourceVector();

				if (theMdlFileData.theBones.Count > 0)
				{
					theOutputFileStreamWriter.WriteLine();
				}

				for (int i = 0; i < theMdlFileData.theBones.Count; i++)
				{
					aBone = theMdlFileData.theBones[i];
					if (aBone.parentBoneIndex == -1)
					{
						aParentBoneName = "";
					}
					else
					{
						aParentBoneName = theMdlFileData.theBones[aBone.parentBoneIndex].theName;
					}

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$DefineBone ";
					}
					else
					{
						line = "$definebone ";
					}
					line += "\"";
					line += aBone.theName;
					line += "\"";
					line += " ";
					line += "\"";
					line += aParentBoneName;
					line += "\"";

					line += " ";
					line += aBone.position.x.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aBone.position.y.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aBone.position.z.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " ";
					line += MathModule.RadiansToDegrees(aBone.rotation.y).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += MathModule.RadiansToDegrees(aBone.rotation.z).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += MathModule.RadiansToDegrees(aBone.rotation.x).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

					//TODO: These fixups are all zeroes for now.
					//      They might be found in the srcbonetransform list.
					//      Note the g_bonetable[nParent].srcRealign that seems linked to the input fixup values of $definebone.
					//FROM: write.cpp
					//mstudiosrcbonetransform_t *pSrcBoneTransform = (mstudiosrcbonetransform_t *)pData;
					//phdr->numsrcbonetransform = nTransformCount;
					//phdr->srcbonetransformindex = pData - pStart;
					//pData += nTransformCount * sizeof( mstudiosrcbonetransform_t );
					//int bt = 0;
					//for ( int i = 0; i < g_numbones; i++ )
					//{
					//	if ( g_bonetable[i].flags & BONE_ALWAYS_PROCEDURAL )
					//		continue;
					//	int nParent = g_bonetable[i].parent;
					//	if ( MatricesAreEqual( identity, g_bonetable[i].srcRealign ) &&
					//		( ( nParent < 0 ) || MatricesAreEqual( identity, g_bonetable[nParent].srcRealign ) ) )
					//		continue;

					//	// What's going on here?
					//	// So, when we realign a bone, we want to do it in a way so that the child bones
					//	// have the same bone->world transform. If we take T as the src realignment transform
					//	// for the parent, P is the parent to world, and C is the child to parent, we expect 
					//	// the child->world is constant after realignment:
					//	//		CtoW = P * C = ( P * T ) * ( T^-1 * C )
					//	// therefore Cnew = ( T^-1 * C )
					//						If (nParent >= 0) Then
					//	{
					//		MatrixInvert( g_bonetable[nParent].srcRealign, pSrcBoneTransform[bt].pretransform );
					//	}
					//						Else
					//	{
					//		SetIdentityMatrix( pSrcBoneTransform[bt].pretransform );
					//	}
					//	MatrixCopy( g_bonetable[i].srcRealign, pSrcBoneTransform[bt].posttransform );
					//	AddToStringTable( &pSrcBoneTransform[bt], &pSrcBoneTransform[bt].sznameindex, g_bonetable[i].name );
					//	++bt;
					//}

					line += " ";
					line += aFixupPosition.x.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aFixupPosition.y.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aFixupPosition.z.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " ";
					line += aFixupRotation.x.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aFixupRotation.y.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aFixupRotation.z.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteProceduralBonesCommand()
		{
			//$proceduralbones "proceduralbones.vrd"
			if (theMdlFileData.theProceduralBonesCommandIsUsed)
			{
				theOutputFileStreamWriter.WriteLine();

				string line = "";
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line += "$ProceduralBones ";
				}
				else
				{
					line += "$proceduralbones ";
				}
				line += "\"";
				line += SourceFileNamesModule.GetVrdFileName(theModelName);
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		private void WriteLimitRotationCommand()
		{
			string line = "";

			//$limitrotation "boneName" "sequenceName1" [["sequenceName2"] ... "sequenceNameX"]
			if (theMdlFileData.theBones != null)
			{
				SourceMdlBone aBone = null;
				bool emptyLineIsAlreadyWritten = false;

				for (int i = 0; i < theMdlFileData.theBones.Count; i++)
				{
					aBone = theMdlFileData.theBones[i];

					if ((aBone.flags & SourceMdlBone.BONE_FIXED_ALIGNMENT) > 0)
					{
						if (!emptyLineIsAlreadyWritten)
						{
							theOutputFileStreamWriter.WriteLine();
							emptyLineIsAlreadyWritten = true;
						}

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line += "$LimitRotation ";
						}
						else
						{
							line += "$limitrotation ";
						}
						line += "\"";
						line += aBone.theName;
						line += "\"";

						//TODO: Finish WriteLimitRotationCommand().
						//If aBone.qAlignment = aBone.rotation Then
						//	line += """"
						//	line += aBone.theName
						//	line += """"
						//End If

						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		private void WriteBoneMergeCommand()
		{
			string line = "";

			//$bonemerge "ValveBiped.Bip01_R_Hand"
			if (theMdlFileData.theBones != null)
			{
				SourceMdlBone aBone = null;
				bool emptyLineIsAlreadyWritten = false;

				for (int i = 0; i < theMdlFileData.theBones.Count; i++)
				{
					aBone = theMdlFileData.theBones[i];

					if ((aBone.flags & SourceMdlBone.BONE_USED_BY_BONE_MERGE) > 0)
					{
						if (!emptyLineIsAlreadyWritten)
						{
							theOutputFileStreamWriter.WriteLine();
							emptyLineIsAlreadyWritten = true;
						}

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$BoneMerge ";
						}
						else
						{
							line = "$bonemerge ";
						}
						line += "\"";
						line += aBone.theName;
						line += "\"";
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		private void WriteJiggleBoneCommand()
		{
			if (theMdlFileData.theBones == null)
			{
				return;
			}

			string line = "";

			line = "";
			theOutputFileStreamWriter.WriteLine(line);

			for (int i = 0; i < theMdlFileData.theBones.Count; i++)
			{
				SourceMdlBone aBone = theMdlFileData.theBones[i];
				if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_JIGGLE && aBone.proceduralRuleOffset != 0)
				{
					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$JiggleBone ";
					}
					else
					{
						line = "$jigglebone ";
					}
					line += "\"";
					line += aBone.theName;
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
					line = "{";
					theOutputFileStreamWriter.WriteLine(line);
					if ((aBone.theJiggleBone.flags & SourceMdlJiggleBone.JIGGLE_IS_FLEXIBLE) > 0)
					{
						line = "\t";
						line += "is_flexible";
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "{";
						theOutputFileStreamWriter.WriteLine(line);

						line = "\t";
						line += "\t";
						line += "length ";
						line += aBone.theJiggleBone.length.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "tip_mass ";
						line += aBone.theJiggleBone.tipMass.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);

						line = "\t";
						line += "\t";
						line += "pitch_stiffness ";
						line += aBone.theJiggleBone.pitchStiffness.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "pitch_damping ";
						line += aBone.theJiggleBone.pitchDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "yaw_stiffness ";
						line += aBone.theJiggleBone.yawStiffness.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "yaw_damping ";
						line += aBone.theJiggleBone.yawDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);

						if ((aBone.theJiggleBone.flags & SourceMdlJiggleBone.JIGGLE_HAS_LENGTH_CONSTRAINT) == 0)
						{
							line = "\t";
							line += "\t";
							line += "allow_length_flex";
							theOutputFileStreamWriter.WriteLine(line);
						}
						line = "\t";
						line += "\t";
						line += "along_stiffness ";
						line += aBone.theJiggleBone.alongStiffness.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "along_damping ";
						line += aBone.theJiggleBone.alongDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);

						WriteJiggleBoneConstraints(aBone);

						line = "\t";
						line += "}";
						theOutputFileStreamWriter.WriteLine(line);
					}
					if ((aBone.theJiggleBone.flags & SourceMdlJiggleBone.JIGGLE_IS_RIGID) > 0)
					{
						line = "\t";
						line += "is_rigid";
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "{";
						theOutputFileStreamWriter.WriteLine(line);

						line = "\t";
						line += "\t";
						line += "length ";
						line += aBone.theJiggleBone.length.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "tip_mass ";
						line += aBone.theJiggleBone.tipMass.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);

						WriteJiggleBoneConstraints(aBone);

						line = "\t";
						line += "}";
						theOutputFileStreamWriter.WriteLine(line);
					}
					if ((aBone.theJiggleBone.flags & SourceMdlJiggleBone.JIGGLE_HAS_BASE_SPRING) > 0)
					{
						line = "\t";
						line += "has_base_spring";
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "{";
						theOutputFileStreamWriter.WriteLine(line);

						line = "\t";
						line += "\t";
						line += "base_mass ";
						line += aBone.theJiggleBone.baseMass.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "stiffness ";
						line += aBone.theJiggleBone.baseStiffness.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "damping ";
						line += aBone.theJiggleBone.baseDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);

						line = "\t";
						line += "\t";
						line += "left_constraint ";
						//line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinLeft).ToString("0.######", TheApp.InternalNumberFormat)
						//line += " "
						//line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxLeft).ToString("0.######", TheApp.InternalNumberFormat)
						line += aBone.theJiggleBone.baseMinLeft.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += aBone.theJiggleBone.baseMaxLeft.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "left_friction ";
						line += aBone.theJiggleBone.baseLeftFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);

						line = "\t";
						line += "\t";
						line += "up_constraint ";
						//line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinUp).ToString("0.######", TheApp.InternalNumberFormat)
						//line += " "
						//line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxUp).ToString("0.######", TheApp.InternalNumberFormat)
						line += aBone.theJiggleBone.baseMinUp.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += aBone.theJiggleBone.baseMaxUp.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "up_friction ";
						line += aBone.theJiggleBone.baseUpFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);

						line = "\t";
						line += "\t";
						line += "forward_constraint ";
						//line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMinForward).ToString("0.######", TheApp.InternalNumberFormat)
						//line += " "
						//line += MathModule.RadiansToDegrees(aBone.theJiggleBone.baseMaxForward).ToString("0.######", TheApp.InternalNumberFormat)
						line += aBone.theJiggleBone.baseMinForward.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += aBone.theJiggleBone.baseMaxForward.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
						line = "\t";
						line += "\t";
						line += "forward_friction ";
						line += aBone.theJiggleBone.baseForwardFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);

						line = "\t";
						line += "}";
						theOutputFileStreamWriter.WriteLine(line);
					}
					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteJiggleBoneConstraints(SourceMdlBone aBone)
		{
			string line = "";

			if ((aBone.theJiggleBone.flags & SourceMdlJiggleBone.JIGGLE_HAS_PITCH_CONSTRAINT) > 0)
			{
				line = "\t";
				line += "\t";
				line += "pitch_constraint ";
				line += MathModule.RadiansToDegrees(aBone.theJiggleBone.minPitch).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += MathModule.RadiansToDegrees(aBone.theJiggleBone.maxPitch).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
				line = "\t";
				line += "\t";
				line += "pitch_friction ";
				line += aBone.theJiggleBone.pitchFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
				line = "\t";
				line += "\t";
				line += "pitch_bounce ";
				line += aBone.theJiggleBone.pitchBounce.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			if ((aBone.theJiggleBone.flags & SourceMdlJiggleBone.JIGGLE_HAS_YAW_CONSTRAINT) > 0)
			{
				line = "\t";
				line += "\t";
				line += "yaw_constraint ";
				line += MathModule.RadiansToDegrees(aBone.theJiggleBone.minYaw).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += MathModule.RadiansToDegrees(aBone.theJiggleBone.maxYaw).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
				line = "\t";
				line += "\t";
				line += "yaw_friction ";
				line += aBone.theJiggleBone.yawFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
				line = "\t";
				line += "\t";
				line += "yaw_bounce ";
				line += aBone.theJiggleBone.yawBounce.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			if ((aBone.theJiggleBone.flags & SourceMdlJiggleBone.JIGGLE_HAS_ANGLE_CONSTRAINT) > 0)
			{
				line = "\t";
				line += "\t";
				line += "angle_constraint ";
				line += MathModule.RadiansToDegrees(aBone.theJiggleBone.angleLimit).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteGroupBox()
		{
			WriteCBoxCommand();
			WriteBBoxCommand();
			if (theMdlFileData.theHitboxSets != null)
			{
				if (theMdlFileData.version <= 10)
				{
					bool skipBoneInBBoxCommandWasUsed = false;
					theOutputFileStreamWriter.WriteLine();
					WriteHBoxCommands(theMdlFileData.theHitboxSets[0].theHitboxes, "", "", ref skipBoneInBBoxCommandWasUsed);
				}
				else
				{
					WriteHBoxRelatedCommands();
				}
			}
		}

		private void WriteCBoxCommand()
		{
			string line = "";
			double minX = 0;
			double minY = 0;
			double minZ = 0;
			double maxX = 0;
			double maxY = 0;
			double maxZ = 0;

			line = "";
			theOutputFileStreamWriter.WriteLine(line);

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
			if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
			{
				line = "$CBox ";
			}
			else
			{
				line = "$cbox ";
			}
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

		private void WriteBBoxCommand()
		{
			string line = "";
			double minX = 0;
			double minY = 0;
			double minZ = 0;
			double maxX = 0;
			double maxY = 0;
			double maxZ = 0;

			line = "";
			theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				line = "// Bounding box or hull. Used for collision with a world object.";
				theOutputFileStreamWriter.WriteLine(line);
			}

			//$bbox -16.0 -16.0 -13.0 16.0 16.0 75.0
			//FROM: VDC wiki: 
			//$bbox (min x) (min y) (min z) (max x) (max y) (max z)
			minX = Math.Round(theMdlFileData.hullMinPosition.x, 3);
			minY = Math.Round(theMdlFileData.hullMinPosition.y, 3);
			minZ = Math.Round(theMdlFileData.hullMinPosition.z, 3);
			maxX = Math.Round(theMdlFileData.hullMaxPosition.x, 3);
			maxY = Math.Round(theMdlFileData.hullMaxPosition.y, 3);
			maxZ = Math.Round(theMdlFileData.hullMaxPosition.z, 3);
			line = "";
			if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
			{
				line += "$BBox ";
			}
			else
			{
				line += "$bbox ";
			}
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

		private void WriteHBoxRelatedCommands()
		{
			string line = "";
			string commentTag = "";
			bool hitBoxWasAutoGenerated = false;
			bool skipBoneInBBoxCommandWasUsed = false;

			if (theMdlFileData.theHitboxSets.Count < 1)
			{
				return;
			}

			hitBoxWasAutoGenerated = (theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX) > 0;
			if (hitBoxWasAutoGenerated && !MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				return;
			}

			theOutputFileStreamWriter.WriteLine();

			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				line = "// Hitbox info. Used for damage-based collision.";
				theOutputFileStreamWriter.WriteLine(line);
			}

			if (hitBoxWasAutoGenerated)
			{
				line = "// The hitbox info below was automatically generated when compiled because no hitbox info was provided.";
				theOutputFileStreamWriter.WriteLine(line);

				//NOTE: Only comment-out the hbox lines if auto-generated.
				commentTag = "// ";
			}

			//FROM: HLMV for survivor_producer: 
			//$hboxset "L4D"
			//$hbox 3 "ValveBiped.Bip01_Pelvis"	    -5.33   -4.00   -4.00     5.33    4.00    4.00
			//$hbox 6 "ValveBiped.Bip01_L_Thigh"	     4.44   -3.02   -2.53    16.87    2.31    1.91
			//$hbox 6 "ValveBiped.Bip01_L_Calf"	     0.44   -1.78   -2.22    17.32    2.66    2.22
			//$hbox 6 "ValveBiped.Bip01_L_Toe0"	    -3.11   -0.44   -1.20     1.33    1.33    2.18
			//$hbox 7 "ValveBiped.Bip01_R_Thigh"	     4.44   -3.02   -2.53    16.87    2.31    1.91
			//$hbox 7 "ValveBiped.Bip01_R_Calf"	     0.44   -1.78   -2.22    17.32    2.66    2.22
			//$hbox 7 "ValveBiped.Bip01_R_Toe0"	    -3.11   -0.44   -1.20     1.33    1.33    2.18
			//$hbox 3 "ValveBiped.Bip01_Spine1"	    -4.44   -3.77   -5.33     4.44    5.55    5.33
			//$hbox 2 "ValveBiped.Bip01_Spine2"	    -2.66   -3.02   -5.77    10.66    5.86    5.77
			//$hbox 1 "ValveBiped.Bip01_Neck1"	     0.00   -2.22   -2.00     3.55    2.22    2.00
			//$hbox 1 "ValveBiped.Bip01_Head1"	    -0.71   -3.55   -2.71     6.39    3.55    2.18
			//$hbox 4 "ValveBiped.Bip01_L_UpperArm"	     0.00   -1.86   -1.78     9.77    1.69    1.78
			//$hbox 4 "ValveBiped.Bip01_L_Forearm"	     0.44   -1.55   -1.55    10.21    1.55    1.55
			//$hbox 4 "ValveBiped.Bip01_L_Hand"	     0.94   -1.28   -2.13     4.94    0.50    1.15
			//$hbox 5 "ValveBiped.Bip01_R_UpperArm"	     0.00   -1.86   -1.78     9.77    1.69    1.78
			//$hbox 5 "ValveBiped.Bip01_R_Forearm"	     0.44   -1.55   -1.55    10.21    1.55    1.55
			//$hbox 5 "ValveBiped.Bip01_R_Hand"	     0.94   -1.28   -2.13     4.94    0.50    1.15

			SourceMdlHitboxSet aHitboxSet = null;
			for (int i = 0; i < theMdlFileData.theHitboxSets.Count; i++)
			{
				aHitboxSet = theMdlFileData.theHitboxSets[i];

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$HBoxSet ";
				}
				else
				{
					line = "$hboxset ";
				}
				line += "\"";
				line += aHitboxSet.theName;
				line += "\"";
				theOutputFileStreamWriter.WriteLine(commentTag + line);

				if (aHitboxSet.theHitboxes == null)
				{
					continue;
				}

				WriteHBoxCommands(aHitboxSet.theHitboxes, commentTag, aHitboxSet.theName, ref skipBoneInBBoxCommandWasUsed);
			}

			if (skipBoneInBBoxCommandWasUsed)
			{
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$SkipBoneInBBox";
				}
				else
				{
					line = "$skipboneinbbox";
				}
				theOutputFileStreamWriter.WriteLine(commentTag + line);
			}
		}

		private void WriteHBoxCommands(List<SourceMdlHitbox> theHitboxes, string commentTag, string hitboxSetName, ref bool theSkipBoneInBBoxCommandWasUsed)
		{
			string line = "";
			SourceMdlHitbox aHitbox = null;

			for (int j = 0; j < theHitboxes.Count; j++)
			{
				aHitbox = theHitboxes[j];
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$HBox ";
				}
				else
				{
					line = "$hbox ";
				}
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
				//NOTE: For L4D2 survivor_teenangst, the extra zeroes cause this compile error: 
				//ERROR: c:\users\zeqmacaw\documents\- unpacked source\left 4 dead 2\left4dead2_dlc3\models\survivors\decompiled 0.26\survivor_teenangst\survivor_teenangst_boxes.qci(10): - bad command 0
				//ERROR: Aborted Processing on 'survivors/survivor_TeenAngst.mdl'
				//TODO: [WriteHboxCommands] Probably need better way to determine when to write extra values.
				//If Me.theMdlFileData.version >= 49 AndAlso hitboxSetName = "cstrike" Then
				//TEST: Check several models from various games to see if this check is good enough.
				//If aHitbox.unknown = -1 Then
				if (theMdlFileData.version >= 49 && aHitbox.unknown != 0)
				{
					//NOTE: Roll (z) is first.
					line += " ";
					line += aHitbox.boundingBoxPitchYawRoll.z.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aHitbox.boundingBoxPitchYawRoll.x.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aHitbox.boundingBoxPitchYawRoll.y.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aHitbox.unknown.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
				}
				if (aHitbox.nameOffset != 0 && !string.IsNullOrEmpty(aHitbox.theName))
				{
					line += " ";
					line += "\"";
					line += aHitbox.theName;
					line += "\"";
				}
				theOutputFileStreamWriter.WriteLine(commentTag + line);

				if (!theSkipBoneInBBoxCommandWasUsed)
				{
					if (aHitbox.boundingBoxMin.x > 0 || aHitbox.boundingBoxMin.y > 0 || aHitbox.boundingBoxMin.z > 0 || aHitbox.boundingBoxMax.x < 0 || aHitbox.boundingBoxMax.y < 0 || aHitbox.boundingBoxMax.z < 0)
					{
						theSkipBoneInBBoxCommandWasUsed = true;
					}
				}
			}
		}

		//Public Sub WriteBodyGroupCommand(ByVal startIndex As Integer)
		public void WriteBodyGroupCommand()
		{
			string line = "";
			SourceMdlBodyPart aBodyPart = null;
			SourceVtxBodyPart07 aVtxBodyPart = null;
			SourceMdlModel aBodyModel = null;
			SourceVtxModel07 aVtxModel = null;

			//$bodygroup "belt"
			//{
			////	studio "zoey_belt.smd"
			//	"blank"
			//}
			//$bodygroup "shoes"
			//{
			////  studio "zoey_shoes.smd"
			//    studio "zoey_feet.smd"
			//}
			//FROM: VDC wiki: 
			//$bodygroup sights
			//{
			//	studio "ironsights.smd"
			//	studio "laser_dot.smd"
			//	blank
			//}
			//If Me.theMdlFileData.theBodyParts IsNot Nothing AndAlso Me.theMdlFileData.theBodyParts.Count > startIndex Then
			if (theMdlFileData.theBodyParts != null && theMdlFileData.theBodyParts.Count > 0)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					//If Me.theMdlFileData.theModelCommandIsUsed AndAlso bodyPartIndex = Me.theMdlFileData.theBodyPartIndexThatShouldUseModelCommand Then
					//	Me.WriteModelCommand()
					//	Continue For
					//End If
					aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];
					if (aBodyPart.theModelCommandIsUsed)
					{
						WriteModelCommand(aBodyPart);
						continue;
					}

					if (theVtxFileData != null && theVtxFileData.theVtxBodyParts != null && theVtxFileData.theVtxBodyParts.Count > 0)
					{
						aVtxBodyPart = theVtxFileData.theVtxBodyParts[bodyPartIndex];
					}
					else
					{
						aVtxBodyPart = null;
					}

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

							if (aVtxBodyPart != null && aVtxBodyPart.theVtxModels != null && aVtxBodyPart.theVtxModels.Count > 0)
							{
								aVtxModel = aVtxBodyPart.theVtxModels[modelIndex];
							}
							else
							{
								aVtxModel = null;
							}

							line = "\t";
							if (aBodyModel.name[0] == '\0' && (aVtxModel != null && aVtxModel.theVtxModelLods[0].theVtxMeshes == null))
							{
								line += "blank";
							}
							else
							{
								aBodyModel.theSmdFileNames[0] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[0], bodyPartIndex, modelIndex, 0, theModelName, new string(aBodyModel.name));
								line += "studio ";
								line += "\"";
								line += aBodyModel.theSmdFileNames[0];
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
			SourceMdlBoneController boneController = null;

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

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$Controller ";
						}
						else
						{
							line = "$controller ";
						}
						line += boneController.inputField.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
						line += " \"";
						line += theMdlFileData.theBones[boneController.boneIndex].theName;
						line += "\" ";
						line += boneController.TypeName;
						line += " ";
						line += boneController.startBlah.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += boneController.endBlah.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		public void WriteScreenAlignCommand()
		{
			string line = "";

			//$screenalign <bone name> <"sphere" or "cylinder">
			try
			{
				if (theMdlFileData.theBones != null)
				{
					SourceMdlBone aBone = null;
					bool emptyLineIsAlreadyWritten = false;

					for (int i = 0; i < theMdlFileData.theBones.Count; i++)
					{
						aBone = theMdlFileData.theBones[i];

						if ((aBone.flags & SourceMdlBone.BONE_SCREEN_ALIGN_SPHERE) > 0)
						{
							if (!emptyLineIsAlreadyWritten)
							{
								theOutputFileStreamWriter.WriteLine();
								emptyLineIsAlreadyWritten = true;
							}

							if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
							{
								line = "$ScreenAlign ";
							}
							else
							{
								line = "$screenalign ";
							}
							line += aBone.theName;
							line += " \"sphere\"";
							theOutputFileStreamWriter.WriteLine(line);
						}
						else if ((aBone.flags & SourceMdlBone.BONE_SCREEN_ALIGN_CYLINDER) > 0)
						{
							if (!emptyLineIsAlreadyWritten)
							{
								theOutputFileStreamWriter.WriteLine();
								emptyLineIsAlreadyWritten = true;
							}

							if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
							{
								line = "$ScreenAlign ";
							}
							else
							{
								line = "$screenalign ";
							}
							line += aBone.theName;
							line += " \"cylinder\"";
							theOutputFileStreamWriter.WriteLine(line);
						}
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		internal override void WriteTextLines(string text, int indentCount)
		{
			string line = "";
			char textChar = '\0';
			int startIndex = 0;
			string indentText = null;
			int lineQuoteCount = 0;
			int lineWordCount = 0;
			string beforeCloseBraceText = null;

			indentText = "";
			for (int j = 1; j <= indentCount; j++)
			{
				indentText += "\t";
			}

			startIndex = 0;
			lineQuoteCount = 0;
			lineWordCount = 0;
			for (int i = 0; i < text.Length; i++)
			{
				textChar = text[i];
				if (textChar == '{')
				{
					if (i > startIndex)
					{
						line = indentText;
						line += text.Substring(startIndex, i - startIndex);
						theOutputFileStreamWriter.WriteLine(line);
					}

					line = indentText;
					line += "{";
					theOutputFileStreamWriter.WriteLine(line);

					indentCount += 1;
					indentText = "";
					for (int j = 1; j <= indentCount; j++)
					{
						indentText += "\t";
					}

					startIndex = i + 1;
					lineQuoteCount = 0;
				}
				else if (textChar == '}')
				{
					if (i > startIndex)
					{
						beforeCloseBraceText = text.Substring(startIndex, i - startIndex).Trim();
						if (!string.IsNullOrEmpty(beforeCloseBraceText))
						{
							line = indentText;
							line += beforeCloseBraceText;
							theOutputFileStreamWriter.WriteLine(line);
						}
					}

					indentCount -= 1;
					indentText = "";
					for (int j = 1; j <= indentCount; j++)
					{
						indentText += "\t";
					}

					line = indentText;
					line += "}";
					theOutputFileStreamWriter.WriteLine(line);

					startIndex = i + 1;
					lineQuoteCount = 0;
				}
				else if (textChar == '\"')
				{
					lineQuoteCount += 1;
					if (lineQuoteCount == 2)
					{
						if (i > startIndex)
						{
							line = indentText;
							line += text.Substring(startIndex, i - startIndex + 1).Trim();
							theOutputFileStreamWriter.Write(line);
						}
						startIndex = i + 1;
						//lineQuoteCount = 0
					}
					else if (lineQuoteCount == 4)
					{
						if (i > startIndex)
						{
							line = indentText;
							line += text.Substring(startIndex, i - startIndex + 1).Trim();
							theOutputFileStreamWriter.WriteLine(line);
						}
						startIndex = i + 1;
						lineQuoteCount = 0;
					}
				}
				else if (textChar == "\n"[0])
				{
					startIndex = i + 1;
					lineQuoteCount = 0;
				}
			}
		}

		public void WriteQciDeclareSequenceLines()
		{
			if (theMdlFileData.theSequenceDescs != null)
			{
				string line = "";

				theOutputFileStreamWriter.WriteLine();

				for (int i = 0; i < theMdlFileData.theSequenceDescs.Count; i++)
				{
					SourceMdlSequenceDesc aSequenceDesc = theMdlFileData.theSequenceDescs[i];

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$DeclareSequence";
					}
					else
					{
						line = "$declaresequence";
					}
					line += " \"";
					line += aSequenceDesc.theName;
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

#endregion

#region Data
		private SourceMdlFileData49 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		private SourceVtxFileData07 theVtxFileData;
		private string theModelName;

		private string theOutputPath;
		private string theOutputFileNameWithoutExtension;

		private SourceMdlBodyPart theBodyPartForFlexWriting;
#endregion

	}

}