using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Drawing.Color;
using FontFamily = System.Windows.Media.FontFamily;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;

namespace Notepad;

// private variables and methods --> modified variable only main window set it not somethign else
// textchanged event just set to true if file is modified then call it in the openclick_method before opening a new file
// research more about global variables in C# WPF applications
//research summary comments
//research event handlers not using onclick methods. 
//run tests before pushed to git hub read about unit testing in WPF applications

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();


        // Textbox_Main.TextChanged += Textbox_Main_OnTextChanged;
        // event handler to check if the text has been modified
    }




    public bool ismodified { get; set; }


    //file stream variable
    public FileStream file_Stream { get; set; }

    //file dialog variables
    public OpenFileDialog OpenfileDialog { get; set; }
    public SaveFileDialog SavefileDialog { get; set; }

    //file path variable
    public string file_Path { get; set; }

    //message box buttons
    public MessageBoxButtons Buttons { get; set; }

    //stream reader and writer
    public StreamWriter streamWriter { get; set; }
    public StreamReader streamReader { get; set; }


    //downloads folder path
   public string Downloads =Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";





    public void OpenFile_OnClick(object sender, RoutedEventArgs e)
    {
        Textbox_Main.TextChanged += Textbox_Main_OnTextChanged;
        Buttons = MessageBoxButtons.YesNoCancel;
        var Caption = "Do you wish to save current file before opening another file";
        DialogResult Result;
        Result = System.Windows.Forms.MessageBox.Show(Caption, string.Empty, Buttons);
        Convert.ToBoolean(Result);


        switch (Result)
        {
            case System.Windows.Forms.DialogResult.Yes:
                MessageBox.Show(Caption);
                if (!File.Exists(file_Path))
                {
                    var savefile_dialog_new_path = new SaveFileDialog();
                    var new_File_Path = savefile_dialog_new_path.FileName;
                    savefile_dialog_new_path.Title = "Save As";
                    savefile_dialog_new_path.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    savefile_dialog_new_path.InitialDirectory = Downloads;
                    savefile_dialog_new_path.AddExtension = true;
                    if (savefile_dialog_new_path.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        using (streamWriter = new StreamWriter(savefile_dialog_new_path.FileName))
                        {
                            streamWriter.Write(Textbox_Main.Text);
                            MessageBox.Show("File Saved", new_File_Path);
                            //Textblock_File_Path.Text = Path.GetFullPath(file_Path);
                            Textblock_File_Type.Text = Path.GetExtension(file_Path);
                        }
                }

                if (File.Exists(file_Path))
                    using (streamWriter = new StreamWriter(file_Path))
                    {
                        streamWriter.Write(Textbox_Main.Text);
                        MessageBox.Show("File Saved", file_Path);
                    }

                //open file a after saving
                var Open_file_after_saving = new OpenFileDialog();
                Open_file_after_saving.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (Open_file_after_saving.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    using (streamReader = new StreamReader(Open_file_after_saving.FileName))
                    {
                        Textbox_Main.Text = streamReader.ReadToEnd();
                        file_Path = Open_file_after_saving.FileName;
                        Textblock_File_Path.Text = file_Path;
                        Textblock_File_Type.Text = Path.GetExtension(file_Path);
                    }


                break;
            case System.Windows.Forms.DialogResult.No:
                var openfileDialog = new OpenFileDialog();
                if (openfileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (streamReader = new StreamReader(openfileDialog.FileName))
                    {

                        Textbox_Main.Text = streamReader.ReadToEnd();
                        file_Path = openfileDialog.FileName;
                        Textblock_File_Path.Text = file_Path;
                        Textblock_File_Type.Text = Path.GetExtension(file_Path);

                    }




                }

                break;

            case System.Windows.Forms.DialogResult.Cancel:
                break;
        }
    }


    public void Save_OnClick(object sender, RoutedEventArgs e)
    {
        {
            Buttons = MessageBoxButtons.YesNoCancel;
            var caption = "Do you wish to save the file?";
            DialogResult Result;
            Result = System.Windows.Forms.MessageBox.Show(caption, string.Empty, Buttons);
            Convert.ToBoolean(Result);

            switch (Result)
            {
                case System.Windows.Forms.DialogResult.Yes:
                    try
                    {
                        //save exisiting file
                        if (File.Exists(file_Path))
                        {

                            Textblock_File_Path.Text = file_Path.ToString();
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
                            var new_File_Path = savefile_dialog_new_path.FileName;
                            savefile_dialog_new_path.Title = "Save As";
                            savefile_dialog_new_path.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                            savefile_dialog_new_path.InitialDirectory = Downloads;
                            savefile_dialog_new_path.AddExtension = true;
                            if (savefile_dialog_new_path.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                using (streamWriter = new StreamWriter(savefile_dialog_new_path.FileName))
                                {
                                    streamWriter.Write(Textbox_Main.Text);
                                    MessageBox.Show("File Saved", new_File_Path);
                                    //Textblock_File_Path.Text = Path.GetFullPath(file_Path);
                                    Textblock_File_Type.Text = Path.GetExtension(file_Path);
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
        /*   Buttons = MessageBoxButtons.YesNoCancel;
           var caption_save_as = "Do you wish to save the file As?";
           DialogResult Result;
           Result = System.Windows.Forms.MessageBox.Show(caption_save_as, string.Empty, Buttons);
           Convert.ToBoolean(Result);


           switch (Result)
           {
               case System.Windows.Forms.DialogResult.Yes:

                   break;
           }


           //same function save as method but always creates a new file*/


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
                file_Path = SavefileDialog.FileName;
                SavefileDialog = new SaveFileDialog();
                SavefileDialog.Title = "Save As";
                SavefileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                SavefileDialog.AddExtension = true;
                SavefileDialog.FileName = "Untitled";
                if (SavefileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    using (streamWriter = new StreamWriter(SavefileDialog.FileName))
                    {
                        streamWriter.Write(Textbox_Main.Text);
                        streamWriter.Close();
                        MessageBox.Show("File Saved", file_Path);
                        Close();
                    }
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }

    }


    private void Print_OnClick(object sender, RoutedEventArgs e)
    {
        //THIS WILL PRINT THE OPEN DOCUMENT TO  A CONNECTED PRINTER

        OpenfileDialog = new OpenFileDialog();
        var file_type = string.Empty;
        OpenfileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        if (OpenfileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            using (var rw = new StreamReader(OpenfileDialog.FileName))
            {
                var p = new PrintDocument();
                p.DefaultPageSettings.Landscape = true;
                p.DefaultPageSettings.Color = false;
                p.DocumentName = OpenfileDialog.FileName;
                Print.IsSubmenuOpen = true;
            }
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
        {
            Buttons = MessageBoxButtons.YesNoCancel;
            var caption = "Do you wish to save the file Before Exiting?";
            DialogResult Result;
            Result = System.Windows.Forms.MessageBox.Show(caption, string.Empty, Buttons);
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
                            file_Path = SavefileDialog.FileName;
                            SavefileDialog = new SaveFileDialog();
                            SavefileDialog.Title = "Save As";
                            SavefileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                            SavefileDialog.AddExtension = true;
                            SavefileDialog.FileName = "Untitled";
                            if (SavefileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                using (streamWriter = new StreamWriter(SavefileDialog.FileName))
                                {
                                    streamWriter.Write(Textbox_Main.Text);
                                    streamWriter.Close();
                                    MessageBox.Show("File Saved", file_Path);
                                    Close();
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
                    using (streamReader = new StreamReader(Textbox_Main.Text))
                    {

                        streamReader.Dispose();

                    }
                    

                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    break;
            }
        }
    }


    private void Time_Date_Stamp_OnClick(object sender, RoutedEventArgs e)
    {


        var create_date_Time
            = DateTime.Now.ToLongDateString() + Environment.NewLine;
        Textbox_Main.AppendText(create_date_Time);



    }


      
    private void Change_Font_OnClick(object sender, RoutedEventArgs e)
    {
        var _chose_font = new FontDialog();
        _chose_font.ShowColor = true;
        _chose_font.ShowApply = true;
        _chose_font.ShowHelp = true;
        _chose_font.MinSize = 20;



        if (_chose_font.ShowDialog() == System.Windows.Forms.DialogResult.OK)

        {


            // var color_convert = new SolidBrush(Color.FromArgb(_chose_font.Color.A, _chose_font.Color.R, _chose_font.Color.G, _chose_font.Color.B));
            Textbox_Main.Foreground = new SolidColorBrush(){Color = System.Windows.Media.Color.FromArgb(_chose_font.Color.A, _chose_font.Color.R, _chose_font.Color.G, _chose_font.Color.B)};

            Textbox_Main.FontFamily = new FontFamily(_chose_font.Font.Name); // font family of text font
            Textbox_Main.FontSize = _chose_font.Font.Size; // size of font
            Textbox_Main.FontWeight = (_chose_font.Font.Bold ? FontWeights.Bold : FontWeights.Regular);
            Textbox_Main.Effect =
            // bold or regular font weight
            /*//different way to change text color from github copilot
            var dsrawing = _chose_font.Color;
            var mediaColor = System.Windows.Media.Color.FromArgb(dsrawing.A, dsrawing.R, dsrawing.G, dsrawing.B);
            Textbox_Main.Foreground = new System.Windows.Media.SolidColorBrush(mediaColor);*/



            // 

        }




    }

    

    private void New_OnClick(object sender, RoutedEventArgs e)
    {
        Textbox_Main.TextChanged += Textbox_Main_OnTextChanged;
        Buttons = MessageBoxButtons.YesNoCancel;
        var Caption = "Do you wish to save current file before creating a new file";
        DialogResult Result;
        Result = System.Windows.Forms.MessageBox.Show(Caption, string.Empty, Buttons);
        Convert.ToBoolean(Result);

        switch (Result)
        {

            case System.Windows.Forms.DialogResult.Yes:

                if (File.Exists(file_Path))
                {
                    if (SavefileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        using (streamWriter = new StreamWriter(file_Path))
                        {
                            streamWriter.Write(Textbox_Main.Text);
                            MessageBox.Show("File Saved");

                        }



                    }

                }
                else if (!File.Exists(file_Path))
                {
                    //var savefile_dialog_new_path = new SaveFileDialog();
                    // var new_File_Path = savefile_dialog_new_path.FileName;
                    file_Path = SavefileDialog.FileName;
                    SavefileDialog = new SaveFileDialog();
                    SavefileDialog.Title = "Save As";
                    SavefileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    SavefileDialog.AddExtension = true;
                    SavefileDialog.FileName = "Untitled";
                    if (SavefileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        using (streamWriter = new StreamWriter(SavefileDialog.FileName))
                        {
                            streamWriter.Write(Textbox_Main.Text);
                            streamWriter.Close();
                            MessageBox.Show("File Saved", file_Path);

                        }





                }



                break;
            case System.Windows.Forms.DialogResult.No:
                var search_folder = new FolderBrowserDialog();
                Textbox_Main.Clear();
                Textblock_File_Path.Text = string.Empty;
                Textblock_File_Type.Text = string.Empty;

                if (search_folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var savefile = new SaveFileDialog();
                    search_folder.SelectedPath = search_folder.SelectedPath;
                    
                    
                    savefile.FileName = "Untitled";
                    savefile.AddExtension = true;
                    savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                     if (savefile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                         
                                    {

                                        using (streamWriter = new StreamWriter(savefile.FileName))
                                        {
                                            
                                            streamWriter.Write(Textbox_Main.Text);
                                            Textblock_File_Path.Text = search_folder.SelectedPath;
                                            Textblock_File_Type.Text = Path.GetExtension(savefile.FileName);
                                            MessageBox.Show("File Created",search_folder.SelectedPath );
                                            
                                        }
                                        /*using (FileStream fs = new FileStream(search_folder.SelectedPath, FileMode.CreateNew))
                                        {
                                          
                                            byte[] info = new UTF8Encoding(true).GetBytes(Textbox_Main.Text);
                                            // Add some information to the file.
                                            fs.Write(info, 0, info.Length);
                                            Textblock_File_Path.Text = search_folder.SelectedPath;
                                            Textblock_File_Type.Text = Path.GetExtension(search_folder.SelectedPath);
                                            MessageBox.Show("File Created",search_folder.SelectedPath );
                                        }*/
                                             
                                             
                                    }
                }
                
               
                
               
                
                
                    
                break;
            case System.Windows.Forms.DialogResult.Cancel:

                //do nothing
                break;





        }


    }

    private void Textbox_Main_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        //event handler to check if the text has been modified
        if


            (ismodified)
            ismodified = true;


        else if

            (!ismodified) ismodified = false;
    }


    private void Textbox_Main_OnKeyDown(object sender, KeyEventArgs e)
    {
        var create_date_Time
            = DateTime.Now.ToLongDateString() + "\n";
        bool Key_f5_Pressed = Keyboard.IsKeyDown(Key.F5);
        // F5 Key Pressed - Insert Date and Time
        if (Key_f5_Pressed)
        {

            Textbox_Main.AppendText(create_date_Time);
            
        }


        bool Key_f6_Pressed = Keyboard.IsKeyDown(Key.F6);

        if (Key_f6_Pressed)
        {
            char format = '•';
            Textbox_Main.TextWrapping = TextWrapping.Wrap;
            


        }




    }

    private void Format_OnClick(object sender, RoutedEventArgs e)
    {
        char format = '•';
        // Convert.ToString(format + "\n" );



        Textbox_Main.TextWrapping = TextWrapping.Wrap;
        Textbox_Main.AppendText(format + "\n");


    }


    

    
}




    



