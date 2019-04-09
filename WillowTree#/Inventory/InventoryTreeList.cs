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
using System.Collections.Generic;
using System.Linq;
using Aga.Controls.Tree;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using WillowTree.CustomControls;

namespace WillowTree.Inventory
{
    public class InventoryList
    {
        public byte invType;

        public delegate void EntryChangeEventHandler(InventoryList ilist, InventoryEntry entry);
        public delegate void EntryChangeNodeEventHandler(InventoryList ilist, TreeNodeAdv node);
        public delegate void EntryChangeByKeyEventHandler(InventoryList ilist, string key);
        public delegate void InventoryListEventHandler(InventoryList ilist);
        public delegate void TreeThemeChangedEventHandler(InventoryList ilist, TreeViewTheme theme);
        //public delegate void InventoryListNavigationDepthChangedEventHandler(InventoryList ilist, int depth);
        //public delegate void InventoryListSortModeChangedEventHandler(InventoryList ilist, Comparer<InventoryEntry> comparer);

        public event EntryChangeEventHandler EntryAdd;
        public event EntryChangeEventHandler EntryRemove;
        public event EntryChangeNodeEventHandler EntryRemoveNode;
        public event EntryChangeByKeyEventHandler EntryAddByKey;
        public event EntryChangeByKeyEventHandler EntryRemoveByKey;
        public event InventoryListEventHandler ListReload;
        public event InventoryListEventHandler NameFormatChanged;
        public event TreeThemeChangedEventHandler TreeThemeChanged;
        //public event InventoryListNavigationDepthChangedEventHandler NavigationDepthChanged;
        //public event InventoryListSortModeChangedEventHandler SortModeChanged;

        public void OnEntryAdd(InventoryEntry entry)
        {
            if (EntryAdd != null)
                EntryAdd(this, entry);
        }
        public void OnEntryRemove(InventoryEntry entry)
        {
            if (EntryRemove != null)
                EntryRemove(this, entry);
        }
        public void OnEntryRemoveNode(TreeNodeAdv node)
        {
            if (EntryRemoveNode != null)
                EntryRemoveNode(this, node);
        }
        // 0 references
        public void OnEntryAddByKey(string key)
        {
            if (EntryAddByKey != null)
                EntryAddByKey(this, key);
        }
        // 0 references
        public void OnEntryRemoveByKey(string key)
        {
            if (EntryRemoveByKey != null)
                EntryRemoveByKey(this, key);
        }

        public void OnListReload()
        {
            if (ListReload != null)
                ListReload(this);
        }
        public void OnNameFormatChanged()
        {
            if (NameFormatChanged != null)
                NameFormatChanged(this);
        }
        public void OnTreeThemeChanged(TreeViewTheme theme)
        {
            if (TreeThemeChanged != null)
                TreeThemeChanged(this, theme);
        }
            
        //public void OnSortModeChanged(Comparer<InventoryEntry> comparer)
        //{
        //    if (SortModeChanged != null)
        //        SortModeChanged(this, comparer);
        //}
        //public void OnNavigationDepthChanged(int depth)
        //{
        //    if (NavigationDepthChanged != null)
        //        NavigationDepthChanged(this, depth);
        //}

        //public List<InventoryEntry> Entries;
        public Dictionary<string, InventoryEntry> Items;

        public InventoryList(byte inventoryType)
        {
            //this.Entries = new List<InventoryEntry>();
            this.Items = new Dictionary<string, InventoryEntry>();
            invType = inventoryType;
        }

        public InventoryEntry this[string key]
        {
            get { return this.Items[key]; }
            set { this.Items[key] = value; }
        }

        public void Add(InventoryEntry entry)
        {
            entry.Key = db.CreateUniqueKey();
            Items.Add(entry.Key, entry);
            OnEntryAdd(entry);
        }
        public void AddSilent(InventoryEntry entry)
        {
            entry.Key = db.CreateUniqueKey();
            Items.Add(entry.Key, entry);
        }

        // 1 references to unused method
        public void Remove(InventoryEntry entry)
        {
            Items.Remove(entry.Key);
            OnEntryRemove(entry);
        }
        // 0 references
        public void RemoveSilent(InventoryEntry entry)
        {
            Items.Remove(entry.Key);
        }

        public void Remove(TreeNodeAdv node)
        {
            InventoryEntry entry = node.GetEntry();
            Items.Remove(entry.Key);
            OnEntryRemoveNode(node);
        }
        // 0 references
        public void RemoveSilent(TreeNodeAdv node)
        {
            InventoryEntry entry = node.GetEntry();
            Items.Remove(entry.Key);
        }
        
