using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace Crowbar
{
	public class UnpackerOutputInfo
	{

		public AppEnums.StatusMessage theStatus;
		public BindingListEx<string> theUnpackedRelativePathFileNames;
		//Public unpackerAction As VpkAppAction

	}

}