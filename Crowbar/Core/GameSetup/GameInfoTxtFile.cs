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
	public class GameInfoTxtFile
	{

#region Creation and Destruction

		// This is only way to create the GameInfoTxtFile singleton object.
		public static GameInfoTxtFile Create()
		{
			if (GameInfoTxtFile.theGameInfoTxtFile == null)
			{
				GameInfoTxtFile.theGameInfoTxtFile = new GameInfoTxtFile();
			}
			return GameInfoTxtFile.theGameInfoTxtFile;
		}

		// Only here to make compiler happy with anything that inherits from this class.
		protected GameInfoTxtFile()
		{
			theGameInfoPathFileNames = new List<string>(2);
			theBackupGameInfoPathFileNames = new SortedList<string, string>(2);
		}

#endregion

#region Methods

		public void WriteNewGamePath(string gameInfoPathFileName, string newGamePath)
		{
			MakeBackupOfGameInfoFile(gameInfoPathFileName, newGamePath);

			StreamReader sr = null;
			StreamWriter sw = null;
			string fullText = "";
			string textUptoPosition = "";
			string textPastPosition = "";

			try
			{
				if (File.Exists(gameInfoPathFileName))
				{
					string buffer = null;
					string token = "";

					sr = new StreamReader(gameInfoPathFileName);
					fullText = sr.ReadToEnd();
					buffer = fullText;

					token = FileManager.ReadKeyValueToken(ref buffer);
					if (token == "GameInfo")
					{
						token = FileManager.ReadKeyValueToken(ref buffer);
						if (token == "{")
						{
							while (!string.IsNullOrEmpty(token))
							{
								token = FileManager.ReadKeyValueToken(ref buffer);
								if (token == "FileSystem")
								{
									token = FileManager.ReadKeyValueToken(ref buffer);
									if (token == "{")
									{
										while (!string.IsNullOrEmpty(token))
										{
											token = FileManager.ReadKeyValueToken(ref buffer);
											if (token == "SearchPaths")
											{
												token = FileManager.ReadKeyValueToken(ref buffer);
												if (token == "{")
												{
													string token2 = "";
													long searchPath0Position = sr.BaseStream.Length - buffer.Length;
													textUptoPosition = fullText.Substring(0, (int)searchPath0Position);
													textPastPosition = buffer;
													token = FileManager.ReadKeyValueToken(ref buffer);
													token2 = FileManager.ReadKeyValueToken(ref buffer);
													if (token == "Game" && token2 == newGamePath)
													{
														//NOTE: Do nothing because crowbar search path already exists.
													}
													else
													{
														sr.Close();
														WriteModifiedFile(gameInfoPathFileName, newGamePath, textUptoPosition, textPastPosition);
													}
												}
												break;
											}
										}
									}
									break;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
				}
			}
		}

		public void RestoreGameInfoFile(string gameInfoPathFileName)
		{
			try
			{
				if (theGameInfoPathFileNames.Contains(gameInfoPathFileName))
				{
					theGameInfoPathFileNames.Remove(gameInfoPathFileName);

					if (!theGameInfoPathFileNames.Contains(gameInfoPathFileName))
					{
						if (theBackupGameInfoPathFileNames.ContainsKey(gameInfoPathFileName))
						{
							string backupPathFileName = theBackupGameInfoPathFileNames[gameInfoPathFileName];

							if (File.Exists(backupPathFileName))
							{
								File.Copy(backupPathFileName, gameInfoPathFileName, true);
								File.Delete(backupPathFileName);
								theBackupGameInfoPathFileNames.Remove(gameInfoPathFileName);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

#endregion

#region Private Methods

		private void MakeBackupOfGameInfoFile(string gameInfoPathFileName, string newGamePath)
		{
			try
			{
				theGameInfoPathFileNames.Add(gameInfoPathFileName);

				if (!theBackupGameInfoPathFileNames.ContainsKey(gameInfoPathFileName))
				{
					if (File.Exists(gameInfoPathFileName))
					{
						string backupPathFileName = Path.Combine(FileManager.GetPath(gameInfoPathFileName), Path.GetFileNameWithoutExtension(gameInfoPathFileName) + "_" + newGamePath + Path.GetExtension(gameInfoPathFileName));

						File.Copy(gameInfoPathFileName, backupPathFileName, true);
						theBackupGameInfoPathFileNames.Add(gameInfoPathFileName, backupPathFileName);
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private void WriteModifiedFile(string gameInfoPathFileName, string newGamePath, string textUptoPosition, string textPastPosition)
		{
			StreamWriter sw = null;

			try
			{
				sw = new StreamWriter(gameInfoPathFileName);

				sw.Write(textUptoPosition);

				// Write line terminator, then write Game{4 tabs}crowbar
				sw.WriteLine();
				sw.Write("\t" + "\t" + "\t" + "Game" + "\t" + "\t" + "\t" + "\t" + newGamePath);

				sw.Write(textPastPosition);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (sw != null)
				{
					sw.Close();
				}
			}
		}

#endregion

#region Data

		private static GameInfoTxtFile theGameInfoTxtFile;

		// This var is used for simple resource counting.
		protected List<string> theGameInfoPathFileNames;
		protected SortedList<string, string> theBackupGameInfoPathFileNames;

#endregion

	}

}