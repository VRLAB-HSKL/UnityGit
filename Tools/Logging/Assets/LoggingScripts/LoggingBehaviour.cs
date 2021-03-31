using UnityEngine;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Config;
//using System.Diagnostics;

/// <summary>
/// Beispiel für die Verwendung von Log4Net.
/// 
/// Diese Klasse ist von Monobehaviour abgeleitet und kann
/// einem Game Object hinzugefügt werden!
/// </summary>
public class LoggingBehaviour : MonoBehaviour
{
    /// <summary>
    /// Logger-Instanz erzeugen
    /// </summary>
    private static readonly ILog Log = LogManager.GetLogger(typeof(LoggingBehaviour));

    private void Awake()
    {
        // Konfigurieren
        // Man sollte diese Funktion ConfigureAllLogging an eine andere Stelle bringen,
        // damit wir das zentral machen, unabhängig von einem Behaviour.
        ConfigureAllLogging();

        Log.Debug(">>> Awake");
        Log.Info("Log-Ausgaben mit allen verfügbaren Ausgabenfunktionen");
        // Einfach mal alle Ausgaben auf die Konsole ...
        Log.Info("Eine Ausgabe mit Info");
        Log.Warn("Eine Ausgabe mit Warn");
        Log.Debug("Eine Ausgabe mit Debug");
        Log.Error("Eine Ausgabe mit Error");
        Log.Fatal("Eine Ausgabe mit Fatal");
        Log.Debug("<<< Awake");
    }


    /// <summary>
    ///  Configure logging to write to Logs\EventLog.txt and the Unity console output.
    /// </summary>
    public static void ConfigureAllLogging()
    {
        var patternLayout = new PatternLayout
        {
            ConversionPattern = "%date %-5level %logger - %message%newline"
        };
        patternLayout.ActivateOptions();
  
        var unityLogger = new UnityAppender
        {
            Layout = new PatternLayout()
        };
        unityLogger.ActivateOptions();
    }
}
