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
	public partial class BaseForm
	{

		public BaseForm()
		{

			//TEST: See if this prevents the overlapping or larger text on Chinese Windows.
			// This should allow Forms that inherit from this class and their widgets to use the system font instead of Visual Studio's default of Microsoft Sans Serif.
			this.Font = new Font(SystemFonts.MessageBoxFont.Name, 8.25f);
			//Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)

			// This call is required by the designer.
			InitializeComponent();
		}

		//Protected Sub InitWidgets(ByVal container As Control)
		//	For Each c As Control In container.Controls
		//		c.Font = SystemFonts.MessageBoxFont

		//		If c.Controls.Count > 0 Then
		//			Me.InitWidgets(c)
		//		End If
		//	Next
		//End Sub

		//NOTE: Without this, when a user changes text in a textbox and then drags a file onto Crowbar, the text reverts back to the text before the change.
		//      With this, the changed text is kept.
		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			this.Validate();
		}

		//' Reduce flicker and/or speed of redraw when resizing main window, partiularly in Win7 Aero mode.
		//'NOTE: This has side-effect of not updating Unpack listview splitcontainer panel and does not show dragging of any splitcontainer splitter bars.
		//Protected Overrides ReadOnly Property CreateParams() As CreateParams
		//	Get
		//		Dim cp As CreateParams = MyBase.CreateParams
		//		cp.ExStyle = cp.ExStyle Or &H2000000
		//		Return cp
		//	End Get
		//End Property 'CreateParams

		//Protected Overrides Sub OnResizeBegin(ByVal e As EventArgs)
		//	SuspendLayout()
		//	MyBase.OnResizeBegin(e)
		//End Sub

		//Protected Overrides Sub OnResizeEnd(ByVal e As EventArgs)
		//	ResumeLayout()
		//	MyBase.OnResizeEnd(e)
		//End Sub

	}
}