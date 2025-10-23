using System.Windows.Controls;
using System.Windows.Threading;
using static Notepad.MainWindow;
using System.Drawing.Printing;
using System.IO;
using System.Media;
using System.Printing.IndexedProperties;
using System.Text;
using System.Windows;
using System.Windows.Forms;

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
using Microsoft.VisualBasic.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static Notepad.FilesModified;
using Keyboard = Microsoft.VisualBasic.Devices.Keyboard;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using Size = System.Windows.Size;
using TextBox = System.Windows.Controls.TextBox;
namespace Notepad;

public class FilesModified
{

    //method prompting saving


    public void Prompt_save_when_open_file ()
    {
        
        
        
        
        
        SaveFileDialog savefile_dialog = new SaveFileDialog();
        savefile_dialog.Filter = "Text files (*.txt)|*.txt";
        savefile_dialog.AddExtension = true;
        savefile_dialog.DefaultExt = ".txt";
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
