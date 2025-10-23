using System.IO;
using System.Windows.Controls;

namespace Notepad;

public class FilesModified: MainWindow
{

    //method prompting saving


    public void Prompt_save_when_open_file ()
    {

        MessageBoxButtons Buttons = MessageBoxButtons.YesNoCancel;
        
        
        
        SaveFileDialog savefile_dialog = new SaveFileDialog();
        savefile_dialog.Filter = "Text files (*.txt)|*.txt";
        savefile_dialog.AddExtension = true;
        savefile_dialog.DefaultExt = ".txt";
        
        string file_Path
            = savefile_dialog.FileName;
        var lastsavetime = File.GetLastWriteTime(file_Path);



    
        
        
        
        
        
    }
    
    
    

    
    public void Texbox_Main_TextChanged (object sender, TextChangedEventArgs e)
    {
        var ismodified = false;


        if (ismodified = true)

        {
            //prompt saving when opening file
            Prompt_save_when_open_file();
            
            
        }
        
        
        
        
    }

    
}
