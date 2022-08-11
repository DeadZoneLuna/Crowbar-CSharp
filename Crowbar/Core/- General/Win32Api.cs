using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Crowbar
{
	//TODO: Replace it with a cross-platform solution
	public class Win32Api
	{
		// Clipboard formats used for cut/copy/drag operations
		public const string CFSTR_PREFERREDDROPEFFECT = "Preferred DropEffect";
		public const string CFSTR_PERFORMEDDROPEFFECT = "Performed DropEffect";
		public const string CFSTR_FILEDESCRIPTORW = "FileGroupDescriptorW";
		public const string CFSTR_FILECONTENTS = "FileContents";

		// File Descriptor Flags
		public const Int32 FD_CLSID = 0x1;
		public const Int32 FD_SIZEPOINT = 0x2;
		public const Int32 FD_ATTRIBUTES = 0x4;
		public const Int32 FD_CREATETIME = 0x8;
		public const Int32 FD_ACCESSTIME = 0x10;
		public const Int32 FD_WRITESTIME = 0x20;
		public const Int32 FD_FILESIZE = 0x40;
		public const Int32 FD_PROGRESSUI = 0x4000;
		public const Int32 FD_LINKUI = 0x8000;

		// Global Memory Flags
		public const Int32 GMEM_MOVEABLE = 0x2;
		public const Int32 GMEM_ZEROINIT = 0x40;
		public const Int32 GHND = (GMEM_MOVEABLE | GMEM_ZEROINIT);
		public const Int32 GMEM_DDESHARE = 0x2000;

		// IDataObject constants
		public const Int32 DV_E_TYMED = unchecked((int)0x80040069);

		/// <summary>Windows messages (WM_*, look in winuser.h)</summary>
		public enum WindowsMessages
		{
			WM_ACTIVATE = 0x6,
			WM_COMMAND = 0x111,
			WM_ENTERIDLE = 0x121,
			WM_MOUSEWHEEL = 0x20A,
			WM_NOTIFY = 0x4E,
			WM_SHOWWINDOW = 0x18
			//HWND_BROADCAST = &HFFFF
		}

		public enum DialogChangeStatus : long
		{
			CDN_FIRST = 0xFFFFFDA7U,
			CDN_INITDONE = (CDN_FIRST - 0x0),
			CDN_SELCHANGE = (CDN_FIRST - 0x1),
			CDN_FOLDERCHANGE = (CDN_FIRST - 0x2),
			CDN_SHAREVIOLATION = (CDN_FIRST - 0x3),
			CDN_HELP = (CDN_FIRST - 0x4),
			CDN_FILEOK = (CDN_FIRST - 0x5),
			CDN_TYPECHANGE = (CDN_FIRST - 0x6)
		}

		public enum DialogChangeProperties
		{
			CDM_FIRST = (0x400 + 100),
			CDM_GETSPEC = (CDM_FIRST + 0x0),
			CDM_GETFILEPATH = (CDM_FIRST + 0x1),
			CDM_GETFOLDERPATH = (CDM_FIRST + 0x2),
			CDM_GETFOLDERIDLIST = (CDM_FIRST + 0x3),
			CDM_SETCONTROLTEXT = (CDM_FIRST + 0x4),
			CDM_HIDECONTROL = (CDM_FIRST + 0x5),
			CDM_SETDEFEXT = (CDM_FIRST + 0x6)
		}

		public enum ListViewMessages
		{
			LVM_FIRST = 0x1000,
			LVM_INSERTITEM = (LVM_FIRST + 77),
			LVM_DELETEALLITEMS = (LVM_FIRST + 9)
			//LVM_FINDITEM = (LVM_FIRST + 13)
			//LVM_SETCOLUMNWIDTH = (LVM_FIRST + 30)
			//LVM_GETITEMTEXT = (LVM_FIRST + 45)
			//LVM_SORTITEMS = (LVM_FIRST + 48)
			//LVSCW_AUTOSIZE_USEHEADER = -2
		}

		public enum ListViewEnums
		{
			LVIF_TEXT = 0x1,
			LVIF_IMAGE = 0x2,
			LVIF_PARAM = 0x4,
			LVIF_STATE = 0x8,
			LVIF_INDENT = 0x10,
			LVIF_GROUPID = 0x100,
			LVIF_COLUMNS = 0x200
		}

		//Public Enum SpecialFolderCSIDL As Integer
		//	CSIDL_DESKTOP = &H0
		//	' <desktop>
		//	CSIDL_INTERNET = &H1
		//	' Internet Explorer (icon on desktop)
		//	CSIDL_PROGRAMS = &H2
		//	' Start Menu\Programs
		//	CSIDL_CONTROLS = &H3
		//	' My Computer\Control Panel
		//	CSIDL_PRINTERS = &H4
		//	' My Computer\Printers
		//	CSIDL_PERSONAL = &H5
		//	' My Documents
		//	CSIDL_FAVORITES = &H6
		//	' <user name>\Favorites
		//	CSIDL_STARTUP = &H7
		//	' Start Menu\Programs\Startup
		//	CSIDL_RECENT = &H8
		//	' <user name>\Recent
		//	CSIDL_SENDTO = &H9
		//	' <user name>\SendTo
		//	CSIDL_BITBUCKET = &HA
		//	' <desktop>\Recycle Bin
		//	CSIDL_STARTMENU = &HB
		//	' <user name>\Start Menu
		//	CSIDL_DESKTOPDIRECTORY = &H10
		//	' <user name>\Desktop
		//	CSIDL_DRIVES = &H11
		//	' My Computer
		//	CSIDL_NETWORK = &H12
		//	' Network Neighborhood
		//	CSIDL_NETHOOD = &H13
		//	' <user name>\nethood
		//	CSIDL_FONTS = &H14
		//	' windows\fonts
		//	CSIDL_TEMPLATES = &H15
		//	CSIDL_COMMON_STARTMENU = &H16
		//	' All Users\Start Menu
		//	CSIDL_COMMON_PROGRAMS = &H17
		//	' All Users\Programs
		//	CSIDL_COMMON_STARTUP = &H18
		//	' All Users\Startup
		//	CSIDL_COMMON_DESKTOPDIRECTORY = &H19
		//	' All Users\Desktop
		//	CSIDL_APPDATA = &H1A
		//	' <user name>\Application Data
		//	CSIDL_PRINTHOOD = &H1B
		//	' <user name>\PrintHood
		//	CSIDL_LOCAL_APPDATA = &H1C
		//	' <user name>\Local Settings\Applicaiton Data (non roaming)
		//	CSIDL_ALTSTARTUP = &H1D
		//	' non localized startup
		//	CSIDL_COMMON_ALTSTARTUP = &H1E
		//	' non localized common startup
		//	CSIDL_COMMON_FAVORITES = &H1F
		//	CSIDL_INTERNET_CACHE = &H20
		//	CSIDL_COOKIES = &H21
		//	CSIDL_HISTORY = &H22
		//	CSIDL_COMMON_APPDATA = &H23
		//	' All Users\Application Data
		//	CSIDL_WINDOWS = &H24
		//	' GetWindowsDirectory()
		//	CSIDL_SYSTEM = &H25
		//	' GetSystemDirectory()
		//	CSIDL_PROGRAM_FILES = &H26
		//	' C:\Program Files
		//	CSIDL_MYPICTURES = &H27
		//	' C:\Program Files\My Pictures
		//	CSIDL_PROFILE = &H28
		//	' USERPROFILE
		//	CSIDL_SYSTEMX86 = &H29
		//	' x86 system directory on RISC
		//	CSIDL_PROGRAM_FILESX86 = &H2A
		//	' x86 C:\Program Files on RISC
		//	CSIDL_PROGRAM_FILES_COMMON = &H2B
		//	' C:\Program Files\Common
		//	CSIDL_PROGRAM_FILES_COMMONX86 = &H2C
		//	' x86 Program Files\Common on RISC
		//	CSIDL_COMMON_TEMPLATES = &H2D
		//	' All Users\Templates
		//	CSIDL_COMMON_DOCUMENTS = &H2E
		//	' All Users\Documents
		//	CSIDL_COMMON_ADMINTOOLS = &H2F
		//	' All Users\Start Menu\Programs\Administrative Tools
		//	CSIDL_ADMINTOOLS = &H30
		//	' <user name>\Start Menu\Programs\Administrative Tools
		//	CSIDL_CONNECTIONS = &H31
		//	' Network and Dial-up Connections
		//End Enum

		public const string Desktop = "::{00021400-0000-0000-C000-000000000046}";
		public const string MyComputer = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
		public const string NetworkPlaces = "::{208D2C60-3AEA-1069-A2D7-08002B30309D}";
		public const string Printers = "::{2227A280-3AEA-1069-A2DE-08002B30309D}";
		public const string RecycleBin = "::{645FF040-5081-101B-9F08-00AA002F954E}";
		public const string Tasks = "::{D6277990-4C6A-11CF-8D87-00AA0060F5BF}";

		[StructLayout(LayoutKind.Sequential)]
		public struct LV_ITEM
		{
			public int mask;
			public int iItem;
			public int iSubItem;
			public int state;
			public int stateMask;
			[MarshalAs(UnmanagedType.LPStr)]
			public string pszText;
			public int cchTextMax;
			public int iImage;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NMHDR
		{
			public IntPtr hwndFrom;
			public uint idFrom;
			public uint code;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct OFNOTIFY
		{
			public NMHDR hdr;
			public IntPtr OPENFILENAME;
			public IntPtr fileNameShareViolation;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			private int _Left;
			private int _Top;
			private int _Right;
			private int _Bottom;

			public RECT(Rectangle Rectangle) : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
			{
			}
			public RECT(int Left, int Top, int Right, int Bottom) : this()
			{
				_Left = Left;
				_Top = Top;
				_Right = Right;
				_Bottom = Bottom;
			}

			public int X
			{
				get
				{
					return _Left;
				}
				set
				{
					_Left = value;
				}
			}
			public int Y
			{
				get
				{
					return _Top;
				}
				set
				{
					_Top = value;
				}
			}
			public int Left
			{
				get
				{
					return _Left;
				}
				set
				{
					_Left = value;
				}
			}
			public int Top
			{
				get
				{
					return _Top;
				}
				set
				{
					_Top = value;
				}
			}
			public int Right
			{
				get
				{
					return _Right;
				}
				set
				{
					_Right = value;
				}
			}
			public int Bottom
			{
				get
				{
					return _Bottom;
				}
				set
				{
					_Bottom = value;
				}
			}
			public int Height
			{
				get
				{
					return _Bottom - _Top;
				}
				set
				{
					_Bottom = value - _Top;
				}
			}
			public int Width
			{
				get
				{
					return _Right - _Left;
				}
				set
				{
					_Right = value + _Left;
				}
			}
			public Point Location
			{
				get
				{
					return new Point(Left, Top);
				}
				set
				{
					_Left = value.X;
					_Top = value.Y;
				}
			}
			public Size Size
			{
				get
				{
					return new Size(Width, Height);
				}
				set
				{
					_Right = value.Width + _Left;
					_Bottom = value.Height + _Top;
				}
			}

			public static implicit operator Rectangle(RECT Rectangle)
			{
				return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
			}
			public static implicit operator RECT(Rectangle Rectangle)
			{
				return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
			}
			public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
			{
				return Rectangle1.Equals(Rectangle2);
			}
			public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
			{
				return !Rectangle1.Equals(Rectangle2);
			}

			public override string ToString()
			{
				return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
			}

			new public bool Equals(RECT Rectangle)
			{
				return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
			}
			public override bool Equals(object Object)
			{
				if (Object is RECT)
					return Equals((RECT)Object);
				else if (Object is Rectangle)
					return Equals(new RECT((Rectangle)Object));

				return false;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WINDOWINFO
		{
			public int cbSize;
			public RECT rcWindow;
			public RECT rcClient;
			public int dwStyle;
			public int dwExStyle;
			public UInt32 dwWindowStatus;
			public UInt32 cxWindowBorders;
			public UInt32 cyWindowBorders;
			public UInt16 atomWindowType;
			public short wCreatorVersion;
		}

		public delegate bool EnumWindowsProc(IntPtr Handle, IntPtr Parameter);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public extern static bool CloseHandle(IntPtr hObject);

		[DllImport("shell32.dll")]
		public extern static void SHChangeNotify(int wEventId, int uFlags, int dwItem1, int dwItem2);

		[DllImport("shell32.dll")]
		private extern static Int32 SHGetFolderPath(IntPtr hwndOwner, Int32 nFolder, IntPtr hToken, Int32 dwFlags, StringBuilder pszPath);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public extern static bool EnumChildWindows(System.IntPtr hWndParent, EnumWindowsProc lpEnumFunc, int lParam);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public extern static void GetClassName(System.IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

		[DllImport("user32.dll")]
		public extern static int GetDlgCtrlID(System.IntPtr hwndCtl);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public extern static IntPtr GetParent(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public extern static bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

		[DllImport("user32.dll", SetLastError = true)]
		public extern static int GetWindowThreadProcessId(IntPtr hwnd, ref IntPtr lpdwProcessId);

		/// <summary>Send message to a window (platform invoke)</summary>
		/// <param name="hWnd">Window handle to send to</param>
		/// <param name="msg">Message</param>
		/// <param name="wParam">wParam</param>
		/// <param name="lParam">lParam</param>
		/// <returns>Zero if failure, otherwise non-zero</returns>
		[DllImport("user32.dll", SetLastError = true)]
		public extern static bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public extern static IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public extern static IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, LV_ITEM lParam);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public extern static IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, System.Text.StringBuilder lParam);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public extern static IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public extern static bool CreateHardLink(string lpNewFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

		public enum SymbolicLink
		{
			File = 0,
			Directory = 1
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		public extern static bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

		private const int MAX_PATH = 260;
		private const int NAMESIZE = 80;
		private const Int32 SHGFI_LARGEICON = 0x0;
		private const Int32 SHGFI_SMALLICON = 0x1;
		private const Int32 SHGFI_USEFILEATTRIBUTES = 0x10;
		private const Int32 SHGFI_ICON = 0x100;
		public const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
		//Public Const CFSTR_FILEDESCRIPTORW As String = "FileGroupDescriptorW"
		//Public Const CFSTR_PREFERREDDROPEFFECT As String = "Preferred DropEffect"
		//Public Const CFSTR_PERFORMEDDROPEFFECT As String = "Performed DropEffect"
		//Public Const FD_PROGRESSUI As Int32 = &H4000

		[StructLayout(LayoutKind.Sequential)]
		private struct SHFILEINFO
		{
			public IntPtr hIcon;
			public int iIcon;
			public int dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAMESIZE)]
			public string szTypeName;
		}

		[DllImport("Shell32.dll")]
		private extern static IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, int uFlags);

		[DllImport("user32.dll", SetLastError = true)]
		private extern static bool DestroyIcon(IntPtr hIcon);

		public static Bitmap GetShellIcon(string path, int fileAttributes = 0)
		{
			Bitmap bmp = null;
			IntPtr ret = System.IntPtr.Zero;
			SHFILEINFO shfi = new SHFILEINFO();

			bmp = null;
			shfi = new SHFILEINFO();
			ret = SHGetFileInfo(path, fileAttributes, ref shfi, Marshal.SizeOf(shfi), SHGFI_USEFILEATTRIBUTES | SHGFI_ICON);
			if (ret != IntPtr.Zero)
			{
				bmp = System.Drawing.Icon.FromHandle(shfi.hIcon).ToBitmap();
				DestroyIcon(shfi.hIcon);
			}

			return bmp;
		}

		//<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
		//Public Shared Function RegisterWindowMessage(ByVal lpString As String) As UInteger
		//End Function

		//Public Shared Function GetSpecialFolderPath(ByVal folderCSIDL As SpecialFolderCSIDL) As String
		//	Dim winPath As New StringBuilder(300)
		//	If SHGetFolderPath(Nothing, folderCSIDL, Nothing, 0, winPath) <> 0 Then
		//		'Throw New ApplicationException("Can't get window's directory")
		//		Return ""
		//	End If
		//	Return winPath.ToString()
		//End Function

		//Public Shared Function GetContentType(ByVal extension As String) As String
		//	Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension)
		//	If Not regKey Is Nothing Then
		//		Dim ct As Object = regKey.GetValue("Content Type")
		//		If Not ct Is Nothing Then
		//			Return ct.ToString()
		//		End If
		//	End If
		//	Return ""
		//End Function

		public static string GetFileTypeDescription(string extension)
		{
			Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);
			if (regKey != null)
			{
				object extensionDefaultValue = regKey.GetValue("");
				if (extensionDefaultValue != null)
				{
					string classname = extensionDefaultValue.ToString();
					Microsoft.Win32.RegistryKey classnameKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(classname);
					if (classnameKey != null)
					{
						object ct = classnameKey.GetValue("");
						if (ct != null)
						{
							return ct.ToString();
						}
					}
				}
			}
			return "";
		}

		public static bool CreateFileAssociation(string extension, string className, string description, string exeProgram)
		{
			const int SHCNE_ASSOCCHANGED = 0x8000000;
			const int SHCNF_IDLIST = 0;

			// ensure that there is a leading dot
			if (extension.Substring(0, 1) != ".")
			{
				extension = "." + extension;
			}

			Microsoft.Win32.RegistryKey currentUser = Microsoft.Win32.Registry.CurrentUser;
			Microsoft.Win32.RegistryKey classesKey = null;
			Microsoft.Win32.RegistryKey extensionKey = null;
			Microsoft.Win32.RegistryKey classnameKey = null;
			Microsoft.Win32.RegistryKey defaultIconKey = null;
			Microsoft.Win32.RegistryKey shellKey = null;
			Microsoft.Win32.RegistryKey shellOpenCommandKey = null;
			try
			{
				Win32Api.DeleteFileAssociation(extension, className, description, "");

				classesKey = currentUser.OpenSubKey("Software\\Classes", true);

				extensionKey = classesKey.CreateSubKey(extension);
				extensionKey.SetValue("", className);

				classnameKey = classesKey.CreateSubKey(className);
				classnameKey.SetValue("", description);

				defaultIconKey = classesKey.CreateSubKey(className + "\\DefaultIcon");
				defaultIconKey.SetValue("", exeProgram + ",0");

				shellKey = classesKey.CreateSubKey(className + "\\Shell");
				shellKey.SetValue("", "Open");

				shellOpenCommandKey = classesKey.CreateSubKey(className + "\\Shell\\Open\\Command");
				shellOpenCommandKey.SetValue("", exeProgram + " \"%1\"");
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				if (classesKey != null)
				{
					classesKey.Close();
				}
				if (extensionKey != null)
				{
					extensionKey.Close();
				}
				if (classnameKey != null)
				{
					classnameKey.Close();
				}
				if (shellOpenCommandKey != null)
				{
					shellOpenCommandKey.Close();
				}
			}

			// notify Windows that file associations have changed
			SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0);

			return true;
		}

		public static bool FileAssociationIsAlreadyAssigned(string extension, string className, string description, string exeProgram)
		{
			// ensure that there is a leading dot
			if (extension.Substring(0, 1) != ".")
			{
				extension = "." + extension;
			}

			Microsoft.Win32.RegistryKey currentUser = Microsoft.Win32.Registry.CurrentUser;
			Microsoft.Win32.RegistryKey classesKey = null;
			Microsoft.Win32.RegistryKey shellKey = null;
			Microsoft.Win32.RegistryKey shellOpenCommandKey = null;
			try
			{
				classesKey = currentUser.OpenSubKey("Software\\Classes", true);

				//shellKey = classesKey.OpenSubKey(className + "\Shell")
				//If shellKey IsNot Nothing Then
				//	If shellKey.GetValueKind("") = Microsoft.Win32.RegistryValueKind.String Then
				//		Dim keyValueString3 As String = CType(shellKey.GetValue(""), String)
				//		If keyValueString3 = "Open" Then
				//			Return True
				//		End If
				//	End If
				//End If

				shellOpenCommandKey = classesKey.OpenSubKey(className + "\\Shell\\Open\\Command");
				if (shellOpenCommandKey != null)
				{
					if (shellOpenCommandKey.GetValueKind("") == Microsoft.Win32.RegistryValueKind.String)
					{
						string keyValueString3 = (shellOpenCommandKey.GetValue("") == null ? null : Convert.ToString(shellOpenCommandKey.GetValue("")));
						if (keyValueString3 == (exeProgram + " \"%1\""))
						{
							return true;
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
				if (classesKey != null)
				{
					classesKey.Close();
				}
				//If Not key1 Is Nothing Then key1.Close()
				//If Not key2 Is Nothing Then key2.Close()
				if (shellOpenCommandKey != null)
				{
					shellOpenCommandKey.Close();
				}
			}

			return false;
		}

		public static bool DeleteFileAssociation(string extension, string className, string description, string exeProgram)
		{
			const int SHCNE_ASSOCCHANGED = 0x8000000;
			const int SHCNF_IDLIST = 0;

			// ensure that there is a leading dot
			if (extension.Substring(0, 1) != ".")
			{
				extension = "." + extension;
			}

			Microsoft.Win32.RegistryKey currentUser = Microsoft.Win32.Registry.CurrentUser;
			Microsoft.Win32.RegistryKey classesKey = null;
			Microsoft.Win32.RegistryKey shellOpenCommandKey = null;
			try
			{
				classesKey = currentUser.OpenSubKey("Software\\Classes", true);
				shellOpenCommandKey = classesKey.OpenSubKey(className + "\\Shell\\Open\\Command");
				if (shellOpenCommandKey != null)
				{
					if (shellOpenCommandKey.GetValueKind("") == Microsoft.Win32.RegistryValueKind.String)
					{
						string keyValueString3 = (shellOpenCommandKey.GetValue("") == null ? null : Convert.ToString(shellOpenCommandKey.GetValue("")));
						if (string.IsNullOrEmpty(exeProgram) || keyValueString3 == (exeProgram + " \"%1\""))
						{
							classesKey.DeleteSubKey(className + "\\Shell\\Open\\Command", false);
							classesKey.DeleteSubKey(className + "\\Shell\\Open", false);
							classesKey.DeleteSubKey(className + "\\Shell", false);
							classesKey.DeleteSubKey(className + "\\DefaultIcon", false);
							classesKey.DeleteSubKey(className, false);
							classesKey.DeleteSubKey(extension, false);
						}
					}
				}
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				if (classesKey != null)
				{
					classesKey.Close();
				}
				//If Not key1 Is Nothing Then key1.Close()
				//If Not key2 Is Nothing Then key2.Close()
				if (shellOpenCommandKey != null)
				{
					shellOpenCommandKey.Close();
				}
			}

			// notify Windows that file associations have changed
			SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0);

			return true;
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public extern static IntPtr GlobalAlloc(int uFlags, int dwBytes);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public extern static IntPtr GlobalFree(HandleRef handle);
	}
}