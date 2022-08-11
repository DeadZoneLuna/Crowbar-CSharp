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
	public class RichTextBoxEx : RichTextBox
	{
		private bool InstanceFieldsInitialized = false;

			private void InitializeInstanceFields()
			{
				UndoToolStripMenuItem = new ToolStripMenuItem("&Undo");
				RedoToolStripMenuItem = new ToolStripMenuItem("&Redo");
				Separator0ToolStripSeparator = new ToolStripSeparator();
				CutToolStripMenuItem = new ToolStripMenuItem("Cu&t");
				CopyToolStripMenuItem = new ToolStripMenuItem("&Copy");
				PasteToolStripMenuItem = new ToolStripMenuItem("&Paste");
				DeleteToolStripMenuItem = new ToolStripMenuItem("&Delete");
				Separator1ToolStripSeparator = new ToolStripSeparator();
				SelectAllToolStripMenuItem = new ToolStripMenuItem("Select &All");
				CopyAllToolStripMenuItem = new ToolStripMenuItem("Copy A&ll");
			}

#region Creation and Destruction

		public RichTextBoxEx() : base()
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}

			CustomMenu = new ContextMenuStrip();
			CustomMenu.Items.Add(UndoToolStripMenuItem);
			CustomMenu.Items.Add(RedoToolStripMenuItem);
			CustomMenu.Items.Add(Separator0ToolStripSeparator);
			CustomMenu.Items.Add(CutToolStripMenuItem);
			CustomMenu.Items.Add(CopyToolStripMenuItem);
			CustomMenu.Items.Add(PasteToolStripMenuItem);
			CustomMenu.Items.Add(DeleteToolStripMenuItem);
			CustomMenu.Items.Add(Separator1ToolStripSeparator);
			CustomMenu.Items.Add(SelectAllToolStripMenuItem);
			CustomMenu.Items.Add(CopyAllToolStripMenuItem);

			ContextMenuStrip = CustomMenu;

			theCueBannerText = "";
		}

#endregion

#region Init and Free

#endregion

#region Properties

		[Browsable(true)]
		[Category("Appearance")]
		[Description("Sets the text of the cue (dimmed text that only shows when Text property is empty).")]
		public string CueBannerText
		{
			get
			{
				return theCueBannerText;
			}
			set
			{
				theCueBannerText = value;
			}
		}

#endregion

