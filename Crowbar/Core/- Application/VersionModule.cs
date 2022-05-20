//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
using System.Xml;

namespace Crowbar
{
	internal static class VersionModule
	{

		public static void ConvertSettingsFile(string appSettingsPathFileName)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(appSettingsPathFileName);

			bool fileIsChanged = false;
			if (ConvertVisibilityPrivateToHidden(xmlDoc))
			{
				fileIsChanged = true;
			}

			if (fileIsChanged)
			{
				string currentFolder = null;
				string settingsFileName = null;
				string backupSettingsFileName = null;

				//NOTE: "ChangeDirectory" to settings folder to avoid problems with longer filenames.
				currentFolder = System.IO.Directory.GetCurrentDirectory();
				System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(appSettingsPathFileName));

				settingsFileName = Path.GetFileName(appSettingsPathFileName);
				backupSettingsFileName = Path.GetFileNameWithoutExtension(appSettingsPathFileName) + " [backup " + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + "]" + Path.GetExtension(appSettingsPathFileName);

				if (!File.Exists(backupSettingsFileName))
				{
					System.IO.File.Copy(settingsFileName, backupSettingsFileName, false);
				}

				System.IO.Directory.SetCurrentDirectory(currentFolder);

				xmlDoc.Save(appSettingsPathFileName);
			}
		}

#region Private Methods

		private static XmlElement CopyElementToName(XmlElement element, string tagName)
		{
			XmlElement newElement = element.OwnerDocument.CreateElement(tagName);

			for (int i = 0; i < element.Attributes.Count; i++)
			{
				newElement.SetAttributeNode((XmlAttribute)(element.Attributes[i].CloneNode(true)));
			}

			for (int i = 0; i < element.ChildNodes.Count; i++)
			{
				newElement.AppendChild(element.ChildNodes[i].CloneNode(true));
			}

			return newElement;
		}

		private static void RenameNodes(XmlDocument xmlDoc, string oldName, string newName)
		{
			XmlNodeList xmlNodes = null;
			XmlNode parentNode = null;
			XmlElement newElement = null;

			xmlNodes = xmlDoc.SelectNodes(oldName);
			foreach (XmlNode anXmlNode in xmlNodes)
			{
				parentNode = anXmlNode.ParentNode;
				newElement = CopyElementToName((XmlElement)anXmlNode, newName);
				parentNode.RemoveChild(anXmlNode);
				parentNode.AppendChild(newElement);
			}
		}

		private static bool ConvertVisibilityPrivateToHidden(XmlDocument xmlDoc)
		{
			bool fileIsChanged = false;

			XmlNodeList xmlNodes = xmlDoc.SelectNodes("//WorkshopItem/Visibility");

			foreach (XmlNode anXmlNode in xmlNodes)
			{
				if (anXmlNode.InnerText == "Private")
				{
					anXmlNode.InnerText = "Hidden";
					fileIsChanged = true;
				}
			}

			return fileIsChanged;
		}

#endregion

	}

}