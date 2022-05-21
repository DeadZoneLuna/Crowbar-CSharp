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
	public class SourceMdlFile10
	{

#region Creation and Destruction

		public SourceMdlFile10(BinaryReader mdlFileReader, SourceMdlFileData10 mdlFileData)
		{
			theInputFileReader = mdlFileReader;
			theMdlFileData = mdlFileData;

			theMdlFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile10(BinaryWriter mdlFileWriter, SourceMdlFileData10 mdlFileData)
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

						theMdlFileData.theSequences.Add(aSequence);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSequence [" + aSequence.theName + "]");

						ReadEvents(aSequence);
						theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aSequence.theEvents alignment");

						ReadPivots(aSequence);
						theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aSequence.thePivots alignment");
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

		//EXTERN int xnode[100][100];
		//	// save transition graph
		//	ptransition	= (byte *)pData;
		//	phdr->numtransitions = numxnodes;
		//	phdr->transitionindex = (pData - pStart);
		//	pData += numxnodes * numxnodes * sizeof( byte );
		//	ALIGN( pData );
		//	for (i = 0; i < numxnodes; i++)
		//	{
		//		for (j = 0; j < numxnodes; j++)
		//		{
		//			*ptransition++ = xnode[i][j];
		//		}
		//	}
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

					theMdlFileData.theBodyParts = new List<SourceMdlBodyPart10>(theMdlFileData.bodyPartCount);
					for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.bodyPartCount; bodyPartIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart10 aBodyPart = new SourceMdlBodyPart10();

						aBodyPart.name = theInputFileReader.ReadChars(64);
						aBodyPart.theName = (new string(aBodyPart.name)).Trim('\0');
						aBodyPart.modelCount = theInputFileReader.ReadInt32();
						aBodyPart.@base = theInputFileReader.ReadInt32();
						aBodyPart.modelOffset = theInputFileReader.ReadInt32();

						theMdlFileData.theBodyParts.Add(aBodyPart);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadModels(aBodyPart);
						if (bodyPartIndex == theMdlFileData.bodyPartCount - 1)
						{
							modelsEndInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart [" + aBodyPart.theName + "]");
					}

					if (modelsEndInputFileStreamPosition > 0)
					{
						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, modelsEndInputFileStreamPosition - 1, 4, "theMdlFileData.theBodyParts - End of Models alignment");

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

					theMdlFileData.theTextures = new List<SourceMdlTexture10>(theMdlFileData.textureCount);
					for (int textureIndex = 0; textureIndex < theMdlFileData.textureCount; textureIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						//boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlTexture10 aTexture = new SourceMdlTexture10();

						//aTexture.fileName = Me.theInputFileReader.ReadChars(64)
						//aTexture.theFileName = CStr(aTexture.fileName).Trim(Chr(0))
						byte[] bytes = theInputFileReader.ReadBytes(64);
						aTexture.theFileName = System.Text.Encoding.Default.GetString(bytes);
						aTexture.theFileName = aTexture.theFileName.Trim('\0');
						aTexture.flags = theInputFileReader.ReadInt32();
						aTexture.width = theInputFileReader.ReadUInt32();
						aTexture.height = theInputFileReader.ReadUInt32();
						aTexture.dataOffset = theInputFileReader.ReadUInt32();

						theMdlFileData.theTextures.Add(aTexture);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aTexture [" + aTexture.theFileName + "]");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadTextureData(aTexture);
						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aTexture.theData alignment");

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

		//FROM: [1999] HLStandardSDK\SourceCode\utils\studiomdl\studiomdl.c
		//void Build_Reference( s_model_t *pmodel)
		//{
		//	int		i, parent;
		//	float	angle[3];
		//
		//	for (i = 0; i < pmodel->numbones; i++)
		//	{
		//		float m[3][4];
		//		vec3_t p;
		//
		//		// convert to degrees
		//		angle[0] = pmodel->skeleton[i].rot[0] * (180.0 / Q_PI);
		//		angle[1] = pmodel->skeleton[i].rot[1] * (180.0 / Q_PI);
		//		angle[2] = pmodel->skeleton[i].rot[2] * (180.0 / Q_PI);
		//
		//		parent = pmodel->node[i].parent;
		//		if (parent == -1) {
		//			// scale the done pos.
		//			// calc rotational matrices
		//			AngleMatrix( angle, bonefixup[i].m );
		//			AngleIMatrix( angle, bonefixup[i].im );
		//			VectorCopy( pmodel->skeleton[i].pos, bonefixup[i].worldorg );
		//		}
		//		else {
		//			// calc compound rotational matrices
		//			// FIXME : Hey, it's orthogical so inv(A) == transpose(A)
		//			AngleMatrix( angle, m );
		//			R_ConcatTransforms( bonefixup[parent].m, m, bonefixup[i].m );
		//			AngleIMatrix( angle, m );
		//			R_ConcatTransforms( m, bonefixup[parent].im, bonefixup[i].im );
		//
		//			// calc true world coord.
		//			VectorTransform(pmodel->skeleton[i].pos, bonefixup[parent].m, p );
		//			VectorAdd( p, bonefixup[parent].worldorg, bonefixup[i].worldorg );
		//		}
		//		// printf("%3d %f %f %f\n", i, bonefixup[i].worldorg[0], bonefixup[i].worldorg[1], bonefixup[i].worldorg[2] );
		//		/*
		//		AngleMatrix( angle, m );
		//		printf("%8.4f %8.4f %8.4f\n", m[0][0], m[1][0], m[2][0] );
		//		printf("%8.4f %8.4f %8.4f\n", m[0][1], m[1][1], m[2][1] );
		//		printf("%8.4f %8.4f %8.4f\n", m[0][2], m[1][2], m[2][2] );
		//		*/
		//	}
		//}
		//				float bonetransform[MAXSTUDIOBONES][3][4];	// bone transformation matrix
		//				float bonematrix[3][4];						// local transformation matrix
		//				vec3_t pos;
		//
		//				for (j = 0; j < numbones; j++)
		//				{
		//					vec3_t angle;
		//
		//					// convert to degrees
		//					angle[0]	= sequence[i].panim[q]->rot[j][n][0] * (180.0 / Q_PI);
		//					angle[1]	= sequence[i].panim[q]->rot[j][n][1] * (180.0 / Q_PI);
		//					angle[2]	= sequence[i].panim[q]->rot[j][n][2] * (180.0 / Q_PI);
		//
		//					AngleMatrix( angle, bonematrix );
		//
		//					bonematrix[0][3] = sequence[i].panim[q]->pos[j][n][0];
		//					bonematrix[1][3] = sequence[i].panim[q]->pos[j][n][1];
		//					bonematrix[2][3] = sequence[i].panim[q]->pos[j][n][2];
		//
		//					if (bonetable[j].parent == -1)
		//					{
		//						MatrixCopy( bonematrix, bonetransform[j] );
		//					}
		//					else
		//					{
		//						R_ConcatTransforms (bonetransform[bonetable[j].parent], bonematrix, bonetransform[j]);
		//					}
		//				}
		// Reverse the above commented section of code. Although the above code transforms animation frames, 
		//    it seems to have the correct transforms for converting from MDL mesh data to SMD mesh data.
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

		private void ReadModels(SourceMdlBodyPart10 aBodyPart)
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

				aBodyPart.theModels = new List<SourceMdlModel10>(aBodyPart.modelCount);
				for (int bodyPartIndex = 0; bodyPartIndex < aBodyPart.modelCount; bodyPartIndex++)
				{
					fileOffsetStart = theInputFileReader.BaseStream.Position;
					//modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlModel10 aModel = new SourceMdlModel10();

					aModel.name = theInputFileReader.ReadChars(64);
					aModel.theName = (new string(aModel.name)).Trim('\0');
					aModel.type = theInputFileReader.ReadInt32();
					aModel.boundingRadius = theInputFileReader.ReadSingle();
					aModel.meshCount = theInputFileReader.ReadInt32();
					aModel.meshOffset = theInputFileReader.ReadInt32();

					aModel.vertexCount = theInputFileReader.ReadInt32();
					aModel.vertexBoneInfoOffset = theInputFileReader.ReadInt32();
					aModel.vertexOffset = theInputFileReader.ReadInt32();
					aModel.normalCount = theInputFileReader.ReadInt32();
					aModel.normalBoneInfoOffset = theInputFileReader.ReadInt32();
					aModel.normalOffset = theInputFileReader.ReadInt32();

					aModel.groupCount = theInputFileReader.ReadInt32();
					aModel.groupOffset = theInputFileReader.ReadInt32();

					aBodyPart.theModels.Add(aModel);

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel [" + aModel.theName + "]");

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					ReadModelVertexBoneInfos(aModel);
					ReadModelNormalBoneInfos(aModel);
					ReadModelVertexes(aModel);
					ReadModelNormals(aModel);
					ReadMeshes(aModel);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private void ReadModelVertexBoneInfos(SourceMdlModel10 aModel)
		{
			if (aModel.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					int vertexBoneInfo = 0;
					theInputFileReader.BaseStream.Seek(aModel.vertexBoneInfoOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theVertexBoneInfos = new List<int>(aModel.vertexCount);
					for (int vertexBoneInfoIndex = 0; vertexBoneInfoIndex < aModel.vertexCount; vertexBoneInfoIndex++)
					{
						vertexBoneInfo = theInputFileReader.ReadByte();
						aModel.theVertexBoneInfos.Add(vertexBoneInfo);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexBoneInfos " + aModel.theVertexBoneInfos.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexBoneInfos alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModelNormalBoneInfos(SourceMdlModel10 aModel)
		{
			if (aModel.normalCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					int normalBoneInfo = 0;
					theInputFileReader.BaseStream.Seek(aModel.normalBoneInfoOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theNormalBoneInfos = new List<int>(aModel.normalCount);
					for (int normalBoneInfoIndex = 0; normalBoneInfoIndex < aModel.normalCount; normalBoneInfoIndex++)
					{
						normalBoneInfo = theInputFileReader.ReadByte();
						aModel.theNormalBoneInfos.Add(normalBoneInfo);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormalBoneInfos " + aModel.theNormalBoneInfos.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aModel.theNormalBoneInfos alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModelVertexes(SourceMdlModel10 aModel)
		{
			if (aModel.vertexCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(aModel.vertexOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theVertexes = new List<SourceVector>(aModel.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModel.vertexCount; vertexIndex++)
					{
						SourceVector vertex = new SourceVector();
						vertex.x = theInputFileReader.ReadSingle();
						vertex.y = theInputFileReader.ReadSingle();
						vertex.z = theInputFileReader.ReadSingle();
						aModel.theVertexes.Add(vertex);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theVertexes " + aModel.theVertexes.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aModel.theVertexes alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadModelNormals(SourceMdlModel10 aModel)
		{
			if (aModel.normalCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(aModel.normalOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aModel.theNormals = new List<SourceVector>(aModel.normalCount);
					for (int normalIndex = 0; normalIndex < aModel.normalCount; normalIndex++)
					{
						SourceVector normal = new SourceVector();
						normal.x = theInputFileReader.ReadSingle();
						normal.y = theInputFileReader.ReadSingle();
						normal.z = theInputFileReader.ReadSingle();
						aModel.theNormals.Add(normal);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormals " + aModel.theNormals.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aModel.theNormals alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadMeshes(SourceMdlModel10 aModel)
		{
			if (aModel.meshCount > 0)
			{
				//Dim meshInputFileStreamPosition As Long
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(aModel.meshOffset, SeekOrigin.Begin);

					aModel.theMeshes = new List<SourceMdlMesh10>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlMesh10 aMesh = new SourceMdlMesh10();

						aMesh.faceCount = theInputFileReader.ReadInt32();
						aMesh.faceOffset = theInputFileReader.ReadInt32();
						aMesh.skinref = theInputFileReader.ReadInt32();
						aMesh.normalCount = theInputFileReader.ReadInt32();
						aMesh.normalOffset = theInputFileReader.ReadInt32();

						aModel.theMeshes.Add(aMesh);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						ReadFaces(aMesh);

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadFaces(SourceMdlMesh10 aMesh)
		{
			if (aMesh.faceCount > 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				try
				{
					theInputFileReader.BaseStream.Seek(aMesh.faceOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aMesh.theStripsAndFans = new List<SourceMeshTriangleStripOrFan10>();
					//For faceIndex As Integer = 0 To aMesh.faceCount - 1
					while (true)
					{
						SourceMeshTriangleStripOrFan10 aStripOrFan = new SourceMeshTriangleStripOrFan10();

						short listCount = theInputFileReader.ReadInt16();
						if (listCount == 0)
						{
							//NOTE: End of list marker has been reached. 
							//Exit For
							break;
						}
						else
						{
							aStripOrFan.theVertexInfos = new List<SourceMdlVertexInfo10>();
							if (listCount < 0)
							{
								aStripOrFan.theFacesAreStoredAsTriangleStrips = false;
							}
							else
							{
								aStripOrFan.theFacesAreStoredAsTriangleStrips = true;
							}
						}
						listCount = (short)Math.Abs(listCount);

						for (int listIndex = 0; listIndex < listCount; listIndex++)
						{
							SourceMdlVertexInfo10 vertexAndNormalIndexInfo = new SourceMdlVertexInfo10();
							vertexAndNormalIndexInfo.vertexIndex = theInputFileReader.ReadUInt16();
							vertexAndNormalIndexInfo.normalIndex = theInputFileReader.ReadUInt16();
							vertexAndNormalIndexInfo.s = theInputFileReader.ReadInt16();
							vertexAndNormalIndexInfo.t = theInputFileReader.ReadInt16();

							aStripOrFan.theVertexInfos.Add(vertexAndNormalIndexInfo);
						}

						aMesh.theStripsAndFans.Add(aStripOrFan);

						//If faceIndex = aMesh.faceCount - 1 Then
						//	' The list should end with a zero.
						//	Dim endOfListMarker As Short
						//	endOfListMarker = Me.theInputFileReader.ReadInt16()
						//	If endOfListMarker <> 0 Then
						//		Dim endOfListMarkerIsNotZero As Integer = 4242
						//	End If
						//End If
					}
					//Next

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFaces " + aMesh.theStripsAndFans.Count.ToString());

					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aMesh.theFaces alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadTextureData(SourceMdlTexture10 aTexture)
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

		protected SourceMdlFileData10 theMdlFileData;

#endregion

	}

}