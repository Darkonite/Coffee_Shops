
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


//public struct Coffee_Shop {
//   public string[] name;
//    float[] x;
//    float[] y;
//}

namespace Coffee_Shops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // de schimbat in dictionary dupa implementare
            Dictionary<string, List<double>> dictionary = new Dictionary<string, List<double>>();
            var path = @"C:\Users\Cosmin\source\repos\Coffee_Shops\Coffee_Shops\coffee_shops_Coding_C.csv";
            using (var reader = new StreamReader(path))
            {
                List<string> CoffeeShopName = new List<string>();//// se va sterge la final de implementare dictionary
                List<double> xCoordinates = new List<double>(); // se va sterge la final de implementare dictionary
                List<double> yCoordinates = new List<double>();// se va sterge la final de implementare dictionary
                List<double> coordinates = new List<double>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    CoffeeShopName.Add(values[0]);//se va sterge
                    xCoordinates.Add(double.Parse(values[1])); // se va sterge
                    yCoordinates.Add(double.Parse(values[2])); // se va sterge
                    coordinates.Add(double.Parse(values[1]));
                    coordinates.Add(double.Parse(values[2]));

                    //coordinates[0] = (double.Parse(values[1]));
                   // coordinates[1]=(double.Parse(values[2]));

                    dictionary.Add(values[0],coordinates.GetRange(coordinates.Count-2,2));
                   // coordinates.RemoveRange(0, 2);
                       
                }

                foreach (string key in dictionary.Keys)
                {
                    Console.Write(key+"  ");
                    foreach (var val in dictionary[key])
                    {
                        Console.Write(val+" ");
                    }
                    Console.WriteLine();
                }

                //introducerea coordonatelor
                Console.WriteLine("Bun venit! Te rog introdu coordonatele tale:");
                Console.Write("x:");
                var x=Console.ReadLine();
                Console.Write("y:");
                var y=Console.ReadLine();

                //initializarea unui vector pentru distante cu numarul de cafenele stocate
                double[] distances=new double[CoffeeShopName.Count];
                for (int i = 0; i < xCoordinates.Count; i++)//dictionary.count merge aici
                {
                    double valx = Math.Pow(double.Parse(x) - xCoordinates[i],2);
                    double valy = Math.Pow(double.Parse(y) - yCoordinates[i], 2);
                    double val = valx + valy;
                    distances[i] = Math.Round(Math.Sqrt(val),4);
                }
                
                //sortarea vectorului crescator si sortarea si a numelor ~ greseala ca nu se sorteaza toate liste
                //schimbare in dictionary dupa implementare
                for (int i = 0; i < distances.Length - 1; i++)
                    for (int j = i + 1; j < distances.Length; j++)
                    {
                        if (distances[i] > distances[j])
                        {
                            double aux = distances[i];
                            distances[i] = distances[j];    
                            distances[j] = aux;
                            string aux2 = CoffeeShopName[i];
                            CoffeeShopName[i] = CoffeeShopName[j];
                            CoffeeShopName[j] = aux2;
                        }
                    }
                for (int i = 0; i < 3; i++)
                { Console.WriteLine(CoffeeShopName[i]+ "   "+ distances[i] + "  "); }
            }

        }
    }
    }

