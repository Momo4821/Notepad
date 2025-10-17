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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
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
            /*List<string> Keybaord_keys = new List<string>();
            Keybaord_keys.Add("F1");
            Keybaord_keys.Add("F2");
            Keybaord_keys.Add("F3");
            Keybaord_keys.Add("F4");
            Keybaord_keys.Add("F5");*/

            /*var keyboard = new Keyboard();
            keyboard = Keybaord_keys.Contains("F5") ? keyboard : null;*/


            InitializeComponent();
        }
        public FileStream FileStream { get;}
        public OpenFileDialog openfile_dialog;
        public MessageBoxButtons Buttons { get; set; }
        public SaveFileDialog savefile_dialog;
        public string file_Path { get; set; }
        public StreamWriter streamWriter;



        //create intial directory to downlaods folder
//Downlaods Folder can't be renamed or moved as it is a system folder
        public string downloads = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
        
        //keybdings
        public Keyboard keyboard = new Keyboard();



        public void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            openfile_dialog = new OpenFileDialog();
            var file_type = string.Empty;
            openfile_dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            
            
            
            Buttons = MessageBoxButtons.YesNo;
            DialogResult Result_Save_Current_File;
            Result_Save_Current_File = System.Windows.Forms.MessageBox.Show("Do you wish to save current file before opening another file", String.Empty, Buttons);
            Convert.ToBoolean(Result_Save_Current_File);
            
            
            file_Path = openfile_dialog.FileName;
            switch (Result_Save_Current_File)
            {
                case  System.Windows.Forms.DialogResult.Yes:
                    if (FileMode.Open !=null) // filemode is not null means there is a open file
                    {
                        savefile_dialog = new SaveFileDialog();
                        savefile_dialog.Filter = "Text files (*.txt)|*.txt";
                        savefile_dialog.Title = "Save file";
                        savefile_dialog.FileName = FileMode.Open.ToString();
                        savefile_dialog.DefaultExt = ".txt";
                        savefile_dialog.FileName = openfile_dialog.FileName;
                        if (savefile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            using (streamWriter = new StreamWriter(savefile_dialog.FileName))
                            {
                                streamWriter.Write(Textbox_Main.Text);
                                MessageBox.Show("File Saved", file_Path);
                                streamWriter.Close();
                            }
                            
                        }
                        
                        
                    }
                    else // filemode is bull meaning no file
                    {
                        if (openfile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            
                            Textblock_File_Path.Text = openfile_dialog.FileName;
                            Textblock_File_Type.Text = Path.GetFileNameWithoutExtension(file_Path);
                            using (StreamReader reader = new StreamReader(openfile_dialog.FileName))
                            {
                              
                            }
                            
                        }
                        {
                            using (StreamReader reader = new StreamReader(openfile_dialog.FileName))
                            {
                                Textbox_Main.Text = reader.ReadToEnd();
                                reader.Close();
                            }
                            
                        }
                        {
                            
                            
                        }
                        
                        
                        
                    }
                    
                    break;
                
                
                
                        
                        
            }
            
            
            

            //open file
            //need to open a file if a file is already opened save file then open new file selected












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
                                    streamWriter.Close();
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

        private void Save_As_OnClick(object sender, RoutedEventArgs e)
        {
          
          //same function as save
          
          openfile_dialog = new OpenFileDialog();
          openfile_dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
          openfile_dialog.CheckFileExists = true;
          openfile_dialog.FileName = "Untitled";

     



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
            DateTime datetimestamp = new DateTime();
            Convert.ToString(datetimestamp);
            keyboard = new Keyboard();
            SendKeys.Send("F5");

            /*// if (Keyboard.IsKeyDown(Key.Return)
            {
                Textbox_Main.AppendText(DateTime.Now.ToString("hh:mm tt dd/MM/yyyy"));

            }*/

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
    
