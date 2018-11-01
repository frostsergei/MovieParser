// <copyright file="ProcesParse.cs" company="LETI">
//     All rights reserved.
// </copyright>
// <author>Sergei Morozov</author>
// <summary>This is Visual Studio 2017 Connuity Edition generated file.</summary>
namespace LibParse
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// This class gets HTML webpage code.
    /// </summary>
    public class ProcesParse
    {
        /// <summary>
        /// Local path to local version of webpage.
        /// </summary>
        private static string local = @"Top250.html";

        /// <summary>
        /// Downloads fresh verdion of webpage and returns in's HTML code.
        /// </summary>
        /// <param name="url">Webpage address.</param>
        /// <returns>The HTML page code.</returns>
        public static string LoadPage(string url)
        {
            try
            {
                var result = string.Empty;
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    if (receiveStream != null)
                    {
                        StreamReader readStream;
                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        }

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
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets local HTML webpage code.
        /// </summary>
        /// <returns>The HTML page code.</returns>
        public static string LoadLocalHtml()
        {
            if (!File.Exists(local))
            {
                return null;
            }

            StreamReader reader = new StreamReader(local);
            string line = string.Empty;
            string res = string.Empty;
            while ((line = reader.ReadLine()) != null)
            {
                res += line;
            }

            return res;
        }
    }
}