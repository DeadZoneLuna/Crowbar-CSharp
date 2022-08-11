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
	public class SourceSmdFile04
	{

#region Creation and Destruction

		public SourceSmdFile04(StreamWriter outputFileStream, SourceMdlFileData04 mdlFileData)
		{
			theOutputFileStreamWriter = outputFileStream;
			theMdlFileData = mdlFileData;
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
				name = "bone" + boneIndex.ToString();

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

		public void WriteSkeletonSection()
		{
			string line = "";
			SourceMdlBone04 aBone = null;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();

			//skeleton
			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				line = "time 0";
			}
			else
			{
				line = "  time 0";
			}
			theOutputFileStreamWriter.WriteLine(line);
			for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
			{
				aBone = theMdlFileData.theBones[boneIndex];

				if (aBone.parentBoneIndex == -1)
				{
					position.x = aBone.position.y;
					position.y = -aBone.position.x;
					position.z = aBone.position.z;

					rotation.x = 0;
					rotation.y = 0;
					rotation.z = MathModule.DegreesToRadians(-90);
				}
				else
				{
					position.x = aBone.position.x;
					position.y = aBone.position.y;
					position.z = aBone.position.z;

					rotation.x = 0;
					rotation.y = 0;
					rotation.z = 0;
				}

				//If aBone.parentBoneIndex = -1 Then
				//	position.x = aBone.positionY.TheFloatValue
				//	position.y = -aBone.positionX.TheFloatValue
				//	position.z = aBone.positionZ.TheFloatValue

				//	rotation.x = aBone.rotationX.TheFloatValue
				//	rotation.y = aBone.rotationY.TheFloatValue
				//	rotation.z = aBone.rotationZ.TheFloatValue + MathModule.DegreesToRadians(-90)
				//Else
				//	position.x = aBone.positionX.TheFloatValue
				//	position.y = aBone.positionY.TheFloatValue
				//	position.z = aBone.positionZ.TheFloatValue

				//	''rotation.x = MathModule.DegreesToRadians(aBone.rotationX / 200)
				//	''rotation.y = MathModule.DegreesToRadians(aBone.rotationY / 200)
				//	''rotation.z = MathModule.DegreesToRadians(aBone.rotationZ / 200)
				//	rotation.x = aBone.rotationX.TheFloatValue
				//	rotation.y = aBone.rotationY.TheFloatValue
				//	rotation.z = aBone.rotationZ.TheFloatValue
				//End If

				line = "    ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += rotation.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += rotation.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += rotation.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				theOutputFileStreamWriter.WriteLine(line);
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteSkeletonSectionForAnimation(SourceMdlSequenceDesc04 aSequenceDesc)
		{
			string line = "";
			SourceMdlBone04 aBone = null;
			SourceMdlSequence04 aSequence = null;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();

			//skeleton
			line = "skeleton";
			theOutputFileStreamWriter.WriteLine(line);

			for (int frameIndex = 0; frameIndex < aSequenceDesc.frameCount; frameIndex++)
			{
				if (MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
				{
					line = "time ";
				}
				else
				{
					line = "  time ";
				}
				line += frameIndex.ToString();
				theOutputFileStreamWriter.WriteLine(line);

				aSequence = aSequenceDesc.theSequences[frameIndex];
				for (int boneIndex = 0; boneIndex < theMdlFileData.theBones.Count; boneIndex++)
				{
					aBone = theMdlFileData.theBones[boneIndex];

					if (aBone.parentBoneIndex == -1)
					{
						position.x = aSequence.thePositionsAndRotations[boneIndex].position.y;
						position.y = -aSequence.thePositionsAndRotations[boneIndex].position.x;
						position.z = aSequence.thePositionsAndRotations[boneIndex].position.z;

						rotation.x = aSequence.thePositionsAndRotations[boneIndex].rotation.x;
						rotation.y = aSequence.thePositionsAndRotations[boneIndex].rotation.y;
						rotation.z = aSequence.thePositionsAndRotations[boneIndex].rotation.z + MathModule.DegreesToRadians(-90);
					}
					else
					{
						position.x = aSequence.thePositionsAndRotations[boneIndex].position.x;
						position.y = aSequence.thePositionsAndRotations[boneIndex].position.y;
						position.z = aSequence.thePositionsAndRotations[boneIndex].position.z;

						rotation.x = aSequence.thePositionsAndRotations[boneIndex].rotation.x;
						rotation.y = aSequence.thePositionsAndRotations[boneIndex].rotation.y;
						rotation.z = aSequence.thePositionsAndRotations[boneIndex].rotation.z;
					}

					line = "    ";
					line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);

					line += " ";
					line += position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

					line += " ";
					line += rotation.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += rotation.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
					line += " ";
					line += rotation.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

					//If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
					//	line += "   # "
					//	line += "pos: "
					//	line += aFrameLine.position.debug_text
					//	line += "   "
					//	line += "rot: "
					//	line += aFrameLine.rotation.debug_text
					//End If

					theOutputFileStreamWriter.WriteLine(line);
				}
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

		public void WriteTrianglesSection(SourceMdlModel04 aBodyModel)
		{
			string line = "";
			string materialLine = "";
			string vertex1Line = "";
			string vertex2Line = "";
			string vertex3Line = "";
			string materialName = null;
			SourceMdlMesh04 aMesh = null;

			//triangles
			line = "triangles";
			theOutputFileStreamWriter.WriteLine(line);

			try
			{
				if (aBodyModel.theMeshes != null)
				{
					for (int meshIndex = 0; meshIndex < aBodyModel.theMeshes.Count; meshIndex++)
					{
						aMesh = aBodyModel.theMeshes[meshIndex];
						materialName = aMesh.theTextureFileName;

						if (aMesh.theFaces != null)
						{
							for (int faceIndex = 0; faceIndex < aMesh.theFaces.Count; faceIndex++)
							{
								materialLine = materialName;

								vertex1Line = GetVertexLine(aBodyModel, aMesh, aMesh.theFaces[faceIndex].vertexInfo[2]);
								vertex2Line = GetVertexLine(aBodyModel, aMesh, aMesh.theFaces[faceIndex].vertexInfo[1]);
								vertex3Line = GetVertexLine(aBodyModel, aMesh, aMesh.theFaces[faceIndex].vertexInfo[0]);

								if (vertex1Line.StartsWith("// ") || vertex2Line.StartsWith("// ") || vertex3Line.StartsWith("// "))
								{
									materialLine = "// " + materialLine;
									if (!vertex1Line.StartsWith("// "))
									{
										vertex1Line = "// " + vertex1Line;
									}
									if (!vertex2Line.StartsWith("// "))
									{
										vertex2Line = "// " + vertex2Line;
									}
									if (!vertex3Line.StartsWith("// "))
									{
										vertex3Line = "// " + vertex3Line;
									}
								}
								theOutputFileStreamWriter.WriteLine(materialLine);
								theOutputFileStreamWriter.WriteLine(vertex1Line);
								theOutputFileStreamWriter.WriteLine(vertex2Line);
								theOutputFileStreamWriter.WriteLine(vertex3Line);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			line = "end";
			theOutputFileStreamWriter.WriteLine(line);
		}

#endregion

#region Private Delegates

#endregion

#region Private Methods

		private string GetVertexLine(SourceMdlModel04 aBodyModel, SourceMdlMesh04 aMesh, SourceMdlFaceVertexInfo04 aVertexInfo)
		{
			string line = null;
			int boneIndex = 0;
			SourceMdlBone04 aBone = null;
			SourceVector position = new SourceVector();
			SourceVector normal = new SourceVector();
			double texCoordX = 0;
			double texCoordY = 0;

			line = "";
			try
			{
				boneIndex = aBodyModel.theVertexes[aVertexInfo.vertexIndex].boneIndex;
				aBone = theMdlFileData.theBones[boneIndex];

				//position.x = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x
				//position.y = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
				//position.z = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z
				position.x = aBodyModel.theVertexes[aVertexInfo.vertexIndex].vector.y;
				position.y = -aBodyModel.theVertexes[aVertexInfo.vertexIndex].vector.x;
				position.z = aBodyModel.theVertexes[aVertexInfo.vertexIndex].vector.z;
				//position.x = aBone.position.x + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x
				//position.y = aBone.position.y + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
				//position.z = aBone.position.z + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z
				//position.x = aBone.position.y + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
				//position.y = -(aBone.position.x + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x)
				//position.z = aBone.position.z + aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z

				//position.x = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x
				//position.y = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
				//position.z = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z
				//position.x = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.y
				//position.y = -(aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.x)
				//position.z = aBodyModel.theVertexes(aVertexInfo.vertexIndex).vector.z

				normal.x = aBodyModel.theNormals[aVertexInfo.normalIndex].vector.x;
				normal.y = aBodyModel.theNormals[aVertexInfo.normalIndex].vector.y;
				normal.z = aBodyModel.theNormals[aVertexInfo.normalIndex].vector.z;

				if (aBodyModel.theVertexes[aVertexInfo.vertexIndex].boneIndex != aBodyModel.theNormals[aVertexInfo.normalIndex].index)
				{
					int debug = 4242;
				}

				texCoordX = aVertexInfo.s / (double)aMesh.textureWidth;
				texCoordY = aVertexInfo.t / (double)aMesh.textureHeight;
				//texCoordY = 1 - aVertexInfo.t / aMesh.textureHeight

				line = "  ";
				line += boneIndex.ToString(MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				line += position.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += position.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += position.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				line += normal.x.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normal.y.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += normal.z.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);

				line += " ";
				line += texCoordX.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				line += " ";
				line += texCoordY.ToString("0.000000", MainCROWBAR.TheApp.InternalNumberFormat);
				//line += (1 - texCoordY).ToString("0.000000", TheApp.InternalNumberFormat)
			}
			catch (Exception ex)
			{
				line = "// " + line;
			}

			return line;
		}

#endregion

#region Data

		private StreamWriter theOutputFileStreamWriter;
		private SourceMdlFileData04 theMdlFileData;

#endregion

	}

}