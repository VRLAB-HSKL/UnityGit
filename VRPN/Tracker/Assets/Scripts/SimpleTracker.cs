using UnityEngine;
// Plugin für die Kommunikation mit einem VRPN-Server
using static VRPN;

/// <summary>
/// Verbindung mit VRPN aufbauen
/// und die Werte eines Trackers ausgeben.
/// </summary>
/// <remarks>
/// Voraussetzung dieser Klasse ist, dass
/// wir einen VRPN-Server erreichen,
/// der einen der Dummy-Tracker (Tracker0)
/// anbietet.
/// </remarks>
public class SimpleTracker : MonoBehaviour
{
    /// <summary>
    /// Hostname des VRPN-Servers
    /// </summary>
    [Header("VRPN Server und Devices")]
    [Tooltip("VRPN Server")]
    public string VRPNServer = "localhost";
    /// <summary>
    /// Name des Trackers in der Konfiguration des VRPN-Servers
    /// </summary>
    [Tooltip("Name des Trackers")]
    public string TrackerDevice = "Tracker0";
    /// <summary>
    /// Channel des Trackers
    /// 
    /// Damit legen wir fest, welchen Sensor wir abfragen.
    /// </summary>
    [Tooltip("Tracker Channel")]
    public int TrackerChannel = 0;

    /// <summary>
    /// Variable für die VRPN-Aufrufe
    /// 
    /// Es gilt vrpnDevice = TrackerDevice@VRPNServer.
    /// </summary>
    private string vrpnDevice;
        
    /// <summary>
    /// Device-Name für VRPN zusammensetzen
    /// </summary>
    private void Awake()
    {
        // Device-Name zusammensetzen
        vrpnDevice = TrackerDevice + "@" + VRPNServer;
        Debug.Log(">>> Awake");
        Debug.Log("-- Fracker-Device und Server > " + vrpnDevice);
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
        Vector3 trackerPos = vrpnTrackerPos(vrpnDevice, TrackerChannel);
        Quaternion trackerQuat = vrpnTrackerQuat(vrpnDevice, TrackerChannel);

        Debug.Log(">>> Update");
        Debug.Log("-- Update: > Trackerposition > " + trackerPos);
        Debug.Log("-- Update: > Tracker Quaternion > " + trackerQuat.ToString());
        Debug.Log("<<< Update");
    }
}
