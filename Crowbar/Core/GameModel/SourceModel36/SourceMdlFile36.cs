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
	public class SourceMdlFile36
	{

#region Creation and Destruction

		public SourceMdlFile36(BinaryReader mdlFileReader, SourceMdlFileData36 mdlFileData)
		{
			this.theInputFileReader = mdlFileReader;
			this.theMdlFileData = mdlFileData;

			this.theMdlFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile36(BinaryWriter mdlFileWriter, SourceMdlFileData36 mdlFileData)
		{
			this.theOutputFileWriter = mdlFileWriter;
			this.theMdlFileData = mdlFileData;
		}

#endregion

#region Methods

		public void ReadMdlHeader00(string logDescription)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			this.theMdlFileData.id = this.theInputFileReader.ReadChars(4);
			this.theMdlFileData.theID = new string(this.theMdlFileData.id);
			this.theMdlFileData.version = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.checksum = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.name = this.theInputFileReader.ReadChars(this.theMdlFileData.name.Length);
			this.theMdlFileData.theModelName = (new string(this.theMdlFileData.name)).Trim('\0');

			this.theMdlFileData.fileSize = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.theActualFileSize = this.theInputFileReader.BaseStream.Length;

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			if (!string.IsNullOrEmpty(logDescription))
			{
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription + " (MDL version: " + this.theMdlFileData.version.ToString() + ")");
			}
		}

		public void ReadMdlHeader01(string logDescription)
		{
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			long fileOffsetStart2 = 0;
			long fileOffsetEnd2 = 0;

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			this.theMdlFileData.eyePosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.eyePosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.eyePosition.z = this.theInputFileReader.ReadSingle();

			this.theMdlFileData.illuminationPosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.illuminationPosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.illuminationPosition.z = this.theInputFileReader.ReadSingle();

			this.theMdlFileData.hullMinPosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.hullMinPosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.hullMinPosition.z = this.theInputFileReader.ReadSingle();

			this.theMdlFileData.hullMaxPosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.hullMaxPosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.hullMaxPosition.z = this.theInputFileReader.ReadSingle();

			this.theMdlFileData.viewBoundingBoxMinPosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.viewBoundingBoxMinPosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.viewBoundingBoxMinPosition.z = this.theInputFileReader.ReadSingle();

			this.theMdlFileData.viewBoundingBoxMaxPosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.viewBoundingBoxMaxPosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.viewBoundingBoxMaxPosition.z = this.theInputFileReader.ReadSingle();

			this.theMdlFileData.flags = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.boneCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneControllerCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneControllerOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.hitboxSetCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.hitboxSetOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.animationCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.animationOffset = this.theInputFileReader.ReadInt32();
			//Me.theMdlFileData.animationGroupCount = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.animationGroupOffset = Me.theInputFileReader.ReadInt32()

			//Me.theMdlFileData.boneDescCount = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.boneDescOffset = Me.theInputFileReader.ReadInt32()

			this.theMdlFileData.localSequenceCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localSequenceOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.sequencesIndexedFlag = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.sequenceGroupCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.sequenceGroupOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.textureCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.textureOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.texturePathCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.texturePathOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinReferenceCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinFamilyCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.bodyPartCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.bodyPartOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.localAttachmentCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localAttachmentOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.transitionCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.transitionOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.flexDescCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexDescOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexControllerCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexControllerOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexRuleCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexRuleOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.ikChainCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.ikChainOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.mouthCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.mouthOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localPoseParamaterCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localPoseParameterOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.surfacePropOffset = this.theInputFileReader.ReadInt32();

			if (this.theMdlFileData.surfacePropOffset > 0)
			{
				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.surfacePropOffset, SeekOrigin.Begin);
				fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

				fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
				if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
				{
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName = " + this.theMdlFileData.theSurfacePropName);
				}
				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}
			else
			{
				this.theMdlFileData.theSurfacePropName = "";
			}

			this.theMdlFileData.keyValueOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.keyValueSize = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.localIkAutoPlayLockCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localIkAutoPlayLockOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.mass = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.contents = this.theInputFileReader.ReadInt32();

			for (int x = 0; x < this.theMdlFileData.unused.Length; x++)
			{
				this.theMdlFileData.unused[x] = this.theInputFileReader.ReadInt32();
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription);

			if (this.theMdlFileData.bodyPartCount == 0 && this.theMdlFileData.localSequenceCount > 0)
			{
				this.theMdlFileData.theMdlFileOnlyHasAnimations = true;
			}
		}

		public void ReadBones()
		{
			if (this.theMdlFileData.boneCount > 0)
			{
				long boneInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theBones = new List<SourceMdlBone37>(this.theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < this.theMdlFileData.boneCount; boneIndex++)
					{
						boneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlBone37 aBone = new SourceMdlBone37();

						aBone.nameOffset = this.theInputFileReader.ReadInt32();
						aBone.parentBoneIndex = this.theInputFileReader.ReadInt32();

						for (int j = 0; j < aBone.boneControllerIndex.Length; j++)
						{
							aBone.boneControllerIndex[j] = this.theInputFileReader.ReadInt32();
						}

						aBone.position = new SourceVector();
						aBone.position.x = this.theInputFileReader.ReadSingle();
						aBone.position.y = this.theInputFileReader.ReadSingle();
						aBone.position.z = this.theInputFileReader.ReadSingle();
						aBone.rotation = new SourceVector();
						aBone.rotation.x = this.theInputFileReader.ReadSingle();
						aBone.rotation.y = this.theInputFileReader.ReadSingle();
						aBone.rotation.z = this.theInputFileReader.ReadSingle();
						aBone.positionScale = new SourceVector();
						aBone.positionScale.x = this.theInputFileReader.ReadSingle();
						aBone.positionScale.y = this.theInputFileReader.ReadSingle();
						aBone.positionScale.z = this.theInputFileReader.ReadSingle();
						aBone.rotationScale = new SourceVector();
						aBone.rotationScale.x = this.theInputFileReader.ReadSingle();
						aBone.rotationScale.y = this.theInputFileReader.ReadSingle();
						aBone.rotationScale.z = this.theInputFileReader.ReadSingle();

						aBone.poseToBoneColumn0 = new SourceVector();
						aBone.poseToBoneColumn1 = new SourceVector();
						aBone.poseToBoneColumn2 = new SourceVector();
						aBone.poseToBoneColumn3 = new SourceVector();
						aBone.poseToBoneColumn0.x = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn1.x = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn2.x = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn3.x = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn0.y = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn1.y = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn2.y = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn3.y = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn0.z = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn1.z = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn2.z = this.theInputFileReader.ReadSingle();
						aBone.poseToBoneColumn3.z = this.theInputFileReader.ReadSingle();

						aBone.qAlignment = new SourceQuaternion();
						aBone.qAlignment.x = this.theInputFileReader.ReadSingle();
						aBone.qAlignment.y = this.theInputFileReader.ReadSingle();
						aBone.qAlignment.z = this.theInputFileReader.ReadSingle();
						aBone.qAlignment.w = this.theInputFileReader.ReadSingle();

						aBone.flags = this.theInputFileReader.ReadInt32();

						aBone.proceduralRuleType = this.theInputFileReader.ReadInt32();
						aBone.proceduralRuleOffset = this.theInputFileReader.ReadInt32();
						aBone.physicsBoneIndex = this.theInputFileReader.ReadInt32();
						aBone.surfacePropNameOffset = this.theInputFileReader.ReadInt32();

						aBone.quat = new SourceQuaternion();
						aBone.quat.x = this.theInputFileReader.ReadSingle();
						aBone.quat.y = this.theInputFileReader.ReadSingle();
						aBone.quat.z = this.theInputFileReader.ReadSingle();
						aBone.quat.w = this.theInputFileReader.ReadSingle();

						aBone.contents = this.theInputFileReader.ReadInt32();

						for (int x = 0; x < aBone.unused.Length; x++)
						{
							aBone.unused[x] = this.theInputFileReader.ReadInt32();
						}

						this.theMdlFileData.theBones.Add(aBone);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aBone.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aBone.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theName = " + aBone.theName);
							}
						}
						else if (aBone.theName == null)
						{
							aBone.theName = "";
						}
						this.theMdlFileData.theBoneNameToBoneIndexMap.Add(aBone.theName, boneIndex);

						if (aBone.proceduralRuleOffset != 0)
						{
							if (aBone.proceduralRuleType == SourceMdlBone37.STUDIO_PROC_AXISINTERP)
							{
								this.ReadAxisInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone37.STUDIO_PROC_QUATINTERP)
							{
								this.theMdlFileData.theProceduralBonesCommandIsUsed = true;
								this.ReadQuatInterpBone(boneInputFileStreamPosition, aBone);
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
							this.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.surfacePropNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aBone.theSurfacePropName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theSurfacePropName = " + aBone.theSurfacePropName);
							}
						}
						else
						{
							aBone.theSurfacePropName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones " + this.theMdlFileData.theBones.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadBoneControllers()
		{
			if (this.theMdlFileData.boneControllerCount > 0)
			{
				long boneControllerInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theBoneControllers = new List<SourceMdlBoneController37>(this.theMdlFileData.boneControllerCount);
					for (int i = 0; i < this.theMdlFileData.boneControllerCount; i++)
					{
						boneControllerInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlBoneController37 aBoneController = new SourceMdlBoneController37();

						aBoneController.boneIndex = this.theInputFileReader.ReadInt32();
						aBoneController.type = this.theInputFileReader.ReadInt32();
						aBoneController.startBlah = this.theInputFileReader.ReadSingle();
						aBoneController.endBlah = this.theInputFileReader.ReadSingle();
						aBoneController.restIndex = this.theInputFileReader.ReadInt32();
						aBoneController.inputField = this.theInputFileReader.ReadInt32();
						for (int x = 0; x < aBoneController.unused.Length; x++)
						{
							aBoneController.unused[x] = this.theInputFileReader.ReadByte();
						}

						this.theMdlFileData.theBoneControllers.Add(aBoneController);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

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

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + this.theMdlFileData.theBoneControllers.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadAttachments()
		{
			if (this.theMdlFileData.localAttachmentCount > 0)
			{
				long attachmentInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localAttachmentOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theAttachments = new List<SourceMdlAttachment37>(this.theMdlFileData.localAttachmentCount);
					for (int i = 0; i < this.theMdlFileData.localAttachmentCount; i++)
					{
						attachmentInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlAttachment37 anAttachment = new SourceMdlAttachment37();

						anAttachment.nameOffset = this.theInputFileReader.ReadInt32();
						anAttachment.type = this.theInputFileReader.ReadInt32();
						anAttachment.boneIndex = this.theInputFileReader.ReadInt32();
						anAttachment.localM11 = this.theInputFileReader.ReadSingle();
						anAttachment.localM12 = this.theInputFileReader.ReadSingle();
						anAttachment.localM13 = this.theInputFileReader.ReadSingle();
						anAttachment.localM14 = this.theInputFileReader.ReadSingle();
						anAttachment.localM21 = this.theInputFileReader.ReadSingle();
						anAttachment.localM22 = this.theInputFileReader.ReadSingle();
						anAttachment.localM23 = this.theInputFileReader.ReadSingle();
						anAttachment.localM24 = this.theInputFileReader.ReadSingle();
						anAttachment.localM31 = this.theInputFileReader.ReadSingle();
						anAttachment.localM32 = this.theInputFileReader.ReadSingle();
						anAttachment.localM33 = this.theInputFileReader.ReadSingle();
						anAttachment.localM34 = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theAttachments.Add(anAttachment);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAttachment");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (anAttachment.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							anAttachment.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName = " + anAttachment.theName);
							}
						}
						else
						{
							anAttachment.theName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + Me.theMdlFileData.theAttachments.Count.ToString())

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadHitboxSets()
		{
			if (this.theMdlFileData.hitboxSetCount > 0)
			{
				long hitboxSetInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.hitboxSetOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theHitboxSets = new List<SourceMdlHitboxSet37>(this.theMdlFileData.hitboxSetCount);
					for (int i = 0; i < this.theMdlFileData.hitboxSetCount; i++)
					{
						hitboxSetInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlHitboxSet37 aHitboxSet = new SourceMdlHitboxSet37();

						aHitboxSet.nameOffset = this.theInputFileReader.ReadInt32();
						aHitboxSet.hitboxCount = this.theInputFileReader.ReadInt32();
						aHitboxSet.hitboxOffset = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theHitboxSets.Add(aHitboxSet);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aHitboxSet.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aHitboxSet.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitboxSet.theName = " + aHitboxSet.theName);
							}
						}
						else
						{
							aHitboxSet.theName = "";
						}

						this.ReadHitboxes(hitboxSetInputFileStreamPosition, aHitboxSet);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets " + this.theMdlFileData.theHitboxSets.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSequences()
		{
			if (this.theMdlFileData.localSequenceCount > 0)
			{
				long seqInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localSequenceOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theSequenceDescs = new List<SourceMdlSequenceDesc36>(this.theMdlFileData.localSequenceCount);
					for (int i = 0; i < this.theMdlFileData.localSequenceCount; i++)
					{
						seqInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlSequenceDesc36 aSeqDesc = new SourceMdlSequenceDesc36();

						aSeqDesc.nameOffset = this.theInputFileReader.ReadInt32();
						aSeqDesc.activityNameOffset = this.theInputFileReader.ReadInt32();
						aSeqDesc.flags = this.theInputFileReader.ReadInt32();
						aSeqDesc.activity = this.theInputFileReader.ReadInt32();
						aSeqDesc.activityWeight = this.theInputFileReader.ReadInt32();
						aSeqDesc.eventCount = this.theInputFileReader.ReadInt32();
						aSeqDesc.eventOffset = this.theInputFileReader.ReadInt32();

						aSeqDesc.bbMin.x = this.theInputFileReader.ReadSingle();
						aSeqDesc.bbMin.y = this.theInputFileReader.ReadSingle();
						aSeqDesc.bbMin.z = this.theInputFileReader.ReadSingle();
						aSeqDesc.bbMax.x = this.theInputFileReader.ReadSingle();
						aSeqDesc.bbMax.y = this.theInputFileReader.ReadSingle();
						aSeqDesc.bbMax.z = this.theInputFileReader.ReadSingle();

						aSeqDesc.blendCount = this.theInputFileReader.ReadInt32();

						//For x As Integer = 0 To aSequence.anim.Length - 1
						//	aSequence.anim(x) = Me.theInputFileReader.ReadInt16()
						//Next
						for (int rowIndex = 0; rowIndex < SourceModule2531.MAXSTUDIOBLENDS; rowIndex++)
						{
							for (int columnIndex = 0; columnIndex < SourceModule2531.MAXSTUDIOBLENDS; columnIndex++)
							{
								aSeqDesc.anim[rowIndex][columnIndex] = this.theInputFileReader.ReadInt16();
							}
						}

						aSeqDesc.movementIndex = this.theInputFileReader.ReadInt32();
						aSeqDesc.groupSize[0] = this.theInputFileReader.ReadInt32();
						aSeqDesc.groupSize[1] = this.theInputFileReader.ReadInt32();
						aSeqDesc.paramIndex[0] = this.theInputFileReader.ReadInt32();
						aSeqDesc.paramIndex[1] = this.theInputFileReader.ReadInt32();
						aSeqDesc.paramStart[0] = this.theInputFileReader.ReadSingle();
						aSeqDesc.paramStart[1] = this.theInputFileReader.ReadSingle();
						aSeqDesc.paramEnd[0] = this.theInputFileReader.ReadSingle();
						aSeqDesc.paramEnd[1] = this.theInputFileReader.ReadSingle();
						aSeqDesc.paramParent = this.theInputFileReader.ReadInt32();

						aSeqDesc.sequenceGroup = this.theInputFileReader.ReadInt32();

						aSeqDesc.fadeInTime = this.theInputFileReader.ReadSingle();
						aSeqDesc.fadeOutTime = this.theInputFileReader.ReadSingle();

						aSeqDesc.entryNodeIndex = this.theInputFileReader.ReadInt32();
						aSeqDesc.exitNodeIndex = this.theInputFileReader.ReadInt32();
						aSeqDesc.nodeFlags = this.theInputFileReader.ReadInt32();

						aSeqDesc.entryPhase = this.theInputFileReader.ReadSingle();
						aSeqDesc.exitPhase = this.theInputFileReader.ReadSingle();
						aSeqDesc.lastFrame = this.theInputFileReader.ReadSingle();

						aSeqDesc.nextSeq = this.theInputFileReader.ReadInt32();
						aSeqDesc.pose = this.theInputFileReader.ReadInt32();

						aSeqDesc.ikRuleCount = this.theInputFileReader.ReadInt32();
						aSeqDesc.autoLayerCount = this.theInputFileReader.ReadInt32();
						aSeqDesc.autoLayerOffset = this.theInputFileReader.ReadInt32();
						aSeqDesc.weightOffset = this.theInputFileReader.ReadInt32();
						aSeqDesc.poseKeyOffset = this.theInputFileReader.ReadInt32();

						aSeqDesc.ikLockCount = this.theInputFileReader.ReadInt32();
						aSeqDesc.ikLockOffset = this.theInputFileReader.ReadInt32();
						aSeqDesc.keyValueOffset = this.theInputFileReader.ReadInt32();
						aSeqDesc.keyValueSize = this.theInputFileReader.ReadInt32();

						for (int x = 0; x < aSeqDesc.unused.Length; x++)
						{
							aSeqDesc.unused[x] = this.theInputFileReader.ReadInt32();
						}

						//NOTE: Not sure why these bytes were ever included; they are always zeroes.
						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition + 3584, SeekOrigin.Begin);

						this.theMdlFileData.theSequenceDescs.Add(aSeqDesc);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aSeqDesc.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aSeqDesc.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);
							if (string.IsNullOrEmpty(aSeqDesc.theName))
							{
								if (this.theMdlFileData.localSequenceCount == 1)
								{
									aSeqDesc.theName = "idle";
								}
								else
								{
									aSeqDesc.theName = "";
								}
							}

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theName = " + aSeqDesc.theName);
						}
						else
						{
							aSeqDesc.theName = "";
						}

						if (aSeqDesc.activityNameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aSeqDesc.theActivityName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName = " + aSeqDesc.theActivityName);
							}
						}
						else
						{
							aSeqDesc.theActivityName = "";
						}

						//NOTE: MDL35 and MDL36 aSeqDesc.eventOffset is really "events plus weights offset", so set stream position here for weights in case there are no events.
						this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin);
						//Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
						this.ReadEvents(seqInputFileStreamPosition, aSeqDesc);
						//Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
						//'Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)
						//NOTE: MDL35 and MDL36 only use events and weights, and the weights are always immediately after events.
						this.ReadMdlAnimBoneWeights(this.theInputFileReader.BaseStream.Position, aSeqDesc);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc");
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
			if (this.theMdlFileData.sequenceGroupCount > 0)
			{
				long sequenceGroupInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.sequenceGroupOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theSequenceGroups = new List<SourceMdlSequenceGroup37>(this.theMdlFileData.sequenceGroupCount);
					for (int sequenceGroupIndex = 0; sequenceGroupIndex < this.theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
					{
						sequenceGroupInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlSequenceGroup37 aSequenceGroup = new SourceMdlSequenceGroup37();

						aSequenceGroup.nameOffset = this.theInputFileReader.ReadInt32();
						aSequenceGroup.fileNameOffset = this.theInputFileReader.ReadInt32();
						aSequenceGroup.cacheOffset = this.theInputFileReader.ReadInt32();
						aSequenceGroup.data = this.theInputFileReader.ReadInt32();

						if (this.theMdlFileData.version == 35)
						{
							for (int x = 0; x < aSequenceGroup.unknown.Length; x++)
							{
								aSequenceGroup.unknown[x] = this.theInputFileReader.ReadInt32();
							}
						}

						this.theMdlFileData.theSequenceGroups.Add(aSequenceGroup);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aSequenceGroup.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(sequenceGroupInputFileStreamPosition + aSequenceGroup.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aSequenceGroup.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequenceGroup.theName = " + aSequenceGroup.theName);
						}
						else
						{
							aSequenceGroup.theName = "";
						}

						if (aSequenceGroup.fileNameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(sequenceGroupInputFileStreamPosition + aSequenceGroup.fileNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aSequenceGroup.theFileName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequenceGroup.theFileName = " + aSequenceGroup.theFileName);
							}
						}
						else
						{
							aSequenceGroup.theFileName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceGroups " + this.theMdlFileData.theSequenceGroups.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSequenceGroups alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadTransitions()
		{
			if (this.theMdlFileData.transitionCount > 0)
			{
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.transitionOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theTransitions = new List<List<int>>(this.theMdlFileData.transitionCount);
					for (int entryNodeIndex = 0; entryNodeIndex < this.theMdlFileData.transitionCount; entryNodeIndex++)
					{
						List<int> exitNodeTransitions = new List<int>(this.theMdlFileData.transitionCount);
						for (int exitNodeIndex = 0; exitNodeIndex < this.theMdlFileData.transitionCount; exitNodeIndex++)
						{
							int aTransitionValue = this.theInputFileReader.ReadByte();


							exitNodeTransitions.Add(aTransitionValue);
						}
						this.theMdlFileData.theTransitions.Add(exitNodeTransitions);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTransitions " + this.theMdlFileData.theTransitions.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTransitions alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadLocalAnimationDescs()
		{
			if (this.theMdlFileData.animationCount > 0)
			{
				long animationDescInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.animationOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theAnimationDescs = new List<SourceMdlAnimationDesc36>(this.theMdlFileData.animationCount);
					for (int i = 0; i < this.theMdlFileData.animationCount; i++)
					{
						animationDescInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlAnimationDesc36 anAnimationDesc = new SourceMdlAnimationDesc36();

						anAnimationDesc.nameOffset = this.theInputFileReader.ReadInt32();
						anAnimationDesc.fps = this.theInputFileReader.ReadSingle();
						anAnimationDesc.flags = this.theInputFileReader.ReadInt32();
						anAnimationDesc.frameCount = this.theInputFileReader.ReadInt32();
						anAnimationDesc.movementCount = this.theInputFileReader.ReadInt32();
						anAnimationDesc.movementOffset = this.theInputFileReader.ReadInt32();

						anAnimationDesc.bbMin.x = this.theInputFileReader.ReadSingle();
						anAnimationDesc.bbMin.y = this.theInputFileReader.ReadSingle();
						anAnimationDesc.bbMin.z = this.theInputFileReader.ReadSingle();
						anAnimationDesc.bbMax.x = this.theInputFileReader.ReadSingle();
						anAnimationDesc.bbMax.y = this.theInputFileReader.ReadSingle();
						anAnimationDesc.bbMax.z = this.theInputFileReader.ReadSingle();

						anAnimationDesc.animOffset = this.theInputFileReader.ReadInt32();

						anAnimationDesc.ikRuleCount = this.theInputFileReader.ReadInt32();
						anAnimationDesc.ikRuleOffset = this.theInputFileReader.ReadInt32();

						for (int x = 0; x < anAnimationDesc.unused.Length; x++)
						{
							anAnimationDesc.unused[x] = this.theInputFileReader.ReadInt32();
						}

						this.theMdlFileData.theAnimationDescs.Add(anAnimationDesc);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc")

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (anAnimationDesc.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							anAnimationDesc.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);
							//If Me.theMdlFileData.theFirstAnimationDesc Is Nothing AndAlso anAnimationDesc.theName(0) <> "@" Then
							//	Me.theMdlFileData.theFirstAnimationDesc = anAnimationDesc
							//End If
							if (anAnimationDesc.theName[0] == '@')
							{
								anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 1);
							}
							if (string.IsNullOrEmpty(anAnimationDesc.theName))
							{
								if (this.theMdlFileData.animationCount == 1)
								{
									anAnimationDesc.theName = "a_idle";
								}
								else
								{
									anAnimationDesc.theName = "";
								}
							}

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAnimationDesc.theName = " + anAnimationDesc.theName);
							}
						}
						else
						{
							anAnimationDesc.theName = "";
						}

						this.ReadAnimations(animationDescInputFileStreamPosition, anAnimationDesc);
						this.ReadMdlMovements(animationDescInputFileStreamPosition, anAnimationDesc);
						this.ReadMdlIkRules(animationDescInputFileStreamPosition, anAnimationDesc);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs " + this.theMdlFileData.theAnimationDescs.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAnimationDescs alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadBodyParts()
		{
			if (this.theMdlFileData.bodyPartCount > 0)
			{
				long bodyPartInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.bodyPartOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theBodyParts = new List<SourceMdlBodyPart37>(this.theMdlFileData.bodyPartCount);
					for (int i = 0; i < this.theMdlFileData.bodyPartCount; i++)
					{
						bodyPartInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart37 aBodyPart = new SourceMdlBodyPart37();

						aBodyPart.nameOffset = this.theInputFileReader.ReadInt32();
						aBodyPart.modelCount = this.theInputFileReader.ReadInt32();
						aBodyPart.@base = this.theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theBodyParts.Add(aBodyPart);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aBodyPart.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aBodyPart.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBodyPart.theName = " + aBodyPart.theName);
							}
						}
						else
						{
							aBodyPart.theName = "";
						}

						this.ReadModels(bodyPartInputFileStreamPosition, aBodyPart);
						//NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts *and* models.
						if (i == this.theMdlFileData.bodyPartCount - 1)
						{
							this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, this.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment");
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
			if (this.theMdlFileData.flexDescCount > 0)
			{
				long flexDescInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.flexDescOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theFlexDescs = new List<SourceMdlFlexDesc>(this.theMdlFileData.flexDescCount);
					for (int i = 0; i < this.theMdlFileData.flexDescCount; i++)
					{
						flexDescInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlFlexDesc aFlexDesc = new SourceMdlFlexDesc();

						aFlexDesc.nameOffset = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theFlexDescs.Add(aFlexDesc);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aFlexDesc.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(flexDescInputFileStreamPosition + aFlexDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aFlexDesc.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexDesc.theName = " + aFlexDesc.theName);
							}
						}
						else
						{
							aFlexDesc.theName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs " + theMdlFileData.theFlexDescs.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexDescs alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadFlexControllers()
		{
			if (this.theMdlFileData.flexControllerCount > 0)
			{
				long flexControllerInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.flexControllerOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theFlexControllers = new List<SourceMdlFlexController>(this.theMdlFileData.flexControllerCount);
					for (int i = 0; i < this.theMdlFileData.flexControllerCount; i++)
					{
						flexControllerInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlFlexController aFlexController = new SourceMdlFlexController();

						aFlexController.typeOffset = this.theInputFileReader.ReadInt32();
						aFlexController.nameOffset = this.theInputFileReader.ReadInt32();
						aFlexController.localToGlobal = this.theInputFileReader.ReadInt32();
						aFlexController.min = this.theInputFileReader.ReadSingle();
						aFlexController.max = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theFlexControllers.Add(aFlexController);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aFlexController.typeOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.typeOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aFlexController.theType = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theType = " + aFlexController.theType);
							}
						}
						else
						{
							aFlexController.theType = "";
						}

						if (aFlexController.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aFlexController.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theName = " + aFlexController.theName);
							}
						}
						else
						{
							aFlexController.theName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//If Me.theMdlFileData.theFlexControllers.Count > 0 Then
					//	Me.theMdlFileData.theModelCommandIsUsed = True
					//End If

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers " + theMdlFileData.theFlexControllers.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexControllers alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadFlexRules()
		{
			if (this.theMdlFileData.flexRuleCount > 0)
			{
				long flexRuleInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.flexRuleOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theFlexRules = new List<SourceMdlFlexRule>(this.theMdlFileData.flexRuleCount);
					for (int i = 0; i < this.theMdlFileData.flexRuleCount; i++)
					{
						flexRuleInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlFlexRule aFlexRule = new SourceMdlFlexRule();

						aFlexRule.flexIndex = this.theInputFileReader.ReadInt32();
						aFlexRule.opCount = this.theInputFileReader.ReadInt32();
						aFlexRule.opOffset = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theFlexRules.Add(aFlexRule);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.theMdlFileData.theFlexDescs[aFlexRule.flexIndex].theDescIsUsedByFlexRule = true;
						this.ReadFlexOps(flexRuleInputFileStreamPosition, aFlexRule);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					//If Me.theMdlFileData.theFlexRules.Count > 0 Then
					//	Me.theMdlFileData.theModelCommandIsUsed = True
					//End If

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + theMdlFileData.theFlexRules.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexRules alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadIkChains()
		{
			if (this.theMdlFileData.ikChainCount > 0)
			{
				long ikChainInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.ikChainOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theIkChains = new List<SourceMdlIkChain37>(this.theMdlFileData.ikChainCount);
					for (int i = 0; i < this.theMdlFileData.ikChainCount; i++)
					{
						ikChainInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlIkChain37 anIkChain = new SourceMdlIkChain37();

						anIkChain.nameOffset = this.theInputFileReader.ReadInt32();
						anIkChain.linkType = this.theInputFileReader.ReadInt32();
						anIkChain.linkCount = this.theInputFileReader.ReadInt32();
						anIkChain.linkOffset = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theIkChains.Add(anIkChain);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (anIkChain.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							anIkChain.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkChain.theName = " + anIkChain.theName);
							}
						}
						else
						{
							anIkChain.theName = "";
						}

						this.ReadIkLinks(ikChainInputFileStreamPosition, anIkChain);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkChains " + theMdlFileData.theIkChains.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theIkChains alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadIkLocks()
		{
			if (this.theMdlFileData.localIkAutoPlayLockCount > 0)
			{
				//Dim ikChainInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theIkLocks = new List<SourceMdlIkLock37>(this.theMdlFileData.localIkAutoPlayLockCount);
					for (int i = 0; i < this.theMdlFileData.localIkAutoPlayLockCount; i++)
					{
						//ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlIkLock37 anIkLock = new SourceMdlIkLock37();

						anIkLock.chainIndex = this.theInputFileReader.ReadInt32();
						anIkLock.posWeight = this.theInputFileReader.ReadSingle();
						anIkLock.localQWeight = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theIkLocks.Add(anIkLock);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks " + theMdlFileData.theIkLocks.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theIkLocks alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadMouths()
		{
			if (this.theMdlFileData.mouthCount > 0)
			{
				//Dim mouthInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.mouthOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theMouths = new List<SourceMdlMouth>(this.theMdlFileData.mouthCount);
					for (int i = 0; i < this.theMdlFileData.mouthCount; i++)
					{
						//mouthInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlMouth aMouth = new SourceMdlMouth();
						aMouth.boneIndex = this.theInputFileReader.ReadInt32();
						aMouth.forward.x = this.theInputFileReader.ReadSingle();
						aMouth.forward.y = this.theInputFileReader.ReadSingle();
						aMouth.forward.z = this.theInputFileReader.ReadSingle();
						aMouth.flexDescIndex = this.theInputFileReader.ReadInt32();
						this.theMdlFileData.theMouths.Add(aMouth);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					if (this.theMdlFileData.theMouths.Count > 0)
					{
						//Me.theMdlFileData.theModelCommandIsUsed = True
						// Seems like any $model can have these lines, so simply assign them to first one.
						this.theMdlFileData.theBodyParts[0].theModelCommandIsUsed = true;
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMouths " + theMdlFileData.theMouths.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theMouths alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadPoseParamDescs()
		{
			if (this.theMdlFileData.localPoseParamaterCount > 0)
			{
				long poseInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localPoseParameterOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.thePoseParamDescs = new List<SourceMdlPoseParamDesc>(this.theMdlFileData.localPoseParamaterCount);
					for (int i = 0; i < this.theMdlFileData.localPoseParamaterCount; i++)
					{
						poseInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlPoseParamDesc aPoseParamDesc = new SourceMdlPoseParamDesc();
						aPoseParamDesc.nameOffset = this.theInputFileReader.ReadInt32();
						aPoseParamDesc.flags = this.theInputFileReader.ReadInt32();
						aPoseParamDesc.startingValue = this.theInputFileReader.ReadSingle();
						aPoseParamDesc.endingValue = this.theInputFileReader.ReadSingle();
						aPoseParamDesc.loopingRange = this.theInputFileReader.ReadSingle();
						this.theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aPoseParamDesc.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(poseInputFileStreamPosition + aPoseParamDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aPoseParamDesc.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aPoseParamDesc.theName = " + aPoseParamDesc.theName);
							}
						}
						else
						{
							aPoseParamDesc.theName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs " + theMdlFileData.thePoseParamDescs.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.thePoseParamDescs alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadTextures()
		{
			if (this.theMdlFileData.textureCount > 0)
			{
				long textureInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.textureOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theTextures = new List<SourceMdlTexture37>(this.theMdlFileData.textureCount);
				for (int i = 0; i < this.theMdlFileData.textureCount; i++)
				{
					textureInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlTexture37 aTexture = new SourceMdlTexture37();

					aTexture.fileNameOffset = this.theInputFileReader.ReadInt32();
					aTexture.flags = this.theInputFileReader.ReadInt32();
					aTexture.width = this.theInputFileReader.ReadSingle();
					aTexture.height = this.theInputFileReader.ReadSingle();
					aTexture.worldUnitsPerU = this.theInputFileReader.ReadSingle();
					aTexture.worldUnitsPerV = this.theInputFileReader.ReadSingle();
					for (int x = 0; x < aTexture.unknown.Length; x++)
					{
						aTexture.unknown[x] = this.theInputFileReader.ReadInt32();
					}

					this.theMdlFileData.theTextures.Add(aTexture);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					if (aTexture.fileNameOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.fileNameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

						aTexture.theFileName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

						// Convert all forward slashes to backward slashes.
						aTexture.theFileName = FileManager.GetNormalizedPathFileName(aTexture.theFileName);

						fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
						if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theName = " + aTexture.theFileName);
						}
					}
					else
					{
						aTexture.theFileName = "";
					}

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures");

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment");
			}
		}

		public void ReadTexturePaths()
		{
			if (this.theMdlFileData.texturePathCount > 0)
			{
				long texturePathInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.texturePathOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theTexturePaths = new List<string>(this.theMdlFileData.texturePathCount);
				int texturePathOffset = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string aTexturePath = null;
				for (int i = 0; i < this.theMdlFileData.texturePathCount; i++)
				{
					texturePathInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
	//				Dim aTexturePath As String
					texturePathOffset = this.theInputFileReader.ReadInt32();

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					if (texturePathOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin);
						fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

						aTexturePath = FileManager.ReadNullTerminatedString(this.theInputFileReader);

						//TEST: Convert all forward slashes to backward slashes.
						aTexturePath = FileManager.GetNormalizedPathFileName(aTexturePath);

						fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
						if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath = " + aTexturePath);
						}
					}
					else
					{
						aTexturePath = "";
					}
					this.theMdlFileData.theTexturePaths.Add(aTexturePath);

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths");

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTexturePaths alignment");
			}
		}

		public void ReadSkinFamilies()
		{
			if (this.theMdlFileData.skinFamilyCount > 0 && this.theMdlFileData.skinReferenceCount > 0)
			{
				long skinFamilyInputFileStreamPosition = 0;
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.skinOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theSkinFamilies = new List<List<short>>(this.theMdlFileData.skinFamilyCount);
				for (int i = 0; i < this.theMdlFileData.skinFamilyCount; i++)
				{
					skinFamilyInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					List<short> aSkinFamily = new List<short>();

					for (int j = 0; j < this.theMdlFileData.skinReferenceCount; j++)
					{
						short aSkinRef = this.theInputFileReader.ReadInt16();
						aSkinFamily.Add(aSkinRef);
					}

					this.theMdlFileData.theSkinFamilies.Add(aSkinFamily);

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

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies");

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment");
			}
		}

		public void ReadKeyValues()
		{
			if (this.theMdlFileData.keyValueSize > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				char nullChar = '\0';

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.keyValueOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				//NOTE: Use -1 to drop the null terminator character.
				this.theMdlFileData.theKeyValuesText = new string(this.theInputFileReader.ReadChars(this.theMdlFileData.keyValueSize - 1));
				//NOTE: Read the null terminator character.
				nullChar = this.theInputFileReader.ReadChar();

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theKeyValuesText = " + this.theMdlFileData.theKeyValuesText);

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theKeyValuesText alignment");
			}
		}

		//Public Sub ReadFinalBytesAlignment()
		//	Me.theMdlFileData.theFileSeekLog.LogAndAlignFromFileSeekLogEnd(Me.theInputFileReader, 4, "Final bytes alignment")
		//End Sub

		public void ReadUnreadBytes()
		{
			this.theMdlFileData.theFileSeekLog.LogUnreadBytes(this.theInputFileReader);
		}

		public void PostProcess()
		{
			if (this.theMdlFileData.theBodyParts != null)
			{
				foreach (SourceMdlBodyPart37 aBodyPart in this.theMdlFileData.theBodyParts)
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

			if (this.theMdlFileData.theFlexDescs != null && this.theMdlFileData.theFlexDescs.Count > 0)
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

				flexDescToFlexFrames = new List<List<FlexFrame37>>(this.theMdlFileData.theFlexDescs.Count);
				for (int x = 0; x < this.theMdlFileData.theFlexDescs.Count; x++)
				{
					List<FlexFrame37> flexFrameList = new List<FlexFrame37>();
					flexDescToFlexFrames.Add(flexFrameList);
				}

				cumulativebodyPartVertexIndexStart = 0;
				for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = this.theMdlFileData.theBodyParts[bodyPartIndex];

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

									meshVertexIndexStart = this.theMdlFileData.theBodyParts[bodyPartIndex].theModels[modelIndex].theMeshes[meshIndex].vertexIndexStart;

									if (aMesh.theFlexes != null && aMesh.theFlexes.Count > 0)
									{
										for (int flexIndex = 0; flexIndex < aMesh.theFlexes.Count; flexIndex++)
										{
											aFlex = aMesh.theFlexes[flexIndex];

											aFlexFrame = null;
											if (flexDescToFlexFrames[aFlex.flexDescIndex] != null)
											{
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of flexDescToFlexFrames(aFlex.flexDescIndex).Count for every iteration:
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

												aFlexFrame.flexName = this.theMdlFileData.theFlexDescs[aFlex.flexDescIndex].theName;
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
												this.theMdlFileData.theFlexDescs[aFlex.flexDescIndex].theDescIsUsedByFlex = true;

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
				this.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				axisInterpBoneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				aBone.theAxisInterpBone = new SourceMdlAxisInterpBone();
				aBone.theAxisInterpBone.control = this.theInputFileReader.ReadInt32();
				for (int x = 0; x < aBone.theAxisInterpBone.pos.Length; x++)
				{
					aBone.theAxisInterpBone.pos[x].x = this.theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.pos[x].y = this.theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.pos[x].z = this.theInputFileReader.ReadSingle();
				}
				for (int x = 0; x < aBone.theAxisInterpBone.quat.Length; x++)
				{
					aBone.theAxisInterpBone.quat[x].x = this.theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.quat[x].y = this.theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.quat[x].z = this.theInputFileReader.ReadSingle();
					aBone.theAxisInterpBone.quat[x].z = this.theInputFileReader.ReadSingle();
				}

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				//If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
				//	Me.ReadTriggers(axisInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
				//End If

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone");
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
				this.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				quatInterpBoneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				aBone.theQuatInterpBone = new SourceMdlQuatInterpBone();
				aBone.theQuatInterpBone.controlBoneIndex = this.theInputFileReader.ReadInt32();
				aBone.theQuatInterpBone.triggerCount = this.theInputFileReader.ReadInt32();
				aBone.theQuatInterpBone.triggerOffset = this.theInputFileReader.ReadInt32();

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				if (aBone.theQuatInterpBone.triggerCount > 0 && aBone.theQuatInterpBone.triggerOffset != 0)
				{
					this.ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone);
				}

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone");
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
				this.theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aQuatInterpBone.theTriggers = new List<SourceMdlQuatInterpBoneInfo>(aQuatInterpBone.triggerCount);
				for (int j = 0; j < aQuatInterpBone.triggerCount; j++)
				{
					SourceMdlQuatInterpBoneInfo aTrigger = new SourceMdlQuatInterpBoneInfo();

					aTrigger.inverseToleranceAngle = this.theInputFileReader.ReadSingle();

					aTrigger.trigger = new SourceQuaternion();
					aTrigger.trigger.x = this.theInputFileReader.ReadSingle();
					aTrigger.trigger.y = this.theInputFileReader.ReadSingle();
					aTrigger.trigger.z = this.theInputFileReader.ReadSingle();
					aTrigger.trigger.w = this.theInputFileReader.ReadSingle();

					aTrigger.pos = new SourceVector();
					aTrigger.pos.x = this.theInputFileReader.ReadSingle();
					aTrigger.pos.y = this.theInputFileReader.ReadSingle();
					aTrigger.pos.z = this.theInputFileReader.ReadSingle();

					aTrigger.quat = new SourceQuaternion();
					aTrigger.quat.x = this.theInputFileReader.ReadSingle();
					aTrigger.quat.y = this.theInputFileReader.ReadSingle();
					aTrigger.quat.z = this.theInputFileReader.ReadSingle();
					aTrigger.quat.w = this.theInputFileReader.ReadSingle();

					aQuatInterpBone.theTriggers.Add(aTrigger);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aQuatInterpBone.theTriggers " + aQuatInterpBone.theTriggers.Count.ToString());
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
					this.theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aHitboxSet.theHitboxes = new List<SourceMdlHitbox37>(aHitboxSet.hitboxCount);
					for (int j = 0; j < aHitboxSet.hitboxCount; j++)
					{
						hitboxInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlHitbox37 aHitbox = new SourceMdlHitbox37();

						aHitbox.boneIndex = this.theInputFileReader.ReadInt32();
						aHitbox.groupIndex = this.theInputFileReader.ReadInt32();
						aHitbox.boundingBoxMin.x = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.y = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.z = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.x = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.y = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.z = this.theInputFileReader.ReadSingle();
						aHitbox.nameOffset = this.theInputFileReader.ReadInt32();
						for (int x = 0; x < aHitbox.unused.Length; x++)
						{
							aHitbox.unused[x] = this.theInputFileReader.ReadByte();
						}

						aHitboxSet.theHitboxes.Add(aHitbox);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aHitbox.nameOffset != 0)
						{
							//NOTE: The nameOffset is absolute offset in studiomdl.
							this.theInputFileReader.BaseStream.Seek(aHitbox.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aHitbox.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitbox.theName = " + aHitbox.theName);
							}
						}
						else
						{
							aHitbox.theName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes " + aHitboxSet.theHitboxes.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment");
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

					this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.poseKeyOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aSeqDesc.thePoseKeys = new List<double>(poseKeyCount);
					for (int j = 0; j < poseKeyCount; j++)
					{
						poseKeyInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						double aPoseKey = this.theInputFileReader.ReadSingle();
						aSeqDesc.thePoseKeys.Add(aPoseKey);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.thePoseKeys " + aSeqDesc.thePoseKeys.Count.ToString());
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

					this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aSeqDesc.theEvents = new List<SourceMdlEvent37>(aSeqDesc.eventCount);
					for (int j = 0; j < aSeqDesc.eventCount; j++)
					{
						//eventInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlEvent37 anEvent = new SourceMdlEvent37();

						anEvent.cycle = this.theInputFileReader.ReadSingle();
						anEvent.eventIndex = this.theInputFileReader.ReadInt32();
						anEvent.eventType = this.theInputFileReader.ReadInt32();
						for (int x = 0; x < anEvent.options.Length; x++)
						{
							anEvent.options[x] = this.theInputFileReader.ReadChar();
						}

						aSeqDesc.theEvents.Add(anEvent);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theEvents " + aSeqDesc.theEvents.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theEvents alignment");
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

					this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aSeqDesc.theAutoLayers = new List<SourceMdlAutoLayer37>(aSeqDesc.autoLayerCount);
					for (int j = 0; j < aSeqDesc.autoLayerCount; j++)
					{
						autoLayerInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlAutoLayer37 anAutoLayer = new SourceMdlAutoLayer37();

						anAutoLayer.sequenceIndex = this.theInputFileReader.ReadInt32();
						anAutoLayer.flags = this.theInputFileReader.ReadInt32();
						anAutoLayer.influenceStart = this.theInputFileReader.ReadSingle();
						anAutoLayer.influencePeak = this.theInputFileReader.ReadSingle();
						anAutoLayer.influenceTail = this.theInputFileReader.ReadSingle();
						anAutoLayer.influenceEnd = this.theInputFileReader.ReadSingle();

						aSeqDesc.theAutoLayers.Add(anAutoLayer);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAutoLayers " + aSeqDesc.theAutoLayers.Count.ToString());
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
			if (this.theMdlFileData.boneCount > 0)
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
					this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aSeqDesc.theBoneWeightsAreDefault = true;
					aSeqDesc.theBoneWeights = new List<double>(this.theMdlFileData.boneCount);
					for (int j = 0; j < this.theMdlFileData.boneCount; j++)
					{
						weightListInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						double anAnimBoneWeight = this.theInputFileReader.ReadSingle();
						aSeqDesc.theBoneWeights.Add(anAnimBoneWeight);

						if (anAnimBoneWeight != 1)
						{
							aSeqDesc.theBoneWeightsAreDefault = false;
						}

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theBoneWeights " + aSeqDesc.theBoneWeights.Count.ToString());
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

					this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aSeqDesc.theIkLocks = new List<SourceMdlIkLock37>(aSeqDesc.ikLockCount);
					for (int j = 0; j < aSeqDesc.ikLockCount; j++)
					{
						lockInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlIkLock37 anIkLock = new SourceMdlIkLock37();

						anIkLock.chainIndex = this.theInputFileReader.ReadInt32();
						anIkLock.posWeight = this.theInputFileReader.ReadSingle();
						anIkLock.localQWeight = this.theInputFileReader.ReadSingle();

						aSeqDesc.theIkLocks.Add(anIkLock);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theIkLocks " + aSeqDesc.theIkLocks.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theIkLocks alignment");
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

					this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aSeqDesc.theKeyValues = FileManager.ReadNullTerminatedString(this.theInputFileReader);

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theKeyValues");

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theKeyValues alignment");
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
					this.theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					animationValuesEnd = 0;

					anAnimationDesc.theAnimations = new List<SourceMdlAnimation37>(this.theMdlFileData.theBones.Count);
					for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
					{
						animationInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlAnimation37 anAnimation = new SourceMdlAnimation37();

						anAnimation.flags = this.theInputFileReader.ReadInt32();
						if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0)
						{
							anAnimation.animationValueOffsets[0] = this.theInputFileReader.ReadInt32();
							anAnimation.animationValueOffsets[1] = this.theInputFileReader.ReadInt32();
							anAnimation.animationValueOffsets[2] = this.theInputFileReader.ReadInt32();
						}
						else
						{
							anAnimation.position = new SourceVector();
							anAnimation.position.x = this.theInputFileReader.ReadSingle();
							anAnimation.position.y = this.theInputFileReader.ReadSingle();
							anAnimation.position.z = this.theInputFileReader.ReadSingle();
						}
						if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0)
						{
							anAnimation.animationValueOffsets[3] = this.theInputFileReader.ReadInt32();
							anAnimation.animationValueOffsets[4] = this.theInputFileReader.ReadInt32();
							anAnimation.animationValueOffsets[5] = this.theInputFileReader.ReadInt32();
							anAnimation.unused = this.theInputFileReader.ReadInt32();
						}
						else
						{
							anAnimation.rotationQuat = new SourceQuaternion();
							anAnimation.rotationQuat.x = this.theInputFileReader.ReadSingle();
							anAnimation.rotationQuat.y = this.theInputFileReader.ReadSingle();
							anAnimation.rotationQuat.z = this.theInputFileReader.ReadSingle();
							anAnimation.rotationQuat.w = this.theInputFileReader.ReadSingle();
						}

						anAnimationDesc.theAnimations.Add(anAnimation);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 0, anAnimationDesc.frameCount, ref animationValuesEnd);
							this.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 1, anAnimationDesc.frameCount, ref animationValuesEnd);
							this.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 2, anAnimationDesc.frameCount, ref animationValuesEnd);
						}
						if ((anAnimation.flags & SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 3, anAnimationDesc.frameCount, ref animationValuesEnd);
							this.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 4, anAnimationDesc.frameCount, ref animationValuesEnd);
							this.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 5, anAnimationDesc.frameCount, ref animationValuesEnd);
							anAnimation.unused = this.theInputFileReader.ReadInt32();
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					if (animationValuesEnd > 0)
					{
						this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, animationValuesEnd, 4, "anAnimation.theAnimationValues alignment");
					}

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations " + anAnimationDesc.theAnimations.Count.ToString())

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theAnimations alignment");
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
					this.theInputFileReader.BaseStream.Seek(animationInputFileStreamPosition + anAnimation.animationValueOffsets[offsetIndex], SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					frameCountRemainingToBeChecked = frameCount;
					while (frameCountRemainingToBeChecked > 0)
					{
						SourceMdlAnimationValue10 animValue = new SourceMdlAnimationValue10();
						animValue.value = this.theInputFileReader.ReadInt16();
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
							animValue2.value = this.theInputFileReader.ReadInt16();
							animValues.Add(animValue2);
						}
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation.theAnimationValues");
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
					this.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.movementOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					anAnimationDesc.theMovements = new List<SourceMdlMovement>(anAnimationDesc.movementCount);
					for (int j = 0; j < anAnimationDesc.movementCount; j++)
					{
						movementInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlMovement aMovement = new SourceMdlMovement();

						aMovement.endframeIndex = this.theInputFileReader.ReadInt32();
						aMovement.motionFlags = this.theInputFileReader.ReadInt32();
						aMovement.v0 = this.theInputFileReader.ReadSingle();
						aMovement.v1 = this.theInputFileReader.ReadSingle();
						aMovement.angle = this.theInputFileReader.ReadSingle();

						aMovement.vector = new SourceVector();
						aMovement.vector.x = this.theInputFileReader.ReadSingle();
						aMovement.vector.y = this.theInputFileReader.ReadSingle();
						aMovement.vector.z = this.theInputFileReader.ReadSingle();
						aMovement.position = new SourceVector();
						aMovement.position.x = this.theInputFileReader.ReadSingle();
						aMovement.position.y = this.theInputFileReader.ReadSingle();
						aMovement.position.z = this.theInputFileReader.ReadSingle();

						anAnimationDesc.theMovements.Add(aMovement);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theMovements " + anAnimationDesc.theMovements.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theMovements alignment");
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
					this.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					anAnimationDesc.theIkRules = new List<SourceMdlIkRule37>(anAnimationDesc.ikRuleCount);
					for (int j = 0; j < anAnimationDesc.ikRuleCount; j++)
					{
						ikRuleInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlIkRule37 anIkRule = new SourceMdlIkRule37();

						anIkRule.index = this.theInputFileReader.ReadInt32();
						anIkRule.type = this.theInputFileReader.ReadInt32();
						anIkRule.chain = this.theInputFileReader.ReadInt32();
						anIkRule.bone = this.theInputFileReader.ReadInt32();

						anIkRule.slot = this.theInputFileReader.ReadInt32();
						anIkRule.height = this.theInputFileReader.ReadSingle();
						anIkRule.radius = this.theInputFileReader.ReadSingle();
						anIkRule.floor = this.theInputFileReader.ReadSingle();

						anIkRule.pos = new SourceVector();
						anIkRule.pos.x = this.theInputFileReader.ReadSingle();
						anIkRule.pos.y = this.theInputFileReader.ReadSingle();
						anIkRule.pos.z = this.theInputFileReader.ReadSingle();
						anIkRule.q = new SourceQuaternion();
						anIkRule.q.x = this.theInputFileReader.ReadSingle();
						anIkRule.q.y = this.theInputFileReader.ReadSingle();
						anIkRule.q.z = this.theInputFileReader.ReadSingle();
						anIkRule.q.w = this.theInputFileReader.ReadSingle();

						anIkRule.weight = this.theInputFileReader.ReadSingle();
						anIkRule.group = this.theInputFileReader.ReadInt32();
						anIkRule.ikErrorIndexStart = this.theInputFileReader.ReadInt32();
						anIkRule.ikErrorOffset = this.theInputFileReader.ReadInt32();

						anIkRule.influenceStart = this.theInputFileReader.ReadSingle();
						anIkRule.influencePeak = this.theInputFileReader.ReadSingle();
						anIkRule.influenceTail = this.theInputFileReader.ReadSingle();
						anIkRule.influenceEnd = this.theInputFileReader.ReadSingle();

						anIkRule.commit = this.theInputFileReader.ReadSingle();
						anIkRule.contact = this.theInputFileReader.ReadSingle();
						anIkRule.pivot = this.theInputFileReader.ReadSingle();
						anIkRule.release = this.theInputFileReader.ReadSingle();

						anAnimationDesc.theIkRules.Add(anIkRule);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadMdlIkErrors(ikRuleInputFileStreamPosition, anIkRule, anAnimationDesc.frameCount);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkRules " + anAnimationDesc.theIkRules.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment");
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
					this.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.ikErrorOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					anIkRule.theIkErrors = new List<SourceMdlIkError37>(ikErrorCount);
					for (int j = 0; j < ikErrorCount; j++)
					{
						//ikErrorInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlIkError37 anIkError = new SourceMdlIkError37();

						anIkError.pos = new SourceVector();
						anIkError.pos.x = this.theInputFileReader.ReadSingle();
						anIkError.pos.y = this.theInputFileReader.ReadSingle();
						anIkError.pos.z = this.theInputFileReader.ReadSingle();
						anIkError.q = new SourceQuaternion();
						anIkError.q.x = this.theInputFileReader.ReadSingle();
						anIkError.q.y = this.theInputFileReader.ReadSingle();
						anIkError.q.z = this.theInputFileReader.ReadSingle();
						anIkError.q.w = this.theInputFileReader.ReadSingle();

						anIkRule.theIkErrors.Add(anIkError);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkErrors " + anIkRule.theIkErrors.Count.ToString());
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
					this.theInputFileReader.BaseStream.Seek(flexRuleInputFileStreamPosition + aFlexRule.opOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aFlexRule.theFlexOps = new List<SourceMdlFlexOp>(aFlexRule.opCount);
					for (int i = 0; i < aFlexRule.opCount; i++)
					{
						//flexRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlFlexOp aFlexOp = new SourceMdlFlexOp();

						aFlexOp.op = this.theInputFileReader.ReadInt32();
						if (aFlexOp.op == SourceMdlFlexOp.STUDIO_CONST)
						{
							aFlexOp.value = this.theInputFileReader.ReadSingle();
						}
						else
						{
							aFlexOp.index = this.theInputFileReader.ReadInt32();
							if (aFlexOp.op == SourceMdlFlexOp.STUDIO_FETCH2)
							{
								this.theMdlFileData.theFlexDescs[aFlexOp.index].theDescIsUsedByFlexRule = true;
							}
						}

						aFlexRule.theFlexOps.Add(aFlexOp);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexRule.theFlexOps " + aFlexRule.theFlexOps.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexOps alignment");
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
					this.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					anIkChain.theLinks = new List<SourceMdlIkLink37>(anIkChain.linkCount);
					for (int j = 0; j < anIkChain.linkCount; j++)
					{
						//ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlIkLink37 anIkLink = new SourceMdlIkLink37();

						anIkLink.boneIndex = this.theInputFileReader.ReadInt32();
						anIkLink.contact.x = this.theInputFileReader.ReadSingle();
						anIkLink.contact.y = this.theInputFileReader.ReadSingle();
						anIkLink.contact.z = this.theInputFileReader.ReadSingle();
						anIkLink.limits.x = this.theInputFileReader.ReadSingle();
						anIkLink.limits.y = this.theInputFileReader.ReadSingle();
						anIkLink.limits.z = this.theInputFileReader.ReadSingle();

						anIkChain.theLinks.Add(anIkLink);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks " + anIkChain.theLinks.Count.ToString());
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
					this.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aBodyPart.theModels = new List<SourceMdlModel37>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlModel37 aModel = new SourceMdlModel37();

						aModel.name = this.theInputFileReader.ReadChars(aModel.name.Length);
						aModel.theName = (new string(aModel.name)).Trim('\0');
						aModel.type = this.theInputFileReader.ReadInt32();
						aModel.boundingRadius = this.theInputFileReader.ReadSingle();
						aModel.meshCount = this.theInputFileReader.ReadInt32();
						aModel.meshOffset = this.theInputFileReader.ReadInt32();

						aModel.vertexCount = this.theInputFileReader.ReadInt32();
						aModel.vertexOffset = this.theInputFileReader.ReadInt32();
						aModel.tangentOffset = this.theInputFileReader.ReadInt32();

						aModel.attachmentCount = this.theInputFileReader.ReadInt32();
						aModel.attachmentOffset = this.theInputFileReader.ReadInt32();
						aModel.eyeballCount = this.theInputFileReader.ReadInt32();
						aModel.eyeballOffset = this.theInputFileReader.ReadInt32();

						for (int x = 0; x < aModel.unused.Length; x++)
						{
							aModel.unused[x] = this.theInputFileReader.ReadInt32();
						}

						aBodyPart.theModels.Add(aModel);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						//NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
						this.ReadEyeballs(modelInputFileStreamPosition, aModel);
						this.ReadMeshes(modelInputFileStreamPosition, aModel);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						//'NOTE: Although studiomdl source code indicates ALIGN64, it seems to align on 32.
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 32, "aModel.theVertexes pre-alignment (NOTE: Should end at: " + CStr(modelInputFileStreamPosition + aModel.vertexOffset - 1) + ")")
						this.ReadVertexes(modelInputFileStreamPosition, aModel);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theTangents pre-alignment (NOTE: Should end at: " + CStr(modelInputFileStreamPosition + aModel.tangentOffset - 1) + ")")
						this.ReadTangents(modelInputFileStreamPosition, aModel);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels " + aBodyPart.theModels.Count.ToString());
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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theEyeballs = new List<SourceMdlEyeball37>(aModel.eyeballCount);
					for (int eyeballIndex = 0; eyeballIndex < aModel.eyeballCount; eyeballIndex++)
					{
						eyeballInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlEyeball37 anEyeball = new SourceMdlEyeball37();

						anEyeball.nameOffset = this.theInputFileReader.ReadInt32();
						anEyeball.boneIndex = this.theInputFileReader.ReadInt32();
						anEyeball.org = new SourceVector();
						anEyeball.org.x = this.theInputFileReader.ReadSingle();
						anEyeball.org.y = this.theInputFileReader.ReadSingle();
						anEyeball.org.z = this.theInputFileReader.ReadSingle();
						anEyeball.zOffset = this.theInputFileReader.ReadSingle();
						anEyeball.radius = this.theInputFileReader.ReadSingle();
						anEyeball.up = new SourceVector();
						anEyeball.up.x = this.theInputFileReader.ReadSingle();
						anEyeball.up.y = this.theInputFileReader.ReadSingle();
						anEyeball.up.z = this.theInputFileReader.ReadSingle();
						anEyeball.forward = new SourceVector();
						anEyeball.forward.x = this.theInputFileReader.ReadSingle();
						anEyeball.forward.y = this.theInputFileReader.ReadSingle();
						anEyeball.forward.z = this.theInputFileReader.ReadSingle();
						anEyeball.texture = this.theInputFileReader.ReadInt32();

						anEyeball.irisMaterial = this.theInputFileReader.ReadInt32();
						anEyeball.irisScale = this.theInputFileReader.ReadSingle();
						anEyeball.glintMaterial = this.theInputFileReader.ReadInt32();

						anEyeball.upperFlexDesc[0] = this.theInputFileReader.ReadInt32();
						anEyeball.upperFlexDesc[1] = this.theInputFileReader.ReadInt32();
						anEyeball.upperFlexDesc[2] = this.theInputFileReader.ReadInt32();
						anEyeball.lowerFlexDesc[0] = this.theInputFileReader.ReadInt32();
						anEyeball.lowerFlexDesc[1] = this.theInputFileReader.ReadInt32();
						anEyeball.lowerFlexDesc[2] = this.theInputFileReader.ReadInt32();
						anEyeball.upperTarget[0] = this.theInputFileReader.ReadSingle();
						anEyeball.upperTarget[1] = this.theInputFileReader.ReadSingle();
						anEyeball.upperTarget[2] = this.theInputFileReader.ReadSingle();
						anEyeball.lowerTarget[0] = this.theInputFileReader.ReadSingle();
						anEyeball.lowerTarget[1] = this.theInputFileReader.ReadSingle();
						anEyeball.lowerTarget[2] = this.theInputFileReader.ReadSingle();

						anEyeball.upperLidFlexDesc = this.theInputFileReader.ReadInt32();
						anEyeball.lowerLidFlexDesc = this.theInputFileReader.ReadInt32();

						anEyeball.pitch[0] = this.theInputFileReader.ReadSingle();
						anEyeball.pitch[1] = this.theInputFileReader.ReadSingle();
						anEyeball.yaw[0] = this.theInputFileReader.ReadSingle();
						anEyeball.yaw[1] = this.theInputFileReader.ReadSingle();

						aModel.theEyeballs.Add(anEyeball);

						//NOTE: Set the default value to -1 to distinguish it from value assigned to it by ReadMeshes().
						anEyeball.theTextureIndex = -1;

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						//NOTE: The mdl file doesn't appear to store the eyeball name; studiomdl only uses it internally with eyelids.
						if (anEyeball.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(eyeballInputFileStreamPosition + anEyeball.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							anEyeball.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEyeball.theName = " + anEyeball.theName);
							}
						}
						else
						{
							anEyeball.theName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
						aBone = this.theMdlFileData.theBones[anEyeball.boneIndex];
						tmp = MathModule.VectorIRotate(aModel.theEyeballs[0].up, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3);
						if (tmp.y > 0.99 && tmp.y < 1.01)
						{
							this.theMdlFileData.theUpAxisYCommandWasUsed = true;
						}
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs " + aModel.theEyeballs.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aModel.theEyeballs alignment");
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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theMeshes = new List<SourceMdlMesh37>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						meshInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlMesh37 aMesh = new SourceMdlMesh37();

						aMesh.materialIndex = this.theInputFileReader.ReadInt32();
						aMesh.modelOffset = this.theInputFileReader.ReadInt32();

						aMesh.vertexCount = this.theInputFileReader.ReadInt32();
						aMesh.vertexIndexStart = this.theInputFileReader.ReadInt32();
						aMesh.flexCount = this.theInputFileReader.ReadInt32();
						aMesh.flexOffset = this.theInputFileReader.ReadInt32();
						aMesh.materialType = this.theInputFileReader.ReadInt32();
						aMesh.materialParam = this.theInputFileReader.ReadInt32();

						aMesh.id = this.theInputFileReader.ReadInt32();
						aMesh.center.x = this.theInputFileReader.ReadSingle();
						aMesh.center.y = this.theInputFileReader.ReadSingle();
						aMesh.center.z = this.theInputFileReader.ReadSingle();
						for (int x = 0; x < aMesh.unused.Length; x++)
						{
							aMesh.unused[x] = this.theInputFileReader.ReadInt32();
						}

						aModel.theMeshes.Add(aMesh);

						//' Fill-in eyeball texture index info.
						//If aMesh.materialType = 1 Then
						//	aModel.theEyeballs(aMesh.materialParam).theTextureIndex = aMesh.materialIndex
						//End If

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aMesh.flexCount > 0 && aMesh.flexOffset != 0)
						{
							this.ReadFlexes(meshInputFileStreamPosition, aMesh);
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes " + aModel.theMeshes.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment");
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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theVertexes = new List<SourceMdlVertex37>(aModel.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModel.vertexCount; vertexIndex++)
					{
						vertexInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlVertex37 aVertex = new SourceMdlVertex37();

						aVertex.boneWeight.weight[0] = this.theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[1] = this.theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[2] = this.theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[3] = this.theInputFileReader.ReadSingle();
						aVertex.boneWeight.bone[0] = this.theInputFileReader.ReadInt16();
						aVertex.boneWeight.bone[1] = this.theInputFileReader.ReadInt16();
						aVertex.boneWeight.bone[2] = this.theInputFileReader.ReadInt16();
						aVertex.boneWeight.bone[3] = this.theInputFileReader.ReadInt16();
						aVertex.boneWeight.boneCount = this.theInputFileReader.ReadInt16();
						aVertex.boneWeight.material = this.theInputFileReader.ReadInt16();
						aVertex.boneWeight.firstRef = this.theInputFileReader.ReadInt16();
						aVertex.boneWeight.lastRef = this.theInputFileReader.ReadInt16();
						aVertex.position.x = this.theInputFileReader.ReadSingle();
						aVertex.position.y = this.theInputFileReader.ReadSingle();
						aVertex.position.z = this.theInputFileReader.ReadSingle();
						aVertex.normal.x = this.theInputFileReader.ReadSingle();
						aVertex.normal.y = this.theInputFileReader.ReadSingle();
						aVertex.normal.z = this.theInputFileReader.ReadSingle();
						aVertex.texCoordX = this.theInputFileReader.ReadSingle();
						aVertex.texCoordY = this.theInputFileReader.ReadSingle();

						aModel.theVertexes.Add(aVertex);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModel.theVertexes.Count.ToString());

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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.tangentOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theTangents = new List<SourceVector4D>(aModel.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModel.vertexCount; vertexIndex++)
					{
						vertexInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceVector4D aTangent = new SourceVector4D();

						aTangent.x = this.theInputFileReader.ReadSingle();
						aTangent.y = this.theInputFileReader.ReadSingle();
						aTangent.z = this.theInputFileReader.ReadSingle();
						aTangent.w = this.theInputFileReader.ReadSingle();

						aModel.theTangents.Add(aTangent);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theTangents " + aModel.theTangents.Count.ToString());

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
					this.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aMesh.theFlexes = new List<SourceMdlFlex37>(aMesh.flexCount);
					for (int k = 0; k < aMesh.flexCount; k++)
					{
						flexInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlFlex37 aFlex = new SourceMdlFlex37();

						aFlex.flexDescIndex = this.theInputFileReader.ReadInt32();

						aFlex.target0 = this.theInputFileReader.ReadSingle();
						aFlex.target1 = this.theInputFileReader.ReadSingle();
						aFlex.target2 = this.theInputFileReader.ReadSingle();
						aFlex.target3 = this.theInputFileReader.ReadSingle();

						aFlex.vertCount = this.theInputFileReader.ReadInt32();
						aFlex.vertOffset = this.theInputFileReader.ReadInt32();

						aMesh.theFlexes.Add(aFlex);

						//'NOTE: Set the frame index here because it is determined by order of flexes in mdl file.
						//'      Start the indexing at 1 because first frame (frame 0) is "basis" frame.
						//Me.theCurrentFrameIndex += 1
						//Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex = Me.theCurrentFrameIndex

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadVertAnims(flexInputFileStreamPosition, aFlex);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aMesh.theFlexes alignment");
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
					this.theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aFlex.theVertAnims = new List<SourceMdlVertAnim37>(aFlex.vertCount);
					for (int k = 0; k < aFlex.vertCount; k++)
					{
						//vertAnimInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlVertAnim37 aVertAnim = new SourceMdlVertAnim37();

						aVertAnim.index = this.theInputFileReader.ReadInt32();
						aVertAnim.delta.x = this.theInputFileReader.ReadSingle();
						aVertAnim.delta.y = this.theInputFileReader.ReadSingle();
						aVertAnim.delta.z = this.theInputFileReader.ReadSingle();
						aVertAnim.nDelta.x = this.theInputFileReader.ReadSingle();
						aVertAnim.nDelta.y = this.theInputFileReader.ReadSingle();
						aVertAnim.nDelta.z = this.theInputFileReader.ReadSingle();

						aFlex.theVertAnims.Add(aVertAnim);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment");
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