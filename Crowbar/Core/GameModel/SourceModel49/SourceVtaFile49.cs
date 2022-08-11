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
	public class SourceVtaFile49
	{

#region Creation and Destruction

		public SourceVtaFile49(StreamWriter outputFileStream, SourceMdlFileData49 mdlFileData, SourceVvdFileData04 vvdFileData, SourceMdlBodyPart bodyPart)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
			theVvdFileData = vvdFileData;
			theBodyPart = bodyPart;
		}

#endregion

#region Methods

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(theOutputFileStreamWriter);
		}

		public void WriteHeaderSection()
		{
			string line = "";

			//version 1
			line = "version 1";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteNodesSection()
		{
			string line = "";
			string name = null;

			//nodes
			line = "nodes";
			theOutputFileStreamWriter.WriteLine(line);

			for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
			{
				name = theMdlFileData.theBones[boneIndex].theName;

				line = "  ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " \"";
				line += name;
				line += "\" ";
				line += theMdlFileData.theBones[boneIndex].parentBoneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSkeletonSectionForVertexAnimation()
		{
			string line = "";

			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0 # basis shape key";
			}
			else
			{
				line = "  time 0 # basis shape key";
			}
			theOutputFileStreamWriter.WriteLine(line);

			int timeIndex = 0;
			int flexTimeIndex = 0;
			FlexFrame aFlexFrame = null;

			timeIndex = 1;
			//NOTE: The first frame was written in code above.
			//For flexTimeIndex = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
			//	aFlexFrame = Me.theMdlFileData.theFlexFrames(flexTimeIndex)
			for (flexTimeIndex = 1; flexTimeIndex < theBodyPart.theFlexFrames.Count; flexTimeIndex++)
			{
				aFlexFrame = theBodyPart.theFlexFrames[flexTimeIndex];

				if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
				{
					line = "time ";
				}
				else
				{
					line = "  time ";
				}
				line += timeIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " # ";
				line += aFlexFrame.flexDescription;
				theOutputFileStreamWriter.WriteLine(line);

				timeIndex += 1;
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteVertexAnimationSection()
		{
			string line = "";

			line = "vertexanimation";
			theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0 # basis shape key";
			}
			else
			{
				line = "  time 0 # basis shape key";
			}
			theOutputFileStreamWriter.WriteLine(line);

			int beginVertexIndex = 0;
			int endVertexIndex = 0;
			int bodyVertexCount = 0;
			SourceMdlBodyPart aBodyPart = null;
			SourceMdlModel aModel = null;
			for (int bodyPartIndex = 0; bodyPartIndex < theMdlFileData.theBodyParts.Count; bodyPartIndex++)
			{
				aBodyPart = theMdlFileData.theBodyParts[bodyPartIndex];

				if (theBodyPart == aBodyPart)
				{
					beginVertexIndex = bodyVertexCount;
					endVertexIndex = bodyVertexCount;
				}

				if (aBodyPart.theModels != null && aBodyPart.theModels.Count > 0)
				{
					for (int modelIndex = 0; modelIndex < aBodyPart.theModels.Count; modelIndex++)
					{
						aModel = aBodyPart.theModels[modelIndex];
						bodyVertexCount += aModel.vertexCount;
					}
				}

				if (theBodyPart == aBodyPart)
				{
					endVertexIndex = bodyVertexCount - 1;
				}
			}

			try
			{
				SourceVertex aVertex = null;
				//For vertexIndex As Integer = 0 To Me.theVvdFileData.theVertexes.Count - 1
				for (int vertexIndex = beginVertexIndex; vertexIndex <= endVertexIndex; vertexIndex++)
				{
					if (theVvdFileData.fixupCount == 0)
					{
						aVertex = theVvdFileData.theVertexes[vertexIndex];
					}
					else
					{
						//NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
						//      Maybe the listing by lodIndex is only needed internally by graphics engine.
						aVertex = theVvdFileData.theFixedVertexesByLod[0][vertexIndex];
					}

					line = "    ";
					line += vertexIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.positionX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.positionY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.positionZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normalX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normalY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normalZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					theOutputFileStreamWriter.WriteLine(line);
				}
			}
			catch
			{
			}

			int timeIndex = 0;
			int flexTimeIndex = 0;
			FlexFrame aFlexFrame = null;

			timeIndex = 1;
			//NOTE: The first frame was written in code above.
			//For flexTimeIndex = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
			//	aFlexFrame = Me.theMdlFileData.theFlexFrames(flexTimeIndex)
			for (flexTimeIndex = 1; flexTimeIndex < theBodyPart.theFlexFrames.Count; flexTimeIndex++)
			{
				aFlexFrame = theBodyPart.theFlexFrames[flexTimeIndex];

				if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
				{
					line = "time ";
				}
				else
				{
					line = "  time ";
				}
				line += timeIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " # ";
				line += aFlexFrame.flexDescription;
				theOutputFileStreamWriter.WriteLine(line);

				for (int x = 0; x < aFlexFrame.flexes.Count; x++)
				{
					WriteVertexAnimLines(aFlexFrame.flexes[x], aFlexFrame.bodyAndMeshVertexIndexStarts[x]);
				}

				timeIndex += 1;
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private void WriteVertexAnimLines(SourceMdlFlex aFlex, int bodyAndMeshVertexIndexStart)
		{
			string line = null;
			SourceVertex aVertex = null;
			int vertexIndex = 0;
			double positionX = 0;
			double positionY = 0;
			double positionZ = 0;
			double normalX = 0;
			double normalY = 0;
			double normalZ = 0;

			for (int i = 0; i < aFlex.theVertAnims.Count; i++)
			{
				SourceMdlVertAnim aVertAnim = aFlex.theVertAnims[i];

				//TODO: Figure out why decompiling teen angst zoey (which has 39 shape keys) gives 55 shapekeys.
				//      - Probably extra ones are related to flexpairs (right and left).
				//      - Eyelids are combined, e.g. second shapekey from source vta is upper_lid_lowerer
				//        that contains both upper_right_lowerer and upper_left_lowerer.

				vertexIndex = aVertAnim.index + bodyAndMeshVertexIndexStart;
				if (theVvdFileData.fixupCount == 0)
				{
					aVertex = theVvdFileData.theVertexes[vertexIndex];
				}
				else
				{
					//NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
					//      Maybe the listing by lodIndex is only needed internally by graphics engine.
					aVertex = theVvdFileData.theFixedVertexesByLod[0][vertexIndex];
				}

				positionX = aVertex.positionX + aVertAnim.flDelta(0).TheFloatValue;
				positionY = aVertex.positionY + aVertAnim.flDelta(1).TheFloatValue;
				positionZ = aVertex.positionZ + aVertAnim.flDelta(2).TheFloatValue;
				normalX = aVertex.normalX + aVertAnim.flNDelta(0).TheFloatValue;
				normalY = aVertex.normalY + aVertAnim.flNDelta(1).TheFloatValue;
				normalZ = aVertex.normalZ + aVertAnim.flNDelta(2).TheFloatValue;
				line = "    ";
				line += vertexIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += positionX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += positionY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += positionZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normalX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normalY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normalZ.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				//TEST:
				//If aFlex.vertAnimType = aFlex.STUDIO_VERT_ANIM_WRINKLE Then
				//	CType(aVertAnim, SourceMdlVertAnimWrinkle).wrinkleDelta = Me.theInputFileReader.ReadInt16()
				//End If
				//If blah Then
				//	line += " // wrinkle value: "
				//	line += aVertAnim.flDelta(0).the16BitValue.ToString()
				//End If

				theOutputFileStreamWriter.WriteLine(line);
			}
		}

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData49 theMdlFileData;
		private SourceVvdFileData04 theVvdFileData;
		private SourceMdlBodyPart theBodyPart;

#endregion

	}

}