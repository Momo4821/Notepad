using System.IO;
using Notepad.CustomDialog;
using Notepad.Functions;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Notepad.Logging;

public class LoggingData
{

  
  private readonly MainWindow _mainWindow;
  private readonly FileService _fileService;
  private SettingsView _settingsView;
  private readonly LoggingData _loggingData;
  public LoggingData ()
  {
  
    _mainWindow = new MainWindow();
    _loggingData = new LoggingData();
    _settingsView = new SettingsView();
    _fileService = new FileService();
    
    
    var levelSwitch = new LoggingLevelSwitch();
    
    
    Log.Logger = new LoggerConfiguration().
      WriteTo.File("Logs/Logs.Txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}").
      MinimumLevel.Information().
      MinimumLevel.ControlledBy(levelSwitch).
      CreateLogger();
    
    Log.Information("Logger Initialized");
    
    
    
   
  
    
    
    
    
  }
  
  private void SwitchLoglevel_Debug ()
  {
    bool isinformation = Log.IsEnabled(LogEventLevel.Information);
    bool isdebug = Log.IsEnabled(LogEventLevel.Debug);
    
    
    if (isinformation)
    {
     
        
      _settingsView.InformationlogLevelButton.IsChecked = true;
    }
      
    
    if(isdebug)
    {
        
      _settingsView.DebugLogLevelButton.IsChecked = true;
    }
       
       
  }
    
    
  }