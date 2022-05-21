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
			theInputFileReader = mdlFileReader;
			theMdlFileData = mdlFileData;

			theMdlFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile14(BinaryWriter mdlFileWriter, SourceMdlFileData14 mdlFileData)
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

			theMdlFileData.id = theInputFileReader.ReadChars(4);
			theMdlFileData.theID = new string(theMdlFileData.id);
			theMdlFileData.version = theInputFileReader.ReadInt32();

			theMdlFileData.name = theInputFileReader.ReadChars(64);
			theMdlFileData.theModelName = (new string(theMdlFileData.name)).Trim('\0');

			theMdlFileData.fileSize = theInputFileReader.ReadInt32();
			theMdlFileData.theActualFileSize = theInputFileReader.BaseStream.Length;

			theMdlFileData.eyePosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.eyePosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.eyePosition.z = theInputFileReader.ReadSingle();

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

			theMdlFileData.hitboxCount = theInputFileReader.ReadInt32();
			theMdlFileData.hitboxOffset = theInputFileReader.ReadInt32();

			theMdlFileData.sequenceCount = theInputFileReader.ReadInt32();
			theMdlFileData.sequenceOffset = theInputFileReader.ReadInt32();

			theMdlFileData.sequenceGroupCount = theInputFileReader.ReadInt32();
			theMdlFileData.sequenceGroupOffset = theInputFileReader.ReadInt32();

			theMdlFileData.textureCount = theInputFileReader.ReadInt32();
			theMdlFileData.textureOffset = theInputFileReader.ReadInt32();
			theMdlFileData.textureDataOffset = theInputFileReader.ReadInt32();

			theMdlFileData.skinReferenceCount = theInputFileReader.ReadInt32();
			theMdlFileData.skinFamilyCount = theInputFileReader.ReadInt32();
			theMdlFileData.skinOffset = theInputFileReader.ReadInt32();

			theMdlFileData.bodyPartCount = theInputFileReader.ReadInt32();
			theMdlFileData.bodyPartOffset = theInputFileReader.ReadInt32();

			theMdlFileData.attachmentCount = theInputFileReader.ReadInt32();
			theMdlFileData.attachmentOffset = theInputFileReader.ReadInt32();

			theMdlFileData.soundTable = theInputFileReader.ReadInt32();
			theMdlFileData.soundOffset = theInputFileReader.ReadInt32();
			theMdlFileData.soundGroups = theInputFileReader.ReadInt32();
			theMdlFileData.soundGroupOffset = theInputFileReader.ReadInt32();

			theMdlFileData.transitionCount = theInputFileReader.ReadInt32();
			theMdlFileData.transitionOffset = theInputFileReader.ReadInt32();

			theMdlFileData.unknown01 = theInputFileReader.ReadInt32();

			theMdlFileData.subModelCount = theInputFileReader.ReadInt32();

			theMdlFileData.vertexCount = theInputFileReader.ReadInt32();
			theMdlFileData.indexCount = theInputFileReader.ReadInt32();
			theMdlFileData.indexOffset = theInputFileReader.ReadInt32();
			theMdlFileData.vertexOffset = theInputFileReader.ReadInt32();
			theMdlFileData.normalOffset = theInputFileReader.ReadInt32();
			theMdlFileData.uvOffset = theInputFileReader.ReadInt32();
			theMdlFileData.unknown08 = theInputFileReader.ReadInt32();
			//Me.theMdlFileData.unknown09 = Me.theInputFileReader.ReadInt32()
			theMdlFileData.weightingWeightOffset = theInputFileReader.ReadInt32();
			//Me.theMdlFileData.unknown10 = Me.theInputFileReader.ReadInt32()
			theMdlFileData.weightingBoneOffset = theInputFileReader.ReadInt32();
			theMdlFileData.unknown11 = theInputFileReader.ReadInt32();

			for (int x = 0; x < theMdlFileData.subModelOffsets.Length; x++)
			{
				theMdlFileData.subModelOffsets[x] = theInputFileReader.ReadInt32();
			}

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
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.boneOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theBones = new List<SourceMdlBone10>(theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < theMdlFileData.boneCount; boneIndex++)
					{
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlBone10 aBone = new SourceMdlBone10();

						aBone.name = theInputFileReader.ReadChars(32);
						aBone.theName = new string(aBone.name);
						aBone.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aBone.theName);
						aBone.parentBoneIndex = theInputFileReader.ReadInt32();
						aBone.flags = theInputFileReader.ReadInt32();
						for (int boneControllerIndexIndex = 0; boneControllerIndexIndex < aBone.boneControllerIndex.Length; boneControllerIndexIndex++)
						{
							aBone.boneControllerIndex[boneControllerIndexIndex] = theInputFileReader.ReadInt32();
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

						theMdlFileData.theBones.Add(aBone);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
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
				//Dim boneControllerInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				theInputFileReader.BaseStream.Seek(theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theBoneControllers = new List<SourceMdlBoneController10>(theMdlFileData.boneControllerCount);
				for (int boneControllerIndex = 0; boneControllerIndex < theMdlFileData.boneControllerCount; boneControllerIndex++)
				{
					//boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlBoneController10 aBoneController = new SourceMdlBoneController10();

					aBoneController.boneIndex = theInputFileReader.ReadInt32();
					aBoneController.type = theInputFileReader.ReadInt32();
					aBoneController.startAngleDegrees = theInputFileReader.ReadSingle();
					aBoneController.endAngleDegrees = theInputFileReader.ReadSingle();
					aBoneController.restIndex = theInputFileReader.ReadInt32();
					aBoneController.index = theInputFileReader.ReadInt32();

					theMdlFileData.theBoneControllers.Add(aBoneController);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + theMdlFileData.theBoneControllers.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment");
			}
		}

		public void ReadAttachments()
		{
			if (theMdlFileData.attachmentCount > 0)
			{
				//Dim attachmentInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				theInputFileReader.BaseStream.Seek(theMdlFileData.attachmentOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theAttachments = new List<SourceMdlAttachment10>(theMdlFileData.attachmentCount);
				for (int attachmentIndex = 0; attachmentIndex < theMdlFileData.attachmentCount; attachmentIndex++)
				{
					//attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlAttachment10 anAttachment = new SourceMdlAttachment10();

					anAttachment.name = theInputFileReader.ReadChars(32);
					anAttachment.theName = new string(anAttachment.name);
					anAttachment.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(anAttachment.theName);
					anAttachment.type = theInputFileReader.ReadInt32();
					anAttachment.boneIndex = theInputFileReader.ReadInt32();

					anAttachment.attachmentPoint = new SourceVector();
					anAttachment.attachmentPoint.x = theInputFileReader.ReadSingle();
					anAttachment.attachmentPoint.y = theInputFileReader.ReadSingle();
					anAttachment.attachmentPoint.z = theInputFileReader.ReadSingle();
					for (int x = 0; x <= 2; x++)
					{
						anAttachment.vectors[x] = new SourceVector();
						anAttachment.vectors[x].x = theInputFileReader.ReadSingle();
						anAttachment.vectors[x].y = theInputFileReader.ReadSingle();
						anAttachment.vectors[x].z = theInputFileReader.ReadSingle();
					}

					theMdlFileData.theAttachments.Add(anAttachment);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + theMdlFileData.theAttachments.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment");
			}
		}

		public void ReadHitboxes()
		{
			if (theMdlFileData.hitboxCount > 0)
			{
				//Dim hitboxInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				theInputFileReader.BaseStream.Seek(theMdlFileData.hitboxOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theHitboxes = new List<SourceMdlHitbox10>(theMdlFileData.hitboxCount);
				for (int hitboxIndex = 0; hitboxIndex < theMdlFileData.hitboxCount; hitboxIndex++)
				{
					//hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlHitbox10 aHitbox = new SourceMdlHitbox10();

					aHitbox.boneIndex = theInputFileReader.ReadInt32();
					aHitbox.groupIndex = theInputFileReader.ReadInt32();
					aHitbox.boundingBoxMin.x = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMin.y = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMin.z = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.x = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.y = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.z = theInputFileReader.ReadSingle();

					theMdlFileData.theHitboxes.Add(aHitbox);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes " + theMdlFileData.theHitboxes.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment");
			}
		}

		public void ReadSequences()
		{
			if (theMdlFileData.sequenceCount > 0)
			{
				//Dim sequenceInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.sequenceOffset, SeekOrigin.Begin);
					theMdlFileData.theSequences = new List<SourceMdlSequenceDesc10>(theMdlFileData.sequenceCount);
					for (int sequenceIndex = 0; sequenceIndex < theMdlFileData.sequenceCount; sequenceIndex++)
					{
						//sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlSequenceDesc10 aSequence = new SourceMdlSequenceDesc10();

						fileOffsetStart = theInputFileReader.BaseStream.Position;

						aSequence.name = theInputFileReader.ReadChars(32);
						aSequence.theName = new string(aSequence.name);
						aSequence.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(aSequence.theName);

						aSequence.fps = theInputFileReader.ReadSingle();

						aSequence.flags = theInputFileReader.ReadInt32();
						aSequence.activityId = theInputFileReader.ReadInt32();
						aSequence.activityWeight = theInputFileReader.ReadInt32();
						aSequence.eventCount = theInputFileReader.ReadInt32();
						aSequence.eventOffset = theInputFileReader.ReadInt32();
						aSequence.frameCount = theInputFileReader.ReadInt32();
						aSequence.pivotCount = theInputFileReader.ReadInt32();
						aSequence.pivotOffset = theInputFileReader.ReadInt32();

						aSequence.motiontype = theInputFileReader.ReadInt32();
						aSequence.motionbone = theInputFileReader.ReadInt32();
						aSequence.linearmovement.x = theInputFileReader.ReadSingle();
						aSequence.linearmovement.y = theInputFileReader.ReadSingle();
						aSequence.linearmovement.z = theInputFileReader.ReadSingle();
						aSequence.automoveposindex = theInputFileReader.ReadInt32();
						aSequence.automoveangleindex = theInputFileReader.ReadInt32();

						aSequence.bbMin.x = theInputFileReader.ReadSingle();
						aSequence.bbMin.y = theInputFileReader.ReadSingle();
						aSequence.bbMin.z = theInputFileReader.ReadSingle();
						aSequence.bbMax.x = theInputFileReader.ReadSingle();
						aSequence.bbMax.y = theInputFileReader.ReadSingle();
						aSequence.bbMax.z = theInputFileReader.ReadSingle();

						aSequence.blendCount = theInputFileReader.ReadInt32();
						aSequence.theSmdRelativePathFileNames = new List<string>(aSequence.blendCount);
						for (int i = 0; i < aSequence.blendCount; i++)
						{
							aSequence.theSmdRelativePathFileNames.Add("");
						}

						aSequence.animOffset = theInputFileReader.ReadInt32();

						for (int x = 0; x < aSequence.blendType.Length; x++)
						{
							aSequence.blendType[x] = theInputFileReader.ReadInt32();
						}
						for (int x = 0; x < aSequence.blendStart.Length; x++)
						{
							aSequence.blendStart[x] = theInputFileReader.ReadSingle();
						}
						for (int x = 0; x < aSequence.blendEnd.Length; x++)
						{
							aSequence.blendEnd[x] = theInputFileReader.ReadSingle();
						}
						aSequence.blendParent = theInputFileReader.ReadInt32();

						aSequence.groupIndex = theInputFileReader.ReadInt32();
						aSequence.entryNodeIndex = theInputFileReader.ReadInt32();
						aSequence.exitNodeIndex = theInputFileReader.ReadInt32();
						aSequence.nodeFlags = theInputFileReader.ReadInt32();
						aSequence.nextSeq = theInputFileReader.ReadInt32();

						//TODO: unknown bytes
						theInputFileReader.ReadInt32();
						theInputFileReader.ReadInt32();
						theInputFileReader.ReadInt32();

						theMdlFileData.theSequences.Add(aSequence);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]");

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
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theSequenceGroupFileHeaders = new List<SourceMdlSequenceGroupFileHeader10>(theMdlFileData.sequenceGroupCount);
					theMdlFileData.theSequenceGroups = new List<SourceMdlSequenceGroup10>(theMdlFileData.sequenceGroupCount);
					for (int sequenceGroupIndex = 0; sequenceGroupIndex < theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
					{
						SourceMdlSequenceGroupFileHeader10 aSequenceGroupFileHeader = new SourceMdlSequenceGroupFileHeader10();
						theMdlFileData.theSequenceGroupFileHeaders.Add(aSequenceGroupFileHeader);

						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlSequenceGroup10 aSequenceGroup = new SourceMdlSequenceGroup10();

						aSequenceGroup.name = theInputFileReader.ReadChars(32);
						aSequenceGroup.theName = (new string(aSequenceGroup.name)).Trim('\0');
						aSequenceGroup.fileName = theInputFileReader.ReadChars(64);
						aSequenceGroup.theFileName = (new string(aSequenceGroup.fileName)).Trim('\0');
						aSequenceGroup.cacheOffset = theInputFileReader.ReadInt32();
						aSequenceGroup.data = theInputFileReader.ReadInt32();

						theMdlFileData.theSequenceGroups.Add(aSequenceGroup);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
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

					theMdlFileData.theTransitions = new List<List<byte>>(theMdlFileData.transitionCount);
					for (int entryNodeIndex = 0; entryNodeIndex < theMdlFileData.transitionCount; entryNodeIndex++)
					{
						List<byte> exitNodeTransitions = new List<byte>(theMdlFileData.transitionCount);
						for (int exitNodeIndex = 0; exitNodeIndex < theMdlFileData.transitionCount; exitNodeIndex++)
						{
							byte aTransitionValue = theInputFileReader.ReadByte();
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

		public void ReadAnimations(int sequenceGroupIndex)
		{
			if (theMdlFileData.theSequences != null)
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
					for (int sequenceIndex = 0; sequenceIndex < theMdlFileData.theSequences.Count; sequenceIndex++)
					{
						aSequence = theMdlFileData.theSequences[sequenceIndex];
						animationValuesEndInputFileStreamPosition = 0;

						if (aSequence.groupIndex != sequenceGroupIndex)
						{
							continue;
						}

						theInputFileReader.BaseStream.Seek(aSequence.animOffset, SeekOrigin.Begin);
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

						aSequence.theAnimations = new List<SourceMdlAnimation10>(aSequence.blendCount * theMdlFileData.theBones.Count);
						for (int blendIndex = 0; blendIndex < aSequence.blendCount; blendIndex++)
						{
							for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
							{
								animationInputFileStreamPosition = theInputFileReader.BaseStream.Position;
								fileOffsetStart = theInputFileReader.BaseStream.Position;
								SourceMdlAnimation10 anAnimation = new SourceMdlAnimation10();

								for (int offsetIndex = 0; offsetIndex < anAnimation.animationValueOffsets.Length; offsetIndex++)
								{
									anAnimation.animationValueOffsets[offsetIndex] = theInputFileReader.ReadUInt16();

									if (anAnimation.animationValueOffsets[offsetIndex] > 0)
									{
										inputFileStreamPosition = theInputFileReader.BaseStream.Position;

										ReadAnimationValues(animationInputFileStreamPosition + anAnimation.animationValueOffsets[offsetIndex], aSequence.frameCount, anAnimation.theAnimationValues[offsetIndex]);
										animationValuesEndInputFileStreamPosition = theInputFileReader.BaseStream.Position;

										theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
									}
								}
								aSequence.theAnimations.Add(anAnimation);

								fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation");
							}
						}

						if (animationValuesEndInputFileStreamPosition > 0)
						{
							inputFileStreamPosition = theInputFileReader.BaseStream.Position;

							theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, animationValuesEndInputFileStreamPosition - 1, 4, "aSequence.theAnimations - End of AnimationValues alignment");

							theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
						}

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence.theAnimations")

						theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aSequence.theAnimations alignment");
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
			if (theMdlFileData.bodyPartCount > 0)
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
					theInputFileReader.BaseStream.Seek(theMdlFileData.bodyPartOffset, SeekOrigin.Begin);
					//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
					modelsEndInputFileStreamPosition = 0;

					theMdlFileData.theBodyParts = new List<SourceMdlBodyPart14>(theMdlFileData.bodyPartCount);
					for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.bodyPartCount; bodyPartIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart14 aBodyPart = new SourceMdlBodyPart14();

						aBodyPart.name = theInputFileReader.ReadChars(64);
						aBodyPart.theName = (new string(aBodyPart.name)).Trim('\0');
						aBodyPart.modelCount = theInputFileReader.ReadInt32();
						aBodyPart.@base = theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = theInputFileReader.ReadInt32();

						theMdlFileData.theBodyParts.Add(aBodyPart);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadModels(aBodyPart);
						//If bodyPartIndex = Me.theMdlFileData.bodyPartCount - 1 Then
						//	modelsEndInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						//End If

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart [" + aBodyPart.theName + "]");
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
			if (theMdlFileData.indexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.indexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theIndexes = new List<UInt16>(theMdlFileData.indexCount);
					for (int i = 0; i < theMdlFileData.indexCount; i++)
					{
						UInt16 index = new UInt16();

						index = theInputFileReader.ReadUInt16();

						theMdlFileData.theIndexes.Add(index);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIndexes " + theMdlFileData.theIndexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadVertexes()
		{
			if (theMdlFileData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					double unused = 0;
					theMdlFileData.theVertexes = new List<SourceVector>(theMdlFileData.vertexCount);
					for (int vertexIndex = 0; vertexIndex < theMdlFileData.vertexCount; vertexIndex++)
					{
						SourceVector vertex = new SourceVector();

						vertex.x = theInputFileReader.ReadSingle();
						vertex.y = theInputFileReader.ReadSingle();
						vertex.z = theInputFileReader.ReadSingle();
						unused = theInputFileReader.ReadSingle();

						theMdlFileData.theVertexes.Add(vertex);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theVertexes " + theMdlFileData.theVertexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadNormals()
		{
			if (theMdlFileData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.normalOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					double unused = 0;
					theMdlFileData.theNormals = new List<SourceVector>(theMdlFileData.vertexCount);
					for (int normalIndex = 0; normalIndex < theMdlFileData.vertexCount; normalIndex++)
					{
						SourceVector normal = new SourceVector();

						normal.x = theInputFileReader.ReadSingle();
						normal.y = theInputFileReader.ReadSingle();
						normal.z = theInputFileReader.ReadSingle();
						unused = theInputFileReader.ReadSingle();

						theMdlFileData.theNormals.Add(normal);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theNormals " + theMdlFileData.theNormals.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadUVs()
		{
			if (theMdlFileData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.uvOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theUVs = new List<SourceVector>(theMdlFileData.vertexCount);
					for (int uvIndex = 0; uvIndex < theMdlFileData.vertexCount; uvIndex++)
					{
						SourceVector uv = new SourceVector();

						uv.x = theInputFileReader.ReadSingle();
						uv.y = theInputFileReader.ReadSingle();

						theMdlFileData.theUVs.Add(uv);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theUVs " + theMdlFileData.theUVs.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadWeightingWeights()
		{
			if (theMdlFileData.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.weightingWeightOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theWeightings = new List<SourceMdlWeighting14>(theMdlFileData.vertexCount);
					for (int weightingIndex = 0; weightingIndex < theMdlFileData.vertexCount; weightingIndex++)
					{
						SourceMdlWeighting14 weighting = new SourceMdlWeighting14();

						for (int x = 0; x <= 3; x++)
						{
							weighting.weights[x] = theInputFileReader.ReadSingle();
						}

						theMdlFileData.theWeightings.Add(weighting);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theWeights.weights " + theMdlFileData.theWeightings.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		public void ReadWeightingBones()
		{
			if (theMdlFileData.theWeightings.Count > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.weightingBoneOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					foreach (SourceMdlWeighting14 weighting in theMdlFileData.theWeightings)
					{
						weighting.boneCount = 0;
						for (int x = 0; x <= 3; x++)
						{
							weighting.bones[x] = theInputFileReader.ReadByte();
							if (weighting.bones[x] != 0xFF)
							{
								weighting.boneCount += 1;
							}
						}
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theWeightings.bones " + theMdlFileData.theWeightings.Count.ToString());
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
				//Dim boneInputFileStreamPosition As Long
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.textureOffset, SeekOrigin.Begin);

					theMdlFileData.theTextures = new List<SourceMdlTexture14>(theMdlFileData.textureCount);
					for (int textureIndex = 0; textureIndex < theMdlFileData.textureCount; textureIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlTexture14 aTexture = new SourceMdlTexture14();

						aTexture.fileName = theInputFileReader.ReadChars(64);
						aTexture.theFileName = (new string(aTexture.fileName)).Trim('\0');
						aTexture.textureName = theInputFileReader.ReadChars(64);
						aTexture.theTextureName = (new string(aTexture.textureName)).Trim('\0');
						aTexture.flags = theInputFileReader.ReadInt32();
						aTexture.width = theInputFileReader.ReadUInt32();
						aTexture.height = theInputFileReader.ReadUInt32();
						aTexture.dataOffset = theInputFileReader.ReadUInt32();

						//'TODO: Unknown bytes.
						//Me.theInputFileReader.ReadUInt32()
						//Me.theInputFileReader.ReadUInt32()
						//Me.theInputFileReader.ReadUInt32()
						//Me.theInputFileReader.ReadUInt32()

						theMdlFileData.theTextures.Add(aTexture);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture [" + aTexture.theFileName + "][" + aTexture.theTextureName + "]");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//Me.ReadTextureData(aTexture)
						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aTexture.theData alignment")

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment");
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
				//Dim boneInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				short aSkinRef = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.skinOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theSkinFamilies = new List<List<short>>(theMdlFileData.skinFamilyCount);
					for (int skinFamilyIndex = 0; skinFamilyIndex < theMdlFileData.skinFamilyCount; skinFamilyIndex++)
					{
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						List<short> aSkinFamily = new List<short>();

						for (int skinRefIndex = 0; skinRefIndex < theMdlFileData.skinReferenceCount; skinRefIndex++)
						{
							aSkinRef = theInputFileReader.ReadInt16();
							aSkinFamily.Add(aSkinRef);
						}

						theMdlFileData.theSkinFamilies.Add(aSkinFamily);

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies " + theMdlFileData.theSkinFamilies.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment");
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

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theMdlFileData.id = theInputFileReader.ReadChars(4);
			theMdlFileData.theID = new string(theMdlFileData.id);
			theMdlFileData.version = theInputFileReader.ReadInt32();

			theMdlFileData.name = theInputFileReader.ReadChars(64);
			theMdlFileData.theModelName = (new string(theMdlFileData.name)).Trim('\0');

			theMdlFileData.fileSize = theInputFileReader.ReadInt32();
			theMdlFileData.theActualFileSize = theInputFileReader.BaseStream.Length;

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "SequenceGroupMDL File Header");
		}

		public void ReadUnreadBytes()
		{
			theMdlFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

		public void BuildBoneTransforms()
		{
			theMdlFileData.theBoneTransforms = new List<SourceBoneTransform10>(theMdlFileData.theBones.Count);
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
			SourceMdlBone10 aBone = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
			int parentBoneIndex = 0;
			for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
			{
	//			Dim aBone As SourceMdlBone10
				SourceBoneTransform10 boneTransform = new SourceBoneTransform10();
	//			Dim parentBoneIndex As Integer

				aBone = theMdlFileData.theBones[boneIndex];

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

				parentBoneIndex = theMdlFileData.theBones[boneIndex].parentBoneIndex;
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
					SourceBoneTransform10 parentBoneTransform = theMdlFileData.theBoneTransforms[parentBoneIndex];

					//			R_ConcatTransforms( g_bonetransform[pbones[i].parent], bonematrix, g_bonetransform[i] );
					MathModule.R_ConcatTransforms(parentBoneTransform.matrixColumn0, parentBoneTransform.matrixColumn1, parentBoneTransform.matrixColumn2, parentBoneTransform.matrixColumn3, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3, ref boneTransform.matrixColumn0, ref boneTransform.matrixColumn1, ref boneTransform.matrixColumn2, ref boneTransform.matrixColumn3);
				}

				theMdlFileData.theBoneTransforms.Add(boneTransform);
			}
		}

		public void WriteInternalMdlFileName(string internalMdlFileName)
		{
			theOutputFileWriter.BaseStream.Seek(0x8, SeekOrigin.Begin);
			//TODO: Should only write up to 64 characters.
			theOutputFileWriter.Write(internalMdlFileName.ToCharArray());
			//NOTE: Write the ending null byte.
			theOutputFileWriter.Write(Convert.ToByte(0));
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
					if (theInputFileReader.BaseStream.Position != aSequence.eventOffset)
					{
						bool offsetIsNotRight = true;
					}

					theInputFileReader.BaseStream.Seek(aSequence.eventOffset, SeekOrigin.Begin);
					aSequence.theEvents = new List<SourceMdlEvent10>(aSequence.eventCount);
					for (int eventIndex = 0; eventIndex < aSequence.eventCount; eventIndex++)
					{
						//sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlEvent10 anEvent = new SourceMdlEvent10();

						fileOffsetStart = theInputFileReader.BaseStream.Position;

						anEvent.frameIndex = theInputFileReader.ReadInt32();
						anEvent.eventIndex = theInputFileReader.ReadInt32();
						anEvent.eventType = theInputFileReader.ReadInt32();
						anEvent.options = theInputFileReader.ReadChars(64);
						anEvent.theOptions = (new string(anEvent.options)).Trim('\0');

						aSequence.theEvents.Add(anEvent);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anEvent");
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
					if (theInputFileReader.BaseStream.Position != aSequence.pivotOffset)
					{
						bool offsetIsNotRight = true;
					}

					theInputFileReader.BaseStream.Seek(aSequence.pivotOffset, SeekOrigin.Begin);
					aSequence.thePivots = new List<SourceMdlPivot10>(aSequence.pivotCount);
					for (int pivotIndex = 0; pivotIndex < aSequence.pivotCount; pivotIndex++)
					{
						//sequenceInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlPivot10 aPivot = new SourceMdlPivot10();

						fileOffsetStart = theInputFileReader.BaseStream.Position;

						aPivot.point.x = theInputFileReader.ReadSingle();
						aPivot.point.y = theInputFileReader.ReadSingle();
						aPivot.point.z = theInputFileReader.ReadSingle();
						aPivot.pivotStart = theInputFileReader.ReadInt32();
						aPivot.pivotEnd = theInputFileReader.ReadInt32();

						aSequence.thePivots.Add(aPivot);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aPivot");
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

			theInputFileReader.BaseStream.Seek(animValuesInputFileStreamPosition, SeekOrigin.Begin);
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
				theInputFileReader.BaseStream.Seek(aBodyPart.modelOffset, SeekOrigin.Begin);

				aBodyPart.theModels = new List<SourceMdlModel14>(aBodyPart.modelCount);
				for (int bodyPartIndex = 0; bodyPartIndex < aBodyPart.modelCount; bodyPartIndex++)
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;
					//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlModel14 aModel = new SourceMdlModel14();

					aModel.name = theInputFileReader.ReadChars(32);
					aModel.theName = (new string(aModel.name)).Trim('\0');
					aModel.modelIndex = theInputFileReader.ReadInt32();

					for (int x = 0; x < aModel.weightingHeaderOffsets.Length; x++)
					{
						aModel.weightingHeaderOffsets[x] = theInputFileReader.ReadInt32();
					}

					aBodyPart.theModels.Add(aModel);

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]");

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					//Me.ReadModelVertexBoneInfos(aModel)
					//Me.ReadModelNormalBoneInfos(aModel)
					//Me.ReadModelVertexes(aModel)
					//Me.ReadModelNormals(aModel)
					//Me.ReadMeshes(aModel)
					ReadWeightingHeaders(aModel);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
						theInputFileReader.BaseStream.Seek(aModel.weightingHeaderOffsets[x], SeekOrigin.Begin);
						fileOffsetStart = theInputFileReader.BaseStream.Position;

						SourceMdlWeightingHeader14 weightingHeader = new SourceMdlWeightingHeader14();

						weightingHeader.weightingHeaderIndex = theInputFileReader.ReadInt32();
						weightingHeader.weightingBoneDataCount = theInputFileReader.ReadInt32();
						weightingHeader.weightingBoneDataOffset = theInputFileReader.ReadInt32();

						aModel.theWeightingHeaders.Add(weightingHeader);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theWeightingHeader [" + aModel.theName + "]");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadWeightingBoneDatas(weightingHeader);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
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
				theInputFileReader.BaseStream.Seek(weightingHeader.weightingBoneDataOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				weightingHeader.theWeightingBoneDatas = new List<SourceMdlWeightingBoneData14>(weightingHeader.weightingBoneDataCount);
				for (int i = 0; i < weightingHeader.weightingBoneDataCount; i++)
				{
					SourceMdlWeightingBoneData14 aBoneData = new SourceMdlWeightingBoneData14();

					for (int x = 0; x < aBoneData.theWeightingBoneIndexes.Length; x++)
					{
						aBoneData.theWeightingBoneIndexes[x] = theInputFileReader.ReadByte();
					}

					weightingHeader.theWeightingBoneDatas.Add(aBoneData);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "weightingHeader.theWeightingBoneDatas " + weightingHeader.theWeightingBoneDatas.Count.ToString());
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
				theInputFileReader.BaseStream.Seek(aTexture.dataOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aTexture.theData = new List<byte>((int)(aTexture.width * aTexture.height));
				//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
				//      Void ResizeTexture(s_texture_t * ptexture)
				//          ptexture->size = ptexture->skinwidth * ptexture->skinheight + 256 * 3;
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of aTexture.width * aTexture.height + 256 * 3 for every iteration:
				long tempVar = aTexture.width * aTexture.height + 256 * 3;
				for (long byteIndex = 0; byteIndex < tempVar; byteIndex++)
				{
					//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					byte data = theInputFileReader.ReadByte();


					aTexture.theData.Add(data);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture.theData");
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