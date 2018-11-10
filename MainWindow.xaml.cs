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

    //Application Manifest File was editied to this <requestedExecutionLevel level="highestAvailable" uiAccess="false" />

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /*
            List<string> Dirs = DirectoryTree.FindActivePaths();
            foreach (string Directory in Dirs)
            {
                Drive.Items.Add(Directory);
            }
            */

        }

        public void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            Name.Text = string.Empty;
        }

        public void Helper_GotFocus(object sender, RoutedEventArgs e)
        {
            StartingPath.Text = string.Empty;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FormHandler Form = new FormHandler(Name, StartingPath, Type, ActualApprox, Occurrence);

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

            DirectoryTree FreshSearch = new DirectoryTree(Form.Name, Form.StartingPath, Form.Type, Form.Occurrence, Form.ActualApprox);


            string Result = FreshSearch.Search();
            Console.WriteLine(Result);

        }

    
    }
}
