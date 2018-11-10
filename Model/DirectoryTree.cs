using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OSSearcher.Model
{
    class EmptyDirectoryException : Exception
    {
        public EmptyDirectoryException(string message) : base(message) { }
    }

    //TODO Files with names Containing
    //TODO Folder Search

    class DirectoryTree
    {
        private string _fileName;
        private string _startingPath;
        private string _fileType;
        private string _occurrence;
        private string _approxActual;

        private string _root;
        List<string> FileFoundPaths = new List<string>();

        private List<string> _dirs;
        public List<string> UnauhorizedFolders = new List<string>();

        public DirectoryTree(string FileName, string StartingPath, string Type, string Occurrence, string ApproxActual)
        {
            this._fileName = FileName;
            this._startingPath = StartingPath;
            this._fileType = Type;
            this._occurrence = Occurrence;
            this._approxActual = ApproxActual;


            this._root = StartingPath;
            this._dirs = FindActivePaths();
        }

        public List<string> GetFoldersInDir(string directory)
        {
            List<string> AccessibleFoldersInDir = new List<string>();

            DirectoryInfo CurrentDirectory = new DirectoryInfo(directory);
            List<System.IO.DirectoryInfo> FoldersInDir = CurrentDirectory.EnumerateDirectories().ToList(); //Enumerates Folders in Dir

            foreach (System.IO.DirectoryInfo folder in FoldersInDir)
            {
                try
                {
                    DirectoryInfo TryDirectory = new DirectoryInfo(folder.FullName);//
                    
                    List<System.IO.FileInfo> filesInDir = TryDirectory.EnumerateFiles().ToList(); //Enumerates Files in Dir

                    AccessibleFoldersInDir.Add(folder.FullName);
                    
                }
                catch(UnauthorizedAccessException)
                {
                    UnauhorizedFolders.Add(folder.FullName);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    UnauhorizedFolders.Add(folder.FullName);
                    continue;
                }
            }
            return AccessibleFoldersInDir;
        }

        public List<string> GetFilesInDir(string directory)
        {
            DirectoryInfo CurrentDirectory = new DirectoryInfo(directory);
            List<System.IO.FileInfo> FilesInDir = CurrentDirectory.EnumerateFiles().ToList();

            List<string> FilesInDirString = new List<string>();

            foreach(System.IO.FileInfo file in FilesInDir)
            {
                FilesInDirString.Add(Path.GetFileNameWithoutExtension(file.ToString()));
            }
            return FilesInDirString;
        }

        public void CheckFolderForFile(List<string> files, string currentPath)
        {
            foreach(string file in files)
            {
                if (file.ToLower() == this._fileName.ToLower())
                {
                    this.FileFoundPaths.Add(currentPath);
                    break;
                }  
            }
        }

        public void CheckForFolder(List<string> folders, string currentPath)
        {
            foreach(string folder in folders)
            {
                //Get Folder to only return last part of the directory
                string result = Path.GetFileName(folder);

                if (result.ToLower() == this._fileName.ToLower())
                {
                    this.FileFoundPaths.Add(currentPath);
                    break;
                }
            }
        }

        public string DetermineWhichFolderToSearchNow(string CurrentDirectory, List<string> FoldersAccessed, List<String> FoldersInCurrentDir)
        {
            foreach (string folder in FoldersInCurrentDir)
            {
                if (!FoldersAccessed.Contains(folder))
                {
                    return folder.ToString();
                }
            }

            if (CurrentDirectory == this._startingPath)
            {
                if (this.FileFoundPaths.Count > 1)
                {
                    foreach (string filepath in this.FileFoundPaths)
                    {
                        Console.WriteLine(filepath);
                    }
                    return "Finished";
                }
            }

            while (CurrentDirectory != this._startingPath)
            {
                DirectoryInfo parentDir = Directory.GetParent(CurrentDirectory);
                CurrentDirectory = parentDir.FullName;

                foreach (System.IO.DirectoryInfo folder in parentDir.EnumerateDirectories().ToList())
                {
                    
                    if (!FoldersAccessed.Contains(folder.FullName) && !UnauhorizedFolders.Contains(folder.FullName))
                    {
                        return folder.FullName;
                    }
                }
            }
            return "Finished";
        }

        public string Search()
        {
            string Directory = this._startingPath;
            List<string> FoldersAccessed = new List<string>();

            while (true)
            {
                Console.WriteLine(Directory);
                List<string> FilesInCurrentDir = GetFilesInDir(Directory);

                FoldersAccessed.Add(Directory);
                List<string> FoldersInCurrentDir = GetFoldersInDir(Directory);

                if (this._fileType == "File")
                {
                    CheckFolderForFile(FilesInCurrentDir, Directory);
                }
                else
                {
                    CheckForFolder(FoldersInCurrentDir, Directory);
                }
                    

                if (this._occurrence == "First" && this.FileFoundPaths.Count > 0)
                {
                    Console.WriteLine("The file was found in " + this.FileFoundPaths[0]);
                    return "Finished";
                }




                Directory = DetermineWhichFolderToSearchNow(Directory, FoldersAccessed, FoldersInCurrentDir);
                if (Directory == "Finished")
                {
                    Console.WriteLine("");
                    foreach(string file in FileFoundPaths)
                    {
                        Console.WriteLine("Found at " + file);
                    }
                    return "Done";
                }
            }
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
                    continue;
                }
            }
            Console.WriteLine("Found Directories");
            return RealPaths;
        }


    }
}