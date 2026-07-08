using System.Windows;
using Notepad.Logging;

namespace Notepad.CustomDialog;



public partial class SettingsView : Window
{ 
    
    private readonly LoggingData _loggingData;
    
    public SettingsView()
    {
        InitializeComponent();
        _loggingData = LoggingData._loggingDataInstance;
        LogLevelButtons();
        
    }


    private void InformationlogLevelButton_OnClick(object sender, RoutedEventArgs e)
    {
        _loggingData.SetLogLevelInformation();
        System.Windows.MessageBox.Show("Log Level set to Information.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void DebugLogLevelButton_OnClick(object sender, RoutedEventArgs e)
    {
        _loggingData.SetLogLevelDebug();
        System.Windows.MessageBox.Show("Log Level set to Debug.", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);
        
    }
    
    
    private void LogLevelButtons ()
    {
        var level = _loggingData.GetCurrentLogLevel();
        InformationlogLevelButton.IsChecked = level == Serilog.Events.LogEventLevel.Information;
        DebugLogLevelButton.IsChecked = level == Serilog.Events.LogEventLevel.Debug;
        
        
    }
}