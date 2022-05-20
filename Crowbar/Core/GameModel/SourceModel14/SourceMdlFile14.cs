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
	public class SourceMdlFile14
	{

#region Creation and Destruction

		public SourceMdlFile14(BinaryReader mdlFileReader, SourceMdlFileData14 mdlFileData)
		{
			this.theInputFileReader = mdlFileReader;
			this.theMdlFileData = mdlFileData;

			this.theMdlFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile14(BinaryWriter mdlFileWriter, SourceMdlFileData14 mdlFileData)
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

			this.theMdlFileData.id = this.theInputFileReader.ReadChars(4);
			this.theMdlFileData.theID = new string(this.theMdlFileData.id);
			this.theMdlFileData.version = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.name = this.theInputFileReader.ReadChars(64);
			this.theMdlFileData.theModelName = (new string(this.theMdlFileData.name)).Trim('\0');

			this.theMdlFileData.fileSize = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.theActualFileSize = this.theInputFileReader.BaseStream.Length;

			this.theMdlFileData.eyePosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.eyePosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.eyePosition.z = this.theInputFileReader.ReadSingle();

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

			this.theMdlFileData.hitboxCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.hitboxOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.sequenceCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.sequenceOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.sequenceGroupCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.sequenceGroupOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.textureCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.textureOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.textureDataOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.skinReferenceCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinFamilyCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.bodyPartCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.bodyPartOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.attachmentCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.attachmentOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.soundTable = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.soundOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.soundGroups = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.soundGroupOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.transitionCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.transitionOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.unknown01 = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.subModelCount = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.vertexCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.indexCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.indexOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.vertexOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.normalOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.uvOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown08 = this.theInputFileReader.ReadInt32();
			//Me.theMdlFileData.unknown09 = Me.theInputFileReader.ReadInt32()
			this.theMdlFileData.weightingWeightOffset = this.theInputFileReader.ReadInt32();
			//Me.theMdlFileData.unknown10 = Me.theInputFileReader.ReadInt32()
			this.theMdlFileData.weightingBoneOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown11 = this.theInputFileReader.ReadInt32();

			for (int x = 0; x < this.theMdlFileData.subModelOffsets.Length; x++)
			{
				this.theMdlFileData.subModelOffsets[x] = this.theInputFileReader.ReadInt32();
			}

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
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theBones = new List<SourceMdlBone10>(this.theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < this.theMdlFileData.boneCount; boneIndex++)
					{
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlBone10 aBone = new SourceMdlBone10();

						aBone.name = this.theInputFileReader.ReadChars(32);
						aBone.theName = new string(aBone.name);
						aBone.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aBone.theName);
						aBone.parentBoneIndex = this.theInputFileReader.ReadInt32();
						aBone.flags = this.theInputFileReader.ReadInt32();
						for (int boneControllerIndexIndex = 0; boneControllerIndexIndex < aBone.boneControllerIndex.Length; boneControllerIndexIndex++)
						{
							aBone.boneControllerIndex[boneControllerIndexIndex] = this.theInputFileReader.ReadInt32();
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

						this.theMdlFileData.theBones.Add(aBone);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
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
				//Dim boneControllerInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theBoneControllers = new List<SourceMdlBoneController10>(this.theMdlFileData.boneControllerCount);
				for (int boneControllerIndex = 0; boneControllerIndex < this.theMdlFileData.boneControllerCount; boneControllerIndex++)
				{
					//boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlBoneController10 aBoneController = new SourceMdlBoneController10();

					aBoneController.boneIndex = this.theInputFileReader.ReadInt32();
					aBoneController.type = this.theInputFileReader.ReadInt32();
					aBoneController.startAngleDegrees = this.theInputFileReader.ReadSingle();
					aBoneController.endAngleDegrees = this.theInputFileReader.ReadSingle();
					aBoneController.restIndex = this.theInputFileReader.ReadInt32();
					aBoneController.index = this.theInputFileReader.ReadInt32();

					this.theMdlFileData.theBoneControllers.Add(aBoneController);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + this.theMdlFileData.theBoneControllers.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment");
			}
		}

		public void ReadAttachments()
		{
			if (this.theMdlFileData.attachmentCount > 0)
			{
				//Dim attachmentInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.attachmentOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theAttachments = new List<SourceMdlAttachment10>(this.theMdlFileData.attachmentCount);
				for (int attachmentIndex = 0; attachmentIndex < this.theMdlFileData.attachmentCount; attachmentIndex++)
				{
					//attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlAttachment10 anAttachment = new SourceMdlAttachment10();

					anAttachment.name = this.theInputFileReader.ReadChars(32);
					anAttachment.theName = new string(anAttachment.name);
					anAttachment.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(anAttachment.theName);
					anAttachment.type = this.theInputFileReader.ReadInt32();
					anAttachment.boneIndex = this.theInputFileReader.ReadInt32();

					anAttachment.attachmentPoint = new SourceVector();
					anAttachment.attachmentPoint.x = this.theInputFileReader.ReadSingle();
					anAttachment.attachmentPoint.y = this.theInputFileReader.ReadSingle();
					anAttachment.attachmentPoint.z = this.theInputFileReader.ReadSingle();
					for (int x = 0; x <= 2; x++)
					{
						anAttachment.vectors[x] = new SourceVector();
						anAttachment.vectors[x].x = this.theInputFileReader.ReadSingle();
						anAttachment.vectors[x].y = this.theInputFileReader.ReadSingle();
						anAttachment.vectors[x].z = this.theInputFileReader.ReadSingle();
					}

					this.theMdlFileData.theAttachments.Add(anAttachment);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + this.theMdlFileData.theAttachments.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment");
			}
		}

		public void ReadHitboxes()
		{
			if (this.theMdlFileData.hitboxCount > 0)
			{
				//Dim hitboxInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.hitboxOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theHitboxes = new List<SourceMdlHitbox10>(this.theMdlFileData.hitboxCount);
				for (int hitboxIndex = 0; hitboxIndex < this.theMdlFileData.hitboxCount; hitboxIndex++)
				{
					//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlHitbox10 aHitbox = new SourceMdlHitbox10();

					aHitbox.boneIndex = this.theInputFileReader.ReadInt32();
					aHitbox.groupIndex = this.theInputFileReader.ReadInt32();
					aHitbox.boundingBoxMin.x = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMin.y = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMin.z = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.x = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.y = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.z = this.theInputFileReader.ReadSingle();

					this.theMdlFileData.theHitboxes.Add(aHitbox);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes " + this.theMdlFileData.theHitboxes.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment");
			}
		}

		public void ReadSequences()
		{
			if (this.theMdlFileData.sequenceCount > 0)
			{
				//Dim sequenceInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.sequenceOffset, SeekOrigin.Begin);
					this.theMdlFileData.theSequences = new List<SourceMdlSequenceDesc10>(this.theMdlFileData.sequenceCount);
					for (int sequenceIndex = 0; sequenceIndex < this.theMdlFileData.sequenceCount; sequenceIndex++)
					{
						//sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlSequenceDesc10 aSequence = new SourceMdlSequenceDesc10();

						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						aSequence.name = this.theInputFileReader.ReadChars(32);
						aSequence.theName = new string(aSequence.name);
						aSequence.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aSequence.theName);

						aSequence.fps = this.theInputFileReader.ReadSingle();

						aSequence.flags = this.theInputFileReader.ReadInt32();
						aSequence.activityId = this.theInputFileReader.ReadInt32();
						aSequence.activityWeight = this.theInputFileReader.ReadInt32();
						aSequence.eventCount = this.theInputFileReader.ReadInt32();
						aSequence.eventOffset = this.theInputFileReader.ReadInt32();
						aSequence.frameCount = this.theInputFileReader.ReadInt32();
						aSequence.pivotCount = this.theInputFileReader.ReadInt32();
						aSequence.pivotOffset = this.theInputFileReader.ReadInt32();

						aSequence.motiontype = this.theInputFileReader.ReadInt32();
						aSequence.motionbone = this.theInputFileReader.ReadInt32();
						aSequence.linearmovement.x = this.theInputFileReader.ReadSingle();
						aSequence.linearmovement.y = this.theInputFileReader.ReadSingle();
						aSequence.linearmovement.z = this.theInputFileReader.ReadSingle();
						aSequence.automoveposindex = this.theInputFileReader.ReadInt32();
						aSequence.automoveangleindex = this.theInputFileReader.ReadInt32();

						aSequence.bbMin.x = this.theInputFileReader.ReadSingle();
						aSequence.bbMin.y = this.theInputFileReader.ReadSingle();
						aSequence.bbMin.z = this.theInputFileReader.ReadSingle();
						aSequence.bbMax.x = this.theInputFileReader.ReadSingle();
						aSequence.bbMax.y = this.theInputFileReader.ReadSingle();
						aSequence.bbMax.z = this.theInputFileReader.ReadSingle();

						aSequence.blendCount = this.theInputFileReader.ReadInt32();
						aSequence.theSmdRelativePathFileNames = new List<string>(aSequence.blendCount);
						for (int i = 0; i < aSequence.blendCount; i++)
						{
							aSequence.theSmdRelativePathFileNames.Add("");
						}

						aSequence.animOffset = this.theInputFileReader.ReadInt32();

						for (int x = 0; x < aSequence.blendType.Length; x++)
						{
							aSequence.blendType[x] = this.theInputFileReader.ReadInt32();
						}
						for (int x = 0; x < aSequence.blendStart.Length; x++)
						{
							aSequence.blendStart[x] = this.theInputFileReader.ReadSingle();
						}
						for (int x = 0; x < aSequence.blendEnd.Length; x++)
						{
							aSequence.blendEnd[x] = this.theInputFileReader.ReadSingle();
						}
						aSequence.blendParent = this.theInputFileReader.ReadInt32();

						aSequence.groupIndex = this.theInputFileReader.ReadInt32();
						aSequence.entryNodeIndex = this.theInputFileReader.ReadInt32();
						aSequence.exitNodeIndex = this.theInputFileReader.ReadInt32();
						aSequence.nodeFlags = this.theInputFileReader.ReadInt32();
						aSequence.nextSeq = this.theInputFileReader.ReadInt32();

						//TODO: unknown bytes
						this.theInputFileReader.ReadInt32();
						this.theInputFileReader.ReadInt32();
						this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theSequences.Add(aSequence);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]");

						//Me.ReadEvents(aSequence)
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.theEvents alignment")

						//Me.ReadPivots(aSequence)
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSequence.thePivots alignment")
					}
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
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theSequenceGroupFileHeaders = new List<SourceMdlSequenceGroupFileHeader10>(this.theMdlFileData.sequenceGroupCount);
					this.theMdlFileData.theSequenceGroups = new List<SourceMdlSequenceGroup10>(this.theMdlFileData.sequenceGroupCount);
					for (int sequenceGroupIndex = 0; sequenceGroupIndex < this.theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
					{
						SourceMdlSequenceGroupFileHeader10 aSequenceGroupFileHeader = new SourceMdlSequenceGroupFileHeader10();
						this.theMdlFileData.theSequenceGroupFileHeaders.Add(aSequenceGroupFileHeader);

						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlSequenceGroup10 aSequenceGroup = new SourceMdlSequenceGroup10();

						aSequenceGroup.name = this.theInputFileReader.ReadChars(32);
						aSequenceGroup.theName = (new string(aSequenceGroup.name)).Trim('\0');
						aSequenceGroup.fileName = this.theInputFileReader.ReadChars(64);
						aSequenceGroup.theFileName = (new string(aSequenceGroup.fileName)).Trim('\0');
						aSequenceGroup.cacheOffset = this.theInputFileReader.ReadInt32();
						aSequenceGroup.data = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theSequenceGroups.Add(aSequenceGroup);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
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

					this.theMdlFileData.theTransitions = new List<List<byte>>(this.theMdlFileData.transitionCount);
					for (int entryNodeIndex = 0; entryNodeIndex < this.theMdlFileData.transitionCount; entryNodeIndex++)
					{
						List<byte> exitNodeTransitions = new List<byte>(this.theMdlFileData.transitionCount);
						for (int exitNodeIndex = 0; exitNodeIndex < this.theMdlFileData.transitionCount; exitNodeIndex++)
						{
							byte aTransitionValue = this.theInputFileReader.ReadByte();
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

		public void ReadAnimations(int sequenceGroupIndex)
		{
			if (this.theMdlFileData.theSequences != null)
			{
				long animationInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				SourceMdlSequenceDesc10 aSequence = null;
				long animationValuesEndInputFileStreamPosition = 0;

				try
				{
					for (int sequenceIndex = 0; sequenceIndex < this.theMdlFileData.theSequences.Count; sequenceIndex++)
					{
						aSequence = this.theMdlFileData.theSequences[sequenceIndex];
						animationValuesEndInputFileStreamPosition = 0;

						if (aSequence.groupIndex != sequenceGroupIndex)
						{
							continue;
						}

						this.theInputFileReader.BaseStream.Seek(aSequence.animOffset, SeekOrigin.Begin);
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

						aSequence.theAnimations = new List<SourceMdlAnimation10>(aSequence.blendCount * this.theMdlFileData.theBones.Count);
						for (int blendIndex = 0; blendIndex < aSequence.blendCount; blendIndex++)
						{
							for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
							{
								animationInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
								fileOffsetStart = this.theInputFileReader.BaseStream.Position;
								SourceMdlAnimation10 anAnimation = new SourceMdlAnimation10();

								for (int offsetIndex = 0; offsetIndex < anAnimation.animationValueOffsets.Length; offsetIndex++)
								{
									anAnimation.animationValueOffsets[offsetIndex] = this.theInputFileReader.ReadUInt16();

									if (anAnimation.animationValueOffsets[offsetIndex] > 0)
									{
										inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

										this.ReadAnimationValues(animationInputFileStreamPosition + anAnimation.animationValueOffsets[offsetIndex], aSequence.frameCount, anAnimation.theAnimationValues[offsetIndex]);
										animationValuesEndInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

										this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
									}
								}
								aSequence.theAnimations.Add(anAnimation);

								fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
								this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation");
							}
						}

						if (animationValuesEndInputFileStreamPosition > 0)
						{
							inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

							this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, animationValuesEndInputFileStreamPosition - 1, 4, "aSequence.theAnimations - End of AnimationValues alignment");

							this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
						}

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence.theAnimations")

						this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aSequence.theAnimations alignment");
					}
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
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				long modelsEndInputFileStreamPosition = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.bodyPartOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					modelsEndInputFileStreamPosition = 0;

					this.theMdlFileData.theBodyParts = new List<SourceMdlBodyPart14>(this.theMdlFileData.bodyPartCount);
					for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.bodyPartCount; bodyPartIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						bodyPartInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart14 aBodyPart = new SourceMdlBodyPart14();

						aBodyPart.name = this.theInputFileReader.ReadChars(64);
						aBodyPart.theName = (new string(aBodyPart.name)).Trim('\0');
						aBodyPart.modelCount = this.theInputFileReader.ReadInt32();
						aBodyPart.@base = this.theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = this.theInputFileReader.ReadInt32();

						this.theMdlFileData.theBodyParts.Add(aBodyPart);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadModels(aBodyPart);
						//If bodyPartIndex = Me.theMdlFileData.bodyPartCount - 1 Then
						//	modelsEndInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//End If

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart [" + aBodyPart.theName + "]");
					}

					//If modelsEndInputFileStreamPosition > 0 Then
					//	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//	Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, modelsEndInputFileStreamPosition - 1, 4, "theMdlFileData.theBodyParts - End of Models alignment")

					//	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					//End If

					//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
					//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadIndexes()
		{
			if (this.theMdlFileData.indexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.indexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theIndexes = new List<UInt16>(this.theMdlFileData.indexCount);
					for (int i = 0; i < this.theMdlFileData.indexCount; i++)
					{
						UInt16 index = new UInt16();

						index = this.theInputFileReader.ReadUInt16();

						this.theMdlFileData.theIndexes.Add(index);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIndexes " + this.theMdlFileData.theIndexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadVertexes()
		{
			if (this.theMdlFileData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					double unused = 0;
					this.theMdlFileData.theVertexes = new List<SourceVector>(this.theMdlFileData.vertexCount);
					for (int vertexIndex = 0; vertexIndex < this.theMdlFileData.vertexCount; vertexIndex++)
					{
						SourceVector vertex = new SourceVector();

						vertex.x = this.theInputFileReader.ReadSingle();
						vertex.y = this.theInputFileReader.ReadSingle();
						vertex.z = this.theInputFileReader.ReadSingle();
						unused = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theVertexes.Add(vertex);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theVertexes " + this.theMdlFileData.theVertexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadNormals()
		{
			if (this.theMdlFileData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.normalOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					double unused = 0;
					this.theMdlFileData.theNormals = new List<SourceVector>(this.theMdlFileData.vertexCount);
					for (int normalIndex = 0; normalIndex < this.theMdlFileData.vertexCount; normalIndex++)
					{
						SourceVector normal = new SourceVector();

						normal.x = this.theInputFileReader.ReadSingle();
						normal.y = this.theInputFileReader.ReadSingle();
						normal.z = this.theInputFileReader.ReadSingle();
						unused = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theNormals.Add(normal);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theNormals " + this.theMdlFileData.theNormals.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadUVs()
		{
			if (this.theMdlFileData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.uvOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theUVs = new List<SourceVector>(this.theMdlFileData.vertexCount);
					for (int uvIndex = 0; uvIndex < this.theMdlFileData.vertexCount; uvIndex++)
					{
						SourceVector uv = new SourceVector();

						uv.x = this.theInputFileReader.ReadSingle();
						uv.y = this.theInputFileReader.ReadSingle();

						this.theMdlFileData.theUVs.Add(uv);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theUVs " + this.theMdlFileData.theUVs.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadWeightingWeights()
		{
			if (this.theMdlFileData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.weightingWeightOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theWeightings = new List<SourceMdlWeighting14>(this.theMdlFileData.vertexCount);
					for (int weightingIndex = 0; weightingIndex < this.theMdlFileData.vertexCount; weightingIndex++)
					{
						SourceMdlWeighting14 weighting = new SourceMdlWeighting14();

						for (int x = 0; x <= 3; x++)
						{
							weighting.weights[x] = this.theInputFileReader.ReadSingle();
						}

						this.theMdlFileData.theWeightings.Add(weighting);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theWeights.weights " + this.theMdlFileData.theWeightings.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadWeightingBones()
		{
			if (this.theMdlFileData.theWeightings.Count > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.weightingBoneOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					foreach (SourceMdlWeighting14 weighting in this.theMdlFileData.theWeightings)
					{
						weighting.boneCount = 0;
						for (int x = 0; x <= 3; x++)
						{
							weighting.bones[x] = this.theInputFileReader.ReadByte();
							if (weighting.bones[x] != 0xFF)
							{
								weighting.boneCount += 1;
							}
						}
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theWeightings.bones " + this.theMdlFileData.theWeightings.Count.ToString());
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
				//Dim boneInputFileStreamPosition As Long
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.textureOffset, SeekOrigin.Begin);

					this.theMdlFileData.theTextures = new List<SourceMdlTexture14>(this.theMdlFileData.textureCount);
					for (int textureIndex = 0; textureIndex < this.theMdlFileData.textureCount; textureIndex++)
					{
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlTexture14 aTexture = new SourceMdlTexture14();

						aTexture.fileName = this.theInputFileReader.ReadChars(64);
						aTexture.theFileName = (new string(aTexture.fileName)).Trim('\0');
						aTexture.textureName = this.theInputFileReader.ReadChars(64);
						aTexture.theTextureName = (new string(aTexture.textureName)).Trim('\0');
						aTexture.flags = this.theInputFileReader.ReadInt32();
						aTexture.width = this.theInputFileReader.ReadUInt32();
						aTexture.height = this.theInputFileReader.ReadUInt32();
						aTexture.dataOffset = this.theInputFileReader.ReadUInt32();

						//'TODO: Unknown bytes.
						//Me.theInputFileReader.ReadUInt32()
						//Me.theInputFileReader.ReadUInt32()
						//Me.theInputFileReader.ReadUInt32()
						//Me.theInputFileReader.ReadUInt32()

						this.theMdlFileData.theTextures.Add(aTexture);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture [" + aTexture.theFileName + "][" + aTexture.theTextureName + "]");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						//Me.ReadTextureData(aTexture)
						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aTexture.theData alignment")

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment");
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
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				short aSkinRef = 0;

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.skinOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theSkinFamilies = new List<List<short>>(this.theMdlFileData.skinFamilyCount);
					for (int skinFamilyIndex = 0; skinFamilyIndex < this.theMdlFileData.skinFamilyCount; skinFamilyIndex++)
					{
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						List<short> aSkinFamily = new List<short>();

						for (int skinRefIndex = 0; skinRefIndex < this.theMdlFileData.skinReferenceCount; skinRefIndex++)
						{
							aSkinRef = this.theInputFileReader.ReadInt16();
							aSkinFamily.Add(aSkinRef);
						}

						this.theMdlFileData.theSkinFamilies.Add(aSkinFamily);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies " + this.theMdlFileData.theSkinFamilies.Count.ToString());

					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadSequenceGroupMdlHeader()
		{
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			this.theMdlFileData.id = this.theInputFileReader.ReadChars(4);
			this.theMdlFileData.theID = new string(this.theMdlFileData.id);
			this.theMdlFileData.version = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.name = this.theInputFileReader.ReadChars(64);
			this.theMdlFileData.theModelName = (new string(this.theMdlFileData.name)).Trim('\0');

			this.theMdlFileData.fileSize = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.theActualFileSize = this.theInputFileReader.BaseStream.Length;

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "SequenceGroupMDL File Header");
		}

		public void ReadUnreadBytes()
		{
			this.theMdlFileData.theFileSeekLog.LogUnreadBytes(this.theInputFileReader);
		}

		public void BuildBoneTransforms()
		{
			this.theMdlFileData.theBoneTransforms = new List<SourceBoneTransform10>(this.theMdlFileData.theBones.Count);
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
			SourceMdlBone10 aBone = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
			int parentBoneIndex = 0;
			for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
			{
	//			Dim aBone As SourceMdlBone10
				SourceBoneTransform10 boneTransform = new SourceBoneTransform10();
	//			Dim parentBoneIndex As Integer

				aBone = this.theMdlFileData.theBones[boneIndex];

				SourceVector boneMatrixColumn0 = new SourceVector();
				SourceVector boneMatrixColumn1 = new SourceVector();
				SourceVector boneMatrixColumn2 = new SourceVector();
				SourceVector boneMatrixColumn3 = new SourceVector();
				//MathModule.AngleMatrix(aBone.rotation.x, aBone.rotation.y, aBone.rotation.z, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
				//MathModule.AngleMatrix(aBone.rotation.z, aBone.rotation.x, aBone.rotation.y, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
				MathModule.AngleMatrix(aBone.rotation.y, aBone.rotation.z, aBone.rotation.x, ref boneMatrixColumn0, ref boneMatrixColumn1, ref boneMatrixColumn2, ref boneMatrixColumn3);

				boneMatrixColumn3.x = aBone.position.x;
				boneMatrixColumn3.y = aBone.position.y;
				boneMatrixColumn3.z = aBone.position.z;

				parentBoneIndex = this.theMdlFileData.theBones[boneIndex].parentBoneIndex;
				if (parentBoneIndex == -1)
				{
					boneTransform.matrixColumn0.x = boneMatrixColumn0.x;
					boneTransform.matrixColumn0.y = boneMatrixColumn0.y;
					boneTransform.matrixColumn0.z = boneMatrixColumn0.z;
					boneTransform.matrixColumn1.x = boneMatrixColumn1.x;
					boneTransform.matrixColumn1.y = boneMatrixColumn1.y;
					boneTransform.matrixColumn1.z = boneMatrixColumn1.z;
					boneTransform.matrixColumn2.x = boneMatrixColumn2.x;
					boneTransform.matrixColumn2.y = boneMatrixColumn2.y;
					boneTransform.matrixColumn2.z = boneMatrixColumn2.z;
					boneTransform.matrixColumn3.x = boneMatrixColumn3.x;
					boneTransform.matrixColumn3.y = boneMatrixColumn3.y;
					boneTransform.matrixColumn3.z = boneMatrixColumn3.z;
				}
				else
				{
					SourceBoneTransform10 parentBoneTransform = this.theMdlFileData.theBoneTransforms[parentBoneIndex];

					//			R_ConcatTransforms( g_bonetransform[pbones[i].parent], bonematrix, g_bonetransform[i] );
					MathModule.R_ConcatTransforms(parentBoneTransform.matrixColumn0, parentBoneTransform.matrixColumn1, parentBoneTransform.matrixColumn2, parentBoneTransform.matrixColumn3, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3, ref boneTransform.matrixColumn0, ref boneTransform.matrixColumn1, ref boneTransform.matrixColumn2, ref boneTransform.matrixColumn3);
				}

				this.theMdlFileData.theBoneTransforms.Add(boneTransform);
			}
		}

		public void WriteInternalMdlFileName(string internalMdlFileName)
		{
			this.theOutputFileWriter.BaseStream.Seek(0x8, SeekOrigin.Begin);
			//TODO: Should only write up to 64 characters.
			this.theOutputFileWriter.Write(internalMdlFileName.ToCharArray());
			//NOTE: Write the ending null byte.
			this.theOutputFileWriter.Write(Convert.ToByte(0));
		}

#endregion

#region Private Methods

		private void ReadEvents(SourceMdlSequenceDesc10 aSequence)
		{
			if (aSequence.eventCount > 0)
			{
				//Dim sequenceInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					if (this.theInputFileReader.BaseStream.Position != aSequence.eventOffset)
					{
						bool offsetIsNotRight = true;
					}

					this.theInputFileReader.BaseStream.Seek(aSequence.eventOffset, SeekOrigin.Begin);
					aSequence.theEvents = new List<SourceMdlEvent10>(aSequence.eventCount);
					for (int eventIndex = 0; eventIndex < aSequence.eventCount; eventIndex++)
					{
						//sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlEvent10 anEvent = new SourceMdlEvent10();

						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						anEvent.frameIndex = this.theInputFileReader.ReadInt32();
						anEvent.eventIndex = this.theInputFileReader.ReadInt32();
						anEvent.eventType = this.theInputFileReader.ReadInt32();
						anEvent.options = this.theInputFileReader.ReadChars(64);
						anEvent.theOptions = (new string(anEvent.options)).Trim('\0');

						aSequence.theEvents.Add(anEvent);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anEvent");
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadPivots(SourceMdlSequenceDesc10 aSequence)
		{
			if (aSequence.pivotCount > 0)
			{
				//Dim sequenceInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					if (this.theInputFileReader.BaseStream.Position != aSequence.pivotOffset)
					{
						bool offsetIsNotRight = true;
					}

					this.theInputFileReader.BaseStream.Seek(aSequence.pivotOffset, SeekOrigin.Begin);
					aSequence.thePivots = new List<SourceMdlPivot10>(aSequence.pivotCount);
					for (int pivotIndex = 0; pivotIndex < aSequence.pivotCount; pivotIndex++)
					{
						//sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlPivot10 aPivot = new SourceMdlPivot10();

						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						aPivot.point.x = this.theInputFileReader.ReadSingle();
						aPivot.point.y = this.theInputFileReader.ReadSingle();
						aPivot.point.z = this.theInputFileReader.ReadSingle();
						aPivot.pivotStart = this.theInputFileReader.ReadInt32();
						aPivot.pivotEnd = this.theInputFileReader.ReadInt32();

						aSequence.thePivots.Add(aPivot);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aPivot");
					}
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadAnimationValues(long animValuesInputFileStreamPosition, int frameCount, List<SourceMdlAnimationValue10> animValues)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			int frameCountRemainingToBeChecked = 0;
			byte currentTotal = 0;
			byte validCount = 0;

			this.theInputFileReader.BaseStream.Seek(animValuesInputFileStreamPosition, SeekOrigin.Begin);
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

		private void ReadModels(SourceMdlBodyPart14 aBodyPart)
		{
			//Dim modelInputFileStreamPosition As Long
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				this.theInputFileReader.BaseStream.Seek(aBodyPart.modelOffset, SeekOrigin.Begin);

				aBodyPart.theModels = new List<SourceMdlModel14>(aBodyPart.modelCount);
				for (int bodyPartIndex = 0; bodyPartIndex < aBodyPart.modelCount; bodyPartIndex++)
				{
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;
					//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlModel14 aModel = new SourceMdlModel14();

					aModel.name = this.theInputFileReader.ReadChars(32);
					aModel.theName = (new string(aModel.name)).Trim('\0');
					aModel.modelIndex = this.theInputFileReader.ReadInt32();

					for (int x = 0; x < aModel.weightingHeaderOffsets.Length; x++)
					{
						aModel.weightingHeaderOffsets[x] = this.theInputFileReader.ReadInt32();
					}

					aBodyPart.theModels.Add(aModel);

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]");

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					//Me.ReadModelVertexBoneInfos(aModel)
					//Me.ReadModelNormalBoneInfos(aModel)
					//Me.ReadModelVertexes(aModel)
					//Me.ReadModelNormals(aModel)
					//Me.ReadMeshes(aModel)
					this.ReadWeightingHeaders(aModel);

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadWeightingHeaders(SourceMdlModel14 aModel)
		{
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				aModel.theWeightingHeaders = new List<SourceMdlWeightingHeader14>(aModel.weightingHeaderOffsets.Length);
				for (int x = 0; x < aModel.weightingHeaderOffsets.Length; x++)
				{
					if (aModel.weightingHeaderOffsets[x] > 0)
					{
						this.theInputFileReader.BaseStream.Seek(aModel.weightingHeaderOffsets[x], SeekOrigin.Begin);
						fileOffsetStart = this.theInputFileReader.BaseStream.Position;

						SourceMdlWeightingHeader14 weightingHeader = new SourceMdlWeightingHeader14();

						weightingHeader.weightingHeaderIndex = this.theInputFileReader.ReadInt32();
						weightingHeader.weightingBoneDataCount = this.theInputFileReader.ReadInt32();
						weightingHeader.weightingBoneDataOffset = this.theInputFileReader.ReadInt32();

						aModel.theWeightingHeaders.Add(weightingHeader);

						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theWeightingHeader [" + aModel.theName + "]");

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						this.ReadWeightingBoneDatas(weightingHeader);

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadWeightingBoneDatas(SourceMdlWeightingHeader14 weightingHeader)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				this.theInputFileReader.BaseStream.Seek(weightingHeader.weightingBoneDataOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				weightingHeader.theWeightingBoneDatas = new List<SourceMdlWeightingBoneData14>(weightingHeader.weightingBoneDataCount);
				for (int i = 0; i < weightingHeader.weightingBoneDataCount; i++)
				{
					SourceMdlWeightingBoneData14 aBoneData = new SourceMdlWeightingBoneData14();

					for (int x = 0; x < aBoneData.theWeightingBoneIndexes.Length; x++)
					{
						aBoneData.theWeightingBoneIndexes[x] = this.theInputFileReader.ReadByte();
					}

					weightingHeader.theWeightingBoneDatas.Add(aBoneData);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "weightingHeader.theWeightingBoneDatas " + weightingHeader.theWeightingBoneDatas.Count.ToString());
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		//Private Sub ReadModelVertexBoneInfos(ByVal aModel As SourceMdlModel14)
		//	If aModel.vertexCount > 0 Then
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long

		//		Try
		//			Dim vertexBoneInfo As Integer
		//			Me.theInputFileReader.BaseStream.Seek(aModel.vertexBoneInfoOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aModel.theVertexBoneInfos = New List(Of Integer)(aModel.vertexCount)
		//			For vertexBoneInfoIndex As Integer = 0 To aModel.vertexCount - 1
		//				vertexBoneInfo = Me.theInputFileReader.ReadByte()
		//				aModel.theVertexBoneInfos.Add(vertexBoneInfo)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexBoneInfos " + aModel.theVertexBoneInfos.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexBoneInfos alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadModelNormalBoneInfos(ByVal aModel As SourceMdlModel14)
		//	If aModel.normalCount > 0 Then
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long

		//		Try
		//			Dim normalBoneInfo As Integer
		//			Me.theInputFileReader.BaseStream.Seek(aModel.normalBoneInfoOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aModel.theNormalBoneInfos = New List(Of Integer)(aModel.normalCount)
		//			For normalBoneInfoIndex As Integer = 0 To aModel.normalCount - 1
		//				normalBoneInfo = Me.theInputFileReader.ReadByte()
		//				aModel.theNormalBoneInfos.Add(normalBoneInfo)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormalBoneInfos " + aModel.theNormalBoneInfos.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theNormalBoneInfos alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadModelVertexes(ByVal aModel As SourceMdlModel14)
		//	If aModel.vertexCount > 0 Then
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(aModel.vertexOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aModel.theVertexes = New List(Of SourceVector)(aModel.vertexCount)
		//			For vertexIndex As Integer = 0 To aModel.vertexCount - 1
		//				Dim vertex As New SourceVector()
		//				vertex.x = Me.theInputFileReader.ReadSingle()
		//				vertex.y = Me.theInputFileReader.ReadSingle()
		//				vertex.z = Me.theInputFileReader.ReadSingle()
		//				aModel.theVertexes.Add(vertex)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModel.theVertexes.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexes alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadModelNormals(ByVal aModel As SourceMdlModel14)
		//	If aModel.normalCount > 0 Then
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(aModel.normalOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aModel.theNormals = New List(Of SourceVector)(aModel.normalCount)
		//			For normalIndex As Integer = 0 To aModel.normalCount - 1
		//				Dim normal As New SourceVector()
		//				normal.x = Me.theInputFileReader.ReadSingle()
		//				normal.y = Me.theInputFileReader.ReadSingle()
		//				normal.z = Me.theInputFileReader.ReadSingle()
		//				aModel.theNormals.Add(normal)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormals " + aModel.theNormals.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theNormals alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadMeshes(ByVal aModel As SourceMdlModel14)
		//	If aModel.weightingBoneIndexCount > 0 Then
		//		'Dim meshInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(aModel.weightingHeaderOffset, SeekOrigin.Begin)

		//			aModel.theMeshes = New List(Of SourceMdlMesh14)(aModel.weightingBoneIndexCount)
		//			For meshIndex As Integer = 0 To aModel.weightingBoneIndexCount - 1
		//				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
		//				Dim aMesh As New SourceMdlMesh14()

		//				aMesh.faceCount = Me.theInputFileReader.ReadInt32()
		//				aMesh.faceOffset = Me.theInputFileReader.ReadInt32()
		//				aMesh.skinref = Me.theInputFileReader.ReadInt32()
		//				aMesh.normalCount = Me.theInputFileReader.ReadInt32()
		//				aMesh.normalOffset = Me.theInputFileReader.ReadInt32()

		//				aModel.theMeshes.Add(aMesh)

		//				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh")

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				Me.ReadFaces(aMesh)

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadFaces(ByVal aMesh As SourceMdlMesh14)
		//	If aMesh.faceCount > 0 Then
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(aMesh.faceOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aMesh.theStripsAndFans = New List(Of SourceMeshTriangleStripOrFan10)()
		//			'For faceIndex As Integer = 0 To aMesh.faceCount - 1
		//			While True
		//				Dim aStripOrFan As New SourceMeshTriangleStripOrFan10()

		//				Dim listCount As Short
		//				listCount = Me.theInputFileReader.ReadInt16()
		//				If listCount = 0 Then
		//					'NOTE: End of list marker has been reached. 
		//					'Exit For
		//					Exit While
		//				Else
		//					aStripOrFan.theVertexInfos = New List(Of SourceMdlVertexInfo10)()
		//					If listCount < 0 Then
		//						aStripOrFan.theFacesAreStoredAsTriangleStrips = False
		//					Else
		//						aStripOrFan.theFacesAreStoredAsTriangleStrips = True
		//					End If
		//				End If
		//				listCount = CShort(Math.Abs(listCount))

		//				For listIndex As Integer = 0 To listCount - 1
		//					Dim vertexAndNormalIndexInfo As New SourceMdlVertexInfo10()
		//					vertexAndNormalIndexInfo.vertexIndex = Me.theInputFileReader.ReadUInt16()
		//					vertexAndNormalIndexInfo.normalIndex = Me.theInputFileReader.ReadUInt16()
		//					vertexAndNormalIndexInfo.s = Me.theInputFileReader.ReadInt16()
		//					vertexAndNormalIndexInfo.t = Me.theInputFileReader.ReadInt16()

		//					aStripOrFan.theVertexInfos.Add(vertexAndNormalIndexInfo)
		//				Next

		//				aMesh.theStripsAndFans.Add(aStripOrFan)

		//				'If faceIndex = aMesh.faceCount - 1 Then
		//				'	' The list should end with a zero.
		//				'	Dim endOfListMarker As Short
		//				'	endOfListMarker = Me.theInputFileReader.ReadInt16()
		//				'	If endOfListMarker <> 0 Then
		//				'		Dim endOfListMarkerIsNotZero As Integer = 4242
		//				'	End If
		//				'End If
		//			End While
		//			'Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFaces " + aMesh.theStripsAndFans.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aMesh.theFaces alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		private void ReadTextureData(SourceMdlTexture14 aTexture)
		{
			//Dim boneInputFileStreamPosition As Long
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				this.theInputFileReader.BaseStream.Seek(aTexture.dataOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aTexture.theData = new List<byte>((int)(aTexture.width * aTexture.height));
				//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
				//      Void ResizeTexture(s_texture_t * ptexture)
				//          ptexture->size = ptexture->skinwidth * ptexture->skinheight + 256 * 3;
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of aTexture.width * aTexture.height + 256 * 3 for every iteration:
				long tempVar = aTexture.width * aTexture.height + 256 * 3;
				for (long byteIndex = 0; byteIndex < tempVar; byteIndex++)
				{
					//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					byte data = this.theInputFileReader.ReadByte();


					aTexture.theData.Add(data);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture.theData");
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

#endregion

#region Data

		protected BinaryReader theInputFileReader;
		protected BinaryWriter theOutputFileWriter;

		protected SourceMdlFileData14 theMdlFileData;

#endregion

	}

}