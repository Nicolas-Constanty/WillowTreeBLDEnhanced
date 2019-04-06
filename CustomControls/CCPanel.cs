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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;

namespace WillowTree.CustomControls
{
    /// <summary>
    /// A custom panel with a visible Text property which is used by CCTabControl to provide tab pages.
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    [DesignTimeVisible(false)]
    public class CCPanel : Panel
    {
        public static new Color DefaultForeColor { get { return Color.Black; } }
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        public new void ResetForeColor() { ForeColor = DefaultForeColor; }
        public bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor; }

        public static new Color DefaultBackColor { get { return Color.Transparent; } }
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        public new void ResetBackColor() { BackColor = DefaultBackColor; }
        public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor; }

        [Browsable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public CCPanel()
        {
            ResetFont();
            ResetForeColor();
            ResetBackColor();
            this.DoubleBuffered = true;
        }
    }
}