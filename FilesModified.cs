using System.IO;
using System.Windows.Controls;
using Notepad;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using TextBox = System.Windows.Controls.TextBox;
using static Notepad.MainWindow;
using Button = System.Windows.Controls.Button;

namespace Notepad;


public class FilesModified : MainWindow
{





 /*  public void _save_file_Before_Open_File()
   {

       Buttons = MessageBoxButtons.YesNoCancel;
       string save_prompt_beofre_open = "You have unsaved changes. Do you want to save before opening a new file?";
       DialogResult Result;
       Result = System.Windows.Forms.MessageBox.Show(save_prompt_beofre_open, file_Path, Buttons);
       Convert.ToBoolean(Result);


       var original_text = File.Exists(file_Path.ToString());
       bool ismodified = original_text.ToString() != Textbox_Main.Text;




       if (ismodified)
       {
           ismodified = true;





       }




    }*/

}