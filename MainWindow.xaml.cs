using System.Windows;
using System.Windows.Input;
using Notepad.CustomDialog;
using Notepad.Functions;

using KeyEventArgs = System.Windows.Input.KeyEventArgs;


namespace Notepad;

public partial class MainWindow
{
    private readonly Methods _methods; 
    private readonly FileService _fileService;
    public MainWindow ()
    {
        InitializeComponent();
        
        var settingsview = new SettingsView();
        _methods = new Methods(this, settingsview);
        _fileService = new FileService();
    }
    
    public void OpenFile_OnClick(object sender, RoutedEventArgs e)
    {
      _methods.Openfile();
       
    }
    
    public void Save_OnClick(object sender, RoutedEventArgs e)
    {
     
        _methods.Savefile();
        
    }

    private void Save_As_OnClick(object sender, RoutedEventArgs e)
    {
        _methods.SaveFileas();
    }


    private void Print_OnClick(object sender, RoutedEventArgs e)
    {
        _methods.Print();

    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
       _methods.Exit();
    }


    private void Time_Date_Stamp_OnClick(object sender, RoutedEventArgs e)
    {
      _methods.Timestamp();

    }
    
    
    private void Change_Font_OnClick(object sender, RoutedEventArgs e)
    {
        _methods.Changefontsize();
        
    }
    
    private void New_OnClick(object sender, RoutedEventArgs e)
    {
        
        _methods.Newfile();
    }
    
    private void Textbox_Main_OnKeyDown(object sender, KeyEventArgs e)
    {
        var createDateTime
            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        bool keyF5Pressed = Keyboard.IsKeyDown(Key.F5);
        bool  keyF6Pressed = Keyboard.IsKeyDown(Key.F6);
       int caret = Textbox_Main.SelectionStart; 
       char format = '\u2022';
       
        
        if(keyF5Pressed)
        {
            /*Textbox_Main.Focus();
            Textbox_Main.Text = Textbox_Main.Text.Insert(caret,format.ToString());
            Textbox_Main.Focus();
            Textbox_Main.SelectionStart =  caret;*/
            
            
            Textbox_Main.Focus();
            Textbox_Main.SelectionStart = Textbox_Main.Text.Length;

            Textbox_Main.Text = Textbox_Main.Text.Insert(caret, createDateTime);
            Textbox_Main.SelectionStart = caret + createDateTime.Length;
         
         
         
 
            
        }
        
        
        if(keyF6Pressed)
        {
            
            
           Textbox_Main.Focus();
           Textbox_Main.SelectionStart = Textbox_Main.SelectionStart;
           Textbox_Main.Text = Textbox_Main.Text.Insert(caret, format.ToString());
           Textbox_Main.SelectionStart = caret + format.ToString().Length;
           
            
        }
    }

    private void Format_OnClick(object sender, RoutedEventArgs e)
    {
    _methods.Format();

    }

    private void Settings_OnClick(object sender, RoutedEventArgs e)
    {
       _methods.Settingsmenu();
    }
}




    