        // 0 references
        public void RemoveByKey(string key)
        {
            InventoryEntry entry = Items[key];
            Items.Remove(key);
            OnEntryRemove(entry);
        }
        // 0 references
        public void RemoveByKeySilent(string key)
        {
            Items.Remove(key);
        }

        //public void Insert(int index, InventoryEntry entry)
        //{
        //    entry.Key = db.CreateUniqueKey();
        //    Entries.Insert(index, entry);
        //    List.Add(entry.Key, entry);

        //    TreeNodeAdv insertnode = Tree.FindFirstNodeByTag(Entries[index].Key, true);
        //    TreeNodeAdv parent = insertnode.Parent;
        //    ColoredTextNode node = new ColoredTextNode(entry.Key, entry.Name);
        //    node.ForeColor = entry.Color;
        //    parent.InsertNode(insertnode.Index, node);
        //}
        public void Duplicate(InventoryEntry entry)
        {
            InventoryEntry copy = new InventoryEntry(entry);
            copy.Key = db.CreateUniqueKey();
            Items.Add(copy.Key, copy);
            OnEntryAdd(copy);
        }
        // 1 references to unused method
        public void DuplicateSilent(InventoryEntry entry)
        {
            InventoryEntry copy = new InventoryEntry(entry);
            copy.Key = db.CreateUniqueKey();
            //Entries.Add(copy);
            Items.Add(copy.Key, copy);
        }

        // 0 references
        public void Duplicate(string key)
        {
            InventoryEntry entry = this.Items[key];
            Duplicate(entry);
        }
        // 0 references
        public void DuplicateSilent(string key)
        {
            InventoryEntry entry = this.Items[key];
            DuplicateSilent(entry);
        }

        public void Clear()
        {
            Items.Clear();
            OnListReload();
        }
        public void ClearSilent()
        {
            Items.Clear();
        }
        
        // 1 references to unused method
        public void ImportFromXml(string InputFile, int EntryType)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(InputFile);
            }
            catch
            {
                MessageBox.Show("Error loading the XML file.  Nothing imported.");
                return;
            }

            XmlNodeList nodes = doc.SelectNodes("/INI/Section");
            foreach (XmlNode node in nodes)
            {

                InventoryEntry entry = new InventoryEntry(node);
                if (entry.Type == InventoryType.Unknown)
                {
                    MessageBox.Show("Invalid entry skipped in XML file");
                    continue;
                }
                // Check to make sure the item is the right type 
                // ItemType 0 = weapons, 1 = items, 2 = all types
                if ((EntryType == InventoryType.Any) || (EntryType == entry.Type))
                    this.AddSilent(entry);
            }

