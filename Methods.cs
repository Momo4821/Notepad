using Microsoft.Win32;
using Notepad.CustomDialog;
using Notepad.Functions;
using Notepad.Logging;
using Serilog;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        _loggingData = LoggingData._loggingDataInstance;
       
    
        
        
    
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
            savefiledialog.InitialDirectory = _fileService.Downloads;
            savefiledialog.CheckFileExists = false;
            savefiledialog.OverwritePrompt = true;
           
            Log.Information("Save File Configuration Used.");
    }
    
    
    private void Configurationopenfiledialog(Microsoft.Win32.OpenFileDialog opendialog)
    {
    
        
        opendialog.Title = "Open File";
        opendialog.InitialDirectory = _fileService.Downloads;
        opendialog.RestoreDirectory = true;
        opendialog.DefaultExt = ".txt";
        opendialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        Log.Information("Open File Configuration Used.");
        
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
        _mainWindow.TextboxMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        _mainWindow.TextboxMain.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
        _mainWindow.TextboxMain.BorderThickness = new Thickness(0); 
        Log.Information($"Textbox Scroll {_mainWindow.TextboxMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled}.");
        
    }
    
    
    
    /// <summary>
    /// ClearUI method is used to clear the UI when creating a new file or when the user chooses not to save the current file before opening a new one.
    /// It clears the text box and resets the file path and file type labels.
    /// </summary>
    private void UpdateFilelables()
    {
        Log.Debug($"UpdatefilesLabls method called.");
        _mainWindow.LabelFilePath.Content = _fileService.FilePath;
        _mainWindow.LabelFileType.Content = _fileService.FileType ;
        Log.Information($"File {_fileService.FilePath} has been updated.");
        Log.Information($"File {_fileService.FileType} has been updated.");
    }
    
    private void ClearUI()
    {
        Log.Debug($"Clearing UI. Clearing Textbox{_mainWindow.TextboxMain.Text}, Filepath: {_fileService.FilePath} and Filetype: {_fileService.FileType}.");
        _mainWindow.TextboxMain.Clear();
        _mainWindow.LabelFilePath.Content = "";
        _mainWindow.LabelFileType.Content = "";
        _fileService.FilePath = null!;
       // Log.Information($"{_mainWindow.Textbox_Main.Text} and  has been Cleared");
       Log.Information("UI Cleared.");
        
    }
    
    
    /// <summary>
    ///File Operations for the application
    /// </summary>
    public void Openfile ()
   {
       Log.Debug("Openfile method called.");
       if (_fileService.HasFile)
       {
       Log.Debug($"File {_fileService.FilePath} is currently open. Prompting user to save before opening a new file.");
           var openfileMessagebox = MessageBoxButtons.YesNoCancel;
           DialogResult Result;
           string caption = "Do you wish to save the current file before opening a new one?";
           Result = MessageBox.Show(caption, _fileService.FilePath ?? "Untitled", openfileMessagebox);
            Log.Debug($"User selected {Result} in the message box.");
           switch (Result)
           {
            case DialogResult.Yes:
                Log.Debug("User chose to save the current file before opening a new one.");
                Savefile();
                break;
            case DialogResult.No:
                Log.Debug("User chose not to save the current file.");
                break;
            case DialogResult.Cancel:
                Log.Debug("User canceled the open file operation.");
                return;
               
               
           }
       
       
       }
       else
       {
           Log.Debug("No file is currently open. Proceeding to open a new file.");
       }
       Log.Debug("Opening file dialog to select a file to open.");
       var openfileDialog = new Microsoft.Win32.OpenFileDialog();
       Configurationopenfiledialog(openfileDialog);
       if (openfileDialog.ShowDialog() == true)
       {
           
           ClearUI();
           using(StreamReader sr = new StreamReader(openfileDialog.OpenFile()))
           {
               _fileService.FilePath = openfileDialog.FileName;
               _mainWindow.TextboxMain.Text = sr.ReadToEnd();
              
            }
           UpdateFilelables();



        }
           
   }
    
    
    public void Savefile()
    {
        Log.Debug("Savefile method called.");
        if (_fileService.HasFile)
        {
            Log.Debug($"File {_fileService.FilePath} is currently open. Saving the file.");
            using (StreamWriter sw = new StreamWriter(_fileService.FilePath))
            {
                sw.Write(_mainWindow.TextboxMain.Text);
                
            }
            MessageBox.Show("File Saved.");
            Log.Information($"File Saved {_fileService.FileName}.");
            UpdateFilelables();
            Log.Information("Labels Updated.");
            
            
        }
        else
        {
            Log.Debug("No file is currently open. Prompting user to save the file as a new file.");
            SaveFileas(); 
            
        }
        
        
    }
    
    public void SaveFileas ()
    { 
        Log.Debug("SaveFileas method called.");
    var savefileas = new Microsoft.Win32.SaveFileDialog();
    Configuraitonsavefiledialog(savefileas);
    if(savefileas.ShowDialog() == true)
    {
        Log.Debug($"Saving new file as {savefileas.FileName}.");
        using (StreamWriter sw = new StreamWriter(savefileas.OpenFile()))
        {
            sw.Write(_mainWindow.TextboxMain.Text);
        }
        _fileService.FilePath = savefileas.FileName;
        UpdateFilelables();
        Log.Information("File saved successfully at {Path}.", _fileService.FilePath);
        MessageBox.Show("File Saved.");   
        
    }
    else
    {
        Log.Debug("User canceled the Save As operation.");
    }
    }
    public void Print()
    {
        Log.Debug("Print method called.");
        var printdialog = new System.Windows.Controls.PrintDialog();
        Printdialogconfiguration(printdialog);
        Log.Debug("PrintDialog UI Method Called.");
        PrintDialogClearUi();


        try
        {   Log.Debug("PrintDialog Shown.");
            if (printdialog.ShowDialog() == true)
            {
              
                Log.Information("Printing Document.");
                printdialog.PrintVisual(_mainWindow.TextboxMain, "Print Document");
            
            }
            else
            {
                Log.Debug("User canceled the print operation.");
            }
        }
        catch (Exception e)
        {
            Log.Error($"An error occurred while trying to print the document. Please check your printer settings and try again.{e}");
            MessageBox.Show($"An error occurred while trying to print the document. Please check your printer settings and try again.{e}");
            throw;
        }
       
      
        Log.Debug("Restoring Textbox Scrollbar Visibility and Border Thickness after printing.");
        _mainWindow.TextboxMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        _mainWindow.TextboxMain.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        _mainWindow.TextboxMain.BorderThickness = new Thickness(1);
      
    }
    
    public void Exit()
    {
       Log.Debug("Exit method called");
        MessageBoxResult Result;
        string caption = "Do you wish to exit the application without saving the current file?";
        Result = System.Windows.MessageBox.Show(caption, _fileService.FilePath ?? "Untitled", MessageBoxButton.YesNoCancel);
       
        Log.Debug($"User Selected: {Result} in the exit prompt.");
        switch (Result)
        {
            case MessageBoxResult.Yes:
                Log.Information("Exiting Application without saving the current file.");
                System.Windows.Application.Current.Shutdown();
                break;
            case MessageBoxResult.No:
                Savefile();
                Log.Information("Exiting Application  with saving the current file.");
                System.Windows.Application.Current.Shutdown();
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
        _mainWindow.TextboxMain.AppendText(create_date_Time);
    }
    
    public void Changefontsize()
    {
        Log.Debug("Changefontsize method called.");
        var choosefont = new FontDialog();
        choosefont.ShowHelp = true;
        choosefont.ShowColor = true;
        choosefont.ShowEffects = true;
        choosefont.MinSize = 20;
        Log.Debug("Font dialog initialized.");

        if (choosefont.ShowDialog() ==  DialogResult.OK)
        {
            Log.Information("Font Changed to {0}, size {1}.", choosefont.Font.FontFamily.Name, choosefont.Font.Size);
            _mainWindow.TextboxMain.Text = choosefont.Font.FontFamily.Name;
            _mainWindow.TextboxMain.FontSize = choosefont.Font.Size;
            _mainWindow.TextboxMain.FontWeight = choosefont.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
            _mainWindow.TextboxMain.FontStyle = choosefont.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            _mainWindow.TextboxMain.TextDecorations = choosefont.Font.Underline ? TextDecorations.Underline : TextDecorations.Baseline;
             _mainWindow.TextboxMain.TextDecorations = choosefont.Font.Strikeout ? TextDecorations.Strikethrough : null;
             _mainWindow.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(choosefont.Color.A, choosefont.Color.R, choosefont.Color.G, choosefont.Color.B));
             
        }
        else
        {
            Log.Debug("Font Dialog Canceled by user.");
        }
        
    }
    
    
    public void Newfile()
    {
        Log.Debug("Newfile method called.");
        MessageBoxResult Result;
        Result = System.Windows.MessageBox.Show("Do you wish to save the current file before creating a new file?", _fileService.FilePath ?? "Untitled", MessageBoxButton.YesNoCancel);

        Log.Debug($"User Selected: {Result} in the new file prompt.");
        switch (Result)
        {
            
            case MessageBoxResult.Yes:
                
                Savefile();
                ClearUI();
                Log.Information("New file created after saving.");
                break;
            case MessageBoxResult.No:
                ClearUI();
                Log.Information("New file created without saving.");
                break;
            case MessageBoxResult.Cancel:
                Log.Debug("New file creation canceled.");
                break;
        }
    }
   public void Format()
   {
       Log.Debug("Format method called.");
       Char Format = '•';
       _mainWindow.TextboxMain.TextWrapping = TextWrapping.Wrap;
       _mainWindow.TextboxMain.AppendText(Format + "\n");
       Log.Information("• Format Used");
       
       
       
   }
   
   public void Settingsmenu()
   {
       Log.Debug("Settingsmenu method called.");
       
    _settingsView = new  SettingsView();
    _settingsView.Show();
    Log.Debug("Settings menu opened.");
    

   }
   
}