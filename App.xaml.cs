using System.Configuration;
using System.Data;
using System.Windows;
using Notepad.Logging;
using Serilog;
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
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception ex = (Exception)args.ExceptionObject;
                Log.Fatal("Unhandled Exception", ex);
    
            };
            
            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                Log.Fatal("Unobserved TaskException", args.Exception);
                args.SetObserved();
            };
            
            
            DispatcherUnhandledException += (sender, args) =>
            {
                Log.Fatal("Unhandled DispatcherUnhandledException", args.Exception);
                args.Handled = true;
                
            };
        }
        

    }

}                  