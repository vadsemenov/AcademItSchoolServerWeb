using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConfig
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var url = ConfigurationManager.AppSettings["SiteUrl"];

            Console.WriteLine(url);

            Console.Read();
        }
    }
}
