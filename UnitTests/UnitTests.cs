namespace LibXml.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class XMLTests
    {
        [TestMethod]
        public void EqualStringsTest()
        {
            string checker = "qwerty";
            string[] data = new string[3];
            data[0] = "querty";
            data[1] = "qwerty";
            data[2] = "werty";
            Assert.IsTrue(ProcessXML.CheckStrings(checker, data));
        }
    }
}

namespace LibProcess.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class InfoTests
    {
        [TestMethod]
        public void TransferStringsTest()
        {
            Movie.MovieData[] m = new Movie.MovieData[1];
            LibProcess.MovieInfo x = new LibProcess.MovieInfo(string.Empty);
            string[] origin = new string[1];
            origin[0] = "Name (Year) Origin Rating (Votes)";
            m = x.StringProcessData(origin, -1);

            Movie.MovieData res = new Movie.MovieData();
            res.Name = "Name ";
            res.Origin = "Origin ";
            res.Year = "Year";
            res.Rating = "Rating";
            res.Votes = "Votes";

            Assert.AreEqual(m[0].Name, res.Name);
            Assert.AreEqual(m[0].Origin, res.Origin);
            Assert.AreEqual(m[0].Year, res.Year);
            Assert.AreEqual(m[0].Rating, res.Rating);
            Assert.AreEqual(m[0].Votes, res.Votes);
        }
    }
}

namespace LibParse.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class ProcesParseTests
    {
        [TestMethod]
        public void LoadLocalHtmlTest()
        {
            string data = ProcesParse.LoadLocalHtml();
            string existing = "Рейтинг составлен по результатам голосования посетителей сайта. Любой желающий может принять в нем участие, проголосовав за свой любимый фильм.";
            Assert.IsTrue(data.IndexOf(existing) != -1);
        }

        [TestMethod]
        public void LoadPageTest()
        {
            // Must be started individually or twice for unknown reasons.
            string data = ProcesParse.LoadPage(@"https://www.kinopoisk.ru/top/");
            string existing = "Рейтинг составлен по результатам голосования посетителей сайта. Любой желающий может принять в нем участие, проголосовав за свой любимый фильм.";
            Assert.IsTrue(data.IndexOf(existing) != -1);
        }
    }
}