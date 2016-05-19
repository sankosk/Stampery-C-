using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client("830fa1bf-bee7-4412-c1d3-31dddba2213d");

            Console.WriteLine("##################### GET STAMP ####################");
            Console.WriteLine(c.GetStamp("8d35d6a20140ac7dac9fdb9f51627899b20749ea87609f3a5d337dab5dff7c70"));
            Console.WriteLine("####################################################");
            Console.WriteLine();

            Console.WriteLine("##################### STAMP DATA ####################");
            string json = "{\"x\":\"true\"}";
            Console.WriteLine(c.StampData(json));
            Console.WriteLine("####################################################");
            Console.WriteLine();

            Console.WriteLine("##################### STAMP A FILE ####################");
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("x", "true");
            Console.WriteLine(c.StampFile(dict, @"C:\Users\Esteban\Documents\V0.zip"));
            Console.WriteLine("####################################################");

        }



    }
}
