using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibParse;
using LibProcess;
using LibXml;
using Movie;
using System.Runtime.InteropServices;

namespace Parser_Libs
{
    public partial class Form1 : Form
    {
        private static string PageText;
        public static string GetHTML() { return PageText; }

        private MovieData[] Movies;
 
        private static DataSet dataSet = new DataSet();

        string[] xmlVal;

        public Form1()
        {
            InitializeComponent();
            ProcessXML.LoadXML("Movies.xml", dataSet);
            PageText = LibParse.ProcesParse.LoadPage(@"https://www.kinopoisk.ru/top/");
            if (PageText == null)
            {
                if (PageText == LibParse.ProcesParse.LoadLocalHtml())
                {
                    buttonSearch.Enabled = false;
                    buttonSave.Enabled = false;
                    comboBoxMovies.Enabled = false;
                    textBoxName.Enabled = false;
                    richTextBoxInfo.Text += "Application has no data to process!\n";
                }
                else richTextBoxInfo.Text += "Application works in offline mode!\n";
            }
            else richTextBoxInfo.Text += "Application works in online mode!\n";
            dataGridViewMovies.RowCount = 1;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            comboBoxMovies.Items.Clear();
            string key = textBoxName.Text;

            if (key == "") key = " ";

            MovieInfo f = new MovieInfo(PageText);

            string[] st = LibXml.ProcessXML.getMovieXML(key, dataSet);
            comboBoxMovies.Items.AddRange(st);

            xmlVal = new string[comboBoxMovies.Items.Count];

            for (int i = 0; i < comboBoxMovies.Items.Count; i++)
                xmlVal[i] = comboBoxMovies.Items[i].ToString();

            Movies = f.stringProcessData(xmlVal, -1);

            if (comboBoxMovies.Items.Count != 0)
            {
                comboBoxMovies.SelectedIndex = 0;

                richTextBoxInfo.Text += "Found data in XML!\n";

                DialogResult dialogResult = MessageBox.Show("Ok, I found something you want. Do you want to search webpage for more results?",
                    "Interesting situation!",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    comboBoxMovies.Items.Clear();

                    string[] movies = f.SetupMovieData(key, 0);

                    Movies = f.stringProcessData(movies, 0);

                    comboBoxMovies.Items.AddRange(movies);
                    comboBoxMovies.SelectedIndex = 0;

                    richTextBoxInfo.Text += "Webpage data aquired!\n";
                }
                else richTextBoxInfo.Text += "Webpage search access canceled!\n";
            }
            else
            {
                string[] movies = f.SetupMovieData(key, 0);
                Movies = f.stringProcessData(movies, 0);
                comboBoxMovies.Items.AddRange(movies);
                comboBoxMovies.SelectedIndex = 0;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxMovies.Items.Count == 0) return;
            DataGridViewRow row = (DataGridViewRow)dataGridViewMovies.Rows[0].Clone();
            row.Cells[0].Value = Movies[comboBoxMovies.SelectedIndex].Name;
            row.Cells[1].Value = Movies[comboBoxMovies.SelectedIndex].Year;
            row.Cells[2].Value = Movies[comboBoxMovies.SelectedIndex].Origin;
            row.Cells[3].Value = Movies[comboBoxMovies.SelectedIndex].Rating;
            row.Cells[4].Value = Movies[comboBoxMovies.SelectedIndex].Votes;
            dataGridViewMovies.Rows.Add(row);
            string temp = "";
            for (int i = 0; i < 5; i++)
            {
                if ((i == 1) || (i == 4)) temp += "(";
                temp += row.Cells[i].Value.ToString();
                if (!((i == 1) || (i == 4))) temp += " ";
                if ((i == 1) || (i == 4)) temp += ") ";
            }

            temp = temp.Replace("  ", " ");
            if (!ProcessXML.checkStrings(temp, xmlVal))
            {
                richTextBoxInfo.Text += "Saved data to XML!\n";
                ProcessXML.addRow(dataSet, Movies[comboBoxMovies.SelectedIndex]);
                ProcessXML.saveDataSetXML("Movies.xml", dataSet);
            }
            else richTextBoxInfo.Text += "Did not saved data to XML! Data already exists!\n";

        }
    }
}
