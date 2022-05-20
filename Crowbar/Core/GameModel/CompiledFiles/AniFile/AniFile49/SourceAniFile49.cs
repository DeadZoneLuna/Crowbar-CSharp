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
	public class SourceAniFile49 : SourceMdlFile49
	{
#region Creation and Destruction

		public SourceAniFile49(BinaryReader aniFileReader, SourceFileData aniFileData, SourceFileData mdlFileData)
		{
			this.theInputFileReader = aniFileReader;
			this.theMdlFileData = (SourceMdlFileData49)aniFileData;
			this.theRealMdlFileData = (SourceMdlFileData49)mdlFileData;

			this.theMdlFileData.theFileSeekLog.FileSize = this.theInputFileReader.BaseStream.Length;

			//NOTE: Need the bone data from the real MDL file because SourceAniFile inherits SourceMdlFile.ReadMdlAnimation() that uses the data.
			this.theMdlFileData.theBones = this.theRealMdlFileData.theBones;
		}

#endregion

#region Methods

		//TODO: [2015-08-16] Currently the same as SourceAniFile48. Not sure how to share the code while still having the two versions call different ReadAniAnimation().
		//Public Sub ReadAniBlocks(ByVal delegateReadAniAnimation As ReadAniAnimationDelegate)
		public void ReadAnimationAniBlocks()
		{
			if (this.theRealMdlFileData.theAnimationDescs != null)
			{
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionIndex = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int sectionFrameCount = 0;
//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				SourceMdlAnimationSection section = null;
				foreach (SourceMdlAnimationDesc49 anAnimationDesc in this.theRealMdlFileData.theAnimationDescs)
				{
					try
					{
						long animBlockInputFileStreamPosition = this.theRealMdlFileData.theAnimBlocks[anAnimationDesc.animBlock].dataStart;
						//Dim animBlockInputFileStreamEndPosition As Long = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataEnd
	//					Dim sectionIndex As Integer

						//NOTE: There can be sections in ANI file even if anAnimationDesc.animBlock = 0.
						if (anAnimationDesc.theSections != null && anAnimationDesc.theSections.Count > 0)
						{
							int sectionCount = anAnimationDesc.theSections.Count;
	//						Dim sectionFrameCount As Integer
	//						Dim section As SourceMdlAnimationSection

							for (sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++)
							{
								section = anAnimationDesc.theSections[sectionIndex];
								if (section.animBlock > 0)
								{
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

									animBlockInputFileStreamPosition = this.theRealMdlFileData.theAnimBlocks[section.animBlock].dataStart;
									//animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(section.animBlock).dataEnd
									this.ReadAnimationFrames(animBlockInputFileStreamPosition + section.animOffset, anAnimationDesc, sectionFrameCount, sectionIndex, (sectionIndex >= sectionCount - 2) || (anAnimationDesc.frameCount == (sectionIndex + 1) * anAnimationDesc.sectionFrameCount));
								}
							}
						}
						else if (anAnimationDesc.animBlock > 0)
						{
							sectionIndex = 0;
							this.ReadAnimationFrames(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex, true);
						}

						//NOTE: These seem to always be stored in the MDL file for MDL44.
						if (this.theMdlFileData.version != 44 && anAnimationDesc.animBlock > 0)
						{
							this.ReadMdlIkRules(animBlockInputFileStreamPosition + anAnimationDesc.animblockIkRuleOffset, anAnimationDesc);
							this.ReadLocalHierarchies(animBlockInputFileStreamPosition, anAnimationDesc);
						}
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
				}
			}
		}

#endregion

#region Private Methods

#endregion

#region Data

		protected SourceMdlFileData49 theRealMdlFileData;

#endregion

	}

}