using log4net.Appender;
using log4net.Core;
using UnityEngine;

/// <summary>
/// Beispiel für einen Log4NET Appender
/// 
/// Dieser Appender verwendet Debug.Log, um die nachrichten
/// von Log4net in die Unity-Konsole zu bringen.
/// 
/// Quelle: http://infalliblecode.com/advanced-unity-debugging-with-log4net/
/// </summary>
public class UnityDebugAppender : AppenderSkeleton
{
  protected override void Append(LoggingEvent loggingEvent)
  {
    var message = RenderLoggingEvent(loggingEvent);
    Debug.Log(message);
  }
}
