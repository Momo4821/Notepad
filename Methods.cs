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
        private readonly Datatypes _datatypes;
    
    public Methods(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        _datatypes = new Datatypes();
    }
   
    
    public void Openfile ()
   {
      string caption = "Do you wish to save the current file before opening a new one";
      var openfilemessagebox = MessageBoxButtons.YesNoCancel;
      DialogResult Result;
      Result = MessageBox.Show(caption, string.Empty, openfilemessagebox);
       Convert.ToBoolean(Result);
       var savefiledialog = new SaveFileDialog();
       var openfiledialog = new OpenFileDialog();
       
      switch (Result)
      {
        case DialogResult.Yes:
            MessageBox.Show(caption);
            if (!File.Exists(_datatypes._Filepath) && savefiledialog.ShowDialog() == DialogResult.OK)
            {
              
              savefiledialog.Title = "Save File";
              savefiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
              savefiledialog.InitialDirectory = _datatypes._Downloads;
              savefiledialog.RestoreDirectory = true;
              savefiledialog.AddExtension = true;
              savefiledialog.DefaultExt = ".txt";
              savefiledialog.CheckFileExists = true;
              savefiledialog.OverwritePrompt = true;
              
                  using(StreamWriter sw = new StreamWriter(_datatypes._Filepath))
                  {
                      sw.Write(_mainWindow.Textbox_Main.Text);
                      MessageBox.Show("File Saved");
                      
                  }
            }
            else if (File.Exists(_datatypes._Filepath))
            {
                using (StreamWriter sw = new StreamWriter(_datatypes._Filepath))
                {
                    
                    sw.Write(_mainWindow.Textbox_Main.Text);
                    MessageBox.Show("File Saved", _datatypes._Filepath);
                    
                }
                
                //open file after saving
                openfiledialog.Title = "Open File";
                openfiledialog.InitialDirectory = _datatypes._Downloads;
                openfiledialog.RestoreDirectory = true;
                openfiledialog.DefaultExt = ".txt";
                openfiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                
                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    var filestream = openfiledialog.OpenFile();
                    using (StreamReader sr = new StreamReader(filestream))
                    {
                        

                        _mainWindow.Textbox_Main.Text = sr.ReadToEnd();
                        _datatypes._Filepath = openfiledialog.FileName;
                        _mainWindow.LabelFileType.Content = _datatypes._Filetype;
                        _mainWindow.LabelFilePath.Content = Path.GetExtension(_datatypes._Filepath);
                        
                    }
                }
            }
            break;
          case DialogResult.No:
              if (openfiledialog.ShowDialog() == DialogResult.OK)
              { 
                  using StreamReader sr = new StreamReader(openfiledialog.OpenFile());
                 _mainWindow.Textbox_Main.Text = sr.ReadToEnd();
                 _datatypes._Filepath = openfiledialog.FileName;
                 _mainWindow.LabelFilePath.Content = _datatypes._Filepath;
                  _mainWindow.LabelFilePath.Content = Path.GetExtension(_datatypes._Filetype);
              }
              break;
          case DialogResult.Cancel:
              break;
              //do nothing
         
      }
   }
    
    public void Savefile()
    {
      
        var savefiledialog  = new SaveFileDialog();
        savefiledialog.Title = $"$Save File {_datatypes._Filename}";
        savefiledialog.RestoreDirectory = true;
        savefiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        savefiledialog.InitialDirectory = _datatypes._Downloads;
        savefiledialog.CheckFileExists = true;
        savefiledialog.OverwritePrompt = true;
        
        
        if (!File.Exists(_datatypes._Filepath) && savefiledialog.ShowDialog() == DialogResult.OK)
        {
            
            savefiledialog.AddExtension = true;
            savefiledialog.DefaultExt = ".txt";
            using (StreamWriter sw = new StreamWriter(savefiledialog.OpenFile()))
            {
                sw.Write(_mainWindow.Textbox_Main.Text);
                MessageBox.Show("File Saved");
                _mainWindow.LabelFilePath.Content = savefiledialog.FileName;
                _mainWindow.LabelFileType.Content = Path.GetExtension(savefiledialog.FileName);
                
            }
        }
        else if (File.Exists(_datatypes._Filepath))
        {
            using (StreamWriter sw = new StreamWriter(_datatypes._Filepath))
            {
                
                sw.Write(_mainWindow.Textbox_Main.Text);
                MessageBox.Show("File Saved", _datatypes._Filepath + _datatypes._Filename);
                _mainWindow.LabelFilePath.Content = _datatypes._Filepath;
                _mainWindow.LabelFileType.Content = Path.GetExtension(_datatypes._Filepath);
                
                
            }
        }
        
    }
    
    public void savefile_as ()
    {
        var savefiledialog  = new SaveFileDialog();
        savefiledialog.Title = $"$Save File As {_datatypes._Filename}";
        savefiledialog.RestoreDirectory = true;
        savefiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        savefiledialog.InitialDirectory = _datatypes._Downloads;
        savefiledialog.CheckFileExists = true;
        
        if (savefiledialog.ShowDialog() == DialogResult.OK)
        {
            savefiledialog.AddExtension = true;
            savefiledialog.DefaultExt = ".txt";
            using (StreamWriter sw = new StreamWriter(savefiledialog.OpenFile()))
            {
                sw.Write(_mainWindow.Textbox_Main.Text);
                MessageBox.Show("File Saved");
                _mainWindow.LabelFilePath.Content = savefiledialog.FileName;
                _mainWindow.LabelFileType.Content = Path.GetExtension(savefiledialog.FileName);
                
            }
        }
        
         else if (File.Exists(_datatypes._Filepath))
         {
              using (StreamWriter sw = new StreamWriter(_datatypes._Filepath))
              {
                  sw.Write(_mainWindow.Textbox_Main.Text);
                  MessageBox.Show("File Saved", _datatypes._Filepath);
                  
              }
             
         }
    }
    
    public void print()
    {
        var printDialog = new PrintDialog();
        printDialog.Document.DocumentName = _datatypes._Filename ?? "Untitled";
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
    
    public void exit()
    {
        string caption = "Do you wish to exit the application without saving the current file?";
        var exitmessagebox = MessageBoxButtons.YesNoCancel;
        DialogResult Result;
        Result = MessageBox.Show(caption, _datatypes._Filename ?? "Untitled", exitmessagebox);
     
        
        switch (Result)
        {
            case DialogResult.Yes:
                Application.Exit();
                break;
            case DialogResult.No:
                Savefile();
                Application.Exit();
                break;
            case DialogResult.Cancel:
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
    
    public void changefontsize()
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
    
    
    
    public void newfile()
    {
        var capation = "Do you wish to save the current file before creating a new file";
        var newmessagebox = MessageBoxButtons.YesNoCancel;
        DialogResult Result;
        Result = MessageBox.Show(capation, _datatypes._Filepath ?? "Untitled", newmessagebox);


        switch (Result)
        {
            
            case DialogResult.Yes:
                Savefile();
                 _mainWindow.Textbox_Main.Clear();
                 _datatypes._Filepath = null;
                 _datatypes._Filename = null;
                 _mainWindow.LabelFilePath.Content = "File Path: " + _datatypes._Filepath;
                 _mainWindow.LabelFileType.Content = "File Type: " + _datatypes._Filetype;
                break;
            case DialogResult.No:
                _mainWindow.Textbox_Main.Clear();
                _datatypes._Filepath = null;
                _datatypes._Filename = null;
                break;
            case DialogResult.Cancel:
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