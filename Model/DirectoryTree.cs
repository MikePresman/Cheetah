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


        public string SearchForAFile(string CurrentRoot)
        {
            bool FileFound = false;

            DirectoryInfo Root = new DirectoryInfo(CurrentRoot);
            List<System.IO.DirectoryInfo> RootFoldersInDir = Root.EnumerateDirectories().ToList(); //Enumerates Folders in Dir

            string CurrentDirectory = CurrentRoot;
            List<int> FolderIndex = new List<int>();

            int i = 0;
            while (!FileFound)
            {
                List<System.IO.DirectoryInfo> foldersInDir = null;
                DirectoryInfo CurrentDir = new DirectoryInfo(CurrentDirectory);

                if (CurrentDir.GetDirectories().Length > 0)
                {
                    foldersInDir = CurrentDir.EnumerateDirectories().ToList(); //Enumerates Folders in Dir
                }

                List<System.IO.FileInfo> filesInDir = CurrentDir.EnumerateFiles().ToList(); //Enumerates Files in Dir

            
                foreach (System.IO.FileInfo file in filesInDir)
                {
                    if (Path.GetFileNameWithoutExtension(file.Name) == this._fileName)
                        return file.DirectoryName;
                    else
                        continue;
                }

                if (foldersInDir != null && foldersInDir.Count > 0 && CurrentDir != Root)
                {
                    Console.WriteLine(CurrentDirectory);
                    FolderIndex.Add(i);
                    i = 0;
                    CurrentDirectory = foldersInDir[i].FullName;
                    if (!(foldersInDir.Count - 1 == i))
                        i++;
                }
                else if (CurrentDir == Root) {
                    CurrentDirectory = foldersInDir[i].FullName;
                    Console.WriteLine(CurrentDirectory);
                }
                else
                {

                    
                    Console.WriteLine(CurrentDirectory);
                    DirectoryInfo parentDir = Directory.GetParent(CurrentDirectory);
                    CurrentDirectory = parentDir.FullName;
                    foldersInDir = parentDir.EnumerateDirectories().ToList();
                    
                    //TODO FIX EXCEPTION
                        CurrentDirectory = foldersInDir[FolderIndex[0] + 1].FullName;
                        int temp = FolderIndex[0] + 1;
                        FolderIndex.RemoveAt(0);
                        FolderIndex.Add(temp);

                    

                 

                    

                }
                
             
            }


            return "";
           }


          

            

            //if search complete and we used this._helper and no results, throw an exception
            //not found start again
            //this._rootPoint++;
            //GetRoot();

            /*
            if filenotfound and this._helper != null
                throw exception

            if filenotfound and this._rootPoint < _dirs.Count
                this._rootPoint++
                getroot
            */


        public string SearchForAFolder(string StartingRoot)
        {
            string hi = "hi";
            return hi;


            //if search complete and we used this._helper and no results, throw an exception
            //not found start again
            //this._rootPoint++;
            //GetRoot();

            /*
            if filenotfound and this._helper != null
                throw exception

            if filenotfound and this._rootPoint < _dirs.Count
                this._rootPoint++
                getroot
            */
        }


        public string DetermineAndHandleSearch()
        {
            string CurrentRoot = GetRoot();
            
            
            if (this._fileType.ToUpper().Equals("FOLDER"))
                return SearchForAFolder(CurrentRoot);

            else
                return SearchForAFile(CurrentRoot);
                
           


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
                    Console.WriteLine("Skipping this non-existent directory");
                }
            }
            return RealPaths;
        }   
        
        
    }
}
