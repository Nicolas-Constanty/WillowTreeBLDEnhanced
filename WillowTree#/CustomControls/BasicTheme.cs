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
//using System.Linq;
using System.ComponentModel;
//using System.Windows.Forms;

namespace WillowTree
{
    [DesignTimeVisible(false)]
    [System.ComponentModel.DesignerCategory("")]
    public class BasicTheme : Component
    {
        public CustomControls.CheckBoxTheme checkBoxTheme1;
        public CustomControls.ComboBoxTheme comboBoxTheme1;
        public CustomControls.GroupBoxTheme groupBoxTheme1;
        public CustomControls.LabelTheme labelTheme1;
        public CustomControls.ListBoxTheme listBoxTheme1;
        public CustomControls.NumericUpDownTheme numericUpDownTheme1;
        public CustomControls.SlideSelectorTheme slideSelectorTheme1;
        public CustomControls.TabControlTheme tabTheme1;
        public CustomControls.TextBoxTheme textBoxTheme1;
        public CustomControls.TreeViewTheme treeViewTheme1;
        public CustomControls.ButtonTheme buttonTheme1;

        private void InitializeComponent()
        {
            this.buttonTheme1 = new WillowTree.CustomControls.ButtonTheme();
            this.checkBoxTheme1 = new WillowTree.CustomControls.CheckBoxTheme();
            this.comboBoxTheme1 = new WillowTree.CustomControls.ComboBoxTheme();
            this.groupBoxTheme1 = new WillowTree.CustomControls.GroupBoxTheme();
            this.labelTheme1 = new WillowTree.CustomControls.LabelTheme();
            this.listBoxTheme1 = new WillowTree.CustomControls.ListBoxTheme();
            this.numericUpDownTheme1 = new WillowTree.CustomControls.NumericUpDownTheme();
            this.slideSelectorTheme1 = new WillowTree.CustomControls.SlideSelectorTheme();
            this.tabTheme1 = new WillowTree.CustomControls.TabControlTheme();
            this.textBoxTheme1 = new WillowTree.CustomControls.TextBoxTheme();
            this.treeViewTheme1 = new WillowTree.CustomControls.TreeViewTheme();
            // 
            // buttonTheme1
            // 
            this.buttonTheme1.BackColor = System.Drawing.Color.Empty;
            this.buttonTheme1.Font = null;
            this.buttonTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // checkBoxTheme1
            // 
            this.checkBoxTheme1.BackColor = System.Drawing.Color.Empty;
            this.checkBoxTheme1.Font = null;
            this.checkBoxTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // comboBoxTheme1
            // 
            this.comboBoxTheme1.BackColor = System.Drawing.Color.Empty;
            this.comboBoxTheme1.Font = null;
            this.comboBoxTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // groupBoxTheme1
            // 
            this.groupBoxTheme1.BackColor = System.Drawing.Color.Empty;
            this.groupBoxTheme1.LineColor = System.Drawing.Color.Empty;
            this.groupBoxTheme1.Font = null;
            this.groupBoxTheme1.ForeColor = System.Drawing.Color.Empty;
            this.groupBoxTheme1.GradientColor = System.Drawing.Color.Empty;
            this.groupBoxTheme1.PanelColor = System.Drawing.Color.Empty;
            // 
            // labelTheme1
            // 
            this.labelTheme1.BackColor = System.Drawing.Color.Empty;
            this.labelTheme1.Font = null;
            this.labelTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // listBoxTheme1
            // 
            this.listBoxTheme1.BackColor = System.Drawing.Color.Empty;
            this.listBoxTheme1.Font = null;
            this.listBoxTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // numericUpDownTheme1
            // 
            this.numericUpDownTheme1.BackColor = System.Drawing.Color.Empty;
            this.numericUpDownTheme1.Font = null;
            this.numericUpDownTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // slideSelectorTheme1
            // 
            this.slideSelectorTheme1.BackColor = System.Drawing.Color.Empty;
            this.slideSelectorTheme1.Font = null;
            this.slideSelectorTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // tabTheme1
            // 
            this.tabTheme1.BackColor = System.Drawing.Color.Empty;
            this.tabTheme1.DisabledTextColor = System.Drawing.Color.Empty;
            this.tabTheme1.Font = null;
            this.tabTheme1.ForeColor = System.Drawing.Color.Empty;
            this.tabTheme1.PanelGradientColor = System.Drawing.Color.Empty;
            this.tabTheme1.LineColor = System.Drawing.Color.Empty;
            this.tabTheme1.SelectedGradientColor = System.Drawing.Color.Empty;
            this.tabTheme1.PanelTextColor = System.Drawing.Color.Empty;
            this.tabTheme1.PanelColor = System.Drawing.Color.Empty;
            this.tabTheme1.SelectedColor = System.Drawing.Color.Empty;
            this.tabTheme1.SelectedTextColor = System.Drawing.Color.Empty;
            // 
            // textBoxTheme1
            // 
            this.textBoxTheme1.BackColor = System.Drawing.Color.Empty;
            this.textBoxTheme1.Font = null;
            this.textBoxTheme1.ForeColor = System.Drawing.Color.Empty;
            // 
            // treeViewTheme1
            // 
            this.treeViewTheme1.BackColor = System.Drawing.Color.Black;
            this.treeViewTheme1.DisabledForeColor = System.Drawing.SystemColors.ControlText;
            this.treeViewTheme1.Font = null;
            this.treeViewTheme1.ForeColor = System.Drawing.Color.White;

        }
    }
}
