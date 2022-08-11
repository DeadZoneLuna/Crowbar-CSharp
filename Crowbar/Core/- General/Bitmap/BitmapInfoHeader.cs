namespace Crowbar
{
	public class BitmapInfoHeader
	{
		//FROM: c:\Program Files (x86)\Windows Kits\8.1\Include\um\wingdi.h
		//typedef struct tagBITMAPINFOHEADER{
		//        DWORD      biSize;
		//        LONG       biWidth;
		//        LONG       biHeight;
		//        WORD       biPlanes;
		//        WORD       biBitCount;
		//        DWORD      biCompression;
		//        DWORD      biSizeImage;
		//        LONG       biXPelsPerMeter;
		//        LONG       biYPelsPerMeter;
		//        DWORD      biClrUsed;
		//        DWORD      biClrImportant;
		//} BITMAPINFOHEADER, FAR *LPBITMAPINFOHEADER, *PBITMAPINFOHEADER;

		public uint size;
		public int width;
		public int height;
		public ushort planes;
		public ushort bitCount;
		public uint compression;
		public uint sizeImage;
		public int xPelsPerMeter;
		public int yPelsPerMeter;
		public uint clrUsed;
		public uint clrImportant;
	}
}