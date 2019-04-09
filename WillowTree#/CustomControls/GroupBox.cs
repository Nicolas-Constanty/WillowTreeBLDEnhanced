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
//using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel;
//using System.Drawing.Text;
//using System.Diagnostics;
//using System.IO;

namespace WillowTree.CustomControls
{
    [DesignTimeVisible(false)]
    [System.ComponentModel.DesignerCategory("")]
    [Designer(typeof(GroupBoxControlDesigner))]
    public class CCGroupBox : Control
    {
        Rectangle GroupRectangle;
        Rectangle TitleRectangle;

        public virtual new void ResetForeColor()
        {
            ForeColor = DefaultForeColor;
        }
        public override Color ForeColor
        {
            get
            {
                if (base.ForeColor == Color.Empty)
                    return DefaultForeColor;
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        public static new Color DefaultForeColor
        {
            get
            {
                return SystemColors.ActiveCaption;
            }
        }
        public virtual bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor; }

        public virtual new void ResetBackColor()
        {
            BackColor = DefaultBackColor;
        }
        public static new Color DefaultBackColor
        {
            get
            {
                return Color.Transparent;
            }
        }
        public virtual bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor; }

        public virtual void ResetLineColor()
        {
            LineColor = DefaultLineColor;
        }
        Color _LineColor;
        public virtual Color LineColor
        {
            get { return _LineColor; }
            set { _LineColor = value; Invalidate(); }
        }
        public static Color DefaultLineColor
        {
            get
            {
                return Color.LightGray;
            }
        }
        public virtual bool ShouldSerializeLineColor() { return LineColor != DefaultLineColor; }

        public virtual void ResetGradientColor()
        {
            GradientColor = DefaultGradientColor;
        }
        Color _GradientColor;
        public virtual Color GradientColor
        {
            get { return _GradientColor; }
            set { _GradientColor = value; Invalidate(); }
        }
        public static Color DefaultGradientColor
        {
            get
            {
                return Color.Transparent;
            }
        }
        public virtual bool ShouldSerializeGradientColor() { return GradientColor != DefaultGradientColor; }

        public virtual void ResetPanelColor()
        {
            PanelColor = DefaultPanelColor;
        }
        Color _PanelColor;
        public virtual Color PanelColor
        {
            get { return _PanelColor; }
            set { _PanelColor = value; Invalidate(); }
        }
        public static Color DefaultPanelColor
        {
            get
            {
                return Color.Transparent;
            }
        }
        public virtual bool ShouldSerializePanelColor() { return PanelColor != DefaultPanelColor; }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public CCGroupBox()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        const int xmargin = 2;
        const int ymargin = 2;
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            bool hasTitle = !(string.IsNullOrEmpty(this.Text) || (this.Font == null));

            if (hasTitle)
            {
                Size textSize = TextRenderer.MeasureText(g, this.Text, this.Font);
                TitleRectangle = new Rectangle(10, 0, textSize.Width + xmargin * 2, textSize.Height + ymargin * 2);
                GroupRectangle = new Rectangle(0, TitleRectangle.Height / 2, this.Width - 1, this.Height - TitleRectangle.Height / 2 - 1);
                //DrawGradientBox(e.Graphics, GroupRectangle);
                //DrawGradientBox(e.Graphics, TitleRectangle);
                DrawGroupBox(e.Graphics, GroupRectangle, TitleRectangle);
                TextRenderer.DrawText(e.Graphics, this.Text, this.Font, new Point(10 + xmargin, 0 + ymargin), this.ForeColor);
            }
            else
            {
                GroupRectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                DrawGradientBox(e.Graphics, GroupRectangle);
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                int height;
                if (string.IsNullOrEmpty(this.Text))
                    height = 0;
                else
                    height = (int)Math.Ceiling(this.Font.GetHeight()) + ymargin * 2;
                return new Rectangle(this.Padding.Left + 3, Padding.Top + height + 3, this.Width - Padding.Left - Padding.Right - 6, this.Height - Padding.Top - Padding.Bottom - height - 6);
             }
        }

