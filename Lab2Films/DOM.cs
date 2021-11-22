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
    class DOM : IAnalizatorXMLStrategy
    {
        XmlDocument doc = new XmlDocument();

        public List<Films> AnalyzeFile(Films mySearch, string path)
        {
            doc.Load(path);
            List<List<Films>> info = new List<List<Films>>();

            if (mySearch.Name == null && mySearch.Genre == null && mySearch.Year == null && mySearch.Director == null && mySearch.Country == null && mySearch.Language == null)
            {
                return ErrorCatch(doc);
            }

            if (mySearch.Name != null) info.Add(SearchByAttribute("Film", "Name", mySearch.Name, doc));
            if (mySearch.Genre != null) info.Add(SearchByAttribute("Film", "Genre", mySearch.Genre, doc));
            if (mySearch.Year != null) info.Add(SearchByAttribute("Film", "Year", mySearch.Year, doc));
            if (mySearch.Director != null) info.Add(SearchByAttribute("Film", "Director", mySearch.Director, doc));
            if (mySearch.Country != null) info.Add(SearchByAttribute("Film", "Country", mySearch.Country, doc));
            if (mySearch.Language != null) info.Add(SearchByAttribute("Film", "Language", mySearch.Language, doc));

            return Cross(info, mySearch);
        }
            
        public List<Films> SearchByAttribute (string nodeName, string attribyte, string myTemplate, XmlDocument doc)
        {
            List<Films> find = new List<Films>();
            if(myTemplate != null)
            {
                XmlNodeList lst = doc.SelectNodes("//" + nodeName + "[@" + attribyte + "=\"" + myTemplate + "\"]");
                foreach (XmlNode e in lst)
                {
                        find.Add(Info(e));
                }
        }
        return find;
    }

        public List<Films> ErrorCatch (XmlDocument doc)
        {
            List<Films> result = new List<Films>();
            XmlNodeList lst = doc.SelectNodes("//" + "film");
            foreach(XmlNode elem in lst)
            {
                result.Add(Info(elem));
            }
            return result;
        }
               
        public Films Info(XmlNode node)
        {
            Films search = new Films();
            search.Name = node.Attributes.GetNamedItem("Name").Value;
            search.Genre = node.Attributes.GetNamedItem("Genre").Value;
            search.Year = node.Attributes.GetNamedItem("Year").Value;
            search.Director = node.Attributes.GetNamedItem("Director").Value;
            search.Country = node.Attributes.GetNamedItem("Country").Value;
            search.Language = node.Attributes.GetNamedItem("Language").Value;

            return search;
        }

        public List<Films> Cross(List<List<Films>> list, Films myTemplate)
        {
            List<Films> result = new List<Films>();
            List<Films> clear = CheckNodes(list, myTemplate);
            foreach(Films elem in clear)
            {
                bool isIn = false;
                foreach(Films s in result)
                {
                    if (s.Compare(elem))
                    {
                        isIn = true;
                    }
                }
                if (!isIn)
                {
                    result.Add(elem);
                }
            }
            return result;
        }

        public List<Films> CheckNodes (List<List<Films>> list, Films myTemplate)
        {
            List<Films> newResult = new List<Films>();
            foreach(List<Films> elem in list)
            {
                foreach (Films s in elem)
                {
                    if ((myTemplate.Name == s.Name || myTemplate.Name == null)&&
                        (myTemplate.Genre == s.Genre || myTemplate.Genre == null) &&
                        (myTemplate.Year == s.Year || myTemplate.Year == null) &&
                        (myTemplate.Director == s.Director || myTemplate.Director == null) &&
                        (myTemplate.Country == s.Country || myTemplate.Country == null) &&
                        (myTemplate.Language == s.Language || myTemplate.Language == null))
                    {
                        newResult.Add(s);
                    }
                }
            }
            return newResult;
        }
    }
}
