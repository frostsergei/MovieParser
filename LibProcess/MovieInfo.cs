// <copyright file="MovieInfo.cs" company="LETI">
//     All rights reserved.
// </copyright>
// <author>Sergei Morozov</author>
// <summary>This file describes data processing.</summary>
namespace LibProcess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Html; 
    using Movie;

    /// <summary>
    /// This class processes movie data.
    /// </summary>
    public class MovieInfo
    {
        /// <summary>
        /// Array of Movie information.
        /// </summary>
        private MovieData[] data;

        /// <summary>
        /// Webpage content.
        /// </summary>
        private string page;

        /// <summary>
        /// Initializes a new instance of the MovieInfo class.
        /// </summary>
        /// <param name="s">Page content.</param>
        public MovieInfo(string s)
        {
            this.page = s;
            this.data = new MovieData[0];
        }

        /// <summary>
        /// Aquire movie data from strings.
        /// </summary>
        /// <param name="words">Set of info strings.</param>
        /// <param name="offset">Help variable. Used to negotiate last empty string.</param>
        /// <returns>Array of found movie data.</returns>
        public MovieData[] StringProcessData(string[] words, int offset)
        {
            this.StringProcess(words, offset);
            return this.data;
        }

        /// <summary>
        /// Get movie data from webpage.
        /// </summary>
        /// <param name="filter">Key of searching filter.</param>
        /// <param name="offset">Help variable. Used to negotiate last empty string..</param>
        /// <returns>Array of found movie strings.</returns>
        public string[] SetupMovieData(string filter, int offset)
        {
            string[] words = this.ParseData(filter);
            this.data = this.StringProcess(words, offset);
            return words;
        }

        /// <summary>
        /// Turn movie strings into movie data structure.
        /// </summary>
        /// <param name="wordsSet">Array of movie strings.</param>
        /// <param name="offset">Help variable. Used to negotiate last empty string..</param>
        /// <returns>Array of found movie data structures.</returns>
        private MovieData[] StringProcess(string[] wordsSet, int offset)
        {
            this.data = new MovieData[wordsSet.Count()];

            for (int i = 0; i < wordsSet.Count(); i++)
            {
                string[] words = wordsSet[i].Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                for (int k = 0; k < words.Length; k++)
                {
                    words[k] = words[k].Replace("  ", " ");
                }

                MovieData m = new MovieData();

                m.Name = string.Empty;
                int mem;

                int count = words.Count() - offset;

                for (mem = 0; mem < count; mem++)
                {
                    m.Name += words[mem].ToString() + " ";
                    if (words[mem + 1][0] == '(')
                    {
                        break;
                    }
                }

                m.Year = words[mem + 1];
                m.Year = m.Year.Trim(new char[] { '(', ')' });

                m.Origin = string.Empty;
                for (mem = mem + 2; mem < count - 3; mem++)
                {
                    m.Origin += words[mem].ToString() + " ";
                    if (words[mem + 1][0] == '(')
                    {
                        break;
                    }
                }

                m.Rating = words[count - 3].ToString();
                m.Votes = words[count - 2].ToString();
                m.Votes = m.Votes.Trim(new char[] { '(', ')' });

                this.data[i] = m;
            }

            return this.data;
        }

        /// <summary>
        /// Get movie data from webpage.
        /// </summary>
        /// <param name="filter">Key of searching filter.</param>
        /// <returns>Array of found movie strings.</returns>
        private string[] ParseData(string filter)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(this.page);
            var moviesItemsLinq = document.All.Where(m => m.LocalName == "tr" && m.ChildElementCount == 4
            && m.Text().Contains(filter) && m.Id != null);

            List<string> words = new List<string>();

            foreach (var emp in moviesItemsLinq)
            {
                string d = emp.Text().Insert(emp.Text().IndexOf(")") + 1, " ");
                string[] w = d.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string s = string.Empty;
                for (int i = 1; i < w.Count(); i++)
                {
                    s += w[i] + " ";
                }

                words.Add(s);
            }

            return words.ToArray();
        }
    }
}