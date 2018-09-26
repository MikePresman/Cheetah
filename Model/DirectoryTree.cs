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
        private string _fileName;
        private string _helper;
        private string _fileType;
        private string _filePath;
        private string _approxActual;

        public DirectoryTree(string FileName, string Helper, string FileType, string FilePath, string ApproxActual)
        {
            this._fileName = FileName;
            this._helper = Helper;
            this._fileType = FileType;
            this._filePath = FilePath;
            this._approxActual = ApproxActual;
        }


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

        public void FileSearchWithHelper()
        {

        }

        public void FileSearchWithoutHelperButDrive()
        {
            
        }

        public void FileSearchWithoutAny()
        {

        }

    }
}
