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
//using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel;
using System.ComponentModel.Design;
//using System.Diagnostics;
using System.ComponentModel.Design.Serialization;

namespace WillowTree.CustomControls
{
    // matt911: This custom designer is needed to control the custom tab
    // control so that when you click on the tabs the various pages show
    // in the designer and you can drag controls onto them.  This is the first
    // control designer I've ever written.  It probably has bugs, but I got it 
    // to do enough to be able to use it for WT#.

    /// <summary>
    /// The default control designer for CCTabControl
    /// </summary>
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    class CCTabControlDesigner : ParentControlDesigner
    {
        ISelectionService _selectionSvc;
        IComponentChangeService _componentChangeSvc;

        bool _selected = false;

        private Adorner tabControlAdorner;
        List<Glyph> _tabGlyphs = new List<Glyph>();

        DesignerVerbCollection _verbs;

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                Control.Layout -= this.Control_Layout;
            }
            if (disposing && tabControlAdorner != null)
            {
                BehaviorService b = BehaviorService;
                if (b != null)
                {
                    b.Adorners.Remove(tabControlAdorner);
                }
                base.Dispose(disposing);
            }
        }

        // GetHitTest decides when clicks should go through to the control.  If 
        // it returns true then any mouse actions should be directed to the 
        // control.
        protected override bool GetHitTest(Point point)
        {
            return base.GetHitTest(point);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            // Add the custom set of glyphs using the BehaviorService. 
            // Glyphs live on adornders.
            tabControlAdorner = new Adorner();
            BehaviorService.Adorners.Add(tabControlAdorner);

            _componentChangeSvc = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            if (_componentChangeSvc == null)
                throw new NotImplementedException("Could not locate the selection service interface");

            _selectionSvc = this.GetService(typeof(ISelectionService)) as ISelectionService;
            if (_selectionSvc == null)
                throw new NotImplementedException("Could not locate the selection service interface");

            _selectionSvc.SelectionChanged += new EventHandler(this.OnSelectionChanged);
            Control.Layout += this.Control_Layout;
        }

        void DestroyTabGlyphs()
        {
            foreach (Glyph g in _tabGlyphs)
            {
                if (g != null)
                    tabControlAdorner.Glyphs.Remove(g);
            }
            _tabGlyphs.Clear();
        }

        void CreateTabGlyphs()
        {
            CCTabControl tabControl = Control as CCTabControl;
            int count = Control.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                Point offset = BehaviorService.ControlToAdornerWindow(Control);
                Rectangle rect = tabControl.GetTabRect(i);
                if (rect != Rectangle.Empty)
                {
                    rect.Offset(offset);
                    Glyph g = new ControlBodyGlyph(rect, Cursors.Hand, Control, new TabBehavior(this));
                    _tabGlyphs.Add(g);
                    tabControlAdorner.Glyphs.Add(g);
                }
                else
                    _tabGlyphs.Add(null);
            }
        }

        void Control_Layout(object sender, LayoutEventArgs e)
        {
            // If the control is selected then the tab glyphs have to be rebuilt.
            // If the control is not selected then there are no tab glyphs defined.
            if (_selected)
            {
                DestroyTabGlyphs();
                CreateTabGlyphs();
            }
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (this._verbs == null)
                {
                    this._verbs = new DesignerVerbCollection();
                    this._verbs.Add(new DesignerVerb("Add Panel", new EventHandler(this.OnAddPanel)));
                    this._verbs.Add(new DesignerVerb("Remove Panel", new EventHandler(this.OnRemovePanel)));
                    //                    this._verbs.Add(new DesignerVerb("Rename Panel", new EventHandler(this.OnRenamePanel)));
                }
                return this._verbs;
            }
        }

        private void OnAddPanel(object sender, EventArgs e)
        {
            CCTabControl tabCtl = Control as CCTabControl;
            IDesignerHost _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
            IComponentChangeService componentChangeSvc = GetService(typeof(IComponentChangeService)) as IComponentChangeService;

            DesignerTransaction _designerTransaction = _designerHost.CreateTransaction("Add Panel");

            Control panel = _designerHost.CreateComponent(tabCtl.GetDefaultPageType()) as Control;
            panel.Text = panel.Name;
            if (panel != null)
            {
                componentChangeSvc.OnComponentChanging(Control, null);
                Control.Controls.Add(panel);
                componentChangeSvc.OnComponentChanged(Control, null, null, null);
            }
            _designerTransaction.Commit();
        }

        private void OnRemovePanel(object sender, EventArgs e)
        {
            //try
            //{
            CCTabControl tabCtl = Control as CCTabControl;
            IDesignerHost _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
            IComponentChangeService componentChangeSvc = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            using (DesignerTransaction _designerTransaction = _designerHost.CreateTransaction("Add Panel"))
            {
                int tabIndex = tabCtl.SelectedIndex;
                if ((tabIndex < 0) || (tabIndex >= tabCtl.TabPages.Count))
                    return;

                Control panel = tabCtl.TabPages[tabIndex];
                componentChangeSvc.OnComponentChanging(tabCtl, null);
                tabCtl.Controls.Remove(panel);
                componentChangeSvc.OnComponentChanged(tabCtl, null, null, null);
                _designerHost.DestroyComponent(panel);
                _designerTransaction.Commit();

                //if (tabCtl.Controls.Count != tabCtl.TabPages.Count)
                //    Debug.WriteLine("Tab control out of sync");

                tabIndex = tabCtl.SelectedIndex;
//                if ((tabIndex < 0) || (tabIndex >= tabCtl.TabPages.Count))
//                    Debug.WriteLine("Tab selection is invalid");
            }
            //}
            //catch (Exception ex) { Debug.WriteLine(ex.ToString()); }
        }

        private void OnRenamePanel(object sender, EventArgs e)
        {

            // TODO: code to rename the selected panel
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            System.Collections.ICollection selection;

            bool selected = false;

            if (_selectionSvc != null)
            {
                selection = _selectionSvc.GetSelectedComponents();
                foreach (object component in selection)
                {
                    if (component == this.Component)
                    {
                        selected = true;
                        break;
                    }
                }
            }

            if (selected != _selected)
            {
                _selected = selected;
                if (selected == true)
                    CreateTabGlyphs();
                else
                    DestroyTabGlyphs();
            }
        }

        public int GetTabIndex(Glyph g)
        {
            return _tabGlyphs.IndexOf(g);
        }

        // This behavior watches for a mouse click in the glyph and sets the
        // tab control's selected index to the tab that corresponds with the glyph.
        public class TabBehavior : Behavior
        {
            ControlDesigner _designer;

            public TabBehavior(ControlDesigner designer)
            {
                _designer = designer;
            }
            public override bool OnMouseUp(Glyph g, System.Windows.Forms.MouseButtons button)
            {
                return base.OnMouseUp(g, button);
            }
            public override bool OnMouseDown(System.Windows.Forms.Design.Behavior.Glyph g, MouseButtons button, Point mouseLoc)
            {

                CCTabControlDesigner tcd = _designer as CCTabControlDesigner;
                int index = ((CCTabControlDesigner)_designer).GetTabIndex(g);
                ((CCTabControl)_designer.Control).SelectedIndex = index;
                return base.OnMouseDown(g, button, mouseLoc);
            }
        }
    }

    public class TabPageCollectionEditor : System.ComponentModel.Design.CollectionEditor
    {
        // This is a very simple collection editor that serves no purpose other than to
        // make it so the add button will add CCPanel objects instead of the Control 
        // objects to the collection.
        public TabPageCollectionEditor(Type type)
            : base(type)
        {
        }

        // This override makes it so that clicking the Add button in the collection
        // editor will add a CCPanel item instead of a Control item.
        protected override Type CreateCollectionItemType()
        {
            return typeof(CCPanel);
        }

        // This override ensures that new items are added to the collection 
        // through the TabPageCollection.Add method, which calls Controls.Add.
        // Values seem to just magically appear or disappear from the collection 
        // without calling Add or Remove otherwise and that will cause an 
        // exception when the number of pages decreases without removing the 
        // _tabs and Controls entries.
        protected override object SetItems(object editValue, object[] value)
        {
            CCTabControl.TabPageCollection pages = editValue as CCTabControl.TabPageCollection;

            pages.Clear();
            foreach (Control page in value)
                pages.Add(page);

            return pages;
        }
    }

    class CCTabControlCodeDomSerializer : CodeDomSerializer
    {
        // This custom CodeDomSerializer is needed to keep CCTabControl.Controls
        // from getting scrambled when it gets serialized by the designer.  The 
        // Controls collection arranges itself by the Z-order of the items put in
        // it, not the order they are added to the control, so they could be in 
        // any order if the default serializer is used.

        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            // Get the serializer of the base class, since it is the same serializer that
            // a derived class should use by default to serialize.
            CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.GetSerializer(typeof(CCTabControl).BaseType, typeof(CodeDomSerializer));

            // Call Synchronize() before serializing any CCTabControl object.
            // This will reorganize the order of the Controls collection to be
            // the same as the TabPages collection so the controls don't end up
            // in a random order when serialized.
            if (value is CCTabControl)
            {
                (value as CCTabControl).Synchronize();
            }

            // Let the base class serializer do all the actual work of serialization.
            object codeObject = baseClassSerializer.Serialize(manager, value);
            return codeObject;
        }
    }
}
