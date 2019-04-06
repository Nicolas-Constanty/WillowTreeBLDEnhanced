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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Collections;
using WillowTree.CustomControls;

namespace WillowTree.CustomControls
{
    // matt911: The managed theme stuff is something I started developing to
    // manage the themes of many controls at once, but it is not finished and
    // I dont think I've even tested it.  IThemeManageable (the interface that
    // is needed for a control to support managed themes) is implmented on the
    // WTLabel control where I was preparing to test it, but I done that yet.
    //
    // I may use this eventually or I may just scrap it.  It is experimental
    // code design which may lead me to choose another path or I may decide
    // completing it is not worth the effort.

    public interface IThemeManageable : IThemeable
    {
//        ManagedTheme GetManagedTheme();
//        string GetManagedThemeName();
        ManagedTheme ManagedTheme { get; set; }
        string ThemeName { get; set; }
//        void SetManagedTheme(ManagedTheme managedTheme);
//        void SetManagedThemeName(string themeName);
    }

    [System.ComponentModel.DesignerCategory("")]
    [DesignTimeVisible(false)]
    public class ThemeManager : Component
    {
        public ThemeManager()
        {
            InitializeComponent();
            Services.ThemeManager = this;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public void InitializeComponent()
        {
        }

        Dictionary<string, ManagedTheme> ManagedThemes = new Dictionary<string, ManagedTheme>();
        /// <summary>
        /// Registers a managed theme for management by the theme manager
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="themeName"></param>
        public void RegisterTheme(ControlTheme theme, string themeName)
        {
            ManagedThemes.Add(themeName, new ManagedTheme(theme));
        }

        /// <summary>
        /// Unregisters a managed theme and removes it from all applied controls
        /// </summary>
        /// <param name="themeName"></param>
        public void UnregisterTheme(string themeName)
        {
            ManagedTheme mtheme;
            if (ManagedThemes.TryGetValue(themeName, out mtheme))
            {
                // Set the theme to null for each control to remove its
                // eventhandler on the theme.
                foreach (IThemeManageable ctl in mtheme.Subscribers)
                {
                    ctl.ManagedTheme = null;
                }
            }

            // Now that all the subscribed controls have been unlinked
            // clear the subscription list and remove the managed theme
            mtheme.Subscribers.Clear();
            ManagedThemes.Remove(themeName);
        }

        /// <summary>
        /// Fetch a managed theme from the theme manager
        /// </summary>
        /// <param name="themeName"></param>
        /// <returns></returns>
        public ManagedTheme GetTheme(string themeName)
        {
            ManagedTheme managedTheme;
            if (ManagedThemes.TryGetValue(themeName, out managedTheme))
                return managedTheme;
            else
                return null;
        }

        /// <summary>
        /// Assign a different control theme to a managed theme 
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="theme"></param>
        public void SetTheme(string themeName, ControlTheme theme)
        {
            ManagedTheme mtheme;
            if (ManagedThemes.TryGetValue(themeName, out mtheme))
            {
                mtheme.Theme = theme;
            }
            else
                throw new ArgumentException("Tried to set unregistered theme");
        }

        /// <summary>
        /// Subscribe a control to a registered theme and apply it
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="themeName"></param>
        public void Subscribe(IThemeManageable ctl, string themeName)
        {
            if (!ManagedThemes.Keys.Contains(themeName))
            {
//                if (ctl is WTLabelManaged)
//                    RegisterTheme(new LabelTheme(), themeName);
            }

            ManagedTheme mtheme;
            if (ManagedThemes.TryGetValue(themeName, out mtheme))
            {
                ctl.ManagedTheme = mtheme;
                mtheme.Subscribe(ctl);
            }
            else
                throw new ArgumentException("Tried to subscribe unregistered theme");
        }

        //public void Subscribe(IThemeManageable ctl, ManagedTheme mtheme)
        //{
        //    ctl.SetManagedTheme(mtheme);
        //    mtheme.Subscribe(ctl);
        //}

        /// <summary>
        /// Unsubscribe a control from a registered control theme and un-apply it
        /// </summary>
        /// <param name="ctl"></param>
        public void Unsubscribe(IThemeManageable ctl)
        {
            ctl.ManagedTheme.Unsubscribe(ctl);
            ctl.ManagedTheme = null;
        }

        //public void Unsubscribe(IThemeManageable ctl, string themeName)
        //{
        //    ctl.GetThemeSubscriber().Subscribers.Remove(ctl);
        //    ctl.SetThemeSubscriber(null);
        //    ctl.SetTheme(null);
        //    ManagedTheme subscriber;
        //    if (ManagedThemes.TryGetValue(themeName, out subscriber))
        //    {
        //    }
        //    else
        //        Debug.Write("Attempted to unsubscribe to a nonexistent theme");
        //}
    }

