//INSTANT C# NOTE: Formerly VB project-level imports:
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
	public class SourceQcFile
	{

#region Methods

		public string GetQcModelName(string qcPathFileName)
		{
			string qcModelName = "";


			using (StreamReader inputFileStream = new StreamReader(qcPathFileName))
			{
				string inputLine = null;
				string modifiedLine = null;

//INSTANT C# NOTE: There is no C# equivalent to VB's implicit 'once only' variable initialization within loops, so the following variable declaration has been placed prior to the loop:
				int pos = 0;
				while (!(inputFileStream.EndOfStream))
				{
					inputLine = inputFileStream.ReadLine();

					modifiedLine = inputLine.ToLower().TrimStart();
					if (modifiedLine.StartsWith("\"$modelname\""))
					{
						modifiedLine = modifiedLine.Replace("\"$modelname\"", "$modelname");
					}
					if (modifiedLine.StartsWith("$modelname"))
					{
						modifiedLine = modifiedLine.Replace("$modelname", "");
						modifiedLine = modifiedLine.Trim();

						// Need to remove any comment after the file name token (which may or may not be double-quoted).
	//					Dim pos As Integer
						if (modifiedLine.StartsWith("\""))
						{
							pos = modifiedLine.IndexOf("\"", 1);
							if (pos >= 0)
							{
								modifiedLine = modifiedLine.Substring(1, pos - 1);
							}
						}
						else
						{
							pos = modifiedLine.IndexOf(" ");
							if (pos >= 0)
							{
								modifiedLine = modifiedLine.Substring(0, pos);
							}
						}

						//temp = temp.Trim(Chr(34))
						qcModelName = modifiedLine.Replace("/", "\\");
						break;
					}
				}
			}

			return qcModelName;
		}

		public string InsertAnIncludeFileCommand(string qcPathFileName, string qciPathFileName)
		{
			string line = "";

			using (StreamWriter outputFileStream = File.AppendText(qcPathFileName))
			{
				outputFileStream.WriteLine();

				if (MainCROWBAR.TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked)
				{
					line += "$Include";
				}
				else
				{
					line += "$include";
				}
				line += " ";
				line += "\"";
				line += FileManager.GetRelativePathFileName(FileManager.GetPath(qcPathFileName), qciPathFileName);
				line += "\"";
				outputFileStream.WriteLine(line);
			}

			return line;
		}

#endregion

#region Private Delegates

		//Private Delegate Sub WriteGroupDelegate()

#endregion

#region Private Methods

		//Private Sub WriteHeaderComment()
		//	Dim line As String = ""

		//	line = "// "
		//	line += TheApp.GetHeaderComment()
		//	Me.theOutputFileStream.WriteLine(line)
		//End Sub

		//Private Sub WriteModelNameCommand()
		//	Dim line As String = ""
		//	'Dim modelPath As String
		//	Dim modelPathFileName As String

		//	'modelPath = FileManager.GetPath(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
		//	'modelPathFileName = Path.Combine(modelPath, theSourceEngineModel.ModelName + ".mdl")
		//	'modelPathFileName = CStr(theSourceEngineModel.MdlFileHeader.name).Trim(Chr(0))
		//	modelPathFileName = theSourceEngineModel.MdlFileHeader.theName

		//	Me.theOutputFileStream.WriteLine()

		//	'$modelname "survivors/survivor_producer.mdl"
		//	'$modelname "custom/survivor_producer.mdl"
		//If TheApp.Settings.DecompileQcUseMixedCaseForKeywordsIsChecked Then
		//	line = "$ModelName "
		//Else
		//	line = "$modelname "
		//End If
		//	line += """"
		//	line += modelPathFileName
		//	line += """"
		//	Me.theOutputFileStream.WriteLine(line)
		//End Sub

		//Private Sub WriteGroup(ByVal qciGroupName As String, ByVal writeGroupAction As WriteGroupDelegate, ByVal includeLineIsCommented As Boolean, ByVal includeLineIsIndented As Boolean)
		//	If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
		//		Dim qciFileName As String
		//		Dim qciPathFileName As String
		//		Dim mainOutputFileStream As StreamWriter

		//		mainOutputFileStream = Me.theOutputFileStream

		//		Try
		//			'qciPathFileName = Path.Combine(Me.theOutputPathName, Me.theOutputFileNameWithoutExtension + "_flexes.qci")
		//			qciFileName = Me.theOutputFileNameWithoutExtension + "_" + qciGroupName + ".qci"
		//			qciPathFileName = Path.Combine(Me.theOutputPathName, qciFileName)

		//			Me.theOutputFileStream = File.CreateText(qciPathFileName)

		//			'Me.WriteFlexLines()
		//			'Me.WriteFlexControllerLines()
		//			'Me.WriteFlexRuleLines()
		//			writeGroupAction.Invoke()
		//		Catch ex As Exception
		//			Throw
		//		Finally
		//			If Me.theOutputFileStream IsNot Nothing Then
		//				Me.theOutputFileStream.Flush()
		//				Me.theOutputFileStream.Close()

		//				Me.theOutputFileStream = mainOutputFileStream
		//			End If
		//		End Try

		//		Try
		//			If File.Exists(qciPathFileName) Then
		//				Dim qciFileInfo As New FileInfo(qciPathFileName)
		//				If qciFileInfo.Length > 0 Then
		//					Dim line As String = ""

		//					Me.theOutputFileStream.WriteLine()

		//					If includeLineIsCommented Then
		//						line += "// "
		//					End If
		//					If includeLineIsIndented Then
		//						line += vbTab
		//					End If
		//					line += "$Include"
		//					line += " "
		//					line += """"
		//					line += qciFileName
		//					line += """"
		//					Me.theOutputFileStream.WriteLine(line)
		//				End If
		//			End If
		//		Catch ex As Exception
		//			Throw
		//		End Try
		//	Else
		//		'Me.WriteFlexLines()
		//		'Me.WriteFlexControllerLines()
		//		'Me.WriteFlexRuleLines()
		//		writeGroupAction.Invoke()
		//	End If
		//End Sub

		protected List<List<short>> GetSkinFamiliesOfChangedMaterials(List<List<short>> iSkinFamilies)
		{
			List<List<short>> skinFamilies = null;
			int skinReferenceCount = 0;
			List<short> firstSkinFamily = null;
			List<short> aSkinFamily = null;
			List<short> textureFileNameIndexes = null;

			skinReferenceCount = iSkinFamilies[0].Count;
			skinFamilies = new List<List<short>>(iSkinFamilies.Count);

			try
			{
				for (int skinFamilyIndex = 0; skinFamilyIndex < iSkinFamilies.Count; skinFamilyIndex++)
				{
					textureFileNameIndexes = new List<short>(skinReferenceCount);
					skinFamilies.Add(textureFileNameIndexes);
				}

				firstSkinFamily = iSkinFamilies[0];
				for (int j = 0; j < skinReferenceCount; j++)
				{
					//NOTE: Start at second skin family because comparing first with all others.
					for (int i = 1; i < iSkinFamilies.Count; i++)
					{
						aSkinFamily = iSkinFamilies[i];

						if (firstSkinFamily[j] != aSkinFamily[j])
						{
							for (int skinFamilyIndex = 0; skinFamilyIndex < iSkinFamilies.Count; skinFamilyIndex++)
							{
								aSkinFamily = iSkinFamilies[skinFamilyIndex];

								textureFileNameIndexes = skinFamilies[skinFamilyIndex];
								textureFileNameIndexes.Add(aSkinFamily[j]);
							}

							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			return skinFamilies;
		}

		protected List<string> GetTextureGroupSkinFamilyLines(List<List<string>> skinFamilies)
		{
			List<string> lines = new List<string>();
			List<string> aSkinFamily = null;
			string aTextureFileName = null;
			string line = "";

			if (MainCROWBAR.TheApp.Settings.DecompileQcSkinFamilyOnSingleLineIsChecked)
			{
				List<int> textureFileNameMaxLengths = new List<int>();
				int length = 0;

				aSkinFamily = skinFamilies[0];
				for (int textureFileNameIndex = 0; textureFileNameIndex < aSkinFamily.Count; textureFileNameIndex++)
				{
					aTextureFileName = aSkinFamily[textureFileNameIndex];
					length = aTextureFileName.Length;

					textureFileNameMaxLengths.Add(length);
				}

				for (int skinFamilyIndex = 1; skinFamilyIndex < skinFamilies.Count; skinFamilyIndex++)
				{
					aSkinFamily = skinFamilies[skinFamilyIndex];

					for (int textureFileNameIndex = 0; textureFileNameIndex < aSkinFamily.Count; textureFileNameIndex++)
					{
						aTextureFileName = aSkinFamily[textureFileNameIndex];
						length = aTextureFileName.Length;

						if (length > textureFileNameMaxLengths[textureFileNameIndex])
						{
							textureFileNameMaxLengths[textureFileNameIndex] = length;
						}
					}
				}

				for (int skinFamilyIndex = 0; skinFamilyIndex < skinFamilies.Count; skinFamilyIndex++)
				{
					aSkinFamily = skinFamilies[skinFamilyIndex];

					line = "\t";
					line += "{";
					line += " ";

					for (int textureFileNameIndex = 0; textureFileNameIndex < aSkinFamily.Count; textureFileNameIndex++)
					{
						aTextureFileName = aSkinFamily[textureFileNameIndex];
						length = textureFileNameMaxLengths[textureFileNameIndex];

						//NOTE: Need at least "+ 2" to account for the double-quotes.
						line += ConversionHelper.LSet("\"" + aTextureFileName + "\"", length + 3);
					}

					//line += " "
					line += "}";
					lines.Add(line);
				}
			}
			else
			{
				for (int skinFamilyIndex = 0; skinFamilyIndex < skinFamilies.Count; skinFamilyIndex++)
				{
					aSkinFamily = skinFamilies[skinFamilyIndex];

					line = "\t";
					line += "{";
					lines.Add(line);

					for (int textureFileNameIndex = 0; textureFileNameIndex < aSkinFamily.Count; textureFileNameIndex++)
					{
						aTextureFileName = aSkinFamily[textureFileNameIndex];

						line = "\t";
						line += "\t";
						line += "\"";
						line += aTextureFileName;
						line += "\"";

						lines.Add(line);
					}

					line = "\t";
					line += "}";
					lines.Add(line);
				}
			}

			return lines;
		}

#endregion

#region Data

		//Private theSourceEngineModel As SourceModel_Old
		//Private theOutputFileStream As StreamWriter
		//Private theOutputPathName As String
		//Private theOutputFileNameWithoutExtension As String

#endregion

	}

}