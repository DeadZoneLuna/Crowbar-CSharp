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
	public class SourceMdlFile2531
	{

#region Creation and Destruction

		public SourceMdlFile2531(BinaryReader mdlFileReader, SourceMdlFileData2531 mdlFileData)
		{
			theInputFileReader = mdlFileReader;
			theMdlFileData = mdlFileData;

			theMdlFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile2531(BinaryWriter mdlFileWriter, SourceMdlFileData2531 mdlFileData)
		{
			theOutputFileWriter = mdlFileWriter;
			theMdlFileData = mdlFileData;
		}

#endregion

#region Methods

		public void ReadMdlHeader()
		{
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theMdlFileData.id = theInputFileReader.ReadChars(theMdlFileData.id.Length);
			theMdlFileData.theID = new string(theMdlFileData.id);
			theMdlFileData.version = theInputFileReader.ReadInt32();
			theMdlFileData.checksum = theInputFileReader.ReadInt32();

			theMdlFileData.name = theInputFileReader.ReadChars(theMdlFileData.name.Length);
			theMdlFileData.theModelName = (new string(theMdlFileData.name)).Trim('\0');

			theMdlFileData.fileSize = theInputFileReader.ReadInt32();
			theMdlFileData.theActualFileSize = theInputFileReader.BaseStream.Length;

			theMdlFileData.eyePosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.eyePosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.eyePosition.z = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.z = theInputFileReader.ReadSingle();

			theMdlFileData.unknown01 = theInputFileReader.ReadSingle();
			theMdlFileData.unknown02 = theInputFileReader.ReadSingle();
			theMdlFileData.unknown03 = theInputFileReader.ReadSingle();

			theMdlFileData.hullMinPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.hullMinPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.hullMinPosition.z = theInputFileReader.ReadSingle();
			theMdlFileData.hullMaxPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.hullMaxPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.hullMaxPosition.z = theInputFileReader.ReadSingle();

			theMdlFileData.viewBoundingBoxMinPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMinPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMinPosition.z = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMaxPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMaxPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMaxPosition.z = theInputFileReader.ReadSingle();

			theMdlFileData.flags = theInputFileReader.ReadInt32();

			theMdlFileData.unknown04 = theInputFileReader.ReadInt32();
			theMdlFileData.unknown05 = theInputFileReader.ReadInt32();

			theMdlFileData.boneCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneOffset = theInputFileReader.ReadInt32();
			theMdlFileData.boneControllerCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneControllerOffset = theInputFileReader.ReadInt32();

			theMdlFileData.hitBoxSetCount = theInputFileReader.ReadInt32();
			theMdlFileData.hitBoxSetOffset = theInputFileReader.ReadInt32();

			theMdlFileData.localAnimationCount = theInputFileReader.ReadInt32();
			theMdlFileData.localAnimationOffset = theInputFileReader.ReadInt32();
			theMdlFileData.localSequenceCount = theInputFileReader.ReadInt32();
			theMdlFileData.localSequenceOffset = theInputFileReader.ReadInt32();
			theMdlFileData.sequencesIndexedFlag = theInputFileReader.ReadInt32();
			theMdlFileData.sequenceGroupCount = theInputFileReader.ReadInt32();
			theMdlFileData.sequenceGroupOffset = theInputFileReader.ReadInt32();

			theMdlFileData.textureCount = theInputFileReader.ReadInt32();
			theMdlFileData.textureOffset = theInputFileReader.ReadInt32();
			theMdlFileData.texturePathCount = theInputFileReader.ReadInt32();
			theMdlFileData.texturePathOffset = theInputFileReader.ReadInt32();
			theMdlFileData.skinReferenceCount = theInputFileReader.ReadInt32();
			theMdlFileData.skinFamilyCount = theInputFileReader.ReadInt32();
			theMdlFileData.skinOffset = theInputFileReader.ReadInt32();

			theMdlFileData.bodyPartCount = theInputFileReader.ReadInt32();
			theMdlFileData.bodyPartOffset = theInputFileReader.ReadInt32();

			theMdlFileData.localAttachmentCount = theInputFileReader.ReadInt32();
			theMdlFileData.localAttachmentOffset = theInputFileReader.ReadInt32();

			theMdlFileData.transitionCount = theInputFileReader.ReadInt32();
			theMdlFileData.transitionOffset = theInputFileReader.ReadInt32();

			theMdlFileData.flexDescCount = theInputFileReader.ReadInt32();
			theMdlFileData.flexDescOffset = theInputFileReader.ReadInt32();
			theMdlFileData.flexControllerCount = theInputFileReader.ReadInt32();
			theMdlFileData.flexControllerOffset = theInputFileReader.ReadInt32();
			theMdlFileData.flexRuleCount = theInputFileReader.ReadInt32();
			theMdlFileData.flexRuleOffset = theInputFileReader.ReadInt32();

			theMdlFileData.ikChainCount = theInputFileReader.ReadInt32();
			theMdlFileData.ikChainOffset = theInputFileReader.ReadInt32();
			theMdlFileData.mouthCount = theInputFileReader.ReadInt32();
			theMdlFileData.mouthOffset = theInputFileReader.ReadInt32();
			theMdlFileData.localPoseParamaterCount = theInputFileReader.ReadInt32();
			theMdlFileData.localPoseParameterOffset = theInputFileReader.ReadInt32();

			theMdlFileData.surfacePropOffset = theInputFileReader.ReadInt32();

			theMdlFileData.unknownCount = theInputFileReader.ReadInt32();
			theMdlFileData.unknownOffset = theInputFileReader.ReadInt32();

			theMdlFileData.includeModelCount = theInputFileReader.ReadInt32();
			theMdlFileData.includeModelOffset = theInputFileReader.ReadInt32();

			theMdlFileData.unknown06 = theInputFileReader.ReadInt32();
			theMdlFileData.unknown07 = theInputFileReader.ReadInt32();
			theMdlFileData.unknown08 = theInputFileReader.ReadInt32();

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header");

			//If Me.theMdlFileData.bodyPartCount = 0 AndAlso Me.theMdlFileData.localSequenceCount > 0 Then
			//	Me.theMdlFileData.theMdlFileOnlyHasAnimations = True
			//End If
		}

		public void ReadBones()
		{
			if (theMdlFileData.boneCount > 0)
			{
				long boneInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.boneOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theBones = new List<SourceMdlBone2531>(theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < theMdlFileData.boneCount; boneIndex++)
					{
						boneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlBone2531 aBone = new SourceMdlBone2531();

						aBone.nameOffset = theInputFileReader.ReadInt32();
						aBone.parentBoneIndex = theInputFileReader.ReadInt32();
						for (int boneControllerIndexIndex = 0; boneControllerIndexIndex < aBone.boneControllerIndex.Length; boneControllerIndexIndex++)
						{
							aBone.boneControllerIndex[boneControllerIndexIndex] = theInputFileReader.ReadInt32();
						}

						//For x As Integer = 0 To aBone.value.Length - 1
						//	aBone.value(x) = Me.theInputFileReader.ReadSingle()
						//Next
						//For x As Integer = 0 To aBone.scale.Length - 1
						//	aBone.scale(x) = Me.theInputFileReader.ReadSingle()
						//Next
						aBone.position.x = theInputFileReader.ReadSingle();
						aBone.position.y = theInputFileReader.ReadSingle();
						aBone.position.z = theInputFileReader.ReadSingle();
						aBone.rotation.x = theInputFileReader.ReadSingle();
						aBone.rotation.y = theInputFileReader.ReadSingle();
						aBone.rotation.z = theInputFileReader.ReadSingle();
						aBone.rotation.w = theInputFileReader.ReadSingle();
						aBone.positionScale.x = theInputFileReader.ReadSingle();
						aBone.positionScale.y = theInputFileReader.ReadSingle();
						aBone.positionScale.z = theInputFileReader.ReadSingle();
						aBone.rotationScale.x = theInputFileReader.ReadSingle();
						aBone.rotationScale.y = theInputFileReader.ReadSingle();
						aBone.rotationScale.z = theInputFileReader.ReadSingle();
						//aBone.unknown01 = Me.theInputFileReader.ReadSingle()
						aBone.rotationScale.w = theInputFileReader.ReadSingle();

						aBone.poseToBoneColumn0.x = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn1.x = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn2.x = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn3.x = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn0.y = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn1.y = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn2.y = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn3.y = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn0.z = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn1.z = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn2.z = theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn3.z = theInputFileReader.ReadSingle();

						aBone.flags = theInputFileReader.ReadInt32();

						aBone.proceduralRuleType = theInputFileReader.ReadInt32();
						aBone.proceduralRuleOffset = theInputFileReader.ReadInt32();
						aBone.physicsBoneIndex = theInputFileReader.ReadInt32();
						aBone.surfacePropNameOffset = theInputFileReader.ReadInt32();
						aBone.contents = theInputFileReader.ReadInt32();

						theMdlFileData.theBones.Add(aBone);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aBone.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aBone.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theName = " + aBone.theName);
							}
						}
						else if (aBone.theName == null)
						{
							aBone.theName = "";
						}
						theMdlFileData.theBoneNameToBoneIndexMap.Add(aBone.theName, boneIndex);

						if (aBone.proceduralRuleOffset != 0)
						{
							if (aBone.proceduralRuleType == SourceMdlBone2531.STUDIO_PROC_AXISINTERP)
							{
								ReadAxisInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone2531.STUDIO_PROC_QUATINTERP)
							{
								theMdlFileData.theProceduralBonesCommandIsUsed = true;
								ReadQuatInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_JIGGLE)
							{
								//Me.ReadJiggleBone(boneInputFileStreamPosition, aBone)
								int debug = 4242;
							}
							else
							{
								int debug = 4242;
							}
						}
						if (aBone.surfacePropNameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.surfacePropNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aBone.theSurfacePropName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theSurfacePropName = " + aBone.theSurfacePropName);
							}
						}
						else
						{
							aBone.theSurfacePropName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadBoneControllers()
		{
			if (theMdlFileData.boneControllerCount > 0)
			{
				//Dim boneControllerInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theBoneControllers = new List<SourceMdlBoneController2531>(theMdlFileData.boneControllerCount);
					for (int i = 0; i < theMdlFileData.boneControllerCount; i++)
					{
						//boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlBoneController2531 aBoneController = new SourceMdlBoneController2531();

						aBoneController.boneIndex = theInputFileReader.ReadInt32();
						aBoneController.type = theInputFileReader.ReadInt32();
						aBoneController.startAngleDegrees = theInputFileReader.ReadSingle();
						aBoneController.endAngleDegrees = theInputFileReader.ReadSingle();
						aBoneController.restIndex = theInputFileReader.ReadInt32();
						aBoneController.inputField = theInputFileReader.ReadInt32();
						for (int x = 0; x < aBoneController.unused.Length; x++)
						{
							aBoneController.unused[x] = theInputFileReader.ReadInt32();
						}

						theMdlFileData.theBoneControllers.Add(aBoneController);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBoneController");

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadAttachments()
		{
			if (theMdlFileData.localAttachmentCount > 0)
			{
				long attachmentInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.localAttachmentOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theAttachments = new List<SourceMdlAttachment2531>(theMdlFileData.localAttachmentCount);
					for (int attachmentIndex = 0; attachmentIndex < theMdlFileData.localAttachmentCount; attachmentIndex++)
					{
						attachmentInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlAttachment2531 anAttachment = new SourceMdlAttachment2531();

						anAttachment.nameOffset = theInputFileReader.ReadInt32();
						anAttachment.type = theInputFileReader.ReadInt32();
						anAttachment.boneIndex = theInputFileReader.ReadInt32();

						//anAttachment.attachmentPointColumn0.x = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn0.y = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn0.z = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn1.x = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn1.y = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn1.z = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn2.x = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn2.y = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn2.z = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn3.x = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn3.y = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPointColumn3.z = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPoint = New SourceVector()
						//anAttachment.attachmentPoint.x = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPoint.y = Me.theInputFileReader.ReadSingle()
						//anAttachment.attachmentPoint.z = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector01.x = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector01.y = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector01.z = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector02.x = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector02.y = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector02.z = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector03.x = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector03.y = Me.theInputFileReader.ReadSingle()
						//anAttachment.vector03.z = Me.theInputFileReader.ReadSingle()
						anAttachment.cXX = theInputFileReader.ReadSingle();
						anAttachment.unused01 = theInputFileReader.ReadSingle();
						anAttachment.unused02 = theInputFileReader.ReadSingle();
						anAttachment.posX = theInputFileReader.ReadSingle();

						anAttachment.cYX = theInputFileReader.ReadSingle();
						anAttachment.unused03 = theInputFileReader.ReadSingle();
						anAttachment.unused04 = theInputFileReader.ReadSingle();
						anAttachment.posY = theInputFileReader.ReadSingle();

						anAttachment.cZX = theInputFileReader.ReadSingle();
						anAttachment.cZY = theInputFileReader.ReadSingle();
						anAttachment.cZZ = theInputFileReader.ReadSingle();
						anAttachment.posZ = theInputFileReader.ReadSingle();


						theMdlFileData.theAttachments.Add(anAttachment);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAttachment");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (anAttachment.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							anAttachment.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName = " + anAttachment.theName);
							}
						}
						else if (anAttachment.theName == null)
						{
							anAttachment.theName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadHitboxSets()
		{
			if (theMdlFileData.hitBoxSetCount > 0)
			{
				long hitboxSetInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.hitBoxSetOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theHitboxSets = new List<SourceMdlHitboxSet2531>(theMdlFileData.hitBoxSetCount);
					for (int i = 0; i < theMdlFileData.hitBoxSetCount; i++)
					{
						hitboxSetInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlHitboxSet2531 aHitboxSet = new SourceMdlHitboxSet2531();

						aHitboxSet.nameOffset = theInputFileReader.ReadInt32();
						aHitboxSet.hitboxCount = theInputFileReader.ReadInt32();
						aHitboxSet.hitboxOffset = theInputFileReader.ReadInt32();

						theMdlFileData.theHitboxSets.Add(aHitboxSet);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aHitboxSet.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aHitboxSet.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitboxSet.theName = " + aHitboxSet.theName);
							}
						}
						else
						{
							aHitboxSet.theName = "";
						}

						ReadHitboxes(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, aHitboxSet);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSequences()
		{
			if (theMdlFileData.localSequenceCount > 0)
			{
				long seqInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.localSequenceOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theSequences = new List<SourceMdlSequenceDesc2531>(theMdlFileData.localSequenceCount);
					for (int i = 0; i < theMdlFileData.localSequenceCount; i++)
					{
						seqInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlSequenceDesc2531 aSequence = new SourceMdlSequenceDesc2531();

						aSequence.nameOffset = theInputFileReader.ReadInt32();
						aSequence.activityNameOffset = theInputFileReader.ReadInt32();
						aSequence.flags = theInputFileReader.ReadInt32();
						aSequence.activityId = theInputFileReader.ReadInt32();
						aSequence.activityWeight = theInputFileReader.ReadInt32();
						aSequence.eventCount = theInputFileReader.ReadInt32();
						aSequence.eventOffset = theInputFileReader.ReadInt32();

						aSequence.bbMin.x = theInputFileReader.ReadSingle();
						aSequence.bbMin.y = theInputFileReader.ReadSingle();
						aSequence.bbMin.z = theInputFileReader.ReadSingle();
						aSequence.bbMax.x = theInputFileReader.ReadSingle();
						aSequence.bbMax.y = theInputFileReader.ReadSingle();
						aSequence.bbMax.z = theInputFileReader.ReadSingle();

						aSequence.blendCount = theInputFileReader.ReadInt32();

						//For x As Integer = 0 To aSequence.anim.Length - 1
						//	aSequence.anim(x) = Me.theInputFileReader.ReadInt16()
						//Next
						for (int rowIndex = 0; rowIndex < SourceModule2531.MAXSTUDIOBLENDS; rowIndex++)
						{
							for (int columnIndex = 0; columnIndex < SourceModule2531.MAXSTUDIOBLENDS; columnIndex++)
							{
								aSequence.anim[rowIndex][columnIndex] = theInputFileReader.ReadInt16();
							}
						}

						aSequence.movementIndex = theInputFileReader.ReadInt32();
						aSequence.groupSize[0] = theInputFileReader.ReadInt32();
						aSequence.groupSize[1] = theInputFileReader.ReadInt32();

						aSequence.paramIndex[0] = theInputFileReader.ReadInt32();
						aSequence.paramIndex[1] = theInputFileReader.ReadInt32();
						aSequence.paramStart[0] = theInputFileReader.ReadSingle();
						aSequence.paramStart[1] = theInputFileReader.ReadSingle();
						aSequence.paramEnd[0] = theInputFileReader.ReadSingle();
						aSequence.paramEnd[1] = theInputFileReader.ReadSingle();
						aSequence.paramParent = theInputFileReader.ReadInt32();

						aSequence.sequenceGroup = theInputFileReader.ReadInt32();

						//aSequence.test = Me.theInputFileReader.ReadInt32()
						aSequence.test = theInputFileReader.ReadSingle();

						aSequence.fadeInTime = theInputFileReader.ReadSingle();
						aSequence.fadeOutTime = theInputFileReader.ReadSingle();

						aSequence.localEntryNodeIndex = theInputFileReader.ReadInt32();
						aSequence.localExitNodeIndex = theInputFileReader.ReadInt32();
						aSequence.nodeFlags = theInputFileReader.ReadInt32();

						aSequence.entryPhase = theInputFileReader.ReadSingle();
						aSequence.exitPhase = theInputFileReader.ReadSingle();
						aSequence.lastFrame = theInputFileReader.ReadSingle();

						aSequence.nextSeq = theInputFileReader.ReadInt32();
						aSequence.pose = theInputFileReader.ReadInt32();

						aSequence.ikRuleCount = theInputFileReader.ReadInt32();
						aSequence.autoLayerCount = theInputFileReader.ReadInt32();
						aSequence.autoLayerOffset = theInputFileReader.ReadInt32();
						//aSequence.weightOffset = Me.theInputFileReader.ReadInt32()
						//aSequence.poseKeyOffset = Me.theInputFileReader.ReadInt32()
						aSequence.unknown01 = theInputFileReader.ReadInt32();

						//aSequence.bbMin2.x = Me.theInputFileReader.ReadSingle()
						//aSequence.bbMin2.y = Me.theInputFileReader.ReadSingle()
						//aSequence.bbMin2.z = Me.theInputFileReader.ReadSingle()
						//aSequence.bbMax2.x = Me.theInputFileReader.ReadSingle()
						//aSequence.bbMax2.y = Me.theInputFileReader.ReadSingle()
						//aSequence.bbMax2.z = Me.theInputFileReader.ReadSingle()
						//For x As Integer = 0 To aSequence.test02.Length - 1
						//	aSequence.test02(x) = Me.theInputFileReader.ReadInt32()
						//Next
						for (int x = 0; x < aSequence.test02.Length; x++)
						{
							aSequence.test02[x] = theInputFileReader.ReadSingle();
						}

						aSequence.test03 = theInputFileReader.ReadInt32();

						aSequence.ikLockCount = theInputFileReader.ReadInt32();
						aSequence.ikLockOffset = theInputFileReader.ReadInt32();
						//aSequence.keyValueOffset = Me.theInputFileReader.ReadInt32()
						aSequence.keyValueSize = theInputFileReader.ReadInt32();
						aSequence.keyValueOffset = theInputFileReader.ReadInt32();

						//aSequence.unknown01 = Me.theInputFileReader.ReadInt32()
						aSequence.unknown02 = theInputFileReader.ReadSingle();
						aSequence.unknown03 = theInputFileReader.ReadSingle();
						for (int x = 0; x < aSequence.unknown04.Length; x++)
						{
							aSequence.unknown04[x] = theInputFileReader.ReadInt32();
						}
						for (int x = 0; x < aSequence.unknown05.Length; x++)
						{
							aSequence.unknown05[x] = theInputFileReader.ReadSingle();
						}

						theMdlFileData.theSequences.Add(aSequence);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence")

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aSequence.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSequence.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aSequence.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequence.theName = " + aSequence.theName);
						}
						else
						{
							aSequence.theName = "";
						}

						//NOTE: Moved this line here so can show the name in the log.
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]");

						if (aSequence.activityNameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSequence.activityNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aSequence.theActivityName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequence.theActivityName = " + aSequence.theActivityName);
							}
						}
						else
						{
							aSequence.theActivityName = "";
						}

						//If (aSeqDesc.groupSize(0) > 1 OrElse aSeqDesc.groupSize(1) > 1) AndAlso aSeqDesc.poseKeyOffset <> 0 Then
						//	Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
						//End If
						//If aSeqDesc.eventCount > 0 AndAlso aSeqDesc.eventOffset <> 0 Then
						//	Me.ReadEvents(seqInputFileStreamPosition, aSeqDesc)
						//End If
						//If aSeqDesc.autoLayerCount > 0 AndAlso aSeqDesc.autoLayerOffset <> 0 Then
						//	Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
						//End If
						//If Me.theMdlFileData.boneCount > 0 AndAlso aSeqDesc.weightOffset > 0 Then
						//	Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
						//End If
						//If aSeqDesc.ikLockCount > 0 AndAlso aSeqDesc.ikLockOffset <> 0 Then
						//	Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
						//End If
						//If (aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)) > 0 AndAlso aSeqDesc.animIndexOffset <> 0 Then
						//	Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
						//End If
						//If aSeqDesc.keyValueSize > 0 AndAlso aSeqDesc.keyValueOffset <> 0 Then
						//	Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)
						//End If
						//If aSeqDesc.activityModifierCount <> 0 AndAlso aSeqDesc.activityModifierOffset <> 0 Then
						//	Me.ReadActivityModifiers(seqInputFileStreamPosition, aSeqDesc)
						//End If

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadLocalAnimationDescs()
		{
			if (theMdlFileData.localAnimationCount > 0)
			{
				long animationDescInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.localAnimationOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theAnimationDescs = new List<SourceMdlAnimationDesc2531>(theMdlFileData.localAnimationCount);
					for (int i = 0; i < theMdlFileData.localAnimationCount; i++)
					{
						animationDescInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlAnimationDesc2531 anAnimationDesc = new SourceMdlAnimationDesc2531();

						anAnimationDesc.nameOffset = theInputFileReader.ReadInt32();
						anAnimationDesc.fps = theInputFileReader.ReadSingle();
						anAnimationDesc.flags = theInputFileReader.ReadInt32();
						anAnimationDesc.frameCount = theInputFileReader.ReadInt32();
						anAnimationDesc.movementCount = theInputFileReader.ReadInt32();
						anAnimationDesc.movementOffset = theInputFileReader.ReadInt32();

						anAnimationDesc.bbMin.x = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMin.y = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMin.z = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMax.x = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMax.y = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMax.z = theInputFileReader.ReadSingle();

						anAnimationDesc.animOffset = theInputFileReader.ReadInt32();

						anAnimationDesc.ikRuleCount = theInputFileReader.ReadInt32();
						anAnimationDesc.ikRuleOffset = theInputFileReader.ReadInt32();

						for (int x = 0; x < anAnimationDesc.unused.Length; x++)
						{
							anAnimationDesc.unused[x] = theInputFileReader.ReadInt32();
						}

						theMdlFileData.theAnimationDescs.Add(anAnimationDesc);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (anAnimationDesc.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							anAnimationDesc.theName = FileManager.ReadNullTerminatedString(theInputFileReader);
							if (anAnimationDesc.theName[0] == '@')
							{
								anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 1);
							}

							//'NOTE: This naming is found in Garry's Mod garrysmod_dir.vpk "\models\m_anm.mdl":  "a_../combine_soldier_xsi/Hold_AR2_base.smd"
							//If anAnimationDesc.theName.StartsWith("a_../") OrElse anAnimationDesc.theName.StartsWith("a_..\") Then
							//	anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 5)
							//	anAnimationDesc.theName = Path.Combine(FileManager.GetPath(anAnimationDesc.theName), "a_" + Path.GetFileName(anAnimationDesc.theName))
							//End If

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAnimationDesc.theName = " + anAnimationDesc.theName);
							}
						}
						else
						{
							anAnimationDesc.theName = "";
						}

						ReadAnimations(animationDescInputFileStreamPosition, anAnimationDesc);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAnimationDescs alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadTextures()
		{
			if (theMdlFileData.textureCount > 0)
			{
				long textureInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;
				//Dim texturePath As String

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.textureOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theTextures = new List<SourceMdlTexture2531>(theMdlFileData.textureCount);
					for (int i = 0; i < theMdlFileData.textureCount; i++)
					{
						textureInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlTexture2531 aTexture = new SourceMdlTexture2531();

						aTexture.fileNameOffset = theInputFileReader.ReadInt32();
						aTexture.flags = theInputFileReader.ReadInt32();
						aTexture.width = theInputFileReader.ReadSingle();
						aTexture.height = theInputFileReader.ReadSingle();
						aTexture.unknown = theInputFileReader.ReadSingle();

						theMdlFileData.theTextures.Add(aTexture);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aTexture.fileNameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.fileNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aTexture.theFileName = FileManager.ReadNullTerminatedString(theInputFileReader);

							// Convert all forward slashes to backward slashes.
							aTexture.theFileName = FileManager.GetNormalizedPathFileName(aTexture.theFileName);

							//NOTE: Leave this commented so QC file simply shows what is stored in MDL file.
							//      Crowbar should always try to show what was in original files unless user opts to do something else.
							//' Delete the path in the texture name that is already in the texturepaths list.
							//For j As Integer = 0 To Me.theMdlFileData.theTexturePaths.Count - 1
							//	texturePath = Me.theMdlFileData.theTexturePaths(j)
							//	If texturePath <> "" AndAlso aTexture.theName.StartsWith(texturePath) Then
							//		aTexture.theName = aTexture.theName.Replace(texturePath, "")
							//		Exit For
							//	End If
							//Next
							//
							//'TEST: If texture name still has a path, remove the path and add it to the texturepaths list.
							//Dim texturePathName As String
							//Dim textureFileName As String
							//texturePathName = FileManager.GetPath(aTexture.theName)
							//textureFileName = Path.GetFileName(aTexture.theName)
							//If aTexture.theName <> textureFileName Then
							//	'NOTE: Place first because it should override whatever is already in list.
							//	'Me.theMdlFileData.theTexturePaths.Add(texturePathName)
							//	Me.theMdlFileData.theTexturePaths.Insert(0, texturePathName)
							//	aTexture.theName = textureFileName
							//End If

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theFileName = " + aTexture.theFileName);
							}
						}
						else
						{
							aTexture.theFileName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadTexturePaths()
		{
			if (theMdlFileData.texturePathCount > 0)
			{
				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.texturePathOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					theMdlFileData.theTexturePaths = new List<string>(theMdlFileData.texturePathCount);

					string aTexturePath = string.Empty;
					for (int i = 0; i < theMdlFileData.texturePathCount; i++)
					{
						long fileOffsetStart = theInputFileReader.BaseStream.Position;
						int texturePathOffset = theInputFileReader.ReadInt32();

						long fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexturePath (offset to text)");

						long inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (texturePathOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin);
							long fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							//TEST: Convert all forward slashes to backward slashes.
							aTexturePath = FileManager.GetNormalizedPathFileName(FileManager.ReadNullTerminatedString(theInputFileReader));

							long fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath (text) = " + aTexturePath);
						}
						theMdlFileData.theTexturePaths.Add(aTexturePath);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTexturePaths alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSkins()
		{
			if (theMdlFileData.skinFamilyCount > 0 && theMdlFileData.skinReferenceCount > 0)
			{
				long skinFamilyInputFileStreamPosition = 0;
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.skinOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theSkinFamilies = new List<List<short>>(theMdlFileData.skinFamilyCount);
					for (int i = 0; i < theMdlFileData.skinFamilyCount; i++)
					{
						skinFamilyInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						List<short> aSkinFamily = new List<short>();

						for (int j = 0; j < theMdlFileData.skinReferenceCount; j++)
						{
							short aSkinRef = theInputFileReader.ReadInt16();
							aSkinFamily.Add(aSkinRef);
						}

						theMdlFileData.theSkinFamilies.Add(aSkinFamily);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSkin");

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

						//If Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 Then
						//	'$pos1 += ($matname_num * 2);
						//	Me.theInputFileReader.BaseStream.Seek(skinFamilyInputFileStreamPosition + Me.theMdlFileData.theTextures.Count * 2, SeekOrigin.Begin)
						//End If
					}

					//'TEST: Remove skinRef from each skinFamily, if it is at same skinRef index in all skinFamilies. 
					//'      Start with the last skinRef index (Me.theMdlFileData.skinReferenceCount)
					//'      and step -1 to 0 until skinRefs are different between skinFamilies.
					//Dim index As Integer = -1
					//For currentSkinRef As Integer = Me.theMdlFileData.skinReferenceCount - 1 To 0 Step -1
					//	For index = 0 To Me.theMdlFileData.skinFamilyCount - 1
					//		Dim aSkinRef As Integer
					//		aSkinRef = Me.theMdlFileData.theSkinFamilies(index)(currentSkinRef)

					//		If aSkinRef <> currentSkinRef Then
					//			Exit For
					//		End If
					//	Next

					//	If index = Me.theMdlFileData.skinFamilyCount Then
					//		For index = 0 To Me.theMdlFileData.skinFamilyCount - 1
					//			Me.theMdlFileData.theSkinFamilies(index).RemoveAt(currentSkinRef)
					//		Next
					//		Me.theMdlFileData.skinReferenceCount -= 1
					//	End If
					//Next

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadIncludeModels()
		{
			if (theMdlFileData.includeModelCount > 0)
			{
				long includeModelInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.includeModelOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theIncludeModels = new List<SourceMdlIncludeModel2531>(theMdlFileData.includeModelCount);
					for (int i = 0; i < theMdlFileData.includeModelCount; i++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						includeModelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlIncludeModel2531 anIncludeModel = new SourceMdlIncludeModel2531();

						anIncludeModel.fileNameOffset = theInputFileReader.ReadInt32();
						for (int x = 0; x < anIncludeModel.unknown.Length; x++)
						{
							anIncludeModel.unknown[x] = theInputFileReader.ReadInt32();
						}

						theMdlFileData.theIncludeModels.Add(anIncludeModel);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (anIncludeModel.fileNameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + anIncludeModel.fileNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							anIncludeModel.theFileName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIncludeModel.theFileName = " + anIncludeModel.theFileName);
							}
						}
						else
						{
							anIncludeModel.theFileName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIncludeModel [" + anIncludeModel.theFileName + "]");
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIncludeModels")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadBodyParts()
		{
			if (theMdlFileData.bodyPartCount > 0)
			{
				long bodyPartInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.bodyPartOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.theBodyParts = new List<SourceMdlBodyPart2531>(theMdlFileData.bodyPartCount);
					for (int i = 0; i < theMdlFileData.bodyPartCount; i++)
					{
						bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart2531 aBodyPart = new SourceMdlBodyPart2531();

						aBodyPart.nameOffset = theInputFileReader.ReadInt32();
						aBodyPart.modelCount = theInputFileReader.ReadInt32();
						aBodyPart.@base = theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = theInputFileReader.ReadInt32();

						theMdlFileData.theBodyParts.Add(aBodyPart);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aBodyPart.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aBodyPart.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBodyPart.theName = " + aBodyPart.theName);
							}
						}
						else
						{
							aBodyPart.theName = "";
						}

						ReadModels(bodyPartInputFileStreamPosition, aBodyPart);

						//'NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts and models.
						//If i = Me.theMdlFileData.bodyPartCount - 1 Then
						//	Me.LogToEndAndAlignToNextStart(Me.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment")
						//End If

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadFlexDescs()
		{
			if (theMdlFileData.flexDescCount > 0)
			{
				long flexDescInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(theMdlFileData.flexDescOffset, SeekOrigin.Begin);
				//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				theMdlFileData.theFlexDescs = new List<SourceMdlFlexDesc>(theMdlFileData.flexDescCount);
				for (int i = 0; i < theMdlFileData.flexDescCount; i++)
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;
					flexDescInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlFlexDesc aFlexDesc = new SourceMdlFlexDesc();
					aFlexDesc.nameOffset = theInputFileReader.ReadInt32();
					theMdlFileData.theFlexDescs.Add(aFlexDesc);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aFlexDesc.nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(flexDescInputFileStreamPosition + aFlexDesc.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aFlexDesc.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexDesc.theName = " + aFlexDesc.theName);
						}
					}
					else
					{
						aFlexDesc.theName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexDesc [" + aFlexDesc.theName + "]");
				}

				//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs")
			}
		}

		public void ReadFlexControllers()
		{
			if (theMdlFileData.flexControllerCount > 0)
			{
				long flexControllerInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(theMdlFileData.flexControllerOffset, SeekOrigin.Begin);
				//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				theMdlFileData.theFlexControllers = new List<SourceMdlFlexController>(theMdlFileData.flexControllerCount);
				for (int i = 0; i < theMdlFileData.flexControllerCount; i++)
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;
					flexControllerInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlFlexController aFlexController = new SourceMdlFlexController();
					aFlexController.typeOffset = theInputFileReader.ReadInt32();
					aFlexController.nameOffset = theInputFileReader.ReadInt32();
					aFlexController.localToGlobal = theInputFileReader.ReadInt32();
					aFlexController.min = theInputFileReader.ReadSingle();
					aFlexController.max = theInputFileReader.ReadSingle();
					theMdlFileData.theFlexControllers.Add(aFlexController);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aFlexController.typeOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.typeOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aFlexController.theType = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theType = " + aFlexController.theType);
						}
					}
					else
					{
						aFlexController.theType = "";
					}
					if (aFlexController.nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aFlexController.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theName = " + aFlexController.theName);
						}
					}
					else
					{
						aFlexController.theName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexController [" + aFlexController.theName + "]");
				}

				if (theMdlFileData.theFlexControllers.Count > 0)
				{
					theMdlFileData.theModelCommandIsUsed = true;
				}

				//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers")
			}
		}

		public void ReadFlexRules()
		{
			if (theMdlFileData.flexRuleCount > 0)
			{
				long flexRuleInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				theInputFileReader.BaseStream.Seek(theMdlFileData.flexRuleOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theFlexRules = new List<SourceMdlFlexRule>(theMdlFileData.flexRuleCount);
				for (int i = 0; i < theMdlFileData.flexRuleCount; i++)
				{
					flexRuleInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlFlexRule aFlexRule = new SourceMdlFlexRule();
					aFlexRule.flexIndex = theInputFileReader.ReadInt32();
					aFlexRule.opCount = theInputFileReader.ReadInt32();
					aFlexRule.opOffset = theInputFileReader.ReadInt32();
					theMdlFileData.theFlexRules.Add(aFlexRule);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					theMdlFileData.theFlexDescs[aFlexRule.flexIndex].theDescIsUsedByFlexRule = true;

					if (aFlexRule.opCount > 0 && aFlexRule.opOffset != 0)
					{
						ReadFlexOps(flexRuleInputFileStreamPosition, aFlexRule);
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				if (theMdlFileData.theFlexRules.Count > 0)
				{
					theMdlFileData.theModelCommandIsUsed = true;
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + theMdlFileData.theFlexDescs.Count.ToString());
			}
		}

		public void ReadPoseParamDescs()
		{
			if (theMdlFileData.localPoseParamaterCount > 0)
			{
				long poseInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.localPoseParameterOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					theMdlFileData.thePoseParamDescs = new List<SourceMdlPoseParamDesc2531>(theMdlFileData.localPoseParamaterCount);
					for (int i = 0; i < theMdlFileData.localPoseParamaterCount; i++)
					{
						poseInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlPoseParamDesc2531 aPoseParamDesc = new SourceMdlPoseParamDesc2531();

						aPoseParamDesc.nameOffset = theInputFileReader.ReadInt32();
						aPoseParamDesc.flags = theInputFileReader.ReadInt32();
						aPoseParamDesc.startingValue = theInputFileReader.ReadSingle();
						aPoseParamDesc.endingValue = theInputFileReader.ReadSingle();
						aPoseParamDesc.loopingRange = theInputFileReader.ReadSingle();

						theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aPoseParamDesc");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aPoseParamDesc.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(poseInputFileStreamPosition + aPoseParamDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aPoseParamDesc.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aPoseParamDesc.theName = " + aPoseParamDesc.theName);
							}
						}
						else
						{
							aPoseParamDesc.theName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSequenceGroups()
		{
			if (theMdlFileData.sequenceGroupCount > 0)
			{
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.sequenceGroupOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					//Me.theMdlFileData.theSequenceGroupFileHeaders = New List(Of SourceMdlSequenceGroupFileHeader2531)(Me.theMdlFileData.sequenceGroupCount)
					theMdlFileData.theSequenceGroups = new List<SourceMdlSequenceGroup2531>(theMdlFileData.sequenceGroupCount);
					for (int sequenceGroupIndex = 0; sequenceGroupIndex < theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlSequenceGroup2531 aSequenceGroup = new SourceMdlSequenceGroup2531();

						//aSequenceGroup.name = Me.theInputFileReader.ReadChars(32)
						//aSequenceGroup.theName = CStr(aSequenceGroup.name).Trim(Chr(0))
						//aSequenceGroup.fileName = Me.theInputFileReader.ReadChars(64)
						//aSequenceGroup.theFileName = CStr(aSequenceGroup.fileName).Trim(Chr(0))
						//aSequenceGroup.cacheOffset = Me.theInputFileReader.ReadInt32()
						//aSequenceGroup.data = Me.theInputFileReader.ReadInt32()
						for (int x = 0; x < aSequenceGroup.unknown.Length; x++)
						{
							//aSequenceGroup.unknown(x) = Me.theInputFileReader.ReadSingle()
							aSequenceGroup.unknown[x] = theInputFileReader.ReadInt32();
						}

						theMdlFileData.theSequenceGroups.Add(aSequenceGroup);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequenceGroup ");
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequenceGroup " + aSequenceGroup.theName + " [filename = " + aSequenceGroup.theFileName + "]")
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceGroups " + Me.theMdlFileData.theSequenceGroups.Count.ToString())

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSequenceGroups alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSurfaceProp()
		{
			if (theMdlFileData.surfacePropOffset > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.surfacePropOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(theInputFileReader);

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theSurfacePropName = " + theMdlFileData.theSurfacePropName);
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
			else
			{
				theMdlFileData.theSurfacePropName = "";
			}
		}

		public void ReadUnreadBytes()
		{
			theMdlFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

		public void CreateFlexFrameList()
		{
			FlexFrame2531 aFlexFrame = null;
			SourceMdlBodyPart2531 aBodyPart = null;
			SourceMdlModel2531 aModel = null;
			SourceMdlMesh2531 aMesh = null;
			SourceMdlFlex2531 aFlex = null;
			FlexFrame2531 searchedFlexFrame = null;

			theMdlFileData.theFlexFrames = new List<FlexFrame2531>();

			//NOTE: Create the defaultflex.
			aFlexFrame = new FlexFrame2531();
			theMdlFileData.theFlexFrames.Add(aFlexFrame);

			if (theMdlFileData.theFlexDescs != null && theMdlFileData.theFlexDescs.Count > 0)
			{
				//Dim flexDescToMeshIndexes As List(Of List(Of Integer))
				List<List<FlexFrame2531>> flexDescToFlexFrames = null;
				int meshVertexIndexStart = 0;

				//flexDescToMeshIndexes = New List(Of List(Of Integer))(Me.theMdlFileData.theFlexDescs.Count)
				//For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
				//	Dim meshIndexList As New List(Of Integer)()
				//	flexDescToMeshIndexes.Add(meshIndexList)
				//Next

				flexDescToFlexFrames = new List<List<FlexFrame2531>>(theMdlFileData.theFlexDescs.Count);
				for (int x = 0; x < theMdlFileData.theFlexDescs.Count; x++)
				{
					List<FlexFrame2531> flexFrameList = new List<FlexFrame2531>();
					flexDescToFlexFrames.Add(flexFrameList);
				}

				for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];

					if (aBodyPart.theModels != null && aBodyPart.theModels.Count > 0)
					{
						for (int modelIndex = 0; modelIndex < aBodyPart.theModels.Count; modelIndex++)
						{
							aModel = aBodyPart.theModels[modelIndex];

							if (aModel.theMeshes != null && aModel.theMeshes.Count > 0)
							{
								for (int meshIndex = 0; meshIndex < aModel.theMeshes.Count; meshIndex++)
								{
									aMesh = aModel.theMeshes[meshIndex];

									meshVertexIndexStart = theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].theMeshes[meshIndex].vertexIndexStart;

									if (aMesh.theFlexes != null && aMesh.theFlexes.Count > 0)
									{
										for (int flexIndex = 0; flexIndex < aMesh.theFlexes.Count; flexIndex++)
										{
											aFlex = aMesh.theFlexes[flexIndex];

											aFlexFrame = null;
											if (flexDescToFlexFrames[aFlex.flexDescIndex] != null)
											{
												int tempVar = flexDescToFlexFrames[aFlex.flexDescIndex].Count;
												for (int x = 0; x < tempVar; x++)
												{
													searchedFlexFrame = flexDescToFlexFrames[aFlex.flexDescIndex][x];
													if (searchedFlexFrame.flexes[0].target0 == aFlex.target0 && searchedFlexFrame.flexes[0].target1 == aFlex.target1 && searchedFlexFrame.flexes[0].target2 == aFlex.target2 && searchedFlexFrame.flexes[0].target3 == aFlex.target3)
													{
														// Add to an existing FlexFrame2531.
														aFlexFrame = searchedFlexFrame;
														break;
													}
												}
											}
											if (aFlexFrame == null)
											{
												aFlexFrame = new FlexFrame2531();
												theMdlFileData.theFlexFrames.Add(aFlexFrame);
												aFlexFrame.bodyAndMeshVertexIndexStarts = new List<int>();
												aFlexFrame.flexes = new List<SourceMdlFlex2531>();

												//Dim aFlexDescPartnerIndex As Integer
												//aFlexDescPartnerIndex = aMesh.theFlexes(flexIndex).flexDescPartnerIndex

												aFlexFrame.flexName = theMdlFileData.theFlexDescs[aFlex.flexDescIndex].theName;
												//If aFlexDescPartnerIndex > 0 Then
												//	'line += "flexpair """
												//	'aFlexFrame.flexName = aFlexFrame.flexName.Remove(aFlexFrame.flexName.Length - 1, 1)
												//	aFlexFrame.flexDescription = aFlexFrame.flexName
												//	aFlexFrame.flexDescription += "+"
												//	aFlexFrame.flexDescription += Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theName
												//	aFlexFrame.flexHasPartner = True
												//	aFlexFrame.flexSplit = Me.GetSplit(aFlex, meshVertexIndexStart)
												//	Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
												//Else
												//line += "flex """
												aFlexFrame.flexDescription = aFlexFrame.flexName;
												aFlexFrame.flexHasPartner = false;
												//End If
												theMdlFileData.theFlexDescs[aFlex.flexDescIndex].theDescIsUsedByFlex = true;

												flexDescToFlexFrames[aFlex.flexDescIndex].Add(aFlexFrame);
											}

											aFlexFrame.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart);
											aFlexFrame.flexes.Add(aFlex);

											//flexDescToMeshIndexes(aFlex.flexDescIndex).Add(meshIndex)
										}
									}
								}
							}
							//For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
							//	flexDescToMeshIndexes(x).Clear()
							//Next
						}
					}
				}
			}
		}

		public void WriteInternalMdlFileName(string internalMdlFileName)
		{
			theOutputFileWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
			//TODO: Should only write up to 128 characters.
			theOutputFileWriter.Write(internalMdlFileName.ToCharArray());
			//NOTE: Write the ending null byte.
			theOutputFileWriter.Write(Convert.ToByte(0));
		}

#endregion

#region Private Methods

		private void ReadAxisInterpBone(long boneInputFileStreamPosition, SourceMdlBone2531 aBone)
		{
			long axisInterpBoneInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);

				axisInterpBoneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aBone.theAxisInterpBone = new SourceMdlAxisInterpBone2531();
				aBone.theAxisInterpBone.controlBoneIndex = theInputFileReader.ReadInt32();
				aBone.theAxisInterpBone.axis = theInputFileReader.ReadInt32();
				for (int x = 0; x < aBone.theAxisInterpBone.pos.Length; x++)
				{
					aBone.theAxisInterpBone.pos[x].x = theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.pos[x].y = theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.pos[x].z = theInputFileReader.ReadSingle();
				}
				for (int x = 0; x < aBone.theAxisInterpBone.quat.Length; x++)
				{
					aBone.theAxisInterpBone.quat[x].x = theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.quat[x].y = theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.quat[x].z = theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.quat[x].z = theInputFileReader.ReadSingle();
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone");

				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadQuatInterpBone(long boneInputFileStreamPosition, SourceMdlBone2531 aBone)
		{
			long quatInterpBoneInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);

				quatInterpBoneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aBone.theQuatInterpBone = new SourceMdlQuatInterpBone2531();
				aBone.theQuatInterpBone.controlBoneIndex = theInputFileReader.ReadInt32();
				aBone.theQuatInterpBone.triggerCount = theInputFileReader.ReadInt32();
				aBone.theQuatInterpBone.triggerOffset = theInputFileReader.ReadInt32();

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone");

				inputFileStreamPosition = theInputFileReader.BaseStream.Position;

				if (aBone.theQuatInterpBone.triggerCount > 0 && aBone.theQuatInterpBone.triggerOffset != 0)
				{
					ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone);
				}

				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadTriggers(long quatInterpBoneInputFileStreamPosition, SourceMdlQuatInterpBone2531 aQuatInterpBone)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin);
				//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aQuatInterpBone.theTriggers = new List<SourceMdlQuatInterpInfo2531>(aQuatInterpBone.triggerCount);
				for (int j = 0; j < aQuatInterpBone.triggerCount; j++)
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;
					SourceMdlQuatInterpInfo2531 aTrigger = new SourceMdlQuatInterpInfo2531();

					aTrigger.inverseToleranceAngle = theInputFileReader.ReadSingle();

					aTrigger.trigger.x = theInputFileReader.ReadSingle();
					aTrigger.trigger.y = theInputFileReader.ReadSingle();
					aTrigger.trigger.z = theInputFileReader.ReadSingle();
					aTrigger.trigger.w = theInputFileReader.ReadSingle();

					aTrigger.pos.x = theInputFileReader.ReadSingle();
					aTrigger.pos.y = theInputFileReader.ReadSingle();
					aTrigger.pos.z = theInputFileReader.ReadSingle();

					aTrigger.quat.x = theInputFileReader.ReadSingle();
					aTrigger.quat.y = theInputFileReader.ReadSingle();
					aTrigger.quat.z = theInputFileReader.ReadSingle();
					aTrigger.quat.w = theInputFileReader.ReadSingle();

					aQuatInterpBone.theTriggers.Add(aTrigger);

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.aQuatInterpBone.aTrigger");
				}

				//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aQuatInterpBone.theTriggers")
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadHitboxes(long hitboxOffsetInputFileStreamPosition, SourceMdlHitboxSet2531 aHitboxSet)
		{
			if (aHitboxSet.hitboxCount > 0)
			{
				//Dim hitboxInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(hitboxOffsetInputFileStreamPosition, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aHitboxSet.theHitboxes = new List<SourceMdlHitbox2531>(aHitboxSet.hitboxCount);
					for (int j = 0; j < aHitboxSet.hitboxCount; j++)
					{
						//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlHitbox2531 aHitbox = new SourceMdlHitbox2531();

						aHitbox.boneIndex = theInputFileReader.ReadInt32();
						aHitbox.groupIndex = theInputFileReader.ReadInt32();
						aHitbox.boundingBoxMin.x = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.y = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.z = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.x = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.y = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.z = theInputFileReader.ReadSingle();

						aHitboxSet.theHitboxes.Add(aHitbox);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitbox");

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModels(long bodyPartInputFileStreamPosition, SourceMdlBodyPart2531 aBodyPart)
		{
			if (aBodyPart.modelCount > 0)
			{
				long modelInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aBodyPart.theModels = new List<SourceMdlModel2531>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlModel2531 aModel = new SourceMdlModel2531();

						aModel.name = theInputFileReader.ReadChars(aModel.name.Length);
						aModel.theName = (new string(aModel.name)).Trim('\0');
						aModel.type = theInputFileReader.ReadInt32();
						aModel.boundingRadius = theInputFileReader.ReadSingle();

						aModel.meshCount = theInputFileReader.ReadInt32();
						aModel.meshOffset = theInputFileReader.ReadInt32();
						aModel.vertexCount = theInputFileReader.ReadInt32();
						aModel.vertexOffset = theInputFileReader.ReadInt32();
						aModel.tangentOffset = theInputFileReader.ReadInt32();

						aModel.vertexListType = theInputFileReader.ReadInt32();

						for (int x = 0; x < aModel.unknown01.Length; x++)
						{
							aModel.unknown01[x] = theInputFileReader.ReadSingle();
						}

						//aModel.unknownCount = Me.theInputFileReader.ReadInt32()
						//aModel.unknownOffset = Me.theInputFileReader.ReadInt32()

						for (int x = 0; x < aModel.unknown02.Length; x++)
						{
							aModel.unknown02[x] = theInputFileReader.ReadSingle();
						}
						//For x As Integer = 0 To aModel.unknown03.Length - 1
						//	aModel.unknown03(x) = Me.theInputFileReader.ReadInt32()
						//Next

						aModel.attachmentCount = theInputFileReader.ReadInt32();
						aModel.attachmentOffset = theInputFileReader.ReadInt32();
						aModel.eyeballCount = theInputFileReader.ReadInt32();
						aModel.eyeballOffset = theInputFileReader.ReadInt32();
						for (int x = 0; x < aModel.unknown03.Length; x++)
						{
							aModel.unknown03[x] = theInputFileReader.ReadInt32();
						}

						aModel.unknown01Count = theInputFileReader.ReadInt32();
						aModel.unknown01Offset = theInputFileReader.ReadInt32();
						aModel.unknown02Count = theInputFileReader.ReadInt32();
						aModel.unknown02Offset = theInputFileReader.ReadInt32();

						aBodyPart.theModels.Add(aModel);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
						ReadEyeballs(modelInputFileStreamPosition, aModel);
						ReadMeshes(modelInputFileStreamPosition, aModel);
						//If (Me.theMdlFileData.flags And SourceMdlFileData2531.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
						if (aModel.vertexListType == 0)
						{
							ReadVertexesType0(modelInputFileStreamPosition, aModel);
						}
						else if (aModel.vertexListType == 1)
						{
							ReadVertexesType1(modelInputFileStreamPosition, aModel);
						}
						else
						{
							ReadVertexesType2(modelInputFileStreamPosition, aModel);
						}
						ReadTangents(modelInputFileStreamPosition, aModel);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aBodyPart.theModels alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadEyeballs(long modelInputFileStreamPosition, SourceMdlModel2531 aModel)
		{
			if (aModel.eyeballCount > 0 && aModel.eyeballOffset != 0)
			{
				long eyeballInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aModel.theEyeballs = new List<SourceMdlEyeball2531>(aModel.eyeballCount);
					for (int k = 0; k < aModel.eyeballCount; k++)
					{
						eyeballInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlEyeball2531 anEyeball = new SourceMdlEyeball2531();

						anEyeball.nameOffset = theInputFileReader.ReadInt32();

						anEyeball.boneIndex = theInputFileReader.ReadInt32();
						anEyeball.org.x = theInputFileReader.ReadSingle();
						anEyeball.org.y = theInputFileReader.ReadSingle();
						anEyeball.org.z = theInputFileReader.ReadSingle();
						anEyeball.zOffset = theInputFileReader.ReadSingle();
						anEyeball.radius = theInputFileReader.ReadSingle();
						anEyeball.up.x = theInputFileReader.ReadSingle();
						anEyeball.up.y = theInputFileReader.ReadSingle();
						anEyeball.up.z = theInputFileReader.ReadSingle();
						anEyeball.forward.x = theInputFileReader.ReadSingle();
						anEyeball.forward.y = theInputFileReader.ReadSingle();
						anEyeball.forward.z = theInputFileReader.ReadSingle();

						anEyeball.texture = theInputFileReader.ReadInt32();
						anEyeball.iris_material = theInputFileReader.ReadInt32();
						anEyeball.iris_scale = theInputFileReader.ReadSingle();
						anEyeball.glint_material = theInputFileReader.ReadInt32();

						anEyeball.upperFlexDesc[0] = theInputFileReader.ReadInt32();
						anEyeball.upperFlexDesc[1] = theInputFileReader.ReadInt32();
						anEyeball.upperFlexDesc[2] = theInputFileReader.ReadInt32();
						anEyeball.lowerFlexDesc[0] = theInputFileReader.ReadInt32();
						anEyeball.lowerFlexDesc[1] = theInputFileReader.ReadInt32();
						anEyeball.lowerFlexDesc[2] = theInputFileReader.ReadInt32();
						anEyeball.upperTarget[0] = theInputFileReader.ReadSingle();
						anEyeball.upperTarget[1] = theInputFileReader.ReadSingle();
						anEyeball.upperTarget[2] = theInputFileReader.ReadSingle();
						anEyeball.lowerTarget[0] = theInputFileReader.ReadSingle();
						anEyeball.lowerTarget[1] = theInputFileReader.ReadSingle();
						anEyeball.lowerTarget[2] = theInputFileReader.ReadSingle();

						anEyeball.upperLidFlexDesc = theInputFileReader.ReadInt32();
						anEyeball.lowerLidFlexDesc = theInputFileReader.ReadInt32();

						anEyeball.minPitch = theInputFileReader.ReadSingle();
						anEyeball.maxPitch = theInputFileReader.ReadSingle();
						anEyeball.minYaw = theInputFileReader.ReadSingle();
						anEyeball.maxYaw = theInputFileReader.ReadSingle();

						aModel.theEyeballs.Add(anEyeball);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anEyeball");

						//'NOTE: Set the default value to -1 to distinguish it from value assigned to it by ReadMeshes().
						//anEyeball.theTextureIndex = -1

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//NOTE: The mdl file doesn't appear to store the eyeball name; studiomdl only uses it internally with eyelids.
						if (anEyeball.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(eyeballInputFileStreamPosition + anEyeball.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							anEyeball.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEyeball.theName = " + anEyeball.theName);
							}
						}
						else
						{
							anEyeball.theName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//If aModel.theEyeballs.Count > 0 Then
					//	Me.theMdlFileData.theModelCommandIsUsed = True
					//End If

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theEyeballs alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMeshes(long modelInputFileStreamPosition, SourceMdlModel2531 aModel)
		{
			if (aModel.meshCount > 0 && aModel.meshOffset != 0)
			{
				long meshInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aModel.theMeshes = new List<SourceMdlMesh2531>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						meshInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlMesh2531 aMesh = new SourceMdlMesh2531();

						aMesh.materialIndex = theInputFileReader.ReadInt32();
						aMesh.modelOffset = theInputFileReader.ReadInt32();
						aMesh.vertexCount = theInputFileReader.ReadInt32();
						aMesh.vertexIndexStart = theInputFileReader.ReadInt32();
						aMesh.flexCount = theInputFileReader.ReadInt32();
						aMesh.flexOffset = theInputFileReader.ReadInt32();

						SourceMdlMeshVertexData meshVertexData = new SourceMdlMeshVertexData();
						meshVertexData.modelVertexDataP = theInputFileReader.ReadInt32();
						for (int x = 0; x < SourceConstants.MAX_NUM_LODS; x++)
						{
							meshVertexData.lodVertexCount[x] = theInputFileReader.ReadInt32();
						}
						aMesh.vertexData = meshVertexData;

						aModel.theMeshes.Add(aMesh);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh");

						//' Fill-in eyeball texture index info.
						//If aMesh.materialType = 1 Then
						//	aModel.theEyeballs(aMesh.materialParam).theTextureIndex = aMesh.materialIndex
						//End If

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aMesh.flexCount > 0 && aMesh.flexOffset != 0)
						{
							ReadFlexes(meshInputFileStreamPosition, aMesh);
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes")

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 32, "aModel.theMeshes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadFlexes(long meshInputFileStreamPosition, SourceMdlMesh2531 aMesh)
		{
			aMesh.theFlexes = new List<SourceMdlFlex2531>(aMesh.flexCount);
			long flexInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				for (int k = 0; k < aMesh.flexCount; k++)
				{
					flexInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlFlex2531 aFlex = new SourceMdlFlex2531();

					aFlex.flexDescIndex = theInputFileReader.ReadInt32();

					aFlex.target0 = theInputFileReader.ReadSingle();
					aFlex.target1 = theInputFileReader.ReadSingle();
					aFlex.target2 = theInputFileReader.ReadSingle();
					aFlex.target3 = theInputFileReader.ReadSingle();

					aFlex.vertCount = theInputFileReader.ReadInt32();
					aFlex.vertOffset = theInputFileReader.ReadInt32();

					aFlex.unknown = theInputFileReader.ReadInt32();
					//------
					//aFlex.flexDescPartnerIndex = Me.theInputFileReader.ReadInt32()
					//aFlex.vertAnimType = Me.theInputFileReader.ReadByte()
					//For x As Integer = 0 To aFlex.unusedChar.Length - 1
					//	aFlex.unusedChar(x) = Me.theInputFileReader.ReadChar()
					//Next
					//For x As Integer = 0 To aFlex.unused.Length - 1
					//	aFlex.unused(x) = Me.theInputFileReader.ReadInt32()
					//Next

					aMesh.theFlexes.Add(aFlex);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aFlex.vertCount > 0 && aFlex.vertOffset != 0)
					{
						ReadVertAnims(flexInputFileStreamPosition, aFlex);
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString());

				//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aMesh.theFlexes alignment")
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private int SortVertAnims(SourceMdlVertAnim2531 x, SourceMdlVertAnim2531 y)
		{
			return x.index.CompareTo(y.index);
		}

		private void ReadVertAnims(long flexInputFileStreamPosition, SourceMdlFlex2531 aFlex)
		{
			long eyeballInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				SourceMdlVertAnim2531 aVertAnim = null;
				aFlex.theVertAnims = new List<SourceMdlVertAnim2531>(aFlex.vertCount);
				for (int k = 0; k < aFlex.vertCount; k++)
				{
					eyeballInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					aVertAnim = new SourceMdlVertAnim2531();

					aVertAnim.index = theInputFileReader.ReadUInt16();

					//aVertAnim.deltaX = Me.theInputFileReader.ReadByte()
					//aVertAnim.deltaY = Me.theInputFileReader.ReadByte()
					//aVertAnim.deltaZ = Me.theInputFileReader.ReadByte()
					//aVertAnim.nDeltaX = Me.theInputFileReader.ReadByte()
					//aVertAnim.nDeltaY = Me.theInputFileReader.ReadByte()
					//aVertAnim.nDeltaZ = Me.theInputFileReader.ReadByte()
					//------
					//TEST: almost correct
					//aVertAnim.deltaX = Me.theInputFileReader.ReadSByte()
					//aVertAnim.deltaY = Me.theInputFileReader.ReadSByte()
					//aVertAnim.deltaZ = Me.theInputFileReader.ReadSByte()
					//aVertAnim.nDeltaX = Me.theInputFileReader.ReadSByte()
					//aVertAnim.nDeltaY = Me.theInputFileReader.ReadSByte()
					//aVertAnim.nDeltaZ = Me.theInputFileReader.ReadSByte()
					//------
					//aVertAnim.deltaX = Me.theInputFileReader.ReadSByte()
					//aVertAnim.nDeltaX = Me.theInputFileReader.ReadSByte()
					//aVertAnim.deltaY = Me.theInputFileReader.ReadSByte()
					//aVertAnim.nDeltaY = Me.theInputFileReader.ReadSByte()
					//aVertAnim.deltaZ = Me.theInputFileReader.ReadSByte()
					//aVertAnim.nDeltaZ = Me.theInputFileReader.ReadSByte()
					//------
					aVertAnim.deltaX = theInputFileReader.ReadInt16();
					aVertAnim.deltaY = theInputFileReader.ReadInt16();
					aVertAnim.deltaZ = theInputFileReader.ReadInt16();
					//------
					//For x As Integer = 0 To 2
					//	aVertAnim.deltaByte(x) = Me.theInputFileReader.ReadByte()
					//Next
					//For x As Integer = 0 To 2
					//	aVertAnim.nDeltaByte(x) = Me.theInputFileReader.ReadByte()
					//Next
					//------
					//For x As Integer = 0 To 2
					//	aVertAnim.deltaUShort(x) = Me.theInputFileReader.ReadUInt16()
					//Next

					aFlex.theVertAnims.Add(aVertAnim);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				//aFlex.theVertAnims.Sort(AddressOf Me.SortVertAnims)

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment");
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadFlexOps(long flexRuleInputFileStreamPosition, SourceMdlFlexRule aFlexRule)
		{
			//Dim flexRuleInputFileStreamPosition As Long
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				theInputFileReader.BaseStream.Seek(flexRuleInputFileStreamPosition + aFlexRule.opOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aFlexRule.theFlexOps = new List<SourceMdlFlexOp>(aFlexRule.opCount);
				for (int i = 0; i < aFlexRule.opCount; i++)
				{
					//flexRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlFlexOp aFlexOp = new SourceMdlFlexOp();
					aFlexOp.op = theInputFileReader.ReadInt32();
					if (aFlexOp.op == SourceMdlFlexOp.STUDIO_CONST)
					{
						aFlexOp.value = theInputFileReader.ReadSingle();
					}
					else
					{
						aFlexOp.index = theInputFileReader.ReadInt32();
						if (aFlexOp.op == SourceMdlFlexOp.STUDIO_FETCH2)
						{
							theMdlFileData.theFlexDescs[aFlexOp.index].theDescIsUsedByFlexRule = true;
						}
					}
					aFlexRule.theFlexOps.Add(aFlexOp);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexRule.theFlexOps " + aFlexRule.theFlexOps.Count.ToString());
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadVertexesType0(long modelInputFileStreamPosition, SourceMdlModel2531 aModel)
		{
			if (aModel.vertexCount > 0)
			{
				//Dim hitboxInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theVertexesType0 = new List<SourceMdlType0Vertex2531>(aModel.vertexCount);
					for (int j = 0; j < aModel.vertexCount; j++)
					{
						//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlType0Vertex2531 aVertex = new SourceMdlType0Vertex2531();

						for (int x = 0; x < aVertex.weight.Length; x++)
						{
							aVertex.weight[x] = theInputFileReader.ReadByte();
						}
						aVertex.unknown1 = theInputFileReader.ReadByte();
						for (int x = 0; x < aVertex.boneIndex.Length; x++)
						{
							aVertex.boneIndex[x] = theInputFileReader.ReadInt16();
						}
						aVertex.unknown2 = theInputFileReader.ReadInt16();

						aVertex.position.x = theInputFileReader.ReadSingle();
						aVertex.position.y = theInputFileReader.ReadSingle();
						aVertex.position.z = theInputFileReader.ReadSingle();
						aVertex.normal.x = theInputFileReader.ReadSingle();
						aVertex.normal.y = theInputFileReader.ReadSingle();
						aVertex.normal.z = theInputFileReader.ReadSingle();
						aVertex.texCoordU = theInputFileReader.ReadSingle();
						aVertex.texCoordV = theInputFileReader.ReadSingle();

						aModel.theVertexesType0.Add(aVertex);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType0 " + aModel.theVertexesType0.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadVertexesType1(long modelInputFileStreamPosition, SourceMdlModel2531 aModel)
		{
			if (aModel.vertexCount > 0)
			{
				long hitboxInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theVertexesType1 = new List<SourceMdlType1Vertex2531>(aModel.vertexCount);
					for (int j = 0; j < aModel.vertexCount; j++)
					{
						hitboxInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlType1Vertex2531 aVertex = new SourceMdlType1Vertex2531();

						//aVertex.positionX = Me.theInputFileReader.ReadUInt16()
						//aVertex.positionY = Me.theInputFileReader.ReadUInt16()
						//aVertex.positionZ = Me.theInputFileReader.ReadUInt16()
						//aVertex.normalIndex = Me.theInputFileReader.ReadUInt16()
						//aVertex.texCoordU = Me.theInputFileReader.ReadUInt16()
						//aVertex.texCoordV = Me.theInputFileReader.ReadUInt16()
						//For x As Integer = 0 To aVertex.unknown.Length - 1
						//	aVertex.unknown(x) = Me.theInputFileReader.ReadByte()
						//Next
						aVertex.positionX = theInputFileReader.ReadUInt16();
						aVertex.positionY = theInputFileReader.ReadUInt16();
						aVertex.positionZ = theInputFileReader.ReadUInt16();
						//aVertex.normalX = Me.theInputFileReader.ReadUInt16()
						//aVertex.normalY = Me.theInputFileReader.ReadUInt16()
						//aVertex.normalZ = Me.theInputFileReader.ReadUInt16()
						aVertex.normalX = theInputFileReader.ReadByte();
						aVertex.normalY = theInputFileReader.ReadByte();
						aVertex.normalZ = theInputFileReader.ReadByte();
						//Me.theInputFileReader.ReadByte()
						aVertex.texCoordU = theInputFileReader.ReadByte();
						theInputFileReader.ReadByte();
						aVertex.texCoordV = theInputFileReader.ReadByte();
						//Me.theInputFileReader.ReadByte()
						//aVertex.scaleX = Me.theInputFileReader.ReadByte()
						//aVertex.scaleY = Me.theInputFileReader.ReadByte()
						//aVertex.scaleZ = Me.theInputFileReader.ReadByte()

						aModel.theVertexesType1.Add(aVertex);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//DEBUG:
						theInputFileReader.BaseStream.Seek(hitboxInputFileStreamPosition, SeekOrigin.Begin);
						for (int x = 0; x < aVertex.unknown.Length; x++)
						{
							aVertex.unknown[x] = theInputFileReader.ReadByte();
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType1 " + aModel.theVertexesType1.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadVertexesType2(long modelInputFileStreamPosition, SourceMdlModel2531 aModel)
		{
			if (aModel.vertexCount > 0)
			{
				//Dim hitboxInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theVertexesType2 = new List<SourceMdlType2Vertex2531>(aModel.vertexCount);
					for (int j = 0; j < aModel.vertexCount; j++)
					{
						//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlType2Vertex2531 aVertex = new SourceMdlType2Vertex2531();

						//aVertex.positionX = Me.theInputFileReader.ReadUInt16()
						//aVertex.positionY = Me.theInputFileReader.ReadUInt16()
						//aVertex.positionZ = Me.theInputFileReader.ReadUInt16()
						//aVertex.normalIndex = Me.theInputFileReader.ReadUInt16()
						//aVertex.texCoordU = Me.theInputFileReader.ReadUInt16()
						//aVertex.texCoordV = Me.theInputFileReader.ReadUInt16()
						//For x As Integer = 0 To aVertex.unknown.Length - 1
						//	aVertex.unknown(x) = Me.theInputFileReader.ReadByte()
						//Next
						aVertex.positionX = theInputFileReader.ReadByte();
						aVertex.positionY = theInputFileReader.ReadByte();
						aVertex.positionZ = theInputFileReader.ReadByte();
						//aVertex.positionX = Me.theInputFileReader.ReadSByte()
						//aVertex.positionY = Me.theInputFileReader.ReadSByte()
						//aVertex.positionZ = Me.theInputFileReader.ReadSByte()

						aVertex.normalX = theInputFileReader.ReadByte();
						aVertex.normalY = theInputFileReader.ReadByte();
						aVertex.texCoordU = theInputFileReader.ReadByte();
						aVertex.normalZ = theInputFileReader.ReadByte();
						aVertex.texCoordV = theInputFileReader.ReadByte();

						aModel.theVertexesType2.Add(aVertex);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType2 " + aModel.theVertexesType2.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadTangents(long modelInputFileStreamPosition, SourceMdlModel2531 aModel)
		{
			if (aModel.vertexCount > 0)
			{
				//Dim hitboxInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.tangentOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theTangents = new List<SourceMdlTangent2531>(aModel.vertexCount);
					for (int j = 0; j < aModel.vertexCount; j++)
					{
						//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlTangent2531 aTangent = new SourceMdlTangent2531();

						aTangent.x = theInputFileReader.ReadSingle();
						aTangent.y = theInputFileReader.ReadSingle();
						aTangent.z = theInputFileReader.ReadSingle();
						aTangent.w = theInputFileReader.ReadSingle();

						aModel.theTangents.Add(aTangent);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTangent")

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theTangents " + aModel.theTangents.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadAnimations(long animationDescInputFileStreamPosition, SourceMdlAnimationDesc2531 anAnimationDesc)
		{
			if (theMdlFileData.boneCount > 0 && anAnimationDesc.animOffset != 0)
			{
				long animationInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					anAnimationDesc.theAnimations = new List<SourceMdlAnimation2531>(theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < theMdlFileData.boneCount; boneIndex++)
					{
						animationInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlAnimation2531 anAnimation = new SourceMdlAnimation2531();

						//anAnimation.flags = Me.theInputFileReader.ReadInt32()
						//If (anAnimation.flags And SourceMdlAnimation2531.STUDIO_POS_ANIMATED) > 0 Then
						//	anAnimation.theOffsets(0) = Me.theInputFileReader.ReadInt32()
						//	anAnimation.theOffsets(1) = Me.theInputFileReader.ReadInt32()
						//	anAnimation.theOffsets(2) = Me.theInputFileReader.ReadInt32()

						//	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//	anAnimation.thePositionAnimationXValues = New List(Of SourceMdlAnimationValue2531)()
						//	anAnimation.thePositionAnimationYValues = New List(Of SourceMdlAnimationValue2531)()
						//	anAnimation.thePositionAnimationZValues = New List(Of SourceMdlAnimationValue2531)()
						//	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(0), anAnimationDesc.frameCount, anAnimation.thePositionAnimationXValues, "anAnimation.thePositionAnimationXValues")
						//	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(1), anAnimationDesc.frameCount, anAnimation.thePositionAnimationYValues, "anAnimation.thePositionAnimationYValues")
						//	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(2), anAnimationDesc.frameCount, anAnimation.thePositionAnimationZValues, "anAnimation.thePositionAnimationZValues")

						//	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
						//Else
						//	anAnimation.thePosition = New SourceVector()
						//	anAnimation.thePosition.x = Me.theInputFileReader.ReadSingle()
						//	anAnimation.thePosition.y = Me.theInputFileReader.ReadSingle()
						//	anAnimation.thePosition.z = Me.theInputFileReader.ReadSingle()
						//End If
						//If (anAnimation.flags And SourceMdlAnimation2531.STUDIO_ROT_ANIMATED) > 0 Then
						//	anAnimation.theOffsets(3) = Me.theInputFileReader.ReadInt32()
						//	anAnimation.theOffsets(4) = Me.theInputFileReader.ReadInt32()
						//	anAnimation.theOffsets(5) = Me.theInputFileReader.ReadInt32()

						//	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//	anAnimation.theRotationAnimationXValues = New List(Of SourceMdlAnimationValue2531)()
						//	anAnimation.theRotationAnimationYValues = New List(Of SourceMdlAnimationValue2531)()
						//	anAnimation.theRotationAnimationZValues = New List(Of SourceMdlAnimationValue2531)()
						//	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(3), anAnimationDesc.frameCount, anAnimation.theRotationAnimationXValues, "anAnimation.theRotationAnimationXValues")
						//	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(4), anAnimationDesc.frameCount, anAnimation.theRotationAnimationYValues, "anAnimation.theRotationAnimationYValues")
						//	Me.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets(5), anAnimationDesc.frameCount, anAnimation.theRotationAnimationZValues, "anAnimation.theRotationAnimationZValues")

						//	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
						//Else
						//	anAnimation.theRotation = New SourceQuaternion()
						//	anAnimation.theRotation.x = Me.theInputFileReader.ReadSingle()
						//	anAnimation.theRotation.y = Me.theInputFileReader.ReadSingle()
						//	anAnimation.theRotation.z = Me.theInputFileReader.ReadSingle()
						//	anAnimation.theRotation.w = Me.theInputFileReader.ReadSingle()
						//End If
						//------
						anAnimation.unknown = theInputFileReader.ReadSingle();
						anAnimation.theOffsets[0] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[1] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[2] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[3] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[4] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[5] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[6] = theInputFileReader.ReadInt32();

						anAnimationDesc.theAnimations.Add(anAnimation);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation")

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (anAnimation.theOffsets[0] > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[0], anAnimationDesc.frameCount, anAnimation.thePositionAnimationXValues, "anAnimation.thePositionAnimationXValues");
						}
						if (anAnimation.theOffsets[1] > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[1], anAnimationDesc.frameCount, anAnimation.thePositionAnimationYValues, "anAnimation.thePositionAnimationYValues");
						}
						if (anAnimation.theOffsets[2] > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[2], anAnimationDesc.frameCount, anAnimation.thePositionAnimationZValues, "anAnimation.thePositionAnimationZValues");
						}

						if (anAnimation.theOffsets[3] > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[3], anAnimationDesc.frameCount, anAnimation.theRotationAnimationXValues, "anAnimation.theRotationAnimationXValues");
						}
						if (anAnimation.theOffsets[4] > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[4], anAnimationDesc.frameCount, anAnimation.theRotationAnimationYValues, "anAnimation.theRotationAnimationYValues");
						}
						if (anAnimation.theOffsets[5] > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[5], anAnimationDesc.frameCount, anAnimation.theRotationAnimationZValues, "anAnimation.theRotationAnimationZValues");
						}
						if (anAnimation.theOffsets[6] > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[6], anAnimationDesc.frameCount, anAnimation.theRotationAnimationWValues, "anAnimation.theRotationAnimationWValues");
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations " + anAnimationDesc.theAnimations.Count.ToString());

					//Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimationDesc.theMeshes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadAnimationValues(long animationValuesInputFileStreamPosition, int frameCount, List<SourceMdlAnimationValue2531> animationValues, string debugDescription)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			int frameCountRemainingToBeChecked = 0;
			SourceMdlAnimationValue2531 anAnimationValue = new SourceMdlAnimationValue2531();
			byte currentTotal = 0;
			byte validCount = 0;

			try
			{
				theInputFileReader.BaseStream.Seek(animationValuesInputFileStreamPosition, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				frameCountRemainingToBeChecked = frameCount;
				while (frameCountRemainingToBeChecked > 0)
				{
					anAnimationValue.value = theInputFileReader.ReadInt16();
					currentTotal = anAnimationValue.total;
					if (currentTotal == 0)
					{
						int badIfThisIsReached = 42;
						break;
					}
					frameCountRemainingToBeChecked -= currentTotal;
					animationValues.Add(anAnimationValue);

					validCount = anAnimationValue.valid;
					for (int i = 1; i <= validCount; i++)
					{
						anAnimationValue.value = theInputFileReader.ReadInt16();
						animationValues.Add(anAnimationValue);
					}
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, debugDescription);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private double GetSplit(SourceMdlFlex2531 aFlex, int meshVertexIndexStart)
		{
			//TODO: Reverse these calculations to get split number.
			//      Yikes! This really should be run over *all* vertex anims to get the exact split number.
			//float scale = 1.0;
			//float side = 0.0;
			//if (g_flexkey[i].split > 0)
			//{
			//	if (psrcanim->pos.x > g_flexkey[i].split) 
			//	{
			//		scale = 0;
			//	}
			//	else if (psrcanim->pos.x < -g_flexkey[i].split) 
			//	{
			//		scale = 1.0;
			//	}
			//	else
			//	{
			//		float t = (g_flexkey[i].split - psrcanim->pos.x) / (2.0 * g_flexkey[i].split);
			//		scale = 3 * t * t - 2 * t * t * t;
			//	}
			//}
			//else if (g_flexkey[i].split < 0)
			//{
			//	if (psrcanim->pos.x < g_flexkey[i].split) 
			//	{
			//		scale = 0;
			//	}
			//	else if (psrcanim->pos.x > -g_flexkey[i].split) 
			//	{
			//		scale = 1.0;
			//	}
			//	else
			//	{
			//		float t = (g_flexkey[i].split - psrcanim->pos.x) / (2.0 * g_flexkey[i].split);
			//		scale = 3 * t * t - 2 * t * t * t;
			//	}
			//}
			//side = 1.0 - scale;
			//pvertanim->side  = 255.0F*pvanim->side;



			//Dim aVertex As SourceVertex
			//Dim vertexIndex As Integer
			//Dim aVertAnim As SourceMdlVertAnim
			//Dim side As Double
			//Dim scale As Double
			//Dim split As Double
			//aVertAnim = aFlex.theVertAnims(0)
			//vertexIndex = aVertAnim.index + meshVertexIndexStart
			//If Me.theSourceEngineModel.theVvdFileHeader.fixupCount = 0 Then
			//	aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(vertexIndex)
			//Else
			//	'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
			//	'      Maybe the listing by lodIndex is only needed internally by graphics engine.
			//	'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
			//	aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(vertexIndex)
			//End If
			//side = aVertAnim.side / 255
			//scale = 1 - side
			//If scale = 1 Then
			//	split = -(aVertex.positionX - 1)
			//ElseIf scale = 0 Then
			//Else
			//End If

			return 1;
		}

#endregion

#region Data

		protected BinaryReader theInputFileReader;
		protected BinaryWriter theOutputFileWriter;

		protected SourceMdlFileData2531 theMdlFileData;

#endregion

	}

}