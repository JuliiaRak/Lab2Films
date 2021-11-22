using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Films
{
    interface IAnalizatorXMLStrategy
    {
        List<Films> AnalyzeFile (Films mySearch, string path);
    }
}
