using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using AngleSharp.Extensions;

namespace Parser_UI
{
    class MovieInfo
    {
        private MovieData[] data;

        private string Page;
        public MovieInfo(string str) { Page = str; data = new MovieData[0]; }


        public string[] ParseData(string filter)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(Page);
            var MoviesItemsLinq = document.All.Where(m => m.LocalName == "tr" && m.ChildElementCount == 4
            && m.Text().Contains(filter) && m.Id != null);

            List<string> words = new List<string>();

            foreach (var emp in MoviesItemsLinq)
            {
                //Get set of strings
                string d = emp.Text().Insert(emp.Text().IndexOf(")") + 1, " ");
                string[] w = d.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string s = "";
                for (int i = 1; i < w.Count(); i++) s += w[i] + " ";
                words.Add(s);

            }
            return words.ToArray();
        }

        public MovieData[] stringProcess(string[] wordsSet, int offset)
        {
            data = new MovieData[wordsSet.Count()];

            for (int i = 0; i < wordsSet.Count(); i++)
            {
                string[] words = wordsSet[i].Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                for (int k = 0; k < words.Length; k++) words[k] = words[k].Replace("  "," ");

                MovieData m = new MovieData();
                //Find Name
                m.Name = "";
                int mem;

                int count= words.Count()-offset;


                for (mem = 0; mem < count; mem++)
                {
                    m.Name += words[mem].ToString() + " ";
                    if (words[mem + 1][0] == '(') break;
                }

                //Find Year
                m.Year = words[mem + 1];
                m.Year = m.Year.Trim(new char[] { '(', ')' });

                //Find Origin
                m.Origin = "";
                for (mem = mem + 2; mem < count - 3; mem++)
                {
                    m.Origin += words[mem].ToString() + " ";
                    if (words[mem + 1][0] == '(') break;
                }

                //Find Rating
                m.Rating = words[count - 3].ToString();

                //Find Votes
                m.Votes = words[count - 2].ToString();
                m.Votes = m.Votes.Trim(new char[] { '(', ')' });

                data[i] = m;
            }
            return data;
        }

        public MovieData[] stringProcessData(string[] words, int offset)
        {
            stringProcess(words, offset);
            return data;
        }

        public string[] SetupMovieData(string filter, int offset)
        {
            string[] words = ParseData(filter);

            data=stringProcess(words, offset);

            return words;
        }

        public MovieData GetMovie(int i) { return data[i]; }
        public MovieData[] GetMovies() { return data; }
    }
}