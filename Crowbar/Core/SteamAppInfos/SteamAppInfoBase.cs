//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Runtime.CompilerServices;
using Steamworks;

namespace Crowbar
{
	public class SteamAppInfoBase
	{

#region Change this section when adding or deleting a game

		//Private BladeSymphonyAppID As New AppId_t(225600)
		//Private EstrangedActIAppID As New AppId_t(261820)
		//Private SourceFilmmakerAppID As New AppId_t(1840)
		//Private SourceSDKAppID As New AppId_t(211)
		//Private TeamFortress2AppID As New AppId_t(440)
		//Private ZombiePanicSourceAppID As New AppId_t(17500)
		//If Me.Name = "Anarchy Arcade" Then
		//	Me.Create(AnarchyArcadeAppID, True, contentFileExtensionList, GetType(AnarchyArcadeTagsUserControl))
		//ElseIf Me.Name = "Black Mesa" Then
		//	Me.Create(BlackMesaAppID, True, contentFileExtensionList, GetType(BlackMesaTagsUserControl))
		//ElseIf Me.Name = "Blade Symphony" Then
		//	Me.Create(BladeSymphonyAppID, False, contentFileExtensionList, GetType(Base_TagsUserControl))
		//ElseIf Me.Name = "Contagion" Then
		//	contentFileExtensionList.Add("vpk", "Source Engine VPK Files")
		//	Me.Create(ContagionAppID, False, contentFileExtensionList, GetType(ContagionTagsUserControl))
		//ElseIf Me.Name = "Estranged: Act I" Then
		//	Me.Create(EstrangedActIAppID, True, contentFileExtensionList, GetType(EstrangedActITagsUserControl))
		//ElseIf Me.Name = "Garry's Mod" Then
		//	Me.CanUseContentFolderOrFile = True
		//	contentFileExtensionList.Add("gma", "Garry's Mod GMA Files")
		//	Me.Create(GarrysModAppID, False, contentFileExtensionList, GetType(GarrysModTagsUserControl))
		//ElseIf Me.Name = "Left 4 Dead 2" Then
		//	contentFileExtensionList.Add("vpk", "Source Engine VPK Files")
		//	Me.Create(Left4Dead2AppID, False, contentFileExtensionList, GetType(Left4Dead2TagsUserControl))
		//ElseIf Me.Name = "Source Filmmaker" Then
		//	Me.Create(SourceFilmmakerAppID, False, contentFileExtensionList, GetType(SourceFilmmakerTagsUserControl))
		//ElseIf Me.Name = "Source SDK" Then
		//	Me.Create(SourceSDKAppID, False, contentFileExtensionList, GetType(Base_TagsUserControl))
		//ElseIf Me.Name = "Team Fortress 2" Then
		//	Me.Create(TeamFortress2AppID, False, contentFileExtensionList, GetType(Base_TagsUserControl))
		//ElseIf Me.Name = "Zombie Panic! Source" Then
		//	Me.Create(ZombiePanicSourceAppID, True, contentFileExtensionList, GetType(Base_TagsUserControl))	
		public static List<SteamAppInfoBase> GetSteamAppInfos()
		{
			List<SteamAppInfoBase> steamAppInfos = new List<SteamAppInfoBase>();

			SteamAppInfoBase anAppInfo;

			//anAppInfo = New AnarchyArcadeAppInfo(GetType(AnarchyArcadeTagsUserControl))
			//steamAppInfos.Add(anAppInfo)
			anAppInfo = new BlackMesaSteamAppInfo();
			steamAppInfos.Add(anAppInfo);
			//anAppInfo = New SteamAppInfo("Blade Symphony")
			//steamAppInfos.Add(anAppInfo)
			anAppInfo = new ContagionSteamAppInfo();
			steamAppInfos.Add(anAppInfo);
			//anAppInfo = New SteamAppInfo("Estranged: Act I")
			//steamAppInfos.Add(anAppInfo)
			anAppInfo = new GarrysModSteamAppInfo();
			steamAppInfos.Add(anAppInfo);
			anAppInfo = new Left4Dead2SteamAppInfo();
			steamAppInfos.Add(anAppInfo);
			//anAppInfo = New SteamAppInfo("Source Filmmaker")
			//steamAppInfos.Add(anAppInfo)
			//anAppInfo = New SteamAppInfo("Team Fortress 2")
			//steamAppInfos.Add(anAppInfo)
			anAppInfo = new ZombiePanicSourceSteamAppInfo();
			steamAppInfos.Add(anAppInfo);

			return steamAppInfos;
		}

#endregion

		public SteamAppInfoBase()
		{
			this.ContentFileExtensionsAndDescriptions = new SortedList<string, string>();
		}

		public virtual string ProcessFileAfterDownload(string givenPathFileName, BackgroundWorkerEx bw)
		{
			string processedPathFileName = givenPathFileName;
			return processedPathFileName;
		}

		public virtual string ProcessFileBeforeUpload(WorkshopItem item, BackgroundWorkerEx bw)
		{
			string processedPathFileName = item.ContentPathFolderOrFileName;
			return processedPathFileName;
		}

		public virtual void CleanUpAfterUpload(BackgroundWorkerEx bw)
		{
		}

#region Delegates

#endregion

#region Data

		public AppId_t ID {get; set;}
		public string Name {get; set;}
		public bool UsesSteamUGC {get; set;}
		public bool CanUseContentFolderOrFile {get; set;}
		public SortedList<string, string> ContentFileExtensionsAndDescriptions {get; set;}
		public Type TagsControlType {get; set;}

#endregion

	}

}