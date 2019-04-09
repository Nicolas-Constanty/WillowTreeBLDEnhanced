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

namespace WillowTree
{
    public static class Parse
    {
        public delegate short ParseShort_ErrorHandler(string str);
        public delegate int ParseInt_ErrorHandler(string str);
        public delegate double ParseDouble_ErrorHandler(string str);

        public static short AsShort(string str)
        {
            short outvalue;
            if (short.TryParse(str, out outvalue))
                return outvalue;
            throw new FormatException();
        }
        public static short AsShort(string str, short defaultvalue)
        {
            short outvalue;
            if (short.TryParse(str, out outvalue))
                return outvalue;
            return defaultvalue;
        }
        public static short AsShort(string str, ParseShort_ErrorHandler errhandler)
        {
            short outvalue;
            if (short.TryParse(str, out outvalue))
                return outvalue;
            if (errhandler != null)
                return errhandler(str);
            return outvalue;
        }

        public static int AsInt(string str)
        {
            int outvalue;
            if (int.TryParse(str, out outvalue))
                return outvalue;
            throw new FormatException();
        }
        public static int AsInt(string str, int defaultvalue)
        {
            int outvalue;
            if (int.TryParse(str, out outvalue))
                return outvalue;
            return defaultvalue;
        }
        public static int AsInt(string str, ParseInt_ErrorHandler errhandler)
        {
            int outvalue;
            if (int.TryParse(str, out outvalue))
                return outvalue;
            if (errhandler != null)
                return errhandler(str);
            return outvalue;
        }

        public static double AsDouble(string str)
        {
            double outvalue;
            if (double.TryParse(str, out outvalue))
                return outvalue;
            throw new FormatException();
        }
        public static double AsDouble(string str, double defaultvalue)
        {
            double outvalue;
            if (double.TryParse(str, out outvalue))
                return outvalue;
            return defaultvalue;
        }
        public static double AsDouble(string str, ParseDouble_ErrorHandler errhandler)
        {
            double outvalue;
            if (double.TryParse(str, out outvalue))
                return outvalue;
            if (errhandler != null)
                return errhandler(str);
            return outvalue;
        }
        public static void ThrowExceptionIfIntString(string StringVal)
        {
            int tempValue;
            if (int.TryParse(StringVal, out tempValue))
                throw new System.FormatException();
        }

    }
}
