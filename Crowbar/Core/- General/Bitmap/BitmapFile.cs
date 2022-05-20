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
	public class BitmapFile
	{

#region Creation and Destruction

		//Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData10)
		//	Me.theInputFileReader = mdlFileReader
		//	Me.theMdlFileData = mdlFileData
		//End Sub

		public BitmapFile(string bmpPathFileName, UInt32 width, UInt32 height, List<byte> data)
		{
			this.thePathFileName = bmpPathFileName;
			this.theWidth = width;
			this.theHeight = height;
			this.theData = data;
		}

#endregion

#region Methods

		//int WriteBMPfile (char *szFile, byte *pbBits, int width, int height, byte *pbPalette)
		//{
		//	int i, rc = 0;
		//	FILE *pfile = NULL;
		//	BITMAPFILEHEADER bmfh;
		//	BITMAPINFOHEADER bmih;
		//	RGBQUAD rgrgbPalette[256];
		//	ULONG cbBmpBits;
		//	BYTE* pbBmpBits;
		//	byte  *pb, *pbPal = NULL;
		//	ULONG cbPalBytes;
		//	ULONG biTrueWidth;
		//
		//	// Bogus parameter check
		//	if (!(pbPalette != NULL && pbBits != NULL))
		//		{ rc = -1000; goto GetOut; }
		//
		//	// File exists?
		//	if ((pfile = fopen(szFile, "wb")) == NULL)
		//		{ rc = -1; goto GetOut; }
		//
		//	biTrueWidth = ((width + 3) & ~3);
		//	cbBmpBits = biTrueWidth * height;
		//	cbPalBytes = 256 * sizeof( RGBQUAD );
		//
		//	// Bogus file header check
		//	bmfh.bfType = MAKEWORD( 'B', 'M' );
		//	bmfh.bfSize = sizeof bmfh + sizeof bmih + cbBmpBits + cbPalBytes;
		//	bmfh.bfReserved1 = 0;
		//	bmfh.bfReserved2 = 0;
		//	bmfh.bfOffBits = sizeof bmfh + sizeof bmih + cbPalBytes;
		//
		//	// Write file header
		//	if (fwrite(&bmfh, sizeof bmfh, 1/*count*/, pfile) != 1)
		//		{ rc = -2; goto GetOut; }
		//
		//	// Size of structure
		//	bmih.biSize = sizeof bmih;
		//	// Width
		//	bmih.biWidth = biTrueWidth;
		//	// Height
		//	bmih.biHeight = height;
		//	// Only 1 plane 
		//	bmih.biPlanes = 1;
		//	// Only 8-bit supported.
		//	bmih.biBitCount = 8;
		//	// Only non-compressed supported.
		//	bmih.biCompression = BI_RGB;
		//	bmih.biSizeImage = 0;
		//
		//	// huh?
		//	bmih.biXPelsPerMeter = 0;
		//	bmih.biYPelsPerMeter = 0;
		//
		//	// Always full palette
		//	bmih.biClrUsed = 256;
		//	bmih.biClrImportant = 0;
		//	
		//	// Write info header
		//	if (fwrite(&bmih, sizeof bmih, 1/*count*/, pfile) != 1)
		//		{ rc = -3; goto GetOut; }
		//	
		//
		//	// convert to expanded palette
		//	pb = pbPalette;
		//
		//	// Copy over used entries
		//	for (i = 0; i < (int)bmih.biClrUsed; i++)
		//	{
		//		rgrgbPalette[i].rgbRed   = *pb++;
		//		rgrgbPalette[i].rgbGreen = *pb++;
		//		rgrgbPalette[i].rgbBlue  = *pb++;
		//        rgrgbPalette[i].rgbReserved = 0;
		//	}
		//
		//	// Write palette (bmih.biClrUsed entries)
		//	cbPalBytes = bmih.biClrUsed * sizeof( RGBQUAD );
		//	if (fwrite(rgrgbPalette, cbPalBytes, 1/*count*/, pfile) != 1)
		//		{ rc = -6; goto GetOut; }
		//
		//
		//	pbBmpBits = malloc(cbBmpBits);
		//
		//	pb = pbBits;
		//	// reverse the order of the data.
		//	pb += (height - 1) * width;
		//	for(i = 0; i < bmih.biHeight; i++)
		//	{
		//		memmove(&pbBmpBits[biTrueWidth * i], pb, width);
		//		pb -= width;
		//	}
		//
		//	// Write bitmap bits (remainder of file)
		//	if (fwrite(pbBmpBits, cbBmpBits, 1/*count*/, pfile) != 1)
		//		{ rc = -7; goto GetOut; }
		//
		//	free(pbBmpBits);
		//
		//GetOut:
		//	if (pfile) 
		//		fclose(pfile);
		//
		//	return rc;
		//}
		public void Write()
		{
			FileStream outputFileStream = null;
			BinaryWriter outputFileWriter = null;
			try
			{
				outputFileStream = new FileStream(this.thePathFileName, FileMode.OpenOrCreate);
				if (outputFileStream != null)
				{
					try
					{
						outputFileWriter = new BinaryWriter(outputFileStream, System.Text.Encoding.ASCII);

						//	biTrueWidth = ((width + 3) & ~3);
						//	cbBmpBits = biTrueWidth * height;
						//	cbPalBytes = 256 * sizeof( RGBQUAD );
						UInt32 alignedWidthUsedInFile = 0;
						UInt32 fileHeaderSize = 0;
						UInt32 infoHeaderSize = 0;
						UInt32 paletteSize = 0;
						UInt32 dataSize = 0;
						//paddedWidthUsedInFile = CUInt(MathModule.AlignLong(Me.theWidth, 3))
						//NOTE: Align to 4 byte boundary.
						alignedWidthUsedInFile = (uint)MathModule.AlignLong(this.theWidth, 4);
						fileHeaderSize = 14;
						infoHeaderSize = 40;
						// 256 * size of BitmapRgbQuad = 256 * 4 = 1024
						paletteSize = 1024;
						dataSize = alignedWidthUsedInFile * this.theHeight;

						//	// Write file header
						outputFileWriter.Write('B');
						outputFileWriter.Write('M');
						outputFileWriter.Write(fileHeaderSize + infoHeaderSize + paletteSize + dataSize);
						outputFileWriter.Write((ushort)0);
						outputFileWriter.Write((ushort)0);
						outputFileWriter.Write(fileHeaderSize + infoHeaderSize + paletteSize);

						//	// Write info header
						outputFileWriter.Write(infoHeaderSize);
						outputFileWriter.Write(alignedWidthUsedInFile);
						outputFileWriter.Write(this.theHeight);
						outputFileWriter.Write((ushort)1);
						outputFileWriter.Write((ushort)8);
						outputFileWriter.Write((uint)0);
						outputFileWriter.Write((uint)0);
						outputFileWriter.Write((uint)0);
						outputFileWriter.Write((uint)0);
						outputFileWriter.Write((uint)256);
						outputFileWriter.Write((uint)0);

						//	// Write palette (bmih.biClrUsed entries)
						for (int dataIndex = this.theData.Count - 768; dataIndex < this.theData.Count; dataIndex += 3)
						{
							outputFileWriter.Write(this.theData[dataIndex + 2]);
							outputFileWriter.Write(this.theData[dataIndex + 1]);
							outputFileWriter.Write(this.theData[dataIndex]);
							outputFileWriter.Write((byte)0);
						}

						//	// Write bitmap bits (remainder of file)
						// Write the rows in reverse order.
						int startOfLastRowOffset = (int)(this.theData.Count - 768 - this.theWidth);
						//For dataStoredIndex As Integer = startOfLastRowOffset To 0 Step CInt(-Me.theWidth)
						int dataStoredIndex = startOfLastRowOffset;
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of this.theHeight - 1UI for every iteration:
						uint tempVar = this.theHeight - 1U;
						for (uint rowIndex = 0; rowIndex <= tempVar; rowIndex++)
						{
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of (int)(dataStoredIndex + this.theWidth - 1) for every iteration:
							int tempVar2 = (int)(dataStoredIndex + this.theWidth - 1);
							for (int dataIndex = dataStoredIndex; dataIndex <= tempVar2; dataIndex++)
							{
								outputFileWriter.Write(this.theData[dataIndex]);
							}
//INSTANT C# NOTE: The ending condition of VB 'For' loops is tested only on entry to the loop. Instant C# has created a temporary variable in order to use the initial value of (int)(dataStoredIndex + alignedWidthUsedInFile - 1) for every iteration:
							int tempVar3 = (int)(dataStoredIndex + alignedWidthUsedInFile - 1);
							for (int paddingIndex = (int)(dataStoredIndex + this.theWidth); paddingIndex <= tempVar3; paddingIndex++)
							{
								outputFileWriter.Write((byte)0);
							}
							dataStoredIndex -= (int)this.theWidth;
						}
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
					finally
					{
						if (outputFileWriter != null)
						{
							outputFileWriter.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				int debug = 4242;
			}
			finally
			{
				if (outputFileStream != null)
				{
					outputFileStream.Close();
				}
			}
		}

#endregion

#region Private Methods

#endregion

#region Data

		//Protected theInputFileReader As BinaryReader
		//Protected theOutputFileWriter As BinaryWriter

		public string thePathFileName;
		public UInt32 theWidth;
		public UInt32 theHeight;
		public List<byte> theData;

#endregion


	}

}