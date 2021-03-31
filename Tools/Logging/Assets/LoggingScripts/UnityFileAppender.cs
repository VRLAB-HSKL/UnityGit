using UnityEngine;
using log4net.Appender;
using log4net.Core;

/*
    File Appender für Unity. Falls Log4nets eigener FileAppender nicht funktioniert.
    Achtung: Die LogDatei kann hier beliebig groß werden!
 */

public class UnityFileAppender : AppenderSkeleton
{
    private string debugLevel;

    protected override void Append(LoggingEvent loggingEvent)
    {
        string message = RenderLoggingEvent(loggingEvent);

        writeToFile(message);


    }

    void writeToFile(string message) {
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter("Logs/DebugLog.txt", true))
             
        {
            file.WriteLine(message);
        }
    }
}