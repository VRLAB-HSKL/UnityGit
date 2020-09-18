using UnityEngine;
// Plugin für die Kommunikation mit einem VRPN-Server
using static VRPN;

/// <summary>
/// Verbindung mit VRPN aufbauen
/// und die Werte eines Trackers ausgeben.
///
/// Voraussetzung dieser Klasse ist, dass
/// wir einen VRPN-Server erreichen.
/// </summary>
public class SimpleTracker : MonoBehaviour
{
    /// <summary>
    /// Channel des Trackers
    /// 
    /// Damit legen wir fest, welchen Sensor wir abfragen.
    /// </summary>
    public int trackerChannel = 0;
    /// <summary>
    /// Name des Trackers in der Konfiguration des VRPN-Servers
    /// </summary>
    public string trackerDevice = "Tracker0";
    /// <summary>
    /// Hostname des VRPN-Servers
    /// </summary>
    public string vrpnServer = "localhost";
    /// <summary>
    /// Variable für die VRPN-Aufrufe
    /// 
    /// Es gilt vrpnDevice = trackerDevice@vrpnServer.
    /// </summary>
    private string vrpnDevice;
        
    /// <summary>
    /// Device-Name für VRPN zusammensetzen
    /// </summary>
    private void Awake()
    {
        // Device-Name zusammensetzen
        vrpnDevice = trackerDevice + "@" + vrpnServer;
        Debug.Log(">>> Awake");
        Debug.Log("-- Awake: > " + vrpnDevice);
        Debug.Log("<<< Awake");
    }

    /// <summary>
    /// Daten vom Server abfragen und auf der Konsole ausgeben!
    /// 
    /// Wir geben die Rotation, die als Quaternion zurückgegeben wird,
    /// als Quaternion, mit Hilfe von Euler-Winkel und als "Axis-Angle", 
    /// also als Drehwinkel und Drehachse, aus!
    /// </summary>
    private void Update()
    {
        Vector3 trackerPos = vrpnTrackerPos(vrpnDevice, trackerChannel);
        Quaternion trackerQuat = vrpnTrackerQuat(vrpnDevice, trackerChannel);
        Vector3 euler = trackerQuat.eulerAngles;
        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        trackerQuat.ToAngleAxis(out angle,out axis);

        Debug.Log(">>> Update");
        Debug.Log("-- Update: > Trackerposition > " + trackerPos);
        Debug.Log("-- Update: > Tracker Quaternion > " + trackerQuat.ToString());
        Debug.Log("-- Update: > Tracker Quaternion Eulerwinkel > " + euler.ToString());
        Debug.Log("-- Update: > Tracker Quaternion Drehwinkel > " + angle);
        Debug.Log("-- Update: > Tracker Quaternion Drehachse > " + axis.ToString());
        Debug.Log("<<< Update");
    }
}
