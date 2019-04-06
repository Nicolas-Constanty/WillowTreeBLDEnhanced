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
using System.ComponentModel;

namespace WillowTree.CustomControls
{
    public interface IThemeable
    {
        void SetTheme(ControlTheme theme);
//        ControlTheme GetTheme();
    }

    // All of the themes are currently marked with the attribute
    // [DesignTimeVisible(true)] so they won't show up in the toolbar or in
    // at the bottom of the form window if you have some on the form.  If you 
    // want to see them, then just change the false to true temporarily and 
    // compile then you will see the components in the toolbox and main form.

    [DesignTimeVisible(true)]
    [System.ComponentModel.DesignerCategory("")]
    public class ControlTheme : Component
    {
        Font _Font;
        Color _ForeColor;
        Color _BackColor;

        bool FontsAreIdentical(Font font1, Font font2)
        {
            return (font1.Name == font2.Name &&
                font1.Size == font2.Size &&
                font1.Unit == font2.Unit &&
                font1.Bold == font2.Bold &&
                font1.GdiCharSet == font2.GdiCharSet &&
                font1.GdiVerticalFont == font2.GdiVerticalFont &&
                font1.Italic == font2.Italic &&
                font1.Strikeout == font2.Strikeout &&
                font1.Underline == font2.Underline);
        }

        bool bFontChanged = false;

        [Browsable(false)]
        public virtual Font DefaultFont
        {
            get { return SystemFonts.DefaultFont; }
        }
        public virtual void ResetFont()
        {
            bFontChanged = false;
            Font = DefaultFont;
        }
        public virtual bool ShouldSerializeFont()
        {
            return bFontChanged;
//            return !FontsAreIdentical(this.Font, DefaultFont);
        }

