using UnityEngine;
using log4net;
using log4net.Appender;
using log4net.Core;

/// <summary> 
/// Beispiel für einen Log4net Appender in Unity.
/// 
/// Je nach Level des Logging-Outputs werden verschiedene
/// Funktionen der Unity-Klasse Debug verwendet.
/// 
/// Quelle: https://stackoverflow.com/questions/23796412/how-to-use-use-log4net-with-unity
/// </summary>
public class UnityAppender : AppenderSkeleton
{
  /// <inheritdoc />
  protected override void Append(LoggingEvent loggingEvent)
  {
    string message = RenderLoggingEvent(loggingEvent);

    if (Level.Compare(loggingEvent.Level, Level.Error) >= 0)
    {
      // everything above or equal to error is an error
      Debug.LogError(message);
    }
    else if (Level.Compare(loggingEvent.Level, Level.Warn) >= 0)
    {
      // everything that is a warning up to error is logged as warning
      Debug.LogWarning(message);
    }
    else
    {
      // everything else we'll just log normally
      Debug.Log(message);
    }
  }
}