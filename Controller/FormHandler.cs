using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OSSearcher.Controller
{

    class FormHandler
    {
        private string _name;
        private string _startingpath;
        private string _type;
        private string _actualApprox;
        private string _occurrence;

        public FormHandler(TextBox name, TextBox StartingPath, ComboBox type, ComboBox actualApprox, ComboBox occurrence)
        {
            this._name = name.Text;
            this._startingpath = StartingPath.Text;
            this._type = type.Text;
            this._actualApprox = actualApprox.Text;
            this._occurrence = occurrence.Text;
        }

        public void Validate()
        {
            if (this._name == "File or Folder Name" || this._name == "") { this._name = null; }
            if (this._startingpath == "Leading Directory, Leave Empty if Unknown" || this._startingpath == "") { this._startingpath = null; }

            Regex FileNameValid = new Regex(@"^[a-zA-Z0-9\s_.-]*$");

            if (this._name == null || !FileNameValid.IsMatch(this._name))
            {
                throw new System.FormatException("Invalid Naming");
            }

            Regex HelperNameValid  = new Regex(@"^(?:[\w]\:|\\)");
            if (this._startingpath != null)
            {
                if (!HelperNameValid.IsMatch(this._startingpath))
                {
                    throw new System.FormatException("Invalid Naming");
                }
            }

            if (string.IsNullOrEmpty(this._name) || string.IsNullOrEmpty(this._type) || string.IsNullOrEmpty(this._actualApprox) || string.IsNullOrEmpty(this._occurrence) && this.StartingPath == null)
            {
                throw new System.MissingFieldException("Woops you are missing information");
            }

            //Check to make sure that the root folder isnt empty
            DirectoryInfo RootDir = new DirectoryInfo(this._startingpath);
            List<System.IO.DirectoryInfo> RootFolders = RootDir.EnumerateDirectories().ToList();
            List<System.IO.FileInfo> RootFiles = RootDir.EnumerateFiles().ToList();

            if (RootFolders.Count == 0 && RootFiles.Count == 0) { Console.WriteLine("Nothing in the Root Directory"); }
        }



    public string Name
        {
            get { return this._name; }
        }

        public string StartingPath
        {
            get { return this._startingpath; }
        }

        public string Type
        {
            get { return this._type; }
        }

        public string ActualApprox
        {
            get { return this._actualApprox; }
        }

        public string Occurrence
        {
            get { return this._occurrence; }
        }
    }
}
