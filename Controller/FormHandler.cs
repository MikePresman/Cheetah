﻿using System;
using System.Collections.Generic;
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
        private string _helper;
        private string _type;
        private string _actualApprox;
        private string _drive;

        public FormHandler(TextBox name, TextBox helper, ComboBox type, ComboBox actualApprox, ComboBox drive)
        {
            this._name = name.Text;
            this._helper = helper.Text;
            this._type = type.Text;
            this._actualApprox = actualApprox.Text;
            this._drive = drive.Text;
        }

        public string Helper
        {
            get { return this._helper; }
        }

        public void Validate()
        {
            if (this._helper == "Leading Directory, Leave Empty if Unknown" || this._helper == "") { this._helper = null; }
            if (this._name == "File or Folder Name" || this._name == "") { this._name = null; }

            if (string.IsNullOrEmpty(this._name) || string.IsNullOrEmpty(this._type) || string.IsNullOrEmpty(this._actualApprox) || string.IsNullOrEmpty(this._drive))
            {
                throw new System.MissingFieldException("Woops you are missing information");
            }

            Regex FileNameValid = new Regex(@"^[a-zA-Z0-9\s]*$");
            if (!FileNameValid.IsMatch(this._name))
            {
                throw new System.FormatException("Invalid Naming");
            }

            Regex HelperNameValid  = new Regex(@"^(?:[\w]\:|\\)");
            if (this._helper != null)
            {
                if (!HelperNameValid.IsMatch(this._helper))
                {
                    throw new System.FormatException("Invalid Naming");
                }
            }
        }
    }
}
