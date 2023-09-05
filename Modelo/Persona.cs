using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB01_EDII.Modelo
{
    public class Persona
    {
        public string name { get; set; }
        public string dpi { get; set; }
        public DateTime datebirth { get; set; } //"1970-01-11T08:48:32.962Z": Esta es una fecha y hora en formato UTC (lo indica la Z al final).
        public string address { get; set; }
    }
}
