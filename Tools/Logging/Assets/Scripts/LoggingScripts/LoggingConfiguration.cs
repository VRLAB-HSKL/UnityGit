using System.IO;
using log4net.Config;
using UnityEngine;

/// <summary> 
/// Klasse zum Laden der xml-Konfigurationsdatei.
/// Stellt sicher, dass die Konfiguration geladen wird.
/// 
/// Quelle: https://www.linkedin.com/pulse/advanced-logging-unity-log4net-charles-amat
/// </summary>
public static class LoggingConfiguration
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ConfigureLogging()
    {
        XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo($"{Application.dataPath}/Resources/log4netConfig.xml"));        
    }
}