        public virtual Font Font
        {
            set
            {
                if (_Font != value)
                {
                    _Font = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _Font;
            }
        }

        [Browsable(false)]
        public virtual Color DefaultForeColor
        {
            get { return Color.Black; }
        }
        public virtual void ResetForeColor()
        {
            ForeColor = DefaultForeColor;
        }
        public virtual bool ShouldSerializeForeColor()
        {
            return (ForeColor != DefaultForeColor);
        }
        public virtual Color ForeColor
        {
            set
            {
                if (_ForeColor != value)
                {
                    _ForeColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _ForeColor;
            }
        }

        [Browsable(false)]
        public virtual Color DefaultBackColor
        {
            get { return Color.White; }
        }
        public virtual void ResetBackColor()
        {
            BackColor = DefaultBackColor;
        }
        public virtual bool ShouldSerializeBackColor()
        {
            return (BackColor != DefaultBackColor);
        }
        public virtual Color BackColor
        {
            set
            {
                if (_BackColor != value)
                {
                    _BackColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _BackColor;
            }
        }

        public event ThemeEventHandler ThemeChanged;
        protected virtual void OnThemeChanged(ThemeEventArgs e)
        {
            if (ThemeChanged != null)
                ThemeChanged(this, e);
        }

        public ControlTheme()
        {
            _ForeColor = DefaultForeColor;
            _BackColor = DefaultBackColor;
            _Font = DefaultFont;
        }
    }

    public delegate void ThemeEventHandler(object sender, ThemeEventArgs e);
    public class ThemeEventArgs : EventArgs
    {
        public ControlTheme Theme { get; set; }
        public ThemeEventArgs() { }
        public ThemeEventArgs(ControlTheme theme)
        {
            Theme = theme;
        }
    }

    [Serializable]
    [DesignTimeVisible(true)]
    public class ButtonTheme : ControlTheme { }

    [Serializable]
    [DesignTimeVisible(true)]
    public class CheckBoxTheme : ControlTheme { }

    [Serializable]
    [DesignTimeVisible(true)]
    public class ComboBoxTheme : ControlTheme { }

    [Serializable]
    [DesignTimeVisible(true)]
    public class GroupBoxTheme : ControlTheme
    {
        private Color _GradientColor;
        private Color _LineColor;
        private Color _PanelColor;

        public override void ResetBackColor() { BackColor = WTGroupBox.DefaultBackColor; }
        public virtual void ResetGradientColor() { GradientColor = WTGroupBox.DefaultGradientColor; }
        public virtual void ResetLineColor() { LineColor = WTGroupBox.DefaultLineColor; }
        public virtual void ResetPanelColor() { PanelColor = WTGroupBox.DefaultPanelColor; }

        public override bool ShouldSerializeBackColor() { return BackColor != WTGroupBox.DefaultBackColor; }
        public virtual bool ShouldSerializeGradientColor() { return GradientColor != WTGroupBox.DefaultGradientColor; }
        public virtual bool ShouldSerializeLineColor() { return LineColor != WTGroupBox.DefaultLineColor; }
        public virtual bool ShouldSerializePanelColor() { return PanelColor != WTGroupBox.DefaultPanelColor; }

        public virtual Color GradientColor
        {
            set
            {
                if (_GradientColor != value)
                {
                    _GradientColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _GradientColor;
            }
        }
        public virtual Color LineColor
        {
            set
            {
                if (_LineColor != value)
                {
                    _LineColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _LineColor;
            }
        }
        public virtual Color PanelColor
        {
            set
            {
                if (_PanelColor != value)
                {
                    _PanelColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _PanelColor;
            }
        }

        public GroupBoxTheme()
        {
            ResetFont();
            ResetForeColor();
            ResetBackColor();
            ResetGradientColor();
            ResetLineColor();
            ResetPanelColor();
        }
    } 

    [Serializable]
    [DesignTimeVisible(true)]
    public class LabelTheme : ControlTheme 
    { 
        public override Color DefaultBackColor { get { return Color.Transparent; } } 
    }

    [Serializable]
    [DesignTimeVisible(true)]
    public class ListBoxTheme : ControlTheme { }

    [Serializable]
    [DesignTimeVisible(true)]
    public class MenuStripTheme : ControlTheme 
    {
        public override Color DefaultBackColor
        {
            get
            {
                return Color.FromArgb(188, 212, 249);
            }
        }
    }
    [Serializable]
    [DesignTimeVisible(true)]
    public class NumericUpDownTheme : ControlTheme { }

    [Serializable]
    [DesignTimeVisible(true)]
    public class PanelTheme : ControlTheme { }

    [Serializable]
    [DesignTimeVisible(true)]
    public class SlideSelectorTheme : ControlTheme 
    {
        public override Color DefaultBackColor
        {
            get
            {
                return Color.Transparent;
            }
        }

        [Browsable(false)]
        public virtual Color DefaultTextBoxBackColor
        {
            get { return Color.White; }
        }
        public virtual void ResetTextBoxBackColor()
        {
            TextBoxBackColor = DefaultTextBoxBackColor;
        }
        public virtual bool ShouldSerializeTextBoxBackColor()
        {
            return (TextBoxBackColor != DefaultTextBoxBackColor);
        }
        private Color _TextBoxBackColor;
        public virtual Color TextBoxBackColor
        {
            set
            {
                if (_TextBoxBackColor != value)
                {
                    _TextBoxBackColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _TextBoxBackColor;
            }
        }  
    }

    [Serializable]
    [DesignTimeVisible(true)]
    public class TabControlTheme : ControlTheme
    {
        public override Color DefaultBackColor { get { return Color.FromArgb(83, 83, 83); } }

        Color _LineColor;
        public virtual Color LineColor
        {
            get
            {
                return _LineColor;
            }
            set
            {
                if (_LineColor != value)
                {
                    _LineColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
        }
        public virtual void ResetLineColor() { LineColor = WTTabControl.DefaultLineColor; }
        public virtual bool ShouldSerializeLineColor() { return LineColor != WTTabControl.DefaultLineColor; }
            
        Color _DisabledTextColor;
        public Color DisabledTextColor
        {
            set
            {
                if (_DisabledTextColor != value)
                {
                    _DisabledTextColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _DisabledTextColor;
            }
        }
        public virtual void ResetDisabledTextColor() { DisabledTextColor = WTTabControl.DefaultDisabledTextColor; }
        public virtual bool ShouldSerializeDisabledTextColor() { return DisabledTextColor != WTTabControl.DefaultDisabledTextColor; }

        Color _PanelColor;
        public Color PanelColor
        {
            set
            {
                if (_PanelColor != value)
                {
                    _PanelColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _PanelColor;
            }
        }
        public virtual void ResetPanelColor() { PanelColor = WTTabControl.DefaultPanelColor; }
        public virtual bool ShouldSerializePanelColor() { return PanelColor != WTTabControl.DefaultPanelColor; }

        Color _PanelGradientColor;
        public Color PanelGradientColor
        {
            set
            {
                if (_PanelGradientColor != value)
                {
                    _PanelGradientColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _PanelGradientColor;
            }
        }
        public virtual void ResetPanelGradientColor() { PanelGradientColor = WTTabControl.DefaultGradientColor; }
        public virtual bool ShouldSerializePanelGradientColor() { return PanelGradientColor != WTTabControl.DefaultGradientColor; }

        Color _PanelTextColor;
        public Color PanelTextColor
        {
            set
            {
                if (_PanelTextColor != value)
                {
                    _PanelTextColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _PanelTextColor;
            }
        }
        public virtual void ResetPanelTextColor() { PanelTextColor = WTTabControl.DefaultForeColor; }
        public virtual bool ShouldSerializePanelTextColor() { return PanelTextColor != WTTabControl.DefaultForeColor; }

        Color _SelectedColor;
        public Color SelectedColor
        {
            set
            {
                if (_SelectedColor != value)
                {
                    _SelectedColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _SelectedColor;
            }
        }
        public virtual void ResetSelectedColor() { SelectedColor = WTTabControl.DefaultSelectedPanelColor; }
        public virtual bool ShouldSerializeSelectedColor() { return SelectedColor != WTTabControl.DefaultSelectedPanelColor; }

        Color _SelectedGradientColor;
        public Color SelectedGradientColor
        {
            set
            {
                if (_SelectedGradientColor != value)
                {
                    _SelectedGradientColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _SelectedGradientColor;
            }
        }
        public virtual void ResetSelectedGradientColor() { SelectedGradientColor = WTTabControl.DefaultGradientColor; }
        public virtual bool ShouldSerializeSelectedGradientColor() { return SelectedGradientColor != WTTabControl.DefaultGradientColor; }

        Color _SelectedTextColor;
        public Color SelectedTextColor
        {
            set
            {
                if (_SelectedTextColor != value)
                {
                    _SelectedTextColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _SelectedTextColor;
            }
        }
        public virtual void ResetSelectedTextColor() { SelectedTextColor = WTTabControl.DefaultSelectedTextColor; }
        public virtual bool ShouldSerializeSelectedTextColor() { return SelectedTextColor != WTTabControl.DefaultSelectedTextColor; }

        public TabControlTheme() : base()
        {
            ResetBackColor();
            ResetLineColor();
            ResetDisabledTextColor();
            ResetPanelColor();
            ResetPanelGradientColor();
            ResetPanelTextColor();
            ResetSelectedColor();
            ResetSelectedGradientColor();
            ResetSelectedTextColor();
        }
    }
    [Serializable]
    [DesignTimeVisible(true)]
    public class TextBoxTheme : ControlTheme { }
    [Serializable]
    [DesignTimeVisible(true)]
    public class TreeViewTheme : ControlTheme 
    {
        // background color of unselected nodes
        private Color _BackColor;
        public override Color BackColor
        {
            set
            {
                if (_BackColor != value)
                {
                    _BackColor = value;
                    _BackBrush = new SolidBrush(_BackColor);
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _BackColor;
            }
        }

        private Brush _BackBrush;
        [Browsable(false)]
        public Brush BackBrush
        {
            set
            {
                if (_BackBrush != value)
                {
                    _BackBrush = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _BackBrush;
            }
        }

        // foreground color of selected nodes when the control is focused
        [Browsable(false)]
        public virtual Color DefaultHighlightForeColor
        {
            get { return Color.White; }
        }
        public virtual void ResetHighlightForeColor()
        {
            HighlightForeColor = DefaultHighlightForeColor;
        }
        public virtual bool ShouldSerializeHighlightForeColor()
        {
            return (HighlightForeColor != DefaultHighlightForeColor);
        }
        private Color _HighlightForeColor;
        public virtual Color HighlightForeColor
        {
            set
            {
                if (_HighlightForeColor != value)
                {
                    _HighlightForeColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _HighlightForeColor;
            }
        }

        // background color of selected nodes when the control is focused
        [Browsable(false)]
        public virtual Color DefaultHighlightBackColor
        {
            get { return Color.FromArgb(49, 106, 197); }
        }
        public virtual void ResetHighlightBackColor()
        {
            HighlightBackColor = DefaultHighlightBackColor;
        }
        public virtual bool ShouldSerializeHighlightBackColor()
        {
            return (HighlightBackColor != DefaultHighlightBackColor);
        }
        private Color _HighlightBackColor;
        public virtual Color HighlightBackColor
        {
            set
            {
                if (_HighlightBackColor != value)
                {
                    _HighlightBackColor = value;
                    _HighlightBackBrush = new SolidBrush(_HighlightBackColor);
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _HighlightBackColor;
            }
        }

        Brush _HighlightBackBrush;
        [Browsable(false)]
        public Brush HighlightBackBrush
        {
            set
            {
                if (_HighlightBackBrush != value)
                {
                    _HighlightBackBrush = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _HighlightBackBrush;
            }
        }

        // foreground color of selected nodes when the control is not focused
        [Browsable(false)]
        public virtual Color DefaultInactiveForeColor
        {
            get { return Color.Black; }
        }
        public virtual void ResetInactiveForeColor()
        {
            InactiveForeColor = DefaultInactiveForeColor;
        }
        public virtual bool ShouldSerializeInactiveForeColor()
        {
            return (InactiveForeColor != DefaultInactiveForeColor);
        }
        Color _InactiveForeColor;
        public virtual Color InactiveForeColor
        {
            set
            {
                if (_InactiveForeColor != value)
                {
                    _InactiveForeColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _InactiveForeColor;
            }
        }

        // background color of selected nodes when the control is not focused
        [Browsable(false)]
        public virtual Color DefaultInactiveBackColor
        {
            get { return Color.FromArgb(212, 208, 200); }
        }
        public virtual void ResetInactiveBackColor()
        {
            InactiveBackColor = DefaultInactiveBackColor;
        }
        public virtual bool ShouldSerializeInactiveBackColor()
        {
            return (InactiveBackColor != DefaultInactiveBackColor);
        }
        private Color _InactiveBackColor;
        public virtual Color InactiveBackColor
        {
            set
            {
                if (_InactiveBackColor != value)
                {
                    _InactiveBackColor = value;
                    _InactiveBackBrush = new SolidBrush(_InactiveBackColor);
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _InactiveBackColor;
            }
        }

        Brush _InactiveBackBrush;        
        [Browsable(false)]
        public Brush InactiveBackBrush
        {
            set
            {
                if (_InactiveBackBrush != value)
                {
                    _InactiveBackBrush = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _InactiveBackBrush;
            }
        }

        // foreground color of nodes when the control is disabled
        [Browsable(false)]
        public virtual Color DefaultDisabledForeColor
        {
            get { return Color.FromArgb(128, 128, 128); }
        }
        public virtual void ResetDisabledForeColor()
        {
            DisabledForeColor = DefaultDisabledForeColor;
        }
        public virtual bool ShouldSerializeDisabledForeColor()
        {
            return (DisabledForeColor != DefaultDisabledForeColor);
        }
        Color _DisabledForeColor;
        public virtual Color DisabledForeColor
        {
            set
            {
                if (_DisabledForeColor != value)
                {
                    _DisabledForeColor = value;
                    OnThemeChanged(new ThemeEventArgs(this));
                }
            }
            get
            {
                return _DisabledForeColor;
            }
        }

        public TreeViewTheme() : base()
        {
            this.ForeColor = DefaultForeColor;
            this.HighlightForeColor = DefaultHighlightForeColor;
            this.InactiveForeColor = DefaultInactiveForeColor;
            this.DisabledForeColor = DefaultInactiveForeColor;
            this.BackColor = DefaultBackColor;
            this.InactiveBackColor = DefaultInactiveBackColor;
            this.HighlightBackColor = DefaultHighlightBackColor;
        }
    }

    [DesignTimeVisible(true)]
    [System.ComponentModel.DesignerCategory("")]
    public class ThemeGroup : Component
    {
        public TabControlTheme TabControl { set; get; }
        public TreeViewTheme TreeView { set; get; }
        public LabelTheme Label { set; get; }
        public TextBoxTheme TextBox { set; get; }
        public NumericUpDownTheme NumericUpDown { set; get; }
        public ComboBoxTheme ComboBox { set; get; }
        public ButtonTheme Button { set; get; }
        public CheckBoxTheme CheckBox { set; get; }
        public ListBoxTheme ListBox { set; get; }
        public GroupBoxTheme GroupBox { set; get; }
        public SlideSelectorTheme SlideSelector { set; get; }
    }

        //public Dictionary<Type, ControlTheme> ThemeDict;

        //public void RegisterTheme(Object obj)
        //{
        //    if (ThemeDict.TryGetValue(obj.GetType(), out themeValue)
        //    {

        //public ControlTheme GetTheme(Type themeType)
        //{
        //    return ThemeDict[themeType];
        //}

}