#region Widget Event Handlers

		//Protected Overrides Sub OnHandleCreated(e As EventArgs)
		//	'If Me.theOriginalFont Is Nothing Then
		//	'	'NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
		//	'	'      so save the Font after widget is visible for first time, but before changing style within the widget.
		//	'	Me.theOriginalFont = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style, Me.Font.Unit)

		//	'	'SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "")
		//	'	SetStyle(ControlStyles.AllPaintingInWmPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
		//	'	SetStyle(ControlStyles.DoubleBuffer, Me.theCueBannerText <> "" AndAlso Me.Text = "")
		//	'	SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "" AndAlso Me.Text = "")
		//	'End If

		//	Me.AutoWordSelection = True
		//	Me.AutoWordSelection = False
		//End Sub

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (!string.IsNullOrEmpty(theCueBannerText) && string.IsNullOrEmpty(Text) && theOriginalFont != null)
			{
				//Dim drawFont As System.Drawing.Font = New System.Drawing.Font(Me.theOriginalFont.FontFamily, Me.theOriginalFont.Size, Me.theOriginalFont.Style, Me.theOriginalFont.Unit)
				System.Drawing.Font drawFont = new System.Drawing.Font(theOriginalFont.FontFamily, theOriginalFont.Size, FontStyle.Italic, theOriginalFont.Unit);

				Color drawForeColor = SystemColors.GrayText;
				Color drawBackColor = SystemColors.Control;
				if (drawForeColor == drawBackColor)
				{
					drawForeColor = ForeColor;
					drawBackColor = BackColor;
				}
				TextRenderer.DrawText(e.Graphics, theCueBannerText, drawFont, new Point(1, 0), drawForeColor, drawBackColor);
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
			if (theOriginalFont == null)
			{
				return;
			}

			if (GetStyle(ControlStyles.UserPaint) != (!string.IsNullOrEmpty(theCueBannerText) && string.IsNullOrEmpty(Text)))
			{
				SetStyle(ControlStyles.AllPaintingInWmPaint, !string.IsNullOrEmpty(theCueBannerText) && string.IsNullOrEmpty(Text));
				SetStyle(ControlStyles.DoubleBuffer, !string.IsNullOrEmpty(theCueBannerText) && string.IsNullOrEmpty(Text));
				SetStyle(ControlStyles.UserPaint, !string.IsNullOrEmpty(theCueBannerText) && string.IsNullOrEmpty(Text));
				if (theOriginalFont != null)
				{
					Font = new System.Drawing.Font(theOriginalFont.FontFamily, theOriginalFont.Size, theOriginalFont.Style, theOriginalFont.Unit);
				}
				Invalidate();
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if (theOriginalFont == null)
			{
				//NOTE: Font gets changed at some point after changing style, messing up when cue banner is turned off, 
				//      so save the Font after widget is visible for first time, but before changing style within the widget.
				theOriginalFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);

				//SetStyle(ControlStyles.UserPaint, Me.theCueBannerText <> "")
				SetStyle(ControlStyles.AllPaintingInWmPaint, !string.IsNullOrEmpty(theCueBannerText) && string.IsNullOrEmpty(Text));
				SetStyle(ControlStyles.DoubleBuffer, !string.IsNullOrEmpty(theCueBannerText) && string.IsNullOrEmpty(Text));
				SetStyle(ControlStyles.UserPaint, !string.IsNullOrEmpty(theCueBannerText) && string.IsNullOrEmpty(Text));

				//WORKAROUND - Without these two lines, selecting individual characters with the mouse often selects to end of words.
				AutoWordSelection = true;
				AutoWordSelection = false;
			}
		}

#endregion

#region Child Widget Event Handlers

		private void CustomMenu_Opening(System.Object sender, System.EventArgs e)
		{
			UndoToolStripMenuItem.Enabled = !ReadOnly && CanUndo;
			RedoToolStripMenuItem.Enabled = !ReadOnly && CanRedo;
			CutToolStripMenuItem.Enabled = !ReadOnly && SelectionLength > 0;
			CopyToolStripMenuItem.Enabled = SelectionLength > 0;
			PasteToolStripMenuItem.Enabled = !ReadOnly && Clipboard.ContainsText();
			DeleteToolStripMenuItem.Enabled = !ReadOnly && SelectionLength > 0;
			SelectAllToolStripMenuItem.Enabled = TextLength > 0 && SelectionLength < TextLength;
			CopyAllToolStripMenuItem.Enabled = TextLength > 0 && SelectionLength < TextLength;
		}

		private void UndoToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			Undo();
		}

		private void RedoToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			Redo();
		}

		private void CutToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			Cut();
		}

		private void CopyToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			Copy();
		}

		private void PasteToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			Paste();
		}

		private void DeleteToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			SelectedText = "";
		}

		private void SelectAllToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			SelectAll();
		}

		private void CopyAllToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			SelectAll();
			Copy();
			//Me.SelectionLength = 0
		}

#endregion

#region Core Event Handlers

#endregion

#region Private Methods

#endregion

