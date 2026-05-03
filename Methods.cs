using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Notepad.Functions;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using PrintDialog = System.Windows.Forms.PrintDialog;


namespace Notepad;

public  class Methods 
{ 
        
        private readonly MainWindow _mainWindow;
        private readonly FileService _fileService;
    
    public Methods(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _fileService = new FileService();
    }
   
   /// <summary>
   /// Dialog configuration for SaveFileDialog and OpenFileDialog are seperated into two methods to avoid
   /// redudent code and to make the code more readable and maintainable.
   /// </summary>
    private void Configuraitonsavefiledialog(Microsoft.Win32.SaveFileDialog savefiledialog)
    {
            savefiledialog.Title = $"$Save File {_fileService.FileName}";
            savefiledialog.RestoreDirectory = true;
            savefiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            savefiledialog.InitialDirectory = _fileService.Downlaods;
            savefiledialog.CheckFileExists = true;
            savefiledialog.OverwritePrompt = true;
    }
    
    
    private void Configurationopenfiledialog(Microsoft.Win32.OpenFileDialog opendialog)
    {
        opendialog.Title = "Open File";
        opendialog.InitialDirectory = _fileService.Downlaods;
        opendialog.RestoreDirectory = true;
        opendialog.DefaultExt = ".txt";
        opendialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        
    }
    
    
    /// <summary>
    /// ClearUI method is used to clear the UI when creating a new file or when the user chooses not to save the current file before opening a new one.
    /// It clears the text box and resets the file path and file type labels.
    /// </summary>
    private void UpdateFilelables(FileService fileService)
    {
        _mainWindow.LabelFilePath.Content = _fileService.Filepath;
        _mainWindow.LabelFileType.Content = _fileService.FileType;
    }
    
    private void ClearUI()
    {
        _mainWindow.Textbox_Main.Clear();
        _mainWindow.LabelFilePath.Content = "";
        _mainWindow.LabelFileType.Content = "";
        _fileService.Filepath = null!;
        
    }
    
    
    /// <summary>
    ///File Operations for the application
    /// </summary>
    public void Openfile ()
   {
       if (_fileService.HasFile)
       {
           var openfileMessagebox = MessageBoxButtons.YesNoCancel;
           DialogResult Result;
           string caption = "Do you wish to save the current file before opening a new one?";
           Result = MessageBox.Show(caption, _fileService.Filepath ?? "Untitled", openfileMessagebox);

           switch (Result)
           {
            case DialogResult.Yes:
                Savefile();
                break;
            case DialogResult.No:
                break;
            case DialogResult.Cancel:
                return;
               
               
           }
       
       
   }
       var openfileDialog = new Microsoft.Win32.OpenFileDialog();
       Configurationopenfiledialog(openfileDialog);
       if (openfileDialog.ShowDialog() == true)
       {
           ClearUI();
           using(StreamReader sr = new StreamReader(openfileDialog.OpenFile()))
           {
               _mainWindow.Textbox_Main.Text = sr.ReadToEnd();
                   
           }
           _fileService.Filepath = openfileDialog.FileName;
           UpdateFilelables(_fileService);
               
       }
           
   }
    
    
    public void Savefile()
    {

        if (_fileService.HasFile)
        {
            using (StreamWriter sw = new StreamWriter(_fileService.Filepath))
            {
                sw.Write(_mainWindow.Textbox_Main.Text);
                
            }
            MessageBox.Show("File Saved");
            UpdateFilelables(_fileService);
            
            
        }
        else
        {
            SaveFileas();
        }
        
        
    }
    
    public void SaveFileas ()
    {
    var savefileas = new Microsoft.Win32.SaveFileDialog();
    Configuraitonsavefiledialog(savefileas);
    if(savefileas.ShowDialog() == true)
    {
        using (StreamWriter sw = new StreamWriter(savefileas.OpenFile()))
        {
            sw.Write(_mainWindow.Textbox_Main.Text);
        }
        _fileService.Filepath = savefileas.FileName;
        UpdateFilelables(_fileService);
        MessageBox.Show("File Saved");        
        
    }
    }
    public void Print()
    {
        var printDialog = new PrintDialog();
        printDialog.Document.DocumentName = _fileService.FileName ?? "Untitled";
        printDialog.ShowHelp = true;
        printDialog.AllowSomePages = true;
        
        if (printDialog.ShowDialog() == DialogResult.OK)
        {

            _mainWindow.Textbox_Main.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            _mainWindow.Textbox_Main.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            printDialog.Document.Print();
            
            
            
        }
        
        _mainWindow.Textbox_Main.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        _mainWindow.Textbox_Main.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        
        
        
    }
    
    public void Exit()
    {
        MessageBoxResult Result;
        string caption = "Do you wish to exit the application without saving the current file?";
        Result = System.Windows.MessageBox.Show(caption, _fileService.Filepath ?? "Untitled", MessageBoxButton.YesNoCancel);
        switch (Result)
        {
            case MessageBoxResult.Yes:
                Application.Exit();
                break;
            case MessageBoxResult.No:
                Savefile();
                Application.Exit();
                break;
            case MessageBoxResult.Cancel:
                break;
                //do nothing
        }
    }
    
    public void Timestamp ()
    {
        var create_date_Time
            = DateTime.Now.ToLongDateString() + Environment.NewLine;
        _mainWindow.Textbox_Main.AppendText(create_date_Time);
    }
    
    public void Changefontsize()
    {
        
        var choosefont = new FontDialog();
        choosefont.ShowHelp = true;
        choosefont.ShowColor = true;
        choosefont.ShowEffects = true;
        choosefont.MinSize = 20;
        

        if (choosefont.ShowDialog() ==  DialogResult.OK)
        {
            _mainWindow.Textbox_Main.Text = choosefont.Font.FontFamily.Name;
            _mainWindow.Textbox_Main.FontSize = choosefont.Font.Size;
            _mainWindow.Textbox_Main.FontWeight = choosefont.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
            _mainWindow.Textbox_Main.FontStyle = choosefont.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            _mainWindow.Textbox_Main.TextDecorations = choosefont.Font.Underline ? TextDecorations.Underline : TextDecorations.Baseline;
             _mainWindow.Textbox_Main.TextDecorations = choosefont.Font.Strikeout ? TextDecorations.Strikethrough : null;
             _mainWindow.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(choosefont.Color.A, choosefont.Color.R, choosefont.Color.G, choosefont.Color.B));
             
        }
        
    }
    
    
    public void Newfile()
    {
        /*var capation = "Do you wish to save the current file before creating a new file";
        var newmessagebox = MessageBoxButtons.YesNoCancel;
        DialogResult Result;
        Result = MessageBox.Show(capation, _datatypes._Filepath ?? "Untitled", newmessagebox);*/
        MessageBoxResult Result;
        Result = System.Windows.MessageBox.Show("Do you wish to save the current file before creating a new file?", _fileService.Filepath ?? "Untitled", MessageBoxButton.YesNoCancel);

        switch (Result)
        {
            
            case MessageBoxResult.Yes:
                Savefile();
                ClearUI();
                break;
            case MessageBoxResult.No:
             ClearUI();
                break;
            case MessageBoxResult.Cancel:
                break;
        }
    }
   public void Format()
   {
       
       Char Format = '•';
       _mainWindow.Textbox_Main.TextWrapping = TextWrapping.Wrap;
       _mainWindow.Textbox_Main.AppendText(Format + "\n");
       
       
       
   }
}