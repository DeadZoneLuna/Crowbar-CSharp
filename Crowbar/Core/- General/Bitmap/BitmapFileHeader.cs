namespace Crowbar
{
	public class BitmapFileHeader
	{
		//FROM: c:\Program Files (x86)\Windows Kits\8.1\Include\um\wingdi.h
		//typedef struct tagBITMAPFILEHEADER {
		//        WORD    bfType;
		//        DWORD   bfSize;
		//        WORD    bfReserved1;
		//        WORD    bfReserved2;
		//        DWORD   bfOffBits;
		//} BITMAPFILEHEADER, FAR *LPBITMAPFILEHEADER, *PBITMAPFILEHEADER;
		public ushort type;
		public uint size;
		public ushort reserved1;
		public ushort reserved2;
		public uint dataOffset;
	}

}