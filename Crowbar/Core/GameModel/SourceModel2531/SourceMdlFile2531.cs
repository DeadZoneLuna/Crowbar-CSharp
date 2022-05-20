//INSTANT C# NOTE: Formerly VB project-level imports:
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
			this.theInputFileReader = mdlFileReader;
			this.theMdlFileData = mdlFileData;

			this.theMdlFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile2531(BinaryWriter mdlFileWriter, SourceMdlFileData2531 mdlFileData)
		{
			this.theOutputFileWriter = mdlFileWriter;
			this.theMdlFileData = mdlFileData;
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

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			this.theMdlFileData.id = this.theInputFileReader.ReadChars(this.theMdlFileData.id.Length);
			this.theMdlFileData.theID = new string(this.theMdlFileData.id);
			this.theMdlFileData.version = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.checksum = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.name = this.theInputFileReader.ReadChars(this.theMdlFileData.name.Length);
			this.theMdlFileData.theModelName = (new string(this.theMdlFileData.name)).Trim('\0');

			this.theMdlFileData.fileSize = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.theActualFileSize = this.theInputFileReader.BaseStream.Length;

			this.theMdlFileData.eyePosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.eyePosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.eyePosition.z = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.illuminationPosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.illuminationPosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.illuminationPosition.z = this.theInputFileReader.ReadSingle();

			this.theMdlFileData.unknown01 = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.unknown02 = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.unknown03 = this.theInputFileReader.ReadSingle();

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

			this.theMdlFileData.unknown04 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown05 = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.boneCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneControllerCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneControllerOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.hitBoxSetCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.hitBoxSetOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.localAnimationCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localAnimationOffset = this.theInputFileReader.ReadInt32();
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

			this.theMdlFileData.unknownCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknownOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.includeModelCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.includeModelOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.unknown06 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown07 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown08 = this.theInputFileReader.ReadInt32();

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header");

			//If Me.theMdlFileData.bodyPartCount = 0 AndAlso Me.theMdlFileData.localSequenceCount > 0 Then
			//	Me.theMdlFileData.theMdlFileOnlyHasAnimations = True
			//End If
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
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theBones = new List<SourceMdlBone2531>(this.theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < this.theMdlFileData.boneCount; boneIndex++)
					{
						boneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlBone2531 aBone = new SourceMdlBone2531();

						aBone.nameOffset = this.theInputFileReader.ReadInt32();
						aBone.parentBoneIndex = this.theInputFileReader.ReadInt32();
						for (int boneControllerIndexIndex = 0; boneControllerIndexIndex < aBone.boneControllerIndex.Length; boneControllerIndexIndex++)
						{
							aBone.boneControllerIndex[boneControllerIndexIndex] = this.theInputFileReader.ReadInt32();
						}

						//For x As Integer = 0 To aBone.value.Length - 1
						//	aBone.value(x) = Me.theInputFileReader.ReadSingle()
						//Next
						//For x As Integer = 0 To aBone.scale.Length - 1
						//	aBone.scale(x) = Me.theInputFileReader.ReadSingle()
						//Next
						aBone.position.x = this.theInputFileReader.ReadSingle();
						aBone.position.y = this.theInputFileReader.ReadSingle();
						aBone.position.z = this.theInputFileReader.ReadSingle();
						aBone.rotation.x = this.theInputFileReader.ReadSingle();
						aBone.rotation.y = this.theInputFileReader.ReadSingle();
						aBone.rotation.z = this.theInputFileReader.ReadSingle();
						aBone.rotation.w = this.theInputFileReader.ReadSingle();
						aBone.positionScale.x = this.theInputFileReader.ReadSingle();
						aBone.positionScale.y = this.theInputFileReader.ReadSingle();
						aBone.positionScale.z = this.theInputFileReader.ReadSingle();
						aBone.rotationScale.x = this.theInputFileReader.ReadSingle();
						aBone.rotationScale.y = this.theInputFileReader.ReadSingle();
						aBone.rotationScale.z = this.theInputFileReader.ReadSingle();
						//aBone.unknown01 = Me.theInputFileReader.ReadSingle()
						aBone.rotationScale.w = this.theInputFileReader.ReadSingle();

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

						aBone.flags = this.theInputFileReader.ReadInt32();

						aBone.proceduralRuleType = this.theInputFileReader.ReadInt32();
						aBone.proceduralRuleOffset = this.theInputFileReader.ReadInt32();
						aBone.physicsBoneIndex = this.theInputFileReader.ReadInt32();
						aBone.surfacePropNameOffset = this.theInputFileReader.ReadInt32();
						aBone.contents = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theBones.Add(aBone);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone");

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
							if (aBone.proceduralRuleType == SourceMdlBone2531.STUDIO_PROC_AXISINTERP)
							{
								this.ReadAxisInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone2531.STUDIO_PROC_QUATINTERP)
							{
								this.theMdlFileData.theProceduralBonesCommandIsUsed = true;
								this.ReadQuatInterpBone(boneInputFileStreamPosition, aBone);
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
			if (this.theMdlFileData.boneControllerCount > 0)
			{
				//Dim boneControllerInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theBoneControllers = new List<SourceMdlBoneController2531>(this.theMdlFileData.boneControllerCount);
					for (int i = 0; i < this.theMdlFileData.boneControllerCount; i++)
					{
						//boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlBoneController2531 aBoneController = new SourceMdlBoneController2531();

						aBoneController.boneIndex = this.theInputFileReader.ReadInt32();
						aBoneController.type = this.theInputFileReader.ReadInt32();
						aBoneController.startAngleDegrees = this.theInputFileReader.ReadSingle();
						aBoneController.endAngleDegrees = this.theInputFileReader.ReadSingle();
						aBoneController.restIndex = this.theInputFileReader.ReadInt32();
						aBoneController.inputField = this.theInputFileReader.ReadInt32();
						for (int x = 0; x < aBoneController.unused.Length; x++)
						{
							aBoneController.unused[x] = this.theInputFileReader.ReadInt32();
						}

						this.theMdlFileData.theBoneControllers.Add(aBoneController);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBoneController");

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

					this.theMdlFileData.theAttachments = new List<SourceMdlAttachment2531>(this.theMdlFileData.localAttachmentCount);
					for (int attachmentIndex = 0; attachmentIndex < this.theMdlFileData.localAttachmentCount; attachmentIndex++)
					{
						attachmentInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlAttachment2531 anAttachment = new SourceMdlAttachment2531();

						anAttachment.nameOffset = this.theInputFileReader.ReadInt32();
						anAttachment.type = this.theInputFileReader.ReadInt32();
						anAttachment.boneIndex = this.theInputFileReader.ReadInt32();

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
						anAttachment.cXX = this.theInputFileReader.ReadSingle();
						anAttachment.unused01 = this.theInputFileReader.ReadSingle();
						anAttachment.unused02 = this.theInputFileReader.ReadSingle();
						anAttachment.posX = this.theInputFileReader.ReadSingle();

						anAttachment.cYX = this.theInputFileReader.ReadSingle();
						anAttachment.unused03 = this.theInputFileReader.ReadSingle();
						anAttachment.unused04 = this.theInputFileReader.ReadSingle();
						anAttachment.posY = this.theInputFileReader.ReadSingle();

						anAttachment.cZX = this.theInputFileReader.ReadSingle();
						anAttachment.cZY = this.theInputFileReader.ReadSingle();
						anAttachment.cZZ = this.theInputFileReader.ReadSingle();
						anAttachment.posZ = this.theInputFileReader.ReadSingle();


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
						else if (anAttachment.theName == null)
						{
							anAttachment.theName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
			if (this.theMdlFileData.hitBoxSetCount > 0)
			{
				long hitboxSetInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.hitBoxSetOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theHitboxSets = new List<SourceMdlHitboxSet2531>(this.theMdlFileData.hitBoxSetCount);
					for (int i = 0; i < this.theMdlFileData.hitBoxSetCount; i++)
					{
						hitboxSetInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlHitboxSet2531 aHitboxSet = new SourceMdlHitboxSet2531();

						aHitboxSet.nameOffset = this.theInputFileReader.ReadInt32();
						aHitboxSet.hitboxCount = this.theInputFileReader.ReadInt32();
						aHitboxSet.hitboxOffset = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theHitboxSets.Add(aHitboxSet);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet");

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

						this.ReadHitboxes(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, aHitboxSet);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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

					this.theMdlFileData.theSequences = new List<SourceMdlSequenceDesc2531>(this.theMdlFileData.localSequenceCount);
					for (int i = 0; i < this.theMdlFileData.localSequenceCount; i++)
					{
						seqInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlSequenceDesc2531 aSequence = new SourceMdlSequenceDesc2531();

						aSequence.nameOffset = this.theInputFileReader.ReadInt32();
						aSequence.activityNameOffset = this.theInputFileReader.ReadInt32();
						aSequence.flags = this.theInputFileReader.ReadInt32();
						aSequence.activityId = this.theInputFileReader.ReadInt32();
						aSequence.activityWeight = this.theInputFileReader.ReadInt32();
						aSequence.eventCount = this.theInputFileReader.ReadInt32();
						aSequence.eventOffset = this.theInputFileReader.ReadInt32();

						aSequence.bbMin.x = this.theInputFileReader.ReadSingle();
						aSequence.bbMin.y = this.theInputFileReader.ReadSingle();
						aSequence.bbMin.z = this.theInputFileReader.ReadSingle();
						aSequence.bbMax.x = this.theInputFileReader.ReadSingle();
						aSequence.bbMax.y = this.theInputFileReader.ReadSingle();
						aSequence.bbMax.z = this.theInputFileReader.ReadSingle();

						aSequence.blendCount = this.theInputFileReader.ReadInt32();

						//For x As Integer = 0 To aSequence.anim.Length - 1
						//	aSequence.anim(x) = Me.theInputFileReader.ReadInt16()
						//Next
						for (int rowIndex = 0; rowIndex < SourceModule2531.MAXSTUDIOBLENDS; rowIndex++)
						{
							for (int columnIndex = 0; columnIndex < SourceModule2531.MAXSTUDIOBLENDS; columnIndex++)
							{
								aSequence.anim[rowIndex][columnIndex] = this.theInputFileReader.ReadInt16();
							}
						}

						aSequence.movementIndex = this.theInputFileReader.ReadInt32();
						aSequence.groupSize[0] = this.theInputFileReader.ReadInt32();
						aSequence.groupSize[1] = this.theInputFileReader.ReadInt32();

						aSequence.paramIndex[0] = this.theInputFileReader.ReadInt32();
						aSequence.paramIndex[1] = this.theInputFileReader.ReadInt32();
						aSequence.paramStart[0] = this.theInputFileReader.ReadSingle();
						aSequence.paramStart[1] = this.theInputFileReader.ReadSingle();
						aSequence.paramEnd[0] = this.theInputFileReader.ReadSingle();
						aSequence.paramEnd[1] = this.theInputFileReader.ReadSingle();
						aSequence.paramParent = this.theInputFileReader.ReadInt32();

						aSequence.sequenceGroup = this.theInputFileReader.ReadInt32();

						//aSequence.test = Me.theInputFileReader.ReadInt32()
						aSequence.test = this.theInputFileReader.ReadSingle();

						aSequence.fadeInTime = this.theInputFileReader.ReadSingle();
						aSequence.fadeOutTime = this.theInputFileReader.ReadSingle();

						aSequence.localEntryNodeIndex = this.theInputFileReader.ReadInt32();
						aSequence.localExitNodeIndex = this.theInputFileReader.ReadInt32();
						aSequence.nodeFlags = this.theInputFileReader.ReadInt32();

						aSequence.entryPhase = this.theInputFileReader.ReadSingle();
						aSequence.exitPhase = this.theInputFileReader.ReadSingle();
						aSequence.lastFrame = this.theInputFileReader.ReadSingle();

						aSequence.nextSeq = this.theInputFileReader.ReadInt32();
						aSequence.pose = this.theInputFileReader.ReadInt32();

						aSequence.ikRuleCount = this.theInputFileReader.ReadInt32();
						aSequence.autoLayerCount = this.theInputFileReader.ReadInt32();
						aSequence.autoLayerOffset = this.theInputFileReader.ReadInt32();
						//aSequence.weightOffset = Me.theInputFileReader.ReadInt32()
						//aSequence.poseKeyOffset = Me.theInputFileReader.ReadInt32()
						aSequence.unknown01 = this.theInputFileReader.ReadInt32();

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
							aSequence.test02[x] = this.theInputFileReader.ReadSingle();
						}

						aSequence.test03 = this.theInputFileReader.ReadInt32();

						aSequence.ikLockCount = this.theInputFileReader.ReadInt32();
						aSequence.ikLockOffset = this.theInputFileReader.ReadInt32();
						//aSequence.keyValueOffset = Me.theInputFileReader.ReadInt32()
						aSequence.keyValueSize = this.theInputFileReader.ReadInt32();
						aSequence.keyValueOffset = this.theInputFileReader.ReadInt32();

						//aSequence.unknown01 = Me.theInputFileReader.ReadInt32()
						aSequence.unknown02 = this.theInputFileReader.ReadSingle();
						aSequence.unknown03 = this.theInputFileReader.ReadSingle();
						for (int x = 0; x < aSequence.unknown04.Length; x++)
						{
							aSequence.unknown04[x] = this.theInputFileReader.ReadInt32();
						}
						for (int x = 0; x < aSequence.unknown05.Length; x++)
						{
							aSequence.unknown05[x] = this.theInputFileReader.ReadSingle();
						}

						this.theMdlFileData.theSequences.Add(aSequence);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence")

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aSequence.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSequence.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aSequence.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequence.theName = " + aSequence.theName);
						}
						else
						{
							aSequence.theName = "";
						}

						//NOTE: Moved this line here so can show the name in the log.
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]");

						if (aSequence.activityNameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSequence.activityNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aSequence.theActivityName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSequence.theActivityName = " + aSequence.theActivityName);
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

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
			if (this.theMdlFileData.localAnimationCount > 0)
			{
				long animationDescInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localAnimationOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theAnimationDescs = new List<SourceMdlAnimationDesc2531>(this.theMdlFileData.localAnimationCount);
					for (int i = 0; i < this.theMdlFileData.localAnimationCount; i++)
					{
						animationDescInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlAnimationDesc2531 anAnimationDesc = new SourceMdlAnimationDesc2531();

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

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (anAnimationDesc.nameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							anAnimationDesc.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);
							if (anAnimationDesc.theName[0] == '@')
							{
								anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 1);
							}

							//'NOTE: This naming is found in Garry's Mod garrysmod_dir.vpk "\models\m_anm.mdl":  "a_../combine_soldier_xsi/Hold_AR2_base.smd"
							//If anAnimationDesc.theName.StartsWith("a_../") OrElse anAnimationDesc.theName.StartsWith("a_..\") Then
							//	anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 5)
							//	anAnimationDesc.theName = Path.Combine(FileManager.GetPath(anAnimationDesc.theName), "a_" + Path.GetFileName(anAnimationDesc.theName))
							//End If

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

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
			if (this.theMdlFileData.textureCount > 0)
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
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.textureOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theTextures = new List<SourceMdlTexture2531>(this.theMdlFileData.textureCount);
					for (int i = 0; i < this.theMdlFileData.textureCount; i++)
					{
						textureInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlTexture2531 aTexture = new SourceMdlTexture2531();

						aTexture.fileNameOffset = this.theInputFileReader.ReadInt32();
						aTexture.flags = this.theInputFileReader.ReadInt32();
						aTexture.width = this.theInputFileReader.ReadSingle();
						aTexture.height = this.theInputFileReader.ReadSingle();
						aTexture.unknown = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theTextures.Add(aTexture);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (aTexture.fileNameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.fileNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							aTexture.theFileName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

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

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theFileName = " + aTexture.theFileName);
							}
						}
						else
						{
							aTexture.theFileName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
			if (this.theMdlFileData.texturePathCount > 0)
			{
				long texturePathInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.texturePathOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theTexturePaths = new List<string>(this.theMdlFileData.texturePathCount);
					int texturePathOffset = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
					string aTexturePath = null;
					for (int i = 0; i < this.theMdlFileData.texturePathCount; i++)
					{
						texturePathInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
	//					Dim aTexturePath As String

						texturePathOffset = this.theInputFileReader.ReadInt32();

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexturePath (offset to text)");

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
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath (text) = " + aTexturePath);
							}
						}
						else
						{
							aTexturePath = "";
						}
						this.theMdlFileData.theTexturePaths.Add(aTexturePath);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
			if (this.theMdlFileData.skinFamilyCount > 0 && this.theMdlFileData.skinReferenceCount > 0)
			{
				long skinFamilyInputFileStreamPosition = 0;
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.skinOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theSkinFamilies = new List<List<short>>(this.theMdlFileData.skinFamilyCount);
					for (int i = 0; i < this.theMdlFileData.skinFamilyCount; i++)
					{
						skinFamilyInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						List<short> aSkinFamily = new List<short>();

						for (int j = 0; j < this.theMdlFileData.skinReferenceCount; j++)
						{
							short aSkinRef = this.theInputFileReader.ReadInt16();
							aSkinFamily.Add(aSkinRef);
						}

						this.theMdlFileData.theSkinFamilies.Add(aSkinFamily);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSkin");

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
			if (this.theMdlFileData.includeModelCount > 0)
			{
				long includeModelInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.includeModelOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.theIncludeModels = new List<SourceMdlIncludeModel2531>(this.theMdlFileData.includeModelCount);
					for (int i = 0; i < this.theMdlFileData.includeModelCount; i++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						includeModelInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlIncludeModel2531 anIncludeModel = new SourceMdlIncludeModel2531();

						anIncludeModel.fileNameOffset = this.theInputFileReader.ReadInt32();
						for (int x = 0; x < anIncludeModel.unknown.Length; x++)
						{
							anIncludeModel.unknown[x] = this.theInputFileReader.ReadInt32();
						}

						this.theMdlFileData.theIncludeModels.Add(anIncludeModel);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (anIncludeModel.fileNameOffset != 0)
						{
							this.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + anIncludeModel.fileNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

							anIncludeModel.theFileName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

							fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
							if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIncludeModel.theFileName = " + anIncludeModel.theFileName);
							}
						}
						else
						{
							anIncludeModel.theFileName = "";
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIncludeModel [" + anIncludeModel.theFileName + "]");
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

					this.theMdlFileData.theBodyParts = new List<SourceMdlBodyPart2531>(this.theMdlFileData.bodyPartCount);
					for (int i = 0; i < this.theMdlFileData.bodyPartCount; i++)
					{
						bodyPartInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart2531 aBodyPart = new SourceMdlBodyPart2531();

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

						//'NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts and models.
						//If i = Me.theMdlFileData.bodyPartCount - 1 Then
						//	Me.LogToEndAndAlignToNextStart(Me.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment")
						//End If

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

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.flexDescOffset, SeekOrigin.Begin);
				//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				this.theMdlFileData.theFlexDescs = new List<SourceMdlFlexDesc>(this.theMdlFileData.flexDescCount);
				for (int i = 0; i < this.theMdlFileData.flexDescCount; i++)
				{
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;
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

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexDesc [" + aFlexDesc.theName + "]");
				}

				//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs")
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

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.flexControllerOffset, SeekOrigin.Begin);
				//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				this.theMdlFileData.theFlexControllers = new List<SourceMdlFlexController>(this.theMdlFileData.flexControllerCount);
				for (int i = 0; i < this.theMdlFileData.flexControllerCount; i++)
				{
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;
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

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexController [" + aFlexController.theName + "]");
				}

				if (this.theMdlFileData.theFlexControllers.Count > 0)
				{
					this.theMdlFileData.theModelCommandIsUsed = true;
				}

				//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers")
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

					if (aFlexRule.opCount > 0 && aFlexRule.opOffset != 0)
					{
						this.ReadFlexOps(flexRuleInputFileStreamPosition, aFlexRule);
					}

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				if (this.theMdlFileData.theFlexRules.Count > 0)
				{
					this.theMdlFileData.theModelCommandIsUsed = true;
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + this.theMdlFileData.theFlexDescs.Count.ToString());
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
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					this.theMdlFileData.thePoseParamDescs = new List<SourceMdlPoseParamDesc2531>(this.theMdlFileData.localPoseParamaterCount);
					for (int i = 0; i < this.theMdlFileData.localPoseParamaterCount; i++)
					{
						poseInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlPoseParamDesc2531 aPoseParamDesc = new SourceMdlPoseParamDesc2531();

						aPoseParamDesc.nameOffset = this.theInputFileReader.ReadInt32();
						aPoseParamDesc.flags = this.theInputFileReader.ReadInt32();
						aPoseParamDesc.startingValue = this.theInputFileReader.ReadSingle();
						aPoseParamDesc.endingValue = this.theInputFileReader.ReadSingle();
						aPoseParamDesc.loopingRange = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aPoseParamDesc");

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
			if (this.theMdlFileData.sequenceGroupCount > 0)
			{
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.sequenceGroupOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					//Me.theMdlFileData.theSequenceGroupFileHeaders = New List(Of SourceMdlSequenceGroupFileHeader2531)(Me.theMdlFileData.sequenceGroupCount)
					this.theMdlFileData.theSequenceGroups = new List<SourceMdlSequenceGroup2531>(this.theMdlFileData.sequenceGroupCount);
					for (int sequenceGroupIndex = 0; sequenceGroupIndex < this.theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
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
							aSequenceGroup.unknown[x] = this.theInputFileReader.ReadInt32();
						}

						this.theMdlFileData.theSequenceGroups.Add(aSequenceGroup);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequenceGroup ");
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
			if (this.theMdlFileData.surfacePropOffset > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.surfacePropOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theSurfacePropName = " + this.theMdlFileData.theSurfacePropName);
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
			else
			{
				this.theMdlFileData.theSurfacePropName = "";
			}
		}

		public void ReadUnreadBytes()
		{
			this.theMdlFileData.theFileSeekLog.LogUnreadBytes(this.theInputFileReader);
		}

		public void CreateFlexFrameList()
		{
			FlexFrame2531 aFlexFrame = null;
			SourceMdlBodyPart2531 aBodyPart = null;
			SourceMdlModel2531 aModel = null;
			SourceMdlMesh2531 aMesh = null;
			SourceMdlFlex2531 aFlex = null;
			FlexFrame2531 searchedFlexFrame = null;

			this.theMdlFileData.theFlexFrames = new List<FlexFrame2531>();

			//NOTE: Create the defaultflex.
			aFlexFrame = new FlexFrame2531();
			this.theMdlFileData.theFlexFrames.Add(aFlexFrame);

			if (this.theMdlFileData.theFlexDescs != null && this.theMdlFileData.theFlexDescs.Count > 0)
			{
				//Dim flexDescToMeshIndexes As List(Of List(Of Integer))
				List<List<FlexFrame2531>> flexDescToFlexFrames = null;
				int meshVertexIndexStart = 0;

				//flexDescToMeshIndexes = New List(Of List(Of Integer))(Me.theMdlFileData.theFlexDescs.Count)
				//For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
				//	Dim meshIndexList As New List(Of Integer)()
				//	flexDescToMeshIndexes.Add(meshIndexList)
				//Next

				flexDescToFlexFrames = new List<List<FlexFrame2531>>(this.theMdlFileData.theFlexDescs.Count);
				for (int x = 0; x < this.theMdlFileData.theFlexDescs.Count; x++)
				{
					List<FlexFrame2531> flexFrameList = new List<FlexFrame2531>();
					flexDescToFlexFrames.Add(flexFrameList);
				}

				for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = this.theMdlFileData.theBodyParts[bodyPartIndex];

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
														// Add to an existing FlexFrame2531.
														aFlexFrame = searchedFlexFrame;
														break;
													}
												}
											}
											if (aFlexFrame == null)
											{
												aFlexFrame = new FlexFrame2531();
												this.theMdlFileData.theFlexFrames.Add(aFlexFrame);
												aFlexFrame.bodyAndMeshVertexIndexStarts = new List<int>();
												aFlexFrame.flexes = new List<SourceMdlFlex2531>();

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
												//	aFlexFrame.flexSplit = Me.GetSplit(aFlex, meshVertexIndexStart)
												//	Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
												//Else
												//line += "flex """
												aFlexFrame.flexDescription = aFlexFrame.flexName;
												aFlexFrame.flexHasPartner = false;
												//End If
												this.theMdlFileData.theFlexDescs[aFlex.flexDescIndex].theDescIsUsedByFlex = true;

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
			this.theOutputFileWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
			//TODO: Should only write up to 128 characters.
			this.theOutputFileWriter.Write(internalMdlFileName.ToCharArray());
			//NOTE: Write the ending null byte.
			this.theOutputFileWriter.Write(Convert.ToByte(0));
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
				this.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);

				axisInterpBoneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aBone.theAxisInterpBone = new SourceMdlAxisInterpBone2531();
				aBone.theAxisInterpBone.controlBoneIndex = this.theInputFileReader.ReadInt32();
				aBone.theAxisInterpBone.axis = this.theInputFileReader.ReadInt32();
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

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone");

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
				this.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);

				quatInterpBoneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aBone.theQuatInterpBone = new SourceMdlQuatInterpBone2531();
				aBone.theQuatInterpBone.controlBoneIndex = this.theInputFileReader.ReadInt32();
				aBone.theQuatInterpBone.triggerCount = this.theInputFileReader.ReadInt32();
				aBone.theQuatInterpBone.triggerOffset = this.theInputFileReader.ReadInt32();

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone");

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				if (aBone.theQuatInterpBone.triggerCount > 0 && aBone.theQuatInterpBone.triggerOffset != 0)
				{
					this.ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone);
				}

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
				this.theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin);
				//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aQuatInterpBone.theTriggers = new List<SourceMdlQuatInterpInfo2531>(aQuatInterpBone.triggerCount);
				for (int j = 0; j < aQuatInterpBone.triggerCount; j++)
				{
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;
					SourceMdlQuatInterpInfo2531 aTrigger = new SourceMdlQuatInterpInfo2531();

					aTrigger.inverseToleranceAngle = this.theInputFileReader.ReadSingle();

					aTrigger.trigger.x = this.theInputFileReader.ReadSingle();
					aTrigger.trigger.y = this.theInputFileReader.ReadSingle();
					aTrigger.trigger.z = this.theInputFileReader.ReadSingle();
					aTrigger.trigger.w = this.theInputFileReader.ReadSingle();

					aTrigger.pos.x = this.theInputFileReader.ReadSingle();
					aTrigger.pos.y = this.theInputFileReader.ReadSingle();
					aTrigger.pos.z = this.theInputFileReader.ReadSingle();

					aTrigger.quat.x = this.theInputFileReader.ReadSingle();
					aTrigger.quat.y = this.theInputFileReader.ReadSingle();
					aTrigger.quat.z = this.theInputFileReader.ReadSingle();
					aTrigger.quat.w = this.theInputFileReader.ReadSingle();

					aQuatInterpBone.theTriggers.Add(aTrigger);

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.aQuatInterpBone.aTrigger");
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
					this.theInputFileReader.BaseStream.Seek(hitboxOffsetInputFileStreamPosition, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aHitboxSet.theHitboxes = new List<SourceMdlHitbox2531>(aHitboxSet.hitboxCount);
					for (int j = 0; j < aHitboxSet.hitboxCount; j++)
					{
						//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlHitbox2531 aHitbox = new SourceMdlHitbox2531();

						aHitbox.boneIndex = this.theInputFileReader.ReadInt32();
						aHitbox.groupIndex = this.theInputFileReader.ReadInt32();
						aHitbox.boundingBoxMin.x = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.y = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.z = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.x = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.y = this.theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.z = this.theInputFileReader.ReadSingle();

						aHitboxSet.theHitboxes.Add(aHitbox);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitbox");

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
					this.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aBodyPart.theModels = new List<SourceMdlModel2531>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlModel2531 aModel = new SourceMdlModel2531();

						aModel.name = this.theInputFileReader.ReadChars(aModel.name.Length);
						aModel.theName = (new string(aModel.name)).Trim('\0');
						aModel.type = this.theInputFileReader.ReadInt32();
						aModel.boundingRadius = this.theInputFileReader.ReadSingle();

						aModel.meshCount = this.theInputFileReader.ReadInt32();
						aModel.meshOffset = this.theInputFileReader.ReadInt32();
						aModel.vertexCount = this.theInputFileReader.ReadInt32();
						aModel.vertexOffset = this.theInputFileReader.ReadInt32();
						aModel.tangentOffset = this.theInputFileReader.ReadInt32();

						aModel.vertexListType = this.theInputFileReader.ReadInt32();

						for (int x = 0; x < aModel.unknown01.Length; x++)
						{
							aModel.unknown01[x] = this.theInputFileReader.ReadSingle();
						}

						//aModel.unknownCount = Me.theInputFileReader.ReadInt32()
						//aModel.unknownOffset = Me.theInputFileReader.ReadInt32()

						for (int x = 0; x < aModel.unknown02.Length; x++)
						{
							aModel.unknown02[x] = this.theInputFileReader.ReadSingle();
						}
						//For x As Integer = 0 To aModel.unknown03.Length - 1
						//	aModel.unknown03(x) = Me.theInputFileReader.ReadInt32()
						//Next

						aModel.attachmentCount = this.theInputFileReader.ReadInt32();
						aModel.attachmentOffset = this.theInputFileReader.ReadInt32();
						aModel.eyeballCount = this.theInputFileReader.ReadInt32();
						aModel.eyeballOffset = this.theInputFileReader.ReadInt32();
						for (int x = 0; x < aModel.unknown03.Length; x++)
						{
							aModel.unknown03[x] = this.theInputFileReader.ReadInt32();
						}

						aModel.unknown01Count = this.theInputFileReader.ReadInt32();
						aModel.unknown01Offset = this.theInputFileReader.ReadInt32();
						aModel.unknown02Count = this.theInputFileReader.ReadInt32();
						aModel.unknown02Offset = this.theInputFileReader.ReadInt32();

						aBodyPart.theModels.Add(aModel);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						//NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
						this.ReadEyeballs(modelInputFileStreamPosition, aModel);
						this.ReadMeshes(modelInputFileStreamPosition, aModel);
						//If (Me.theMdlFileData.flags And SourceMdlFileData2531.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
						if (aModel.vertexListType == 0)
						{
							this.ReadVertexesType0(modelInputFileStreamPosition, aModel);
						}
						else if (aModel.vertexListType == 1)
						{
							this.ReadVertexesType1(modelInputFileStreamPosition, aModel);
						}
						else
						{
							this.ReadVertexesType2(modelInputFileStreamPosition, aModel);
						}
						this.ReadTangents(modelInputFileStreamPosition, aModel);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aModel.theEyeballs = new List<SourceMdlEyeball2531>(aModel.eyeballCount);
					for (int k = 0; k < aModel.eyeballCount; k++)
					{
						eyeballInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlEyeball2531 anEyeball = new SourceMdlEyeball2531();

						anEyeball.nameOffset = this.theInputFileReader.ReadInt32();

						anEyeball.boneIndex = this.theInputFileReader.ReadInt32();
						anEyeball.org.x = this.theInputFileReader.ReadSingle();
						anEyeball.org.y = this.theInputFileReader.ReadSingle();
						anEyeball.org.z = this.theInputFileReader.ReadSingle();
						anEyeball.zOffset = this.theInputFileReader.ReadSingle();
						anEyeball.radius = this.theInputFileReader.ReadSingle();
						anEyeball.up.x = this.theInputFileReader.ReadSingle();
						anEyeball.up.y = this.theInputFileReader.ReadSingle();
						anEyeball.up.z = this.theInputFileReader.ReadSingle();
						anEyeball.forward.x = this.theInputFileReader.ReadSingle();
						anEyeball.forward.y = this.theInputFileReader.ReadSingle();
						anEyeball.forward.z = this.theInputFileReader.ReadSingle();

						anEyeball.texture = this.theInputFileReader.ReadInt32();
						anEyeball.iris_material = this.theInputFileReader.ReadInt32();
						anEyeball.iris_scale = this.theInputFileReader.ReadSingle();
						anEyeball.glint_material = this.theInputFileReader.ReadInt32();

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

						anEyeball.minPitch = this.theInputFileReader.ReadSingle();
						anEyeball.maxPitch = this.theInputFileReader.ReadSingle();
						anEyeball.minYaw = this.theInputFileReader.ReadSingle();
						anEyeball.maxYaw = this.theInputFileReader.ReadSingle();

						aModel.theEyeballs.Add(anEyeball);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anEyeball");

						//'NOTE: Set the default value to -1 to distinguish it from value assigned to it by ReadMeshes().
						//anEyeball.theTextureIndex = -1

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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

					aModel.theMeshes = new List<SourceMdlMesh2531>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						meshInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						SourceMdlMesh2531 aMesh = new SourceMdlMesh2531();

						aMesh.materialIndex = this.theInputFileReader.ReadInt32();
						aMesh.modelOffset = this.theInputFileReader.ReadInt32();
						aMesh.vertexCount = this.theInputFileReader.ReadInt32();
						aMesh.vertexIndexStart = this.theInputFileReader.ReadInt32();
						aMesh.flexCount = this.theInputFileReader.ReadInt32();
						aMesh.flexOffset = this.theInputFileReader.ReadInt32();

						SourceMdlMeshVertexData meshVertexData = new SourceMdlMeshVertexData();
						meshVertexData.modelVertexDataP = this.theInputFileReader.ReadInt32();
						for (int x = 0; x < SourceConstants.MAX_NUM_LODS; x++)
						{
							meshVertexData.lodVertexCount[x] = this.theInputFileReader.ReadInt32();
						}
						aMesh.vertexData = meshVertexData;

						aModel.theMeshes.Add(aMesh);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh");

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
				this.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				for (int k = 0; k < aMesh.flexCount; k++)
				{
					flexInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlFlex2531 aFlex = new SourceMdlFlex2531();

					aFlex.flexDescIndex = this.theInputFileReader.ReadInt32();

					aFlex.target0 = this.theInputFileReader.ReadSingle();
					aFlex.target1 = this.theInputFileReader.ReadSingle();
					aFlex.target2 = this.theInputFileReader.ReadSingle();
					aFlex.target3 = this.theInputFileReader.ReadSingle();

					aFlex.vertCount = this.theInputFileReader.ReadInt32();
					aFlex.vertOffset = this.theInputFileReader.ReadInt32();

					aFlex.unknown = this.theInputFileReader.ReadInt32();
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

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					if (aFlex.vertCount > 0 && aFlex.vertOffset != 0)
					{
						this.ReadVertAnims(flexInputFileStreamPosition, aFlex);
					}

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString());

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
				this.theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				SourceMdlVertAnim2531 aVertAnim = null;
				aFlex.theVertAnims = new List<SourceMdlVertAnim2531>(aFlex.vertCount);
				for (int k = 0; k < aFlex.vertCount; k++)
				{
					eyeballInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					aVertAnim = new SourceMdlVertAnim2531();

					aVertAnim.index = this.theInputFileReader.ReadUInt16();

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
					aVertAnim.deltaX = this.theInputFileReader.ReadInt16();
					aVertAnim.deltaY = this.theInputFileReader.ReadInt16();
					aVertAnim.deltaZ = this.theInputFileReader.ReadInt16();
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

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				//aFlex.theVertAnims.Sort(AddressOf Me.SortVertAnims)

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment");
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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theVertexesType0 = new List<SourceMdlType0Vertex2531>(aModel.vertexCount);
					for (int j = 0; j < aModel.vertexCount; j++)
					{
						//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlType0Vertex2531 aVertex = new SourceMdlType0Vertex2531();

						for (int x = 0; x < aVertex.weight.Length; x++)
						{
							aVertex.weight[x] = this.theInputFileReader.ReadByte();
						}
						aVertex.unknown1 = this.theInputFileReader.ReadByte();
						for (int x = 0; x < aVertex.boneIndex.Length; x++)
						{
							aVertex.boneIndex[x] = this.theInputFileReader.ReadInt16();
						}
						aVertex.unknown2 = this.theInputFileReader.ReadInt16();

						aVertex.position.x = this.theInputFileReader.ReadSingle();
						aVertex.position.y = this.theInputFileReader.ReadSingle();
						aVertex.position.z = this.theInputFileReader.ReadSingle();
						aVertex.normal.x = this.theInputFileReader.ReadSingle();
						aVertex.normal.y = this.theInputFileReader.ReadSingle();
						aVertex.normal.z = this.theInputFileReader.ReadSingle();
						aVertex.texCoordU = this.theInputFileReader.ReadSingle();
						aVertex.texCoordV = this.theInputFileReader.ReadSingle();

						aModel.theVertexesType0.Add(aVertex);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType0 " + aModel.theVertexesType0.Count.ToString());

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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theVertexesType1 = new List<SourceMdlType1Vertex2531>(aModel.vertexCount);
					for (int j = 0; j < aModel.vertexCount; j++)
					{
						hitboxInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
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
						aVertex.positionX = this.theInputFileReader.ReadUInt16();
						aVertex.positionY = this.theInputFileReader.ReadUInt16();
						aVertex.positionZ = this.theInputFileReader.ReadUInt16();
						//aVertex.normalX = Me.theInputFileReader.ReadUInt16()
						//aVertex.normalY = Me.theInputFileReader.ReadUInt16()
						//aVertex.normalZ = Me.theInputFileReader.ReadUInt16()
						aVertex.normalX = this.theInputFileReader.ReadByte();
						aVertex.normalY = this.theInputFileReader.ReadByte();
						aVertex.normalZ = this.theInputFileReader.ReadByte();
						//Me.theInputFileReader.ReadByte()
						aVertex.texCoordU = this.theInputFileReader.ReadByte();
						this.theInputFileReader.ReadByte();
						aVertex.texCoordV = this.theInputFileReader.ReadByte();
						//Me.theInputFileReader.ReadByte()
						//aVertex.scaleX = Me.theInputFileReader.ReadByte()
						//aVertex.scaleY = Me.theInputFileReader.ReadByte()
						//aVertex.scaleZ = Me.theInputFileReader.ReadByte()

						aModel.theVertexesType1.Add(aVertex);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						//DEBUG:
						this.theInputFileReader.BaseStream.Seek(hitboxInputFileStreamPosition, SeekOrigin.Begin);
						for (int x = 0; x < aVertex.unknown.Length; x++)
						{
							aVertex.unknown[x] = this.theInputFileReader.ReadByte();
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType1 " + aModel.theVertexesType1.Count.ToString());

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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

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
						aVertex.positionX = this.theInputFileReader.ReadByte();
						aVertex.positionY = this.theInputFileReader.ReadByte();
						aVertex.positionZ = this.theInputFileReader.ReadByte();
						//aVertex.positionX = Me.theInputFileReader.ReadSByte()
						//aVertex.positionY = Me.theInputFileReader.ReadSByte()
						//aVertex.positionZ = Me.theInputFileReader.ReadSByte()

						aVertex.normalX = this.theInputFileReader.ReadByte();
						aVertex.normalY = this.theInputFileReader.ReadByte();
						aVertex.texCoordU = this.theInputFileReader.ReadByte();
						aVertex.normalZ = this.theInputFileReader.ReadByte();
						aVertex.texCoordV = this.theInputFileReader.ReadByte();

						aModel.theVertexesType2.Add(aVertex);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aVertex")

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexesType2 " + aModel.theVertexesType2.Count.ToString());

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
					this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.tangentOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aModel.theTangents = new List<SourceMdlTangent2531>(aModel.vertexCount);
					for (int j = 0; j < aModel.vertexCount; j++)
					{
						//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlTangent2531 aTangent = new SourceMdlTangent2531();

						aTangent.x = this.theInputFileReader.ReadSingle();
						aTangent.y = this.theInputFileReader.ReadSingle();
						aTangent.z = this.theInputFileReader.ReadSingle();
						aTangent.w = this.theInputFileReader.ReadSingle();

						aModel.theTangents.Add(aTangent);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTangent")

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theTangents " + aModel.theTangents.Count.ToString());

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
			if (this.theMdlFileData.boneCount > 0 && anAnimationDesc.animOffset != 0)
			{
				long animationInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					anAnimationDesc.theAnimations = new List<SourceMdlAnimation2531>(this.theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < this.theMdlFileData.boneCount; boneIndex++)
					{
						animationInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
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
						anAnimation.unknown = this.theInputFileReader.ReadSingle();
						anAnimation.theOffsets[0] = this.theInputFileReader.ReadInt32();
						anAnimation.theOffsets[1] = this.theInputFileReader.ReadInt32();
						anAnimation.theOffsets[2] = this.theInputFileReader.ReadInt32();
						anAnimation.theOffsets[3] = this.theInputFileReader.ReadInt32();
						anAnimation.theOffsets[4] = this.theInputFileReader.ReadInt32();
						anAnimation.theOffsets[5] = this.theInputFileReader.ReadInt32();
						anAnimation.theOffsets[6] = this.theInputFileReader.ReadInt32();

						anAnimationDesc.theAnimations.Add(anAnimation);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation")

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						if (anAnimation.theOffsets[0] > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[0], anAnimationDesc.frameCount, anAnimation.thePositionAnimationXValues, "anAnimation.thePositionAnimationXValues");
						}
						if (anAnimation.theOffsets[1] > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[1], anAnimationDesc.frameCount, anAnimation.thePositionAnimationYValues, "anAnimation.thePositionAnimationYValues");
						}
						if (anAnimation.theOffsets[2] > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[2], anAnimationDesc.frameCount, anAnimation.thePositionAnimationZValues, "anAnimation.thePositionAnimationZValues");
						}

						if (anAnimation.theOffsets[3] > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[3], anAnimationDesc.frameCount, anAnimation.theRotationAnimationXValues, "anAnimation.theRotationAnimationXValues");
						}
						if (anAnimation.theOffsets[4] > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[4], anAnimationDesc.frameCount, anAnimation.theRotationAnimationYValues, "anAnimation.theRotationAnimationYValues");
						}
						if (anAnimation.theOffsets[5] > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[5], anAnimationDesc.frameCount, anAnimation.theRotationAnimationZValues, "anAnimation.theRotationAnimationZValues");
						}
						if (anAnimation.theOffsets[6] > 0)
						{
							this.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.theOffsets[6], anAnimationDesc.frameCount, anAnimation.theRotationAnimationWValues, "anAnimation.theRotationAnimationWValues");
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations " + anAnimationDesc.theAnimations.Count.ToString());

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
				this.theInputFileReader.BaseStream.Seek(animationValuesInputFileStreamPosition, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				frameCountRemainingToBeChecked = frameCount;
				while (frameCountRemainingToBeChecked > 0)
				{
					anAnimationValue.value = this.theInputFileReader.ReadInt16();
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
						anAnimationValue.value = this.theInputFileReader.ReadInt16();
						animationValues.Add(anAnimationValue);
					}
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, debugDescription);
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