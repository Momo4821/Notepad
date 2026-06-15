using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Notepad.CustomDialog;
using Notepad.Functions;
using Notepad.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;


namespace Notepad;

public  class Methods 
{ 
        
        private readonly MainWindow _mainWindow;
        private readonly FileService _fileService;
        private SettingsView _settingsView;
        private readonly LoggingData _loggingData;
    
 
    
    
    public Methods(MainWindow mainWindow, SettingsView settingsView)
    {
        _settingsView = settingsView;
        _mainWindow = mainWindow;
        _fileService = new FileService();
        _loggingData = new LoggingData();
       
    
        
        
    
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
            savefiledialog.CheckFileExists = false;
            savefiledialog.OverwritePrompt = true;
            Log.Information("Save File Configuration Used");
    }
    
    
    private void Configurationopenfiledialog(Microsoft.Win32.OpenFileDialog opendialog)
    {
    
        
        opendialog.Title = "Open File";
        opendialog.InitialDirectory = _fileService.Downlaods;
        opendialog.RestoreDirectory = true;
        opendialog.DefaultExt = ".txt";
        opendialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        Log.Information("Open File Configuration Used");
        
    }
    
    private void Printdialogconfiguration(System.Windows.Controls.PrintDialog printdialog)
    {
        printdialog.PageRangeSelection = System.Windows.Controls.PageRangeSelection.AllPages;
        printdialog.UserPageRangeEnabled = false;
        printdialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
        
        printdialog.PrintTicket.PageMediaSize = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.NorthAmericaLetter);
    }
    
    private void PrintDialogClearUi ()
    {
        _mainWindow.Textbox_Main.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        _mainWindow.Textbox_Main.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
        _mainWindow.Textbox_Main.BorderThickness = new Thickness(0); 
        Log.Logger.Information($"Textbox Scroll {_mainWindow.Textbox_Main.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled}");
        
    }
    
    
    
    /// <summary>
    /// ClearUI method is used to clear the UI when creating a new file or when the user chooses not to save the current file before opening a new one.
    /// It clears the text box and resets the file path and file type labels.
    /// </summary>
    private void UpdateFilelables(FileService fileService)
    {
        _mainWindow.LabelFilePath.Content = _fileService.Filepath;
        _mainWindow.LabelFileType.Content = _fileService.FileType;
        Log.Logger.Information($"File {_fileService.Filepath} has been updated");
        Log.Logger.Information($"File {_fileService.FileType} has been updated");
    }
    
    private void ClearUI()
    {
        _mainWindow.Textbox_Main.Clear();
        _mainWindow.LabelFilePath.Content = "";
        _mainWindow.LabelFileType.Content = "";
        _fileService.Filepath = null!;
       // Log.Logger.Information($"{_mainWindow.Textbox_Main.Text} and  has been Cleared");
       Log.Logger.Information("UI Cleared");
        
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
            Log.Logger.Information($"File Saved {_fileService.FileName}");
            UpdateFilelables(_fileService);
            Log.Logger.Information("Labels Updated");
            
            
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
        Log.Information("File saved successfully at {Path}", _fileService.Filepath);
        MessageBox.Show("File Saved");   
        
    }
    }
    public void Print()
    {
        
        var printdialog = new System.Windows.Controls.PrintDialog();
        Printdialogconfiguration(printdialog);
        Log.Information("PrintDialog UI Cleared");
        PrintDialogClearUi();


        try
        {       Log.Information("PrintDialog Shown");
            if (printdialog.ShowDialog() == true)
            {
              
                
                printdialog.PrintVisual(_mainWindow.Textbox_Main, "Print Document");
            
            }
        }
        catch (Exception e)
        {
            Log.Error($"An error occurred while trying to print the document. Please check your printer settings and try again.{e}");
            MessageBox.Show($"An error occurred while trying to print the document. Please check your printer settings and try again.{e}");
            throw;
        }
       
        Log.Information("Texbox Horizonital ScrollBar Visibility Set to Visible");
        _mainWindow.Textbox_Main.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        _mainWindow.Textbox_Main.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        Log.Information("Texbox Veritcal ScrollBar Visibility Set to Visible");
        Log.Information("Textbox Border Thickness Set to Visible");
        _mainWindow.Textbox_Main.BorderThickness = new Thickness(1);
      
    }
    
    public void Exit()
    {
        MessageBoxResult Result;
        string caption = "Do you wish to exit the application without saving the current file?";
        Result = System.Windows.MessageBox.Show(caption, _fileService.Filepath ?? "Untitled", MessageBoxButton.YesNoCancel);
        switch (Result)
        {
            case MessageBoxResult.Yes:
                Log.Information("Exiting Application");
                Application.Exit();
                break;
            case MessageBoxResult.No:
                Savefile();
                Log.Information("Exiting Application");
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
       Log.Information("• Format Used");
       
       
       
   }
   
   public void Settingsmenu()
   {
       
    _settingsView = new  SettingsView();
    

   }
   
}