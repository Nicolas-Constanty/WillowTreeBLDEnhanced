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
//using System.Linq;

namespace WillowTree
{
    static class StringExtensions
    {
        public static string Before(this string strInput, string Terminator)
        {
            int index = strInput.IndexOf(Terminator);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(0, index);
        }
        public static string Before(this string strInput, char Terminator)
        {
            int index = strInput.IndexOf(Terminator);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(0, index);
        }

        public static string BeforeLast(this string strInput, string Terminator)
        {
            int index = strInput.LastIndexOf(Terminator);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(0, index);
        }
        public static string BeforeLast(this string strInput, char Terminator)
        {
            int index = strInput.LastIndexOf(Terminator);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(0, index);
        }

        public static string After(this string strInput, string Prefix)
        {
            int index = strInput.IndexOf(Prefix);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(index + Prefix.Length);
        }
        public static string After(this string strInput, char Prefix)
        {
            int index = strInput.IndexOf(Prefix);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(index + 1);
        }

        public static string AfterLast(this string strInput, string Prefix)
        {
            int index = strInput.LastIndexOf(Prefix);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(index + Prefix.Length);
        }
        public static string AfterLast(this string strInput, char Prefix)
        {
            int index = strInput.IndexOf(Prefix);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(index + 1);
        }

        public static string StartAt(this string strInput, string Beginning)
        {
            int index = strInput.IndexOf(Beginning);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(index);
        }
        public static string StartAt(this string strInput, char Beginning)
        {
            int index = strInput.IndexOf(Beginning);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(index);
        }

        public static string EndAt(this string strInput, string Ending)
        {
            int index = strInput.IndexOf(Ending);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(0, index + Ending.Length);
        }
        public static string EndAt(this string strInput, char Ending)
        {
            int index = strInput.IndexOf(Ending);
            if (index < 0) 
                return String.Empty;
            return strInput.Substring(0, index + 1);
        }

        public static string StartAtLast(this string strInput, string Beginning)
        {
            int index = strInput.LastIndexOf(Beginning);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(index);
        }
        public static string StartAtLast(this string strInput, char Beginning)
        {
            int index = strInput.LastIndexOf(Beginning);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(index);
        }

        public static string EndAtLast(this string strInput, string Ending)
        {
            int index = strInput.LastIndexOf(Ending);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(0, index + Ending.Length);
        }
        public static string EndAtLast(this string strInput, char Ending)
        {
            int index = strInput.LastIndexOf(Ending);
            if (index < 0)
                return String.Empty;
            return strInput.Substring(0, index + 1);
        }
    }
}
