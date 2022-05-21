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
	public class SourceMdlFile49
	{

#region Creation and Destruction

		public SourceMdlFile49(BinaryReader mdlFileReader, SourceMdlFileData49 mdlFileData)
		{
			theInputFileReader = mdlFileReader;
			theMdlFileData = mdlFileData;

			theMdlFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile49(BinaryWriter mdlFileWriter, SourceMdlFileData49 mdlFileData)
		{
			theOutputFileWriter = mdlFileWriter;
			theMdlFileData = mdlFileData;
		}

		// Only here to make compiler happy with SourceAniFileXX that inherits from this class.
		protected SourceMdlFile49()
		{
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

			theMdlFileData.name = theInputFileReader.ReadChars(64);
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

			// Offsets: 0x50, 0x54, 0x58
			theMdlFileData.eyePosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.eyePosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.eyePosition.z = theInputFileReader.ReadSingle();

			// Offsets: 0x5C, 0x60, 0x64
			theMdlFileData.illuminationPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.z = theInputFileReader.ReadSingle();

			// Offsets: 0x68, 0x6C, 0x70
			theMdlFileData.hullMinPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.hullMinPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.hullMinPosition.z = theInputFileReader.ReadSingle();

			// Offsets: 0x74, 0x78, 0x7C
			theMdlFileData.hullMaxPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.hullMaxPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.hullMaxPosition.z = theInputFileReader.ReadSingle();

			// Offsets: 0x80, 0x84, 0x88
			theMdlFileData.viewBoundingBoxMinPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMinPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMinPosition.z = theInputFileReader.ReadSingle();

			// Offsets: 0x8C, 0x90, 0x94
			theMdlFileData.viewBoundingBoxMaxPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMaxPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMaxPosition.z = theInputFileReader.ReadSingle();

			// Offsets: 0x98
			theMdlFileData.flags = theInputFileReader.ReadInt32();

			// Offsets: 0x9C (156), 0xA0
			theMdlFileData.boneCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xA4, 0xA8
			theMdlFileData.boneControllerCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneControllerOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xAC (172), 0xB0
			theMdlFileData.hitboxSetCount = theInputFileReader.ReadInt32();
			theMdlFileData.hitboxSetOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xB4 (180), 0xB8
			theMdlFileData.localAnimationCount = theInputFileReader.ReadInt32();
			theMdlFileData.localAnimationOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xBC (188), 0xC0 (192)
			theMdlFileData.localSequenceCount = theInputFileReader.ReadInt32();
			theMdlFileData.localSequenceOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xC4, 0xC8
			theMdlFileData.activityListVersion = theInputFileReader.ReadInt32();
			theMdlFileData.eventsIndexed = theInputFileReader.ReadInt32();

			// Offsets: 0xCC (204), 0xD0 (208)
			theMdlFileData.textureCount = theInputFileReader.ReadInt32();
			theMdlFileData.textureOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xD4 (212), 0xD8
			theMdlFileData.texturePathCount = theInputFileReader.ReadInt32();
			theMdlFileData.texturePathOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xDC, 0xE0 (224), 0xE4 (228)
			theMdlFileData.skinReferenceCount = theInputFileReader.ReadInt32();
			theMdlFileData.skinFamilyCount = theInputFileReader.ReadInt32();
			theMdlFileData.skinFamilyOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xE8 (232), 0xEC (236)
			theMdlFileData.bodyPartCount = theInputFileReader.ReadInt32();
			theMdlFileData.bodyPartOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xF0 (240), 0xF4 (244)
			theMdlFileData.localAttachmentCount = theInputFileReader.ReadInt32();
			theMdlFileData.localAttachmentOffset = theInputFileReader.ReadInt32();

			// Offsets: 0xF8, 0xFC, 0x0100
			theMdlFileData.localNodeCount = theInputFileReader.ReadInt32();
			theMdlFileData.localNodeOffset = theInputFileReader.ReadInt32();

			theMdlFileData.localNodeNameOffset = theInputFileReader.ReadInt32();

			// Offsets: 0x0104 (), 0x0108 ()
			theMdlFileData.flexDescCount = theInputFileReader.ReadInt32();
			theMdlFileData.flexDescOffset = theInputFileReader.ReadInt32();

			// Offsets: 0x010C (), 0x0110 ()
			theMdlFileData.flexControllerCount = theInputFileReader.ReadInt32();
			theMdlFileData.flexControllerOffset = theInputFileReader.ReadInt32();

			// Offsets: 0x0114 (), 0x0118 ()
			theMdlFileData.flexRuleCount = theInputFileReader.ReadInt32();
			theMdlFileData.flexRuleOffset = theInputFileReader.ReadInt32();

			// Offsets: 0x011C (), 0x0120 ()
			theMdlFileData.ikChainCount = theInputFileReader.ReadInt32();
			theMdlFileData.ikChainOffset = theInputFileReader.ReadInt32();

			// Offsets: 0x0124 (), 0x0128 ()
			theMdlFileData.mouthCount = theInputFileReader.ReadInt32();
			theMdlFileData.mouthOffset = theInputFileReader.ReadInt32();

			// Offsets: 0x012C (), 0x0130 ()
			theMdlFileData.localPoseParamaterCount = theInputFileReader.ReadInt32();
			theMdlFileData.localPoseParameterOffset = theInputFileReader.ReadInt32();

			// Offsets: 0x0134 ()
			theMdlFileData.surfacePropOffset = theInputFileReader.ReadInt32();

			//TODO: Same as some lines below. Move to a separate function.
			if (theMdlFileData.surfacePropOffset > 0)
			{
				inputFileStreamPosition = theInputFileReader.BaseStream.Position;
				theInputFileReader.BaseStream.Seek(theMdlFileData.surfacePropOffset, SeekOrigin.Begin);
				fileOffsetStart2 = theInputFileReader.BaseStream.Position;

				theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(theInputFileReader);

				fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
				//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName = " + theMdlFileData.theSurfacePropName);
				//End If
				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}
			else
			{
				theMdlFileData.theSurfacePropName = "";
			}

			// Offsets: 0x0138 (312), 0x013C (316)
			theMdlFileData.keyValueOffset = theInputFileReader.ReadInt32();
			theMdlFileData.keyValueSize = theInputFileReader.ReadInt32();

			theMdlFileData.localIkAutoPlayLockCount = theInputFileReader.ReadInt32();
			theMdlFileData.localIkAutoPlayLockOffset = theInputFileReader.ReadInt32();

			theMdlFileData.mass = theInputFileReader.ReadSingle();
			theMdlFileData.contents = theInputFileReader.ReadInt32();

			theMdlFileData.includeModelCount = theInputFileReader.ReadInt32();
			theMdlFileData.includeModelOffset = theInputFileReader.ReadInt32();

			theMdlFileData.virtualModelP = theInputFileReader.ReadInt32();

			theMdlFileData.animBlockNameOffset = theInputFileReader.ReadInt32();
			theMdlFileData.animBlockCount = theInputFileReader.ReadInt32();
			theMdlFileData.animBlockOffset = theInputFileReader.ReadInt32();
			theMdlFileData.animBlockModelP = theInputFileReader.ReadInt32();
			if (theMdlFileData.animBlockCount > 0)
			{
				if (theMdlFileData.animBlockNameOffset > 0)
				{
					inputFileStreamPosition = theInputFileReader.BaseStream.Position;
					theInputFileReader.BaseStream.Seek(theMdlFileData.animBlockNameOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;

					theMdlFileData.theAnimBlockRelativePathFileName = FileManager.ReadNullTerminatedString(theInputFileReader);

					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theAnimBlockRelativePathFileName = " + theMdlFileData.theAnimBlockRelativePathFileName);
					//End If
					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
				if (theMdlFileData.animBlockOffset > 0)
				{
					inputFileStreamPosition = theInputFileReader.BaseStream.Position;
					theInputFileReader.BaseStream.Seek(theMdlFileData.animBlockOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;

					theMdlFileData.theAnimBlocks = new List<SourceMdlAnimBlock>(theMdlFileData.animBlockCount);
					for (int offset = 0; offset < theMdlFileData.animBlockCount; offset++)
					{
						SourceMdlAnimBlock anAnimBlock = new SourceMdlAnimBlock();
						anAnimBlock.dataStart = theInputFileReader.ReadInt32();
						anAnimBlock.dataEnd = theInputFileReader.ReadInt32();
						theMdlFileData.theAnimBlocks.Add(anAnimBlock);
					}

					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theAnimBlocks " + theMdlFileData.theAnimBlocks.Count.ToString());
					//End If
					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}

			theMdlFileData.boneTableByNameOffset = theInputFileReader.ReadInt32();

			theMdlFileData.vertexBaseP = theInputFileReader.ReadInt32();
			theMdlFileData.indexBaseP = theInputFileReader.ReadInt32();

			theMdlFileData.directionalLightDot = theInputFileReader.ReadByte();

			theMdlFileData.rootLod = theInputFileReader.ReadByte();

			theMdlFileData.allowedRootLodCount_VERSION48 = theInputFileReader.ReadByte();

			theMdlFileData.unused = theInputFileReader.ReadByte();

			theMdlFileData.zeroframecacheindex_VERSION44to47 = theInputFileReader.ReadInt32();

			theMdlFileData.flexControllerUiCount = theInputFileReader.ReadInt32();
			theMdlFileData.flexControllerUiOffset = theInputFileReader.ReadInt32();

			theMdlFileData.vertAnimFixedPointScale = theInputFileReader.ReadSingle();
			theMdlFileData.surfacePropLookup = theInputFileReader.ReadInt32();

			theMdlFileData.studioHeader2Offset = theInputFileReader.ReadInt32();

			theMdlFileData.unused2 = theInputFileReader.ReadInt32();

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription);

			if (theMdlFileData.bodyPartCount == 0 && theMdlFileData.localSequenceCount > 0)
			{
				theMdlFileData.theMdlFileOnlyHasAnimations = true;
			}
		}

		public void ReadMdlHeader02(string logDescription)
		{
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			long fileOffsetStart2 = 0;
			long fileOffsetEnd2 = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theMdlFileData.sourceBoneTransformCount = theInputFileReader.ReadInt32();
			theMdlFileData.sourceBoneTransformOffset = theInputFileReader.ReadInt32();
			theMdlFileData.illumPositionAttachmentNumber = theInputFileReader.ReadInt32();
			theMdlFileData.maxEyeDeflection = theInputFileReader.ReadSingle();
			theMdlFileData.linearBoneOffset = theInputFileReader.ReadInt32();

			//TODO: According to MDL v48 source code, the following fields are not used.
			//      Test various MDL v48 models to see if any use these. 

			theMdlFileData.nameCopyOffset = theInputFileReader.ReadInt32();
			if (theMdlFileData.nameCopyOffset > 0)
			{
				inputFileStreamPosition = theInputFileReader.BaseStream.Position;
				theInputFileReader.BaseStream.Seek(fileOffsetStart + theMdlFileData.nameCopyOffset, SeekOrigin.Begin);
				fileOffsetStart2 = theInputFileReader.BaseStream.Position;

				theMdlFileData.theNameCopy = FileManager.ReadNullTerminatedString(theInputFileReader);
				theMdlFileData.theModelName = theMdlFileData.theNameCopy;

				fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theNameCopy = " + theMdlFileData.theNameCopy);
				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}
			else
			{
				theMdlFileData.theNameCopy = "";
			}

			theMdlFileData.boneFlexDriverCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneFlexDriverOffset = theInputFileReader.ReadInt32();

			for (int x = 0; x < theMdlFileData.reserved.Length; x++)
			{
				theMdlFileData.reserved[x] = theInputFileReader.ReadInt32();
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription);
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

					theMdlFileData.theBones = new List<SourceMdlBone>(theMdlFileData.boneCount);
					for (int boneIndex = 0; boneIndex < theMdlFileData.boneCount; boneIndex++)
					{
						boneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlBone aBone = new SourceMdlBone();

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

						aBone.quat = new SourceQuaternion();
						aBone.quat.x = theInputFileReader.ReadSingle();
						aBone.quat.y = theInputFileReader.ReadSingle();
						aBone.quat.z = theInputFileReader.ReadSingle();
						aBone.quat.w = theInputFileReader.ReadSingle();

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
						aBone.contents = theInputFileReader.ReadInt32();

						for (int k = 0; k <= 7; k++)
						{
							aBone.unused[k] = theInputFileReader.ReadInt32();
						}

						theMdlFileData.theBones.Add(aBone);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aBone.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aBone.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theName = " + aBone.theName);
							//End If
						}
						else if (aBone.theName == null)
						{
							aBone.theName = "";
						}
						theMdlFileData.theBoneNameToBoneIndexMap.Add(aBone.theName, boneIndex);

						if (aBone.proceduralRuleOffset != 0)
						{
							if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_AXISINTERP)
							{
								//TODO: The text file for this info seems to be in a different file than the VRD file.
								ReadAxisInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_QUATINTERP)
							{
								theMdlFileData.theProceduralBonesCommandIsUsed = true;
								ReadQuatInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_AIMATBONE || aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_AIMATATTACH)
							{
								theMdlFileData.theProceduralBonesCommandIsUsed = true;
								// Used by pistons on Portal 2 "player\ballbot.mdl" model.
								ReadAimAtBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_JIGGLE)
							{
								ReadJiggleBone(boneInputFileStreamPosition, aBone);
							}
						}

						if (aBone.surfacePropNameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.surfacePropNameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aBone.theSurfacePropName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theSurfacePropName = " + aBone.theSurfacePropName);
							//End If
						}
						else
						{
							aBone.theSurfacePropName = "";
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones " + theMdlFileData.theBones.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBones alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		//TODO: VERIFY ReadAxisInterpBone()
		private void ReadAxisInterpBone(long boneInputFileStreamPosition, SourceMdlBone aBone)
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
			catch
			{
			}
		}

		private void ReadQuatInterpBone(long boneInputFileStreamPosition, SourceMdlBone aBone)
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
			catch
			{
			}
		}

		private void ReadAimAtBone(long boneInputFileStreamPosition, SourceMdlBone aBone)
		{
			long aimAtBoneInputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			try
			{
				theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aimAtBoneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				aBone.theAimAtBone = new SourceMdlAimAtBone();
				aBone.theAimAtBone.parentBoneIndex = theInputFileReader.ReadInt32();
				aBone.theAimAtBone.aimBoneOrAttachmentIndex = theInputFileReader.ReadInt32();

				aBone.theAimAtBone.aim = new SourceVector();
				aBone.theAimAtBone.aim.x = theInputFileReader.ReadSingle();
				aBone.theAimAtBone.aim.y = theInputFileReader.ReadSingle();
				aBone.theAimAtBone.aim.z = theInputFileReader.ReadSingle();

				aBone.theAimAtBone.up = new SourceVector();
				aBone.theAimAtBone.up.x = theInputFileReader.ReadSingle();
				aBone.theAimAtBone.up.y = theInputFileReader.ReadSingle();
				aBone.theAimAtBone.up.z = theInputFileReader.ReadSingle();

				aBone.theAimAtBone.basePos = new SourceVector();
				aBone.theAimAtBone.basePos.x = theInputFileReader.ReadSingle();
				aBone.theAimAtBone.basePos.y = theInputFileReader.ReadSingle();
				aBone.theAimAtBone.basePos.z = theInputFileReader.ReadSingle();

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAimAtBone");
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
			catch
			{
			}
		}

		private void ReadJiggleBone(long boneInputFileStreamPosition, SourceMdlBone aBone)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			aBone.theJiggleBone = new SourceMdlJiggleBone();
			aBone.theJiggleBone.flags = theInputFileReader.ReadInt32();
			aBone.theJiggleBone.length = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.tipMass = theInputFileReader.ReadSingle();

			aBone.theJiggleBone.yawStiffness = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.yawDamping = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.pitchStiffness = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.pitchDamping = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.alongStiffness = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.alongDamping = theInputFileReader.ReadSingle();

			aBone.theJiggleBone.angleLimit = theInputFileReader.ReadSingle();

			aBone.theJiggleBone.minYaw = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.maxYaw = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.yawFriction = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.yawBounce = theInputFileReader.ReadSingle();

			aBone.theJiggleBone.minPitch = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.maxPitch = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.pitchFriction = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.pitchBounce = theInputFileReader.ReadSingle();

			aBone.theJiggleBone.baseMass = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseStiffness = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseDamping = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMinLeft = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMaxLeft = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseLeftFriction = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMinUp = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMaxUp = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseUpFriction = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMinForward = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMaxForward = theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseForwardFriction = theInputFileReader.ReadSingle();

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theJiggleBone");
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

				theInputFileReader.BaseStream.Seek(theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theBoneControllers = new List<SourceMdlBoneController>(theMdlFileData.boneControllerCount);
				for (int i = 0; i < theMdlFileData.boneControllerCount; i++)
				{
					boneControllerInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlBoneController aBoneController = new SourceMdlBoneController();

					aBoneController.boneIndex = theInputFileReader.ReadInt32();
					aBoneController.type = theInputFileReader.ReadInt32();
					aBoneController.startBlah = theInputFileReader.ReadSingle();
					aBoneController.endBlah = theInputFileReader.ReadSingle();
					aBoneController.restIndex = theInputFileReader.ReadInt32();
					aBoneController.inputField = theInputFileReader.ReadInt32();
					if (theMdlFileData.version > 10)
					{
						for (int x = 0; x < aBoneController.unused.Length; x++)
						{
							aBoneController.unused[x] = theInputFileReader.ReadInt32();
						}
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

				theInputFileReader.BaseStream.Seek(theMdlFileData.localAttachmentOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theAttachments = new List<SourceMdlAttachment>(theMdlFileData.localAttachmentCount);
				for (int i = 0; i < theMdlFileData.localAttachmentCount; i++)
				{
					attachmentInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlAttachment anAttachment = new SourceMdlAttachment();

					if (theMdlFileData.version == 10)
					{
						anAttachment.name = theInputFileReader.ReadChars(32);
						anAttachment.theName = new string(anAttachment.name);
						anAttachment.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(anAttachment.theName);
						anAttachment.type = theInputFileReader.ReadInt32();
						anAttachment.bone = theInputFileReader.ReadInt32();

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
					}
					else
					{
						anAttachment.nameOffset = theInputFileReader.ReadInt32();
						anAttachment.flags = theInputFileReader.ReadInt32();
						anAttachment.localBoneIndex = theInputFileReader.ReadInt32();
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
						for (int x = 0; x <= 7; x++)
						{
							anAttachment.unused[x] = theInputFileReader.ReadInt32();
						}
					}

					theMdlFileData.theAttachments.Add(anAttachment);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (anAttachment.nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						anAttachment.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName = " + anAttachment.theName);
						//End If
					}
					else
					{
						anAttachment.theName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + theMdlFileData.theAttachments.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment");
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

				theInputFileReader.BaseStream.Seek(theMdlFileData.hitboxSetOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theHitboxSets = new List<SourceMdlHitboxSet>(theMdlFileData.hitboxSetCount);
				for (int i = 0; i < theMdlFileData.hitboxSetCount; i++)
				{
					hitboxSetInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlHitboxSet aHitboxSet = new SourceMdlHitboxSet();
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
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitboxSet.theName = " + aHitboxSet.theName);
						//End If
					}
					else
					{
						aHitboxSet.theName = "";
					}
					ReadHitboxes(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, aHitboxSet);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets " + theMdlFileData.theHitboxSets.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment");
			}
		}

		private void ReadHitboxes(long hitboxOffsetInputFileStreamPosition, SourceMdlHitboxSet aHitboxSet)
		{
			if (aHitboxSet.hitboxCount > 0)
			{
				long hitboxInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(hitboxOffsetInputFileStreamPosition, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aHitboxSet.theHitboxes = new List<SourceMdlHitbox>(aHitboxSet.hitboxCount);
				for (int j = 0; j < aHitboxSet.hitboxCount; j++)
				{
					hitboxInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlHitbox aHitbox = new SourceMdlHitbox();

					aHitbox.boneIndex = theInputFileReader.ReadInt32();
					aHitbox.groupIndex = theInputFileReader.ReadInt32();
					aHitbox.boundingBoxMin.x = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMin.y = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMin.z = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.x = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.y = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.z = theInputFileReader.ReadSingle();
					aHitbox.nameOffset = theInputFileReader.ReadInt32();
					//NOTE: Roll (z) is first.
					aHitbox.boundingBoxPitchYawRoll.z = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxPitchYawRoll.x = theInputFileReader.ReadSingle();
					aHitbox.boundingBoxPitchYawRoll.y = theInputFileReader.ReadSingle();
					aHitbox.unknown = theInputFileReader.ReadSingle();
					for (int x = 0; x < aHitbox.unused_VERSION49.Length; x++)
					{
						aHitbox.unused_VERSION49[x] = theInputFileReader.ReadInt32();
					}

					aHitboxSet.theHitboxes.Add(aHitbox);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					//NOTE: Ignore getting the hitbox name because it's not important for compiling, 
					//      and unknown when to use nameOffset as absolute or relative offset.
					//NOTE: I looked at many models from various games that use MDL44 and none of them had anything for the name. 
					//NOTE: Contrary to the studiomdl source code for MDL44, the nameOffset is absolute offset for the following tested models 
					//      from Half-Life 2\hl1\hl1_pak_dir\models: agrunt, scientist
					//      Me.theInputFileReader.BaseStream.Seek(aHitbox.nameOffset, SeekOrigin.Begin)
					//NOTE: A custom CSS model (MDL v48) has "aHitbox.nameOffset <> 0", 
					//      and it looks like the aHitbox.nameOffset is intended to be an absolute offset 
					//      to the initial null byte at the start of the string list at end of file.
					//      CSS HLMV opens and shows the hitboxes.
					//NOTE: The "SCAL" seems to be in every MDL file in Vindictus.
					if (aHitbox.nameOffset != 0 && ((theMdlFileData.version == 44 && theMdlFileData.flexControllerUiOffset == SourceMdlFileData49.text_SCAL_VERSION44Vindictus) || theMdlFileData.version == 49))
					{
						//NOTE: For Vindictus pet_succubus_tiny.mdl (MDL44), relative offset is correct.
						theInputFileReader.BaseStream.Seek(hitboxInputFileStreamPosition + aHitbox.nameOffset, SeekOrigin.Begin);
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
		}

		public void ReadBoneTableByName()
		{
			if (theMdlFileData.boneTableByNameOffset != 0 && theMdlFileData.theBones != null)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				theInputFileReader.BaseStream.Seek(theMdlFileData.boneTableByNameOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theBoneTableByName = new List<int>(theMdlFileData.theBones.Count);
				byte index = 0;
				for (int i = 0; i < theMdlFileData.theBones.Count; i++)
				{
					index = theInputFileReader.ReadByte();
					theMdlFileData.theBoneTableByName.Add(index);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneTableByName");
			}
		}

		public void ReadLocalAnimationDescs()
		{
			long animInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			theInputFileReader.BaseStream.Seek(theMdlFileData.localAnimationOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			theMdlFileData.theAnimationDescs = new List<SourceMdlAnimationDesc49>(theMdlFileData.localAnimationCount);
			for (int i = 0; i < theMdlFileData.localAnimationCount; i++)
			{
				animInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				SourceMdlAnimationDesc49 anAnimationDesc = new SourceMdlAnimationDesc49();

				anAnimationDesc.theOffsetStart = theInputFileReader.BaseStream.Position;

				anAnimationDesc.baseHeaderOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.nameOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.fps = theInputFileReader.ReadSingle();
				anAnimationDesc.flags = theInputFileReader.ReadInt32();
				anAnimationDesc.frameCount = theInputFileReader.ReadInt32();
				anAnimationDesc.movementCount = theInputFileReader.ReadInt32();
				anAnimationDesc.movementOffset = theInputFileReader.ReadInt32();

				anAnimationDesc.ikRuleZeroFrameOffset_VERSION49 = theInputFileReader.ReadInt32();

				for (int x = 0; x < anAnimationDesc.unused1.Length; x++)
				{
					anAnimationDesc.unused1[x] = theInputFileReader.ReadInt32();
				}

				anAnimationDesc.animBlock = theInputFileReader.ReadInt32();
				anAnimationDesc.animOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.ikRuleCount = theInputFileReader.ReadInt32();
				anAnimationDesc.ikRuleOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.animblockIkRuleOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.localHierarchyCount = theInputFileReader.ReadInt32();
				anAnimationDesc.localHierarchyOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.sectionOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.sectionFrameCount = theInputFileReader.ReadInt32();

				anAnimationDesc.spanFrameCount = theInputFileReader.ReadInt16();
				anAnimationDesc.spanCount = theInputFileReader.ReadInt16();
				anAnimationDesc.spanOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.spanStallTime = theInputFileReader.ReadSingle();

				theMdlFileData.theAnimationDescs.Add(anAnimationDesc);

				inputFileStreamPosition = theInputFileReader.BaseStream.Position;

				ReadAnimationDescName(animInputFileStreamPosition, anAnimationDesc);
				ReadAnimationDescSpanData(animInputFileStreamPosition, anAnimationDesc);
				ReadMdlMovements(animInputFileStreamPosition, anAnimationDesc);

				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs " + theMdlFileData.theAnimationDescs.Count.ToString());
		}

		public void ReadAnimationSections()
		{
			if (theMdlFileData.theAnimationDescs != null)
			{
				foreach (SourceMdlAnimationDesc49 anAnimationDesc in theMdlFileData.theAnimationDescs)
				{
					anAnimationDesc.theSectionsOfFrameAnim = new List<SourceAniFrameAnim49>();
					SourceAniFrameAnim49 aSectionOfFrameAnimation = new SourceAniFrameAnim49();
					anAnimationDesc.theSectionsOfFrameAnim.Add(aSectionOfFrameAnimation);

					anAnimationDesc.theSectionsOfAnimations = new List<List<SourceMdlAnimation>>();
					List<SourceMdlAnimation> aSectionOfAnimation = new List<SourceMdlAnimation>();
					anAnimationDesc.theSectionsOfAnimations.Add(aSectionOfAnimation);

					if (anAnimationDesc.sectionOffset != 0 && anAnimationDesc.sectionFrameCount > 0)
					{
						//TODO: Shouldn't this be set to largest sectionFrameCount?
						theMdlFileData.theSectionFrameCount = anAnimationDesc.sectionFrameCount;
						if (theMdlFileData.theSectionFrameMinFrameCount >= anAnimationDesc.frameCount)
						{
							theMdlFileData.theSectionFrameMinFrameCount = anAnimationDesc.frameCount - 1;
						}

						//FROM: simplify.cpp:
						//      panim->numsections = (int)(panim->numframes / panim->sectionframes) + 2;
						//NOTE: It is unclear why "+ 2" is used in studiomdl.
						int sectionCount = Convert.ToInt32(Math.Truncate(anAnimationDesc.frameCount / (double)anAnimationDesc.sectionFrameCount)) + 2;

						//NOTE: First sectionOfAnimation was created above.
						for (int sectionIndex = 1; sectionIndex < sectionCount; sectionIndex++)
						{
							aSectionOfFrameAnimation = new SourceAniFrameAnim49();
							anAnimationDesc.theSectionsOfFrameAnim.Add(aSectionOfFrameAnimation);

							aSectionOfAnimation = new List<SourceMdlAnimation>();
							anAnimationDesc.theSectionsOfAnimations.Add(aSectionOfAnimation);
						}

						long offset = anAnimationDesc.theOffsetStart + anAnimationDesc.sectionOffset;
						if (offset != theInputFileReader.BaseStream.Position)
						{
							//TODO: It looks like more than one animDesc can point to same sections, so need to revise how this is done.
							//Me.theMdlFileData.theFileSeekLog.Add(Me.theInputFileReader.BaseStream.Position, Me.theInputFileReader.BaseStream.Position, "[ERROR] anAnimationDesc.theSections [" + anAnimationDesc.theName + "] offset mismatch: pos = " + Me.theInputFileReader.BaseStream.Position.ToString() + " offset = " + offset.ToString())
							theInputFileReader.BaseStream.Seek(offset, SeekOrigin.Begin);
						}

						anAnimationDesc.theSections = new List<SourceMdlAnimationSection>(sectionCount);
						for (int sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++)
						{
							ReadMdlAnimationSection(theInputFileReader.BaseStream.Position, anAnimationDesc, theMdlFileData.theFileSeekLog);
						}
					}
				}
			}
		}

		public void ReadAnimationMdlBlocks()
		{
			if (theMdlFileData.theAnimationDescs != null)
			{
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionIndex = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionFrameCount = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				SourceMdlAnimationSection section = null;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				long adjustedAnimOffset = 0;
				foreach (SourceMdlAnimationDesc49 anAnimationDesc in theMdlFileData.theAnimationDescs)
				{
					try
					{
						long animInputFileStreamPosition = anAnimationDesc.theOffsetStart;
	//					Dim sectionIndex As Integer

						//NOTE: Need to check section.animBlock no matter what anAnimationDesc.animBlock is.
						if (anAnimationDesc.theSections != null && anAnimationDesc.theSections.Count > 0)
						{
							int sectionCount = anAnimationDesc.theSections.Count;
	//						Dim sectionFrameCount As Integer
	//						Dim section As SourceMdlAnimationSection
	//						Dim adjustedAnimOffset As Long

							for (sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++)
							{
								section = anAnimationDesc.theSections[sectionIndex];
								if (section.animBlock == 0)
								{
									//NOTE: This is weird, but it fits with a few oddball models (such as L4D2 "left4dead2\ghostanim.mdl") while not messing up the normal ones.
									adjustedAnimOffset = section.animOffset + (anAnimationDesc.animOffset - anAnimationDesc.theSections[0].animOffset);

									if (sectionIndex < sectionCount - 2)
									{
										sectionFrameCount = anAnimationDesc.sectionFrameCount;
									}
									else
									{
										//NOTE: Due to the weird calculation of sectionCount in studiomdl, this line is called twice, which means there are two "last" sections.
										//      This also likely means that the last section is bogus unused data.
										sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 2) * anAnimationDesc.sectionFrameCount);
									}

									ReadAnimationFrames(animInputFileStreamPosition + adjustedAnimOffset, anAnimationDesc, sectionFrameCount, sectionIndex, (sectionIndex >= sectionCount - 2) || (anAnimationDesc.frameCount == (sectionIndex + 1) * anAnimationDesc.sectionFrameCount));
								}
							}
						}
						else if (anAnimationDesc.animBlock == 0)
						{
							sectionIndex = 0;
							ReadAnimationFrames(animInputFileStreamPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex, true);
						}

						//NOTE: These seem to always be stored in the MDL file for MDL44.
						if (theMdlFileData.version == 44 || anAnimationDesc.animBlock == 0)
						{
							ReadMdlIkRules(animInputFileStreamPosition, anAnimationDesc);
							ReadLocalHierarchies(animInputFileStreamPosition, anAnimationDesc);
						}
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
				}
			}
		}

		protected void ReadAnimationDescName(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc)
		{
			if (anAnimationDesc.nameOffset != 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				anAnimationDesc.theName = FileManager.ReadNullTerminatedString(theInputFileReader);
				//If anAnimDesc.theName(0) = "@" Then
				//	anAnimDesc.theName = anAnimDesc.theName.Remove(0, 1)
				//End If

				//NOTE: This naming is found in Garry's Mod garrysmod_dir.vpk "\models\m_anm.mdl":  "a_../combine_soldier_xsi/Hold_AR2_base.smd"
				if (anAnimationDesc.theName.StartsWith("a_../") || anAnimationDesc.theName.StartsWith("a_..\\"))
				{
					anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 5);
					anAnimationDesc.theName = Path.Combine(FileManager.GetPath(anAnimationDesc.theName), "a_" + Path.GetFileName(anAnimationDesc.theName));
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theName = " + anAnimationDesc.theName);
			}
			else
			{
				anAnimationDesc.theName = "";
			}
		}

		//			for (j = 0; j < g_numbones; j++)
		//			{
		//				if (g_bonetable[j].flags & BONE_HAS_SAVEFRAME_POS)
		//				{
		//					for (int n = 0; n < panimdesc[i].zeroframecount; n++)
		//					{
		//						*(Vector48 *)pData = anim->sanim[panimdesc[i].zeroframespan*n][j].pos;
		//						pData += sizeof( Vector48 );
		//					}
		//				}
		//				if (g_bonetable[j].flags & BONE_HAS_SAVEFRAME_ROT)
		//				{
		//					for (int n = 0; n < panimdesc[i].zeroframecount; n++)
		//					{
		//						Quaternion q;
		//						AngleQuaternion( anim->sanim[panimdesc[i].zeroframespan*n][j].rot, q );
		//						*((Quaternion64 *)pData) = q;
		//						pData += sizeof( Quaternion64 );
		//					}
		//				}
		//			}
		protected long ReadAnimationDescSpanData(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			// The data seems to be copy of first several anim frames in the ANI file.
			if (anAnimationDesc.spanFrameCount != 0 || anAnimationDesc.spanCount != 0 || anAnimationDesc.spanOffset != 0 || anAnimationDesc.spanStallTime != 0)
			{
				//NOTE: This code is reached by HL2 antlion.mdl and HL2:EP1 ghostanim_van.mdl.
				//NOTE: This code is reached by L4D2's pak01_dir.vpk\models\v_models\v_huntingrifle.mdl and v_snip_awp.mdl.
				//NOTE: This code is reached by DoI's doi_models_dir_vpk\models\weapons\v_g43.mdl and v_vickers.mdl.
				fileOffsetStart = animInputFileStreamPosition + anAnimationDesc.spanOffset;
				fileOffsetEnd = animInputFileStreamPosition + anAnimationDesc.spanOffset - 1;
				SourceMdlBone aBone = null;
				for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
				{
					aBone = theMdlFileData.theBones[boneIndex];
					if ((aBone.flags & SourceMdlBone.BONE_HAS_SAVEFRAME_POS) > 0)
					{
						//SourceVector48bits (6 bytes)
						fileOffsetEnd += anAnimationDesc.spanCount * 6;
					}
					if ((aBone.flags & SourceMdlBone.BONE_HAS_SAVEFRAME_ROT) > 0)
					{
						//SourceQuaternion64bits (8 bytes)
						fileOffsetEnd += anAnimationDesc.spanCount * 8;
					}
					if ((aBone.flags & SourceMdlBone.BONE_HAS_SAVEFRAME_ROT32) > 0)
					{
						//SourceQuaternion32bits (4 bytes)
						fileOffsetEnd += anAnimationDesc.spanCount * 4;
					}
					if (aBone.flags > 0xFFFFFF)
					{
						int debug = 4242;
					}
				}

				theMdlFileData.theAnimBlockSizeNoStallOptionIsUsed = true;

				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.spanOffset (zeroframes/saveframes) [" + anAnimationDesc.theName + "] [spanFrameCount = " + anAnimationDesc.spanFrameCount.ToString() + "] [spanCount = " + anAnimationDesc.spanCount.ToString() + "]");

				//'TEST: 
				//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.spanOffset (zeroframes/saveframes) alignment")
				//'TEST: 
				//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 8, "anAnimationDesc.spanOffset (zeroframes/saveframes) alignment")
				//'TEST: 
				//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 16, "anAnimationDesc.spanOffset (zeroframes/saveframes) alignment")

				//If fileOffsetEndOfAnimDescsIncludingSpanData < fileOffsetEnd Then
				//	fileOffsetEndOfAnimDescsIncludingSpanData = fileOffsetEnd
				//End If
			}

			return fileOffsetEnd;
		}

		protected void ReadAnimationFrames(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc, int sectionFrameCount, int sectionIndex, bool lastSectionIsBeingRead)
		{
			if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_FRAMEANIM) != 0)
			{
				//TODO: Do any MDL v48 models use this flag?
				//NOTE: This code is reached by L4D2's pak01_dir.vpk\models\v_models\v_huntingrifle.mdl.
				ReadAnimationFrameByBone(animInputFileStreamPosition, anAnimationDesc, sectionFrameCount, sectionIndex, lastSectionIsBeingRead);
			}
			else
			{
				ReadMdlAnimation(animInputFileStreamPosition, anAnimationDesc, sectionFrameCount, sectionIndex, lastSectionIsBeingRead);
			}
		}

		protected void ReadAnimationFrameByBone(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc, int sectionFrameCount, int sectionIndex, bool lastSectionIsBeingRead)
		{
			theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin);

			long animFrameInputFileStreamPosition = 0;
			long boneFrameDataStartInputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			int boneCount = 0;
			byte boneFlag = 0;
			BoneConstantInfo49 aBoneConstantInfo = null;
			List<BoneFrameDataInfo49> aBoneFrameDataInfoList = null;
			BoneFrameDataInfo49 aBoneFrameDataInfo = null;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			SourceAniFrameAnim49 aSectionOfAnimation = anAnimationDesc.theSectionsOfFrameAnim[sectionIndex];

			boneCount = theMdlFileData.theBones.Count;
			try
			{
				animFrameInputFileStreamPosition = theInputFileReader.BaseStream.Position;

				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aSectionOfAnimation.constantsOffset = theInputFileReader.ReadInt32();
				aSectionOfAnimation.frameOffset = theInputFileReader.ReadInt32();
				aSectionOfAnimation.frameLength = theInputFileReader.ReadInt32();
				for (int x = 0; x < aSectionOfAnimation.unused.Length; x++)
				{
					aSectionOfAnimation.unused[x] = theInputFileReader.ReadInt32();
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAniFrameAnim [" + anAnimationDesc.theName + "] (frameCount = " + anAnimationDesc.frameCount.ToString() + "; sectionFrameCount = " + sectionFrameCount.ToString() + ")");

				fileOffsetStart = theInputFileReader.BaseStream.Position;

				aSectionOfAnimation.theBoneFlags = new List<byte>(boneCount);
				for (int boneIndex = 0; boneIndex < boneCount; boneIndex++)
				{
					boneFlag = theInputFileReader.ReadByte();
					aSectionOfAnimation.theBoneFlags.Add(boneFlag);

					//DEBUG:
					if ((boneFlag & 0x20) > 0)
					{
						int unknownFlagIsUsed = 4242;
					}
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAniFrameAnim.theBoneFlags " + aSectionOfAnimation.theBoneFlags.Count.ToString());
				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "anAniFrameAnim.theBoneFlags alignment");

				if (aSectionOfAnimation.constantsOffset != 0)
				{
					theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + aSectionOfAnimation.constantsOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSectionOfAnimation.theBoneConstantInfos = new List<BoneConstantInfo49>(boneCount);
					for (int boneIndex = 0; boneIndex < boneCount; boneIndex++)
					{
						aBoneConstantInfo = new BoneConstantInfo49();
						aSectionOfAnimation.theBoneConstantInfos.Add(aBoneConstantInfo);

						boneFlag = aSectionOfAnimation.theBoneFlags[boneIndex];
						if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_CONST_ROT2) > 0)
						{
							aBoneConstantInfo.theConstantRotationUnknown = new SourceQuaternion48bitsViaBytes();
							aBoneConstantInfo.theConstantRotationUnknown.theBytes = theInputFileReader.ReadBytes(6);
						}
						if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_RAWROT) > 0)
						{
							aBoneConstantInfo.theConstantRawRot = new SourceQuaternion48bits();
							aBoneConstantInfo.theConstantRawRot.theXInput = theInputFileReader.ReadUInt16();
							aBoneConstantInfo.theConstantRawRot.theYInput = theInputFileReader.ReadUInt16();
							aBoneConstantInfo.theConstantRawRot.theZWInput = theInputFileReader.ReadUInt16();
						}
						if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_RAWPOS) > 0)
						{
							aBoneConstantInfo.theConstantRawPos = new SourceVector48bits();
							aBoneConstantInfo.theConstantRawPos.theXInput.the16BitValue = theInputFileReader.ReadUInt16();
							aBoneConstantInfo.theConstantRawPos.theYInput.the16BitValue = theInputFileReader.ReadUInt16();
							aBoneConstantInfo.theConstantRawPos.theZInput.the16BitValue = theInputFileReader.ReadUInt16();
						}
						//If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_CONST_ROT2) > 0 Then
						//	aBoneConstantInfo.theConstantRotationUnknown = New SourceQuaternion48bitsViaBytes()
						//	aBoneConstantInfo.theConstantRotationUnknown.theBytes = Me.theInputFileReader.ReadBytes(6)
						//End If
					}

					if (theInputFileReader.BaseStream.Position > fileOffsetStart)
					{
						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSectionOfAnimation.theBoneConstantInfos " + aSectionOfAnimation.theBoneConstantInfos.Count.ToString());
						theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aSectionOfAnimation.theBoneConstantInfos alignment");
					}
				}

				if (aSectionOfAnimation.frameLength > 0 && aSectionOfAnimation.frameOffset != 0)
				{
					theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + aSectionOfAnimation.frameOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aSectionOfAnimation.theBoneFrameDataInfos = new List<List<BoneFrameDataInfo49>>(sectionFrameCount);

					//NOTE: This adjustment is weird, but it fits all the data I've seen.
					int adjustedFrameCount = 0;
					if (lastSectionIsBeingRead)
					{
						adjustedFrameCount = sectionFrameCount;
					}
					else
					{
						adjustedFrameCount = sectionFrameCount + 1;
					}

					for (int frameIndex = 0; frameIndex < adjustedFrameCount; frameIndex++)
					{
						aBoneFrameDataInfoList = new List<BoneFrameDataInfo49>(boneCount);
						if (lastSectionIsBeingRead || (frameIndex < (adjustedFrameCount - 1)))
						{
							aSectionOfAnimation.theBoneFrameDataInfos.Add(aBoneFrameDataInfoList);
						}

						boneFrameDataStartInputFileStreamPosition = theInputFileReader.BaseStream.Position;

						for (int boneIndex = 0; boneIndex < boneCount; boneIndex++)
						{
							aBoneFrameDataInfo = new BoneFrameDataInfo49();
							aBoneFrameDataInfoList.Add(aBoneFrameDataInfo);

							boneFlag = aSectionOfAnimation.theBoneFlags[boneIndex];

							if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIM_ROT2) > 0)
							{
								aBoneFrameDataInfo.theAnimRotationUnknown = new SourceQuaternion48bitsViaBytes();
								aBoneFrameDataInfo.theAnimRotationUnknown.theBytes = theInputFileReader.ReadBytes(6);
							}
							if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIMROT) > 0)
							{
								aBoneFrameDataInfo.theAnimRotation = new SourceQuaternion48bits();
								aBoneFrameDataInfo.theAnimRotation.theXInput = theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimRotation.theYInput = theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimRotation.theZWInput = theInputFileReader.ReadUInt16();
							}
							if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIMPOS) > 0)
							{
								aBoneFrameDataInfo.theAnimPosition = new SourceVector48bits();
								aBoneFrameDataInfo.theAnimPosition.theXInput.the16BitValue = theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimPosition.theYInput.the16BitValue = theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimPosition.theZInput.the16BitValue = theInputFileReader.ReadUInt16();
							}
							if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_FULLANIMPOS) > 0)
							{
								aBoneFrameDataInfo.theFullAnimPosition = new SourceVector();
								aBoneFrameDataInfo.theFullAnimPosition.x = theInputFileReader.ReadSingle();
								aBoneFrameDataInfo.theFullAnimPosition.y = theInputFileReader.ReadSingle();
								aBoneFrameDataInfo.theFullAnimPosition.z = theInputFileReader.ReadSingle();
							}
							//If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIM_ROT2) > 0 Then
							//	aBoneFrameDataInfo.theAnimRotationUnknown = New SourceQuaternion48bitsViaBytes()
							//	aBoneFrameDataInfo.theAnimRotationUnknown.theBytes = Me.theInputFileReader.ReadBytes(6)
							//End If
						}

						//DEBUG: Check frame data length for debugging.
						if ((aSectionOfAnimation.frameLength) != (theInputFileReader.BaseStream.Position - boneFrameDataStartInputFileStreamPosition))
						{
							int somethingIsWrong = 4242;
						}
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					string text = "aSectionOfAnimation.theBoneFrameDataInfos " + aSectionOfAnimation.theBoneFrameDataInfos.Count.ToString();
					if (!lastSectionIsBeingRead)
					{
						text += " plus an extra unused aBoneFrameDataInfo";
					}
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, text);
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aSectionOfAnimation.theBoneFrameDataInfos alignment");
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		protected void ReadMdlAnimation(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc, int sectionFrameCount, int sectionIndex, bool lastSectionIsBeingRead)
		{
			long animationInputFileStreamPosition = 0;
			long nextAnimationInputFileStreamPosition = 0;
			long animValuePointerInputFileStreamPosition = 0;
			long rotValuePointerInputFileStreamPosition = 0;
			long posValuePointerInputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			SourceMdlAnimation anAnimation = null;
			int boneCount = 0;
			byte boneIndex = 0;

			theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin);

			List<SourceMdlAnimation> aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations[sectionIndex];

			if (theMdlFileData.theBones == null)
			{
				boneCount = 1;
			}
			else
			{
				boneCount = theMdlFileData.theBones.Count;
			}
			for (int j = 0; j < boneCount; j++)
			{
				animationInputFileStreamPosition = theInputFileReader.BaseStream.Position;

				boneIndex = theInputFileReader.ReadByte();
				if (boneIndex == 255)
				{
					theInputFileReader.ReadByte();
					theInputFileReader.ReadInt16();

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(animationInputFileStreamPosition, fileOffsetEnd, "anAnimationDesc.anAnimation [" + anAnimationDesc.theName + "] (boneIndex = 255)");

					break;
				}
				//DEBUG:
				if (boneIndex >= boneCount)
				{
					// L4D2 "left4dead2\ghostanim.mdl" reaches here.
					int badIfGetsHere = 42;
					break;
				}

				anAnimation = new SourceMdlAnimation();
				aSectionOfAnimation.Add(anAnimation);

				anAnimation.boneIndex = boneIndex;
				anAnimation.flags = theInputFileReader.ReadByte();
				anAnimation.nextSourceMdlAnimationOffset = theInputFileReader.ReadInt16();

				//DEBUG:
				if ((anAnimation.flags & 0x40) > 0)
				{
					int badIfGetsHere = 42;
				}
				if ((anAnimation.flags & 0x80) > 0)
				{
					int badIfGetsHere = 42;
				}

				//If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				//End If
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0)
				{
					anAnimation.theRot64bits = new SourceQuaternion64bits();
					anAnimation.theRot64bits.theBytes = theInputFileReader.ReadBytes(8);

					//Me.DebugQuaternion(anAnimation.theRot64)
				}
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0)
				{
					anAnimation.theRot48bits = new SourceQuaternion48bits();
					anAnimation.theRot48bits.theXInput = theInputFileReader.ReadUInt16();
					anAnimation.theRot48bits.theYInput = theInputFileReader.ReadUInt16();
					anAnimation.theRot48bits.theZWInput = theInputFileReader.ReadUInt16();
				}
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0)
				{
					anAnimation.thePos = new SourceVector48bits();
					anAnimation.thePos.theXInput.the16BitValue = theInputFileReader.ReadUInt16();
					anAnimation.thePos.theYInput.the16BitValue = theInputFileReader.ReadUInt16();
					anAnimation.thePos.theZInput.the16BitValue = theInputFileReader.ReadUInt16();
				}

				animValuePointerInputFileStreamPosition = theInputFileReader.BaseStream.Position;

				// First, read both sets of offsets.
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0)
				{
					rotValuePointerInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					anAnimation.theRotV = new SourceMdlAnimationValuePointer();

					anAnimation.theRotV.animXValueOffset = theInputFileReader.ReadInt16();
					if (anAnimation.theRotV.theAnimXValues == null)
					{
						anAnimation.theRotV.theAnimXValues = new List<SourceMdlAnimationValue>();
					}

					anAnimation.theRotV.animYValueOffset = theInputFileReader.ReadInt16();
					if (anAnimation.theRotV.theAnimYValues == null)
					{
						anAnimation.theRotV.theAnimYValues = new List<SourceMdlAnimationValue>();
					}

					anAnimation.theRotV.animZValueOffset = theInputFileReader.ReadInt16();
					if (anAnimation.theRotV.theAnimZValues == null)
					{
						anAnimation.theRotV.theAnimZValues = new List<SourceMdlAnimationValue>();
					}
				}
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0)
				{
					posValuePointerInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					anAnimation.thePosV = new SourceMdlAnimationValuePointer();

					anAnimation.thePosV.animXValueOffset = theInputFileReader.ReadInt16();
					if (anAnimation.thePosV.theAnimXValues == null)
					{
						anAnimation.thePosV.theAnimXValues = new List<SourceMdlAnimationValue>();
					}

					anAnimation.thePosV.animYValueOffset = theInputFileReader.ReadInt16();
					if (anAnimation.thePosV.theAnimYValues == null)
					{
						anAnimation.thePosV.theAnimYValues = new List<SourceMdlAnimationValue>();
					}

					anAnimation.thePosV.animZValueOffset = theInputFileReader.ReadInt16();
					if (anAnimation.thePosV.theAnimZValues == null)
					{
						anAnimation.thePosV.theAnimZValues = new List<SourceMdlAnimationValue>();
					}
				}

				theMdlFileData.theFileSeekLog.Add(animationInputFileStreamPosition, theInputFileReader.BaseStream.Position - 1, "anAnimationDesc.anAnimation  [" + anAnimationDesc.theName + "] (frameCount = " + anAnimationDesc.frameCount.ToString() + "; sectionFrameCount = " + sectionFrameCount.ToString() + ")");

				// Second, read the anim values using the offsets.
				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0)
				{
					if (anAnimation.theRotV.animXValueOffset > 0)
					{
						ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animXValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimXValues, "anAnimation.theRotV.theAnimXValues");
					}
					if (anAnimation.theRotV.animYValueOffset > 0)
					{
						ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animYValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimYValues, "anAnimation.theRotV.theAnimYValues");
					}
					if (anAnimation.theRotV.animZValueOffset > 0)
					{
						ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animZValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimZValues, "anAnimation.theRotV.theAnimZValues");
					}
				}
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0)
				{
					if (anAnimation.thePosV.animXValueOffset > 0)
					{
						ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animXValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimXValues, "anAnimation.thePosV.theAnimXValues");
					}
					if (anAnimation.thePosV.animYValueOffset > 0)
					{
						ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animYValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimYValues, "anAnimation.thePosV.theAnimYValues");
					}
					if (anAnimation.thePosV.animZValueOffset > 0)
					{
						ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animZValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimZValues, "anAnimation.thePosV.theAnimZValues");
					}
				}
				//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				//NOTE: If the offset is 0 then there are no more bone animation structures, so end the loop.
				if (anAnimation.nextSourceMdlAnimationOffset == 0)
				{
					//j = boneCount
					//lastFullAnimDataWasFound = True
					break;
				}
				else
				{
					// Skip to next anim, just in case not all data is being read in.
					nextAnimationInputFileStreamPosition = animationInputFileStreamPosition + anAnimation.nextSourceMdlAnimationOffset;
					//'TEST: Use this with ANI file, to see if it extracts better.
					//nextAnimationInputFileStreamPosition = animationInputFileStreamPosition + CType(anAnimation.nextSourceMdlAnimationOffset, UShort)
					if (nextAnimationInputFileStreamPosition < theInputFileReader.BaseStream.Position)
					{
						//PROBLEM! Should not be going backwards in file.
						int i = 42;
						break;
					}
					else if (nextAnimationInputFileStreamPosition > theInputFileReader.BaseStream.Position)
					{
						//PROBLEM! Should not be skipping ahead. Crowbar has skipped some data, but continue decompiling.
						int i = 42;
					}

					theInputFileReader.BaseStream.Seek(nextAnimationInputFileStreamPosition, SeekOrigin.Begin);
				}
			}

			if (boneIndex != 255)
			{
				//NOTE: There is always an unused empty data structure at the end of the list.
				//prevanim					= destanim;
				//destanim->nextoffset		= pData - (byte *)destanim;
				//destanim					= (mstudioanim_t *)pData;
				//pData						+= sizeof( *destanim );
				fileOffsetStart = theInputFileReader.BaseStream.Position;
				theInputFileReader.ReadByte();
				theInputFileReader.ReadByte();
				theInputFileReader.ReadInt16();

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.anAnimation [" + anAnimationDesc.theName + "] (unused empty data structure at the end of the list)");
			}

			//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.anAnimation [" + anAnimationDesc.theName + "] alignment")
		}

		//==========================================================
		//FROM: SourceEngine2007_source\utils\studiomdl\simplify.cpp
		//      Section within: static void CompressAnimations( ).
		//      This shows how the data is stored before being written to file.
		//memset( data, 0, sizeof( data ) ); 
		//pcount = data; 
		//pvalue = pcount + 1;
		//
		//pcount->num.valid = 1;
		//pcount->num.total = 1;
		//pvalue->value = value[0];
		//pvalue++;
		//
		//// build a RLE of deltas from the default pose
		//for (m = 1; m < n; m++)
		//{
		//	if (pcount->num.total == 255)
		//	{
		//		// chain too long, force a new entry
		//		pcount = pvalue;
		//		pvalue = pcount + 1;
		//		pcount->num.valid++;
		//		pvalue->value = value[m];
		//		pvalue++;
		//	} 
		//	// insert value if they're not equal, 
		//	// or if we're not on a run and the run is less than 3 units
		//	else if ((value[m] != value[m-1]) 
		//		|| ((pcount->num.total == pcount->num.valid) && ((m < n - 1) && value[m] != value[m+1])))
		//	{
		//		if (pcount->num.total != pcount->num.valid)
		//		{
		//			//if (j == 0) printf("%d:%d   ", pcount->num.valid, pcount->num.total ); 
		//			pcount = pvalue;
		//			pvalue = pcount + 1;
		//		}
		//		pcount->num.valid++;
		//		pvalue->value = value[m];
		//		pvalue++;
		//	}
		//	pcount->num.total++;
		//}
		////if (j == 0) printf("%d:%d\n", pcount->num.valid, pcount->num.total ); 
		//
		//panim->anim[w][j].num[k] = pvalue - data;
		//if (panim->anim[w][j].num[k] == 2 && value[0] == 0)
		//{
		//	panim->anim[w][j].num[k] = 0;
		//}
		//else
		//{
		//	panim->anim[w][j].data[k] = (mstudioanimvalue_t *)kalloc( pvalue - data, sizeof( mstudioanimvalue_t ) );
		//	memmove( panim->anim[w][j].data[k], data, (pvalue - data) * sizeof( mstudioanimvalue_t ) );
		//}

		//=======================================================
		//FROM: SourceEngine2007_source\utils\studiomdl\write.cpp
		//      Section within: void WriteAnimationData( s_animation_t *srcanim, mstudioanimdesc_t *destanimdesc, byte *&pLocalData, byte *&pExtData ).
		//      This shows how the data is written to file.
		//mstudioanim_valueptr_t *posvptr	= NULL;
		//mstudioanim_valueptr_t *rotvptr	= NULL;
		//
		//// allocate room for rotation ptrs
		//rotvptr	= (mstudioanim_valueptr_t *)pData;
		//pData += sizeof( *rotvptr );
		//
		//// skip all position info if there's no animation
		//if (psrcdata->num[0] != 0 || psrcdata->num[1] != 0 || psrcdata->num[2] != 0)
		//{
		//	posvptr	= (mstudioanim_valueptr_t *)pData;
		//	pData += sizeof( *posvptr );
		//}
		//
		//mstudioanimvalue_t	*destanimvalue = (mstudioanimvalue_t *)pData;
		//
		//if (rotvptr)
		//{
		//	// store rotation animations
		//	for (k = 3; k < 6; k++)
		//	{
		//		if (psrcdata->num[k] == 0)
		//		{
		//			rotvptr->offset[k-3] = 0;
		//		}
		//		else
		//		{
		//			rotvptr->offset[k-3] = ((byte *)destanimvalue - (byte *)rotvptr);
		//			for (n = 0; n < psrcdata->num[k]; n++)
		//			{
		//				destanimvalue->value = psrcdata->data[k][n].value;
		//				destanimvalue++;
		//			}
		//		}
		//	}
		//	destanim->flags |= STUDIO_ANIM_ANIMROT;
		//}
		//
		//if (posvptr)
		//{
		//	// store position animations
		//	for (k = 0; k < 3; k++)
		//	{
		//		if (psrcdata->num[k] == 0)
		//		{
		//			posvptr->offset[k] = 0;
		//		}
		//		else
		//		{
		//			posvptr->offset[k] = ((byte *)destanimvalue - (byte *)posvptr);
		//			for (n = 0; n < psrcdata->num[k]; n++)
		//			{
		//				destanimvalue->value = psrcdata->data[k][n].value;
		//				destanimvalue++;
		//			}
		//		}
		//	}
		//	destanim->flags |= STUDIO_ANIM_ANIMPOS;
		//}
		//rawanimbytes += ((byte *)destanimvalue - pData);
		//pData = (byte *)destanimvalue;

		//===================================================
		//FROM: SourceEngine2007_source\public\bone_setup.cpp
		//      The ExtractAnimValue function shows how the values are extracted per frame from the data in the mdl file.
		//void ExtractAnimValue( int frame, mstudioanimvalue_t *panimvalue, float scale, float &v1 )
		//{
		//	if ( !panimvalue )
		//	{
		//		v1 = 0;
		//		return;
		//	}

		//	int k = frame;

		//	while (panimvalue->num.total <= k)
		//	{
		//		k -= panimvalue->num.total;
		//		panimvalue += panimvalue->num.valid + 1;
		//		if ( panimvalue->num.total == 0 )
		//		{
		//			Assert( 0 ); // running off the end of the animation stream is bad
		//			v1 = 0;
		//			return;
		//		}
		//	}
		//	if (panimvalue->num.valid > k)
		//	{
		//		v1 = panimvalue[k+1].value * scale;
		//	}
		//	else
		//	{
		//		// get last valid data block
		//		v1 = panimvalue[panimvalue->num.valid].value * scale;
		//	}
		//}
		private void ReadMdlAnimValues(long animValuesInputFileStreamPosition, int frameCount, bool lastSectionIsBeingRead, List<SourceMdlAnimationValue> theAnimValues, string debugDescription)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			int frameCountRemainingToBeChecked = 0;
			SourceMdlAnimationValue animValue = new SourceMdlAnimationValue();
			byte currentTotal = 0;
			byte validCount = 0;
			int accumulatedTotal = 0;

			theInputFileReader.BaseStream.Seek(animValuesInputFileStreamPosition, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			frameCountRemainingToBeChecked = frameCount;
			accumulatedTotal = 0;
			while (frameCountRemainingToBeChecked > 0)
			{
				animValue.value = theInputFileReader.ReadInt16();
				currentTotal = animValue.total;
				accumulatedTotal += currentTotal;
				if (currentTotal == 0)
				{
					int badIfThisIsReached = 42;
					break;
				}
				frameCountRemainingToBeChecked -= currentTotal;
				theAnimValues.Add(animValue);

				validCount = animValue.valid;
				for (int i = 1; i <= validCount; i++)
				{
					animValue.value = theInputFileReader.ReadInt16();
					theAnimValues.Add(animValue);
				}
			}
			if (!lastSectionIsBeingRead && accumulatedTotal == frameCount)
			{
				theInputFileReader.ReadInt16();
				theInputFileReader.ReadInt16();
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, debugDescription + " (accumulatedTotal = " + accumulatedTotal.ToString() + ")");
		}

		//Private Sub DebugQuaternion(ByVal q As SourceQuaternion64)
		//	Dim sqx As Double = q.X * q.X
		//	Dim sqy As Double = q.Y * q.Y
		//	Dim sqz As Double = q.Z * q.Z
		//	Dim sqw As Double = q.W * q.W

		//	' If quaternion is normalised the unit is one, otherwise it is the correction factor
		//	Dim unit As Double = sqx + sqy + sqz + sqw
		//	If unit = 1 Then
		//		Dim i As Integer = 42
		//	ElseIf unit = -1 Then
		//		Dim i As Integer = 42
		//	Else
		//		Dim i As Integer = 42
		//	End If

		//End Sub

		protected long ReadMdlIkRules(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc)
		{
			if (anAnimationDesc.ikRuleCount > 0)
			{
				long ikRuleInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				if (theMdlFileData.version >= 48 && anAnimationDesc.animBlock > 0 && anAnimationDesc.animblockIkRuleOffset == 0)
				{
					//Return 0
				}
				else
				{
					theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin);
				}
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				anAnimationDesc.theIkRules = new List<SourceMdlIkRule>(anAnimationDesc.ikRuleCount);
				for (int ikRuleIndex = 0; ikRuleIndex < anAnimationDesc.ikRuleCount; ikRuleIndex++)
				{
					ikRuleInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlIkRule anIkRule = new SourceMdlIkRule();

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

					anIkRule.compressedIkErrorOffset = theInputFileReader.ReadInt32();
					anIkRule.unused2 = theInputFileReader.ReadInt32();
					anIkRule.ikErrorIndexStart = theInputFileReader.ReadInt32();
					anIkRule.ikErrorOffset = theInputFileReader.ReadInt32();

					anIkRule.influenceStart = theInputFileReader.ReadSingle();
					anIkRule.influencePeak = theInputFileReader.ReadSingle();
					anIkRule.influenceTail = theInputFileReader.ReadSingle();
					anIkRule.influenceEnd = theInputFileReader.ReadSingle();

					anIkRule.unused3 = theInputFileReader.ReadSingle();
					anIkRule.contact = theInputFileReader.ReadSingle();
					anIkRule.drop = theInputFileReader.ReadSingle();
					anIkRule.top = theInputFileReader.ReadSingle();

					anIkRule.unused6 = theInputFileReader.ReadInt32();
					anIkRule.unused7 = theInputFileReader.ReadInt32();
					anIkRule.unused8 = theInputFileReader.ReadInt32();

					anIkRule.attachmentNameOffset = theInputFileReader.ReadInt32();

					for (int x = 0; x < anIkRule.unused.Length; x++)
					{
						anIkRule.unused[x] = theInputFileReader.ReadInt32();
					}

					anAnimationDesc.theIkRules.Add(anIkRule);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (anIkRule.attachmentNameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.attachmentNameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						anIkRule.theAttachmentName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkRule.theAttachmentName = " + anIkRule.theAttachmentName);
						//End If
					}
					else
					{
						anIkRule.theAttachmentName = "";
					}

					if (anIkRule.compressedIkErrorOffset != 0)
					{
						long compressedIkErrorsEndOffset = ReadCompressedIkErrors(ikRuleInputFileStreamPosition, ikRuleIndex, anAnimationDesc);


						//If fileOffsetEndOfIkRuleExtraData < compressedIkErrorsEndOffset Then
						//	fileOffsetEndOfIkRuleExtraData = compressedIkErrorsEndOffset
						//End If
					}

					if (anIkRule.ikErrorOffset != 0)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				string description = "anAnimationDesc.theIkRules " + anAnimationDesc.theIkRules.Count.ToString();
				if (anAnimationDesc.animBlock > 0 && anAnimationDesc.animblockIkRuleOffset == 0)
				{
					description += "   [animblockIkRuleOffset = 0]";
				}
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, description);

				//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment")

				//	Return Me.theInputFileReader.BaseStream.Position - 1
			}

			return 0;
		}

		private long ReadCompressedIkErrors(long ikRuleInputFileStreamPosition, int ikRuleIndex, SourceMdlAnimationDesc49 anAnimationDesc)
		{
			SourceMdlIkRule anIkRule = anAnimationDesc.theIkRules[ikRuleIndex];

			long compressedIkErrorInputFileStreamPosition = 0;
			long kInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.compressedIkErrorOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;
			compressedIkErrorInputFileStreamPosition = theInputFileReader.BaseStream.Position;

			anIkRule.theCompressedIkError = new SourceMdlCompressedIkError();

			// First, read the scale data.
			for (int k = 0; k < anIkRule.theCompressedIkError.scale.Length; k++)
			{
				kInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				anIkRule.theCompressedIkError.scale[k] = theInputFileReader.ReadSingle();

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(kInputFileStreamPosition, fileOffsetEnd, "anIkRule.theCompressedIkError [ikRuleIndex = " + ikRuleIndex.ToString() + "] [scale = " + anIkRule.theCompressedIkError.scale[k].ToString() + "]");
			}

			// Second, read the offset data.
			for (int k = 0; k < anIkRule.theCompressedIkError.offset.Length; k++)
			{
				kInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				anIkRule.theCompressedIkError.offset[k] = theInputFileReader.ReadInt16();

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(kInputFileStreamPosition, fileOffsetEnd, "anIkRule.theCompressedIkError [ikRuleIndex = " + ikRuleIndex.ToString() + "] [offset = " + anIkRule.theCompressedIkError.offset[k].ToString() + "]");
			}

			//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkRule.theCompressedIkError (scale and offset data)")

			//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			//TODO:  Third, read the anim values. This code does not correctly handle versions below 48.
			if (theMdlFileData.version >= 48)
			{
				for (int k = 0; k < anIkRule.theCompressedIkError.scale.Length; k++)
				{
					//compressedIkErrorInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					// Read the mstudioanimvalue_t.
					//int size = srcanim->ikrule[j].errorData.numanim[k] * sizeof( mstudioanimvalue_t );
					//memmove( pData, srcanim->ikrule[j].errorData.anim[k], size );
					//TODO: Figure out what frameCount should be.
					if (anIkRule.theCompressedIkError.offset[k] > 0)
					{
						//Dim frameCount As Integer = 1
						anIkRule.theCompressedIkError.theAnimValues[k] = new List<SourceMdlAnimationValue>();
						ReadMdlAnimValues(compressedIkErrorInputFileStreamPosition + anIkRule.theCompressedIkError.offset[k], anAnimationDesc.frameCount, true, anIkRule.theCompressedIkError.theAnimValues[k], "anIkRule.theCompressedIkError.theAnimValues(" + k.ToString() + ")");
					}
				}
			}

			//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

			//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkRule.theCompressedIkError ")

			return theInputFileReader.BaseStream.Position - 1;
		}

		protected void ReadMdlAnimationSection(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc, FileSeekLog aFileSeekLog)
		{
			//Dim animSectionInputFileStreamPosition As Long
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			try
			{
				fileOffsetStart = animInputFileStreamPosition;

				//animSectionInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				SourceMdlAnimationSection anAnimSection = new SourceMdlAnimationSection();
				anAnimSection.animBlock = theInputFileReader.ReadInt32();
				anAnimSection.animOffset = theInputFileReader.ReadInt32();
				anAnimationDesc.theSections.Add(anAnimSection);

				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				//aFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimSection [" + anAnimationDesc.theName + "] [animBlock = " + anAnimSection.animBlock.ToString() + "] [calculated ANI file Offset = " + (Me.theMdlFileData.theAnimBlocks(anAnimSection.animBlock).dataStart + anAnimSection.animOffset).ToString() + "]")
				if (theMdlFileData.theAnimBlocks == null)
				{
					aFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimSection [" + anAnimationDesc.theName + "] [animBlock = " + anAnimSection.animBlock.ToString() + "]");
				}
				else
				{
					int animBlock = 0;
					string description = null;
					animBlock = anAnimSection.animBlock;
					if (animBlock == 0)
					{
						description = "MDL";
					}
					else
					{
						description = "ANI";
					}
					aFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimSection [" + anAnimationDesc.theName + "] [animBlock = " + animBlock.ToString() + "] [calculated " + description + " file Offset = " + (theMdlFileData.theAnimBlocks[animBlock].dataStart + anAnimSection.animOffset).ToString() + "]");
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		protected void ReadMdlMovements(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc)
		{
			if (anAnimationDesc.movementCount > 0)
			{
				long movementInputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

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
			}
		}

		protected long ReadLocalHierarchies(long animInputFileStreamPosition, SourceMdlAnimationDesc49 anAnimationDesc)
		{
			if (anAnimationDesc.localHierarchyCount > 0)
			{
				long localHieararchyInputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.localHierarchyOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				anAnimationDesc.theLocalHierarchies = new List<SourceMdlLocalHierarchy>(anAnimationDesc.localHierarchyCount);
				for (int j = 0; j < anAnimationDesc.localHierarchyCount; j++)
				{
					localHieararchyInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlLocalHierarchy aLocalHierarchy = new SourceMdlLocalHierarchy();

					aLocalHierarchy.boneIndex = theInputFileReader.ReadInt32();
					aLocalHierarchy.boneNewParentIndex = theInputFileReader.ReadInt32();
					aLocalHierarchy.startInfluence = theInputFileReader.ReadSingle();
					aLocalHierarchy.peakInfluence = theInputFileReader.ReadSingle();
					aLocalHierarchy.tailInfluence = theInputFileReader.ReadSingle();
					aLocalHierarchy.endInfluence = theInputFileReader.ReadSingle();
					aLocalHierarchy.startFrameIndex = theInputFileReader.ReadInt32();
					aLocalHierarchy.localAnimOffset = theInputFileReader.ReadInt32();
					for (int x = 0; x < aLocalHierarchy.unused.Length; x++)
					{
						aLocalHierarchy.unused[x] = theInputFileReader.ReadInt32();
					}

					anAnimationDesc.theLocalHierarchies.Add(aLocalHierarchy);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theLocalHierarchies " + anAnimationDesc.theLocalHierarchies.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theLocalHierarchies alignment");

				return theInputFileReader.BaseStream.Position - 1;
			}
//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return 0;
		}

		public void ReadSequenceDescs()
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
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theSequenceDescs = new List<SourceMdlSequenceDesc>(theMdlFileData.localSequenceCount);
					for (int i = 0; i < theMdlFileData.localSequenceCount; i++)
					{
						seqInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlSequenceDesc aSeqDesc = new SourceMdlSequenceDesc();

						aSeqDesc.baseHeaderOffset = theInputFileReader.ReadInt32();
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
						aSeqDesc.animIndexOffset = theInputFileReader.ReadInt32();
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

						aSeqDesc.fadeInTime = theInputFileReader.ReadSingle();
						aSeqDesc.fadeOutTime = theInputFileReader.ReadSingle();

						aSeqDesc.localEntryNodeIndex = theInputFileReader.ReadInt32();
						aSeqDesc.localExitNodeIndex = theInputFileReader.ReadInt32();
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
						aSeqDesc.cyclePoseIndex = theInputFileReader.ReadInt32();

						aSeqDesc.activityModifierOffset = 0;
						aSeqDesc.activityModifierCount = 0;
						if (theMdlFileData.version == 48 || theMdlFileData.version == 49)
						{
							aSeqDesc.activityModifierOffset = theInputFileReader.ReadInt32();
							aSeqDesc.activityModifierCount = theInputFileReader.ReadInt32();
							for (int x = 0; x <= 4; x++)
							{
								aSeqDesc.unused[x] = theInputFileReader.ReadInt32();
							}
						}
						else
						{
							for (int x = 0; x <= 6; x++)
							{
								aSeqDesc.unused[x] = theInputFileReader.ReadInt32();
							}
						}

						theMdlFileData.theSequenceDescs.Add(aSeqDesc);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (aSeqDesc.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							aSeqDesc.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

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
							//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName = " + aSeqDesc.theActivityName);
							//End If
						}
						else
						{
							aSeqDesc.theActivityName = "";
						}
						if ((aSeqDesc.groupSize[0] > 1 || aSeqDesc.groupSize[1] > 1) && aSeqDesc.poseKeyOffset != 0)
						{
							ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.eventCount > 0 && aSeqDesc.eventOffset != 0)
						{
							ReadEvents(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.autoLayerCount > 0 && aSeqDesc.autoLayerOffset != 0)
						{
							ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc);
						}
						if (theMdlFileData.boneCount > 0 && aSeqDesc.weightOffset > 0)
						{
							ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.ikLockCount > 0 && aSeqDesc.ikLockOffset != 0)
						{
							ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc);
						}
						if ((aSeqDesc.groupSize[0] * aSeqDesc.groupSize[1]) > 0 && aSeqDesc.animIndexOffset != 0)
						{
							ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.keyValueSize > 0 && aSeqDesc.keyValueOffset != 0)
						{
							ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.activityModifierCount != 0 && aSeqDesc.activityModifierOffset != 0)
						{
							ReadActivityModifiers(seqInputFileStreamPosition, aSeqDesc);
						}

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs " + theMdlFileData.theSequenceDescs.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadPoseKeys(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
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
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.thePoseKeys [" + aSeqDesc.theName + "] " + aSeqDesc.thePoseKeys.Count.ToString());
		}

		private void ReadEvents(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			int eventCount = aSeqDesc.eventCount;
			long eventInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			long fileOffsetStart2 = 0;
			long fileOffsetEnd2 = 0;

			theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			aSeqDesc.theEvents = new List<SourceMdlEvent>(eventCount);
			for (int j = 0; j < eventCount; j++)
			{
				eventInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				SourceMdlEvent anEvent = new SourceMdlEvent();
				anEvent.cycle = theInputFileReader.ReadSingle();
				anEvent.eventIndex = theInputFileReader.ReadInt32();
				anEvent.eventType = theInputFileReader.ReadInt32();
				for (int x = 0; x < anEvent.options.Length; x++)
				{
					anEvent.options[x] = theInputFileReader.ReadChar();
				}
				anEvent.nameOffset = theInputFileReader.ReadInt32();
				aSeqDesc.theEvents.Add(anEvent);

				inputFileStreamPosition = theInputFileReader.BaseStream.Position;

				//if ( isdigit( g_sequence[i].event[j].eventname[0] ) )
				//{
				//	 pevent[j].event = atoi( g_sequence[i].event[j].eventname );
				//	 pevent[j].type = 0;
				//	 pevent[j].szeventindex = 0;
				//}
				//Else
				//{
				//	 AddToStringTable( &pevent[j], &pevent[j].szeventindex, g_sequence[i].event[j].eventname );
				//	 pevent[j].type = NEW_EVENT_STYLE;
				//}
				if (anEvent.nameOffset != 0)
				{
					theInputFileReader.BaseStream.Seek(eventInputFileStreamPosition + anEvent.nameOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;

					anEvent.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEvent.theName = " + anEvent.theName);
					//End If
				}
				else
				{
					//anEvent.theName = ""
					anEvent.theName = anEvent.eventIndex.ToString();
				}

				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theEvents [" + aSeqDesc.theName + "] " + aSeqDesc.theEvents.Count.ToString());
		}

		private void ReadAutoLayers(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			int autoLayerCount = aSeqDesc.autoLayerCount;
			long autoLayerInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			aSeqDesc.theAutoLayers = new List<SourceMdlAutoLayer>(autoLayerCount);
			for (int j = 0; j < autoLayerCount; j++)
			{
				autoLayerInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				SourceMdlAutoLayer anAutoLayer = new SourceMdlAutoLayer();
				anAutoLayer.sequenceIndex = theInputFileReader.ReadInt16();
				anAutoLayer.poseIndex = theInputFileReader.ReadInt16();
				anAutoLayer.flags = theInputFileReader.ReadInt32();
				anAutoLayer.influenceStart = theInputFileReader.ReadSingle();
				anAutoLayer.influencePeak = theInputFileReader.ReadSingle();
				anAutoLayer.influenceTail = theInputFileReader.ReadSingle();
				anAutoLayer.influenceEnd = theInputFileReader.ReadSingle();
				aSeqDesc.theAutoLayers.Add(anAutoLayer);

				//NOTE: Change NaN to 0. This is needed for HL2DM\HL2\hl2_misc_dir.vpk\models\combine_soldier_anims.mdl for its "Man_Gun" $sequence.
				if (double.IsNaN(anAutoLayer.influenceStart))
				{
					anAutoLayer.influenceStart = 0;
				}
				if (double.IsNaN(anAutoLayer.influencePeak))
				{
					anAutoLayer.influencePeak = 0;
				}
				if (double.IsNaN(anAutoLayer.influenceTail))
				{
					anAutoLayer.influenceTail = 0;
				}
				if (double.IsNaN(anAutoLayer.influenceEnd))
				{
					anAutoLayer.influenceEnd = 0;
				}

				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAutoLayers [" + aSeqDesc.theName + "] " + aSeqDesc.theAutoLayers.Count.ToString());
		}

		private void ReadMdlAnimBoneWeights(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			long weightListInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.weightOffset, SeekOrigin.Begin);
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
			//NOTE: A sequence can point to same weights as another.
			//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theBoneWeights [" + aSeqDesc.theName + "] " + aSeqDesc.theBoneWeights.Count.ToString());
			//End If
		}

		private void ReadSequenceIkLocks(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			int lockCount = aSeqDesc.ikLockCount;
			long lockInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			aSeqDesc.theIkLocks = new List<SourceMdlIkLock>(lockCount);
			for (int j = 0; j < lockCount; j++)
			{
				lockInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				SourceMdlIkLock anIkLock = new SourceMdlIkLock();
				anIkLock.chainIndex = theInputFileReader.ReadInt32();
				anIkLock.posWeight = theInputFileReader.ReadSingle();
				anIkLock.localQWeight = theInputFileReader.ReadSingle();
				anIkLock.flags = theInputFileReader.ReadInt32();
				for (int x = 0; x < anIkLock.unused.Length; x++)
				{
					anIkLock.unused[x] = theInputFileReader.ReadInt32();
				}
				aSeqDesc.theIkLocks.Add(anIkLock);

				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theIkLocks [" + aSeqDesc.theName + "] " + aSeqDesc.theIkLocks.Count.ToString());
		}

		private void ReadMdlAnimIndexes(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			int animIndexCount = aSeqDesc.groupSize[0] * aSeqDesc.groupSize[1];
			long animIndexInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.animIndexOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			aSeqDesc.theAnimDescIndexes = new List<short>(animIndexCount);
			for (int j = 0; j < animIndexCount; j++)
			{
				animIndexInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				short anAnimIndex = theInputFileReader.ReadInt16();
				aSeqDesc.theAnimDescIndexes.Add(anAnimIndex);

				if (theMdlFileData.theAnimationDescs != null && theMdlFileData.theAnimationDescs.Count > anAnimIndex)
				{
					//NOTE: Set this boolean for use in writing lines in qc file.
					theMdlFileData.theAnimationDescs[anAnimIndex].theAnimIsLinkedToSequence = true;
					theMdlFileData.theAnimationDescs[anAnimIndex].theLinkedSequences.Add(aSeqDesc);
				}

				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			//TODO: A sequence can point to same anims as another?
			//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAnimDescIndexes [" + aSeqDesc.theName + "] " + aSeqDesc.theAnimDescIndexes.Count.ToString());
			//End If
		}

		private void ReadSequenceKeyValues(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			aSeqDesc.theKeyValues = FileManager.ReadNullTerminatedString(theInputFileReader);

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theKeyValues [" + aSeqDesc.theName + "] = " + aSeqDesc.theKeyValues);
		}

		private void ReadActivityModifiers(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			int activityModifierCount = 0;
			long activityModifierInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			long fileOffsetStart2 = 0;
			long fileOffsetEnd2 = 0;

			theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityModifierOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			activityModifierCount = aSeqDesc.activityModifierCount;
			aSeqDesc.theActivityModifiers = new List<SourceMdlActivityModifier>(activityModifierCount);
			for (int j = 0; j < activityModifierCount; j++)
			{
				activityModifierInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				SourceMdlActivityModifier anActivityModifier = new SourceMdlActivityModifier();
				anActivityModifier.nameOffset = theInputFileReader.ReadInt32();
				aSeqDesc.theActivityModifiers.Add(anActivityModifier);

				inputFileStreamPosition = theInputFileReader.BaseStream.Position;

				if (anActivityModifier.nameOffset != 0)
				{
					theInputFileReader.BaseStream.Seek(activityModifierInputFileStreamPosition + anActivityModifier.nameOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;

					anActivityModifier.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anActivityModifier.theName = " + anActivityModifier.theName);
					//End If
				}
				else
				{
					anActivityModifier.theName = "";
				}

				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theActivityModifiers [" + aSeqDesc.theName + "] " + aSeqDesc.theActivityModifiers.Count.ToString());
		}

		public void ReadLocalNodeNames()
		{
			//	// save transition graph
			//	int *pxnodename = (int *)pData;
			//	phdr->localnodenameindex = (pData - pStart);
			//	pData += g_numxnodes * sizeof( *pxnodename );
			//	ALIGN4( pData );
			//	for (i = 0; i < g_numxnodes; i++)
			//	{
			//		AddToStringTable( phdr, pxnodename, g_xnodename[i+1] );
			//		// printf("%d : %s\n", i, g_xnodename[i+1] );
			//		pxnodename++;
			//	}
			if (theMdlFileData.localNodeCount > 0 && theMdlFileData.localNodeNameOffset != 0)
			{
				long localNodeNameInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(theMdlFileData.localNodeNameOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theLocalNodeNames = new List<string>(theMdlFileData.localNodeCount);
				int localNodeNameOffset = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string aLocalNodeName = null;
				for (int i = 0; i < theMdlFileData.localNodeCount; i++)
				{
					localNodeNameInputFileStreamPosition = theInputFileReader.BaseStream.Position;
	//				Dim aLocalNodeName As String
					localNodeNameOffset = theInputFileReader.ReadInt32();

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (localNodeNameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(localNodeNameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aLocalNodeName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aLocalNodeName = " + aLocalNodeName);
						//End If
					}
					else
					{
						aLocalNodeName = "";
					}
					theMdlFileData.theLocalNodeNames.Add(aLocalNodeName);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLocalNodeNames " + theMdlFileData.theLocalNodeNames.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theLocalNodeNames alignment");
			}
		}

		public void ReadLocalNodes()
		{
			//	ptransition	= (byte *)pData;
			//	phdr->numlocalnodes = IsChar( g_numxnodes );
			//	phdr->localnodeindex = IsInt24( pData - pStart );
			//	pData += g_numxnodes * g_numxnodes * sizeof( byte );
			//	ALIGN4( pData );
			//	for (i = 0; i < g_numxnodes; i++)
			//	{
			////		printf("%2d (%12s) : ", i + 1, g_xnodename[i+1] );
			//		for (j = 0; j < g_numxnodes; j++)
			//		{
			//			*ptransition++ = g_xnode[i][j];
			////			printf(" %2d", g_xnode[i][j] );
			//		}
			////		printf("\n" );
			//	}
			if (theMdlFileData.localNodeCount > 0 && theMdlFileData.localNodeOffset != 0)
			{
				//Dim localNodeInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				theInputFileReader.BaseStream.Seek(theMdlFileData.localNodeOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theLocalNodes = new List<List<byte>>(theMdlFileData.localNodeCount);
				for (int i = 0; i < theMdlFileData.localNodeCount; i++)
				{
					List<byte> exitNodes = new List<byte>(theMdlFileData.localNodeCount);

					for (int j = 0; j < theMdlFileData.localNodeCount; j++)
					{
						byte nodeValue = theInputFileReader.ReadByte();
						exitNodes.Add(nodeValue);
					}

					theMdlFileData.theLocalNodes.Add(exitNodes);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLocalNodes " + theMdlFileData.theLocalNodes.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theLocalNodes alignment");
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

				theInputFileReader.BaseStream.Seek(theMdlFileData.bodyPartOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theBodyParts = new List<SourceMdlBodyPart>(theMdlFileData.bodyPartCount);
				for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.bodyPartCount; bodyPartIndex++)
				{
					bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlBodyPart aBodyPart = new SourceMdlBodyPart();
					aBodyPart.nameOffset = theInputFileReader.ReadInt32();
					aBodyPart.modelCount = theInputFileReader.ReadInt32();
					aBodyPart.@base = theInputFileReader.ReadInt32();
					aBodyPart.modelOffset = theInputFileReader.ReadInt32();
					theMdlFileData.theBodyParts.Add(aBodyPart);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aBodyPart.nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aBodyPart.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBodyPart.theName = " + aBodyPart.theName);
						//End If
					}
					else
					{
						aBodyPart.theName = "";
					}

					ReadModels(bodyPartInputFileStreamPosition, aBodyPart, bodyPartIndex);
					//NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts and models.
					if (bodyPartIndex == theMdlFileData.bodyPartCount - 1)
					{
						theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment");
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts " + theMdlFileData.theBodyParts.Count.ToString());
			}
		}

		private void ReadModels(long bodyPartInputFileStreamPosition, SourceMdlBodyPart aBodyPart, int bodyPartIndex)
		{
			if (aBodyPart.modelCount > 0)
			{
				long modelInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				string modelName = null;

				theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin);
				//fileOffsetStart = Me.theInputFileReader.BaseStream.Position

				aBodyPart.theModels = new List<SourceMdlModel>(aBodyPart.modelCount);
				for (int j = 0; j < aBodyPart.modelCount; j++)
				{
					modelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					SourceMdlModel aModel = new SourceMdlModel();

					aModel.name = theInputFileReader.ReadChars(64);
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
					SourceMdlModelVertexData modelVertexData = new SourceMdlModelVertexData();
					modelVertexData.vertexDataP = theInputFileReader.ReadInt32();
					modelVertexData.tangentDataP = theInputFileReader.ReadInt32();
					aModel.vertexData = modelVertexData;
					for (int x = 0; x <= 7; x++)
					{
						aModel.unused[x] = theInputFileReader.ReadInt32();
					}

					aBodyPart.theModels.Add(aModel);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					//NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
					ReadEyeballs(modelInputFileStreamPosition, aModel);
					ReadMeshes(modelInputFileStreamPosition, aModel);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);

					modelName = (new string(aModel.name)).Trim('\0');
					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel Name = " + modelName);
				}

				//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels " + aBodyPart.theModels.Count.ToString())
			}
		}

		private void ReadMeshes(long modelInputFileStreamPosition, SourceMdlModel aModel)
		{
			if (aModel.meshCount > 0 && aModel.meshOffset != 0)
			{
				aModel.theMeshes = new List<SourceMdlMesh>(aModel.meshCount);
				long meshInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
				{
					meshInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlMesh aMesh = new SourceMdlMesh();

					aMesh.materialIndex = theInputFileReader.ReadInt32();
					aMesh.modelOffset = theInputFileReader.ReadInt32();
					aMesh.vertexCount = theInputFileReader.ReadInt32();
					aMesh.vertexIndexStart = theInputFileReader.ReadInt32();
					aMesh.flexCount = theInputFileReader.ReadInt32();
					aMesh.flexOffset = theInputFileReader.ReadInt32();
					aMesh.materialType = theInputFileReader.ReadInt32();
					aMesh.materialParam = theInputFileReader.ReadInt32();
					aMesh.id = theInputFileReader.ReadInt32();
					aMesh.centerX = theInputFileReader.ReadSingle();
					aMesh.centerY = theInputFileReader.ReadSingle();
					aMesh.centerZ = theInputFileReader.ReadSingle();
					SourceMdlMeshVertexData meshVertexData = new SourceMdlMeshVertexData();
					meshVertexData.modelVertexDataP = theInputFileReader.ReadInt32();
					for (int x = 0; x < SourceConstants.MAX_NUM_LODS; x++)
					{
						meshVertexData.lodVertexCount[x] = theInputFileReader.ReadInt32();
					}
					aMesh.vertexData = meshVertexData;
					for (int x = 0; x <= 7; x++)
					{
						aMesh.unused[x] = theInputFileReader.ReadInt32();
					}

					aModel.theMeshes.Add(aMesh);

					// Fill-in eyeball texture index info.
					if (aMesh.materialType == 1)
					{
						aModel.theEyeballs[aMesh.materialParam].theTextureIndex = aMesh.materialIndex;
					}

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
		}

		private void ReadEyeballs(long modelInputFileStreamPosition, SourceMdlModel aModel)
		{
			if (aModel.eyeballCount > 0 && aModel.eyeballOffset != 0)
			{
				aModel.theEyeballs = new List<SourceMdlEyeball>(aModel.eyeballCount);
				long eyeballInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				for (int k = 0; k < aModel.eyeballCount; k++)
				{
					eyeballInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlEyeball anEyeball = new SourceMdlEyeball();

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

					anEyeball.unused1 = theInputFileReader.ReadInt32();
					anEyeball.irisScale = theInputFileReader.ReadSingle();
					anEyeball.unused2 = theInputFileReader.ReadInt32();

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

					for (int x = 0; x < anEyeball.unused.Length; x++)
					{
						anEyeball.unused[x] = theInputFileReader.ReadInt32();
					}

					anEyeball.eyeballIsNonFacs = theInputFileReader.ReadByte();

					for (int x = 0; x < anEyeball.unused3.Length; x++)
					{
						anEyeball.unused3[x] = theInputFileReader.ReadChar();
					}
					for (int x = 0; x < anEyeball.unused4.Length; x++)
					{
						anEyeball.unused4[x] = theInputFileReader.ReadInt32();
					}

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
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEyeball.theName = " + anEyeball.theName);
						//End If
					}
					else
					{
						anEyeball.theName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				if (aModel.theEyeballs.Count > 0)
				{
					// Detect if $upaxis Y was used.
					//FROM: [48] SourceEngine2007_source se2007_src\src_main\utils\studiomdl\studiomdl.cpp
					//      Option_Eyeball()
					//	AngleMatrix( g_defaultrotation, vtmp );
					//	VectorIRotate( Vector( 0, 0, 1 ), vtmp, tmp );
					//	VectorIRotate( tmp, pmodel->source->boneToPose[eyeball->bone], eyeball->up );
					SourceMdlEyeball anEyeball = null;
					SourceMdlBone aBone = null;
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
		}

		private void ReadFlexes(long meshInputFileStreamPosition, SourceMdlMesh aMesh)
		{
			aMesh.theFlexes = new List<SourceMdlFlex>(aMesh.flexCount);
			long flexInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			for (int k = 0; k < aMesh.flexCount; k++)
			{
				flexInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				SourceMdlFlex aFlex = new SourceMdlFlex();

				aFlex.flexDescIndex = theInputFileReader.ReadInt32();

				aFlex.target0 = theInputFileReader.ReadSingle();
				aFlex.target1 = theInputFileReader.ReadSingle();
				aFlex.target2 = theInputFileReader.ReadSingle();
				aFlex.target3 = theInputFileReader.ReadSingle();

				aFlex.vertCount = theInputFileReader.ReadInt32();
				aFlex.vertOffset = theInputFileReader.ReadInt32();

				aFlex.flexDescPartnerIndex = theInputFileReader.ReadInt32();
				aFlex.vertAnimType = theInputFileReader.ReadByte();
				for (int x = 0; x < aFlex.unusedChar.Length; x++)
				{
					aFlex.unusedChar[x] = theInputFileReader.ReadChar();
				}
				for (int x = 0; x < aFlex.unused.Length; x++)
				{
					aFlex.unused[x] = theInputFileReader.ReadInt32();
				}
				aMesh.theFlexes.Add(aFlex);

				//'NOTE: Set the frame index here because it is determined by order of flexes in mdl file.
				//'      Start the indexing at 1 because first frame (frame 0) is "basis" frame.
				//Me.theCurrentFrameIndex += 1
				//Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex = Me.theCurrentFrameIndex

				inputFileStreamPosition = theInputFileReader.BaseStream.Position;

				if (aFlex.vertCount > 0 && aFlex.vertOffset != 0)
				{
					ReadVertAnims(flexInputFileStreamPosition, aFlex);
				}

				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString());

			theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aMesh.theFlexes alignment");
		}

		private void ReadVertAnims(long flexInputFileStreamPosition, SourceMdlFlex aFlex)
		{
			aFlex.theVertAnims = new List<SourceMdlVertAnim>(aFlex.vertCount);
			long eyeballInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin);
			fileOffsetStart = theInputFileReader.BaseStream.Position;

			SourceMdlVertAnim aVertAnim = null;
			for (int k = 0; k < aFlex.vertCount; k++)
			{
				eyeballInputFileStreamPosition = theInputFileReader.BaseStream.Position;
				if (aFlex.vertAnimType == aFlex.STUDIO_VERT_ANIM_WRINKLE)
				{
					aVertAnim = new SourceMdlVertAnimWrinkle();
				}
				else
				{
					aVertAnim = new SourceMdlVertAnim();
				}

				aVertAnim.index = theInputFileReader.ReadUInt16();
				aVertAnim.speed = theInputFileReader.ReadByte();
				aVertAnim.side = theInputFileReader.ReadByte();

				for (int x = 0; x <= 2; x++)
				{
					aVertAnim.set_deltaUShort(x, theInputFileReader.ReadUInt16());
				}
				for (int x = 0; x <= 2; x++)
				{
					aVertAnim.set_nDeltaUShort(x, theInputFileReader.ReadUInt16());
				}

				if (aFlex.vertAnimType == aFlex.STUDIO_VERT_ANIM_WRINKLE)
				{
					((SourceMdlVertAnimWrinkle)aVertAnim).wrinkleDelta = theInputFileReader.ReadInt16();
				}

				aFlex.theVertAnims.Add(aVertAnim);

				inputFileStreamPosition = theInputFileReader.BaseStream.Position;

				theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			//aFlex.theVertAnims.Sort(AddressOf Me.SortVertAnims)

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString());

			theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment");
		}

		private int SortVertAnims(SourceMdlVertAnim x, SourceMdlVertAnim y)
		{
			return x.index.CompareTo(y.index);
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
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexDesc.theName = " + aFlexDesc.theName);
						//End If
					}
					else
					{
						aFlexDesc.theName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs " + theMdlFileData.theFlexDescs.Count.ToString());
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
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theType = " + aFlexController.theType);
						//End If
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
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theName = " + aFlexController.theName);
						//End If
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

				//If Me.theMdlFileData.theFlexRules.Count > 0 Then
				//	Me.theMdlFileData.theModelCommandIsUsed = True
				//End If

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + theMdlFileData.theFlexRules.Count.ToString());
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

				theInputFileReader.BaseStream.Seek(theMdlFileData.ikChainOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theIkChains = new List<SourceMdlIkChain>(theMdlFileData.ikChainCount);
				for (int i = 0; i < theMdlFileData.ikChainCount; i++)
				{
					ikChainInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlIkChain anIkChain = new SourceMdlIkChain();
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
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkChain.theName = " + anIkChain.theName);
						//End If
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
			}
		}

		private void ReadIkLinks(long ikChainInputFileStreamPosition, SourceMdlIkChain anIkChain)
		{
			if (anIkChain.linkCount > 0)
			{
				//Dim ikLinkInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				anIkChain.theLinks = new List<SourceMdlIkLink>(anIkChain.linkCount);
				for (int j = 0; j < anIkChain.linkCount; j++)
				{
					//ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlIkLink anIkLink = new SourceMdlIkLink();
					anIkLink.boneIndex = theInputFileReader.ReadInt32();
					anIkLink.idealBendingDirection.x = theInputFileReader.ReadSingle();
					anIkLink.idealBendingDirection.y = theInputFileReader.ReadSingle();
					anIkLink.idealBendingDirection.z = theInputFileReader.ReadSingle();
					anIkLink.unused0.x = theInputFileReader.ReadSingle();
					anIkLink.unused0.y = theInputFileReader.ReadSingle();
					anIkLink.unused0.z = theInputFileReader.ReadSingle();
					anIkChain.theLinks.Add(anIkLink);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks " + anIkChain.theLinks.Count.ToString());
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

				theInputFileReader.BaseStream.Seek(theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theIkLocks = new List<SourceMdlIkLock>(theMdlFileData.localIkAutoPlayLockCount);
				for (int i = 0; i < theMdlFileData.localIkAutoPlayLockCount; i++)
				{
					//ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlIkLock anIkLock = new SourceMdlIkLock();
					anIkLock.chainIndex = theInputFileReader.ReadInt32();
					anIkLock.posWeight = theInputFileReader.ReadSingle();
					anIkLock.localQWeight = theInputFileReader.ReadSingle();
					anIkLock.flags = theInputFileReader.ReadInt32();
					for (int x = 0; x < anIkLock.unused.Length; x++)
					{
						anIkLock.unused[x] = theInputFileReader.ReadInt32();
					}
					theMdlFileData.theIkLocks.Add(anIkLock);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks " + theMdlFileData.theIkLocks.Count.ToString());
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
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aPoseParamDesc.theName = " + aPoseParamDesc.theName);
						//End If
					}
					else
					{
						aPoseParamDesc.theName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs " + theMdlFileData.thePoseParamDescs.Count.ToString());
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

				theInputFileReader.BaseStream.Seek(theMdlFileData.textureOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theTextures = new List<SourceMdlTexture>(theMdlFileData.textureCount);
				for (int i = 0; i < theMdlFileData.textureCount; i++)
				{
					textureInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlTexture aTexture = new SourceMdlTexture();
					aTexture.nameOffset = theInputFileReader.ReadInt32();
					aTexture.flags = theInputFileReader.ReadInt32();
					aTexture.used = theInputFileReader.ReadInt32();
					aTexture.unused1 = theInputFileReader.ReadInt32();
					aTexture.materialP = theInputFileReader.ReadInt32();
					aTexture.clientMaterialP = theInputFileReader.ReadInt32();
					for (int x = 0; x <= 9; x++)
					{
						aTexture.unused[x] = theInputFileReader.ReadInt32();
					}
					theMdlFileData.theTextures.Add(aTexture);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aTexture.nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aTexture.thePathFileName = FileManager.ReadNullTerminatedString(theInputFileReader);

						// Convert all forward slashes to backward slashes.
						aTexture.thePathFileName = FileManager.GetNormalizedPathFileName(aTexture.thePathFileName);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.thePathFileName = " + aTexture.thePathFileName);
						//End If
					}
					else
					{
						aTexture.thePathFileName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures " + theMdlFileData.theTextures.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTextures alignment");
			}
		}

		public void ReadTexturePaths()
		{
			if (theMdlFileData.texturePathCount > 0)
			{
				long texturePathInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(theMdlFileData.texturePathOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theTexturePaths = new List<string>(theMdlFileData.texturePathCount);
				int texturePathOffset = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string aTexturePath = null;
				for (int i = 0; i < theMdlFileData.texturePathCount; i++)
				{
					texturePathInputFileStreamPosition = theInputFileReader.BaseStream.Position;
	//				Dim aTexturePath As String
					texturePathOffset = theInputFileReader.ReadInt32();

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (texturePathOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aTexturePath = FileManager.ReadNullTerminatedString(theInputFileReader);

						//TEST: Convert all forward slashes to backward slashes.
						aTexturePath = FileManager.GetNormalizedPathFileName(aTexturePath);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath = " + aTexturePath);
						//End If
					}
					else
					{
						aTexturePath = "";
					}
					theMdlFileData.theTexturePaths.Add(aTexturePath);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths " + theMdlFileData.theTexturePaths.Count.ToString());

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

				theInputFileReader.BaseStream.Seek(theMdlFileData.skinFamilyOffset, SeekOrigin.Begin);
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
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies " + theMdlFileData.theSkinFamilies.Count.ToString());

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment");
			}
		}

		public void ReadModelGroups()
		{
			if (theMdlFileData.includeModelCount > 0)
			{
				long includeModelInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(theMdlFileData.includeModelOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theModelGroups = new List<SourceMdlModelGroup>(theMdlFileData.includeModelCount);
				for (int i = 0; i < theMdlFileData.includeModelCount; i++)
				{
					includeModelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlModelGroup aModelGroup = new SourceMdlModelGroup();
					aModelGroup.labelOffset = theInputFileReader.ReadInt32();
					aModelGroup.fileNameOffset = theInputFileReader.ReadInt32();
					theMdlFileData.theModelGroups.Add(aModelGroup);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aModelGroup.labelOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.labelOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aModelGroup.theLabel = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theLabel = " + aModelGroup.theLabel);
						//End If
					}
					else
					{
						aModelGroup.theLabel = "";
					}
					if (aModelGroup.fileNameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.fileNameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aModelGroup.theFileName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theFileName = " + aModelGroup.theFileName);
						//End If
					}
					else
					{
						aModelGroup.theFileName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theModelGroups " + theMdlFileData.theModelGroups.Count.ToString());
			}
		}

		public void ReadFlexControllerUis()
		{
			if (theMdlFileData.flexControllerUiCount > 0 && !(theMdlFileData.version == 44 && theMdlFileData.flexControllerUiOffset == SourceMdlFileData49.text_SCAL_VERSION44Vindictus))
			{
				long flexControllerUiInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(theMdlFileData.flexControllerUiOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theFlexControllerUis = new List<SourceMdlFlexControllerUi>(theMdlFileData.flexControllerUiCount);
				for (int i = 0; i < theMdlFileData.flexControllerUiCount; i++)
				{
					flexControllerUiInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlFlexControllerUi aFlexControllerUi = new SourceMdlFlexControllerUi();
					aFlexControllerUi.nameOffset = theInputFileReader.ReadInt32();
					aFlexControllerUi.config0 = theInputFileReader.ReadInt32();
					aFlexControllerUi.config1 = theInputFileReader.ReadInt32();
					aFlexControllerUi.config2 = theInputFileReader.ReadInt32();
					aFlexControllerUi.remapType = theInputFileReader.ReadByte();
					aFlexControllerUi.controlIsStereo = theInputFileReader.ReadByte();
					for (int x = 0; x < aFlexControllerUi.unused.Length; x++)
					{
						aFlexControllerUi.unused[x] = theInputFileReader.ReadByte();
					}
					theMdlFileData.theFlexControllerUis.Add(aFlexControllerUi);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aFlexControllerUi.nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(flexControllerUiInputFileStreamPosition + aFlexControllerUi.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aFlexControllerUi.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexControllerUi.theName = " + aFlexControllerUi.theName);
						//End If
					}
					else
					{
						aFlexControllerUi.theName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllerUis " + theMdlFileData.theFlexControllerUis.Count.ToString());
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

		//FROM: SourceEngine2007\src_main\utils\studiomdl\write.cpp
		//static void WriteBoneTransforms( studiohdr2_t *phdr, mstudiobone_t *pBone )
		//{
		//	matrix3x4_t identity;
		//	SetIdentityMatrix( identity );

		//	int nTransformCount = 0;
		//	for (int i = 0; i < g_numbones; i++)
		//	{
		//		if ( g_bonetable[i].flags & BONE_ALWAYS_PROCEDURAL )
		//			continue;
		//		int nParent = g_bonetable[i].parent;

		//		// Transformation is necessary if either you or your parent was realigned
		//		if ( MatricesAreEqual( identity, g_bonetable[i].srcRealign ) &&
		//			 ( ( nParent < 0 ) || MatricesAreEqual( identity, g_bonetable[nParent].srcRealign ) ) )
		//			continue;

		//		++nTransformCount;
		//	}

		//	// save bone transform info
		//	mstudiosrcbonetransform_t *pSrcBoneTransform = (mstudiosrcbonetransform_t *)pData;
		//	phdr->numsrcbonetransform = nTransformCount;
		//	phdr->srcbonetransformindex = pData - pStart;
		//	pData += nTransformCount * sizeof( mstudiosrcbonetransform_t );
		//	int bt = 0;
		//	for ( int i = 0; i < g_numbones; i++ )
		//	{
		//		if ( g_bonetable[i].flags & BONE_ALWAYS_PROCEDURAL )
		//			continue;
		//		int nParent = g_bonetable[i].parent;
		//		if ( MatricesAreEqual( identity, g_bonetable[i].srcRealign ) &&
		//			( ( nParent < 0 ) || MatricesAreEqual( identity, g_bonetable[nParent].srcRealign ) ) )
		//			continue;
		//
		//		// What's going on here?
		//		// So, when we realign a bone, we want to do it in a way so that the child bones
		//		// have the same bone->world transform. If we take T as the src realignment transform
		//		// for the parent, P is the parent to world, and C is the child to parent, we expect 
		//		// the child->world is constant after realignment:
		//		//		CtoW = P * C = ( P * T ) * ( T^-1 * C )
		//		// therefore Cnew = ( T^-1 * C )
		//		if ( nParent >= 0 )
		//		{
		//			MatrixInvert( g_bonetable[nParent].srcRealign, pSrcBoneTransform[bt].pretransform );
		//		}
		//		else
		//		{
		//			SetIdentityMatrix( pSrcBoneTransform[bt].pretransform );
		//		}
		//		MatrixCopy( g_bonetable[i].srcRealign, pSrcBoneTransform[bt].posttransform );
		//		AddToStringTable( &pSrcBoneTransform[bt], &pSrcBoneTransform[bt].sznameindex, g_bonetable[i].name );
		//		++bt;
		//	}
		//	ALIGN4( pData );
		//
		//[second part is in comment before next Sub below]
		//}
		public void ReadBoneTransforms()
		{
			if (theMdlFileData.sourceBoneTransformCount > 0)
			{
				long boneInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				theInputFileReader.BaseStream.Seek(theMdlFileData.sourceBoneTransformOffset, SeekOrigin.Begin);
				fileOffsetStart = theInputFileReader.BaseStream.Position;

				theMdlFileData.theBoneTransforms = new List<SourceMdlBoneTransform>(theMdlFileData.sourceBoneTransformCount);
				for (int i = 0; i < theMdlFileData.sourceBoneTransformCount; i++)
				{
					boneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlBoneTransform aBoneTransform = new SourceMdlBoneTransform();

					aBoneTransform.nameOffset = theInputFileReader.ReadInt32();

					aBoneTransform.preTransformColumn0 = new SourceVector();
					aBoneTransform.preTransformColumn1 = new SourceVector();
					aBoneTransform.preTransformColumn2 = new SourceVector();
					aBoneTransform.preTransformColumn3 = new SourceVector();
					aBoneTransform.preTransformColumn0.x = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn1.x = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn2.x = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn3.x = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn0.y = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn1.y = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn2.y = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn3.y = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn0.z = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn1.z = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn2.z = theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn3.z = theInputFileReader.ReadSingle();

					aBoneTransform.postTransformColumn0 = new SourceVector();
					aBoneTransform.postTransformColumn1 = new SourceVector();
					aBoneTransform.postTransformColumn2 = new SourceVector();
					aBoneTransform.postTransformColumn3 = new SourceVector();
					aBoneTransform.postTransformColumn0.x = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn1.x = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn2.x = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn3.x = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn0.y = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn1.y = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn2.y = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn3.y = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn0.z = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn1.z = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn2.z = theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn3.z = theInputFileReader.ReadSingle();

					theMdlFileData.theBoneTransforms.Add(aBoneTransform);

					inputFileStreamPosition = theInputFileReader.BaseStream.Position;

					if (aBoneTransform.nameOffset != 0)
					{
						theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBoneTransform.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = theInputFileReader.BaseStream.Position;

						aBoneTransform.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBoneTransform.theName = " + aBoneTransform.theName);
						//End If
					}
					else if (aBoneTransform.theName == null)
					{
						aBoneTransform.theName = "";
					}

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneTransforms " + theMdlFileData.theBoneTransforms.Count.ToString());

				//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneTransforms alignment")
			}
		}

		// Part of function in above Sub's comment.
		//	if (g_numbones > 1)
		//	{
		//		// write second bone table
		//		phdr->linearboneindex = pData - (byte *)phdr;
		//		mstudiolinearbone_t *pLinearBone =  (mstudiolinearbone_t *)pData;
		//		pData += sizeof( *pLinearBone );
		//
		//		pLinearBone->numbones = g_numbones;
		//
		//#define WRITE_BONE_BLOCK( type, srcfield, dest, destindex ) \
		//		type *##dest = (type *)pData; \
		//		pLinearBone->##destindex = pData - (byte *)pLinearBone; \
		//		pData += g_numbones * sizeof( *##dest ); \
		//		ALIGN4( pData ); \
		//		for ( int i = 0; i < g_numbones; i++) \
		//			dest##[i] = pBone[i].##srcfield;

		//		WRITE_BONE_BLOCK( int, flags, pFlags, flagsindex );
		//		WRITE_BONE_BLOCK( int, parent, pParent, parentindex );
		//		WRITE_BONE_BLOCK( Vector, pos, pPos, posindex );
		//		WRITE_BONE_BLOCK( Quaternion, quat, pQuat, quatindex );
		//		WRITE_BONE_BLOCK( RadianEuler, rot, pRot, rotindex );
		//		WRITE_BONE_BLOCK( matrix3x4_t, poseToBone, pPoseToBone, posetoboneindex );
		//		WRITE_BONE_BLOCK( Vector, posscale, pPoseScale, posscaleindex );
		//		WRITE_BONE_BLOCK( Vector, rotscale, pRotScale, rotscaleindex );
		//		WRITE_BONE_BLOCK( Quaternion, qAlignment, pQAlignment, qalignmentindex );
		//	}
		public void ReadLinearBoneTable()
		{
			if (theMdlFileData.linearBoneOffset > 0)
			{
				try
				{
					long boneTableInputFileStreamPosition = 0;
					//Dim inputFileStreamPosition As Long
					long fileOffsetStart = 0;
					long fileOffsetEnd = 0;
					long fileOffsetStart2 = 0;
					long fileOffsetEnd2 = 0;

					//If Me.theMdlFileData.studioHeader2Offset_VERSION48 + Me.theMdlFileData.linearBoneOffset <> Me.theInputFileReader.BaseStream.Position Then
					//	Dim debug As Integer = 4242
					//End If
					theInputFileReader.BaseStream.Seek(theMdlFileData.studioHeader2Offset + theMdlFileData.linearBoneOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;
					boneTableInputFileStreamPosition = theInputFileReader.BaseStream.Position;

					SourceMdlLinearBone linearBoneTable = null;
					theMdlFileData.theLinearBoneTable = new SourceMdlLinearBone();
					linearBoneTable = theMdlFileData.theLinearBoneTable;
					linearBoneTable.boneCount = theInputFileReader.ReadInt32();
					linearBoneTable.flagsOffset = theInputFileReader.ReadInt32();
					linearBoneTable.parentOffset = theInputFileReader.ReadInt32();
					linearBoneTable.posOffset = theInputFileReader.ReadInt32();
					linearBoneTable.quatOffset = theInputFileReader.ReadInt32();
					linearBoneTable.rotOffset = theInputFileReader.ReadInt32();
					linearBoneTable.poseToBoneOffset = theInputFileReader.ReadInt32();
					linearBoneTable.posScaleOffset = theInputFileReader.ReadInt32();
					linearBoneTable.rotScaleOffset = theInputFileReader.ReadInt32();
					linearBoneTable.qAlignmentOffset = theInputFileReader.ReadInt32();
					for (int x = 0; x < linearBoneTable.unused.Length; x++)
					{
						linearBoneTable.unused[x] = theInputFileReader.ReadInt32();
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLinearBoneTable header");

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.flagsOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						int flags = theInputFileReader.ReadInt32();
						linearBoneTable.theFlags.Add(flags);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theFlags");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theFlags alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.parentOffset != theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.parentOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						int parent = theInputFileReader.ReadInt32();
						linearBoneTable.theParents.Add(parent);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theParents");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theParents alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.posOffset != theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.posOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector position = new SourceVector();
						position.x = theInputFileReader.ReadSingle();
						position.y = theInputFileReader.ReadSingle();
						position.z = theInputFileReader.ReadSingle();
						linearBoneTable.thePositions.Add(position);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePositions");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePositions alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.quatOffset != theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.quatOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceQuaternion quaternion = new SourceQuaternion();
						quaternion.x = theInputFileReader.ReadSingle();
						quaternion.y = theInputFileReader.ReadSingle();
						quaternion.z = theInputFileReader.ReadSingle();
						quaternion.w = theInputFileReader.ReadSingle();
						linearBoneTable.theQuaternions.Add(quaternion);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theQuaternions");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theQuaternions alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.rotOffset != theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.rotOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector rotation = new SourceVector();
						rotation.x = theInputFileReader.ReadSingle();
						rotation.y = theInputFileReader.ReadSingle();
						rotation.z = theInputFileReader.ReadSingle();
						linearBoneTable.theRotations.Add(rotation);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theRotations");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theRotations alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.poseToBoneOffset != theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.poseToBoneOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector poseToBoneDataColumn0 = new SourceVector();
						SourceVector poseToBoneDataColumn1 = new SourceVector();
						SourceVector poseToBoneDataColumn2 = new SourceVector();
						SourceVector poseToBoneDataColumn3 = new SourceVector();
						poseToBoneDataColumn0.x = theInputFileReader.ReadSingle();
						poseToBoneDataColumn1.x = theInputFileReader.ReadSingle();
						poseToBoneDataColumn2.x = theInputFileReader.ReadSingle();
						poseToBoneDataColumn3.x = theInputFileReader.ReadSingle();
						poseToBoneDataColumn0.y = theInputFileReader.ReadSingle();
						poseToBoneDataColumn1.y = theInputFileReader.ReadSingle();
						poseToBoneDataColumn2.y = theInputFileReader.ReadSingle();
						poseToBoneDataColumn3.y = theInputFileReader.ReadSingle();
						poseToBoneDataColumn0.z = theInputFileReader.ReadSingle();
						poseToBoneDataColumn1.z = theInputFileReader.ReadSingle();
						poseToBoneDataColumn2.z = theInputFileReader.ReadSingle();
						poseToBoneDataColumn3.z = theInputFileReader.ReadSingle();
						linearBoneTable.thePoseToBoneDataColumn0s.Add(poseToBoneDataColumn0);
						linearBoneTable.thePoseToBoneDataColumn1s.Add(poseToBoneDataColumn1);
						linearBoneTable.thePoseToBoneDataColumn2s.Add(poseToBoneDataColumn2);
						linearBoneTable.thePoseToBoneDataColumn3s.Add(poseToBoneDataColumn3);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePoseToBoneDataColumns");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePoseToBoneDataColumns alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.posScaleOffset != theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.posScaleOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector positionScale = new SourceVector();
						positionScale.x = theInputFileReader.ReadSingle();
						positionScale.y = theInputFileReader.ReadSingle();
						positionScale.z = theInputFileReader.ReadSingle();
						linearBoneTable.thePositionScales.Add(positionScale);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePositionScales");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePositionScales alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.rotScaleOffset != theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.rotScaleOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector rotationScale = new SourceVector();
						rotationScale.x = theInputFileReader.ReadSingle();
						rotationScale.y = theInputFileReader.ReadSingle();
						rotationScale.z = theInputFileReader.ReadSingle();
						linearBoneTable.theRotationScales.Add(rotationScale);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theRotationScales");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theRotationScales alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.qAlignmentOffset != theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.qAlignmentOffset, SeekOrigin.Begin);
					fileOffsetStart2 = theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceQuaternion qAlignment = new SourceQuaternion();
						qAlignment.x = theInputFileReader.ReadSingle();
						qAlignment.y = theInputFileReader.ReadSingle();
						qAlignment.z = theInputFileReader.ReadSingle();
						qAlignment.w = theInputFileReader.ReadSingle();
						linearBoneTable.theQAlignments.Add(qAlignment);
					}
					fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
					//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theQAlignments");
					//End If
					theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theQAlignments alignment");
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		//Public Sub ReadFinalBytesAlignment()
		//	Me.theMdlFileData.theFileSeekLog.LogAndAlignFromFileSeekLogEnd(Me.theInputFileReader, 4, "Final bytes alignment")
		//End Sub

		//Public Sub ReadUnknownValues(ByVal aFileSeekLog As FileSeekLog)
		//	'Me.theMdlFileData.theUnknownValues = New List(Of UnknownValue)()

		//	Dim offsetStart As Long
		//	Dim offsetEnd As Long
		//	Dim offsetGapStart As Long
		//	Dim offsetGapEnd As Long
		//	offsetStart = -1
		//	Try
		//		For i As Integer = 0 To aFileSeekLog.theFileSeekList.Count - 1
		//			If offsetStart = -1 Then
		//				offsetStart = aFileSeekLog.theFileSeekList.Keys(i)
		//			End If
		//			offsetEnd = aFileSeekLog.theFileSeekList.Values(i)
		//			If (i = aFileSeekLog.theFileSeekList.Count - 1) Then
		//				Exit For
		//			ElseIf (offsetEnd + 1 <> aFileSeekLog.theFileSeekList.Keys(i + 1)) Then
		//				offsetGapStart = offsetEnd + 1
		//				offsetGapEnd = aFileSeekLog.theFileSeekList.Keys(i + 1) - 1
		//				Me.theInputFileReader.BaseStream.Seek(offsetGapStart, SeekOrigin.Begin)
		//				For offset As Long = offsetGapStart To offsetGapEnd Step 4
		//					If offsetGapEnd - offset < 3 Then
		//						For byteOffset As Long = offset To offsetGapEnd
		//							Dim anUnknownValue As New UnknownValue()
		//							anUnknownValue.offset = byteOffset
		//							anUnknownValue.type = "Byte"
		//							anUnknownValue.value = Me.theInputFileReader.ReadByte()
		//							Me.theMdlFileData.theUnknownValues.Add(anUnknownValue)
		//						Next
		//					Else
		//						Dim anUnknownValue As New UnknownValue()
		//						anUnknownValue.offset = offset
		//						anUnknownValue.type = "Int32"
		//						anUnknownValue.value = Me.theInputFileReader.ReadInt32()
		//						Me.theMdlFileData.theUnknownValues.Add(anUnknownValue)
		//					End If
		//				Next
		//				offsetStart = -1
		//			End If
		//		Next
		//	Catch ex As Exception
		//		Dim debug As Integer = 4242
		//	End Try
		//End Sub

		public void ReadUnreadBytes()
		{
			theMdlFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

		public void PostProcess()
		{
			if (theMdlFileData.theBodyParts != null)
			{
				foreach (SourceMdlBodyPart aBodyPart in theMdlFileData.theBodyParts)
				{
					foreach (SourceMdlModel aBodyModel in aBodyPart.theModels)
					{
						if (aBodyModel.theEyeballs != null && aBodyModel.theEyeballs.Count > 0)
						{
							aBodyPart.theModelCommandIsUsed = true;
							aBodyPart.theEyeballOptionIsUsed = true;
							break;
						}

						if (aBodyModel.theMeshes != null)
						{
							foreach (SourceMdlMesh aMesh in aBodyModel.theMeshes)
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
			FlexFrame aFlexFrame = null;
			SourceMdlBodyPart aBodyPart = null;
			SourceMdlModel aModel = null;
			SourceMdlMesh aMesh = null;
			SourceMdlFlex aFlex = null;
			FlexFrame searchedFlexFrame = null;

			//Me.theMdlFileData.theFlexFrames = New List(Of FlexFrame)()

			//'NOTE: Create the defaultflex.
			//aFlexFrame = New FlexFrame()
			//Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)

			if (theMdlFileData.theFlexDescs != null && theMdlFileData.theFlexDescs.Count > 0)
			{
				List<List<FlexFrame>> flexDescToFlexFrames = null;
				int meshVertexIndexStart = 0;
				int cumulativebodyPartVertexIndexStart = 0;

				for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
				{
					aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];

					flexDescToFlexFrames = new List<List<FlexFrame>>(theMdlFileData.theFlexDescs.Count);
					for (int x = 0; x < theMdlFileData.theFlexDescs.Count; x++)
					{
						List<FlexFrame> flexFrameList = new List<FlexFrame>();
						flexDescToFlexFrames.Add(flexFrameList);
					}

					aBodyPart.theFlexFrames = new List<FlexFrame>();
					//NOTE: Create the defaultflex.
					aFlexFrame = new FlexFrame();
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
												aFlexFrame = new FlexFrame();
												//Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)
												aBodyPart.theFlexFrames.Add(aFlexFrame);
												aFlexFrame.bodyAndMeshVertexIndexStarts = new List<int>();
												aFlexFrame.flexes = new List<SourceMdlFlex>();

												int aFlexDescPartnerIndex = aMesh.theFlexes[flexIndex].flexDescPartnerIndex;

												aFlexFrame.flexName = theMdlFileData.theFlexDescs[aFlex.flexDescIndex].theName;
												if (aFlexDescPartnerIndex > 0)
												{
													//line += "flexpair """
													//aFlexFrame.flexName = aFlexFrame.flexName.Remove(aFlexFrame.flexName.Length - 1, 1)
													aFlexFrame.flexDescription = aFlexFrame.flexName;
													aFlexFrame.flexDescription += "+";
													aFlexFrame.flexDescription += theMdlFileData.theFlexDescs[aFlex.flexDescPartnerIndex].theName;
													aFlexFrame.flexHasPartner = true;
													aFlexFrame.flexPartnerName = theMdlFileData.theFlexDescs[aFlex.flexDescPartnerIndex].theName;
													aFlexFrame.flexSplit = GetSplit(aFlex, meshVertexIndexStart);
													theMdlFileData.theFlexDescs[aFlex.flexDescPartnerIndex].theDescIsUsedByFlex = true;
												}
												else
												{
													//line += "flex """
													aFlexFrame.flexDescription = aFlexFrame.flexName;
													aFlexFrame.flexHasPartner = false;
												}
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

		public void WriteInternalMdlFileName(string internalMdlFileName)
		{
			theOutputFileWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
			//TODO: Should only write up to 64 characters.
			theOutputFileWriter.Write(internalMdlFileName.ToCharArray());
			//NOTE: Write the ending null byte.
			theOutputFileWriter.Write(Convert.ToByte(0));
		}

		public void WriteInternalMdlFileNameCopy(string internalMdlFileNameCopy)
		{
			if (theMdlFileData.nameCopyOffset > 0)
			{
				// Set a new offset for the file name at end-of-file's null byte.
				theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
				//NOTE: Important that offset be an Integer (4 bytes) rather than a Long (8 bytes).
				int offset = (int)theOutputFileWriter.BaseStream.Position - 0x198;
				theOutputFileWriter.BaseStream.Seek(0x1AC, SeekOrigin.Begin);
				theOutputFileWriter.Write(offset);

				// Write the new file name.
				//Me.theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin)
				theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
				theOutputFileWriter.Write(internalMdlFileNameCopy.ToCharArray());
				//NOTE: Write the ending null byte.
				theOutputFileWriter.Write(Convert.ToByte(0));

				// Write the new file size.
				theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
				offset = (int)theOutputFileWriter.BaseStream.Position;
				theOutputFileWriter.BaseStream.Seek(0x4C, SeekOrigin.Begin);
				theOutputFileWriter.Write(offset);
			}
		}

		//TODO: Review WriteInternalAniFileName() for each MDL version.
		public void WriteInternalAniFileName(string internalAniFileName)
		{
			if (theMdlFileData.animBlockCount > 0)
			{
				if (theMdlFileData.animBlockNameOffset > 0)
				{

					if (theMdlFileData.version == 44)
					{
						theOutputFileWriter.BaseStream.Seek(theMdlFileData.animBlockNameOffset, SeekOrigin.Begin);
						//TODO: Should only write up to existing count of characters.
						if (theMdlFileData.theAnimBlockRelativePathFileName.Length > internalAniFileName.Length)
						{
							theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
							long offset = theOutputFileWriter.BaseStream.Position;
							theOutputFileWriter.BaseStream.Seek(0x15C, SeekOrigin.Begin);
							theOutputFileWriter.Write(offset);
							theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
						}
						theOutputFileWriter.Write(internalAniFileName.ToCharArray());
						//NOTE: Write the ending null byte.
						theOutputFileWriter.Write(Convert.ToByte(0));
					}
					else if (theMdlFileData.version <= 48)
					{
						// Set a new offset for the file name at end-of-file's second null byte.
						theOutputFileWriter.BaseStream.Seek(-2, SeekOrigin.End);
						//NOTE: Important that offset be an Integer (4 bytes) rather than a Long (8 bytes).
						int offset = (int)theOutputFileWriter.BaseStream.Position;
						theOutputFileWriter.BaseStream.Seek(0x15C, SeekOrigin.Begin);
						theOutputFileWriter.Write(offset);

						// Write the new file name.
						theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
						theOutputFileWriter.Write(internalAniFileName.ToCharArray());
						//NOTE: Write the ending null byte.
						theOutputFileWriter.Write(Convert.ToByte(0));

						// Write the new end-of-file's null bytes.
						theOutputFileWriter.Write(Convert.ToByte(0));
						theOutputFileWriter.Write(Convert.ToByte(0));

						// Write the new file size.
						theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
						offset = (int)theOutputFileWriter.BaseStream.Position;
						theOutputFileWriter.BaseStream.Seek(0x4C, SeekOrigin.Begin);
						theOutputFileWriter.Write(offset);
					}
					else
					{
						// Set a new offset for the file name at end-of-file's null byte.
						theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
						//NOTE: Important that offset be an Integer (4 bytes) rather than a Long (8 bytes).
						int offset = (int)theOutputFileWriter.BaseStream.Position;
						theOutputFileWriter.BaseStream.Seek(0x15C, SeekOrigin.Begin);
						theOutputFileWriter.Write(offset);

						// Write the new file name.
						//Me.theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin)
						theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
						theOutputFileWriter.Write(internalAniFileName.ToCharArray());
						//NOTE: Write the ending null byte.
						theOutputFileWriter.Write(Convert.ToByte(0));

						// Write the new file size.
						theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
						offset = (int)theOutputFileWriter.BaseStream.Position;
						theOutputFileWriter.BaseStream.Seek(0x4C, SeekOrigin.Begin);
						theOutputFileWriter.Write(offset);
					}

				}
			}
		}

#endregion

#region Private Methods

		//Protected Sub LogToEndAndAlignToNextStart(ByVal fileOffsetEnd As Long, ByVal byteAlignmentCount As Integer, ByVal description As String)
		//	Dim fileOffsetStart2 As Long
		//	Dim fileOffsetEnd2 As Long

		//	'fileOffsetStart2 = fileOffsetEnd + 1
		//	'fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
		//	'If fileOffsetEnd2 >= fileOffsetStart2 Then
		//	'	Me.theInputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
		//	'	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, description)
		//	'End If
		//	fileOffsetStart2 = fileOffsetEnd + 1
		//	fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
		//	Me.theInputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
		//	If fileOffsetEnd2 >= fileOffsetStart2 Then
		//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, description)
		//	End If
		//End Sub

		//NOTE: eyelidPartIndex values:
		//      0: lowerer
		//      1: raiser
		private int FindFlexFrameIndex(List<FlexFrame> flexFrames, string flexName, int eyelidPartIndex)
		{
			int eyelidFlexCount = 0;
			for (int i = 0; i < flexFrames.Count; i++)
			{
				if (flexName == flexFrames[i].flexName)
				{
					if (eyelidFlexCount == eyelidPartIndex)
					{
						return i;
					}
					else
					{
						eyelidFlexCount += 1;
					}
				}
			}
			return 0;
		}

		private double GetSplit(SourceMdlFlex aFlex, int meshVertexIndexStart)
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

		protected SourceMdlFileData49 theMdlFileData;

#endregion

	}

}