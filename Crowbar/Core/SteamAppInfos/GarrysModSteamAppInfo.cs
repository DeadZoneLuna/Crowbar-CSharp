using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.ComponentModel;
using System.IO;
using System.Web.Script.Serialization;
using Steamworks;

namespace Crowbar
{
	public class GarrysModSteamAppInfo : SteamAppInfoBase
	{
		public GarrysModSteamAppInfo() : base()
		{

			ID = new AppId_t(4000);
			Name = "Garry's Mod";
			UsesSteamUGC = true;
			CanUseContentFolderOrFile = true;
			ContentFileExtensionsAndDescriptions.Add("gma", "Garry's Mod GMA Files");
			TagsControlType = typeof(GarrysModTagsUserControl);
		}

		public override string ProcessFileAfterDownload(string givenPathFileName, BackgroundWorkerEx bw)
		{
			string processedPathFileName = "";

			if (Directory.Exists(givenPathFileName))
			{
				// If the folder contains only one GMA file, then move+rename GMA file from folder and delete the folder.
				string[] sourcePathFileNames = Directory.GetFiles(givenPathFileName, "*.gma");
				if (sourcePathFileNames.Length == 1)
				{
					string sourcePathFileName = sourcePathFileNames[0];
					if (File.Exists(sourcePathFileName))
					{
						string givenPath = Path.GetDirectoryName(givenPathFileName);
						string givenFolder = Path.GetFileName(givenPathFileName);
						string fileName = givenFolder + ".gma";
						processedPathFileName = Path.Combine(givenPath, fileName);
						processedPathFileName = FileManager.GetTestedPathFileName(processedPathFileName);
						File.Move(sourcePathFileName, processedPathFileName);
						Directory.Delete(givenPathFileName);
					}
				}
				else
				{
					processedPathFileName = givenPathFileName;
				}
			}
			else
			{
				string processedGivenPathFileName = Path.ChangeExtension(givenPathFileName, ".lzma");
				try
				{
					if (File.Exists(givenPathFileName))
					{
						File.Move(givenPathFileName, processedGivenPathFileName);
						bw.ReportProgress(0, "Renamed \"" + Path.GetFileName(givenPathFileName) + "\" to \"" + Path.GetFileName(processedGivenPathFileName) + "\"" + "\r\n");
					}
				}
				catch (Exception ex)
				{
					bw.ReportProgress(0, "Crowbar tried to rename the file \"" + Path.GetFileName(givenPathFileName) + "\" to \"" + Path.GetFileName(processedGivenPathFileName) + "\" but Windows gave this message: " + ex.Message);
				}

				processedPathFileName = Path.ChangeExtension(processedGivenPathFileName, ".gma");

				bw.ReportProgress(0, "Decompressing downloaded Garry's Mod workshop file into a GMA file." + "\r\n");
				Process lzmaExeProcess = new Process();
				try
				{
					lzmaExeProcess.StartInfo.UseShellExecute = false;
					//NOTE: From Microsoft website: 
					//      On Windows Vista and earlier versions of the Windows operating system, 
					//      the length of the arguments added to the length of the full path to the process must be less than 2080. 
					//      On Windows 7 and later versions, the length must be less than 32699. 
					//FROM BAT file: lzma.exe d %1 "%~n1.gma"
					lzmaExeProcess.StartInfo.FileName = MainCROWBAR.TheApp.LzmaExePathFileName;
					lzmaExeProcess.StartInfo.Arguments = "d \"" + processedGivenPathFileName + "\" \"" + processedPathFileName + "\"";
#if DEBUG
					lzmaExeProcess.StartInfo.CreateNoWindow = false;
#else
					lzmaExeProcess.StartInfo.CreateNoWindow = true;
#endif
					lzmaExeProcess.Start();
					lzmaExeProcess.WaitForExit();
				}
				catch (Exception ex)
				{
					throw new System.Exception("Crowbar tried to decompress the file \"" + processedGivenPathFileName + "\" to \"" + processedPathFileName + "\" but Windows gave this message: " + ex.Message);
				}
				finally
				{
					lzmaExeProcess.Close();
					bw.ReportProgress(0, "Decompress done." + "\r\n");
				}

				try
				{
					if (File.Exists(processedGivenPathFileName))
					{
						File.Delete(processedGivenPathFileName);
						bw.ReportProgress(0, "Deleted: \"" + processedGivenPathFileName + "\"" + "\r\n");
					}
				}
				catch (Exception ex)
				{
					bw.ReportProgress(0, "Crowbar tried to delete the file \"" + processedGivenPathFileName + "\" but Windows gave this message: " + ex.Message);
				}
			}

			return processedPathFileName;
		}

