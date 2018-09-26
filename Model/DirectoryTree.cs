using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OSSearcher.Model
{
    class DirectoryTree
    {
        public DirectoryTree() { }

        public static List<string> FindActivePaths()
        {
            string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<string> RealPaths = new List<string>();

            foreach (char letter in Alphabet)
            {
                String charToString = letter.ToString();
                string Path = charToString + ":\\";

                try
                {
                    DirectoryInfo d = new DirectoryInfo(Path);
                    d.GetDirectories();
                    RealPaths.Add(Path);
                    
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    Console.WriteLine("Skipping this non-existant directory");
                }
            }
            return RealPaths;
        }
    }


  
}
