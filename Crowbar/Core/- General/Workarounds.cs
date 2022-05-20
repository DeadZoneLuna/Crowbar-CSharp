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
	internal static class Workarounds
	{

		// Call this in Form.Load() or UserControl.Load().
		// Input is the sibling control to the immediate right or the parent control of this control.
		public static void WorkaroundForFrameworkAnchorRightSizingBug(Control anchoredWidget, Control siblingOrParentWidget, bool widgetIsParent = false)
		{
			if (widgetIsParent)
			{
				anchoredWidget.Size = new System.Drawing.Size(siblingOrParentWidget.Width - siblingOrParentWidget.Padding.Right - anchoredWidget.Margin.Right - anchoredWidget.Left, anchoredWidget.Height);
			}
			else
			{
				anchoredWidget.Size = new System.Drawing.Size(siblingOrParentWidget.Left - siblingOrParentWidget.Margin.Left - anchoredWidget.Margin.Right - anchoredWidget.Left, anchoredWidget.Height);
			}
		}

	}

}