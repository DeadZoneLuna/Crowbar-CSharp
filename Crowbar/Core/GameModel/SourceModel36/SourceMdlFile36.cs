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
	public class SourceMdlFile36
	{

#region Creation and Destruction

		public SourceMdlFile36(BinaryReader mdlFileReader, SourceMdlFileData36 mdlFileData)
		{
			theInputFileReader = mdlFileReader;
			theMdlFileData = mdlFileData;

			theMdlFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile36(BinaryWriter mdlFileWriter, SourceMdlFileData36 mdlFileData)
		{
			theOutputFileWriter = mdlFileWriter;
			theMdlFileData = mdlFileData;
		}

#endregion

#region Methods

		public void ReadMdlHeader00(string logDescription)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theMdlFileData.id = theInputFileReader.ReadChars(4);
			theMdlFileData.theID = new string(theMdlFileData.id);
			theMdlFileData.version = theInputFileReader.ReadInt32();

			theMdlFileData.checksum = theInputFileReader.ReadInt32();

			theMdlFileData.name = theInputFileReader.ReadChars(theMdlFileData.name.Length);
			theMdlFileData.theModelName = (new string(theMdlFileData.name)).Trim('\0');

			theMdlFileData.fileSize = theInputFileReader.ReadInt32();
			theMdlFileData.theActualFileSize = theInputFileReader.BaseStream.Length;

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			if (!string.IsNullOrEmpty(logDescription))
			{
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription + " (MDL version: " + theMdlFileData.version.ToString() + ")");
			}
		}

		public void ReadMdlHeader01(string logDescription)
		{
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			long fileOffsetStart2 = 0;
			long fileOffsetEnd2 = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theMdlFileData.eyePosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.eyePosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.eyePosition.z = theInputFileReader.ReadSingle();

			theMdlFileData.illuminationPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.z = theInputFileReader.ReadSingle();

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

			theMdlFileData.boneCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneOffset = theInputFileReader.ReadInt32();
			theMdlFileData.boneControllerCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneControllerOffset = theInputFileReader.ReadInt32();

			theMdlFileData.hitboxSetCount = theInputFileReader.ReadInt32();
			theMdlFileData.hitboxSetOffset = theInputFileReader.ReadInt32();

			theMdlFileData.animationCount = theInputFileReader.ReadInt32();
			theMdlFileData.animationOffset = theInputFileReader.ReadInt32();
			//Me.theMdlFileData.animationGroupCount = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.animationGroupOffset = Me.theInputFileReader.ReadInt32()

			//Me.theMdlFileData.boneDescCount = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.boneDescOffset = Me.theInputFileReader.ReadInt32()

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

			if (theMdlFileData.surfacePropOffset > 0)
			{
				inputFileStreamPosition = theInputFileReader.BaseStream.Position;
				theInputFileReader.BaseStream.Seek(theMdlFileData.surfacePropOffset, SeekOrigin.Begin);
				fileOffsetStart2 = theInputFileReader.BaseStream.Position;

				theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(theInputFileReader);

				fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
				if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
				{
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName = " + theMdlFileData.theSurfacePropName);
				}
				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}
			else
			{
				theMdlFileData.theSurfacePropName = "";
			}

			theMdlFileData.keyValueOffset = theInputFileReader.ReadInt32();
			theMdlFileData.keyValueSize = theInputFileReader.ReadInt32();

			theMdlFileData.localIkAutoPlayLockCount = theInputFileReader.ReadInt32();
			theMdlFileData.localIkAutoPlayLockOffset = theInputFileReader.ReadInt32();

			theMdlFileData.mass = theInputFileReader.ReadSingle();
			theMdlFileData.contents = theInputFileReader.ReadInt32();

			for (int x = 0; x < theMdlFileData.unused.Length; x++)
			{
				theMdlFileData.unused[x] = theInputFileReader.ReadInt32();
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription);

			if (theMdlFileData.bodyPartCount == 0 && theMdlFileData.localSequenceCount > 0)
			{
				theMdlFileData.theMdlFileOnlyHasAnimations = true;
			}
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
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theBones = new List<SourceMdlBone37>(theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < theMdlFileData.boneCount; boneIndex++)
					{
						boneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlBone37 aBone = new SourceMdlBone37();

						aBone.nameOffset = theInputFileReader.ReadInt32();
						aBone.parentBoneIndex = theInputFileReader.ReadInt32();

						for (int j = 0; j < aBone.boneControllerIndex.Length; j++)
						{
							aBone.boneControllerIndex[j] = theInputFileReader.ReadInt32();
						}

						aBone.position = new SourceVector();
						aBone.position.x = theInputFileReader.ReadSingle();
						aBone.position.y = theInputFileReader.ReadSingle();
						aBone.position.z = theInputFileReader.ReadSingle();
						aBone.rotation = new SourceVector();
						aBone.rotation.x = theInputFileReader.ReadSingle();
						aBone.rotation.y = theInputFileReader.ReadSingle();
						aBone.rotation.z = theInputFileReader.ReadSingle();
						aBone.positionScale = new SourceVector();
						aBone.positionScale.x = theInputFileReader.ReadSingle();
						aBone.positionScale.y = theInputFileReader.ReadSingle();
						aBone.positionScale.z = theInputFileReader.ReadSingle();
						aBone.rotationScale = new SourceVector();
						aBone.rotationScale.x = theInputFileReader.ReadSingle();
						aBone.rotationScale.y = theInputFileReader.ReadSingle();
						aBone.rotationScale.z = theInputFileReader.ReadSingle();

						aBone.poseToBoneColumn0 = new SourceVector();
						aBone.poseToBoneColumn1 = new SourceVector();
						aBone.poseToBoneColumn2 = new SourceVector();
						aBone.poseToBoneColumn3 = new SourceVector();
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

						aBone.qAlignment = new SourceQuaternion();
						aBone.qAlignment.x = theInputFileReader.ReadSingle();
						aBone.qAlignment.y = theInputFileReader.ReadSingle();
						aBone.qAlignment.z = theInputFileReader.ReadSingle();
						aBone.qAlignment.w = theInputFileReader.ReadSingle();

						aBone.flags = theInputFileReader.ReadInt32();

						aBone.proceduralRuleType = theInputFileReader.ReadInt32();
						aBone.proceduralRuleOffset = theInputFileReader.ReadInt32();
						aBone.physicsBoneIndex = theInputFileReader.ReadInt32();
						aBone.surfacePropNameOffset = theInputFileReader.ReadInt32();

						aBone.quat = new SourceQuaternion();
						aBone.quat.x = theInputFileReader.ReadSingle();
						aBone.quat.y = theInputFileReader.ReadSingle();
						aBone.quat.z = theInputFileReader.ReadSingle();
						aBone.quat.w = theInputFileReader.ReadSingle();

						aBone.contents = theInputFileReader.ReadInt32();

						for (int x = 0; x < aBone.unused.Length; x++)
						{
							aBone.unused[x] = theInputFileReader.ReadInt32();
						}

						theMdlFileData.theBones.Add(aBone);

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
							if (aBone.proceduralRuleType == SourceMdlBone37.STUDIO_PROC_AXISINTERP)
							{
								ReadAxisInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone37.STUDIO_PROC_QUATINTERP)
							{
								theMdlFileData.theProceduralBonesCommandIsUsed = true;
								ReadQuatInterpBone(boneInputFileStreamPosition, aBone);
								//ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
								//	Me.ReadJiggleBone(boneInputFileStreamPosition, aBone)
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

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones " + theMdlFileData.theBones.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment");
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
				long boneControllerInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theBoneControllers = new List<SourceMdlBoneController37>(theMdlFileData.boneControllerCount);
					for (int i = 0; i < theMdlFileData.boneControllerCount; i++)
					{
						boneControllerInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlBoneController37 aBoneController = new SourceMdlBoneController37();

						aBoneController.boneIndex = theInputFileReader.ReadInt32();
						aBoneController.type = theInputFileReader.ReadInt32();
						aBoneController.startBlah = theInputFileReader.ReadSingle();
						aBoneController.endBlah = theInputFileReader.ReadSingle();
						aBoneController.restIndex = theInputFileReader.ReadInt32();
						aBoneController.inputField = theInputFileReader.ReadInt32();
						for (int x = 0; x < aBoneController.unused.Length; x++)
						{
							aBoneController.unused[x] = theInputFileReader.ReadByte();
						}

						theMdlFileData.theBoneControllers.Add(aBoneController);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//If aBoneController.nameOffset <> 0 Then
						//	Me.theInputFileReader.BaseStream.Seek(boneControllerInputFileStreamPosition + aBoneController.nameOffset, SeekOrigin.Begin)
						//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						//	aBoneController.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName")
						//	End If
						//Else
						//	aBoneController.theName = ""
						//End If

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + theMdlFileData.theBoneControllers.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment");
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

					theMdlFileData.theAttachments = new List<SourceMdlAttachment37>(theMdlFileData.localAttachmentCount);
					for (int i = 0; i < theMdlFileData.localAttachmentCount; i++)
					{
						attachmentInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlAttachment37 anAttachment = new SourceMdlAttachment37();

						anAttachment.nameOffset = theInputFileReader.ReadInt32();
						anAttachment.type = theInputFileReader.ReadInt32();
						anAttachment.boneIndex = theInputFileReader.ReadInt32();
						anAttachment.localM11 = theInputFileReader.ReadSingle();
						anAttachment.localM12 = theInputFileReader.ReadSingle();
						anAttachment.localM13 = theInputFileReader.ReadSingle();
						anAttachment.localM14 = theInputFileReader.ReadSingle();
						anAttachment.localM21 = theInputFileReader.ReadSingle();
						anAttachment.localM22 = theInputFileReader.ReadSingle();
						anAttachment.localM23 = theInputFileReader.ReadSingle();
						anAttachment.localM24 = theInputFileReader.ReadSingle();
						anAttachment.localM31 = theInputFileReader.ReadSingle();
						anAttachment.localM32 = theInputFileReader.ReadSingle();
						anAttachment.localM33 = theInputFileReader.ReadSingle();
						anAttachment.localM34 = theInputFileReader.ReadSingle();

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
						else
						{
							anAttachment.theName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + Me.theMdlFileData.theAttachments.Count.ToString())

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadHitboxSets()
		{
			if (theMdlFileData.hitboxSetCount > 0)
			{
				long hitboxSetInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.hitboxSetOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theHitboxSets = new List<SourceMdlHitboxSet37>(theMdlFileData.hitboxSetCount);
					for (int i = 0; i < theMdlFileData.hitboxSetCount; i++)
					{
						hitboxSetInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlHitboxSet37 aHitboxSet = new SourceMdlHitboxSet37();

						aHitboxSet.nameOffset = theInputFileReader.ReadInt32();
						aHitboxSet.hitboxCount = theInputFileReader.ReadInt32();
						aHitboxSet.hitboxOffset = theInputFileReader.ReadInt32();

						theMdlFileData.theHitboxSets.Add(aHitboxSet);

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

						ReadHitboxes(hitboxSetInputFileStreamPosition, aHitboxSet);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets " + theMdlFileData.theHitboxSets.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment");
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

					theMdlFileData.theSequenceDescs = new List<SourceMdlSequenceDesc36>(theMdlFileData.localSequenceCount);
					for (int i = 0; i < theMdlFileData.localSequenceCount; i++)
					{
						seqInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlSequenceDesc36 aSeqDesc = new SourceMdlSequenceDesc36();

						aSeqDesc.nameOffset = theInputFileReader.ReadInt32();
						aSeqDesc.activityNameOffset = theInputFileReader.ReadInt32();
						aSeqDesc.flags = theInputFileReader.ReadInt32();
						aSeqDesc.activity = theInputFileReader.ReadInt32();
						aSeqDesc.activityWeight = theInputFileReader.ReadInt32();
						aSeqDesc.eventCount = theInputFileReader.ReadInt32();
						aSeqDesc.eventOffset = theInputFileReader.ReadInt32();

						aSeqDesc.bbMin.x = theInputFileReader.ReadSingle();
						aSeqDesc.bbMin.y = theInputFileReader.ReadSingle();
						aSeqDesc.bbMin.z = theInputFileReader.ReadSingle();
						aSeqDesc.bbMax.x = theInputFileReader.ReadSingle();
						aSeqDesc.bbMax.y = theInputFileReader.ReadSingle();
						aSeqDesc.bbMax.z = theInputFileReader.ReadSingle();

						aSeqDesc.blendCount = theInputFileReader.ReadInt32();

						//For x As Integer = 0 To aSequence.anim.Length - 1
						//	aSequence.anim(x) = Me.theInputFileReader.ReadInt16()
						//Next
						for (int rowIndex = 0; rowIndex < SourceModule2531.MAXSTUDIOBLENDS; rowIndex++)
						{
							for (int columnIndex = 0; columnIndex < SourceModule2531.MAXSTUDIOBLENDS; columnIndex++)
							{
								aSeqDesc.anim[rowIndex][columnIndex] = theInputFileReader.ReadInt16();
							}
						}

						aSeqDesc.movementIndex = theInputFileReader.ReadInt32();
						aSeqDesc.groupSize[0] = theInputFileReader.ReadInt32();
						aSeqDesc.groupSize[1] = theInputFileReader.ReadInt32();
						aSeqDesc.paramIndex[0] = theInputFileReader.ReadInt32();
						aSeqDesc.paramIndex[1] = theInputFileReader.ReadInt32();
						aSeqDesc.paramStart[0] = theInputFileReader.ReadSingle();
						aSeqDesc.paramStart[1] = theInputFileReader.ReadSingle();
						aSeqDesc.paramEnd[0] = theInputFileReader.ReadSingle();
						aSeqDesc.paramEnd[1] = theInputFileReader.ReadSingle();
						aSeqDesc.paramParent = theInputFileReader.ReadInt32();

						aSeqDesc.sequenceGroup = theInputFileReader.ReadInt32();

						aSeqDesc.fadeInTime = theInputFileReader.ReadSingle();
						aSeqDesc.fadeOutTime = theInputFileReader.ReadSingle();

						aSeqDesc.entryNodeIndex = theInputFileReader.ReadInt32();
						aSeqDesc.exitNodeIndex = theInputFileReader.ReadInt32();
						aSeqDesc.nodeFlags = theInputFileReader.ReadInt32();

						aSeqDesc.entryPhase = theInputFileReader.ReadSingle();
						aSeqDesc.exitPhase = theInputFileReader.ReadSingle();
						aSeqDesc.lastFrame = theInputFileReader.ReadSingle();

						aSeqDesc.nextSeq = theInputFileReader.ReadInt32();
						aSeqDesc.pose = theInputFileReader.ReadInt32();

						aSeqDesc.ikRuleCount = theInputFileReader.ReadInt32();
						aSeqDesc.autoLayerCount = theInputFileReader.ReadInt32();
						aSeqDesc.autoLayerOffset = theInputFileReader.ReadInt32();
						aSeqDesc.weightOffset = theInputFileReader.ReadInt32();
						aSeqDesc.poseKeyOffset = theInputFileReader.ReadInt32();

						aSeqDesc.ikLockCount = theInputFileReader.ReadInt32();
						aSeqDesc.ikLockOffset = theInputFileReader.ReadInt32();
						aSeqDesc.keyValueOffset = theInputFileReader.ReadInt32();
						aSeqDesc.keyValueSize = theInputFileReader.ReadInt32();

						for (int x = 0; x < aSeqDesc.unused.Length; x++)
						{
							aSeqDesc.unused[x] = theInputFileReader.ReadInt32();
						}

						//NOTE: Not sure why these bytes were ever included; they are always zeroes.
						inputFileStreamPosition = theInputFileReader.BaseStream.Position;
						theInputFileReader.BaseStream.Seek(inputFileStreamPosition + 3584, SeekOrigin.Begin);

						theMdlFileData.theSequenceDescs.Add(aSeqDesc);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aSeqDesc.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aSeqDesc.theName = FileManager.ReadNullTerminatedString(theInputFileReader);
							if (string.IsNullOrEmpty(aSeqDesc.theName))
							{
								if (theMdlFileData.localSequenceCount == 1)
								{
									aSeqDesc.theName = "idle";
								}
								else
								{
									aSeqDesc.theName = "";
								}
							}

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theName = " + aSeqDesc.theName);
						}
						else
						{
							aSeqDesc.theName = "";
						}

						if (aSeqDesc.activityNameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aSeqDesc.theActivityName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName = " + aSeqDesc.theActivityName);
							}
						}
						else
						{
							aSeqDesc.theActivityName = "";
						}

						//NOTE: MDL35 and MDL36 aSeqDesc.eventOffset is really "events plus weights offset", so set stream position here for weights in case there are no events.
						theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin);
						//Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
						ReadEvents(seqInputFileStreamPosition, aSeqDesc);
						//Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
						//'Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)
						//NOTE: MDL35 and MDL36 only use events and weights, and the weights are always immediately after events.
						ReadMdlAnimBoneWeights(theInputFileReader.BaseStream.Position, aSeqDesc);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc");
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs " + Me.theMdlFileData.theSequenceDescs.Count.ToString())
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
				long sequenceGroupInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.sequenceGroupOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theSequenceGroups = new List<SourceMdlSequenceGroup37>(theMdlFileData.sequenceGroupCount);
					for (int sequenceGroupIndex = 0; sequenceGroupIndex < theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
					{
						sequenceGroupInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlSequenceGroup37 aSequenceGroup = new SourceMdlSequenceGroup37();

						aSequenceGroup.nameOffset = theInputFileReader.ReadInt32();
						aSequenceGroup.fileNameOffset = theInputFileReader.ReadInt32();
						aSequenceGroup.cacheOffset = theInputFileReader.ReadInt32();
						aSequenceGroup.data = theInputFileReader.ReadInt32();

						if (theMdlFileData.version == 35)
						{
							for (int x = 0; x < aSequenceGroup.unknown.Length; x++)
							{
								aSequenceGroup.unknown[x] = theInputFileReader.ReadInt32();
							}
						}

						theMdlFileData.theSequenceGroups.Add(aSequenceGroup);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aSequenceGroup.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(sequenceGroupInputFileStreamPosition + aSequenceGroup.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aSequenceGroup.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequenceGroup.theName = " + aSequenceGroup.theName);
						}
						else
						{
							aSequenceGroup.theName = "";
						}

						if (aSequenceGroup.fileNameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(sequenceGroupInputFileStreamPosition + aSequenceGroup.fileNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aSequenceGroup.theFileName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequenceGroup.theFileName = " + aSequenceGroup.theFileName);
							}
						}
						else
						{
							aSequenceGroup.theFileName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceGroups " + theMdlFileData.theSequenceGroups.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSequenceGroups alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadTransitions()
		{
			if (theMdlFileData.transitionCount > 0)
			{
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.transitionOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theTransitions = new List<List<int>>(theMdlFileData.transitionCount);
					for (int entryNodeIndex = 0; entryNodeIndex < theMdlFileData.transitionCount; entryNodeIndex++)
					{
						List<int> exitNodeTransitions = new List<int>(theMdlFileData.transitionCount);
						for (int exitNodeIndex = 0; exitNodeIndex < theMdlFileData.transitionCount; exitNodeIndex++)
						{
							int aTransitionValue = theInputFileReader.ReadByte();


							exitNodeTransitions.Add(aTransitionValue);
						}
						theMdlFileData.theTransitions.Add(exitNodeTransitions);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTransitions " + theMdlFileData.theTransitions.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTransitions alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadLocalAnimationDescs()
		{
			if (theMdlFileData.animationCount > 0)
			{
				long animationDescInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.animationOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theAnimationDescs = new List<SourceMdlAnimationDesc36>(theMdlFileData.animationCount);
					for (int i = 0; i < theMdlFileData.animationCount; i++)
					{
						animationDescInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlAnimationDesc36 anAnimationDesc = new SourceMdlAnimationDesc36();

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

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc")

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (anAnimationDesc.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							anAnimationDesc.theName = FileManager.ReadNullTerminatedString(theInputFileReader);
							//If Me.theMdlFileData.theFirstAnimationDesc Is Nothing AndAlso anAnimationDesc.theName(0) <> "@" Then
							//	Me.theMdlFileData.theFirstAnimationDesc = anAnimationDesc
							//End If
							if (anAnimationDesc.theName[0] == '@')
							{
								anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 1);
							}
							if (string.IsNullOrEmpty(anAnimationDesc.theName))
							{
								if (theMdlFileData.animationCount == 1)
								{
									anAnimationDesc.theName = "a_idle";
								}
								else
								{
									anAnimationDesc.theName = "";
								}
							}

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
						ReadMdlMovements(animationDescInputFileStreamPosition, anAnimationDesc);
						ReadMdlIkRules(animationDescInputFileStreamPosition, anAnimationDesc);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs " + theMdlFileData.theAnimationDescs.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAnimationDescs alignment");
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

					theMdlFileData.theBodyParts = new List<SourceMdlBodyPart37>(theMdlFileData.bodyPartCount);
					for (int i = 0; i < theMdlFileData.bodyPartCount; i++)
					{
						bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart37 aBodyPart = new SourceMdlBodyPart37();

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
						//NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts *and* models.
						if (i == theMdlFileData.bodyPartCount - 1)
						{
							theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment");
						}

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

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.flexDescOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theFlexDescs = new List<SourceMdlFlexDesc>(theMdlFileData.flexDescCount);
					for (int i = 0; i < theMdlFileData.flexDescCount; i++)
					{
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
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs " + theMdlFileData.theFlexDescs.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexDescs alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
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

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.flexControllerOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theFlexControllers = new List<SourceMdlFlexController>(theMdlFileData.flexControllerCount);
					for (int i = 0; i < theMdlFileData.flexControllerCount; i++)
					{
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
					}

					//If Me.theMdlFileData.theFlexControllers.Count > 0 Then
					//	Me.theMdlFileData.theModelCommandIsUsed = True
					//End If

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers " + theMdlFileData.theFlexControllers.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexControllers alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
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

				try
				{
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
						ReadFlexOps(flexRuleInputFileStreamPosition, aFlexRule);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//If Me.theMdlFileData.theFlexRules.Count > 0 Then
					//	Me.theMdlFileData.theModelCommandIsUsed = True
					//End If

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + theMdlFileData.theFlexRules.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexRules alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadIkChains()
		{
			if (theMdlFileData.ikChainCount > 0)
			{
				long ikChainInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.ikChainOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theIkChains = new List<SourceMdlIkChain37>(theMdlFileData.ikChainCount);
					for (int i = 0; i < theMdlFileData.ikChainCount; i++)
					{
						ikChainInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlIkChain37 anIkChain = new SourceMdlIkChain37();

						anIkChain.nameOffset = theInputFileReader.ReadInt32();
						anIkChain.linkType = theInputFileReader.ReadInt32();
						anIkChain.linkCount = theInputFileReader.ReadInt32();
						anIkChain.linkOffset = theInputFileReader.ReadInt32();

						theMdlFileData.theIkChains.Add(anIkChain);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (anIkChain.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							anIkChain.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkChain.theName = " + anIkChain.theName);
							}
						}
						else
						{
							anIkChain.theName = "";
						}

						ReadIkLinks(ikChainInputFileStreamPosition, anIkChain);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkChains " + theMdlFileData.theIkChains.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theIkChains alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadIkLocks()
		{
			if (theMdlFileData.localIkAutoPlayLockCount > 0)
			{
				//Dim ikChainInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theIkLocks = new List<SourceMdlIkLock37>(theMdlFileData.localIkAutoPlayLockCount);
					for (int i = 0; i < theMdlFileData.localIkAutoPlayLockCount; i++)
					{
						//ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlIkLock37 anIkLock = new SourceMdlIkLock37();

						anIkLock.chainIndex = theInputFileReader.ReadInt32();
						anIkLock.posWeight = theInputFileReader.ReadSingle();
						anIkLock.localQWeight = theInputFileReader.ReadSingle();

						theMdlFileData.theIkLocks.Add(anIkLock);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks " + theMdlFileData.theIkLocks.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theIkLocks alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadMouths()
		{
			if (theMdlFileData.mouthCount > 0)
			{
				//Dim mouthInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.mouthOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theMouths = new List<SourceMdlMouth>(theMdlFileData.mouthCount);
					for (int i = 0; i < theMdlFileData.mouthCount; i++)
					{
						//mouthInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlMouth aMouth = new SourceMdlMouth();
						aMouth.boneIndex = theInputFileReader.ReadInt32();
						aMouth.forward.x = theInputFileReader.ReadSingle();
						aMouth.forward.y = theInputFileReader.ReadSingle();
						aMouth.forward.z = theInputFileReader.ReadSingle();
						aMouth.flexDescIndex = theInputFileReader.ReadInt32();
						theMdlFileData.theMouths.Add(aMouth);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					if (theMdlFileData.theMouths.Count > 0)
					{
						//Me.theMdlFileData.theModelCommandIsUsed = True
						// Seems like any $model can have these lines, so simply assign them to first one.
						theMdlFileData.theBodyParts[0].theModelCommandIsUsed = true;
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMouths " + theMdlFileData.theMouths.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theMouths alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
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
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.thePoseParamDescs = new List<SourceMdlPoseParamDesc>(theMdlFileData.localPoseParamaterCount);
					for (int i = 0; i < theMdlFileData.localPoseParamaterCount; i++)
					{
						poseInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlPoseParamDesc aPoseParamDesc = new SourceMdlPoseParamDesc();
						aPoseParamDesc.nameOffset = theInputFileReader.ReadInt32();
						aPoseParamDesc.flags = theInputFileReader.ReadInt32();
						aPoseParamDesc.startingValue = theInputFileReader.ReadSingle();
						aPoseParamDesc.endingValue = theInputFileReader.ReadSingle();
						aPoseParamDesc.loopingRange = theInputFileReader.ReadSingle();
						theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc);

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

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs " + theMdlFileData.thePoseParamDescs.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.thePoseParamDescs alignment");
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

				theInputFileReader.BaseStream.Seek(theMdlFileData.textureOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theTextures = new List<SourceMdlTexture37>(theMdlFileData.textureCount);
				for (int i = 0; i < theMdlFileData.textureCount; i++)
				{
					textureInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlTexture37 aTexture = new SourceMdlTexture37();

					aTexture.fileNameOffset = theInputFileReader.ReadInt32();
					aTexture.flags = theInputFileReader.ReadInt32();
					aTexture.width = theInputFileReader.ReadSingle();
					aTexture.height = theInputFileReader.ReadSingle();
					aTexture.worldUnitsPerU = theInputFileReader.ReadSingle();
					aTexture.worldUnitsPerV = theInputFileReader.ReadSingle();
					for (int x = 0; x < aTexture.unknown.Length; x++)
					{
						aTexture.unknown[x] = theInputFileReader.ReadInt32();
					}

					theMdlFileData.theTextures.Add(aTexture);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aTexture.fileNameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.fileNameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aTexture.theFileName = FileManager.ReadNullTerminatedString(theInputFileReader);

						// Convert all forward slashes to backward slashes.
						aTexture.theFileName = FileManager.GetNormalizedPathFileName(aTexture.theFileName);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theName = " + aTexture.theFileName);
						}
					}
					else
					{
						aTexture.theFileName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures");

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment");
			}
		}

		public void ReadTexturePaths()
		{
			if (theMdlFileData.texturePathCount > 0)
			{
				theInputFileReader.BaseStream.Seek(theMdlFileData.texturePathOffset, SeekOrigin.Begin);
				long fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theTexturePaths = new List<string>(theMdlFileData.texturePathCount);

				string aTexturePath = string.Empty;
				for (int i = 0; i < theMdlFileData.texturePathCount; i++)
				{
					int texturePathOffset = theInputFileReader.ReadInt32();
					long inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (texturePathOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin);
						long fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						//TEST: Convert all forward slashes to backward slashes.
						aTexturePath = FileManager.GetNormalizedPathFileName(FileManager.ReadNullTerminatedString(theInputFileReader));

						long fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath = " + aTexturePath);
					}
					theMdlFileData.theTexturePaths.Add(aTexturePath);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				long fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths");

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTexturePaths alignment");
			}
		}

		public void ReadSkinFamilies()
		{
			if (theMdlFileData.skinFamilyCount > 0 && theMdlFileData.skinReferenceCount > 0)
			{
				long skinFamilyInputFileStreamPosition = 0;
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				theInputFileReader.BaseStream.Seek(theMdlFileData.skinOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theSkinFamilies = new List<List<short>>(theMdlFileData.skinFamilyCount);
				for (int i = 0; i < theMdlFileData.skinFamilyCount; i++)
				{
					skinFamilyInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					List<short> aSkinFamily = new List<short>();

					for (int j = 0; j < theMdlFileData.skinReferenceCount; j++)
					{
						short aSkinRef = theInputFileReader.ReadInt16();
						aSkinFamily.Add(aSkinRef);
					}

					theMdlFileData.theSkinFamilies.Add(aSkinFamily);

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

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies");

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment");
			}
		}

		public void ReadKeyValues()
		{
			if (theMdlFileData.keyValueSize > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				char nullChar = '\0';

				theInputFileReader.BaseStream.Seek(theMdlFileData.keyValueOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				//NOTE: Use -1 to drop the null terminator character.
				theMdlFileData.theKeyValuesText = new string(theInputFileReader.ReadChars(theMdlFileData.keyValueSize - 1));
				//NOTE: Read the null terminator character.
				nullChar = theInputFileReader.ReadChar();

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theKeyValuesText = " + theMdlFileData.theKeyValuesText);

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theKeyValuesText alignment");
			}
		}

		//Public Sub ReadFinalBytesAlignment()
		//	Me.theMdlFileData.theFileSeekLog.LogAndAlignFromFileSeekLogEnd(Me.theInputFileReader, 4, "Final bytes alignment")
		//End Sub

		public void ReadUnreadBytes()
		{
			theMdlFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

		public void PostProcess()
		{
			if (theMdlFileData.theBodyParts != null)
			{
				foreach (SourceMdlBodyPart37 aBodyPart in theMdlFileData.theBodyParts)
				{
					foreach (SourceMdlModel37 aBodyModel in aBodyPart.theModels)
					{
						if (aBodyModel.theEyeballs != null && aBodyModel.theEyeballs.Count > 0)
						{
							aBodyPart.theModelCommandIsUsed = true;
							aBodyPart.theEyeballOptionIsUsed = true;
							break;
						}

						if (aBodyModel.theMeshes != null)
						{
							foreach (SourceMdlMesh37 aMesh in aBodyModel.theMeshes)
							{
								if (aMesh.theFlexes != null && aMesh.theFlexes.Count > 0)
								{
									aBodyPart.theModelCommandIsUsed = true;
									break;
								}
							}
							if (aBodyPart.theModelCommandIsUsed)
							{
								break;
							}
						}
					}
				}
			}
		}

		public void CreateFlexFrameList()
		{
			FlexFrame37 aFlexFrame = null;
			SourceMdlBodyPart37 aBodyPart = null;
			SourceMdlModel37 aModel = null;
			SourceMdlMesh37 aMesh = null;
			SourceMdlFlex37 aFlex = null;
			FlexFrame37 searchedFlexFrame = null;

			//Me.theMdlFileData.theFlexFrames = New List(Of FlexFrame37)()

			//'NOTE: Create the defaultflex.
			//aFlexFrame = New FlexFrame37()
			//Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)

			if (theMdlFileData.theFlexDescs != null && theMdlFileData.theFlexDescs.Count > 0)
			{
				//Dim flexDescToMeshIndexes As List(Of List(Of Integer))
				List<List<FlexFrame37>> flexDescToFlexFrames = null;
				int meshVertexIndexStart = 0;
				int cumulativebodyPartVertexIndexStart = 0;

				//flexDescToMeshIndexes = New List(Of List(Of Integer))(Me.theMdlFileData.theFlexDescs.Count)
				//For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
				//	Dim meshIndexList As New List(Of Integer)()
				//	flexDescToMeshIndexes.Add(meshIndexList)
				//Next

				flexDescToFlexFrames = new List<List<FlexFrame37>>(theMdlFileData.theFlexDescs.Count);
				for (int x = 0; x < theMdlFileData.theFlexDescs.Count; x++)
				{
					List<FlexFrame37> flexFrameList = new List<FlexFrame37>();
					flexDescToFlexFrames.Add(flexFrameList);
				}

				cumulativebodyPartVertexIndexStart = 0;
				for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];

					aBodyPart.theFlexFrames = new List<FlexFrame37>();
					//NOTE: Create the defaultflex.
					aFlexFrame = new FlexFrame37();
					aBodyPart.theFlexFrames.Add(aFlexFrame);

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
														// Add to an existing flexFrame.
														aFlexFrame = searchedFlexFrame;
														break;
													}
												}
											}
											if (aFlexFrame == null)
											{
												aFlexFrame = new FlexFrame37();
												//Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)
												aBodyPart.theFlexFrames.Add(aFlexFrame);
												aFlexFrame.bodyAndMeshVertexIndexStarts = new List<int>();
												aFlexFrame.flexes = new List<SourceMdlFlex37>();

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
												//	aFlexFrame.flexPartnerName = Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theName
												//	aFlexFrame.flexSplit = Me.GetSplit(aFlex, meshVertexIndexStart)
												//	Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
												//Else
												//	'line += "flex """
												aFlexFrame.flexDescription = aFlexFrame.flexName;
												//	aFlexFrame.flexHasPartner = False
												//End If
												theMdlFileData.theFlexDescs[aFlex.flexDescIndex].theDescIsUsedByFlex = true;

												flexDescToFlexFrames[aFlex.flexDescIndex].Add(aFlexFrame);
											}

											aFlexFrame.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart + cumulativebodyPartVertexIndexStart);
											aFlexFrame.flexes.Add(aFlex);

											//flexDescToMeshIndexes(aFlex.flexDescIndex).Add(meshIndex)
										}
									}
								}
							}
							//For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
							//	flexDescToMeshIndexes(x).Clear()
							//Next

							cumulativebodyPartVertexIndexStart += aModel.vertexCount;
						}
					}
				}
			}
		}

#endregion

#region Private Methods

		//TODO: VERIFY ReadAxisInterpBone()
		private void ReadAxisInterpBone(long boneInputFileStreamPosition, SourceMdlBone37 aBone)
		{
			long axisInterpBoneInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				axisInterpBoneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				aBone.theAxisInterpBone = new SourceMdlAxisInterpBone();
				aBone.theAxisInterpBone.control = theInputFileReader.ReadInt32();
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

				inputFileStreamPosition = theInputFileReader.BaseStream.Position;

				//If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
				//	Me.ReadTriggers(axisInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
				//End If

				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone");
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadQuatInterpBone(long boneInputFileStreamPosition, SourceMdlBone37 aBone)
		{
			long quatInterpBoneInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				quatInterpBoneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				aBone.theQuatInterpBone = new SourceMdlQuatInterpBone();
				aBone.theQuatInterpBone.controlBoneIndex = theInputFileReader.ReadInt32();
				aBone.theQuatInterpBone.triggerCount = theInputFileReader.ReadInt32();
				aBone.theQuatInterpBone.triggerOffset = theInputFileReader.ReadInt32();

				inputFileStreamPosition = theInputFileReader.BaseStream.Position;

				if (aBone.theQuatInterpBone.triggerCount > 0 && aBone.theQuatInterpBone.triggerOffset != 0)
				{
					ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone);
				}

				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone");
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadTriggers(long quatInterpBoneInputFileStreamPosition, SourceMdlQuatInterpBone aQuatInterpBone)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aQuatInterpBone.theTriggers = new List<SourceMdlQuatInterpBoneInfo>(aQuatInterpBone.triggerCount);
				for (int j = 0; j < aQuatInterpBone.triggerCount; j++)
				{
					SourceMdlQuatInterpBoneInfo aTrigger = new SourceMdlQuatInterpBoneInfo();

					aTrigger.inverseToleranceAngle = theInputFileReader.ReadSingle();

					aTrigger.trigger = new SourceQuaternion();
					aTrigger.trigger.x = theInputFileReader.ReadSingle();
					aTrigger.trigger.y = theInputFileReader.ReadSingle();
					aTrigger.trigger.z = theInputFileReader.ReadSingle();
					aTrigger.trigger.w = theInputFileReader.ReadSingle();

					aTrigger.pos = new SourceVector();
					aTrigger.pos.x = theInputFileReader.ReadSingle();
					aTrigger.pos.y = theInputFileReader.ReadSingle();
					aTrigger.pos.z = theInputFileReader.ReadSingle();

					aTrigger.quat = new SourceQuaternion();
					aTrigger.quat.x = theInputFileReader.ReadSingle();
					aTrigger.quat.y = theInputFileReader.ReadSingle();
					aTrigger.quat.z = theInputFileReader.ReadSingle();
					aTrigger.quat.w = theInputFileReader.ReadSingle();

					aQuatInterpBone.theTriggers.Add(aTrigger);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aQuatInterpBone.theTriggers " + aQuatInterpBone.theTriggers.Count.ToString());
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadHitboxes(long hitboxSetInputFileStreamPosition, SourceMdlHitboxSet37 aHitboxSet)
		{
			if (aHitboxSet.hitboxCount > 0)
			{
				long hitboxInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aHitboxSet.theHitboxes = new List<SourceMdlHitbox37>(aHitboxSet.hitboxCount);
					for (int j = 0; j < aHitboxSet.hitboxCount; j++)
					{
						hitboxInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlHitbox37 aHitbox = new SourceMdlHitbox37();

						aHitbox.boneIndex = theInputFileReader.ReadInt32();
						aHitbox.groupIndex = theInputFileReader.ReadInt32();
						aHitbox.boundingBoxMin.x = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.y = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.z = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.x = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.y = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.z = theInputFileReader.ReadSingle();
						aHitbox.nameOffset = theInputFileReader.ReadInt32();
						for (int x = 0; x < aHitbox.unused.Length; x++)
						{
							aHitbox.unused[x] = theInputFileReader.ReadByte();
						}

						aHitboxSet.theHitboxes.Add(aHitbox);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aHitbox.nameOffset != 0)
						{
							//NOTE: The nameOffset is absolute offset in studiomdl.
							theInputFileReader.BaseStream.Seek(aHitbox.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aHitbox.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitbox.theName = " + aHitbox.theName);
							}
						}
						else
						{
							aHitbox.theName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes " + aHitboxSet.theHitboxes.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadPoseKeys(long seqInputFileStreamPosition, SourceMdlSequenceDesc36 aSeqDesc)
		{
			if ((aSeqDesc.groupSize[0] > 1 || aSeqDesc.groupSize[1] > 1) && aSeqDesc.poseKeyOffset != 0)
			{
				try
				{
					int poseKeyCount = aSeqDesc.groupSize[0] + aSeqDesc.groupSize[1];
					long poseKeyInputFileStreamPosition = 0;
					//Dim inputFileStreamPosition As Long
					long fileOffsetStart = 0;
					long fileOffsetEnd = 0;

					theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.poseKeyOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSeqDesc.thePoseKeys = new List<double>(poseKeyCount);
					for (int j = 0; j < poseKeyCount; j++)
					{
						poseKeyInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						double aPoseKey = theInputFileReader.ReadSingle();
						aSeqDesc.thePoseKeys.Add(aPoseKey);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.thePoseKeys " + aSeqDesc.thePoseKeys.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadEvents(long seqInputFileStreamPosition, SourceMdlSequenceDesc36 aSeqDesc)
		{
			if (aSeqDesc.eventCount > 0 && aSeqDesc.eventOffset != 0)
			{
				try
				{
					//Dim eventInputFileStreamPosition As Long
					//Dim inputFileStreamPosition As Long
					long fileOffsetStart = 0;
					long fileOffsetEnd = 0;
					//Dim fileOffsetStart2 As Long
					//Dim fileOffsetEnd2 As Long

					theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSeqDesc.theEvents = new List<SourceMdlEvent37>(aSeqDesc.eventCount);
					for (int j = 0; j < aSeqDesc.eventCount; j++)
					{
						//eventInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlEvent37 anEvent = new SourceMdlEvent37();

						anEvent.cycle = theInputFileReader.ReadSingle();
						anEvent.eventIndex = theInputFileReader.ReadInt32();
						anEvent.eventType = theInputFileReader.ReadInt32();
						for (int x = 0; x < anEvent.options.Length; x++)
						{
							anEvent.options[x] = theInputFileReader.ReadChar();
						}

						aSeqDesc.theEvents.Add(anEvent);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theEvents " + aSeqDesc.theEvents.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theEvents alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadAutoLayers(long seqInputFileStreamPosition, SourceMdlSequenceDesc36 aSeqDesc)
		{
			if (aSeqDesc.autoLayerCount > 0 && aSeqDesc.autoLayerOffset != 0)
			{
				try
				{
					long autoLayerInputFileStreamPosition = 0;
					//Dim inputFileStreamPosition As Long
					long fileOffsetStart = 0;
					long fileOffsetEnd = 0;

					theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSeqDesc.theAutoLayers = new List<SourceMdlAutoLayer37>(aSeqDesc.autoLayerCount);
					for (int j = 0; j < aSeqDesc.autoLayerCount; j++)
					{
						autoLayerInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlAutoLayer37 anAutoLayer = new SourceMdlAutoLayer37();

						anAutoLayer.sequenceIndex = theInputFileReader.ReadInt32();
						anAutoLayer.flags = theInputFileReader.ReadInt32();
						anAutoLayer.influenceStart = theInputFileReader.ReadSingle();
						anAutoLayer.influencePeak = theInputFileReader.ReadSingle();
						anAutoLayer.influenceTail = theInputFileReader.ReadSingle();
						anAutoLayer.influenceEnd = theInputFileReader.ReadSingle();

						aSeqDesc.theAutoLayers.Add(anAutoLayer);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAutoLayers " + aSeqDesc.theAutoLayers.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMdlAnimBoneWeights(long seqInputFileStreamPosition, SourceMdlSequenceDesc36 aSeqDesc)
		{
			//If Me.theMdlFileData.boneCount > 0 AndAlso aSeqDesc.weightOffset > 0 Then
			if (theMdlFileData.boneCount > 0)
			{
				try
				{
					long weightListInputFileStreamPosition = 0;
					//Dim inputFileStreamPosition As Long
					long fileOffsetStart = 0;
					long fileOffsetEnd = 0;
					//Dim fileOffsetStart2 As Long
					//Dim fileOffsetEnd2 As Long

					//Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.weightOffset, SeekOrigin.Begin)
					theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSeqDesc.theBoneWeightsAreDefault = true;
					aSeqDesc.theBoneWeights = new List<double>(theMdlFileData.boneCount);
					for (int j = 0; j < theMdlFileData.boneCount; j++)
					{
						weightListInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						double anAnimBoneWeight = theInputFileReader.ReadSingle();
						aSeqDesc.theBoneWeights.Add(anAnimBoneWeight);

						if (anAnimBoneWeight != 1)
						{
							aSeqDesc.theBoneWeightsAreDefault = false;
						}

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart))
					{
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theBoneWeights " + aSeqDesc.theBoneWeights.Count.ToString());
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadSequenceIkLocks(long seqInputFileStreamPosition, SourceMdlSequenceDesc36 aSeqDesc)
		{
			if (aSeqDesc.ikLockCount > 0 && aSeqDesc.ikLockOffset != 0)
			{
				try
				{
					long lockInputFileStreamPosition = 0;
					//Dim inputFileStreamPosition As Long
					long fileOffsetStart = 0;
					long fileOffsetEnd = 0;

					theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSeqDesc.theIkLocks = new List<SourceMdlIkLock37>(aSeqDesc.ikLockCount);
					for (int j = 0; j < aSeqDesc.ikLockCount; j++)
					{
						lockInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlIkLock37 anIkLock = new SourceMdlIkLock37();

						anIkLock.chainIndex = theInputFileReader.ReadInt32();
						anIkLock.posWeight = theInputFileReader.ReadSingle();
						anIkLock.localQWeight = theInputFileReader.ReadSingle();

						aSeqDesc.theIkLocks.Add(anIkLock);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theIkLocks " + aSeqDesc.theIkLocks.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theIkLocks alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		//Private Sub ReadMdlAnimIndexes(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc36)
		//	If (aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)) > 0 AndAlso aSeqDesc.blendOffset <> 0 Then
		//		Try
		//			Dim animIndexCount As Integer
		//			animIndexCount = aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)
		//			Dim animIndexInputFileStreamPosition As Long
		//			'Dim inputFileStreamPosition As Long
		//			Dim fileOffsetStart As Long
		//			Dim fileOffsetEnd As Long

		//			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.blendOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aSeqDesc.theAnimDescIndexes = New List(Of Short)(animIndexCount)
		//			For j As Integer = 0 To animIndexCount - 1
		//				animIndexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anAnimIndex As Short

		//				anAnimIndex = Me.theInputFileReader.ReadInt16()

		//				aSeqDesc.theAnimDescIndexes.Add(anAnimIndex)

		//				If Me.theMdlFileData.theAnimationDescs IsNot Nothing Then
		//					'NOTE: Set this boolean for use in writing lines in qc file.
		//					Me.theMdlFileData.theAnimationDescs(anAnimIndex).theAnimIsLinkedToSequence = True
		//					Me.theMdlFileData.theAnimationDescs(anAnimIndex).theLinkedSequences.Add(aSeqDesc)
		//				End If

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			'TODO: A sequence can point to same anims as another?
		//			If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
		//				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAnimDescIndexes " + aSeqDesc.theAnimDescIndexes.Count.ToString())
		//			End If

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theAnimDescIndexes alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		private void ReadSequenceKeyValues(long seqInputFileStreamPosition, SourceMdlSequenceDesc36 aSeqDesc)
		{
			if (aSeqDesc.keyValueSize > 0 && aSeqDesc.keyValueOffset != 0)
			{
				try
				{
					long fileOffsetStart = 0;
					long fileOffsetEnd = 0;

					theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSeqDesc.theKeyValues = FileManager.ReadNullTerminatedString(theInputFileReader);

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theKeyValues");

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theKeyValues alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadAnimations(long animationDescInputFileStreamPosition, SourceMdlAnimationDesc36 anAnimationDesc)
		{
			if (anAnimationDesc.animOffset > 0)
			{
				long animationInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				long animationValuesEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					animationValuesEnd = 0;

					anAnimationDesc.theAnimations = new List<SourceMdlAnimation37>(theMdlFileData.theBones.Count);
					for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
					{
						animationInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlAnimation37 anAnimation = new SourceMdlAnimation37();

						anAnimation.flags = theInputFileReader.ReadInt32();
						if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0)
						{
							anAnimation.animationValueOffsets[0] = theInputFileReader.ReadInt32();
							anAnimation.animationValueOffsets[1] = theInputFileReader.ReadInt32();
							anAnimation.animationValueOffsets[2] = theInputFileReader.ReadInt32();
						}
						else
						{
							anAnimation.position = new SourceVector();
							anAnimation.position.x = theInputFileReader.ReadSingle();
							anAnimation.position.y = theInputFileReader.ReadSingle();
							anAnimation.position.z = theInputFileReader.ReadSingle();
						}
						if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0)
						{
							anAnimation.animationValueOffsets[3] = theInputFileReader.ReadInt32();
							anAnimation.animationValueOffsets[4] = theInputFileReader.ReadInt32();
							anAnimation.animationValueOffsets[5] = theInputFileReader.ReadInt32();
							anAnimation.unused = theInputFileReader.ReadInt32();
						}
						else
						{
							anAnimation.rotationQuat = new SourceQuaternion();
							anAnimation.rotationQuat.x = theInputFileReader.ReadSingle();
							anAnimation.rotationQuat.y = theInputFileReader.ReadSingle();
							anAnimation.rotationQuat.z = theInputFileReader.ReadSingle();
							anAnimation.rotationQuat.w = theInputFileReader.ReadSingle();
						}

						anAnimationDesc.theAnimations.Add(anAnimation);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 0, anAnimationDesc.frameCount, ref animationValuesEnd);
							ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 1, anAnimationDesc.frameCount, ref animationValuesEnd);
							ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 2, anAnimationDesc.frameCount, ref animationValuesEnd);
						}
						if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0)
						{
							ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 3, anAnimationDesc.frameCount, ref animationValuesEnd);
							ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 4, anAnimationDesc.frameCount, ref animationValuesEnd);
							ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 5, anAnimationDesc.frameCount, ref animationValuesEnd);
							anAnimation.unused = theInputFileReader.ReadInt32();
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					if (animationValuesEnd > 0)
					{
						theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, animationValuesEnd, 4, "anAnimation.theAnimationValues alignment");
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations " + anAnimationDesc.theAnimations.Count.ToString())

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theAnimations alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadAnimationValues(long animationInputFileStreamPosition, SourceMdlAnimation37 anAnimation, int offsetIndex, int frameCount, ref long fileOffsetEnd)
		{
			if (anAnimation.animationValueOffsets[offsetIndex] > 0)
			{
				long fileOffsetStart = 0;
				//Dim fileOffsetEnd As Long
				int frameCountRemainingToBeChecked = 0;
				byte currentTotal = 0;
				byte validCount = 0;
				List<SourceMdlAnimationValue10> animValues = null;

				anAnimation.theAnimationValues[offsetIndex] = new List<SourceMdlAnimationValue10>();
				animValues = anAnimation.theAnimationValues[offsetIndex];

				try
				{
					theInputFileReader.BaseStream.Seek(animationInputFileStreamPosition + anAnimation.animationValueOffsets[offsetIndex], SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					frameCountRemainingToBeChecked = frameCount;
					while (frameCountRemainingToBeChecked > 0)
					{
						SourceMdlAnimationValue10 animValue = new SourceMdlAnimationValue10();
						animValue.value = theInputFileReader.ReadInt16();
						currentTotal = animValue.total;
						if (currentTotal == 0)
						{
							int badIfThisIsReached = 42;
							break;
						}
						frameCountRemainingToBeChecked -= currentTotal;
						animValues.Add(animValue);

						validCount = animValue.valid;
						for (int i = 1; i <= validCount; i++)
						{
							SourceMdlAnimationValue10 animValue2 = new SourceMdlAnimationValue10();
							animValue2.value = theInputFileReader.ReadInt16();
							animValues.Add(animValue2);
						}
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation.theAnimationValues");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMdlMovements(long animInputFileStreamPosition, SourceMdlAnimationDesc36 anAnimationDesc)
		{
			if (anAnimationDesc.movementCount > 0)
			{
				long movementInputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.movementOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					anAnimationDesc.theMovements = new List<SourceMdlMovement>(anAnimationDesc.movementCount);
					for (int j = 0; j < anAnimationDesc.movementCount; j++)
					{
						movementInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlMovement aMovement = new SourceMdlMovement();

						aMovement.endframeIndex = theInputFileReader.ReadInt32();
						aMovement.motionFlags = theInputFileReader.ReadInt32();
						aMovement.v0 = theInputFileReader.ReadSingle();
						aMovement.v1 = theInputFileReader.ReadSingle();
						aMovement.angle = theInputFileReader.ReadSingle();

						aMovement.vector = new SourceVector();
						aMovement.vector.x = theInputFileReader.ReadSingle();
						aMovement.vector.y = theInputFileReader.ReadSingle();
						aMovement.vector.z = theInputFileReader.ReadSingle();
						aMovement.position = new SourceVector();
						aMovement.position.x = theInputFileReader.ReadSingle();
						aMovement.position.y = theInputFileReader.ReadSingle();
						aMovement.position.z = theInputFileReader.ReadSingle();

						anAnimationDesc.theMovements.Add(aMovement);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theMovements " + anAnimationDesc.theMovements.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theMovements alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMdlIkRules(long animInputFileStreamPosition, SourceMdlAnimationDesc36 anAnimationDesc)
		{
			if (anAnimationDesc.ikRuleCount > 0)
			{
				long ikRuleInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					anAnimationDesc.theIkRules = new List<SourceMdlIkRule37>(anAnimationDesc.ikRuleCount);
					for (int j = 0; j < anAnimationDesc.ikRuleCount; j++)
					{
						ikRuleInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlIkRule37 anIkRule = new SourceMdlIkRule37();

						anIkRule.index = theInputFileReader.ReadInt32();
						anIkRule.type = theInputFileReader.ReadInt32();
						anIkRule.chain = theInputFileReader.ReadInt32();
						anIkRule.bone = theInputFileReader.ReadInt32();

						anIkRule.slot = theInputFileReader.ReadInt32();
						anIkRule.height = theInputFileReader.ReadSingle();
						anIkRule.radius = theInputFileReader.ReadSingle();
						anIkRule.floor = theInputFileReader.ReadSingle();

						anIkRule.pos = new SourceVector();
						anIkRule.pos.x = theInputFileReader.ReadSingle();
						anIkRule.pos.y = theInputFileReader.ReadSingle();
						anIkRule.pos.z = theInputFileReader.ReadSingle();
						anIkRule.q = new SourceQuaternion();
						anIkRule.q.x = theInputFileReader.ReadSingle();
						anIkRule.q.y = theInputFileReader.ReadSingle();
						anIkRule.q.z = theInputFileReader.ReadSingle();
						anIkRule.q.w = theInputFileReader.ReadSingle();

						anIkRule.weight = theInputFileReader.ReadSingle();
						anIkRule.group = theInputFileReader.ReadInt32();
						anIkRule.ikErrorIndexStart = theInputFileReader.ReadInt32();
						anIkRule.ikErrorOffset = theInputFileReader.ReadInt32();

						anIkRule.influenceStart = theInputFileReader.ReadSingle();
						anIkRule.influencePeak = theInputFileReader.ReadSingle();
						anIkRule.influenceTail = theInputFileReader.ReadSingle();
						anIkRule.influenceEnd = theInputFileReader.ReadSingle();

						anIkRule.commit = theInputFileReader.ReadSingle();
						anIkRule.contact = theInputFileReader.ReadSingle();
						anIkRule.pivot = theInputFileReader.ReadSingle();
						anIkRule.release = theInputFileReader.ReadSingle();

						anAnimationDesc.theIkRules.Add(anIkRule);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadMdlIkErrors(ikRuleInputFileStreamPosition, anIkRule, anAnimationDesc.frameCount);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkRules " + anAnimationDesc.theIkRules.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMdlIkErrors(long ikRuleInputFileStreamPosition, SourceMdlIkRule37 anIkRule, int frameCount)
		{
			//pikrule->start	= g_panimation[i]->ikrule[j].start / (g_panimation[i]->numframes - 1.0f);
			//pikrule->end	= g_panimation[i]->ikrule[j].end / (g_panimation[i]->numframes - 1.0f);
			//pRule->numerror = pRule->end - pRule->start + 1;
			//if (pRule->end >= panim->numframes)
			//	pRule->numerror = pRule->numerror + 2;
			int ikErrorStart = 0;
			int ikErrorEnd = 0;
			int ikErrorCount = 0;
			ikErrorStart = Convert.ToInt32(anIkRule.influenceStart * (frameCount - 1));
			ikErrorEnd = Convert.ToInt32(anIkRule.influenceEnd * (frameCount - 1));
			ikErrorCount = ikErrorEnd - ikErrorStart + 1;
			if (ikErrorEnd >= frameCount)
			{
				ikErrorCount += 2;
			}

			if (ikErrorCount > 0)
			{
				//Dim ikErrorInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.ikErrorOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					anIkRule.theIkErrors = new List<SourceMdlIkError37>(ikErrorCount);
					for (int j = 0; j < ikErrorCount; j++)
					{
						//ikErrorInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlIkError37 anIkError = new SourceMdlIkError37();

						anIkError.pos = new SourceVector();
						anIkError.pos.x = theInputFileReader.ReadSingle();
						anIkError.pos.y = theInputFileReader.ReadSingle();
						anIkError.pos.z = theInputFileReader.ReadSingle();
						anIkError.q = new SourceQuaternion();
						anIkError.q.x = theInputFileReader.ReadSingle();
						anIkError.q.y = theInputFileReader.ReadSingle();
						anIkError.q.z = theInputFileReader.ReadSingle();
						anIkError.q.w = theInputFileReader.ReadSingle();

						anIkRule.theIkErrors.Add(anIkError);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkErrors " + anIkRule.theIkErrors.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadFlexOps(long flexRuleInputFileStreamPosition, SourceMdlFlexRule aFlexRule)
		{
			if (aFlexRule.opCount > 0 && aFlexRule.opOffset != 0)
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

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexOps alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadIkLinks(long ikChainInputFileStreamPosition, SourceMdlIkChain37 anIkChain)
		{
			if (anIkChain.linkCount > 0)
			{
				//Dim ikLinkInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					anIkChain.theLinks = new List<SourceMdlIkLink37>(anIkChain.linkCount);
					for (int j = 0; j < anIkChain.linkCount; j++)
					{
						//ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlIkLink37 anIkLink = new SourceMdlIkLink37();

						anIkLink.boneIndex = theInputFileReader.ReadInt32();
						anIkLink.contact.x = theInputFileReader.ReadSingle();
						anIkLink.contact.y = theInputFileReader.ReadSingle();
						anIkLink.contact.z = theInputFileReader.ReadSingle();
						anIkLink.limits.x = theInputFileReader.ReadSingle();
						anIkLink.limits.y = theInputFileReader.ReadSingle();
						anIkLink.limits.z = theInputFileReader.ReadSingle();

						anIkChain.theLinks.Add(anIkLink);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks " + anIkChain.theLinks.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModels(long bodyPartInputFileStreamPosition, SourceMdlBodyPart37 aBodyPart)
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
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aBodyPart.theModels = new List<SourceMdlModel37>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlModel37 aModel = new SourceMdlModel37();

						aModel.name = theInputFileReader.ReadChars(aModel.name.Length);
						aModel.theName = (new string(aModel.name)).Trim('\0');
						aModel.type = theInputFileReader.ReadInt32();
						aModel.boundingRadius = theInputFileReader.ReadSingle();
						aModel.meshCount = theInputFileReader.ReadInt32();
						aModel.meshOffset = theInputFileReader.ReadInt32();

						aModel.vertexCount = theInputFileReader.ReadInt32();
						aModel.vertexOffset = theInputFileReader.ReadInt32();
						aModel.tangentOffset = theInputFileReader.ReadInt32();

						aModel.attachmentCount = theInputFileReader.ReadInt32();
						aModel.attachmentOffset = theInputFileReader.ReadInt32();
						aModel.eyeballCount = theInputFileReader.ReadInt32();
						aModel.eyeballOffset = theInputFileReader.ReadInt32();

						for (int x = 0; x < aModel.unused.Length; x++)
						{
							aModel.unused[x] = theInputFileReader.ReadInt32();
						}

						aBodyPart.theModels.Add(aModel);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
						ReadEyeballs(modelInputFileStreamPosition, aModel);
						ReadMeshes(modelInputFileStreamPosition, aModel);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						//'NOTE: Although studiomdl source code indicates ALIGN64, it seems to align on 32.
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 32, "aModel.theVertexes pre-alignment (NOTE: Should end at: " + CStr(modelInputFileStreamPosition + aModel.vertexOffset - 1) + ")")
						ReadVertexes(modelInputFileStreamPosition, aModel);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theTangents pre-alignment (NOTE: Should end at: " + CStr(modelInputFileStreamPosition + aModel.tangentOffset - 1) + ")")
						ReadTangents(modelInputFileStreamPosition, aModel);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels " + aBodyPart.theModels.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadEyeballs(long modelInputFileStreamPosition, SourceMdlModel37 aModel)
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
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theEyeballs = new List<SourceMdlEyeball37>(aModel.eyeballCount);
					for (int eyeballIndex = 0; eyeballIndex < aModel.eyeballCount; eyeballIndex++)
					{
						eyeballInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlEyeball37 anEyeball = new SourceMdlEyeball37();

						anEyeball.nameOffset = theInputFileReader.ReadInt32();
						anEyeball.boneIndex = theInputFileReader.ReadInt32();
						anEyeball.org = new SourceVector();
						anEyeball.org.x = theInputFileReader.ReadSingle();
						anEyeball.org.y = theInputFileReader.ReadSingle();
						anEyeball.org.z = theInputFileReader.ReadSingle();
						anEyeball.zOffset = theInputFileReader.ReadSingle();
						anEyeball.radius = theInputFileReader.ReadSingle();
						anEyeball.up = new SourceVector();
						anEyeball.up.x = theInputFileReader.ReadSingle();
						anEyeball.up.y = theInputFileReader.ReadSingle();
						anEyeball.up.z = theInputFileReader.ReadSingle();
						anEyeball.forward = new SourceVector();
						anEyeball.forward.x = theInputFileReader.ReadSingle();
						anEyeball.forward.y = theInputFileReader.ReadSingle();
						anEyeball.forward.z = theInputFileReader.ReadSingle();
						anEyeball.texture = theInputFileReader.ReadInt32();

						anEyeball.irisMaterial = theInputFileReader.ReadInt32();
						anEyeball.irisScale = theInputFileReader.ReadSingle();
						anEyeball.glintMaterial = theInputFileReader.ReadInt32();

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

						anEyeball.pitch[0] = theInputFileReader.ReadSingle();
						anEyeball.pitch[1] = theInputFileReader.ReadSingle();
						anEyeball.yaw[0] = theInputFileReader.ReadSingle();
						anEyeball.yaw[1] = theInputFileReader.ReadSingle();

						aModel.theEyeballs.Add(anEyeball);

						//NOTE: Set the default value to -1 to distinguish it from value assigned to it by ReadMeshes().
						anEyeball.theTextureIndex = -1;

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
					if (aModel.theEyeballs.Count > 0)
					{
						// Detect if $upaxis Y was used.
						//FROM: [48] SourceEngine2007_source se2007_src\src_main\utils\studiomdl\studiomdl.cpp
						//      Option_Eyeball()
						//	AngleMatrix( g_defaultrotation, vtmp );
						//	VectorIRotate( Vector( 0, 0, 1 ), vtmp, tmp );
						//	VectorIRotate( tmp, pmodel->source->boneToPose[eyeball->bone], eyeball->up );
						SourceMdlEyeball37 anEyeball = null;
						SourceMdlBone37 aBone = null;
						//Dim upVec As New SourceVector(0, 0, 1)
						SourceVector tmp = null;
						anEyeball = aModel.theEyeballs[0];
						aBone = theMdlFileData.theBones[anEyeball.boneIndex];
						tmp = MathModule.VectorIRotate(aModel.theEyeballs[0].up, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3);
						if (tmp.y > 0.99 && tmp.y < 1.01)
						{
							theMdlFileData.theUpAxisYCommandWasUsed = true;
						}
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs " + aModel.theEyeballs.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aModel.theEyeballs alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMeshes(long modelInputFileStreamPosition, SourceMdlModel37 aModel)
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
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theMeshes = new List<SourceMdlMesh37>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						meshInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlMesh37 aMesh = new SourceMdlMesh37();

						aMesh.materialIndex = theInputFileReader.ReadInt32();
						aMesh.modelOffset = theInputFileReader.ReadInt32();

						aMesh.vertexCount = theInputFileReader.ReadInt32();
						aMesh.vertexIndexStart = theInputFileReader.ReadInt32();
						aMesh.flexCount = theInputFileReader.ReadInt32();
						aMesh.flexOffset = theInputFileReader.ReadInt32();
						aMesh.materialType = theInputFileReader.ReadInt32();
						aMesh.materialParam = theInputFileReader.ReadInt32();

						aMesh.id = theInputFileReader.ReadInt32();
						aMesh.center.x = theInputFileReader.ReadSingle();
						aMesh.center.y = theInputFileReader.ReadSingle();
						aMesh.center.z = theInputFileReader.ReadSingle();
						for (int x = 0; x < aMesh.unused.Length; x++)
						{
							aMesh.unused[x] = theInputFileReader.ReadInt32();
						}

						aModel.theMeshes.Add(aMesh);

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
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes " + aModel.theMeshes.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadVertexes(long modelInputFileStreamPosition, SourceMdlModel37 aModel)
		{
			if (aModel.vertexCount > 0 && aModel.vertexOffset != 0)
			{
				long vertexInputFileStreamPosition = 0;
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theVertexes = new List<SourceMdlVertex37>(aModel.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModel.vertexCount; vertexIndex++)
					{
						vertexInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlVertex37 aVertex = new SourceMdlVertex37();

						aVertex.boneWeight.weight[0] = theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[1] = theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[2] = theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[3] = theInputFileReader.ReadSingle();
						aVertex.boneWeight.bone[0] = theInputFileReader.ReadInt16();
						aVertex.boneWeight.bone[1] = theInputFileReader.ReadInt16();
						aVertex.boneWeight.bone[2] = theInputFileReader.ReadInt16();
						aVertex.boneWeight.bone[3] = theInputFileReader.ReadInt16();
						aVertex.boneWeight.boneCount = theInputFileReader.ReadInt16();
						aVertex.boneWeight.material = theInputFileReader.ReadInt16();
						aVertex.boneWeight.firstRef = theInputFileReader.ReadInt16();
						aVertex.boneWeight.lastRef = theInputFileReader.ReadInt16();
						aVertex.position.x = theInputFileReader.ReadSingle();
						aVertex.position.y = theInputFileReader.ReadSingle();
						aVertex.position.z = theInputFileReader.ReadSingle();
						aVertex.normal.x = theInputFileReader.ReadSingle();
						aVertex.normal.y = theInputFileReader.ReadSingle();
						aVertex.normal.z = theInputFileReader.ReadSingle();
						aVertex.texCoordX = theInputFileReader.ReadSingle();
						aVertex.texCoordY = theInputFileReader.ReadSingle();

						aModel.theVertexes.Add(aVertex);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModel.theVertexes.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadTangents(long modelInputFileStreamPosition, SourceMdlModel37 aModel)
		{
			if (aModel.vertexCount > 0 && aModel.tangentOffset != 0)
			{
				long vertexInputFileStreamPosition = 0;
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.tangentOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theTangents = new List<SourceVector4D>(aModel.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModel.vertexCount; vertexIndex++)
					{
						vertexInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceVector4D aTangent = new SourceVector4D();

						aTangent.x = theInputFileReader.ReadSingle();
						aTangent.y = theInputFileReader.ReadSingle();
						aTangent.z = theInputFileReader.ReadSingle();
						aTangent.w = theInputFileReader.ReadSingle();

						aModel.theTangents.Add(aTangent);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theTangents " + aModel.theTangents.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theTangents alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadFlexes(long meshInputFileStreamPosition, SourceMdlMesh37 aMesh)
		{
			if (aMesh.flexCount > 0 && aMesh.flexOffset != 0)
			{
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

					aMesh.theFlexes = new List<SourceMdlFlex37>(aMesh.flexCount);
					for (int k = 0; k < aMesh.flexCount; k++)
					{
						flexInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlFlex37 aFlex = new SourceMdlFlex37();

						aFlex.flexDescIndex = theInputFileReader.ReadInt32();

						aFlex.target0 = theInputFileReader.ReadSingle();
						aFlex.target1 = theInputFileReader.ReadSingle();
						aFlex.target2 = theInputFileReader.ReadSingle();
						aFlex.target3 = theInputFileReader.ReadSingle();

						aFlex.vertCount = theInputFileReader.ReadInt32();
						aFlex.vertOffset = theInputFileReader.ReadInt32();

						aMesh.theFlexes.Add(aFlex);

						//'NOTE: Set the frame index here because it is determined by order of flexes in mdl file.
						//'      Start the indexing at 1 because first frame (frame 0) is "basis" frame.
						//Me.theCurrentFrameIndex += 1
						//Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex = Me.theCurrentFrameIndex

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadVertAnims(flexInputFileStreamPosition, aFlex);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aMesh.theFlexes alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadVertAnims(long flexInputFileStreamPosition, SourceMdlFlex37 aFlex)
		{
			if (aFlex.vertCount > 0 && aFlex.vertOffset != 0)
			{
				//Dim vertAnimInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aFlex.theVertAnims = new List<SourceMdlVertAnim37>(aFlex.vertCount);
					for (int k = 0; k < aFlex.vertCount; k++)
					{
						//vertAnimInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlVertAnim37 aVertAnim = new SourceMdlVertAnim37();

						aVertAnim.index = theInputFileReader.ReadInt32();
						aVertAnim.delta.x = theInputFileReader.ReadSingle();
						aVertAnim.delta.y = theInputFileReader.ReadSingle();
						aVertAnim.delta.z = theInputFileReader.ReadSingle();
						aVertAnim.nDelta.x = theInputFileReader.ReadSingle();
						aVertAnim.nDelta.y = theInputFileReader.ReadSingle();
						aVertAnim.nDelta.z = theInputFileReader.ReadSingle();

						aFlex.theVertAnims.Add(aVertAnim);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

#endregion

#region Data

		protected BinaryReader theInputFileReader;
		protected BinaryWriter theOutputFileWriter;

		protected SourceMdlFileData36 theMdlFileData;

#endregion

	}

}