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
	internal static class Common
	{

		public static string ReadPhyCollisionTextSection(BinaryReader theInputFileReader, long endOffset)
		{
			string result = "";
			long streamLastPosition = 0;

			try
			{
				//streamLastPosition = theInputFileReader.BaseStream.Length() - 1
				streamLastPosition = endOffset;

				if (streamLastPosition > theInputFileReader.BaseStream.Position)
				{
					//NOTE: Use -1 to avoid including the null terminator character.
					result = new string(theInputFileReader.ReadChars((int)(streamLastPosition - theInputFileReader.BaseStream.Position - 1)));
					// Read the NULL byte to help with debug logging.
					theInputFileReader.ReadChar();
					// Only grab text to the first NULL byte. (Needed for PHY data stored within Titanfall 2 MDL file.)
					result = result.Substring(0, result.IndexOf('\0'));
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			return result;
		}

		public static string GetFlexRule(List<SourceMdlFlexDesc> flexDescs, List<SourceMdlFlexController> flexControllers, SourceMdlFlexRule flexRule)
		{
			string flexRuleEquation = "\t";
			flexRuleEquation += "%";
			flexRuleEquation += flexDescs[flexRule.flexIndex].theName;
			flexRuleEquation += " = ";
			if (flexRule.theFlexOps != null && flexRule.theFlexOps.Count > 0)
			{
				SourceMdlFlexOp aFlexOp = null;
				bool dmxFlexOpWasUsed = false;

				// Convert to infix notation.

				Stack<IntermediateExpression> stack = new Stack<IntermediateExpression>();
				string rightExpr = null;
				string leftExpr = null;

				dmxFlexOpWasUsed = false;
				for (int i = 0; i < flexRule.theFlexOps.Count; i++)
				{
					aFlexOp = flexRule.theFlexOps[i];
					if (aFlexOp.op == SourceMdlFlexOp.STUDIO_CONST)
					{
						stack.Push(new IntermediateExpression(Math.Round(aFlexOp.value, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat), 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_FETCH1)
					{
						//int m = pFlexcontroller( (LocalFlexController_t)pops->d.index)->localToGlobal;
						//stack[k] = src[m];
						//k++; 
						stack.Push(new IntermediateExpression(flexControllers[aFlexOp.index].theName, 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_FETCH2)
					{
						stack.Push(new IntermediateExpression("%" + flexDescs[aFlexOp.index].theName, 10));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_ADD)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();

						string newExpr = Convert.ToString(leftIntermediate.theExpression) + " + " + Convert.ToString(rightIntermediate.theExpression);
						stack.Push(new IntermediateExpression(newExpr, 1));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_SUB)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();

						string newExpr = Convert.ToString(leftIntermediate.theExpression) + " - " + Convert.ToString(rightIntermediate.theExpression);
						stack.Push(new IntermediateExpression(newExpr, 1));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_MUL)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (rightIntermediate.thePrecedence < 2)
						{
							rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")";
						}
						else
						{
							rightExpr = rightIntermediate.theExpression;
						}

						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (leftIntermediate.thePrecedence < 2)
						{
							leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")";
						}
						else
						{
							leftExpr = leftIntermediate.theExpression;
						}

						string newExpr = leftExpr + " * " + rightExpr;
						stack.Push(new IntermediateExpression(newExpr, 2));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_DIV)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (rightIntermediate.thePrecedence < 2)
						{
							rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")";
						}
						else
						{
							rightExpr = rightIntermediate.theExpression;
						}

						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (leftIntermediate.thePrecedence < 2)
						{
							leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")";
						}
						else
						{
							leftExpr = leftIntermediate.theExpression;
						}

						string newExpr = leftExpr + " / " + rightExpr;
						stack.Push(new IntermediateExpression(newExpr, 2));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_NEG)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();

						string newExpr = "-" + rightIntermediate.theExpression;
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
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (rightIntermediate.thePrecedence < 5)
						{
							rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")";
						}
						else
						{
							rightExpr = rightIntermediate.theExpression;
						}

						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (leftIntermediate.thePrecedence < 5)
						{
							leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")";
						}
						else
						{
							leftExpr = leftIntermediate.theExpression;
						}

						string newExpr = "max(" + leftExpr + ", " + rightExpr + ")";
						stack.Push(new IntermediateExpression(newExpr, 5));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_MIN)
					{
						IntermediateExpression rightIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (rightIntermediate.thePrecedence < 5)
						{
							rightExpr = "(" + Convert.ToString(rightIntermediate.theExpression) + ")";
						}
						else
						{
							rightExpr = rightIntermediate.theExpression;
						}

						IntermediateExpression leftIntermediate = (Crowbar.IntermediateExpression)stack.Pop();
						if (leftIntermediate.thePrecedence < 5)
						{
							leftExpr = "(" + Convert.ToString(leftIntermediate.theExpression) + ")";
						}
						else
						{
							leftExpr = leftIntermediate.theExpression;
						}

						string newExpr = "min(" + leftExpr + ", " + rightExpr + ")";
						stack.Push(new IntermediateExpression(newExpr, 5));
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_2WAY_0)
					{
						//TODO: SourceMdlFlexOp.STUDIO_2WAY_0
						//	'#define STUDIO_2WAY_0	15	// Fetch a value from a 2 Way slider for the 1st value RemapVal( 0.0, 0.5, 0.0, 1.0 )
						//	'int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
						//	'stack[ k ] = RemapValClamped( src[m], -1.0f, 0.0f, 1.0f, 0.0f );
						//	'k++; 

						//	= C + (D - C) * (min(max((val - A) / (B - A), 0.0f), 1.0f))
						//	"1 - (min(max(" + flexControllers(aFlexOp.index).theName + " + 1, 0), 1))"
						stack.Push(new IntermediateExpression("(1 - (min(max(" + flexControllers[aFlexOp.index].theName + " + 1, 0), 1)))", 5));
						dmxFlexOpWasUsed = true;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_2WAY_1)
					{
						//TODO:			   SourceMdlFlexOp.STUDIO_2WAY_1()
						//#define STUDIO_2WAY_1	16	// Fetch a value from a 2 Way slider for the 2nd value RemapVal( 0.5, 1.0, 0.0, 1.0 )
						//int m = pFlexcontroller( (LocalFlexController_t)pops->d.index )->localToGlobal;
						//stack[ k ] = RemapValClamped( src[m], 0.0f, 1.0f, 0.0f, 1.0f );
						//k++; 

						//	= C + (D - C) * (min(max((val - A) / (B - A), 0.0f), 1.0f))
						//	"(min(max(" + flexControllers(aFlexOp.index).theName + ", 0), 1))"
						stack.Push(new IntermediateExpression("(min(max(" + flexControllers[aFlexOp.index].theName + ", 0), 1))", 5));
						dmxFlexOpWasUsed = true;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_NWAY)
					{
						//TODO:			   SourceMdlFlexOp.STUDIO_NWAY()
						SourceMdlFlexController v = flexControllers[aFlexOp.index];

	//					Dim valueControllerIndex As IntermediateExpression
	//					Dim flValue As String
						var valueControllerIndex = (Crowbar.IntermediateExpression)stack.Pop();
						var flValue = flexControllers[int.Parse(valueControllerIndex.theExpression)].theName;

	//					Dim filterRampW As IntermediateExpression
	//					Dim filterRampZ As IntermediateExpression
	//					Dim filterRampY As IntermediateExpression
	//					Dim filterRampX As IntermediateExpression
						var filterRampW = (Crowbar.IntermediateExpression)stack.Pop();
						var filterRampZ = (Crowbar.IntermediateExpression)stack.Pop();
						var filterRampY = (Crowbar.IntermediateExpression)stack.Pop();
						var filterRampX = (Crowbar.IntermediateExpression)stack.Pop();

	//					Dim greaterThanX As String
	//					Dim lessThanY As String
	//					Dim remapX As String
	//					Dim greaterThanEqualY As String
	//					Dim lessThanEqualZ As String
	//					Dim greaterThanZ As String
	//					Dim lessThanW As String
	//					Dim remapZ As String
						var greaterThanX = "min(1, (-min(0, (" + filterRampX.theExpression + " - " + flValue + "))))";
						var lessThanY = "min(1, (-min(0, (" + flValue + " - " + filterRampY.theExpression + "))))";
						var remapX = "min(max((" + flValue + " - " + filterRampX.theExpression + ") / (" + filterRampY.theExpression + " - " + filterRampX.theExpression + "), 0), 1)";
						var greaterThanEqualY = "-(min(1, (-min(0, (" + flValue + " - " + filterRampY.theExpression + ")))) - 1)";
						var lessThanEqualZ = "-(min(1, (-min(0, (" + filterRampZ.theExpression + " - " + flValue + ")))) - 1)";
						var greaterThanZ = "min(1, (-min(0, (" + filterRampZ.theExpression + " - " + flValue + "))))";
						var lessThanW = "min(1, (-min(0, (" + flValue + " - " + filterRampW.theExpression + "))))";
						var remapZ = "(1 - (min(max((" + flValue + " - " + filterRampZ.theExpression + ") / (" + filterRampW.theExpression + " - " + filterRampZ.theExpression + "), 0), 1)))";

						flValue = "((" + greaterThanX + " * " + lessThanY + ") * " + remapX + ") + (" + greaterThanEqualY + " * " + lessThanEqualZ + ") + ((" + greaterThanZ + " * " + lessThanW + ") * " + remapZ + ")";

						stack.Push(new IntermediateExpression("((" + flValue + ") * (" + v.theName + "))", 5));
						dmxFlexOpWasUsed = true;
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
	//					Dim count As Integer
	//					Dim newExpression As String
	//					Dim intermediateExp As IntermediateExpression
						var count = aFlexOp.index;
						var newExpression = "";
						var intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
						newExpression += intermediateExp.theExpression;
						for (int j = 2; j <= count; j++)
						{
							intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
							newExpression += " * " + intermediateExp.theExpression;
						}
						newExpression = "(" + newExpression + ")";
						stack.Push(new IntermediateExpression(newExpression, 5));
						dmxFlexOpWasUsed = true;
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
	//					Dim count As Integer
	//					Dim newExpression As String
	//					Dim intermediateExp As IntermediateExpression
						var count = aFlexOp.index;
						var newExpression = "";
						var intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
						newExpression += intermediateExp.theExpression;
						for (int j = 2; j <= count; j++)
						{
							intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
							newExpression += " * " + intermediateExp.theExpression;
						}
						intermediateExp = (Crowbar.IntermediateExpression)stack.Pop();
						newExpression = intermediateExp.theExpression + " * (1 - " + newExpression + ")";
						newExpression = "(" + newExpression + ")";
						stack.Push(new IntermediateExpression(newExpression, 5));
						dmxFlexOpWasUsed = true;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_DME_LOWER_EYELID)
					{
						SourceMdlFlexController pCloseLidV = flexControllers[aFlexOp.index];
	//					Dim flCloseLidV As String
						string flCloseLidVMin = Math.Round(pCloseLidV.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidVMax = Math.Round(pCloseLidV.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						var flCloseLidV = "(min(max((" + pCloseLidV.theName + " - " + flCloseLidVMin + ") / (" + flCloseLidVMax + " - " + flCloseLidVMin + "), 0), 1))";

						IntermediateExpression closeLidIndex = (Crowbar.IntermediateExpression)stack.Pop();
						SourceMdlFlexController pCloseLid = flexControllers[int.Parse(closeLidIndex.theExpression)];
	//					Dim flCloseLid As String
						string flCloseLidMin = Math.Round(pCloseLid.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidMax = Math.Round(pCloseLid.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						var flCloseLid = "(min(max((" + pCloseLid.theName + " - " + flCloseLidMin + ") / (" + flCloseLidMax + " - " + flCloseLidMin + "), 0), 1))";

						// Unused, but need to pop it off the stack.
						IntermediateExpression blinkIndex = (Crowbar.IntermediateExpression)stack.Pop();

						IntermediateExpression eyeUpDownIndex = (Crowbar.IntermediateExpression)stack.Pop();
						SourceMdlFlexController pEyeUpDown = flexControllers[int.Parse(eyeUpDownIndex.theExpression)];
	//					Dim flEyeUpDown As String
						string flEyeUpDownMin = Math.Round(pEyeUpDown.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flEyeUpDownMax = Math.Round(pEyeUpDown.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						var flEyeUpDown = "(-1 + 2 * (min(max((" + pEyeUpDown.theName + " - " + flEyeUpDownMin + ") / (" + flEyeUpDownMax + " - " + flEyeUpDownMin + "), 0), 1)))";

						stack.Push(new IntermediateExpression("(min(1, (1 - " + flEyeUpDown + ")) * (1 - " + flCloseLidV + ") * " + flCloseLid + ")", 5));
						dmxFlexOpWasUsed = true;
					}
					else if (aFlexOp.op == SourceMdlFlexOp.STUDIO_DME_UPPER_EYELID)
					{
						SourceMdlFlexController pCloseLidV = flexControllers[aFlexOp.index];
	//					Dim flCloseLidV As String
						string flCloseLidVMin = Math.Round(pCloseLidV.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidVMax = Math.Round(pCloseLidV.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						var flCloseLidV = "(min(max((" + pCloseLidV.theName + " - " + flCloseLidVMin + ") / (" + flCloseLidVMax + " - " + flCloseLidVMin + "), 0), 1))";

						IntermediateExpression closeLidIndex = (Crowbar.IntermediateExpression)stack.Pop();
						SourceMdlFlexController pCloseLid = flexControllers[int.Parse(closeLidIndex.theExpression)];
	//					Dim flCloseLid As String
						string flCloseLidMin = Math.Round(pCloseLid.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flCloseLidMax = Math.Round(pCloseLid.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						var flCloseLid = "(min(max((" + pCloseLid.theName + " - " + flCloseLidMin + ") / (" + flCloseLidMax + " - " + flCloseLidMin + "), 0), 1))";

						// Unused, but need to pop it off the stack.
						IntermediateExpression blinkIndex = (Crowbar.IntermediateExpression)stack.Pop();

						IntermediateExpression eyeUpDownIndex = (Crowbar.IntermediateExpression)stack.Pop();
						SourceMdlFlexController pEyeUpDown = flexControllers[int.Parse(eyeUpDownIndex.theExpression)];
	//					Dim flEyeUpDown As String
						string flEyeUpDownMin = Math.Round(pEyeUpDown.min, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						string flEyeUpDownMax = Math.Round(pEyeUpDown.max, 6).ToString("0.######", MainCROWBAR.TheApp.InternalNumberFormat);
						var flEyeUpDown = "(-1 + 2 * (min(max((" + pEyeUpDown.theName + " - " + flEyeUpDownMin + ") / (" + flEyeUpDownMax + " - " + flEyeUpDownMin + "), 0), 1)))";

						stack.Push(new IntermediateExpression("(min(1, (1 + " + flEyeUpDown + ")) * " + flCloseLidV + " * " + flCloseLid + ")", 5));
						dmxFlexOpWasUsed = true;
					}
					else
					{
						stack.Clear();
						break;
					}
				}

				// The loop above leaves the final expression on the top of the stack.
				if (dmxFlexOpWasUsed)
				{
					flexRuleEquation += stack.Peek().theExpression + " // WARNING: Expression is an approximation of what can only be done via DMX file.";
				}
				else if (stack.Count == 1)
				{
					flexRuleEquation += stack.Peek().theExpression;
				}
				else if (stack.Count == 0 || stack.Count > 1)
				{
					flexRuleEquation = "// " + flexRuleEquation + stack.Peek().theExpression + " // ERROR: Unknown flex operation.";
				}
				else
				{
					flexRuleEquation = "// [Empty flex rule found and ignored.]";
				}
			}
			return flexRuleEquation;
		}

		public static void ProcessTexturePaths(List<string> theTexturePaths, List<SourceMdlTexture> theTextures, List<string> theModifiedTexturePaths, List<string> theModifiedTextureFileNames)
		{
			if (theTexturePaths != null)
			{
				foreach (string aTexturePath in theTexturePaths)
				{
					theModifiedTexturePaths.Add(aTexturePath);
				}
			}
			if (theTextures != null)
			{
				foreach (SourceMdlTexture aTexture in theTextures)
				{
					theModifiedTextureFileNames.Add(aTexture.thePathFileName);
				}
			}

			if (MainCROWBAR.TheApp.Settings.DecompileRemovePathFromSmdMaterialFileNamesIsChecked)
			{
				//SourceFileNamesModule.CopyPathsFromTextureFileNamesToTexturePaths(theModifiedTexturePaths, theModifiedTextureFileNames)
				SourceFileNamesModule.MovePathsFromTextureFileNamesToTexturePaths(ref theModifiedTexturePaths, ref theModifiedTextureFileNames);
			}
		}

		public static void WriteHeaderComment(StreamWriter outputFileStreamWriter)
		{
			if (!MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
			{
				string line = "";

				line = "// ";
				line += MainCROWBAR.TheApp.GetHeaderComment();
				outputFileStreamWriter.WriteLine(line);
			}
		}

	}

}