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
	public class SourceQcFile37 : SourceQcFile
	{
#region Creation and Destruction

		public SourceQcFile37(StreamWriter outputFileStream, string outputPathFileName, SourceMdlFileData37 mdlFileData, SourcePhyFileData phyFileData, SourceVtxFileData06 vtxFileData, string modelName)
		{
			this.theOutputFileStreamWriter = outputFileStream;
			this.theMdlFileData = mdlFileData;
			this.thePhyFileData = phyFileData;
			this.theVtxFileData = vtxFileData;
			this.theModelName = modelName;

			this.theOutputPath = FileManager.GetPath(outputPathFileName);
			this.theOutputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPathFileName);
		}

#endregion

#region Methods

		//Public Function GetMdlRelativePathFileName(ByVal qcPathFileName As String) As String
		//	Dim modelRelativePathFileName As String

		//	modelRelativePathFileName = ""

		//	Using inputFileStream As StreamReader = New StreamReader(qcPathFileName)
		//		Dim inputLine As String
		//		Dim temp As String

		//		While (Not (inputFileStream.EndOfStream))
		//			inputLine = inputFileStream.ReadLine()

		//			temp = inputLine.ToLower().TrimStart()
		//			If temp.StartsWith("$modelname") Then
		//				temp = temp.Replace("$modelname", "")
		//				temp = temp.Trim()
		//				temp = temp.Trim(Chr(34))
		//				modelRelativePathFileName = temp.Replace("/", "\")
		//				Exit While
		//			End If
		//		End While
		//	End Using

		//	Return modelRelativePathFileName
		//End Function

		//Public Sub InsertAnIncludeFileCommand(ByVal qcPathFileName As String, ByVal includedPathFileName As String)
		//	Using outputFileStream As StreamWriter = File.AppendText(qcPathFileName)
		//		If File.Exists(includedPathFileName) Then
		//			Dim qciFileInfo As New FileInfo(includedPathFileName)
		//			If qciFileInfo.Length > 0 Then
		//				Dim line As String = ""

		//				outputFileStream.WriteLine()

		//If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
		//	line += "$Include "
		//Else
		//	line += "$include "
		//End If
		//				line += """"
		//				line += FileManager.GetRelativePathFileName(qcPathFileName, includedPathFileName)
		//				line += """"
		//				outputFileStream.WriteLine(line)
		//			End If
		//		End If
		//	End Using
		//End Sub

#endregion

#region Private Delegates

		public delegate void WriteGroupDelegate();

#endregion

