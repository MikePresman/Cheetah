using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OSSearcher.Controller;
using OSSearcher.Model;

namespace OSSearcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<string> Dirs = DirectoryTree.FindActivePaths();
            foreach (string Directory in Dirs)
            {
                Drive.Items.Add(Directory);
            }

            
        
            if (Name.IsFocused){
                Name.Text = string.Empty;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FormHandler Form = new FormHandler(Name, Helper, Type, ActualApprox, Drive);

            try
            {
                Form.Validate();
            }
            catch (System.FormatException)
            {
                MessageBox.Show("There is a problem with your submission please check it again", "Error Message");
            }
            catch (System.MissingFieldException)
            {
                MessageBox.Show("One or more fields haven't been filled out", "Error Message");
            }

            DirectoryTree FreshSearch = new DirectoryTree(Form.Name, Form.Helper, Form.Type, Form.Drive, Form.ActualApprox);


     

            
            string Result = FreshSearch.DetermineAndHandleSearch();
            MessageBox.Show(Result);

        }

      


    }
}
