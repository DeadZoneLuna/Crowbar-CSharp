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
	public class ProgressBarEx : ProgressBar
	{
		//      Note that Paint() is not called unless UserPaint style is True.
		//      Also OnPaint() is not called unless Paint() is called, but then bar is not drawn.
		//      Overriding and then calling MyBase.OnPaint() does not draw bar.
		//      Conclusion: Must override OnPaint() to draw text and bar. 

		public ProgressBarEx() : base()
		{

			theText = "";
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}

		public override string Text
		{
			get
			{
				return theText;
			}
			set
			{
				if (theText != value)
				{
					theText = value;
				}
			}
		}

		public int Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				if (value < base.Minimum)
				{
					value = base.Minimum;
				}
				else if (value > base.Maximum)
				{
					value = base.Maximum;
				}
				base.Value = value;
				//NOTE: Do this so bar is re-painted when Value changes.
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int range = Maximum - Minimum;
			double percent = (double)(Value - Minimum) / (double)range;
			Rectangle rect = ClientRectangle;
			Rectangle bounds = e.ClipRectangle;
			if (rect.Width > 0 && percent > 0)
			{
				if (ProgressBarRenderer.IsSupported)
				{
					ProgressBarRenderer.DrawHorizontalBar(g, DisplayRectangle);
					rect.Inflate(-2, -2);
					rect.Width = Convert.ToInt32(rect.Width * percent);
					if (rect.Width == 0)
					{
						rect.Width = 1;
					}
					//NOTE: This always draws with Green color, so use other code to draw with widget's colors.
					//ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, rect)
					LinearGradientBrush gradientBrush = new LinearGradientBrush(rect, BackColor, ForeColor, LinearGradientMode.Vertical);
					e.Graphics.FillRectangle(gradientBrush, rect);
				}
				else
				{
					double barWidth = percent * bounds.Width;
					using (SolidBrush backBrush = new SolidBrush(BackColor))
					{
						g.FillRectangle(backBrush, bounds);
					}
					using (SolidBrush foreBrush = new SolidBrush(ForeColor))
					{
						g.FillRectangle(foreBrush, new RectangleF(0, 0, (float)barWidth, bounds.Height));
					}
					ControlPaint.DrawBorder(g, bounds, Color.Black, ButtonBorderStyle.Solid);
				}
			}
			else
			{
				if (ProgressBarRenderer.IsSupported)
				{
					ProgressBarRenderer.DrawHorizontalBar(g, DisplayRectangle);
				}
				else
				{
					ControlPaint.DrawBorder(g, bounds, Color.Black, ButtonBorderStyle.Solid);
				}
			}

			if (!string.IsNullOrEmpty(theText))
			{
				double x = 0;
				double y = 0;
				x = Width * 0.5 - (g.MeasureString(theText, Font).Width * 0.5);
				y = Height * 0.5 - (g.MeasureString(theText, Font).Height * 0.5);
				TextRenderer.DrawText(g, theText, Font, new Point(Convert.ToInt32(x), Convert.ToInt32(y)), ForeColor, BackColor);
			}

		}

		private string theText;

	}

}