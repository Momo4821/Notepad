using System.IO;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
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





        public OpenFileDialog openfile;
        public string file_Path { get; set; }

        public void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
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
                        Textbox_Main.Text = reader.ReadToEnd(); //display file in text
                        Textblock_File_Path.Text = file_Path; //display file path
                        Textblock_File_Type.Text = file_type;


                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Invalid File. Only txt");
                    throw;
                }

            }
        }



        public void Save_OnClick(object sender, RoutedEventArgs e)

        {
            //Messagebox for save file
            MessageBoxButtons Buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save file?";
            DialogResult Result;
            Result = System.Windows.Forms.MessageBox.Show(caption, String.Empty, Buttons);
            Convert.ToBoolean(Result);

            
            //save dioluge option                                   
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            
            
            //file path can't be null so we need to get directories for the file to be saved
            //DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(@"C:\Downloads"));

            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.AddExtension = true;
            
            switch (Result)
            {
                case System.Windows.Forms.DialogResult.Yes:
                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        
                        using StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
                        {
                            sw.Write(Textbox_Main.Text.ToString());
                            
                        }
                        
                        
                    }

                    break;    
                
                case System.Windows.Forms.DialogResult.No:
                    //

                    break;
                
                case System.Windows.Forms.DialogResult.Cancel:
                break;
            }
            
            




        }
        
        

        private void Save_As_OnClick(object sender, RoutedEventArgs e)
        {


            //this will save the file as a new file
            if (file_Path != string.Empty)
            {
                
                
                
            }


        }

        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            //THIS WILL PRINT THE OPEN DOCUMENT TO  A CONNECTED PRINTER




        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Time_Date_Stamp_OnClick(object sender, RoutedEventArgs e)
        {






        }

        private void Change_Font_OnClick(object sender, RoutedEventArgs e)

        {


            throw new NotImplementedException();


        }

        private void New_OnClick(object sender, RoutedEventArgs e)
        {

            MessageBoxButtons Buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save your file before creating a new one";
            var new_file = new OpenFileDialog();
            DialogResult result_new;



            /*//create a new document
            if (Textbox_Main.Text != string.Empty)
            {
                //result = System.Windows.Forms.MessageBox.Show(caption,Textblock_File_Path.Text, Buttons);
                result_new = System.Windows.Forms.MessageBox.Show(caption, Textblock_File_Path.Text, Buttons);

                if (result_new == System.Windows.Forms.DialogResult.Yes)
                {
                    File.AppendText(Textblock_File_Path.ToString());
                }

                Textbox_Main.Clear();
                Textblock_File_Type.Text = string.Empty;
                Textblock_File_Path.Text = string.Empty;



            }*/










            //when pressed new check if textbox has been modified give user option to save




            //after save clear textbox



        }
    }
}
