using System.IO;
using System.Windows.Forms.VisualStyles;
using Notepad.Functions;
using static Notepad.MainWindow;


namespace Notepad;

public  class Methods 
{ 
        
        private MainWindow _mainWindow;
        private Datatypes _datatypes;
    
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
              
                  using(StreamWriter sw = new StreamWriter(_datatypes._Filepath))
                  {
                      sw.Write(_mainWindow.Textbox_Main.Text);
                      MessageBox.Show("File Saved");
                      
                  }
            }

            if (File.Exists(_datatypes._Filepath))
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
                        


                        _datatypes._Filepath = openfiledialog.FileName;
                        _mainWindow.Textblock_File_Path.Text = _datatypes._Filepath;
                        _mainWindow.Textblock_File_Type.Text = Path.GetExtension(_datatypes._Filepath);
                        
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
                 _mainWindow.Textblock_File_Path.Text = _datatypes._Filepath;
                  _mainWindow.Textblock_File_Type.Text = Path.GetExtension(_datatypes._Filepath);
              }
              break;
          case DialogResult.Cancel:
              break;
              //do nothing
          
          
         
         
         
      }
      
      
         
      
      
      
      
   }
   

    
    public void savefile()
    {
      
        
        
        
        
        
        
        
        
    }
    
    
    
}