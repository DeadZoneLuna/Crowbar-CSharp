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
	internal static class SourceFileNamesModule
	{

		public static string CreateBodyGroupSmdFileName(string givenBodyGroupSmdFileName, int bodyPartIndex, int modelIndex, int lodIndex, string modelName, string bodyModelName)
		{
			//Dim bodyModelNameTrimmed As String
			//Dim bodyModelFileName As String = ""
			//Dim bodyModelFileNameWithoutExtension As String
			//Dim bodyGroupSmdFileName As String = ""

			//If (bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0) _
			// AndAlso (theModelCommandIsUsed OrElse (bodyPartCount = 1 AndAlso bodyModelCount = 1)) _
			// Then
			//	bodyGroupSmdFileName = modelName
			//	bodyGroupSmdFileName += "_reference"
			//	bodyGroupSmdFileName += ".smd"
			//Else
			//	If FileManager.FilePathHasInvalidChars(bodyModelName.Trim(Chr(0))) Then
			//		bodyModelFileName = "body"
			//		bodyModelFileName += CStr(bodyPartIndex)
			//		bodyModelFileName += "_model"
			//		bodyModelFileName += CStr(modelIndex)
			//	Else
			//		bodyModelFileName = Path.GetFileName(bodyModelName.Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
			//	End If
			//	bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName)

			//	If Not bodyModelFileName.StartsWith(modelName) Then
			//		bodyGroupSmdFileName += modelName + "_"
			//	End If
			//	bodyGroupSmdFileName += bodyModelFileNameWithoutExtension
			//	If lodIndex > 0 Then
			//		bodyGroupSmdFileName += "_lod"
			//		bodyGroupSmdFileName += lodIndex.ToString()
			//	End If
			//	bodyGroupSmdFileName += ".smd"
			//End If
			//======
			//bodyModelNameTrimmed = bodyModelName.Trim(Chr(0))
			//Try
			//	bodyModelFileName = Path.GetFileName(bodyModelNameTrimmed).ToLower(TheApp.InternalCultureInfo)
			//	If FileManager.FilePathHasInvalidChars(bodyModelFileName) Then
			//		bodyModelFileName = "body"
			//		bodyModelFileName += CStr(bodyPartIndex)
			//		bodyModelFileName += "_model"
			//		bodyModelFileName += CStr(modelIndex)
			//	End If
			//Catch ex As Exception
			//	bodyModelFileName = "body"
			//	bodyModelFileName += CStr(bodyPartIndex)
			//	bodyModelFileName += "_model"
			//	bodyModelFileName += CStr(modelIndex)
			//End Try
			//bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName)
			//
			//If Not bodyModelFileName.StartsWith(modelName) Then
			//	bodyGroupSmdFileName += modelName + "_"
			//End If
			//bodyGroupSmdFileName += bodyModelFileNameWithoutExtension
			//If lodIndex > 0 Then
			//	bodyGroupSmdFileName += "_lod"
			//	bodyGroupSmdFileName += lodIndex.ToString()
			//End If
			//bodyGroupSmdFileName += ".smd"
			//======
			//'NOTE: Ignore bodyModelName altogether because already making up the first part of file names 
			//'      so might as well make the rest of the file names unique with an easy pattern.

			//Dim bodyGroupSmdFileName As String

			//If bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0 AndAlso Not String.IsNullOrEmpty(sequenceGroupFileName) AndAlso Not FileManager.FilePathHasInvalidChars(sequenceGroupFileName) Then
			//	bodyGroupSmdFileName = Path.GetFileName(sequenceGroupFileName.Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
			//	If Not bodyGroupSmdFileName.StartsWith(modelName) Then
			//		bodyGroupSmdFileName = modelName + "_" + bodyGroupSmdFileName
			//	End If
			//Else
			//	bodyGroupSmdFileName = modelName
			//	bodyGroupSmdFileName += "_"
			//	If bodyPartCount = 1 AndAlso bodyModelCount = 1 AndAlso lodIndex = 0 Then
			//		bodyGroupSmdFileName += "reference"
			//	Else
			//		bodyGroupSmdFileName += "body"
			//		bodyGroupSmdFileName += CStr(bodyPartIndex)
			//		bodyGroupSmdFileName += "_model"
			//		bodyGroupSmdFileName += CStr(modelIndex)
			//	End If
			//	If lodIndex > 0 Then
			//		bodyGroupSmdFileName += "_lod"
			//		bodyGroupSmdFileName += lodIndex.ToString()
			//	End If
			//	If includeExtension Then
			//		bodyGroupSmdFileName += ".smd"
			//	End If
			//End If
			//======
			// Use bodyModel name, but make sure the file name is unique for this model.
			string bodyGroupSmdFileName = "";
			string bodyModelFileName = "";
			string bodyModelFileNameWithoutExtension = "";

			if (!string.IsNullOrEmpty(givenBodyGroupSmdFileName))
			{
				bodyGroupSmdFileName = givenBodyGroupSmdFileName;
			}
			else
			{
				try
				{
					bodyModelFileName = Path.GetFileName(bodyModelName.Trim('\0'));
					if (FileManager.FilePathHasInvalidChars(bodyModelFileName))
					{
						bodyModelFileName = "body";
						bodyModelFileName += bodyPartIndex.ToString();
						bodyModelFileName += "_model";
						bodyModelFileName += modelIndex.ToString();
					}
				}
				catch (Exception ex)
				{
					bodyModelFileName = "body";
					bodyModelFileName += bodyPartIndex.ToString();
					bodyModelFileName += "_model";
					bodyModelFileName += modelIndex.ToString();
				}
				bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName);

				if (MainCROWBAR.TheApp.Settings.DecompilePrefixFileNamesWithModelNameIsChecked && !(bodyModelFileName.ToLower(MainCROWBAR.TheApp.InternalCultureInfo).StartsWith(modelName.ToLower(MainCROWBAR.TheApp.InternalCultureInfo))))
				{
					bodyGroupSmdFileName += modelName + "_";
				}
				bodyGroupSmdFileName += bodyModelFileNameWithoutExtension;
				if (lodIndex > 0)
				{
					bodyGroupSmdFileName += "_lod";
					bodyGroupSmdFileName += lodIndex.ToString();
				}

				bodyGroupSmdFileName = SourceFileNamesModule.GetUniqueSmdFileName(bodyGroupSmdFileName);

				bodyGroupSmdFileName += ".smd";
			}

			return bodyGroupSmdFileName;
		}

		public static string GetAnimationSmdRelativePath(string modelName)
		{
			string path = "";

			if (MainCROWBAR.TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked)
			{
				path = modelName + "_" + App.AnimsSubFolderName;
			}

			return path;
		}

		public static string CreateAnimationSmdRelativePathFileName(string givenAnimationSmdRelativePathFileName, string modelName, string iAnimationName, int blendIndex = -2)
		{
			string animationName = null;
			string animationSmdRelativePathFileName = null;

			if (!string.IsNullOrEmpty(givenAnimationSmdRelativePathFileName))
			{
				animationSmdRelativePathFileName = givenAnimationSmdRelativePathFileName;
			}
			else
			{
				// Clean the iAnimationName.
				try
				{
					iAnimationName = iAnimationName.Trim('\0');
					//iAnimationName = iAnimationName.Replace(":", "")
					//iAnimationName = iAnimationName.Replace("\", "")
					//iAnimationName = iAnimationName.Replace("/", "")
					foreach (char invalidChar in Path.GetInvalidFileNameChars())
					{
						iAnimationName = iAnimationName.Replace(invalidChar.ToString(), "");
					}
					if (FileManager.FilePathHasInvalidChars(iAnimationName))
					{
						iAnimationName = "anim";
					}
				}
				catch (Exception ex)
				{
					iAnimationName = "anim";
				}

				// Set the name
				if (blendIndex >= 0)
				{
					// For MDL v6 and v10.
					animationName = iAnimationName + "_blend" + (blendIndex + 1).ToString("00");
				}
				else if (blendIndex == -1)
				{
					// For MDL v6 and v10.
					animationName = iAnimationName;
				}
				else
				{
					if (string.IsNullOrEmpty(iAnimationName))
					{
						animationName = "";
					}
					else if (iAnimationName[0] == '@')
					{
						//NOTE: The file name for the animation data file is not stored in mdl file (which makes sense), 
						//      so make the file name the same as the animation name.
						animationName = iAnimationName.Substring(1);
					}
					else
					{
						animationName = iAnimationName;
					}
				}

				// If anims are not stored in anims folder, add some more to the name.
				if (!MainCROWBAR.TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked)
				{
					animationName = modelName + "_anim_" + iAnimationName;
				}

				// Set the path.
				animationSmdRelativePathFileName = Path.Combine(GetAnimationSmdRelativePath(modelName), animationName);

				animationSmdRelativePathFileName = SourceFileNamesModule.GetUniqueSmdFileName(animationSmdRelativePathFileName);

				// Set the extension.
				if (Path.GetExtension(animationSmdRelativePathFileName) != ".smd")
				{
					//animationSmdRelativePathFileName = Path.ChangeExtension(animationSmdRelativePathFileName, ".smd")
					//NOTE: Add the ".smd" extension, keeping the existing extension in file name, which is often ".dmx" for newer models. 
					//      Thus, user can see that model might have newer features that Crowbar does not yet handle.
					animationSmdRelativePathFileName += ".smd";
				}
			}

			return animationSmdRelativePathFileName;
		}

		public static string CreateCorrectiveAnimationName(string givenAnimationSmdRelativePathFileName)
		{
			string animationName = givenAnimationSmdRelativePathFileName + "_" + "corrective_animation";


			return animationName;
		}

		public static string CreateCorrectiveAnimationSmdRelativePathFileName(string givenAnimationSmdRelativePathFileName, string modelName)
		{
			string animationSmdRelativePathFileName = CreateCorrectiveAnimationName(givenAnimationSmdRelativePathFileName) + ".smd";

			animationSmdRelativePathFileName = Path.Combine(GetAnimationSmdRelativePath(modelName), animationSmdRelativePathFileName);

			return animationSmdRelativePathFileName;
		}

		public static string GetVrdFileName(string modelName)
		{
			string vrdFileName = modelName;

			vrdFileName += ".vrd";

			return vrdFileName;
		}

		public static string GetVtaFileName(string modelName, int bodyPartIndex)
		{
			string vtaFileName = modelName;

			vtaFileName += "_";
			vtaFileName += (bodyPartIndex + 1).ToString("00");
			vtaFileName += ".vta";

			return vtaFileName;
		}

		public static string CreatePhysicsSmdFileName(string givenPhysicsSmdFileName, string modelName)
		{
			string physicsSmdFileName = null;

			if (!string.IsNullOrEmpty(givenPhysicsSmdFileName))
			{
				physicsSmdFileName = givenPhysicsSmdFileName;
			}
			else
			{
				physicsSmdFileName = modelName;
				physicsSmdFileName += "_physics";

				physicsSmdFileName = SourceFileNamesModule.GetUniqueSmdFileName(physicsSmdFileName);

				physicsSmdFileName += ".smd";
			}

			return physicsSmdFileName;
		}

		public static string GetDeclareSequenceQciFileName(string modelName)
		{
			string declareSequenceQciFileName = modelName;

			declareSequenceQciFileName += "_DeclareSequence.qci";

			return declareSequenceQciFileName;
		}

		//'TODO: Call *after* both ReadTextures() and ReadTexturePaths() are called.
		//Public Sub CopyPathsFromTextureFileNamesToTexturePaths(ByVal texturePaths As List(Of String), ByVal texturePathFileNames As List(Of String))
		//	' Make all lowercase list copy of texturePaths.
		//	Dim texturePathsLowercase As List(Of String)
		//	texturePathsLowercase = New List(Of String)(texturePaths.Count)
		//	For Each aTexturePath As String In texturePaths
		//		texturePathsLowercase.Add(aTexturePath.ToLower())
		//	Next

		//	For texturePathFileNameIndex As Integer = 0 To texturePathFileNames.Count - 1
		//		Dim aTexturePathFileName As String
		//		Dim aTexturePathFileNameLowercase As String
		//		aTexturePathFileName = texturePathFileNames(texturePathFileNameIndex)
		//		aTexturePathFileNameLowercase = aTexturePathFileName.ToLower()

		//		' If the texturePathFileName starts with a path that is in the texturePaths list, then remove the texturePath from the texturePathFileName.
		//		For texturePathIndex As Integer = 0 To texturePathsLowercase.Count - 1
		//			Dim aTexturePathLowercase As String
		//			aTexturePathLowercase = texturePathsLowercase(texturePathIndex)

		//			If aTexturePathLowercase <> "" AndAlso aTexturePathFileNameLowercase.StartsWith(aTexturePathLowercase) Then
		//				Dim startOffsetAfterPathSeparator As Integer
		//				If aTexturePathLowercase.EndsWith(Path.DirectorySeparatorChar) OrElse aTexturePathLowercase.EndsWith(Path.AltDirectorySeparatorChar) Then
		//					startOffsetAfterPathSeparator = aTexturePathLowercase.Length
		//				Else
		//					startOffsetAfterPathSeparator = aTexturePathLowercase.Length + 1
		//				End If
		//				texturePathFileNames(texturePathFileNameIndex) = aTexturePathFileName.Substring(startOffsetAfterPathSeparator)
		//				Exit For
		//			End If
		//		Next

		//		Dim texturePath As String
		//		Dim texturePathLowercase As String
		//		Dim textureFileName As String
		//		texturePath = FileManager.GetPath(aTexturePathFileName)
		//		texturePathLowercase = texturePath.ToLower()
		//		textureFileName = Path.GetFileName(aTexturePathFileName)
		//		If aTexturePathFileName <> textureFileName AndAlso Not texturePathsLowercase.Contains(texturePathLowercase) AndAlso Not texturePathsLowercase.Contains(texturePathLowercase + Path.DirectorySeparatorChar) AndAlso Not texturePathsLowercase.Contains(texturePathLowercase + Path.AltDirectorySeparatorChar) Then
		//			'NOTE: Place first because it should override whatever is already in list.
		//			texturePaths.Insert(0, texturePath)
		//		End If
		//	Next
		//End Sub

		//NOTE: Call *after* both ReadTextures() and ReadTexturePaths() are called.
		public static void MovePathsFromTextureFileNamesToTexturePaths(ref List<string> texturePaths, ref List<string> texturePathFileNames)
		{
			// Make all lowercase list copy of texturePaths.
			List<string> texturePathsLowercase = new List<string>(texturePaths.Count);
			foreach (string aTexturePath in texturePaths)
			{
				texturePathsLowercase.Add(aTexturePath.ToLower());
			}

			for (int fileNameIndex = 0; fileNameIndex < texturePathFileNames.Count; fileNameIndex++)
			{
				string aTexturePathFileName = texturePathFileNames[fileNameIndex];
				aTexturePathFileName = FileManager.GetCleanPathFileName(aTexturePathFileName, false);

	//			Dim aTexturePath As String
	//			Dim aTextureFileName As String
				var aTexturePath = FileManager.GetPath(aTexturePathFileName);
				var aTextureFileName = Path.GetFileName(aTexturePathFileName);

	//			Dim aTexturePathFileNameLowercase As String
	//			Dim aTexturePathLowercase As String
				var aTexturePathFileNameLowercase = aTexturePathFileName.ToLower();
				var aTexturePathLowercase = FileManager.GetPath(aTexturePathFileNameLowercase);

				// If the texturePathFileName starts with a path, then ...
				if (!string.IsNullOrEmpty(aTexturePathLowercase))
				{
					// ... insert the path into texturePaths, if it is not already there.
					if (!texturePathsLowercase.Contains(aTexturePathLowercase) && !texturePathsLowercase.Contains(aTexturePathLowercase + Path.DirectorySeparatorChar) && !texturePathsLowercase.Contains(aTexturePathLowercase + Path.AltDirectorySeparatorChar))
					{
						//NOTE: Place first because it should override whatever is already in list.
						texturePaths.Insert(0, aTexturePath);
						texturePathsLowercase.Insert(0, aTexturePathLowercase);
					}

					// ... and remove it from the texturePathFileName in texturePathFileNames.
					texturePathFileNames[fileNameIndex] = aTextureFileName;
				}
			}
		}

		private static string GetUniqueSmdFileName(string givenSmdFileName)
		{
			string smdFileName = givenSmdFileName;

			//NOTE: Starting this at 1 means the first file name will not have a number and the second name will have a 2.
			int nameNumber = 1;
			while (MainCROWBAR.TheApp.SmdFileNames.Contains(smdFileName.ToLower(MainCROWBAR.TheApp.InternalCultureInfo)))
			{
				nameNumber += 1;
				smdFileName = givenSmdFileName + "_" + nameNumber.ToString();
			}

			MainCROWBAR.TheApp.SmdFileNames.Add(smdFileName.ToLower(MainCROWBAR.TheApp.InternalCultureInfo));
			return smdFileName;
		}

	}

}