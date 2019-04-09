/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2010 XanderChaos
 *  Copyright (C) 2010 JackSchitt
 * 
 *  WillowTree# is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  WillowTree# is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with WillowTree#.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
//using System.Linq;
using System.Windows.Forms;
//using System.Diagnostics;
//using System.Threading;

namespace WillowTree
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // The try/catch is commented out in debug build so that the exceptions will fall
            // through to the debugger.  In release build the code produces an exception
            // log upon an unhandled exception instead.  When developing run either the release
            // or the debug build, depending upon which is easier for you to locate the
            // errors.
#if !DEBUG
            // Allow all exceptions in the UI form thread to be thrown instead of using a 
            // custom Application.ThreadException handler.  This allows them to be caught 
            // with the try/catch block below.  If ThreadException is used then the exception
            // would be handled in the .NET default way and never would reach the catch block
            // before the application closes.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            try
            {
#endif
                if (Util.CheckFrameworkVersion() == false)
                    return;

                //SplashScreen splash = new SplashScreen("Resources\\loading.png");
                //splash.Show(true);
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Windows.Forms.Application.Run(new WillowTreeMain());
#if !DEBUG
            }
            catch (Exception ex)
            {
                SaveExceptionToLog(ex);
            } 
#endif
        }

        public static void SaveExceptionToLog(Exception ex)
        {
            // If any exceptions occur while initializing a static data members of a class
            // it will throw a TypeInitializationException with the InnerException containing
            // the actual exception.  The InnerException is where the interest lies for
            // debugging and troubleshooting, so replace ex with that in this case.
            if (ex is TypeInitializationException)
                ex = ex.InnerException;

            bool writtenToClipboard = false;
            bool writtenToFile = false;
            string exceptionText = ex.ToString();
            const string exceptionHeader = "WillowTree# failed due to an unhandled exception.\n";
            const string clipboardOkHeader = "The clipboard contains a copy of this error report.\n";
            const string fileOkHeader = "The file 'WillowTree#.log' in the application folder contains a copy of this error report.\n";
            const string fileAndClipboardOkHeader = "The file 'WillowTree#.log' in the application folder and the clipboard contain a copy of this error report.\n";
            const string noCopiesHeader = "Could not access the file 'WillowTree#.log' in the application folder or the clipboard, so no copy of this report has been logged.\n";

            try
            {
                Clipboard.SetText("WillowTree# failed due to an unhandled exception.\n\n" + ex.ToString() + "\n");
                writtenToClipboard= true;
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                // This exception can occur if the clipboard is busy.  Don't allow it to
                // halt the application.  Other clipboard exceptions only occur due to
                // improper programming as far as I can tell.
            }

            try 
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(db.AppPath + "WillowTree#.log", false))
                {
                    sw.Write(exceptionText);
                }
                writtenToFile = true;
            }
            catch (Exception swex)
            {
                if ((swex is UnauthorizedAccessException) ||
                    (swex is ArgumentException) ||
                    (swex is System.IO.IOException) ||
                    (swex is System.Security.SecurityException))
                {
                    // Ignore routine exceptions that indicate failure when attempting to 
                    // write to files and leave writtenToFile false.  Re-throw other more 
                    // serious exceptions.
                }
                else throw;
            }

            string copyLocationsText = (writtenToFile ? (writtenToClipboard ? fileAndClipboardOkHeader :  fileOkHeader) : (writtenToClipboard ? clipboardOkHeader : noCopiesHeader));
            string failMessage = exceptionHeader + copyLocationsText + "\n" + exceptionText;
            MessageBox.Show(failMessage, "Application Failure");
        }
    }
}
