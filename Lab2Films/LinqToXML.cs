using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Lab2Films
{
    class LinqToXML : IAnalizatorXMLStrategy
    {
        private List<Films> find = null;
        XDocument doc = new XDocument();

        public List<Films> AnalyzeFile(Films mySearch, string path)
        {
            doc = XDocument.Load(@path);
            find = new List<Films>();
            List<XElement> matches = (from val in doc.Descendants("Film")
                                      where ((mySearch.Name == null || mySearch.Name == val.Attribute("Name").Value) &&
                                      (mySearch.Genre == null || mySearch.Genre == val.Attribute("Genre").Value) &&
                                      (mySearch.Year == null || mySearch.Year == val.Attribute("Year").Value) &&
                                      (mySearch.Director == null || mySearch.Director == val.Attribute("Director").Value) &&
                                      (mySearch.Country == null || mySearch.Country == val.Attribute("Country").Value) &&
                                      (mySearch.Language == null || mySearch.Language == val.Attribute("Language").Value))
                                      select val).ToList();
            foreach(XElement match in matches)
            {
                Films res = new Films();
                res.Name = match.Attribute("Name").Value;
                res.Genre = match.Attribute("Genre").Value;
                res.Year = match.Attribute("Year").Value;
                res.Director = match.Attribute("Director").Value;
                res.Country = match.Attribute("Country").Value;
                res.Language = match.Attribute("Language").Value;
                find.Add(res);
            }
            return find;
        }
    }
}
