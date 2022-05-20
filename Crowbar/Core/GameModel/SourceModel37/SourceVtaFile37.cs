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
	public class SourceVtaFile37
	{

#region Creation and Destruction

		public SourceVtaFile37(StreamWriter outputFileStream, SourceMdlFileData37 mdlFileData)
		{
			this.theOutputFileStreamWriter = outputFileStream;
			this.theMdlFileData = mdlFileData;
		}

#endregion

#region Methods

		public void WriteHeaderComment()
		{
			Common.WriteHeaderComment(this.theOutputFileStreamWriter);
		}

		public void WriteHeaderSection()
		{
			string line = "";

			//version 1
			line = "version 1";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteNodesSection()
		{
			string line = "";
			string name = null;

			//nodes
			line = "nodes";
			this.theOutputFileStreamWriter.WriteLine(line);

			for (int boneIndex = 0; boneIndex < this.theMdlFileData.theBones.Count; boneIndex++)
			{
				name = this.theMdlFileData.theBones[boneIndex].theName;

				line = "  ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " \"";
				line += name;
				line += "\" ";
				line += this.theMdlFileData.theBones[boneIndex].parentBoneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				this.theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSkeletonSectionForVertexAnimation()
		{
			string line = "";

			//skeleton
			line = "skeleton";
			this.theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0 # basis shape key";
			}
			else
			{
				line = "  time 0 # basis shape key";
			}
			this.theOutputFileStreamWriter.WriteLine(line);

			int timeIndex = 0;
			int flexTimeIndex = 0;
			FlexFrame37 aFlexFrame = null;

			timeIndex = 1;
			//NOTE: The first frame was written in code above.
			for (flexTimeIndex = 1; flexTimeIndex < this.theMdlFileData.theFlexFrames.Count; flexTimeIndex++)
			{
				aFlexFrame = this.theMdlFileData.theFlexFrames[flexTimeIndex];

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
				this.theOutputFileStreamWriter.WriteLine(line);

				timeIndex += 1;
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteVertexAnimationSection()
		{
			string line = "";
			SourceMdlVertex37 aVertex = null;

			//vertexanimation
			line = "vertexanimation";
			this.theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0 # basis shape key";
			}
			else
			{
				line = "  time 0 # basis shape key";
			}
			this.theOutputFileStreamWriter.WriteLine(line);

			try
			{
				for (int vertexIndex = 0; vertexIndex < this.theMdlFileData.theVertexes.Count; vertexIndex++)
				{
					aVertex = this.theMdlFileData.theVertexes[vertexIndex];

					line = "    ";
					line += vertexIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normal.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normal.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += aVertex.normal.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					this.theOutputFileStreamWriter.WriteLine(line);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			int timeIndex = 0;
			int flexTimeIndex = 0;
			FlexFrame37 aFlexFrame = null;

			timeIndex = 1;
			//NOTE: The first frame was written in code above.
			for (flexTimeIndex = 1; flexTimeIndex < this.theMdlFileData.theFlexFrames.Count; flexTimeIndex++)
			{
				aFlexFrame = this.theMdlFileData.theFlexFrames[flexTimeIndex];

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
				this.theOutputFileStreamWriter.WriteLine(line);

				for (int x = 0; x < aFlexFrame.flexes.Count; x++)
				{
					this.WriteVertexAnimLines(aFlexFrame.flexes[x], aFlexFrame.bodyAndMeshVertexIndexStarts[x]);
				}

				timeIndex += 1;
			}

			line = "end";
			this.theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private void WriteVertexAnimLines(SourceMdlFlex37 aFlex, int bodyAndMeshVertexIndexStart)
		{
			string line = null;
			SourceMdlVertex37 aVertex = null;
			int vertexIndex = 0;
			double positionX = 0;
			double positionY = 0;
			double positionZ = 0;
			double normalX = 0;
			double normalY = 0;
			double normalZ = 0;

			for (int i = 0; i < aFlex.theVertAnims.Count; i++)
			{
				SourceMdlVertAnim37 aVertAnim = aFlex.theVertAnims[i];

				vertexIndex = aVertAnim.index + bodyAndMeshVertexIndexStart;
				aVertex = this.theMdlFileData.theVertexes[vertexIndex];

				positionX = aVertex.position.x + aVertAnim.delta.x;
				positionY = aVertex.position.y + aVertAnim.delta.y;
				positionZ = aVertex.position.z + aVertAnim.delta.z;
				normalX = aVertex.normal.x + aVertAnim.nDelta.x;
				normalY = aVertex.normal.y + aVertAnim.nDelta.y;
				normalZ = aVertex.normal.z + aVertAnim.nDelta.z;
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

				// For debugging.
				//line += " // "
				//line += aVertAnim.flDelta(0).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flDelta(1).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flDelta(2).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flNDelta(0).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flNDelta(1).the16BitValue.ToString()
				//line += " "
				//line += aVertAnim.flNDelta(2).the16BitValue.ToString()

				this.theOutputFileStreamWriter.WriteLine(line);
			}
		}

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData37 theMdlFileData;

#endregion

	}

}