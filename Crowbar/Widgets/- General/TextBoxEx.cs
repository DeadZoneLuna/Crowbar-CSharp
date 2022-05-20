//INSTANT C# NOTE: Formerly VB project-level imports:
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
	public class TextBoxEx : TextBox
	{
		public TextBoxEx() : base()
		{

			this.theCueBannerText = "";
		}

		[Browsable(true)]
		[Category("Appearance")]
		[Description("Sets the text of the cue (dimmed text that only shows when Text property is empty).")]
		public string CueBannerText
		{
			get
			{
				return this.theCueBannerText;
			}
			set
			{
				this.theCueBannerText = value;
			}
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				this.SelectAll();
			}
			base.OnKeyDown(e);
		}

		protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
			if (!this.Multiline && e.KeyChar == (char)Keys.Return)
			{
				try
				{
					// Cause validation, which means Validating and Validated events are raised.
					this.FindForm().Validate();
					if (this.Parent is ContainerControl)
					{
						((ContainerControl)this.Parent).Validate();
					}
					//NOTE: Prevent annoying beep when textbox is single line.
					e.Handled = true;
				}
				catch (Exception ex)
				{
					int debug = 4242;
				}
			}
			base.OnKeyPress(e);
		}

		//Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
		//	'MyBase.OnPaintBackground(e)
		//	Using sb As New SolidBrush(Color.Red)
		//		e.Graphics.FillRectangle(sb, Me.ClientRectangle)
		//		sb.Dispose()
		//	End Using
		//End Sub

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (!string.IsNullOrEmpty(this.theCueBannerText) && string.IsNullOrEmpty(this.Text) && this.theOriginalFont != null)
			{
				//Using sb As New SolidBrush(SystemColors.Control)
				//	e.Graphics.FillRectangle(sb, Me.ClientRectangle)
				//	sb.Dispose()
				//End Using

				//Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
				System.Drawing.Font drawFont = new System.Drawing.Font(this.theOriginalFont.FontFamily, this.theOriginalFont.Size, FontStyle.Italic, this.theOriginalFont.Unit);

				Color drawForeColor = SystemColors.GrayText;
				Color drawBackColor = SystemColors.Control;
				if (drawForeColor == drawBackColor)
				{
					drawForeColor = this.ForeColor;
					drawBackColor = this.BackColor;
				}
				TextRenderer.DrawText(e.Graphics, this.theCueBannerText, drawFont, new Point(1, 0), drawForeColor, drawBackColor);
				//======
				//' Draw higlight.
				//Dim higlightForeColor As Color = SystemColors.ControlLightLight
				//'Dim higlightBackColor As Color = SystemColors.Control
				//'If higlightForeColor = higlightBackColor Then
				//'	higlightForeColor = Me.ForeColor
				//'	higlightBackColor = Me.BackColor
				//'End If
				//'TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 1), higlightForeColor, higlightBackColor)
				//TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(1, 1), higlightForeColor)
				//' Draw shadow.
				//Dim shadowForeColor As Color = SystemColors.ControlDark
				//'Dim shadowBackColor As Color = SystemColors.Control
				//'If shadowForeColor = shadowBackColor Then
				//'	shadowForeColor = Me.ForeColor
				//'	shadowBackColor = Me.BackColor
				//'End If
				//'TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(-1, -1), shadowForeColor, shadowBackColor)
				//TextRenderer.DrawText(e.Graphics, Me.theCueBannerText, drawFont, New Point(-1, -1), shadowForeColor)
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);

			// This did not solve the bug.
			//If Not Me.Visible Then
			//	Exit Sub
			//End If

			// This did not solve the bug.
			if (this.theOriginalFont == null)
			{
				return;
			}

			if (GetStyle(ControlStyles.UserPaint) != (!string.IsNullOrEmpty(this.theCueBannerText) && string.IsNullOrEmpty(this.Text)))
			{
				SetStyle(ControlStyles.AllPaintingInWmPaint, !string.IsNullOrEmpty(this.theCueBannerText) && string.IsNullOrEmpty(this.Text));
				SetStyle(ControlStyles.DoubleBuffer, !string.IsNullOrEmpty(this.theCueBannerText) && string.IsNullOrEmpty(this.Text));
				SetStyle(ControlStyles.UserPaint, !string.IsNullOrEmpty(this.theCueBannerText) && string.IsNullOrEmpty(this.Text));
				if (this.theOriginalFont != null)
				{
					this.Font = new System.Drawing.Font(this.theOriginalFont.FontFamily, this.theOriginalFont.Size, this.theOriginalFont.Style, this.theOriginalFont.Unit);
				}
				this.Invalidate();
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if (this.theOriginalFont == null)
			{
				//NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
				//      so save the Font after widget is visible for first time, but before changing style within the widget.
				this.theOriginalFont = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, this.Font.Style, this.Font.Unit);

				//SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "")
				SetStyle(ControlStyles.AllPaintingInWmPaint, !string.IsNullOrEmpty(this.theCueBannerText) && string.IsNullOrEmpty(this.Text));
				SetStyle(ControlStyles.DoubleBuffer, !string.IsNullOrEmpty(this.theCueBannerText) && string.IsNullOrEmpty(this.Text));
				SetStyle(ControlStyles.UserPaint, !string.IsNullOrEmpty(this.theCueBannerText) && string.IsNullOrEmpty(this.Text));
				//Me.Invalidate()
			}
		}

		private string theCueBannerText;
		private Font theOriginalFont;

	}

}