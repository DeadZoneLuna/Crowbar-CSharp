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
	public class CheckBoxEx : CheckBox
	{
		public CheckBoxEx() : base()
		{

		}

		public bool IsReadOnly
		{
			get
			{
				return this.theControlIsReadOnly;
			}
			set
			{
				if (this.theControlIsReadOnly != value)
				{
					this.theControlIsReadOnly = value;

					if (this.theControlIsReadOnly)
					{
						this.ForeColor = SystemColors.ControlText;
						this.BackColor = SystemColors.Control;
						// [CheckBoxEx] Maybe: Somehow change backcolor of the box.
						// [CheckBoxEx] Maybe: Somehow disable checkmarking of the box.
					}
					else
					{
						this.ForeColor = SystemColors.ControlText;
						this.BackColor = SystemColors.Window;
					}
				}
			}
		}

		protected bool theControlIsReadOnly;

	}

}