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
				return theControlIsReadOnly;
			}
			set
			{
				if (theControlIsReadOnly != value)
				{
					theControlIsReadOnly = value;

					if (theControlIsReadOnly)
					{
						ForeColor = SystemColors.ControlText;
						BackColor = SystemColors.Control;
						// [CheckBoxEx] Maybe: Somehow change backcolor of the box.
						// [CheckBoxEx] Maybe: Somehow disable checkmarking of the box.
					}
					else
					{
						ForeColor = SystemColors.ControlText;
						BackColor = SystemColors.Window;
					}
				}
			}
		}

		protected bool theControlIsReadOnly;

	}

}