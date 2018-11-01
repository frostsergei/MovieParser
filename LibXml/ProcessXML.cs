// <copyright file="ProcessXML.cs" company="LETI">
//     All rights reserved.
// </copyright>
// <author>Sergei Morozov</author>
// <summary>This file describes function to work with XML.</summary>
namespace LibXml
{
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using Movie;

    /// <summary>
    /// This class processes the XML data.
    /// </summary>
    public class ProcessXML
    {
        /// <summary>
        /// Initializes a new instance of the MovieInfo class.
        /// </summary>
        /// <param name="dataSet">Updated dataset.</param>
        /// <param name="data">MovieData to add to XML.</param>
        public static void AddRow(DataSet dataSet, MovieData data)
        {
            if (dataSet.Tables.Count == 0)
            {
                DataTable dt = new DataTable(); // Create empty (for now) table.
                dt.TableName = "Movies"; // Table name
                dt.Columns.Add("Name");
                dt.Columns.Add("Year");
                dt.Columns.Add("Origin");
                dt.Columns.Add("Rating");
                dt.Columns.Add("Votes");

                dataSet.Tables.Add(dt);

                DataRow row = dataSet.Tables["Movies"].NewRow();
                row["Name"] = data.Name;
                row["Year"] = data.Year;
                row["Origin"] = data.Origin;
                row["Rating"] = data.Rating;
                row["Votes"] = data.Votes;

                dataSet.Tables["Movies"].Rows.Add(row);
            }
            else
            {
                DataRow row = dataSet.Tables["Movies"].NewRow();
                row["Name"] = data.Name;
                row["Year"] = data.Year;
                row["Origin"] = data.Origin;
                row["Rating"] = data.Rating;
                row["Votes"] = data.Votes;

                dataSet.Tables["Movies"].Rows.Add(row);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MovieInfo class.
        /// </summary>
        /// <param name="file">Name of updated file.</param>
        /// <param name="dataSet">Info dataset to write.</param>
        public static void SaveDataSetXML(string file, DataSet dataSet)
        {
            dataSet.WriteXml(file);
        }

        /// <summary>
        /// Initializes a new instance of the MovieInfo class.
        /// </summary>
        /// <param name="file">Name of read file.</param>
        /// <param name="dataSet">Destination dataset.</param>
        public static void LoadXML(string file, DataSet dataSet)
        {
            if (File.Exists(file))
            {
                dataSet.ReadXml(file);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.TableName = "Movies"; // название таблицы
                dt.Columns.Add("Name");
                dt.Columns.Add("Year");
                dt.Columns.Add("Origin");
                dt.Columns.Add("Rating");
                dt.Columns.Add("Votes");
                dataSet.Tables.Add(dt);
                dataSet.WriteXml(file);
            }
        }

        /// <summary>
        /// Read movies from dataset.
        /// </summary>
        /// <param name="key">Search key. Used to filter movies.</param>
        /// <param name="dataSet">Destination dataset.</param>
        /// <returns>Array of found movie strings.</returns>
        public static string[] GetMovieXML(string key, DataSet dataSet)
        {
            List<string> str = new List<string>();

            if (dataSet.Tables.Count != 0)
            {
                foreach (DataRow item in dataSet.Tables["Movies"].Rows)
                {
                    string s = string.Empty;

                    if (item["Name"] != null)
                    {
                        s += item["Name"].ToString() + " ";
                    }

                    if (item["Year"] != null)
                    {
                        s += "(" + item["Year"].ToString() + ")" + " ";
                    }

                    if (item["Origin"] != null)
                    {
                        s += item["Origin"].ToString() + " ";
                    }

                    if (item["Rating"] != null)
                    {
                        s += item["Rating"].ToString() + " ";
                    }

                    if (item["Votes"] != null)
                    {
                        s += "(" + item["Votes"].ToString() + ")" + " ";
                    }

                    s = s.Replace("  ", " ");

                    if (s.IndexOf(key) != -1)
                    {
                        str.Add(s);
                    }
                }
            }

            return str.ToArray();
        }

        /// <summary>
        /// Chech if string is (partially) repeared.
        /// </summary>
        /// <param name="val">Checked string.</param>
        /// <param name="xmlVal">Saved strings.</param>
        /// <returns>Array of found movie strings.</returns>
        public static bool CheckStrings(string val, string[] xmlVal)
        {
            string valsp = val.Replace(" ", string.Empty);
            valsp = valsp.Replace(" ", string.Empty);

            foreach (string v in xmlVal)
            {
                string vsp = v.Replace(" ", string.Empty);
                vsp = vsp.Replace(" ", string.Empty);

                if ((vsp.IndexOf(valsp) != -1) || (valsp.IndexOf(vsp) != -1))
                {
                    return true;
                }
            }

            return false;
        }
    }
}