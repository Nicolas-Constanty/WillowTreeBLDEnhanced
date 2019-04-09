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
using System.Diagnostics;
using WillowTree.CustomControls;

namespace WillowTree
{
    /// WillowTree.Services contains shared services or data that need to be
    /// made available to all components or controls in WillowTree independently
    /// of the WillowTreeMain form.  Classes that require instancing cannot be
    /// instanced directly in a static class so I define properties that will
    /// create an instance when they are first used.  This prevents every plugin
    /// or other control that might use the service while not hosted on
    /// the WillowTreeMain form from having to know how to create and initialize
    /// it.  For example, when user controls are designed there will be no 
    /// currently running instance of WillowTreeMain to ask for the themes to 
    /// render the control colors, so those have to be shared here instead of as
    /// variables of WillowTreeMain.
    public static class Services
    {
        static private ThemeManager _ThemeManager;
        /// <summary>
        /// Single instance of the theme manager to be shared by any objects that 
        /// need it in the application. This will not change or be released until 
        /// the application terminates.
        /// </summary>
        static public ThemeManager ThemeManager
        {
            get
            {
                if (_ThemeManager == null)
                    _ThemeManager = new ThemeManager();

                return _ThemeManager;
            }
            set
            {
                if (_ThemeManager == null)
                    _ThemeManager = value;
            }
        }

        private static PluginComponentManager _PluginManager;
        /// <summary>
        /// Single instance of the plugin manager to be shared by any objects that 
        /// need it in the application. This will not change or be released until 
        /// the application terminates.
        /// </summary>
        public static PluginComponentManager PluginManager
        {
            get
            {
                if (_PluginManager == null)
                    _PluginManager = new PluginComponentManager();

                return _PluginManager;
            }
            set
            {
                if (_PluginManager == null)
                    _PluginManager = value;
            }
        }

        private static AppThemes _AppThemes;
        /// <summary>
        /// Single instance of the application themes to be shared by any objects 
        /// that need it in the application. This will not change or be released 
        /// until the application terminates.
        /// </summary>
        public static AppThemes AppThemes
        {
            get
            {
                if (_AppThemes == null)
                    _AppThemes = new AppThemes();

                return _AppThemes;
            }
            set
            {
                if (_AppThemes == null)
                    _AppThemes = value;
            }
        }

//        public LabelTheme _LTheme; 
        public static LabelTheme LTheme
        {
            get { return AppThemes.labelTheme2; }
        }
    }
}
