using System.IO;
using Notepad.Functions;



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
        
        
    }
    
    
    
    public void exit()
    {
        
        
    }
    
    
    
    
    public void changefontsize()
    {
        
        
    }
    
    
    
    public void newfile()
    {
        
        
    }
    
    
    
    
}