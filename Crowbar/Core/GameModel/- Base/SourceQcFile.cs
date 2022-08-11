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

					string text;
					keyValuesText = keyValuesText.TrimStart();
					if (keyValuesText.StartsWith(startText))
						text = keyValuesText.Remove(0, startText.Length);
					else if (keyValuesText.StartsWith(startText2))
						text = keyValuesText.Remove(0, startText2.Length);
					else
						text = keyValuesText;
					text = text.TrimStart();

					bool startBracesHas = text[0] == '{';
					if (!startBracesHas) theOutputFileStreamWriter.WriteLine(lineLevel + "{");
					WriteTextLines(text, startBracesHas ? indentLevel : indentLevel + 1);
					if (!startBracesHas) theOutputFileStreamWriter.WriteLine(lineLevel + "}");
				}
			}
			catch (Exception)
			{

			}
		}
		#endregion

		#region Private Methods
		internal virtual void WriteTextLines(string text, int indentCount)
		{

		}

		internal void WriteFlexControllerLines(List<SourceMdlFlexController> theFlexControllers, bool theEyeballOptionIsUsed)
		{
			if (theFlexControllers != null && theFlexControllers.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine(string.Empty);
				for (int i = 0; i < theFlexControllers.Count; i++)
				{
					SourceMdlFlexController aFlexController = theFlexControllers[i];
					if (!theEyeballOptionIsUsed && aFlexController.theType == "eyes" && (aFlexController.theName == "eyes_updown" || aFlexController.theName == "eyes_rightleft"))
						continue;

					theOutputFileStreamWriter.WriteLine(string.Format("\tflexcontroller {0} range {1} {2} \"{3}\"", 
						aFlexController.theType, 
						aFlexController.min.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat), 
						aFlexController.max.ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat), 
						aFlexController.theName));
				}
			}
		}

		internal void WriteFlexRuleLines(List<SourceMdlFlexRule> theFlexRules, List<SourceMdlFlexDesc> theFlexDescs, List<SourceMdlFlexController> theFlexControllers, bool isBeta = false)
		{
			//NOTE: All flex rules are correct for teenangst zoey.
			if (theFlexRules != null && theFlexRules.Count > 0)
			{
				theOutputFileStreamWriter.WriteLine(string.Empty);
				for (int i = 0; i < theFlexDescs.Count; i++)
				{
					SourceMdlFlexDesc flexDesc = theFlexDescs[i];
					if (!flexDesc.theDescIsUsedByFlex && flexDesc.theDescIsUsedByFlexRule)
						theOutputFileStreamWriter.WriteLine("\tlocalvar " + flexDesc.theName);
				}

				for (int i = 0; i < theFlexRules.Count; i++)
				{
					SourceMdlFlexRule aFlexRule = theFlexRules[i];
					theOutputFileStreamWriter.WriteLine(GetFlexRule(theFlexDescs, theFlexControllers, aFlexRule, isBeta));
				}
			}
		}

		internal string GetFlexRule(List<SourceMdlFlexDesc> flexDescs, List<SourceMdlFlexController> flexControllers, SourceMdlFlexRule flexRule, bool isBeta = false)
		{
			string flexRuleEquation = $"\t%{flexDescs[flexRule.flexIndex].theName} = ";
			if (flexRule.theFlexOps != null && flexRule.theFlexOps.Count > 0)
			{
				// Convert to infix notation.
				int count;
				string leftExpr;
				string rightExpr;
				string newExpression = string.Empty;
				IntermediateExpression intermediateExp;
				Stack<IntermediateExpression> stack = new Stack<IntermediateExpression>();
				bool dmxFlexOpWasUsed = false;

				for (int i = 0; i < flexRule.theFlexOps.Count; i++)
				{
					SourceMdlFlexOp aFlexOp = flexRule.theFlexOps[i];
					if (aFlexOp.op == SourceMdlFlexOp.STUDIO_CONST)
						stack.Push(new IntermediateExpression(Math.Round(aFlexOp.value, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat), 10));
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_FETCH1)
					{
						//int m = pFlexcontroller( (LocalFlexController_t)pops->d.index)->localToGlobal;
						//stack[k] = src[m];
						//k++; 
						stack.Push(new IntermediateExpression(flexControllers[aFlexOp.index].theName, 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_FETCH2)
						stack.Push(new IntermediateExpression($"%{flexDescs[aFlexOp.index].theName}", 10));
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_ADD)
					{
						IntermediateExpression rightIntermediate = stack.Pop();
						IntermediateExpression leftIntermediate = stack.Pop();

						string newExpr = $"{leftIntermediate.theExpression} + {rightIntermediate.theExpression}";
						stack.Push(new IntermediateExpression(newExpr, 1));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_SUB)
					{
						IntermediateExpression rightIntermediate = stack.Pop();
						IntermediateExpression leftIntermediate = stack.Pop();

						string newExpr = $"{leftIntermediate.theExpression} - {rightIntermediate.theExpression}";
						stack.Push(new IntermediateExpression(newExpr, 1));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_MUL)
					{
						IntermediateExpression rightIntermediate = stack.Pop();
						if (rightIntermediate.thePrecedence < 2)
							rightExpr = $"({rightIntermediate.theExpression})";
						else
							rightExpr = rightIntermediate.theExpression;

						IntermediateExpression leftIntermediate = stack.Pop();
						if (leftIntermediate.thePrecedence < 2)
							leftExpr = $"({leftIntermediate.theExpression})";
						else
							leftExpr = leftIntermediate.theExpression;

						string newExpr = $"{leftExpr} * {rightExpr}";
						stack.Push(new IntermediateExpression(newExpr, 2));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_DIV)
					{
						IntermediateExpression rightIntermediate = stack.Pop();
						if (rightIntermediate.thePrecedence < 2)
							rightExpr = $"({rightIntermediate.theExpression})";
						else
							rightExpr = rightIntermediate.theExpression;

						IntermediateExpression leftIntermediate = stack.Pop();
						if (leftIntermediate.thePrecedence < 2)
							leftExpr = $"({leftIntermediate.theExpression})";
						else
							leftExpr = leftIntermediate.theExpression;

						string newExpr = $"{leftExpr} / {rightExpr}";
						stack.Push(new IntermediateExpression(newExpr, 2));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_NEG)
					{
						IntermediateExpression rightIntermediate = stack.Pop();

						string newExpr = $"-{rightIntermediate.theExpression}";
						stack.Push(new IntermediateExpression(newExpr, 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_EXP)
					{
						int ignoreThisOpBecauseItIsMistakeToBeHere = 4242;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_OPEN)
					{
						int ignoreThisOpBecauseItIsMistakeToBeHere = 4242;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_CLOSE)
					{
						int ignoreThisOpBecauseItIsMistakeToBeHere = 4242;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_COMMA)
					{
						int ignoreThisOpBecauseItIsMistakeToBeHere = 4242;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_MAX)
					{
						IntermediateExpression rightIntermediate = stack.Pop();
						if (rightIntermediate.thePrecedence < 5)
							rightExpr = $"({rightIntermediate.theExpression})";
						else
							rightExpr = rightIntermediate.theExpression;

						IntermediateExpression leftIntermediate = stack.Pop();
						if (leftIntermediate.thePrecedence < 5)
							leftExpr = $"({leftIntermediate.theExpression})";
						else
							leftExpr = leftIntermediate.theExpression;

						string newExpr = (isBeta ? " " : string.Empty) + $"max({leftExpr}, {rightExpr})";
						stack.Push(new IntermediateExpression(newExpr, 5));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_MIN)
					{
						IntermediateExpression rightIntermediate = stack.Pop();
						if (rightIntermediate.thePrecedence < 5)
							rightExpr = $"({rightIntermediate.theExpression})";
						else
							rightExpr = rightIntermediate.theExpression;

						IntermediateExpression leftIntermediate = stack.Pop();
						if (leftIntermediate.thePrecedence < 5)
							leftExpr = $"({leftIntermediate.theExpression})";
						else
							leftExpr = leftIntermediate.theExpression;

						string newExpr = (isBeta ? " " : string.Empty) + $"min({leftExpr}, {rightExpr})";
						stack.Push(new IntermediateExpression(newExpr, 5));
					}
					else if (!isBeta && aFlexOp.op == SourceMdlFlexOp.STUDIO_2WAY_0)
					{
						//TODO: SourceMdlFlexOp.STUDIO_2WAY_0
						//	'#define STUDIO_2WAY_0	15	// Fetch a value from a 2 Way slider for the 1st value RemapVal( 0.0, 0.5, 0.0, 1.0 )
						//	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
						//	'stack[ k ] = RemapValClamped( src[m], -1.0f, 0.0f, 1.0f, 0.0f );
						//	'k++; 

						//	= C + (D - C) * (min(max((val - A) / (B - A), 0.0f), 1.0f))
						//	"1 - (min(max(" + flexControllers(aFlexOp.index).theName + " + 1, 0), 1))"
						stack.Push(new IntermediateExpression($"(1 - (min(max({flexControllers[aFlexOp.index].theName} + 1, 0), 1)))", 5));
						dmxFlexOpWasUsed = !isBeta;
					}
					else if (!isBeta && aFlexOp.op == SourceMdlFlexOp.STUDIO_2WAY_1)
					{
						//TODO:			   SourceMdlFlexOp.STUDIO_2WAY_1()
						//#define STUDIO_2WAY_1	16	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
						//int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
						//stack[ k ] = RemapValClamped( src[m], 0.0f, 1.0f, 0.0f, 1.0f );
						//k++; 

						//	= C + (D - C) * (min(max((val - A) / (B - A), 0.0f), 1.0f))
						//	"(min(max(" + flexControllers(aFlexOp.index).theName + ", 0), 1))"
						stack.Push(new IntermediateExpression($"(min(max({flexControllers[aFlexOp.index].theName}, 0), 1))", 5));
						dmxFlexOpWasUsed = !isBeta;
					}
					else if (!isBeta && aFlexOp.op == SourceMdlFlexOp.STUDIO_NWAY)
					{
						//TODO:			   SourceMdlFlexOp.STUDIO_NWAY()
						SourceMdlFlexController v = flexControllers[aFlexOp.index];
						IntermediateExpression valueControllerIndex = stack.Pop();
						string flValue = flexControllers[int.Parse(valueControllerIndex.theExpression)].theName;

						IntermediateExpression filterRampW = stack.Pop();
						IntermediateExpression filterRampZ = stack.Pop();
						IntermediateExpression filterRampY = stack.Pop();
						IntermediateExpression filterRampX = stack.Pop();

						string greaterThanX = $"min(1, (-min(0, ({filterRampX.theExpression} - {flValue}))))";
						string lessThanY = $"min(1, (-min(0, ({flValue} - {filterRampY.theExpression}))))";
						string remapX = $"min(max(({flValue} - {filterRampX.theExpression}) / ({filterRampY.theExpression} - {filterRampX.theExpression}), 0), 1)";
						string greaterThanEqualY = $"-(min(1, (-min(0, ({flValue} - {filterRampY.theExpression})))) - 1)";
						string lessThanEqualZ = $"-(min(1, (-min(0, ({filterRampZ.theExpression} - {flValue})))) - 1)";
						string greaterThanZ = $"min(1, (-min(0, ({filterRampZ.theExpression} - {flValue}))))";
						string lessThanW = $"min(1, (-min(0, ({flValue} - {filterRampW.theExpression}))))";
						string remapZ = $"(1 - (min(max(({flValue} - {filterRampZ.theExpression}) / ({filterRampW.theExpression} - {filterRampZ.theExpression}), 0), 1)))";

						flValue = $"(({greaterThanX} * {lessThanY}) * {remapX}) + ({greaterThanEqualY} * {lessThanEqualZ}) + (({greaterThanZ} * {lessThanW}) * {remapZ})";

						stack.Push(new IntermediateExpression($"(({flValue}) * ({v.theName}))", 5));
						dmxFlexOpWasUsed = !isBeta;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_COMBO)
					{
						//#define STUDIO_COMBO	18	// Perform a combo operation (essentially multiply the last N values on the stack)
						//int m = pops->d.index;
						//int km = k - m;
						//for ( int i = km + 1; i < k; ++i )
						//{
						//	stack[ km ] *= stack[ i ];
						//}
						//k = k - m + 1;
						count = aFlexOp.index;
						intermediateExp = stack.Pop();
						newExpression += intermediateExp.theExpression;
						for (int j = 2; j <= count; j++)
						{
							intermediateExp = stack.Pop();
							newExpression += $" * {intermediateExp.theExpression}";
						}
						newExpression = $"({newExpression})";
						stack.Push(new IntermediateExpression(newExpression, 5));
						dmxFlexOpWasUsed = !isBeta;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_DOMINATE)
					{
						//int m = pops->d.index;
						//int km = k - m;
						//float dv = stack[ km ];
						//for ( int i = km + 1; i < k; ++i )
						//{
						//	dv *= stack[ i ];
						//}
						//stack[ km - 1 ] *= 1.0f - dv;
						//k -= m;
						count = aFlexOp.index;
						intermediateExp = stack.Pop();
						newExpression += intermediateExp.theExpression;
						for (int j = 2; j <= count; j++)
						{
							intermediateExp = stack.Pop();
							newExpression += $" * {intermediateExp.theExpression}";
						}
						intermediateExp = stack.Pop();
						newExpression = $"({intermediateExp.theExpression} * (1 - {newExpression}))";
						stack.Push(new IntermediateExpression(newExpression, 5));
						dmxFlexOpWasUsed = !isBeta;
					}
					else if (!isBeta && aFlexOp.op == SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID)
					{
						SourceMdlFlexController pCloseLidV = flexControllers[aFlexOp.index];
						string flCloseLidVMin = Math.Round(pCloseLidV.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidVMax = Math.Round(pCloseLidV.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidV = $"(min(max(({pCloseLidV.theName} - {flCloseLidVMin}) / ({flCloseLidVMax} - {flCloseLidVMin}), 0), 1))";

						IntermediateExpression closeLidIndex = stack.Pop();
						SourceMdlFlexController pCloseLid = flexControllers[int.Parse(closeLidIndex.theExpression)];
						string flCloseLidMin = Math.Round(pCloseLid.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidMax = Math.Round(pCloseLid.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLid = $"(min(max(({pCloseLid.theName} - {flCloseLidMin}) / ({flCloseLidMax} - {flCloseLidMin}), 0), 1))";

						// Unused, but need to pop it off the stack.
						IntermediateExpression blinkIndex = stack.Pop();

						IntermediateExpression eyeUpDownIndex = stack.Pop();
						SourceMdlFlexController pEyeUpDown = flexControllers[int.Parse(eyeUpDownIndex.theExpression)];
						string flEyeUpDownMin = Math.Round(pEyeUpDown.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flEyeUpDownMax = Math.Round(pEyeUpDown.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flEyeUpDown = $"(-1 + 2 * (min(max(({pEyeUpDown.theName} - {flEyeUpDownMin}) / ({flEyeUpDownMax} - {flEyeUpDownMin}), 0), 1)))";

						stack.Push(new IntermediateExpression($"(min(1, (1 - {flEyeUpDown})) * (1 - {flCloseLidV}) * {flCloseLid})", 5));
						dmxFlexOpWasUsed = !isBeta;
					}
					else if (!isBeta && aFlexOp.op == SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID)
					{
						SourceMdlFlexController pCloseLidV = flexControllers[aFlexOp.index];
						string flCloseLidVMin = Math.Round(pCloseLidV.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidVMax = Math.Round(pCloseLidV.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidV = $"(min(max(({pCloseLidV.theName} - {flCloseLidVMin}) / ({flCloseLidVMax} - {flCloseLidVMin}), 0), 1))";

						IntermediateExpression closeLidIndex = stack.Pop();
						SourceMdlFlexController pCloseLid = flexControllers[int.Parse(closeLidIndex.theExpression)];
						string flCloseLidMin = Math.Round(pCloseLid.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidMax = Math.Round(pCloseLid.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLid = $"(min(max(({pCloseLid.theName} - {flCloseLidMin}) / ({flCloseLidMax} - {flCloseLidMin}), 0), 1))";

						// Unused, but need to pop it off the stack.
						IntermediateExpression blinkIndex = stack.Pop();

						IntermediateExpression eyeUpDownIndex = stack.Pop();
						SourceMdlFlexController pEyeUpDown = flexControllers[int.Parse(eyeUpDownIndex.theExpression)];
						string flEyeUpDownMin = Math.Round(pEyeUpDown.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flEyeUpDownMax = Math.Round(pEyeUpDown.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flEyeUpDown = $"(-1 + 2 * (min(max(({pEyeUpDown.theName} - {flEyeUpDownMin}) / ({flEyeUpDownMax} - {flEyeUpDownMin}), 0), 1)))";

						stack.Push(new IntermediateExpression($"(min(1, (1 + {flEyeUpDown})) * {flCloseLidV} * {flCloseLid})", 5));
						dmxFlexOpWasUsed = !isBeta;
					}
					else
					{
						stack.Clear();
						break;
					}
				}

				// The loop above leaves the final expression on the top of the stack.
				if (dmxFlexOpWasUsed)
					flexRuleEquation += $"{stack.Peek().theExpression} // WARNING: Expression is an approximation of what can only be done via DMX file.";
				else if (stack.Count == 1)
					flexRuleEquation += stack.Peek().theExpression;
				else if (stack.Count == 0 || stack.Count > 1)
					flexRuleEquation = $"// {flexRuleEquation + stack.Peek().theExpression} // ERROR: Unknown flex operation.";
				else
					flexRuleEquation = "// [Empty flex rule found and ignored.]";
			}
			return flexRuleEquation;
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