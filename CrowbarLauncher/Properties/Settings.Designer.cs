﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;

namespace CrowbarLauncher
{
	namespace Properties
	{

		[System.Runtime.CompilerServices.CompilerGeneratedAttribute(), System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0"), System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		internal sealed partial class Settings : System.Configuration.ApplicationSettingsBase
		{
			private static Settings defaultInstance = (Settings)System.Configuration.ApplicationSettingsBase.Synchronized(new Settings());

#region My.Settings Auto-Save Functionality
#if WINDOWSFORMS
		private static bool addedHandler;

		private static object addedHandlerLockObject = new object();

		[System.Diagnostics.DebuggerNonUserCodeAttribute(), System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		private static void AutoSaveSettings(System.Object sender, System.EventArgs e)
		{
			if (My.MyApplication.Application.SaveMySettingsOnExit)
			{
				defaultInstance.Save();
			}
		}
#endif
#endregion

			public static Settings Default
			{
				get
				{

#if WINDOWSFORMS
				   if (!addedHandler)
				   {
						lock (addedHandlerLockObject)
						{
							if (!addedHandler)
							{
								My.MyApplication.Application.Shutdown += new Microsoft.VisualBasic.ApplicationServices.ShutdownEventHandler(AutoSaveSettings);
								addedHandler = true;
							}
						}
					}
#endif
					return defaultInstance;
				}
			}
		}
	}

//INSTANT C# NOTE: This block was only required to support 'My.Settings' in VB. 'Properties.Settings' is used in C#:
//Namespace My
//
//	<Microsoft.VisualBasic.HideModuleNameAttribute(), System.Diagnostics.DebuggerNonUserCodeAttribute(), System.Runtime.CompilerServices.CompilerGeneratedAttribute()> Friend Module MySettingsProperty
//
//		<System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")> Friend ReadOnly Property Settings() As global::CrowbarLauncher.My.MySettings
//			Get
//				return global::CrowbarLauncher.My.MySettings.Default
//			End Get
//		End Property
//	End Module
//End Namespace

}