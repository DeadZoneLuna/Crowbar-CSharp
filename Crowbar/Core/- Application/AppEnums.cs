//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace Crowbar
{
	public static class AppEnums
	{

		public enum InputOptions
		{
			[Description("File")]
			File,
			[Description("Folder")]
			Folder,
			[Description("Folder and subfolders")]
			FolderRecursion
		}

		public enum DownloadOutputPathOptions
		{
			//<Description("Downloads folder")> DownloadsFolder
			[Description("Documents folder")]
			DocumentsFolder,
			[Description("Work folder")]
			WorkFolder
		}

		public enum UnpackOutputPathOptions
		{
			[Description("Same folder (as Package)")]
			SameFolder,
			[Description("Subfolder (of Package)")]
			Subfolder,
			[Description("Work folder")]
			WorkFolder,
			[Description("Game's addons folder")]
			GameAddonsFolder
		}

		public enum UnpackSearchFieldOptions
		{
			[Description("Files")]
			Files,
			[Description("Files and Folders")]
			FilesAndFolders
		}

		public enum DecompileOutputPathOptions
		{
			[Description("Work folder")]
			WorkFolder,
			[Description("Subfolder (of MDL input)")]
			Subfolder
		}

		public enum CompileOutputPathOptions
		{
			[Description("Game's \"models\" folder")]
			GameModelsFolder,
			[Description("Work folder")]
			WorkFolder,
			[Description("Subfolder (of QC input)")]
			Subfolder
		}

		public enum PackInputOptions
		{
			[Description("Folder")]
			Folder,
			[Description("Parent of child folders")]
			ParentFolder
		}

		public enum PackOutputPathOptions
		{
			[Description("Work folder")]
			WorkFolder,
			[Description("Parent folder")]
			ParentFolder
		}

		public enum PublishSearchFieldOptions
		{
			[Description("ID")]
			ID,
			[Description("Owner")]
			Owner,
			[Description("Title")]
			Title,
			[Description("Description")]
			Description,
			[Description("[All fields]")]
			AllFields
		}

		public enum StatusMessage
		{
			[Description("Success")]
			Success,
			[Description("Error")]
			Error,
			[Description("Canceled")]
			Canceled,
			[Description("Skipped")]
			Skipped,

			[Description("ErrorUnableToCreateTempFolder")]
			ErrorUnableToCreateTempFolder,

			[Description("ErrorRequiredSequenceGroupMdlFileNotFound")]
			ErrorRequiredSequenceGroupMdlFileNotFound,
			[Description("ErrorRequiredTextureMdlFileNotFound")]
			ErrorRequiredTextureMdlFileNotFound,

			[Description("ErrorRequiredMdlFileNotFound")]
			ErrorRequiredMdlFileNotFound,
			[Description("ErrorRequiredAniFileNotFound")]
			ErrorRequiredAniFileNotFound,
			[Description("ErrorRequiredVtxFileNotFound")]
			ErrorRequiredVtxFileNotFound,
			[Description("ErrorRequiredVvdFileNotFound")]
			ErrorRequiredVvdFileNotFound,

			[Description("ErrorInvalidMdlFileId")]
			ErrorInvalidMdlFileId,
			[Description("ErrorInvalidInternalMdlFileSize")]
			ErrorInvalidInternalMdlFileSize
		}

		[FlagsAttribute]
		public enum FilesFoundFlags
		{
			[Description("AllFilesFound")]
			AllFilesFound = 0,
			[Description("ErrorRequiredSequenceGroupMdlFileNotFound")]
			ErrorRequiredSequenceGroupMdlFileNotFound = 1,
			[Description("ErrorRequiredTextureMdlFileNotFound")]
			ErrorRequiredTextureMdlFileNotFound = 2,

			[Description("ErrorRequiredMdlFileNotFound")]
			ErrorRequiredMdlFileNotFound = 4,
			[Description("ErrorRequiredAniFileNotFound")]
			ErrorRequiredAniFileNotFound = 8,
			[Description("ErrorRequiredVtxFileNotFound")]
			ErrorRequiredVtxFileNotFound = 16,
			[Description("ErrorRequiredVvdFileNotFound")]
			ErrorRequiredVvdFileNotFound = 32,

			[Description("Error")]
			Error = 64
		}

		public enum ActionType
		{
			[Description("Unknown")]
			Unknown,
			[Description("SetUpGames")]
			SetUpGames,
			[Description("Download")]
			Download,
			[Description("Unpack")]
			Unpack,
			[Description("Preview")]
			Preview,
			[Description("Decompile")]
			Decompile,
			[Description("Edit")]
			Edit,
			[Description("Compile")]
			Compile,
			[Description("View")]
			View,
			[Description("Pack")]
			Pack,
			[Description("Publish")]
			Publish
			//<Description("Options")> Options
		}

		public enum ContainerType
		{
			GMA,
			VPK
		}

		public enum ArchiveAction
		{
			Undefined,
			//Extract
			ExtractAndOpen,
			ExtractToTemp,
			ExtractFolderTree,
			Insert,
			List,
			Pack,
			Unpack
		}

		public enum ViewerType
		{
			[Description("Preview")]
			Preview,
			[Description("View")]
			View
		}

		public enum DecompiledFileType
		{
			QC,
			ReferenceMesh,
			LodMesh,
			BoneAnimation,
			PhysicsMesh,
			VertexAnimation,
			ProceduralBones,
			TextureBmp,
			Debug,
			DeclareSequenceQci
		}

		public enum ProgressOptions
		{
			WarningPhyFileChecksumDoesNotMatchMdlFileChecksum,

			WritingFileStarted,
			WritingFileFailed,
			WritingFileFinished
		}

		public enum FindDirection
		{
			Previous,
			Next
		}

		public enum GameEngine
		{
			[Description("GoldSource")]
			GoldSource,
			[Description("Source")]
			Source,
			[Description("Source 2")]
			Source2
		}

		public enum SupportedMdlVersion
		{
			[Description("Do not override")]
			DoNotOverride,
			[Description("06")]
			MDLv06,
			[Description("10")]
			MDLv10,
			[Description("2531")]
			MDLv2531,
			[Description("27")]
			MDLv27,
			[Description("28")]
			MDLv28,
			[Description("29")]
			MDLv29,
			[Description("30")]
			MDLv30,
			[Description("31")]
			MDLv31,
			[Description("32")]
			MDLv32,
			[Description("35")]
			MDLv35,
			[Description("36")]
			MDLv36,
			[Description("37")]
			MDLv37,
			[Description("38")]
			MDLv38,
			[Description("44")]
			MDLv44,
			[Description("45")]
			MDLv45,
			[Description("46")]
			MDLv46,
			[Description("47")]
			MDLv47,
			[Description("48")]
			MDLv48,
			[Description("49")]
			MDLv49,
			[Description("52")]
			MDLv52,
			[Description("53")]
			MDLv53,
			[Description("57")]
			MDLv57
		}

		public enum OrientationType
		{
			[Description("Horizontal")]
			Horizontal,
			[Description("Vertical")]
			Vertical
		}

	}

}