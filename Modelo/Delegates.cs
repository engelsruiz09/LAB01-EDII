using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB01_EDII.Modelo
{
    public class Delegates
    {
        public static System.Comparison<Persona> NameComparison = delegate (Persona p1, Persona p2)
        {
            return p1.name.CompareTo(p2.name);
        };

        public static System.Comparison<Persona> DpiComparison = delegate (Persona p1, Persona p2)
        {
            return p1.dpi.CompareTo(p2.dpi);
        };
    }
}
