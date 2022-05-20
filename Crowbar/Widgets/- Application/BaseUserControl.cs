//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public partial class BaseUserControl
	{

		public BaseUserControl()
		{

			//TEST: See if this prevents the overlapping or larger text on Chinese Windows.
			// This should allow Forms that inherit from this class and their widgets to use the system font instead of Visual Studio's default of Microsoft Sans Serif.
			this.Font = new Font(SystemFonts.MessageBoxFont.Name, 8.25f);

			// This call is required by the designer.
			InitializeComponent();
		}

	}

}