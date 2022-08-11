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
	public class SourceQcFile2531 : SourceQcFile
	{
#region Creation and Destruction

		public SourceQcFile2531(StreamWriter outputFileStream, string outputPathFileName, SourceMdlFileData2531 mdlFileData, SourceVtxFileData107 vtxFileData, SourcePhyFileData phyFileData, string modelName)
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

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(theOutputFileStreamWriter);
		}

		public void WriteAttachmentCommand()
		{
			string line = "";

			if (theMdlFileData.theAttachments != null)
			{
				try
				{
					line = "";
					theOutputFileStreamWriter.WriteLine(line);

					SourceMdlAttachment2531 anAttachment = null;
					for (int i = 0; i < theMdlFileData.theAttachments.Count; i++)
					{
						anAttachment = theMdlFileData.theAttachments[i];

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
						line += theMdlFileData.theBones[anAttachment.boneIndex].theName;
						line += "\"";
						line += " ";

						line += anAttachment.posX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += anAttachment.posY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += anAttachment.posZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

						theOutputFileStreamWriter.WriteLine(line);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
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

		public void WriteBodyGroupCommand()
		{
			string line = "";
			SourceMdlBodyPart2531 aBodyPart = null;
			SourceMdlModel2531 aBodyModel = null;

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
								aBodyModel.theSmdFileNames[0] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[0], bodyPartIndex, modelIndex, 0, theModelName, theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].theName);
								line += "studio ";
								line += "\"";
								line += FileManager.GetPathFileNameWithoutExtension(aBodyModel.theSmdFileNames[0]);
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

		public void WriteCdMaterialsCommand()
		{
			string line = "";

			if (theMdlFileData.theTexturePaths != null)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.theTexturePaths.Count; i++)
				{
					string aTexturePath = theMdlFileData.theTexturePaths[i];
					//NOTE: Write out null or empty strings, because Crowbar should show what was stored.
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

		public void WriteControllerCommand()
		{
			//$controller mouth "jaw" X 0 20
			//$controller 0 "tracker" LYR -1 1
			if (theMdlFileData.theBoneControllers != null)
			{
				try
				{
					if (theMdlFileData.theBoneControllers.Count > 0)
					{
						theOutputFileStreamWriter.WriteLine();
					}

					SourceMdlBoneController2531 boneController = null;
					string line = "";
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
						if (boneController.inputField == 4)
						{
							line += "Mouth";
						}
						else
						{
							line += boneController.inputField.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
						}
						line += " \"";
						line += theMdlFileData.theBones[boneController.boneIndex].theName;
						line += "\" ";
						line += SourceModule2531.GetControlText(boneController.type);
						line += " ";
						line += boneController.startAngleDegrees.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += boneController.endAngleDegrees.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						theOutputFileStreamWriter.WriteLine(line);
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
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

		//Public Sub WriteFlagsCommand()
		//	Dim line As String = ""

		//	Me.theOutputFileStreamWriter.WriteLine()

		//If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
		//	line = "$Flags "
		//Else
		//	line = "$flags "
		//End If
		//	line += Me.theMdlFileData.flags.ToString(TheApp.InternalNumberFormat)
		//	Me.theOutputFileStreamWriter.WriteLine(line)
		//End Sub

		public void WriteHBoxRelatedCommands()
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

			SourceMdlHitboxSet2531 aHitboxSet = null;
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
		}

		private void WriteHBoxCommands(List<SourceMdlHitbox2531> theHitboxes, string commentTag, string hitboxSetName, ref bool theSkipBoneInBBoxCommandWasUsed)
		{
			string line = "";
			SourceMdlHitbox2531 aHitbox = null;

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

		public void WriteIncludeModelCommands()
		{
			string line = "";

			if (theMdlFileData.theIncludeModels != null)
			{
				line = "";
				theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < theMdlFileData.theIncludeModels.Count; i++)
				{
					SourceMdlIncludeModel2531 anIncludeModel = theMdlFileData.theIncludeModels[i];

					if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
					{
						line = "$IncludeModel ";
					}
					else
					{
						line = "$includemodel ";
					}
					line += "\"";
					if (anIncludeModel.theFileName.StartsWith("models/"))
					{
						line += anIncludeModel.theFileName.Substring(7);
					}
					else
					{
						line += anIncludeModel.theFileName;
					}
					line += "\"";
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteLodCommand()
		{
			string line = "";

			//NOTE: Data is from VTX file.
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

				SourceVtxBodyPart107 aBodyPart = null;
				SourceVtxModel107 aVtxModel = null;
				SourceMdlModel2531 aBodyModel = null;
				LodQcInfo aLodQcInfo = null;
				List<LodQcInfo> aLodQcInfoList = null;
				SortedList<double, List<LodQcInfo>> aLodList = null;
				double switchPoint = 0;

				aLodList = new SortedList<double, List<LodQcInfo>>();
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
								//If aModel.name(0) = ChrW(0) Then
								//	Continue For
								//End If

								//NOTE: Start loop at 1 to skip first LOD, which isn't needed for the $lod command.
								for (int lodIndex = 1; lodIndex < theVtxFileData.lodCount; lodIndex++)
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
					theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					theOutputFileStreamWriter.WriteLine(line);

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
						theOutputFileStreamWriter.WriteLine(line);
					}

					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
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
					theOutputFileStreamWriter.WriteLine(line);

					line = "{";
					theOutputFileStreamWriter.WriteLine(line);

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
						theOutputFileStreamWriter.WriteLine(line);
					}

					line = "}";
					theOutputFileStreamWriter.WriteLine(line);
				}
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

		//Public Sub WriteSequenceGroupCommands()
		//	Dim line As String = ""
		//	Dim aSequenceGroup As SourceMdlSequenceGroup2531

		//	If Me.theMdlFileData.theSequenceGroups IsNot Nothing AndAlso Me.theMdlFileData.theSequenceGroups.Count > 1 Then
		//		Me.theOutputFileStreamWriter.WriteLine()

		//		For sequenceGroupIndex As Integer = 0 To Me.theMdlFileData.theSequenceGroups.Count - 1
		//			aSequenceGroup = Me.theMdlFileData.theSequenceGroups(sequenceGroupIndex)

		//			If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
		//				line = "$SequenceGroup "
		//			Else
		//				line = "$sequencegroup "
		//			End If
		//			line += """"
		//			line += aSequenceGroup.theName
		//			line += """"

		//			Me.theOutputFileStreamWriter.WriteLine(line)
		//		Next
		//	End If
		//End Sub

		public void WriteSequenceCommands()
		{
			string line = "";
			SourceMdlSequenceDesc2531 aSequence = null;

			if (theMdlFileData.theSequences != null && theMdlFileData.theSequences.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine();

				for (int sequenceIndex = 0; sequenceIndex < theMdlFileData.theSequences.Count; sequenceIndex++)
				{
					aSequence = theMdlFileData.theSequences[sequenceIndex];

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
						SourceMdlTexture2531 aTexture = theMdlFileData.theTextures[aSkinFamily[j]];

						textureFileNames.Add(aTexture.theFileName);
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

		public void WriteCollisionModelOrCollisionJointsCommand()
		{
			string line = "";

			//NOTE: Data is from PHY file.
			if (thePhyFileData != null && thePhyFileData.solidCount > 0)
			{
				theOutputFileStreamWriter.WriteLine(line);

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
					if (lineQuoteCount == 4)
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

#region Private Delegates

#endregion

#region Private Methods

		private void WriteSequenceOptions(SourceMdlSequenceDesc2531 aSequenceDesc)
		{
			string line = "";
			int anAnimDescIndex = 0;
			SourceMdlAnimationDesc2531 anAnimationDesc = null;

			for (int blendIndex = 0; blendIndex < aSequenceDesc.blendCount; blendIndex++)
			{
				anAnimDescIndex = aSequenceDesc.anim[blendIndex][0];
				if (anAnimDescIndex >= theMdlFileData.theAnimationDescs.Count)
				{
					anAnimDescIndex = theMdlFileData.theAnimationDescs.Count - 1;
				}
				anAnimationDesc = theMdlFileData.theAnimationDescs[anAnimDescIndex];

				anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, theModelName, anAnimationDesc.theName);
				line = "\t";
				line += "\"";
				line += anAnimationDesc.theSmdRelativePathFileName;
				line += "\"";
				theOutputFileStreamWriter.WriteLine(line);
			}

			//If aSequenceDesc.activityId > 0 Then
			//	line = vbTab
			//	line += SourceModule2531.activityMap(aSequenceDesc.activityId)
			//	line += " "
			//	line += aSequenceDesc.activityWeight.ToString(TheApp.InternalNumberFormat)
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If
			if (!string.IsNullOrEmpty(aSequenceDesc.theActivityName))
			{
				line = "\t";
				line += "activity ";
				line += "\"";
				line += aSequenceDesc.theActivityName;
				line += "\"";
				line += " ";
				line += aSequenceDesc.activityWeight.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			//For i As Integer = 0 To 1
			//	If aSequenceDesc.blendType(i) <> 0 Then
			//		line = vbTab
			//		line += "blend "
			//		line += """"
			//		line += SourceModule2531.GetControlText(aSequenceDesc.blendType(i))
			//		line += """"
			//		line += " "
			//		line += aSequenceDesc.blendStart(i).ToString("0.######", TheApp.InternalNumberFormat)
			//		line += " "
			//		line += aSequenceDesc.blendEnd(i).ToString("0.######", TheApp.InternalNumberFormat)
			//		Me.theOutputFileStreamWriter.WriteLine(line)
			//	End If
			//Next

			//If aSequenceDesc.theEvents IsNot Nothing Then
			//	Dim frameIndex As Integer
			//	For j As Integer = 0 To aSequenceDesc.theEvents.Count - 1
			//		If aSequenceDesc.frameCount <= 1 Then
			//			frameIndex = 0
			//		Else
			//			frameIndex = aSequenceDesc.theEvents(j).frameIndex
			//		End If
			//		line = vbTab
			//		line += "{ "
			//		line += "event "
			//		line += aSequenceDesc.theEvents(j).eventIndex.ToString(TheApp.InternalNumberFormat)
			//		line += " "
			//		line += frameIndex.ToString(TheApp.InternalNumberFormat)
			//		If aSequenceDesc.theEvents(j).theOptions <> "" Then
			//			line += " """
			//			line += aSequenceDesc.theEvents(j).theOptions
			//			line += """"
			//		End If
			//		line += " }"
			//		Me.theOutputFileStreamWriter.WriteLine(line)
			//	Next
			//End If

			line = "\t";
			line += "fps ";
			//line += Me.theMdlFileData.theAnimationDescs(aSequenceDesc.theAnimDescIndexes(0)).fps.ToString("0.######", TheApp.InternalNumberFormat)
			//NOTE: Not sure why VtMB model "character/monster/manbat/Throw_Objects/ThrowTaxi.mdl" has aSequenceDesc.anim(0) = 1 when there is only 1 animDesc.
			//      So, use this "if" block to handle the situation.
			anAnimDescIndex = aSequenceDesc.anim[0][0];
			if (anAnimDescIndex >= theMdlFileData.theAnimationDescs.Count)
			{
				anAnimDescIndex = theMdlFileData.theAnimationDescs.Count - 1;
			}
			line += theMdlFileData.theAnimationDescs[anAnimDescIndex].fps.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			theOutputFileStreamWriter.WriteLine(line);

			//If (aSequenceDesc.flags And SourceMdlSequenceDesc2531.STUDIO_LOOPING) > 0 Then
			//	line = vbTab
			//	line += "loop"
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If

			//If aSequenceDesc.motiontype > 0 Then
			//	line = vbTab
			//	line += SourceModule2531.GetMultipleControlText(aSequenceDesc.motiontype)
			//	Me.theOutputFileStreamWriter.WriteLine(line)
			//End If
		}

#endregion

#region Constants

#endregion

#region Data
		private SourceMdlFileData2531 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		private SourceVtxFileData107 theVtxFileData;
		private string theModelName;

		private string theOutputPath;
		private string theOutputFileNameWithoutExtension;
#endregion

	}

}