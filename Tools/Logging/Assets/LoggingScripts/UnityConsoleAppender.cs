using UnityEngine;
using log4net.Appender;
using log4net.Core;

/// <summary> 
/// Appender für die Unity Console.
/// Je nach Level des Logging-Outputs werden verschiedene Funktionen der Unity-Klasse Debug verwendet.
/// Quelle: https://stackoverflow.com/questions/23796412/how-to-use-use-log4net-with-unity/
/// </summary>
public class UnityConsoleAppender : AppenderSkeleton
{

    protected override void Append(LoggingEvent loggingEvent)
    {
        var properties = loggingEvent.Properties;
        properties["MyProperty"] = "I_am_a_CustomProperty";
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

        AddStringProperty("CustomProperty");
    }

    public void AddStringProperty(string value)
    {
        // do whatever has to be done

    }
}