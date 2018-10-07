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
        private string _fileDrive;
        private string _approxActual;

        private int _rootPoint;
        private List<string> _dirs;

        public DirectoryTree(string FileName, string Helper, string FileType, string FileDrive, string ApproxActual)
        {
            this._fileName = FileName;
            this._helper = Helper;
            this._fileType = FileType;
            this._fileDrive = FileDrive;
            this._approxActual = ApproxActual;

            this._rootPoint = 0;
            this._dirs = FindActivePaths();
        }

        public string DetermineAndHandleSearch()
        {
           
            string CurrentRoot = GetRoot();

            //Begin Search
            //at each step check its name
            //if name is actual or containing
            //once match check if its a file or folder or unknown
            //if found check



            //if search complete and we used this._helper and no results, throw an exception
            //not found start again
            //this._rootPoint++;
            //GetRoot();

            return CurrentRoot;
        }

        public string GetRoot()
        {
            string startingPoint = null;

            if (this._rootPoint > 0)
            {
                startingPoint = this._dirs[this._rootPoint];
                return startingPoint;
            }

            if (this._helper != null)
            {
                startingPoint = this._helper;
            }
            else if (this._helper == null && this._fileDrive == "Unknown")
            {
                startingPoint = this._dirs[this._rootPoint];
            }
            else
            {
                startingPoint = this._fileDrive;
            }
            return startingPoint;
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

 
    }
}
