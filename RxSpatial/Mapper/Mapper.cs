using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
namespace Mapper
{
    class Mapper
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.SetIn(new StreamReader(args[0]));
            }

            string line;
            string[] words;

            while ((line = Console.ReadLine()) != null)
            {
                words = line.Split(' ');

                foreach (string word in words)
                    Console.WriteLine(word.ToLower());
            }
        }
    }
}
