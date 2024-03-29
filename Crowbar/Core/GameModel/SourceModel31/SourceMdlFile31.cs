﻿//INSTANT C# NOTE: Formerly VB project-level imports:
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
	public class SourceMdlFile31
	{

#region Creation and Destruction

		public SourceMdlFile31(BinaryReader mdlFileReader, SourceMdlFileData31 mdlFileData)
		{
			theInputFileReader = mdlFileReader;
			theMdlFileData = mdlFileData;

			theMdlFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

		//Public Sub New(ByVal mdlFileWriter As BinaryWriter, ByVal mdlFileData As SourceMdlFileData31)
		//	Me.theOutputFileWriter = mdlFileWriter
		//	Me.theMdlFileData = mdlFileData
		//End Sub

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

			//MDL27 - Probably alignment filler.
			if (theMdlFileData.version == 27)
			{
				theInputFileReader.ReadInt32();
			}

			theMdlFileData.illuminationPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.illuminationPosition.z = theInputFileReader.ReadSingle();

			//MDL27 - Probably alignment filler.
			if (theMdlFileData.version == 27)
			{
				theInputFileReader.ReadInt32();
			}

			theMdlFileData.hullMinPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.hullMinPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.hullMinPosition.z = theInputFileReader.ReadSingle();

			//MDL27 - Probably alignment filler.
			if (theMdlFileData.version == 27)
			{
				theInputFileReader.ReadInt32();
			}

			theMdlFileData.hullMaxPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.hullMaxPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.hullMaxPosition.z = theInputFileReader.ReadSingle();

			theMdlFileData.viewBoundingBoxMinPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMinPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMinPosition.z = theInputFileReader.ReadSingle();

			theMdlFileData.viewBoundingBoxMaxPosition.x = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMaxPosition.y = theInputFileReader.ReadSingle();
			theMdlFileData.viewBoundingBoxMaxPosition.z = theInputFileReader.ReadSingle();

			//MDL27 - Probably alignment filler.
			if (theMdlFileData.version == 27)
			{
				theInputFileReader.ReadInt32();
				theInputFileReader.ReadInt32();
				theInputFileReader.ReadInt32();
			}

			theMdlFileData.flags = theInputFileReader.ReadInt32();

			theMdlFileData.boneCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneOffset = theInputFileReader.ReadInt32();
			theMdlFileData.boneControllerCount = theInputFileReader.ReadInt32();
			theMdlFileData.boneControllerOffset = theInputFileReader.ReadInt32();

			if (theMdlFileData.version >= 27 && theMdlFileData.version <= 30)
			{
				theMdlFileData.hitboxCount_MDL27to30 = theInputFileReader.ReadInt32();
				theMdlFileData.hitboxOffset_MDL27to30 = theInputFileReader.ReadInt32();
			}
			else
			{
				theMdlFileData.hitboxSetCount = theInputFileReader.ReadInt32();
				theMdlFileData.hitboxSetOffset = theInputFileReader.ReadInt32();
			}

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

			//Me.theMdlFileData.keyValueOffset = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.keyValueSize = Me.theInputFileReader.ReadInt32()

			//Me.theMdlFileData.localIkAutoPlayLockCount = Me.theInputFileReader.ReadInt32()
			//Me.theMdlFileData.localIkAutoPlayLockOffset = Me.theInputFileReader.ReadInt32()

			//Me.theMdlFileData.mass = Me.theInputFileReader.ReadSingle()
			//Me.theMdlFileData.contents = Me.theInputFileReader.ReadInt32()

			//For x As Integer = 0 To Me.theMdlFileData.unused.Length - 1
			//	Me.theMdlFileData.unused(x) = Me.theInputFileReader.ReadInt32()
			//Next

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

					theMdlFileData.theBones = new List<SourceMdlBone31>(theMdlFileData.boneCount);
					for (int i = 0; i < theMdlFileData.boneCount; i++)
					{
						boneInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlBone31 aBone = new SourceMdlBone31();

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

						//aBone.qAlignment = New SourceQuaternion()
						//aBone.qAlignment.x = Me.theInputFileReader.ReadSingle()
						//aBone.qAlignment.y = Me.theInputFileReader.ReadSingle()
						//aBone.qAlignment.z = Me.theInputFileReader.ReadSingle()
						//aBone.qAlignment.w = Me.theInputFileReader.ReadSingle()

						aBone.flags = theInputFileReader.ReadInt32();

						aBone.proceduralRuleType = theInputFileReader.ReadInt32();
						aBone.proceduralRuleOffset = theInputFileReader.ReadInt32();
						aBone.physicsBoneIndex = theInputFileReader.ReadInt32();
						aBone.surfacePropNameOffset = theInputFileReader.ReadInt32();

						//aBone.quat = New SourceQuaternion()
						//aBone.quat.x = Me.theInputFileReader.ReadSingle()
						//aBone.quat.y = Me.theInputFileReader.ReadSingle()
						//aBone.quat.z = Me.theInputFileReader.ReadSingle()
						//aBone.quat.w = Me.theInputFileReader.ReadSingle()

						//aBone.contents = Me.theInputFileReader.ReadInt32()

						//For x As Integer = 0 To aBone.unused.Length - 1
						//	aBone.unused(x) = Me.theInputFileReader.ReadInt32()
						//Next

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

						//If aBone.proceduralRuleOffset <> 0 Then
						//	If aBone.proceduralRuleType = SourceMdlBone37.STUDIO_PROC_AXISINTERP Then
						//		Me.ReadAxisInterpBone(boneInputFileStreamPosition, aBone)
						//	ElseIf aBone.proceduralRuleType = SourceMdlBone37.STUDIO_PROC_QUATINTERP Then
						//		Me.theMdlFileData.theProceduralBonesCommandIsUsed = True
						//		Me.ReadQuatInterpBone(boneInputFileStreamPosition, aBone)
						//		'ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
						//		'	Me.ReadJiggleBone(boneInputFileStreamPosition, aBone)
						//	Else
						//		Dim debug As Integer = 4242
						//	End If
						//End If

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

		//Public Sub ReadBoneControllers()
		//	If Me.theMdlFileData.boneControllerCount > 0 Then
		//		Dim boneControllerInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneControllerOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theBoneControllers = New List(Of SourceMdlBoneController37)(Me.theMdlFileData.boneControllerCount)
		//			For i As Integer = 0 To Me.theMdlFileData.boneControllerCount - 1
		//				boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aBoneController As New SourceMdlBoneController37()

		//				aBoneController.boneIndex = Me.theInputFileReader.ReadInt32()
		//				aBoneController.type = Me.theInputFileReader.ReadInt32()
		//				aBoneController.startBlah = Me.theInputFileReader.ReadSingle()
		//				aBoneController.endBlah = Me.theInputFileReader.ReadSingle()
		//				aBoneController.restIndex = Me.theInputFileReader.ReadInt32()
		//				aBoneController.inputField = Me.theInputFileReader.ReadInt32()
		//				For x As Integer = 0 To aBoneController.unused.Length - 1
		//					aBoneController.unused(x) = Me.theInputFileReader.ReadByte()
		//				Next

		//				Me.theMdlFileData.theBoneControllers.Add(aBoneController)

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'If aBoneController.nameOffset <> 0 Then
		//				'	Me.theInputFileReader.BaseStream.Seek(boneControllerInputFileStreamPosition + aBoneController.nameOffset, SeekOrigin.Begin)
		//				'	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//				'	aBoneController.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//				'	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//				'	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//				'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName")
		//				'	End If
		//				'Else
		//				'	aBoneController.theName = ""
		//				'End If

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers " + Me.theMdlFileData.theBoneControllers.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Public Sub ReadAttachments()
		//	If Me.theMdlFileData.localAttachmentCount > 0 Then
		//		Dim attachmentInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		Dim fileOffsetStart2 As Long
		//		Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAttachmentOffset, SeekOrigin.Begin)
		//			'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theAttachments = New List(Of SourceMdlAttachment37)(Me.theMdlFileData.localAttachmentCount)
		//			For i As Integer = 0 To Me.theMdlFileData.localAttachmentCount - 1
		//				attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				fileOffsetStart = Me.theInputFileReader.BaseStream.Position
		//				Dim anAttachment As New SourceMdlAttachment37()

		//				anAttachment.nameOffset = Me.theInputFileReader.ReadInt32
		//				anAttachment.type = Me.theInputFileReader.ReadInt32()
		//				anAttachment.boneIndex = Me.theInputFileReader.ReadInt32()
		//				anAttachment.localM11 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM12 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM13 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM14 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM21 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM22 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM23 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM24 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM31 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM32 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM33 = Me.theInputFileReader.ReadSingle()
		//				anAttachment.localM34 = Me.theInputFileReader.ReadSingle()

		//				Me.theMdlFileData.theAttachments.Add(anAttachment)

		//				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAttachment")

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				If anAttachment.nameOffset <> 0 Then
		//					Me.theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin)
		//					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//					anAttachment.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName = " + anAttachment.theName)
		//					End If
		//				Else
		//					anAttachment.theName = ""
		//				End If

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments " + Me.theMdlFileData.theAttachments.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		public void ReadHitboxes_MDL27to30()
		{
			if (theMdlFileData.hitboxCount_MDL27to30 > 0)
			{
				long hitboxInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(theMdlFileData.hitboxOffset_MDL27to30, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					theMdlFileData.theHitboxes_MDL27to30 = new List<SourceMdlHitbox31>(theMdlFileData.hitboxCount_MDL27to30);
					for (int j = 0; j < theMdlFileData.hitboxCount_MDL27to30; j++)
					{
						hitboxInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlHitbox31 aHitbox = new SourceMdlHitbox31();

						//DEBUG: Unknown because these are zeroes in HL2 beta leak pipe01_lcurve01.
						aHitbox.boneIndex = theInputFileReader.ReadInt32();
						aHitbox.groupIndex = theInputFileReader.ReadInt32();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
							theInputFileReader.ReadInt32();
						}

						aHitbox.boundingBoxMin.x = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.y = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.z = theInputFileReader.ReadSingle();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
						}

						aHitbox.boundingBoxMax.x = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.y = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.z = theInputFileReader.ReadSingle();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
						}

						theMdlFileData.theHitboxes_MDL27to30.Add(aHitbox);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//If aHitbox.nameOffset <> 0 Then
						//	'NOTE: The nameOffset is absolute offset in studiomdl.
						//	Me.theInputFileReader.BaseStream.Seek(aHitbox.nameOffset, SeekOrigin.Begin)
						//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						//	aHitbox.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitbox.theName = " + aHitbox.theName)
						//	End If
						//Else
						//	aHitbox.theName = ""
						//End If

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxes_MDL27to30 " + theMdlFileData.theHitboxes_MDL27to30.Count.ToString());
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

					theMdlFileData.theHitboxSets = new List<SourceMdlHitboxSet31>(theMdlFileData.hitboxSetCount);
					for (int i = 0; i < theMdlFileData.hitboxSetCount; i++)
					{
						hitboxSetInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlHitboxSet31 aHitboxSet = new SourceMdlHitboxSet31();

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
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		//Public Sub ReadBoneDescs()
		//	If Me.theMdlFileData.boneDescCount > 0 Then
		//		Dim boneDescInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		Dim fileOffsetStart2 As Long
		//		Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneDescOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theBoneDescs = New List(Of SourceMdlBoneDesc37)(Me.theMdlFileData.boneDescCount)
		//			For i As Integer = 0 To Me.theMdlFileData.boneCount - 1
		//				boneDescInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aBoneDesc As New SourceMdlBoneDesc37()

		//				aBoneDesc.nameOffset = Me.theInputFileReader.ReadInt32()
		//				aBoneDesc.parentBoneIndex = Me.theInputFileReader.ReadInt32()

		//				aBoneDesc.position = New SourceVector()
		//				aBoneDesc.position.x = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.position.y = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.position.z = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.rotation = New SourceVector()
		//				aBoneDesc.rotation.x = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.rotation.y = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.rotation.z = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.positionScale = New SourceVector()
		//				aBoneDesc.positionScale.x = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.positionScale.y = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.positionScale.z = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.rotationScale = New SourceVector()
		//				aBoneDesc.rotationScale.x = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.rotationScale.y = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.rotationScale.z = Me.theInputFileReader.ReadSingle()

		//				aBoneDesc.poseToBoneColumn0 = New SourceVector()
		//				aBoneDesc.poseToBoneColumn1 = New SourceVector()
		//				aBoneDesc.poseToBoneColumn2 = New SourceVector()
		//				aBoneDesc.poseToBoneColumn3 = New SourceVector()
		//				aBoneDesc.poseToBoneColumn0.x = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn1.x = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn2.x = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn3.x = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn0.y = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn1.y = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn2.y = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn3.y = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn0.z = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn1.z = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn2.z = Me.theInputFileReader.ReadSingle()
		//				aBoneDesc.poseToBoneColumn3.z = Me.theInputFileReader.ReadSingle()

		//				For x As Integer = 0 To aBoneDesc.unused.Length - 1
		//					aBoneDesc.unused(x) = Me.theInputFileReader.ReadInt32()
		//				Next

		//				Me.theMdlFileData.theBoneDescs.Add(aBoneDesc)

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				If aBoneDesc.nameOffset <> 0 Then
		//					Me.theInputFileReader.BaseStream.Seek(boneDescInputFileStreamPosition + aBoneDesc.nameOffset, SeekOrigin.Begin)
		//					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//					aBoneDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBoneDesc.theName = " + aBoneDesc.theName)
		//					End If
		//				ElseIf aBoneDesc.theName Is Nothing Then
		//					aBoneDesc.theName = ""
		//				End If

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneDescs " + Me.theMdlFileData.theBoneDescs.Count.ToString())

		//			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theBoneDescs alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Public Sub ReadAnimGroups()
		//	If Me.theMdlFileData.animationGroupCount > 0 Then
		//		'Dim seqInputFileStreamPosition As Long
		//		'Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.animationGroupOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theAnimGroups = New List(Of SourceMdlAnimGroup31)(Me.theMdlFileData.animationGroupCount)
		//			For i As Integer = 0 To Me.theMdlFileData.animationGroupCount - 1
		//				'seqInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aSeqDesc As New SourceMdlAnimGroup31()

		//				aSeqDesc.group = Me.theInputFileReader.ReadInt32()
		//				aSeqDesc.index = Me.theInputFileReader.ReadInt32()

		//				Me.theMdlFileData.theAnimGroups.Add(aSeqDesc)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimGroups " + Me.theMdlFileData.theAnimGroups.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theAnimGroups alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

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

					theMdlFileData.theSequenceDescs = new List<SourceMdlSequenceDesc31>(theMdlFileData.localSequenceCount);
					for (int i = 0; i < theMdlFileData.localSequenceCount; i++)
					{
						seqInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlSequenceDesc31 aSeqDesc = new SourceMdlSequenceDesc31();

						aSeqDesc.nameOffset = theInputFileReader.ReadInt32();
						aSeqDesc.activityNameOffset = theInputFileReader.ReadInt32();
						aSeqDesc.flags = theInputFileReader.ReadInt32();
						aSeqDesc.activity = theInputFileReader.ReadInt32();
						aSeqDesc.activityWeight = theInputFileReader.ReadInt32();
						aSeqDesc.eventCount = theInputFileReader.ReadInt32();
						aSeqDesc.eventOffset = theInputFileReader.ReadInt32();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
						}

						aSeqDesc.bbMin.x = theInputFileReader.ReadSingle();
						aSeqDesc.bbMin.y = theInputFileReader.ReadSingle();
						aSeqDesc.bbMin.z = theInputFileReader.ReadSingle();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
						}

						aSeqDesc.bbMax.x = theInputFileReader.ReadSingle();
						aSeqDesc.bbMax.y = theInputFileReader.ReadSingle();
						aSeqDesc.bbMax.z = theInputFileReader.ReadSingle();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
						}

						aSeqDesc.frameCount = theInputFileReader.ReadInt32();

						aSeqDesc.blendCount = theInputFileReader.ReadInt32();
						aSeqDesc.blendOffset = theInputFileReader.ReadInt32();

						aSeqDesc.sequenceGroup = theInputFileReader.ReadInt32();

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
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.BaseStream.Seek(inputFileStreamPosition + 4052, SeekOrigin.Begin);
						}
						else if (theMdlFileData.version == 28)
						{
							theInputFileReader.BaseStream.Seek(inputFileStreamPosition + 4044, SeekOrigin.Begin);
						}
						else if (theMdlFileData.version == 29 || theMdlFileData.version == 30)
						{
							theInputFileReader.BaseStream.Seek(inputFileStreamPosition + 4056, SeekOrigin.Begin);
						}
						else
						{
							theInputFileReader.BaseStream.Seek(inputFileStreamPosition + 4060, SeekOrigin.Begin);
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
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName = " + aSeqDesc.theActivityName);
							}
						}
						else
						{
							aSeqDesc.theActivityName = "";
						}

						//Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadEvents(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
						//Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)

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

					theMdlFileData.theSequenceGroups = new List<SourceMdlSequenceGroup31>(theMdlFileData.sequenceGroupCount);
					for (int sequenceGroupIndex = 0; sequenceGroupIndex < theMdlFileData.sequenceGroupCount; sequenceGroupIndex++)
					{
						sequenceGroupInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlSequenceGroup31 aSequenceGroup = new SourceMdlSequenceGroup31();

						aSequenceGroup.nameOffset = theInputFileReader.ReadInt32();
						aSequenceGroup.fileNameOffset = theInputFileReader.ReadInt32();
						aSequenceGroup.cacheOffset = theInputFileReader.ReadInt32();
						aSequenceGroup.data = theInputFileReader.ReadInt32();

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

		//Public Sub ReadTransitions()
		//	If Me.theMdlFileData.transitionCount > 0 Then
		//		'Dim boneInputFileStreamPosition As Long
		//		'Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.transitionOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theTransitions = New List(Of List(Of Integer))(Me.theMdlFileData.transitionCount)
		//			For entryNodeIndex As Integer = 0 To Me.theMdlFileData.transitionCount - 1
		//				Dim exitNodeTransitions As New List(Of Integer)(Me.theMdlFileData.transitionCount)
		//				For exitNodeIndex As Integer = 0 To Me.theMdlFileData.transitionCount - 1
		//					Dim aTransitionValue As Integer

		//					aTransitionValue = Me.theInputFileReader.ReadByte()

		//					exitNodeTransitions.Add(aTransitionValue)
		//				Next
		//				Me.theMdlFileData.theTransitions.Add(exitNodeTransitions)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTransitions " + Me.theMdlFileData.theTransitions.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theTransitions alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

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

					theMdlFileData.theAnimationDescs = new List<SourceMdlAnimationDesc31>(theMdlFileData.animationCount);
					for (int i = 0; i < theMdlFileData.animationCount; i++)
					{
						animationDescInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						//fileOffsetStart = Me.theInputFileReader.BaseStream.Position
						SourceMdlAnimationDesc31 anAnimationDesc = new SourceMdlAnimationDesc31();

						anAnimationDesc.nameOffset = theInputFileReader.ReadInt32();
						anAnimationDesc.fps = theInputFileReader.ReadSingle();
						anAnimationDesc.flags = theInputFileReader.ReadInt32();
						anAnimationDesc.frameCount = theInputFileReader.ReadInt32();
						anAnimationDesc.movementCount = theInputFileReader.ReadInt32();
						anAnimationDesc.movementOffset = theInputFileReader.ReadInt32();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
							theInputFileReader.ReadInt32();
						}

						anAnimationDesc.bbMin.x = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMin.y = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMin.z = theInputFileReader.ReadSingle();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
						}

						anAnimationDesc.bbMax.x = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMax.y = theInputFileReader.ReadSingle();
						anAnimationDesc.bbMax.z = theInputFileReader.ReadSingle();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadInt32();
						}

						anAnimationDesc.animOffset = theInputFileReader.ReadInt32();


						if (theMdlFileData.version == 27)
						{
							// Probably alignment filler.
							theInputFileReader.ReadInt32();
							theInputFileReader.ReadInt32();
							theInputFileReader.ReadInt32();
						}
						else if (theMdlFileData.version == 31)
						{
							anAnimationDesc.ikRuleCount = theInputFileReader.ReadInt32();
							anAnimationDesc.ikRuleOffset = theInputFileReader.ReadInt32();
						}

						//For x As Integer = 0 To anAnimationDesc.unused.Length - 1
						//	anAnimationDesc.unused(x) = Me.theInputFileReader.ReadInt32()
						//Next

						theMdlFileData.theAnimationDescs.Add(anAnimationDesc);

						//fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
						//Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc")

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						if (anAnimationDesc.nameOffset != 0)
						{
							theInputFileReader.BaseStream.Seek(animationDescInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin);
							fileOffsetStart2 = theInputFileReader.BaseStream.Position;

							anAnimationDesc.theName = FileManager.ReadNullTerminatedString(theInputFileReader);

							fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
							if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
							{
								theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAnimationDesc.theName = " + anAnimationDesc.theName);
							}

							if (anAnimationDesc.theName[0] == '@')
							{
								anAnimationDesc.theName = anAnimationDesc.theName.Remove(0, 1);
							}
						}
						else
						{
							anAnimationDesc.theName = "";
						}

						ReadAnimations(animationDescInputFileStreamPosition, anAnimationDesc);
						ReadMdlMovements(animationDescInputFileStreamPosition, anAnimationDesc);
						//Me.ReadMdlIkRules(animationDescInputFileStreamPosition, anAnimationDesc)

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs " + theMdlFileData.theAnimationDescs.Count.ToString());
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

					theMdlFileData.theBodyParts = new List<SourceMdlBodyPart31>(theMdlFileData.bodyPartCount);
					for (int i = 0; i < theMdlFileData.bodyPartCount; i++)
					{
						bodyPartInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlBodyPart31 aBodyPart = new SourceMdlBodyPart31();

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
						//'NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts *and* models.
						//If i = Me.theMdlFileData.bodyPartCount - 1 Then
						//	Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, Me.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment")
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

		//Public Sub ReadFlexDescs()
		//	If Me.theMdlFileData.flexDescCount > 0 Then
		//		Dim flexDescInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		Dim fileOffsetStart2 As Long
		//		Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexDescOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theFlexDescs = New List(Of SourceMdlFlexDesc)(Me.theMdlFileData.flexDescCount)
		//			For i As Integer = 0 To Me.theMdlFileData.flexDescCount - 1
		//				flexDescInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aFlexDesc As New SourceMdlFlexDesc()

		//				aFlexDesc.nameOffset = Me.theInputFileReader.ReadInt32()

		//				Me.theMdlFileData.theFlexDescs.Add(aFlexDesc)

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				If aFlexDesc.nameOffset <> 0 Then
		//					Me.theInputFileReader.BaseStream.Seek(flexDescInputFileStreamPosition + aFlexDesc.nameOffset, SeekOrigin.Begin)
		//					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//					aFlexDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexDesc.theName = " + aFlexDesc.theName)
		//					End If
		//				Else
		//					aFlexDesc.theName = ""
		//				End If

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexDescs " + theMdlFileData.theFlexDescs.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexDescs alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Public Sub ReadFlexControllers()
		//	If Me.theMdlFileData.flexControllerCount > 0 Then
		//		Dim flexControllerInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		Dim fileOffsetStart2 As Long
		//		Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexControllerOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theFlexControllers = New List(Of SourceMdlFlexController)(Me.theMdlFileData.flexControllerCount)
		//			For i As Integer = 0 To Me.theMdlFileData.flexControllerCount - 1
		//				flexControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aFlexController As New SourceMdlFlexController()

		//				aFlexController.typeOffset = Me.theInputFileReader.ReadInt32()
		//				aFlexController.nameOffset = Me.theInputFileReader.ReadInt32()
		//				aFlexController.localToGlobal = Me.theInputFileReader.ReadInt32()
		//				aFlexController.min = Me.theInputFileReader.ReadSingle()
		//				aFlexController.max = Me.theInputFileReader.ReadSingle()

		//				Me.theMdlFileData.theFlexControllers.Add(aFlexController)

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				If aFlexController.typeOffset <> 0 Then
		//					Me.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.typeOffset, SeekOrigin.Begin)
		//					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//					aFlexController.theType = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theType = " + aFlexController.theType)
		//					End If
		//				Else
		//					aFlexController.theType = ""
		//				End If

		//				If aFlexController.nameOffset <> 0 Then
		//					Me.theInputFileReader.BaseStream.Seek(flexControllerInputFileStreamPosition + aFlexController.nameOffset, SeekOrigin.Begin)
		//					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//					aFlexController.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexController.theName = " + aFlexController.theName)
		//					End If
		//				Else
		//					aFlexController.theName = ""
		//				End If

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			If Me.theMdlFileData.theFlexControllers.Count > 0 Then
		//				Me.theMdlFileData.theModelCommandIsUsed = True
		//			End If

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllers " + theMdlFileData.theFlexControllers.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexControllers alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Public Sub ReadFlexRules()
		//	If Me.theMdlFileData.flexRuleCount > 0 Then
		//		Dim flexRuleInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexRuleOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theFlexRules = New List(Of SourceMdlFlexRule)(Me.theMdlFileData.flexRuleCount)
		//			For i As Integer = 0 To Me.theMdlFileData.flexRuleCount - 1
		//				flexRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aFlexRule As New SourceMdlFlexRule()

		//				aFlexRule.flexIndex = Me.theInputFileReader.ReadInt32()
		//				aFlexRule.opCount = Me.theInputFileReader.ReadInt32()
		//				aFlexRule.opOffset = Me.theInputFileReader.ReadInt32()

		//				Me.theMdlFileData.theFlexRules.Add(aFlexRule)

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				Me.theMdlFileData.theFlexDescs(aFlexRule.flexIndex).theDescIsUsedByFlexRule = True
		//				Me.ReadFlexOps(flexRuleInputFileStreamPosition, aFlexRule)

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			If Me.theMdlFileData.theFlexRules.Count > 0 Then
		//				Me.theMdlFileData.theModelCommandIsUsed = True
		//			End If

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexRules " + theMdlFileData.theFlexRules.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexRules alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Public Sub ReadIkChains()
		//	If Me.theMdlFileData.ikChainCount > 0 Then
		//		Dim ikChainInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		Dim fileOffsetStart2 As Long
		//		Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.ikChainOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theIkChains = New List(Of SourceMdlIkChain37)(Me.theMdlFileData.ikChainCount)
		//			For i As Integer = 0 To Me.theMdlFileData.ikChainCount - 1
		//				ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anIkChain As New SourceMdlIkChain37()

		//				anIkChain.nameOffset = Me.theInputFileReader.ReadInt32()
		//				anIkChain.linkType = Me.theInputFileReader.ReadInt32()
		//				anIkChain.linkCount = Me.theInputFileReader.ReadInt32()
		//				anIkChain.linkOffset = Me.theInputFileReader.ReadInt32()

		//				Me.theMdlFileData.theIkChains.Add(anIkChain)

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				If anIkChain.nameOffset <> 0 Then
		//					Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.nameOffset, SeekOrigin.Begin)
		//					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//					anIkChain.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkChain.theName = " + anIkChain.theName)
		//					End If
		//				Else
		//					anIkChain.theName = ""
		//				End If

		//				Me.ReadIkLinks(ikChainInputFileStreamPosition, anIkChain)

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkChains " + theMdlFileData.theIkChains.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theIkChains alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Public Sub ReadIkLocks()
		//	If Me.theMdlFileData.localIkAutoPlayLockCount > 0 Then
		//		'Dim ikChainInputFileStreamPosition As Long
		//		'Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theIkLocks = New List(Of SourceMdlIkLock37)(Me.theMdlFileData.localIkAutoPlayLockCount)
		//			For i As Integer = 0 To Me.theMdlFileData.localIkAutoPlayLockCount - 1
		//				'ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anIkLock As New SourceMdlIkLock37()

		//				anIkLock.chainIndex = Me.theInputFileReader.ReadInt32()
		//				anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
		//				anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()

		//				Me.theMdlFileData.theIkLocks.Add(anIkLock)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks " + theMdlFileData.theIkLocks.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theIkLocks alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Public Sub ReadMouths()
		//	If Me.theMdlFileData.mouthCount > 0 Then
		//		'Dim mouthInputFileStreamPosition As Long
		//		'Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.mouthOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.theMouths = New List(Of SourceMdlMouth)(Me.theMdlFileData.mouthCount)
		//			For i As Integer = 0 To Me.theMdlFileData.mouthCount - 1
		//				'mouthInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aMouth As New SourceMdlMouth()
		//				aMouth.boneIndex = Me.theInputFileReader.ReadInt32()
		//				aMouth.forward.x = Me.theInputFileReader.ReadSingle()
		//				aMouth.forward.y = Me.theInputFileReader.ReadSingle()
		//				aMouth.forward.z = Me.theInputFileReader.ReadSingle()
		//				aMouth.flexDescIndex = Me.theInputFileReader.ReadInt32()
		//				Me.theMdlFileData.theMouths.Add(aMouth)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			If Me.theMdlFileData.theMouths.Count > 0 Then
		//				Me.theMdlFileData.theModelCommandIsUsed = True
		//			End If

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMouths " + theMdlFileData.theMouths.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theMouths alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Public Sub ReadPoseParamDescs()
		//	If Me.theMdlFileData.localPoseParamaterCount > 0 Then
		//		Dim poseInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		Dim fileOffsetStart2 As Long
		//		Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localPoseParameterOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			Me.theMdlFileData.thePoseParamDescs = New List(Of SourceMdlPoseParamDesc)(Me.theMdlFileData.localPoseParamaterCount)
		//			For i As Integer = 0 To Me.theMdlFileData.localPoseParamaterCount - 1
		//				poseInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aPoseParamDesc As New SourceMdlPoseParamDesc()
		//				aPoseParamDesc.nameOffset = Me.theInputFileReader.ReadInt32()
		//				aPoseParamDesc.flags = Me.theInputFileReader.ReadInt32()
		//				aPoseParamDesc.startingValue = Me.theInputFileReader.ReadSingle()
		//				aPoseParamDesc.endingValue = Me.theInputFileReader.ReadSingle()
		//				aPoseParamDesc.loopingRange = Me.theInputFileReader.ReadSingle()
		//				Me.theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc)

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				If aPoseParamDesc.nameOffset <> 0 Then
		//					Me.theInputFileReader.BaseStream.Seek(poseInputFileStreamPosition + aPoseParamDesc.nameOffset, SeekOrigin.Begin)
		//					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//					aPoseParamDesc.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aPoseParamDesc.theName = " + aPoseParamDesc.theName)
		//					End If
		//				Else
		//					aPoseParamDesc.theName = ""
		//				End If

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs " + theMdlFileData.thePoseParamDescs.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.thePoseParamDescs alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

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

				theMdlFileData.theTextures = new List<SourceMdlTexture31>(theMdlFileData.textureCount);
				for (int i = 0; i < theMdlFileData.textureCount; i++)
				{
					textureInputFileStreamPosition = theInputFileReader.BaseStream.Position;
					SourceMdlTexture31 aTexture = new SourceMdlTexture31();

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

						aTexture.thePathFileName = FileManager.ReadNullTerminatedString(theInputFileReader);

						// Convert all forward slashes to backward slashes.
						aTexture.thePathFileName = FileManager.GetNormalizedPathFileName(aTexture.thePathFileName);

						fileOffsetEnd2 = theInputFileReader.BaseStream.Position - 1;
						if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theName = " + aTexture.thePathFileName);
						}
					}
					else
					{
						aTexture.thePathFileName = "";
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
						if (!theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2))
						{
							theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath = " + aTexturePath);
						}
					}
					else
					{
						aTexturePath = "";
					}
					theMdlFileData.theTexturePaths.Add(aTexturePath);

					theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
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
				}

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies");

				theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment");
			}
		}

		//Public Sub ReadFinalBytesAlignment()
		//	Me.theMdlFileData.theFileSeekLog.LogAndAlignFromFileSeekLogEnd(Me.theInputFileReader, 4, "Final bytes alignment")
		//End Sub

		public void ReadUnreadBytes()
		{
			theMdlFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

#endregion

#region Private Methods

		//'TODO: VERIFY ReadAxisInterpBone()
		//Private Sub ReadAxisInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone37)
		//	Dim axisInterpBoneInputFileStreamPosition As Long
		//	Dim inputFileStreamPosition As Long
		//	Dim fileOffsetStart As Long
		//	Dim fileOffsetEnd As Long

		//	Try
		//		Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
		//		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//		axisInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//		aBone.theAxisInterpBone = New SourceMdlAxisInterpBone()
		//		aBone.theAxisInterpBone.control = Me.theInputFileReader.ReadInt32()
		//		For x As Integer = 0 To aBone.theAxisInterpBone.pos.Length - 1
		//			aBone.theAxisInterpBone.pos(x).x = Me.theInputFileReader.ReadSingle()
		//			aBone.theAxisInterpBone.pos(x).y = Me.theInputFileReader.ReadSingle()
		//			aBone.theAxisInterpBone.pos(x).z = Me.theInputFileReader.ReadSingle()
		//		Next
		//		For x As Integer = 0 To aBone.theAxisInterpBone.quat.Length - 1
		//			aBone.theAxisInterpBone.quat(x).x = Me.theInputFileReader.ReadSingle()
		//			aBone.theAxisInterpBone.quat(x).y = Me.theInputFileReader.ReadSingle()
		//			aBone.theAxisInterpBone.quat(x).z = Me.theInputFileReader.ReadSingle()
		//			aBone.theAxisInterpBone.quat(x).z = Me.theInputFileReader.ReadSingle()
		//		Next

		//		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//		'If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
		//		'	Me.ReadTriggers(axisInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
		//		'End If

		//		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

		//		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone")
		//	Catch ex As Exception
		//		Dim debug As Integer = 4242
		//	End Try
		//End Sub

		//Private Sub ReadQuatInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone37)
		//	Dim quatInterpBoneInputFileStreamPosition As Long
		//	Dim inputFileStreamPosition As Long
		//	Dim fileOffsetStart As Long
		//	Dim fileOffsetEnd As Long

		//	Try
		//		Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
		//		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//		quatInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//		aBone.theQuatInterpBone = New SourceMdlQuatInterpBone()
		//		aBone.theQuatInterpBone.controlBoneIndex = Me.theInputFileReader.ReadInt32()
		//		aBone.theQuatInterpBone.triggerCount = Me.theInputFileReader.ReadInt32()
		//		aBone.theQuatInterpBone.triggerOffset = Me.theInputFileReader.ReadInt32()

		//		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//		If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
		//			Me.ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
		//		End If

		//		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

		//		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone")
		//	Catch ex As Exception
		//		Dim debug As Integer = 4242
		//	End Try
		//End Sub

		//Private Sub ReadTriggers(ByVal quatInterpBoneInputFileStreamPosition As Long, ByVal aQuatInterpBone As SourceMdlQuatInterpBone)
		//	Dim fileOffsetStart As Long
		//	Dim fileOffsetEnd As Long

		//	Try
		//		Me.theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin)
		//		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//		aQuatInterpBone.theTriggers = New List(Of SourceMdlQuatInterpBoneInfo)(aQuatInterpBone.triggerCount)
		//		For j As Integer = 0 To aQuatInterpBone.triggerCount - 1
		//			Dim aTrigger As New SourceMdlQuatInterpBoneInfo()

		//			aTrigger.inverseToleranceAngle = Me.theInputFileReader.ReadSingle()

		//			aTrigger.trigger = New SourceQuaternion()
		//			aTrigger.trigger.x = Me.theInputFileReader.ReadSingle()
		//			aTrigger.trigger.y = Me.theInputFileReader.ReadSingle()
		//			aTrigger.trigger.z = Me.theInputFileReader.ReadSingle()
		//			aTrigger.trigger.w = Me.theInputFileReader.ReadSingle()

		//			aTrigger.pos = New SourceVector()
		//			aTrigger.pos.x = Me.theInputFileReader.ReadSingle()
		//			aTrigger.pos.y = Me.theInputFileReader.ReadSingle()
		//			aTrigger.pos.z = Me.theInputFileReader.ReadSingle()

		//			aTrigger.quat = New SourceQuaternion()
		//			aTrigger.quat.x = Me.theInputFileReader.ReadSingle()
		//			aTrigger.quat.y = Me.theInputFileReader.ReadSingle()
		//			aTrigger.quat.z = Me.theInputFileReader.ReadSingle()
		//			aTrigger.quat.w = Me.theInputFileReader.ReadSingle()

		//			aQuatInterpBone.theTriggers.Add(aTrigger)
		//		Next

		//		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aQuatInterpBone.theTriggers " + aQuatInterpBone.theTriggers.Count.ToString())
		//	Catch ex As Exception
		//		Dim debug As Integer = 4242
		//	End Try
		//End Sub

		private void ReadHitboxes(long hitboxSetInputFileStreamPosition, SourceMdlHitboxSet31 aHitboxSet)
		{
			if (aHitboxSet.hitboxCount > 0)
			{
				long hitboxInputFileStreamPosition = 0;
				long inputFileStreamPosition = 0;
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					aHitboxSet.theHitboxes = new List<SourceMdlHitbox31>(aHitboxSet.hitboxCount);
					for (int j = 0; j < aHitboxSet.hitboxCount; j++)
					{
						hitboxInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlHitbox31 aHitbox = new SourceMdlHitbox31();

						aHitbox.boneIndex = theInputFileReader.ReadInt32();
						aHitbox.groupIndex = theInputFileReader.ReadInt32();
						aHitbox.boundingBoxMin.x = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.y = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMin.z = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.x = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.y = theInputFileReader.ReadSingle();
						aHitbox.boundingBoxMax.z = theInputFileReader.ReadSingle();
						//aHitbox.nameOffset = Me.theInputFileReader.ReadInt32()
						//For x As Integer = 0 To aHitbox.unused.Length - 1
						//	aHitbox.unused(x) = Me.theInputFileReader.ReadByte()
						//Next

						aHitboxSet.theHitboxes.Add(aHitbox);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//If aHitbox.nameOffset <> 0 Then
						//	'NOTE: The nameOffset is absolute offset in studiomdl.
						//	Me.theInputFileReader.BaseStream.Seek(aHitbox.nameOffset, SeekOrigin.Begin)
						//	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

						//	aHitbox.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

						//	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
						//	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						//		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitbox.theName = " + aHitbox.theName)
						//	End If
						//Else
						//	aHitbox.theName = ""
						//End If

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

		//Private Sub ReadPoseKeys(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		//	If (aSeqDesc.groupSize(0) > 1 OrElse aSeqDesc.groupSize(1) > 1) AndAlso aSeqDesc.poseKeyOffset <> 0 Then
		//		Try
		//			Dim poseKeyCount As Integer
		//			poseKeyCount = aSeqDesc.groupSize(0) + aSeqDesc.groupSize(1)
		//			Dim poseKeyInputFileStreamPosition As Long
		//			'Dim inputFileStreamPosition As Long
		//			Dim fileOffsetStart As Long
		//			Dim fileOffsetEnd As Long

		//			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.poseKeyOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aSeqDesc.thePoseKeys = New List(Of Double)(poseKeyCount)
		//			For j As Integer = 0 To poseKeyCount - 1
		//				poseKeyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aPoseKey As Double
		//				aPoseKey = Me.theInputFileReader.ReadSingle()
		//				aSeqDesc.thePoseKeys.Add(aPoseKey)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.thePoseKeys " + aSeqDesc.thePoseKeys.Count.ToString())
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadEvents(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		//	If aSeqDesc.eventCount > 0 AndAlso aSeqDesc.eventOffset <> 0 Then
		//		Try
		//			'Dim eventInputFileStreamPosition As Long
		//			'Dim inputFileStreamPosition As Long
		//			Dim fileOffsetStart As Long
		//			Dim fileOffsetEnd As Long
		//			'Dim fileOffsetStart2 As Long
		//			'Dim fileOffsetEnd2 As Long

		//			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aSeqDesc.theEvents = New List(Of SourceMdlEvent37)(aSeqDesc.eventCount)
		//			For j As Integer = 0 To aSeqDesc.eventCount - 1
		//				'eventInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anEvent As New SourceMdlEvent37()

		//				anEvent.cycle = Me.theInputFileReader.ReadSingle()
		//				anEvent.eventIndex = Me.theInputFileReader.ReadInt32()
		//				anEvent.eventType = Me.theInputFileReader.ReadInt32()
		//				For x As Integer = 0 To anEvent.options.Length - 1
		//					anEvent.options(x) = Me.theInputFileReader.ReadChar()
		//				Next

		//				aSeqDesc.theEvents.Add(anEvent)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theEvents " + aSeqDesc.theEvents.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theEvents alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadAutoLayers(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		//	If aSeqDesc.autoLayerCount > 0 AndAlso aSeqDesc.autoLayerOffset <> 0 Then
		//		Try
		//			Dim autoLayerInputFileStreamPosition As Long
		//			'Dim inputFileStreamPosition As Long
		//			Dim fileOffsetStart As Long
		//			Dim fileOffsetEnd As Long

		//			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aSeqDesc.theAutoLayers = New List(Of SourceMdlAutoLayer37)(aSeqDesc.autoLayerCount)
		//			For j As Integer = 0 To aSeqDesc.autoLayerCount - 1
		//				autoLayerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anAutoLayer As New SourceMdlAutoLayer37()

		//				anAutoLayer.sequenceIndex = Me.theInputFileReader.ReadInt32()
		//				anAutoLayer.flags = Me.theInputFileReader.ReadInt32()
		//				anAutoLayer.influenceStart = Me.theInputFileReader.ReadSingle()
		//				anAutoLayer.influencePeak = Me.theInputFileReader.ReadSingle()
		//				anAutoLayer.influenceTail = Me.theInputFileReader.ReadSingle()
		//				anAutoLayer.influenceEnd = Me.theInputFileReader.ReadSingle()

		//				aSeqDesc.theAutoLayers.Add(anAutoLayer)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAutoLayers " + aSeqDesc.theAutoLayers.Count.ToString())
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadMdlAnimBoneWeights(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		//	If Me.theMdlFileData.boneCount > 0 AndAlso aSeqDesc.weightOffset > 0 Then
		//		Try
		//			Dim weightListInputFileStreamPosition As Long
		//			'Dim inputFileStreamPosition As Long
		//			Dim fileOffsetStart As Long
		//			Dim fileOffsetEnd As Long
		//			'Dim fileOffsetStart2 As Long
		//			'Dim fileOffsetEnd2 As Long

		//			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.weightOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aSeqDesc.theBoneWeightsAreDefault = True
		//			aSeqDesc.theBoneWeights = New List(Of Double)(Me.theMdlFileData.boneCount)
		//			For j As Integer = 0 To Me.theMdlFileData.boneCount - 1
		//				weightListInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anAnimBoneWeight As Double
		//				anAnimBoneWeight = Me.theInputFileReader.ReadSingle()
		//				aSeqDesc.theBoneWeights.Add(anAnimBoneWeight)

		//				If anAnimBoneWeight <> 1 Then
		//					aSeqDesc.theBoneWeightsAreDefault = False
		//				End If

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
		//				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theBoneWeights " + aSeqDesc.theBoneWeights.Count.ToString())
		//			End If
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadSequenceIkLocks(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		//	If aSeqDesc.ikLockCount > 0 AndAlso aSeqDesc.ikLockOffset <> 0 Then
		//		Try
		//			Dim lockInputFileStreamPosition As Long
		//			'Dim inputFileStreamPosition As Long
		//			Dim fileOffsetStart As Long
		//			Dim fileOffsetEnd As Long

		//			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aSeqDesc.theIkLocks = New List(Of SourceMdlIkLock37)(aSeqDesc.ikLockCount)
		//			For j As Integer = 0 To aSeqDesc.ikLockCount - 1
		//				lockInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anIkLock As New SourceMdlIkLock37()

		//				anIkLock.chainIndex = Me.theInputFileReader.ReadInt32()
		//				anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
		//				anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()

		//				aSeqDesc.theIkLocks.Add(anIkLock)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theIkLocks " + aSeqDesc.theIkLocks.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theIkLocks alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadMdlAnimIndexes(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
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

		//Private Sub ReadSequenceKeyValues(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc37)
		//	If aSeqDesc.keyValueSize > 0 AndAlso aSeqDesc.keyValueOffset <> 0 Then
		//		Try
		//			Dim fileOffsetStart As Long
		//			Dim fileOffsetEnd As Long

		//			Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aSeqDesc.theKeyValues = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theKeyValues")

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aSeqDesc.theKeyValues alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		private void ReadAnimations(long animationDescInputFileStreamPosition, SourceMdlAnimationDesc31 anAnimationDesc)
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

					anAnimationDesc.theAnimations = new List<SourceMdlAnimation31>(theMdlFileData.theBones.Count);
					for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
					{
						animationInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						fileOffsetStart = theInputFileReader.BaseStream.Position;
						SourceMdlAnimation31 anAnimation = new SourceMdlAnimation31();

						//'anAnimation.flags = Me.theInputFileReader.ReadInt32()
						//'If (anAnimation.flags And SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0 Then
						//'	anAnimation.animationValueOffsets(0) = Me.theInputFileReader.ReadInt32()
						//'	anAnimation.animationValueOffsets(1) = Me.theInputFileReader.ReadInt32()
						//'	anAnimation.animationValueOffsets(2) = Me.theInputFileReader.ReadInt32()
						//'Else
						//anAnimation.position = New SourceVector()
						//anAnimation.position.x = Me.theInputFileReader.ReadSingle()
						//anAnimation.position.y = Me.theInputFileReader.ReadSingle()
						//anAnimation.position.z = Me.theInputFileReader.ReadSingle()
						//'End If
						//'If (anAnimation.flags And SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0 Then
						//'	anAnimation.animationValueOffsets(3) = Me.theInputFileReader.ReadInt32()
						//'	anAnimation.animationValueOffsets(4) = Me.theInputFileReader.ReadInt32()
						//'	anAnimation.animationValueOffsets(5) = Me.theInputFileReader.ReadInt32()
						//'	'anAnimation.unused = Me.theInputFileReader.ReadInt32()
						//'Else
						//anAnimation.rotationQuat = New SourceQuaternion()
						//anAnimation.rotationQuat.x = Me.theInputFileReader.ReadSingle()
						//anAnimation.rotationQuat.y = Me.theInputFileReader.ReadSingle()
						//anAnimation.rotationQuat.z = Me.theInputFileReader.ReadSingle()
						//anAnimation.rotationQuat.w = Me.theInputFileReader.ReadSingle()
						//'End If
						anAnimation.unknown = theInputFileReader.ReadSingle();
						anAnimation.theOffsets[0] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[1] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[2] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[3] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[4] = theInputFileReader.ReadInt32();
						anAnimation.theOffsets[5] = theInputFileReader.ReadInt32();

						anAnimationDesc.theAnimations.Add(anAnimation);

						fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
						theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation");

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//If (anAnimation.flags And SourceMdlAnimation37.STUDIO_POS_ANIMATED) > 0 Then
						//	Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 0, anAnimationDesc.frameCount, animationValuesEnd)
						//	Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 1, anAnimationDesc.frameCount, animationValuesEnd)
						//	Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 2, anAnimationDesc.frameCount, animationValuesEnd)
						//End If
						//If (anAnimation.flags And SourceMdlAnimation37.STUDIO_ROT_ANIMATED) > 0 Then
						//	Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 3, anAnimationDesc.frameCount, animationValuesEnd)
						//	Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 4, anAnimationDesc.frameCount, animationValuesEnd)
						//	Me.ReadAnimationValues(animationInputFileStreamPosition, anAnimation, 5, anAnimationDesc.frameCount, animationValuesEnd)
						//	'anAnimation.unused = Me.theInputFileReader.ReadInt32()
						//End If
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

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}
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

		//Private Sub ReadAnimationValues(ByVal animationInputFileStreamPosition As Long, ByVal anAnimation As SourceMdlAnimation31, ByVal offsetIndex As Integer, ByVal frameCount As Integer, ByRef fileOffsetEnd As Long)
		//	If anAnimation.animationValueOffsets(offsetIndex) > 0 Then
		//		Dim fileOffsetStart As Long
		//		'Dim fileOffsetEnd As Long
		//		Dim frameCountRemainingToBeChecked As Integer
		//		Dim currentTotal As Byte
		//		Dim validCount As Byte
		//		Dim animValues As List(Of SourceMdlAnimationValue)

		//		anAnimation.theAnimationValues(offsetIndex) = New List(Of SourceMdlAnimationValue)()
		//		animValues = anAnimation.theAnimationValues(offsetIndex)

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(animationInputFileStreamPosition + anAnimation.animationValueOffsets(offsetIndex), SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			frameCountRemainingToBeChecked = frameCount
		//			While (frameCountRemainingToBeChecked > 0)
		//				Dim animValue As New SourceMdlAnimationValue()
		//				animValue.value = Me.theInputFileReader.ReadInt16()
		//				currentTotal = animValue.total
		//				If currentTotal = 0 Then
		//					Dim badIfThisIsReached As Integer = 42
		//					Exit While
		//				End If
		//				frameCountRemainingToBeChecked -= currentTotal
		//				animValues.Add(animValue)

		//				validCount = animValue.valid
		//				For i As Integer = 1 To validCount
		//					Dim animValue2 As New SourceMdlAnimationValue()
		//					animValue2.value = Me.theInputFileReader.ReadInt16()
		//					animValues.Add(animValue2)
		//				Next
		//			End While

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimation.theAnimationValues")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadMdlMovements(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc37)
		//	If anAnimationDesc.movementCount > 0 Then
		//		Dim movementInputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.movementOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			anAnimationDesc.theMovements = New List(Of SourceMdlMovement)(anAnimationDesc.movementCount)
		//			For j As Integer = 0 To anAnimationDesc.movementCount - 1
		//				movementInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aMovement As New SourceMdlMovement()

		//				aMovement.endframeIndex = Me.theInputFileReader.ReadInt32()
		//				aMovement.motionFlags = Me.theInputFileReader.ReadInt32()
		//				aMovement.v0 = Me.theInputFileReader.ReadSingle()
		//				aMovement.v1 = Me.theInputFileReader.ReadSingle()
		//				aMovement.angle = Me.theInputFileReader.ReadSingle()

		//				aMovement.vector = New SourceVector()
		//				aMovement.vector.x = Me.theInputFileReader.ReadSingle()
		//				aMovement.vector.y = Me.theInputFileReader.ReadSingle()
		//				aMovement.vector.z = Me.theInputFileReader.ReadSingle()
		//				aMovement.position = New SourceVector()
		//				aMovement.position.x = Me.theInputFileReader.ReadSingle()
		//				aMovement.position.y = Me.theInputFileReader.ReadSingle()
		//				aMovement.position.z = Me.theInputFileReader.ReadSingle()

		//				anAnimationDesc.theMovements.Add(aMovement)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theMovements " + anAnimationDesc.theMovements.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theMovements alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadMdlIkRules(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc37)
		//	If anAnimationDesc.ikRuleCount > 0 Then
		//		Dim ikRuleInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			anAnimationDesc.theIkRules = New List(Of SourceMdlIkRule37)(anAnimationDesc.ikRuleCount)
		//			For j As Integer = 0 To anAnimationDesc.ikRuleCount - 1
		//				ikRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anIkRule As New SourceMdlIkRule37()

		//				anIkRule.index = Me.theInputFileReader.ReadInt32()
		//				anIkRule.type = Me.theInputFileReader.ReadInt32()
		//				anIkRule.chain = Me.theInputFileReader.ReadInt32()
		//				anIkRule.bone = Me.theInputFileReader.ReadInt32()

		//				anIkRule.slot = Me.theInputFileReader.ReadInt32()
		//				anIkRule.height = Me.theInputFileReader.ReadSingle()
		//				anIkRule.radius = Me.theInputFileReader.ReadSingle()
		//				anIkRule.floor = Me.theInputFileReader.ReadSingle()

		//				anIkRule.pos = New SourceVector()
		//				anIkRule.pos.x = Me.theInputFileReader.ReadSingle()
		//				anIkRule.pos.y = Me.theInputFileReader.ReadSingle()
		//				anIkRule.pos.z = Me.theInputFileReader.ReadSingle()
		//				anIkRule.q = New SourceQuaternion()
		//				anIkRule.q.x = Me.theInputFileReader.ReadSingle()
		//				anIkRule.q.y = Me.theInputFileReader.ReadSingle()
		//				anIkRule.q.z = Me.theInputFileReader.ReadSingle()
		//				anIkRule.q.w = Me.theInputFileReader.ReadSingle()

		//				anIkRule.weight = Me.theInputFileReader.ReadSingle()
		//				anIkRule.group = Me.theInputFileReader.ReadInt32()
		//				anIkRule.ikErrorIndexStart = Me.theInputFileReader.ReadInt32()
		//				anIkRule.ikErrorOffset = Me.theInputFileReader.ReadInt32()

		//				anIkRule.influenceStart = Me.theInputFileReader.ReadSingle()
		//				anIkRule.influencePeak = Me.theInputFileReader.ReadSingle()
		//				anIkRule.influenceTail = Me.theInputFileReader.ReadSingle()
		//				anIkRule.influenceEnd = Me.theInputFileReader.ReadSingle()

		//				anIkRule.commit = Me.theInputFileReader.ReadSingle()
		//				anIkRule.contact = Me.theInputFileReader.ReadSingle()
		//				anIkRule.pivot = Me.theInputFileReader.ReadSingle()
		//				anIkRule.release = Me.theInputFileReader.ReadSingle()

		//				anAnimationDesc.theIkRules.Add(anIkRule)

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				Me.ReadMdlIkErrors(ikRuleInputFileStreamPosition, anIkRule, anAnimationDesc.frameCount)

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkRules " + anAnimationDesc.theIkRules.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadMdlIkErrors(ByVal ikRuleInputFileStreamPosition As Long, ByVal anIkRule As SourceMdlIkRule37, ByVal frameCount As Integer)
		//	'pikrule->start	= g_panimation[i]->ikrule[j].start / (g_panimation[i]->numframes - 1.0f);
		//	'pikrule->end	= g_panimation[i]->ikrule[j].end / (g_panimation[i]->numframes - 1.0f);
		//	'pRule->numerror = pRule->end - pRule->start + 1;
		//	'if (pRule->end >= panim->numframes)
		//	'	pRule->numerror = pRule->numerror + 2;
		//	Dim ikErrorStart As Integer
		//	Dim ikErrorEnd As Integer
		//	Dim ikErrorCount As Integer
		//	ikErrorStart = CInt(anIkRule.influenceStart * (frameCount - 1))
		//	ikErrorEnd = CInt(anIkRule.influenceEnd * (frameCount - 1))
		//	ikErrorCount = ikErrorEnd - ikErrorStart + 1
		//	If ikErrorEnd >= frameCount Then
		//		ikErrorCount += 2
		//	End If

		//	If ikErrorCount > 0 Then
		//		'Dim ikErrorInputFileStreamPosition As Long
		//		'Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.ikErrorOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			anIkRule.theIkErrors = New List(Of SourceMdlIkError37)(ikErrorCount)
		//			For j As Integer = 0 To ikErrorCount - 1
		//				'ikErrorInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anIkError As New SourceMdlIkError37()

		//				anIkError.pos = New SourceVector()
		//				anIkError.pos.x = Me.theInputFileReader.ReadSingle()
		//				anIkError.pos.y = Me.theInputFileReader.ReadSingle()
		//				anIkError.pos.z = Me.theInputFileReader.ReadSingle()
		//				anIkError.q = New SourceQuaternion()
		//				anIkError.q.x = Me.theInputFileReader.ReadSingle()
		//				anIkError.q.y = Me.theInputFileReader.ReadSingle()
		//				anIkError.q.z = Me.theInputFileReader.ReadSingle()
		//				anIkError.q.w = Me.theInputFileReader.ReadSingle()

		//				anIkRule.theIkErrors.Add(anIkError)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkErrors " + anIkRule.theIkErrors.Count.ToString())
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadFlexOps(ByVal flexRuleInputFileStreamPosition As Long, ByVal aFlexRule As SourceMdlFlexRule)
		//	If aFlexRule.opCount > 0 AndAlso aFlexRule.opOffset <> 0 Then
		//		'Dim flexRuleInputFileStreamPosition As Long
		//		'Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(flexRuleInputFileStreamPosition + aFlexRule.opOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aFlexRule.theFlexOps = New List(Of SourceMdlFlexOp)(aFlexRule.opCount)
		//			For i As Integer = 0 To aFlexRule.opCount - 1
		//				'flexRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aFlexOp As New SourceMdlFlexOp()

		//				aFlexOp.op = Me.theInputFileReader.ReadInt32()
		//				If aFlexOp.op = SourceMdlFlexOp.STUDIO_CONST Then
		//					aFlexOp.value = Me.theInputFileReader.ReadSingle()
		//				Else
		//					aFlexOp.index = Me.theInputFileReader.ReadInt32()
		//					If aFlexOp.op = SourceMdlFlexOp.STUDIO_FETCH2 Then
		//						Me.theMdlFileData.theFlexDescs(aFlexOp.index).theDescIsUsedByFlexRule = True
		//					End If
		//				End If

		//				aFlexRule.theFlexOps.Add(aFlexOp)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlexRule.theFlexOps " + aFlexRule.theFlexOps.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "theMdlFileData.theFlexOps alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadIkLinks(ByVal ikChainInputFileStreamPosition As Long, ByVal anIkChain As SourceMdlIkChain37)
		//	If anIkChain.linkCount > 0 Then
		//		'Dim ikLinkInputFileStreamPosition As Long
		//		'Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			anIkChain.theLinks = New List(Of SourceMdlIkLink37)(anIkChain.linkCount)
		//			For j As Integer = 0 To anIkChain.linkCount - 1
		//				'ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anIkLink As New SourceMdlIkLink37()

		//				anIkLink.boneIndex = Me.theInputFileReader.ReadInt32()
		//				anIkLink.contact.x = Me.theInputFileReader.ReadSingle()
		//				anIkLink.contact.y = Me.theInputFileReader.ReadSingle()
		//				anIkLink.contact.z = Me.theInputFileReader.ReadSingle()
		//				anIkLink.limits.x = Me.theInputFileReader.ReadSingle()
		//				anIkLink.limits.y = Me.theInputFileReader.ReadSingle()
		//				anIkLink.limits.z = Me.theInputFileReader.ReadSingle()

		//				anIkChain.theLinks.Add(anIkLink)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks " + anIkChain.theLinks.Count.ToString())
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		private void ReadMdlMovements(long animInputFileStreamPosition, SourceMdlAnimationDesc31 anAnimationDesc)
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

		private void ReadModels(long bodyPartInputFileStreamPosition, SourceMdlBodyPart31 aBodyPart)
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

					aBodyPart.theModels = new List<SourceMdlModel31>(aBodyPart.modelCount);
					for (int j = 0; j < aBodyPart.modelCount; j++)
					{
						modelInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlModel31 aModel = new SourceMdlModel31();

						aModel.name = theInputFileReader.ReadChars(aModel.name.Length);
						aModel.theName = (new string(aModel.name)).Trim('\0');
						aModel.type = theInputFileReader.ReadInt32();
						aModel.boundingRadius = theInputFileReader.ReadSingle();
						aModel.meshCount = theInputFileReader.ReadInt32();
						aModel.meshOffset = theInputFileReader.ReadInt32();

						aModel.vertexCount = theInputFileReader.ReadInt32();
						aModel.vertexOffset = theInputFileReader.ReadInt32();

						// MDL 27 to 30 
						if (theMdlFileData.version >= 27 && theMdlFileData.version <= 30)
						{
							aModel.normalOffset_MDL27to30 = theInputFileReader.ReadInt32();
						}

						aModel.tangentOffset = theInputFileReader.ReadInt32();

						// MDL 27 to 30 
						if (theMdlFileData.version >= 27 && theMdlFileData.version <= 30)
						{
							aModel.texCoordOffset_MDL27to30 = theInputFileReader.ReadInt32();
							aModel.boneWeightsOffset_MDL27to30 = theInputFileReader.ReadInt32();
						}

						aModel.attachmentCount = theInputFileReader.ReadInt32();
						aModel.attachmentOffset = theInputFileReader.ReadInt32();
						aModel.eyeballCount = theInputFileReader.ReadInt32();
						aModel.eyeballOffset = theInputFileReader.ReadInt32();

						//For x As Integer = 0 To aModel.unused.Length - 1
						//	aModel.unused(x) = Me.theInputFileReader.ReadInt32()
						//Next

						aBodyPart.theModels.Add(aModel);

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//'NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
						//Me.ReadEyeballs(modelInputFileStreamPosition, aModel)
						ReadMeshes(modelInputFileStreamPosition, aModel);
						ReadVertexes(modelInputFileStreamPosition, aModel);
						ReadTangents(modelInputFileStreamPosition, aModel);

						// MDL 27 to 30 
						//NOTE: These must be called after ReadVertexes().
						if (theMdlFileData.version >= 27 && theMdlFileData.version <= 30)
						{
							ReadBoneWeights(modelInputFileStreamPosition, aModel);
							ReadNormals(modelInputFileStreamPosition, aModel);
							ReadTexCoords(modelInputFileStreamPosition, aModel);
						}

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

		//Private Sub ReadEyeballs(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel37)
		//	If aModel.eyeballCount > 0 AndAlso aModel.eyeballOffset <> 0 Then
		//		Dim eyeballInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		Dim fileOffsetStart2 As Long
		//		Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.eyeballOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aModel.theEyeballs = New List(Of SourceMdlEyeball37)(aModel.eyeballCount)
		//			For eyeballIndex As Integer = 0 To aModel.eyeballCount - 1
		//				eyeballInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim anEyeball As New SourceMdlEyeball37()

		//				anEyeball.nameOffset = Me.theInputFileReader.ReadInt32()
		//				anEyeball.boneIndex = Me.theInputFileReader.ReadInt32()
		//				anEyeball.org = New SourceVector()
		//				anEyeball.org.x = Me.theInputFileReader.ReadSingle()
		//				anEyeball.org.y = Me.theInputFileReader.ReadSingle()
		//				anEyeball.org.z = Me.theInputFileReader.ReadSingle()
		//				anEyeball.zOffset = Me.theInputFileReader.ReadSingle()
		//				anEyeball.radius = Me.theInputFileReader.ReadSingle()
		//				anEyeball.up = New SourceVector()
		//				anEyeball.up.x = Me.theInputFileReader.ReadSingle()
		//				anEyeball.up.y = Me.theInputFileReader.ReadSingle()
		//				anEyeball.up.z = Me.theInputFileReader.ReadSingle()
		//				anEyeball.forward = New SourceVector()
		//				anEyeball.forward.x = Me.theInputFileReader.ReadSingle()
		//				anEyeball.forward.y = Me.theInputFileReader.ReadSingle()
		//				anEyeball.forward.z = Me.theInputFileReader.ReadSingle()
		//				anEyeball.texture = Me.theInputFileReader.ReadInt32()

		//				anEyeball.irisMaterial = Me.theInputFileReader.ReadInt32()
		//				anEyeball.irisScale = Me.theInputFileReader.ReadSingle()
		//				anEyeball.glintMaterial = Me.theInputFileReader.ReadInt32()

		//				anEyeball.upperFlexDesc(0) = Me.theInputFileReader.ReadInt32()
		//				anEyeball.upperFlexDesc(1) = Me.theInputFileReader.ReadInt32()
		//				anEyeball.upperFlexDesc(2) = Me.theInputFileReader.ReadInt32()
		//				anEyeball.lowerFlexDesc(0) = Me.theInputFileReader.ReadInt32()
		//				anEyeball.lowerFlexDesc(1) = Me.theInputFileReader.ReadInt32()
		//				anEyeball.lowerFlexDesc(2) = Me.theInputFileReader.ReadInt32()
		//				anEyeball.upperTarget(0) = Me.theInputFileReader.ReadSingle()
		//				anEyeball.upperTarget(1) = Me.theInputFileReader.ReadSingle()
		//				anEyeball.upperTarget(2) = Me.theInputFileReader.ReadSingle()
		//				anEyeball.lowerTarget(0) = Me.theInputFileReader.ReadSingle()
		//				anEyeball.lowerTarget(1) = Me.theInputFileReader.ReadSingle()
		//				anEyeball.lowerTarget(2) = Me.theInputFileReader.ReadSingle()

		//				anEyeball.upperLidFlexDesc = Me.theInputFileReader.ReadInt32()
		//				anEyeball.lowerLidFlexDesc = Me.theInputFileReader.ReadInt32()

		//				anEyeball.pitch(0) = Me.theInputFileReader.ReadSingle()
		//				anEyeball.pitch(1) = Me.theInputFileReader.ReadSingle()
		//				anEyeball.yaw(0) = Me.theInputFileReader.ReadSingle()
		//				anEyeball.yaw(1) = Me.theInputFileReader.ReadSingle()

		//				aModel.theEyeballs.Add(anEyeball)

		//				'NOTE: Set the default value to -1 to distinguish it from value assigned to it by ReadMeshes().
		//				anEyeball.theTextureIndex = -1

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'NOTE: The mdl file doesn't appear to store the eyeball name; studiomdl only uses it internally with eyelids.
		//				If anEyeball.nameOffset <> 0 Then
		//					Me.theInputFileReader.BaseStream.Seek(eyeballInputFileStreamPosition + anEyeball.nameOffset, SeekOrigin.Begin)
		//					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		//					anEyeball.theName = FileManager.ReadNullTerminatedString(Me.theInputFileReader)

		//					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position - 1
		//					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
		//						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEyeball.theName = " + anEyeball.theName)
		//					End If
		//				Else
		//					anEyeball.theName = ""
		//				End If

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			If aModel.theEyeballs.Count > 0 Then
		//				Me.theMdlFileData.theModelCommandIsUsed = True
		//			End If

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theEyeballs " + aModel.theEyeballs.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theEyeballs alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		private void ReadMeshes(long modelInputFileStreamPosition, SourceMdlModel31 aModel)
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

					aModel.theMeshes = new List<SourceMdlMesh31>(aModel.meshCount);
					for (int meshIndex = 0; meshIndex < aModel.meshCount; meshIndex++)
					{
						meshInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlMesh31 aMesh = new SourceMdlMesh31();

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

						if (theMdlFileData.version == 27)
						{
							for (int x = 0; x < aMesh.unused_MDL27.Length; x++)
							{
								aMesh.unused_MDL27[x] = theInputFileReader.ReadInt32();
							}
						}
						else
						{
							for (int x = 0; x < aMesh.unused.Length; x++)
							{
								aMesh.unused[x] = theInputFileReader.ReadInt32();
							}
						}

						aModel.theMeshes.Add(aMesh);

						//' Fill-in eyeball texture index info.
						//If aMesh.materialType = 1 Then
						//	aModel.theEyeballs(aMesh.materialParam).theTextureIndex = aMesh.materialIndex
						//End If

						inputFileStreamPosition = theInputFileReader.BaseStream.Position;

						//If aMesh.flexCount > 0 AndAlso aMesh.flexOffset <> 0 Then
						//	Me.ReadFlexes(meshInputFileStreamPosition, aMesh)
						//End If

						theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes " + aModel.theMeshes.Count.ToString());

					//Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aModel.theMeshes alignment")
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadVertexes(long modelInputFileStreamPosition, SourceMdlModel31 aModel)
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

					aModel.theVertexes = new List<SourceMdlVertex31>(aModel.vertexCount);
					for (int vertexIndex = 0; vertexIndex < aModel.vertexCount; vertexIndex++)
					{
						vertexInputFileStreamPosition = theInputFileReader.BaseStream.Position;
						SourceMdlVertex31 aVertex = new SourceMdlVertex31();

						// MDL 31 
						if (theMdlFileData.version >= 31)
						{
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
						}
						aVertex.position.x = theInputFileReader.ReadSingle();
						aVertex.position.y = theInputFileReader.ReadSingle();
						aVertex.position.z = theInputFileReader.ReadSingle();

						if (theMdlFileData.version == 27)
						{
							// MDL 27 - Probably alignment filler.
							theInputFileReader.ReadSingle();
						}
						else if (theMdlFileData.version >= 31)
						{
							// MDL 31 
							aVertex.normal.x = theInputFileReader.ReadSingle();
							aVertex.normal.y = theInputFileReader.ReadSingle();
							aVertex.normal.z = theInputFileReader.ReadSingle();
							aVertex.texCoordX = theInputFileReader.ReadSingle();
							aVertex.texCoordY = theInputFileReader.ReadSingle();
						}

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

		private void ReadTangents(long modelInputFileStreamPosition, SourceMdlModel31 aModel)
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

		private void ReadBoneWeights(long modelInputFileStreamPosition, SourceMdlModel31 aModel)
		{
			if (aModel.vertexCount > 0 && aModel.boneWeightsOffset_MDL27to30 != 0 && aModel.theVertexes != null)
			{
				//Dim vertexInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.boneWeightsOffset_MDL27to30, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					foreach (SourceMdlVertex31 aVertex in aModel.theVertexes)
					{
						//vertexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
						SourceMdlBoneWeight31 aBoneWeight = new SourceMdlBoneWeight31();

						aVertex.boneWeight.bone[0] = theInputFileReader.ReadInt32();
						aVertex.boneWeight.bone[1] = theInputFileReader.ReadInt32();
						aVertex.boneWeight.bone[2] = theInputFileReader.ReadInt32();
						aVertex.boneWeight.bone[3] = theInputFileReader.ReadInt32();
						aVertex.boneWeight.weight[0] = theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[1] = theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[2] = theInputFileReader.ReadSingle();
						aVertex.boneWeight.weight[3] = theInputFileReader.ReadSingle();
						aVertex.boneWeight.boneCount = theInputFileReader.ReadInt16();
						aVertex.boneWeight.material = theInputFileReader.ReadInt16();
						aVertex.boneWeight.firstRef = theInputFileReader.ReadInt16();
						aVertex.boneWeight.lastRef = theInputFileReader.ReadInt16();

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theBoneWeights " + aModel.theVertexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadNormals(long modelInputFileStreamPosition, SourceMdlModel31 aModel)
		{
			if (aModel.vertexCount > 0 && aModel.normalOffset_MDL27to30 != 0 && aModel.theVertexes != null)
			{
				//Dim vertexInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.normalOffset_MDL27to30, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					foreach (SourceMdlVertex31 aVertex in aModel.theVertexes)
					{
						//vertexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						aVertex.normal.x = theInputFileReader.ReadSingle();
						aVertex.normal.y = theInputFileReader.ReadSingle();
						aVertex.normal.z = theInputFileReader.ReadSingle();

						//MDL27 - Probably alignment filler.
						if (theMdlFileData.version == 27)
						{
							theInputFileReader.ReadSingle();
						}

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theNormals " + aModel.theVertexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		private void ReadTexCoords(long modelInputFileStreamPosition, SourceMdlModel31 aModel)
		{
			if (aModel.vertexCount > 0 && aModel.texCoordOffset_MDL27to30 != 0 && aModel.theVertexes != null)
			{
				//Dim vertexInputFileStreamPosition As Long
				//Dim inputFileStreamPosition As Long
				long fileOffsetStart = 0;
				long fileOffsetEnd = 0;
				//Dim fileOffsetStart2 As Long
				//Dim fileOffsetEnd2 As Long

				try
				{
					theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.texCoordOffset_MDL27to30, SeekOrigin.Begin);
					fileOffsetStart = theInputFileReader.BaseStream.Position;

					foreach (SourceMdlVertex31 aVertex in aModel.theVertexes)
					{
						//vertexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						aVertex.texCoordX = theInputFileReader.ReadSingle();
						aVertex.texCoordY = theInputFileReader.ReadSingle();

						//inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

						//Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
					}

					fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
					theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theTexCoords " + aModel.theVertexes.Count.ToString());
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
		}

		//Private Sub ReadFlexes(ByVal meshInputFileStreamPosition As Long, ByVal aMesh As SourceMdlMesh37)
		//	If aMesh.flexCount > 0 AndAlso aMesh.flexOffset <> 0 Then
		//		Dim flexInputFileStreamPosition As Long
		//		Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(meshInputFileStreamPosition + aMesh.flexOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aMesh.theFlexes = New List(Of SourceMdlFlex37)(aMesh.flexCount)
		//			For k As Integer = 0 To aMesh.flexCount - 1
		//				flexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aFlex As New SourceMdlFlex37()

		//				aFlex.flexDescIndex = Me.theInputFileReader.ReadInt32()

		//				aFlex.target0 = Me.theInputFileReader.ReadSingle()
		//				aFlex.target1 = Me.theInputFileReader.ReadSingle()
		//				aFlex.target2 = Me.theInputFileReader.ReadSingle()
		//				aFlex.target3 = Me.theInputFileReader.ReadSingle()

		//				aFlex.vertCount = Me.theInputFileReader.ReadInt32()
		//				aFlex.vertOffset = Me.theInputFileReader.ReadInt32()

		//				aMesh.theFlexes.Add(aFlex)

		//				''NOTE: Set the frame index here because it is determined by order of flexes in mdl file.
		//				''      Start the indexing at 1 because first frame (frame 0) is "basis" frame.
		//				'Me.theCurrentFrameIndex += 1
		//				'Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theVtaFrameIndex = Me.theCurrentFrameIndex

		//				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				Me.ReadVertAnims(flexInputFileStreamPosition, aFlex)

		//				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aMesh.theFlexes " + aMesh.theFlexes.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aMesh.theFlexes alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

		//Private Sub ReadVertAnims(ByVal flexInputFileStreamPosition As Long, ByVal aFlex As SourceMdlFlex37)
		//	If aFlex.vertCount > 0 AndAlso aFlex.vertOffset <> 0 Then
		//		'Dim vertAnimInputFileStreamPosition As Long
		//		'Dim inputFileStreamPosition As Long
		//		Dim fileOffsetStart As Long
		//		Dim fileOffsetEnd As Long
		//		'Dim fileOffsetStart2 As Long
		//		'Dim fileOffsetEnd2 As Long

		//		Try
		//			Me.theInputFileReader.BaseStream.Seek(flexInputFileStreamPosition + aFlex.vertOffset, SeekOrigin.Begin)
		//			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		//			aFlex.theVertAnims = New List(Of SourceMdlVertAnim37)(aFlex.vertCount)
		//			For k As Integer = 0 To aFlex.vertCount - 1
		//				'vertAnimInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		//				Dim aVertAnim As New SourceMdlVertAnim37()

		//				aVertAnim.index = Me.theInputFileReader.ReadInt32()
		//				aVertAnim.delta.x = Me.theInputFileReader.ReadSingle()
		//				aVertAnim.delta.y = Me.theInputFileReader.ReadSingle()
		//				aVertAnim.delta.z = Me.theInputFileReader.ReadSingle()
		//				aVertAnim.nDelta.x = Me.theInputFileReader.ReadSingle()
		//				aVertAnim.nDelta.y = Me.theInputFileReader.ReadSingle()
		//				aVertAnim.nDelta.z = Me.theInputFileReader.ReadSingle()

		//				aFlex.theVertAnims.Add(aVertAnim)

		//				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		//				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		//			Next

		//			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		//			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aFlex.theVertAnims " + aFlex.theVertAnims.Count.ToString())

		//			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "aFlex.theVertAnims alignment")
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		End Try
		//	End If
		//End Sub

#endregion

#region Data

		protected BinaryReader theInputFileReader;
		protected BinaryWriter theOutputFileWriter;

		protected SourceMdlFileData31 theMdlFileData;

#endregion

	}

}