		public override string ProcessFileBeforeUpload(WorkshopItem item, BackgroundWorkerEx bw)
		{
			string processedPathFileName = item.ContentPathFolderOrFileName;
			theBackgroundWorker = bw;

			// Create a folder in the Windows Temp path, to prevent potential file collisions and to provide user a more obvious folder name.
			Guid guid = new Guid();
			guid = Guid.NewGuid();
			string tempCrowbarFolder = "Crowbar_" + guid.ToString();
			string tempPath = Path.GetTempPath();
			theTempCrowbarPath = Path.Combine(tempPath, tempCrowbarFolder);
			try
			{
				FileManager.CreatePath(theTempCrowbarPath);
			}
			catch (Exception ex)
			{
				throw new System.Exception("Crowbar tried to create folder path \"" + theTempCrowbarPath + "\", but Windows gave this message: " + ex.Message);
			}

			string gmaPathFileName = null;
			if (Directory.Exists(item.ContentPathFolderOrFileName))
			{
				theBackgroundWorker.ReportProgress(0, "Creating GMA file." + "\r\n");

				//NOTE: File name is all lowercase in case Garry's Mod needs that on Linux.
				if (item.IsDraft)
				{
					gmaPathFileName = Path.Combine(theTempCrowbarPath, "new_item_via_crowbar.gma");
				}
				else
				{
					gmaPathFileName = Path.Combine(theTempCrowbarPath, item.ID + "_via_crowbar.gma");
				}

				//TODO: Create GMA file without calling gmad.exe.
				string appInstallPath = "";
				SteamPipe steamPipe = new SteamPipe();
				string result = steamPipe.Open("GetAppInstallPath", null, "");
				if (result == "success")
				{
					appInstallPath = steamPipe.GetAppInstallPath(ID.ToString());
				}
				steamPipe.Shut();
				if (!string.IsNullOrEmpty(appInstallPath))
				{
					string garrysModBinPath = Path.Combine(appInstallPath, "bin");
					string garrysModPathGmadExe = Path.Combine(garrysModBinPath, "gmad.exe");
					if (File.Exists(garrysModPathGmadExe))
					{
						string addonJsonPathFileName = CreateAddonJsonFile(item.ContentPathFolderOrFileName, item.Title, item.Tags);
						if (File.Exists(addonJsonPathFileName))
						{
							Process gmadExeProcess = new Process();
							try
							{
								gmadExeProcess.StartInfo.UseShellExecute = false;
								//NOTE: From Microsoft website: 
								//      On Windows Vista and earlier versions of the Windows operating system, 
								//      the length of the arguments added to the length of the full path to the process must be less than 2080. 
								//      On Windows 7 and later versions, the length must be less than 32699. 
								gmadExeProcess.StartInfo.FileName = garrysModPathGmadExe;
								//gmad.exe create -folder "<FULL PATH TO ADDON FOLDER>" -out "<FULL PATH TO OUTPUT .gma FILE>"
								gmadExeProcess.StartInfo.Arguments = "create -folder \"" + item.ContentPathFolderOrFileName + "\" -out \"" + gmaPathFileName + "\"";
								gmadExeProcess.StartInfo.RedirectStandardOutput = true;
								gmadExeProcess.StartInfo.RedirectStandardError = true;
								gmadExeProcess.StartInfo.RedirectStandardInput = true;
#if DEBUG
								gmadExeProcess.StartInfo.CreateNoWindow = false;
#else
									gmadExeProcess.StartInfo.CreateNoWindow = true;
#endif
								gmadExeProcess.OutputDataReceived += myProcess_OutputDataReceived;
								gmadExeProcess.ErrorDataReceived += myProcess_ErrorDataReceived;

								gmadExeProcess.Start();
								gmadExeProcess.StandardInput.AutoFlush = true;
								gmadExeProcess.BeginOutputReadLine();
								gmadExeProcess.BeginErrorReadLine();
								gmadExeProcess.WaitForExit();
								//gmadExeProcess.Close()
							}
							catch (Exception ex)
							{
								throw new System.Exception("Crowbar tried to create the file \"" + gmaPathFileName + "\" with Garry's Mod gmad.exe, but got this error message: " + ex.Message);
							}
							finally
							{
								gmadExeProcess.Close();
								gmadExeProcess.OutputDataReceived -= myProcess_OutputDataReceived;
								gmadExeProcess.ErrorDataReceived -= myProcess_ErrorDataReceived;

								//If Not File.Exists(gmaPathFileName) Then
								//	Throw New System.Exception("Crowbar tried to create the file """ + gmaPathFileName + """ with Garry's Mod gmad.exe, but the file was not created.")
								//End If
							}
						}
						else
						{
							throw new System.Exception("Crowbar tried to create the file \"" + addonJsonPathFileName + "\", but the file was not created.");
						}
					}
					else
					{
						throw new System.Exception("Crowbar tried to run \"" + garrysModPathGmadExe + "\", but the file was not found. Note that Garry's Mod must be installed for this to work.");
					}
				}
			}
			else
			{
				gmaPathFileName = item.ContentPathFolderOrFileName;
			}

			//		Dim gmaFileInfo As New FileInfo(gmaPathFileName)
			//		Dim uncompressedFileSize As UInt32 = CUInt(gmaFileInfo.Length)

			//		'NOTE: Compress GMA file for Garry's Mod before uploading it.
			//		'      Calling lzma.exe (outside of Crowbar) works (i.e. subscribed item can be used within Garry's Mod), but does not compress to same bytes as Garry's Mod gmpublish.exe. 
			//		'      In tests, files were smaller, possibly because lzma.exe has newer compression code than what Garry's Mod gmpublish.exe has.

			//		Dim givenFileNameWithoutExtension As String
			//		givenFileNameWithoutExtension = Path.GetFileNameWithoutExtension(gmaPathFileName)
			//		processedPathFileName = Path.Combine(Me.theTempCrowbarPath, givenFileNameWithoutExtension + ".lzma")

			//		Try
			//			If File.Exists(processedPathFileName) Then
			//				File.Delete(processedPathFileName)
			//			End If
			//		Catch ex As Exception
			//			Throw New System.Exception("Crowbar tried to delete an old temp file """ + processedPathFileName + """ but Windows gave this message: " + ex.Message)
			//		End Try

			//		Me.theBackgroundWorker.ReportProgress(0, "Compressing GMA file." + vbCrLf)
			//		Dim lzmaExeProcess As New Process()
			//		Try
			//			lzmaExeProcess.StartInfo.UseShellExecute = False
			//			'NOTE: From Microsoft website: 
			//			'      On Windows Vista and earlier versions of the Windows operating system, 
			//			'      the length of the arguments added to the length of the full path to the process must be less than 2080. 
			//			'      On Windows 7 and later versions, the length must be less than 32699. 
			//			lzmaExeProcess.StartInfo.FileName = TheApp.LzmaExePathFileName
			//			'lzmaExeProcess.StartInfo.Arguments = "e """ + gmaPathFileName + """ """ + processedPathFileName + """ -d25 -fb256"
			//			'lzmaExeProcess.StartInfo.Arguments = "e """ + givenPathFileName + """ """ + processedPathFileName + """ -d25"
			//			lzmaExeProcess.StartInfo.Arguments = "e """ + gmaPathFileName + """ """ + processedPathFileName + """ -d25 -fb32"
			//#If DEBUG Then
			//			lzmaExeProcess.StartInfo.CreateNoWindow = False
			//#Else
			//				lzmaExeProcess.StartInfo.CreateNoWindow = True
			//#End If
			//			lzmaExeProcess.Start()
			//			lzmaExeProcess.WaitForExit()
			//			lzmaExeProcess.Close()
			//		Catch ex As Exception
			//			Throw New System.Exception("Crowbar tried to compress the file """ + gmaPathFileName + """ to """ + processedPathFileName + """ but Windows gave this message: " + ex.Message)
			//		Finally
			//			lzmaExeProcess.Close()
			//		End Try

			//		' Write 8 extra bytes after the lzma compressed data: 4 bytes for uncompressed file size and 4 magic bytes (BEEFCACE), both values in little-endian order.
			//		Dim outputFileStream As FileStream = Nothing
			//		Try
			//			If File.Exists(processedPathFileName) Then
			//				outputFileStream = New FileStream(processedPathFileName, FileMode.Open)
			//				If outputFileStream IsNot Nothing Then
			//					Dim inputFileWriter As BinaryWriter = Nothing
			//					Try
			//						inputFileWriter = New BinaryWriter(outputFileStream)

			//						inputFileWriter.Seek(0, SeekOrigin.End)
			//						inputFileWriter.Write(uncompressedFileSize)
			//						'-1091581234   BEEFCACE in little endian order: CE CA EF BE
			//						inputFileWriter.Write(-1091581234)
			//					Catch
			//					Finally
			//						If inputFileWriter IsNot Nothing Then
			//							inputFileWriter.Close()
			//						End If
			//					End Try
			//				End If
			//			End If
			//		Catch
			//		Finally
			//			If outputFileStream IsNot Nothing Then
			//				outputFileStream.Close()
			//			End If
			//		End Try

			processedPathFileName = gmaPathFileName;
			return processedPathFileName;
		}

