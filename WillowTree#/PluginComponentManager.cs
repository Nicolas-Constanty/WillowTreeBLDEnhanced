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
using System.Windows.Forms;

namespace WillowTree
{
   public interface IPlugin
    {
        string Name { get; set; }
        void InitializePlugin(PluginComponentManager pm);
        void ReleasePlugin();
    }

    public struct PluginEvents 
    {
        /// <summary>
        /// This event handler will be called when the user has chosen a savegame
        /// file and is about to load it into WillowTree.
        /// </summary>
        public EventHandler<PluginEventArgs> GameLoading;
        /// <summary>
        /// This event handler will be called when once WillowTree has loaded 
        /// a new savegame file.
        /// </summary>
        public EventHandler<PluginEventArgs> GameLoaded;
        /// <summary>
        /// This event handler will be called when the user has chosen to save
        /// the savegame file back to disk, before the file is saved.
        /// </summary>
        public EventHandler<PluginEventArgs> GameSaving;
        /// <summary>
        /// This event handler will be called when a savegame file is finished
        /// saving to disk.
        /// </summary>
        public EventHandler<PluginEventArgs> GameSaved;
        /// <summary>
        /// This event handler will be called each time the plugin becomes
        /// selected for display, typically when its tab page comes into view.
        /// </summary>
        public EventHandler<PluginEventArgs> PluginSelected;
        /// <summary>
        ///  This event handler will be called to let the plugin know it is
        ///  is no longer in view, typically when a different tab page becomes
        ///  selected.
        /// </summary>
        public EventHandler<PluginEventArgs> PluginUnselected;

        public EventHandler<PluginCommandEventArgs> PluginCommand;
    }
    
    public class PluginComponentManager
    {
        private event EventHandler<PluginEventArgs> GameLoading;
        private event EventHandler<PluginEventArgs> GameLoaded;
        private event EventHandler<PluginEventArgs> GameSaving;
        private event EventHandler<PluginEventArgs> GameSaved;

        private Dictionary<IPlugin, PluginEvents> pluginEventTable = new Dictionary<IPlugin, PluginEvents>();

        /// <summary>
        /// The application calls this to execute the GameLoading event handler 
        /// of all registered plugins
        /// </summary>
        public void OnGameLoading(PluginEventArgs e)
        {
            if (GameLoading != null)
                GameLoading(this, e);
        }

        /// <summary>
        /// The application calls this to execute the GameLoaded event handler 
        /// of all registered plugins
        /// </summary>
        public void OnGameLoaded(PluginEventArgs e)
        {
            if (GameLoaded != null)
                GameLoaded(this, e);
        }

        /// <summary>
        /// The application calls this to execute the GameSaving event handler 
        /// of all registered plugins
        /// </summary>
        public void OnGameSaving(PluginEventArgs e)
        {
            if (GameSaving != null)
                GameSaving(this, e);
        }

        /// <summary>
        /// The application calls this to execute the GameSaved event handler 
        /// of all registered plugins
        /// </summary>
        public void OnGameSaved(PluginEventArgs e)
        {
            if (GameSaved != null)
                GameSaved(this, e);
        }

        /// <summary>
        /// The application calls this to signal a plugin when it is selected
        /// or otherwise being displayed to the user.  This could be called when 
        /// the plugin's tab page is selected so that it can update its controls
        /// to reflect data that may have changed while the plugin was hidden.
        /// </summary>
        public void OnPluginSelected(IPlugin plugin, PluginEventArgs e)
        {
            PluginEvents functions;
            if (pluginEventTable.TryGetValue(plugin, out functions) == true)
            {
                if (functions.PluginSelected != null)
                    functions.PluginSelected(this, e);
            }
        }

        /// <summary>
        /// The application calls this to tell a plugin when it is no longer
        /// being displayed or otherwise selected for input.  If a plugin
        /// uses GUI events like animation timers, then it can
        /// detach them until it is selected again.
        /// </summary>
        public void OnPluginUnselected(IPlugin plugin, PluginEventArgs e)
        {
            PluginEvents functions;
            if (pluginEventTable.TryGetValue(plugin, out functions) == true)
            {
                if (functions.PluginUnselected != null)
                    functions.PluginUnselected(this, e);
            }
        }