    [System.ComponentModel.DesignerCategory("")]
    [DesignTimeVisible(false)]
    public class ManagedTheme
    {
        public ControlTheme Theme;
        public List<IThemeManageable> Subscribers = new List<IThemeManageable>();

        public void Subscribe(IThemeManageable control)
        {
            control.SetTheme(this.Theme);
            Subscribers.Add(control);
        }
        public void Unsubscribe(IThemeManageable control)
        {
            control.SetTheme(null);
            Subscribers.Remove(control);
        }

        public ManagedTheme() { }
        public ManagedTheme(ControlTheme theme)
        {
            this.Theme = theme;
        }
    }

    // A test control for managing labels.
//    [DesignTimeVisible(false)]
    [System.ComponentModel.DesignerCategory("")]
    public class WTLabelManaged : Label, IThemeable, IThemeManageable
    {
        ThemeManager _ThemeManager;
        ThemeManager ThemeManager
        {
            get { return _ThemeManager; }
            set { _ThemeManager = value; }
        }

        ManagedTheme _ManagedTheme;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagedTheme ManagedTheme
        {
            get
            {
                return _ManagedTheme;
            }
            set
            {
                if (value != _ManagedTheme)
                {
                    if (_ManagedTheme != null)
                        _ManagedTheme.Unsubscribe(this);
                    _ManagedTheme = value;
                    if (value != null)
                        value.Subscribe(this);
                }
            }
        }

        string _ThemeName;
        public string ThemeName
        {
            get
            {
                return _ThemeName;
            }
            set
            {
                if (value != _ThemeName)
                {
                    if (_ManagedTheme != null)
                        _ManagedTheme.Unsubscribe(this);
                    _ThemeName = value;
                    if (_ThemeName != null)
                        _ThemeManager.Subscribe(this, value);
                }
            }
        }

        LabelTheme _Theme;
        public LabelTheme Theme
        {
            get
            {
                return _Theme;
            }
            set
            {
                if (_Theme != value)
                {
                    if (_Theme != null)
                        _Theme.ThemeChanged -= ThemeChanged_Callback;
                    _Theme = value;
                    if (value != null)
                    {
                        value.ThemeChanged += ThemeChanged_Callback;
                        ApplyThemeColors(value);
                    }
                    else
                        RestoreDefaultColors();
                }
            }
        }

        void ThemeChanged_Callback(object sender, ThemeEventArgs e)
        {
            SetTheme(e.Theme);
        }

        public ControlTheme GetTheme()
        {
            return _Theme;
        }
        public ManagedTheme GetManagedTheme()
        {
            return _ManagedTheme;
        }

        public void SetTheme(ControlTheme theme)
        {
            if (theme != this.Theme)
                this.Theme = theme as LabelTheme;
            else
                if (this.Theme != null)
                    ApplyThemeColors(this.Theme);
                else
                    RestoreDefaultColors();
        }
        public void SetManagedTheme(ManagedTheme theme)
        {
            if (theme != this.ManagedTheme)
                this.ManagedTheme = theme;
            else
                SetTheme(theme.Theme);
        }

        public void ApplyThemeColors(LabelTheme theme)
        {
            this.Font = theme.Font;
            this.ForeColor = theme.ForeColor;
            this.BackColor = theme.BackColor;
        }
        public void RestoreDefaultColors()
        {
            Font = DefaultFont;
            ForeColor = DefaultForeColor;
            BackColor = DefaultBackColor;
        }

        public void ResetTheme()
        {
            // Don't allow the theme to reset if it is managed
            if (_ManagedTheme == null)
                return;

            Theme = null;
        }

        public bool HasTheme()
        {
            return (_Theme == null) && (_ManagedTheme == null);
        }

        public bool ShouldSerializeTheme()
        {
            return HasTheme();
        }
        public bool ShouldSerializeThemeName()
        {
            return (_ThemeName != null);
        }
        public bool ShouldSerializeFont()
        {
            return (Font != DefaultFont) && HasTheme();
        }
        public bool ShouldSerializeForeColor()
        {
            return (ForeColor != DefaultForeColor) && HasTheme();
        }
        public bool ShouldSerializeBackColor()
        {
            return (BackColor != DefaultBackColor) && HasTheme();
        }

        public WTLabelManaged()
        {
            this.DoubleBuffered = true;
            this.RestoreDefaultColors();
            this.ThemeManager = Services.ThemeManager;
            if (this.ThemeManager == null)
                throw new NullReferenceException();
        }
    }
    public class ThemeMissingException : NullReferenceException
    {
        public ThemeMissingException() : base("The theme manager could not be created.") { }
    }
}
