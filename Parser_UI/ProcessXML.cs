using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Parser_UI
{
    class ProcessXML
    {
        public static void addRow(DataSet dataSet, MovieData data)
        {
            if (dataSet.Tables.Count == 0)
            {
                DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
                dt.TableName = "Movies"; // название таблицы
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

        public static void saveDataSetXML(string file, DataSet dataSet)
        {
            dataSet.WriteXml(file);
        }

        public static void LoadXML(string file, DataSet dataSet)
        {


            if (File.Exists(file)) // если существует данный файл
            {
                dataSet.ReadXml(file);

            }
            else
            {
                DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
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
        public static void getMovieXML(string key, ComboBox cmb, DataSet dataSet)
        {
            cmb.Items.Clear();
            if(dataSet.Tables.Count!=0)
            foreach (DataRow item in dataSet.Tables["Movies"].Rows)
            {
                
                string s = "";

                if (item["Name"] != null) s += item["Name"].ToString() + " "; 
                if (item["Year"] != null) s += "("+item["Year"].ToString() + ")" + " ";
                if (item["Origin"] != null) s += item["Origin"].ToString() + " ";
                if (item["Rating"] != null) s += item["Rating"].ToString() + " ";
                if (item["Votes"] != null) s += "("+item["Votes"].ToString() +")"+ " ";
                    s = s.Replace("  ", " ");

                if (s.IndexOf(key) != -1) cmb.Items.Add(s);
            }          
        }

        public static bool checkStrings(string val, string[] xmlVal)
        {
            string valsp = val.Replace(" ","");
            valsp = valsp.Replace(" ", "");

            foreach (string v in xmlVal)
            {
                string vsp = v.Replace(" ","");
                vsp = vsp.Replace(" ","");

                if ((vsp.IndexOf(valsp)!=-1)||(valsp.IndexOf(vsp)!=-1))
                { return true; }
            }
            return false;
        }
    }
}