#region Data

		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(CustomMenu))]
		private ContextMenuStrip _CustomMenu;
		private ContextMenuStrip CustomMenu
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _CustomMenu;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_CustomMenu != null)
				{
					_CustomMenu.Opening -= CustomMenu_Opening;
				}

				_CustomMenu = value;

				if (value != null)
				{
					_CustomMenu.Opening += CustomMenu_Opening;
				}
			}
		}

		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(UndoToolStripMenuItem))]
		private ToolStripMenuItem _UndoToolStripMenuItem;
		private ToolStripMenuItem UndoToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _UndoToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_UndoToolStripMenuItem != null)
				{
					_UndoToolStripMenuItem.Click -= UndoToolStripMenuItem_Click;
				}

				_UndoToolStripMenuItem = value;

				if (value != null)
				{
					_UndoToolStripMenuItem.Click += UndoToolStripMenuItem_Click;
				}
			}
		}
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(RedoToolStripMenuItem))]
		private ToolStripMenuItem _RedoToolStripMenuItem;
		private ToolStripMenuItem RedoToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _RedoToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_RedoToolStripMenuItem != null)
				{
					_RedoToolStripMenuItem.Click -= RedoToolStripMenuItem_Click;
				}

				_RedoToolStripMenuItem = value;

				if (value != null)
				{
					_RedoToolStripMenuItem.Click += RedoToolStripMenuItem_Click;
				}
			}
		}
		private ToolStripSeparator Separator0ToolStripSeparator;
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(CutToolStripMenuItem))]
		private ToolStripMenuItem _CutToolStripMenuItem;
		private ToolStripMenuItem CutToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _CutToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_CutToolStripMenuItem != null)
				{
					_CutToolStripMenuItem.Click -= CutToolStripMenuItem_Click;
				}

				_CutToolStripMenuItem = value;

				if (value != null)
				{
					_CutToolStripMenuItem.Click += CutToolStripMenuItem_Click;
				}
			}
		}
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(CopyToolStripMenuItem))]
		private ToolStripMenuItem _CopyToolStripMenuItem;
		private ToolStripMenuItem CopyToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _CopyToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_CopyToolStripMenuItem != null)
				{
					_CopyToolStripMenuItem.Click -= CopyToolStripMenuItem_Click;
				}

				_CopyToolStripMenuItem = value;

				if (value != null)
				{
					_CopyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
				}
			}
		}
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(PasteToolStripMenuItem))]
		private ToolStripMenuItem _PasteToolStripMenuItem;
		private ToolStripMenuItem PasteToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _PasteToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_PasteToolStripMenuItem != null)
				{
					_PasteToolStripMenuItem.Click -= PasteToolStripMenuItem_Click;
				}

				_PasteToolStripMenuItem = value;

				if (value != null)
				{
					_PasteToolStripMenuItem.Click += PasteToolStripMenuItem_Click;
				}
			}
		}
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(DeleteToolStripMenuItem))]
		private ToolStripMenuItem _DeleteToolStripMenuItem;
		private ToolStripMenuItem DeleteToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _DeleteToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_DeleteToolStripMenuItem != null)
				{
					_DeleteToolStripMenuItem.Click -= DeleteToolStripMenuItem_Click;
				}

				_DeleteToolStripMenuItem = value;

				if (value != null)
				{
					_DeleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
				}
			}
		}
		private ToolStripSeparator Separator1ToolStripSeparator;
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(SelectAllToolStripMenuItem))]
		private ToolStripMenuItem _SelectAllToolStripMenuItem;
		private ToolStripMenuItem SelectAllToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _SelectAllToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_SelectAllToolStripMenuItem != null)
				{
					_SelectAllToolStripMenuItem.Click -= SelectAllToolStripMenuItem_Click;
				}

				_SelectAllToolStripMenuItem = value;

				if (value != null)
				{
					_SelectAllToolStripMenuItem.Click += SelectAllToolStripMenuItem_Click;
				}
			}
		}
		[System.Runtime.CompilerServices.AccessedThroughProperty(nameof(CopyAllToolStripMenuItem))]
		private ToolStripMenuItem _CopyAllToolStripMenuItem;
		private ToolStripMenuItem CopyAllToolStripMenuItem
		{
			[System.Diagnostics.DebuggerNonUserCode]
			get
			{
				return _CopyAllToolStripMenuItem;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), System.Diagnostics.DebuggerNonUserCode]
			set
			{
				if (_CopyAllToolStripMenuItem != null)
				{
					_CopyAllToolStripMenuItem.Click -= CopyAllToolStripMenuItem_Click;
				}

				_CopyAllToolStripMenuItem = value;

				if (value != null)
				{
					_CopyAllToolStripMenuItem.Click += CopyAllToolStripMenuItem_Click;
				}
			}
		}

		private string theCueBannerText;
		private Font theOriginalFont;

#endregion

	}

}