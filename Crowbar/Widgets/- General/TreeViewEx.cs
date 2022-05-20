//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Crowbar
{
	public class TreeViewEx : TreeView
	{
		public TreeViewEx() : base()
		{
		}

		protected override void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			if (e.Node.Bounds.IsEmpty)
			{
				return;
			}

			e.DrawDefault = true;
			if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
			{
				if ((e.State & TreeNodeStates.Focused) == TreeNodeStates.Focused)
				{
					//e.Graphics.FillRectangle(Brushes.Red, e.Bounds)
					//TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.NodeFont, e.Bounds, e.Node.ForeColor, e.Node.BackColor, TextFormatFlags.GlyphOverhangPadding)
					//e.DrawDefault = False
				}
				else
				{
					e.Graphics.FillRectangle(SystemBrushes.ControlDark, e.Bounds);
					TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.NodeFont, e.Bounds, e.Node.ForeColor, e.Node.BackColor, TextFormatFlags.GlyphOverhangPadding);
					e.DrawDefault = false;
				}
			}

			base.OnDrawNode(e);
		}

	}

}