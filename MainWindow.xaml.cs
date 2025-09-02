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
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TextBox = System.Windows.Controls.TextBox;

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


        public void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
//open file
            var file_Content = string.Empty; 
            var file_Path = string.Empty; 
            using OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file_Path = openFileDialog.FileName; 
                var file_Stream = openFileDialog.OpenFile();


                try
                {

                    using (StreamReader reader = new StreamReader(file_Stream))
                    {
                        Texbox_Main.Text = reader.ReadToEnd(); //display file in text
                        Textblock_File_Path.Text = file_Path; //display file path

                    }
                   


                }
                catch (Exception exception)
                {
                    Console.WriteLine("Invalid File");
                    throw;
                }

            

       
            



                    //
                ; //display file in text



            }



           

        }



      


    }

}