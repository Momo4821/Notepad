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

        public void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
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
                    MessageBox.Show("Invalid File. Only txt");
                    throw;
                }





            }
        }


        public void Save_OnClick(object sender, RoutedEventArgs e)
        {
       

            //if textbox is modified give the option to save
            MessageBoxButtons Buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save your progress";
            DialogResult result;

          


            result = System.Windows.Forms.MessageBox.Show(caption,Textblock_File_Path.Text, Buttons);


            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //we need to append the file that is open in the text box
            /*    using (StreamWriter sw = File.AppendText(Textblock_File_Path.Text.ToString()))
                {
                    sw.WriteLine(Texbox_Main.Text); 
                    
                }*/
            File.AppendText(Textblock_File_Path.ToString());
            File.WriteAllText(Textblock_File_Path.Text.ToString(),Texbox_Main.Text);

            }

            //messagebox to show if the user should save or not
















        }
        
        private void Save_As_OnClick(object sender, RoutedEventArgs e)
        {


            //this will save the file as a new file
            SaveFileDialog save_as = new SaveFileDialog();

            save_as.FileName = "Document"; // Default file name





        }

        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            
            
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save your progress";
            DialogResult result;
            result = System.Windows.Forms.MessageBox.Show(caption, Texbox_Main.Text, buttons);
            Convert.ToBoolean(result);

            switch (result)
            {
                case System.Windows.Forms.DialogResult.Yes :
                    using StreamWriter sr = new StreamWriter;
                    break;

            }



            //close app
            this.Close();



        }

        private void Time_Date_Stamp_OnClick(object sender, RoutedEventArgs e)
        {
            DateTime date_stamp = DateTime.UtcNow;

            Texbox_Main.Text = date_stamp.ToString();


            if (Keyboard.IsKeyDown(Key.F5)) // need to work on 
            {
                
                Texbox_Main.Text = date_stamp.ToString();

            }

        }

        private void Change_Font_OnClick(object sender, RoutedEventArgs e)

        {


            throw new NotImplementedException();


        }

        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            





        }
    }

}