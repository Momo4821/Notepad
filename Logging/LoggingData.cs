using System.IO;
using Notepad.CustomDialog;
using Notepad.Functions;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog;
using Serilog.Core;
using Serilog.Events;
namespace Notepad.Logging;

public class LoggingData
{
  public static LoggingData  _loggingDataInstance { get; } = new LoggingData();
  private readonly LoggingLevelSwitch _levelSwitch;
  private LoggingData ()
  {
    _levelSwitch = new LoggingLevelSwitch(initialMinimumLevel: LogEventLevel.Information);
   
    
    
    Log.Logger = new LoggerConfiguration().
      WriteTo.File("Logs/Logs.Txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}").
      MinimumLevel.Information().
      MinimumLevel.ControlledBy(_levelSwitch).
      CreateLogger();
    
    Log.Information("Logger Initialized");
    
    
  }
  
public void SetLogLevelInformation()
{
  _levelSwitch.MinimumLevel = LogEventLevel.Information;
  Log.Information("Log Level set to Information.");
  
}

public void SetLogLevelDebug()
{
  _levelSwitch.MinimumLevel = LogEventLevel.Debug;
  Log.Debug("Log Level set to Debug.");
}
  
  
public LogEventLevel GetCurrentLogLevel ()
{
  
  return _levelSwitch.MinimumLevel;
  
}
  
    
    
  }