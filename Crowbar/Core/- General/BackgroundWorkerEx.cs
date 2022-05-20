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
	public class BackgroundWorkerEx : BackgroundWorker
	{
#region Create and Destroy

		public static BackgroundWorkerEx RunBackgroundWorker(BackgroundWorkerEx bw, DoWorkEventHandler bw_DoWork, ProgressChangedEventHandler bw_ProgressChanged, RunWorkerCompletedEventHandler bw_RunWorkerCompleted, object bw_argument)
		{
			if (bw == null)
			{
				bw = new BackgroundWorkerEx();
				bw.WorkerSupportsCancellation = true;
				bw.WorkerReportsProgress = true;
			}
			else
			{
				while (bw.IsBusy)
				{
					bw.CancelAsync();
					Application.DoEvents();
				}
			}

			bw.DoWorkHandler = bw_DoWork;
			bw.ProgressChangedHandler = bw_ProgressChanged;
			bw.RunWorkerCompleted += bw.BWE_RunWorkerCompleted;
			bw.ExternalRunWorkerCompletedHandler = bw_RunWorkerCompleted;

			bw.RunWorkerAsync(bw_argument);

			return bw;
		}

#endregion

#region Properties

		public DoWorkEventHandler DoWorkHandler
		{
			get
			{
				return this.theDoWorkHandler;
			}
			set
			{
				this.theDoWorkHandler = value;
				this.DoWork += this.theDoWorkHandler;
			}
		}

		public ProgressChangedEventHandler ProgressChangedHandler
		{
			get
			{
				return this.theProgressChangedHandler;
			}
			set
			{
				this.theProgressChangedHandler = value;
				this.ProgressChanged += this.theProgressChangedHandler;
			}
		}

		public RunWorkerCompletedEventHandler ExternalRunWorkerCompletedHandler
		{
			get
			{
				return this.theExternalRunWorkerCompletedHandler;
			}
			set
			{
				this.theExternalRunWorkerCompletedHandler = value;
			}
		}

#endregion

#region Methods

		public void Kill()
		{
			this.DoWork -= this.theDoWorkHandler;
			this.ProgressChanged -= this.theProgressChangedHandler;
			this.RunWorkerCompleted -= this.BWE_RunWorkerCompleted;
		}

#endregion

#region Private Methods

		private void BWE_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			//Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)

			//RemoveHandler bw.DoWork, Me.theDoWorkHandler
			//RemoveHandler bw.ProgressChanged, Me.theProgressChangedHandler
			//RemoveHandler bw.RunWorkerCompleted, AddressOf Me.BWE_RunWorkerCompleted
			//Me.theExternalRunWorkerCompletedHandler(bw, e)
			//======
			this.Kill();
			this.theExternalRunWorkerCompletedHandler(this, e);
		}

#endregion

#region Data

		private DoWorkEventHandler theDoWorkHandler;
		private ProgressChangedEventHandler theProgressChangedHandler;
		private RunWorkerCompletedEventHandler theExternalRunWorkerCompletedHandler;

#endregion

#region Example Usage Class Template

		//Public Sub New()
		//	MyBase.New()

		//	Me.isRunning = False
		//End Sub

		//Public Sub Run()
		//	If Not Me.isRunning Then
		//		Me.isRunning = True

		//		Dim worker As BackgroundWorkerEx = Nothing
		//		Dim inputInfo As String = ""
		//		worker = BackgroundWorkerEx.RunBackgroundWorker(worker, AddressOf Me.Worker_DoWork, AddressOf Me.Worker_ProgressChanged, AddressOf Me.Worker_RunWorkerCompleted, inputInfo)

		//	End If
		//End Sub

		//'NOTE: This is run in a background thread.
		//Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		//	Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
		//	Dim outputInfo As String = ""

		//	e.Result = outputInfo
		//End Sub

		//Private Sub Worker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		//	If e.ProgressPercentage = 0 Then
		//	ElseIf e.ProgressPercentage = 1 Then
		//	End If
		//End Sub

		//Private Sub Worker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		//	If e.Cancelled Then
		//	Else
		//	End If

		//	Me.isRunning = False
		//End Sub

		//Private isRunning As Boolean

#endregion

	}

}