		public override void CleanUpAfterUpload(BackgroundWorkerEx bw)
		{
			if (Directory.Exists(theTempCrowbarPath))
			{
				try
				{
					Directory.Delete(theTempCrowbarPath, true);
				}
				catch (Exception ex)
				{
					bw.ReportProgress(0, "Crowbar tried to delete its temp folder \"" + theTempCrowbarPath + "\" but Windows gave this message: " + ex.Message);
				}
			}
			theTempCrowbarPath = "";
		}

		//Example 01:
		//{
		//	"title"		:	"My Server Content",
		//	"type"		:	"ServerContent",
		//	"tags"		:	[ "roleplay", "realism" ],
		//	"ignore"	:
		//	[
		//		"*.psd",
		//		"*.vcproj",
		//		"*.svn*"
		//	]
		//}
		//Example 02:
		//{
		//	"title": "Ragdoll Fight",
		//	"type": "tool",
		//	"tags": 
		//	[
		//		"scenic",
		//		"fun"
		//	]
		//}
		public string CreateAddonJsonFile(string addonJsonPath, string itemTitle, BindingListEx<string> itemTags)
		{
			string addonJsonPathFileName = Path.Combine(addonJsonPath, "addon.json");

			ArrangeTagsForEasierUseInAddonJsonFile(ref itemTags);

			try
			{
				if (File.Exists(addonJsonPathFileName))
				{
					//NOTE: User's data in Crowbar overrides data in "addon.json" file.
					File.Delete(addonJsonPathFileName);
				}
			}
			catch (Exception ex)
			{
				throw new System.Exception("Crowbar tried to delete an old temp file \"" + addonJsonPathFileName + "\" but Windows gave this message: " + ex.Message);
			}

			// Remove the "Addon" tag because it should not go into the json file.
			itemTags.Remove("Addon");

			StreamWriter fileStream = File.CreateText(addonJsonPathFileName);
			fileStream.AutoFlush = true;
			try
			{
				JavaScriptSerializer jss = new JavaScriptSerializer();
				if (File.Exists(addonJsonPathFileName))
				{
					fileStream.WriteLine("{");
					fileStream.WriteLine("\t" + "\"title\": " + jss.Serialize(itemTitle) + ",");
					if (itemTags.Count > 1)
					{
						fileStream.WriteLine("\t" + "\"type\": " + jss.Serialize(itemTags[0]) + ",");
						fileStream.WriteLine("\t" + "\"tags\": ");
						fileStream.WriteLine("\t" + "[");
						if (itemTags.Count > 2)
						{
							fileStream.WriteLine("\t" + "\t" + jss.Serialize(itemTags[1]) + ",");
							fileStream.WriteLine("\t" + "\t" + jss.Serialize(itemTags[2]));
						}
						else
						{
							fileStream.WriteLine("\t" + "\t" + jss.Serialize(itemTags[1]));
						}
						fileStream.WriteLine("\t" + "]");
					}
					else
					{
						fileStream.WriteLine("\t" + "\"type\": " + jss.Serialize(itemTags[0]));
					}
					fileStream.WriteLine("}");
					fileStream.Flush();
				}
			}
			catch (Exception ex)
			{
				//NOTE: This is here in case I missed something, such as itemTags being empty.
				int debug = 4242;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Flush();
					fileStream.Close();
					fileStream = null;
				}

				//NOTE: Add the "Addon" tag back because itemTags is an object used throughout app and is not a local copy.
				itemTags.Add("Addon");
			}

