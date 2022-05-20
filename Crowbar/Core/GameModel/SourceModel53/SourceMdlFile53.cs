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
	public class SourceMdlFile53
	{

#region Creation and Destruction

		public SourceMdlFile53(BinaryReader mdlFileReader, SourceMdlFileData53 mdlFileData)
		{
			this.theInputFileReader = mdlFileReader;
			this.theMdlFileData = mdlFileData;

			this.theMdlFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;
		}

		public SourceMdlFile53(BinaryWriter mdlFileWriter, SourceMdlFileData53 mdlFileData)
		{
			this.theOutputFileWriter = mdlFileWriter;
			this.theMdlFileData = mdlFileData;
		}

		// Only here to make compiler happy with SourceAniFileXX that inherits from this class.
		protected SourceMdlFile53()
		{
		}

#endregion

#region Methods

		public void ReadMdlHeader00(string logDescription)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			// Offsets: 0x00, 0x04, 0x08, 0x0C (12), 0x4C (76)
			this.theMdlFileData.id = this.theInputFileReader.ReadChars(4);
			this.theMdlFileData.theID = new string(this.theMdlFileData.id);
			this.theMdlFileData.version = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.checksum = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.nameCopyOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.theNameCopy = this.GetStringAtOffset(0, this.theMdlFileData.nameCopyOffset, "theNameCopy");
			this.theMdlFileData.name = this.theInputFileReader.ReadChars(64);
			if (!string.IsNullOrEmpty(this.theMdlFileData.theNameCopy))
			{
				this.theMdlFileData.theModelName = this.theMdlFileData.theNameCopy;
			}
			else
			{
				this.theMdlFileData.theModelName = (new string(this.theMdlFileData.name)).Trim('\0');
			}

			this.theMdlFileData.fileSize = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.theActualFileSize = this.theInputFileReader.BaseStream.Length;

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			if (!string.IsNullOrEmpty(logDescription))
			{
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription + " (Actual version: " + this.theMdlFileData.version.ToString() + "; override version: 53)");
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

			// Offsets: 0x50, 0x54, 0x58
			this.theMdlFileData.eyePositionX = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.eyePositionY = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.eyePositionZ = this.theInputFileReader.ReadSingle();

			// Offsets: 0x5C, 0x60, 0x64
			this.theMdlFileData.illuminationPosition.x = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.illuminationPosition.y = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.illuminationPosition.z = this.theInputFileReader.ReadSingle();

			// Offsets: 0x68, 0x6C, 0x70
			this.theMdlFileData.hullMinPositionX = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.hullMinPositionY = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.hullMinPositionZ = this.theInputFileReader.ReadSingle();

			// Offsets: 0x74, 0x78, 0x7C
			this.theMdlFileData.hullMaxPositionX = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.hullMaxPositionY = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.hullMaxPositionZ = this.theInputFileReader.ReadSingle();

			// Offsets: 0x80, 0x84, 0x88
			this.theMdlFileData.viewBoundingBoxMinPositionX = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.viewBoundingBoxMinPositionY = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.viewBoundingBoxMinPositionZ = this.theInputFileReader.ReadSingle();

			// Offsets: 0x8C, 0x90, 0x94
			this.theMdlFileData.viewBoundingBoxMaxPositionX = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.viewBoundingBoxMaxPositionY = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.viewBoundingBoxMaxPositionZ = this.theInputFileReader.ReadSingle();

			// Offsets: 0x98
			this.theMdlFileData.flags = this.theInputFileReader.ReadInt32();

			// Offsets: 0x9C (156), 0xA0
			this.theMdlFileData.boneCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xA4, 0xA8
			this.theMdlFileData.boneControllerCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.boneControllerOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xAC (172), 0xB0
			this.theMdlFileData.hitboxSetCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.hitboxSetOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xB4 (180), 0xB8
			this.theMdlFileData.localAnimationCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localAnimationOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xBC (188), 0xC0 (192)
			this.theMdlFileData.localSequenceCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localSequenceOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xC4, 0xC8
			this.theMdlFileData.activityListVersion = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.eventsIndexed = this.theInputFileReader.ReadInt32();

			// Offsets: 0xCC (204), 0xD0 (208)
			this.theMdlFileData.textureCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.textureOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xD4 (212), 0xD8
			this.theMdlFileData.texturePathCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.texturePathOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xDC, 0xE0 (224), 0xE4 (228)
			this.theMdlFileData.skinReferenceCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinFamilyCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.skinFamilyOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xE8 (232), 0xEC (236)
			this.theMdlFileData.bodyPartCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.bodyPartOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xF0 (240), 0xF4 (244)
			this.theMdlFileData.localAttachmentCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localAttachmentOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0xF8, 0xFC, 0x0100
			this.theMdlFileData.localNodeCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localNodeOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.localNodeNameOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0x0104 (), 0x0108 ()
			this.theMdlFileData.flexDescCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexDescOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0x010C (), 0x0110 ()
			this.theMdlFileData.flexControllerCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexControllerOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0x0114 (), 0x0118 ()
			this.theMdlFileData.flexRuleCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexRuleOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0x011C (), 0x0120 ()
			this.theMdlFileData.ikChainCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.ikChainOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0x0124 (), 0x0128 ()
			this.theMdlFileData.mouthCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.mouthOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0x012C (), 0x0130 ()
			this.theMdlFileData.localPoseParamaterCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localPoseParameterOffset = this.theInputFileReader.ReadInt32();

			// Offsets: 0x0134 ()
			this.theMdlFileData.surfacePropOffset = this.theInputFileReader.ReadInt32();

			//'TODO: Same as some lines below. Move to a separate function.
			//If Me.theMdlFileData.surfacePropOffset > 0 Then
			//	inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			//	Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.surfacePropOffset, SeekOrigin.Begin)
			//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

			//	Me.theMdlFileData.theSurfacePropName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

			//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
			//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
			//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName")
			//	End If
			//	Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			//Else
			//	Me.theMdlFileData.theSurfacePropName = ""
			//End If
			//------
			this.theMdlFileData.theSurfacePropName = this.GetStringAtOffset(0, this.theMdlFileData.surfacePropOffset, "theSurfacePropName");

			// Offsets: 0x0138 (312), 0x013C (316)
			this.theMdlFileData.keyValueOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.keyValueSize = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.localIkAutoPlayLockCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.localIkAutoPlayLockOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.mass = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.contents = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.includeModelCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.includeModelOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.virtualModelP = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.animBlockNameOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.animBlockCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.animBlockOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.animBlockModelP = this.theInputFileReader.ReadInt32();
			if (this.theMdlFileData.animBlockCount > 0)
			{
				if (this.theMdlFileData.animBlockNameOffset > 0)
				{
					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.animBlockNameOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theAnimBlockRelativePathFileName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theAnimBlockRelativePathFileName = " + this.theMdlFileData.theAnimBlockRelativePathFileName);
					}
					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
				if (this.theMdlFileData.animBlockOffset > 0)
				{
					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.animBlockOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theAnimBlocks = new List<SourceMdlAnimBlock>(this.theMdlFileData.animBlockCount);
					for (int offset = 0; offset < this.theMdlFileData.animBlockCount; offset++)
					{
						SourceMdlAnimBlock anAnimBlock = new SourceMdlAnimBlock();
						anAnimBlock.dataStart = this.theInputFileReader.ReadInt32();
						anAnimBlock.dataEnd = this.theInputFileReader.ReadInt32();
						this.theMdlFileData.theAnimBlocks.Add(anAnimBlock);
					}

					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theAnimBlocks " + this.theMdlFileData.theAnimBlocks.Count.ToString());
					}
					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}
			}

			this.theMdlFileData.boneTableByNameOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.vertexBaseP = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.indexBaseP = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.directionalLightDot = this.theInputFileReader.ReadByte();

			this.theMdlFileData.rootLod = this.theInputFileReader.ReadByte();

			this.theMdlFileData.allowedRootLodCount = this.theInputFileReader.ReadByte();

			this.theMdlFileData.unused = this.theInputFileReader.ReadByte();

			this.theMdlFileData.unused4 = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.flexControllerUiCount = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.flexControllerUiOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.vertAnimFixedPointScale = this.theInputFileReader.ReadSingle();
			this.theMdlFileData.surfacePropLookup = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.studioHeader2Offset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.unknownOffset01 = this.theInputFileReader.ReadInt32();

			if (this.theMdlFileData.bodyPartCount == 0 && this.theMdlFileData.localSequenceCount > 0)
			{
				this.theMdlFileData.theMdlFileOnlyHasAnimations = true;
			}

			//Me.theMdlFileData.sourceBoneTransformCount = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.sourceBoneTransformOffset = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.illumPositionAttachmentIndex = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.maxEyeDeflection = Me.theInputFileReader.ReadSingle()
			//Me.theMdlFileData.linearBoneOffset = Me.theInputFileReader.ReadInt32()

			//Me.theMdlFileData.nameOffset = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.boneFlexDriverCount = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.boneFlexDriverOffset = Me.theInputFileReader.ReadInt32()

			//For x As Integer = 0 To Me.theMdlFileData.reserved.Length - 1
			//	Me.theMdlFileData.reserved(x) = Me.theInputFileReader.ReadInt32()
			//Next
			//======
			this.theMdlFileData.unknown01 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown02 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown03 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown04 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.vtxOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.vvdOffset = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown05 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.phyOffset = this.theInputFileReader.ReadInt32();

			this.theMdlFileData.unknown06 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown07 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown08 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknown09 = this.theInputFileReader.ReadInt32();
			this.theMdlFileData.unknownOffset02 = this.theInputFileReader.ReadInt32();

			for (int x = 0; x < this.theMdlFileData.unknown.Length; x++)
			{
				this.theMdlFileData.unknown[x] = this.theInputFileReader.ReadInt32();
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, logDescription);
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

					this.theMdlFileData.theBones = new List<SourceMdlBone>(this.theMdlFileData.boneCount);
					for (int i = 0; i < this.theMdlFileData.boneCount; i++)
					{
						boneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlBone aBone = new SourceMdlBone();

						aBone.nameOffset = this.theInputFileReader.ReadInt32();

						aBone.parentBoneIndex = this.theInputFileReader.ReadInt32();

						//' Skip some fields.
						//Me.theInputFileReader.ReadBytes(208)
						//------
						for (int j = 0; j < aBone.boneControllerIndex.Length; j++)
						{
							aBone.boneControllerIndex[j] = this.theInputFileReader.ReadInt32();
						}
						aBone.position = new SourceVector();
						aBone.position.x = this.theInputFileReader.ReadSingle();
						aBone.position.y = this.theInputFileReader.ReadSingle();
						aBone.position.z = this.theInputFileReader.ReadSingle();

						aBone.quat = new SourceQuaternion();
						aBone.quat.x = this.theInputFileReader.ReadSingle();
						aBone.quat.y = this.theInputFileReader.ReadSingle();
						aBone.quat.z = this.theInputFileReader.ReadSingle();
						aBone.quat.w = this.theInputFileReader.ReadSingle();

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
						aBone.contents = this.theInputFileReader.ReadInt32();

						for (int k = 0; k <= 7; k++)
						{
							aBone.unused[k] = this.theInputFileReader.ReadInt32();
						}

						//TODO: Add to data structure.
						for (int x = 1; x <= 7; x++)
						{
							this.theInputFileReader.ReadInt32();
						}

						this.theMdlFileData.theBones.Add(aBone);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						//If aBone.nameOffset <> 0 Then
						//	Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.nameOffset, SeekOrigin.Begin)
						//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						//	aBone.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theName")
						//	End If
						//ElseIf aBone.theName Is Nothing Then
						//	aBone.theName = ""
						//End If
						//------
						aBone.theName = this.GetStringAtOffset(boneInputFileStreamPosition, aBone.nameOffset, "aBone.theName");

						if (aBone.proceduralRuleOffset != 0)
						{
							if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_AXISINTERP)
							{
								this.ReadAxisInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_QUATINTERP)
							{
								this.theMdlFileData.theProceduralBonesCommandIsUsed = true;
								this.ReadQuatInterpBone(boneInputFileStreamPosition, aBone);
							}
							else if (aBone.proceduralRuleType == SourceMdlBone.STUDIO_PROC_JIGGLE)
							{
								this.ReadJiggleBone(boneInputFileStreamPosition, aBone);
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

		//TODO: VERIFY ReadAxisInterpBone()
		private void ReadAxisInterpBone(long boneInputFileStreamPosition, SourceMdlBone aBone)
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
			catch
			{
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
			catch
			{
			}
		}

		private void ReadJiggleBone(long boneInputFileStreamPosition, SourceMdlBone aBone)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			this.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			aBone.theJiggleBone = new SourceMdlJiggleBone();
			aBone.theJiggleBone.flags = this.theInputFileReader.ReadInt32();
			aBone.theJiggleBone.length = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.tipMass = this.theInputFileReader.ReadSingle();

			aBone.theJiggleBone.yawStiffness = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.yawDamping = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.pitchStiffness = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.pitchDamping = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.alongStiffness = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.alongDamping = this.theInputFileReader.ReadSingle();

			aBone.theJiggleBone.angleLimit = this.theInputFileReader.ReadSingle();

			aBone.theJiggleBone.minYaw = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.maxYaw = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.yawFriction = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.yawBounce = this.theInputFileReader.ReadSingle();

			aBone.theJiggleBone.minPitch = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.maxPitch = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.pitchFriction = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.pitchBounce = this.theInputFileReader.ReadSingle();

			aBone.theJiggleBone.baseMass = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseStiffness = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseDamping = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMinLeft = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMaxLeft = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseLeftFriction = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMinUp = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMaxUp = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseUpFriction = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMinForward = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseMaxForward = this.theInputFileReader.ReadSingle();
			aBone.theJiggleBone.baseForwardFriction = this.theInputFileReader.ReadSingle();

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theJiggleBone");
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

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneControllerOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theBoneControllers = new List<SourceMdlBoneController>(this.theMdlFileData.boneControllerCount);
				for (int i = 0; i < this.theMdlFileData.boneControllerCount; i++)
				{
					boneControllerInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlBoneController aBoneController = new SourceMdlBoneController();

					aBoneController.boneIndex = this.theInputFileReader.ReadInt32();
					aBoneController.type = this.theInputFileReader.ReadInt32();
					aBoneController.startBlah = this.theInputFileReader.ReadSingle();
					aBoneController.endBlah = this.theInputFileReader.ReadSingle();
					aBoneController.restIndex = this.theInputFileReader.ReadInt32();
					aBoneController.inputField = this.theInputFileReader.ReadInt32();
					if (this.theMdlFileData.version > 10)
					{
						for (int x = 0; x < aBoneController.unused.Length; x++)
						{
							aBoneController.unused[x] = this.theInputFileReader.ReadInt32();
						}
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
		}

		public void ReadAttachments()
		{
			if (this.theMdlFileData.localAttachmentCount > 0)
			{
				long attachmentInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localAttachmentOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theAttachments = new List<SourceMdlAttachment>(this.theMdlFileData.localAttachmentCount);
				for (int i = 0; i < this.theMdlFileData.localAttachmentCount; i++)
				{
					attachmentInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlAttachment anAttachment = new SourceMdlAttachment();

					if (this.theMdlFileData.version == 10)
					{
						anAttachment.name = this.theInputFileReader.ReadChars(32);
						anAttachment.theName = new string(anAttachment.name);
						anAttachment.theName = StringClass.ConvertFromNullTerminatedOrFullLengthString(anAttachment.theName);
						anAttachment.type = this.theInputFileReader.ReadInt32();
						anAttachment.bone = this.theInputFileReader.ReadInt32();

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
					}
					else
					{
						anAttachment.nameOffset = this.theInputFileReader.ReadInt32();
						anAttachment.flags = this.theInputFileReader.ReadInt32();
						anAttachment.localBoneIndex = this.theInputFileReader.ReadInt32();
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
						for (int x = 0; x <= 7; x++)
						{
							anAttachment.unused[x] = this.theInputFileReader.ReadInt32();
						}
					}

					this.theMdlFileData.theAttachments.Add(anAttachment);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					//If anAttachment.nameOffset <> 0 Then
					//	Me.theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin)
					//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					//	anAttachment.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName")
					//	End If
					//Else
					//	anAttachment.theName = ""
					//End If
					//------
					anAttachment.theName = this.GetStringAtOffset(attachmentInputFileStreamPosition, anAttachment.nameOffset, "anAttachment.theName");

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + this.theMdlFileData.theAttachments.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment");
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
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.hitboxSetOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theHitboxSets = new List<SourceMdlHitboxSet>(this.theMdlFileData.hitboxSetCount);
				for (int i = 0; i < this.theMdlFileData.hitboxSetCount; i++)
				{
					hitboxSetInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlHitboxSet aHitboxSet = new SourceMdlHitboxSet();
					aHitboxSet.nameOffset = this.theInputFileReader.ReadInt32();
					aHitboxSet.hitboxCount = this.theInputFileReader.ReadInt32();
					aHitboxSet.hitboxOffset = this.theInputFileReader.ReadInt32();
					this.theMdlFileData.theHitboxSets.Add(aHitboxSet);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					//If aHitboxSet.nameOffset <> 0 Then
					//	Me.theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.nameOffset, SeekOrigin.Begin)
					//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					//	aHitboxSet.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitboxSet.theName")
					//	End If
					//Else
					//	aHitboxSet.theName = ""
					//End If
					//------
					aHitboxSet.theName = this.GetStringAtOffset(hitboxSetInputFileStreamPosition, aHitboxSet.nameOffset, "aHitboxSet.theName");

					this.ReadHitboxes(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, aHitboxSet);

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets " + this.theMdlFileData.theHitboxSets.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment");
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

				this.theInputFileReader.BaseStream.Seek(hitboxOffsetInputFileStreamPosition, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aHitboxSet.theHitboxes = new List<SourceMdlHitbox>(aHitboxSet.hitboxCount);
				for (int j = 0; j < aHitboxSet.hitboxCount; j++)
				{
					hitboxInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlHitbox aHitbox = new SourceMdlHitbox();

					aHitbox.boneIndex = this.theInputFileReader.ReadInt32();
					aHitbox.groupIndex = this.theInputFileReader.ReadInt32();
					aHitbox.boundingBoxMin.x = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMin.y = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMin.z = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.x = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.y = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxMax.z = this.theInputFileReader.ReadSingle();
					aHitbox.nameOffset = this.theInputFileReader.ReadInt32();
					//NOTE: Roll (z) is first.
					aHitbox.boundingBoxPitchYawRoll.z = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxPitchYawRoll.x = this.theInputFileReader.ReadSingle();
					aHitbox.boundingBoxPitchYawRoll.y = this.theInputFileReader.ReadSingle();
					//aHitbox.unknown = Me.theInputFileReader.ReadInt32()
					//For x As Integer = 0 To aHitbox.unused_VERSION49.Length - 1
					//	aHitbox.unused_VERSION49(x) = Me.theInputFileReader.ReadInt32()
					//Next
					aHitbox.unused[0] = this.theInputFileReader.ReadInt32();
					aHitbox.unused[1] = this.theInputFileReader.ReadInt32();
					aHitbox.unused[2] = this.theInputFileReader.ReadInt32();
					aHitbox.unused[3] = this.theInputFileReader.ReadInt32();
					aHitbox.unused[4] = this.theInputFileReader.ReadInt32();

					aHitboxSet.theHitboxes.Add(aHitbox);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					if (aHitbox.nameOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(hitboxInputFileStreamPosition + aHitbox.nameOffset, SeekOrigin.Begin);
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
		}

		public void ReadBoneTableByName()
		{
			if (this.theMdlFileData.boneTableByNameOffset != 0 && this.theMdlFileData.theBones != null)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.boneTableByNameOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theBoneTableByName = new List<int>(this.theMdlFileData.theBones.Count);
				byte index = 0;
				for (int i = 0; i < this.theMdlFileData.theBones.Count; i++)
				{
					index = this.theInputFileReader.ReadByte();
					this.theMdlFileData.theBoneTableByName.Add(index);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneTableByName");
			}
		}

		public void ReadLocalAnimationDescs()
		{
			long animInputFileStreamPosition = 0;
			long inputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localAnimationOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			this.theMdlFileData.theAnimationDescs = new List<SourceMdlAnimationDesc52>(this.theMdlFileData.localAnimationCount);
			for (int i = 0; i < this.theMdlFileData.localAnimationCount; i++)
			{
				animInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				SourceMdlAnimationDesc52 anAnimationDesc = new SourceMdlAnimationDesc52();

				anAnimationDesc.theOffsetStart = this.theInputFileReader.BaseStream.Position;

				anAnimationDesc.baseHeaderOffset = this.theInputFileReader.ReadInt32();
				anAnimationDesc.nameOffset = this.theInputFileReader.ReadInt32();
				anAnimationDesc.fps = this.theInputFileReader.ReadSingle();
				anAnimationDesc.flags = this.theInputFileReader.ReadInt32();
				anAnimationDesc.frameCount = this.theInputFileReader.ReadInt32();
				anAnimationDesc.movementCount = this.theInputFileReader.ReadInt32();
				anAnimationDesc.movementOffset = this.theInputFileReader.ReadInt32();

				anAnimationDesc.ikRuleZeroFrameOffset = this.theInputFileReader.ReadInt32();

				for (int x = 0; x < anAnimationDesc.unused1.Length; x++)
				{
					anAnimationDesc.unused1[x] = this.theInputFileReader.ReadInt32();
				}

				anAnimationDesc.animBlock = this.theInputFileReader.ReadInt32();
				anAnimationDesc.animOffset = this.theInputFileReader.ReadInt32();
				anAnimationDesc.ikRuleCount = this.theInputFileReader.ReadInt32();
				anAnimationDesc.ikRuleOffset = this.theInputFileReader.ReadInt32();
				anAnimationDesc.animblockIkRuleOffset = this.theInputFileReader.ReadInt32();
				anAnimationDesc.localHierarchyCount = this.theInputFileReader.ReadInt32();
				anAnimationDesc.localHierarchyOffset = this.theInputFileReader.ReadInt32();
				anAnimationDesc.sectionOffset = this.theInputFileReader.ReadInt32();
				anAnimationDesc.sectionFrameCount = this.theInputFileReader.ReadInt32();

				anAnimationDesc.spanFrameCount = this.theInputFileReader.ReadInt16();
				anAnimationDesc.spanCount = this.theInputFileReader.ReadInt16();
				//anAnimationDesc.spanOffset = Me.theInputFileReader.ReadInt32()
				//anAnimationDesc.spanStallTime = Me.theInputFileReader.ReadSingle()

				this.theMdlFileData.theAnimationDescs.Add(anAnimationDesc);

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				this.ReadAnimationDescName(animInputFileStreamPosition, anAnimationDesc);
				//Me.ReadAnimationDescSpanData(animInputFileStreamPosition, anAnimationDesc)

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs " + this.theMdlFileData.theAnimationDescs.Count.ToString());
		}

		public void ReadAnimationSections()
		{
			if (this.theMdlFileData.theAnimationDescs != null)
			{
				foreach (SourceMdlAnimationDesc52 anAnimationDesc in this.theMdlFileData.theAnimationDescs)
				{
					if (anAnimationDesc.sectionOffset != 0 && anAnimationDesc.sectionFrameCount > 0)
					{
						int sectionCount;

						//FROM: simplify.cpp:
						//      panim->numsections = (int)(panim->numframes / panim->sectionframes) + 2;
						//NOTE: It is unclear why "+ 2" is used in studiomdl.
						sectionCount = Convert.ToInt32(Math.Truncate(anAnimationDesc.frameCount / (double)anAnimationDesc.sectionFrameCount)) + 2;

						long offset = anAnimationDesc.theOffsetStart + anAnimationDesc.sectionOffset;
						if (offset != this.theInputFileReader.BaseStream.Position)
						{
							//TODO: It looks like more than one animDesc can point to same sections, so need to revise how this is done.
							//Me.theMdlFileData.theFileSeekLog.Add(Me.theInputFileReader.BaseStream.Position, Me.theInputFileReader.BaseStream.Position, "[ERROR] anAnimationDesc.theSections [" + anAnimationDesc.theName + "] offset mismatch: pos = " + Me.theInputFileReader.BaseStream.Position.ToString() + " offset = " + offset.ToString())
							this.theInputFileReader.BaseStream.Seek(offset, SeekOrigin.Begin);
						}

						anAnimationDesc.theSections = new List<SourceMdlAnimationSection>(sectionCount);
						for (int sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++)
						{
							this.ReadMdlAnimationSection(this.theInputFileReader.BaseStream.Position, anAnimationDesc, this.theMdlFileData.theFileSeekLog);
						}
					}
				}
			}
		}

		public void ReadAnimationMdlBlocks()
		{
			if (this.theMdlFileData.theAnimationDescs != null)
			{
				long animInputFileStreamPosition = 0;
				SourceMdlAnimationDesc52 anAnimationDesc = null;
				List<SourceMdlAnimation> aSectionOfAnimation = null;
				SourceAniFrameAnim52 aSectionOfFrameAnimation = null;

//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionIndex = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionFrameCount = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionCount = 0;
				long adjustedAnimOffset = 0;
				for (int anAnimDescIndex = 0; anAnimDescIndex < this.theMdlFileData.theAnimationDescs.Count; anAnimDescIndex++)
				{
					anAnimationDesc = this.theMdlFileData.theAnimationDescs[anAnimDescIndex];

					animInputFileStreamPosition = anAnimationDesc.theOffsetStart;

					if (this.theMdlFileData.theFirstAnimationDesc == null && anAnimationDesc.theName[0] != '@')
					{
						this.theMdlFileData.theFirstAnimationDesc = anAnimationDesc;
					}

					if ((anAnimationDesc.flags & SourceMdlAnimationDesc.STUDIO_FRAMEANIM) != 0)
					{
						anAnimationDesc.theSectionsOfFrameAnim = new List<SourceAniFrameAnim52>();
						aSectionOfFrameAnimation = new SourceAniFrameAnim52();
						anAnimationDesc.theSectionsOfFrameAnim.Add(aSectionOfFrameAnimation);

	//					Dim sectionIndex As Integer
						if (anAnimationDesc.sectionOffset != 0 && anAnimationDesc.sectionFrameCount > 0)
						{
	//						Dim sectionFrameCount As Integer
	//						Dim sectionCount As Integer

							//TODO: Shouldn't this be set to largest sectionFrameCount?
							this.theMdlFileData.theSectionFrameCount = anAnimationDesc.sectionFrameCount;
							if (this.theMdlFileData.theSectionFrameMinFrameCount >= anAnimationDesc.frameCount)
							{
								this.theMdlFileData.theSectionFrameMinFrameCount = anAnimationDesc.frameCount - 1;
							}

							//'FROM: simplify.cpp:
							//'      panim->numsections = (int)(panim->numframes / panim->sectionframes) + 2;
							//'NOTE: It is unclear why "+ 2" is used in studiomdl.
							sectionCount = Convert.ToInt32(Math.Truncate(anAnimationDesc.frameCount / (double)anAnimationDesc.sectionFrameCount)) + 2;

							//NOTE: First sectionOfAnimation was created above.
							for (sectionIndex = 1; sectionIndex < sectionCount; sectionIndex++)
							{
								aSectionOfFrameAnimation = new SourceAniFrameAnim52();
								anAnimationDesc.theSectionsOfFrameAnim.Add(aSectionOfFrameAnimation);
							}

							//TODO: Is this "if" check correct? Should it be removed? 
							//      Maybe when there are sections, the section.animBlock determines which file the data is in.
							if (anAnimationDesc.animBlock == 0)
							{
								for (sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++)
								{
									if (anAnimationDesc.theSections[sectionIndex].animBlock == 0)
									{
										aSectionOfFrameAnimation = anAnimationDesc.theSectionsOfFrameAnim[sectionIndex];

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

										this.ReadAnimationFrameByBone(animInputFileStreamPosition + anAnimationDesc.theSections[sectionIndex].animOffset, anAnimationDesc, sectionFrameCount, sectionIndex, (sectionIndex >= sectionCount - 2) || (anAnimationDesc.frameCount == (sectionIndex + 1) * anAnimationDesc.sectionFrameCount));
									}
								}
							}
						}
						else if (anAnimationDesc.animBlock == 0)
						{
							//NOTE: This code is reached by L4D2's pak01_dir.vpk\models\v_models\v_huntingrifle.mdl.
							sectionIndex = 0;
							this.ReadAnimationFrameByBone(animInputFileStreamPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex, true);
						}
					}
					else
					{
						anAnimationDesc.theSectionsOfAnimations = new List<List<SourceMdlAnimation>>();
						aSectionOfAnimation = new List<SourceMdlAnimation>();
						anAnimationDesc.theSectionsOfAnimations.Add(aSectionOfAnimation);

						if (anAnimationDesc.sectionOffset != 0 && anAnimationDesc.sectionFrameCount > 0)
						{
	//						Dim sectionFrameCount As Integer
	//						Dim sectionCount As Integer

							//TODO: Shouldn't this be set to largest sectionFrameCount?
							this.theMdlFileData.theSectionFrameCount = anAnimationDesc.sectionFrameCount;
							if (this.theMdlFileData.theSectionFrameMinFrameCount >= anAnimationDesc.frameCount)
							{
								this.theMdlFileData.theSectionFrameMinFrameCount = anAnimationDesc.frameCount - 1;
							}

							//'FROM: simplify.cpp:
							//'      panim->numsections = (int)(panim->numframes / panim->sectionframes) + 2;
							//'NOTE: It is unclear why "+ 2" is used in studiomdl.
							sectionCount = Convert.ToInt32(Math.Truncate(anAnimationDesc.frameCount / (double)anAnimationDesc.sectionFrameCount)) + 2;

							//NOTE: First sectionOfAnimation was created above.
							for (int sectionIndex2 = 1; sectionIndex2 < sectionCount; sectionIndex2++)
							{
								aSectionOfAnimation = new List<SourceMdlAnimation>();
								anAnimationDesc.theSectionsOfAnimations.Add(aSectionOfAnimation);
							}

	//						Dim adjustedAnimOffset As Long
							if (anAnimationDesc.animBlock == 0)
							{
								for (int sectionIndex2 = 0; sectionIndex2 < sectionCount; sectionIndex2++)
								{
									if (anAnimationDesc.theSections[sectionIndex2].animBlock == 0)
									{
										//NOTE: This is weird, but it fits with a few oddball models (such as L4D2 "left4dead2\ghostanim.mdl") while not messing up the normal ones.
										adjustedAnimOffset = anAnimationDesc.theSections[sectionIndex2].animOffset + (anAnimationDesc.animOffset - anAnimationDesc.theSections[0].animOffset);

										aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations[sectionIndex2];

										if (sectionIndex2 < sectionCount - 2)
										{
											sectionFrameCount = anAnimationDesc.sectionFrameCount;
										}
										else
										{
											//NOTE: Due to the weird calculation of sectionCount in studiomdl, this line is called twice, which means there are two "last" sections.
											//      This also likely means that the last section is bogus unused data.
											sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 2) * anAnimationDesc.sectionFrameCount);
										}

										this.ReadMdlAnimation(animInputFileStreamPosition + adjustedAnimOffset, anAnimationDesc, sectionFrameCount, aSectionOfAnimation, (sectionIndex2 >= sectionCount - 2) || (anAnimationDesc.frameCount == (sectionIndex2 + 1) * anAnimationDesc.sectionFrameCount));
									}
								}
							}
						}
						else if (anAnimationDesc.animBlock == 0)
						{
							this.ReadMdlAnimation(animInputFileStreamPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, anAnimationDesc.theSectionsOfAnimations[0], true);
						}
					}

					if (anAnimationDesc.animBlock == 0)
					{
						this.ReadMdlIkRules(animInputFileStreamPosition, anAnimationDesc);
						this.ReadLocalHierarchies(animInputFileStreamPosition, anAnimationDesc);
					}

					this.ReadMdlMovements(animInputFileStreamPosition, anAnimationDesc);
				}
			}
		}

		protected void ReadAnimationDescName(long animInputFileStreamPosition, SourceMdlAnimationDesc52 anAnimationDesc)
		{
			if (anAnimationDesc.nameOffset != 0)
			{
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				this.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				anAnimationDesc.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);
				//If anAnimDesc.theName(0) = "@" Then
				//	anAnimDesc.theName = anAnimDesc.theName.Remove(0, 1)
				//End If

				//NOTE: This naming is found in Garry's Mod garrysmod_dir.vpk "\models\m_anm.mdl":  "a_../combine_soldier_xsi/Hold_AR2_base.smd"
				if (anAnimationDesc.theName.StartsWith("a_../") || anAnimationDesc.theName.StartsWith("a_..\\"))
				{
					anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 5);
					anAnimationDesc.theName = Path.Combine(FileManager.GetPath(anAnimationDesc.theName), "a_" + Path.GetFileName(anAnimationDesc.theName));
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theName = " + anAnimationDesc.theName);
			}
			else
			{
				anAnimationDesc.theName = "";
			}
		}

		protected long ReadAnimationDescSpanData(long animInputFileStreamPosition, SourceMdlAnimationDesc52 anAnimationDesc)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			// The data seems to be copy of first several anim frames in the ANI file.
			if (anAnimationDesc.spanFrameCount != 0 || anAnimationDesc.spanCount != 0 || anAnimationDesc.spanOffset != 0 || anAnimationDesc.spanStallTime != 0)
			{
				//NOTE: This code is reached by L4D2's pak01_dir.vpk\models\v_models\v_huntingrifle.mdl and v_snip_awp.mdl.
				//NOTE: This code is reached by DoI's doi_models_dir_vpk\models\weapons\v_g43.mdl and v_vickers.mdl.
				fileOffsetStart = animInputFileStreamPosition + anAnimationDesc.spanOffset;
				fileOffsetEnd = animInputFileStreamPosition + anAnimationDesc.spanOffset - 1;
				SourceMdlBone aBone = null;
				for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
				{
					aBone = this.theMdlFileData.theBones[boneIndex];
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
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.spanOffset (zeroframes/saveframes) [" + anAnimationDesc.theName + "] [spanFrameCount = " + anAnimationDesc.spanFrameCount.ToString() + "] [spanCount = " + anAnimationDesc.spanCount.ToString() + "]");

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

		//TODO: Should this be the same as SourceAniFile49.ReadAniAnimation()?
		//Protected Sub ReadAnimationFrameByBone(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc52, ByVal sectionFrameCount As Integer, ByVal aSectionOfAnimation As List(Of SourceMdlAnimation))
		//	Dim animationInputFileStreamPosition As Long
		//	'Dim inputFileStreamPosition As Long
		//	'Dim fileOffsetStart As Long
		//	'Dim fileOffsetEnd As Long
		//	'Dim fileOffsetStart2 As Long
		//	'Dim fileOffsetEnd2 As Long
		//	Dim boneCount As Integer

		//	Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin)
		//	'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//	For frameIndex As Integer = 0 To sectionFrameCount - 1
		//		animationInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//		'Dim anAnimation As New SourceMdlAnimation()

		//		'boneCount = Me.theMdlFileData.theBones.Count
		//		'For boneIndex As Integer = 0 To boneCount - 1
		//		'	anAnimation.flags = Me.theInputFileReader.ReadByte()
		//		'Next

		//		'aSectionOfAnimation.Add(anAnimation)

		//		''inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//		''Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

		//		Me.theMdlFileData.theFileSeekLog.Add(animationInputFileStreamPosition, Me.theInputFileReader.BaseStream.Position - 1, "anAnimationDesc.anAnimation [ReadAnimationFrameByBone()] (frameCount = " + CStr(anAnimationDesc.frameCount) + "; sectionFrameCount = " + CStr(sectionFrameCount) + ")")
		//	Next

		//	'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.anAnimation [ReadAnimationFrameByBone()] alignment")
		//End Sub
		//======
		protected void ReadAnimationFrameByBone(long animInputFileStreamPosition, SourceMdlAnimationDesc52 anAnimationDesc, int sectionFrameCount, int sectionIndex, bool lastSectionIsBeingRead)
		{
			this.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin);

			long animFrameInputFileStreamPosition = 0;
			long boneFrameDataStartInputFileStreamPosition = 0;
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			int boneCount = 0;
			byte boneFlag = 0;
			BoneConstantInfo49 aBoneConstantInfo = null;
			List<BoneFrameDataInfo49> aBoneFrameDataInfoList = null;
			BoneFrameDataInfo49 aBoneFrameDataInfo = null;

			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			SourceAniFrameAnim52 aSectionOfAnimation = anAnimationDesc.theSectionsOfFrameAnim[sectionIndex];

			boneCount = this.theMdlFileData.theBones.Count;
			try
			{
				animFrameInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aSectionOfAnimation.constantsOffset = this.theInputFileReader.ReadInt32();
				aSectionOfAnimation.frameOffset = this.theInputFileReader.ReadInt32();
				aSectionOfAnimation.frameLength = this.theInputFileReader.ReadInt32();
				for (int x = 0; x < aSectionOfAnimation.unused.Length; x++)
				{
					aSectionOfAnimation.unused[x] = this.theInputFileReader.ReadInt32();
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.aSectionOfAnimation [" + anAnimationDesc.theName + "] (frameCount = " + anAnimationDesc.frameCount.ToString() + "; sectionFrameCount = " + sectionFrameCount.ToString() + ")");

				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aSectionOfAnimation.theBoneFlags = new List<byte>(boneCount);
				for (int boneIndex = 0; boneIndex < boneCount; boneIndex++)
				{
					boneFlag = this.theInputFileReader.ReadByte();
					aSectionOfAnimation.theBoneFlags.Add(boneFlag);

					//DEBUG:
					if ((boneFlag & 0x20) > 0)
					{
						//TODO: Titanfall models get here.
						int unknownFlagIsUsed = 4242;
					}
					if (boneFlag > 0xFF)
					{
						int unknownFlagIsUsed = 4242;
					}
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSectionOfAnimation.theBoneFlags " + aSectionOfAnimation.theBoneFlags.Count.ToString());
				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aSectionOfAnimation.theBoneFlags alignment");

				if (aSectionOfAnimation.constantsOffset != 0)
				{
					this.theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + aSectionOfAnimation.constantsOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					aSectionOfAnimation.theBoneConstantInfos = new List<BoneConstantInfo49>(boneCount);
					for (int boneIndex = 0; boneIndex < boneCount; boneIndex++)
					{
						aBoneConstantInfo = new BoneConstantInfo49();
						aSectionOfAnimation.theBoneConstantInfos.Add(aBoneConstantInfo);

						boneFlag = aSectionOfAnimation.theBoneFlags[boneIndex];
						if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_CONST_ROT2) > 0)
						{
							aBoneConstantInfo.theConstantRotationUnknown = new SourceQuaternion48bitsViaBytes();
							aBoneConstantInfo.theConstantRotationUnknown.theBytes = this.theInputFileReader.ReadBytes(6);
						}
						if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_RAWROT) > 0)
						{
							aBoneConstantInfo.theConstantRawRot = new SourceQuaternion48bits();
							aBoneConstantInfo.theConstantRawRot.theXInput = this.theInputFileReader.ReadUInt16();
							aBoneConstantInfo.theConstantRawRot.theYInput = this.theInputFileReader.ReadUInt16();
							aBoneConstantInfo.theConstantRawRot.theZWInput = this.theInputFileReader.ReadUInt16();
						}
						if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_RAWPOS) > 0)
						{
							aBoneConstantInfo.theConstantRawPos = new SourceVector48bits();
							aBoneConstantInfo.theConstantRawPos.theXInput.the16BitValue = this.theInputFileReader.ReadUInt16();
							aBoneConstantInfo.theConstantRawPos.theYInput.the16BitValue = this.theInputFileReader.ReadUInt16();
							aBoneConstantInfo.theConstantRawPos.theZInput.the16BitValue = this.theInputFileReader.ReadUInt16();
						}
					}

					if (this.theInputFileReader.BaseStream.Position > fileOffsetStart)
					{
						fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSectionOfAnimation.theBoneConstantInfos " + aSectionOfAnimation.theBoneConstantInfos.Count.ToString());
						this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aSectionOfAnimation.theBoneConstantInfos alignment");
					}
				}

				if (aSectionOfAnimation.frameOffset != 0)
				{
					this.theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + aSectionOfAnimation.frameOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

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

					for (int frameIndex = 0; frameIndex < sectionFrameCount; frameIndex++)
					{
						aBoneFrameDataInfoList = new List<BoneFrameDataInfo49>(boneCount);
						if (lastSectionIsBeingRead || (frameIndex < (adjustedFrameCount - 1)))
						{
							aSectionOfAnimation.theBoneFrameDataInfos.Add(aBoneFrameDataInfoList);
						}

						boneFrameDataStartInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						for (int boneIndex = 0; boneIndex < boneCount; boneIndex++)
						{
							aBoneFrameDataInfo = new BoneFrameDataInfo49();
							aBoneFrameDataInfoList.Add(aBoneFrameDataInfo);

							boneFlag = aSectionOfAnimation.theBoneFlags[boneIndex];

							if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIM_ROT2) > 0)
							{
								aBoneFrameDataInfo.theAnimRotationUnknown = new SourceQuaternion48bitsViaBytes();
								aBoneFrameDataInfo.theAnimRotationUnknown.theBytes = this.theInputFileReader.ReadBytes(6);
							}
							//If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIMROT) > 0 Then
							if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_FULLANIMPOS) > 0)
							{
								aBoneFrameDataInfo.theAnimRotation = new SourceQuaternion48bits();
								aBoneFrameDataInfo.theAnimRotation.theXInput = this.theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimRotation.theYInput = this.theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimRotation.theZWInput = this.theInputFileReader.ReadUInt16();
							}
							if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIMPOS) > 0)
							{
								aBoneFrameDataInfo.theAnimPosition = new SourceVector48bits();
								aBoneFrameDataInfo.theAnimPosition.theXInput.the16BitValue = this.theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimPosition.theYInput.the16BitValue = this.theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimPosition.theZInput.the16BitValue = this.theInputFileReader.ReadUInt16();
							}
							//If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_FULLANIMPOS) > 0 Then
							if ((boneFlag & SourceAniFrameAnim49.STUDIO_FRAME_ANIMROT) > 0)
							{
								//aBoneFrameDataInfo.theFullAnimPosition = New SourceVector()
								//aBoneFrameDataInfo.theFullAnimPosition.x = Me.theInputFileReader.ReadSingle()
								//aBoneFrameDataInfo.theFullAnimPosition.y = Me.theInputFileReader.ReadSingle()
								//aBoneFrameDataInfo.theFullAnimPosition.z = Me.theInputFileReader.ReadSingle()
								aBoneFrameDataInfo.theAnimPosition = new SourceVector48bits();
								aBoneFrameDataInfo.theAnimPosition.theXInput.the16BitValue = this.theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimPosition.theYInput.the16BitValue = this.theInputFileReader.ReadUInt16();
								aBoneFrameDataInfo.theAnimPosition.theZInput.the16BitValue = this.theInputFileReader.ReadUInt16();
							}
						}

						//DEBUG: Check frame data length for debugging.
						if ((aSectionOfAnimation.frameLength) != (this.theInputFileReader.BaseStream.Position - boneFrameDataStartInputFileStreamPosition))
						{
							int somethingIsWrong = 4242;
						}
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					string text = "aSectionOfAnimation.theBoneFrameDataInfos " + aSectionOfAnimation.theBoneFrameDataInfos.Count.ToString();
					if (!lastSectionIsBeingRead)
					{
						text += " plus an extra unused aBoneFrameDataInfo";
					}
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, text);
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aSectionOfAnimation.theBoneFrameDataInfos alignment");
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		protected void ReadMdlAnimation(long animInputFileStreamPosition, SourceMdlAnimationDesc52 anAnimationDesc, int sectionFrameCount, List<SourceMdlAnimation> aSectionOfAnimation, bool lastSectionIsBeingRead)
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

			this.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin);

			if (this.theMdlFileData.theBones == null)
			{
				boneCount = 1;
			}
			else
			{
				boneCount = this.theMdlFileData.theBones.Count;
			}
			for (int j = 0; j < boneCount; j++)
			{
				animationInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				boneIndex = this.theInputFileReader.ReadByte();
				if (boneIndex == 255)
				{
					this.theInputFileReader.ReadByte();
					this.theInputFileReader.ReadInt16();

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(animationInputFileStreamPosition, fileOffsetEnd, "anAnimationDesc.anAnimation (boneIndex = 255)");

					//Continue For
					break;
				}
				//DEBUG:
				if (boneIndex >= boneCount)
				{
					int badIfGetsHere = 42;
					break;
				}

				anAnimation = new SourceMdlAnimation();
				aSectionOfAnimation.Add(anAnimation);

				anAnimation.boneIndex = boneIndex;
				anAnimation.flags = this.theInputFileReader.ReadByte();
				anAnimation.nextSourceMdlAnimationOffset = this.theInputFileReader.ReadInt16();

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
					anAnimation.theRot64bits.theBytes = this.theInputFileReader.ReadBytes(8);

					//Me.DebugQuaternion(anAnimation.theRot64)
				}
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0)
				{
					anAnimation.theRot48bits = new SourceQuaternion48bits();
					anAnimation.theRot48bits.theXInput = this.theInputFileReader.ReadUInt16();
					anAnimation.theRot48bits.theYInput = this.theInputFileReader.ReadUInt16();
					anAnimation.theRot48bits.theZWInput = this.theInputFileReader.ReadUInt16();
				}
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0)
				{
					anAnimation.thePos = new SourceVector48bits();
					anAnimation.thePos.theXInput.the16BitValue = this.theInputFileReader.ReadUInt16();
					anAnimation.thePos.theYInput.the16BitValue = this.theInputFileReader.ReadUInt16();
					anAnimation.thePos.theZInput.the16BitValue = this.theInputFileReader.ReadUInt16();
				}

				animValuePointerInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				// First, read both sets of offsets.
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0)
				{
					rotValuePointerInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					anAnimation.theRotV = new SourceMdlAnimationValuePointer();

					anAnimation.theRotV.animXValueOffset = this.theInputFileReader.ReadInt16();
					if (anAnimation.theRotV.theAnimXValues == null)
					{
						anAnimation.theRotV.theAnimXValues = new List<SourceMdlAnimationValue>();
					}

					anAnimation.theRotV.animYValueOffset = this.theInputFileReader.ReadInt16();
					if (anAnimation.theRotV.theAnimYValues == null)
					{
						anAnimation.theRotV.theAnimYValues = new List<SourceMdlAnimationValue>();
					}

					anAnimation.theRotV.animZValueOffset = this.theInputFileReader.ReadInt16();
					if (anAnimation.theRotV.theAnimZValues == null)
					{
						anAnimation.theRotV.theAnimZValues = new List<SourceMdlAnimationValue>();
					}
				}
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0)
				{
					posValuePointerInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					anAnimation.thePosV = new SourceMdlAnimationValuePointer();

					anAnimation.thePosV.animXValueOffset = this.theInputFileReader.ReadInt16();
					if (anAnimation.thePosV.theAnimXValues == null)
					{
						anAnimation.thePosV.theAnimXValues = new List<SourceMdlAnimationValue>();
					}

					anAnimation.thePosV.animYValueOffset = this.theInputFileReader.ReadInt16();
					if (anAnimation.thePosV.theAnimYValues == null)
					{
						anAnimation.thePosV.theAnimYValues = new List<SourceMdlAnimationValue>();
					}

					anAnimation.thePosV.animZValueOffset = this.theInputFileReader.ReadInt16();
					if (anAnimation.thePosV.theAnimZValues == null)
					{
						anAnimation.thePosV.theAnimZValues = new List<SourceMdlAnimationValue>();
					}
				}

				this.theMdlFileData.theFileSeekLog.Add(animationInputFileStreamPosition, this.theInputFileReader.BaseStream.Position - 1, "anAnimationDesc.anAnimation (frameCount = " + anAnimationDesc.frameCount.ToString() + "; sectionFrameCount = " + sectionFrameCount.ToString() + ")");

				// Second, read the anim values using the offsets.
				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0)
				{
					if (anAnimation.theRotV.animXValueOffset > 0)
					{
						this.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animXValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimXValues, "anAnimation.theRotV.theAnimXValues");
					}
					if (anAnimation.theRotV.animYValueOffset > 0)
					{
						this.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animYValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimYValues, "anAnimation.theRotV.theAnimYValues");
					}
					if (anAnimation.theRotV.animZValueOffset > 0)
					{
						this.ReadMdlAnimValues(rotValuePointerInputFileStreamPosition + anAnimation.theRotV.animZValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.theRotV.theAnimZValues, "anAnimation.theRotV.theAnimZValues");
					}
				}
				if ((anAnimation.flags & SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0)
				{
					if (anAnimation.thePosV.animXValueOffset > 0)
					{
						this.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animXValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimXValues, "anAnimation.thePosV.theAnimXValues");
					}
					if (anAnimation.thePosV.animYValueOffset > 0)
					{
						this.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animYValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimYValues, "anAnimation.thePosV.theAnimYValues");
					}
					if (anAnimation.thePosV.animZValueOffset > 0)
					{
						this.ReadMdlAnimValues(posValuePointerInputFileStreamPosition + anAnimation.thePosV.animZValueOffset, sectionFrameCount, lastSectionIsBeingRead, anAnimation.thePosV.theAnimZValues, "anAnimation.thePosV.theAnimZValues");
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
					//'TEST: Use this with ANI file, so see if it extracts better.
					//nextAnimationInputFileStreamPosition = animationInputFileStreamPosition + CType(anAnimation.nextSourceMdlAnimationOffset, UShort)
					if (nextAnimationInputFileStreamPosition < this.theInputFileReader.BaseStream.Position)
					{
						//PROBLEM! Should not be going backwards in file.
						int i = 42;
						break;
					}
					else if (nextAnimationInputFileStreamPosition > this.theInputFileReader.BaseStream.Position)
					{
						//PROBLEM! Should not be skipping ahead. Crowbar has skipped some data, but continue decompiling.
						int i = 42;
					}

					this.theInputFileReader.BaseStream.Seek(nextAnimationInputFileStreamPosition, SeekOrigin.Begin);
				}
			}

			if (boneIndex != 255)
			{
				//NOTE: There is always an unused empty data structure at the end of the list.
				//prevanim					= destanim;
				//destanim->nextoffset		= pData - (byte *)destanim;
				//destanim					= (mstudioanim_t *)pData;
				//pData						+= sizeof( *destanim );
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;
				this.theInputFileReader.ReadByte();
				this.theInputFileReader.ReadByte();
				this.theInputFileReader.ReadInt16();

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.anAnimation [" + anAnimationDesc.theName + "] (unused empty data structure at the end of the list)");
			}

			this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.anAnimation [" + anAnimationDesc.theName + "] alignment");
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

			this.theInputFileReader.BaseStream.Seek(animValuesInputFileStreamPosition, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			frameCountRemainingToBeChecked = frameCount;
			accumulatedTotal = 0;
			while (frameCountRemainingToBeChecked > 0)
			{
				animValue.value = this.theInputFileReader.ReadInt16();
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
					animValue.value = this.theInputFileReader.ReadInt16();
					theAnimValues.Add(animValue);
				}
			}
			if (!lastSectionIsBeingRead && accumulatedTotal == frameCount)
			{
				this.theInputFileReader.ReadInt16();
				this.theInputFileReader.ReadInt16();
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, debugDescription + " (accumulatedTotal = " + accumulatedTotal.ToString() + ")");
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

		protected void ReadMdlIkRules(long animInputFileStreamPosition, SourceMdlAnimationDesc52 anAnimationDesc)
		{
			if (anAnimationDesc.ikRuleCount > 0)
			{
				long ikRuleInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;
				long fileOffsetEndOfIkRuleExtraData = 0;
				long fileOffsetOfLastEndOfIkRuleExtraData = 0;


				if (anAnimationDesc.animBlock > 0 && anAnimationDesc.animblockIkRuleOffset == 0)
				{
					//Return 0
				}
				else
				{
					this.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin);
				}
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				fileOffsetOfLastEndOfIkRuleExtraData = 0;

				anAnimationDesc.theIkRules = new List<SourceMdlIkRule>(anAnimationDesc.ikRuleCount);
				for (int ikRuleIndex = 0; ikRuleIndex < anAnimationDesc.ikRuleCount; ikRuleIndex++)
				{
					ikRuleInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlIkRule anIkRule = new SourceMdlIkRule();

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

					anIkRule.compressedIkErrorOffset = this.theInputFileReader.ReadInt32();
					anIkRule.unused2 = this.theInputFileReader.ReadInt32();
					anIkRule.ikErrorIndexStart = this.theInputFileReader.ReadInt32();
					anIkRule.ikErrorOffset = this.theInputFileReader.ReadInt32();

					anIkRule.influenceStart = this.theInputFileReader.ReadSingle();
					anIkRule.influencePeak = this.theInputFileReader.ReadSingle();
					anIkRule.influenceTail = this.theInputFileReader.ReadSingle();
					anIkRule.influenceEnd = this.theInputFileReader.ReadSingle();

					anIkRule.unused3 = this.theInputFileReader.ReadSingle();
					anIkRule.contact = this.theInputFileReader.ReadSingle();
					anIkRule.drop = this.theInputFileReader.ReadSingle();
					anIkRule.top = this.theInputFileReader.ReadSingle();

					anIkRule.unused6 = this.theInputFileReader.ReadInt32();
					anIkRule.unused7 = this.theInputFileReader.ReadInt32();
					anIkRule.unused8 = this.theInputFileReader.ReadInt32();

					anIkRule.attachmentNameOffset = this.theInputFileReader.ReadInt32();

					for (int x = 0; x < anIkRule.unused.Length; x++)
					{
						anIkRule.unused[x] = this.theInputFileReader.ReadInt32();
					}

					anAnimationDesc.theIkRules.Add(anIkRule);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					fileOffsetEndOfIkRuleExtraData = 0;

					if (anIkRule.attachmentNameOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.attachmentNameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

						anIkRule.theAttachmentName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

						fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
						//If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkRule.theAttachmentName = " + anIkRule.theAttachmentName);
						//End If

						//'TODO: Probably should change this from (fileOffsetStart2 + 127) to (Me.theInputFileReader.BaseStream.Position - 1).
						//If fileOffsetEndOfIkRuleExtraData < (fileOffsetStart2 + 127) Then
						//	fileOffsetEndOfIkRuleExtraData = (fileOffsetStart2 + 127)
						//End If
					}
					else
					{
						anIkRule.theAttachmentName = "";
					}

					if (anIkRule.compressedIkErrorOffset != 0)
					{
						long compressedIkErrorsEndOffset = this.ReadCompressedIkErrors(ikRuleInputFileStreamPosition, ikRuleIndex, anAnimationDesc);


						if (fileOffsetEndOfIkRuleExtraData < compressedIkErrorsEndOffset)
						{
							fileOffsetEndOfIkRuleExtraData = compressedIkErrorsEndOffset;
						}
					}

					if (anIkRule.ikErrorOffset != 0)
					{
						int debug = 4242;
					}

					if (fileOffsetEndOfIkRuleExtraData > 0)
					{
						this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEndOfIkRuleExtraData, 4, "anIkRule extra-data alignment");
						fileOffsetOfLastEndOfIkRuleExtraData = this.theInputFileReader.BaseStream.Position - 1;
					}

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				string description = "anAnimationDesc.theIkRules " + anAnimationDesc.theIkRules.Count.ToString();
				if (anAnimationDesc.animBlock > 0 && anAnimationDesc.animblockIkRuleOffset == 0)
				{
					description += "   [animblockIkRuleOffset = 0]";
				}
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, description);

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment");

				//If fileOffsetOfLastEndOfIkRuleExtraData > 0 Then
				//	Return fileOffsetOfLastEndOfIkRuleExtraData
				//Else
				//	Return Me.theInputFileReader.BaseStream.Position - 1
				//End If
			}
		}

		private long ReadCompressedIkErrors(long ikRuleInputFileStreamPosition, int ikRuleIndex, SourceMdlAnimationDesc52 anAnimationDesc)
		{
			SourceMdlIkRule anIkRule = anAnimationDesc.theIkRules[ikRuleIndex];

			long compressedIkErrorInputFileStreamPosition = 0;
			long kInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			this.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.compressedIkErrorOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;
			compressedIkErrorInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

			anIkRule.theCompressedIkError = new SourceMdlCompressedIkError();

			// First, read the scale data.
			for (int k = 0; k < anIkRule.theCompressedIkError.scale.Length; k++)
			{
				kInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				anIkRule.theCompressedIkError.scale[k] = this.theInputFileReader.ReadSingle();

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(kInputFileStreamPosition, fileOffsetEnd, "anIkRule.theCompressedIkError [ikRuleIndex = " + ikRuleIndex.ToString() + "] [scale = " + anIkRule.theCompressedIkError.scale[k].ToString() + "]");
			}

			// Second, read the offset data.
			for (int k = 0; k < anIkRule.theCompressedIkError.offset.Length; k++)
			{
				kInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				anIkRule.theCompressedIkError.offset[k] = this.theInputFileReader.ReadInt16();

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(kInputFileStreamPosition, fileOffsetEnd, "anIkRule.theCompressedIkError [ikRuleIndex = " + ikRuleIndex.ToString() + "] [offset = " + anIkRule.theCompressedIkError.offset[k].ToString() + "]");
			}

			//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkRule.theCompressedIkError (scale and offset data)")

			//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			// Third, read the anim values.
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
					this.ReadMdlAnimValues(compressedIkErrorInputFileStreamPosition + anIkRule.theCompressedIkError.offset[k], anAnimationDesc.frameCount, true, anIkRule.theCompressedIkError.theAnimValues[k], "anIkRule.theCompressedIkError.theAnimValues(" + k.ToString() + ")");
				}
			}

			//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

			//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkRule.theCompressedIkError ")

			return this.theInputFileReader.BaseStream.Position - 1;
		}

		protected void ReadMdlAnimationSection(long animInputFileStreamPosition, SourceMdlAnimationDesc52 anAnimationDesc, FileSeekLog aFileSeekLog)
		{
			//Dim animSectionInputFileStreamPosition As Long
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			fileOffsetStart = animInputFileStreamPosition;

			//animSectionInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			SourceMdlAnimationSection anAnimSection = new SourceMdlAnimationSection();
			anAnimSection.animBlock = this.theInputFileReader.ReadInt32();
			anAnimSection.animOffset = this.theInputFileReader.ReadInt32();
			anAnimationDesc.theSections.Add(anAnimSection);

			//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			aFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theSections");
		}

		protected void ReadMdlMovements(long animInputFileStreamPosition, SourceMdlAnimationDesc52 anAnimationDesc)
		{
			if (anAnimationDesc.movementCount > 0)
			{
				long movementInputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

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
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theMovements");
			}
		}

		protected void ReadLocalHierarchies(long animInputFileStreamPosition, SourceMdlAnimationDesc52 anAnimationDesc)
		{
			if (anAnimationDesc.localHierarchyCount > 0)
			{
				long localHieararchyInputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				this.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.localHierarchyOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				anAnimationDesc.theLocalHierarchies = new List<SourceMdlLocalHierarchy>(anAnimationDesc.localHierarchyCount);
				for (int j = 0; j < anAnimationDesc.localHierarchyCount; j++)
				{
					localHieararchyInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlLocalHierarchy aLocalHierarchy = new SourceMdlLocalHierarchy();

					aLocalHierarchy.boneIndex = this.theInputFileReader.ReadInt32();
					aLocalHierarchy.boneNewParentIndex = this.theInputFileReader.ReadInt32();
					aLocalHierarchy.startInfluence = this.theInputFileReader.ReadSingle();
					aLocalHierarchy.peakInfluence = this.theInputFileReader.ReadSingle();
					aLocalHierarchy.tailInfluence = this.theInputFileReader.ReadSingle();
					aLocalHierarchy.endInfluence = this.theInputFileReader.ReadSingle();
					aLocalHierarchy.startFrameIndex = this.theInputFileReader.ReadInt32();
					aLocalHierarchy.localAnimOffset = this.theInputFileReader.ReadInt32();
					for (int x = 0; x < aLocalHierarchy.unused.Length; x++)
					{
						aLocalHierarchy.unused[x] = this.theInputFileReader.ReadInt32();
					}

					anAnimationDesc.theLocalHierarchies.Add(aLocalHierarchy);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theLocalHierarchies " + anAnimationDesc.theLocalHierarchies.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theLocalHierarchies alignment");
			}
		}

		public void ReadSequenceDescs()
		{
			if (this.theMdlFileData.localSequenceCount > 0)
			{
				long seqInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localSequenceOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;

					this.theMdlFileData.theSequenceDescs = new List<SourceMdlSequenceDesc>(this.theMdlFileData.localSequenceCount);
					for (int i = 0; i < this.theMdlFileData.localSequenceCount; i++)
					{
						seqInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
						SourceMdlSequenceDesc aSeqDesc = new SourceMdlSequenceDesc();
						aSeqDesc.baseHeaderOffset = this.theInputFileReader.ReadInt32();
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
						aSeqDesc.animIndexOffset = this.theInputFileReader.ReadInt32();
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

						aSeqDesc.fadeInTime = this.theInputFileReader.ReadSingle();
						aSeqDesc.fadeOutTime = this.theInputFileReader.ReadSingle();

						aSeqDesc.localEntryNodeIndex = this.theInputFileReader.ReadInt32();
						aSeqDesc.localExitNodeIndex = this.theInputFileReader.ReadInt32();
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
						aSeqDesc.cyclePoseIndex = this.theInputFileReader.ReadInt32();

						aSeqDesc.activityModifierOffset = 0;
						aSeqDesc.activityModifierCount = 0;
						aSeqDesc.activityModifierOffset = this.theInputFileReader.ReadInt32();
						aSeqDesc.activityModifierCount = this.theInputFileReader.ReadInt32();
						for (int x = 0; x <= 4; x++)
						{
							aSeqDesc.unused[x] = this.theInputFileReader.ReadInt32();
						}
						for (int x = 0; x <= 4; x++)
						{
							this.theInputFileReader.ReadInt32();
						}

						this.theMdlFileData.theSequenceDescs.Add(aSeqDesc);

						inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

						//If aSeqDesc.nameOffset <> 0 Then
						//	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.nameOffset, SeekOrigin.Begin)
						//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						//	aSeqDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						//	Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theLabel")
						//Else
						//	aSeqDesc.theName = ""
						//End If
						//------
						aSeqDesc.theName = this.GetStringAtOffset(seqInputFileStreamPosition, aSeqDesc.nameOffset, "aSeqDesc.theName");

						//If aSeqDesc.activityNameOffset <> 0 Then
						//	Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityNameOffset, SeekOrigin.Begin)
						//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						//	aSeqDesc.theActivityName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName")
						//	End If
						//Else
						//	aSeqDesc.theActivityName = ""
						//End If
						//------
						aSeqDesc.theActivityName = this.GetStringAtOffset(seqInputFileStreamPosition, aSeqDesc.activityNameOffset, "aSeqDesc.theActivityName");

						if ((aSeqDesc.groupSize[0] > 1 || aSeqDesc.groupSize[1] > 1) && aSeqDesc.poseKeyOffset != 0)
						{
							this.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.eventCount > 0 && aSeqDesc.eventOffset != 0)
						{
							this.ReadEvents(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.autoLayerCount > 0 && aSeqDesc.autoLayerOffset != 0)
						{
							this.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc);
						}
						if (this.theMdlFileData.boneCount > 0 && aSeqDesc.weightOffset > 0)
						{
							this.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.ikLockCount > 0 && aSeqDesc.ikLockOffset != 0)
						{
							this.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc);
						}
						if ((aSeqDesc.groupSize[0] * aSeqDesc.groupSize[1]) > 0 && aSeqDesc.animIndexOffset != 0)
						{
							this.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.keyValueSize > 0 && aSeqDesc.keyValueOffset != 0)
						{
							this.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc);
						}
						if (aSeqDesc.activityModifierCount != 0 && aSeqDesc.activityModifierOffset != 0)
						{
							this.ReadActivityModifiers(seqInputFileStreamPosition, aSeqDesc);
						}

						this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs " + this.theMdlFileData.theSequenceDescs.Count.ToString());
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
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "[" + aSeqDesc.theName + "] " + "aSeqDesc.thePoseKeys " + aSeqDesc.thePoseKeys.Count.ToString());
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

			this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			aSeqDesc.theEvents = new List<SourceMdlEvent>(eventCount);
			for (int j = 0; j < eventCount; j++)
			{
				eventInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				SourceMdlEvent anEvent = new SourceMdlEvent();
				anEvent.cycle = this.theInputFileReader.ReadSingle();
				anEvent.eventIndex = this.theInputFileReader.ReadInt32();
				anEvent.eventType = this.theInputFileReader.ReadInt32();
				for (int x = 0; x < anEvent.options.Length; x++)
				{
					anEvent.options[x] = this.theInputFileReader.ReadChar();
				}
				anEvent.nameOffset = this.theInputFileReader.ReadInt32();
				aSeqDesc.theEvents.Add(anEvent);

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

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
					this.theInputFileReader.BaseStream.Seek(eventInputFileStreamPosition + anEvent.nameOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

					anEvent.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEvent.theName = " + anEvent.theName);
					}
				}
				else
				{
					//anEvent.theName = ""
					anEvent.theName = anEvent.eventIndex.ToString();
				}

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theEvents " + aSeqDesc.theEvents.Count.ToString());

			this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theEvents alignment");
		}

		private void ReadAutoLayers(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			int autoLayerCount = aSeqDesc.autoLayerCount;
			long autoLayerInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			aSeqDesc.theAutoLayers = new List<SourceMdlAutoLayer>(autoLayerCount);
			for (int j = 0; j < autoLayerCount; j++)
			{
				autoLayerInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				SourceMdlAutoLayer anAutoLayer = new SourceMdlAutoLayer();
				anAutoLayer.sequenceIndex = this.theInputFileReader.ReadInt16();
				anAutoLayer.poseIndex = this.theInputFileReader.ReadInt16();
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
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theAutoLayers " + aSeqDesc.theAutoLayers.Count.ToString());
		}

		private void ReadMdlAnimBoneWeights(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			long weightListInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.weightOffset, SeekOrigin.Begin);
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
			//NOTE: A sequence can point to same weights as another.
			if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart))
			{
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theBoneWeights " + aSeqDesc.theBoneWeights.Count.ToString());
			}
		}

		private void ReadSequenceIkLocks(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			int lockCount = aSeqDesc.ikLockCount;
			long lockInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			aSeqDesc.theIkLocks = new List<SourceMdlIkLock>(lockCount);
			for (int j = 0; j < lockCount; j++)
			{
				lockInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				SourceMdlIkLock anIkLock = new SourceMdlIkLock();
				anIkLock.chainIndex = this.theInputFileReader.ReadInt32();
				anIkLock.posWeight = this.theInputFileReader.ReadSingle();
				anIkLock.localQWeight = this.theInputFileReader.ReadSingle();
				anIkLock.flags = this.theInputFileReader.ReadInt32();
				for (int x = 0; x < anIkLock.unused.Length; x++)
				{
					anIkLock.unused[x] = this.theInputFileReader.ReadInt32();
				}
				aSeqDesc.theIkLocks.Add(anIkLock);

				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theIkLocks " + aSeqDesc.theIkLocks.Count.ToString());
		}

		private void ReadMdlAnimIndexes(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			int animIndexCount = aSeqDesc.groupSize[0] * aSeqDesc.groupSize[1];
			long animIndexInputFileStreamPosition = 0;
			//Dim inputFileStreamPosition As Long
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.animIndexOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			aSeqDesc.theAnimDescIndexes = new List<short>(animIndexCount);
			for (int j = 0; j < animIndexCount; j++)
			{
				animIndexInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				short anAnimIndex = this.theInputFileReader.ReadInt16();
				aSeqDesc.theAnimDescIndexes.Add(anAnimIndex);

				if (this.theMdlFileData.theAnimationDescs != null && this.theMdlFileData.theAnimationDescs.Count > anAnimIndex)
				{
					//NOTE: Set this boolean for use in writing lines in qc file.
					this.theMdlFileData.theAnimationDescs[anAnimIndex].theAnimIsLinkedToSequence = true;
					this.theMdlFileData.theAnimationDescs[anAnimIndex].theLinkedSequences.Add(aSeqDesc);
				}

				//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			//TODO: A sequence can point to same anims as another?
			if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart))
			{
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theAnimDescIndexes " + aSeqDesc.theAnimDescIndexes.Count.ToString());
			}

			this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theAnimDescIndexes alignment");
		}

		private void ReadSequenceKeyValues(long seqInputFileStreamPosition, SourceMdlSequenceDesc aSeqDesc)
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			aSeqDesc.theKeyValues = FileManager.ReadNullTerminatedString(this.theInputFileReader);

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theKeyValues = " + aSeqDesc.theKeyValues);

			this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theKeyValues alignment");
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

			this.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityModifierOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			activityModifierCount = aSeqDesc.activityModifierCount;
			aSeqDesc.theActivityModifiers = new List<SourceMdlActivityModifier>(activityModifierCount);
			for (int j = 0; j < activityModifierCount; j++)
			{
				activityModifierInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				SourceMdlActivityModifier anActivityModifier = new SourceMdlActivityModifier();
				anActivityModifier.nameOffset = this.theInputFileReader.ReadInt32();
				aSeqDesc.theActivityModifiers.Add(anActivityModifier);

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				if (anActivityModifier.nameOffset != 0)
				{
					this.theInputFileReader.BaseStream.Seek(activityModifierInputFileStreamPosition + anActivityModifier.nameOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

					anActivityModifier.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anActivityModifier.theName = " + anActivityModifier.theName);
					}
				}
				else
				{
					anActivityModifier.theName = "";
				}

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theActivityModifiers " + aSeqDesc.theActivityModifiers.Count.ToString());

			//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "[" + aSeqDesc.theName + "] " + "aSeqDesc.theActivityModifiers alignment")
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
			if (this.theMdlFileData.localNodeCount > 0 && this.theMdlFileData.localNodeNameOffset != 0)
			{
				long localNodeNameInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localNodeNameOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theLocalNodeNames = new List<string>(this.theMdlFileData.localNodeCount);
				int localNodeNameOffset = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				string aLocalNodeName = null;
				for (int i = 0; i < this.theMdlFileData.localNodeCount; i++)
				{
					localNodeNameInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
	//				Dim aLocalNodeName As String
					localNodeNameOffset = this.theInputFileReader.ReadInt32();

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					if (localNodeNameOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(localNodeNameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

						aLocalNodeName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

						fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
						if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aLocalNodeName = " + aLocalNodeName);
						}
					}
					else
					{
						aLocalNodeName = "";
					}
					this.theMdlFileData.theLocalNodeNames.Add(aLocalNodeName);

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLocalNodeNames " + this.theMdlFileData.theLocalNodeNames.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theLocalNodeNames alignment");
			}
		}

		//TODO: 
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

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.bodyPartOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theBodyParts = new List<SourceMdlBodyPart>(this.theMdlFileData.bodyPartCount);
				for (int bodyPartIndex = 0; bodyPartIndex < this.theMdlFileData.bodyPartCount; bodyPartIndex++)
				{
					bodyPartInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlBodyPart aBodyPart = new SourceMdlBodyPart();
					aBodyPart.nameOffset = this.theInputFileReader.ReadInt32();
					aBodyPart.modelCount = this.theInputFileReader.ReadInt32();
					aBodyPart.@base = this.theInputFileReader.ReadInt32();
					aBodyPart.modelOffset = this.theInputFileReader.ReadInt32();
					this.theMdlFileData.theBodyParts.Add(aBodyPart);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					//If aBodyPart.nameOffset <> 0 Then
					//	Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.nameOffset, SeekOrigin.Begin)
					//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					//	aBodyPart.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBodyPart.theName")
					//	End If
					//Else
					//	aBodyPart.theName = ""
					//End If
					//------
					aBodyPart.theName = this.GetStringAtOffset(bodyPartInputFileStreamPosition, aBodyPart.nameOffset, "aBodyPart.theName");

					this.ReadModels(bodyPartInputFileStreamPosition, aBodyPart, bodyPartIndex);
					//NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts and models.
					if (bodyPartIndex == this.theMdlFileData.bodyPartCount - 1)
					{
						this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, this.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment");
					}

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts " + this.theMdlFileData.theBodyParts.Count.ToString());
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

				this.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aBodyPart.theModels = new List<SourceMdlModel>(aBodyPart.modelCount);
				for (int j = 0; j < aBodyPart.modelCount; j++)
				{
					modelInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlModel aModel = new SourceMdlModel();

					aModel.name = this.theInputFileReader.ReadChars(64);
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
					SourceMdlModelVertexData modelVertexData = new SourceMdlModelVertexData();
					modelVertexData.vertexDataP = this.theInputFileReader.ReadInt32();
					modelVertexData.tangentDataP = this.theInputFileReader.ReadInt32();
					aModel.vertexData = modelVertexData;
					for (int x = 0; x <= 7; x++)
					{
						aModel.unused[x] = this.theInputFileReader.ReadInt32();
					}

					aBodyPart.theModels.Add(aModel);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					//NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
					this.ReadEyeballs(modelInputFileStreamPosition, aModel, bodyPartIndex);
					this.ReadMeshes(modelInputFileStreamPosition, aModel);

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels " + aBodyPart.theModels.Count.ToString());
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

				this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
				{
					meshInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlMesh aMesh = new SourceMdlMesh();

					aMesh.materialIndex = this.theInputFileReader.ReadInt32();
					aMesh.modelOffset = this.theInputFileReader.ReadInt32();
					aMesh.vertexCount = this.theInputFileReader.ReadInt32();
					aMesh.vertexIndexStart = this.theInputFileReader.ReadInt32();
					aMesh.flexCount = this.theInputFileReader.ReadInt32();
					aMesh.flexOffset = this.theInputFileReader.ReadInt32();
					aMesh.materialType = this.theInputFileReader.ReadInt32();
					aMesh.materialParam = this.theInputFileReader.ReadInt32();
					aMesh.id = this.theInputFileReader.ReadInt32();
					aMesh.centerX = this.theInputFileReader.ReadSingle();
					aMesh.centerY = this.theInputFileReader.ReadSingle();
					aMesh.centerZ = this.theInputFileReader.ReadSingle();
					SourceMdlMeshVertexData meshVertexData = new SourceMdlMeshVertexData();
					meshVertexData.modelVertexDataP = this.theInputFileReader.ReadInt32();
					for (int x = 0; x < SourceConstants.MAX_NUM_LODS; x++)
					{
						meshVertexData.lodVertexCount[x] = this.theInputFileReader.ReadInt32();
					}
					aMesh.vertexData = meshVertexData;
					for (int x = 0; x <= 7; x++)
					{
						aMesh.unused[x] = this.theInputFileReader.ReadInt32();
					}

					aModel.theMeshes.Add(aMesh);

					// Fill-in eyeball texture index info.
					if (aMesh.materialType == 1)
					{
						aModel.theEyeballs[aMesh.materialParam].theTextureIndex = aMesh.materialIndex;
					}

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
		}

		private void ReadEyeballs(long modelInputFileStreamPosition, SourceMdlModel aModel, int bodyPartIndex)
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

				this.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				for (int k = 0; k < aModel.eyeballCount; k++)
				{
					eyeballInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlEyeball anEyeball = new SourceMdlEyeball();

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

					anEyeball.unused1 = this.theInputFileReader.ReadInt32();
					anEyeball.irisScale = this.theInputFileReader.ReadSingle();
					anEyeball.unused2 = this.theInputFileReader.ReadInt32();

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

					for (int x = 0; x < anEyeball.unused.Length; x++)
					{
						anEyeball.unused[x] = this.theInputFileReader.ReadInt32();
					}

					anEyeball.eyeballIsNonFacs = this.theInputFileReader.ReadByte();

					for (int x = 0; x < anEyeball.unused3.Length; x++)
					{
						anEyeball.unused3[x] = this.theInputFileReader.ReadChar();
					}
					for (int x = 0; x < anEyeball.unused4.Length; x++)
					{
						anEyeball.unused4[x] = this.theInputFileReader.ReadInt32();
					}

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

				if (aModel.theEyeballs.Count > 0)
				{
					this.theMdlFileData.theModelCommandIsUsed = true;
					this.theMdlFileData.theBodyPartIndexThatShouldUseModelCommand = bodyPartIndex;
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs " + aModel.theEyeballs.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aModel.theEyeballs alignment");
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

			this.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			for (int k = 0; k < aMesh.flexCount; k++)
			{
				flexInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				SourceMdlFlex aFlex = new SourceMdlFlex();

				aFlex.flexDescIndex = this.theInputFileReader.ReadInt32();

				aFlex.target0 = this.theInputFileReader.ReadSingle();
				aFlex.target1 = this.theInputFileReader.ReadSingle();
				aFlex.target2 = this.theInputFileReader.ReadSingle();
				aFlex.target3 = this.theInputFileReader.ReadSingle();

				aFlex.vertCount = this.theInputFileReader.ReadInt32();
				aFlex.vertOffset = this.theInputFileReader.ReadInt32();

				aFlex.flexDescPartnerIndex = this.theInputFileReader.ReadInt32();
				aFlex.vertAnimType = this.theInputFileReader.ReadByte();
				for (int x = 0; x < aFlex.unusedChar.Length; x++)
				{
					aFlex.unusedChar[x] = this.theInputFileReader.ReadChar();
				}
				for (int x = 0; x < aFlex.unused.Length; x++)
				{
					aFlex.unused[x] = this.theInputFileReader.ReadInt32();
				}
				aMesh.theFlexes.Add(aFlex);

				//'NOTE: Set the frame index here because it is determined by order of flexes in mdl file.
				//'      Start the indexing at 1 because first frame (frame 0) is "basis" frame.
				//Me.theCurrentFrameIndex += 1
				//Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex = Me.theCurrentFrameIndex

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				if (aFlex.vertCount > 0 && aFlex.vertOffset != 0)
				{
					this.ReadVertAnims(flexInputFileStreamPosition, aFlex);
				}

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString());

			this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aMesh.theFlexes alignment");
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

			this.theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin);
			fileOffsetStart = this.theInputFileReader.BaseStream.Position;

			SourceMdlVertAnim aVertAnim = null;
			for (int k = 0; k < aFlex.vertCount; k++)
			{
				eyeballInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				if (aFlex.vertAnimType == aFlex.STUDIO_VERT_ANIM_WRINKLE)
				{
					aVertAnim = new SourceMdlVertAnimWrinkle();
				}
				else
				{
					aVertAnim = new SourceMdlVertAnim();
				}

				aVertAnim.index = this.theInputFileReader.ReadUInt16();
				aVertAnim.speed = this.theInputFileReader.ReadByte();
				aVertAnim.side = this.theInputFileReader.ReadByte();

				for (int x = 0; x <= 2; x++)
				{
					aVertAnim.set_deltaUShort(x, this.theInputFileReader.ReadUInt16());
				}
				for (int x = 0; x <= 2; x++)
				{
					aVertAnim.set_nDeltaUShort(x, this.theInputFileReader.ReadUInt16());
				}

				if (aFlex.vertAnimType == aFlex.STUDIO_VERT_ANIM_WRINKLE)
				{
					((SourceMdlVertAnimWrinkle)aVertAnim).wrinkleDelta = this.theInputFileReader.ReadInt16();
				}

				aFlex.theVertAnims.Add(aVertAnim);

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}

			//aFlex.theVertAnims.Sort(AddressOf Me.SortVertAnims)

			fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
			this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString());

			this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment");
		}

		private int SortVertAnims(SourceMdlVertAnim x, SourceMdlVertAnim y)
		{
			return x.index.CompareTo(y.index);
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
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs " + this.theMdlFileData.theFlexDescs.Count.ToString());
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

				if (this.theMdlFileData.theFlexControllers.Count > 0)
				{
					this.theMdlFileData.theModelCommandIsUsed = true;
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers " + this.theMdlFileData.theFlexControllers.Count.ToString());
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
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + this.theMdlFileData.theFlexRules.Count.ToString());
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

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.ikChainOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theIkChains = new List<SourceMdlIkChain>(this.theMdlFileData.ikChainCount);
				for (int i = 0; i < this.theMdlFileData.ikChainCount; i++)
				{
					ikChainInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlIkChain anIkChain = new SourceMdlIkChain();
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
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkChains " + this.theMdlFileData.theIkChains.Count.ToString());
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

				this.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				anIkChain.theLinks = new List<SourceMdlIkLink>(anIkChain.linkCount);
				for (int j = 0; j < anIkChain.linkCount; j++)
				{
					//ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlIkLink anIkLink = new SourceMdlIkLink();
					anIkLink.boneIndex = this.theInputFileReader.ReadInt32();
					anIkLink.idealBendingDirection.x = this.theInputFileReader.ReadSingle();
					anIkLink.idealBendingDirection.y = this.theInputFileReader.ReadSingle();
					anIkLink.idealBendingDirection.z = this.theInputFileReader.ReadSingle();
					anIkLink.unused0.x = this.theInputFileReader.ReadSingle();
					anIkLink.unused0.y = this.theInputFileReader.ReadSingle();
					anIkLink.unused0.z = this.theInputFileReader.ReadSingle();
					anIkChain.theLinks.Add(anIkLink);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks " + anIkChain.theLinks.Count.ToString());
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

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theIkLocks = new List<SourceMdlIkLock>(this.theMdlFileData.localIkAutoPlayLockCount);
				for (int i = 0; i < this.theMdlFileData.localIkAutoPlayLockCount; i++)
				{
					//ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
					SourceMdlIkLock anIkLock = new SourceMdlIkLock();
					anIkLock.chainIndex = this.theInputFileReader.ReadInt32();
					anIkLock.posWeight = this.theInputFileReader.ReadSingle();
					anIkLock.localQWeight = this.theInputFileReader.ReadSingle();
					anIkLock.flags = this.theInputFileReader.ReadInt32();
					for (int x = 0; x < anIkLock.unused.Length; x++)
					{
						anIkLock.unused[x] = this.theInputFileReader.ReadInt32();
					}
					this.theMdlFileData.theIkLocks.Add(anIkLock);

					//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks " + this.theMdlFileData.theIkLocks.Count.ToString());
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
					this.theMdlFileData.theModelCommandIsUsed = true;
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMouths " + this.theMdlFileData.theMouths.Count.ToString());
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
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs " + this.theMdlFileData.thePoseParamDescs.Count.ToString());
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
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long
				//Dim texturePath As String

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.textureOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theTextures = new List<SourceMdlTexture>(this.theMdlFileData.textureCount);
				for (int i = 0; i < this.theMdlFileData.textureCount; i++)
				{
					textureInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlTexture aTexture = new SourceMdlTexture();
					aTexture.nameOffset = this.theInputFileReader.ReadInt32();
					aTexture.flags = this.theInputFileReader.ReadInt32();
					aTexture.used = this.theInputFileReader.ReadInt32();
					aTexture.unused1 = this.theInputFileReader.ReadInt32();
					aTexture.materialP = this.theInputFileReader.ReadInt32();
					aTexture.clientMaterialP = this.theInputFileReader.ReadInt32();
					for (int x = 0; x <= 4; x++)
					{
						aTexture.unused[x] = this.theInputFileReader.ReadInt32();
					}
					this.theMdlFileData.theTextures.Add(aTexture);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					//If aTexture.nameOffset <> 0 Then
					//	Me.theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.nameOffset, SeekOrigin.Begin)
					//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					//	aTexture.thePathFileName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

					//	' Convert all forward slashes to backward slashes.
					//	aTexture.thePathFileName = FileManager.GetNormalizedPathFileName(aTexture.thePathFileName)

					//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
					//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theName")
					//	End If
					//Else
					//	aTexture.thePathFileName = ""
					//End If
					//------
					aTexture.thePathFileName = this.GetStringAtOffset(textureInputFileStreamPosition, aTexture.nameOffset, "aTexture.thePathFileName");

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures " + this.theMdlFileData.theTextures.Count.ToString());

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
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths " + this.theMdlFileData.theTexturePaths.Count.ToString());

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

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.skinFamilyOffset, SeekOrigin.Begin);
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
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies " + this.theMdlFileData.theSkinFamilies.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment");
			}
		}

		public void ReadModelGroups()
		{
			if (this.theMdlFileData.includeModelCount > 0)
			{
				long includeModelInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.includeModelOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theModelGroups = new List<SourceMdlModelGroup>(this.theMdlFileData.includeModelCount);
				for (int i = 0; i < this.theMdlFileData.includeModelCount; i++)
				{
					includeModelInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlModelGroup aModelGroup = new SourceMdlModelGroup();
					aModelGroup.labelOffset = this.theInputFileReader.ReadInt32();
					aModelGroup.fileNameOffset = this.theInputFileReader.ReadInt32();
					this.theMdlFileData.theModelGroups.Add(aModelGroup);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					if (aModelGroup.labelOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.labelOffset, SeekOrigin.Begin);
						fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

						aModelGroup.theLabel = FileManager.ReadNullTerminatedString(this.theInputFileReader);

						fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
						if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theLabel = " + aModelGroup.theLabel);
						}
					}
					else
					{
						aModelGroup.theLabel = "";
					}
					if (aModelGroup.fileNameOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.fileNameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

						aModelGroup.theFileName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

						fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
						if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theFileName = " + aModelGroup.theFileName);
						}
					}
					else
					{
						aModelGroup.theFileName = "";
					}

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theModelGroups " + this.theMdlFileData.theModelGroups.Count.ToString());
			}
		}

		public void ReadFlexControllerUis()
		{
			if (this.theMdlFileData.flexControllerUiCount > 0)
			{
				long flexControllerUiInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.flexControllerUiOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theFlexControllerUis = new List<SourceMdlFlexControllerUi>(this.theMdlFileData.flexControllerUiCount);
				for (int i = 0; i < this.theMdlFileData.flexControllerUiCount; i++)
				{
					flexControllerUiInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlFlexControllerUi aFlexControllerUi = new SourceMdlFlexControllerUi();
					aFlexControllerUi.nameOffset = this.theInputFileReader.ReadInt32();
					aFlexControllerUi.config0 = this.theInputFileReader.ReadInt32();
					aFlexControllerUi.config1 = this.theInputFileReader.ReadInt32();
					aFlexControllerUi.config2 = this.theInputFileReader.ReadInt32();
					aFlexControllerUi.remapType = this.theInputFileReader.ReadByte();
					aFlexControllerUi.controlIsStereo = this.theInputFileReader.ReadByte();
					for (int x = 0; x < aFlexControllerUi.unused.Length; x++)
					{
						aFlexControllerUi.unused[x] = this.theInputFileReader.ReadByte();
					}
					this.theMdlFileData.theFlexControllerUis.Add(aFlexControllerUi);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					if (aFlexControllerUi.nameOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(flexControllerUiInputFileStreamPosition + aFlexControllerUi.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

						aFlexControllerUi.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

						fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
						if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexControllerUi.theName = " + aFlexControllerUi.theName);
						}
					}
					else
					{
						aFlexControllerUi.theName = "";
					}

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllerUis " + this.theMdlFileData.theFlexControllerUis.Count.ToString());
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
			if (this.theMdlFileData.sourceBoneTransformCount > 0)
			{
				long boneInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				long fileOffsetStart2 = 0;
				long fileOffsetEnd2 = 0;

				this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.sourceBoneTransformOffset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				this.theMdlFileData.theBoneTransforms = new List<SourceMdlBoneTransform>(this.theMdlFileData.sourceBoneTransformCount);
				for (int i = 0; i < this.theMdlFileData.sourceBoneTransformCount; i++)
				{
					boneInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
					SourceMdlBoneTransform aBoneTransform = new SourceMdlBoneTransform();

					aBoneTransform.nameOffset = this.theInputFileReader.ReadInt32();

					aBoneTransform.preTransformColumn0 = new SourceVector();
					aBoneTransform.preTransformColumn1 = new SourceVector();
					aBoneTransform.preTransformColumn2 = new SourceVector();
					aBoneTransform.preTransformColumn3 = new SourceVector();
					aBoneTransform.preTransformColumn0.x = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn1.x = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn2.x = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn3.x = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn0.y = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn1.y = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn2.y = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn3.y = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn0.z = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn1.z = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn2.z = this.theInputFileReader.ReadSingle();
					aBoneTransform.preTransformColumn3.z = this.theInputFileReader.ReadSingle();

					aBoneTransform.postTransformColumn0 = new SourceVector();
					aBoneTransform.postTransformColumn1 = new SourceVector();
					aBoneTransform.postTransformColumn2 = new SourceVector();
					aBoneTransform.postTransformColumn3 = new SourceVector();
					aBoneTransform.postTransformColumn0.x = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn1.x = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn2.x = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn3.x = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn0.y = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn1.y = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn2.y = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn3.y = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn0.z = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn1.z = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn2.z = this.theInputFileReader.ReadSingle();
					aBoneTransform.postTransformColumn3.z = this.theInputFileReader.ReadSingle();

					this.theMdlFileData.theBoneTransforms.Add(aBoneTransform);

					inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					if (aBoneTransform.nameOffset != 0)
					{
						this.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBoneTransform.nameOffset, SeekOrigin.Begin);
						fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;

						aBoneTransform.theName = FileManager.ReadNullTerminatedString(this.theInputFileReader);

						fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
						if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBoneTransform.theName = " + aBoneTransform.theName);
						}
					}
					else if (aBoneTransform.theName == null)
					{
						aBoneTransform.theName = "";
					}

					this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneTransforms " + this.theMdlFileData.theBoneTransforms.Count.ToString());

				this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneTransforms alignment");
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
			if (this.theMdlFileData.linearBoneOffset > 0)
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
					this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.studioHeader2Offset + this.theMdlFileData.linearBoneOffset, SeekOrigin.Begin);
					fileOffsetStart = this.theInputFileReader.BaseStream.Position;
					boneTableInputFileStreamPosition = this.theInputFileReader.BaseStream.Position;

					SourceMdlLinearBone linearBoneTable = null;
					this.theMdlFileData.theLinearBoneTable = new SourceMdlLinearBone();
					linearBoneTable = this.theMdlFileData.theLinearBoneTable;
					linearBoneTable.boneCount = this.theInputFileReader.ReadInt32();
					linearBoneTable.flagsOffset = this.theInputFileReader.ReadInt32();
					linearBoneTable.parentOffset = this.theInputFileReader.ReadInt32();
					linearBoneTable.posOffset = this.theInputFileReader.ReadInt32();
					linearBoneTable.quatOffset = this.theInputFileReader.ReadInt32();
					linearBoneTable.rotOffset = this.theInputFileReader.ReadInt32();
					linearBoneTable.poseToBoneOffset = this.theInputFileReader.ReadInt32();
					linearBoneTable.posScaleOffset = this.theInputFileReader.ReadInt32();
					linearBoneTable.rotScaleOffset = this.theInputFileReader.ReadInt32();
					linearBoneTable.qAlignmentOffset = this.theInputFileReader.ReadInt32();
					for (int x = 0; x < linearBoneTable.unused.Length; x++)
					{
						linearBoneTable.unused[x] = this.theInputFileReader.ReadInt32();
					}

					fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLinearBoneTable header");

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.flagsOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						int flags = this.theInputFileReader.ReadInt32();
						linearBoneTable.theFlags.Add(flags);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theFlags");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theFlags alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.parentOffset != this.theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.parentOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						int parent = this.theInputFileReader.ReadInt32();
						linearBoneTable.theParents.Add(parent);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theParents");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theParents alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.posOffset != this.theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.posOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector position = new SourceVector();
						position.x = this.theInputFileReader.ReadSingle();
						position.y = this.theInputFileReader.ReadSingle();
						position.z = this.theInputFileReader.ReadSingle();
						linearBoneTable.thePositions.Add(position);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePositions");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePositions alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.quatOffset != this.theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.quatOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceQuaternion quaternion = new SourceQuaternion();
						quaternion.x = this.theInputFileReader.ReadSingle();
						quaternion.y = this.theInputFileReader.ReadSingle();
						quaternion.z = this.theInputFileReader.ReadSingle();
						quaternion.w = this.theInputFileReader.ReadSingle();
						linearBoneTable.theQuaternions.Add(quaternion);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theQuaternions");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theQuaternions alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.rotOffset != this.theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.rotOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector rotation = new SourceVector();
						rotation.x = this.theInputFileReader.ReadSingle();
						rotation.y = this.theInputFileReader.ReadSingle();
						rotation.z = this.theInputFileReader.ReadSingle();
						linearBoneTable.theRotations.Add(rotation);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theRotations");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theRotations alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.poseToBoneOffset != this.theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.poseToBoneOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector poseToBoneDataColumn0 = new SourceVector();
						SourceVector poseToBoneDataColumn1 = new SourceVector();
						SourceVector poseToBoneDataColumn2 = new SourceVector();
						SourceVector poseToBoneDataColumn3 = new SourceVector();
						poseToBoneDataColumn0.x = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn1.x = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn2.x = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn3.x = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn0.y = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn1.y = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn2.y = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn3.y = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn0.z = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn1.z = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn2.z = this.theInputFileReader.ReadSingle();
						poseToBoneDataColumn3.z = this.theInputFileReader.ReadSingle();
						linearBoneTable.thePoseToBoneDataColumn0s.Add(poseToBoneDataColumn0);
						linearBoneTable.thePoseToBoneDataColumn1s.Add(poseToBoneDataColumn1);
						linearBoneTable.thePoseToBoneDataColumn2s.Add(poseToBoneDataColumn2);
						linearBoneTable.thePoseToBoneDataColumn3s.Add(poseToBoneDataColumn3);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePoseToBoneDataColumns");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePoseToBoneDataColumns alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.posScaleOffset != this.theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.posScaleOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector positionScale = new SourceVector();
						positionScale.x = this.theInputFileReader.ReadSingle();
						positionScale.y = this.theInputFileReader.ReadSingle();
						positionScale.z = this.theInputFileReader.ReadSingle();
						linearBoneTable.thePositionScales.Add(positionScale);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.thePositionScales");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.thePositionScales alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.rotScaleOffset != this.theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.rotScaleOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceVector rotationScale = new SourceVector();
						rotationScale.x = this.theInputFileReader.ReadSingle();
						rotationScale.y = this.theInputFileReader.ReadSingle();
						rotationScale.z = this.theInputFileReader.ReadSingle();
						linearBoneTable.theRotationScales.Add(rotationScale);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theRotationScales");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theRotationScales alignment");

					if (boneTableInputFileStreamPosition + linearBoneTable.qAlignmentOffset != this.theInputFileReader.BaseStream.Position)
					{
						int debug = 4242;
					}

					this.theInputFileReader.BaseStream.Seek(boneTableInputFileStreamPosition + linearBoneTable.qAlignmentOffset, SeekOrigin.Begin);
					fileOffsetStart2 = this.theInputFileReader.BaseStream.Position;
					for (int i = 0; i < linearBoneTable.boneCount; i++)
					{
						SourceQuaternion qAlignment = new SourceQuaternion();
						qAlignment.x = this.theInputFileReader.ReadSingle();
						qAlignment.y = this.theInputFileReader.ReadSingle();
						qAlignment.z = this.theInputFileReader.ReadSingle();
						qAlignment.w = this.theInputFileReader.ReadSingle();
						linearBoneTable.theQAlignments.Add(qAlignment);
					}
					fileOffsetEnd2 = this.theInputFileReader.BaseStream.Position - 1;
					if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
					{
						this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theMdlFileData.theLinearBoneTable.theQAlignments");
					}
					this.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(this.theInputFileReader, fileOffsetEnd2, 4, "theMdlFileData.theLinearBoneTable.theQAlignments alignment");
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
		//	Catch

		//	End Try
		//End Sub

		public void ReadUnreadBytes()
		{
			this.theMdlFileData.theFileSeekLog.LogUnreadBytes(this.theInputFileReader);
		}

		public void SetReaderToPhyOffset()
		{
			this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.phyOffset, SeekOrigin.Begin);
		}

		public void SetReaderToVtxOffset()
		{
			this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.vtxOffset, SeekOrigin.Begin);
		}

		public void SetReaderToVvdOffset()
		{
			this.theInputFileReader.BaseStream.Seek(this.theMdlFileData.vvdOffset, SeekOrigin.Begin);
		}

		public void CreateFlexFrameList()
		{
			FlexFrame aFlexFrame = null;
			SourceMdlBodyPart aBodyPart = null;
			SourceMdlModel aModel = null;
			SourceMdlMesh aMesh = null;
			SourceMdlFlex aFlex = null;
			FlexFrame searchedFlexFrame = null;

			this.theMdlFileData.theFlexFrames = new List<FlexFrame>();

			//NOTE: Create the defaultflex.
			aFlexFrame = new FlexFrame();
			this.theMdlFileData.theFlexFrames.Add(aFlexFrame);

			if (this.theMdlFileData.theFlexDescs != null && this.theMdlFileData.theFlexDescs.Count > 0)
			{
				//Dim flexDescToMeshIndexes As List(Of List(Of Integer))
				List<List<FlexFrame>> flexDescToFlexFrames = null;
				int meshVertexIndexStart = 0;

				//flexDescToMeshIndexes = New List(Of List(Of Integer))(Me.theMdlFileData.theFlexDescs.Count)
				//For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
				//	Dim meshIndexList As New List(Of Integer)()
				//	flexDescToMeshIndexes.Add(meshIndexList)
				//Next

				flexDescToFlexFrames = new List<List<FlexFrame>>(this.theMdlFileData.theFlexDescs.Count);
				for (int x = 0; x < this.theMdlFileData.theFlexDescs.Count; x++)
				{
					List<FlexFrame> flexFrameList = new List<FlexFrame>();
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
														// Add to an existing flexFrame.
														aFlexFrame = searchedFlexFrame;
														break;
													}
												}
											}
											if (aFlexFrame == null)
											{
												aFlexFrame = new FlexFrame();
												this.theMdlFileData.theFlexFrames.Add(aFlexFrame);
												aFlexFrame.bodyAndMeshVertexIndexStarts = new List<int>();
												aFlexFrame.flexes = new List<SourceMdlFlex>();

												int aFlexDescPartnerIndex = aMesh.theFlexes[flexIndex].flexDescPartnerIndex;

												aFlexFrame.flexName = this.theMdlFileData.theFlexDescs[aFlex.flexDescIndex].theName;
												if (aFlexDescPartnerIndex > 0)
												{
													//line += "flexpair """
													//aFlexFrame.flexName = aFlexFrame.flexName.Remove(aFlexFrame.flexName.Length - 1, 1)
													aFlexFrame.flexDescription = aFlexFrame.flexName;
													aFlexFrame.flexDescription += "+";
													aFlexFrame.flexDescription += this.theMdlFileData.theFlexDescs[aFlex.flexDescPartnerIndex].theName;
													aFlexFrame.flexHasPartner = true;
													aFlexFrame.flexSplit = this.GetSplit(aFlex, meshVertexIndexStart);
													this.theMdlFileData.theFlexDescs[aFlex.flexDescPartnerIndex].theDescIsUsedByFlex = true;
												}
												else
												{
													//line += "flex """
													aFlexFrame.flexDescription = aFlexFrame.flexName;
													aFlexFrame.flexHasPartner = false;
												}
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
			//TODO: Should only write up to 64 characters.
			this.theOutputFileWriter.Write(internalMdlFileName.ToCharArray());
			//NOTE: Write the ending null byte.
			this.theOutputFileWriter.Write(Convert.ToByte(0));
		}

		public void WriteInternalAniFileName(string internalAniFileName)
		{
			if (this.theMdlFileData.animBlockCount > 0)
			{
				if (this.theMdlFileData.animBlockNameOffset > 0)
				{
					// Set a new offset for the file name at end-of-file's second null byte.
					this.theOutputFileWriter.BaseStream.Seek(-2, SeekOrigin.End);
					//NOTE: Important that offset be an Integer (4 bytes) rather than a Long (8 bytes).
					int offset = (int)this.theOutputFileWriter.BaseStream.Position;
					this.theOutputFileWriter.BaseStream.Seek(0x15C, SeekOrigin.Begin);
					this.theOutputFileWriter.Write(offset);

					// Write the new file name.
					this.theOutputFileWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
					this.theOutputFileWriter.Write(internalAniFileName.ToCharArray());
					//NOTE: Write the ending null byte.
					this.theOutputFileWriter.Write(Convert.ToByte(0));

					// Write the new end-of-file's null bytes.
					this.theOutputFileWriter.Write(Convert.ToByte(0));
					this.theOutputFileWriter.Write(Convert.ToByte(0));

					// Write the new file size.
					this.theOutputFileWriter.BaseStream.Seek(0, SeekOrigin.End);
					offset = (int)this.theOutputFileWriter.BaseStream.Position;
					this.theOutputFileWriter.BaseStream.Seek(0x4C, SeekOrigin.Begin);
					this.theOutputFileWriter.Write(offset);
				}
			}
		}

#endregion

#region Private Methods

		private string GetStringAtOffset(long startOffset, long offset, string variableNameForLog)
		{
			string aString = null;

			if (offset > 0)
			{
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;

				inputFileStreamPosition = this.theInputFileReader.BaseStream.Position;
				this.theInputFileReader.BaseStream.Seek(startOffset + offset, SeekOrigin.Begin);
				fileOffsetStart = this.theInputFileReader.BaseStream.Position;

				aString = FileManager.ReadNullTerminatedString(this.theInputFileReader);

				fileOffsetEnd = this.theInputFileReader.BaseStream.Position - 1;
				if (!this.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart))
				{
					this.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, variableNameForLog + " = \"" + aString + "\"");
				}
				this.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}
			else
			{
				aString = "";
			}

			return aString;
		}

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

		protected SourceMdlFileData53 theMdlFileData;
		protected SourceMdlFileData53 theRealMdlFileData;

#endregion

	}

}