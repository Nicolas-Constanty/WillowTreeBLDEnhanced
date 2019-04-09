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
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing.Design;
using System.Collections;
using Aga.Controls.Tree;
using Aga.Controls;
using Aga.Controls.Tree.NodeControls;

namespace WillowTree.CustomControls
{
    class AppTheme
    {
        static public Font Font = SystemFonts.DefaultFont;
        static private LabelTheme _labelTheme1;
        static public LabelTheme labelTheme1
        {
            get 
            {
                if (_labelTheme1 == null)
                {
                    AppThemes jojo = new AppThemes();
                    _labelTheme1 = jojo.labelTheme1;
                }
                return _labelTheme1;
            }
        }
    }

    [DesignTimeVisible(true)]
    [System.ComponentModel.DesignerCategory("")]
    public class WTLabel : Label, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as LabelTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as LabelTheme;
        }

        public void UpdateTheme(LabelTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public bool HasTheme() { return Theme != null; }

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

        public static new Color DefaultBackColor { get { return Color.Transparent; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }

        public bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor && !HasTheme(); }
        public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor && !HasTheme(); } 
        
        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        LabelTheme _Theme;
        [DefaultValue(null)]
        public LabelTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTLabel()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTPanel : Panel
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as PanelTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as PanelTheme;
        }

        public void UpdateTheme(PanelTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public bool HasTheme() { return Theme != null; }

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

        public static new Color DefaultBackColor { get { return Color.Transparent; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }

        public bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor && !HasTheme(); }
        public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor && !HasTheme(); } 

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        PanelTheme _Theme;
        [DefaultValue(null)]
        public PanelTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTPanel()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }        
    }

    [DesignTimeVisible(true)]
    public class WTTabControl : WillowTree.CustomControls.CCTabControl, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as TabControlTheme;
        }

        public static new Color DefaultBackColor { get { return Color.FromArgb(83, 83, 83); } }
        public static new Color DefaultForeColor { get { return Color.White; }}
        public static new Color DefaultGradientColor{ get { return Color.FromArgb(235, 235, 235); } }
        public static new Color DefaultLineColor { get { return Color.FromArgb(83,83,83); } }
        public static new Color DefaultPanelColor { get { return Color.FromArgb(83, 83, 83); } }
        public static new Color DefaultSelectedPanelColor { get { return Color.FromArgb(211, 215, 221); } }
        public static new Color DefaultSelectedTextColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }
        public override void ResetGradientColor() { GradientColor = DefaultGradientColor; }
        public override void ResetLineColor() { LineColor = DefaultLineColor; }
        public override void ResetPanelColor() { PanelColor = DefaultPanelColor; }
        public override void ResetSelectedPanelColor() { SelectedPanelColor = DefaultSelectedPanelColor; }
        public override void ResetSelectedTextColor() { SelectedTextColor = DefaultSelectedTextColor; }

        public override bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor; }
        public override bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor; }
        public override bool ShouldSerializeGradientColor() { return GradientColor != DefaultGradientColor; }
        public override bool ShouldSerializeLineColor() { return LineColor != DefaultLineColor; }
        public override bool ShouldSerializePanelColor() { return PanelColor != DefaultPanelColor; }
        public override bool ShouldSerializeSelectedPanelColor() { return SelectedPanelColor != DefaultSelectedPanelColor; }
        public override bool ShouldSerializeSelectedTextColor() { return SelectedTextColor != DefaultSelectedTextColor; }

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
        public override Color SelectedTextColor
        {
            get
            {
                return base.SelectedTextColor;
            }
            set
            {
                base.SelectedTextColor = value;
            }
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as TabControlTheme;
        }

        public void UpdateTheme(TabControlTheme theme)
        {
            BeginUpdate();
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
            this.GradientColor = theme.PanelGradientColor;
            this.PanelColor = theme.PanelColor;
            this.DisabledTextColor = theme.DisabledTextColor;
            this.LineColor = theme.LineColor;
            this.SelectedTextColor = theme.SelectedTextColor;
            this.SelectedPanelColor = theme.SelectedColor;
            EndUpdate();
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            BeginUpdate();
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
            this.GradientColor = DefaultGradientColor;
            this.PanelColor = DefaultPanelColor;
            this.DisabledTextColor = DefaultDisabledTextColor;
            this.LineColor = DefaultLineColor;
            this.SelectedTextColor = DefaultSelectedTextColor;
            this.SelectedPanelColor = DefaultSelectedPanelColor;
            EndUpdate();
        }

        TabControlTheme _Theme;
        [DefaultValue(null)]
        public TabControlTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTTabControl()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTGroupBox : WillowTree.CustomControls.CCGroupBox, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as GroupBoxTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as GroupBoxTheme;
        }

        public void UpdateTheme(GroupBoxTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
            this.PanelColor = theme.PanelColor;
            this.GradientColor = theme.GradientColor;
            this.LineColor = theme.LineColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }
        public override void ResetGradientColor() { GradientColor = DefaultGradientColor; }
        public override void ResetLineColor() { LineColor = DefaultLineColor; }
        public override void ResetPanelColor() { PanelColor = DefaultPanelColor; }

        public static new Color DefaultBackColor { get { return Color.Transparent; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }
        public static new Color DefaultGradientColor { get { return Color.FromArgb(239, 243, 252); } }
        public static new Color DefaultLineColor { get { return Color.FromArgb(167, 173, 182); } }
        public static new Color DefaultPanelColor { get { return Color.FromArgb(236, 241, 251); } }

        public override bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor; }
        public override bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor; }
        public override bool ShouldSerializeGradientColor() { return GradientColor != DefaultGradientColor; }
        public override bool ShouldSerializeLineColor() { return LineColor != DefaultLineColor; }
        public override bool ShouldSerializePanelColor() { return PanelColor != DefaultPanelColor; }

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
        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
            this.PanelColor = DefaultPanelColor;
            this.GradientColor = DefaultGradientColor;
            this.LineColor = DefaultLineColor;
        }

        GroupBoxTheme _Theme;
        [DefaultValue(null)]
        public GroupBoxTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTGroupBox()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTListBox : ListBox, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as ListBoxTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as ListBoxTheme;
        }

        public void UpdateTheme(ListBoxTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        public bool HasTheme() { return Theme != null; }

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

        public static new Color DefaultBackColor { get { return Color.White; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }

        public bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor && !HasTheme(); }
        public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor && !HasTheme(); }

        ListBoxTheme _Theme;
        [DefaultValue(null)]
        public ListBoxTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTListBox()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTTreeView : TreeViewAdv, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as TreeViewTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as TreeViewTheme;
        }

        public void UpdateTheme(TreeViewTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        //public new Color DefaultBackColor { get { return Color.White; } }
        //public new  void ResetBackColor() { BackColor = DefaultBackColor; }
        //public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor; }

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        public bool HasTheme() { return Theme != null; }

        public static new Color DefaultBackColor { get { return Color.White; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }

        public bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor && !HasTheme(); }
        public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor && !HasTheme(); }

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

        TreeViewTheme _Theme;
        [DefaultValue(null)]
        public TreeViewTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= ThemeChanged_Callback;
                    else
                        ntb.DrawText += AgaControlsExtensions.ColoredTextNode_DrawText;

                    _Theme = value;
                    if (value == null)
                    {
                        ntb.DrawText -= AgaControlsExtensions.ColoredTextNode_DrawText;
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += ThemeChanged_Callback;
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }

        NodeTextBox ntb = new NodeTextBox();
        public WTTreeView()
        {
            Model = new TreeModel();
//            this.DoubleBuffered = true;
            SetDefaultTheme();
//            Theme = Services.AppThemes.TreeView;
            ntb.DataPropertyName = "Text";
            ntb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            ntb.IncrementalSearchEnabled = true;
            ntb.LeftMargin = 3;
            ntb.ParentColumn = null;

//            ntb.DrawText += AgaControlsExtensions.ColoredTextNode_DrawText;
            this.NodeControls.Add(ntb);
        }
    }

    [DesignTimeVisible(true)]
    public class WTComboBox : ComboBox, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as ComboBoxTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as ComboBoxTheme;
        }

        public void UpdateTheme(ComboBoxTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

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

        public bool HasTheme() { return Theme != null; }

        public static new Color DefaultBackColor { get { return Color.White; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }

        public virtual bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor && !HasTheme(); }
        public virtual bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor && !HasTheme(); }

        ComboBoxTheme _Theme;
        [DefaultValue(null)]
        public ComboBoxTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTComboBox()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    public class HexUpDown : NumericUpDown
    {
        public string _hexFormatPositive = "{0:X}";
        [DefaultValue("{0:X}")]
        public string HexFormatPositive
        {
            get
            {
                return _hexFormatPositive;
            }
            set
            {
                _hexFormatPositive = value;
            }
        }

        public string _hexFormatNegative = "-{0:X}";
        [DefaultValue("-{0:X}")]
        public string HexFormatNegative
        {
            get
            {
                return _hexFormatNegative;
            }
            set
            {
                _hexFormatNegative = value;
            }
        }

        public bool _signedHex = true;
        [DefaultValue(true)]
        public bool SignedHex
        {
            get
            {
                return _signedHex;
            }
            set
            {
                _signedHex = value;
            }
        }

        protected override void ValidateEditText()
        {
            {
                base.ValidateEditText();
                return;
            }

            // (matt911) I don't remember why I replaced the commented code below with
            // the code above, so I am leaving it here for now in case it turns
            // out to be needed.  10/13/2012
            //
            //long longValue;
            //string text = base.Text;
            //if (string.IsNullOrEmpty(text))
            //{
            //    UpdateEditText();
            //    return;
            //}

            //try
            //{
            //    if (text.First() == '-')
            //    {
            //        try
            //        {
            //            longValue = -Convert.ToInt64(text.Substring(1), 16);
            //            if (longValue < 0)
            //            {
            //                // Value is equal to or less than Int64.MinValue
            //                this.Value = this.Minimum;
            //                return;
            //            }
            //        }
            //        catch (OverflowException)
            //        {
            //            this.Value = this.Minimum;
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        try
            //        {
            //            longValue = Convert.ToInt64(text, 16);
            //            if (longValue < 0)
            //            {
            //                // Value is bigger than Int64.MaxValue
            //                this.Value = this.Maximum;
            //                return;
            //            }
            //        }
            //        catch (OverflowException)
            //        {
            //            this.Value = this.Maximum;
            //            return;
            //        }
            //    }

            //    if (longValue > this.Maximum)
            //    {
            //        this.Value = this.Maximum;
            //        UpdateEditText();
            //    }
            //    else if (longValue < Int32.MinValue)
            //    {
            //        this.Value = this.Minimum;
            //        UpdateEditText();
            //    }
            //    else
            //        this.Value = (int)longValue;
            //}
            //catch
            //{
            //    if (0 <= this.Maximum && 0 >= this.Minimum)
            //        base.Text = "0";
            //    else
            //        base.Text = this.Minimum.ToString();
            //}
        }

        protected override void UpdateEditText()
        {
            if (Hexadecimal == false)
            {
                base.UpdateEditText();
                return;
            }

            if (_signedHex == false || base.Value >= 0)
                base.Text = string.Format(_hexFormatPositive, (Int64)base.Value);
            else
            {
                UInt64 twosComplement = (UInt64)(~((Int64)base.Value)) + 1;
                base.Text = string.Format(_hexFormatNegative, twosComplement); 
            }
        }
    }

    [DesignTimeVisible(true)]
    public class WTNumericUpDown : HexUpDown, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as NumericUpDownTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as NumericUpDownTheme;
        }

        public void UpdateTheme(NumericUpDownTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public bool HasTheme() { return Theme != null; }

        public static new Color DefaultBackColor { get { return Color.White; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }

        public bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor && !HasTheme(); }
        public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor && !HasTheme(); }
        public bool ShouldSerializeMaximum() { return Maximum != int.MaxValue; }
        public bool ShouldSerializeMinimum() { return Minimum != int.MinValue; }

        public new decimal Minimum
        {
            get { return base.Minimum; }
            set { base.Minimum = value; }
        }

        public new decimal Maximum
        {
            get { return base.Maximum; }
            set { base.Maximum = value; }
        }

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

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        NumericUpDownTheme _Theme;
        [DefaultValue(null)]
        public NumericUpDownTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }

        public WTNumericUpDown()
        {
            this.DoubleBuffered = true;
            Maximum = int.MaxValue;
            Minimum = int.MinValue;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTTextBox : TextBox, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as TextBoxTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as TextBoxTheme;
        }

        public void UpdateTheme(TextBoxTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        public bool HasTheme() { return Theme != null; }

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

        public static new Color DefaultBackColor { get { return Color.White; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }

        public bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor && !HasTheme(); }
        public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor && !HasTheme(); }

        TextBoxTheme _Theme;
        [DefaultValue(null)]
        public TextBoxTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTTextBox()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTMenuStrip : MenuStrip, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as MenuStripTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as MenuStripTheme;
        }

        public void UpdateTheme(MenuStripTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        public bool HasTheme() { return _Theme != null; }

//        public new static Color DefaultBackColor { get { return Color.FromArgb(188, 212, 249); } }
        public override void ResetBackColor()
        {
            BackColor = Color.FromArgb(188, 212, 249);
        }
        public static new Color DefaultBackColor { get { return Color.FromArgb(188, 212, 249); } }
        public virtual bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor; }
        public virtual new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        public override void ResetForeColor()
        {
            ForeColor = Color.Black;
        }
        public static new Color DefaultForeColor { get { return Color.Black; } }
        public virtual bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor; }
        public virtual new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }
        MenuStripTheme _Theme;
        [DefaultValue(null)]
        public MenuStripTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }

		// There is a bug or undesirable interaction with the menustrip
        // that often changes the visible property to false when you
        // edit a menustrip that is on a tab page then save the form.
        // This property is here only so that the designer serialization
        // visibility can be set to false so the designer won't ever
        // serialize visible = false into the initialization code.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public WTMenuStrip()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTButton : Button, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as ButtonTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as ButtonTheme;
        }

        public void UpdateTheme(ButtonTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        public bool HasTheme() { return Theme != null; }

        public static new Color DefaultBackColor { get { return Color.Transparent; } }
        public static new Color DefaultForeColor { get { return Color.Black; } }

        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }

        public bool ShouldSerializeForeColor() { return ForeColor != DefaultForeColor && !HasTheme(); }
        public bool ShouldSerializeBackColor() { return BackColor != DefaultBackColor && !HasTheme(); }

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

        ButtonTheme _Theme;
        [DefaultValue(null)]
        public ButtonTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTButton()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTCheckBox : CheckBox, IThemeable
    {
        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as CheckBoxTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as CheckBoxTheme;
        }

        public void UpdateTheme(CheckBoxTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
        }

        private static new Color DefaultForeColor { get { return Color.Black; } }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }
        public virtual bool ShouldSerializeForeColor() { return (ForeColor != DefaultForeColor); }
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

        private static new Color DefaultBackColor { get { return Color.Transparent; } }
        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public virtual bool ShouldSerializeBackColor() { return (BackColor != DefaultBackColor); }
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
        CheckBoxTheme _Theme;
        [DefaultValue(null)]
        public CheckBoxTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTCheckBox()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }

    [DesignTimeVisible(true)]
    public class WTFlowLayoutPanel : FlowLayoutPanel
    {
        public WTFlowLayoutPanel()
        {
            this.DoubleBuffered = true;
        }
    }

    [DesignTimeVisible(true)]
    public class WTSlideSelector : SlideSelector, IThemeable
    {
        //Color forecolor;
        //public override Color ForeColor
        //{
        //    get { return forecolor; }
        //    set
        //    {
        //        forecolor = value;
        //        Slider.ForeColor = value;
        //        UpDown.ForeColor = value;
        //    }
        //}

        //Color backcolor;
        //public override Color BackColor
        //{
        //    get { return backcolor; }
        //    set
        //    {
        //        backcolor = value;
        //        Slider.BackColor = value;
        //        UpDown.BackColor = value;
        //    }
        //}

        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            this.Theme = e.Theme as SlideSelectorTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            this.Theme = theme as SlideSelectorTheme;
        }

        public void UpdateTheme(SlideSelectorTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
            this.UpDown.BackColor = theme.TextBoxBackColor;
        }

        public void ResetTheme()
        {
            Theme = null;
        }

        private static new Color DefaultForeColor { get { return Color.Black; } }
        public override void ResetForeColor() { ForeColor = DefaultForeColor; }
        public virtual bool ShouldSerializeForeColor() { return (ForeColor != DefaultForeColor); }
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

        private static new Color DefaultBackColor { get { return Color.Transparent; } }
        public override void ResetBackColor() { BackColor = DefaultBackColor; }
        public virtual bool ShouldSerializeBackColor() { return (BackColor != DefaultBackColor); }
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

        private static Color DefaultUpDownBackColor { get { return Color.White; } }
        public virtual void ResetUpDownBackColor() { UpDownBackColor = DefaultUpDownBackColor; }
        public virtual bool ShouldSerializeUpDownBackColor() { return (UpDownBackColor != DefaultUpDownBackColor); }
        public override Color UpDownBackColor
        {
            get
            {
                return base.UpDownBackColor;
            }
            set
            {
                base.UpDownBackColor = value;
            }
        }

        
