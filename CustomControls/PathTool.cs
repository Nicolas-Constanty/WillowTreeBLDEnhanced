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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WillowTree.CustomControls
{
    public class CCPathTool
    {
        public static GraphicsPath FlatRoundedRectangle(Rectangle rect, int arcsize)
        {
            GraphicsPath path = new GraphicsPath();
            int x1 = rect.Left;
            int x2 = rect.Right - arcsize;
            int y1 = rect.Top;
            int y2 = rect.Bottom - arcsize;
            int archeight = arcsize;
            int arcwidth = arcsize;
            path.AddArc(x1, y1, arcwidth, archeight, 180, 90);
            path.AddArc(x2, y1, arcwidth, archeight, 270, 90);
            path.AddLine(rect.Right, y1 + archeight, rect.Right, rect.Bottom);
            path.AddLine(rect.Left, rect.Bottom, rect.Left, y2 + arcsize);
            path.CloseAllFigures();
            return path;
        }

        public static GraphicsPath RoundedRectangle(Rectangle rect, int arcsize)
        {
            GraphicsPath path = new GraphicsPath();
            int x1 = rect.Left;
            int x2 = rect.Right - arcsize;
            int y1 = rect.Top;
            int y2 = rect.Bottom - arcsize;
            int archeight = arcsize;
            int arcwidth = arcsize;
            path.AddArc(x1, y1, arcwidth, archeight, 180, 90);
            path.AddArc(x2, y1, arcwidth, archeight, 270, 90);
            path.AddArc(x2, y2, arcwidth, archeight, 0, 90);
            path.AddArc(x1, y2, arcwidth, archeight, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }
}
