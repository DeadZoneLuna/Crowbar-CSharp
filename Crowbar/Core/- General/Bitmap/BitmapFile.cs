using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	public class BitmapFile
	{
		#region Data
		//Protected theInputFileReader As BinaryReader
		//Protected theOutputFileWriter As BinaryWriter

		public string thePathFileName;
		public uint theWidth;
		public uint theHeight;
		public List<byte> theData;
		#endregion

		#region Creation and Destruction
		//Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData10)
		//	Me.theInputFileReader = mdlFileReader
		//	Me.theMdlFileData = mdlFileData
		//End Sub

		public BitmapFile(string bmpPathFileName, uint width, uint height, List<byte> data)
		{
			thePathFileName = bmpPathFileName;
			theWidth = width;
			theHeight = height;
			theData = data;
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
				outputFileStream = new FileStream(thePathFileName, FileMode.OpenOrCreate);
				if (outputFileStream != null)
				{
					try
					{
						outputFileWriter = new BinaryWriter(outputFileStream, System.Text.Encoding.ASCII);

						//	biTrueWidth = ((width + 3) & ~3);
						//	cbBmpBits = biTrueWidth * height;
						//	cbPalBytes = 256 * sizeof( RGBQUAD );
						//paddedWidthUsedInFile = CUInt(MathModule.AlignLong(Me.theWidth, 3))
						//NOTE: Align to 4 byte boundary.
						uint alignedWidthUsedInFile = (uint)MathModule.AlignLong(theWidth, 4);
						uint fileHeaderSize = 14;
						uint infoHeaderSize = 40;
						// 256 * size of BitmapRgbQuad = 256 * 4 = 1024
						uint paletteSize = 1024;
						uint dataSize = alignedWidthUsedInFile * theHeight;

						// Write file header
						outputFileWriter.Write('B');
						outputFileWriter.Write('M');
						outputFileWriter.Write(fileHeaderSize + infoHeaderSize + paletteSize + dataSize);
						outputFileWriter.Write((ushort)0);
						outputFileWriter.Write((ushort)0);
						outputFileWriter.Write(fileHeaderSize + infoHeaderSize + paletteSize);

						// Write info header
						outputFileWriter.Write(infoHeaderSize);
						outputFileWriter.Write(alignedWidthUsedInFile);
						outputFileWriter.Write(theHeight);
						outputFileWriter.Write((ushort)1);
						outputFileWriter.Write((ushort)8);
						outputFileWriter.Write((uint)0);
						outputFileWriter.Write((uint)0);
						outputFileWriter.Write((uint)0);
						outputFileWriter.Write((uint)0);
						outputFileWriter.Write((uint)256);
						outputFileWriter.Write((uint)0);

						// Write palette (bmih.biClrUsed entries)
						for (int dataIndex = theData.Count - 768; dataIndex < theData.Count; dataIndex += 3)
						{
							outputFileWriter.Write(theData[dataIndex + 2]);
							outputFileWriter.Write(theData[dataIndex + 1]);
							outputFileWriter.Write(theData[dataIndex]);
							outputFileWriter.Write((byte)0);
						}

						// Write bitmap bits (remainder of file)
						// Write the rows in reverse order.
						int startOfLastRowOffset = (int)(theData.Count - 768 - theWidth);
						int dataStoredIndex = startOfLastRowOffset;
						uint tempVar = theHeight - 1U;
						for (uint rowIndex = 0; rowIndex <= tempVar; rowIndex++)
						{
							int tempVar2 = (int)(dataStoredIndex + theWidth - 1);
							for (int dataIndex = dataStoredIndex; dataIndex <= tempVar2; dataIndex++)
								outputFileWriter.Write(theData[dataIndex]);
							int tempVar3 = (int)(dataStoredIndex + alignedWidthUsedInFile - 1);
							for (int paddingIndex = (int)(dataStoredIndex + theWidth); paddingIndex <= tempVar3; paddingIndex++)
								outputFileWriter.Write((byte)0);
							dataStoredIndex -= (int)theWidth;
						}
					}
					catch (Exception ex)
					{
						int debug = 4242;
					}
					finally
					{
						if (outputFileWriter != null)
							outputFileWriter.Close();
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
					outputFileStream.Close();
			}
		}
		#endregion

		#region Private Methods
		#endregion
	}
}