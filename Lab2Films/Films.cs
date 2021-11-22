using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Films
{
    public class Films
    {
        public string Name = null;
        public string Genre = null;
        public string Year = null;
        public string Director = null;
        public string Country = null;
        public string Language = null;

        public Films() { }

        public Films (string[] data)
        {
            Name = data[0];
            Genre = data[1];
            Year = data[2];
            Director = data[3];
            Country = data[4];
            Language = data[5];
        }
        public bool Compare(Films obj)
        {
            if ((this.Name == obj.Name) &&
                (this.Genre == obj.Genre) &&
                (this.Year == obj.Year) &&
                (this.Director == obj.Director) &&
                (this.Country == obj.Country) &&
                (this.Language == obj.Language))
            {
                return true;
            }
            else return false;
        }
    }
}