#region Private Methods

		public void WriteGroup(string qciGroupName, WriteGroupDelegate writeGroupAction, bool includeLineIsCommented, bool includeLineIsIndented)
		{
			if (MainCROWBAR.TheApp.Settings.DecompileGroupIntoQciFilesIsChecked)
			{
				string qciFileName = null;
				string qciPathFileName = null;
				StreamWriter mainOutputFileStream = this.theOutputFileStreamWriter;


				try
				{
					//qciPathFileName = Path.Combine(Me.theOutputPathName, Me.theOutputFileNameWithoutExtension + "_flexes.qci")
					qciFileName = this.theOutputFileNameWithoutExtension + "_" + qciGroupName + ".qci";
					qciPathFileName = Path.Combine(this.theOutputPath, qciFileName);

					this.theOutputFileStreamWriter = File.CreateText(qciPathFileName);

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
					if (this.theOutputFileStreamWriter != null)
					{
						this.theOutputFileStreamWriter.Flush();
						this.theOutputFileStreamWriter.Close();

						this.theOutputFileStreamWriter = mainOutputFileStream;
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

							this.theOutputFileStreamWriter.WriteLine();

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
							this.theOutputFileStreamWriter.WriteLine(line);
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
			//Dim modelPath As String
			string modelPathFileName;

			//modelPath = FileManager.GetPath(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
			//modelPathFileName = Path.Combine(modelPath, theSourceEngineModel.ModelName + ".mdl")
			//modelPathFileName = CStr(theSourceEngineModel.MdlFileHeader.name).Trim(Chr(0))
			modelPathFileName = this.theMdlFileData.theModelName;

			this.theOutputFileStreamWriter.WriteLine();

			//$modelname "survivors/survivor_producer.mdl"
			//$modelname "custom/survivor_producer.mdl"
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
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteIncludeMainQcLine()
		{
			string line = "";

			this.theOutputFileStreamWriter.WriteLine();

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
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(this.theOutputFileStreamWriter);
		}

		public void WriteStaticPropCommand()
		{
			string line = "";

			//$staticprop
			if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$StaticProp";
				}
				else
				{
					line = "$staticprop";
				}
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteModelCommand()
		{
			string line = "";
			SourceMdlBodyPart37 aBodyPart = null;
			SourceMdlModel37 aBodyModel = null;
			//Dim referenceSmdFileName As String
			//Dim aBone As SourceMdlBone
			List<string> eyeballNames = new List<string>();


			//$model "producer" "producer_model_merged.dmx.smd" {
			////-doesn't work     eyeball righteye ValveBiped.Bip01_Head1 -1.260 -0.086 64.594 eyeball_r 1.050  3.000 producer_head 0.530
			////-doesn't work     eyeball lefteye ValveBiped.Bip01_Head1 1.260 -0.086 64.594 eyeball_l 1.050  -3.000 producer_head 0.530
			//     mouth 0 "mouth"  ValveBiped.Bip01_Head1 0.000 1.000 0.000
			//}
			if (this.theMdlFileData.theBodyParts != null && this.theMdlFileData.theBodyParts.Count > 0)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				aBodyPart = this.theMdlFileData.theBodyParts[0];
				aBodyModel = aBodyPart.theModels[0];
				//referenceSmdFileName = Me.GetModelPathFileName(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0))
				//referenceSmdFileName = theSourceEngineModel.GetLodSmdFileName(0)
				aBodyModel.theSmdFileNames[0] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[0], 0, 0, 0, this.theModelName, new string(this.theMdlFileData.theBodyParts[0].theModels[0].name));

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
				this.theOutputFileStreamWriter.WriteLine(line);

				//NOTE: Must call WriteEyeballLines() before WriteEyelidLines(), because eyeballNames are created in first and sent to other.
				this.WriteEyeballLines(ref eyeballNames);
				this.WriteEyelidLines(eyeballNames);

				this.WriteMouthLines();

				this.WriteGroup("flex", WriteGroupFlex, false, true);

				line = "}";
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		private void WriteEyeballLines(ref List<string> eyeballNames)
		{
			string line = "";
			SourceMdlBodyPart37 aBodyPart = null;
			SourceMdlModel37 aModel = null;
			SourceMdlEyeball37 anEyeball = null;
			string eyeballTextureName = null;
			string irisTextureName = null;
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
				aBodyPart = this.theMdlFileData.theBodyParts[0];
				if (aBodyPart.theModels != null && aBodyPart.theModels.Count > 0)
				{
					aModel = aBodyPart.theModels[0];
					if (aModel.theEyeballs != null && aModel.theEyeballs.Count > 0)
					{
						line = "";
						this.theOutputFileStreamWriter.WriteLine(line);

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
							SourceMdlBone37 aBone = this.theMdlFileData.theBones[anEyeball.boneIndex];
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
							}
							else
							{
								eyeballTextureName = this.theMdlFileData.theTextures[anEyeball.theTextureIndex].theFileName;
								//eyeballTextureName = Path.GetFileName(theSourceEngineModel.theMdlFileHeader.theTextures(anEyeball.theTextureIndex).theName)
							}

							if (anEyeball.irisMaterial == -1)
							{
								irisTextureName = "[unknown_texture]";
							}
							else
							{
								irisTextureName = this.theMdlFileData.theTextures[anEyeball.irisMaterial].theFileName;
							}

							line = "\t";
							line += "eyeball \"";
							line += eyeballNames[eyeballIndex];
							line += "\" \"";
							line += this.theMdlFileData.theBones[anEyeball.boneIndex].theName;
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
							line += " \"";
							line += irisTextureName;
							line += "\" ";
							line += Math.Round(irisScale, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
							this.theOutputFileStreamWriter.WriteLine(line);

							//NOTE: Used to write frame indexes for eyelid lines and prevent eyelid flexes from being written in flex list in qc file.
							this.theMdlFileData.theFlexDescs[anEyeball.upperLidFlexDesc].theDescIsUsedByEyelid = true;
							this.theMdlFileData.theFlexDescs[anEyeball.lowerLidFlexDesc].theDescIsUsedByEyelid = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			this.CreateListOfEyelidFlexFrameIndexes();
		}

		private void CreateListOfEyelidFlexFrameIndexes()
		{
			FlexFrame37 aFlexFrame = null;

			this.theMdlFileData.theEyelidFlexFrameIndexes = new List<int>();
			for (int frameIndex = 1; frameIndex < this.theMdlFileData.theFlexFrames.Count; frameIndex++)
			{
				aFlexFrame = this.theMdlFileData.theFlexFrames[frameIndex];
				if (!this.theMdlFileData.theEyelidFlexFrameIndexes.Contains(frameIndex))
				{
					if (this.theMdlFileData.theFlexDescs[aFlexFrame.flexes[0].flexDescIndex].theDescIsUsedByEyelid)
					{
						this.theMdlFileData.theEyelidFlexFrameIndexes.Add(frameIndex);
					}
				}
			}
		}

		private void WriteEyelidLines(List<string> eyeballNames)
		{
			string line = "";
			SourceMdlBodyPart37 aBodyPart = null;
			SourceMdlModel37 aModel = null;
			SourceMdlEyeball37 anEyeball = null;
			int frameIndex = 0;
			string eyelidName = null;

			try
			{
				// Write eyelid options.
				//$definevariable expressions "zoeyp.vta"
				//eyelid  upper_right $expressions$ lowerer 1 -0.19 neutral 0 0.13 raiser 2 0.27 split 0.1 eyeball righteye
				//eyelid  lower_right $expressions$ lowerer 3 -0.32 neutral 0 -0.19 raiser 4 -0.02 split 0.1 eyeball righteye
				//eyelid  upper_left $expressions$ lowerer 1 -0.19 neutral 0 0.13 raiser 2 0.27 split -0.1 eyeball lefteye
				//eyelid  lower_left $expressions$ lowerer 3 -0.32 neutral 0 -0.19 raiser 4 -0.02 split -0.1 eyeball lefteye
				aBodyPart = this.theMdlFileData.theBodyParts[0];
				if (aBodyPart.theModels != null && aBodyPart.theModels.Count > 0 && this.theMdlFileData.theEyelidFlexFrameIndexes.Count > 0)
				{
					aModel = aBodyPart.theModels[0];
					if (aModel.theEyeballs != null && aModel.theEyeballs.Count > 0)
					{
						line = "";
						this.theOutputFileStreamWriter.WriteLine(line);

						frameIndex = 0;
						for (int eyeballIndex = 0; eyeballIndex < aModel.theEyeballs.Count; eyeballIndex++)
						{
							anEyeball = aModel.theEyeballs[eyeballIndex];

							if (frameIndex + 3 >= this.theMdlFileData.theEyelidFlexFrameIndexes.Count)
							{
								frameIndex = 0;
							}
							eyelidName = this.theMdlFileData.theFlexDescs[anEyeball.upperLidFlexDesc].theName;

							line = "\t";
							line += "eyelid ";
							line += eyelidName;
							//line += " """
							//line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
							//line += ".vta"" "
							line += " \"";
							line += SourceFileNamesModule.GetVtaFileName(this.theModelName, 0);
							line += "\" ";
							line += "lowerer ";
							//TODO: The frame indexes here and for raiser need correcting.
							//line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.upperFlexDesc(0)).theVtaFrameIndex.ToString()
							//TEST:
							//line += anEyeball.upperFlexDesc(0).ToString()
							//TEST:
							line += this.theMdlFileData.theEyelidFlexFrameIndexes[frameIndex].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
							frameIndex += 1;
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
							line += this.theMdlFileData.theEyelidFlexFrameIndexes[frameIndex].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
							frameIndex += 1;
							line += " ";
							line += anEyeball.upperTarget[2].ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += "split ";
							//TODO: simplify.cpp RemapVertexAnimations(); probably should call SourceMdlFile.GetSplit()?
							line += this.GetSplitNumber(eyelidName);
							line += " eyeball \"";
							line += eyeballNames[eyeballIndex];
							line += "\"";
							this.theOutputFileStreamWriter.WriteLine(line);

							this.theMdlFileData.theFlexDescs[anEyeball.upperLidFlexDesc].theDescIsUsedByFlex = true;
							this.theMdlFileData.theFlexDescs[anEyeball.upperFlexDesc[0]].theDescIsUsedByFlex = true;
							this.theMdlFileData.theFlexDescs[anEyeball.upperFlexDesc[1]].theDescIsUsedByFlex = true;
							this.theMdlFileData.theFlexDescs[anEyeball.upperFlexDesc[2]].theDescIsUsedByFlex = true;

							eyelidName = this.theMdlFileData.theFlexDescs[anEyeball.lowerLidFlexDesc].theName;

							line = "\t";
							line += "eyelid ";
							line += eyelidName;
							//line += " """
							//line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
							//line += ".vta"" "
							line += " \"";
							line += SourceFileNamesModule.GetVtaFileName(this.theModelName, 0);
							line += "\" ";
							line += "lowerer ";
							//line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(anEyeball.lowerFlexDesc(0)).theVtaFrameIndex.ToString()
							//TEST:
							//line += anEyeball.lowerFlexDesc(0).ToString()
							//TEST:
							line += this.theMdlFileData.theEyelidFlexFrameIndexes[frameIndex].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
							frameIndex += 1;
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
							line += this.theMdlFileData.theEyelidFlexFrameIndexes[frameIndex].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
							frameIndex += 1;
							line += " ";
							line += anEyeball.lowerTarget[2].ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
							line += " ";
							line += "split ";
							//TODO: simplify.cpp RemapVertexAnimations(); probably should call SourceMdlFile.GetSplit()?
							line += this.GetSplitNumber(eyelidName);
							line += " eyeball \"";
							line += eyeballNames[eyeballIndex];
							line += "\"";
							this.theOutputFileStreamWriter.WriteLine(line);

							this.theMdlFileData.theFlexDescs[anEyeball.lowerLidFlexDesc].theDescIsUsedByFlex = true;
							this.theMdlFileData.theFlexDescs[anEyeball.lowerFlexDesc[0]].theDescIsUsedByFlex = true;
							this.theMdlFileData.theFlexDescs[anEyeball.lowerFlexDesc[1]].theDescIsUsedByFlex = true;
							this.theMdlFileData.theFlexDescs[anEyeball.lowerFlexDesc[2]].theDescIsUsedByFlex = true;
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
			if (this.theMdlFileData.theMouths != null && this.theMdlFileData.theMouths.Count > 0)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.theMouths.Count; i++)
				{
					SourceMdlMouth aMouth = this.theMdlFileData.theMouths[i];
					offsetX = Math.Round(aMouth.forward.x, 3);
					offsetY = Math.Round(aMouth.forward.y, 3);
					offsetZ = Math.Round(aMouth.forward.z, 3);

					line = "\t";
					line += "mouth ";
					line += i.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " \"";
					line += this.theMdlFileData.theFlexDescs[aMouth.flexDescIndex].theName;
					line += "\" \"";
					line += this.theMdlFileData.theBones[aMouth.boneIndex].theName;
					line += "\" ";
					line += offsetX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += offsetY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += offsetZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);

					this.theMdlFileData.theFlexDescs[aMouth.flexDescIndex].theDescIsUsedByFlex = true;
				}
			}
		}

		private void WriteGroupFlex()
		{
			this.WriteFlexLines();
			this.WriteFlexControllerLines();
			this.WriteFlexRuleLines();
		}

		private void WriteFlexLines()
		{
			string line = "";

			// Write flexfile (contains flexDescs).
			if (this.theMdlFileData.theFlexFrames != null && this.theMdlFileData.theFlexFrames.Count > 0)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				line = "\t";
				line += "flexfile";
				//line += Path.GetFileNameWithoutExtension(CStr(Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(0).theModels(0).name).Trim(Chr(0)))
				//line += ".vta"""
				line += " \"";
				line += SourceFileNamesModule.GetVtaFileName(this.theModelName, 0);
				line += "\" ";
				this.theOutputFileStreamWriter.WriteLine(line);

				line = "\t";
				line += "{";
				this.theOutputFileStreamWriter.WriteLine(line);

				//======
				line = "\t";
				line += "\t";
				line += "defaultflex frame 0";
				this.theOutputFileStreamWriter.WriteLine(line);

				//NOTE: Start at index 1 because defaultflex frame is at index 0.
				FlexFrame37 aFlexFrame = null;
				for (int frameIndex = 1; frameIndex < this.theMdlFileData.theFlexFrames.Count; frameIndex++)
				{
					aFlexFrame = this.theMdlFileData.theFlexFrames[frameIndex];
					line = "\t";
					line += "\t";
					if (this.theMdlFileData.theFlexDescs[aFlexFrame.flexes[0].flexDescIndex].theDescIsUsedByEyelid)
					{
						line += "// Already in eyelid lines: ";
					}
					//If aFlexFrame.flexHasPartner Then
					//	line += "flexpair """
					//	line += aFlexFrame.flexName.Substring(0, aFlexFrame.flexName.Length - 1)
					//Else
					line += "flex \"";
					line += aFlexFrame.flexName;
					//End If
					line += "\"";
					//If aFlexFrame.flexHasPartner Then
					//	line += " "
					//	line += aFlexFrame.flexSplit.ToString("0.######", TheApp.InternalNumberFormat)
					//End If
					line += " frame ";
					line += frameIndex.ToString();
					this.theOutputFileStreamWriter.WriteLine(line);
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
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		private void WriteFlexControllerLines()
		{
			string line = "";

			//NOTE: Writes out flexcontrollers correctly for teenangst zoey.
			if (this.theMdlFileData.theFlexControllers != null && this.theMdlFileData.theFlexControllers.Count > 0)
			{
				SourceMdlFlexController aFlexController = null;

				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.theFlexControllers.Count; i++)
				{
					aFlexController = this.theMdlFileData.theFlexControllers[i];

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
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteFlexRuleLines()
		{
			string line = "";

			//NOTE: All flex rules are correct for teenangst zoey.
			if (this.theMdlFileData.theFlexRules != null && this.theMdlFileData.theFlexRules.Count > 0)
			{
				SourceMdlFlexRule aFlexRule = null;

				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.theFlexDescs.Count; i++)
				{
					SourceMdlFlexDesc flexDesc = this.theMdlFileData.theFlexDescs[i];

					if (!flexDesc.theDescIsUsedByFlex && flexDesc.theDescIsUsedByFlexRule)
					{
						line = "\t";
						line += "localvar ";
						line += flexDesc.theName;
						this.theOutputFileStreamWriter.WriteLine(line);
					}
				}

				for (int i = 0; i < this.theMdlFileData.theFlexRules.Count; i++)
				{
					aFlexRule = this.theMdlFileData.theFlexRules[i];
					line = this.GetFlexRule(aFlexRule);
					this.theOutputFileStreamWriter.WriteLine(line);
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

		private string GetFlexRule(SourceMdlFlexRule aFlexRule)
		{
			string flexRuleEquation = "\t";
			flexRuleEquation += "%";
			flexRuleEquation += this.theMdlFileData.theFlexDescs[aFlexRule.flexIndex].theName;
			flexRuleEquation += " = ";
			if (aFlexRule.theFlexOps != null && aFlexRule.theFlexOps.Count > 0)
			{
				SourceMdlFlexOp aFlexOp = null;

				// Convert to infix notation.

				Stack<IntermediateExpression> stack = new Stack<IntermediateExpression>();
				string rightExpr = null;
				string leftExpr = null;

//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int count = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string newExpression = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				IntermediateExpression intermediateExp = null;
				for (int i = 0; i < aFlexRule.theFlexOps.Count; i++)
				{
					aFlexOp = aFlexRule.theFlexOps[i];
					if (aFlexOp.op == SourceMdlFlexOp.STUDIO_CONST)
					{
						stack.Push(new IntermediateExpression(Math.Round(aFlexOp.value, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat), 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_FETCH1)
					{
						//int m = pFlexcontroller( (LocalFlexController_t)pops->d.index)->localToGlobal;
						//stack[k] = src[m];
						//k++; 
						stack.Push(new IntermediateExpression(this.theMdlFileData.theFlexControllers[aFlexOp.index].theName, 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_FETCH2)
					{
						stack.Push(new IntermediateExpression("%" + this.theMdlFileData.theFlexDescs[aFlexOp.index].theName, 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_ADD)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();

						string newExpr = Convert.ToString(leftIntermediate.theExpression) + " + " + Convert.ToString(rightIntermediate.theExpression);
						stack.Push(new IntermediateExpression(newExpr, 1));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_SUB)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();

						string newExpr = Convert.ToString(leftIntermediate.theExpression) + " - " + Convert.ToString(rightIntermediate.theExpression);
						stack.Push(new IntermediateExpression(newExpr, 1));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_MUL)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (rightIntermediate.thePrecedence < 2)
						{
							rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")";
						}
						else
						{
							rightExpr = rightIntermediate.theExpression;
						}

						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (leftIntermediate.thePrecedence < 2)
						{
							leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")";
						}
						else
						{
							leftExpr = leftIntermediate.theExpression;
						}

						string newExpr = leftExpr + " * " + rightExpr;
						stack.Push(new IntermediateExpression(newExpr, 2));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_DIV)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (rightIntermediate.thePrecedence < 2)
						{
							rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")";
						}
						else
						{
							rightExpr = rightIntermediate.theExpression;
						}

						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (leftIntermediate.thePrecedence < 2)
						{
							leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")";
						}
						else
						{
							leftExpr = leftIntermediate.theExpression;
						}

						string newExpr = leftExpr + " / " + rightExpr;
						stack.Push(new IntermediateExpression(newExpr, 2));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_NEG)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();

						string newExpr = "-" + rightIntermediate.theExpression;
						stack.Push(new IntermediateExpression(newExpr, 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_EXP)
					{
						int ignoreThisOpBecauseItIsMistakeToBeHere = 4242;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_OPEN)
					{
						int ignoreThisOpBecauseItIsMistakeToBeHere = 4242;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_CLOSE)
					{
						int ignoreThisOpBecauseItIsMistakeToBeHere = 4242;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_COMMA)
					{
						int ignoreThisOpBecauseItIsMistakeToBeHere = 4242;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_MAX)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (rightIntermediate.thePrecedence < 5)
						{
							rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")";
						}
						else
						{
							rightExpr = rightIntermediate.theExpression;
						}

						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (leftIntermediate.thePrecedence < 5)
						{
							leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")";
						}
						else
						{
							leftExpr = leftIntermediate.theExpression;
						}

						string newExpr = " max(" + leftExpr + ", " + rightExpr + ")";
						stack.Push(new IntermediateExpression(newExpr, 5));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_MIN)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (rightIntermediate.thePrecedence < 5)
						{
							rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")";
						}
						else
						{
							rightExpr = rightIntermediate.theExpression;
						}

						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (leftIntermediate.thePrecedence < 5)
						{
							leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")";
						}
						else
						{
							leftExpr = leftIntermediate.theExpression;
						}

						string newExpr = " min(" + leftExpr + ", " + rightExpr + ")";
						stack.Push(new IntermediateExpression(newExpr, 5));
						//TODO: SourceMdlFlexOp.STUDIO_2WAY_0
						//ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_2WAY_0 Then
						//	'	'#define STUDIO_2WAY_0	15	// Fetch a value from a 2 Way slider for the 1st value RemapVal( 0.0, 0.5, 0.0, 1.0 )
						//	'	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
						//	'	'stack[ k ] = RemapValClamped( src[m], -1.0f, 0.0f, 1.0f, 0.0f );
						//	'	'k++; 
						//	Dim newExpression As String
						//	'newExpression = CStr(Me.RemapValClamped(aFlexOp.value, -1, 0, 1, 0))
						//	newExpression = "RemapValClamped(" + theSourceEngineModel.theMdlFileHeader.theFlexControllers(aFlexOp.index).theName + ", -1, 0, 1, 0)"
						//	stack.Push(New IntermediateExpression(newExpression, 5))
						//TODO: SourceMdlFlexOp.STUDIO_2WAY_1
						//ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_2WAY_1 Then
						//	'#define STUDIO_2WAY_1	16	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
						//	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
						//	'stack[ k ] = RemapValClamped( src[m], 0.0f, 1.0f, 0.0f, 1.0f );
						//	'k++; 
						//	Dim newExpression As String
						//	'newExpression = CStr(Me.RemapValClamped(aFlexOp.value, 0, 1, 0, 1))
						//	newExpression = "RemapValClamped(" + theSourceEngineModel.theMdlFileHeader.theFlexControllers(aFlexOp.index).theName + ", 0, 1, 0, 1)"
						//	stack.Push(New IntermediateExpression(newExpression, 5))
						//TODO: SourceMdlFlexOp.STUDIO_NWAY
						//ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_NWAY Then
						//	Dim x As Integer = 42
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_COMBO)
					{
						//#define STUDIO_COMBO	18	// Perform a combo operation (essentially multiply the last N values on the stack)
						//int m = pops->d.index;
						//int km = k - m;
						//for ( int i = km + 1; i < k; ++i )
						//{
						//	stack[ km ] *= stack[ i ];
						//}
						//k = k - m + 1;
	//					Dim count As Integer
	//					Dim newExpression As String
	//					Dim intermediateExp As IntermediateExpression
						count = aFlexOp.index;
						newExpression = "";
						intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
						newExpression += intermediateExp.theExpression;
						for (int j = 2; j <= count; j++)
						{
							intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
							newExpression += " * " + intermediateExp.theExpression;
						}
						newExpression = "(" + newExpression + ")";
						stack.Push(new IntermediateExpression(newExpression, 5));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_DOMINATE)
					{
						//int m = pops->d.index;
						//int km = k - m;
						//float dv = stack[ km ];
						//for ( int i = km + 1; i < k; ++i )
						//{
						//	dv *= stack[ i ];
						//}
						//stack[ km - 1 ] *= 1.0f - dv;
						//k -= m;
	//					Dim count As Integer
	//					Dim newExpression As String
	//					Dim intermediateExp As IntermediateExpression
						count = aFlexOp.index;
						newExpression = "";
						intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
						newExpression += intermediateExp.theExpression;
						for (int j = 2; j <= count; j++)
						{
							intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
							newExpression += " * " + intermediateExp.theExpression;
						}
						intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
						newExpression = intermediateExp.theExpression + " * (1 - " + newExpression + ")";
						newExpression = "(" + newExpression + ")";
						stack.Push(new IntermediateExpression(newExpression, 5));
						//TODO: SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID
						//ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID Then
						//	Dim x As Integer = 42
						//TODO: SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID
						//ElseIf aFlexOp.op = SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID Then
						//	Dim x As Integer = 42
					}
					else
					{
						stack.Clear();
						break;
					}
				}

				// The loop above leaves the final expression on the top of the stack.
				if (stack.Count == 1)
				{
					flexRuleEquation += stack.Peek().theExpression;
				}
				else if (stack.Count == 0 || stack.Count > 1)
				{
					flexRuleEquation = "// [Decompiler failed to parse expression. Please report the error with the following info: this qc file, the mdl filename that was decompiled, and where the mdl file was found (e.g. the game's name or a web link).]";
				}
				else
				{
					flexRuleEquation = "// [Empty flex rule found and ignored.]";
				}
			}
			return flexRuleEquation;
		}

		public void WriteGroupLod()
		{
			this.WriteLodCommand();
		}

		private void WriteLodCommand()
		{
			string line = "";

			//NOTE: Data is from VTX file.
			//$lod 10
			// {
			//  replacemodel "producer_model_merged.dmx" "lod1_producer_model_merged.dmx"
			//}
			//$lod 15
			// {
			//  replacemodel "producer_model_merged.dmx" "lod2_producer_model_merged.dmx"
			//}
			//$lod 40
			// {
			//  replacemodel "producer_model_merged.dmx" "lod3_producer_model_merged.dmx"
			//}
			if (this.theVtxFileData != null && this.theMdlFileData.theBodyParts != null)
			{
				//Dim referenceSmdFileName As String
				//Dim lodSmdFileName As String

				if (this.theVtxFileData.theVtxBodyParts == null)
				{
					return;
				}
				if (this.theVtxFileData.theVtxBodyParts[0].theVtxModels == null)
				{
					return;
				}
				if (this.theVtxFileData.theVtxBodyParts[0].theVtxModels[0].theVtxModelLods == null)
				{
					return;
				}

				//referenceSmdFileName = theSourceEngineModel.GetBodyGroupSmdFileName(0, 0, 0)

				//line = ""
				//Me.theOutputFileStreamWriter.WriteLine(line)

				//'NOTE: Start loop at 1 to skip first LOD, which isn't needed for the $lod command.
				//For lodIndex As Integer = 1 To theSourceEngineModel.theVtxFileHeader.lodCount - 1
				//	Dim switchPoint As Single
				//	'TODO: Need to check that each of these objects exist first before using them.
				//	If lodIndex >= theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods.Count Then
				//		Return
				//	End If

				//	switchPoint = theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(0).theVtxModels(0).theVtxModelLods(lodIndex).switchPoint

				//	lodSmdFileName = theSourceEngineModel.GetBodyGroupSmdFileName(0, 0, lodIndex)

				//	line = ""
				//	If switchPoint = -1 Then
				//		'// Shadow lod reserves -1 as switch value
				//		'// which uniquely identifies a shadow lod
				//		'newLOD.switchValue = -1.0f;
				//		line += "$shadowlod"
				//	Else
				//If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
				//	line += "$LOD "
				//Else
				//	line += "$lod "
				//End If
				//		line += switchPoint.ToString("0.######", TheApp.InternalNumberFormat)
				//	End If
				//	Me.theOutputFileStreamWriter.WriteLine(line)

				//	line = "{"
				//	Me.theOutputFileStreamWriter.WriteLine(line)

				//	line = vbTab
				//	line += "replacemodel "
				//	line += """"
				//	line += referenceSmdFileName
				//	line += """ """
				//	line += lodSmdFileName
				//	line += """"
				//	Me.theOutputFileStreamWriter.WriteLine(line)

				//	line = "}"
				//	Me.theOutputFileStreamWriter.WriteLine(line)
				//Next
				//======
				SourceVtxBodyPart06 aBodyPart = null;
				SourceVtxModel06 aVtxModel = null;
				SourceMdlModel37 aBodyModel = null;
				LodQcInfo aLodQcInfo = null;
				List<LodQcInfo> aLodQcInfoList = null;
				SortedList<double, List<LodQcInfo>> aLodList = null;
				double switchPoint = 0;

				aLodList = new SortedList<double, List<LodQcInfo>>();
				for (int bodyPartIndex = 0; bodyPartIndex < this.theVtxFileData.theVtxBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = this.theVtxFileData.theVtxBodyParts[bodyPartIndex];

					if (aBodyPart.theVtxModels != null)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theVtxModels.Count; modelIndex++)
						{
							aVtxModel = aBodyPart.theVtxModels[modelIndex];

							if (aVtxModel.theVtxModelLods != null)
							{
								aBodyModel = this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex];
								//If aModel.name(0) = ChrW(0) Then
								//	Continue For
								//End If

								//NOTE: Start loop at 1 to skip first LOD, which isn't needed for the $lod command.
								for (int lodIndex = 1; lodIndex < this.theVtxFileData.lodCount; lodIndex++)
								{
									//TODO: Why would this count be different than the file header count?
									if (lodIndex >= aVtxModel.theVtxModelLods.Count)
									{
										break;
									}

									//If lodIndex = 0 Then
									//    If Not TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked Then
									//        Continue For
									//    End If
									//ElseIf lodIndex > 0 Then
									//    If Not TheApp.Settings.DecompileLodMeshSmdFilesIsChecked Then
									//        Exit For
									//    End If
									//End If

									switchPoint = aVtxModel.theVtxModelLods[lodIndex].switchPoint;
									if (!aLodList.ContainsKey(switchPoint))
									{
										aLodQcInfoList = new List<LodQcInfo>();
										aLodList.Add(switchPoint, aLodQcInfoList);
									}
									else
									{
										aLodQcInfoList = aLodList[switchPoint];
									}

									aBodyModel.theSmdFileNames[0] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[0], bodyPartIndex, modelIndex, 0, this.theModelName, new string(this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name));
									aBodyModel.theSmdFileNames[lodIndex] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[lodIndex], bodyPartIndex, modelIndex, lodIndex, this.theModelName, new string(this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name));
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
				this.theOutputFileStreamWriter.WriteLine(line);

				List<LodQcInfo> lodQcInfoListOfShadowLod = null;

				for (int lodListIndex = 0; lodListIndex < aLodList.Count; lodListIndex++)
				{
					switchPoint = aLodList.Keys[lodListIndex];
					if (switchPoint == -1)
					{
						// Skip writing $shadowlod. Write it last after this loop.
						lodQcInfoListOfShadowLod = aLodList.Values[lodListIndex];
						continue;
					}

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
					this.theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					this.theOutputFileStreamWriter.WriteLine(line);

					for (int i = 0; i < aLodQcInfoList.Count; i++)
					{
						aLodQcInfo = aLodQcInfoList[i];

						line = "\t";
						line += "replacemodel ";
						line += "\"";
						line += aLodQcInfo.referenceFileName;
						line += "\" \"";
						line += aLodQcInfo.lodFileName;
						line += "\"";
						this.theOutputFileStreamWriter.WriteLine(line);
					}

					line = "}";
					this.theOutputFileStreamWriter.WriteLine(line);
				}

				//NOTE: As a requirement for the compiler, write $shadowlod last.
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
					this.theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					this.theOutputFileStreamWriter.WriteLine(line);

					for (int i = 0; i < lodQcInfoListOfShadowLod.Count; i++)
					{
						aLodQcInfo = lodQcInfoListOfShadowLod[i];

						line = "\t";
						line += "replacemodel ";
						line += "\"";
						line += aLodQcInfo.referenceFileName;
						line += "\" \"";
						line += aLodQcInfo.lodFileName;
						line += "\"";
						this.theOutputFileStreamWriter.WriteLine(line);
					}

					line = "}";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteNoForcedFadeCommand()
		{
			string line = "";

			//$noforcedfade
			if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_NO_FORCED_FADE) > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$NoForcedFade";
				}
				else
				{
					line = "$noforcedfade";
				}
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteForcePhonemeCrossfadeCommand()
		{
			string line = "";

			//$forcephonemecrossfade
			if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_FORCE_PHONEME_CROSSFADE) > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$ForcePhonemeCrossFade";
				}
				else
				{
					line = "$forcephonemecrossfade";
				}
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WritePoseParameterCommand()
		{
			string line = "";

			//$poseparameter body_pitch -90.00 90.00 360.00
			//$poseparameter body_yaw -90.00 90.00 360.00
			//$poseparameter head_pitch -90.00 90.00 360.00
			//$poseparameter head_yaw -90.00 90.00 360.00
			if (this.theMdlFileData.thePoseParamDescs != null)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.thePoseParamDescs.Count; i++)
				{
					SourceMdlPoseParamDesc aPoseParamDesc = this.theMdlFileData.thePoseParamDescs[i];
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
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteAmbientBoostCommand()
		{
			string line = "";

			//$ambientboost
			if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_AMBIENT_BOOST) > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$AmbientBoost";
				}
				else
				{
					line = "$ambientboost";
				}
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteOpaqueCommand()
		{
			string line = "";

			//$mostlyopaque
			//$opaque
			if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_FORCE_OPAQUE) > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$Opaque";
				}
				else
				{
					line = "$opaque";
				}
				this.theOutputFileStreamWriter.WriteLine(line);
			}
			else if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_TRANSLUCENT_TWOPASS) > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$MostlyOpaque";
				}
				else
				{
					line = "$mostlyopaque";
				}
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteObsoleteCommand()
		{
			string line = "";

			//$obsolete
			if ((this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_OBSOLETE) > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$Obsolete";
				}
				else
				{
					line = "$obsolete";
				}
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteCdMaterialsCommand()
		{
			string line = "";

			//$cdmaterials "models\survivors\producer\"
			//$cdmaterials "models\survivors\"
			//$cdmaterials ""
			if (this.theMdlFileData.theTexturePaths != null)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.theTexturePaths.Count; i++)
				{
					string aTexturePath = this.theMdlFileData.theTexturePaths[i];
					//NOTE: Write out null or empty strings, because Crowbar should show what was stored.
					//If Not String.IsNullOrEmpty(aTexturePath) Then
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
					this.theOutputFileStreamWriter.WriteLine(line);
					//End If
				}
			}
		}

		public void WriteTextureGroupCommand()
		{
			string line = "";

			if (this.theMdlFileData.theSkinFamilies != null && this.theMdlFileData.theSkinFamilies.Count > 0 && this.theMdlFileData.theTextures != null && this.theMdlFileData.theTextures.Count > 0 && this.theMdlFileData.skinReferenceCount > 0)
			{
				List<List<short>> processedSkinFamilies = null;
				if (MainCROWBAR.TheApp.Settings.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked)
				{
					processedSkinFamilies = this.GetSkinFamiliesOfChangedMaterials(this.theMdlFileData.theSkinFamilies);
				}
				else
				{
					processedSkinFamilies = this.theMdlFileData.theSkinFamilies;
				}

				List<List<string>> skinFamiliesOfTextureFileNames = new List<List<string>>(processedSkinFamilies.Count);
				int skinReferenceCount = processedSkinFamilies[0].Count;
				for (int i = 0; i < processedSkinFamilies.Count; i++)
				{
					List<short> aSkinFamily = processedSkinFamilies[i];

					List<string> textureFileNames = new List<string>(skinReferenceCount);
					for (int j = 0; j < skinReferenceCount; j++)
					{
						SourceMdlTexture37 aTexture = this.theMdlFileData.theTextures[aSkinFamily[j]];

						textureFileNames.Add(aTexture.theFileName);
					}

					skinFamiliesOfTextureFileNames.Add(textureFileNames);
				}

				if ((!MainCROWBAR.TheApp.Settings.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked) || (skinFamiliesOfTextureFileNames.Count > 1))
				{
					this.theOutputFileStreamWriter.WriteLine();

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$TextureGroup \"skinfamilies\"";
					}
					else
					{
						line = "$texturegroup \"skinfamilies\"";
					}
					this.theOutputFileStreamWriter.WriteLine(line);
					line = "{";
					this.theOutputFileStreamWriter.WriteLine(line);

					List<string> skinFamilyLines = this.GetTextureGroupSkinFamilyLines(skinFamiliesOfTextureFileNames);
					for (int skinFamilyLineIndex = 0; skinFamilyLineIndex < skinFamilyLines.Count; skinFamilyLineIndex++)
					{
						this.theOutputFileStreamWriter.WriteLine(skinFamilyLines[skinFamilyLineIndex]);
					}

					line = "}";
					this.theOutputFileStreamWriter.WriteLine(line);
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
			if (this.theMdlFileData.theTextures != null)
			{
				string line = "";

				this.theOutputFileStreamWriter.WriteLine(line);

				line = "// This list shows the VMT file names used in the SMD files.";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int j = 0; j < this.theMdlFileData.theTextures.Count; j++)
				{
					SourceMdlTexture37 aTexture = this.theMdlFileData.theTextures[j];
					line = "// \"";
					line += aTexture.theFileName;
					line += ".vmt\"";
					this.theOutputFileStreamWriter.WriteLine(line);
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
			//$attachment "pills" "ValveBiped.Bip01_Spine" -2.63 0.63 -7.56 rotate -41.18 -88.48 -87.05
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
			if (this.theMdlFileData.theAttachments != null)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.theAttachments.Count; i++)
				{
					SourceMdlAttachment37 anAttachment = this.theMdlFileData.theAttachments[i];
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
						line += i.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					}
					else
					{
						line += "\"";
						line += anAttachment.theName;
						line += "\"";
					}
					line += " \"";
					line += this.theMdlFileData.theBones[anAttachment.boneIndex].theName;
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

					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteIncludeModelCommands()
		{
			string line = "";

			//$includemodel "survivors/anim_producer.mdl"
			//$includemodel "survivors/anim_gestures.mdl"
			if (this.theMdlFileData.theIncludeModelFileNames != null)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.theIncludeModelFileNames.Count; i++)
				{
					string anIncludeModelFileName = this.theMdlFileData.theIncludeModelFileNames[i];
					if (!string.IsNullOrEmpty(anIncludeModelFileName))
					{
						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$IncludeModel ";
						}
						else
						{
							line = "$includemodel ";
						}
						line += "\"";
						if (anIncludeModelFileName.StartsWith("models/"))
						{
							line += anIncludeModelFileName.Substring(7);
						}
						else
						{
							line += anIncludeModelFileName;
						}
						line += "\"";
						this.theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		public void WriteSurfacePropCommand()
		{
			string line = "";

			if (!string.IsNullOrEmpty(this.theMdlFileData.theSurfacePropName))
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

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
				line += this.theMdlFileData.theSurfacePropName;
				line += "\"";
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteJointSurfacePropCommand()
		{
			string line = "";

			//$jointsurfaceprop <bone name> <surfaceprop>
			//$jointsurfaceprop "ValveBiped.Bip01_L_Toe0"	 "flesh"
			if (this.theMdlFileData.theBones != null)
			{
				SourceMdlBone37 aBone = null;
				bool emptyLineIsAlreadyWritten = false;

				for (int i = 0; i < this.theMdlFileData.theBones.Count; i++)
				{
					aBone = this.theMdlFileData.theBones[i];

					if (aBone.theSurfacePropName != this.theMdlFileData.theSurfacePropName)
					{
						if (!emptyLineIsAlreadyWritten)
						{
							line = "";
							this.theOutputFileStreamWriter.WriteLine(line);
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
						this.theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		public void WriteContentsCommand()
		{
			if (this.theMdlFileData.contents > 0)
			{
				string line = "";

				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				//$contents "monster" "grate"
				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line = "$Contents";
				}
				else
				{
					line = "$contents";
				}
				line += this.GetContentsFlags(this.theMdlFileData.contents);
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		public void WriteJointContentsCommand()
		{
			string line = "";

			//$jointcontents "<bone_name>" "<content_type_1>" "<content_type_2>" "<content_type_3>"
			if (this.theMdlFileData.theBones != null)
			{
				SourceMdlBone37 aBone = null;
				bool emptyLineIsAlreadyWritten = false;

				for (int i = 0; i < this.theMdlFileData.theBones.Count; i++)
				{
					aBone = this.theMdlFileData.theBones[i];

					if (aBone.contents != this.theMdlFileData.contents)
					{
						if (!emptyLineIsAlreadyWritten)
						{
							line = "";
							this.theOutputFileStreamWriter.WriteLine(line);
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
						line += this.GetContentsFlags(aBone.contents);
						this.theOutputFileStreamWriter.WriteLine(line);
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

			offsetX = Math.Round(this.theMdlFileData.eyePosition.y, 3);
			offsetY = -Math.Round(this.theMdlFileData.eyePosition.x, 3);
			offsetZ = Math.Round(this.theMdlFileData.eyePosition.z, 3);

			if (offsetX == 0 && offsetY == 0 && offsetZ == 0)
			{
				return;
			}

			line = "";
			this.theOutputFileStreamWriter.WriteLine(line);

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
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteIllumPositionCommand()
		{
			string line = null;
			double offsetX = 0;
			double offsetY = 0;
			double offsetZ = 0;

			offsetX = Math.Round(this.theMdlFileData.illuminationPosition.y, 3);
			offsetY = -Math.Round(this.theMdlFileData.illuminationPosition.x, 3);
			offsetZ = Math.Round(this.theMdlFileData.illuminationPosition.z, 3);

			line = "";
			this.theOutputFileStreamWriter.WriteLine(line);

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
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteGroupAnimation()
		{
			this.WritePoseParameterCommand();
			this.WriteIkChainCommand();
			this.WriteIkAutoPlayLockCommand();
			this.FillInWeightLists();
			//NOTE: Must write $WeightList lines before animations or sequences that use them.
			this.WriteWeightListCommand();
			//NOTE: Must write $animation lines before $sequence lines that use them.
			try
			{
				this.WriteAnimationOrDeclareAnimationCommand();
			}
			catch (Exception ex)
			{
			}
			try
			{
				this.WriteSequenceOrDeclareSequenceCommand();
			}
			catch (Exception ex)
			{
			}
			this.WriteIncludeModelCommands();
		}

		private void FillInWeightLists()
		{
			if (this.theMdlFileData.theSequenceDescs != null)
			{
				SourceMdlSequenceDesc37 aSeqDesc = null;
				SourceMdlWeightList aWeightList = null;
				int aWeightListIndex = 0;

				for (int i = 0; i < this.theMdlFileData.theSequenceDescs.Count; i++)
				{
					aSeqDesc = this.theMdlFileData.theSequenceDescs[i];

					try
					{
						if (aSeqDesc.theBoneWeights != null && aSeqDesc.theBoneWeights.Count > 0 && !aSeqDesc.theBoneWeightsAreDefault)
						{
							for (aWeightListIndex = 0; aWeightListIndex < this.theMdlFileData.theWeightLists.Count; aWeightListIndex++)
							{
								aWeightList = this.theMdlFileData.theWeightLists[aWeightListIndex];

								if (GenericsModule.ListsAreEqual(aSeqDesc.theBoneWeights, aWeightList.theWeights))
								{
									break;
								}
							}

							if (aWeightListIndex < this.theMdlFileData.theWeightLists.Count)
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

								this.theMdlFileData.theWeightLists.Add(aWeightList);

								aSeqDesc.theWeightListIndex = this.theMdlFileData.theWeightLists.Count - 1;
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
			foreach (SourceMdlWeightList aWeightList in this.theMdlFileData.theWeightLists)
			{
				this.theOutputFileStreamWriter.WriteLine();

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
				this.theOutputFileStreamWriter.WriteLine(commentTag + line);

				for (int boneWeightIndex = 0; boneWeightIndex < aWeightList.theWeights.Count; boneWeightIndex++)
				{
					line = "\t";
					line += " \"";
					line += this.theMdlFileData.theBones[boneWeightIndex].theName;
					line += "\" ";
					line += aWeightList.theWeights[boneWeightIndex].ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(commentTag + line);
				}

				line = "}";
				this.theOutputFileStreamWriter.WriteLine(commentTag + line);
			}
		}

		private void WriteAnimationOrDeclareAnimationCommand()
		{
			if (this.theMdlFileData.theAnimationDescs != null)
			{
				for (int i = 0; i < this.theMdlFileData.theAnimationDescs.Count; i++)
				{
					SourceMdlAnimationDesc37 anAnimationDesc = this.theMdlFileData.theAnimationDescs[i];

					if (anAnimationDesc.theName[0] != '@')
					{
						this.WriteAnimationLine(anAnimationDesc);
					}
				}
			}
		}

		private void WriteAnimationLine(SourceMdlAnimationDesc37 anAnimationDesc)
		{
			string line = "";

			this.theOutputFileStreamWriter.WriteLine();

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
				this.theOutputFileStreamWriter.WriteLine(line);
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
				line += " \"";
				if (!anAnimationDesc.theName.StartsWith("a_"))
				{
					line += "a_";
				}
				anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, this.theModelName, anAnimationDesc.theName);
				line += anAnimationDesc.theName;
				line += "\" \"";
				line += anAnimationDesc.theSmdRelativePathFileName;
				line += "\"";
				//NOTE: Opening brace must be on same line as the command.
				line += " {";
				this.theOutputFileStreamWriter.WriteLine(line);

				this.WriteAnimationOptions(null, anAnimationDesc, null);

				line = "}";
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		private void WriteSequenceOrDeclareSequenceCommand()
		{
			//$sequence producer "producer" fps 30.00
			//$sequence ragdoll "ragdoll" ACT_DIERAGDOLL 1 fps 30.00
			if (this.theMdlFileData.theSequenceDescs != null)
			{
				for (int i = 0; i < this.theMdlFileData.theSequenceDescs.Count; i++)
				{
					SourceMdlSequenceDesc37 aSequenceDesc = this.theMdlFileData.theSequenceDescs[i];

					this.WriteSequenceLine(aSequenceDesc);
				}
			}
		}

		private void WriteSequenceLine(SourceMdlSequenceDesc37 aSequenceDesc)
		{
			string line = "";

			this.theOutputFileStreamWriter.WriteLine();

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
				this.theOutputFileStreamWriter.WriteLine(line);
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
					this.theOutputFileStreamWriter.WriteLine(line);

					try
					{
						this.WriteSequenceOptions(aSequenceDesc);
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}

					line = "}";
					this.theOutputFileStreamWriter.WriteLine(line);
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
		//posecycle          // v48; done
		//post               // baked-in
		//predelta           // done
		//realtime           // done
		//rtransition        // done
		//snap               // done
		//transition         // done
		//worldspace         // done       
		//ParseAnimationToken( animations[0] )
		//Cmd_ImpliedAnimation( pseq, token )
		private void WriteSequenceOptions(SourceMdlSequenceDesc37 aSequenceDesc)
		{
			string line = "";
			string valueString = null;
			SourceMdlAnimationDesc37 impliedAnimDesc = null;

			SourceMdlAnimationDesc37 anAnimationDesc = null;
			string name = null;
			for (int j = 0; j < aSequenceDesc.theAnimDescIndexes.Count; j++)
			{
				anAnimationDesc = this.theMdlFileData.theAnimationDescs[aSequenceDesc.theAnimDescIndexes[j]];
				name = anAnimationDesc.theName;

				line = "\t";
				line += "\"";
				if (name[0] == '@')
				{
					//NOTE: There should only be one implied anim desc.
					impliedAnimDesc = anAnimationDesc;
					anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, this.theModelName, anAnimationDesc.theName);
					line += anAnimationDesc.theSmdRelativePathFileName;
				}
				else
				{
					if (!name.StartsWith("a_"))
					{
						line += "a_";
					}
					line += name;
				}
				line += "\"";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			if (!string.IsNullOrEmpty(aSequenceDesc.theActivityName))
			{
				line = "\t";
				line += "activity ";
				line += "\"";
				line += aSequenceDesc.theActivityName;
				line += "\" ";
				line += aSequenceDesc.activityWeight.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_AUTOPLAY) > 0)
			{
				line = "\t";
				line += "autoplay";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			this.WriteSequenceBlendInfo(aSequenceDesc);

			if (aSequenceDesc.groupSize[0] != aSequenceDesc.groupSize[1])
			{
				line = "\t";
				line += "blendwidth ";
				line += aSequenceDesc.groupSize[0].ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			this.WriteSequenceDeltaInfo(aSequenceDesc);

			if (aSequenceDesc.theEvents != null)
			{
				int frameIndex = 0;
				int frameCount = this.theMdlFileData.theAnimationDescs[aSequenceDesc.theAnimDescIndexes[0]].frameCount;
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
					line += aSequenceDesc.theEvents[j].eventIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += frameIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					if (aSequenceDesc.theEvents[j].options != "".ToCharArray())
					{
						line += " \"";
						line += (new string(aSequenceDesc.theEvents[j].options)).Trim('\0');
						line += "\"";
					}
					line += " }";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			valueString = aSequenceDesc.fadeInTime.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line = "\t";
			line += "fadein ";
			line += valueString;
			this.theOutputFileStreamWriter.WriteLine(line);

			valueString = aSequenceDesc.fadeOutTime.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			line = "\t";
			line += "fadeout ";
			line += valueString;
			this.theOutputFileStreamWriter.WriteLine(line);

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_HIDDEN) > 0)
			{
				line = "\t";
				line += "hidden";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			//If aSeqDesc.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theIkLocks IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theIkChains IsNot Nothing Then
			if (aSequenceDesc.theIkLocks != null && this.theMdlFileData.theIkChains != null)
			{
				SourceMdlIkLock37 ikLock = null;

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
					line += this.theMdlFileData.theIkChains[ikLock.chainIndex].theName;
					line += "\"";
					line += " ";
					line += ikLock.posWeight.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += ikLock.localQWeight.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			this.WriteKeyValues(aSequenceDesc.theKeyValues, "keyvalues");

			this.WriteSequenceLayerInfo(aSequenceDesc);

			//TODO: WriteSequenceNodeInfo()
			this.WriteSequenceNodeInfo(aSequenceDesc);

			//If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_REALTIME) > 0 Then
			//	line = vbTab
			//	line += "realtime"
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_SNAP) > 0)
			{
				line = "\t";
				line += "snap";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_WORLD) > 0)
			{
				line = "\t";
				line += "worldspace";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			//If blah Then
			//	line = vbTab
			//	line += ""
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If

			SourceMdlAnimationDesc37 firstAnimDesc = this.theMdlFileData.theAnimationDescs[aSequenceDesc.theAnimDescIndexes[0]];
			//Me.WriteAnimationOptions(aSequenceDesc, firstAnimDesc, impliedAnimDesc)
			if (impliedAnimDesc != null)
			{
				this.WriteAnimationOptions(aSequenceDesc, firstAnimDesc, impliedAnimDesc);
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
		private void WriteAnimationOptions(SourceMdlSequenceDesc37 aSequenceDesc, SourceMdlAnimationDesc37 anAnimationDesc, SourceMdlAnimationDesc37 impliedAnimDesc)
		{
			string line = "";

			line = "\t";
			line += "fps ";
			line += anAnimationDesc.fps.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			this.theOutputFileStreamWriter.WriteLine(line);

			if (aSequenceDesc == null)
			{
				if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_LOOPING) > 0)
				{
					line = "\t";
					line += "loop";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
			else
			{
				if ((aSequenceDesc.flags & SourceMdlAnimationDesc.STUDIO_LOOPING) > 0)
				{
					line = "\t";
					line += "loop";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			this.WriteCmdListOptions(aSequenceDesc, anAnimationDesc, impliedAnimDesc);
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
		private void WriteCmdListOptions(SourceMdlSequenceDesc37 aSequenceDesc, SourceMdlAnimationDesc37 anAnimationDesc, SourceMdlAnimationDesc37 impliedAnimDesc)
		{
			string line = "";

			if (anAnimationDesc.theIkRules != null)
			{
				int endFrameIndex = 0;
				double tempValue = 0;
				endFrameIndex = anAnimationDesc.frameCount - 1;

				//NOTE: Copied from MDL v37 studiomdl.exe:
				//contact
				//pad
				//floor
				//range
				//height
				//------
				//release
				//world
				//footstep
				//touch
				foreach (SourceMdlIkRule37 anIkRule in anAnimationDesc.theIkRules)
				{
					line = "\t";
					line += "ikrule";
					line += " \"";
					line += this.theMdlFileData.theIkChains[anIkRule.chain].theName;
					line += "\"";
					if (anIkRule.type == SourceMdlIkRule.IK_SELF)
					{
						line += " touch \"";
						if (anIkRule.bone >= 0)
						{
							line += this.theMdlFileData.theBones[anIkRule.bone].theName;
						}
						line += "\"";
					}
					else if (anIkRule.type == SourceMdlIkRule.IK_WORLD)
					{
						line += " world ";
						line += anIkRule.slot.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					}
					else if (anIkRule.type == SourceMdlIkRule.IK_GROUND)
					{
						line += " footstep ";
						line += anIkRule.slot.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					}
					else if (anIkRule.type == SourceMdlIkRule.IK_RELEASE)
					{
						line += " release";
					}

					//pikrule->contact= g_panimation[i]->ikrule[j].contact / (g_panimation[i]->numframes - 1.0f);
					tempValue = anIkRule.contact * endFrameIndex;
					line += " contact ";
					line += tempValue.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " pad ";
					line += anIkRule.radius.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " floor ";
					line += anIkRule.floor.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					//pikrule->start	= g_panimation[i]->ikrule[j].start / (g_panimation[i]->numframes - 1.0f);
					//pikrule->peak	= g_panimation[i]->ikrule[j].peak / (g_panimation[i]->numframes - 1.0f);
					//pikrule->tail	= g_panimation[i]->ikrule[j].tail / (g_panimation[i]->numframes - 1.0f);
					//pikrule->end	= g_panimation[i]->ikrule[j].end / (g_panimation[i]->numframes - 1.0f);
					line += " range ";
					tempValue = anIkRule.influenceStart * endFrameIndex;
					line += tempValue.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					tempValue = anIkRule.influencePeak * endFrameIndex;
					line += tempValue.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					tempValue = anIkRule.influenceTail * endFrameIndex;
					line += tempValue.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					tempValue = anIkRule.influenceEnd * endFrameIndex;
					line += tempValue.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " height ";
					line += anIkRule.height.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_ALLZEROS) > 0)
			{
				line = "\t";
				line += "noanimation";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			//'TODO: This seems valid according to source code, but it checks same flag (STUDIO_DELTA) as "delta" option.
			//'      Unsure how to determine which option is intended or if both are intended.
			//If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
			//	line = vbTab
			//	line += "// This subtract line guesses the animation name and frame index. There is no way to determine which $animation and which frame was used. Change as needed."
			//	Me.theOutputFileStreamWriter.WriteLine(line)

			//	line = vbTab
			//	'line += "// "
			//	line += "subtract"
			//	line += " """
			//	'TODO: Change to writing anim_name.
			//	' Doesn't seem to be direct way to get this name.
			//	' For now, do what MDL Decompiler seems to do; use the first animation name.
			//	'line += "[anim_name]"
			//	'line += Me.theFirstAnimationDescName
			//	line += Me.theMdlFileData.theFirstAnimationDesc.theName
			//	line += """ "
			//	'TODO: Change to writing frameIndex.
			//	' Doesn't seem to be direct way to get this value.
			//	' For now, do what MDL Decompiler seems to do; use zero for the frameIndex.
			//	'line += "[frameIndex]"
			//	line += "0"
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If

			if (aSequenceDesc == null && anAnimationDesc.theMovements != null)
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
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			//TODO: Can probably reduce the info written in v0.24.
			// weightlist "top_bottom"
			SourceMdlSequenceDesc37 aSeqDesc = null;
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
				this.WriteSequenceWeightListLine(aSeqDesc);
			}
		}

		private void WriteSequenceWeightListLine(SourceMdlSequenceDesc37 aSeqDesc)
		{
			string line = "";

			line = "\t";
			line += "weightlist ";
			//NOTE: Name is not stored, so use something reasonable. Needs to be the same as used in $weightlist.
			line += "\"";
			//line += "weights_"
			//line += aSeqDesc.theName
			line += this.theMdlFileData.theWeightLists[aSeqDesc.theWeightListIndex].theName;
			line += "\"";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		private void WriteSequenceBlendInfo(SourceMdlSequenceDesc37 aSeqDesc)
		{
			string line = "";

			for (int i = 0; i <= 1; i++)
			{
				if (aSeqDesc.paramIndex[i] != -1)
				{
					line = "\t";
					line += "blend ";
					line += "\"";
					line += this.theMdlFileData.thePoseParamDescs[aSeqDesc.paramIndex[i]].theName;
					line += "\"";
					line += " ";
					line += aSeqDesc.paramStart[i].ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aSeqDesc.paramEnd[i].ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteSequenceDeltaInfo(SourceMdlSequenceDesc37 aSeqDesc)
		{
			string line = "";

			if ((aSeqDesc.flags & SourceMdlAnimationDesc.STUDIO_DELTA) > 0)
			{
				if ((aSeqDesc.flags & SourceMdlAnimationDesc.STUDIO_POST) > 0)
				{
					line = "\t";
					//line += "// "
					line += "delta";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
				else
				{
					line = "\t";
					line += "predelta";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteSequenceLayerInfo(SourceMdlSequenceDesc37 aSeqDesc)
		{
			if (aSeqDesc.autoLayerCount > 0)
			{
				string line = "";
				SourceMdlAutoLayer37 layer = null;
				string otherSequenceName = null;

				for (int j = 0; j < aSeqDesc.theAutoLayers.Count; j++)
				{
					layer = aSeqDesc.theAutoLayers[j];
					otherSequenceName = this.theMdlFileData.theSequenceDescs[layer.sequenceIndex].theName;

					if (layer.flags == 0)
					{
						//addlayer <string|other $sequence name>
						line = "\t";
						//line += "// "
						line += "addlayer ";
						line += "\"";
						line += otherSequenceName;
						line += "\"";
						this.theOutputFileStreamWriter.WriteLine(line);
					}
					else
					{
						//blendlayer <string|other $sequence name> <int|startframe> <int|peakframe> <int|tailframe> <int|endframe> [spline] [xfade]
						line = "\t";
						line += "blendlayer ";
						line += "\"";
						line += otherSequenceName;
						line += "\"";

						line += " ";
						line += layer.influenceStart.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += layer.influencePeak.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += layer.influenceTail.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += layer.influenceEnd.ToString("0.##", MainCROWBAR.TheApp.InternalNumberFormat);

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
						if ((layer.flags & SourceMdlAutoLayer.STUDIO_AL_LOCAL) > 0)
						{
							line += " local";
						}

						this.theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		private void WriteSequenceNodeInfo(SourceMdlSequenceDesc37 aSeqDesc)
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
					this.theOutputFileStreamWriter.WriteLine(line);
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
					this.theOutputFileStreamWriter.WriteLine(line);
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
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteIkChainCommand()
		{
			string line = "";
			double offsetX = 0;
			double offsetY = 0;
			double offsetZ = 0;

			try
			{
				if (this.theMdlFileData.theIkChains != null)
				{
					line = "";
					this.theOutputFileStreamWriter.WriteLine(line);

					for (int i = 0; i < this.theMdlFileData.theIkChains.Count; i++)
					{
						int boneIndex = this.theMdlFileData.theIkChains[i].theLinks[this.theMdlFileData.theIkChains[i].theLinks.Count - 1].boneIndex;
						offsetX = Math.Round(this.theMdlFileData.theIkChains[i].theLinks[0].contact.x, 3);
						offsetY = Math.Round(this.theMdlFileData.theIkChains[i].theLinks[0].contact.y, 3);
						offsetZ = Math.Round(this.theMdlFileData.theIkChains[i].theLinks[0].contact.z, 3);

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$IKChain \"";
						}
						else
						{
							line = "$ikchain \"";
						}
						line += this.theMdlFileData.theIkChains[i].theName;
						line += "\" \"";
						line += this.theMdlFileData.theBones[boneIndex].theName;
						line += "\"";

						line += " knee ";
						line += offsetX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += offsetY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += offsetZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						//------
						//		GetToken(false);
						//
						//		if (lookupControl( token ) != -1)
						//		{
						//			g_ikchain[g_numikchains].axis = lookupControl( token );
						//			GetToken(false);
						//			g_ikchain[g_numikchains].value = atof( token );
						//		}
						//		else if (_strcmpi( "height", token ) == 0)
						//		{
						//			GetToken(false);
						//			g_ikchain[g_numikchains].height = atof( token );
						//		}
						//		else if (_strcmpi( "pad", token ) == 0)
						//		{
						//			GetToken(false);
						//			g_ikchain[g_numikchains].radius = atof( token ) / 2.0;
						//		}
						//		else if (_strcmpi( "floor", token ) == 0)
						//		{
						//			GetToken(false);
						//			g_ikchain[g_numikchains].floor = atof( token );
						//		}

						this.theOutputFileStreamWriter.WriteLine(line);
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
			SourceMdlIkLock37 ikLock = null;

			//$ikautoplaylock <chain name> <pos lock> <angle lock>
			//$ikautoplaylock rfoot 1.0 0.1
			//$ikautoplaylock lfoot 1.0 0.1
			try
			{
				if (this.theMdlFileData.theIkLocks != null)
				{
					line = "";
					this.theOutputFileStreamWriter.WriteLine(line);

					for (int i = 0; i < this.theMdlFileData.theIkLocks.Count; i++)
					{
						ikLock = this.theMdlFileData.theIkLocks[i];

						if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
						{
							line = "$IKAutoPlayLock \"";
						}
						else
						{
							line = "$ikautoplaylock \"";
						}
						line += this.theMdlFileData.theIkChains[ikLock.chainIndex].theName;
						line += "\"";
						line += " ";
						line += ikLock.posWeight.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += ikLock.localQWeight.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						this.theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		public void WriteGroupCollision()
		{
			this.WriteCollisionModelOrCollisionJointsCommand();
			this.WriteCollisionTextCommand();
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
			if (this.thePhyFileData != null && this.thePhyFileData.solidCount > 0)
			{
				this.theOutputFileStreamWriter.WriteLine(line);

				//If Me.theSourceEngineModel.PhyFileHeader.checksum <> Me.theSourceEngineModel.MdlFileHeader.checksum Then
				//	line = "// The PHY file's checksum value is not the same as the MDL file's checksum value."
				//	Me.theOutputFileStreamWriter.WriteLine(line)
				//End If

				//NOTE: The smd file name for $collisionjoints is not stored in the mdl file.
				//TODO: Find a better way to determine which to use.
				//If theSourceEngineModel.thePhyFileHeader.theSourcePhyPhysCollisionModels.Count < 2 Then
				//TODO: Why not use this "if" statement? It seems reasonable that a solid matches a set of convex shapes for one bone.
				//      For example, L4D2 van has several convex shapes, but only one solid and one bone.
				//      Same for w_minigun. Both use $concave.
				//If Me.theSourceEngineModel.thePhyFileHeader.solidCount = 1 Then
				if (this.thePhyFileData.theSourcePhyIsCollisionModel)
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
				this.thePhyFileData.thePhysicsMeshSmdFileName = SourceFileNamesModule.CreatePhysicsSmdFileName(this.thePhyFileData.thePhysicsMeshSmdFileName, this.theModelName);
				line += this.thePhyFileData.thePhysicsMeshSmdFileName;
				line += "\"";
				this.theOutputFileStreamWriter.WriteLine(line);
				line = "{";
				this.theOutputFileStreamWriter.WriteLine(line);

				this.WriteCollisionModelOrCollisionJointsOptions();

				line = "}";
				this.theOutputFileStreamWriter.WriteLine(line);
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
			line += this.thePhyFileData.theSourcePhyEditParamsSection.totalMass.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			this.theOutputFileStreamWriter.WriteLine(line);
			line = "\t";
			line += "$inertia ";
			line += this.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theInertia.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			this.theOutputFileStreamWriter.WriteLine(line);
			line = "\t";
			line += "$damping ";
			line += this.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			this.theOutputFileStreamWriter.WriteLine(line);
			line = "\t";
			line += "$rotdamping ";
			line += this.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			this.theOutputFileStreamWriter.WriteLine(line);
			if (!string.IsNullOrEmpty(this.thePhyFileData.theSourcePhyEditParamsSection.rootName))
			{
				line = "\t";
				line += "$rootbone \"";
				line += this.thePhyFileData.theSourcePhyEditParamsSection.rootName;
				line += "\"";
				this.theOutputFileStreamWriter.WriteLine(line);
			}
			if (this.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap != null && this.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap.Count > 0)
			{
				foreach (string jointMergeKey in this.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap.Keys)
				{
					foreach (string jointMergeValue in this.thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap[jointMergeKey])
					{
						line = "\t";
						line += "$jointmerge \"";
						line += jointMergeKey;
						line += "\" \"";
						line += jointMergeValue;
						line += "\"";
						this.theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
			if (this.thePhyFileData.theSourcePhyEditParamsSection.concave == "1")
			{
				line = "\t";
				line += "$concave";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			for (int i = 0; i < this.thePhyFileData.theSourcePhyPhysCollisionModels.Count; i++)
			{
				SourcePhyPhysCollisionModel aSourcePhysCollisionModel = this.thePhyFileData.theSourcePhyPhysCollisionModels[i];

				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				//If aSourcePhysCollisionModel.theDragCoefficientIsValid Then
				//End If

				if (aSourcePhysCollisionModel.theMassBiasIsValid)
				{
					line = "\t";
					line += "$jointmassbias \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" ";
					line += aSourcePhysCollisionModel.theMassBias.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
				}

				if (aSourcePhysCollisionModel.theDamping != this.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theDamping)
				{
					line = "\t";
					line += "$jointdamping \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" ";
					line += aSourcePhysCollisionModel.theDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
				}

				if (aSourcePhysCollisionModel.theInertia != this.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theInertia)
				{
					line = "\t";
					line += "$jointinertia \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" ";
					line += aSourcePhysCollisionModel.theInertia.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
				}

				if (aSourcePhysCollisionModel.theRotDamping != this.thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping)
				{
					line = "\t";
					line += "$jointrotdamping \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" ";
					line += aSourcePhysCollisionModel.theRotDamping.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
				}

				if (this.thePhyFileData.theSourcePhyRagdollConstraintDescs.ContainsKey(aSourcePhysCollisionModel.theIndex))
				{
					SourcePhyRagdollConstraint aConstraint = this.thePhyFileData.theSourcePhyRagdollConstraintDescs[aSourcePhysCollisionModel.theIndex];
					line = "\t";
					line += "$jointconstrain \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" x limit ";
					line += aConstraint.theXMin.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theXMax.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theXFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
					line = "\t";
					line += "$jointconstrain \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" y limit ";
					line += aConstraint.theYMin.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theYMax.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theYFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
					line = "\t";
					line += "$jointconstrain \"";
					line += aSourcePhysCollisionModel.theName;
					line += "\" z limit ";
					line += aConstraint.theZMin.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theZMax.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aConstraint.theZFriction.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}

			if (!this.thePhyFileData.theSourcePhySelfCollides)
			{
				line = "\t";
				line += "$noselfcollisions";
				this.theOutputFileStreamWriter.WriteLine(line);
			}
			else
			{
				foreach (SourcePhyCollisionPair aSourcePhyCollisionPair in this.thePhyFileData.theSourcePhyCollisionPairs)
				{
					line = "\t";
					line += "$jointcollide";
					line += " ";
					line += "\"";
					line += this.thePhyFileData.theSourcePhyPhysCollisionModels[aSourcePhyCollisionPair.obj0].theName;
					line += "\"";
					line += " ";
					line += "\"";
					line += this.thePhyFileData.theSourcePhyPhysCollisionModels[aSourcePhyCollisionPair.obj1].theName;
					line += "\"";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteCollisionTextCommand()
		{
			string line = "";

			try
			{
				if (this.thePhyFileData != null && this.thePhyFileData.theSourcePhyCollisionText != null && this.thePhyFileData.theSourcePhyCollisionText.Length > 0)
				{
					line = "";
					this.theOutputFileStreamWriter.WriteLine(line);

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$CollisionText";
					}
					else
					{
						line = "$collisiontext";
					}
					this.theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					this.theOutputFileStreamWriter.WriteLine(line);

					this.WriteTextLines(this.thePhyFileData.theSourcePhyCollisionText, 1);

					line = "}";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
			catch (Exception ex)
			{

			}
		}

		public void WriteGroupBone()
		{
			this.WriteDefineBoneCommand();
			this.WriteBoneMergeCommand();

			this.WriteProceduralBonesCommand();
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
			if (this.theMdlFileData.theBones != null)
			{
				SourceMdlBone37 aBone = null;
				string aParentBoneName = null;
				SourceVector aFixupPosition = new SourceVector();
				SourceVector aFixupRotation = new SourceVector();

				if (this.theMdlFileData.theBones.Count > 0)
				{
					this.theOutputFileStreamWriter.WriteLine();
				}

				for (int i = 0; i < this.theMdlFileData.theBones.Count; i++)
				{
					aBone = this.theMdlFileData.theBones[i];
					if (aBone.parentBoneIndex == -1)
					{
						aParentBoneName = "";
					}
					else
					{
						aParentBoneName = this.theMdlFileData.theBones[aBone.parentBoneIndex].theName;
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

					if (this.theMdlFileData.version == 2531)
					{
						line += " 0.000000 0.000000 0.000000";
					}
					else
					{
						line += " ";
						line += MathModule.RadiansToDegrees(aBone.rotation.y).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += MathModule.RadiansToDegrees(aBone.rotation.z).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += MathModule.RadiansToDegrees(aBone.rotation.x).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
					}

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

					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		private void WriteProceduralBonesCommand()
		{
			//$proceduralbones "proceduralbones.vrd"
			if (this.theMdlFileData.theProceduralBonesCommandIsUsed)
			{
				this.theOutputFileStreamWriter.WriteLine();

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
				line += SourceFileNamesModule.GetVrdFileName(this.theModelName);
				line += "\"";
				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

		private void WriteBoneMergeCommand()
		{
			string line = "";

			//$bonemerge "ValveBiped.Bip01_R_Hand"
			if (this.theMdlFileData.theBones != null)
			{
				SourceMdlBone37 aBone = null;
				bool emptyLineIsAlreadyWritten = false;

				for (int i = 0; i < this.theMdlFileData.theBones.Count; i++)
				{
					aBone = this.theMdlFileData.theBones[i];

					if ((aBone.flags & SourceMdlBone.BONE_USED_BY_BONE_MERGE) > 0)
					{
						if (!emptyLineIsAlreadyWritten)
						{
							this.theOutputFileStreamWriter.WriteLine();
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
						this.theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
		}

		public void WriteGroupBox()
		{
			this.WriteCBoxCommand();
			this.WriteBBoxCommand();
			if (this.theMdlFileData.theHitboxSets != null)
			{
				if (this.theMdlFileData.version <= 10)
				{
					bool skipBoneInBBoxCommandWasUsed = false;
					this.theOutputFileStreamWriter.WriteLine();
					this.WriteHBoxCommands(this.theMdlFileData.theHitboxSets[0].theHitboxes, "", "", ref skipBoneInBBoxCommandWasUsed);
				}
				else
				{
					this.WriteHBoxRelatedCommands();
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
			this.theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				line = "// Clipping box or view bounding box.";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			//FROM: VDC wiki: 
			//$cbox <float|minx> <float|miny> <float|minz> <float|maxx> <float|maxy> <float|maxz> 
			minX = Math.Round(this.theMdlFileData.viewBoundingBoxMinPosition.x, 3);
			minY = Math.Round(this.theMdlFileData.viewBoundingBoxMinPosition.y, 3);
			minZ = Math.Round(this.theMdlFileData.viewBoundingBoxMinPosition.z, 3);
			maxX = Math.Round(this.theMdlFileData.viewBoundingBoxMaxPosition.x, 3);
			maxY = Math.Round(this.theMdlFileData.viewBoundingBoxMaxPosition.y, 3);
			maxZ = Math.Round(this.theMdlFileData.viewBoundingBoxMaxPosition.z, 3);
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
			this.theOutputFileStreamWriter.WriteLine(line);
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
			this.theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				line = "// Bounding box or hull. Used for collision with a world object.";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			//$bbox -16.0 -16.0 -13.0 16.0 16.0 75.0
			//FROM: VDC wiki: 
			//$bbox (min x) (min y) (min z) (max x) (max y) (max z)
			minX = Math.Round(this.theMdlFileData.hullMinPosition.x, 3);
			minY = Math.Round(this.theMdlFileData.hullMinPosition.y, 3);
			minZ = Math.Round(this.theMdlFileData.hullMinPosition.z, 3);
			maxX = Math.Round(this.theMdlFileData.hullMaxPosition.x, 3);
			maxY = Math.Round(this.theMdlFileData.hullMaxPosition.y, 3);
			maxZ = Math.Round(this.theMdlFileData.hullMaxPosition.z, 3);
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
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		private void WriteHBoxRelatedCommands()
		{
			string line = "";
			string commentTag = "";
			bool hitBoxWasAutoGenerated = false;
			bool skipBoneInBBoxCommandWasUsed = false;

			if (this.theMdlFileData.theHitboxSets.Count < 1)
			{
				return;
			}

			hitBoxWasAutoGenerated = (this.theMdlFileData.flags & SourceMdlFileData.STUDIOHDR_FLAGS_AUTOGENERATED_HITBOX) > 0;
			if (hitBoxWasAutoGenerated && !MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				return;
			}

			this.theOutputFileStreamWriter.WriteLine();

			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				line = "// Hitbox info. Used for damage-based collision.";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			if (hitBoxWasAutoGenerated)
			{
				line = "// The hitbox info below was automatically generated when compiled because no hitbox info was provided.";
				this.theOutputFileStreamWriter.WriteLine(line);

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

			SourceMdlHitboxSet37 aHitboxSet = null;
			for (int i = 0; i < this.theMdlFileData.theHitboxSets.Count; i++)
			{
				aHitboxSet = this.theMdlFileData.theHitboxSets[i];

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
				this.theOutputFileStreamWriter.WriteLine(commentTag + line);

				if (aHitboxSet.theHitboxes == null)
				{
					continue;
				}

				this.WriteHBoxCommands(aHitboxSet.theHitboxes, commentTag, aHitboxSet.theName, ref skipBoneInBBoxCommandWasUsed);
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
				this.theOutputFileStreamWriter.WriteLine(commentTag + line);
			}
		}

		private void WriteHBoxCommands(List<SourceMdlHitbox37> theHitboxes, string commentTag, string hitboxSetName, ref bool theSkipBoneInBBoxCommandWasUsed)
		{
			string line = "";
			SourceMdlHitbox37 aHitbox = null;

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
				line += this.theMdlFileData.theBones[aHitbox.boneIndex].theName;
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
				line += " ";
				line += "\"";
				line += aHitbox.theName;
				line += "\"";
				this.theOutputFileStreamWriter.WriteLine(commentTag + line);

				if (!theSkipBoneInBBoxCommandWasUsed)
				{
					if (aHitbox.boundingBoxMin.x > 0 || aHitbox.boundingBoxMin.y > 0 || aHitbox.boundingBoxMin.z > 0 || aHitbox.boundingBoxMax.x < 0 || aHitbox.boundingBoxMax.y < 0 || aHitbox.boundingBoxMax.z < 0)
					{
						theSkipBoneInBBoxCommandWasUsed = true;
					}
				}
			}
		}

		public void WriteBodyGroupCommand(int startIndex)
		{
			string line = "";
			SourceMdlBodyPart37 aBodyPart = null;
			SourceVtxBodyPart06 aVtxBodyPart = null;
			SourceMdlModel37 aBodyModel = null;
			SourceVtxModel06 aVtxModel = null;

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
			if (this.theMdlFileData.theBodyParts != null && this.theMdlFileData.theBodyParts.Count > startIndex)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int bodyPartIndex = startIndex; bodyPartIndex < this.theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = this.theMdlFileData.theBodyParts[bodyPartIndex];
					aVtxBodyPart = this.theVtxFileData.theVtxBodyParts[bodyPartIndex];

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
					this.theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					this.theOutputFileStreamWriter.WriteLine(line);

					if (aBodyPart.theModels != null && aBodyPart.theModels.Count > 0)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theModels.Count; modelIndex++)
						{
							aBodyModel = aBodyPart.theModels[modelIndex];
							aVtxModel = aVtxBodyPart.theVtxModels[modelIndex];

							line = "\t";
							//If aModel.name(0) = ChrW(0) Then
							if (aBodyModel.name[0] == '\0' && aVtxModel.theVtxModelLods[0].theVtxMeshes == null)
							{
								line += "blank";
							}
							else
							{
								aBodyModel.theSmdFileNames[0] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[0], bodyPartIndex, modelIndex, 0, this.theModelName, new string(this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].name));
								line += "studio ";
								line += "\"";
								line += aBodyModel.theSmdFileNames[0];
								line += "\"";
							}
							this.theOutputFileStreamWriter.WriteLine(line);
						}
					}

					line = "}";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteControllerCommand()
		{
			string line = "";
			SourceMdlBoneController37 boneController = null;

			//$controller mouth "jaw" X 0 20
			//$controller 0 "tracker" LYR -1 1
			try
			{
				if (this.theMdlFileData.theBoneControllers != null)
				{
					if (this.theMdlFileData.theBoneControllers.Count > 0)
					{
						this.theOutputFileStreamWriter.WriteLine();
					}

					for (int i = 0; i < this.theMdlFileData.theBoneControllers.Count; i++)
					{
						boneController = this.theMdlFileData.theBoneControllers[i];

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
						line += this.theMdlFileData.theBones[boneController.boneIndex].theName;
						line += "\" ";
						line += boneController.TypeName;
						line += " ";
						line += boneController.startBlah.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += boneController.endBlah.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						this.theOutputFileStreamWriter.WriteLine(line);
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
				if (this.theMdlFileData.theBones != null)
				{
					SourceMdlBone37 aBone = null;
					bool emptyLineIsAlreadyWritten = false;

					for (int i = 0; i < this.theMdlFileData.theBones.Count; i++)
					{
						aBone = this.theMdlFileData.theBones[i];

						if ((aBone.flags & SourceMdlBone.BONE_SCREEN_ALIGN_SPHERE) > 0)
						{
							if (!emptyLineIsAlreadyWritten)
							{
								this.theOutputFileStreamWriter.WriteLine();
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
							this.theOutputFileStreamWriter.WriteLine(line);
						}
						else if ((aBone.flags & SourceMdlBone.BONE_SCREEN_ALIGN_CYLINDER) > 0)
						{
							if (!emptyLineIsAlreadyWritten)
							{
								this.theOutputFileStreamWriter.WriteLine();
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
							this.theOutputFileStreamWriter.WriteLine(line);
						}
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		public void WriteKeyValues(string keyValuesText, string commandOrOptionText)
		{
			string line = "";
			string startText = "mdlkeyvalue" + "\n";
			string text = null;

			//$keyvalues
			//{
			//	"particles"
			//	{
			//		"effect"
			//		{
			//		name("sparks_head")
			//		attachment_type("follow_attachment")
			//		attachment_point("Head_sparks")
			//		}
			//		"effect"
			//		{
			//		name("sparks_head_wire1")
			//		attachment_type("follow_attachment")
			//		attachment_point("Head_Wire_1")
			//		}
			//		"effect"
			//		{
			//		name("sparks_knee_wire1")
			//		attachment_type("follow_attachment")
			//		attachment_point("R_Knee_Wire_1")
			//		}
			//		"effect"
			//		{
			//		name("sparks_knee_wire2")
			//		attachment_type("follow_attachment")
			//		attachment_point("R_Knee_Wire_2")
			//		}
			//		"effect"
			//		{
			//		name("sparks_ankle_wire1")
			//		attachment_type("follow_attachment")
			//		attachment_point("L_Ankle_Wire_1")
			//		}
			//		"effect"
			//		{
			//		name("sparks_ankle_wire2")
			//		attachment_type("follow_attachment")
			//		attachment_point("L_Ankle_Wire_2")
			//		}			
			//	}
			//}
			try
			{
				if (keyValuesText != null && keyValuesText.Length > 0)
				{
					line = "";
					this.theOutputFileStreamWriter.WriteLine(line);

					line = commandOrOptionText;
					this.theOutputFileStreamWriter.WriteLine(line);

					if (keyValuesText.StartsWith(startText))
					{
						text = keyValuesText.Remove(0, startText.Length);
					}
					else
					{
						text = keyValuesText;
					}

					//lengthToRemove = 0
					//While True
					//	stopIndex = text.IndexOf(openBraceText)
					//	If stopIndex > -1 Then
					//		If stopIndex > 0 Then
					//			line = text.Substring(0, stopIndex)
					//			Me.theOutputFileStreamWriter.WriteLine(line)
					//		End If

					//		line = "{"
					//		lengthToRemove = stopIndex + openBraceText.Length
					//	Else
					//		stopIndex = text.IndexOf(closeBraceText)
					//		If stopIndex > -1 Then
					//			If stopIndex > 0 Then
					//				line = text.Substring(0, stopIndex)
					//				Me.theOutputFileStreamWriter.WriteLine(line)
					//			End If

					//			line = "}"
					//			lengthToRemove = stopIndex + closeBraceText.Length
					//		Else
					//			stopIndex = text.IndexOf(linefeedCharText)
					//			If stopIndex > -1 Then
					//				line = text.Substring(0, stopIndex)
					//				lengthToRemove = stopIndex + linefeedCharText.Length
					//			Else
					//				line = text
					//			End If
					//		End If
					//	End If
					//	Me.theOutputFileStreamWriter.WriteLine(line)

					//	If stopIndex > -1 Then
					//		text = text.Remove(0, lengthToRemove)
					//		If text = "" Then
					//			Exit While
					//		End If
					//	End If
					//End While

					this.WriteTextLines(text, 0);
				}
			}
			catch (Exception ex)
			{

			}
		}

		private void WriteTextLines(string text, int indentCount)
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
						this.theOutputFileStreamWriter.WriteLine(line);
					}

					line = indentText;
					line += "{";
					this.theOutputFileStreamWriter.WriteLine(line);

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
							this.theOutputFileStreamWriter.WriteLine(line);
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
					this.theOutputFileStreamWriter.WriteLine(line);

					startIndex = i + 1;
					lineQuoteCount = 0;
				}
				else if (textChar == '\"')
				{
					lineQuoteCount += 1;
					if (lineQuoteCount == 4)
					{
						if (i > startIndex)
						{
							line = indentText;
							line += text.Substring(startIndex, i - startIndex + 1).Trim();
							this.theOutputFileStreamWriter.WriteLine(line);
						}
						startIndex = i + 1;
						lineQuoteCount = 0;
					}
					//If lineQuoteCount = 2 OrElse lineQuoteCount = 4 Then
					//	lineWordCount += 1
					//End If
				}
				else if (textChar == "\n"[0])
				{
					startIndex = i + 1;
					lineQuoteCount = 0;
				}
			}
		}

#endregion

#region Data

		//Private theModel As SourceModel
		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData37 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		private SourceVtxFileData06 theVtxFileData;
		private string theModelName;

		private string theOutputPath;
		private string theOutputFileNameWithoutExtension;

#endregion

	}

}