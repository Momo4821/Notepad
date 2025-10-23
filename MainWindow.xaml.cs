using System.Drawing.Printing;
using System.IO;
using System.Media;
using System.Printing.IndexedProperties;
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
using Microsoft.VisualBasic.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static Notepad.MainWindow;
using Keyboard = Microsoft.VisualBasic.Devices.Keyboard;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using Size = System.Windows.Size;
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
            var file_modified = new FilesModified();
            InitializeComponent();
        }

        
    
        
        //file stream variable
        public FileStream FileStream { get; }

        //file dialog variables
        public OpenFileDialog openfile_dialog;
        public SaveFileDialog savefile_dialog;

        //file path variable
        public string file_Path { get; }

        //message box buttons
        public MessageBoxButtons Buttons { get; set; }

        //stream reader and writer
        public StreamWriter streamWriter;
        public StreamReader streamReader;



        //downloads folder path
        public string downloads = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";





        //create file in use method






        public void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {




            openfile_dialog = new OpenFileDialog();
            var file_type = string.Empty;
            openfile_dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openfile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                var file_Path = openfile_dialog.FileName;
                file_type = Path.GetExtension(file_Path);
                var file_Stream = openfile_dialog.OpenFile();




                using (StreamReader reader = new StreamReader(file_Stream))
                {
                    Textbox_Main.Text = reader.ReadToEnd(); //display file in text
                    Textblock_File_Path.Text = file_Path; //display file path
                    Textblock_File_Type.Text = file_type;



                }
                



            }

        }

        public void Save_OnClick(object sender, RoutedEventArgs e)
        {

            {
                Buttons = MessageBoxButtons.YesNoCancel;
                string caption = "Do you wish to save the file?";
                DialogResult Result;
                Result = System.Windows.Forms.MessageBox.Show(caption, String.Empty, Buttons);
                Convert.ToBoolean(Result);
                
                switch (Result)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        try
                        {
                            //save exisiting file
                            if (File.Exists(file_Path))
                            {
                              using (streamWriter = new StreamWriter(file_Path))
                                {
                                    streamWriter.Write(Textbox_Main.Text);
                                    MessageBox.Show("File Saved", file_Path);
                                    
                                }
                                
                                /*File.WriteAllText(Path.GetFullPath(file_Path), Textbox_Main.Text);
                                MessageBox.Show("File Saved", file_Path);*/

                            }
                            //create new file
                            else if (!File.Exists(file_Path))
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
                                        MessageBox.Show("File Saved", new_File_Path);
                                        
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
                        

                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        break;

                }

            }

        }

        private void Save_As_OnClick(object sender, RoutedEventArgs e)
        {
          
          //same function as save
        savefile_dialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
        savefile_dialog.CreatePrompt = true;

        if (savefile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            
            
            
        }

     



        }




        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            //THIS WILL PRINT THE OPEN DOCUMENT TO  A CONNECTED PRINTER

            openfile_dialog = new OpenFileDialog();
            var file_type = string.Empty;
            openfile_dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openfile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamReader rw = new StreamReader(openfile_dialog.FileName))
                {
                    PrintDocument p = new PrintDocument();
                    p.DefaultPageSettings.Landscape = true;
                    p.DefaultPageSettings.Color = false;
                    p.DocumentName = openfile_dialog.FileName;
                    Print.IsSubmenuOpen = true;

                }


            }


        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {

            {
                Buttons = MessageBoxButtons.YesNoCancel;
                string caption = "Do you wish to save the file Before Exiting?";
                DialogResult Result;
                Result = System.Windows.Forms.MessageBox.Show(caption, String.Empty, Buttons);
                Convert.ToBoolean(Result);



                switch (Result)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        try
                        {
                            //save exisiting file
                            if (File.Exists(file_Path))
                            {
                                using (streamWriter = new StreamWriter(file_Path))
                                {
                                    streamWriter.Write(Textbox_Main.Text);
                                    streamWriter.Close();
                                    MessageBox.Show("File Saved", file_Path);
                                }
                               

                            }
                            //create new file
                            else if (!File.Exists(file_Path))
                            {
                                var savefile_dialog_new_path = new SaveFileDialog();
                                string new_File_Path = savefile_dialog_new_path.FileName;
                                savefile_dialog = new SaveFileDialog();
                                savefile_dialog.Title = "Save As";
                                savefile_dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                                savefile_dialog.AddExtension = true;
                                savefile_dialog.FileName = "Untitled";
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
        }

        private void Time_Date_Stamp_OnClick(object sender, RoutedEventArgs e)
        {
       

            /*// if (Keyboard.IsKeyDown(Key.Return)
            {
                Textbox_Main.AppendText(DateTime.Now.ToString("hh:mm tt dd/MM/yyyy"));

            }*/

        }





        private void Change_Font_OnClick(object sender, RoutedEventArgs e)

            {


               


            }

            private void New_OnClick(object sender, RoutedEventArgs e)
            {


            }

        }
    }
    
