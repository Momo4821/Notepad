using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {



            //open file
            var file_Content = string.Empty; // will hold the content of the file
            var file_Path = string.Empty; // will hold the path of the file

            using OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file_Path = openFileDialog.FileName;
                var file_Stream = openFileDialog.OpenFile();


                using (StreamReader reader = new StreamReader(file_Stream))
                {
                    file_Content = reader.ReadToEnd();

                }

            }


           

        }
    }
}