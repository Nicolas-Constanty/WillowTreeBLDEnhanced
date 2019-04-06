/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2010, 2011 XanderChaos
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
using System.Linq;

namespace WillowTree
{
    public static partial class Util 
    {
        public static void ResizeArrayLarger(ref string[,] Input, int rows, int cols)
        {
            string[,] newArray = new string[rows, cols];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        public static void ResizeArraySmaller(ref string[,] Input, int rows, int cols)
        {
            string[,] newArray = new string[rows, cols];
            Array.Copy(Input, 0, newArray, 0, (long)(rows * cols));
            Input = newArray;
        }
        public static void ResizeArrayLarger(ref string[] Input, int rows)
        {
            string[] newArray = new string[rows];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        public static void ResizeArraySmaller(ref string[] Input, int rows)
        {
            string[] newArray = new string[rows];
            Array.Copy(Input, 0, newArray, 0, (long)rows);
            Input = newArray;
        }
        public static void ResizeArrayLarger(ref int[,] Input, int rows, int cols)
        {
            int[,] newArray = new int[rows, cols];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        public static void ResizeArraySmaller(ref int[,] Input, int rows, int cols)
        {
            int[,] newArray = new int[rows, cols];
            Array.Copy(Input, 0, newArray, 0, (long)((rows) * cols));
            Input = newArray;
        }
        public static void ResizeArrayLarger(ref int[] Input, int rows)
        {
            int[] newArray = new int[rows];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        public static void ResizeArraySmaller(ref int[] Input, int rows)
        {
            int[] newArray = new int[rows];
            Array.Copy(Input, 0, newArray, 0, (long)rows);
            Input = newArray;
        }
        public static void ResizeArrayLarger(ref float[] Input, int rows)
        {
            float[] newArray = new float[rows];
            Array.Copy(Input, newArray, Input.Length);
            Input = newArray;
        }
        public static void ResizeArraySmaller(ref float[] Input, int rows)
        {
            float[] newArray = new float[rows];
            Array.Copy(Input, 0, newArray, 0, (long)rows);
            Input = newArray;
        }
        public static bool CheckIfNull(string[] Array)
        {
            try
            {
                if (Array.Length > 0)
                    return false;
                else
                    return true;
            }
            catch { return true; }
        }

        public static int IndexOf(this string[] Array, string value)
        {
            int count = Array.Length;
            for (int i = 0; i < count; i++)
            {
                if (Array[i] == value)
                    return i;
            }

            return -1;
        }
    }
}
