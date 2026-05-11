using System.Windows;
using System.Windows.Input;
using Notepad.CustomDialog;
using Notepad.Functions;
using Serilog;
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
        
        
        
        
AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
{
    Exception ex = (Exception)args.ExceptionObject;
    Log.Fatal("Unhandled Exception", ex);
    
};
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
        var create_date_Time
            = DateTime.Now.ToLongDateString() + "\n";
        bool test = Keyboard.IsKeyDown(Key.F5);
        bool Key_f5_Pressed = Keyboard.IsKeyDown(Key.F5);
        if (Key_f5_Pressed)
        {

            Textbox_Main.AppendText(create_date_Time);
            
        }


        bool Key_f6_Pressed = Keyboard.IsKeyDown(Key.F6);

        if (Key_f6_Pressed)
        {
            char format = '•';
            Textbox_Main.TextWrapping = TextWrapping.Wrap;
            


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




    