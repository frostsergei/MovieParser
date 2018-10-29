using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibParse
{
    public class ProcesParse
    {
        private static string local = @"Top250.html";
        public static string LoadPage(string url)
        {
            try
            {
                var result = "";
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    if (receiveStream != null)
                    {
                        StreamReader readStream;
                        if (response.CharacterSet == null)
                            readStream = new StreamReader(receiveStream);
                        else
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        result = readStream.ReadToEnd();
                        readStream.Close();
                    }
                    response.Close();
                }
                StreamWriter streamwriter = new StreamWriter(local, false, Encoding.UTF8);
                streamwriter.Write(result);
                streamwriter.Close();

                return result;
            }
            catch { return null; }
        }

        public static string LoadLocalHtml()
        {
            if (!File.Exists(local)) return null;
            StreamReader reader = new StreamReader(local);
            String line = "";
            string res = "";
            while ((line = reader.ReadLine()) != null)
            {
                res += line;
            }
            return res;
        }
    }
}

