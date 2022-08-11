using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crowbar
{
	public class AppExceptionHandler
	{
		public void Application_ThreadException(object sender, ThreadExceptionEventArgs t)
		{
			UnhandledExceptionWindow anUnhandledExceptionWindow = new UnhandledExceptionWindow();
			try
			{
				string errorReportText = "################################################################################";

				errorReportText += "\r\n";
				errorReportText += "###### ";
				errorReportText += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				errorReportText += "   ";
				errorReportText += ConversionHelper.AssemblyProduct;
				errorReportText += " ";
				errorReportText += ConversionHelper.VersionName;
				errorReportText += "\r\n";
				errorReportText += "\r\n";

				errorReportText += "=== Steps to reproduce the error ===";
				errorReportText += "\r\n";
				errorReportText += "[Describe the last few tasks you did in ";
				errorReportText += ConversionHelper.AssemblyProduct;
				errorReportText += " before the error occurred.]";
				errorReportText += "\r\n";
				errorReportText += "\r\n";

				errorReportText += "=== What you expected to see ===";
				errorReportText += "\r\n";
				errorReportText += "[Explain what you expected ";
				errorReportText += ConversionHelper.AssemblyProduct;
				errorReportText += " to do.]";
				errorReportText += "\r\n";
				errorReportText += "\r\n";

				errorReportText += "=== Context info ===";
				errorReportText += "\r\n";
				if (MainCROWBAR.TheApp == null)
					errorReportText += "Exception occured before or after TheApp's lifetime.";
				else
				{
					//If TheApp.Settings.ViewerIsRunning Then
					//	errorReportText += "Viewing "
					//	errorReportText += TheApp.Settings.ViewMdlPathFileName
					//	errorReportText += vbCrLf
					//End If
					if (MainCROWBAR.TheApp.Settings.DecompilerIsRunning)
					{
						errorReportText += "Decompiling ";
						errorReportText += MainCROWBAR.TheApp.Settings.DecompileMdlPathFileName;
						errorReportText += "\r\n";
					}
					if (MainCROWBAR.TheApp.Settings.CompilerIsRunning)
					{
						errorReportText += "Compiling ";
						errorReportText += MainCROWBAR.TheApp.Settings.CompileQcPathFileName;
						errorReportText += "\r\n";
					}
				}
				errorReportText += "\r\n";
				errorReportText += "\r\n";

				errorReportText += "=== Exception error description ===";
				errorReportText += "\r\n";
				errorReportText += t.Exception.Message;
				errorReportText += "\r\n";
				errorReportText += "\r\n";

				errorReportText += "=== Call stack ===";
				errorReportText += "\r\n";
				errorReportText += t.Exception.StackTrace;
				errorReportText += "\r\n";

				errorReportText += "\r\n";
				errorReportText += "\r\n";
				errorReportText += "\r\n";

				WriteToErrorFile(errorReportText);

				anUnhandledExceptionWindow.ErrorReportTextBox.Text = errorReportText;
				anUnhandledExceptionWindow.ShowDialog();
			}
			catch
			{
			}
			finally
			{
				anUnhandledExceptionWindow.Dispose();
			}

			Application.Exit();
		}

		private void WriteToErrorFile(string errorReportText)
		{
			using (StreamWriter sw = new StreamWriter(MainCROWBAR.TheApp.ErrorPathFileName, true))
			{
				sw.Write(errorReportText);
				sw.Close();
			}
		}
	}
}