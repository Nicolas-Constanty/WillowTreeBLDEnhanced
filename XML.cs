/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2011 Matthew Carter <matt911@users.sf.net>
 *  Copyright (C) 2010 XanderChaos
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
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;
using Aga.Controls.Tree;

namespace WillowTree
{
    /// <summary>

    /// Create a New XML file to store or load data

    /// </summary>

    public class XmlFile
    {
        private static Dictionary<string, XmlFile> XmlCache = new Dictionary<string, XmlFile>();

        public static XmlFile XmlFileFromCache(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return null;

            // Cache all XML files opened with XmlFileFromCache in a
            // dictionary then reuse them.  This prevents having to re-read
            // the file data constantly by creating a new XmlFile then
            // releasing it.
            XmlFile xml;
            if (XmlCache.TryGetValue(filename, out xml))
                return xml;

            // If its not in the cache then open from disk and add to cache
            xml = new XmlFile(filename);
            XmlCache.Add(filename, xml);
            return xml;
        }

        public static void XmlFileCacheRelease(string filename)
        {
            XmlCache.Remove(filename);
        }
        
        public string path;
        public XmlDocument xmlrdrdoc = null;
        //private XmlNodeList xnrdrList = null;
        //private string[] arrListSectionNames = null;
        private List<string> listListSectionNames = new List<string>();

        /// <summary>

        /// INIFile Constructor.

        /// </summary>

        /// <PARAM name="INIPath"></PARAM>