        public void OnPluginCommand(IPlugin plugin, PluginCommandEventArgs e)
        {
            PluginEvents functions;
            if (pluginEventTable.TryGetValue(plugin, out functions) == true)
            {
                if (functions.PluginCommand!= null)
                    functions.PluginCommand(this, e);
            }
        }

        /// <summary>
        /// This calls a plugin's InitializePlugin method to let 
        /// it know that the application is ready and it can initialize.
        /// When the plugin initializes it should call RegisterPlugin 
        /// to subscribe to plugin events.
        /// </summary>
        public void InitializePlugin(IPlugin plugin)
        {
            plugin.InitializePlugin(this);
        }

        /// <summary>
        /// Plugins call this to register for plugin events.  Typically it will
        /// be called in the plugin's InitializePlugin method.  If a particular
        /// event is not needed it can be left null in the PluginEvents structure.
        /// </summary>
        public void RegisterPlugin(IPlugin plugin, PluginEvents eventHandlers)
        {
            // Store a list of the event handlers so they can be detached later
            pluginEventTable.Add(plugin, eventHandlers);

            if (eventHandlers.GameLoading != null)
                GameLoading += eventHandlers.GameLoading;
            if (eventHandlers.GameLoaded != null)
                GameLoaded += eventHandlers.GameLoaded;
            if (eventHandlers.GameSaving != null)
                GameSaving += eventHandlers.GameSaving;
            if (eventHandlers.GameSaved != null)
                GameSaved += eventHandlers.GameSaved;
        }

        private void DetachEvents(PluginEvents eventHandlers)
        {
            if (eventHandlers.GameLoading != null)
                GameLoading -= eventHandlers.GameLoading;
            if (eventHandlers.GameLoaded != null)
                GameLoaded -= eventHandlers.GameLoaded;
            if (eventHandlers.GameSaving != null)
                GameSaving -= eventHandlers.GameSaving;
            if (eventHandlers.GameSaved != null)
                GameSaved -= eventHandlers.GameSaved;
        }

        public IPlugin GetPlugin(Type pluginType)
        {
            foreach (IPlugin plugin in pluginEventTable.Keys)
            {
                if (plugin.GetType() == pluginType)
                    return plugin;
            }
            return null;
        }

        public void UnregisterPlugin(IPlugin plugin)
        {
            // Retrieve the list of event handlers to detach
            PluginEvents eventHandlers;
            if (pluginEventTable.TryGetValue(plugin, out eventHandlers) == true)
            {
                DetachEvents(eventHandlers);
                pluginEventTable.Remove(plugin);
                plugin.ReleasePlugin();
            }
        }

        public void UnregisterAllPlugins()
        {
            foreach (KeyValuePair<IPlugin, PluginEvents> kvp in pluginEventTable)
            {
                DetachEvents(kvp.Value);
                kvp.Key.ReleasePlugin();
            }
            pluginEventTable.Clear();
        }    
    }

    public class PluginEventArgs : EventArgs
    {
        public PluginEventArgs()
        { }

        public PluginEventArgs(WillowTreeMain willowTreeMain, string filename)
        {
            _Filename = filename;
            _WTM = willowTreeMain;
        }

        // These fields are made into properties to make them read-only and
        // prevent the plugins from changing them.
 
        WillowTreeMain _WTM;
        public WillowTreeMain WTM
        {
            get { return _WTM; }
        }

        private string _Filename;
        public string Filename
        {
            get { return _Filename; }
        } 
    }

    public enum PluginCommand : int
    {
        ChangeSortMode,
        IncreaseNavigationDepth,
    }

    public class PluginCommandEventArgs : EventArgs
    {
        public PluginCommandEventArgs()
        { }

        public PluginCommandEventArgs(WillowTreeMain willowTreeMain, PluginCommand command)
        {
            _Command = command;
            _WTM = willowTreeMain;
        }

        // These fields are made into properties to make them read-only and
        // prevent the plugins from changing them.

        WillowTreeMain _WTM;
        public WillowTreeMain WTM
        {
            get { return _WTM; }
        }

        private PluginCommand _Command;
        public PluginCommand Command
        {
            get { return _Command; }
        }
    }
}
