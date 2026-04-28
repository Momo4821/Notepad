using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Notepad.Functions;
using FontFamily = System.Windows.Media.FontFamily;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

using PrintDialog = System.Windows.Controls.PrintDialog;


namespace Notepad;

public partial class MainWindow
{
    private readonly Methods _methods; 
    private readonly Datatypes _datatypes;
    public MainWindow ()
    {
        InitializeComponent();
        
        _methods = new Methods(this);
        _datatypes = new Datatypes();
        
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
        _methods.savefile_as();
    }


    private void Print_OnClick(object sender, RoutedEventArgs e)
    {
        _methods.print();

    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
       _methods.exit();
    }


    private void Time_Date_Stamp_OnClick(object sender, RoutedEventArgs e)
    {


        var create_date_Time
            = DateTime.Now.ToLongDateString() + Environment.NewLine;
        Textbox_Main.AppendText(create_date_Time);



    }
    
    
    private void Change_Font_OnClick(object sender, RoutedEventArgs e)
    {
        _methods.changefontsize();
        
    }

    

    private void New_OnClick(object sender, RoutedEventArgs e)
    {
        
        _methods.newfile();
    }
    
    private void Textbox_Main_OnKeyDown(object sender, KeyEventArgs e)
    {
        var create_date_Time
            = DateTime.Now.ToLongDateString() + "\n";
        bool test = Keyboard.IsKeyDown(Key.F5);
        bool Key_f5_Pressed = Keyboard.IsKeyDown(Key.F5);
        // F5 Key Pressed - Insert Date and Time
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
        
        /*
        char format = '•';
        // Convert.ToString(format + "\n" );



        Textbox_Main.TextWrapping = TextWrapping.Wrap;
        Textbox_Main.AppendText(format + "\n");
        */


    }
}




    