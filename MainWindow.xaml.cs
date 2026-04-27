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
    private readonly Methods _methods; // create an instance of the methods class to call the methods in the methods class
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
        
        Textbox_Main.TextChanged += Textbox_Main_OnTextChanged;

        var printdialog = new PrintDialog();
        var printdocument = new PrintDocument();
        var printersettings = new PrinterSettings();

        printdialog.CurrentPageEnabled = true;
       
        if (printdialog.ShowDialog() == true)
        {

            Textbox_Main.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            Textbox_Main.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;



            Textbox_Main.BorderThickness = new Thickness(0);


            printdialog.PrintVisual(Textbox_Main, "Printing Page");



    

        }


        Textbox_Main.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        Textbox_Main.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        Textbox_Main.BorderThickness = new Thickness(default);






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
        
        
        var _chose_font = new FontDialog();
        _chose_font.ShowColor = true;
        _chose_font.ShowApply = true;
        _chose_font.ShowHelp = true;
        _chose_font.MinSize = 20;



        if (_chose_font.ShowDialog() == System.Windows.Forms.DialogResult.OK)

        {


            // var color_convert = new SolidBrush(Color.FromArgb(_chose_font.Color.A, _chose_font.Color.R, _chose_font.Color.G, _chose_font.Color.B));
            Textbox_Main.Foreground = new SolidColorBrush(){Color = System.Windows.Media.Color.FromArgb(_chose_font.Color.A, _chose_font.Color.R, _chose_font.Color.G, _chose_font.Color.B)};
            Textbox_Main.FontFamily = new FontFamily(_chose_font.Font.Name); // font family of text font
            Textbox_Main.FontSize = _chose_font.Font.Size; // size of font3
            Textbox_Main.FontWeight = _chose_font.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
            Textbox_Main.FontStyle = _chose_font.Font.Italic ? FontStyles.Italic : FontStyles.Normal; // ten
            Textbox_Main.TextDecorations = _chose_font.Font.Strikeout ? TextDecorations.Strikethrough : TextDecorations.Baseline;
            Textbox_Main.TextDecorations =
                _chose_font.Font.Underline ? TextDecorations.Underline : TextDecorations.Baseline;
      ;
                
            // bold or regular font weight
            /*//different way to change text color from github copilot
            var dsrawing = _chose_font.Color;
            var mediaColor = System.Windows.Media.Color.FromArgb(dsrawing.A, dsrawing.R, dsrawing.G, dsrawing.B);
            Textbox_Main.Foreground = new System.Windows.Media.SolidColorBrush(mediaColor);*/



            // 

        }




    }

    

    private void New_OnClick(object sender, RoutedEventArgs e)
    {
        

        _methods.newfile();

              
    }
                
    

    
    //don't need this since this is not b
    private void Textbox_Main_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        //event handler to check if the text has been modified
      if(!_datatypes.Ismodified)
      {
          
          _datatypes.Ismodified = true;
          
      }
      
      
    }


    private void Textbox_Main_OnKeyDown(object sender, KeyEventArgs e)
    {
        var create_date_Time
            = DateTime.Now.ToLongDateString() + "\n";
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
        char format = '•';
        // Convert.ToString(format + "\n" );



        Textbox_Main.TextWrapping = TextWrapping.Wrap;
        Textbox_Main.AppendText(format + "\n");


    }


    

    
}




    