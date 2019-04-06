/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2011 Matthew Carter <matt911@users.sf.net>
 *  Copyright (C) 2011 XanderChaos
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using X360.STFS;
using X360.IO;

namespace WillowTree
{
    public partial class XBoxIDDialog : Form
    {
        public XBoxUniqueID ID;

        public XBoxIDDialog()
        {
            InitializeComponent();
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("sav", "");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                XBoxIDFilePath.Text = tempOpen.FileName();
                try
                {
                    ID = new XBoxUniqueID(tempOpen.FileName());
                    ProfileBox.Text = ID.ProfileID.ToString("X");
                    DeviceBox.Text = BitConverter.ToString(ID.DeviceID);
                    DeviceBox.Text = DeviceBox.Text.Replace("-", "");
                }
                catch
                {
                    ID = null;
                    MessageBox.Show("The file is not a valid Xbox 360 savegame file.");
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ID != null)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Please select a valid Xbox 360 save to use first.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
    public class XBoxUniqueID
    {
        public long ProfileID { get; private set; }
        public byte[] DeviceID { get; private set; }

        public XBoxUniqueID(string FileName)
        {
            BinaryReader br = new BinaryReader(File.Open(FileName, FileMode.Open), Encoding.ASCII);
            string Magic = new string(br.ReadChars(3));
            if (Magic != "CON")
            {
                throw new FileFormatException();
            }
            br.Close();
            br = null;

            STFSPackage CON = new STFSPackage(new DJsIO(FileName, DJFileMode.Open, true), new X360.Other.LogRecord());
            ProfileID = CON.Header.ProfileID;
            DeviceID = CON.Header.DeviceID;
            CON.CloseIO();
        }
    }
}
