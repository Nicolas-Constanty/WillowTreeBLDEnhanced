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
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
//using System.Windows.Forms.Design;
//using System.Windows.Forms.Design.Behavior;
using System.ComponentModel;
using System.Collections.ObjectModel;
//using System.ComponentModel.Design;
//using System.Runtime;
//using System.Diagnostics;
using System.ComponentModel.Design.Serialization;

namespace WillowTree.CustomControls
{
    public class HorizontalRowLayoutEngine
    {
        public int CurrentWidth;
        public Point LayoutPoint;
        public Rectangle LayoutRectangle;
        public int Spacing;

        public Point Origin
        {
            get { return LayoutRectangle.Location; }
            set { LayoutRectangle.Location = value; }
        }

        public int RowHeight
        {
            get { return LayoutRectangle.Height; }
            set { LayoutRectangle.Height = value; }
        }

        public int MaxWidth 
        { 
            get { return LayoutRectangle.Width; }
            set { LayoutRectangle.Width = value; }
        }

        public void ResetLayout()
        {
            LayoutPoint = LayoutRectangle.Location;
        }

        public Rectangle GetLayoutRect(Size size)
        {
            Rectangle rect = new Rectangle(LayoutPoint, size);
            if (Rectangle.Intersect(rect, LayoutRectangle) == rect)
            {
                LayoutPoint.X += size.Width + Spacing;
                return rect;
            }

            return Rectangle.Empty;
        }

        public HorizontalRowLayoutEngine()
        {
        }

        public HorizontalRowLayoutEngine(Rectangle layoutRectangle)
        {
            LayoutRectangle = layoutRectangle;
            LayoutPoint = LayoutRectangle.Location;
        }
    }

    public class CCTabRenderer
    {
        public int ArcSize = 11;
        public Rectangle BorderRectangle;
        public int PanelPadding = 3;
        public int TextPadding = 4;
        public Point TabSpacing = new Point(2, 2);

        public Size Measure(Control ctl, TabInfo tab)
        {
            Size size = TextRenderer.MeasureText(tab.Text, ctl.Font);
            size.Width += 2 * TextPadding;
            size.Height += 2 * TextPadding;
            return size;
        }

        public void Paint(PaintEventArgs e, CCTabControl owner, TabInfo tab)
        {
            if (!tab.HasFlag(TabState.Visible) || (tab.TabRectangle == Rectangle.Empty))
                return;

            if (tab.HasFlag(TabState.Selected))
                PaintSelectedPage(e.Graphics, owner, tab);
            else
                this.PaintTab(e.Graphics, owner, tab);
        }

        public void PaintTab(Graphics g, CCTabControl ctl, TabInfo tab)
        {
            using (GraphicsPath path = CCPathTool.FlatRoundedRectangle(
                tab.TabRectangle, ArcSize))
            {
                g.FillPath(new SolidBrush(ctl.PanelColor), path);
                g.DrawPath(new Pen(ctl.LineColor), path);
            }

            Point textPoint = tab.TabRectangle.Location;
            textPoint.Offset(TextPadding, TextPadding);

            Color textColor;
            if (tab.HasFlag(TabState.Enabled))
                textColor = ctl.ForeColor;
            else
                textColor = ctl.DisabledTextColor;
            TextRenderer.DrawText(g, tab.Text, ctl.Font, textPoint, textColor);
        }

