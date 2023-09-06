using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using LAB01_EDII.Modelo;
using LAB01_EDII.Arbol;

namespace LAB01_EDII
{
    public class Program
    {
        private static int countIns = 0;
        private static int countDel = 0;
        private static int countPat = 0;

        private static AVL<Persona> arbolPersonas = new AVL<Persona>();
        private static List<Persona> personas = new List<Persona>();
        public static void Main(string[] args)
        {
            try
            {
                
                //Mostrardisplay();
                string route = @"C:\\Users\\julio\\Downloads\\inputsprueba.csv";
                //@"C:\\Users\\julio\\Downloads\\LAB01-EDII\\inputs.csv";
                if (File.Exists(route))
                {
                    string[] FileData = File.ReadAllLines(route);
                    foreach (var item in FileData)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            string[] fila = item.Split(';');
                            //util el ?  en esta situacion para la deserialización de datos, donde la operación podría no producir un resultado válido y, en lugar de lanzar una excepción, podria que el resultado sea null, deserializa el objeto JSON en la posición fila[1] a una instancia de la clase Persona.
                            Persona? persona = JsonSerializer.Deserialize<Persona>(fila[1]);
                            // //fila[0] tiene la acción (INSERT, PATCH, DELETE).
                            //fila[1] contiene la serialización json.
                            if (fila[0] == "INSERT")
                            {
                                countIns++;
                                // persona! le esta diciendo al compilador: "Confia en mi, en este punto, persona no es null, asi que no me adviertas sobre la posibilidad de que pueda ser null".
                                arbolPersonas.Add(persona!, Delegates.NameComparison, Delegates.DpiComparison);
                            }
                            else if (fila[0] == "DELETE")
                            {
                                countDel++;
                                arbolPersonas.Delete(persona!, Delegates.NameComparison, Delegates.DpiComparison);
                            }
                            else if (fila[0] == "PATCH")
                            {
                                countPat++;
                                arbolPersonas.Patch(persona!, Delegates.NameComparison, Delegates.DpiComparison);
                            }
                        }
                    }

                    string flag;
                    do
                    {
                        Console.Clear();
                        Mostrardisplay();
                        Console.WriteLine("*********** Menú: *************");
                        Console.WriteLine("1- Mostrar Registros completos");
                        Console.WriteLine("2- Buscar Registros");
                        Console.WriteLine("3- Salir");
                        Console.WriteLine("Por favor elija una opción del sistema:");
                        flag = Console.ReadLine();

                        switch (flag)
                        {
                            case "1":
                                todosregistros(FileData);
                                break;
                            case "2":
                                buscarregistros(arbolPersonas);
                                break;
                            case "3":
                                Console.WriteLine("Saliendo...");
                                break;
                            default:
                                Console.WriteLine("Opción no válida.");
                                break;
                        }

                        //Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();

                    } while (flag != "3");

                }
            }
            catch (Exception)
            {
                Console.WriteLine("El sistema ha presentado un error inesperado");
            }


        }

        private static void Mostrardisplay()
        {
            Console.WriteLine("████████╗ █████╗ ██╗     ███████╗███╗   ██╗████████╗    ██╗  ██╗██╗   ██╗██████╗ ");
            Console.WriteLine("╚══██╔══╝██╔══██╗██║     ██╔════╝████╗  ██║╚══██╔══╝    ██║  ██║██║   ██║██╔══██╗");
            Console.WriteLine("   ██║   ███████║██║     █████╗  ██╔██╗ ██║   ██║       ███████║██║   ██║██████╔╝");
            Console.WriteLine("   ██║   ██╔══██║██║     ██╔══╝  ██║╚██╗██║   ██║       ██╔══██║██║   ██║██╔══██╗");
            Console.WriteLine("   ██║   ██║  ██║███████╗███████╗██║ ╚████║   ██║       ██║  ██║╚██████╔╝██████╔╝");
            Console.WriteLine("   ╚═╝   ╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═══╝   ╚═╝       ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ");
            Console.WriteLine(new string('*', 80));//llamo al constructor de la clase string y tomo un caracter y un numero entero como argumentos, repitiendo el caracter 80 veces.
            Console.WriteLine();
        }

        private static void resumenOperaciones()
        {
            //Console.Clear();
            //Mostrardisplay();
            Console.WriteLine($"Su resumen de las operaciones ingresadas es:");
            Console.WriteLine($"INSERT: {countIns}");
            Console.WriteLine($"DELETE: {countDel}");
            Console.WriteLine($"PATCH: {countPat}");
            Console.WriteLine();
        }

        private static void todosregistros(string[] FileData)
        {
            Console.Clear();
            Mostrardisplay();
            int rowNumber = 1;
            foreach (var line in FileData)
            {
                string[] fila = line.Split(';');
                // Mostrando el tipo de accion y el contenido JSON.
                Console.WriteLine($"{rowNumber} - {fila[0]} - {fila[1]}");
                rowNumber++;
            }
            Console.WriteLine("Presione cualquier tecla para regresar a menu...");
        }

        private static void buscarregistros(AVL<Persona> arbolPersonas)
        {
            char flag2;
            do
            {
                Console.Clear(); 
                Mostrardisplay();
                resumenOperaciones();

                personas.Clear();
                //resumenOperaciones();
                Console.WriteLine("Ingrese el nombre de la persona que quiere buscar en minusculas: ");
                string? name = Console.ReadLine().ToLower(); //leo la entrada del usuario en minusculas y la dicha variable name de tipo nullable ya que podria ser null

                Persona temporal = new Persona(); //creo un nuevo objeto de Persona y lo asigno a temporal 

                temporal.name = name!; //asigno el valor leido en la consola al atributo name del objeto temporal, al valor leido en consola con valor ! de afirmacion de no nulidad.

                arbolPersonas.QueryResults(arbolPersonas.Root!, temporal, Delegates.NameComparison, personas);
                // llamo a mi metodo queryresults para llenar la lista, tambien verifico que la raiz del arbol no este nulla , el objeto persona que tiene el nombre que se quiere buscar, y el respectivo delegado para comparar por el atributo de nombre, y su respectiva lista para almacenar los resultados de la busqueda

                string line = "";
                if (personas.Count() == 0)
                {
                    line = "Sin coincidencias para el nombre:  " + name;
                    Console.WriteLine(line);
                }
                else
                {
                    Console.WriteLine($"Se encontraron en el sistema: {personas.Count} coincidencias para el nombre: {name} ");
                    int counter = 0;
                    foreach (var item in personas)
                    {
                        Console.WriteLine($"name: {item.name} \t dpi: {item.dpi} \t dateBirth: {item.datebirth} \t address: {item.address}");
                        counter++;
                        line += JsonSerializer.Serialize<Persona>(item) + "\n";
                    }
                    string output = @"C:\\Users\\julio\\Downloads\\LAB01-EDII\\packages" + name + ".txt";
                    File.WriteAllText(output, line);
                }

                Console.WriteLine("¿Desea realizar otra búsqueda? Ingrese \'1\' o \'2\'");
                Console.WriteLine("1. Si");
                Console.WriteLine("2. No");
                //Console.WriteLine("3. Menu");
                flag2 = Console.ReadKey().KeyChar;
            } while (flag2 == '1');
        }
    }
}
