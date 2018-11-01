// <copyright file="Form1.cs" company="LETI">
//     All rights reserved.
// </copyright>
// <author>Sergei Morozov</author>
// <summary>This file describes application GUI and data transfers.</summary>
namespace Parser_Libs
{
    using System;
    using System.Data;
    using System.Windows.Forms;
    using LibXml;
    using Movie;

    /// <summary>
    /// This class desctribes application GUI.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// HTML webpage text.
        /// </summary>
        private static string pageText;

        /// <summary>
        /// Dataset to process saved data.
        /// </summary>
        private static DataSet dataSet = new DataSet();

        /// <summary>
        /// String describing data saved in local XML.
        /// </summary>
        private static string[] xmlVal;

        /// <summary>
        /// Array of movies data structures.
        /// </summary>
        private MovieData[] movies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1" /> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            ProcessXML.LoadXML("Movies.xml", dataSet);
            pageText = LibParse.ProcesParse.LoadPage(@"https://www.kinopoisk.ru/top/");
            if (pageText == null)
            {
                pageText = LibParse.ProcesParse.LoadLocalHtml();
                if (pageText == null)
                {
                    buttonSearch.Enabled = false;
                    buttonSave.Enabled = false;
                    comboBoxMovies.Enabled = false;
                    textBoxName.Enabled = false;
                    richTextBoxInfo.Text += "Application has no data to process!\n";
                }
                else
                {
                    richTextBoxInfo.Text += "Application works in offline mode!\n";
                }
            }
            else
            {
                richTextBoxInfo.Text += "Application works in online mode!\n";
            }

            dataGridViewMovies.RowCount = 1;
        }

        /// <summary>
        /// Search button actions: aquire webpage and setup movie data using it.
        /// </summary>
        /// <param name="sender">Standart WinForms object parameter.</param>
        /// <param name="e">Standart WinForms EventArgs parameter.</param>
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            comboBoxMovies.Items.Clear();
            string key = textBoxName.Text;

            if (key == string.Empty)
            {
                key = " ";
            }

            LibProcess.MovieInfo f = new LibProcess.MovieInfo(pageText);

            string[] st = LibXml.ProcessXML.GetMovieXML(key, dataSet);
            comboBoxMovies.Items.AddRange(st);

            xmlVal = new string[comboBoxMovies.Items.Count];

            for (int i = 0; i < comboBoxMovies.Items.Count; i++)
            {
                xmlVal[i] = comboBoxMovies.Items[i].ToString();
            }

            this.movies = f.StringProcessData(xmlVal, -1);

            if (comboBoxMovies.Items.Count != 0)
            {
                comboBoxMovies.SelectedIndex = 0;

                richTextBoxInfo.Text += "Found data in XML!\n";

                DialogResult dialogResult = MessageBox.Show(
                "Ok, I found something you want. Do you want to search webpage for more results?",
                "Interesting situation!",
                MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    comboBoxMovies.Items.Clear();

                    string[] moviesStr = f.SetupMovieData(key, 0);

                    this.movies = f.StringProcessData(moviesStr, 0);

                    comboBoxMovies.Items.AddRange(moviesStr);
                    comboBoxMovies.SelectedIndex = 0;

                    richTextBoxInfo.Text += "Webpage data aquired!\n";
                }
                else
                {
                    richTextBoxInfo.Text += "Webpage search access canceled!\n";
                }
            }
            else
            {
                string[] movies = f.SetupMovieData(key, 0);
                this.movies = f.StringProcessData(movies, 0);
                comboBoxMovies.Items.AddRange(movies);
                comboBoxMovies.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Save selected movie in XML file.
        /// </summary>
        /// <param name="sender">Standart WinForms object parameter.</param>
        /// <param name="e">Standart WinForms EventArgs parameter.</param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxMovies.Items.Count == 0)
            {
                return;
            }

            DataGridViewRow row = (DataGridViewRow)dataGridViewMovies.Rows[0].Clone();
            row.Cells[0].Value = this.movies[comboBoxMovies.SelectedIndex].Name;
            row.Cells[1].Value = this.movies[comboBoxMovies.SelectedIndex].Year;
            row.Cells[2].Value = this.movies[comboBoxMovies.SelectedIndex].Origin;
            row.Cells[3].Value = this.movies[comboBoxMovies.SelectedIndex].Rating;
            row.Cells[4].Value = this.movies[comboBoxMovies.SelectedIndex].Votes;

            dataGridViewMovies.Rows.Add(row);
            string temp = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                if ((i == 1) || (i == 4))
                {
                    temp += "(";
                }

                temp += row.Cells[i].Value.ToString();
                if (!((i == 1) || (i == 4)))
                {
                    temp += " ";
                }

                if ((i == 1) || (i == 4))
                {
                    temp += ") ";
                }
            }

            temp = temp.Replace("  ", " ");
            if (!ProcessXML.CheckStrings(temp, xmlVal))
            {
                this.richTextBoxInfo.Text += "Saved data to XML!\n";
                ProcessXML.AddRow(dataSet, this.movies[comboBoxMovies.SelectedIndex]);
                ProcessXML.SaveDataSetXML("Movies.xml", dataSet);
            }
            else
            {
                richTextBoxInfo.Text += "Did not saved data to XML! Data already exists!\n";
            }
        }
    }
}
