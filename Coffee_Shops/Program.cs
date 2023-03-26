using System;
using System.Collections.Generic;
using System.IO;

namespace Coffee_Shops
{
    internal class Program
    {

        public class Coffee_Shop
        {
            string name;
            double xCoord;
            double yCoord;
            public Coffee_Shop(string name, double xCoord, double yCoord)
            {
                this.name = name;
                this.xCoord = xCoord;
                this.yCoord = yCoord;

            }

            public string GetName() { return name; }
            public double GetXCoord() { return xCoord; }
            public double GetYCoord() { return yCoord; }

        }
        static void Main(string[] args)
        {

            if (args.Length == 0 || args.Length == 1)
            {
                Console.WriteLine("Bun venit! Te rog introdu coordonatele tale pentru a rula programul.");
                Console.WriteLine("Datele se vor introduce sub forma \"47.6\" \"-122.4\". ");
            }
            else
            {
                var path = @"C:\Users\Cosmin\source\repos\Coffee_Shops\Coffee_Shops\coffee_shops_Coding_C.csv";
                using (var reader = new StreamReader(path))
                {
                    Coffee_Shop[] coffee_shops = new Coffee_Shop[6];
                    int index = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        coffee_shops[index] = new Coffee_Shop(values[0], double.Parse(values[1]), double.Parse(values[2]));
                        index++;
                    }

                    List<double> distance = new List<double>();
                    foreach (var coord in coffee_shops)
                    {
                        var valx = Math.Pow(double.Parse(args[0]) - coord.GetXCoord(), 2);
                        var valy = Math.Pow(double.Parse(args[1]) - coord.GetYCoord(), 2);
                        double val = valx + valy;
                        distance.Add(Math.Round(Math.Sqrt(val), 4));
                    }
                    for (int i = 0; i < distance.Count - 1; i++)
                        for (int j = i + 1; j < distance.Count; j++)
                        {
                            if (distance[i] > distance[j])
                            {
                                double aux = distance[i];
                                distance[i] = distance[j];
                                distance[j] = aux;
                                Coffee_Shop caf = coffee_shops[i];
                                coffee_shops[i] = coffee_shops[j];
                                coffee_shops[j] = caf;
                            }
                        }
                    int firstThreeShops = 0;
                    foreach (var caf in coffee_shops)
                    {
                        Console.WriteLine(caf.GetName() + "   " + distance[firstThreeShops]);
                        firstThreeShops++;
                        if (firstThreeShops == 3) break;
                    }
                }


            }

        }
    }
}