			return addonJsonPathFileName;
		}

		public void ReadDataFromAddonJsonFile(string addonJsonPathFileName, ref string itemTitle, ref BindingListEx<string> itemTags)
		{
			if (File.Exists(addonJsonPathFileName))
			{
				StreamReader fileStream = new StreamReader(addonJsonPathFileName);
				string addonFileContents = null;

				try
				{
					addonFileContents = fileStream.ReadToEnd();
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
				finally
				{
					fileStream.Close();
				}

				if (addonFileContents != null && !string.IsNullOrEmpty(addonFileContents))
				{
					JavaScriptSerializer jss = new JavaScriptSerializer();
					GarrysMod_AddonJson addon = jss.Deserialize<GarrysMod_AddonJson>(addonFileContents);

					itemTitle = addon.title;
					itemTags.Clear();
					itemTags.Add(addon.type);
					foreach (string tag in addon.tags)
					{
						itemTags.Add(tag);
					}
					for (int tagIndex = 0; tagIndex < itemTags.Count; tagIndex++)
					{
						if (itemTags[tagIndex] != "ServerContent" && itemTags[tagIndex] != "Addon")
						{
							if (itemTags[tagIndex].Length > 1)
							{
								itemTags[tagIndex] = itemTags[tagIndex].Substring(0, 1).ToUpper() + itemTags[tagIndex].Substring(1);
							}
							else if (itemTags[tagIndex].Length == 1)
							{
								itemTags[tagIndex] = itemTags[tagIndex].ToUpper();
							}
						}
					}
				}
			}
		}

		private void ArrangeTagsForEasierUseInAddonJsonFile(ref BindingListEx<string> tags)
		{
			IList anEnumList = EnumHelper.ToList(typeof(GarrysModTypeTags));
			int index = 0;
			foreach (string tag in tags)
			{
				index = EnumHelper.IndexOfKeyAsCaseInsensitiveString(tag, anEnumList);
				if (index != -1)
				{
					tags.Remove(tag);
					tags.Insert(0, tag);
					break;
				}
			}
			for (int tagIndex = 0; tagIndex < tags.Count; tagIndex++)
			{
				if (tags[tagIndex] != "ServerContent" && tags[tagIndex] != "Addon")
				{
					tags[tagIndex] = tags[tagIndex].ToLower();
				}
			}
			//NOTE: Not sure how this became empty for me in testing, so let's make sure there is a tag so Crowbar does not show exception window.
			if (tags.Count == 0)
			{
				tags.Add("ServerContent");
			}
		}

		private void myProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
		{
			Process myProcess = (Process)sender;
			string line = null;

			try
			{
				line = e.Data;
				if (line != null)
				{
					//Me.theProcessHasOutputData = True
					theBackgroundWorker.ReportProgress(1, line + "\r\n");
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				//If Me.CancellationPending Then
				//	Me.StopPack(True, myProcess)
				//ElseIf Me.theSkipCurrentModelIsActive Then
				//	Me.StopPack(True, myProcess)
				//End If
			}
		}

		private void myProcess_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
		{
			Process myProcess = (Process)sender;
			string line = null;

			try
			{
				line = e.Data;
				if (line != null)
				{
					theBackgroundWorker.ReportProgress(1, line + "\r\n");
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				//If Me.CancellationPending Then
				//	Me.StopPack(True, myProcess)
				//ElseIf Me.theSkipCurrentModelIsActive Then
				//	Me.StopPack(True, myProcess)
				//End If
			}
		}

		public enum GarrysModTypeTags
		{
			[Description("Effects")]
			Effects,
			[Description("Game Mode")]
			Gamemode,
			[Description("Map")]
			Map,
			[Description("Model")]
			Model,
			[Description("NPC")]
			NPC,
			[Description("Server Content")]
			ServerContent,
			[Description("Tool")]
			Tool,
			[Description("Vehicle")]
			Vehicle,
			[Description("Weapon")]
			Weapon
		}

		private BackgroundWorkerEx theBackgroundWorker;
		private string theTempCrowbarPath;

#region Notes about other attempts at compressing GMA file

		//NOTE: C# library was too slow to compress.
		//If Me.ID = GarrysModAppID Then
		//	Using gmaStream As FileStream = New FileStream(givenPathFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)
		//		Dim ms As MemoryStream = Nothing
		//		Try
		//			ms = CType(LZMA.LZMAEncodeStream.CompressStreamLZMA(gmaStream), MemoryStream)

		//			processedPathFileName = Path.ChangeExtension(givenPathFileName, ".lzma")
		//			Using outStream As FileStream = New FileStream(processedPathFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)
		//				ms.WriteTo(outStream)
		//			End Using
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		Finally
		//			If ms IsNot Nothing Then
		//				ms.Close()
		//			End If
		//		End Try
		//	End Using
		//End If
		//======
		//NOTE: SevenZipCompressor requires library files to be in same folder as Crowbar.exe, but want to put them in user "%appdata%" folder.
		//If Me.ID = GarrysModAppID Then
		//	Using gmaStream As FileStream = New FileStream(givenPathFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)
		//		Try
		//			processedPathFileName = Path.ChangeExtension(givenPathFileName, ".lzma")
		//			Using outStream As FileStream = New FileStream(processedPathFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)
		//				'TODO: This line raises exception if SevenZipSharp.dll is not beside Crowbar.exe.
		//				SevenZip.SevenZipCompressor.SetLibraryPath(TheApp.SevenZDLLPathFileName)
		//				'SevenZip.SevenZipCompressor.SetLibraryPath("")
		//				'SevenZip.SevenZipCompressor.SetLibraryPath("C:\Program Files\7-Zip\7z.dll")
		//				Dim compress As SevenZip.SevenZipCompressor = New SevenZip.SevenZipCompressor()
		//				compress.ArchiveFormat = SevenZip.OutArchiveFormat.Zip
		//				compress.CompressionMethod = SevenZip.CompressionMethod.Lzma
		//				compress.CompressionLevel = SevenZip.CompressionLevel.High
		//				compress.CompressStream(gmaStream, outStream)
		//				'compressor.CompressFiles(compressedFile, new string[] { sourceFile })
		//			End Using
		//		Catch ex As Exception
		//			Dim debug As Integer = 4242
		//		Finally
		//		End Try
		//	End Using
		//End If

#endregion

	}

}