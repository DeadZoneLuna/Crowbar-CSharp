//FROM: How to: Stretch a ToolStripTextBox to Fill the Remaining Width of a ToolStrip (Windows Forms)
//      https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/stretch-a-toolstriptextbox-to-fill-the-remaining-width-of-a-toolstrip-wf
//      05-Aug-2017

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
	public class ToolStripSpringTextBox : ToolStripTextBox
	{
		//IMPORTANT: The control must have AutoSize = True for this to be called.
		public override Size GetPreferredSize(Size constrainingSize)
		{

			// Use the default size if the text box is on the overflow menu
			// or is on a vertical ToolStrip.
			if (IsOnOverflow || Owner.Orientation == Orientation.Vertical)
			{
				return DefaultSize;
			}

			// Declare a variable to store the total available width as 
			// it is calculated, starting with the display width of the 
			// owning ToolStrip.
			Int32 width = Owner.DisplayRectangle.Width;

			// Subtract the width of the overflow button if it is displayed. 
			if (Owner.OverflowButton.Visible)
			{
				width = width - Owner.OverflowButton.Width - Owner.OverflowButton.Margin.Horizontal;
			}

			// Declare a variable to maintain a count of ToolStripSpringTextBox 
			// items currently displayed in the owning ToolStrip. 
			Int32 springBoxCount = 0;

			foreach (ToolStripItem item in Owner.Items)
			{

				// Ignore items on the overflow menu.
				if (item.IsOnOverflow)
				{
					continue;
				}

				if (item is ToolStripSpringTextBox)
				{
					// For ToolStripSpringTextBox items, increment the count and 
					// subtract the margin width from the total available width.
					springBoxCount += 1;
					width -= item.Margin.Horizontal;
				}
				else
				{
					// For all other items, subtract the full width from the total
					// available width.
					width = width - item.Width - item.Margin.Horizontal;
				}
			}

			// If there are multiple ToolStripSpringTextBox items in the owning
			// ToolStrip, divide the total available width between them. 
			if (springBoxCount > 1)
			{
				width = Convert.ToInt32(width / (double)springBoxCount);
			}

			// If the available width is less than the default width, use the
			// default width, forcing one or more items onto the overflow menu.
			if (width < DefaultSize.Width)
			{
				width = DefaultSize.Width;
			}

			// Retrieve the preferred size from the base class, but change the
			// width to the calculated width. 
			Size preferredSize = base.GetPreferredSize(constrainingSize);
			preferredSize.Width = width;
			return preferredSize;

		}

	}

}