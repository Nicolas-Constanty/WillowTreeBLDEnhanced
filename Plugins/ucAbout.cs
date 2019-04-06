/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2011 Matthew Carter <matt911@users.sf.net>
 *  Copyright (C) 2010, 2011 XanderChaos
 *  Copyright (C) 2011 Thomas Kaiser
 *  Copyright (C) 2010 JackSchitt
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
using System.Net;
#if !DEBUG
using System.Threading;
#endif

namespace WillowTree.Plugins
{
    public partial class ucAbout : UserControl, IPlugin
    {
        string DownloadURLFromServer;
        string VersionFromServer;

        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.PluginSelected = OnPluginSelected;
            events.PluginUnselected = OnPluginUnselected;
            pm.RegisterPlugin(this, events);

#if !DEBUG
            // Only check for new version if it's not a debug build.
            ThreadPool.QueueUserWorkItem(CheckVersion);
#endif
            UpdateButton.Hide();        
        }

        public void ReleasePlugin() 
        {
        }

        public ucAbout()
        {
            InitializeComponent();
        }

        public void OnPluginSelected(Object sender, PluginEventArgs e)
        {
            if (VersionFromServer == null)
                timer1.Enabled = true;
        }

        public void OnPluginUnselected(Object sender, PluginEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void CheckVerPopup()
        {
            if (VersionFromServer != Util.GetVersion() && VersionFromServer != "" && VersionFromServer != null)
            {
                UpdateButton.Text = "Version " + VersionFromServer + " is now available! Click here to download.";
                UpdateButton.Show();
            }
        }

        //Recovers the latest version from the sourceforge server.
        public void CheckVersion(object state)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string VersionTextFromServer = webClient.DownloadString("http://willowtree.sourceforge.net/version.txt");
                    string[] RemoteVersionInfo = VersionTextFromServer.Replace("\r\n", "\n").Split('\n');
                    if ((RemoteVersionInfo.Count() > 1) || (RemoteVersionInfo.Count() <= 3))
                    {
                        VersionFromServer = RemoteVersionInfo[0];
                        DownloadURLFromServer = RemoteVersionInfo[1];
                    }
                }
            }
            catch { }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + DownloadURLFromServer);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (VersionFromServer != null)
            {
                timer1.Enabled = false;
                CheckVerPopup();
            }
        }
    }
}
