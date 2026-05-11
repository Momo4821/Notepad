using Serilog;
using Serilog.Core;

namespace Notepad.Logging;

public class LoggingData
{
    
  public LoggingData ()
  {
    var levelSwitch = new LoggingLevelSwitch();
    
    
    Log.Logger = new LoggerConfiguration().
      WriteTo.File("Logs.Txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}").
      MinimumLevel.Information().
      MinimumLevel.ControlledBy(levelSwitch).
      CreateLogger();
    
    
    
  }
    
    
  
}