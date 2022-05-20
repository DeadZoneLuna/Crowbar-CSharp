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
	public class SourceQcFile2531 : SourceQcFile
	{
#region Creation and Destruction

		public SourceQcFile2531(StreamWriter outputFileStream, string outputPathFileName, SourceMdlFileData2531 mdlFileData, SourceVtxFileData107 vtxFileData, SourcePhyFileData phyFileData, string modelName)
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

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(this.theOutputFileStreamWriter);
		}

		public void WriteAttachmentCommand()
		{
			string line = "";

			if (this.theMdlFileData.theAttachments != null)
			{
				try
				{
					line = "";
					this.theOutputFileStreamWriter.WriteLine(line);

					SourceMdlAttachment2531 anAttachment = null;
					for (int i = 0; i < this.theMdlFileData.theAttachments.Count; i++)
					{
						anAttachment = this.theMdlFileData.theAttachments[i];

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

						line += anAttachment.posX.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += anAttachment.posY.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += anAttachment.posZ.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);

						this.theOutputFileStreamWriter.WriteLine(line);
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

			this.theOutputFileStreamWriter.WriteLine();

			if (MainCROWBAR.TheApp.Settings.DecompileDebugInfoFilesIsChecked)
			{
				line = "// Bounding box or hull. Used for collision with a world object.";
				this.theOutputFileStreamWriter.WriteLine(line);
			}

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

		public void WriteBodyGroupCommand()
		{
			string line = "";
			SourceMdlBodyPart2531 aBodyPart = null;
			SourceMdlModel2531 aBodyModel = null;

			if (this.theMdlFileData.theBodyParts != null && this.theMdlFileData.theBodyParts.Count > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = this.theMdlFileData.theBodyParts[bodyPartIndex];

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

							line = "\t";
							if (aBodyModel.theName == "blank")
							{
								line += "blank";
							}
							else
							{
								aBodyModel.theSmdFileNames[0] = SourceFileNamesModule.CreateBodyGroupSmdFileName(aBodyModel.theSmdFileNames[0], bodyPartIndex, modelIndex, 0, this.theModelName, this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].theName);
								line += "studio ";
								line += "\"";
								line += FileManager.GetPathFileNameWithoutExtension(aBodyModel.theSmdFileNames[0]);
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

		public void WriteCBoxCommand()
		{
			string line = "";
			double minX = 0;
			double minY = 0;
			double minZ = 0;
			double maxX = 0;
			double maxY = 0;
			double maxZ = 0;

			this.theOutputFileStreamWriter.WriteLine();

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

		public void WriteCdMaterialsCommand()
		{
			string line = "";

			if (this.theMdlFileData.theTexturePaths != null)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.theTexturePaths.Count; i++)
				{
					string aTexturePath = this.theMdlFileData.theTexturePaths[i];
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
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteControllerCommand()
		{
			//$controller mouth "jaw" X 0 20
			//$controller 0 "tracker" LYR -1 1
			if (this.theMdlFileData.theBoneControllers != null)
			{
				try
				{
					if (this.theMdlFileData.theBoneControllers.Count > 0)
					{
						this.theOutputFileStreamWriter.WriteLine();
					}

					SourceMdlBoneController2531 boneController = null;
					string line = "";
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
						if (boneController.inputField == 4)
						{
							line += "Mouth";
						}
						else
						{
							line += boneController.inputField.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
						}
						line += " \"";
						line += this.theMdlFileData.theBones[boneController.boneIndex].theName;
						line += "\" ";
						line += SourceModule2531.GetControlText(boneController.type);
						line += " ";
						line += boneController.startAngleDegrees.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						line += " ";
						line += boneController.endAngleDegrees.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						this.theOutputFileStreamWriter.WriteLine(line);
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

			offsetX = Math.Round(this.theMdlFileData.eyePosition.y, 3);
			offsetY = -Math.Round(this.theMdlFileData.eyePosition.x, 3);
			offsetZ = Math.Round(this.theMdlFileData.eyePosition.z, 3);

			if (offsetX == 0 && offsetY == 0 && offsetZ == 0)
			{
				return;
			}

			line = "";
			this.theOutputFileStreamWriter.WriteLine(line);

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

			SourceMdlHitboxSet2531 aHitboxSet = null;
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

		public void WriteIncludeModelCommands()
		{
			string line = "";

			if (this.theMdlFileData.theIncludeModels != null)
			{
				line = "";
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int i = 0; i < this.theMdlFileData.theIncludeModels.Count; i++)
				{
					SourceMdlIncludeModel2531 anIncludeModel = this.theMdlFileData.theIncludeModels[i];

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
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
		}

		public void WriteLodCommand()
		{
			string line = "";

			//NOTE: Data is from VTX file.
			if (this.theVtxFileData != null && this.theMdlFileData.theBodyParts != null)
			{
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

				SourceVtxBodyPart107 aBodyPart = null;
				SourceVtxModel107 aVtxModel = null;
				SourceMdlModel2531 aBodyModel = null;
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

		public void WriteModelNameCommand()
		{
			string line = "";
			string modelPathFileName = this.theMdlFileData.theModelName;


			this.theOutputFileStreamWriter.WriteLine();

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

			if (this.theMdlFileData.theSequences != null && this.theMdlFileData.theSequences.Count > 0)
			{
				this.theOutputFileStreamWriter.WriteLine();

				for (int sequenceIndex = 0; sequenceIndex < this.theMdlFileData.theSequences.Count; sequenceIndex++)
				{
					aSequence = this.theMdlFileData.theSequences[sequenceIndex];

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
					this.theOutputFileStreamWriter.WriteLine(line);

					this.WriteSequenceOptions(aSequence);

					line = "}";
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
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
						SourceMdlTexture2531 aTexture = this.theMdlFileData.theTextures[aSkinFamily[j]];

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

		public void WriteCollisionModelOrCollisionJointsCommand()
		{
			string line = "";

			//NOTE: Data is from PHY file.
			if (this.thePhyFileData != null && this.thePhyFileData.solidCount > 0)
			{
				this.theOutputFileStreamWriter.WriteLine(line);

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
			if (this.thePhyFileData.theSourcePhyEditParamsSection.concave == "1")
			{
				line = "\t";
				line += "$concave";
				this.theOutputFileStreamWriter.WriteLine(line);
				line = "\t";
				line += "$maxconvexpieces ";
				line += this.thePhyFileData.theSourcePhyMaxConvexPieces.ToString();
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
				if (anAnimDescIndex >= this.theMdlFileData.theAnimationDescs.Count)
				{
					anAnimDescIndex = this.theMdlFileData.theAnimationDescs.Count - 1;
				}
				anAnimationDesc = this.theMdlFileData.theAnimationDescs[anAnimDescIndex];

				anAnimationDesc.theSmdRelativePathFileName = SourceFileNamesModule.CreateAnimationSmdRelativePathFileName(anAnimationDesc.theSmdRelativePathFileName, this.theModelName, anAnimationDesc.theName);
				line = "\t";
				line += "\"";
				line += anAnimationDesc.theSmdRelativePathFileName;
				line += "\"";
				this.theOutputFileStreamWriter.WriteLine(line);
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
				this.theOutputFileStreamWriter.WriteLine(line);
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
			if (anAnimDescIndex >= this.theMdlFileData.theAnimationDescs.Count)
			{
				anAnimDescIndex = this.theMdlFileData.theAnimationDescs.Count - 1;
			}
			line += this.theMdlFileData.theAnimationDescs[anAnimDescIndex].fps.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
			this.theOutputFileStreamWriter.WriteLine(line);

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

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData2531 theMdlFileData;
		private SourcePhyFileData thePhyFileData;
		private SourceVtxFileData107 theVtxFileData;
		private string theModelName;

		private string theOutputPath;
		private string theOutputFileNameWithoutExtension;

#endregion

	}

}