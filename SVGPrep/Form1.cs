using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;

namespace SVGPrep
{
    public partial class Form1 : Form
    {
        XDocument svg;
        string currentFile = "";
        bool isGridDirty = false;

        string formTitle = "SVG Prep";
        XNamespace ns = "http://www.w3.org/2000/svg";
        XNamespace xlink = "http://www.w3.org/1999/xlink";
        string hoverText = "\n.hover_group:hover path, .hover_group:hover ellipse, .hover_group:hover rect {stroke:#26a9e0;stroke-width:2px;}";


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// close the form only after checking with the user about whether they want to save the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            // Check if current file is dirty
            if (isGridDirty)
            {
                if (MessageBox.Show("You have unsaved changes in the current SVG file. If you click OK, any unsaved changes will be discarded",
                                    "Unsaved Changes",
                                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// open an SVG file, process it, and populate the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            // Check if current file is dirty
            if (isGridDirty)
            {
                if (MessageBox.Show("You have unsaved changes in the current SVG file. If you click OK, any unsaved changes will be discarded",
                                    "Unsaved Changes",
                                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }

            ALinksGrid.Visible = false;
            // Open file

            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "SVG Files|*.svg";
            if (openFile1.ShowDialog() != DialogResult.OK)
                return;

            svg = XDocument.Load(openFile1.FileName);

            // Process SVG DOM and populate the grid

            if (!svg.Root.Element(ns + "style").Value.Contains(".hover_group"))
                svg.Root.Element(ns + "style").Value += hoverText;

            ALinksGrid.AutoGenerateColumns = false;
            ALinksGrid.Rows.Clear();
            foreach (XElement alink in svg.Descendants(ns + "a"))
            {
                alink.SetAttributeValue("cursor", "pointer");   // set cursor="pointer"
                alink.SetAttributeValue("target", "_blank");    // set target="_blank"
                if(alink.Attribute("tabindex") == null)         // set tabindex="0" if not exist
                    alink.SetAttributeValue("tabindex", "0");

                // set class="hover_group" for all groups with <a> as immediate parent

                foreach (XElement group in alink.Elements(ns + "g"))
                {
                    XAttribute attr = group.Attribute("class");
                    if (attr != null)
                        attr.Value = "hover_group;" + attr.Value;
                    else
                    group.SetAttributeValue("class", "hover_group");
                }

                // create <title> if not exist in decendents

                XElement title = null;
                if(alink.Descendants(ns + "title").Count() > 0)
                    title = alink.Descendants(ns + "title").First();
                if ( title == null)
                {
                    title = new XElement(ns + "title", "");
                    alink.Add(title);
                }

                // create <desc> if not exist in descendents

                XElement desc = null;
                if (alink.Descendants(ns + "desc").Count() > 0)
                    desc = alink.Descendants(ns + "desc").First();
                if (desc == null)
                {
                    desc = new XElement(ns + "desc", "");
                    alink.Add(desc);
                }

                // add ids for <title> and <desc> if not exist

                string titleid, descid;
                if (title.Attribute("id") == null)
                {
                    titleid = GenerateId();
                    title.SetAttributeValue("id", titleid);
                }
                else titleid = title.Attribute("id").Value;

                if (desc.Attribute("id") == null)
                {
                    descid = GenerateId();
                    desc.SetAttributeValue("id", descid);
                }
                else descid = desc.Attribute("id").Value;

                alink.SetAttributeValue("aria-labelledby", titleid + " " + descid); // set aria-labelledby="[titleid] [descid]"

                // create a new row for the link in the grid

                ALinksGrid.Rows.Add(
                    alink.DescendantsAndSelf().First(el => el.Attribute("id") != null).Attribute("id").Value,   // id
                    alink.Attribute(xlink + "href").Value,                                                      // href
                    alink.Descendants(ns + "title").First().Value,                                              // title
                    alink.Descendants(ns + "desc").First().Value,                                               // desc
                    alink.Attribute("tabindex").Value,                                                          // tabindex
                    alink.Ancestors(ns + "a").Count() > 0 ? true : false,                                       // nested?
                    false);                                                                                     // delete link? (no by default)
            }
            ALinksGrid.Refresh();
            ALinksGrid.ColumnHeadersVisible = true;
            ALinksGrid.Visible = true;
            ALinksGrid.CellValueChanged += ALinksGrid_CellValueChanged;

            currentFile = openFile1.FileName;
            ButtonSave.Enabled = true;
            ButtonSaveAs.Enabled = true;
            this.Text = String.Format("{0} - {1}", formTitle, currentFile);
            isGridDirty = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Save As button clicked. update the SVG DOM and the grid and save to a user-defined file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "SVG files (*.svg)|*.svg";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    UpdateData();   // update SVG DOM and grid

                    Stream output = saveFileDialog1.OpenFile();
                    if (output != null)
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;
                        settings.NewLineOnAttributes = true;

                        XmlWriter writer = XmlWriter.Create(output, settings);
                        svg.Save(writer);
                        writer.Flush();
                        output.Close();
                    }

                    currentFile = saveFileDialog1.FileName;
                    this.Text = String.Format("{0} - {1}", formTitle, currentFile);
                    isGridDirty = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving the file. Try saving it in another file.\n" + ex.Message);
                }
            }

        }

        /// <summary>
        /// Save button clicked. update the SVG DOM and the grid and save to a user-defined file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (isGridDirty)
            {
                try
                {
                    UpdateData();   // update SVG DOM and grid

                    Stream output = File.OpenWrite(currentFile);
                    if (output != null)
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;
                        settings.NewLineOnAttributes = true;

                        XmlWriter writer = XmlWriter.Create(output, settings);
                        svg.Save(writer);
                        writer.Flush();
                        output.Close();
                    }

                    isGridDirty = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving the file. Try saving it in another file.\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// the user changed something in the grid. keep track of change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ALinksGrid_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            isGridDirty = true;
        }

        /// <summary>
        /// update the SVG DOM and the grid
        /// </summary>
        void UpdateData()
        {
            var alinks = svg.Descendants(ns + "a");
            ALinksGrid.CellValueChanged -= ALinksGrid_CellValueChanged;
            List<DataGridViewRow> toDelete = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in ALinksGrid.Rows)
            {
                XElement alink = svg.Descendants().First(el => (el.Attribute("id") != null) && (el.Attribute("id").Value == (string)row.Cells["ID"].Value));
                if (alink.Name.LocalName != "a")
                    alink = alink.Parent;
                if (alink == null)
                    throw new Exception(String.Format("Cannot find the <a> tag for the current row with href=\"{0}\"", row.Cells["id"].Value));

                // delete <a> from SVG DOM if specified, including any <title> or <desc> in it
                if ((bool)row.Cells["Delete"].Value)
                {
                    XElement parent = alink.Parent;
                    alink.Remove();
                    foreach (XElement title in alink.Elements(ns + "title"))
                        title.Remove();
                    foreach (XElement desc in alink.Elements(ns + "desc"))
                        desc.Remove();
                    parent.Elements().ElementAt(alink.ElementsBeforeSelf().Count()).AddBeforeSelf(alink.Elements());    //add the <a>'s children back
                    toDelete.Add(row);
                    continue;
                }

                alink.Descendants(ns + "title").First().Value = (string)row.Cells["Title"].Value;   // update <title>
                alink.Descendants(ns + "desc").First().Value = (string)row.Cells["Desc"].Value;     // update <desc>
                alink.Attribute("tabindex").Value = (string)row.Cells["TabIndex"].Value;            // update tabindex="#"

            }

            // remove deleted links from the grid

            ALinksGrid.Visible = false;
            foreach (var row in toDelete)
                ALinksGrid.Rows.Remove(row);
            ALinksGrid.Refresh();
            ALinksGrid.Visible = true;

            ALinksGrid.CellValueChanged += ALinksGrid_CellValueChanged;
        }

