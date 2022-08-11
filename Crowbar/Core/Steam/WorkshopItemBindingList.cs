using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class WorkshopItemBindingList : BindingListEx<WorkshopItem>
	{
#region Create and Destroy

		public WorkshopItemBindingList() : base()
		{

			theDraftItemCount = 0;
			theTemplateItemCount = 0;
			theChangedItemCount = 0;
			thePublishedItemCount = 0;
		}

#endregion

#region Properties

		public uint DraftItemCount
		{
			get
			{
				return theDraftItemCount;
			}
		}

		public uint TemplateItemCount
		{
			get
			{
				return theTemplateItemCount;
			}
		}

		public uint ChangedItemCount
		{
			get
			{
				return theChangedItemCount;
			}
		}

		public uint PublishedItemCount
		{
			get
			{
				return thePublishedItemCount;
			}
		}

#endregion

#region Event Handlers

		protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
		{
			base.OnListChanged(e);

			if (e.ListChangedType == System.ComponentModel.ListChangedType.ItemAdded)
			{
				WorkshopItem item = this[e.NewIndex];
				if (item.IsDraft)
				{
					theDraftItemCount += 1U;
				}
				else if (item.IsTemplate)
				{
					theTemplateItemCount += 1U;
				}
				else if (item.IsChanged)
				{
					theChangedItemCount += 1U;
				}
				else
				{
					thePublishedItemCount += 1U;
				}
			}
			else if (e.ListChangedType == System.ComponentModel.ListChangedType.ItemDeleted && e.OldIndex == -2)
			{
				WorkshopItem item = this[e.NewIndex];
				if (item.IsDraft)
				{
					theDraftItemCount -= 1U;
				}
				else if (item.IsTemplate)
				{
					theTemplateItemCount -= 1U;
				}
				else if (item.IsChanged)
				{
					theChangedItemCount -= 1U;
				}
				else
				{
					thePublishedItemCount -= 1U;
				}
				//ElseIf e.ListChangedType = System.ComponentModel.ListChangedType.ItemDeleted AndAlso e.OldIndex = -1 Then
			}
			else if (e.ListChangedType == System.ComponentModel.ListChangedType.Reset)
			{
				theDraftItemCount = 0;
				theTemplateItemCount = 0;
				theChangedItemCount = 0;
				thePublishedItemCount = 0;
			}
		}

#endregion

#region Data

		private uint theDraftItemCount;
		private uint theTemplateItemCount;
		private uint theChangedItemCount;
		private uint thePublishedItemCount;

#endregion

	}

}