        void DrawGroupBox(Graphics g, Rectangle groupRect, Rectangle titleRect)
        {
            if (groupRect.Height < 0)
                return;

            int archeight = 11;
            int arcwidth = 11;
            int boxwidth = groupRect.Width;
            int boxheight = groupRect.Height;
            int x1 = groupRect.Left;
            int x2 = x1 + boxwidth - arcwidth;
            //            int x2 = boxwidth;
            int y1 = groupRect.Top;
            int y2 = y1 + boxheight - archeight;
            int x3 = titleRect.Left;
            int x4 = x3 + titleRect.Width - arcwidth;
            int y3 = titleRect.Top;
            int y4 = y3 + titleRect.Height - archeight;

            Pen linePen = new Pen(LineColor);
            GraphicsPath pathPage = new GraphicsPath();
            pathPage.AddLine(titleRect.Right, y1, x2, y1);
            pathPage.AddArc(x2, y1, arcwidth, archeight, 270, 90);
            pathPage.AddArc(x2, y2, arcwidth, archeight, 0, 90);
            pathPage.AddArc(x1, y2, arcwidth, archeight, 90, 90);
            pathPage.AddArc(x1, y1, arcwidth, archeight, 180, 90);
            pathPage.AddLine(x1 + (arcwidth+1)/2, y1, titleRect.Left, y1);
            pathPage.AddArc(x3, y4, arcwidth, archeight, 180, -90);
            pathPage.AddArc(x4, y4, arcwidth, archeight, 90, -90);
            pathPage.CloseAllFigures();
            g.FillPath(new LinearGradientBrush(new Point(0, y1-1), new Point(0, y1 + boxheight), PanelColor, GradientColor), pathPage);
//            g.DrawPath(linePen, pathPage);

            GraphicsPath pathTitle = new GraphicsPath();
            pathTitle.AddArc(x3, y3, arcwidth, archeight, 180, 90);
            pathTitle.AddArc(x4, y3, arcwidth, archeight, 270, 90);
            pathTitle.AddArc(x4, y4, arcwidth, archeight, 0, 90);
            pathTitle.AddArc(x3, y4, arcwidth, archeight, 90, 90);
            pathTitle.CloseAllFigures();
            g.FillPath(new SolidBrush(GradientColor), pathTitle);

//            g.FillPath(new LinearGradientBrush(new Point(0, y3), new Point(0, y3 + boxheight), PanelColor, GradientColor), pathTitle);
//            g.DrawPath(new Pen(Color.FromArgb(100, Color.Black)), pathTitle);
            pathTitle.AddPath(pathPage, false);
//            g.DrawPath(new Pen(Color.FromArgb(100, Color.Black)), pathTitle);
            g.DrawPath(new Pen(LineColor), pathTitle);
            //            DrawGradientBox(g, titleRect);
        }

        void DrawGradientBox(Graphics g, Rectangle rect)
        {
            int archeight = 11;
            int arcwidth = 11;
            int boxwidth = rect.Width;
            int boxheight = rect.Height;
            int x1 = rect.Left;
            int x2 = x1 + boxwidth - arcwidth;
            //            int x2 = boxwidth;
            int y1 = rect.Top;
            int y2 = y1 + boxheight - archeight;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(x1, y1, arcwidth, archeight, 180, 90);
            path.AddArc(x2, y1, arcwidth, archeight, 270, 90);
            path.AddArc(x2, y2, arcwidth, archeight, 0, 90);
            path.AddArc(x1, y2, arcwidth, archeight, 90, 90);
            path.CloseAllFigures();
            g.FillPath(new LinearGradientBrush(new Point(0, y1), new Point(0, y1 + boxheight), PanelColor, GradientColor), path);
            g.DrawPath(new Pen(LineColor), path);
        }
    }

    class GroupBoxControlDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
        }

        public override System.Collections.IList SnapLines
        {
            get
            {
                //                if (!File.Exists("c:\filedog.txt"))
                //{

                //    StringWriter sw = new StringWriter();
                //    foreach (SnapLine line in base.SnapLines)
                //    {
                //        sw.WriteLine("Priority: " + line.Priority.ToString());
                //        sw.WriteLine("Offset: " + line.Offset.ToString());
                //        sw.WriteLine("Filter: " + line.Filter);
                //        sw.WriteLine("SnapLineType: " + line.SnapLineType.ToString());
                //    }
                //    this.Control.Text = sw.ToString();
                //}
                int ymargin = 2;
                int height;
                if (string.IsNullOrEmpty(this.Control.Text))
                    height = 0;
                else
                    height = this.Control.Font.Height + ymargin * 2;
            //        height = (int)Math.Ceiling(this.Control.Font.GetHeight()) + ymargin * 2;

                ArrayList _SnapLines = new ArrayList() {
                        new SnapLine(SnapLineType.Top,    0,    SnapLinePriority.Low),
                        new SnapLine(SnapLineType.Bottom, this.Control.Height - 1, SnapLinePriority.Low),
                        new SnapLine(SnapLineType.Left,   0,   SnapLinePriority.Low),
                        new SnapLine(SnapLineType.Right,  this.Control.Width - 1,  SnapLinePriority.Low),
                        new SnapLine(SnapLineType.Horizontal, -this.Control.Margin.Top,    "Margin.Top",    SnapLinePriority.Always),
                        new SnapLine(SnapLineType.Horizontal, this.Control.Height + this.Control.Margin.Bottom, "Margin.Bottom", SnapLinePriority.Always),
                        new SnapLine(SnapLineType.Vertical,   -this.Control.Margin.Left,   "Margin.Left",   SnapLinePriority.Always),
                        new SnapLine(SnapLineType.Vertical,   this.Control.Width + this.Control.Margin.Right,  "Margin.Right",  SnapLinePriority.Always),
                        new SnapLine(SnapLineType.Horizontal, height + 3,    "Padding.Top",    SnapLinePriority.Always),
                        new SnapLine(SnapLineType.Horizontal, this.Control.Height - 3, "Padding.Bottom", SnapLinePriority.Always),
                        new SnapLine(SnapLineType.Vertical,   3,   "Padding.Left",   SnapLinePriority.Always),
                        new SnapLine(SnapLineType.Vertical,   this.Control.Width - 3,  "Padding.Right",  SnapLinePriority.Always),
                    };
                return _SnapLines;
            }
        }
    }
}
