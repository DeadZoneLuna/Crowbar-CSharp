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
	public class WorkshopItemBindingList : BindingListEx<WorkshopItem>
	{
#region Create and Destroy

		public WorkshopItemBindingList() : base()
		{

			this.theDraftItemCount = 0;
			this.theTemplateItemCount = 0;
			this.theChangedItemCount = 0;
			this.thePublishedItemCount = 0;
		}

#endregion

#region Properties

		public uint DraftItemCount
		{
			get
			{
				return this.theDraftItemCount;
			}
		}

		public uint TemplateItemCount
		{
			get
			{
				return this.theTemplateItemCount;
			}
		}

		public uint ChangedItemCount
		{
			get
			{
				return this.theChangedItemCount;
			}
		}

		public uint PublishedItemCount
		{
			get
			{
				return this.thePublishedItemCount;
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
					this.theDraftItemCount += 1U;
				}
				else if (item.IsTemplate)
				{
					this.theTemplateItemCount += 1U;
				}
				else if (item.IsChanged)
				{
					this.theChangedItemCount += 1U;
				}
				else
				{
					this.thePublishedItemCount += 1U;
				}
			}
			else if (e.ListChangedType == System.ComponentModel.ListChangedType.ItemDeleted && e.OldIndex == -2)
			{
				WorkshopItem item = this[e.NewIndex];
				if (item.IsDraft)
				{
					this.theDraftItemCount -= 1U;
				}
				else if (item.IsTemplate)
				{
					this.theTemplateItemCount -= 1U;
				}
				else if (item.IsChanged)
				{
					this.theChangedItemCount -= 1U;
				}
				else
				{
					this.thePublishedItemCount -= 1U;
				}
				//ElseIf e.ListChangedType = System.ComponentModel.ListChangedType.ItemDeleted AndAlso e.OldIndex = -1 Then
			}
			else if (e.ListChangedType == System.ComponentModel.ListChangedType.Reset)
			{
				this.theDraftItemCount = 0;
				this.theTemplateItemCount = 0;
				this.theChangedItemCount = 0;
				this.thePublishedItemCount = 0;
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