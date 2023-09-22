using LAB01_EDII.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace LAB01_EDII.Modelo
//{
//    public class CodificacionAritmetica
//    {
//        dado las formulas de las presentaciones
//         Límites inferiores y superiores
//        Inferior = AI + (AS - AI) * NI
//        Superior = AI + (AS - AI) * NS
//        Donde:
//        AI = Antiguo Inferior
//        AS = Antiguo Superior
//        NI = Nuevo Inferior
//        NS = Nuevo Superior
//        public decimal Coding(string texto, Dictionary<char, Letra> dictionario)
//        {

//            decimal I = 0, S = 0, AI = 0, AS = 1;
//            for (int i = 0; i < texto.Length; i++)
//            {
//                decimal NI = dictionario[texto[i]].inferior;
//                decimal NS = dictionario[texto[i]].superior;
//                I = AI + (AS - AI) * NI;
//                S = AI + (AS - AI) * NS;
//                AI = I;
//                AS = S;
//            }
//            return I;
//        }


//        //Utilizar el intervalo y el número decimal hasta llegar 0.0
//        //(Inferior - RI)/(RS-RI)

//        public string Decode(decimal code, Dictionary<char, Letra> dictionario, int expectedlenght)
//        {
//            StringBuilder resul = new StringBuilder();  // Usar StringBuilder para mejorar la eficiencia
//            while (resul.Length < expectedlenght)
//            {
//                decimal inf, sup;
//                foreach (var item in dictionario)
//                {
//                    inf = item.Value.inferior;
//                    sup = item.Value.superior;
//                    if (code >= inf && code < sup)
//                    {
//                        resul.Append(item.Key);  // Usar Append en lugar de +=
//                        code = (code - inf) / (sup - inf);
//                        break;  // una vez encuentre el caracter se sale
//                    }
//                }
//            }
//            return resul.ToString();
//        }

//    }
//}
