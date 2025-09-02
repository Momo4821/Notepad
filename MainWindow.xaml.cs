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
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
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
            var file_type = string.Empty;
            

            


            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
        
                file_Path = openFileDialog.FileName;
                file_type = Path.GetExtension(file_Path);
                var file_Stream = openFileDialog.OpenFile();
               

                try
                {
                   
                    using (StreamReader reader = new StreamReader(file_Stream))
                    {
                        Texbox_Main.Text = reader.ReadToEnd(); //display file in text
                        Textblock_File_Path.Text = file_Path; //display file path
                        Textblock_File_Type.Text = file_type;


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


        public void Save_OnClick(object sender, RoutedEventArgs e)
        {
            

            //if textbox is modified give the option to save
            MessageBoxButtons Buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save your progress";
            DialogResult result;

            //messagebox to show if the user should save or not

           


        }
           


        private void Save_As_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Time_Date_Stamp_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Change_Font_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}