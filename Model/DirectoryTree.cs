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
        private string _occurrence;
        private string _approxActual;

        private int _rootPoint;
        private List<string> _dirs;

        public DirectoryTree(string FileName, string Helper, string Occurrence, string FileDrive, string ApproxActual)
        {
            this._fileName = FileName;
            this._helper = Helper;
            this._fileType = Occurrence;
            this._occurrence = FileDrive;
            this._approxActual = ApproxActual;

            this._rootPoint = 0;
            this._dirs = FindActivePaths();
        }

        public string SearchForAFile(string CurrentRoot)
        {
            bool FileFound = false;
            int i = 0;
            int numberOfTimesRootAccessed = 1;
            string CurrentDirectory = CurrentRoot;

            DirectoryInfo Root = new DirectoryInfo(CurrentRoot);

            List<System.IO.DirectoryInfo> RootFoldersInDir = Root.EnumerateDirectories().ToList(); //Enumerates Folders in Dir
            List<int> FolderIndex = new List<int>();
            List<string> FoldersAccessed = new List<string>();
            List<string> FileFoundPath = new List<string>();
            List<string> UnauthorizedFilePaths = new List<string>();


            while (!FileFound)
            {
                List<System.IO.DirectoryInfo> foldersInDir = null;
                DirectoryInfo CurrentDir = new DirectoryInfo(CurrentDirectory);
                Console.WriteLine(CurrentDirectory);

               
                    if (CurrentDir.GetDirectories().Length > 0)
                    {
                        foldersInDir = CurrentDir.EnumerateDirectories().ToList(); //Enumerates Folders in Dir
                    }
          
              


                //Check the files in the directory to see if they match what we're looking for
                List<System.IO.FileInfo> filesInDir = CurrentDir.EnumerateFiles().ToList(); //Enumerates Files in Dir
                foreach (System.IO.FileInfo file in filesInDir)
                {
                    if (Path.GetFileNameWithoutExtension(file.Name.ToLower()) == this._fileName.ToLower() && this._occurrence.Equals("First"))
                    {
                        return("File Found at... " + CurrentDirectory + "\n");
                        
                    }
                    else if (Path.GetFileNameWithoutExtension(file.Name.ToLower()) == this._fileName.ToLower() && this._occurrence.Equals("Find All"))
                    {
                        FileFoundPath.Add(CurrentDirectory);
                    }//else if prompt
                    else
                        continue;
                }


                //Checking if Folders Exist in the current directory
                if (foldersInDir != null && CurrentDir != Root)
                {

                    FoldersAccessed.Add(CurrentDirectory);
                    CurrentDirectory = foldersInDir[i].FullName;
                    i = 0;

                    if (foldersInDir.Count() - 1 < i)
                    {
                        i++;
                    }
                }
                //Checking if we are in the Root dir
                else if (CurrentDir == Root && foldersInDir.Count > 0)
                {
                    numberOfTimesRootAccessed++;
                    FoldersAccessed.Add(CurrentDirectory);
                    CurrentDirectory = foldersInDir[i].FullName;
                }
                //We've checked the root, havent found the file, and no folders exist to continue the search, so the search has found no results
                else if (foldersInDir == null && FoldersAccessed == null)
                {
                    return "Nothing Here";
                }
                else
                {
                    //We're in a folder that contains no more folders within it, but it is has parent directories where we can go back and check the other folders within them, only if they havent been checked before
                    string notAccessed = null;
                    while (notAccessed == null)
                    {

                        DirectoryInfo parentDir = Directory.GetParent(CurrentDirectory);
                        CurrentDirectory = parentDir.FullName;

                        foldersInDir = parentDir.EnumerateDirectories().ToList();
                        //we're in the base root now, we're checking if any more folders exist to search
                        if (CurrentDirectory == Root.FullName)
                        {
                            foreach (DirectoryInfo folder in foldersInDir)
                            {
                                if (!FoldersAccessed.Contains(folder.FullName))
                                {
                                    numberOfTimesRootAccessed++;
                                    CurrentDirectory = folder.FullName;
                                    break;
                                    
                                }
                            }
                        }
                        //if we were at the base root just up above, and got no other folders to search then we've completed the search with no results
                        if (CurrentDirectory == Root.FullName)
                        {
                            if (numberOfTimesRootAccessed == foldersInDir.Count())
                            {
                                List<string> distinct = FileFoundPath.Distinct().ToList();
                                Console.Write("\n");
                                foreach (string path in distinct)
                                {
                                    Console.WriteLine("File Found at... " + path);
                                }
                                return "End of Search";
                               
                            }
                            return "Nothing Found";
                        }

                        //if we are in any other folder that isnt the base root, but is a parent root to the directory we were just in, find another folder to go into and search. one that hasnt been accessed yet
                        foreach (DirectoryInfo folder in foldersInDir)
                        {
                            if (FoldersAccessed.Contains(folder.FullName))
                            {
                                continue;
                            }
                            else
                            {
                                DirectoryInfo CheckDirectory = new DirectoryInfo(folder.FullName);
                                try
                                {
                                    CheckDirectory.GetDirectories();
                                   
                                }catch (System.UnauthorizedAccessException)
                                {
                                    if (!RootFoldersInDir.Contains(folder) && FileFoundPath == null || this._occurrence == "Single")
                                    {
                                        return "nothing found";
                                    }else if (!RootFoldersInDir.Contains(folder) && FileFoundPath != null)
                                    {
                                        List<string> distinct = FileFoundPath.Distinct().ToList();
                                        Console.Write("\n");
                                        foreach (string path in distinct)
                                        {
                                            Console.WriteLine("File Found at... " + path);
                                        }
                                        return "Search Complete";
                                    }
                                    
                                    continue;
                                }
                                
                                notAccessed = folder.FullName;
                                CurrentDirectory = notAccessed;
                                FoldersAccessed.Add(CurrentDirectory);
                                break;
                            }
                        }

                    }

                }
            }
            return "Nothing";
        }

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
            string CurrentRoot = this._helper;

            if (this._fileType.ToUpper().Equals("FOLDER"))
                return SearchForAFolder(CurrentRoot);

            else
                return SearchForAFile(CurrentRoot);
        }

        /*
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
            else if (this._helper == null && this._occurrence == "Unknown")
            {
                startingPoint = this._dirs[this._rootPoint];
            }
            else
            {
                startingPoint = this._occurrence;
            }
            return startingPoint;
        }
        */

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