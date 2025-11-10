using System.Configuration;
using System.Data;
using System.Windows;
using Application = System.Windows.Application;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Window_Startup(object sender, StartupEventArgs args)
        {
            Window window1 = new Window();

            window1.Title = "Notepad";
            window1.Height = 600;
            window1.Width = 800;
            window1.Show();
        }
        
private void theme_changed(object sender, System.EventArgs e)
        {
            // Code to handle theme change
        }




        
        
        
        

    }

}                  