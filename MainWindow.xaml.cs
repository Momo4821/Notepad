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




        public FileStream FileStream { get; set; }
        public OpenFileDialog openfile_dialog; 
        public MessageBoxButtons Buttons = MessageBoxButtons.YesNoCancel;
        public SaveFileDialog savefile_dialog;
        public string file_Path { get; set; }
        public StreamWriter streamWriter;

        public void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
           openfile_dialog = new OpenFileDialog();
            var file_type = string.Empty;
            openfile_dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openfile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                file_Path = openfile_dialog.FileName;
                file_type = Path.GetExtension(file_Path);
                var file_Stream = openfile_dialog.OpenFile();


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
            Buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save the file?";
            DialogResult Result;
            //Result = System.Windows.Forms.MessageBox.Show(caption,);






        }

        private void Save_As_OnClick(object sender, RoutedEventArgs e)
        {

            Buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save file?";
            DialogResult Result;
            Result = System.Windows.Forms.MessageBox.Show(caption, String.Empty, Buttons);
            Convert.ToBoolean(Result);

            
            //save dioluge option                                   
            savefile_dialog  = new SaveFileDialog();
            savefile_dialog.Title = "Do you wish to save the file?";
            
            
            //file path can't be null so we need to get directories for the file to be saved
            //DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(@"C:\Downloads"));

            savefile_dialog.Filter = "Text files (*.txt)|*.txt";
            savefile_dialog.AddExtension = true;
            
            switch (Result)
            {
                case System.Windows.Forms.DialogResult.Yes:
                    if (savefile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && File.Exists(openfile_dialog.FileName))
                    {
                        using StreamWriter sw = File.AppendText(openfile_dialog.FileName);
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
     

        

        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            //THIS WILL PRINT THE OPEN DOCUMENT TO  A CONNECTED PRINTER




        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save the file Before Exiting?";
            DialogResult Result;
            Result = System.Windows.Forms.MessageBox.Show(caption, String.Empty, Buttons);
            Convert.ToBoolean(Result);
         
            

            switch(Result)
             {
                 case System.Windows.Forms.DialogResult.Yes:
                     try
                     { //save exisiting file
                         if (File.Exists(file_Path))
                         {
                             File.WriteAllText(Path.GetFullPath(file_Path), Textbox_Main.Text);
                             MessageBox.Show("File Saved", file_Path);

                         }
                         //create new file
                         else
                         {
                             var savefile_dialog_new_path = new SaveFileDialog();
                             string new_File_Path = savefile_dialog_new_path.FileName;
                             savefile_dialog = new SaveFileDialog();
                             savefile_dialog.Title = "Save As";
                             savefile_dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                             savefile_dialog.AddExtension = true;
                             if (savefile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                             {
                                 using (streamWriter = new StreamWriter(savefile_dialog.FileName))
                                 {
                                     streamWriter.Write(Textbox_Main.Text);
                                     streamWriter.Close();
                                     MessageBox.Show("File Saved", new_File_Path);
                                     this.Close();
                                 }
                                 
                             }

                         }
                     }
                     catch (Exception exception)
                     {
                         Console.WriteLine(exception);
                         throw;
                     }
                     break;
                 
                 case System.Windows.Forms.DialogResult.No:
                     this.Close();
                     
                     break;
                     case System.Windows.Forms.DialogResult.Cancel:
                         break;
                 
             }

            
        }

        private void Time_Date_Stamp_OnClick(object sender, RoutedEventArgs e)
        {
            DateTime datetimestamp = new DateTime();
            Convert.ToString(datetimestamp);

            if (Keyboard.IsKeyDown(Key.F5))
            {
                
                //if file is open not new
                using (FileStream fs = new FileStream(openfile_dialog.FileName, FileMode.Open))
                {
    

                }
                
            }

                



        }

        private void Change_Font_OnClick(object sender, RoutedEventArgs e)

        {


            throw new NotImplementedException();


        }

        private void New_OnClick(object sender, RoutedEventArgs e)
        {
        MessageBoxButtons Buttons = MessageBoxButtons.YesNoCancel;
        string caption = "Do you wish to save file?";
        DialogResult Result;
        Result = System.Windows.Forms.MessageBox.Show(caption, String.Empty, Buttons);
        Convert.ToString(Result);
      

            //check if file is open and save before createing a new file

            switch (Result)
            {
                
                case System.Windows.Forms.DialogResult.Yes:
                    //checkf file
                    
                    break;
                case System.Windows.Forms.DialogResult.No:
                    break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        break;
            }
            
           

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
