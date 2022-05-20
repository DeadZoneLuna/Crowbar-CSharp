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
	public class FileSeekLog
	{

		public FileSeekLog()
		{
			this.theFileSeekList = new SortedList<long, long>();
			this.theFileSeekDescriptionList = new SortedList<long, string>();
		}

		public bool ContainsKey(long startOffset)
		{
			return this.theFileSeekList.ContainsKey(startOffset);
		}

		public void Add(long startOffset, long endOffset, string description)
		{
			try
			{
				if (this.theFileSeekList.ContainsKey(startOffset) && this.theFileSeekList[startOffset] == endOffset)
				{
					this.theFileSeekDescriptionList[startOffset] += "; " + description;
				}
				else if (this.theFileSeekList.ContainsKey(startOffset))
				{
					string temp = this.theFileSeekDescriptionList[startOffset];
					this.theFileSeekDescriptionList[startOffset] = "[ERROR] ";
					this.theFileSeekDescriptionList[startOffset] += temp + "; [" + startOffset.ToString() + " - " + endOffset.ToString() + "] " + description;
				}
				else
				{
					this.theFileSeekList.Add(startOffset, endOffset);
					this.theFileSeekDescriptionList.Add(startOffset, description);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		public void Remove(long startOffset)
		{
			try
			{
				if (this.theFileSeekList.ContainsKey(startOffset))
				{
					this.theFileSeekList.Remove(startOffset);
				}
				if (this.theFileSeekDescriptionList.ContainsKey(startOffset))
				{
					this.theFileSeekDescriptionList.Remove(startOffset);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		public void Clear()
		{
			this.theFileSeekList.Clear();
			this.theFileSeekDescriptionList.Clear();
		}

		public long FileSize
		{
			get
			{
				return this.theFileSize;
			}
			set
			{
				if (this.theFileSize != value)
				{
					this.theFileSize = value;
					this.Add(this.theFileSize, this.theFileSize, "END OF FILE + 1 (File size)");
				}
			}
		}

		public void LogToEndAndAlignToNextStart(BinaryReader inputFileReader, long fileOffsetEnd, int byteAlignmentCount, string description, long expectedAlignOffsetEnd = -1)
		{
			long fileOffsetStart2 = 0;
			long fileOffsetEnd2 = 0;

			fileOffsetStart2 = fileOffsetEnd + 1;
			fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1;
			inputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin);
			if (fileOffsetEnd2 >= fileOffsetStart2)
			{
				bool allZeroesWereFound = false;
				if (expectedAlignOffsetEnd > -1 && expectedAlignOffsetEnd != fileOffsetEnd2)
				{
					description = "[ERROR: Should end at " + expectedAlignOffsetEnd.ToString() + "] " + description;
					description += " - " + fileOffsetStart2.ToString() + ":" + this.GetByteValues(inputFileReader, fileOffsetStart2, fileOffsetEnd2, ref allZeroesWereFound);
					description += " - " + (fileOffsetEnd2 + 1).ToString() + ":" + this.GetByteValues(inputFileReader, fileOffsetEnd2 + 1, expectedAlignOffsetEnd, ref allZeroesWereFound);
				}
				else
				{
					description += this.GetByteValues(inputFileReader, fileOffsetStart2, fileOffsetEnd2, ref allZeroesWereFound);
				}

				this.Add(fileOffsetStart2, fileOffsetEnd2, description);
			}
		}

		public void LogAndAlignFromFileSeekLogEnd(BinaryReader inputFileReader, int byteAlignmentCount, string description)
		{
			//Dim fileOffsetStart2 As Long
			//Dim fileOffsetEnd2 As Long

			//fileOffsetStart2 = Me.theFileSeekList.Values(Me.theFileSeekList.Count - 1) + 1
			//fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
			//inputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
			//If fileOffsetEnd2 >= fileOffsetStart2 Then
			//	Me.Add(fileOffsetStart2, fileOffsetEnd2, description)
			//End If
			//Me.LogToEndAndAlignToNextStart(inputFileReader, Me.theFileSeekList.Values(Me.theFileSeekList.Count - 1), byteAlignmentCount, description)
			//NOTE: The "- 2" skips the final value that should be the "END OF FILE + 1 (File size)".
			this.LogToEndAndAlignToNextStart(inputFileReader, this.theFileSeekList.Values[this.theFileSeekList.Count - 2], byteAlignmentCount, description);
		}

		public void LogUnreadBytes(BinaryReader inputFileReader)
		{
			long offsetStart = 0;
			long offsetEnd = 0;
			string description = null;
			string byteValues = null;
			bool allZeroesWereFound = false;
			SortedList<long, long> tempFileSeekList = new SortedList<long, long>();
			SortedList<long, string> tempFileSeekDescriptionList = new SortedList<long, string>();

			offsetStart = -1;
			offsetEnd = -1;
			try
			{
				for (int i = 0; i < this.theFileSeekList.Count; i++)
				{
					offsetStart = this.theFileSeekList.Keys[i];

					if (offsetEnd < offsetStart - 1)
					{
						description = "[ERROR] Unread bytes";
						byteValues = this.GetByteValues(inputFileReader, offsetEnd + 1, offsetStart - 1, ref allZeroesWereFound);
						if (allZeroesWereFound)
						{
							description += " (all zeroes)";
						}
						else
						{
							description += " (non-zero)";
						}
						description += byteValues;

						// Can't add into the list that is being iterated, so use temp list.
						if (this.theFileSeekList.ContainsKey(offsetEnd + 1))
						{
							string temp = this.theFileSeekDescriptionList[offsetEnd + 1];
							this.theFileSeekDescriptionList[offsetEnd + 1] = "[ERROR] ";
							this.theFileSeekDescriptionList[offsetEnd + 1] += temp + "; [" + (offsetEnd + 1).ToString() + " - " + (offsetStart - 1).ToString() + "] " + description;
						}
						else
						{
							tempFileSeekList.Add(offsetEnd + 1, offsetStart - 1);
							tempFileSeekDescriptionList.Add(offsetEnd + 1, description);
						}
					}

					offsetEnd = this.theFileSeekList.Values[i];
				}

				for (int i = 0; i < tempFileSeekList.Count; i++)
				{
					this.Add(tempFileSeekList.Keys[i], tempFileSeekList.Values[i], tempFileSeekDescriptionList.Values[i]);
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}

			this.LogErrors();
		}

		public long theFileSize;
		public SortedList<long, long> theFileSeekList;
		public SortedList<long, string> theFileSeekDescriptionList;

		private void LogErrors()
		{
			long offsetStart = 0;
			long offsetEnd = 0;

			try
			{
				for (int i = 0; i < this.theFileSeekList.Count; i++)
				{
					offsetStart = this.theFileSeekList.Keys[i];
					offsetEnd = this.theFileSeekList.Values[i];

					if ((i < this.theFileSeekList.Count - 1) && (offsetEnd + 1 != this.theFileSeekList.Keys[i + 1]))
					{
						this.theFileSeekDescriptionList[offsetStart] = "[ERROR] [End offset is incorrect] " + this.theFileSeekDescriptionList[offsetStart];
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
		}

		private string GetByteValues(BinaryReader inputFileReader, long fileOffsetStart2, long fileOffsetEnd2, ref bool allZeroesWereFound)
		{
			string byteValues = null;

			long inputFileStreamPosition = inputFileReader.BaseStream.Position;

			byte byteValue = 0;

			long adjustedFileOffsetEnd2 = 0;
			if ((fileOffsetEnd2 - fileOffsetStart2) > 20)
			{
				adjustedFileOffsetEnd2 = fileOffsetStart2 + 20;
			}
			else
			{
				adjustedFileOffsetEnd2 = fileOffsetEnd2;
			}

			try
			{
				inputFileReader.BaseStream.Seek(fileOffsetStart2, SeekOrigin.Begin);
				allZeroesWereFound = true;
				byteValues = " [";
				for (long byteOffset = fileOffsetStart2; byteOffset <= adjustedFileOffsetEnd2; byteOffset++)
				{
					byteValue = inputFileReader.ReadByte();
					byteValues += " " + byteValue.ToString("X2");
					if (byteValue != 0)
					{
						allZeroesWereFound = false;
					}
				}
				if ((fileOffsetEnd2 - fileOffsetStart2) > 20)
				{
					byteValues += " ...";
					//'NOTE: Indicate non-zeroes if more than 20 bytes unread because might be non-zeroes past the first 20.
					//allZeroesWereFound = False
					for (long byteOffset = adjustedFileOffsetEnd2 + 1; byteOffset <= fileOffsetEnd2; byteOffset++)
					{
						byteValue = inputFileReader.ReadByte();
						if (byteValue != 0)
						{
							allZeroesWereFound = false;
							break;
						}
					}
				}
				byteValues += " ]";

				inputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin);
			}
			catch (Exception ex)
			{
				allZeroesWereFound = false;
				byteValues = "[incomplete read due to error]";
			}

			return byteValues;
		}

	}

}