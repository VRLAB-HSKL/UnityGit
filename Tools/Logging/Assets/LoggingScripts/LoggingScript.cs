using UnityEngine;
using log4net;


/// <summary> 
/// Klasse zur Demonstration von log4net in Unity.
/// Gibt alle Log-Level einmal auf der Konsole aus
/// 
/// </summary>
public class LoggingScript : MonoBehaviour
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(LoggingScript));

    private void Start()
    {
        Log.Fatal("Ausgabe mit Fatal");
        Log.Error("Ausgabe mit Error");
        Log.Warn("Ausgabe mit Warn");
        Log.Info("Ausgabe mit Info");
        Log.Debug("Ausgabe mit Debug");
    }
}