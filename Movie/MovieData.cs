// <copyright file="MovieData.cs" company="LETI">
//     All rights reserved.
// </copyright>
// <author>Sergei Morozov</author>
// <summary>This file describes movie class.</summary>
namespace Movie
{
    /// <summary>
    /// Class contains movie info.
    /// </summary>
    public class MovieData
    {
        /// <summary>
        /// Movie Russian name.
        /// </summary>
        private string name;

        /// <summary>
        /// Movie English name.
        /// </summary>
        private string origin;

        /// <summary>
        /// Year of movie release.
        /// </summary>
        private string year;

        /// <summary>
        /// Movie Kinopoisk rating.
        /// </summary>
        private string rating;

        /// <summary>
        /// Number of voters on Kinopoisk.
        /// </summary>
        private string votes;

        /// <summary>
        /// Initializes a new instance of the MovieData class.
        /// </summary>
        public MovieData()
        {
            this.Name = string.Empty;
            this.Year = string.Empty;
            this.Origin = string.Empty;
            this.Rating = string.Empty;
            this.Votes = string.Empty;
        }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        /// <value>The Name property gets/sets the value of the string field, name.</value>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        /// <summary>
        /// Gets or sets Origin.
        /// </summary>
        /// <value>The Name property gets/sets the value of the string field, origin.</value>
        public string Origin
        {
            get => this.origin;
            set => this.origin = value;
        }

        /// <summary>
        /// Gets or sets Year.
        /// </summary>
        /// <value>The Name property gets/sets the value of the string field, _name.</value>
        public string Year
        {
            get => this.year;
            set => this.year = value;
        }

        /// <summary>
        /// Gets or sets Rating.
        /// </summary>
        /// <value>The Name property gets/sets the value of the string field, rating.</value>
        public string Rating
        {
            get => this.rating;
            set => this.rating = value;
        }

        /// <summary>
        /// Gets or sets Votes.
        /// </summary>
        /// <value>The Name property gets/sets the value of the string field, votes.</value>
        public string Votes
        {
            get => this.votes;
            set => this.votes = value;
        }
    }
}
