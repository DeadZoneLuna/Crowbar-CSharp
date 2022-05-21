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
	public class SourceQcFile31 : SourceQcFile
	{
#region Creation and Destruction

		//Public Sub New(ByVal outputFileStream As StreamWriter, ByVal outputPathFileName As String, ByVal mdlFileData As SourceMdlFileData31, ByVal phyFileData As SourcePhyFileData37, ByVal vtxFileData As SourceVtxFileData06, ByVal modelName As String)
		public SourceQcFile31(StreamWriter outputFileStream, string outputPathFileName, SourceMdlFileData31 mdlFileData, SourceVtxFileData06 vtxFileData, SourcePhyFileData phyFileData, string modelName)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			thePhyFileData = phyFileData;
			theVtxFileData = vtxFileData;
			theModelName = modelName;

			theOutputPath = FileManager.GetPath(outputPathFileName);
			theOutputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPathFileName);
		}

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
			//Dim modelPath As String
			string modelPathFileName;

			//modelPath = FileManager.GetPath(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
			//modelPathFileName = Path.Combine(modelPath, theSourceEngineModel.ModelName + ".mdl")
			//modelPathFileName = CStr(theSourceEngineModel.MdlFileHeader.name).Trim(Chr(0))
			modelPathFileName = theMdlFileData.theModelName;

			theOutputFileStreamWriter.WriteLine();

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
			theOutputFileStreamWriter.WriteLine(line);
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

		public void WriteBodyGroupCommand()
		{
			string line = "";
			SourceMdlBodyPart31 aBodyPart = null;
			SourceVtxBodyPart06 aVtxBodyPart = null;
			SourceMdlModel31 aBodyModel = null;
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

		public void WriteGroupLod()
		{
			WriteLodCommand();
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

		public void WriteIllumPositionCommand()
		{
			string line = null;
			double offsetX = 0;
			double offsetY = 0;
			double offsetZ = 0;

			offsetX = Math.Round(theMdlFileData.illuminationPosition.y, 3);
			offsetY = -Math.Round(theMdlFileData.illuminationPosition.x, 3);
			offsetZ = Math.Round(theMdlFileData.illuminationPosition.z, 3);

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
			theOutputFileStreamWriter.WriteLine(line);
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
			//NOTE: Probably stored in different order in MDL file, based on MDL v48 source code.
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

		public void WriteCdMaterialsCommand()
		{
			string line = "";

			//$cdmaterials "models\survivors\producer\"
			//$cdmaterials "models\survivors\"
			//$cdmaterials ""
			if (theMdlFileData.theTexturePaths != null)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.theTexturePaths.Count; i++)
				{
					string aTexturePath = theMdlFileData.theTexturePaths[i];
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
					theOutputFileStreamWriter.WriteLine(line);
					//End If
				}
			}
		}

		public void WriteTextureGroupCommand()
		{
			string line = "";

			if (theMdlFileData.theSkinFamilies != null && theMdlFileData.theSkinFamilies.Count > 0 && theMdlFileData.theTextures != null && theMdlFileData.theTextures.Count > 0 && theMdlFileData.skinReferenceCount > 0)
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

					List<string> textureFileNames = new List<string>(skinReferenceCount);
					for (int j = 0; j < skinReferenceCount; j++)
					{
						SourceMdlTexture31 aTexture = theMdlFileData.theTextures[aSkinFamily[j]];

						textureFileNames.Add(aTexture.thePathFileName);
					}

					skinFamiliesOfTextureFileNames.Add(textureFileNames);
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
			if (theMdlFileData.theTextures != null)
			{
				string line = "";

				theOutputFileStreamWriter.WriteLine(line);

				line = "// This list shows the VMT file names used in the SMD files.";
				theOutputFileStreamWriter.WriteLine(line);

				for (int j = 0; j < theMdlFileData.theTextures.Count; j++)
				{
					SourceMdlTexture31 aTexture = theMdlFileData.theTextures[j];
					line = "// \"";
					line += aTexture.thePathFileName;
					line += ".vmt\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteGroupBox()
		{
			WriteCBoxCommand();
			WriteBBoxCommand();
			if (theMdlFileData.version >= 27 && theMdlFileData.version <= 30)
			{
				WriteHBoxRelatedCommands_MDL27to30();
			}
			else
			{
				WriteHBoxRelatedCommands();
			}
		}

		public void WriteGroupAnimation()
		{
			//NOTE: Must write $animation lines before $sequence lines that use them.
			try
			{
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
		}

		public void WriteGroupCollision()
		{
			WriteCollisionModelOrCollisionJointsCommand();
			//Me.WriteCollisionTextCommand()
		}

#endregion

#region Private Delegates

		public delegate void WriteGroupDelegate();

#endregion

#region Private Methods

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
				if (theVtxFileData.lodCount <= 1)
				{
					return;
				}

				SourceVtxBodyPart06 aBodyPart = null;
				SourceVtxModel06 aVtxModel = null;
				SourceMdlModel31 aBodyModel = null;
				int lodIndex = 0;
				LodQcInfo aLodQcInfo = null;
				List<LodQcInfo> aLodQcInfoList = null;
				SortedList<double, List<LodQcInfo>> aLodList = null;
				SortedList<double, bool> aLodListOfFacialFlags = null;
				double switchPoint = 0;

				aLodList = new SortedList<double, List<LodQcInfo>>();
				aLodListOfFacialFlags = new SortedList<double, bool>();
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

			foreach (SourceMdlBone31 aBone in theMdlFileData.theBones)
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

		private void WriteHBoxRelatedCommands_MDL27to30()
		{
			string line = "";
			string commentTag = "";
			bool hitBoxWasAutoGenerated = false;
			bool skipBoneInBBoxCommandWasUsed = false;

			if (theMdlFileData.theHitboxes_MDL27to30 == null || theMdlFileData.theHitboxes_MDL27to30.Count < 1)
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

			WriteHBoxCommands_MDL27to30(theMdlFileData.theHitboxes_MDL27to30, commentTag, ref skipBoneInBBoxCommandWasUsed);

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

		private void WriteHBoxCommands_MDL27to30(List<SourceMdlHitbox31> theHitboxes, string commentTag, ref bool theSkipBoneInBBoxCommandWasUsed)
		{
			string line = "";
			SourceMdlHitbox31 aHitbox = null;

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

		private void WriteHBoxRelatedCommands()
		{
			string line = "";
			string commentTag = "";
			bool hitBoxWasAutoGenerated = false;
			bool skipBoneInBBoxCommandWasUsed = false;

			if (theMdlFileData.theHitboxSets == null || theMdlFileData.theHitboxSets.Count < 1)
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

			SourceMdlHitboxSet31 aHitboxSet = null;
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

		private void WriteHBoxCommands(List<SourceMdlHitbox31> theHitboxes, string commentTag, string hitboxSetName, ref bool theSkipBoneInBBoxCommandWasUsed)
		{
			string line = "";
			SourceMdlHitbox31 aHitbox = null;

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

		private void WriteAnimationOrDeclareAnimationCommand()
		{
			if (theMdlFileData.theAnimationDescs != null)
			{
				for (int i = 0; i < theMdlFileData.theAnimationDescs.Count; i++)
				{
					SourceMdlAnimationDesc31 anAnimationDesc = theMdlFileData.theAnimationDescs[i];

					if (anAnimationDesc.theName[0] != '@')
					{
						WriteAnimationLine(anAnimationDesc);
					}
				}
			}
		}

		private void WriteAnimationLine(SourceMdlAnimationDesc31 anAnimationDesc)
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
		private void WriteAnimationOptions(SourceMdlSequenceDesc31 aSequenceDesc, SourceMdlAnimationDesc31 anAnimationDesc, SourceMdlAnimationDesc31 impliedAnimDesc)
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

			//Me.WriteCmdListOptions(aSequenceDesc, anAnimationDesc, impliedAnimDesc)
		}

		private void WriteSequenceOrDeclareSequenceCommand()
		{
			//$sequence producer "producer" fps 30.00
			//$sequence ragdoll "ragdoll" ACT_DIERAGDOLL 1 fps 30.00
			if (theMdlFileData.theSequenceDescs != null)
			{
				for (int i = 0; i < theMdlFileData.theSequenceDescs.Count; i++)
				{
					SourceMdlSequenceDesc31 aSequenceDesc = theMdlFileData.theSequenceDescs[i];

					//Me.WriteSequenceLine(aSequenceDesc)
				}
			}
		}

		//Private Sub WriteSequenceLine(ByVal aSequenceDesc As SourceMdlSequenceDesc31)
		//	Dim line As String = ""

		//	Me.theOutputFileStreamWriter.WriteLine()

		//	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_OVERRIDE) > 0 Then
		//		If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
		//			line = "$DeclareSequence"
		//		Else
		//			line = "$declaresequence"
		//		End If
		//		line += " """
		//		line += aSequenceDesc.theName
		//		line += """"
		//		Me.theOutputFileStreamWriter.WriteLine(line)
		//	Else
		//		If aSequenceDesc.theAnimDescIndexes IsNot Nothing OrElse aSequenceDesc.theAnimDescIndexes.Count > 0 Then
		//			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
		//				line = "$Sequence "
		//			Else
		//				line = "$sequence "
		//			End If
		//			line += """"
		//			line += aSequenceDesc.theName
		//			line += """"
		//			'NOTE: Opening brace must be on same line as the command.
		//			line += " {"
		//			Me.theOutputFileStreamWriter.WriteLine(line)

		//			Try
		//				Me.WriteSequenceOptions(aSequenceDesc)
		//			Catch ex As Exception
		//				Dim debug As Integer = 4242
		//			End Try

		//			line = "}"
		//			Me.theOutputFileStreamWriter.WriteLine(line)
		//		End If
		//	End If
		//End Sub

		//Private Sub WriteSequenceOptions(ByVal aSequenceDesc As SourceMdlSequenceDesc37)
		//	Dim line As String = ""
		//	Dim valueString As String
		//	Dim impliedAnimDesc As SourceMdlAnimationDesc37 = Nothing

		//	Dim anAnimationDesc As SourceMdlAnimationDesc37
		//	Dim name As String
		//	For j As Integer = 0 To aSequenceDesc.theAnimDescIndexes.Count - 1
		//		anAnimationDesc = Me.theMdlFileData.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(j))
		//		name = anAnimationDesc.theName

		//		line = vbTab
		//		line += """"
		//		If name(0) = "@" Then
		//			'NOTE: There should only be one implied anim desc.
		//			impliedAnimDesc = anAnimationDesc
		//			anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, Me.theModelName, anAnimationDesc.theName)
		//			line += anAnimationDesc.theSmdRelativePathFileName
		//		Else
		//			If Not name.StartsWith("a_") Then
		//				line += "a_"
		//			End If
		//			line += name
		//		End If
		//		line += """"
		//		Me.theOutputFileStreamWriter.WriteLine(line)
		//	Next

		//	If aSequenceDesc.theActivityName <> "" Then
		//		line = vbTab
		//		line += "activity "
		//		line += """"
		//		line += aSequenceDesc.theActivityName
		//		line += """ "
		//		line += aSequenceDesc.activityWeight.ToString(TheApp.InternalNumberFormat)
		//		Me.theOutputFileStreamWriter.WriteLine(line)
		//	End If

		//	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_AUTOPLAY) > 0 Then
		//		line = vbTab
		//		line += "autoplay"
		//		Me.theOutputFileStreamWriter.WriteLine(line)
		//	End If

		//	If aSequenceDesc.theEvents IsNot Nothing Then
		//		Dim frameIndex As Integer
		//		Dim frameCount As Integer
		//		frameCount = Me.theMdlFileData.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(0)).frameCount
		//		For j As Integer = 0 To aSequenceDesc.theEvents.Count - 1
		//			If frameCount <= 1 Then
		//				frameIndex = 0
		//			Else
		//				frameIndex = CInt(aSequenceDesc.theEvents(j).cycle * (frameCount - 1))
		//			End If
		//			line = vbTab
		//			line += "{ "
		//			line += "event "
		//			line += aSequenceDesc.theEvents(j).eventIndex.ToString(TheApp.InternalNumberFormat)
		//			line += " "
		//			line += frameIndex.ToString(TheApp.InternalNumberFormat)
		//			If aSequenceDesc.theEvents(j).options <> "" Then
		//				line += " """
		//				line += CStr(aSequenceDesc.theEvents(j).options).Trim(Chr(0))
		//				line += """"
		//			End If
		//			line += " }"
		//			Me.theOutputFileStreamWriter.WriteLine(line)
		//		Next
		//	End If

		//	valueString = aSequenceDesc.fadeInTime.ToString("0.######", TheApp.InternalNumberFormat)
		//	line = vbTab
		//	line += "fadein "
		//	line += valueString
		//	Me.theOutputFileStreamWriter.WriteLine(line)

		//	valueString = aSequenceDesc.fadeOutTime.ToString("0.######", TheApp.InternalNumberFormat)
		//	line = vbTab
		//	line += "fadeout "
		//	line += valueString
		//	Me.theOutputFileStreamWriter.WriteLine(line)

		//	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_HIDDEN) > 0 Then
		//		line = vbTab
		//		line += "hidden"
		//		Me.theOutputFileStreamWriter.WriteLine(line)
		//	End If

		//	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_SNAP) > 0 Then
		//		line = vbTab
		//		line += "snap"
		//		Me.theOutputFileStreamWriter.WriteLine(line)
		//	End If

		//	If (aSequenceDesc.flags And SourceMdlAnimationDesc.STUDIO_WORLD) > 0 Then
		//		line = vbTab
		//		line += "worldspace"
		//		Me.theOutputFileStreamWriter.WriteLine(line)
		//	End If

		//	Me.WriteAnimationOptions(aSequenceDesc, firstAnimDesc, impliedAnimDesc)
		//End Sub

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

				//NOTE: The smd file name for $collisionjoints is not stored in the mdl file.
				//TODO: Find a better way to determine which to use.
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
			if (thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap != null && thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap.Count > 0)
			{
				foreach (string jointMergeKey in thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap.Keys)
				{
					foreach (string jointMergeValue in thePhyFileData.theSourcePhyEditParamsSection.jointMergeMap[jointMergeKey])
					{
						line = "\t";
						line += "$jointmerge \"";
						line += jointMergeKey;
						line += "\" \"";
						line += jointMergeValue;
						line += "\"";
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
			}
			if (thePhyFileData.theSourcePhyEditParamsSection.concave == "1")
			{
				line = "\t";
				line += "$concave";
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
			else
			{
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

#endregion

#region Data

		//Private theModel As SourceModel
		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData31 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		private SourceVtxFileData06 theVtxFileData;
		private string theModelName;

		private string theOutputPath;
		private string theOutputFileNameWithoutExtension;

#endregion

	}

}