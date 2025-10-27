using System.IO;
using System.Windows.Controls;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using TextBox = System.Windows.Controls.TextBox;

namespace Notepad;

public class FilesModified : MainWindow
{

    public void Original_text_Before_Saving()
    {
        var originalText = openfile_dialog.OpenFile();
       bool ismodified = Textbox_Main.Text != originalText.ToString();
        
        
    }
     
    
    
    
    
    public void Prompt_save_when_Open_New_File_when_file_is_Already_Open()
    {
        void Texbox_Main_TextChanged(object sender, TextChangedEventArgs e)
        {

    
            Buttons = MessageBoxButtons.YesNoCancel;
            string caption = "Do you wish to save current file before opening another file";
            DialogResult Result;
            Result = System.Windows.Forms.MessageBox.Show(caption, file_Path, Buttons);
            Convert.ToBoolean(Result);
            Textbox_Main = sender as TextBox; // 
           var originalText = openfile_dialog.OpenFile(); // get original text from file
            bool ismodified = Textbox_Main.Text != originalText.ToString();

            switch (Result)
            {
                case System.Windows.Forms.DialogResult.Yes:
                 
                        if (ismodified)
                        {
                            if (savefile_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                            var last_modified = File.GetLastWriteTime(file_Path);
                            Convert.ToString(last_modified);
                            savefile_dialog.Title = file_Path;
                            savefile_dialog.FileName = file_Path;
                            savefile_dialog.Filter = "All files (*.*)|*.*";
                            savefile_dialog.InitialDirectory = downloads;
                            using (streamWriter = new StreamWriter(savefile_dialog.FileName))
                            {
                                streamWriter.Write(Textbox_Main);
                                MessageBox.Show($"File Saved  {last_modified} ", file_Path);



                            }



                        }

                    }
                        else
                        {
                            //do nothing
                        }

                        break;
                        case System.Windows.Forms.DialogResult.No:
                        //open file anyway
                        //do nothing


                        break;
                        case System.Windows.Forms.DialogResult.Cancel:
                        break;

                    }
            }
        }
    }


