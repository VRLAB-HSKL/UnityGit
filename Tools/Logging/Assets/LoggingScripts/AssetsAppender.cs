using UnityEngine;
using log4net.Appender;
using log4net.Core;

/// <summary>
/// File Appender für Unity, mit der wir den Dateinamen
/// in der Klasse vereinbaren können.
/// 
/// Als Default wird das Verzeichnis StreamingAssets eingesetzt.
/// Es muss darauf geachtet werden, dass im Editor ein Verzeichnis
/// angegeben wird in das der Editor schreibt!
/// 
/// In Android-Anwendungen befindet sich dieses Verzeichnis
/// laut Unity-Dokumentation im apk-file!
/// </summary>
public class AssetsAppender : AppenderSkeleton
{
    /// <summary>
    /// Instanz von StreamWriter mit Pfad und Dateinamen.
    /// 
    /// Wir verwenden StramingsAssets.
    /// </summary>
    private System.IO.StreamWriter file = new System.IO.StreamWriter($"{Application.streamingAssetsPath}/LogOutput.txt", true);

    /// <summary>
    /// Überschreiben der Append-Funktion
    /// </summary>
    /// <param name="loggingEvent">Daten des Events aus log4net</param>
    protected override void Append(LoggingEvent loggingEvent)
    {
        string message = RenderLoggingEvent(loggingEvent);

        writeToFile(message);
    }

    /// <summary>
    /// Ausgabe in Datei mit Hilfe von WriteLine
    /// </summary>
    /// <param name="message">String der geschrieben werden soll</param>
    private void writeToFile(string message) 
    {
            file.WriteLine(message);
    }
}