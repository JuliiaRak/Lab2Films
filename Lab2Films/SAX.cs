using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Lab2Films
{
    class SAX : IAnalizatorXMLStrategy
    {
        public List<Films> AnalyzeFile(Films film, string path)
        {
            List<Films> AllResults = new List<Films>();
            var xmlReader = new XmlTextReader(path);
            while (xmlReader.Read())
            {
                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        string Name = "";
                        string Genre = "";
                        string Year = "";
                        string Director = ""; 
                        string Country = "";
                        string Language = "";

                        if(xmlReader.Name.Equals("Name")&&(xmlReader.Value.Equals(film.Name) || film.Name == null))
                        {
                            Name = xmlReader.Value;
                            xmlReader.MoveToNextAttribute();

                            if (xmlReader.Name.Equals("Genre") && (xmlReader.Value.Equals(film.Genre) || film.Genre == null))
                            {
                                Genre = xmlReader.Value;
                                xmlReader.MoveToNextAttribute();

                                if (xmlReader.Name.Equals("Year") && (xmlReader.Value.Equals(film.Year) || film.Year == null))
                                {
                                    Year = xmlReader.Value;
                                    xmlReader.MoveToNextAttribute();

                                    if (xmlReader.Name.Equals("Director") && (xmlReader.Value.Equals(film.Director) || film.Director == null))
                                    {
                                        Director = xmlReader.Value;
                                        xmlReader.MoveToNextAttribute();

                                        if (xmlReader.Name.Equals("Country") && (xmlReader.Value.Equals(film.Country) || film.Country == null))
                                        {
                                            Country = xmlReader.Value;
                                            xmlReader.MoveToNextAttribute();

                                            if (xmlReader.Name.Equals("Language") && (xmlReader.Value.Equals(film.Language) || film.Language == null))
                                            {
                                                Language = xmlReader.Value;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        if (Name != "" && Genre != "" && Year != "" && Director != "" && Country != "" && Language != "")
                        {
                            Films myFilm = new Films();
                            myFilm.Name = Name;
                            myFilm.Genre = Genre;
                            myFilm.Year = Year;
                            myFilm.Director = Director;
                            myFilm.Country = Country;
                            myFilm.Language = Language;

                            AllResults.Add(myFilm);
                        }
                    }
                }
            }
            xmlReader.Close();
            return AllResults;
        }
    }
}
