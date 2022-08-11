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
	public class SourcePhyFile
	{

#region Creation and Destruction

		public SourcePhyFile(BinaryReader phyFileReader, SourcePhyFileData phyFileData, long endOffset = 0)
		{
			theInputFileReader = phyFileReader;
			thePhyFileData = phyFileData;
			thePhyEndOffset = endOffset;

			thePhyFileData.theFileSeekLog.FileSize = theInputFileReader.BaseStream.Length;
		}

#endregion

#region Methods

		public void ReadSourcePhyHeader()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			// Offsets: 0x00, 0x04, 0x08, 0x0C (12)
			//FROM: Zoey_TeenAngst
			//10 00 00 00 
			//00 00 00 00 
			//12 00 00 00 
			//1f de 9d 20 
			thePhyFileData.size = theInputFileReader.ReadInt32();
			thePhyFileData.id = theInputFileReader.ReadInt32();
			thePhyFileData.solidCount = theInputFileReader.ReadInt32();
			thePhyFileData.checksum = theInputFileReader.ReadInt32();

			//NOTE: If header size ever increases, this will at least skip over extra stuff.
			theInputFileReader.BaseStream.Seek(fileOffsetStart + thePhyFileData.size, SeekOrigin.Begin);

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Header");
		}

		public void ReadSourceCollisionData()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			string ivpsId;
			int triangleCount = 0;
			int triangleIndex = 0;
			List<int> vertices = null;
			long nextSolidDataStreamPosition = 0;
			long phyDataStreamPosition = 0;
			long faceDataStreamPosition = 0;
			long vertexDataStreamPosition = 0;
			long vertexDataOffset = 0;
			SourcePhyFaceSection faceSection = null;

			thePhyFileData.theSourcePhyMaxConvexPieces = 0;
			thePhyFileData.theSourcePhyCollisionDatas = new List<SourcePhyCollisionData>();
			for (int solidIndex = 0; solidIndex < thePhyFileData.solidCount; solidIndex++)
			{
				SourcePhyCollisionData collisionData = new SourcePhyCollisionData();
				collisionData.theFaceSections = new List<SourcePhyFaceSection>();
				collisionData.theVertices = new List<SourcePhyVertex>();

				fileOffsetStart = theInputFileReader.BaseStream.Position;

				//b8 01 00 00   size
				collisionData.size = theInputFileReader.ReadInt32();
				nextSolidDataStreamPosition = theInputFileReader.BaseStream.Position + collisionData.size;

				phyDataStreamPosition = theInputFileReader.BaseStream.Position;
				//56 50 48 59   VPHY
				string vphyId = new string(theInputFileReader.ReadChars(4));
				theInputFileReader.BaseStream.Seek(phyDataStreamPosition, SeekOrigin.Begin);
				if(vphyId != "VPHY")
				{
					ReadPhyData_VERSION37();
				}
				else
				{
					ReadPhyData_VERSION48();
				}

				//49 56 50 53   IVPS
				ivpsId = new string(theInputFileReader.ReadChars(4));

				vertices = new List<int>();
				vertexDataStreamPosition = theInputFileReader.BaseStream.Position + collisionData.size;
				while (theInputFileReader.BaseStream.Position < vertexDataStreamPosition)
				{
					faceSection = new SourcePhyFaceSection();

					faceDataStreamPosition = theInputFileReader.BaseStream.Position;

					//d0 00 00 00 
					//29 00 00 00 
					//04 15 00 00 
					vertexDataOffset = theInputFileReader.ReadInt32();
					vertexDataStreamPosition = faceDataStreamPosition + vertexDataOffset;

					if (vphyId != "VPHY")
					{
						// This is MDL v37 model, so use different code.
						faceSection.theBoneIndex = theInputFileReader.ReadInt32();
						if (thePhyFileData.solidCount == 1)
						{
							thePhyFileData.theSourcePhyIsCollisionModel = true;
						}
					}
					else
					{
						//TODO: Verify why this is using "- 1". Needed for L4D2 survivor_teenangst.
						faceSection.theBoneIndex = theInputFileReader.ReadInt32() - 1;
						if (faceSection.theBoneIndex < 0)
						{
							faceSection.theBoneIndex = 0;
							thePhyFileData.theSourcePhyIsCollisionModel = true;
						}
					}

					theInputFileReader.ReadInt32();

					//0c 00 00 00    count of lines after this (00 - 0b)
					triangleCount = theInputFileReader.ReadInt32();

					//00 b0 00 00 
					//	00 00 06 00   ' vertex index 00
					//	01 00 18 00   ' vertex index 01
					//	02 00 0e 00   ' vertex index 02
					//01 a0 00 00 
					//	00 00 05 00 
					//	03 00 1d 00   ' vertex index 03
					//	01 00 fa 7f 
					//02 70 00 00 
					//	04 00 06 00   ' vertex index 04
					//	03 00 fb 7f 
					//	00 00 0c 00 
					//03 60 00 00 
					//	04 00 22 00 
					//	05 00 17 00 
					//	03 00 fa 7f 
					//04 80 00 00 
					//	00 00 f2 7f 
					//	02 00 07 00 
					//	06 00 02 00 
					//05 90 00 00 
					//	00 00 fe 7f 
					//	06 00 13 00 
					//	04 00 f4 7f 
					//06 30 00 00 
					//	06 00 f9 7f 
					//	02 00 e8 7f 
					//	01 00 02 00 
					//07 20 00 00 
					//	06 00 fe 7f 
					//	01 00 04 00 
					//	07 00 0b 00 
					//08 50 00 00 
					//	03 00 06 00 
					//	07 00 fc 7f 
					//	01 00 e3 7f 
					//09 50 00 00 
					//	03 00 e9 7f 
					//	05 00 08 00 
					//	07 00 fa 7f 
					//0a 10 00 00 
					//	04 00 ed 7f 
					//	06 00 f5 7f 
					//	07 00 02 00 
					//0b 
					//	00 
					//	00 00 
					//	04 00 
					//		fe 7f 
					//	07 00 
					//		f8 7f 
					//	05 00 
					//		de 7f 
					for (int i = 0; i < triangleCount; i++)
					{
						SourcePhyFace phyTriangle = new SourcePhyFace();
						triangleIndex = theInputFileReader.ReadByte();
						theInputFileReader.ReadByte();
						theInputFileReader.ReadUInt16();

						for (int j = 0; j <= 2; j++)
						{
							phyTriangle.vertexIndex[j] = theInputFileReader.ReadUInt16();
							theInputFileReader.ReadUInt16();
							if (!vertices.Contains(phyTriangle.vertexIndex[j]))
							{
								vertices.Add(phyTriangle.vertexIndex[j]);
							}
						}
						faceSection.theFaces.Add(phyTriangle);
					}
					collisionData.theFaceSections.Add(faceSection);
				}

				if (thePhyFileData.theSourcePhyMaxConvexPieces < collisionData.theFaceSections.Count)
				{
					thePhyFileData.theSourcePhyMaxConvexPieces = collisionData.theFaceSections.Count;
				}

				theInputFileReader.BaseStream.Seek(vertexDataStreamPosition, SeekOrigin.Begin);

				// Vertex data section.
				//	' 8 distinct vertices
				//ae 69 29 3b 01 6f 4d bd f6 4a 9b 3c 78 f7 18 00 
				//ed dd fe 3d f0 37 1c 3d b8 60 16 3d 78 f7 18 00 
				//e2 98 a4 3a 08 34 1c 3d df 72 22 3d 78 f7 18 00 
				//6a cb 00 3e 21 6b 4d bd a7 26 83 3c 78 f7 18 00 
				//fb 9a c7 3a d7 29 1a bd 06 46 0d bd 78 f7 18 00 
				//ec 69 ff 3d f7 25 1a bd 30 58 19 bd 78 f7 18 00 
				//20 04 4b 39 34 79 4f 3d 82 e2 61 bc 78 f7 18 00 
				//07 b1 fc 3d 1c 7d 4f 3d 90 15 89 bc 78 f7 18 00 
	//			Dim w As Double
				List<SourcePhyVertex> faceSection0Vertices = collisionData.theFaceSections[0].theVertices;
				for (int i = 0; i < vertices.Count; i++)
				{
					SourcePhyVertex phyVertex = new SourcePhyVertex();

					phyVertex.vertex.x = theInputFileReader.ReadSingle();
					phyVertex.vertex.y = theInputFileReader.ReadSingle();
					phyVertex.vertex.z = theInputFileReader.ReadSingle();
					double w = theInputFileReader.ReadSingle();

					faceSection0Vertices.Add(phyVertex);
				}
				for (int faceSectionIndex = 1; faceSectionIndex < collisionData.theFaceSections.Count; faceSectionIndex++)
				{
					faceSection = collisionData.theFaceSections[faceSectionIndex];
					for (int i = 0; i < vertices.Count; i++)
					{
						SourcePhyVertex phyVertex = new SourcePhyVertex();

						phyVertex.vertex.x = faceSection0Vertices[i].vertex.x;
						phyVertex.vertex.y = faceSection0Vertices[i].vertex.y;
						phyVertex.vertex.z = faceSection0Vertices[i].vertex.z;

						faceSection.theVertices.Add(phyVertex);
					}
				}



				//TODO: Figure out this section.
				// Section after vertex section. Presumably, this section contains normal data, but still unsure how it is arranged.
				// It does not seem to be arranged by count of face sections.
				// First 32-bits seems to be size of list or pointer to next list.
				// Example data from two-cubes (concavity_test from Bang).
				// One list of Two structs.
				//Address: 0x0490
				//38 00 00 00   ' Size of list or pointer to next list of structs, which in this case is last section of CollisionData below.
				//70 fd ff ff 
				//00 00 80 31 
				//a9 13 d0 bd 7a ec 1d 3d 1c 12 89 3e 
				//60 bf 84 00 
				//------
				//00 00 00 00    ' Zero offset to indicate last struct?
				//b4 fb ff ff 
				//00 00 00 00 
				//00 00 00 00 00 00 00 00 26 33 34 3e 
				//91 91 91 00 



				//TODO: Figure out this section.
				// Last section of the CollisionData.
				// Example data from two-cubes (concavity_test from Bang).
				//Address: 0x04c8
				//00 00 00 00 
				//68 fc ff ff 
				//00 00 00 32 
				//a9 13 50 be 79 ec 9d 3d 26 33 34 3e 
				//91 91 91 00



				thePhyFileData.theSourcePhyCollisionDatas.Add(collisionData);

				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Solid(" + solidIndex.ToString() + ")");

				theInputFileReader.BaseStream.Seek(nextSolidDataStreamPosition, SeekOrigin.Begin);
			}
			thePhyFileData.theSourcePhyKeyValueDataOffset = theInputFileReader.BaseStream.Position;
		}

		public void ReadPhyData_VERSION37()
		{
			//FROM: HL2 leak - bullsquid.phy
			//66 c5 36 bc 
			//d8 ac 31 bd 
			//d9 14 dd bd 
			//19 bd 01 3d 
			//ae 4f 31 3d 
			//52 d0 4a 3d 
			//e1 01 0b 3f 
			//d8 1c 03 00 
			//00 03 00 00 
			//00 00 00 00 
			//00 00 00 00
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
		}

		public void ReadPhyData_VERSION48()
		{
			int tempInt = 0;

			//56 50 48 59   VPHY
			char[] vphyId = new char[4];
			vphyId = theInputFileReader.ReadChars(4);

			//00 01         version?
			//00 00         model type?
			tempInt = theInputFileReader.ReadUInt16();
			tempInt = theInputFileReader.ReadUInt16();

			//9c 01 00 00   surface size? might be size of remaining solid struct after "axisMapSize?" field
			//              Seems to be size of data struct from VPHY field to last section of CollisionData.
			theInputFileReader.ReadInt32();


			//00 00 30 3f   dragAxisAreas x?
			//00 00 80 3f   dragAxisAreas y?
			//00 00 80 3f   dragAxisAreas z?
			//00 00 00 00   axisMapSize?
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();

			//2c fe 80 3d 
			//a5 85 83 39 
			//27 ab 91 3a 
			//52 06 3c 3a 
			//28 a7 a9 3a 
			//c8 2a bb 3a 
			//5e 08 a8 3d 
			//ec 9c 01 00 
			//80 01 00 00   size of something? add this to address right after it = address right after the next VPHY
			//00 00 00 00 
			//00 00 00 00 
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
			theInputFileReader.ReadInt32();
		}

		public void CalculateVertexNormals()
		{
			SourcePhyCollisionData collisionData = null;
			SourcePhyFace aTriangle = null;
			SourcePhyFaceSection faceSection = null;

			if (thePhyFileData.theSourcePhyCollisionDatas != null)
			{
				for (int collisionDataIndex = 0; collisionDataIndex < thePhyFileData.theSourcePhyCollisionDatas.Count; collisionDataIndex++)
				{
					collisionData = thePhyFileData.theSourcePhyCollisionDatas[collisionDataIndex];

					for (int faceSectionIndex = 0; faceSectionIndex < collisionData.theFaceSections.Count; faceSectionIndex++)
					{
						faceSection = collisionData.theFaceSections[faceSectionIndex];

						for (int triangleIndex = 0; triangleIndex < faceSection.theFaces.Count; triangleIndex++)
						{
							aTriangle = faceSection.theFaces[triangleIndex];

							CalculateFaceNormal(faceSection, aTriangle);
						}
					}
				}
			}
		}

		public void ReadSourcePhysCollisionModels()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			try
			{
				string line = null;
				bool thereIsAValue = true;
				string key = "";
				string value = "";
				long tempStreamOffset = 0;
				SourcePhyPhysCollisionModel aSourcePhysCollisionModel = null;
				thePhyFileData.theSourcePhyPhysCollisionModels = new List<SourcePhyPhysCollisionModel>();
				theDampingToCountMap = new SortedList<float, int>();
				theInertiaToCountMap = new SortedList<float, int>();
				theRotDampingToCountMap = new SortedList<float, int>();
				do
				{
					aSourcePhysCollisionModel = new SourcePhyPhysCollisionModel();
					tempStreamOffset = theInputFileReader.BaseStream.Position;
					line = FileManager.ReadTextLine(theInputFileReader);
					if (line == null || line != "solid {")
					{
						theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin);
						break;
					}

					while (thereIsAValue)
					{
						thereIsAValue = FileManager.ReadKeyValueLine(theInputFileReader, ref key, ref value);
						if (thereIsAValue)
						{
							if (key == "index")
							{
								aSourcePhysCollisionModel.theIndex = int.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "name")
							{
								aSourcePhysCollisionModel.theName = value;
							}
							else if (key == "parent")
							{
								aSourcePhysCollisionModel.theParentIsValid = true;
								aSourcePhysCollisionModel.theParentName = value;
							}
							else if (key == "mass")
							{
								aSourcePhysCollisionModel.theMass = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "surfaceprop")
							{
								aSourcePhysCollisionModel.theSurfaceProp = value;
							}
							else if (key == "damping")
							{
								aSourcePhysCollisionModel.theDamping = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
								if (theDampingToCountMap.ContainsKey(aSourcePhysCollisionModel.theDamping))
								{
									theDampingToCountMap[aSourcePhysCollisionModel.theDamping] += 1;
								}
								else
								{
									theDampingToCountMap.Add(aSourcePhysCollisionModel.theDamping, 1);
								}
							}
							else if (key == "rotdamping")
							{
								aSourcePhysCollisionModel.theRotDamping = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
								if (theRotDampingToCountMap.ContainsKey(aSourcePhysCollisionModel.theRotDamping))
								{
									theRotDampingToCountMap[aSourcePhysCollisionModel.theRotDamping] += 1;
								}
								else
								{
									theRotDampingToCountMap.Add(aSourcePhysCollisionModel.theRotDamping, 1);
								}
							}
							else if (key == "drag")
							{
								aSourcePhysCollisionModel.theDragCoefficientIsValid = true;
								aSourcePhysCollisionModel.theDragCoefficient = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "rollingDrag")
							{
								aSourcePhysCollisionModel.theRollingDragCoefficientIsValid = true;
								aSourcePhysCollisionModel.theRollingDragCoefficient = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "inertia")
							{
								aSourcePhysCollisionModel.theInertia = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
								if (theInertiaToCountMap.ContainsKey(aSourcePhysCollisionModel.theInertia))
								{
									theInertiaToCountMap[aSourcePhysCollisionModel.theInertia] += 1;
								}
								else
								{
									theInertiaToCountMap.Add(aSourcePhysCollisionModel.theInertia, 1);
								}
							}
							else if (key == "volume")
							{
								aSourcePhysCollisionModel.theVolume = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "massbias")
							{
								aSourcePhysCollisionModel.theMassBiasIsValid = true;
								aSourcePhysCollisionModel.theMassBias = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
						}
					}

					//NOTE: Above while loop should return the ending brace.
					if (key == null || key != "}")
					{
						break;
					}
					thePhyFileData.theSourcePhyPhysCollisionModels.Add(aSourcePhysCollisionModel);
					thereIsAValue = true;
				} while (line != null);

				float maxValue = 0;
				int maxCount = 0;
				thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues = new SourcePhyPhysCollisionModel();
				for (int i = 0; i < theDampingToCountMap.Count; i++)
				{
					if (maxCount <= theDampingToCountMap.Values[i])
					{
						maxValue = theDampingToCountMap.Keys[i];
						maxCount = theDampingToCountMap.Values[i];
					}
				}
				thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theDamping = maxValue;
				maxCount = 0;
				for (int i = 0; i < theInertiaToCountMap.Count; i++)
				{
					if (maxCount <= theInertiaToCountMap.Values[i])
					{
						maxValue = theInertiaToCountMap.Keys[i];
						maxCount = theInertiaToCountMap.Values[i];
					}
				}
				thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theInertia = maxValue;
				maxCount = 0;
				for (int i = 0; i < theRotDampingToCountMap.Count; i++)
				{
					if (maxCount <= theRotDampingToCountMap.Values[i])
					{
						maxValue = theRotDampingToCountMap.Keys[i];
						maxCount = theRotDampingToCountMap.Values[i];
					}
				}
				thePhyFileData.theSourcePhyPhysCollisionModelMostUsedValues.theRotDamping = maxValue;
			}
			catch (Exception ex)
			{
				throw;
				//Finally
			}

			fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
			thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Properties");
		}

		public void ReadSourcePhyRagdollConstraintDescs()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			try
			{
				string line = null;
				bool thereIsAValue = true;
				string key = "";
				string value = "";
				long tempStreamOffset = 0;
				SourcePhyRagdollConstraint aSourceRagdollConstraintDesc = null;
				thePhyFileData.theSourcePhyRagdollConstraintDescs = new SortedList<int, SourcePhyRagdollConstraint>();
				do
				{
					aSourceRagdollConstraintDesc = new SourcePhyRagdollConstraint();
					tempStreamOffset = theInputFileReader.BaseStream.Position;
					line = FileManager.ReadTextLine(theInputFileReader);
					if (line == null || line != "ragdollconstraint {")
					{
						theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin);
						break;
					}

					while (thereIsAValue)
					{
						thereIsAValue = FileManager.ReadKeyValueLine(theInputFileReader, ref key, ref value);
						if (thereIsAValue)
						{
							if (key == "parent")
							{
								aSourceRagdollConstraintDesc.theParentIndex = int.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "child")
							{
								aSourceRagdollConstraintDesc.theChildIndex = int.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "xmin")
							{
								aSourceRagdollConstraintDesc.theXMin = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "xmax")
							{
								aSourceRagdollConstraintDesc.theXMax = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "xfriction")
							{
								aSourceRagdollConstraintDesc.theXFriction = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "ymin")
							{
								aSourceRagdollConstraintDesc.theYMin = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "ymax")
							{
								aSourceRagdollConstraintDesc.theYMax = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "yfriction")
							{
								aSourceRagdollConstraintDesc.theYFriction = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "zmin")
							{
								aSourceRagdollConstraintDesc.theZMin = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "zmax")
							{
								aSourceRagdollConstraintDesc.theZMax = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
							else if (key == "zfriction")
							{
								aSourceRagdollConstraintDesc.theZFriction = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
						}
					}

					//NOTE: Above while loop should return the ending brace.
					if (key == null || key != "}")
					{
						break;
					}
					thePhyFileData.theSourcePhyRagdollConstraintDescs.Add(aSourceRagdollConstraintDesc.theChildIndex, aSourceRagdollConstraintDesc);
					thereIsAValue = true;
				} while (line != null);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
			}

			if (fileOffsetStart < theInputFileReader.BaseStream.Position)
			{
				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Ragdoll constraints");
			}
		}

		public void ReadSourcePhyCollisionRules()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			try
			{
				string line = null;
				bool thereIsAValue = true;
				string key = "";
				string value = "";
				long tempStreamOffset = 0;
				SourcePhyCollisionPair aSourcePhyCollisionPair = null;
				thePhyFileData.theSourcePhyCollisionPairs = new List<SourcePhyCollisionPair>();
				do
				{
					tempStreamOffset = theInputFileReader.BaseStream.Position;
					line = FileManager.ReadTextLine(theInputFileReader);
					if (line == null || line != "collisionrules {")
					{
						theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin);
						break;
					}

					char[] delimiters = {','};
					string[] tokens = {""};
					while (thereIsAValue)
					{
						thereIsAValue = FileManager.ReadKeyValueLine(theInputFileReader, ref key, ref value);
						if (thereIsAValue)
						{
							if (key == "collisionpair")
							{
								tokens = value.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
								if (tokens.Length == 2)
								{
									aSourcePhyCollisionPair = new SourcePhyCollisionPair();
									aSourcePhyCollisionPair.obj0 = int.Parse(tokens[0], MainCROWBAR.TheApp.InternalNumberFormat);
									aSourcePhyCollisionPair.obj1 = int.Parse(tokens[1], MainCROWBAR.TheApp.InternalNumberFormat);
									thePhyFileData.theSourcePhyCollisionPairs.Add(aSourcePhyCollisionPair);
								}
							}
							else if (key == "selfcollisions")
							{
								thePhyFileData.theSourcePhySelfCollides = false;
							}
						}
					}

					//NOTE: Above while loop should return the ending brace.
					if (key == null || key != "}")
					{
						break;
					}
					thereIsAValue = true;
				} while (line != null);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
			}

			if (fileOffsetStart < theInputFileReader.BaseStream.Position)
			{
				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Collision rules");
			}
		}

		public void ReadSourcePhyEditParamsSection()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			try
			{
				string line = null;
				bool thereIsAValue = true;
				string key = "";
				string value = "";
				long tempStreamOffset = 0;
				thePhyFileData.theSourcePhyEditParamsSection = new SourcePhyEditParamsSection();
				do
				{
					tempStreamOffset = theInputFileReader.BaseStream.Position;
					line = FileManager.ReadTextLine(theInputFileReader);
					if (line == null || line != "editparams {")
					{
						theInputFileReader.BaseStream.Seek(tempStreamOffset, SeekOrigin.Begin);
						break;
					}

					char[] delimiters = {','};
					string[] tokens = {""};
					while (thereIsAValue)
					{
						thereIsAValue = FileManager.ReadKeyValueLine(theInputFileReader, ref key, ref value);
						if (key == "rootname")
						{
							thereIsAValue = true;
							if (key != value)
							{
								thePhyFileData.theSourcePhyEditParamsSection.rootName = value;
								//Else
								//	Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyEditParamsSection.rootName = ""
							}
						}
						else if (thereIsAValue)
						{
							if (key == "concave")
							{
								thePhyFileData.theSourcePhyEditParamsSection.concave = value;
							}
							else if (key == "totalmass")
							{
								thePhyFileData.theSourcePhyEditParamsSection.totalMass = float.Parse(value, MainCROWBAR.TheApp.InternalNumberFormat);
							}
						}
					}

					//NOTE: Above while loop should return the ending brace.
					if (key == null || key != "}")
					{
						break;
					}
					thereIsAValue = true;
				} while (line != null);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}

			if (fileOffsetStart < theInputFileReader.BaseStream.Position)
			{
				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Edit params");
			}
		}

		public void ReadCollisionTextSection()
		{
			long fileOffsetStart = 0;
			long fileOffsetEnd = 0;

			fileOffsetStart = theInputFileReader.BaseStream.Position;

			try
			{
				long endOffset = 0;
				if (thePhyEndOffset == 0)
				{
					endOffset = theInputFileReader.BaseStream.Length - 1;
				}
				else
				{
					endOffset = thePhyEndOffset;
				}

				thePhyFileData.theSourcePhyCollisionText = Common.ReadPhyCollisionTextSection(theInputFileReader, endOffset);
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
			}

			if (fileOffsetStart < theInputFileReader.BaseStream.Position)
			{
				fileOffsetEnd = theInputFileReader.BaseStream.Position - 1;
				thePhyFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "Collision text");
			}
		}

		public void ReadUnreadBytes()
		{
			thePhyFileData.theFileSeekLog.LogUnreadBytes(theInputFileReader);
		}