            OnListReload();
        }
        // 0 references
        public void LoadFromXml(string InputFile, int EntryType)
        {
            this.ClearSilent();
            ImportFromXml(InputFile, EntryType);
        }
        // 0 references
        public void SaveToXml(string InputFile)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                using (XmlTextWriter writer = new XmlTextWriter(InputFile, System.Text.Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("INI");
                    foreach (InventoryEntry entry in this.Items.Values)
                    {
                        writer.WriteStartElement("Section");
                        writer.WriteRaw(entry.ToXmlText());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failure while writing to XML file \"" + InputFile + "\".  " + e.Message);
            }
        }
    }

    public class InventoryTreeList
    {
        public InventoryList Unsorted;
        public List<InventoryEntry> Sorted;
        public WTTreeView Tree;

        public InventoryComparisonIterator IEComparisonEngine =
            new InventoryComparisonIterator(InventoryComparers.DefaultComparerList);

        public int NavigationLayers;

        #region partCollection
        private string[] weaponParts = new string[]
        {
            "Item Grade",
            "Manufacturer",
            "Weapon Type",
            "Body",
            "Grip",
            "Mag",
            "Barrel",
            "Sight",
            "Stock",
            "Action",
            "Accessory",
            "Material",
            "Prefix",
            "Title"
        };
        private string[] itemParts = new string[]
        {
            "Item Grade",
            "Item Type",
            "Body",
            "Left Side",
            "Right Side",
            "Material",
            "Manufacturer",
            "Prefix",
            "Title"
        };
        #endregion

        public InventoryTreeList(WTTreeView tree, InventoryList ilist)
        {
            this.Unsorted = ilist;
            this.Sorted = new List<InventoryEntry>();
            this.Tree = tree;
            this.NavigationLayers = 1;

            Unsorted.EntryAdd += OnEntryAdd;
            Unsorted.EntryRemove += OnEntryRemove;
            Unsorted.EntryRemoveNode += OnEntryRemoveNode;
            Unsorted.ListReload += OnListReload;
            Unsorted.NameFormatChanged += OnNameFormatChanged;
            Unsorted.TreeThemeChanged += OnTreeThemeChanged;
            //Unsorted.NavigationDepthChanged += OnNavigationDepthChanged;
            //Unsorted.SortModeChanged += OnSortModeChanged;
        }

        public void OnEntryAdd(InventoryList ilist, InventoryEntry entry)
        {
            Sorted.Add(entry);
            AddToTreeView(entry);
        }
        public void OnEntryRemoveNode(InventoryList ilist, TreeNodeAdv node)
        {
            InventoryEntry entry = node.GetEntry();
            Sorted.Remove(entry);
            RemoveFromTreeView(node, false);
        }
        public void OnEntryRemove(InventoryList ilist, InventoryEntry entry)
        {
            Sorted.Remove(entry);
            RemoveFromTreeView(entry, false);
        }
        public void OnListReload(InventoryList ilist)
        {
            Sorted.Clear();
            ClearTreeView();

            Sorted.AddRange(Unsorted.Items.Values);
            SortByCustom();
        }
        public void OnNameFormatChanged(InventoryList ilist)
        {
            UpdateNames();
        }
        public void OnNavigationDepthChanged(InventoryList ilist, int depth)
        {

        }
        public void OnSortModeChanged(InventoryList ilist, IComparer<InventoryEntry> comparer)
        {
            if (comparer == null)
                NextSort();
        }
        public void OnTreeThemeChanged(InventoryList ilist, TreeViewTheme theme)
        {
            Tree.Theme = theme;
        }

        public bool updateSelection = false;

        public void Add(InventoryEntry entry)
        {
            Unsorted.Add(entry);
            // Implicit event call to OnEntryAdd occurs here
        }
        // 0 references
        public void Add(IEnumerable<InventoryEntry> entries)
        {
            Tree.BeginUpdate();
            foreach (InventoryEntry entry in entries)
                Add(entry);
            Tree.EndUpdate();
        }
        // 0 references
        public void Add(IEnumerable<TreeNodeAdv> nodes)
        {
            Tree.BeginUpdate();
            foreach (TreeNodeAdv node in nodes)
            {
                if (node.Children.Count > 0)
                    continue;

                InventoryEntry weapon = node.GetEntry();
                Add(weapon);
            }
            Tree.EndUpdate();
        }
        public void AddNew(byte invType)
        {
            List<int> values = null;
            List<string> parts = new List<string>();

            if (invType == InventoryType.Weapon)
            {
                values = new List<int>() { 0, 5, 0, 0 };
                parts.AddRange(weaponParts);
            }
            else if (invType == InventoryType.Item)
            {
                values = new List<int>() { 1, 5, 0, 0 };
                parts.AddRange(itemParts);
            }
            
            this.Add(new InventoryEntry(invType, parts, values));
        }

        // 1 references to unused method
        public void Remove(InventoryEntry entry, bool updateSelection)
        {
            this.updateSelection = updateSelection;
            Unsorted.Remove(entry);
            // Implicit event call to OnEntryRemove occurs here
        }
        public void Remove(TreeNodeAdv nodeAdv, bool updateSelection)
        {
            this.updateSelection = updateSelection;
            Unsorted.Remove(nodeAdv);
        }
        // 0 references
        public void Remove(IEnumerable<InventoryEntry> entriesToRemove)
        {
            InventoryEntry[] entries = entriesToRemove.ToArray();

            Tree.BeginUpdate();
            foreach (InventoryEntry entry in entries)
                Remove(entry, false);
            Tree.EndUpdate();
        }
        public void Remove(IEnumerable<TreeNodeAdv> nodesToRemove)
        {
            TreeNodeAdv[] nodes = nodesToRemove.ToArray();
            Tree.BeginUpdate();

            foreach (TreeNodeAdv node in nodes)
            {
                // Nodes with children are not items they are categories
                if (node.Children.Count == 0)
                    Remove(node, false);
            }
            Tree.EndUpdate();
        }

        public void ClearTreeView()
        {
            Tree.Clear();
        }
        public void RemoveFromTreeView(TreeNodeAdv node, bool selectNextNode)
        {
            _next = null;

            if (node == null)
                return;

            // If the node being removed is the selected node in the tree
            // a new node will have to be selected.
            if (node == Tree.SelectedNode)
            {
                _next = node.NextVisibleNode;
                // Navigate through children until an actual item node is
                // found if the new node is a navigation node.
                if (_next != null)
                {
                    while (_next.Children.Count > 0)
                        _next = _next.Children[0];
                    //while (newnode.Children.Count > 0)
                    //    newnode = newnode.Children[0];
                }
            }

            // Remove the item node and any parent navigation nodes that are
            // empty.
            if (node != null)
            {
                TreeNodeAdv parent = node.Parent;
                node.Remove();
                while ((parent != Tree.Root) && (parent.Children.Count == 0))
                {
                    node = parent;
                    parent = node.Parent;
                    node.Remove();
                }
            }

            if (selectNextNode == true)
                Tree.SelectedNode = _next;
        }
        public void RemoveFromTreeView(InventoryEntry entry, bool selectNextNode)
        {
            // This is much slower than removing an entry by its node because it
            // has to search the whole tree for the entry first.  Use
            // RemoveFromTreeView(TreeNodeAdv node, bool selectNextNode) when 
            // possible.
            _next = null;

            // First find the node being removed in the tree
            TreeNodeAdv node = Tree.FindFirstNodeByTag(entry, true);
            if (node == null)
                return;

            // If the node being removed is the selected node in the tree
            // a new node will have to be selected.
            if (node == Tree.SelectedNode)
            {
                _next = node.NextVisibleNode;
                // Navigate through children until an actual item node is
                // found if the new node is a navigation node.
                if (_next != null)
                {
                    while (_next.Children.Count > 0)
                        _next = _next.Children[0];
                    //while (newnode.Children.Count > 0)
                    //    newnode = newnode.Children[0];
                }
            }

            // Remove the item node and any parent navigation nodes that are
            // empty.
            if (node != null)
            {
                TreeNodeAdv parent = node.Parent;
                node.Remove();
                while ((parent != Tree.Root) && (parent.Children.Count == 0))
                {
                    node = parent;
                    parent = node.Parent;
                    node.Remove();
                }
            }

            if (selectNextNode == true)
                Tree.SelectedNode = _next;
        }

        //public void Insert(int index, InventoryEntry entry)
        //{
        //    entry.Key = db.CreateUniqueKey();
        //    Entries.Insert(index, entry);
        //    List.Add(entry.Key, entry);

        //    TreeNodeAdv insertnode = Tree.FindFirstNodeByTag(Entries[index].Key, true);
        //    TreeNodeAdv parent = insertnode.Parent;
        //    ColoredTextNode node = new ColoredTextNode(entry.Key, entry.Name);
        //    node.ForeColor = entry.Color;
        //    parent.InsertNode(insertnode.Index, node);
        //}
        public void Duplicate(InventoryEntry entry)
        {
            InventoryEntry copy = new InventoryEntry(entry);
            Unsorted.Add(copy);
            // Implicit event call to OnEntryAdd occurs here
        }
        // 0 references
        public void Duplicate(string key)
        {
            // TODO: implement using DuplicateByKey for greater speed
            InventoryEntry entry = Unsorted[key];
            Duplicate(entry);
        }
        // 0 references
        public void Duplicate(IEnumerable<TreeNodeAdv> nodes)
        {
            Tree.BeginUpdate();
            foreach (TreeNodeAdv node in nodes)
            {
                if (node.Children.Count > 0)
                    continue;

                InventoryEntry weapon = node.GetEntry();
                Duplicate(weapon);
            }
            Tree.EndUpdate();
        }
        // 0 references
        public void Duplicate(IEnumerable<InventoryEntry> entries)
        {
            Tree.BeginUpdate();
            foreach (InventoryEntry entry in entries)
                Duplicate(entry);
            Tree.EndUpdate();
        }

        public void Clear()
        {
            Unsorted.Clear();
            // Implicit event call to OnListReload here
        }

        public InventoryEntry this[int index]
        {
            get
            {
                return Sorted[index];
            }
        }

        public InventoryEntry this[string key]
        {

            get
            {
                _searchKey = key;
                return Sorted.Find(KeyEquals);
            }
        }

        string _searchKey;
        bool KeyEquals(InventoryEntry entry)
        {
            if (entry.Key == _searchKey)
                return true;

            return false;
        }
        public void ImportFromXml(string InputFile, int EntryType)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(InputFile);
            }
            catch
            {
                MessageBox.Show("Error loading the XML file.  Nothing imported.");
                return;
            }

            XmlNodeList nodes = doc.SelectNodes("/INI/Section");
            Tree.BeginUpdate();
            foreach (XmlNode node in nodes)
            {
                InventoryEntry entry = new InventoryEntry(node);
                if (entry.Type == InventoryType.Unknown)
                {
                    MessageBox.Show("Invalid entry skipped in XML file");
                    continue;
                }

                // Check to make sure the item is the right type then add it
                // ItemType -1 = unknown, 0 = weapons, 1 = items, 2 = any type
                if ((EntryType == InventoryType.Any) || (EntryType == entry.Type))
                    this.Add(entry);
            }
            Tree.EndUpdate();
        }
        public void LoadFromXml(string InputFile, int EntryType)
        {
            Tree.BeginUpdate();
            this.Clear();
            ImportFromXml(InputFile, EntryType);
            Tree.EndUpdate();
        }
        public void SaveToXml(string InputFile)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                using (XmlTextWriter writer = new XmlTextWriter(InputFile, System.Text.Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("INI");
                    foreach (InventoryEntry entry in this.Sorted)
                    {
                        writer.WriteStartElement("Section");
                        writer.WriteRaw(entry.ToXmlText());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    db.OpenedLockerFilename(InputFile);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failure while writing to XML file \"" + InputFile + "\".  " + e.Message);
            }
        }

        public void AdjustSelectionAfterAdd()
        {
            Tree.SelectedNode = _parent.FindNodeAdvByTag(_node, false);
        }
        public void AdjustSelectionAfterRemove()
        {
            if (_next != null)
                Tree.SelectedNode = _next;
        }

        public TreeNodeAdv CreateNavigationNodes(InventoryEntry entry)
        {
            int[] sortmodes = IEComparisonEngine.CurrentComparer().comparisons;
            int loopcount = (NavigationLayers < sortmodes.Length) ? NavigationLayers : sortmodes.Length;

            TreeNodeAdv navnode = null;
            TreeNodeAdv newbranch = null;
            string currentcategory;
            string categorytext;
            for (int i = 0; i < loopcount; i++)
            {
                // 0: Name
                // 1: Rarity
                // 2: Category
                // 3: Title
                // 4: Prefix
                // 5: Model
                // 6: Manufacturer
                // 7: Level
                // 8: Key

                switch (sortmodes[i])
                {
                    case 2:
                        currentcategory = entry.Category;
                        if (currentcategory == "")
                            currentcategory = "none";
                        if (!CategoryLookup.TryGetValue(currentcategory, out categorytext))
                        {
                            currentcategory = "(Unknown)";
                            categorytext = "(Unknown)";
                        }
                        break;
                    case 6:
                        currentcategory = entry.NameParts[0];
                        if (currentcategory == "")
                            currentcategory = "No Manufacturer";
                        categorytext = currentcategory;
                        break;
                    case 7:
                        currentcategory = "Level " + entry.EffectiveLevel.ToString();
                        categorytext = currentcategory;
                        break;
                    case 3:
                        currentcategory = entry.NameParts[3];
                        if (currentcategory == "")
                            currentcategory = "No Title";
                        categorytext = currentcategory;
                        break;
                    case 4:
                        currentcategory = entry.NameParts[2];
                        if (currentcategory == "")
                            currentcategory = "No Prefix";
                        categorytext = currentcategory;
                        break;
                    case 5:
                        currentcategory = entry.NameParts[1];
                        if (currentcategory == "")
                            currentcategory = "No Model";
                        categorytext = currentcategory;
                        break;
                    case 1:
                        currentcategory = entry.Name;
                        categorytext = currentcategory;
                        break;
                    default:
                        return navnode;
                }

                //Debugger.Break();
                //if (navnode == null)
                //    newbranch = Tree.FindFirstNodeByTag(currentcategory, false);
                //else
                if (navnode != null)
                    newbranch = navnode.FindFirstByTag(currentcategory, false);
                else
                    newbranch = Tree.Root.FindFirstByTag(currentcategory, false);

                if (newbranch == null)
                {
                    // This category does not exist yet.  Create a node for it.
                    ColoredTextNode data = new ColoredTextNode();
                    data.Tag = currentcategory;
                    data.ForeColor = Color.LightSkyBlue;
                    if (GlobalSettings.UseColor)
                        data.Text = categorytext;
                    else    
                        data.Text = "--- " + categorytext + " ---";

                    if (navnode == null)
                    {
                        (Tree.Model as TreeModel).Nodes.Add(data);
                        navnode = Tree.Root;
                    }
                    else
                        navnode.AddNode(data);

                    newbranch = navnode.Children[navnode.Children.Count - 1];
                    //newbranch = navnode.FindFirstByTag(currentcategory, false);

                    //if (navnode == null)
                    //{
                    //    (Tree.Model as TreeModel).Nodes.Add(data);
                    //    newbranch = Tree.FindNodeByTag(data).Tag as Node;
                    //}
                    //else
                    //    navnode.Nodes.Add(newbranch);
                }
                // Update the navnode then iterate again for the next tier of 
                // category nodes until all category nodes are present
                navnode = newbranch;
            }
            return navnode;
        }

        TreeNodeAdv _parent;    // The last parent node to have a child added
        ColoredTextNode _node;  // The child that was added to the last parent node
        TreeNodeAdv _next;
        int _lastNodeIndex = -1;

        public void AddToTreeView(InventoryEntry entry)
        {
            _parent = CreateNavigationNodes(entry);

            _node = new ColoredTextNode();
            _node.Tag = entry;
            _node.ForeColor = entry.Color;
            _node.Text = entry.Name;

            Collection<Node> nodes;
            if (_parent == null)
            {
                _parent = Tree.Root;
                nodes = (Tree.Model as TreeModel).Nodes;
            }
            else
                nodes = (_parent.Tag as Node).Nodes;

            IComparer<InventoryEntry> Comparer = IEComparisonEngine.CurrentComparer();

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (Comparer.Compare(nodes[i].Tag as InventoryEntry, entry) == -1)
                {
                    _lastNodeIndex = i + 1;
                    nodes.Insert(_lastNodeIndex, _node);
                    return;
                }
            }
            nodes.Insert(0, _node);
            _lastNodeIndex = 0;
        }

        // 0 references
        //public void AddToTreeView2(InventoryEntry entry)
        //{
        //    _parent = CreateNavigationNodes(entry);

        //    _node = new ColoredTextNode();
        //    _node.Tag = entry;
        //    _node.Text = entry.Name;
        //    _node.ForeColor = entry.Color;

        //    if (_parent == null)
        //    {
        //        _parent = Tree.Root;
        //        (Tree.Model as TreeModel).Nodes.Add(_node);
        //    }
        //    else
        //        _parent.AddNode(_node);
        //}
        
        public void UpdateTree()
        {
            // This procedure clears the GUI tree and rebuilds it.  It
            // adds navigation nodes as needed to show item categories
            Tree.BeginUpdate();
            Tree.Clear();
            foreach (InventoryEntry entry in Sorted)
                AddToTreeView(entry);
            Tree.EndUpdate();
        }

        public void UpdateNames()
        {
            foreach (InventoryEntry entry in Sorted)
                entry.BuildName();

            UpdateTree();
        }

        public void IncreaseNavigationDepth()
        {
            NavigationLayers++;
            if (NavigationLayers > 3)
                NavigationLayers = 0;
            UpdateTree();
        }

        public void NextSort()
        {
            IEComparisonEngine.NextComparer();
            SortByCustom();
        }

        // 0 references
        //public void CopyTo(InventoryList dest, IEnumerable<TreeNodeAdv> nodesToCopy, int entryType)
        //{
        //    TreeNodeAdv[] nodes = nodesToCopy.ToArray();

        //    foreach (TreeNodeAdv node in nodes)
        //    {
        //        InventoryEntry old = node.GetEntry();
        //        // If the entry is null it is because the tag isn't an inventory
        //        // entry object.  That means it is a category node so don't duplicate
        //        // it.
        //        if (old == null)
        //            continue;

        //        InventoryEntry entry = new InventoryEntry(old);
        //        if (entry.Type == entryType || entryType == InventoryType.Any)
        //            dest.Add(entry);
        //    }
        //}
        // 0 references
        //public void Copy(IEnumerable<TreeNodeAdv> nodesToCopy, InventoryList dest)
        //{
        //    TreeNodeAdv[] nodes = nodesToCopy.ToArray();

        //    foreach (TreeNodeAdv node in nodes)
        //    {
        //        InventoryEntry old = node.GetEntry();
        //        // If the entry is null it is because the tag isn't an inventory
        //        // entry object.  That means it is a category node so don't duplicate
        //        // it.
        //        if (old == null)
        //            continue;

        //        InventoryEntry entry = new InventoryEntry(old);
        //        dest.Add(entry);
        //    }
        //}

        public void CopySelected(InventoryList dest, bool deleteSource)
        {
            TreeNodeAdv[] nodes = Tree.SelectedNodes.ToArray();

            Tree.BeginUpdate();
            foreach (TreeNodeAdv node in nodes)
            {
                InventoryEntry old = node.GetEntry();
                // If the entry is null it is because the tag isn't an inventory
                // entry object.  That means it is a category node so don't duplicate
                // it.
                if (old == null)
                    continue;

                InventoryEntry entry = new InventoryEntry(old);
                if (deleteSource == true) Remove(node, false);
                dest.Add(entry);
            }
            Tree.EndUpdate();
        }

        public void DeleteSelected()
        {
            TreeNodeAdv[] nodes = Tree.SelectedNodes.ToArray();

            this.Remove(nodes);
            AdjustSelectionAfterRemove();
        }

        public void DuplicateSelected()
        {
            TreeNodeAdv[] nodes = Tree.SelectedNodes.ToArray();

            foreach (TreeNodeAdv node in nodes)
            {
                InventoryEntry old = node.GetEntry();
                // If the entry is null it is because the tag isn't an inventory
                // entry object.  That means it is a category node so don't duplicate
                // it.
                if (old == null)
                    continue;

                InventoryEntry entry = new InventoryEntry(old);
                Add(entry);
            }
        }
                    
        //public void MoveSelected(InventoryList dest, int itemType)
        //{
        //    Tree.BeginUpdate();
        //    TreeNodeAdv NextSelection = null;
        //    while (Tree.SelectedNode != null)
        //    {
        //        // If the node has children then it is a category node so don't
        //        // delete it.
        //        if (Tree.SelectedNode.Children.Count > 0)
        //        {
        //            NextSelection = Tree.SelectedNode;
        //            Tree.SelectedNode.IsSelected = false;
        //        }
        //        else
        //        {
        //            NextSelection = Tree.SelectedNode.NextVisibleNode;
        //            InventoryEntry entry = Tree.SelectedNode.GetEntry();
        //            Remove(entry, false);
        //            if (entry.Type == itemType || entry.Type == InventoryType.Any)
        //                dest.Add(entry);
        //  //          Sorted.Remove(entry);
        //  //          Unsorted.RemoveByKeySilent(entry.Key);
        //  //          RemoveFromTreeView(Tree.SelectedNode, false);
        //        }
        //    }
        //    if (NextSelection != null)
        //        Tree.SelectedNode = NextSelection;
        //    Tree.EndUpdate();
        //}

        public void PurgeDuplicates()
        {
            string lastGoodFile = db.OpenedLockerFilename();    //Keep last valid locker path file
            string tempfile = db.DataPath + "purgeduplicates.temp";
            SaveToXml(tempfile);
            XmlFile.PurgeDuplicates(tempfile);
            LoadFromXml(tempfile, InventoryType.Any);
            System.IO.File.Delete(tempfile);

            db.OpenedLockerFilename(lastGoodFile);  //Restore last valid locker path file
        }

        #region CategoryLookup Initializer
        public Dictionary<string, string> CategoryLookup = new Dictionary<string, string>()
                    {
                        { "all", "ALL" },
                        { "ammo", "AMMO" },
                        { "ammoupgrade", "AMMO UPGRADES" },
                        { "any", "ANY" },
                        { "ar", "ASSAULT RIFLES" },
                        { "comm", "CLASS MODS" },
                        { "compare", "COMPARE" },
                        { "elemental", "ELEMENTAL ARTIFACTS" },
                        { "equipped", "EQUIPPED" },
                        { "eridian", "ERIDIAN WEAPONS" },
                        { "grenade", "GRENADES" },
                        { "health", "MED KITS" },
                        { "healthTab", "HEALTH" },
                        { "instahealth", "INSTA-HEALTHS" },
                        { "items", "ITEMS" },
                        { "manufacturers", "BRANDS" },
                        { "mod", "GRENADE MODS" },
                        { "money", "MONEY" },
                        { "none", "STOCK ITEMS" },
                        { "personal", "PERSONAL" },
                        { "repeater", "REPEATERS" },
                        { "revolver", "REVOLVERS" },
                        { "rocket", "ROCKET LAUNCHERS" },
                        { "sdu", "UPGRADES" },
                        { "shield", "SHIELDS" },
                        { "shop", "SHOP" },
                        { "shotgun", "SHOTGUNS" },
                        { "smg", "SUB-MACHINE GUNS" },
                        { "sniper", "SNIPER RIFLES" },
                        { "types", "TYPES" },
                        { "weapon", "WEAPONS" },
                    };
        #endregion

        public void SortByCustom()
        {
            Sorted.Sort(IEComparisonEngine.CurrentComparer().Compare);
            UpdateTree();
        }

        public void RemoveSort()
        {
            // The lookup table is never sorted, so it contains the original 
            // unsorted list.  Copy the inventory entries from there.
            Sorted.Clear();
            foreach (InventoryEntry entry in Unsorted.Items.Values)
            {
                Sorted.Add(entry);
            }
            UpdateTree();
        }
    }
   
    public class CaseInsensitiveComparer : EqualityComparer<string>
    {

        public override bool Equals(string x, string y)
        {
            return x.Equals(y, StringComparison.CurrentCultureIgnoreCase);
        }
        public override int GetHashCode(string s)
        {
            return s.GetHashCode();
        }
    }

    public class InventoryComparer : Comparer<InventoryEntry>
    {
        public int[] comparisons;

        public InventoryComparer(int[] comparisonarray)
        {
            comparisons = comparisonarray;
        }

        public override int Compare(InventoryEntry x, InventoryEntry y)
        {
            int result = 0;
            foreach (int comparison in comparisons)
            {
                switch (comparison)
                {
                    // 0: Name
                    // 1: Rarity
                    // 2: Category
                    // 3: Title
                    // 4: Prefix
                    // 5: Model
                    // 6: Manufacturer
                    // 7: Level
                    // 8: Key
                    case 0: result = string.Compare(x.Name, y.Name); break;
                    case 1:
                        if (x.Rarity > y.Rarity)
                            result = -1;
                        else if (x.Rarity < y.Rarity)
                            result = 1;
                        else
                            result = 0;
                        break;
                    case 2: result = string.Compare(x.Category, y.Category); break;
                    case 3: result = string.Compare(x.NameParts[3], y.NameParts[3]); break;
                    case 4: result = string.Compare(x.NameParts[2], y.NameParts[2]); break;
                    case 5: result = string.Compare(x.NameParts[1], y.NameParts[1]); break;
                    case 6: result = string.Compare(x.NameParts[0], y.NameParts[0]); break;
                    case 7:
                        if (x.EffectiveLevel > y.EffectiveLevel)
                            result = -1;
                        else if (x.EffectiveLevel < y.EffectiveLevel)
                            result = 1;
                        else
                            result = 0;
                        break;
                    case 8:
                        int xkeyval = int.Parse(x.Key);
                        int ykeyval = int.Parse(y.Key);
                        if (xkeyval < ykeyval)
                            result = -1;
                        else if (xkeyval > ykeyval)
                            result = 1;
                        else
                            result = 0;
                        break;
                }
                if (result != 0)
                    return result;
            }
            return result;
        }
    }

    public static class InventoryComparers
    {
        public static InventoryComparer KeyComparer = new InventoryComparer(new int[] { 8 });
        public static InventoryComparer CategoryNameComparer = new InventoryComparer(new int[] { 2, 0, 8 });
        public static InventoryComparer ManufacturerNameComparer = new InventoryComparer(new int[] { 6, 0, 8 });
        public static InventoryComparer CategoryRarityLevelComparer = new InventoryComparer(new int[] { 2, 1, 7, 8 });
        public static InventoryComparer CategoryTitlePrefixModelComparer = new InventoryComparer(new int[] { 2, 3, 4, 5, 8 });
        public static InventoryComparer CategoryLevelNameComparer = new InventoryComparer(new int[] { 2, 7, 0, 8 });
        public static InventoryComparer NameComparer = new InventoryComparer(new int[] { 0, 8 });
        public static InventoryComparer RarityLevelComparer = new InventoryComparer(new int[] { 1, 7, 8 });
        public static InventoryComparer TitlePrefixModelComparer = new InventoryComparer(new int[] { 3, 4, 5, 8 });
        public static InventoryComparer LevelNameComparer = new InventoryComparer(new int[] { 7, 0, 8 });

        public static InventoryComparer[] DefaultComparerList =
                new InventoryComparer[] 
                    {
                        KeyComparer,
                        CategoryNameComparer,
                        ManufacturerNameComparer,
                        CategoryRarityLevelComparer,
                        CategoryTitlePrefixModelComparer,
                        CategoryLevelNameComparer,
                        NameComparer,
                        RarityLevelComparer,
                        LevelNameComparer
                    };
    }

    public class InventoryComparisonIterator
    {
        public int ComparerIndex;
        public InventoryComparer[] Comparers;
        public InventoryComparisonIterator(InventoryComparer[] comparers)
        {
            Comparers = comparers;
        }
        public void NextComparer()
        {
            ComparerIndex++;
            if (ComparerIndex >= Comparers.Length)
                ComparerIndex = 0;
        }
        public void PreviousComparer()
        {
            ComparerIndex++;
            if (ComparerIndex >= Comparers.Length)
                ComparerIndex = 0;
        }
        public InventoryComparer CurrentComparer()
        {
            return Comparers[ComparerIndex];
        }
    }
}