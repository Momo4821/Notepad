using System.IO;
using Notepad.Functions;
using static Notepad.MainWindow;


namespace Notepad;

public class Methods
{
    
    MainWindow MainWindow;
    Datatypes _Datatypes;
    
    
    public void Openfile ()
    {
        
        _Datatypes = new Datatypes();

        var Caption = "Do you wish to save current file before opening another file";
        var openfile_messagebox = new  MessageBoxButtons();
        DialogResult Result;
        Result = System.Windows.Forms.MessageBox.Show(Caption, string.Empty, openfile_messagebox);
        Convert.ToBoolean(Result);
        
        
        
        var savefiledialog = new SaveFileDialog();
        _Datatypes._Filepath = savefiledialog.FileName;
        switch(Result)
        {
            case DialogResult.Yes:
                if (!File.Exists(_Datatypes._Filepath) && savefiledialog.ShowDialog() == DialogResult.OK )
                {
                    savefiledialog.Title = "Save";
                    savefiledialog.FileName = "Untitled";
                    savefiledialog.InitialDirectory = _Datatypes._Downloads;
                    savefiledialog.DefaultExt = "txt";
                    savefiledialog.AddExtension = true;
                    
                    using (StreamWriter sw = new StreamWriter(savefiledialog.FileName))
                    {
                        sw.Write(MainWindow.Textbox_Main);
                        MessageBox.Show("File Save", _Datatypes._Filepath);
                        MainWindow.Textblock_File_Type.Text = Path.GetExtension(_Datatypes._Filepath);
                    }
                }
                else if(File.Exists(_Datatypes._Filepath))
                {
                    using (StreamWriter sw = new StreamWriter(_Datatypes._Filepath))
                    {
                        sw.Write(MainWindow.Textbox_Main.Text);
                        MessageBox.Show("File Save", _Datatypes._Filepath);
                    }
                    
                    
                }
                
                //open file after saving
                var openfiledialog = new OpenFileDialog();
                openfiledialog.Title = "Open";
                openfiledialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openfiledialog.InitialDirectory = _Datatypes._Downloads;
                openfiledialog.DefaultExt = "txt";
                
                
                
                
                break;
            case DialogResult.No:
                break;
            case DialogResult.Cancel:
                break;
                
    }         
    
    }
    
    }