//        private static new Color DefaultForeColor { get { return Color.Black; } }

        /// <summary>
        /// Set the MinimumAdvanced and MaximumAdvanced to int32 or int16
        /// </summary>
        /// <param name="setHigh"></param>
        /// <param name="wtSlideSel"></param>
        public static void MinMaxAdvanced(bool setHigh, ref WTSlideSelector wtSlideSel)
        {
            if (setHigh)
            {
                wtSlideSel.MaximumAdvanced = int.MaxValue;
                wtSlideSel.MinimumAdvanced = int.MinValue;
            }
            else
            {
                wtSlideSel.MaximumAdvanced = short.MaxValue;
                wtSlideSel.MinimumAdvanced = short.MinValue;
            }
        }

        public void SetDefaultTheme()
        {
            this.Font = DefaultFont;
            this.ForeColor = DefaultForeColor;
            this.BackColor = DefaultBackColor;
            this.UpDownBackColor = DefaultUpDownBackColor;
        }

        SlideSelectorTheme _Theme;
        [DefaultValue(null)]
        public SlideSelectorTheme Theme
        {
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= new ThemeEventHandler(ThemeChanged_Callback);
                    _Theme = value;
                    if (value == null)
                    {
                        SetDefaultTheme();
                        return;
                    }
                    value.ThemeChanged += new ThemeEventHandler(ThemeChanged_Callback);
                }
                if (value != null)
                    UpdateTheme(value);
            }
            get
            {
                return _Theme;
            }
        }
        public WTSlideSelector()
        {
            this.DoubleBuffered = true;
            SetDefaultTheme();
        }
    }
}
