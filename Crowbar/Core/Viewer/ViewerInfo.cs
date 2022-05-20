//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class ViewerInfo
	{

		public ViewerActionType viewerAction;
		public int gameSetupSelectedIndex;
		public string mdlPathFileName;
		public bool viewAsReplacement;
		public string viewAsReplacementExtraSubfolder;
		public AppEnums.SupportedMdlVersion mdlVersionOverride;

		public enum ViewerActionType
		{
			GetData,
			ViewModel,
			OpenViewer
		}

	}

}