using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoLibraryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files;

            Console.WriteLine("Hello!");

            files = Directory.GetFiles("X:\\");

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(files[i]);
            }

            Console.ReadKey();
        }
    }
}