        /// <summary>
        /// when the button is clicked, this method deletes all title and desc tags in the SVG that are not already shown in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteTitles_Click(object sender, EventArgs e)
        {
            List<XElement> goodTitles = new List<XElement>();
            foreach (XElement alink in svg.Descendants(ns + "a"))
            {
                goodTitles.Add(alink.Descendants(ns + "title").First());
            }

            IEnumerable<XElement> titles = svg.Descendants(ns + "title");
            int i = titles.Count();
            while(i-- > 0)
            {
                XElement title = titles.ElementAt(i);
                if (!goodTitles.Contains(title))
                {
                    XElement desc = title.Parent.Element(ns + "desc");
                    title.Remove();
                    if (desc != null)
                        desc.Remove();
                }
                else
                {
                    //debug
                }
            }
        }

        /// <summary>
        /// generate a unique ID in the current SVG
        /// </summary>
        /// <returns></returns>
        private string GenerateId()
        {
            IEnumerable<XAttribute> ids = svg.Descendants().Where(x => x.Attribute("id") != null ).Attributes("id");
            string newId = "sp-";
            while (true)
            {
                newId += Guid.NewGuid().ToString("N").Substring(0, 5);
                foreach (XAttribute id in ids)
                {
                    if (id.Value == newId)
                        continue;
                }
                break;
            }
            return newId;
        }

        /// <summary>
        /// Open a clicked link in the browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ALinksGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)  // link column
            {
                Process.Start(ALinksGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }
    }

}
