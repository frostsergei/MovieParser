using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser_UI
{
    class MovieData
    {
        private string name;
        private string origin;
        private string year;
        private string rating;
        private string votes;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Origin
        {
            get => origin;
            set => origin = value;
        }

        public string Year
        {
            get => year;
            set => year = value;
        }

        public string Rating
        {
            get => rating;
            set => rating = value;
        }

        public string Votes
        {
            get => votes;
            set => votes = value;
        }

        public MovieData()
        {
            Name = "";
            Year = "";
            Origin = "";
            Rating = "";
            Votes = "";
        }

    }
}
