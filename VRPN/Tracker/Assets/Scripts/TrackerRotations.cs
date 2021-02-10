using UnityEngine;

// Plugin für die Kommunikation mit einem VRPN-Server
using static VRPN;

/// <summary>
/// Verbindung mit VRPN aufbauen
/// und die Werte eines Trackers abfragen.
/// 
/// Wir verwenden den in der VRPN-Distritubion
/// enthaltenen Dummy-Tracker vrpn_Tracker_Spin,
/// der als Default eine Rotation um die y-Achse
/// übertragt.
/// </summary>
public class TrackerRotations : MonoBehaviour
{
    /// <summary>
    /// Schalter, um die Rotation anzuwenden oder zu stoppen
    /// </summary>
    public bool on = true;
    /// <summary>
    /// Geschwindigkeit der Rotation
    /// </summary>  
    public float speed = 5.0f;
    /// <summary>
    /// Wollen wir Logging-Ausgaben in Awake?
    /// </summary>
    public bool tracing = false;
    /// <summary>
    /// Wollen wir die VRPN-Ausgaben des Trackers in Update protokollieren?
    /// </summary>
    public bool logVRPN = true;
    /// <summary>
    /// Channel des Trackers
    /// 
    /// Damit legen wir fest, welchen Sensor wir abfragen.
    /// </summary>
    public int trackerChannel = 0;
    /// <summary>
    /// Name des Trackers in der Konfiguration des VRPN-Servers
    /// </summary>
    public string trackerDevice = "Tracker1";
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
        if (tracing)
        {
            Debug.Log(">>> Awake");
            Debug.Log("-- Awake: > " + vrpnDevice);
            Debug.Log("<<< Awake");
        }
    }

    /// <summary>
    /// Daten vom Server abfragen und auf der Konsole ausgeben!
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            on = !on;
        Vector3 trackerPos = vrpnTrackerPos(vrpnDevice, trackerChannel);
        Quaternion trackerQuat = vrpnTrackerQuat(vrpnDevice, trackerChannel);
        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        trackerQuat.ToAngleAxis(out angle, out axis);


        if (logVRPN)
        {
            Debug.Log(">>> Update");
            Debug.Log("-- Update: > Trackerposition > " + trackerPos);
            Debug.Log("-- Update: > Tracker Quaternion Drehwinkel > " + angle);
            Debug.Log("-- Update: > Tracker Quaternion Drehachse > " + axis.ToString());
            Debug.Log("<<< Update");
        }

        // Neue Rotation setzen
        if (on)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  trackerQuat,
                                                  Time.deltaTime * speed);
        }
    }
}