        public XmlFile(string filePath)
        {
            List<string> listfilePath=new List<string>();
            string targetfile = "";

            filePath = Path.GetFullPath(filePath);
            this.path = filePath;
            listfilePath.Add(filePath); //Contains all ini style filenames
            string fileext = Path.GetExtension(filePath);
            if ((fileext == ".ini") || (fileext == ".txt"))
            {   
                string filename = Path.GetFileNameWithoutExtension(filePath);
                // Must add the directory separator character to the end of the folder because
                // it is stored that way in db.DataPath.
                string folder = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar;

                if (db.DataPath.Length <= folder.Length && folder.Substring(0, db.DataPath.Length)
                    .Equals(db.DataPath, StringComparison.OrdinalIgnoreCase))
                {
                    // If the INI or TXT file is in the Data folder (db.DataPath) or its
                    // descendant then make a corresponding XML file in the Xml folder 
                    // (db.XmlPath).  This is different from below because the Xml folder 
                    // doesn't necessarily have to be db.DataPath + "Xml\".  The Xml path 
                    // could be C:\Xml\ and the Data path could be C:\Data\ and a file named
                    // C:\Data\Quests\Part1.ini would produce an XML file named
                    // C:\Xml\Quests\Part1.xml.  The code below would produce a file named
                    // C:\Data\Quests\Xml\Part1.xml
                    folder = db.XmlPath + folder.Substring(db.DataPath.Length);
                }
                else
                {
                    // This handles cases where the file is not in the Data folder or
                    // one of its subfolders.
                    //
                    // A subfolder called Xml will be created if necessary in the folder that 
                    // contains the INI file and the file folder will have "Xml\" appended.
                    // (matt911) I don't think there is any place that WillowTree# even
                    // attempts to use an ini or txt file as an XmlFile except when it is in 
                    // the Data folder, so this code line is probably never executed.  It might
                    // have some use if an application other than WT# was to use the XML
                    // code though.
                    folder = folder + "Xml" + Path.DirectorySeparatorChar;
                }
                targetfile = Path.Combine(folder, filename + ".xml");
                if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(targetfile)) == false)
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetfile));

                ConvertIni2Xml(listfilePath, targetfile);
                this.path = targetfile;
            }
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            listListSectionNames.Clear();
        }
        
        public XmlFile(List<string> filePaths, string targetfile)
        {
            //List<string> listfilePath = new List<string>();

            ConvertIni2Xml(filePaths, targetfile);
            path = targetfile;

            xmlrdrdoc = null;
            //arrListSectionNames = null;
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void Reload()
        {
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            listListSectionNames.Clear();
        }
        public void Reload(string filename)
        {
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            path = filename;
            listListSectionNames.Clear();
        }
        public void XmlWriteValue(string Section, string Key, string Value)
        {
            //WritePrivateProfileString(Section, Key, Value, this.path);

            // search node
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            //Select the old item with the matching name

            XmlElement root = doc.DocumentElement;
            XmlNode item = root.SelectSingleNode("/INI/Section[Name='" + Section + "']/" + Key);

            if (!(item==null))
                item.InnerText = Value;
            else
            {
                // Create new section with supplied parameters
                XmlElement newSection = doc.CreateElement("Section");
                //newitem.SetAttribute("Name", itemname);
                string innerxml = "<Name>" + Section + "</Name>";
                innerxml = innerxml + "<" + Key + ">" + Value + "</" + Key + ">";

                newSection.InnerXml = innerxml;
                root.AppendChild(newSection);
            }

            //save the output to a file

            doc.Save(path);
            xmlrdrdoc = null;
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <returns></returns>
        public string XmlReadValue(string Section, string Key)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            string temp = "";

            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("/INI/Section[Name=\"" + Section + "\"]");

            foreach (XmlNode xn in xnrdrList)
            {
                XmlNode node = xn[Key];
                if (node != null)
                    temp = node.InnerText.ToString();
                //Console.WriteLine(xn.InnerText);
            }
            
            return temp;
        }

        public XmlNode XmlReadNode(string Section)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            XmlNode xn = xmlrdrdoc.SelectSingleNode("/INI/Section[Name=\"" + Section + "\"]");
            return xn;
        }

        public XmlNode XmlReadNode(string xpath, string name)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            XmlNode xn = xmlrdrdoc.SelectSingleNode(xpath + "[Name=\"" + name + "\"]");
            return xn;
        }

        public XmlNode XmlReadNode(string xpath, string key, string value)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            XmlNode xn = xmlrdrdoc.SelectSingleNode(xpath + "[" + key + "=\"" + value + "\"]");
            return xn;
        }
        
        // Looks for the first section that has a Key/Value combination matching
        // AssociatedKey/AssociatedValue and returns the Value of the requested Key.
        public string XmlReadAssociatedValue(string Key, string AssociatedKey, string AssociatedValue)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            string temp = "";

            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("/INI/Section[" + AssociatedKey + "=\"" + AssociatedValue + "\"]");

            foreach (XmlNode xn in xnrdrList)
            {
                XmlNode node = xn[Key];
                if (node != null)
                {
                    temp = node.InnerText.ToString();
                    break;
                }
                //Console.WriteLine(xn.InnerText);
            }

            return temp;

        }

        public List<string> XmlReadSection(string Section)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            List<string> temp = new List<string>();

            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("/INI/Section[Name=\"" + Section + "\"]");
            foreach (XmlNode xn in xnrdrList)
            {
                foreach (XmlNode cnd in xn.ChildNodes)
                {
                    if (cnd.Name !="Name")
                        temp.Add(cnd.Name + ":" + cnd.InnerText.ToString());
                    //Console.WriteLine(xn.InnerText);
                }
            }

            return temp;

        }

        /*
        public string[] ListSectionNames_strarr()
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            if (arrListSectionNames == null)
            {
                StringBuilder temp = new StringBuilder(255);


                XPathNavigator navigator = xmlrdrdoc.CreateNavigator();

                XPathExpression expression = navigator.Compile("/GEAR/Item/Name");// "//@Name"); //

                //expression.AddSort("Item"
                expression.AddSort("../Type", XmlSortOrder.Descending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);
                expression.AddSort("../Name", XmlSortOrder.Ascending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);

                XPathNodeIterator iterator = navigator.Select(expression);

                foreach (XPathNavigator item in iterator)
                {
                    temp.Append(item.Value.ToString());
                    temp.Append('\n');
                    //Console.WriteLine(xn.InnerText);
                }
                temp.Length = temp.Length - 1; //remove last element
                arrListSectionNames = temp.ToString().Split('\n');

                XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("//@Name");
                foreach (XmlNode xn in xnrdrList)
                {
                    temp.Append(xn.InnerText.ToString());
                    temp.Append('\n');
                    //Console.WriteLine(xn.InnerText);
                }
                temp.Length = temp.Length - 1; //remove last element
                arrListSectionNames = temp.ToString().Split('\n');
 
            }

            return arrListSectionNames;
            
        } */

        public List<string> stListSectionNames()
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            if (listListSectionNames.Count == 0)
            {
                XmlNodeList nodes = xmlrdrdoc.SelectNodes("/INI/Section/Name");
                foreach (XmlNode node in nodes)
                    listListSectionNames.Add(node.InnerText);
            }
            return listListSectionNames;
        }

        public List<string> stListSectionNames(string Type)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            if (listListSectionNames.Count == 0)
            {
                XmlNodeList nodes = xmlrdrdoc.SelectNodes("/INI/Section[Type=\"" + Type + "\"]/Name");// "//@Name"); //
                foreach (XmlNode node in nodes)
                    listListSectionNames.Add(node.InnerText);
            }
            return listListSectionNames;
        }

        public List<string> stListSectionNames2()
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            if (listListSectionNames.Count == 0)
            {
                //StringBuilder temp = new StringBuilder(255);

                XPathNavigator navigator = xmlrdrdoc.CreateNavigator();

                XPathExpression expression = navigator.Compile("/INI/Section/Name");// "//@Name"); //

                //expression.AddSort("Item"
                expression.AddSort("../Type", XmlSortOrder.Descending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);
                expression.AddSort("../Name", XmlSortOrder.Ascending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);

                XPathNodeIterator iterator = navigator.Select(expression);

                foreach (XPathNavigator item in iterator)
                {
                    listListSectionNames.Add(item.Value.ToString());
                }
            }
            return listListSectionNames;
        }
        public List<string> stListSectionNames2(string Type)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            if (listListSectionNames.Count == 0)
            {
                //StringBuilder temp = new StringBuilder(255);

                XPathNavigator navigator = xmlrdrdoc.CreateNavigator();

                XPathExpression expression = navigator.Compile("/INI/Section[Type=\"" + Type + "\"]/Name");// "//@Name"); //

                //expression.AddSort("Item"
                expression.AddSort("../Type", XmlSortOrder.Descending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);
                expression.AddSort("../Name", XmlSortOrder.Ascending, XmlCaseOrder.UpperFirst, string.Empty, XmlDataType.Text);

                XPathNodeIterator iterator = navigator.Select(expression);

                foreach (XPathNavigator item in iterator)
                {
                    listListSectionNames.Add(item.Value.ToString());
                }
            }
            return listListSectionNames;
        }

        /*
        public void WriteSectionNames(string SelectionName, string TypeString)
        {
            arrListSectionNames = null;
                //WritePrivateProfileSectionNames(SelectionName, "Type="+TypeString , path);
 
        }*/

        public void AddSection(string sectionname, List<string> subsectionnames, List<string> subsectionvalues)
        {
            XmlTextReader reader = new XmlTextReader(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlElement root = doc.DocumentElement;
            XmlElement newSection = doc.CreateElement("Section");
            //newitem.SetAttribute("Name", itemname);
            string innerxml = "";
            //innerxml = "<Type>" + itemtype + "</Type>" +
            innerxml = "<Name>" + sectionname + "</Name>";

            for (int Progress = 0; Progress < subsectionnames.Count; Progress++)
                innerxml = innerxml + "<" + subsectionnames[Progress] + ">" + subsectionvalues[Progress] + "</" + subsectionnames[Progress] + ">";

            newSection.InnerXml = innerxml;
            root.AppendChild(newSection);

            doc.Save(path);

            listListSectionNames.Add(sectionname);

            // Read in new
            xmlrdrdoc = null;
            //arrListSectionNames = null;
            //listListSectionNames.Clear();
        }

        private static void ConvertIni2Xml(List<string> iniNames, string xmlName)
        {
            bool xmlNeedsUpdate;

            if (File.Exists(xmlName))
            {
                // If any of the INI files used to create the XML file is newer than 
                // the XML file then the XML file needs to be rebuilt.
                DateTime xmlWriteTime = System.IO.File.GetLastWriteTimeUtc(xmlName);
                xmlNeedsUpdate = false;

                foreach (string iniName in iniNames)
                {
                    if (File.GetLastWriteTime(iniName) >= xmlWriteTime)
                        xmlNeedsUpdate = true;
                }
            }
            else
            {
                xmlNeedsUpdate = true;
            }

            if (xmlNeedsUpdate)
            {
                XmlTextWriter writer = new XmlTextWriter(xmlName, new System.Text.ASCIIEncoding());               
                try
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    writer.WriteStartDocument();
                    writer.WriteComment("Comment");
                    writer.WriteStartElement("INI");

                    string line;
                    bool sectionIsOpen = false;

                    // Read each INI file and write its data to the XML file
                    foreach (string iniName in iniNames)
                    {
                        StreamReader file = new System.IO.StreamReader(iniName);
                        int lineNumber = 0;

                        // Keep reading lines until the end of the file
                        while ((line = file.ReadLine()) != null)
                        {
                            lineNumber++;
                            // Ignore any empty lines
                            if (line.Length > 0)
                            {
                                line = line.TrimEnd();
                                // Section headers in INI files look like:
                                // [Section]
                                if (line.StartsWith("[") && line.EndsWith("]"))
                                {
                                    // --- This line is a section header ---
                                    // Terminate the previous Xml element if there was 
                                    // already a section open.
                                    if (sectionIsOpen)
                                        writer.WriteEndElement();

                                    // Write a new XML element to hold this INI section
                                    writer.WriteStartElement("Section");
                                    sectionIsOpen = true;
                                    string sectionName = line.Substring(1, line.Length - 2);
                                    writer.WriteElementString("Name", sectionName);
                                }
                                else if (line.Contains("="))
                                {
                                    // --- This line is a property assignment line ---
                                    string propName = line.Substring(0, line.IndexOf("="));
                                    propName = propName.Replace("[", "");
                                    propName = propName.Replace("]", "");
                                    propName = propName.Replace("(", "");
                                    propName = propName.Replace(")", "");
                                    string propValue = line.Substring(line.IndexOf("=") + 1);

                                    if (propValue.StartsWith("\""))
                                    {
                                        propValue = propValue.Substring(1, propValue.Length - 2);
                                    }
                                    writer.WriteElementString(propName, propValue);
                                }
                                else if (line[0] == ';')
                                {
                                    // Comment lines start with a semicolon, ignore them.
                                }
                                else
                                    throw new FileFormatException("File format is invalid on line " + lineNumber + "\r\nFile: " + iniName);
                            }
                        }
                        file.Close();
                    }
                    if (sectionIsOpen)
                        writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                    writer.Close();
                }
                catch (Exception)
                {
                    writer.Close();
                    File.Delete(xmlName);

                    // Re-throw the exception.  This code does not consume the exception.
                    // It just to makes sure any incomplete XML file is deleted so next
                    // time the application runs it will attempt to build it again.
                    throw;
                }
            }
        }
        public List<string> XmlSearchSection(string Searchfor)
        {
            if (xmlrdrdoc == null)
            {
                xmlrdrdoc = new XmlDocument();
                xmlrdrdoc.Load(path);
            }

            List<string> searchresult = new List<string>();

            // Search for //INI/Section[contains(.,"searchtext")]/Name

            //Gives a nodelist of all Name nodes, just iterate and highlight them
            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("//INI/Section[contains(translate(.,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + Searchfor.ToUpperInvariant() + "\")]/Name");
            foreach (XmlNode xn in xnrdrList)
            {
                searchresult.Add(xn.InnerText);
            }

            return searchresult;

        }

        // Clear duplicate items from an xml item/weapon file
        // TODO: This is an ugly method that probably could be done in an easier
        // to understand, more correct, or faster way.  It does seem to work
        // though, so I'm not going to try to rewrite it right now.
        public static void PurgeDuplicates(string InputFile)
        {
            // A tree model to store the nodes in
            TreeModel model = new TreeModel();

            // rootnode
            Node ndroot = new Node("INI");
            model.Nodes.Add(ndroot);

            XmlDocument xmlrdrdoc = new XmlDocument();

            xmlrdrdoc.Load(InputFile);

            // get a list of all items
            XmlNodeList xnrdrList = xmlrdrdoc.SelectNodes("/INI/Section");
            foreach (XmlNode xn in xnrdrList)
            {
                Node ndparent = ndroot;
                Node ndchild = null;
                bool bFound;

                string[] strParts = new string[]
                {
                    xn.GetElement("Type", ""),
                    xn.GetElement("Part1", ""),
                    xn.GetElement("Part2", ""),
                    xn.GetElement("Part3", ""),
                    xn.GetElement("Part4", ""),
                    xn.GetElement("Part5", ""),
                    xn.GetElement("Part6", ""),
                    xn.GetElement("Part7", ""),
                    xn.GetElement("Part8", ""),
                    xn.GetElement("Part9", ""),
                    xn.GetElement("Part10", ""),
                    xn.GetElement("Part11", ""),
                    xn.GetElement("Part12", ""),
                    xn.GetElement("Part13", ""),
                    xn.GetElement("Part14", ""),
                    xn.GetElement("Name", ""),
                    xn.GetElement("Rating", ""),
                    xn.GetElement("Description", ""),
                    xn.GetElement("RemAmmo_Quantity", ""),
                    xn.GetElement("Quality", ""),
                    xn.GetElement("Level", ""),
                };

                for (int partindex = 0; partindex < 21; partindex++)
                {
                    // All sections
                    // read the xml values
                    bFound = false;

                    for (int ndcnt = 0; ndcnt < ndparent.Nodes.Count; ndcnt++)
                    {
                        if (ndparent.Nodes[ndcnt].Text == strParts[partindex])
                        {
                            bFound = true;
                            ndparent = ndparent.Nodes[ndcnt];
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        ndchild = new ColoredTextNode();
                        ndchild.Text = strParts[partindex];
                        //                        ndchild.Expand();
                        ndparent.Nodes.Add(ndchild);
                        ndparent = ndchild;
                    }
                }
            }
            //            LockerTreetry2.EndUpdate();

            List<string> listNames = new List<string>();

            XmlTextWriter writer = new XmlTextWriter(InputFile, new System.Text.ASCIIEncoding());
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("Comment");
            writer.WriteStartElement("INI");

            for (int ndcntrt = 0; ndcntrt < ndroot.Nodes.Count; ndcntrt++)
            {
                Node ndtype = ndroot.Nodes[ndcntrt];
                for (int ndcnttype = 0; ndcnttype < ndtype.Nodes.Count; ndcnttype++)
                {
                    Node ndpart1 = ndtype.Nodes[ndcnttype];

                    for (int ndcntpart1 = 0; ndcntpart1 < ndpart1.Nodes.Count; ndcntpart1++)
                    {
                        Node ndpart2 = ndpart1.Nodes[ndcntpart1];

                        for (int ndcntpart2 = 0; ndcntpart2 < ndpart2.Nodes.Count; ndcntpart2++)
                        {
                            Node ndpart3 = ndpart2.Nodes[ndcntpart2];
                            for (int ndcntpart3 = 0; ndcntpart3 < ndpart3.Nodes.Count; ndcntpart3++)
                            {
                                Node ndpart4 = ndpart3.Nodes[ndcntpart3];

                                for (int ndcntpart4 = 0; ndcntpart4 < ndpart4.Nodes.Count; ndcntpart4++)
                                {
                                    Node ndpart5 = ndpart4.Nodes[ndcntpart4];

                                    for (int ndcntpart5 = 0; ndcntpart5 < ndpart5.Nodes.Count; ndcntpart5++)
                                    {
                                        Node ndpart6 = ndpart5.Nodes[ndcntpart5];

                                        for (int ndcntpart6 = 0; ndcntpart6 < ndpart6.Nodes.Count; ndcntpart6++)
                                        {
                                            Node ndpart7 = ndpart6.Nodes[ndcntpart6];

                                            for (int ndcntpart7 = 0; ndcntpart7 < ndpart7.Nodes.Count; ndcntpart7++)
                                            {
                                                Node ndpart8 = ndpart7.Nodes[ndcntpart7];

                                                for (int ndcntpart8 = 0; ndcntpart8 < ndpart8.Nodes.Count; ndcntpart8++)
                                                {
                                                    Node ndpart9 = ndpart8.Nodes[ndcntpart8];

                                                    for (int ndcntpart9 = 0; ndcntpart9 < ndpart9.Nodes.Count; ndcntpart9++)
                                                    {
                                                        Node ndpart10 = ndpart9.Nodes[ndcntpart9];

                                                        for (int ndcntpart10 = 0; ndcntpart10 < ndpart10.Nodes.Count; ndcntpart10++)
                                                        {
                                                            Node ndpart11 = ndpart10.Nodes[ndcntpart10];

                                                            for (int ndcntpart11 = 0; ndcntpart11 < ndpart11.Nodes.Count; ndcntpart11++)
                                                            {
                                                                Node ndpart12 = ndpart11.Nodes[ndcntpart11];

                                                                for (int ndcntpart12 = 0; ndcntpart12 < ndpart12.Nodes.Count; ndcntpart12++)
                                                                {
                                                                    Node ndpart13 = ndpart12.Nodes[ndcntpart12];

                                                                    for (int ndcntpart13 = 0; ndcntpart13 < ndpart13.Nodes.Count; ndcntpart13++)
                                                                    {
                                                                        Node ndpart14 = ndpart13.Nodes[ndcntpart13];

                                                                        for (int ndcntpart14 = 0; ndcntpart14 < ndpart14.Nodes.Count; ndcntpart14++)
                                                                        {

                                                                            Node ndpart15 = ndpart14.Nodes[ndcntpart14];
                                                                            for (int ndcntpart15 = 0; ndcntpart15 < ndpart15.Nodes.Count; ndcntpart15++)
                                                                            {
                                                                                Node ndpart16 = ndpart15.Nodes[ndcntpart15];
                                                                                for (int ndcntpart16 = 0; ndcntpart16 < ndpart16.Nodes.Count; ndcntpart16++)
                                                                                {
                                                                                    Node ndpart17 = ndpart16.Nodes[ndcntpart16];
                                                                                    for (int ndcntpart17 = 0; ndcntpart17 < ndpart17.Nodes.Count; ndcntpart17++)
                                                                                    {
                                                                                        Node ndpart18 = ndpart17.Nodes[ndcntpart17];
                                                                                        for (int ndcntpart18 = 0; ndcntpart18 < ndpart18.Nodes.Count; ndcntpart18++)
                                                                                        {
                                                                                            Node ndpart19 = ndpart18.Nodes[ndcntpart18];
                                                                                            for (int ndcntpart19 = 0; ndcntpart19 < ndpart19.Nodes.Count; ndcntpart19++)
                                                                                            {
                                                                                                Node ndpart20 = ndpart19.Nodes[ndcntpart19];

                                                                                                writer.WriteStartElement("Section");
                                                                                                writer.WriteElementString("Name", ndpart15.Text);
                                                                                                writer.WriteElementString("Type", ndtype.Text);
                                                                                                writer.WriteElementString("Rating", ndpart16.Text);
                                                                                                writer.WriteElementString("Description", ndpart17.Text.Replace('\"', ' ').Trim());

                                                                                                writer.WriteElementString("Part1", ndpart1.Text);
                                                                                                writer.WriteElementString("Part2", ndpart2.Text);
                                                                                                writer.WriteElementString("Part3", ndpart3.Text);
                                                                                                writer.WriteElementString("Part4", ndpart4.Text);
                                                                                                writer.WriteElementString("Part5", ndpart5.Text);
                                                                                                writer.WriteElementString("Part6", ndpart6.Text);
                                                                                                writer.WriteElementString("Part7", ndpart7.Text);
                                                                                                writer.WriteElementString("Part8", ndpart8.Text);
                                                                                                writer.WriteElementString("Part9", ndpart9.Text);
                                                                                                writer.WriteElementString("Part10", ndpart10.Text);
                                                                                                writer.WriteElementString("Part11", ndpart11.Text);
                                                                                                writer.WriteElementString("Part12", ndpart12.Text);
                                                                                                writer.WriteElementString("Part13", ndpart13.Text);
                                                                                                writer.WriteElementString("Part14", ndpart14.Text);
                                                                                                writer.WriteElementString("RemAmmo_Quantity", ndpart18.Text);
                                                                                                writer.WriteElementString("Quality", ndpart19.Text);
                                                                                                writer.WriteElementString("Level", ndpart20.Text);
                                                                                                writer.WriteEndElement();
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }

    public static class XmlNodeExtensions
    {
        // These XmlNode extensions are used to handle some repetitive error
        // handling and data type conversion related to getting values which may
        // or may not exist from an XmlNode.
        public static string GetElement(this XmlNode parent, string elementname)
        {
            if (parent == null)
                throw new NullReferenceException("xml node was null");

            XmlNode child = parent[elementname];
            if (child == null)
                throw new FormatException("Element " + elementname + " not found in xml node");
            else
                return child.InnerText;
        }

        public static string GetElement(this XmlNode parent, string elementname, string defaultvalue)
        {
            if (parent == null)
                return defaultvalue;

            XmlNode child = parent[elementname];
            if (child == null)
                return defaultvalue;
            else
                return child.InnerText;
        }

        public static int GetElementAsInt(this XmlNode parent, string elementname)
        {
            if (parent == null)
                throw new NullReferenceException("xml node was null");

            string elementtext = parent.GetElement(elementname);
            int outvalue;
            if (!int.TryParse(elementtext, out outvalue))
                throw new FormatException();

            return outvalue;
        }

        public static int GetElementAsInt(this XmlNode parent, string elementname, int defaultvalue)
        {
            if (parent == null)
                return defaultvalue;

            string elementtext = parent.GetElement(elementname, "");
            int outvalue;
            if (int.TryParse(elementtext, out outvalue))
                return outvalue;
            return defaultvalue;
        }

        public static void SetElement(this XmlNode parent, string elementname, string elementvalue)
        {
            XmlNode child = parent[elementname];
            if (child == null)
                child = parent.OwnerDocument.CreateElement(elementname);
            child.InnerText = elementvalue;
        }

        public static bool TryGetElement(this XmlNode parent, string elementname, out string stringout)
        {
            if (parent == null)
            {
                stringout = string.Empty;
                return false;
            }

            XmlNode child = parent[elementname];
            if (child == null)
            {
                stringout = string.Empty;
                return false;
            }

            stringout = child.InnerText;
            if (stringout == null)
                stringout = string.Empty;
            return true;
        }

        public static bool TryGetElementAsInt(this XmlNode parent, string elementname, out int value)
        {
            if (parent == null)
            {
                value = 0;
                return false;
            }

            string elementtext;
            if (!parent.TryGetElement(elementname, out elementtext))
            {
                value = 0;
                return false;
            }
            bool result = int.TryParse(elementtext, out value);
            return result;
        }
    }
}
