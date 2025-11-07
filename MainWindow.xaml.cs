using Microsoft.VisualBasic.CompilerServices;
using System.Drawing.Printing;
using System.IO;
using System.Media;
using System.Printing.IndexedProperties;
using System.Security.Cryptography.X509Certificates;
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
using static Notepad.MainWindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static Notepad.FilesModified;
using Keyboard = Microsoft.VisualBasic.Devices.Keyboard;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using Size = System.Windows.Size;
using TextBox = System.Windows.Controls.TextBox;

namespace Notepad

// private variables and methods --> modified variable only main window set it not somethign else
// textchanged event just set to true if file is modified then call it in the openclick_method before opening a new file
// research more about global variables in C# WPF applications
//research summary comments
//research event handlers not using onclick methods. 
//run tests before pushed to git hub read about unit testing in WPF applications

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            Textbox_Main.TextChanged += Textbox_Main_OnTextChanged;
            // event handler to check if the text has been modified

        }



        //orignial text
        private string Original_Text_Value;
        public bool ismodified { get; set; }


        //file stream variable
        public FileStream file_Stream { get; set; }

        //file dialog variables
        public OpenFileDialog openfile_dialog { get; set; }
        public SaveFileDialog savefile_dialog { get; set; }

        //file path variable
        public string file_Path { get; set; }

        //message box buttons
        public MessageBoxButtons Buttons { get; set; }

        //stream reader and writer
        public StreamWriter streamWriter;
        public StreamReader streamReader;



        //downloads folder path
        public static string Downloads =
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";

        // files modified class 
        // public static FilesModified files_Modified = new FilesModified();




        public void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            Buttons = MessageBoxButtons.YesNoCancel;
            String Caption = "Do you wish to save current file before opening another file";
            DialogResult Result;
            Result = System.Windows.Forms.MessageBox.Show(Caption, String.Empty, Buttons);
            Convert.ToBoolean(Result);


            

            switch (Result)
            {
                case System.Windows.Forms.DialogResult.Yes:
                    MessageBox.Show(Caption);
                    if (!File.Exists(file_Path))
                    {
                        var savefile_dialog_new_path = new SaveFileDialog();
                        string new_File_Path = savefile_dialog_new_path.FileName;
                        savefile_dialog_new_path.Title = "Save As";
                        savefile_dialog_new_path.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                        savefile_dialog_new_path.InitialDirectory = Downloads;
                        savefile_dialog_new_path.AddExtension = true;
                        if (savefile_dialog_new_path.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            using (streamWriter = new StreamWriter(savefile_dialog_new_path.FileName))
                            {
                                streamWriter.Write(Textbox_Main.Text);
                                MessageBox.Show("File Saved", new_File_Path);
                                //Textblock_File_Path.Text = Path.GetFullPath(file_Path);
                                Textblock_File_Type.Text = Path.GetExtension(file_Path);

                            }

                        }
                    }

                    if (File.Exists(file_Path))
                        {
                            using (streamWriter = new StreamWriter(file_Path))
                            {
                                streamWriter.Write(Textbox_Main.Text);
                                MessageBox.Show("File Saved", file_Path);

                            }
                    
                    
                    
                            }
                    
                    //open file a after saving
                    var Open_file_after_saving = new OpenFileDialog();
                    Open_file_after_saving.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    if (Open_file_after_saving.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        using (streamReader = new StreamReader(Open_file_after_saving.FileName))
                        {
                            Textbox_Main.Text = streamReader.ReadToEnd();
                            file_Path = Open_file_after_saving.FileName;
                            Textblock_File_Path.Text = file_Path;
                            Textblock_File_Type.Text = Path.GetExtension(file_Path);

                        }
                    }
                   
                    
                    break;
                case System.Windows.Forms.DialogResult.No:
                    //opem file anyway
                    //do nothing 
                    
                    break;
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
                                savefile_dialog_new_path.Title = "Save As";
                                savefile_dialog_new_path.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                                savefile_dialog_new_path.InitialDirectory = Downloads;
                                savefile_dialog_new_path.AddExtension = true;
                                if (savefile_dialog_new_path.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    using (streamWriter = new StreamWriter(savefile_dialog_new_path.FileName))
                                    {
                                        streamWriter.Write(Textbox_Main.Text);
                                        MessageBox.Show("File Saved", new_File_Path);
                                        //Textblock_File_Path.Text = Path.GetFullPath(file_Path);
                                        Textblock_File_Type.Text = Path.GetExtension(file_Path);

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


        private void Textbox_Main_OnTextChanged(object sender, TextChangedEventArgs e)
        {
                //event handler to check if the text has been modified
            if (ismodified)
            {

                ismodified = true;
            }
            else if (!ismodified)
            {
                ismodified = false;
            }
        }

       
    
    }
}