        private void PaintSelectedPage(Graphics g, CCTabControl ctl, TabInfo tab)
        {
            Rectangle pageRect = BorderRectangle;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(tab.TabRectangle.Left - ArcSize, pageRect.Top - ArcSize, ArcSize, ArcSize, 90, -90);
                path.AddArc(tab.TabRectangle.Left, tab.TabRectangle.Top, ArcSize, ArcSize, 180, 90);
                path.AddArc(tab.TabRectangle.Right - ArcSize, tab.TabRectangle.Top, ArcSize, ArcSize, 270, 90);
                path.AddArc(tab.TabRectangle.Right, pageRect.Top - ArcSize, ArcSize, ArcSize, 180, -90);
                path.AddArc(pageRect.Right - ArcSize, pageRect.Top, ArcSize, ArcSize, 270, 90);
                path.AddArc(pageRect.Right - ArcSize, pageRect.Bottom - ArcSize, ArcSize, ArcSize, 0, 90);
                path.AddArc(pageRect.Left, pageRect.Bottom - ArcSize, ArcSize, ArcSize, 90, 90);
                path.AddArc(pageRect.Left, pageRect.Top, ArcSize, ArcSize, 180, 90);
                path.CloseAllFigures();
                g.FillPath(new LinearGradientBrush(Rectangle.FromLTRB(pageRect.Left, tab.TabRectangle.Top, pageRect.Right, pageRect.Bottom), ctl.SelectedPanelColor, ctl.GradientColor, LinearGradientMode.Vertical), path);
                g.DrawPath(new Pen(ctl.LineColor), path);

                Point textPoint = tab.TabRectangle.Location;
                textPoint.Offset(TextPadding, TextPadding);

                Color textColor;
                if (tab.HasFlag(TabState.Enabled))
                    textColor = ctl.SelectedTextColor;
                else
                    textColor = ctl.DisabledTextColor;
                TextRenderer.DrawText(g, tab.Text, ctl.Font, textPoint, textColor);
            }
        }
    }

    public class TabInfo
    {
        public Rectangle TabRectangle;
        public TabState State;
        public string Text;

        public bool HasFlag(TabState flag)
        {
            return (State & flag) == flag;
        }
        public void SetFlag(TabState flag)
        {
            State = (State & ~flag) | flag;
        }
        public void ToggleFlag(TabState flag)
        {
            State = (State ^ flag);
        }
        public void ClearFlag(TabState flag)
        {
            State = State & ~flag;
        }

        public TabInfo()
        {
            State = TabState.Visible | TabState.Enabled;
        }

        public TabInfo(Control ctl, CCTabRenderer c, string caption)
        {
            Text = caption;
            State = TabState.Visible | TabState.Enabled;
        }
    }

    public enum TabState : int
    {
        // The tab states are binary flags so they should be powers of two
        Visible = 1,
        Enabled = 2,
        Selected = 4,

        // Anything that isn't a power of two is a combination of two states
        Selectable = 3,  // Visible + Enabled
    }

    // matt911: I'm pretty much a complete novice when it comes to writing
    // writing designers and controls but Windows Forms just has too many
    // limitations and bugs to avoid having to write custom controls.  This
    // tab control is not as comprehensive as that one, but I made it to work
    // around the lack of ability to control the background color and use
    // transparency in the Windows Forms tab control and I also didn't like the
    // look of the Windows Forms one.  This could still use a lot of 
    // improvement, but it gets the job done (barely) for this purpose.
    //
    // This tab control does not support many things that the Windows Forms tab 
    // control does like multiline, tab scrolling, or right-to-left layout nor 
    // does it work exactly like the Windows tab control because instead of
    // using TabPage controls you can add any control to it to act as a tab.
    /// <summary>
    /// A custom tab control for Windows Forms
    /// </summary>
    [System.ComponentModel.DesignTimeVisible(true)]
    [System.ComponentModel.DesignerCategory("")]
    [Designer(typeof(CCTabControlDesigner))]
    [DesignerSerializer(typeof(CCTabControlCodeDomSerializer), typeof(CodeDomSerializer))]
    public class CCTabControl : Control
    {
        private List<TabInfo> _tabs = new List<TabInfo>();
        private Single _dpiY;
        CCTabRenderer _renderer = new CCTabRenderer();
        HorizontalRowLayoutEngine _layoutEngine;

        private Color _GradientColor;
        public Color GradientColor
        {
            set
            {
                if (value != _GradientColor)
                {
                    _GradientColor = value;
                    Invalidate();
                }
            }
            get
            {
                return _GradientColor;
            }
        }
        public static Color DefaultGradientColor
        {
            get
            {
                return SystemColors.Window;
            }
        }
        public virtual void ResetGradientColor()
        {
            GradientColor = DefaultGradientColor;
        }
        public virtual bool ShouldSerializeGradientColor() { return GradientColor != DefaultGradientColor; }

        private Color _PanelColor;
        public Color PanelColor
        {
            set
            {
                if (value != _PanelColor)
                {
                    _PanelColor = value;
                    Invalidate();
                }
            }
            get
            {
                return _PanelColor;
            }
        }
        public static Color DefaultPanelColor
        {
            get
            {
                return Color.WhiteSmoke;
            }
        }
        public virtual void ResetPanelColor()
        {
            PanelColor = DefaultPanelColor;
        }
        public virtual bool ShouldSerializePanelColor() { return PanelColor != DefaultPanelColor; }

        private Color _SelectedPanelColor;
        public Color SelectedPanelColor
        {
            set
            {
                if (value != _SelectedPanelColor)
                {
                    _SelectedPanelColor = value;
                    Invalidate();
                }
            }
            get
            {
                return _SelectedPanelColor;
            }
        }
        public static Color DefaultSelectedPanelColor
        {
            get
            {
                return SystemColors.Window;
            }
        }
        public virtual void ResetSelectedPanelColor()
        {
            SelectedPanelColor = DefaultSelectedPanelColor;
        }
        public virtual bool ShouldSerializeSelectedPanelColor() { return SelectedPanelColor != DefaultSelectedPanelColor; }

        private Color _LineColor;
        public virtual Color LineColor
        {
            set
            {
                if (value != _LineColor)
                {
                    _LineColor = value;
                    Invalidate();
                }
            }
            get
            {
                return _LineColor;
            }
        }
        public static Color DefaultLineColor
        {
            get
            {
                return Color.Gray;
            }
        }
        public virtual void ResetLineColor()
        {
            LineColor = DefaultLineColor;
        }
        public virtual bool ShouldSerializeLineColor() { return LineColor != DefaultLineColor; }

        private Color _SelectedTextColor;
        public virtual Color SelectedTextColor
        {
            set
            {
                if (value != _SelectedTextColor)
                {
                    _SelectedTextColor = value;
                    Invalidate();
                }
            }
            get
            {
                return _SelectedTextColor;
            }
        }
        public static Color DefaultSelectedTextColor
        {
            get
            {
                return SystemColors.WindowText;
            }
        }
        public virtual void ResetSelectedTextColor()
        {
            SelectedTextColor = DefaultSelectedTextColor;
        }
        public virtual bool ShouldSerializeSelectedTextColor() { return SelectedTextColor != DefaultSelectedTextColor; }

        private Color _DisabledTextColor;
        public Color DisabledTextColor
        {
            set
            {
                if (value != _DisabledTextColor)
                {
                    _DisabledTextColor = value;
                    Invalidate();
                }
            }
            get
            {
                return _DisabledTextColor;
            }
        }
        public static Color DefaultDisabledTextColor
        {
            get
            {
                return SystemColors.GrayText;
            }
        }
        public virtual void ResetDisabledTextColor()
        {
            DisabledTextColor = DefaultDisabledTextColor;
        }
        public virtual bool ShouldSerializeDisabledTextColor() { return DisabledTextColor != DefaultDisabledTextColor; }

        public static new Color DefaultBackColor
        {
            get
            {
                return Color.Transparent;
            }
        }
        public override void ResetBackColor()
        {
            BackColor = DefaultBackColor;
        }
        public virtual bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor; }
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

        public static new Color DefaultForeColor
        {
            get
            {
                return SystemColors.WindowText;
            }
        }
        public virtual new void ResetForeColor()
        {
            ForeColor = DefaultForeColor;
        }
        public virtual bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor; }
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

        public int TabCount
        {
            get { return _tabPages.Count; }
        }

        /// <summary>
        /// Reorganizes the order of the Controls collection to be the same as
        /// the TabPages collection.  Used by CCTabControlCodeDomSerializer.
        /// </summary>
        public void Synchronize()
        {
            // The layout engine used by Control causes the Controls collection to 
            // be reorganized by the visual Z-Order on the screen after a control is
            // added to Controls and possibly at other times so the order of the
            // Controls is unpredictable and not necessarily the same order the tabs
            // appear in the tab control.  If layout is blocked with SuspendLayout()
            // then that reordering does not occur, but usually layout is active.
            //
            // For the tabs to appear in the right order after Controls is serialized
            // and reloaded, Controls has to be arranged into the actual order the 
            // tabs were added (the order that TabPages is in). This method does that. 
            // It is called by the custom serializer CCTabControlCodeDomSerializer 
            // right before serializing any CCTabControl.Controls object.
            int tabcount = _tabPages.Count;

            SuspendLayout();
            for (int i = 0; i < tabcount; i++)
            {
                Control page = _tabPages[i];
                if (page.Parent == this)
                {
                    Controls.SetChildIndex(_tabPages[i], i);
                    _tabPages[i].TabIndex = i;
                }
            }
            ResumeLayout(false);
        }

        public new class ControlCollection : System.Windows.Forms.Control.ControlCollection
        {
            CCTabControl _owner;
            public ControlCollection(CCTabControl owner)
                : base(owner)
            {
                _owner = owner;
            }

            public override void Add(Control child)
            {
                base.Add(child);
                _owner.AddTabInternal(child);
            }
            public override void AddRange(Control[] controls)
            {
                foreach (Control control in controls)
                    Add(control);
            }
            public override void Remove(Control child)
            {
                base.Remove(child);
                _owner.RemoveTabInternal(child);
            }

            internal void AddInternal(Control child)
            {
                base.Add(child);
            }
            internal void AddRangeInternal(Control[] controls)
            {
                base.AddRange(controls);
            }
            internal void RemoveInternal(Control child)
            {
                base.Remove(child);
            }
        }

        [Editor(typeof(TabPageCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public class TabPageCollection : Collection<Control>
        {
            CCTabControl _owner;
            public CCTabControl Owner
            {
                get { return _owner; }
            }

            public TabPageCollection(CCTabControl owner)
                : base()
            {
                this._owner = owner;
            }

            public new void Add(Control control)
            {
                _owner.Controls.Add(control);
            }
            public new void Clear()
            {
                _owner.Controls.Clear();
            }

            // This is to hide the inherited Insert method of the collection 
            // since ControlCollection does not have an insert method and I
            // dont feel like writing one right now.
            private new void Insert(int index, Control control)
            {
            }

            public new bool Remove(Control control)
            {
                int index = this.IndexOf(control);
                if (index == -1)
                    return false;

                _owner.Controls.RemoveAt(index);
                return true;
            }
            public new bool RemoveAt(int index)
            {
                if (index >= _owner.Controls.Count || index < 0)
                    return false;

                _owner.Controls.RemoveAt(index);
                return true;
            }

            internal void AddInternal(Control control)
            {
                base.Add(control);
            }
            internal void ClearInternal()
            {
                for (int i = this.Count - 1; i >= 0; i--)
                {
                    this[i].TextChanged -= _owner.OnChildTextChanged;
                    this[i].EnabledChanged -= _owner.OnChildEnabledChanged;
                    this[i].SizeChanged -= _owner.OnChildRectangleChanged;
                    this[i].LocationChanged -= _owner.OnChildRectangleChanged;
                    this.RemoveAtInternal(i);
                }
            }
            internal void InsertInternal(int index, Control control)
            {
                base.Insert(index, control);
            }
            internal void RemoveInternal(Control control)
            {
                base.Remove(control);
            }
            internal void RemoveAtInternal(int index)
            {
                base.RemoveAt(index);
            }
        }

        private TabPageCollection _tabPages;
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabPageCollection TabPages
        {
            get
            {
                return _tabPages;
            }
        }

        int _updatingCount = 0;
        bool _needsPaint = false;

        public void BeginUpdate()
        {
            _updatingCount++;
        }

        public void EndUpdate()
        {
            _updatingCount--;
            if ((_updatingCount == 0) && (_needsPaint == true))
                Invalidate();
        }

        int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if ((value < -1) || (value >= _tabs.Count))
                    value = -1;
                //                    throw new ArgumentOutOfRangeException();

                // Don't allow changing to a disabled tab except in design mode
                if ((value != -1) && (_tabs[value].HasFlag(TabState.Enabled) == false))
                    if (!DesignMode) return;

                // Make sure the selected index is valid or else there will be
                // exceptions when attempting to make the old tab invisible.
                if ((_selectedIndex >= _tabs.Count) || (_selectedIndex < -1))
                    _selectedIndex = -1;

                // Normally making a child control visible or invisible
                // will signal a layout event.  The layout of this control
                // doesn't change when a new tab is selected since every 
                // child panel still has the same size and position so those 
                // layout events are blocked using SuspendLayout().
                SuspendLayout();
                if (_selectedIndex >= 0)
                    _tabPages[_selectedIndex].Hide();
                _selectedIndex = value;
                if (_selectedIndex >= 0)
                {
                    _tabPages[value].Size = DisplayRectangle.Size;
                    _tabPages[value].Show();
                }
                ResumeLayout(false);

                OnSelectedIndexChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        // The DisplayRectangle determines the docking region for the child
        // controls.
        private Rectangle _displayRectangle;
        public override Rectangle DisplayRectangle
        {
            // Note that for some reason I don't understand Microsoft has made
            // the ClientRectangle and DisplayRectangle one pixel taller and 
            // wider than the area you can draw in with Graphics.DrawRectangle.
            // It will make calculations of drawable layout rectangles incorrect 
            // if this is not accounted for by all code that uses them.
            get
            {
                return _displayRectangle;
            }
        }

        public event EventHandler SelectedIndexChanged;
        // These are other events that are implemented in the Windows Forms tab control
        // I didn't implement them because I didn't see the need, but implementing
        // them would make the control more compatible with the Forms one.
        //public event TabControlEventHandler Deselected;
        //public event TabControlCancelEventHandler Deselecting;
        //public event TabControlEventHandler Selected;
        //public event TabControlCancelEventHandler Selecting;

        protected void OnChildRectangleChanged(object sender, EventArgs e)
        {
            Control ctl = sender as Control;

            if (ctl.Location != _displayRectangle.Location)
                ctl.Location = _displayRectangle.Location;
            if (ctl.Size != _displayRectangle.Size)
                ctl.Size = _displayRectangle.Size;
        }
        protected void OnChildEnabledChanged(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            bool enabled = ctl.Enabled;
            int index = _tabPages.IndexOf(ctl);
            if (index == -1)
                return;

            if (enabled)
                _tabs[index].SetFlag(TabState.Enabled);
            else
                _tabs[index].ClearFlag(TabState.Enabled);

            if (index == _selectedIndex)
            {
                if (!SelectNextTab(false))
                    if (!SelectPreviousTab(false))
                        SelectedIndex = -1;
            }
            Invalidate();
        }

        protected virtual void OnChildTextChanged(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            int index = _tabPages.IndexOf(ctl);
            if (index != -1)
            {
                TabInfo tab = _tabs[index];
                tab.Text = ctl.Text;
                tab.TabRectangle.Size = _renderer.Measure(this, tab);

                RelocateTabRectangles(index);
            }
        }

        internal void AddTabInternal(Control control)
        {
            // This method does everything that would normally be in OnControlAdded
            // including setting events on the control, creating a corresponding
            // entry in TabPages, creating a TabGlyph and placing it in _tabs,
            // and updating the SelectedIndex if desired.
            control.LocationChanged += new EventHandler(OnChildRectangleChanged);
            control.SizeChanged += new EventHandler(OnChildRectangleChanged);
            control.EnabledChanged += new EventHandler(OnChildEnabledChanged);
            control.TextChanged += new EventHandler(OnChildTextChanged);

            // Hide the new panel.  It will be shown only when
            // its tab is selected.
            control.Hide();

            control.SetBounds(_displayRectangle.X, _displayRectangle.Y, _displayRectangle.Width, _displayRectangle.Height, BoundsSpecified.All);

            _tabPages.AddInternal(control);

            string tabText = control.Text;
            if (string.IsNullOrEmpty(tabText))
                tabText = control.Name;

            TabInfo tab = new TabInfo();
            tab.Text = tabText;
            tab.TabRectangle = _layoutEngine.GetLayoutRect(_renderer.Measure(this, tab));
            _tabs.Add(tab);

            // Always try to select the new tab in design mode or if there is
            // no currently selected tab.
            if (DesignMode || _selectedIndex == -1)
            {
                // Only select the tab if it is enabled
                if (control.Enabled)
                    SelectedIndex = _tabs.Count - 1;
            }

            Invalidate();
            OnLayout(new LayoutEventArgs(this, ""));
        }

        internal bool RemoveTabInternal(Control control)
        {
            // This method does everything that would normally be in OnControlRemoved
            // including removing events from the control, removing the corresponding
            // entry from TabPages, and removing the TabGlyph from _tabs, and
            // updating the SelectedIndex if needed.
            int index = _tabPages.IndexOf(control);
            if (index == -1)
                return false;

            control.TextChanged -= new EventHandler(OnChildTextChanged);
            control.EnabledChanged -= new EventHandler(OnChildEnabledChanged);
            control.SizeChanged -= new EventHandler(OnChildRectangleChanged);
            control.LocationChanged -= new EventHandler(OnChildRectangleChanged);

            _tabPages.RemoveAtInternal(index);
            RemoveTabRectangle(index);

            if (index < _selectedIndex)
            {
                // The tab has not changed.  No event is signaled here by using the
                // SelectedIndex property setter or calling OnSelectedIndexChanged
                // even though the index has changed.
                _selectedIndex--;
            }
            else if (index == _selectedIndex)
            {
                // The selected tab is being removed.  Since the tab is changing
                // either the SelectedIndex property setter has to be used or 
                // the selection event has to be signaled.
                if ((index < _tabs.Count) && (_tabPages[index].Enabled))
                {
                    // The new tab taking the space is valid and enabled. Select it.
                    _tabPages[index].Size = DisplayRectangle.Size;
                    _tabPages[index].Show();
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
                else
                    // The new tab is invalid or disabled.  Select a new one.
                    if (!SelectNextTab(false))
                        if (!SelectPreviousTab(false))
                            SelectedIndex = -1;
            }

            Invalidate();
            OnLayout(new LayoutEventArgs(this, ""));
            return true;
        }

        /// <summary>
        /// Recalculate the location of the tab rectangles.  The size is
        /// preserved to save calculation time.  Use RecalculateTabRectangles
        /// if the size needs to be changed too.
        /// </summary>
        protected void RelocateTabRectangles(int startIndex)
        {
            _layoutEngine.ResetLayout();

            int count = _tabs.Count;
            for (int i = startIndex; i < count; i++)
            {
                TabInfo tab = _tabs[i];
                Control page = _tabPages[i];
                tab.TabRectangle = _layoutEngine.GetLayoutRect(tab.TabRectangle.Size);
            }

            Invalidate();
        }

        /// <summary>
        /// Recalculate the location and size of the tab rectangles.  Use this
        /// when the font changes or another change is made that will affect
        /// the size of all tabs.
        /// </summary>
        protected void RecalculateTabRectangles()
        {
            _layoutEngine.ResetLayout();

            // Calculate the layout rectangle for each tab
            int count = _tabs.Count;
            for (int i = 0; i < count; i++)
            {
                TabInfo tab = _tabs[i];
                Control page = _tabPages[i];
                tab.TabRectangle = _layoutEngine.GetLayoutRect(_renderer.Measure(this, tab));
            }

            Invalidate();
        }

        /// <summary>
        /// Remove a TabGlyph from the private tab glyph collection _tabs and
        /// adjust the other TabGlyphs as necessary.
        /// </summary>
        void RemoveTabRectangle(int index)
        {
            // Reset the layout point to where the tab is being removed from
            _layoutEngine.LayoutPoint = _tabs[index].TabRectangle.Location;
            _tabs.RemoveAt(index);

            // Adjust the locations of the tabs after the one that was removed
            int count = _tabs.Count;
            for (int i = index; i < count; i++)
            {
                TabInfo tab = _tabs[i];
                tab.TabRectangle = _layoutEngine.GetLayoutRect(tab.TabRectangle.Size);
                _tabPages[i].TabIndex = index;
            }

            UpdateDisplayRectangle();
        }

        /// <summary>
        /// Recalculate the value of DisplayRectangle from the current font
        /// and TabRenderContext
        /// </summary>
        void UpdateDisplayRectangle()
        {
            // Adjust the display rectangle
            int tabRegionHeight = _layoutEngine.RowHeight + _tabIndent.Y;
            _displayRectangle = ClientRectangle;
            _displayRectangle.Height -= tabRegionHeight;
            _displayRectangle.Y = tabRegionHeight;
            _displayRectangle.Inflate(-_renderer.PanelPadding, -_renderer.PanelPadding);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Detect if the click is in the rectangle of one of the tab glyphs
            // and set the corresponding page active.
            int count = _tabs.Count;
            for (int i = 0; i < count; i++)
            {
                if (_tabs[i].TabRectangle.Contains(e.Location))
                    SelectedIndex = i;
            }
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (Font == null)
                Font = DefaultFont;

            // The display rectangle and the position of all tabs is influenced
            // by the height of the font, so recalculate them.
            _layoutEngine.RowHeight = (int)(_dpiY * (this.Font.Height + _renderer.TextPadding * 2) / 96F);
            UpdateDisplayRectangle();
            RecalculateTabRectangles();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // If it misses a paint operation while updating is disabled,
            // remember it in _needsPaint so the control can be invalidated
            // when EndUpdate is called.
            if (_updatingCount != 0)
            {
                _needsPaint = true;
                return;
            }
            base.OnPaint(e);

            int tabCount = _tabs.Count;

            //if (_tabs.Count != _pages.Count)
            //{
            //    Debug.WriteLine("_tabs and _pages are out of sync. Resetting.");
            //}

            if (_selectedIndex >= _tabs.Count)
                _selectedIndex = -1;

            int tabToDraw = _selectedIndex;

            // Display rectangle is the area that the panel occupies.
            // Calculate from that the rectangle that the border around it
            // should occupy.
            _renderer.BorderRectangle = new Rectangle(_displayRectangle.X - _renderer.PanelPadding, _displayRectangle.Y - _renderer.PanelPadding, _displayRectangle.Width + 2 * _renderer.PanelPadding - 1, DisplayRectangle.Height + 2 * _renderer.PanelPadding - 1);

            for (int i = _tabs.Count - 1; i >= 0; i--)
            {
                if (i == _selectedIndex)
                    continue;

                TabInfo tab = _tabs[i];
                tab.ClearFlag(TabState.Selected);
                _renderer.Paint(e, this, tab);
            }

            if (_selectedIndex != -1)
            {
                TabInfo tab = _tabs[_selectedIndex];
                tab.SetFlag(TabState.Selected);
                _renderer.Paint(e, this, tab);
            }
            else
            {
                TabInfo tab = new TabInfo();
                tab.Text = "(No Tabs)";
                tab.TabRectangle = _layoutEngine.GetLayoutRect(_renderer.Measure(this, tab));
                tab.SetFlag(TabState.Selected);
                _renderer.Paint(e, this, tab);
                _layoutEngine.ResetLayout();
            }

            _needsPaint = false;
        }
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // This is the method that is called to set the Size or Location 
            // property of the tab control as a result of a property setting 
            // (Size = value or Location = value) or a SetBounds call.
            base.SetBoundsCore(x, y, width, height, specified);
            UpdateDisplayRectangle();
            RelocateTabRectangles(0);

            if (_selectedIndex != -1)
                TabPages[_selectedIndex].Bounds = DisplayRectangle;
        }

        public virtual CCPanel CreatePageInstance()
        {
            return new CCPanel();
        }

        public void DeselectTab(int index)
        {
            if (index < 0 || index >= _tabPages.Count)
                throw new ArgumentOutOfRangeException("specified tab index is not in the valid range");

            _tabPages[index].Hide();
            _selectedIndex = index;

            SelectNextTab(true);
        }
        public void DeselectTab(string tabPageName)
        {
            if (tabPageName == null)
                throw new ArgumentNullException("tabPageName was null");
            Control[] pages = Controls.Find(tabPageName, false);
            if (pages == null || pages.Length == 0)
                throw new ArgumentNullException("no tab matching the name exists");

            int index = _tabPages.IndexOf(pages[0]);

            _tabPages[_selectedIndex].Hide();
            _selectedIndex = index;

            SelectNextTab(true);
        }
        public void DeselectTab(TabPage tabPage)
        {
            if (tabPage == null)
                throw new ArgumentNullException("tried to deselect a null tab");
            int index = _tabPages.IndexOf(tabPage);
            if (index == -1)
                throw new ArgumentOutOfRangeException("no matching tab page exists");

            _tabPages[_selectedIndex].Hide();
            _selectedIndex = index;

            SelectNextTab(true);
        }

        public virtual Type GetDefaultPageType()
        {
            return typeof(CCPanel);
        }

        public int GetTabIndex(Control tabPage)
        {
            return TabPages.IndexOf(tabPage);
        }

        public Rectangle GetTabRect(int index)
        {
            if ((index >= _tabs.Count) || (index < 0))
                return Rectangle.Empty;
            else
                return _tabs[index].TabRectangle;
        }

        public bool SelectPreviousTab(bool allowWrap)
        {
            int start = _selectedIndex;
            if (_selectedIndex < 0)
                start = 0;
            int count = _tabs.Count;

            for (int i = start - 1; i >= 0; i--)
            {
                if (_tabs[i].HasFlag(TabState.Enabled | TabState.Visible))
                {
                    SelectedIndex = i;
                    return true;
                }
            }

            if (!allowWrap)
                return false;

            for (int i = count - 1; i > start; i--)
            {
                if (_tabs[i].HasFlag(TabState.Enabled))
                {
                    SelectedIndex = i;
                    return true;
                }
            }

            return false;
        }
        public bool SelectNextTab(bool allowWrap)
        {
            int start = _selectedIndex;
            int count = _tabs.Count;

            for (int i = start + 1; i < count; i++)
            {
                if (_tabs[i].HasFlag(TabState.Selectable))
                {
                    SelectedIndex = i;
                    return true;
                }
            }

            if (!allowWrap)
                return false;

            for (int i = 0; i < start; i++)
            {
                if (_tabs[i].HasFlag(TabState.Selectable))
                {
                    SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        public void SelectTab(int index)
        {
            if (index < 0 || index >= _tabPages.Count)
                throw new ArgumentOutOfRangeException("specified tab index is not in the valid range");

            SelectedIndex = index;
        }
        public void SelectTab(string tabPageName)
        {
            if (tabPageName == null)
                throw new ArgumentNullException("tabPageName was null");
            Control[] pages = Controls.Find(tabPageName, false);
            if (pages == null)
                throw new ArgumentNullException("no tab matching the name exists");

            SelectedIndex = _tabPages.IndexOf(pages[0]);
        }
        public void SelectTab(Control tabPage)
        {
            if (tabPage == null)
                throw new ArgumentNullException("tried to select a null tab");
            int index = _tabPages.IndexOf(tabPage);
            if (index == -1)
                throw new ArgumentOutOfRangeException("no matching tab page exists");

            SelectedIndex = index;
        }

        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlCollection(this);
        }

        Point _tabIndent = new Point(10, 4);

        public CCTabControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // The tab renderer and layout engine should be initialized before
            // doing anything that changes the font, size, or location of the
            // window since UpdateDisplayRectangle accesses _renderer and
            // _layoutEngine.

            // Get the dpi since the height in pixels of the font depends on it
            using (Graphics g = this.CreateGraphics())
            {
                _dpiY = g.DpiY;
            }
            
            _renderer = new CCTabRenderer();
            _layoutEngine = new HorizontalRowLayoutEngine();

            _layoutEngine.LayoutRectangle = new Rectangle(_tabIndent, new Size(65536, (int)(_dpiY*(this.Font.Height + _renderer.TextPadding * 2)/96F)));
            _layoutEngine.Spacing = _renderer.TabSpacing.X;

            _selectedIndex = -1;
            _tabPages = new TabPageCollection(this);
            _GradientColor = DefaultGradientColor;
            _PanelColor = DefaultPanelColor;
            _SelectedPanelColor = DefaultSelectedPanelColor;
            _SelectedTextColor = DefaultSelectedTextColor;
            _LineColor = DefaultLineColor;
            _DisabledTextColor = DefaultDisabledTextColor;
            ForeColor = DefaultForeColor;

            Font = DefaultFont;
            Size = new Size(200, 100);
            UpdateDisplayRectangle();
        }
    }
}
