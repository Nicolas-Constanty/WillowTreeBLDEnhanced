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
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WillowTree.CustomControls
{
    [System.ComponentModel.DesignTimeVisible(true)]
    [System.ComponentModel.DesignerCategory("")]
    public class Slider : Control
    {
        #region Events

        public event EventHandler MaximumValueChanged;
        public event EventHandler MinimumValueChanged;
        public event EventHandler ValueChanged;

        #endregion

        #region Fields

        internal int _Minimum = 0;
        internal int _Maximum = 100;
        internal int _Value = 50;
        internal TrackBarThumbState _ThumbState = TrackBarThumbState.Normal;
        internal Rectangle _ThumbRect;
        internal bool _Dragging = false;
        Pen _TickPen = Pens.Black;

        #endregion

        #region Methods

        public virtual void OnMaximumValueChanged(EventArgs e)
        {
            if (MaximumValueChanged != null)
                MaximumValueChanged(this, e);
        }
        public virtual void OnMinimumValueChanged(EventArgs e)
        {
            if (MinimumValueChanged != null)
                MinimumValueChanged(this, e);
        }
        public virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        public static void DrawBottomPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
        {
            int x1 = bounds.X;
            int y1 = bounds.Y;
            int x2 = bounds.X + bounds.Width - 1;
            int y2 = bounds.Y + bounds.Height - 1;
            int x3 = (x1 + x2) / 2;
            int y3 = y2 - (x3 - x1);
            Point[] points = new Point[] { 
                new Point(x1, y1),
                new Point(x1, y3),
                new Point(x3, y2),
                new Point(x2, y3),
                new Point(x2, y1),
                new Point(x1, y1),
            };

            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            Brush fillBrush;
            Pen drawPen = new Pen(new SolidBrush(Color.FromArgb(77, 97, 133)));

            if (state == TrackBarThumbState.Normal)
                fillBrush = new SolidBrush(Color.FromArgb(188, 212, 249));
            else if (state == TrackBarThumbState.Hot)
                fillBrush = Brushes.BlanchedAlmond;
            else if (state == TrackBarThumbState.Pressed)
                fillBrush = Brushes.BlanchedAlmond;
            else // (state == TrackBarThumbState.Disabled)
                fillBrush = Brushes.Gray;

            g.FillPath(fillBrush, path);
            g.DrawLines(drawPen, points);
        }
        public static void DrawHorizontalTrack(Graphics g, Rectangle bounds)
        {
//            ControlPaint.DrawBorder(g, bounds, Color.LightSteelBlue, ButtonBorderStyle.Inset);
            ControlPaint.DrawButton(g, bounds, ButtonState.Pushed);
//            ControlPaint.DrawBorder(g, bounds, SystemColors.Control, ButtonBorderStyle.Inset);
        }

        void DrawTicks(Graphics g, Rectangle rect, int tickCount)
        {
            int x1 = rect.Left;
            int x2 = rect.Right - 1;
            int y1 = rect.Top;
            int y2 = rect.Bottom - 1;
            int drawWidth = rect.Width - 1;
            int halfTick = tickCount / 2;
            int tickMax = tickCount - 1;

            g.DrawLine(_TickPen, x1, y1, x1, y2);
            for (int i = 1; i < tickCount - 1; i++)
            {
                int xval = x1 + (drawWidth * i + halfTick) / tickMax;
                g.DrawLine(_TickPen, xval, y1, xval, y2 - 1);
            }
            g.DrawLine(_TickPen, x2, y1, x2, y2);
        }

        void SetThumbState(TrackBarThumbState state)
        {
            if (state != _ThumbState)
            {
                _ThumbState = state;
                Invalidate(_ThumbRect);
                Update();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (Enabled)
            {
                // Mouse move events are only received while in the control's
                // rectangle. If the mouse suddenly jumps from on top of the
                // thumb to outside the control rectangle it won't ever receive
                // a mouse move event where the pointer is not on the thumb, so
                // this handles that case and changes the state from hot back
                // to normal.
                if (Focused == false)
                    SetThumbState(TrackBarThumbState.Normal);
            }
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Enabled)
            {
                if (!Focused)
                    Focus();
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    // See if it is a new click on the thumb rectangle
                    if (_ThumbRect.Contains(e.Location))
                    {
                        _Dragging = true;
                        SetThumbState(TrackBarThumbState.Normal);
                    }
                }
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Enabled)
            {
                _Dragging = false;
                UpdateThumbValue(e.Location);

                TrackBarThumbState thumbstate;
                if (Focused || _ThumbRect.Contains(e.Location))
                    thumbstate = TrackBarThumbState.Hot;
                else
                    thumbstate = TrackBarThumbState.Normal;
                SetThumbState(thumbstate);
            }
            base.OnMouseUp(e);
        }
        void UpdateThumbValue(Point pt)
        {
            int ticks = _Maximum - _Minimum;
            int sliderpixels = this.Width - 2 * 13;
            int TickSelected = ((pt.X - 13) * ticks + sliderpixels / 2) / (sliderpixels - 1) + _Minimum;
            int tickx = (_Value - _Minimum) * (sliderpixels - 1) / ticks + 13;
            int leftover = (pt.X - tickx);
//            Parent.Controls["textBox1"].Text = leftover.ToString();
            if (TickSelected > _Maximum)
                TickSelected = _Maximum;
            else if (TickSelected < _Minimum)
                TickSelected = _Minimum;
            if (TickSelected != _Value)
            {
                _Value = TickSelected;
                OnValueChanged(EventArgs.Empty);
                Invalidate();
                Update();
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Enabled)
            {
                if (_Dragging)
                {
                    UpdateThumbValue(e.Location);
                    return;
                }

                TrackBarThumbState thumbstate; ;
                if (Focused || _ThumbRect.Contains(e.Location))
                    thumbstate = TrackBarThumbState.Hot;
                else
                    thumbstate = TrackBarThumbState.Normal;
                SetThumbState(thumbstate);
            }
            base.OnMouseMove(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = new SolidBrush(this.ForeColor);
            Pen pen = new Pen(brush);

            //            DrawTrack(g, pen);

            int padding = 2;
            int ticks = _Maximum - _Minimum + 1;
            int sliderpixels = this.Width - 2 * (5 + padding);
            int tickheight = 3;

//            _ThumbRect = new Rectangle(padding + (_Value - _Minimum) * (sliderpixels - 1) / (ticks - 1), tickheight, 11, 21);
            int xval = padding + ((sliderpixels - 1) * (_Value - _Minimum) + (ticks / 2)) / (ticks - 1);
            _ThumbRect = new Rectangle(xval, tickheight, 11, 21);

            if (!Enabled)
                _ThumbState = TrackBarThumbState.Disabled;

            TrackBarRenderer.DrawHorizontalTrack(g, new Rectangle(padding, 10, this.Width - 2 * padding, 4));
            TrackBarRenderer.DrawBottomPointingThumb(g, _ThumbRect, _ThumbState);
//            DrawHorizontalTrack(g, new Rectangle(padding, 10, this.Width - 2 * padding, 6));
//            DrawBottomPointingThumb(g, _ThumbRect, _ThumbState);
            EdgeStyle edge;
            edge = EdgeStyle.Bump;
            TrackBarRenderer.DrawHorizontalTicks(g, new Rectangle(5 + padding - 1, 26, sliderpixels + 1, tickheight), ticks, edge);
//            TrackBarRenderer.DrawHorizontalTicks(g, new Rectangle(5 + padding - 1, 26, sliderpixels + 1, 4), 2, edge);
//            DrawTicks(g, new Rectangle(5 + padding, 26, sliderpixels, tickheight), ticks);

            if (Focused && !(Parent is SlideSelector))
                System.Windows.Forms.ControlPaint.DrawFocusRectangle(g, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }
        protected override void OnGotFocus(EventArgs e)
        {
            if (Enabled && !_Dragging)
                SetThumbState(TrackBarThumbState.Hot);
            // Force repaint so focus rectangle will be drawn
            Invalidate();
            Update();
            OnChangeUICues(new UICuesEventArgs(UICues.ChangeFocus | UICues.ShowFocus));

            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            if (Enabled)
                SetThumbState(TrackBarThumbState.Normal);
            // Force repaint so focus rectangle will be erased
            Invalidate();
            Update();
            OnChangeUICues(new UICuesEventArgs(UICues.ChangeFocus));
            base.OnLostFocus(e);
        }
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Down:
                case Keys.Up:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Down:
                    if (_Value < _Maximum)
                        Value++;
                    break;
                case Keys.Left:
                case Keys.Up:
                    if (_Value > _Minimum)
                        Value--;
                    break;
                default:
                    break;
            }
            base.OnKeyDown(e);
        }

        #endregion

        #region Properties

        public int Maximum
        {

            get
            {
                return _Maximum;
            }
            set
            {
                _Maximum = value;
                OnMaximumValueChanged(EventArgs.Empty);
            }
        }
        public int Minimum
        {

            get
            {
                return _Minimum;
            }
            set
            {
                _Minimum = value;
                OnMinimumValueChanged(EventArgs.Empty);
            }
        }

        public Color TickColor
        {
            get
            {
                return _TickPen.Color;
            }
            set
            {
                _TickPen = new Pen(new SolidBrush(value));
            }
        }
        public int Value
        {

            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    Invalidate();
                    Update();
                }
                OnValueChanged(EventArgs.Empty);
            }
        }

        #endregion

        public Slider()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.Selectable, true);
        }
    }
}
