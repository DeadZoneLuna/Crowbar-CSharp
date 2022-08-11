using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	internal static class VersionModule
	{
		public static void ConvertSettingsFile(string appSettingsPathFileName)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(appSettingsPathFileName);

			if (!ConvertVisibilityPrivateToHidden(xmlDoc))
				return;

			//NOTE: "ChangeDirectory" to settings folder to avoid problems with longer filenames.
			string currentFolder = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(Path.GetDirectoryName(appSettingsPathFileName));

			string settingsFileName = Path.GetFileName(appSettingsPathFileName);
			string backupSettingsFileName = Path.GetFileNameWithoutExtension(appSettingsPathFileName) + " [backup " + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + "]" + Path.GetExtension(appSettingsPathFileName);

			if (!File.Exists(backupSettingsFileName))
				File.Copy(settingsFileName, backupSettingsFileName, false);

			Directory.SetCurrentDirectory(currentFolder);
			xmlDoc.Save(appSettingsPathFileName);
		}

		#region Private Methods
		private static XmlElement CopyElementToName(XmlElement element, string tagName)
		{
			XmlElement newElement = element.OwnerDocument.CreateElement(tagName);
			for (int i = 0; i < element.Attributes.Count; i++)
				newElement.SetAttributeNode((XmlAttribute)(element.Attributes[i].CloneNode(true)));

			for (int i = 0; i < element.ChildNodes.Count; i++)
				newElement.AppendChild(element.ChildNodes[i].CloneNode(true));

			return newElement;
		}

		private static void RenameNodes(XmlDocument xmlDoc, string oldName, string newName)
		{
			XmlNodeList xmlNodes = xmlDoc.SelectNodes(oldName);
			foreach (XmlNode anXmlNode in xmlNodes)
			{
				XmlNode parentNode = anXmlNode.ParentNode;
				XmlElement newElement = CopyElementToName((XmlElement)anXmlNode, newName);
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