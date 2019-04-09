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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WillowTree
{
    // This component doesn't do anything except act as an object that
    // applications can use to retrieve the themes even if WillowTreeMain
    // isn't running.  There is an automatically created instance of it in
    // WillowTree.Services.AppThemes that plugins can use to programatically
    // select particular themes.  I hope to create a UI type editor that can
    // make it possible to select any of these themes in the designer in any
    // in any WTControl whether it is in WillowTreeMain or a custom UserControl.
    public partial class AppThemes : Component
    {
        public CustomControls.ButtonTheme buttonTheme1;
        public CustomControls.ButtonTheme Button;
        public CustomControls.CheckBoxTheme checkBoxTheme1;
        public CustomControls.CheckBoxTheme checkBoxTheme2;
        public CustomControls.ComboBoxTheme comboBoxTheme1;
        public CustomControls.ComboBoxTheme comboBoxTheme2;
        public CustomControls.GroupBoxTheme groupBoxTheme1;
        public CustomControls.GroupBoxTheme groupBoxTheme2;
        public CustomControls.LabelTheme labelTheme1;
        public CustomControls.LabelTheme labelTheme2;
        public CustomControls.ListBoxTheme listBoxTheme1;
        public CustomControls.ListBoxTheme listBoxTheme2;
        public CustomControls.MenuStripTheme menuStripTheme1;
        public CustomControls.MenuStripTheme menuStripTheme2;
        public CustomControls.NumericUpDownTheme numericUpDownTheme1;
        public CustomControls.NumericUpDownTheme numericUpDownTheme2;
        public CustomControls.SlideSelectorTheme slideSelectorTheme1;
        public CustomControls.SlideSelectorTheme slideSelectorTheme2;
        public CustomControls.TabControlTheme tabTheme1;
        public CustomControls.TabControlTheme tabTheme2;
        public CustomControls.TextBoxTheme textBoxTheme1;
        public CustomControls.TextBoxTheme textBoxTheme2;
        public CustomControls.TreeViewTheme treeViewTheme1;
        public CustomControls.TreeViewTheme treeViewTheme2;

        public AppThemes()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.buttonTheme1 = new WillowTree.CustomControls.ButtonTheme();
            this.Button = new WillowTree.CustomControls.ButtonTheme();
            this.checkBoxTheme1 = new WillowTree.CustomControls.CheckBoxTheme();
            this.checkBoxTheme2 = new WillowTree.CustomControls.CheckBoxTheme();
            this.comboBoxTheme1 = new WillowTree.CustomControls.ComboBoxTheme();
            this.comboBoxTheme2 = new WillowTree.CustomControls.ComboBoxTheme();
            this.groupBoxTheme1 = new WillowTree.CustomControls.GroupBoxTheme();
            this.groupBoxTheme2 = new WillowTree.CustomControls.GroupBoxTheme();
            this.labelTheme1 = new WillowTree.CustomControls.LabelTheme();
            this.labelTheme2 = new WillowTree.CustomControls.LabelTheme();
            this.listBoxTheme1 = new WillowTree.CustomControls.ListBoxTheme();
            this.listBoxTheme2 = new WillowTree.CustomControls.ListBoxTheme();
            this.menuStripTheme1 = new WillowTree.CustomControls.MenuStripTheme();
            this.menuStripTheme2 = new WillowTree.CustomControls.MenuStripTheme();
            this.numericUpDownTheme1 = new WillowTree.CustomControls.NumericUpDownTheme();
            this.numericUpDownTheme2 = new WillowTree.CustomControls.NumericUpDownTheme();
            this.slideSelectorTheme1 = new WillowTree.CustomControls.SlideSelectorTheme();
            this.slideSelectorTheme2 = new WillowTree.CustomControls.SlideSelectorTheme();
            this.tabTheme1 = new WillowTree.CustomControls.TabControlTheme();
            this.tabTheme2 = new WillowTree.CustomControls.TabControlTheme();
            this.textBoxTheme1 = new WillowTree.CustomControls.TextBoxTheme();
            this.textBoxTheme2 = new WillowTree.CustomControls.TextBoxTheme();
            this.treeViewTheme1 = new WillowTree.CustomControls.TreeViewTheme();
            this.treeViewTheme2 = new WillowTree.CustomControls.TreeViewTheme();
            // 
            // checkBoxTheme1
            // 
            this.checkBoxTheme1.BackColor = System.Drawing.Color.Empty;
            this.checkBoxTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // checkBoxTheme2
            // 
            this.checkBoxTheme2.BackColor = System.Drawing.Color.Transparent;
            // 
            // comboBoxTheme1
            // 
            this.comboBoxTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.comboBoxTheme1.ForeColor = System.Drawing.Color.White;
            // 
            // comboBoxTheme2
            // 
            this.comboBoxTheme2.BackColor = System.Drawing.Color.Empty;
            this.comboBoxTheme2.ForeColor = System.Drawing.Color.Empty;
            // 
            // groupBoxTheme1
            // 
            this.groupBoxTheme1.ForeColor = System.Drawing.Color.White;
            this.groupBoxTheme1.GradientColor = System.Drawing.Color.AliceBlue;
            this.groupBoxTheme1.LineColor = System.Drawing.Color.Black;
            this.groupBoxTheme1.PanelColor = System.Drawing.Color.SlateGray;
            // 
            // labelTheme1
            // 
            this.labelTheme1.ForeColor = System.Drawing.Color.White;
            // 
            // labelTheme2
            // 
            this.labelTheme2.ForeColor = System.Drawing.Color.Empty;
            // 
            // listBoxTheme1
            // 
            this.listBoxTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.listBoxTheme1.ForeColor = System.Drawing.Color.White;
            // 
            // listBoxTheme2
            // 
            this.listBoxTheme2.BackColor = System.Drawing.Color.Empty;
            this.listBoxTheme2.ForeColor = System.Drawing.Color.Empty;
            // 
            // menuStripTheme1
            // 
            this.menuStripTheme1.BackColor = System.Drawing.Color.Transparent;
            // 
            // numericUpDownTheme1
            // 
            this.numericUpDownTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.numericUpDownTheme1.ForeColor = System.Drawing.Color.White;
            // 
            // slideSelectorTheme1
            // 
            this.slideSelectorTheme1.TextBoxBackColor = System.Drawing.Color.Empty;
            // 
            // slideSelectorTheme2
            // 
            this.slideSelectorTheme2.BackColor = System.Drawing.Color.Empty;
            this.slideSelectorTheme2.ForeColor = System.Drawing.Color.Empty;
            this.slideSelectorTheme2.TextBoxBackColor = System.Drawing.Color.Empty;
            // 
            // tabTheme1
            // 
            this.tabTheme1.ForeColor = System.Drawing.Color.White;
            this.tabTheme1.SelectedGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabTheme1.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabTheme2
            // 
            this.tabTheme2.BackColor = System.Drawing.Color.Empty;
            this.tabTheme2.DisabledTextColor = System.Drawing.Color.Empty;
            this.tabTheme2.ForeColor = System.Drawing.Color.Empty;
            this.tabTheme2.LineColor = System.Drawing.Color.Empty;
            this.tabTheme2.PanelColor = System.Drawing.Color.Empty;
            this.tabTheme2.PanelGradientColor = System.Drawing.Color.Empty;
            this.tabTheme2.PanelTextColor = System.Drawing.Color.Empty;
            this.tabTheme2.SelectedColor = System.Drawing.Color.Empty;
            this.tabTheme2.SelectedGradientColor = System.Drawing.Color.Empty;
            this.tabTheme2.SelectedTextColor = System.Drawing.Color.Empty;
            // 
            // textBoxTheme1
            // 
            this.textBoxTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.textBoxTheme1.ForeColor = System.Drawing.Color.White;
            // 
            // treeViewTheme1
            // 
            this.treeViewTheme1.BackColor = System.Drawing.Color.Black;
            this.treeViewTheme1.DisabledForeColor = System.Drawing.Color.White;
            this.treeViewTheme1.ForeColor = System.Drawing.Color.White;
            this.treeViewTheme1.HighlightBackColor = System.Drawing.Color.PaleGoldenrod;
            this.treeViewTheme1.HighlightForeColor = System.Drawing.Color.Black;
            this.treeViewTheme1.InactiveBackColor = System.Drawing.Color.Gray;
            this.treeViewTheme1.InactiveForeColor = System.Drawing.Color.LightGray;
            // 
            // treeViewTheme2
            // 
            this.treeViewTheme2.DisabledForeColor = System.Drawing.SystemColors.ControlText;

        }
    }
}