#endregion

#region Private Methods

		private void CalculateFaceNormal(SourcePhyFaceSection faceSection, SourcePhyFace aTriangle)
		{
			SourceVector[] vertex = new SourceVector[4];
			SourceVector vector0 = new SourceVector();
			SourceVector vector1 = new SourceVector();
			SourceVector normalVector = new SourceVector();

			for (int vertexIndex = 0; vertexIndex <= 2; vertexIndex++)
			{
				vertex[vertexIndex] = faceSection.theVertices[aTriangle.vertexIndex[vertexIndex]].vertex;
			}

			vector0.x = vertex[0].x - vertex[1].x;
			vector0.y = vertex[0].y - vertex[1].y;
			vector0.z = vertex[0].z - vertex[1].z;

			vector1.x = vertex[1].x - vertex[2].x;
			vector1.y = vertex[1].y - vertex[2].y;
			vector1.z = vertex[1].z - vertex[2].z;

			normalVector = vector0.CrossProduct(vector1);
			//NOTE: Do not need to normalize here. It will be normalized once after all of the normals are added together.
			//normalVector = normalVector.Normalize()

			SourcePhyVertex phyVertex = null;
			for (int vertexIndex = 0; vertexIndex <= 2; vertexIndex++)
			{
				//NOTE: Instead of storing all of the normals, just store one and keep adding to it. 
				//      Can then just do the normalize once when normal is first accessed.
				phyVertex = faceSection.theVertices[aTriangle.vertexIndex[vertexIndex]];
				//phyVertex.Normal.x += normalVector.x
				//phyVertex.Normal.y += normalVector.y
				//phyVertex.Normal.z += normalVector.z
				phyVertex.UnnormalizedNormal.x += normalVector.x;
				phyVertex.UnnormalizedNormal.y += normalVector.y;
				phyVertex.UnnormalizedNormal.z += normalVector.z;
			}
		}

#endregion

#region Data

		private BinaryReader theInputFileReader;
		private SourcePhyFileData thePhyFileData;
		private long thePhyEndOffset;

		private SortedList<float, int> theDampingToCountMap;
		private SortedList<float, int> theInertiaToCountMap;
		private SortedList<float, int> theRotDampingToCountMap;

#endregion

	}

}