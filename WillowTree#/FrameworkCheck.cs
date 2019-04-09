/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2011 Matthew Carter <matt911@users.sf.net>
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
using System.Linq;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WillowTree
{
    public static partial class Util
    {
        public static bool CheckFrameworkVersion()
        {
            // Apparently .NET framework programs don't verify that they have
            // access to the correct framework version automatically, so this
            // routine checks the registry to ensure that NET Framework 3.5
            // service pack 1 is installed on the machine.  It returns true
            // if NET 3.5 SP1 is installed and false if it is not.
            // 
            // This was needed to solve an issue where people would report
            // being unable to save games at all even though they could open
            // them.  It was related to not having NET Framework 3.5 SP1.
            //
            // The registry keys checked here are in accordance with Microsoft's
            // recommendations at http://support.microsoft.com/kb/318785

            RegistryKey regkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v3.5");

            try
            {
                if ((regkey == null) ||
                    ((int)regkey.GetValue("Install") != 1) ||
                    ((int)regkey.GetValue("SP") < 1))
                {
                    MessageBox.Show("This program requires Microsoft .NET Framework 3.5 Service Pack 1 or greater.");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("There was an error reading the registry.  The program was unable to read HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v3.5");
                return false;
            }

            return true;
        }
    }
}
