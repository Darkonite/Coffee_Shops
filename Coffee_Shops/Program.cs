using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

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
        private static void testInputs(string[] args)
        {
            try
            {
                if (args.Length == 2)
                { var y = "ok"; }

            }
            catch (Exception exc)
            {
                Console.WriteLine("Bun venit! Te rog introdu date pentru a rula programul.");
                Console.WriteLine("Datele se vor introduce sub forma x: \"47.6\" y: \"-122.4\" url: \"link\". ");
            }
            try
            {

                if ((double.Parse(args[0]) - 1).GetType() == typeof(double) || (double.Parse(args[1]) - 1).GetType() != typeof(double))
                {
                    var x = "ok";
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Coordonatele introduse nu sunt conforme!");
            }

            try
            {
                WebRequest req = WebRequest.Create(args[2]);
                WebResponse res = req.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Message.Contains("remote name could not be resolved"))
                {
                    Console.WriteLine("Linkul nu poate fi accesat.");
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                testInputs(args);
            }
            catch (Exception exp)
            { Console.WriteLine(""); }
            var response = new WebClient().DownloadString(args[2]);
            var numberOfEnters = Regex.Matches(response, @"\n").Count;
            string[] datas = response.Split(new char[] { ',', '/', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int z = 0, index = 0;
            Coffee_Shop[] coffee_shops = new Coffee_Shop[numberOfEnters + 1];
            List<string> shop_data = new List<string>();
            foreach (string data in datas)
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    shop_data.Add(data);
                    z++;
                    if (z == 3)
                    {
                        coffee_shops[index] = new Coffee_Shop(shop_data[0], double.Parse(shop_data[1]), double.Parse(shop_data[2]));
                        z = 0;
                        shop_data.RemoveRange(0, 3);
                        index++;

                    }
                }
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
