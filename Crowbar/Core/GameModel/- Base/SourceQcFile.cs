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

		public void WriteKeyValues(string keyValuesText, string commandOrOptionText, int indentLevel = 0)
		{
			string lineLevel = new string('\t', indentLevel);
			string startText = "mdlkeyvalue" + "\n";
			string startText2 = "\"mdlkeyvalue\"";
			string text = null;

			//$keyvalues
			//{
			//	"particles"
			//	{
			//		"effect"
			//		{
			//		name("sparks_head")
			//		attachment_type("follow_attachment")
			//		attachment_point("Head_sparks")
			//		}
			//		"effect"
			//		{
			//		name("sparks_head_wire1")
			//		attachment_type("follow_attachment")
			//		attachment_point("Head_Wire_1")
			//		}
			//		"effect"
			//		{
			//		name("sparks_knee_wire1")
			//		attachment_type("follow_attachment")
			//		attachment_point("R_Knee_Wire_1")
			//		}
			//		"effect"
			//		{
			//		name("sparks_knee_wire2")
			//		attachment_type("follow_attachment")
			//		attachment_point("R_Knee_Wire_2")
			//		}
			//		"effect"
			//		{
			//		name("sparks_ankle_wire1")
			//		attachment_type("follow_attachment")
			//		attachment_point("L_Ankle_Wire_1")
			//		}
			//		"effect"
			//		{
			//		name("sparks_ankle_wire2")
			//		attachment_type("follow_attachment")
			//		attachment_point("L_Ankle_Wire_2")
			//		}			
			//	}
			//}
			try
			{
				if (keyValuesText != null && keyValuesText.Length > 0)
				{
					theOutputFileStreamWriter.WriteLine(lineLevel);
					theOutputFileStreamWriter.WriteLine(lineLevel + commandOrOptionText);

					keyValuesText = keyValuesText.TrimStart();
					if (keyValuesText.StartsWith(startText))
					{
						text = keyValuesText.Remove(0, startText.Length);
					}
					else if (keyValuesText.StartsWith(startText2))
					{
						text = keyValuesText.Remove(0, startText2.Length);
					}
					else
					{
						text = keyValuesText;
					}
					text = text.TrimStart();

					theOutputFileStreamWriter.WriteLine(lineLevel + "{");
					WriteTextLines(text, indentLevel + 1);
					theOutputFileStreamWriter.WriteLine(lineLevel + "}");
				}
			}
			catch (Exception ex)
			{

			}
		}
		#endregion

		#region Private Methods
		internal virtual void WriteTextLines(string text, int indentCount)
		{

		}

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
		internal StreamWriter theOutputFileStreamWriter;
		#endregion
	}

}